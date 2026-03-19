Imports System.Data
Imports System.Drawing
Imports System.Drawing.Printing
'Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class FrmProddespatchprn
    Dim msql, sql1 As String
    Dim n As Integer
    Dim selectedDocEntries As New List(Of String)
    Dim selectedDocNums As New List(Of String)
    Private WithEvents PrintDocument1 As New Printing.PrintDocument
    Private currentRow As Integer = 0
    Private itemsPerPage As Integer = 0
    Private pageNumber As Integer = 1
    Dim docEntryList As String
    Dim docnumlist As String
    Private grandTotal As Integer = 0
    Dim cTotalQty As Integer = 0

    Private Sub FrmProddespatchprn_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call loadparty()
    End Sub
    Private Sub loadparty()
        Dim qry As String = "select cardcode,cardname from ocrd where groupcode in (112,126)  and validfor='Y'"
        Dim dt As DataTable = getDataTable(qry)
        cmbparty.Items.Clear()
        cmbparty.DataSource = Nothing
        cmbparty.DataSource = dt
        cmbparty.DisplayMember = "cardname"   ' 👁️ what user sees
        cmbparty.ValueMember = "cardcode"       ' 💾 actual value
        cmbparty.SelectedIndex = -1
    End Sub

    Private Sub loaddata()
        dg.Rows.Clear()
        Dim totqty As Integer = 0
        'msql = "declare @d1 as nvarchar(20) " _
        '     & " declare @d2 as nvarchar(20) " _
        '     & " set @d1='" & dtpfr.Value.ToString("yyyy-MM-dd") & "' " _
        '     & " set @d2='" & dtpto.Value.ToString("yyyy-MM-dd") & "' " _
        '     & " select 'False' sel, b.docnum,b.docentry,b.u_docdate,b.u_opercode, convert(int,sum(c.u_accpqty)) Totqty from [@inm_owip] b " _
        '     & " inner join [@inm_wip1] c on c.docentry=b.docentry " _
        '     & " where  b.u_docdate>=@d1 and b.u_docdate<=@d2 and b.U_OperCode='IRONGD' and c.u_accptwhs='GFINISH'  and isnull(b.u_process,'') not in ('Y')" _
        '     & " group by b.docnum,b.docentry,b.u_docdate,b.u_opercode order by b.docnum"

        msql = "declare @d1 as nvarchar(20) " _
            & " declare @d2 as nvarchar(20) " _
            & " set @d1='" & dtpfr.Value.ToString("yyyy-MM-dd") & "' " _
            & " set @d2='" & dtpto.Value.ToString("yyyy-MM-dd") & "' " _
            & " select 'False' sel, b.docnum,b.docentry,b.u_docdate,b.u_opercode, convert(int,sum(c.u_accpqty)) Totqty from [@inm_owip] b " _
            & " inner join [@inm_wip1] c on c.docentry=b.docentry " _
            & " where  b.u_docdate>=@d1 and b.u_docdate<=@d2 and b.U_OperCode='IRONGD'  and isnull(b.u_process,'') not in ('Y')" _
            & " group by b.docnum,b.docentry,b.u_docdate,b.u_opercode order by b.docnum"

        Dim dt As DataTable = getDataTable(msql)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                n = dg.Rows.Add
                dg.Rows(n).Cells(0).Value = rw("sel")
                dg.Rows(n).Cells(1).Value = rw("docnum")
                dg.Rows(n).Cells(2).Value = rw("docentry")
                dg.Rows(n).Cells(3).Value = rw("u_docdate")
                dg.Rows(n).Cells(4).Value = rw("u_opercode")
                dg.Rows(n).Cells(5).Value = rw("Totqty")
                totqty = totqty + rw("Totqty")
            Next
        End If
        lblmqty.Text = totqty
    End Sub

    Private Sub loaddata2()
        dg1.DataSource = Nothing
        'Dim selectedDocNums As New List(Of String)
        'Dim selectedDocEntries As New List(Of String)
        'Dim selectedDocDates As New List(Of String)

        For Each row As DataGridViewRow In dg.Rows
            If Convert.ToBoolean(row.Cells("sel").Value) = True Then
                selectedDocNums.Add(row.Cells("docnum").Value.ToString())
                selectedDocEntries.Add(row.Cells("docentry").Value.ToString())
            End If
        Next
        docEntryList = String.Join(",", selectedDocEntries)
        docnumlist = String.Join(",", selectedDocNums)
        n = 0
        'sql1 = "declare @d1 as nvarchar(20) " _
        '    & "declare @d2 as nvarchar(20) " _
        '    & " set @d1='" & dtpfr.Value.ToString("yyyy-MM-dd") & "' " _
        '    & " set @d2 ='" & dtpto.Value.ToString("yyyy-MM-dd") & "' "




        'sql1 = "select c.u_itemcode ItemCode,c.u_itemname ItemName,convert(int,sum(u_accpqty)) Qty,s.SONO,s.SOEntry,o.docdate SODate,'   ' Bno from [@inm_owip] b " _
        '  & " inner join [@inm_wip1] c on c.docentry=b.docentry " _
        '  & " inner join (select c.u_itemcode,c.u_itemname,c.U_WOEntry ,isnull(d.u_sono,0) SONO,isnull(d.U_SOEntry,0) SOEntry from [@inm_ofcp] b " _
        '  & " inner join [@inm_fcp2] c on c.docentry=b.docentry " _
        '  & " left join [@inm_fcp4] d on d.docentry=b.docentry and d.U_UniqID=c.LineId " _
        '  & "group by c.u_itemcode,c.u_itemname,c.u_woentry,isnull(d.u_sono,0),isnull(d.U_SOEntry,0))  s on s.u_woentry=c.U_WOEntry and s.u_itemcode=c.u_itemcode " _
        '  & " left join (select docnum,docentry,docdate from ordr where CardCode  IN ('C032204','C042852','C036350', 'C025103',	 'C036347','C059778')) o on o.docentry=s.soentry " _
        '  & " where b.docentry in (" & Trim(docEntryList) & ") and b.U_OperCode='IRONGD'  " _
        '  & " group by c.u_itemcode,c.u_itemname,s.SONO,s.SOEntry,o.docdate  having convert(int,sum(u_accpqty))>0"



        'sql1 = ";WITH AccpQty AS ( " _
        '    & " SELECT c.u_itemcode, c.u_itemname, c.u_woentry, SUM(CONVERT(INT, c.u_accpqty)) AS TotalQty  FROM [@inm_owip] b " _
        '    & " INNER JOIN [@inm_wip1] c     ON c.docentry = b.docentry " _
        '    & " WHERE b.docentry IN  ( " & Trim(docEntryList) & ")   AND b.U_OperCode = 'IRONGD' " _
        '    & " GROUP BY  c.u_itemcode, c.u_itemname, c.u_woentry), " _
        '    & " SOData AS (  " _
        '    & " SELECT c.u_itemcode,c.u_itemname,c.u_woentry,ISNULL(d.u_sono,'') AS SONO,ISNULL(d.U_SOEntry,0)  AS SOEntry, o.DocDate AS SODate, " _
        '    & "  ISNULL(o.DocTotal - o.PaidToDate,0) AS SOPending," _
        '    & " ROW_NUMBER() OVER (PARTITION BY c.u_itemcode, c.u_woentry ORDER BY o.DocDate, o.DocEntry) AS RN FROM [@inm_ofcp] b " _
        '    & " INNER JOIN [@inm_fcp2] c ON c.docentry = b.docentry " _
        '    & " LEFT JOIN [@inm_fcp4] d  ON d.docentry = b.docentry  AND d.U_UniqID = c.LineId " _
        '    & " LEFT JOIN ORDR o  ON o.DocEntry = d.U_SOEntry  AND o.CANCELED = 'N'  AND o.CardCode IN  ('C032204','C042852','C036350','C025103','C036347','C059778'))," _
        '    & " FinalData AS ( " _
        '    & " SELECT a.u_itemcode, a.u_itemname, a.u_woentry, s.SONO, s.SOEntry, s.SODate, a.TotalQty,ISNULL(s.SOPending,0) AS SOPending, " _
        '    & " SUM(ISNULL(s.SOPending,0)) OVER (PARTITION BY a.u_itemcode, a.u_woentry ORDER BY s.RN ROWS UNBOUNDED PRECEDING ) AS CumPending " _
        '    & " FROM AccpQty a " _
        '    & " LEFT JOIN SOData s ON s.u_itemcode = a.u_itemcode AND s.u_woentry  = a.u_woentry ) " _
        '    & " SELECT u_itemcode  AS ItemCode, u_itemname  AS ItemName, " _
        '    & "CASE WHEN SOEntry = 0 THEN TotalQty  WHEN TotalQty >= CumPending THEN SOPending " _
        '    & " WHEN TotalQty > (CumPending - SOPending)  THEN TotalQty - (CumPending - SOPending)   ELSE 0  END  Qty, " _
        '    & "  CASE WHEN SOEntry = 0 THEN 0 ELSE SONO END AS SONO, " _
        '    & " isnull(SOEntry,0) AS SOEntry, SODate, '   ' Bno FROM FinalData " _
        '    & " where  CASE WHEN SOEntry = 0 THEN TotalQty  WHEN TotalQty >= CumPending THEN SOPending " _
        '    & " WHEN TotalQty > (CumPending - SOPending)  THEN TotalQty - (CumPending - SOPending)   ELSE 0  END >0 " _
        '    & " ORDER BY  u_itemcode, ISNULL(SODate,'19000101'); "


        '** new 
        sql1 = "declare @cardcode as nvarchar(50) " _
                & " set @cardcode='" & cmbparty.SelectedValue.ToString.Trim & "' " _
                & ";WITH AccpQty AS (  " _
                & "   SELECT c.u_itemcode, c.u_itemname, c.u_woentry, SUM(CONVERT(INT, c.u_accpqty)) AS TotalQty  FROM [@inm_owip] b   " _
                & "   INNER JOIN [@inm_wip1] c     ON c.docentry = b.docentry  " _
                & "   WHERE b.docentry IN  (" & Trim(docEntryList) & ")   AND b.U_OperCode = 'IRONGD'  " _
                & "   GROUP BY  c.u_itemcode, c.u_itemname, c.u_woentry),  " _
                & "SOData AS (   " _
                & " SELECT c.u_itemcode,c.u_itemname,c.u_woentry,ISNULL(d.u_sono,'') AS SONO,ISNULL(d.U_SOEntry,0)  AS SOEntry, o.DocDate AS SODate,  " _
                & " isnull(o.balordqty,0) AS SOPending," _
                & " ROW_NUMBER() OVER (PARTITION BY c.u_itemcode, c.u_woentry ORDER BY o.DocDate, o.DocEntry) AS RN FROM [@inm_ofcp] b   " _
                & " INNER JOIN [@inm_fcp2] c ON c.docentry = b.docentry   " _
                & " LEFT JOIN [@inm_fcp4] d  ON d.docentry = b.docentry  AND d.U_UniqID = c.LineId   " _
                & " left join (Select T0.Docnum,T0.DocDate,T0.Docentry, t.u_brandgroup, t1.itemcode,t1.dscription as itemname,isnull(sum(T1.linetotal),0) [OrderTotal], " _
                & "		(isnull(sum(t1.quantity),0)-(isnull(t2.delqty,0)+isnull(t2.invqty,0))) as balordqty, " _
                & "		T0.Cardcode,T0.Cardname from ordr T0  " _
                & "		INNER JOIN rdr1 T1 on T0.DocEntry=T1.DocEntry and t1.treetype<>'I' " _
                & "		inner join oitm t on t.itemcode=t1.itemcode " _
                & "		left Join (select k.basetype,k.baseentry,k.baseline,k.itemcode,sum(k.delqty) delqty,sum(k.delamt) delamt,sum(k.Invqty) Invqty,sum(k.Invamt) Invamt from ( " _
                & "			select b.basetype, b.baseentry,b.baseline,b.itemcode,sum(b.quantity) delqty,sum(b.linetotal) Delamt,0 Invqty,0 Invamt from dln1 b " _
                & "			inner join odln c on c.docentry=b.docentry " _
                & "			where b.treetype<>'I' and b.basetype=17 " _
                & "			group by b.baseentry,b.baseline,b.itemcode,b.basetype " _
                & "			union all " _
                & "			select b.basetype, b.baseentry,b.baseline,b.itemcode,0 delqty,0 delamt,sum(b.quantity) Invqty,sum(b.linetotal) Invamt from inv1 b " _
                & "			inner join oinv c on c.docentry=b.docentry " _
                & "			where b.treetype<>'I' and b.basetype=17 " _
                & "			group by b.baseentry,b.baseline,b.itemcode,b.basetype) k " _
                & "			group by k.basetype,k.baseentry,k.baseline,k.itemcode) T2 on T2.baseentry=t0.docentry and t2.BaseLine=t1.LineNum  and t2.itemcode=t1.itemcode " _
                & "			where t0.cardcode in (@cardcode) and t0.DocStatus='O' and t0.CANCELED='N' " _
                & "						group by  T0.Docnum,T0.DocDate,t0.docentry,t.u_brandgroup, t1.itemcode,t1.dscription, isnull(t2.delamt,0) , isnull(T2.invamt,0), " _
                & "						isnull(t2.delqty,0),isnull(t2.invqty,0),T0.Cardcode,T0.Cardname " _
                & "						having (isnull(sum(t1.quantity),0)-(isnull(t2.delqty,0)+isnull(t2.invqty,0)))>0) o on o.DocEntry=d.U_SOEntry and o.itemcode=c.U_ItemCode)," _
                & "FinalData AS (   " _
                & "  SELECT a.u_itemcode, a.u_itemname, a.u_woentry, s.SONO, s.SOEntry, s.SODate, a.TotalQty,ISNULL(s.SOPending,0) AS SOPending,  " _
                & "  SUM(ISNULL(s.SOPending,0)) OVER (PARTITION BY a.u_itemcode, a.u_woentry ORDER BY s.RN ROWS UNBOUNDED PRECEDING ) AS CumPending   " _
                & "  FROM AccpQty a  " _
                & "  LEFT JOIN SOData s ON s.u_itemcode = a.u_itemcode AND s.u_woentry  = a.u_woentry )  " _
                & "   SELECT u_itemcode  AS ItemCode, u_itemname  AS ItemName,   " _
                & "  CASE WHEN SOEntry = 0 THEN TotalQty  WHEN TotalQty >= CumPending THEN SOPending  " _
                & "  WHEN TotalQty > (CumPending - SOPending)  THEN TotalQty - (CumPending - SOPending)   ELSE 0  END  Qty,   " _
                & "  CASE WHEN SOEntry = 0 THEN 0 ELSE SONO END AS SONO,  isnull(SOEntry,0) AS SOEntry, SODate, '   ' Bno FROM FinalData  " _
                & "  where  CASE WHEN SOEntry = 0 THEN TotalQty  WHEN TotalQty >= CumPending THEN SOPending  " _
                & "  WHEN TotalQty > (CumPending - SOPending)  THEN TotalQty - (CumPending - SOPending)   ELSE 0  END >0  " _
                & "  ORDER BY  u_itemcode, ISNULL(SODate,'19000101'); "




        Dim dt1 As DataTable = getDataTable(sql1)
        dg1.DataSource = dt1
        dgformat(dg1)
        LblTotqty.Text = Convert.ToInt32(dt1.Compute("Sum(Qty)", ""))

    End Sub

    Private Sub BtnDisp_Click(sender As System.Object, e As System.EventArgs) Handles BtnDisp.Click
        Call loaddata()
    End Sub

    Private Sub btnexit_Click(sender As System.Object, e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub BtnLoad_Click(sender As System.Object, e As System.EventArgs) Handles BtnLoad.Click
        If Len(Trim(cmbparty.Text)) > 0 Then
            Call loaddata2()
        Else
            MsgBox("Pls Select Party Name!")
        End If

    End Sub

    Private Sub btnprint_Click(sender As System.Object, e As System.EventArgs) Handles btnprint.Click

        If dg1.Rows.Count - 1 > 0 Then
            Dim pd As New PrintDocument


            'Set A4 Page
            pd.DefaultPageSettings.PaperSize = New PaperSize("A4", 827, 1169)
            pd.DefaultPageSettings.Margins = New Margins(40, 40, 40, 40)

            AddHandler pd.PrintPage, AddressOf PrintDocument1_PrintPage

            Dim dlg As New PrintDialog
            dlg.Document = pd

            If dlg.ShowDialog() = DialogResult.OK Then
                pageNumber = 1
                currentRow = 0
                pd.Print()
            End If
            Dim commands As New List(Of SqlCommand)
            msql = "update [@inm_owip] set u_process='Y' where docentry in (" & docEntryList & ")"
            Dim cmd As New SqlCommand(msql)
            commands.Add(cmd)
            Dim result As Boolean = ExecuteTransactionWithCommands(commands)

            If result Then
                'MsgBox("All records saved successfully!")
            Else
                MsgBox("Transaction failed. No data saved.")
            End If
        Else
            MsgBox("Pls Select Completion Number then Submit!")
        End If

    End Sub
    Private Sub DrawRightAligned(g As Graphics, text As String, f As Font, b As Brush, rightX As Integer, y As Integer)

        Dim w As Integer = CInt(g.MeasureString(text, f).Width)
        g.DrawString(text, f, b, rightX - w, y)
    End Sub




    'Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
    '    Dim g As Graphics = e.Graphics
    '    g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

    '    Dim left As Integer = e.MarginBounds.Left
    '    Dim top As Integer = e.MarginBounds.Top
    '    Dim rightLimit As Integer = e.MarginBounds.Right
    '    Dim y As Integer = top

    '    Dim fTitle As New Font("Arial", 18, FontStyle.Bold)
    '    Dim fSub As New Font("Arial", 12, FontStyle.Bold)
    '    Dim fBold As New Font("Arial", 10, FontStyle.Bold)
    '    Dim f10 As New Font("Arial", 10)
    '    Dim penBlack As New Pen(Color.Black, 1)
    '    Dim penLight As New Pen(Color.LightGray, 1)

    '    '============ Watermark ============
    '    Dim wmFont As New Font("Arial", 60, FontStyle.Bold)
    '    Dim wmBrush As New SolidBrush(Color.FromArgb(25, Color.Black))
    '    g.TranslateTransform(300, 300)
    '    g.RotateTransform(-30)
    '    g.DrawString("ATITHYA", wmFont, wmBrush, 0, 0)
    '    g.ResetTransform()

    '    '============ Header ============
    '    g.DrawString("ATITHYA CLOTHING COMPANY", fTitle, Brushes.Black, left, y)
    '    y += 35
    '    g.DrawString("Itemwise Consolidated Despatch Print", fSub, Brushes.Black, left, y)
    '    y += 25

    '    '============ Completion Nos (wrapped) ============
    '    Dim longText As String = "Completion Nos: " & docnumlist
    '    Dim wrapRect As New RectangleF(left, y, rightLimit - left, 2000)
    '    g.DrawString(longText, f10, Brushes.Black, wrapRect)
    '    y += CInt(g.MeasureString(longText, f10, rightLimit - left).Height) + 10

    '    '============ Date range ============
    '    g.DrawString("Date: " & dtpfr.Value.ToString("dd-MM-yyyy") & " To " & dtpto.Value.ToString("dd-MM-yyyy"), f10, Brushes.Black, left, y)
    '    y += 25

    '    '============ Column positions ============
    '    Dim col_Item As Integer = left
    '    Dim col_Qty As Integer = left + 300
    '    Dim col_SONo As Integer = left + 420
    '    Dim col_SOEntry As Integer = left + 520
    '    Dim col_SODate As Integer = left + 550
    '    Dim col_BNo As Integer = rightLimit - 80   'fits in page

    '    '============ Table header ============
    '    g.DrawLine(penBlack, left, y, rightLimit, y)
    '    y += 10

    '    g.DrawString("Item Name", fBold, Brushes.Black, col_Item, y)
    '    DrawRightAligned(g, "Qty", fBold, Brushes.Black, col_Qty, y)
    '    DrawRightAligned(g, "SONo", fBold, Brushes.Black, col_SONo, y)
    '    DrawRightAligned(g, "SOEntry", fBold, Brushes.Black, col_SOEntry, y)
    '    g.DrawString("SODate", fBold, Brushes.Black, col_SODate, y)
    '    g.DrawString("BNo", fBold, Brushes.Black, col_BNo, y)
    '    y += 20

    '    g.DrawLine(penBlack, left, y, rightLimit, y)
    '    y += 5

    '    '============ Print rows ============
    '    Dim rowHeight As Integer = 22
    '    Dim maxRowsPerPage As Integer = 30
    '    Dim count As Integer = 0
    '    Dim i As Integer = currentRow

    '    While i < dg1.Rows.Count
    '        If count >= maxRowsPerPage Then
    '            g.DrawLine(penBlack, left, y, rightLimit, y)
    '            g.DrawString("Page: " & pageNumber, f10, Brushes.Black, rightLimit - 80, e.MarginBounds.Bottom)

    '            pageNumber += 1
    '            currentRow = i
    '            e.HasMorePages = True
    '            Return
    '        End If

    '        Dim row = dg1.Rows(i)
    '        Dim itemName As String = row.Cells("ItemCode").Value.ToString() & "-" & row.Cells("ItemName").Value.ToString()
    '        Dim qty As Integer = CInt(row.Cells("Qty").Value)
    '        Dim sono As String = row.Cells("SONo").Value.ToString()
    '        Dim soentry As String = row.Cells("SOEntry").Value.ToString()
    '        'Dim sodate As String = If(IsDBNull(row.Cells("SODate").Value), "", CDate(row.Cells("SODate").Value).ToString("dd-MM-yyyy")))
    '        Dim sodate As String = ""

    '        If Not IsDBNull(row.Cells("SODate").Value) Then
    '            sodate = CDate(row.Cells("SODate").Value).ToString("dd-MM-yyyy")
    '        End If

    '        Dim bno As String = row.Cells("BNo").Value.ToString()

    '        'Update grand total
    '        grandTotal += qty

    '        'Draw row
    '        g.DrawString(itemName, f10, Brushes.Black, col_Item, y)
    '        DrawRightAligned(g, qty.ToString(), f10, Brushes.Black, col_Qty, y)
    '        DrawRightAligned(g, sono, f10, Brushes.Black, col_SONo, y)
    '        DrawRightAligned(g, soentry, f10, Brushes.Black, col_SOEntry, y)
    '        g.DrawString(sodate, f10, Brushes.Black, col_SODate, y)
    '        g.DrawString(bno, f10, Brushes.Black, col_BNo, y)

    '        y += rowHeight
    '        g.DrawLine(penLight, left, y, rightLimit, y)
    '        y += 3

    '        count += 1
    '        i += 1
    '    End While

    '    '============ End of last page ============
    '    g.DrawLine(penBlack, left, y, rightLimit, y)
    '    y += 15
    '    g.DrawString("GRAND TOTAL: " & grandTotal.ToString(), fSub, Brushes.Black, rightLimit - 200, y)
    '    y += 25
    '    g.DrawString("Page: " & pageNumber, f10, Brushes.Black, rightLimit - 80, e.MarginBounds.Bottom)

    '    'Reset for next print
    '    e.HasMorePages = False
    '    pageNumber = 1
    '    currentRow = 0
    '    grandTotal = 0
    'End Sub

    Private Sub DrawColumnLines(g As Graphics, pen As Pen, topY As Integer, bottomY As Integer,
                            left As Integer, col_Qty As Integer, col_SONo As Integer,
                            col_SOEntry As Integer, col_SODate As Integer,
                            col_BNo As Integer, rightLimit As Integer)

        g.DrawLine(pen, left, topY, left, bottomY)
        g.DrawLine(pen, col_Qty, topY, col_Qty, bottomY)
        g.DrawLine(pen, col_SONo, topY, col_SONo, bottomY)
        g.DrawLine(pen, col_SOEntry, topY, col_SOEntry, bottomY)
        g.DrawLine(pen, col_SODate, topY, col_SODate, bottomY)
        g.DrawLine(pen, col_BNo, topY, col_BNo, bottomY)
        g.DrawLine(pen, rightLimit, topY, rightLimit, bottomY)

    End Sub

    Private Sub DrawCentered(g As Graphics, text As String, font As Font, brush As Brush, xLeft As Integer, xRight As Integer, y As Integer)
        Dim areaWidth As Integer = xRight - xLeft
        Dim textSize As SizeF = g.MeasureString(text, font)
        Dim xCenter As Integer = xLeft + (areaWidth - textSize.Width) \ 2

        g.DrawString(text, font, brush, xCenter, y)
    End Sub

    Private Sub DrawLeftInCell(g As Graphics, text As String, font As Font, brush As Brush, xLeft As Integer, xRight As Integer, y As Integer)
        Dim rect As New RectangleF(xLeft + 2, y, xRight - xLeft - 4, font.Height + 4)
        Dim sf As New StringFormat()
        sf.Alignment = StringAlignment.Near
        sf.LineAlignment = StringAlignment.Near
        sf.Trimming = StringTrimming.EllipsisCharacter
        sf.FormatFlags = StringFormatFlags.LineLimit
        g.DrawString(text, font, brush, rect, sf)
    End Sub

    Private Sub DrawRightInCell(g As Graphics, text As String, font As Font, brush As Brush, xLeft As Integer, xRight As Integer, y As Integer)
        Dim rect As New RectangleF(xLeft + 2, y, xRight - xLeft - 4, font.Height + 4)
        Dim sf As New StringFormat()
        sf.Alignment = StringAlignment.Far
        sf.LineAlignment = StringAlignment.Near
        sf.Trimming = StringTrimming.EllipsisCharacter
        g.DrawString(text, font, brush, rect, sf)
    End Sub

    'Private Sub DrawCentered(g As Graphics, text As String, font As Font, brush As Brush, xLeft As Integer, xRight As Integer, y As Integer)

    '    Dim cellWidth As Integer = xRight - xLeft
    '    Dim textSize As SizeF = g.MeasureString(text, font)
    '    Dim x As Integer = xLeft + (cellWidth - textSize.Width) / 2

    '    g.DrawString(text, font, brush, x, y)
    'End Sub


    'Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage

    '    Dim g As Graphics = e.Graphics
    '    g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

    '    Dim left As Integer = e.MarginBounds.Left
    '    Dim top As Integer = e.MarginBounds.Top
    '    Dim rightLimit As Integer = e.MarginBounds.Right
    '    Dim y As Integer = top

    '    Dim fTitle As New Font("Arial", 18, FontStyle.Bold)
    '    Dim fSub As New Font("Arial", 12, FontStyle.Bold)
    '    Dim fBold As New Font("Arial", 10, FontStyle.Bold)
    '    Dim f10 As New Font("Arial", 10)
    '    Dim penBlack As New Pen(Color.Black, 1)
    '    Dim penBlack1 As New Pen(Color.Black, 0.75)
    '    Dim penLight As New Pen(Color.LightGray, 1)

    '    '============ Watermark ============
    '    Dim wmFont As New Font("Arial", 60, FontStyle.Bold)
    '    Dim wmBrush As New SolidBrush(Color.FromArgb(25, Color.Black))
    '    g.TranslateTransform(300, 300)
    '    g.RotateTransform(-30)
    '    g.DrawString("ATITHYA", wmFont, wmBrush, 0, 0)
    '    g.ResetTransform()

    '    '============ Header ============
    '    g.DrawString("ATITHYA CLOTHING COMPANY", fTitle, Brushes.Black, left, y)
    '    y += 35
    '    g.DrawString("Itemwise Consolidated Despatch Print", fSub, Brushes.Black, left, y)
    '    y += 25

    '    Dim longText As String = "Completion Nos: " & docnumlist
    '    Dim wrapRect As New RectangleF(left, y, rightLimit - left, 2000)
    '    g.DrawString(longText, f10, Brushes.Black, wrapRect)
    '    y += CInt(g.MeasureString(longText, f10, rightLimit - left).Height) + 10

    '    g.DrawString("Date: " & dtpfr.Value.ToString("dd-MM-yyyy") & " To " & dtpto.Value.ToString("dd-MM-yyyy"), f10, Brushes.Black, left, y)
    '    y += 25

    '    '============ Column positions ============
    '    'Dim col_Item As Integer = left
    '    'Dim col_Qty As Integer = left + 400
    '    'Dim col_SONo As Integer = left + 500
    '    'Dim col_SOEntry As Integer = left + 600
    '    'Dim col_SODate As Integer = left + 601
    '    'Dim col_BNo As Integer = rightLimit - 70


    '    'Dim col_Item As Integer = left
    '    'Dim col_Qty As Integer = left + 300    'Item width = 300
    '    'Dim col_SONo As Integer = left + 380   'SONo width = 80
    '    'Dim col_SOEntry As Integer = left + 460
    '    'Dim col_SODate As Integer = left + 540
    '    'Dim col_BNo As Integer = rightLimit - 70


    '    Dim w_Item As Integer = 300
    '    Dim w_Qty As Integer = 80
    '    Dim w_SONo As Integer = 80
    '    Dim w_SOEntry As Integer = 80
    '    Dim w_SODate As Integer = 80

    '    Dim col_Item As Integer = left
    '    Dim col_Qty As Integer = col_Item + w_Item
    '    Dim col_SONo As Integer = col_Qty + w_Qty
    '    Dim col_SOEntry As Integer = col_SONo + w_SONo
    '    Dim col_SODate As Integer = col_SOEntry + w_SOEntry
    '    Dim col_BNo As Integer = rightLimit - 80



    '    '============ Column Header ============
    '    Dim headerTop As Integer = y

    '    g.DrawLine(penBlack, left, y, rightLimit, y)
    '    y += 10

    '    DrawColumnLines(g, penBlack, headerTop, headerTop + 20, left, col_Qty, col_SONo, col_SOEntry, col_SODate, col_BNo, rightLimit)

    '    'g.DrawString("Item Name", fBold, Brushes.Black, col_Item, y)
    '    'DrawRightAligned(g, "Qty", fBold, Brushes.Black, col_Qty, y)
    '    'DrawRightAligned(g, "SONo", fBold, Brushes.Black, col_SONo, y)
    '    'DrawRightAligned(g, "SOEntry", fBold, Brushes.Black, col_SOEntry, y)
    '    'g.DrawString("SODate", fBold, Brushes.Black, col_SODate, y)
    '    'g.DrawString("BNo", fBold, Brushes.Black, col_BNo, y)


    '    'DrawCentered(g, "Item Name", fBold, Brushes.Black, col_Item, col_Qty, y)
    '    'DrawCentered(g, "Qty", fBold, Brushes.Black, col_Qty, col_SONo, y)
    '    'DrawCentered(g, "SONo", fBold, Brushes.Black, col_SONo, col_SOEntry, y)
    '    'DrawCentered(g, "SOEntry", fBold, Brushes.Black, col_SOEntry, col_SODate, y)
    '    'DrawCentered(g, "SODate", fBold, Brushes.Black, col_SODate, col_BNo, y)
    '    'DrawCentered(g, "BNo", fBold, Brushes.Black, col_BNo, rightLimit, y)


    '    DrawCentered(g, "Item Name", fBold, Brushes.Black, col_Item, col_Qty, y)
    '    DrawCentered(g, "Qty", fBold, Brushes.Black, col_Qty, col_SONo, y)
    '    DrawCentered(g, "SONo", fBold, Brushes.Black, col_SONo, col_SOEntry, y)
    '    DrawCentered(g, "SOEntry", fBold, Brushes.Black, col_SOEntry, col_SODate, y)
    '    DrawCentered(g, "SODate", fBold, Brushes.Black, col_SODate, col_BNo, y)
    '    DrawCentered(g, "BNo", fBold, Brushes.Black, col_BNo, rightLimit, y)



    '    y += 20
    '    g.DrawLine(penBlack, left, y, rightLimit, y)

    '    Dim headerBottom As Integer = y
    '    y += 5

    '    '--- Draw Vertical Lines For Header ---
    '    DrawColumnLines(g, penBlack, headerTop, headerBottom, left, col_Qty, col_SONo, col_SOEntry, col_SODate, col_BNo, rightLimit)

    '    '============ Print rows ============
    '    Dim rowHeight As Integer = 22
    '    Dim maxRowsPerPage As Integer = 30
    '    Dim count As Integer = 0
    '    Dim i As Integer = currentRow

    '    While i < dg1.Rows.Count

    '        If count >= maxRowsPerPage Then
    '            g.DrawLine(penBlack, left, y, rightLimit, y)
    '            g.DrawString("Page: " & pageNumber, f10, Brushes.Black, rightLimit - 80, e.MarginBounds.Bottom)
    '            pageNumber += 1
    '            currentRow = i
    '            e.HasMorePages = True
    '            Return
    '        End If

    '        Dim rowTop As Integer = y

    '        Dim row = dg1.Rows(i)
    '        Dim itemName As String = row.Cells("ItemCode").Value.ToString() & "-" & row.Cells("ItemName").Value.ToString()
    '        Dim qty As Integer = CInt(row.Cells("Qty").Value)
    '        Dim sono As String = row.Cells("SONo").Value.ToString()
    '        Dim soentry As String = row.Cells("SOEntry").Value.ToString()
    '        Dim sodate As String = ""

    '        If Not IsDBNull(row.Cells("SODate").Value) Then
    '            sodate = CDate(row.Cells("SODate").Value).ToString("dd-MM-yyyy")
    '        End If

    '        Dim bno As String = row.Cells("BNo").Value.ToString()

    '        grandTotal += qty

    '        g.DrawString(itemName, f10, Brushes.Black, col_Item, y)
    '        DrawRightAligned(g, qty.ToString(), f10, Brushes.Black, col_Qty, y)
    '        DrawRightAligned(g, sono, f10, Brushes.Black, col_SONo, y)
    '        DrawRightAligned(g, soentry, f10, Brushes.Black, col_SOEntry, y)
    '        g.DrawString(sodate, f10, Brushes.Black, col_SODate, y)
    '        g.DrawString(bno, f10, Brushes.Black, col_BNo, y)


    '        y += rowHeight
    '        Dim rowBottom As Integer = y

    '        g.DrawLine(penLight, left, y, rightLimit, y)
    '        y += 3

    '        '--- Draw Vertical Lines For Each Row ---
    '        'DrawColumnLines(g, penLight, rowTop, rowBottom, left, col_Qty, col_SONo, col_SOEntry, col_SODate, col_BNo, rightLimit)
    '        DrawColumnLines(g, penblack1, rowTop, rowBottom, left, col_Qty, col_SONo, col_SOEntry, col_SODate, col_BNo, rightLimit)

    '        count += 1
    '        i += 1
    '    End While

    '    '============ End Page ============
    '    g.DrawLine(penBlack, left, y, rightLimit, y)
    '    y += 15

    '    g.DrawString("GRAND TOTAL: " & grandTotal.ToString(), fSub, Brushes.Black, rightLimit - 200, y)
    '    y += 25
    '    g.DrawString("Page: " & pageNumber, f10, Brushes.Black, rightLimit - 80, e.MarginBounds.Bottom)

    '    e.HasMorePages = False
    '    pageNumber = 1
    '    currentRow = 0
    '    grandTotal = 0

    'End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs)

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        Dim left As Integer = e.MarginBounds.Left
        Dim top As Integer = e.MarginBounds.Top
        Dim rightLimit As Integer = e.MarginBounds.Right
        Dim y As Integer = top

        '===== Fonts =====
        Dim fTitle As New Font("Arial", 18, FontStyle.Bold)
        Dim fSub As New Font("Arial", 12, FontStyle.Bold)
        Dim fBold As New Font("Arial", 10, FontStyle.Bold)
        Dim f10 As New Font("Arial", 10)

        Dim penBlack As New Pen(Color.Black, 1)
        Dim penThin As New Pen(Color.Black, 0.75)
        Dim penLight As New Pen(Color.LightGray, 1)

        '===== Watermark =====
        'Dim wmFont As New Font("Arial", 60, FontStyle.Bold)
        'Dim wmBrush As New SolidBrush(Color.FromArgb(25, Color.Black))
        'g.TranslateTransform(300, 300)
        'g.RotateTransform(-30)
        'g.DrawString("ATITHYA", wmFont, wmBrush, 0, 0)
        'g.ResetTransform()


        Dim title1 As String = "ATITHYA CLOTHING COMPANY"
        Dim title1Width As Single = g.MeasureString(title1, fTitle).Width
        Dim centerX As Single = e.MarginBounds.Left + ((e.MarginBounds.Width - title1Width) / 2)
        g.DrawString(title1, fTitle, Brushes.Black, centerX, y)

        y += 35

        '============ Center Line 2 ============
        Dim title2 As String = "Itemwise Consolidated Despatch Print"
        Dim title2Width As Single = g.MeasureString(title2, fSub).Width
        centerX = e.MarginBounds.Left + ((e.MarginBounds.Width - title2Width) / 2)
        g.DrawString(title2, fSub, Brushes.Black, centerX, y)
        y += 30

        Dim title3 As String = cmbparty.Text.ToString.Trim
        Dim title3Width As Single = g.MeasureString(title3, fSub).Width
        centerX = e.MarginBounds.Left + ((e.MarginBounds.Width - title3Width) / 2)
        g.DrawString(title3, fSub, Brushes.Black, centerX, y)
        y += 25

        g.DrawLine(penBlack, left, y, rightLimit, y)
        y += 10
        'Dim longText As String = "Completion Nos: " & docnumlist
        'y += 10
        g.DrawString("Completion Nos: ", fBold, Brushes.Black, left, y)
        Dim longText As String = docnumlist
        ''**in windows
        'If mos = "WIN" Then
        Dim wrapRect As New RectangleF(left + 130, y, rightLimit - left, 2000)
        g.DrawString(longText, f10, Brushes.Black, wrapRect)
        y += CInt(g.MeasureString(longText, f10, rightLimit - left).Height) + 10
        'Else
        '    Dim textWidth As Integer = rightLimit - (left + 130)
        '    Dim textSize As SizeF = g.MeasureString(longText, f10, textWidth)
        '    Dim wrapRect As New RectangleF(left + 130, y, textWidth, textSize.Height)
        '    g.DrawString(longText, f10, Brushes.Black, wrapRect)
        '    y += CInt(textSize.Height) + 10
        '    'y += 25
        'End If

        ''*** linux
        'Dim textWidth As Integer = rightLimit - (left + 130)
        'Dim textSize As SizeF = g.MeasureString(longText, f10, textWidth)
        'Dim wrapRect As New RectangleF(left + 130, y, textWidth, textSize.Height)
        'g.DrawString(longText, f10, Brushes.Black, wrapRect)
        'y += CInt(textSize.Height) + 10



        g.DrawString("Date: " & dtpfr.Value.ToString("dd-MM-yyyy") &
                     " To " & dtpto.Value.ToString("dd-MM-yyyy"),
                     fBold, Brushes.Black, left, y)
        y += 25

        '===== Column Widths =====
        Dim w_Item As Integer = 300
        Dim w_Qty As Integer = 80
        Dim w_SONo As Integer = 80
        Dim w_SOEntry As Integer = 80
        Dim w_SODate As Integer = 80

        '===== Column X Positions =====
        Dim col_Item As Integer = left
        Dim col_Qty As Integer = col_Item + w_Item
        Dim col_SONo As Integer = col_Qty + w_Qty
        Dim col_SOEntry As Integer = col_SONo + w_SONo
        Dim col_SODate As Integer = col_SOEntry + w_SOEntry
        Dim col_BNo As Integer = rightLimit - 80

        '===== Column Header =====
        Dim headerTop As Integer = y
        g.DrawLine(penBlack, left, y, rightLimit, y)
        y += 10

        DrawCentered(g, "Item Name", fBold, Brushes.Black, col_Item, col_Qty, y)
        DrawCentered(g, "Qty", fBold, Brushes.Black, col_Qty, col_SONo, y)
        DrawCentered(g, "SONo", fBold, Brushes.Black, col_SONo, col_SOEntry, y)
        DrawCentered(g, "SOEntry", fBold, Brushes.Black, col_SOEntry, col_SODate, y)
        DrawCentered(g, "SODate", fBold, Brushes.Black, col_SODate, col_BNo, y)
        DrawCentered(g, "BNo", fBold, Brushes.Black, col_BNo, rightLimit, y)

        y += 20
        g.DrawLine(penBlack, left, y, rightLimit, y)

        Dim headerBottom As Integer = y
        y += 5

        If mos = "WIN" Then
            DrawColumnLines(g, penBlack, headerTop, headerBottom, left,
                        col_Qty, col_SONo, col_SOEntry, col_SODate, col_BNo, rightLimit)
        Else
            g.DrawLine(penBlack, left, y, rightLimit, y)
        End If


        '===== Print Rows =====
        Dim rowHeight As Integer = 22
        Dim maxRows As Integer = 30
        Dim count As Integer = 0
        Dim i As Integer = currentRow

        While i < dg1.Rows.Count

            If count >= maxRows Then
                g.DrawLine(penBlack, left, y, rightLimit, y)
                g.DrawString("Page: " & pageNumber, f10, Brushes.Black, rightLimit - 80, e.MarginBounds.Bottom)
                pageNumber += 1
                currentRow = i
                e.HasMorePages = True
                Return
            End If

            Dim rowTop As Integer = y

            Dim row = dg1.Rows(i)
            Dim itemName As String = row.Cells("ItemCode").Value.ToString() & "--" &
                                     row.Cells("ItemName").Value.ToString()
            Dim qty As String = Convert.ToInt32(row.Cells("Qty").Value).ToString()
            Dim sono As String = Convert.ToInt32(row.Cells("SONo").Value).ToString()
            Dim soentry As String = Convert.ToInt32(row.Cells("SOEntry").Value).ToString()

            Dim sodate As String = ""
            If Not IsDBNull(row.Cells("SODate").Value) Then
                sodate = CDate(row.Cells("SODate").Value).ToString("dd-MM-yyyy")
            End If

            Dim bno As String = row.Cells("BNo").Value.ToString()

            grandTotal += CInt(qty)

            '==== DRAW DATA IN PERFECT COLUMNS ====
            DrawLeftInCell(g, itemName, f10, Brushes.Black, col_Item, col_Qty, y)
            DrawRightInCell(g, qty, f10, Brushes.Black, col_Qty, col_SONo, y)
            DrawRightInCell(g, sono, f10, Brushes.Black, col_SONo, col_SOEntry, y)
            DrawRightInCell(g, soentry, f10, Brushes.Black, col_SOEntry, col_SODate, y)
            DrawLeftInCell(g, sodate, f10, Brushes.Black, col_SODate, col_BNo, y)
            DrawLeftInCell(g, bno, f10, Brushes.Black, col_BNo, rightLimit, y)

            y += rowHeight
            Dim rowBottom As Integer = y

            g.DrawLine(penLight, left, y, rightLimit, y)
            y += 3
            If mos = "WIN" Then
                DrawColumnLines(g, penThin, rowTop, rowBottom, left,
                            col_Qty, col_SONo, col_SOEntry, col_SODate, col_BNo, rightLimit)
            End If


            count += 1
            i += 1
        End While

        '===== Footer =====
        g.DrawLine(penBlack, left, y, rightLimit, y)
        y += 15
        g.DrawString("GRAND TOTAL: " & grandTotal, fSub, Brushes.Black, rightLimit - 200, y)
        y += 25
        g.DrawString("Page: " & pageNumber, f10, Brushes.Black, rightLimit - 80, e.MarginBounds.Bottom)

        e.HasMorePages = False
        pageNumber = 1
        currentRow = 0
        grandTotal = 0

    End Sub
    Private Sub dgformat(dgv As DataGridView)
        dgv.EnableHeadersVisualStyles = False
        dgv.ScrollBars = ScrollBars.Both
        dgv.SelectionMode = DataGridViewSelectionMode.CellSelect
        'dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        'dgv.ColumnHeadersHeight = 120 '

        For Each col As DataGridViewColumn In dgv.Columns

            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            col.HeaderCell.Style.WrapMode = DataGridViewTriState.True
            col.HeaderCell.Style.Font = New Font("Arial", 9, FontStyle.Bold)
            col.HeaderCell.Style.BackColor = Color.SteelBlue
            col.HeaderCell.Style.ForeColor = Color.White

            'If col.Index > 0 Then
            '    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '    col.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
            '    col.DefaultCellStyle.Format = "0"

            'End If
            'If col.Index = 0 Then
            '    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '    col.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
            'End If

            With col.DefaultCellStyle
                If col.Index > 0 Then
                    '.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Font = New Font("Arial", 9, FontStyle.Regular)
                    If col.HeaderText.ToUpper().Contains("QTY") Or col.HeaderText.ToUpper().Contains("SONO") Or col.HeaderText.ToUpper().Contains("SOENTRY") Then
                        .Format = "0" ' Decimal with 2 digits
                        .Alignment = DataGridViewContentAlignment.MiddleRight
                        'Else
                        '    '.Format = "0" ' Integer
                        '    .Alignment = DataGridViewContentAlignment.MiddleLeft
                    End If
                    '.Format = "0"
                Else
                    .Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Font = New Font("Arial", 9, FontStyle.Regular)
                End If
                If col.HeaderText.Contains("ItemCode") Then
                    col.Width = 100
                ElseIf col.HeaderText.Contains("ItemName") Then
                    col.Width = 200
                ElseIf col.HeaderText.Contains("Qty") Then
                    col.Width = 60
                ElseIf col.HeaderText.Contains("SONO") Then
                    col.Width = 50
                ElseIf col.HeaderText.Contains("SOEntry") Then
                    col.Width = 100
                ElseIf col.HeaderText.Contains("SODate") Then
                    col.Width = 80
                ElseIf col.HeaderText.Contains("Bno") Then
                    col.Width = 60

                End If

            End With




            col.SortMode = DataGridViewColumnSortMode.NotSortable
            col.ReadOnly = True
        Next



    End Sub

    Private Sub chksel_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chksel.CheckedChanged
        If chksel.Checked = True Then
            For i As Integer = 0 To dg.Rows.Count - 1
                dg.Rows(i).Cells(0).Value = True
            Next
        Else
            For i As Integer = 0 To dg.Rows.Count - 1
                dg.Rows(i).Cells(0).Value = False
            Next
        End If
    End Sub

    Private Sub dg_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick

    End Sub

    Private Sub dg_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellValueChanged
        'If e.ColumnIndex = dg.Columns("Sel").Index Then  'checkbox column name = Select
        '    Dim isChecked As Boolean = CBool(dg.Rows(e.RowIndex).Cells("Sel").Value)
        '    Dim cqty As Integer = CInt(dg.Rows(e.RowIndex).Cells("Totqty").Value)

        '    If isChecked Then
        '        cTotalQty += cqty     'add
        '    Else
        '        cTotalQty -= cqty     'subtract
        '    End If

        '    lblselqty.Text = cTotalQty
        'End If

        If e.RowIndex >= 0 Then
            If TypeOf dg.Columns(e.ColumnIndex) Is DataGridViewCheckBoxColumn Then

                Dim isChecked As Boolean = CBool(dg.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                Dim cqty As Integer = CInt(dg.Rows(e.RowIndex).Cells("Totqty").Value)

                If isChecked Then
                    cTotalQty += cqty
                Else
                    cTotalQty -= cqty
                End If

                lblselqty.Text = cTotalQty

            End If
        End If

    End Sub

    Private Sub dg_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dg.CurrentCellDirtyStateChanged
        If TypeOf dg.CurrentCell Is DataGridViewCheckBoxCell Then
            dg.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
End Class
Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports Excel = Microsoft.Office.Interop.Excel

Public Class FrmGSTR1
    Dim msql, msql2, msql3, msql4, msql5, msql6, msql7 As String
    Dim location As String
    Dim i As Int32
    'Dim cmd As New OleDbCommand
    Dim xlapp As New Excel.Application
    Dim xlWorkBook As New Excel.Workbook
    Dim xlWorkSheet As New Excel.Worksheet
    Dim lstSheets As New List(Of String)
    Dim misValue As Object = System.Reflection.Missing.Value
    Dim chartRange As Excel.Range
   

    Private Sub FrmGSTR1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'lstsheet.Items.Add("b2b")
        'lstsheet.Items.Add("b2cl")
        'lstsheet.Items.Add("b2cs")
        'lstsheet.Items.Add("cdnr")
        'lstsheet.Items.Add("cdnur")
        'lstsheet.Items.Add("hsn")
        'lstsheet.Items.Add("docs")
        lstSheets.Add("b2b")
        lstSheets.Add("b2cl")
        lstSheets.Add("b2cs")
        lstSheets.Add("cdnr")
        lstSheets.Add("cdnur")
        lstSheets.Add("hsn")
        lstSheets.Add("docs")
    End Sub


    Private Sub SaveInfo(ByVal sCurrent As String)

        If sCurrent.ToLower = "b2b" Then
            msql = "DECLARE @d1 AS nvarchar(20) " & vbCrLf _
             & "DECLARE @d2 AS nvarchar(20)" & vbCrLf _
             & "SET @d1 = '" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' " & vbCrLf _
             & "SET @d2 = '" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "'" & vbCrLf _
            & "select * from " & vbCrLf _
            & "(select 2 as sno, cr.U_GSTIN 'GSTIN/UIN of Recipient',c.DocNum 'Invoice Number',c.docdate 'Invoice date', " & vbCrLf _
            & "c.DocTotal() 'Invoice Value',LEFT(rtrim(ltrim(cr.u_gstin)),2)+'-'+LTRIM(RTRIM(CS.name)) as 'Place Of Supply'," & vbCrLf _
            & "CASE when right(rtrim(TX.taxcode),1)='R' then 'Y' else 'N' end 'Reverse Charge','Regular' as 'Invoice Type'," & vbCrLf _
            & " '' as 'E-Commerce GSTIN',tx.ttaxrate 'Rate', b.taxpayable 'Taxable Value',0 as 'Cess Amount' from OINV c " & vbCrLf _
            & "left join (select docentry,sum(quantity) qty,sum(linetotal) taxpayable from inv1  group by docentry) b on b.docentry=c.docentry " & vbCrLf _
            & "Left Join" & vbCrLf _
            & "(select b.DocEntry,REPLACE(b.stccode,ltrim(rtrim(convert(nvarchar(10),ceiling(sc.rate)))),'') as taxcode," & vbCrLf _
            & " SC.rate ttaxrate, b.StcCode,sum(b.taxsum) taxamt from INV4 b " & vbCrLf _
            & "left join ostc sc on sc.code=b.stccode " & vbCrLf _
            & "left join ostt st on st.absid=b.statype " & vbCrLf _
            & "group by b.DocEntry,REPLACE(b.stccode,ltrim(rtrim(convert(nvarchar(10),ceiling(sc.rate)))),''),SC.rate, b.StcCode) tx on tx.DocEntry=c.DocEntry " & vbCrLf _
            & "left join OCRD cr on cr.CardCode=c.CardCode " & vbCrLf _
            & "left join (select code,name,country from ocst where country='IN') cs on cs.code=cr.state1  " & vbCrLf _
            & "where c.docdate>=@d1 and c.DocDate<= @d2 and (cr.qrygroup1 is null or cr.QryGroup1<>'Y') " & vbCrLf _
            & "union all " & vbCrLf _
            & "select 1 sno, convert(nvarchar(10),count(distinct cr.u_gstin)) nogst, COUNT(b.docnum) noinv,'1900-01-01', SUM(doctotal) invvalue, " & vbCrLf _
            & "'','','','',0,sum(c.taxpayable) ,0 from OINV b " & vbCrLf _
            & "left join (select docentry,sum(quantity) qty,sum(linetotal) taxpayable from inv1 group by docentry) c on c.docentry=b.docentry " & vbCrLf _
            & "left join (select tt.docentry, SUM(tt.taxsum) taxsum from INV4 tt " & vbCrLf _
            & "left join OSTC tc on tc.Code=tt.StcCode   " & vbCrLf _
            & "group by tt.DocEntry ) tx on tx.DocEntry=b.docentry " & vbCrLf _
            & "left join OCRD cr on cr.CardCode=b.cardcode " & vbCrLf _
            & " where b.DocDate>=@d1 and b.DocDate<=@d2 and (cr.qrygroup1 is null or cr.QryGroup1<>'Y')) k " & vbCrLf _
            & "order by k.sno,k.[Invoice Number]"
        ElseIf sCurrent.ToLower = "b2cl" Then

            msql = "DECLARE @d1 AS nvarchar(20)" & vbCrLf _
                   & " DECLARE @d2 AS nvarchar(20) " & vbCrLf _
                & "SET @d1 ='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' " & vbCrLf _
                & "SET @d2 ='" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' " & vbCrLf _
             & "select * from " & vbCrLf _
             & "(select 2 as sno,c.DocNum 'Invoice Number',c.docdate 'Invoice date',c.DocTotal() 'Invoice Value',LEFT(rtrim(ltrim(cr.u_gstin)),2)+'-'+LTRIM(RTRIM(CS.name)) as 'Place Of Supply'," & vbCrLf _
              & "tx.ttaxrate() 'Rate',b.taxablevalue 'Taxable Value',0 as 'Cess Amount','' as 'E-Commerce GSTIN' from OINV c " & vbCrLf _
             & "left join (select docentry,sum(quantity) qty,sum(linetotal) taxablevalue from inv1 group by docentry) b on b.docentry=c.docentry " & vbCrLf _
              & "Left Join " & vbCrLf _
             & "(select b.DocEntry, REPLACE(b.stccode,ltrim(rtrim(convert(nvarchar(10),ceiling(sc.rate)))),'') as taxcode," & vbCrLf _
              & "SC.rate ttaxrate, b.StcCode,sum(b.taxsum) taxamt from INV4 b " & vbCrLf _
              & "left join ostc sc on sc.code=b.stccode  " & vbCrLf _
              & "left join ostt st on st.absid=b.statype " & vbCrLf _
              & "group by b.DocEntry,REPLACE(b.stccode,ltrim(rtrim(convert(nvarchar(10),ceiling(sc.rate)))),''),SC.rate, b.StcCode) tx on tx.DocEntry=c.DocEntry " & vbCrLf _
              & "left join OCRD cr on cr.CardCode=c.CardCode " & vbCrLf _
              & "left join (select code,name,country from ocst where country='IN') cs on cs.code=cr.state1  " & vbCrLf _
              & "where c.docdate>=@d1 and c.DocDate<= @d2 and (cr.qrygroup1 is null or cr.QryGroup1<>'Y' and c.doctotal>250000) " & vbCrLf _
             & "union all " & vbCrLf _
             & "select 1 sno,  COUNT(b.docnum) noinv,'1900-01-01', SUM(doctotal) invvalue,'',0,sum(c.taxablevalue), 0,'' from OINV b " & vbCrLf _
             & "left join (select docentry,sum(quantity) qty,sum(linetotal) taxablevalue from inv1 group by docentry) c on c.docentry=b.docentry " & vbCrLf _
             & "left join (select tt.docentry, SUM(tt.taxsum) taxsum from INV4 tt " & vbCrLf _
             & "left join OSTC tc on tc.Code=tt.StcCode   " & vbCrLf _
             & " group by tt.DocEntry ) tx on tx.DocEntry=b.docentry " & vbCrLf _
             & "left join OCRD cr on cr.CardCode=b.cardcode " & vbCrLf _
             & "where b.DocDate>=@d1 and b.DocDate<=@d2 and (cr.qrygroup1 is null or cr.QryGroup1<>'Y' and b.doctotal>250000)) k " & vbCrLf _
             & "order by k.sno,k.[Invoice Number]"

        End If

        Try

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim cmd As New OleDbCommand
            Dim da As OleDbDataAdapter

            cmd.CommandText = msql
            cmd.Connection = con
            da.SelectCommand = cmd
            Dim dt As System.Data.DataTable
            da.Fill(dt)
            dgrid.DataSource = dt
            location = SaveFileDialog1.FileName
            xlWorkSheet = CType(xlWorkBook.Worksheets.Add(), Excel.Worksheet)
            xlWorkSheet = xlWorkBook.Sheets(sCurrent)
            For Each col As DataGridViewColumn In dgrid.Columns
                xlWorkSheet.Cells(1, col.Index + 1) = col.HeaderText
                For Each rowa As DataGridViewRow In dgrid.Rows
                    xlWorkSheet.Cells(rowa.Index + 2, col.Index + 1) = rowa.Cells(col.Index).Value
                Next
            Next
            chartRange = xlWorkSheet.Range("A1", "Z1")
            xlWorkSheet.SaveAs(location)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub DeleteCurrent(ByVal sCurrent As String)
        Try
            Dim dcmd As New OleDbCommand

            dcmd.CommandText = "Delete From " & sCurrent
            dcmd.Connection = con
            dcmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub


    Private Sub cmdexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexport.Click
        'xlapp = New Excel.Application
        If xlapp.Sheets.Count <= 7 Then
            For i = 0 To xlapp.Sheets.Count - 1
                'This will do the same as your IF statements.
                SaveInfo(lstSheets(i))
                DeleteCurrent(i)
            Next
        Else
            MsgBox("More sheets exist that are not handled.")
        End If
    End Sub


End Class
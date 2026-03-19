Imports System.Drawing
Imports System.Drawing.Printing

Public Class Frmiewiprep
    Dim msql, sql As String
    Dim i, j As Integer
    Private WithEvents PrintDocument1 As New Printing.PrintDocument
    Private currentRow As Integer = 0
    Private itemsPerPage As Integer = 0
    Private pageNumber As Integer = 1
    Dim docEntryList As String
    Dim docnumlist As String
    Private grandTotal As Integer = 0
    Dim cTotalQty As Integer = 0

    Private Sub Frmiewiprep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = My.Computer.Screen.Bounds.Height - 30
        Me.Width = My.Computer.Screen.Bounds.Width + 5
        dg.Width = Me.Width - 50
    End Sub

    Private Sub loaddata()
        msql = "select c.u_status, case when isnull(sc.scqty,0)<>0 then 'SC' else 'IH' end Ptype, c.u_brandgroup,c.docnum CutPlanno,c.docentry,c.u_docdate,c.planqty,c.fplanqty CompleteQty,c.PlanQty-c.Fplanqty CutingBalQty,isnull(r.cut,0) Cut," & vbCrLf _
              & " isnull(r.emb,0) Emb,isnull(r.st,0) ST,isnull(r.kaja,0) Kaja,isnull(sc.scqty,0) ScQty from ( " & vbCrLf _
              & " select b.u_status, b.docnum,a.docentry,b.u_docdate,tt.u_brandgroup, sum(a.u_totalqty) PlanQty,sum(a.u_prodqty) Fplanqty from [@inm_fcp2] a with (nolock) " & vbCrLf _
              & " inner join [@inm_ofcp] b with (nolock) on b.docentry=a.docentry " & vbCrLf _
              & " inner join oitm tt on tt.itemcode=a.U_ItemCode " & vbCrLf _
              & " where  a.U_ItemCode not like '%ACC%' AND U_Place='IH' " & vbCrLf
        If chkspl.Checked = True Then
            msql = msql & " and U_Remarks like'%SPL%' " & vbCrLf
        Else
            msql = msql & " and U_Remarks not like'%SPL%' " & vbCrLf
        End If
        'and U_Remarks not like'%SPL%' " & vbCrLf _

        msql = msql & " and b.U_Status='R'  group by b.docnum,a.docentry,b.u_docdate,tt.u_brandgroup,b.U_Status) c " & vbCrLf _
              & " left join (select kp.u_brandgroup,kp.cutplanno,kp.cutpdocentry,sum(kp.cutplanqty) cutplanqty ,sum(kp.cutplprdqty) cutplprdqty,sum(kp.cut) Cut,sum(kp.emb) Emb,sum(kp.st) ST,sum(kp.kaja) Kaja,sum(kp.chk) Chk,sum(kp.Iron) Iron from ( " & vbCrLf _
              & " select p.u_brandgroup,p.cutplanno,p.cutpdocentry,sum(p.cutplanqty) cutplanqty ,sum(p.cutplprdqty) cutplprdqty,sum(p.cut) Cut,sum(p.emb) Emb,sum(p.st) ST,sum(p.kaja) Kaja,sum(p.chk) Chk,sum(p.Iron) Iron from ( " & vbCrLf _
              & " select k.u_brandgroup,k.cutplanno,cutpdocentry, k.cutplanqty,k.cutplprdqty, " & vbCrLf _
              & " case when k.opercode='CUTGD' then  sum(k.rgqty) else 0 end Cut, " & vbCrLf _
              & "  case when k.opercode='EMBGD' then  sum(k.rgqty) else 0 end EMB," & vbCrLf _
              & " case when k.opercode='STGD' then  sum(k.rgqty) else 0 end ST, " & vbCrLf _
              & " case when k.opercode='KAJAGD' then  sum(k.rgqty) else 0 end Kaja, " & vbCrLf _
              & " case when k.opercode='CHKGD' then  sum(k.rgqty) else 0 end Chk, " & vbCrLf _
              & " case when k.opercode='IRONGD' then  sum(k.rgqty) else 0 end Iron from ( " & vbCrLf _
              & " select t.u_brandgroup, a.DocNum WONo,a.DocEntry WOEntry,a.U_ItemCode ItemCode,a.U_ItemName ItemName, " & vbCrLf _
              & " b.U_OperID OperCode,b.U_OperName OperName,b.U_NewSeq Sequence,c.U_CutNo CutNo,c.U_AccQty CutQty, " & vbCrLf _
              & " (isnull(c.U_AccQty,0)+isnull(c.u_rewinqty,0)-isnull(c.U_RewAccQty,0)) -(isnull(c.U_OutQty,0)+isnull(c.U_IssdQty,0)+isnull(c.U_JOQty,0)+isnull(c.U_PDEQty,0)) nIssueQty, " & vbCrLf _
              & " (isnull(c.U_AccQty,0)-(isnull(c.U_OutQty,0)+isnull(c.U_IssdQty,0)+isnull(c.U_JOQty,0)+isnull(c.U_PDEQty,0)))+ " & vbCrLf _
              & " ((isnull(c.U_RewQty,0)+isnull(c.U_RewInQty,0))-(isnull(c.U_RewPDEQty,0)+isnull(c.U_OpenPDEQty,0)+isnull(c.U_RewOutQty,0))) as issueqty, " & vbCrLf _
              & " (isnull(c.U_AccQty,0)-(isnull(c.U_OutQty,0)+isnull(c.U_IssdQty,0)+isnull(c.U_JOQty,0)+isnull(c.U_PDEQty,0))) rgqty,g.docnum cutplanno,f.docentry cutpdocentry, " & vbCrLf _
              & " f.u_totalqty cutplanqty,f.U_ProdQty cutplprdqty from [dbo].[@INM_OWOR] a,[@INM_WOR2] b,[@INM_WOR8] c,[@inm_fcp2] f,[@inm_ofcp] g,oitm t " & vbCrLf _
              & " where a.DocEntry = b.DocEntry And b.DocEntry = c.DocEntry  and a.U_Status='R'  " & vbCrLf _
              & " and b.u_operid not in ('IRONGD') and b.LineId =c.U_UniqID   and a.U_ItemCode not like 'ACC%' and g.U_Place='IH' " & vbCrLf _
              & " and  f.U_WOEntry=a.docentry and f.docentry=g.docentry and f.u_itemcode=a.u_itemcode and t.itemcode=a.U_ItemCode and " & vbCrLf _
              & " ((isnull(c.U_AccQty,0)-(isnull(c.U_OutQty,0)+isnull(c.U_IssdQty,0)+isnull(c.U_JOQty,0)+isnull(c.U_PDEQty,0)))+ " & vbCrLf _
              & " ((isnull(c.U_RewQty,0)+isnull(c.U_RewInQty,0))-(isnull(c.U_RewPDEQty,0)+isnull(c.U_OpenPDEQty,0)+isnull(c.U_RewOutQty,0))))>0 ) k " & vbCrLf _
              & " group by k.opercode, k.u_brandgroup,k.cutplanno,cutpdocentry, k.cutplanqty,k.cutplprdqty) p " & vbCrLf _
              & " group by p.u_brandgroup,p.cutplanno,p.cutpdocentry " & vbCrLf _
              & " union all " & vbCrLf _
              & " select ll.u_brandgroup,ll.cutplanno,ll.cutpdocentry,sum(ll.cutplanqty) cutplanqty,sum(ll.cutplprdqty) cutplprdqty, " & vbCrLf _
              & " sum(ll.cut) Cut,sum(ll.emb) Emb,sum(ll.st) ST,sum(ll.kaja) Kaja,sum(ll.Chk) Chk,sum(ll.Iron) Iron from ( " & vbCrLf _
              & " select kk.u_brandgroup,kk.cutplanno,kk.cutpdocentry,kk.cutplanqty,kk.cutplprdqty, " & vbCrLf _
              & " case when kk.opercode='CUTGD' then  sum(kk.balqty) else 0 end Cut, " & vbCrLf _
              & " case when kk.opercode='EMBGD' then  sum(kk.balqty) else 0 end EMB, " & vbCrLf _
              & " case when kk.opercode='STGD' then  sum(kk.balqty) else 0 end ST, " & vbCrLf _
              & " case when kk.opercode='KAJAGD' then  sum(kk.balqty) else 0 end Kaja, " & vbCrLf _
              & " case when kk.opercode='CHKGD' then  sum(kk.balqty) else 0 end Chk, " & vbCrLf _
              & " case when kk.opercode='IRONGD' then  sum(kk.balqty) else 0 end Iron  from ( " & vbCrLf _
              & " select  b.u_type,  b.U_DTOperCode opercode, g.docnum cutplanno,f.docentry cutpdocentry, b.u_tlno, t.U_BrandGroup,  a.U_ItemCode,c.ItemName,c.U_Style, c.U_Size, " & vbCrLf _
              & " a.U_WONo,a.U_WOEntry,b.U_DocDate Date,b .docnum [PROD DEL NO],DATEDIFF(d,b.u_docdate ,GETDATE()) [No Of Days], " & vbCrLf _
              & " a.U_CutNo,sum(a.U_RecdQty) rcdqty,sum(a.u_cmplqty) relqty,sum(isnull(a.u_wipqty,0)) as openqty,  sum(a.u_cmplqty+ISNULL(a.u_wipqty,0)) cmplqty, " & vbCrLf _
              & " sum(f.u_totalqty) cutplanqty,sum(f.U_ProdQty) cutplprdqty, " & vbCrLf _
              & " sum(a.U_RecdQty-(a.U_CmplQty+ISNULL(U_WIPQty,0))) as balqty from [@inm_pde1] a,[@INM_OPDE] b,OITM c,[@inm_fcp2] f,[@inm_ofcp] g,oitm t " & vbCrLf _
              & " where b.DocEntry=a.DocEntry and a.U_ItemCode=c.itemcode and b.u_docstatus='R' and g.U_Place='IH' and b.canceled not in ('Y') and f.U_WOEntry=a.u_woentry and f.docentry=g.DocEntry " & vbCrLf _
              & " And f.U_ItemCode = a.U_ItemCode And t.itemcode = a.u_itemcode " & vbCrLf _
              & " group by b.u_type,  b.U_dtOperCode, a.U_ItemCode,c.ItemName,c.U_Style,  c.U_Size, a.U_WONo,a.U_WOEntry,a.U_CutNo,b.U_DocDate,b.docnum,b.u_tlno,g.docnum,f.docentry, " & vbCrLf _
              & "  t.u_brandgroup having  sum(a.U_RecdQty-(a.U_CmplQty+ISNULL(U_WIPQty,0))) <>0 ) kk " & vbCrLf _
              & " group by kk.u_brandgroup,kk.opercode, kk.cutplanno,kk.cutpdocentry,kk.cutplanqty,kk.cutplprdqty) ll " & vbCrLf _
              & " group by ll.u_brandgroup,ll.cutplanno,ll.cutpdocentry) kp " & vbCrLf _
              & " group by kp.u_brandgroup,kp.cutplanno,kp.cutpdocentry) r on r.cutpdocentry=c.DocEntry and r.U_BrandGroup=c.U_BrandGroup " & vbCrLf _
              & " left join (select t.u_brandgroup, b.U_FCPEntry,sum(b.u_orderqty-b.u_accpqty) scqty from [@insc_jor1] b with (nolock) " & vbCrLf _
              & " inner join [@insc_ojor] c on c.docentry=b.docentry  " & vbCrLf _
              & " inner join oitm t on t.itemcode=b.u_itemcode " & vbCrLf _
              & "  where c.u_status='O' group by b.u_fcpentry,t.u_brandgroup) sc on sc.u_brandgroup=c.u_brandgroup and sc.u_fcpentry=c.DocEntry " & vbCrLf _
              & " where (isnull(r.cut,0)+isnull(r.emb,0)+isnull(r.st,0)+isnull(r.kaja,0)+isnull(r.chk,0)+isnull(r.iron,0)+isnull(sc.scqty,0))>0 " & vbCrLf _
              & " or (c.u_status<>'C' and (isnull(r.cut,0)+isnull(r.emb,0)+isnull(r.st,0)+isnull(r.kaja,0)+isnull(r.chk,0)+isnull(r.iron,0)+isnull(sc.scqty,0)<0)) " & vbCrLf
        Cursor = Cursors.WaitCursor
        dg.DataSource = Nothing
        Dim dt As DataTable = getDataTable(msql)
        If dt.Rows.Count > 0 Then
            dg.DataSource = dt
            dg.ColumnHeadersDefaultCellStyle.Font = New Font(DataGridView.DefaultFont, FontStyle.Bold)
            dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            For i = 0 To dg.Columns.Count - 1
                '        dgsr.Columns(i).ReadOnly = True
                If i > 5 Then
                    dg.Columns(i).ValueType = GetType(Int32)
                    dg.Columns(i).DefaultCellStyle.Format = ("0")
                    dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                End If
                '        If i > 1 Then
                '            dgsr.Columns(i).ValueType = GetType(Decimal)
                '            dgsr.Columns(i).DefaultCellStyle.Format = ("0.00")
                ' 
                '   End If
                dg.Columns(i).ReadOnly = True
            Next
        Else
            dg.DataSource = Nothing
        End If

        Cursor = Cursors.Default
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If chkacc.Checked = True Then
            Call loadforaccessory()
        Else

            Call loaddata()
        End If

    End Sub

    Private Sub butxl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butxl.Click
        gridexcelexport(dg, 1)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub loaddata2()
        msql = "select b.[lineno] linno, b.empno,b.empname, b.opername,b.jobgrade,sum(b.totmin) totmin, sum(b.totprod) prodqty,count(b.opername) cnt,sum(b.totprod)/count(b.opername) avgqty,c.pcs*8 daypcs from perf1 b " & vbCrLf _
               & " left join processjobmaster c on c.opername=b.opername " & vbCrLf _
               & " where b.date>='2022-07-01' and b.date<='2022-09-30'  " & vbCrLf _
               & " and b.empno in (12100388,12104097,12103662,12100645,12103930,12200118,12104040,12101002,12101966,12102244,12202564, " & vbCrLf _
               & " 12101816,12101471,12100686,12102948,12100378,12100687,12101195,12103396,12103442,12102314, " & vbCrLf _
               & " 12102228,12102904,12100329,12103360,12102627,12100981,12200939,12200614,12102472,12102951, " & vbCrLf _
               & " 12103408,12101001,12100980,12101471,12100704,12100402,12103949,12103806,12101964,12200009)" & vbCrLf _
               & " b.incyn='Y'  group by b.[lineno],b.opername,b.empno,b.empname,c.pcs,b.jobgrade order by  b.[lineno],b.empname "
    End Sub

    Private Sub loadforaccessory()
        msql = "select 'False' sel, f.DocNum CutPlanNo,format (f.U_DocDate,'dd-MM-yyyy') as CutPlanDate,b.U_CutNo,it.U_BrandGroup,it.U_Style,it.U_Size,b.U_AccpQty  from [@INM_OWIP] a left join [@INM_WIP1] b on a.DocEntry=b.DocEntry " _
               & " left join oitm it on b.U_ItemCode=it.ItemCode " _
               & " left join [@INM_OFCP] f on b.U_FCPEntry=f.DocEntry " _
               & " where a.U_OperCode='CUTGD' and U_DocStatus='C' and it.ItmsGrpCod not in (105) " _
               & " and a.U_DocDate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and a.U_DocDate<=' " & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' " _
               & " order by f.docnum,f.u_docdate,it.u_brandgroup,it.u_style,it.u_size "
        Cursor = Cursors.WaitCursor
        dg.DataSource = Nothing
        Dim dt As DataTable = getDataTable(msql)
        If dt.Rows.Count > 0 Then
            dg.DataSource = dt
            If TypeOf dg.Columns("sel") Is DataGridViewCheckBoxColumn = False Then
                Dim chk As New DataGridViewCheckBoxColumn()
                chk.Name = "sel"
                chk.HeaderText = ""
                chk.DataPropertyName = "sel"
                chk.Width = 40
                chk.TrueValue = True
                chk.FalseValue = False

                dg.Columns.Remove("sel")
                dg.Columns.Insert(0, chk)

                dg.Columns("sel").ReadOnly = False
                dg.Columns("sel").Frozen = True
                dg.AllowUserToAddRows = False
                dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End If

        End If
        Cursor = Cursors.Default


    End Sub

    Private Sub chkacc_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkacc.CheckedChanged
        If chkacc.Checked = True Then
            chkspl.Checked = False
        End If
    End Sub

    Private Sub chkspl_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkspl.CheckedChanged
        If chkspl.Checked = True Then
            chkacc.Checked = False
        End If
    End Sub

    Private Sub Btnprint_Click(sender As System.Object, e As System.EventArgs) Handles Btnprint.Click
        If dg.Rows.Count - 1 > 0 Then
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
            'Dim commands As New List(Of OleDbCommand)
            'msql = "update [@inm_owip] set u_process='Y' where docentry in (" & docEntryList & ")"
            'Dim cmd As New OleDbCommand(msql)
            'commands.Add(cmd)
            'Dim result As Boolean = ExecuteTransactionWithCommands(commands)

            'If result Then
            '    'MsgBox("All records saved successfully!")
            'Else
            '    MsgBox("Transaction failed. No data saved.")
            'End If
        Else
            MsgBox("Pls Select Completion Number then Submit!")
        End If

    End Sub

    Private Sub DrawRightAligned(g As Graphics, text As String, f As Font, b As Brush, rightX As Integer, y As Integer)

        Dim w As Integer = CInt(g.MeasureString(text, f).Width)
        g.DrawString(text, f, b, rightX - w, y)
    End Sub

    Private Sub DrawColumnLines(g As Graphics, pen As Pen, topY As Integer, bottomY As Integer,
                            left As Integer, col_cutplanno As Integer, col_cutplandate As Integer,
                            col_cutno As Integer, col_brand As Integer, col_style As Integer, col_size As Integer,
                            col_Qty As Integer, rightLimit As Integer)

        'col_cutplanno, col_cutplandate, col_cutno, col_brand, col_style, col_size, col_Qty, rightLimit

        g.DrawLine(pen, left, topY, left, bottomY)
        g.DrawLine(pen, col_cutplanno, topY, col_cutplanno, bottomY)
        g.DrawLine(pen, col_cutplandate, topY, col_cutplandate, bottomY)
        g.DrawLine(pen, col_cutno, topY, col_cutno, bottomY)
        g.DrawLine(pen, col_brand, topY, col_brand, bottomY)
        g.DrawLine(pen, col_style, topY, col_style, bottomY)
        g.DrawLine(pen, col_size, topY, col_size, bottomY)
        g.DrawLine(pen, col_Qty, topY, col_Qty, bottomY)
        g.DrawLine(pen, rightLimit, topY, rightLimit, bottomY)

        'g.DrawLine(pen, col_Qty, topY, col_Qty, bottomY)
        'g.DrawLine(pen, col_SONo, topY, col_SONo, bottomY)
        'g.DrawLine(pen, col_SOEntry, topY, col_SOEntry, bottomY)
        'g.DrawLine(pen, col_SODate, topY, col_SODate, bottomY)
        'g.DrawLine(pen, col_BNo, topY, col_BNo, bottomY)
        'g.DrawLine(pen, rightLimit, topY, rightLimit, bottomY)

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
        Dim wmFont As New Font("Arial", 60, FontStyle.Bold)
        Dim wmBrush As New SolidBrush(Color.FromArgb(25, Color.Black))
        g.TranslateTransform(300, 300)
        g.RotateTransform(-30)
        g.DrawString("ATITHYA", wmFont, wmBrush, 0, 0)
        g.ResetTransform()

        '===== Header =====
        'g.DrawString("ATITHYA CLOTHING COMPANY", fTitle, Brushes.Black, left, y)
        'y += 35
        'g.DrawString("Itemwise Consolidated Despatch Print", fSub, Brushes.Black, left, y)
        'y += 25

        Dim title1 As String = "ATITHYA CLOTHING COMPANY"
        Dim title1Width As Single = g.MeasureString(title1, fTitle).Width
        Dim centerX As Single = e.MarginBounds.Left + ((e.MarginBounds.Width - title1Width) / 2)
        g.DrawString(title1, fTitle, Brushes.Black, centerX, y)

        y += 35

        '============ Center Line 2 ============
        Dim title2 As String = "Indent for Accessories"
        Dim title2Width As Single = g.MeasureString(title2, fSub).Width
        centerX = e.MarginBounds.Left + ((e.MarginBounds.Width - title2Width) / 2)
        g.DrawString(title2, fSub, Brushes.Black, centerX, y)
        y += 30

        Dim title3 As String = txtremark.Text.ToString.Trim
        Dim title3Width As Single = g.MeasureString(title3, fSub).Width
        centerX = e.MarginBounds.Left + ((e.MarginBounds.Width - title3Width) / 2)
        g.DrawString(title3, fSub, Brushes.Black, centerX, y)
        y += 25


        'g.DrawLine(penBlack, left, y, rightLimit, y)
        'y += 10
        ''Dim longText As String = "Completion Nos: " & docnumlist
        'g.DrawString("Completion Nos: ", fBold, Brushes.Black, left, y)
        'Dim longText As String = docnumlist
        'Dim wrapRect As New RectangleF(left + 130, y, rightLimit - left, 2000)
        'g.DrawString(longText, f10, Brushes.Black, wrapRect)
        'y += CInt(g.MeasureString(longText, f10, rightLimit - left).Height) + 10

        g.DrawString("Date: " & Format(CDate(mskdatefr.Text), "dd-MM-yyyy") &
                     " To " & Format(CDate(Mskdateto.Text), "dd-MM-yyyy"),
                     fBold, Brushes.Black, left, y)
        y += 25

        '===== Column Widths =====
        Dim w_planno As Integer = 70
        Dim w_plandate As Integer = 100
        Dim w_cutno As Integer = 80
        Dim w_brand As Integer = 250
        Dim w_style As Integer = 70
        Dim w_size As Integer = 50
        Dim w_Qty As Integer = 80

        'Dim w_SONo As Integer = 80
        'Dim w_SOEntry As Integer = 80
        'Dim w_SODate As Integer = 80

        '===== Column X Positions =====
        Dim col_cutplanno As Integer = left
        Dim col_cutplandate As Integer = col_cutplanno + w_planno
        Dim col_cutno As Integer = col_cutplandate + w_plandate
        Dim col_brand As Integer = col_cutno + w_cutno
        Dim col_style As Integer = col_brand + w_brand
        Dim col_size As Integer = col_style + w_style
        Dim col_qty As Integer = col_size + w_size



       
        'Dim col_BNo As Integer = rightLimit - 80

        '===== Column Header =====
        Dim headerTop As Integer = y
        g.DrawLine(penBlack, left, y, rightLimit, y)
        y += 10

        DrawCentered(g, "CutPlanNo", fBold, Brushes.Black, col_cutplanno, col_cutplandate, y)
        DrawCentered(g, "PlanDate", fBold, Brushes.Black, col_cutplandate, col_cutno, y)
        DrawCentered(g, "CutNo", fBold, Brushes.Black, col_cutno, col_brand, y)
        DrawCentered(g, "BrandName", fBold, Brushes.Black, col_brand, col_style, y)
        DrawCentered(g, "Style", fBold, Brushes.Black, col_style, col_size, y)
        DrawCentered(g, "Size", fBold, Brushes.Black, col_size, col_qty, y)
        DrawCentered(g, "Qty", fBold, Brushes.Black, col_qty, rightLimit, y)

        y += 20
        g.DrawLine(penBlack, left, y, rightLimit, y)

        Dim headerBottom As Integer = y
        y += 5

        DrawColumnLines(g, penBlack, headerTop, headerBottom, left, col_cutplanno, col_cutplandate, col_cutno, col_brand, col_style, col_size, col_qty, rightLimit)
        'col_qty, col_SONo, col_SOEntry, col_SODate, col_BNo, rightLimit)

        '===== Print Rows =====
        Dim rowHeight As Integer = 22
        Dim maxRows As Integer = 30
        Dim count As Integer = 0
        Dim i As Integer = currentRow

        While i < dg.Rows.Count
            If Convert.ToBoolean(dg.Rows(i).Cells("sel").Value) = True Then
                If count >= maxRows Then
                    g.DrawLine(penBlack, left, y, rightLimit, y)
                    g.DrawString("Page: " & pageNumber, f10, Brushes.Black, rightLimit - 80, e.MarginBounds.Bottom)
                    pageNumber += 1
                    currentRow = i
                    e.HasMorePages = True
                    Return
                End If

                Dim rowTop As Integer = y

                Dim row = dg.Rows(i)
                Dim cutplanno As String = dg.Rows(i).Cells("CutPlanNo").Value
                Dim cutplandate As String = dg.Rows(i).Cells("CutPlanDate").Value
                Dim cutno As String = dg.Rows(i).Cells("U_CutNo").Value
                Dim Brandname As String = dg.Rows(i).Cells("U_BrandGroup").Value
                Dim style As String = dg.Rows(i).Cells("U_Style").Value
                Dim size As String = dg.Rows(i).Cells("U_size").Value
                Dim qty As String = Convert.ToInt32(row.Cells("u_AccpQty").Value).ToString()
                'U_CutNo,U_BrandGroup	U_Style	U_Size	U_AccpQty

                'Dim itemName As String = row.Cells("ItemCode").Value.ToString() & "--" &
                '                         row.Cells("ItemName").Value.ToString()
                'Dim qty As String = Convert.ToInt32(row.Cells("Qty").Value).ToString()
                'Dim sono As String = Convert.ToInt32(row.Cells("SONo").Value).ToString()
                'Dim soentry As String = Convert.ToInt32(row.Cells("SOEntry").Value).ToString()

                'Dim sodate As String = ""
                'If Not IsDBNull(row.Cells("SODate").Value) Then
                '    sodate = CDate(row.Cells("SODate").Value).ToString("dd-MM-yyyy")
                'End If

                'Dim bno As String = row.Cells("BNo").Value.ToString()

                grandTotal += CInt(qty)
                'col_cutplanno, col_cutplandate, col_cutno, col_brand, col_style, col_size, col_qty, rightLimit
                '==== DRAW DATA IN PERFECT COLUMNS ====
                DrawLeftInCell(g, cutplanno, f10, Brushes.Black, col_cutplanno, col_cutplandate, y)
                DrawLeftInCell(g, cutplandate, f10, Brushes.Black, col_cutplandate, col_cutno, y)
                DrawLeftInCell(g, cutno, f10, Brushes.Black, col_cutno, col_brand, y)
                DrawLeftInCell(g, Brandname, f10, Brushes.Black, col_brand, col_style, y)
                DrawLeftInCell(g, style, f10, Brushes.Black, col_style, col_size, y)
                DrawLeftInCell(g, size, f10, Brushes.Black, col_size, col_qty, y)
                DrawRightInCell(g, qty, f10, Brushes.Black, col_qty, rightLimit, y)


                y += rowHeight
                Dim rowBottom As Integer = y

                g.DrawLine(penLight, left, y, rightLimit, y)
                y += 3

                'DrawColumnLines(g, penThin, rowTop, rowBottom, left,
                '                col_Qty, col_SONo, col_SOEntry, col_SODate, col_BNo, rightLimit)

                DrawColumnLines(g, penThin, rowTop, rowBottom, left, col_cutplanno, col_cutplandate, col_cutno, col_brand, col_style, col_size, col_qty, rightLimit)

                count += 1
                'i += 1
            End If
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

    Private Sub dg_CurrentCellDirtyStateChanged(sender As Object, e As System.EventArgs) Handles dg.CurrentCellDirtyStateChanged
        If TypeOf dg.CurrentCell Is DataGridViewCheckBoxCell Then
            dg.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

End Class
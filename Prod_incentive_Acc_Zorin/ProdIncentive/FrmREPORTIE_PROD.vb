Public Class FrmREPORTIE_PROD
    Dim msql, msql2 As String
    Private Sub FrmREPORTIE_PROD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width + 5
        Me.Height = My.Computer.Screen.Bounds.Height - 20
    End Sub

    Private Sub loaddata()
        If optcomp.Checked = True Then
            'KAJA Completion
            msql = "select k.u_docdate,k.U_OperCode,k.lno, k.U_ItemCode,k.U_ItemName,k.U_BrandGroup,k.U_Style,k.U_Size,k.accpqty,k.U_AccptWhs from " & vbCrLf _
                   & "(select 'IN HOUSE' stype,b.docnum,b.u_docdate, a.U_OperCode,b.U_LineNo lno,a.U_AccptWhs,a.U_ItemCode,a.U_ItemName,it.U_BrandGroup,it.U_Style,it.U_Size,a.U_Sequence,a.U_WONo,a.U_WOEntry,a.U_CutNo,sum(a.U_ProdQty) prodqty,SUM(a.U_AccpQty)  accpqty,SUM(a.U_RewQty) rewqty,SUM(a.U_RejQty) rejqty  " & vbCrLf _
                   & " from [@INM_WIP1] a with (nolock),[@INM_OWIP] b with (nolock) ,OITM it with (nolock) " & vbCrLf _
                   & " where a.DocEntry=b.DocEntry and a.U_ItemCode=it.ItemCode and b.U_DocDate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.u_docdate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' and b.U_DocStatus='C'  " & vbCrLf _
                   & "group by b.docnum,b.u_docdate,a.U_OperCode,b.U_LineNo,it.U_Style,it.U_Size,it.U_BrandGroup,a.U_ItemCode,a.U_ItemName,a.U_Sequence,a.U_WONo,a.U_WOEntry,a.U_CutNo,a.U_AccptWhs) k " & vbCrLf _
                   & " where (k.u_opercode='KAJAGD' ) " & vbCrLf _
                   & " order by k.u_opercode,k.lno,k.u_docdate,k.docnum,k.u_brandgroup,k.U_Style,k.U_Size "
        ElseIf optproc.Checked = True Then

            'KAJA Process
            msql = "select k.U_DTOperCode,k.U_type,k.U_TLNo,k.U_BrandGroup,k.U_Style,k.U_Size,k.balqty from " & vbCrLf _
                   & "(select  b.u_type,  b.U_DTOperCode,b.u_tlno,  a.U_ItemCode,c.ItemName,c.U_BrandGroup,c.U_Style, c.U_Size, " & vbCrLf _
                   & "a.U_WONo,a.U_WOEntry,b.U_DocDate Date,b .docnum [PROD DEL NO],DATEDIFF(d,b.u_docdate ,GETDATE()) [No Of Days], " & vbCrLf _
                   & "a.U_CutNo,sum(a.U_RecdQty) rcdqty,sum(a.u_cmplqty) relqty,sum(isnull(a.u_wipqty,0)) as openqty,  sum(a.u_cmplqty+ISNULL(a.u_wipqty,0)) cmplqty,sum(a.U_RecdQty-(a.U_CmplQty+ISNULL(U_WIPQty,0))) as balqty  " & vbCrLf _
                   & " from [@inm_pde1] a with (nolock),[@INM_OPDE] b with (nolock),OITM c with (nolock) " & vbCrLf _
                   & " where b.DocEntry=a.DocEntry and a.U_ItemCode=c.itemcode and b.u_docstatus='R' and b.canceled not in ('Y') and b.U_Type='RG' " & vbCrLf _
                   & " and   (b.u_dtopercode in ('KAJAGD','EMBGD','FUSGD','CUTGD') )  " & vbCrLf _
                   & " group by b.u_type,  b.U_dtOperCode, a.U_ItemCode,c.ItemName,c.U_BrandGroup,c.U_Style,  c.U_Size, a.U_WONo,a.U_WOEntry,a.U_CutNo,b.U_DocDate,b.docnum,b.u_tlno " & vbCrLf _
                   & " having  sum(a.U_RecdQty-(a.U_CmplQty+ISNULL(U_WIPQty,0))) <>0)K " & vbCrLf _
                   & " order by k.u_dtopercode, k.U_TLNo,k.U_Type, k.U_BrandGroup "
        ElseIf optready.Checked = True Then

            msql = "select k.opercode, k.itemcode,k.itemname,k.opercode,k.opername,sum(k.stock) stock,sum(rgqty) rgqty from " & vbCrLf _
                   & " (select a.DocNum WONo,a.DocEntry WOEntry,a.U_ItemCode ItemCode,a.U_ItemName ItemName,b.U_OperID OperCode,b.U_OperName OperName,b.U_NewSeq Sequence," & vbCrLf _
                   & "c.U_CutNo CutNo,c.U_AccQty CutQty,(isnull(c.U_AccQty,0)+isnull(c.u_rewinqty,0)-isnull(c.U_RewAccQty,0))-(isnull(c.U_OutQty,0)+isnull(c.U_IssdQty,0)+isnull(c.U_JOQty,0)+isnull(c.U_PDEQty,0)) Stock, " & vbCrLf _
                   & "(isnull(c.U_AccQty,0)+isnull(c.u_rewinqty,0)-isnull(c.U_RewAccQty,0)) -(isnull(c.U_OutQty,0)+isnull(c.U_IssdQty,0)+isnull(c.U_JOQty,0)+isnull(c.U_PDEQty,0)) nIssueQty, " & vbCrLf _
                   & "(isnull(c.U_AccQty,0)-(isnull(c.U_OutQty,0)+isnull(c.U_IssdQty,0)+isnull(c.U_JOQty,0)+isnull(c.U_PDEQty,0)))+ " & vbCrLf _
                   & "((isnull(c.U_RewQty,0)+isnull(c.U_RewInQty,0))-(isnull(c.U_RewPDEQty,0)+isnull(c.U_OpenPDEQty,0)+isnull(c.U_RewOutQty,0))) as issueqty, " & vbCrLf _
                   & "(isnull(c.U_AccQty,0)-(isnull(c.U_OutQty,0)+isnull(c.U_IssdQty,0)+isnull(c.U_JOQty,0)+isnull(c.U_PDEQty,0))) rgqty " & vbCrLf _
                   & " from [@INM_OWOR] a with (nolock),[@INM_WOR2] b with (nolock),[@INM_WOR8] c with (nolock), OITM it with (nolock) " & vbCrLf _
                   & " where a.DocEntry = b.DocEntry And b.DocEntry = c.DocEntry and a.U_ItemCode = it.ItemCode  and a.U_Status='R' and b.u_operid not in ('IRONGD') " & vbCrLf _
                   & " and b.LineId =c.U_UniqID and (b.u_operid in ('FUSGD','EMBGD','CUTGD'))  and a.U_ItemCode not like 'ACC%' " & vbCrLf _
                   & "and ((isnull(c.U_AccQty,0)-(isnull(c.U_OutQty,0)+isnull(c.U_IssdQty,0)+isnull(c.U_JOQty,0)+isnull(c.U_PDEQty,0)))+ " & vbCrLf _
                   & "((isnull(c.U_RewQty,0)+isnull(c.U_RewInQty,0))-(isnull(c.U_RewPDEQty,0)+isnull(c.U_OpenPDEQty,0)+isnull(c.U_RewOutQty,0))))>0 ) k " & vbCrLf _
                   & " group by k.opercode, k.itemcode,k.itemname,k.opercode,k.opername " & vbCrLf _
                   & "order by k.opercode, k.itemcode "
        End If

        Cursor = Cursors.WaitCursor
        dg.DataSource = Nothing

        Dim dt As DataTable = getDataTable(msql)
        dg.DataSource = dt

        Cursor = Cursors.Default
    End Sub

    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.Click
        If Chkst.Checked = True Then

            Lblstqty.Text = getstqty()
        Else

            Call loaddata()
        End If

    End Sub
    Private Function getstqty() As Int32
        Cursor = Cursors.WaitCursor
        Dim mqty As Integer
        msql = "select sum(c.U_AccpQty) qty from [@inm_owip] b with (nolock)" _
            & " inner join [@inm_wip1] c with (nolock) on b.docentry=c.docentry " _
            & " where b.u_docdate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.u_docdate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' and b.U_TranType='RG' and b.U_DocStatus='C' and b.U_OperCode='KAJAGD' and c.U_AccptWhs='KAJA' "
        Dim dt As DataTable = getDataTable(msql)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                mqty = rw("qty")
            Next
        Else
            mqty = 0
        End If
        Cursor = Cursors.Default
        Return mqty
    End Function


    Private Sub CmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexcel.Click
        'gridexcelexport(dg, 1)
        If optcomp.Checked = True Then
            gridexcelexport4(dg, 0, "ProdCompletion", "Production Completion Statement")
        ElseIf optproc.Checked = True Then
            gridexcelexport4(dg, 0, "ProdProcess", "Production Process Pending Statement")
        ElseIf optready.Checked = True Then
            gridexcelexport4(dg, 0, "Prodready", "Production Ready Stock Statement")
        End If

    End Sub

    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick

    End Sub
End Class
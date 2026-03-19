Imports System.io
Public Class FrmIEReport
    Dim msql, sql2, sql3 As String
    Dim i, j As Int16
    Dim mhtot, motot, mshtot As Int32
    Dim msal, msal1 As Double
    Private Sub FrmIEReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width + 5
        Me.Height = My.Computer.Screen.Bounds.Height - 20
        ' dg.Width = My.Computer.Screen.Bounds.Width - 20
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub loaddata()

        msql = "declare @nod as float " & vbCrLf _
              & " select @nod=count(distinct dot)from rrcolor.dbo.empdailysalary where dot>='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and dot<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "'" & vbCrLf _
              & " set @nod=1"

        msql = msql & "select isnull(k.itmsgrpnam,'') Itmsgrpnam, m.nlinno Linno, isnull(ic.vname,'') as InchargeName, isnull(g.brand,'') Brand,isnull(m.nomachine,0) Nomachine,(CONVERT(int,isnull(m.nomachine,'0'))-ISNULL(p.operator,0)) Non_Used_Mac,isnull(p.headcnt,0) HeadCnt,isnull(p.operator,0) Operator, " & vbCrLf _
              & "isnull(round(SUM(k.fullqty),2),0) FullQty,isnull(round(SUM(k.halfqty),2),0) HalfQty,isnull(round(SUM(k.othqty),2),0) OthQty,isnull(round(SUM(k.prodqty),2),0) Prodqty , " & vbCrLf _
              & " case when sum(k.prodqty)>0 and isnull(p.operator,0)>0 then round(sum(k.prodqty)/isnull(p.operator,0),2) else 0 end AvgperHead ,isnull(SUM(k.issdqty),0) Issued_Qty from ( " & vbCrLf _
              & " select kk.itmsgrpnam,kk.u_brandgroup,kk.linno,sum(kk.Fprodqty) Fullqty,SUM(kk.hprodqty) halfqty,SUM(kk.oprodqty) othqty, SUM(kk.prodqty) prodqty,SUM(kk.issdqty) issdqty from  " & vbCrLf _
              & " (select tb.ItmsGrpNam,t.u_brandgroup, CASE when ISNULL(c.u_ocrname5,'')='' then  case when ISNULL(c.u_lineno,'') like '%SAMPLE%' then '200' else c.U_LineNo end else c.U_OcrName5 end linno ,b.u_cutno, " & vbCrLf _
              & " CASE when t.U_Style='FULL' then  SUM(b.U_AccpQty) else 0 end Fprodqty,CASE when t.U_Style='HALF' then  SUM(b.U_AccpQty) else 0 end Hprodqty, " & vbCrLf _
              & " CASE when t.U_Style Not in ('FULL','HALF') then  SUM(b.U_AccpQty) else 0 end Oprodqty, SUM(b.U_AccpQty) prodqty,0 fissdqty,0 hissdqty,0 oissdqty,0 issdqty from [@INM_WIP1] b   " & vbCrLf _
              & " inner join [@INM_OWIP] c on c.DocEntry=b.DocEntry  " & vbCrLf _
              & " inner join oitm t on t.ItemCode=b.U_ItemCode  " & vbCrLf _
              & " inner join oitb tb on tb.ItmsGrpCod=t.ItmsGrpCod  " & vbCrLf _
              & " where c.U_DocDate>='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and c.u_docdate<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and b.U_AccptWhs in ('STITCH','KAJA') and c.u_lineno not in ('General','SC')  group by tb.ItmsGrpNam,c.U_LineNo,c.u_ocrname5,t.u_brandgroup,t.u_style,b.u_cutno  " & vbCrLf _
              & " union all " & vbCrLf _
              & " select tb.ItmsGrpNam,t.u_brandgroup,CASE when c.U_Type='RG' then  case when c.u_flno like '%SAMPLE%' then '200' else c.U_FLNo end else '500' end  linno,b.u_cutno, " & vbCrLf _
              & " 0 fprodqty,0 hprodqty,0 oprodqty, 0 prodqty, CASE when t.U_Style='FULL' then  SUM(b.U_IssdQty) else 0 end Fissqty,CASE when t.U_Style='HALF' then  SUM(b.U_IssdQty) else 0 end Hissdqty, " & vbCrLf _
              & " CASE when t.U_Style Not in ('FULL','HALF') then  SUM(b.U_IssdQty) else 0 end Oissdqty,  SUM(b.U_IssdQty) issdqty from [@INM_PDE1] b   " & vbCrLf _
              & " inner join [@INM_OPDE] c on c.DocEntry=b.DocEntry  " & vbCrLf _
              & " inner join oitm t on t.ItemCode=b.U_ItemCode  " & vbCrLf _
              & " inner join OITB tb on tb.ItmsGrpCod=t.ItmsGrpCod  " & vbCrLf _
              & " where c.U_DocDate>='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and c.u_docdate<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and  c.U_DfOperCode in ('STGD','KAJAGD') and c.u_flno not in ('General','SC')  group by tb.ItmsGrpNam,t.u_brandgroup,c.U_FLNo ,c.u_type,t.u_style,b.u_cutno) kk  " & vbCrLf _
              & " group by kk.itmsgrpnam,kk.u_brandgroup,kk.linno ) k  "



        'msql = "select isnull(k.itmsgrpnam,'') itmsgrpnam, m.nlinno linno, isnull(ic.vname,'') as inchargeName, isnull(g.brand,'') brand,isnull(m.nomachine,0) Nomachine,(CONVERT(int,isnull(m.nomachine,'0'))-ISNULL(p.operator,0)) Non_Used_Mac,isnull(p.headcnt,0) headcnt,isnull(p.operator,0) operator, isnull(round(SUM(k.prodqty),2),0) prodqty , case when sum(k.prodqty)>0 and isnull(p.operator,0)>0 then round(sum(k.prodqty)/isnull(p.operator,0),2) else 0 end avgperhead ,isnull(SUM(k.issdqty),0) Issued_Qty from (" & vbCrLf _
        '       & " select kk.itmsgrpnam,kk.u_brandgroup,kk.linno,SUM(kk.prodqty) prodqty,SUM(kk.issdqty) issdqty from  " & vbCrLf _
        '       & " (select tb.ItmsGrpNam,t.u_brandgroup, CASE when ISNULL(c.u_ocrname5,'')='' then  case when ISNULL(c.u_lineno,'') like '%SAMPLE%' then '200' else c.U_LineNo end else c.U_OcrName5 end linno , b.U_CutNo, SUM(b.U_AccpQty) prodqty,0 issdqty from [@INM_WIP1] b   " & vbCrLf _
        '       & " left join [@INM_OWIP] c on c.DocEntry=b.DocEntry  " & vbCrLf _
        '       & " left join oitm t on t.ItemCode=b.U_ItemCode  " & vbCrLf _
        '       & " left join oitb tb on tb.ItmsGrpCod=t.ItmsGrpCod  " & vbCrLf _
        '       & " where c.U_DocDate='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and b.U_AccptWhs in ('STITCH','KAJA')  group by tb.ItmsGrpNam,c.U_LineNo,c.u_ocrname5,t.u_brandgroup,b.u_cutno  " & vbCrLf _
        '       & " union all  " & vbCrLf _
        '       & " select tb.ItmsGrpNam,t.u_brandgroup,CASE when c.U_Type='RG' then  case when c.u_flno like '%SAMPLE%' then '200' else c.U_FLNo end else '500' end  linno,b.U_CutNo,0 prodqty, SUM(b.U_IssdQty) issdqty from [@INM_PDE1] b   " & vbCrLf _
        '       & " left join [@INM_OPDE] c on c.DocEntry=b.DocEntry  " & vbCrLf _
        '       & " left join oitm t on t.ItemCode=b.U_ItemCode  " & vbCrLf _
        '       & " left join OITB tb on tb.ItmsGrpCod=t.ItmsGrpCod  " & vbCrLf _
        '       & " where c.U_DocDate='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and  c.U_DfOperCode in ('STGD','KAJAGD') and c.u_flno not in ('General','SC')  group by tb.ItmsGrpNam,t.u_brandgroup,c.U_FLNo ,b.u_cutno,c.u_type) kk  " & vbCrLf _
        '       & " group by kk.itmsgrpnam,kk.u_brandgroup,kk.linno) k  " & vbCrLf

        msql = msql & " Left Join  " & vbCrLf _
               & " (select k.linno,(select ','+ x.brandgrp from   " & vbCrLf _
               & " (select  CASE when ISNULL(c.u_ocrname5,'')='' then  case when ISNULL(c.u_lineno,'') like '%SAMPLE%' then '200' else c.U_LineNo end else c.U_OcrName5 end linno,t.U_BrandGroup brandgrp from [@INM_WIP1] b   " & vbCrLf _
               & " inner join [@INM_OWIP] c on c.DocEntry=b.DocEntry   " & vbCrLf _
               & " inner join oitm t on t.ItemCode=b.U_ItemCode   " & vbCrLf _
               & " where c.U_DocDate>='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and c.u_docdate<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and b.U_AccptWhs in ('STITCH','KAJA')  group by CASE when ISNULL(c.u_ocrname5,'')='' then  case when ISNULL(c.u_lineno,'') like '%SAMPLE%' then '200' else c.U_LineNo end else c.U_OcrName5 end ,t.U_BrandGroup )x  " & vbCrLf _
               & " where(x.linno = k.linno)    FOR XML PATH('')) brand  from  " & vbCrLf _
               & " (select tb.ItmsGrpNam,t.u_brandgroup, CASE when ISNULL(c.u_ocrname5,'')='' then  case when ISNULL(c.u_lineno,'') like '%SAMPLE%' then '200' else c.U_LineNo end else c.U_OcrName5 end linno ,isnull(c.U_OcrName5,'') ocrname, SUM(b.U_AccpQty) prodqty from [@INM_WIP1] b   " & vbCrLf _
               & " inner join [@INM_OWIP] c on c.DocEntry=b.DocEntry   " & vbCrLf _
               & " inner join oitm t on t.ItemCode=b.U_ItemCode   " & vbCrLf _
               & " inner join oitb tb on tb.ItmsGrpCod=t.ItmsGrpCod   " & vbCrLf _
               & "  where c.U_DocDate>='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and c.u_docdate<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and b.U_AccptWhs in ('STITCH','KAJA')  group by tb.ItmsGrpNam,c.U_LineNo,c.u_ocrname5,t.u_brandgroup) k  " & vbCrLf _
               & " group by k.linno) g on g.linno=k.linno  " & vbCrLf _
               & " left join (select k.linno,round(sum(k.cnt)/@nod,0) headcnt,round(sum(k.operator)/@nod,0) operator, SUM(k.tsalary) tsalary,SUM(k.hsalary) hsalary,(SUM(k.tsalary)+SUM(k.hsalary)) totsalary  from   " & vbCrLf _
               & " (select linno,COUNT(nempno) cnt,CASE when cdepartment like '%STITCHING%' or cdepartment like '%FINISHED%' then count(nempno) else 0 end operator,   " & vbCrLf _
               & "  isnull(CASE when cdepartment like '%STITCHING%' or cdepartment like '%FINISHED%' then SUM(daysalary) else 0 end,0)  tsalary , " & vbCrLf _
               & "  isnull(CASE when (cdepartment like '%HELPER%' or cdepartment like '%QUALITY%') then SUM(daysalary) else 0 end,0)  hsalary " & vbCrLf _
               & " from rrcolor.dbo.empdailysalary where dot>='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and dot<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and (cdepartment like '%STITCHING%' or cdepartment like '%HELPER%' or cdepartment like '%QUALITY%' or cdepartment like '%FINISHED%' )  " & vbCrLf _
               & " group by linno,cdepartment) k   group by linno) p on p.linno=k.linno   " & vbCrLf

        msql = msql & "left join (select b.floorno,c.empnam,b.linno,vname from rrcolor.dbo.floormaster b " & vbCrLf _
              & " Left Join " & vbCrLf _
              & " (select  k.floorno,(select '/'+ x.empname from (select floorno,empname from rrcolor.dbo.floorinchargemaster c group by floorno,empname ) x " & vbCrLf _
               & " where (x.floorno=k.floorno)  for XML PATH('')) as empnam  from (select * from rrcolor.dbo.floorinchargemaster ) k group by k.floorno) c on c.floorno=b.floorno " & vbCrLf _
               & " left join rrcolor.dbo.empmaster e on e.nempno=convert(int,b.inchargename)) ic on ic.linno=k.linno " & vbCrLf _
               & " right join rrcolor.dbo.linemaster m on m.Linno=k.linno collate SQL_Latin1_General_CP850_CI_AS   where CONVERT(int,ISNULL(m.nomachine,'0'))>0 " & vbCrLf _
               & " group by  k.itmsgrpnam,m.nlinno, k.linno,g.brand,p.headcnt,p.operator,ic.vname,m.nomachine order by k.ItmsGrpNam desc, abs(m.nlinno) "




        Dim dt4 As DataTable = getDataTable(msql)
        ' mtot = 0
        If dt4.Rows.Count > 0 Then

            dg.DataSource = dt4

            dg.ColumnHeadersDefaultCellStyle.Font = New Font(DataGridView.DefaultFont, FontStyle.Bold)
            dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'If chkcon.Checked = True Then
            dg.Columns(0).Width = 55
            dg.Columns(1).Width = 50
            dg.Columns(2).Width = 125
            dg.Columns(3).Width = 200
            dg.Columns(4).Width = 70
            dg.Columns(5).Width = 100
            'dg.Columns(4).Width = 70
            'dg.Columns(5).Width = 80

            dg.Columns(6).Width = 60
            dg.Columns(7).Width = 70
            dg.Columns(8).Width = 70
            dg.Columns(9).Width = 70
            dg.Columns(10).Width = 70
            dg.Columns(11).Width = 70
            dg.Columns(12).Width = 85
            dg.Columns(13).Width = 70



            For i = 0 To dg.Columns.Count - 1
                '        dgsr.Columns(i).ReadOnly = True
                If (i >= 6 And i <= 11) Or i = 13 Then
                    dg.Columns(i).ValueType = GetType(Int32)
                    dg.Columns(i).DefaultCellStyle.Format = ("0")
                    dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                ElseIf i = 12 Then
                    dg.Columns(i).ValueType = GetType(Decimal)
                    dg.Columns(i).DefaultCellStyle.Format = ("0.00")
                    dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                ElseIf i = 4 Or i = 5 Then
                    dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If

                '        If i > 1 Then
                '            dgsr.Columns(i).ValueType = GetType(Decimal)
                '            dgsr.Columns(i).DefaultCellStyle.Format = ("0.00")
                '        End If
            Next

        Else
            dg.DataSource = Nothing
        End If

        lblhtot.Text = 0
        lblotot.Text = 0
        lbltotmac.Text = 0
        lblntotmac.Text = 0
        lblshqty.Text = 0
        lblmskqty.Text = 0
        lblmskclqty.Text = 0
        For i = 0 To dg.Rows.Count - 1


            lbltotmac.Text = Val(lbltotmac.Text) + Val(dg.Rows(i).Cells(4).Value)
            'lblntotmac.Text = Val(lblntotmac.Text) + IIf(Val(dg.Rows(i).Cells(5).Value) > 0, Val(dg.Rows(i).Cells(5).Value), 0)
            lblntotmac.Text = Val(lblntotmac.Text) + Val(dg.Rows(i).Cells(5).Value)
            lblhtot.Text = Val(lblhtot.Text) + Val(dg.Rows(i).Cells(6).Value)
            lblotot.Text = Val(lblotot.Text) + Val(dg.Rows(i).Cells(7).Value)
            If Trim(dg.Rows(i).Cells(0).Value) = "SHIRT" Then
                lblshqty.Text = Val(lblshqty.Text) + Val(dg.Rows(i).Cells(11).Value)
            End If

            If Trim(dg.Rows(i).Cells(0).Value) = "MASK" Then
                If InStr(Trim(dg.Rows(i).Cells(3).Value), "WHITE") > 0 Then
                    lblmskqty.Text = Val(lblmskqty.Text) + Val(dg.Rows(i).Cells(10).Value)
                End If
                If InStr(Trim(dg.Rows(i).Cells(3).Value), "COL") > 0 Then
                    lblmskclqty.Text = Val(lblmskclqty.Text) + Val(dg.Rows(i).Cells(10).Value)
                End If
            End If


        Next

    End Sub
    Private Sub loadcons()
        sql2 = "select CASE when l.ItmsGrpNam='SHIRT' then 'SHIRT' else l.brand end lname, SUM(l.headcnt) headcnt,sum(l.operator) operator, SUM(l.prodqty) prodqty,SUM(l.issued_qty) Issued_Qty, " & vbCrLf _
               & "case when sum(l.prodqty)>0 and isnull(sum(l.operator),0)>0 then round(sum(l.prodqty)/isnull(sum(l.operator),0),2) else 0 end oper_avghead," & vbCrLf _
               & " case when sum(l.prodqty)>0 and isnull(sum(l.headcnt),0)>0 then round(sum(l.prodqty)/isnull(sum(l.headcnt),0),2) else 0 end Tot_avghead from ( " & vbCrLf _
                & "select k.itmsgrpnam, k.linno, ic.vname as inchargeName, g.brand,isnull(p.headcnt,0) headcnt,isnull(p.operator,0) operator, round(SUM(k.prodqty),2) prodqty , case when sum(k.prodqty)>0 and isnull(p.operator,0)>0 then round(sum(k.prodqty)/isnull(p.operator,0),2) else 0 end avgperhead ,SUM(k.issdqty) Issued_Qty from (" & vbCrLf _
                     & " select kk.itmsgrpnam,kk.u_brandgroup,kk.linno,SUM(kk.prodqty) prodqty,SUM(kk.issdqty) issdqty from  " & vbCrLf _
                     & " (select tb.ItmsGrpNam,t.u_brandgroup, CASE when ISNULL(c.u_ocrname5,'')='' then  case when ISNULL(c.u_lineno,'') like '%SAMPLE%' then '200' else c.U_LineNo end else c.U_OcrName5 end linno , b.U_CutNo, SUM(b.U_AccpQty) prodqty,0 issdqty from [@INM_WIP1] b   " & vbCrLf _
                     & " left join [@INM_OWIP] c on c.DocEntry=b.DocEntry  " & vbCrLf _
                     & " left join oitm t on t.ItemCode=b.U_ItemCode  " & vbCrLf _
                     & " left join oitb tb on tb.ItmsGrpCod=t.ItmsGrpCod  " & vbCrLf _
                     & " where c.U_DocDate>='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and c.u_docdate<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and b.U_AccptWhs in ('STITCH','KAJA') and c.U_TranType='RG' and c.u_lineno not in ('General','SC') group by tb.ItmsGrpNam,c.U_LineNo,c.u_ocrname5,t.u_brandgroup,b.u_cutno  " & vbCrLf _
                     & " union all  " & vbCrLf _
                     & " select tb.ItmsGrpNam,t.u_brandgroup,CASE when c.U_Type='RG' then  case when c.u_flno like '%SAMPLE%' then '200' else c.U_FLNo end else '500' end  linno,b.U_CutNo,0 prodqty, SUM(b.U_IssdQty) issdqty from [@INM_PDE1] b   " & vbCrLf _
                     & " left join [@INM_OPDE] c on c.DocEntry=b.DocEntry  " & vbCrLf _
                     & " left join oitm t on t.ItemCode=b.U_ItemCode  " & vbCrLf _
                     & " left join OITB tb on tb.ItmsGrpCod=t.ItmsGrpCod  " & vbCrLf _
                     & " where c.U_DocDate='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and c.u_docdate<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and  c.U_DfOperCode in ('STGD','KAJAGD') and c.u_flno not in ('General','SC')  group by tb.ItmsGrpNam,t.u_brandgroup,c.U_FLNo ,b.u_cutno,c.u_type) kk  " & vbCrLf _
                     & " group by kk.itmsgrpnam,kk.u_brandgroup,kk.linno) k  " & vbCrLf _
                      & " Left Join  " & vbCrLf _
                     & " (select k.linno,(select ','+ x.brandgrp from   " & vbCrLf _
                     & " (select  CASE when ISNULL(c.u_ocrname5,'')='' then  case when ISNULL(c.u_lineno,'') like '%SAMPLE%' then '200' else c.U_LineNo end else c.U_OcrName5 end linno,t.U_BrandGroup brandgrp from [@INM_WIP1] b   " & vbCrLf _
                     & " left join [@INM_OWIP] c on c.DocEntry=b.DocEntry   " & vbCrLf _
                     & " left join oitm t on t.ItemCode=b.U_ItemCode   " & vbCrLf _
                     & " where c.U_DocDate='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and c.u_docdate<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and b.U_AccptWhs in ('STITCH','KAJA')  group by CASE when ISNULL(c.u_ocrname5,'')='' then  case when ISNULL(c.u_lineno,'') like '%SAMPLE%' then '200' else c.U_LineNo end else c.U_OcrName5 end ,t.U_BrandGroup )x  " & vbCrLf _
                     & " where(x.linno = k.linno)    FOR XML PATH('')) brand  from  " & vbCrLf _
                     & " (select tb.ItmsGrpNam,t.u_brandgroup, CASE when ISNULL(c.u_ocrname5,'')='' then  case when ISNULL(c.u_lineno,'') like '%SAMPLE%' then '200' else c.U_LineNo end else c.U_OcrName5 end linno ,isnull(c.U_OcrName5,'') ocrname, SUM(b.U_AccpQty) prodqty from [@INM_WIP1] b   " & vbCrLf _
                     & " left join [@INM_OWIP] c on c.DocEntry=b.DocEntry   " & vbCrLf _
                     & " left join oitm t on t.ItemCode=b.U_ItemCode   " & vbCrLf _
                     & " left join oitb tb on tb.ItmsGrpCod=t.ItmsGrpCod   " & vbCrLf _
                     & "  where c.U_DocDate='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and c.u_docdate<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and b.U_AccptWhs in ('STITCH','KAJA')  group by tb.ItmsGrpNam,c.U_LineNo,c.u_ocrname5,t.u_brandgroup) k  " & vbCrLf _
                     & " group by k.linno) g on g.linno=k.linno  " & vbCrLf _
                     & " left join (select k.linno,sum(k.cnt) headcnt,sum(k.operator) operator  from   " & vbCrLf _
                     & " (select linno,COUNT(nempno) cnt,CASE when cdepartment like '%STITCHING%' then count(nempno) else 0 end operator   " & vbCrLf _
                     & " from rrcolor.dbo.empdailysalary where dot>='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and dot<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and (cdepartment like '%STITCHING%' or cdepartment like '%HELPER%' or cdepartment like '%QUALITY%' )  " & vbCrLf _
                     & " group by linno,cdepartment) k   group by linno) p on p.linno=k.linno   " & vbCrLf

        sql2 = sql2 & "left join (select b.floorno,c.empnam,b.linno,vname from rrcolor.dbo.floormaster b " & vbCrLf _
              & " Left Join " & vbCrLf _
              & " (select  k.floorno,(select '/'+ x.empname from (select floorno,empname from rrcolor.dbo.floorinchargemaster c group by floorno,empname ) x " & vbCrLf _
               & " where (x.floorno=k.floorno)  for XML PATH('')) as empnam  from (select * from rrcolor.dbo.floorinchargemaster ) k group by k.floorno) c on c.floorno=b.floorno " & vbCrLf _
               & " left join rrcolor.dbo.empmaster e on e.nempno=convert(int,b.inchargename)) ic on ic.linno=k.linno " & vbCrLf _
               & " group by  k.itmsgrpnam, k.linno,g.brand,p.headcnt,p.operator,ic.vname) l  " & vbCrLf _
               & "  group by CASE when l.ItmsGrpNam='SHIRT' then 'SHIRT' else l.brand end "

        Dim dt As DataTable = getDataTable(sql2)
        ' mtot = 0
        If dt.Rows.Count > 0 Then
            dg2.DataSource = dt
            dg2.ColumnHeadersDefaultCellStyle.Font = New Font(DataGridView.DefaultFont, FontStyle.Bold)
            dg2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            For i = 0 To dg2.Columns.Count - 1
                '        dgsr.Columns(i).ReadOnly = True
                If i = 3 Or i = 4 Then
                    dg2.Columns(i).ValueType = GetType(Int32)
                    dg2.Columns(i).DefaultCellStyle.Format = ("0")
                    dg2.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                ElseIf i = 5 Or i = 6 Then
                    dg2.Columns(i).ValueType = GetType(Decimal)
                    dg2.Columns(i).DefaultCellStyle.Format = ("0.00")
                    dg2.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
                '        If i > 1 Then
                '            dgsr.Columns(i).ValueType = GetType(Decimal)
                '            dgsr.Columns(i).DefaultCellStyle.Format = ("0.00")
                '        End If
            Next
        Else
            dg2.DataSource = Nothing
        End If

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Call loaddata()
        Call loadcons()
        Lblshper.Text = 0
        lblmskper.Text = 0
        lblmskclper.Text = 0

        lblsal.Text = Format(getsalaryst("SHIRT"), "##########0.00")
        lblmsksal.Text = Format(getsalaryst("WHITE MASK"), "##########0.00")
        lblmskclsal.Text = Format(getsalaryst("COLOR MASK"), "##########0.00")

        Lblshper.Text = Format(Val(lblsal.Text) / Val(lblshqty.Text), "#####0.00") & "%"
        lblmskper.Text = Format(Val(lblmsksal.Text) / Val(lblmskqty.Text), "#####0.00") & "%"
        lblmskclper.Text = Format(Val(lblmskclsal.Text) / Val(lblmskclqty.Text), "#####0.00") & "%"

    End Sub

    Private Function getsalaryst(ByVal mtype As String) As Double
        If mtype = "SHIRT" Then
            sql3 = " select isnull(SUM(k.sal),0) salary from " & vbCrLf _
                  & " (select SUM(daysalary) sal from rrcolor.dbo.empdailysalary where dot>='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and dot<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and csno in (13,14,39,47,49,50) " & vbCrLf _
                  & "   union all " & vbCrLf _
                  & " select SUM(amt) sal from prodcost.dbo.dailycontractdata where cdate>='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and cdate<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "') k "

        ElseIf mtype = "WHITE MASK" Or mtype = "COLOR MASK" Then
            sql3 = " select isnull(SUM(j.salary),0) sal,j.styp from (" & vbCrLf _
                 & "select CASE when k.grpnam like '%WHITE%' then 'WHITE MASK' when k.grpnam like '%COL%' then 'COLOR MASK' else '' end styp,   SUM(k.sal) salary from " & vbCrLf _
                  & "(select CASE when csno in (44,45) then '2 LAYER MASK COLOR'    when csno in (7,36) then '2 LAYER MASK WHITE' " & vbCrLf _
                  & " when csno in (33,38) then '3 LAYER MASK COLOR'    when csno in (35,37) then '3 LAYER MASK WHITE'  else '' end grpnam, " & vbCrLf _
                  & " SUM(daysalary) sal from rrcolor.dbo.empdailysalary where dot>='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and dot<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and csno in (7,9,26,33,35,36,37,38,40,42,44,45) " & vbCrLf _
                  & " group by csno) k group by k.grpnam)j  where j.styp='" & mtype & "'  group by j.styp"
        Else

        End If

        If mtype = "SHIRT" Or mtype = "WHITE MASK" Or mtype = "COLOR MASK" Then
            Dim dt As DataTable = getDataTable(sql3)
            ' mtot = 0
            msal1 = 0
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    msal1 = row(0)
                Next
            End If

        Else
            msal1 = 0
        End If
        Return msal1
    End Function


    'msql=" select m.linno,m.cnt,count(c.nemp_id) operator from  " & vbcrlf _
    '    &" (select linno,sum(nomachine) cnt from rrcolor.dbo.linemaster where linno not in ('15','16','17','18','19','20','General','SAMPLE LINE','500','600','700') " & vbcrlf _
    '    &" group by linno) m " & vbcrlf _
    '    &" left join  rrcolor.dbo.empdailysalary c on c.linno=m.Linno " & vbcrlf _
    '    &" where c.dot='2022-05-11' and c.csno in (49,13) " & vbcrlf _
    '    &" group by m.linno,m.cnt order by convert(int,m.linno) "

End Class
Public Class FrmLineCost
    Dim msql As String
    Private Sub BtnDisp_Click(sender As System.Object, e As System.EventArgs) Handles BtnDisp.Click

        If MsgBox("Check Particular Date with Target pcs Cost ", MsgBoxStyle.YesNo) = vbYes Then
            Call loaddaydata()
        Else
            Call loaddata()
        End If
    End Sub
    Private Sub loaddata()
        Cursor = Cursors.WaitCursor
        msql = "declare @d1 as nvarchar(20) " _
              & "declare @d2 as nvarchar(20) " _
                & "set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'" _
                & "set @d2='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "'" _
                & "select kk.subdept,kk.linno, sum(kk.cnt) cnt,sum(kk.salary) salary,sum(isnull(kk.qty,0)+isnull(p.qty,0)) Qty ,sum(kk.amt) Amt,case when kk.subdept='CONTRACTOR' and sum(isnull(kk.qty,0)+isnull(p.qty,0))>0 then sum(kk.amt)/sum(isnull(kk.qty,0)+isnull(p.qty,0)) else " _
                & "case when sum(isnull(kk.qty,0)+isnull(p.qty,0))>0 then sum(kk.salary)/sum(isnull(kk.qty,0)+isnull(p.qty,0)) else  sum(kk.salary)/1 end end cost from ( " _
                & "select j.subdept,j.linno, sum(j.cnt) cnt,sum(j.actsalary) salary,isnull(sum(j.qty),0) Qty ,isnull(sum(j.amt),0) Amt from ( " _
                & "select  k.nempno,k.vname,case when k.subdept='STITCHING' and k.subsection='TAILOR(PIECE RATE)' then 'CONTRACTOR' else k.subdept end subdept, " _
                & "k.subsection,case when k.subdept='STITCHING' and k.subsection<>'TAILOR(PIECE RATE)'  and k.subcategory not in ('IE','MANAGER','INCHARGE','PRODUCTION') then k.subcategory else '' end Linno, " _
                & "avg(k.cnt) cnt,sum(k.dotcnt) dotcnt,k.totsalary,(k.totsalary/26)*  " _
                & "case when sum(k.dotcnt)>=20 then (2+ sum(k.dotcnt)) when sum(k.dotcnt)>12 and sum(k.dotcnt)<20 then (1 +sum(k.dotcnt)) else sum(k.dotcnt) end  actsalary,sum(k.qty) Qty,sum(k.amt) Amt from ( " _
                & "select b.nempno,em.vname, case when isnull(em.subdept,'')='' then em.vdepartment else em.subdept end subdept,isnull(em.subsection,'') subsection,isnull(em.subcategory,'') subcategory, b.dot,count(b.nempno) cnt,count(b.dot) dotcnt,em.totsalary,g.qty,g.amt  from rrcolor.dbo.empdailysalary b " _
                & "inner join rrcolor.dbo.empmaster em on em.nempno= b.nempno " _
                & "left join ( " _
                & " select  cdate,nempno,vname,isnull(sum(qty),0) qty,isnull(sum(amt),0) amt from prodcost.dbo.dailycontractdata where cdate>=@d1 and cdate<=@d2 " _
                & " group by cdate,nempno,vname) g on g.nempno=b.nempno and g.cdate=b.dot " _
                & " where b.dot>=@d1 and b.dot<=@d2 and em.vshrtname='ACC' " _
                & " group by b.nempno,em.vname,b.dot, case when isnull(em.subdept,'')='' then em.vdepartment else em.subdept end,em.subsection,em.subcategory,em.totsalary,g.qty,g.amt) k " _
                & "group by k.nempno,k.vname,k.subdept,k.subsection,k.subcategory,k.totsalary) j " _
                & " group by j.subdept,j.linno) kk " _
                & "left join (select case when c.u_opercode in ('CUTGD','FUSGD')  and t.ItmsGrpCod not in (105,111) then 'CUTTING' " _
                & "        when c.u_opercode in('CUTGD','FUSGD')  and t.ItmsGrpCod in (105,111) then 'CUTTING COLCAN'  " _
                & "        when c.u_opercode='EMBGD' then 'EMBROIDING' " _
                & "        when c.u_opercode in ('KAJAGD','STGD') then 'STITCHING' " _
                & "        when c.u_opercode='IRONGD' then 'IRONING' else '' end opercode, " _
                & "    case when c.u_lineno not in ('GENERAL','IH','SAMPLE LINE') then c.u_lineno else '' end u_lineno,  " _
                & "     isnull(sum(b.u_accpqty),0) qty from antsprodlive.dbo.[@inm_wip1] b with (nolock) " _
                & "     inner join antsprodlive.dbo.[@inm_owip] c with (nolock) on c.docentry=b.docentry " _
                & "     inner join antsprodlive.dbo.oitm t on t.itemcode=b.u_itemcode " _
                & "   where c.u_docdate>=@d1 and c.u_docdate<=@d2 and c.u_trantype='RG' and left(ltrim(rtrim(c.u_lineno)),2)<>'SC' and t.itmsgrpcod not in (102) " _
                & "  group by case when c.u_opercode in('CUTGD','FUSGD')  and t.ItmsGrpCod not in (105,111) then 'CUTTING' " _
                & " when c.u_opercode in('CUTGD','FUSGD')  and t.ItmsGrpCod in (105,111) then 'CUTTING COLCAN'  " _
                & " when c.u_opercode='EMBGD' then 'EMBROIDING' " _
                & " when c.u_opercode in ('KAJAGD','STGD') then 'STITCHING' " _
                & " when c.u_opercode='IRONGD' then 'IRONING' else '' end " _
                & " ,case when c.u_lineno not in ('GENERAL','IH','SAMPLE LINE') then c.u_lineno else '' end) p on p.opercode collate SQL_Latin1_General_CP1_CI_AS =kk.subdept  and p.u_lineno collate SQL_Latin1_General_CP1_CI_AS=kk.linno  " _
                & " group by kk.subdept,kk.linno " _
                & " order by case when kk.subdept in ('CUTTING','CUTTING COLCAN') then 1 when kk.subdept in ('EMBROIDING') then 2 " _
                & " when kk.subdept in ('CONTRACTOR','STITCHING','PRODUCTION') then 3 " _
                & " when kk.subdept in ('IRONING') then 4  when kk.subdept in ('SOURCING','PATTERN') then 5 " _
                & " when kk.subdept in ('STORES & ACCESSORIES','LOADING','FABRIC') then 6 " _
                & " else 7 end,kk.subdept "


        Dim dt4 As DataTable = getDataTable(msql)
        ' mtot = 0
        If dt4.Rows.Count > 0 Then

            Dg.DataSource = dt4

            Dg.ColumnHeadersDefaultCellStyle.Font = New Font(DataGridView.DefaultFont, FontStyle.Bold)
            Dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'If chkcon.Checked = True Then
            Dg.Columns(0).Width = 155
            Dg.Columns(1).Width = 50
            Dg.Columns(2).Width = 50
            Dg.Columns(3).Width = 125
            Dg.Columns(4).Width = 100
            Dg.Columns(5).Width = 100
            'dg.Columns(4).Width = 70
            'dg.Columns(5).Width = 80

            Dg.Columns(6).Width = 80



            For i As Integer = 0 To Dg.Columns.Count - 1
                '        dgsr.Columns(i).ReadOnly = True
                If i = 2 Or i = 4 Then
                    Dg.Columns(i).ValueType = GetType(Int32)
                    Dg.Columns(i).DefaultCellStyle.Format = ("0")
                    Dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                ElseIf i = 3 Or i = 5 Or i = 6 Then
                    Dg.Columns(i).ValueType = GetType(Decimal)
                    Dg.Columns(i).DefaultCellStyle.Format = ("0.00")
                    Dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                ElseIf i = 0 Or i = 1 Then
                    Dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If

                '        If i > 1 Then
                '            dgsr.Columns(i).ValueType = GetType(Decimal)
                '            dgsr.Columns(i).DefaultCellStyle.Format = ("0.00")
                '        End If
            Next

        Else
            Dg.DataSource = Nothing
        End If
        Cursor = Cursors.Default

    End Sub

    Private Sub BtnCls_Click(sender As System.Object, e As System.EventArgs) Handles BtnCls.Click
        Dg.DataSource = Nothing
    End Sub

    Private Sub FrmLineCost_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width + 5
        Me.Height = My.Computer.Screen.Bounds.Height - 20
    End Sub

    Private Sub BtnExcel_Click(sender As System.Object, e As System.EventArgs) Handles BtnExcel.Click

        gridexcelexport(Dg, 1)
    End Sub

    Private Sub BtnExit_Click(sender As System.Object, e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub loaddaydata()
        If Format(CDate(mskdatefr.Text), "yyyy-MM-dd") = Format(CDate(mskdateto.Text), "yyyy-MM-dd") Then
            'msql = "select k.subdept,k.linno1,k.salary,isnull(tr.prdtarget,0) Targetpcs,  k.salary/ case when isnull(tr.prdtarget,0)>0  then tr.prdtarget else nullif(case when k.subdept='STITCHING' then 1 else 0 end,0) end prdcost from ( " _
            '     & "select case when c.subdept='STITCHING' and c.subsection='TAILOR(PIECE RATE)' then 'CONTRACTOR' else c.subdept end subdept, " _
            '     & "case when c.subdept='STITCHING' and c.subsection<>'TAILOR(PIECE RATE)'  and c.subcategory not in ('IE','MANAGER','INCHARGE','PRODUCTION') then c.subcategory else '' end Linno1, " _
            '     & "sum(b.daysalary) salary from rrcolor.dbo.empdailysalary b " _
            '     & "inner join rrcolor.dbo.empmaster c on c.nempno=b.nempno " _
            '     & " where b.dot>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'  and b.dot<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' and c.vshrtname='ACC' " _
            '     & " group by case when c.subdept='STITCHING' and c.subsection='TAILOR(PIECE RATE)' then 'CONTRACTOR' else c.subdept end, " _
            '     & " case when c.subdept='STITCHING' and c.subsection<>'TAILOR(PIECE RATE)'  and c.subcategory not in ('IE','MANAGER','INCHARGE','PRODUCTION') then c.subcategory else '' end) k " _
            '     & " left join ( " _
            '     & "       select a.[lineno] linno,d.nopcs,a.nomac,(d.nopcs*a.nomac) prdtarget from prodcost.dbo.operf a " _
            '     & "       inner join prodcost.dbo.incentivemaster d on d.brand=a.brand " _
            '     & "        where date='2025-02-11'  group by a.[lineno],d.nopcs,a.nomac) tr on tr.linno=k.Linno1 " _
            '     & "   order by case when k.subdept in ('CUTTING','CUTTING COLCAN') then 1 " _
            '     & " when k.subdept in ('EMBROIDING') then 2  when k.subdept in ('CONTRACTOR','STITCHING','PRODUCTION') then 3 " _
            '     & " when k.subdept in ('IRONING') then 4  when k.subdept in ('SOURCING','PATTERN') then 5 " _
            '     & " when k.subdept in ('STORES & ACCESSORIES','LOADING','FABRIC') then 6  else 7 end,k.subdept"


            msql = "declare @d1 as nvarchar(20) " _
                    & "declare @d2 as nvarchar(20) " _
                    & "set @d1='2025-02-12' " _
                    & "set @d2='2025-02-12' " _
                    & "select k.subdept,k.linno1,k.salary, isnull(cn.amt,0) contractamt, isnull(case when isnull(tr.prdtarget,0)>0 then isnull(tr.prdtarget,0) else isnull(cn.qty,0) end,0) Targetpcs,  case when isnull(cn.cost,0)>0 then isnull(cn.cost,0) else isnull(k.salary/ case when isnull(tr.prdtarget,0)>0  then tr.prdtarget else nullif(case when k.subdept='STITCHING' then 1 else 0 end,0) end,0) end prdcost from ( " _
                 & "select case when c.subdept='STITCHING' and c.subsection='TAILOR(PIECE RATE)' then 'CONTRACTOR' else c.subdept end subdept, " _
                 & "case when c.subdept='STITCHING' and c.subsection<>'TAILOR(PIECE RATE)'  and c.subcategory not in ('IE','MANAGER','INCHARGE','PRODUCTION') then c.subcategory else '' end Linno1," _
                 & "sum(b.daysalary) salary from rrcolor.dbo.empdailysalary b " _
                 & "inner join rrcolor.dbo.empmaster c on c.nempno=b.nempno " _
                 & " where b.dot>=@d1 and b.dot<=@d2 and c.vshrtname='ACC' " _
                 & " group by case when c.subdept='STITCHING' and c.subsection='TAILOR(PIECE RATE)' then 'CONTRACTOR' else c.subdept end, " _
                 & " case when c.subdept='STITCHING' and c.subsection<>'TAILOR(PIECE RATE)'  and c.subcategory not in ('IE','MANAGER','INCHARGE','PRODUCTION') then c.subcategory else '' end) k " _
                 & " left join ( " _
                 & "          select a.[lineno] linno,d.nopcs,a.nomac,(d.nopcs*a.nomac) prdtarget from prodcost.dbo.operf a " _
                 & "          inner join prodcost.dbo.incentivemaster d on d.brand=a.brand " _
                 & "          where date=@d1  group by a.[lineno],d.nopcs,a.nomac) tr on tr.linno=k.Linno1 " _
                 & " left join (select  'CONTRACTOR' vname,isnull(sum(qty),0) qty,isnull(sum(amt),0) amt, (isnull(sum(amt),0)/isnull(sum(qty),0)) cost  from prodcost.dbo.dailycontractdata where cdate>=@d1 and cdate<=@d2) cn on cn.vname=k.subdept " _
                 & " order by case when k.subdept in ('CUTTING','CUTTING COLCAN') then 1 " _
                 & " when k.subdept in ('EMBROIDING') then 2 when k.subdept in ('CONTRACTOR','STITCHING','PRODUCTION') then 3 " _
                 & " when k.subdept in ('IRONING') then 4  when k.subdept in ('SOURCING','PATTERN') then 5 " _
                 & " when k.subdept in ('STORES & ACCESSORIES','LOADING','FABRIC') then 6  else 7 end,k.subdept "

            Dim dt4 As DataTable = getDataTable(msql)
            ' mtot = 0
            If dt4.Rows.Count > 0 Then

                Dg.DataSource = dt4

                Dg.ColumnHeadersDefaultCellStyle.Font = New Font(DataGridView.DefaultFont, FontStyle.Bold)
                Dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                'If chkcon.Checked = True Then
                Dg.Columns(0).Width = 155
                Dg.Columns(1).Width = 50
                Dg.Columns(2).Width = 50
                Dg.Columns(3).Width = 125
                Dg.Columns(4).Width = 100
                Dg.Columns(5).Width = 100
                'dg.Columns(4).Width = 70
                'dg.Columns(5).Width = 80

                'Dg.Columns(6).Width = 80



                For i As Integer = 0 To Dg.Columns.Count - 1
                    '        dgsr.Columns(i).ReadOnly = True
                    If i = 4 Then
                        Dg.Columns(i).ValueType = GetType(Int32)
                        Dg.Columns(i).DefaultCellStyle.Format = ("0")
                        Dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                    ElseIf i = 2 Or i = 3 Or i = 5 Then
                        Dg.Columns(i).ValueType = GetType(Decimal)
                        Dg.Columns(i).DefaultCellStyle.Format = ("0.00")
                        Dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    ElseIf i = 0 Or i = 1 Then
                        Dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    End If

                    '        If i > 1 Then
                    '            dgsr.Columns(i).ValueType = GetType(Decimal)
                    '            dgsr.Columns(i).DefaultCellStyle.Format = ("0.00")
                    '        End If
                Next

            Else
                Dg.DataSource = Nothing
            End If


        Else
            MsgBox("Single Date only ")
        End If

    End Sub

End Class
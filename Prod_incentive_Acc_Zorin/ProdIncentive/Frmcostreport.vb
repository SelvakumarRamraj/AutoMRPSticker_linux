Imports System.IO
Imports AxMSFlexGridLib
Imports System.Data.OleDb
Imports System.Data
Imports CarlosAg.ExcelXmlWriter
Imports System.Data.SqlClient

Public Class Frmcostreport
    Dim msql, msql2, msql3, msql4 As String
    Dim mdocno As Long
    Dim miron As Double
    Dim j, i, k, jk, l, msel As Int32
    Dim trans As SqlTransaction
    Dim mihavday As Single
    Dim mscavday As Single

    Private Sub cmdexit_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.ClickButtonArea
        Me.Close()
    End Sub

    Private Sub Frmcostreport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.MdiParent = MDIForm
        Me.Height = MDIFORM1.Height - 25
        Me.Width = My.Computer.Screen.Bounds.Width
        cmbtype.Items.Add("IN HOUSE")
        cmbtype.Items.Add("SC")
        Call flxhead()
        cmbtype.Text = "IN HOUSE"
        mskdatefr.Text = Format(Now(), "dd-MM-yyyy")
        mskdateto.Text = Format(Now(), "dd-MM-yyyy")

    End Sub

    Private Sub flxhead()
        flx.Rows = 1
        flx.Cols = 9
        flx.set_ColWidth(0, 1900)
        flx.set_ColWidth(1, 1400)
        flx.set_ColWidth(2, 1200)
        flx.set_ColWidth(3, 1500)
        flx.set_ColWidth(4, 1500)
        flx.set_ColWidth(5, 1200)
        flx.set_ColWidth(6, 1000)
        flx.set_ColWidth(7, 1000)
        flx.set_ColWidth(8, 1000)
        'flx.set_ColWidth(9, 1000)

        flx.Row = 0
        flx.Col = 0
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 0, "Department")

        flx.Col = 1
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 1, "WareHouse")

        flx.Col = 2
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 2, "Target")

        flx.Col = 3
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 3, "Tot Present")

        flx.Col = 4
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 4, "Tot Wages")

        flx.Col = 5
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 5, "Tot.Prod")

        flx.Col = 6
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 6, "Avg.Wages")


        flx.Col = 7
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 7, "Manual Ironing")

        flx.Col = 8
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 8, "Tot_Avg")
        'flx.set_ColAlignment(0, 2)
        'flx.Rows = flx.Rows + 1
        'flx.Row = flx.Rows - 1
    End Sub

    Private Sub flxehead()
        flxe.Rows = 1
        flxe.Cols = 5
        flxe.set_ColWidth(0, 3500)
        flxe.set_ColWidth(1, 1400)
        flxe.set_ColWidth(2, 1200)
        flxe.set_ColWidth(3, 1500)
        flxe.set_ColWidth(4, 1500)
        'flx.set_ColWidth(5, 1200)
        'flx.set_ColWidth(6, 1000)
        'flx.set_ColWidth(7, 1000)
        'flx.set_ColWidth(8, 1000)
        'flx.set_ColWidth(9, 1000)

        flxe.Row = 0
        flxe.Col = 0
        flxe.CellAlignment = 3
        flxe.CellFontBold = True
        flxe.set_TextMatrix(0, 0, "Acct Name")

        flxe.Col = 1
        flxe.CellAlignment = 3
        flxe.CellFontBold = True
        flxe.set_TextMatrix(0, 1, "Tot.Expenses")

        flxe.Col = 2
        flxe.CellAlignment = 3
        flxe.CellFontBold = True
        flxe.set_TextMatrix(0, 2, "InHouse Exp.")

        flxe.Col = 3
        flxe.CellAlignment = 3
        flxe.CellFontBold = True
        flxe.set_TextMatrix(0, 3, "Tot Prod")

        flxe.Col = 4
        flxe.CellAlignment = 3
        flxe.CellFontBold = True
        flxe.set_TextMatrix(0, 4, "Avg Exp")

        ''flx.set_ColAlignment(0, 2)
        'flxe.Rows = flx.Rows + 1
        'flxe.Row = flx.Rows - 1
    End Sub


    Private Sub loaddata()
        Dim msql As String
        lbltotwage.Text = 0
        totlblexp.Text = 0
        lbltotprod.Text = 0
        lbltotprsnt.Text = 0
        lblrgqty.Text = 0
        lbllsqty.Text = 0
        mihavday = 0

        If Val(Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "dd")) > 0 Then
            msql = "select * from " & Trim(mcostdbnam) & ".dbo.attlog where attdate='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' order by empid"

            msql = ""

            msql = "declare @d1 nvarchar(20) " & vbCrLf _
            & " declare @d2 nvarchar(20) " & vbCrLf _
            & " declare @ihvday real " & vbCrLf _
            & "set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' " & vbCrLf _
            & "set @d2='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' " & vbCrLf _
            & " select @ihvday= AVG(k.prsnt) from (select b.empid, Sum(b.prsnt) prsnt from acccost.dbo.attlog b " & vbCrLf _
            & " left join acccost.dbo.Empmaster e on e.empid=b.empid " & vbCrLf _
            & " where b.attdate>=@d1 and b.attdate<=@d2 and (e.costtype='IN HOUSE' or isnull(e.ih,0)>0) group by b.empid) k  " & vbCrLf

            msql = msql & "SELECT @ihvday as avday,  Kk.subdept,Kk.costtype,Kk.tarket,(kK.totprsnt/(select count(k.dd) from(select attdate as dd from " & Trim(mcostdbnam) & ".dbo.attlog where attdate>=@d1 and attdate<=@d2 group by attdate) k)) as totprsnt," & vbCrLf _
            & " (KK.totexp) totexp,KK.totprod,KK.avgwage,isnull(g.mqty,0) mqty FROM " & vbCrLf _
            & "(select k.subdept,k.costtype,sum(k.tarket) tarket,sum(k.prsnt) totprsnt, isnull(sum(k.exps+k.epf+k.daybonus),0) totexp,(isnull(jb.accqty,0)+ISNULL(fp.netprod,0)) totprod,case when (isnull(jb.accqty,0)+ISNULL(fp.netprod,0))>0 then SUM(k.exps+k.epf+isnull(k.daybonus,0))/(ISNULL(jb.accqty,0)+ISNULL(fp.netprod,0)) else 0 end avgwage,sum(k.epf) epf from " & vbCrLf _
            & "(select e.empid,e.empname,e.department,e.salary,e.subdept,e.costtype,e.[target] tarket,e.per,c.prsnt,datediff(day, @d1, dateadd(month, 1, @d1)) noday,CASE when e.ih=100 then e.salary/datediff(day, @d1, dateadd(month, 1, @d1)) else  (e.salary/datediff(day, @d1, dateadd(month, 1, @d1)))*e.ih/100 end daysalary," & vbCrLf _
            & " CASE when e.ih=100 then c.prsnt*(e.salary/datediff(day, @d1, dateadd(month, 1, @d1))) else c.prsnt* ((e.salary/datediff(day, @d1, dateadd(month, 1, @d1))))*e.ih/100 end exps, " & vbCrLf _
            & " CASE when e.salary>=15000 and e.ih>e.sc then round((15000/datediff(day, @d1, dateadd(month, 1, @d1)))*12/100,2) else round(((e.salary/datediff(day, @d1, dateadd(month, 1, @d1))) *70/100)*12/100,2) end epf," & vbCrLf _
            & " case when e.ih=100 then (e.salary/12)/datediff(day, @d1, dateadd(month, 1, @d1)) else (((e.salary*e.ih)/100) /12)/datediff(day, @d1, dateadd(month, 1, @d1)) end daybonus,e.ih from " & Trim(mcostdbnam) & ".dbo.Empmaster e " & vbCrLf _
            & "left join " & Trim(mcostdbnam) & ".dbo.attlog c on c.empid=e.empid " & vbCrLf _
            & " where c.attdate>=@d1 and c.attdate<=@d2 ) k " & vbCrLf _
            & " Left Join " & vbCrLf _
            & "(select  case when d.u_opercode IN ('CUTGD') then 'CUTTING'  " & vbCrLf _
            & " when d.u_opercode IN ('FUSGD') then 'FUSING' " & vbCrLf _
            & " when d.u_opercode IN ('EMBGD') then 'EMB' " & vbCrLf _
            & " when d.u_opercode IN ('STGD','KAJAGD') then 'STITCHING' " & vbCrLf _
            & " when d.u_opercode IN ('IRONGD') then 'IRONING' " & vbCrLf _
            & " Else       d.u_opercode   end u_opercode, " & vbCrLf _
            & " SUM(U_AccpQty) accqty from [@inm_wip1] b " & vbCrLf _
            & " left join [@inm_owip] d on d.docentry=b.docentry " & vbCrLf _
            & " where d.u_docdate>=@d1 and d.u_docdate<=@d2 and left(b.u_itemcode,3)<>'ACC' " & vbCrLf _
            & " group by  case when d.u_opercode IN ('CUTGD') then 'CUTTING'  " & vbCrLf _
            & " when d.u_opercode IN ('FUSGD') then 'FUSING' " & vbCrLf _
            & " when d.u_opercode IN ('EMBGD') then 'EMB' " & vbCrLf _
            & "  when d.u_opercode IN ('STGD','KAJAGD') then 'STITCHING' " & vbCrLf _
            & "when d.u_opercode IN ('IRONGD') then 'IRONING' " & vbCrLf _
            & " Else d.u_opercode end ) jb on jb.u_opercode= k.subdept collate SQL_Latin1_General_CP850_CI_AS " & vbCrLf _
            & " left join (select 'FUSING' costtyp,SUM(b.u_accpqty) prod,SUM(b.u_accpqty)/3 as netprod   from [@INM_WIP1] b " & vbCrLf _
            & " left join [@INM_OWIP] c on c.DocEntry=b.DocEntry " & vbCrLf _
            & " where c.U_DocDate>=@d1 and c.U_DocDate<=@d2 and LEFT(b.U_ItemCode,3)='ACC' " & vbCrLf _
            & " and upper(left(b.u_itemname,4)) in ('SKIN','PATC','NECK') and c.U_OperCode in('CUTGD')) fp on fp.costtyp= k.subdept collate SQL_Latin1_General_CP850_CI_AS " & vbCrLf _
            & " where k.costtype='" & cmbtype.Text & "' or isnull(k.ih,0)>0 " & vbCrLf _
            & "group by k.subdept,k.costtype,jb.accqty,fp.netprod) kk " & vbCrLf _
            & " left join (select 'IRONING' as costtype,sum(qty) mqty from " & Trim(mcostdbnam) & ".dbo.manualiron where idate>=@d1 and idate<=@d2 ) g on g.costtype=kk.subdept " & vbCrLf _
            & "group by Kk.subdept,Kk.costtype,Kk.tarket,kk.totprsnt, KK.totexp,KK.totprod,KK.avgwage,kk.epf,g.mqty" & vbCrLf _
            & "order by kk.totprod "

            'msql = "select * from inward1 where date>='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "'"

            miron = 0
            Dim CMD As New SqlCommand(msql, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            CMD.CommandTimeout = 300
            'MsgBox(msql)
            'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
            'trans.Begin()
            flx.Clear()
            Call flxhead()

            Try
                ''Dim DR As SqlDataReader
                Dim DR As SqlDataReader
                DR = CMD.ExecuteReader
                If DR.HasRows = True Then
                    'mihavday = DR.Item("avday")
                    With flx
                        While DR.Read
                            mihavday = DR.Item("avday")
                            .Rows = .Rows + 1
                            .Row = .Rows - 1

                            If Trim(DR.Item("subdept")) = "IRONING" Then
                                miron = DR.Item("totprod")
                            End If
                            .set_TextMatrix(.Row, 0, DR.Item("subdept"))
                            .set_TextMatrix(.Row, 1, DR.Item("costtype"))
                            .set_TextMatrix(.Row, 2, Format(DR.Item("tarket"), "#######0"))
                            .set_TextMatrix(.Row, 3, Format(DR.Item("totprsnt"), "####0"))
                            .set_TextMatrix(.Row, 4, Format(DR.Item("totexp"), "########0.00"))
                            .set_TextMatrix(.Row, 5, Format(DR.Item("totprod"), "########0"))
                            .set_TextMatrix(.Row, 6, Format(DR.Item("avgwage"), "####0.00"))
                            .set_TextMatrix(.Row, 7, Format(DR.Item("mqty"), "####0.00"))
                            If DR.Item("mqty") > 0 Then
                                .set_TextMatrix(.Row, 8, Format(DR.Item("totexp") / (DR.Item("totprod") + DR.Item("mqty")), "####0.00"))
                            Else
                                .set_TextMatrix(.Row, 8, 0)
                            End If

                            totlblexp.Text = Format(Val(totlblexp.Text) + Val(DR.Item("totexp")), "########0.00")

                            lbltotprsnt.Text = Format(Val(lbltotprsnt.Text) + Val(DR.Item("totprsnt")), "######0")

                        End While
                    End With
                    lbltotprod.Text = miron
                    lbltotwage.Text = Format(Val(totlblexp.Text) / miron, "####0.00")
                End If

                DR.Close()

            Catch sqlEx As sqlException  '
                MsgBox(sqlEx.Message)


            Catch ex As Exception
                'MsgBox("Check " & DR.Item("quality"))

                MsgBox(ex.Message)
                'MsgBox("Check " & DR.Item("quality"))
                'dr.close()
                flx.Clear()
                Call flxhead()
            End Try

            CMD.Dispose()
        End If


        msql = "select SUM(k.rgqty) rgqty,SUM(k.lsqty) lsqty from " & vbCrLf _
           & "(select CASE when LEFT(u_itemcode,2)<>'LS' then  SUM(U_AccpQty) else 0 end rgqty,CASE when LEFT(u_itemcode,2)='LS' then  SUM(U_AccpQty) else 0 end lsqty from [@INM_WIP1] where DocEntry in (select docentry from [@INM_OWIP] where U_DocDate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & " ' and U_DocDate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' and U_OperCode='IRONGD') and left(u_itemcode,3)<>'ACC' group by LEFT(u_itemcode,2)) k"

        Dim CMD2 As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        CMD2.CommandTimeout = 300
        'MsgBox(msql)
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()

        Try
            ''Dim DR As SqlDataReader
            Dim DR2 As SqlDataReader
            DR2 = CMD2.ExecuteReader
            If DR2.HasRows = True Then
                While DR2.Read
                    lblrgqty.Text = Format(DR2.Item("rgqty"), "###########0")
                    lbllsqty.Text = Format(DR2.Item("lsqty"), "###########0")

                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try




        Call loadexp()

    End Sub
    Private Sub loadexp()
        lblihtot.Text = 0
        lblexpcost.Text = 0
        If Val(Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "dd")) > 0 Then


            If chkratio.Checked = True Then
                msql2 = "select l.AcctCode,l.acctname,l.totexp," & vbCrLf _
               & " case when l.totexp>0 then round(l.totexp*(round(l.ih*100/l.totemp,3))/100,2) else 0 end ihtot," & vbCrLf _
               & " case when l.totexp>0 then round(l.totexp*(round(l.sc*100/l.totemp,3))/100,2) else 0 end sctot," & vbCrLf _
              & " case when l.totexp>0 then round(l.totexp*(round(l.wh*100/l.totemp,3))/100,2) else 0 end whtot, " & vbCrLf _
              & " round(l.ih*100/l.totemp,2) ihp,round(l.sc*100/l.totemp,2) scp, " & vbCrLf _
              & " round(l.wh*100/l.totemp,2) shp from  " & vbCrLf _
             & " (select k.AcctCode,k.acctname,k.totexp, convert(real,em.ih)  ih,convert(real,em.sc)  sc,convert(real,em.wh)  wh, " & vbCrLf _
             & " em.totemp totemp from  " & vbCrLf _
             & "(select b.AcctCode,b.acctname,SUM(c.Debit-Credit) as totexp from oact b " & vbCrLf _
             & " left join jdt1 c on c.Account=b.AcctCode " & vbCrLf _
             & "where b.GroupMask>=5 and convert(int, left(b.acctcode,4))>=5002 and convert(int, left(b.acctcode,4))<>5007 and b.Postable='Y' " & vbCrLf _
             & " and c.RefDate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & " ' and c.RefDate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' and b.AcctCode not in ('50020000011','50021000001','50021000002','50050906000','50051110000','50021000000','50021000001','50021000002','50050901001','50050901002','50050909000', '50050903000') " & vbCrLf _
             & " group by b.AcctCode,b.AcctName) k, " & vbCrLf _
             & " (select [IN HOUSE] as ih,[SC],[WH],([IN HOUSE]+[SC]+[WH]) totemp from  " & vbCrLf _
             & "   (select costtype,totno from (select costtype,  count(costtype) as totno from " & Trim(mcostdbnam) & ".dbo.Empmaster " & vbCrLf _
             & "   group by costtype  )s) k " & vbCrLf _
             & "    pivot " & vbCrLf _
             & "( " & vbCrLf _
            & "   sum(totno) for costtype in ([IN HOUSE],[SC],[WH])  ) p) em ) l  where l.totexp<>0  order by l.acctcode" & vbCrLf

            Else

                msql2 = "select lk.* from " & vbCrLf _
                & "(select l.AcctCode,l.acctname,l.totexp," & vbCrLf _
                & " case when oc.ratiowise='N' then  case when l.totexp>0 then round(l.totexp*oc.ih/100,2) end else " & vbCrLf _
                & " case when l.totexp>0 then round(l.totexp*(round(l.ih*100/l.totemp,3))/100,2) else 0 end end ihtot, " & vbCrLf _
                & " case when oc.ratiowise='N' then  case when l.totexp>0 then round(l.totexp*oc.SC/100,2) end else  " & vbCrLf _
                & " case when l.totexp>0 then round(l.totexp*(round(l.sc*100/l.totemp,3))/100,2) else 0 end end sctot, " & vbCrLf _
                & " case when oc.ratiowise='N' then  case when l.totexp>0 then round(l.totexp*oc.wh/100,2) end else " & vbCrLf _
                & "case when l.totexp>0 then round(l.totexp*(round(l.wh*100/l.totemp,3))/100,2) else 0 end end whtot, " & vbCrLf _
                & " round(l.ih*100/l.totemp,2) ihp,round(l.sc*100/l.totemp,2) scp, " & vbCrLf _
                & " round(l.wh*100/l.totemp,2) shp from  " & vbCrLf _
                & " (select k.AcctCode,k.acctname,k.totexp, convert(real,em.ih)  ih,convert(real,em.sc)  sc,convert(real,em.wh)  wh, " & vbCrLf _
                & " em.totemp totemp from  " & vbCrLf _
                & "(select b.AcctCode,b.acctname,SUM(c.Debit-Credit) as totexp from oact b " & vbCrLf _
                & " left join jdt1 c on c.Account=b.AcctCode " & vbCrLf _
                & "where b.GroupMask>=5 and convert(int, left(b.acctcode,4))>=5002 and convert(int, left(b.acctcode,4))<>5007 and b.Postable='Y' " & vbCrLf _
                & " and c.RefDate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & " ' and c.RefDate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' and b.AcctCode not in ('50020000011','50021000001','50021000002','50050906000','50051110000','50021000000','50021000001','50021000002','50050901001','50050901002','50050909000', '50050903000') " & vbCrLf _
                & " group by b.AcctCode,b.AcctName) k, " & vbCrLf _
                & " (select [IN HOUSE] as ih,[SC],[WH],([IN HOUSE]+[SC]+[WH]) totemp from  " & vbCrLf _
                & "   (select costtype,totno from (select costtype,  count(costtype) as totno from " & Trim(mcostdbnam) & ".dbo.Empmaster " & vbCrLf _
                & "   group by costtype  )s) k " & vbCrLf _
                & "    pivot " & vbCrLf _
                & "( " & vbCrLf _
                & "   sum(totno) for costtype in ([IN HOUSE],[SC],[WH])  ) p) em ) l  " & vbCrLf _
                & " left join " & Trim(mcostdbnam) & ".dbo.oactmast oc on oc.acctcode=l.acctcode where l.totexp<>0 " & vbCrLf _
                & " union all " & vbCrLf _
                & " select  '50051900000' acctcode,'DEPRECIATION' acctname,SUM(ll.deptotal) totexp, SUM(ll.ihextot) ihtot,SUM(ll.scextot) sctot,SUM(ll.whextot) whtot,0 ihp,0 scp,0 shp from  " & vbCrLf _
                & "(select k.acctname, k.deptotal, CASE when k.ratioper='N' then " & vbCrLf _
                & "  CASE when k.deptotal>0 then  round(k.deptotal*k.ih/100,2) end else " & vbCrLf _
                & " CASE when k.deptotal>0 then round(k.deptotal*jj.ihp/100,2) else 0 end end as ihextot, " & vbCrLf _
                & " CASE when k.ratioper='N' then " & vbCrLf _
                & " CASE when k.deptotal>0 then  round(k.deptotal*k.sc/100,2) end else " & vbCrLf _
                & " CASE when k.deptotal>0 then round(k.deptotal*jj.scp/100,2) else 0 end end as scextot, " & vbCrLf _
                & " CASE when k.ratioper='N' then " & vbCrLf _
                & " CASE when k.deptotal>0 then  round(k.deptotal*k.wh/100,2) end else " & vbCrLf _
                & " CASE when k.deptotal>0 then round(k.deptotal*jj.whp/100,2) else 0 end end as whextot from " & vbCrLf _
                & "(select c.AcctCode,c.AcctName,c.CurrTotal,b.depper,((c.CurrTotal*b.depper/100)/12)/(datediff(day, '" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "', dateadd(month, 1, '" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "')))*(select count(k.dd) from(select attdate as dd from " & Trim(mcostdbnam) & ".dbo.attlog where attdate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and attdate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' group by attdate) k) deptotal, b.ih,b.sc,b.wh,b.ratioper from oact c " & vbCrLf _
                & " inner join " & Trim(mcostdbnam) & ".dbo.depreciationmaster b on b.acctcode collate SQL_Latin1_General_CP850_CI_AS=c.AcctCode) k, " & vbCrLf _
                & "(select l.ih,l.sc,l.wh,l.totemp,CONVERT(real,l.ih)/CONVERT(real,l.totemp)*100 ihp,CONVERT(real,l.sc)/CONVERT(real,l.totemp)*100 scp,CONVERT(real,l.wh)/CONVERT(real,l.totemp)*100 whp from  " & vbCrLf _
                & " (select [IN HOUSE] as ih,[SC],[WH],([IN HOUSE]+[SC]+[WH]) totemp from  " & vbCrLf _
                & " (select costtype,totno from (select costtype,  count(costtype) as totno from " & Trim(mcostdbnam) & ".dbo.Empmaster  " & vbCrLf _
                & " group by costtype  )s) k  " & vbCrLf _
                & "pivot (" & vbCrLf _
                & "  sum(totno) for costtype in ([IN HOUSE],[SC],[WH])  ) p) l) jj) ll " & vbCrLf _
                & " union all " & vbCrLf _
                & "select '50052000000' acctcode, 'INTEREST ON CAPITAL' acctname,((SUM(Credit-Debit)*12/100)/12)/(datediff(day, '" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "', dateadd(month, 1, '" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'))-acccost.dbo.f_count_sundays(" & Format(CDate(mskdatefr.Text), "yyyy") & "," & Format(CDate(mskdatefr.Text), "MM") & "))*(select count(k.dd) from(select attdate as dd from " & Trim(mcostdbnam) & ".dbo.attlog where attdate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and attdate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' group by attdate) k) totexp," & vbCrLf _
                & " ((SUM(Credit-Debit)*12/100)/12)/(datediff(day, '" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "', dateadd(month, 1, '" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'))-acccost.dbo.f_count_sundays(" & Format(CDate(mskdatefr.Text), "yyyy") & "," & Format(CDate(mskdatefr.Text), "MM") & "))*(select count(k.dd) from(select attdate as dd from " & Trim(mcostdbnam) & ".dbo.attlog where attdate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and attdate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' group by attdate) k) as ihtot,0 sctot,0 whtot,0 ihp,0 scp,0 shp from jdt1 where refdate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' and account='30101000000')" & vbCrLf _
                & " lk" & vbCrLf _
                & " order by lk.acctcode"

            End If



            Dim CMD1 As New SqlCommand(msql2, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            CMD1.CommandTimeout = 300
            'MsgBox(msql)
            'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
            'trans.Begin()
            flxe.Clear()
            Call flxehead()

            Try
                ''Dim DR As SqlDataReader
                Dim DR1 As SqlDataReader
                DR1 = CMD1.ExecuteReader
                If DR1.HasRows = True Then
                    With flxe
                        While DR1.Read
                            .Rows = .Rows + 1
                            .Row = .Rows - 1

                            .set_TextMatrix(.Row, 0, DR1.Item("acctname"))
                            .set_TextMatrix(.Row, 1, Format(DR1.Item("totexp"), "##########0.00"))
                            If IsDBNull(DR1.Item("ihtot")) = False Then
                                .set_TextMatrix(.Row, 2, Format(DR1.Item("ihtot"), "##########0.00"))
                            Else
                                .set_TextMatrix(.Row, 2, 0)
                            End If
                            .set_TextMatrix(.Row, 3, Format(Val(lbltotprod.Text), "####0"))
                            If IsDBNull(DR1.Item("ihtot")) = False Then
                                .set_TextMatrix(.Row, 4, Format((DR1.Item("ihtot") / Val(lbltotprod.Text)), "########0.00"))
                            Else
                                .set_TextMatrix(.Row, 4, 0)
                            End If

                            If IsDBNull(DR1.Item("ihtot")) = False Then
                                lblihtot.Text = Format(Val(lblihtot.Text) + Val(DR1.Item("ihtot")), "##########0.00")
                            Else
                            End If

                            'lbltotprsnt.Text = Format(Val(lbltotprsnt.Text) + Val(DR.Item("ihtot")), "##########0.00")

                        End While
                    End With


                    lblexpcost.Text = Format((Val(lblihtot.Text) / Val(lbltotprod.Text)), "####0.00")
                End If

                DR1.Close()

            Catch sqlEx As sqlException  '
                MsgBox(sqlEx.Message)


            Catch ex As Exception
                'MsgBox("Check " & DR.Item("quality"))

                MsgBox(ex.Message)
                'MsgBox("Check " & DR.Item("quality"))
                'dr.close()
                flxe.Clear()
                Call flxehead()
            End Try

            CMD1.Dispose()
        End If



            lbltotcostpcs.Text = Format(Val(lblexpcost.Text) + Val(lbltotwage.Text), "###0.00")

        

    End Sub

   

    
    Private Sub getdelqty()
        msql = "select SUM(k.shqty) shqty,SUM(k.lsqty) lsqty from  " & vbCrLf _
          & "(select CASE when left(u_frmitemcd,2)='LS' then SUM(b.U_Qty) else 0 end lsqty, " & vbCrLf _
          & " CASE when left(u_frmitemcd,2)<>'LS' then SUM(b.U_Qty) else 0 end SHqty " & vbCrLf _
          & " from [@INS_ICC1] b " & vbCrLf _
          & "left join [@INS_OICC] c on c.DocEntry=b.docentry " & vbCrLf _
          & "where c.U_DocDate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and c.U_DocDate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' and left(convert(nvarchar(100),c.U_Remarks),2)<>'SC' and LEFT(b.u_frmitemcd,3)<>'ACC' " & vbCrLf _
          & " group by b.U_FrmItemCd) k " & vbCrLf

        'expens
        '        select l.AcctCode,l.acctname,l.totexp,
        'case when l.totexp>0 then round(l.totexp*(round(l.ih*100/l.totemp,3))/100,2) else 0 end ihtot,
        'case when l.totexp>0 then round(l.totexp*(round(l.sc*100/l.totemp,3))/100,2) else 0 end sctot,
        'case when l.totexp>0 then round(l.totexp*(round(l.wh*100/l.totemp,3))/100,2) else 0 end whtot,
        'round(l.ih*100/l.totemp,2) ihp,round(l.sc*100/l.totemp,2) scp,
        'round(l.wh*100/l.totemp,2) shp from 
        '(select k.AcctCode,k.acctname,k.totexp, convert(real,em.ih)  ih,convert(real,em.sc)  sc,convert(real,em.wh)  wh,
        'em.totemp totemp from 
        '(select b.AcctCode,b.acctname,SUM(c.Debit-Credit) as totexp from oact b
        'left join jdt1 c on c.Account=b.AcctCode
        'where b.GroupMask>=5 and convert(int, left(b.acctcode,4))>=5002 and convert(int, left(b.acctcode,4))<>5007 and b.Postable='Y'
        'and c.RefDate>='2017-12-01' and c.RefDate<='2017-12-15' and b.AcctCode not in ('50020000011','50021000001','50021000002','50050906000','50051110000')
        'group by b.AcctCode,b.AcctName) k,
        '(select [IN HOUSE] as ih,[SC],[WH],([IN HOUSE]+[SC]+[WH]) totemp from 
        '(select costtype,totno from 
        '(select costtype,  count(costtype) as totno from acccost.dbo.Empmaster
        ' group by costtype
        ' )s) k
        '                pivot()
        '(
        ' sum(totno) for costtype in ([IN HOUSE],[SC],[WH])
        ' ) p) em ) l

    End Sub

    Private Sub scload()

        Dim msql As String
        lbltotwages.Text = 0
        totlblexps.Text = 0
        lbltotprods.Text = 0
        lbltotprsnts.Text = 0
        If Val(Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "dd")) > 0 Then
            msql = "declare @d1 nvarchar(20) " & vbCrLf _
                & "declare @d2 nvarchar(20)  " & vbCrLf _
                & "set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'   " & vbCrLf _
                & " set @d2='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "'  " & vbCrLf _
               & "select k.costtype,sum(k.tarket) tarket,(sum(k.prsnt) /(select count(k.dd) from(select attdate as dd from " & Trim(mcostdbnam) & ".dbo.attlog where attdate>=@d1 and attdate<=@d2 group by attdate) k)) as totprsnt," & vbCrLf _
                & " isnull(sum(k.exps),0)+sum(k.epf)+sum(isnull(k.daybonus,0)) salry,isnull(sum(k.exps),0)+sum(k.epf)+sum(isnull(k.daybonus,0))+ISNULL(jb.prodamt,0) totexp,isnull(jb.accqty,0) totprod,case when (isnull(sum(k.exps),0)+sum(k.epf)+isnull(jb.prodamt,0))>0 then (SUM(k.exps)+sum(k.epf+isnull(k.daybonus,0))+isnull(jb.prodamt,0))/ISNULL(jb.accqty,0) else 0 end avgwage,sum(k.epf) epf from   " & vbCrLf _
               & "(select e.empid,e.empname,e.department,e.salary,e.costtype,e.[target] tarket,e.per,c.prsnt,datediff(day, @d1, dateadd(month, 1, @d1)) noday,case when isnull(e.sc,0)=100 then e.salary/datediff(day, @d1, dateadd(month, 1, @d1)) else (e.salary/datediff(day, @d1, dateadd(month, 1, @d1)))*e.sc/100  end daysalary," & vbCrLf _
               & " case when isnull(e.sc,0)=100 then c.prsnt*(e.salary/datediff(day, @d1, dateadd(month, 1, @d1))) else  c.prsnt* ((e.salary/datediff(day, @d1, dateadd(month, 1, @d1))))*e.sc/100 end exps,   " & vbCrLf _
               & " CASE when e.salary>=15000 and e.sc>e.ih then round((15000/datediff(day, @d1, dateadd(month, 1, @d1)))*12/100,2) else round(((e.salary/datediff(day, @d1, dateadd(month, 1, @d1))) *70/100)*12/100,2) end epf," & vbCrLf _
               & " case when isnull(e.sc,0)=100 then (e.salary/12)/datediff(day, @d1, dateadd(month, 1, @d1)) else (((e.salary*e.sc)/100) /12)/datediff(day, @d1, dateadd(month, 1, @d1)) end daybonus from " & Trim(mcostdbnam) & ".dbo.Empmaster e " & vbCrLf _
               & "left join " & Trim(mcostdbnam) & ".dbo.attlog c on c.empid=e.empid   " & vbCrLf _
               & " where c.attdate>=@d1 and c.attdate<=@d2 and e.costtype='SC' ) k   " & vbCrLf _
               & " left join   " & vbCrLf _
               & "(select 'SC' sc, SUM(b.U_AccpQty) accqty,SUM(b.U_LabCost) prodamt from [@insc_pdn1] b  " & vbCrLf _
               & " left join [@INSC_oPDN] c on c.DocEntry=b.DocEntry  " & vbCrLf _
               & " where c.U_DocDate>= @d1 and c.u_docdate<=@d2 and LEFT(b.U_ItemCode,3)<>'ACC') jb on jb.sc=k.costtype  " & vbCrLf _
               & " group by k.costtype,jb.accqty,jb.prodamt  " & vbCrLf

            miron = 0
            Dim CMD As New SqlCommand(msql, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            CMD.CommandTimeout = 300
            'MsgBox(msql)
            'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
            'trans.Begin()
            flxs.Clear()
            Call flxshead()

            Try
                ''Dim DR As SqlDataReader
                Dim DR As SqlDataReader
                DR = CMD.ExecuteReader
                If DR.HasRows = True Then
                    With flxs
                        While DR.Read
                            .Rows = .Rows + 1
                            .Row = .Rows - 1


                            miron = DR.Item("totprod")

                            .set_TextMatrix(.Row, 0, DR.Item("Costtype"))
                            .set_TextMatrix(.Row, 1, Format(DR.Item("tarket"), "#######0"))
                            .set_TextMatrix(.Row, 2, Format(DR.Item("totprsnt"), "####0"))
                            .set_TextMatrix(.Row, 3, Format(DR.Item("salry"), "#######0"))
                            .set_TextMatrix(.Row, 4, Format(DR.Item("totexp"), "########0.00"))
                            .set_TextMatrix(.Row, 5, Format(DR.Item("totprod"), "########0"))
                            .set_TextMatrix(.Row, 6, Format(DR.Item("avgwage"), "####0.00"))


                            totlblexps.Text = Format(Val(totlblexps.Text) + Val(DR.Item("totexp")), "########0.00")

                            lbltotprsnts.Text = Format(Val(lbltotprsnts.Text) + Val(DR.Item("totprsnt")), "#####0")

                        End While
                    End With
                    lbltotprods.Text = miron
                    lbltotwages.Text = Format(Val(totlblexps.Text) / miron, "####0.00")
                End If

                DR.Close()

            Catch sqlEx As sqlException  '
                MsgBox(sqlEx.Message)


            Catch ex As Exception
                'MsgBox("Check " & DR.Item("quality"))

                MsgBox(ex.Message)
                'MsgBox("Check " & DR.Item("quality"))
                'dr.close()
                flx.Clear()
                Call flxhead()
            End Try

            CMD.Dispose()
        End If

        Call scloadexp()
    End Sub

    Private Sub cmddisp_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.ClickButtonArea
        Call loaddata()
        Call scload()
    End Sub

    Private Sub cmdexport_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdexport.ClickButtonArea
        'excelexport(flx)
        excelexportrep(flx, flxe, flxs, flxse)
    End Sub

    Private Sub flxshead()
        flxs.Rows = 1
        flxs.Cols = 7
        flxs.set_ColWidth(0, 1900)
        flxs.set_ColWidth(1, 1200)
        flxs.set_ColWidth(2, 1200)
        flxs.set_ColWidth(3, 1500)
        flxs.set_ColWidth(4, 1500)
        flxs.set_ColWidth(5, 1200)
        flxs.set_ColWidth(6, 1000)
        'flx.set_ColWidth(7, 1000)
        'flx.set_ColWidth(8, 1000)
        'flx.set_ColWidth(9, 1000)

        flxs.Row = 0
        flxs.Col = 0
        flxs.CellAlignment = 3
        flxs.CellFontBold = True
        flxs.set_TextMatrix(0, 0, "Department")

        
        flxs.Col = 1
        flxs.CellAlignment = 3
        flxs.CellFontBold = True
        flxs.set_TextMatrix(0, 1, "Target")

        flxs.Col = 2
        flxs.CellAlignment = 3
        flxs.CellFontBold = True
        flxs.set_TextMatrix(0, 2, "Tot Present")

        flxs.Col = 3
        flxs.CellAlignment = 3
        flxs.CellFontBold = True
        flxs.set_TextMatrix(0, 3, "Salary")


        flxs.Col = 4
        flxs.CellAlignment = 3
        flxs.CellFontBold = True
        flxs.set_TextMatrix(0, 4, "Tot Wages")

        flxs.Col = 5
        flxs.CellAlignment = 3
        flxs.CellFontBold = True
        flxs.set_TextMatrix(0, 5, "Tot.Prod")

        flxs.Col = 6
        flxs.CellAlignment = 3
        flxs.CellFontBold = True
        flxs.set_TextMatrix(0, 6, "Avg.Wages")

        'flx.set_ColAlignment(0, 2)
        'flxs.Rows = flxs.Rows + 1
        'flxs.Row = flxs.Rows - 1
    End Sub

    Private Sub flxsehead()
        flxse.Rows = 1
        flxse.Cols = 5
        flxse.set_ColWidth(0, 3500)
        flxse.set_ColWidth(1, 1400)
        flxse.set_ColWidth(2, 1200)
        flxse.set_ColWidth(3, 1500)
        flxse.set_ColWidth(4, 1500)
        'flx.set_ColWidth(5, 1200)
        'flx.set_ColWidth(6, 1000)
        'flx.set_ColWidth(7, 1000)
        'flx.set_ColWidth(8, 1000)
        'flx.set_ColWidth(9, 1000)

        flxse.Row = 0
        flxse.Col = 0
        flxse.CellAlignment = 3
        flxse.CellFontBold = True
        flxse.set_TextMatrix(0, 0, "Acct Name")

        flxse.Col = 1
        flxse.CellAlignment = 3
        flxse.CellFontBold = True
        flxse.set_TextMatrix(0, 1, "Tot.Expenses")

        flxse.Col = 2
        flxse.CellAlignment = 3
        flxse.CellFontBold = True
        flxse.set_TextMatrix(0, 2, "SC Exp.")

        flxse.Col = 3
        flxse.CellAlignment = 3
        flxse.CellFontBold = True
        flxse.set_TextMatrix(0, 3, "Tot Prod")

        flxse.Col = 4
        flxse.CellAlignment = 3
        flxse.CellFontBold = True
        flxse.set_TextMatrix(0, 4, "Avg Exp")

        ''flx.set_ColAlignment(0, 2)
        'flxe.Rows = flx.Rows + 1
        'flxe.Row = flx.Rows - 1
    End Sub

    Private Sub cmdscdisp_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdscdisp.ClickButtonArea
        Call scload()
    End Sub

    Private Sub scloadexp()
        lblihtots.Text = 0
        lblexpcosts.Text = 0
        If Val(Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "dd")) > 0 Then


            If chkratio.Checked = True Then
                msql2 = "select l.AcctCode,l.acctname,l.totexp," & vbCrLf _
               & " case when l.totexp>0 then round(l.totexp*(round(l.ih*100/l.totemp,3))/100,2) else 0 end ihtot," & vbCrLf _
               & " case when l.totexp>0 then round(l.totexp*(round(l.sc*100/l.totemp,3))/100,2) else 0 end sctot," & vbCrLf _
              & " case when l.totexp>0 then round(l.totexp*(round(l.wh*100/l.totemp,3))/100,2) else 0 end whtot, " & vbCrLf _
              & " round(l.ih*100/l.totemp,2) ihp,round(l.sc*100/l.totemp,2) scp, " & vbCrLf _
              & " round(l.wh*100/l.totemp,2) shp from  " & vbCrLf _
             & " (select k.AcctCode,k.acctname,k.totexp, convert(real,em.ih)  ih,convert(real,em.sc)  sc,convert(real,em.wh)  wh, " & vbCrLf _
             & " em.totemp totemp from  " & vbCrLf _
             & "(select b.AcctCode,b.acctname,SUM(c.Debit-Credit) as totexp from oact b " & vbCrLf _
             & " left join jdt1 c on c.Account=b.AcctCode " & vbCrLf _
             & "where b.GroupMask>=5 and convert(int, left(b.acctcode,4))>=5002 and convert(int, left(b.acctcode,4))<>5007 and b.Postable='Y' " & vbCrLf _
             & " and c.RefDate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & " ' and c.RefDate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' and b.AcctCode not in ('50020000011','50021000001','50021000002','50050906000','50051110000','50021000000','50021000001','50021000002','50050901001','50050901002','50050909000', '50050903000') " & vbCrLf _
             & " group by b.AcctCode,b.AcctName) k, " & vbCrLf _
             & " (select [IN HOUSE] as ih,[SC],[WH],([IN HOUSE]+[SC]+[WH]) totemp from  " & vbCrLf _
             & "   (select costtype,totno from (select costtype,  count(costtype) as totno from " & Trim(mcostdbnam) & ".dbo.Empmaster " & vbCrLf _
             & "   group by costtype  )s) k " & vbCrLf _
             & "    pivot " & vbCrLf _
             & "( " & vbCrLf _
            & "   sum(totno) for costtype in ([IN HOUSE],[SC],[WH])  ) p) em ) l  where l.totexp<>0 order by l.acctcode" & vbCrLf

            Else
                msql2 = "select lk.* from (select l.AcctCode,l.acctname,l.totexp," & vbCrLf _
               & " case when oc.ratiowise='N' then  case when l.totexp>0 then round(l.totexp*oc.ih/100,2) end else " & vbCrLf _
               & " case when l.totexp>0 then round(l.totexp*(round(l.ih*100/l.totemp,3))/100,2) else 0 end end ihtot, " & vbCrLf _
               & " case when oc.ratiowise='N' then  case when l.totexp>0 then round(l.totexp*oc.SC/100,2) end else  " & vbCrLf _
               & " case when l.totexp>0 then round(l.totexp*(round(l.sc*100/l.totemp,3))/100,2) else 0 end end sctot, " & vbCrLf _
               & " case when oc.ratiowise='N' then  case when l.totexp>0 then round(l.totexp*oc.wh/100,2) end else " & vbCrLf _
               & "case when l.totexp>0 then round(l.totexp*(round(l.wh*100/l.totemp,3))/100,2) else 0 end end whtot, " & vbCrLf _
               & " round(l.ih*100/l.totemp,2) ihp,round(l.sc*100/l.totemp,2) scp, " & vbCrLf _
               & " round(l.wh*100/l.totemp,2) shp from  " & vbCrLf _
               & " (select k.AcctCode,k.acctname,k.totexp, convert(real,em.ih)  ih,convert(real,em.sc)  sc,convert(real,em.wh)  wh, " & vbCrLf _
               & " em.totemp totemp from  " & vbCrLf _
               & "(select b.AcctCode,b.acctname,SUM(c.Debit-Credit) as totexp from oact b " & vbCrLf _
               & " left join jdt1 c on c.Account=b.AcctCode " & vbCrLf _
               & "where b.GroupMask>=5 and convert(int, left(b.acctcode,4))>=5002 and convert(int, left(b.acctcode,4))<>5007 and b.Postable='Y' " & vbCrLf _
               & " and c.RefDate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & " ' and c.RefDate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' and b.AcctCode not in ('50020000011','50021000001','50021000002','50050906000','50051110000','50021000000','50021000001','50021000002','50050901001','50050901002','50050909000', '50050903000') " & vbCrLf _
               & " group by b.AcctCode,b.AcctName) k, " & vbCrLf _
               & " (select [IN HOUSE] as ih,[SC],[WH],([IN HOUSE]+[SC]+[WH]) totemp from  " & vbCrLf _
               & "   (select costtype,totno from (select costtype,  count(costtype) as totno from " & Trim(mcostdbnam) & ".dbo.Empmaster " & vbCrLf _
               & "   group by costtype  )s) k " & vbCrLf _
               & "    pivot " & vbCrLf _
               & "( " & vbCrLf _
               & "   sum(totno) for costtype in ([IN HOUSE],[SC],[WH])  ) p) em ) l  " & vbCrLf _
               & " left join " & Trim(mcostdbnam) & ".dbo.oactmast oc on oc.acctcode=l.acctcode where l.totexp<>0 " & vbCrLf _
                & " union all " & vbCrLf _
                & " select  '50051900000' acctcode,'DEPRECIATION' acctname,SUM(ll.deptotal) totexp, SUM(ll.ihextot) ihtot,SUM(ll.scextot) sctot,SUM(ll.whextot) whtot,0 ihp,0 scp,0 shp from  " & vbCrLf _
                & "(select k.acctname, k.deptotal, CASE when k.ratioper='N' then " & vbCrLf _
                & "  CASE when k.deptotal>0 then  round(k.deptotal*k.ih/100,2) end else " & vbCrLf _
                & " CASE when k.deptotal>0 then round(k.deptotal*jj.ihp/100,2) else 0 end end as ihextot, " & vbCrLf _
                & " CASE when k.ratioper='N' then " & vbCrLf _
                & " CASE when k.deptotal>0 then  round(k.deptotal*k.sc/100,2) end else " & vbCrLf _
                & " CASE when k.deptotal>0 then round(k.deptotal*jj.scp/100,2) else 0 end end as scextot, " & vbCrLf _
                & " CASE when k.ratioper='N' then " & vbCrLf _
                & " CASE when k.deptotal>0 then  round(k.deptotal*k.wh/100,2) end else " & vbCrLf _
                & " CASE when k.deptotal>0 then round(k.deptotal*jj.whp/100,2) else 0 end end as whextot from " & vbCrLf _
                & "(select c.AcctCode,c.AcctName,c.CurrTotal,b.depper,((c.CurrTotal*b.depper/100)/12)/(datediff(day, '" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "', dateadd(month, 1, '" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "')))*(select count(k.dd) from(select attdate as dd from " & Trim(mcostdbnam) & ".dbo.attlog where attdate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and attdate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' group by attdate) k) deptotal, b.ih,b.sc,b.wh,b.ratioper from oact c " & vbCrLf _
                & " inner join " & Trim(mcostdbnam) & ".dbo.depreciationmaster b on b.acctcode collate SQL_Latin1_General_CP850_CI_AS=c.AcctCode) k, " & vbCrLf _
                & "(select l.ih,l.sc,l.wh,l.totemp,CONVERT(real,l.ih)/CONVERT(real,l.totemp)*100 ihp,CONVERT(real,l.sc)/CONVERT(real,l.totemp)*100 scp,CONVERT(real,l.wh)/CONVERT(real,l.totemp)*100 whp from  " & vbCrLf _
                & " (select [IN HOUSE] as ih,[SC],[WH],([IN HOUSE]+[SC]+[WH]) totemp from  " & vbCrLf _
                & " (select costtype,totno from (select costtype,  count(costtype) as totno from " & Trim(mcostdbnam) & ".dbo.Empmaster  " & vbCrLf _
                & " group by costtype  )s) k  " & vbCrLf _
                & "pivot (" & vbCrLf _
                & "  sum(totno) for costtype in ([IN HOUSE],[SC],[WH])  ) p) l) jj) ll) lk " & vbCrLf _
                & " order by lk.acctcode"

            End If



            Dim CMD1 As New SqlCommand(msql2, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            CMD1.CommandTimeout = 300
            'MsgBox(msql)
            'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
            'trans.Begin()
            flxse.Clear()
            Call flxsehead()

            Try
                ''Dim DR As SqlDataReader
                Dim DR1 As SqlDataReader
                DR1 = CMD1.ExecuteReader
                If DR1.HasRows = True Then
                    With flxse
                        While DR1.Read
                            .Rows = .Rows + 1
                            .Row = .Rows - 1
                            If DR1.Item("acctcode") = "50020000011" Then
                                lblstexp.Text = Format(Val(DR1.Item("totexp")), "##########0.00")
                            Else
                                .set_TextMatrix(.Row, 0, DR1.Item("acctname"))
                                .set_TextMatrix(.Row, 1, Format(DR1.Item("totexp"), "##########0.00"))
                                If IsDBNull(DR1.Item("sctot")) = False Then
                                    .set_TextMatrix(.Row, 2, Format(DR1.Item("sctot"), "##########0.00"))
                                Else
                                    .set_TextMatrix(.Row, 2, 0)
                                End If
                                .set_TextMatrix(.Row, 3, Format(Val(lbltotprods.Text), "####0"))
                                If IsDBNull(DR1.Item("sctot")) = False Then
                                    .set_TextMatrix(.Row, 4, Format((DR1.Item("sctot") / Val(lbltotprods.Text)), "########0.00"))
                                Else
                                    .set_TextMatrix(.Row, 4, 0)
                                End If


                                If IsDBNull(DR1.Item("sctot")) = False Then
                                    lblihtots.Text = Format(Val(lblihtots.Text) + Val(DR1.Item("sctot")), "##########0.00")
                                Else
                                    lblihtots.Text = 0
                                End If
                                'lbltotprsnt.Text = Format(Val(lbltotprsnt.Text) + Val(DR.Item("ihtot")), "##########0.00")

                            End If
                        End While
                    End With


                    lblexpcosts.Text = Format((Val(lblihtots.Text) / Val(lbltotprods.Text)), "####0.00")
                End If

                DR1.Close()

            Catch sqlEx As sqlException  '
                MsgBox(sqlEx.Message)


            Catch ex As Exception
                'MsgBox("Check " & DR.Item("quality"))

                MsgBox(ex.Message)
                'MsgBox("Check " & DR.Item("quality"))
                'dr.close()
                flxse.Clear()
                Call flxsehead()
            End Try

            CMD1.Dispose()
        End If

            lbltotcostsc.Text = Format(Val(lbltotwages.Text) + Val(lblexpcosts.Text), "####0.00")

    End Sub

    Private Sub tabpg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabpg.Click

    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub excelexportrep(ByVal CTRL As AxMSFlexGrid, ByVal ctrl2 As AxMSFlexGrid, ByVal sCTRL As AxMSFlexGrid, ByVal sctrl2 As AxMSFlexGrid)


        Dim ldir, lmdir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\barcodadd.txt"

        ldir = System.AppDomain.CurrentDomain.BaseDirectory()
        lmdir = Trim(ldir) & "costDET.xls"

        Dim ticks As Integer = Environment.TickCount
        ' Create the workbook
        Dim book As Workbook = New Workbook
        ' Set the author
        book.Properties.Author = "CarlosAg"

        ' Add some style
        Dim style As WorksheetStyle = book.Styles.Add("style1")
        style.Font.Bold = True
        style.Font.Size = 10
        style.Alignment.Vertical = StyleVerticalAlignment.Center


        Dim style2 As WorksheetStyle = book.Styles.Add("style2")
        style2.Font.Bold = True
        style2.Font.Size = 10
        style2.Alignment.Vertical = StyleVerticalAlignment.Center

        Dim style22 As WorksheetStyle = book.Styles.Add("style22")
        style22.Font.Bold = True
        style22.Font.Size = 10
        style22.Alignment.Horizontal = StyleHorizontalAlignment.Center
        'style22.Alignment.Vertical = StyleVerticalAlignment.Center

        Dim style3 As WorksheetStyle = book.Styles.Add("style3")
        style3.Font.Bold = True
        style3.Font.Size = 12
        style3.Alignment.Horizontal = StyleHorizontalAlignment.Center
        'style3.Alignment.Vertical = StyleVerticalAlignment.Center

        Dim sheet As Worksheet = book.Worksheets.Add("In House Cost Sheet")
        'Dim style2 As WorksheetStyle = book.Styles.Add("style2")

        Dim Rw1 As WorksheetRow = sheet.Table.Rows.Add
        'Rw1.Cells.Add("", DataType.String, "style1")
        'Rw1.Cells.Add("", DataType.String, "style1")
        'Rw1.Cells.Add("ATITHYA CLOTHING COMPANY", DataType.String, "style3")
        Dim cell As WorksheetCell = Rw1.Cells.Add("ATITHYA CLOTHING COMPANY", DataType.String, "style3")
        cell.MergeAcross = 9  '         // Merge two cells to


        Dim Rw2 As WorksheetRow = sheet.Table.Rows.Add
        'Rw2.Cells.Add("", DataType.String, "style1")
        'Rw2.Cells.Add("", DataType.String, "style1")
        Dim cell1 As WorksheetCell = Rw2.Cells.Add("IN HOUSE COST SHEET From " & Format(CDate(mskdatefr.Text), "dd-MM-yyyy") & " To " & Format(CDate(mskdateto.Text), "dd-MM-yyyy"), DataType.String, "style22")
        cell1.MergeAcross = 9

        Dim Rw3 As WorksheetRow = sheet.Table.Rows.Add
        'style2.Font.Bold = False


        For i = 0 To CTRL.Rows - 1

            Dim Row0 As WorksheetRow = sheet.Table.Rows.Add

            For j = 0 To CTRL.Cols - 1

                ' Add a cell
                'Row0.Cells.Add("Hello World", DataType.String, "style1")
                If j > 1 Then
                    If i = 0 Then
                        Row0.Cells.Add(CTRL.get_TextMatrix(i, j), DataType.String, "style2")
                    Else
                        Row0.Cells.Add(Convert.ToDecimal(CTRL.get_TextMatrix(i, j)), DataType.Number, "style1")

                    End If

                Else
                    If i = 0 Then
                        Row0.Cells.Add(CTRL.get_TextMatrix(i, j), DataType.String, "style2")
                    Else
                        Row0.Cells.Add(CTRL.get_TextMatrix(i, j), DataType.String, "style1")
                    End If

                End If
            Next j
            If i = 0 Then
                'Dim style As WorksheetStyle = book.Styles.Add("style1")
                style.Font.Bold = True
            Else
                'style = book.Styles.Add("style1")
                'style = book.Styles.Add("style1")
                style.Font.Bold = False
                style.Font.Size = 10
                style.Alignment.Vertical = StyleVerticalAlignment.Top
                'style.Alignment.Horizontal = StyleHorizontalAlignment.Justify
            End If

        Next i

        Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
        Row2.Cells.Add("", DataType.String, "style1")
        Row2.Cells.Add("", DataType.String, "style1")
        Row2.Cells.Add("Total", DataType.String, "style2")
        Row2.Cells.Add(Format(Convert.ToDecimal(lbltotprsnt.Text), "####0"), DataType.Number, "style2")
        Row2.Cells.Add(Format(Convert.ToDecimal(totlblexp.Text), "##########0.00"), DataType.Number, "style2")
        Row2.Cells.Add(Format(Convert.ToDecimal(lbltotprod.Text), "#########0"), DataType.Number, "style2")
        Row2.Cells.Add(Convert.ToDecimal(lbltotwage.Text), DataType.Number, "style2")

        Dim Row3 As WorksheetRow = sheet.Table.Rows.Add
        Row3.Cells.Add("", DataType.String, "style1")
        Row3.Cells.Add("", DataType.String, "style1")
        Row3.Cells.Add("", DataType.String, "style1")
        Row3.Cells.Add("", DataType.String, "style1")
        Row3.Cells.Add("Regular Shirt", DataType.String, "style2")
        Row3.Cells.Add(Format(Convert.ToDecimal(lblrgqty.Text), "########0"), DataType.Number, "style2")
        Dim Row1 As WorksheetRow = sheet.Table.Rows.Add
        Row1.Cells.Add("", DataType.String, "style1")
        Row1.Cells.Add("", DataType.String, "style1")
        Row1.Cells.Add("", DataType.String, "style1")
        Row1.Cells.Add("", DataType.String, "style1")
        Row1.Cells.Add("LS Shirt", DataType.String, "style2")
        Row1.Cells.Add(Format(Convert.ToDecimal(lbllsqty.Text), "########0"), DataType.Number, "style2")

        Dim Row7 As WorksheetRow = sheet.Table.Rows.Add
        Row7.Cells.Add("", DataType.String, "style1")
        Row7.Cells.Add("", DataType.String, "style1")
        Row7.Cells.Add("", DataType.String, "style1")
        Row7.Cells.Add("", DataType.String, "style1")
        Row7.Cells.Add("", DataType.String, "style1")
        Row7.Cells.Add("", DataType.String, "style1")
        Row7.Cells.Add("", DataType.String, "style1")
        Row7.Cells.Add("", DataType.String, "style1")
        Row7.Cells.Add(Label7.Text, DataType.String, "style3")
        Row7.Cells.Add(Format(Convert.ToDecimal(lbltotcostpcs.Text), "########0.00"), DataType.Number, "style3")
        Dim Row8 As WorksheetRow = sheet.Table.Rows.Add
        '***


        For k = 0 To ctrl2.Rows - 1

            Dim Row4 As WorksheetRow = sheet.Table.Rows.Add

            For jk = 0 To ctrl2.Cols - 1

                ' Add a cell
                'Row0.Cells.Add("Hello World", DataType.String, "style1")
                If jk > 0 Then
                    If k = 0 Then
                        Row4.Cells.Add(ctrl2.get_TextMatrix(k, jk), DataType.String, "style2")
                    Else
                        Row4.Cells.Add(Convert.ToDecimal(ctrl2.get_TextMatrix(k, jk)), DataType.Number, "style1")
                    End If

                Else
                    If k = 0 Then
                        Row4.Cells.Add(ctrl2.get_TextMatrix(k, jk), DataType.String, "style2")
                    Else
                        Row4.Cells.Add(ctrl2.get_TextMatrix(k, jk), DataType.String, "style1")
                    End If

                End If

            Next jk
            If k = 0 Then
                'Dim style As WorksheetStyle = book.Styles.Add("style1")
                style.Font.Bold = True
            Else
                'style = book.Styles.Add("style1")
                'style = book.Styles.Add("style1")
                style.Font.Bold = False
                style.Font.Size = 10
                style.Alignment.Vertical = StyleVerticalAlignment.Top
                'style.Alignment.Horizontal = StyleHorizontalAlignment.Justify
            End If


        Next k

        Dim Row5 As WorksheetRow = sheet.Table.Rows.Add

        Dim Row6 As WorksheetRow = sheet.Table.Rows.Add
        Row6.Cells.Add("", DataType.String, "style1")
        Row6.Cells.Add("Total", DataType.String, "style2")
        Row6.Cells.Add(Format(Convert.ToDecimal(lblihtot.Text), "##########0.00"), DataType.Number, "style2")
        Row6.Cells.Add(Convert.ToDecimal(lblexpcost.Text), DataType.Number, "style2")


        '***sheet 2



        Dim sheet1 As Worksheet = book.Worksheets.Add("SubContract Cost Sheet")
        'Dim style2 As WorksheetStyle = book.Styles.Add("style2")

        Dim sRw1 As WorksheetRow = sheet1.Table.Rows.Add
        'Rw1.Cells.Add("", DataType.String, "style1")
        'Rw1.Cells.Add("", DataType.String, "style1")
        'Rw1.Cells.Add("ATITHYA CLOTHING COMPANY", DataType.String, "style3")
        Dim scell As WorksheetCell = sRw1.Cells.Add("ATITHYA CLOTHING COMPANY", DataType.String, "style3")
        scell.MergeAcross = 6  '         // Merge two cells to


        Dim sRw2 As WorksheetRow = sheet1.Table.Rows.Add
        'Rw2.Cells.Add("", DataType.String, "style1")
        'Rw2.Cells.Add("", DataType.String, "style1")
        Dim scell1 As WorksheetCell = sRw2.Cells.Add("Sub Contract COST SHEET From " & Format(CDate(mskdatefr.Text), "dd-MM-yyyy") & " To " & Format(CDate(mskdateto.Text), "dd-MM-yyyy"), DataType.String, "style22")
        scell1.MergeAcross = 6

        Dim sRw3 As WorksheetRow = sheet1.Table.Rows.Add
        'style2.Font.Bold = False


        For i = 0 To sCTRL.Rows - 1

            Dim sRow0 As WorksheetRow = sheet1.Table.Rows.Add

            For j = 0 To sCTRL.Cols - 1

                ' Add a cell
                'Row0.Cells.Add("Hello World", DataType.String, "style1")
                If j > 1 Then
                    If i = 0 Then
                        sRow0.Cells.Add(sCTRL.get_TextMatrix(i, j), DataType.String, "style2")
                    Else
                        sRow0.Cells.Add(Convert.ToDecimal(sCTRL.get_TextMatrix(i, j)), DataType.Number, "style1")

                    End If

                Else
                    If i = 0 Then
                        sRow0.Cells.Add(sCTRL.get_TextMatrix(i, j), DataType.String, "style2")
                    Else
                        sRow0.Cells.Add(sCTRL.get_TextMatrix(i, j), DataType.String, "style1")
                    End If

                End If
            Next j
            If i = 0 Then
                'Dim style As WorksheetStyle = book.Styles.Add("style1")
                style.Font.Bold = True
            Else
                'style = book.Styles.Add("style1")
                'style = book.Styles.Add("style1")
                style.Font.Bold = False
                style.Font.Size = 10
                style.Alignment.Vertical = StyleVerticalAlignment.Top
                'style.Alignment.Horizontal = StyleHorizontalAlignment.Justify
            End If

        Next i

        Dim sRow2 As WorksheetRow = sheet1.Table.Rows.Add
        sRow2.Cells.Add("", DataType.String, "style1")
        sRow2.Cells.Add("", DataType.String, "style1")
        sRow2.Cells.Add("Total", DataType.String, "style2")
        sRow2.Cells.Add(Format(Convert.ToDecimal(lbltotprsnts.Text), "####0"), DataType.Number, "style2")
        sRow2.Cells.Add(Format(Convert.ToDecimal(totlblexps.Text), "##########0.00"), DataType.Number, "style2")
        sRow2.Cells.Add(Format(Convert.ToDecimal(lbltotprods.Text), "#########0"), DataType.Number, "style2")
        sRow2.Cells.Add(Convert.ToDecimal(lbltotwages.Text), DataType.Number, "style2")

        'Dim Row3 As WorksheetRow = sheet.Table.Rows.Add
        'Row3.Cells.Add("", DataType.String, "style1")
        'Row3.Cells.Add("", DataType.String, "style1")
        'Row3.Cells.Add("", DataType.String, "style1")
        'Row3.Cells.Add("", DataType.String, "style1")
        'Row3.Cells.Add("", DataType.String, "style1")
        'Row3.Cells.Add("Regular Shirt", DataType.String, "style2")
        'Row3.Cells.Add(Format(Convert.ToDecimal(lblrgqty.Text), "########0"), DataType.Number, "style2")
        'Dim Row1 As WorksheetRow = sheet.Table.Rows.Add
        'Row1.Cells.Add("", DataType.String, "style1")
        'Row1.Cells.Add("", DataType.String, "style1")
        'Row1.Cells.Add("", DataType.String, "style1")
        'Row1.Cells.Add("", DataType.String, "style1")
        'Row1.Cells.Add("", DataType.String, "style1")
        'Row1.Cells.Add("LS Shirt", DataType.String, "style2")
        'Row1.Cells.Add(Format(Convert.ToDecimal(lbllsqty.Text), "########0"), DataType.Number, "style2")

        Dim sRow7 As WorksheetRow = sheet1.Table.Rows.Add
        sRow7.Cells.Add("", DataType.String, "style1")
        sRow7.Cells.Add("", DataType.String, "style1")
        sRow7.Cells.Add("", DataType.String, "style1")
        sRow7.Cells.Add("", DataType.String, "style1")
        sRow7.Cells.Add("", DataType.String, "style1")
        sRow7.Cells.Add("", DataType.String, "style1")
        sRow7.Cells.Add("", DataType.String, "style1")
        sRow7.Cells.Add(Label6.Text, DataType.String, "style3")
        sRow7.Cells.Add(Format(Convert.ToDecimal(lbltotcostsc.Text), "########0.00"), DataType.Number, "style3")
        Dim sRow8 As WorksheetRow = sheet1.Table.Rows.Add
        '***


        For k = 0 To sctrl2.Rows - 1

            Dim sRow4 As WorksheetRow = sheet1.Table.Rows.Add

            For jk = 0 To sctrl2.Cols - 1

                ' Add a cell
                'Row0.Cells.Add("Hello World", DataType.String, "style1")
                If jk > 0 Then
                    If k = 0 Then
                        sRow4.Cells.Add(sctrl2.get_TextMatrix(k, jk), DataType.String, "style2")
                    Else
                        sRow4.Cells.Add(Convert.ToDecimal(sctrl2.get_TextMatrix(k, jk)), DataType.Number, "style1")
                    End If

                Else
                    If k = 0 Then
                        sRow4.Cells.Add(sctrl2.get_TextMatrix(k, jk), DataType.String, "style2")
                    Else
                        sRow4.Cells.Add(sctrl2.get_TextMatrix(k, jk), DataType.String, "style1")
                    End If

                End If

            Next jk
            If k = 0 Then
                'Dim style As WorksheetStyle = book.Styles.Add("style1")
                style.Font.Bold = True
            Else
                'style = book.Styles.Add("style1")
                'style = book.Styles.Add("style1")
                style.Font.Bold = False
                style.Font.Size = 10
                style.Alignment.Vertical = StyleVerticalAlignment.Top
                'style.Alignment.Horizontal = StyleHorizontalAlignment.Justify
            End If


        Next k

        Dim sRow5 As WorksheetRow = sheet1.Table.Rows.Add

        Dim sRow6 As WorksheetRow = sheet1.Table.Rows.Add
        sRow6.Cells.Add("", DataType.String, "style1")
        sRow6.Cells.Add("Total", DataType.String, "style2")
        sRow6.Cells.Add(Format(Convert.ToDecimal(lblihtots.Text), "##########0.00"), DataType.Number, "style2")
        sRow6.Cells.Add(Convert.ToDecimal(lblexpcosts.Text), DataType.Number, "style2")








        '*****************


        ' Save it
        'book.Save("c:\test.xls")
        book.Save(lmdir)
        'open file
        Process.Start(lmdir)
        'Console.WriteLine("Time:{0}", (Environment.TickCount - ticks))
    End Sub
    'Private Sub scexcel(ByVal sCTRL As AxMSFlexGrid, ByVal sctrl2 As AxMSFlexGrid)


    '    Dim sheet1 As Worksheet = book.Worksheets.Add("SubContract Cost Sheet")
    '    'Dim style2 As WorksheetStyle = book.Styles.Add("style2")

    '    Dim sRw1 As WorksheetRow = sheet1.Table.Rows.Add
    '    'Rw1.Cells.Add("", DataType.String, "style1")
    '    'Rw1.Cells.Add("", DataType.String, "style1")
    '    'Rw1.Cells.Add("ATITHYA CLOTHING COMPANY", DataType.String, "style3")
    '    Dim scell As WorksheetCell = sRw1.Cells.Add("ATITHYA CLOTHING COMPANY", DataType.String, "style3")
    '    scell.MergeAcross = 9  '         // Merge two cells to


    '    Dim sRw2 As WorksheetRow = sheet1.Table.Rows.Add
    '    'Rw2.Cells.Add("", DataType.String, "style1")
    '    'Rw2.Cells.Add("", DataType.String, "style1")
    '    Dim scell1 As WorksheetCell = sRw2.Cells.Add("Sub Contract COST SHEET From " & Format(CDate(mskdatefr.Text), "dd-MM-yyyy") & " To " & Format(CDate(mskdateto.Text), "dd-MM-yyyy"), DataType.String, "style2")
    '    scell1.MergeAcross = 9

    '    Dim sRw3 As WorksheetRow = sheet1.Table.Rows.Add
    '    'style2.Font.Bold = False


    '    For i = 0 To sCTRL.Rows - 1

    '        Dim sRow0 As WorksheetRow = sheet1.Table.Rows.Add

    '        For j = 0 To sCTRL.Cols - 1

    '            ' Add a cell
    '            'Row0.Cells.Add("Hello World", DataType.String, "style1")
    '            If j > 1 Then
    '                If i = 0 Then
    '                    sRow0.Cells.Add(sCTRL.get_TextMatrix(i, j), DataType.String, "style2")
    '                Else
    '                    sRow0.Cells.Add(Convert.ToDecimal(sCTRL.get_TextMatrix(i, j)), DataType.Number, "style1")

    '                End If

    '            Else
    '                If i = 0 Then
    '                    sRow0.Cells.Add(sCTRL.get_TextMatrix(i, j), DataType.String, "style2")
    '                Else
    '                    sRow0.Cells.Add(sCTRL.get_TextMatrix(i, j), DataType.String, "style1")
    '                End If

    '            End If
    '        Next j
    '        If i = 0 Then
    '            'Dim style As WorksheetStyle = book.Styles.Add("style1")
    '            style.Font.Bold = True
    '        Else
    '            'style = book.Styles.Add("style1")
    '            'style = book.Styles.Add("style1")
    '            style.Font.Bold = False
    '            style.Font.Size = 10
    '            style.Alignment.Vertical = StyleVerticalAlignment.Top
    '            'style.Alignment.Horizontal = StyleHorizontalAlignment.Justify
    '        End If

    '    Next i

    '    Dim sRow2 As WorksheetRow = sheet1.Table.Rows.Add
    '    sRow2.Cells.Add("", DataType.String, "style1")
    '    sRow2.Cells.Add("", DataType.String, "style1")
    '    sRow2.Cells.Add("Total", DataType.String, "style2")
    '    sRow2.Cells.Add(Format(Convert.ToDecimal(lbltotprsnts.Text), "####0"), DataType.Number, "style2")
    '    sRow2.Cells.Add(Format(Convert.ToDecimal(totlblexps.Text), "##########0.00"), DataType.Number, "style2")
    '    sRow2.Cells.Add(Format(Convert.ToDecimal(lbltotprods.Text), "#########0"), DataType.Number, "style2")
    '    sRow2.Cells.Add(Convert.ToDecimal(lbltotwages.Text), DataType.Number, "style2")

    '    'Dim Row3 As WorksheetRow = sheet.Table.Rows.Add
    '    'Row3.Cells.Add("", DataType.String, "style1")
    '    'Row3.Cells.Add("", DataType.String, "style1")
    '    'Row3.Cells.Add("", DataType.String, "style1")
    '    'Row3.Cells.Add("", DataType.String, "style1")
    '    'Row3.Cells.Add("", DataType.String, "style1")
    '    'Row3.Cells.Add("Regular Shirt", DataType.String, "style2")
    '    'Row3.Cells.Add(Format(Convert.ToDecimal(lblrgqty.Text), "########0"), DataType.Number, "style2")
    '    'Dim Row1 As WorksheetRow = sheet.Table.Rows.Add
    '    'Row1.Cells.Add("", DataType.String, "style1")
    '    'Row1.Cells.Add("", DataType.String, "style1")
    '    'Row1.Cells.Add("", DataType.String, "style1")
    '    'Row1.Cells.Add("", DataType.String, "style1")
    '    'Row1.Cells.Add("", DataType.String, "style1")
    '    'Row1.Cells.Add("LS Shirt", DataType.String, "style2")
    '    'Row1.Cells.Add(Format(Convert.ToDecimal(lbllsqty.Text), "########0"), DataType.Number, "style2")

    '    Dim sRow7 As WorksheetRow = sheet1.Table.Rows.Add
    '    sRow7.Cells.Add("", DataType.String, "style1")
    '    sRow7.Cells.Add("", DataType.String, "style1")
    '    sRow7.Cells.Add("", DataType.String, "style1")
    '    sRow7.Cells.Add("", DataType.String, "style1")
    '    sRow7.Cells.Add("", DataType.String, "style1")
    '    sRow7.Cells.Add("", DataType.String, "style1")
    '    sRow7.Cells.Add("", DataType.String, "style1")
    '    sRow7.Cells.Add("", DataType.String, "style1")
    '    sRow7.Cells.Add("", DataType.String, "style1")
    '    sRow7.Cells.Add(Label6.Text, DataType.String, "style3")
    '    sRow7.Cells.Add(Format(Convert.ToDecimal(lbltotcostpcs.Text), "########0.00"), DataType.Number, "style3")
    '    Dim sRow8 As WorksheetRow = sheet1.Table.Rows.Add
    '    '***


    '    For k = 0 To sctrl2.Rows - 1

    '        Dim sRow4 As WorksheetRow = sheet1.Table.Rows.Add

    '        For jk = 0 To sctrl2.Cols - 1

    '            ' Add a cell
    '            'Row0.Cells.Add("Hello World", DataType.String, "style1")
    '            If jk > 0 Then
    '                If k = 0 Then
    '                    sRow4.Cells.Add(sctrl2.get_TextMatrix(k, jk), DataType.String, "style2")
    '                Else
    '                    sRow4.Cells.Add(Convert.ToDecimal(sctrl2.get_TextMatrix(k, jk)), DataType.Number, "style1")
    '                End If

    '            Else
    '                If k = 0 Then
    '                    sRow4.Cells.Add(sctrl2.get_TextMatrix(k, jk), DataType.String, "style2")
    '                Else
    '                    sRow4.Cells.Add(sctrl2.get_TextMatrix(k, jk), DataType.String, "style1")
    '                End If

    '            End If

    '        Next jk
    '        If k = 0 Then
    '            'Dim style As WorksheetStyle = book.Styles.Add("style1")
    '            style.Font.Bold = True
    '        Else
    '            'style = book.Styles.Add("style1")
    '            'style = book.Styles.Add("style1")
    '            style.Font.Bold = False
    '            style.Font.Size = 10
    '            style.Alignment.Vertical = StyleVerticalAlignment.Top
    '            'style.Alignment.Horizontal = StyleHorizontalAlignment.Justify
    '        End If


    '    Next k

    '    Dim sRow5 As WorksheetRow = sheet1.Table.Rows.Add

    '    Dim sRow6 As WorksheetRow = sheet1.Table.Rows.Add
    '    sRow6.Cells.Add("", DataType.String, "style1")
    '    sRow6.Cells.Add("Total", DataType.String, "style2")
    '    sRow6.Cells.Add(Format(Convert.ToDecimal(lblihtots.Text), "##########0.00"), DataType.Number, "style2")
    '    sRow6.Cells.Add(Convert.ToDecimal(lblexpcosts.Text), DataType.Number, "style2")


    'End Sub
    Private Sub cmdscexp_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdscexp.ClickButtonArea

    End Sub
End Class
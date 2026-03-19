Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports CarlosAg.ExcelXmlWriter
Imports System.IO
Public Class Frmincentivecalc
    Dim i, j, msel, o_id, n, k As Integer
    Dim MSQL, qry, dqry, dqry1, dqry2, merr, qry1 As String
    Dim trans As SqlTransaction
    Dim mtotincent, matt, mdate As Boolean
    'Dim s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16, s17, s18, s19, s20, s21, s22, s23, s24, s25, s26, s27, s28, s29, s30, s31, mtot As Double
    Private Sub Frmincentivecalc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        dg.Width = My.Computer.Screen.Bounds.Width - 20

        For i = 1 To 30
            cmbline.Items.Add(Str(i))
        Next
        cmbline.Items.Add("GENERAL")
        mdate = False
        matt = False
        optavg.Checked = True
    End Sub

    Private Sub calcincentive()

        ' dqry = "select b.date,b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr," & vbCrLf _
        '     & "isnull((b.totprodqty-(b.nomac*d.nopcs)),0) as incqty, round((isnull(f.incentive,0)*c.pper)/100,2)  incentive, " & vbCrLf _
        '& " isnull(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))* round((f.incentive*c.pper)/100,2)),0) else 0 end,0) amt,isnull(em.salary,0) salary," & vbCrLf _
        '& "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
        '& " (datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
        '& "SUM(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*f.incentive),0) else 0 end) over (partition by b.date,b.[lineno]) as totincentive, " & vbCrLf _
        '& " sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary " & vbCrLf _
        '& " from " & Trim(mcostdbnam) & ".dbo.operf b " & vbCrLf

        ' dqry = dqry & " left join (select l.bno,l.empid,l.empno,l.jobgrade,l.[lineno],l.pper from " & vbCrLf _
        '    & " (select bno, empid,empno,jobgrade,[lineno],opername,totprod,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade) mprod ,COUNT(empno) over(partition by bno,[lineno],jobgrade,empno) cnt, " & vbCrLf _
        '    & " case when totprod>0 and COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)<=1 then round((convert(real,totprod)/convert(real,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade)))*100,0) " & vbCrLf _
        '    & " else case  when COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)>1 then 100 else  0 end end pper,incyn    from " & Trim(mcostdbnam) & ".dbo.perf1) l " & vbCrLf _
        '    & " where incyn not in ('N'))  c on c.bno=b.bno and c.[lineno]=b.[lineno] "

        ' '& " left join (select bno, empid,empno,jobgrade,[lineno] from " & Trim(mcostdbnam) & ".dbo.perf1 where incyn not in ('N') group by empid,empno,jobgrade,bno,[lineno]) c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf _


        ' dqry = dqry & " left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) d on d.brand=b.brand " & vbCrLf _
        '& " left join " & Trim(mcostdbnam) & ".dbo.empmaster em on em.nemp_id=c.empid  " & vbCrLf _
        '& " Left Join " & vbCrLf _
        '& " (select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b " & vbCrLf _
        '& " Left Join " & vbCrLf _
        '& " (select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
        '& "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade " & vbCrLf _
        '& "where    date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' "

        '****New
        dqry = "select b.date,b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr," & vbCrLf _
            & "isnull((b.totprodqty-(b.nomac*d.nopcs)),0) as incqty,  round(isnull(c.incentiveamt,0),2) as incentive,  " & vbCrLf _
            & " isnull(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))* c.incentiveamt),2) else 0 end,0) amt,isnull(em.salary,0) salary," & vbCrLf _
       & "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
       & " (datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
       & "SUM(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*c.incentiveamt),0) else 0 end) over (partition by b.date,b.[lineno]) as totincentive, " & vbCrLf _
       & " sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary " & vbCrLf _
       & " from " & Trim(mcostdbnam) & ".dbo.operf b " & vbCrLf

        'dqry = dqry & " left join (select l.bno,l.empid,l.empno,l.jobgrade,l.[lineno],l.pper from " & vbCrLf _
        '   & " (select bno, empid,empno,jobgrade,[lineno],opername,totprod,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade) mprod ,COUNT(empno) over(partition by bno,[lineno],jobgrade,empno) cnt, " & vbCrLf _
        '   & " case when totprod>0 and COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)<=1 then round((convert(real,totprod)/convert(real,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade)))*100,0) " & vbCrLf _
        '   & " else case  when COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)>1 then 100 else  0 end end pper,incyn    from " & Trim(mcostdbnam) & ".dbo.perf1) l " & vbCrLf _
        '   & " where incyn not in ('N'))  c on c.bno=b.bno and c.[lineno]=b.[lineno] "

        dqry = dqry & "left join (select j.brand,j.bno,j.empid,j.empno,j.[lineno],SUM(j.incentiveamt) incentiveamt from (" & vbCrLf _
                           & " select c.brand, b.bno, b.empid,b.empno,b.jobgrade,b.[lineno],b.opername,b.totprod,b.totmin, (b.totmin/convert(real,480))*100 perc, " & vbCrLf _
                           & " (b.totmin/convert(real,480))*100 pper,  case when isnull(b.totmin,0)>0 then round(i.incentive* (b.totmin/convert(real,480)),2) else 0 end incentiveamt, " & vbCrLf _
                           & " i.incentive, b.incyn    from prodcost.dbo.perf1 b " & vbCrLf _
                           & " left join prodcost.dbo.operf c on c.bno=b.bno and c.date=b.date " & vbCrLf _
                           & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade)j " & vbCrLf _
                           & " group by j.brand,j.bno,j.empid,j.empno,j.[lineno])   c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf



        '& " left join (select bno, empid,empno,jobgrade,[lineno] from " & Trim(mcostdbnam) & ".dbo.perf1 where incyn not in ('N') group by empid,empno,jobgrade,bno,[lineno]) c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf _


        dqry = dqry & " left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) d on d.brand=b.brand " & vbCrLf _
       & " left join " & Trim(mcostdbnam) & ".dbo.empmaster em on em.nemp_id=c.empid  " & vbCrLf _
       & " Left Join " & vbCrLf _
       & " (select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b " & vbCrLf _
       & " Left Join " & vbCrLf _
       & " (select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
       & "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade " & vbCrLf _
       & "where    date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' "






        '& "where   (b.totprodqty-(b.nomac*d.nopcs)) > 0 and date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' "
        If Len(Trim(cmbline.Text)) > 0 Then
            dqry = dqry + " and b.[lineno]='" & Trim(cmbline.Text) & "'"
        End If
        If chkall.Checked = False Then
            dqry = dqry & "  and (b.totprodqty-(b.nomac*d.nopcs)) > 0 "
        End If
        dqry = dqry + " order by b.date,b.[lineno],em.jobgrade "

        '& "where  case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then  (b.totprodqty-(b.nomac*d.nopcs)) else 0 end  between f.nprodpcsfr and f.nprodpcsto and date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' "

        qry1 = "select sum(totprodqty) totqty from " & Trim(mcostdbnam) & ".dbo.operf where between f.nprodpcsfr and f.nprodpcsto and date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' "
        If Len(Trim(cmbline.Text)) > 0 Then
            qry1 = qry1 + " and [lineno] not in ('100')"
        End If

        qry = "select b.date,b.brand,b.processname,b.[lineno], c.empid,c.jobgrade, b.totprodqty,b.totrejqty,b.nomac,(b.nomac*15) minpcs, COUNT(c.empid) over(partition by b.date,b.[lineno]) utiper," & vbCrLf _
            & "((b.nomac*d.nopcs)-b.totprodqty) as incqty,ceiling(convert(decimal(10,2),((b.nomac*d.nopcs)-b.totprodqty))/50) as totrnk " & vbCrLf _
            & " from " & Trim(mcostdbnam) & ".dbo.operf b " & vbCrLf _
            & " left join (select bno, empid,jobgrade,[lineno] from " & Trim(mcostdbnam) & ".dbo.perf1 where incyn='Y' group by empid,jobgrade,bno,[lineno]) c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf _
            & " left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) d on d.brand=b.brand " & vbCrLf _
            & " where date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'"

        'qry = " select * from " & Trim(mcostdbnam) & ".dbo.operf with (nolock) where bno=" & Val(txtno.Text) & " and  [lineno]='" & cmbline.Text & "' and date='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"

        MSQL = "select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from incentivemaster b" & vbCrLf _
         & " Left Join " & vbCrLf _
         & "(select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
         & "(select brand, nprodpcsfr,nprodpcsto from " & Trim(mcostdbnam) & "dbo.incentivemaster group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto"



        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        cmd.CommandText = dqry
        cmd.Connection = con
        da.SelectCommand = cmd
        Dim dt As New DataTable
        da.Fill(dt)
        dg.DataSource = dt
        dg.Columns(0).Width = 80
        dg.Columns(1).Width = 150
        dg.Columns(2).Width = 90
        dg.Columns(3).Width = 50
        dg.Columns(4).Width = 50
        dg.Columns(5).Width = 50
        dg.Columns(6).Width = 150
        dg.Columns(7).Width = 50
        dg.Columns(8).Width = 70
        dg.Columns(9).Width = 40
        dg.Columns(10).Width = 50
        dg.Columns(11).Width = 50
        dg.Columns(12).Width = 50
        dg.Columns(13).Width = 50
        dg.Columns(14).Width = 50
        dg.Columns(15).Width = 50
        dg.Columns(16).Width = 50
        dg.Columns(17).Width = 50
        dg.Columns(18).Width = 50
        dg.Columns(19).Width = 50

        dg.Columns(0).ReadOnly = True
        dg.Columns(1).ReadOnly = True
        dg.Columns(2).ReadOnly = True
        dg.Columns(3).ReadOnly = True
        dg.Columns(4).ReadOnly = True
        dg.Columns(5).ReadOnly = True
        dg.Columns(6).ReadOnly = True
        dg.Columns(7).ReadOnly = True
        dg.Columns(8).ReadOnly = True
        dg.Columns(9).ReadOnly = True
        dg.Columns(10).ReadOnly = True
        dg.Columns(11).ReadOnly = True
        dg.Columns(12).ReadOnly = True
        dg.Columns(13).ReadOnly = True
        dg.Columns(14).ReadOnly = True
        dg.Columns(15).ReadOnly = True
        dg.Columns(16).ReadOnly = True
        dg.Columns(17).ReadOnly = True
        dg.Columns(18).ReadOnly = True
        dg.Columns(19).ReadOnly = True

        lbltotinc.Text = ""
        lbldsalary.Text = ""
        lbltotqty.Text = ""
        n = 0
        For i = 0 To dg.Rows.Count - 1
            lbltotinc.Text = Val(lbltotinc.Text) + Val(dg.Rows(i).Cells(15).Value)
            lbldsalary.Text = Val(lbldsalary.Text) + Val(dg.Rows(i).Cells(17).Value)
            n = n + 1

        Next
        lblman.Text = n
        lbltotqty.Text = gettotqty()
        'Dim dt1 As DataTable = getDataTable(qry)
        'If dt1.Rows.Count > 0 Then
        '    For Each row As DataRow In dt1.Rows

        '    Next
        'End If
        '        MSQL = " SELECT SNO,EMPNO,EMPID,EMPNAME,opername,sam,totprod,totmin,isnull(wt,0) wt,isnull(mcdwntime,0) mcdwntime,isnull(elecdwntime,0) elecdwntime ,isnull(others,0) others,totoffstdtime,onstdtime,samproduced,per,effper,totper,uttper,jobgrade,isnull(incentive,0) incentive,isnull(amount,0) amount,brand,isnull(rejqty,0) rejqty,salary,incyn from " & Trim(mcostdbnam) & ".dbo.perf1 where bno=" & Val(txtno.Text) & " and [lineno]='" & Trim(cmbline.Text) & "' and date='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' order by sno"

        '        dg.Rows.Clear()
        '        Dim dt As DataTable = getDataTable(MSQL)
        '        For Each row As DataRow In dt.Rows
        '            dg.Rows.Add()
        '            dg.Rows(dg.RowCount - 1).Cells(0).Value = row("sno")
        '            dg.Rows(dg.RowCount - 1).Cells(1).Value = row("empid")
        '            dg.Rows(dg.RowCount - 1).Cells(2).Value = row("empno")
        '            dg.Rows(dg.RowCount - 1).Cells(3).Value = row("empname")
        '            dg.Rows(dg.RowCount - 1).Cells(4).Value = row("opername")
        '            dg.Rows(dg.RowCount - 1).Cells(5).Value = row("sam")
        '            dg.Rows(dg.RowCount - 1).Cells(6).Value = row("totprod")
        '            dg.Rows(dg.RowCount - 1).Cells(7).Value = row("totmin")
        dt.Dispose()
        cmd.Dispose()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim fr, tt, mtot, mtot1 As Integer
        'fr = 0
        'tt = 0
        'mtot = 240
        'mtot1 = 0
        'For nn As Integer = 0 To 5 - 1
        '    mtot1 = mtot1 + 50
        '    fr = nn * 50

        '    TextBox1.Text = TextBox1.Text & Str(fr) & " - " & Str(mtot1) & vbCrLf
        'Next
        mtotincent = False
        Call calcincentive()
        Call calcincincharge()
        Call qcflincentive()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("Cost Sheet Export", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            gridexcelexport(dg, 1)
        Else
            If mtotincent = False Then
                If matt = True Then
                    gridexcelexport(dg, 1)
                Else
                    expexcelrep()
                End If

            Else
                expexcelrep2()
            End If
            End If
    End Sub

    Private Function gettotqty() As Integer ' generating auto employee id for a new employee

        'Dim query = "Select IsNull(Max(bno),0)+1 operid From " & Trim(mcostdbnam) & ".dbo.operf"



        qry1 = "select isnull(sum(totprodqty),0) totqty from " & Trim(mcostdbnam) & ".dbo.operf where date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' "
        If Len(Trim(cmbline.Text)) > 0 Then
            qry1 = qry1 + " and [lineno]='" & Trim(cmbline.Text) & "'"
        Else
            qry1 = qry1 + " and [lineno] not in ('100')"
        End If

        Dim dr As SqlDataReader
        dr = getDataReader(qry1)
        dr.Read()
        If IsDBNull(dr("totqty")) = False Then
            o_id = dr("totqty")
        Else
            o_id = 0
        End If
        dr.Close()

        Return o_id

    End Function
    Private Sub calccost()
        MSQL = "select k.linno,k.totprodqty,k.nomac,COUNT(k.cnt) noman,SUM(k.salary) salary,round(SUM(k.salary)/27,2) daysal,round(SUM(k.salary)/COUNT(k.cnt),2) avgsal,  round(k.nomac+ round(k.nomac*51.40/100,0),0) actmanpwr,round(COUNT(k.cnt)-(k.nomac+ round(k.nomac*51.40/100,0)),0) excess," & vbCrLf _
            & "round(k.nomac*18.57,0) actprodqty, round((SUM(k.salary)/COUNT(k.cnt)) * (k.nomac+ round(k.nomac*51.40/100,0)),2) actsal, round((SUM(k.salary)/COUNT(k.cnt)) * (k.nomac+ round(k.nomac*51.40/100,0))/27,2) actdaysal, " & vbCrLf _
            & "round((SUM(k.salary)/COUNT(k.cnt)) * (k.nomac+ round(k.nomac*51.40/100,0))/27/(case when k.nomac>0 then k.nomac*18 else 18 end),2) actcost " & vbCrLf _
            & ", round(case when k.totprodqty>0 then(SUM(k.salary)/27)/k.totprodqty else 0 end,2) ourcost        from " & vbCrLf _
            & " (select b.[lineno] linno, b.empno cnt,c.totprodqty,c.nomac,d.salary from " & Trim(mcostdbnam) & ".dbo.perf1 b " & vbCrLf _
            & "left join " & Trim(mcostdbnam) & ".dbo.operf c on c.bno=b.bno   " & vbCrLf _
            & "left join " & Trim(mcostdbnam) & ".dbo.empmaster d on d.nemp_id=b.empid and d.nempno=b.empno " & vbCrLf _
            & "where b.DATE='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'  " & vbCrLf _
            & "group by b.[lineno], b.empno,c.totprodqty,c.nomac,d.salary) k " & vbCrLf _
            & "group by k.linno,k.totprodqty,k.nomac "

        Dim cmd1 As New SqlCommand
        Dim da1 As New SqlDataAdapter
        'dg.Rows.Clear()
        cmd1.CommandText = MSQL
        cmd1.Connection = con
        da1.SelectCommand = cmd1
        Dim dt1 As New DataTable
        da1.Fill(dt1)
        dg.DataSource = dt1

    End Sub
    Private Sub calcincincharge()
        'old
        ' dqry = "select kk.jobgrade,kk.empno,em.empname, kk.[lineno],isnull(cc.lineamt,0) lineamt,round(ISNULL(cc.lineamt,0)*2.5/100,0) lineinchargamt from " & vbCrLf _
        '       & " (select c.empid,c.empno,c.jobgrade,c.[lineno] from " & Trim(mcostdbnam) & ".dbo.operf b " & vbCrLf _
        '       & "left join " & Trim(mcostdbnam) & ".dbo.perf1 c on c.bno=b.bno " & vbCrLf _
        '       & "where  b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and c.jobgrade in ('XI') " & vbCrLf _
        '       & " group by c.[lineno], c.empid,c.empno,c.jobgrade) kk " & vbCrLf _
        '       & " left join ("

        ' dqry = dqry + "select l.[lineno],l.lineamt,SUM(l.lineamt) over() tot from " & vbCrLf _
        '   & "(select j.[lineno],j.totincentive  lineamt from  " & vbCrLf _
        ' & "(select b.date,b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, c.jobgrade, b.totprodqty,b.totrejqty,b.nomac, (b.nomac*d.nopcs) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr," & vbCrLf _
        '     & "(b.totprodqty-(b.nomac*d.nopcs)) as incqty, f.incentive, " & vbCrLf _
        '& " case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*f.incentive),0) else 0 end amt,em.salary," & vbCrLf _
        '& "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
        '& " (datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
        '& "SUM(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*f.incentive),0) else 0 end) over (partition by b.[lineno]) as totincentive, " & vbCrLf _
        '& " sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno])  totsalary" & vbCrLf _
        '& " from " & Trim(mcostdbnam) & ".dbo.operf b " & vbCrLf _
        '& " left join (select bno, empid,empno,jobgrade,[lineno] from " & Trim(mcostdbnam) & ".dbo.perf1 where incyn not in ('N') group by empid,empno,jobgrade,bno,[lineno]) c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf _
        '& " left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) d on d.brand=b.brand " & vbCrLf _
        '& " Left Join " & vbCrLf _
        '& " (select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b " & vbCrLf _
        '& " Left Join " & vbCrLf _
        '& " (select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
        '& "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=c.jobgrade " & vbCrLf _
        '& " left join " & Trim(mcostdbnam) & ".dbo.empmaster em on em.nemp_id=c.empid  " & vbCrLf _
        '& "where  case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then  (b.totprodqty-(b.nomac*d.nopcs)) else 0 end  between f.nprodpcsfr and f.nprodpcsto and date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' "
        ' 'If Len(Trim(cmbline.Text)) > 0 Then
        ' 'dqry = dqry + " and b.[lineno]='" & Trim(cmbline.Text) & "'"
        ' 'End If
        ' dqry = dqry + ") j group by j.[lineno],j.totincentive) l "
        ' dqry = dqry + ") cc on cc.[lineno]=kk.[lineno]"
        ' dqry = dqry + " left join " & Trim(mcostdbnam) & ".dbo.empmaster em on em.nemp_id=kk.empid order by kk.empno "

        dqry = "declare @d1 as nvarchar(20) " & vbCrLf
        dqry = dqry & " declare @dday as integer  " & vbCrLf _
                    & "set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'" & vbCrLf _
                    & "set @dday= (datediff(day, dateadd(day, 1-day(@d1), @d1),dateadd(month, 1, dateadd(day, 1-day(@d1), @d1))))"





        dqry = dqry & " select kk.jobgrade,kk.empno,em.empname, kk.[lineno],isnull(cc.lineamt,0) lineamt,case when ISNULL(cc.lineamt,0)>0 then ROUND(isnull(cc.lineamt,0)/isnull(cc.noper,0),0) else 0 end  lineinchargamt,isnull(round(em.salary/@dday,0),0) dsalary from " & vbCrLf _
             & " (select c.empid,c.empno,c.jobgrade,c.[lineno] from " & Trim(mcostdbnam) & ".dbo.operf b " & vbCrLf _
             & "left join " & Trim(mcostdbnam) & ".dbo.perf1 c on c.bno=b.bno " & vbCrLf _
             & "where  b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and c.jobgrade in ('XI') " & vbCrLf _
             & " group by c.[lineno], c.empid,c.empno,c.jobgrade) kk " & vbCrLf _
             & " left join ("

        dqry = dqry + "select l.[lineno],l.lineamt,SUM(l.lineamt) over() tot,isnull(l.noper,0) noper from " & vbCrLf _
          & "(select j.[lineno],j.totincentive  lineamt,isnull(j.noper,0) noper from  " & vbCrLf _
        & "(select b.date,b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, (b.nomac*d.nopcs) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr," & vbCrLf _
            & "(b.totprodqty-(b.nomac*d.nopcs)) as incqty, round((isnull(f.incentive,0)*c.pper)/100,2) incentive, " & vbCrLf _
       & " case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*round((f.incentive*c.pper)/100,2)),0) else 0 end amt,em.salary," & vbCrLf _
       & "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
       & " (datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
       & "SUM(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*f.incentive),0) else 0 end) over (partition by b.[lineno]) as totincentive, " & vbCrLf _
       & " sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno])  totsalary, COUNT(c.empid) over (PARTITION by b.[lineno]) noper" & vbCrLf _
       & " from " & Trim(mcostdbnam) & ".dbo.operf b " & vbCrLf


        dqry = dqry & " left join (select l.bno,l.empid,l.empno,l.jobgrade,l.[lineno],l.pper from " & vbCrLf _
           & " (select bno, empid,empno,jobgrade,[lineno],opername,totprod,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade) mprod ,COUNT(empno) over(partition by bno,[lineno],jobgrade,empno) cnt, " & vbCrLf _
           & " case when totprod>0 and COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)<=1 then round((convert(real,totprod)/convert(real,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade)))*100,0) " & vbCrLf _
           & " else case  when COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)>1 then 100 else  0 end end pper,incyn    from " & Trim(mcostdbnam) & ".dbo.perf1) l " & vbCrLf _
           & " where incyn not in ('N'))  c on c.bno=b.bno and c.[lineno]=b.[lineno] "

        '& " left join (select bno, empid,empno,jobgrade,[lineno] from " & Trim(mcostdbnam) & ".dbo.perf1 where incyn not in ('N') group by empid,empno,jobgrade,bno,[lineno]) c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf _

        dqry = dqry & " left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) d on d.brand=b.brand " & vbCrLf _
       & " left join " & Trim(mcostdbnam) & ".dbo.empmaster em on em.nemp_id=c.empid  " & vbCrLf _
       & " Left Join " & vbCrLf _
       & " (select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b " & vbCrLf _
       & " Left Join " & vbCrLf _
       & " (select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
       & "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade " & vbCrLf _
       & "where  (b.totprodqty-(b.nomac*d.nopcs)) > 0  and date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' "
        'If Len(Trim(cmbline.Text)) > 0 Then
        'dqry = dqry + " and b.[lineno]='" & Trim(cmbline.Text) & "'"
        'End If
        dqry = dqry + ") j group by j.[lineno],j.totincentive,isnull(j.noper,0)) l "
        dqry = dqry + ") cc on cc.[lineno]=kk.[lineno]"
        dqry = dqry + " left join " & Trim(mcostdbnam) & ".dbo.empmaster em on em.nemp_id=kk.empid order by kk.empno "
        'dqry = dqry + " order by b.date,b.[lineno],c.jobgrade "


        Dim cmd1 As New SqlCommand
        Dim da1 As New SqlDataAdapter

        cmd1.CommandText = dqry
        cmd1.Connection = con
        da1.SelectCommand = cmd1
        Dim dt1 As New DataTable
        da1.Fill(dt1)
        dg1.DataSource = dt1

        dg1.Columns(0).Width = 50
        dg1.Columns(1).Width = 70
        dg1.Columns(2).Width = 200
        dg1.Columns(3).Width = 50
        dg1.Columns(4).Width = 80
        dg1.Columns(5).Width = 70
        dg1.Columns(6).Width = 70

        dg1.Columns(0).ReadOnly = True
        dg1.Columns(1).ReadOnly = True
        dg1.Columns(2).ReadOnly = True
        dg1.Columns(3).ReadOnly = True
        dg1.Columns(4).ReadOnly = True
        dg1.Columns(5).ReadOnly = True
        dg1.Columns(6).ReadOnly = True


        lbldg1man.Text = ""
        lbldg1inc.Text = ""
        lbldg1sal.Text = ""

        n = 0
        For i = 0 To dg1.Rows.Count - 1
            lbldg1inc.Text = Val(lbldg1inc.Text) + Val(dg1.Rows(i).Cells(5).Value)
            lbldg1sal.Text = Val(lbldg1sal.Text) + Val(dg1.Rows(i).Cells(6).Value)

            'lbldg1inc.Text = Val(lbldg1inc.Text) + Val(dg1.Rows(i).Cells(17).Value)
            n = n + 1

        Next
        lbldg1man.Text = n
        dt1.Dispose()
        cmd1.Dispose()
    End Sub

    Private Sub qcflincentive()

        dqry = "select v.jobgrade,v.empno,v.empname, v.totlnamt,v.totamt from ("
        dqry = dqry + "select  jj.jobgrade,jj.empno,em.empname, jj.[lineno],ISNULL(ll.lineinchargamt,0) lineinchargamt,round(ISNULL(ll.lineinchargamt,0)*18/100,0) qcflinchargamt,SUM(round(ISNULL(ll.lineinchargamt,0)*18/100,0)) over(partition by jj.empno) totamt,SUM(ISNULL(ll.lineinchargamt,0)) over(partition by jj.empno) totlnamt from  " & vbCrLf _
              & "(select c.empid,c.empno,c.jobgrade,c.[lineno] from " & Trim(mcostdbnam) & ".dbo.operf b " & vbCrLf _
              & "left join " & Trim(mcostdbnam) & ".dbo.perf1 c on c.bno=b.bno " & vbCrLf _
              & " where  b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and c.jobgrade in ('XII') " & vbCrLf _
              & "group by c.[lineno], c.empid,c.empno,c.jobgrade) jj " & vbCrLf _
              & "Left Join ("

        dqry = dqry + "select kk.jobgrade, kk.empid,kk.empno,kk.[lineno],isnull(cc.lineamt,0) lineamt,round(ISNULL(cc.lineamt,0)*2.5/100,0) lineinchargamt,  isnull(cc.tot,0) tot from " & vbCrLf _
             & " (select c.empid,c.empno,c.jobgrade,c.[lineno] from " & Trim(mcostdbnam) & ".dbo.operf b " & vbCrLf _
             & "left join " & Trim(mcostdbnam) & ".dbo.perf1 c on c.bno=b.bno " & vbCrLf _
             & "where  b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and c.jobgrade in ('XI') " & vbCrLf _
             & " group by c.[lineno], c.empid,c.empno,c.jobgrade) kk " & vbCrLf _
             & " left join ("

        dqry = dqry + "select l.[lineno],l.lineamt,SUM(l.lineamt) over() tot from " & vbCrLf _
          & "(select j.[lineno],j.totincentive  lineamt from  " & vbCrLf _
        & "(select b.date,b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, c.jobgrade, b.totprodqty,b.totrejqty,b.nomac, (b.nomac*d.nopcs) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr," & vbCrLf _
            & "(b.totprodqty-(b.nomac*d.nopcs)) as incqty, f.incentive, " & vbCrLf _
       & " case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*f.incentive),0) else 0 end amt,em.salary," & vbCrLf _
       & "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
       & " (datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
       & "SUM(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*f.incentive),0) else 0 end) over (partition by b.[lineno]) as totincentive, " & vbCrLf _
       & " sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) totsalary " & vbCrLf _
       & " from " & Trim(mcostdbnam) & ".dbo.operf b " & vbCrLf _
       & " left join (select bno, empid,empno,jobgrade,[lineno] from " & Trim(mcostdbnam) & ".dbo.perf1 where incyn not in ('N')  group by empid,empno,jobgrade,bno,[lineno]) c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf _
       & " left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) d on d.brand=b.brand " & vbCrLf _
       & " Left Join " & vbCrLf _
       & " (select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b " & vbCrLf _
       & " Left Join " & vbCrLf _
       & " (select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
       & "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=c.jobgrade " & vbCrLf _
       & " left join " & Trim(mcostdbnam) & ".dbo.empmaster em on em.nemp_id=c.empid  " & vbCrLf _
       & "where  case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then  (b.totprodqty-(b.nomac*d.nopcs)) else 0 end  between f.nprodpcsfr and f.nprodpcsto and date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' "
       
        dqry = dqry + ") j group by j.[lineno],j.totincentive) l "
        dqry = dqry + ") cc on cc.[lineno]=kk.[lineno]"
        dqry = dqry + ") ll on ll.[lineno]=jj.[lineno]"
        dqry = dqry + "left join " & Trim(mcostdbnam) & ".dbo.empmaster em on em.nemp_id=jj.empid "
        dqry = dqry + ") v group by v.jobgrade,v.empno,v.empname, v.totlnamt,v.totamt order by v.empno"

        Dim cmd1 As New SqlCommand
        Dim da1 As New SqlDataAdapter

        cmd1.CommandText = dqry
        cmd1.Connection = con
        da1.SelectCommand = cmd1
        Dim dt1 As New DataTable
        da1.Fill(dt1)
        dg2.DataSource = dt1

        dg2.Columns(0).Width = 50
        dg2.Columns(1).Width = 70
        dg2.Columns(2).Width = 200
        dg2.Columns(3).Width = 50
        dg2.Columns(4).Width = 80
        'dg2.Columns(5).Width = 70

        dg2.Columns(0).ReadOnly = True
        dg2.Columns(1).ReadOnly = True
        dg2.Columns(2).ReadOnly = True
        dg2.Columns(3).ReadOnly = True
        dg2.Columns(4).ReadOnly = True
        'dg2.Columns(5).ReadOnly = True


        lbldg2man.Text = ""
        lbldg2inc.Text = ""

        n = 0
        For i = 0 To dg2.Rows.Count - 1
            lbldg2inc.Text = Val(lbldg2inc.Text) + Val(dg2.Rows(i).Cells(4).Value)
            'lbldg1inc.Text = Val(lbldg1inc.Text) + Val(dg1.Rows(i).Cells(17).Value)
            n = n + 1
        Next

        lbldg2man.Text = n
        dt1.Dispose()
        cmd1.Dispose()
    End Sub



    Private Sub expexcelrep()

        'Dim ldir, lmdir As String
        ''dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        ''mdir = Trim(dir) & "\barcodadd.txt"

        'ldir = System.AppDomain.CurrentDomain.BaseDirectory()
        'lmdir = Trim(ldir) & "StitchRateProposal.xls"

        'Dim ticks As Integer = Environment.TickCount
        '' Create the workbook
        'Dim book As Workbook = New Workbook
        '' Set the author
        'book.Properties.Author = "CarlosAg"

        '' Add some style
        'Dim style As WorksheetStyle = book.Styles.Add("style1")
        'style.Font.Bold = True
        'style.Font.Size = 10
        'style.Alignment.Vertical = StyleVerticalAlignment.Center


        'Dim style2 As WorksheetStyle = book.Styles.Add("style2")
        'style2.Font.Bold = False
        'style2.Font.Size = 10
        'style2.Alignment.Vertical = StyleVerticalAlignment.Center

        'Dim style22 As WorksheetStyle = book.Styles.Add("style22")
        'style22.Font.Bold = True
        'style22.Font.Size = 10
        'style22.Alignment.Horizontal = StyleHorizontalAlignment.Center
        ''style22.Alignment.Vertical = StyleVerticalAlignment.Center

        'Dim style3 As WorksheetStyle = book.Styles.Add("style3")
        'style3.Font.Bold = True
        'style3.Font.Size = 12
        'style3.Alignment.Horizontal = StyleHorizontalAlignment.Center
        ''style3.Alignment.Vertical = StyleVerticalAlignment.Center


        'Dim stylelf As WorksheetStyle = book.Styles.Add("styleLF")
        'stylelf.Font.Bold = True
        'stylelf.Font.Size = 10
        'stylelf.Alignment.Horizontal = StyleHorizontalAlignment.Left


        'Dim sheet As Worksheet = book.Worksheets.Add("Stitching Rate Proposal")
        ''Dim style2 As WorksheetStyle = book.Styles.Add("style2")

        'Dim Rw1 As WorksheetRow = sheet.Table.Rows.Add
        'Dim cell As WorksheetCell = Rw1.Cells.Add("ATITHYA CLOTHING COMPANY", DataType.String, "style3")
        'cell.MergeAcross = 8  '         // Merge two cells to

        'Dim RwW2 As WorksheetRow = sheet.Table.Rows.Add
        'RwW2.Cells.Add("", DataType.String, "style3")
        ''Dim cell2 As WorksheetCell = Rw2.Cells.Add("" & Trim(txtcardcode.Text) & " - " & Trim(cmbparty.Text) & "Stitching Charges Effect From :" & Format(CDate(mskdate.Text), "dd-MM-yyyy"), DataType.String, "stylelf")
        '';cell1.MergeAcross = 9

        'Dim Rw2 As WorksheetRow = sheet.Table.Rows.Add
        'Rw2.Cells.Add(txtcardcode.Text, DataType.String, "styleLF")
        'Rw2.Cells.Add(cmbparty.Text, DataType.String, "styleLF")
        'Rw2.Cells.Add("Stitching Charges Effect From :" & Format(CDate(mskdate.Text), "dd-MM-yyyy"), DataType.String, "styleLF")

        ''Dim cell1 As WorksheetCell = Rw2.Cells.Add("" & Trim(txtcardcode.Text) & " - " & Trim(cmbparty.Text) & "Stitching Charges Effect From :" & Format(CDate(mskdate.Text), "dd-MM-yyyy"), DataType.String, "stylelf")
        ''cell1.MergeAcross = 9

        ''Dim Rw3 As WorksheetRow = sheet.Table.Rows.Add
        ''style2.Font.Bold = False
        'Dim RwW3 As WorksheetRow = sheet.Table.Rows.Add
        'RwW3.Cells.Add("", DataType.String, "style3")



        'Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
        'Row0.Cells.Add("BRAND NAME", DataType.String, "style22")
        'Row0.Cells.Add("STYLE", DataType.String, "style22")
        'Row0.Cells.Add("CUTTING", DataType.String, "style22")
        'Row0.Cells.Add("FUSING", DataType.String, "style22")
        'Row0.Cells.Add("EMBROIDERY", DataType.String, "style22")
        'Row0.Cells.Add("STITCHING", DataType.String, "style22")
        'Row0.Cells.Add("KAJA", DataType.String, "style22")
        'Row0.Cells.Add("CHECKING", DataType.String, "style22")
        'Row0.Cells.Add("IRONING & PACKING", DataType.String, "style22")

        ''For i = 0 To dg.Columns.Count - 1
        ''    Row0.Cells.Add(dg.Rows(0).Cells(i).Value, DataType.String, "style2")
        ''Next i

        ''style2.Font.Bold = True
        'n = 0
        'For k = 0 To dg.Rows.Count - 1
        '    Dim Row1 As WorksheetRow = sheet.Table.Rows.Add

        '    Row1.Cells.Add(dg.Rows(k).Cells(0).Value, DataType.String, "style2")
        '    n = n + 1
        '    Row1.Cells.Add(dg.Rows(k).Cells(1).Value, DataType.String, "style2")
        '    n = n + 1
        '    If n > 1 Then
        '        Row1.Cells.Add(Format(Val(dg.Rows(k).Cells(2).Value), "####0.00"), DataType.Number, "style2")
        '        Row1.Cells.Add(Format(Val(dg.Rows(k).Cells(3).Value), "####0.00"), DataType.Number, "style2")
        '        Row1.Cells.Add(Format(Val(dg.Rows(k).Cells(4).Value), "####0.00"), DataType.Number, "style2")
        '        Row1.Cells.Add(Format(Val(dg.Rows(k).Cells(5).Value), "####0.00"), DataType.Number, "style2")
        '        Row1.Cells.Add(Format(Val(dg.Rows(k).Cells(6).Value), "####0.00"), DataType.Number, "style2")
        '        Row1.Cells.Add(Format(Val(dg.Rows(k).Cells(7).Value), "####0.00"), DataType.Number, "style2")
        '        Row1.Cells.Add(Format(Val(dg.Rows(k).Cells(8).Value), "####0.00"), DataType.Number, "style2")
        '        n = 0
        '    End If
        'Next k
        ''style2.Font.Bold = False






        ''*****************


        '' Save it
        ''book.Save("c:\test.xls")
        'book.Save(lmdir)
        ''open file
        'Process.Start(lmdir)
        ''Console.WriteLine("Time:{0}", (Environment.TickCount - ticks))



        Dim ldir, lmdir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\barcodadd.txt"

        If mos = "WIN" Then
            ldir = System.AppDomain.CurrentDomain.BaseDirectory()
            lmdir = Trim(ldir) & "incentive Report.xls"
        Else
            lmdir = mxlfilepath & "incentive Report.xls"
        End If


        Dim ticks As Integer = Environment.TickCount
        ' Create the workbook
        Dim book As Workbook = New Workbook
        ' Set the author
        book.Properties.Author = "CarlosAg"

        ' Add some style
        Dim style As WorksheetStyle = book.Styles.Add("Header1")
        style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style.Font.Bold = True
        style.Font.Size = 14
        style.Alignment.Vertical = StyleVerticalAlignment.Center

        Dim sheet As Worksheet = book.Worksheets.Add("Incentive")
        'Dim style2 As WorksheetStyle = book.Styles.Add("style2")

        'style2.Font.Bold = False

        Dim Row As WorksheetRow = sheet.Table.Rows.Add
        style.Font.Bold = True
        style.Font.Size = 14
        'styleh.Alignment.Vertical = StyleVerticalAlignment.Center
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Row.Cells.Add(head1, DataType.String, "Header1")

        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        Dim cell As WorksheetCell = Row.Cells.Add("ATITHYA CLOTHING COMPANY")
        cell.MergeAcross = 22
        cell.StyleID = "Header1"

        'Dim stylh2 As WorksheetStyle = book.Styles.Add("Header2")
        'Dim Rh2 As WorksheetRow = sheet.Table.Rows.Add
        Row = sheet.Table.Rows.Add
        style = book.Styles.Add("Header2")
        style.Alignment.Vertical = StyleVerticalAlignment.Center
        style.Alignment.Horizontal = StyleHorizontalAlignment.Center

        style.Font.Bold = True
        style.Font.Size = 12
        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        cell = Row.Cells.Add("INCENTIVE REPORT From " & Format(CDate(mskdatefr.Text), "dd-MM-yyyy") & " To " & Format(CDate(Mskdateto.Text), "dd-MM-yyyy"))
        cell.MergeAcross = 22
        cell.StyleID = "Header2"
        'style.Alignment.Vertical = StyleVerticalAlignment.Center
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Row.Cells.Add(head2, DataType.String, "Header2")
        'Row3.Cells.Add("Absent", DataType.String, "style1")




        ' Add some style
        'Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
        Row = sheet.Table.Rows.Add
        style = book.Styles.Add("style1")
        style.Alignment.Vertical = StyleVerticalAlignment.Center
        style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style.Font.Bold = True
        style.Font.Size = 12
        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
        'style.Alignment.Vertical = StyleVerticalAlignment.Center


        'Export Header Names Start
        Dim columnsCount As Integer = dg.Columns.Count

        'Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
        'Dim column As Integer = 0
        Dim column = dg.Columns.Count
        'Do Until column = columnsCount
        'For Each column In CTRL.Columns
        style.Font.Bold = True
        For iC As Integer = 0 To column - 1
            '- lastcol
            'Do Until column = columnsCount - lastcol
            Row.Cells.Add(dg.Columns(iC).HeaderText, DataType.String, "style1")

            'Row2.Cells.Add(column.Name, DataType.String, "style1")
            'Worksheet.Cells(1, column.Index + 1).Value = column.Name
        Next
        'Export Header Name End
        'Dim style3 As WorksheetStyle = book.Styles.Add("style1")
        style = book.Styles.Add("style2")
        style.Font.Bold = False
        style.Font.Size = 9
        style.Alignment.Vertical = StyleVerticalAlignment.Top
        'style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        'Export Each Row Start
        For i As Integer = 0 To dg.Rows.Count - 1
            'Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
            Row = sheet.Table.Rows.Add
            Dim columnIndex As Integer = 0
            Do Until columnIndex = columnsCount - (1 - 1)
                If (columnIndex >= 5 And columnIndex <= 6) Or columnIndex >= 9 Then
                    Row.Cells.Add(dg.Item(columnIndex, i).Value, DataType.Number, "style2")
                ElseIf columnIndex = 0 Then
                    If mtotincent = False Then
                        If mdate = False Then
                            Row.Cells.Add(Format(dg.Item(columnIndex, i).Value, "dd-MM-yyyy"), DataType.String, "style2")
                        Else
                            Row.Cells.Add(dg.Item(columnIndex, i).Value.ToString, DataType.String, "style2")
                        End If

                    Else
                        Row.Cells.Add(dg.Item(columnIndex, i).Value.ToString, DataType.String, "style2")
                    End If
                Else

                    Row.Cells.Add(dg.Item(columnIndex, i).Value.ToString, DataType.String, "style2")
                End If
                    'Row2.Cells(i + 2, columnIndex + 1).Value = CTRL.Item(columnIndex, i).Value.ToString
                    columnIndex += 1
            Loop
        Next

        Row = sheet.Table.Rows.Add
        style = book.Styles.Add("style11")
        'style.Alignment.Vertical = StyleVerticalAlignment.Center
        'style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style.Font.Bold = True
        style.Font.Size = 11

        'Row = sheet.Table.Rows.Add
        Row.Cells.Add("", DataType.String, "style11")

        Row = sheet.Table.Rows.Add

        Row.Cells.Add("", DataType.String, "style11")
        Row.Cells.Add("", DataType.String, "style11")
        Row.Cells.Add("", DataType.String, "style11")
        Row.Cells.Add(Label7.Text, DataType.String, "style11")
        Row.Cells.Add(lblman.Text, DataType.Number, "style11")
        Row.Cells.Add("", DataType.String, "style11")
        Row.Cells.Add(Label6.Text, DataType.String, "style11")
        Row.Cells.Add(lbltotqty.Text, DataType.Number, "style11")
        Row.Cells.Add("", DataType.String, "style11")
        Row.Cells.Add(Label4.Text, DataType.String, "style11")
        Row.Cells.Add(lbltotinc.Text, DataType.Number, "style11")
        Row.Cells.Add("", DataType.String, "style11")
        Row.Cells.Add(Label5.Text, DataType.String, "style11")
        Row.Cells.Add(lbldsalary.Text, DataType.Number, "style11")


        '****************************************



        Dim sheet1 As Worksheet = book.Worksheets.Add("Line Incharge Incentive")
        'Dim style1 As WorksheetStyle = book.Styles.Add("style5")
        Dim Row1 As WorksheetRow = sheet1.Table.Rows.Add
        Dim style1 As WorksheetStyle = book.Styles.Add("Header11")
        style1.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style1.Font.Bold = True
        style1.Font.Size = 14
        style1.Alignment.Vertical = StyleVerticalAlignment.Center

        'Dim Row1 As WorksheetRow = sheet1.Table.Rows.Add
        'style.Font.Bold = True
        'style.Font.Size = 14
        'styleh.Alignment.Vertical = StyleVerticalAlignment.Center
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Row.Cells.Add(head1, DataType.String, "Header1")

        style1.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        Dim cell1 As WorksheetCell = Row1.Cells.Add("ATITHYA CLOTHING COMPANY")
        cell1.MergeAcross = 5
        cell1.StyleID = "Header11"

        'Dim stylh2 As WorksheetStyle = book.Styles.Add("Header2")
        'Dim Rh2 As WorksheetRow = sheet.Table.Rows.Add
        'Dim Row1 As WorksheetRow = sheet1.Table.Rows.Add
        Row1 = sheet1.Table.Rows.Add
        style1 = book.Styles.Add("Header22")
        style1.Alignment.Vertical = StyleVerticalAlignment.Center
        style1.Alignment.Horizontal = StyleHorizontalAlignment.Center

        style1.Font.Bold = True
        style1.Font.Size = 12
        style1.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        cell = Row1.Cells.Add("LINE INCHARGE INCENTIVE REPORT From " & Format(CDate(mskdatefr.Text), "dd-MM-yyyy") & " To " & Format(CDate(Mskdateto.Text), "dd-MM-yyyy"))
        cell.MergeAcross = 5
        cell.StyleID = "Header22"




        Row1 = sheet1.Table.Rows.Add

        style1 = book.Styles.Add("style5")
        style1.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style1.Font.Bold = True
        style1.Font.Size = 12
        style1.Alignment.Vertical = StyleVerticalAlignment.Center

        'Dim sheet1 As Worksheet = book.Worksheets.Add("Line Incharge Incentive")

        style1 = book.Styles.Add("style6")
        style1.Font.Bold = False
        style1.Font.Size = 9
        style1.Alignment.Vertical = StyleVerticalAlignment.Top
        'style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style1.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

     
        'Export Header Names Start
        Dim columnsCount1 As Integer = dg1.Columns.Count

        'Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
        'Dim column As Integer = 0
        Dim column1 = dg1.Columns.Count
        'Do Until column = columnsCount
        'For Each column In CTRL.Columns
        style1.Font.Bold = True
        For iC As Integer = 0 To column1 - 1
            Row1.Cells.Add(dg1.Columns(iC).HeaderText, DataType.String, "style5")
        Next
        'Export Header Name End
        'Dim style3 As WorksheetStyle = book.Styles.Add("style1")
       
        'Export Each Row Start
        For i As Integer = 0 To dg1.Rows.Count - 1
            'Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
            Row1 = sheet1.Table.Rows.Add
            Dim columnIndex As Integer = 0
            Do Until columnIndex = columnsCount1 - (1 - 1)
                If (columnIndex >= 4 And columnIndex <= 5) Then
                    Row1.Cells.Add(dg1.Item(columnIndex, i).Value, DataType.Number, "style6")
                Else

                    Row1.Cells.Add(dg1.Item(columnIndex, i).Value.ToString, DataType.String, "style6")
                End If
                'Row2.Cells(i + 2, columnIndex + 1).Value = CTRL.Item(columnIndex, i).Value.ToString
                columnIndex += 1
            Loop
        Next

        Row1 = sheet1.Table.Rows.Add
        style1 = book.Styles.Add("style12")
        'style.Alignment.Vertical = StyleVerticalAlignment.Center
        'style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style1.Font.Bold = True
        style1.Font.Size = 11

        'Row = sheet.Table.Rows.Add
        ' Row1.Cells.Add("", DataType.String, "style12")

        Row1 = sheet1.Table.Rows.Add

        Row1.Cells.Add(Label8.Text, DataType.String, "style12")
        Row1.Cells.Add(lbldg1man.Text, DataType.Number, "style12")
        Row1.Cells.Add("", DataType.String, "style12")
        Row1.Cells.Add(Label10.Text, DataType.String, "style12")
        Row1.Cells.Add(lbldg1inc.Text, DataType.Number, "style12")
        


        '********************************************** Floor QC Line incharge
        '**********************************************



        Dim sheet2 As Worksheet = book.Worksheets.Add("FLOOR Incharge Incentive")
        'Dim style1 As WorksheetStyle = book.Styles.Add("style5")
        Dim Row2 As WorksheetRow = sheet2.Table.Rows.Add
        Dim style2 As WorksheetStyle = book.Styles.Add("Header12")
        style2.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style2.Font.Bold = True
        style2.Font.Size = 14
        style2.Alignment.Vertical = StyleVerticalAlignment.Center

        'Dim Row1 As WorksheetRow = sheet1.Table.Rows.Add
        'style.Font.Bold = True
        'style.Font.Size = 14
        'styleh.Alignment.Vertical = StyleVerticalAlignment.Center
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Row.Cells.Add(head1, DataType.String, "Header1")

        style2.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        Dim cell2 As WorksheetCell = Row2.Cells.Add("ATITHYA CLOTHING COMPANY")
        cell2.MergeAcross = 4
        cell2.StyleID = "Header12"

        'Dim stylh2 As WorksheetStyle = book.Styles.Add("Header2")
        'Dim Rh2 As WorksheetRow = sheet.Table.Rows.Add
        'Dim Row1 As WorksheetRow = sheet1.Table.Rows.Add
        Row2 = sheet2.Table.Rows.Add
        style2 = book.Styles.Add("Header23")
        style2.Alignment.Vertical = StyleVerticalAlignment.Center
        style2.Alignment.Horizontal = StyleHorizontalAlignment.Center

        style2.Font.Bold = True
        style2.Font.Size = 12
        style2.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        cell2 = Row2.Cells.Add("FLOOR/QC INCHARGE INCENTIVE REPORT From " & Format(CDate(mskdatefr.Text), "dd-MM-yyyy") & " To " & Format(CDate(Mskdateto.Text), "dd-MM-yyyy"))
        cell2.MergeAcross = 4
        cell2.StyleID = "Header23"




        Row2 = sheet2.Table.Rows.Add

        style2 = book.Styles.Add("style55")
        style2.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style2.Font.Bold = True
        style2.Font.Size = 12
        style2.Alignment.Vertical = StyleVerticalAlignment.Center

        'Dim sheet1 As Worksheet = book.Worksheets.Add("Line Incharge Incentive")

        style2 = book.Styles.Add("style66")
        style2.Font.Bold = False
        style2.Font.Size = 9
        style2.Alignment.Vertical = StyleVerticalAlignment.Top
        'style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style2.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)


        'Export Header Names Start
        Dim columnsCount2 As Integer = dg2.Columns.Count

        'Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
        'Dim column As Integer = 0
        Dim column2 = dg2.Columns.Count
        'Do Until column = columnsCount
        'For Each column In CTRL.Columns
        style2.Font.Bold = True
        For iC As Integer = 0 To column2 - 1
            Row2.Cells.Add(dg2.Columns(iC).HeaderText, DataType.String, "style55")
        Next

        'Export Header Name End
        'Dim style3 As WorksheetStyle = book.Styles.Add("style1")

        'Export Each Row Start
        For i As Integer = 0 To dg2.Rows.Count - 1
            'Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
            Row2 = sheet2.Table.Rows.Add
            Dim columnIndex As Integer = 0
            Do Until columnIndex = columnsCount2 - (1 - 1)
                If (columnIndex >= 3 And columnIndex <= 4) Or columnIndex = 1 Then
                    Row2.Cells.Add(dg2.Item(columnIndex, i).Value, DataType.Number, "style66")
                Else

                    Row2.Cells.Add(dg2.Item(columnIndex, i).Value.ToString, DataType.String, "style66")
                End If
                'Row2.Cells(i + 2, columnIndex + 1).Value = CTRL.Item(columnIndex, i).Value.ToString
                columnIndex += 1
            Loop
        Next

        Row2 = sheet2.Table.Rows.Add
        style2 = book.Styles.Add("style122")
        'style.Alignment.Vertical = StyleVerticalAlignment.Center
        'style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style2.Font.Bold = True
        style2.Font.Size = 11

        'Row = sheet.Table.Rows.Add
        ' Row1.Cells.Add("", DataType.String, "style12")

        Row2 = sheet2.Table.Rows.Add

        Row2.Cells.Add(Label12.Text, DataType.String, "style122")
        Row2.Cells.Add(lbldg2man.Text, DataType.Number, "style122")
        Row2.Cells.Add("", DataType.String, "style122")
        Row2.Cells.Add(Label14.Text, DataType.String, "style122")
        Row2.Cells.Add(lbldg2inc.Text, DataType.Number, "style122")



        book.Save(lmdir)
        'open file
        If mos = "WIN" Then
            Process.Start(lmdir)
        Else
            OpenWithLibreOffice(lmdir)
        End If

        mdate = False
    End Sub

    Private Sub expexcelrep2()




        Dim ldir, lmdir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\barcodadd.txt"
        If mos = "WIN" Then
            ldir = System.AppDomain.CurrentDomain.BaseDirectory()
            lmdir = Trim(ldir) & "incentive Report.xls"
        Else
            lmdir = mxlfilepath & "incentive Report1.xls"
        End If


        Dim ticks As Integer = Environment.TickCount
        ' Create the workbook
        Dim book As Workbook = New Workbook
        ' Set the author
        book.Properties.Author = "CarlosAg"

        ' Add some style
        Dim style As WorksheetStyle = book.Styles.Add("Header1")
        style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style.Font.Bold = True
        style.Font.Size = 14
        style.Alignment.Vertical = StyleVerticalAlignment.Center

        Dim sheet As Worksheet = book.Worksheets.Add("Incentive")
        'Dim style2 As WorksheetStyle = book.Styles.Add("style2")

        'style2.Font.Bold = False

        Dim Row As WorksheetRow = sheet.Table.Rows.Add
        style.Font.Bold = True
        style.Font.Size = 14
        'styleh.Alignment.Vertical = StyleVerticalAlignment.Center
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Row.Cells.Add(head1, DataType.String, "Header1")

        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        Dim cell As WorksheetCell = Row.Cells.Add("ATITHYA CLOTHING COMPANY")
        cell.MergeAcross = 22
        cell.StyleID = "Header1"

        'Dim stylh2 As WorksheetStyle = book.Styles.Add("Header2")
        'Dim Rh2 As WorksheetRow = sheet.Table.Rows.Add
        Row = sheet.Table.Rows.Add
        style = book.Styles.Add("Header2")
        style.Alignment.Vertical = StyleVerticalAlignment.Center
        style.Alignment.Horizontal = StyleHorizontalAlignment.Center

        style.Font.Bold = True
        style.Font.Size = 12
        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        cell = Row.Cells.Add("INCENTIVE REPORT From " & Format(CDate(mskdatefr.Text), "dd-MM-yyyy") & " To " & Format(CDate(Mskdateto.Text), "dd-MM-yyyy"))
        cell.MergeAcross = 22
        cell.StyleID = "Header2"
        'style.Alignment.Vertical = StyleVerticalAlignment.Center
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Row.Cells.Add(head2, DataType.String, "Header2")
        'Row3.Cells.Add("Absent", DataType.String, "style1")




        ' Add some style
        'Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
        Row = sheet.Table.Rows.Add
        style = book.Styles.Add("style1")
        style.Alignment.Vertical = StyleVerticalAlignment.Center
        style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style.Font.Bold = True
        style.Font.Size = 12
        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
        'style.Alignment.Vertical = StyleVerticalAlignment.Center


        style = book.Styles.Add("style41")
        style.Alignment.Vertical = StyleVerticalAlignment.Center
        style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style.Font.Bold = True
        style.Font.Size = 10
        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        'Export Header Names Start
        Dim columnsCount As Integer = dg.Columns.Count

        'Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
        'Dim column As Integer = 0
        Dim column = dg.Columns.Count
        'Do Until column = columnsCount
        'For Each column In CTRL.Columns
        style.Font.Bold = True
        For iC As Integer = 0 To column - 1
            '- lastcol
            'Do Until column = columnsCount - lastcol
            Row.Cells.Add(dg.Columns(iC).HeaderText, DataType.String, "style1")

            'Row2.Cells.Add(column.Name, DataType.String, "style1")
            'Worksheet.Cells(1, column.Index + 1).Value = column.Name
        Next
        'Export Header Name End
        'Dim style3 As WorksheetStyle = book.Styles.Add("style1")
        style = book.Styles.Add("style2")
        style.Font.Bold = False
        style.Font.Size = 9
        style.Alignment.Vertical = StyleVerticalAlignment.Top
        'style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        'Export Each Row Start
        'stylecol = book.Styles.Add("stylecol")
        For i As Integer = 0 To dg.Rows.Count - 1
            'Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
            Row = sheet.Table.Rows.Add
            Dim columnIndex As Integer = 0
            Do Until columnIndex = columnsCount - (1 - 1)
                If (columnIndex >= 4 And columnIndex <= 36) Then
                    If Val(dg.Item(1, i).Value & vbNullString) <= 6 Then
                        Row.Cells.Add(dg.Item(columnIndex, i).Value, DataType.Number, "style41")
                    Else
                        Row.Cells.Add(dg.Item(columnIndex, i).Value, DataType.Number, "style2")
                    End If
                ElseIf columnIndex = 0 Then
                    If mtotincent = False Then
                        Row.Cells.Add(Format(dg.Item(columnIndex, i).Value, "dd-MM-yyyy"), DataType.String, "style2")
                    Else
                        If Val(dg.Item(1, i).Value & vbNullString) = 0 Then
                            Row.Cells.Add(dg.Item(columnIndex, i).Value & vbNullString, DataType.String, "style41")
                        Else
                            Row.Cells.Add(dg.Item(columnIndex, i).Value & vbNullString, DataType.String, "style2")
                        End If
                    End If
                Else
                    If Val(dg.Item(1, i).Value & vbNullString) <= 6 Then
                        Row.Cells.Add(dg.Item(columnIndex, i).Value & vbNullString, DataType.String, "style41")
                    Else
                        Row.Cells.Add(dg.Item(columnIndex, i).Value & vbNullString, DataType.String, "style2")
                    End If
                End If
                'style.Interior.Color = System.Drawing.ColorTranslator.ToOle(dg.Rows(i).Cells(columnIndex).Style.BackColor)
                'style.Font.Color = System.Drawing.ColorTranslator.ToOle(dg.Rows(i).Cells(columnIndex).Style.ForeColor)
                    'Row2.Cells(i + 2, columnIndex + 1).Value = CTRL.Item(columnIndex, i).Value.ToString
                    columnIndex += 1
            Loop
        Next
        style.Font.Bold = False

        Row = sheet.Table.Rows.Add
        style = book.Styles.Add("style11")
        'style.Alignment.Vertical = StyleVerticalAlignment.Center
        'style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style.Font.Bold = True
        style.Font.Size = 11

        'Row = sheet.Table.Rows.Add
        Row.Cells.Add("", DataType.String, "style11")

        Row = sheet.Table.Rows.Add

        'Row.Cells.Add("", DataType.String, "style11")
        'Row.Cells.Add("", DataType.String, "style11")
        'Row.Cells.Add(Label7.Text, DataType.String, "style11")
        'Row.Cells.Add(lblman.Text, DataType.Number, "style11")
        'Row.Cells.Add("", DataType.String, "style11")
        'Row.Cells.Add(Label6.Text, DataType.String, "style11")
        'Row.Cells.Add(lbltotqty.Text, DataType.Number, "style11")
        'Row.Cells.Add("", DataType.String, "style11")
        'Row.Cells.Add(Label4.Text, DataType.String, "style11")
        'Row.Cells.Add(lbltotinc.Text, DataType.Number, "style11")
        'Row.Cells.Add("", DataType.String, "style11")
        'Row.Cells.Add(Label5.Text, DataType.String, "style11")
        'Row.Cells.Add(lbldsalary.Text, DataType.Number, "style11")


        '****************************************

        style.Font.Bold = False

        Dim sheet1 As Worksheet = book.Worksheets.Add("Line Incharge Incentive")
        'Dim style1 As WorksheetStyle = book.Styles.Add("style5")
        Dim Row1 As WorksheetRow = sheet1.Table.Rows.Add
        Dim style1 As WorksheetStyle = book.Styles.Add("Header11")
        style1.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style1.Font.Bold = True
        style1.Font.Size = 14
        style1.Alignment.Vertical = StyleVerticalAlignment.Center

        'Dim Row1 As WorksheetRow = sheet1.Table.Rows.Add
        'style.Font.Bold = True
        'style.Font.Size = 14
        'styleh.Alignment.Vertical = StyleVerticalAlignment.Center
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Row.Cells.Add(head1, DataType.String, "Header1")

        style1.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        Dim cell1 As WorksheetCell = Row1.Cells.Add("ATITHYA CLOTHING COMPANY")
        cell1.MergeAcross = 5
        cell1.StyleID = "Header11"

        'Dim stylh2 As WorksheetStyle = book.Styles.Add("Header2")
        'Dim Rh2 As WorksheetRow = sheet.Table.Rows.Add
        'Dim Row1 As WorksheetRow = sheet1.Table.Rows.Add
        Row1 = sheet1.Table.Rows.Add
        style1 = book.Styles.Add("Header22")
        style1.Alignment.Vertical = StyleVerticalAlignment.Center
        style1.Alignment.Horizontal = StyleHorizontalAlignment.Center

        style1.Font.Bold = True
        style1.Font.Size = 12
        style1.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        cell = Row1.Cells.Add("LINE INCHARGE INCENTIVE REPORT From " & Format(CDate(mskdatefr.Text), "dd-MM-yyyy") & " To " & Format(CDate(Mskdateto.Text), "dd-MM-yyyy"))
        cell.MergeAcross = 5
        cell.StyleID = "Header22"




        Row1 = sheet1.Table.Rows.Add

        style1 = book.Styles.Add("style5")
        style1.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style1.Font.Bold = True
        style1.Font.Size = 12
        style1.Alignment.Vertical = StyleVerticalAlignment.Center

        'Dim sheet1 As Worksheet = book.Worksheets.Add("Line Incharge Incentive")
        style1.Font.Bold = False
        style1 = book.Styles.Add("style6")
        style1.Font.Bold = False
        style1.Font.Size = 9
        style1.Alignment.Vertical = StyleVerticalAlignment.Top
        'style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style1.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style1.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)


        'Export Header Names Start
        Dim columnsCount1 As Integer = dg1.Columns.Count

        'Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
        'Dim column As Integer = 0
        Dim column1 = dg1.Columns.Count
        'Do Until column = columnsCount
        'For Each column In CTRL.Columns
        style1.Font.Bold = True
        For iC As Integer = 0 To column1 - 1
            Row1.Cells.Add(dg1.Columns(iC).HeaderText, DataType.String, "style5")
        Next
        'Export Header Name End
        'Dim style3 As WorksheetStyle = book.Styles.Add("style1")

        'Export Each Row Start
        For i As Integer = 0 To dg1.Rows.Count - 1
            'Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
            Row1 = sheet1.Table.Rows.Add
            Dim columnIndex As Integer = 0
            Do Until columnIndex = columnsCount1 - (1 - 1)
                If (columnIndex >= 3 And columnIndex <= 34) Then
                    Row1.Cells.Add(dg1.Item(columnIndex, i).Value & vbNullString, DataType.Number, "style6")
                Else

                    Row1.Cells.Add(dg1.Item(columnIndex, i).Value & vbNullString, DataType.String, "style6")
                End If
                'Row2.Cells(i + 2, columnIndex + 1).Value = CTRL.Item(columnIndex, i).Value.ToString
                columnIndex += 1
            Loop
        Next

        Row1 = sheet1.Table.Rows.Add
        style1 = book.Styles.Add("style12")
        'style.Alignment.Vertical = StyleVerticalAlignment.Center
        'style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style1.Font.Bold = True
        style1.Font.Size = 11

        'Row = sheet.Table.Rows.Add
        ' Row1.Cells.Add("", DataType.String, "style12")

        Row1 = sheet1.Table.Rows.Add

        Row1.Cells.Add(Label8.Text, DataType.String, "style12")
        Row1.Cells.Add(lbldg1man.Text, DataType.Number, "style12")
        Row1.Cells.Add("", DataType.String, "style12")
        Row1.Cells.Add(Label10.Text, DataType.String, "style12")
        Row1.Cells.Add(lbldg1inc.Text, DataType.Number, "style12")



        '********************************************** Floor QC Line incharge
        '**********************************************



        Dim sheet2 As Worksheet = book.Worksheets.Add("FLOOR Incharge Incentive")
        'Dim style1 As WorksheetStyle = book.Styles.Add("style5")
        Dim Row2 As WorksheetRow = sheet2.Table.Rows.Add
        Dim style2 As WorksheetStyle = book.Styles.Add("Header12")
        style2.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style2.Font.Bold = True
        style2.Font.Size = 14
        style2.Alignment.Vertical = StyleVerticalAlignment.Center

        'Dim Row1 As WorksheetRow = sheet1.Table.Rows.Add
        'style.Font.Bold = True
        'style.Font.Size = 14
        'styleh.Alignment.Vertical = StyleVerticalAlignment.Center
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Row.Cells.Add(head1, DataType.String, "Header1")

        style2.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        Dim cell2 As WorksheetCell = Row2.Cells.Add("ATITHYA CLOTHING COMPANY")
        cell2.MergeAcross = 4
        cell2.StyleID = "Header12"

        'Dim stylh2 As WorksheetStyle = book.Styles.Add("Header2")
        'Dim Rh2 As WorksheetRow = sheet.Table.Rows.Add
        'Dim Row1 As WorksheetRow = sheet1.Table.Rows.Add
        Row2 = sheet2.Table.Rows.Add
        style2 = book.Styles.Add("Header23")
        style2.Alignment.Vertical = StyleVerticalAlignment.Center
        style2.Alignment.Horizontal = StyleHorizontalAlignment.Center

        style2.Font.Bold = True
        style2.Font.Size = 12
        style2.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        cell2 = Row2.Cells.Add("FLOOR/QC INCHARGE INCENTIVE REPORT From " & Format(CDate(mskdatefr.Text), "dd-MM-yyyy") & " To " & Format(CDate(Mskdateto.Text), "dd-MM-yyyy"))
        cell2.MergeAcross = 4
        cell2.StyleID = "Header23"




        Row2 = sheet2.Table.Rows.Add

        style2 = book.Styles.Add("style55")
        style2.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style2.Font.Bold = True
        style2.Font.Size = 12
        style2.Alignment.Vertical = StyleVerticalAlignment.Center

        'Dim sheet1 As Worksheet = book.Worksheets.Add("Line Incharge Incentive")

        style2 = book.Styles.Add("style66")
        style2.Font.Bold = False
        style2.Font.Size = 9
        style2.Alignment.Vertical = StyleVerticalAlignment.Top
        'style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style2.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)


        'Export Header Names Start
        Dim columnsCount2 As Integer = dg2.Columns.Count

        'Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
        'Dim column As Integer = 0
        Dim column2 = dg2.Columns.Count
        'Do Until column = columnsCount
        'For Each column In CTRL.Columns
        style2.Font.Bold = True
        For iC As Integer = 0 To column2 - 1
            Row2.Cells.Add(dg2.Columns(iC).HeaderText, DataType.String, "style55")
        Next

        'Export Header Name End
        'Dim style3 As WorksheetStyle = book.Styles.Add("style1")

        'Export Each Row Start
        For i As Integer = 0 To dg2.Rows.Count - 1
            'Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
            Row2 = sheet2.Table.Rows.Add
            Dim columnIndex As Integer = 0
            Do Until columnIndex = columnsCount2 - (1 - 1)
                If (columnIndex >= 3 And columnIndex <= 4) Or columnIndex = 1 Then
                    Row2.Cells.Add(dg2.Item(columnIndex, i).Value, DataType.Number, "style66")
                Else
                    Row2.Cells.Add(dg2.Item(columnIndex, i).Value.ToString, DataType.String, "style66")
                End If
                'Row2.Cells(i + 2, columnIndex + 1).Value = CTRL.Item(columnIndex, i).Value.ToString
                columnIndex += 1
            Loop
        Next

        Row2 = sheet2.Table.Rows.Add
        style2 = book.Styles.Add("style122")
        'style.Alignment.Vertical = StyleVerticalAlignment.Center
        'style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style2.Font.Bold = True
        style2.Font.Size = 11

        'Row = sheet.Table.Rows.Add
        ' Row1.Cells.Add("", DataType.String, "style12")

        Row2 = sheet2.Table.Rows.Add

        Row2.Cells.Add(Label12.Text, DataType.String, "style122")
        Row2.Cells.Add(lbldg2man.Text, DataType.Number, "style122")
        Row2.Cells.Add("", DataType.String, "style122")
        Row2.Cells.Add(Label14.Text, DataType.String, "style122")
        Row2.Cells.Add(lbldg2inc.Text, DataType.Number, "style122")



        book.Save(lmdir)
        'open file
        If mos = "WIN" Then
            Process.Start(lmdir)
        Else
            OpenWithLibreOffice(lmdir)
        End If

    End Sub




    'dqry = "select b.date,b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, c.jobgrade, b.totprodqty,b.totrejqty,b.nomac, (b.nomac*d.nopcs) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr," & vbCrLf _
    '     & "(b.totprodqty-(b.nomac*d.nopcs)) as incqty, isnull(f.incentive,0) incentive, " & vbCrLf _
    '& " case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*f.incentive),0) else 0 end amt,isnull(em.salary,0) salary," & vbCrLf _
    '& "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
    '& " (datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
    '& "SUM(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*f.incentive),0) else 0 end) over (partition by b.[lineno]) as totincentive, " & vbCrLf _
    '& " sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) " & vbCrLf _
    '& " from " & Trim(mcostdbnam) & ".dbo.operf b " & vbCrLf _
    '& " left join (select bno, empid,empno,jobgrade,[lineno] from " & Trim(mcostdbnam) & ".dbo.perf1 where incyn not in ('N') group by empid,empno,jobgrade,bno,[lineno]) c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf _
    '& " left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) d on d.brand=b.brand " & vbCrLf _
    '& " Left Join " & vbCrLf _
    '& " (select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b " & vbCrLf _
    '& " Left Join " & vbCrLf _
    '& " (select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
    '& "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=c.jobgrade " & vbCrLf _
    '& " left join " & Trim(mcostdbnam) & ".dbo.empmaster em on em.nemp_id=c.empid  " & vbCrLf
    '& "where   (b.totprodqty-(b.nomac*d.nopcs)) > 0 and date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' "
    ' If Len(Trim(cmbline.Text)) > 0 Then
    '     dqry = dqry + " and b.[lineno]='" & Trim(cmbline.Text) & "'"
    ' End If
    ' dqry = dqry + " order by b.date,b.[lineno],c.jobgrade "
    '& "where  case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then  (b.totprodqty-(b.nomac*d.nopcs)) else 0 end  between f.nprodpcsfr and f.nprodpcsto and date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' "

    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbldg1sal.Click

    End Sub

    Private Sub Label17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbldg2sal.Click

    End Sub

    Private Sub butdup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butdup.Click
        'For x As Integer = 0 To dg.Rows.Count - 1
        '    For y As Integer = x + 1 To dg.Rows.Count - 1
        '        If dg.Rows(x).Cells(0).Value.ToString = dg.Rows(y).Cells(0).Value.ToString Then
        '            MsgBox("Duplicate Data!")
        '            'Exit Sub
        '        Else
        '            'save_data()
        '            Me.Close()
        '        End If
        '    Next
        'Next
        dg.Sort(dg.Columns(5), System.ComponentModel.ListSortDirection.Ascending)

        For i = 0 To dg.RowCount - 2
            For j = i + 1 To dg.RowCount - 1
                Dim row2 = dg.Rows(j)

                If Not row2.IsNewRow Then
                    Dim row1 = dg.Rows(i)

                    If row1.Cells(5).Value.ToString() = row2.Cells(5).Value.ToString() And row1.Cells(3).Value.ToString() = row2.Cells(3).Value.ToString() Then
                        row1.DefaultCellStyle.BackColor = Color.LightGreen
                        row2.DefaultCellStyle.BackColor = Color.LightGreen
                    End If
                End If
            Next
        Next

    End Sub
    Private Sub calctotinc()
        Dim s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16, s17, s18, s19, s20, s21, s22, s23, s24, s25, s26, s27, s28, s29, s30, s31, mtot As Double
        'MSQL = "select lk.[lineno],lk.empno,lk.empname,lk.jobgrade," & vbCrLf _
        '  & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
        '  & "( [1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22]+[23]+[24]+[25]+[26]+[27]+[28]+[29]+[30]+[31]) total from " & vbCrLf

        ' MSQL = "select lk.[lineno],lk.empno,lk.empname,lk.jobgrade," & vbCrLf _
        '          & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
        '         & "(case when [1]>0 then [1] else 0 end + " & vbCrLf _
        '         & "case when [2]>0 then [2] else 0 end +  " & vbCrLf _
        '         & " case when [3]>0 then [3] else 0 end +  " & vbCrLf _
        '         & " case when [4]>0 then [4] else 0 end +  " & vbCrLf _
        '         & " case when [5]>0 then [5] else 0 end +  " & vbCrLf _
        '         & " case when [6]>0 then [6] else 0 end +case when [7]>0 then [7] else 0 end +case when [8]>0 then [8] else 0 end +  " & vbCrLf _
        '         & " case when [9]>0 then [9] else 0 end + case when [10]>0 then [10] else 0 end +case when [11]>0 then [11] else 0 end +case when [12]>0 then [12] else 0 end +  " & vbCrLf _
        '         & " case when [13]>0 then [13] else 0 end +case when [14]>0 then [14] else 0 end +case when [15]>0 then [15] else 0 end +case when [16]>0 then [16] else 0 end +  " & vbCrLf _
        '         & " case when [17]>0 then [17] else 0 end + case when [18]>0 then [18] else 0 end + case when [19]>0 then [19] else 0 end + case when [20]>0 then [20] else 0 end +  " & vbCrLf _
        '         & " case when [21]>0 then [21] else 0 end + case when [22]>0 then [22] else 0 end + case when [23]>0 then [23] else 0 end + case when [24]>0 then [24] else 0 end + case when [25]>0 then [25] else 0 end +  " & vbCrLf _
        '         & " case when [26]>0 then [26] else 0 end + case when [27]>0 then [27] else 0 end + case when [28]>0 then [28] else 0 end + case when [29]>0 then [29] else 0 end +  " & vbCrLf _
        '         & " case when [30]>0 then [30] else 0 end + case when [31]>0 then [31] else 0 end ) as total from " & vbCrLf




        ' MSQL = MSQL & "(select 1 sno,l.[Lineno],'' empno,''empname,l.ptype jobgrade, sum(l.[1]) [1],sum(l.[2]) [2] ,sum(l.[3]) [3],sum(l.[4]) [4],sum(l.[5]) [5],sum(l.[6]) [6],sum(l.[7]) [7], " & vbCrLf _
        '& " sum(l.[8]) [8],sum(l.[9]) [9],sum(l.[10]) [10],sum(l.[11]) [11],sum(l.[12]) [12],sum(l.[13]) [13],sum(l.[14]) [14],  " & vbCrLf _
        '& "sum(l.[15]) [15],sum(l.[16]) [16],sum(l.[17]) [17],sum(l.[18]) [18],sum(l.[19]) [19],sum(l.[20]) [20],sum(l.[21]) [21],  " & vbCrLf _
        '& "sum(l.[22]) [22],sum(l.[23]) [23],sum(l.[24]) [24],sum(l.[25]) [25],sum(l.[26]) [26],sum(l.[27]) [27],  " & vbCrLf _
        '& "sum(l.[28]) [28],sum(l.[29]) [29],sum(l.[30]) [30],sum(l.[31]) [31],0 totinc  from  " & vbCrLf _
        '& "(select [Lineno],ptype, isnull([1],0) [1],isnull([2],0) [2] ,ISNULL([3],0) [3],isnull([4],0) [4],isnull([5],0) [5],isnull([6],0) [6],isnull([7],0) [7],  " & vbCrLf _
        '& "isnull([8],0) [8],isnull([9],0) [9],isnull([10],0) [10],isnull([11],0) [11],isnull([12],0) [12],isnull([13],0) [13],isnull([14],0) [14],  " & vbCrLf _
        '& "isnull([15],0) [15],isnull([16],0) [16],isnull([17],0) [17],isnull([18],0) [18],isnull([19],0) [19],isnull([20],0) [20],isnull([21],0) [21],  " & vbCrLf _
        '& "isnull([22],0) [22],isnull([23],0) [23],isnull([24],0) [24],isnull([25],0) [25],isnull([26],0) [26],isnull([27],0) [27],  " & vbCrLf _
        '& " isnull([28],0) [28],isnull([29],0) [29],isnull([30],0) [30],isnull([31],0) [31]  from  " & vbCrLf _
        '& "(select k.dat,k.[lineno],k.brand,'ProdQty' ptype, isnull(sum(k.totprodqty),0) totprodqty from   " & vbCrLf _
        '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
        '& "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        '& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        '& " union all  " & vbCrLf _
        '& "select k.dat,k.[lineno],k.brand,'TargetQty' ptype, isnull(sum(k.target),0) target from   " & vbCrLf _
        '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
        '& "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        '& "group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        '& " union all  " & vbCrLf _
        '& " select k.dat,k.[lineno],k.brand,'ZExcessQty' ptype, isnull(sum(k.exqty),0) exqty from   " & vbCrLf _
        '& " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
        '& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        '& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        '& " union all  " & vbCrLf _
        '& " select k.dat,k.[lineno],k.brand,'Machine' ptype, isnull(sum(k.nomac),0) exqty from   " & vbCrLf _
        '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
        '& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        '& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        '& " union all  " & vbCrLf _
        '& " select k.dat,k.[lineno],k.brand,'Nopcs' ptype, isnull(sum(k.nopcs),0) exqty from   " & vbCrLf _
        '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,c.nopcs, b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
        '& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        '& " group by k.dat,k.[lineno],k.brand ) s  " & vbCrLf _
        '& " pivot (sum(totprodqty) FOR [dat] IN  " & vbCrLf _
        '& " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) Pv1) l  " & vbCrLf _
        '& " group by l.[Lineno],l.ptype  " & vbCrLf


        ' ' '*****
        ' MSQL = MSQL & " union all select 2 sno, [lineno],empno,empname,jobgrade, " & vbCrLf _
        '    & " isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6]," & vbCrLf _
        '    & " isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12]," & vbCrLf _
        '    & " isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18]," & vbCrLf _
        '    & "isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24]," & vbCrLf _
        '    & " isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30]," & vbCrLf _
        '    & " isnull([31],0) as [31]," & vbCrLf _
        '    & " (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+ " & vbCrLf _
        '    & "isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+ " & vbCrLf _
        '    & "isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+ " & vbCrLf _
        '    & " isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+ " & vbCrLf _
        '    & " isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from " & vbCrLf

        ' MSQL = MSQL & "(select s.[Lineno],s.empno,s.empname,s.jobgrade,SUM(s.amt) amt, s.dat from " & vbCrLf _
        '         & "(select b.date,DATEPART(d,b.date) dat, b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr," & vbCrLf _
        '         & "isnull((b.totprodqty-(b.nomac*d.nopcs)),0) as incqty, round((isnull(f.incentive,0)*c.pper)/100,2)  incentive, " & vbCrLf _
        '         & "isnull(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))* round((f.incentive*c.pper)/100,2)),0) else 0 end,0) amt,isnull(em.salary,0) salary," & vbCrLf _
        '         & "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
        '         & "(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
        '         & "SUM(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*f.incentive),0) else 0 end) over (partition by b.[lineno]) as totincentive, " & vbCrLf _
        '         & "sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary " & vbCrLf _
        '         & "from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)" & vbCrLf _
        '         & "left join (select l.bno,l.empid,l.empno,l.jobgrade,l.[lineno],l.pper from  " & vbCrLf _
        '         & "(select bno, empid,empno,jobgrade,[lineno],opername,totprod,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade) mprod ,COUNT(empno) over(partition by bno,[lineno],jobgrade,empno) cnt,  " & vbCrLf _
        '         & "case when totprod>0 and COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)<=1 then round((convert(real,totprod)/convert(real,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade)))*100,0)  " & vbCrLf _
        '         & "else case  when COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)>1 then 100 else  0 end end pper,incyn    from " & Trim(mcostdbnam) & ".dbo.perf1 with (nolock)) l  " & vbCrLf _
        '         & "where incyn not in ('N'))  c on c.bno=b.bno and c.[lineno]=b.[lineno]  left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) d on d.brand=b.brand  " & vbCrLf _
        '         & "left join " & Trim(mcostdbnam) & ".dbo.empmaster em with (nolock) on em.nemp_id=c.empid  " & vbCrLf _
        '         & "Left Join" & vbCrLf _
        '         & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b with (nolock) " & vbCrLf _
        '         & " Left Join " & vbCrLf _
        '         & "(select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
        '         & "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade " & vbCrLf _
        '         & "where    date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'   and (b.totprodqty-(b.nomac*d.nopcs)) > 0  ) s " & vbCrLf _
        '       & "group by s.[Lineno],s.empno,s.empname,s.jobgrade,s.dat ) k " & vbCrLf

        ' MSQL = MSQL & "  PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
        '   & "([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P  ) lk " & vbCrLf _
        '   & " order by  lk.[lineno],lk.sno,lk.jobgrade,lk.empno"

        ' '--New -rw
        ' MSQL = "select lk.[lineno],lk.empno,lk.empname,lk.jobgrade," & vbCrLf _
        '         & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
        '        & "(case when [1]>0 then [1] else 0 end + " & vbCrLf _
        '        & "case when [2]>0 then [2] else 0 end +  " & vbCrLf _
        '        & " case when [3]>0 then [3] else 0 end +  " & vbCrLf _
        '        & " case when [4]>0 then [4] else 0 end +  " & vbCrLf _
        '        & " case when [5]>0 then [5] else 0 end +  " & vbCrLf _
        '        & " case when [6]>0 then [6] else 0 end +case when [7]>0 then [7] else 0 end +case when [8]>0 then [8] else 0 end +  " & vbCrLf _
        '        & " case when [9]>0 then [9] else 0 end + case when [10]>0 then [10] else 0 end +case when [11]>0 then [11] else 0 end +case when [12]>0 then [12] else 0 end +  " & vbCrLf _
        '        & " case when [13]>0 then [13] else 0 end +case when [14]>0 then [14] else 0 end +case when [15]>0 then [15] else 0 end +case when [16]>0 then [16] else 0 end +  " & vbCrLf _
        '        & " case when [17]>0 then [17] else 0 end + case when [18]>0 then [18] else 0 end + case when [19]>0 then [19] else 0 end + case when [20]>0 then [20] else 0 end +  " & vbCrLf _
        '        & " case when [21]>0 then [21] else 0 end + case when [22]>0 then [22] else 0 end + case when [23]>0 then [23] else 0 end + case when [24]>0 then [24] else 0 end + case when [25]>0 then [25] else 0 end +  " & vbCrLf _
        '        & " case when [26]>0 then [26] else 0 end + case when [27]>0 then [27] else 0 end + case when [28]>0 then [28] else 0 end + case when [29]>0 then [29] else 0 end +  " & vbCrLf _
        '        & " case when [30]>0 then [30] else 0 end + case when [31]>0 then [31] else 0 end ) as total from " & vbCrLf




        ' MSQL = MSQL & "(select 1 sno,l.[Lineno],'' empno,''empname,l.ptype jobgrade, sum(l.[1]) [1],sum(l.[2]) [2] ,sum(l.[3]) [3],sum(l.[4]) [4],sum(l.[5]) [5],sum(l.[6]) [6],sum(l.[7]) [7], " & vbCrLf _
        '& " sum(l.[8]) [8],sum(l.[9]) [9],sum(l.[10]) [10],sum(l.[11]) [11],sum(l.[12]) [12],sum(l.[13]) [13],sum(l.[14]) [14],  " & vbCrLf _
        '& "sum(l.[15]) [15],sum(l.[16]) [16],sum(l.[17]) [17],sum(l.[18]) [18],sum(l.[19]) [19],sum(l.[20]) [20],sum(l.[21]) [21],  " & vbCrLf _
        '& "sum(l.[22]) [22],sum(l.[23]) [23],sum(l.[24]) [24],sum(l.[25]) [25],sum(l.[26]) [26],sum(l.[27]) [27],  " & vbCrLf _
        '& "sum(l.[28]) [28],sum(l.[29]) [29],sum(l.[30]) [30],sum(l.[31]) [31],0 totinc  from  " & vbCrLf _
        '& "(select [Lineno],ptype, isnull([1],0) [1],isnull([2],0) [2] ,ISNULL([3],0) [3],isnull([4],0) [4],isnull([5],0) [5],isnull([6],0) [6],isnull([7],0) [7],  " & vbCrLf _
        '& "isnull([8],0) [8],isnull([9],0) [9],isnull([10],0) [10],isnull([11],0) [11],isnull([12],0) [12],isnull([13],0) [13],isnull([14],0) [14],  " & vbCrLf _
        '& "isnull([15],0) [15],isnull([16],0) [16],isnull([17],0) [17],isnull([18],0) [18],isnull([19],0) [19],isnull([20],0) [20],isnull([21],0) [21],  " & vbCrLf _
        '& "isnull([22],0) [22],isnull([23],0) [23],isnull([24],0) [24],isnull([25],0) [25],isnull([26],0) [26],isnull([27],0) [27],  " & vbCrLf _
        '& " isnull([28],0) [28],isnull([29],0) [29],isnull([30],0) [30],isnull([31],0) [31]  from  " & vbCrLf _
        '& "(select k.dat,k.[lineno],k.brand,'ProdQty' ptype, isnull(sum(k.totprodqty),0) totprodqty from   " & vbCrLf _
        '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
        '& "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        '& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        '& " union all  " & vbCrLf _
        '& "select k.dat,k.[lineno],k.brand,'TargetQty' ptype, isnull(sum(k.target),0) target from   " & vbCrLf _
        '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
        '& "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        '& "group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        '& " union all  " & vbCrLf _
        '& " select k.dat,k.[lineno],k.brand,'ZExcessQty' ptype, isnull(sum(k.exqty),0) exqty from   " & vbCrLf _
        '& " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
        '& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        '& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        '& " union all  " & vbCrLf _
        '& " select k.dat,k.[lineno],k.brand,'Machine' ptype, isnull(sum(k.nomac),0) exqty from   " & vbCrLf _
        '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
        '& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        '& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        '& " union all  " & vbCrLf _
        '& " select k.dat,k.[lineno],k.brand,'Nopcs' ptype, isnull(sum(k.nopcs),0) exqty from   " & vbCrLf _
        '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,c.nopcs, b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
        '& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        '& " group by k.dat,k.[lineno],k.brand ) s  " & vbCrLf _
        '& " pivot (sum(totprodqty) FOR [dat] IN  " & vbCrLf _
        '& " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) Pv1) l  " & vbCrLf _
        '& " group by l.[Lineno],l.ptype  " & vbCrLf


        ' '*****
        ' MSQL = MSQL & " union all select 2 sno, [lineno],empno,empname,jobgrade, " & vbCrLf _
        '    & " isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6]," & vbCrLf _
        '    & " isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12]," & vbCrLf _
        '    & " isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18]," & vbCrLf _
        '    & "isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24]," & vbCrLf _
        '    & " isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30]," & vbCrLf _
        '    & " isnull([31],0) as [31]," & vbCrLf _
        '    & " (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+ " & vbCrLf _
        '    & "isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+ " & vbCrLf _
        '    & "isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+ " & vbCrLf _
        '    & " isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+ " & vbCrLf _
        '    & " isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from " & vbCrLf

        ' MSQL = MSQL & "(select s.[Lineno],s.empno,s.empname,s.jobgrade,SUM(s.amt) amt, s.dat from " & vbCrLf _
        '         & "(select b.date,DATEPART(d,b.date) dat, b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr," & vbCrLf _
        '         & "isnull((b.totprodqty-(b.nomac*d.nopcs)),0) as incqty, round(isnull(c.incentiveamt,0),2) as incentive,  " & vbCrLf _
        '         & "isnull(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))* c.incentiveamt),2) else 0 end,0) amt,isnull(em.salary,0) salary," & vbCrLf _
        '         & "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
        '         & "(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
        '         & "SUM(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*c.incentiveamt),0) else 0 end) over (partition by b.date,b.[lineno]) as totincentive, " & vbCrLf _
        '         & "sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary " & vbCrLf _
        '         & "from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)" & vbCrLf

        ' MSQL = MSQL & "left join (select j.brand,j.bno,j.empid,j.empno,j.[lineno],SUM(j.incentiveamt) incentiveamt from (" & vbCrLf _
        '             & " select c.brand, b.bno, b.empid,b.empno,b.jobgrade,b.[lineno],b.opername,b.totprod,b.totmin, (b.totmin/convert(real,480))*100 perc, " & vbCrLf _
        '             & " (b.totmin/convert(real,480))*100 pper,  case when isnull(b.totmin,0)>0 then round(i.incentive* (b.totmin/convert(real,480)),2) else 0 end incentiveamt, " & vbCrLf _
        '             & " i.incentive, b.incyn    from prodcost.dbo.perf1 b " & vbCrLf _
        '             & " left join prodcost.dbo.operf c on c.bno=b.bno and c.date=b.date " & vbCrLf _
        '             & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade)j " & vbCrLf _
        '             & " group by j.brand,j.bno,j.empid,j.empno,j.[lineno])   c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf

        ' MSQL = MSQL & " left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) d on d.brand=b.brand  " & vbCrLf _
        '         & "left join " & Trim(mcostdbnam) & ".dbo.empmaster em with (nolock) on em.nemp_id=c.empid  " & vbCrLf _
        '         & "Left Join" & vbCrLf _
        '         & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b with (nolock) " & vbCrLf _
        '         & " Left Join " & vbCrLf _
        '         & "(select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
        '         & "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade " & vbCrLf _
        '         & "where    date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'   and (b.totprodqty-(b.nomac*d.nopcs)) > 0  ) s " & vbCrLf _
        '       & "group by s.[Lineno],s.empno,s.empname,s.jobgrade,s.dat ) k " & vbCrLf

        ' MSQL = MSQL & "  PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
        '   & "([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P  ) lk " & vbCrLf _
        '   & " order by  lk.[lineno],lk.sno,lk.jobgrade,lk.empno"


        ''--NEw Rework
        ' MSQL = "select lk.[lineno],lk.empno,lk.empname,lk.jobgrade," & vbCrLf _
        '         & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
        '        & "(case when [1]>0 then [1] else 0 end + " & vbCrLf _
        '        & "case when [2]>0 then [2] else 0 end +  " & vbCrLf _
        '        & " case when [3]>0 then [3] else 0 end +  " & vbCrLf _
        '        & " case when [4]>0 then [4] else 0 end +  " & vbCrLf _
        '        & " case when [5]>0 then [5] else 0 end +  " & vbCrLf _
        '        & " case when [6]>0 then [6] else 0 end +case when [7]>0 then [7] else 0 end +case when [8]>0 then [8] else 0 end +  " & vbCrLf _
        '        & " case when [9]>0 then [9] else 0 end + case when [10]>0 then [10] else 0 end +case when [11]>0 then [11] else 0 end +case when [12]>0 then [12] else 0 end +  " & vbCrLf _
        '        & " case when [13]>0 then [13] else 0 end +case when [14]>0 then [14] else 0 end +case when [15]>0 then [15] else 0 end +case when [16]>0 then [16] else 0 end +  " & vbCrLf _
        '        & " case when [17]>0 then [17] else 0 end + case when [18]>0 then [18] else 0 end + case when [19]>0 then [19] else 0 end + case when [20]>0 then [20] else 0 end +  " & vbCrLf _
        '        & " case when [21]>0 then [21] else 0 end + case when [22]>0 then [22] else 0 end + case when [23]>0 then [23] else 0 end + case when [24]>0 then [24] else 0 end + case when [25]>0 then [25] else 0 end +  " & vbCrLf _
        '        & " case when [26]>0 then [26] else 0 end + case when [27]>0 then [27] else 0 end + case when [28]>0 then [28] else 0 end + case when [29]>0 then [29] else 0 end +  " & vbCrLf _
        '        & " case when [30]>0 then [30] else 0 end + case when [31]>0 then [31] else 0 end ) as total from " & vbCrLf




        ' MSQL = MSQL & "(select 1 sno,l.[Lineno],'' empno,''empname,l.ptype jobgrade, sum(l.[1]) [1],sum(l.[2]) [2] ,sum(l.[3]) [3],sum(l.[4]) [4],sum(l.[5]) [5],sum(l.[6]) [6],sum(l.[7]) [7], " & vbCrLf _
        '& " sum(l.[8]) [8],sum(l.[9]) [9],sum(l.[10]) [10],sum(l.[11]) [11],sum(l.[12]) [12],sum(l.[13]) [13],sum(l.[14]) [14],  " & vbCrLf _
        '& "sum(l.[15]) [15],sum(l.[16]) [16],sum(l.[17]) [17],sum(l.[18]) [18],sum(l.[19]) [19],sum(l.[20]) [20],sum(l.[21]) [21],  " & vbCrLf _
        '& "sum(l.[22]) [22],sum(l.[23]) [23],sum(l.[24]) [24],sum(l.[25]) [25],sum(l.[26]) [26],sum(l.[27]) [27],  " & vbCrLf _
        '& "sum(l.[28]) [28],sum(l.[29]) [29],sum(l.[30]) [30],sum(l.[31]) [31],0 totinc  from  " & vbCrLf _
        '& "(select [Lineno],ptype, isnull([1],0) [1],isnull([2],0) [2] ,ISNULL([3],0) [3],isnull([4],0) [4],isnull([5],0) [5],isnull([6],0) [6],isnull([7],0) [7],  " & vbCrLf _
        '& "isnull([8],0) [8],isnull([9],0) [9],isnull([10],0) [10],isnull([11],0) [11],isnull([12],0) [12],isnull([13],0) [13],isnull([14],0) [14],  " & vbCrLf _
        '& "isnull([15],0) [15],isnull([16],0) [16],isnull([17],0) [17],isnull([18],0) [18],isnull([19],0) [19],isnull([20],0) [20],isnull([21],0) [21],  " & vbCrLf _
        '& "isnull([22],0) [22],isnull([23],0) [23],isnull([24],0) [24],isnull([25],0) [25],isnull([26],0) [26],isnull([27],0) [27],  " & vbCrLf _
        '& " isnull([28],0) [28],isnull([29],0) [29],isnull([30],0) [30],isnull([31],0) [31]  from  " & vbCrLf _
        '& "(select k.dat,k.[lineno],k.brand,'ProdQty' ptype, isnull(sum(k.totprodqty),0) totprodqty from   " & vbCrLf _
        '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,(b.totprodqty-isnull(b.totrejqty,0)) totprodqty,b.nomac,b.nomac*c.nopcs target,((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
        '& "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        '& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        '& " union all  " & vbCrLf _
        '& "select k.dat,k.[lineno],k.brand,'TargetQty' ptype, isnull(sum(k.target),0) target from   " & vbCrLf _
        '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,(b.totprodqty-isnull(b.totrejqty,0)) totprodqty,b.nomac,b.nomac*c.nopcs target,((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
        '& "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        '& "group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        '& " union all  " & vbCrLf _
        '& " select k.dat,k.[lineno],k.brand,'ZExcessQty' ptype, isnull(sum(k.exqty),0) exqty from   " & vbCrLf _
        '& " (select datepart(d,b.date) dat, b.[lineno],b.brand,(b.totprodqty-isnull(b.totrejqty,0)) totprodqty,b.nomac,b.nomac*c.nopcs target,((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
        '& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        '& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        '& " union all  " & vbCrLf _
        '& " select k.dat,k.[lineno],k.brand,'Machine' ptype, isnull(sum(k.nomac),0) exqty from   " & vbCrLf _
        '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,(b.totprodqty-isnull(b.totrejqty,0)) totprodqty,b.nomac,b.nomac*c.nopcs target,((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
        '& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        '& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        '& " union all  " & vbCrLf _
        '& " select k.dat,k.[lineno],k.brand,'Nopcs' ptype, isnull(sum(k.nopcs),0) exqty from   " & vbCrLf _
        '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,(b.totprodqty-isnull(b.totrejqty,0)) totprodqty,b.nomac,c.nopcs, b.nomac*c.nopcs target,((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
        '& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        '& " group by k.dat,k.[lineno],k.brand ) s  " & vbCrLf _
        '& " pivot (sum(totprodqty) FOR [dat] IN  " & vbCrLf _
        '& " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) Pv1) l  " & vbCrLf _
        '& " group by l.[Lineno],l.ptype  " & vbCrLf


        ' '*****
        ' MSQL = MSQL & " union all select 2 sno, [lineno],empno,empname,jobgrade, " & vbCrLf _
        '    & " isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6]," & vbCrLf _
        '    & " isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12]," & vbCrLf _
        '    & " isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18]," & vbCrLf _
        '    & "isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24]," & vbCrLf _
        '    & " isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30]," & vbCrLf _
        '    & " isnull([31],0) as [31]," & vbCrLf _
        '    & " (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+ " & vbCrLf _
        '    & "isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+ " & vbCrLf _
        '    & "isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+ " & vbCrLf _
        '    & " isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+ " & vbCrLf _
        '    & " isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from " & vbCrLf

        ' MSQL = MSQL & "(select s.[Lineno],s.empno,s.empname,s.jobgrade,SUM(s.amt) amt, s.dat from " & vbCrLf _
        '         & "(select b.date,DATEPART(d,b.date) dat, b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr," & vbCrLf _
        '         & "isnull(((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*d.nopcs)),0) as incqty, round(isnull(c.incentiveamt,0),2) as incentive,  " & vbCrLf _
        '         & "isnull(case when ((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*d.nopcs)) > 0 then round((((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*d.nopcs))* c.incentiveamt),2) else 0 end,0) amt,isnull(em.salary,0) salary," & vbCrLf _
        '         & "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
        '         & "(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
        '         & "SUM(case when ((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*d.nopcs)) > 0 then round((((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*d.nopcs))*c.incentiveamt),0) else 0 end) over (partition by b.date,b.[lineno]) as totincentive, " & vbCrLf _
        '         & "sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary " & vbCrLf _
        '         & "from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)" & vbCrLf

        ' MSQL = MSQL & "left join (select j.brand,j.bno,j.empid,j.empno,j.[lineno],SUM(j.incentiveamt) incentiveamt from (" & vbCrLf _
        '             & " select c.brand, b.bno, b.empid,b.empno,b.jobgrade,b.[lineno],b.opername,b.totprod,b.totmin, (b.totmin/convert(real,480))*100 perc, " & vbCrLf _
        '             & " (b.totmin/convert(real,480))*100 pper,  case when isnull(b.totmin,0)>0 then round(i.incentive* (b.totmin/convert(real,480)),2) else 0 end incentiveamt, " & vbCrLf _
        '             & " i.incentive, b.incyn    from prodcost.dbo.perf1 b " & vbCrLf _
        '             & " left join prodcost.dbo.operf c on c.bno=b.bno and c.date=b.date " & vbCrLf _
        '             & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade)j " & vbCrLf _
        '             & " group by j.brand,j.bno,j.empid,j.empno,j.[lineno])   c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf

        ' MSQL = MSQL & " left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) d on d.brand=b.brand  " & vbCrLf _
        '         & "left join " & Trim(mcostdbnam) & ".dbo.empmaster em with (nolock) on em.nemp_id=c.empid  " & vbCrLf _
        '         & "Left Join" & vbCrLf _
        '         & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b with (nolock) " & vbCrLf _
        '         & " Left Join " & vbCrLf _
        '         & "(select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
        '         & "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade " & vbCrLf _
        '         & "where    date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'   and (b.totprodqty-(b.nomac*d.nopcs)) > 0  ) s " & vbCrLf _
        '       & "group by s.[Lineno],s.empno,s.empname,s.jobgrade,s.dat ) k " & vbCrLf

        ' MSQL = MSQL & "  PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
        '   & "([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P  ) lk " & vbCrLf _
        '   & " order by  lk.[lineno],lk.sno,lk.jobgrade,lk.empno"




        If Chkmerge.Checked = True Then
            '' new mergeline qry
            MSQL = " select lk.[lineno],lk.empno,lk.empname,lk.jobgrade, " & vbCrLf _
                   & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
                   & " (case when [1]>0 then [1] else 0 end + case when [2]>0 then [2] else 0 end + case when [3]>0 then [3] else 0 end +  " & vbCrLf _
                   & " case when [4]>0 then [4] else 0 end +  case when [5]>0 then [5] else 0 end + case when [6]>0 then [6] else 0 end + " & vbCrLf _
                   & " case when [7]>0 then [7] else 0 end +case when [8]>0 then [8] else 0 end +  " & vbCrLf _
                   & " case when [9]>0 then [9] else 0 end + case when [10]>0 then [10] else 0 end +case when [11]>0 then [11] else 0 end +case when [12]>0 then [12] else 0 end +  " & vbCrLf _
                   & " case when [13]>0 then [13] else 0 end +case when [14]>0 then [14] else 0 end +case when [15]>0 then [15] else 0 end +case when [16]>0 then [16] else 0 end + " & vbCrLf _
                   & " case when [17]>0 then [17] else 0 end + case when [18]>0 then [18] else 0 end + case when [19]>0 then [19] else 0 end + case when [20]>0 then [20] else 0 end + " & vbCrLf _
                   & " case when [21]>0 then [21] else 0 end + case when [22]>0 then [22] else 0 end + case when [23]>0 then [23] else 0 end + case when [24]>0 then [24] else 0 end + case when [25]>0 then [25] else 0 end +  " & vbCrLf _
                   & " case when [26]>0 then [26] else 0 end + case when [27]>0 then [27] else 0 end + case when [28]>0 then [28] else 0 end + case when [29]>0 then [29] else 0 end +  " & vbCrLf _
                   & " case when [30]>0 then [30] else 0 end + case when [31]>0 then [31] else 0 end ) as total from  " & vbCrLf _
                   & " (select 1 sno,l.[Lineno],convert(int,l.mergno) empno,''empname,l.ptype jobgrade, sum(l.[1]) [1],sum(l.[2]) [2] ,sum(l.[3]) [3],sum(l.[4]) [4],sum(l.[5]) [5],sum(l.[6]) [6],sum(l.[7]) [7],  " & vbCrLf _
                   & " sum(l.[8]) [8],sum(l.[9]) [9],sum(l.[10]) [10],sum(l.[11]) [11],sum(l.[12]) [12],sum(l.[13]) [13],sum(l.[14]) [14],  " & vbCrLf _
                   & " sum(l.[15]) [15],sum(l.[16]) [16],sum(l.[17]) [17],sum(l.[18]) [18],sum(l.[19]) [19],sum(l.[20]) [20],sum(l.[21]) [21], " & vbCrLf _
                   & " sum(l.[22]) [22],sum(l.[23]) [23],sum(l.[24]) [24],sum(l.[25]) [25],sum(l.[26]) [26],sum(l.[27]) [27]," & vbCrLf _
                   & " sum(l.[28]) [28],sum(l.[29]) [29],sum(l.[30]) [30],sum(l.[31]) [31],0 totinc  from " & vbCrLf _
                   & " (select [Lineno],ptype,mergno, isnull([1],0) [1],isnull([2],0) [2] ,ISNULL([3],0) [3],isnull([4],0) [4],isnull([5],0) [5],isnull([6],0) [6],isnull([7],0) [7],  " & vbCrLf _
                   & " isnull([8],0) [8],isnull([9],0) [9],isnull([10],0) [10],isnull([11],0) [11],isnull([12],0) [12],isnull([13],0) [13],isnull([14],0) [14], " & vbCrLf _
                   & " isnull([15],0) [15],isnull([16],0) [16],isnull([17],0) [17],isnull([18],0) [18],isnull([19],0) [19],isnull([20],0) [20],isnull([21],0) [21], " & vbCrLf _
                   & " isnull([22],0) [22],isnull([23],0) [23],isnull([24],0) [24],isnull([25],0) [25],isnull([26],0) [26],isnull([27],0) [27], " & vbCrLf _
                   & " isnull([28],0) [28],isnull([29],0) [29],isnull([30],0) [30],isnull([31],0) [31]  from  " & vbCrLf _
                   & " (select k.dat,k.[lineno],k.mergno,k.brand,'ProdQty' ptype, isnull(sum(k.totprodqty),0) totprodqty from  " & vbCrLf _
                   & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,m.mergno  from prodcost.dbo.operf b with (nolock) " & vbCrLf _
                   & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster with (nolock) group by brand,nopcs) c on c.brand=b.brand " & vbCrLf _
                   & " left join prodcost.dbo.mergeline m on m.linenum=b.[lineno] " & vbCrLf _
                   & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k   " & vbCrLf _
                   & " group by k.dat,k.[lineno],k.brand ,k.mergno " & vbCrLf _
                   & " union all " & vbCrLf _
                   & " select k.dat,k.[lineno],k.mergno,k.brand,'TargetQty' ptype, isnull(sum(k.target),0) target from   " & vbCrLf _
                   & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,m.mergno  from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
                   & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                   & " left join prodcost.dbo.mergeline m on m.linenum=b.[lineno] " & vbCrLf _
                   & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k   " & vbCrLf _
                   & " group by k.dat,k.[lineno],k.brand,k.mergno   " & vbCrLf _
                   & " union all " & vbCrLf _
                   & " select k.dat,k.[lineno],k.mergno,k.brand,'ZExcessQty' ptype, isnull(sum(k.exqty),0) exqty from  " & vbCrLf _
                   & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,m.mergno  from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
                   & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                   & " left join prodcost.dbo.mergeline m on m.linenum=b.[lineno] " & vbCrLf _
                   & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
                   & " group by k.dat,k.[lineno],k.brand ,k.mergno  " & vbCrLf _
                   & " union all " & vbCrLf _
                   & " select k.dat,k.[lineno],k.mergno, k.brand,'Machine' ptype, isnull(sum(k.nomac),0) exqty from  " & vbCrLf _
                   & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,m.mergno  from prodcost.dbo.operf b with (nolock) " & vbCrLf _
                   & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                   & " left join prodcost.dbo.mergeline m on m.linenum=b.[lineno] " & vbCrLf _
                   & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
                   & " group by k.dat,k.[lineno],k.brand,k.mergno  " & vbCrLf _
                   & " union all " & vbCrLf _
                   & " select k.dat,k.[lineno],k.mergno, k.brand,'Nopcs' ptype, isnull(sum(k.nopcs),0) exqty from  " & vbCrLf _
                   & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,c.nopcs, b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,m.mergno  from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
                   & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                   & " left join prodcost.dbo.mergeline m on m.linenum=b.[lineno] " & vbCrLf _
                   & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
                   & " group by k.dat,k.[lineno],k.brand,k.mergno ) s  " & vbCrLf _
                   & " pivot (sum(totprodqty) FOR [dat] IN  " & vbCrLf _
                   & " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) Pv1) l  " & vbCrLf _
                   & " group by l.[Lineno],l.ptype,l.mergno " & vbCrLf _
                   & " union all " & vbCrLf _
                   & " select 2 sno, [lineno],empno,empname,jobgrade, " & vbCrLf _
                   & " isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6], " & vbCrLf _
                   & " isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12], " & vbCrLf _
                   & " isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18], " & vbCrLf _
                   & " isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24], " & vbCrLf _
                   & " isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30], " & vbCrLf _
                   & " isnull([31],0) as [31]," & vbCrLf _
                   & " (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+  " & vbCrLf _
                   & " isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+  " & vbCrLf _
                   & " isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+ " & vbCrLf _
                   & " isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+ " & vbCrLf _
                   & " isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from " & vbCrLf _
                   & " (select s.[Lineno],s.empno,s.empname,s.jobgrade,SUM(s.amt) amt, s.dat from  " & vbCrLf _
                   & " (select b.date,DATEPART(d,b.date) dat, b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr, " & vbCrLf _
                   & " isnull((b.totprodqty-(b.nomac*d.nopcs)),0) as incqty, round(isnull(c.incentiveamt,0),2) as incentive," & vbCrLf _
                   & " isnull(case when ((b.totprodqty-(b.nomac*d.nopcs))/isnull(m.mergno,0)) > 0 then round((((b.totprodqty-(b.nomac*d.nopcs))/isnull(m.mergno,0))* c.incentiveamt),2) else 0 end,0) amt, " & vbCrLf _
                   & " isnull(em.salary,0) salary," & vbCrLf _
                   & " isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
                   & " (datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
                   & " SUM(case when ((b.totprodqty-(b.nomac*d.nopcs))/ isnull(m.mergno,0)) > 0 then round((((b.totprodqty-(b.nomac*d.nopcs))/isnull(m.mergno,0))*c.incentiveamt),0) else 0 end) over (partition by b.date,b.[lineno]) as totincentive, " & vbCrLf _
                   & " sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary " & vbCrLf _
                   & " from prodcost.dbo.operf b with (nolock) " & vbCrLf _
                   & " left join (select j.brand,j.bno,j.empid,j.empno,j.[lineno],SUM(j.incentiveamt) incentiveamt from ( " & vbCrLf _
                   & " select c.brand, b.bno, b.empid,b.empno,b.jobgrade,b.[lineno],b.opername,b.totprod,b.totmin, (b.totmin/convert(real,480))*100 perc,  " & vbCrLf _
                   & " (b.totmin/convert(real,480))*100 pper,  case when isnull(b.totmin,0)>0 then round(i.incentive* (b.totmin/convert(real,480)),2) else 0 end incentiveamt, " & vbCrLf _
                   & " i.incentive, b.incyn    from prodcost.dbo.perf1 b  " & vbCrLf _
                   & " left join prodcost.dbo.operf c on c.bno=b.bno and c.date=b.date  " & vbCrLf _
                   & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade)j  " & vbCrLf _
                   & " group by j.brand,j.bno,j.empid,j.empno,j.[lineno])   c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf _
                   & " left join (select brand,nopcs from prodcost.dbo.incentivemaster with (nolock) group by brand,nopcs) d on d.brand=b.brand  " & vbCrLf _
                   & " left join prodcost.dbo.empmaster em with (nolock) on em.nemp_id=c.empid  " & vbCrLf _
                   & " left join prodcost.dbo.mergeline m on m.linenum=b.[lineno]  " & vbCrLf _
                   & " Left Join " & vbCrLf _
                   & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from prodcost.dbo.incentivemaster b with (nolock) " & vbCrLf _
                   & " Left Join " & vbCrLf _
                   & " (select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
                   & " (select brand, nprodpcsfr,nprodpcsto from  prodcost.dbo.incentivemaster with (nolock) group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade  " & vbCrLf _
                   & " where    date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'   and (b.totprodqty-(b.nomac*d.nopcs)) > 0  ) s " & vbCrLf _
                   & " group by s.[Lineno],s.empno,s.empname,s.jobgrade,s.dat ) k " & vbCrLf _
                   & " PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
                   & " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P  ) lk " & vbCrLf _
                   & " order by  lk.[lineno],lk.sno,lk.jobgrade,lk.empno "

        Else
            'old
            'MSQL = " select lk.[lineno],lk.empno,lk.empname,lk.jobgrade, " & vbCrLf _
            '        & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
            '        & " (case when [1]>0 then [1] else 0 end + case when [2]>0 then [2] else 0 end + case when [3]>0 then [3] else 0 end +  " & vbCrLf _
            '        & " case when [4]>0 then [4] else 0 end +  case when [5]>0 then [5] else 0 end + case when [6]>0 then [6] else 0 end + " & vbCrLf _
            '        & " case when [7]>0 then [7] else 0 end +case when [8]>0 then [8] else 0 end +  " & vbCrLf _
            '        & " case when [9]>0 then [9] else 0 end + case when [10]>0 then [10] else 0 end +case when [11]>0 then [11] else 0 end +case when [12]>0 then [12] else 0 end +  " & vbCrLf _
            '        & " case when [13]>0 then [13] else 0 end +case when [14]>0 then [14] else 0 end +case when [15]>0 then [15] else 0 end +case when [16]>0 then [16] else 0 end + " & vbCrLf _
            '        & " case when [17]>0 then [17] else 0 end + case when [18]>0 then [18] else 0 end + case when [19]>0 then [19] else 0 end + case when [20]>0 then [20] else 0 end + " & vbCrLf _
            '        & " case when [21]>0 then [21] else 0 end + case when [22]>0 then [22] else 0 end + case when [23]>0 then [23] else 0 end + case when [24]>0 then [24] else 0 end + case when [25]>0 then [25] else 0 end +  " & vbCrLf _
            '        & " case when [26]>0 then [26] else 0 end + case when [27]>0 then [27] else 0 end + case when [28]>0 then [28] else 0 end + case when [29]>0 then [29] else 0 end +  " & vbCrLf _
            '        & " case when [30]>0 then [30] else 0 end + case when [31]>0 then [31] else 0 end ) as total from  " & vbCrLf _
            '        & " (select 1 sno,l.[Lineno],convert(int,l.mergno)  empno,''empname,l.ptype jobgrade, sum(l.[1]) [1],sum(l.[2]) [2] ,sum(l.[3]) [3],sum(l.[4]) [4],sum(l.[5]) [5],sum(l.[6]) [6],sum(l.[7]) [7],  " & vbCrLf _
            '        & " sum(l.[8]) [8],sum(l.[9]) [9],sum(l.[10]) [10],sum(l.[11]) [11],sum(l.[12]) [12],sum(l.[13]) [13],sum(l.[14]) [14],  " & vbCrLf _
            '        & " sum(l.[15]) [15],sum(l.[16]) [16],sum(l.[17]) [17],sum(l.[18]) [18],sum(l.[19]) [19],sum(l.[20]) [20],sum(l.[21]) [21], " & vbCrLf _
            '        & " sum(l.[22]) [22],sum(l.[23]) [23],sum(l.[24]) [24],sum(l.[25]) [25],sum(l.[26]) [26],sum(l.[27]) [27]," & vbCrLf _
            '        & " sum(l.[28]) [28],sum(l.[29]) [29],sum(l.[30]) [30],sum(l.[31]) [31],0 totinc  from " & vbCrLf _
            '        & " (select [Lineno],ptype,mergno, isnull([1],0) [1],isnull([2],0) [2] ,ISNULL([3],0) [3],isnull([4],0) [4],isnull([5],0) [5],isnull([6],0) [6],isnull([7],0) [7],  " & vbCrLf _
            '        & " isnull([8],0) [8],isnull([9],0) [9],isnull([10],0) [10],isnull([11],0) [11],isnull([12],0) [12],isnull([13],0) [13],isnull([14],0) [14], " & vbCrLf _
            '        & " isnull([15],0) [15],isnull([16],0) [16],isnull([17],0) [17],isnull([18],0) [18],isnull([19],0) [19],isnull([20],0) [20],isnull([21],0) [21], " & vbCrLf _
            '        & " isnull([22],0) [22],isnull([23],0) [23],isnull([24],0) [24],isnull([25],0) [25],isnull([26],0) [26],isnull([27],0) [27], " & vbCrLf _
            '        & " isnull([28],0) [28],isnull([29],0) [29],isnull([30],0) [30],isnull([31],0) [31]  from  " & vbCrLf _
            '        & " (select k.dat,k.[lineno],k.mergno,k.brand,'ProdQty' ptype, isnull(sum(k.totprodqty),0) totprodqty from  " & vbCrLf _
            '        & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno  from prodcost.dbo.operf b with (nolock) " & vbCrLf _
            '        & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster with (nolock) group by brand,nopcs) c on c.brand=b.brand " & vbCrLf _
            '        & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k   " & vbCrLf _
            '        & " group by k.dat,k.[lineno],k.brand ,k.mergno " & vbCrLf _
            '        & " union all " & vbCrLf _
            '        & " select k.dat,k.[lineno],k.mergno,k.brand,'TargetQty' ptype, isnull(sum(k.target),0) target from   " & vbCrLf _
            '        & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno  from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
            '        & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            '        & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k   " & vbCrLf _
            '        & " group by k.dat,k.[lineno],k.brand,k.mergno   " & vbCrLf _
            '        & " union all " & vbCrLf _
            '        & " select k.dat,k.[lineno],k.mergno,k.brand,'ZExcessQty' ptype, isnull(sum(k.exqty),0) exqty from  " & vbCrLf _
            '        & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno   from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
            '        & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            '        & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            '        & " group by k.dat,k.[lineno],k.brand ,k.mergno  " & vbCrLf _
            '        & " union all " & vbCrLf _
            '        & " select k.dat,k.[lineno],k.mergno, k.brand,'Machine' ptype, isnull(sum(k.nomac),0) exqty from  " & vbCrLf _
            '        & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno  from prodcost.dbo.operf b with (nolock) " & vbCrLf _
            '        & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            '        & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            '        & " group by k.dat,k.[lineno],k.brand,k.mergno  " & vbCrLf _
            '        & " union all " & vbCrLf _
            '        & " select k.dat,k.[lineno],k.mergno, k.brand,'Nopcs' ptype, isnull(sum(k.nopcs),0) exqty from  " & vbCrLf _
            '        & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,c.nopcs, b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno   from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
            '        & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            '        & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            '        & " group by k.dat,k.[lineno],k.brand,k.mergno ) s  " & vbCrLf _
            '        & " pivot (sum(totprodqty) FOR [dat] IN  " & vbCrLf _
            '        & " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) Pv1) l  " & vbCrLf _
            '        & " group by l.[Lineno],l.ptype,l.mergno " & vbCrLf _
            '        & " union all " & vbCrLf _
            '        & " select 2 sno, [lineno],empno,empname,jobgrade, " & vbCrLf _
            '        & " isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6], " & vbCrLf _
            '        & " isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12], " & vbCrLf _
            '        & " isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18], " & vbCrLf _
            '        & " isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24], " & vbCrLf _
            '        & " isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30], " & vbCrLf _
            '        & " isnull([31],0) as [31]," & vbCrLf _
            '        & " (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+  " & vbCrLf _
            '        & " isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+  " & vbCrLf _
            '        & " isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+ " & vbCrLf _
            '        & " isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+ " & vbCrLf _
            '        & " isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from " & vbCrLf _
            '        & " (select s.[Lineno],s.empno,s.empname,s.jobgrade,SUM(s.amt) amt, s.dat from  " & vbCrLf _
            '        & " (select b.date,DATEPART(d,b.date) dat, b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr, " & vbCrLf _
            '        & " isnull((b.totprodqty-(b.nomac*d.nopcs)),0) as incqty, round(isnull(c.incentiveamt,0),2) as incentive," & vbCrLf _
            '        & " isnull(case when ((b.totprodqty-(b.nomac*d.nopcs))/isnull(b.mergno,1)) > 0 then round((((b.totprodqty-(b.nomac*d.nopcs))/isnull(b.mergno,1))* c.incentiveamt),2) else 0 end,0) amt, " & vbCrLf _
            '        & " isnull(em.salary,0) salary," & vbCrLf _
            '        & " isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
            '        & " (datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
            '        & " SUM(case when ((b.totprodqty-(b.nomac*d.nopcs))/ isnull(b.mergno,1)) > 0 then round((((b.totprodqty-(b.nomac*d.nopcs))/isnull(b.mergno,1))*c.incentiveamt),0) else 0 end) over (partition by b.date,b.[lineno]) as totincentive, " & vbCrLf _
            '        & " sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary " & vbCrLf _
            '        & " from prodcost.dbo.operf b with (nolock) " & vbCrLf _
            '        & " left join (select j.brand,j.bno,j.empid,j.empno,j.[lineno],SUM(j.incentiveamt) incentiveamt from ( " & vbCrLf _
            '        & " select c.brand, b.bno, b.empid,b.empno,b.jobgrade,b.[lineno],b.opername,b.totprod,b.totmin, (b.totmin/convert(real,480))*100 perc,  " & vbCrLf _
            '        & " (b.totmin/convert(real,480))*100 pper,  case when isnull(b.totmin,0)>0 then round(i.incentive* (b.totmin/convert(real,480)),2) else 0 end incentiveamt, " & vbCrLf _
            '        & " i.incentive, b.incyn    from prodcost.dbo.perf1 b  " & vbCrLf _
            '        & " left join prodcost.dbo.operf c on c.bno=b.bno and c.date=b.date  " & vbCrLf _
            '        & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade)j  " & vbCrLf _
            '        & " group by j.brand,j.bno,j.empid,j.empno,j.[lineno])   c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf _
            '        & " left join (select brand,nopcs from prodcost.dbo.incentivemaster with (nolock) group by brand,nopcs) d on d.brand=b.brand  " & vbCrLf _
            '        & " left join prodcost.dbo.empmaster em with (nolock) on em.nemp_id=c.empid  " & vbCrLf _
            '        & " left join prodcost.dbo.mergeline m on m.linenum=b.[lineno]  " & vbCrLf _
            '        & " Left Join " & vbCrLf _
            '        & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from prodcost.dbo.incentivemaster b with (nolock) " & vbCrLf _
            '        & " Left Join " & vbCrLf _
            '        & " (select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
            '        & " (select brand, nprodpcsfr,nprodpcsto from  prodcost.dbo.incentivemaster with (nolock) group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade  " & vbCrLf _
            '        & " where    date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'   and (b.totprodqty-(b.nomac*d.nopcs)) > 0  ) s " & vbCrLf _
            '        & " group by s.[Lineno],s.empno,s.empname,s.jobgrade,s.dat ) k " & vbCrLf _
            '        & " PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
            '        & " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P  ) lk " & vbCrLf _
            '        & " order by  lk.[lineno],lk.sno,lk.jobgrade,lk.empno "

            'End If


            '*** New
            MSQL = " select lk.[lineno],lk.empno,lk.empname,lk.jobgrade, " & vbCrLf _
                    & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
                    & " (case when [1]>0 then [1] else 0 end + case when [2]>0 then [2] else 0 end + case when [3]>0 then [3] else 0 end +  " & vbCrLf _
                    & " case when [4]>0 then [4] else 0 end +  case when [5]>0 then [5] else 0 end + case when [6]>0 then [6] else 0 end + " & vbCrLf _
                    & " case when [7]>0 then [7] else 0 end +case when [8]>0 then [8] else 0 end +  " & vbCrLf _
                    & " case when [9]>0 then [9] else 0 end + case when [10]>0 then [10] else 0 end +case when [11]>0 then [11] else 0 end +case when [12]>0 then [12] else 0 end +  " & vbCrLf _
                    & " case when [13]>0 then [13] else 0 end +case when [14]>0 then [14] else 0 end +case when [15]>0 then [15] else 0 end +case when [16]>0 then [16] else 0 end + " & vbCrLf _
                    & " case when [17]>0 then [17] else 0 end + case when [18]>0 then [18] else 0 end + case when [19]>0 then [19] else 0 end + case when [20]>0 then [20] else 0 end + " & vbCrLf _
                    & " case when [21]>0 then [21] else 0 end + case when [22]>0 then [22] else 0 end + case when [23]>0 then [23] else 0 end + case when [24]>0 then [24] else 0 end + case when [25]>0 then [25] else 0 end +  " & vbCrLf _
                    & " case when [26]>0 then [26] else 0 end + case when [27]>0 then [27] else 0 end + case when [28]>0 then [28] else 0 end + case when [29]>0 then [29] else 0 end +  " & vbCrLf _
                    & " case when [30]>0 then [30] else 0 end + case when [31]>0 then [31] else 0 end ) as total from  " & vbCrLf _
                    & " (select 1 sno,l.[Lineno],convert(int,l.mergno)  empno,''empname,l.ptype jobgrade, sum(l.[1]) [1],sum(l.[2]) [2] ,sum(l.[3]) [3],sum(l.[4]) [4],sum(l.[5]) [5],sum(l.[6]) [6],sum(l.[7]) [7],  " & vbCrLf _
                    & " sum(l.[8]) [8],sum(l.[9]) [9],sum(l.[10]) [10],sum(l.[11]) [11],sum(l.[12]) [12],sum(l.[13]) [13],sum(l.[14]) [14],  " & vbCrLf _
                    & " sum(l.[15]) [15],sum(l.[16]) [16],sum(l.[17]) [17],sum(l.[18]) [18],sum(l.[19]) [19],sum(l.[20]) [20],sum(l.[21]) [21], " & vbCrLf _
                    & " sum(l.[22]) [22],sum(l.[23]) [23],sum(l.[24]) [24],sum(l.[25]) [25],sum(l.[26]) [26],sum(l.[27]) [27]," & vbCrLf _
                    & " sum(l.[28]) [28],sum(l.[29]) [29],sum(l.[30]) [30],sum(l.[31]) [31],0 totinc  from " & vbCrLf _
                    & " (select [Lineno],ptype,mergno, isnull([1],0) [1],isnull([2],0) [2] ,ISNULL([3],0) [3],isnull([4],0) [4],isnull([5],0) [5],isnull([6],0) [6],isnull([7],0) [7],  " & vbCrLf _
                    & " isnull([8],0) [8],isnull([9],0) [9],isnull([10],0) [10],isnull([11],0) [11],isnull([12],0) [12],isnull([13],0) [13],isnull([14],0) [14], " & vbCrLf _
                    & " isnull([15],0) [15],isnull([16],0) [16],isnull([17],0) [17],isnull([18],0) [18],isnull([19],0) [19],isnull([20],0) [20],isnull([21],0) [21], " & vbCrLf _
                    & " isnull([22],0) [22],isnull([23],0) [23],isnull([24],0) [24],isnull([25],0) [25],isnull([26],0) [26],isnull([27],0) [27], " & vbCrLf _
                    & " isnull([28],0) [28],isnull([29],0) [29],isnull([30],0) [30],isnull([31],0) [31]  from  " & vbCrLf _
                    & " (select k.dat,k.[lineno],k.mergno,k.brand,'ProdQty' ptype, isnull(sum(k.totprodqty),0) totprodqty from  " & vbCrLf _
                    & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno  from prodcost.dbo.operf b with (nolock) " & vbCrLf _
                    & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster with (nolock) group by brand,nopcs) c on c.brand=b.brand " & vbCrLf _
                    & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k   " & vbCrLf _
                    & " group by k.dat,k.[lineno],k.brand ,k.mergno " & vbCrLf _
                    & " union all " & vbCrLf _
                    & " select k.dat,k.[lineno],k.mergno,k.brand,'TargetQty' ptype, isnull(sum(k.target),0) target from   " & vbCrLf _
                    & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno  from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
                    & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                    & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k   " & vbCrLf _
                    & " group by k.dat,k.[lineno],k.brand,k.mergno   " & vbCrLf _
                    & " union all " & vbCrLf _
                    & " select k.dat,k.[lineno],k.mergno,k.brand,'ZExcessQty' ptype, isnull(sum(k.exqty),0) exqty from  " & vbCrLf _
                    & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno   from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
                    & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                    & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
                    & " group by k.dat,k.[lineno],k.brand ,k.mergno  " & vbCrLf _
                    & " union all " & vbCrLf _
                    & " select k.dat,k.[lineno],k.mergno, k.brand,'Machine' ptype, isnull(sum(k.nomac),0) exqty from  " & vbCrLf _
                    & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno  from prodcost.dbo.operf b with (nolock) " & vbCrLf _
                    & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                    & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
                    & " group by k.dat,k.[lineno],k.brand,k.mergno  " & vbCrLf _
                    & " union all " & vbCrLf _
                    & " select k.dat,k.[lineno],k.mergno, k.brand,'Nopcs' ptype, isnull(sum(k.nopcs),0) exqty from  " & vbCrLf _
                    & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,c.nopcs, b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno   from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
                    & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                    & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
                    & " group by k.dat,k.[lineno],k.brand,k.mergno ) s  " & vbCrLf _
                    & " pivot (sum(totprodqty) FOR [dat] IN  " & vbCrLf _
                    & " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) Pv1) l  " & vbCrLf _
                    & " group by l.[Lineno],l.ptype,l.mergno " & vbCrLf _
                    & " union all " & vbCrLf _
                    & " select 2 sno, [lineno],empno,empname,jobgrade, " & vbCrLf _
                    & " isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6], " & vbCrLf _
                    & " isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12], " & vbCrLf _
                    & " isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18], " & vbCrLf _
                    & " isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24], " & vbCrLf _
                    & " isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30], " & vbCrLf _
                    & " isnull([31],0) as [31]," & vbCrLf _
                    & " (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+  " & vbCrLf _
                    & " isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+  " & vbCrLf _
                    & " isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+ " & vbCrLf _
                    & " isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+ " & vbCrLf _
                    & " isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from (" & vbCrLf


            '' New old
            'MSQL = MSQL & "select s.[lineno],s.empno,s.empname,s.jobgrade,sum(isnull(s.Actincentive,0)) amt,datepart(d,s.date) dat from (" & vbCrLf _
            '             & " select k.date,k.bno,k.linno [lineno],k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs," & vbCrLf _
            '             & " k.totprod,k.jobgrade,k.rate,k.totcnt,k.avgpcs,k.operexspcs,k.totincentamt, " & vbCrLf _
            '             & " case when isnull(k.operexspcs,0)>0 then sum(k.operexspcs) over (partition by k.opername,k.date,k.linno) else 0 end totoppcs, " & vbCrLf _
            '             & " round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per, " & vbCrLf _
            '             & " round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive " & vbCrLf _
            '             & " from ( " & vbCrLf _
            '             & " select c.date,c.bno, c.[lineno] linno,c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,isnull(j.pcs,0)*8 sampcs, b.totprod,b.jobgrade,i.incentive rate, " & vbCrLf _
            '             & " count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt,(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs, " & vbCrLf _
            '             & "  b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs2, " & vbCrLf _
            '             & " case when (b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) else 0 end operexspcs, " & vbCrLf _
            '             & " case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then (c.totprodqty-(c.nomac*i.nopcs))*i.incentive else 0 end totincentamt " & vbCrLf _
            '             & " from perf1 b " & vbCrLf _
            '             & " inner join operf c on c.bno=b.bno " & vbCrLf _
            '             & " left join PROCESSJOBMASTER j on j.OPERNAME=b.opername " & vbCrLf _
            '             & " left join incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade " & vbCrLf _
            '             & " where c.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and c.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and  b.jobgrade not in ('XII') ) k) s " & vbCrLf _
            '             & " group by  s.[lineno],s.empno,s.empname,s.jobgrade,s.date " & vbCrLf

            'new wo manpower
            'MSQL = MSQL & " select s.[linno] [Lineno],s.empno,s.empname,s.jobgrade,sum(isnull(s.Actincentive,0)) amt,datepart(d,s.date) dat from ( " & vbCrLf _
            '            & " select k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs,k.totprod,k.jobgrade,k.rate,k.totcnt,k.avgpcs,k.operexspcs,k.totincentamt, " & vbCrLf _
            '            & " case when isnull(k.operexspcs,0)>0 then sum(k.operexspcs) over (partition by k.opername,k.date,k.linno) else 0 end totoppcs, " & vbCrLf _
            '            & " round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per, " & vbCrLf _
            '            & " round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive from ( " & vbCrLf _
            '            & " select c.date,c.bno, c.[lineno] linno,c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,isnull(j.pcs,0)*8 sampcs, b.totprod,b.jobgrade,i.incentive rate, " & vbCrLf _
            '            & " count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt,(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs, " & vbCrLf _
            '            & " b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs2, " & vbCrLf _
            '            & " case when (b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) else 0 end operexspcs, " & vbCrLf _
            '            & " case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then (c.totprodqty-(c.nomac*i.nopcs))*i.incentive else 0 end totincentamt   from prodcost.dbo.perf1 b " & vbCrLf _
            '            & " inner join prodcost.dbo.operf c on c.bno=b.bno " & vbCrLf _
            '            & " left join prodcost.dbo.PROCESSJOBMASTER j on j.OPERNAME=b.opername " & vbCrLf _
            '            & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade " & vbCrLf _
            '            & " where c.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and c.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and  b.jobgrade not in ('XII') ) k  ) s  " & vbCrLf _
            '            & " group by  s.[linno],s.empno,s.empname,s.jobgrade,s.date " & vbCrLf



            '**new Manpower
            'MSQL = MSQL & " select s.[linno] [Lineno],s.empno,s.empname,s.jobgrade,sum(isnull(s.Actincentive,0)) amt,datepart(d,s.date) dat from (  " & vbCrLf _
            '            & "  select k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs,k.totprod,k.jobgrade,k.rate,k.totcnt,k.MANPOWER, k.avgpcs,k.operexspcs,k.totincentamt,  " & vbCrLf _
            '            & "  case when isnull(k.operexspcs,0)>0 then sum(k.operexspcs) over (partition by k.opername,k.date,k.linno) else 0 end totoppcs, " & vbCrLf _
            '            & "  round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per, " & vbCrLf _
            '            & "  round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive from (  " & vbCrLf _
            '            & "  select c.date,c.bno, c.[lineno] linno,c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,(isnull(j.pcs,0)*8) * j.MANPOWER sampcs, b.totprod,b.jobgrade,i.incentive rate, " & vbCrLf _
            '            & "  count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt, j.MANPOWER,((isnull(j.pcs,0)*8)*j.manpower)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs, " & vbCrLf _
            '            & "  b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs2, " & vbCrLf _
            '            & "  case when (b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) else 0 end operexspcs, " & vbCrLf _
            '            & "  case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then ((c.totprodqty-(c.nomac*i.nopcs))*(i.incentive*j.manpower)) else 0 end totincentamt   from prodcost.dbo.perf1 b " & vbCrLf _
            '            & "  inner join prodcost.dbo.operf c on c.bno=b.bno  " & vbCrLf _
            '            & "  left join prodcost.dbo.PROCESSJOBMASTER j on j.OPERNAME=b.opername " & vbCrLf _
            '            & "  left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade  " & vbCrLf _
            '            & "  where c.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and c.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and  b.jobgrade not in ('XII') ) k ) s  " & vbCrLf _
            '            & "  group by  s.[linno],s.empno,s.empname,s.jobgrade,s.date " & vbCrLf




            ''**new lat
            'MSQL = MSQL & "select s.[linno] [Lineno],s.empno,s.empname,s.jobgrade,sum(isnull(s.Actincentive,0)) amt,datepart(d,s.date) dat from ( " & vbCrLf _
            '            & " select k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs,k.sampcs2, k.totprod,k.jobgrade,k.rate,k.totcnt,k.MANPOWER, k.avgpcs,k.operexspcs, k.totincentamt, " & vbCrLf _
            '            & " case when isnull(k.operexspcs,0)>0 then sum(k.operexspcs) over (partition by k.opername,k.date,k.linno) else 0 end totoppcs, " & vbCrLf _
            '            & " round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per, " & vbCrLf _
            '            & " round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive from ( " & vbCrLf _
            '            & " select c.date,c.bno, c.[lineno] linno,c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,(isnull(j.pcs,0)*8) * j.MANPOWER sampcs2," & vbCrLf _
            '            & " case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end sampcs, b.totprod,b.jobgrade,i.incentive rate," & vbCrLf _
            '            & " count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt, j.MANPOWER,((isnull(j.pcs,0)*8)*j.manpower)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs, " & vbCrLf _
            '            & " b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs2," & vbCrLf _
            '            & " case when (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end) else 0 end operexspcs," & vbCrLf _
            '            & " case when (b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ case when totmin=480 then j.manpower else count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) end )>0 then b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ case when totmin=480 then j.manpower else count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) end else 0 end operexspcs3," & vbCrLf _
            '            & " case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then ((c.totprodqty-(c.nomac*i.nopcs))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end else 0 end totincentamt " & vbCrLf _
            '            & " from prodcost.dbo.perf1 b " & vbCrLf _
            '            & " inner join prodcost.dbo.operf c on c.bno=b.bno " & vbCrLf _
            '            & " left join prodcost.dbo.PROCESSJOBMASTER j on j.OPERNAME=b.opername " & vbCrLf _
            '            & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade " & vbCrLf _
            '            & " where c.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and c.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and  b.jobgrade not in ('XII') ) k) s   " & vbCrLf _
            '            & " group by  s.[linno],s.empno,s.empname,s.jobgrade,s.date " & vbCrLf



            '**new lat2


            'MSQL = MSQL & "select s.[linno] [Lineno],s.empno,s.empname,s.jobgrade,sum(isnull(s.Actincentive,0)) amt,datepart(d,s.date) dat from (   " & vbCrLf _
            '             & " select k.mergno, k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs,k.sampcs2, k.totprod,k.jobgrade,k.rate,k.totcnt,k.MANPOWER, k.avgpcs,k.operexspcs, k.totincentamt, " & vbCrLf _
            '             & " case when isnull(k.operexspcs,0)>0 then sum(k.operexspcs) over (partition by k.opername,k.date,k.linno) else 0 end totoppcs," & vbCrLf _
            '             & " round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per, " & vbCrLf _
            '             & "round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive from (  " & vbCrLf _
            '             & "select c.date,c.bno, c.[lineno] linno,isnull(c.mergno,1) mergno, c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,(isnull(j.pcs,0)*8) * j.MANPOWER sampcs2," & vbCrLf _
            '             & "case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end sampcs, b.totprod,b.jobgrade,i.incentive rate,  " & vbCrLf _
            '             & " count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt, j.MANPOWER,((isnull(j.pcs,0)*8)*j.manpower)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs,  " & vbCrLf _
            '             & " b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs2, " & vbCrLf _
            '             & " case when (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end) else 0 end operexspcs," & vbCrLf _
            '             & " case when (b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ case when totmin=480 then j.manpower else count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) end )>0 then b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ " & vbCrLf _
            '             & "case when totmin=480 then j.manpower else count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) end else 0 end operexspcs3, " & vbCrLf _
            '             & " case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then " & vbCrLf _
            '             & " case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end " & vbCrLf _
            '             & " else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))   *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end " & vbCrLf _
            '             & " else 0 end totincentamt from prodcost.dbo.perf1 b  " & vbCrLf _
            '             & " inner join prodcost.dbo.operf c on c.bno=b.bno " & vbCrLf _
            '             & " left join prodcost.dbo.PROCESSJOBMASTER j on j.OPERNAME=b.opername and rtrim(ltrim(replace(j.style,'FULL','')))=rtrim(ltrim(c.style))  " & vbCrLf _
            '             & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade " & vbCrLf _
            '             & " where c.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and c.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and  b.jobgrade not in ('XII') ) k ) s   " & vbCrLf _
            '             & " group by  s.[linno],s.empno,s.empname,s.jobgrade,s.date " & vbCrLf

            ' New Lat3
            MSQL = MSQL & "select s.[linno] [Lineno],s.empno,s.empname,s.jobgrade,sum(isnull(s.Actincentive,0)) amt,datepart(d,s.date) dat from (   " & vbCrLf _
                         & " select k.mergno, k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs,k.sampcs2, k.totprod,k.jobgrade,k.rate,k.totcnt,k.MANPOWER, k.avgpcs,k.operexspcs, k.totincentamt, " & vbCrLf _
                         & " case when isnull(k.operexspcs,0)>0 then sum(k.operexspcs) over (partition by k.opername,k.date,k.linno) else 0 end totoppcs," & vbCrLf _
                         & " round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per, " & vbCrLf _
                         & "round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive from (  " & vbCrLf _
                         & "select c.date,c.bno, c.[lineno] linno,isnull(c.mergno,1) mergno, c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,(isnull(j.pcs,0)*8) * j.MANPOWER sampcs2," & vbCrLf _
                         & "case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end sampcs, b.totprod,b.jobgrade,i.incentive rate,  " & vbCrLf _
                         & " count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt, j.MANPOWER,((isnull(j.pcs,0)*8)*j.manpower)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs,  " & vbCrLf _
                         & " b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs2, " & vbCrLf _
                         & " case when (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end) else 0 end operexspcs," & vbCrLf _
                         & " case when (b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ case when totmin=480 then j.manpower else count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) end )>0 then b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ " & vbCrLf _
                         & "case when totmin=480 then j.manpower else count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) end else 0 end operexspcs3, " & vbCrLf _
                         & " case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then " & vbCrLf _
                         & " case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end " & vbCrLf _
                         & " else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))   *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end " & vbCrLf _
                         & " else 0 end totincentamtold, " & vbCrLf _
                         & " case  when (count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))<j.manpower then " & vbCrLf _
                         & "	case when sum(b.totprod) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno])-(isnull(j.pcs,0)*8)>0 then " & vbCrLf _
                         & "		case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then  " & vbCrLf _
                         & "		case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end  " & vbCrLf _
                         & "		else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))  *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end  " & vbCrLf _
                         & "		else 0 end " & vbCrLf _
                         & "	else " & vbCrLf _
                         & "		case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then " & vbCrLf _
                         & "		case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end " & vbCrLf _
                         & "		else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))  *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end " & vbCrLf _
                         & "		else 0 end/(count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno])) " & vbCrLf _
                         & "   End " & vbCrLf _
                         & " else " & vbCrLf _
                         & "	case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then  " & vbCrLf _
                         & "	case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end  " & vbCrLf _
                         & "	else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))  *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end  " & vbCrLf _
                         & " else 0 end end  totincentamt " & vbCrLf _
                         & " from prodcost.dbo.perf1 b  " & vbCrLf _
                         & " inner join prodcost.dbo.operf c on c.bno=b.bno " & vbCrLf _
                         & " left join prodcost.dbo.PROCESSJOBMASTER j on j.OPERNAME=b.opername and rtrim(ltrim(replace(j.style,'FULL','')))=rtrim(ltrim(c.style))  " & vbCrLf _
                         & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade " & vbCrLf _
                         & " where c.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and c.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and  b.jobgrade not in ('XII') ) k ) s   " & vbCrLf _
                         & " group by  s.[linno],s.empno,s.empname,s.jobgrade,s.date " & vbCrLf

                            MSQL = MSQL & " ) k " & vbCrLf _
                              & " PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
                              & " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P  ) lk " & vbCrLf _
                              & " order by  lk.[lineno],lk.sno,lk.jobgrade,lk.empno "

        End If



        s1 = 0
        s2 = 0
        s3 = 0
        s4 = 0
        s5 = 0
        s6 = 0
        s7 = 0
        s8 = 0
        s9 = 0
        s10 = 0
        s11 = 0
        s12 = 0
        s13 = 0
        s14 = 0
        s15 = 0
        s16 = 0
        s17 = 0
        s18 = 0
        s19 = 0
        s20 = 0
        s21 = 0
        s22 = 0
        s23 = 0
        s24 = 0
        s25 = 0
        s26 = 0
        s27 = 0
        s28 = 0
        s29 = 0
        s30 = 0
        s31 = 0
        mtot = 0
        dg.DataSource = Nothing
        dg.Rows.Clear()
        Dim dt As DataTable = getDataTable(MSQL)

        If dt.Rows.Count > 0 Then
            dg.ColumnCount = dt.Columns.Count + 1
            For i = 0 To dt.Columns.Count - 1
                dg.Columns(i).Name = dt.Columns(i).ColumnName
                k = i
            Next i
            dg.Columns(k + 1).Name = "Sno"
        End If

        dg.Columns(0).Width = 30
        dg.Columns(1).Width = 50
        dg.Columns(2).Width = 90
        dg.Columns(3).Width = 50
        For j = 4 To 34
            dg.Columns(j).Width = 40
        Next
        dg.Columns(35).Width = 45
        Dim sno As Integer
        Dim lin As String
        sno = 1
        lin = ""
        For Each row As DataRow In dt.Rows
            'j = dg.Rows.Add()
            'lin = row(0)
            If Len(Trim(lin)) > 0 And row(0) <> lin Then
                j = dg.Rows.Add()
                dg.Rows(j).Cells(1).Value = 0
                dg.Rows(j).Cells(2).Value = lin & " - Total..."
                dg.Rows(j).Cells(4).Value = s1
                dg.Rows(j).Cells(5).Value = s2
                dg.Rows(j).Cells(6).Value = s3
                dg.Rows(j).Cells(7).Value = s4
                dg.Rows(j).Cells(8).Value = s5
                dg.Rows(j).Cells(9).Value = s6
                dg.Rows(j).Cells(10).Value = s7
                dg.Rows(j).Cells(11).Value = s8
                dg.Rows(j).Cells(12).Value = s9
                dg.Rows(j).Cells(13).Value = s10
                dg.Rows(j).Cells(14).Value = s11
                dg.Rows(j).Cells(15).Value = s12
                dg.Rows(j).Cells(16).Value = s13
                dg.Rows(j).Cells(17).Value = s14
                dg.Rows(j).Cells(18).Value = s15
                dg.Rows(j).Cells(19).Value = s16
                dg.Rows(j).Cells(20).Value = s17
                dg.Rows(j).Cells(21).Value = s18
                dg.Rows(j).Cells(22).Value = s19
                dg.Rows(j).Cells(23).Value = s20
                dg.Rows(j).Cells(24).Value = s21
                dg.Rows(j).Cells(25).Value = s22
                dg.Rows(j).Cells(26).Value = s23
                dg.Rows(j).Cells(27).Value = s24
                dg.Rows(j).Cells(28).Value = s25
                dg.Rows(j).Cells(29).Value = s26
                dg.Rows(j).Cells(30).Value = s27
                dg.Rows(j).Cells(31).Value = s28
                dg.Rows(j).Cells(32).Value = s29
                dg.Rows(j).Cells(33).Value = s30
                dg.Rows(j).Cells(34).Value = s31
                dg.Rows(j).Cells(35).Value = mtot
                dg.Rows(j).Cells(36).Value = sno
                s1 = 0
                s2 = 0
                s3 = 0
                s4 = 0
                s5 = 0
                s6 = 0
                s7 = 0
                s8 = 0
                s9 = 0
                s10 = 0
                s11 = 0
                s12 = 0
                s13 = 0
                s14 = 0
                s15 = 0
                s16 = 0
                s17 = 0
                s18 = 0
                s19 = 0
                s20 = 0
                s21 = 0
                s22 = 0
                s23 = 0
                s24 = 0
                s25 = 0
                s26 = 0
                s27 = 0
                s28 = 0
                s29 = 0
                s30 = 0
                s31 = 0
                mtot = 0
                sno = sno + 1
                j = dg.Rows.Add()
                dg.Rows(j).Cells(36).Value = sno
                dg.Rows(j).DefaultCellStyle.BackColor = Color.BurlyWood
                sno = sno + 1
            End If
            j = dg.Rows.Add()
            lin = row(0)

            For i = 0 To dt.Columns.Count - 1
                dg.Rows(j).Cells(i).Value = row(i)
            Next i
            dg.Rows(j).Cells(i).Value = sno
            If IsDBNull(dg.Rows(j).Cells(2).Value) = False Then
                If Len(Trim(dg.Rows(j).Cells(2).Value)) > 0 Then
                    s1 = s1 + Val(dg.Rows(j).Cells(4).Value)
                    s2 = s2 + Val(dg.Rows(j).Cells(5).Value)
                    s3 = s3 + Val(dg.Rows(j).Cells(6).Value)
                    s4 = s4 + Val(dg.Rows(j).Cells(7).Value)
                    s5 = s5 + Val(dg.Rows(j).Cells(8).Value)
                    s6 = s6 + Val(dg.Rows(j).Cells(9).Value)
                    s7 = s7 + Val(dg.Rows(j).Cells(10).Value)
                    s8 = s8 + Val(dg.Rows(j).Cells(11).Value)
                    s9 = s9 + Val(dg.Rows(j).Cells(12).Value)
                    s10 = s10 + Val(dg.Rows(j).Cells(13).Value)
                    s11 = s11 + Val(dg.Rows(j).Cells(14).Value)
                    s12 = s12 + Val(dg.Rows(j).Cells(15).Value)
                    s13 = s13 + Val(dg.Rows(j).Cells(16).Value)
                    s14 = s14 + Val(dg.Rows(j).Cells(17).Value)
                    s15 = s15 + Val(dg.Rows(j).Cells(18).Value)
                    s16 = s16 + Val(dg.Rows(j).Cells(19).Value)
                    s17 = s17 + Val(dg.Rows(j).Cells(20).Value)
                    s18 = s18 + Val(dg.Rows(j).Cells(21).Value)
                    s19 = s19 + Val(dg.Rows(j).Cells(22).Value)
                    s20 = s20 + Val(dg.Rows(j).Cells(23).Value)
                    s21 = s21 + Val(dg.Rows(j).Cells(24).Value)
                    s22 = s22 + Val(dg.Rows(j).Cells(25).Value)
                    s23 = s23 + Val(dg.Rows(j).Cells(26).Value)
                    s24 = s24 + Val(dg.Rows(j).Cells(27).Value)
                    s25 = s25 + Val(dg.Rows(j).Cells(28).Value)
                    s26 = s26 + Val(dg.Rows(j).Cells(29).Value)
                    s27 = s27 + Val(dg.Rows(j).Cells(30).Value)
                    s28 = s28 + Val(dg.Rows(j).Cells(31).Value)
                    s29 = s29 + Val(dg.Rows(j).Cells(32).Value)
                    s30 = s30 + Val(dg.Rows(j).Cells(33).Value)
                    s31 = s31 + Val(dg.Rows(j).Cells(34).Value)
                    mtot = mtot + Val(dg.Rows(j).Cells(35).Value)
                End If
            End If
            sno = sno + 1
        Next

        j = dg.Rows.Add()
        dg.Rows(j).Cells(1).Value = 0
        dg.Rows(j).Cells(2).Value = lin & " - Total..."
        dg.Rows(j).Cells(4).Value = s1
        dg.Rows(j).Cells(5).Value = s2
        dg.Rows(j).Cells(6).Value = s3
        dg.Rows(j).Cells(7).Value = s4
        dg.Rows(j).Cells(8).Value = s5
        dg.Rows(j).Cells(9).Value = s6
        dg.Rows(j).Cells(10).Value = s7
        dg.Rows(j).Cells(11).Value = s8
        dg.Rows(j).Cells(12).Value = s9
        dg.Rows(j).Cells(13).Value = s10
        dg.Rows(j).Cells(14).Value = s11
        dg.Rows(j).Cells(15).Value = s12
        dg.Rows(j).Cells(16).Value = s13
        dg.Rows(j).Cells(17).Value = s14
        dg.Rows(j).Cells(18).Value = s15
        dg.Rows(j).Cells(19).Value = s16
        dg.Rows(j).Cells(20).Value = s17
        dg.Rows(j).Cells(21).Value = s18
        dg.Rows(j).Cells(22).Value = s19
        dg.Rows(j).Cells(23).Value = s20
        dg.Rows(j).Cells(24).Value = s21
        dg.Rows(j).Cells(25).Value = s22
        dg.Rows(j).Cells(26).Value = s23
        dg.Rows(j).Cells(27).Value = s24
        dg.Rows(j).Cells(28).Value = s25
        dg.Rows(j).Cells(29).Value = s26
        dg.Rows(j).Cells(30).Value = s27
        dg.Rows(j).Cells(31).Value = s28
        dg.Rows(j).Cells(32).Value = s29
        dg.Rows(j).Cells(33).Value = s30
        dg.Rows(j).Cells(34).Value = s31
        dg.Rows(j).Cells(35).Value = mtot
        dg.Rows(j).Cells(36).Value = sno
        dg.Rows(j).Cells(36).Value = Format(gettotal(), "#######0.00")
        dg.Rows(j).DefaultCellStyle.BackColor = Color.BurlyWood
        'dg.Columns(i).DefaultCellStyle.BackColor = Color.Linen
        'dg.Columns(i).DefaultCellStyle.ForeColor = Color.DarkSlateGray

        dg.ReadOnly = True

        'For k = 1 To dg.Rows.Count - 1
        '    If Len(Trim(dg.Rows(k).Cells(2).Value)) > 0 Then
        '        s1 = s1 + Val(dg.Rows(k).Cells(4).Value)
        '        s2 = s2 + Val(dg.Rows(k).Cells(5).Value)
        '        s3 = s3 + Val(dg.Rows(k).Cells(6).Value)
        '        s4 = s4 + Val(dg.Rows(k).Cells(7).Value)
        '        s5 = s5 + Val(dg.Rows(k).Cells(8).Value)
        '        s6 = s6 + Val(dg.Rows(k).Cells(9).Value)
        '        s7 = s7 + Val(dg.Rows(k).Cells(10).Value)
        '        s8 = s8 + Val(dg.Rows(k).Cells(11).Value)
        '        s9 = s9 + Val(dg.Rows(k).Cells(12).Value)
        '        s10 = s10 + Val(dg.Rows(k).Cells(13).Value)
        '        s11 = s11 + Val(dg.Rows(k).Cells(14).Value)
        '        s12 = s12 + Val(dg.Rows(k).Cells(15).Value)
        '        s13 = s13 + Val(dg.Rows(k).Cells(16).Value)
        '        s14 = s14 + Val(dg.Rows(k).Cells(17).Value)
        '        s15 = s15 + Val(dg.Rows(k).Cells(18).Value)
        '        s16 = s16 + Val(dg.Rows(k).Cells(19).Value)
        '        s17 = s17 + Val(dg.Rows(k).Cells(20).Value)
        '        s18 = s18 + Val(dg.Rows(k).Cells(21).Value)
        '        s19 = s19 + Val(dg.Rows(k).Cells(22).Value)
        '        s20 = s20 + Val(dg.Rows(k).Cells(23).Value)
        '        s21 = s21 + Val(dg.Rows(k).Cells(24).Value)
        '        s22 = s22 + Val(dg.Rows(k).Cells(25).Value)
        '        s23 = s23 + Val(dg.Rows(k).Cells(26).Value)
        '        s24 = s24 + Val(dg.Rows(k).Cells(27).Value)
        '        s25 = s25 + Val(dg.Rows(k).Cells(28).Value)
        '        s26 = s26 + Val(dg.Rows(k).Cells(29).Value)
        '        s27 = s27 + Val(dg.Rows(k).Cells(30).Value)
        '        s28 = s28 + Val(dg.Rows(k).Cells(31).Value)
        '        s29 = s29 + Val(dg.Rows(k).Cells(32).Value)
        '        s30 = s30 + Val(dg.Rows(k).Cells(33).Value)
        '        s31 = s31 + Val(dg.Rows(k).Cells(34).Value)
        '        mtot = mtot + Val(dg.Rows(k).Cells(35).Value)
        '    End If
        'Next k

        'Dim myrow = dt.NewRow

        ''  myrow(0) = "Amount"

        ''  myrow(1) = total

        ''dt.Rows.Add(myrow

        'myrow(0) = ""
        'myrow(1) = 0
        'myrow(2) = "Total..."
        ''myrow(3) = ""
        'myrow(4) = s1
        'myrow(5) = s2
        'myrow(6) = s3
        'myrow(7) = s4
        'myrow(8) = s5
        'myrow(9) = s6
        'myrow(10) = s7
        'myrow(11) = s8
        'myrow(12) = s9
        'myrow(13) = s10
        'myrow(14) = s11
        'myrow(15) = s12
        'myrow(16) = s13
        'myrow(17) = s14
        'myrow(18) = s15
        'myrow(19) = s16
        'myrow(20) = s17
        'myrow(21) = s18
        'myrow(22) = s19
        'myrow(23) = s20
        'myrow(24) = s21
        'myrow(25) = s22
        'myrow(26) = s23
        'myrow(27) = s24
        'myrow(28) = s25
        'myrow(29) = s26
        'myrow(30) = s27
        'myrow(31) = s28
        'myrow(32) = s29
        'myrow(33) = s30
        'myrow(34) = s31
        'myrow(35) = mtot
        'dt.Rows.Add(myrow)

        'dg.Rows(dg.RowCount - 1).Cells(2).Value = "Total..."
        'dg.Rows(dg.RowCount - 1).Cells(4).Value = s1
        'dg.Rows(dg.RowCount - 1).Cells(5).Value = s2
        'dg.Rows(dg.RowCount - 1).Cells(6).Value = s3
        'dg.Rows(dg.RowCount - 1).Cells(7).Value = s4
        'dg.Rows(dg.RowCount - 1).Cells(8).Value = s5
        'dg.Rows(dg.RowCount - 1).Cells(9).Value = s6
        'dg.Rows(dg.RowCount - 1).Cells(10).Value = s7
        'dg.Rows(dg.RowCount - 1).Cells(11).Value = s8
        'dg.Rows(dg.RowCount - 1).Cells(12).Value = s9
        'dg.Rows(dg.RowCount - 1).Cells(13).Value = s10
        'dg.Rows(dg.RowCount - 1).Cells(14).Value = s11
        'dg.Rows(dg.RowCount - 1).Cells(15).Value = s12
        'dg.Rows(dg.RowCount - 1).Cells(16).Value = s13
        'dg.Rows(dg.RowCount - 1).Cells(17).Value = s14
        'dg.Rows(dg.RowCount - 1).Cells(18).Value = s15
        'dg.Rows(dg.RowCount - 1).Cells(19).Value = s16
        'dg.Rows(dg.RowCount - 1).Cells(20).Value = s17
        'dg.Rows(dg.RowCount - 1).Cells(21).Value = s18
        'dg.Rows(dg.RowCount - 1).Cells(22).Value = s19
        'dg.Rows(dg.RowCount - 1).Cells(23).Value = s20
        'dg.Rows(dg.RowCount - 1).Cells(24).Value = s21
        'dg.Rows(dg.RowCount - 1).Cells(25).Value = s22
        'dg.Rows(dg.RowCount - 1).Cells(26).Value = s23
        'dg.Rows(dg.RowCount - 1).Cells(27).Value = s24
        'dg.Rows(dg.RowCount - 1).Cells(28).Value = s25
        'dg.Rows(dg.RowCount - 1).Cells(29).Value = s26
        'dg.Rows(dg.RowCount - 1).Cells(30).Value = s27
        'dg.Rows(dg.RowCount - 1).Cells(31).Value = s28
        'dg.Rows(dg.RowCount - 1).Cells(32).Value = s29
        'dg.Rows(dg.RowCount - 1).Cells(33).Value = s30
        'dg.Rows(dg.RowCount - 1).Cells(34).Value = s31
        'dg.Rows(dg.RowCount - 1).Cells(35).Value = mtot
        dt.Dispose()

    End Sub
    Private Sub calcincharge()
        Dim s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16, s17, s18, s19, s20, s21, s22, s23, s24, s25, s26, s27, s28, s29, s30, s31, mtot As Double

        MSQL = " declare @d1 as nvarchar(20) " & vbCrLf _
              & "declare @d2 as nvarchar(20) " & vbCrLf _
              & "declare @rr as real " & vbCrLf _
              & " declare @dday as integer  " & vbCrLf _
              & " declare @incper as real" & vbCrLf _
              & " set @incper=" & Val(txtinc.Text) & vbCrLf _
              & " set @rr=" & Val(txtinc.Text) & vbCrLf _
              & "set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'" & vbCrLf _
              & "set @d2='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'" & vbCrLf _
              & "set @dday= (datediff(day, dateadd(day, 1-day(@d1), @d1),dateadd(month, 1, dateadd(day, 1-day(@d1), @d1)))) " & vbCrLf
        'old
        ''MSQL = MSQL & " select empno,empname,jobgrade,isnull([1],0) as [1],isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4]," & vbCrLf _
        '' & "isnull([5],0) as [5],isnull([6],0) as [6],isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10]," & vbCrLf _
        '' & "isnull([11],0) as [11],isnull([12],0) as [12],isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16]," & vbCrLf _
        '' & "isnull([17],0) as [17],isnull([18],0) as [18],isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22]," & vbCrLf _
        '' & "isnull([23],0) as [23],isnull([24],0) as [24],isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29]," & vbCrLf _
        '' & "isnull([30],0) as [30],isnull([31],0) as [31]," & vbCrLf _
        '' & "(isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+ " & vbCrLf _
        '' & "isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+" & vbCrLf _
        '' & "isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+ " & vbCrLf _
        '' & "isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+" & vbCrLf _
        '' & "isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc  from " & vbCrLf

        '**Average
        If optavg.Checked = True Then
            'old
            'MSQL = MSQL & " select empno,empname,jobgrade,isnull([1],0) as [1],isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4]," & vbCrLf _
            ' & "isnull([5],0) as [5],isnull([6],0) as [6],isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10]," & vbCrLf _
            ' & "isnull([11],0) as [11],isnull([12],0) as [12],isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16]," & vbCrLf _
            ' & "isnull([17],0) as [17],isnull([18],0) as [18],isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22]," & vbCrLf _
            ' & "isnull([23],0) as [23],isnull([24],0) as [24],isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29]," & vbCrLf _
            ' & "isnull([30],0) as [30],isnull([31],0) as [31]," & vbCrLf _
            ' & "(isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+ " & vbCrLf _
            ' & "isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+" & vbCrLf _
            ' & "isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+ " & vbCrLf _
            ' & "isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+" & vbCrLf _
            ' & "isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc  from " & vbCrLf


            ''MSQL = MSQL & "(select k.dat,k.empno,k.empname,k.jobgrade,SUM(k.lineinchargamt) amt from " & vbCrLf _
            '' & "(select case when cc.dat IS null then datepart(d,kk.Date) else cc.dat end dat, kk.jobgrade,kk.empno,em.empname, kk.[lineno],isnull(cc.lineamt,0) lineamt," & vbCrLf _
            '' & "case when ISNULL(cc.lineamt,0)>0 then ROUND( (isnull(cc.lineamt,0)/isnull(cc.noper,0)) * @incper,0) else 0 end  lineinchargamt,isnull(round(em.salary/@dday,0),0) dsalary from " & vbCrLf
            ''MSQL = MSQL & "(select c.date, c.empid,c.empno,c.jobgrade,c.[lineno] from " & Trim(mcostdbnam) & ".dbo.operf b  " & vbCrLf _
            '' & "left join " & Trim(mcostdbnam) & ".dbo.perf1 c on c.bno=b.bno  " & vbCrLf _
            '' & "where  b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<=' " & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and c.jobgrade in ('XI')  " & vbCrLf _
            '' & " group by c.date,c.[lineno], c.empid,c.empno,c.jobgrade) kk " & vbCrLf _
            '' & "left join ( " & vbCrLf _
            '' & " select l.dat,l.[lineno],l.lineamt,SUM(l.lineamt) over() tot,isnull(l.noper,0) noper from " & vbCrLf _
            '' & "(select j.dat, j.[lineno],j.totincentive  lineamt,isnull(j.noper,0) noper from   " & vbCrLf _
            '' & "(select b.date,DATEPART(d,b.date) dat, b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, (b.nomac*d.nopcs) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr, " & vbCrLf _
            '' & "(b.totprodqty-(b.nomac*d.nopcs)) as incqty, round((isnull(f.incentive,0)*c.pper)/100,2) incentive,  " & vbCrLf _
            '' & " case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*round((f.incentive*c.pper)/100,2)),0) else 0 end amt,em.salary, " & vbCrLf _
            '' & "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary, " & vbCrLf _
            '' & " (datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday,  " & vbCrLf _
            '' & " SUM(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*f.incentive),0) else 0 end) over (partition by b.DATE, b.[lineno]) as totincentive, " & vbCrLf _
            '' & " sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno])  totsalary, COUNT(c.empid) over (PARTITION by b.date, b.[lineno]) noper " & vbCrLf _
            '' & " from " & Trim(mcostdbnam) & ".dbo.operf b  " & vbCrLf _
            '' & "left join (select l.bno,l.empid,l.empno,l.jobgrade,l.[lineno],l.pper from  " & vbCrLf _
            '' & "(select bno, empid,empno,jobgrade,[lineno],opername,totprod,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade) mprod ,COUNT(empno) over(partition by bno,[lineno],jobgrade,empno) cnt,  " & vbCrLf _
            '' & " case when totprod>0 and COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)<=1 then round((convert(real,totprod)/convert(real,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade)))*100,0)  " & vbCrLf _
            '' & " else case  when COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)>1 then 100 else  0 end end pper,incyn    from " & Trim(mcostdbnam) & ".dbo.perf1) l  " & vbCrLf _
            '' & " where incyn not in ('N'))  c on c.bno=b.bno and c.[lineno]=b.[lineno]  left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) d on d.brand=b.brand  " & vbCrLf _
            '' & " left join " & Trim(mcostdbnam) & ".dbo.empmaster em on em.nemp_id=c.empid  " & vbCrLf _
            '' & " Left Join " & vbCrLf _
            '' & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b  " & vbCrLf _
            '' & " Left Join " & vbCrLf _
            '' & "(select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
            '' & "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade " & vbCrLf _
            '' & " where  (b.totprodqty-(b.nomac*d.nopcs)) > 0  and date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) j  " & vbCrLf _
            '' & " group by j.[lineno],j.totincentive,j.noper,j.dat) l ) cc on cc.[lineno]=kk.[lineno] left join " & Trim(mcostdbnam) & ".dbo.empmaster em on em.nemp_id=kk.empid  " & vbCrLf _
            '' & " group by case when cc.dat IS null then datepart(d,kk.Date) else cc.dat end, kk.jobgrade,kk.empno,em.empname, kk.[lineno],isnull(cc.lineamt,0),isnull(cc.noper,0),case when ISNULL(cc.lineamt,0)>0 then ROUND((isnull(cc.lineamt,0)/isnull(cc.noper,0))*@incper,0) else 0 end ,isnull(round(em.salary/@dday,0),0)) k " & vbCrLf _
            '' & " group by  k.dat,k.empno,k.empname,k.jobgrade) s " & vbCrLf
            'old
            'MSQL = MSQL & "(select k.dat,k.empno,k.empname,k.jobgrade,SUM(k.lineinchargamt) amt from ( " & vbCrLf _
            ' & " select case when cc.dat IS null then datepart(d,kk.Date) else cc.dat end dat, kk.jobgrade,kk.empno,em.empname, kk.[lineno],isnull(cc.lineamt,0) lineamt, " & vbCrLf _
            ' & " case when ISNULL(cc.lineamt,0)>0 then ROUND((isnull(cc.lineamt,0)/isnull(cc.nomanpwr,0)) * @incper,0) else 0 end  lineinchargamt,isnull(round(em.salary/@dday,0),0) dsalary from  " & vbCrLf _
            ' & "(select c.date, c.empid,c.empno,c.jobgrade,c.[lineno],DATEPART(d,b.date) adat from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
            ' & "left join prodcost.dbo.perf1 c with (nolock) on c.bno=b.bno  " & vbCrLf _
            ' & "where  b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and c.jobgrade in ('XI') " & vbCrLf _
            ' & "group by c.date,c.[lineno], c.empid,c.empno,c.jobgrade,DATEPART(d,b.date)) kk  " & vbCrLf _
            ' & "   Left Join " & vbCrLf _
            ' & "(select k.dat,k.[lineno],SUM(k.amt) lineamt,k.nomanpwr from  " & vbCrLf _
            ' & "(select s.[Lineno],s.empno,s.empname,s.jobgrade,SUM(s.amt) amt, s.dat,s.nomanpwr from  " & vbCrLf _
            ' & "(select b.date,DATEPART(d,b.date) dat, b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr, " & vbCrLf _
            ' & " isnull((b.totprodqty-(b.nomac*d.nopcs)),0) as incqty, round((isnull(f.incentive,0)*c.pper)/100,2)  incentive,  " & vbCrLf _
            ' & "isnull(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))* round((f.incentive*c.pper)/100,2)),0) else 0 end,0) amt,isnull(em.salary,0) salary, " & vbCrLf _
            ' & " isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary, " & vbCrLf _
            ' & "(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday,  " & vbCrLf _
            ' & "SUM(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*f.incentive),0) else 0 end) over (partition by b.[lineno]) as totincentive,  " & vbCrLf _
            ' & " sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary  " & vbCrLf _
            ' & "from prodcost.dbo.operf b with (nolock) " & vbCrLf _
            ' & "left join (select l.bno,l.empid,l.empno,l.jobgrade,l.[lineno],l.pper from   " & vbCrLf _
            ' & "(select bno, empid,empno,jobgrade,[lineno],opername,totprod,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade) mprod ,COUNT(empno) over(partition by bno,[lineno],jobgrade,empno) cnt,   " & vbCrLf _
            ' & "case when totprod>0 and COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)<=1 then round((convert(real,totprod)/convert(real,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade)))*100,0)   " & vbCrLf _
            ' & "else case  when COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)>1 then 100 else  0 end end pper,incyn    from prodcost.dbo.perf1 with (nolock)) l   " & vbCrLf _
            ' & "where incyn not in ('N'))  c on c.bno=b.bno and c.[lineno]=b.[lineno]  left join (select brand,nopcs from prodcost.dbo.incentivemaster with (nolock) group by brand,nopcs) d on d.brand=b.brand   " & vbCrLf _
            ' & "left join prodcost.dbo.empmaster em with (nolock) on em.nemp_id=c.empid   " & vbCrLf _
            ' & " Left Join " & vbCrLf _
            ' & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from prodcost.dbo.incentivemaster b with (nolock)  " & vbCrLf _
            ' & " Left Join " & vbCrLf _
            ' & "(select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from  " & vbCrLf _
            ' & "(select brand, nprodpcsfr,nprodpcsto from  prodcost.dbo.incentivemaster with (nolock) group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade  " & vbCrLf _
            ' & "where    date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'  and (b.totprodqty-(b.nomac*d.nopcs)) > 0  ) s  " & vbCrLf _
            ' & "group by s.[Lineno],s.empno,s.empname,s.jobgrade,s.dat,s.nomanpwr ) k " & vbCrLf _
            ' & "group by k.dat,k.[lineno],k.nomanpwr) cc on cc.[lineno]=kk.[lineno] and cc.dat=kk.adat " & vbCrLf _
            ' & " left join prodcost.dbo.empmaster em on em.nemp_id=kk.empid   " & vbCrLf _
            '  & " group by case when cc.dat IS null then datepart(d,kk.Date) else cc.dat end, kk.jobgrade,kk.empno,em.empname, kk.[lineno],isnull(cc.lineamt,0), " & vbCrLf _
            '  & "case when ISNULL(cc.lineamt,0)>0 then ROUND((isnull(cc.lineamt,0)/isnull(cc.nomanpwr,0)) * @incper,0)  else 0 end ,isnull(round(em.salary/@dday,0),0)  ) k  " & vbCrLf _
            '  & "group by  k.dat,k.empno,k.empname,k.jobgrade) s  " & vbCrLf

            'MSQL = MSQL & " PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
            '& "([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P " & vbCrLf


            'new
            'new
            MSQL = MSQL & "select isnull(jj.empno,0) empno,isnull(jj.empname,'') empname, ll.[lineno] ,(sum(ll.[1])*@rr)/100 [1],(sum(ll.[2])*@rr)/100 [2],(sum(ll.[3])*@rr)/100 [3],(sum(ll.[4])*@rr)/100 [4],(sum(ll.[5])*@rr)/100 [5],(sum(ll.[6])*@rr)/100 [6],(sum(ll.[7])*@rr)/100 [7],(sum(ll.[8])*@rr)/100 [8],(sum(ll.[9])*@rr)/100 [9],(sum(ll.[10])*@rr)/100 [10]," & vbCrLf _
                   & "(sum(ll.[11])*@rr)/100 [11],(sum(ll.[12])*@rr)/100 [12],(sum(ll.[13])*@rr)/100 [13],(sum(ll.[14])*@rr)/100 [14],(sum(ll.[15])*@rr)/100 [15],(sum(ll.[16])*@rr)/100 [16],(sum(ll.[17])*@rr)/100 [17],(sum(ll.[18])*@rr)/100 [18],(sum(ll.[19])*@rr)/100 [19],(sum(ll.[20])*@rr)/100 [20], " & vbCrLf _
                   & "(sum(ll.[21])*@rr)/100 [21],(sum(ll.[22])*@rr)/100 [22],(sum(ll.[23])*@rr)/100 [23],(sum(ll.[24])*@rr)/100 [24],(sum(ll.[25])*@rr)/100 [25],(sum(ll.[26])*@rr)/100 [26],(sum(ll.[27])*@rr)/100 [27],(sum(ll.[28])*@rr)/100 [28],(sum(ll.[29])*@rr)/100 [29],(sum(ll.[30])*@rr)/100 [30], " & vbCrLf _
                   & "(sum(ll.[31])*@rr)/100 [31],   (sum(ll.totinc)*@rr)/100 Totinc from ( " & vbCrLf _
                   & "select 2 sno, [lineno],  " & vbCrLf _
                   & "   isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6],  " & vbCrLf _
                   & "   isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12],  " & vbCrLf _
                   & "   isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18],  " & vbCrLf _
                   & "   isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24],  " & vbCrLf _
                   & "   isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30],  " & vbCrLf _
                   & "   isnull([31],0) as [31], " & vbCrLf _
                   & "   (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+   " & vbCrLf _
                   & "   isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+   " & vbCrLf _
                   & "   isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+  " & vbCrLf _
                   & "   isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+  " & vbCrLf _
                   & "   isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from ( " & vbCrLf
            'new W/O manpower
            'MSQL = MSQL & " select s.[linno] [Lineno],s.empno,s.empname,s.jobgrade,sum(isnull(s.Actincentive,0)) amt,datepart(d,s.date) dat from (  " & vbCrLf _
            '   & "       select k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs,k.totprod,k.jobgrade,k.rate,k.totcnt,k.avgpcs,k.operexspcs,k.totincentamt,  " & vbCrLf _
            '   & "       case when isnull(k.operexspcs,0)>0 then sum(k.operexspcs) over (partition by k.opername,k.date,k.linno) else 0 end totoppcs,  " & vbCrLf _
            '   & "       round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per, " & vbCrLf _
            '   & "       round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive from (  " & vbCrLf _
            '   & "       select c.date,c.bno, c.[lineno] linno,c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,isnull(j.pcs,0)*8 sampcs, b.totprod,b.jobgrade,i.incentive rate,  " & vbCrLf _
            '   & "       count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt,(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs,  " & vbCrLf _
            '   & "       b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs2,  " & vbCrLf _
            '   & "       case when (b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) else 0 end operexspcs,  " & vbCrLf _
            '   & "       case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then (c.totprodqty-(c.nomac*i.nopcs))*i.incentive else 0 end totincentamt   from prodcost.dbo.perf1 b  " & vbCrLf _
            '   & "       inner join prodcost.dbo.operf c on c.bno=b.bno  " & vbCrLf _
            '   & "       left join prodcost.dbo.PROCESSJOBMASTER j on j.OPERNAME=b.opername  " & vbCrLf _
            '   & "       left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade  " & vbCrLf _
            '   & "       where c.date>=@d1 and c.date<=@d2 and  b.jobgrade not in ('XII') ) k  ) s   " & vbCrLf _
            '   & "       group by  s.[linno],s.empno,s.empname,s.jobgrade,s.date " & vbCrLf

            '** new manpower
            MSQL = MSQL & "select s.[linno] [Lineno],s.empno,s.empname,s.jobgrade,sum(isnull(s.Actincentive,0)) amt,datepart(d,s.date) dat from (  " & vbCrLf _
                        & "  select k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs,k.totprod,k.jobgrade,k.rate,k.totcnt,k.MANPOWER, k.avgpcs,k.operexspcs,k.totincentamt, " & vbCrLf _
                        & "  case when isnull(k.operexspcs,0)>0 then sum(k.operexspcs) over (partition by k.opername,k.date,k.linno) else 0 end totoppcs,  " & vbCrLf _
                        & "  round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per,  " & vbCrLf _
                        & "  round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive from (  " & vbCrLf _
                        & "  select c.date,c.bno, c.[lineno] linno,c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,(isnull(j.pcs,0)*8) * j.MANPOWER sampcs, b.totprod,b.jobgrade,i.incentive rate,  " & vbCrLf _
                        & "  count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt, j.MANPOWER,((isnull(j.pcs,0)*8)*j.manpower)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs, " & vbCrLf _
                        & "  b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs2,  " & vbCrLf _
                        & "  case when (b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) else 0 end operexspcs, " & vbCrLf _
                        & "  case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then ((c.totprodqty-(c.nomac*i.nopcs))*(i.incentive*j.manpower)) else 0 end totincentamt   from prodcost.dbo.perf1 b  " & vbCrLf _
                        & "  inner join prodcost.dbo.operf c on c.bno=b.bno  " & vbCrLf _
                        & "  left join prodcost.dbo.PROCESSJOBMASTER j on j.OPERNAME=b.opername and rtrim(ltrim(replace(j.style,'FULL','')))=rtrim(ltrim(c.style)) " & vbCrLf _
                        & "  left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade  " & vbCrLf _
                        & "  where c.date>=@d1 and c.date<=@d2 and  b.jobgrade not in ('XII') ) k ) s   " & vbCrLf _
                        & "  group by  s.[linno],s.empno,s.empname,s.jobgrade,s.date " & vbCrLf




                            MSQL = MSQL & "  ) k  " & vbCrLf _
                                   & " PIVOT (SUM(amt) FOR [dat] IN  " & vbCrLf _
                                   & " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P ) ll " & vbCrLf _
                                   & " left join (select empid,empno,empname,[lineno],jobgrade from prodcost.dbo.perf1 where date>=@d1 and date<=@d2 and jobgrade='XI' " & vbCrLf _
                                   & " group by empid,empno,empname,[lineno],jobgrade) jj on jj.[lineno]=ll.[lineno] " & vbCrLf _
                                   & " group by jj.empid,jj.empno,jj.empname, ll.[lineno] " & vbCrLf

        End If
        '**2.5% 
        If opt25.Checked = True Then
            MSQL = MSQL & "(select k.dat,k.empno,k.empname,k.jobgrade,SUM(k.lineinchargamt) amt from ( " & vbCrLf _
                & " select case when cc.dat IS null then datepart(d,kk.Date) else cc.dat end dat, kk.jobgrade,kk.empno,em.empname, kk.[lineno],isnull(cc.lineamt,0) lineamt, " & vbCrLf _
                & " case when ISNULL(cc.lineamt,0)>0 then ROUND((isnull(cc.lineamt,0)*2.5)/100,0) else 0 end  lineinchargamt,isnull(round(em.salary/@dday,0),0) dsalary from  " & vbCrLf _
                & "(select c.date, c.empid,c.empno,c.jobgrade,c.[lineno],DATEPART(d,b.date) adat from prodcost.dbo.operf b  " & vbCrLf _
                & "left join prodcost.dbo.perf1 c on c.bno=b.bno  " & vbCrLf _
                & "where  b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and c.jobgrade in ('XI') " & vbCrLf _
                & "group by c.date,c.[lineno], c.empid,c.empno,c.jobgrade,DATEPART(d,b.date)) kk  " & vbCrLf _
                & "   Left Join " & vbCrLf _
                & "(select k.dat,k.[lineno],SUM(k.amt) lineamt from  " & vbCrLf _
                & "(select s.[Lineno],s.empno,s.empname,s.jobgrade,SUM(s.amt) amt, s.dat from  " & vbCrLf _
                & "(select b.date,DATEPART(d,b.date) dat, b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr, " & vbCrLf _
                & " isnull((b.totprodqty-(b.nomac*d.nopcs)),0) as incqty, round((isnull(f.incentive,0)*c.pper)/100,2)  incentive,  " & vbCrLf _
                & "isnull(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))* round((f.incentive*c.pper)/100,2)),0) else 0 end,0) amt,isnull(em.salary,0) salary, " & vbCrLf _
                & " isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary, " & vbCrLf _
                & "(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday,  " & vbCrLf _
                & "SUM(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*f.incentive),0) else 0 end) over (partition by b.[lineno]) as totincentive,  " & vbCrLf _
                & " sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary  " & vbCrLf _
                & "from prodcost.dbo.operf b with (nolock) " & vbCrLf _
                & "left join (select l.bno,l.empid,l.empno,l.jobgrade,l.[lineno],l.pper from   " & vbCrLf _
                & "(select bno, empid,empno,jobgrade,[lineno],opername,totprod,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade) mprod ,COUNT(empno) over(partition by bno,[lineno],jobgrade,empno) cnt,   " & vbCrLf _
                & "case when totprod>0 and COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)<=1 then round((convert(real,totprod)/convert(real,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade)))*100,0)   " & vbCrLf _
                & "else case  when COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)>1 then 100 else  0 end end pper,incyn    from prodcost.dbo.perf1 with (nolock)) l   " & vbCrLf _
                & "where incyn not in ('N'))  c on c.bno=b.bno and c.[lineno]=b.[lineno]  left join (select brand,nopcs from prodcost.dbo.incentivemaster with (nolock) group by brand,nopcs) d on d.brand=b.brand   " & vbCrLf _
                & "left join prodcost.dbo.empmaster em with (nolock) on em.nemp_id=c.empid   " & vbCrLf _
                & " Left Join " & vbCrLf _
                & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from prodcost.dbo.incentivemaster b with (nolock)  " & vbCrLf _
                & " Left Join " & vbCrLf _
                & "(select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from  " & vbCrLf _
                & "(select brand, nprodpcsfr,nprodpcsto from  prodcost.dbo.incentivemaster with (nolock) group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade  " & vbCrLf _
                & "where    date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'  and (b.totprodqty-(b.nomac*d.nopcs)) > 0  ) s  " & vbCrLf _
                & "group by s.[Lineno],s.empno,s.empname,s.jobgrade,s.dat ) k " & vbCrLf _
                & "group by k.dat,k.[lineno]) cc on cc.[lineno]=kk.[lineno] and cc.dat=kk.adat " & vbCrLf _
                & " left join prodcost.dbo.empmaster em on em.nemp_id=kk.empid   " & vbCrLf _
                 & " group by case when cc.dat IS null then datepart(d,kk.Date) else cc.dat end, kk.jobgrade,kk.empno,em.empname, kk.[lineno],isnull(cc.lineamt,0), " & vbCrLf _
                 & "case when ISNULL(cc.lineamt,0)>0 then ROUND((isnull(cc.lineamt,0)*2.5)/100,0) else 0 end ,isnull(round(em.salary/@dday,0),0)  ) k  " & vbCrLf _
                 & "group by  k.dat,k.empno,k.empname,k.jobgrade) s  " & vbCrLf
            MSQL = MSQL & " PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
        & "([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P " & vbCrLf



            '     'new
            '     MSQL = MSQL = "select jj.empno,jj.empname, ll.[lineno] ,(sum(ll.[1])*@rr)/100 [1],(sum(ll.[2])*@rr)/100 [2],(sum(ll.[3])*@rr)/100 [3],(sum(ll.[4])*@rr)/100 [4],(sum(ll.[5])*@rr)/100 [5],(sum(ll.[6])*@rr)/100 [6],(sum(ll.[7])*@rr)/100 [7],(sum(ll.[8])*@rr)/100 [8],(sum(ll.[9])*@rr)/100 [9],(sum(ll.[10])*@rr)/100 [10]," & vbCrLf _
            '            & "(sum(ll.[11])*@rr)/100 [11],(sum(ll.[12])*@rr)/100 [12],(sum(ll.[13])*@rr)/100 [13],(sum(ll.[14])*@rr)/100 [14],(sum(ll.[15])*@rr)/100 [15],(sum(ll.[16])*@rr)/100 [16],(sum(ll.[17])*@rr)/100 [17],(sum(ll.[18])*@rr)/100 [18],(sum(ll.[19])*@rr)/100 [19],(sum(ll.[20])*@rr)/100 [20], " & vbCrLf _
            '            & "(sum(ll.[21])*@rr)/100 [21],(sum(ll.[22])*@rr)/100 [22],(sum(ll.[23])*@rr)/100 [23],(sum(ll.[24])*@rr)/100 [24],(sum(ll.[25])*@rr)/100 [25],(sum(ll.[26])*@rr)/100 [26],(sum(ll.[27])*@rr)/100 [27],(sum(ll.[28])*@rr)/100 [28],(sum(ll.[29])*@rr)/100 [29],(sum(ll.[30])*@rr)/100 [30], " & vbCrLf _
            '            & "(sum(ll.[31])*@rr)/100 [31],   (sum(ll.totinc)*@rr)/100 Totinc from ( " & vbCrLf _
            '            & "select 2 sno, [lineno],  " & vbCrLf _
            '            & "   isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6],  " & vbCrLf _
            '            & "   isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12],  " & vbCrLf _
            '            & "   isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18],  " & vbCrLf _
            '            & "   isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24],  " & vbCrLf _
            '            & "   isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30],  " & vbCrLf _
            '            & "   isnull([31],0) as [31], " & vbCrLf _
            '            & "   (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+   " & vbCrLf _
            '            & "   isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+   " & vbCrLf _
            '            & "   isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+  " & vbCrLf _
            '            & "   isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+  " & vbCrLf _
            '            & "   isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from ( " & vbCrLf _
            '            & " select s.[linno] [Lineno],s.empno,s.empname,s.jobgrade,sum(isnull(s.Actincentive,0)) amt,datepart(d,s.date) dat from (  " & vbCrLf _
            '            & "       select k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs,k.totprod,k.jobgrade,k.rate,k.totcnt,k.avgpcs,k.operexspcs,k.totincentamt,  " & vbCrLf _
            '            & "       case when isnull(k.operexspcs,0)>0 then sum(k.operexspcs) over (partition by k.opername,k.date,k.linno) else 0 end totoppcs,  " & vbCrLf _
            '            & "       round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per, " & vbCrLf _
            '            & "       round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive from (  " & vbCrLf _
            '            & "       select c.date,c.bno, c.[lineno] linno,c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,isnull(j.pcs,0)*8 sampcs, b.totprod,b.jobgrade,i.incentive rate,  " & vbCrLf _
            '            & "       count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt,(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs,  " & vbCrLf _
            '            & "       b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs2,  " & vbCrLf _
            '            & "       case when (b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) else 0 end operexspcs,  " & vbCrLf _
            '            & "       case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then (c.totprodqty-(c.nomac*i.nopcs))*i.incentive else 0 end totincentamt   from prodcost.dbo.perf1 b  " & vbCrLf _
            '            & "       inner join prodcost.dbo.operf c on c.bno=b.bno  " & vbCrLf _
            '            & "       left join prodcost.dbo.PROCESSJOBMASTER j on j.OPERNAME=b.opername  " & vbCrLf _
            '            & "       left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade  " & vbCrLf _
            '            & "       where c.date>=@d1 and c.date<=@d2 and  b.jobgrade not in ('XI') ) k  ) s   " & vbCrLf _
            '            & "       group by  s.[linno],s.empno,s.empname,s.jobgrade,s.date " & vbCrLf _
            '            & "  ) k  " & vbCrLf _
            '            & " PIVOT (SUM(amt) FOR [dat] IN  " & vbCrLf _
            '            & " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P ) ll " & vbCrLf _
            '& " left join (select empid,empno,empname,[lineno],jobgrade from perf1 where date>=@d1 and date<=@d2 and jobgrade='XI' " & vbCrLf _
            '& " group by empid,empno,empname,[lineno],jobgrade) jj on jj.[lineno]=ll.[lineno] " & vbCrLf _
            '& " group by jj.empid,jj.empno,jj.empname, ll.[lineno] " & vbCrLf



        End If

        'MSQL = MSQL & " PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
        ' & "([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P " & vbCrLf









        Dim cmdV As New SqlCommand
        Dim dat1 As New SqlDataAdapter

        cmdV.CommandText = MSQL
        cmdV.Connection = con
        cmdV.CommandTimeout = 600
        dat1.SelectCommand = cmdV
        Dim dt1 As New DataTable
        dat1.Fill(dt1)
        dg1.DataSource = dt1
        dg1.Columns(0).Width = 30
        dg1.Columns(1).Width = 50
        dg1.Columns(2).Width = 90
        'dg1.Columns(3).Width = 50
        For j = 3 To 33
            dg1.Columns(j).Width = 40
        Next
        dg1.Columns(34).Width = 45

        s1 = 0
        s2 = 0
        s3 = 0
        s4 = 0
        s5 = 0
        s6 = 0
        s7 = 0
        s8 = 0
        s9 = 0
        s10 = 0
        s11 = 0
        s12 = 0
        s13 = 0
        s14 = 0
        s15 = 0
        s16 = 0
        s17 = 0
        s18 = 0
        s19 = 0
        s20 = 0
        s21 = 0
        s22 = 0
        s23 = 0
        s24 = 0
        s25 = 0
        s26 = 0
        s27 = 0
        s28 = 0
        s29 = 0
        s30 = 0
        s31 = 0
        mtot = 0
        For k = 0 To dg1.Rows.Count - 1
            If Len(Trim(dg1.Rows(k).Cells(1).Value)) > 0 Then
                s1 = s1 + Format(Val(dg1.Rows(k).Cells(3).Value), "####0.00")
                s2 = s2 + Format(Val(dg1.Rows(k).Cells(4).Value), "####0.00")
                s3 = s3 + Format(Val(dg1.Rows(k).Cells(5).Value), "####0.00")
                s4 = s4 + Format(Val(dg1.Rows(k).Cells(6).Value), "####0.00")
                s5 = s5 + Format(Val(dg1.Rows(k).Cells(7).Value), "####0.00")
                s6 = s6 + Format(Val(dg1.Rows(k).Cells(8).Value), "####0.00")
                s7 = s7 + Format(Val(dg1.Rows(k).Cells(9).Value), "####0.00")
                s8 = s8 + Format(Val(dg1.Rows(k).Cells(10).Value), "####0.00")
                s9 = s9 + Format(Val(dg1.Rows(k).Cells(11).Value), "####0.00")
                s10 = s10 + Format(Val(dg1.Rows(k).Cells(12).Value), "####0.00")
                s11 = s11 + Format(Val(dg1.Rows(k).Cells(13).Value), "####0.00")
                s12 = s12 + Format(Val(dg1.Rows(k).Cells(14).Value), "####0.00")
                s13 = s13 + Format(Val(dg1.Rows(k).Cells(15).Value), "####0.00")
                s14 = s14 + Format(Val(dg1.Rows(k).Cells(16).Value), "####0.00")
                s15 = s15 + Format(Val(dg1.Rows(k).Cells(17).Value), "####0.00")
                s16 = s16 + Format(Val(dg1.Rows(k).Cells(18).Value), "####0.00")
                s17 = s17 + Format(Val(dg1.Rows(k).Cells(19).Value), "####0.00")
                s18 = s18 + Format(Val(dg1.Rows(k).Cells(20).Value), "####0.00")
                s19 = s19 + Format(Val(dg1.Rows(k).Cells(21).Value), "####0.00")
                s20 = s20 + Format(Val(dg1.Rows(k).Cells(22).Value), "####0.00")
                s21 = s21 + Format(Val(dg1.Rows(k).Cells(23).Value), "####0.00")
                s22 = s22 + Format(Val(dg1.Rows(k).Cells(24).Value), "####0.00")
                s23 = s23 + Format(Val(dg1.Rows(k).Cells(25).Value), "####0.00")
                s24 = s24 + Format(Val(dg1.Rows(k).Cells(26).Value), "####0.00")
                s25 = s25 + Format(Val(dg1.Rows(k).Cells(27).Value), "####0.00")
                s26 = s26 + Format(Val(dg1.Rows(k).Cells(28).Value), "####0.00")
                s27 = s27 + Format(Val(dg1.Rows(k).Cells(29).Value), "####0.00")
                s28 = s28 + Format(Val(dg1.Rows(k).Cells(30).Value), "####0.00")
                s29 = s29 + Format(Val(dg1.Rows(k).Cells(31).Value), "####0.00")
                s30 = s30 + Format(Val(dg1.Rows(k).Cells(32).Value), "####0.00")
                s31 = s31 + Format(Val(dg1.Rows(k).Cells(33).Value), "####0.00")
                mtot = mtot + Format(Val(dg1.Rows(k).Cells(34).Value), "####0.00")
            End If
        Next k

        Dim myrow As DataRow = dt1.NewRow

        ''  myrow(0) = "Amount"

        ''  myrow(1) = total

        ''dt.Rows.Add(myrow
        Dim S As Integer = 0
        myrow(0) = S
        'myrow(1) = 0
        myrow(1) = "Total..."
        ''myrow(3) = ""
        myrow(3) = s1
        myrow(4) = s2
        myrow(5) = s3
        myrow(6) = s4
        myrow(7) = s5
        myrow(8) = s6
        myrow(9) = s7
        myrow(10) = s8
        myrow(11) = s9
        myrow(12) = s10
        myrow(13) = s11
        myrow(14) = s12
        myrow(15) = s13
        myrow(16) = s14
        myrow(17) = s15
        myrow(18) = s16
        myrow(19) = s17
        myrow(20) = s18
        myrow(21) = s19
        myrow(22) = s20
        myrow(23) = s21
        myrow(24) = s22
        myrow(25) = s23
        myrow(26) = s24
        myrow(27) = s25
        myrow(28) = s26
        myrow(29) = s27
        myrow(30) = s28
        myrow(31) = s29
        myrow(32) = s30
        myrow(33) = s31
        myrow(34) = mtot
        dt1.Rows.Add(myrow)

        dg1.Rows(dg1.RowCount - 1).Cells(1).Value = "Total..."
        dg1.Rows(dg1.RowCount - 1).Cells(3).Value = s1
        dg1.Rows(dg1.RowCount - 1).Cells(4).Value = s2
        dg1.Rows(dg1.RowCount - 1).Cells(5).Value = s3
        dg1.Rows(dg1.RowCount - 1).Cells(6).Value = s4
        dg1.Rows(dg1.RowCount - 1).Cells(7).Value = s5
        dg1.Rows(dg1.RowCount - 1).Cells(8).Value = s6
        dg1.Rows(dg1.RowCount - 1).Cells(9).Value = s7
        dg1.Rows(dg1.RowCount - 1).Cells(10).Value = s8
        dg1.Rows(dg1.RowCount - 1).Cells(11).Value = s9
        dg1.Rows(dg1.RowCount - 1).Cells(12).Value = s10
        dg1.Rows(dg1.RowCount - 1).Cells(13).Value = s11
        dg1.Rows(dg1.RowCount - 1).Cells(14).Value = s12
        dg1.Rows(dg1.RowCount - 1).Cells(15).Value = s13
        dg1.Rows(dg1.RowCount - 1).Cells(16).Value = s14
        dg1.Rows(dg1.RowCount - 1).Cells(17).Value = s15
        dg1.Rows(dg1.RowCount - 1).Cells(18).Value = s16
        dg1.Rows(dg1.RowCount - 1).Cells(19).Value = s17
        dg1.Rows(dg1.RowCount - 1).Cells(20).Value = s18
        dg1.Rows(dg1.RowCount - 1).Cells(21).Value = s19
        dg1.Rows(dg1.RowCount - 1).Cells(22).Value = s20
        dg1.Rows(dg1.RowCount - 1).Cells(23).Value = s21
        dg1.Rows(dg1.RowCount - 1).Cells(24).Value = s22
        dg1.Rows(dg1.RowCount - 1).Cells(25).Value = s23
        dg1.Rows(dg1.RowCount - 1).Cells(26).Value = s24
        dg1.Rows(dg1.RowCount - 1).Cells(27).Value = s25
        dg1.Rows(dg1.RowCount - 1).Cells(28).Value = s26
        dg1.Rows(dg1.RowCount - 1).Cells(29).Value = s27
        dg1.Rows(dg1.RowCount - 1).Cells(30).Value = s28
        dg1.Rows(dg1.RowCount - 1).Cells(31).Value = s29
        dg1.Rows(dg1.RowCount - 1).Cells(32).Value = s30
        dg1.Rows(dg1.RowCount - 1).Cells(33).Value = s31
        dg1.Rows(dg1.RowCount - 1).Cells(34).Value = mtot
        dt1.Dispose()
        cmdV.Dispose()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Cursor = Cursors.WaitCursor
        mtotincent = True
        Call calctotinc()
        Call calcincharge()
        Cursor = Cursors.Default
    End Sub


    Private Sub targetsum()

        MSQL = "select l.[Lineno],l.ptype, sum(l.[1]) [1],sum(l.[2]) [2] ,sum(l.[3]) [3],sum(l.[4]) [4],sum(l.[5]) [5],sum(l.[6]) [6],sum(l.[7]) [7], " & vbCrLf _
        & " sum(l.[8]) [8],sum(l.[9]) [9],sum(l.[10]) [10],sum(l.[11]) [11],sum(l.[12]) [12],sum(l.[13]) [13],sum(l.[14]) [14],  " & vbCrLf _
        & "sum(l.[15]) [15],sum(l.[16]) [16],sum(l.[17]) [17],sum(l.[18]) [18],sum(l.[19]) [19],sum(l.[20]) [20],sum(l.[21]) [21],  " & vbCrLf _
        & "sum(l.[22]) [22],sum(l.[23]) [23],sum(l.[24]) [24],sum(l.[25]) [25],sum(l.[26]) [26],sum(l.[27]) [27],  " & vbCrLf _
        & "sum(l.[28]) [28],sum(l.[29]) [29],sum(l.[30]) [30],sum(l.[31]) [31] from  " & vbCrLf _
        & "(select [Lineno],ptype, isnull([1],0) [1],isnull([2],0) [2] ,ISNULL([3],0) [3],isnull([4],0) [4],isnull([5],0) [5],isnull([6],0) [6],isnull([7],0) [7],  " & vbCrLf _
        & "isnull([8],0) [8],isnull([9],0) [9],isnull([10],0) [10],isnull([11],0) [11],isnull([12],0) [12],isnull([13],0) [13],isnull([14],0) [14],  " & vbCrLf _
        & "isnull([15],0) [15],isnull([16],0) [16],isnull([17],0) [17],isnull([18],0) [18],isnull([19],0) [19],isnull([20],0) [20],isnull([21],0) [21],  " & vbCrLf _
        & "isnull([22],0) [22],isnull([23],0) [23],isnull([24],0) [24],isnull([25],0) [25],isnull([26],0) [26],isnull([27],0) [27],  " & vbCrLf _
        & " isnull([28],0) [28],isnull([29],0) [29],isnull([30],0) [30],isnull([31],0) [31]  from  " & vbCrLf _
        & "(select k.dat,k.[lineno],k.brand,'ProdQty' ptype, isnull(sum(k.totprodqty),0) totprodqty from   " & vbCrLf _
        & "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b  " & vbCrLf _
        & "inner join (select brand,nopcs from incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        & " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        & " union all  " & vbCrLf _
        & "select k.dat,k.[lineno],k.brand,'TargetQty' ptype, isnull(sum(k.target),0) target from   " & vbCrLf _
        & "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b  " & vbCrLf _
        & "inner join (select brand,nopcs from incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        & "group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        & " union all  " & vbCrLf _
        & " select k.dat,k.[lineno],k.brand,'ZExcessQty' ptype, isnull(sum(k.exqty),0) exqty from   " & vbCrLf _
        & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b  " & vbCrLf _
        & " inner join (select brand,nopcs from incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        & " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        & " union all  " & vbCrLf _
        & " select k.dat,k.[lineno],k.brand,'Machine' ptype, isnull(sum(k.nomac),0) exqty from   " & vbCrLf _
        & "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b  " & vbCrLf _
        & " inner join (select brand,nopcs from incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        & " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
        & " union all  " & vbCrLf _
        & " select k.dat,k.[lineno],k.brand,'Nopcs' ptype, isnull(sum(k.nopcs),0) exqty from   " & vbCrLf _
        & "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,c.nopcs, b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b  " & vbCrLf _
        & " inner join (select brand,nopcs from incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
        & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
        & " group by k.dat,k.[lineno],k.brand ) s  " & vbCrLf _
        & " pivot (sum(totprodqty) FOR [dat] IN  " & vbCrLf _
        & " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) Pv1) l  " & vbCrLf _
        & " group by l.[Lineno],l.ptype  " & vbCrLf _
        & " order by l.[lineno],l.ptype  " & vbCrLf

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        If MsgBox("Individual Incentive with Attendance!", vbYesNo) = vbYes Then
            Call loadempincentiveatt()
            matt = True
        Else
            matt = False
            '' MSQL = "select kj.empno,kj.empname,sum(kj.total) total from ( select lk.[lineno],lk.empno,lk.empname,lk.jobgrade," & vbCrLf _
            ''                  & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
            ''                 & "(case when [1]>0 then [1] else 0 end + " & vbCrLf _
            ''                 & "case when [2]>0 then [2] else 0 end +  " & vbCrLf _
            ''                 & " case when [3]>0 then [3] else 0 end +  " & vbCrLf _
            ''                 & " case when [4]>0 then [4] else 0 end +  " & vbCrLf _
            ''                 & " case when [5]>0 then [5] else 0 end +  " & vbCrLf _
            ''                 & " case when [6]>0 then [6] else 0 end +case when [7]>0 then [7] else 0 end +case when [8]>0 then [8] else 0 end +  " & vbCrLf _
            ''                 & " case when [9]>0 then [9] else 0 end + case when [10]>0 then [10] else 0 end +case when [11]>0 then [11] else 0 end +case when [12]>0 then [12] else 0 end +  " & vbCrLf _
            ''                 & " case when [13]>0 then [13] else 0 end +case when [14]>0 then [14] else 0 end +case when [15]>0 then [15] else 0 end +case when [16]>0 then [16] else 0 end +  " & vbCrLf _
            ''                 & " case when [17]>0 then [17] else 0 end + case when [18]>0 then [18] else 0 end + case when [19]>0 then [19] else 0 end + case when [20]>0 then [20] else 0 end +  " & vbCrLf _
            ''                 & " case when [21]>0 then [21] else 0 end + case when [22]>0 then [22] else 0 end + case when [23]>0 then [23] else 0 end + case when [24]>0 then [24] else 0 end + case when [25]>0 then [25] else 0 end +  " & vbCrLf _
            ''                 & " case when [26]>0 then [26] else 0 end + case when [27]>0 then [27] else 0 end + case when [28]>0 then [28] else 0 end + case when [29]>0 then [29] else 0 end +  " & vbCrLf _
            ''                 & " case when [30]>0 then [30] else 0 end + case when [31]>0 then [31] else 0 end ) as total from " & vbCrLf




            '' MSQL = MSQL & "(select 1 sno,l.[Lineno],'' empno,''empname,l.ptype jobgrade, sum(l.[1]) [1],sum(l.[2]) [2] ,sum(l.[3]) [3],sum(l.[4]) [4],sum(l.[5]) [5],sum(l.[6]) [6],sum(l.[7]) [7], " & vbCrLf _
            ''& " sum(l.[8]) [8],sum(l.[9]) [9],sum(l.[10]) [10],sum(l.[11]) [11],sum(l.[12]) [12],sum(l.[13]) [13],sum(l.[14]) [14],  " & vbCrLf _
            ''& "sum(l.[15]) [15],sum(l.[16]) [16],sum(l.[17]) [17],sum(l.[18]) [18],sum(l.[19]) [19],sum(l.[20]) [20],sum(l.[21]) [21],  " & vbCrLf _
            ''& "sum(l.[22]) [22],sum(l.[23]) [23],sum(l.[24]) [24],sum(l.[25]) [25],sum(l.[26]) [26],sum(l.[27]) [27],  " & vbCrLf _
            ''& "sum(l.[28]) [28],sum(l.[29]) [29],sum(l.[30]) [30],sum(l.[31]) [31],0 totinc  from  " & vbCrLf _
            ''& "(select [Lineno],ptype, isnull([1],0) [1],isnull([2],0) [2] ,ISNULL([3],0) [3],isnull([4],0) [4],isnull([5],0) [5],isnull([6],0) [6],isnull([7],0) [7],  " & vbCrLf _
            ''& "isnull([8],0) [8],isnull([9],0) [9],isnull([10],0) [10],isnull([11],0) [11],isnull([12],0) [12],isnull([13],0) [13],isnull([14],0) [14],  " & vbCrLf _
            ''& "isnull([15],0) [15],isnull([16],0) [16],isnull([17],0) [17],isnull([18],0) [18],isnull([19],0) [19],isnull([20],0) [20],isnull([21],0) [21],  " & vbCrLf _
            ''& "isnull([22],0) [22],isnull([23],0) [23],isnull([24],0) [24],isnull([25],0) [25],isnull([26],0) [26],isnull([27],0) [27],  " & vbCrLf _
            ''& " isnull([28],0) [28],isnull([29],0) [29],isnull([30],0) [30],isnull([31],0) [31]  from  " & vbCrLf _
            ''& "(select k.dat,k.[lineno],k.brand,'ProdQty' ptype, isnull(sum(k.totprodqty),0) totprodqty from   " & vbCrLf _
            ''& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
            ''& "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            ''& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            ''& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
            ''& " union all  " & vbCrLf _
            ''& "select k.dat,k.[lineno],k.brand,'TargetQty' ptype, isnull(sum(k.target),0) target from   " & vbCrLf _
            ''& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
            ''& "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            ''& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            ''& "group by k.dat,k.[lineno],k.brand  " & vbCrLf _
            ''& " union all  " & vbCrLf _
            ''& " select k.dat,k.[lineno],k.brand,'ZExcessQty' ptype, isnull(sum(k.exqty),0) exqty from   " & vbCrLf _
            ''& " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
            ''& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            ''& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            ''& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
            ''& " union all  " & vbCrLf _
            ''& " select k.dat,k.[lineno],k.brand,'Machine' ptype, isnull(sum(k.nomac),0) exqty from   " & vbCrLf _
            ''& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
            ''& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            ''& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            ''& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
            ''& " union all  " & vbCrLf _
            ''& " select k.dat,k.[lineno],k.brand,'Nopcs' ptype, isnull(sum(k.nopcs),0) exqty from   " & vbCrLf _
            ''& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,c.nopcs, b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
            ''& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            ''& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            ''& " group by k.dat,k.[lineno],k.brand ) s  " & vbCrLf _
            ''& " pivot (sum(totprodqty) FOR [dat] IN  " & vbCrLf _
            ''& " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) Pv1) l  " & vbCrLf _
            ''& " group by l.[Lineno],l.ptype  " & vbCrLf


            '' '*****
            '' MSQL = MSQL & " union all select 2 sno, [lineno],empno,empname,jobgrade, " & vbCrLf _
            ''    & " isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6]," & vbCrLf _
            ''    & " isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12]," & vbCrLf _
            ''    & " isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18]," & vbCrLf _
            ''    & "isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24]," & vbCrLf _
            ''    & " isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30]," & vbCrLf _
            ''    & " isnull([31],0) as [31]," & vbCrLf _
            ''    & " (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+ " & vbCrLf _
            ''    & "isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+ " & vbCrLf _
            ''    & "isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+ " & vbCrLf _
            ''    & " isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+ " & vbCrLf _
            ''    & " isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from " & vbCrLf

            '' MSQL = MSQL & "(select s.[Lineno],s.empno,s.empname,s.jobgrade,SUM(s.amt) amt, s.dat from " & vbCrLf _
            ''         & "(select b.date,DATEPART(d,b.date) dat, b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr," & vbCrLf _
            ''         & "isnull((b.totprodqty-(b.nomac*d.nopcs)),0) as incqty, round((isnull(f.incentive,0)*c.pper)/100,2)  incentive, " & vbCrLf _
            ''         & "isnull(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))* round((f.incentive*c.pper)/100,2)),0) else 0 end,0) amt,isnull(em.salary,0) salary," & vbCrLf _
            ''         & "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
            ''         & "(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
            ''         & "SUM(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*f.incentive),0) else 0 end) over (partition by b.[lineno]) as totincentive, " & vbCrLf _
            ''         & "sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary " & vbCrLf _
            ''         & "from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)" & vbCrLf _
            ''         & "left join (select l.bno,l.empid,l.empno,l.jobgrade,l.[lineno],l.pper from  " & vbCrLf _
            ''         & "(select bno, empid,empno,jobgrade,[lineno],opername,totprod,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade) mprod ,COUNT(empno) over(partition by bno,[lineno],jobgrade,empno) cnt,  " & vbCrLf _
            ''         & "case when totprod>0 and COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)<=1 then round((convert(real,totprod)/convert(real,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade)))*100,0)  " & vbCrLf _
            ''         & "else case  when COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)>1 then 100 else  0 end end pper,incyn    from " & Trim(mcostdbnam) & ".dbo.perf1 with (nolock)) l  " & vbCrLf _
            ''         & "where incyn not in ('N'))  c on c.bno=b.bno and c.[lineno]=b.[lineno]  " & vbCrLf _
            ''         & "left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) d on d.brand=b.brand  " & vbCrLf _
            ''         & "left join " & Trim(mcostdbnam) & ".dbo.empmaster em with (nolock) on em.nemp_id=c.empid  " & vbCrLf _
            ''         & "Left Join" & vbCrLf _
            ''         & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b with (nolock) " & vbCrLf _
            ''         & " Left Join " & vbCrLf _
            ''         & "(select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
            ''         & "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade " & vbCrLf _
            ''         & "where    date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'   and (b.totprodqty-(b.nomac*d.nopcs)) > 0  ) s " & vbCrLf _
            ''       & "group by s.[Lineno],s.empno,s.empname,s.jobgrade,s.dat ) k " & vbCrLf

            '' MSQL = MSQL & "  PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
            ''   & "([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P  ) lk) kj " & vbCrLf _
            ''   & " group by kj.empno,kj.empname" & vbCrLf _
            ''   & " order by kj.empno "
            '' '& " order by  lk.[lineno],lk.sno,lk.jobgrade,lk.empno"

            ' '********new rw
            ' '' " CASE when jj.incentive>=50 then jj.incentive else 0 end nincentive,  case when sum(jj.incentive) over(partition by jj.empid)>=50 then   sum(jj.incentive) over(partition by jj.empid) else 0 end totempinc," & vbCrLf 

            ' MSQL = " SELECT JJ.[LINENO], jj.jobgrade,jj.empid, jj.empno,jj.empname,jj.total ,COUNT(jj.empno) over (partition by jj.[lineno]) cmpcnt2,ll.empcnt, jj.incentive, " & vbCrLf _
            '       & " CASE when jj.incentive>=50 then jj.incentive else 0 end nincentive,  case when sum(jj.incentive) over(partition by jj.empid)>=50 then   case when sum(jj.incentive) over(partition by jj.empid)> jj.incenceiling  then jj.incenceiling else  sum(jj.incentive) over(partition by jj.empid) end else 0 end totempinc," & vbCrLf _
            '       & " SUM(CASE when jj.incentive>=50 then jj.incentive else 0 end) over(partition by jj.[lineno]) totincentive," & vbCrLf _
            '       & " round((SUM(CASE when jj.incentive>=50 then jj.incentive else 0 end) over(partition by jj.[lineno])/ll.empcnt)*1.25,2)  inchargeamt from ( " & vbCrLf

            ' MSQL = MSQL & "select kj.[lineno], jj.jobgrade,kj.empid, kj.empno,kj.empname,sum(kj.total) total, case when sum(kj.total)>ll.incenceiling and ll.active='Y' then ll.incenceiling else sum(kj.total) end Incentive ,ll.incenceiling from ( select lk.[lineno],lk.empid, lk.empno,lk.empname,lk.jobgrade," & vbCrLf _
            '          & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
            '         & "(case when [1]>0 then [1] else 0 end + " & vbCrLf _
            '         & "case when [2]>0 then [2] else 0 end +  " & vbCrLf _
            '         & " case when [3]>0 then [3] else 0 end +  " & vbCrLf _
            '         & " case when [4]>0 then [4] else 0 end +  " & vbCrLf _
            '         & " case when [5]>0 then [5] else 0 end +  " & vbCrLf _
            '         & " case when [6]>0 then [6] else 0 end +case when [7]>0 then [7] else 0 end +case when [8]>0 then [8] else 0 end +  " & vbCrLf _
            '         & " case when [9]>0 then [9] else 0 end + case when [10]>0 then [10] else 0 end +case when [11]>0 then [11] else 0 end +case when [12]>0 then [12] else 0 end +  " & vbCrLf _
            '         & " case when [13]>0 then [13] else 0 end +case when [14]>0 then [14] else 0 end +case when [15]>0 then [15] else 0 end +case when [16]>0 then [16] else 0 end +  " & vbCrLf _
            '         & " case when [17]>0 then [17] else 0 end + case when [18]>0 then [18] else 0 end + case when [19]>0 then [19] else 0 end + case when [20]>0 then [20] else 0 end +  " & vbCrLf _
            '         & " case when [21]>0 then [21] else 0 end + case when [22]>0 then [22] else 0 end + case when [23]>0 then [23] else 0 end + case when [24]>0 then [24] else 0 end + case when [25]>0 then [25] else 0 end +  " & vbCrLf _
            '         & " case when [26]>0 then [26] else 0 end + case when [27]>0 then [27] else 0 end + case when [28]>0 then [28] else 0 end + case when [29]>0 then [29] else 0 end +  " & vbCrLf _
            '         & " case when [30]>0 then [30] else 0 end + case when [31]>0 then [31] else 0 end ) as total from " & vbCrLf
            ' MSQL = MSQL & "(select 1 sno,l.[Lineno],'' empid,'' empno,''empname,l.ptype jobgrade, sum(l.[1]) [1],sum(l.[2]) [2] ,sum(l.[3]) [3],sum(l.[4]) [4],sum(l.[5]) [5],sum(l.[6]) [6],sum(l.[7]) [7], " & vbCrLf _
            '& " sum(l.[8]) [8],sum(l.[9]) [9],sum(l.[10]) [10],sum(l.[11]) [11],sum(l.[12]) [12],sum(l.[13]) [13],sum(l.[14]) [14],  " & vbCrLf _
            '& "sum(l.[15]) [15],sum(l.[16]) [16],sum(l.[17]) [17],sum(l.[18]) [18],sum(l.[19]) [19],sum(l.[20]) [20],sum(l.[21]) [21],  " & vbCrLf _
            '& "sum(l.[22]) [22],sum(l.[23]) [23],sum(l.[24]) [24],sum(l.[25]) [25],sum(l.[26]) [26],sum(l.[27]) [27],  " & vbCrLf _
            '& "sum(l.[28]) [28],sum(l.[29]) [29],sum(l.[30]) [30],sum(l.[31]) [31],0 totinc  from  " & vbCrLf _
            '& "(select [Lineno],ptype, isnull([1],0) [1],isnull([2],0) [2] ,ISNULL([3],0) [3],isnull([4],0) [4],isnull([5],0) [5],isnull([6],0) [6],isnull([7],0) [7],  " & vbCrLf _
            '& "isnull([8],0) [8],isnull([9],0) [9],isnull([10],0) [10],isnull([11],0) [11],isnull([12],0) [12],isnull([13],0) [13],isnull([14],0) [14],  " & vbCrLf _
            '& "isnull([15],0) [15],isnull([16],0) [16],isnull([17],0) [17],isnull([18],0) [18],isnull([19],0) [19],isnull([20],0) [20],isnull([21],0) [21],  " & vbCrLf _
            '& "isnull([22],0) [22],isnull([23],0) [23],isnull([24],0) [24],isnull([25],0) [25],isnull([26],0) [26],isnull([27],0) [27],  " & vbCrLf _
            '& " isnull([28],0) [28],isnull([29],0) [29],isnull([30],0) [30],isnull([31],0) [31]  from  " & vbCrLf _
            '& "(select k.dat,k.[lineno],k.brand,'ProdQty' ptype, isnull(sum(k.totprodqty),0) totprodqty from   " & vbCrLf _
            '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,(b.totprodqty-isnull(b.totrejqty,0)) totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
            '& "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            '& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
            '& " union all  " & vbCrLf _
            '& "select k.dat,k.[lineno],k.brand,'TargetQty' ptype, isnull(sum(k.target),0) target from   " & vbCrLf _
            '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
            '& "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            '& "group by k.dat,k.[lineno],k.brand  " & vbCrLf _
            '& " union all  " & vbCrLf _
            '& " select k.dat,k.[lineno],k.brand,'ZExcessQty' ptype, isnull(sum(k.exqty),0) exqty from   " & vbCrLf _
            '& " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
            '& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            '& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
            '& " union all  " & vbCrLf _
            '& " select k.dat,k.[lineno],k.brand,'Machine' ptype, isnull(sum(k.nomac),0) exqty from   " & vbCrLf _
            '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
            '& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            '& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
            '& " union all  " & vbCrLf _
            '& " select k.dat,k.[lineno],k.brand,'Nopcs' ptype, isnull(sum(k.nopcs),0) exqty from   " & vbCrLf _
            '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,c.nopcs, b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
            '& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            '& " group by k.dat,k.[lineno],k.brand ) s  " & vbCrLf _
            '& " pivot (sum(totprodqty) FOR [dat] IN  " & vbCrLf _
            '& " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) Pv1) l  " & vbCrLf _
            '& " group by l.[Lineno],l.ptype  " & vbCrLf
            ' '*****
            ' MSQL = MSQL & " union all select 2 sno, [lineno],empid,empno,empname,jobgrade, " & vbCrLf _
            '    & " isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6]," & vbCrLf _
            '    & " isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12]," & vbCrLf _
            '    & " isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18]," & vbCrLf _
            '    & "isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24]," & vbCrLf _
            '    & " isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30]," & vbCrLf _
            '    & " isnull([31],0) as [31]," & vbCrLf _
            '    & " (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+ " & vbCrLf _
            '    & "isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+ " & vbCrLf _
            '    & "isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+ " & vbCrLf _
            '    & " isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+ " & vbCrLf _
            '    & " isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from " & vbCrLf
            ' MSQL = MSQL & "(select s.[Lineno],s.empid, s.empno,s.empname,s.jobgrade,SUM(s.amt) amt, s.dat from " & vbCrLf _
            '                & "(select b.date,DATEPART(d,b.date) dat, b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr," & vbCrLf _
            '                & "isnull((b.totprodqty-(b.nomac*d.nopcs)),0) as incqty, round(isnull(c.incentiveamt,0),2) as incentive,  " & vbCrLf _
            '                & "isnull(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))* c.incentiveamt),2) else 0 end,0) amt,isnull(em.salary,0) salary," & vbCrLf _
            '                & "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
            '                & "(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
            '                & "SUM(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*c.incentiveamt),0) else 0 end) over (partition by b.date,b.[lineno]) as totincentive, " & vbCrLf _
            '                & "sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary " & vbCrLf _
            '                & "from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)" & vbCrLf

            ' MSQL = MSQL & "left join (select j.brand,j.bno,j.empid,j.empno,j.[lineno],SUM(j.incentiveamt) incentiveamt from (" & vbCrLf _
            '             & " select c.brand, b.bno, b.empid,b.empno,b.jobgrade,b.[lineno],b.opername,b.totprod,b.totmin, (b.totmin/convert(real,480))*100 perc, " & vbCrLf _
            '             & " (b.totmin/convert(real,480))*100 pper,  case when isnull(b.totmin,0)>0 then round(i.incentive* (b.totmin/convert(real,480)),2) else 0 end incentiveamt, " & vbCrLf _
            '             & " i.incentive, b.incyn    from prodcost.dbo.perf1 b " & vbCrLf _
            '             & " left join prodcost.dbo.operf c on c.bno=b.bno and c.date=b.date " & vbCrLf _
            '             & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade)j " & vbCrLf _
            '             & " group by j.brand,j.bno,j.empid,j.empno,j.[lineno])   c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf


            ' MSQL = MSQL & "left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) d on d.brand=b.brand  " & vbCrLf _
            '    & "left join " & Trim(mcostdbnam) & ".dbo.empmaster em with (nolock) on em.nemp_id=c.empid  " & vbCrLf _
            '    & "Left Join" & vbCrLf _
            '    & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b with (nolock) " & vbCrLf _
            '    & " Left Join " & vbCrLf _
            '    & "(select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
            '    & "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade " & vbCrLf _
            '    & "where    date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'   and (b.totprodqty-(b.nomac*d.nopcs)) > 0  ) s " & vbCrLf _
            '  & "group by s.[Lineno],s.empid,s.empno,s.empname,s.jobgrade,s.dat ) k " & vbCrLf

            ' MSQL = MSQL & "  PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
            '   & "([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P  ) lk) kj " & vbCrLf _
            '   & " left join prodcost.dbo.empmaster jj on jj.nempno=kj.empno and jj.nemp_id=kj.empid " & vbCrLf _
            '   & " left join prodcost.dbo.incentive_ceiling ll on ll.jobgrade=jj.jobgrade " & vbCrLf _
            '   & " where jj.jobgrade not in ('XII') group by kj.empid,kj.empno,kj.empname,jj.jobgrade,ll.incenceiling,ll.active,kj.[lineno]) jj" & vbCrLf

            ' MSQL = MSQL & " left join (select j.[lineno],AVG(j.cnt) empcnt from( select k.date,k.[lineno],COUNT(k.empid) cnt from  " & vbCrLf _
            ' & " (select DATE,[lineno],empid,SUM(totmin) totmin from prodcost.dbo.perf1 with (nolock) where DATE>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and totprod>0 and  jobgrade in ('I','II','III','IV','V','VI') " & vbCrLf _
            ' & "  group by date,[lineno],empid ) k group by k.date,k.[lineno])j " & vbCrLf _
            ' & " group by j.[lineno] ) ll on ll.[lineno]=jj.[Lineno] " & vbCrLf _
            ' & " order by jj.[lineno],jj.jobgrade "


            ' ''New with RW


            '' MSQL = " SELECT JJ.[LINENO], jj.jobgrade,jj.empid, jj.empno,jj.empname,jj.total ,COUNT(jj.empno) over (partition by jj.[lineno]) cmpcnt2,ll.empcnt, jj.incentive, " & vbCrLf _
            ''     & " CASE when jj.incentive>=50 then jj.incentive else 0 end nincentive,  case when sum(jj.incentive) over(partition by jj.empid)>=50 then   case when sum(jj.incentive) over(partition by jj.empid)> jj.incenceiling  then jj.incenceiling else  sum(jj.incentive) over(partition by jj.empid) end else 0 end totempinc," & vbCrLf _
            ''     & " SUM(CASE when jj.incentive>=50 then jj.incentive else 0 end) over(partition by jj.[lineno]) totincentive," & vbCrLf _
            ''     & " round((SUM(CASE when jj.incentive>=50 then jj.incentive else 0 end) over(partition by jj.[lineno])/ll.empcnt)*1.25,2)  inchargeamt from ( " & vbCrLf

            '' MSQL = MSQL & "select kj.[lineno], jj.jobgrade,kj.empid, kj.empno,kj.empname,sum(kj.total) total, case when sum(kj.total)>ll.incenceiling and ll.active='Y' then ll.incenceiling else sum(kj.total) end Incentive ,ll.incenceiling from ( select lk.[lineno],lk.empid, lk.empno,lk.empname,lk.jobgrade," & vbCrLf _
            ''          & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
            ''         & "(case when [1]>0 then [1] else 0 end + " & vbCrLf _
            ''         & "case when [2]>0 then [2] else 0 end +  " & vbCrLf _
            ''         & " case when [3]>0 then [3] else 0 end +  " & vbCrLf _
            ''         & " case when [4]>0 then [4] else 0 end +  " & vbCrLf _
            ''         & " case when [5]>0 then [5] else 0 end +  " & vbCrLf _
            ''         & " case when [6]>0 then [6] else 0 end +case when [7]>0 then [7] else 0 end +case when [8]>0 then [8] else 0 end +  " & vbCrLf _
            ''         & " case when [9]>0 then [9] else 0 end + case when [10]>0 then [10] else 0 end +case when [11]>0 then [11] else 0 end +case when [12]>0 then [12] else 0 end +  " & vbCrLf _
            ''         & " case when [13]>0 then [13] else 0 end +case when [14]>0 then [14] else 0 end +case when [15]>0 then [15] else 0 end +case when [16]>0 then [16] else 0 end +  " & vbCrLf _
            ''         & " case when [17]>0 then [17] else 0 end + case when [18]>0 then [18] else 0 end + case when [19]>0 then [19] else 0 end + case when [20]>0 then [20] else 0 end +  " & vbCrLf _
            ''         & " case when [21]>0 then [21] else 0 end + case when [22]>0 then [22] else 0 end + case when [23]>0 then [23] else 0 end + case when [24]>0 then [24] else 0 end + case when [25]>0 then [25] else 0 end +  " & vbCrLf _
            ''         & " case when [26]>0 then [26] else 0 end + case when [27]>0 then [27] else 0 end + case when [28]>0 then [28] else 0 end + case when [29]>0 then [29] else 0 end +  " & vbCrLf _
            ''         & " case when [30]>0 then [30] else 0 end + case when [31]>0 then [31] else 0 end ) as total from " & vbCrLf
            '' MSQL = MSQL & "(select 1 sno,l.[Lineno],'' empid,'' empno,''empname,l.ptype jobgrade, sum(l.[1]) [1],sum(l.[2]) [2] ,sum(l.[3]) [3],sum(l.[4]) [4],sum(l.[5]) [5],sum(l.[6]) [6],sum(l.[7]) [7], " & vbCrLf _
            ''& " sum(l.[8]) [8],sum(l.[9]) [9],sum(l.[10]) [10],sum(l.[11]) [11],sum(l.[12]) [12],sum(l.[13]) [13],sum(l.[14]) [14],  " & vbCrLf _
            ''& "sum(l.[15]) [15],sum(l.[16]) [16],sum(l.[17]) [17],sum(l.[18]) [18],sum(l.[19]) [19],sum(l.[20]) [20],sum(l.[21]) [21],  " & vbCrLf _
            ''& "sum(l.[22]) [22],sum(l.[23]) [23],sum(l.[24]) [24],sum(l.[25]) [25],sum(l.[26]) [26],sum(l.[27]) [27],  " & vbCrLf _
            ''& "sum(l.[28]) [28],sum(l.[29]) [29],sum(l.[30]) [30],sum(l.[31]) [31],0 totinc  from  " & vbCrLf _
            ''& "(select [Lineno],ptype, isnull([1],0) [1],isnull([2],0) [2] ,ISNULL([3],0) [3],isnull([4],0) [4],isnull([5],0) [5],isnull([6],0) [6],isnull([7],0) [7],  " & vbCrLf _
            ''& "isnull([8],0) [8],isnull([9],0) [9],isnull([10],0) [10],isnull([11],0) [11],isnull([12],0) [12],isnull([13],0) [13],isnull([14],0) [14],  " & vbCrLf _
            ''& "isnull([15],0) [15],isnull([16],0) [16],isnull([17],0) [17],isnull([18],0) [18],isnull([19],0) [19],isnull([20],0) [20],isnull([21],0) [21],  " & vbCrLf _
            ''& "isnull([22],0) [22],isnull([23],0) [23],isnull([24],0) [24],isnull([25],0) [25],isnull([26],0) [26],isnull([27],0) [27],  " & vbCrLf _
            ''& " isnull([28],0) [28],isnull([29],0) [29],isnull([30],0) [30],isnull([31],0) [31]  from  " & vbCrLf _
            ''& "(select k.dat,k.[lineno],k.brand,'ProdQty' ptype, isnull(sum(k.totprodqty),0) totprodqty from   " & vbCrLf _
            ''& "(select datepart(d,b.date) dat, b.[lineno],b.brand,(b.totprodqty-isnull(b.totrejqty,0)) totprodqty,b.nomac,b.nomac*c.nopcs target,((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
            ''& "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            ''& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            ''& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
            ''& " union all  " & vbCrLf _
            ''& "select k.dat,k.[lineno],k.brand,'TargetQty' ptype, isnull(sum(k.target),0) target from   " & vbCrLf _
            ''& "(select datepart(d,b.date) dat, b.[lineno],b.brand,(b.totprodqty-isnull(b.totrejqty,0)) totprodqty,b.nomac,b.nomac*c.nopcs target,((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
            ''& "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            ''& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            ''& "group by k.dat,k.[lineno],k.brand  " & vbCrLf _
            ''& " union all  " & vbCrLf _
            ''& " select k.dat,k.[lineno],k.brand,'ZExcessQty' ptype, isnull(sum(k.exqty),0) exqty from   " & vbCrLf _
            ''& " (select datepart(d,b.date) dat, b.[lineno],b.brand,(b.totprodqty-isnull(b.totrejqty,0)) totprodqty,b.nomac,b.nomac*c.nopcs target,((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
            ''& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            ''& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            ''& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
            ''& " union all  " & vbCrLf _
            ''& " select k.dat,k.[lineno],k.brand,'Machine' ptype, isnull(sum(k.nomac),0) exqty from   " & vbCrLf _
            ''& "(select datepart(d,b.date) dat, b.[lineno],b.brand,(b.totprodqty-isnull(b.totrejqty,0)) totprodqty,b.nomac,b.nomac*c.nopcs target,((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
            ''& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            ''& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            ''& " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
            ''& " union all  " & vbCrLf _
            ''& " select k.dat,k.[lineno],k.brand,'Nopcs' ptype, isnull(sum(k.nopcs),0) exqty from   " & vbCrLf _
            ''& "(select datepart(d,b.date) dat, b.[lineno],b.brand,(b.totprodqty-isnull(b.totrejqty,0)) totprodqty,b.nomac,c.nopcs, b.nomac*c.nopcs target,((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
            ''& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            ''& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            ''& " group by k.dat,k.[lineno],k.brand ) s  " & vbCrLf _
            ''& " pivot (sum(totprodqty) FOR [dat] IN  " & vbCrLf _
            ''& " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) Pv1) l  " & vbCrLf _
            ''& " group by l.[Lineno],l.ptype  " & vbCrLf
            '' '*****
            '' MSQL = MSQL & " union all select 2 sno, [lineno],empid,empno,empname,jobgrade, " & vbCrLf _
            ''    & " isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6]," & vbCrLf _
            ''    & " isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12]," & vbCrLf _
            ''    & " isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18]," & vbCrLf _
            ''    & "isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24]," & vbCrLf _
            ''    & " isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30]," & vbCrLf _
            ''    & " isnull([31],0) as [31]," & vbCrLf _
            ''    & " (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+ " & vbCrLf _
            ''    & "isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+ " & vbCrLf _
            ''    & "isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+ " & vbCrLf _
            ''    & " isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+ " & vbCrLf _
            ''    & " isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from " & vbCrLf
            '' MSQL = MSQL & "(select s.[Lineno],s.empid, s.empno,s.empname,s.jobgrade,SUM(s.amt) amt, s.dat from " & vbCrLf _
            ''                & "(select b.date,DATEPART(d,b.date) dat, b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr," & vbCrLf _
            ''                & "isnull(((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*d.nopcs)),0) as incqty, round(isnull(c.incentiveamt,0),2) as incentive,  " & vbCrLf _
            ''                & "isnull(case when ((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*d.nopcs)) > 0 then round((((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*d.nopcs))* c.incentiveamt),2) else 0 end,0) amt,isnull(em.salary,0) salary," & vbCrLf _
            ''                & "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
            ''                & "(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
            ''                & "SUM(case when ((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*d.nopcs)) > 0 then round((((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*d.nopcs))*c.incentiveamt),0) else 0 end) over (partition by b.date,b.[lineno]) as totincentive, " & vbCrLf _
            ''                & "sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary " & vbCrLf _
            ''                & "from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)" & vbCrLf

            '' MSQL = MSQL & "left join (select j.brand,j.bno,j.empid,j.empno,j.[lineno],SUM(j.incentiveamt) incentiveamt from (" & vbCrLf _
            ''             & " select c.brand, b.bno, b.empid,b.empno,b.jobgrade,b.[lineno],b.opername,b.totprod,b.totmin, (b.totmin/convert(real,480))*100 perc, " & vbCrLf _
            ''             & " (b.totmin/convert(real,480))*100 pper,  case when isnull(b.totmin,0)>0 then round(i.incentive* (b.totmin/convert(real,480)),2) else 0 end incentiveamt, " & vbCrLf _
            ''             & " i.incentive, b.incyn    from prodcost.dbo.perf1 b " & vbCrLf _
            ''             & " left join prodcost.dbo.operf c on c.bno=b.bno and c.date=b.date " & vbCrLf _
            ''             & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade)j " & vbCrLf _
            ''             & " group by j.brand,j.bno,j.empid,j.empno,j.[lineno])   c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf


            '' MSQL = MSQL & "left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) d on d.brand=b.brand  " & vbCrLf _
            ''    & "left join " & Trim(mcostdbnam) & ".dbo.empmaster em with (nolock) on em.nemp_id=c.empid  " & vbCrLf _
            ''    & "Left Join" & vbCrLf _
            ''    & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b with (nolock) " & vbCrLf _
            ''    & " Left Join " & vbCrLf _
            ''    & "(select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
            ''    & "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade " & vbCrLf _
            ''    & "where    date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'   and (b.totprodqty-(b.nomac*d.nopcs)) > 0  ) s " & vbCrLf _
            ''  & "group by s.[Lineno],s.empid,s.empno,s.empname,s.jobgrade,s.dat ) k " & vbCrLf

            '' MSQL = MSQL & "  PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
            ''   & "([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P  ) lk) kj " & vbCrLf _
            ''   & " left join prodcost.dbo.empmaster jj on jj.nempno=kj.empno and jj.nemp_id=kj.empid " & vbCrLf _
            ''   & " left join prodcost.dbo.incentive_ceiling ll on ll.jobgrade=jj.jobgrade " & vbCrLf _
            ''   & " where jj.jobgrade not in ('XII') group by kj.empid,kj.empno,kj.empname,jj.jobgrade,ll.incenceiling,ll.active,kj.[lineno]) jj" & vbCrLf

            '' MSQL = MSQL & " left join (select j.[lineno],AVG(j.cnt) empcnt from( select k.date,k.[lineno],COUNT(k.empid) cnt from  " & vbCrLf _
            '' & " (select DATE,[lineno],empid,SUM(totmin) totmin from prodcost.dbo.perf1 with (nolock) where DATE>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and totprod>0 and  jobgrade in ('I','II','III','IV','V','VI') " & vbCrLf _
            '' & "  group by date,[lineno],empid ) k group by k.date,k.[lineno])j " & vbCrLf _
            '' & " group by j.[lineno] ) ll on ll.[lineno]=jj.[Lineno] " & vbCrLf _
            '' & " order by jj.[lineno],jj.jobgrade "



            '' New Merge Line

            ' MSQL = " SELECT JJ.[LINENO], jj.jobgrade,jj.empid, jj.empno,jj.empname,jj.total ,COUNT(jj.empno) over (partition by jj.[lineno]) cmpcnt2,ll.empcnt, jj.incentive, " & vbCrLf _
            '       & " CASE when jj.incentive>=50 then jj.incentive else 0 end nincentive,  case when sum(jj.incentive) over(partition by jj.empid)>=50 then   case when sum(jj.incentive) over(partition by jj.empid)> jj.incenceiling  then jj.incenceiling else  sum(jj.incentive) over(partition by jj.empid) end else 0 end totempinc," & vbCrLf _
            '       & " SUM(CASE when jj.incentive>=50 then jj.incentive else 0 end) over(partition by jj.[lineno]) totincentive," & vbCrLf _
            '       & " round((SUM(CASE when jj.incentive>=50 then jj.incentive else 0 end) over(partition by jj.[lineno])/ll.empcnt)*1.25,2)  inchargeamt from ( " & vbCrLf

            ' MSQL = MSQL & "select kj.[lineno], jj.jobgrade,kj.empid, kj.empno,kj.empname,sum(kj.total) total, case when sum(kj.total)>ll.incenceiling and ll.active='Y' then ll.incenceiling else sum(kj.total) end Incentive ,ll.incenceiling from ( " & vbCrLf _
            '             & " select lk.[lineno],lk.empid, lk.empno,lk.empname,lk.jobgrade," & vbCrLf _
            '          & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
            '         & "(case when [1]>0 then [1] else 0 end + " & vbCrLf _
            '         & "case when [2]>0 then [2] else 0 end +  " & vbCrLf _
            '         & " case when [3]>0 then [3] else 0 end +  " & vbCrLf _
            '         & " case when [4]>0 then [4] else 0 end +  " & vbCrLf _
            '         & " case when [5]>0 then [5] else 0 end +  " & vbCrLf _
            '         & " case when [6]>0 then [6] else 0 end +case when [7]>0 then [7] else 0 end +case when [8]>0 then [8] else 0 end +  " & vbCrLf _
            '         & " case when [9]>0 then [9] else 0 end + case when [10]>0 then [10] else 0 end +case when [11]>0 then [11] else 0 end +case when [12]>0 then [12] else 0 end +  " & vbCrLf _
            '         & " case when [13]>0 then [13] else 0 end +case when [14]>0 then [14] else 0 end +case when [15]>0 then [15] else 0 end +case when [16]>0 then [16] else 0 end +  " & vbCrLf _
            '         & " case when [17]>0 then [17] else 0 end + case when [18]>0 then [18] else 0 end + case when [19]>0 then [19] else 0 end + case when [20]>0 then [20] else 0 end +  " & vbCrLf _
            '         & " case when [21]>0 then [21] else 0 end + case when [22]>0 then [22] else 0 end + case when [23]>0 then [23] else 0 end + case when [24]>0 then [24] else 0 end + case when [25]>0 then [25] else 0 end +  " & vbCrLf _
            '         & " case when [26]>0 then [26] else 0 end + case when [27]>0 then [27] else 0 end + case when [28]>0 then [28] else 0 end + case when [29]>0 then [29] else 0 end +  " & vbCrLf _
            '         & " case when [30]>0 then [30] else 0 end + case when [31]>0 then [31] else 0 end ) as total from " & vbCrLf
            ' MSQL = MSQL & "(select 1 sno,l.[Lineno],'' empid,'' empno,''empname,l.ptype jobgrade, sum(l.[1]) [1],sum(l.[2]) [2] ,sum(l.[3]) [3],sum(l.[4]) [4],sum(l.[5]) [5],sum(l.[6]) [6],sum(l.[7]) [7], " & vbCrLf _
            '& " sum(l.[8]) [8],sum(l.[9]) [9],sum(l.[10]) [10],sum(l.[11]) [11],sum(l.[12]) [12],sum(l.[13]) [13],sum(l.[14]) [14],  " & vbCrLf _
            '& "sum(l.[15]) [15],sum(l.[16]) [16],sum(l.[17]) [17],sum(l.[18]) [18],sum(l.[19]) [19],sum(l.[20]) [20],sum(l.[21]) [21],  " & vbCrLf _
            '& "sum(l.[22]) [22],sum(l.[23]) [23],sum(l.[24]) [24],sum(l.[25]) [25],sum(l.[26]) [26],sum(l.[27]) [27],  " & vbCrLf _
            '& "sum(l.[28]) [28],sum(l.[29]) [29],sum(l.[30]) [30],sum(l.[31]) [31],0 totinc  from  " & vbCrLf _
            '& "(select [Lineno],ptype,mergno, isnull([1],0) [1],isnull([2],0) [2] ,ISNULL([3],0) [3],isnull([4],0) [4],isnull([5],0) [5],isnull([6],0) [6],isnull([7],0) [7],  " & vbCrLf _
            '& "isnull([8],0) [8],isnull([9],0) [9],isnull([10],0) [10],isnull([11],0) [11],isnull([12],0) [12],isnull([13],0) [13],isnull([14],0) [14],  " & vbCrLf _
            '& "isnull([15],0) [15],isnull([16],0) [16],isnull([17],0) [17],isnull([18],0) [18],isnull([19],0) [19],isnull([20],0) [20],isnull([21],0) [21],  " & vbCrLf _
            '& "isnull([22],0) [22],isnull([23],0) [23],isnull([24],0) [24],isnull([25],0) [25],isnull([26],0) [26],isnull([27],0) [27],  " & vbCrLf _
            '& " isnull([28],0) [28],isnull([29],0) [29],isnull([30],0) [30],isnull([31],0) [31]  from  " & vbCrLf _
            '& "(select k.dat,k.[lineno],k.mergno,k.brand,'ProdQty' ptype, isnull(sum(k.totprodqty),0) totprodqty from   " & vbCrLf _
            '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,(b.totprodqty-isnull(b.totrejqty,0)) totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,m.mergno  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
            '& "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            '& " left join prodcost.dbo.mergeline m on m.linenum=b.[lineno] " & vbCrLf _
            '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            '& " group by k.dat,k.[lineno],k.brand,k.mergno  " & vbCrLf _
            '& " union all  " & vbCrLf _
            '& "select k.dat,k.[lineno],k.mergno,k.brand,'TargetQty' ptype, isnull(sum(k.target),0) target from   " & vbCrLf _
            '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,m.mergno  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
            '& "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            '& "  left join prodcost.dbo.mergeline m on m.linenum=b.[lineno] " & vbCrLf _
            '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            '& "group by k.dat,k.[lineno],k.brand,k.mergno  " & vbCrLf _
            '& " union all  " & vbCrLf _
            '& " select k.dat,k.[lineno],k.mergno,k.brand,'ZExcessQty' ptype, isnull(sum(k.exqty),0) exqty from   " & vbCrLf _
            '& " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,m.mergno  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
            '& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            '& "  left join prodcost.dbo.mergeline m on m.linenum=b.[lineno] " & vbCrLf _
            '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            '& " group by k.dat,k.[lineno],k.brand,k.mergno  " & vbCrLf _
            '& " union all  " & vbCrLf _
            '& " select k.dat,k.[lineno],k.mergno,k.brand,'Machine' ptype, isnull(sum(k.nomac),0) exqty from   " & vbCrLf _
            '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,((b.totprodqty-isnull(b.totrejqty,0))-(b.nomac*c.nopcs)) exqty ,m.mergno from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
            '& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            '& "  left join prodcost.dbo.mergeline m on m.linenum=b.[lineno] " & vbCrLf _
            '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            '& " group by k.dat,k.[lineno],k.brand,k.mergno  " & vbCrLf _
            '& " union all  " & vbCrLf _
            '& " select k.dat,k.[lineno],k.mergno,k.brand,'Nopcs' ptype, isnull(sum(k.nopcs),0) exqty from   " & vbCrLf _
            '& "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,c.nopcs, b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,m.mergno  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
            '& " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
            '& "  left join prodcost.dbo.mergeline m on m.linenum=b.[lineno] " & vbCrLf _
            '& " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
            '& " group by k.dat,k.[lineno],k.brand,k.mergno ) s  " & vbCrLf _
            '& " pivot (sum(totprodqty) FOR [dat] IN  " & vbCrLf _
            '& " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) Pv1) l  " & vbCrLf _
            '& " group by l.[Lineno],l.ptype  " & vbCrLf
            ' '*****
            ' MSQL = MSQL & " union all select 2 sno, [lineno],empid,empno,empname,jobgrade, " & vbCrLf _
            '    & " isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6]," & vbCrLf _
            '    & " isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12]," & vbCrLf _
            '    & " isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18]," & vbCrLf _
            '    & "isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24]," & vbCrLf _
            '    & " isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30]," & vbCrLf _
            '    & " isnull([31],0) as [31]," & vbCrLf _
            '    & " (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+ " & vbCrLf _
            '    & "isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+ " & vbCrLf _
            '    & "isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+ " & vbCrLf _
            '    & " isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+ " & vbCrLf _
            '    & " isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from " & vbCrLf
            ' MSQL = MSQL & "(select s.[Lineno],s.empid, s.empno,s.empname,s.jobgrade,SUM(s.amt) amt, s.dat from " & vbCrLf _
            '                & "(select b.date,DATEPART(d,b.date) dat, b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr," & vbCrLf _
            '                & "isnull((b.totprodqty-(b.nomac*d.nopcs)),0) as incqty, round(isnull(c.incentiveamt,0),2) as incentive,  " & vbCrLf _
            '                & "isnull(case when ((b.totprodqty-(b.nomac*d.nopcs))/isnull(m.mergno,0)) > 0 then round((((b.totprodqty-(b.nomac*d.nopcs))/isnull(m.mergno,0))* c.incentiveamt),2) else 0 end,0) amt,isnull(em.salary,0) salary," & vbCrLf _
            '                & "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
            '                & "(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
            '                & "SUM(case when ((b.totprodqty-(b.nomac*d.nopcs))/isnull(m.mergno,0)) > 0 then round((((b.totprodqty-(b.nomac*d.nopcs))/isnull(m.mergno,0))*c.incentiveamt),0) else 0 end) over (partition by b.date,b.[lineno]) as totincentive, " & vbCrLf _
            '                & "sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary " & vbCrLf _
            '                & "from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)" & vbCrLf

            ' MSQL = MSQL & "left join (select j.brand,j.bno,j.empid,j.empno,j.[lineno],SUM(j.incentiveamt) incentiveamt from (" & vbCrLf _
            '             & " select c.brand, b.bno, b.empid,b.empno,b.jobgrade,b.[lineno],b.opername,b.totprod,b.totmin, (b.totmin/convert(real,480))*100 perc, " & vbCrLf _
            '             & " (b.totmin/convert(real,480))*100 pper,  case when isnull(b.totmin,0)>0 then round(i.incentive* (b.totmin/convert(real,480)),2) else 0 end incentiveamt, " & vbCrLf _
            '             & " i.incentive, b.incyn    from prodcost.dbo.perf1 b " & vbCrLf _
            '             & " left join prodcost.dbo.operf c on c.bno=b.bno and c.date=b.date " & vbCrLf _
            '             & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade)j " & vbCrLf _
            '             & " group by j.brand,j.bno,j.empid,j.empno,j.[lineno])   c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf


            ' MSQL = MSQL & "left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) d on d.brand=b.brand  " & vbCrLf _
            '    & "left join " & Trim(mcostdbnam) & ".dbo.empmaster em with (nolock) on em.nemp_id=c.empid  " & vbCrLf _
            '    & "left join " & Trim(mcostdbnam) & ".dbo.mergeline m on m.linenum=b.[lineno] " & vbCrLf _
            '    & "Left Join" & vbCrLf _
            '    & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b with (nolock) " & vbCrLf _
            '    & " Left Join " & vbCrLf _
            '    & "(select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
            '    & "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade " & vbCrLf _
            '    & "where    date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'   and (b.totprodqty-(b.nomac*d.nopcs)) > 0  ) s " & vbCrLf _
            '  & "group by s.[Lineno],s.empid,s.empno,s.empname,s.jobgrade,s.dat ) k " & vbCrLf

            ' MSQL = MSQL & "  PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
            '   & "([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P  ) lk) kj " & vbCrLf _
            '   & " left join prodcost.dbo.empmaster jj on jj.nempno=kj.empno and jj.nemp_id=kj.empid " & vbCrLf _
            '   & " left join prodcost.dbo.incentive_ceiling ll on ll.jobgrade=jj.jobgrade " & vbCrLf _
            '   & " where jj.jobgrade not in ('XII') group by kj.empid,kj.empno,kj.empname,jj.jobgrade,ll.incenceiling,ll.active,kj.[lineno]) jj" & vbCrLf

            ' MSQL = MSQL & " left join (select j.[lineno],AVG(j.cnt) empcnt from( select k.date,k.[lineno],COUNT(k.empid) cnt from  " & vbCrLf _
            ' & " (select DATE,[lineno],empid,SUM(totmin) totmin from prodcost.dbo.perf1 with (nolock) where DATE>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and totprod>0 and  jobgrade in ('I','II','III','IV','V','VI') " & vbCrLf _
            ' & "  group by date,[lineno],empid ) k group by k.date,k.[lineno])j " & vbCrLf _
            ' & " group by j.[lineno] ) ll on ll.[lineno]=jj.[Lineno] " & vbCrLf _
            ' & " order by jj.[lineno],jj.jobgrade "



            ' ' New 


            MSQL = " SELECT JJ.[LINENO], jj.jobgrade,jj.empid, jj.empno,jj.empname,jj.total ,  sum(jj.total) over(partition by jj.empid) Acttotal, COUNT(jj.empno) over (partition by jj.[lineno]) cmpcnt2,ll.empcnt, jj.incentive, " & vbCrLf _
                  & " CASE when jj.incentive>=50 then jj.incentive else 0 end nincentive,  case when sum(jj.incentive) over(partition by jj.empid)>=50 then   " & vbCrLf _
                  & " case when sum(jj.incentive) over(partition by jj.empid)> jj.incenceiling  then jj.incenceiling else  sum(jj.incentive) over(partition by jj.empid) end else 0 end totempinc," & vbCrLf _
                  & " SUM(CASE when jj.incentive>=50 then jj.incentive else 0 end) over(partition by jj.[lineno]) totincentive," & vbCrLf _
                  & " isnull(round((SUM(CASE when jj.incentive>=50 then jj.incentive else 0 end) over(partition by jj.[lineno])/ll.empcnt)*1.25,2),0)  inchargeamt from ( " & vbCrLf _
                  & "select kj.[lineno], isnull(jj.jobgrade,kj.jobgrade) jobgrade,kj.empid, kj.empno,kj.empname,sum(kj.total) total, case when sum(kj.total)>isnull(ll.incenceiling,0) and ll.active='Y' then isnull(ll.incenceiling,0) else sum(kj.total) end Incentive ,isnull(ll.incenceiling,0) incenceiling from ( " & vbCrLf


            MSQL = MSQL & " select lk.[lineno],lk.empid, lk.empno,lk.empname,lk.jobgrade, " & vbCrLf _
                        & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
                        & " (case when [1]>0 then [1] else 0 end + case when [2]>0 then [2] else 0 end + case when [3]>0 then [3] else 0 end +  " & vbCrLf _
                        & " case when [4]>0 then [4] else 0 end +  case when [5]>0 then [5] else 0 end + case when [6]>0 then [6] else 0 end + " & vbCrLf _
                        & " case when [7]>0 then [7] else 0 end +case when [8]>0 then [8] else 0 end +  " & vbCrLf _
                        & " case when [9]>0 then [9] else 0 end + case when [10]>0 then [10] else 0 end +case when [11]>0 then [11] else 0 end +case when [12]>0 then [12] else 0 end +  " & vbCrLf _
                        & " case when [13]>0 then [13] else 0 end +case when [14]>0 then [14] else 0 end +case when [15]>0 then [15] else 0 end +case when [16]>0 then [16] else 0 end + " & vbCrLf _
                        & " case when [17]>0 then [17] else 0 end + case when [18]>0 then [18] else 0 end + case when [19]>0 then [19] else 0 end + case when [20]>0 then [20] else 0 end + " & vbCrLf _
                        & " case when [21]>0 then [21] else 0 end + case when [22]>0 then [22] else 0 end + case when [23]>0 then [23] else 0 end + case when [24]>0 then [24] else 0 end + case when [25]>0 then [25] else 0 end +  " & vbCrLf _
                        & " case when [26]>0 then [26] else 0 end + case when [27]>0 then [27] else 0 end + case when [28]>0 then [28] else 0 end + case when [29]>0 then [29] else 0 end +  " & vbCrLf _
                        & " case when [30]>0 then [30] else 0 end + case when [31]>0 then [31] else 0 end ) as total from  " & vbCrLf _
                        & " (select 1 sno,l.[Lineno],'' empid, convert(int,l.mergno)  empno,''empname,l.ptype jobgrade, sum(l.[1]) [1],sum(l.[2]) [2] ,sum(l.[3]) [3],sum(l.[4]) [4],sum(l.[5]) [5],sum(l.[6]) [6],sum(l.[7]) [7],  " & vbCrLf _
                        & " sum(l.[8]) [8],sum(l.[9]) [9],sum(l.[10]) [10],sum(l.[11]) [11],sum(l.[12]) [12],sum(l.[13]) [13],sum(l.[14]) [14],  " & vbCrLf _
                        & " sum(l.[15]) [15],sum(l.[16]) [16],sum(l.[17]) [17],sum(l.[18]) [18],sum(l.[19]) [19],sum(l.[20]) [20],sum(l.[21]) [21], " & vbCrLf _
                        & " sum(l.[22]) [22],sum(l.[23]) [23],sum(l.[24]) [24],sum(l.[25]) [25],sum(l.[26]) [26],sum(l.[27]) [27]," & vbCrLf _
                        & " sum(l.[28]) [28],sum(l.[29]) [29],sum(l.[30]) [30],sum(l.[31]) [31],0 totinc  from " & vbCrLf _
                        & " (select [Lineno],ptype,mergno, isnull([1],0) [1],isnull([2],0) [2] ,ISNULL([3],0) [3],isnull([4],0) [4],isnull([5],0) [5],isnull([6],0) [6],isnull([7],0) [7],  " & vbCrLf _
                        & " isnull([8],0) [8],isnull([9],0) [9],isnull([10],0) [10],isnull([11],0) [11],isnull([12],0) [12],isnull([13],0) [13],isnull([14],0) [14], " & vbCrLf _
                        & " isnull([15],0) [15],isnull([16],0) [16],isnull([17],0) [17],isnull([18],0) [18],isnull([19],0) [19],isnull([20],0) [20],isnull([21],0) [21], " & vbCrLf _
                        & " isnull([22],0) [22],isnull([23],0) [23],isnull([24],0) [24],isnull([25],0) [25],isnull([26],0) [26],isnull([27],0) [27], " & vbCrLf _
                        & " isnull([28],0) [28],isnull([29],0) [29],isnull([30],0) [30],isnull([31],0) [31]  from  " & vbCrLf _
                        & " (select k.dat,k.[lineno],k.mergno,k.brand,'ProdQty' ptype, isnull(sum(k.totprodqty),0) totprodqty from  " & vbCrLf _
                        & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno  from prodcost.dbo.operf b with (nolock) " & vbCrLf _
                        & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster with (nolock) group by brand,nopcs) c on c.brand=b.brand " & vbCrLf _
                        & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k   " & vbCrLf _
                        & " group by k.dat,k.[lineno],k.brand ,k.mergno " & vbCrLf _
                        & " union all " & vbCrLf _
                        & " select k.dat,k.[lineno],k.mergno,k.brand,'TargetQty' ptype, isnull(sum(k.target),0) target from   " & vbCrLf _
                        & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno  from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
                        & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                        & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k   " & vbCrLf _
                        & " group by k.dat,k.[lineno],k.brand,k.mergno   " & vbCrLf _
                        & " union all " & vbCrLf _
                        & " select k.dat,k.[lineno],k.mergno,k.brand,'ZExcessQty' ptype, isnull(sum(k.exqty),0) exqty from  " & vbCrLf _
                        & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno   from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
                        & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                        & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
                        & " group by k.dat,k.[lineno],k.brand ,k.mergno  " & vbCrLf _
                        & " union all " & vbCrLf _
                        & " select k.dat,k.[lineno],k.mergno, k.brand,'Machine' ptype, isnull(sum(k.nomac),0) exqty from  " & vbCrLf _
                        & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno  from prodcost.dbo.operf b with (nolock) " & vbCrLf _
                        & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                        & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
                        & " group by k.dat,k.[lineno],k.brand,k.mergno  " & vbCrLf _
                        & " union all " & vbCrLf _
                        & " select k.dat,k.[lineno],k.mergno, k.brand,'Nopcs' ptype, isnull(sum(k.nopcs),0) exqty from  " & vbCrLf _
                        & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,c.nopcs, b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno   from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
                        & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                        & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
                        & " group by k.dat,k.[lineno],k.brand,k.mergno ) s  " & vbCrLf _
                        & " pivot (sum(totprodqty) FOR [dat] IN  " & vbCrLf _
                        & " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) Pv1) l  " & vbCrLf _
                        & " group by l.[Lineno],l.ptype,l.mergno " & vbCrLf _
                        & " union all " & vbCrLf _
                        & " select 2 sno, [lineno],empid,empno,empname,jobgrade, " & vbCrLf _
                        & " isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6], " & vbCrLf _
                        & " isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12], " & vbCrLf _
                        & " isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18], " & vbCrLf _
                        & " isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24], " & vbCrLf _
                        & " isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30], " & vbCrLf _
                        & " isnull([31],0) as [31]," & vbCrLf _
                        & " (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+  " & vbCrLf _
                        & " isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+  " & vbCrLf _
                        & " isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+ " & vbCrLf _
                        & " isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+ " & vbCrLf _
                        & " isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from (" & vbCrLf
            'new old
            'MSQL = MSQL & " (select s.[Lineno],s.empid,s.empno,s.empname,s.jobgrade,SUM(s.amt) amt, s.dat from  " & vbCrLf _
            '            & " (select b.date,DATEPART(d,b.date) dat, b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr, " & vbCrLf _
            '            & " isnull((b.totprodqty-(b.nomac*d.nopcs)),0) as incqty, round(isnull(c.incentiveamt,0),2) as incentive," & vbCrLf _
            '            & " isnull(case when ((b.totprodqty-(b.nomac*d.nopcs))/isnull(b.mergno,1)) > 0 then round((((b.totprodqty-(b.nomac*d.nopcs))/isnull(b.mergno,1))* c.incentiveamt),2) else 0 end,0) amt, " & vbCrLf _
            '            & " isnull(em.salary,0) salary," & vbCrLf _
            '            & " isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
            '            & " (datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
            '            & " SUM(case when ((b.totprodqty-(b.nomac*d.nopcs))/ isnull(b.mergno,1)) > 0 then round((((b.totprodqty-(b.nomac*d.nopcs))/isnull(b.mergno,1))*c.incentiveamt),0) else 0 end) over (partition by b.date,b.[lineno]) as totincentive, " & vbCrLf _
            '            & " sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary " & vbCrLf _
            '            & " from prodcost.dbo.operf b with (nolock) " & vbCrLf _
            '            & " left join (select j.brand,j.bno,j.empid,j.empno,j.[lineno],SUM(j.incentiveamt) incentiveamt from ( " & vbCrLf _
            '            & " select c.brand, b.bno, b.empid,b.empno,b.jobgrade,b.[lineno],b.opername,b.totprod,b.totmin, (b.totmin/convert(real,480))*100 perc,  " & vbCrLf _
            '            & " (b.totmin/convert(real,480))*100 pper,  case when isnull(b.totmin,0)>0 then round(i.incentive* (b.totmin/convert(real,480)),2) else 0 end incentiveamt, " & vbCrLf _
            '            & " i.incentive, b.incyn    from prodcost.dbo.perf1 b  " & vbCrLf _
            '            & " left join prodcost.dbo.operf c on c.bno=b.bno and c.date=b.date  " & vbCrLf _
            '            & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade)j  " & vbCrLf _
            '            & " group by j.brand,j.bno,j.empid,j.empno,j.[lineno])   c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf _
            '            & " left join (select brand,nopcs from prodcost.dbo.incentivemaster with (nolock) group by brand,nopcs) d on d.brand=b.brand  " & vbCrLf _
            '            & " left join prodcost.dbo.empmaster em with (nolock) on em.nemp_id=c.empid  " & vbCrLf _
            '            & " left join prodcost.dbo.mergeline m on m.linenum=b.[lineno]  " & vbCrLf _
            '            & " Left Join " & vbCrLf _
            '            & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from prodcost.dbo.incentivemaster b with (nolock) " & vbCrLf _
            '            & " Left Join " & vbCrLf _
            '            & " (select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
            '            & " (select brand, nprodpcsfr,nprodpcsto from  prodcost.dbo.incentivemaster with (nolock) group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade  " & vbCrLf _
            '            & " where    date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'   and (b.totprodqty-(b.nomac*d.nopcs)) > 0  ) s " & vbCrLf _
            '            & " group by s.[Lineno],s.empid,s.empno,s.empname,s.jobgrade,s.dat " & vbCrLf

            MSQL = MSQL & "select s.[linno] [Lineno],s.empid,s.empno,s.empname,s.jobgrade,sum(isnull(s.Actincentive,0)) amt,datepart(d,s.date) dat from (   " & vbCrLf _
                             & " select k.mergno, k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs,k.sampcs2, k.totprod,k.jobgrade,k.rate,k.totcnt,k.MANPOWER, k.avgpcs,k.operexspcs, k.totincentamt, " & vbCrLf _
                             & " case when isnull(k.operexspcs,0)>0 then sum(k.operexspcs) over (partition by k.opername,k.date,k.linno) else 0 end totoppcs," & vbCrLf _
                             & " round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per, " & vbCrLf _
                             & "round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive from (  " & vbCrLf _
                             & "select c.date,c.bno, c.[lineno] linno,isnull(c.mergno,1) mergno, c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,(isnull(j.pcs,0)*8) * j.MANPOWER sampcs2," & vbCrLf _
                             & "case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end sampcs, b.totprod,b.jobgrade,i.incentive rate,  " & vbCrLf _
                             & " count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt, j.MANPOWER,((isnull(j.pcs,0)*8)*j.manpower)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs,  " & vbCrLf _
                             & " b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs2, " & vbCrLf _
                             & " case when (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end) else 0 end operexspcs," & vbCrLf _
                             & " case when (b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ case when totmin=480 then j.manpower else count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) end )>0 then b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ " & vbCrLf _
                             & "case when totmin=480 then j.manpower else count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) end else 0 end operexspcs3, " & vbCrLf _
                             & " case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then " & vbCrLf _
                             & " case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end " & vbCrLf _
                             & " else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))   *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end " & vbCrLf _
                             & " else 0 end totincentamtold, " & vbCrLf
            MSQL = MSQL & " case  when (count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))<j.manpower then " & vbCrLf _
                       & "	case when sum(b.totprod) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno])-(isnull(j.pcs,0)*8)>0 then " & vbCrLf _
                       & "		case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then  " & vbCrLf _
                       & "		case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end  " & vbCrLf _
                       & "		else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))  *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end  " & vbCrLf _
                       & "		else 0 end " & vbCrLf _
                       & "	else " & vbCrLf _
                       & "		case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then " & vbCrLf _
                       & "		case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end " & vbCrLf _
                       & "		else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))  *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end " & vbCrLf _
                       & "		else 0 end/(count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno])) " & vbCrLf _
                       & "   End " & vbCrLf _
                       & " else " & vbCrLf _
                       & "	case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then  " & vbCrLf _
                       & "	case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end  " & vbCrLf _
                       & "	else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))  *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end  " & vbCrLf _
                       & " else 0 end end  totincentamt " & vbCrLf

            MSQL = MSQL & " from prodcost.dbo.perf1 b  " & vbCrLf _
                             & " inner join prodcost.dbo.operf c on c.bno=b.bno " & vbCrLf _
                             & " left join prodcost.dbo.PROCESSJOBMASTER j on j.OPERNAME=b.opername and rtrim(ltrim(replace(j.style,'FULL','')))=rtrim(ltrim(c.style))  " & vbCrLf _
                             & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade " & vbCrLf _
                             & " where c.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and c.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and  b.jobgrade not in ('XII') ) k ) s   " & vbCrLf _
                             & " group by  s.[linno],s.empid, s.empno,s.empname,s.jobgrade,s.date " & vbCrLf






            MSQL = MSQL & ") k " & vbCrLf _
                        & " PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
                        & " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P  ) lk ) kj " & vbCrLf

            MSQL = MSQL & " left join prodcost.dbo.empmaster jj on jj.nempno=kj.empno and jj.nemp_id=kj.empid " & vbCrLf _
              & " left join prodcost.dbo.incentive_ceiling ll on ll.jobgrade=jj.jobgrade " & vbCrLf _
              & " where kj.jobgrade not in ('XII','Machine','Nopcs','ProdQty','TargetQty','ZExcessQty','') group by kj.empid,kj.empno,kj.empname,jj.jobgrade,kj.jobgrade,ll.incenceiling,ll.active,kj.[lineno]) jj" & vbCrLf

            MSQL = MSQL & " left join (select j.[lineno],AVG(j.cnt) empcnt from( select k.date,k.[lineno],COUNT(k.empid) cnt from  " & vbCrLf _
            & " (select DATE,[lineno],empid,SUM(totmin) totmin from prodcost.dbo.perf1 with (nolock) where DATE>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and totprod>0 and  jobgrade in ('I','II','III','IV','V','VI') " & vbCrLf _
            & "  group by date,[lineno],empid ) k group by k.date,k.[lineno])j " & vbCrLf _
            & " group by j.[lineno] ) ll on ll.[lineno]=jj.[Lineno] " & vbCrLf _
            & " order by jj.[lineno],jj.jobgrade "




            Cursor = Cursors.WaitCursor
            dg.DataSource = Nothing
            dg.Rows.Clear()

            Dim cmda As New SqlCommand
            Dim dat2 As New SqlDataAdapter

            cmda.CommandText = MSQL
            cmda.Connection = con
            cmda.CommandTimeout = 600
            dat2.SelectCommand = cmda
            Dim dt2 As New DataTable
            dat2.Fill(dt2)
            dg.DataSource = dt2
            dg.Columns(0).Width = 30
            dg.Columns(1).Width = 50
            dg.Columns(2).Width = 120
            dg.Columns(3).Width = 80
            dg.Columns(4).Width = 80
            'dg.Columns(3).Visible = False

            mtotincent = False
            mdate = True
            dt2.Dispose()
            cmda.Dispose()
            Cursor = Cursors.Default
        End If

    End Sub

    Private Sub butrej_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butrej.Click
        MSQL = "select b.bno,b.empid,b.empno,b.[lineno], b.opername,b.jobgrade, b.sam,b.totprod,b.totmin,b.rejqty,c.totrejqty,SUM(b.rejqty) over(partition by b.bno) actrejqty,c.brand,isnull(d.incentive,0) incentive,round((b.rejqty*isnull(d.incentive,0)) * 2,0) fineamt from " & Trim(mcostdbnam) & ".dbo.perf1 b" & vbCrLf _
               & "left join " & Trim(mcostdbnam) & ".dbo.operf c on c.bno=b.bno " & vbCrLf _
               & "left join " & Trim(mcostdbnam) & ".dbo.incentivemaster d on d.brand=b.brand and d.grade=b.jobgrade " & vbCrLf _
               & "where c.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and c.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and b.rejqty>3 " & vbCrLf

        qry1 = "select k.empno,em.empname, SUM(k.rejqty) rejqty,SUM(k.fineamt) fineamt from ( " & vbCrLf _
              & "select b.bno,b.empid,b.empno,b.[lineno], b.opername,b.jobgrade, b.sam,b.totprod,b.totmin,b.rejqty,c.totrejqty,SUM(b.rejqty) over(partition by b.bno) actrejqty,c.brand,isnull(d.incentive,0) incentive,round((b.rejqty*isnull(d.incentive,0)) * 2,0) fineamt from " & Trim(mcostdbnam) & ".dbo.perf1 b" & vbCrLf _
                      & "left join " & Trim(mcostdbnam) & ".dbo.operf c on c.bno=b.bno " & vbCrLf _
                      & "left join " & Trim(mcostdbnam) & ".dbo.incentivemaster d on d.brand=b.brand and d.grade=b.jobgrade " & vbCrLf _
                      & "where c.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and c.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and b.rejqty>3 ) k " & vbCrLf _
                      & "left join " & Trim(mcostdbnam) & ".dbo.empmaster em on em.nempno=k.empno " & vbCrLf _
                      & " group by k.empno,em.empname,k.[lineno] order by k.empno "

        dqry = "REJECTION INCENTIVE REPORT " & Format(CDate(mskdatefr.Text), "dd-MM-yyyy") & " To " & Format(CDate(Mskdateto.Text), "dd-MM-yyyy")

        exportexcelDataqry2(MSQL, qry1, dqry)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Call calccost()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        ' Frmchart.ShowDialog()
    End Sub

    Private Sub calcinctotnew()
        Dim s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16, s17, s18, s19, s20, s21, s22, s23, s24, s25, s26, s27, s28, s29, s30, s31, mtot As Double
        'MSQL = "select lk.[lineno],lk.empno,lk.empname,lk.jobgrade," & vbCrLf _
        '  & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
        '  & "( [1]+[2]+[3]+[4]+[5]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22]+[23]+[24]+[25]+[26]+[27]+[28]+[29]+[30]+[31]) total from " & vbCrLf

        MSQL = "select lk.[lineno],lk.empno,lk.empname,lk.jobgrade," & vbCrLf _
                 & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
                & "(case when [1]>0 then [1] else 0 end + " & vbCrLf _
                & "case when [2]>0 then [2] else 0 end +  " & vbCrLf _
                & " case when [3]>0 then [3] else 0 end +  " & vbCrLf _
                & " case when [4]>0 then [4] else 0 end +  " & vbCrLf _
                & " case when [5]>0 then [5] else 0 end +  " & vbCrLf _
                & " case when [6]>0 then [6] else 0 end +case when [7]>0 then [7] else 0 end +case when [8]>0 then [8] else 0 end +  " & vbCrLf _
                & " case when [9]>0 then [9] else 0 end + case when [10]>0 then [10] else 0 end +case when [11]>0 then [11] else 0 end +case when [12]>0 then [12] else 0 end +  " & vbCrLf _
                & " case when [13]>0 then [13] else 0 end +case when [14]>0 then [14] else 0 end +case when [15]>0 then [15] else 0 end +case when [16]>0 then [16] else 0 end +  " & vbCrLf _
                & " case when [17]>0 then [17] else 0 end + case when [18]>0 then [18] else 0 end + case when [19]>0 then [19] else 0 end + case when [20]>0 then [20] else 0 end +  " & vbCrLf _
                & " case when [21]>0 then [21] else 0 end + case when [22]>0 then [22] else 0 end + case when [23]>0 then [23] else 0 end + case when [24]>0 then [24] else 0 end + case when [25]>0 then [25] else 0 end +  " & vbCrLf _
                & " case when [26]>0 then [26] else 0 end + case when [27]>0 then [27] else 0 end + case when [28]>0 then [28] else 0 end + case when [29]>0 then [29] else 0 end +  " & vbCrLf _
                & " case when [30]>0 then [30] else 0 end + case when [31]>0 then [31] else 0 end ) as total from " & vbCrLf




        MSQL = MSQL & "(select 1 sno,l.[Lineno],'' empno,''empname,l.ptype jobgrade, sum(l.[1]) [1],sum(l.[2]) [2] ,sum(l.[3]) [3],sum(l.[4]) [4],sum(l.[5]) [5],sum(l.[6]) [6],sum(l.[7]) [7], " & vbCrLf _
       & " sum(l.[8]) [8],sum(l.[9]) [9],sum(l.[10]) [10],sum(l.[11]) [11],sum(l.[12]) [12],sum(l.[13]) [13],sum(l.[14]) [14],  " & vbCrLf _
       & "sum(l.[15]) [15],sum(l.[16]) [16],sum(l.[17]) [17],sum(l.[18]) [18],sum(l.[19]) [19],sum(l.[20]) [20],sum(l.[21]) [21],  " & vbCrLf _
       & "sum(l.[22]) [22],sum(l.[23]) [23],sum(l.[24]) [24],sum(l.[25]) [25],sum(l.[26]) [26],sum(l.[27]) [27],  " & vbCrLf _
       & "sum(l.[28]) [28],sum(l.[29]) [29],sum(l.[30]) [30],sum(l.[31]) [31],0 totinc  from  " & vbCrLf _
       & "(select [Lineno],ptype, isnull([1],0) [1],isnull([2],0) [2] ,ISNULL([3],0) [3],isnull([4],0) [4],isnull([5],0) [5],isnull([6],0) [6],isnull([7],0) [7],  " & vbCrLf _
       & "isnull([8],0) [8],isnull([9],0) [9],isnull([10],0) [10],isnull([11],0) [11],isnull([12],0) [12],isnull([13],0) [13],isnull([14],0) [14],  " & vbCrLf _
       & "isnull([15],0) [15],isnull([16],0) [16],isnull([17],0) [17],isnull([18],0) [18],isnull([19],0) [19],isnull([20],0) [20],isnull([21],0) [21],  " & vbCrLf _
       & "isnull([22],0) [22],isnull([23],0) [23],isnull([24],0) [24],isnull([25],0) [25],isnull([26],0) [26],isnull([27],0) [27],  " & vbCrLf _
       & " isnull([28],0) [28],isnull([29],0) [29],isnull([30],0) [30],isnull([31],0) [31]  from  " & vbCrLf _
       & "(select k.dat,k.[lineno],k.brand,'ProdQty' ptype, isnull(sum(k.totprodqty),0) totprodqty from   " & vbCrLf _
       & "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
       & "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
       & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
       & " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
       & " union all  " & vbCrLf _
       & "select k.dat,k.[lineno],k.brand,'TargetQty' ptype, isnull(sum(k.target),0) target from   " & vbCrLf _
       & "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
       & "inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
       & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
       & "group by k.dat,k.[lineno],k.brand  " & vbCrLf _
       & " union all  " & vbCrLf _
       & " select k.dat,k.[lineno],k.brand,'ZExcessQty' ptype, isnull(sum(k.exqty),0) exqty from   " & vbCrLf _
       & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
       & " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
       & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
       & " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
       & " union all  " & vbCrLf _
       & " select k.dat,k.[lineno],k.brand,'Machine' ptype, isnull(sum(k.nomac),0) exqty from   " & vbCrLf _
       & "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock) " & vbCrLf _
       & " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
       & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
       & " group by k.dat,k.[lineno],k.brand  " & vbCrLf _
       & " union all  " & vbCrLf _
       & " select k.dat,k.[lineno],k.brand,'Nopcs' ptype, isnull(sum(k.nopcs),0) exqty from   " & vbCrLf _
       & "(select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty,b.nomac,c.nopcs, b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty  from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)  " & vbCrLf _
       & " inner join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
       & " where b.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' ) k  " & vbCrLf _
       & " group by k.dat,k.[lineno],k.brand ) s  " & vbCrLf _
       & " pivot (sum(totprodqty) FOR [dat] IN  " & vbCrLf _
       & " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) Pv1) l  " & vbCrLf _
       & " group by l.[Lineno],l.ptype  " & vbCrLf


        '*****
        MSQL = MSQL & " union all select 2 sno, [lineno],empno,empname,jobgrade, " & vbCrLf _
           & " isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6]," & vbCrLf _
           & " isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12]," & vbCrLf _
           & " isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18]," & vbCrLf _
           & "isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24]," & vbCrLf _
           & " isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30]," & vbCrLf _
           & " isnull([31],0) as [31]," & vbCrLf _
           & " (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+ " & vbCrLf _
           & "isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+ " & vbCrLf _
           & "isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+ " & vbCrLf _
           & " isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+ " & vbCrLf _
           & " isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from " & vbCrLf

        MSQL = MSQL & "(select s.[Lineno],s.empno,s.empname,s.jobgrade,SUM(s.amt) amt, s.dat from " & vbCrLf _
                & "(select b.date,DATEPART(d,b.date) dat, b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr," & vbCrLf _
                & "isnull((b.totprodqty-(b.nomac*d.nopcs)),0) as incqty, round((isnull(f.incentive,0)*c.pper)/100,2)  incentive, " & vbCrLf _
                & "isnull(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))* round((f.incentive*c.pper)/100,2)),0) else 0 end,0) amt,isnull(em.salary,0) salary," & vbCrLf _
                & "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
                & "(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
                & "SUM(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*f.incentive),0) else 0 end) over (partition by b.[lineno]) as totincentive, " & vbCrLf _
                & "sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary " & vbCrLf _
                & "from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)" & vbCrLf

        MSQL = MSQL & "left join (select j.brand,j.bno,j.empid,j.empno,j.[lineno],SUM(j.incentiveamt) incentiveamt from (" & vbCrLf _
                    & " select c.brand, b.bno, b.empid,b.empno,b.jobgrade,b.[lineno],b.opername,b.totprod,b.totmin, (b.totmin/convert(real,480))*100 perc, " & vbCrLf _
                    & " (b.totmin/convert(real,480))*100 pper,  case when isnull(b.totmin,0)>0 then round(i.incentive* (b.totmin/convert(real,480)),2) else 0 end incentiveamt, " & vbCrLf _
                    & " i.incentive, b.incyn    from prodcost.dbo.perf1 b " & vbCrLf _
                    & " left join prodcost.dbo.operf c on c.bno=b.bno and c.date=b.date " & vbCrLf _
                    & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade)j " & vbCrLf _
                    & " group by j.brand,j.bno,j.empid,j.empno,j.[lineno])   c on c.bno=b.bno and c.[lineno]=b.[lineno] " & vbCrLf


        'MSQL = MSQL & "left join (select l.bno,l.empid,l.empno,l.jobgrade,l.[lineno],l.pper from  " & vbCrLf _
        '    & "(select bno, empid,empno,jobgrade,[lineno],opername,totprod,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade) mprod ,COUNT(empno) over(partition by bno,[lineno],jobgrade,empno) cnt,  " & vbCrLf _
        '    & "case when totprod>0 and COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)<=1 then round((convert(real,totprod)/convert(real,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade)))*100,0)  " & vbCrLf _
        '    & "else case  when COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)>1 then 100 else  0 end end pper,incyn    from " & Trim(mcostdbnam) & ".dbo.perf1 with (nolock)) l  " & vbCrLf _
        '    & "where incyn not in ('N'))  c on c.bno=b.bno and c.[lineno]=b.[lineno]  " & vbCrLf

        MSQL = MSQL & " left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) d on d.brand=b.brand  " & vbCrLf _
                & "left join " & Trim(mcostdbnam) & ".dbo.empmaster em with (nolock) on em.nemp_id=c.empid  " & vbCrLf _
                & "Left Join" & vbCrLf _
                & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b with (nolock) " & vbCrLf _
                & " Left Join " & vbCrLf _
                & "(select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
                & "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade " & vbCrLf _
                & "where    date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'   and (b.totprodqty-(b.nomac*d.nopcs)) > 0  ) s " & vbCrLf _
              & "group by s.[Lineno],s.empno,s.empname,s.jobgrade,s.dat ) k " & vbCrLf

        MSQL = MSQL & "  PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
          & "([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P  ) lk " & vbCrLf _
          & " order by  lk.[lineno],lk.sno,lk.jobgrade,lk.empno"



    End Sub

    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick

    End Sub

    Private Sub dg_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dg.CellFormatting
        If e.RowIndex > 0 Then
            'dataGridView1.Rows(i).Cells(j).Value IsNot Nothing
            If dg.Rows(e.RowIndex).Cells(2).Value IsNot Nothing Then
                If InStr(Trim(dg.Rows(e.RowIndex).Cells(2).Value.ToString), "Total") > 0 Then
                    dg.Rows(e.RowIndex).DefaultCellStyle.Font = New Font("Lao UI", 9, FontStyle.Bold)
                    dg.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.LightCyan
                    dg.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Peru
                End If
            End If

        End If
    End Sub

    Private Sub newincentive()
        MSQL = "select k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.empid,k.empno,k.empname,k.opername,k.sampcs,k.totprod,k.jobgrade,k.rate,k.totcnt,k.avgpcs,k.operexspcs,k.totincentamt, " _
         & " sum(k.operexspcs) over (order by k.opername) totoppcs, " _
        & " round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (order by k.opername)))*100 else 0 end,2) per, " _
        & " round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (order by k.opername)))*100 else 0 end,2)/100,2) Actincentive " _
        & " from ( " _
        & " select c.date,c.bno, c.[lineno] linno,c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs,  b.empid,b.empno,b.empname,b.opername,isnull(j.pcs,0)*8 sampcs, b.totprod,b.jobgrade,i.incentive rate, " _
        & " count(b.opername) over (order by b.opername) totcnt,(isnull(j.pcs,0)*8)/ count(b.opername) over (order by b.opername) avgpcs, " _
        & " b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (order by b.opername) operexspcs,(c.totprodqty-(c.nomac*i.nopcs))*i.incentive totincentamt  from perf1 b " _
        & " inner join operf c on c.bno=b.bno " _
        & " left join PROCESSJOBMASTER j on j.OPERNAME=b.opername " _
        & " left join incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade   where b.bno=7049 and c.[lineno]=1) k "



        '***
        '        select k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs,k.totprod,k.jobgrade,k.rate,k.totcnt,k.avgpcs,k.operexspcs,k.totincentamt,
        'sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno) totoppcs,
        '--convert(float,isnull(k.operexspcs,0))/convert(float,sum(isnull(k.operexspcs,0)) over (order by k.opername)),
        'convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)),
        'round(case when isnull(k.operexspcs,0)<>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per, 
        '--round((convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100,2) per, 
        'round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive
        'from (
        'select c.date,c.bno, c.[lineno] linno,c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,isnull(j.pcs,0)*8 sampcs, b.totprod,b.jobgrade,i.incentive rate,
        'count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt,(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs,
        'b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs,
        'case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then (c.totprodqty-(c.nomac*i.nopcs))*i.incentive else 0 end totincentamt
        'from perf1 b
        'inner join operf c on c.bno=b.bno
        'left join PROCESSJOBMASTER j on j.OPERNAME=b.opername
        'left join incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade
        'where c.date>='2024-01-06' and c.date<='2024-01-08' and  b.jobgrade not in ('XII') ) k 
        'order by k.date,k.linno,k.opername






        '        select k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs,k.totprod,k.jobgrade,k.rate,k.totcnt,k.avgpcs,k.operexspcs,k.totincentamt,
        'sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno) totoppcs,
        '--convert(float,isnull(k.operexspcs,0))/convert(float,sum(isnull(k.operexspcs,0)) over (order by k.opername)),
        'convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)),
        'round(case when isnull(k.operexspcs,0)<>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per, 
        '--round((convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100,2) per, 
        'round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive
        'from (
        'select c.date,c.bno, c.[lineno] linno,c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,isnull(j.pcs,0)*8 sampcs, b.totprod,b.jobgrade,i.incentive rate,
        'count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt,(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs,
        'b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs,
        'case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then (c.totprodqty-(c.nomac*i.nopcs))*i.incentive else 0 end totincentamt
        'from perf1 b
        'inner join operf c on c.bno=b.bno
        'left join PROCESSJOBMASTER j on j.OPERNAME=b.opername
        'left join incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade
        'where c.date>='2024-01-06' and c.date<='2024-01-08' and  b.jobgrade not in ('XII') ) k 
        'order by k.date,k.linno,k.opername


        '****************************
        'select k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs,k.totprod,k.jobgrade,k.rate,k.totcnt,k.avgpcs,k.operexspcs,k.totincentamt,
        ' case when isnull(k.operexspcs,0)>0 then sum(k.operexspcs) over (partition by k.opername,k.date,k.linno) else 0 end totoppcs,
        '--convert(float,isnull(k.operexspcs,0))/convert(float,sum(isnull(k.operexspcs,0)) over (order by k.opername)),
        '--convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)),
        'round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per,

        ' --convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno,convert(float,k.operexspcs) order by k.opername,k.date,k.linno,convert(float,k.operexspcs))) tper,

        '--round((convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100,2) per, 
        'round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive
        'from (
        'select c.date,c.bno, c.[lineno] linno,c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,isnull(j.pcs,0)*8 sampcs, b.totprod,b.jobgrade,i.incentive rate,
        'count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt,(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs,
        'b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs2,
        'case when (b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) else 0 end operexspcs,
        'case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then (c.totprodqty-(c.nomac*i.nopcs))*i.incentive else 0 end totincentamt
        'from perf1 b
        'inner join operf c on c.bno=b.bno
        'left join PROCESSJOBMASTER j on j.OPERNAME=b.opername
        'left join incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade
        'where c.date>='2024-01-06' and c.date<='2024-01-08' and  b.jobgrade not in ('XII') ) k 
        'order by k.date,k.linno,k.opername

    End Sub

    Private Function gettotal() As Double
        Dim mttot As Double
        qry1 = "select 'Grand Total ' tot ,   sum(ll.totinc) Totinc from ( " & vbCrLf _
            & "	select 2 sno, [lineno],  " & vbCrLf _
            & "          isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6],  " & vbCrLf _
            & "          isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12],  " & vbCrLf _
            & "          isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18],  " & vbCrLf _
            & "          isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24],  " & vbCrLf _
            & "          isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30],  " & vbCrLf _
            & "          isnull([31],0) as [31], (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+   " & vbCrLf _
            & "          isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+   " & vbCrLf _
            & "          isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+  " & vbCrLf _
            & "          isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+  " & vbCrLf _
            & "          isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from ( " & vbCrLf _
            & " select s.[linno] [Lineno],s.empno,s.empname,s.jobgrade,sum(isnull(s.Actincentive,0)) amt,datepart(d,s.date) dat from (  " & vbCrLf _
            & "              select k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs,k.totprod,k.jobgrade,k.rate,k.totcnt,k.avgpcs,k.operexspcs,k.totincentamt,  " & vbCrLf _
            & "              case when isnull(k.operexspcs,0)>0 then sum(k.operexspcs) over (partition by k.opername,k.date,k.linno) else 0 end totoppcs,  " & vbCrLf _
            & "              round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per,  " & vbCrLf _
            & "              round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive from (  " & vbCrLf _
            & "              select c.date,c.bno, c.[lineno] linno,c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,isnull(j.pcs,0)*8 sampcs, b.totprod,b.jobgrade,i.incentive rate,  " & vbCrLf _
            & "              count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt,(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs,  " & vbCrLf _
            & "              b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs2,  " & vbCrLf _
            & "              case when (b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) else 0 end operexspcs,  " & vbCrLf _
            & "              case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))>0 then (c.totprodqty-(c.nomac*i.nopcs))*i.incentive else 0 end totincentamt   from prodcost.dbo.perf1 b  " & vbCrLf _
            & "              inner join prodcost.dbo.operf c on c.bno=b.bno  " & vbCrLf _
            & "              left join prodcost.dbo.PROCESSJOBMASTER j on j.OPERNAME=b.opername  " & vbCrLf _
            & "              left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade  " & vbCrLf _
            & "              where c.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and c.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and  b.jobgrade not in ('XII') ) k  ) s    " & vbCrLf _
            & "              group by  s.[linno],s.empno,s.empname,s.jobgrade,s.date  ) k  " & vbCrLf _
            & "    PIVOT (SUM(amt) FOR [dat] IN  " & vbCrLf _
            & "    ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P ) ll " & vbCrLf

        Dim dtot As DataTable = getDataTable(qry1)
        If dtot.Rows.Count > 0 Then
            For Each rw As DataRow In dtot.Rows
                mttot = rw("totinc")
            Next
        Else
            mttot = 0
        End If
        Return mttot
    End Function


    Private Sub findactsam()

        'declare @absnt as int
        'declare @eff as int

        'set @absnt=5
        'set @eff=95

        'select b.[lineno] linno,b.brand, b.nomac, c.nopcs permactarget,  (b.nomac*c.nopcs) targetpcs, (b.nomac*8*60) totmin,@absnt absentper,@eff eff, ((b.nomac*8*60)-((b.nomac*8*60)*@absnt/100))*@eff/100 finmin,
        '(((b.nomac*8*60)-((b.nomac*8*60)*@absnt/100))*@eff/100)/c.nopcs actsam,b.totprodqty from operf b
        'left join (select brand,nopcs from  incentivemaster group by brand,nopcs) c on c.brand=b.brand
        'where b.date='2024-04-22' --and [lineno]=1


        'attendance
        'select nempno,nemp_id,vname,department,designation,sum(att) nodays,(26-sum(att)) absnt  from empdailysalary 
        'where dot>='2024-04-01' and dot<='2024-04-30'
        'group by nempno,nemp_id,vname,department,designation

    End Sub
    Private Sub loadempincentiveatt()

        MSQL = " declare @d1 as nvarchar(20) " _
              & "declare @d2 as nvarchar(20) " _
              & "declare @totday as int " _
              & " set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' " _
              & " set @d2='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' " _
              & " set @totday=" & Val(txtnoday.Text) _
              & " select jg.empid,jg.empno,jg.empname,jg.acttotal,gk.attendance,gk.absnt,case when gk.absnt<=1 then jg.acttotal when gk.absnt>1 and gk.absnt<=2 then jg.acttotal*50/100 when gk.absnt>2 and gk.absnt<=3 then jg.acttotal*25/100 else 0 end NetAmt,emm.subdept from ( " _
              & " SELECT JJ.[LINENO], jj.jobgrade,jj.empid, jj.empno,jj.empname,jj.total ,  sum(jj.total) over(partition by jj.empid) Acttotal, COUNT(jj.empno) over (partition by jj.[lineno]) cmpcnt2,ll.empcnt, jj.incentive, " & vbCrLf _
              & " CASE when jj.incentive>=50 then jj.incentive else 0 end nincentive,  case when sum(jj.incentive) over(partition by jj.empid)>=50 then   " & vbCrLf _
              & " case when sum(jj.incentive) over(partition by jj.empid)> jj.incenceiling  then jj.incenceiling else  sum(jj.incentive) over(partition by jj.empid) end else 0 end totempinc," & vbCrLf _
              & " SUM(CASE when jj.incentive>=50 then jj.incentive else 0 end) over(partition by jj.[lineno]) totincentive," & vbCrLf _
              & " round((SUM(CASE when jj.incentive>=50 then jj.incentive else 0 end) over(partition by jj.[lineno])/ll.empcnt)*1.25,2)  inchargeamt from ( " & vbCrLf _
              & "select kj.[lineno], isnull(jj.jobgrade,kj.jobgrade) jobgrade ,kj.empid, kj.empno,kj.empname,sum(kj.total) total, case when sum(kj.total)>ll.incenceiling and ll.active='Y' then ll.incenceiling else sum(kj.total) end Incentive ,ll.incenceiling from ( " & vbCrLf


        MSQL = MSQL & " select lk.[lineno],lk.empid, lk.empno,lk.empname,lk.jobgrade, " & vbCrLf _
                    & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
                    & " (case when [1]>0 then [1] else 0 end + case when [2]>0 then [2] else 0 end + case when [3]>0 then [3] else 0 end +  " & vbCrLf _
                    & " case when [4]>0 then [4] else 0 end +  case when [5]>0 then [5] else 0 end + case when [6]>0 then [6] else 0 end + " & vbCrLf _
                    & " case when [7]>0 then [7] else 0 end +case when [8]>0 then [8] else 0 end +  " & vbCrLf _
                    & " case when [9]>0 then [9] else 0 end + case when [10]>0 then [10] else 0 end +case when [11]>0 then [11] else 0 end +case when [12]>0 then [12] else 0 end +  " & vbCrLf _
                    & " case when [13]>0 then [13] else 0 end +case when [14]>0 then [14] else 0 end +case when [15]>0 then [15] else 0 end +case when [16]>0 then [16] else 0 end + " & vbCrLf _
                    & " case when [17]>0 then [17] else 0 end + case when [18]>0 then [18] else 0 end + case when [19]>0 then [19] else 0 end + case when [20]>0 then [20] else 0 end + " & vbCrLf _
                    & " case when [21]>0 then [21] else 0 end + case when [22]>0 then [22] else 0 end + case when [23]>0 then [23] else 0 end + case when [24]>0 then [24] else 0 end + case when [25]>0 then [25] else 0 end +  " & vbCrLf _
                    & " case when [26]>0 then [26] else 0 end + case when [27]>0 then [27] else 0 end + case when [28]>0 then [28] else 0 end + case when [29]>0 then [29] else 0 end +  " & vbCrLf _
                    & " case when [30]>0 then [30] else 0 end + case when [31]>0 then [31] else 0 end ) as total from  " & vbCrLf _
                    & " (select 1 sno,l.[Lineno],'' empid, convert(int,l.mergno)  empno,''empname,l.ptype jobgrade, sum(l.[1]) [1],sum(l.[2]) [2] ,sum(l.[3]) [3],sum(l.[4]) [4],sum(l.[5]) [5],sum(l.[6]) [6],sum(l.[7]) [7],  " & vbCrLf _
                    & " sum(l.[8]) [8],sum(l.[9]) [9],sum(l.[10]) [10],sum(l.[11]) [11],sum(l.[12]) [12],sum(l.[13]) [13],sum(l.[14]) [14],  " & vbCrLf _
                    & " sum(l.[15]) [15],sum(l.[16]) [16],sum(l.[17]) [17],sum(l.[18]) [18],sum(l.[19]) [19],sum(l.[20]) [20],sum(l.[21]) [21], " & vbCrLf _
                    & " sum(l.[22]) [22],sum(l.[23]) [23],sum(l.[24]) [24],sum(l.[25]) [25],sum(l.[26]) [26],sum(l.[27]) [27]," & vbCrLf _
                    & " sum(l.[28]) [28],sum(l.[29]) [29],sum(l.[30]) [30],sum(l.[31]) [31],0 totinc  from " & vbCrLf _
                    & " (select [Lineno],ptype,mergno, isnull([1],0) [1],isnull([2],0) [2] ,ISNULL([3],0) [3],isnull([4],0) [4],isnull([5],0) [5],isnull([6],0) [6],isnull([7],0) [7],  " & vbCrLf _
                    & " isnull([8],0) [8],isnull([9],0) [9],isnull([10],0) [10],isnull([11],0) [11],isnull([12],0) [12],isnull([13],0) [13],isnull([14],0) [14], " & vbCrLf _
                    & " isnull([15],0) [15],isnull([16],0) [16],isnull([17],0) [17],isnull([18],0) [18],isnull([19],0) [19],isnull([20],0) [20],isnull([21],0) [21], " & vbCrLf _
                    & " isnull([22],0) [22],isnull([23],0) [23],isnull([24],0) [24],isnull([25],0) [25],isnull([26],0) [26],isnull([27],0) [27], " & vbCrLf _
                    & " isnull([28],0) [28],isnull([29],0) [29],isnull([30],0) [30],isnull([31],0) [31]  from  " & vbCrLf _
                    & " (select k.dat,k.[lineno],k.mergno,k.brand,'ProdQty' ptype, isnull(sum(k.totprodqty),0) totprodqty from  " & vbCrLf _
                    & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno  from prodcost.dbo.operf b with (nolock) " & vbCrLf _
                    & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster with (nolock) group by brand,nopcs) c on c.brand=b.brand " & vbCrLf _
                    & " where b.date>=@d1 and b.date<=@d2 ) k   " & vbCrLf _
                    & " group by k.dat,k.[lineno],k.brand ,k.mergno " & vbCrLf _
                    & " union all " & vbCrLf _
                    & " select k.dat,k.[lineno],k.mergno,k.brand,'TargetQty' ptype, isnull(sum(k.target),0) target from   " & vbCrLf _
                    & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno  from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
                    & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                    & " where b.date>=@d1 and b.date<=@d2 ) k   " & vbCrLf _
                    & " group by k.dat,k.[lineno],k.brand,k.mergno   " & vbCrLf _
                    & " union all " & vbCrLf _
                    & " select k.dat,k.[lineno],k.mergno,k.brand,'ZExcessQty' ptype, isnull(sum(k.exqty),0) exqty from  " & vbCrLf _
                    & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno   from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
                    & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                    & " where b.date>=@d1 and b.date<=@d2 ) k  " & vbCrLf _
                    & " group by k.dat,k.[lineno],k.brand ,k.mergno  " & vbCrLf _
                    & " union all " & vbCrLf _
                    & " select k.dat,k.[lineno],k.mergno, k.brand,'Machine' ptype, isnull(sum(k.nomac),0) exqty from  " & vbCrLf _
                    & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno  from prodcost.dbo.operf b with (nolock) " & vbCrLf _
                    & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                    & " where b.date>=@d1 and b.date<=@d2 ) k  " & vbCrLf _
                    & " group by k.dat,k.[lineno],k.brand,k.mergno  " & vbCrLf _
                    & " union all " & vbCrLf _
                    & " select k.dat,k.[lineno],k.mergno, k.brand,'Nopcs' ptype, isnull(sum(k.nopcs),0) exqty from  " & vbCrLf _
                    & " (select datepart(d,b.date) dat, b.[lineno],b.brand,b.totprodqty totprodqty,b.nomac,c.nopcs, b.nomac*c.nopcs target,(b.totprodqty-(b.nomac*c.nopcs)) exqty,isnull(b.mergno,1) mergno   from prodcost.dbo.operf b with (nolock)  " & vbCrLf _
                    & " inner join (select brand,nopcs from prodcost.dbo.incentivemaster group by brand,nopcs) c on c.brand=b.brand  " & vbCrLf _
                    & " where b.date>=@d1 and b.date<=@d2 ) k  " & vbCrLf _
                    & " group by k.dat,k.[lineno],k.brand,k.mergno ) s  " & vbCrLf _
                    & " pivot (sum(totprodqty) FOR [dat] IN  " & vbCrLf _
                    & " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) Pv1) l  " & vbCrLf _
                    & " group by l.[Lineno],l.ptype,l.mergno " & vbCrLf _
                    & " union all " & vbCrLf _
                    & " select 2 sno, [lineno],empid,empno,empname,jobgrade, " & vbCrLf _
                    & " isnull([1],0) as [1], isnull([2],0) as [2],isnull([3],0) as [3],isnull([4],0) as [4],isnull([5],0) as [5],isnull([6],0) as [6], " & vbCrLf _
                    & " isnull([7],0) as [7],isnull([8],0) as [8],isnull([9],0) as [9],isnull([10],0) as [10],isnull([11],0) as [11],isnull([12],0) as [12], " & vbCrLf _
                    & " isnull([13],0) as [13],isnull([14],0) as [14],isnull([15],0) as [15],isnull([16],0) as [16],isnull([17],0) as [17],isnull([18],0) as [18], " & vbCrLf _
                    & " isnull([19],0) as [19],isnull([20],0) as [20],isnull([21],0) as [21],isnull([22],0) as [22],isnull([23],0) as [23],isnull([24],0) as [24], " & vbCrLf _
                    & " isnull([25],0) as [25],isnull([26],0) as [26],isnull([27],0) as [27],isnull([28],0) as [28],isnull([29],0) as [29],isnull([30],0) as [30], " & vbCrLf _
                    & " isnull([31],0) as [31]," & vbCrLf _
                    & " (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+  " & vbCrLf _
                    & " isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0)+  " & vbCrLf _
                    & " isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+ " & vbCrLf _
                    & " isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+isnull([24],0)+isnull([25],0)+ " & vbCrLf _
                    & " isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) totinc   from (" & vbCrLf

        'MSQL = MSQL & "select s.[linno] [Lineno],s.empid,s.empno,s.empname,s.jobgrade,sum(isnull(s.Actincentive,0)) amt,datepart(d,s.date) dat from (   " & vbCrLf _
        '                 & " select k.mergno, k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs,k.sampcs2, k.totprod,k.jobgrade,k.rate,k.totcnt,k.MANPOWER, k.avgpcs,k.operexspcs, k.totincentamt, " & vbCrLf _
        '                 & " case when isnull(k.operexspcs,0)>0 then sum(k.operexspcs) over (partition by k.opername,k.date,k.linno) else 0 end totoppcs," & vbCrLf _
        '                 & " round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per, " & vbCrLf _
        '                 & "round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive from (  " & vbCrLf _
        '                 & "select c.date,c.bno, c.[lineno] linno,isnull(c.mergno,1) mergno, c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,(isnull(j.pcs,0)*8) * j.MANPOWER sampcs2," & vbCrLf _
        '                 & "case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end sampcs, b.totprod,b.jobgrade,i.incentive rate,  " & vbCrLf _
        '                 & " count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt, j.MANPOWER,((isnull(j.pcs,0)*8)*j.manpower)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs,  " & vbCrLf _
        '                 & " b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs2, " & vbCrLf _
        '                 & " case when (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end) else 0 end operexspcs," & vbCrLf _
        '                 & " case when (b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ case when totmin=480 then j.manpower else count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) end )>0 then b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ " & vbCrLf _
        '                 & "case when totmin=480 then j.manpower else count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) end else 0 end operexspcs3, " & vbCrLf _
        '                 & " case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then " & vbCrLf _
        '                 & " case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end " & vbCrLf _
        '                 & " else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))   *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end " & vbCrLf _
        '                 & " else 0 end totincentamtold, " & vbCrLf
        'MSQL = MSQL & " case  when (count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))<j.manpower then " & vbCrLf _
        '                & "	case when sum(b.totprod) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno])-(isnull(j.pcs,0)*8)>0 then " & vbCrLf _
        '                & "		case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then  " & vbCrLf _
        '                & "		case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end  " & vbCrLf _
        '                & "		else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))  *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end  " & vbCrLf _
        '                & "		else 0 end " & vbCrLf _
        '                & "	else " & vbCrLf _
        '                & "		case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then " & vbCrLf _
        '                & "		case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end " & vbCrLf _
        '                & "		else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))  *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end " & vbCrLf _
        '                & "		else 0 end/(count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno])) " & vbCrLf _
        '                & "   End " & vbCrLf _
        '                & " else " & vbCrLf _
        '                & "	case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then  " & vbCrLf _
        '                & "	case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end  " & vbCrLf _
        '                & "	else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))  *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end  " & vbCrLf _
        '                & " else 0 end end  totincentamt " & vbCrLf
        'MSQL = MSQL & " from prodcost.dbo.perf1 b  " & vbCrLf _
        '& " inner join prodcost.dbo.operf c on c.bno=b.bno " & vbCrLf _
        '& " left join prodcost.dbo.PROCESSJOBMASTER j on j.OPERNAME=b.opername and rtrim(ltrim(replace(j.style,'FULL','')))=rtrim(ltrim(c.style))  " & vbCrLf _
        '& " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade " & vbCrLf _
        '& " where c.date>=@d1 and c.date<=@d2 and  b.jobgrade not in ('XII') ) k ) s   " & vbCrLf _
        '& " group by  s.[linno],s.empid, s.empno,s.empname,s.jobgrade,s.date " & vbCrLf






        MSQL = MSQL & "select s.[linno] [Lineno],s.empid,s.empno,s.empname,s.jobgrade,sum(isnull(s.Actincentive,0)) amt,datepart(d,s.date) dat from (   " & vbCrLf _
                         & " select k.mergno, k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,k.sampcs,k.sampcs2, k.totprod,k.jobgrade,k.rate,k.totcnt,k.MANPOWER, k.avgpcs,k.operexspcs, k.totincentamt, " & vbCrLf _
                         & " case when isnull(k.operexspcs,0)>0 then sum(k.operexspcs) over (partition by k.opername,k.date,k.linno) else 0 end totoppcs," & vbCrLf _
                         & " round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per, " & vbCrLf _
                         & "round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive from (  " & vbCrLf _
                         & "select c.date,c.bno, c.[lineno] linno,isnull(c.mergno,1) mergno, c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,(isnull(j.pcs,0)*8) * j.MANPOWER sampcs2," & vbCrLf _
                         & "case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end sampcs, b.totprod,b.jobgrade,i.incentive rate,  " & vbCrLf _
                         & " count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt, j.MANPOWER,((isnull(j.pcs,0)*8)*j.manpower)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs,  " & vbCrLf _
                         & " b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs2, " & vbCrLf _
                         & " case when (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end) else 0 end operexspcs," & vbCrLf _
                         & " case when (b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ case when totmin=480 then j.manpower else count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) end )>0 then b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ " & vbCrLf _
                         & "case when totmin=480 then j.manpower else count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) end else 0 end operexspcs3, " & vbCrLf _
                         & " case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then " & vbCrLf _
                         & " case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end " & vbCrLf _
                         & " else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))   *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end " & vbCrLf _
                         & " else 0 end totincentamtold, " & vbCrLf _
                         & " case  when (count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]))<j.manpower then " & vbCrLf _
                         & "	case when sum(b.totprod) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno])-(isnull(j.pcs,0)*8)>0 then " & vbCrLf _
                         & "		case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then  " & vbCrLf _
                         & "		case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end  " & vbCrLf _
                         & "		else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))  *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end  " & vbCrLf _
                         & "		else 0 end " & vbCrLf _
                         & "	else " & vbCrLf _
                         & "		case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then " & vbCrLf _
                         & "		case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end " & vbCrLf _
                         & "		else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))  *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end " & vbCrLf _
                         & "		else 0 end/(count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno])) " & vbCrLf _
                         & "   End " & vbCrLf _
                         & " else " & vbCrLf _
                         & "	case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then  " & vbCrLf _
                         & "	case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end  " & vbCrLf _
                         & "	else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))  *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end  " & vbCrLf _
                         & " else 0 end end  totincentamt " & vbCrLf _
                         & " from prodcost.dbo.perf1 b  " & vbCrLf _
                         & " inner join prodcost.dbo.operf c on c.bno=b.bno " & vbCrLf _
                         & " left join prodcost.dbo.PROCESSJOBMASTER j on j.OPERNAME=b.opername and rtrim(ltrim(replace(j.style,'FULL','')))=rtrim(ltrim(c.style))  " & vbCrLf _
                         & " left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade " & vbCrLf _
                         & " where c.date>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and c.date<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and  b.jobgrade not in ('XII') ) k ) s   " & vbCrLf _
                         & " group by  s.[linno],s.empid, s.empno,s.empname,s.jobgrade,s.date " & vbCrLf









        MSQL = MSQL & ") k " & vbCrLf _
                    & " PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
                    & " ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P  ) lk ) kj " & vbCrLf

        MSQL = MSQL & " left join prodcost.dbo.empmaster jj on jj.nempno=kj.empno and jj.nemp_id=kj.empid " & vbCrLf _
          & " left join prodcost.dbo.incentive_ceiling ll on ll.jobgrade=jj.jobgrade " & vbCrLf _
          & " where kj.jobgrade not in ('XII') group by kj.empid,kj.empno,kj.empname,jj.jobgrade,kj.jobgrade, ll.incenceiling,ll.active,kj.[lineno]) jj" & vbCrLf

        MSQL = MSQL & " left join (select j.[lineno],AVG(j.cnt) empcnt from( select k.date,k.[lineno],COUNT(k.empid) cnt from  " & vbCrLf _
        & " (select DATE,[lineno],empid,SUM(totmin) totmin from prodcost.dbo.perf1 with (nolock) where DATE>=@d1 and date<=@d2 and totprod>0 and  jobgrade in ('I','II','III','IV','V','VI') " & vbCrLf _
        & "  group by date,[lineno],empid ) k group by k.date,k.[lineno])j " & vbCrLf _
        & " group by j.[lineno] ) ll on ll.[lineno]=jj.[Lineno] ) jg " & vbCrLf _
        & " left join (select nempno,nemp_id,sum(att) attendance,(@totday-sum(att)) absnt  from rrcolor.dbo.empdailysalary " & vbCrLf _
        & " where dot>=@d1 and dot<=@d2 " & vbCrLf _
        & " group by nempno,nemp_id) gk on gk.nempno=jg.empno " & vbCrLf _
        & "left join rrcolor.dbo.empmaster emm on emm.nempno=jg.empno " & vbCrLf _
        & " group by   jg.empid,jg.empno,jg.empname,jg.acttotal,gk.attendance,gk.absnt,emm.subdept "
        '& " order by jj.[lineno],jj.jobgrade "




        Cursor = Cursors.WaitCursor
        dg.DataSource = Nothing
        dg.Rows.Clear()

        Dim cmda As New SqlCommand
        Dim dat2 As New SqlDataAdapter

        cmda.CommandText = MSQL
        cmda.Connection = con
        cmda.CommandTimeout = 600
        dat2.SelectCommand = cmda
        Dim dt2 As New DataTable
        dat2.Fill(dt2)
        dg.DataSource = dt2
        dg.Columns(0).Width = 30
        dg.Columns(1).Width = 50
        dg.Columns(2).Width = 120
        dg.Columns(3).Width = 80
        dg.Columns(4).Width = 80
        dg.Columns(5).Width = 120
        'dg.Columns(3).Visible = False

        mtotincent = False
        mdate = True
        dt2.Dispose()
        cmda.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click

        Dim datTim1 As Date = Format(CDate(mskdatefr.Text), "dd-MM-yyyy")
        Dim datTim2 As Date = Format(CDate(Mskdateto.Text), "dd-MM-yyyy")

        Dim nBetweenDayCnt As Integer = 0
        Dim i As Integer = 0
        Dim temp As DateTime

        While True

            temp = datTim1.AddDays(i)
            'AndAlso temp.DayOfWeek <> DayOfWeek.Saturday
            If temp.DayOfWeek <> DayOfWeek.Sunday Then
                nBetweenDayCnt += 1
            End If

            Dim Between As TimeSpan = datTim2 - temp

            If Between.Days <= 0 Then
                Exit While
            End If

            temp = datTim1.AddDays(i)
            i += 1
        End While
        MsgBox(nBetweenDayCnt.ToString())
        'Debug.WriteLine(nBetweenDayCnt.ToString())

    End Sub

    Private Sub Mskdateto_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Mskdateto.LostFocus
        If Val(Format(CDate(Mskdateto.Text), "dd")) > 0 Then
            txtnoday.Text = getnodaymonth(Format(CDate(mskdatefr.Text), "dd-MM-yyyy"), Format(CDate(Mskdateto.Text), "dd-MM-yyyy"))

        End If
    End Sub

    Private Sub Mskdateto_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles Mskdateto.MaskInputRejected

    End Sub

    '---nosunday
    '    declare @mydate date='2024-12-17'
    'declare @myreqday varchar(10)='Sunday'

    'select datename(weekday,dateadd(dd, 31 -1, dateadd(mm, datediff(mm,0, @mydate), 0)))
    'select datename(weekday,dateadd(dd, 31 -1, dateadd(mm, datediff(mm,0, current_timestamp), 0)))

    'select datename(weekday,datefromparts(year(@mydate),month(@mydate),31))

    'select Eomonth(@myDate)
    'select Datename(weekday,@mydate)
    'select datepart(weekday,@mydate)
    'select 4+ case when day(eomonth(@mydate))>=29 and datename(weekday,datefromparts(year(@mydate),month(@mydate),29))=@myreqday then 1 else 0 end
    '         + case when day(eomonth(@mydate))>=30 and datename(weekday,datefromparts(year(@mydate),month(@mydate),30))=@myreqday then 1 else 0 end
    '         +case when day(eomonth(@mydate))>=31 and datename(weekday,datefromparts(year(@mydate),month(@mydate),31))=@myreqday then 1 else 0 end

    '-------nosunday2 another method

    'declare @d1 as nvarchar(20) 
    'declare @dday as integer 
    'declare @myreqday varchar(10)='Sunday'
    'declare @nosund as integer
    'set @d1='2024-12-16'
    'set @dday= (datediff(day, dateadd(day, 1-day(@d1), @d1),dateadd(month, 1, dateadd(day, 1-day(@d1), @d1))))
    'set @nosund=(select 4+ case when @dday>=29 and datename(weekday,datefromparts(year(@d1),month(@d1),29))=@myreqday then 1 else 0 end
    '         + case when @dday>=30 and datename(weekday,datefromparts(year(@d1),month(@d1),30))=@myreqday then 1 else 0 end
    '         +case when @dday>=31 and datename(weekday,datefromparts(year(@d1),month(@d1),31))=@myreqday then 1 else 0 end)

    '		 select @nosund


    '    select c.PunchNo nempno,d.nemp_id,b.AttendanceDate dot,c.empname vname,c.Department,isnull(c.Designation,'') Designation,c.Category,c.section,b.Attendance att, (b.Attendance* (d.totsalary/(@dday-@nosund))) getsalary, d.daysalary,d.totsalary,d.cdepartment,d.csno,d.linno,d.sfloor from attendance b with (nolock)
    'inner join employeedetails c with (nolock) on c.empno=b.empno
    'inner join rrcolor.dbo.empmaster d on d.nempno=c.PunchNo
    'where b.AttendanceDate='2024-12-16' and b.Attendance>0


    ' no of sunday new method query

    'DECLARE @StartDate DATE = '2024-04-01';  -- Set your start date
    'DECLARE @EndDate DATE = '2025-01-31';    -- Set your end date
    'declare @nosund as int;

    'set @nosund =(SELECT 
    '    DATEDIFF(WEEK, @StartDate, @EndDate) 
    '    + CASE WHEN DATEPART(WEEKDAY, @StartDate) = 1 THEN 1 ELSE 0 END AS SundayCount);

    ''**Checking Incentive
    'select k.mergno, k.date,k.bno,k.linno,k.brand,k.tottarget,k.totprodqty,k.excesspcs,k.totmin, k.empid,k.empno,k.empname,k.opername,
    '  k.sampcs,k.sampcs2, k.totprod,k.jobgrade,k.rate,k.totcnt,k.MANPOWER, k.avgpcs,k.operexspcs, k.totincentamt,
    '                    case when isnull(k.operexspcs,0)>0 then sum(k.operexspcs) over (partition by k.opername,k.date,k.linno) else 0 end totoppcs,
    '                    round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2) per,
    '                   round(k.totincentamt*round(case when isnull(k.operexspcs,0)>0 then (convert(float,k.operexspcs)/convert(float,sum(k.operexspcs) over (partition by k.opername,k.date,k.linno order by k.opername,k.date,k.linno)))*100 else 0 end,2)/100,2) Actincentive from ( 
    '                   select c.date,c.bno, c.[lineno] linno,isnull(c.mergno,1) mergno, c.brand,(c.nomac*i.nopcs) TotTarget, c.totprodqty,(c.totprodqty-(c.nomac*i.nopcs)) excesspcs, b.totmin, b.empid,b.empno,b.empname,b.opername,(isnull(j.pcs,0)*8) * j.MANPOWER sampcs2,
    '                   case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end sampcs, b.totprod,b.jobgrade,i.incentive rate, 
    '                    count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) totcnt, j.MANPOWER,((isnull(j.pcs,0)*8)*j.manpower)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) avgpcs, 
    '                    b.totprod-(isnull(j.pcs,0)*8)/ count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) operexspcs2,
    '  case when (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end) else 0 end operexspcs,
    '                    case when (b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/ case when totmin=480 then j.manpower else count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) end )>0 then b.totprod-((isnull(j.pcs,0)*8)*j.manpower)/
    '                   case when totmin=480 then j.manpower else count(b.opername) over (partition by  b.opername,c.date,c.[lineno]  order by b.opername,c.date,c.[lineno]) end else 0 end operexspcs3,
    '                    case when (c.totprodqty-(c.nomac*i.nopcs))>0 and (b.totprod-(case when b.totmin=480 then  (isnull(j.pcs,0)*8) / j.MANPOWER else (isnull(j.pcs,0)*b.totmin)/60 end))>0 then
    '                    case when b.totmin=480 then (((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1)) *(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end
    '                    else ((((c.totprodqty-(c.nomac*i.nopcs))/isnull(c.mergno,1))   *(convert(float,b.totmin)/480))*(i.incentive*j.manpower))*case when isnull(b.totmin,0)>0 then (convert(float,b.totmin)/480) else 0 end end
    '                    else 0 end totincentamt from prodcost.dbo.perf1 b 
    '                    inner join prodcost.dbo.operf c on c.bno=b.bno
    '                    left join prodcost.dbo.PROCESSJOBMASTER j on j.OPERNAME=b.opername and rtrim(ltrim(replace(j.style,'FULL','')))=rtrim(ltrim(c.style)) 
    '                    left join prodcost.dbo.incentivemaster i on i.brand=c.brand and i.grade=b.jobgrade
    '                    where c.date>='2025-05-01' and c.date<='2025-05-31' and  b.jobgrade not in ('XII') ) k 
    '  -- where k.empno=12100687
    '   order by k.date,k.opername, k.empno


End Class
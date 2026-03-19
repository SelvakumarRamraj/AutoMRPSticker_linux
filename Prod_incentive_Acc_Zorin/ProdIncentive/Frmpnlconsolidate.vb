Imports System.Data
Imports System.IO
Imports Syste
Imports System.Windows.Forms
Imports System.Data.OleDb


Public Class Frmpnlconsolidate
    Dim msql As String
    Dim msql2 As String
    Dim msql3 As String

    Private Sub Frmpnlconsolidate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width

        msql = "select distinct compnyname from oadm"

        'msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " WHERE CMP_ID='" & mcmpid & "' group by " & Trim(mfield) & " order by " & Trim(mfield)

        Dim cmd As New OleDb.OleDbCommand(msql, con)
        'Dim CMD As New OleDb.OleDbCommand("SELECT sectionname FROM section GROUP BY sectionname ORDER BY sectionname", con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        ''Dim DR As SqlDataReader
        Dim DR As OleDb.OleDbDataReader
        DR = cmd.ExecuteReader
        If DR.HasRows = True Then
            While DR.Read
                curcompname = DR.Item("compnyname") & vbNullString
            End While
        End If
        DR.Close()
        cmd.Dispose()
        Call chktable()

        If mautopnl = "Y" Then

            If Val(Format(Now(), "MM")) >= 4 And Val(Format(Now(), "MM")) <= 12 Then
                mskdatefr.Text = CDate((Format(Now(), "yyyy") + "-04-01"))
                '--set @d4=convert(nvarchar(4),datepart(yyyy,getdate()))+'-04-01'
            End If

            If Val(Format(Now, "MM")) >= 1 And Val(Format(Now, "MM")) <= 3 Then
                mskdatefr.Text = CDate((Format(DatePart(DateInterval.Year, Now()) - 1) + "-04-01"))
                'set @d5=convert(nvarchar(4),(datepart(yyyy,@d2)-1))+'-04-01'
            End If

            mskdateto.Text = CDate(Format(Now, "yyyy-MM-dd"))

            cmdexport.PerformClick()
            Me.Close()
            'End

        End If




    End Sub

    Private Sub exportpnl()

        msql = "DECLARE @frmdat AS nvarchar(20)" & vbCrLf _
               & "DECLARE @todat AS nvarchar(20)" & vbCrLf _
               & "DECLARE @PEC INT" & vbCrLf _
               & "SET @frmdat ='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'" & vbCrLf _
               & " SET @todat ='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "'" & vbCrLf _
               & "DECLARE @level AS int" & vbCrLf _
               & "DECLARE @hidetitle AS nvarchar(1)" & vbCrLf _
               & "SET @level =10 --{?Levels}" & vbCrLf _
               & "SET @PEC   = '1'" & vbCrLf _
               & "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED ;" & vbCrLf _
& "WITH CTB AS (SELECT  A.AcctCode,A.AcctName,SUM(A.OB) OB,SUM(A.Debit) Debit,SUM(a.Credit) Credit,sum(a.curperiod) curperiod,sum(a.balance) balance, a.Levels,A.GrpLine,A.GroupMask,A.FatherNum,A.Postable   FROM " & vbCrLf _
& "(select OC.acctcode,oc.acctname,isnull(sum(l.ob),0) ob,isnull(sum(l.debit),0) debit,isnull(sum(l.credit),0) credit,isnull(sum(l.curbal),0) curperiod,(isnull(sum(l.ob),0)+ isnull(sum(l.curbal),0)) as balance, " & vbCrLf _
        & "oc.Levels, oc.GrpLine, oc.GroupMask, oc.FatherNum, oc.Postable   from OACT oc " & vbCrLf _
  & "   Left Join " & vbCrLf _
   & " (select k.account,SUM(k.ob) ob,SUM(k.debit) debit,SUM(k.credit) credit,SUM(k.curbal) curbal from" & vbCrLf _
    & "   (select Account, 0 as ob,sum(debit) debit,SUM(credit) credit, sum(debit-credit) curbal from jdt1 where  RefDate<=@todat group by account" & vbCrLf _
         & " union all" & vbCrLf _
   & "  SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit)*-1 as curbal from jdt1 where Account='15010000000'  and refDATE<=@todat) as  Curbal" & vbCrLf _
    & "  FROM OACT where AcctCode='40004000000') k group by k.account ) l on l.Account=oc.AcctCode " & vbCrLf _
    & " where oc.GroupMask>=1 and  oc.GroupMask<4  and oc.acctcode NOT IN ('50070015000','50070001000','40001070000','50070000000','50070009000','50070005000') " & vbCrLf _
    & "GROUP BY oc.AcctCode,oc.AcctName,oc.Levels,oc.GrpLine,oc.GroupMask,oc.FatherNum,oc.Postable ) A " & vbCrLf _
    & "GROUP BY A.AcctCode,A.AcctName,a.Levels,A.GrpLine,A.GroupMask,A.FatherNum,A.Postable " & vbCrLf _
    & "UNION ALL " & vbCrLf _
 & "	select p.AcctCode,p.AcctName,c.OB,c.debit,c.Credit,C.curperiod,c.balance,p.Levels, p.GrpLine, p.GroupMask, p.fathernum, p.Postable 	from CTB as c  " & vbCrLf _
   & " inner join OACT as p on c.FatherNum=p.AcctCode" & vbCrLf _
   & "WHERE c.GroupMask>=1 and c.GroupMask<4 AND	 P.AcctCode NOT IN ('50070015000','50070001000','40001070000','50070000000','50070009000','50070005000'))" & vbCrLf _
    & "INSERT INTO tmpbsheet2" & vbCrLf _
    & " SELECT isnull(acctcode,'') as acctcode ,isnull(AcctName,'') as 'Liabilities' ,isnull(cb,0)*-1 as cb,isnull(postable,'') as postable,isnull(levels,0) as levels,  isnull(Zacctcode,'') as zacctcode ," & vbCrLf _
    & "case when isnull(ZAcctName,'')='    CURRENT ASSETS, LOANS , ADVANCES & DEPOSITS' then '    CURRENT ASSETS'  else isnull(ZAcctName,'') end  as 'Assets', isnull(Zcb,0) as zcb,isnull(Zpostable,'') as zpostable," & vbCrLf _
    & "isnull(Zlevels,0) as zlevels ,  (select CompnyName from OADM) compcode,@frmdat datefr,@todat dateto   from " & vbCrLf _
    & " (select ROW_NUMBER() OVER(ORDER BY k.ACCTCODE) AS ROWNUM, k.acctcode,k.AcctName ,k.OB,k.Debit,k.Credit,k.CB,	 k.Levels,k.GrpLine,k.Postable from " & vbCrLf _
 & " (select R.acctcode,replace(space((R.Levels-1)*4),' ',' ')+R.AcctName AcctName,SUM(R.ob) OB,SUM(R.Debit) Debit,SUM(R.Credit) Credit,SUM(R.ob)+SUM(R.Debit)-SUM(R.Credit) CB, R.Levels, R.GrpLine, R.Postable from CTB R" & vbCrLf _
  & " WHERE R.LEVELS<= 10 and R.GroupMask>1 and r.GroupMask <4 --AND R.AcctCode NOT IN('50030000000') --and AccntntCod = 1" & vbCrLf _
     & " group by R.acctcode,R.AcctName,R.Levels,R.GrpLine,R.groupmask,Postable " & vbCrLf _
     & "   union all" & vbCrLf _
      & "select '33106000000' acctcode,'Net Profit' AcctName,SUM(l.ob) OB,SUM(l.Debit) Debit,SUM(l.Credit) Credit,(SUM(l.ob)+SUM(l.Debit)-SUM(l.Credit))*-1 CB, 1 Levels,1 GrpLine,'Y' Postable 	  from CTB l" & vbCrLf _
   & "WHERE  l.Postable='Y') k where k.CB<>0) ab full join " & vbCrLf _
    & "(select ROW_NUMBER() OVER(ORDER BY k.ACCTCODE) AS ROWNUM, k.acctcode zacctcode,k.AcctName zacctname ,k.OB zob,k.Debit zdebit,k.Credit zcredit,k.CB zcb,  k.Levels zlevels,k.GrpLine zgrpline,	  k.Postable  as  zpostable from " & vbCrLf _
   & "(select R.acctcode,replace(space((R.Levels-1)*4),' ',' ')+R.AcctName AcctName,SUM(R.ob) OB,SUM(R.Debit) Debit,SUM(R.Credit) Credit,SUM(R.ob)+SUM(R.Debit)-SUM(R.Credit) CB, R.Levels, R.GrpLine, R.Postable from CTB R " & vbCrLf _
   & " WHERE R.LEVELS<= 10 and R.GroupMask<2 --AND R.AcctCode NOT IN('50030000000') --and AccntntCod = 1 " & vbCrLf _
   & " group by R.acctcode,R.AcctName,R.Levels,R.GrpLine,R.groupmask,Postable ) k " & vbCrLf _
      & " where k.CB<>0  ) bc on bc.ROWNUM=ab.ROWNUM"


        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        If Trim(msameserver) = "Y" Then
            msql2 = "delete from " & Trim(rdbnam) & ".dbo.tmpbsheet2 where compcode='" & Trim(curcompname) & "'"
            Dim CMD2 As New OleDbCommand(msql2, con)
            CMD2.ExecuteNonQuery()
            CMD2.Dispose()

            msql2 = "delete from tmpbsheet2 where compcode='" & Trim(curcompname) & "'"
            Dim CMD3 As New OleDbCommand(msql2, con)
            CMD3.ExecuteNonQuery()
            CMD3.Dispose()


            'msql = "delete from " & Trim(linkserver) & "." & Trim(rdbnam) & ".dbo.tmpbsheet2 where compcode='" & Trim(curcompname) & "'"
            Dim CMD1 As New OleDbCommand(msql, con)
            CMD1.ExecuteNonQuery()
            CMD1.Dispose()

            If msamedb = "N" Then
                msql = "insert into " & Trim(rdbnam) & ".dbo.tmpbsheet2 select * from tmpbsheet2"
                Dim CMD4 As New OleDbCommand(msql, con)
                CMD4.ExecuteNonQuery()
                CMD4.Dispose()
            End If

        Else

            msql2 = "delete from " & Trim(linkserver) & "." & Trim(rdbnam) & ".dbo.tmpbsheet2 where compcode='" & Trim(curcompname) & "'"
            Dim CMD2 As New OleDbCommand(msql2, con)
            CMD2.ExecuteNonQuery()
            CMD2.Dispose()

            msql2 = "delete from tmpbsheet2 where compcode='" & Trim(curcompname) & "'"
            Dim CMD3 As New OleDbCommand(msql2, con)
            CMD3.ExecuteNonQuery()
            CMD3.Dispose()

            'msql = "delete from " & Trim(linkserver) & "." & Trim(rdbnam) & ".dbo.tmpbsheet2 where compcode='" & Trim(curcompname) & "'"
            Dim CMD1 As New OleDbCommand(msql, con)
            CMD1.ExecuteNonQuery()
            CMD1.Dispose()

            If Len(Trim(linkserver)) > 0 Then
                msql = "insert into " & Trim(linkserver) & "." & Trim(rdbnam) & ".dbo.tmpbsheet2 select * from tmpbsheet2"
                Dim CMD4 As New OleDbCommand(msql, con)
                CMD4.CommandTimeout = 600
                CMD4.ExecuteNonQuery()
                CMD4.Dispose()
            End If
        End If


        '" & Trim(linkserver) & "." & Trim(rdbnam) & ".dbo.



    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexport.Click
        Cursor = Cursors.WaitCursor
        'Call exportpnl()
        Call loadpnl()
        Cursor = Cursors.Default
    End Sub

    Private Sub loadpnl()


        msql3 = "DECLARE @frmdat date " & vbCrLf _
 & "DECLARE @todat date" & vbCrLf _
 & "DECLARE @PEC INT" & vbCrLf _
  & "declare @salper as float" & vbCrLf _
& "SET @frmdat ='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'" & vbCrLf _
& "SET @todat ='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "'" & vbCrLf _
& " declare @level as int" & vbCrLf _
& "SET @level =10" & vbCrLf _
& " declare @newyr as nvarchar(1)" & vbCrLf _
& " select @newyr= case when @todat >= '" & Trim(nxtyr) & "' then  'N' else 'Y' end" & vbCrLf _
& "SET @PEC   = '1'" & vbCrLf _
& " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED ;" & vbCrLf _
& " select @salper= SUM(credit-debit) from jdt1 where Account in (select acctcode from OACT where FatherNum= '40001000000') and RefDate>=@frmdat and RefDate<=@todat  and transtype not in ('-3');" & vbCrLf _
& "if @newyr='N'" & vbCrLf _
& "WITH CTB AS ( SELECT  A.AcctCode,A.AcctName,SUM(A.OB) OB,SUM(A.Debit) Debit,SUM(a.Credit) Credit,sum(a.curperiod) curperiod,sum(a.balance) balance, a.Levels,A.GrpLine,A.GroupMask,A.FatherNum,A.Postable   FROM " & vbCrLf _
 & " (select OC.acctcode,oc.acctname,isnull(l.ob,0) ob,isnull(l.debit,0) debit,isnull(l.credit,0) credit,isnull(l.curbal,0) curperiod,(isnull(l.ob,0)+ isnull(l.curbal,0)) as balance, " & vbCrLf _
 & "       oc.Levels, oc.GrpLine, oc.GroupMask, oc.FatherNum, oc.Postable   from OACT oc " & vbCrLf _
 & "  Left Join" & vbCrLf _
   & " (select k.account,SUM(k.ob) ob,SUM(k.debit) debit,SUM(k.credit) credit,SUM(k.curbal) curbal from " & vbCrLf _
   & " (select Account, 0 as ob,sum(debit) debit,SUM(credit) credit, sum(debit-credit) curbal from jdt1 where RefDate>=@frmdat and RefDate<=@todat and Account not in ('40001030000') and transtype not in ('-3') group by account) k 	group by k.account " & vbCrLf _
   & "   union all " & vbCrLf _
   & "           --closing stock" & vbCrLf _
   & "    select k.acctcode,sum(k.ob) ob,sum(k.debit) debit,sum(k.credit) credit,sum(k.curbal) curbal from " & vbCrLf _
   & " (SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit)*-1 as curbal from jdt1 where Account in (select acctcode from oact where FatherNum='15000000000') and refDATE<=@todat) as  Curbal  FROM OACT where AcctCode='40004000000') k " & vbCrLf _
   & "	  group by k.acctcode " & vbCrLf _
   & " union all " & vbCrLf _
  & "--opening Stock" & vbCrLf _
 & " select k.acctcode,sum(k.ob) ob,sum(k.debit) debit,sum(k.credit) credit,sum(k.curbal) curbal from	" & vbCrLf _
 & "(SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit) as curbal from jdt1 where Account in (select acctcode from oact where FatherNum='15000000000')  and   refDATE<@frmdat) as  Curbal  FROM OACT where AcctCode='50011000000') k " & vbCrLf _
 & "  group by k.acctcode	 " & vbCrLf _
  & "--sales" & vbCrLf _
   & " union all" & vbCrLf


        msql3 = msql3 & "select lk.taccount as acctcode,SUM(lk.ob) ob,SUM(lk.debit) debit,SUM(lk.credit) credit,SUM(lk.curbal) curbal from " & vbCrLf _
            & "(select b.Account,CASE when c.state IS null OR c.state='TN' then '400010300011'" & vbCrLf _
                  & "when c.state IS not null OR c.state not in('TN') then '400010300021'  " & vbCrLf _
                  & "else '400010300011' end taccount, " & vbCrLf _
                  & "CASE when c.state IS null OR c.state='TN' then 'LOCAL SALES' " & vbCrLf _
                  & "when c.state IS not null OR c.state not in('TN') then 'INTERSTATE SALES'  " & vbCrLf _
                  & "else 'LOCAL SALES 1'  " & vbCrLf _
                  & "end stype,0 ob, SUM(b.debit) debit,SUM(b.credit) credit,  SUM(b.Debit-b.Credit) curbal from jdt1 b " & vbCrLf _
           & "left join (select cardcode,state from CRD1 where AdresType='B' group by cardcode,state) c on c.cardcode=b.contraact " & vbCrLf _
            & " where b.Account= '40001030000' and  b.RefDate>=@frmdat and b.RefDate<=@todat  and transtype not in (-3)" & vbCrLf _
            & " group by b.account,CASE when c.state is null OR c.state ='TN' then 'LOCAL SALES'  " & vbCrLf _
                  & " when c.state IS not null OR c.state not in('TN') then 'INTERSTATE SALES' " & vbCrLf _
                  & " else 'LOCAL SALES 1' end,CASE when c.state IS null OR c.state='TN' then '400010300011' " & vbCrLf _
                  & " when c.state IS not null OR c.state not in('TN') then '400010300021' " & vbCrLf _
                  & " else '400010300011' end  ) lk group by lk.taccount,lk.account" & vbCrLf



        msql3 = msql3 & " --gp" & vbCrLf _
 & "union all" & vbCrLf _
 & "select '50030000000' as acctcode,0 as ob,0 as debit,0 as credit,SUM(kk.balance) as curbal from " & vbCrLf _
 & "(select OC.acctcode,oc.acctname,isnull(l.ob,0) ob,isnull(l.debit,0) debit,isnull(l.credit,0) credit,isnull(l.curbal,0) curperiod,(isnull(l.ob,0)+ isnull(l.curbal,0)) as balance from OACT oc" & vbCrLf _
    & "  Left Join" & vbCrLf _
 & "(select k.account,SUM(k.ob) ob,SUM(k.debit) debit,SUM(k.credit) credit,SUM(k.curbal) curbal from" & vbCrLf _
 & "(select Account, 0 as ob,sum(debit) debit,SUM(credit) credit, sum(debit-credit) curbal from jdt1 where RefDate>=@frmdat and RefDate<=@todat  and transtype not in ('-3') group by account) k 	group by k.account " & vbCrLf _
    & " union all" & vbCrLf _
   & " --cl stk" & vbCrLf _
   & "select k.acctcode,sum(k.ob) ob,sum(k.debit) debit,sum(k.credit) credit,sum(k.curbal) curbal from " & vbCrLf _
   & "(SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit)*-1 as curbal from jdt1 where Account in (select acctcode from oact where FatherNum='15000000000') and refDATE<=@todat) as  Curbal  FROM OACT where AcctCode='40004000000') k  group by k.acctcode " & vbCrLf _
   & " UNION ALL " & vbCrLf _
   & "select k.acctcode,0 ob,0 debit,0 credit,sum(k.curbal) curbal from	 " & vbCrLf _
 & "(SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit) as curbal from jdt1 where Account in (select acctcode from oact where FatherNum='15000000000')  and  refDATE<@frmdat) as  Curbal  FROM OACT where AcctCode='50011000000') k " & vbCrLf _
 & " group by k.acctcode	) l on l.Account=oc.AcctCode " & vbCrLf _
 & "where   oc.acctcode NOT IN ('50070015000','50070001000','40001070000','50070000000','50070009000','50070005000','50070013000','50070017000','50070003000','50070004000','40002020000','40002030000','40002040000','40002050000','40002060000','40002070000')" & vbCrLf _
 & " and CONVERT(float,oc.acctcode)>=convert(float,(select min(CONVERT(float,AcctCode)) dd from OACT where GroupMask=4))  and CONVERT(float,oc.acctcode) <convert(float,(select acctcode from OACT where AcctName='INDIRECT EXPENSES')))kk) l on l.Account=oc.AcctCode " & vbCrLf _
 & "where oc.GroupMask>=4  and oc.acctcode NOT IN ('50070015000','50070001000','40001070000','50070000000','50070009000','50070005000','50070013000','50070017000','50070003000','50070004000')) A " & vbCrLf _
 & "GROUP BY A.AcctCode,A.AcctName,a.Levels,A.GrpLine,A.GroupMask,A.FatherNum,A.Postable " & vbCrLf _
 & "--np" & vbCrLf _
    & "UNION ALL" & vbCrLf _
  & "select p.AcctCode,p.AcctName,c.OB,c.debit,c.Credit,C.curperiod,c.balance,  p.Levels,p.GrpLine,p.GroupMask,p.fathernum,p.Postable 	from CTB as c " & vbCrLf _
 & "inner join OACT as p on c.FatherNum=p.AcctCode" & vbCrLf _
 & "WHERE CONVERT(INTEGER,LEFT(P.ACCTCODE,1))>=4  AND P.AcctCode NOT IN ('50070015000','50070001000','40001070000','50070000000','50070009000','50070005000','50070013000','50070017000','50070003000','50070004000'))" & vbCrLf _
& "INSERT INTO TMPPNL2 (SPCE,ACCTCODE,ACCTNAME,OB,DEBIT,CREDIT,CURRPERIOD,CB,GP,POSTABLE,LEVELS,compcode,datefr,dateto)" & vbCrLf _
& "SELECT LEN(rtrim(c.acctname))-len(rtrim(ltrim(c.ACCTNAME))) spcl,C.ACCTCODE,C.ACCTNAME,C.OB,C.DEBIT,C.CREDIT,C.curperiod*-1 as currperiod,c.CB*-1 as cb,CASE when c.GP>0 then c.GP else c.GP*-1 end gp , c.POSTABLE,C.LEVELS," & vbCrLf _
   & "(select CompnyName from OADM) compcode,@frmdat datefr,@todat dateto FROM" & vbCrLf _
& "(select R.acctcode, space((R.Levels-1)*4)+R.AcctName AcctName,SUM(r.OB) OB,SUM(r.Debit) Debit,SUM(r.Credit) Credit,sum(r.curperiod) curperiod," & vbCrLf _
 & " CASE when r.AcctCode='500000000000000' then SUM(r.balance)-(select SUM(balance) from ctb where AcctCode IN(  '50030000000','50050000000')) " & vbCrLf _
 & " else sum(r.balance) end cb,	R.Levels,R.GrpLine,	 R.Postable,convert(decimal(10,2),(SUM(r.balance) /@salper)*100)  AS GP	from CTB R " & vbCrLf _
& "	WHERE R.LEVELS<= 10 and R.GroupMask IN ( 4,5) " & vbCrLf _
& "	group by R.acctcode,R.AcctName,R.Levels,R.GrpLine,R.groupmask,r.Postable " & vbCrLf _
    & "union all " & vbCrLf _
 & " select '60000100000' acctcode,case when ln.cb>0 then 'Net Profit' else 'Net Profit' end AcctName,0 OB,0 Debit,0 Credit,0 curperiod,ln.cb cb, 1 Levels,1 GrpLine,	 'Y' Postable,convert(decimal(10,2),(ln.cb /@salper)*100) AS gp from " & vbCrLf _
  & "(select sum(n.balance) cb	 from CTB n " & vbCrLf _
  & " WHERE n.LEVELS<=10  and n.GroupMask IN ( 4,5) and n.Postable='Y' and AcctCode not in ('50030000000' ) )ln ) c" & vbCrLf _
 & "where Levels<=@level and cb<>0" & vbCrLf

        msql3 = msql3 & "if @newyr='Y' " & vbCrLf _
        & "WITH CTB AS ( SELECT  A.AcctCode,A.AcctName,SUM(A.OB) OB,SUM(A.Debit) Debit,SUM(a.Credit) Credit,sum(a.curperiod) curperiod,sum(a.balance) balance, a.Levels,A.GrpLine,A.GroupMask,A.FatherNum,A.Postable   FROM " & vbCrLf _
         & " (select OC.acctcode,oc.acctname,isnull(l.ob,0) ob,isnull(l.debit,0) debit,isnull(l.credit,0) credit,isnull(l.curbal,0) curperiod,(isnull(l.ob,0)+ isnull(l.curbal,0)) as balance, " & vbCrLf _
         & "       oc.Levels, oc.GrpLine, oc.GroupMask, oc.FatherNum, oc.Postable  from OACT oc " & vbCrLf _
        & "Left Join" & vbCrLf _
    & "(select k.account,SUM(k.ob) ob,SUM(k.debit) debit,SUM(k.credit) credit,SUM(k.curbal) curbal from " & vbCrLf _
    & "(select Account, sum(debit-credit) ob,0 as debit,0 as credit,0 as curbal from jdt1 where DateDiff(dd, refdate,@frmdat)>0  and Account not in ('40001030000') group by account " & vbCrLf _
    & " union all" & vbCrLf _
     & "select Account, 0 as ob,sum(debit) debit,SUM(credit) credit, sum(debit-credit) curbal from jdt1 where RefDate>=@frmdat and RefDate<=@todat and Account not in ('40001030000') and transtype not in ('-3') group by account) k group by k.account " & vbCrLf _
     & "union all " & vbCrLf _
     & "         --closing stock" & vbCrLf _
               & "select k.acctcode,sum(k.ob) ob,sum(k.debit) debit,sum(k.credit) credit,sum(k.curbal) curbal from " & vbCrLf _
  & "(SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit)*-1 as curbal from jdt1 where Account='15010000000'  and refDATE<=@todat) as  Curbal FROM OACT where AcctCode='40004000000' " & vbCrLf _
        & "union all" & vbCrLf _
   & "SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit)*-1 as curbal from jdt1 where Account='15010000001'  and refDATE<=@todat) as  Curbal FROM OACT where AcctCode='40004000000') k " & vbCrLf _
   & "group by k.acctcode" & vbCrLf _
      & "  UNION ALL" & vbCrLf _
 & "--opening Stock " & vbCrLf _
     & "select k.acctcode,sum(k.ob) ob,sum(k.debit) debit,sum(k.credit) credit,sum(k.curbal) curbal from	" & vbCrLf _
 & "(SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit) as curbal from jdt1 where Account='15010000000' and transtype in ('-2','310000001')  and   refDATE<=@frmdat) as  Curbal  FROM OACT where AcctCode='50011000000'	" & vbCrLf _
 & " union all" & vbCrLf _
  & "SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit) as curbal from jdt1 where Account='15010000001' and transtype in ('-2','310000001')  and                 refDATE<=@frmdat) as  Curbal   FROM OACT where AcctCode='50011000000') k " & vbCrLf _
  & "group by k.acctcode	" & vbCrLf _
& " --sales" & vbCrLf _
       & "union all" & vbCrLf

        '        msql3 = msql3 & "select lk.taccount as acctcode,SUM(lk.ob) ob,SUM(lk.debit) debit,SUM(lk.credit) credit,SUM(lk.curbal) curbal from	 " & vbCrLf _
        ' & "(select case when k.utax='Taxable' and k.stype='InterState Sales' then '400010300021' " & vbCrLf _
        '             & " when k.utax='Non Taxable' and k.stype='InterState Sales' then  '400010300022'" & vbCrLf _
        '             & " when k.utax='Non Taxable' and k.stype='Local Sales' then   '400010300012' " & vbCrLf _
        '             & " when k.utax='Taxable' and k.stype='Local Sales' then    '400010300011' end as taccount," & vbCrLf _
        '      & "k.account,0 as ob,isnull(sum(k.debit),0) debit,isnull(sum(k.credit),0) credit, isnull(sum(k.debit-k.credit),0) as curbal,k.utax, k.stype from  " & vbCrLf _
        '  & "(select t0.transid,t0.TransType, t0.refdate,t0.account,t0.contraact,v.docentry,t0.baseref,t0.Credit,t0.Debit,cr.state,v.taxcode U_taxcode,   CASE  when v.TaxCode is null or LEFT(v.taxcode,1)='N' then 'Non Taxable' else 'Taxable' end as utax,case when cr.state='TN' then 'Local Sales' else 'InterState Sales' end stype from jdt1 t0 " & vbCrLf _
        '  & "left join (select cardcode,state,AdresType from CRD1 group by CardCode,State,AdresType) cr on cr.CardCode=t0.contraact and cr.AdresType='B' " & vbCrLf _
        '    & " left join(select b.docentry,case when  PATINDEX('%[0-9]%',b1.taxcode)>0 then  substring(b1.taxcode,1,PATINDEX('%[0-9]%',b1.taxcode)-1) else b1.taxcode end taxcode,b.ObjType,b.CANCELED from Oinv b " & vbCrLf _
        '   & " left join inv1 b1 on b1.DocEntry =b.DocEntry  " & vbCrLf _
        '   & " where b.CANCELED='N' and b1.ItemCode not in ('FrightCharges')  AND LEN(LTRIM(RTRIM(B1.TAXCODE)))>0 " & vbCrLf _
        '   & " group by b.docentry,case when  PATINDEX('%[0-9]%',b1.taxcode)>0 then  substring(b1.taxcode,1,PATINDEX('%[0-9]%',b1.taxcode)-1) else b1.taxcode end ,b.ObjType,b.CANCELED) v on v.DocEntry=t0.createdby and  v.ObjType= t0.TransType	 " & vbCrLf _
        '  & " where t0.account='40001030000' and TransType=13 " & vbCrLf _
        '        & " union all " & vbCrLf _
        '  & " select TransId,TransType,RefDate,Account,ContraAct, '' AS docentry,BaseRef,Credit,Debit,'TN' AS state,'No Tax' AS u_taxcode, 'Non Taxable' AS utax,'Local Sales' as stype from jdt1 where ContraAct not in (select CardCode from CRD1 where AdresType='B') and transtype not in (30) and Account='40001030000' " & vbCrLf _
        '        & " union all " & vbCrLf _
        '  & " select TransId,TransType,RefDate,Account,ContraAct, '' AS docentry,BaseRef,Credit,Debit,'TN' AS state,'No Tax' AS u_taxcode, 'Non Taxable' AS utax,'Local Sales' as stype from jdt1 where  Account='40001030000' and TransType=30 " & vbCrLf _
        '        & " union all " & vbCrLf _
        '  & "  select TransId,TransType,RefDate,Account,ContraAct, '' AS docentry,BaseRef,Credit,Debit,'TN' AS state,'No Tax' AS u_taxcode, 'Non Taxable' AS utax,'Local Sales' as stype from jdt1 where  Account='40001030000' and (TransType=18 or TransType=19) " & vbCrLf _
        '        & " union all " & vbCrLf _
        '  & " select t0.transid,t0.TransType, t0.refdate,t0.account,t0.contraact,rn.DocEntry,t0.baseref,t0.Credit,t0.Debit,cr.state,rn.taxcode U_taxcode, case when rn.TaxCode is null or LEFT(rn.taxcode,1)='N' then 'Non Taxable' else 'Taxable' end as utax, case when cr.state='TN' then 'Local Sales' else 'InterState Sales' end stype " & vbCrLf _
        '  & " from jdt1 t0 " & vbCrLf _
        '  & " left join (select cardcode,state,AdresType from CRD1 group by CardCode,State,AdresType) cr on cr.CardCode=t0.contraact and cr.AdresType='B' " & vbCrLf _
        '  & " left join(select b.docentry,case when  PATINDEX('%[0-9]%',b1.taxcode)>0 then  substring(b1.taxcode,1,PATINDEX('%[0-9]%',b1.taxcode)-1) else b1.taxcode end taxcode ,b.ObjType,b.CANCELED from Orin b " & vbCrLf _
        '   & " left join rin1 b1 on b1.DocEntry =b.DocEntry " & vbCrLf _
        '         & "			where b.CANCELED='N' and b1.ItemCode not in ('FrightCharges')   AND LEN(LTRIM(RTRIM(B1.TAXCODE)))>0 " & vbCrLf _
        '   & " group by b.docentry,case when  PATINDEX('%[0-9]%',b1.taxcode)>0 then  substring(b1.taxcode,1,PATINDEX('%[0-9]%',b1.taxcode)-1) else b1.taxcode end ,b.ObjType,b.CANCELED) rn on rn.DocEntry=t0.createdby and  rn.ObjType= t0.TransType " & vbCrLf _
        '  & " where t0.account='40001030000' and TransType=14) k where  k.RefDate>=@frmdat and k.RefDate<=@todat  " & vbCrLf _
        '  & " group by k.Account,k.utax,k.stype " & vbCrLf _
        '        & " union all " & vbCrLf _
        '  & " select case when k.utax='Taxable' and k.stype='InterState Sales' then '400010300021' " & vbCrLf _
        '  & "           when k.utax='Non Taxable' and k.stype='InterState Sales' then  '400010300022' " & vbCrLf _
        '  & "           when k.utax='Non Taxable' and k.stype='Local Sales' then   '400010300012' " & vbCrLf _
        '  & "           when k.utax='Taxable' and k.stype='Local Sales' then    '400010300011' end as taccount, " & vbCrLf _
        '  & "    k.account,isnull(sum(k.debit-k.credit),0) as ob,0 as debit,0 as credit,0 as curbal,k.utax, k.stype from  " & vbCrLf _
        '  & " (select t0.transid,t0.TransType, t0.refdate,t0.account,t0.contraact,v.docentry,t0.baseref,t0.Credit,t0.Debit,cr.state,v.taxcode U_taxcode,   CASE  when v.TaxCode is null or LEFT(v.taxcode,1)='N' then 'Non Taxable' else 'Taxable' end as utax,case when cr.state='TN' then 'Local Sales' else 'InterState Sales' end stype from jdt1 t0 " & vbCrLf _
        ' & "	left join (select cardcode,state,AdresType from CRD1 group by CardCode,State,AdresType) cr on cr.CardCode=t0.contraact and cr.AdresType='B' " & vbCrLf _
        ' & "	  left join(select b.docentry,case when  PATINDEX('%[0-9]%',b1.taxcode)>0 then  substring(b1.taxcode,1,PATINDEX('%[0-9]%',b1.taxcode)-1) else b1.taxcode end taxcode,b.ObjType,b.CANCELED from Oinv b " & vbCrLf _
        '  & "	left join inv1 b1 on b1.DocEntry =b.DocEntry  " & vbCrLf _
        '  & "	where b.CANCELED='N' and b1.ItemCode not in ('FrightCharges')   AND LEN(LTRIM(RTRIM(B1.TAXCODE)))>0 " & vbCrLf _
        '& "	group by b.docentry,case when  PATINDEX('%[0-9]%',b1.taxcode)>0 then  substring(b1.taxcode,1,PATINDEX('%[0-9]%',b1.taxcode)-1) else b1.taxcode end ,b.ObjType,b.CANCELED) v on v.DocEntry=t0.createdby and  v.ObjType= t0.TransType	 " & vbCrLf _
        '& "		where t0.account='40001030000' and TransType=13 " & vbCrLf _
        '       & " union all " & vbCrLf _
        ' & "	select TransId,TransType,RefDate,Account,ContraAct, '' AS docentry,BaseRef,Credit,Debit,'TN' AS state,'No Tax' AS u_taxcode, 'Non Taxable' AS utax,'Local Sales' as stype from jdt1 where ContraAct not in (select CardCode from CRD1 where AdresType='B') and transtype not in (30) and Account='40001030000' " & vbCrLf _
        '    & "    union all " & vbCrLf _
        ' & "	select TransId,TransType,RefDate,Account,ContraAct, '' AS docentry,BaseRef,Credit,Debit,'TN' AS state,'No Tax' AS u_taxcode, 'Non Taxable' AS utax,'Local Sales' as stype from jdt1 where  Account='40001030000' and TransType=30 " & vbCrLf _
        '      & " union all " & vbCrLf _
        ' & "	select TransId,TransType,RefDate,Account,ContraAct, '' AS docentry,BaseRef,Credit,Debit,'TN' AS state,'No Tax' AS u_taxcode, 'Non Taxable' AS utax,'Local Sales' as stype from jdt1 where  Account='40001030000' and (TransType=18 or TransType=19) " & vbCrLf _
        ' & "	union all " & vbCrLf _
        ' & " 	select t0.transid,t0.TransType, t0.refdate,t0.account,t0.contraact,rn.DocEntry,t0.baseref,t0.Credit,t0.Debit,cr.state,rn.taxcode U_taxcode , case when rn.TaxCode is null or LEFT(rn.taxcode,1)='N' then 'Non Taxable' else 'Taxable' end as utax, case when cr.state='TN' then 'Local Sales' else 'InterState Sales' end stype " & vbCrLf _
        ' & "	from jdt1 t0 " & vbCrLf _
        ' & "	left join (select cardcode,state,AdresType from CRD1 group by CardCode,State,AdresType) cr on cr.CardCode=t0.contraact and cr.AdresType='B' " & vbCrLf _
        ' & "	left join(select b.docentry,case when  PATINDEX('%[0-9]%',b1.taxcode)>0 then  substring(b1.taxcode,1,PATINDEX('%[0-9]%',b1.taxcode)-1) else b1.taxcode end taxcode,b.ObjType,b.CANCELED from Orin b " & vbCrLf _
        ' & "		left join rin1 b1 on b1.DocEntry =b.DocEntry  " & vbCrLf _
        ' & "		where b.CANCELED='N' and b1.ItemCode not in ('FrightCharges')  AND LEN(LTRIM(RTRIM(B1.TAXCODE)))>0 " & vbCrLf _
        ' & "		group by b.docentry,case when  PATINDEX('%[0-9]%',b1.taxcode)>0 then  substring(b1.taxcode,1,PATINDEX('%[0-9]%',b1.taxcode)-1) else b1.taxcode end,b.ObjType,b.CANCELED) rn on rn.DocEntry=t0.createdby and  rn.ObjType= t0.TransType " & vbCrLf _
        ' & "	where t0.account='40001030000' and TransType=14) k where DateDiff(dd,k.RefDate,@frmdat) > 0  " & vbCrLf _
        ' & "	group by k.Account,k.utax,k.stype) lk " & vbCrLf _
        ' & "	group by lk.taccount,lk.account " & vbCrLf

        msql3 = msql3 & "select lk.taccount as acctcode,SUM(lk.ob) ob,SUM(lk.debit) debit,SUM(lk.credit) credit,SUM(lk.curbal) curbal from " & vbCrLf _
            & "(select b.Account,CASE when c.state IS null OR c.state='TN' then '400010300011'" & vbCrLf _
                  & "when c.state IS not null OR c.state not in('TN') then '400010300021'  " & vbCrLf _
                  & "else '400010300011' end taccount, " & vbCrLf _
                  & "CASE when c.state IS null OR c.state='TN' then 'LOCAL SALES' " & vbCrLf _
                  & "when c.state IS not null OR c.state not in('TN') then 'INTERSTATE SALES'  " & vbCrLf _
                  & "else 'LOCAL SALES 1'  " & vbCrLf _
                  & "end stype,0 ob, SUM(b.debit) debit,SUM(b.credit) credit,  SUM(b.Debit-b.Credit) curbal from jdt1 b " & vbCrLf _
           & "left join (select cardcode,state from CRD1 where AdresType='B' group by cardcode,state) c on c.cardcode=b.contraact " & vbCrLf _
            & " where b.Account= '40001030000' and  b.RefDate>=@frmdat and b.RefDate<=@todat  and transtype not in (-3)" & vbCrLf _
            & " group by b.account,CASE when c.state is null OR c.state ='TN' then 'LOCAL SALES'  " & vbCrLf _
                  & " when c.state IS not null OR c.state not in('TN') then 'INTERSTATE SALES' " & vbCrLf _
                  & " else 'LOCAL SALES 1' end,CASE when c.state IS null OR c.state='TN' then '400010300011' " & vbCrLf _
                  & " when c.state IS not null OR c.state not in('TN') then '400010300021' " & vbCrLf _
                  & " else '400010300011' end  ) lk group by lk.taccount,lk.account" & vbCrLf



        msql3 = msql3 & " --gp" & vbCrLf _
        & "union all" & vbCrLf _
 & "select '50030000000' as acctcode,0 as ob,0 as debit,0 as credit,SUM(kk.balance) as curbal from " & vbCrLf _
 & "(select OC.acctcode,oc.acctname,isnull(l.ob,0) ob,isnull(l.debit,0) debit,isnull(l.credit,0) credit,isnull(l.curbal,0) curperiod,(isnull(l.ob,0)+ isnull(l.curbal,0)) as balance from OACT oc " & vbCrLf _
       & " Left Join" & vbCrLf _
 & "(select k.account,SUM(k.ob) ob,SUM(k.debit) debit,SUM(k.credit) credit,SUM(k.curbal) curbal from " & vbCrLf _
 & "(select Account, sum(debit-credit) ob,0 as debit,0 as credit,0 as curbal from jdt1 where DateDiff(dd, refdate,@frmdat)>0  and transtype not in ('-3') group by account " & vbCrLf _
        & "union all" & vbCrLf _
 & "select Account, 0 as ob,sum(debit) debit,SUM(credit) credit, sum(debit-credit) curbal from jdt1 where RefDate>=@frmdat and RefDate<=@todat  and transtype not in ('-3') group by account) k " & vbCrLf _
 & "group by k.account" & vbCrLf _
    & " union all" & vbCrLf _
 & "select k.acctcode,sum(k.ob) ob,sum(k.debit) debit,sum(k.credit) credit,sum(k.curbal) curbal from " & vbCrLf _
   & "(SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit)*-1 as curbal from jdt1 where Account in (select acctcode from oact where FatherNum='15000000000') and refDATE<=@todat) as  Curbal  FROM OACT where AcctCode='40004000000') k  group by k.acctcode " & vbCrLf _
   & " UNION ALL " & vbCrLf _
   & "select k.acctcode,0 ob,0 debit,0 credit,sum(k.curbal) curbal from	 " & vbCrLf _
 & "(SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit) as curbal from jdt1 where Account in (select acctcode from oact where FatherNum='15000000000')  and  refDATE<@frmdat) as  Curbal  FROM OACT where AcctCode='50011000000') k " & vbCrLf _
 & " group by k.acctcode	) l on l.Account=oc.AcctCode " & vbCrLf _
 & "where   oc.acctcode NOT IN ('50070015000','50070001000','40001070000','50070000000','50070009000','50070005000','50070013000','50070017000','50070003000','50070004000','40002020000','40002030000','40002040000','40002050000','40002060000','40002070000') " & vbCrLf _
 & " and CONVERT(float,oc.acctcode)>=convert(float,(select min(CONVERT(float,AcctCode)) dd from OACT where GroupMask=4))  and CONVERT(float,oc.acctcode) <convert(float,(select acctcode from OACT where AcctName='INDIRECT EXPENSES')))kk ) l on l.Account=oc.AcctCode" & vbCrLf _
 & "where oc.GroupMask>=4  and oc.acctcode NOT IN ('50070015000','50070001000','40001070000','50070000000','50070009000','50070005000','50070013000','50070017000','50070017000','50070003000','50070004000')) A " & vbCrLf _
 & "GROUP BY A.AcctCode,A.AcctName,a.Levels,A.GrpLine,A.GroupMask,A.FatherNum,A.Postable " & vbCrLf _
 & "--np" & vbCrLf _
    & " UNION ALL" & vbCrLf _
 & "select p.AcctCode,p.AcctName,c.OB,c.debit,c.Credit,C.curperiod,c.balance,  p.Levels,p.GrpLine,p.GroupMask,p.fathernum,p.Postable from CTB as c " & vbCrLf _
  & "inner join OACT as p on c.FatherNum=p.AcctCode " & vbCrLf _
 & "WHERE CONVERT(INTEGER,LEFT(P.ACCTCODE,1))>=4  AND P.AcctCode NOT IN ('50070015000','50070001000','40001070000','50070000000','50070009000','50070005000','50070013000','50070017000','50070003000','50070004000'))" & vbCrLf _
 & "INSERT INTO TMPPNL2 (SPCE,ACCTCODE,ACCTNAME,OB,DEBIT,CREDIT,CURRPERIOD,CB,GP,POSTABLE,LEVELS,compcode,datefr,dateto)" & vbCrLf _
& "SELECT LEN(rtrim(c.acctname))-len(rtrim(ltrim(c.ACCTNAME))) spcl,C.ACCTCODE,C.ACCTNAME,C.OB,C.DEBIT,C.CREDIT,C.curperiod*-1 as currperiod,c.CB*-1 as cb,CASE when c.GP>0 then c.GP else c.GP*-1 end gp , c.POSTABLE,C.LEVELS, " & vbCrLf _
& "(select CompnyName from OADM) compcode,@frmdat datefr,@todat dateto FROM" & vbCrLf _
& "(select R.acctcode, space((R.Levels-1)*4)+R.AcctName AcctName,SUM(r.OB) OB,SUM(r.Debit) Debit,SUM(r.Credit) Credit,sum(r.curperiod) curperiod, " & vbCrLf _
 & "CASE when r.AcctCode='500000000000000' then SUM(r.balance)-(select SUM(balance) from ctb where AcctCode IN(  '50030000000','50050000000'))  else sum(r.balance) end cb," & vbCrLf _
 & " R.Levels,R.GrpLine,	 R.Postable,convert(decimal(10,2),(SUM(r.balance) /@salper)*100)  AS GP from CTB R" & vbCrLf _
 & "WHERE R.LEVELS<= 10 and R.GroupMask IN ( 4,5) " & vbCrLf _
 & "group by R.acctcode,R.AcctName,R.Levels,R.GrpLine,R.groupmask,r.Postable" & vbCrLf _
    & "union all" & vbCrLf _
 & " select '60000100000' acctcode,case when ln.cb>0 then 'Net Profit' else 'Net Profit' end AcctName,0 OB,0 Debit,0 Credit,0 curperiod,ln.cb cb, 1 Levels,1 GrpLine,	 'Y' Postable," & vbCrLf _
 & "  convert(decimal(10,2),(ln.cb /@salper)*100) AS gp from " & vbCrLf _
 & " (select sum(n.balance) cb	 from CTB n " & vbCrLf _
   & "WHERE n.LEVELS<=10  and n.GroupMask IN ( 4,5) and n.Postable='Y' and AcctCode not in ('50030000000' ) )ln ) c " & vbCrLf _
 & "where Levels<=@level and cb<>0" & vbCrLf


        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        If Trim(msameserver) = "Y" Then
            msql2 = "delete from " & Trim(rdbnam) & ".dbo.tmppnl2 where compcode='" & Trim(curcompname) & "'"
            Dim CMD2 As New OleDbCommand(msql2, con)
            CMD2.ExecuteNonQuery()
            CMD2.Dispose()

            msql2 = "delete from tmppnl2 where compcode='" & Trim(curcompname) & "'"
            Dim CMD3 As New OleDbCommand(msql2, con)
            CMD3.ExecuteNonQuery()
            CMD3.Dispose()

            'msql = "delete from " & Trim(linkserver) & "." & Trim(rdbnam) & ".dbo.tmpbsheet2 where compcode='" & Trim(curcompname) & "'"
            Dim CMD1 As New OleDbCommand(msql3, con)
            CMD1.CommandTimeout = 600
            CMD1.ExecuteNonQuery()
            CMD1.Dispose()


            If msamedb = "N" Then
                msql = "insert into " & Trim(rdbnam) & ".dbo.tmppnl2 select * from tmppnl2"
                Dim CMD4 As New OleDbCommand(msql, con)
                CMD4.CommandTimeout = 600
                CMD4.ExecuteNonQuery()
                CMD4.Dispose()
            End If

        Else

            msql2 = "delete from " & Trim(linkserver) & "." & Trim(rdbnam) & ".dbo.tmppnl2 where compcode='" & Trim(curcompname) & "'"
            Dim CMD2 As New OleDbCommand(msql2, con)
            CMD2.ExecuteNonQuery()
            CMD2.Dispose()

            msql2 = "delete from tmppnl2 where compcode='" & Trim(curcompname) & "'"
            Dim CMD3 As New OleDbCommand(msql2, con)
            CMD3.ExecuteNonQuery()
            CMD3.Dispose()

            'msql = "delete from " & Trim(linkserver) & "." & Trim(rdbnam) & ".dbo.tmpbsheet2 where compcode='" & Trim(curcompname) & "'"
            Dim CMD1 As New OleDbCommand(msql3, con)
            CMD1.CommandTimeout = 600
            CMD1.ExecuteNonQuery()
            CMD1.Dispose()


            If Len(Trim(linkserver)) > 0 Then
                msql = "insert into " & Trim(linkserver) & "." & Trim(rdbnam) & ".dbo.tmppnl2 select * from tmppnl2"
                Dim CMD4 As New OleDbCommand(msql, con)
                CMD4.CommandTimeout = 300
                CMD4.ExecuteNonQuery()
                CMD4.Dispose()
            End If
        End If
    End Sub

    Private Sub rrpnl()
        msql = "DECLARE @frmdat DATETIME" & vbCrLf _
         & "DECLARE @todat DATETIME" & vbCrLf _
         & "DECLARE @PEC INT" & vbCrLf _
         & "declare @salper as float" & vbCrLf _
        & " SET @PEC   = '1'" & vbCrLf _
      & "SET @frmdat={?DateFrom}" & vbCrLf _
 & "SET @todat ={?DateTo};" & vbCrLf _
 & "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED ;" & vbCrLf _
 & " select @salper= SUM(credit-debit) from jdt1 where Account in (select acctcode from OACT where FatherNum= '40001000000' and AcctCode not in ('40001070000'))  and RefDate>=@frmdat and RefDate<=@todat;" & vbCrLf _
& "WITH CTB AS ( SELECT  A.AcctCode,A.AcctName,SUM(A.OB) OB,SUM(A.Debit) Debit,SUM(a.Credit) Credit,sum(a.curperiod) curperiod,sum(a.balance) balance, a.Levels,A.GrpLine,A.GroupMask,A.FatherNum,A.Postable   FROM " & vbCrLf _
 & " (select OC.acctcode,oc.acctname,isnull(l.ob,0) ob,isnull(l.debit,0) debit,isnull(l.credit,0) credit,isnull(l.curbal,0) curperiod,(isnull(l.ob,0)+ isnull(l.curbal,0)) as balance,oc.Levels, oc.GrpLine, oc.GroupMask, oc.FatherNum, oc.Postable from OACT oc" & vbCrLf _
 & "Left Join" & vbCrLf _
    & "(select k.account,SUM(k.ob) ob,SUM(k.debit) debit,SUM(k.credit) credit,SUM(k.curbal) curbal from " & vbCrLf _
   & "(select Account, 0 as ob,sum(debit) debit,SUM(credit) credit, sum(debit-credit) curbal from jdt1 where RefDate>=@frmdat and RefDate<=@todat and Account not in ('40001030000') group by account) k group by k.account" & vbCrLf _
   & " union all" & vbCrLf _
    & "SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit)*-1 as curbal from jdt1 where Account='15010000000'  and refDATE<=@todat) as  Curbal FROM OACT where AcctCode='40004000000'" & vbCrLf _
   & "UNION ALL" & vbCrLf _
    & "SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit) as curbal from jdt1 where Account='15010000000' and  refDATE<@frmdat) as  Curbal FROM OACT where AcctCode='50011000000'" & vbCrLf _
    & " --sales" & vbCrLf _
    & "union all" & vbCrLf _
    & "select lk.taccount as acctcode,SUM(lk.ob) ob,SUM(lk.debit) debit,SUM(lk.credit) credit,SUM(lk.curbal) curbal from     " & vbCrLf _
 & "(select case when l.utax='Taxable' and l.stype='InterState Sales' then '400010300021'" & vbCrLf _
                & "when l.utax='Non Taxable' and l.stype='InterState Sales' then  '400010300022'" & vbCrLf _
                & "when l.utax='Non Taxable' and l.stype='Local Sales' then   '400010300012'" & vbCrLf _
                & "when l.utax='Taxable' and l.stype='Local Sales' then    '400010300011' end as taccount," & vbCrLf _
         & "l.account,0 as ob,isnull(sum(l.debit),0) debit,isnull(sum(l.credit),0) credit, isnull(sum(l.debit-l.credit),0) as curbal,l.utax, l.stype from" & vbCrLf _
     & "(select  '40001030000' as account, case when k.utax IS null then 'Non Taxable' else k.utax end as utax,case when k.stype is null then 'Local Sales' else k.stype end as stype,0 as ob,SUM(k.dr) as debit,SUM(k.cr) as credit, SUM(k.bal) bal from " & vbCrLf _
        & "(select  t0.transtype,t0.account,t0.contraact,t0.ref1,t0.CreatedBy, isnull(SUM(t0.debit),0) dr,isnull(SUM(t0.credit),0) cr,SUM(isnull(t0.debit,0)-isnull(credit,0)) as bal,d.utax,d.stype from jdt1 t0  with (nolock) " & vbCrLf _
        & " left join (select o.DocEntry, o.CardCode,o.cardname,o.u_taxcode,cr.state," & vbCrLf _
         & "CASE  when o.U_TaxCode is null or LEFT(o.u_taxcode,1)='N' then 'Non Taxable' else 'Taxable' end as utax,case when cr.state='TN' then 'Local Sales' else 'InterState Sales' end stype from oinv o with (nolock)" & vbCrLf _
         & "left join CRD1 cr  with (nolock) on cr.CardCode=o.CardCode and cr.AdresType='B'" & vbCrLf _
         & "where o.DocDate>=@frmdat and o.DocDate<=@todat   group by o.CardCode,o.u_taxcode,o.cardname,cr.State,o.docentry) d on d.CardCode=t0.ContraAct and d.DocEntry=t0.CreatedBy" & vbCrLf _
         & "where RefDate>=@frmdat  and RefDate<=@todat  and account='40001030000' group by t0.TransType,t0.Account,t0.ref1,t0.CreatedBy ,t0.contraact,d.utax,d.stype) k" & vbCrLf _
         & "group by k.utax,k.stype) l" & vbCrLf _
& "group by case when l.utax='Taxable' and l.stype='InterState Sales' then '400010300021'" & vbCrLf _
                & "when l.utax='Non Taxable' and l.stype='InterState Sales' then  '400010300022'" & vbCrLf _
                & "when l.utax='Non Taxable' and l.stype='Local Sales' then   '400010300012'" & vbCrLf _
                & "when l.utax='Taxable' and l.stype='Local Sales' then    '400010300011' end ," & vbCrLf _
         & "l.account,l.utax, l.stype)  lk group by lk.taccount,lk.account" & vbCrLf _
 & "--gp" & vbCrLf _
       & " union all" & vbCrLf _
    & "select '50030000000' as acctcode,0 as ob,0 as debit,0 as credit,SUM(kk.balance) as curbal from" & vbCrLf _
    & "(select OC.acctcode,oc.acctname,isnull(l.ob,0) ob,isnull(l.debit,0) debit,isnull(l.credit,0) credit,isnull(l.curbal,0) curperiod,(isnull(l.ob,0)+ isnull(l.curbal,0)) as balance from OACT oc " & vbCrLf _
        & "Left Join" & vbCrLf _
    & "(select k.account,SUM(k.ob) ob,SUM(k.debit) debit,SUM(k.credit) credit,SUM(k.curbal) curbal from " & vbCrLf _
    & "(select Account, 0 as ob,sum(debit) debit,SUM(credit) credit, sum(debit-credit) curbal from jdt1 where RefDate>=@frmdat and RefDate<=@todat  group by account) k group by k.account" & vbCrLf _
    & "union all" & vbCrLf _
    & "SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit)*-1 as curbal from jdt1 where Account='15010000000'  and refDATE<=@todat) as  Curbal FROM OACT where AcctCode='40004000000' " & vbCrLf _
    & "UNION ALL" & vbCrLf _
    & "--SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit) as curbal from jdt1 where Account='15010000000' and transtype in ('-2','310000001')  and refDATE<=@frmdat) as  Curbal" & vbCrLf _
    & "--FROM OACT where AcctCode='50011000000'" & vbCrLf _
    & "SELECT  ACCTCODE,0 as OB,0 Debit,0 as credit,(select  SUM(Debit-Credit) as curbal from jdt1 where Account='15010000000' and  refDATE<@frmdat) as  Curbal FROM OACT where AcctCode='50011000000') l on l.Account=oc.AcctCode" & vbCrLf _
    & "where   oc.acctcode NOT IN ('50070015000','50070001000','40001070000','50070000000','50070009000','50070005000','50070004000')" & vbCrLf _
    & "and CONVERT(float,oc.acctcode)>=convert(float,(select min(CONVERT(float,AcctCode)) dd from OACT where GroupMask=4))  and CONVERT(float,oc.acctcode) <convert(float,(select acctcode from OACT where AcctName='INDIRECT EXPENSES')))kk ) l on l.Account=oc.AcctCode" & vbCrLf _
    & "where oc.GroupMask>=4  and oc.acctcode NOT IN ('50070015000','50070001000','40001070000','50070000000','50070009000','50070005000','50070004000')) A " & vbCrLf _
    & "GROUP BY A.AcctCode,A.AcctName,a.Levels,A.GrpLine,A.GroupMask,A.FatherNum,A.Postable " & vbCrLf _
    & "--np" & vbCrLf _
        & "UNION ALL" & vbCrLf _
        & "select p.AcctCode,p.AcctName,c.OB,c.debit,c.Credit,C.curperiod,c.balance,  p.Levels,p.GrpLine,p.GroupMask,p.fathernum,p.Postable from CTB as c" & vbCrLf _
    & "inner join OACT as p on c.FatherNum=p.AcctCode " & vbCrLf _
    & "WHERE CONVERT(INTEGER,LEFT(P.ACCTCODE,1))>=4  AND P.AcctCode NOT IN ('50070015000','50070001000','40001070000','50070000000','50070009000','50070005000','50070004000'))" & vbCrLf _
& "SELECT C.ACCTCODE,C.ACCTNAME,C.OB,C.DEBIT,C.CREDIT,C.curperiod*-1 as currperiod,c.CB*-1 as cb,CASE when c.GP>0 then c.GP else c.GP*-1 end gp , c.POSTABLE,C.LEVELS FROM " & vbCrLf _
& "(select R.acctcode, space((R.Levels-1)*4)+R.AcctName AcctName,SUM(r.OB) OB,SUM(r.Debit) Debit,SUM(r.Credit) Credit,sum(r.curperiod) curperiod,sum(r.balance) cb," & vbCrLf _
    & "R.Levels,R.GrpLine,     R.Postable,convert(decimal(10,2),(SUM(r.balance) /@salper)*100)  AS GP from CTB R " & vbCrLf _
    & "WHERE R.LEVELS<= 10 and R.GroupMask IN ( 4,5) group by R.acctcode,R.AcctName,R.Levels,R.GrpLine,R.groupmask,r.Postable" & vbCrLf _
    & "union all" & vbCrLf _
     & "select '' acctcode,case when ln.cb>0 then 'Net Profit' else 'Net Profit' end AcctName,0 OB,0 Debit,0 Credit,0 curperiod,ln.cb cb,1 Levels,1 GrpLine,     'Y' Postable,convert(decimal(10,2),(ln.cb /@salper)*100) AS gp from" & vbCrLf _
     & "(select sum(n.balance) cb     from CTB n" & vbCrLf _
      & "WHERE n.LEVELS<=10 and n.GroupMask IN ( 4,5) and n.Postable='Y' and AcctCode not in ('50030000000' ) )ln ) c" & vbCrLf

    End Sub

    Private Sub chktable()
        msql = "IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'tmppnl2')" & vbcrlf _
             & "begin " & vbcrlf _
             & " CREATE TABLE [dbo].[tmppnl2]( " & vbcrlf _
   & "[spce] [int] NULL," & vbcrlf _
   & "[ACCTCODE] [nvarchar](15) NULL," & vbcrlf _
   & " [ACCTNAME] [nvarchar](4000) NULL," & vbcrlf _
   & "[OB] [int] NULL," & vbcrlf _
   & "[DEBIT] [numeric](38, 6) NULL," & vbcrlf _
   & "[CREDIT] [numeric](38, 6) NULL," & vbcrlf _
   & "[currperiod] [numeric](38, 6) NULL," & vbcrlf _
   & "[cb] [numeric](38, 6) NULL," & vbcrlf _
   & "[gp] [decimal](12, 2) NULL," & vbcrlf _
   & "[POSTABLE] [varchar](1) NULL," & vbcrlf _
   & "[LEVELS] [int] NULL," & vbcrlf _
   & "[compcode] [nvarchar](100) NULL," & vbcrlf _
   & "[datefr] [datetime] NULL," & vbcrlf _
   & "[dateto] [datetime] NULL" & vbcrlf _
      & ") ON [PRIMARY]" & vbcrlf _
      & "end " & vbcrlf


        msql2 = "IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'tmpbsheet2')" & vbCrLf _
               & "begin " & vbCrLf _
               & "CREATE TABLE [dbo].[tmpbsheet2](" & vbCrLf _
         & "[acctcode] [nvarchar](15) NOT NULL," & vbCrLf _
     & "[Liabilities] [nvarchar](4000) NOT NULL," & vbCrLf _
     & "[cb] [numeric](38, 6) NULL," & vbCrLf _
     & "[postable] [varchar](1) NOT NULL," & vbCrLf _
     & "[levels] [int] NOT NULL," & vbCrLf _
     & "[zacctcode] [nvarchar](15) NOT NULL," & vbCrLf _
     & "[Assets] [nvarchar](4000) NOT NULL," & vbCrLf _
     & "[zcb] [numeric](38, 6) NOT NULL," & vbCrLf _
     & "[zpostable] [char](1) NOT NULL," & vbCrLf _
     & "[zlevels] [smallint] NOT NULL," & vbCrLf _
     & "[COMPCODE] [nvarchar](100) NULL," & vbCrLf _
     & "[DATEFR] [datetime] NULL," & vbCrLf _
     & "[DATETO] [datetime] NULL" & vbCrLf _
       & ") ON [PRIMARY]" & vbCrLf _
        & "end" & vbCrLf

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim CMD1 As New OleDbCommand(msql, con)
        cMD1.ExecuteNonQuery()
        CMD1.Dispose()

        Dim CMD2 As New OleDbCommand(msql2, con)
        CMD2.ExecuteNonQuery()
        CMD2.Dispose()
    End Sub


    Private Sub txtsql_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsql.TextChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        lsv.Columns.Clear()
        lsv.Items.Clear()
        ' Get SQL Query from textbox
        Dim sql As String = Trim(txtsql.Text)

        ' Create Command object
        Dim NewCommand As New OleDbCommand(sql, con)

        Try
            ' Open Connection
            'con.Open()
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            ' Execute Command
            'NewCommand.ExecuteNonQuery()

            Dim NewReader As OleDbDataReader = NewCommand.ExecuteReader()

            ' Get column names for list view from data reader
            For i As Integer = 0 To NewReader.FieldCount - 1
                Dim header As New ColumnHeader
                header.Text = NewReader.GetName(i)
                lsv.Columns.Add(header)
            Next

            ' Get rows of data and show in list view
            While NewReader.Read()
                ' Create list view item
                Dim NewItem As New ListViewItem

                ' Specify text and subitems of list view
                NewItem.Text = NewReader.GetValue(0).ToString()
                For i As Integer = 1 To NewReader.FieldCount - 1
                    NewItem.SubItems.Add(NewReader.GetValue(i).ToString())
                Next

                ' Add item to list view items collection
                lsv.Items.Add(NewItem)
            End While

            ' Close data reader
            NewReader.Close()





            ' Display Result Message
            txtresult.Text = "SQL executed successfuly"

        Catch ex As Exception
            ' Display error message
            txtresult.Text = ex.ToString()

        Finally
            'con.Close()

        End Try
    End Sub


    '    --profit and loss account
    'select acctcode,acctname,SUM(ob) ob,SUM(debit) debit,SUM(credit) credit,SUM(currperiod) currperiod,SUM(cb) cb,(sum(cb)/(select sum(cb) from tmppnl where ACCTCODE='40001000000'))*100 per, postable,levels from tmppnl
    '	group by ACCTCODE,ACCTNAME,POSTABLE,levels
    'order by acctcode,levels


    '****************balance sheet
    'select zacctcode,assets,zcb,zpostable,zlevels ,acctcode,lia,cb,post, levl from 
    '(select  ROW_NUMBER() OVER(ORDER BY j.zACCTCODE) AS ROWNUM,j.zacctcode,j.assets,j.zcb,j.zpostable,j.zlevels from
    '(select K.grpmask,k.ptype,k.zacctcode,k.assets,SUM(k.zcb) zcb,K.zpostable,k.zlevels from 
    '(select LEFT(zacctcode,1) as grpmask,'Assets' ptype, zacctcode,assets,zcb,zpostable,zlevels from tmpbsheet2
    ' union all
    ' select LEFT(acctcode,1) as grpmask,'Liabilities' ptype,acctcode,liabilities,cb,postable,levels from tmpbsheet2 where cb<>0) k
    ' group by K.grpmask,k.ptype,k.zacctcode,k.assets,K.zpostable,k.zlevels) j
    ' where j.ptype='Liabilities') l full join


    ' (select  ROW_NUMBER() OVER(ORDER BY j.zACCTCODE) AS ROWNUM,j.zacctcode acctcode,j.assets lia,j.zcb cb,j.zpostable post,j.zlevels levl from
    '(select K.grpmask,k.ptype,k.zacctcode,k.assets,SUM(k.zcb) zcb,K.zpostable,k.zlevels from 
    '(select LEFT(zacctcode,1) as grpmask,'Assets' ptype, zacctcode,assets,zcb,zpostable,zlevels from tmpbsheet2
    ' union all
    ' select LEFT(acctcode,1) as grpmask,'Liabilities' ptype,acctcode,liabilities,cb,postable,levels from tmpbsheet2 where cb<>0) k
    ' group by K.grpmask,k.ptype,k.zacctcode,k.assets,K.zpostable,k.zlevels) j
    ' where j.ptype='Assets') j on j.ROWNUM=l.ROWNUM

End Class
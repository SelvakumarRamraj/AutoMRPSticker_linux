Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Xml.Linq


Public Class Frmchart
    Dim msql As String
    Private Sub Frmchart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub loaddata()
        '' msql = "select lk.[lineno],lk.empno,lk.empname,lk.jobgrade," & vbCrLf _
        ''                 & " [1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31], " & vbCrLf _
        ''                & "(case when [1]>0 then [1] else 0 end + " & vbCrLf _
        ''                & "case when [2]>0 then [2] else 0 end +  " & vbCrLf _
        ''                & " case when [3]>0 then [3] else 0 end +  " & vbCrLf _
        ''                & " case when [4]>0 then [4] else 0 end +  " & vbCrLf _
        ''                & " case when [5]>0 then [5] else 0 end +  " & vbCrLf _
        ''                & " case when [6]>0 then [6] else 0 end +case when [7]>0 then [7] else 0 end +case when [8]>0 then [8] else 0 end +  " & vbCrLf _
        ''                & " case when [9]>0 then [9] else 0 end + case when [10]>0 then [10] else 0 end +case when [11]>0 then [11] else 0 end +case when [12]>0 then [12] else 0 end +  " & vbCrLf _
        ''                & " case when [13]>0 then [13] else 0 end +case when [14]>0 then [14] else 0 end +case when [15]>0 then [15] else 0 end +case when [16]>0 then [16] else 0 end +  " & vbCrLf _
        ''                & " case when [17]>0 then [17] else 0 end + case when [18]>0 then [18] else 0 end + case when [19]>0 then [19] else 0 end + case when [20]>0 then [20] else 0 end +  " & vbCrLf _
        ''                & " case when [21]>0 then [21] else 0 end + case when [22]>0 then [22] else 0 end + case when [23]>0 then [23] else 0 end + case when [24]>0 then [24] else 0 end + case when [25]>0 then [25] else 0 end +  " & vbCrLf _
        ''                & " case when [26]>0 then [26] else 0 end + case when [27]>0 then [27] else 0 end + case when [28]>0 then [28] else 0 end + case when [29]>0 then [29] else 0 end +  " & vbCrLf _
        ''                & " case when [30]>0 then [30] else 0 end + case when [31]>0 then [31] else 0 end ) as total from " & vbCrLf




        '' msql = msql & "(select 1 sno,l.[Lineno],'' empno,''empname,l.ptype jobgrade, sum(l.[1]) [1],sum(l.[2]) [2] ,sum(l.[3]) [3],sum(l.[4]) [4],sum(l.[5]) [5],sum(l.[6]) [6],sum(l.[7]) [7], " & vbCrLf _
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
        '' msql = msql & " union all select 2 sno, [lineno],empno,empname,jobgrade, " & vbCrLf _
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
        '' msql = msql & "union all select [lineno],case when jobgrade='I' then amt else 0 end I,case when jobgrade='II' then amt else 0 end II " & vbCrLf
        ''        &"case when jobgrade='III' then amt else 0 end III,case when jobgrade='I' then amt else 0 end I 
        '' jobgra("")

        '' msql = msql & "(select s.[Lineno],s.empno,s.empname,s.jobgrade,SUM(s.amt) amt, s.dat from " & vbCrLf _
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
        ''         & "where incyn not in ('N'))  c on c.bno=b.bno and c.[lineno]=b.[lineno]  left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) d on d.brand=b.brand  " & vbCrLf _
        ''         & "left join " & Trim(mcostdbnam) & ".dbo.empmaster em with (nolock) on em.nemp_id=c.empid  " & vbCrLf _
        ''         & "Left Join" & vbCrLf _
        ''         & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b with (nolock) " & vbCrLf _
        ''         & " Left Join " & vbCrLf _
        ''         & "(select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
        ''         & "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade " & vbCrLf _
        ''         & "where    date>='" & Format(CDate(Frmincentivecalc.mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Frmincentivecalc.Mskdateto.Text), "yyyy-MM-dd") & "'   and (b.totprodqty-(b.nomac*d.nopcs)) > 0  ) s " & vbCrLf _
        ''       & "group by s.[Lineno],s.empno,s.empname,s.jobgrade,s.dat ) k " & vbCrLf

        '' msql = msql & "  PIVOT (SUM(amt) FOR [dat] IN " & vbCrLf _
        ''   & "([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) P  ) lk " & vbCrLf _
        ''   & " order by  lk.[lineno],lk.sno,lk.jobgrade,lk.empno"



        'msql = msql & "select s.[Lineno],s.jobgrade,SUM(s.amt) amt from " & vbCrLf _
        '               & "(select b.date,DATEPART(d,b.date) dat, b.brand,b.processname,b.[lineno], c.empid,c.empno, em.empname, em.jobgrade, b.totprodqty,b.totrejqty,b.nomac, isnull((b.nomac*d.nopcs),0) Target, COUNT(c.empid) over(partition by b.date,b.[lineno]) nomanpwr," & vbCrLf _
        '               & "isnull((b.totprodqty-(b.nomac*d.nopcs)),0) as incqty, round((isnull(f.incentive,0)*c.pper)/100,2)  incentive, " & vbCrLf _
        '               & "isnull(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))* round((f.incentive*c.pper)/100,2)),0) else 0 end,0) amt,isnull(em.salary,0) salary," & vbCrLf _
        '               & "isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0) dsalary," & vbCrLf _
        '               & "(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))) noday, " & vbCrLf _
        '               & "SUM(case when (b.totprodqty-(b.nomac*d.nopcs)) > 0 then round(((b.totprodqty-(b.nomac*d.nopcs))*f.incentive),0) else 0 end) over (partition by b.[lineno]) as totincentive, " & vbCrLf _
        '               & "sum(isnull(round(em.salary/(datediff(day, dateadd(day, 1-day(b.date), b.date),dateadd(month, 1, dateadd(day, 1-day(b.date), b.date)))),0),0)) over (partition by b.[lineno]) as totsalary " & vbCrLf _
        '               & "from " & Trim(mcostdbnam) & ".dbo.operf b with (nolock)" & vbCrLf _
        '               & "left join (select l.bno,l.empid,l.empno,l.jobgrade,l.[lineno],l.pper from  " & vbCrLf _
        '               & "(select bno, empid,empno,jobgrade,[lineno],opername,totprod,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade) mprod ,COUNT(empno) over(partition by bno,[lineno],jobgrade,empno) cnt,  " & vbCrLf _
        '               & "case when totprod>0 and COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)<=1 then round((convert(real,totprod)/convert(real,MAX(totprod) over (partition by bno,[lineno],opername,jobgrade)))*100,0)  " & vbCrLf _
        '               & "else case  when COUNT(empno) over(partition by bno,[lineno],jobgrade,empno)>1 then 100 else  0 end end pper,incyn    from " & Trim(mcostdbnam) & ".dbo.perf1 with (nolock)) l  " & vbCrLf _
        '               & "where incyn not in ('N'))  c on c.bno=b.bno and c.[lineno]=b.[lineno]  left join (select brand,nopcs from " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nopcs) d on d.brand=b.brand  " & vbCrLf _
        '               & "left join " & Trim(mcostdbnam) & ".dbo.empmaster em with (nolock) on em.nemp_id=c.empid  " & vbCrLf _
        '               & "Left Join" & vbCrLf _
        '               & "(select b.brand,b.nprodpcsfr,b.nprodpcsto, b.grade,b.nopcs,b.incentive,c.posrnk from " & Trim(mcostdbnam) & ".dbo.incentivemaster b with (nolock) " & vbCrLf _
        '               & " Left Join " & vbCrLf _
        '               & "(select k.brand, k.nprodpcsfr,k.nprodpcsto,rank() over(partition by k.brand order by k.nprodpcsto) posrnk from " & vbCrLf _
        '               & "(select brand, nprodpcsfr,nprodpcsto from  " & Trim(mcostdbnam) & ".dbo.incentivemaster with (nolock) group by brand,nprodpcsfr,nprodpcsto) k) c on c.brand=b.brand and c.nprodpcsto=b.nprodpcsto) f on f.brand=b.brand and f.grade=em.jobgrade " & vbCrLf _
        '               & "where    date>='" & Format(CDate(Frmincentivecalc.mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Format(CDate(Frmincentivecalc.Mskdateto.Text), "yyyy-MM-dd") & "'   and (b.totprodqty-(b.nomac*d.nopcs)) > 0  ) s " & vbCrLf _
        '             & "group by s.[Lineno],s.jobgrade order by s.[Lineno],s.jobgrade "

        'Dim dt As DataTable = getDataTable(msql)
        ''Get the DISTINCT Countries.
        'Dim linenos As List(Of String) = (From p In dt.AsEnumerable() Select p.Field(Of String)("Lineno")).Distinct().ToList()

        ''Remove the Default Series.
        'If Chart1.Series.Count() = 1 Then
        '    Chart1.Series.Remove(Chart1.Series(0))
        'End If

        ''Loop through the Countries.
        'For Each lineno As String In linenos

        '    'Get the Year for each Country.
        '    Dim x As Integer() = (From p In dt.AsEnumerable() _
        '                          Where p.Field(Of String)("Lineno") = country _
        '                          Order By p.Field(Of String)("jobgrade") _
        '                          Select p.Field(Of String)("jobgrade")).ToArray()

        '    'Get the Total of Orders for each Country.
        '    Dim y As Integer() = (From p In dt.AsEnumerable() _
        '                          Where p.Field(Of String)("lienno") = country _
        '                          Order By p.Field(Of String)("jobgrade") _
        '                          Select p.Field(Of double)("amt")).ToArray()

        '    'Add Series to the Chart.
        '    Chart1.Series.Add(New Series(lineno))
        '    Chart1.Series(country).IsValueShownAsLabel = True
        '    Chart1.Series(country).BorderWidth = 3
        '    Chart1.Series(country).ChartType = SeriesChartType.Line
        '    Chart1.Series(country).Points.DataBindXY(x, y)
        'Next

        'Chart1.Legends(0).Enabled = True
    End Sub
End Class
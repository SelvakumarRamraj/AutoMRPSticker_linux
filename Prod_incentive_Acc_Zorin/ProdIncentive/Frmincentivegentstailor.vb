Imports System.Data
Imports System.Data.OleDb
Imports CarlosAg.ExcelXmlWriter
Imports System.IO
Imports System.Data.SqlClient

Public Class Frmincentivegentstailor
    Dim i, j, msel, o_id, n, k As Integer
    Dim MSQL, qry, dqry, dqry1, dqry2, merr, qry1 As String
    Private Sub Frmincentivegentstailor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width + 5
        Me.Height = My.Computer.Screen.Bounds.Height - 20
        'dg.Width = My.Computer.Screen.Bounds.Width - 20
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick

    End Sub
    Private Sub loaddata()
        If Len(Trim(txtempno.Text)) > 0 Then
            MSQL = "select  cdate as date,nempno,vname,cdepartment,rate,sum(qty) qty, sum(amt) amt from Prodcost.dbo.dailycontractdata with (nolock)  where cdate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and cdate<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' and nempno=" & Val(txtempno.Text) & "  group by cdate,nempno,vname,cdepartment,rate order by cdate"
        Else
            'MSQL = "select  nempno,vname,sum(qty) qty,sum(amt) amt,CASE when SUM(qty)>0 then sum(qty)/count(cdate) else 0 end avgqty from Prodcost.dbo.dailycontractdata with (nolock) where cdate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and cdate<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' group by nempno,vname order by nempno"
            'MSQL = "select  b.nempno,b.vname,sum(b.qty) qty,sum(b.amt) amt,CASE when SUM(b.qty)>0 then sum(b.qty)/count(b.cdate) else 0 end avgqty,c.totsalary from Prodcost.dbo.dailycontractdata b with (nolock) " & vbCrLf _
            '      & " left join rrcolor.dbo.empmaster c on c.nempno=b.nempno " & vbCrLf _
            '      & " where b.cdate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and cdate<='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' group by b.nempno,b.vname,c.totsalary order by b.nempno "

            'MSQL = " declare @dt as nvarchar(20) " & vbCrLf _
            '      & " declare @dt2 as nvarchar(20) " & vbCrLf _
            '      & " set @dt='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' " & vbCrLf _
            '      & " set @dt2='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' " & vbCrLf _
            '      & " select k.nempno,k.vname,k.cdepartment,k.rate,k.qty,k.amt,k.avgqty,round(((k.wrkday-k.wksund)+k.numofsundays)* (k.totsalary/k.monday),0) daysal, k.totsalary,k.wrkday, k.wksund,k.numofsundays from ( " & vbCrLf _
            '      & " select  b.nempno,b.vname,b.cdepartment,b.rate,sum(b.qty) qty,sum(b.amt) amt,CASE when SUM(b.qty)>0 then sum(b.qty)/count(b.cdate) else 0 end avgqty, " & vbCrLf _
            '      & " d.totsalary, sum(c.att)  wrkday, datediff(day, @dt, dateadd(month, 1, @dt)) monday,SUM(c.wksund) wksund ,DATEDIFF(ww, CAST(@dt AS datetime)-1, @dt2) AS NumOfSundays  from Prodcost.dbo.dailycontractdata b with (nolock) " & vbCrLf _
            '      & " left join rrcolor.dbo.empmaster d on d.nempno=b.nempno " & vbCrLf _
            '      & " left join (select nempno,dot,case when att=0.5 and DATENAME(DW,dot)<>'Sunday' then 0 else CASE when DATENAME(DW,dot)='Sunday' and att=0.5 then 1 else att end end att, case when DATENAME(DW,dot)='Sunday' then COUNT(DATENAME(DW,dot)) else 0 end wksund,daysalary,totsalary  from rrcolor.dbo.empdailysalary group by nempno,dot,daysalary,totsalary,att) c on c.nempno=b.nempno and c.dot=b.cdate " & vbCrLf _
            '      & " where b.cdate>=@dt and cdate<=@dt2 group by b.nempno,b.vname,d.totsalaryb.cdepartment,b.rate, ) k  order by k.nempno "


            MSQL = " declare @dt as nvarchar(20) " & vbCrLf _
                 & " declare @dt2 as nvarchar(20) " & vbCrLf _
                 & " set @dt='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' " & vbCrLf _
                 & " set @dt2='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "' " & vbCrLf _
                 & " select k.nempno,k.vname,k.cdepartment,k.rate,k.qty,k.amt,k.avgqty from ( " & vbCrLf _
                 & " select  b.nempno,b.vname,b.cdepartment,b.rate,sum(b.qty) qty,sum(b.amt) amt,CASE when SUM(b.qty)>0 then sum(b.qty)/count(b.cdate) else 0 end avgqty " & vbCrLf _
                 & "  from Prodcost.dbo.dailycontractdata b with (nolock) " & vbCrLf _
                 & " left join rrcolor.dbo.empmaster d on d.nempno=b.nempno " & vbCrLf _
                 & " left join (select nempno,dot,case when att=0.5 and DATENAME(DW,dot)<>'Sunday' then 0 else CASE when DATENAME(DW,dot)='Sunday' and att=0.5 then 1 else att end end att, case when DATENAME(DW,dot)='Sunday' then COUNT(DATENAME(DW,dot)) else 0 end wksund,daysalary,totsalary  from rrcolor.dbo.empdailysalary group by nempno,dot,daysalary,totsalary,att) c on c.nempno=b.nempno and c.dot=b.cdate " & vbCrLf _
                 & " where b.cdate>=@dt and cdate<=@dt2 group by b.nempno,b.vname,b.cdepartment,b.rate " & vbCrLf _
                 & " union all " & vbCrLf _
                 & " select 19000000 nempno,'Total' vname,'Total' cdepartment,0 rate,0 qty, sum(amt) amt,0 avgqty from prodcost.dbo.dailycontractdata where cdate>=@dt and cdate<=@dt2 " & vbCrLf _
                 & " ) k  order by k.nempno,k.cdepartment "


        End If

        Dim dt As DataTable = getDataTable(MSQL)
        If dt.Rows.Count > 0 Then
            dg.DataSource = dt
            dg.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.Maroon
            dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'Color.Maroon
            For i As Integer = 0 To dg.Columns.Count - 1

                dg.Columns(i).HeaderCell.Style.Font = New Font("Lao UI", 9, FontStyle.Bold)

                ' scp.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                'sco.Columns(i).Frozen = True
                'If i < 6 Then
                '    scp.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
                'End If
                If dt.Columns(i).DataType.Name.ToString() = "Int32" Or dt.Columns(i).DataType.Name.ToString() = "Decimal" Then
                    'If i > 5 And i < 17 Then
                    ' dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
                    dg.Columns(i).Width = 80
                    dg.Columns(i).ValueType = GetType(Decimal)
                    dg.Columns(i).DefaultCellStyle.Format = ("0")
                    dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
                    dg.Columns(i).Width = 80
                    'sco.Columns(i).HeaderCell.Style.WrapMode = DataGridViewTriState.True
                End If

                If dt.Columns(i).DataType.Name.ToString() = "Double" Then
                    'If i > 5 And i < 17 Then
                    ' dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
                    dg.Columns(i).Width = 90
                    dg.Columns(i).ValueType = GetType(Decimal)
                    dg.Columns(i).DefaultCellStyle.Format = ("0.00")
                    dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
                    dg.Columns(i).Width = 90
                    'sco.Columns(i).HeaderCell.Style.WrapMode = DataGridViewTriState.True
                End If
                'sco.Columns(i).Width = 50
                dg.Columns(i).ReadOnly = True
            Next

            dg.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single
        Else
            dg.DataSource = Nothing
        End If

        dg.Rows(dg.Rows.Count - 1).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
        dg.Rows(dg.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Yellow
    End Sub

    Private Sub butexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butexit.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        dg.DataSource = Nothing
        Call loaddata()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        gridexcelexport4(dg, 1, "GensTailorIncentive", "Gens Tailor Incentive as from " & Format(CDate(mskdatefr.Text), "dd-MM-yyyy") & " to " & Format(CDate(Mskdateto.Text), "dd-MM-yyyy"))

    End Sub

    Private Sub loadcompare()
        Cursor = Cursors.WaitCursor

        '**old
        'MSQL = " declare @d1 as nvarchar(20)" & vbCrLf _
        '       & " declare @d2 as nvarchar(20) " & vbCrLf _
        '       & " set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'" & vbCrLf _
        '       & " set @d2='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'" & vbCrLf _
        '       & " select k.cdate,k.empcnt,k.qty,k.amt,round(k.rate,2) rate, isnull(d.stqty,0) stqty, isnull(e.stsal,0) stsal,  " & vbCrLf _
        '       & " case when isnull(e.stsal,0)>0 and isnull(d.stqty,0)>0 then  round((isnull(e.stsal,0)/isnull(d.stqty,0)),2) else 0 end IH_St_rate,  isnull(o.ironqty,0) ironqty, ISNULL(g.ironsal,0) ironsal," & vbCrLf _
        '       & " case when ISNULL(g.ironsal,0)>0 and isnull(o.ironqty,0)>0 then round((ISNULL(g.ironsal,0)/isnull(o.ironqty,0)),2) else 0 end IH_IRON_Rate,isnull(s.desqty,0) desqty " & vbCrLf _
        '       & " from ( " & vbCrLf _
        '       & " select cdate,COUNT(nempno) empcnt, SUM(Qty) qty ,SUM(amt) amt, CASE when SUM(amt)>0 then  SUM(amt)/SUM(qty) else 0 end rate from Prodcost.dbo.dailycontractdata with (nolock) " & vbCrLf _
        '       & " where cdate>=@d1 and cdate<=@d2  group by cdate ) k " & vbCrLf _
        '       & " left join (select b.U_DocDate,SUM(c.U_RecdQty) stqty from [@INM_OPDE] b with (nolock) " & vbCrLf _
        '       & " left join [@INM_PDE1] c with (nolock) on c.DocEntry=b.DocEntry " & vbCrLf _
        '       & " left join oitm t with (nolock) on t.itemcode=c.u_itemcode " & vbCrLf _
        '       & " where b.u_docdate>=@d1 and b.u_docdate<=@d2  and b.u_type='RG' and b.u_docstatus='R' and b.canceled not in ('Y') " & vbCrLf _
        '       & " and t.ItmsGrpCod not in (170,100,102,105) and b.U_DFOperCode in ('STGD','KAJAGD') group by b.u_docdate) d on d.U_DocDate=k.cdate  " & vbCrLf _
        '       & "  left join (select b.U_DocDate,SUM(c.U_AccpQty) ironqty from [@INM_Owip] b with (nolock) " & vbCrLf _
        '       & "               left join [@INM_wip1] c with (nolock) on c.DocEntry=b.DocEntry " & vbCrLf _
        '       & "               left join oitm t with (nolock) on t.itemcode=c.u_itemcode " & vbCrLf _
        '       & "               where b.u_docdate>=@d1 and b.u_docdate<=@d2  and b.u_trantype='RG'  and b.canceled not in ('Y')  " & vbCrLf _
        '       & "               and t.ItmsGrpCod not in (170,100,102,105) and  b.U_OperCode='IRONGD' and  c.U_AccptWhs='GFINISH' and LEFT(c.u_cutno,1) not in ('V') " & vbCrLf _
        '       & "               group by b.u_docdate) o on o.U_DocDate =k.cdate " & vbCrLf _
        '       & "  left join (select j.u_docdate,SUM(j.desqty) desqty from (    " & vbCrLf _
        '       & "              select c.U_DocDate,SUM(U_Qty) desqty  from [@INS_ICC1] b with (nolock) " & vbCrLf _
        '       & "              left join [@INS_OICC] c with (nolock) on c.DocEntry=b.DocEntry " & vbCrLf _
        '       & "              left join oitm t with (nolock) on t.ItemCode=U_ToItemCd " & vbCrLf _
        '       & "              where c.U_DocDate>=@d1 and c.U_DocDate<=@d2 and U_ProcessType='IH' and U_Approved='Y' and t.ItmsGrpCod not in (170,100,102,105) group by c.u_docdate " & vbCrLf _
        '       & "              union all " & vbCrLf _
        '       & "              select c.docdate,SUM(b.Quantity) desqty from inv1 b with (nolock) " & vbCrLf _
        '       & "              left join oinv c with (nolock) on c.docentry=b.docentry " & vbCrLf _
        '       & "              where  c.DocDate>=@d1 and c.DocDate<=@d2 and c.CANCELED not in ('Y') and c.U_Team in ('IH')  " & vbCrLf _
        '       & "              group by c.docdate) j group by j.u_docdate) s on s.U_DocDate=k.cdate  " & vbCrLf

        'MSQL = MSQL & "  left join (select dot, COUNT(distinct nempno) cntemp, round((SUM(daysalary)+(100000/26)),2) stsal from rrcolor.dbo.empdailysalary where dot>='2021-03-01' and dot<='2021-03-31' and csno in (13,14,39)" & vbCrLf _
        '            & "              group by dot) e on e.dot=k.cdate " & vbCrLf _
        '            & "   left join (select dot, COUNT(distinct nempno) cntemp, round(SUM(daysalary),2) ironsal from rrcolor.dbo.empdailysalary where dot>='2021-03-01' and dot<='2021-03-31' and csno in (15) " & vbCrLf _
        '            & "               group by dot) g on g.dot=k.cdate " & vbCrLf _
        '            & "order by k.cdate "

        '**new
        'MSQL = " declare @d1 as nvarchar(20)" & vbCrLf _
        '       & " declare @d2 as nvarchar(20) " & vbCrLf _
        '       & " set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'" & vbCrLf _
        '       & " set @d2='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'" & vbCrLf _
        '       & " select k.cdate,k.empcnt,k.qty,k.amt,round(k.rate,2) rate, isnull(d.stqty,0) stqty, isnull(e.stsal,0) stsal,  " & vbCrLf _
        '       & " case when isnull(e.stsal,0)>0 and isnull(d.stqty,0)>0 then  round((isnull(e.stsal,0)/isnull(d.stqty,0)),2) else 0 end IH_St_rate,  isnull(o.ironqty,0) ironqty, ISNULL(g.ironsal,0) ironsal," & vbCrLf _
        '       & " case when ISNULL(g.ironsal,0)>0 and isnull(o.ironqty,0)>0 then round((ISNULL(g.ironsal,0)/isnull(o.ironqty,0)),2) else 0 end IH_IRON_Rate,isnull(s.desqty,0) desqty " & vbCrLf _
        '       & " from ( " & vbCrLf _
        '       & " select cdate,COUNT(nempno) empcnt, SUM(Qty) qty ,SUM(amt) amt, CASE when SUM(amt)>0 then  SUM(amt)/SUM(qty) else 0 end rate from Prodcost.dbo.dailycontractdata with (nolock) " & vbCrLf _
        '       & " where cdate>=@d1 and cdate<=@d2  group by cdate ) k " & vbCrLf _
        '       & " left join (select b.U_DocDate,SUM(c.U_RecdQty) stqty from [@INM_OPDE] b with (nolock) " & vbCrLf _
        '       & " left join [@INM_PDE1] c with (nolock) on c.DocEntry=b.DocEntry " & vbCrLf _
        '       & " left join oitm t with (nolock) on t.itemcode=c.u_itemcode " & vbCrLf _
        '       & " where b.u_docdate>=@d1 and b.u_docdate<=@d2  and b.u_type='RG' and b.u_docstatus='R' and b.canceled not in ('Y') " & vbCrLf _
        '       & " and t.ItmsGrpCod not in (170,100,102,105) and b.U_DFOperCode in ('STGD','KAJAGD') group by b.u_docdate) d on d.U_DocDate=k.cdate  " & vbCrLf _
        '       & "  left join (select b.U_DocDate,SUM(c.U_AccpQty) ironqty from [@INM_Owip] b with (nolock) " & vbCrLf _
        '       & "               left join [@INM_wip1] c with (nolock) on c.DocEntry=b.DocEntry " & vbCrLf _
        '       & "               left join oitm t with (nolock) on t.itemcode=c.u_itemcode " & vbCrLf _
        '       & "               where b.u_docdate>=@d1 and b.u_docdate<=@d2  and b.u_trantype='RG'  and b.canceled not in ('Y')  " & vbCrLf _
        '       & "               and t.ItmsGrpCod not in (170,100,102,105) and  b.U_OperCode='IRONGD' and  c.U_AccptWhs='GFINISH' and LEFT(c.u_cutno,1) not in ('V') " & vbCrLf _
        '       & "               group by b.u_docdate) o on o.U_DocDate =k.cdate " & vbCrLf _
        '       & "  left join (select j.u_docdate,SUM(j.desqty) desqty from (    " & vbCrLf _
        '       & "              select c.U_DocDate,SUM(U_Qty) desqty  from [@INS_ICC1] b with (nolock) " & vbCrLf _
        '       & "              left join [@INS_OICC] c with (nolock) on c.DocEntry=b.DocEntry " & vbCrLf _
        '       & "              left join oitm t with (nolock) on t.ItemCode=U_ToItemCd " & vbCrLf _
        '       & "              where c.U_DocDate>=@d1 and c.U_DocDate<=@d2 and U_ProcessType='IH' and U_Approved='Y' and t.ItmsGrpCod not in (170,100,102,105) group by c.u_docdate " & vbCrLf _
        '       & "              union all " & vbCrLf _
        '       & "              select c.docdate,SUM(b.Quantity) desqty from inv1 b with (nolock) " & vbCrLf _
        '       & "              left join oinv c with (nolock) on c.docentry=b.docentry " & vbCrLf _
        '       & "              where  c.DocDate>=@d1 and c.DocDate<=@d2 and c.CANCELED not in ('Y') and c.U_Team in ('IH')  " & vbCrLf _
        '       & "              group by c.docdate) j group by j.u_docdate) s on s.U_DocDate=k.cdate  " & vbCrLf

        'MSQL = MSQL & " left join (select b.dot, COUNT(distinct b.nempno) cntemp, round(SUM(b.daysalary),2) stsal from rrcolor.dbo.empdailysalary b " & vbCrLf _
        '            & "           left join rrcolor.dbo.empmaster c on c.nempno=b.nempno " & vbCrLf _
        '            & "            where b.dot>=@d1 and b.dot<=@d2 and c.subdept='STITCHING' group by b.dot) e on e.dot=k.cdate " & vbCrLf _
        '            & " left join (select b.dot, COUNT(distinct b.nempno) cntemp, round(SUM(b.daysalary),2) ironsal from rrcolor.dbo.empdailysalary b " & vbCrLf _
        '            & "            left join rrcolor.dbo.empmaster c on c.nempno=b.nempno " & vbCrLf _
        '            & "            where b.dot>=@d1 and b.dot<=@d2 and c.subdept='IRONING'  group by b.dot) g on g.dot=k.cdate " & vbCrLf _
        '            & "  order by k.cdate "


        '**Latest old
        'MSQL = " declare @d1 as nvarchar(20)" & vbCrLf _
        '      & " declare @d2 as nvarchar(20) " & vbCrLf _
        '      & " declare @lstdat as nvarchar(20) " & vbCrLf _
        '      & " set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'" & vbCrLf _
        '      & " set @d2='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'" & vbCrLf _
        '      & " set @lstdat= convert(date,(SELECT DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, @d1) + 1, 0))+1),103) " & vbCrLf _
        '      & " select kk.* from ( " & vbCrLf _
        '      & " select k.cdate,k.empcnt,k.qty,k.amt,round(k.rate,2) rate, isnull(d.stqty,0) stqty, isnull(d.stRWqty,0) stRWQty, isnull(d.stqty+d.strwqty,0) Tot_St_Qty, " & vbCrLf _
        '      & " isnull(e.stsal,0) stsal,case when isnull(e.stsal,0)>0 and isnull(d.stqty+d.strwqty,0)>0 then  round((isnull(e.stsal,0)/isnull(d.stqty+d.strwqty,0)),2) else 0 end IH_St_rate, " & vbCrLf _
        '      & " case when isnull(e.stsal+k.amt,0)>0 and isnull(d.stqty+d.strwqty,0)>0 then  round((isnull(e.stsal+k.amt,0)/isnull(d.stqty+d.strwqty,0)),2) else 0 end Tot_IH_St_rate," & vbCrLf _
        '      & " isnull(o.ironqty,0) ironqty,isnull(o.ironRwqty,0) IronRWqty, isnull(o.ironqty+o.ironrwqty,0) Tot_IronQty, ISNULL(g.ironsal,0) ironsal, " & vbCrLf _
        '      & " case when ISNULL(g.ironsal,0)>0 and isnull(o.ironqty+o.IronRWqty,0)>0 then round((ISNULL(g.ironsal,0)/isnull(o.ironqty+o.IronRWqty,0)),2) else 0 end IH_IRON_Rate,isnull(s.desqty,0) desqty,  " & vbCrLf _
        '      & " isnull(e.cntemp,0) st_emp_cnt,  isnull(g.cntemp,0) Iron_emp_cnt  from (  " & vbCrLf _
        '      & " select cdate,COUNT(nempno) empcnt, SUM(Qty) qty ,SUM(amt) amt, CASE when SUM(amt)>0 then  SUM(amt)/SUM(qty) else 0 end rate from Prodcost.dbo.dailycontractdata with (nolock)  " & vbCrLf _
        '      & " where cdate>=@d1 and cdate<=@d2  group by cdate ) k  " & vbCrLf _
        '      & " left join (select j.u_docdate,sum(j.stqty) Stqty,sum(j.strwqty) strwqty from ( " & vbCrLf _
        '      & "     select b.U_DocDate, isnull(case when b.u_type='RG' then  SUM(c.U_RecdQty) end,0) stqty,isnull(case when b.u_type='RW' then  SUM(c.U_RecdQty) end,0) strwqty from [@INM_OPDE] b with (nolock)  " & vbCrLf _
        '      & "     left join [@INM_PDE1] c with (nolock) on c.DocEntry=b.DocEntry  " & vbCrLf _
        '      & "     left join oitm t with (nolock) on t.itemcode=c.u_itemcode  " & vbCrLf _
        '      & "     where b.u_docdate>=@d1 and b.u_docdate<=@d2 and b.u_docstatus='R' and b.canceled not in ('Y')  " & vbCrLf _
        '      & "     and t.ItmsGrpCod not in (170,100,102,105) and b.U_DFOperCode in ('STGD','KAJAGD') group by b.u_docdate,b.u_type) j " & vbCrLf _
        '      & "    group by j.u_docdate)  d on d.U_DocDate=k.cdate   	   " & vbCrLf _
        '      & " left join (select j.u_docdate,sum(j.ironqty) ironqty,sum(j.ironrwqty) IronRWqty from ( " & vbCrLf _
        '      & "     select b.U_DocDate, case when b.u_trantype='RG' then  SUM(c.U_AccpQty) else 0 end ironqty," & vbCrLf _
        '      & "     case when b.u_trantype='RW' then  SUM(c.U_AccpQty) else 0 end ironRWqty from [@INM_Owip] b with (nolock)  " & vbCrLf _
        '      & "     left join [@INM_wip1] c with (nolock) on c.DocEntry=b.DocEntry  " & vbCrLf _
        '      & "     left join oitm t with (nolock) on t.itemcode=c.u_itemcode  " & vbCrLf _
        '      & "     where b.u_docdate>=@d1 and b.u_docdate<=@d2  and b.canceled not in ('Y')   " & vbCrLf _
        '      & "     and t.ItmsGrpCod not in (170,100,102,105) and  b.U_OperCode='IRONGD' and  c.U_AccptWhs='GFINISH' and LEFT(c.u_cutno,1) not in ('V')  " & vbCrLf _
        '      & "     group by b.u_docdate,b.u_trantype) j group by j.u_docdate)  o on o.U_DocDate =k.cdate" & vbCrLf _
        '      & " left join (select c.u_docdate, sum(b.u_qty) desqty from [@ins_bicc1] b " & vbCrLf _
        '      & "     inner join [@ins_obicc] c on c.docentry=b.docentry " & vbCrLf _
        '      & "     where c.u_docdate>=@d1 and c.u_docdate<=@d2 and c.u_fromwhs='GFINISH' " & vbCrLf _
        '      & "     and c.canceled='N' and c.u_stockp='C' group by c.u_docdate) s on s.U_DocDate=k.cdate  " & vbCrLf

        'If Chkot.Checked = True Then
        '    MSQL = MSQL & " left join (select b.dot, COUNT(distinct b.nempno) cntemp, round(SUM(b.daysalary),2) stsal from rrcolor.dbo.empdailysalary b " & vbCrLf _
        '          & "     left join rrcolor.dbo.empmaster c on c.nempno=b.nempno " & vbCrLf _
        '          & "     where b.dot>=@d1 and b.dot<=@d2 and c.subdept in ('STITCHING','PRODUCTION')  and c.subsection not in ('TAILOR(PIECE RATE)') " & vbCrLf _
        '          & "     group by b.dot) e on e.dot=k.cdate " & vbCrLf
        'Else

        '    MSQL = MSQL & " Left join (select b.dot, COUNT(distinct b.nempno) cntemp, round(SUM(b.daysalary)+isnull(ot.otsal,0),2) stsal from rrcolor.dbo.empdailysalary b " & vbCrLf _
        '                & "            left join rrcolor.dbo.empmaster c on c.nempno=b.nempno  " & vbCrLf _
        '                & "              left join (select j.attendancedate,sum(j.otsal) otsal from ( " & vbCrLf _
        '                & "            select k.AttendanceDate,k.empno,ep.PunchNo, isnull(sum(em.daysalary),0) daysalary, SUM(CASE WHEN isnull(k.othr,0) > 0 THEN k.othr  ELSE 0  END) AS othr, " & vbCrLf _
        '    & " case when SUM(CASE WHEN k.othr > 0 THEN k.othr  ELSE 0  END) >0 then (sum(em.daysalary)/9)*SUM(CASE WHEN k.othr > 0 THEN k.othr  ELSE 0  END) else 0 end otsal " & vbCrLf _
        '    & " from (select bb.AttendanceDate, bb.empno,bb.intime,bb.outtime, " & vbCrLf _
        '    & " case when bb.intime<>'1900-01-01 00:00:00.000' and bb.outtime<>'1900-01-01 00:00:00.000' then " & vbCrLf _
        '    & " (DATEDIFF(MINUTE, intime,outtime) / 60)-9 	else    0 	end othr  from ehr.dbo.Attendance bb " & vbCrLf _
        '    & " where  bb.attendancedate>=@d1 and bb.attendancedate<=@d2) k " & vbCrLf _
        '    & " inner join ehr.dbo.EmployeeDetails ep on ep.empno=k.EmpNo " & vbCrLf _
        '    & " left join rrcolor.dbo.empmaster em on em.nempno=ep.PunchNo " & vbCrLf _
        '    & " where  em.subdept in ('STITCHING','PRODUCTION')  and em.subsection not in ('TAILOR(PIECE RATE)') " & vbCrLf _
        '    & " group by k.empno,ep.PunchNo,k.AttendanceDate " & vbCrLf _
        '    & " having  SUM(CASE WHEN isnull(k.othr,0) > 0 THEN k.othr  ELSE 0  END)>0) j " & vbCrLf _
        '    & " group by j.AttendanceDate)  ot on ot.AttendanceDate=b.dot " & vbCrLf _
        '    & "  where b.dot>=@d1 and b.dot<=@d2 and c.subdept in ('STITCHING','PRODUCTION') and c.subsection not in ('TAILOR(PIECE RATE)') " & vbCrLf _
        '    & " group by b.dot,ot.otsal ) e on e.dot=k.cdate "
        'End If
        'If Chkot.Checked = True Then
        '    MSQL = MSQL & " left join (select b.dot, COUNT(distinct b.nempno) cntemp, round(SUM(b.daysalary),2) ironsal from rrcolor.dbo.empdailysalary b " & vbCrLf _
        '          & "     left join rrcolor.dbo.empmaster c on c.nempno=b.nempno  " & vbCrLf _
        '          & "     where b.dot>=@d1 and b.dot<=@d2 and c.subdept='IRONING' " & vbCrLf _
        '          & "     group by b.dot) g on g.dot=k.cdate  " & vbCrLf

        'Else

        '    MSQL = MSQL & " Left join (select b.dot, COUNT(distinct b.nempno) cntemp, round(SUM(b.daysalary)+isnull(ot.otsal,0),2) ironsal from rrcolor.dbo.empdailysalary b " & vbCrLf _
        '               & "            left join rrcolor.dbo.empmaster c on c.nempno=b.nempno  " & vbCrLf _
        '               & "              left join (select j.attendancedate,sum(j.otsal) otsal from ( " & vbCrLf _
        '               & "            select k.AttendanceDate,k.empno,ep.PunchNo, isnull(sum(em.daysalary),0) daysalary, SUM(CASE WHEN isnull(k.othr,0) > 0 THEN k.othr  ELSE 0  END) AS othr, " & vbCrLf _
        '   & " case when SUM(CASE WHEN k.othr > 0 THEN k.othr  ELSE 0  END) >0 then (sum(em.daysalary)/9)*SUM(CASE WHEN k.othr > 0 THEN k.othr  ELSE 0  END) else 0 end otsal " & vbCrLf _
        '   & " from (select bb.AttendanceDate, bb.empno,bb.intime,bb.outtime, " & vbCrLf _
        '   & " case when bb.intime<>'1900-01-01 00:00:00.000' and bb.outtime<>'1900-01-01 00:00:00.000' then " & vbCrLf _
        '   & " (DATEDIFF(MINUTE, intime,outtime) / 60)-9 	else    0 	end othr  from ehr.dbo.Attendance bb " & vbCrLf _
        '   & " where  bb.attendancedate>=@d1 and bb.attendancedate<=@d2) k " & vbCrLf _
        '   & " inner join ehr.dbo.EmployeeDetails ep on ep.empno=k.EmpNo " & vbCrLf _
        '   & " left join rrcolor.dbo.empmaster em on em.nempno=ep.PunchNo " & vbCrLf _
        '   & " where  em.subdept in ('IRONING')   " & vbCrLf _
        '   & " group by k.empno,ep.PunchNo,k.AttendanceDate " & vbCrLf _
        '   & " having  SUM(CASE WHEN isnull(k.othr,0) > 0 THEN k.othr  ELSE 0  END)>0) j " & vbCrLf _
        '   & " group by j.AttendanceDate)  ot on ot.AttendanceDate=b.dot " & vbCrLf _
        '   & "  where b.dot>=@d1 and b.dot<=@d2 and c.subdept in ('IRONING')  " & vbCrLf _
        '   & " group by b.dot,ot.otsal ) g on g.dot=k.cdate " & vbCrLf
        'End If



        'MSQL = MSQL & "  union all " & vbCrLf _
        '      & " select @lstdat cdate,0	empcnt,0 qty,0	amt,0	rate,0	stqty,0	stRWQty,0 Tot_St_Qty,0 stsal, 0 IH_St_rate,0 Tot_IH_St_rate,0 ironqty,0	IronRWqty,0	Tot_IronQty, " & vbCrLf _
        '      & " 0 ironsal,0	IH_IRON_Rate,0	desqty,0 st_emp_cnt, 0 Iron_emp_cnt ) kk " & vbCrLf _
        '      & " order by kk.cdate "


        'Latest
        MSQL = " declare @d1 as nvarchar(20)" & vbCrLf _
             & " declare @d2 as nvarchar(20) " & vbCrLf _
             & " declare @lstdat as nvarchar(20) " & vbCrLf _
             & " set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'" & vbCrLf _
             & " set @d2='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'" & vbCrLf _
             & " set @lstdat= convert(date,(SELECT DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, @d1) + 1, 0))+1),103) " & vbCrLf _
             & " select kk.* from ( " & vbCrLf _
             & " select k.cdate,k.empcnt,k.qty,k.amt,round(k.rate,2) rate, isnull(d.stqty,0) stqty, isnull(d.stRWqty,0) stRWQty, isnull(d.stqty+d.strwqty,0) Tot_St_Qty, " & vbCrLf _
             & " isnull(e.stsal,0) stsal,case when isnull(e.stsal,0)>0 and isnull(d.stqty+d.strwqty,0)>0 then  round((isnull(e.stsal,0)/isnull(d.stqty+d.strwqty,0)),2) else 0 end IH_St_rate, " & vbCrLf _
             & " case when isnull(e.stsal+k.amt,0)>0 and isnull(d.stqty+d.strwqty,0)>0 then  round((isnull(e.stsal+k.amt,0)/isnull(d.stqty+d.strwqty,0)),2) else 0 end Tot_IH_St_rate," & vbCrLf _
             & " isnull(o.ironqty,0) ironqty,isnull(o.ironRwqty,0) IronRWqty, isnull(o.ironqty+o.ironrwqty,0) Tot_IronQty, ISNULL(g.ironsal,0) ironsal, " & vbCrLf _
             & " case when ISNULL(g.ironsal,0)>0 and isnull(o.ironqty+o.IronRWqty,0)>0 then round((ISNULL(g.ironsal,0)/isnull(o.ironqty+o.IronRWqty,0)),2) else 0 end IH_IRON_Rate," & vbCrLf _
             & " isnull(irg.ironsalgen,0) IronsalGen, " & vbCrLf _
             & " case when ISNULL(irg.ironsalgen,0)>0 and isnull(o.ironqty+o.IronRWqty,0)>0 then round((ISNULL(irg.ironsalgen,0)/isnull(o.ironqty+o.IronRWqty,0)),2) else 0 end IRON_Gen_Rate," & vbCrLf _
             & " isnull(irds.ironsaldes,0) IronsalDesp , case when ISNULL(irds.ironsaldes,0)>0 and isnull(o.ironqty+o.IronRWqty,0)>0 then round((ISNULL(irds.ironsaldes,0)/isnull(o.ironqty+o.IronRWqty,0)),2) else 0 end IRON_Desp_Rate," & vbCrLf _
             & " isnull(s.desqty,0) desqty,  " & vbCrLf _
             & " isnull(e.cntemp,0) st_emp_cnt,  isnull(g.cntemp,0) Iron_emp_cnt  from (  " & vbCrLf _
             & " select cdate,COUNT(nempno) empcnt, SUM(Qty) qty ,SUM(amt) amt, CASE when SUM(amt)>0 then  SUM(amt)/SUM(qty) else 0 end rate from Prodcost.dbo.dailycontractdata with (nolock)  " & vbCrLf _
             & " where cdate>=@d1 and cdate<=@d2  group by cdate ) k  " & vbCrLf _
             & " left join (select j.u_docdate,sum(j.stqty) Stqty,sum(j.strwqty) strwqty from ( " & vbCrLf _
             & "     select b.U_DocDate, isnull(case when b.u_type='RG' then  SUM(c.U_RecdQty) end,0) stqty,isnull(case when b.u_type='RW' then  SUM(c.U_RecdQty) end,0) strwqty from [@INM_OPDE] b with (nolock)  " & vbCrLf _
             & "     left join [@INM_PDE1] c with (nolock) on c.DocEntry=b.DocEntry  " & vbCrLf _
             & "     left join oitm t with (nolock) on t.itemcode=c.u_itemcode  " & vbCrLf _
             & "     where b.u_docdate>=@d1 and b.u_docdate<=@d2 and b.u_docstatus='R' and b.canceled not in ('Y')  " & vbCrLf _
             & "     and t.ItmsGrpCod not in (170,100,102,105) and b.U_DFOperCode in ('STGD','KAJAGD') group by b.u_docdate,b.u_type) j " & vbCrLf _
             & "    group by j.u_docdate)  d on d.U_DocDate=k.cdate   	   " & vbCrLf _
             & " left join (select j.u_docdate,sum(j.ironqty) ironqty,sum(j.ironrwqty) IronRWqty from ( " & vbCrLf _
             & "     select b.U_DocDate, case when b.u_trantype='RG' then  SUM(c.U_AccpQty) else 0 end ironqty," & vbCrLf _
             & "     case when b.u_trantype='RW' then  SUM(c.U_AccpQty) else 0 end ironRWqty from [@INM_Owip] b with (nolock)  " & vbCrLf _
             & "     left join [@INM_wip1] c with (nolock) on c.DocEntry=b.DocEntry  " & vbCrLf _
             & "     left join oitm t with (nolock) on t.itemcode=c.u_itemcode  " & vbCrLf _
             & "     where b.u_docdate>=@d1 and b.u_docdate<=@d2  and b.canceled not in ('Y')   " & vbCrLf _
             & "     and t.ItmsGrpCod not in (170,100,102,105) and  b.U_OperCode='IRONGD' and  c.U_AccptWhs='GFINISH' and LEFT(c.u_cutno,1) not in ('V')  " & vbCrLf _
             & "     group by b.u_docdate,b.u_trantype) j group by j.u_docdate)  o on o.U_DocDate =k.cdate" & vbCrLf _
             & " left join (select c.u_docdate, sum(b.u_qty) desqty from [@ins_bicc1] b " & vbCrLf _
             & "     inner join [@ins_obicc] c on c.docentry=b.docentry " & vbCrLf _
             & "     where c.u_docdate>=@d1 and c.u_docdate<=@d2 and c.u_fromwhs='GFINISH' " & vbCrLf _
             & "     and c.canceled='N' and c.u_stockp='C' group by c.u_docdate) s on s.U_DocDate=k.cdate  " & vbCrLf

        If Chkot.Checked = True Then
            MSQL = MSQL & " left join (select b.dot, COUNT(distinct b.nempno) cntemp, round(SUM(b.daysalary),2) stsal from rrcolor.dbo.empdailysalary b " & vbCrLf _
                  & "     left join rrcolor.dbo.empmaster c on c.nempno=b.nempno " & vbCrLf _
                  & "     where b.dot>=@d1 and b.dot<=@d2 and c.subdept in ('STITCHING','PRODUCTION')  and c.subsection not in ('TAILOR(PIECE RATE)') " & vbCrLf _
                  & "     group by b.dot) e on e.dot=k.cdate " & vbCrLf
        Else

            MSQL = MSQL & " Left join (select b.dot, COUNT(distinct b.nempno) cntemp, round(SUM(b.daysalary)+isnull(ot.otsal,0),2) stsal from rrcolor.dbo.empdailysalary b " & vbCrLf _
                        & "            left join rrcolor.dbo.empmaster c on c.nempno=b.nempno  " & vbCrLf _
                        & "              left join (select j.attendancedate,sum(j.otsal) otsal from ( " & vbCrLf _
                        & "            select k.AttendanceDate,k.empno,ep.PunchNo, isnull(sum(em.daysalary),0) daysalary, SUM(CASE WHEN isnull(k.othr,0) > 0 THEN k.othr  ELSE 0  END) AS othr, " & vbCrLf _
            & " case when SUM(CASE WHEN k.othr > 0 THEN k.othr  ELSE 0  END) >0 then (sum(em.daysalary)/9)*SUM(CASE WHEN k.othr > 0 THEN k.othr  ELSE 0  END) else 0 end otsal " & vbCrLf _
            & " from (select bb.AttendanceDate, bb.empno,bb.intime,bb.outtime, " & vbCrLf _
            & " case when bb.intime<>'1900-01-01 00:00:00.000' and bb.outtime<>'1900-01-01 00:00:00.000' then " & vbCrLf _
            & " (DATEDIFF(MINUTE, intime,outtime) / 60)-9 	else    0 	end othr  from ehr.dbo.Attendance bb " & vbCrLf _
            & " where  bb.attendancedate>=@d1 and bb.attendancedate<=@d2) k " & vbCrLf _
            & " inner join ehr.dbo.EmployeeDetails ep on ep.empno=k.EmpNo " & vbCrLf _
            & " left join rrcolor.dbo.empmaster em on em.nempno=ep.PunchNo " & vbCrLf _
            & " where  em.subdept in ('STITCHING','PRODUCTION')  and em.subsection not in ('TAILOR(PIECE RATE)') " & vbCrLf _
            & " group by k.empno,ep.PunchNo,k.AttendanceDate " & vbCrLf _
            & " having  SUM(CASE WHEN isnull(k.othr,0) > 0 THEN k.othr  ELSE 0  END)>0) j " & vbCrLf _
            & " group by j.AttendanceDate)  ot on ot.AttendanceDate=b.dot " & vbCrLf _
            & "  where b.dot>=@d1 and b.dot<=@d2 and c.subdept in ('STITCHING','PRODUCTION') and c.subsection not in ('TAILOR(PIECE RATE)') " & vbCrLf _
            & " group by b.dot,ot.otsal ) e on e.dot=k.cdate "
        End If


        If Chkot.Checked = True Then

            MSQL = MSQL & "left join (select b.dot, COUNT(distinct b.nempno) cntemp, round(SUM(b.daysalary),2) ironsal from rrcolor.dbo.empdailysalary b  " & vbCrLf _
                        & "           left join rrcolor.dbo.empmaster c on c.nempno=b.nempno   " & vbCrLf _
                        & "           where b.dot>=@d1 and b.dot<=@d2 and c.subdept='IRONING'  and c.subsection  not in ('PRODUCTION','QUALITY','SCAVENGER','DESPATCH','HOUSE KEEPING','DATA ENTRY','DESPATCH-DATA ENTRY','IE','CHECKING') " & vbCrLf _
                        & " group by b.dot) g on g.dot=k.cdate  " & vbCrLf _
                        & " left join (select b.dot, COUNT(distinct b.nempno) cntemp, round(SUM(b.daysalary),2) ironsalgen from rrcolor.dbo.empdailysalary b   " & vbCrLf _
                        & "            left join rrcolor.dbo.empmaster c on c.nempno=b.nempno   " & vbCrLf _
                        & "           where b.dot>=@d1 and b.dot<=@d2 and c.subdept='IRONING' and c.subsection in ('PRODUCTION','SCAVENGER','HOUSE KEEPING','DATA ENTRY','IE','CHECKING')  " & vbCrLf _
                        & " group by b.dot) irg on irg.dot=k.cdate " & vbCrLf _
                        & "left join (select b.dot, COUNT(distinct b.nempno) cntemp, round(SUM(b.daysalary),2) ironsaldes from rrcolor.dbo.empdailysalary b  " & vbCrLf _
                        & "           left join rrcolor.dbo.empmaster c on c.nempno=b.nempno   " & vbCrLf _
                        & "           where b.dot>=@d1 and b.dot<=@d2 and c.subdept='IRONING' and c.subsection in ('DESPATCH','DESPATCH-DATA ENTRY','QUALITY') " & vbCrLf _
                        & "  group by b.dot) irds on irds.dot=k.cdate "


        Else

            MSQL = MSQL & "Left join (select b.dot, COUNT(distinct b.nempno) cntemp, round(SUM(b.daysalary)+isnull(ot.otsal,0),2) ironsal from rrcolor.dbo.empdailysalary b  " & vbCrLf _
                        & "            left join rrcolor.dbo.empmaster c on c.nempno=b.nempno  " & vbCrLf _
                        & "            left join (select j.attendancedate,sum(j.otsal) otsal from (  " & vbCrLf _
                        & "            select k.AttendanceDate,k.empno,ep.PunchNo, isnull(sum(em.daysalary),0) daysalary, SUM(CASE WHEN isnull(k.othr,0) > 0 THEN k.othr  ELSE 0  END) AS othr,  " & vbCrLf _
                        & "			case when SUM(CASE WHEN k.othr > 0 THEN k.othr  ELSE 0  END) >0 then (sum(em.daysalary)/9)*SUM(CASE WHEN k.othr > 0 THEN k.othr  ELSE 0  END) else 0 end otsal  " & vbCrLf _
                        & "			from (select bb.AttendanceDate, bb.empno,bb.intime,bb.outtime,  " & vbCrLf _
                        & "			case when bb.intime<>'1900-01-01 00:00:00.000' and bb.outtime<>'1900-01-01 00:00:00.000' then  " & vbCrLf _
                        & "			(DATEDIFF(MINUTE, intime,outtime) / 60)-9 	else    0 	end othr  from ehr.dbo.Attendance bb  " & vbCrLf _
                        & "			where  bb.attendancedate>=@d1 and bb.attendancedate<=@d2) k  " & vbCrLf _
                        & "			inner join ehr.dbo.EmployeeDetails ep on ep.empno=k.EmpNo  " & vbCrLf _
                        & "			left join rrcolor.dbo.empmaster em on em.nempno=ep.PunchNo  " & vbCrLf _
                        & "			where  em.subdept in ('IRONING') and  em.subsection  not in ('PRODUCTION','QUALITY','SCAVENGER','DESPATCH','HOUSE KEEPING','DATA ENTRY','DESPATCH-DATA ENTRY','IE','CHECKING') " & vbCrLf _
                        & "			group by k.empno,ep.PunchNo,k.AttendanceDate  " & vbCrLf _
                        & "			having  SUM(CASE WHEN isnull(k.othr,0) > 0 THEN k.othr  ELSE 0  END)>0) j  " & vbCrLf _
                        & "			group by j.AttendanceDate)  ot on ot.AttendanceDate=b.dot  " & vbCrLf _
                        & " where b.dot>=@d1 and b.dot<=@d2 and c.subdept in ('IRONING')  and  c.subsection  not in ('PRODUCTION','QUALITY','SCAVENGER','DESPATCH','HOUSE KEEPING','DATA ENTRY','DESPATCH-DATA ENTRY','IE','CHECKING') " & vbCrLf _
                        & " group by b.dot,ot.otsal ) g on g.dot=k.cdate " & vbCrLf _
                        & " Left join (select b.dot, COUNT(distinct b.nempno) cntemp, round(SUM(b.daysalary)+isnull(ot.otsal,0),2) ironsalgen from rrcolor.dbo.empdailysalary b  " & vbCrLf _
                        & "          left join rrcolor.dbo.empmaster c on c.nempno=b.nempno   " & vbCrLf _
                        & "          left join (select j.attendancedate,sum(j.otsal) otsal from (  " & vbCrLf _
                        & "           select k.AttendanceDate,k.empno,ep.PunchNo, isnull(sum(em.daysalary),0) daysalary, SUM(CASE WHEN isnull(k.othr,0) > 0 THEN k.othr  ELSE 0  END) AS othr,  " & vbCrLf _
                        & "			case when SUM(CASE WHEN k.othr > 0 THEN k.othr  ELSE 0  END) >0 then (sum(em.daysalary)/9)*SUM(CASE WHEN k.othr > 0 THEN k.othr  ELSE 0  END) else 0 end otsal  " & vbCrLf _
                        & "			from (select bb.AttendanceDate, bb.empno,bb.intime,bb.outtime,  " & vbCrLf _
                        & "			case when bb.intime<>'1900-01-01 00:00:00.000' and bb.outtime<>'1900-01-01 00:00:00.000' then  " & vbCrLf _
                        & "			(DATEDIFF(MINUTE, intime,outtime) / 60)-9 	else    0 	end othr  from ehr.dbo.Attendance bb  " & vbCrLf _
                        & "			where  bb.attendancedate>=@d1 and bb.attendancedate<=@d2) k  " & vbCrLf _
                        & "			inner join ehr.dbo.EmployeeDetails ep on ep.empno=k.EmpNo  " & vbCrLf _
                        & "			left join rrcolor.dbo.empmaster em on em.nempno=ep.PunchNo  " & vbCrLf _
                        & "			where  em.subdept in ('IRONING')   and  em.subsection in ('PRODUCTION','SCAVENGER','HOUSE KEEPING','DATA ENTRY','IE','CHECKING')  " & vbCrLf _
                        & "			group by k.empno,ep.PunchNo,k.AttendanceDate  " & vbCrLf _
                        & "			having  SUM(CASE WHEN isnull(k.othr,0) > 0 THEN k.othr  ELSE 0  END)>0) j  " & vbCrLf _
                        & "			group by j.AttendanceDate)  ot on ot.AttendanceDate=b.dot  " & vbCrLf _
                        & " where b.dot>=@d1 and b.dot<=@d2 and c.subdept in ('IRONING')  and  c.subsection in ('PRODUCTION','SCAVENGER','HOUSE KEEPING','DATA ENTRY','IE','CHECKING') " & vbCrLf _
                        & " group by b.dot,ot.otsal ) irg on irg.dot=k.cdate " & vbCrLf _
                        & " Left join (select b.dot, COUNT(distinct b.nempno) cntemp, round(SUM(b.daysalary)+isnull(ot.otsal,0),2) ironsaldes from rrcolor.dbo.empdailysalary b  " & vbCrLf _
                        & "           left join rrcolor.dbo.empmaster c on c.nempno=b.nempno   " & vbCrLf _
                        & "           left join (select j.attendancedate,sum(j.otsal) otsal from (  " & vbCrLf _
                        & "            select k.AttendanceDate,k.empno,ep.PunchNo, isnull(sum(em.daysalary),0) daysalary, SUM(CASE WHEN isnull(k.othr,0) > 0 THEN k.othr  ELSE 0  END) AS othr,  " & vbCrLf _
                        & "			case when SUM(CASE WHEN k.othr > 0 THEN k.othr  ELSE 0  END) >0 then (sum(em.daysalary)/9)*SUM(CASE WHEN k.othr > 0 THEN k.othr  ELSE 0  END) else 0 end otsal  " & vbCrLf _
                        & "			from (select bb.AttendanceDate, bb.empno,bb.intime,bb.outtime,  " & vbCrLf _
                        & "			case when bb.intime<>'1900-01-01 00:00:00.000' and bb.outtime<>'1900-01-01 00:00:00.000' then  " & vbCrLf _
                        & "			(DATEDIFF(MINUTE, intime,outtime) / 60)-9 	else    0 	end othr  from ehr.dbo.Attendance bb  " & vbCrLf _
                        & "			where  bb.attendancedate>=@d1 and bb.attendancedate<=@d2) k  " & vbCrLf _
                        & "			inner join ehr.dbo.EmployeeDetails ep on ep.empno=k.EmpNo  " & vbCrLf _
                        & "			left join rrcolor.dbo.empmaster em on em.nempno=ep.PunchNo  " & vbCrLf _
                        & "			where  em.subdept in ('IRONING') and em.subsection in ('DESPATCH','DESPATCH-DATA ENTRY','QUALITY') " & vbCrLf _
                        & "			group by k.empno,ep.PunchNo,k.AttendanceDate  " & vbCrLf _
                        & "			having  SUM(CASE WHEN isnull(k.othr,0) > 0 THEN k.othr  ELSE 0  END)>0) j  " & vbCrLf _
                        & "			group by j.AttendanceDate)  ot on ot.AttendanceDate=b.dot  " & vbCrLf _
                        & " where b.dot>=@d1 and b.dot<=@d2 and c.subdept in ('IRONING') and c.subsection in ('DESPATCH','DESPATCH-DATA ENTRY','QUALITY') " & vbCrLf _
                        & " group by b.dot,ot.otsal ) irds on irds.dot=k.cdate " & vbCrLf


        End If



        MSQL = MSQL & "  union all " & vbCrLf _
              & " select @lstdat cdate,0	empcnt,0 qty,0	amt,0	rate,0	stqty,0	stRWQty,0 Tot_St_Qty,0 stsal, 0 IH_St_rate,0 Tot_IH_St_rate,0 ironqty,0	IronRWqty,0	Tot_IronQty, " & vbCrLf _
              & " 0 ironsal,0	IH_IRON_Rate,0 ironsalgen,0 IRON_Gen_Rate,0 iornsaldesp, 0 IRON_Desp_Rate, 0	desqty,0 st_emp_cnt, 0 Iron_emp_cnt ) kk " & vbCrLf _
              & " order by kk.cdate "





        'othr

        'select  attendancedate,empno,intime,outtime,(DATEDIFF(MINUTE, intime,outtime) / 60)-9 othr from ehr.dbo.attendance where attendancedate='2025-02-17'
        'and ((DATEDIFF(MINUTE, intime,outtime) / 60)-9 )>0


        Dim dt As DataTable = getDataTable(MSQL)
        If dt.Rows.Count > 0 Then
            dg.DataSource = dt

            'scp.Columns(0).Width = 140
            'scp.Columns(1).Width = 150
            ''dgsr.Columns(2).Width = 175
            dg.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.Maroon
            dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'Color.Maroon
            For i As Integer = 0 To dg.Columns.Count - 1

                dg.Columns(i).HeaderCell.Style.Font = New Font("Lao UI", 9, FontStyle.Bold)

                ' scp.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                'sco.Columns(i).Frozen = True
                'If i < 6 Then
                '    scp.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
                'End If
                If dt.Columns(i).DataType.Name.ToString() = "Int32" Or dt.Columns(i).DataType.Name.ToString() = "Decimal" Then
                    'If i > 5 And i < 17 Then
                    ' dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
                    dg.Columns(i).Width = 80
                    dg.Columns(i).ValueType = GetType(Decimal)
                    dg.Columns(i).DefaultCellStyle.Format = ("0")
                    dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
                    dg.Columns(i).Width = 80
                    'sco.Columns(i).HeaderCell.Style.WrapMode = DataGridViewTriState.True
                End If

                If dt.Columns(i).DataType.Name.ToString() = "Double" Then
                    'If i > 5 And i < 17 Then
                    ' dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
                    dg.Columns(i).Width = 90
                    dg.Columns(i).ValueType = GetType(Decimal)
                    dg.Columns(i).DefaultCellStyle.Format = ("0.00")
                    dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
                    dg.Columns(i).Width = 90
                    'sco.Columns(i).HeaderCell.Style.WrapMode = DataGridViewTriState.True
                End If
                'sco.Columns(i).Width = 50
                dg.Columns(i).ReadOnly = True
            Next

            dg.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single

            'dg.Rows(dg.Rows.Count - 1).Cells(0).Value = "Total"
            dg.Rows(dg.Rows.Count - 1).Cells(1).Value = Format(Convert.ToDouble(dt.Compute("Avg(empcnt)", "")), "#####0")
            dg.Rows(dg.Rows.Count - 1).Cells(2).Value = Format(Convert.ToDouble(dt.Compute("Sum(qty)", "")), "##########0")
            dg.Rows(dg.Rows.Count - 1).Cells(3).Value = Format(Convert.ToDouble(dt.Compute("Sum(amt)", "")), "##############0.00")
            dg.Rows(dg.Rows.Count - 1).Cells(4).Value = Format(Convert.ToDouble(dt.Compute("Sum(amt)", "")) / Convert.ToDouble(dt.Compute("Sum(qty)", "")), "##0.00")
            dg.Rows(dg.Rows.Count - 1).Cells(5).Value = Format(Convert.ToDouble(dt.Compute("Sum(stqty)", "")), "##########0")
            dg.Rows(dg.Rows.Count - 1).Cells(6).Value = Format(Convert.ToDouble(dt.Compute("Sum(strwqty)", "")), "##########0")
            dg.Rows(dg.Rows.Count - 1).Cells(7).Value = Format(Convert.ToDouble(dt.Compute("Sum([Tot_St_Qty])", "")), "##########0")
            dg.Rows(dg.Rows.Count - 1).Cells(8).Value = Format(Convert.ToDouble(dt.Compute("Sum(stsal)", "")), "##############0.00")
            dg.Rows(dg.Rows.Count - 1).Cells(9).Value = Format(Convert.ToDouble(dt.Compute("Sum(stsal)", "")) / Convert.ToDouble(dt.Compute("Sum([Tot_St_Qty])", "")), "####0.00")
            dg.Rows(dg.Rows.Count - 1).Cells(10).Value = Format((Convert.ToDouble(dt.Compute("Sum(stsal)", "")) + Convert.ToDouble(dt.Compute("Sum(amt)", ""))) / Convert.ToDouble(dt.Compute("Sum([Tot_St_Qty])", "")), "#####0.00")
            dg.Rows(dg.Rows.Count - 1).Cells(11).Value = Format(Convert.ToDouble(dt.Compute("Sum(ironqty)", "")), "##########0")
            dg.Rows(dg.Rows.Count - 1).Cells(12).Value = Format(Convert.ToDouble(dt.Compute("Sum(ironrwqty)", "")), "##########0")
            dg.Rows(dg.Rows.Count - 1).Cells(13).Value = Format(Convert.ToDouble(dt.Compute("Sum([Tot_IronQty])", "")), "##########0")
            dg.Rows(dg.Rows.Count - 1).Cells(14).Value = Format(Convert.ToDouble(dt.Compute("Sum(ironsal)", "")), "##############0.00")
            dg.Rows(dg.Rows.Count - 1).Cells(15).Value = Format(Convert.ToDouble(dt.Compute("Sum(ironsal)", "")) / Convert.ToDouble(dt.Compute("Sum([Tot_IronQty])", "")), "####0.00")

            dg.Rows(dg.Rows.Count - 1).Cells(16).Value = Format(Convert.ToDouble(dt.Compute("Sum(ironsalgen)", "")), "##############0.00")
            dg.Rows(dg.Rows.Count - 1).Cells(17).Value = Format(Convert.ToDouble(dt.Compute("Sum(ironsalgen)", "")) / Convert.ToDouble(dt.Compute("Sum([Tot_IronQty])", "")), "####0.00")

            dg.Rows(dg.Rows.Count - 1).Cells(18).Value = Format(Convert.ToDouble(dt.Compute("Sum(ironsaldesp)", "")), "##############0.00")
            dg.Rows(dg.Rows.Count - 1).Cells(19).Value = Format(Convert.ToDouble(dt.Compute("Sum(ironsaldesp)", "")) / Convert.ToDouble(dt.Compute("Sum([Tot_IronQty])", "")), "####0.00")

            dg.Rows(dg.Rows.Count - 1).Cells(20).Value = Format(Convert.ToDouble(dt.Compute("Sum(desqty)", "")), "##########0")
            dg.Rows(dg.Rows.Count - 1).Cells(21).Value = Format(Convert.ToDouble(dt.Compute("Avg([st_emp_cnt])", "")), "#####0")
            dg.Rows(dg.Rows.Count - 1).Cells(22).Value = Format(Convert.ToDouble(dt.Compute("Avg([Iron_emp_cnt])", "")), "#####0")
            For j As Integer = 0 To dg.Columns.Count - 1
                dg.Rows(dg.Rows.Count - 1).Cells(j).Style.Font = New Font("Calibri", 9, FontStyle.Bold)
                dg.Rows(dg.Rows.Count - 1).Cells(j).Style.BackColor = Color.PeachPuff
            Next

        Else
            dg.DataSource = Nothing
        End If

        '      select jk.u_style,sum(jk.stqty) stqty,sum(jk.strwqty) strwqty,sum(jk.totqty) totqty from (
        '   select j.u_docdate,j.u_style, sum(j.stqty) Stqty,sum(j.strwqty) strwqty,sum(j.stqty+isnull(j.strwqty,0)) totqty from (
        'select b.U_DocDate,t.u_style, isnull(case when b.u_type='RG' then  SUM(c.U_RecdQty) end,0) stqty,isnull(case when b.u_type='RW' then  SUM(c.U_RecdQty) end,0) strwqty from [@INM_OPDE] b with (nolock)  
        '               left join [@INM_PDE1] c with (nolock) on c.DocEntry=b.DocEntry  
        '               left join oitm t with (nolock) on t.itemcode=c.u_itemcode  
        '               where b.u_docdate>='2025-02-01' and b.u_docdate<='2025-02-28' and b.u_docstatus='R' and b.canceled not in ('Y')  
        '               and t.ItmsGrpCod not in (170,100,102,105) and b.U_DFOperCode in ('STGD','KAJAGD') group by b.u_docdate,b.u_type,t.u_style) j group by j.u_docdate,j.u_style) jk group by jk.U_Style'

        Cursor = Cursors.Default
    End Sub

    Private Sub loadcomparecut()
        Cursor = Cursors.WaitCursor
        MSQL = "declare @d1 as nvarchar(20) " & vbCrLf _
               & " declare @d2 as nvarchar(20) " & vbCrLf _
               & " declare @lstdat as nvarchar(20) " & vbCrLf _
               & " set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'" & vbCrLf _
               & " set @d2='" & Format(CDate(Mskdateto.Text), "yyyy-MM-dd") & "'" & vbCrLf _
               & " set @lstdat= convert(date,(SELECT DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, @d1) + 1, 0))+1),103) " & vbCrLf _
               & " select  jk.dot,jk.cut_empcnt,jk.cutqty,jk.cutsal,jk.cut_rate,jk.Acc_empcnt,jk.accqty,jk.Accsal,jk.Acc_rate,jk.emb_empcnt,jk.embqty,jk.embsal,jk.emb_rate " & vbCrLf _
               & " from (select kk.dot,kk.cut_empcnt,kk.cutqty,kk.cutsal,case when kk.cutsal>0 and kk.cutqty>0 then kk.cutsal/kk.cutqty else 0 end cut_rate, " & vbCrLf _
               & " isnull(kk.Acc_empcnt,0) Acc_empcnt,isnull(kk.accqty,0) accqty,isnull(kk.Accsal,0) Accsal, " & vbCrLf _
               & " case when kk.accsal>0 and kk.accqty>0 then kk.accsal/kk.accqty else 0 end Acc_rate , " & vbCrLf _
               & " kk.emb_empcnt,kk.embqty,kk.embsal,case when kk.embsal>0 and kk.embqty>0 then kk.embsal/kk.embqty else 0 end emb_rate  from ( " & vbCrLf _
               & " select k.dot,k.cut_empcnt,isnull(ct.cutqty,0) cutqty, k.cutsal,f.acc_empcnt,ac.accqty,f.Accsal " & vbCrLf _
               & " ,isnull(g.emb_empcnt,0) emb_empcnt,isnull(em.Embqty,0) Embqty,isnull(g.Embsal,0) Embsal from " & vbCrLf _
               & " (select b.dot, COUNT(distinct b.nempno) cut_Empcnt, round(SUM(b.daysalary),2) Cutsal from rrcolor.dbo.empdailysalary b " & vbCrLf _
               & "  left join rrcolor.dbo.empmaster c on c.nempno=b.nempno where b.dot>=@d1 and b.dot<=@d2 and c.subdept='CUTTING' group by b.dot) k " & vbCrLf _
               & " left join (select b.dot, COUNT(distinct b.nempno) Emb_empcnt, round(SUM(b.daysalary),2) Embsal from rrcolor.dbo.empdailysalary b " & vbCrLf _
               & "            left join rrcolor.dbo.empmaster c on c.nempno=b.nempno " & vbCrLf _
               & "             where b.dot>=@d1 and b.dot<=@d2 and c.subdept='EMBROIDING' " & vbCrLf _
               & "              group by b.dot) g on g.dot=k.dot " & vbCrLf _
               & " left join (select b.dot, COUNT(distinct b.nempno) Acc_empcnt, round(SUM(b.daysalary),2) Accsal from rrcolor.dbo.empdailysalary b " & vbCrLf _
               & "             left join rrcolor.dbo.empmaster c on c.nempno=b.nempno " & vbCrLf _
               & "             where b.dot>=@d1 and b.dot<=@d2 and c.subdept='CUTTING COLCAN' " & vbCrLf _
               & "             group by b.dot) f on f.dot=k.dot  " & vbCrLf _
               & " left join (select b.U_DocDate,SUM(c.U_AccpQty) cutqty from [@INM_Owip] b with (nolock)  " & vbCrLf _
               & "                left join [@INM_wip1] c with (nolock) on c.DocEntry=b.DocEntry  " & vbCrLf _
               & "                left join oitm t with (nolock) on t.itemcode=c.u_itemcode  " & vbCrLf _
               & "                where b.u_docdate>=@d1 and b.u_docdate<=@d2  and b.u_trantype='RG'  and b.canceled not in ('Y')   " & vbCrLf _
               & "                and t.ItmsGrpCod not in (170,100,102,105) and  b.U_OperCode='CUTGD' --and  c.U_AccptWhs='Cut' " & vbCrLf _
               & "                group by b.u_docdate) ct on ct.U_DocDate=k.dot " & vbCrLf _
               & " left join (select b.U_DocDate,SUM(c.U_AccpQty) Embqty from [@INM_Owip] b with (nolock)  " & vbCrLf _
               & "                left join [@INM_wip1] c with (nolock) on c.DocEntry=b.DocEntry  " & vbCrLf _
               & "                left join oitm t with (nolock) on t.itemcode=c.u_itemcode  " & vbCrLf _
               & "                where b.u_docdate>=@d1 and b.u_docdate<=@d2  and b.u_trantype='RG'  and b.canceled not in ('Y')   " & vbCrLf _
               & "                and t.ItmsGrpCod not in (170,100,102,105) and  b.U_OperCode='EMBGD' " & vbCrLf _
               & "                group by b.u_docdate) em on em.U_DocDate=k.dot " & vbCrLf _
               & " left join (select  b.U_DocDate,SUM(c.U_AccpQty) accqty from [@INM_Owip] b with (nolock)  " & vbCrLf _
               & "                left join [@INM_wip1] c with (nolock) on c.DocEntry=b.DocEntry  " & vbCrLf _
               & "                left join oitm t with (nolock) on t.itemcode=c.u_itemcode  " & vbCrLf _
               & "                where b.u_docdate>=@d1 and b.u_docdate<=@d2  and b.u_trantype='RG'  and b.canceled not in ('Y')  " & vbCrLf _
               & "                and t.ItmsGrpCod  in (105,111,115) and  b.U_OperCode='CUTGD' and  c.U_AccptWhs='ACC' " & vbCrLf _
               & "                group by b.u_docdate) ac on ac.u_docdate=k.dot ) kk " & vbCrLf _
               & " union all " & vbCrLf _
               & " select @lstdat dot,0 cut_empcnt,0 cutqty,0 cutsal,0 cut_rate,0 Acc_empcnt,0 Accqty,0 Accsal,0 Acc_rate, 0 emb_empcnt,0 embqty,0 embsal,0 emb_rate) jk " & vbCrLf _
               & " order by jk.dot "


        Dim dt As DataTable = getDataTable(MSQL)
        If dt.Rows.Count > 0 Then
            dg.DataSource = dt
            dg.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.Maroon
            dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'Color.Maroon
            For i As Integer = 0 To dg.Columns.Count - 1

                dg.Columns(i).HeaderCell.Style.Font = New Font("Lao UI", 9, FontStyle.Bold)

                If dt.Columns(i).DataType.Name.ToString() = "Int32" Or dt.Columns(i).DataType.Name.ToString() = "Decimal" Then
                    'If i > 5 And i < 17 Then
                    ' dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
                    dg.Columns(i).Width = 80
                    dg.Columns(i).ValueType = GetType(Decimal)
                    dg.Columns(i).DefaultCellStyle.Format = ("0")
                    dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
                    dg.Columns(i).Width = 80
                    'sco.Columns(i).HeaderCell.Style.WrapMode = DataGridViewTriState.True
                End If

                If dt.Columns(i).DataType.Name.ToString() = "Double" Then
                    'If i > 5 And i < 17 Then
                    ' dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
                    dg.Columns(i).Width = 90
                    dg.Columns(i).ValueType = GetType(Decimal)
                    dg.Columns(i).DefaultCellStyle.Format = ("0.00")
                    dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
                    dg.Columns(i).Width = 90
                    'sco.Columns(i).HeaderCell.Style.WrapMode = DataGridViewTriState.True
                End If
                'sco.Columns(i).Width = 50
                dg.Columns(i).ReadOnly = True
            Next

            dg.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single

            'cut_empcnt, cutqty, cutsal, cut_rate, Acc_empcnt, accqty, Accsal, Acc_rate, emb_empcnt, embqty, embsal, emb_rate

            'dg.Rows(dg.Rows.Count - 1).Cells(0).Value = "Total"
            dg.Rows(dg.Rows.Count - 1).Cells(1).Value = Format(Convert.ToDouble(dt.Compute("Avg([cut_empcnt])", "")), "#####0")
            dg.Rows(dg.Rows.Count - 1).Cells(2).Value = Format(Convert.ToDouble(dt.Compute("Sum(cutqty)", "")), "##########0")
            dg.Rows(dg.Rows.Count - 1).Cells(3).Value = Format(Convert.ToDouble(dt.Compute("Sum(cutsal)", "")), "##############0.00")
            dg.Rows(dg.Rows.Count - 1).Cells(4).Value = Format(Convert.ToDouble(dt.Compute("Sum(cutsal)", "")) / Convert.ToDouble(dt.Compute("Sum(cutqty)", "")), "##0.00")
            dg.Rows(dg.Rows.Count - 1).Cells(5).Value = Format(Convert.ToDouble(dt.Compute("avg([Acc_empcnt])", "")), "##########0")
            dg.Rows(dg.Rows.Count - 1).Cells(6).Value = Format(Convert.ToDouble(dt.Compute("Sum(accqty)", "")), "##########0")
            dg.Rows(dg.Rows.Count - 1).Cells(7).Value = Format(Convert.ToDouble(dt.Compute("Sum(accsal)", "")), "##############0.00")
            dg.Rows(dg.Rows.Count - 1).Cells(8).Value = Format(Convert.ToDouble(dt.Compute("Sum(accsal)", "")) / Convert.ToDouble(dt.Compute("Sum(accqty)", "")), "##0.00")
            dg.Rows(dg.Rows.Count - 1).Cells(9).Value = Format(Convert.ToDouble(dt.Compute("avg([emb_empcnt])", "")), "##########0")
            dg.Rows(dg.Rows.Count - 1).Cells(10).Value = Format(Convert.ToDouble(dt.Compute("Sum(embqty)", "")), "##########0")
            dg.Rows(dg.Rows.Count - 1).Cells(11).Value = Format(Convert.ToDouble(dt.Compute("Sum(embsal)", "")), "##############0.00")
            dg.Rows(dg.Rows.Count - 1).Cells(12).Value = Format(Convert.ToDouble(dt.Compute("Sum(embsal)", "")) / Convert.ToDouble(dt.Compute("Sum(embqty)", "")), "##0.00")



            For j As Integer = 0 To dg.Columns.Count - 1
                dg.Rows(dg.Rows.Count - 1).Cells(j).Style.Font = New Font("Calibri", 9, FontStyle.Bold)
                dg.Rows(dg.Rows.Count - 1).Cells(j).Style.BackColor = Color.PeachPuff
            Next

        Else
            dg.DataSource = Nothing
        End If

        Cursor = Cursors.Default
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Chkcut.Checked = True Then
            Call loadcomparecut()
        Else
            Call loadcompare()
        End If
        'Call loadcompare()
    End Sub


    'cut_emb


    '    declare @d1 as nvarchar(20) 
    'declare @d2 as nvarchar(20)  
    'declare @lstdat as nvarchar(20)
    'set @d1='2025-02-01'
    'set @d2='2025-02-28'
    'set @lstdat= convert(date,(SELECT DATEADD(d, -1, DATEADD(m, DATEDIFF(m, 0, @d1) + 1, 0))+1),103)

    'select jk.* from (
    ' select kk.dot,kk.cut_empcnt,kk.cutqty,kk.cutsal,case when kk.cutsal>0 and kk.cutqty>0 then kk.cutsal/kk.cutqty else 0 end cut_rate,
    '  kk.emb_empcnt,kk.embqty,kk.embsal,case when kk.embsal>0 and kk.embqty>0 then kk.embsal/kk.embqty else 0 end emb_rate  from (
    ' select k.dot,k.cut_empcnt,isnull(ct.cutqty,0) cutqty, k.cutsal,isnull(g.emb_empcnt,0) emb_empcnt,isnull(em.Embqty,0) Embqty,isnull(g.Embsal,0) Embsal from 
    ' (select b.dot, COUNT(distinct b.nempno) cut_Empcnt, round(SUM(b.daysalary),2) Cutsal from rrcolor.dbo.empdailysalary b
    '               left join rrcolor.dbo.empmaster c on c.nempno=b.nempno
    '               where b.dot>=@d1 and b.dot<=@d2 and c.subdept='CUTTING' 
    '               group by b.dot) k
    '    left join (select b.dot, COUNT(distinct b.nempno) Emb_empcnt, round(SUM(b.daysalary),2) Embsal from rrcolor.dbo.empdailysalary b
    '               left join rrcolor.dbo.empmaster c on c.nempno=b.nempno 
    '               where b.dot>=@d1 and b.dot<=@d2 and c.subdept='EMBROIDING'
    '               group by b.dot) g on g.dot=k.dot 

    '  left join (select b.U_DocDate,SUM(c.U_AccpQty) cutqty from [@INM_Owip] b with (nolock)  
    '                               left join [@INM_wip1] c with (nolock) on c.DocEntry=b.DocEntry  
    '                               left join oitm t with (nolock) on t.itemcode=c.u_itemcode  
    '                               where b.u_docdate>='2025-02-01' and b.u_docdate<='2025-02-28'  and b.u_trantype='RG'  and b.canceled not in ('Y')   
    '                               and t.ItmsGrpCod not in (170,100,102,105) and  b.U_OperCode='CUTGD' --and  c.U_AccptWhs='Cut' --and LEFT(c.u_cutno,1) not in ('V')  
    '                               group by b.u_docdate) ct on ct.U_DocDate=k.dot

    '  left join (select b.U_DocDate,SUM(c.U_AccpQty) Embqty from [@INM_Owip] b with (nolock)  
    '                               left join [@INM_wip1] c with (nolock) on c.DocEntry=b.DocEntry  
    '                               left join oitm t with (nolock) on t.itemcode=c.u_itemcode  
    '                               where b.u_docdate>='2025-02-01' and b.u_docdate<='2025-02-28'  and b.u_trantype='RG'  and b.canceled not in ('Y')   
    '                               and t.ItmsGrpCod not in (170,100,102,105) and  b.U_OperCode='EMBGD' --and  c.U_AccptWhs='Cut' --and LEFT(c.u_cutno,1) not in ('V')  
    '                               group by b.u_docdate) em on em.U_DocDate=k.dot 

    '							   ) kk
    '       union all		
    '	  select @lstdat dot,0 cut_empcnt,0 cutqty,0 cutsal,0 cut_rate,0 emb_empcnt,0 embqty,0 embsal,0 emb_rate) jk				   
    '	order by jk.dot

    '   'select ep.punchno, k.empno,  SUM(CASE WHEN k.othr > 0 THEN k.othr  ELSE 0  END) AS othr from (
    'select bb.AttendanceDate, bb.empno,bb.intime,bb.outtime,
    'case when bb.intime='1900-01-01 00:00:00.000' and bb.outtime<>'1900-01-01 00:00:00.000' then
    '(DATEDIFF(MINUTE, DATEADD(HOUR, 9, CAST(bb.attendancedate AS DATETIME)),bb.outtime) / 60)-9  
    '  when bb.intime<>'1900-01-01 00:00:00.000' and bb.outtime='1900-01-01 00:00:00.000' then
    '  (DATEDIFF(MINUTE, bb.intime, dateadd(hour,18,CAST(bb.attendancedate AS DATETIME)) ) / 60)-9  
    '  else
    '(DATEDIFF(MINUTE, intime,outtime) / 60)-9 end othr3,

    'case when bb.intime<>'1900-01-01 00:00:00.000' and bb.outtime<>'1900-01-01 00:00:00.000' then
    ' (DATEDIFF(MINUTE, intime,outtime) / 60)-9
    'else
    '   0
    'end othr

    ' from ehr.dbo.Attendance bb
    'where bb.empno in ('E22-02234') and  bb.attendancedate>='2025-02-01' and bb.attendancedate<='2025-02-28') k
    'inner join ehr.dbo.EmployeeDetails ep on ep.empno=k.EmpNo
    'inner join rrcolor.dbo.empmaster em on em.nempno=ep.PunchNo  and em.subsection not in ('TAILOR(PIECE RATE)')
    'group by k.empno,ep.PunchNo
    'having  SUM(CASE WHEN k.othr > 0 THEN k.othr  ELSE 0  END)>0



    'raja incentive
    '    select k.* from 
    '(select date, [lineno] linno,sum(nomac) nomac,count(date) cnt,round(sum(nomac)/count(date),0) avgmac from prodcost.dbo.operf where date>='2025-02-01' and date<='2025-02-28' and [lineno]<>'20'
    ' group by [lineno],date) k

    'select j.u_docdate,j.u_flno, sum(j.stqty) Stqty,sum(j.strwqty) strwqty from ( 
    ' select b.U_DocDate,b.u_flno, isnull(case when b.u_type='RG' then  SUM(c.U_RecdQty) end,0) stqty,isnull(case when b.u_type='RW' then  SUM(c.U_RecdQty) end,0) strwqty from [@INM_OPDE] b with (nolock) 
    ' left join [@INM_PDE1] c with (nolock) on c.DocEntry=b.DocEntry 
    ' left join oitm t with (nolock) on t.itemcode=c.u_itemcode  
    ' where b.u_docdate>=@d1 and b.u_docdate<=@d2 and b.u_docstatus='R' and b.canceled not in ('Y')  
    ' and t.ItmsGrpCod not in (170,100,102,105) and b.U_DFOperCode in ('STGD','KAJAGD') group by b.u_docdate,b.u_type,b.u_flno) j 
    ' group by j.u_docdate,j.u_flno



    'declare @d1 as nvarchar(20)
    'declare @d2 as nvarchar(20)

    'set @d1='2025-02-01'
    'set @d2='2025-02-28'


    'select  e.date,d.lno,d.nomac,e.nopcs,d.nomac*e.nopcs totqty,l.stqty,l.strwqty, sum(d.nomac*e.nopcs) over (partition by e.date order by e.date) total, 
    'sum(l.stqty+l.strwqty)  over (partition by e.date order by e.date) prodqty from 
    ' (select actlineno lno,sum(nomac) nomac,remarks	 from prodcost.dbo.linemaster group by actlineno,remarks) d
    '  inner join ( select b.date, b.[lineno] lno,c.nopcs from prodcost.dbo.operf b
    '  left join ( select brand,nomac,nopcs from prodcost.dbo.incentivemaster where active='Y'
    '  group by brand,nomac,nopcs) c on c.brand=b.brand
    '  where b.date>=@d1 and b.date<=@d2 and b.totprodqty<>0) e on d.lno=e.lno

    'left join (
    '    select j.u_docdate,j.u_flno, sum(j.stqty) Stqty,sum(j.strwqty) strwqty from ( 
    '  select b.U_DocDate,b.u_flno, isnull(case when b.u_type='RG' then  SUM(c.U_RecdQty) end,0) stqty,isnull(case when b.u_type='RW' then  SUM(c.U_RecdQty) end,0) strwqty from [@INM_OPDE] b with (nolock) 
    ' left join [@INM_PDE1] c with (nolock) on c.DocEntry=b.DocEntry 
    ' left join oitm t with (nolock) on t.itemcode=c.u_itemcode  
    ' where b.u_docdate>=@d1 and b.u_docdate<=@d2 and b.u_docstatus='R' and b.canceled not in ('Y')  
    ' and t.ItmsGrpCod not in (170,100,102,105) and b.U_DFOperCode in ('STGD','KAJAGD') group by b.u_docdate,b.u_type,b.u_flno) j 
    ' group by j.u_docdate,j.u_flno) l on l.u_docdate=e.date and l.U_FLNo=d.lno collate SQL_Latin1_General_CP850_CI_AS


End Class
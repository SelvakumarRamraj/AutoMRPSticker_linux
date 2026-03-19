Imports Microsoft.VisualBasic
Public Class frmlineprodconsolidaterep
    Dim msql, msql2 As String
    Private Sub frmlineprodconsolidaterep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = My.Computer.Screen.Bounds.Height - 10
        Me.Width = My.Computer.Screen.Bounds.Width
        dg.Width = My.Computer.Screen.Bounds.Width - 25
    End Sub
    Private Sub loaddata()
        msql = "declare @d1 as nvarchar(20)" & vbCrLf _
               & "declare @d2 as nvarchar(20) " & vbCrLf _
               & " set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'" & vbCrLf _
               & " set @d2='" & Format(CDate(mskdateto.Text), "yyyy.MM-dd") & "'" & vbCrLf _
              & "select case when k.[date] is null then l.u_docdate else k.[date] end [date],isnull(sum(k.L1Cnt),0) 'L1Cnt',isnull(sum(k.L1Qty),0) 'L1Qty',isnull(sum(k.L2Cnt),0) 'L2Cnt',isnull(sum(k.L2Qty),0) 'L2Qty',isnull(sum(k.L3Cnt),0) 'L3Cnt',isnull(sum(k.L3Qty),0) 'L3Qty'," & vbCrLf _
              & " isnull(sum(k.L4Cnt),0) 'L4Cnt',isnull(sum(k.L4Qty),0) 'L4Qty',isnull(sum(k.L5Cnt),0) 'L5Cnt',isnull(sum(k.L5Qty),0) 'L5Qty',isnull(sum(k.L6Cnt),0) 'L6Cnt',isnull(sum(k.L6Qty),0) 'L6Qty', " & vbCrLf _
              & " isnull(sum(k.L7Cnt),0) 'L7Cnt',isnull(sum(k.L7Qty),0) 'L7Qty',isnull(sum(k.L8Cnt),0) 'L8Cnt',isnull(sum(k.L8Qty),0) 'L8Qty',isnull(sum(k.L9Cnt),0) 'L9Cnt',isnull(sum(k.L9Qty),0) 'L9Qty', " & vbCrLf _
              & " isnull(sum(k.L10Cnt),0) 'L10Cnt',isnull(sum(k.L10Qty),0) 'L10Qty',isnull(sum(k.L11Cnt),0) 'L11Cnt',isnull(sum(k.L11Qty),0) 'L11Qty',isnull(sum(k.L12Cnt),0) 'L12Cnt',isnull(sum(k.L12Qty),0) 'L12Qty'," & vbCrLf _
              & "isnull(sum(k.L13Cnt),0) 'L13Cnt',isnull(sum(k.L13Qty),0) 'L13Qty',isnull(sum(k.totlinecnt),0) TotCnt,isnull(sum(k.totlineqty),0) TotQty, isnull(l.irnqty,0) TotIRNQty from ( " & vbCrLf _
              & " select [date], isnull(sum([1]),0) as 'L1Qty',isnull(sum([2]),0) as'L2Qty',isnull(sum([3]),0) as 'L3Qty',isnull(sum([4]),0) as 'L4Qty', " & vbCrLf _
              & " isnull(sum([5]),0) as 'L5Qty',isnull(sum([6]),0) as 'L6Qty',isnull(sum([7]),0) as 'L7Qty',isnull(sum([8]),0) as 'L8Qty',isnull(sum([9]),0) as 'L9Qty'," & vbCrLf _
              & " isnull(sum([10]),0) as 'L10Qty',isnull(sum([11]),0) as 'L11Qty',isnull(sum([12]),0) as 'L12Qty',isnull(sum([13]),0) as 'L13Qty'," & vbCrLf _
              & " sum((isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0))) Totlineqty, " & vbCrLf _
              & " 0 as 'L1Cnt', 0 as'L2Cnt',0 as 'L3Cnt',0 as 'L4Cnt',0 as 'L5Cnt',0 as 'L6Cnt',0 as 'L7Cnt',0 as 'L8Cnt',0 as 'L9Cnt',0 as 'L10Cnt',0 as 'L11Cnt',0 as 'L12Cnt',0 as 'L13Cnt', " & vbCrLf _
              & " 0 as totlinecnt  from (" & vbCrLf
        msql = msql & " select d.[date],d.[lineno],count(b.cnt) cnt,sum(d.stqty) stqty from  " & vbCrLf _
                   & " (select c.u_docdate [date],c.u_lineno [lineno], sum(b.U_AccpQty) stqty from antsprodlive.dbo.[@inm_wip1] b " & vbCrLf _
                   & " inner join antsprodlive.dbo.[@inm_owip] c on c.docentry=b.docentry " & vbCrLf _
                   & " where c.u_docdate>=@d1 and c.u_docdate<=@d2 and c.U_OperCode in ('STGD','KAJAGD') " & vbCrLf _
                   & " group by c.u_docdate,c.u_lineno) d " & vbCrLf _
                   & " left join (select b.[date],c.[Lineno], count(b.empid) cnt from prodcost.dbo.perf1 b " & vbCrLf _
                   & " inner join prodcost.dbo.operf c on c.bno=b.bno and c.[date]=b.[date] " & vbCrLf _
          & " where b.[date]>=@d1 and b.[date]<=@d2 " & vbCrLf _
          & " group by b.[date],c.[Lineno]) b on b.[date]=d.[date] and b.[lineno] collate SQL_Latin1_General_CP850_CI_AS =d.[lineno] group by d.[date],d.[lineno] " & vbCrLf


        '& " (select b.[date],c.[Lineno], count(b.empid) cnt,d.stqty from prodcost.dbo.perf1 b " & vbCrLf _
        '& " inner join prodcost.dbo.operf c on c.bno=b.bno and c.[date]=b.[date] " & vbCrLf _
        '& " left join (select c.u_docdate,c.u_lineno, sum(b.U_AccpQty) stqty from antsprodlive.dbo.[@inm_wip1] b " & vbCrLf _
        '              & "  inner join antsprodlive.dbo.[@inm_owip] c on c.docentry=b.docentry " & vbCrLf _
        '              & "  where c.u_docdate>=@d1 and c.u_docdate<=@d2 and c.U_OperCode in ('STGD','KAJAGD') " & vbCrLf _
        '              & "  group by c.u_docdate,c.u_lineno) d on d.u_docdate=b.[date]  and d.u_lineno=c.[lineno] collate SQL_Latin1_General_CP850_CI_AS " & vbCrLf _
        '              & "  where b.[date]>=@d1 and b.[date]<=@d2 " & vbCrLf _
        '              & "  group by b.[date],c.[Lineno],d.stqty
        msql = msql & " ) s " & vbCrLf _
                            & "  pivot (sum(s.stqty) for s.[lineno] in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13])) P " & vbCrLf _
                            & "  group by [date] " & vbCrLf _
                            & " union all " & vbCrLf _
                            & " select [date],0 as 'L1Qty',0 as'L2Qty',0 as 'L3Qty',0 as 'L4Qty',0 as 'L5Qty',0 as 'L6Qty',0 as 'L7Qty',0 as 'L8Qty',0 as 'L9Qty',0 as 'L10Qty',0 as 'L11Qty', " & vbCrLf _
                            & " 0 as 'L12Qty',0 as 'L13Qty',0 as totlineqty, " & vbCrLf _
                            & " isnull(sum([1]),0) as 'L1Cnt',isnull(sum([2]),0) as'L2Cnt',isnull(sum([3]),0) as 'L3Cnt',isnull(sum([4]),0) as 'L4Cnt',isnull(sum([5]),0) as 'L5Cnt', " & vbCrLf _
                            & " isnull(sum([6]),0) as 'L6Cnt',isnull(sum([7]),0) as 'L7Cnt',isnull(sum([8]),0) as 'L8Cnt',isnull(sum([9]),0) as 'L9Cnt',isnull(sum([10]),0) as 'L10Cnt', " & vbCrLf _
                            & " isnull(sum([11]),0) as 'L11Cnt',isnull(sum([12]),0) as 'L12Cnt',isnull(sum([13]),0) as 'L13Cnt', " & vbCrLf _
                            & " sum((isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0))) Totlinecnt " & vbCrLf _
                            & " from  (select l.[date],l.[Lineno], count(l.empid) cnt,l.stqty from (" & vbCrLf _
                            & " select b.[date],c.[Lineno], b.empid,d.stqty from prodcost.dbo.perf1 b " & vbCrLf _
                            & " inner join prodcost.dbo.operf c on c.bno=b.bno and c.[date]=b.[date] " & vbCrLf _
                            & " left join (select c.u_docdate,c.u_lineno, sum(b.U_AccpQty) stqty from antsprodlive.dbo.[@inm_wip1] b " & vbCrLf _
                            & " inner join antsprodlive.dbo.[@inm_owip] c on c.docentry=b.docentry " & vbCrLf _
                            & " where c.u_docdate>=@d1 and c.u_docdate<=@d2 and c.U_OperCode in ('STGD','KAJAGD') " & vbCrLf _
                            & " group by c.u_docdate,c.u_lineno) d on d.u_docdate=b.[date]  and d.u_lineno=c.[lineno] collate SQL_Latin1_General_CP850_CI_AS " & vbCrLf _
                            & " where b.[date]>=@d1 and b.[date]<=@d2  " & vbCrLf _
                            & " group by b.[date],c.[Lineno],d.stqty,b.empid) l group by l.[date],l.[Lineno],l.stqty  ) s " & vbCrLf _
                            & " pivot (sum(s.cnt) for s.[lineno] in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13])) P " & vbCrLf _
                            & " group by [date]) k " & vbCrLf _
         & " right Join (select c.u_docdate, 0 stqty,sum(U_AccpQty) irnqty from antsprodlive.dbo.[@inm_wip1] b " & vbCrLf _
         & " inner join antsprodlive.dbo.[@inm_owip] c on c.docentry=b.docentry " & vbCrLf _
         & " where c.u_docdate>=@d1 and c.u_docdate<=@d2 and c.U_OperCode in ('IRONGD') and c.u_lineno<>'SC' and left(b.u_cutno,1)<>'V' " & vbCrLf _
         & " group by c.u_docdate) l on l.u_docdate=k.[date] " & vbCrLf _
         & " group by k.[date],l.irnqty,l.u_docdate " & vbCrLf _
         & " union all " & vbCrLf _
         & " select  dateadd(d,1,@d2) date,0 L1Cnt,0 L1Qty,0	L2Cnt,0	L2Qty,0	L3Cnt,0	L3Qty,0	L4Cnt,0	L4Qty,0	L5Cnt,0	L5Qty,0	L6Cnt,0	L6Qty,0	L7Cnt,0	L7Qty,0	L8Cnt,0	L8Qty,0	L9Cnt,0	L9Qty, " & vbCrLf _
         & " 0 L10Cnt,0 L10Qty,0	L11Cnt,0 L11Qty,0 L12Cnt,0 L12Qty,0	L13Cnt,0 L13Qty,0 TotlineCnt,0 TotLineQty,0 TotalIRNQty " & vbCrLf _
         & " order by [date]"

        Cursor = Cursors.WaitCursor
        dg.DataSource = Nothing

        Dim dt As DataTable = getDataTable(msql)
        dg.DataSource = dt
        dg.EnableHeadersVisualStyles = False
        For i As Integer = 0 To dg.Columns.Count - 1

            dg.Columns(i).HeaderCell.Style.Font = New Font("Lao UI", 9, FontStyle.Bold)
            dg.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            dg.Columns(i).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Regular)
            If i Mod 2 = 0 Then
                dg.Columns(i).HeaderCell.Style.BackColor = Color.Honeydew
                dg.Columns(i).HeaderCell.Style.ForeColor = Color.DarkSlateBlue
            Else
                dg.Columns(i).HeaderCell.Style.BackColor = Color.LightYellow
                dg.Columns(i).HeaderCell.Style.ForeColor = Color.DarkOliveGreen
            End If
            If i = 0 Then
                dg.Columns(i).Width = 75
            End If
            If i > 0 Then
                If i >= 27 Then
                    dg.Columns(i).Width = 68
                    dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
                Else
                    dg.Columns(i).Width = 50
                    dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Regular)
                End If

                'dg.Columns(i).Width = 50
                dg.Columns(i).ValueType = GetType(Integer)
                dg.Columns(i).DefaultCellStyle.Format = ("######0")
                dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                If i Mod 2 = 0 Then
                    dg.Columns(i).DefaultCellStyle.BackColor = Color.Honeydew
                    dg.Columns(i).DefaultCellStyle.ForeColor = Color.DarkSlateBlue
                Else
                    dg.Columns(i).DefaultCellStyle.BackColor = Color.LightYellow
                    dg.Columns(i).DefaultCellStyle.ForeColor = Color.DarkOliveGreen
                End If

            End If
            dg.Columns(i).ReadOnly = True
        Next i
        dg.Rows(dg.Rows.Count - 1).DefaultCellStyle.BackColor = Color.RoyalBlue
        dg.Rows(dg.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.White
        dg.Rows(dg.Rows.Count - 1).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
        dg.Rows(dg.Rows.Count - 1).Cells(25).Value = "Total"
        dg.Rows(dg.Rows.Count - 1).Cells(27).Value = Convert.ToDouble(dt.Compute("avg(Totcnt)", ""))
        dg.Rows(dg.Rows.Count - 1).Cells(28).Value = Convert.ToDouble(dt.Compute("Sum(Totqty)", ""))
        dg.Rows(dg.Rows.Count - 1).Cells(29).Value = Convert.ToDouble(dt.Compute("Sum(Totirnqty)", ""))

        Cursor = Cursors.Default

    End Sub
    Private Sub loaddata2()
        msql = "declare @d1 as nvarchar(20)" & vbCrLf _
               & "declare @d2 as nvarchar(20) " & vbCrLf _
               & " set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'" & vbCrLf _
               & " set @d2='" & Format(CDate(mskdateto.Text), "yyyy.MM-dd") & "'" & vbCrLf _
              & "select case when k.[date] is null then l.u_docdate else k.[date] end [date],isnull(sum(k.L1Cnt),0) 'L1Sal',isnull(sum(k.L1Qty),0) 'L1Qty',isnull(sum(k.L2Cnt),0) 'L2Sal',isnull(sum(k.L2Qty),0) 'L2Qty',isnull(sum(k.L3Cnt),0) 'L3Sal',isnull(sum(k.L3Qty),0) 'L3Qty'," & vbCrLf _
              & " isnull(sum(k.L4Cnt),0) 'L4Sal',isnull(sum(k.L4Qty),0) 'L4Qty',isnull(sum(k.L5Cnt),0) 'L5Sal',isnull(sum(k.L5Qty),0) 'L5Qty',isnull(sum(k.L6Cnt),0) 'L6Sal',isnull(sum(k.L6Qty),0) 'L6Qty', " & vbCrLf _
              & " isnull(sum(k.L7Cnt),0) 'L7Sal',isnull(sum(k.L7Qty),0) 'L7Qty',isnull(sum(k.L8Cnt),0) 'L8Sal',isnull(sum(k.L8Qty),0) 'L8Qty',isnull(sum(k.L9Cnt),0) 'L9Sal',isnull(sum(k.L9Qty),0) 'L9Qty', " & vbCrLf _
              & " isnull(sum(k.L10Cnt),0) 'L10Sal',isnull(sum(k.L10Qty),0) 'L10Qty',isnull(sum(k.L11Cnt),0) 'L11Sal',isnull(sum(k.L11Qty),0) 'L11Qty',isnull(sum(k.L12Cnt),0) 'L12Sal',isnull(sum(k.L12Qty),0) 'L12Qty'," & vbCrLf _
              & "isnull(sum(k.L13Cnt),0) 'L13Sal',isnull(sum(k.L13Qty),0) 'L13Qty',isnull(sum(k.totlinecnt),0) TotSal,isnull(sum(k.totlineqty),0) TotQty, isnull(l.irnqty,0) TotIRNQty from ( " & vbCrLf _
              & " select [date], isnull(sum([1]),0) as 'L1Qty',isnull(sum([2]),0) as'L2Qty',isnull(sum([3]),0) as 'L3Qty',isnull(sum([4]),0) as 'L4Qty', " & vbCrLf _
              & " isnull(sum([5]),0) as 'L5Qty',isnull(sum([6]),0) as 'L6Qty',isnull(sum([7]),0) as 'L7Qty',isnull(sum([8]),0) as 'L8Qty',isnull(sum([9]),0) as 'L9Qty'," & vbCrLf _
              & " isnull(sum([10]),0) as 'L10Qty',isnull(sum([11]),0) as 'L11Qty',isnull(sum([12]),0) as 'L12Qty',isnull(sum([13]),0) as 'L13Qty'," & vbCrLf _
              & " sum((isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0))) Totlineqty, " & vbCrLf _
              & " 0 as 'L1Cnt', 0 as'L2Cnt',0 as 'L3Cnt',0 as 'L4Cnt',0 as 'L5Cnt',0 as 'L6Cnt',0 as 'L7Cnt',0 as 'L8Cnt',0 as 'L9Cnt',0 as 'L10Cnt',0 as 'L11Cnt',0 as 'L12Cnt',0 as 'L13Cnt', " & vbCrLf _
              & " 0 as totlinecnt  from (" & vbCrLf
        msql = msql & " select d.[date],d.[lineno],count(b.cnt) cnt,sum(d.stqty) stqty from  " & vbCrLf _
                          & " (select c.u_docdate [date],c.u_lineno [lineno], sum(b.U_AccpQty) stqty from antsprodlive.dbo.[@inm_wip1] b " & vbCrLf _
                          & " inner join antsprodlive.dbo.[@inm_owip] c on c.docentry=b.docentry " & vbCrLf _
                          & " where c.u_docdate>=@d1 and c.u_docdate<=@d2 and c.U_OperCode in ('STGD','KAJAGD') " & vbCrLf _
                          & " group by c.u_docdate,c.u_lineno) d " & vbCrLf _
                          & " left join (select b.[date],c.[Lineno], count(b.empid) cnt from prodcost.dbo.perf1 b " & vbCrLf _
                          & " inner join prodcost.dbo.operf c on c.bno=b.bno and c.[date]=b.[date] " & vbCrLf _
                 & " where b.[date]>=@d1 and b.[date]<=@d2 " & vbCrLf _
                 & " group by b.[date],c.[Lineno]) b on b.[date]=d.[date] and b.[lineno] collate SQL_Latin1_General_CP850_CI_AS =d.[lineno] group by d.[date],d.[lineno] " & vbCrLf



        '& " (select b.[date],c.[Lineno], count(b.empid) cnt,d.stqty from prodcost.dbo.perf1 b " & vbCrLf _
        '& " inner join prodcost.dbo.operf c on c.bno=b.bno and c.[date]=b.[date] " & vbCrLf _
        '& " left join (select c.u_docdate,c.u_lineno, sum(b.U_AccpQty) stqty from antsprodlive.dbo.[@inm_wip1] b " & vbCrLf _
        '              & "  inner join antsprodlive.dbo.[@inm_owip] c on c.docentry=b.docentry " & vbCrLf _
        '              & "  where c.u_docdate>=@d1 and c.u_docdate<=@d2 and c.U_OperCode in ('STGD','KAJAGD')  " & vbCrLf _
        '              & "  group by c.u_docdate,c.u_lineno) d on d.u_docdate=b.[date]  and d.u_lineno=c.[lineno] collate SQL_Latin1_General_CP850_CI_AS " & vbCrLf _
        '              & "  where b.[date]>=@d1 and b.[date]<=@d2 " & vbCrLf _
        '              & "  group by b.[date],c.[Lineno],d.stqty
        msql = msql & " ) s " & vbCrLf _
                            & "  pivot (sum(s.stqty) for s.[lineno] in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13])) P " & vbCrLf _
                            & "  group by [date] " & vbCrLf _
                            & " union all " & vbCrLf _
                            & " select [date],0 as 'L1Qty',0 as'L2Qty',0 as 'L3Qty',0 as 'L4Qty',0 as 'L5Qty',0 as 'L6Qty',0 as 'L7Qty',0 as 'L8Qty',0 as 'L9Qty',0 as 'L10Qty',0 as 'L11Qty', " & vbCrLf _
                            & " 0 as 'L12Qty',0 as 'L13Qty',0 as totlineqty, " & vbCrLf _
                            & " isnull(sum([1]),0) as 'L1Cnt',isnull(sum([2]),0) as'L2Cnt',isnull(sum([3]),0) as 'L3Cnt',isnull(sum([4]),0) as 'L4Cnt',isnull(sum([5]),0) as 'L5Cnt', " & vbCrLf _
                            & " isnull(sum([6]),0) as 'L6Cnt',isnull(sum([7]),0) as 'L7Cnt',isnull(sum([8]),0) as 'L8Cnt',isnull(sum([9]),0) as 'L9Cnt',isnull(sum([10]),0) as 'L10Cnt', " & vbCrLf _
                            & " isnull(sum([11]),0) as 'L11Cnt',isnull(sum([12]),0) as 'L12Cnt',isnull(sum([13]),0) as 'L13Cnt', " & vbCrLf _
                            & " sum((isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+isnull([13],0))) Totlinecnt " & vbCrLf _
                            & " from  ( " & vbCrLf _
                            & " select l.[date],l.[Lineno], sum(l.dsalary) cnt,l.stqty from ( " & vbCrLf _
                            & " select b.[date],c.[Lineno],count(b.empid) over(partition by b.empid,b.[date]) cnt,b.empid, case when count(b.empid)>0 then (isnull(b.salary,0)/30)/count(b.empid) over (partition by b.empid,b.[date]) else isnull(b.salary,0)/30 end dsalary , d.stqty from prodcost.dbo.perf1 b " & vbCrLf _
                            & " inner join prodcost.dbo.operf c on c.bno=b.bno and c.[date]=b.[date] " & vbCrLf _
                            & " left join (select c.u_docdate,c.u_lineno, sum(b.U_AccpQty) stqty from antsprodlive.dbo.[@inm_wip1] b  " & vbCrLf _
                            & " inner join antsprodlive.dbo.[@inm_owip] c on c.docentry=b.docentry " & vbCrLf _
                            & " where c.u_docdate>=@d1 and c.u_docdate<=@d2 and c.U_OperCode in ('STGD','KAJAGD') " & vbCrLf _
                            & " group by c.u_docdate,c.u_lineno) d on d.u_docdate=b.[date]  and d.u_lineno=c.[lineno] collate SQL_Latin1_General_CP850_CI_AS " & vbCrLf _
                            & " where b.[date]>=@d1 and b.[date]<=@d2 " & vbCrLf _
                            & " group by b.[date],c.[Lineno],d.stqty,b.empid,b.salary) l  group by l.[date],l.[Lineno],l.stqty " & vbCrLf _
                            & "  ) s " & vbCrLf _
                            & " pivot (sum(s.cnt) for s.[lineno] in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13])) P " & vbCrLf _
                            & " group by [date]) k " & vbCrLf _
         & " right Join (select c.u_docdate, 0 stqty,sum(U_AccpQty) irnqty from antsprodlive.dbo.[@inm_wip1] b " & vbCrLf _
         & " inner join antsprodlive.dbo.[@inm_owip] c on c.docentry=b.docentry " & vbCrLf _
         & " where c.u_docdate>=@d1 and c.u_docdate<=@d2 and c.U_OperCode in ('IRONGD') and c.u_lineno<>'SC' and left(b.u_cutno,1)<>'V' " & vbCrLf _
         & " group by c.u_docdate) l on l.u_docdate=k.[date] " & vbCrLf _
         & " group by k.[date],l.irnqty,l.u_docdate " & vbCrLf _
         & " union all " & vbCrLf _
         & " select  dateadd(d,1,@d2) date,0 L1Cnt,0 L1Qty,0	L2Cnt,0	L2Qty,0	L3Cnt,0	L3Qty,0	L4Cnt,0	L4Qty,0	L5Cnt,0	L5Qty,0	L6Cnt,0	L6Qty,0	L7Cnt,0	L7Qty,0	L8Cnt,0	L8Qty,0	L9Cnt,0	L9Qty, " & vbCrLf _
         & " 0 L10Cnt,0 L10Qty,0	L11Cnt,0 L11Qty,0 L12Cnt,0 L12Qty,0	L13Cnt,0 L13Qty,0 TotlineCnt,0 TotLineQty,0 TotalIRNQty " & vbCrLf _
         & " order by [date]"

        Cursor = Cursors.WaitCursor
        dg.DataSource = Nothing

        Dim dt As DataTable = getDataTable(msql)
        dg.DataSource = dt
        dg.EnableHeadersVisualStyles = False
        For i As Integer = 0 To dg.Columns.Count - 1

            dg.Columns(i).HeaderCell.Style.Font = New Font("Lao UI", 9, FontStyle.Bold)
            dg.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            dg.Columns(i).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Regular)
            If i Mod 2 = 0 Then
                dg.Columns(i).HeaderCell.Style.BackColor = Color.Honeydew
                dg.Columns(i).HeaderCell.Style.ForeColor = Color.DarkSlateBlue
            Else
                dg.Columns(i).HeaderCell.Style.BackColor = Color.LightYellow
                dg.Columns(i).HeaderCell.Style.ForeColor = Color.DarkOliveGreen
            End If
            If i = 0 Then
                dg.Columns(i).Width = 75
            End If
            If i > 0 Then
                If i >= 27 Then
                    dg.Columns(i).Width = 68
                    dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
                Else
                    dg.Columns(i).Width = 50
                    dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Regular)
                End If

                'dg.Columns(i).Width = 50
                dg.Columns(i).ValueType = GetType(Integer)
                dg.Columns(i).DefaultCellStyle.Format = ("######0")
                dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                If i Mod 2 = 0 Then
                    dg.Columns(i).DefaultCellStyle.BackColor = Color.Honeydew
                    dg.Columns(i).DefaultCellStyle.ForeColor = Color.DarkSlateBlue
                Else
                    dg.Columns(i).DefaultCellStyle.BackColor = Color.LightYellow
                    dg.Columns(i).DefaultCellStyle.ForeColor = Color.DarkOliveGreen
                End If

            End If
            dg.Columns(i).ReadOnly = True
        Next i
        dg.Rows(dg.Rows.Count - 1).DefaultCellStyle.BackColor = Color.RoyalBlue
        dg.Rows(dg.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.White
        dg.Rows(dg.Rows.Count - 1).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
        dg.Rows(dg.Rows.Count - 1).Cells(25).Value = "Total"
        dg.Rows(dg.Rows.Count - 1).Cells(27).Value = Convert.ToDouble(dt.Compute("sum(TotSal)", ""))
        dg.Rows(dg.Rows.Count - 1).Cells(28).Value = Convert.ToDouble(dt.Compute("Sum(Totqty)", ""))
        dg.Rows(dg.Rows.Count - 1).Cells(29).Value = Convert.ToDouble(dt.Compute("Sum(Totirnqty)", ""))

        Cursor = Cursors.Default

    End Sub
    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.Click
        If chkamt.Checked = True Then
            Call loaddata2()
        ElseIf chkana.Checked = True Then
            Call loadana()
        ElseIf Chklinecooli.Checked = True Then
            If MsgBox("Employee wise detail", vbYesNo) = vbYes Then
                Call loadempwisecooli()
            Else
                Call loadcooli()
            End If
        ElseIf Chkot.Checked Then
            Call getotqty()
        Else
            Call loaddata()
        End If

    End Sub

    Private Sub CmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdExit.Click
        Me.Close()
    End Sub
    Private Sub dg_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dg.DataError
        If (e.Exception.Message = "DataGridViewComboBoxCell value is not valid.") Then
            Dim value As Object = dg.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & vbNullString
            If Not CType(dg.Columns(e.ColumnIndex), DataGridViewComboBoxColumn).Items.Contains(Text) Then
                CType(dg.Columns(e.ColumnIndex), DataGridViewComboBoxColumn).Items.Add(Text)
                e.ThrowException = False
            End If

        End If
    End Sub

    Private Sub cmdexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexcel.Click
        gridexcelexport4(dg, 0, "ProdCosolidateRep", "Production Compare and Consolidated Statement")
    End Sub

    Private Sub loadana()
        'msql2 = "declare @d1 as nvarchar(20)" & vbCrLf _
        '       & "declare @d2 as nvarchar(20) " & vbCrLf _
        '       & " set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'" & vbCrLf _
        '       & " set @d2='" & Format(CDate(mskdateto.Text), "yyyy.MM-dd") & "'" & vbCrLf _
        '       & "select b.[lineno] linno, b.empno,b.empname, b.opername,b.jobgrade,sum(b.totmin) totmin, sum(b.totprod) prodqty,count(b.opername) cnt,sum(b.totprod)/count(b.opername) avgqty,c.pcs*8 daypcs from prodcost.dbo.perf1 b " & vbCrLf _
        '       & " left join prodcost.dbo.processjobmaster c on c.opername=b.opername " & vbCrLf _
        '       & " where b.date>=@d1 and b.date<=@d2  " & vbCrLf _
        '       & " and b.empno in (12100388,12104097,12103662,12100645,12103930,12200118,12104040,12101002,12101966,12102244,12202564, " & vbCrLf _
        '       & " 12101816,12101471,12100686,12102948,12100378,12100687,12101195,12103396,12103442,12102314, " & vbCrLf _
        '       & " 12102228,12102904,12100329,12103360,12102627,12100981,12200939,12200614,12102472,12102951, " & vbCrLf _
        '       & " 12103408,12101001,12100980,12101471,12100704,12100402,12103949,12103806,12101964,12200009)" & vbCrLf _
        '       & " and b.incyn='Y'  group by b.[lineno],b.opername,b.empno,b.empname,c.pcs,b.jobgrade order by  b.[lineno],b.empname "


        msql2 = "declare @d1 as nvarchar(20)" & vbCrLf _
                       & "declare @d2 as nvarchar(20) " & vbCrLf _
                       & " set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'" & vbCrLf _
                       & " set @d2='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "'" & vbCrLf _
                       & " select k.linno,k.empno,k.empname,k.opername,k.jobgrade,k.totmin,k.prodqty,k.cnt,k.avgqty,k.daypcs,nn.totcnt from ( " & vbCrLf _
                       & "select b.[lineno] linno, b.empno,b.empname, b.opername,b.jobgrade,sum(b.totmin) totmin, sum(b.totprod) prodqty,count(b.opername) cnt,sum(b.totprod)/count(b.opername) avgqty,c.pcs*8 daypcs from prodcost.dbo.perf1 b " & vbCrLf _
                       & " left join prodcost.dbo.processjobmaster c on c.opername=b.opername " & vbCrLf _
                       & " where b.date>=@d1 and b.date<=@d2  " & vbCrLf
        If Len(Trim(txtlineno.Text)) > 0 Then
            msql2 = msql2 & " and b.[lineno]='" & Trim(txtlineno.Text) & "' "
        End If
        If Len(Trim(txtempid.Text)) > 0 Then
            msql2 = msql2 & " and b.empno=" & Val(txtempid.Text)
        End If

        msql2 = msql2 & " group by b.[lineno],b.opername,b.empno,b.empname,c.pcs,b.jobgrade) k " & vbCrLf _
        & " left join (select  k.linno, count(k.linno) totcnt from ( " & vbCrLf _
        & "            select [lineno] linno,empno, count([lineno]) totcnt from prodcost.dbo.perf1 where date>=@d1 and date<=@d2 " & vbCrLf _
        & "             group by [lineno],empno) k group by k.linno) nn on nn.linno=k.linno " & vbCrLf _
        & "  order by  k.[linno],k.empname "


        Cursor = Cursors.WaitCursor
        dg.DataSource = Nothing

        Dim dt2 As DataTable = getDataTable(msql2)
        dg.DataSource = dt2
        dg.EnableHeadersVisualStyles = False
        Cursor = Cursors.Default
    End Sub

    Private Sub chkana_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkana.CheckedChanged
        If chkana.Checked = True Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False
        End If
    End Sub

    Private Sub chkamt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkamt.CheckedChanged
        If chkamt.Checked = True Then
            Panel1.Visible = False
        End If
    End Sub

    Private Sub loadcooli()
        msql = "declare @d1 nvarchar(20) " _
               & " declare @d2 nvarchar(20) " _
               & " declare @noday int " _
               & " set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' " _
               & " set @d2='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' " _
               & " set @noday=(Select day(dateadd(mm,DateDiff(mm, -1, getdate()),0) -1)) " _
               & " select jj.* from ( " _
               & " select j.date,j.[lineno], j.cnt,j.totprodqty,j.targetqty,sum(j.daysal) salary, case when isnull(j.cnt,0)>0 and isnull(j.targetqty,0)>0 then sum(j.daysal)/j.targetqty else 0 end targetcooli, " _
               & " case when isnull(j.cnt,0)>0 and isnull(j.totprodqty,0)>0  then sum(j.daysal)/j.totprodqty else 0 end totprodcooli from ( " _
               & " select  k.date,c.empno, k.[lineno],k.totprodqty,k.targetqty,c.totsalary,count(c.empno) over(partition by k.date,k.[lineno]) cnt , " _
               & " case when isnull(c.totsalary,0)>0 then  c.totsalary/@noday else 0 end daysal from ( " _
               & " select b.bno,b.date,b.[lineno], b.brand,b.style,b.totprodqty,b.nomac,i.nopcs,b.nomac*i.nopcs targetqty from prodcost.dbo.operf b " _
               & " left join (select brand,nopcs from prodcost.dbo.incentivemaster where active<>'N' group by brand,nopcs) i on b.brand=i.brand " _
               & "  where b.date>=@d1 and b.date<=@d2 and isnull(b.brand,'')<>'') k " _
               & " inner join (select c.bno, c.date, c.empno,c.[lineno],em.totsalary from prodcost.dbo.perf1 c " _
               & " left join rrcolor.dbo.empmaster em on em.nempno=c.empno " _
               & " where c.date>=@d1 and c.date<=@d2 " _
               & "  group by c.date,c.empno,c.[lineno],em.totsalary,c.bno) c on k.bno=c.bno) j " _
               & " group by  j.date,j.[lineno],j.totprodqty,j.targetqty,j.cnt " _
               & " union all " _
               & " select @d2 date,1000 [lineno],0 cnt, 0 totprodqty,0 targetqty,0 salary,0 targetcooli,0 totprodcooli) jj order by jj.date,abs(jj.[lineno]) "


        Cursor = Cursors.WaitCursor
        dg.DataSource = Nothing

        Dim dt As DataTable = getDataTable(msql)
        dg.DataSource = dt
        dg.EnableHeadersVisualStyles = False
        For i As Integer = 0 To dg.Columns.Count - 1

            dg.Columns(i).HeaderCell.Style.Font = New Font("Lao UI", 9, FontStyle.Bold)
            dg.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            dg.Columns(i).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Regular)
            If i Mod 2 = 0 Then
                dg.Columns(i).HeaderCell.Style.BackColor = Color.Honeydew
                dg.Columns(i).HeaderCell.Style.ForeColor = Color.DarkSlateBlue
            Else
                dg.Columns(i).HeaderCell.Style.BackColor = Color.LightYellow
                dg.Columns(i).HeaderCell.Style.ForeColor = Color.DarkOliveGreen
                End If
            If i = 0 Then
                dg.Columns(i).Width = 75
                End If
            If i >= 2 And i <= 4 Then
                dg.Columns(i).ValueType = GetType(Integer)
                dg.Columns(i).DefaultCellStyle.Format = ("######0")
                dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If

            If i >= 5 And i <= 7 Then
                dg.Columns(i).ValueType = GetType(Decimal)
                dg.Columns(i).DefaultCellStyle.Format = ("#######0.00")
                dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If

                'If i > 0 Then
                '    If i >= 27 Then
                '        dg.Columns(i).Width = 68
                '        dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
                '    Else
                '        dg.Columns(i).Width = 50
                '        dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Regular)
                '    End If

                '    'dg.Columns(i).Width = 50
                '    dg.Columns(i).ValueType = GetType(Integer)
                '    dg.Columns(i).DefaultCellStyle.Format = ("######0")
                '    dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '    If i Mod 2 = 0 Then
                '        dg.Columns(i).DefaultCellStyle.BackColor = Color.Honeydew
                '        dg.Columns(i).DefaultCellStyle.ForeColor = Color.DarkSlateBlue
                '    Else
                '        dg.Columns(i).DefaultCellStyle.BackColor = Color.LightYellow
                '        dg.Columns(i).DefaultCellStyle.ForeColor = Color.DarkOliveGreen
                '    End If

                'End If
            dg.Columns(i).ReadOnly = True
        Next i
        dg.Rows(dg.Rows.Count - 1).DefaultCellStyle.BackColor = Color.RoyalBlue
        dg.Rows(dg.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.White
        dg.Rows(dg.Rows.Count - 1).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
        dg.Rows(dg.Rows.Count - 1).Cells(1).Value = "Total"
        dg.Rows(dg.Rows.Count - 1).Cells(3).Value = Convert.ToDouble(dt.Compute("sum(Totprodqty)", ""))
        dg.Rows(dg.Rows.Count - 1).Cells(4).Value = Convert.ToDouble(dt.Compute("Sum(Targetqty)", ""))
        dg.Rows(dg.Rows.Count - 1).Cells(5).Value = Convert.ToDouble(dt.Compute("Sum(salary)", ""))
        dg.Rows(dg.Rows.Count - 1).Cells(6).Value = Convert.ToDouble(dt.Compute("sum(salary)/sum(targetqty)", ""))
        dg.Rows(dg.Rows.Count - 1).Cells(7).Value = Convert.ToDouble(dt.Compute("sum(salary)/sum(totprodqty)", ""))

        'dg.Rows(dg.Rows.Count - 1).Cells(6).Value = Convert.ToDouble(dt.Compute("avg(targetcooli)", ""))
        'dg.Rows(dg.Rows.Count - 1).Cells(7).Value = Convert.ToDouble(dt.Compute("avg(totprodcooli)", ""))

        Cursor = Cursors.Default

    End Sub

    Private Sub loadempwisecooli()
        msql = "declare @d1 nvarchar(20) " _
            & " declare @d2 nvarchar(20) " _
            & " set @d1='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' " _
            & " set @d2='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' " _
            & " select kk.* from ( " _
            & " select jj.empno,isnull(jj.vname,'') vname,isnull(jj.vdepartment,'') vdepartment,isnull(jj.totsalary,0) totsalary,isnull(sum(jj.daysal),0) salary, isnull(avg(jj.daysal),0) avgsal from (" _
            & " select j.date,j.empno,j.vname,j.vdepartment, j.[lineno],j.totsalary,(j.totsalary/27) daysal from ( " _
            & " select c.date, c.empno,em.vname,em.vdepartment, c.[lineno],em.totsalary from prodcost.dbo.perf1 c " _
            & " left join rrcolor.dbo.empmaster em on em.nempno=c.empno " _
            & " where c.date>=@d1 and c.date<=@d2 " _
            & " group by c.date, c.empno,em.vname,em.vdepartment,c.[lineno],em.totsalary) j) jj " _
            & " group by jj.empno,jj.vname,jj.vdepartment,jj.totsalary " _
            & " union all " _
            & " select 70000001 emnp,'Total' vname,'Z' vdepartment,0 tosalary,0 salary,0 avgsalary ) kk " _
            & " order by kk.vdepartment,kk.empno,kk.vname"
        Cursor = Cursors.WaitCursor
        dg.DataSource = Nothing

        Dim dt2 As DataTable = getDataTable(msql)
        dg.DataSource = dt2
        dg.EnableHeadersVisualStyles = False


        dg.EnableHeadersVisualStyles = False
        For i As Integer = 0 To dg.Columns.Count - 1

            dg.Columns(i).HeaderCell.Style.Font = New Font("Lao UI", 9, FontStyle.Bold)
            dg.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            dg.Columns(i).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            dg.Columns(i).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Regular)
            If i Mod 2 = 0 Then
                dg.Columns(i).HeaderCell.Style.BackColor = Color.Honeydew
                dg.Columns(i).HeaderCell.Style.ForeColor = Color.DarkSlateBlue
            Else
                dg.Columns(i).HeaderCell.Style.BackColor = Color.LightYellow
                dg.Columns(i).HeaderCell.Style.ForeColor = Color.DarkOliveGreen
                End If
            If i = 0 Then
                dg.Columns(i).Width = 75
                End If
            'If i = 3 Then
            '    dg.Columns(i).ValueType = GetType(Integer)
            '    dg.Columns(i).DefaultCellStyle.Format = ("######0")
            '    dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'End If

            If i >= 3 And i <= 5 Then
                dg.Columns(i).ValueType = GetType(Decimal)
                dg.Columns(i).DefaultCellStyle.Format = ("#######0.00")
                dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If


            dg.Columns(i).ReadOnly = True
        Next i
        dg.Rows(dg.Rows.Count - 1).DefaultCellStyle.BackColor = Color.RoyalBlue
        dg.Rows(dg.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.White
        dg.Rows(dg.Rows.Count - 1).DefaultCellStyle.Font = New Font("Lao UI", 8, FontStyle.Bold)
        dg.Rows(dg.Rows.Count - 1).Cells(1).Value = "Total"
        dg.Rows(dg.Rows.Count - 1).Cells(3).Value = Convert.ToDouble(dt2.Compute("sum(Totsalary)", ""))
        dg.Rows(dg.Rows.Count - 1).Cells(4).Value = Convert.ToDouble(dt2.Compute("Sum(salary)", ""))
        dg.Rows(dg.Rows.Count - 1).Cells(5).Value = Convert.ToDouble(dt2.Compute("avg(avgsal)", ""))


        Cursor = Cursors.Default

    End Sub

    Private Sub cmdclear_Click(sender As Object, e As EventArgs) Handles cmdclear.Click
        dg.DataSource = Nothing
        chkamt.Checked = False
        Chklinecooli.Checked = False
        chkana.Checked = False
    End Sub

    Private Sub getotqty()
        'msql = "select b.u_docdate, b.u_lineno, sum(c.U_AccpQty) Qty from [@inm_owip] b " _
        '& " inner join [@inm_wip1] c with (nolock) on b.docentry=c.docentry " _
        '& " where b.u_docdate >='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.u_docdate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' and b.createtime>" & Val(txttime.Text) & " and b.u_opercode='KAJAGD' and b.u_trantype='RG' " _
        '& " group by b.u_docdate,b.u_lineno order by b.u_docdate,b.u_lineno"

        If MsgBox("Linewise OT Production ", vbYesNo) = vbYes Then
            msql = "select k.u_docdate,k.u_lineno,sum(k.qty) qty,sum(k.otqty) otqty,sum(k.qty-k.otqty) DayQty from ( " _
                    & " select b.u_docdate,b.u_lineno,  sum(c.U_AccpQty) Qty,case when b.createtime>=1800 then sum(c.u_accpqty) else 0 end otqty from [@inm_owip] b " _
                    & " inner join [@inm_wip1] c with (nolock) on b.docentry=c.docentry	" _
                    & " where b.u_docdate >='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.u_docdate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' and b.u_opercode='KAJAGD' and U_TranType='RG' " _
                    & " group by b.u_docdate,b.createtime,b.u_lineno) k " _
                    & " group by k.u_docdate,k.u_lineno " _
                    & " order by k.u_docdate,k.u_lineno "

        Else

            msql = "select k.u_docdate,sum(k.qty) qty,sum(k.otqty) otqty,sum(k.qty-k.otqty) DayQty from ( " _
                    & " select b.u_docdate,  sum(c.U_AccpQty) Qty,case when b.createtime>=1800 then sum(c.u_accpqty) else 0 end otqty from [@inm_owip] b " _
                    & " inner join [@inm_wip1] c with (nolock) on b.docentry=c.docentry	" _
                    & " where b.u_docdate >='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and b.u_docdate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' and b.u_opercode='KAJAGD' and U_TranType='RG' " _
                    & " group by b.u_docdate,b.createtime) k " _
                    & " group by k.u_docdate " _
                    & " order by k.u_docdate "
        End If


        Cursor = Cursors.WaitCursor
        dg.DataSource = Nothing

        Dim dt2 As DataTable = getDataTable(msql)
        dg.DataSource = dt2
        Cursor = Cursors.Default
    End Sub
End Class
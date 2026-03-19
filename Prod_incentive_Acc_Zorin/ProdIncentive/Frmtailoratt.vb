Public Class Frmtailoratt
    Dim sql, sql2 As String
    
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If chkpivot.Checked = True Then
            Call loadpivot()
        Else
            sql = "declare @d1 as nvarchar(20) " _
                & " declare @d2 as nvarchar(20) " _
                & " set @d1='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' " _
                & " set @d2='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' "

            If chkdet.Checked = False Then
                'sql = sql & " select dot, linno,count(cdepartment) cnt from rrcolor.dbo.empdailysalary " _
                '    & " where dot>=@d1 and dot<=@d2 and (csno in (13,49,50,51,52,53,54) or csNo>=60 and csno<=85)  and linno>0 group by dot,linno order by dot,linno"


                sql = sql & "select k.dot,k.linno,sum(k.attcnt) attcnt,sum(k.mascnt) mascnt from ( " _
                            & " select dot, linno,count(cdepartment) attcnt,0 mascnt from rrcolor.dbo.empdailysalary where dot>=@d1 and dot<=@d2 " _
                            & " and (csno in (13,49,50,51,52,53,54) or csNo>=60 and csno<=85) and linno>0 group by dot,linno " _
                            & " union all  " _
                            & " select b.dot,b.linno,0 attcnt,count(c.subsection) mascnt  from rrcolor.dbo.empdailysalary b " _
                            & " inner join rrcolor.dbo.empmaster c on c.nempno=b.nempno " _
                            & " where rtrim(c.subdept)='STITCHING' and rtrim(c.subsection) in ('TAILOR','TAILOR(PIECE RATE)') and b.dot>=@d1 and b.dot<=@d2 and b.linno>0 " _
                            & " group by b.dot,b.linno) k group by k.dot,k.linno  order by k.dot,k.linno "



            Else
                'sql = sql & "select dot, linno,count(cdepartment) over( partition by dot,linno order by dot,linno) cnt,nempno,vname,department,designation,cdepartment,csno from rrcolor.dbo.empdailysalary " _
                '            & " where dot>=@d1 and dot<=@d2 and (csno in (13,49,50,51,52,53,54) or csNo>=60 and csno<=85)  and linno>0 "

                sql = sql & "select  row_Number() over(order by k.dot,k.linno) sno, k.dot,k.linno,sum(k.attcnt) attcnt,sum(k.mascnt) mascnt,k.nempno,k.vname,k.department,k.designation,k.cdepartment,k.csno,k.subsection from ( " _
                            & " select b.dot, b.linno,count(b.cdepartment) over( partition by b.dot,b.linno order by b.dot,b.linno) attcnt,0 mascnt, b.nempno,b.vname,b.department, " _
                            & " b.designation,b.cdepartment,b.csno,c.subsection from rrcolor.dbo.empdailysalary b " _
                            & " left join rrcolor.dbo.empmaster c on c.nempno=b.nempno " _
                            & " where b.dot>=@d1 and b.dot<=@d2 and (b.csno in (13,49,50,51,52,53,54) or b.csNo>=60 and b.csno<=85) and b.linno>0 " _
                            & " union all " _
                            & " select b.dot,b.linno,0 attcnt,count(c.subsection) over( partition by b.dot,b.linno order by b.dot,b.linno) mascnt,b.nempno,b.vname,b.department, " _
                            & " b.designation,b.cdepartment,b.csno,c.subsection  from rrcolor.dbo.empdailysalary b " _
                            & " left join rrcolor.dbo.empmaster c on c.nempno=b.nempno " _
                            & " where rtrim(c.subdept)='STITCHING' and rtrim(c.subsection) in ('TAILOR','TAILOR(PIECE RATE)') and b.dot>=@d1 and b.dot<=@d2 and b.linno>0) k "


                If Convert.ToInt16(txtlinno.Text) > 0 Then
                    sql = sql & " where k.linno=" & Convert.ToInt16(txtlinno.Text)
                End If
                sql = sql & " group by k.dot,k.linno,k.nempno,k.vname,k.department,k.designation,k.cdepartment,k.csno,k.subsection  order by dot,linno"
            End If


            'checking tailor
            Dim mksql As String = "select k.dot,k.linno,sum(k.attcnt) attcnt,sum(k.mascnt) mascnt,sum(k.attcnt-k.mascnt) diff from ( " _
                            & "select dot, linno,count(cdepartment) attcnt,0 mascnt from rrcolor.dbo.empdailysalary where dot>=@d1 and dot<=@d2 " _
                            & "and (csno in (13,49,50,51,52,53,54) or csNo>=60 and csno<=85) and linno>0 group by dot,linno  " _
                            & " union all " _
                            & "select date,[lineno] linno,0 attcnt,nomac mascnt from prodcost.dbo.operf where date>@d1 and date<=@d2 " _
                            & ") k group by k.dot,k.linno  " _
                            & "having sum(k.attcnt-k.mascnt)<>0 " _
                            & " order by k.dot,k.linno "


            Dim dt4 As DataTable = getDataTable(sql)
                    ' mtot = 0
            If dt4.Rows.Count > 0 Then

                dg.DataSource = dt4

                dg.ColumnHeadersDefaultCellStyle.Font = New Font(DataGridView.DefaultFont, FontStyle.Bold)
                dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                        'For i = 0 To dg.Columns.Count - 1
                        '    '        dgsr.Columns(i).ReadOnly = True
                        '    If (i >= 6 And i <= 11) Or i = 13 Then
                        '        dg.Columns(i).ValueType = GetType(Int32)
                        '        dg.Columns(i).DefaultCellStyle.Format = ("0")
                        '        dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                        '    ElseIf i = 12 Then
                        '        dg.Columns(i).ValueType = GetType(Decimal)
                        '        dg.Columns(i).DefaultCellStyle.Format = ("0.00")
                        '        dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        '    ElseIf i = 4 Or i = 5 Then
                        '        dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        '    End If

                        '    '        If i > 1 Then
                        '    '            dgsr.Columns(i).ValueType = GetType(Decimal)
                        '    '            dgsr.Columns(i).DefaultCellStyle.Format = ("0.00")
                        '    '        End If
                        'Next

            Else
                dg.DataSource = Nothing
                    End If
        End If

    End Sub

    Private Sub loadpivot()
        sql2 = "DECLARE @cols AS NVARCHAR(MAX)," _
             & " @colsWithTotal AS NVARCHAR(MAX), " _
        & " @sql  AS NVARCHAR(MAX), " _
        & " @d1 AS NVARCHAR(20), " _
        & " @d2 AS NVARCHAR(20); " _
        & " SET @d1 = '" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "'; " _
        & " SET @d2 = '" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "'; " _
        & " SELECT @cols = STRING_AGG(QUOTENAME(CONVERT(VARCHAR(10), dot, 120)), ',') FROM ( " _
        & "    SELECT DISTINCT dot FROM rrcolor.dbo.empdailysalary  WHERE dot BETWEEN @d1 AND @d2) AS DateList; " _
        & " SELECT @colsWithTotal = STRING_AGG('ISNULL(' + QUOTENAME(CONVERT(VARCHAR(10), dot, 120)) + ', 0)', ' + ') FROM ( " _
        & "    SELECT DISTINCT dot FROM rrcolor.dbo.empdailysalary WHERE dot BETWEEN @d1 AND @d2 ) AS DateList; " _
        & " SET @sql = ' " _
        & " WITH BaseData AS ( " _
        & "    SELECT dot, linno, ''attcnt'' AS metric, COUNT(cdepartment) AS val  FROM rrcolor.dbo.empdailysalary " _
        & "        WHERE dot BETWEEN ''' + @d1 + ''' AND ''' + @d2 + '''  AND (csno IN (13,49,50,51,52,53,54) OR (csno BETWEEN 60 AND 85)) " _
        & "    AND linno > 0     GROUP BY dot, linno " _
        & " UNION ALL " _
        & " SELECT b.dot, b.linno, ''mascnt'' AS metric, COUNT(c.subsection) AS val  FROM rrcolor.dbo.empdailysalary b " _
        & " INNER JOIN rrcolor.dbo.empmaster c ON c.nempno = b.nempno " _
        & " WHERE RTRIM(c.subdept) = ''STITCHING''  AND RTRIM(c.subsection) IN (''TAILOR'', ''TAILOR(PIECE RATE)'') " _
        & " AND b.dot BETWEEN ''' + @d1 + ''' AND ''' + @d2 + '''  AND b.linno > 0  GROUP BY b.dot, b.linno) " _
        & " SELECT linno, metric, ' + @cols + ', Total = ' + @colsWithTotal + 'FROM BaseData " _
        & " PIVOT ( " _
        & "        SUM(val) FOR dot IN (' + @cols + ')) AS PivotResult " _
        & " ORDER BY linno, metric;'" _
        & " EXEC sp_executesql @sql; "

        Dim dt4 As DataTable = getDataTable(sql2)
        ' mtot = 0
        If dt4.Rows.Count > 0 Then

            dg.DataSource = dt4

            dg.ColumnHeadersDefaultCellStyle.Font = New Font(DataGridView.DefaultFont, FontStyle.Bold)
            dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            'For i = 0 To dg.Columns.Count - 1
            '    '        dgsr.Columns(i).ReadOnly = True
            '    If (i >= 6 And i <= 11) Or i = 13 Then
            '        dg.Columns(i).ValueType = GetType(Int32)
            '        dg.Columns(i).DefaultCellStyle.Format = ("0")
            '        dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            '    ElseIf i = 12 Then
            '        dg.Columns(i).ValueType = GetType(Decimal)
            '        dg.Columns(i).DefaultCellStyle.Format = ("0.00")
            '        dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '    ElseIf i = 4 Or i = 5 Then
            '        dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '    End If

            '    '        If i > 1 Then
            '    '            dgsr.Columns(i).ValueType = GetType(Decimal)
            '    '            dgsr.Columns(i).DefaultCellStyle.Format = ("0.00")
            '    '        End If
            'Next

        Else
            dg.DataSource = Nothing
        End If


    End Sub

    Private Sub Frmtailoratt_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width + 5
        Me.Height = My.Computer.Screen.Bounds.Height - 20
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Btnxl_Click(sender As System.Object, e As System.EventArgs) Handles Btnxl.Click
        gridexcelexport(dg, 1)
    End Sub



    '**efficiency
    '    select c.date, c.[lineno] linno,count(distinct b.empno) cnt,8 hourss,sum(b.sam) samm,c.totprodqty ,count(distinct b.empno)*8*60 totminatt,
    'sum(b.sam)*c.totprodqty totproducedmin,((sum(b.sam)*c.totprodqty)/(count(distinct b.empno)*8*60))*100 eff,t.attcnt nomac
    ' from perf1 b
    'inner join operf c on c.bno=b.bno
    'left join (select dot, linno,count(cdepartment) attcnt,0 mascnt from rrcolor.dbo.empdailysalary where dot>='2025-08-01' and dot<='2025-08-24'
    '                             and (csno in (13,49,50,51,52,53,54) or csNo>=60 and csno<=85) and linno>0 group by dot,linno) t on t.dot=c.date and c.[lineno]=t.linno 
    'where c.date>='2025-08-01' and c.date<='2025-08-24' and  c.[Lineno]='1'
    'group by c.date, c.totprodqty,c.[Lineno],t.attcnt
End Class
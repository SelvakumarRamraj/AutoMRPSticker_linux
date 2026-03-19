Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class FrmShirtdailydata

    Dim i, j, k, msel, o_id, n As Integer
    Dim MSQL, msql2, msql3, msql4, qry, dqry1, dqry2, merr As String

    Private Sub FrmShirtdailydata_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        If Butdel.Visible = True Then Butdel.Visible = False
        dgcmbload()
    End Sub


    ' Dim trans As OleDb.OleDbTransaction
    Private Sub Butdel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Butdel.Click
        msel = 3
        If MsgBox("Delete are U Sure!", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Call delrec()
        Else
            If Butdel.Visible = True Then Butdel.Visible = False
        End If
    End Sub

    Private Sub delrec()
        If MsgBox("Delete anyway !", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            msql4 = "delete  from  prodcost.dbo.Shirtdailydata where cdate='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"
            Try
                executeQuery(msql4)
                MsgBox("Deleted the Record Date " & Format(CDate(mskdate.Text), "yyyy-MM-dd"))
                If Butdel.Visible = True Then Butdel.Visible = False
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            If Butdel.Visible = True Then Butdel.Visible = False
        End If

    End Sub

    Private Sub butadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butadd.Click
        If Len(Trim(txtlineno.Text)) > 0 Then
            msel = 1
            n = dg.Rows.Add
            dg.CurrentCell = dg.Rows(n).Cells(2)
        Else
            MsgBox("Line No should not Empty!")
        End If

        'dg.Focus()

        'Call attload()
    End Sub


    Private Sub attload()
        If msel > 1 Then
            dg.Rows.Clear()
            'dg.Columns(2).Visible = True
            'dg.Columns(3).Visible = True
            Call dgcmbload()
            ' Dim data As DataTable = getDataTable("Select rtrim(dept) from rrcolor.dbo.departmentmast")

            msql4 = "select * from  prodcost.dbo.Shirtdailydata where cdate='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"
            Dim dt2 As DataTable = getDataTable(msql4)
            If dt2.Rows.Count - 1 > 0 Then
                MSQL = "select nempno,nemp_id,vname,cdepartment,csno,isnull(rate,0) rate,isnull(qty,0) qty, case when isnull(amt,0)=0 then case when isnull(qty,0)>0 then isnull(qty,0)* isnull(rate,0) else 0 end else amt end amt from  prodcost.dbo.Shirtdailydata where  cdate='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'  order by cdepartment,nempno "
                Dim dt As DataTable = getDataTable(MSQL)
                'Dg.Rows.Clear()
                'lblatt.Text = dt.Rows.Count
                For Each row As DataRow In dt.Rows
                    n = dg.Rows.Add
                    dg.Rows(n).Cells(0).Value = row("nempno")
                    dg.Rows(n).Cells(1).Value = row("nemp_id")
                    dg.Rows(n).Cells(2).Value = row("vname")
                    dg.Rows(n).Cells(3).Value = row("cdepartment")
                    dg.Rows(n).Cells(4).Value = row("csno")
                    dg.Rows(n).Cells(5).Value = Format(row("rate"), "######0.00")
                    dg.Rows(n).Cells(6).Value = row("Qty")
                    dg.Rows(n).Cells(7).Value = Format(row("amt"), "########0.00")
                Next
            Else
                'MSQL = "select b.nempno,b.nemp_id,b.vname,b.cdepartment,b.csno,c.rate from rrcolor.dbo.empDailysalary b " & vbCrLf _
                '      & " inner join prodcost.dbo.processratemast c on  c.nemp_id=b.nemp_id and c.nempno=b.nempno " & vbCrLf _
                '      & " where b.dot='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"

                'Dim dt As DataTable = getDataTable(MSQL)
                'For Each row As DataRow In dt.Rows
                '    n = dg.Rows.Add
                '    dg.Rows(n).Cells(0).Value = row("nempno")
                '    dg.Rows(n).Cells(1).Value = row("nemp_id")
                '    dg.Rows(n).Cells(2).Value = row("vname")
                '    dg.Rows(n).Cells(3).Value = row("cdepartment")
                '    dg.Rows(n).Cells(4).Value = row("csno")
                '    dg.Rows(n).Cells(5).Value = Format(row("rate"), "######0.00")
                '    dg.Rows(n).Cells(6).Value = 0
                'Next

            End If
        End If
    End Sub
    Private Sub dgcmbload()

        Dim data As DataTable = getDataTable("Select cdepartment as dept from prodcost.dbo.shirtprocessratemast ")
        ' n = Dg.Rows.Add
        'Dim cbCell As DataGridViewComboBoxCell = CType(Dg.Rows(0).Cells(14), DataGridViewComboBoxCell)
        CType(Me.dg.Columns(3), DataGridViewComboBoxColumn).Items.Clear()
        For Each row As DataRow In data.Rows
            ' cbCell.Items.Add(row(0).ToString)
            CType(Me.dg.Columns(3), DataGridViewComboBoxColumn).Items.Add(row(0))
        Next

    End Sub

    Private Sub saverec()

        If MsgBox("Save ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try

                'msql4 = "select *  from  prodcost.dbo.dailycontractdata where cdate='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"
                'Dim dt3 As DataTable = getDataTable(msql4)

                For i = 0 To dg.Rows.Count - 1
                    msql4 = "select *  from  prodcost.dbo.shirtdailydata where cdate='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' and nempno=" & Val(dg.Rows(i).Cells(0).Value) & " and cdepartment='" & dg.Rows(i).Cells(3).Value & "'"
                    Dim dt4 As DataTable = getDataTable(msql4)
                    If dt4.Rows.Count > 0 Then
                        msql3 = "update  prodcost.dbo.shirtdailydata set cdepartment='" & dg.Rows(i).Cells(3).Value & "',csno=" & Val(dg.Rows(i).Cells(4).Value) & ",rate=" & Val(dg.Rows(i).Cells(5).Value) & " " & vbCrLf _
                               & ",qty=" & Val(dg.Rows(i).Cells(6).Value) & ",amt=" & Val(dg.Rows(i).Cells(7).Value) & "linno='" & Trim(txtlineno.Text) & "'  where cdate='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' and nempno=" & Val(dg.Rows(i).Cells(0).Value)
                        'executeQuery(msql3)
                    Else
                        msql3 = "insert into  prodcost.dbo.shirtdailydata (nempno,nemp_id,cdate,vname,cdepartment,csno,rate,qty,amt,linno) values (" & Val(dg.Rows(i).Cells(0).Value) & "," & Val(dg.Rows(i).Cells(1).Value) & ",'" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'," & vbCrLf _
                                & "'" & Trim(dg.Rows(i).Cells(2).Value) & "','" & Trim(dg.Rows(i).Cells(3).Value) & "'," & Val(dg.Rows(i).Cells(4).Value) & "" & vbCrLf _
                                & "," & Val(dg.Rows(i).Cells(5).Value) & "," & Val(dg.Rows(i).Cells(6).Value) & "," & (Val(dg.Rows(i).Cells(5).Value) * Val(dg.Rows(i).Cells(6).Value)) & ",'" & Trim(txtlineno.Text) & "')"
                        'executeQuery(msql2)
                    End If
                    Try
                        executeQuery(msql3)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                Next i
                MsgBox("Saved Sucessfully!")
                butclear.PerformClick()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If


        '    If dt3.Rows.Count - 1 > 0 Then
        '        For k = 0 To dg.Rows.Count - 1
        '            msql3 = "update  prodcost.dbo.dailycontractdata set cdepartment='" & dg.Rows(k).Cells(3).Value & "',csno=" & Val(dg.Rows(k).Cells(4).Value) & ",rate=" & Val(dg.Rows(k).Cells(5).Value) & " " & vbCrLf _
        '                    & ",qty=" & Val(dg.Rows(k).Cells(6).Value) & ",amt=" & Val(dg.Rows(k).Cells(7).Value) & "  where cdate='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' and nempno=" & Val(dg.Rows(k).Cells(0).Value)
        '            executeQuery(msql3)
        '        Next k
        '        MsgBox("Updated Sucessfully!")
        '        dg.Rows.Clear()
        '        'lblatt.Text = 0
        '    Else

        '        For i = 0 To dg.Rows.Count - 1
        '            msql2 = "insert into  prodcost.dbo.dailycontractdata (nempno,nemp_id,cdate,vname,cdepartment,csno,rate,qty,amt) values (" & Val(dg.Rows(i).Cells(0).Value) & "," & Val(dg.Rows(i).Cells(1).Value) & ",'" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'," & vbCrLf _
        '                    & "'" & Trim(dg.Rows(i).Cells(2).Value) & "','" & Trim(dg.Rows(i).Cells(3).Value) & "'," & Val(dg.Rows(i).Cells(4).Value) & "" & vbCrLf _
        '                    & "," & Val(dg.Rows(i).Cells(5).Value) & "," & Val(dg.Rows(i).Cells(6).Value) & "," & (Val(dg.Rows(i).Cells(5).Value) * Val(dg.Rows(i).Cells(6).Value)) & ")"

        '            '& ",'" & Trim(dg2.Rows(i).Cells(9).Value) & "'," & Val(dg2.Rows(i).Cells(10).Value) & "," & Val(dg2.Rows(i).Cells(11).Value) & ",'" & dg2.Rows(i).Cells(12).Value & "'" & ")"
        '            executeQuery(msql2)
        '        Next i
        '        MsgBox("Saved Sucessfully!")
        '        dg.Rows.Clear()
        '        'lblatt.Text = 0
        '    End If

        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '    End Try
        'End If

    End Sub

    Private Sub saverec3()

        If MsgBox("Save ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try

                msql4 = "select *  from  prodcost.dbo.shirtdailydata where cdate='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"
                'executeQuery(msql4)
                Dim dt3 As DataTable = getDataTable(msql4)

                If dt3.Rows.Count - 1 > 0 Then
                    For k = 0 To dg.Rows.Count - 1
                        msql3 = "update  prodcost.dbo.shirtdailydata set cdepartment='" & dg.Rows(k).Cells(3).Value & "',csno=" & Val(dg.Rows(k).Cells(4).Value) & ",rate=" & Val(dg.Rows(k).Cells(5).Value) & " " & vbCrLf _
                                & ",qty=" & Val(dg.Rows(k).Cells(6).Value) & ",amt=" & Val(dg.Rows(k).Cells(7).Value) & ",linno='" & Trim(txtlineno.Text) & "'  where cdate='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' and nempno=" & Val(dg.Rows(k).Cells(0).Value) & " and cdepartment='" & dg.Rows(k).Cells(3).Value & "'"
                        executeQuery(msql3)
                    Next k
                    MsgBox("Updated Sucessfully!")
                    dg.Rows.Clear()
                    'lblatt.Text = 0
                Else

                    For i = 0 To dg.Rows.Count - 1
                        msql2 = "insert into  prodcost.dbo.shirtdailydata (nempno,nemp_id,cdate,vname,cdepartment,csno,rate,qty,amt,linno) values (" & Val(dg.Rows(i).Cells(0).Value) & "," & Val(dg.Rows(i).Cells(1).Value) & ",'" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'," & vbCrLf _
                                & "'" & Trim(dg.Rows(i).Cells(2).Value) & "','" & Trim(dg.Rows(i).Cells(3).Value) & "'," & Val(dg.Rows(i).Cells(4).Value) & "" & vbCrLf _
                                & "," & Val(dg.Rows(i).Cells(5).Value) & "," & Val(dg.Rows(i).Cells(6).Value) & "," & (Val(dg.Rows(i).Cells(5).Value) * Val(dg.Rows(i).Cells(6).Value)) & ",'" & Trim(txtlineno.Text) & "')"

                        '& ",'" & Trim(dg2.Rows(i).Cells(9).Value) & "'," & Val(dg2.Rows(i).Cells(10).Value) & "," & Val(dg2.Rows(i).Cells(11).Value) & ",'" & dg2.Rows(i).Cells(12).Value & "'" & ")"
                        executeQuery(msql2)
                    Next i
                    MsgBox("Saved Sucessfully!")
                    dg.Rows.Clear()
                    'lblatt.Text = 0
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub


    Private Sub saverec2()

        If MsgBox("Save ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Cursor = Cursors.WaitCursor
            Try
                For j = 0 To dg.Rows.Count - 1
                    msql2 = "insert into  prodcost.dbo.shirtdailydata (nempno,nemp_id,cdate,vname,cdepartment,csno,rate,qty,amt,linno) values (" & Val(dg.Rows(j).Cells(0).Value) & "," & Val(dg.Rows(j).Cells(1).Value) & ",'" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'," & vbCrLf _
                        & "'" & Trim(dg.Rows(j).Cells(2).Value) & "','" & Trim(dg.Rows(j).Cells(3).Value) & "'," & Val(dg.Rows(j).Cells(4).Value) & "" & vbCrLf _
                        & "," & Val(dg.Rows(j).Cells(5).Value) & "," & Val(dg.Rows(j).Cells(6).Value) & "," & (Val(dg.Rows(j).Cells(5).Value) * Val(dg.Rows(j).Cells(6).Value)) & ",'" & Trim(txtlineno.Text) & "')"
                    executeQuery(msql2)
                    'MsgBox("Updated Sucessfully!")
                    'dg.Rows.Clear()
                Next j

                MsgBox("Add Sucessfully!")
                dg.Rows.Clear()
                ''lblatt.Text = 0
                '    Else

                'For i = 0 To dg.Rows.Count - 1
                '    msql2 = "insert into  prodcost.dbo.dailycontractdata (nempno,nemp_id,cdate,vname,cdepartment,csno,rate,qty,amt) values (" & Val(dg.Rows(i).Cells(0).Value) & "," & Val(dg.Rows(i).Cells(1).Value) & ",'" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'," & vbCrLf _
                '            & "'" & Trim(dg.Rows(i).Cells(2).Value) & "','" & Trim(dg.Rows(i).Cells(3).Value) & "'," & Val(dg.Rows(i).Cells(4).Value) & "" & vbCrLf _
                '            & "," & Val(dg.Rows(i).Cells(5).Value) & "," & Val(dg.Rows(i).Cells(6).Value) & "," & (Val(dg.Rows(i).Cells(5).Value) * Val(dg.Rows(i).Cells(6).Value)) & ")"

                '    '& ",'" & Trim(dg2.Rows(i).Cells(9).Value) & "'," & Val(dg2.Rows(i).Cells(10).Value) & "," & Val(dg2.Rows(i).Cells(11).Value) & ",'" & dg2.Rows(i).Cells(12).Value & "'" & ")"
                '    executeQuery(msql2)
                'Next i
                'MsgBox("Saved Sucessfully!")
                'dg.Rows.Clear()
                ''lblatt.Text = 0
                '    End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            Cursor = Cursors.Default
        End If

    End Sub

    Private Sub butsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butsave.Click
        If Len(Trim(txtlineno.Text)) > 0 Then
            Call saverec()
        Else
            MsgBox("Line No should not Empty!")
        End If

    End Sub

    Private Sub mskdate_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles mskdate.KeyUp
        If e.Control And e.Alt And e.KeyCode = Keys.F10 Then

            If Butdel.Visible = False Then Butdel.Visible = True
        End If
    End Sub

    Private Sub mskdate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskdate.MaskInputRejected

    End Sub

    Private Sub butexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butexit.Click
        Me.Close()
    End Sub

    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick
        If e.ColumnIndex = 0 Then
            Dim OBJ As New FrmempCFL
            OBJ.ShowDialog()
            If Len(Trim(mempno)) > 0 Then
                dg.Rows(e.RowIndex).Cells(1).Value = mempid
                dg.Rows(e.RowIndex).Cells(0).Value = mempno
                dg.Rows(e.RowIndex).Cells(2).Value = mempname
                'dg.Rows(e.RowIndex).Cells(24).Value = mempsalary
                'dg.Rows(e.RowIndex).Cells(26).Value = mjbgrade
                dg.CurrentCell = dg.Rows(e.RowIndex).Cells(3)
                dg.BeginEdit(False)
            End If
            OBJ.Close()
        End If
    End Sub

    Private Sub dg_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellEndEdit
        If e.ColumnIndex = 6 Then
            If Val(dg.Rows(e.RowIndex).Cells(6).Value) > 0 Then
                dg.Rows(e.RowIndex).Cells(7).Value = Format(Val(dg.Rows(e.RowIndex).Cells(6).Value) * Val(dg.Rows(e.RowIndex).Cells(5).Value), "#####0.00")
            Else
                dg.Rows(e.RowIndex).Cells(7).Value = 0
            End If
        End If

        If e.ColumnIndex = 5 Then
            If Val(dg.Rows(e.RowIndex).Cells(5).Value) > 0 And Val(dg.Rows(e.RowIndex).Cells(6).Value) > 0 Then
                dg.Rows(e.RowIndex).Cells(7).Value = Format(Val(dg.Rows(e.RowIndex).Cells(6).Value) * Val(dg.Rows(e.RowIndex).Cells(5).Value), "#####0.00")
            Else
                dg.Rows(e.RowIndex).Cells(7).Value = 0
            End If
        End If
        If e.ColumnIndex = 0 Then
            dg.Rows(e.RowIndex).Cells(1).Value = getempid(Val(dg.Rows(e.RowIndex).Cells(0).Value))
        End If
        If e.ColumnIndex = 1 Then
            dg.Rows(e.RowIndex).Cells(2).Value = getempnam(Val(dg.Rows(e.RowIndex).Cells(0).Value))
        End If

        If e.ColumnIndex = 4 Then

        End If

    End Sub
    Private Function getempnam(ByVal empid As Integer) As String
        Dim mpnam As String
        MSQL = "select vname from rrcolor.dbo.empmaster  where nempno=" & empid
        Dim dt As DataTable = getDataTable(MSQL)
        If dt.Rows.Count > 0 Then
            'For Each row As DataRow In dt.Rows

            'Next
            mpnam = dt.Rows(0)(0)
        Else
            mpnam = ""
        End If
        Return mpnam
    End Function

    Private Sub txtlineno_TextChanged(sender As Object, e As EventArgs) Handles txtlineno.TextChanged

    End Sub

    Private Function getempid(ByVal empid As Integer) As Integer
        Dim mpid As Integer
        MSQL = "select nemp_id from rrcolor.dbo.empmaster  where nempno=" & empid
        Dim dt As DataTable = getDataTable(MSQL)
        If dt.Rows.Count > 0 Then
            'For Each row As DataRow In dt.Rows

            'Next
            mpid = dt.Rows(0)(0)
        Else
            mpid = 0
        End If
        Return mpid
    End Function
    Private Function getdeptid(ByVal dept As String) As Integer
        Dim mpid As Integer
        MSQL = "select csno from prodcost.dbo.shirtprocessratemast  where cdepartment='" & dept & "'"
        Dim dt As DataTable = getDataTable(MSQL)
        If dt.Rows.Count > 0 Then
            'For Each row As DataRow In dt.Rows

            'Next
            mpid = dt.Rows(0)(0)
        Else
            mpid = 0
        End If
        Return mpid
    End Function

    Private Function getdeptrate(ByVal mcsno As Integer) As Single
        Dim mrate As Single
        MSQL = "select rate from prodcost.dbo.shirtprocessratemast  where csno=" & mcsno
        Dim dt As DataTable = getDataTable(MSQL)
        If dt.Rows.Count > 0 Then
            'For Each row As DataRow In dt.Rows

            'Next
            mrate = dt.Rows(0)(0)
        Else
            mrate = 0
        End If
        Return mrate
    End Function

    Private Sub dg_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellEnter
        If e.ColumnIndex = 0 Then
            Dim OBJ As New FrmempCFL
            OBJ.ShowDialog()
            If Len(Trim(mempno)) > 0 Then
                dg.Rows(e.RowIndex).Cells(0).Value = mempno
                dg.Rows(e.RowIndex).Cells(1).Value = mempid
                'dg.Rows(e.RowIndex).Cells(0).Value = mempno
                dg.Rows(e.RowIndex).Cells(2).Value = mempname
                SendKeys.Send("{TAB}")
                SendKeys.Send("{TAB}")
                'dg.Rows(e.RowIndex).Cells(24).Value = mempsalary
                'dg.Rows(e.RowIndex).Cells(26).Value = mjbgrade
                'dg.CurrentCell = dg.Rows(e.RowIndex).Cells(2)
                'dg.BeginEdit(False)
            End If
            'OBJ.Close()
        End If

        'If e.ColumnIndex = 2 Then
        '    dg.Rows(e.RowIndex).Cells(2).Value = getempnam(Val(dg.Rows(e.RowIndex).Cells(0).Value))
        'End If

        If e.ColumnIndex = 4 Then
            dg.Rows(e.RowIndex).Cells(4).Value = getdeptid(dg.Rows(e.RowIndex).Cells(3).Value)
            dg.Rows(e.RowIndex).Cells(5).Value = getdeptrate(Val(dg.Rows(e.RowIndex).Cells(4).Value))
            SendKeys.Send("{TAB}")
            SendKeys.Send("{TAB}")
        End If

        If e.ColumnIndex = 6 Then
            dg.BeginEdit(True)
        End If
    End Sub

    Private Sub dg_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dg.CellMouseDoubleClick
        If e.ColumnIndex = 0 Then
            Dim OBJ As New FrmempCFL
            OBJ.ShowDialog()
            If Len(Trim(mempno)) > 0 Then
                dg.Rows(e.RowIndex).Cells(0).Value = mempno
                dg.Rows(e.RowIndex).Cells(1).Value = mempid
                dg.Rows(e.RowIndex).Cells(2).Value = mempname
                'dg.Rows(e.RowIndex).Cells(24).Value = mempsalary
                'dg.Rows(e.RowIndex).Cells(26).Value = mjbgrade
                dg.CurrentCell = dg.Rows(e.RowIndex).Cells(3)
                dg.BeginEdit(False)
            End If
            OBJ.Close()
        End If
    End Sub

    Private Sub dg_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dg.DataError
        If (e.Exception.Message = "DataGridViewComboBoxCell value is not valid.") Then
            Dim value As Object = dg.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            If Not CType(dg.Columns(e.ColumnIndex), DataGridViewComboBoxColumn).Items.Contains(value) Then
                CType(dg.Columns(e.ColumnIndex), DataGridViewComboBoxColumn).Items.Add(value)
                e.ThrowException = False
            End If

        End If
    End Sub

    Private Sub butclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butclear.Click
        dg.Rows.Clear()
    End Sub
    Private Sub addrw()
        'If msel = 1 Or msel = 2 Then
        dg.Rows.Add()

        If (dg.RowCount - 1) > 0 Then
            'If dg.Rows(dg.RowCount - 1).Cells(0).Value = 0 Then
            dg.Rows(dg.RowCount - 1).Cells(0).Value = dg.Rows(dg.RowCount - 2).Cells(0).Value + 1
        Else
            'dg.Rows(dg.RowCount - 1).Cells(19).Value = txtwrkhour.Text
            dg.Rows(dg.RowCount - 1).Cells(0).Value = 1

        End If
        'End If
    End Sub

    Private Sub dg_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dg.KeyDown
        If e.KeyCode = Keys.F2 Then

            n = dg.Rows.Add()
            'dg.Columns(n).ReadOnly = False
            'If (dg.RowCount - 1) > 0 Then
            '    'If dg.Rows(dg.RowCount - 1).Cells(0).Value = 0 Then
            '    dg.Rows(dg.RowCount - 1).Cells(0).Value = dg.Rows(dg.RowCount - 2).Cells(0).Value + 1
            '    'dg.Columns(n).ReadOnly = False
            '    'dg.CurrentCell = dg.Rows(dg.CurrentRow.Index + 1).Cells(1)
            '    ' dg.CurrentCell = dg.Rows(dg.CurrentCell.RowIndex + 1).Cells(1)
            'Else
            '    'dg.Rows(dg.RowCount - 1).Cells(19).Value = txtwrkhour.Text
            '    dg.Rows(dg.RowCount - 1).Cells(0).Value = 1
            'End If
            dg.CurrentCell = dg.Rows(n).Cells(0)

            'dg.Columns(n).ReadOnly = False
            'dg.BeginEdit(True)
        End If

        If e.KeyCode = Keys.F5 Then
            Dim OBJ As New FrmempCFL
            OBJ.ShowDialog()
            If Len(Trim(mempno)) > 0 Then
                dg.Rows(dg.CurrentCell.RowIndex).Cells(1).Value = mempid
                dg.Rows(dg.CurrentCell.RowIndex).Cells(0).Value = mempno
                dg.Rows(dg.CurrentCell.RowIndex).Cells(2).Value = mempname
                'dg.Rows(dg.CurrentCell.RowIndex).Cells(24).Value = mempsalary
                'dg.Rows(dg.CurrentCell.RowIndex).Cells(26).Value = mjbgrade
                dg.CurrentCell = dg.Rows(dg.CurrentCell.RowIndex).Cells(3)
                dg.BeginEdit(False)
            End If
        End If



        If e.KeyCode = Keys.F9 Then
            dg.Rows.RemoveAt(dg.CurrentRow.Index)
        End If
    End Sub

    Private Sub butdisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butdisp.Click
        dg.Rows.Clear()
        'dg.Columns(2).Visible = True
        'dg.Columns(3).Visible = True
        Call dgcmbload()


        MSQL = "select b.nempno,b.nemp_id,b.vname,b.cdepartment,b.csno,c.rate from rrcolor.dbo.empDailysalary b " & vbCrLf _
              & " left join prodcost.dbo.shirtprocessratemast c on c.department=b.cdepartment and c.csno=b.csno " & vbCrLf _
              & " where b.dot='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' and b.nempno not in (select nempno from prodcost.dbo.shirtdailydata)  and b.csno>=49 " & vbCrLf

        Dim dt As DataTable = getDataTable(MSQL)
        For Each row As DataRow In dt.Rows
            n = dg.Rows.Add
            dg.Rows(n).Cells(0).Value = row("nempno")
            dg.Rows(n).Cells(1).Value = row("nemp_id")
            dg.Rows(n).Cells(2).Value = row("vname")
            dg.Rows(n).Cells(3).Value = row("cdepartment")
            dg.Rows(n).Cells(4).Value = row("csno")
            dg.Rows(n).Cells(5).Value = Format(row("rate"), "######0.00")
            dg.Rows(n).Cells(6).Value = 0
        Next

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call saverec2()

    End Sub

    Private Sub Btnedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnedit.Click
        msel = 2
        Call attload()
    End Sub

End Class
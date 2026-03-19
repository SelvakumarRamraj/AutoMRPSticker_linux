Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class Frmprocessratemast
    Dim i, j, msel, msel2, o_id, n As Integer
    Dim MSQL, msql2, msql3, msql4, qry, dqry1, dqry2, merr As String
    Dim trans As SqlTransaction

    Private Sub Frmprocessratemast_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        Call loadcombo2()
    End Sub
    Private Sub loadcombo2()
        cmbprocname.Items.Clear()
        msql4 = "select dept,sno from rrcolor.dbo.departmentmast where sno>=49  order by sno"
        Dim dtt As DataTable = getDataTable(msql4)
        cmbprocname.DataSource = dtt
        cmbprocname.DisplayMember = "dept"
        cmbprocname.ValueMember = "sno"
        'dtt.Dispose()
    End Sub
    Private Sub loaddata()
        dg.Rows.Clear()
        Cursor = Cursors.WaitCursor
        dgcmbload()
        msel = 1
        'MSQL = "select nempno,nemp_id,vname,cdepartment,csno from rrcolor.dbo.empmaster  where csno in(" & Trim(mcontprocfilt) & " ) and nempno>=12000000 and nempno not in (select nempno from Prodcost.dbo.processratemast) "

        MSQL = "select k.nempno,k.nemp_id,k.vname,k.cdepartment,k.csno,isnull(d.rate,0) rate from (" _
                & " select nempno,nemp_id,vname,cdepartment,csno from rrcolor.dbo.empmaster  b " _
                & " where csno in( " & Trim(mcontprocfilt) & ") and  " _
                & " nempno>=12000000 and nempno not in (select nempno from Prodcost.dbo.processratemast)) k " _
                & " inner join prodcost.dbo.processitemratemast d on d.csno=k.csno "


        Dim dt As DataTable = getDataTable(MSQL)
        For Each row As DataRow In dt.Rows
            n = dg.Rows.Add
            dg.Rows(n).Cells(0).Value = row("nempno")
            dg.Rows(n).Cells(1).Value = row("nemp_id")
            dg.Rows(n).Cells(2).Value = row("vname")
            dg.Rows(n).Cells(3).Value = row("cdepartment")
            dg.Rows(n).Cells(4).Value = row("csno")
            dg.Rows(n).Cells(5).Value = row("rate")
        Next
        Cursor = Cursors.Default
        msel = 1
    End Sub
    Private Sub saverec(ByVal msav As Integer)
        If msav = 1 Then
            Try
                For i = 0 To dg.Rows.Count - 1
                    ' nempno, nemp_id, vname, cdepartment, csno, Rate
                    msql4 = "insert into prodcost.dbo.processratemast(nempno,nemp_id,vname,cdepartment,csno,rate)" & vbCrLf _
                       & "values(" & Val(dg.Rows(i).Cells(0).Value) & "," & Val(dg.Rows(i).Cells(1).Value) & ",'" & dg.Rows(i).Cells(2).Value & "','" & dg.Rows(i).Cells(3).Value & "'" & vbCrLf _
                       & "," & Val(dg.Rows(i).Cells(4).Value) & "," & Val(dg.Rows(i).Cells(5).Value) & ")"


                    executeQuery(msql4)
                Next
                'executeQuery(msql4)
                MsgBox("Added! Sucessfully")
                dg.Rows.Clear()
            Catch ex As Exception
                MsgBox("Error on Add Record - " & ex.Message)
            End Try

        End If

        If msav > 1 Then
            'Cursor = Cursors.WaitCursor
            'Try

            '    For i = 0 To dg.Rows.Count - 1
            '        msql2 = "update prodcost.dbo.processratemast set daysalary=" & Val(dg.Rows(i).Cells(12).Value) & ",totsalary=" & Val(dg.Rows(i).Cells(13).Value) & ",cdepartment='" & dg.Rows(i).Cells(14).Value & "',csno=" & Val(dg.Rows(i).Cells(15).Value) & " where nempno=" & Val(dg.Rows(i).Cells(0).Value)
            '        executeQuery(msql2)
            '    Next i
            '    MsgBox("Updated Sucessfully!")
            '    dg.Rows.Clear()
            'Catch ex As Exception
            '    MsgBox("Err on Update - " & ex.Message)
            'End Try

            '***
            Cursor = Cursors.WaitCursor
            Try

                For i = 0 To dg.Rows.Count - 1

                    If dg.Rows(i).DefaultCellStyle.BackColor = Color.Green Then
                        msql2 = "update prodcost.dbo.processratemast set cdepartment='" & dg.Rows(i).Cells(3).Value & "',csno=" & Val(dg.Rows(i).Cells(4).Value) & ",rate=" & Val(dg.Rows(i).Cells(5).Value) & " where nempno=" & Val(dg.Rows(i).Cells(0).Value)
                        executeQuery(msql2)
                    End If
                Next i
                MsgBox("Updated Sucessfully!")
                dg.Rows.Clear()
            Catch ex As Exception
                MsgBox("Err on Update - " & ex.Message)
            End Try
            'Cursor = Cursors.Default

            Cursor = Cursors.Default




            'Cursor = Cursors.Default
        End If
        Cursor = Cursors.Default

    End Sub


    Private Sub loaddisp()
        MSQL = MSQL = "select nempno,nemp_id,vname,cdepartment,csno,rate from  Prodcost.dbo.processratemast)"
        Dim dt As DataTable = getDataTable(MSQL)
        For Each row As DataRow In dt.Rows
            n = dg.Rows.Add
            dg.Rows(n).Cells(0).Value = row("nempno")
            dg.Rows(n).Cells(1).Value = row("nemp_id")
            dg.Rows(n).Cells(2).Value = row("vname")
            dg.Rows(n).Cells(3).Value = row("cdepartment")
            dg.Rows(n).Cells(4).Value = row("csno")
            dg.Rows(n).Cells(5).Value = 0
        Next
        Cursor = Cursors.Default
        msel = 1
    End Sub
    Private Sub loaddisp2()
        dg1.Rows.Clear()
        MSQL = "select cdepartment,csno,rate from  Prodcost.dbo.processitemratemast"
        Dim dt As DataTable = getDataTable(MSQL)
        For Each row As DataRow In dt.Rows
            n = dg1.Rows.Add
            dg1.Rows(n).Cells(0).Value = row("cdepartment")
            dg1.Rows(n).Cells(1).Value = row("csno")
            dg1.Rows(n).Cells(2).Value = row("rate")
        Next
        Cursor = Cursors.Default
        'msel = 1
    End Sub

    Private Sub butexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butexit.Click
        Me.Close()
    End Sub

    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick
        'If e.ColumnIndex = 0 Then
        '    Dim OBJ As New FrmempCFLnew
        '    OBJ.ShowDialog()
        '    If Len(Trim(mempno)) > 0 Then
        '        dg.Rows(e.RowIndex).Cells(0).Value = mempno
        '        dg.Rows(e.RowIndex).Cells(1).Value = mempid
        '        dg.Rows(e.RowIndex).Cells(2).Value = mempname
        '        dg.Rows(e.RowIndex).Cells(3).Value = mprocnam
        '        dg.Rows(e.RowIndex).Cells(4).Value = mprsno
        '        'dg.Rows(e.RowIndex).Cells(24).Value = mempsalary
        '        'dg.Rows(e.RowIndex).Cells(26).Value = mjbgrade
        '        'dg.CurrentCell = dg.Rows(e.RowIndex).Cells(4)
        '        dg.BeginEdit(False)
        '    End If
        '    OBJ.Close()
        'End If
    End Sub

    Private Sub dg_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellEndEdit
        If e.RowIndex >= 0 Then
            If Len(Trim(dg.Rows(e.RowIndex).Cells(3).Value)) > 0 Then
                dg.Rows(e.RowIndex).Cells(4).Value = getsno(dg.Rows(e.RowIndex).Cells(3).Value)
                ' SendKeys.Send("{Tab}")
            End If
            dg.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Green
        End If
    End Sub

    Private Sub dg_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellEnter
        'If e.ColumnIndex = 0 Then
        '    Dim OBJ As New FrmempCFLnew
        '    OBJ.ShowDialog()
        '    If Len(Trim(mempno)) > 0 Then
        '        dg.Rows(e.RowIndex).Cells(0).Value = mempno
        '        dg.Rows(e.RowIndex).Cells(1).Value = mempid
        '        dg.Rows(e.RowIndex).Cells(2).Value = mempname
        '        dg.Rows(e.RowIndex).Cells(3).Value = mprocnam
        '        dg.Rows(e.RowIndex).Cells(4).Value = mprsno
        '        'dg.Rows(e.RowIndex).Cells(24).Value = mempsalary
        '        'dg.Rows(e.RowIndex).Cells(26).Value = mjbgrade
        '        'dg.CurrentCell = dg.Rows(e.RowIndex).Cells(4)
        '        'dg.BeginEdit(False)
        '    End If
        '    OBJ.Close()
        'End If
    End Sub

    Private Sub dg_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dg.CellMouseDoubleClick
        'If e.ColumnIndex = 0 Then
        '    Dim OBJ As New FrmempCFLnew
        '    OBJ.ShowDialog()
        '    If Len(Trim(mempno)) > 0 Then
        '        dg.Rows(e.RowIndex).Cells(0).Value = mempno
        '        dg.Rows(e.RowIndex).Cells(1).Value = mempid
        '        dg.Rows(e.RowIndex).Cells(2).Value = mempname
        '        dg.Rows(e.RowIndex).Cells(3).Value = mprocnam
        '        dg.Rows(e.RowIndex).Cells(4).Value = mprsno
        '        'dg.Rows(e.RowIndex).Cells(24).Value = mempsalary
        '        'dg.Rows(e.RowIndex).Cells(26).Value = mjbgrade
        '        'dg.CurrentCell = dg.Rows(e.RowIndex).Cells(4)
        '        dg.BeginEdit(False)
        '    End If
        '    OBJ.Close()
        'End If
    End Sub

    Private Sub dg_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dg.DataError
        If (e.Context _
                = (DataGridViewDataErrorContexts.Formatting Or DataGridViewDataErrorContexts.PreferredSize)) Then
            e.ThrowException = False
        End If

        If (e.Exception.Message = "DataGridViewComboBoxCell value is not valid.") Then
            Dim value As Object = dg.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            If Not CType(dg.Columns(e.ColumnIndex), DataGridViewComboBoxColumn).Items.Contains(value) Then
                CType(dg.Columns(e.ColumnIndex), DataGridViewComboBoxColumn).Items.Add(value)
                e.ThrowException = False
            End If

        End If
    End Sub

    Private Sub butadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butadd.Click
        msel = 1
        Call loaddata()
    End Sub

    Private Sub dgcmbload()
       
        Dim data As DataTable = getDataTable("Select dept from rrcolor.dbo.departmentmast where sno>=49 ")
        ' n = Dg.Rows.Add
        'Dim cbCell As DataGridViewComboBoxCell = CType(Dg.Rows(0).Cells(14), DataGridViewComboBoxCell)
        CType(Me.dg.Columns(3), DataGridViewComboBoxColumn).Items.Clear()
        For Each row As DataRow In data.Rows
            ' cbCell.Items.Add(row(0).ToString)
            CType(Me.dg.Columns(3), DataGridViewComboBoxColumn).Items.Add(row(0))
        Next

    End Sub

    Private Function getsno(ByVal dept As String) As Integer
        Dim nn As Integer
        Dim ssql As String
        ssql = "select sno from rrcolor.dbo.departmentmast where sno>=49 and dept='" & Trim(dept) & "'"
        Dim dtt As DataTable = getDataTable(ssql)
        If dtt.Rows.Count > 0 Then
            For Each crow As DataRow In dtt.Rows
                nn = crow("sno")
            Next
        End If
        Return nn
    End Function

    Private Sub butsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butsave.Click
        Call saverec(msel)
    End Sub

    Private Sub butdisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butdisp.Click
        dg.Rows.Clear()
        Cursor = Cursors.WaitCursor
        dgcmbload()
        msel = 2
        MSQL = "select nempno,nemp_id,vname,cdepartment,csno,rate from  Prodcost.dbo.processratemast"
        Dim dt As DataTable = getDataTable(MSQL)
        For Each row As DataRow In dt.Rows
            n = dg.Rows.Add
            dg.Rows(n).Cells(0).Value = row("nempno")
            dg.Rows(n).Cells(1).Value = row("nemp_id")
            dg.Rows(n).Cells(2).Value = row("vname")
            dg.Rows(n).Cells(3).Value = row("cdepartment")
            dg.Rows(n).Cells(4).Value = row("csno")
            dg.Rows(n).Cells(5).Value = row("rate")
        Next
        Cursor = Cursors.Default
        msel = 2
    End Sub




    Private Sub saveadd(ByVal msav As Integer)
        If msav = 1 Then
            Try
                ' nempno, nemp_id, vname, cdepartment, csno, Rate
                If Len(Trim(cmbprocname.Text)) > 0 Then
                    msql4 = "insert into prodcost.dbo.processitemratemast(cdepartment,csno,rate)" & vbCrLf _
                       & " values( '" & Trim(cmbprocname.Text) & "'," & cmbprocname.SelectedValue & "," & Val(txtrate.Text) & ")"
                    '& "," & Val(dg.Rows(i).Cells(4).Value) & "," & Val(dg.Rows(i).Cells(5).Value) & ")"
                    executeQuery(msql4)
                    'executeQuery(msql4)
                    MsgBox("Added! Sucessfully")
                End If
            Catch ex As Exception
                MsgBox("Error on Add Record - " & ex.Message)
            End Try

        End If

        'If msav > 1 Then

        '    '***
        '    Cursor = Cursors.WaitCursor
        '    Try

        '        For i = 0 To dg.Rows.Count - 1

        '            If dg.Rows(i).DefaultCellStyle.BackColor = Color.Green Then
        '                msql2 = "update prodcost.dbo.processratemast set cdepartment='" & dg.Rows(i).Cells(3).Value & "',csno=" & Val(dg.Rows(i).Cells(4).Value) & ",rate=" & Val(dg.Rows(i).Cells(5).Value) & " where nempno=" & Val(dg.Rows(i).Cells(0).Value)
        '                executeQuery(msql2)
        '            End If
        '        Next i
        '        MsgBox("Updated Sucessfully!")
        '        dg.Rows.Clear()
        '    Catch ex As Exception
        '        MsgBox("Err on Update - " & ex.Message)
        '    End Try
        '    'Cursor = Cursors.Default

        '    Cursor = Cursors.Default




        '    'Cursor = Cursors.Default
        'End If
        Cursor = Cursors.Default

    End Sub
    Private Sub updtrec()
        '***
        Cursor = Cursors.WaitCursor
        Try

            For i = 0 To dg1.Rows.Count - 1

                If dg1.Rows(i).DefaultCellStyle.BackColor = Color.Green Then
                    msql2 = "update prodcost.dbo.processitemratemast set cdepartment='" & dg1.Rows(i).Cells(0).Value & "',csno=" & Val(dg1.Rows(i).Cells(1).Value) & ",rate=" & Val(dg1.Rows(i).Cells(2).Value) & " where csno=" & Val(dg1.Rows(i).Cells(1).Value)
                    executeQuery(msql2)
                End If
            Next i
            MsgBox("Updated Sucessfully!")
            dg.Rows.Clear()
        Catch ex As Exception
            MsgBox("Err on Update - " & ex.Message)
        End Try
        'Cursor = Cursors.Default

        Cursor = Cursors.Default

    End Sub

    Private Sub cmbprocname_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbprocname.GotFocus
        Call loaddisp2()
    End Sub

    Private Sub cmbprocname_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbprocname.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtrate.Focus()
        End If
    End Sub

    Private Sub cmbprocname_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbprocname.SelectedIndexChanged

    End Sub

    Private Sub txtrate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtrate.KeyPress
        If Asc(e.KeyChar) = 13 Then
            butadd2.Focus()
        End If
    End Sub

    Private Sub txtrate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrate.TextChanged

    End Sub

    Private Sub butadd2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butadd2.Click
        If MsgBox("Add Record!", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Call saveadd(1)
            Call loaddisp2()
        End If
    End Sub

    Private Sub dg1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg1.CellContentClick

    End Sub

    Private Sub dg1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg1.CellEndEdit
        If e.RowIndex >= 0 Then
            'If Len(Trim(dg.Rows(e.RowIndex).Cells(3).Value)) > 0 Then
            '    dg.Rows(e.RowIndex).Cells(4).Value = getsno(dg.Rows(e.RowIndex).Cells(3).Value)
            '    ' SendKeys.Send("{Tab}")
            'End If
            dg1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Green
        End If
    End Sub

    Private Sub butupdt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butupdt.Click
        If MsgBox("Update Record!", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Call updtrec()
            Call loaddisp2()
        End If
    End Sub
End Class
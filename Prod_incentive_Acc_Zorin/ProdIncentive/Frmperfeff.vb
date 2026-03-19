Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class Frmperfeff
    Dim msql, msql2, merr, qry, qry1 As String
    Dim i, k, j, msel, msel1 As Int16
    Dim o_id
    Dim e_id
    Dim flag As Boolean
    Dim icol As Int32
    Private transd As SqlTransaction
    Dim lastkey As String

    Private Sub Frmperfeff_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        TabControl1.Height = MDIFORM1.Height - 50
        TabControl1.Width = MDIFORM1.Width - 10
        Call loadsubdept()
    End Sub

    Private Function autoId() As Integer ' generating auto employee id for a new employee

        Dim query = "Select IsNull(Max(PID),0)+1 operid From " & Trim(mcostdbnam) & ".dbo.operationmaster"
        Dim dr As SqlDataReader
        dr = getDataReader(query)
        dr.Read()
        o_id = dr("operid")
        dr.Close()

        Return o_id

    End Function

    Private Sub loaddata()
        msql = "select empid,empname,department,subdept,[lineno] linno from " & Trim(mcostdbnam) & ".dbo.empmaster where subdept='" & cmbdepartment.Text & "'"

        'dg.Columns(0).Width = 100
        'dg.Columns(1).Width = 200
        'dg.Columns(2).Width = 150
        'dg.Columns(3).Width = 150
        'dg.Columns(4).Width = 100

        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        cmd.CommandText = msql
        cmd.Connection = con
        da.SelectCommand = cmd
        Dim dt As New DataTable
        da.Fill(dt)
        dg.DataSource = dt
        dg.Columns(0).Width = 100
        dg.Columns(1).Width = 200
        dg.Columns(2).Width = 150
        dg.Columns(3).Width = 150
        dg.Columns(4).Width = 100


        Dim col2 As New DataGridViewComboBoxColumn
        col2.DataPropertyName = "linno"
        col2.HeaderText = "linno"
        col2.Name = "linno"

        col2.Items.Add("StockItem1")
        col2.Items.Add("StockItem2")

        'Dim dgvc As DataGridViewComboBoxCell
        'dgvc = dg.Rows(0).Cells(4)
        'dgvc.Items.Add("General")
        For k = 1 To 16
            col2.Items.Add(k)
        Next k

        dg.Columns(0).ReadOnly = True
        dg.Columns(1).ReadOnly = True
        dg.Columns(2).ReadOnly = True
        dg.Columns(3).ReadOnly = True
        'dg.Columns(4).ReadOnly = True

    End Sub
    Private Sub loadsubdept()
        'msql = "select subdept from empmaster group by subdept order by subdept"

        Dim query = "Select subdept From " & Trim(mcostdbnam) & ".dbo.empmaster group by subdept order by subdept"
        Dim dr As SqlDataReader

        dr = getDataReader(query)
        cmbdepartment.DataSource = Nothing
        cmbdepartment.Items.Clear()

        cmbeffdept.DataSource = Nothing
        cmbeffdept.Items.Clear()
        cmbrsubdept.DataSource = Nothing
        cmbrsubdept.Items.Clear()

        If dr.HasRows = True Then
            Dim dt As DataTable = New DataTable
            dt.Load(dr)
            cmbdepartment.DataSource = dt
            cmbdepartment.DisplayMember = "subdept"
            cmbdepartment.ValueMember = "subdept"

            
            cmbeffdept.DataSource = dt
            cmbeffdept.DisplayMember = "subdept"
            cmbeffdept.ValueMember = "subdept"

            cmbrsubdept.DataSource = dt
            cmbrsubdept.DisplayMember = "subdept"
            cmbrsubdept.ValueMember = "subdept"


        End If

        dr.Close()

    End Sub
    Private Sub dgohead()

        dgoclear()
        dgo.DataSource = Nothing
        dgo.ColumnCount = 5
        dgo.Columns(0).Name = "Operation"
        dgo.Columns(1).Name = "Mac Type"
        dgo.Columns(2).Name = "SAM"
        dgo.Columns(3).Name = "Tot.Prod"
        dgo.Columns(4).Name = "TotMin"



        dgo.Columns(0).Width = 200
        dgo.Columns(1).Width = 100
        dgo.Columns(2).Width = 100
        dgo.Columns(3).Width = 100
        dgo.Columns(4).Width = 100


        'Dim dgvc As DataGridViewComboBoxCell
        'dgvc = dg.Rows(0).Cells(4)
        'dgvc.Items.Add("General")
        'For k = 1 To 16
        '    dgvc.Items.Add(k)
        'Next k



        dgo.ColumnHeadersDefaultCellStyle.Font = New Font(dg.Font, FontStyle.Bold)
        dgo.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub
    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick

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


   
    Private Sub butdisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butdisp.Click
        loaddata()
    End Sub

    Private Sub ButAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButAdd.Click
        msel = 1

        dgohead()
        dgo.Rows.Add()
    End Sub
    Private Sub dgoclear()
        'dgo.Rows.Clear()
        'Dim i As Integer
        'For i = 0 To dgo.Rows.Count - 1
        '    'Me.dgo.Rows(0).Selected = True
        '    'Me.dgo.Rows(0).Dispose()
        '    'Me.dgo.Rows.RemoveAt(Me.dgo.SelectedRows(0).Index)
        '    dgo.Rows.RemoveAt(i)
        'Next i

        '  For Each row As DataGridViewRow In dgo.SelectedRows
        '      If Not (row.IsNewRow) Then
        '          dgo.Rows.Remove(row)
        '      End If
        'Next

        'For Each row As DataGridViewRow In dgo.SelectedRows
        '    Try
        '        dgo.Rows.Remove(row)
        '    Catch
        '        'MessageBox.Show("Do not highlite the bottom row")
        '    End Try
        'Next

        For j = dgo.Rows.Count - 2 To 1 Step -1
            dgo.Rows.RemoveAt(j)
        Next

        'For j = dgo.Rows.Count - 2 To 1 Step -1
        dgo.Columns.Clear()
        'Next


        'dgo.AllowUserToAddRows = True
        ''AndAlso _row.Cells(i).Value <> ""
        'Dim blank As Boolean = True
        'For Each _row As DataGridViewRow In dgo.Rows
        '    blank = True
        '    For i As Integer = 0 To _row.Cells.Count - 1
        '        If _row.Cells(i).Value IsNot Nothing Then
        '            blank = False
        '            Exit For
        '        End If
        '    Next
        '    If blank Then
        '        If Not _row.IsNewRow Then
        '            dgo.Rows.Remove(_row)
        '        End If
        '    End If
        'Next



    End Sub
    Private Sub Butosave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Butosave.Click
        If msel = 1 Then
            For Each row As DataGridViewRow In dgo.Rows
                msql = "insert into " & Trim(mcostdbnam) & ".dbo.operationmaster(operation,mactype,sam,totprod,totmin,itmgrp)" & vbCrLf _
                & " Values ('" & row.Cells(0).Value & "','" & row.Cells(1).Value & "'," & row.Cells(2).Value & "," & row.Cells(3).Value & "," & row.Cells(4).Value & ",'" & cmbgroup.Text & "'" & ")"
                If Len(Trim(row.Cells(0).Value)) > 0 Then
                    executeQuery(msql)
                End If
            Next
            MsgBox("Saved!")
        End If
        If msel = 2 Then
            For Each row As DataGridViewRow In dgo.Rows
                If Val(row.Cells(0).Value) > 0 Then
                    msql = "update " & Trim(mcostdbnam) & ".dbo.operationmaster set operation='" & row.Cells(1).Value & "',  mactype='" & row.Cells(2).Value & "', sam=" & row.Cells(3).Value & ",totprod=" & row.Cells(4).Value & ",totmin=" & row.Cells(5).Value & " where id =" & Val(row.Cells(0).Value)
                    executeQuery(msql)

                End If
            Next
            MsgBox("Updated!")
        End If
    End Sub

    Private Sub loadoperation()

        msql = "select * from " & Trim(mcostdbnam) & ".dbo.operationmaster "
        If Len(Trim(cmbgroup.Text)) > 0 Then
            msql = msql & " where itmgrp='" & cmbgroup.Text & "'"
        End If

        'dgo.AllowUserToAddRows = False
        dgoclear()
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        cmd.CommandText = msql
        cmd.Connection = con
        da.SelectCommand = cmd
        Dim dt As New DataTable
        da.Fill(dt)
        dgo.DataSource = dt
        dgo.Columns(0).Width = 80
        dgo.Columns(1).Width = 200
        dgo.Columns(2).Width = 100
        dgo.Columns(3).Width = 100
        dgo.Columns(4).Width = 100
        dgo.Columns(5).Width = 150




        'Dim col2 As New DataGridViewComboBoxColumn
        'col2.DataPropertyName = "linno"
        'col2.HeaderText = "linno"
        'col2.Name = "linno"

        'col2.Items.Add("StockItem1")
        'col2.Items.Add("StockItem2")

        ''Dim dgvc As DataGridViewComboBoxCell
        ''dgvc = dg.Rows(0).Cells(4)
        ''dgvc.Items.Add("General")
        'For k = 1 To 16
        '    col2.Items.Add(k)
        'Next k

        dgo.Columns(0).ReadOnly = True
        'dg.Columns(1).ReadOnly = True
        'dg.Columns(2).ReadOnly = True
        'dg.Columns(3).ReadOnly = True
        ''dg.Columns(4).ReadOnly = True

    End Sub

    Private Sub Butodisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Butodisp.Click
        msel = 2
        'dgohead()
        loadoperation()
    End Sub

    Private Sub Label19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label19.Click

    End Sub

    Private Sub dg1head()

        dg1.ColumnCount = 19
        dg1.Columns(0).Name = "Emp_Id"
        dg1.Columns(1).Name = "EMP.Name"
        dg1.Columns(2).Name = "Operation"
        dg1.Columns(3).Name = "SAM"
        dg1.Columns(4).Name = "Cur.Prod"
        dg1.Columns(5).Name = "Tot.MIN"
        dg1.Columns(6).Name = "Wait Time"
        dg1.Columns(7).Name = "M/C Dwn Time"
        dg1.Columns(8).Name = "Elec.Dwn Time"
        dg1.Columns(9).Name = "Others"
        dg1.Columns(10).Name = "Tot.OFF STD Time"
        dg1.Columns(11).Name = "On STD Time"
        dg1.Columns(12).Name = "SAM Produced"
        dg1.Columns(13).Name = "Per%"
        dg1.Columns(14).Name = "Eff%"
        dg1.Columns(15).Name = "Total"
        dg1.Columns(16).Name = "UTT%"
        dg1.Columns(17).Name = "MAC.Type"
        dg1.Columns(18).Name = "Tot.Prod"

        dg1.Columns(0).Width = 100
        dg1.Columns(1).Width = 250
        dg1.Columns(2).Width = 250
        dg1.Columns(3).Width = 75
        dg1.Columns(4).Width = 80
        dg1.Columns(5).Width = 80
        dg1.Columns(6).Width = 80
        dg1.Columns(7).Width = 100
        dg1.Columns(8).Width = 100
        dg1.Columns(9).Width = 100
        dg1.Columns(10).Width = 80
        dg1.Columns(11).Width = 80
        dg1.Columns(12).Width = 70
        dg1.Columns(13).Width = 70
        dg1.Columns(14).Width = 75
        dg1.Columns(15).Width = 70
        dg1.Columns(16).Width = 70
        dg1.Columns(17).Width = 70
        dg1.Columns(18).Width = 70

        dg1.Columns(0).ReadOnly = True
        dg1.Columns(1).ReadOnly = True
        dg1.Columns(2).ReadOnly = True
        dg1.Columns(3).ReadOnly = True
        dg1.Columns(4).ReadOnly = True
        dg1.Columns(5).ReadOnly = True
        dg1.Columns(6).ReadOnly = True
        dg1.Columns(7).ReadOnly = True
        dg1.Columns(8).ReadOnly = True
        dg1.Columns(9).ReadOnly = True
        dg1.Columns(10).ReadOnly = True
        dg1.Columns(11).ReadOnly = True
        dg1.Columns(13).ReadOnly = True
        dg1.Columns(14).ReadOnly = True
        dg1.Columns(15).ReadOnly = True
        dg1.Columns(16).ReadOnly = True
        dg1.Columns(17).ReadOnly = True
        dg1.Columns(18).ReadOnly = True


        dg1.ColumnHeadersDefaultCellStyle.Font = New Font(dg.Font, FontStyle.Bold)
        dg1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private Sub TabPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage2.Click
        Call dg1head()
        ' loaddgoperation()
        loaditmgrp()
    End Sub

    Private Sub Buteadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buteadd.Click
        j = dg1.Rows.Add()
        dg1.Rows(j).Cells(0).Value = txtempid.Text
        dg1.Rows(j).Cells(1).Value = cmbempname.Text
        dg1.Rows(j).Cells(2).Value = txtopername.Text
        dg1.Rows(j).Cells(3).Value = Val(txtsam.Text)
        dg1.Rows(j).Cells(4).Value = Val(txttotprod.Text)
        dg1.Rows(j).Cells(5).Value = Val(txttotminwork.Text)
        dg1.Rows(j).Cells(6).Value = Val(txtwaittime.Text)
        dg1.Rows(j).Cells(7).Value = Val(txtmacdwntime.Text)
        dg1.Rows(j).Cells(8).Value = Val(txtElecdwntime.Text)
        dg1.Rows(j).Cells(9).Value = Val(txtother.Text)
        dg1.Rows(j).Cells(17).Value = txtmactype.Text
        dg1.Rows(j).Cells(18).Value = Val(txtmtotprod.Text)

        'dg1.Columns(17).Name = "MAC.Type"
        'dg1.Columns(18).Name = "Tot.Prod"
        'dg1.Rows(j).Cells(10).Value = txtempid.Text
        dg1.Rows(j).Cells(10).Value = (Val(dg1.Rows(j).Cells(6).Value) + Val(dg1.Rows(j).Cells(7).Value) + Val(dg1.Rows(j).Cells(8).Value) + Val(dg1.Rows(j).Cells(9).Value))
        dg1.Rows(j).Cells(11).Value = Val(dg1.Rows(j).Cells(5).Value) - Val(dg1.Rows(j).Cells(10).Value)
        dg1.Rows(j).Cells(12).Value = Val(txtsam.Text) * Val(dg1.Rows(j).Cells(4).Value)
        dg1.Rows(j).Cells(13).Value = Format(Val(dg1.Rows(j).Cells(12).Value) / Val(dg1.Rows(j).Cells(11).Value) * 100, "####0")
        dg1.Rows(j).Cells(14).Value = Format(Val(dg1.Rows(j).Cells(12).Value) / Val(dg1.Rows(j).Cells(5).Value) * 100, "####0")
        dg1.Rows(j).Cells(16).Value = Val(dg1.Rows(j).Cells(11).Value) / Val(dg1.Rows(j).Cells(5).Value) * 100

        txtempid.Text = ""
        cmbempname.Text = ""
        txtopername.Text = ""
        txtsam.Text = ""
        txttotprod.Text = ""
        txttotminwork.Text = ""
        txtwaittime.Text = ""
        txtmacdwntime.Text = ""
        txtElecdwntime.Text = ""
        txtother.Text = ""
        txtmactype.Text = ""
        txtmtotprod.Text = ""

        If dgs.Visible = True Then dgs.Visible = False

    End Sub


    Private Sub loadempname()
        'msql = "select subdept from empmaster group by subdept order by subdept"
        Dim query As String

        query = "Select empname,empid From " & Trim(mcostdbnam) & ".dbo.empmaster " & vbCrLf

        If Len(Trim(cmbeffdept.Text)) > 0 Or Len(Trim(cmblineno.Text)) > 0 Then
            query = query & "where "
        End If

        If Len(Trim(cmbeffdept.Text)) > 0 Then
            query = query & " subdept='" & cmbeffdept.Text & "'"
        End If

        If Len(Trim(cmblineno.Text)) > 0 Then
            If Len(Trim(cmbeffdept.Text)) > 0 Then
                query = query & " and "
            End If
            query = query & " [lineno]='" & cmblineno.Text & "'"
        End If

        query = query & " group by empid,empname order by empname"

        Dim dr As SqlDataReader

        dr = getDataReader(query)
        cmbempname.DataSource = Nothing
        cmbempname.Items.Clear()

        If dr.HasRows = True Then
            Dim dt As DataTable = New DataTable
            dt.Load(dr)
            cmbempname.DataSource = dt
            cmbempname.DisplayMember = "empname"
            cmbempname.ValueMember = "empid"

        End If

        dr.Close()
        txtempid.Text = ""
        cmbempname.Text = ""

    End Sub

    Private Sub butenew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butenew.Click
        txtno.Text = autoIde()
        'txtno.Enabled = False
        msel = 1
        mskdate.Text = Format(Date.Now, "dd-MM-yyyy")
        mskdate.Focus()

    End Sub

    Private Function autoIde() As Integer ' generating auto employee id for a new employee

        Dim query = "Select IsNull(Max(docnum),0)+1 eid From " & Trim(mcostdbnam) & ".dbo.perfeffhead"
        Dim dr As SqlDataReader
        dr = getDataReader(query)
        dr.Read()
        e_id = dr("eid")
        dr.Close()

        Return e_id

    End Function

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex

            Case 0 ' User clicks on First Tab
                ' code to do here
                'formclear()
                'reload()
                ''loadparty()
                'cmbptype.Items.Clear()
                'cmbptype.Items.Add("Borrower")
                'cmbptype.Items.Add("Lender")
                ''cmbcompany.Text = ""
                ''AddHandlers()  
                icol = Val(txtcol.Text)

            Case 1 ' User clicks on Second Tab
                ' code to do here
                'formclear()
                'reload1()
                'loadparty()
                'mskldate.Text = Format(Date.Now, "dd-MM-yyyy")
                'cmbinttype.Items.Clear()
                'cmbptype.Items.Add("Borrower")
                'cmbptype.Items.Add("Lender")
                'cmbinttype.Items.Add("Daily")
                'cmbinttype.Items.Add("Weekly")
                'cmbinttype.Items.Add("Monthly")


            Case 2 ' User clicks on Third Tab
                ' code to do here
                'formclear()
                'ClearAllGROUP(GroupBox3, True)
                'Call loadparty()
                'Mskintdate.Text = Format(Date.Now, "dd-MM-yyyy")
                'txtintid.Text = autoIdint()
                'dg2.Rows.Clear()
                'loadsubdept()
                'loaddgoperation()
                Call dg1head()
                dg1.Rows.Clear()

                'Case 3 ' User clicks on Fourth Tab
                '    ' code to do here
                '    formclear()
                '    ClearAllGROUP(GroupBox4, True)
                '    Call loadparty()
                '    mskprdate.Text = Format(Date.Now, "dd-MM-yyyy")
                '    txtprid.Text = autoIdpr()
                '    dg4.Rows.Clear()
                '    dg3.Rows.Clear()
                '    loadloangrid()
            Case 3
                loadsubdept()
                loaditmgrp()

        End Select
    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click
        icol = Val(txtcol.Text)
    End Sub

    Private Sub TabPage3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage3.Click
        cmbgroup.Items.Clear()
        cmbgroup.Items.Add("SHIRT")
        cmbgroup.Items.Add("PANT")
        cmbgroup.Items.Add("LS SHIRT")
        cmbgroup.Items.Add("KURTHA SET")
        cmbgroup.Items.Add("KURTHA TOP")
        cmbgroup.Items.Add("KURTHA BOTTOM")
        cmbgroup.Items.Add("SHORTS")


    End Sub

    Private Sub cmbempname_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbempname.KeyPress
        'If Asc(e.KeyChar) = 13 Then
        '    cmbopername.Focus()
        'End If
    End Sub

    Private Sub cmbempname_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbempname.LostFocus

    End Sub

    Private Sub cmbempname_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbempname.SelectedIndexChanged
        txtempid.Text = cmbempname.SelectedValue.ToString
    End Sub

    Private Sub cmblineno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmblineno.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbeffgrp.Focus()
        End If
    End Sub

    Private Sub cmblineno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmblineno.LostFocus
        loadempname()
        'loaddgoperation()
    End Sub

    Private Sub cmblineno_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmblineno.SelectedIndexChanged

    End Sub

    'Private Sub loadopername()
    '    '****
    '    'msql = "select subdept from empmaster group by subdept order by subdept"

    '    Dim query = "Select * From " & Trim(mcostdbnam) & ".dbo.operationmaster " & vbCrLf
    '    Dim dr As OleDbDataReader

    '    dr = getDataReader(query)
    '    cmbopername.DataSource = Nothing
    '    cmbopername.Items.Clear()

    '    If dr.HasRows = True Then
    '        Dim dt As DataTable = New DataTable
    '        dt.Load(dr)
    '        cmbopername.DataSource = dt
    '        cmbopername.DisplayMember = "operation"
    '        cmbopername.ValueMember = "id"

    '    End If

    '    dr.Close()

    'End Sub

    'Private Sub loadsam()
    '    Dim msql = "Select * From " & Trim(mcostdbnam) & ".dbo.operationmaster where operation='" & cmbopername.Text & "'" & vbCrLf
    '    Dim dr1 As OleDbDataReader

    '    dr1 = getDataReader(msql)
    '    'cmbopername.DataSource = Nothing
    '    'cmbopername.Items.Clear()

    '    If dr1.HasRows = True Then
    '        While dr1.Read
    '            txtsam.Text = dr1.Item("SAM")
    '            txttotminwork.Text = dr1.Item("totmin")
    '        End While
    '    End If

    '    dr1.Close()
    'End Sub

    'Private Sub cmbopername_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If Asc(e.KeyChar) = 13 Then
    '        Call loadsam()
    '    End If
    'End Sub

    'Private Sub cmbopername_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    txtopid.Text = cmbopername.SelectedValue
    '    Call loadsam()
    'End Sub

    Private Sub butedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butedit.Click
        msel = 2
        txtno.Focus()

    End Sub

    Private Sub txtno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtno.KeyPress
        If Asc(e.KeyChar) = 13 Then
            mskdate.Focus()
        End If
    End Sub

    Private Sub txtno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtno.LostFocus
        If msel > 1 Then
            Call loadperf()
        End If
    End Sub

    Private Sub txtno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtno.TextChanged

    End Sub

    Private Sub loadperf()
        Dim query, query1 As String
        'query = "Select	D.*, M.manager_name, M.manager_id"
        'query = query + " From	Department D "
        'query = query + " Inner Join Manager M On D.manager_id = M.manager_id"
        query = "select * from " & Trim(mcostdbnam) & ".dbo.perfeffhead where docnum=" & Val(txtno.Text)
        Dim dt1 As DataTable = getDataTable(query)
        For Each row1 As DataRow In dt1.Rows
            mskdate.Text = Format(row1("edate"), "dd-MM-yyyy")
            cmbeffdept.Text = row1("subdept")
            cmblineno.Text = row1("lineno")
        Next

        query = "select * from " & Trim(mcostdbnam) & ".dbo.perfefficient where docnum=" & Val(txtno.Text)
        dg1.Rows.Clear()

        Dim dt As DataTable = getDataTable(query)

        For Each row As DataRow In dt.Rows

            dg1.Rows.Add()
            dg1.Rows(dg1.RowCount - 1).Cells(0).Value = row("empid")
            dg1.Rows(dg1.RowCount - 1).Cells(1).Value = row("empname")
            dg1.Rows(dg1.RowCount - 1).Cells(2).Value = row("operation")
            dg1.Rows(dg1.RowCount - 1).Cells(3).Value = row("sam")
            dg1.Rows(dg1.RowCount - 1).Cells(4).Value = row("curprod")
            dg1.Rows(dg1.RowCount - 1).Cells(5).Value = row("totmin")
            dg1.Rows(dg1.RowCount - 1).Cells(6).Value = row("wtime")
            dg1.Rows(dg1.RowCount - 1).Cells(7).Value = row("mcdowntime")
            dg1.Rows(dg1.RowCount - 1).Cells(8).Value = row("electricaldowntime")
            dg1.Rows(dg1.RowCount - 1).Cells(9).Value = row("other")
            dg1.Rows(dg1.RowCount - 1).Cells(10).Value = row("totoffstdtime")
            dg1.Rows(dg1.RowCount - 1).Cells(11).Value = row("onstdtime")
            dg1.Rows(dg1.RowCount - 1).Cells(12).Value = row("samproduced")
            dg1.Rows(dg1.RowCount - 1).Cells(13).Value = Format(row("per"), "####0")
            dg1.Rows(dg1.RowCount - 1).Cells(14).Value = Format(row("effper"), "####0")
            dg1.Rows(dg1.RowCount - 1).Cells(15).Value = Format(row("total"), "####0")
            dg1.Rows(dg1.RowCount - 1).Cells(16).Value = Format(row("uttime"), "####0")
            dg1.Rows(dg1.RowCount - 1).Cells(17).Value = row("mactype")
            dg1.Rows(dg1.RowCount - 1).Cells(18).Value = row("totprod")

            'dg1.Rows(dg1.RowCount - 1).Cells("FromDB1").Value = True
            'dg1.Rows(dg1.RowCount - 1).DefaultCellStyle.ForeColor = Color.Black

        Next

        'txtlid.Text = autoId1()
    End Sub
    Private Sub loaddgoperation()
        Dim query As String
        'query = "Select	D.*, M.manager_name, M.manager_id"
        'query = query + " From	Department D "
        'query = query + " Inner Join Manager M On D.manager_id = M.manager_id"

        query = "select * from " & Trim(mcostdbnam) & ".dbo.operationmaster "
        If Len(Trim(cmbeffgrp.Text)) > 0 Then
            query = query & " where itmgrp='" & cmbeffgrp.Text & "'"
        End If
        query = query & " order by operation"
        dgs.Rows.Clear()

        Dim dt As DataTable = getDataTable(query)

        For Each row As DataRow In dt.Rows

            dgs.Rows.Add()
            dgs.Rows(dgs.RowCount - 1).Cells(0).Value = row("operation")
            dgs.Rows(dgs.RowCount - 1).Cells(1).Value = row("mactype")
            dgs.Rows(dgs.RowCount - 1).Cells(2).Value = row("sam")
            dgs.Rows(dgs.RowCount - 1).Cells(3).Value = row("totprod")
            dgs.Rows(dgs.RowCount - 1).Cells(4).Value = row("totmin")
            dgs.Rows(dgs.RowCount - 1).Cells(5).Value = row("id")
            'dg1.Rows(dg1.RowCount - 1).Cells(6).Value = row("wtime")
            'dg1.Rows(dg1.RowCount - 1).Cells(7).Value = row("macdowntime")
            'dg1.Rows(dg1.RowCount - 1).Cells(8).Value = row("electricaldowntime")
            'dg1.Rows(dg1.RowCount - 1).Cells(9).Value = row("other")

            'dg1.Rows(dg1.RowCount - 1).Cells("FromDB1").Value = True
            'dg1.Rows(dg1.RowCount - 1).DefaultCellStyle.ForeColor = Color.Black

        Next

        'txtlid.Text = autoId1()
    End Sub

    Private Sub butesave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butesave.Click
        Me.Cursor = Cursors.WaitCursor

        If msel = 1 Or msel = 2 Then
            If msel = 2 Then
                qry = "delete from " & Trim(mcostdbnam) & ".dbo.perfeffhead where docnum=" & Val(txtno.Text)
                qry1 = "delete from " & Trim(mcostdbnam) & ".dbo.perfefficient where docnum=" & Val(txtno.Text)
            End If
            msql2 = "insert into " & Trim(mcostdbnam) & ".dbo.perfeffhead (docnum,edate,subdept,[lineno],itmgrp) " & vbCrLf _
                  & " Values (" & Val(txtno.Text) & ",'" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "','" & cmbeffdept.Text & "','" & cmblineno.Text & "','" & cmbeffgrp.Text & "')"
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If


            transd = con.BeginTransaction
            'If msel = 2 Then
            Dim dcmd1 As New SqlCommand(qry, con, transd)
            Dim dcmd2 As New SqlCommand(qry1, con, transd)


            'End If
            Dim cmd2 As New SqlCommand(msql2, con, transd)

            Try
                If msel = 2 Then
                    dcmd1.ExecuteNonQuery()
                    dcmd2.ExecuteNonQuery()
                End If

                cmd2.ExecuteNonQuery()
                'transd.Commit()
                'transd.Dispose()
                For Each row As DataGridViewRow In dg1.Rows
                    msql = "insert into " & Trim(mcostdbnam) & ".dbo.perfefficient(edate,empid,empname,operation,sam,curprod,totmin,wtime,mcdowntime,electricaldowntime,other,totoffstdtime,onstdtime,samproduced,per,effper,total,uttime,mactype,totprod,subdept,[lineno],docnum,itmgrp)" & vbCrLf _
                    & " Values ('" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'," & Val(row.Cells(0).Value) & ",'" & row.Cells(1).Value & "','" & row.Cells(2).Value & "'," & Val(row.Cells(3).Value) & "" & vbCrLf _
                    & "," & Val(row.Cells(4).Value) & "," & Val(row.Cells(5).Value) & "," & Val(row.Cells(6).Value) & "," & Val(row.Cells(7).Value) & "," & Val(row.Cells(8).Value) & "," & Val(row.Cells(9).Value) & vbCrLf _
                     & "," & Val(row.Cells(10).Value) & "," & Val(row.Cells(11).Value) & "," & Val(row.Cells(12).Value) & "," & Val(row.Cells(13).Value) & "," & Val(row.Cells(14).Value) & "," & Val(row.Cells(15).Value) & "," & Val(row.Cells(16).Value) & ",'" & row.Cells(17).Value & "'," & Val(row.Cells(18).Value) & "" & vbCrLf _
                     & ",'" & cmbeffdept.Text & "','" & cmblineno.Text & "'," & Val(txtno.Text) & ",'" & cmbeffgrp.Text & "'" & " )"
                    Dim cmd1 As New SqlCommand(msql, con, transd)
                    cmd1.ExecuteNonQuery()
                    'executeQuery(msql)
                Next
                transd.Commit()
                'transd.Dispose()
                'con.Close()
                MsgBox("Saved!")
                butecancel.PerformClick()
                'con.Close()
            Catch ex As Exception
                'If InStr(merr, "PRIMARY KEY") > 0 Then

                'End If
                'merr = Trim(ex.Message)
                '    transd.Rollback()
                '    MsgBox(ex2.Message)
                'Catch EX As OleDbException
                If Not transd Is Nothing Then
                    transd.Rollback()
                End If
                'If MSEL = 2 Then
                ' trans.Rollback()
                ' End If
                'MsgBox(EX.Message)
                'DS.Dispose()
                'DA.Dispose()
                'DS1.Dispose()
                'DA1.Dispose()
                merr = Trim(ex.Message)

                If InStr(merr, "PRIMARY KEY") > 0 Then

                    txtno.Text = autoIde()
                    butesave.PerformClick()
                    'Dim CMD3 As New OleDb.OleDbCommand("SELECT MAX(docnum) AS TNO FROM oinward", con)
                    'If con.State = ConnectionState.Closed Then
                    '    con.Open()
                    'End If
                    'Dim CBNO As Int32 = IIf(IsDBNull(CMD3.ExecuteScalar) = False, CMD3.ExecuteScalar, 0)
                    'txtno.Text = CBNO + 1
                    'CMD3.Dispose()
                    ''con.Close()
                    'Call SAVEREC()
                Else
                    MsgBox(merr)
                End If
            Finally
                con.Close()
            End Try


            'For Each row As DataGridViewRow In dg1.Rows
            '    msql = "insert into " & Trim(mcostdbnam) & ".dbo.perfefficient(edate,empid,empname,operation,sam,curprod,totmin,wtime,mcdowntime,electricaldowntime,other,totoffstdtime,onstdtime,samproduced,per,effper,total,uttime,mactype,totprod,subdept,[lineno],docnum)" & vbCrLf _
            '    & " Values ('" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'," & Val(row.Cells(0).Value) & ",'" & row.Cells(1).Value & "','" & row.Cells(2).Value & "'," & Val(row.Cells(3).Value) & "" & vbCrLf _
            '    & "," & Val(row.Cells(4).Value) & "," & Val(row.Cells(5).Value) & "," & Val(row.Cells(6).Value) & "," & Val(row.Cells(7).Value) & "," & Val(row.Cells(8).Value) & "," & Val(row.Cells(9).Value) & vbCrLf _
            '     & "," & Val(row.Cells(10).Value) & "," & Val(row.Cells(11).Value) & "," & Val(row.Cells(12).Value) & "," & Val(row.Cells(13).Value) & "," & Val(row.Cells(14).Value) & "," & Val(row.Cells(15).Value) & "," & Val(row.Cells(16).Value) & ",'" & row.Cells(17).Value & "'," & Val(row.Cells(18).Value) & "" & vbCrLf _
            '     & ",'" & cmbeffdept.Text & "','" & cmblineno.Text & "'," & Val(txtno.Text) & " )"
            '    executeQuery(msql)
            'Next
            'MsgBox("Saved!")
        End If


        'If msel = 2 Then
        '    For Each row As DataGridViewRow In dg1.Rows
        '        msql = "update " & Trim(mcostdbnam) & ".dbo.perfefficient set empid=" & row.Cells(0).Value & ",empname='" & row.Cells(1).Value & "',operation='" & row.Cells(2).Value & "', sam=" & row.Cells(3).Value & ",curprod=" & row.Cells(4).Value & vbCrLf _
        '         & ",totmin=" & row.Cells(5).Value & ",wtime=" & row.Cells(6).Value & ",macdowntime=" & row.Cells(7).Value & ",electricaldowntime=" & row.Cells(8).Value & ",other=" & row.Cells(9).Value & " where id=" & Val(txtno.Text)
        '        executeQuery(msql)
        '    Next
        '    MsgBox("Updated!")
        'End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Butocancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Butocancel.Click
        dgoclear()
        'dgo.Rows.Clear()
    End Sub

    Private Sub cmbopername_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtopername_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtopername.GotFocus
        If Len(Trim(txtopername.Text)) = 0 Then
            If dgs.Visible = False Then dgs.Visible = True
        End If
    End Sub

    Private Sub txtopername_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtopername.KeyDown
        If e.KeyCode = Keys.Up Then
            dgs.Focus()
            If Not dgs.CurrentRow.Index = 0 Then
                If Not dgs.CurrentRow.Index = -1 Then
                    dgs.CurrentCell = dgs.Rows(dgs.CurrentRow.Index - 1).Cells(0)
                Else
                    dgs.CurrentCell = dgs.Rows(dgs.CurrentRow.Index).Cells(0)
                End If
            End If
        ElseIf e.KeyCode = Keys.Down Then
            dgs.Focus()
            If Not dgs.CurrentRow.Index = dgs.Rows.Count - 1 Then
                If Not dgs.CurrentRow.Index = -1 Then
                    dgs.CurrentCell = dgs.Rows(dgs.CurrentRow.Index + 1).Cells(0)
                Else
                    dgs.CurrentCell = dgs.Rows(dgs.CurrentRow.Index).Cells(0)
                End If
            End If
        End If

        If e.KeyCode = Keys.Return Then
            If txtopername.Text <> dgs.SelectedCells(0).Value Then
                txtopername.Text = dgs.SelectedCells(0).Value
                txtopid.Text = dgs.Rows(dgs.CurrentRow.Index).Cells(5).Value
                txtmactype.Text = dgs.Rows(dgs.CurrentRow.Index).Cells(1).Value
                txtsam.Text = dgs.Rows(dgs.CurrentRow.Index).Cells(2).Value
                txttotminwork.Text = dgs.Rows(dgs.CurrentRow.Index).Cells(4).Value
                txtmtotprod.Text = dgs.Rows(dgs.CurrentRow.Index).Cells(3).Value

                e.SuppressKeyPress = True
                txttotprod.Focus()
                'mskdob.Focus()

            End If
        End If
    End Sub

    Private Sub txtopername_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtopername.KeyPress
        For j As Integer = 0 To dgs.Rows.Count - 1
            If e.KeyChar <> Chr(Keys.Back) Then
                If dgs.Rows(j).Cells(0).Value.ToString.StartsWith(txtopername.Text & e.KeyChar, StringComparison.CurrentCultureIgnoreCase) Then
                    flag = True
                    Exit For

                Else
                    flag = False

                End If

            Else

                If txtopername.Text.Length <> 0 Then
                    flag = True
                End If
            End If

        Next

        If flag = False Then
            If e.KeyChar <> Chr(Keys.Return) Then
                If txtopername.SelectedText = "" Then
                    e.Handled = True
                    Beep()
                Else
                    Dim searchindex As Integer = 0
                    For Each row As DataGridViewRow In dgs.Rows
                        For Each cell As DataGridViewCell In row.Cells
                            If cell.Value.StartsWith(e.KeyChar, StringComparison.InvariantCultureIgnoreCase) Then
                                cell.Selected = True
                                MsgBox("Found")
                                dgs.CurrentCell = dgs.SelectedCells(0)
                                cell.Style.BackColor = Color.Yellow
                                searchindex += 1
                            End If
                        Next
                    Next
                End If
            End If
        End If


        If Asc(e.KeyChar) = 27 Then
            If dgs.Visible = True Then dgs.Visible = False
        End If
        If Asc(e.KeyChar) = 13 Then
            If msel > 1 Then
                If dgs.Visible = True Then dgs.Visible = False
                'LOADEMPMASTER()
            End If
        End If
    End Sub

    Private Sub txtopername_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtopername.LostFocus
        If dgs.Focused = False Then
            If txtopername.Text = "" Then
                If dgs.Rows.Count > 0 Then
                    dgs.Rows(0).Selected = True
                    txtopername.Text = dgs.SelectedRows(0).Cells(0).Value
                End If
            End If

        End If

        If dgs.Focused = False Then
            dgs.Visible = False
        End If
        'LOADEMPMASTER()





        'If msel = 1 Then
        '    If dgs.Visible = True Then dgs.Visible = False
        'End If
    End Sub

    Private Sub txtopername_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtopername.TextChanged
        If Len(Trim(txtopername.Text)) = 0 Then
            If dgs.Visible = False Then dgs.Visible = True
        End If


        If Flag = True Then
            dgs.ClearSelection()
            For Each row As DataGridViewRow In dgs.Rows
                For Each cell As DataGridViewCell In row.Cells
                    If cell.ColumnIndex = 0 Then
                        If cell.Value.StartsWith(txtopername.Text, StringComparison.InvariantCultureIgnoreCase) Then
                            cell.Selected = True
                            dgs.CurrentCell = dgs.SelectedCells(0)
                            'Exit For
                        End If
                    End If

                Next
            Next
        Else

            If txtopername.Text = "" Then
                dgs.Rows(0).Selected = True
            End If
        End If
    End Sub

    Private Sub dgs_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgs.CellContentClick

    End Sub

    Private Sub dgs_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgs.CellDoubleClick
        txtopername.Text = dgs.SelectedCells(0).Value
        txtopid.Text = dgs.Rows(dgs.CurrentRow.Index).Cells(5).Value
        txtmactype.Text = dgs.Rows(dgs.CurrentRow.Index).Cells(1).Value
        txtsam.Text = dgs.Rows(dgs.CurrentRow.Index).Cells(2).Value
        txttotminwork.Text = dgs.Rows(dgs.CurrentRow.Index).Cells(4).Value
        txtmtotprod.Text = dgs.Rows(dgs.CurrentRow.Index).Cells(3).Value
        txttotprod.Focus()
        If dgs.Visible = True Then dgs.Visible = False
    End Sub

    Private Sub butsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butsave.Click
        Me.Cursor = Cursors.WaitCursor
        For Each row As DataGridViewRow In dg.Rows
            If IsDBNull(row.Cells(4).Value) = False Then
                If row.Cells(4).Value > 0 Then
                    msql = "update " & Trim(mcostdbnam) & ".dbo.empmaster set [lineno]=" & row.Cells(4).Value & " where empid=" & row.Cells(0).Value & " and empname='" & row.Cells(1).Value & "'"
                    executeQuery(msql)
                End If
            End If
        Next
        MsgBox("updated!")
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub butecancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butecancel.Click
        dg1.Rows.Clear()
        'CLEAR(Me)
        'CLEAR2(Me, TabControl1)
        txtno.Text = ""
        mskdate.Text = "__-__-____"
        cmbeffdept.Text = ""
        cmblineno.Text = ""
        txtempid.Text = ""
        cmbempname.Text = ""
        txtopername.Text = ""
        txtsam.Text = ""
        txttotprod.Text = ""
        txttotminwork.Text = ""
        txtwaittime.Text = ""
        txtmacdwntime.Text = ""
        txtElecdwntime.Text = ""
        txtother.Text = ""
        txtmactype.Text = ""
        txtmtotprod.Text = ""
        If dgs.Visible = True Then dgs.Visible = False
        'ClearAllCtrls(Panel2, False)
        'txtno.Text = autoIde()
    End Sub
    Private Sub editperf()
        If dg1.SelectedRows.Count = 0 Then

            Return

        End If
        txtempid.Text = dg1.SelectedRows(0).Cells(0).Value
        cmbempname.Text = dg1.SelectedRows(0).Cells(1).Value
        txtopername.Text = dg1.SelectedRows(0).Cells(2).Value
        txtsam.Text = dg1.SelectedRows(0).Cells(3).Value
        txttotprod.Text = dg1.SelectedRows(0).Cells(4).Value
        txttotminwork.Text = dg1.SelectedRows(0).Cells(5).Value
        txtwaittime.Text = dg1.SelectedRows(0).Cells(6).Value
        txtmacdwntime.Text = dg1.SelectedRows(0).Cells(7).Value
        txtElecdwntime.Text = dg1.SelectedRows(0).Cells(8).Value
        txtother.Text = dg1.SelectedRows(0).Cells(9).Value
        txtmactype.Text = dg1.SelectedRows(0).Cells(17).Value
        txtmtotprod.Text = dg1.SelectedRows(0).Cells(18).Value
    End Sub

    Private Sub dg1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg1.CellContentClick

    End Sub

    Private Sub dg1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg1.CellDoubleClick
        Call editperf()
        'dg1.SelectedRows(0).Cells(0).Value
        'dg1.Rows.RemoveAt(dg1.CurrentRow.Index)
        dg1.Rows.Remove(dg1.SelectedRows(0))
    End Sub

    Private Sub butcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butcancel.Click
        'dg.Rows.Clear()
        dgclear()
    End Sub


    Private Sub dg_jumpRecord(ByVal lastkey As String, ByRef found As Boolean)
        icol = Val(txtcol.Text)
        For i As Integer = 0 To (dg.Rows.Count) - 1

            Dim rowText = dg.Rows(i).Cells(icol).Value.ToString

            If StrConv(rowText.ToString, VbStrConv.Uppercase).StartsWith(lastkey) Then
                dg.ClearSelection()
                dg.CurrentCell = dg.Rows(i).Cells(icol)
                dg.Rows(i).Selected = True
                found = True
                Exit Sub
            End If

        Next
        found = False

    End Sub

    Private Sub dgo_jumpRecord(ByVal lastkey As String, ByRef found As Boolean)
        icol = Val(txtocol.Text)
        For i As Integer = 0 To (dgo.Rows.Count) - 1

            Dim rowText = dgo.Rows(i).Cells(icol).Value.ToString

            If StrConv(rowText.ToString, VbStrConv.Uppercase).StartsWith(lastkey) Then
                dgo.ClearSelection()
                dgo.CurrentCell = dgo.Rows(i).Cells(icol)
                dgo.Rows(i).Selected = True
                found = True
                Exit Sub
            End If

        Next
        found = False

    End Sub


    Private Sub dg_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dg.KeyPress
        icol = Val(txtcol.Text)
        'Dim input = StrConv(e.KeyChar.ToString())
        Dim input = StrConv(e.KeyChar, VbStrConv.Uppercase)
        lastkey = lastkey + input
        Dim found As Boolean

        Try
            If StrConv(dg.CurrentCell.Value.ToString.Substring(0, 1), VbStrConv.Uppercase) = input Then
                Dim curIndex = dg.CurrentRow.Index
                If curIndex < dg.Rows.Count - 1 Then
                    dg.ClearSelection()
                    curIndex += 1
                    dg.CurrentCell = dg.Rows(curIndex).Cells(icol)
                    dg.Rows(curIndex).Selected = True
                End If
            Else
                dg_jumpRecord(lastkey, found)

                If Not found Then
                    lastkey = input
                    dg_jumpRecord(lastkey, found)
                End If
            End If
        Catch ex As Exception
            lastkey = ""
            input = ""
            found = False
        End Try
    End Sub

    Private Sub dgo_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgo.CellContentClick

    End Sub

    Private Sub dgo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgo.KeyPress
        'icol = Val(txtcol.Text)
        ''Dim input = StrConv(e.KeyChar.ToString())
        'Dim input = StrConv(e.KeyChar, VbStrConv.Uppercase)
        'lastkey = lastkey + input
        'Dim found As Boolean

        'Try
        '    If StrConv(dgo.CurrentCell.Value.ToString.Substring(0, 1), VbStrConv.Uppercase) = input Then
        '        Dim curIndex = dgo.CurrentRow.Index
        '        If curIndex < dgo.Rows.Count - 1 Then
        '            dgo.ClearSelection()
        '            curIndex += 1
        '            dgo.CurrentCell = dgo.Rows(curIndex).Cells(icol)
        '            dgo.Rows(curIndex).Selected = True
        '        End If
        '    Else
        '        dgo_jumpRecord(lastkey, found)

        '        If Not found Then
        '            lastkey = input
        '            dgo_jumpRecord(lastkey, found)
        '        End If
        '    End If
        'Catch ex As Exception
        '    lastkey = ""
        '    input = ""
        '    found = False
        'End Try
    End Sub

    Private Sub cmbrdept_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbrsubdept.SelectedIndexChanged

    End Sub

    Private Sub cmbrlineno_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbrlineno.SelectedIndexChanged

    End Sub

    Private Sub cmdrdisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdrdisp.Click
        qry = "select edate,empid,empname,itmgrp,subdept,[lineno] linno,operation,mactype,sam,totprod,curprod,totmin,wtime,mcdowntime" & vbCrLf _
            & ",electricaldowntime,other,totoffstdtime,onstdtime,samproduced,[PER] pper,effper eff,round(SUM(effper) over(partition by empid,empname),0) tot,uttime from " & Trim(mcostdbnam) & ".dbo.perfefficient"

        'qry = qry & " Where "


        If Len(Trim(cmbrsubdept.Text)) > 0 Or Len(Trim(cmbrlineno.Text)) > 0 Or mskrdatefr.MaskCompleted = True Then
            qry = qry & " Where "
        End If
        If mskrdatefr.MaskCompleted = True And mskrdateto.MaskCompleted = True Then
            qry = qry & " edate >='" & Format(CDate(mskrdatefr.Text), "yyyy-MM-dd") & "' and edate<='" & Format(CDate(mskrdateto.Text), "yyyy-MM-dd") & "' "
        End If
        If Len(Trim(cmbrsubdept.Text)) > 0 Then
            If mskrdatefr.MaskCompleted = True Or mskrdateto.MaskCompleted = True Then
                qry = qry & " and "
            End If

            qry = qry & " subdept='" & cmbrsubdept.Text & "'"
        End If
        If Len(Trim(cmbrlineno.Text)) > 0 Then

            If Len(Trim(cmbrsubdept.Text)) > 0 Then
                qry = qry & " and "
            End If
            qry = qry & " [lineno]='" & cmbrlineno.Text & "'"
        End If

        qry = qry & " order by subdept,[lineno],empid,empname"


        'msql = "select * from " & Trim(mcostdbnam) & ".dbo.operationmaster "

        'dgo.AllowUserToAddRows = False
        dgrclear()
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        cmd.CommandText = qry
        cmd.Connection = con
        da.SelectCommand = cmd
        Dim dt As New DataTable
        da.Fill(dt)
        dgr.DataSource = dt
        AddHandler Me.dgr.CellPainting, AddressOf Me.dgr_CellPainting

        'dgr.Columns(0).Width = 200
        'dgr.Columns(1).Width = 100
        'dgr.Columns(2).Width = 100
        'dgr.Columns(3).Width = 100
        'dgr.Columns(4).Width = 100

    End Sub

    Private Sub TabPage4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage4.Click
        Call loadsubdept()
    End Sub
    Private Sub dgrclear()
        For j = dgr.Rows.Count - 2 To 1 Step -1
            dgr.Rows.RemoveAt(j)
        Next

        'For j = dgo.Rows.Count - 2 To 1 Step -1
        dgr.Columns.Clear()
    End Sub

    Private Sub dgclear()
        For j = dg.Rows.Count - 2 To 1 Step -1
            dg.Rows.RemoveAt(j)
        Next

        'For j = dgo.Rows.Count - 2 To 1 Step -1
        dg.Columns.Clear()
    End Sub

    Private Sub MaskedTextBox1_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskrdateto.MaskInputRejected

    End Sub

    Private Sub cmdrcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdrcancel.Click
        dgrclear()
    End Sub

    Private Sub dgr_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgr.CellContentClick

    End Sub

    Private Sub dgr_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgr.CellPainting
        'merge the cell[1,1] and cell[2,1]
        If (e.RowIndex = 1) Then
            If (e.ColumnIndex = 1) Then
                e.PaintBackground(e.ClipBounds, True)
                Dim r As Rectangle = e.CellBounds
                Dim r1 As Rectangle = Me.dgr.GetCellDisplayRectangle(2, 1, True)
                r.Width = (r.Width _
                            + (r1.Width - 1))
                r.Height = (r.Height - 1)
                Dim brBk As SolidBrush = New SolidBrush(e.CellStyle.BackColor)
                Dim brFr As SolidBrush = New SolidBrush(e.CellStyle.ForeColor)
                e.Graphics.FillRectangle(brBk, r)
                Dim sf As StringFormat = New StringFormat
                sf.Alignment = StringAlignment.Center
                sf.LineAlignment = StringAlignment.Center
                'e.Graphics.DrawString("cell merged", e.CellStyle.Font, brFr, r, sf)
                e.Handled = True
            End If

            If (e.ColumnIndex = 20) Then
                Dim p As Pen = New Pen(Me.dgr.GridColor)
                e.Graphics.DrawLine(p, e.CellBounds.Left, (e.CellBounds.Bottom - 1), e.CellBounds.Right, (e.CellBounds.Bottom - 1))
                e.Graphics.DrawLine(p, (e.CellBounds.Right - 1), e.CellBounds.Top, (e.CellBounds.Right - 1), e.CellBounds.Bottom)
                e.Handled = True
            End If

        End If
    End Sub

    Private Sub cmdexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexcel.Click
        'gridexcelexport(dgr, 1)
        ngridexcelexport(dgr, 1, "PERFORMANCE REPORT", cmbrsubdept.Text & " Line No " & cmbrlineno.Text & " Date from " & Format(CDate(mskrdatefr.Text), "dd-MM-yyyy") & " To " & Format(CDate(mskrdateto.Text), "dd-MM-yyyy"))
    End Sub

    Private Sub cmbeffgrp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbeffgrp.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbempname.Focus()
        End If
    End Sub

    Private Sub cmbeffgrp_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbeffgrp.LostFocus
        loaddgoperation()
    End Sub

    Private Sub cmbeffgrp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbeffgrp.SelectedIndexChanged

    End Sub

    Private Sub cmbgroup_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbgroup.GotFocus
        cmbgroup.Items.Clear()
        cmbgroup.Items.Add("SHIRT")
        cmbgroup.Items.Add("PANT")
        cmbgroup.Items.Add("LS SHIRT")
        cmbgroup.Items.Add("KURTHA SET")
        cmbgroup.Items.Add("KURTHA TOP")
        cmbgroup.Items.Add("KURTHA BOTTOM")
        cmbgroup.Items.Add("SHORTS")
    End Sub

    Private Sub cmbgroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbgroup.SelectedIndexChanged

    End Sub

    Private Sub loaditmgrp()
        'msql = "select subdept from empmaster group by subdept order by subdept"

        Dim query = "Select isnull(itmgrp,'') itmgrp From " & Trim(mcostdbnam) & ".dbo.operationmaster group by itmgrp order by itmgrp"
        Dim dr As SqlDataReader

        dr = getDataReader(query)
        cmbeffgrp.DataSource = Nothing
        cmbeffgrp.Items.Clear()

        
        If dr.HasRows = True Then
            Dim dt As DataTable = New DataTable
            dt.Load(dr)
            cmbeffgrp.DataSource = dt
            cmbeffgrp.DisplayMember = "itmgrp"
            cmbeffgrp.ValueMember = "itmgrp"
        End If

        dr.Close()

    End Sub

    Private Sub txtcol_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcol.TextChanged

    End Sub
End Class
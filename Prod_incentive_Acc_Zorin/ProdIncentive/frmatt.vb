Imports System.IO
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class frmatt
    Dim msql, msql2 As String
    Dim j, i, k, msel As Integer

    Private Sub frmatt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        Call dvhead()
        Call flxhead()
    End Sub
    Private Sub dvhead()

        'DV.RowCount = 1
        'DV.ColumnCount = 3

        Dim Depart As New DataGridViewComboBoxColumn
        Dim present As New DataGridViewTextBoxColumn
        'Dim colorcode As New DataGridViewTextBoxColumn
        'Dim colorname As New DataGridViewTextBoxColumn
        'Dim colimg As New DataGridViewImageColumn
        'Dim coltype As New DataGridViewTextBoxColumn
        'Dim btn As New DataGridViewButtonColumn()

        'Dim inImg As New DataGridViewImageCell()
        'colimg.HeaderText = "Image"
        'colimg.Name = "img"
        'colimg.ImageLayout = DataGridViewImageCellLayout.Stretch

        'colorcode.ValueType = GetType(String)
        'colorcode.HeaderText = "Brand"


        'greltype.Name = "Reltype"

        msql = "select department from tmpdepartment"
        Dim CMD As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()
        'Try
        ''Dim DR As SqlDataReader
        Dim DR As SqlDataReader
        DR = CMD.ExecuteReader
        If DR.HasRows = True Then
            With Depart
                Depart.HeaderText = "Department"
                While DR.Read
                    Depart.Items.Add(DR.Item("department") & vbNullString)
                End While
                Depart.AutoComplete = True
            End With

        End If


        present.ValueType = GetType(Integer)
        present.HeaderText = "Present"

        With dv
            dv.Columns.Add(Depart)
            dv.Columns.Add(present)
            
        End With
        dv.ColumnHeadersDefaultCellStyle.Font = New Font(dv.Font, FontStyle.Bold)
        dv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dv.Columns(0).Width = 200
        dv.Columns(1).Width = 70
        'dv.Columns(2).Width = 70
        'dv.Columns(3).Width = 200
        'dv.Columns(4).Width = 100
        'dv.Columns(5).Width = 70

        'Dim row As DataGridViewRow = dvproof.Rows(0)
        'row.Height = 25
        'colimg.ImageLayout = DataGridViewImageCellLayout.Zoom
        'dvproof.ReadOnly = True
        dv.AllowUserToAddRows = False
        'dvproof.Rows.Add()

    End Sub

    Private Sub dispform()
        'DV.AutoGenerateColumns = False

        Dim j As Integer
        ' Call dvproofhead()
        Dim darlf As New SqlDataAdapter
        Dim dsrlf As New DataSet

        msql = "select * from tmpattpresent where docdate='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"

        'If Len(Trim(txtcolcode.Text)) > 0 Then
        '    If msel = 3 Then
        '        'qryCustomers = "SELECT * FROM RRCOLOR.dbo.colormast WHERE colcode = '" & txtcolcode.Text & "'"
        '        qryCustomers = "SELECT * FROM RRCOLOR.dbo.colormast WHERE mktcode = '" & txtcolcode.Text & "'"
        '    Else
        '        'qryCustomers = "SELECT * FROM RRCOLOR.dbo.colormast"
        '        'qryCustomers = "SELECT * FROM RRCOLOR.dbo.colormast WHERE colcode = '" & txtcolcode.Text & "'"
        '        qryCustomers = "SELECT * FROM RRCOLOR.dbo.colormast WHERE mktcode = '" & txtcolcode.Text & "'"
        '    End If
        'Else
        '    qryCustomers = "SELECT * FROM RRCOLOR.dbo.colormast"
        'End If


        darlf.SelectCommand = New SqlCommand(msql, con)

        Dim cb1 As SqlCommandBuilder = New SqlCommandBuilder(darlf)

        darlf.Fill(dsrlf, "tmpattpresent")

        Dim dtdf As DataTable = dsrlf.Tables("tmpattpresent")

        Try


            With dtdf
                If .Rows.Count > 0 Then
                    For k = 0 To .Rows.Count - 1

                        j = dv.Rows.Add()

                        dv.Rows.Item(j).Cells(0).Value = .Rows(k)("department") & vbNullString
                        dv.Rows.Item(j).Cells(1).Value = .Rows(k)("attpresent") & vbNullString
                        'dv.Rows.Item(j).Cells(2).Value = .Rows(k)("colcode") & vbNullString
                        'dv.Rows.Item(j).Cells(3).Value = .Rows(k)("colorname") & vbNullString
                        'dv.Rows.Item(j).Cells(4).Value = .Rows(k)("ctype") & vbNullString
                    Next k
                End If
            End With

            darlf.Dispose()
            dsrlf.Dispose()
            dtdf.Dispose()
        Catch ex As sqlException
            MsgBox(ex.ToString)
        Finally


        End Try
    End Sub


    Private Sub saveattachment()


        If msel = 2 Then
            'Dim CMD2 As New SqlCommand("delete from VinHR_Img.dbo.empform where empid=" & Val(Txtempcode.Text), con)
            '                                  & Microsoft.VisualBasic.Format(CDate(mskddate.Text), "yyyy-MM-dd") & "' where docnum=" & Val(flxv.get_TextMatrix(k, 0)), con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            'Dim CMD2 As New OleDbCommand("delete from RRCOLOR.dbo.colormast where colcode='" & Val(txtcolcode.Text) & "'", con)
            Dim CMD2 As New SqlCommand("delete from tmpattpresent where docdate='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'", con)

            'Dim CMD2 As New OleDbCommand("delete from RRCOLOR.dbo.colormast ", con)
            CMD2.ExecuteNonQuery()
            CMD2.Dispose()
        End If




        msql2 = "select * from tmpattpresent"
        'ElseIf msel = 2 Then
        'Call delrecord2("select * from VinHR_Img.dbo.empphoto where empid=" & Val(Txtempcode.Text))
        'sqlqry = "select * from VinHR_Img.dbo.empphoto where empid=" & Val(Txtempcode.Text)
        'sqlqry = "select * from VinHR_Img.dbo.empphoto"

        'End If

        Dim dafmg As New SqlDataAdapter
        Dim dsfmg As New DataSet
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        dafmg.SelectCommand = New SqlCommand(msql2, con)
        Dim cb2 As SqlCommandBuilder = New SqlCommandBuilder(dafmg)
        dafmg.Fill(dsfmg, "tmpattpresent")
        Dim dtmg As DataTable = dsfmg.Tables("tmpattpresent")
        Try
            ' add a row
            Dim newRow As DataRow

            For j = 0 To (dv.RowCount - 1)
                If Len(Trim(dv.Rows.Item(j).Cells(0).Value)) > 0 Then
                    newRow = dtmg.NewRow()
                    'newRow("colorcode") = txtcolcode.Text & vbNullString
                    newRow("docdate") = CDate(mskdate.Text)
                    newRow("department") = dv.Rows.Item(j).Cells(0).Value & vbNullString
                    newRow("attpresent") = Val(dv.Rows.Item(j).Cells(1).Value)

                    dtmg.Rows.Add(newRow)
                End If
            Next
            '    End With
            dafmg.Update(dsfmg, "tmpattpresent")
            MsgBox("Attachement successfully saved.", MsgBoxStyle.Information)
            cb2.Dispose()
            dtmg.Dispose()
            dafmg.Dispose()
            dsfmg.Dispose()
        Catch ex As sqlException
            'MsgBox(mcode)
            MsgBox(ex.ToString)
            'mcode = ""
        End Try



    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        msel = 1
        cmdclear.PerformClick()
        mskdate.Focus()

    End Sub

    Private Sub cmdedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdedit.Click
        msel = 2
        mskdate.Focus()
    End Sub

    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.Click
        msel = 3
        'If Len(Trim(txtcolcode.Text)) > 0 Then
        mskdate.Focus()

        'End If
    End Sub

    Private Sub cmddisp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmddisp.KeyPress
        If msel > 1 Then
            If Asc(e.KeyChar) = 13 Then
                Call dispform()
            End If
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Call saveattachment()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        Dim i As Integer
        For i = 0 To Me.dv.Rows.Count - 1
            Me.dv.Rows(0).Selected = True
            Me.dv.Rows(0).Dispose()
            Me.dv.Rows.RemoveAt(Me.dv.SelectedRows(0).Index)
        Next
        mskdate.Text = ""

    End Sub

    Private Sub CmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdExit.Click
        Me.Close()
    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage1.Click
        Call dploaddata()
    End Sub

    Private Sub flxhead()
        flx.Rows = 1
        flx.Cols = 1
        flx.set_ColWidth(0, 2500)
        

        flx.Row = 0
        flx.Col = 0
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 0, "Department")

       
    End Sub


    Private Sub SAVEREC()



        msql = "select * from tmpdepartment "


        'msql = "delete from purchasehead where purcno=" & Val(txtbno.Text) & " and cmp_id='" & mcmpid & "' and cmpyr_id='" & mcmpyrid & "'"
        'msql2 = "delete from purchaseline where purcno=" & Val(txtbno.Text) & " and cmp_id='" & mcmpid & "' and cmpyr_id='" & mcmpyrid & "'"

        Dim CMD As New SqlCommand("delete from tmpdepartment", con)
        'Dim CMD2 As New OleDb.OleDbCommand("delete from zdesp1 where docentry=" & Val(txtno.Text), con)


        Dim DA As New SqlDataAdapter(msql, con)
        Dim CB As New SqlCommandBuilder(DA)
        Dim DS As New DataSet

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim TRANS As SqlTransaction = con.BeginTransaction

        Try

            CMD.Transaction = TRANS
            CMD.ExecuteNonQuery()


            DA.SelectCommand.Transaction = TRANS



            For j = 1 To flx.Rows - 1
                If Len(Trim(flx.get_TextMatrix(j, 0))) > 0 Then
                    DA.Fill(DS, "tmpdepartment")
                    Dim DSRW As DataRow
                    DSRW = DS.Tables("tmpdepartment").NewRow
                    DSRW.Item("department") = flx.get_TextMatrix(j, 0) & vbNullString
                    DS.Tables("tmpdepartment").Rows.Add(DSRW)
                End If
            Next j

            DA.Update(DS, "tmpdepartment")

            TRANS.Commit()
            DS.Dispose()
            DA.Dispose()


            MsgBox("SUCCESSFULLY SAVED!")
            'If Microsoft.VisualBasic.MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            '    'Call TAKPRINT()
            'End If
            cmdclear.PerformClick()

            CLEAR(Me)
            disable(Me)
            cmdSave.Enabled = False
            If flx.Enabled = False Then flx.Enabled = True

        Catch EX As FieldAccessException
            If Not TRANS Is Nothing Then
                TRANS.Rollback()
            End If
            DS.Dispose()
            DA.Dispose()
            MsgBox(EX.Message)
            CLEAR(Me)
            disable(Me)
            cmdSave.Enabled = False
        Catch EX As ExecutionEngineException
            If Not TRANS Is Nothing Then
                TRANS.Rollback()
            End If
            'trans.Rollback()
            DS.Dispose()
            DA.Dispose()
            MsgBox(EX.Message)
            CLEAR(Me)
            disable(Me)
            cmdSave.Enabled = False
        Catch EX As ApplicationException
            If Not TRANS Is Nothing Then
                TRANS.Rollback()
            End If
            'trans.Rollback()
            DS.Dispose()
            DA.Dispose()
            MsgBox(EX.Message)
            CLEAR(Me)
            disable(Me)
            cmdSave.Enabled = False
        Catch EX As ArgumentException
            If Not TRANS Is Nothing Then
                TRANS.Rollback()
            End If
            'trans.Rollback()
            DS.Dispose()
            DA.Dispose()
            MsgBox(EX.Message)
            CLEAR(Me)
            disable(Me)
            cmdSave.Enabled = False
        Catch EX As sqlException
            If Not TRANS Is Nothing Then
                TRANS.Rollback()
            End If
            'If MSEL = 2 Then
            ' trans.Rollback()
            ' End If
            'MsgBox(EX.Message)
            DS.Dispose()
            DA.Dispose()


        Catch EX As ConstraintException
            'trans.Rollback()
            If Not TRANS Is Nothing Then
                TRANS.Rollback()
            End If
            DS.Dispose()
            DA.Dispose()
            MsgBox(EX.Message)
            CLEAR(Me)
            disable(Me)
            cmdSave.Enabled = False
        Catch EX As Exception
            If Not TRANS Is Nothing Then
                TRANS.Rollback()
            End If
            MsgBox(EX.Message)
            DS.Dispose()
            DA.Dispose()
            CLEAR(Me)
            disable(Me)
            cmdSave.Enabled = False
        Finally

            CMD.Dispose()
            TRANS.Dispose()
        End Try


        CLEAR(Me)
        'enable(Me)
        If flx.Enabled = False Then flx.Enabled = True
        
    End Sub


    Private Sub butsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butsave.Click
        Call SAVEREC()
    End Sub

    Private Sub flx_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flx.Enter

    End Sub

    Private Sub flx_KeyPressEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyPressEvent) Handles flx.KeyPressEvent
        editflx(flx, e.keyAscii, butsave)

    End Sub

    Private Sub dploaddata()
        Call flxhead()
        Dim CMD1 As New SqlCommand("SELECT * FROM tmpdepartment", con)

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim DR1 As SqlDataReader

        DR1 = CMD1.ExecuteReader
        If DR1.HasRows = True Then

            With flx
                While DR1.Read
                    .Rows = .Rows + 1
                    .Row = .Rows - 1
                    .set_TextMatrix(.Row, 0, DR1.Item("department") & vbNullString)
                    
                End While
            End With

        End If
        DR1.Close()
        CMD1.Dispose()
    End Sub

    Private Sub TabPage1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage1.DoubleClick

    End Sub

    Private Sub TabPage1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPage1.GotFocus
        Call dploaddata()
    End Sub

    Private Sub dv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dv.CellContentClick

    End Sub

    Private Sub dv_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dv.KeyDown
        If e.KeyCode = Keys.F2 Then
            dv.Rows.Add()
        End If
        If e.KeyCode = Keys.F9 Then
            dv.Rows.Remove(dv.CurrentRow)
        End If
    End Sub

    Private Sub mskdate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mskdate.KeyPress
        If msel > 1 Then
            If Asc(e.KeyChar) = 13 Then
                Call dispform()
            End If
        End If
    End Sub

    Private Sub mskdate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskdate.MaskInputRejected

    End Sub
End Class
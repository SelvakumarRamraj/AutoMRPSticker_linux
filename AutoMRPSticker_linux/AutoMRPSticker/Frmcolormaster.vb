Imports System.IO
Imports System.Math
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.VbStrConv
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms


Public Class Frmcolormaster
    Dim arr() As Byte
    Dim k, j As Int32
    Private m_SelectedStyle As DataGridViewCellStyle
    Private m_SelectedRow As Integer = -1
    Dim msel As Int16
    Dim sqlqry, mcode, msql As String
    Dim filepath, tmpstr, qryCustomers As String
    Dim argb As Integer


    Private Sub Frmcolormaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIParent1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        checkConnection()
        Call dvhead()
        Call loadcombobox()
        Label3.BackColor = System.Drawing.ColorTranslator.FromHtml("#9FC12B")
        'msql = "select u_name from [@incm_bnd1] where isnull(u_name,'')<>'' order by u_name "
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If
        'Dim CMD As New OleDbCommand(msql, con)
        'cmbbrand.Items.Clear()
        'Try

        '    Dim DR As OleDbDataReader
        '    DR = CMD.ExecuteReader

        '    If DR.HasRows = True Then

        '        While DR.Read
        '            cmbbrand.Items.Add(DR.Item("u_name"))
        '        End While
        '    End If
        'Catch ex As Exception
        'End Try
    End Sub
    Private Sub loadcombobox()
        msql = "select u_name from [@incm_bnd1] where isnull(u_name,'')<>'' order by u_name "
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim CMD As New SqlCommand(msql, con)
        cmbbrand.Items.Clear()
        Try

            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader

            If DR.HasRows = True Then

                While DR.Read
                    cmbbrand.Items.Add(DR.Item("u_name"))
                End While
            End If
            DR.Close()
        Catch ex As Exception

            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub dvhead()

        'DV.RowCount = 1
        'DV.ColumnCount = 3

        Dim brand As New DataGridViewComboBoxColumn
        Dim mktcolorname As New DataGridViewTextBoxColumn
        Dim colorcode As New DataGridViewTextBoxColumn
        Dim colorname As New DataGridViewTextBoxColumn
        Dim colimg As New DataGridViewImageColumn
        Dim coltype As New DataGridViewTextBoxColumn
        'Dim btn As New DataGridViewButtonColumn()

        Dim inImg As New DataGridViewImageCell()
        colimg.HeaderText = "Image"
        colimg.Name = "img"
        colimg.ImageLayout = DataGridViewImageCellLayout.Stretch

        Dim pantoncol As New DataGridViewTextBoxColumn
        Dim coldisp As New DataGridViewTextBoxColumn
        Dim brandgrp As New DataGridViewTextBoxColumn
        'colorcode.ValueType = GetType(String)
        'colorcode.HeaderText = "Brand"

        With brand
            'greltype.Name = "Reltype"

            brand.HeaderText = " Brand"


            msql = "select u_name from [@incm_bnd1] where isnull(u_name,'')<>'' order by u_name "
            checkConnection()

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim CMD As New SqlCommand(msql, con)

            Try

                Dim DR As SqlDataReader
                DR = CMD.ExecuteReader

                If DR.HasRows = True Then

                    While DR.Read
                        brand.Items.Add(DR.Item("u_name"))
                    End While
                End If
                DR.Close()
            Catch ex As Exception
            End Try

            'brand.Items.Add("RAMRAJ SHIRT")
            'brand.Items.Add("RAMRAJ GENXT")
            'brand.Items.Add("LINEN PARK")
            'brand.Items.Add("RAMRAJ LAGNAA")
            'brand.Items.Add("VIVEAGA SHIRT")
            'brand.Items.Add("RAMRAJ HANKEYS")
            'brand.Items.Add("RAMRAJ DESIGNER WEAR")
            'brand.Items.Add("RAMRAJ HANKEYS")

            brand.AutoComplete = True

        End With


        mktcolorname.ValueType = GetType(String)
        mktcolorname.HeaderText = "Mkt.Color Code"

        colorcode.ValueType = GetType(String)
        colorcode.HeaderText = "Prod.Color Code"

        colorname.ValueType = GetType(String)
        colorname.HeaderText = "Color.Name"

        coltype.ValueType = GetType(String)
        coltype.HeaderText = "CType"

        'DataGridView1.Columns.Add(btn)
        'btn.HeaderText = "Select Image"
        'btn.Text = "Click Here"
        'btn.Name = "btn"
        'btn.UseColumnTextForButtonValue = True
        pantoncol.ValueType = GetType(String)
        pantoncol.HeaderText = "Pantone"

        coldisp.ValueType = GetType(String)
        coldisp.HeaderText = "Color"
        brandgrp.ValueType = GetType(String)
        brandgrp.HeaderText = "Brand Group"

        With dv
            dv.Columns.Add(brand)
            dv.Columns.Add(mktcolorname)
            dv.Columns.Add(colorcode)
            dv.Columns.Add(colorname)
            'dv.Columns.Add(colimg)
            dv.Columns.Add(coltype)
            dv.Columns.Add(colimg)
            dv.Columns.Add(pantoncol)
            dv.Columns.Add(coldisp)
            dv.Columns.Add(brandgrp)
            'dvproof.Columns.Add(btn)
        End With
        dv.ColumnHeadersDefaultCellStyle.Font = New Font(dv.Font, FontStyle.Bold)
        dv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dv.Columns(0).Width = 200
        dv.Columns(1).Width = 70
        dv.Columns(2).Width = 70
        dv.Columns(3).Width = 200
        dv.Columns(4).Width = 100
        dv.Columns(5).Width = 70
        dv.Columns(6).Width = 70
        dv.Columns(7).Width = 60
        dv.Columns(8).Width = 150

        'Dim row As DataGridViewRow = dvproof.Rows(0)
        'row.Height = 25
        colimg.ImageLayout = DataGridViewImageCellLayout.Zoom
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



        'If Len(Trim(txtcolcode.Text)) > 0 Or Len(Trim(cmbbrand.Text)) = 0 Then

        'If Len(Trim(txtcolcode.Text)) > 0 And Len(Trim(cmbbrand.Text)) = 0 Then
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

        qryCustomers = "SELECT * FROM RRCOLOR.dbo.colormast"

        If Len(Trim(txtcolcode.Text)) > 0 Or Len(Trim(cmbbrand.Text)) > 0 Then
            qryCustomers = Trim(qryCustomers) & " where "
        End If
        If Len(Trim(txtcolcode.Text)) > 0 Then
            qryCustomers = Trim(qryCustomers) & " mktcode='" & txtcolcode.Text & "'"
        End If
        If Len(Trim(cmbbrand.Text)) > 0 Then
            If Len(Trim(txtcolcode.Text)) > 0 Then
                qryCustomers = Trim(qryCustomers) & " and "
            End If
            qryCustomers = Trim(qryCustomers) & " brand='" & Trim(cmbbrand.Text) & "'"
        End If


        darlf.SelectCommand = New SqlCommand(qryCustomers, con)

        Dim cb1 As SqlCommandBuilder = New SqlCommandBuilder(darlf)

        darlf.Fill(dsrlf, "RRCOLOR.dbo.colormast")

        Dim dtdf As DataTable = dsrlf.Tables("RRCOLOR.dbo.colormast")

        Try


            With dtdf
                If .Rows.Count > 0 Then
                    For k = 0 To .Rows.Count - 1

                        j = dv.Rows.Add()

                        dv.Rows.Item(j).Cells(0).Value = .Rows(k)("brand") & vbNullString
                        dv.Rows.Item(j).Cells(1).Value = .Rows(k)("mktcode") & vbNullString
                        dv.Rows.Item(j).Cells(2).Value = .Rows(k)("colcode") & vbNullString
                        dv.Rows.Item(j).Cells(3).Value = .Rows(k)("colorname") & vbNullString
                        dv.Rows.Item(j).Cells(4).Value = .Rows(k)("ctype") & vbNullString
                        dv.Rows.Item(j).Cells(6).Value = .Rows(k)("remark") & vbNullString
                        If Len(Trim(dv.Rows.Item(j).Cells(6).Value)) > 0 Then
                            'dv.Rows(j).Cells(7).Style.BackColor = System.Drawing.ColorTranslator.FromHtml(.Rows(k)("remark"))

                            Dim colorValue As String = Trim(.Rows(k)("remark").ToString())

                            If Not colorValue.StartsWith("#") Then
                                colorValue = "#" & colorValue
                            End If
                            dv.Rows(j).Cells(7).Style.BackColor = System.Drawing.ColorTranslator.FromHtml(colorValue)

                            'dv.Columns.Item(1).DefaultCellStyle.BackColor = Color.Blue
                        End If
                        dv.Rows.Item(j).Cells(8).Value = .Rows(k)("name") & vbNullString
                        If IsDBNull(.Rows(k)("colimage")) = False Then
                            arr = .Rows(k)("colimage")
                            ' bmpImage = DirectCast(Image.FromFile(filepath, True), Bitmap)

                            ' PictureBox1.Image = PictureBox1.Image.FromStream(New IO.MemoryStream(arr))
                            Try
                                dv.Rows.Item(j).Cells(5).Value = System.Drawing.Image.FromStream(New IO.MemoryStream(arr))
                                arr = Nothing
                            Catch ex As Exception
                                arr = Nothing
                            End Try
                        End If
                    Next k
                End If





            End With

            darlf.Dispose()
            dsrlf.Dispose()
            dtdf.Dispose()
        Catch ex As OleDbException
            MsgBox(ex.ToString)
        Finally


        End Try
    End Sub


    Private Sub saveattachment()
        If msel = 1 Or msel = 2 Then
            'If Len(Trim(txtcolcode.Text)) > 0 Or Len(Trim(cmbbrand.Text)) > 0 Then
            If dv.Rows.Count > 0 Then
                If msel = 2 Then
                    'Dim CMD2 As New SqlCommand("delete from VinHR_Img.dbo.empform where empid=" & Val(Txtempcode.Text), con)
                    '                                  & Microsoft.VisualBasic.Format(CDate(mskddate.Text), "yyyy-MM-dd") & "' where docnum=" & Val(flxv.get_TextMatrix(k, 0)), con)
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If
                    ''Dim CMD2 As New OleDbCommand("delete from RRCOLOR.dbo.colormast where colcode='" & Val(txtcolcode.Text) & "'", con)

                    'Dim CMD2 As New OleDbCommand("delete from RRCOLOR.dbo.colormast where mktcode='" & Trim(txtcolcode.Text) & "'", con)


                    msql = "delete  FROM RRCOLOR.dbo.colormast"
                    If Len(Trim(txtcolcode.Text)) > 0 Or Len(Trim(cmbbrand.Text)) > 0 Then
                        msql = Trim(msql) & " where "
                    End If
                    If Len(Trim(txtcolcode.Text)) > 0 Then
                        msql = Trim(msql) & " mktcode='" & txtcolcode.Text & "'"
                    End If
                    If Len(Trim(cmbbrand.Text)) > 0 Then
                        If Len(Trim(txtcolcode.Text)) > 0 Then
                            msql = Trim(msql) & " and "
                        End If
                        msql = Trim(msql) & " brand='" & cmbbrand.Text & "'"
                    End If

                    Dim CMD2 As New SqlCommand(msql, con)
                    If Len(Trim(txtcolcode.Text)) = 0 And Len(Trim(cmbbrand.Text)) = 0 Then
                        If MsgBox("Delete all Record ?", vbYesNo) = vbYes Then

                            'CMD2.ExecuteNonQuery()
                            CMD2.Dispose()
                        Else
                            CMD2.Dispose()
                        End If
                    Else
                        CMD2.ExecuteNonQuery()
                        CMD2.Dispose()
                    End If


                End If




                sqlqry = "select * from RRCOLOR.dbo.colormast"
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
                dafmg.SelectCommand = New SqlCommand(sqlqry, con)
                Dim cb2 As SqlCommandBuilder = New SqlCommandBuilder(dafmg)
                dafmg.Fill(dsfmg, "RRCOLOR.dbo.colormast")
                Dim dtmg As DataTable = dsfmg.Tables("RRCOLOR.dbo.colormast")
                'If Val(TXTEMPID.Text) = 0 Then
                '    MsgBox("Please fill up color code .", MsgBoxStyle.Critical)
                '    Exit Sub
                'End If
                'If Len(Trim(filepath)) > 0 Then
                '    'If Trim(txtFileName.Text) = "" Then Exit Sub
                '    FileOpen(1, filepath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
                '    'Resize array so that it can accomodate the file
                '    ReDim arr(FileLen(filepath) - 1)
                '    FileGet(1, arr)
                '    FileClose(1)
                'End If
                Try
                    ' add a row
                    Dim newRow As DataRow
                    'newRow = dt2.NewRow()
                    'newRow("empid") = Val(Txtempcode.Text)
                    'If Len(Trim(filepath)) > 0 Then
                    '    newRow("empimage") = GetResizedImage(arr, 188, 250)
                    'Else
                    '    newRow("empimage") = arr
                    'End If

                    ''dt2.Rows.Add(newRow)

                    ''Dim newRow As DataRow

                    For j = 0 To (dv.RowCount - 1)
                        If Len(Trim(dv.Rows.Item(j).Cells(0).Value)) > 0 Then
                            'Dim Stream As MemoryStream = New MemoryStream
                            'Dim filename As String = dvproof.Rows.Item(j).Cells(1).Value
                            'Dim ms As MemoryStream = New MemoryStream(CType(dvproof.Rows.Item(j).Cells(1).Value, Byte()))
                            'picProduct.Image = Image.FromStream(MS)
                            ' Dim ms As MemoryStream = New MemoryStream( dvproof.Rows.Item(j).Cells(1).Value)

                            ' Dim fimage As Bitmap = New Bitmap(ms)
                            'fimage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                            ''Dim pic() As Byte = New Byte(image)
                            newRow = dtmg.NewRow()
                            'newRow("colorcode") = txtcolcode.Text & vbNullString
                            newRow("brand") = dv.Rows.Item(j).Cells(0).Value & vbNullString
                            newRow("mktcode") = dv.Rows.Item(j).Cells(1).Value & vbNullString

                            newRow("colcode") = dv.Rows.Item(j).Cells(2).Value & vbNullString
                            mcode = dv.Rows.Item(j).Cells(2).Value & vbNullString
                            newRow("colorname") = dv.Rows.Item(j).Cells(3).Value & vbNullString
                            newRow("ctype") = dv.Rows.Item(j).Cells(4).Value & vbNullString
                            newRow("remark") = dv.Rows.Item(j).Cells(6).Value & vbNullString
                            newRow("name") = dv.Rows.Item(j).Cells(8).Value & vbNullString
                            'newRow("formimg") = imageToByteArray(fimage)
                            'Dim img() As Byte = CType(dv.Rows.Item(j).Cells(4).Value, Byte())
                            'If (img Is Nothing) Then
                            Dim obj As New Object
                            obj = dv.Rows.Item(j).Cells(5).Value

                            If obj Is Nothing Then
                                'If dv.Rows.Item(j).Cells(4).Value = Nothing Then
                                'If rv.Cells(i).Value Is Nothing OrElse rw.Cells(i).Value = DBNull.Value OrElse String.IsNullOrWhitespace(rw.Cells(i).Value.ToString()) Then
                                newRow("colimage") = Nothing
                                'newRow("colimage") = imageToByteArray(dv.Rows.Item(j).Cells(4).Value)
                            Else
                                newRow("colimage") = imageToByteArray(dv.Rows.Item(j).Cells(5).Value)
                                'newRow("colimage") = Nothing
                            End If

                            dtmg.Rows.Add(newRow)
                        End If
                    Next
                    '    End With
                    dafmg.Update(dsfmg, "RRCOLOR.dbo.Colormast")
                    MsgBox("Attachement successfully saved.", MsgBoxStyle.Information)
                    cb2.Dispose()
                    dtmg.Dispose()
                    dafmg.Dispose()
                    dsfmg.Dispose()
                    msel = 0
                    cmdclear.PerformClick()
                Catch ex As OleDbException
                    MsgBox(mcode)
                    MsgBox(ex.ToString)
                    mcode = ""
                    msel = 0
                    cmdclear.PerformClick()
                End Try
            End If

            'End If

        End If


    End Sub

    Private Sub dv_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dv.CellBeginEdit
        If Len(Trim(dv.Rows.Item(e.RowIndex).Cells(6).Value)) > 0 Then
            dv.Rows(e.RowIndex).Cells(7).Style.BackColor = System.Drawing.ColorTranslator.FromHtml(Trim(dv.Rows.Item(e.RowIndex).Cells(6).Value))
            'dv.Columns.Item(1).DefaultCellStyle.BackColor = Color.Blue
        End If
    End Sub

    Private Sub dv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dv.CellContentClick
        If TypeOf dv.Columns(e.ColumnIndex) Is DataGridViewImageColumn AndAlso e.RowIndex >= 0 Then
            'TODO - Button Clicked - Execute Code Here
            fd.Title = "Select your Image."
            fd.InitialDirectory = "C:\"
            fd.Filter = "Image Files|*.gif;*.jpg;*.png;*.bmp;"
            fd.RestoreDirectory = False
            Dim bmpImage As Bitmap = Nothing
            Dim inimg2 As New DataGridViewImageCell
            If fd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                inimg2.Value = Image.FromFile(fd.FileName)
            End If
            filepath = fd.FileName
            If filepath <> "OpenFileDialog1" Then
                ' proofimg.ImageLayout = DataGridViewImageCellLayout.Stretch
                bmpImage = DirectCast(Image.FromFile(filepath, True), Bitmap)
                dv.Rows(e.RowIndex).Cells(5).Value = bmpImage
            End If

            ' DirectCast(dvproof.Columns(1), DataGridViewImageColumn).ImageLayout = DataGridViewImageCellLayout.Zoom
            'dvproof.Rows(0).Cells.Add(inimg)
            'filepath = fd.FileName
        End If
    End Sub

    Private Sub dv_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dv.CellEndEdit
        If Len(Trim(dv.Rows.Item(e.RowIndex).Cells(6).Value)) > 0 Then
            dv.Rows(e.RowIndex).Cells(7).Style.BackColor = System.Drawing.ColorTranslator.FromHtml(Trim(dv.Rows.Item(e.RowIndex).Cells(6).Value))
            'dv.Columns.Item(1).DefaultCellStyle.BackColor = Color.Blue
        End If
    End Sub

    Private Sub dv_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dv.CellMouseDown
        Select Case e.Button

            Case System.Windows.Forms.MouseButtons.Left

                'Record_Click(sender, New System.EventArgs())

            Case System.Windows.Forms.MouseButtons.Middle

                'Stop_Click(sender, New System.EventArgs())

            Case System.Windows.Forms.MouseButtons.Right
                If e.ColumnIndex = 5 Then
                    'If dvproof.CurrentRow.Cells(1).Value = Nullable Then
                    ' Try
                    ' Dim ms As MemoryStream = New MemoryStream(CType(dvproof.Rows(e.RowIndex).Cells(1).Value, Byte()))

                    'System.Drawing.Image.FromStream(New IO.MemoryStream(arr))
                    PictureBox2.Image = dv.Rows(e.RowIndex).Cells(5).Value
                    'System.Drawing.Image.FromStream(New IO.MemoryStream(arr))
                    'Catch
                    'End Try
                    'End If
                End If
                'Info_Click(sender, New System.EventArgs())

        End Select
    End Sub

    Private Sub dv_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dv.DataError
        If (e.Exception.Message = "DataGridViewComboBoxCell value is not valid.") Then
            Dim value As Object = dv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & vbNullString
            If Not CType(dv.Columns(e.ColumnIndex), DataGridViewComboBoxColumn).Items.Contains(Text) Then
                CType(dv.Columns(e.ColumnIndex), DataGridViewComboBoxColumn).Items.Add(Text)
                e.ThrowException = False
            End If

        End If
    End Sub

    Private Sub cmddwnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddwnload.Click
        sd.Title = "Download file"
        sd.InitialDirectory = "C:\"
        sd.FileName = "*.jpg"
        sd.Filter = "Image Files|*.gif;*.jpg;*.png;*.bmp;"
        'fd.RestoreDirectory = False

        If sd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            PictureBox2.Image.Save(sd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg)
        End If
        'filepath = fd.FileName
    End Sub

    Private Sub dv_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dv.KeyDown
        If e.KeyCode = Keys.F2 Then
            dv.Rows.Add()
        End If
        If e.KeyCode = Keys.F9 Then
            dv.Rows.Remove(dv.CurrentRow)
        End If
    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        msel = 1
        'cmdclear.PerformClick()
        dv.Focus()
        dv.Rows.Add()

    End Sub

    Private Sub cmdedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdedit.Click
        msel = 2
        txtcolcode.Focus()


    End Sub

    Private Sub cmddel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddel.Click
        'Dim i As Integer
        'For i = 0 To Me.dv.Rows.Count - 1
        '    Me.dv.Rows(0).Selected = True
        '    Me.dv.Rows(0).Dispose()
        '    Me.dv.Rows.RemoveAt(Me.dv.SelectedRows(0).Index)
        'Next

    End Sub

    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.Click
        msel = 3
        'If Len(Trim(txtcolcode.Text)) > 0 Then
        Call dispform()
        'End If


    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Call saveattachment()
    End Sub

    Private Sub CmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        Dim i As Integer
        For i = 0 To Me.dv.Rows.Count - 1
            Me.dv.Rows(0).Selected = True
            Me.dv.Rows(0).Dispose()
            Me.dv.Rows.RemoveAt(Me.dv.SelectedRows(0).Index)
        Next
        txtcolcode.Text = ""
        PictureBox2.Image = Nothing
        msel = 0
    End Sub

    Private Sub txtcolcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcolcode.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If Len(Trim(txtcolcode.Text)) > 0 Then
                Call dispform()
            End If
        End If
    End Sub

    Private Sub txtcolcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcolcode.TextChanged

    End Sub

    Private Sub cmdedit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmdedit.KeyPress

    End Sub
    'gridexcelexport(dv, 1)

    Private Sub cmdexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexcel.Click
        gridexcelexport(dv, 1)
    End Sub

    Private Sub Label71_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label71.Click

    End Sub

    Private Sub sd_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles sd.FileOk

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ColorDialog1.ShowDialog <> System.Windows.Forms.DialogResult.Cancel Then
            'Label1.ForeColor = ColorDialog1.Color
            argb = ColorDialog1.Color.ToArgb()
            'txthtmlcol.Text = argb
            Dim clr As Color
            clr = Color.FromArgb(argb)

            Dim wc As String = ColorTranslator.ToHtml(clr).ToString()
            txthtmlcol.Text = wc
            'Dim conv As New ColorConverter
            'Dim c As Color = ColorDialog1.Color
            'Dim s As String = conv.ConvertToString(c)
            'Dim h As String = Hex(c.ToArgb)
            'txthtmlcol.Text = h
        End If
        'argb = ColorDialog1.Color.ToArgb()
    End Sub

    Private Sub cmbbrand_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbbrand.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If Len(Trim(cmbbrand.Text)) > 0 Then
                Call dispform()
                'cmdSave.Focus()
            End If
        End If
    End Sub

    Private Sub cmbbrand_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbbrand.SelectedIndexChanged

    End Sub

    Private Sub txthtmlcol_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txthtmlcol.TextChanged
        Label3.BackColor = System.Drawing.ColorTranslator.FromHtml("#9FC12B")
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub
End Class
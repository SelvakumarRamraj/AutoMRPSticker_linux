Imports System.IO
Imports System.Math
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.VbStrConv
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class Frmpurcscan
    Dim arr() As Byte
    Dim k, j As Int32
    Private m_SelectedStyle As DataGridViewCellStyle
    Private m_SelectedRow As Integer = -1
    Dim msel As Int16
    Dim sqlqry, msql, mcode As String
    Dim filepath, tmpstr, qryCustomers As String

    Private Sub Frmpurcscan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        Call dvhead()
    End Sub
    Private Sub dvhead()

        'DV.RowCount = 1
        'DV.ColumnCount = 3

        Dim mno As New DataGridViewTextBoxColumn
        Dim mpartyname As New DataGridViewTextBoxColumn
        Dim colimg As New DataGridViewImageColumn
        '--Dim coltype As New DataGridViewTextBoxColumn
        'Dim btn As New DataGridViewButtonColumn()

        Dim inImg As New DataGridViewImageCell()
        colimg.HeaderText = "Image"
        colimg.Name = "img"
        colimg.ImageLayout = DataGridViewImageCellLayout.Stretch

        'colorcode.ValueType = GetType(String)
        'colorcode.HeaderText = "Brand"

        mno.ValueType = GetType(Int32)
        mno.HeaderText = "Party Name"

        mpartyname.ValueType = GetType(String)
        mpartyname.HeaderText = "Party Name"

        
        With dv
            dv.Columns.Add(mno)
            dv.Columns.Add(mpartyname)
            dv.Columns.Add(colimg)

            'dvproof.Columns.Add(btn)
        End With
        dv.ColumnHeadersDefaultCellStyle.Font = New Font(dv.Font, FontStyle.Bold)
        dv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dv.Columns(0).Width = 70
        dv.Columns(1).Width = 200
        dv.Columns(2).Width = 70
        'dv.Columns(2).Width = 70
        'dv.Columns(3).Width = 200
        'dv.Columns(4).Width = 100
        'dv.Columns(5).Width = 70

        'Dim row As DataGridViewRow = dvproof.Rows(0)
        'row.Height = 25
        colimg.ImageLayout = DataGridViewImageCellLayout.Zoom
        'dvproof.ReadOnly = True
        dv.AllowUserToAddRows = False
        'dvproof.Rows.Add()

    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        Dim i As Integer
        For i = 0 To Me.dv.Rows.Count - 1
            Me.dv.Rows(0).Selected = True
            Me.dv.Rows(0).Dispose()
            Me.dv.Rows.RemoveAt(Me.dv.SelectedRows(0).Index)
        Next
        mskdate.Text = ""
        PictureBox2.Image = Nothing
    End Sub
    Private Sub dispform()
        'DV.AutoGenerateColumns = False

        Dim j As Integer


        ' Call dvproofhead()
        Dim darlf As New OleDb.OleDbDataAdapter
        Dim dsrlf As New DataSet



        If Val(Microsoft.VisualBasic.Format(CDate(mskdate.Text), "dd")) > 0 Then
            If msel = 3 Then
                'qryCustomers = "SELECT * FROM RRCOLOR.dbo.colormast WHERE colcode = '" & txtcolcode.Text & "'"
                qryCustomers = "SELECT * FROM RRCOLOR.dbo.purcscan WHERE date = '" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' order by date,bno"
            Else
                'qryCustomers = "SELECT * FROM RRCOLOR.dbo.colormast"
                'qryCustomers = "SELECT * FROM RRCOLOR.dbo.colormast WHERE colcode = '" & txtcolcode.Text & "'"
                qryCustomers = "SELECT * FROM RRCOLOR.dbo.purcscan WHERE date = '" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' order by date,bno"
            End If



            darlf.SelectCommand = New OleDbCommand(qryCustomers, con)

            Dim cb1 As OleDbCommandBuilder = New OleDbCommandBuilder(darlf)

            darlf.Fill(dsrlf, "RRCOLOR.dbo.purcscan")

            Dim dtdf As DataTable = dsrlf.Tables("RRCOLOR.dbo.purcscan")

            Try


                With dtdf
                    If .Rows.Count > 0 Then
                        'mskdate.Text = .Rows(k)("date")
                        For k = 0 To .Rows.Count - 1

                            j = dv.Rows.Add()
                            dv.Rows.Item(j).Cells(0).Value = Val(.Rows(k)("bno"))
                            dv.Rows.Item(j).Cells(1).Value = .Rows(k)("party") & vbNullString

                            If IsDBNull(.Rows(k)("purscan")) = False Then
                                arr = .Rows(k)("purscan")
                                ' bmpImage = DirectCast(Image.FromFile(filepath, True), Bitmap)

                                ' PictureBox1.Image = PictureBox1.Image.FromStream(New IO.MemoryStream(arr))
                                dv.Rows.Item(j).Cells(2).Value = System.Drawing.Image.FromStream(New IO.MemoryStream(arr))
                                arr = Nothing
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

        Else
            'qryCustomers = "SELECT * FROM RRCOLOR.dbo.purcscan order by date,bno"
        End If
    End Sub


    Private Sub saveattachment()


        If msel = 2 Then
            'Dim CMD2 As New SqlCommand("delete from VinHR_Img.dbo.empform where empid=" & Val(Txtempcode.Text), con)
            '                                  & Microsoft.VisualBasic.Format(CDate(mskddate.Text), "yyyy-MM-dd") & "' where docnum=" & Val(flxv.get_TextMatrix(k, 0)), con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            'Dim CMD2 As New OleDbCommand("delete from RRCOLOR.dbo.colormast where colcode='" & Val(txtcolcode.Text) & "'", con)
            Dim CMD2 As New OleDbCommand("delete from RRCOLOR.dbo.purcscan where date='" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'", con)

            'Dim CMD2 As New OleDbCommand("delete from RRCOLOR.dbo.colormast ", con)
            CMD2.ExecuteNonQuery()
            CMD2.Dispose()
        End If




        sqlqry = "select * from RRCOLOR.dbo.purcscan"
        'ElseIf msel = 2 Then
        'Call delrecord2("select * from VinHR_Img.dbo.empphoto where empid=" & Val(Txtempcode.Text))
        'sqlqry = "select * from VinHR_Img.dbo.empphoto where empid=" & Val(Txtempcode.Text)
        'sqlqry = "select * from VinHR_Img.dbo.empphoto"

        'End If

        Dim dafmg As New OleDbDataAdapter
        Dim dsfmg As New DataSet
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        dafmg.SelectCommand = New OleDbCommand(sqlqry, con)
        Dim cb2 As OleDbCommandBuilder = New OleDbCommandBuilder(dafmg)
        dafmg.Fill(dsfmg, "RRCOLOR.dbo.purcscan")
        Dim dtmg As DataTable = dsfmg.Tables("RRCOLOR.dbo.purcscan")
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
                    newRow("date") = Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd")
                    newRow("bno") = Val(dv.Rows.Item(j).Cells(0).Value)
                    newRow("party") = dv.Rows.Item(j).Cells(1).Value & vbNullString
                    
                    Dim obj As New Object
                    obj = dv.Rows.Item(j).Cells(2).Value

                    If obj Is Nothing Then
                        'If dv.Rows.Item(j).Cells(4).Value = Nothing Then
                        'If rv.Cells(i).Value Is Nothing OrElse rw.Cells(i).Value = DBNull.Value OrElse String.IsNullOrWhitespace(rw.Cells(i).Value.ToString()) Then
                        newRow("purscan") = Nothing
                        'newRow("colimage") = imageToByteArray(dv.Rows.Item(j).Cells(4).Value)
                    Else
                        newRow("purscan") = imageToByteArray(dv.Rows.Item(j).Cells(2).Value)
                        'newRow("colimage") = Nothing
                    End If

                    dtmg.Rows.Add(newRow)
                End If
            Next
            '    End With
            dafmg.Update(dsfmg, "RRCOLOR.dbo.purcscan")
            MsgBox("Attachement successfully saved.", MsgBoxStyle.Information)
            cb2.Dispose()
            dtmg.Dispose()
            dafmg.Dispose()
            dsfmg.Dispose()
        Catch ex As OleDbException
            MsgBox(mcode)
            MsgBox(ex.ToString)
            mcode = ""
        End Try



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
            If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                inimg2.Value = Image.FromFile(fd.FileName)
            End If
            filepath = fd.FileName
            If filepath <> "OpenFileDialog1" Then
                ' proofimg.ImageLayout = DataGridViewImageCellLayout.Stretch
                bmpImage = DirectCast(Image.FromFile(filepath, True), Bitmap)
                dv.Rows(e.RowIndex).Cells(2).Value = bmpImage
            End If

            ' DirectCast(dvproof.Columns(1), DataGridViewImageColumn).ImageLayout = DataGridViewImageCellLayout.Zoom
            'dvproof.Rows(0).Cells.Add(inimg)
            'filepath = fd.FileName
        End If
    End Sub

    Private Sub dv_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dv.CellMouseDown
        Select Case e.Button

            Case Windows.Forms.MouseButtons.Left

                'Record_Click(sender, New System.EventArgs())

            Case Windows.Forms.MouseButtons.Middle

                'Stop_Click(sender, New System.EventArgs())

            Case Windows.Forms.MouseButtons.Right
                If e.ColumnIndex = 2 Then
                    'If dvproof.CurrentRow.Cells(1).Value = Nullable Then
                    ' Try
                    ' Dim ms As MemoryStream = New MemoryStream(CType(dvproof.Rows(e.RowIndex).Cells(1).Value, Byte()))

                    'System.Drawing.Image.FromStream(New IO.MemoryStream(arr))
                    PictureBox2.Image = dv.Rows(e.RowIndex).Cells(2).Value
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
        cmdclear.PerformClick()


    End Sub

    Private Sub cmdedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdedit.Click
        msel = 2
        mskdate.Focus()


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

    Private Sub cmddwnload_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddwnload.Click
        sd.Title = "Download file"
        sd.InitialDirectory = "C:\"
        sd.FileName = "*.jpg"
        sd.Filter = "Image Files|*.gif;*.jpg;*.png;*.bmp;"
        'fd.RestoreDirectory = False

        If sd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PictureBox2.Image.Save(sd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg)
        End If
        'filepath = fd.FileName
    End Sub

    Private Sub mskdate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mskdate.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If Val(Microsoft.VisualBasic.Format(CDate(mskdate.Text), "dd")) > 0 Then
                Call dispform()
            End If
        End If
    End Sub

    Private Sub mskdate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskdate.MaskInputRejected

    End Sub

    Private Sub dv_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles dv.RowPrePaint
        If e.RowIndex >= 0 Then
            Me.dv.Rows(e.RowIndex).Cells(0).Value = e.RowIndex + 1
        End If
    End Sub

    Private Sub cmdexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexcel.Click
        Cursor = Cursors.WaitCursor

        'msql = "insert into purcscan select b.* from  mpls.rrcolor.dbo.purcscan b where not exists(select * from purcscan a where a.Bno=b.Bno and a.Party=b.Party)"
        msql = "exec rrcolor.dbo.dwnloadscan"
        'msql4 = "Exec insertrbartemp " & Val(txtbno.Text) & ",'" & Trim(cmbprnon.Text) & "','" & Trim(linkserver) & "','" & Trim(dbnam) & "'"
        Dim rCMD As New OleDb.OleDbCommand(msql, con)

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        'dCMD.CommandText = "Exec inserbartemp 'Test', 'Test', 'Test'"
        rCMD.CommandText = msql
        rCMD.Connection = con 'Active Connection 
        rCMD.CommandTimeout = 300
        Try
            rCMD.ExecuteNonQuery()

            rCMD.Dispose()
            'MsgBox("completed")
        Catch ex As Exception
            rCMD.Dispose()
            MsgBox(ex.Message)

        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub cmdprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdprint.Click
        PrintDialog1 = New PrintDialog

        PrintDialog1.Document = PrintDocument1 'pbxLogo.Image

        Dim r As DialogResult = PrintDialog1.ShowDialog

        If r = Windows.Forms.DialogResult.OK Then

            PrintDocument1.Print()

        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ''PrintDocument1.DefaultPageSettings.PaperSize
        ''the above property exposes the current height and width of a 
        ''page in pixels
        'With PrintDocument1.DefaultPageSettings.PaperSize
        '    Dim ImageSize As Size = New Size(.Width, .Height)
        '    e.Graphics.DrawImage(PictureBox2.Image, 0, 0)
        '    'You now have you stretch your image to this size
        '    'And in the Print Event Of PrintDocument you can draw this image 
        'End With

        ''e.Graphics.DrawImage(PictureBox2.Image, 0, 0)

        Dim i As Image = PictureBox2.Image
        Dim newWidth As Single = (i.Width * (100 / i.HorizontalResolution))
        Dim newHeight As Single = (i.Height * (100 / i.VerticalResolution))
        Dim widthFactor As Single = (newWidth / e.MarginBounds.Width)
        Dim heightFactor As Single = (newHeight / e.MarginBounds.Height)
        If ((widthFactor > 1) _
                    Or (heightFactor > 1)) Then
            If (widthFactor > heightFactor) Then
                newWidth = (newWidth / widthFactor)
                newHeight = (newHeight / widthFactor)
            Else
                newWidth = (newWidth / heightFactor)
                newHeight = (newHeight / heightFactor)
            End If

        End If

        e.Graphics.DrawImage(i, 0, 0, CType(newWidth, Integer), CType(newHeight, Integer))
    End Sub
End Class
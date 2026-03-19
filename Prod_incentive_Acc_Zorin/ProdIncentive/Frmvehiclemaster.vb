Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.DATA
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.IO.MemoryStream

Public Class Frmvehiclemaster
    Dim filepath, msql, msql2, merr As String
    Dim msel, n As Integer
    Dim trans2 As OleDb.OleDbTransaction
    'Dim Trans6 As SqlTransaction
    Private Sub Frmvehiclemaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        CLEAR(Me)
        cmbvehtype.Items.Add("BUS")
        cmbvehtype.Items.Add("VAN")
        cmbvehtype.Items.Add("MINI VAN")
        cmbvehtype.Items.Add("CAR")
        cmbvehtype.Items.Add("BIKE")
        cmbvehtype.Items.Add("SCOOTER")
        'cmbvehtype.Items.Add("MOPET")

        'disable(Me)
    End Sub

    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butdisp.Click
        msel = 4
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrto.TextChanged

    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click

    End Sub

    Private Sub cmbvehtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbvehtype.SelectedIndexChanged

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub cmbvehname_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbvehname.SelectedIndexChanged

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        OFDg.Title = "Select your Image."
        OFDg.InitialDirectory = "C:\"
        OFDg.Filter = "Image Files|*.gif;*.jpg;*.png;*.bmp;"
        OFDg.RestoreDirectory = False
        Dim bmpImage As Bitmap = Nothing
        'Dim inimg2 As New DataGridViewImageCell
        If OFDg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'inimg2.Value = Image.FromFile(fd.FileName)
            picfc.Image = Image.FromFile(OFDg.FileName)
        End If
        filepath = OFDg.FileName
    End Sub



    '     qry2 = "insert into empphotomaster (empid,empimage) values (@empid,@photo)"

    '  qry2 = "update empphotomaster set empimage=@photo where empid=" & Val(txtid.Text)


    'If msel = 1 Then
    '           icmd.Parameters.AddWithValue("@empid", txtid.Text)
    '       End If
    '   Dim ms As New IO.MemoryStream()
    '       emppic.Image.Save(ms, emppic.Image.RawFormat)
    '   Dim data As Byte() = ms.GetBuffer()
    '   Dim p As New SqlParameter("@photo", SqlDbType.Image)
    '       p.Value = data
    '       icmd.Parameters.Add(p)


    ' icmd.ExecuteNonQuery()

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        ofdg2.Title = "Select your Image."
        ofdg2.InitialDirectory = "C:\"
        ofdg2.Filter = "Image Files|*.gif;*.jpg;*.png;*.bmp;"
        ofdg2.RestoreDirectory = False
        Dim bmpImage As Bitmap = Nothing
        'Dim inimg2 As New DataGridViewImageCell
        If ofdg2.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'inimg2.Value = Image.FromFile(fd.FileName)
            picins.Image = Image.FromFile(ofdg2.FileName)
        End If
        filepath = ofdg2.FileName
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Ofdg3.Title = "Select your Image."
        Ofdg3.InitialDirectory = "C:\"
        Ofdg3.Filter = "Image Files|*.gif;*.jpg;*.png;*.bmp;"
        Ofdg3.RestoreDirectory = False
        Dim bmpImage As Bitmap = Nothing
        'Dim inimg2 As New DataGridViewImageCell
        If Ofdg3.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'inimg2.Value = Image.FromFile(fd.FileName)
            picpermit.Image = Image.FromFile(Ofdg3.FileName)
        End If
        filepath = Ofdg3.FileName
    End Sub

    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click
        Ofdg4.Title = "Select your Image."
        Ofdg4.InitialDirectory = "C:\"
        Ofdg4.Filter = "Image Files|*.gif;*.jpg;*.png;*.bmp;"
        Ofdg4.RestoreDirectory = False
        Dim bmpImage As Bitmap = Nothing
        'Dim inimg2 As New DataGridViewImageCell
        If Ofdg4.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'inimg2.Value = Image.FromFile(fd.FileName)
            pictax.Image = Image.FromFile(Ofdg4.FileName)
        End If
        filepath = Ofdg4.FileName
    End Sub

    Private Sub butexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butexit.Click
        Me.Close()
    End Sub

    Private Sub butnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butnew.Click
        msel = 1
    End Sub

    Private Sub butedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butedit.Click
        msel = 2
    End Sub

    Private Sub butdel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butdel.Click
        msel = 3
    End Sub

    Private Sub saverec()
        'Dim strsql As String = "select * from " & Trim(mcostdbnam) & ".dbo.processjobmaster where opername='" & Trim(txtopername.Text) & "' and process='" & Trim(cmbdepartment.Text) & "'"

        'If dataexists(strsql) = False Then

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        If msel = 1 Then
            'msql = "DECLARE @vehname nvarchar(50),@vehno nvarchar(10),@fcdate datetime,@rto nvarchar(50),@place nvarchar(50),@vehtype nvarchar(20),@insdate datetime,@insname nvarchar(50),@permitdate datetime,@taxdate datetime,@fcpic image,@inspic image,@permitpic image,@taxpic image" & vbCrLf

            'msql = " insert into transport.dbo.vehiclemaster(vehiclename,vehno,fcetdate,rto,place,vehtype,insrenewdate,insurname,permitrenewdate,taxrenewdate,fcpic,inspic,permitpic,taxpic) " & vbCrLf _
            '               & " Values (?,?,?,?,?,?,?,?,?,?,?,?,?,?) " & vbCrLf

            'msql = " insert into transport.dbo.vehiclemaster(vehiclename,vehno,fcetdate,rto,place,vehtype,insrenewdate,insurname,permitrenewdate,taxrenewdate,fcpic,inspic,permitpic,taxpic) " & vbCrLf _
            '               & " Values (@vehname,@vehno,@fcdate,@rto,@place,@vehtype,@insdate,@insname,@permitdate,@taxdate,@fcpic,@inspic,@permitpic,@taxpic) " & vbCrLf
            'Dim imgData As Byte()         ' storage for the img bytes
            'imgData = ImgToByteArray(PictureBox1.Image, ImageFormat.Jpeg)

            Dim ms As New IO.MemoryStream()
            'Dim ms As MemoryStream = New MemoryStream
            Dim data As Byte()
            If Not picfc.Image Is Nothing Then
                'picfc.Image.Save(ms, picfc.Image.RawFormat)
                'data = ms.GetBuffer()
                data = imageToByteArray(picfc.Image)
                'Dim data As Byte() = ms.GetBuffer()
                ''Dim p As New OleDbParameter("@fcpic", OleDbType.Binary)
                'Dim p As New SqlParameter("@fcpic", SqlDbType.Image)
                'p.Value = data
                'cmd.Parameters.Add(p)
            Else
                data = Nothing
            End If


            'Dim ms1 As New IO.MemoryStream()
            Dim ms1 As MemoryStream = New MemoryStream
            Dim data1 As Byte()
            If Not picins.Image Is Nothing Then
                'picins.Image.Save(ms1, picins.Image.RawFormat)
                'data1 = ms1.GetBuffer
                data1 = imageToByteArray(picins.Image)
                'Dim data1 As Byte() = ms1.GetBuffer()
                ''Dim p1 As New OleDbParameter("@inspic", OleDbType.Binary)
                'Dim p1 As New SqlParameter("@inspic", SqlDbType.Image)
                'p1.Value = data1
                'cmd.Parameters.Add(p1)
            Else
                data1 = Nothing
            End If

            Dim ms2 As New IO.MemoryStream()
            'Dim ms2 As MemoryStream = New MemoryStream
            Dim data2 As Byte()
            If Not picpermit.Image Is Nothing Then
                'picpermit.Image.Save(ms2, picpermit.Image.RawFormat)
                'data2 = ms2.GetBuffer
                data2 = imageToByteArray(picpermit.Image)
                'ms.ToArray()
                'Dim data2 As Byte() = ms.GetBuffer()
                ''Dim p2 As New OleDbParameter("@permitpic", OleDbType.Binary)
                'Dim p2 As New SqlParameter("@permitpic", SqlDbType.Image)
                'p2.Value = data2
                'cmd.Parameters.Add(p2)

            Else
                data2 = Nothing
            End If


            'Dim ms3 As New IO.MemoryStream()
            Dim ms3 As MemoryStream = New MemoryStream
            Dim data3 As Byte()
            If Not pictax.Image Is Nothing Then
                'pictax.Image.Save(ms3, pictax.Image.RawFormat)
                'data3 = ms3.GetBuffer()
                data3 = imageToByteArray(pictax.Image)
                'Dim data3 As Byte() = ms3.GetBuffer()
                ''Dim p3 As New OleDbParameter("@taxpic", OleDbType.Binary)
                'Dim p3 As New SqlParameter("@taxpic", SqlDbType.Image)
                'p3.Value = data3
                'cmd.Parameters.Add(p3)
            Else
                data3 = Nothing
            End If






            'msql = " insert into transport.dbo.vehiclemaster(vehiclename,vehno,fcetdate,rto,place,vehtype,insrenewdate,insurname,permitrenewdate,taxrenewdate,fcpic,inspic,permitpic,taxpic) " & vbCrLf _
            '    & " Values ('" & cmbvehname.Text & "','" & cmbvehno.Text & "','" & Format(CDate(mskfcrenewdate.Text), "yyyy-MM-dd") & "','" & txtrto.Text & "','" & txtplace.Text & "','" & cmbvehtype.Text & "'" & vbCrLf _
            '    & ",'" & Format(CDate(mskinsdate.Text), "yyyy-MM-dd") & "','" & txtinsname.Text & "','" & Format(CDate(mskpermitdate.Text), "yyyy-MM-dd") & "','" & Format(CDate(msktaxdate.Text), "yyyy-MM-dd") & "'" & vbCrLf _
            '    & "," & imageToByteArray(picfc.Image) & "," & imageToByteArray(picins.Image) & "," & imageToByteArray(picpermit.Image)data2 & "," & imageToByteArray(pictax.Image ) & ")"


            'If con Is Nothing OrElse con.State = ConnectionState.Closed Then
            '    con.Open()
            'End If

            trans2 = con.BeginTransaction



            Dim cmd As New OleDb.OleDbCommand(msql, con)

            'Dim cmd As New SqlCommand(msql, con)

            'cmd.Parameters.Add(New OleDbParameter("@vehname", OleDbType.VarChar)).Value = cmbvehname.Text
            'cmd.Parameters.Add(New OleDbParameter("@vehno", OleDbType.VarChar)).Value = cmbvehno.Text

            cmd.Parameters.AddWithValue("@vehname", cmbvehname.Text)
            cmd.Parameters.AddWithValue("@vehno", cmbvehno.Text)
            If mskfcrenewdate.Text <> Nothing Then
                cmd.Parameters.AddWithValue("@fcdate", Format(CDate(mskfcrenewdate.Text), "yyyy-MM-dd"))
                'cmd.Parameters.Add(New OleDbParameter("@fcdate", OleDbType.Date)).Value = Format(CDate(mskfcrenewdate.Text), "yyyy-MM-dd")
            End If
            cmd.Parameters.AddWithValue("@rto", txtrto.Text)
            cmd.Parameters.AddWithValue("@place", txtplace.Text)
            cmd.Parameters.AddWithValue("@vehtype", cmbvehtype.Text)
            'cmd.Parameters.Add(New OleDbParameter("@rto", OleDbType.VarChar)).Value = txtrto.Text
            'cmd.Parameters.Add(New OleDbParameter("@place", OleDbType.VarChar)).Value = txtplace.Text
            'cmd.Parameters.Add(New OleDbParameter("@vehtype", OleDbType.VarChar)).Value = cmbvehtype.Text
            If mskinsdate.Text <> Nothing Then
                cmd.Parameters.AddWithValue("@insdate", Format(CDate(mskinsdate.Text), "yyyy-MM-dd"))
                'cmd.Parameters.Add(New OleDbParameter("@insdate", OleDbType.Date)).Value = Format(CDate(mskinsdate.Text), "yyyy-MM-dd")
            End If
            cmd.Parameters.AddWithValue("@insname", txtinsname.Text)
            'cmd.Parameters.Add(New OleDbParameter("@insname", OleDbType.VarChar)).Value = txtinsname.Text

            If mskpermitdate.Text IsNot Nothing AndAlso Not mskpermitdate.MaskCompleted Then
                cmd.Parameters.AddWithValue("@permitdate", Format(CDate(mskpermitdate.Text), "yyyy-MM-dd"))
                'cmd.Parameters.Add(New OleDbParameter("@permitdate", OleDbType.Date)).Value = Format(CDate(mskpermitdate.Text), "yyyy-MM-dd")
            End If
            If msktaxdate.Text IsNot Nothing AndAlso Not msktaxdate.MaskCompleted Then
                cmd.Parameters.AddWithValue("@taxdate", Format(CDate(msktaxdate.Text), "yyyy-MM-dd"))
                'cmd.Parameters.Add(New OleDbParameter("@taxdate", OleDbType.Date)).Value = Format(CDate(msktaxdate.Text), "yyyy-MM-dd")
            End If


            'Dim ms As New IO.MemoryStream()
            'If Not picfc.Image Is Nothing Then
            '    picfc.Image.Save(ms, picfc.Image.RawFormat)
            '    Dim data As Byte() = ms.GetBuffer()
            '    'Dim p As New OleDbParameter("@fcpic", OleDbType.Binary)
            '    Dim p As New SqlParameter("@fcpic", SqlDbType.Image)
            '    p.Value = data
            '    cmd.Parameters.Add(p)
            'End If


            'Dim ms1 As New IO.MemoryStream()
            'If Not picins.Image Is Nothing Then
            '    picins.Image.Save(ms, picins.Image.RawFormat)
            '    Dim data1 As Byte() = ms.GetBuffer()
            '    'Dim p1 As New OleDbParameter("@inspic", OleDbType.Binary)
            '    Dim p1 As New SqlParameter("@inspic", SqlDbType.Image)
            '    p1.Value = data1
            '    cmd.Parameters.Add(p1)
            'End If

            'Dim ms2 As New IO.MemoryStream()
            'If Not picpermit.Image Is Nothing Then
            '    picpermit.Image.Save(ms, picpermit.Image.RawFormat)
            '    'ms.ToArray()
            '    Dim data2 As Byte() = ms.GetBuffer()
            '    'Dim p2 As New OleDbParameter("@permitpic", OleDbType.Binary)
            '    Dim p2 As New SqlParameter("@permitpic", SqlDbType.Image)
            '    p2.Value = data2
            '    cmd.Parameters.Add(p2)
            'End If


            'Dim ms3 As New IO.MemoryStream()
            'If Not pictax.Image Is Nothing Then
            '    pictax.Image.Save(ms, pictax.Image.RawFormat)
            '    Dim data3 As Byte() = ms.GetBuffer()
            '    'Dim p3 As New OleDbParameter("@taxpic", OleDbType.Binary)
            '    Dim p3 As New SqlParameter("@taxpic", SqlDbType.Image)
            '    p3.Value = data3
            '    cmd.Parameters.Add(p3)
            'End If


            'Try

            cmd.ExecuteNonQuery()

            'trans2.Commit()
            MsgBox("Saved!")
            'trans2.Commit()

            'Call cancel()
            'Catch ex As Exception
            '    trans.Rollback()
            '    MsgBox(ex.Message)

            '    merr = Trim(ex.Message)

            'End Try
        End If



    End Sub

    Private Sub updaterec()
        'Dim strsql As String = "select * from " & Trim(mcostdbnam) & ".dbo.processjobmaster where opername='" & Trim(txtopername.Text) & "' and process='" & Trim(cmbdepartment.Text) & "'"
        'If dataexists(strsql) = False Then

        If msel = 1 Then

            'msql = "insert into transport.dbo.vehiclemaster(vehiclename,vehno,fcetdate,rto,place,vehtype,insrenewdate,insurname,permitrenewdate,taxrenewdate,fcpic,inspic,permitpic,taxpic)" & vbCrLf _
            '               & " Values (@vehname,@vehno,@fcdate,@rto,@place,@vehtype,@insdate,@insname,@permitdate,@taxdate,@fcpic,@inspic,@permitpic,@taxpic) " & vbCrLf

            msql = "update transport.dbo.vehiclemaster set vehiclename=@vehname,vehno=@vehno,fcetdate=@fcdate,rto=@rto,place=@place,vehtype=@vehtype,insrenewdate=@insdate,insurname=@insname," & vbCrLf _
            & "permitrenewdate=@permitdate,taxrenewdate=@taxdate,fcpic=@fcpic,inspic=@inspic,permitpic=@permitpic,taxpic=@taxpic where vheno='" & cmbvehno.Text & "'"

            trans = con.BeginTransaction

            Dim cmd As New OleDbCommand(msql, con, trans)
            cmd.Parameters.AddWithValue("@vehname", cmbvehname.Text)
            cmd.Parameters.AddWithValue("@vehno", cmbvehno.Text)
            cmd.Parameters.AddWithValue("@fcdate", Format(CDate(mskfcrenewdate.Text), "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@rto", txtrto.Text)
            cmd.Parameters.AddWithValue("@place", txtplace.Text)
            cmd.Parameters.AddWithValue("@vehtype", cmbvehtype.Text)
            cmd.Parameters.AddWithValue("@insdate", Format(CDate(mskinsdate.Text), "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@insname", txtinsname.Text)
            cmd.Parameters.AddWithValue("@permitdate", Format(CDate(mskpermitdate.Text), "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@taxdate", Format(CDate(msktaxdate.Text), "yyyy-MM-dd"))

            Dim ms As New IO.MemoryStream()
            picfc.Image.Save(ms, picfc.Image.RawFormat)
            Dim data As Byte() = ms.GetBuffer()
            Dim p As New OleDbParameter("@fcpic", OleDbType.Binary)
            p.Value = data
            cmd.Parameters.Add(p)


            Dim ms1 As New IO.MemoryStream()
            picins.Image.Save(ms, picins.Image.RawFormat)
            Dim data1 As Byte() = ms.GetBuffer()
            Dim p1 As New OleDbParameter("@inspic", OleDbType.Binary)
            p1.Value = data1
            cmd.Parameters.Add(p1)

            Dim ms2 As New IO.MemoryStream()
            picpermit.Image.Save(ms, picpermit.Image.RawFormat)
            Dim data2 As Byte() = ms.GetBuffer()
            Dim p2 As New OleDbParameter("@permitpic", OleDbType.Binary)
            p2.Value = data2
            cmd.Parameters.Add(p2)

            Dim ms3 As New IO.MemoryStream()
            pictax.Image.Save(ms, pictax.Image.RawFormat)
            Dim data3 As Byte() = ms.GetBuffer()
            Dim p3 As New OleDbParameter("@taxpic", OleDbType.Binary)
            p3.Value = data3
            cmd.Parameters.Add(p3)


            Try

                cmd.ExecuteNonQuery()


                MsgBox("Updated!")
                trans.Commit()
                'Call cancel()
            Catch ex As Exception
                trans.Rollback()
                MsgBox(ex.Message)

                merr = Trim(ex.Message)

            End Try
        End If



    End Sub

    Private Sub butsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butsave.Click
        Call saverec()
        Call loadhead()
    End Sub

    Private Sub loadhead()


        msql2 = " select vehno,vehiclename from transport.dbo.vehiclemaster with (nolock) group by vehno,vehiclename  order by vehno"    

        dg.Rows.Clear()

        Dim dt1 As DataTable = getDataTable(msql2)
        If dt1.Rows.Count > 0 Then
            For Each row As DataRow In dt1.Rows
                n = dg.Rows.Add
                dg.Rows(n).Cells(0).Value = row("vehno")
                dg.Rows(n).Cells(1).Value = row("vehiclename")

            Next
        End If
    End Sub

    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick

    End Sub
End Class
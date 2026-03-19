Imports System
Imports System.IO
Imports System.Net.Mail
Imports Microsoft.VisualBasic
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
'--Imports System.Net.Mail.SmtpClient
'Imports System.Net.Mail.MailMessage
'Imports System.Net.Mail.Attachment
Imports System.Net


Public Class Frmmail
    Dim mail As New MailMessage()
    Dim attachToMsg As Attachment
    Private Shared Function customCertValidation(ByVal sender As Object, _
                                                 ByVal cert As X509Certificate, _
                                                 ByVal chain As X509Chain, _
                                                 ByVal errors As SslPolicyErrors) As Boolean

        Return True

    End Function
    Private Sub btnAttachment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachment.Click
        Dim ofd As New OpenFileDialog
        ofd.ShowDialog()
        If ofd.FileName <> "" Then
            txtAttachements.Text = txtAttachements.Text & ofd.FileName & ","
        End If

        'Dim attachToMsg As Attachment

        '' Dim ofd As New OpenFileDialog

        ''openDLG.AddExtension = True
        'ofd.ReadOnlyChecked = True
        'ofd.Multiselect = True
        'ofd.Title = "Select the file(s) you want added to the message..."
        'ofd.Filter = "All Files (*.*)|*.*"

        'If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then

        '    For Each item As String In ofd.FileNames

        '        'Create a new System.NET.Mail.Attachment class instance for each file.
        '        'attachToMsg = New System.Net.Mail.Attachment(item)
        '        attachToMsg = New Attachment(item)
        '        'Then add the attachment to your message. You have to do this everytime you run the code
        '        'above.
        '        mail.Attachments.Add(attachToMsg)

        '    Next

        '    MsgBox("I have finished adding all of the selected files! You can do more if you want!")

        'End If







    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Try
            Dim SmtpServer2 As New SmtpClient()
            ''Dim mail As New MailMessage()

            ' 'SmtpServer.Credentials = New Net.NetworkCredential("username@gmail.com", "password")
            ' 'SmtpServer.Credentials = New Net.NetworkCredential("selvakumar", txtPwd.Text)
            'SmtpServer2.UseDefaultCredentials = True
            SmtpServer2.Credentials = New Net.NetworkCredential(txtuser.Text, txtPwd.Text)
            'SmtpServer2.EnableSsl = True
            'System.Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls

            ''587-out,25 local
            'SmtpServer2.Port =  TLS/STARTTLS:587


            SmtpServer2.Host = txtsmtphost.Text
            ''Dim oServer As New SmtpServer("smtp.ramrajcotton.net")

            If Trim(cmbencryptcon.Text) = "TLS" Then
                SmtpServer2.Port = 587
                SmtpServer2.EnableSsl = True
                System.Net.ServicePointManager.ServerCertificateValidationCallback = _
                New RemoteCertificateValidationCallback(AddressOf AcceptAllCertifications)

            ElseIf Trim(cmbencryptcon.Text) = "SSL" Then
                SmtpServer2.Port = 587
                SmtpServer2.EnableSsl = True
                System.Net.ServicePointManager.ServerCertificateValidationCallback = _
                 New RemoteCertificateValidationCallback(AddressOf AcceptAllCertifications)
            Else
                SmtpServer2.Port = 25
            End If
            '      SmtpServer2.EnableSsl = True

            '      System.Net.ServicePointManager.ServerCertificateValidationCallback = _
            'New RemoteCertificateValidationCallback(AddressOf AcceptAllCertifications)

            'System.Net.ServicePointManager.ServerCertificateValidationCallback = Delegate(ByVal object  s, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal sslPolicyErrors As SslPolicyErrors){Return true)


            mail = New MailMessage()
            mail.From = New MailAddress(txtFrom.Text)



            If txtTo.Text.Contains(";") Then
                Dim emailList As String()
                emailList = txtTo.Text.Split(";")
                'emailList = Split(";", txtTo.Text) '  txtTo.Split(";")
                For Each email As String In emailList
                    mail.To.Add(email)
                Next
            Else
                mail.To.Add(txtTo.Text)
            End If
            If txtcc.Text <> "" Then
                If txtcc.Text.Contains(";") Then
                    Dim ccList As String()
                    ccList = txtcc.Text.Split(";")
                    For Each ccTo As String In ccList
                        mail.CC.Add(ccTo)
                    Next
                Else
                    mail.CC.Add(txtcc.Text)
                End If
            End If

            If txtAttachements.Text <> "" Then
                If txtAttachements.Text.Contains(",") Then
                    Dim attachList As String()
                    attachList = txtAttachements.Text.Split(",")
                    For Each attccTo As String In attachList
                        If Len(Trim(attccTo)) > 0 Then
                            attachToMsg = New Attachment(attccTo)
                            mail.Attachments.Add(attachToMsg)
                        End If
                        'mail.CC.Add(attccTo)
                    Next
                Else
                    'mail.CC.Add(txtcc.Text)
                End If
            End If


            'mail.To.Add(txtTo.Text)

            'For Each strfile As String In txtAttachements
            '    If Not strfile = "" Then
            '        Dim MsgAttach As New Attachment(strfile)
            '        mail.Attachments.Add(MsgAttach)
            '    End If
            'Next

            'For Each strfile As String In fileList
            ' If Not strfile = "" Then
            'Dim MsgAttach As New Attachment(txtAttachements.Text)
            'mail.Attachments.Add(MsgAttach)
            'mail.Attachments.Add(txtAttachements)

            'End If
            'Next
            mail.Subject = txtSubject.Text
            mail.Body = txtBody.Text
            SmtpServer2.Send(mail)
            MsgBox("mail send")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Frmmail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        txtuser.Text = "selvakumar@atithya.net"
        txtPwd.Text = "Selvauma@536"
        txtsmtphost.Text = "mail.atithya.net"
        '25-local,587-outside
        txtport.Text = 587
        txtFrom.Text = "selvakumar@atithya.net"

        cmbencryptcon.Items.Add(" ")
        cmbencryptcon.Items.Add("SSL")
        cmbencryptcon.Items.Add("TLS")

        'Incoming mail Pop3=110 ssl/tls=995
        'Incoming Mail IMAP=143 ssl/tls=993
        'Outgoing Mail SMTP=25,2525,465,587


    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        sendmail(txtuser.Text, txtPwd.Text, txtsmtphost.Text, txtport.Text, txtFrom.Text, txtTo.Text, txtSubject.Text, txtBody.Text, txtcc.Text, txtAttachements.Text)

    End Sub
    Public Function AcceptAllCertifications(ByVal sender As Object, ByVal certification As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim Mail As New MailMessage

        If txtAttachements.Text <> "" Then
            If txtAttachements.Text.Contains(",") Then
                Dim attachList As String()
                attachList = txtAttachements.Text.Split(",")
                For Each attccTo As String In attachList
                    If Len(Trim(attccTo)) > 0 Then
                        attachToMsg = New Attachment(attccTo)
                        Mail.Attachments.Add(attachToMsg)
                    End If
                    'mail.CC.Add(attccTo)
                Next
            Else
                'mail.CC.Add(txtcc.Text)
            End If
        End If

        'Attachment = New System.Net.Mail.Attachment(pdfFile)
        'Mail.Attachments.Add(Attachment)
        Mail.Subject = txtSubject.Text
        Mail.To.Add(txtTo.Text)
        Mail.CC.Add(txtcc.Text)
        Mail.From = New MailAddress(txtFrom.Text, txtuser.Text)
        Mail.Body = txtBody.Text

        ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf customCertValidation)

        Dim SMTP As New SmtpClient(txtsmtphost.Text) '--smtp
        SMTP.EnableSsl = True
        SMTP.Credentials = New System.Net.NetworkCredential(txtuser.Text, txtPwd.Text)
        SMTP.Port = txtport.Text
        SMTP.Send(Mail)
        SMTP.Timeout = 21000
        MsgBox("Mail Send")
        'on.Close()
        'Me.Close()
    End Sub
End Class
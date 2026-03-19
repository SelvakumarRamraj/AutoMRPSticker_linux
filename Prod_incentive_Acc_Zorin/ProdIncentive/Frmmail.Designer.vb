<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmmail
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cmbencryptcon = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtport = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtsmtphost = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtuser = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtcc = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnSend = New System.Windows.Forms.Button
        Me.btnAttachment = New System.Windows.Forms.Button
        Me.txtAttachements = New System.Windows.Forms.TextBox
        Me.lblAttachment = New System.Windows.Forms.Label
        Me.txtBody = New System.Windows.Forms.TextBox
        Me.lblbody = New System.Windows.Forms.Label
        Me.txtSubject = New System.Windows.Forms.TextBox
        Me.lblSub = New System.Windows.Forms.Label
        Me.txtTo = New System.Windows.Forms.TextBox
        Me.lblto = New System.Windows.Forms.Label
        Me.txtPwd = New System.Windows.Forms.TextBox
        Me.lblpwd = New System.Windows.Forms.Label
        Me.txtFrom = New System.Windows.Forms.TextBox
        Me.lblFrom = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.cmbencryptcon)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtport)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtsmtphost)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtuser)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtcc)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Controls.Add(Me.btnAttachment)
        Me.Panel1.Controls.Add(Me.txtAttachements)
        Me.Panel1.Controls.Add(Me.lblAttachment)
        Me.Panel1.Controls.Add(Me.txtBody)
        Me.Panel1.Controls.Add(Me.lblbody)
        Me.Panel1.Controls.Add(Me.txtSubject)
        Me.Panel1.Controls.Add(Me.lblSub)
        Me.Panel1.Controls.Add(Me.txtTo)
        Me.Panel1.Controls.Add(Me.lblto)
        Me.Panel1.Controls.Add(Me.txtPwd)
        Me.Panel1.Controls.Add(Me.lblpwd)
        Me.Panel1.Controls.Add(Me.txtFrom)
        Me.Panel1.Controls.Add(Me.lblFrom)
        Me.Panel1.Location = New System.Drawing.Point(238, 57)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(631, 550)
        Me.Panel1.TabIndex = 1
        '
        'cmbencryptcon
        '
        Me.cmbencryptcon.FormattingEnabled = True
        Me.cmbencryptcon.Location = New System.Drawing.Point(165, 113)
        Me.cmbencryptcon.Name = "cmbencryptcon"
        Me.cmbencryptcon.Size = New System.Drawing.Size(158, 21)
        Me.cmbencryptcon.TabIndex = 24
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(10, 113)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(148, 15)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Encrypted connection "
        '
        'txtport
        '
        Me.txtport.Location = New System.Drawing.Point(92, 83)
        Me.txtport.Name = "txtport"
        Me.txtport.Size = New System.Drawing.Size(511, 20)
        Me.txtport.TabIndex = 21
        Me.txtport.Text = "587"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(48, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 15)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Port"
        '
        'txtsmtphost
        '
        Me.txtsmtphost.Location = New System.Drawing.Point(92, 57)
        Me.txtsmtphost.Name = "txtsmtphost"
        Me.txtsmtphost.Size = New System.Drawing.Size(511, 20)
        Me.txtsmtphost.TabIndex = 19
        Me.txtsmtphost.Text = "mail.atithya.net"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 15)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "SMTP Host"
        '
        'txtuser
        '
        Me.txtuser.Location = New System.Drawing.Point(92, 5)
        Me.txtuser.Name = "txtuser"
        Me.txtuser.Size = New System.Drawing.Size(511, 20)
        Me.txtuser.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 15)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "User Name"
        '
        'txtcc
        '
        Me.txtcc.Location = New System.Drawing.Point(92, 195)
        Me.txtcc.Name = "txtcc"
        Me.txtcc.Size = New System.Drawing.Size(511, 20)
        Me.txtcc.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(43, 198)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 15)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "CC"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(158, 510)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(62, 28)
        Me.btnClear.TabIndex = 10
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(431, 643)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(62, 28)
        Me.btnSend.TabIndex = 9
        Me.btnSend.Text = "Sendold"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'btnAttachment
        '
        Me.btnAttachment.Location = New System.Drawing.Point(92, 222)
        Me.btnAttachment.Name = "btnAttachment"
        Me.btnAttachment.Size = New System.Drawing.Size(39, 23)
        Me.btnAttachment.TabIndex = 5
        Me.btnAttachment.Text = "..."
        Me.btnAttachment.UseVisualStyleBackColor = True
        '
        'txtAttachements
        '
        Me.txtAttachements.Location = New System.Drawing.Point(137, 222)
        Me.txtAttachements.Multiline = True
        Me.txtAttachements.Name = "txtAttachements"
        Me.txtAttachements.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAttachements.Size = New System.Drawing.Size(466, 69)
        Me.txtAttachements.TabIndex = 6
        '
        'lblAttachment
        '
        Me.lblAttachment.AutoSize = True
        Me.lblAttachment.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttachment.Location = New System.Drawing.Point(2, 225)
        Me.lblAttachment.Name = "lblAttachment"
        Me.lblAttachment.Size = New System.Drawing.Size(86, 15)
        Me.lblAttachment.TabIndex = 11
        Me.lblAttachment.Text = "Attachment :"
        '
        'txtBody
        '
        Me.txtBody.Location = New System.Drawing.Point(92, 323)
        Me.txtBody.Multiline = True
        Me.txtBody.Name = "txtBody"
        Me.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBody.Size = New System.Drawing.Size(511, 181)
        Me.txtBody.TabIndex = 8
        '
        'lblbody
        '
        Me.lblbody.AutoSize = True
        Me.lblbody.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbody.Location = New System.Drawing.Point(32, 326)
        Me.lblbody.Name = "lblbody"
        Me.lblbody.Size = New System.Drawing.Size(46, 15)
        Me.lblbody.TabIndex = 9
        Me.lblbody.Text = "Body :"
        '
        'txtSubject
        '
        Me.txtSubject.Location = New System.Drawing.Point(92, 297)
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(511, 20)
        Me.txtSubject.TabIndex = 7
        '
        'lblSub
        '
        Me.lblSub.AutoSize = True
        Me.lblSub.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSub.Location = New System.Drawing.Point(20, 300)
        Me.lblSub.Name = "lblSub"
        Me.lblSub.Size = New System.Drawing.Size(63, 15)
        Me.lblSub.TabIndex = 7
        Me.lblSub.Text = "Subject :"
        '
        'txtTo
        '
        Me.txtTo.Location = New System.Drawing.Point(92, 169)
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(511, 20)
        Me.txtTo.TabIndex = 3
        '
        'lblto
        '
        Me.lblto.AutoSize = True
        Me.lblto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblto.Location = New System.Drawing.Point(43, 172)
        Me.lblto.Name = "lblto"
        Me.lblto.Size = New System.Drawing.Size(31, 15)
        Me.lblto.TabIndex = 5
        Me.lblto.Text = "To :"
        '
        'txtPwd
        '
        Me.txtPwd.Location = New System.Drawing.Point(92, 31)
        Me.txtPwd.Name = "txtPwd"
        Me.txtPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPwd.Size = New System.Drawing.Size(511, 20)
        Me.txtPwd.TabIndex = 1
        Me.txtPwd.UseSystemPasswordChar = True
        '
        'lblpwd
        '
        Me.lblpwd.AutoSize = True
        Me.lblpwd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpwd.Location = New System.Drawing.Point(10, 34)
        Me.lblpwd.Name = "lblpwd"
        Me.lblpwd.Size = New System.Drawing.Size(77, 15)
        Me.lblpwd.TabIndex = 3
        Me.lblpwd.Text = "Password :"
        '
        'txtFrom
        '
        Me.txtFrom.Location = New System.Drawing.Point(92, 143)
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(511, 20)
        Me.txtFrom.TabIndex = 2
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.Location = New System.Drawing.Point(33, 146)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(48, 15)
        Me.lblFrom.TabIndex = 0
        Me.lblFrom.Text = "From :"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(233, 613)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(77, 513)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Send"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Frmmail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 698)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnSend)
        Me.Name = "Frmmail"
        Me.Text = "Frmmail"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents btnAttachment As System.Windows.Forms.Button
    Friend WithEvents txtAttachements As System.Windows.Forms.TextBox
    Friend WithEvents lblAttachment As System.Windows.Forms.Label
    Friend WithEvents txtBody As System.Windows.Forms.TextBox
    Friend WithEvents lblbody As System.Windows.Forms.Label
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents lblSub As System.Windows.Forms.Label
    Friend WithEvents txtTo As System.Windows.Forms.TextBox
    Friend WithEvents lblto As System.Windows.Forms.Label
    Friend WithEvents txtPwd As System.Windows.Forms.TextBox
    Friend WithEvents lblpwd As System.Windows.Forms.Label
    Friend WithEvents txtFrom As System.Windows.Forms.TextBox
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents txtcc As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtuser As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtport As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtsmtphost As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cmbencryptcon As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class

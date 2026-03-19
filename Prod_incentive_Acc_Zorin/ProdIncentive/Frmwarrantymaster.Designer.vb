<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmwarrantymaster
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
        Me.txtnomon = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.cmbvehno = New System.Windows.Forms.ComboBox
        Me.txtslno = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.mskwarexdate = New System.Windows.Forms.MaskedTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.mskdop = New System.Windows.Forms.MaskedTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbparts = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.butexit = New System.Windows.Forms.Button
        Me.butsave = New System.Windows.Forms.Button
        Me.butdisp = New System.Windows.Forms.Button
        Me.butdel = New System.Windows.Forms.Button
        Me.butedit = New System.Windows.Forms.Button
        Me.butnew = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Snow
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.TextBox2)
        Me.Panel1.Controls.Add(Me.txtnomon)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.cmbvehno)
        Me.Panel1.Controls.Add(Me.txtslno)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.mskwarexdate)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.mskdop)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cmbparts)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Location = New System.Drawing.Point(229, 55)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(606, 253)
        Me.Panel1.TabIndex = 30
        '
        'txtnomon
        '
        Me.txtnomon.Location = New System.Drawing.Point(352, 114)
        Me.txtnomon.Name = "txtnomon"
        Me.txtnomon.Size = New System.Drawing.Size(94, 20)
        Me.txtnomon.TabIndex = 26
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(263, 115)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(85, 15)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "No.of Month"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(130, 88)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(50, 15)
        Me.Label13.TabIndex = 24
        Me.Label13.Text = "SL.No."
        '
        'cmbvehno
        '
        Me.cmbvehno.FormattingEnabled = True
        Me.cmbvehno.ItemHeight = 13
        Me.cmbvehno.Location = New System.Drawing.Point(190, 62)
        Me.cmbvehno.Name = "cmbvehno"
        Me.cmbvehno.Size = New System.Drawing.Size(269, 21)
        Me.cmbvehno.TabIndex = 23
        '
        'txtslno
        '
        Me.txtslno.Location = New System.Drawing.Point(189, 89)
        Me.txtslno.Name = "txtslno"
        Me.txtslno.Size = New System.Drawing.Size(267, 20)
        Me.txtslno.TabIndex = 22
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Bisque
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.DarkRed
        Me.Label12.Location = New System.Drawing.Point(1, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(603, 26)
        Me.Label12.TabIndex = 21
        Me.Label12.Text = "Spare Parts Warranty Master"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(33, 139)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(150, 15)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Warranty Expired Date"
        '
        'mskwarexdate
        '
        Me.mskwarexdate.Location = New System.Drawing.Point(189, 139)
        Me.mskwarexdate.Mask = "##-##-####"
        Me.mskwarexdate.Name = "mskwarexdate"
        Me.mskwarexdate.Size = New System.Drawing.Size(67, 20)
        Me.mskwarexdate.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(141, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "DOP"
        '
        'mskdop
        '
        Me.mskdop.Location = New System.Drawing.Point(190, 113)
        Me.mskdop.Mask = "##-##-####"
        Me.mskdop.Name = "mskdop"
        Me.mskdop.Size = New System.Drawing.Size(67, 20)
        Me.mskdop.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(108, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Vehicle No"
        '
        'cmbparts
        '
        Me.cmbparts.FormattingEnabled = True
        Me.cmbparts.Location = New System.Drawing.Point(190, 38)
        Me.cmbparts.Name = "cmbparts"
        Me.cmbparts.Size = New System.Drawing.Size(269, 21)
        Me.cmbparts.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(98, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Parts Name"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(136, 167)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 15)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Image"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(189, 167)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(267, 20)
        Me.TextBox1.TabIndex = 4
        '
        'butexit
        '
        Me.butexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butexit.Location = New System.Drawing.Point(606, 314)
        Me.butexit.Name = "butexit"
        Me.butexit.Size = New System.Drawing.Size(57, 24)
        Me.butexit.TabIndex = 35
        Me.butexit.Tag = "15"
        Me.butexit.Text = "Exit"
        Me.butexit.UseVisualStyleBackColor = True
        '
        'butsave
        '
        Me.butsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butsave.Location = New System.Drawing.Point(546, 314)
        Me.butsave.Name = "butsave"
        Me.butsave.Size = New System.Drawing.Size(57, 24)
        Me.butsave.TabIndex = 34
        Me.butsave.Tag = "14"
        Me.butsave.Text = "Save"
        Me.butsave.UseVisualStyleBackColor = True
        '
        'butdisp
        '
        Me.butdisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butdisp.Location = New System.Drawing.Point(485, 314)
        Me.butdisp.Name = "butdisp"
        Me.butdisp.Size = New System.Drawing.Size(57, 24)
        Me.butdisp.TabIndex = 33
        Me.butdisp.Tag = "13"
        Me.butdisp.Text = "Display"
        Me.butdisp.UseVisualStyleBackColor = True
        '
        'butdel
        '
        Me.butdel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butdel.Location = New System.Drawing.Point(426, 314)
        Me.butdel.Name = "butdel"
        Me.butdel.Size = New System.Drawing.Size(57, 24)
        Me.butdel.TabIndex = 31
        Me.butdel.Tag = "12"
        Me.butdel.Text = "Delete"
        Me.butdel.UseVisualStyleBackColor = True
        '
        'butedit
        '
        Me.butedit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butedit.Location = New System.Drawing.Point(366, 314)
        Me.butedit.Name = "butedit"
        Me.butedit.Size = New System.Drawing.Size(57, 24)
        Me.butedit.TabIndex = 32
        Me.butedit.Tag = "11"
        Me.butedit.Text = "Edit"
        Me.butedit.UseVisualStyleBackColor = True
        '
        'butnew
        '
        Me.butnew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butnew.Location = New System.Drawing.Point(305, 314)
        Me.butnew.Name = "butnew"
        Me.butnew.Size = New System.Drawing.Size(57, 24)
        Me.butnew.TabIndex = 29
        Me.butnew.Tag = "1"
        Me.butnew.Text = "New"
        Me.butnew.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(277, 138)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 15)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = " KM"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(316, 137)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(143, 20)
        Me.TextBox2.TabIndex = 27
        '
        'Frmwarrantymaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1064, 493)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.butexit)
        Me.Controls.Add(Me.butsave)
        Me.Controls.Add(Me.butdisp)
        Me.Controls.Add(Me.butdel)
        Me.Controls.Add(Me.butedit)
        Me.Controls.Add(Me.butnew)
        Me.Name = "Frmwarrantymaster"
        Me.Text = "Frmwarrantymaster"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbvehno As System.Windows.Forms.ComboBox
    Friend WithEvents txtslno As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents mskwarexdate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents mskdop As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbparts As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents butexit As System.Windows.Forms.Button
    Friend WithEvents butsave As System.Windows.Forms.Button
    Friend WithEvents butdisp As System.Windows.Forms.Button
    Friend WithEvents butdel As System.Windows.Forms.Button
    Friend WithEvents butedit As System.Windows.Forms.Button
    Friend WithEvents butnew As System.Windows.Forms.Button
    Friend WithEvents txtnomon As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
End Class

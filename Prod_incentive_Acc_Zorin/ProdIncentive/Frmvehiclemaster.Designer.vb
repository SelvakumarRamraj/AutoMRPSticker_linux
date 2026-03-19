<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmvehiclemaster
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
        Me.butexit = New System.Windows.Forms.Button
        Me.butsave = New System.Windows.Forms.Button
        Me.butdisp = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtinsname = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.pictax = New System.Windows.Forms.PictureBox
        Me.picpermit = New System.Windows.Forms.PictureBox
        Me.picins = New System.Windows.Forms.PictureBox
        Me.picfc = New System.Windows.Forms.PictureBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.PictureBox4 = New System.Windows.Forms.PictureBox
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.msktaxdate = New System.Windows.Forms.MaskedTextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.mskpermitdate = New System.Windows.Forms.MaskedTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtrto = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.mskinsdate = New System.Windows.Forms.MaskedTextBox
        Me.cmbvehtype = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtplace = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.mskfcrenewdate = New System.Windows.Forms.MaskedTextBox
        Me.cmbvehno = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbvehname = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.butnew = New System.Windows.Forms.Button
        Me.butedit = New System.Windows.Forms.Button
        Me.butdel = New System.Windows.Forms.Button
        Me.OFDg = New System.Windows.Forms.OpenFileDialog
        Me.ofdg2 = New System.Windows.Forms.OpenFileDialog
        Me.Ofdg3 = New System.Windows.Forms.OpenFileDialog
        Me.Ofdg4 = New System.Windows.Forms.OpenFileDialog
        Me.dg = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel1.SuspendLayout()
        CType(Me.pictax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picpermit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picins, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picfc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'butexit
        '
        Me.butexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butexit.Location = New System.Drawing.Point(644, 508)
        Me.butexit.Name = "butexit"
        Me.butexit.Size = New System.Drawing.Size(57, 24)
        Me.butexit.TabIndex = 28
        Me.butexit.Tag = "15"
        Me.butexit.Text = "Exit"
        Me.butexit.UseVisualStyleBackColor = True
        '
        'butsave
        '
        Me.butsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butsave.Location = New System.Drawing.Point(584, 508)
        Me.butsave.Name = "butsave"
        Me.butsave.Size = New System.Drawing.Size(57, 24)
        Me.butsave.TabIndex = 27
        Me.butsave.Tag = "14"
        Me.butsave.Text = "Save"
        Me.butsave.UseVisualStyleBackColor = True
        '
        'butdisp
        '
        Me.butdisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butdisp.Location = New System.Drawing.Point(523, 508)
        Me.butdisp.Name = "butdisp"
        Me.butdisp.Size = New System.Drawing.Size(57, 24)
        Me.butdisp.TabIndex = 26
        Me.butdisp.Tag = "13"
        Me.butdisp.Text = "Display"
        Me.butdisp.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Snow
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.dg)
        Me.Panel1.Controls.Add(Me.txtinsname)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.pictax)
        Me.Panel1.Controls.Add(Me.picpermit)
        Me.Panel1.Controls.Add(Me.picins)
        Me.Panel1.Controls.Add(Me.picfc)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.PictureBox4)
        Me.Panel1.Controls.Add(Me.PictureBox3)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.msktaxdate)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.mskpermitdate)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtrto)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.mskinsdate)
        Me.Panel1.Controls.Add(Me.cmbvehtype)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtplace)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.mskfcrenewdate)
        Me.Panel1.Controls.Add(Me.cmbvehno)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cmbvehname)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Location = New System.Drawing.Point(127, 45)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(931, 439)
        Me.Panel1.TabIndex = 1
        '
        'txtinsname
        '
        Me.txtinsname.Location = New System.Drawing.Point(190, 186)
        Me.txtinsname.Name = "txtinsname"
        Me.txtinsname.Size = New System.Drawing.Size(267, 20)
        Me.txtinsname.TabIndex = 4
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(489, 278)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(30, 15)
        Me.Label16.TabIndex = 34
        Me.Label16.Text = "Tax"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(171, 274)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(70, 15)
        Me.Label14.TabIndex = 32
        Me.Label14.Text = "Insurance"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(39, 276)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(28, 15)
        Me.Label13.TabIndex = 31
        Me.Label13.Text = "FC "
        '
        'pictax
        '
        Me.pictax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pictax.Location = New System.Drawing.Point(458, 294)
        Me.pictax.Name = "pictax"
        Me.pictax.Size = New System.Drawing.Size(115, 133)
        Me.pictax.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictax.TabIndex = 30
        Me.pictax.TabStop = False
        '
        'picpermit
        '
        Me.picpermit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picpermit.Location = New System.Drawing.Point(303, 294)
        Me.picpermit.Name = "picpermit"
        Me.picpermit.Size = New System.Drawing.Size(115, 133)
        Me.picpermit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picpermit.TabIndex = 29
        Me.picpermit.TabStop = False
        '
        'picins
        '
        Me.picins.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picins.Location = New System.Drawing.Point(153, 294)
        Me.picins.Name = "picins"
        Me.picins.Size = New System.Drawing.Size(115, 133)
        Me.picins.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picins.TabIndex = 28
        Me.picins.TabStop = False
        '
        'picfc
        '
        Me.picfc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picfc.Location = New System.Drawing.Point(4, 294)
        Me.picfc.Name = "picfc"
        Me.picfc.Size = New System.Drawing.Size(115, 133)
        Me.picfc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picfc.TabIndex = 27
        Me.picfc.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.RosyBrown
        Me.Panel2.Location = New System.Drawing.Point(-2, 265)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(606, 6)
        Me.Panel2.TabIndex = 26
        '
        'PictureBox4
        '
        Me.PictureBox4.BackgroundImage = Global.sapbarcode.My.Resources.Resources.attach
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox4.Location = New System.Drawing.Point(554, 276)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(19, 16)
        Me.PictureBox4.TabIndex = 25
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackgroundImage = Global.sapbarcode.My.Resources.Resources.attach
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox3.Location = New System.Drawing.Point(249, 276)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(19, 17)
        Me.PictureBox3.TabIndex = 24
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.sapbarcode.My.Resources.Resources.attach
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(400, 277)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(18, 16)
        Me.PictureBox2.TabIndex = 23
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.sapbarcode.My.Resources.Resources.attach
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(101, 277)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(18, 16)
        Me.PictureBox1.TabIndex = 22
        Me.PictureBox1.TabStop = False
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
        Me.Label12.Text = "Vehicle Master"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(274, 212)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(124, 15)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Tax Renewal Date"
        '
        'msktaxdate
        '
        Me.msktaxdate.Location = New System.Drawing.Point(404, 210)
        Me.msktaxdate.Mask = "##-##-####"
        Me.msktaxdate.Name = "msktaxdate"
        Me.msktaxdate.Size = New System.Drawing.Size(67, 20)
        Me.msktaxdate.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(39, 209)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(143, 15)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Permit Renewal Date"
        '
        'mskpermitdate
        '
        Me.mskpermitdate.Location = New System.Drawing.Point(189, 209)
        Me.mskpermitdate.Mask = "##-##-####"
        Me.mskpermitdate.Name = "mskpermitdate"
        Me.mskpermitdate.Size = New System.Drawing.Size(67, 20)
        Me.mskpermitdate.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(88, 187)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 15)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Insurer Name"
        '
        'txtrto
        '
        Me.txtrto.Location = New System.Drawing.Point(190, 113)
        Me.txtrto.Name = "txtrto"
        Me.txtrto.Size = New System.Drawing.Size(281, 20)
        Me.txtrto.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(20, 162)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(164, 15)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Insurance Renewal Date"
        '
        'mskinsdate
        '
        Me.mskinsdate.Location = New System.Drawing.Point(190, 162)
        Me.mskinsdate.Mask = "##-##-####"
        Me.mskinsdate.Name = "mskinsdate"
        Me.mskinsdate.Size = New System.Drawing.Size(67, 20)
        Me.mskinsdate.TabIndex = 6
        '
        'cmbvehtype
        '
        Me.cmbvehtype.FormattingEnabled = True
        Me.cmbvehtype.ItemHeight = 13
        Me.cmbvehtype.Location = New System.Drawing.Point(187, 236)
        Me.cmbvehtype.Name = "cmbvehtype"
        Me.cmbvehtype.Size = New System.Drawing.Size(269, 21)
        Me.cmbvehtype.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(116, 237)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(65, 15)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Veh.Type"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(140, 140)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 15)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Place"
        '
        'txtplace
        '
        Me.txtplace.Location = New System.Drawing.Point(189, 139)
        Me.txtplace.Name = "txtplace"
        Me.txtplace.Size = New System.Drawing.Size(267, 20)
        Me.txtplace.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(66, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "FC Renewal Date"
        '
        'mskfcrenewdate
        '
        Me.mskfcrenewdate.Location = New System.Drawing.Point(190, 89)
        Me.mskfcrenewdate.Mask = "##-##-####"
        Me.mskfcrenewdate.Name = "mskfcrenewdate"
        Me.mskfcrenewdate.Size = New System.Drawing.Size(67, 20)
        Me.mskfcrenewdate.TabIndex = 3
        '
        'cmbvehno
        '
        Me.cmbvehno.FormattingEnabled = True
        Me.cmbvehno.Location = New System.Drawing.Point(190, 62)
        Me.cmbvehno.Name = "cmbvehno"
        Me.cmbvehno.Size = New System.Drawing.Size(269, 21)
        Me.cmbvehno.TabIndex = 2
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
        'cmbvehname
        '
        Me.cmbvehname.FormattingEnabled = True
        Me.cmbvehname.Location = New System.Drawing.Point(190, 38)
        Me.cmbvehname.Name = "cmbvehname"
        Me.cmbvehname.Size = New System.Drawing.Size(269, 21)
        Me.cmbvehname.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(88, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Vehicle Name"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(148, 115)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 15)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "RTO"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(333, 277)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(49, 15)
        Me.Label15.TabIndex = 33
        Me.Label15.Text = "Permit"
        '
        'butnew
        '
        Me.butnew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butnew.Location = New System.Drawing.Point(343, 508)
        Me.butnew.Name = "butnew"
        Me.butnew.Size = New System.Drawing.Size(57, 24)
        Me.butnew.TabIndex = 0
        Me.butnew.Tag = "1"
        Me.butnew.Text = "New"
        Me.butnew.UseVisualStyleBackColor = True
        '
        'butedit
        '
        Me.butedit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butedit.Location = New System.Drawing.Point(404, 508)
        Me.butedit.Name = "butedit"
        Me.butedit.Size = New System.Drawing.Size(57, 24)
        Me.butedit.TabIndex = 24
        Me.butedit.Tag = "11"
        Me.butedit.Text = "Edit"
        Me.butedit.UseVisualStyleBackColor = True
        '
        'butdel
        '
        Me.butdel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butdel.Location = New System.Drawing.Point(464, 508)
        Me.butdel.Name = "butdel"
        Me.butdel.Size = New System.Drawing.Size(57, 24)
        Me.butdel.TabIndex = 8
        Me.butdel.Tag = "12"
        Me.butdel.Text = "Delete"
        Me.butdel.UseVisualStyleBackColor = True
        '
        'OFDg
        '
        Me.OFDg.FileName = "OpenFileDialog1"
        '
        'ofdg2
        '
        Me.ofdg2.FileName = "OpenFileDialog1"
        '
        'Ofdg3
        '
        Me.Ofdg3.FileName = "OpenFileDialog1"
        '
        'Ofdg4
        '
        Me.Ofdg4.FileName = "OpenFileDialog1"
        '
        'dg
        '
        Me.dg.AllowUserToAddRows = False
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
        Me.dg.Location = New System.Drawing.Point(603, -2)
        Me.dg.Name = "dg"
        Me.dg.RowHeadersVisible = False
        Me.dg.Size = New System.Drawing.Size(321, 434)
        Me.dg.TabIndex = 35
        '
        'Column1
        '
        Me.Column1.HeaderText = "Veh.No"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "Vehicle Name"
        Me.Column2.Name = "Column2"
        '
        'Frmvehiclemaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1070, 703)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.butexit)
        Me.Controls.Add(Me.butsave)
        Me.Controls.Add(Me.butdisp)
        Me.Controls.Add(Me.butdel)
        Me.Controls.Add(Me.butedit)
        Me.Controls.Add(Me.butnew)
        Me.Name = "Frmvehiclemaster"
        Me.Text = "Frmvehiclemaster"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.pictax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picpermit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picins, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picfc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents butexit As System.Windows.Forms.Button
    Friend WithEvents butsave As System.Windows.Forms.Button
    Friend WithEvents butdisp As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents butnew As System.Windows.Forms.Button
    Friend WithEvents butedit As System.Windows.Forms.Button
    Friend WithEvents butdel As System.Windows.Forms.Button
    Friend WithEvents cmbvehname As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents mskfcrenewdate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmbvehno As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtrto As System.Windows.Forms.TextBox
    Friend WithEvents cmbvehtype As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtplace As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents mskinsdate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents msktaxdate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents mskpermitdate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtinsname As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents pictax As System.Windows.Forms.PictureBox
    Friend WithEvents picpermit As System.Windows.Forms.PictureBox
    Friend WithEvents picins As System.Windows.Forms.PictureBox
    Friend WithEvents picfc As System.Windows.Forms.PictureBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents OFDg As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ofdg2 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Ofdg3 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Ofdg4 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents dg As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class

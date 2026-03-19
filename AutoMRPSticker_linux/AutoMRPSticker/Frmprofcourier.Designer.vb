<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmprofcourier
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frmprofcourier))
        Me.cmbcomp = New System.Windows.Forms.ComboBox()
        Me.txtcardcode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblname = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtno = New System.Windows.Forms.TextBox()
        Me.flx = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.cmbstate = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbagent = New System.Windows.Forms.ComboBox()
        Me.chkbox = New System.Windows.Forms.CheckBox()
        Me.cmddisp = New System.Windows.Forms.Button()
        Me.cmdprint = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.chkperson = New System.Windows.Forms.CheckBox()
        Me.cmdsel = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbcity = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbdist = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtport = New System.Windows.Forms.TextBox()
        Me.txt1 = New System.Windows.Forms.TextBox()
        Me.txt2 = New System.Windows.Forms.TextBox()
        Me.chkdirprn = New System.Windows.Forms.CheckBox()
        Me.chkdealparty = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtpodno = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.mskdate = New System.Windows.Forms.MaskedTextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtport2 = New System.Windows.Forms.TextBox()
        Me.cmdrep = New System.Windows.Forms.Button()
        Me.cmdexcel = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.butbrowse = New System.Windows.Forms.Button()
        Me.txtfile = New System.Windows.Forms.TextBox()
        Me.Dialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.cmbver = New System.Windows.Forms.ComboBox()
        Me.chkexcel = New System.Windows.Forms.CheckBox()
        Me.txtremark = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.flxt = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.butsearch = New System.Windows.Forms.Button()
        Me.butsrchexcel = New System.Windows.Forms.Button()
        Me.chksearch = New System.Windows.Forms.CheckBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.CMBYR = New System.Windows.Forms.ComboBox()
        Me.chkvendor = New System.Windows.Forms.CheckBox()
        Me.txtcourierno = New System.Windows.Forms.TextBox()
        Me.txtdocentry = New System.Windows.Forms.TextBox()
        Me.chkprn = New System.Windows.Forms.CheckBox()
        Me.chkwinv = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtnocopy = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        CType(Me.flx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbcomp
        '
        Me.cmbcomp.FormattingEnabled = True
        Me.cmbcomp.Location = New System.Drawing.Point(104, 7)
        Me.cmbcomp.Name = "cmbcomp"
        Me.cmbcomp.Size = New System.Drawing.Size(253, 21)
        Me.cmbcomp.TabIndex = 0
        '
        'txtcardcode
        '
        Me.txtcardcode.Location = New System.Drawing.Point(344, 85)
        Me.txtcardcode.Name = "txtcardcode"
        Me.txtcardcode.Size = New System.Drawing.Size(222, 20)
        Me.txtcardcode.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Company Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(276, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Cardcode"
        '
        'lblname
        '
        Me.lblname.BackColor = System.Drawing.Color.NavajoWhite
        Me.lblname.Location = New System.Drawing.Point(572, 38)
        Me.lblname.Name = "lblname"
        Me.lblname.Size = New System.Drawing.Size(381, 27)
        Me.lblname.TabIndex = 4
        Me.lblname.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(268, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Invoice No"
        '
        'txtno
        '
        Me.txtno.Location = New System.Drawing.Point(344, 35)
        Me.txtno.Name = "txtno"
        Me.txtno.Size = New System.Drawing.Size(222, 20)
        Me.txtno.TabIndex = 5
        '
        'flx
        '
        Me.flx.Location = New System.Drawing.Point(92, 198)
        Me.flx.Name = "flx"
        Me.flx.OcxState = CType(resources.GetObject("flx.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flx.Size = New System.Drawing.Size(780, 372)
        Me.flx.TabIndex = 7
        '
        'cmbstate
        '
        Me.cmbstate.FormattingEnabled = True
        Me.cmbstate.Location = New System.Drawing.Point(214, 119)
        Me.cmbstate.Name = "cmbstate"
        Me.cmbstate.Size = New System.Drawing.Size(93, 21)
        Me.cmbstate.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(170, 122)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "State"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(314, 125)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Agent"
        '
        'cmbagent
        '
        Me.cmbagent.FormattingEnabled = True
        Me.cmbagent.Location = New System.Drawing.Point(355, 122)
        Me.cmbagent.Name = "cmbagent"
        Me.cmbagent.Size = New System.Drawing.Size(214, 21)
        Me.cmbagent.TabIndex = 10
        '
        'chkbox
        '
        Me.chkbox.AutoSize = True
        Me.chkbox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbox.Location = New System.Drawing.Point(575, 132)
        Me.chkbox.Name = "chkbox"
        Me.chkbox.Size = New System.Drawing.Size(77, 17)
        Me.chkbox.TabIndex = 12
        Me.chkbox.Text = "Print Box"
        Me.chkbox.UseVisualStyleBackColor = True
        '
        'cmddisp
        '
        Me.cmddisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddisp.Location = New System.Drawing.Point(642, 162)
        Me.cmddisp.Name = "cmddisp"
        Me.cmddisp.Size = New System.Drawing.Size(70, 30)
        Me.cmddisp.TabIndex = 13
        Me.cmddisp.Text = "Display"
        Me.cmddisp.UseVisualStyleBackColor = True
        '
        'cmdprint
        '
        Me.cmdprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdprint.Location = New System.Drawing.Point(718, 162)
        Me.cmdprint.Name = "cmdprint"
        Me.cmdprint.Size = New System.Drawing.Size(70, 30)
        Me.cmdprint.TabIndex = 14
        Me.cmdprint.Text = "Print"
        Me.cmdprint.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.Location = New System.Drawing.Point(794, 162)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(70, 30)
        Me.cmdexit.TabIndex = 15
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'chkperson
        '
        Me.chkperson.AutoSize = True
        Me.chkperson.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkperson.Location = New System.Drawing.Point(653, 133)
        Me.chkperson.Name = "chkperson"
        Me.chkperson.Size = New System.Drawing.Size(75, 17)
        Me.chkperson.TabIndex = 16
        Me.chkperson.Text = "Personal"
        Me.chkperson.UseVisualStyleBackColor = True
        '
        'cmdsel
        '
        Me.cmdsel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsel.Location = New System.Drawing.Point(3, 198)
        Me.cmdsel.Name = "cmdsel"
        Me.cmdsel.Size = New System.Drawing.Size(83, 30)
        Me.cmdsel.TabIndex = 17
        Me.cmdsel.Text = "Select All"
        Me.cmdsel.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(179, 149)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 13)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "City"
        '
        'cmbcity
        '
        Me.cmbcity.FormattingEnabled = True
        Me.cmbcity.Location = New System.Drawing.Point(214, 146)
        Me.cmbcity.Name = "cmbcity"
        Me.cmbcity.Size = New System.Drawing.Size(229, 21)
        Me.cmbcity.TabIndex = 18
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(160, 176)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(47, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "District"
        '
        'cmbdist
        '
        Me.cmbdist.FormattingEnabled = True
        Me.cmbdist.Location = New System.Drawing.Point(214, 173)
        Me.cmbdist.Name = "cmbdist"
        Me.cmbdist.Size = New System.Drawing.Size(229, 21)
        Me.cmbdist.TabIndex = 20
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(524, 11)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 16)
        Me.Label10.TabIndex = 227
        Me.Label10.Text = "LPT Port"
        '
        'txtport
        '
        Me.txtport.Location = New System.Drawing.Point(585, 8)
        Me.txtport.Name = "txtport"
        Me.txtport.Size = New System.Drawing.Size(70, 20)
        Me.txtport.TabIndex = 226
        Me.txtport.Text = "1"
        '
        'txt1
        '
        Me.txt1.Location = New System.Drawing.Point(576, 83)
        Me.txt1.Name = "txt1"
        Me.txt1.Size = New System.Drawing.Size(53, 20)
        Me.txt1.TabIndex = 228
        '
        'txt2
        '
        Me.txt2.Location = New System.Drawing.Point(635, 83)
        Me.txt2.Name = "txt2"
        Me.txt2.Size = New System.Drawing.Size(47, 20)
        Me.txt2.TabIndex = 229
        '
        'chkdirprn
        '
        Me.chkdirprn.AutoSize = True
        Me.chkdirprn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkdirprn.Location = New System.Drawing.Point(742, 133)
        Me.chkdirprn.Name = "chkdirprn"
        Me.chkdirprn.Size = New System.Drawing.Size(90, 17)
        Me.chkdirprn.TabIndex = 230
        Me.chkdirprn.Text = "Print Direct"
        Me.chkdirprn.UseVisualStyleBackColor = True
        Me.chkdirprn.Visible = False
        '
        'chkdealparty
        '
        Me.chkdealparty.AutoSize = True
        Me.chkdealparty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkdealparty.Location = New System.Drawing.Point(79, 123)
        Me.chkdealparty.Name = "chkdealparty"
        Me.chkdealparty.Size = New System.Drawing.Size(85, 17)
        Me.chkdealparty.TabIndex = 231
        Me.chkdealparty.Text = "Deal Party"
        Me.chkdealparty.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 13)
        Me.Label8.TabIndex = 233
        Me.Label8.Text = "POD.No"
        '
        'txtpodno
        '
        Me.txtpodno.Location = New System.Drawing.Point(65, 61)
        Me.txtpodno.Name = "txtpodno"
        Me.txtpodno.Size = New System.Drawing.Size(222, 20)
        Me.txtpodno.TabIndex = 232
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(659, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(36, 20)
        Me.Label9.TabIndex = 235
        Me.Label9.Text = "Date"
        '
        'mskdate
        '
        Me.mskdate.Location = New System.Drawing.Point(698, 8)
        Me.mskdate.Mask = "##-##-####"
        Me.mskdate.Name = "mskdate"
        Me.mskdate.Size = New System.Drawing.Size(107, 20)
        Me.mskdate.TabIndex = 234
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(38, 35)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(138, 20)
        Me.Label11.TabIndex = 237
        Me.Label11.Text = "LPT Port for Barcode"
        '
        'txtport2
        '
        Me.txtport2.Location = New System.Drawing.Point(182, 35)
        Me.txtport2.Name = "txtport2"
        Me.txtport2.Size = New System.Drawing.Size(70, 20)
        Me.txtport2.TabIndex = 236
        Me.txtport2.Text = "2"
        '
        'cmdrep
        '
        Me.cmdrep.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdrep.Location = New System.Drawing.Point(808, 4)
        Me.cmdrep.Name = "cmdrep"
        Me.cmdrep.Size = New System.Drawing.Size(70, 27)
        Me.cmdrep.TabIndex = 238
        Me.cmdrep.Text = "Report"
        Me.cmdrep.UseVisualStyleBackColor = True
        '
        'cmdexcel
        '
        Me.cmdexcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexcel.Location = New System.Drawing.Point(879, 4)
        Me.cmdexcel.Name = "cmdexcel"
        Me.cmdexcel.Size = New System.Drawing.Size(70, 27)
        Me.cmdexcel.TabIndex = 239
        Me.cmdexcel.Text = "Excel"
        Me.cmdexcel.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(6, 442)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(82, 12)
        Me.Label12.TabIndex = 240
        Me.Label12.Text = "0"
        '
        'butbrowse
        '
        Me.butbrowse.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butbrowse.Location = New System.Drawing.Point(893, 109)
        Me.butbrowse.Name = "butbrowse"
        Me.butbrowse.Size = New System.Drawing.Size(60, 23)
        Me.butbrowse.TabIndex = 241
        Me.butbrowse.Text = "Browse"
        Me.butbrowse.UseVisualStyleBackColor = True
        '
        'txtfile
        '
        Me.txtfile.Location = New System.Drawing.Point(688, 109)
        Me.txtfile.Name = "txtfile"
        Me.txtfile.Size = New System.Drawing.Size(199, 20)
        Me.txtfile.TabIndex = 242
        '
        'Dialog1
        '
        Me.Dialog1.FileName = "OpenFileDialog1"
        '
        'cmbver
        '
        Me.cmbver.FormattingEnabled = True
        Me.cmbver.Location = New System.Drawing.Point(875, 149)
        Me.cmbver.Name = "cmbver"
        Me.cmbver.Size = New System.Drawing.Size(74, 21)
        Me.cmbver.TabIndex = 243
        '
        'chkexcel
        '
        Me.chkexcel.AutoSize = True
        Me.chkexcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkexcel.Location = New System.Drawing.Point(625, 113)
        Me.chkexcel.Name = "chkexcel"
        Me.chkexcel.Size = New System.Drawing.Size(57, 17)
        Me.chkexcel.TabIndex = 244
        Me.chkexcel.Text = "Excel"
        Me.chkexcel.UseVisualStyleBackColor = True
        '
        'txtremark
        '
        Me.txtremark.Location = New System.Drawing.Point(754, 83)
        Me.txtremark.Name = "txtremark"
        Me.txtremark.Size = New System.Drawing.Size(199, 20)
        Me.txtremark.TabIndex = 245
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(695, 88)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(50, 13)
        Me.Label13.TabIndex = 246
        Me.Label13.Text = "Remark"
        '
        'flxt
        '
        Me.flxt.Location = New System.Drawing.Point(873, 204)
        Me.flxt.Name = "flxt"
        Me.flxt.OcxState = CType(resources.GetObject("flxt.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxt.Size = New System.Drawing.Size(321, 186)
        Me.flxt.TabIndex = 247
        '
        'butsearch
        '
        Me.butsearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butsearch.Location = New System.Drawing.Point(893, 176)
        Me.butsearch.Name = "butsearch"
        Me.butsearch.Size = New System.Drawing.Size(70, 27)
        Me.butsearch.TabIndex = 248
        Me.butsearch.Text = "Search"
        Me.butsearch.UseVisualStyleBackColor = True
        '
        'butsrchexcel
        '
        Me.butsrchexcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butsrchexcel.Location = New System.Drawing.Point(969, 176)
        Me.butsrchexcel.Name = "butsrchexcel"
        Me.butsrchexcel.Size = New System.Drawing.Size(70, 27)
        Me.butsrchexcel.TabIndex = 249
        Me.butsrchexcel.Text = "Excel"
        Me.butsrchexcel.UseVisualStyleBackColor = True
        '
        'chksearch
        '
        Me.chksearch.AutoSize = True
        Me.chksearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chksearch.Location = New System.Drawing.Point(959, 88)
        Me.chksearch.Name = "chksearch"
        Me.chksearch.Size = New System.Drawing.Size(66, 17)
        Me.chksearch.TabIndex = 250
        Me.chksearch.Text = "Search"
        Me.chksearch.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(365, 9)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(66, 18)
        Me.Label14.TabIndex = 251
        Me.Label14.Text = "Finace Yr"
        '
        'CMBYR
        '
        Me.CMBYR.FormattingEnabled = True
        Me.CMBYR.Location = New System.Drawing.Point(427, 7)
        Me.CMBYR.Name = "CMBYR"
        Me.CMBYR.Size = New System.Drawing.Size(95, 21)
        Me.CMBYR.TabIndex = 252
        '
        'chkvendor
        '
        Me.chkvendor.AutoSize = True
        Me.chkvendor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkvendor.Location = New System.Drawing.Point(79, 100)
        Me.chkvendor.Name = "chkvendor"
        Me.chkvendor.Size = New System.Drawing.Size(98, 17)
        Me.chkvendor.TabIndex = 253
        Me.chkvendor.Text = "Vendor Type"
        Me.chkvendor.UseVisualStyleBackColor = True
        '
        'txtcourierno
        '
        Me.txtcourierno.Enabled = False
        Me.txtcourierno.Location = New System.Drawing.Point(293, 61)
        Me.txtcourierno.Name = "txtcourierno"
        Me.txtcourierno.Size = New System.Drawing.Size(186, 20)
        Me.txtcourierno.TabIndex = 254
        '
        'txtdocentry
        '
        Me.txtdocentry.Enabled = False
        Me.txtdocentry.Location = New System.Drawing.Point(959, 38)
        Me.txtdocentry.Name = "txtdocentry"
        Me.txtdocentry.Size = New System.Drawing.Size(222, 20)
        Me.txtdocentry.TabIndex = 256
        '
        'chkprn
        '
        Me.chkprn.AutoSize = True
        Me.chkprn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkprn.Location = New System.Drawing.Point(3, 83)
        Me.chkprn.Name = "chkprn"
        Me.chkprn.Size = New System.Drawing.Size(52, 17)
        Me.chkprn.TabIndex = 257
        Me.chkprn.Text = "Print"
        Me.chkprn.UseVisualStyleBackColor = True
        '
        'chkwinv
        '
        Me.chkwinv.AutoSize = True
        Me.chkwinv.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkwinv.Location = New System.Drawing.Point(65, 149)
        Me.chkwinv.Name = "chkwinv"
        Me.chkwinv.Size = New System.Drawing.Size(98, 17)
        Me.chkwinv.TabIndex = 258
        Me.chkwinv.Text = "With Invoice"
        Me.chkwinv.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(1111, 141)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(70, 48)
        Me.Button1.TabIndex = 259
        Me.Button1.Text = "Company Address Print"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtnocopy
        '
        Me.txtnocopy.Location = New System.Drawing.Point(1035, 146)
        Me.txtnocopy.Name = "txtnocopy"
        Me.txtnocopy.Size = New System.Drawing.Size(61, 20)
        Me.txtnocopy.TabIndex = 260
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(956, 153)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(70, 13)
        Me.Label15.TabIndex = 261
        Me.Label15.Text = "No of Copy"
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'Frmprofcourier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1206, 698)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtnocopy)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.chkwinv)
        Me.Controls.Add(Me.chkprn)
        Me.Controls.Add(Me.txtdocentry)
        Me.Controls.Add(Me.txtcourierno)
        Me.Controls.Add(Me.chkvendor)
        Me.Controls.Add(Me.CMBYR)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.chksearch)
        Me.Controls.Add(Me.butsrchexcel)
        Me.Controls.Add(Me.butsearch)
        Me.Controls.Add(Me.flxt)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtremark)
        Me.Controls.Add(Me.chkexcel)
        Me.Controls.Add(Me.cmbver)
        Me.Controls.Add(Me.txtfile)
        Me.Controls.Add(Me.butbrowse)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.cmdexcel)
        Me.Controls.Add(Me.cmdrep)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtport2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.mskdate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtpodno)
        Me.Controls.Add(Me.chkdealparty)
        Me.Controls.Add(Me.chkdirprn)
        Me.Controls.Add(Me.txt2)
        Me.Controls.Add(Me.txt1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtport)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmbdist)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbcity)
        Me.Controls.Add(Me.cmdsel)
        Me.Controls.Add(Me.chkperson)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdprint)
        Me.Controls.Add(Me.cmddisp)
        Me.Controls.Add(Me.chkbox)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbagent)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbstate)
        Me.Controls.Add(Me.flx)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtno)
        Me.Controls.Add(Me.lblname)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtcardcode)
        Me.Controls.Add(Me.cmbcomp)
        Me.Name = "Frmprofcourier"
        Me.Text = "Frmprofcourier"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.flx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbcomp As System.Windows.Forms.ComboBox
    Friend WithEvents txtcardcode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblname As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtno As System.Windows.Forms.TextBox
    Friend WithEvents flx As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents cmbstate As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbagent As System.Windows.Forms.ComboBox
    Friend WithEvents chkbox As System.Windows.Forms.CheckBox
    Friend WithEvents cmddisp As System.Windows.Forms.Button
    Friend WithEvents cmdprint As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents chkperson As System.Windows.Forms.CheckBox
    Friend WithEvents cmdsel As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbcity As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbdist As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtport As System.Windows.Forms.TextBox
    Friend WithEvents txt1 As System.Windows.Forms.TextBox
    Friend WithEvents txt2 As System.Windows.Forms.TextBox
    Friend WithEvents chkdirprn As System.Windows.Forms.CheckBox
    Friend WithEvents chkdealparty As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtpodno As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents mskdate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtport2 As System.Windows.Forms.TextBox
    Friend WithEvents cmdrep As System.Windows.Forms.Button
    Friend WithEvents cmdexcel As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents butbrowse As System.Windows.Forms.Button
    Friend WithEvents txtfile As System.Windows.Forms.TextBox
    Friend WithEvents Dialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmbver As System.Windows.Forms.ComboBox
    Friend WithEvents chkexcel As System.Windows.Forms.CheckBox
    Friend WithEvents txtremark As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents flxt As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents butsearch As System.Windows.Forms.Button
    Friend WithEvents butsrchexcel As System.Windows.Forms.Button
    Friend WithEvents chksearch As System.Windows.Forms.CheckBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents CMBYR As System.Windows.Forms.ComboBox
    Friend WithEvents chkvendor As System.Windows.Forms.CheckBox
    Friend WithEvents txtcourierno As System.Windows.Forms.TextBox
    Friend WithEvents txtdocentry As System.Windows.Forms.TextBox
    Friend WithEvents chkprn As System.Windows.Forms.CheckBox
    Friend WithEvents chkwinv As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtnocopy As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
End Class

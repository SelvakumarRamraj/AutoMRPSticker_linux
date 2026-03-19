<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmpackage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frmpackage))
        Me.cmdsend = New System.Windows.Forms.Button
        Me.cmdrcd = New System.Windows.Forms.Button
        Me.txtno = New System.Windows.Forms.TextBox
        Me.lbldate2 = New System.Windows.Forms.Label
        Me.lblamt = New System.Windows.Forms.Label
        Me.lblparty = New System.Windows.Forms.Label
        Me.lbldocentry = New System.Windows.Forms.Label
        Me.cmbboxno = New System.Windows.Forms.ComboBox
        Me.cmbboxtype = New System.Windows.Forms.ComboBox
        Me.cmbwgt = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmdupdt = New System.Windows.Forms.Button
        Me.flxp = New AxMSFlexGridLib.AxMSFlexGrid
        Me.flxc = New AxMSFlexGridLib.AxMSFlexGrid
        Me.flxh = New AxMSFlexGridLib.AxMSFlexGrid
        Me.lblptot = New System.Windows.Forms.Label
        Me.lblctot = New System.Windows.Forms.Label
        Me.cmdexit = New System.Windows.Forms.Button
        Me.cmdclear = New System.Windows.Forms.Button
        Me.cmbboxtype1 = New System.Windows.Forms.ComboBox
        Me.cmbwgt1 = New System.Windows.Forms.ComboBox
        Me.cmdadd = New System.Windows.Forms.Button
        Me.cmdprint = New System.Windows.Forms.Button
        Me.cmbtype = New System.Windows.Forms.ComboBox
        Me.lblctot2 = New System.Windows.Forms.Label
        Me.mskdate = New System.Windows.Forms.MaskedTextBox
        Me.lbldate = New System.Windows.Forms.MaskedTextBox
        Me.view1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.chkprod = New System.Windows.Forms.CheckBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.cmbyear = New System.Windows.Forms.ComboBox
        Me.chkscheme = New System.Windows.Forms.CheckBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.chkinvprn = New System.Windows.Forms.CheckBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtport = New System.Windows.Forms.TextBox
        CType(Me.flxp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdsend
        '
        Me.cmdsend.Location = New System.Drawing.Point(577, 207)
        Me.cmdsend.Name = "cmdsend"
        Me.cmdsend.Size = New System.Drawing.Size(58, 20)
        Me.cmdsend.TabIndex = 3
        Me.cmdsend.Text = " >"
        Me.cmdsend.UseVisualStyleBackColor = True
        '
        'cmdrcd
        '
        Me.cmdrcd.Location = New System.Drawing.Point(1085, 104)
        Me.cmdrcd.Name = "cmdrcd"
        Me.cmdrcd.Size = New System.Drawing.Size(58, 20)
        Me.cmdrcd.TabIndex = 4
        Me.cmdrcd.Text = "Delete"
        Me.cmdrcd.UseVisualStyleBackColor = True
        '
        'txtno
        '
        Me.txtno.Location = New System.Drawing.Point(247, 13)
        Me.txtno.Name = "txtno"
        Me.txtno.Size = New System.Drawing.Size(148, 20)
        Me.txtno.TabIndex = 2
        '
        'lbldate2
        '
        Me.lbldate2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbldate2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldate2.Location = New System.Drawing.Point(493, 39)
        Me.lbldate2.Name = "lbldate2"
        Me.lbldate2.Size = New System.Drawing.Size(129, 23)
        Me.lbldate2.TabIndex = 6
        Me.lbldate2.Text = "Label1"
        Me.lbldate2.Visible = False
        '
        'lblamt
        '
        Me.lblamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblamt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblamt.Location = New System.Drawing.Point(913, 13)
        Me.lblamt.Name = "lblamt"
        Me.lblamt.Size = New System.Drawing.Size(129, 23)
        Me.lblamt.TabIndex = 7
        Me.lblamt.Text = "0.00"
        Me.lblamt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblparty
        '
        Me.lblparty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblparty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblparty.Location = New System.Drawing.Point(493, 13)
        Me.lblparty.Name = "lblparty"
        Me.lblparty.Size = New System.Drawing.Size(386, 23)
        Me.lblparty.TabIndex = 8
        Me.lblparty.Text = "Label1"
        '
        'lbldocentry
        '
        Me.lbldocentry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbldocentry.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldocentry.Location = New System.Drawing.Point(247, 36)
        Me.lbldocentry.Name = "lbldocentry"
        Me.lbldocentry.Size = New System.Drawing.Size(148, 23)
        Me.lbldocentry.TabIndex = 9
        '
        'cmbboxno
        '
        Me.cmbboxno.FormattingEnabled = True
        Me.cmbboxno.Location = New System.Drawing.Point(31, 90)
        Me.cmbboxno.Name = "cmbboxno"
        Me.cmbboxno.Size = New System.Drawing.Size(148, 21)
        Me.cmbboxno.TabIndex = 10
        '
        'cmbboxtype
        '
        Me.cmbboxtype.FormattingEnabled = True
        Me.cmbboxtype.Location = New System.Drawing.Point(212, 90)
        Me.cmbboxtype.Name = "cmbboxtype"
        Me.cmbboxtype.Size = New System.Drawing.Size(148, 21)
        Me.cmbboxtype.TabIndex = 11
        '
        'cmbwgt
        '
        Me.cmbwgt.FormattingEnabled = True
        Me.cmbwgt.Location = New System.Drawing.Point(366, 90)
        Me.cmbwgt.Name = "cmbwgt"
        Me.cmbwgt.Size = New System.Drawing.Size(172, 21)
        Me.cmbwgt.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(52, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Box No"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(223, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Packing Type"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(396, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Measurement"
        '
        'cmdupdt
        '
        Me.cmdupdt.Location = New System.Drawing.Point(577, 567)
        Me.cmdupdt.Name = "cmdupdt"
        Me.cmdupdt.Size = New System.Drawing.Size(58, 26)
        Me.cmdupdt.TabIndex = 16
        Me.cmdupdt.Text = "Update"
        Me.cmdupdt.UseVisualStyleBackColor = True
        '
        'flxp
        '
        Me.flxp.Location = New System.Drawing.Point(640, 163)
        Me.flxp.Name = "flxp"
        Me.flxp.OcxState = CType(resources.GetObject("flxp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxp.Size = New System.Drawing.Size(466, 452)
        Me.flxp.TabIndex = 2
        '
        'flxc
        '
        Me.flxc.Location = New System.Drawing.Point(55, 163)
        Me.flxc.Name = "flxc"
        Me.flxc.OcxState = CType(resources.GetObject("flxc.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxc.Size = New System.Drawing.Size(522, 452)
        Me.flxc.TabIndex = 1
        '
        'flxh
        '
        Me.flxh.Location = New System.Drawing.Point(640, 52)
        Me.flxh.Name = "flxh"
        Me.flxh.OcxState = CType(resources.GetObject("flxh.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxh.Size = New System.Drawing.Size(439, 87)
        Me.flxh.TabIndex = 0
        '
        'lblptot
        '
        Me.lblptot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblptot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblptot.Location = New System.Drawing.Point(797, 626)
        Me.lblptot.Name = "lblptot"
        Me.lblptot.Size = New System.Drawing.Size(123, 26)
        Me.lblptot.TabIndex = 17
        Me.lblptot.Text = "0"
        Me.lblptot.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblctot
        '
        Me.lblctot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblctot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblctot.Location = New System.Drawing.Point(454, 618)
        Me.lblctot.Name = "lblctot"
        Me.lblctot.Size = New System.Drawing.Size(123, 26)
        Me.lblctot.TabIndex = 18
        Me.lblctot.Text = "0"
        Me.lblctot.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdexit
        '
        Me.cmdexit.Location = New System.Drawing.Point(1160, 234)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(58, 26)
        Me.cmdexit.TabIndex = 19
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'cmdclear
        '
        Me.cmdclear.Location = New System.Drawing.Point(1160, 74)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(58, 26)
        Me.cmdclear.TabIndex = 20
        Me.cmdclear.Text = "Clear"
        Me.cmdclear.UseVisualStyleBackColor = True
        '
        'cmbboxtype1
        '
        Me.cmbboxtype1.FormattingEnabled = True
        Me.cmbboxtype1.Location = New System.Drawing.Point(212, 118)
        Me.cmbboxtype1.Name = "cmbboxtype1"
        Me.cmbboxtype1.Size = New System.Drawing.Size(148, 21)
        Me.cmbboxtype1.TabIndex = 21
        Me.cmbboxtype1.Visible = False
        '
        'cmbwgt1
        '
        Me.cmbwgt1.FormattingEnabled = True
        Me.cmbwgt1.Location = New System.Drawing.Point(455, 118)
        Me.cmbwgt1.Name = "cmbwgt1"
        Me.cmbwgt1.Size = New System.Drawing.Size(172, 21)
        Me.cmbwgt1.TabIndex = 22
        Me.cmbwgt1.Visible = False
        '
        'cmdadd
        '
        Me.cmdadd.Location = New System.Drawing.Point(553, 85)
        Me.cmdadd.Name = "cmdadd"
        Me.cmdadd.Size = New System.Drawing.Size(58, 26)
        Me.cmdadd.TabIndex = 23
        Me.cmdadd.Text = "Add"
        Me.cmdadd.UseVisualStyleBackColor = True
        '
        'cmdprint
        '
        Me.cmdprint.Location = New System.Drawing.Point(1160, 118)
        Me.cmdprint.Name = "cmdprint"
        Me.cmdprint.Size = New System.Drawing.Size(58, 26)
        Me.cmdprint.TabIndex = 24
        Me.cmdprint.Text = "Print"
        Me.cmdprint.UseVisualStyleBackColor = True
        '
        'cmbtype
        '
        Me.cmbtype.FormattingEnabled = True
        Me.cmbtype.Location = New System.Drawing.Point(150, 11)
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.Size = New System.Drawing.Size(91, 21)
        Me.cmbtype.TabIndex = 1
        '
        'lblctot2
        '
        Me.lblctot2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblctot2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblctot2.ForeColor = System.Drawing.Color.Maroon
        Me.lblctot2.Location = New System.Drawing.Point(55, 618)
        Me.lblctot2.Name = "lblctot2"
        Me.lblctot2.Size = New System.Drawing.Size(123, 26)
        Me.lblctot2.TabIndex = 27
        Me.lblctot2.Text = "0"
        Me.lblctot2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'mskdate
        '
        Me.mskdate.Location = New System.Drawing.Point(401, 41)
        Me.mskdate.Mask = "##-##-####"
        Me.mskdate.Name = "mskdate"
        Me.mskdate.Size = New System.Drawing.Size(79, 20)
        Me.mskdate.TabIndex = 28
        '
        'lbldate
        '
        Me.lbldate.Location = New System.Drawing.Point(401, 11)
        Me.lbldate.Mask = "##-##-####"
        Me.lbldate.Name = "lbldate"
        Me.lbldate.Size = New System.Drawing.Size(79, 20)
        Me.lbldate.TabIndex = 3
        '
        'view1
        '
        Me.view1.ActiveViewIndex = -1
        Me.view1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.view1.Cursor = System.Windows.Forms.Cursors.Default
        Me.view1.Location = New System.Drawing.Point(627, 618)
        Me.view1.Name = "view1"
        Me.view1.SelectionFormula = ""
        Me.view1.Size = New System.Drawing.Size(105, 56)
        Me.view1.TabIndex = 30
        Me.view1.ViewTimeSelectionFormula = ""
        '
        'chkprod
        '
        Me.chkprod.AutoSize = True
        Me.chkprod.Location = New System.Drawing.Point(12, 45)
        Me.chkprod.Name = "chkprod"
        Me.chkprod.Size = New System.Drawing.Size(77, 17)
        Me.chkprod.TabIndex = 31
        Me.chkprod.Text = "Production"
        Me.chkprod.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(15, 2)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(89, 15)
        Me.Label14.TabIndex = 243
        Me.Label14.Text = "Posting Period"
        '
        'cmbyear
        '
        Me.cmbyear.FormattingEnabled = True
        Me.cmbyear.Location = New System.Drawing.Point(12, 20)
        Me.cmbyear.Name = "cmbyear"
        Me.cmbyear.Size = New System.Drawing.Size(118, 21)
        Me.cmbyear.TabIndex = 0
        '
        'chkscheme
        '
        Me.chkscheme.AutoSize = True
        Me.chkscheme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkscheme.Location = New System.Drawing.Point(150, 41)
        Me.chkscheme.Name = "chkscheme"
        Me.chkscheme.Size = New System.Drawing.Size(71, 17)
        Me.chkscheme.TabIndex = 244
        Me.chkscheme.Text = "Scheme"
        Me.chkscheme.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1160, 150)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(58, 36)
        Me.Button1.TabIndex = 245
        Me.Button1.Text = "Print Invoice"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'chkinvprn
        '
        Me.chkinvprn.AutoSize = True
        Me.chkinvprn.Location = New System.Drawing.Point(4, 120)
        Me.chkinvprn.Name = "chkinvprn"
        Me.chkinvprn.Size = New System.Drawing.Size(85, 17)
        Me.chkinvprn.TabIndex = 246
        Me.chkinvprn.Text = "Invoice Print"
        Me.chkinvprn.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(1160, 191)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(58, 36)
        Me.Button2.TabIndex = 247
        Me.Button2.Text = "Bundle Barcode"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(1048, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 15)
        Me.Label4.TabIndex = 248
        Me.Label4.Text = "LPT Port"
        '
        'txtport
        '
        Me.txtport.Location = New System.Drawing.Point(1102, 13)
        Me.txtport.Name = "txtport"
        Me.txtport.Size = New System.Drawing.Size(89, 20)
        Me.txtport.TabIndex = 249
        Me.txtport.Text = "1"
        '
        'Frmpackage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1243, 698)
        Me.Controls.Add(Me.txtport)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.chkinvprn)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.chkscheme)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.cmbyear)
        Me.Controls.Add(Me.chkprod)
        Me.Controls.Add(Me.view1)
        Me.Controls.Add(Me.lbldate)
        Me.Controls.Add(Me.mskdate)
        Me.Controls.Add(Me.lblctot2)
        Me.Controls.Add(Me.cmbtype)
        Me.Controls.Add(Me.cmdprint)
        Me.Controls.Add(Me.cmdadd)
        Me.Controls.Add(Me.cmbwgt1)
        Me.Controls.Add(Me.cmbboxtype1)
        Me.Controls.Add(Me.cmdclear)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.lblctot)
        Me.Controls.Add(Me.lblptot)
        Me.Controls.Add(Me.cmdupdt)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbwgt)
        Me.Controls.Add(Me.cmbboxtype)
        Me.Controls.Add(Me.cmbboxno)
        Me.Controls.Add(Me.lbldocentry)
        Me.Controls.Add(Me.lblparty)
        Me.Controls.Add(Me.lblamt)
        Me.Controls.Add(Me.lbldate2)
        Me.Controls.Add(Me.txtno)
        Me.Controls.Add(Me.cmdrcd)
        Me.Controls.Add(Me.cmdsend)
        Me.Controls.Add(Me.flxp)
        Me.Controls.Add(Me.flxc)
        Me.Controls.Add(Me.flxh)
        Me.Name = "Frmpackage"
        Me.Text = "Frmpackage"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.flxp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents flxh As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents flxc As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents flxp As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents cmdsend As System.Windows.Forms.Button
    Friend WithEvents cmdrcd As System.Windows.Forms.Button
    Friend WithEvents txtno As System.Windows.Forms.TextBox
    Friend WithEvents lbldate2 As System.Windows.Forms.Label
    Friend WithEvents lblamt As System.Windows.Forms.Label
    Friend WithEvents lblparty As System.Windows.Forms.Label
    Friend WithEvents lbldocentry As System.Windows.Forms.Label
    Friend WithEvents cmbboxno As System.Windows.Forms.ComboBox
    Friend WithEvents cmbboxtype As System.Windows.Forms.ComboBox
    Friend WithEvents cmbwgt As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdupdt As System.Windows.Forms.Button
    Friend WithEvents lblptot As System.Windows.Forms.Label
    Friend WithEvents lblctot As System.Windows.Forms.Label
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdclear As System.Windows.Forms.Button
    Friend WithEvents cmbboxtype1 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbwgt1 As System.Windows.Forms.ComboBox
    Friend WithEvents cmdadd As System.Windows.Forms.Button
    Friend WithEvents cmdprint As System.Windows.Forms.Button
    Friend WithEvents cmbtype As System.Windows.Forms.ComboBox
    Friend WithEvents lblctot2 As System.Windows.Forms.Label
    Friend WithEvents mskdate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lbldate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents view1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents chkprod As System.Windows.Forms.CheckBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmbyear As System.Windows.Forms.ComboBox
    Friend WithEvents chkscheme As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chkinvprn As System.Windows.Forms.CheckBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtport As System.Windows.Forms.TextBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmmktbarcode
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Groupbox1 = New System.Windows.Forms.GroupBox()
        Me.chktwin = New System.Windows.Forms.CheckBox()
        Me.lblcardname = New System.Windows.Forms.Label()
        Me.lblcardcode = New System.Windows.Forms.Label()
        Me.Chksr = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbtype = New System.Windows.Forms.ComboBox()
        Me.cmbyr = New System.Windows.Forms.ComboBox()
        Me.lbldocentry = New System.Windows.Forms.Label()
        Me.txtmont = New System.Windows.Forms.TextBox()
        Me.txtdocnum = New System.Windows.Forms.TextBox()
        Me.txtyr = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.dtp = New System.Windows.Forms.DateTimePicker()
        Me.optsample = New System.Windows.Forms.RadioButton()
        Me.optsdraft = New System.Windows.Forms.RadioButton()
        Me.optdateord = New System.Windows.Forms.RadioButton()
        Me.OptSales = New System.Windows.Forms.RadioButton()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chksel = New System.Windows.Forms.CheckBox()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.dg = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Btnclear = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbprinter = New System.Windows.Forms.ComboBox()
        Me.txtno = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkliberty = New System.Windows.Forms.CheckBox()
        Me.Groupbox1.SuspendLayout()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Groupbox1
        '
        Me.Groupbox1.BackColor = System.Drawing.Color.LightCyan
        Me.Groupbox1.Controls.Add(Me.chkliberty)
        Me.Groupbox1.Controls.Add(Me.chktwin)
        Me.Groupbox1.Controls.Add(Me.lblcardname)
        Me.Groupbox1.Controls.Add(Me.lblcardcode)
        Me.Groupbox1.Controls.Add(Me.Chksr)
        Me.Groupbox1.Controls.Add(Me.Label1)
        Me.Groupbox1.Controls.Add(Me.cmbtype)
        Me.Groupbox1.Controls.Add(Me.cmbyr)
        Me.Groupbox1.Controls.Add(Me.lbldocentry)
        Me.Groupbox1.Controls.Add(Me.txtmont)
        Me.Groupbox1.Controls.Add(Me.txtdocnum)
        Me.Groupbox1.Controls.Add(Me.txtyr)
        Me.Groupbox1.Controls.Add(Me.Label12)
        Me.Groupbox1.Controls.Add(Me.dtp)
        Me.Groupbox1.Controls.Add(Me.optsample)
        Me.Groupbox1.Controls.Add(Me.optsdraft)
        Me.Groupbox1.Controls.Add(Me.optdateord)
        Me.Groupbox1.Controls.Add(Me.OptSales)
        Me.Groupbox1.Controls.Add(Me.Label11)
        Me.Groupbox1.Controls.Add(Me.Label8)
        Me.Groupbox1.Controls.Add(Me.chksel)
        Me.Groupbox1.Controls.Add(Me.BtnAdd)
        Me.Groupbox1.Controls.Add(Me.dg)
        Me.Groupbox1.Controls.Add(Me.Btnclear)
        Me.Groupbox1.Controls.Add(Me.BtnExit)
        Me.Groupbox1.Controls.Add(Me.BtnPrint)
        Me.Groupbox1.Controls.Add(Me.Label7)
        Me.Groupbox1.Controls.Add(Me.cmbprinter)
        Me.Groupbox1.Controls.Add(Me.txtno)
        Me.Groupbox1.Controls.Add(Me.Label6)
        Me.Groupbox1.Location = New System.Drawing.Point(6, 38)
        Me.Groupbox1.Name = "Groupbox1"
        Me.Groupbox1.Size = New System.Drawing.Size(1368, 651)
        Me.Groupbox1.TabIndex = 1
        Me.Groupbox1.TabStop = False
        '
        'chktwin
        '
        Me.chktwin.AutoSize = True
        Me.chktwin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chktwin.Location = New System.Drawing.Point(720, 23)
        Me.chktwin.Name = "chktwin"
        Me.chktwin.Size = New System.Drawing.Size(78, 17)
        Me.chktwin.TabIndex = 43
        Me.chktwin.Text = "Twin Box"
        Me.chktwin.UseVisualStyleBackColor = True
        '
        'lblcardname
        '
        Me.lblcardname.BackColor = System.Drawing.Color.Thistle
        Me.lblcardname.Location = New System.Drawing.Point(506, 90)
        Me.lblcardname.Name = "lblcardname"
        Me.lblcardname.Size = New System.Drawing.Size(430, 23)
        Me.lblcardname.TabIndex = 42
        '
        'lblcardcode
        '
        Me.lblcardcode.BackColor = System.Drawing.Color.Thistle
        Me.lblcardcode.Location = New System.Drawing.Point(558, 63)
        Me.lblcardcode.Name = "lblcardcode"
        Me.lblcardcode.Size = New System.Drawing.Size(108, 23)
        Me.lblcardcode.TabIndex = 41
        '
        'Chksr
        '
        Me.Chksr.AutoSize = True
        Me.Chksr.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chksr.Location = New System.Drawing.Point(984, 19)
        Me.Chksr.Name = "Chksr"
        Me.Chksr.Size = New System.Drawing.Size(119, 17)
        Me.Chksr.TabIndex = 40
        Me.Chksr.Text = "Excel-Showroom"
        Me.Chksr.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(433, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "Barcode Type"
        Me.Label1.Visible = False
        '
        'cmbtype
        '
        Me.cmbtype.FormattingEnabled = True
        Me.cmbtype.Location = New System.Drawing.Point(525, 20)
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.Size = New System.Drawing.Size(170, 21)
        Me.cmbtype.TabIndex = 38
        '
        'cmbyr
        '
        Me.cmbyr.FormattingEnabled = True
        Me.cmbyr.Location = New System.Drawing.Point(98, 58)
        Me.cmbyr.Name = "cmbyr"
        Me.cmbyr.Size = New System.Drawing.Size(106, 21)
        Me.cmbyr.TabIndex = 37
        '
        'lbldocentry
        '
        Me.lbldocentry.AutoSize = True
        Me.lbldocentry.Location = New System.Drawing.Point(457, 95)
        Me.lbldocentry.Name = "lbldocentry"
        Me.lbldocentry.Size = New System.Drawing.Size(13, 13)
        Me.lbldocentry.TabIndex = 36
        Me.lbldocentry.Text = "0"
        '
        'txtmont
        '
        Me.txtmont.Location = New System.Drawing.Point(395, 60)
        Me.txtmont.Name = "txtmont"
        Me.txtmont.Size = New System.Drawing.Size(64, 20)
        Me.txtmont.TabIndex = 35
        '
        'txtdocnum
        '
        Me.txtdocnum.Location = New System.Drawing.Point(271, 60)
        Me.txtdocnum.Name = "txtdocnum"
        Me.txtdocnum.Size = New System.Drawing.Size(118, 20)
        Me.txtdocnum.TabIndex = 33
        '
        'txtyr
        '
        Me.txtyr.Enabled = False
        Me.txtyr.Location = New System.Drawing.Point(468, 60)
        Me.txtyr.Name = "txtyr"
        Me.txtyr.Size = New System.Drawing.Size(62, 20)
        Me.txtyr.TabIndex = 32
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(61, 63)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(33, 13)
        Me.Label12.TabIndex = 31
        Me.Label12.Text = "Year"
        '
        'dtp
        '
        Me.dtp.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp.Location = New System.Drawing.Point(18, 47)
        Me.dtp.Name = "dtp"
        Me.dtp.Size = New System.Drawing.Size(19, 20)
        Me.dtp.TabIndex = 30
        Me.dtp.Visible = False
        '
        'optsample
        '
        Me.optsample.AutoSize = True
        Me.optsample.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optsample.Location = New System.Drawing.Point(325, 19)
        Me.optsample.Name = "optsample"
        Me.optsample.Size = New System.Drawing.Size(74, 17)
        Me.optsample.TabIndex = 29
        Me.optsample.TabStop = True
        Me.optsample.Text = "SAMPLE"
        Me.optsample.UseVisualStyleBackColor = True
        '
        'optsdraft
        '
        Me.optsdraft.AutoSize = True
        Me.optsdraft.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optsdraft.Location = New System.Drawing.Point(210, 19)
        Me.optsdraft.Name = "optsdraft"
        Me.optsdraft.Size = New System.Drawing.Size(109, 17)
        Me.optsdraft.TabIndex = 28
        Me.optsdraft.TabStop = True
        Me.optsdraft.Text = "SALES DRAFT"
        Me.optsdraft.UseVisualStyleBackColor = True
        '
        'optdateord
        '
        Me.optdateord.AutoSize = True
        Me.optdateord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optdateord.Location = New System.Drawing.Point(98, 19)
        Me.optdateord.Name = "optdateord"
        Me.optdateord.Size = New System.Drawing.Size(106, 17)
        Me.optdateord.TabIndex = 27
        Me.optdateord.TabStop = True
        Me.optdateord.Text = "DATE ORDER"
        Me.optdateord.UseVisualStyleBackColor = True
        '
        'OptSales
        '
        Me.OptSales.AutoSize = True
        Me.OptSales.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptSales.Location = New System.Drawing.Point(30, 19)
        Me.OptSales.Name = "OptSales"
        Me.OptSales.Size = New System.Drawing.Size(64, 17)
        Me.OptSales.TabIndex = 26
        Me.OptSales.TabStop = True
        Me.OptSales.Text = "SALES"
        Me.OptSales.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(212, 63)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(55, 13)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "DocNum"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Maroon
        Me.Label8.Location = New System.Drawing.Point(113, 572)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(91, 13)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "F7--> Add Row"
        '
        'chksel
        '
        Me.chksel.AutoSize = True
        Me.chksel.Location = New System.Drawing.Point(16, 140)
        Me.chksel.Name = "chksel"
        Me.chksel.Size = New System.Drawing.Size(15, 14)
        Me.chksel.TabIndex = 20
        Me.chksel.UseVisualStyleBackColor = True
        '
        'BtnAdd
        '
        Me.BtnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAdd.Location = New System.Drawing.Point(720, 60)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(75, 23)
        Me.BtnAdd.TabIndex = 19
        Me.BtnAdd.Text = "Load"
        Me.BtnAdd.UseVisualStyleBackColor = True
        '
        'dg
        '
        Me.dg.AllowUserToAddRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dg.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8, Me.Column13, Me.Column9, Me.Column10, Me.Column14, Me.Column11, Me.Column15, Me.Column16})
        Me.dg.Location = New System.Drawing.Point(13, 128)
        Me.dg.Name = "dg"
        Me.dg.RowHeadersVisible = False
        Me.dg.Size = New System.Drawing.Size(1330, 421)
        Me.dg.TabIndex = 18
        '
        'Column1
        '
        Me.Column1.HeaderText = "Sel"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 50
        '
        'Column2
        '
        Me.Column2.HeaderText = "DocNum"
        Me.Column2.Name = "Column2"
        '
        'Column3
        '
        Me.Column3.HeaderText = "Item Name"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 200
        '
        'Column4
        '
        Me.Column4.HeaderText = "Style"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 60
        '
        'Column5
        '
        Me.Column5.HeaderText = "Size"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 60
        '
        'Column6
        '
        Me.Column6.HeaderText = "Color"
        Me.Column6.Name = "Column6"
        Me.Column6.Width = 60
        '
        'Column7
        '
        Me.Column7.HeaderText = "Item Group"
        Me.Column7.Name = "Column7"
        Me.Column7.Width = 120
        '
        'Column8
        '
        Me.Column8.HeaderText = "Mfd"
        Me.Column8.Name = "Column8"
        '
        'Column13
        '
        Me.Column13.HeaderText = "Cstype"
        Me.Column13.Name = "Column13"
        Me.Column13.Width = 50
        '
        'Column9
        '
        Me.Column9.HeaderText = "Barcode"
        Me.Column9.Name = "Column9"
        Me.Column9.Width = 80
        '
        'Column10
        '
        Me.Column10.HeaderText = "TxBarcode"
        Me.Column10.Name = "Column10"
        '
        'Column14
        '
        Me.Column14.HeaderText = "BoxMRP"
        Me.Column14.Name = "Column14"
        Me.Column14.Width = 70
        '
        'Column11
        '
        Me.Column11.HeaderText = "MRP"
        Me.Column11.Name = "Column11"
        '
        'Column15
        '
        Me.Column15.HeaderText = "Box Qty"
        Me.Column15.Name = "Column15"
        Me.Column15.Width = 70
        '
        'Column16
        '
        Me.Column16.HeaderText = "Quantity"
        Me.Column16.Name = "Column16"
        Me.Column16.Width = 70
        '
        'Btnclear
        '
        Me.Btnclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btnclear.Location = New System.Drawing.Point(331, 566)
        Me.Btnclear.Name = "Btnclear"
        Me.Btnclear.Size = New System.Drawing.Size(75, 23)
        Me.Btnclear.TabIndex = 17
        Me.Btnclear.Text = "Clear"
        Me.Btnclear.UseVisualStyleBackColor = True
        '
        'BtnExit
        '
        Me.BtnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExit.Location = New System.Drawing.Point(433, 566)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(75, 23)
        Me.BtnExit.TabIndex = 16
        Me.BtnExit.Text = "Exit"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'BtnPrint
        '
        Me.BtnPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrint.Location = New System.Drawing.Point(222, 566)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(75, 23)
        Me.BtnPrint.TabIndex = 15
        Me.BtnPrint.Text = "Print"
        Me.BtnPrint.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(66, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Printer"
        '
        'cmbprinter
        '
        Me.cmbprinter.FormattingEnabled = True
        Me.cmbprinter.Location = New System.Drawing.Point(111, 94)
        Me.cmbprinter.Name = "cmbprinter"
        Me.cmbprinter.Size = New System.Drawing.Size(305, 21)
        Me.cmbprinter.TabIndex = 12
        '
        'txtno
        '
        Me.txtno.Location = New System.Drawing.Point(1061, 50)
        Me.txtno.Name = "txtno"
        Me.txtno.Size = New System.Drawing.Size(85, 20)
        Me.txtno.TabIndex = 11
        Me.txtno.Text = "1"
        Me.txtno.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(1029, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(26, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Qty"
        Me.Label6.Visible = False
        '
        'chkliberty
        '
        Me.chkliberty.AutoSize = True
        Me.chkliberty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkliberty.Location = New System.Drawing.Point(804, 24)
        Me.chkliberty.Name = "chkliberty"
        Me.chkliberty.Size = New System.Drawing.Size(87, 17)
        Me.chkliberty.TabIndex = 44
        Me.chkliberty.Text = "Liberty Cut"
        Me.chkliberty.UseVisualStyleBackColor = True
        '
        'Frmmktbarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1431, 701)
        Me.Controls.Add(Me.Groupbox1)
        Me.Name = "Frmmktbarcode"
        Me.Text = "Frmmktbarcode"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Groupbox1.ResumeLayout(False)
        Me.Groupbox1.PerformLayout()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Groupbox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtp As System.Windows.Forms.DateTimePicker
    Friend WithEvents optsample As System.Windows.Forms.RadioButton
    Friend WithEvents optsdraft As System.Windows.Forms.RadioButton
    Friend WithEvents optdateord As System.Windows.Forms.RadioButton
    Friend WithEvents OptSales As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chksel As System.Windows.Forms.CheckBox
    Friend WithEvents BtnAdd As System.Windows.Forms.Button
    Friend WithEvents dg As System.Windows.Forms.DataGridView
    Friend WithEvents Btnclear As System.Windows.Forms.Button
    Friend WithEvents BtnExit As System.Windows.Forms.Button
    Friend WithEvents BtnPrint As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbprinter As System.Windows.Forms.ComboBox
    Friend WithEvents txtno As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtdocnum As System.Windows.Forms.TextBox
    Friend WithEvents txtyr As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtmont As System.Windows.Forms.TextBox
    Friend WithEvents lbldocentry As System.Windows.Forms.Label
    Friend WithEvents cmbyr As System.Windows.Forms.ComboBox
    Friend WithEvents cmbtype As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Chksr As System.Windows.Forms.CheckBox
    Friend WithEvents lblcardname As System.Windows.Forms.Label
    Friend WithEvents lblcardcode As System.Windows.Forms.Label
    Friend WithEvents chktwin As System.Windows.Forms.CheckBox
    Friend WithEvents chkliberty As System.Windows.Forms.CheckBox
End Class

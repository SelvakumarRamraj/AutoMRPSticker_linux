<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frminvprn
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.dg = New System.Windows.Forms.DataGridView
        Me.Column7 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmdDisp = New System.Windows.Forms.Button
        Me.cmdprint = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        Me.mskdatefr = New System.Windows.Forms.MaskedTextBox
        Me.mskdateto = New System.Windows.Forms.MaskedTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbbrand = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtno = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.optforward = New System.Windows.Forms.RadioButton
        Me.optLorry = New System.Windows.Forms.RadioButton
        Me.optdel = New System.Windows.Forms.RadioButton
        Me.optinv = New System.Windows.Forms.RadioButton
        Me.view1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtrepcode = New System.Windows.Forms.TextBox
        Me.cmdsaverep = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtreppath = New System.Windows.Forms.TextBox
        Me.txtfile = New System.Windows.Forms.TextBox
        Me.butsel = New System.Windows.Forms.Button
        Me.fd = New System.Windows.Forms.OpenFileDialog
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dg
        '
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column7, Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column8, Me.Column9})
        Me.dg.Location = New System.Drawing.Point(3, 98)
        Me.dg.Name = "dg"
        Me.dg.RowHeadersVisible = False
        Me.dg.Size = New System.Drawing.Size(1069, 431)
        Me.dg.TabIndex = 0
        '
        'Column7
        '
        Me.Column7.HeaderText = "Sel"
        Me.Column7.Name = "Column7"
        Me.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column7.Width = 50
        '
        'Column1
        '
        Me.Column1.HeaderText = "Docnum"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 75
        '
        'Column2
        '
        Me.Column2.HeaderText = "Docdate"
        Me.Column2.Name = "Column2"
        '
        'Column3
        '
        Me.Column3.HeaderText = "Docentry"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 75
        '
        'Column4
        '
        Me.Column4.HeaderText = "Cardcode"
        Me.Column4.Name = "Column4"
        '
        'Column5
        '
        Me.Column5.HeaderText = "Cardname"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 325
        '
        'Column6
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column6.DefaultCellStyle = DataGridViewCellStyle1
        Me.Column6.HeaderText = "Doctotal"
        Me.Column6.Name = "Column6"
        '
        'Column8
        '
        Me.Column8.HeaderText = "Brand Name"
        Me.Column8.Name = "Column8"
        '
        'Column9
        '
        Me.Column9.HeaderText = "Print"
        Me.Column9.Name = "Column9"
        '
        'cmdDisp
        '
        Me.cmdDisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDisp.Location = New System.Drawing.Point(717, 66)
        Me.cmdDisp.Name = "cmdDisp"
        Me.cmdDisp.Size = New System.Drawing.Size(64, 23)
        Me.cmdDisp.TabIndex = 1
        Me.cmdDisp.Text = "Load"
        Me.cmdDisp.UseVisualStyleBackColor = True
        '
        'cmdprint
        '
        Me.cmdprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdprint.Location = New System.Drawing.Point(787, 66)
        Me.cmdprint.Name = "cmdprint"
        Me.cmdprint.Size = New System.Drawing.Size(64, 23)
        Me.cmdprint.TabIndex = 2
        Me.cmdprint.Text = "Print"
        Me.cmdprint.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.Location = New System.Drawing.Point(857, 66)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(64, 23)
        Me.cmdexit.TabIndex = 3
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'mskdatefr
        '
        Me.mskdatefr.Location = New System.Drawing.Point(252, 43)
        Me.mskdatefr.Mask = "##-##-####"
        Me.mskdatefr.Name = "mskdatefr"
        Me.mskdatefr.Size = New System.Drawing.Size(72, 20)
        Me.mskdatefr.TabIndex = 4
        '
        'mskdateto
        '
        Me.mskdateto.Location = New System.Drawing.Point(362, 43)
        Me.mskdateto.Mask = "##-##-####"
        Me.mskdateto.Name = "mskdateto"
        Me.mskdateto.Size = New System.Drawing.Size(72, 20)
        Me.mskdateto.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(179, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Date From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(330, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(22, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "To"
        '
        'cmbbrand
        '
        Me.cmbbrand.FormattingEnabled = True
        Me.cmbbrand.Location = New System.Drawing.Point(532, 43)
        Me.cmbbrand.Name = "cmbbrand"
        Me.cmbbrand.Size = New System.Drawing.Size(173, 21)
        Me.cmbbrand.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(440, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Brand Name"
        '
        'txtno
        '
        Me.txtno.Location = New System.Drawing.Point(271, 72)
        Me.txtno.Name = "txtno"
        Me.txtno.Size = New System.Drawing.Size(100, 20)
        Me.txtno.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(167, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Search Docnum"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.optforward)
        Me.Panel1.Controls.Add(Me.optLorry)
        Me.Panel1.Controls.Add(Me.optdel)
        Me.Panel1.Controls.Add(Me.optinv)
        Me.Panel1.Location = New System.Drawing.Point(165, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(750, 33)
        Me.Panel1.TabIndex = 12
        '
        'optforward
        '
        Me.optforward.AutoSize = True
        Me.optforward.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optforward.Location = New System.Drawing.Point(278, 8)
        Me.optforward.Name = "optforward"
        Me.optforward.Size = New System.Drawing.Size(87, 17)
        Me.optforward.TabIndex = 3
        Me.optforward.TabStop = True
        Me.optforward.Text = "Forwarding"
        Me.optforward.UseVisualStyleBackColor = True
        '
        'optLorry
        '
        Me.optLorry.AutoSize = True
        Me.optLorry.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optLorry.Location = New System.Drawing.Point(168, 9)
        Me.optLorry.Name = "optLorry"
        Me.optLorry.Size = New System.Drawing.Size(85, 17)
        Me.optLorry.TabIndex = 2
        Me.optLorry.TabStop = True
        Me.optLorry.Text = "Lorry Copy"
        Me.optLorry.UseVisualStyleBackColor = True
        '
        'optdel
        '
        Me.optdel.AutoSize = True
        Me.optdel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optdel.Location = New System.Drawing.Point(88, 8)
        Me.optdel.Name = "optdel"
        Me.optdel.Size = New System.Drawing.Size(71, 17)
        Me.optdel.TabIndex = 1
        Me.optdel.TabStop = True
        Me.optdel.Text = "Delivery"
        Me.optdel.UseVisualStyleBackColor = True
        '
        'optinv
        '
        Me.optinv.AutoSize = True
        Me.optinv.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optinv.Location = New System.Drawing.Point(8, 9)
        Me.optinv.Name = "optinv"
        Me.optinv.Size = New System.Drawing.Size(67, 17)
        Me.optinv.TabIndex = 0
        Me.optinv.TabStop = True
        Me.optinv.Text = "Invoice"
        Me.optinv.UseVisualStyleBackColor = True
        '
        'view1
        '
        Me.view1.ActiveViewIndex = -1
        Me.view1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.view1.Cursor = System.Windows.Forms.Cursors.Default
        Me.view1.Location = New System.Drawing.Point(3, 535)
        Me.view1.Name = "view1"
        Me.view1.SelectionFormula = ""
        Me.view1.Size = New System.Drawing.Size(105, 56)
        Me.view1.TabIndex = 31
        Me.view1.ViewTimeSelectionFormula = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(278, 552)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 13)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Report Code"
        '
        'txtrepcode
        '
        Me.txtrepcode.Location = New System.Drawing.Point(362, 549)
        Me.txtrepcode.Name = "txtrepcode"
        Me.txtrepcode.Size = New System.Drawing.Size(142, 20)
        Me.txtrepcode.TabIndex = 32
        '
        'cmdsaverep
        '
        Me.cmdsaverep.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsaverep.Location = New System.Drawing.Point(508, 549)
        Me.cmdsaverep.Name = "cmdsaverep"
        Me.cmdsaverep.Size = New System.Drawing.Size(110, 23)
        Me.cmdsaverep.TabIndex = 34
        Me.cmdsaverep.Text = "SaveReport"
        Me.cmdsaverep.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(239, 578)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(117, 13)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "Save Rep.File Path"
        '
        'txtreppath
        '
        Me.txtreppath.Location = New System.Drawing.Point(362, 575)
        Me.txtreppath.Name = "txtreppath"
        Me.txtreppath.Size = New System.Drawing.Size(142, 20)
        Me.txtreppath.TabIndex = 35
        '
        'txtfile
        '
        Me.txtfile.Location = New System.Drawing.Point(648, 552)
        Me.txtfile.Name = "txtfile"
        Me.txtfile.Size = New System.Drawing.Size(316, 20)
        Me.txtfile.TabIndex = 37
        '
        'butsel
        '
        Me.butsel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butsel.Location = New System.Drawing.Point(968, 550)
        Me.butsel.Name = "butsel"
        Me.butsel.Size = New System.Drawing.Size(58, 23)
        Me.butsel.TabIndex = 38
        Me.butsel.Text = "Browse"
        Me.butsel.UseVisualStyleBackColor = True
        '
        'fd
        '
        Me.fd.FileName = "OpenFileDialog1"
        '
        'Frminvprn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1084, 705)
        Me.Controls.Add(Me.butsel)
        Me.Controls.Add(Me.txtfile)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtreppath)
        Me.Controls.Add(Me.cmdsaverep)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtrepcode)
        Me.Controls.Add(Me.view1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtno)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbbrand)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.mskdateto)
        Me.Controls.Add(Me.mskdatefr)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdprint)
        Me.Controls.Add(Me.cmdDisp)
        Me.Controls.Add(Me.dg)
        Me.Name = "Frminvprn"
        Me.Text = "Frminvprn"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dg As System.Windows.Forms.DataGridView
    Friend WithEvents cmdDisp As System.Windows.Forms.Button
    Friend WithEvents cmdprint As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents mskdatefr As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskdateto As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbbrand As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtno As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents optinv As System.Windows.Forms.RadioButton
    Friend WithEvents optLorry As System.Windows.Forms.RadioButton
    Friend WithEvents optdel As System.Windows.Forms.RadioButton
    Friend WithEvents optforward As System.Windows.Forms.RadioButton
    Friend WithEvents view1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtrepcode As System.Windows.Forms.TextBox
    Friend WithEvents cmdsaverep As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtreppath As System.Windows.Forms.TextBox
    Friend WithEvents txtfile As System.Windows.Forms.TextBox
    Friend WithEvents butsel As System.Windows.Forms.Button
    Friend WithEvents fd As System.Windows.Forms.OpenFileDialog
End Class

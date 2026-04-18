<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProddespatchprn
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnDisp = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblTotqty = New System.Windows.Forms.Label()
        Me.dtpfr = New System.Windows.Forms.DateTimePicker()
        Me.dtpto = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dg = New System.Windows.Forms.DataGridView()
        Me.Sel = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Docnum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Docentry = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Docdate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.opercode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Totqty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dg1 = New System.Windows.Forms.DataGridView()
        Me.BtnLoad = New System.Windows.Forms.Button()
        Me.btnexit = New System.Windows.Forms.Button()
        Me.btnprint = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chksel = New System.Windows.Forms.CheckBox()
        Me.lblmqty = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblselqty = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbparty = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dg1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnDisp
        '
        Me.BtnDisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDisp.Location = New System.Drawing.Point(360, 14)
        Me.BtnDisp.Name = "BtnDisp"
        Me.BtnDisp.Size = New System.Drawing.Size(75, 23)
        Me.BtnDisp.TabIndex = 0
        Me.BtnDisp.Text = "Display"
        Me.BtnDisp.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(39, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Date From"
        '
        'LblTotqty
        '
        Me.LblTotqty.AutoSize = True
        Me.LblTotqty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotqty.Location = New System.Drawing.Point(835, 655)
        Me.LblTotqty.Name = "LblTotqty"
        Me.LblTotqty.Size = New System.Drawing.Size(15, 15)
        Me.LblTotqty.TabIndex = 2
        Me.LblTotqty.Text = "0"
        '
        'dtpfr
        '
        Me.dtpfr.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfr.Location = New System.Drawing.Point(110, 15)
        Me.dtpfr.Name = "dtpfr"
        Me.dtpfr.Size = New System.Drawing.Size(103, 20)
        Me.dtpfr.TabIndex = 3
        '
        'dtpto
        '
        Me.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpto.Location = New System.Drawing.Point(247, 15)
        Me.dtpto.Name = "dtpto"
        Me.dtpto.Size = New System.Drawing.Size(103, 20)
        Me.dtpto.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(219, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "To"
        '
        'dg
        '
        Me.dg.AllowUserToAddRows = False
        Me.dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dg.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Sel, Me.Docnum, Me.Docentry, Me.Docdate, Me.opercode, Me.Totqty})
        Me.dg.Location = New System.Drawing.Point(12, 43)
        Me.dg.Name = "dg"
        Me.dg.RowHeadersVisible = False
        Me.dg.Size = New System.Drawing.Size(513, 598)
        Me.dg.TabIndex = 6
        '
        'Sel
        '
        Me.Sel.HeaderText = "Sel"
        Me.Sel.Name = "Sel"
        '
        'Docnum
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Docnum.DefaultCellStyle = DataGridViewCellStyle2
        Me.Docnum.HeaderText = "Docnum"
        Me.Docnum.Name = "Docnum"
        Me.Docnum.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Docnum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Docentry
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Docentry.DefaultCellStyle = DataGridViewCellStyle3
        Me.Docentry.HeaderText = "Docentry"
        Me.Docentry.Name = "Docentry"
        Me.Docentry.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Docentry.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Docdate
        '
        Me.Docdate.HeaderText = "Date"
        Me.Docdate.Name = "Docdate"
        Me.Docdate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Docdate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'opercode
        '
        Me.opercode.HeaderText = "Process"
        Me.opercode.Name = "opercode"
        '
        'Totqty
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Totqty.DefaultCellStyle = DataGridViewCellStyle4
        Me.Totqty.HeaderText = "Total Qty"
        Me.Totqty.Name = "Totqty"
        Me.Totqty.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Totqty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'dg1
        '
        Me.dg1.AllowUserToAddRows = False
        Me.dg1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dg1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg1.Location = New System.Drawing.Point(616, 45)
        Me.dg1.Name = "dg1"
        Me.dg1.RowHeadersVisible = False
        Me.dg1.Size = New System.Drawing.Size(711, 584)
        Me.dg1.TabIndex = 7
        '
        'BtnLoad
        '
        Me.BtnLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnLoad.Location = New System.Drawing.Point(531, 43)
        Me.BtnLoad.Name = "BtnLoad"
        Me.BtnLoad.Size = New System.Drawing.Size(75, 23)
        Me.BtnLoad.TabIndex = 8
        Me.BtnLoad.Text = "Submit"
        Me.BtnLoad.UseVisualStyleBackColor = True
        '
        'btnexit
        '
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.Location = New System.Drawing.Point(1116, 15)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(75, 23)
        Me.btnexit.TabIndex = 9
        Me.btnexit.Text = "Exit"
        Me.btnexit.UseVisualStyleBackColor = True
        '
        'btnprint
        '
        Me.btnprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.Location = New System.Drawing.Point(1019, 18)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(75, 23)
        Me.btnprint.TabIndex = 10
        Me.btnprint.Text = "Print"
        Me.btnprint.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(721, 655)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 15)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Total Qty"
        '
        'chksel
        '
        Me.chksel.AutoSize = True
        Me.chksel.Location = New System.Drawing.Point(21, 47)
        Me.chksel.Name = "chksel"
        Me.chksel.Size = New System.Drawing.Size(15, 14)
        Me.chksel.TabIndex = 12
        Me.chksel.UseVisualStyleBackColor = True
        '
        'lblmqty
        '
        Me.lblmqty.AutoSize = True
        Me.lblmqty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmqty.Location = New System.Drawing.Point(501, 655)
        Me.lblmqty.Name = "lblmqty"
        Me.lblmqty.Size = New System.Drawing.Size(15, 15)
        Me.lblmqty.TabIndex = 13
        Me.lblmqty.Text = "0"
        Me.lblmqty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(403, 655)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 15)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Total Qty"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(116, 655)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 15)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Total Selected Qty"
        '
        'lblselqty
        '
        Me.lblselqty.AutoSize = True
        Me.lblselqty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblselqty.Location = New System.Drawing.Point(276, 655)
        Me.lblselqty.Name = "lblselqty"
        Me.lblselqty.Size = New System.Drawing.Size(15, 15)
        Me.lblselqty.TabIndex = 15
        Me.lblselqty.Text = "0"
        Me.lblselqty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn1.HeaderText = "Docnum"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 85
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewTextBoxColumn2.HeaderText = "Docentry"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn2.Width = 85
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Date"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn3.Width = 85
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Process"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 85
        '
        'DataGridViewTextBoxColumn5
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewTextBoxColumn5.HeaderText = "Total Qty"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn5.Width = 85
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(1005, 655)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 15)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Total Selected Qty"
        Me.Label6.Visible = False
        '
        'cmbparty
        '
        Me.cmbparty.FormattingEnabled = True
        Me.cmbparty.Location = New System.Drawing.Point(616, 10)
        Me.cmbparty.Name = "cmbparty"
        Me.cmbparty.Size = New System.Drawing.Size(372, 21)
        Me.cmbparty.TabIndex = 18
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(538, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 13)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Party Name"
        '
        'FrmProddespatchprn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1363, 703)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmbparty)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblselqty)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblmqty)
        Me.Controls.Add(Me.chksel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.BtnLoad)
        Me.Controls.Add(Me.dg1)
        Me.Controls.Add(Me.dg)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtpto)
        Me.Controls.Add(Me.dtpfr)
        Me.Controls.Add(Me.LblTotqty)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnDisp)
        Me.Name = "FrmProddespatchprn"
        Me.Text = "FrmProddespatchprn"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dg1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnDisp As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LblTotqty As System.Windows.Forms.Label
    Friend WithEvents dtpfr As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dg As System.Windows.Forms.DataGridView
    Friend WithEvents dg1 As System.Windows.Forms.DataGridView
    Friend WithEvents Sel As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Docnum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Docentry As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Docdate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents opercode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Totqty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BtnLoad As System.Windows.Forms.Button
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnprint As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chksel As System.Windows.Forms.CheckBox
    Friend WithEvents lblmqty As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblselqty As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbparty As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class

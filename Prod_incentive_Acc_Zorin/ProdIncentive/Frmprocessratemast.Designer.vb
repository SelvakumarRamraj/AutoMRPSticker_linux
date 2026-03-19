<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmprocessratemast
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.dg = New System.Windows.Forms.DataGridView
        Me.nempno = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.nemp_id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.vname = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cdepartment = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.csno = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Rate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.butadd = New System.Windows.Forms.Button
        Me.butdisp = New System.Windows.Forms.Button
        Me.butsave = New System.Windows.Forms.Button
        Me.butexit = New System.Windows.Forms.Button
        Me.dg1 = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmbprocname = New System.Windows.Forms.ComboBox
        Me.txtrate = New System.Windows.Forms.TextBox
        Me.butadd2 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.butupdt = New System.Windows.Forms.Button
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dg1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.dg.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.nempno, Me.nemp_id, Me.vname, Me.cdepartment, Me.csno, Me.Rate})
        Me.dg.Location = New System.Drawing.Point(12, 60)
        Me.dg.Name = "dg"
        Me.dg.RowHeadersVisible = False
        Me.dg.Size = New System.Drawing.Size(625, 417)
        Me.dg.TabIndex = 0
        '
        'nempno
        '
        Me.nempno.HeaderText = "Emp.No"
        Me.nempno.Name = "nempno"
        Me.nempno.ReadOnly = True
        Me.nempno.Width = 70
        '
        'nemp_id
        '
        Me.nemp_id.HeaderText = "Emp_id"
        Me.nemp_id.Name = "nemp_id"
        Me.nemp_id.ReadOnly = True
        Me.nemp_id.Width = 70
        '
        'vname
        '
        Me.vname.HeaderText = "Emplyee Name"
        Me.vname.Name = "vname"
        Me.vname.ReadOnly = True
        Me.vname.Width = 150
        '
        'cdepartment
        '
        Me.cdepartment.HeaderText = "Process Name"
        Me.cdepartment.Name = "cdepartment"
        Me.cdepartment.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cdepartment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.cdepartment.Width = 200
        '
        'csno
        '
        Me.csno.HeaderText = "Csno"
        Me.csno.Name = "csno"
        Me.csno.ReadOnly = True
        Me.csno.Width = 50
        '
        'Rate
        '
        Me.Rate.HeaderText = "Rate"
        Me.Rate.Name = "Rate"
        Me.Rate.Width = 70
        '
        'butadd
        '
        Me.butadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butadd.Location = New System.Drawing.Point(143, 31)
        Me.butadd.Name = "butadd"
        Me.butadd.Size = New System.Drawing.Size(58, 23)
        Me.butadd.TabIndex = 1
        Me.butadd.Text = "Add"
        Me.butadd.UseVisualStyleBackColor = True
        '
        'butdisp
        '
        Me.butdisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butdisp.Location = New System.Drawing.Point(79, 31)
        Me.butdisp.Name = "butdisp"
        Me.butdisp.Size = New System.Drawing.Size(58, 23)
        Me.butdisp.TabIndex = 2
        Me.butdisp.Text = "Load"
        Me.butdisp.UseVisualStyleBackColor = True
        '
        'butsave
        '
        Me.butsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butsave.Location = New System.Drawing.Point(207, 31)
        Me.butsave.Name = "butsave"
        Me.butsave.Size = New System.Drawing.Size(58, 23)
        Me.butsave.TabIndex = 3
        Me.butsave.Text = "Save"
        Me.butsave.UseVisualStyleBackColor = True
        '
        'butexit
        '
        Me.butexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butexit.Location = New System.Drawing.Point(271, 31)
        Me.butexit.Name = "butexit"
        Me.butexit.Size = New System.Drawing.Size(58, 23)
        Me.butexit.TabIndex = 4
        Me.butexit.Text = "Exit"
        Me.butexit.UseVisualStyleBackColor = True
        '
        'dg1
        '
        Me.dg1.AllowUserToAddRows = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dg1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3})
        Me.dg1.Location = New System.Drawing.Point(643, 60)
        Me.dg1.Name = "dg1"
        Me.dg1.RowHeadersVisible = False
        Me.dg1.Size = New System.Drawing.Size(406, 417)
        Me.dg1.TabIndex = 5
        '
        'Column1
        '
        Me.Column1.HeaderText = "Process Name"
        Me.Column1.Name = "Column1"
        Me.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column1.Width = 250
        '
        'Column2
        '
        Me.Column2.HeaderText = "CNo"
        Me.Column2.Name = "Column2"
        Me.Column2.Width = 50
        '
        'Column3
        '
        Me.Column3.HeaderText = "Rate"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 70
        '
        'cmbprocname
        '
        Me.cmbprocname.FormattingEnabled = True
        Me.cmbprocname.Location = New System.Drawing.Point(643, 31)
        Me.cmbprocname.Name = "cmbprocname"
        Me.cmbprocname.Size = New System.Drawing.Size(233, 21)
        Me.cmbprocname.TabIndex = 6
        '
        'txtrate
        '
        Me.txtrate.Location = New System.Drawing.Point(882, 32)
        Me.txtrate.Name = "txtrate"
        Me.txtrate.Size = New System.Drawing.Size(100, 20)
        Me.txtrate.TabIndex = 7
        '
        'butadd2
        '
        Me.butadd2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butadd2.Location = New System.Drawing.Point(991, 31)
        Me.butadd2.Name = "butadd2"
        Me.butadd2.Size = New System.Drawing.Size(58, 23)
        Me.butadd2.TabIndex = 8
        Me.butadd2.Text = "Add"
        Me.butadd2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(692, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Process Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(911, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Rate"
        '
        'butupdt
        '
        Me.butupdt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butupdt.Location = New System.Drawing.Point(991, 483)
        Me.butupdt.Name = "butupdt"
        Me.butupdt.Size = New System.Drawing.Size(58, 23)
        Me.butupdt.TabIndex = 11
        Me.butupdt.Text = "Update"
        Me.butupdt.UseVisualStyleBackColor = True
        '
        'Frmprocessratemast
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1061, 731)
        Me.Controls.Add(Me.butupdt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.butadd2)
        Me.Controls.Add(Me.txtrate)
        Me.Controls.Add(Me.cmbprocname)
        Me.Controls.Add(Me.dg1)
        Me.Controls.Add(Me.butexit)
        Me.Controls.Add(Me.butsave)
        Me.Controls.Add(Me.butdisp)
        Me.Controls.Add(Me.butadd)
        Me.Controls.Add(Me.dg)
        Me.Name = "Frmprocessratemast"
        Me.Text = "Frmprocessratemast"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dg1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dg As System.Windows.Forms.DataGridView
    Friend WithEvents butadd As System.Windows.Forms.Button
    Friend WithEvents butdisp As System.Windows.Forms.Button
    Friend WithEvents butsave As System.Windows.Forms.Button
    Friend WithEvents butexit As System.Windows.Forms.Button
    Friend WithEvents nempno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents nemp_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents vname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cdepartment As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents csno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Rate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dg1 As System.Windows.Forms.DataGridView
    Friend WithEvents cmbprocname As System.Windows.Forms.ComboBox
    Friend WithEvents txtrate As System.Windows.Forms.TextBox
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents butadd2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents butupdt As System.Windows.Forms.Button
End Class

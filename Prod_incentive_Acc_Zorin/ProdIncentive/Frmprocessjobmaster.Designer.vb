<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmprocessjobmaster
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.butexit = New System.Windows.Forms.Button
        Me.butcancel = New System.Windows.Forms.Button
        Me.butsave = New System.Windows.Forms.Button
        Me.butdel = New System.Windows.Forms.Button
        Me.dg = New System.Windows.Forms.DataGridView
        Me.sno = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.opername = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.mctype = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.sam = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.pcs = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.manpwr = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grade = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.process = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.style = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'butexit
        '
        Me.butexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butexit.Location = New System.Drawing.Point(830, 12)
        Me.butexit.Name = "butexit"
        Me.butexit.Size = New System.Drawing.Size(55, 23)
        Me.butexit.TabIndex = 20
        Me.butexit.Text = "Exit"
        Me.butexit.UseVisualStyleBackColor = True
        '
        'butcancel
        '
        Me.butcancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butcancel.Location = New System.Drawing.Point(773, 12)
        Me.butcancel.Name = "butcancel"
        Me.butcancel.Size = New System.Drawing.Size(55, 23)
        Me.butcancel.TabIndex = 19
        Me.butcancel.Text = "Cancel"
        Me.butcancel.UseVisualStyleBackColor = True
        '
        'butsave
        '
        Me.butsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butsave.Location = New System.Drawing.Point(659, 12)
        Me.butsave.Name = "butsave"
        Me.butsave.Size = New System.Drawing.Size(55, 23)
        Me.butsave.TabIndex = 18
        Me.butsave.Text = "Add"
        Me.butsave.UseVisualStyleBackColor = True
        '
        'butdel
        '
        Me.butdel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butdel.Location = New System.Drawing.Point(716, 12)
        Me.butdel.Name = "butdel"
        Me.butdel.Size = New System.Drawing.Size(55, 23)
        Me.butdel.TabIndex = 17
        Me.butdel.Text = "Delete"
        Me.butdel.UseVisualStyleBackColor = True
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
        Me.dg.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.sno, Me.opername, Me.mctype, Me.sam, Me.pcs, Me.manpwr, Me.grade, Me.process, Me.style})
        Me.dg.Location = New System.Drawing.Point(12, 74)
        Me.dg.MultiSelect = False
        Me.dg.Name = "dg"
        Me.dg.RowHeadersVisible = False
        Me.dg.Size = New System.Drawing.Size(1000, 514)
        Me.dg.TabIndex = 16
        '
        'sno
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.sno.DefaultCellStyle = DataGridViewCellStyle2
        Me.sno.HeaderText = "Sno"
        Me.sno.Name = "sno"
        '
        'opername
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.opername.DefaultCellStyle = DataGridViewCellStyle3
        Me.opername.HeaderText = "Operation Name"
        Me.opername.Name = "opername"
        Me.opername.Width = 250
        '
        'mctype
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.mctype.DefaultCellStyle = DataGridViewCellStyle4
        Me.mctype.HeaderText = "Mac Type"
        Me.mctype.Name = "mctype"
        Me.mctype.Width = 80
        '
        'sam
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.sam.DefaultCellStyle = DataGridViewCellStyle5
        Me.sam.HeaderText = "SAM"
        Me.sam.Name = "sam"
        Me.sam.Width = 70
        '
        'pcs
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.pcs.DefaultCellStyle = DataGridViewCellStyle6
        Me.pcs.HeaderText = "PCS"
        Me.pcs.Name = "pcs"
        Me.pcs.Width = 70
        '
        'manpwr
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.manpwr.DefaultCellStyle = DataGridViewCellStyle7
        Me.manpwr.HeaderText = "Man Power"
        Me.manpwr.Name = "manpwr"
        Me.manpwr.Width = 80
        '
        'grade
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.grade.DefaultCellStyle = DataGridViewCellStyle8
        Me.grade.HeaderText = "Job Grade"
        Me.grade.Name = "grade"
        Me.grade.Width = 80
        '
        'process
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.process.DefaultCellStyle = DataGridViewCellStyle9
        Me.process.HeaderText = "Process Dept"
        Me.process.Name = "process"
        Me.process.Width = 150
        '
        'style
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.style.DefaultCellStyle = DataGridViewCellStyle10
        Me.style.HeaderText = "Style"
        Me.style.Name = "style"
        '
        'Frmprocessjobmaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 703)
        Me.Controls.Add(Me.butexit)
        Me.Controls.Add(Me.butcancel)
        Me.Controls.Add(Me.butsave)
        Me.Controls.Add(Me.butdel)
        Me.Controls.Add(Me.dg)
        Me.Name = "Frmprocessjobmaster"
        Me.Text = "Process Job Master"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents butexit As System.Windows.Forms.Button
    Friend WithEvents butcancel As System.Windows.Forms.Button
    Friend WithEvents butsave As System.Windows.Forms.Button
    Friend WithEvents butdel As System.Windows.Forms.Button
    Friend WithEvents dg As System.Windows.Forms.DataGridView
    Friend WithEvents sno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents opername As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mctype As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sam As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents manpwr As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grade As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents process As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents style As System.Windows.Forms.DataGridViewTextBoxColumn
End Class

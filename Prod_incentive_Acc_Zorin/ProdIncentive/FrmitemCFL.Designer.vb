<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmitemCFL
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.butcancel = New System.Windows.Forms.Button
        Me.butchoose = New System.Windows.Forms.Button
        Me.dgvGrid = New System.Windows.Forms.DataGridView
        Me.lblName = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        CType(Me.dgvGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'butcancel
        '
        Me.butcancel.BackColor = System.Drawing.Color.Khaki
        Me.butcancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butcancel.Location = New System.Drawing.Point(84, 215)
        Me.butcancel.Name = "butcancel"
        Me.butcancel.Size = New System.Drawing.Size(75, 23)
        Me.butcancel.TabIndex = 123
        Me.butcancel.Text = "Cancel"
        Me.butcancel.UseVisualStyleBackColor = False
        '
        'butchoose
        '
        Me.butchoose.BackColor = System.Drawing.Color.Khaki
        Me.butchoose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butchoose.Location = New System.Drawing.Point(3, 215)
        Me.butchoose.Name = "butchoose"
        Me.butchoose.Size = New System.Drawing.Size(75, 23)
        Me.butchoose.TabIndex = 122
        Me.butchoose.Text = "Choose"
        Me.butchoose.UseVisualStyleBackColor = False
        '
        'dgvGrid
        '
        Me.dgvGrid.AllowUserToAddRows = False
        Me.dgvGrid.AllowUserToDeleteRows = False
        Me.dgvGrid.AllowUserToResizeRows = False
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.dgvGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        Me.dgvGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.dgvGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(233, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvGrid.EnableHeadersVisualStyles = False
        Me.dgvGrid.Location = New System.Drawing.Point(3, 35)
        Me.dgvGrid.MultiSelect = False
        Me.dgvGrid.Name = "dgvGrid"
        Me.dgvGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(233, Byte), Integer))
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(233, Byte), Integer))
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LightSteelBlue
        Me.dgvGrid.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvGrid.RowTemplate.Height = 18
        Me.dgvGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvGrid.Size = New System.Drawing.Size(500, 174)
        Me.dgvGrid.TabIndex = 121
        Me.dgvGrid.TabStop = False
        Me.dgvGrid.VirtualMode = True
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(89, 11)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(71, 13)
        Me.lblName.TabIndex = 120
        Me.lblName.Text = "Item Name"
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.Location = New System.Drawing.Point(166, 12)
        Me.txtName.MaxLength = 100
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(309, 18)
        Me.txtName.TabIndex = 119
        '
        'FrmitemCFL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(512, 253)
        Me.Controls.Add(Me.butcancel)
        Me.Controls.Add(Me.butchoose)
        Me.Controls.Add(Me.dgvGrid)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.txtName)
        Me.Name = "FrmitemCFL"
        Me.Text = "FrmitemCFL"
        CType(Me.dgvGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents butcancel As System.Windows.Forms.Button
    Friend WithEvents butchoose As System.Windows.Forms.Button
    Private WithEvents dgvGrid As System.Windows.Forms.DataGridView
    Private WithEvents lblName As System.Windows.Forms.Label
    Private WithEvents txtName As System.Windows.Forms.TextBox
End Class

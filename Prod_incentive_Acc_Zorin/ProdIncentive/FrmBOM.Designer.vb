<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBOM
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.CMDADD = New System.Windows.Forms.Button
        Me.CMDUPDT = New System.Windows.Forms.Button
        Me.CMDDEL = New System.Windows.Forms.Button
        Me.cmdclear = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.DataGridView1.Location = New System.Drawing.Point(39, 134)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(882, 383)
        Me.DataGridView1.TabIndex = 0
        '
        'CMDADD
        '
        Me.CMDADD.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDADD.Location = New System.Drawing.Point(490, 97)
        Me.CMDADD.Name = "CMDADD"
        Me.CMDADD.Size = New System.Drawing.Size(85, 31)
        Me.CMDADD.TabIndex = 1
        Me.CMDADD.Text = "Add"
        Me.CMDADD.UseVisualStyleBackColor = True
        '
        'CMDUPDT
        '
        Me.CMDUPDT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDUPDT.Location = New System.Drawing.Point(581, 97)
        Me.CMDUPDT.Name = "CMDUPDT"
        Me.CMDUPDT.Size = New System.Drawing.Size(85, 31)
        Me.CMDUPDT.TabIndex = 2
        Me.CMDUPDT.Text = "Update"
        Me.CMDUPDT.UseVisualStyleBackColor = True
        '
        'CMDDEL
        '
        Me.CMDDEL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMDDEL.Location = New System.Drawing.Point(672, 97)
        Me.CMDDEL.Name = "CMDDEL"
        Me.CMDDEL.Size = New System.Drawing.Size(85, 31)
        Me.CMDDEL.TabIndex = 3
        Me.CMDDEL.Text = "Delete"
        Me.CMDDEL.UseVisualStyleBackColor = True
        '
        'cmdclear
        '
        Me.cmdclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.Location = New System.Drawing.Point(763, 97)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(85, 31)
        Me.cmdclear.TabIndex = 4
        Me.cmdclear.Text = "Refresh"
        Me.cmdclear.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.Location = New System.Drawing.Point(854, 97)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(85, 31)
        Me.cmdexit.TabIndex = 5
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'FrmBOM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 734)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdclear)
        Me.Controls.Add(Me.CMDDEL)
        Me.Controls.Add(Me.CMDUPDT)
        Me.Controls.Add(Me.CMDADD)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "FrmBOM"
        Me.Text = "FRMBOM"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents CMDADD As System.Windows.Forms.Button
    Friend WithEvents CMDUPDT As System.Windows.Forms.Button
    Friend WithEvents CMDDEL As System.Windows.Forms.Button
    Friend WithEvents cmdclear As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
End Class

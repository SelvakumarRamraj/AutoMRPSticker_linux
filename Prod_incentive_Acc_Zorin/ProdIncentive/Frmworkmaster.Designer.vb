<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmworkmaster
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label12 = New System.Windows.Forms.Label
        Me.cmbservice = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.butexit = New System.Windows.Forms.Button
        Me.butsave = New System.Windows.Forms.Button
        Me.butdisp = New System.Windows.Forms.Button
        Me.butdel = New System.Windows.Forms.Button
        Me.butedit = New System.Windows.Forms.Button
        Me.butnew = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Snow
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.cmbservice)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(230, 71)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(606, 269)
        Me.Panel1.TabIndex = 37
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
        Me.Label12.Text = "Spare Parts Master"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbservice
        '
        Me.cmbservice.FormattingEnabled = True
        Me.cmbservice.Location = New System.Drawing.Point(190, 76)
        Me.cmbservice.Name = "cmbservice"
        Me.cmbservice.Size = New System.Drawing.Size(269, 21)
        Me.cmbservice.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(56, 77)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Service/Work Name"
        '
        'butexit
        '
        Me.butexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butexit.Location = New System.Drawing.Point(627, 346)
        Me.butexit.Name = "butexit"
        Me.butexit.Size = New System.Drawing.Size(57, 24)
        Me.butexit.TabIndex = 42
        Me.butexit.Tag = "15"
        Me.butexit.Text = "Exit"
        Me.butexit.UseVisualStyleBackColor = True
        '
        'butsave
        '
        Me.butsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butsave.Location = New System.Drawing.Point(567, 346)
        Me.butsave.Name = "butsave"
        Me.butsave.Size = New System.Drawing.Size(57, 24)
        Me.butsave.TabIndex = 41
        Me.butsave.Tag = "14"
        Me.butsave.Text = "Save"
        Me.butsave.UseVisualStyleBackColor = True
        '
        'butdisp
        '
        Me.butdisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butdisp.Location = New System.Drawing.Point(506, 346)
        Me.butdisp.Name = "butdisp"
        Me.butdisp.Size = New System.Drawing.Size(57, 24)
        Me.butdisp.TabIndex = 40
        Me.butdisp.Tag = "13"
        Me.butdisp.Text = "Display"
        Me.butdisp.UseVisualStyleBackColor = True
        '
        'butdel
        '
        Me.butdel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butdel.Location = New System.Drawing.Point(447, 346)
        Me.butdel.Name = "butdel"
        Me.butdel.Size = New System.Drawing.Size(57, 24)
        Me.butdel.TabIndex = 38
        Me.butdel.Tag = "12"
        Me.butdel.Text = "Delete"
        Me.butdel.UseVisualStyleBackColor = True
        '
        'butedit
        '
        Me.butedit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butedit.Location = New System.Drawing.Point(387, 346)
        Me.butedit.Name = "butedit"
        Me.butedit.Size = New System.Drawing.Size(57, 24)
        Me.butedit.TabIndex = 39
        Me.butedit.Tag = "11"
        Me.butedit.Text = "Edit"
        Me.butedit.UseVisualStyleBackColor = True
        '
        'butnew
        '
        Me.butnew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butnew.Location = New System.Drawing.Point(326, 346)
        Me.butnew.Name = "butnew"
        Me.butnew.Size = New System.Drawing.Size(57, 24)
        Me.butnew.TabIndex = 36
        Me.butnew.Tag = "1"
        Me.butnew.Text = "New"
        Me.butnew.UseVisualStyleBackColor = True
        '
        'Frmworkmaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1067, 440)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.butexit)
        Me.Controls.Add(Me.butsave)
        Me.Controls.Add(Me.butdisp)
        Me.Controls.Add(Me.butdel)
        Me.Controls.Add(Me.butedit)
        Me.Controls.Add(Me.butnew)
        Me.Name = "Frmworkmaster"
        Me.Text = "Frmworkmaster"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbservice As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents butexit As System.Windows.Forms.Button
    Friend WithEvents butsave As System.Windows.Forms.Button
    Friend WithEvents butdisp As System.Windows.Forms.Button
    Friend WithEvents butdel As System.Windows.Forms.Button
    Friend WithEvents butedit As System.Windows.Forms.Button
    Friend WithEvents butnew As System.Windows.Forms.Button
End Class

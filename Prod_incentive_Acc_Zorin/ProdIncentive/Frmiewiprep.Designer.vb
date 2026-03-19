<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmiewiprep
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
        Me.dg = New System.Windows.Forms.DataGridView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.butxl = New System.Windows.Forms.Button()
        Me.chkspl = New System.Windows.Forms.CheckBox()
        Me.Mskdateto = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.mskdatefr = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkacc = New System.Windows.Forms.CheckBox()
        Me.Btnprint = New System.Windows.Forms.Button()
        Me.txtremark = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dg
        '
        Me.dg.AllowUserToAddRows = False
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Location = New System.Drawing.Point(4, 77)
        Me.dg.Name = "dg"
        Me.dg.RowHeadersVisible = False
        Me.dg.Size = New System.Drawing.Size(1225, 653)
        Me.dg.TabIndex = 0
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(985, 35)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(61, 23)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Exit"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(751, 34)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(61, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Display"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'butxl
        '
        Me.butxl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butxl.Location = New System.Drawing.Point(818, 33)
        Me.butxl.Name = "butxl"
        Me.butxl.Size = New System.Drawing.Size(61, 23)
        Me.butxl.TabIndex = 8
        Me.butxl.Text = "Excel"
        Me.butxl.UseVisualStyleBackColor = True
        '
        'chkspl
        '
        Me.chkspl.AutoSize = True
        Me.chkspl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkspl.Location = New System.Drawing.Point(696, 39)
        Me.chkspl.Name = "chkspl"
        Me.chkspl.Size = New System.Drawing.Size(49, 17)
        Me.chkspl.TabIndex = 9
        Me.chkspl.Text = "SPL"
        Me.chkspl.UseVisualStyleBackColor = True
        '
        'Mskdateto
        '
        Me.Mskdateto.Location = New System.Drawing.Point(190, 36)
        Me.Mskdateto.Mask = "##-##-####"
        Me.Mskdateto.Name = "Mskdateto"
        Me.Mskdateto.Size = New System.Drawing.Size(72, 20)
        Me.Mskdateto.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(162, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(22, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "To"
        '
        'mskdatefr
        '
        Me.mskdatefr.Location = New System.Drawing.Point(81, 36)
        Me.mskdatefr.Mask = "##-##-####"
        Me.mskdatefr.Name = "mskdatefr"
        Me.mskdatefr.Size = New System.Drawing.Size(72, 20)
        Me.mskdatefr.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Date From"
        '
        'chkacc
        '
        Me.chkacc.AutoSize = True
        Me.chkacc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkacc.Location = New System.Drawing.Point(12, 12)
        Me.chkacc.Name = "chkacc"
        Me.chkacc.Size = New System.Drawing.Size(84, 17)
        Me.chkacc.TabIndex = 14
        Me.chkacc.Text = "Accessory"
        Me.chkacc.UseVisualStyleBackColor = True
        '
        'Btnprint
        '
        Me.Btnprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btnprint.Location = New System.Drawing.Point(885, 35)
        Me.Btnprint.Name = "Btnprint"
        Me.Btnprint.Size = New System.Drawing.Size(94, 23)
        Me.Btnprint.TabIndex = 15
        Me.Btnprint.Text = "Indent Print"
        Me.Btnprint.UseVisualStyleBackColor = True
        '
        'txtremark
        '
        Me.txtremark.Location = New System.Drawing.Point(336, 37)
        Me.txtremark.Name = "txtremark"
        Me.txtremark.Size = New System.Drawing.Size(325, 20)
        Me.txtremark.TabIndex = 16
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(274, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Remarks"
        '
        'Frmiewiprep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1241, 735)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtremark)
        Me.Controls.Add(Me.Btnprint)
        Me.Controls.Add(Me.chkacc)
        Me.Controls.Add(Me.Mskdateto)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.mskdatefr)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.chkspl)
        Me.Controls.Add(Me.butxl)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dg)
        Me.Name = "Frmiewiprep"
        Me.Text = "Frmiewiprep"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dg As System.Windows.Forms.DataGridView
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents butxl As System.Windows.Forms.Button
    Friend WithEvents chkspl As System.Windows.Forms.CheckBox
    Friend WithEvents Mskdateto As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mskdatefr As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkacc As System.Windows.Forms.CheckBox
    Friend WithEvents Btnprint As System.Windows.Forms.Button
    Friend WithEvents txtremark As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class

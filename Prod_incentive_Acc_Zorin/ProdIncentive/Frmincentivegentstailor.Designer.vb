<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmincentivegentstailor
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
        Me.Mskdateto = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.mskdatefr = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dg = New System.Windows.Forms.DataGridView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.butexit = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtempno = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Chkcut = New System.Windows.Forms.CheckBox()
        Me.Chkot = New System.Windows.Forms.CheckBox()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Mskdateto
        '
        Me.Mskdateto.Location = New System.Drawing.Point(191, 12)
        Me.Mskdateto.Mask = "##-##-####"
        Me.Mskdateto.Name = "Mskdateto"
        Me.Mskdateto.Size = New System.Drawing.Size(72, 20)
        Me.Mskdateto.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(163, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(22, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "To"
        '
        'mskdatefr
        '
        Me.mskdatefr.Location = New System.Drawing.Point(85, 12)
        Me.mskdatefr.Mask = "##-##-####"
        Me.mskdatefr.Name = "mskdatefr"
        Me.mskdatefr.Size = New System.Drawing.Size(72, 20)
        Me.mskdatefr.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(14, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Date From"
        '
        'dg
        '
        Me.dg.AllowUserToAddRows = False
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Location = New System.Drawing.Point(5, 83)
        Me.dg.Name = "dg"
        Me.dg.Size = New System.Drawing.Size(1153, 514)
        Me.dg.TabIndex = 13
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(450, 12)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(103, 23)
        Me.Button2.TabIndex = 19
        Me.Button2.Text = "Excel Export"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(369, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "Display"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'butexit
        '
        Me.butexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butexit.Location = New System.Drawing.Point(695, 12)
        Me.butexit.Name = "butexit"
        Me.butexit.Size = New System.Drawing.Size(58, 23)
        Me.butexit.TabIndex = 20
        Me.butexit.Text = "Exit"
        Me.butexit.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(14, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Emp.No."
        '
        'txtempno
        '
        Me.txtempno.Location = New System.Drawing.Point(75, 49)
        Me.txtempno.Name = "txtempno"
        Me.txtempno.Size = New System.Drawing.Size(110, 20)
        Me.txtempno.TabIndex = 22
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(559, 12)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(130, 23)
        Me.Button3.TabIndex = 23
        Me.Button3.Text = "Production Compare"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Chkcut
        '
        Me.Chkcut.AutoSize = True
        Me.Chkcut.Location = New System.Drawing.Point(775, 17)
        Me.Chkcut.Name = "Chkcut"
        Me.Chkcut.Size = New System.Drawing.Size(104, 17)
        Me.Chkcut.TabIndex = 24
        Me.Chkcut.Text = "Cutting Compare"
        Me.Chkcut.UseVisualStyleBackColor = True
        '
        'Chkot
        '
        Me.Chkot.AutoSize = True
        Me.Chkot.Location = New System.Drawing.Point(907, 17)
        Me.Chkot.Name = "Chkot"
        Me.Chkot.Size = New System.Drawing.Size(81, 17)
        Me.Chkot.TabIndex = 25
        Me.Chkot.Text = "Without OT"
        Me.Chkot.UseVisualStyleBackColor = True
        '
        'Frmincentivegentstailor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1159, 703)
        Me.Controls.Add(Me.Chkot)
        Me.Controls.Add(Me.Chkcut)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.txtempno)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.butexit)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dg)
        Me.Controls.Add(Me.Mskdateto)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.mskdatefr)
        Me.Controls.Add(Me.Label2)
        Me.Name = "Frmincentivegentstailor"
        Me.Text = "Frmincentivegentstailor"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Mskdateto As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mskdatefr As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dg As System.Windows.Forms.DataGridView
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents butexit As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtempno As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Chkcut As System.Windows.Forms.CheckBox
    Friend WithEvents Chkot As System.Windows.Forms.CheckBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmtailoratt
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
        Me.dg = New System.Windows.Forms.DataGridView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.mskdatto = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.mskdatfr = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkdet = New System.Windows.Forms.CheckBox()
        Me.txtlinno = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Btnxl = New System.Windows.Forms.Button()
        Me.chkpivot = New System.Windows.Forms.CheckBox()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dg
        '
        Me.dg.AllowUserToAddRows = False
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Location = New System.Drawing.Point(7, 83)
        Me.dg.Name = "dg"
        Me.dg.RowHeadersVisible = False
        Me.dg.Size = New System.Drawing.Size(1246, 623)
        Me.dg.TabIndex = 13
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(772, 48)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(61, 23)
        Me.Button2.TabIndex = 12
        Me.Button2.Text = "Exit"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(620, 47)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(61, 23)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Display"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'mskdatto
        '
        Me.mskdatto.Location = New System.Drawing.Point(243, 48)
        Me.mskdatto.Mask = "##-##-####"
        Me.mskdatto.Name = "mskdatto"
        Me.mskdatto.Size = New System.Drawing.Size(70, 20)
        Me.mskdatto.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(213, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(22, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "To"
        '
        'mskdatfr
        '
        Me.mskdatfr.Location = New System.Drawing.Point(137, 48)
        Me.mskdatfr.Mask = "##-##-####"
        Me.mskdatfr.Name = "mskdatfr"
        Me.mskdatfr.Size = New System.Drawing.Size(70, 20)
        Me.mskdatfr.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(70, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Date From"
        '
        'chkdet
        '
        Me.chkdet.AutoSize = True
        Me.chkdet.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkdet.Location = New System.Drawing.Point(334, 50)
        Me.chkdet.Name = "chkdet"
        Me.chkdet.Size = New System.Drawing.Size(59, 17)
        Me.chkdet.TabIndex = 14
        Me.chkdet.Text = "Detail"
        Me.chkdet.UseVisualStyleBackColor = True
        '
        'txtlinno
        '
        Me.txtlinno.Location = New System.Drawing.Point(456, 48)
        Me.txtlinno.Name = "txtlinno"
        Me.txtlinno.Size = New System.Drawing.Size(100, 20)
        Me.txtlinno.TabIndex = 15
        Me.txtlinno.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(399, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Line No"
        '
        'Btnxl
        '
        Me.Btnxl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btnxl.Location = New System.Drawing.Point(684, 48)
        Me.Btnxl.Name = "Btnxl"
        Me.Btnxl.Size = New System.Drawing.Size(86, 23)
        Me.Btnxl.TabIndex = 17
        Me.Btnxl.Text = "Excel Export"
        Me.Btnxl.UseVisualStyleBackColor = True
        '
        'chkpivot
        '
        Me.chkpivot.AutoSize = True
        Me.chkpivot.Location = New System.Drawing.Point(137, 25)
        Me.chkpivot.Name = "chkpivot"
        Me.chkpivot.Size = New System.Drawing.Size(93, 17)
        Me.chkpivot.TabIndex = 18
        Me.chkpivot.Text = "Daywise Pivot"
        Me.chkpivot.UseVisualStyleBackColor = True
        '
        'Frmtailoratt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1259, 735)
        Me.Controls.Add(Me.chkpivot)
        Me.Controls.Add(Me.Btnxl)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtlinno)
        Me.Controls.Add(Me.chkdet)
        Me.Controls.Add(Me.dg)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.mskdatto)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.mskdatfr)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frmtailoratt"
        Me.Text = "Frmtailoratt"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dg As System.Windows.Forms.DataGridView
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents mskdatto As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mskdatfr As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkdet As System.Windows.Forms.CheckBox
    Friend WithEvents txtlinno As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Btnxl As System.Windows.Forms.Button
    Friend WithEvents chkpivot As System.Windows.Forms.CheckBox
End Class

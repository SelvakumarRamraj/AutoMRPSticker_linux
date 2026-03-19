<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmprofcourwgt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frmprofcourwgt))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpfr = New System.Windows.Forms.DateTimePicker()
        Me.dtpto = New System.Windows.Forms.DateTimePicker()
        Me.Btndisp = New System.Windows.Forms.Button()
        Me.Btnsave = New System.Windows.Forms.Button()
        Me.btnexit = New System.Windows.Forms.Button()
        Me.Dg = New System.Windows.Forms.DataGridView()
        Me.optdateord = New System.Windows.Forms.RadioButton()
        Me.optinv = New System.Windows.Forms.RadioButton()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        CType(Me.Dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(60, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Date From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(229, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(22, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "To"
        '
        'dtpfr
        '
        Me.dtpfr.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfr.Location = New System.Drawing.Point(131, 30)
        Me.dtpfr.Name = "dtpfr"
        Me.dtpfr.Size = New System.Drawing.Size(92, 20)
        Me.dtpfr.TabIndex = 2
        '
        'dtpto
        '
        Me.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpto.Location = New System.Drawing.Point(257, 30)
        Me.dtpto.Name = "dtpto"
        Me.dtpto.Size = New System.Drawing.Size(92, 20)
        Me.dtpto.TabIndex = 3
        '
        'Btndisp
        '
        Me.Btndisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btndisp.Location = New System.Drawing.Point(621, 30)
        Me.Btndisp.Name = "Btndisp"
        Me.Btndisp.Size = New System.Drawing.Size(75, 23)
        Me.Btndisp.TabIndex = 4
        Me.Btndisp.Text = "Display"
        Me.Btndisp.UseVisualStyleBackColor = True
        '
        'Btnsave
        '
        Me.Btnsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btnsave.Location = New System.Drawing.Point(715, 30)
        Me.Btnsave.Name = "Btnsave"
        Me.Btnsave.Size = New System.Drawing.Size(75, 23)
        Me.Btnsave.TabIndex = 5
        Me.Btnsave.Text = "Save"
        Me.Btnsave.UseVisualStyleBackColor = True
        '
        'btnexit
        '
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.Location = New System.Drawing.Point(819, 30)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(75, 23)
        Me.btnexit.TabIndex = 6
        Me.btnexit.Text = "Exit"
        Me.btnexit.UseVisualStyleBackColor = True
        '
        'Dg
        '
        Me.Dg.AllowUserToAddRows = False
        Me.Dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dg.Location = New System.Drawing.Point(63, 57)
        Me.Dg.Name = "Dg"
        Me.Dg.RowHeadersVisible = False
        Me.Dg.Size = New System.Drawing.Size(1164, 621)
        Me.Dg.TabIndex = 7
        '
        'optdateord
        '
        Me.optdateord.AutoSize = True
        Me.optdateord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optdateord.Location = New System.Drawing.Point(410, 34)
        Me.optdateord.Name = "optdateord"
        Me.optdateord.Size = New System.Drawing.Size(87, 17)
        Me.optdateord.TabIndex = 8
        Me.optdateord.TabStop = True
        Me.optdateord.Text = "Date Order"
        Me.optdateord.UseVisualStyleBackColor = True
        '
        'optinv
        '
        Me.optinv.AutoSize = True
        Me.optinv.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optinv.Location = New System.Drawing.Point(503, 34)
        Me.optinv.Name = "optinv"
        Me.optinv.Size = New System.Drawing.Size(102, 17)
        Me.optinv.TabIndex = 9
        Me.optinv.TabStop = True
        Me.optinv.Text = "Sales Invoice"
        Me.optinv.UseVisualStyleBackColor = True
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'Frmprofcourwgt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1264, 703)
        Me.Controls.Add(Me.optinv)
        Me.Controls.Add(Me.optdateord)
        Me.Controls.Add(Me.Dg)
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.Btnsave)
        Me.Controls.Add(Me.Btndisp)
        Me.Controls.Add(Me.dtpto)
        Me.Controls.Add(Me.dtpfr)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frmprofcourwgt"
        Me.Text = "Frmprofcourwgt"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.Dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpfr As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Btndisp As System.Windows.Forms.Button
    Friend WithEvents Btnsave As System.Windows.Forms.Button
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents Dg As System.Windows.Forms.DataGridView
    Friend WithEvents optdateord As System.Windows.Forms.RadioButton
    Friend WithEvents optinv As System.Windows.Forms.RadioButton
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
End Class

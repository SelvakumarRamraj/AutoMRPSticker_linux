<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmREPORTIE_PROD
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
        Me.cmdexcel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.mskdatefr = New System.Windows.Forms.MaskedTextBox()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.CmdExit = New System.Windows.Forms.Button()
        Me.cmddisp = New System.Windows.Forms.Button()
        Me.dg = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.mskdateto = New System.Windows.Forms.MaskedTextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.optready = New System.Windows.Forms.RadioButton()
        Me.optproc = New System.Windows.Forms.RadioButton()
        Me.optcomp = New System.Windows.Forms.RadioButton()
        Me.Chkst = New System.Windows.Forms.CheckBox()
        Me.Lblstqty = New System.Windows.Forms.Label()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdexcel
        '
        Me.cmdexcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexcel.Location = New System.Drawing.Point(619, 27)
        Me.cmdexcel.Name = "cmdexcel"
        Me.cmdexcel.Size = New System.Drawing.Size(60, 23)
        Me.cmdexcel.TabIndex = 86
        Me.cmdexcel.Tag = "6"
        Me.cmdexcel.Text = "&Excel"
        Me.cmdexcel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 14)
        Me.Label1.TabIndex = 84
        Me.Label1.Text = "Date"
        '
        'mskdatefr
        '
        Me.mskdatefr.Location = New System.Drawing.Point(60, 24)
        Me.mskdatefr.Mask = "##-##-####"
        Me.mskdatefr.Name = "mskdatefr"
        Me.mskdatefr.Size = New System.Drawing.Size(64, 20)
        Me.mskdatefr.TabIndex = 0
        '
        'cmdclear
        '
        Me.cmdclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.Location = New System.Drawing.Point(685, 27)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(75, 23)
        Me.cmdclear.TabIndex = 7
        Me.cmdclear.Text = "&Clear"
        Me.cmdclear.UseVisualStyleBackColor = True
        '
        'CmdExit
        '
        Me.CmdExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdExit.Location = New System.Drawing.Point(766, 26)
        Me.CmdExit.Name = "CmdExit"
        Me.CmdExit.Size = New System.Drawing.Size(75, 23)
        Me.CmdExit.TabIndex = 8
        Me.CmdExit.Text = "E&xit"
        Me.CmdExit.UseVisualStyleBackColor = True
        '
        'cmddisp
        '
        Me.cmddisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddisp.Location = New System.Drawing.Point(538, 27)
        Me.cmddisp.Name = "cmddisp"
        Me.cmddisp.Size = New System.Drawing.Size(75, 23)
        Me.cmddisp.TabIndex = 81
        Me.cmddisp.Tag = "5"
        Me.cmddisp.Text = "D&isplay"
        Me.cmddisp.UseVisualStyleBackColor = True
        '
        'dg
        '
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Location = New System.Drawing.Point(12, 75)
        Me.dg.Name = "dg"
        Me.dg.Size = New System.Drawing.Size(1060, 494)
        Me.dg.TabIndex = 87
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(127, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 14)
        Me.Label2.TabIndex = 88
        Me.Label2.Text = "To"
        '
        'mskdateto
        '
        Me.mskdateto.Location = New System.Drawing.Point(155, 24)
        Me.mskdateto.Mask = "##-##-####"
        Me.mskdateto.Name = "mskdateto"
        Me.mskdateto.Size = New System.Drawing.Size(64, 20)
        Me.mskdateto.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.optready)
        Me.Panel1.Controls.Add(Me.optproc)
        Me.Panel1.Controls.Add(Me.optcomp)
        Me.Panel1.Location = New System.Drawing.Point(238, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(294, 34)
        Me.Panel1.TabIndex = 90
        '
        'optready
        '
        Me.optready.AutoSize = True
        Me.optready.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optready.Location = New System.Drawing.Point(190, 11)
        Me.optready.Name = "optready"
        Me.optready.Size = New System.Drawing.Size(98, 17)
        Me.optready.TabIndex = 4
        Me.optready.TabStop = True
        Me.optready.Text = "Ready Stock"
        Me.optready.UseVisualStyleBackColor = True
        '
        'optproc
        '
        Me.optproc.AutoSize = True
        Me.optproc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optproc.Location = New System.Drawing.Point(109, 11)
        Me.optproc.Name = "optproc"
        Me.optproc.Size = New System.Drawing.Size(70, 17)
        Me.optproc.TabIndex = 3
        Me.optproc.TabStop = True
        Me.optproc.Text = "Process"
        Me.optproc.UseVisualStyleBackColor = True
        '
        'optcomp
        '
        Me.optcomp.AutoSize = True
        Me.optcomp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optcomp.Location = New System.Drawing.Point(10, 10)
        Me.optcomp.Name = "optcomp"
        Me.optcomp.Size = New System.Drawing.Size(87, 17)
        Me.optcomp.TabIndex = 2
        Me.optcomp.TabStop = True
        Me.optcomp.Text = "Completion"
        Me.optcomp.UseVisualStyleBackColor = True
        '
        'Chkst
        '
        Me.Chkst.AutoSize = True
        Me.Chkst.Location = New System.Drawing.Point(847, 29)
        Me.Chkst.Name = "Chkst"
        Me.Chkst.Size = New System.Drawing.Size(111, 17)
        Me.Chkst.TabIndex = 91
        Me.Chkst.Text = "Stitching Prod Qty"
        Me.Chkst.UseVisualStyleBackColor = True
        '
        'Lblstqty
        '
        Me.Lblstqty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lblstqty.ForeColor = System.Drawing.Color.DarkGreen
        Me.Lblstqty.Location = New System.Drawing.Point(847, 50)
        Me.Lblstqty.Name = "Lblstqty"
        Me.Lblstqty.Size = New System.Drawing.Size(144, 19)
        Me.Lblstqty.TabIndex = 92
        Me.Lblstqty.Text = "0"
        Me.Lblstqty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmREPORTIE_PROD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1084, 698)
        Me.Controls.Add(Me.Lblstqty)
        Me.Controls.Add(Me.Chkst)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.mskdateto)
        Me.Controls.Add(Me.dg)
        Me.Controls.Add(Me.cmdexcel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.mskdatefr)
        Me.Controls.Add(Me.cmdclear)
        Me.Controls.Add(Me.CmdExit)
        Me.Controls.Add(Me.cmddisp)
        Me.Name = "FrmREPORTIE_PROD"
        Me.Text = "FrmREPORTIE_PROD"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdexcel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mskdatefr As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmdclear As System.Windows.Forms.Button
    Friend WithEvents CmdExit As System.Windows.Forms.Button
    Friend WithEvents cmddisp As System.Windows.Forms.Button
    Friend WithEvents dg As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mskdateto As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents optready As System.Windows.Forms.RadioButton
    Friend WithEvents optproc As System.Windows.Forms.RadioButton
    Friend WithEvents optcomp As System.Windows.Forms.RadioButton
    Friend WithEvents Chkst As System.Windows.Forms.CheckBox
    Friend WithEvents Lblstqty As System.Windows.Forms.Label
End Class

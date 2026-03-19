<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmpnlconsolidate
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
        Me.Label9 = New System.Windows.Forms.Label
        Me.mskdateto = New System.Windows.Forms.MaskedTextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.mskdatefr = New System.Windows.Forms.MaskedTextBox
        Me.cmdexport = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        Me.txtsql = New System.Windows.Forms.TextBox
        Me.txtresult = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.lsv = New System.Windows.Forms.ListView
        Me.SuspendLayout()
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(265, 41)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 17)
        Me.Label9.TabIndex = 35
        Me.Label9.Text = "To"
        '
        'mskdateto
        '
        Me.mskdateto.Location = New System.Drawing.Point(322, 38)
        Me.mskdateto.Mask = "##-##-####"
        Me.mskdateto.Name = "mskdateto"
        Me.mskdateto.Size = New System.Drawing.Size(90, 20)
        Me.mskdateto.TabIndex = 34
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(114, 41)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 17)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "Date"
        '
        'mskdatefr
        '
        Me.mskdatefr.Location = New System.Drawing.Point(165, 38)
        Me.mskdatefr.Mask = "##-##-####"
        Me.mskdatefr.Name = "mskdatefr"
        Me.mskdatefr.Size = New System.Drawing.Size(90, 20)
        Me.mskdatefr.TabIndex = 33
        '
        'cmdexport
        '
        Me.cmdexport.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexport.Location = New System.Drawing.Point(497, 41)
        Me.cmdexport.Name = "cmdexport"
        Me.cmdexport.Size = New System.Drawing.Size(75, 23)
        Me.cmdexport.TabIndex = 36
        Me.cmdexport.Text = "Export"
        Me.cmdexport.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.Location = New System.Drawing.Point(610, 41)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(75, 23)
        Me.cmdexit.TabIndex = 37
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'txtsql
        '
        Me.txtsql.Location = New System.Drawing.Point(63, 81)
        Me.txtsql.Multiline = True
        Me.txtsql.Name = "txtsql"
        Me.txtsql.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtsql.Size = New System.Drawing.Size(752, 116)
        Me.txtsql.TabIndex = 38
        Me.txtsql.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'txtresult
        '
        Me.txtresult.Location = New System.Drawing.Point(63, 232)
        Me.txtresult.Multiline = True
        Me.txtresult.Name = "txtresult"
        Me.txtresult.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtresult.Size = New System.Drawing.Size(752, 32)
        Me.txtresult.TabIndex = 39
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(306, 203)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 40
        Me.Button1.Text = "Go"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'lsv
        '
        Me.lsv.Location = New System.Drawing.Point(63, 270)
        Me.lsv.Name = "lsv"
        Me.lsv.Size = New System.Drawing.Size(752, 97)
        Me.lsv.TabIndex = 41
        Me.lsv.UseCompatibleStateImageBehavior = False
        '
        'Frmpnlconsolidate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 698)
        Me.Controls.Add(Me.lsv)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtresult)
        Me.Controls.Add(Me.txtsql)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdexport)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.mskdateto)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.mskdatefr)
        Me.Name = "Frmpnlconsolidate"
        Me.Text = "Frmpnlconsolidate"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents mskdateto As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents mskdatefr As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmdexport As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents txtsql As System.Windows.Forms.TextBox
    Friend WithEvents txtresult As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lsv As System.Windows.Forms.ListView
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGPSummary
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.dt = New System.Windows.Forms.DateTimePicker()
        Me.dt1 = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.frm1Refresh = New System.Windows.Forms.Button()
        Me.Btnexit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'dt
        '
        Me.dt.Location = New System.Drawing.Point(118, 9)
        Me.dt.Name = "dt"
        Me.dt.Size = New System.Drawing.Size(204, 20)
        Me.dt.TabIndex = 139
        '
        'dt1
        '
        Me.dt1.Location = New System.Drawing.Point(424, 9)
        Me.dt1.Name = "dt1"
        Me.dt1.Size = New System.Drawing.Size(230, 20)
        Me.dt1.TabIndex = 140
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(22, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 13)
        Me.Label6.TabIndex = 141
        Me.Label6.Text = "From Date  :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(351, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 142
        Me.Label1.Text = "To Date  :"
        '
        'frm1Refresh
        '
        Me.frm1Refresh.Location = New System.Drawing.Point(673, 8)
        Me.frm1Refresh.Name = "frm1Refresh"
        Me.frm1Refresh.Size = New System.Drawing.Size(92, 23)
        Me.frm1Refresh.TabIndex = 143
        Me.frm1Refresh.Text = "Display"
        Me.frm1Refresh.UseVisualStyleBackColor = True
        '
        'Btnexit
        '
        Me.Btnexit.Location = New System.Drawing.Point(771, 10)
        Me.Btnexit.Name = "Btnexit"
        Me.Btnexit.Size = New System.Drawing.Size(92, 23)
        Me.Btnexit.TabIndex = 144
        Me.Btnexit.Text = "Exit"
        Me.Btnexit.UseVisualStyleBackColor = True
        '
        'frmGPSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1263, 657)
        Me.Controls.Add(Me.Btnexit)
        Me.Controls.Add(Me.frm1Refresh)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.dt1)
        Me.Controls.Add(Me.dt)
        Me.Name = "frmGPSummary"
        Me.Text = "GPSummary"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dt As System.Windows.Forms.DateTimePicker
    Friend WithEvents dt1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents frm1Refresh As System.Windows.Forms.Button
    Friend WithEvents Btnexit As Button
End Class

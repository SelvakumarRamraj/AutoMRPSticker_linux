<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmscanprint
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
        Me.view1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbyr = New System.Windows.Forms.ComboBox
        Me.txtno = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdok = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'view1
        '
        Me.view1.ActiveViewIndex = -1
        Me.view1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.view1.Cursor = System.Windows.Forms.Cursors.Default
        Me.view1.Location = New System.Drawing.Point(3, 41)
        Me.view1.Name = "view1"
        Me.view1.SelectionFormula = ""
        Me.view1.Size = New System.Drawing.Size(1001, 559)
        Me.view1.TabIndex = 32
        Me.view1.ViewTimeSelectionFormula = ""
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(329, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 23)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Posting Period"
        '
        'cmbyr
        '
        Me.cmbyr.FormattingEnabled = True
        Me.cmbyr.Location = New System.Drawing.Point(427, 11)
        Me.cmbyr.Name = "cmbyr"
        Me.cmbyr.Size = New System.Drawing.Size(121, 21)
        Me.cmbyr.TabIndex = 50
        '
        'txtno
        '
        Me.txtno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtno.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtno.Location = New System.Drawing.Point(156, 12)
        Me.txtno.Name = "txtno"
        Me.txtno.Size = New System.Drawing.Size(160, 20)
        Me.txtno.TabIndex = 49
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(81, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 23)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "DocNum"
        '
        'cmdok
        '
        Me.cmdok.Location = New System.Drawing.Point(590, 14)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(65, 20)
        Me.cmdok.TabIndex = 53
        Me.cmdok.Text = "OK"
        Me.cmdok.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.Location = New System.Drawing.Point(677, 14)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(65, 20)
        Me.cmdexit.TabIndex = 54
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'Frmscanprint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 698)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdok)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbyr)
        Me.Controls.Add(Me.txtno)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.view1)
        Me.Name = "Frmscanprint"
        Me.Text = "Frmscanprint"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents view1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbyr As System.Windows.Forms.ComboBox
    Friend WithEvents txtno As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdok As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
End Class

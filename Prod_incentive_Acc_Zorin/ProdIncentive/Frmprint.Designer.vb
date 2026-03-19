<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmprint
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frmprint))
        Me.flx = New AxMSFlexGridLib.AxMSFlexGrid
        Me.flxh = New AxMSFlexGridLib.AxMSFlexGrid
        Me.mskdatfr = New System.Windows.Forms.MaskedTextBox
        Me.mskdatto = New System.Windows.Forms.MaskedTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        CType(Me.flx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'flx
        '
        Me.flx.Location = New System.Drawing.Point(12, 231)
        Me.flx.Name = "flx"
        Me.flx.OcxState = CType(resources.GetObject("flx.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flx.Size = New System.Drawing.Size(973, 331)
        Me.flx.TabIndex = 0
        '
        'flxh
        '
        Me.flxh.Location = New System.Drawing.Point(12, 80)
        Me.flxh.Name = "flxh"
        Me.flxh.OcxState = CType(resources.GetObject("flxh.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxh.Size = New System.Drawing.Size(973, 145)
        Me.flxh.TabIndex = 1
        '
        'mskdatfr
        '
        Me.mskdatfr.Location = New System.Drawing.Point(90, 12)
        Me.mskdatfr.Mask = "##-##-####"
        Me.mskdatfr.Name = "mskdatfr"
        Me.mskdatfr.Size = New System.Drawing.Size(87, 20)
        Me.mskdatfr.TabIndex = 2
        '
        'mskdatto
        '
        Me.mskdatto.Location = New System.Drawing.Point(221, 12)
        Me.mskdatto.Mask = "##-##-####"
        Me.mskdatto.Name = "mskdatto"
        Me.mskdatto.Size = New System.Drawing.Size(87, 20)
        Me.mskdatto.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(35, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 15)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(183, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 15)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "To"
        '
        'Frmprint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 734)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.mskdatto)
        Me.Controls.Add(Me.mskdatfr)
        Me.Controls.Add(Me.flxh)
        Me.Controls.Add(Me.flx)
        Me.Name = "Frmprint"
        Me.Text = "Frmprint"
        CType(Me.flx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents flx As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents flxh As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents mskdatfr As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskdatto As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class

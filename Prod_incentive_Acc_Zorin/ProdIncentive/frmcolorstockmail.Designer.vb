<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmcolorstockmail
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmcolorstockmail))
        Me.flx = New AxMSFlexGridLib.AxMSFlexGrid
        Me.cmdxl = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        Me.cmdmail = New System.Windows.Forms.Button
        Me.cmddisp = New System.Windows.Forms.Button
        Me.cmdsel = New System.Windows.Forms.Button
        Me.flxp = New AxMSFlexGridLib.AxMSFlexGrid
        CType(Me.flx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'flx
        '
        Me.flx.Location = New System.Drawing.Point(12, 174)
        Me.flx.Name = "flx"
        Me.flx.OcxState = CType(resources.GetObject("flx.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flx.Size = New System.Drawing.Size(933, 284)
        Me.flx.TabIndex = 1
        '
        'cmdxl
        '
        Me.cmdxl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdxl.Location = New System.Drawing.Point(714, 12)
        Me.cmdxl.Name = "cmdxl"
        Me.cmdxl.Size = New System.Drawing.Size(75, 23)
        Me.cmdxl.TabIndex = 8
        Me.cmdxl.Text = "Export XL"
        Me.cmdxl.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.Location = New System.Drawing.Point(795, 13)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(75, 23)
        Me.cmdexit.TabIndex = 7
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'cmdmail
        '
        Me.cmdmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdmail.Location = New System.Drawing.Point(640, 62)
        Me.cmdmail.Name = "cmdmail"
        Me.cmdmail.Size = New System.Drawing.Size(75, 23)
        Me.cmdmail.TabIndex = 6
        Me.cmdmail.Text = "Mail"
        Me.cmdmail.UseVisualStyleBackColor = True
        '
        'cmddisp
        '
        Me.cmddisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddisp.Location = New System.Drawing.Point(552, 13)
        Me.cmddisp.Name = "cmddisp"
        Me.cmddisp.Size = New System.Drawing.Size(75, 23)
        Me.cmddisp.TabIndex = 5
        Me.cmddisp.Text = "Load Data"
        Me.cmddisp.UseVisualStyleBackColor = True
        '
        'cmdsel
        '
        Me.cmdsel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsel.Location = New System.Drawing.Point(12, 464)
        Me.cmdsel.Name = "cmdsel"
        Me.cmdsel.Size = New System.Drawing.Size(75, 23)
        Me.cmdsel.TabIndex = 9
        Me.cmdsel.Text = "Select All"
        Me.cmdsel.UseVisualStyleBackColor = True
        '
        'flxp
        '
        Me.flxp.Location = New System.Drawing.Point(12, 3)
        Me.flxp.Name = "flxp"
        Me.flxp.OcxState = CType(resources.GetObject("flxp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxp.Size = New System.Drawing.Size(518, 165)
        Me.flxp.TabIndex = 10
        '
        'frmcolorstockmail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 698)
        Me.Controls.Add(Me.flxp)
        Me.Controls.Add(Me.cmdsel)
        Me.Controls.Add(Me.cmdxl)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdmail)
        Me.Controls.Add(Me.cmddisp)
        Me.Controls.Add(Me.flx)
        Me.Name = "frmcolorstockmail"
        Me.Text = "frmcolorstockmail"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.flx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents flx As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents cmdxl As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdmail As System.Windows.Forms.Button
    Friend WithEvents cmddisp As System.Windows.Forms.Button
    Friend WithEvents cmdsel As System.Windows.Forms.Button
    Friend WithEvents flxp As AxMSFlexGridLib.AxMSFlexGrid
End Class

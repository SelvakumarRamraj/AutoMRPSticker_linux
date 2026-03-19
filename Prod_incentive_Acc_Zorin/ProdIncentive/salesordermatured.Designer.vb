<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class salesordermatured
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(salesordermatured))
        Me.flx = New AxMSFlexGridLib.AxMSFlexGrid
        Me.cmddisp = New System.Windows.Forms.Button
        Me.cmdupdt = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        Me.cmdxl = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.mskdate = New System.Windows.Forms.MaskedTextBox
        Me.cmdsel = New System.Windows.Forms.Button
        Me.cmbagent = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        CType(Me.flx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'flx
        '
        Me.flx.Location = New System.Drawing.Point(33, 62)
        Me.flx.Name = "flx"
        Me.flx.OcxState = CType(resources.GetObject("flx.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flx.Size = New System.Drawing.Size(888, 323)
        Me.flx.TabIndex = 0
        '
        'cmddisp
        '
        Me.cmddisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddisp.Location = New System.Drawing.Point(515, 13)
        Me.cmddisp.Name = "cmddisp"
        Me.cmddisp.Size = New System.Drawing.Size(75, 23)
        Me.cmddisp.TabIndex = 1
        Me.cmddisp.Text = "Load Data"
        Me.cmddisp.UseVisualStyleBackColor = True
        '
        'cmdupdt
        '
        Me.cmdupdt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdupdt.Location = New System.Drawing.Point(596, 13)
        Me.cmdupdt.Name = "cmdupdt"
        Me.cmdupdt.Size = New System.Drawing.Size(75, 23)
        Me.cmdupdt.TabIndex = 2
        Me.cmdupdt.Text = "Update"
        Me.cmdupdt.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.Location = New System.Drawing.Point(758, 13)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(75, 23)
        Me.cmdexit.TabIndex = 3
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'cmdxl
        '
        Me.cmdxl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdxl.Location = New System.Drawing.Point(677, 12)
        Me.cmdxl.Name = "cmdxl"
        Me.cmdxl.Size = New System.Drawing.Size(75, 23)
        Me.cmdxl.TabIndex = 4
        Me.cmdxl.Text = "Export XL"
        Me.cmdxl.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(57, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 23)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Date"
        '
        'mskdate
        '
        Me.mskdate.Location = New System.Drawing.Point(114, 10)
        Me.mskdate.Mask = "##-##-####"
        Me.mskdate.Name = "mskdate"
        Me.mskdate.Size = New System.Drawing.Size(90, 20)
        Me.mskdate.TabIndex = 6
        '
        'cmdsel
        '
        Me.cmdsel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsel.Location = New System.Drawing.Point(33, 391)
        Me.cmdsel.Name = "cmdsel"
        Me.cmdsel.Size = New System.Drawing.Size(75, 23)
        Me.cmdsel.TabIndex = 7
        Me.cmdsel.Text = "Select All"
        Me.cmdsel.UseVisualStyleBackColor = True
        '
        'cmbagent
        '
        Me.cmbagent.FormattingEnabled = True
        Me.cmbagent.Location = New System.Drawing.Point(300, 10)
        Me.cmbagent.Name = "cmbagent"
        Me.cmbagent.Size = New System.Drawing.Size(179, 21)
        Me.cmbagent.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(227, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 22)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Areacode"
        '
        'salesordermatured
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 698)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbagent)
        Me.Controls.Add(Me.cmdsel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.mskdate)
        Me.Controls.Add(Me.cmdxl)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdupdt)
        Me.Controls.Add(Me.cmddisp)
        Me.Controls.Add(Me.flx)
        Me.Name = "salesordermatured"
        Me.Text = "salesordermatured"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.flx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents flx As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents cmddisp As System.Windows.Forms.Button
    Friend WithEvents cmdupdt As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdxl As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mskdate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmdsel As System.Windows.Forms.Button
    Friend WithEvents cmbagent As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class

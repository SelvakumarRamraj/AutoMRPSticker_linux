<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmantsprocessbarcode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmantsprocessbarcode))
        Me.flx = New AxMSFlexGridLib.AxMSFlexGrid
        Me.cmddisp = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtno = New System.Windows.Forms.TextBox
        Me.cmbyr = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdprint = New System.Windows.Forms.Button
        Me.cmdclear = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtport = New System.Windows.Forms.TextBox
        Me.chkprndir = New System.Windows.Forms.CheckBox
        CType(Me.flx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'flx
        '
        Me.flx.Location = New System.Drawing.Point(50, 137)
        Me.flx.Name = "flx"
        Me.flx.OcxState = CType(resources.GetObject("flx.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flx.Size = New System.Drawing.Size(904, 180)
        Me.flx.TabIndex = 0
        '
        'cmddisp
        '
        Me.cmddisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddisp.Location = New System.Drawing.Point(577, 16)
        Me.cmddisp.Name = "cmddisp"
        Me.cmddisp.Size = New System.Drawing.Size(75, 23)
        Me.cmddisp.TabIndex = 4
        Me.cmddisp.Text = "Display"
        Me.cmddisp.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(255, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(141, 21)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Cutting Completion No."
        '
        'txtno
        '
        Me.txtno.Location = New System.Drawing.Point(402, 12)
        Me.txtno.Name = "txtno"
        Me.txtno.Size = New System.Drawing.Size(133, 20)
        Me.txtno.TabIndex = 1
        '
        'cmbyr
        '
        Me.cmbyr.FormattingEnabled = True
        Me.cmbyr.Location = New System.Drawing.Point(118, 12)
        Me.cmbyr.Name = "cmbyr"
        Me.cmbyr.Size = New System.Drawing.Size(131, 21)
        Me.cmbyr.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 18)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Posting Period"
        '
        'cmdprint
        '
        Me.cmdprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdprint.Location = New System.Drawing.Point(658, 18)
        Me.cmdprint.Name = "cmdprint"
        Me.cmdprint.Size = New System.Drawing.Size(75, 23)
        Me.cmdprint.TabIndex = 6
        Me.cmdprint.Text = "Print"
        Me.cmdprint.UseVisualStyleBackColor = True
        '
        'cmdclear
        '
        Me.cmdclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.Location = New System.Drawing.Point(739, 18)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(75, 23)
        Me.cmdclear.TabIndex = 7
        Me.cmdclear.Text = "Clear"
        Me.cmdclear.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.Location = New System.Drawing.Point(820, 18)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(75, 23)
        Me.cmdexit.TabIndex = 8
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(143, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 17)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Port"
        '
        'txtport
        '
        Me.txtport.Location = New System.Drawing.Point(185, 50)
        Me.txtport.Name = "txtport"
        Me.txtport.Size = New System.Drawing.Size(133, 20)
        Me.txtport.TabIndex = 2
        '
        'chkprndir
        '
        Me.chkprndir.AutoSize = True
        Me.chkprndir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkprndir.Location = New System.Drawing.Point(425, 52)
        Me.chkprndir.Name = "chkprndir"
        Me.chkprndir.Size = New System.Drawing.Size(90, 17)
        Me.chkprndir.TabIndex = 3
        Me.chkprndir.Text = "Print Direct"
        Me.chkprndir.UseVisualStyleBackColor = True
        '
        'frmantsprocessbarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 698)
        Me.Controls.Add(Me.chkprndir)
        Me.Controls.Add(Me.txtport)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdclear)
        Me.Controls.Add(Me.cmdprint)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbyr)
        Me.Controls.Add(Me.txtno)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmddisp)
        Me.Controls.Add(Me.flx)
        Me.Name = "frmantsprocessbarcode"
        Me.Text = "frmantsprocessbarcode"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.flx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents flx As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents cmddisp As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtno As System.Windows.Forms.TextBox
    Friend WithEvents cmbyr As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdprint As System.Windows.Forms.Button
    Friend WithEvents cmdclear As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtport As System.Windows.Forms.TextBox
    Friend WithEvents chkprndir As System.Windows.Forms.CheckBox
End Class

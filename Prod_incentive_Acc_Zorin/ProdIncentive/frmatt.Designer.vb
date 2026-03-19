<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmatt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmatt))
        Me.Tab1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.butsave = New System.Windows.Forms.Button
        Me.flx = New AxMSFlexGridLib.AxMSFlexGrid
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.cmdclear = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmddisp = New System.Windows.Forms.Button
        Me.cmdedit = New System.Windows.Forms.Button
        Me.cmdadd = New System.Windows.Forms.Button
        Me.mskdate = New System.Windows.Forms.MaskedTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.dv = New System.Windows.Forms.DataGridView
        Me.CmdExit = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Tab1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.flx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.dv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Tab1
        '
        Me.Tab1.Controls.Add(Me.TabPage1)
        Me.Tab1.Controls.Add(Me.TabPage2)
        Me.Tab1.Location = New System.Drawing.Point(44, 84)
        Me.Tab1.Name = "Tab1"
        Me.Tab1.SelectedIndex = 0
        Me.Tab1.Size = New System.Drawing.Size(841, 426)
        Me.Tab1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.butsave)
        Me.TabPage1.Controls.Add(Me.flx)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(833, 400)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Department Master"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'butsave
        '
        Me.butsave.Location = New System.Drawing.Point(232, 330)
        Me.butsave.Name = "butsave"
        Me.butsave.Size = New System.Drawing.Size(75, 23)
        Me.butsave.TabIndex = 77
        Me.butsave.Text = "Save"
        Me.butsave.UseVisualStyleBackColor = True
        '
        'flx
        '
        Me.flx.Location = New System.Drawing.Point(28, 30)
        Me.flx.Name = "flx"
        Me.flx.OcxState = CType(resources.GetObject("flx.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flx.Size = New System.Drawing.Size(334, 293)
        Me.flx.TabIndex = 76
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.cmdclear)
        Me.TabPage2.Controls.Add(Me.cmdSave)
        Me.TabPage2.Controls.Add(Me.cmddisp)
        Me.TabPage2.Controls.Add(Me.cmdedit)
        Me.TabPage2.Controls.Add(Me.cmdadd)
        Me.TabPage2.Controls.Add(Me.mskdate)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.dv)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(833, 400)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Attendance"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'cmdclear
        '
        Me.cmdclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.Location = New System.Drawing.Point(556, 336)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(75, 23)
        Me.cmdclear.TabIndex = 76
        Me.cmdclear.Text = "&Clear"
        Me.cmdclear.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSave.Location = New System.Drawing.Point(475, 336)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 74
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmddisp
        '
        Me.cmddisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddisp.Location = New System.Drawing.Point(394, 336)
        Me.cmddisp.Name = "cmddisp"
        Me.cmddisp.Size = New System.Drawing.Size(75, 23)
        Me.cmddisp.TabIndex = 73
        Me.cmddisp.Tag = "4"
        Me.cmddisp.Text = "D&isplay"
        Me.cmddisp.UseVisualStyleBackColor = True
        '
        'cmdedit
        '
        Me.cmdedit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdedit.Location = New System.Drawing.Point(232, 336)
        Me.cmdedit.Name = "cmdedit"
        Me.cmdedit.Size = New System.Drawing.Size(75, 23)
        Me.cmdedit.TabIndex = 72
        Me.cmdedit.Tag = "2"
        Me.cmdedit.Text = "&Edit"
        Me.cmdedit.UseVisualStyleBackColor = True
        '
        'cmdadd
        '
        Me.cmdadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdadd.Location = New System.Drawing.Point(151, 336)
        Me.cmdadd.Name = "cmdadd"
        Me.cmdadd.Size = New System.Drawing.Size(75, 23)
        Me.cmdadd.TabIndex = 71
        Me.cmdadd.Tag = "1"
        Me.cmdadd.Text = "&Add"
        Me.cmdadd.UseVisualStyleBackColor = True
        '
        'mskdate
        '
        Me.mskdate.Location = New System.Drawing.Point(124, 21)
        Me.mskdate.Mask = "##-##-####"
        Me.mskdate.Name = "mskdate"
        Me.mskdate.Size = New System.Drawing.Size(72, 20)
        Me.mskdate.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(60, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Date"
        '
        'dv
        '
        Me.dv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dv.Location = New System.Drawing.Point(44, 58)
        Me.dv.Name = "dv"
        Me.dv.Size = New System.Drawing.Size(538, 256)
        Me.dv.TabIndex = 0
        '
        'CmdExit
        '
        Me.CmdExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdExit.Location = New System.Drawing.Point(724, 40)
        Me.CmdExit.Name = "CmdExit"
        Me.CmdExit.Size = New System.Drawing.Size(75, 23)
        Me.CmdExit.TabIndex = 75
        Me.CmdExit.Text = "E&xit"
        Me.CmdExit.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.DarkRed
        Me.Label2.Location = New System.Drawing.Point(591, 101)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 13)
        Me.Label2.TabIndex = 77
        Me.Label2.Text = "F2 -> ADD Record"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.DarkRed
        Me.Label3.Location = New System.Drawing.Point(591, 143)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 13)
        Me.Label3.TabIndex = 78
        Me.Label3.Text = "F9 -> Delete Record"
        '
        'frmatt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 698)
        Me.Controls.Add(Me.Tab1)
        Me.Controls.Add(Me.CmdExit)
        Me.Name = "frmatt"
        Me.Text = "frmatt"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Tab1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.flx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.dv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Tab1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents dv As System.Windows.Forms.DataGridView
    Friend WithEvents mskdate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdclear As System.Windows.Forms.Button
    Friend WithEvents CmdExit As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmddisp As System.Windows.Forms.Button
    Friend WithEvents cmdedit As System.Windows.Forms.Button
    Friend WithEvents cmdadd As System.Windows.Forms.Button
    Friend WithEvents flx As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents butsave As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class

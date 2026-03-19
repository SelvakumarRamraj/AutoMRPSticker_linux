<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmcolormaster
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
        Me.fd = New System.Windows.Forms.OpenFileDialog
        Me.sd = New System.Windows.Forms.SaveFileDialog
        Me.dv = New System.Windows.Forms.DataGridView
        Me.txtcolcode = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdclear = New System.Windows.Forms.Button
        Me.CmdExit = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmddisp = New System.Windows.Forms.Button
        Me.cmddel = New System.Windows.Forms.Button
        Me.cmdedit = New System.Windows.Forms.Button
        Me.cmdadd = New System.Windows.Forms.Button
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.cmddwnload = New System.Windows.Forms.Button
        Me.Label71 = New System.Windows.Forms.Label
        Me.cmdexcel = New System.Windows.Forms.Button
        CType(Me.dv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fd
        '
        Me.fd.FileName = "OpenFileDialog1"
        '
        'sd
        '
        '
        'dv
        '
        Me.dv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dv.Location = New System.Drawing.Point(27, 64)
        Me.dv.Name = "dv"
        Me.dv.Size = New System.Drawing.Size(906, 414)
        Me.dv.TabIndex = 0
        '
        'txtcolcode
        '
        Me.txtcolcode.Location = New System.Drawing.Point(117, 13)
        Me.txtcolcode.Name = "txtcolcode"
        Me.txtcolcode.Size = New System.Drawing.Size(154, 20)
        Me.txtcolcode.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(52, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Color Code"
        '
        'cmdclear
        '
        Me.cmdclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.Location = New System.Drawing.Point(806, 10)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(75, 23)
        Me.cmdclear.TabIndex = 70
        Me.cmdclear.Text = "&Clear"
        Me.cmdclear.UseVisualStyleBackColor = True
        '
        'CmdExit
        '
        Me.CmdExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdExit.Location = New System.Drawing.Point(887, 10)
        Me.CmdExit.Name = "CmdExit"
        Me.CmdExit.Size = New System.Drawing.Size(75, 23)
        Me.CmdExit.TabIndex = 69
        Me.CmdExit.Text = "E&xit"
        Me.CmdExit.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSave.Location = New System.Drawing.Point(725, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 68
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmddisp
        '
        Me.cmddisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddisp.Location = New System.Drawing.Point(644, 10)
        Me.cmddisp.Name = "cmddisp"
        Me.cmddisp.Size = New System.Drawing.Size(75, 23)
        Me.cmddisp.TabIndex = 67
        Me.cmddisp.Tag = "4"
        Me.cmddisp.Text = "D&isplay"
        Me.cmddisp.UseVisualStyleBackColor = True
        '
        'cmddel
        '
        Me.cmddel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddel.Location = New System.Drawing.Point(644, 41)
        Me.cmddel.Name = "cmddel"
        Me.cmddel.Size = New System.Drawing.Size(75, 23)
        Me.cmddel.TabIndex = 66
        Me.cmddel.Tag = "3"
        Me.cmddel.Text = "&Del"
        Me.cmddel.UseVisualStyleBackColor = True
        '
        'cmdedit
        '
        Me.cmdedit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdedit.Location = New System.Drawing.Point(482, 10)
        Me.cmdedit.Name = "cmdedit"
        Me.cmdedit.Size = New System.Drawing.Size(75, 23)
        Me.cmdedit.TabIndex = 65
        Me.cmdedit.Tag = "2"
        Me.cmdedit.Text = "&Edit"
        Me.cmdedit.UseVisualStyleBackColor = True
        '
        'cmdadd
        '
        Me.cmdadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdadd.Location = New System.Drawing.Point(401, 10)
        Me.cmdadd.Name = "cmdadd"
        Me.cmdadd.Size = New System.Drawing.Size(75, 23)
        Me.cmdadd.TabIndex = 64
        Me.cmdadd.Tag = "1"
        Me.cmdadd.Text = "&Add"
        Me.cmdadd.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox2.Location = New System.Drawing.Point(961, 64)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(219, 189)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 71
        Me.PictureBox2.TabStop = False
        '
        'cmddwnload
        '
        Me.cmddwnload.Location = New System.Drawing.Point(1000, 259)
        Me.cmddwnload.Name = "cmddwnload"
        Me.cmddwnload.Size = New System.Drawing.Size(135, 23)
        Me.cmddwnload.TabIndex = 72
        Me.cmddwnload.Text = "Image Download"
        Me.cmddwnload.UseVisualStyleBackColor = True
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.ForeColor = System.Drawing.Color.Maroon
        Me.Label71.Location = New System.Drawing.Point(37, 495)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(436, 13)
        Me.Label71.TabIndex = 73
        Me.Label71.Text = "F2-- Add Row    F9-Remove Row             Mouse Right Click to Zoom Image"
        '
        'cmdexcel
        '
        Me.cmdexcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexcel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdexcel.Location = New System.Drawing.Point(725, 41)
        Me.cmdexcel.Name = "cmdexcel"
        Me.cmdexcel.Size = New System.Drawing.Size(75, 23)
        Me.cmdexcel.TabIndex = 74
        Me.cmdexcel.Text = "&Excel"
        Me.cmdexcel.UseVisualStyleBackColor = True
        '
        'Frmcolormaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1208, 698)
        Me.Controls.Add(Me.cmdexcel)
        Me.Controls.Add(Me.Label71)
        Me.Controls.Add(Me.cmddwnload)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.cmdclear)
        Me.Controls.Add(Me.CmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmddisp)
        Me.Controls.Add(Me.cmddel)
        Me.Controls.Add(Me.cmdedit)
        Me.Controls.Add(Me.cmdadd)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtcolcode)
        Me.Controls.Add(Me.dv)
        Me.Name = "Frmcolormaster"
        Me.Text = "Frmcolormaster"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents fd As System.Windows.Forms.OpenFileDialog
    Friend WithEvents sd As System.Windows.Forms.SaveFileDialog
    Friend WithEvents dv As System.Windows.Forms.DataGridView
    Friend WithEvents txtcolcode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdclear As System.Windows.Forms.Button
    Friend WithEvents CmdExit As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmddisp As System.Windows.Forms.Button
    Friend WithEvents cmddel As System.Windows.Forms.Button
    Friend WithEvents cmdedit As System.Windows.Forms.Button
    Friend WithEvents cmdadd As System.Windows.Forms.Button
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents cmddwnload As System.Windows.Forms.Button
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents cmdexcel As System.Windows.Forms.Button
End Class

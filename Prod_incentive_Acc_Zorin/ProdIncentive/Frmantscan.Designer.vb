<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmantscan
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
        Me.cmdclear = New System.Windows.Forms.Button
        Me.cmdprint = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        Me.cmdsave = New System.Windows.Forms.Button
        Me.cmddisp = New System.Windows.Forms.Button
        Me.cmddel = New System.Windows.Forms.Button
        Me.cmdedit = New System.Windows.Forms.Button
        Me.cmdadd = New System.Windows.Forms.Button
        Me.txtno = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.mskdate = New System.Windows.Forms.MaskedTextBox
        Me.cmbyr = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmblinefr = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbprocessfr = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.cmbprocessto = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmblineto = New System.Windows.Forms.ComboBox
        Me.dv = New System.Windows.Forms.DataGridView
        Me.txtcutno = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.dv1 = New System.Windows.Forms.DataGridView
        Me.lbltotqty = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmbtype = New System.Windows.Forms.ComboBox
        Me.txtdelno = New System.Windows.Forms.TextBox
        Me.txtlineid = New System.Windows.Forms.TextBox
        CType(Me.dv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dv1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdclear
        '
        Me.cmdclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.Location = New System.Drawing.Point(856, 57)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(56, 29)
        Me.cmdclear.TabIndex = 46
        Me.cmdclear.Tag = "14"
        Me.cmdclear.Text = "Clear"
        Me.cmdclear.UseVisualStyleBackColor = True
        '
        'cmdprint
        '
        Me.cmdprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdprint.Location = New System.Drawing.Point(948, 102)
        Me.cmdprint.Name = "cmdprint"
        Me.cmdprint.Size = New System.Drawing.Size(56, 29)
        Me.cmdprint.TabIndex = 45
        Me.cmdprint.Tag = "16"
        Me.cmdprint.Text = "Print"
        Me.cmdprint.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.Location = New System.Drawing.Point(916, 57)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(56, 29)
        Me.cmdexit.TabIndex = 44
        Me.cmdexit.Tag = "15"
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'cmdsave
        '
        Me.cmdsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsave.Location = New System.Drawing.Point(797, 57)
        Me.cmdsave.Name = "cmdsave"
        Me.cmdsave.Size = New System.Drawing.Size(56, 29)
        Me.cmdsave.TabIndex = 43
        Me.cmdsave.Tag = "13"
        Me.cmdsave.Text = "Save"
        Me.cmdsave.UseVisualStyleBackColor = True
        '
        'cmddisp
        '
        Me.cmddisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddisp.Location = New System.Drawing.Point(739, 57)
        Me.cmddisp.Name = "cmddisp"
        Me.cmddisp.Size = New System.Drawing.Size(56, 29)
        Me.cmddisp.TabIndex = 42
        Me.cmddisp.Tag = "12"
        Me.cmddisp.Text = "Display"
        Me.cmddisp.UseVisualStyleBackColor = True
        '
        'cmddel
        '
        Me.cmddel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddel.Location = New System.Drawing.Point(682, 57)
        Me.cmddel.Name = "cmddel"
        Me.cmddel.Size = New System.Drawing.Size(56, 29)
        Me.cmddel.TabIndex = 41
        Me.cmddel.Tag = "11"
        Me.cmddel.Text = "Delete"
        Me.cmddel.UseVisualStyleBackColor = True
        '
        'cmdedit
        '
        Me.cmdedit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdedit.Location = New System.Drawing.Point(624, 57)
        Me.cmdedit.Name = "cmdedit"
        Me.cmdedit.Size = New System.Drawing.Size(56, 29)
        Me.cmdedit.TabIndex = 40
        Me.cmdedit.Tag = "10"
        Me.cmdedit.Text = "Edit"
        Me.cmdedit.UseVisualStyleBackColor = True
        '
        'cmdadd
        '
        Me.cmdadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdadd.Location = New System.Drawing.Point(567, 57)
        Me.cmdadd.Name = "cmdadd"
        Me.cmdadd.Size = New System.Drawing.Size(56, 29)
        Me.cmdadd.TabIndex = 39
        Me.cmdadd.Tag = "0"
        Me.cmdadd.Text = "New"
        Me.cmdadd.UseVisualStyleBackColor = True
        '
        'txtno
        '
        Me.txtno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtno.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtno.Location = New System.Drawing.Point(299, 11)
        Me.txtno.Name = "txtno"
        Me.txtno.Size = New System.Drawing.Size(160, 20)
        Me.txtno.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(242, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 20)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "DocNum"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(465, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 18)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Date"
        '
        'mskdate
        '
        Me.mskdate.Location = New System.Drawing.Point(514, 9)
        Me.mskdate.Mask = "##-##-####"
        Me.mskdate.Name = "mskdate"
        Me.mskdate.Size = New System.Drawing.Size(90, 20)
        Me.mskdate.TabIndex = 2
        '
        'cmbyr
        '
        Me.cmbyr.FormattingEnabled = True
        Me.cmbyr.Location = New System.Drawing.Point(115, 11)
        Me.cmbyr.Name = "cmbyr"
        Me.cmbyr.Size = New System.Drawing.Size(121, 21)
        Me.cmbyr.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(17, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 20)
        Me.Label3.TabIndex = 48
        Me.Label3.Text = "Posting Period"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(356, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 23)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "Line No"
        '
        'cmblinefr
        '
        Me.cmblinefr.FormattingEnabled = True
        Me.cmblinefr.Location = New System.Drawing.Point(440, 41)
        Me.cmblinefr.Name = "cmblinefr"
        Me.cmblinefr.Size = New System.Drawing.Size(121, 21)
        Me.cmblinefr.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(17, 44)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 23)
        Me.Label5.TabIndex = 52
        Me.Label5.Text = "Process From"
        '
        'cmbprocessfr
        '
        Me.cmbprocessfr.FormattingEnabled = True
        Me.cmbprocessfr.Location = New System.Drawing.Point(115, 43)
        Me.cmbprocessfr.Name = "cmbprocessfr"
        Me.cmbprocessfr.Size = New System.Drawing.Size(224, 21)
        Me.cmbprocessfr.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 71)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 23)
        Me.Label6.TabIndex = 56
        Me.Label6.Text = "Process To"
        '
        'cmbprocessto
        '
        Me.cmbprocessto.FormattingEnabled = True
        Me.cmbprocessto.Location = New System.Drawing.Point(115, 70)
        Me.cmbprocessto.Name = "cmbprocessto"
        Me.cmbprocessto.Size = New System.Drawing.Size(224, 21)
        Me.cmbprocessto.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(356, 68)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 23)
        Me.Label7.TabIndex = 54
        Me.Label7.Text = "Line No To"
        '
        'cmblineto
        '
        Me.cmblineto.FormattingEnabled = True
        Me.cmblineto.Location = New System.Drawing.Point(440, 68)
        Me.cmblineto.Name = "cmblineto"
        Me.cmblineto.Size = New System.Drawing.Size(121, 21)
        Me.cmblineto.TabIndex = 7
        '
        'dv
        '
        Me.dv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dv.Location = New System.Drawing.Point(30, 137)
        Me.dv.Name = "dv"
        Me.dv.Size = New System.Drawing.Size(954, 229)
        Me.dv.TabIndex = 57
        '
        'txtcutno
        '
        Me.txtcutno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcutno.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtcutno.Location = New System.Drawing.Point(84, 111)
        Me.txtcutno.Name = "txtcutno"
        Me.txtcutno.Size = New System.Drawing.Size(266, 20)
        Me.txtcutno.TabIndex = 8
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(35, 114)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 17)
        Me.Label8.TabIndex = 59
        Me.Label8.Text = "Cutno"
        '
        'dv1
        '
        Me.dv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dv1.Location = New System.Drawing.Point(30, 380)
        Me.dv1.Name = "dv1"
        Me.dv1.Size = New System.Drawing.Size(876, 150)
        Me.dv1.TabIndex = 60
        '
        'lbltotqty
        '
        Me.lbltotqty.Location = New System.Drawing.Point(724, 533)
        Me.lbltotqty.Name = "lbltotqty"
        Me.lbltotqty.Size = New System.Drawing.Size(147, 17)
        Me.lbltotqty.TabIndex = 61
        Me.lbltotqty.Text = "0"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(615, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(50, 20)
        Me.Label9.TabIndex = 63
        Me.Label9.Text = "Type"
        '
        'cmbtype
        '
        Me.cmbtype.FormattingEnabled = True
        Me.cmbtype.Location = New System.Drawing.Point(671, 8)
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.Size = New System.Drawing.Size(121, 21)
        Me.cmbtype.TabIndex = 62
        '
        'txtdelno
        '
        Me.txtdelno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdelno.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtdelno.Location = New System.Drawing.Point(356, 111)
        Me.txtdelno.Name = "txtdelno"
        Me.txtdelno.Size = New System.Drawing.Size(160, 20)
        Me.txtdelno.TabIndex = 64
        '
        'txtlineid
        '
        Me.txtlineid.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlineid.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtlineid.Location = New System.Drawing.Point(522, 114)
        Me.txtlineid.Name = "txtlineid"
        Me.txtlineid.Size = New System.Drawing.Size(158, 20)
        Me.txtlineid.TabIndex = 65
        '
        'Frmantscan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 698)
        Me.Controls.Add(Me.txtlineid)
        Me.Controls.Add(Me.txtdelno)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cmbtype)
        Me.Controls.Add(Me.lbltotqty)
        Me.Controls.Add(Me.dv1)
        Me.Controls.Add(Me.txtcutno)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.dv)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbprocessto)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmblineto)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbprocessfr)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmblinefr)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbyr)
        Me.Controls.Add(Me.cmdclear)
        Me.Controls.Add(Me.cmdprint)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdsave)
        Me.Controls.Add(Me.cmddisp)
        Me.Controls.Add(Me.cmddel)
        Me.Controls.Add(Me.cmdedit)
        Me.Controls.Add(Me.cmdadd)
        Me.Controls.Add(Me.txtno)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.mskdate)
        Me.Name = "Frmantscan"
        Me.Text = "Frmantscan"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdclear As System.Windows.Forms.Button
    Friend WithEvents cmdprint As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdsave As System.Windows.Forms.Button
    Friend WithEvents cmddisp As System.Windows.Forms.Button
    Friend WithEvents cmddel As System.Windows.Forms.Button
    Friend WithEvents cmdedit As System.Windows.Forms.Button
    Friend WithEvents cmdadd As System.Windows.Forms.Button
    Friend WithEvents txtno As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mskdate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmbyr As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmblinefr As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbprocessfr As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbprocessto As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmblineto As System.Windows.Forms.ComboBox
    Friend WithEvents dv As System.Windows.Forms.DataGridView
    Friend WithEvents txtcutno As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dv1 As System.Windows.Forms.DataGridView
    Friend WithEvents lbltotqty As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbtype As System.Windows.Forms.ComboBox
    Friend WithEvents txtdelno As System.Windows.Forms.TextBox
    Friend WithEvents txtlineid As System.Windows.Forms.TextBox
End Class

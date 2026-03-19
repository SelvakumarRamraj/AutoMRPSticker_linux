<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmlineprodconsolidaterep
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.mskdateto = New System.Windows.Forms.MaskedTextBox()
        Me.dg = New System.Windows.Forms.DataGridView()
        Me.cmdexcel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.mskdatefr = New System.Windows.Forms.MaskedTextBox()
        Me.cmdclear = New System.Windows.Forms.Button()
        Me.CmdExit = New System.Windows.Forms.Button()
        Me.cmddisp = New System.Windows.Forms.Button()
        Me.chkamt = New System.Windows.Forms.CheckBox()
        Me.chkana = New System.Windows.Forms.CheckBox()
        Me.txtlineno = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtempid = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Chklinecooli = New System.Windows.Forms.CheckBox()
        Me.Chkot = New System.Windows.Forms.CheckBox()
        Me.txttime = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(127, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 14)
        Me.Label2.TabIndex = 97
        Me.Label2.Text = "To"
        '
        'mskdateto
        '
        Me.mskdateto.Location = New System.Drawing.Point(155, 19)
        Me.mskdateto.Mask = "##-##-####"
        Me.mskdateto.Name = "mskdateto"
        Me.mskdateto.Size = New System.Drawing.Size(64, 20)
        Me.mskdateto.TabIndex = 90
        '
        'dg
        '
        Me.dg.AllowUserToAddRows = False
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Location = New System.Drawing.Point(12, 70)
        Me.dg.Name = "dg"
        Me.dg.RowHeadersVisible = False
        Me.dg.Size = New System.Drawing.Size(1244, 542)
        Me.dg.TabIndex = 96
        Me.dg.VirtualMode = True
        '
        'cmdexcel
        '
        Me.cmdexcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexcel.Location = New System.Drawing.Point(780, 22)
        Me.cmdexcel.Name = "cmdexcel"
        Me.cmdexcel.Size = New System.Drawing.Size(60, 23)
        Me.cmdexcel.TabIndex = 95
        Me.cmdexcel.Tag = "6"
        Me.cmdexcel.Text = "&Excel"
        Me.cmdexcel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 14)
        Me.Label1.TabIndex = 94
        Me.Label1.Text = "Date"
        '
        'mskdatefr
        '
        Me.mskdatefr.Location = New System.Drawing.Point(60, 19)
        Me.mskdatefr.Mask = "##-##-####"
        Me.mskdatefr.Name = "mskdatefr"
        Me.mskdatefr.Size = New System.Drawing.Size(64, 20)
        Me.mskdatefr.TabIndex = 89
        '
        'cmdclear
        '
        Me.cmdclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.Location = New System.Drawing.Point(846, 22)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(75, 23)
        Me.cmdclear.TabIndex = 91
        Me.cmdclear.Text = "&Clear"
        Me.cmdclear.UseVisualStyleBackColor = True
        '
        'CmdExit
        '
        Me.CmdExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdExit.Location = New System.Drawing.Point(927, 21)
        Me.CmdExit.Name = "CmdExit"
        Me.CmdExit.Size = New System.Drawing.Size(75, 23)
        Me.CmdExit.TabIndex = 92
        Me.CmdExit.Text = "E&xit"
        Me.CmdExit.UseVisualStyleBackColor = True
        '
        'cmddisp
        '
        Me.cmddisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddisp.Location = New System.Drawing.Point(699, 22)
        Me.cmddisp.Name = "cmddisp"
        Me.cmddisp.Size = New System.Drawing.Size(75, 23)
        Me.cmddisp.TabIndex = 93
        Me.cmddisp.Tag = "5"
        Me.cmddisp.Text = "D&isplay"
        Me.cmddisp.UseVisualStyleBackColor = True
        '
        'chkamt
        '
        Me.chkamt.AutoSize = True
        Me.chkamt.Location = New System.Drawing.Point(251, 21)
        Me.chkamt.Name = "chkamt"
        Me.chkamt.Size = New System.Drawing.Size(76, 17)
        Me.chkamt.TabIndex = 98
        Me.chkamt.Text = "Salarywise"
        Me.chkamt.UseVisualStyleBackColor = True
        '
        'chkana
        '
        Me.chkana.AutoSize = True
        Me.chkana.Location = New System.Drawing.Point(351, 19)
        Me.chkana.Name = "chkana"
        Me.chkana.Size = New System.Drawing.Size(64, 17)
        Me.chkana.TabIndex = 99
        Me.chkana.Text = "Analysis"
        Me.chkana.UseVisualStyleBackColor = True
        '
        'txtlineno
        '
        Me.txtlineno.Location = New System.Drawing.Point(48, 5)
        Me.txtlineno.Name = "txtlineno"
        Me.txtlineno.Size = New System.Drawing.Size(100, 20)
        Me.txtlineno.TabIndex = 100
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 101
        Me.Label3.Text = "Lineno"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtempid)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtlineno)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(421, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(241, 52)
        Me.Panel1.TabIndex = 102
        Me.Panel1.Visible = False
        '
        'txtempid
        '
        Me.txtempid.Location = New System.Drawing.Point(48, 29)
        Me.txtempid.Name = "txtempid"
        Me.txtempid.Size = New System.Drawing.Size(157, 20)
        Me.txtempid.TabIndex = 102
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 103
        Me.Label4.Text = "EmpId"
        '
        'Chklinecooli
        '
        Me.Chklinecooli.AutoSize = True
        Me.Chklinecooli.Location = New System.Drawing.Point(252, 44)
        Me.Chklinecooli.Name = "Chklinecooli"
        Me.Chklinecooli.Size = New System.Drawing.Size(90, 17)
        Me.Chklinecooli.TabIndex = 103
        Me.Chklinecooli.Text = "Cooli Analysis"
        Me.Chklinecooli.UseVisualStyleBackColor = True
        '
        'Chkot
        '
        Me.Chkot.AutoSize = True
        Me.Chkot.Location = New System.Drawing.Point(699, 51)
        Me.Chkot.Name = "Chkot"
        Me.Chkot.Size = New System.Drawing.Size(129, 17)
        Me.Chkot.TabIndex = 104
        Me.Chkot.Text = "Over Time Production"
        Me.Chkot.UseVisualStyleBackColor = True
        '
        'txttime
        '
        Me.txttime.Location = New System.Drawing.Point(975, 48)
        Me.txttime.Name = "txttime"
        Me.txttime.Size = New System.Drawing.Size(88, 20)
        Me.txttime.TabIndex = 105
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(846, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(123, 13)
        Me.Label5.TabIndex = 106
        Me.Label5.Text = "OT Start Time 24 Format"
        '
        'frmlineprodconsolidaterep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1258, 698)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txttime)
        Me.Controls.Add(Me.Chkot)
        Me.Controls.Add(Me.Chklinecooli)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.chkana)
        Me.Controls.Add(Me.chkamt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.mskdateto)
        Me.Controls.Add(Me.dg)
        Me.Controls.Add(Me.cmdexcel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.mskdatefr)
        Me.Controls.Add(Me.cmdclear)
        Me.Controls.Add(Me.CmdExit)
        Me.Controls.Add(Me.cmddisp)
        Me.Name = "frmlineprodconsolidaterep"
        Me.Text = "frmlineprodconsolidaterep"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mskdateto As System.Windows.Forms.MaskedTextBox
    Friend WithEvents dg As System.Windows.Forms.DataGridView
    Friend WithEvents cmdexcel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mskdatefr As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmdclear As System.Windows.Forms.Button
    Friend WithEvents CmdExit As System.Windows.Forms.Button
    Friend WithEvents cmddisp As System.Windows.Forms.Button
    Friend WithEvents chkamt As System.Windows.Forms.CheckBox
    Friend WithEvents chkana As System.Windows.Forms.CheckBox
    Friend WithEvents txtlineno As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtempid As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Chklinecooli As System.Windows.Forms.CheckBox
    Friend WithEvents Chkot As System.Windows.Forms.CheckBox
    Friend WithEvents txttime As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class

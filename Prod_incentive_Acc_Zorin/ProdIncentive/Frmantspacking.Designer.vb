<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmantspacking
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmantspacking))
        Me.txtno = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.mskdate = New System.Windows.Forms.MaskedTextBox
        Me.flx = New AxMSFlexGridLib.AxMSFlexGrid
        Me.flxcode = New AxMSFlexGridLib.AxMSFlexGrid
        Me.cmdprint = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        Me.cmdsave = New System.Windows.Forms.Button
        Me.cmddisp = New System.Windows.Forms.Button
        Me.cmddel = New System.Windows.Forms.Button
        Me.cmdedit = New System.Windows.Forms.Button
        Me.cmdadd = New System.Windows.Forms.Button
        Me.view1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.lblqty = New System.Windows.Forms.Label
        Me.lblamt = New System.Windows.Forms.Label
        Me.cmdclear = New System.Windows.Forms.Button
        Me.cmdexcel = New System.Windows.Forms.Button
        Me.txtbno1 = New System.Windows.Forms.TextBox
        Me.txtbno2 = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        CType(Me.flx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtno
        '
        Me.txtno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtno.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtno.Location = New System.Drawing.Point(119, 12)
        Me.txtno.Name = "txtno"
        Me.txtno.Size = New System.Drawing.Size(160, 20)
        Me.txtno.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(44, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 23)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "DocNum"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(291, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 23)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Date"
        '
        'mskdate
        '
        Me.mskdate.Location = New System.Drawing.Point(342, 12)
        Me.mskdate.Mask = "##-##-####"
        Me.mskdate.Name = "mskdate"
        Me.mskdate.Size = New System.Drawing.Size(90, 20)
        Me.mskdate.TabIndex = 5
        '
        'flx
        '
        Me.flx.Location = New System.Drawing.Point(12, 41)
        Me.flx.Name = "flx"
        Me.flx.OcxState = CType(resources.GetObject("flx.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flx.Size = New System.Drawing.Size(679, 312)
        Me.flx.TabIndex = 7
        '
        'flxcode
        '
        Me.flxcode.Location = New System.Drawing.Point(80, 97)
        Me.flxcode.Name = "flxcode"
        Me.flxcode.OcxState = CType(resources.GetObject("flxcode.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxcode.Size = New System.Drawing.Size(578, 226)
        Me.flxcode.TabIndex = 8
        Me.flxcode.Visible = False
        '
        'cmdprint
        '
        Me.cmdprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdprint.Location = New System.Drawing.Point(704, 47)
        Me.cmdprint.Name = "cmdprint"
        Me.cmdprint.Size = New System.Drawing.Size(56, 29)
        Me.cmdprint.TabIndex = 30
        Me.cmdprint.Tag = "11"
        Me.cmdprint.Text = "Print"
        Me.cmdprint.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.Location = New System.Drawing.Point(837, 12)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(56, 29)
        Me.cmdexit.TabIndex = 29
        Me.cmdexit.Tag = "13"
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'cmdsave
        '
        Me.cmdsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsave.Location = New System.Drawing.Point(704, 12)
        Me.cmdsave.Name = "cmdsave"
        Me.cmdsave.Size = New System.Drawing.Size(56, 29)
        Me.cmdsave.TabIndex = 28
        Me.cmdsave.Tag = "12"
        Me.cmdsave.Text = "Save"
        Me.cmdsave.UseVisualStyleBackColor = True
        '
        'cmddisp
        '
        Me.cmddisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddisp.Location = New System.Drawing.Point(642, 9)
        Me.cmddisp.Name = "cmddisp"
        Me.cmddisp.Size = New System.Drawing.Size(56, 29)
        Me.cmddisp.TabIndex = 27
        Me.cmddisp.Tag = "4"
        Me.cmddisp.Text = "Display"
        Me.cmddisp.UseVisualStyleBackColor = True
        '
        'cmddel
        '
        Me.cmddel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddel.Location = New System.Drawing.Point(580, 9)
        Me.cmddel.Name = "cmddel"
        Me.cmddel.Size = New System.Drawing.Size(56, 29)
        Me.cmddel.TabIndex = 26
        Me.cmddel.Tag = "3"
        Me.cmddel.Text = "Delete"
        Me.cmddel.UseVisualStyleBackColor = True
        '
        'cmdedit
        '
        Me.cmdedit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdedit.Location = New System.Drawing.Point(518, 9)
        Me.cmdedit.Name = "cmdedit"
        Me.cmdedit.Size = New System.Drawing.Size(56, 29)
        Me.cmdedit.TabIndex = 25
        Me.cmdedit.Tag = "2"
        Me.cmdedit.Text = "Edit"
        Me.cmdedit.UseVisualStyleBackColor = True
        '
        'cmdadd
        '
        Me.cmdadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdadd.Location = New System.Drawing.Point(456, 9)
        Me.cmdadd.Name = "cmdadd"
        Me.cmdadd.Size = New System.Drawing.Size(56, 29)
        Me.cmdadd.TabIndex = 24
        Me.cmdadd.Tag = "1"
        Me.cmdadd.Text = "New"
        Me.cmdadd.UseVisualStyleBackColor = True
        '
        'view1
        '
        Me.view1.ActiveViewIndex = -1
        Me.view1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.view1.Cursor = System.Windows.Forms.Cursors.Default
        Me.view1.Location = New System.Drawing.Point(12, 376)
        Me.view1.Name = "view1"
        Me.view1.SelectionFormula = ""
        Me.view1.Size = New System.Drawing.Size(810, 277)
        Me.view1.TabIndex = 31
        Me.view1.ViewTimeSelectionFormula = ""
        '
        'lblqty
        '
        Me.lblqty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblqty.Location = New System.Drawing.Point(444, 356)
        Me.lblqty.Name = "lblqty"
        Me.lblqty.Size = New System.Drawing.Size(100, 17)
        Me.lblqty.TabIndex = 32
        Me.lblqty.Text = "0"
        '
        'lblamt
        '
        Me.lblamt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblamt.Location = New System.Drawing.Point(570, 356)
        Me.lblamt.Name = "lblamt"
        Me.lblamt.Size = New System.Drawing.Size(100, 17)
        Me.lblamt.TabIndex = 33
        Me.lblamt.Text = "0"
        '
        'cmdclear
        '
        Me.cmdclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdclear.Location = New System.Drawing.Point(766, 12)
        Me.cmdclear.Name = "cmdclear"
        Me.cmdclear.Size = New System.Drawing.Size(56, 29)
        Me.cmdclear.TabIndex = 34
        Me.cmdclear.Tag = "12"
        Me.cmdclear.Text = "Clear"
        Me.cmdclear.UseVisualStyleBackColor = True
        '
        'cmdexcel
        '
        Me.cmdexcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexcel.Location = New System.Drawing.Point(812, 124)
        Me.cmdexcel.Name = "cmdexcel"
        Me.cmdexcel.Size = New System.Drawing.Size(91, 26)
        Me.cmdexcel.TabIndex = 35
        Me.cmdexcel.Text = "Excel Export"
        Me.cmdexcel.UseVisualStyleBackColor = True
        '
        'txtbno1
        '
        Me.txtbno1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbno1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtbno1.Location = New System.Drawing.Point(757, 97)
        Me.txtbno1.Name = "txtbno1"
        Me.txtbno1.Size = New System.Drawing.Size(94, 20)
        Me.txtbno1.TabIndex = 36
        '
        'txtbno2
        '
        Me.txtbno2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbno2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtbno2.Location = New System.Drawing.Point(882, 97)
        Me.txtbno2.Name = "txtbno2"
        Me.txtbno2.Size = New System.Drawing.Size(85, 20)
        Me.txtbno2.TabIndex = 37
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(856, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 13)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "To"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(712, 100)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "B.No."
        '
        'frmantspacking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 698)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtbno2)
        Me.Controls.Add(Me.txtbno1)
        Me.Controls.Add(Me.cmdexcel)
        Me.Controls.Add(Me.cmdclear)
        Me.Controls.Add(Me.lblamt)
        Me.Controls.Add(Me.lblqty)
        Me.Controls.Add(Me.view1)
        Me.Controls.Add(Me.cmdprint)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdsave)
        Me.Controls.Add(Me.cmddisp)
        Me.Controls.Add(Me.cmddel)
        Me.Controls.Add(Me.cmdedit)
        Me.Controls.Add(Me.cmdadd)
        Me.Controls.Add(Me.flxcode)
        Me.Controls.Add(Me.flx)
        Me.Controls.Add(Me.txtno)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.mskdate)
        Me.Name = "frmantspacking"
        Me.Text = "Frmantspacking"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.flx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtno As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mskdate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents flx As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents flxcode As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents cmdprint As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdsave As System.Windows.Forms.Button
    Friend WithEvents cmddisp As System.Windows.Forms.Button
    Friend WithEvents cmddel As System.Windows.Forms.Button
    Friend WithEvents cmdedit As System.Windows.Forms.Button
    Friend WithEvents cmdadd As System.Windows.Forms.Button
    Friend WithEvents view1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents lblqty As System.Windows.Forms.Label
    Friend WithEvents lblamt As System.Windows.Forms.Label
    Friend WithEvents cmdclear As System.Windows.Forms.Button
    Friend WithEvents cmdexcel As System.Windows.Forms.Button
    Friend WithEvents txtbno1 As System.Windows.Forms.TextBox
    Friend WithEvents txtbno2 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmorderprn
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frmorderprn))
        Me.mskdate = New System.Windows.Forms.MaskedTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtno = New System.Windows.Forms.TextBox
        Me.cmbparty = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.mskorddate = New System.Windows.Forms.MaskedTextBox
        Me.txtordno = New System.Windows.Forms.TextBox
        Me.txtprnno = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtremark = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblcardcode = New System.Windows.Forms.Label
        Me.lblagent = New System.Windows.Forms.Label
        Me.cmdadd = New System.Windows.Forms.Button
        Me.cmdedit = New System.Windows.Forms.Button
        Me.cmddel = New System.Windows.Forms.Button
        Me.flx = New AxMSFlexGridLib.AxMSFlexGrid
        Me.cmddisp = New System.Windows.Forms.Button
        Me.cmdsave = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        Me.cmdprint = New System.Windows.Forms.Button
        Me.view1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.cmddispall = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label9 = New System.Windows.Forms.Label
        Me.mskdateto = New System.Windows.Forms.MaskedTextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.mskdatefr = New System.Windows.Forms.MaskedTextBox
        CType(Me.flx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'mskdate
        '
        Me.mskdate.Location = New System.Drawing.Point(354, 55)
        Me.mskdate.Mask = "##-##-####"
        Me.mskdate.Name = "mskdate"
        Me.mskdate.Size = New System.Drawing.Size(90, 20)
        Me.mskdate.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(303, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Date"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(56, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 23)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "DocNum"
        '
        'txtno
        '
        Me.txtno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtno.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtno.Location = New System.Drawing.Point(131, 55)
        Me.txtno.Name = "txtno"
        Me.txtno.Size = New System.Drawing.Size(160, 20)
        Me.txtno.TabIndex = 1
        '
        'cmbparty
        '
        Me.cmbparty.FormattingEnabled = True
        Me.cmbparty.Location = New System.Drawing.Point(48, 131)
        Me.cmbparty.Name = "cmbparty"
        Me.cmbparty.Size = New System.Drawing.Size(329, 21)
        Me.cmbparty.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(144, 105)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 23)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Party Name"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(436, 105)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 23)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Ord.No"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(538, 105)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 23)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Ord.Date"
        '
        'mskorddate
        '
        Me.mskorddate.Location = New System.Drawing.Point(528, 132)
        Me.mskorddate.Mask = "##-##-####"
        Me.mskorddate.Name = "mskorddate"
        Me.mskorddate.Size = New System.Drawing.Size(90, 20)
        Me.mskorddate.TabIndex = 5
        '
        'txtordno
        '
        Me.txtordno.Location = New System.Drawing.Point(383, 132)
        Me.txtordno.Name = "txtordno"
        Me.txtordno.Size = New System.Drawing.Size(127, 20)
        Me.txtordno.TabIndex = 4
        '
        'txtprnno
        '
        Me.txtprnno.Location = New System.Drawing.Point(645, 132)
        Me.txtprnno.Name = "txtprnno"
        Me.txtprnno.Size = New System.Drawing.Size(127, 20)
        Me.txtprnno.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(698, 105)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 23)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Print No"
        '
        'txtremark
        '
        Me.txtremark.Location = New System.Drawing.Point(789, 132)
        Me.txtremark.Name = "txtremark"
        Me.txtremark.Size = New System.Drawing.Size(276, 20)
        Me.txtremark.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(842, 105)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 23)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Remark"
        '
        'lblcardcode
        '
        Me.lblcardcode.Location = New System.Drawing.Point(51, 185)
        Me.lblcardcode.Name = "lblcardcode"
        Me.lblcardcode.Size = New System.Drawing.Size(168, 23)
        Me.lblcardcode.TabIndex = 14
        Me.lblcardcode.Text = "L"
        '
        'lblagent
        '
        Me.lblagent.Location = New System.Drawing.Point(264, 185)
        Me.lblagent.Name = "lblagent"
        Me.lblagent.Size = New System.Drawing.Size(168, 23)
        Me.lblagent.TabIndex = 15
        Me.lblagent.Text = "A"
        '
        'cmdadd
        '
        Me.cmdadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdadd.Location = New System.Drawing.Point(494, 175)
        Me.cmdadd.Name = "cmdadd"
        Me.cmdadd.Size = New System.Drawing.Size(75, 33)
        Me.cmdadd.TabIndex = 0
        Me.cmdadd.Tag = "1"
        Me.cmdadd.Text = "New"
        Me.cmdadd.UseVisualStyleBackColor = True
        '
        'cmdedit
        '
        Me.cmdedit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdedit.Location = New System.Drawing.Point(575, 175)
        Me.cmdedit.Name = "cmdedit"
        Me.cmdedit.Size = New System.Drawing.Size(75, 33)
        Me.cmdedit.TabIndex = 17
        Me.cmdedit.Tag = "9"
        Me.cmdedit.Text = "Edit"
        Me.cmdedit.UseVisualStyleBackColor = True
        '
        'cmddel
        '
        Me.cmddel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddel.Location = New System.Drawing.Point(656, 175)
        Me.cmddel.Name = "cmddel"
        Me.cmddel.Size = New System.Drawing.Size(75, 33)
        Me.cmddel.TabIndex = 18
        Me.cmddel.Tag = "10"
        Me.cmddel.Text = "Delete"
        Me.cmddel.UseVisualStyleBackColor = True
        '
        'flx
        '
        Me.flx.Location = New System.Drawing.Point(12, 212)
        Me.flx.Name = "flx"
        Me.flx.OcxState = CType(resources.GetObject("flx.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flx.Size = New System.Drawing.Size(1107, 430)
        Me.flx.TabIndex = 8
        '
        'cmddisp
        '
        Me.cmddisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddisp.Location = New System.Drawing.Point(742, 175)
        Me.cmddisp.Name = "cmddisp"
        Me.cmddisp.Size = New System.Drawing.Size(75, 33)
        Me.cmddisp.TabIndex = 20
        Me.cmddisp.Tag = "11"
        Me.cmddisp.Text = "Display"
        Me.cmddisp.UseVisualStyleBackColor = True
        '
        'cmdsave
        '
        Me.cmdsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsave.Location = New System.Drawing.Point(823, 175)
        Me.cmdsave.Name = "cmdsave"
        Me.cmdsave.Size = New System.Drawing.Size(75, 33)
        Me.cmdsave.TabIndex = 21
        Me.cmdsave.Tag = "12"
        Me.cmdsave.Text = "Save"
        Me.cmdsave.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.Location = New System.Drawing.Point(904, 175)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(75, 33)
        Me.cmdexit.TabIndex = 22
        Me.cmdexit.Tag = "13"
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'cmdprint
        '
        Me.cmdprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdprint.Location = New System.Drawing.Point(990, 175)
        Me.cmdprint.Name = "cmdprint"
        Me.cmdprint.Size = New System.Drawing.Size(75, 33)
        Me.cmdprint.TabIndex = 23
        Me.cmdprint.Tag = "11"
        Me.cmdprint.Text = "Print"
        Me.cmdprint.UseVisualStyleBackColor = True
        '
        'view1
        '
        Me.view1.ActiveViewIndex = -1
        Me.view1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.view1.Location = New System.Drawing.Point(935, 641)
        Me.view1.Name = "view1"
        Me.view1.SelectionFormula = ""
        Me.view1.Size = New System.Drawing.Size(160, 55)
        Me.view1.TabIndex = 26
        Me.view1.ViewTimeSelectionFormula = ""
        Me.view1.Visible = False
        '
        'cmddispall
        '
        Me.cmddispall.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddispall.Location = New System.Drawing.Point(342, 17)
        Me.cmddispall.Name = "cmddispall"
        Me.cmddispall.Size = New System.Drawing.Size(75, 33)
        Me.cmddispall.TabIndex = 31
        Me.cmddispall.Tag = "11"
        Me.cmddispall.Text = "Display All"
        Me.cmddispall.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.mskdateto)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.mskdatefr)
        Me.Panel1.Controls.Add(Me.cmddispall)
        Me.Panel1.Location = New System.Drawing.Point(645, 25)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(420, 67)
        Me.Panel1.TabIndex = 28
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(156, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 17)
        Me.Label9.TabIndex = 31
        Me.Label9.Text = "To"
        '
        'mskdateto
        '
        Me.mskdateto.Location = New System.Drawing.Point(213, 17)
        Me.mskdateto.Mask = "##-##-####"
        Me.mskdateto.Name = "mskdateto"
        Me.mskdateto.Size = New System.Drawing.Size(90, 20)
        Me.mskdateto.TabIndex = 30
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(5, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 23)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "Date"
        '
        'mskdatefr
        '
        Me.mskdatefr.Location = New System.Drawing.Point(56, 17)
        Me.mskdatefr.Mask = "##-##-####"
        Me.mskdatefr.Name = "mskdatefr"
        Me.mskdatefr.Size = New System.Drawing.Size(90, 20)
        Me.mskdatefr.TabIndex = 29
        '
        'Frmorderprn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1143, 698)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.view1)
        Me.Controls.Add(Me.cmdprint)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdsave)
        Me.Controls.Add(Me.cmddisp)
        Me.Controls.Add(Me.flx)
        Me.Controls.Add(Me.cmddel)
        Me.Controls.Add(Me.cmdedit)
        Me.Controls.Add(Me.cmdadd)
        Me.Controls.Add(Me.lblagent)
        Me.Controls.Add(Me.lblcardcode)
        Me.Controls.Add(Me.txtremark)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtprnno)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtordno)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.mskorddate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbparty)
        Me.Controls.Add(Me.txtno)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.mskdate)
        Me.Name = "Frmorderprn"
        Me.Text = "Frmorderprn"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.flx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mskdate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtno As System.Windows.Forms.TextBox
    Friend WithEvents cmbparty As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents mskorddate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtordno As System.Windows.Forms.TextBox
    Friend WithEvents txtprnno As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtremark As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblcardcode As System.Windows.Forms.Label
    Friend WithEvents lblagent As System.Windows.Forms.Label
    Friend WithEvents cmdadd As System.Windows.Forms.Button
    Friend WithEvents cmdedit As System.Windows.Forms.Button
    Friend WithEvents cmddel As System.Windows.Forms.Button
    Friend WithEvents flx As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents cmddisp As System.Windows.Forms.Button
    Friend WithEvents cmdsave As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdprint As System.Windows.Forms.Button
    Friend WithEvents view1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents cmddispall As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents mskdateto As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents mskdatefr As System.Windows.Forms.MaskedTextBox
End Class

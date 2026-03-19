<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmcustomercare
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frmcustomercare))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbagent = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbparty = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbbrand = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.mskdatefr = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.mskdateto = New System.Windows.Forms.MaskedTextBox()
        Me.flxsummary = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.flxitemsum = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.flxoutstand = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.flxpymtperf = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.flxitemso = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.flxsalesorder = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.flxsummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxitemsum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxoutstand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxpymtperf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxitemso, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxsalesorder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(12, 256)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1011, 456)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.flxitemsum)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1003, 430)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Party Name"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.flxoutstand)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1003, 430)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Outstanding"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.flxpymtperf)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1003, 430)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Pymt Performance"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.flxitemso)
        Me.TabPage4.Controls.Add(Me.flxsalesorder)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(1003, 430)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Sales Order Pending"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(533, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 17)
        Me.Label5.TabIndex = 207
        Me.Label5.Text = "Area Code"
        '
        'cmbagent
        '
        Me.cmbagent.FormattingEnabled = True
        Me.cmbagent.Location = New System.Drawing.Point(603, 5)
        Me.cmbagent.Name = "cmbagent"
        Me.cmbagent.Size = New System.Drawing.Size(214, 21)
        Me.cmbagent.TabIndex = 206
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(235, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 19)
        Me.Label4.TabIndex = 205
        Me.Label4.Text = "Party"
        '
        'cmbparty
        '
        Me.cmbparty.FormattingEnabled = True
        Me.cmbparty.Location = New System.Drawing.Point(283, 4)
        Me.cmbparty.Name = "cmbparty"
        Me.cmbparty.Size = New System.Drawing.Size(244, 21)
        Me.cmbparty.TabIndex = 204
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(823, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 17)
        Me.Label3.TabIndex = 203
        Me.Label3.Text = "Brand"
        '
        'cmbbrand
        '
        Me.cmbbrand.FormattingEnabled = True
        Me.cmbbrand.Location = New System.Drawing.Point(871, 5)
        Me.cmbbrand.Name = "cmbbrand"
        Me.cmbbrand.Size = New System.Drawing.Size(214, 21)
        Me.cmbbrand.TabIndex = 202
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 20)
        Me.Label2.TabIndex = 201
        Me.Label2.Text = "Date"
        '
        'mskdatefr
        '
        Me.mskdatefr.Location = New System.Drawing.Point(55, 6)
        Me.mskdatefr.Mask = "##-##-####"
        Me.mskdatefr.Name = "mskdatefr"
        Me.mskdatefr.Size = New System.Drawing.Size(72, 20)
        Me.mskdatefr.TabIndex = 200
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(133, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 20)
        Me.Label1.TabIndex = 209
        Me.Label1.Text = "To"
        '
        'mskdateto
        '
        Me.mskdateto.Location = New System.Drawing.Point(164, 6)
        Me.mskdateto.Mask = "##-##-####"
        Me.mskdateto.Name = "mskdateto"
        Me.mskdateto.Size = New System.Drawing.Size(72, 20)
        Me.mskdateto.TabIndex = 208
        '
        'flxsummary
        '
        Me.flxsummary.Location = New System.Drawing.Point(16, 42)
        Me.flxsummary.Name = "flxsummary"
        Me.flxsummary.OcxState = CType(resources.GetObject("flxsummary.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxsummary.Size = New System.Drawing.Size(1007, 208)
        Me.flxsummary.TabIndex = 0
        '
        'flxitemsum
        '
        Me.flxitemsum.Location = New System.Drawing.Point(16, 16)
        Me.flxitemsum.Name = "flxitemsum"
        Me.flxitemsum.OcxState = CType(resources.GetObject("flxitemsum.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxitemsum.Size = New System.Drawing.Size(737, 387)
        Me.flxitemsum.TabIndex = 0
        '
        'flxoutstand
        '
        Me.flxoutstand.Location = New System.Drawing.Point(16, 20)
        Me.flxoutstand.Name = "flxoutstand"
        Me.flxoutstand.OcxState = CType(resources.GetObject("flxoutstand.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxoutstand.Size = New System.Drawing.Size(784, 367)
        Me.flxoutstand.TabIndex = 0
        '
        'flxpymtperf
        '
        Me.flxpymtperf.Location = New System.Drawing.Point(15, 54)
        Me.flxpymtperf.Name = "flxpymtperf"
        Me.flxpymtperf.OcxState = CType(resources.GetObject("flxpymtperf.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxpymtperf.Size = New System.Drawing.Size(681, 318)
        Me.flxpymtperf.TabIndex = 0
        '
        'flxitemso
        '
        Me.flxitemso.Location = New System.Drawing.Point(29, 258)
        Me.flxitemso.Name = "flxitemso"
        Me.flxitemso.OcxState = CType(resources.GetObject("flxitemso.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxitemso.Size = New System.Drawing.Size(787, 152)
        Me.flxitemso.TabIndex = 1
        '
        'flxsalesorder
        '
        Me.flxsalesorder.Location = New System.Drawing.Point(29, 15)
        Me.flxsalesorder.Name = "flxsalesorder"
        Me.flxsalesorder.OcxState = CType(resources.GetObject("flxsalesorder.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxsalesorder.Size = New System.Drawing.Size(787, 220)
        Me.flxsalesorder.TabIndex = 0
        '
        'Frmcustomercare
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1208, 698)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.mskdateto)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbagent)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbparty)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbbrand)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.mskdatefr)
        Me.Controls.Add(Me.flxsummary)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frmcustomercare"
        Me.Text = "Frmcustomercare"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        CType(Me.flxsummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxitemsum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxoutstand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxpymtperf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxitemso, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxsalesorder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents flxsummary As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents flxitemsum As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents flxoutstand As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents flxpymtperf As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents flxsalesorder As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents flxitemso As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbagent As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbparty As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbbrand As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mskdatefr As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mskdateto As System.Windows.Forms.MaskedTextBox
End Class

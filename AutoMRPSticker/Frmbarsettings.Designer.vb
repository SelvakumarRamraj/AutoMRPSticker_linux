<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmbarsettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmbarsettings))
        Me.flx = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.txtno = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.mskdate = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbcomp = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbtype = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbprntype = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbprnon = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtstickcol = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtrow = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtcol = New System.Windows.Forms.TextBox()
        Me.chkactive = New System.Windows.Forms.CheckBox()
        Me.flxcode = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtprint = New System.Windows.Forms.TextBox()
        Me.chkprod = New System.Windows.Forms.CheckBox()
        Me.chktype = New System.Windows.Forms.CheckBox()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.Btndel = New System.Windows.Forms.Button()
        Me.Btndisp = New System.Windows.Forms.Button()
        Me.Btnclear = New System.Windows.Forms.Button()
        Me.Btnsave = New System.Windows.Forms.Button()
        Me.Btnexit = New System.Windows.Forms.Button()
        Me.Btnxlexp = New System.Windows.Forms.Button()
        CType(Me.flx,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.flxcode,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'flx
        '
        Me.flx.AllowDrop = true
        Me.flx.Location = New System.Drawing.Point(35, 163)
        Me.flx.Name = "flx"
        Me.flx.OcxState = CType(resources.GetObject("flx.OcxState"),System.Windows.Forms.AxHost.State)
        Me.flx.Size = New System.Drawing.Size(768, 446)
        Me.flx.TabIndex = 181
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(402, 39)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(32, 20)
        Me.DateTimePicker1.TabIndex = 189
        '
        'txtno
        '
        Me.txtno.Location = New System.Drawing.Point(113, 39)
        Me.txtno.Name = "txtno"
        Me.txtno.Size = New System.Drawing.Size(132, 20)
        Me.txtno.TabIndex = 190
        '
        'label1
        '
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.label1.Location = New System.Drawing.Point(48, 42)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(59, 23)
        Me.label1.TabIndex = 191
        Me.label1.Text = "DocNum"
        '
        'mskdate
        '
        Me.mskdate.Location = New System.Drawing.Point(290, 39)
        Me.mskdate.Mask = "##-##-####"
        Me.mskdate.Name = "mskdate"
        Me.mskdate.Size = New System.Drawing.Size(107, 20)
        Me.mskdate.TabIndex = 192
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.Location = New System.Drawing.Point(251, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 20)
        Me.Label2.TabIndex = 193
        Me.Label2.Text = "Date"
        '
        'cmbcomp
        '
        Me.cmbcomp.FormattingEnabled = true
        Me.cmbcomp.Location = New System.Drawing.Point(513, 33)
        Me.cmbcomp.Name = "cmbcomp"
        Me.cmbcomp.Size = New System.Drawing.Size(214, 21)
        Me.cmbcomp.TabIndex = 194
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label3.Location = New System.Drawing.Point(436, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 23)
        Me.Label3.TabIndex = 195
        Me.Label3.Text = "Company"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label4.Location = New System.Drawing.Point(72, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 23)
        Me.Label4.TabIndex = 197
        Me.Label4.Text = "Type"
        '
        'cmbtype
        '
        Me.cmbtype.FormattingEnabled = true
        Me.cmbtype.Location = New System.Drawing.Point(113, 70)
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.Size = New System.Drawing.Size(214, 21)
        Me.cmbtype.TabIndex = 196
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label5.Location = New System.Drawing.Point(436, 70)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 23)
        Me.Label5.TabIndex = 199
        Me.Label5.Text = "Print Type"
        '
        'cmbprntype
        '
        Me.cmbprntype.FormattingEnabled = true
        Me.cmbprntype.Location = New System.Drawing.Point(513, 70)
        Me.cmbprntype.Name = "cmbprntype"
        Me.cmbprntype.Size = New System.Drawing.Size(214, 21)
        Me.cmbprntype.TabIndex = 198
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label6.Location = New System.Drawing.Point(42, 100)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 23)
        Me.Label6.TabIndex = 201
        Me.Label6.Text = "Print On"
        '
        'cmbprnon
        '
        Me.cmbprnon.FormattingEnabled = true
        Me.cmbprnon.Location = New System.Drawing.Point(113, 97)
        Me.cmbprnon.Name = "cmbprnon"
        Me.cmbprnon.Size = New System.Drawing.Size(214, 21)
        Me.cmbprnon.TabIndex = 200
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label7.Location = New System.Drawing.Point(399, 100)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(102, 23)
        Me.Label7.TabIndex = 203
        Me.Label7.Text = "Sticker Coloumn"
        '
        'txtstickcol
        '
        Me.txtstickcol.Location = New System.Drawing.Point(513, 100)
        Me.txtstickcol.Name = "txtstickcol"
        Me.txtstickcol.Size = New System.Drawing.Size(70, 20)
        Me.txtstickcol.TabIndex = 202
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label8.Location = New System.Drawing.Point(589, 100)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(97, 23)
        Me.Label8.TabIndex = 205
        Me.Label8.Text = "Incr Row Value"
        '
        'txtrow
        '
        Me.txtrow.Location = New System.Drawing.Point(687, 97)
        Me.txtrow.Name = "txtrow"
        Me.txtrow.Size = New System.Drawing.Size(66, 20)
        Me.txtrow.TabIndex = 204
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label9.Location = New System.Drawing.Point(757, 99)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(97, 23)
        Me.Label9.TabIndex = 207
        Me.Label9.Text = "Incr Col Value"
        '
        'txtcol
        '
        Me.txtcol.Location = New System.Drawing.Point(860, 98)
        Me.txtcol.Name = "txtcol"
        Me.txtcol.Size = New System.Drawing.Size(66, 20)
        Me.txtcol.TabIndex = 206
        '
        'chkactive
        '
        Me.chkactive.AutoSize = true
        Me.chkactive.Location = New System.Drawing.Point(113, 125)
        Me.chkactive.Name = "chkactive"
        Me.chkactive.Size = New System.Drawing.Size(56, 17)
        Me.chkactive.TabIndex = 208
        Me.chkactive.Text = "Active"
        Me.chkactive.UseVisualStyleBackColor = true
        '
        'flxcode
        '
        Me.flxcode.AllowDrop = true
        Me.flxcode.Location = New System.Drawing.Point(802, 163)
        Me.flxcode.Name = "flxcode"
        Me.flxcode.OcxState = CType(resources.GetObject("flxcode.OcxState"),System.Windows.Forms.AxHost.State)
        Me.flxcode.Size = New System.Drawing.Size(202, 384)
        Me.flxcode.TabIndex = 209
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label10.Location = New System.Drawing.Point(175, 125)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(51, 18)
        Me.Label10.TabIndex = 211
        Me.Label10.Text = "Printer"
        '
        'txtprint
        '
        Me.txtprint.Location = New System.Drawing.Point(240, 124)
        Me.txtprint.Name = "txtprint"
        Me.txtprint.Size = New System.Drawing.Size(222, 20)
        Me.txtprint.TabIndex = 210
        '
        'chkprod
        '
        Me.chkprod.AutoSize = true
        Me.chkprod.Location = New System.Drawing.Point(751, 34)
        Me.chkprod.Name = "chkprod"
        Me.chkprod.Size = New System.Drawing.Size(77, 17)
        Me.chkprod.TabIndex = 212
        Me.chkprod.Text = "Production"
        Me.chkprod.UseVisualStyleBackColor = true
        '
        'chktype
        '
        Me.chktype.AutoSize = true
        Me.chktype.Location = New System.Drawing.Point(514, 127)
        Me.chktype.Name = "chktype"
        Me.chktype.Size = New System.Drawing.Size(72, 17)
        Me.chktype.TabIndex = 214
        Me.chktype.Text = "ATITHYA"
        Me.chktype.UseVisualStyleBackColor = true
        '
        'BtnAdd
        '
        Me.BtnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.BtnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.BtnAdd.Location = New System.Drawing.Point(40, 624)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(67, 33)
        Me.BtnAdd.TabIndex = 215
        Me.BtnAdd.Tag = "1"
        Me.BtnAdd.Text = "Add"
        Me.BtnAdd.UseVisualStyleBackColor = true
        '
        'BtnEdit
        '
        Me.BtnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.BtnEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.BtnEdit.Location = New System.Drawing.Point(113, 624)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(67, 33)
        Me.BtnEdit.TabIndex = 216
        Me.BtnEdit.Tag = "2"
        Me.BtnEdit.Text = "Edit"
        Me.BtnEdit.UseVisualStyleBackColor = true
        '
        'Btndel
        '
        Me.Btndel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Btndel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Btndel.Location = New System.Drawing.Point(186, 624)
        Me.Btndel.Name = "Btndel"
        Me.Btndel.Size = New System.Drawing.Size(67, 33)
        Me.Btndel.TabIndex = 217
        Me.Btndel.Tag = "3"
        Me.Btndel.Text = "Delete"
        Me.Btndel.UseVisualStyleBackColor = true
        '
        'Btndisp
        '
        Me.Btndisp.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Btndisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Btndisp.Location = New System.Drawing.Point(260, 624)
        Me.Btndisp.Name = "Btndisp"
        Me.Btndisp.Size = New System.Drawing.Size(67, 33)
        Me.Btndisp.TabIndex = 218
        Me.Btndisp.Tag = "4"
        Me.Btndisp.Text = "Display"
        Me.Btndisp.UseVisualStyleBackColor = true
        '
        'Btnclear
        '
        Me.Btnclear.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Btnclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Btnclear.Location = New System.Drawing.Point(333, 624)
        Me.Btnclear.Name = "Btnclear"
        Me.Btnclear.Size = New System.Drawing.Size(67, 33)
        Me.Btnclear.TabIndex = 219
        Me.Btnclear.Text = "Clear"
        Me.Btnclear.UseVisualStyleBackColor = true
        '
        'Btnsave
        '
        Me.Btnsave.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Btnsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Btnsave.Location = New System.Drawing.Point(402, 624)
        Me.Btnsave.Name = "Btnsave"
        Me.Btnsave.Size = New System.Drawing.Size(67, 33)
        Me.Btnsave.TabIndex = 220
        Me.Btnsave.Text = "Save"
        Me.Btnsave.UseVisualStyleBackColor = true
        '
        'Btnexit
        '
        Me.Btnexit.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Btnexit.Location = New System.Drawing.Point(475, 624)
        Me.Btnexit.Name = "Btnexit"
        Me.Btnexit.Size = New System.Drawing.Size(67, 33)
        Me.Btnexit.TabIndex = 221
        Me.Btnexit.Text = "Exit"
        Me.Btnexit.UseVisualStyleBackColor = true
        '
        'Btnxlexp
        '
        Me.Btnxlexp.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Btnxlexp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Btnxlexp.Location = New System.Drawing.Point(548, 624)
        Me.Btnxlexp.Name = "Btnxlexp"
        Me.Btnxlexp.Size = New System.Drawing.Size(71, 38)
        Me.Btnxlexp.TabIndex = 222
        Me.Btnxlexp.Text = "Excel Export"
        Me.Btnxlexp.UseVisualStyleBackColor = true
        '
        'frmbarsettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(1016, 698)
        Me.Controls.Add(Me.Btnxlexp)
        Me.Controls.Add(Me.Btnexit)
        Me.Controls.Add(Me.Btnsave)
        Me.Controls.Add(Me.Btnclear)
        Me.Controls.Add(Me.Btndisp)
        Me.Controls.Add(Me.Btndel)
        Me.Controls.Add(Me.BtnEdit)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.chktype)
        Me.Controls.Add(Me.chkprod)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtprint)
        Me.Controls.Add(Me.flxcode)
        Me.Controls.Add(Me.chkactive)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtcol)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtrow)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtstickcol)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbprnon)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbprntype)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbtype)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbcomp)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.mskdate)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.txtno)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.flx)
        Me.Name = "frmbarsettings"
        Me.Text = "Frmbarsettings"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.flx,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.flxcode,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents flx As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtno As System.Windows.Forms.TextBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents mskdate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbcomp As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbtype As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbprntype As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbprnon As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtstickcol As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtrow As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtcol As System.Windows.Forms.TextBox
    Friend WithEvents chkactive As System.Windows.Forms.CheckBox
    Friend WithEvents flxcode As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtprint As System.Windows.Forms.TextBox
    Friend WithEvents chkprod As System.Windows.Forms.CheckBox
    Friend WithEvents chktype As System.Windows.Forms.CheckBox
    Friend WithEvents BtnAdd As System.Windows.Forms.Button
    Friend WithEvents BtnEdit As System.Windows.Forms.Button
    Friend WithEvents Btndel As System.Windows.Forms.Button
    Friend WithEvents Btndisp As System.Windows.Forms.Button
    Friend WithEvents Btnclear As System.Windows.Forms.Button
    Friend WithEvents Btnsave As System.Windows.Forms.Button
    Friend WithEvents Btnexit As System.Windows.Forms.Button
    Friend WithEvents Btnxlexp As System.Windows.Forms.Button
End Class

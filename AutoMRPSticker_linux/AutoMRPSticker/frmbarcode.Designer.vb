<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmbarcode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmbarcode))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtbno = New System.Windows.Forms.TextBox()
        Me.flx = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.flxc = New AxMSFlexGridLib.AxMSFlexGrid()
        Me.txtcol = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtrow = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtstickcol = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbprnon = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbprntype = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbtype = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbcomp = New System.Windows.Forms.ComboBox()
        Me.txtdocno = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chkmrp = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtport = New System.Windows.Forms.TextBox()
        Me.cmdsel = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtprint = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.chkprndir = New System.Windows.Forms.CheckBox()
        Me.chksr = New System.Windows.Forms.CheckBox()
        Me.cmbyr = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbmont = New System.Windows.Forms.ComboBox()
        Me.lblrec = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbyear = New System.Windows.Forms.ComboBox()
        Me.cmbprinter = New System.Windows.Forms.ComboBox()
        Me.Btndel = New System.Windows.Forms.Button()
        Me.Btnok = New System.Windows.Forms.Button()
        Me.Btnprint = New System.Windows.Forms.Button()
        Me.Btnexit = New System.Windows.Forms.Button()
        CType(Me.flx,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.flxc,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(106, 180)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Bill Number"
        '
        'txtbno
        '
        Me.txtbno.Location = New System.Drawing.Point(197, 175)
        Me.txtbno.Name = "txtbno"
        Me.txtbno.Size = New System.Drawing.Size(100, 20)
        Me.txtbno.TabIndex = 1
        '
        'flx
        '
        Me.flx.Location = New System.Drawing.Point(103, 212)
        Me.flx.Name = "flx"
        Me.flx.OcxState = CType(resources.GetObject("flx.OcxState"),System.Windows.Forms.AxHost.State)
        Me.flx.Size = New System.Drawing.Size(677, 320)
        Me.flx.TabIndex = 178
        '
        'flxc
        '
        Me.flxc.Location = New System.Drawing.Point(106, 27)
        Me.flxc.Name = "flxc"
        Me.flxc.OcxState = CType(resources.GetObject("flxc.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxc.Size = New System.Drawing.Size(677, 156)
        Me.flxc.TabIndex = 179
        '
        'txtcol
        '
        Me.txtcol.Location = New System.Drawing.Point(844, 90)
        Me.txtcol.Name = "txtcol"
        Me.txtcol.Size = New System.Drawing.Size(66, 20)
        Me.txtcol.TabIndex = 219
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(571, 90)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(97, 23)
        Me.Label8.TabIndex = 218
        Me.Label8.Text = "Incr Row Value"
        '
        'txtrow
        '
        Me.txtrow.Location = New System.Drawing.Point(669, 90)
        Me.txtrow.Name = "txtrow"
        Me.txtrow.Size = New System.Drawing.Size(66, 20)
        Me.txtrow.TabIndex = 217
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(381, 87)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(102, 23)
        Me.Label7.TabIndex = 216
        Me.Label7.Text = "Sticker Coloumn"
        '
        'txtstickcol
        '
        Me.txtstickcol.Location = New System.Drawing.Point(495, 87)
        Me.txtstickcol.Name = "txtstickcol"
        Me.txtstickcol.Size = New System.Drawing.Size(70, 20)
        Me.txtstickcol.TabIndex = 215
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(46, 84)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 23)
        Me.Label6.TabIndex = 214
        Me.Label6.Text = "Print On"
        '
        'cmbprnon
        '
        Me.cmbprnon.FormattingEnabled = True
        Me.cmbprnon.Location = New System.Drawing.Point(106, 81)
        Me.cmbprnon.Name = "cmbprnon"
        Me.cmbprnon.Size = New System.Drawing.Size(214, 21)
        Me.cmbprnon.TabIndex = 213
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(418, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 23)
        Me.Label5.TabIndex = 212
        Me.Label5.Text = "Print Type"
        '
        'cmbprntype
        '
        Me.cmbprntype.FormattingEnabled = True
        Me.cmbprntype.Location = New System.Drawing.Point(495, 57)
        Me.cmbprntype.Name = "cmbprntype"
        Me.cmbprntype.Size = New System.Drawing.Size(214, 21)
        Me.cmbprntype.TabIndex = 211
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(65, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 23)
        Me.Label4.TabIndex = 210
        Me.Label4.Text = "Type"
        '
        'cmbtype
        '
        Me.cmbtype.FormattingEnabled = True
        Me.cmbtype.Location = New System.Drawing.Point(106, 54)
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.Size = New System.Drawing.Size(214, 21)
        Me.cmbtype.TabIndex = 209
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(424, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 23)
        Me.Label3.TabIndex = 208
        Me.Label3.Text = "Company"
        '
        'cmbcomp
        '
        Me.cmbcomp.FormattingEnabled = True
        Me.cmbcomp.Location = New System.Drawing.Point(495, 30)
        Me.cmbcomp.Name = "cmbcomp"
        Me.cmbcomp.Size = New System.Drawing.Size(214, 21)
        Me.cmbcomp.TabIndex = 207
        '
        'txtdocno
        '
        Me.txtdocno.Location = New System.Drawing.Point(106, 27)
        Me.txtdocno.Name = "txtdocno"
        Me.txtdocno.Size = New System.Drawing.Size(100, 20)
        Me.txtdocno.TabIndex = 221
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(75, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 15)
        Me.Label2.TabIndex = 220
        Me.Label2.Text = "No"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(741, 90)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(97, 23)
        Me.Label9.TabIndex = 222
        Me.Label9.Text = "Incr Col Value"
        '
        'chkmrp
        '
        Me.chkmrp.AutoSize = True
        Me.chkmrp.Location = New System.Drawing.Point(721, 32)
        Me.chkmrp.Name = "chkmrp"
        Me.chkmrp.Size = New System.Drawing.Size(50, 17)
        Me.chkmrp.TabIndex = 223
        Me.chkmrp.Text = "MRP"
        Me.chkmrp.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(103, 108)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 20)
        Me.Label10.TabIndex = 225
        Me.Label10.Text = "LPT Port"
        '
        'txtport
        '
        Me.txtport.Location = New System.Drawing.Point(177, 105)
        Me.txtport.Name = "txtport"
        Me.txtport.Size = New System.Drawing.Size(70, 20)
        Me.txtport.TabIndex = 224
        Me.txtport.Text = "1"
        '
        'cmdsel
        '
        Me.cmdsel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsel.Location = New System.Drawing.Point(5, 212)
        Me.cmdsel.Name = "cmdsel"
        Me.cmdsel.Size = New System.Drawing.Size(83, 32)
        Me.cmdsel.TabIndex = 226
        Me.cmdsel.Text = "Select All"
        Me.cmdsel.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(253, 110)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(44, 18)
        Me.Label11.TabIndex = 228
        Me.Label11.Text = "Printer"
        '
        'txtprint
        '
        Me.txtprint.Location = New System.Drawing.Point(303, 110)
        Me.txtprint.Name = "txtprint"
        Me.txtprint.Size = New System.Drawing.Size(222, 20)
        Me.txtprint.TabIndex = 227
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(5, 250)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(83, 51)
        Me.Button1.TabIndex = 229
        Me.Button1.Text = "Set Mtr to 1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'chkprndir
        '
        Me.chkprndir.AutoSize = True
        Me.chkprndir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkprndir.Location = New System.Drawing.Point(157, 131)
        Me.chkprndir.Name = "chkprndir"
        Me.chkprndir.Size = New System.Drawing.Size(90, 17)
        Me.chkprndir.TabIndex = 230
        Me.chkprndir.Text = "Print Direct"
        Me.chkprndir.UseVisualStyleBackColor = True
        '
        'chksr
        '
        Me.chksr.AutoSize = True
        Me.chksr.Location = New System.Drawing.Point(777, 32)
        Me.chksr.Name = "chksr"
        Me.chksr.Size = New System.Drawing.Size(197, 17)
        Me.chksr.TabIndex = 233
        Me.chksr.Text = "Rate Revision  ShowRoom Barcode"
        Me.chksr.UseVisualStyleBackColor = True
        '
        'cmbyr
        '
        Me.cmbyr.FormattingEnabled = True
        Me.cmbyr.Location = New System.Drawing.Point(512, 175)
        Me.cmbyr.Name = "cmbyr"
        Me.cmbyr.Size = New System.Drawing.Size(107, 21)
        Me.cmbyr.TabIndex = 234
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(470, 181)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(36, 15)
        Me.Label12.TabIndex = 236
        Me.Label12.Text = "Year"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(307, 181)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(47, 15)
        Me.Label13.TabIndex = 238
        Me.Label13.Text = "Month"
        '
        'cmbmont
        '
        Me.cmbmont.FormattingEnabled = True
        Me.cmbmont.Location = New System.Drawing.Point(357, 175)
        Me.cmbmont.Name = "cmbmont"
        Me.cmbmont.Size = New System.Drawing.Size(107, 21)
        Me.cmbmont.TabIndex = 237
        '
        'lblrec
        '
        Me.lblrec.Location = New System.Drawing.Point(5, 497)
        Me.lblrec.Name = "lblrec"
        Me.lblrec.Size = New System.Drawing.Size(83, 23)
        Me.lblrec.TabIndex = 239
        Me.lblrec.Text = "0"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(8, 5)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(92, 20)
        Me.Label14.TabIndex = 241
        Me.Label14.Text = "Posting Period"
        '
        'cmbyear
        '
        Me.cmbyear.FormattingEnabled = True
        Me.cmbyear.Location = New System.Drawing.Point(106, 2)
        Me.cmbyear.Name = "cmbyear"
        Me.cmbyear.Size = New System.Drawing.Size(121, 21)
        Me.cmbyear.TabIndex = 240
        '
        'cmbprinter
        '
        Me.cmbprinter.FormattingEnabled = True
        Me.cmbprinter.Location = New System.Drawing.Point(797, 162)
        Me.cmbprinter.Name = "cmbprinter"
        Me.cmbprinter.Size = New System.Drawing.Size(149, 21)
        Me.cmbprinter.TabIndex = 243
        '
        'Btndel
        '
        Me.Btndel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Btndel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btndel.Location = New System.Drawing.Point(843, 223)
        Me.Btndel.Name = "Btndel"
        Me.Btndel.Size = New System.Drawing.Size(67, 33)
        Me.Btndel.TabIndex = 244
        Me.Btndel.Tag = "2"
        Me.Btndel.Text = "Delete"
        Me.Btndel.UseVisualStyleBackColor = True
        '
        'Btnok
        '
        Me.Btnok.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Btnok.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btnok.Location = New System.Drawing.Point(724, 115)
        Me.Btnok.Name = "Btnok"
        Me.Btnok.Size = New System.Drawing.Size(67, 33)
        Me.Btnok.TabIndex = 245
        Me.Btnok.Tag = "1"
        Me.Btnok.Text = "Ok"
        Me.Btnok.UseVisualStyleBackColor = True
        '
        'Btnprint
        '
        Me.Btnprint.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Btnprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btnprint.Location = New System.Drawing.Point(797, 116)
        Me.Btnprint.Name = "Btnprint"
        Me.Btnprint.Size = New System.Drawing.Size(67, 33)
        Me.Btnprint.TabIndex = 246
        Me.Btnprint.Tag = "2"
        Me.Btnprint.Text = "Print"
        Me.Btnprint.UseVisualStyleBackColor = True
        '
        'Btnexit
        '
        Me.Btnexit.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btnexit.Location = New System.Drawing.Point(870, 116)
        Me.Btnexit.Name = "Btnexit"
        Me.Btnexit.Size = New System.Drawing.Size(67, 33)
        Me.Btnexit.TabIndex = 247
        Me.Btnexit.Tag = ""
        Me.Btnexit.Text = "Exit"
        Me.Btnexit.UseVisualStyleBackColor = True
        '
        'frmbarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 698)
        Me.Controls.Add(Me.Btnexit)
        Me.Controls.Add(Me.Btnprint)
        Me.Controls.Add(Me.Btndel)
        Me.Controls.Add(Me.cmbprinter)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.cmbyear)
        Me.Controls.Add(Me.lblrec)
        Me.Controls.Add(Me.flxc)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cmbmont)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.cmbyr)
        Me.Controls.Add(Me.chksr)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmdsel)
        Me.Controls.Add(Me.chkmrp)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtdocno)
        Me.Controls.Add(Me.Label2)
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
        Me.Controls.Add(Me.flx)
        Me.Controls.Add(Me.txtbno)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtport)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtprint)
        Me.Controls.Add(Me.chkprndir)
        Me.Controls.Add(Me.Btnok)
        Me.Name = "frmbarcode"
        Me.Text = "FrmBarcode"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.flx,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.flxc,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtbno As System.Windows.Forms.TextBox
    Friend WithEvents flx As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents flxc As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents txtcol As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtrow As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtstickcol As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbprnon As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbprntype As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbtype As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbcomp As System.Windows.Forms.ComboBox
    Friend WithEvents txtdocno As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents chkmrp As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtport As System.Windows.Forms.TextBox
    Friend WithEvents cmdsel As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtprint As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chkprndir As System.Windows.Forms.CheckBox
    Friend WithEvents chksr As System.Windows.Forms.CheckBox
    Friend WithEvents cmbyr As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbmont As System.Windows.Forms.ComboBox
    Friend WithEvents lblrec As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmbyear As System.Windows.Forms.ComboBox
    Friend WithEvents cmbprinter As System.Windows.Forms.ComboBox
    Friend WithEvents Btndel As System.Windows.Forms.Button
    Friend WithEvents Btnok As System.Windows.Forms.Button
    Friend WithEvents Btnprint As System.Windows.Forms.Button
    Friend WithEvents Btnexit As System.Windows.Forms.Button
End Class

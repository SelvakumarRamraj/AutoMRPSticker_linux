<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmxlsrbarcode
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.chkrhl = New System.Windows.Forms.CheckBox()
        Me.chksr = New System.Windows.Forms.CheckBox()
        Me.txtcardcode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbparty = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtno = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.mskdatefr = New System.Windows.Forms.MaskedTextBox()
        Me.dg1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdadd = New System.Windows.Forms.Button()
        Me.cmddel = New System.Windows.Forms.Button()
        Me.cmdupdt = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.cmdcls = New System.Windows.Forms.Button()
        Me.chkoth = New System.Windows.Forms.CheckBox()
        CType(Me.dg1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkrhl
        '
        Me.chkrhl.AutoSize = True
        Me.chkrhl.Location = New System.Drawing.Point(585, 26)
        Me.chkrhl.Name = "chkrhl"
        Me.chkrhl.Size = New System.Drawing.Size(77, 17)
        Me.chkrhl.TabIndex = 261
        Me.chkrhl.Text = "Rhl Format"
        Me.chkrhl.UseVisualStyleBackColor = True
        '
        'chksr
        '
        Me.chksr.AutoSize = True
        Me.chksr.Location = New System.Drawing.Point(408, 24)
        Me.chksr.Name = "chksr"
        Me.chksr.Size = New System.Drawing.Size(81, 17)
        Me.chksr.TabIndex = 260
        Me.chksr.Text = "ShowRoom"
        Me.chksr.UseVisualStyleBackColor = True
        '
        'txtcardcode
        '
        Me.txtcardcode.Enabled = False
        Me.txtcardcode.Location = New System.Drawing.Point(455, 75)
        Me.txtcardcode.Name = "txtcardcode"
        Me.txtcardcode.Size = New System.Drawing.Size(170, 20)
        Me.txtcardcode.TabIndex = 259
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(19, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 21)
        Me.Label1.TabIndex = 258
        Me.Label1.Text = "Party Name"
        '
        'cmbparty
        '
        Me.cmbparty.FormattingEnabled = True
        Me.cmbparty.Location = New System.Drawing.Point(103, 72)
        Me.cmbparty.Name = "cmbparty"
        Me.cmbparty.Size = New System.Drawing.Size(336, 21)
        Me.cmbparty.TabIndex = 257
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 21)
        Me.Label3.TabIndex = 256
        Me.Label3.Text = "Docnum"
        '
        'txtno
        '
        Me.txtno.Location = New System.Drawing.Point(74, 23)
        Me.txtno.Name = "txtno"
        Me.txtno.Size = New System.Drawing.Size(129, 20)
        Me.txtno.TabIndex = 253
        Me.txtno.Tag = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(204, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 20)
        Me.Label2.TabIndex = 255
        Me.Label2.Text = "Date"
        '
        'mskdatefr
        '
        Me.mskdatefr.Location = New System.Drawing.Point(246, 25)
        Me.mskdatefr.Mask = "##-##-####"
        Me.mskdatefr.Name = "mskdatefr"
        Me.mskdatefr.Size = New System.Drawing.Size(72, 20)
        Me.mskdatefr.TabIndex = 254
        '
        'dg1
        '
        Me.dg1.AllowUserToAddRows = False
        Me.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8})
        Me.dg1.Location = New System.Drawing.Point(22, 110)
        Me.dg1.Name = "dg1"
        Me.dg1.Size = New System.Drawing.Size(880, 375)
        Me.dg1.TabIndex = 262
        '
        'Column1
        '
        Me.Column1.HeaderText = "Itemcode"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "Quantity"
        Me.Column2.Name = "Column2"
        '
        'Column3
        '
        Me.Column3.HeaderText = "Linenum"
        Me.Column3.Name = "Column3"
        '
        'Column4
        '
        Me.Column4.HeaderText = "Batch"
        Me.Column4.Name = "Column4"
        '
        'Column5
        '
        Me.Column5.HeaderText = "ColorName"
        Me.Column5.Name = "Column5"
        '
        'Column6
        '
        Me.Column6.HeaderText = "StickerDate"
        Me.Column6.Name = "Column6"
        '
        'Column7
        '
        Me.Column7.HeaderText = "MRP"
        Me.Column7.Name = "Column7"
        '
        'Column8
        '
        Me.Column8.HeaderText = "Barcode"
        Me.Column8.Name = "Column8"
        '
        'cmdadd
        '
        Me.cmdadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdadd.Location = New System.Drawing.Point(826, 23)
        Me.cmdadd.Name = "cmdadd"
        Me.cmdadd.Size = New System.Drawing.Size(64, 24)
        Me.cmdadd.TabIndex = 263
        Me.cmdadd.Text = "Add"
        Me.cmdadd.UseVisualStyleBackColor = True
        '
        'cmddel
        '
        Me.cmddel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddel.Location = New System.Drawing.Point(896, 23)
        Me.cmddel.Name = "cmddel"
        Me.cmddel.Size = New System.Drawing.Size(64, 24)
        Me.cmddel.TabIndex = 264
        Me.cmddel.Text = "Delete"
        Me.cmddel.UseVisualStyleBackColor = True
        '
        'cmdupdt
        '
        Me.cmdupdt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdupdt.Location = New System.Drawing.Point(966, 24)
        Me.cmdupdt.Name = "cmdupdt"
        Me.cmdupdt.Size = New System.Drawing.Size(64, 24)
        Me.cmdupdt.TabIndex = 265
        Me.cmdupdt.Text = "Save"
        Me.cmdupdt.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.Location = New System.Drawing.Point(1107, 24)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(64, 24)
        Me.cmdexit.TabIndex = 266
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'cmdcls
        '
        Me.cmdcls.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcls.Location = New System.Drawing.Point(1036, 26)
        Me.cmdcls.Name = "cmdcls"
        Me.cmdcls.Size = New System.Drawing.Size(64, 24)
        Me.cmdcls.TabIndex = 267
        Me.cmdcls.Text = "Clear"
        Me.cmdcls.UseVisualStyleBackColor = True
        '
        'chkoth
        '
        Me.chkoth.AutoSize = True
        Me.chkoth.Location = New System.Drawing.Point(495, 23)
        Me.chkoth.Name = "chkoth"
        Me.chkoth.Size = New System.Drawing.Size(52, 17)
        Me.chkoth.TabIndex = 268
        Me.chkoth.Text = "Other"
        Me.chkoth.UseVisualStyleBackColor = True
        '
        'Frmxlsrbarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1214, 701)
        Me.Controls.Add(Me.chkoth)
        Me.Controls.Add(Me.cmdcls)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdupdt)
        Me.Controls.Add(Me.cmddel)
        Me.Controls.Add(Me.cmdadd)
        Me.Controls.Add(Me.dg1)
        Me.Controls.Add(Me.chkrhl)
        Me.Controls.Add(Me.chksr)
        Me.Controls.Add(Me.txtcardcode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbparty)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtno)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.mskdatefr)
        Me.Name = "Frmxlsrbarcode"
        Me.Text = "Frmxlsrbarcode"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dg1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkrhl As System.Windows.Forms.CheckBox
    Friend WithEvents chksr As System.Windows.Forms.CheckBox
    Friend WithEvents txtcardcode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbparty As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtno As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mskdatefr As System.Windows.Forms.MaskedTextBox
    Friend WithEvents dg1 As System.Windows.Forms.DataGridView
    Friend WithEvents cmdadd As System.Windows.Forms.Button
    Friend WithEvents cmddel As System.Windows.Forms.Button
    Friend WithEvents cmdupdt As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdcls As System.Windows.Forms.Button
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkoth As System.Windows.Forms.CheckBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmlineloadrep
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
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.mskdatto = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.mskdatfr = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtlineno = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dg = New System.Windows.Forms.DataGridView()
        Me.Chkdat = New System.Windows.Forms.CheckBox()
        Me.butexcel = New System.Windows.Forms.Button()
        Me.chkcomplete = New System.Windows.Forms.CheckBox()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(734, 10)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(61, 23)
        Me.Button2.TabIndex = 11
        Me.Button2.Text = "Exit"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(557, 10)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(61, 23)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Display"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'mskdatto
        '
        Me.mskdatto.Location = New System.Drawing.Point(280, 11)
        Me.mskdatto.Mask = "##-##-####"
        Me.mskdatto.Name = "mskdatto"
        Me.mskdatto.Size = New System.Drawing.Size(70, 20)
        Me.mskdatto.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(254, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(22, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "To"
        '
        'mskdatfr
        '
        Me.mskdatfr.Location = New System.Drawing.Point(178, 11)
        Me.mskdatfr.Mask = "##-##-####"
        Me.mskdatfr.Name = "mskdatfr"
        Me.mskdatfr.Size = New System.Drawing.Size(70, 20)
        Me.mskdatfr.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(109, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Date From"
        '
        'txtlineno
        '
        Me.txtlineno.Location = New System.Drawing.Point(438, 11)
        Me.txtlineno.Name = "txtlineno"
        Me.txtlineno.Size = New System.Drawing.Size(100, 20)
        Me.txtlineno.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(381, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Line No"
        '
        'dg
        '
        Me.dg.AllowUserToAddRows = False
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Location = New System.Drawing.Point(12, 65)
        Me.dg.Name = "dg"
        Me.dg.RowHeadersVisible = False
        Me.dg.Size = New System.Drawing.Size(857, 310)
        Me.dg.TabIndex = 14
        '
        'Chkdat
        '
        Me.Chkdat.AutoSize = True
        Me.Chkdat.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chkdat.Location = New System.Drawing.Point(13, 13)
        Me.Chkdat.Name = "Chkdat"
        Me.Chkdat.Size = New System.Drawing.Size(78, 17)
        Me.Chkdat.TabIndex = 15
        Me.Chkdat.Text = "Datewise"
        Me.Chkdat.UseVisualStyleBackColor = True
        '
        'butexcel
        '
        Me.butexcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butexcel.Location = New System.Drawing.Point(624, 10)
        Me.butexcel.Name = "butexcel"
        Me.butexcel.Size = New System.Drawing.Size(101, 23)
        Me.butexcel.TabIndex = 16
        Me.butexcel.Text = "Excel Export"
        Me.butexcel.UseVisualStyleBackColor = True
        '
        'chkcomplete
        '
        Me.chkcomplete.AutoSize = True
        Me.chkcomplete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkcomplete.Location = New System.Drawing.Point(438, 37)
        Me.chkcomplete.Name = "chkcomplete"
        Me.chkcomplete.Size = New System.Drawing.Size(142, 17)
        Me.chkcomplete.TabIndex = 17
        Me.chkcomplete.Text = "Stitching Completion"
        Me.chkcomplete.UseVisualStyleBackColor = True
        '
        'frmlineloadrep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1057, 709)
        Me.Controls.Add(Me.chkcomplete)
        Me.Controls.Add(Me.butexcel)
        Me.Controls.Add(Me.Chkdat)
        Me.Controls.Add(Me.dg)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtlineno)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.mskdatto)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.mskdatfr)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmlineloadrep"
        Me.Text = "Frmlineloadrep"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents mskdatto As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mskdatfr As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtlineno As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dg As System.Windows.Forms.DataGridView
    Friend WithEvents Chkdat As System.Windows.Forms.CheckBox
    Friend WithEvents butexcel As System.Windows.Forms.Button
    Friend WithEvents chkcomplete As System.Windows.Forms.CheckBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmopermaster
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
        Me.butcancel = New System.Windows.Forms.Button
        Me.butsave = New System.Windows.Forms.Button
        Me.butdel = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbdepartment = New System.Windows.Forms.ComboBox
        Me.dg = New System.Windows.Forms.DataGridView
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtopername = New System.Windows.Forms.TextBox
        Me.butexit = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbstyle = New System.Windows.Forms.ComboBox
        Me.txtmctype = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbgrade = New System.Windows.Forms.ComboBox
        Me.txtsam = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtpcs = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtmanpower = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.butupdt = New System.Windows.Forms.Button
        Me.cmboper = New System.Windows.Forms.ComboBox
        Me.cmbdept = New System.Windows.Forms.ComboBox
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'butcancel
        '
        Me.butcancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butcancel.Location = New System.Drawing.Point(618, 156)
        Me.butcancel.Name = "butcancel"
        Me.butcancel.Size = New System.Drawing.Size(55, 23)
        Me.butcancel.TabIndex = 6
        Me.butcancel.Text = "Cancel"
        Me.butcancel.UseVisualStyleBackColor = True
        '
        'butsave
        '
        Me.butsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butsave.Location = New System.Drawing.Point(504, 156)
        Me.butsave.Name = "butsave"
        Me.butsave.Size = New System.Drawing.Size(55, 23)
        Me.butsave.TabIndex = 4
        Me.butsave.Text = "Add"
        Me.butsave.UseVisualStyleBackColor = True
        '
        'butdel
        '
        Me.butdel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butdel.Location = New System.Drawing.Point(561, 156)
        Me.butdel.Name = "butdel"
        Me.butdel.Size = New System.Drawing.Size(55, 23)
        Me.butdel.TabIndex = 5
        Me.butdel.Text = "Delete"
        Me.butdel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(137, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Department"
        '
        'cmbdepartment
        '
        Me.cmbdepartment.FormattingEnabled = True
        Me.cmbdepartment.Location = New System.Drawing.Point(215, 62)
        Me.cmbdepartment.Name = "cmbdepartment"
        Me.cmbdepartment.Size = New System.Drawing.Size(275, 21)
        Me.cmbdepartment.TabIndex = 2
        '
        'dg
        '
        Me.dg.AllowUserToAddRows = False
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Location = New System.Drawing.Point(47, 218)
        Me.dg.MultiSelect = False
        Me.dg.Name = "dg"
        Me.dg.RowHeadersVisible = False
        Me.dg.Size = New System.Drawing.Size(1001, 316)
        Me.dg.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(111, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Operation Name"
        '
        'txtopername
        '
        Me.txtopername.Location = New System.Drawing.Point(215, 18)
        Me.txtopername.Name = "txtopername"
        Me.txtopername.Size = New System.Drawing.Size(338, 20)
        Me.txtopername.TabIndex = 0
        '
        'butexit
        '
        Me.butexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butexit.Location = New System.Drawing.Point(675, 156)
        Me.butexit.Name = "butexit"
        Me.butexit.Size = New System.Drawing.Size(55, 23)
        Me.butexit.TabIndex = 7
        Me.butexit.Text = "Exit"
        Me.butexit.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(171, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Style"
        '
        'cmbstyle
        '
        Me.cmbstyle.FormattingEnabled = True
        Me.cmbstyle.Location = New System.Drawing.Point(215, 86)
        Me.cmbstyle.Name = "cmbstyle"
        Me.cmbstyle.Size = New System.Drawing.Size(275, 21)
        Me.cmbstyle.TabIndex = 3
        '
        'txtmctype
        '
        Me.txtmctype.Location = New System.Drawing.Point(215, 41)
        Me.txtmctype.Name = "txtmctype"
        Me.txtmctype.Size = New System.Drawing.Size(338, 20)
        Me.txtmctype.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(146, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Mac Type"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(142, 113)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Job Grade"
        '
        'cmbgrade
        '
        Me.cmbgrade.FormattingEnabled = True
        Me.cmbgrade.Location = New System.Drawing.Point(215, 113)
        Me.cmbgrade.Name = "cmbgrade"
        Me.cmbgrade.Size = New System.Drawing.Size(119, 21)
        Me.cmbgrade.TabIndex = 19
        '
        'txtsam
        '
        Me.txtsam.Location = New System.Drawing.Point(215, 140)
        Me.txtsam.Name = "txtsam"
        Me.txtsam.Size = New System.Drawing.Size(119, 20)
        Me.txtsam.TabIndex = 21
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(176, 140)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(33, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "SAM"
        '
        'txtpcs
        '
        Me.txtpcs.Location = New System.Drawing.Point(215, 166)
        Me.txtpcs.Name = "txtpcs"
        Me.txtpcs.Size = New System.Drawing.Size(119, 20)
        Me.txtpcs.TabIndex = 23
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(178, 166)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 13)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "PCS"
        '
        'txtmanpower
        '
        Me.txtmanpower.Location = New System.Drawing.Point(215, 192)
        Me.txtmanpower.Name = "txtmanpower"
        Me.txtmanpower.Size = New System.Drawing.Size(119, 20)
        Me.txtmanpower.TabIndex = 25
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(139, 192)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 13)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "Man Power"
        '
        'butupdt
        '
        Me.butupdt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butupdt.Location = New System.Drawing.Point(507, 187)
        Me.butupdt.Name = "butupdt"
        Me.butupdt.Size = New System.Drawing.Size(66, 23)
        Me.butupdt.TabIndex = 27
        Me.butupdt.Text = "Update"
        Me.butupdt.UseVisualStyleBackColor = True
        '
        'cmboper
        '
        Me.cmboper.FormattingEnabled = True
        Me.cmboper.Location = New System.Drawing.Point(560, 20)
        Me.cmboper.Name = "cmboper"
        Me.cmboper.Size = New System.Drawing.Size(61, 21)
        Me.cmboper.TabIndex = 28
        '
        'cmbdept
        '
        Me.cmbdept.FormattingEnabled = True
        Me.cmbdept.Location = New System.Drawing.Point(500, 63)
        Me.cmbdept.Name = "cmbdept"
        Me.cmbdept.Size = New System.Drawing.Size(275, 21)
        Me.cmbdept.TabIndex = 29
        '
        'Frmopermaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1070, 703)
        Me.Controls.Add(Me.cmbdept)
        Me.Controls.Add(Me.cmboper)
        Me.Controls.Add(Me.butupdt)
        Me.Controls.Add(Me.txtmanpower)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtpcs)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtsam)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbgrade)
        Me.Controls.Add(Me.txtmctype)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbstyle)
        Me.Controls.Add(Me.butexit)
        Me.Controls.Add(Me.txtopername)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.butcancel)
        Me.Controls.Add(Me.butsave)
        Me.Controls.Add(Me.butdel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbdepartment)
        Me.Controls.Add(Me.dg)
        Me.Name = "Frmopermaster"
        Me.Text = "Frmopermaster"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents butcancel As System.Windows.Forms.Button
    Friend WithEvents butsave As System.Windows.Forms.Button
    Friend WithEvents butdel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbdepartment As System.Windows.Forms.ComboBox
    Friend WithEvents dg As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtopername As System.Windows.Forms.TextBox
    Friend WithEvents butexit As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbstyle As System.Windows.Forms.ComboBox
    Friend WithEvents txtmctype As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbgrade As System.Windows.Forms.ComboBox
    Friend WithEvents txtsam As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtpcs As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtmanpower As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents butupdt As System.Windows.Forms.Button
    Friend WithEvents cmboper As System.Windows.Forms.ComboBox
    Friend WithEvents cmbdept As System.Windows.Forms.ComboBox
End Class

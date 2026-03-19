<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmstylemaster
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
        Me.butexit = New System.Windows.Forms.Button()
        Me.txtstyle = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.butcancel = New System.Windows.Forms.Button()
        Me.butsave = New System.Windows.Forms.Button()
        Me.butdel = New System.Windows.Forms.Button()
        Me.dg = New System.Windows.Forms.DataGridView()
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'butexit
        '
        Me.butexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butexit.Location = New System.Drawing.Point(819, 63)
        Me.butexit.Name = "butexit"
        Me.butexit.Size = New System.Drawing.Size(55, 23)
        Me.butexit.TabIndex = 22
        Me.butexit.Text = "Exit"
        Me.butexit.UseVisualStyleBackColor = True
        '
        'txtstyle
        '
        Me.txtstyle.Location = New System.Drawing.Point(301, 63)
        Me.txtstyle.Name = "txtstyle"
        Me.txtstyle.Size = New System.Drawing.Size(338, 20)
        Me.txtstyle.TabIndex = 21
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(224, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "Style Name"
        '
        'butcancel
        '
        Me.butcancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butcancel.Location = New System.Drawing.Point(762, 63)
        Me.butcancel.Name = "butcancel"
        Me.butcancel.Size = New System.Drawing.Size(55, 23)
        Me.butcancel.TabIndex = 19
        Me.butcancel.Text = "Cancel"
        Me.butcancel.UseVisualStyleBackColor = True
        '
        'butsave
        '
        Me.butsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butsave.Location = New System.Drawing.Point(648, 63)
        Me.butsave.Name = "butsave"
        Me.butsave.Size = New System.Drawing.Size(55, 23)
        Me.butsave.TabIndex = 18
        Me.butsave.Text = "Add"
        Me.butsave.UseVisualStyleBackColor = True
        '
        'butdel
        '
        Me.butdel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butdel.Location = New System.Drawing.Point(705, 63)
        Me.butdel.Name = "butdel"
        Me.butdel.Size = New System.Drawing.Size(55, 23)
        Me.butdel.TabIndex = 17
        Me.butdel.Text = "Delete"
        Me.butdel.UseVisualStyleBackColor = True
        '
        'dg
        '
        Me.dg.AllowUserToAddRows = False
        Me.dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Location = New System.Drawing.Point(269, 101)
        Me.dg.MultiSelect = False
        Me.dg.Name = "dg"
        Me.dg.RowHeadersVisible = False
        Me.dg.Size = New System.Drawing.Size(462, 514)
        Me.dg.TabIndex = 16
        '
        'Frmstylemaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 703)
        Me.Controls.Add(Me.butexit)
        Me.Controls.Add(Me.txtstyle)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.butcancel)
        Me.Controls.Add(Me.butsave)
        Me.Controls.Add(Me.butdel)
        Me.Controls.Add(Me.dg)
        Me.Name = "Frmstylemaster"
        Me.Text = "Frmstylemaster"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents butexit As System.Windows.Forms.Button
    Friend WithEvents txtstyle As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents butcancel As System.Windows.Forms.Button
    Friend WithEvents butsave As System.Windows.Forms.Button
    Friend WithEvents butdel As System.Windows.Forms.Button
    Friend WithEvents dg As System.Windows.Forms.DataGridView
End Class

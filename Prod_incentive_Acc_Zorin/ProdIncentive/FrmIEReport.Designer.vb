<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmIEReport
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.mskdatfr = New System.Windows.Forms.MaskedTextBox
        Me.mskdatto = New System.Windows.Forms.MaskedTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.dg = New System.Windows.Forms.DataGridView
        Me.dg2 = New System.Windows.Forms.DataGridView
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblhtot = New System.Windows.Forms.Label
        Me.lblotot = New System.Windows.Forms.Label
        Me.lblntotmac = New System.Windows.Forms.Label
        Me.lbltotmac = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblsal = New System.Windows.Forms.Label
        Me.lblshqty = New System.Windows.Forms.Label
        Me.lblmsksal = New System.Windows.Forms.Label
        Me.lblmskqty = New System.Windows.Forms.Label
        Me.lblmskclsal = New System.Windows.Forms.Label
        Me.lblmskclqty = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Lblshper = New System.Windows.Forms.Label
        Me.lblmskper = New System.Windows.Forms.Label
        Me.lblmskclper = New System.Windows.Forms.Label
        CType(Me.dg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dg2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Date From"
        '
        'mskdatfr
        '
        Me.mskdatfr.Location = New System.Drawing.Point(76, 10)
        Me.mskdatfr.Mask = "##-##-####"
        Me.mskdatfr.Name = "mskdatfr"
        Me.mskdatfr.Size = New System.Drawing.Size(70, 20)
        Me.mskdatfr.TabIndex = 1
        '
        'mskdatto
        '
        Me.mskdatto.Location = New System.Drawing.Point(182, 10)
        Me.mskdatto.Mask = "##-##-####"
        Me.mskdatto.Name = "mskdatto"
        Me.mskdatto.Size = New System.Drawing.Size(70, 20)
        Me.mskdatto.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(152, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(22, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "To"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(269, 10)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(61, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Display"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(342, 10)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(61, 23)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Exit"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'dg
        '
        Me.dg.AllowUserToAddRows = False
        Me.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg.Location = New System.Drawing.Point(12, 40)
        Me.dg.Name = "dg"
        Me.dg.RowHeadersVisible = False
        Me.dg.Size = New System.Drawing.Size(1194, 362)
        Me.dg.TabIndex = 6
        '
        'dg2
        '
        Me.dg2.AllowUserToAddRows = False
        Me.dg2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dg2.Location = New System.Drawing.Point(12, 522)
        Me.dg2.Name = "dg2"
        Me.dg2.RowHeadersVisible = False
        Me.dg2.Size = New System.Drawing.Size(854, 182)
        Me.dg2.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Maroon
        Me.Label3.Location = New System.Drawing.Point(202, 506)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(145, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "CONSOLIDATED Report"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.Label4.Location = New System.Drawing.Point(228, 409)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(105, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Total Attendance"
        '
        'lblhtot
        '
        Me.lblhtot.AutoSize = True
        Me.lblhtot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblhtot.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.lblhtot.Location = New System.Drawing.Point(630, 409)
        Me.lblhtot.Name = "lblhtot"
        Me.lblhtot.Size = New System.Drawing.Size(14, 13)
        Me.lblhtot.TabIndex = 10
        Me.lblhtot.Text = "0"
        Me.lblhtot.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblotot
        '
        Me.lblotot.AutoSize = True
        Me.lblotot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblotot.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.lblotot.Location = New System.Drawing.Point(687, 409)
        Me.lblotot.Name = "lblotot"
        Me.lblotot.Size = New System.Drawing.Size(14, 13)
        Me.lblotot.TabIndex = 11
        Me.lblotot.Text = "0"
        Me.lblotot.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblntotmac
        '
        Me.lblntotmac.AutoSize = True
        Me.lblntotmac.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblntotmac.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.lblntotmac.Location = New System.Drawing.Point(532, 409)
        Me.lblntotmac.Name = "lblntotmac"
        Me.lblntotmac.Size = New System.Drawing.Size(14, 13)
        Me.lblntotmac.TabIndex = 13
        Me.lblntotmac.Text = "0"
        Me.lblntotmac.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbltotmac
        '
        Me.lbltotmac.AutoSize = True
        Me.lbltotmac.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotmac.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.lbltotmac.Location = New System.Drawing.Point(472, 409)
        Me.lbltotmac.Name = "lbltotmac"
        Me.lbltotmac.Size = New System.Drawing.Size(14, 13)
        Me.lbltotmac.TabIndex = 12
        Me.lbltotmac.Text = "0"
        Me.lbltotmac.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblmskclper)
        Me.Panel1.Controls.Add(Me.lblmskper)
        Me.Panel1.Controls.Add(Me.Lblshper)
        Me.Panel1.Controls.Add(Me.lblshqty)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.lblsal)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.lblmsksal)
        Me.Panel1.Controls.Add(Me.lblmskclqty)
        Me.Panel1.Controls.Add(Me.lblmskqty)
        Me.Panel1.Controls.Add(Me.lblmskclsal)
        Me.Panel1.Location = New System.Drawing.Point(725, 408)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(434, 106)
        Me.Panel1.TabIndex = 22
        '
        'lblsal
        '
        Me.lblsal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsal.ForeColor = System.Drawing.Color.Maroon
        Me.lblsal.Location = New System.Drawing.Point(69, 13)
        Me.lblsal.Name = "lblsal"
        Me.lblsal.Size = New System.Drawing.Size(127, 21)
        Me.lblsal.TabIndex = 14
        Me.lblsal.Text = "0"
        Me.lblsal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblshqty
        '
        Me.lblshqty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblshqty.ForeColor = System.Drawing.Color.Maroon
        Me.lblshqty.Location = New System.Drawing.Point(201, 12)
        Me.lblshqty.Name = "lblshqty"
        Me.lblshqty.Size = New System.Drawing.Size(103, 21)
        Me.lblshqty.TabIndex = 15
        Me.lblshqty.Text = "0"
        Me.lblshqty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblmsksal
        '
        Me.lblmsksal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmsksal.ForeColor = System.Drawing.Color.Maroon
        Me.lblmsksal.Location = New System.Drawing.Point(69, 46)
        Me.lblmsksal.Name = "lblmsksal"
        Me.lblmsksal.Size = New System.Drawing.Size(127, 21)
        Me.lblmsksal.TabIndex = 16
        Me.lblmsksal.Text = "0"
        Me.lblmsksal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblmskqty
        '
        Me.lblmskqty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmskqty.ForeColor = System.Drawing.Color.Maroon
        Me.lblmskqty.Location = New System.Drawing.Point(201, 45)
        Me.lblmskqty.Name = "lblmskqty"
        Me.lblmskqty.Size = New System.Drawing.Size(103, 21)
        Me.lblmskqty.TabIndex = 17
        Me.lblmskqty.Text = "0"
        Me.lblmskqty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblmskclsal
        '
        Me.lblmskclsal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmskclsal.ForeColor = System.Drawing.Color.Maroon
        Me.lblmskclsal.Location = New System.Drawing.Point(69, 68)
        Me.lblmskclsal.Name = "lblmskclsal"
        Me.lblmskclsal.Size = New System.Drawing.Size(127, 21)
        Me.lblmskclsal.TabIndex = 18
        Me.lblmskclsal.Text = "0"
        Me.lblmskclsal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblmskclqty
        '
        Me.lblmskclqty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmskclqty.ForeColor = System.Drawing.Color.Maroon
        Me.lblmskclqty.Location = New System.Drawing.Point(201, 67)
        Me.lblmskclqty.Name = "lblmskclqty"
        Me.lblmskclqty.Size = New System.Drawing.Size(103, 21)
        Me.lblmskclqty.TabIndex = 19
        Me.lblmskclqty.Text = "0"
        Me.lblmskclqty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.Label7.Location = New System.Drawing.Point(6, 52)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 13)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "White"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.Label8.Location = New System.Drawing.Point(6, 75)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(36, 13)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Color"
        '
        'Lblshper
        '
        Me.Lblshper.AutoSize = True
        Me.Lblshper.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lblshper.ForeColor = System.Drawing.Color.DarkGreen
        Me.Lblshper.Location = New System.Drawing.Point(346, 17)
        Me.Lblshper.Name = "Lblshper"
        Me.Lblshper.Size = New System.Drawing.Size(14, 13)
        Me.Lblshper.TabIndex = 22
        Me.Lblshper.Text = "0"
        '
        'lblmskper
        '
        Me.lblmskper.AutoSize = True
        Me.lblmskper.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmskper.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblmskper.Location = New System.Drawing.Point(346, 46)
        Me.lblmskper.Name = "lblmskper"
        Me.lblmskper.Size = New System.Drawing.Size(14, 13)
        Me.lblmskper.TabIndex = 23
        Me.lblmskper.Text = "0"
        '
        'lblmskclper
        '
        Me.lblmskclper.AutoSize = True
        Me.lblmskclper.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmskclper.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblmskclper.Location = New System.Drawing.Point(346, 71)
        Me.lblmskclper.Name = "lblmskclper"
        Me.lblmskclper.Size = New System.Drawing.Size(14, 13)
        Me.lblmskclper.TabIndex = 24
        Me.lblmskclper.Text = "0"
        '
        'FrmIEReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1207, 735)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblntotmac)
        Me.Controls.Add(Me.lbltotmac)
        Me.Controls.Add(Me.lblotot)
        Me.Controls.Add(Me.lblhtot)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dg2)
        Me.Controls.Add(Me.dg)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.mskdatto)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.mskdatfr)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmIEReport"
        Me.Text = "FrmIEReport"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dg2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mskdatfr As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskdatto As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents dg As System.Windows.Forms.DataGridView
    Friend WithEvents dg2 As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblhtot As System.Windows.Forms.Label
    Friend WithEvents lblotot As System.Windows.Forms.Label
    Friend WithEvents lblntotmac As System.Windows.Forms.Label
    Friend WithEvents lbltotmac As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblmskclper As System.Windows.Forms.Label
    Friend WithEvents lblmskper As System.Windows.Forms.Label
    Friend WithEvents Lblshper As System.Windows.Forms.Label
    Friend WithEvents lblshqty As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblsal As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblmsksal As System.Windows.Forms.Label
    Friend WithEvents lblmskclqty As System.Windows.Forms.Label
    Friend WithEvents lblmskqty As System.Windows.Forms.Label
    Friend WithEvents lblmskclsal As System.Windows.Forms.Label
End Class

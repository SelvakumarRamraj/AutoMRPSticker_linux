<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmteamdist
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmteamdist))
        Me.flxh = New AxMSFlexGridLib.AxMSFlexGrid
        Me.Label9 = New System.Windows.Forms.Label
        Me.mskdatfr = New System.Windows.Forms.MaskedTextBox
        Me.mskdatto = New System.Windows.Forms.MaskedTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmddisp = New System.Windows.Forms.Button
        Me.cmdsave = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        Me.flxv = New AxMSFlexGridLib.AxMSFlexGrid
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.flxw = New AxMSFlexGridLib.AxMSFlexGrid
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblhqty = New System.Windows.Forms.Label
        Me.lblhmtrs = New System.Windows.Forms.Label
        Me.lblwmtrs = New System.Windows.Forms.Label
        Me.lblwqty = New System.Windows.Forms.Label
        Me.lblvmtrs = New System.Windows.Forms.Label
        Me.lblvqty = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.mskddate = New System.Windows.Forms.MaskedTextBox
        Me.cmdexcel = New System.Windows.Forms.Button
        CType(Me.flxh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxw, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'flxh
        '
        Me.flxh.Location = New System.Drawing.Point(12, 38)
        Me.flxh.Name = "flxh"
        Me.flxh.OcxState = CType(resources.GetObject("flxh.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxh.Size = New System.Drawing.Size(696, 207)
        Me.flxh.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(40, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 20)
        Me.Label9.TabIndex = 237
        Me.Label9.Text = "Date"
        '
        'mskdatfr
        '
        Me.mskdatfr.Location = New System.Drawing.Point(79, 12)
        Me.mskdatfr.Mask = "##-##-####"
        Me.mskdatfr.Name = "mskdatfr"
        Me.mskdatfr.Size = New System.Drawing.Size(75, 20)
        Me.mskdatfr.TabIndex = 236
        '
        'mskdatto
        '
        Me.mskdatto.Location = New System.Drawing.Point(191, 12)
        Me.mskdatto.Mask = "##-##-####"
        Me.mskdatto.Name = "mskdatto"
        Me.mskdatto.Size = New System.Drawing.Size(75, 20)
        Me.mskdatto.TabIndex = 238
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(160, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 16)
        Me.Label1.TabIndex = 239
        Me.Label1.Text = "To"
        '
        'cmddisp
        '
        Me.cmddisp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddisp.Location = New System.Drawing.Point(443, 7)
        Me.cmddisp.Name = "cmddisp"
        Me.cmddisp.Size = New System.Drawing.Size(72, 29)
        Me.cmddisp.TabIndex = 240
        Me.cmddisp.Text = "Display"
        Me.cmddisp.UseVisualStyleBackColor = True
        '
        'cmdsave
        '
        Me.cmdsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsave.Location = New System.Drawing.Point(535, 7)
        Me.cmdsave.Name = "cmdsave"
        Me.cmdsave.Size = New System.Drawing.Size(72, 29)
        Me.cmdsave.TabIndex = 241
        Me.cmdsave.Text = "Save"
        Me.cmdsave.UseVisualStyleBackColor = True
        '
        'cmdexit
        '
        Me.cmdexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.Location = New System.Drawing.Point(613, 6)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(72, 29)
        Me.cmdexit.TabIndex = 242
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.UseVisualStyleBackColor = True
        '
        'flxv
        '
        Me.flxv.Location = New System.Drawing.Point(12, 273)
        Me.flxv.Name = "flxv"
        Me.flxv.OcxState = CType(resources.GetObject("flxv.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxv.Size = New System.Drawing.Size(618, 325)
        Me.flxv.TabIndex = 243
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial Black", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.RoyalBlue
        Me.Label2.Location = New System.Drawing.Point(157, 244)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 21)
        Me.Label2.TabIndex = 244
        Me.Label2.Text = "VICTORY"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial Black", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.Label3.Location = New System.Drawing.Point(714, 243)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(109, 24)
        Me.Label3.TabIndex = 245
        Me.Label3.Text = "WINNER"
        '
        'flxw
        '
        Me.flxw.Location = New System.Drawing.Point(636, 268)
        Me.flxw.Name = "flxw"
        Me.flxw.OcxState = CType(resources.GetObject("flxw.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flxw.Size = New System.Drawing.Size(609, 325)
        Me.flxw.TabIndex = 246
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial Black", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.DarkRed
        Me.Label4.Location = New System.Drawing.Point(925, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 24)
        Me.Label4.TabIndex = 247
        Me.Label4.Text = "Mtrs"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial Black", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.DarkRed
        Me.Label5.Location = New System.Drawing.Point(749, 69)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 24)
        Me.Label5.TabIndex = 248
        Me.Label5.Text = "Qty"
        '
        'lblhqty
        '
        Me.lblhqty.Font = New System.Drawing.Font("Arial Black", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblhqty.ForeColor = System.Drawing.Color.DarkRed
        Me.lblhqty.Location = New System.Drawing.Point(714, 106)
        Me.lblhqty.Name = "lblhqty"
        Me.lblhqty.Size = New System.Drawing.Size(128, 24)
        Me.lblhqty.TabIndex = 249
        Me.lblhqty.Text = "0"
        Me.lblhqty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblhmtrs
        '
        Me.lblhmtrs.Font = New System.Drawing.Font("Arial Black", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblhmtrs.ForeColor = System.Drawing.Color.DarkRed
        Me.lblhmtrs.Location = New System.Drawing.Point(879, 106)
        Me.lblhmtrs.Name = "lblhmtrs"
        Me.lblhmtrs.Size = New System.Drawing.Size(125, 24)
        Me.lblhmtrs.TabIndex = 250
        Me.lblhmtrs.Text = "0"
        Me.lblhmtrs.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblwmtrs
        '
        Me.lblwmtrs.Font = New System.Drawing.Font("Arial Black", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblwmtrs.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.lblwmtrs.Location = New System.Drawing.Point(1068, 609)
        Me.lblwmtrs.Name = "lblwmtrs"
        Me.lblwmtrs.Size = New System.Drawing.Size(125, 24)
        Me.lblwmtrs.TabIndex = 252
        Me.lblwmtrs.Text = "0"
        Me.lblwmtrs.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblwqty
        '
        Me.lblwqty.Font = New System.Drawing.Font("Arial Black", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblwqty.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.lblwqty.Location = New System.Drawing.Point(903, 609)
        Me.lblwqty.Name = "lblwqty"
        Me.lblwqty.Size = New System.Drawing.Size(128, 24)
        Me.lblwqty.TabIndex = 251
        Me.lblwqty.Text = "0"
        Me.lblwqty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblvmtrs
        '
        Me.lblvmtrs.Font = New System.Drawing.Font("Arial Black", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvmtrs.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblvmtrs.Location = New System.Drawing.Point(390, 610)
        Me.lblvmtrs.Name = "lblvmtrs"
        Me.lblvmtrs.Size = New System.Drawing.Size(125, 24)
        Me.lblvmtrs.TabIndex = 254
        Me.lblvmtrs.Text = "0"
        Me.lblvmtrs.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblvqty
        '
        Me.lblvqty.Font = New System.Drawing.Font("Arial Black", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvqty.ForeColor = System.Drawing.Color.RoyalBlue
        Me.lblvqty.Location = New System.Drawing.Point(225, 610)
        Me.lblvqty.Name = "lblvqty"
        Me.lblvqty.Size = New System.Drawing.Size(128, 24)
        Me.lblvqty.TabIndex = 253
        Me.lblvqty.Text = "0"
        Me.lblvqty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial Black", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.RoyalBlue
        Me.Label11.Location = New System.Drawing.Point(38, 613)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(71, 21)
        Me.Label11.TabIndex = 255
        Me.Label11.Text = "Total"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial Black", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.Label12.Location = New System.Drawing.Point(745, 607)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(109, 24)
        Me.Label12.TabIndex = 256
        Me.Label12.Text = "Total"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(723, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 20)
        Me.Label6.TabIndex = 258
        Me.Label6.Text = "Distribution Date"
        '
        'mskddate
        '
        Me.mskddate.Location = New System.Drawing.Point(836, 16)
        Me.mskddate.Mask = "##-##-####"
        Me.mskddate.Name = "mskddate"
        Me.mskddate.Size = New System.Drawing.Size(75, 20)
        Me.mskddate.TabIndex = 257
        '
        'cmdexcel
        '
        Me.cmdexcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexcel.Location = New System.Drawing.Point(601, 245)
        Me.cmdexcel.Name = "cmdexcel"
        Me.cmdexcel.Size = New System.Drawing.Size(62, 22)
        Me.cmdexcel.TabIndex = 259
        Me.cmdexcel.Text = "Excel"
        Me.cmdexcel.UseVisualStyleBackColor = True
        '
        'frmteamdist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1257, 734)
        Me.Controls.Add(Me.cmdexcel)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.mskddate)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblvmtrs)
        Me.Controls.Add(Me.lblvqty)
        Me.Controls.Add(Me.lblwmtrs)
        Me.Controls.Add(Me.lblwqty)
        Me.Controls.Add(Me.lblhmtrs)
        Me.Controls.Add(Me.lblhqty)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.flxw)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.flxv)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdsave)
        Me.Controls.Add(Me.cmddisp)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.mskdatto)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.mskdatfr)
        Me.Controls.Add(Me.flxh)
        Me.Name = "frmteamdist"
        Me.Text = "frmteamdist"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.flxh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxw, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents flxh As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents mskdatfr As System.Windows.Forms.MaskedTextBox
    Friend WithEvents mskdatto As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmddisp As System.Windows.Forms.Button
    Friend WithEvents cmdsave As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents flxv As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents flxw As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblhqty As System.Windows.Forms.Label
    Friend WithEvents lblhmtrs As System.Windows.Forms.Label
    Friend WithEvents lblwmtrs As System.Windows.Forms.Label
    Friend WithEvents lblwqty As System.Windows.Forms.Label
    Friend WithEvents lblvmtrs As System.Windows.Forms.Label
    Friend WithEvents lblvqty As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents mskddate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmdexcel As System.Windows.Forms.Button
End Class

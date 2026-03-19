<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmincentive
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frmincentive))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.flx = New AxMSFlexGridLib.AxMSFlexGrid
        Me.Label1 = New System.Windows.Forms.Label
        Me.mskdateto = New System.Windows.Forms.MaskedTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.mskdatefr = New System.Windows.Forms.MaskedTextBox
        Me.cmdupdt = New CButtonLib.CButton
        Me.cmddel = New CButtonLib.CButton
        Me.cmdadd = New CButtonLib.CButton
        Me.cmdexit = New CButtonLib.CButton
        Me.txtno = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.CMBYR = New System.Windows.Forms.ComboBox
        Me.txtyno = New System.Windows.Forms.TextBox
        Me.txtdocentry = New System.Windows.Forms.TextBox
        CType(Me.flx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'flx
        '
        Me.flx.Location = New System.Drawing.Point(12, 68)
        Me.flx.Name = "flx"
        Me.flx.OcxState = CType(resources.GetObject("flx.OcxState"), System.Windows.Forms.AxHost.State)
        Me.flx.Size = New System.Drawing.Size(1002, 395)
        Me.flx.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(367, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 20)
        Me.Label1.TabIndex = 213
        Me.Label1.Text = "To"
        '
        'mskdateto
        '
        Me.mskdateto.Location = New System.Drawing.Point(398, 12)
        Me.mskdateto.Mask = "##-##-####"
        Me.mskdateto.Name = "mskdateto"
        Me.mskdateto.Size = New System.Drawing.Size(72, 20)
        Me.mskdateto.TabIndex = 212
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(247, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 20)
        Me.Label2.TabIndex = 211
        Me.Label2.Text = "Date"
        '
        'mskdatefr
        '
        Me.mskdatefr.Location = New System.Drawing.Point(289, 12)
        Me.mskdatefr.Mask = "##-##-####"
        Me.mskdatefr.Name = "mskdatefr"
        Me.mskdatefr.Size = New System.Drawing.Size(72, 20)
        Me.mskdatefr.TabIndex = 210
        '
        'cmdupdt
        '
        Me.cmdupdt.BorderColor = System.Drawing.Color.Silver
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.cmdupdt.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.5!, 1.007168!, 1.0!}
        Me.cmdupdt.ColorFillBlend = CBlendItems1
        Me.cmdupdt.ColorFillSolid = System.Drawing.Color.Silver
        Me.cmdupdt.Corners.All = CType(6, Short)
        Me.cmdupdt.Corners.LowerLeft = CType(6, Short)
        Me.cmdupdt.Corners.LowerRight = CType(6, Short)
        Me.cmdupdt.Corners.UpperLeft = CType(6, Short)
        Me.cmdupdt.Corners.UpperRight = CType(6, Short)
        Me.cmdupdt.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.cmdupdt.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.cmdupdt.FocalPoints.CenterPtX = 0.6410257!
        Me.cmdupdt.FocalPoints.CenterPtY = 0.0!
        Me.cmdupdt.FocalPoints.FocusPtX = 0.0!
        Me.cmdupdt.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.cmdupdt.FocusPtTracker = DesignerRectTracker2
        Me.cmdupdt.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdupdt.ForeColor = System.Drawing.SystemColors.InfoText
        Me.cmdupdt.Image = Nothing
        Me.cmdupdt.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdupdt.ImageIndex = 0
        Me.cmdupdt.ImageSize = New System.Drawing.Size(16, 16)
        Me.cmdupdt.Location = New System.Drawing.Point(777, 15)
        Me.cmdupdt.Name = "cmdupdt"
        Me.cmdupdt.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.cmdupdt.SideImage = Nothing
        Me.cmdupdt.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdupdt.SideImageSize = New System.Drawing.Size(48, 48)
        Me.cmdupdt.Size = New System.Drawing.Size(66, 29)
        Me.cmdupdt.TabIndex = 233
        Me.cmdupdt.Tag = "2"
        Me.cmdupdt.Text = "Save"
        Me.cmdupdt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdupdt.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.cmdupdt.TextMargin = New System.Windows.Forms.Padding(0)
        Me.cmdupdt.TextShadow = System.Drawing.Color.LightGray
        '
        'cmddel
        '
        Me.cmddel.BorderColor = System.Drawing.Color.Silver
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.cmddel.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.5!, 1.007168!, 1.0!}
        Me.cmddel.ColorFillBlend = CBlendItems2
        Me.cmddel.ColorFillSolid = System.Drawing.Color.Silver
        Me.cmddel.Corners.All = CType(6, Short)
        Me.cmddel.Corners.LowerLeft = CType(6, Short)
        Me.cmddel.Corners.LowerRight = CType(6, Short)
        Me.cmddel.Corners.UpperLeft = CType(6, Short)
        Me.cmddel.Corners.UpperRight = CType(6, Short)
        Me.cmddel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.cmddel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.cmddel.FocalPoints.CenterPtX = 0.6410257!
        Me.cmddel.FocalPoints.CenterPtY = 0.0!
        Me.cmddel.FocalPoints.FocusPtX = 0.0!
        Me.cmddel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.cmddel.FocusPtTracker = DesignerRectTracker4
        Me.cmddel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddel.ForeColor = System.Drawing.SystemColors.InfoText
        Me.cmddel.Image = Nothing
        Me.cmddel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmddel.ImageIndex = 0
        Me.cmddel.ImageSize = New System.Drawing.Size(16, 16)
        Me.cmddel.Location = New System.Drawing.Point(705, 15)
        Me.cmddel.Name = "cmddel"
        Me.cmddel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.cmddel.SideImage = Nothing
        Me.cmddel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmddel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.cmddel.Size = New System.Drawing.Size(66, 29)
        Me.cmddel.TabIndex = 234
        Me.cmddel.Tag = "2"
        Me.cmddel.Text = "Delete"
        Me.cmddel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmddel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.cmddel.TextMargin = New System.Windows.Forms.Padding(0)
        Me.cmddel.TextShadow = System.Drawing.Color.LightGray
        '
        'cmdadd
        '
        Me.cmdadd.BorderColor = System.Drawing.Color.Silver
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.cmdadd.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.5!, 1.007168!, 1.0!}
        Me.cmdadd.ColorFillBlend = CBlendItems3
        Me.cmdadd.ColorFillSolid = System.Drawing.Color.Silver
        Me.cmdadd.Corners.All = CType(6, Short)
        Me.cmdadd.Corners.LowerLeft = CType(6, Short)
        Me.cmdadd.Corners.LowerRight = CType(6, Short)
        Me.cmdadd.Corners.UpperLeft = CType(6, Short)
        Me.cmdadd.Corners.UpperRight = CType(6, Short)
        Me.cmdadd.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.cmdadd.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.cmdadd.FocalPoints.CenterPtX = 0.6410257!
        Me.cmdadd.FocalPoints.CenterPtY = 0.0!
        Me.cmdadd.FocalPoints.FocusPtX = 0.0!
        Me.cmdadd.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.cmdadd.FocusPtTracker = DesignerRectTracker6
        Me.cmdadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdadd.ForeColor = System.Drawing.SystemColors.InfoText
        Me.cmdadd.Image = Nothing
        Me.cmdadd.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdadd.ImageIndex = 0
        Me.cmdadd.ImageSize = New System.Drawing.Size(16, 16)
        Me.cmdadd.Location = New System.Drawing.Point(633, 14)
        Me.cmdadd.Name = "cmdadd"
        Me.cmdadd.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.cmdadd.SideImage = Nothing
        Me.cmdadd.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdadd.SideImageSize = New System.Drawing.Size(48, 48)
        Me.cmdadd.Size = New System.Drawing.Size(66, 29)
        Me.cmdadd.TabIndex = 235
        Me.cmdadd.Tag = "2"
        Me.cmdadd.Text = "Add"
        Me.cmdadd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdadd.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.cmdadd.TextMargin = New System.Windows.Forms.Padding(0)
        Me.cmdadd.TextShadow = System.Drawing.Color.LightGray
        '
        'cmdexit
        '
        Me.cmdexit.BorderColor = System.Drawing.Color.Silver
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.cmdexit.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.5!, 1.007168!, 1.0!}
        Me.cmdexit.ColorFillBlend = CBlendItems4
        Me.cmdexit.ColorFillSolid = System.Drawing.Color.Silver
        Me.cmdexit.Corners.All = CType(6, Short)
        Me.cmdexit.Corners.LowerLeft = CType(6, Short)
        Me.cmdexit.Corners.LowerRight = CType(6, Short)
        Me.cmdexit.Corners.UpperLeft = CType(6, Short)
        Me.cmdexit.Corners.UpperRight = CType(6, Short)
        Me.cmdexit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.cmdexit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.cmdexit.FocalPoints.CenterPtX = 0.6410257!
        Me.cmdexit.FocalPoints.CenterPtY = 0.0!
        Me.cmdexit.FocalPoints.FocusPtX = 0.0!
        Me.cmdexit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.cmdexit.FocusPtTracker = DesignerRectTracker8
        Me.cmdexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.SystemColors.InfoText
        Me.cmdexit.Image = Nothing
        Me.cmdexit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdexit.ImageIndex = 0
        Me.cmdexit.ImageSize = New System.Drawing.Size(16, 16)
        Me.cmdexit.Location = New System.Drawing.Point(849, 15)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.cmdexit.SideImage = Nothing
        Me.cmdexit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdexit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.cmdexit.Size = New System.Drawing.Size(66, 29)
        Me.cmdexit.TabIndex = 236
        Me.cmdexit.Tag = "2"
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdexit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.cmdexit.TextMargin = New System.Windows.Forms.Padding(0)
        Me.cmdexit.TextShadow = System.Drawing.Color.LightGray
        '
        'txtno
        '
        Me.txtno.Location = New System.Drawing.Point(82, 14)
        Me.txtno.Name = "txtno"
        Me.txtno.Size = New System.Drawing.Size(129, 20)
        Me.txtno.TabIndex = 237
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(22, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 21)
        Me.Label3.TabIndex = 238
        Me.Label3.Text = "Docnum"
        '
        'CMBYR
        '
        Me.CMBYR.FormattingEnabled = True
        Me.CMBYR.Location = New System.Drawing.Point(482, 12)
        Me.CMBYR.Name = "CMBYR"
        Me.CMBYR.Size = New System.Drawing.Size(131, 21)
        Me.CMBYR.TabIndex = 239
        '
        'txtyno
        '
        Me.txtyno.Location = New System.Drawing.Point(476, 43)
        Me.txtyno.Name = "txtyno"
        Me.txtyno.Size = New System.Drawing.Size(126, 20)
        Me.txtyno.TabIndex = 240
        '
        'txtdocentry
        '
        Me.txtdocentry.Location = New System.Drawing.Point(83, 41)
        Me.txtdocentry.Name = "txtdocentry"
        Me.txtdocentry.Size = New System.Drawing.Size(127, 20)
        Me.txtdocentry.TabIndex = 241
        '
        'Frmincentive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 734)
        Me.Controls.Add(Me.txtdocentry)
        Me.Controls.Add(Me.txtyno)
        Me.Controls.Add(Me.CMBYR)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtno)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.cmdadd)
        Me.Controls.Add(Me.cmddel)
        Me.Controls.Add(Me.cmdupdt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.mskdateto)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.mskdatefr)
        Me.Controls.Add(Me.flx)
        Me.KeyPreview = True
        Me.Name = "Frmincentive"
        Me.Text = "Frmincentive"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.flx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents flx As AxMSFlexGridLib.AxMSFlexGrid
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents mskdateto As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mskdatefr As System.Windows.Forms.MaskedTextBox
    Friend WithEvents cmdupdt As CButtonLib.CButton
    Friend WithEvents cmddel As CButtonLib.CButton
    Friend WithEvents cmdadd As CButtonLib.CButton
    Friend WithEvents cmdexit As CButtonLib.CButton
    Friend WithEvents txtno As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CMBYR As System.Windows.Forms.ComboBox
    Friend WithEvents txtyno As System.Windows.Forms.TextBox
    Friend WithEvents txtdocentry As System.Windows.Forms.TextBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmlogin
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
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frmlogin))
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.cmdlogin = New CButtonLib.CButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtloginname = New System.Windows.Forms.TextBox
        Me.txtpasswd = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.CButton2 = New CButtonLib.CButton
        Me.CButton3 = New CButtonLib.CButton
        Me.cmbcompname = New System.Windows.Forms.ComboBox
        Me.cmbyear = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.AxMSFlexGrid1 = New AxMSFlexGridLib.AxMSFlexGrid
        CType(Me.AxMSFlexGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdlogin
        '
        Me.cmdlogin.BorderColor = System.Drawing.Color.Silver
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.cmdlogin.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.5!, 1.007168!, 1.0!}
        Me.cmdlogin.ColorFillBlend = CBlendItems1
        Me.cmdlogin.ColorFillSolid = System.Drawing.Color.Silver
        Me.cmdlogin.Corners.All = CType(6, Short)
        Me.cmdlogin.Corners.LowerLeft = CType(6, Short)
        Me.cmdlogin.Corners.LowerRight = CType(6, Short)
        Me.cmdlogin.Corners.UpperLeft = CType(6, Short)
        Me.cmdlogin.Corners.UpperRight = CType(6, Short)
        Me.cmdlogin.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.cmdlogin.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.cmdlogin.FocalPoints.CenterPtX = 0.6410257!
        Me.cmdlogin.FocalPoints.CenterPtY = 0.0!
        Me.cmdlogin.FocalPoints.FocusPtX = 0.0!
        Me.cmdlogin.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.cmdlogin.FocusPtTracker = DesignerRectTracker2
        Me.cmdlogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdlogin.ForeColor = System.Drawing.SystemColors.InfoText
        Me.cmdlogin.Image = Nothing
        Me.cmdlogin.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdlogin.ImageIndex = 0
        Me.cmdlogin.ImageSize = New System.Drawing.Size(16, 16)
        Me.cmdlogin.Location = New System.Drawing.Point(82, 191)
        Me.cmdlogin.Name = "cmdlogin"
        Me.cmdlogin.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.cmdlogin.SideImage = Nothing
        Me.cmdlogin.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdlogin.SideImageSize = New System.Drawing.Size(48, 48)
        Me.cmdlogin.Size = New System.Drawing.Size(78, 33)
        Me.cmdlogin.TabIndex = 4
        Me.cmdlogin.Text = "Login"
        Me.cmdlogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdlogin.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.cmdlogin.TextMargin = New System.Windows.Forms.Padding(0)
        Me.cmdlogin.TextShadow = System.Drawing.Color.LightGray
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(39, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Username"
        '
        'txtloginname
        '
        Me.txtloginname.BackColor = System.Drawing.SystemColors.Menu
        Me.txtloginname.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtloginname.Location = New System.Drawing.Point(163, 70)
        Me.txtloginname.Name = "txtloginname"
        Me.txtloginname.Size = New System.Drawing.Size(288, 22)
        Me.txtloginname.TabIndex = 0
        '
        'txtpasswd
        '
        Me.txtpasswd.BackColor = System.Drawing.SystemColors.Menu
        Me.txtpasswd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpasswd.Location = New System.Drawing.Point(162, 98)
        Me.txtpasswd.Name = "txtpasswd"
        Me.txtpasswd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpasswd.Size = New System.Drawing.Size(288, 22)
        Me.txtpasswd.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(38, 102)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 16)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Password"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(38, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 16)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Company Name"
        '
        'CButton2
        '
        Me.CButton2.BorderColor = System.Drawing.Color.Silver
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.CButton2.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.5!, 1.007168!, 1.0!}
        Me.CButton2.ColorFillBlend = CBlendItems2
        Me.CButton2.ColorFillSolid = System.Drawing.Color.Silver
        Me.CButton2.Corners.All = CType(6, Short)
        Me.CButton2.Corners.LowerLeft = CType(6, Short)
        Me.CButton2.Corners.LowerRight = CType(6, Short)
        Me.CButton2.Corners.UpperLeft = CType(6, Short)
        Me.CButton2.Corners.UpperRight = CType(6, Short)
        Me.CButton2.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.CButton2.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.CButton2.FocalPoints.CenterPtX = 0.6410257!
        Me.CButton2.FocalPoints.CenterPtY = 0.0!
        Me.CButton2.FocalPoints.FocusPtX = 0.0!
        Me.CButton2.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.CButton2.FocusPtTracker = DesignerRectTracker4
        Me.CButton2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CButton2.ForeColor = System.Drawing.SystemColors.InfoText
        Me.CButton2.Image = Nothing
        Me.CButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CButton2.ImageIndex = 0
        Me.CButton2.ImageSize = New System.Drawing.Size(16, 16)
        Me.CButton2.Location = New System.Drawing.Point(196, 191)
        Me.CButton2.Name = "CButton2"
        Me.CButton2.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.CButton2.SideImage = Nothing
        Me.CButton2.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CButton2.SideImageSize = New System.Drawing.Size(48, 48)
        Me.CButton2.Size = New System.Drawing.Size(78, 33)
        Me.CButton2.TabIndex = 5
        Me.CButton2.Text = "Clear All"
        Me.CButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.CButton2.TextMargin = New System.Windows.Forms.Padding(0)
        Me.CButton2.TextShadow = System.Drawing.Color.LightGray
        '
        'CButton3
        '
        Me.CButton3.BorderColor = System.Drawing.Color.Silver
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.CButton3.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.5!, 1.007168!, 1.0!}
        Me.CButton3.ColorFillBlend = CBlendItems3
        Me.CButton3.ColorFillSolid = System.Drawing.Color.Silver
        Me.CButton3.Corners.All = CType(6, Short)
        Me.CButton3.Corners.LowerLeft = CType(6, Short)
        Me.CButton3.Corners.LowerRight = CType(6, Short)
        Me.CButton3.Corners.UpperLeft = CType(6, Short)
        Me.CButton3.Corners.UpperRight = CType(6, Short)
        Me.CButton3.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.CButton3.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.CButton3.FocalPoints.CenterPtX = 0.6410257!
        Me.CButton3.FocalPoints.CenterPtY = 0.0!
        Me.CButton3.FocalPoints.FocusPtX = 0.0!
        Me.CButton3.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.CButton3.FocusPtTracker = DesignerRectTracker6
        Me.CButton3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CButton3.ForeColor = System.Drawing.SystemColors.InfoText
        Me.CButton3.Image = Nothing
        Me.CButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CButton3.ImageIndex = 0
        Me.CButton3.ImageSize = New System.Drawing.Size(16, 16)
        Me.CButton3.Location = New System.Drawing.Point(308, 191)
        Me.CButton3.Name = "CButton3"
        Me.CButton3.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.CButton3.SideImage = Nothing
        Me.CButton3.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CButton3.SideImageSize = New System.Drawing.Size(48, 48)
        Me.CButton3.Size = New System.Drawing.Size(78, 33)
        Me.CButton3.TabIndex = 6
        Me.CButton3.Text = "Exit"
        Me.CButton3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.CButton3.TextMargin = New System.Windows.Forms.Padding(0)
        Me.CButton3.TextShadow = System.Drawing.Color.LightGray
        '
        'cmbcompname
        '
        Me.cmbcompname.FormattingEnabled = True
        Me.cmbcompname.Location = New System.Drawing.Point(162, 43)
        Me.cmbcompname.Name = "cmbcompname"
        Me.cmbcompname.Size = New System.Drawing.Size(287, 21)
        Me.cmbcompname.TabIndex = 2
        '
        'cmbyear
        '
        Me.cmbyear.FormattingEnabled = True
        Me.cmbyear.Location = New System.Drawing.Point(163, 137)
        Me.cmbyear.Name = "cmbyear"
        Me.cmbyear.Size = New System.Drawing.Size(287, 21)
        Me.cmbyear.TabIndex = 3
        Me.cmbyear.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(38, 142)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 16)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Select Year"
        Me.Label4.Visible = False
        '
        'AxMSFlexGrid1
        '
        Me.AxMSFlexGrid1.Location = New System.Drawing.Point(411, 201)
        Me.AxMSFlexGrid1.Name = "AxMSFlexGrid1"
        Me.AxMSFlexGrid1.OcxState = CType(resources.GetObject("AxMSFlexGrid1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMSFlexGrid1.Size = New System.Drawing.Size(75, 23)
        Me.AxMSFlexGrid1.TabIndex = 11
        Me.AxMSFlexGrid1.Visible = False
        '
        'Frmlogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(498, 254)
        Me.Controls.Add(Me.AxMSFlexGrid1)
        Me.Controls.Add(Me.cmbyear)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbcompname)
        Me.Controls.Add(Me.CButton3)
        Me.Controls.Add(Me.CButton2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtpasswd)
        Me.Controls.Add(Me.txtloginname)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdlogin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frmlogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        CType(Me.AxMSFlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdlogin As CButtonLib.CButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtloginname As System.Windows.Forms.TextBox
    Friend WithEvents txtpasswd As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CButton2 As CButtonLib.CButton
    Friend WithEvents CButton3 As CButtonLib.CButton
    Friend WithEvents cmbcompname As System.Windows.Forms.ComboBox
    Friend WithEvents cmbyear As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents AxMSFlexGrid1 As AxMSFlexGridLib.AxMSFlexGrid
    'Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    'Friend WithEvents RectangleShape3 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    'Friend WithEvents RectangleShape2 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    'Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    'Friend WithEvents RectangleShape4 As Microsoft.VisualBasic.PowerPacks.RectangleShape

End Class

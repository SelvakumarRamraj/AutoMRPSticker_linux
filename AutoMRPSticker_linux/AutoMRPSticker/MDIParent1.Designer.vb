<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDIParent1
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
        Me.components = New System.ComponentModel.Container()
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.MKTWHSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BarcodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewBarcodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OldBarcodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutoBarcodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PackagesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProfessionalCourierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BundleCourierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BundleweightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GatepassToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransportPrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SummaryReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TripSummaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GatePassSummaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ColorMasterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PRODWHSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProdBarcodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductionDespatchPrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ProductionQRCodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MKTWHSToolStripMenuItem, Me.PRODWHSToolStripMenuItem, Me.QuitToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(1434, 24)
        Me.MenuStrip.TabIndex = 5
        Me.MenuStrip.Text = "MenuStrip"
        '
        'MKTWHSToolStripMenuItem
        '
        Me.MKTWHSToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BarcodeToolStripMenuItem, Me.AutoBarcodeToolStripMenuItem, Me.PackagesToolStripMenuItem, Me.ProfessionalCourierToolStripMenuItem, Me.BundleCourierToolStripMenuItem, Me.TransportToolStripMenuItem, Me.ColorMasterToolStripMenuItem})
        Me.MKTWHSToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MKTWHSToolStripMenuItem.Name = "MKTWHSToolStripMenuItem"
        Me.MKTWHSToolStripMenuItem.Size = New System.Drawing.Size(76, 20)
        Me.MKTWHSToolStripMenuItem.Text = "MKT WHS"
        '
        'BarcodeToolStripMenuItem
        '
        Me.BarcodeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewBarcodeToolStripMenuItem, Me.BarToolStripMenuItem, Me.OldBarcodeToolStripMenuItem, Me.ProductionQRCodeToolStripMenuItem})
        Me.BarcodeToolStripMenuItem.Name = "BarcodeToolStripMenuItem"
        Me.BarcodeToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.BarcodeToolStripMenuItem.Text = "Barcode"
        '
        'NewBarcodeToolStripMenuItem
        '
        Me.NewBarcodeToolStripMenuItem.Name = "NewBarcodeToolStripMenuItem"
        Me.NewBarcodeToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.NewBarcodeToolStripMenuItem.Text = "New Barcode"
        '
        'BarToolStripMenuItem
        '
        Me.BarToolStripMenuItem.Name = "BarToolStripMenuItem"
        Me.BarToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.BarToolStripMenuItem.Text = "Barcode Settings"
        '
        'OldBarcodeToolStripMenuItem
        '
        Me.OldBarcodeToolStripMenuItem.Name = "OldBarcodeToolStripMenuItem"
        Me.OldBarcodeToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.OldBarcodeToolStripMenuItem.Text = "Old Barcode"
        '
        'AutoBarcodeToolStripMenuItem
        '
        Me.AutoBarcodeToolStripMenuItem.Name = "AutoBarcodeToolStripMenuItem"
        Me.AutoBarcodeToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.AutoBarcodeToolStripMenuItem.Text = "Auto Barcode"
        '
        'PackagesToolStripMenuItem
        '
        Me.PackagesToolStripMenuItem.Name = "PackagesToolStripMenuItem"
        Me.PackagesToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.PackagesToolStripMenuItem.Text = "Packages"
        '
        'ProfessionalCourierToolStripMenuItem
        '
        Me.ProfessionalCourierToolStripMenuItem.Name = "ProfessionalCourierToolStripMenuItem"
        Me.ProfessionalCourierToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.ProfessionalCourierToolStripMenuItem.Text = "Professional Courier"
        '
        'BundleCourierToolStripMenuItem
        '
        Me.BundleCourierToolStripMenuItem.Name = "BundleCourierToolStripMenuItem"
        Me.BundleCourierToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.BundleCourierToolStripMenuItem.Text = "Bundle Courier"
        '
        'TransportToolStripMenuItem
        '
        Me.TransportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BundleweightToolStripMenuItem, Me.GatepassToolStripMenuItem1, Me.PrintToolStripMenuItem, Me.ReportsToolStripMenuItem})
        Me.TransportToolStripMenuItem.Name = "TransportToolStripMenuItem"
        Me.TransportToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.TransportToolStripMenuItem.Text = "Transport"
        '
        'BundleweightToolStripMenuItem
        '
        Me.BundleweightToolStripMenuItem.Name = "BundleweightToolStripMenuItem"
        Me.BundleweightToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.BundleweightToolStripMenuItem.Text = "Bundle Weight"
        '
        'GatepassToolStripMenuItem1
        '
        Me.GatepassToolStripMenuItem1.Name = "GatepassToolStripMenuItem1"
        Me.GatepassToolStripMenuItem1.Size = New System.Drawing.Size(157, 22)
        Me.GatepassToolStripMenuItem1.Text = "Gatepass"
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TransportPrintToolStripMenuItem})
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.PrintToolStripMenuItem.Text = "Print"
        '
        'TransportPrintToolStripMenuItem
        '
        Me.TransportPrintToolStripMenuItem.Name = "TransportPrintToolStripMenuItem"
        Me.TransportPrintToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.TransportPrintToolStripMenuItem.Text = "Transport Print"
        '
        'ReportsToolStripMenuItem
        '
        Me.ReportsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SummaryReportsToolStripMenuItem, Me.TripSummaryToolStripMenuItem, Me.GatePassSummaryToolStripMenuItem})
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        Me.ReportsToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.ReportsToolStripMenuItem.Text = "Reports"
        '
        'SummaryReportsToolStripMenuItem
        '
        Me.SummaryReportsToolStripMenuItem.Name = "SummaryReportsToolStripMenuItem"
        Me.SummaryReportsToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.SummaryReportsToolStripMenuItem.Text = "Summary Reports"
        '
        'TripSummaryToolStripMenuItem
        '
        Me.TripSummaryToolStripMenuItem.Name = "TripSummaryToolStripMenuItem"
        Me.TripSummaryToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.TripSummaryToolStripMenuItem.Text = "Trip Summary"
        '
        'GatePassSummaryToolStripMenuItem
        '
        Me.GatePassSummaryToolStripMenuItem.Name = "GatePassSummaryToolStripMenuItem"
        Me.GatePassSummaryToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.GatePassSummaryToolStripMenuItem.Text = "Gate Pass Summary"
        '
        'ColorMasterToolStripMenuItem
        '
        Me.ColorMasterToolStripMenuItem.Name = "ColorMasterToolStripMenuItem"
        Me.ColorMasterToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.ColorMasterToolStripMenuItem.Text = "Color Master"
        '
        'PRODWHSToolStripMenuItem
        '
        Me.PRODWHSToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProdBarcodeToolStripMenuItem, Me.ProductionDespatchPrintToolStripMenuItem})
        Me.PRODWHSToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PRODWHSToolStripMenuItem.Name = "PRODWHSToolStripMenuItem"
        Me.PRODWHSToolStripMenuItem.Size = New System.Drawing.Size(83, 20)
        Me.PRODWHSToolStripMenuItem.Text = "PROD WHS"
        '
        'ProdBarcodeToolStripMenuItem
        '
        Me.ProdBarcodeToolStripMenuItem.Name = "ProdBarcodeToolStripMenuItem"
        Me.ProdBarcodeToolStripMenuItem.Size = New System.Drawing.Size(220, 22)
        Me.ProdBarcodeToolStripMenuItem.Text = "Prod Barcode"
        '
        'ProductionDespatchPrintToolStripMenuItem
        '
        Me.ProductionDespatchPrintToolStripMenuItem.Name = "ProductionDespatchPrintToolStripMenuItem"
        Me.ProductionDespatchPrintToolStripMenuItem.Size = New System.Drawing.Size(220, 22)
        Me.ProductionDespatchPrintToolStripMenuItem.Text = "Production Despatch Print"
        '
        'QuitToolStripMenuItem
        '
        Me.QuitToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
        Me.QuitToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.QuitToolStripMenuItem.Text = "Quit"
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 759)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1434, 22)
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip"
        '
        'ToolStripStatusLabel
        '
        Me.ToolStripStatusLabel.Name = "ToolStripStatusLabel"
        Me.ToolStripStatusLabel.Size = New System.Drawing.Size(39, 17)
        Me.ToolStripStatusLabel.Text = "Status"
        '
        'ProductionQRCodeToolStripMenuItem
        '
        Me.ProductionQRCodeToolStripMenuItem.Name = "ProductionQRCodeToolStripMenuItem"
        Me.ProductionQRCodeToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.ProductionQRCodeToolStripMenuItem.Text = "Production QR Code"
        '
        'MDIParent1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1434, 781)
        Me.Controls.Add(Me.MenuStrip)
        Me.Controls.Add(Me.StatusStrip)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip
        Me.Name = "MDIParent1"
        Me.Text = "ACC BARCODE "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents MKTWHSToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BarcodeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AutoBarcodeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PRODWHSToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProdBarcodeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents QuitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PackagesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProductionDespatchPrintToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProfessionalCourierToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BundleCourierToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TransportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BundleweightToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GatepassToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents PrintToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TransportPrintToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SummaryReportsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TripSummaryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GatePassSummaryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ColorMasterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewBarcodeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OldBarcodeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProductionQRCodeToolStripMenuItem As ToolStripMenuItem
End Class

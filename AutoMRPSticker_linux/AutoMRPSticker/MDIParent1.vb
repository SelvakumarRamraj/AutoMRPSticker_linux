Imports System.Windows.Forms

Public Class MDIParent1

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
        ' Create a new instance of the child form.
        Dim ChildForm As New System.Windows.Forms.Form
        ' Make it a child of this MDI form before showing it.
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "Window " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub



    Private m_ChildFormNumber As Integer

    Private Sub BarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarcodeToolStripMenuItem.Click
        'Dim obj As New Frmmktbarcodenormal
        'obj.MdiParent = Me
        'obj.Show()
        'obj.WindowState = FormWindowState.Normal   ' Reset first
        'obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub AutoBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutoBarcodeToolStripMenuItem.Click
        Dim obj As New Frmmktbarcode
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub QuitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click
        End
    End Sub

    Private Sub PackagesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PackagesToolStripMenuItem.Click
        Dim obj As New Frmpackage
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ProductionDespatchPrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductionDespatchPrintToolStripMenuItem.Click
        Dim obj As New FrmProddespatchprn
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub MenuStrip_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip.ItemClicked

    End Sub

    Private Sub ProdBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProdBarcodeToolStripMenuItem.Click
        Dim obj As New RRQRCODE
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub BundleCourierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BundleCourierToolStripMenuItem.Click
        Dim obj As New Frmprofcourwgt
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ProfessionalCourierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProfessionalCourierToolStripMenuItem.Click
        Dim obj As New Frmprofcourier
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub BundleweightToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BundleweightToolStripMenuItem.Click
        Dim obj As New frmBundleWeight
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub GatepassToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles GatepassToolStripMenuItem1.Click
        Dim obj As New frmgatepassdatagrid
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub TransportPrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransportPrintToolStripMenuItem.Click
        Dim obj As New frmTransportPrint
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub SummaryReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SummaryReportsToolStripMenuItem.Click
        Dim obj As New frmSummary
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub TripSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TripSummaryToolStripMenuItem.Click
        Dim obj As New mgatepass  'frmMultiGatepass Print
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub GatePassSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GatePassSummaryToolStripMenuItem.Click
        Dim obj As New frmGPSummary 'frmMultiGatepass Print
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ColorMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ColorMasterToolStripMenuItem.Click
        Dim obj As New Frmcolormaster 'frmMultiGatepass Print
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub BarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BarToolStripMenuItem.Click
        Dim obj As New frmbarsettings
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub NewBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewBarcodeToolStripMenuItem.Click
        Dim obj As New Frmmktbarcodenormal
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub OldBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OldBarcodeToolStripMenuItem.Click
        Dim obj As New frmbarcode
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ProductionQRCodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductionQRCodeToolStripMenuItem.Click
        Dim obj As New RRQRCODE
        obj.MdiParent = Me
        obj.Show()
        obj.WindowState = FormWindowState.Normal   ' Reset first
        obj.WindowState = FormWindowState.Maximized
    End Sub
End Class

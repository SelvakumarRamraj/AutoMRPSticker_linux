
Public Class MDIFORM1
    Dim i As Int16
    Private Sub BarCodeSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New frmbarsettings
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    


    Private Sub MDIFORM1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        For i = System.Windows.Forms.Application.OpenForms.Count - 1 To 1 Step -1
            Dim form As Form = System.Windows.Forms.Application.OpenForms(i)
            form.Close()
        Next i
        End
    End Sub

    Private Sub MDIFORM1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.WindowsDefaultLocation
        Me.Height = My.Computer.Screen.Bounds.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        Me.Text = dbnam
        'Me.BarCodeSettingsToolStripMenuItem.Enabled = False
        'Me.BarcodePrintToolStripMenuItem.Enabled = False
        'Me.ToolStripMenuItem1.Enabled = False
        Me.ToolStripMenuItem6.Enabled = True
        '*courier
        'Me.ToolStripMenuItem2.Enabled = False

    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New Frmprofcourier
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New frmteamdist
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        'Dim OBJ As New Frmincentive
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New Frmpackage
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub ToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem6.Click
       


    End Sub

    Private Sub ShowRoomBarcodeDownloadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New Frmsrbarcode
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub BARCODEBOMToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New FrmBOM
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub CONTENTBOMToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New frmcontbom
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub LocalBarcodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New frmbarcode

        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub RemoteBarcodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New Frmbarcoderemote1

        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub OrderInwardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New Frmorderprn
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub AntsPackagingEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New frmantspacking
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub AntsProcessBarcodePrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim OBJ As New frmantsprocessbarcode
        OBJ.Show()
        OBJ.MdiParent = Me
    End Sub

    Private Sub AntsProcessDeliveryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New Frmantscan
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub COLORMASTERToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New Frmcolormaster
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub STOCKREPORTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New Frmstockreport
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub SALESREPORTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New frmsalerep
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub AntsAttendanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim OBJ As New frmatt
        OBJ.Show()
        OBJ.MdiParent = Me
    End Sub

    Private Sub ExportPNLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New Frmpnlconsolidate
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub ToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New salesordermatured
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub MailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MailToolStripMenuItem.Click
        'Dim OBJ As New Frmmail
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub ColorStockMailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ColorStockMailToolStripMenuItem.Click
        'Dim OBJ As New frmcolorstockmail
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub SalesOrderScanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New Frmpurcscan
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub GSTR1ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim OBJ As New frmoperperform
        OBJ.Show()
        OBJ.MdiParent = Me
    End Sub

    Private Sub ACCCOSTATTENDANCEToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    

    Private Sub CostAttendanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New Frmcostattlog
        'OBJ.Show()
        'OBJ.MdiParent = Me

    End Sub

    Private Sub CostemployeemasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New frmcostempmaster
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub COSTReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New Frmcostreport
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub PerformanceEfficientToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim OBJ As New Frmperfeff
        OBJ.Show()
        OBJ.MdiParent = Me
    End Sub


    Private Sub QuitToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitToolStripMenuItem1.Click
        For i = System.Windows.Forms.Application.OpenForms.Count - 1 To 1 Step -1
            Dim form As Form = System.Windows.Forms.Application.OpenForms(i)
            form.Close()
        Next i

        End
    End Sub

    Private Sub inv_printToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New Frminvprn
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub QRBarcodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New RRQRCODE
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub RTGSMailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RTGSMailToolStripMenuItem.Click
        'Dim OBJ As New FrmRTGSpymt
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub ToolStripMenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem9.Click
        Dim OBJ As New Frmopermaster
        OBJ.Show()
        OBJ.MdiParent = Me
    End Sub

    Private Sub ToolStripMenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem10.Click
        Dim OBJ As New Frmstylemaster
        OBJ.Show()
        OBJ.MdiParent = Me
    End Sub

    Private Sub EmployeeMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmployeeMasterToolStripMenuItem.Click
        Dim OBJ As New Frmempmaster
        OBJ.Show()
        OBJ.MdiParent = Me
    End Sub

    Private Sub PerfDataEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PerfDataEntryToolStripMenuItem.Click
        Dim OBJ As New Frmperfdataentry
        OBJ.Show()
        OBJ.MdiParent = Me
    End Sub

    Private Sub IncentiveCalcToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IncentiveCalcToolStripMenuItem.Click
        Dim OBJ As New Frmincentivecalc
        OBJ.Show()
        OBJ.MdiParent = Me
    End Sub

    Private Sub VehicleMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim OBJ As New Frmvehiclemaster
        'OBJ.Show()
        'OBJ.MdiParent = Me
    End Sub

    Private Sub ToolStripMenuItem1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        
    End Sub

    Private Sub ProcessRateMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProcessRateMasterToolStripMenuItem.Click
        Dim OBJ As New Frmprocessratemast
        OBJ.Show()
        OBJ.MdiParent = Me
    End Sub

    Private Sub DailyContractEntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DailyContractEntryToolStripMenuItem.Click
        Dim OBJ As New Frmdailycontractdata
        OBJ.Show()
        OBJ.MdiParent = Me
    End Sub

    Private Sub IEReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IEReportToolStripMenuItem.Click
        Dim OBJ As New FrmIEReport
        OBJ.Show()
        OBJ.MdiParent = Me
    End Sub

    Private Sub LinewiseLoadingReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinewiseLoadingReportToolStripMenuItem.Click
        Dim obj As New frmlineloadrep
        obj.Show()
        obj.MdiParent = Me
    End Sub

    Private Sub ToolStripMenuItem2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click

    End Sub

    Private Sub DdToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DdToolStripMenuItem.Click
        Dim obj As New Frmincentivegentstailor
        obj.Show()
        obj.MdiParent = Me
    End Sub

    Private Sub ProductionReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductionReportToolStripMenuItem.Click
        Dim obj As New FrmREPORTIE_PROD
        obj.Show()
        obj.MdiParent = Me
    End Sub

    Private Sub IEWIPReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IEWIPReportToolStripMenuItem.Click
        Dim obj As New Frmiewiprep
        obj.Show()
        obj.MdiParent = Me
    End Sub

    Private Sub LinewiseProductionCosolidateReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinewiseProductionCosolidateReportToolStripMenuItem.Click
        Dim obj As New frmlineprodconsolidaterep
        obj.Show()
        obj.MdiParent = Me
    End Sub

    Private Sub StitchingTargetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StitchingTargetToolStripMenuItem.Click
        Dim obj As New Frmsttarget
        obj.Show()
        obj.MdiParent = Me
    End Sub

    Private Sub LineCostToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LineCostToolStripMenuItem.Click
        Dim obj As New FrmLineCost
        obj.Show()
        obj.MdiParent = Me
    End Sub

    Private Sub ShirtDailyEntryToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ShirtDailyEntryToolStripMenuItem.Click
        Dim obj As New FrmShirtdailydata

        obj.MdiParent = Me
        obj.Show()
    End Sub

    Private Sub TailorAttendanceToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TailorAttendanceToolStripMenuItem.Click
        Dim obj As New Frmtailoratt
        obj.Show()
        obj.MdiParent = Me
    End Sub
End Class
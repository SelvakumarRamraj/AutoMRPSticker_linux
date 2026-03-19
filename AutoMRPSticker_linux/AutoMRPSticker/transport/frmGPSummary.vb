Public Class frmGPSummary
    Dim cryptfile As String
    Private Sub frm1Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frm1Refresh.Click
        'Call MAIN()

        'Dim cryRpt1 As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
        'cryptfile = loadrptdb2(Trim(dbGPSU), Trim(dbreportpath))
        'cryRpt1.Load(cryptfile)
        'CrystalReportLogOn(cryRpt1, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))
        'cryRpt1.SetParameterValue("@fromdate", dt.Value.ToString("yyyy-MM-dd"))
        'cryRpt1.SetParameterValue("@todate", dt1.Value.ToString("yyyy-MM-dd"))
        'CrystalReportViewer1.ReportSource = cryRpt1
        'CrystalReportViewer1.PrintReport()
        'CrystalReportViewer1.Refresh()

        Dim paramDict As New Dictionary(Of String, Object)
        paramDict("@fromdate") = dt.Value.ToString("yyyy-MM-dd")
        paramDict("@todate") = dt1.Value.ToString("yyyy-MM-dd")
        'paramDict("CustomerId") = customerId

        ' Build request


        Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

        Dim req As New PrintRequest() With {
             .ReportName = dbGPSU,
             .PrinterName = If(isPrint, mlsprinter, ""),     ' "" = preview
             .UseDB = True,
             .ServerName = mdbserver,
             .DatabaseName = mdbname,
             .DBUser = mdbuserid,
             .DBPassword = mdbpwd,
             .Parameters = paramDict
        }
        'Dim success As Boolean = CallCrystalPrintService(req)

        Dim success As Boolean
        success = PrintCrystalReport(req, isPrint)

    End Sub

    Private Sub GPSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Btnexit_Click(sender As Object, e As EventArgs) Handles Btnexit.Click
        Me.Close()
    End Sub
End Class
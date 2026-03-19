Public Class mgatepass
    Dim cryptfile As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'live
        'Call MAIN()
        'Me.Cursor = Cursors.WaitCursor

        'Dim cryRpt3 As New CrystalDecisions.CrystalReports.Engine.ReportDocument()

        ''      cryRpt3.Load(Trim(dbreportpath) & Trim(dbtripsummary))


        'cryptfile = loadrptdb2(Trim(dbtripsummary), Trim(dbreportpath))
        'cryRpt3.Load(cryptfile)


        'CrystalReportLogOn(cryRpt3, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))
        'cryRpt3.Refresh()

        'cryRpt3.SetParameterValue("dockey", txtdrvname.Text.Trim)
        'CrystalReportViewer1.Visible = True
        'Me.CrystalReportViewer1.ReportSource = cryRpt3
        'Me.CrystalReportViewer1.PrintReport()
        'Me.CrystalReportViewer1.Refresh()

        Dim mreportname As String = Trim(dbtripsummary)
        Dim paramDict As New Dictionary(Of String, Object)
        paramDict.Add("dockey", txtdrvname.Text.Trim)
        ' paramDict("Dockey@") = Val(Label7.Text)


        Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

        Dim req As New PrintRequest() With {
             .ReportName = mreportname,
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

        Me.Cursor = Cursors.Default
    End Sub


    Private Sub mgatepass_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtdrvname_TextChanged(sender As Object, e As EventArgs) Handles txtdrvname.TextChanged

    End Sub
End Class
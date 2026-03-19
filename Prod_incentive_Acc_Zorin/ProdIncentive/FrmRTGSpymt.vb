Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports System.Net.Mail
'Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates


Public Class FrmRTGSpymt
    Dim msql As String
    Dim cryRpt As New ReportDocument
    Dim cryptfile As String
    Dim mtotal As Double
    Private Shared Function customCertValidation(ByVal sender As Object, _
                                                ByVal cert As X509Certificate, _
                                                ByVal chain As X509Chain, _
                                                ByVal errors As SslPolicyErrors) As Boolean

        Return True

    End Function
    Private Sub FrmRTGSpymt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        mskdatefr.Text = Microsoft.VisualBasic.Format(Now, "dd-MM-yyyy")
        mskdateto.Text = Microsoft.VisualBasic.Format(Now, "dd-MM-yyyy")
        loadcombo("opwz", "wizardname", cmbpwzname, "wizardname")
    End Sub
    Private Sub loaddata()
        msql = "select ROW_NUMBER() over(order by b.docnum) sno,b.DocNum, f.rctid,d.e_mail, CASE when len(rtrim(ltrim(isnull(d.CardFName,''))))>0 then d.CardFName else b.CardName end Party," & vbCrLf _
               & " bankacct AccountNo,d.DflSwift IFSCCode,b.doctotal Amount,f.IdNumber paywzno,f.WizardName,f.PmntDate paywzdate   from OVPM b " & vbCrLf _
               & "left join ODSC c on c.BankCode=b.BankCode " & vbCrLf _
               & "left join ocrd d on d.CardCode=b.CardCode " & vbCrLf _
               & "Left Join (select a.IdNumber, b.RctId, a.PmntDate,b.CardCode,b.CardName,b.PymAmount,a.WizardName from OPWZ a " & vbCrLf _
                  & "left join PWZ4 b on b.IdEntry=a.IdNumber) f on f.RctId=b.DocEntry" & vbCrLf _
               & "where b.PayMth='RTGS' and f.PmntDate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and f.PmntDate<='" & Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "'"
        If Len(Trim(cmbpwzname.Text)) > 0 Then
            msql = msql & " and f.wizardname='" & cmbpwzname.Text & "'"
        End If
        mtotal = 0
        Dim dt As DataTable = getDataTable(msql)
        dg.Rows.Clear()
        'dg.DataSource = dt

        For Each row As DataRow In dt.Rows

            dg.Rows.Add()
            dg.Rows(dg.RowCount - 1).Cells(1).Value = row("sno")
            dg.Rows(dg.RowCount - 1).Cells(2).Value = row("docnum")
            dg.Rows(dg.RowCount - 1).Cells(3).Value = row("rctid")
            dg.Rows(dg.RowCount - 1).Cells(4).Value = row("e_mail")
            dg.Rows(dg.RowCount - 1).Cells(5).Value = row("party")
            dg.Rows(dg.RowCount - 1).Cells(6).Value = row("accountno")
            dg.Rows(dg.RowCount - 1).Cells(7).Value = row("ifsccode")
            dg.Rows(dg.RowCount - 1).Cells(8).Value = Format(row("amount"), "#,##,##,##,##,##0.00")
            dg.Rows(dg.RowCount - 1).Cells(9).Value = row("paywzno")
            dg.Rows(dg.RowCount - 1).Cells(10).Value = row("WizardName")
            dg.Rows(dg.RowCount - 1).Cells(11).Value = Format(row("paywzdate"), "dd-MM-yyyy")
            'dg.Rows(dg.RowCount - 1).DefaultCellStyle.ForeColor = Color.Black
            mtotal = mtotal + Val(row("amount"))
        Next
        dg.Rows.Add()
        dg.Rows.Add()


        dg.Rows(dg.RowCount - 1).Cells(4).Value = "Total ...."
        dg.Rows(dg.RowCount - 1).Cells(8).Value = Format(mtotal, "#,##,##,##,##,##0.00")

        Dim style As New DataGridViewCellStyle()
        style.Font = New Font(dg.Font, FontStyle.Bold)
        dg.Rows(dg.RowCount - 1).DefaultCellStyle = style

        'For Each row As DataRow In dt.Rows
        '    lbldate.Text = Format(row("docdate"), "dd-MM-yyyy")
        '    If row("u_ordtype") = "SO" Then
        '        chkscheme.Checked = True
        '    Else
        '        chkscheme.Checked = False
        '    End If
        'Next

    End Sub

    Private Sub cmdexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexport.Click
        Call loaddata()
    End Sub


     

    Private Sub cmdmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdmail.Click
        'pb.Maximum = 100
        'pb.Value = 0
        'For i As Integer = 0 To dg.RowCount - 1
        '    If dg.Item(0, i).Value = "True" Then

        '        If Len(Trim(dg.Rows(i).Cells(4).Value)) > 0 Then

        '            loadmail(dg.Rows(i).Cells(3).Value, mmailid, mpdffile, dg.Rows(i).Cells(2).Value, dg.Rows(i).Cells(4).Value)
        '            pb.Value = pb.Value + 1
        '        End If
        '        'crystalinv(Val(dg.Rows(i).Cells(3).Value), mfwrepname)
        '    End If
        'Next
    End Sub

    'Private Sub crystalinv(ByVal mdocentry As Integer, ByVal mrepname As String)

    '    Me.Cursor = Cursors.WaitCursor
    '    Dim cryRpt As New ReportDocument()
    '    'cryRpt.Load(Trim(mreppath) & "GST PREINVOICE cor.rpt")
    '    cryRpt.Load(Trim(mreppath) & Trim(mrepname))
    '    CrystalReportLogOn(cryRpt, Trim(mkserver), dbnam, Trim(dbuser), Trim(mkpwd))
    '    cryRpt.SetParameterValue("Dockey@", mdocentry)

    '    Me.view1.ReportSource = cryRpt
    '    Me.view1.PrintReport()
    '    'view1.PrintToPrinter(1, False, 1, 1)
    '    'Me.View1.ReportSource = cryRpt
    '    Me.view1.Refresh()
    '    cryRpt.Dispose()
    '    Me.Cursor = Cursors.Default

    'End Sub
    'Private Sub loadmail(ByVal mdocentry As String, ByVal mailid As String, ByVal pdffilenam As String, ByVal mdocnum As String, ByVal tomailid As String)

    '    Dim attachment As System.Net.Mail.Attachment
    '    cryptfile = loadrptdb2(Trim(mrdoccode), Trim(mreppath))

    '    Dim pdfFile As String = (Trim(mreppath) + Trim(mpdffile) + Trim(mdocnum) + "_" + Trim(mdocentry) + ".pdf")

    '    If System.IO.File.Exists(pdfFile) = True Then
    '        System.IO.File.Delete(pdfFile)
    '    End If


    '    cryRpt.Load(cryptfile)
    '    cryRpt.Refresh()
    '    CrystalReportLogOn(cryRpt, Trim(mkserver), Trim(dbnam), Trim(dbuser), Trim(mkpwd))
    '    cryRpt.SetParameterValue("DocKey@", Val(mdocentry))
    '    view1.ReportSource = cryRpt
    '    view1.Refresh()

    '    Try
    '        Dim CrExportOptions As ExportOptions
    '        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
    '        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions
    '        CrDiskFileDestinationOptions.DiskFileName = pdfFile
    '        CrExportOptions = cryRpt.ExportOptions
    '        With CrExportOptions
    '            .ExportDestinationType = ExportDestinationType.DiskFile
    '            .ExportFormatType = ExportFormatType.PortableDocFormat
    '            .DestinationOptions = CrDiskFileDestinationOptions
    '            .FormatOptions = CrFormatTypeOptions
    '        End With
    '        cryRpt.Export()
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try

    '    Dim Mail As New MailMessage

    '    attachment = New System.Net.Mail.Attachment(pdfFile)
    '    Mail.Attachments.Add(attachment)
    '    Mail.Subject = Trim(txtsubj.Text)
    '    Mail.To.Add(tomailid)
    '    Mail.CC.Add(mccmailid)
    '    'Mail.Bcc.Add(ds.Tables(0).Rows(i).Item("BCCEMailID"))
    '    'Mail.Bcc.Add("sap.randr@ramrajcotton.net")

    '    Mail.From = New MailAddress(mmailusername, mmailid)
    '    Mail.Body = Trim(txtbody.Text)

    '    ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf customCertValidation)

    '    Dim SMTP As New SmtpClient(msmtp)
    '    SMTP.EnableSsl = False
    '    SMTP.Credentials = New System.Net.NetworkCredential(mmailusername, mmailpwd)
    '    SMTP.Port = mport
    '    Try
    '        SMTP.Send(Mail)
    '        SMTP.Timeout = 11000
    '        'con.Close()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    '    ' Me.Close()

    'End Sub


    ' cryptfile = loadrptdb2(Trim(dbreports), Trim(dbreportpath))

    'Dim pdfFile As String = (Trim(dbreportpath) + Trim(dbreppdf) + ds.Tables(0).Rows(i).Item("Date") + ".pdf")

    '    If System.IO.File.Exists(pdfFile) = True Then
    '        System.IO.File.Delete(pdfFile)
    '    End If

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        For i As Integer = 0 To dg.RowCount - 1
            If IsDBNull(dg.Rows(i).Cells(4).Value) = False Then
                If Mid(dg.Rows(i).Cells(4).Value, 1, 5) = "Total" Then
                Else
                    dg.Rows(i).Cells(0).Value = "True"
                End If
            End If

            'dg.Rows(i).Cells(0).Value = "True"
        Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        For i As Integer = 0 To dg.RowCount - 1
            If IsDBNull(dg.Rows(i).Cells(4).Value) = False Then
                If Mid(dg.Rows(i).Cells(4).Value, 1, 5) = "Total" Then
                Else
                    dg.Rows(i).Cells(0).Value = "False"
                End If
            End If
        Next
    End Sub
End Class
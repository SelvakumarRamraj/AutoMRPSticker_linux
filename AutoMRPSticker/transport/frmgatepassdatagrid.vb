Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.ReportSource
'Imports CrystalDecisions.CrystalReports.Engine.Section
'Imports CrystalDecisions.CrystalReports.Engine.Sections
Imports System.Net.IPHostEntry
Imports System.Net.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Net.Mail.MailMessage
Imports System.Net.Mail.Attachment
Imports System.Collections.Specialized
Imports System.Text
Imports Microsoft.VisualBasic

Imports System.Configuration
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.ComponentModel
Public Class frmgatepassdatagrid
    ' Dim cryptfile As String
    Private Sub gatepassdatagrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        checkConnection()
        Dim strHostName As String
        Dim strIPAddress As String
        strHostName = System.Net.Dns.GetHostName()
        strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(1).ToString()
        '' MessageBox.Show("Host Name: " & strHostName & "; IP Address: " & strIPAddress)
        Button2.Enabled = True
        If strIPAddress = "192.168.1.50" Then
            Button4.Visible = True
        End If
        cmbfrom.Text = mgpfrom


        mskdate.Text = Today()
        txttime.Text = Format(Now, "hh:mm tt")



        dv.AutoGenerateColumns = False
        'Call MAIN()
        Dim da As New SqlDataAdapter, ds As New DataSet
        da.SelectCommand = New SqlCommand
        da.SelectCommand.Connection = con
        da.SelectCommand.CommandType = CommandType.StoredProcedure
        da.SelectCommand.CommandText = "INSGetGatepasstransport"
        da.Fill(ds, "tbl2")
        cmbto.DataSource = ds.Tables("tbl2")
        cmbto.DisplayMember = "TRANSPORT"
        con.Close()

    End Sub


    Private Sub Dv_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dv.DataError
        If (e.Exception.Message = "DataGridViewComboBoxCell value is not valid.") Then
            Dim value As Object = dv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & vbNullString
            If Not CType(dv.Columns(e.ColumnIndex), DataGridViewComboBoxColumn).Items.Contains(Text) Then
                CType(dv.Columns(e.ColumnIndex), DataGridViewComboBoxColumn).Items.Add(Text)
                e.ThrowException = False
            End If

        End If
    End Sub




    Private Sub Sdocnum_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Sdocnum.KeyPress
        'live




        ''    Dim FirstValue As Boolean = True


        'For Each dgRow As DataGridViewRow In dv.Rows

        '    If Not FirstValue Then

        '        TextBox1.Text += ", "

        '    End If

        '    If dgRow.Cells(1).Value Is Nothing Then

        '        TextBox1.Text += ""

        '    Else
        '        TextBox1.Text += dgRow.Cells(1).Value.ToString

        '    End If

        '    FirstValue = False

        'Next


        Try

            If e.KeyChar = Convert.ToChar(13) Then

                If Label10.Text.Contains(Sdocnum.Text + ",") Then

                    MsgBox("Already Selected")

                Else

                    If dv.Rows.Count >= 40 Then

                        MsgBox("YOU CANNOT ENTRY ABOVE")

                    Else

                        dv.AutoGenerateColumns = False
                        'Call MAIN()
                        Dim da As New SqlDataAdapter, ds As New DataSet
                        da.SelectCommand = New SqlCommand
                        da.SelectCommand.Connection = con
                        da.SelectCommand.CommandType = CommandType.StoredProcedure
                        da.SelectCommand.CommandText = "INSGetGatepassSelectInvoiceno"
                        da.SelectCommand.Parameters.Add("@docnum", SqlDbType.Int).Value = Sdocnum.Text.Trim()
                        da.SelectCommand.Parameters.Add("@transport", SqlDbType.NVarChar).Value = cmbto.Text.Trim()
                        da.Fill(ds, "tbl1")

                        Dim dt As DataTable = ds.Tables("tbl1")


                        dv.Rows.Add(dv.Rows.Count, dt.Rows(0)("docnum"), dt.Rows(0)("bundle"), cmbto.Text)
                        Label9.Text = Val(Label9.Text) + Val(dt.Rows(0)("bundle"))
                        dv.Sort(dv.Columns(0), ListSortDirection.Descending)

                        Label10.Text = Label10.Text + dt.Rows(0)("docnum").ToString + ","

                        'For Each dgRow As DataGridViewRow In dv.Rows
                        '    dgRow.Cells(1).Selected = True
                        'Next

                    End If



                End If
            End If


            ''       Call cmbto_LostFocus(sender, e)




        Catch ex As Exception

            '' System.Web.HttpContext.Current.Response.Write(ex.Message)

            MsgBox(ex.Message)

        Finally
            con.Close()



        End Try
        Sdocnum.SelectedIndex = Sdocnum.FindStringExact(Sdocnum.Text)


    End Sub
    Private Sub cmbto_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbto.LostFocus
        ''  Sdocnum.DataSource = Nothing
        dv.AutoGenerateColumns = False
        'Call MAIN()
        Dim da1 As New SqlDataAdapter, ds1 As New DataSet
        da1.SelectCommand = New SqlCommand
        da1.SelectCommand.Connection = con
        da1.SelectCommand.CommandType = CommandType.StoredProcedure
        da1.SelectCommand.CommandText = "INSGetGatepassInvoiceno"
        da1.SelectCommand.Parameters.Add("@transport", SqlDbType.NVarChar).Value = cmbto.Text.Trim()
        da1.SelectCommand.Parameters.Add("@docnum", SqlDbType.VarChar).Value = Label10.Text.ToString
        da1.Fill(ds1, "tbl1")
        Sdocnum.DataSource = ds1.Tables("tbl1")
        Sdocnum.DisplayMember = "docnum"
        con.Close()
        Cursor = Cursors.Default
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.txtno.Enabled = False
        Button2.Enabled = False
        'Call MAIN()
        Dim da1 As New SqlDataAdapter, ds1 As New DataSet
        da1.SelectCommand = New SqlCommand
        da1.SelectCommand.Connection = con
        da1.SelectCommand.CommandType = CommandType.StoredProcedure
        da1.SelectCommand.CommandText = "INSGetGatepassnogen"
        da1.Fill(ds1, "tbl3")
        Dim dt1 As DataTable = ds1.Tables("tbl3")
        txtno.Text = dt1.Rows(0)("sno")
        txtno.Enabled = False
        Dim J As Integer
        For J = 0 To (dv.RowCount - 1)
            If Len(Trim(dv.Rows.Item(J).Cells(0).Value)) > 0 Then

                'Call MAIN()

                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                Dim da As New SqlDataAdapter, ds As New DataSet


                da.SelectCommand = New SqlCommand
                da.SelectCommand.Connection = con
                da.SelectCommand.CommandType = CommandType.StoredProcedure
                da.SelectCommand.CommandText = "INSGetGatepassinsert"
                da.SelectCommand.Parameters.Add("@GPid", SqlDbType.NVarChar).Value = txtno.Text
                da.SelectCommand.Parameters.Add("@GPfrom", SqlDbType.NVarChar).Value = cmbfrom.Text
                da.SelectCommand.Parameters.Add("@GPto", SqlDbType.NVarChar).Value = dv.Rows.Item(J).Cells(3).Value & vbNullString
                da.SelectCommand.Parameters.Add("@Vechicle", SqlDbType.NVarChar).Value = txtvehicleno.Text
                da.SelectCommand.Parameters.Add("@DrvName", SqlDbType.NVarChar).Value = txtdrvname.Text
                da.SelectCommand.Parameters.Add("@Selectedocnum", SqlDbType.NVarChar).Value = dv.Rows.Item(J).Cells(1).Value & vbNullString
                da.SelectCommand.Parameters.Add("@Selectedbundle", SqlDbType.NVarChar).Value = dv.Rows.Item(J).Cells(2).Value & vbNullString
                da.SelectCommand.Parameters.Add("@year", SqlDbType.NVarChar).Value = mperiod
                'da.Fill(ds, "tbl2")
                da.SelectCommand.ExecuteNonQuery()
                da.SelectCommand.Dispose()
                con.Close()
            End If
        Next


        'Dim dt As DataTable = ds.Tables("tbl2")


        'Try


        '    For j = 0 To (dv.RowCount - 1)
        '        If Len(Trim(dv.Rows.Item(j).Cells(0).Value)) > 0 Then
        '            Dim newRow As DataRow
        '            newRow = dt.NewRow()
        '            newRow("GPDATE") = mskdate.Text.Trim()
        '            newRow("GPTIME") = txttime.Text.Trim()
        '            newRow("GPfrom") = cmbfrom.Text.Trim()
        '            newRow("GPto") = cmbto.Text.Trim()
        '            newRow("Vechicle") = txtvehicleno.Text.Trim()
        '            newRow("DrvName") = txtdrvname.Text.Trim()
        '            newRow("Selectedocnum") = dv.Rows.Item(J).Cells(1).Value & vbNullString
        '            newRow("Selectedbundle") = dv.Rows.Item(J).Cells(2).Value & vbNullString
        '            dt.Rows.Add(newRow)
        '        End If
        '    Next

        '    da.Update(ds, "tbl2")
        '    dt.Dispose()
        '    da.Dispose()
        '    ds.Dispose()


        'Catch ex As SqlException
        '    MsgBox(ex.ToString)
        'End Try

        Button5_Click(sender, e)


        MsgBox("Saved SucessFully")




    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Button2.Enabled = True
        ' CrystalReportViewer1.Visible = False

        Me.txtno.Enabled = False

        'Call MAIN()
        Dim da As New SqlDataAdapter, ds As New DataSet
        da.SelectCommand = New SqlCommand
        da.SelectCommand.Connection = con
        da.SelectCommand.CommandType = CommandType.StoredProcedure
        da.SelectCommand.CommandText = "INSGetGatepassnogen"
        da.Fill(ds, "tbl2")
        Dim dt As DataTable = ds.Tables("tbl2")

        txtno.Text = dt.Rows(0)("sno")
        txtno.Enabled = False


        cmbfrom.Text = ""
        ''cmbto.Text = ""
        txtvehicleno.Text = ""
        txtdrvname.Text = ""
        mskdate.Text = Today()
        txttime.Text = Format(Now, "hh:mm tt")
        dv.DataSource = Nothing
        dv.Rows.Clear()


        cmbfrom.Text = mgpfrom ' System.Configuration.ConfigurationManager.AppSettings("GPFrom")

        con.Close()

    End Sub

    Private Sub Sdocnum_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sdocnum.SelectedIndexChanged

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'live
        Dim result As Integer = MessageBox.Show("Yes-Single Transport,No-Multi Transport", "caption", MessageBoxButtons.YesNoCancel)
        If result = System.Windows.Forms.DialogResult.Cancel Then
            ' Me.CrystalReportViewer1.Visible = False
            Me.txtno.Text = ""
            Me.txtno.Enabled = False
            Me.cmbfrom.Text = ""
            Me.cmbto.Text = ""
            Me.txtvehicleno.Text = ""
            Me.txtdrvname.Text = ""
            Me.mskdate.Text = Today()
            Me.txttime.Text = Format(Now, "hh:mm tt")
            Me.dv.DataSource = Nothing
        ElseIf result = System.Windows.Forms.DialogResult.No Then
            ' Call main()
            'Me.Cursor = Cursors.WaitCursor

            'Dim cryRpt3 As New ReportDocument()

            ''cryRpt3.Load(Trim(dbreportpath) & Trim(dbmGATEPASS))

            'cryptfile = loadrptdb2(Trim(dbmGATEPASS), Trim(dbreportpath))
            'cryRpt3.Load(cryptfile)

            'CrystalReportLogOn(cryRpt3, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))
            'cryRpt3.Refresh()

            'cryRpt3.SetParameterValue("@Dockey", Val(txtno.Text))
            ''Me.CrystalReportViewer1.Font = "Draft 10cpi, 10pt"
            'CrystalReportViewer1.Visible = True
            'Me.CrystalReportViewer1.ReportSource = cryRpt3
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()

            Dim paramDict As New Dictionary(Of String, Object)
            paramDict("@Dockey") = Val(txtno.Text)

            'paramDict("CustomerId") = customerId

            ' Build request


            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = dbmGATEPASS,
             .PrinterName = If(isPrint, mlsprinter, ""),     ' "" = preview
             .UseDB = True,
             .ServerName = mdbserver,
             .DatabaseName = mdbname,
             .DBUser = mdbuserid,
             .DBPassword = mdbpwd,
             .Parameters = paramDict
        }

            Dim success As Boolean
            success = PrintCrystalReport(req, isPrint)


            Me.Cursor = Cursors.Default
            Me.txtno.Text = ""
            Me.cmbfrom.Text = ""
            Me.cmbto.Text = ""
            Me.txtvehicleno.Text = ""
            Me.txtdrvname.Text = ""
            Me.mskdate.Text = Today()
            Me.txttime.Text = Format(Now, "hh:mm tt")
            Me.dv.DataSource = Nothing


        ElseIf result = System.Windows.Forms.DialogResult.Yes Then
            ' Call main()
            'Me.Cursor = Cursors.WaitCursor

            'Dim cryRpt3 As New ReportDocument()

            ''  cryRpt3.Load(Trim(dbreportpath) & Trim(dbGATEPASS))


            'cryptfile = loadrptdb2(Trim(dbGATEPASS), Trim(dbreportpath))
            'cryRpt3.Load(cryptfile)

            'CrystalReportLogOn(cryRpt3, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))
            'cryRpt3.Refresh()

            'cryRpt3.SetParameterValue("@Dockey", Val(txtno.Text))
            ''Me.CrystalReportViewer1.Font = "Draft 10cpi, 10pt"
            'CrystalReportViewer1.Visible = True
            'Me.CrystalReportViewer1.ReportSource = cryRpt3
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()


            Dim paramDict As New Dictionary(Of String, Object)
            paramDict("@Dockey") = Val(txtno.Text)

            'paramDict("CustomerId") = customerId

            ' Build request

            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = dbGATEPASS,
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
            Me.txtno.Text = ""
            Me.cmbfrom.Text = ""
            Me.cmbto.Text = ""
            Me.txtvehicleno.Text = ""
            Me.txtdrvname.Text = ""
            Me.mskdate.Text = Today()
            Me.txttime.Text = Format(Now, "hh:mm tt")
            Me.dv.DataSource = Nothing



        End If


        Me.txtno.Text = ""
        Me.cmbfrom.Text = ""
        Me.cmbto.Text = ""
        Me.txtvehicleno.Text = ""
        Me.txtdrvname.Text = ""
        Me.mskdate.Text = Today()
        Me.txttime.Text = Format(Now, "hh:mm tt")
        Me.dv.DataSource = Nothing




    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'live
        'Call MAIN()
        'Me.Cursor = Cursors.WaitCursor

        'Dim cryRpt3 As New ReportDocument()

        ''  cryRpt3.Load(Trim(dbreportpath) & Trim(dbGATEPASS))


        'cryptfile = loadrptdb2(Trim(dbGATEPASS), Trim(dbreportpath))
        'cryRpt3.Load(cryptfile)


        'CrystalReportLogOn(cryRpt3, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))
        'cryRpt3.Refresh()

        'cryRpt3.SetParameterValue("@Dockey", Val(txtno.Text))
        'CrystalReportViewer1.Visible = True
        'Me.CrystalReportViewer1.ReportSource = cryRpt3
        'Me.CrystalReportViewer1.PrintReport()
        'Me.CrystalReportViewer1.Refresh()

        Dim paramDict As New Dictionary(Of String, Object)
        paramDict("@Dockey") = Val(txtno.Text)

        'paramDict("CustomerId") = customerId

        ' Build request


        Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

        Dim req As New PrintRequest() With {
             .ReportName = dbGATEPASS,
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


        'Me.Cursor = Cursors.Default
    End Sub







    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'Call MAIN()
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim da As New SqlDataAdapter, ds As New DataSet
        da.SelectCommand = New SqlCommand
        da.SelectCommand.Connection = con
        da.SelectCommand.CommandType = CommandType.StoredProcedure
        da.SelectCommand.CommandText = "INSGetGatepasscancel"
        da.SelectCommand.Parameters.Add("@GPid", SqlDbType.NVarChar).Value = txtno.Text
        da.SelectCommand.ExecuteNonQuery()
        da.SelectCommand.Dispose()
        con.Close()
    End Sub






    Private Sub txtvehicleno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtvehicleno.TextChanged

    End Sub


    Private Sub Sdocnum_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Sdocnum.TextChanged

        'If Len(Sdocnum.Text) > 2 Then
        '    Call MAIN()
        '    Dim da As New SqlDataAdapter, ds As New DataSet
        '    da.SelectCommand = New SqlCommand
        '    da.SelectCommand.Connection = con1
        '    da.SelectCommand.CommandType = CommandType.StoredProcedure
        '    da.SelectCommand.CommandText = "INSGetGatepassSelectInvoiceno"
        '    da.SelectCommand.Parameters.Add("@docnum", SqlDbType.Int).Value = Sdocnum.Text.Trim()
        '    da.Fill(ds, "tbl1")
        '    Dim dt As DataTable = ds.Tables("tbl1")

        '    Sdocnum.DataSource = ds.Tables("tbl1")
        '    Sdocnum.DisplayMember = "docnum"
        '    'Call MAIN()
        '    'Dim da1 As New SqlDataAdapter, ds1 As New DataSet
        '    'da1.SelectCommand = New SqlCommand
        '    'da1.SelectCommand.Connection = con1
        '    'da1.SelectCommand.CommandType = CommandType.StoredProcedure
        '    'da1.SelectCommand.CommandText = "INSGetGatepassInvoiceno"
        '    'da1.SelectCommand.Parameters.Add("@transport", SqlDbType.NVarChar).Value = cmbto.Text.Trim()
        '    'da1.SelectCommand.Parameters.Add("@docnum", SqlDbType.VarChar).Value = Label10.Text.ToString
        '    'da1.Fill(ds1, "tbl1")
        '    'Sdocnum.DataSource = ds1.Tables("tbl1")
        '    'Sdocnum.DisplayMember = "docnum"


        '    'da1.Dispose()
        '    'ds1.Dispose()
        '    'con1.Close()
        'Else
        '    Sdocnum.Text = ""
        'End If

    End Sub

    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Me.txtno.Enabled = False
        'CrystalReportViewer1.Visible = False
        Me.txtno.Text = ""
        Me.cmbfrom.Text = ""
        Me.cmbto.Text = ""
        Me.txtvehicleno.Text = ""
        Me.txtdrvname.Text = ""
        Me.mskdate.Text = Today()
        Me.txttime.Text = Format(Now, "hh:mm tt")
        Me.dv.DataSource = Nothing
        Me.txtno.Enabled = True
    End Sub

    Private Sub cmbto_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbto.SelectedIndexChanged

    End Sub

    Private Sub dv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dv.CellContentClick

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Label9.Text = 0
        Dim i As Integer
        For i = 0 To dv.Rows.Count - 2
            Label9.Text += dv.Rows(i).Cells(2).Value
        Next
    End Sub

    Private Sub dv_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dv.RowsRemoved
        Label9.Text = 0
        Dim i As Integer
        For i = 0 To dv.Rows.Count - 2
            Label9.Text += dv.Rows(i).Cells(2).Value
        Next
    End Sub

    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click

    End Sub

    Private Sub txtno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtno.TextChanged

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub mskdate_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles mskdate.MaskInputRejected

    End Sub

    Private Sub txtdrvname_TextChanged(sender As Object, e As EventArgs) Handles txtdrvname.TextChanged

    End Sub
End Class
Imports System.IO
Imports System.IO.Ports
Imports System.Data.SqlClient
'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.ReportSource
'Imports CrystalDecisions.Shared
Imports System.Configuration
Imports System.ComponentModel
Imports System.Drawing.Printing

Public Class frmBundleWeight

    Dim msg, msg1, text1
    Dim dtsr As String
    Dim DTSR1 As String
    Dim cryptfile As String

    Private Sub txtno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtno.KeyPress

        Try
            If e.KeyChar = Convert.ToChar(13) Then
                dv.DataSource = Nothing
                dv.ColumnCount = 4

                dv.Columns(0).HeaderText = "Docnum"
                dv.Columns(0).Name = "Docnum"
                dv.Columns(0).DataPropertyName = "Docnum"
                dv.Columns(0).Width = 200

                dv.Columns(0).ReadOnly = True

                dv.Columns(1).HeaderText = "Docentry"
                dv.Columns(1).Name = "Docentry"
                dv.Columns(1).DataPropertyName = "Docentry"
                dv.Columns(1).Width = 200

                dv.Columns(1).ReadOnly = True

                dv.Columns(2).Name = "PackageNum"
                dv.Columns(2).HeaderText = "PackageNum"
                dv.Columns(2).DataPropertyName = "PackageNum"
                dv.Columns(2).Width = 200
                dv.Columns(2).ReadOnly = True

                dv.Columns(3).Name = "wgt"
                dv.Columns(3).HeaderText = "WEIGHT SCALE"
                dv.Columns(3).DataPropertyName = "wgt"
                dv.Columns(3).Width = 250
                dv.AutoGenerateColumns = False
                ' Call MAIN()
                Dim da As New SqlDataAdapter, ds As New DataSet
                da.SelectCommand = New SqlCommand
                da.SelectCommand.Connection = con
                da.SelectCommand.CommandType = CommandType.Text
                da.SelectCommand.CommandText = "select a.Docnum,a.DocEntry,isnull(b.PackageNum,1) PackageNum,isnull(b.[Weight],0) wgt from " & dtsr & "  a Left join " & DTSR1 & "  b on b.docentry = a.docentry  where isnull(PackageNum,1) <> 0 and  CASE when CONVERT(nvarchar(max),ISNULL(a.U_RefNo,''))  = '' then CONVERT(nvarchar(max),ISNULL(a.DocNum,''))  else CONVERT(nvarchar(max),ISNULL(a.U_RefNo,'')) end  = '" & txtno.Text & "' and PIndicator = '" & Trim(dbperiod) & "' Order by a.DocEntry"
                da.Fill(ds, "tbl1")
                Dim dt As DataTable = ds.Tables("tbl1")
                dv.DataSource = dt
                dv.Sort(dv.Columns(2), ListSortDirection.Ascending)
                dv.CurrentCell = dv.Rows(0).Cells(3)
                dv.BeginEdit(True)
                con.Close()
            End If
        Catch ex As Exception
            '' System.Web.HttpContext.Current.Response.Write(ex.Message)
            MsgBox(ex.Message)
            con.Close()
        Finally
        End Try
    End Sub

    Private Sub txtno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtno.TextChanged

        If Len(txtno.Text) > 0 Then
            Try
                ' Call MAIN()
                Name = "SELECT isnull(U_PASS,'') U_PASS,isnull(u_refno,'') u_refno, isnull(U_Dsnation,'') U_Dsnation,isnull(CARDNAME,'') CArdname,isnull(u_esugam,'') u_esugam, isnull(u_areacode,'') u_areacode,isnull(docentry,'') Docentry ,isnull(U_Transport,'') U_Transport,isnull(u_noofbun,'') u_noofbun,case when isnull(U_Gpno,0) > 0 then 'Gate Pass no : '+U_Gpno else '' end GPNO  FROM  " & dtsr & "   where docnum = (" & txtno.Text & ") and PIndicator = ('" & Trim(dbperiod) & "') "
                Dim CMDNAME As New SqlCommand(Name, con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                Dim DRPname As SqlDataReader
                DRPname = CMDNAME.ExecuteReader
                DRPname.Read()
                lbldocentry.Text = DRPname("docentry")
                Label2.Text = DRPname("CARDNAME")
                DRPname.Close()
                CMDNAME.Dispose()
                con.Close()
                con.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

    End Sub

    Private Sub dv_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dv.CellClick

    End Sub

    Private Sub dv_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dv.KeyDown
        If e.KeyValue = Keys.F4 Then
            Button1.Focus()
            Button1_Click(sender, e)
        End If
        If e.KeyCode = Keys.F3 Then
            Button2_Click(sender, e)
        End If
    End Sub

    Private Sub dv_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dv.KeyPress

        'If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.F9) Then
        '    Try
        '        With serialport1
        '            .PortName = "COM1"
        '            .BaudRate = 2400
        '            .Parity = Parity.None
        '            .DataBits = 8
        '            .StopBits = StopBits.One
        '            .ReadTimeout = 500
        '        End With
        '        serialport1.Open()
        '        text1 = ""
        '        msg = ""
        '        Dim message As String = serialport1.ReadLine
        '        If message.Length > 6 Then
        '            msg1 = message.Substring(2)
        '        Else
        '            msg1 = message.Substring(3)
        '        End If
        '        msg = msg1.substring(0, msg1.length - 2)
        '        msg = Val(msg)
        '        text1 = msg
        '        serialport1.Close()
        '        MsgBox(text1)
        '        serialport1.Close()
        '    Catch ex As Exception
        '        '' System.Web.HttpContext.Current.Response.Write(ex.Message)
        '        MsgBox(ex.Message)
        '    End Try
        '    Dim i As Integer
        '    For i = 0 To dv.Rows.Count - 1
        '        dv.Rows(i).Cells(3).Value() = text1
        '    Next

        'End If
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.F3) Then
            Label9.Text = 0
            Dim i As Integer
            For i = 0 To dv.Rows.Count - 1
                Label9.Text += dv.Rows(i).Cells(3).Value
            Next
            Microsoft.VisualBasic.Format(Label9.Text.ToString, "#######0.00")
            Button2_Click(e, sender)
        End If
    End Sub





    Private Sub dv_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dv.RowEnter
        Label9.Text = 0
        Dim i As Integer
        For i = 0 To dv.Rows.Count - 1
            Label9.Text += dv.Rows(i).Cells(3).Value
            Microsoft.VisualBasic.Format(Label9.Text, "#######0.00")
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        txtno.Enabled = True
        txtno.Focus()
    End Sub

    Private Sub BundleWeight_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyValue = Keys.F9 Then
            Button4.Focus()
            Button4_Click(e, sender)
        End If
        If e.KeyValue = Keys.F4 Then
            Button1.Focus()
            Button1_Click(e, sender)
        End If

    End Sub

    Private Sub BundleWeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.F9) Then
            Button4.Focus()
            Button4_Click(e, sender)
        Else
            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.F3) Then
                Label9.Text = 0
                Dim i As Integer
                For i = 0 To dv.Rows.Count - 1
                    Label9.Text += dv.Rows(i).Cells(3).Value
                    Microsoft.VisualBasic.Format(Label9.Text, "#######0.00")
                Next
                Button2_Click(e, sender)
            End If
        End If

    End Sub

    Private Sub BundleWeight_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        checkConnection()
        txtno.Enabled = False
        If CheckBox1.Checked = False Then
            dtsr = "OINV"
            DTSR1 = "RINV7"
        Else
            dtsr = "ODLN"
            DTSR1 = "RDLN7"
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click





        'Call MAIN()
        Dim com2 As New SqlCommand
        If con.State = ConnectionState.Closed Then con.Open()
        com2.Connection = con
        com2.CommandText = "update " & dtsr & "  set U_LrWgt = '" & Label9.Text & "',U_Lrwight = '" & Label9.Text & "',U_LR_Weight = '" & Label9.Text & "' where docentry in (" & lbldocentry.Text & ") and PIndicator in ('" & Trim(dbperiod) & "') "
        com2.ExecuteNonQuery()
        com2.Dispose()
        con.Close()


        Dim i As Integer = 0
        For i = 0 To dv.Rows.Count - 2

            'Call MAIN()
            Dim com1 As New SqlCommand
            If con.State = ConnectionState.Closed Then con.Open()
            com1.Connection = con
            com1.CommandText = "update " & DTSR1 & "  set Weight = '" & dv.Rows(i).Cells(3).Value & "' , [wgtupdtdate] = GETDATE()  where docentry in (" & lbldocentry.Text & ") and PackageNum in ('" & dv.Rows(i).Cells(2).Value & "') "
            com1.ExecuteNonQuery()
            com1.Dispose()
            con.Close()


        Next
        MsgBox("Update Sucessfully")

        txtno.Enabled = False
        dv.DataSource = Nothing
        Label9.Text = 0
        txtno.Text = ""
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        


        dv.DataSource = Nothing
        txtno.Text = ""
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub dv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dv.CellContentClick

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim serialport1 As New SerialPort
        Try
            With serialport1
                .PortName = System.Configuration.ConfigurationSettings.AppSettings("comPort")
                .BaudRate = 9600
                .Parity = Parity.None
                .DataBits = 8
                .StopBits = StopBits.One
                '.Handshake = IO.Ports.Handshake.None
                '.ReceivedBytesThreshold = 1
                .ReadTimeout = 1000
            End With
             
            serialport1.Open()
            text1 = ""
            msg = ""
            Dim message As String = serialport1.ReadLine
            msg = Val(message)
            text1 = message
            dv.CurrentCell.Value = text1
        Catch ex As Exception
            '' System.Web.HttpContext.Current.Response.Write(ex.Message)
            MsgBox(ex.Message)
            serialport1.Close()
        Finally
            serialport1.Close()
            serialport1.Dispose()
        End Try

    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = False Then
            dtsr = "OINV"
            DTSR1 = "RINV7"
        Else
            dtsr = "ODLN"
            DTSR1 = "RDLN7"
        End If

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim serialport1 As New SerialPort
        Try
            With serialport1
                .PortName = System.Configuration.ConfigurationSettings.AppSettings("comPort")
                .BaudRate = 9600
                .Parity = Parity.None
                .DataBits = 8
                .StopBits = StopBits.One
                .ReadTimeout = 1000
            End With

            serialport1.Open()
            text1 = ""
            msg = ""
            Dim message As String = serialport1.ReadLine
            msg = Val(message)
            text1 = message

        Catch ex As Exception
            MsgBox(ex.Message)
            serialport1.Close()
        Finally
            serialport1.Close()
            serialport1.Dispose()
        End Try

        ' Call MAIN()

        '   text1 = "50.000"
        Dim strArr As String()
        Dim DOCENTRY As Integer = 0
        Dim PAGAKENUM As String = ""
        strArr = ScanText.Text.Split("-")
        strArr(0).ToString()
        DOCENTRY = strArr(1).ToString
        PAGAKENUM = strArr(2).ToString

        If strArr(0).ToString() = "I" Or strArr(0).ToString() = "i" Then
            dtsr = "OINV"
            DTSR1 = "RINV7"
        Else
            dtsr = "ODLN"
            DTSR1 = "RDLN7"
        End If




        'Call MAIN()


        Dim wg As Double = RTrim(text1 * 10.0 / 10.0)
        wg = Double.Parse(wg.ToString("##,###.000"))

            'RTrim(text1)
            Dim com1 As New SqlCommand
        If con.State = ConnectionState.Closed Then con.Open()
        com1.Connection = con
        com1.CommandText = "update " & DTSR1 & "  set Weight = '" & wg & "' , [wgtupdtdate] = GETDATE() where docentry in (" & DOCENTRY & ") and PackageNum in ('" & PAGAKENUM & "') "
            com1.ExecuteNonQuery()
            com1.Dispose()
        con.Close()





        Dim com2 As New SqlCommand
        If con.State = ConnectionState.Closed Then con.Open()
        com2.Connection = con
        com2.CommandText = "update " & dtsr & "  set U_LrWgt = (SELECT SUM(Weight)  FROM  " & DTSR1 & " WHERE DOCENTRY  = " & DOCENTRY & "),U_Lrwight = (SELECT SUM(Weight)  FROM  " & DTSR1 & " WHERE DOCENTRY  = " & DOCENTRY & "),U_LR_Weight = (SELECT SUM(Weight)  FROM  " & DTSR1 & " WHERE DOCENTRY  = " & DOCENTRY & ") where docentry in (" & DOCENTRY & ") and PIndicator in ('" & Trim(dbperiod) & "') "
            com2.ExecuteNonQuery()
            com2.Dispose()
        con.Close()




        dv.DataSource = Nothing
            dv.ColumnCount = 4

            dv.Columns(0).HeaderText = "Docnum"
            dv.Columns(0).Name = "Docnum"
            dv.Columns(0).DataPropertyName = "Docnum"
            dv.Columns(0).Width = 200

            dv.Columns(0).ReadOnly = True

            dv.Columns(1).HeaderText = "Docentry"
            dv.Columns(1).Name = "Docentry"
            dv.Columns(1).DataPropertyName = "Docentry"
            dv.Columns(1).Width = 200

            dv.Columns(1).ReadOnly = True

            dv.Columns(2).Name = "PackageNum"
            dv.Columns(2).HeaderText = "PackageNum"
            dv.Columns(2).DataPropertyName = "PackageNum"
            dv.Columns(2).Width = 200
            dv.Columns(2).ReadOnly = True

            dv.Columns(3).Name = "wgt"
            dv.Columns(3).HeaderText = "WEIGHT SCALE"
            dv.Columns(3).DataPropertyName = "wgt"
            dv.Columns(3).Width = 250
            dv.AutoGenerateColumns = False
        'Call MAIN()
        Dim da As New SqlDataAdapter, ds As New DataSet
            da.SelectCommand = New SqlCommand
        da.SelectCommand.Connection = con
        da.SelectCommand.CommandType = CommandType.Text
        da.SelectCommand.CommandText = "select a.Docnum,a.DocEntry,isnull(b.PackageNum,1) PackageNum,isnull(b.[Weight],0) wgt from " & dtsr & "  a Left join " & DTSR1 & "  b on b.docentry = a.docentry  where isnull(PackageNum,1) <> 0 and  CASE when CONVERT(nvarchar(max),ISNULL(a.U_RefNo,''))  = '' then CONVERT(nvarchar(max),ISNULL(a.DocNum,''))  else CONVERT(nvarchar(max),ISNULL(a.U_RefNo,'')) end  = (SELECT  isnull(u_refno,docnum)  FROM  OINV WHERE DOCENTRY  = '" & DOCENTRY & "' ) and PIndicator = '" & Trim(dbperiod) & "' Order by a.DocEntry"
            da.Fill(ds, "tbl1")
            Dim dt As DataTable = ds.Tables("tbl1")
            dv.DataSource = dt
            dv.Sort(dv.Columns(2), ListSortDirection.Ascending)
            dv.CurrentCell = dv.Rows(0).Cells(3)
            dv.BeginEdit(True)
        con.Close()




        'Call MAIN()
        Name = "select  Docnum FROM " & dtsr & " where  DOCENTRY  = '" & DOCENTRY & "'"
        Dim CMDNAME As New SqlCommand(Name, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim DRPname As SqlDataReader
        DRPname = CMDNAME.ExecuteReader
        DRPname.Read()
        Dim dir, mdir As String
        Dim mtbox, j, txtbno As Integer
        Dim sno As Integer
        Dim lin As Integer

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "Weight.txt"

        FileOpen(1, mdir, OpenMode.Output)
        lin = 0



        PrintLine(1, TAB(0), "^XA")
        lin = lin + 1
        PrintLine(1, TAB(0), "^PRC")
        lin = lin + 1
        PrintLine(1, TAB(0), "^LH0,0^FS")
        lin = lin + 1
        PrintLine(1, TAB(0), "^LL304")
        lin = lin + 1
        PrintLine(1, TAB(0), "^MD0")
        lin = lin + 1
        PrintLine(1, TAB(0), "^MNY")
        lin = lin + 1
        PrintLine(1, TAB(0), "^LH0,0^FS")
        lin = lin + 1
        PrintLine(1, TAB(0), "^FO255,40^A0N,60,100^CI13^FR^FDWeight^FS")
        lin = lin + 1
        PrintLine(1, TAB(0), "^FO155,100^A0N,200,170^CI13^FR^FD" & wg.ToString("##,###.000") & "^FS")
        lin = lin + 1
        PrintLine(1, TAB(0), "^FO630,200^A0N,60,60^CI13^FR^FDkg^FS")
        lin = lin + 1
        PrintLine(1, TAB(0), "^FO300,300^A0N,40,40^CI13^FR^FDI-" & DRPname("Docnum") & "-" & PAGAKENUM & "^FS")
        lin = lin + 1
        PrintLine(1, TAB(0), "^PQ1,0,0,N")
        lin = lin + 1
        PrintLine(1, TAB(0), "^XZ")
        DRPname.Close()
        con.Close()
        con.Close()

        FileClose(1)


        If mos = "WIN" Then
            dir = System.AppDomain.CurrentDomain.BaseDirectory()
            mdir = Trim(dir) & "Weight.txt"
            Shell("cmd.exe /c" & "TYPE " & mdir & " > lpt1")
        Else
            Dim printer As String = tscprinter1
        Dim filePath As String = mlinpath
        Dim filePathname As String = mlinpath & "Weight.txt"
        Dim success As Boolean = PrintTscRaw(printer, filePathname)
        End If

        ScanText.Text = ""
        ScanText.Focus()

    End Sub

    Private Sub ScanText_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ScanText.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            'Button5_Click(sender, e)
            Call getaddwgt()
        End If

    End Sub

    Private Sub ScanText_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ScanText.KeyUp

    End Sub

    

    Private Sub ScanText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScanText.TextChanged

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'Transport Copy
        'live
        'Call MAIN()
        'Me.Cursor = Cursors.WaitCursor
        'Dim cryRpt1 As New ReportDocument()
        ''cryRpt1.Load(Trim(dbreportpath) & Trim(DBTRANS))

        'cryptfile = loadrptdb2(Trim(DBTRANS), Trim(dbreportpath))
        'cryRpt1.Load(cryptfile)

        'CrystalReportLogOn(cryRpt1, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))
        'cryRpt1.SetParameterValue("Dockey@", Val(lbldocentry.Text))
        'Me.CrystalReportViewer1.ReportSource = cryRpt1
        'Me.CrystalReportViewer1.PrintReport()
        'Me.CrystalReportViewer1.Refresh()
        'cryRpt1.Refresh()
        'cryRpt1.Dispose()
        'Me.Cursor = Cursors.Default
        Dim paramDict As New Dictionary(Of String, Object)
        paramDict.Add("Dockey@", Val(lbldocentry.Text))
        ' paramDict("Dockey@") = Val(Label7.Text)
        Dim req As New PrintRequest() With {
            .ReportName = DBTRANS,
            .PrinterName = "",     ' "" = preview
            .UseDB = True,
            .ServerName = mdbserver,
            .DatabaseName = mdbname,
            .DBUser = mdbuserid,
            .DBPassword = mdbpwd,
            .Parameters = paramDict
       }

        ' Dim req As New PrintRequest() With {
        '     .ReportName = reportName,
        '     .PrinterName = printerName,     ' "" = preview
        '     .UseDB = True,
        '     .ServerName = serverName,
        '     .DatabaseName = databaseName,
        '     .DBUser = dbUser,
        '     .DBPassword = dbPassword,
        '     .Parameters = paramDict
        '}
        Dim success As Boolean = CallCrystalPrintService(req)

    End Sub

    Private Sub btnInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvoice.Click
        'Forwarding Print
        'live
        'Me.Cursor = Cursors.WaitCursor
        'Dim cryRpt3 As New ReportDocument()
        ''  cryRpt3.Load(Trim(dbreportpath) & Trim(DBFRW))

        'cryptfile = loadrptdb2(Trim(DBFRW), Trim(dbreportpath))
        'cryRpt3.Load(cryptfile)


        'CrystalReportLogOn(cryRpt3, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))


        'cryRpt3.SetParameterValue("Dockey@", Val(lbldocentry.Text))
        ''Me.CrystalReportViewer1.Font = "Draft 10cpi, 10pt"

        'Me.CrystalReportViewer1.ReportSource = cryRpt3
        'Me.CrystalReportViewer1.PrintReport()
        'Me.CrystalReportViewer1.Refresh()
        'cryRpt3.Refresh()

        'Me.Cursor = Cursors.Default


        Dim paramDict As New Dictionary(Of String, Object)
        paramDict.Add("Dockey@", Val(lbldocentry.Text))
        ' paramDict("Dockey@") = Val(Label7.Text)
        Dim req As New PrintRequest() With {
            .ReportName = DBFRW,
            .PrinterName = "",     ' "" = preview
            .UseDB = True,
            .ServerName = mdbserver,
            .DatabaseName = mdbname,
            .DBUser = mdbuserid,
            .DBPassword = mdbpwd,
            .Parameters = paramDict
       }

        ' Dim req As New PrintRequest() With {
        '     .ReportName = reportName,
        '     .PrinterName = printerName,     ' "" = preview
        '     .UseDB = True,
        '     .ServerName = serverName,
        '     .DatabaseName = databaseName,
        '     .DBUser = dbUser,
        '     .DBPassword = dbPassword,
        '     .Parameters = paramDict
        '}
        Dim success As Boolean = CallCrystalPrintService(req)

    End Sub

    Private Sub getaddwgt()
        Dim serialport1 As New SerialPort
        Try
            With serialport1
                .PortName = System.Configuration.ConfigurationSettings.AppSettings("comPort")
                .BaudRate = 9600
                .Parity = Parity.None
                .DataBits = 8
                .StopBits = StopBits.One
                .ReadTimeout = 2000
            End With

            serialport1.Open()
            text1 = ""
            msg = ""
            Dim message As String = serialport1.ReadLine
            msg = Val(message)
            MsgBox(msg)
            MsgBox(message)
            text1 = IIf(Mid(message.Trim, 1, 1) = 0, Mid(message.Trim, 2, message.Trim.Length - 1), message.Trim)

        Catch ex As Exception
            MsgBox(ex.Message)
            text1 = "0.00"
            serialport1.Close()
        Finally
            serialport1.Close()
            serialport1.Dispose()
        End Try

        'Call MAIN()

        '   text1 = "50.000"
        Dim strArr As String()
        Dim DOCENTRY As Integer = 0
        Dim PAGAKENUM As String = ""
        Dim mdocnum As Integer = 0
        Dim dctype As String = ""
        strArr = ScanText.Text.Split("-")
        strArr(0).ToString()
        DOCENTRY = strArr(1).ToString
        PAGAKENUM = strArr(2).ToString

        If strArr(0).ToString() = "I" Or strArr(0).ToString() = "i" Then
            dtsr = "OINV"
            DTSR1 = "RINV7"
            dctype = "I"
        Else
            dtsr = "ODLN"
            DTSR1 = "RDLN7"
            dctype = "D"
        End If




        'Call MAIN()


        Dim wg As Double = RTrim(text1 * 10.0 / 10.0)
        wg = Double.Parse(wg.ToString("##,###.000"))

        'RTrim(text1)
        Dim com1 As New SqlCommand
        If con.State = ConnectionState.Closed Then con.Open()
        com1.Connection = con
        com1.CommandText = "update " & DTSR1 & "  set Weight = '" & wg & "' , [wgtupdtdate] = GETDATE() where docentry in (" & DOCENTRY & ") and PackageNum in ('" & PAGAKENUM & "') "
        com1.ExecuteNonQuery()
        com1.Dispose()
        con.Close()





        Dim com2 As New SqlCommand
        If con.State = ConnectionState.Closed Then con.Open()
        com2.Connection = con
        com2.CommandText = "update " & dtsr & "  set U_LrWgt = (SELECT SUM(Weight)  FROM  " & DTSR1 & " WHERE DOCENTRY  = " & DOCENTRY & "),U_Lrwight = (SELECT SUM(Weight)  FROM  " & DTSR1 & " WHERE DOCENTRY  = " & DOCENTRY & "),U_LR_Weight = (SELECT SUM(Weight)  FROM  " & DTSR1 & " WHERE DOCENTRY  = " & DOCENTRY & ") where docentry in (" & DOCENTRY & ") and PIndicator in ('" & Trim(dbperiod) & "') "
        com2.ExecuteNonQuery()
        com2.Dispose()
        con.Close()




        dv.DataSource = Nothing
        dv.ColumnCount = 4

        dv.Columns(0).HeaderText = "Docnum"
        dv.Columns(0).Name = "Docnum"
        dv.Columns(0).DataPropertyName = "Docnum"
        dv.Columns(0).Width = 200

        dv.Columns(0).ReadOnly = True

        dv.Columns(1).HeaderText = "Docentry"
        dv.Columns(1).Name = "Docentry"
        dv.Columns(1).DataPropertyName = "Docentry"
        dv.Columns(1).Width = 200

        dv.Columns(1).ReadOnly = True

        dv.Columns(2).Name = "PackageNum"
        dv.Columns(2).HeaderText = "PackageNum"
        dv.Columns(2).DataPropertyName = "PackageNum"
        dv.Columns(2).Width = 200
        dv.Columns(2).ReadOnly = True

        dv.Columns(3).Name = "wgt"
        dv.Columns(3).HeaderText = "WEIGHT SCALE"
        dv.Columns(3).DataPropertyName = "wgt"
        dv.Columns(3).Width = 250
        dv.AutoGenerateColumns = False
        'Call MAIN()
        Dim da As New SqlDataAdapter, ds As New DataSet
        da.SelectCommand = New SqlCommand
        da.SelectCommand.Connection = con
        da.SelectCommand.CommandType = CommandType.Text
        da.SelectCommand.CommandText = "select a.Docnum,a.DocEntry,isnull(b.PackageNum,1) PackageNum,isnull(b.[Weight],0) wgt from " & dtsr & "  a Left join " & DTSR1 & "  b on b.docentry = a.docentry  where isnull(PackageNum,1) <> 0 and  CASE when CONVERT(nvarchar(max),ISNULL(a.U_RefNo,''))  = '' then CONVERT(nvarchar(max),ISNULL(a.DocNum,''))  else CONVERT(nvarchar(max),ISNULL(a.U_RefNo,'')) end  = (SELECT  isnull(u_refno,docnum)  FROM  OINV WHERE DOCENTRY  = '" & DOCENTRY & "' ) and PIndicator = '" & Trim(dbperiod) & "' and b.packagenum=" & PAGAKENUM & " Order by a.DocEntry"
        da.Fill(ds, "tbl1")
        Dim dt As DataTable = ds.Tables("tbl1")
        dv.DataSource = dt
        dv.Sort(dv.Columns(2), ListSortDirection.Ascending)
        dv.CurrentCell = dv.Rows(0).Cells(3)
        dv.BeginEdit(True)
        con.Close()




        'Call MAIN()
        Name = "select  Docnum,pindicator FROM " & dtsr & " where  DOCENTRY  = '" & DOCENTRY & "'"
        Dim CMDNAME As New SqlCommand(Name, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim DRPname As SqlDataReader
        DRPname = CMDNAME.ExecuteReader
        DRPname.Read()
        Dim dir, mdir As String
        Dim mtbox, j, txtbno As Integer
        Dim sno As Integer
        Dim lin As Integer

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        'Dim mreppath = dir
        mdir = Trim(dir) & "Weight.txt"
        Dim cmbyear As String
        cmbyear = DRPname("Pindicator")
        If mproduction = "Y" Then
            mdocnum = DOCENTRY
        Else
            mdocnum = DOCENTRY
            'mdocnum = DRPname("Docnum")
        End If

        'FileOpen(1, mdir, OpenMode.Output)
        'lin = 0



        'PrintLine(1, TAB(0), "^XA")
        'lin = lin + 1
        'PrintLine(1, TAB(0), "^PRC")
        'lin = lin + 1
        'PrintLine(1, TAB(0), "^LH0,0^FS")
        'lin = lin + 1
        'PrintLine(1, TAB(0), "^LL304")
        'lin = lin + 1
        'PrintLine(1, TAB(0), "^MD0")
        'lin = lin + 1
        'PrintLine(1, TAB(0), "^MNY")
        'lin = lin + 1
        'PrintLine(1, TAB(0), "^LH0,0^FS")
        'lin = lin + 1
        'PrintLine(1, TAB(0), "^FO255,40^A0N,60,100^CI13^FR^FDWeight^FS")
        'lin = lin + 1
        'PrintLine(1, TAB(0), "^FO155,100^A0N,200,170^CI13^FR^FD" & wg.ToString("##,###.000") & "^FS")
        'lin = lin + 1
        'PrintLine(1, TAB(0), "^FO630,200^A0N,60,60^CI13^FR^FDkg^FS")
        'lin = lin + 1
        'PrintLine(1, TAB(0), "^FO300,300^A0N,40,40^CI13^FR^FDI-" & DRPname("Docnum") & "-" & PAGAKENUM & "^FS")
        'lin = lin + 1
        'PrintLine(1, TAB(0), "^PQ1,0,0,N")
        'lin = lin + 1
        'PrintLine(1, TAB(0), "^XZ")
        'DRPname.Close()
        'con1.Close()
        'con1.Close()

        'FileClose(1)



        'dir = System.AppDomain.CurrentDomain.BaseDirectory()
        'mdir = Trim(dir) & "Weight.txt"
        'Shell("cmd.exe /c" & "TYPE " & mdir & " > lpt1")

        '*************crystal
        Me.Cursor = Cursors.WaitCursor
        If mproduction = "Y" Then
            'live
            ''Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()

            ''cryRpt.Load(Trim(mreppath) & "Address Print ws bundle-acc.rpt")
            ''If mproduction = "Y" Then
            ''cryRpt.Load(Trim(mreppath) & "Address Print ws bundle-acc-Newprd.rpt")
            ''cryRpt.Load(Trim(mreppath) & "Address Print ws bundle-acc-New.rpt")
            '''Else
            ''    cryRpt.Load(Trim(mreppath) & "Address Print ws bundle-acc-New.rpt")
            ''End If

            ''CrystalReportLogOn(cryRpt, Trim(dbmyservername), dbmydbname, Trim(dbuserid), Trim(dbmypwd))
            ''cryRpt.SetParameterValue("Dockey@", Val(mdocnum))
            ''cryRpt.SetParameterValue("period@", cmbyear)
            ''cryRpt.SetParameterValue("period@", dctype)
            ''cryRpt.SetParameterValue("Wgt", text1)
            ''cryRpt.SetParameterValue("Packnum@", PAGAKENUM)
            ''PAGAKENUM


            ''CrystalReportViewer1.ReportSource = cryRpt

            ''Dim doctoprint As New System.Drawing.Printing.PrintDocument()
            ''doctoprint.PrinterSettings.PrinterName = prntername '(ex. "Epson SQ-1170 ESC/P 2")
            ''For i As Integer = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
            ''    Dim rawKind As Integer
            ''    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Address" Then
            ''        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
            ''        cryRpt.PrintOptions.PaperSize = rawKind
            ''        Exit For
            ''    End If
            ''Next

            ''cryRpt.PrintOptions.PrinterName = prntername
            ''cryRpt.PrintToPrinter(1, False, 0, 0)
            ''Me.view1.PrintReport()
            ''CrystalReportViewer1.Refresh()
            ''cryRpt.Dispose()
            ' Dim mreportname = "Address Print ws bundle-acc-New.rpt"
            ' Dim paramDict As New Dictionary(Of String, Object)
            ' paramDict.Add("Dockey@", Val(mdocnum))
            ' paramDict.Add("period@", cmbyear)
            ' paramDict.Add("Wgt", text1)
            ' paramDict.Add("Packnum@", PAGAKENUM)

            ' ' paramDict("Dockey@") = Val(Label7.Text)
            ' Dim req As New PrintRequest() With {
            '     .ReportName = mreportname,
            '     .PrinterName = "",     ' "" = preview
            '     .UseDB = True,
            '     .ServerName = mdbserver,
            '     .DatabaseName = mdbname,
            '     .DBUser = mdbuserid,
            '     .DBPassword = mdbpwd,
            '     .Parameters = paramDict
            '}
            ' Dim success As Boolean = CallCrystalPrintService(req)


            bundappbarcode(DOCENTRY, cmbyear, wg, PAGAKENUM, dctype)


        Else
            'cryRpt.Load(Trim(mreppath) & "Address Print ws bundle-acc-New.rpt")
        End If

        Me.Cursor = Cursors.Default


        '***************



        ScanText.Text = ""
        ScanText.Focus()

    End Sub



    'Private Sub ScanText_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ScanText.KeyPress
    '    If e.KeyChar = Convert.ToChar(13) Then
    '        Button5_Click(sender, e)
    '    End If

    'End Sub

    Private Sub citrixprint(ByVal filepath As String)
        'Dim printerPath As String = "\\TSClient\PrinterName" ' Adjust for Citrix printer redirection
        'Dim filePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nsbarcodE.txt")
        Dim printerPath As String = GetDefaultPrinterName()
        MsgBox("default Print" & printerPath)
        Try
            ' Read file contents
            'Dim commandstxt As String = File.ReadAllText(filepath)
            Dim commantstxt As String = File.ReadAllText(filepath, System.Text.Encoding.GetEncoding(1252))
            ' Send to printer
            Dim printDoc As New Printing.PrintDocument()
            printDoc.PrinterSettings.PrinterName = printerPath

            BarcodePrint.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, commantstxt)
            'BarcodePrint.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, commandstxt)


            'AddHandler printDoc.PrintPage, Sub(sender As Object, e As Printing.PrintPageEventArgs)
            '                                   e.Graphics.DrawString(commandstxt, New Font("Courier New", 10), Brushes.Black, New PointF(100, 100))
            '                               End Sub


            'AddHandler printDoc.PrintPage, Sub(sender As Object, e As Printing.PrintPageEventArgs)
            '                                   e.Graphics.DrawString(commandstxt, SystemFonts.DefaultFont, Brushes.Black, New PointF(100, 100))
            '                               End Sub

            printDoc.Print()
            'MessageBox.Show("Print job sent successfully.", "Print Status")
        Catch ex As Exception
            MessageBox.Show("Error: {ex.Message}", "Print Error")
        End Try
    End Sub

    Private Sub CrystalReportViewer1_Load(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        MsgBox("Default Printer : " & GetDefaultPrinterName())
    End Sub


    Private Sub bundappbarcode(ByVal docentry As Integer, period As String, wgt As Double, packageno As Integer, doctype As String)
        'Dim sqlstr, sqlstr2 As String
        'Dim strArr2 As String()
        'Dim DOCENTRY As Integer = 0
        'Dim PAGAKENUM As String = ""
        Dim mkdocnum As Integer = 0
        'Dim dctype As String = ""
        'strArr2 = ScanText.Text.Split("-")
        'strArr2(0).ToString()
        'DOCENTRY = strArr2(1).ToString
        'PAGAKENUM = strArr2(2).ToString


        If doctype = "I" Or doctype = "i" Then
            dtsr = "OINV"
            DTSR1 = "RINV7"
            dctype = "I"
            sqlstr = "select max(packagenum) maxpak from rinv7 where docentry=" & docentry
            sqlstr2 = "select docnum from oinv where docentry=" & docentry
        Else
            dtsr = "ODLN"
            DTSR1 = "RDLN7"
            dctype = "D"
            sqlstr = "select max(packagenum) maxpak from rdln7 where docentry=" & docentry
            sqlstr2 = "select docnum from odln where docentry=" & docentry
        End If


        Dim dtd As DataTable = getDataTable(sqlstr2)
        If dtd.Rows.Count > 0 Then
            For Each rdw As DataRow In dtd.Rows
                mkdocnum = rdw("docnum")
            Next
        End If
        'Call MAIN()


        'Dim wg As Double = RTrim(text1 * 10.0 / 10.0)
        'wg = Double.Parse(wg.ToString("##,###.000"))

        Dim dir As String
        Dim madd1, madd2, madd3, madd4, madd5, mcell, mcardfname, mtransport, mdes, mremark, minvno, mdist, mbar As String
        Dim mpackage, maxpack, mpackno As Integer
        Dim mwgt As String

        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "bundadd.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)

        'If Chkprndir.Checked = True Then
        '    FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        'Else
        FileOpen(1, mdir, OpenMode.Output)
        'End If



        'Dim dt2 As DataTable = getDataTable("select max(packagenum) maxpak from rinv7 where docentry=" & Val(lbldocentry.Text))
        Dim dt2 As DataTable = getDataTable(sqlstr)

        If dt2.Rows.Count > 0 Then
            For Each rw1 As DataRow In dt2.Rows
                maxpack = rw1("maxpak")
            Next
        End If

        'Dim dt3 As DataTable = getDataTable("select packagenum,weight from rinv7 where docentry=" & Val(lbldocentry.Text) & " and packagenum=" & packageno)
        'If dt3.Rows.Count > 0 Then
        '    For Each rw2 As DataRow In dt3.Rows
        '        mpackno = rw2("packagenum")
        '        mwgt = rw2("weight")

        '    Next
        'End If



        Dim dt As DataTable = getDataTable("exec [@PRINTLAYOUTMAIN] 'Address','" & mperiod & "'," & mkdocnum)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                If rw("packagenum") = packageno Then
                    mcardfname = Trim(rw("cardfname") & vbNullString)
                    madd1 = rw("building") & vbNullString
                    If Len(Trim(Replace(rw("block"), "-", ""))) > 0 Then
                        madd2 = Trim(rw("block") & vbNullString)
                    Else
                        madd2 = ""
                    End If
                    If Len(Trim(Replace(rw("street"), "-", ""))) > 0 Then
                        madd3 = Trim(rw("street") & vbNullString)
                    Else
                        madd3 = ""
                    End If

                    If Len(Trim(Replace(rw("City"), "-", ""))) > 0 Then
                        madd4 = rw("City") & " - " & rw("zipcode") & vbNullString
                    Else
                        madd4 = ""
                    End If

                    If Len(Trim(Replace(rw("state"), "-", ""))) > 0 Then
                        madd5 = Trim(rw("state") & vbNullString)
                    Else
                        madd5 = ""
                    End If
                    If Len(Trim(Replace(rw("Cellular"), "-", ""))) > 0 Then
                        mcell = Trim(rw("Cellular") & vbNullString)
                    Else
                        mcell = ""
                    End If
                    If IsDBNull(rw("packagenum")) = False Then
                        mpackage = rw("packagenum")
                    Else
                        mpackage = 0
                    End If
                    If IsDBNull(rw("u_transport")) = False Then
                        mtransport = rw("u_transport")
                    Else
                        mtransport = ""
                    End If
                    If IsDBNull(rw("u_dsnation")) = False Then
                        mdes = rw("u_dsnation")
                    Else
                        mdes = ""
                    End If
                    If IsDBNull(rw("u_remarks2")) = False Then
                        mremark = rw("u_remarks2")
                    Else
                        mremark = ""
                    End If
                    If IsDBNull(rw("invoicenos")) = False Then
                        minvno = rw("invoicenos")
                    Else
                        minvno = ""
                    End If

                    If IsDBNull(rw("district")) = False Then
                        mdist = rw("district")
                    Else
                        mdist = ""
                    End If

                    mpackno = rw("packagenum")
                    mwgt = Format(Convert.ToDecimal(rw("wgt")), "####.000").ToString.Trim
                    mbar = rw("bar")
                    If IsDBNull(rw("u_remarks2")) = False Then
                        mremark = rw("u_remarks2")
                    Else
                        mremark = ""
                    End If


                    If mos = "WIN" Then
                        'PrintLine(1, TAB(0), "<xpml><page quantity='0' pitch='210.1 mm'></xpml>SIZE 107.10 mm, 209.6 mm")
                        PrintLine(1, TAB(0), "SIZE 107.10 mm, 209.6 mm")
                    Else
                        PrintLine(1, TAB(0), "SIZE 107.10 mm, 209.6 mm")
                    End If

                    PrintLine(1, TAB(0), "DIRECTION 0,0")
                    PrintLine(1, TAB(0), "REFERENCE 0,0")
                    PrintLine(1, TAB(0), "OFFSET 0 mm")
                    PrintLine(1, TAB(0), "SPEED 14")
                    PrintLine(1, TAB(0), "SET PEEL OFF")
                    PrintLine(1, TAB(0), "SET CUTTER OFF")
                    PrintLine(1, TAB(0), "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        'PrintLine(1, TAB(0), "<xpml></page></xpml><xpml><page quantity='1' pitch='209.6 mm'></xpml>SET TEAR ON")
                        PrintLine(1, TAB(0), "SET TEAR ON")
                    Else
                        PrintLine(1, TAB(0), "SET TEAR ON")
                    End If

                    PrintLine(1, TAB(0), "CLS")
                    PrintLine(1, TAB(0), "CODEPAGE 1252")


                    PrintLine(1, TAB(0), "TEXT 828,107," & """ROMAN.TTF""" & ",90,1,24," & """To,""")
                    PrintLine(1, TAB(0), "TEXT 809,829," & """ROMAN.TTF""" & ",90,1,18," & """INV : " & Trim(minvno) & " / Bundle No : " & mpackage & " / " & maxpack & """")
                    PrintLine(1, TAB(0), "BAR 311,1570,445,3")
                    PrintLine(1, TAB(0), "BAR 38,1570,274,3")
                    PrintLine(1, TAB(0), "BAR 38,102,3,1469")
                    PrintLine(1, TAB(0), "BAR 39,102,274,3")
                    PrintLine(1, TAB(0), "BAR 312,102,445,3")
                    PrintLine(1, TAB(0), "BAR 755,103,3,1469")
                    PrintLine(1, TAB(0), "BAR 311,102,3,1471")
                    PrintLine(1, TAB(0), "TEXT 734,113," & """ROMAN.TTF""" & ",90,1,24," & """M/s." & Trim(mcardfname) & """")
                    PrintLine(1, TAB(0), "TEXT 658,113," & """ROMAN.TTF""" & ",90,1,18," & """" & Trim(madd1) & ",""")
                    PrintLine(1, TAB(0), "TEXT 599,113," & """ROMAN.TTF""" & ",90,1,20," & """" & Trim(madd2) & ",""")
                    PrintLine(1, TAB(0), "TEXT 531,113," & """ROMAN.TTF""" & ",90,1,24," & """" & Trim(madd3) & """")
                    PrintLine(1, TAB(0), "TEXT 451,113," & """ROMAN.TTF""" & ",90,1,24," & """" & Trim(madd4) & """")
                    PrintLine(1, TAB(0), "TEXT 373,113," & """ROMAN.TTF""" & ",90,1,20," & """" & Trim(mdist) & " " & Trim(madd5) & ". " & IIf(Len(Trim(mcell)) > 0, "Mobile No: " & mcell, "") & """")

                    PrintLine(1, TAB(0), "TEXT 306,113," & """ROMAN.TTF""" & ",90,1,15," & """From :""")

                    PrintLine(1, TAB(0), "TEXT 258,113," & """ROMAN.TTF""" & ",90,1,15," & """ATITHYA CLOTHING COMPANY """)
                    PrintLine(1, TAB(0), "TEXT 212,113," & """ROMAN.TTF""" & ",90,1,11," & """(A Unit of ENES Textile Mills),""")
                    PrintLine(1, TAB(0), "TEXT 168,113," & """ROMAN.TTF""" & ",90,1,13," & """No.2/453,SVD Nagar, Kovilpappakudi, """)
                    PrintLine(1, TAB(0), "TEXT 127,113," & """ROMAN.TTF""" & ",90,1,13," & """Madurai-625018, TN.""")
                    PrintLine(1, TAB(0), "TEXT 289,797," & """ROMAN.TTF""" & ",90,1,15," & """Transport : " & Trim(mtransport) & """")
                    PrintLine(1, TAB(0), "BAR 40,787, 272, 3")
                    'PrintLine(1, TAB(0), "TEXT 646,664," & """0""" & ",270,16,20," & """" & Trim(mtransport) & """")
                    PrintLine(1, TAB(0), "TEXT 239,797," & """ROMAN.TTF""" & ",90,1,18," & """Destination : " & Trim(mdes) & """")
                    'PrintLine(1, TAB(0), "TEXT 712,642," & """0""" & ",270,16,20," & """" & Trim(mdes) & """")
                    PrintLine(1, TAB(0), "TEXT 123,1356," & """ROMAN.TTF""" & ",90,1,20," & """Wt." & Trim(mwgt) & """")
                    'PrintLine(1, TAB(0), "BARCODE 164,797," & """39""" & ",102,0,90,3,8," & """" & Trim(mbar) & """")

                    'PrintLine(1, TAB(0), "TEXT 774, 860, " & """0""" & ", 270, 14, 17, " & """" & Trim(mremark) & """")
                    'PrintLine(1, TAB(0), "TEXT 775, 700, " & """0""" & ", 270, 17, 20, " & """ONLINE TIRUPUR""")
                    'PrintLine(1, TAB(0), "TEXT 35, 804, " & """0""" & ", 270, 20, 20, " & """INV :  " & Trim(minvno) & " / Bundle No : " & mpackage & " / " & maxpack & """")
                    'PrintLine(1, TAB(0), "TEXT 225,1618," & """0""" & ",270,21,24," & """" & Trim(madd1) & "," & Trim(madd2) & ",""")
                    'PrintLine(1, TAB(0), "TEXT 300,1618," & """0""" & ",270,21,24," & """" & Trim(madd3) & """")
                    'PrintLine(1, TAB(0), "TEXT 368,1618," & """0""" & ",270,21,24," & """" & Trim(madd4) & """")
                    'PrintLine(1, TAB(0), "TEXT 445,1618," & """0""" & ",270,24,27," & """" & Trim(mdist) & " " & Trim(madd5) & ". " & IIf(Len(Trim(mcell)) > 0, "Mobile No: " & mcell, "") & """")
                    'PrintLine(1, TAB(0), "Text(534, 1618, '0', 270, 21, 24," & "TIRUPUR")
                    PrintLine(1, TAB(0), "PRINT 1,1")
                    If mos = "WIN" Then
                        'PrintLine(1, TAB(0), "<xpml></page></xpml><xpml><end/></xpml>")
                    End If

                End If
            Next

        End If

        'PrintLine(1, TAB(0), mkstr)
        'mkstr = ""

        FileClose(1)
        If mos = "WIN" Then

            If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                ' Call updateprn()
                If MsgBox("Lpt Port", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text) & ":")
                    Shell("rawpr.bat " & mdir)

                Else
                    Dim text As String = File.ReadAllText(mdir)
                    Dim pd As PrintDialog = New PrintDialog()
                    pd.PrinterSettings = New PrinterSettings()
                    BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName, text)
                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                    'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                End If
            End If

        Else
            Dim printer As String = tscprinter1
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "bundadd.txt"
            Dim success As Boolean = PrintTscRaw(printer, filePathname)
        End If


    End Sub

End Class
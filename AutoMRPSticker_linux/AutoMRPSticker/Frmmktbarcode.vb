Imports System.IO
Imports System.Drawing.Printing
Imports System.Configuration
Imports Microsoft.VisualBasic
Imports BarTender
Imports WashcareLbl.connection
Imports System.IO.Ports
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Imports System.Globalization
Imports System.Threading
Imports System.Net.Sockets
Imports System.Threading.Tasks
Imports System.Data.SqlClient




Public Class Frmmktbarcode
    'Dim WithEvents serial As New SerialPort("COM4", 9600)

    Private cameraForm As FrmCamera
    'Private scanner As WebCamQRScanner
    'Private scanner As QRScannerBox
    'Private scanner As QRScanner800
    Private scanner As QRScanner800N

    Private printDocument As New PrintDocument()
    Dim mfile, fwash, fsilk, fpant, mtype, mcitrix, mlpt As String
    Dim x, y, n, mqty, lstcol As Integer
    Private rowFilterText As String = String.Empty
    Private rowIndex As Integer = 0
    Dim mfil, mfildet, msql, prnon, msql2, mperiod, mdir, mbarstr As String

    Dim objsetting As New Printing.PrinterSettings
    Dim strPrinter As String
    Dim ci As CultureInfo = New CultureInfo("en-IN")

    Dim strcol As String()
    Dim mkcolor As String
    Dim strArrc As String()
    'Dim DOCENTRYc As Integer = 0
    Dim mbarcode As String = ""

    Private printerIp As String
    Private printerPort As Integer
    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal PSZpRINTER As String) As Boolean

    Dim WithEvents ArduinoPort As New SerialPort("COM3", 9600)
    'Private WithEvents txtScanner As New TextBox()
    Dim qrcodearr As String()
    Dim qrcod As String
    Dim batch As String
    'Dim lastScanText As String
    'Dim scannedText As String
    'Private WithEvents ScanTimer As New Timer()
    Private WithEvents ScanTimer As New System.Windows.Forms.Timer()
    Private lastScanText As String = ""
    Dim typeText As String
    Private isCameraActive As Boolean = False
    Dim manual As Integer
    Dim dtpk As New DataTable
    Dim drag As Boolean
    Dim mouseX As Integer
    Dim mouseY As Integer
    Dim minvtypeh As String
    Dim minvtyped As String
    Dim minvtable As String
    'Dim n As Integer

    Private Sub Frmmktbarcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        'Dim x As Integer = (Me.ClientSize.Width - Groupbox1.Width) / 2
        'Dim y As Integer = (Me.ClientSize.Height - Groupbox1.Height) / 2
        'Groupbox1.Location = New Point(x, y)

        Groupbox1.Width = Me.ClientSize.Width
        Groupbox1.Height = Me.ClientSize.Height

        dg.Width = Groupbox1.Width - 10
        'MsgBox(txtscanner.Location)

        Btnclear.PerformClick()
        'Call loadprinter()
        'mdbserver = System.Configuration.ConfigurationSettings.AppSettings("myservername")
        'mdbname = ConfigurationSettings.AppSettings("mydbname")
        'mdbuserid = ConfigurationSettings.AppSettings("userid")
        'mdbpwd = decodefilesql(ConfigurationSettings.AppSettings("mypwd"))

        'fwash = System.Configuration.ConfigurationSettings.AppSettings("Washcarefile")
        'fsilk = ConfigurationSettings.AppSettings("Silkfile")
        'fpant = ConfigurationSettings.AppSettings("Pantfile")
        'mprinter = ConfigurationSettings.AppSettings("Printername")
        'mperiod = ConfigurationSettings.AppSettings("period")
        'printerIp = ConfigurationSettings.AppSettings("printerip")
        'printerPort = ConfigurationSettings.AppSettings("prnport")
        'mcitrix = ConfigurationSettings.AppSettings("Citrix")
        'mlpt = ConfigurationSettings.AppSettings("PrintLpt")
        'mos = ConfigurationSettings.AppSettings("OS")
        'mlinpath = ConfigurationSettings.AppSettings("Linuxfilepath")


        'Call loadprinter()

        'cmbprinter.Text = mprinter

        OptSales.Checked = True

        'dtp.MinDate = New DateTime(1753, 1, 1)
        'dtp.MaxDate = DateTime.Today
        'dtp.Format = DateTimePickerFormat.Custom
        'dtp.CustomFormat = "yyyy"
        'dtp.Value = Today
        If mcitrix = "Y" Then
            If Label3.Visible = False Then Label3.Visible = True
            If cmbvprinter.Visible = False Then cmbvprinter.Visible = True
            'Call LoadCitrixSessionPrinters()
            Call LoadAllPrinters()
        Else
            If Label3.Visible = True Then Label3.Visible = False
            If cmbvprinter.Visible = True Then cmbvprinter.Visible = False
            Call loadprinter()
            cmbprinter.Text = mprinter
        End If


        Call loadcmbyr()
        cmbtype.Items.Clear()
        cmbtype.Items.Add("Dealer")
        cmbtype.Items.Add("Showroom")
        cmbtype.Items.Add("Franchise")
        cmbtype.Items.Add("Distributor")
        cmbtype.Items.Add("TN")
        cmbtype.Items.Add("OS")
        cmbtype.Items.Add("Pothys")

        cmbyr.Text = mperiod
        Call loadfit()
        cmbfit.Text = ""
        ScanTimer.Interval = 300

        Call unoopen()
        txtscanner.AcceptsTab = True
        'Dgpk.Visible = False
    End Sub

    Private Sub dtp_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtp.ValueChanged
        txtyr.Text = Year(dtp.Value)
    End Sub


    Private Sub loadprinter()

        If mcitrix = "Y" Then
            Dim printers As List(Of String) = GetPrinterNamesWithSessions()

            ' Display the printers
            If printers.Count > 0 Then
                Console.WriteLine("Printers with Sessions:")
                For Each printerName As String In printers
                    cmbprinter.Items.Add(printerName)
                    'cmbprinter.Text = printerName
                    'Console.WriteLine(printerName)
                Next
                cmbprinter.SelectedIndex = 0 ' default to the first
            Else
                Console.WriteLine("No printers with sessions found.")
            End If

        Else

            Dim pkInstalledPrinters As String

            ' Find all printers installed
            For Each pkInstalledPrinters In PrinterSettings.InstalledPrinters
                cmbprinter.Items.Add(pkInstalledPrinters)
            Next pkInstalledPrinters
            cmbprinter.SelectedIndex = -1
        End If

        ' Set the combo to the first printer in the list
        cmbprinter.SelectedIndex = -1

    End Sub

    Private Sub LoadCitrixSessionPrinters()
        Try
            cmbprinter.Items.Clear()

            For Each printerName As String In PrinterSettings.InstalledPrinters
                ' You may include all or filter for session/redirected printers
                ' Common indicators: "from", "in session", "Citrix"
                If printerName.ToLower().Contains("from") OrElse
                   printerName.ToLower().Contains("session") OrElse
                   printerName.ToLower().Contains("citrix") Then
                    cmbprinter.Items.Add(printerName)
                End If
            Next

            If cmbprinter.Items.Count > 0 Then
                cmbprinter.SelectedIndex = 0
            Else
                cmbprinter.Items.Add("No Citrix printers found")
                cmbprinter.SelectedIndex = 0
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading Citrix printers: " & ex.Message)
        End Try
    End Sub


    Private Sub crtmptabst()
        If Chksr.Checked = True Then
            mfil = "(select t0.docentry,t2.u_itemcode as itemcode,t0.itemcode remarks,t2.U_Size, t1.u_catalgcode,t1.u_itemname as u_catalogname,t0.linenum,t0.freetxt,t0.quantity,CASE when t3.InvntItem='N' then 'S' else '' end treetype,'' as text from srbardet t0 " & vbCrLf _
                & "left join [@INS_PLM1] t1 on  convert(nvarchar(40),t1.U_Remarks)= t0.itemcode " & vbCrLf _
                & " left join [@INS_OPLM] t2 on t2.DocEntry=t1.DocEntry " & vbCrLf _
                & "left join OITM t3 on t3.ItemCode=t2.U_ItemCode " & vbCrLf _
                & " where t1.U_Lock<>'Y')"
            mfildet = "(select docentry,docnum,docdate,CardCode,cardname from srbardet group by docentry,docnum,docdate,CardCode,cardname) "

            'mfil = "V_sampinv1"
            'mfildet = "v_sampOinv"
        Else


            If OptSales.Checked = True Then 'Sales
                mfil = "INV1"
                mfildet = "OINV"
                prnon = "SALES"
                'ElseIf Trim(cmbprnon.Text) = "SALESPCS" Then
                '   mfil = "INV1"
                '  mfildet = "OINV"
            ElseIf optdateord.Checked = True Then  ' "DATE ORDER" 
                mfil = "DLN1"
                mfildet = "ODLN"
                prnon = "DATE ORDER"
            ElseIf optsdraft.Checked = True Then ' "INV DRAFT" 
                mfil = "DRF1"
                mfildet = "ODRF"
                prnon = "INV DRAFT"
            ElseIf optsample.Checked = True Then  ' "SAMPLE" 
                mfil = "V_sampinv1"
                mfildet = "v_sampOinv"
                prnon = "SAMPLE"
            End If
        End If


        If Chksr.Checked = True Then
            msql = "Exec insertbartempSRXL " & Val(txtdocnum.Text) & "," & Val(txtmont.Text) & "," & Val(txtyr.Text) & ",'" & Trim(prnon) & "',1"
        Else
            msql = "Exec insertbartemp " & Val(txtdocnum.Text) & "," & Val(txtmont.Text) & "," & Val(txtyr.Text) & ",'" & Trim(prnon) & "',0"
        End If


        'msql = "Exec insertbartemp " & Val(txtbno.Text) & "," & Val(cmbmont.Text) & "," & Val(cmbyr.Text) & ",'" & Trim(cmbprnon.Text) & "'"

        'Dim dCMD As New OleDb.OleDbCommand(msql, con)

        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        ''dCMD.CommandText = "Exec inserbartemp 'Test', 'Test', 'Test'"
        'dCMD.CommandText = msql
        'dCMD.Connection = con 'Active Connection 
        'dCMD.CommandTimeout = 300
        'Cursor = Cursors.WaitCursor
        Try
            executeQuery(msql)
            'dCMD.ExecuteNonQuery()

            'dCMD.Dispose()
        Catch ex As Exception
            'dCMD.Dispose()
            MsgBox(ex.Message)

        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub txtdocnum_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdocnum.LostFocus
        'msql2 = "select month(docdate) monn,docentry from oinv where docnum=" & Val(txtdocnum.Text) & " and indicator='" & Trim(cmbyr.Text) & "'"

        If OptSales.Checked = True Then 'Sales
            If Chksr.Checked = True Then
                msql2 = "select month(docdate) monn,docentry,year(docdate) yr,cardcode,cardname from srbarhead where docnum=" & Val(txtdocnum.Text)
            Else
                msql2 = "select month(docdate) monn,docentry,year(docdate) yr,cardcode,cardname from oinv where docnum=" & Val(txtdocnum.Text) & " and pindicator='" & Trim(cmbyr.Text) & "'"
            End If


        ElseIf optdateord.Checked = True Then  ' "DATE ORDER" 
            msql2 = "select month(docdate) monn,docentry,year(docdate) yr,cardcode,cardname from odln where docnum=" & Val(txtdocnum.Text) & " and pindicator='" & Trim(cmbyr.Text) & "'"

        ElseIf optsdraft.Checked = True Then ' "INV DRAFT" 
            msql2 = "select month(docdate) monn,docentry,year(docdate) yr,cardcode,cardname from odrf where docnum=" & Val(txtdocnum.Text) & " and pindicator='" & Trim(cmbyr.Text) & "'"

        ElseIf optsample.Checked = True Then  ' "SAMPLE" 
            msql2 = "select month(docdate) monn,docentry,year(docdate) yr,cardcode,cardname from v_sampoinv where docnum=" & Val(txtdocnum.Text) & " and pindicator='" & Trim(cmbyr.Text) & "'"
            'mfil = "V_sampinv1"
            'mfildet = "v_sampOinv"
            'prnon = "SAMPLE"
        ElseIf OptSamDrf.Checked = True Then
            msql2 = "select month(docdate) monn,docentry,year(docdate) yr,cardcode,cardname from odrf where docnum=" & Val(txtdocnum.Text) & " and pindicator='" & Trim(cmbyr.Text) & "'"
        End If



        ' msql2 = "select month(docdate) monn,docentry,year(docdate) yr from oinv where docnum=" & Val(txtdocnum.Text) & " and year(docdate)=" & Val(txtyr.Text)

        'msql2 = "select month(docdate) monn from oinv where docnum=" & Val(txtdocnum.Text) & " and   year(docdate)= case when month(docdate)>=1 and month(docdate)<=3 then " & Val(txtyr.Text) + 1 & " else " & Val(txtyr.Text) & " end"
        Dim dt As DataTable = getDataTable(msql2)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                txtmont.Text = rw("monn")
                lbldocentry.Text = rw("docentry")
                txtyr.Text = rw("yr")
                lblcardcode.Text = rw("cardcode")
                lblcardname.Text = rw("cardname")
            Next
        Else
            txtmont.Text = 0
        End If
    End Sub

    Private Sub loadfit()
        msql = "select k.fit from ( " _
              & " select case when len(rtrim(ltrim(isnull(u_fitname,''))))>0 and charindex('/',u_fitname)>0   then substring(u_fitname,1,charindex('/',u_fitname)-1) else '' end fit, " _
              & " case when len(rtrim(ltrim(isnull(u_fitname,''))))>0 and charindex('/',substring(u_fitname,charindex('/',u_fitname)+1,30))>0 then substring(substring(u_fitname,charindex('/',u_fitname)+1,30),1,charindex('/',substring(u_fitname,charindex('/',u_fitname)+1,30))-1) else '' end cut " _
              & " from oitm  where len(rtrim(ltrim(isnull(u_fitname,''))))>0 " _
              & "  group by case when len(rtrim(ltrim(isnull(u_fitname,''))))>0 and charindex('/',u_fitname)>0   then substring(u_fitname,1,charindex('/',u_fitname)-1) else '' end ," _
              & " case when len(rtrim(ltrim(isnull(u_fitname,''))))>0 and charindex('/',substring(u_fitname,charindex('/',u_fitname)+1,30))>0 then substring(substring(u_fitname,charindex('/',u_fitname)+1,30),1,charindex('/',substring(u_fitname,charindex('/',u_fitname)+1,30))-1) else '' end ) k " _
              & "  where isnull(k.fit,'')<>'' group by k.fit  order by k.fit"

        Dim dt As DataTable = getDataTable(msql)
        If dt.Rows.Count > 0 Then
            cmbfit.Items.Clear()
            For Each rw As DataRow In dt.Rows
                cmbfit.Items.Add(rw("fit"))
            Next
        End If



    End Sub

    Private Sub txtdocnum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdocnum.TextChanged

    End Sub

    Private Sub Groupbox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Groupbox1.Enter

    End Sub
    Private Sub loaddata()
        Call crtmptabst()
        dg.Rows.Clear()
        If Len(Trim(cmbtype.Text)) > 0 Then
            If chkmfgdate.Checked = True Then
                msql = "declare @monno as integer " _
                       & " declare @yr as integer " _
                       & " declare @mfd as nvarchar(15) " _
                       & " set @monno=" & Val(txtmont.Text) _
                       & " set @yr=" & Val(txtyr.Text) _
                       & " set @mfd='MFG:'+(SELECT  left(DATENAME(MONTH, CAST(convert(varchar(4),@yr) +'-' + RIGHT('00' + CAST(@monno AS VARCHAR(2)), 2) + '-01' AS DATETIME)),3) + ' '+ convert(varchar(4),@yr)  AS MonthName) "
            Else
                msql = "declare @monno as integer " _
                      & " declare @yr as integer " _
                      & " declare @mfd as nvarchar(15) " _
                      & " set @monno=" & Val(txtmont.Text) _
                      & " set @yr=" & Val(txtyr.Text) _
                      & " set @mfd=''"
            End If



            If Trim(cmbtype.Text) = "Dealer" Or Trim(cmbtype.Text) = "TN" Then
                'msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,barcode2 mbarcode,barcode2 txbarcode, boxmrp,mrp,boxqty,quantity,size2 from bartemp where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"
                msql = msql & " select b.docnum,b.u_subgrp6,b.u_style,b.u_size,b.color,b.u_itemgrp,case when len(rtrim(@mfd))>0 then @mfd else b.mfd end mfd,b.cstype,b.barcode2 mbarcode,b.barcode2 txbarcode, b.boxmrp,b.mrp,b.boxqty,b.quantity,b.size2, " _
                     & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',t.u_fitname)>0   then substring(t.u_fitname,1,charindex('/',t.u_fitname)-1) else '' end fit," _
                     & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))>0 then substring(substring(t.u_fitname,charindex('/',t.u_fitname)+1,30),1,charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))-1) else '' end cut,b.itemcode,isnull(b.batchnum,'') batchnum,0 noprint, " _
                     & " t.itmsgrpcod from bartemp b with (nolock) " _
                     & " inner join oitm t on t.itemcode=b.itemcode " _
                     & " where b.docentry=" & Val(lbldocentry.Text) & " and month(b.docdate)=" & Val(txtmont.Text) & " and year(b.docdate)=" & Val(txtyr.Text) & " order by b.linenum "

            ElseIf Trim(cmbtype.Text) = "Showroom" Then
                ' msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,autocode mbarcode,autocode txbarcode, boxmrp,mrp,boxqty,quantity,size2 from bartemp with (nolock) where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"

                msql = msql & " select b.docnum,b.u_subgrp6,b.u_style,b.u_size,b.color,b.u_itemgrp,case when len(rtrim(@mfd))>0 then @mfd else b.mfd end mfd,b.cstype,b.autocode mbarcode,b.autocode txbarcode, b.boxmrp,b.mrp,b.boxqty,b.quantity,b.size2, " _
                    & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',t.u_fitname)>0   then substring(t.u_fitname,1,charindex('/',t.u_fitname)-1) else '' end fit, " _
                    & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))>0 then substring(substring(t.u_fitname,charindex('/',t.u_fitname)+1,30),1,charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))-1) else '' end cut,b.itemcode,isnull(b.batchnum,'') batchnum,0 noprint, " _
                    & " t.itmsgrpcod from bartemp b with (nolock) " _
                    & " inner join oitm t on t.itemcode=b.itemcode " _
                    & " where b.docentry=" & Val(lbldocentry.Text) & " and month(b.docdate)=" & Val(txtmont.Text) & " and year(b.docdate)=" & Val(txtyr.Text) & " order by b.linenum "

            ElseIf Trim(cmbtype.Text) = "Franchise" Then
                'msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,autocode mbarcode,autocode txbarcode, boxmrp,mrp,boxqty,quantity,size2 from bartemp with (nolock) where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"
                msql = msql & " select b.docnum,b.u_subgrp6,b.u_style,b.u_size,b.color,b.u_itemgrp,case when len(rtrim(@mfd))>0 then @mfd else b.mfd end mfd,b.cstype,b.autocode mbarcode,b.autocode txbarcode, b.boxmrp,b.mrp,b.boxqty,b.quantity,b.size2," _
                       & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',t.u_fitname)>0   then substring(t.u_fitname,1,charindex('/',t.u_fitname)-1) else '' end fit, " _
                       & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))>0 then substring(substring(t.u_fitname,charindex('/',t.u_fitname)+1,30),1,charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))-1) else '' end cut,b.itemcode,isnull(b.batchnum,'') batchnum,0 noprint, " _
                       & " t.itmsgrpcod from bartemp b with (nolock)  " _
                       & " inner join oitm t on t.itemcode=b.itemcode " _
                       & " where b.docentry=" & Val(lbldocentry.Text) & " and month(b.docdate)=" & Val(txtmont.Text) & " and year(b.docdate)=" & Val(txtyr.Text) & " order by b.linenum "
            ElseIf Trim(cmbtype.Text) = "OS" Then 'u_remarks
                'msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,u_remarks mbarcode,u_remarks txbarcode, boxmrp,mrp,boxqty,quantity,size2 from bartemp with (nolock) where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"

                msql = msql & " select b.docnum,b.u_subgrp6,b.u_style,b.u_size,b.color,b.u_itemgrp,case when len(rtrim(@mfd))>0 then @mfd else b.mfd end mfd,b.cstype,b.u_remarks mbarcode,b.u_remarks txbarcode, b.boxmrp,b.mrp,b.boxqty,b.quantity,b.size2, " _
                      & "  case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',t.u_fitname)>0   then substring(t.u_fitname,1,charindex('/',t.u_fitname)-1) else '' end fit, " _
                      & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))>0 then substring(substring(t.u_fitname,charindex('/',t.u_fitname)+1,30),1,charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))-1) else '' end cut,b.itemcode,isnull(b.batchnum,'') batchnum,0 noprint, " _
                      & " t.itmsgrpcod from bartemp b with (nolock) " _
                      & " inner join oitm t on t.itemcode=b.itemcode  " _
                      & " where b.docentry=" & Val(lbldocentry.Text) & " and month(b.docdate)=" & Val(txtmont.Text) & " and year(b.docdate)=" & Val(txtyr.Text) & " order by b.linenum "

            ElseIf Trim(cmbtype.Text) = "Distributor" Then
                'msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,odoocode mbarcode,odoocode txbarcode, boxmrp,mrp,boxqty,quantity,size2 from bartemp with (nolock) where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"
                msql = msql & "select b.docnum,b.u_subgrp6,b.u_style,b.u_size,b.color,b.u_itemgrp,case when len(rtrim(@mfd))>0 then @mfd else b.mfd end mfd,b.cstype,b.odoocode mbarcode,b.odoocode txbarcode, b.boxmrp,b.mrp,b.boxqty,b.quantity,b.size2," _
                       & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',t.u_fitname)>0   then substring(t.u_fitname,1,charindex('/',t.u_fitname)-1) else '' end fit, " _
                       & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))>0 then substring(substring(t.u_fitname,charindex('/',t.u_fitname)+1,30),1,charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))-1) else '' end cut,b.itemcode,isnull(b.batchnum,'') batchnum,0 noprint, " _
                       & " t.itmsgrpcod from bartemp b with (nolock) " _
                       & " inner join oitm t on t.itemcode=b.itemcode " _
                       & " where b.docentry=" & Val(lbldocentry.Text) & " and month(b.docdate)=" & Val(txtmont.Text) & " and year(b.docdate)=" & Val(txtyr.Text) & " order by b.linenum "
            ElseIf Trim(cmbtype.Text) = "Pothys" Then
                'msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,autocode mbarcode,autocode txbarcode, boxmrp,mrp,boxqty,quantity,size2 from bartemp with (nolock) where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"
                msql = msql & " select b.docnum,b.u_subgrp6,b.u_style,b.u_size,b.color,b.u_itemgrp,case when len(rtrim(@mfd))>0 then @mfd else b.mfd end mfd,b.cstype,b.autocode mbarcode,b.autocode txbarcode, b.boxmrp,b.mrp,b.boxqty,b.quantity,b.size2," _
                       & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',t.u_fitname)>0   then substring(t.u_fitname,1,charindex('/',t.u_fitname)-1) else '' end fit, " _
                       & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))>0 then substring(substring(t.u_fitname,charindex('/',t.u_fitname)+1,30),1,charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))-1) else '' end cut,b.itemcode,isnull(b.batchnum,'') batchnum,0 noprint,  " _
                       & " t.itmsgrpcod from bartemp b with (nolock) " _
                       & " inner join oitm t on t.itemcode=b.itemcode " _
                       & " where b.docentry=" & Val(lbldocentry.Text) & " and month(b.docdate)=" & Val(txtmont.Text) & " and year(b.docdate)=" & Val(txtyr.Text) & " order by b.linenum "
            End If

            Dim dtb As DataTable = getDataTable(msql)
            If dtb.Rows.Count > 0 Then
                For Each rw As DataRow In dtb.Rows
                    n = dg.Rows.Add
                    dg.Rows(n).Cells(1).Value = rw("docnum")
                    dg.Rows(n).Cells(1).ReadOnly = True
                    dg.Rows(n).Cells(2).Value = rw("u_subgrp6")
                    dg.Rows(n).Cells(2).ReadOnly = True
                    dg.Rows(n).Cells(3).Value = rw("u_style")
                    dg.Rows(n).Cells(3).ReadOnly = True
                    dg.Rows(n).Cells(4).Value = rw("u_size")
                    dg.Rows(n).Cells(4).ReadOnly = True
                    dg.Rows(n).Cells(5).Value = rw("color")
                    dg.Rows(n).Cells(5).ReadOnly = True
                    dg.Rows(n).Cells(6).Value = rw("u_itemgrp")
                    dg.Rows(n).Cells(6).ReadOnly = True
                    dg.Rows(n).Cells(7).Value = rw("mfd")
                    dg.Rows(n).Cells(7).ReadOnly = True
                    dg.Rows(n).Cells(8).Value = rw("cstype")
                    dg.Rows(n).Cells(8).ReadOnly = True
                    dg.Rows(n).Cells(9).Value = rw("mbarcode")
                    dg.Rows(n).Cells(9).ReadOnly = True
                    dg.Rows(n).Cells(10).Value = rw("txbarcode")
                    dg.Rows(n).Cells(10).ReadOnly = True
                    dg.Rows(n).Cells(11).Value = rw("boxmrp")
                    dg.Rows(n).Cells(11).ReadOnly = True
                    dg.Rows(n).Cells(12).Value = rw("mrp")
                    dg.Rows(n).Cells(12).ReadOnly = True
                    dg.Rows(n).Cells(13).Value = rw("boxqty")
                    dg.Rows(n).Cells(13).ReadOnly = True
                    dg.Rows(n).Cells(14).Value = rw("Quantity")
                    dg.Rows(n).Cells(15).Value = rw("Size2")
                    dg.Rows(n).Cells(15).ReadOnly = True
                    If Len(Trim(cmbfit.Text)) > 0 Then
                        dg.Rows(n).Cells(16).Value = cmbfit.Text
                    Else
                        dg.Rows(n).Cells(16).Value = rw("fit")
                    End If

                    dg.Rows(n).Cells(16).ReadOnly = True
                    If chkliberty.Checked = True Then
                        dg.Rows(n).Cells(17).Value = "LIBERTY CUT"
                    Else
                        dg.Rows(n).Cells(17).Value = rw("cut")
                    End If

                    dg.Rows(n).Cells(17).ReadOnly = True

                    dg.Rows(n).Cells(18).Value = rw("itemcode")
                    dg.Rows(n).Cells(18).ReadOnly = True
                    dg.Rows(n).Cells(19).Value = rw("batchnum")
                    dg.Rows(n).Cells(19).ReadOnly = True
                    dg.Rows(n).Cells(20).Value = rw("noprint")
                Next
            Else
                'txtmont.Text = 0
            End If
            Lblcnt.Text = dg.Rows.Count
            txtscanner.Focus()
        Else
            MsgBox("Select Barcode Type!..")
        End If

    End Sub

    Private Sub loadcmbyr()
        msql2 = "select distinct indicator from ofpr"
        cmbyr.Items.Clear()
        'Dim dt As DataTable = getDataTable(msql)
        'Dim dr As SqlClient.SqlDataReader
        'dr = getDataReader(msql)
        Dim dt As DataTable = getDataTable(msql2)
        cmbyr.DataSource = Nothing
        cmbyr.Items.Clear()
        cmbyr.DataSource = dt
        cmbyr.DisplayMember = "indicator"
        cmbyr.ValueMember = "indicator"


    End Sub

    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        If Val(txtdocnum.Text.ToString) > 0 Then
            dgas.Rows.Clear()
            txtnopack.Text = 0
            If OptSamDrf.Checked = True Then
                Call samplebarcode(Val(lbldocentry.Text))
            Else

                If Chkshirt.Checked = True Or chkpant.Checked = True Then
                    Call loaddata()
                    'If Dgpk.Visible = False Then Dgpk.Visible = True
                    'Call loadpackitem(Convert.ToInt32(lbldocentry.Text))
                    'ColorRowsByPackNo()
                    'loadconsolidatepack()

                    If OptSales.Checked = True Then
                        minvtypeh = "rinv7"
                        minvtyped = "rinv8"
                    ElseIf optdateord.Checked = True Then
                        minvtypeh = "rdln7"
                        minvtyped = "rdln8"
                    Else
                        minvtypeh = "rinv7"
                        minvtyped = "rinv8"
                    End If
                    loadpackdet(Val(lbldocentry.Text), minvtypeh, minvtyped)

                Else
                    MsgBox("Select Shirt or Pant Check Box!")
                End If

                'StartCameraDelayed()
            End If
            Btnchk.PerformClick()
        Else
            MsgBox("Pls Type Invoice/Delivery Number")
        End If

    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        'printsticker()
        If OptSamDrf.Checked = True Then
            sampleprint()
        ElseIf chkgstmrp.Checked = True Then
            shrmqrcode()
        Else
            If Chkonline.Checked = True Then
                'onlineprint()
                If chkecom.Checked = True Then
                    Call loadecomprn()
                ElseIf chkprima.Checked = True Then
                    Call loadprimaprn()
                Else
                    onlineprint2()
                End If
            Else
                'speedprint()
                If Chkvertical.Checked = True Then
                    speedprint2vert()
                Else
                    'speedprint2()
                    speedprint()
                End If

            End If

        End If

        'speedprint2() new
        'printraw()
        'PrintLabel()
    End Sub


    Private Sub printsticker()
        Dim btapp As New BarTender.Application
        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then
                'Dim btapp As New BarTender.Application
                Dim btFormat As BarTender.Format
                'btapp = New BarTender.Application
                mqty = Integer.Parse(txtno.Text.ToString)
                mfile = System.Windows.Forms.Application.StartupPath & "\Acclbl.btw"

                btFormat = btapp.Formats.Open(mfile, False, "")


                'specify printer. if not, printer specified in format is used.
                If Len(Trim(cmbprinter.Text)) > 0 Then
                    btFormat.Printer = cmbprinter.Text
                End If


                btFormat.SetNamedSubStringValue("MRP", Val(dg.Rows(i).Cells(12).Value))
                btFormat.SetNamedSubStringValue("u_subgrp6", Trim(dg.Rows(i).Cells(2).Value))
                btFormat.SetNamedSubStringValue("COLOR", Trim(dg.Rows(i).Cells(5).Value))
                btFormat.SetNamedSubStringValue("U_ITEMGRP", Trim(dg.Rows(i).Cells(6).Value))
                btFormat.SetNamedSubStringValue("U_STYLE", Trim(dg.Rows(i).Cells(3).Value))
                btFormat.SetNamedSubStringValue("U_SIZE", Trim(dg.Rows(i).Cells(4).Value))
                btFormat.SetNamedSubStringValue("MFD", Trim(dg.Rows(i).Cells(7).Value))
                btFormat.SetNamedSubStringValue("CSTYPE", Trim(dg.Rows(i).Cells(8).Value))
                btFormat.SetNamedSubStringValue("DOCNUM", Trim(dg.Rows(i).Cells(1).Value))
                btFormat.SetNamedSubStringValue("MBarcode", Trim(dg.Rows(i).Cells(9).Value))
                If Trim(cmbtype.Text) <> "Dealer" Or Trim(cmbtype.Text) <> "TN" Then
                    btFormat.SetNamedSubStringValue("AUTOCODE", Trim(dg.Rows(i).Cells(10).Value))
                End If


                'btFormat.IdenticalCopiesOfLabel = mqty
                btFormat.IdenticalCopiesOfLabel = Val(dg.Rows(i).Cells(14).Value)



                'Print the document

                'btFormat.PrintOut(False, False)

                'End the BarTender process
                btFormat.PrintOut(False, False)

                'btapp.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges)
                'System.Runtime.InteropServices.Marshal.ReleaseComObject(btapp)
            End If

        Next i
        btapp.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges)
        System.Runtime.InteropServices.Marshal.ReleaseComObject(btapp)
    End Sub

    Private Sub speedprint2()
        Dim dir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "nsbarcodE.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
        'If chkprndir.Checked = True Then
        '    FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        'Else


        Dim fNum As Integer = FileSystem.FreeFile()
        FileSystem.FileOpen(fNum, mdir, OpenMode.Output, OpenAccess.Write, OpenShare.Shared, -1)
        'FileSystem.PrintLine(fNum, "This is a line with the Rupee symbol: ₹")
        'FileSystem.FileClose(fNum)



        'FileOpen(1, mdir, OpenMode.Output)
        'End If
        Dim rupeeSymbol As String = ChrW(&H20B9)
        'Dim rupeeSymbol As String = ChrW(&H20B9)



        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then
                'PrintLine(1, TAB(0), DR.Item("firstdet"))
                If Chkshirt.Checked = True Then
                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='41.0 mm'></xpml>SIZE 67.5 mm, 41 mm")
                    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum, "SPEED 7")
                    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='41.0 mm'></xpml>SET TEAR ON")
                    FileSystem.PrintLine(fNum, "CLS")
                    FileSystem.PrintLine(fNum, "BITMAP 371,192,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'PrintLine(1, TAB(0), "TEXT BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    '"₹"
                    'PrintBitmap()
                    FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT 528,218," & """" & "0" & """" & ",180,13,12," & """" & "MRP Rs." & """")
                    'FileSystem.PrintLine(fNum, TAB(0), "TEXT 385,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP:" & """")
                    'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "IndianRupee.TTF" & """" & ",180,32,1," & """" & "`" & """")

                    'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "ITF Rupee.TTF" & """" & ",180,32,1," & """" & "S" & """")
                    'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & "₹" & """")

                    'FileSystem.PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & rupeeSymbol & """")

                    FileSystem.PrintLine(fNum, "TEXT 366,218," & """" & "0" & """" & ",180,14,12," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 390,280," & """" & "0" & """" & ",180,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :"
                    FileSystem.PrintLine(fNum, "TEXT 530,250," & """" & "0" & """" & ",180,10,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 528,313," & """" & "0" & """" & ",180,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 447,250," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 407,313," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 170,188," & """" & "0" & """" & ",180,11,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 93,185," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")

                    FileSystem.PrintLine(fNum, "TEXT 155,317," & """" & "0" & """" & ",180,9,11," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 99,316," & """" & "0" & """" & ",180,18,11," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 49,317," & """" & "0" & """" & ",180,9,12," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum, "TEXT 58,244," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1 N" & """")
                    FileSystem.PrintLine(fNum, "QRCODE 121,115,L,5,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 528,178," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 524,115," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 465,110," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 215,212," & """" & "ROMAN.TTF" & """" & ",180,8,7," & """" & "(Incl.of all Taxes)" & """")

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                        FileSystem.PrintLine(fNum, "TEXT 373,108," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum, "TEXT  523,281," & """" & "0" & """" & ",180,7,9," & """" & "Commodity Name:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 527,148," & """" & "0" & """" & ",180,10,9," & """" & "Made in India" & """")

                    FileSystem.PrintLine(fNum, "TEXT 524,67," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    FileSystem.PrintLine(fNum, "TEXT 524,40," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")
                    FileSystem.PrintLine(fNum, "TEXT 149,245," & """" & "0" & """" & ",180,9,9," & """" & "Net Qty :" & """")


                    If chkliberty.Checked = True Then
                        FileSystem.PrintLine(fNum, "TEXT 170,152," & """" & "0" & """" & ",180,10,9," & """" & "LIBERTY CUT" & """")
                    End If


                ElseIf chkpant.Checked = True Then

                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='41.0 mm'></xpml>SIZE 67.5 mm, 41 mm")
                    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum, "SPEED 7")
                    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='41.0 mm'></xpml>SET TEAR ON")
                    FileSystem.PrintLine(fNum, "CLS")
                    FileSystem.PrintLine(fNum, "BITMAP 369,192,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'PrintLine(1, TAB(0), "TEXT BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    '"₹"
                    'PrintBitmap()
                    FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT 528,218," & """" & "0" & """" & ",180,13,12," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum, "TEXT 366,218," & """" & "0" & """" & ",180,14,12," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 390,280," & """" & "0" & """" & ",180,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :"
                    FileSystem.PrintLine(fNum, "TEXT 530,250," & """" & "0" & """" & ",180,10,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 528,313," & """" & "0" & """" & ",180,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 447,250," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 414,313," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 175,188," & """" & "0" & """" & ",180,11,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 96,185," & """" & "0" & """" & ",180,8,10," & """" & "LENGTH" & """")


                    FileSystem.PrintLine(fNum, "TEXT 183,312," & """" & "0" & """" & ",180,8,9," & """" & "Size: Waist" & """")
                    FileSystem.PrintLine(fNum, "TEXT 73,155," & """" & "0" & """" & ",180,18,12," & """" & Trim(Math.Round(Val(dg.Rows(i).Cells(4).Value) * 2.54, 0)) & """")
                    FileSystem.PrintLine(fNum, "TEXT 49,312," & """" & "0" & """" & ",180,8,10," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum, "TEXT  63,244," & """" & "0" & """" & ",180,11,9," & """" & "1 N" & """")
                    FileSystem.PrintLine(fNum, "QRCODE 121,115,L,5,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 528,178," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 524,115," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 465,110," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 221,212," & """" & "0" & """" & ",180,8,7," & """" & "(Incl.of all Taxes)" & """")

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                        FileSystem.PrintLine(fNum, "TEXT 373,108," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum, "TEXT  528,281," & """" & "0" & """" & ",180,7,9," & """" & "Commodity Name:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 527,147," & """" & "0" & """" & ",180,10,9," & """" & "Made in India" & """")

                    FileSystem.PrintLine(fNum, "TEXT 524,67," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    FileSystem.PrintLine(fNum, "TEXT 524,40," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")
                    FileSystem.PrintLine(fNum, "TEXT 149,245," & """" & "0" & """" & ",180,10,9," & """" & "Net Qty :" & """")

                    FileSystem.PrintLine(fNum, "TEXT 85,311," & """" & "0" & """" & ",180,8,10," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 130,153," & """" & "0" & """" & ",180,8,10," & """" & "Code :" & """")




                ElseIf chktwin.Checked = True Then
                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='41.0 mm'></xpml>SIZE 67.5 mm, 41 mm")
                    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum, "SPEED 7")
                    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='41.0 mm'></xpml>SET TEAR ON")
                    FileSystem.PrintLine(fNum, "CLS")
                    'FileSystem.PrintLine(fNum, "BITMAP 322,162,9,56,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'PrintLine(1, TAB(0), "TEXT BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    FileSystem.PrintLine(fNum, "BITMAP 370,190,3,32,1,ðÿø?ÿüÿþÿÿ ÿÿƒÿÿÁÿÿàÿÿðÿø?ÿüÿøÿ€ÿ ÿüÿü?ÿøÿøÿ€   ø?ÿüÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    FileSystem.PrintLine(fNum, "BITMAP 323,163,2,24,1,áÿðÿøü?þ?ÿÿÿÇÿü?øÿñÿÀ  ñÿø€  ÿÿÿÿÿÿÿÿÿÿÿÿ")


                    'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    '"₹"
                    'PrintBitmap()
                    FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT 528,218," & """" & "0" & """" & ",180,13,12," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum, "TEXT 366,218," & """" & "0" & """" & ",180,14,12," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 365,283," & """" & "0" & """" & ",180,10,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :"
                    FileSystem.PrintLine(fNum, "TEXT 530,250," & """" & "0" & """" & ",180,10,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 528,313," & """" & "0" & """" & ",180,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 447,250," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 406,313," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 168,155," & """" & "0" & """" & ",180,11,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT  93,152," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")

                    FileSystem.PrintLine(fNum, "TEXT 152,321," & """" & "0" & """" & ",180,9,11," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 100,316," & """" & "0" & """" & ",180,19,10," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 44,314," & """" & "0" & """" & ",180,7,9," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum, "TEXT 69,249," & """" & "0" & """" & ",180,11,9," & """" & "2 N" & """")
                    FileSystem.PrintLine(fNum, "QRCODE 121,115,L,5,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 524,146," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 524,115," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 465,110," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 219,217," & """" & "0" & """" & ",180,9,9," & """" & "(Incl.of all Taxes)" & """")

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                        FileSystem.PrintLine(fNum, "TEXT 372,108," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum, "TEXT  528,282," & """" & "0" & """" & ",180,8,9," & """" & "Commodity Name:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 338,148," & """" & "0" & """" & ",180,10,9," & """" & "Made in India" & """")

                    FileSystem.PrintLine(fNum, "TEXT 524,67," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    FileSystem.PrintLine(fNum, "TEXT 524,40," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")
                    FileSystem.PrintLine(fNum, "TEXT 528,181," & """" & "0" & """" & ",180,9,8," & """" & "Unit Sale Price: Rs." & """")

                    FileSystem.PrintLine(fNum, "TEXT 319,180," & """" & "0" & """" & ",180,11,8," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value) / 2, "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 229,180," & """" & "0" & """" & ",180,6,7," & """" & "(Incl.of all Taxes)" & """")

                    FileSystem.PrintLine(fNum, "TEXT 168,253," & """" & "0" & """" & ",180,10,9," & """" & "Net Qty :" & """")




                ElseIf chkset.Checked = True Then

                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='41.0 mm'></xpml>SIZE 67.5 mm, 41 mm")
                    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum, "SPEED 7")
                    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='41.0 mm'></xpml>SET TEAR ON")
                    FileSystem.PrintLine(fNum, "CLS")
                    FileSystem.PrintLine(fNum, "BITMAP 366,96,4,40,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    FileSystem.PrintLine(fNum, "BITMAP 214,152,21,24,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")

                    FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT 527,123," & """" & "0" & """" & ",180,13,12," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum, "TEXT 368,123," & """" & "0" & """" & ",180,14,12," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    'FileSystem.PrintLine(fNum, "TEXT 365,283," & """" & "0" & """" & ",180,10,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :"
                    FileSystem.PrintLine(fNum, "TEXT 527,218," & """" & "0" & """" & ",80,11,7," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 528,290," & """" & "0" & """" & ",180,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 411,218," & """" & "0" & """" & ",180,9,7," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 411,286," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 411,195," & """" & "0" & """" & ",180,8,7," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 364,195," & """" & "0" & """" & ",180,6,7," & """" & "SLEEVE" & """")
                    FileSystem.PrintLine(fNum, "TEXT 527,261," & """" & "0" & """" & ",180,11,7," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum, "TEXT  411,264," & """" & "0" & """" & ",180,13,9," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 373,264," & """" & "0" & """" & ",180,7,9," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum, "TEXT 414,238," & """" & "0" & """" & ",180,8,7," & """" & "2 N" & """")
                    FileSystem.PrintLine(fNum, "QRCODE 100,95,L,4,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 133,221," & """" & "0" & """" & ",180,7,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 524,87," & """" & "0" & """" & ",180,15,10," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 465,84," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 230,117," & """" & "0" & """" & ",180,7,7," & """" & "(Incl.of all Taxes)" & """")

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                        FileSystem.PrintLine(fNum, "TEXT 373,83," & """" & "0" & """" & ",180,8,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum, "TEXT 528,316," & """" & "0" & """" & ",180,7,8," & """" & "Commodity Name:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 134,188," & """" & "0" & """" & ",180,8,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum, "TEXT 524,61," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    FileSystem.PrintLine(fNum, "TEXT 524,34," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")
                    FileSystem.PrintLine(fNum, "TEXT 528,172," & """" & "0" & """" & ",180,6,8," & """" & "Unit Sale Price: Rs." & """")
                    FileSystem.PrintLine(fNum, "TEXT 364,171," & """" & "0" & """" & ",180,9,8," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value) / 2, "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 411,147," & """" & "0" & """" & ",180,6,6," & """" & "(Incl.of all Taxes)" & """")
                    FileSystem.PrintLine(fNum, "TEXT 527,239," & """" & "0" & """" & ",180,9,7," & """" & "Net Qty :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 387,318," & """" & "0" & """" & ",180,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    '*****

                    FileSystem.PrintLine(fNum, "TEXT  259,286," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 259,194," & """" & "0" & """" & ",180,8,7," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 212,194," & """" & "0" & """" & ",180,6,7," & """" & "SLEEVE" & """")
                    FileSystem.PrintLine(fNum, "TEXT 527,261," & """" & "0" & """" & ",180,11,7," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 224,264," & """" & "0" & """" & ",180,7,9," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum, "TEXT 259,218," & """" & "0" & """" & ",180,8,7," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 528,194," & """" & "0" & """" & ",180,11,7," & """" & "Style:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 209,170," & """" & "0" & """" & ",180,9,8," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value) / 2, "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 258,171," & """" & "0" & """" & ",180,8,8," & """" & "Rs." & """")
                    FileSystem.PrintLine(fNum, "TEXT 259,147," & """" & "0" & """" & ",180,6,6," & """" & "(Incl.of all Taxes)" & """")
                    FileSystem.PrintLine(fNum, "TEXT 259,238," & """" & "0" & """" & ",180,8,7," & """" & "2 N" & """")
                    FileSystem.PrintLine(fNum, "TEXT 259,238," & """" & "0" & """" & ",180,8,8," & """" & "Rs." & """")

                Else
                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='41.0 mm'></xpml>SIZE 67.5 mm, 41 mm")
                    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum, "SPEED 7")
                    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='41.0 mm'></xpml>SET TEAR ON")
                    FileSystem.PrintLine(fNum, "CLS")
                    FileSystem.PrintLine(fNum, "BITMAP 371,192,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'PrintLine(1, TAB(0), "TEXT BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    '"₹"
                    'PrintBitmap()
                    FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT 528,218," & """" & "0" & """" & ",180,13,12," & """" & "MRP Rs." & """")
                    'FileSystem.PrintLine(fNum, TAB(0), "TEXT 385,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP:" & """")
                    'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "IndianRupee.TTF" & """" & ",180,32,1," & """" & "`" & """")

                    'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "ITF Rupee.TTF" & """" & ",180,32,1," & """" & "S" & """")
                    'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & "₹" & """")

                    'FileSystem.PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & rupeeSymbol & """")

                    FileSystem.PrintLine(fNum, "TEXT 366,218," & """" & "0" & """" & ",180,14,12," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 390,280," & """" & "0" & """" & ",180,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :"
                    FileSystem.PrintLine(fNum, "TEXT 530,250," & """" & "0" & """" & ",180,10,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 528,313," & """" & "0" & """" & ",180,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 447,250," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 407,313," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 170,188," & """" & "0" & """" & ",180,11,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 93,185," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")

                    FileSystem.PrintLine(fNum, "TEXT 155,317," & """" & "0" & """" & ",180,9,11," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 99,316," & """" & "0" & """" & ",180,18,11," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 49,317," & """" & "0" & """" & ",180,9,12," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum, "TEXT 58,244," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1 N" & """")
                    FileSystem.PrintLine(fNum, "QRCODE 121,115,L,5,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 528,178," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 524,115," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 465,110," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 215,212," & """" & "ROMAN.TTF" & """" & ",180,8,7," & """" & "(Incl.of all Taxes)" & """")

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                        FileSystem.PrintLine(fNum, "TEXT 373,108," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum, "TEXT  523,281," & """" & "0" & """" & ",180,7,9," & """" & "Commodity Name:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 527,148," & """" & "0" & """" & ",180,10,9," & """" & "Made in India" & """")

                    FileSystem.PrintLine(fNum, "TEXT 524,67," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    FileSystem.PrintLine(fNum, "TEXT 524,40," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")
                    FileSystem.PrintLine(fNum, "TEXT 149,245," & """" & "0" & """" & ",180,9,9," & """" & "Net Qty :" & """")


                    If chkliberty.Checked = True Then
                        FileSystem.PrintLine(fNum, "TEXT 170,152," & """" & "0" & """" & ",180,10,9," & """" & "LIBERTY CUT" & """")
                    End If



                    FileSystem.PrintLine(fNum, "TEXT 538,230," & """" & "0" & """" & ",180,8,9," & """" & "CONTENT :" & """")
                    'TEXT 439,258,"ROMAN.TTF",180,1,9,"SL18-CYAN"
                    FileSystem.PrintLine(fNum, "TEXT 439,258," & """" & "ROMAN.TTF" & """" & ",180,1,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 430,230," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 182,252," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                        'Text(109, 249, "0", 180, 11, 10, "Length")
                        FileSystem.PrintLine(fNum, "TEXT 109,249," & """" & "0" & """" & ",180,11,10," & """" & "Length" & """")
                        'Text(182, 211, "0", 180, 9, 17, "Code:")
                        FileSystem.PrintLine(fNum, "TEXT 182,211," & """" & "0" & """" & ",180,9,17," & """" & "Code:" & """")
                        FileSystem.PrintLine(fNum, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                        'TEXT 63,198,"0",180,7,10,"(Inch)"
                        FileSystem.PrintLine(fNum, "TEXT 63,198," & """" & "0" & """" & ",180,7,10," & """" & "(Inch)" & """")
                    Else
                        FileSystem.PrintLine(fNum, "TEXT 103,249," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")
                        FileSystem.PrintLine(fNum, "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """")
                        FileSystem.PrintLine(fNum, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                        FileSystem.PrintLine(fNum, "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """")
                    End If

                    'TEXT 172,158,"0",180,11,9,"Net Qty :1N"
                    'FileSystem.PrintLine(fNum, "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 116,216," & """" & "0" & """" & ",180,16,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    'FileSystem.PrintLine(fNum, "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """")
                    If chktwin.Checked = True Then
                        FileSystem.PrintLine(fNum, "TEXT 172,158," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :2N" & """")
                    ElseIf chkset.Checked = True Then
                        FileSystem.PrintLine(fNum, "TEXT 172,158," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :" & Val(txtno.Text) & "N" & """")
                    Else
                        FileSystem.PrintLine(fNum, "TEXT 172,158," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1N" & """")
                    End If

                    FileSystem.PrintLine(fNum, "QRCODE 519,199,L,3,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 367,159," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 322,131," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 277,131," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 346,313," & """" & "ROMAN.TTF" & """" & ",180,1,7," & """" & "(Incl.of all Taxes)" & """")
                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                        FileSystem.PrintLine(fNum, "TEXT 538,133," & """" & "0" & """" & ",180,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If
                    If chktwin.Checked = True Then
                        FileSystem.PrintLine(fNum, "TEXT 182,291," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "(1+1) 2N" & """")
                    End If
                    If chkliberty.Checked = True Then
                        FileSystem.PrintLine(fNum, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & "LIBERTY CUT" & """")
                    End If


                    If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                        FileSystem.PrintLine(fNum, "TEXT 120,279," & """" & "0" & """" & ",180,12,9," & """" & (Trim(dg.Rows(i).Cells(15).Value) & " cm") & """")
                        FileSystem.PrintLine(fNum, "TEXT 182,283," & """" & "0" & """" & ",180,11,11," & """" & "Size:" & """")
                        'FileSystem.PrintLine(fNum, "TEXT 63,279," & """" & "0" & """" & ",180,10,9," & """" & "cm" & """")
                    End If
                End If



                FileSystem.PrintLine(fNum, "PRINT 1," & Val(dg.Rows(i).Cells(14).Value))
                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")






                'PrintLine(1, TAB(0), "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                'PrintLine(1, TAB(0), "DIRECTION 0,0")
                'PrintLine(1, TAB(0), "REFERENCE 0,0")
                'PrintLine(1, TAB(0), "OFFSET 0 mm")
                'PrintLine(1, TAB(0), "SPEED 14")
                'PrintLine(1, TAB(0), "SET PEEL OFF")
                'PrintLine(1, TAB(0), "SET CUTTER OFF")
                'PrintLine(1, TAB(0), "SET PARTIAL_CUTTER OFF")
                'PrintLine(1, TAB(0), "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
                'PrintLine(1, TAB(0), "CLS")
                ''PrintLine(1, TAB(0), "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                ''PrintLine(1, TAB(0), "TEXTBITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                ''"₹"
                ''PrintBitmap()
                'PrintLine(1, TAB(0), "CODEPAGE 1252")
                'PrintLine(1, TAB(0), "TEXT 385,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP:" & """")
                ''PrintLine(1, TAB(0), "TEXT 283,312," & """" & "IndianRupee.ttf" & """" & ",180,32,1," & """" & "`" & """")

                ''PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & "₹" & """")

                'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & rupeeSymbol & """")

                'PrintLine(1, TAB(0), "TEXT 277,339," & """" & "0" & """" & ",180,14,12," & """" & Trim(dg.Rows(i).Cells(12).Value) & """")
                'PrintLine(1, TAB(0), "TEXT 538,291," & """" & "0" & """" & ",180,14,12," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                'PrintLine(1, TAB(0), "TEXT 538,258," & """" & "0" & """" & ",180,10,9," & """" & "COL:" & """")
                'PrintLine(1, TAB(0), "TEXT 538,230," & """" & "0" & """" & ",180,8,9," & """" & "CONTENT :" & """")
                'PrintLine(1, TAB(0), "TEXT 476,258," & """" & "ROMAN.TTF" & """" & ",180,1,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                'PrintLine(1, TAB(0), "TEXT 430,230," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                'PrintLine(1, TAB(0), "TEXT 182,252," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                'PrintLine(1, TAB(0), "TEXT 103,249," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")
                'PrintLine(1, TAB(0), "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """")
                'PrintLine(1, TAB(0), "TEXT 116,216," & """" & "0" & """" & ",180,16,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                'PrintLine(1, TAB(0), "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """")
                'PrintLine(1, TAB(0), "TEXT 126,158," & """" & "0" & """" & ",180,12,9," & """" & "Qty :1N" & """")
                'PrintLine(1, TAB(0), "QRCODE 519,199,L,3,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                'PrintLine(1, TAB(0), "TEXT 368,159," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                'PrintLine(1, TAB(0), "TEXT 334,131," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                'PrintLine(1, TAB(0), "TEXT 277,131," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                'PrintLine(1, TAB(0), "TEXT 346,313," & """" & "ROMAN.TTF" & """" & ",180,1,7," & """" & "(Incl.of all Taxes)" & """")
                'If Trim(cmbtype.Text) <> "Dealer" Or Trim(cmbtype.Text) <> "TN" Then
                '    PrintLine(1, TAB(0), "TEXT 538,127," & """" & "0" & """" & ",180,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                'End If

                'PrintLine(1, TAB(0), "PRINT 1," & Val(dg.Rows(i).Cells(14).Value))
                'PrintLine(1, TAB(0), "<xpml></page></xpml><xpml><end/></xpml>")







            End If
        Next
        FileSystem.FileClose(fNum)
        'FileClose(1)


        'PrintTextFile(mdir)
        'PrintToTSCPrinter(mdir, Trim(cmbprinter.Text))


        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
            ' Call updateprn()

            If mcitrix = "Y" Then
                citrixprint(mdir)
            Else
                If MsgBox("Lpt Port", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text) & ":")
                    Shell("rawpr.bat " & mdir)

                Else

                    'Dim text As String = File.ReadAllText(mdir)
                    Dim text As String = File.ReadAllText(mdir, System.Text.Encoding.GetEncoding(1252))
                    Dim pd As PrintDialog = New PrintDialog()
                    pd.PrinterSettings = New PrinterSettings()
                    If Len(Trim(cmbprinter.Text)) > 0 Then
                        BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName = Trim(cmbprinter.Text), text)
                    Else
                        BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName, text)
                    End If

                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                    'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                End If
            End If
        End If


        'PrintLine(1, TAB(0), "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
        'PrintLine(1, TAB(0), "DIRECTION 0,0")
        'PrintLine(1, TAB(0), "REFERENCE 0,0")
        'PrintLine(1, TAB(0), "OFFSET 0 mm")
        'PrintLine(1, TAB(0), "SPEED 14")
        'PrintLine(1, TAB(0), "SET PEEL OFF")
        'PrintLine(1, TAB(0), "SET CUTTER OFF")
        'PrintLine(1, TAB(0), "SET PARTIAL_CUTTER OFF")
        'PrintLine(1, TAB(0), "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
        'PrintLine(1, TAB(0), "CLS")
        'PrintLine(1, TAB(0), "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
        'PrintLine(1, TAB(0), "CODEPAGE 1252")
        'PrintLine(1, TAB(0), "TEXT 385,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP:" & """")
        'PrintLine(1, TAB(0), "TEXT 277,339," & """" & "0" & """" & ",180,14,12," & """" & "1195.00" & """")
        'PrintLine(1, TAB(0), "TEXT 538,291," & """" & "0" & """" & ",180,14,12," & """" & "CLASSIC COTTON LUXURY FUL" & """")
        'PrintLine(1, TAB(0), "TEXT 538,258," & """" & "0" & """" & ",180,10,9," & """" & "COL:" & """")
        'PrintLine(1, TAB(0), "TEXT 538,230," & """" & "0" & """" & ",180,8,9," & """" & "CONTENT :" & """")
        'PrintLine(1, TAB(0), "TEXT 476,258," & """" & "ROMAN.TTF" & """" & ",180,1,9," & """" & "SL18-CYAN" & """")
        'PrintLine(1, TAB(0), "TEXT 430,230," & """" & "0" & """" & ",180,9,9," & """" & "KURTHA AND PYJAMA" & """")
        'PrintLine(1, TAB(0), "TEXT 182,252," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "FULL" & """")
        'PrintLine(1, TAB(0), "TEXT 103,249," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")
        'PrintLine(1, TAB(0), "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """")
        'PrintLine(1, TAB(0), "TEXT 116,216," & """" & "0" & """" & ",180,16,21," & """" & "38" & """")
        'PrintLine(1, TAB(0), "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """")
        'PrintLine(1, TAB(0), "TEXT 126,158," & """" & "0" & """" & ",180,12,9," & """" & "Qty :1N" & """")
        'PrintLine(1, TAB(0), "QRCODE 519,199,L,3,A,180,M2,S7," & """" & "12345678" & """")
        'PrintLine(1, TAB(0), "TEXT 368,159," & """" & "0" & """" & ",180,10,8," & """" & "MFD: Nov 2023" & """")
        'PrintLine(1, TAB(0), "TEXT 334,131," & """" & "0" & """" & ",180,18,12," & """" & "##" & """")
        'PrintLine(1, TAB(0), "TEXT 277,131," & """" & "0" & """" & ",180,9,8," & """" & "51924" & """")
        'PrintLine(1, TAB(0), "TEXT 346,313," & """" & "ROMAN.TTF" & """" & ",180,1,7," & """" & "(Incl.of all Taxes)" & """")
        'PrintLine(1, TAB(0), "TEXT 538,127," & """" & "0" & """" & ",180,9,7," & """" & "2311-9000F36G99-3S" & """")
        'PrintLine(1, TAB(0), "PRINT 1,3")
        'PrintLine(1, TAB(0), "<xpml></page></xpml><xpml><end/></xpml>")


    End Sub
    Private Sub speedprint()
        Dim dir As String

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "nsbarcodEH.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
        'If chkprndir.Checked = True Then
        '    FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        'Else


        Dim fNum As Integer = FileSystem.FreeFile()
        FileSystem.FileOpen(fNum, mdir, OpenMode.Output, OpenAccess.Write, OpenShare.Shared, -1)
        'FileSystem.PrintLine(fNum, "This is a line with the Rupee symbol: ₹")
        'FileSystem.FileClose(fNum)



        'FileOpen(1, mdir, OpenMode.Output)
        'End If
        Dim rupeeSymbol As String = ChrW(&H20B9)
        'Dim rupeeSymbol As String = ChrW(&H20B9)
        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then
                'PrintLine(1, TAB(0), DR.Item("firstdet"))

                FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum, "SPEED 7")
                FileSystem.PrintLine(fNum, "SET PEEL OFF")
                FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
                FileSystem.PrintLine(fNum, "CLS")
                FileSystem.PrintLine(fNum, "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                'PrintLine(1, TAB(0), "TEXT BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                '"₹"
                'PrintBitmap()
                FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                FileSystem.PrintLine(fNum, TAB(0), "TEXT 433,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP Rs." & """")
                'FileSystem.PrintLine(fNum, TAB(0), "TEXT 385,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP:" & """")
                'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "IndianRupee.TTF" & """" & ",180,32,1," & """" & "`" & """")

                'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "ITF Rupee.TTF" & """" & ",180,32,1," & """" & "S" & """")
                'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & "₹" & """")

                'FileSystem.PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & rupeeSymbol & """")

                FileSystem.PrintLine(fNum, "TEXT 277,339," & """" & "0" & """" & ",180,14,12," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                FileSystem.PrintLine(fNum, "TEXT 538,291," & """" & "0" & """" & ",180,11,12," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                'TEXT 538,258,"0",180,9,9,"COLOUR :"
                FileSystem.PrintLine(fNum, "TEXT 538,258," & """" & "0" & """" & ",180,9,9," & """" & "COLOUR :" & """")
                FileSystem.PrintLine(fNum, "TEXT 538,230," & """" & "0" & """" & ",180,8,9," & """" & "COMMODITY :" & """")
                'TEXT 439,258,"ROMAN.TTF",180,1,9,"SL18-CYAN"
                FileSystem.PrintLine(fNum, "TEXT 439,258," & """" & "ROMAN.TTF" & """" & ",180,1,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")

                FileSystem.PrintLine(fNum, "TEXT 410,230," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 367,189," & """" & "0" & """" & ",180,10,8," & """" & "Made in India" & """")


                FileSystem.PrintLine(fNum, "TEXT 182,252," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")


                If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                    'Text(109, 249, "0", 180, 11, 10, "Length")
                    FileSystem.PrintLine(fNum, "TEXT 109,249," & """" & "0" & """" & ",180,11,10," & """" & "Length" & """")
                    'Text(182, 211, "0", 180, 9, 17, "Code:")
                    FileSystem.PrintLine(fNum, "TEXT 182,211," & """" & "0" & """" & ",180,9,17," & """" & "Code:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    'TEXT 63,198,"0",180,7,10,"(Inch)"
                    FileSystem.PrintLine(fNum, "TEXT 63,198," & """" & "0" & """" & ",180,7,10," & """" & "(Inch)" & """")
                Else
                    FileSystem.PrintLine(fNum, "TEXT 103,249," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")
                    FileSystem.PrintLine(fNum, "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """")
                End If

                'TEXT 172,158,"0",180,11,9,"Net Qty :1N"
                'FileSystem.PrintLine(fNum, "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """")
                'FileSystem.PrintLine(fNum, "TEXT 116,216," & """" & "0" & """" & ",180,16,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                'FileSystem.PrintLine(fNum, "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """")
                If chktwin.Checked = True Then
                    FileSystem.PrintLine(fNum, "TEXT 182,160," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :2N" & """")
                ElseIf chkset.Checked = True Then
                    FileSystem.PrintLine(fNum, "TEXT 182,160," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :" & Val(txtno.Text) & "N" & """")
                Else
                    FileSystem.PrintLine(fNum, "TEXT 182,160," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1N" & """")
                End If

                FileSystem.PrintLine(fNum, "QRCODE 519,199,L,3,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 367,159," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 322,131," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 277,131," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 346,313," & """" & "ROMAN.TTF" & """" & ",180,1,7," & """" & "(Incl.of all Taxes)" & """")
                If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                    FileSystem.PrintLine(fNum, "TEXT 538,133," & """" & "0" & """" & ",180,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                End If
                If chktwin.Checked = True Then
                    FileSystem.PrintLine(fNum, "TEXT 182,291," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "(1+1) 2N" & """")
                End If
                'If chkliberty.Checked = True Then
                'FileSystem.PrintLine(fNum, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & "LIBERTY CUT" & """")
                'FileSystem.PrintLine(fNum, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                'End If


                If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                    FileSystem.PrintLine(fNum, "TEXT 120,279," & """" & "0" & """" & ",180,12,9," & """" & (Trim(dg.Rows(i).Cells(15).Value) & " cm") & """")
                    FileSystem.PrintLine(fNum, "TEXT 182,283," & """" & "0" & """" & ",180,11,11," & """" & "Size:" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 63,279," & """" & "0" & """" & ",180,10,9," & """" & "cm" & """")
                End If
                If Chkshirt.Checked = True Then
                    FileSystem.PrintLine(fNum, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & Trim(dg.Rows(i).Cells(16).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 182,130," & """" & "0" & """" & ",180,8,9," & """" & Trim(dg.Rows(i).Cells(17).Value) & """")
                End If

                'Text(182, 282, "0", 180, 8, 9, "PRESTIGIOUS FIT")
                'Text(182, 130, "0", 180, 8, 9, "LIBERTY CUT")

                'Text(120, 279, "0", 180, 12, 9, "106")
                'Text(182, 283, "0", 180, 11, 11, "Size:")
                'Text(63, 279, "0", 180, 10, 9, "cm")
                If chkmfg.Checked = False Then
                    FileSystem.PrintLine(fNum, "TEXT 464,107," & """" & "0" & """" & ",180,8,7," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    FileSystem.PrintLine(fNum, "TEXT 444,85," & """" & "0" & """" & ",180,8,7," & """" & "address details are available in the box" & """")
                End If



                'FileSystem.PrintLine(fNum, "PRINT 1," & Val(dg.Rows(i).Cells(14).Value))
                FileSystem.PrintLine(fNum, "PRINT 1,1")
                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                dg.Rows(i).Cells(0).Value = False
                dg.Rows(i).DefaultCellStyle.BackColor = Color.White
            End If

        Next
        FileSystem.FileClose(fNum)
        'FileClose(1)


        'PrintTextFile(mdir)
        'PrintToTSCPrinter(mdir, Trim(cmbprinter.Text))




        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
            ' Call updateprn()
            If mcitrix = "Y" Then
                'citrixprint(mdir)
                citrixprint2(mdir, cmbprinter.SelectedItem.ToString())
            Else
                If MsgBox("Lpt Port", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'Shell("RAWPRINT " & Trim(cmbprinter.Text) & " " & mdir)
                    Shell("rawpr.bat " & mdir)

                Else

                    'Dim text As String = File.ReadAllText(mdir)
                    Dim text As String = File.ReadAllText(mdir, System.Text.Encoding.GetEncoding(1252))
                    Dim pd As PrintDialog = New PrintDialog()
                    pd.PrinterSettings = New PrinterSettings()
                    If Len(Trim(cmbprinter.Text)) > 0 Then
                        'MsgBox(cmbprinter.Text)
                        BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName = Trim(cmbprinter.Text), text)
                    Else
                        BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName, text)
                    End If

                End If
            End If
        End If

        cmbfit.Text = ""
        'chkliberty.Checked = False


    End Sub
    Private Sub citrixprint(ByVal filepath As String)
        'Dim printerPath As String = "\\TSClient\PrinterName" ' Adjust for Citrix printer redirection
        'Dim filePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nsbarcodE.txt")
        Dim printerPath As String = GetDefaultPrinterName()
        'MsgBox("default Print" & printerPath)
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

    Private Sub citrixprint2(ByVal filepath As String, ByVal printerName As String)
        Try
            ' Read the print command (raw ZPL/EPL/TSC) from file
            Dim commantstxt As String = File.ReadAllText(filepath, System.Text.Encoding.GetEncoding(1252))

            ' Create the print document and assign the specific printer
            Dim printDoc As New Printing.PrintDocument()
            printDoc.PrinterSettings.PrinterName = printerName

            ' Validate printer
            If Not printDoc.PrinterSettings.IsValid Then
                MessageBox.Show("Invalid printer: " & printerName, "Print Error")
                Return
            End If

            ' Send raw string to the printer
            BarcodePrint.SendStringToPrinter(printerName, commantstxt)

            ' Optional: invoke dummy print if needed (not required for raw send)
            ' printDoc.Print()

            'RawPrinterHelper.SendStringToPrinter(printerName, commandText)

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Print Error")
        End Try
    End Sub



    Private Sub onlineprint()
        Dim dir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"
        Dim mno As Integer
        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "nsbarcodE.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
        'If chkprndir.Checked = True Then
        '    FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        'Else
        mno = 1

        Dim fNum As Integer = FileSystem.FreeFile()
        FileSystem.FileOpen(fNum, mdir, OpenMode.Output, OpenAccess.Write, OpenShare.Shared, -1)
        'FileSystem.PrintLine(fNum, "This is a line with the Rupee symbol: ₹")
        'FileSystem.FileClose(fNum)



        'FileOpen(1, mdir, OpenMode.Output)
        'End If
        Dim rupeeSymbol As String = ChrW(&H20B9)
        'Dim rupeeSymbol As String = ChrW(&H20B9)
        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then
                'PrintLine(1, TAB(0), DR.Item("firstdet"))

                FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='23.5 mm'></xpml>SIZE 77.6 mm, 45 mm")
                FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum, "SPEED 7")
                FileSystem.PrintLine(fNum, "SET PEEL OFF")
                FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='23.5 mm'></xpml>SET TEAR ON")
                FileSystem.PrintLine(fNum, "CLS")
                'FileSystem.PrintLine(fNum, "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                '"₹"
                'PrintBitmap()
                FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")


                FileSystem.PrintLine(fNum, TAB(0), "TEXT 619,182," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                FileSystem.PrintLine(fNum, TAB(0), "TEXT 619,142," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                FileSystem.PrintLine(fNum, TAB(0), "TEXT 619,107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                FileSystem.PrintLine(fNum, TAB(0), "TEXT 619,61," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                FileSystem.PrintLine(fNum, "QRCODE 426,156,L,5,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                FileSystem.PrintLine(fNum, TAB(0), "TEXT 520,107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                FileSystem.PrintLine(fNum, TAB(0), "TEXT 495,107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")


                FileSystem.PrintLine(fNum, TAB(0), "TEXT 308,182," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                FileSystem.PrintLine(fNum, TAB(0), "TEXT 308,142," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                FileSystem.PrintLine(fNum, TAB(0), "TEXT 305,107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                FileSystem.PrintLine(fNum, TAB(0), "TEXT 308,61," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                FileSystem.PrintLine(fNum, "QRCODE 110,156,L,5,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                FileSystem.PrintLine(fNum, TAB(0), "TEXT 215,107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                FileSystem.PrintLine(fNum, TAB(0), "TEXT 185,107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")

                FileSystem.PrintLine(fNum, "PRINT 1," & Val(dg.Rows(i).Cells(14).Value))
                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")












            End If
        Next
        FileSystem.FileClose(fNum)
        'FileClose(1)


        'PrintTextFile(mdir)
        'PrintToTSCPrinter(mdir, Trim(cmbprinter.Text))


        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
            ' Call updateprn()

            If mcitrix = "Y" Then
                citrixprint(mdir)
            Else
                If MsgBox("Lpt Port", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text) & ":")
                    Shell("rawpr.bat " & mdir)

                Else
                    'Dim builder StringBuilder = new StringBuilder
                    'Dim file As System.IO.StreamWriter
                    'file = My.Computer.FileSystem.OpenTextFileWriter(System.Configuration.ConfigurationSettings.AppSettings("reportpath") + "\test.txt", False, System.Text.Encoding.ASCII)


                    'file.WriteLine(builder.ToString())
                    'file.Close()


                    'Dim text As String = File.ReadAllText(mdir)
                    Dim text As String = File.ReadAllText(mdir, System.Text.Encoding.GetEncoding(1252))
                    Dim pd As PrintDialog = New PrintDialog()
                    pd.PrinterSettings = New PrinterSettings()
                    If Len(Trim(cmbprinter.Text)) > 0 Then
                        BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName = Trim(cmbprinter.Text), text)
                    Else
                        BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName, text)
                    End If

                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                    'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                End If
            End If
        End If

        cmbfit.Text = ""
        'chkliberty.Checked = False


    End Sub

    Private Sub onlineprint2()
        Dim dir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"
        Dim mno, rwno As Integer
        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "online.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
        'If chkprndir.Checked = True Then
        '    FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        'Else
        mno = 1
        rwno = 0
        Dim fNum As Integer = FileSystem.FreeFile()
        FileSystem.FileOpen(fNum, mdir, OpenMode.Output, OpenAccess.Write, OpenShare.Shared, -1)
        'FileSystem.PrintLine(fNum, "This is a line with the Rupee symbol: ₹")
        'FileSystem.FileClose(fNum)



        'FileOpen(1, mdir, OpenMode.Output)
        'End If
        Dim rupeeSymbol As String = ChrW(&H20B9)
        'Dim rupeeSymbol As String = ChrW(&H20B9)
        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then
                'PrintLine(1, TAB(0), DR.Item("firstdet"))

                FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='23.5 mm'></xpml>SIZE 77.6 mm, 45 mm")
                FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum, "SPEED 7")
                FileSystem.PrintLine(fNum, "SET PEEL OFF")
                FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='23.5 mm'></xpml>SET TEAR ON")
                FileSystem.PrintLine(fNum, "CLS")
                'FileSystem.PrintLine(fNum, "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                '"₹"
                'PrintBitmap()
                FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")

                For j = 1 To Val(dg.Rows(i).Cells(14).Value)
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(619 - rwno) & ",182," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(619 - rwno) & ",142," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(619 - rwno) & ",107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(619 - rwno) & ",61," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    FileSystem.PrintLine(fNum, "QRCODE " & Str(426 - rwno) & ",156,L,5,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(520 - rwno) & ",107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(495 - rwno) & ",107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    rwno = rwno + 314
                    If (j Mod 2) = 0 Then
                        'If rwno > 314 Then
                        FileSystem.PrintLine(fNum, "PRINT 1,1")
                        FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                        rwno = 0
                        If j < Val(dg.Rows(i).Cells(14).Value) Then
                            FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='23.5 mm'></xpml>SIZE 77.6 mm, 45 mm")
                            FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                            FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                            FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                            FileSystem.PrintLine(fNum, "SPEED 7")
                            FileSystem.PrintLine(fNum, "SET PEEL OFF")
                            FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                            FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                            FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='23.5 mm'></xpml>SET TEAR ON")
                            FileSystem.PrintLine(fNum, "CLS")
                            FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                        End If


                    End If



                    If j >= Val(dg.Rows(i).Cells(14).Value) Then
                        rwno = 0
                        If (j Mod 2) <> 0 Then
                            FileSystem.PrintLine(fNum, "PRINT 1,1")
                            FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                        End If
                        'FileSystem.PrintLine(fNum, "PRINT 1,1")
                        'FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                    End If
                Next j

                'FileSystem.PrintLine(fNum, TAB(0), "TEXT 308,182," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                'FileSystem.PrintLine(fNum, TAB(0), "TEXT 308,142," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                'FileSystem.PrintLine(fNum, TAB(0), "TEXT 305,107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                'FileSystem.PrintLine(fNum, TAB(0), "TEXT 308,61," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                'FileSystem.PrintLine(fNum, "QRCODE 110,156,L,5,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                'FileSystem.PrintLine(fNum, TAB(0), "TEXT 215,107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                'FileSystem.PrintLine(fNum, TAB(0), "TEXT 185,107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")



                'FileSystem.PrintLine(fNum, "PRINT 1," & Val(dg.Rows(i).Cells(14).Value))
                'FileSystem.PrintLine(fNum, "PRINT 1,1")
                'FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")












            End If
        Next
        FileSystem.FileClose(fNum)
        'FileClose(1)


        'PrintTextFile(mdir)
        'PrintToTSCPrinter(mdir, Trim(cmbprinter.Text))


        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
            ' Call updateprn()

            If mcitrix = "Y" Then
                'MsgBox("online print" & mdir)
                'citrixprint(mdir)
                citrixprint2(mdir, cmbprinter.SelectedItem.ToString())
            Else
                If MsgBox("Lpt Port", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text) & ":")
                    Shell("rawpr.bat " & mdir)

                Else
                    'Dim builder StringBuilder = new StringBuilder
                    'Dim file As System.IO.StreamWriter
                    'file = My.Computer.FileSystem.OpenTextFileWriter(System.Configuration.ConfigurationSettings.AppSettings("reportpath") + "\test.txt", False, System.Text.Encoding.ASCII)


                    'file.WriteLine(builder.ToString())
                    'file.Close()


                    'Dim text As String = File.ReadAllText(mdir)
                    Dim text As String = File.ReadAllText(mdir, System.Text.Encoding.GetEncoding(1252))
                    Dim pd As PrintDialog = New PrintDialog()
                    pd.PrinterSettings = New PrinterSettings()
                    If Len(Trim(cmbprinter.Text)) > 0 Then
                        BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName = Trim(cmbprinter.Text), text)
                    Else
                        BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName, text)
                    End If

                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                    'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                End If
            End If
        End If
        cmbfit.Text = ""
        'chkliberty.Checked = False


    End Sub


    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub PrintBitmap()
        ' Replace "USB001" with the appropriate port for your TSC printer
        Using serialPort As New SerialPort("USB004", 9600)
            Try
                serialPort.Open()

                ' Assuming your encoded data is stored in a variable called encodedData
                Dim decodedBytes As Byte() = Convert.FromBase64String("àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")

                ' Send the TSC commands to print the bitmap
                ' Dim tscCommands As String = "^XA^FO100,100^GFA,283,312,3," & Convert.ToBase64String(decodedBytes) & "^FS^XZ"

                Dim tscCommands As String = "BITMAP 283,312,3,32,1" & Convert.ToBase64String(decodedBytes) & "^FS^XZ"


                ' Write the commands to the serial port
                serialPort.Write(tscCommands)

                ' Close the port
                serialPort.Close()
            Catch ex As Exception
                ' Handle exceptions
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub


    Private Sub PrintTextFile(ByVal filepath As String)
        ' Create a PrintDialog instance
        Dim printDialog As New PrintDialog()

        ' Set the PrintDocument for the PrintDialog
        printDialog.Document = printDocument

        ' Show the PrintDialog to allow the user to select a printer
        If printDialog.ShowDialog() = DialogResult.OK Then
            ' Set the PrintDocument's PrintController to the standard PrintController
            printDocument.PrintController = New StandardPrintController()

            ' Specify the file path of the text file you want to print
            'Dim filePath As String = "C:\path\to\your\file.txt"

            ' Set the PrintDocument's PrintPage event handler
            AddHandler printDocument.PrintPage, Sub(sender, e)
                                                    ' Read the contents of the text file
                                                    Dim fileContents As String = File.ReadAllText(filepath)

                                                    ' Set the font and position for printing
                                                    Dim font As New Font("Arial", 12)
                                                    Dim position As New PointF(100, 100)

                                                    ' Print the contents of the text file
                                                    e.Graphics.DrawString(fileContents, font, Brushes.Black, position)
                                                End Sub

            ' Print the document
            printDocument.Print()
        End If
    End Sub



    Private Sub PrintToTSCPrinter(ByVal filepath As String, ByVal printername As String)
        Try
            ' Create a PrintDocument instance
            Dim printDocument As New PrintDocument()

            ' Set the printer name to the name of your TSC printer
            printDocument.PrinterSettings.PrinterName = printername

            ' Specify the file path of the text file you want to print
            'Dim filePath As String = "C:\path\to\your\file.txt"

            ' Set the PrintDocument's PrintPage event handler
            AddHandler printDocument.PrintPage, Sub(sender, e)
                                                    ' Read the contents of the text file
                                                    Dim fileContents As String = System.IO.File.ReadAllText(filepath)

                                                    ' Set the font and position for printing
                                                    Dim font As New Font("Arial", 12)
                                                    Dim position As New PointF(100, 100)

                                                    ' Print the contents of the text file
                                                    e.Graphics.DrawString(fileContents, font, Brushes.Black, position)
                                                End Sub

            ' Start the printing process
            printDocument.Print()

            ' MessageBox.Show("Text file sent to the printer successfully.")
        Catch ex As Exception
            ' Handle exceptions
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub printraw()
        'Dim rawtext As String
        Dim pd As New PrintDocument()
        ' Add an event handler to handle the PrintPage event
        AddHandler pd.PrintPage, AddressOf OnPrintPage

        ' Set the printer name (replace "YourPrinterName" with your actual printer name)
        pd.PrinterSettings.PrinterName = mprinter
        ' "YourPrinterName"

        ' Print the document
        pd.Print()
    End Sub
    Sub OnPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        ' Raw text to be printed
        'Dim rawText As String = "This is raw text to be printed."
        Dim rawText As String
        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then

                rawText = "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm" _
                            & "DIRECTION 0,0" _
                            & "REFERENCE 0,0" _
                            & "OFFSET 0 mm" _
                            & "SPEED 10" _
                            & "SET PEEL OFF" _
                            & "SET CUTTER OFF" _
                            & "SET PARTIAL_CUTTER OFF" _
                            & "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON" _
                            & "CLS" _
                            & "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ" _
                            & "CODEPAGE 1252" _
                            & "TEXT 385,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP:" & """" _
                            & "TEXT 277,339," & """" & "0" & """" & ",180,14,12," & """" & Trim(dg.Rows(i).Cells(12).Value) & """" _
                            & "TEXT 538,291," & """" & "0" & """" & ",180,11,12," & """" & Trim(dg.Rows(i).Cells(2).Value) & """" _
                            & "TEXT 538,258," & """" & "0" & """" & ",180,10,9," & """" & "COL:" & """" _
                            & "TEXT 538,230," & """" & "0" & """" & ",180,8,9," & """" & "CONTENT :" & """" _
                            & "TEXT 476,258," & """" & "ROMAN.TTF" & """" & ",180,1,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """" _
                            & "TEXT 430,230," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """" _
                            & "TEXT 182,252," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """" _
                            & "TEXT 103,249," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """" _
                            & "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """" _
                            & "TEXT 116,216," & """" & "0" & """" & ",180,16,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """" _
                            & "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """" _
                            & "TEXT 126,158," & """" & "0" & """" & ",180,12,9," & """" & "Qty :1N" & """" _
                            & "QRCODE 519,199,L,3,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """" _
                            & "TEXT 367,159," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """" _
                            & "TEXT 322,131," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """" _
                            & "TEXT 277,131," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """" _
                            & "TEXT 346,313," & """" & "ROMAN.TTF" & """" & ",180,1,7," & """" & "(Incl.of all Taxes)" & """"
                If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                    rawText = rawText & "TEXT 538,127," & """" & "0" & """" & ",180,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """"
                End If

                rawText = rawText & "PRINT 1," & Val(dg.Rows(i).Cells(14).Value) _
                              & "<xpml></page></xpml><xpml><end/></xpml>"


                ' Create a font for printing
                Using font As New Font("Arial", 12)
                    ' Draw the text on the page
                    e.Graphics.DrawString(rawText, font, Brushes.Black, New PointF(10, 10))
                    'e.Graphics.DrawString(rawText)
                End Using
            End If
        Next

    End Sub

    'Public Class Form1
    '    Private Sub PrintRupeeSymbolToTSCPrinter()
    '        Try
    '            ' Replace "192.168.1.100" and 9100 with the IP address and port of your TSC printer
    '            Using tcpClient As New TcpClient("192.168.1.100", 9100)
    '                Using networkStream As NetworkStream = tcpClient.GetStream()

    '                    ' Send TSPL commands to set font and print the rupee symbol
    '                    Dim tsplCommands As String =
    '                        "TEXT 100,100,'0',180,10,10,'₹'" & vbCrLf

    '                    ' Convert the TSPL commands to bytes using ASCII encoding
    '                    Dim data As Byte() = Encoding.ASCII.GetBytes(tsplCommands)

    '                    ' Send the data to the printer
    '                    networkStream.Write(data, 0, data.Length)

    '                    MessageBox.Show("Rupee symbol sent to the printer successfully.")
    '                End Using
    '            End Using
    '        Catch ex As Exception
    '            ' Handle exceptions
    '            MessageBox.Show("Error: " & ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
    '        ' Call the PrintRupeeSymbolToTSCPrinter function when the form loads
    '        PrintRupeeSymbolToTSCPrinter()
    '    End Sub
    'End Class


    Private Sub PrintLabel()

        Dim tsplCommand As String
        printerIp = "192.166.0.192"
        printerPort = 9100
        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then

                Try
                    ' Construct the TSPL command string
                    'Dim tsplCommand As String
                    'tsplcommand= "^XA^FO100,100^FDHello, World!^FS^XZ"

                    tsplCommand = "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm" _
                                & "DIRECTION 0,0" _
                                & "REFERENCE 0,0" _
                                & "OFFSET 0 mm" _
                                & "SPEED 14" _
                                & "SET PEEL OFF" _
                                & "SET CUTTER OFF" _
                                & "SET PARTIAL_CUTTER OFF" _
                                & "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON" _
                                & "CLS" _
                                & "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ" _
                                & "CODEPAGE 1252" _
                                & "TEXT 385,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP:" & """" _
                                & "TEXT 277,339," & """" & "0" & """" & ",180,14,12," & """" & Trim(dg.Rows(i).Cells(12).Value) & """" _
                                & "TEXT 538,291," & """" & "0" & """" & ",180,14,12," & """" & Trim(dg.Rows(i).Cells(2).Value) & """" _
                                & "TEXT 538,258," & """" & "0" & """" & ",180,10,9," & """" & "COL:" & """" _
                                & "TEXT 538,230," & """" & "0" & """" & ",180,8,9," & """" & "CONTENT :" & """" _
                                & "TEXT 476,258," & """" & "ROMAN.TTF" & """" & ",180,1,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """" _
                                & "TEXT 430,230," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """" _
                                & "TEXT 182,252," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """" _
                                & "TEXT 103,249," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """" _
                                & "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """" _
                                & "TEXT 116,216," & """" & "0" & """" & ",180,16,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """" _
                                & "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """" _
                                & "TEXT 126,158," & """" & "0" & """" & ",180,12,9," & """" & "Qty :1N" & """" _
                                & "QRCODE 519,199,L,3,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """" _
                                & "TEXT 368,159," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """" _
                                & "TEXT 334,131," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """" _
                                & "TEXT 277,131," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """" _
                                & "TEXT 346,313," & """" & "ROMAN.TTF" & """" & ",180,1,7," & """" & "(Incl.of all Taxes)" & """"
                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                        tsplCommand = tsplCommand & "TEXT 538,127," & """" & "0" & """" & ",180,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """"
                    End If

                    tsplCommand = tsplCommand & "PRINT 1," & Val(dg.Rows(i).Cells(14).Value) _
                                  & "<xpml></page></xpml><xpml><end/></xpml>"

                    ' Create a TCP client
                    Using client As New TcpClient(printerIp, printerPort)
                        ' Get the network stream
                        Using stream As NetworkStream = client.GetStream()
                            ' Convert the TSPL command string to bytes
                            Dim dataBytes As Byte() = Encoding.ASCII.GetBytes(tsplCommand)

                            ' Send the TSPL command to the printer
                            stream.Write(dataBytes, 0, dataBytes.Length)
                        End Using
                    End Using

                Catch ex As Exception
                    ' Handle exceptions
                    Console.WriteLine("Error printing label: " & ex.Message)
                End Try
            End If
        Next


        'Dim file As System.IO.StreamWriter
        'file = My.Computer.FileSystem.OpenTextFileWriter(System.Configuration.ConfigurationSettings.AppSettings("reportpath") + "\test.txt", False, System.Text.Encoding.ASCII)


        'file.WriteLine(builder.ToString())
        'file.Close()


    End Sub

    Private Sub Btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnclear.Click
        txtyr.Text = ""
        txtmont.Text = ""
        lbldocentry.Text = ""
        lblcardcode.Text = ""
        lblcardname.Text = ""
        cmbfit.Text = ""
        chkliberty.Checked = False
        OptSamDrf.Checked = False
        dgs.Rows.Clear()
        dg.Rows.Clear()
    End Sub

    Private Sub chksel_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chksel.CheckedChanged
        If chksel.Checked = True Then

            For Each Rw As DataGridViewRow In dg.Rows

                dg.Item(0, Rw.Index).Value = True
                'item(Col.index, Row.index) so you can set value on each cell of the datagrid

            Next

        Else

            For Each Rw As DataGridViewRow In dg.Rows

                dg.Item(0, Rw.Index).Value = False
                'item(Col.index, Row.index) so you can set value on each cell of the datagrid

            Next
        End If
    End Sub

    Private Sub chktwin_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chktwin.CheckedChanged
        If chktwin.Checked = True Then
            chkliberty.Checked = False
        End If
    End Sub

    Private Sub chkliberty_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkliberty.CheckedChanged
        If chkliberty.Checked = True Then
            chktwin.Checked = False
        End If
    End Sub

    Private Sub OptSales_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptSales.CheckedChanged
        txtdocnum.Focus()
    End Sub

    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick


        If e.RowIndex < 0 Then Exit Sub

        'Check if checkbox column clicked
        If TypeOf dg.Columns(e.ColumnIndex) Is DataGridViewCheckBoxColumn Then
            dg.CommitEdit(DataGridViewDataErrorContexts.Commit)

            Dim chk As Boolean = CBool(dg.Rows(e.RowIndex).Cells(0).Value)

            Dim itemCode As String = dg.Rows(e.RowIndex).Cells(18).Value.ToString()
            ' Dim qty As Integer = CInt(dg.Rows(e.RowIndex).Cells("Qty").Value)

            If chk Then
                AddQtyToItem(Dgpk, dgph, itemCode, 1)
                'AddToSelected(itemCode, qty)
            Else

                'AddQtyToItem(Dgpk, itemCode, -1)
            End If

        End If
    End Sub

    'Private Sub dg_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellDoubleClick
    '    If dg.CurrentCell.ColumnIndex = 14 Then
    '        'e.Handled = True
    '        Dim cell As DataGridViewCell = dg.Rows(e.RowIndex).Cells(14)
    '        dg.CurrentCell = cell
    '        dg.BeginEdit(True)
    '    End If
    'End Sub

    Private Sub dg_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellEnter
        If dg.CurrentCell.ColumnIndex = 14 Then
            'e.Handled = True
            Dim cell As DataGridViewCell = dg.Rows(e.RowIndex).Cells(14)
            dg.CurrentCell = cell
            dg.BeginEdit(True)
        End If
    End Sub

    Private Sub dg_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dg.EditingControlShowing
        Dim iCurCol As Integer = dg.CurrentCell.ColumnIndex
        Select Case iCurCol
            Case 14
                'only allow numerics
                If TypeOf e.Control Is TextBox Then
                    Dim tb As TextBox = TryCast(e.Control, TextBox)
                    AddHandler tb.KeyPress, AddressOf dg_KeyPress
                End If
            Case Else

        End Select
    End Sub

    Private Sub dg_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dg.KeyPress
        If e.KeyChar = vbBack Then

        Else
            If Not (Char.IsNumber(e.KeyChar)) Then
                e.Handled = True
            End If
        End If

    End Sub

    Private Sub chkset_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkset.CheckedChanged
        If chkset.Checked = True Then
            Panelset.Visible = True
        Else
            Panelset.Visible = False
        End If
    End Sub

    Private Sub samplebarcode(ByVal docent As Integer)
        msql = "declare @docentry as integer " _
             & " declare @state as nvarchar(30) " _
            & " set @docentry=" & docent _
            & " set @state=(select cc.state from odrf bb inner join crd1 cc on bb.cardcode=cc.cardcode and bb.paytocode=cc.address  where docentry=@docentry group by cc.state) " _
            & " select @state stat, k.u_brandgroup,sum(k.HFfrrate) HFfrate, sum(k.HFmrp) HFmrp, case when isnull(sum(k.hfmrp),0)>0 and isnull(sum(k.hffrrate),0)>0 then  round(sum(k.HFmrp-k.HFfrrate)/sum(k.HFfrrate)*100,0) else 0 end  HFfrMarg, " _
            & " sum(k.FLfrrate) FLfrrate, sum(k.FLmrp) FLmrp,case when isnull(sum(k.flmrp),0)>0 and isnull(sum(k.flfrrate),0)>0 then round(sum(k.FLmrp-k.FLfrrate)/sum(k.FLfrrate)*100,0) else 0 end FLFrMarg,k.fabtype    from ( " _
            & " select it.u_brandgroup,it.u_style,c.pricelist,c.itemcode,case when isnull(it.u_style,'')='HALF' then  c.price else 0 end HFfrrate, " _
            & " case when isnull(it.u_style,'')='FULL' then c.price else  0  end FLfrrate,0 HFmrp,0 FLMrp,it.fabtype from itm1 c  " _
            & " inner join (select u_brandgroup,itemcode,u_style,u_size,isnull(U_Standard,'') Fabtype from oitm where u_size='38' and u_oscode='Warehouse')  it on c.itemcode=it.itemcode " _
            & " right join (select b.cardcode,t.u_brandgroup,cr.listnum,cr.u_mrplistnum from odrf b " _
            & " inner join drf1 d on b.docentry=d.docentry " _
            & " inner join oitm t on d.itemcode=t.itemcode " _
            & " inner join ocrd cr on b.cardcode=cr.cardcode " _
            & " where t.u_size='38' and b.docentry=@docentry) fr on it.u_brandgroup=fr.u_brandgroup and c.pricelist=fr.listnum " _
            & " union all " _
            & " select it.u_brandgroup,it.u_style,c.pricelist,c.itemcode,0 HFfrrate,0 FLFrate, " _
            & " case when isnull(it.u_style,'')='HALF' then  c.price else 0 end HFmrp, " _
            & " case when isnull(it.u_style,'')='FULL' then  c.price else 0 end FLmrp,it.fabtype  from itm1 c " _
            & " inner join (select u_brandgroup,itemcode,u_style,u_size,isnull(U_Standard,'') Fabtype from oitm where u_size='38' and u_oscode='Warehouse')  it on c.itemcode=it.itemcode " _
            & " right join (select b.cardcode,t.u_brandgroup,cr.listnum,cr.u_mrplistnum from odrf b " _
            & " inner join drf1 d on b.docentry=d.docentry " _
            & " inner join oitm t on d.itemcode=t.itemcode " _
            & " inner join ocrd cr on b.cardcode=cr.cardcode " _
            & " where t.u_size='38' and b.docentry=@docentry) fr on it.u_brandgroup=fr.u_brandgroup and c.pricelist=fr.u_mrplistnum) k " _
            & " group by k.u_brandgroup,k.fabtype order by k.u_brandgroup "




        Dim dts As DataTable = getDataTable(msql)
        If dts.Rows.Count > 0 Then
            For Each rw As DataRow In dts.Rows
                n = dgs.Rows.Add

                dgs.Rows(n).Cells(1).Value = rw("u_brandgroup")
                dgs.Rows(n).Cells(1).ReadOnly = True
                dgs.Rows(n).Cells(2).Value = 1
                dgs.Rows(n).Cells(3).Value = rw("HFfrate")
                dgs.Rows(n).Cells(3).ReadOnly = True
                dgs.Rows(n).Cells(4).Value = rw("HFMRP")
                dgs.Rows(n).Cells(4).ReadOnly = True
                dgs.Rows(n).Cells(5).Value = rw("HFfrMarg")
                dgs.Rows(n).Cells(5).ReadOnly = True
                dgs.Rows(n).Cells(6).Value = rw("FLfrrate")
                dgs.Rows(n).Cells(6).ReadOnly = True
                dgs.Rows(n).Cells(7).Value = rw("FLmrp")
                dgs.Rows(n).Cells(7).ReadOnly = True
                dgs.Rows(n).Cells(8).Value = rw("FLFrMarg")
                dgs.Rows(n).Cells(8).ReadOnly = True
                dgs.Rows(n).Cells(9).Value = rw("fabtype")
                dgs.Rows(n).Cells(9).ReadOnly = True
                dgs.Rows(n).Cells(10).Value = rw("stat")
                dgs.Rows(n).Cells(10).ReadOnly = True

            Next
        Else
            'txtmont.Text = 0
        End If

    End Sub

    Private Sub OptSamDrf_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptSamDrf.CheckedChanged
        If OptSamDrf.Checked = True Then
            If dgs.Visible = False Then dgs.Visible = True
        Else
            If dgs.Visible = True Then dgs.Visible = False
        End If
    End Sub

    Private Sub sampleprint()
        Dim dir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "sampbarcodE.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
        'If chkprndir.Checked = True Then
        '    FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        'Else


        Dim fNum As Integer = FileSystem.FreeFile()
        FileSystem.FileOpen(fNum, mdir, OpenMode.Output, OpenAccess.Write, OpenShare.Shared, -1)
        'FileSystem.PrintLine(fNum, "This is a line with the Rupee symbol: ₹")
        'FileSystem.FileClose(fNum)



        'FileOpen(1, mdir, OpenMode.Output)
        'End If
        Dim rupeeSymbol As String = ChrW(&H20B9)
        'Dim rupeeSymbol As String = ChrW(&H20B9)
        For i As Integer = 0 To dgs.Rows.Count - 1
            Dim c As Boolean
            c = dgs.Rows(i).Cells(0).Value
            If c = True Then
                'PrintLine(1, TAB(0), DR.Item("firstdet"))

                FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum, "SPEED 7")
                FileSystem.PrintLine(fNum, "SET PEEL OFF")
                FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
                FileSystem.PrintLine(fNum, "CLS")
                'FileSystem.PrintLine(fNum, "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                '"₹"
                FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                'FileSystem.PrintLine(fNum, TAB(0), "TEXT 433,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP Rs." & """")

                'FileSystem.PrintLine(fNum, "TEXT 381,326," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 520,326," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dgs.Rows(i).Cells(1).Value) & """")
                If Val(dgs.Rows(i).Cells(3).Value) > 0 Then
                    'FileSystem.PrintLine(fNum, "TEXT 520,326," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dgs.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 514,230," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "HALF" & """")
                    FileSystem.PrintLine(fNum, "TEXT 410,230," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Microsoft.VisualBasic.Format(Val(dgs.Rows(i).Cells(3).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 272,230," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Microsoft.VisualBasic.Format(Val(dgs.Rows(i).Cells(4).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 111,230," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Microsoft.VisualBasic.Format(Val(dgs.Rows(i).Cells(5).Value), "######.00") & "%" & """")

                End If
                If Val(dgs.Rows(i).Cells(6).Value) > 0 Then
                    FileSystem.PrintLine(fNum, "TEXT 512,183," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "FULL" & """")
                    ' FileSystem.PrintLine(fNum, "TEXT 520,326," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dgs.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 409,183," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Microsoft.VisualBasic.Format(Val(dgs.Rows(i).Cells(6).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 269,183," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Microsoft.VisualBasic.Format(Val(dgs.Rows(i).Cells(7).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 111,183," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Microsoft.VisualBasic.Format(Val(dgs.Rows(i).Cells(8).Value), "######.00") & "%" & """")
                End If


                FileSystem.PrintLine(fNum, "TEXT 410,135," & """" & "ROMAN.TTF" & """" & ",180,1,10," & """" & "Fab.Type : " & Trim(dgs.Rows(i).Cells(9).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 520,274," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "STYLE" & """")
                FileSystem.PrintLine(fNum, "TEXT 379,274," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "FR" & """")
                FileSystem.PrintLine(fNum, "TEXT 249,274," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "MRP" & """")
                FileSystem.PrintLine(fNum, "TEXT 140,274," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "MARGIN" & """")
                FileSystem.PrintLine(fNum, "TEXT 528,121," & """" & "ROMAN.TTF" & """" & ",180,1,6," & """" & Trim(dgs.Rows(i).Cells(10).Value) & """")

                FileSystem.PrintLine(fNum, "PRINT 1," & Val(dgs.Rows(i).Cells(2).Value))
                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")

            End If
        Next
        FileSystem.FileClose(fNum)


        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            If MsgBox("Lpt Port", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Shell("rawpr.bat " & mdir)
            Else
                'Dim text As String = File.ReadAllText(mdir)
                Dim text As String = File.ReadAllText(mdir, System.Text.Encoding.GetEncoding(1252))
                Dim pd As PrintDialog = New PrintDialog()
                pd.PrinterSettings = New PrinterSettings()
                If Len(Trim(cmbprinter.Text)) > 0 Then
                    BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName = Trim(cmbprinter.Text), text)
                Else
                    BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName, text)
                End If

            End If
        End If

        cmbfit.Text = ""
    End Sub

    Private Sub txtno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtno.TextChanged
        If OptSamDrf.Checked = True Then
            For i As Integer = 0 To dgs.Rows.Count - 1
                dgs.Rows(i).Cells(2).Value = Val(txtno.Text)
            Next
        End If
    End Sub

    '*********************New using client ip based print
    Private Sub speedprintip(ByVal prnip As String, ByVal prnport As String)
        Dim dir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"
        Dim networkPrinterPath As String = "\\ComputerName\SharedPrinterName"  ' Update with actual network path

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "nsbarcodE.txt"



        Dim fNum As Integer = FileSystem.FreeFile()
        FileSystem.FileOpen(fNum, mdir, OpenMode.Output, OpenAccess.Write, OpenShare.Shared, -1)

        Dim rupeeSymbol As String = ChrW(&H20B9)
        'Dim rupeeSymbol As String = ChrW(&H20B9)
        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then
                'PrintLine(1, TAB(0), DR.Item("firstdet"))
                mbarstr = "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm" _
                          & "DIRECTION 0,0" _
                          & "REFERENCE 0,0" _
                          & "OFFSET 0 mm" _
                          & "SPEED 7" _
                          & "SET PEEL OFF" _
                          & "SET CUTTER OFF" _
                          & "SET PARTIAL_CUTTER OFF" _
                          & "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON" _
                          & "CLS" _
                          & "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ" _
                          & "CODEPAGE 1252" _
                          & "TEXT 433,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP Rs." & """" _
                          & "TEXT 277,339," & """" & "0" & """" & ",180,14,12," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """" _
                          & "TEXT 538,291," & """" & "0" & """" & ",180,11,12," & """" & Trim(dg.Rows(i).Cells(2).Value) & """" _
                          & "TEXT 538,258," & """" & "0" & """" & ",180,9,9," & """" & "COLOUR :" & """" _
                          & "TEXT 538,230," & """" & "0" & """" & ",180,8,9," & """" & "COMMODITY :" & """" _
                          & "TEXT 439,258," & """" & "ROMAN.TTF" & """" & ",180,1,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """" _
                          & "TEXT 367,189," & """" & "0" & """" & ",180,10,8," & """" & "Made in India" & """" _
                          & "TEXT 182,252," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """"

                If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                    'Text(109, 249, "0", 180, 11, 10, "Length")
                    mbarstr = mbarstr & "TEXT 109,249," & """" & "0" & """" & ",180,11,10," & """" & "Length" & """" _
                                      & "TEXT 182,211," & """" & "0" & """" & ",180,9,17," & """" & "Code:" & """" _
                                      & "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """" _
                                      & "TEXT 63,198," & """" & "0" & """" & ",180,7,10," & """" & "(Inch)" & """"
                Else
                    mbarstr = mbarstr & "TEXT 103,249," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """" _
                                      & "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """" _
                                      & "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """" _
                                      & "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """"
                End If

                If chktwin.Checked = True Then
                    mbarstr = mbarstr & "TEXT 182,160," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :2N" & """"
                ElseIf chkset.Checked = True Then
                    mbarstr = mbarstr & "TEXT 182,160," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :" & Val(txtno.Text) & "N" & """"
                Else
                    mbarstr = mbarstr & "TEXT 182,160," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1N" & """"
                End If

                mbarstr = mbarstr & "QRCODE 519,199,L,3,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """" _
                                  & "TEXT 367,159," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """" _
                                  & "TEXT 322,131," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """" _
                                  & "TEXT 277,131," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """" _
                                  & "TEXT 346,313," & """" & "ROMAN.TTF" & """" & ",180,1,7," & """" & "(Incl.of all Taxes)" & """"
                If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                    mbarstr = mbarstr & "TEXT 538,133," & """" & "0" & """" & ",180,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """"
                End If
                If chktwin.Checked = True Then
                    mbarstr = mbarstr & "TEXT 182,291," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "(1+1) 2N" & """"
                End If

                If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                    mbarstr = mbarstr & "TEXT 120,279," & """" & "0" & """" & ",180,12,9," & """" & (Trim(dg.Rows(i).Cells(15).Value) & " cm") & """" _
                                      & "TEXT 182,283," & """" & "0" & """" & ",180,11,11," & """" & "Size:" & """"
                End If
                If Chkshirt.Checked = True Then
                    mbarstr = mbarstr & "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & Trim(dg.Rows(i).Cells(16).Value) & """" _
                                      & "TEXT 182,130," & """" & "0" & """" & ",180,8,9," & """" & Trim(dg.Rows(i).Cells(17).Value) & """"
                End If

                mbarstr = mbarstr & "PRINT 1," & Val(dg.Rows(i).Cells(14).Value) _
                                  & "<xpml></page></xpml><xpml><end/></xpml>"






            End If
        Next

        'If Not String.IsNullOrEmpty(printerIp) Then
        '    Try
        '        Using client As New TcpClient(prnip, prnport)
        '            Dim stream As NetworkStream = client.GetStream()
        '            Dim commandBytes As Byte() = Encoding.ASCII.GetBytes(mbarstr)

        '            ' Send the ZPL command
        '            stream.Write(commandBytes, 0, commandBytes.Length)
        '            stream.Flush()
        '            MessageBox.Show("Print command sent to printer at ")
        '        End Using
        '    Catch ex As Exception
        '        MessageBox.Show("Error sending print command: " & ex.Message)
        '    End Try
        'Else
        '    MessageBox.Show("No printer found for location: ")
        'End If



        Try
            ' Create a TCP socket to connect to the printer
            Using clientSocket As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                ' Connect to the network printer
                clientSocket.Connect(prnip, prnport)

                ' Convert the TSPL/ZPL command to a byte array
                Dim commandBytes As Byte() = Encoding.ASCII.GetBytes(mbarstr)

                ' Send the command to the printer
                clientSocket.Send(commandBytes)

                Console.WriteLine("Barcode printed successfully.")
            End Using
        Catch ex As Exception
            Console.WriteLine("Error printing barcode: " & ex.Message)
        End Try


        '***Network path

        'Dim printDoc As New PrintDocument()
        'printDoc.PrinterSettings.PrinterName = networkPrinterPath

        '' Event handler for printing
        'AddHandler printDoc.PrintPage, Sub(sender, e)
        '                                   Dim font As New Font("Arial", 10)
        '                                   e.Graphics.DrawString(mbarstr, font, Brushes.Black, 10, 10)
        '                               End Sub

        '' Print the document
        'Try
        '    If printDoc.PrinterSettings.IsValid Then
        '        printDoc.Print()
        '        Console.WriteLine("Barcode printed successfully.")
        '    Else
        '        Console.WriteLine("Printer not found or invalid.")
        '    End If
        'Catch ex As Exception
        '    Console.WriteLine("Error printing barcode: " & ex.Message)
        'End Try



        'If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '    'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
        '    ' Call updateprn()
        '    If MsgBox("Lpt Port", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

        '        'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text) & ":")
        '        Shell("rawpr.bat " & mdir)

        '    Else
        '        'Dim builder StringBuilder = new StringBuilder
        '        'Dim file As System.IO.StreamWriter
        '        'file = My.Computer.FileSystem.OpenTextFileWriter(System.Configuration.ConfigurationSettings.AppSettings("reportpath") + "\test.txt", False, System.Text.Encoding.ASCII)


        '        'file.WriteLine(builder.ToString())
        '        'file.Close()


        '        'Dim text As String = File.ReadAllText(mdir)
        '        Dim text As String = File.ReadAllText(mdir, System.Text.Encoding.GetEncoding(1252))
        '        Dim pd As PrintDialog = New PrintDialog()
        '        pd.PrinterSettings = New PrinterSettings()
        '        If Len(Trim(cmbprinter.Text)) > 0 Then
        '            BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName = Trim(cmbprinter.Text), text)
        '        Else
        '            BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName, text)
        '        End If

        '        'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
        '        'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
        '        'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
        '    End If
        'End If

        cmbfit.Text = ""
        'chkliberty.Checked = False


    End Sub



    Private Sub speedprint2vert()
        Dim dir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "nsbarcodEV.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
        'If chkprndir.Checked = True Then
        '    FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        'Else


        Dim fNum As Integer = FileSystem.FreeFile()
        FileSystem.FileOpen(fNum, mdir, OpenMode.Output, OpenAccess.Write, OpenShare.Shared, -1)
        'FileSystem.PrintLine(fNum, "This is a line with the Rupee symbol: ₹")
        'FileSystem.FileClose(fNum)



        'FileOpen(1, mdir, OpenMode.Output)
        'End If
        Dim rupeeSymbol As String = ChrW(&H20B9)
        'Dim rupeeSymbol As String = ChrW(&H20B9)



        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then
                'PrintLine(1, TAB(0), DR.Item("firstdet"))
                If Chkshirt.Checked = True Then
                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum, "SPEED 7")
                    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
                    FileSystem.PrintLine(fNum, "CLS")
                    FileSystem.PrintLine(fNum, "BITMAP 520,161,4,24,1,ÿÿÜÿÿÇÌÿ‡ÌþÌüÌøÌð#ÌàsÈÀñÀð ø ü þ ?ÿŒÿÌÿÿÌÿÿÌÿÿïÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")


                    'PrintLine(1, TAB(0), "TEXT BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    '"₹"
                    'PrintBitmap()
                    FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum, "TEXT 543,193," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 493,13," & """" & "0" & """" & ",90,8,11," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :9,10"
                    FileSystem.PrintLine(fNum, "TEXT 462,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 430,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 460,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    'FileSystem.PrintLine(fNum, "TEXT 300,8," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum, "TEXT 425,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 300,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum, "TEXT 397,13," & """" & "ROMAN.TTF" & """" & ",90,1,10," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 397,70," & """" & "0" & """" & ",90,9,10," & """" & "SLEEVE" & """")

                    FileSystem.PrintLine(fNum, "TEXT 398,193," & """" & "0" & """" & ",90,11,20," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 398,260," & """" & "0" & """" & ",90,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 400,310," & """" & "0" & """" & ",90,12,21," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum, "TEXT 330,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :1 N" & """")
                    FileSystem.PrintLine(fNum, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 295,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 330,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 330,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 517,90," & """" & "ROMAN.TTF" & """" & ",90,1,7," & """" & "(Incl.of all Taxes)" & """")

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                        FileSystem.PrintLine(fNum, "TEXT 240,12," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    If chkvertfit.Checked = True Then
                        'fit
                        FileSystem.PrintLine(fNum, "TEXT 270,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(16).Value) & """")
                        'cut
                        FileSystem.PrintLine(fNum, "TEXT 360,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(17).Value) & """")
                    End If


                    '***
                    'FileSystem.PrintLine(fNum, "TEXT  523,281," & """" & "0" & """" & ",180,7,9," & """" & "Commodity Name:" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 300,8," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")


                    'FileSystem.PrintLine(fNum, "TEXT 524,67," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 524,40," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")

                    'FileSystem.PrintLine(fNum, "TEXT 149,245," & """" & "0" & """" & ",180,9,9," & """" & "Net Qty :" & """")
                    ''***
                    'FileSystem.PrintLine(fNum, "TEXT 270,8," & """" & "0" & """" & ",90,9,10," & """" & "SMART FIT" & """")
                    'If chkliberty.Checked = True Then
                    '    FileSystem.PrintLine(fNum, "TEXT 360,8," & """" & "0" & """" & ",90,9,10," & """" & "LIBERTY CUT" & """")
                    'End If


                ElseIf chkpant.Checked = True Then

                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum, "SPEED 7")
                    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
                    FileSystem.PrintLine(fNum, "CLS")
                    'FileSystem.PrintLine(fNum, "BITMAP 369,192,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    FileSystem.PrintLine(fNum, "BITMAP 520,161,4,24,1,ÿÿÜÿÿÇÌÿ‡ÌþÌüÌøÌð#ÌàsÈÀñÀð ø ü þ ?ÿŒÿÌÿÿÌÿÿÌÿÿïÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")

                    'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    '"₹"
                    'PrintBitmap()
                    FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum, "TEXT 543,191," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 491,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :"
                    FileSystem.PrintLine(fNum, "TEXT 458,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 425,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 458,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 425,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 280,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum, "TEXT 395,13," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 395,75," & """" & "0" & """" & ",90,8,12," & """" & "LENGTH" & """")
                    FileSystem.PrintLine(fNum, "TEXT 360,13," & """" & "0" & """" & ",90,11,20," & """" & "Code :" & """")


                    'FileSystem.PrintLine(fNum, "TEXT 183,312," & """" & "0" & """" & ",180,8,9," & """" & "Size: Waist" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 360,80S," & """" & "0" & """" & ",90,18,12," & """" & Trim(Math.Round(Val(dg.Rows(i).Cells(4).Value) * 2.54, 0)) & """")
                    'FileSystem.PrintLine(fNum, "TEXT 49,312," & """" & "0" & """" & ",180,8,10," & """" & "cm" & """")

                    FileSystem.PrintLine(fNum, "TEXT 350,95," & """" & "0" & """" & ",90,18,12," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 365,150," & """" & "0" & """" & ",90,12,21," & """" & "(Inch)" & """")
                    FileSystem.PrintLine(fNum, "TEXT 305,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :1N" & """")
                    FileSystem.PrintLine(fNum, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 275,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 306,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 306,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 517,90," & """" & "ROMAN.TTF" & """" & ",90,1,7," & """" & "(Incl.of all Taxes)" & """")

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                        FileSystem.PrintLine(fNum, "TEXT 249,13," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum, "TEXT 390,240," & """" & "0" & """" & ",90,12,9," & """" & Trim(Math.Round(Val(dg.Rows(i).Cells(4).Value) * 2.54, 0)) & "cm" & """")
                    FileSystem.PrintLine(fNum, "TEXT 395,175," & """" & "0" & """" & ",90,11,11," & """" & "Size:" & """")

                    'FileSystem.PrintLine(fNum, "TEXT  390,240," & """" & "0" & """" & ",90,12,9," & """" & "Commodity Name:" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 527,147," & """" & "0" & """" & ",180,10,9," & """" & "Made in India" & """")

                    'FileSystem.PrintLine(fNum, "TEXT 524,67," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 524,40," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 149,245," & """" & "0" & """" & ",180,10,9," & """" & "Net Qty :" & """")

                    'FileSystem.PrintLine(fNum, "TEXT 85,311," & """" & "0" & """" & ",180,8,10," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    'FileSystem.PrintLine(fNum, "TEXT 130,153," & """" & "0" & """" & ",180,8,10," & """" & "Code :" & """")




                ElseIf chktwin.Checked = True Then
                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum, "SPEED 7")
                    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
                    FileSystem.PrintLine(fNum, "CLS")
                    ''FileSystem.PrintLine(fNum, "BITMAP 322,162,9,56,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    ''PrintLine(1, TAB(0), "TEXT BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'FileSystem.PrintLine(fNum, "BITMAP 370,190,3,32,1,ðÿø?ÿüÿþÿÿ ÿÿƒÿÿÁÿÿàÿÿðÿø?ÿüÿøÿ€ÿ ÿüÿü?ÿøÿøÿ€   ø?ÿüÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'FileSystem.PrintLine(fNum, "BITMAP 323,163,2,24,1,áÿðÿøü?þ?ÿÿÿÇÿü?øÿñÿÀ  ñÿø€  ÿÿÿÿÿÿÿÿÿÿÿÿ")

                    PrintLine(1, TAB(0), "BITMAP 520,161,4,24,1,ÿÿÜÿÿÇÌÿ‡ÌþÌüÌøÌð#ÌàsÈÀñÀð ø ü þ ?ÿŒÿÌÿÿÌÿÿÌÿÿïÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")


                    'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    '"₹"
                    'PrintBitmap()
                    FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum, "TEXT 543,193," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 491,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :"
                    FileSystem.PrintLine(fNum, "TEXT 459,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 430,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 455,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 427,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 270,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum, "TEXT 397,13," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 397,75," & """" & "0" & """" & ",90,9,11," & """" & "SLEEVE" & """")

                    FileSystem.PrintLine(fNum, "TEXT 360,13," & """" & "0" & """" & ",90,11,20," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 360,75," & """" & "0" & """" & ",90,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 360,130," & """" & "0" & """" & ",90,12,21," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum, "TEXT 300,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :2N" & """")
                    FileSystem.PrintLine(fNum, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 270,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 300,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 300,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 517,90," & """" & "ROMAN.TTF" & """" & ",90,1,9," & """" & "(Incl.of all Taxes)" & """")

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                        FileSystem.PrintLine(fNum, "TEXT 240,13," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If
                    FileSystem.PrintLine(fNum, "TEXT  493,220," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & "(1+1) 2N" & """")

                    'FileSystem.PrintLine(fNum, "TEXT  528,282," & """" & "0" & """" & ",180,8,9," & """" & "Commodity Name:" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 338,148," & """" & "0" & """" & ",180,10,9," & """" & "Made in India" & """")

                    'FileSystem.PrintLine(fNum, "TEXT 524,67," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 524,40," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 528,181," & """" & "0" & """" & ",180,9,8," & """" & "Unit Sale Price: Rs." & """")

                    'FileSystem.PrintLine(fNum, "TEXT 319,180," & """" & "0" & """" & ",180,11,8," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value) / 2, "######.00") & """")
                    'FileSystem.PrintLine(fNum, "TEXT 229,180," & """" & "0" & """" & ",180,6,7," & """" & "(Incl.of all Taxes)" & """")

                    'FileSystem.PrintLine(fNum, "TEXT 168,253," & """" & "0" & """" & ",180,10,9," & """" & "Net Qty :" & """")




                ElseIf chkset.Checked = True Then

                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum, "SPEED 7")
                    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
                    FileSystem.PrintLine(fNum, "CLS")
                    'FileSystem.PrintLine(fNum, "BITMAP 366,96,4,40,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'FileSystem.PrintLine(fNum, "BITMAP 214,152,21,24,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")

                    FileSystem.PrintLine(fNum, "BITMAP 520,161,4,24,1,ÿÿÜÿÿÇÌÿ‡ÌþÌüÌøÌð#ÌàsÈÀñÀð ø ü þ ?ÿŒÿÌÿÿÌÿÿÌÿÿïÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")


                    FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum, "TEXT 543,193," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 490,13," & """" & "0" & """" & ",90,8,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :"
                    FileSystem.PrintLine(fNum, "TEXT 460,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 430,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 458,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 425,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 270,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum, "TEXT 395,13," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 394,75," & """" & "0" & """" & ",90,9,10," & """" & "SLEEVE" & """")
                    FileSystem.PrintLine(fNum, "TEXT 360,13," & """" & "0" & """" & ",90,11,20," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum, "TEXT  360,75," & """" & "0" & """" & ",90,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 360,130," & """" & "0" & """" & ",90,12,21," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum, "TEXT 300,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :2N" & """")
                    FileSystem.PrintLine(fNum, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 270,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 300,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 300,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 517,90," & """" & "ROMAN.TTF" & """" & ",90,1,7," & """" & "(Incl.of all Taxes)" & """")

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                        FileSystem.PrintLine(fNum, "TEXT 240,13," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & "" & """")
                    FileSystem.PrintLine(fNum, "TEXT 182,130," & """" & "0" & """" & ",180,8,9," & """" & "" & """")



                    'FileSystem.PrintLine(fNum, "TEXT 528,316," & """" & "0" & """" & ",180,7,8," & """" & "Commodity Name:" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 134,188," & """" & "0" & """" & ",180,8,7," & """" & "Made in India" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 524,61," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 524,34," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 528,172," & """" & "0" & """" & ",180,6,8," & """" & "Unit Sale Price: Rs." & """")
                    'FileSystem.PrintLine(fNum, "TEXT 364,171," & """" & "0" & """" & ",180,9,8," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value) / 2, "######.00") & """")
                    'FileSystem.PrintLine(fNum, "TEXT 411,147," & """" & "0" & """" & ",180,6,6," & """" & "(Incl.of all Taxes)" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 527,239," & """" & "0" & """" & ",180,9,7," & """" & "Net Qty :" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 387,318," & """" & "0" & """" & ",180,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    ''*****

                    'FileSystem.PrintLine(fNum, "TEXT  259,286," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    'FileSystem.PrintLine(fNum, "TEXT 259,194," & """" & "0" & """" & ",180,8,7," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    'FileSystem.PrintLine(fNum, "TEXT 212,194," & """" & "0" & """" & ",180,6,7," & """" & "SLEEVE" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 527,261," & """" & "0" & """" & ",180,11,7," & """" & "Size:" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 224,264," & """" & "0" & """" & ",180,7,9," & """" & "cm" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 259,218," & """" & "0" & """" & ",180,8,7," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    'FileSystem.PrintLine(fNum, "TEXT 528,194," & """" & "0" & """" & ",180,11,7," & """" & "Style:" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 209,170," & """" & "0" & """" & ",180,9,8," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value) / 2, "######.00") & """")
                    'FileSystem.PrintLine(fNum, "TEXT 258,171," & """" & "0" & """" & ",180,8,8," & """" & "Rs." & """")
                    'FileSystem.PrintLine(fNum, "TEXT 259,147," & """" & "0" & """" & ",180,6,6," & """" & "(Incl.of all Taxes)" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 259,238," & """" & "0" & """" & ",180,8,7," & """" & "2 N" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 259,238," & """" & "0" & """" & ",180,8,8," & """" & "Rs." & """")

                Else
                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='41.0 mm'></xpml>SIZE 67.5 mm, 41 mm")
                    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum, "SPEED 7")
                    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='41.0 mm'></xpml>SET TEAR ON")
                    FileSystem.PrintLine(fNum, "CLS")
                    FileSystem.PrintLine(fNum, "BITMAP 371,192,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'PrintLine(1, TAB(0), "TEXT BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    '"₹"
                    'PrintBitmap()
                    FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT 528,218," & """" & "0" & """" & ",180,13,12," & """" & "MRP Rs." & """")
                    'FileSystem.PrintLine(fNum, TAB(0), "TEXT 385,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP:" & """")
                    'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "IndianRupee.TTF" & """" & ",180,32,1," & """" & "`" & """")

                    'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "ITF Rupee.TTF" & """" & ",180,32,1," & """" & "S" & """")
                    'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & "₹" & """")

                    'FileSystem.PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & rupeeSymbol & """")

                    FileSystem.PrintLine(fNum, "TEXT 366,218," & """" & "0" & """" & ",180,14,12," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum, "TEXT 390,280," & """" & "0" & """" & ",180,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :"
                    FileSystem.PrintLine(fNum, "TEXT 530,250," & """" & "0" & """" & ",180,10,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 528,313," & """" & "0" & """" & ",180,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum, "TEXT 447,250," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 407,313," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 170,188," & """" & "0" & """" & ",180,11,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 93,185," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")

                    FileSystem.PrintLine(fNum, "TEXT 155,317," & """" & "0" & """" & ",180,9,11," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 99,316," & """" & "0" & """" & ",180,18,11," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 49,317," & """" & "0" & """" & ",180,9,12," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum, "TEXT 58,244," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1 N" & """")
                    FileSystem.PrintLine(fNum, "QRCODE 121,115,L,5,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 528,178," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 524,115," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 465,110," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 215,212," & """" & "ROMAN.TTF" & """" & ",180,8,7," & """" & "(Incl.of all Taxes)" & """")

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                        FileSystem.PrintLine(fNum, "TEXT 373,108," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum, "TEXT  523,281," & """" & "0" & """" & ",180,7,9," & """" & "Commodity Name:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 527,148," & """" & "0" & """" & ",180,10,9," & """" & "Made in India" & """")

                    FileSystem.PrintLine(fNum, "TEXT 524,67," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    FileSystem.PrintLine(fNum, "TEXT 524,40," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")
                    FileSystem.PrintLine(fNum, "TEXT 149,245," & """" & "0" & """" & ",180,9,9," & """" & "Net Qty :" & """")


                    If chkliberty.Checked = True Then
                        FileSystem.PrintLine(fNum, "TEXT 170,152," & """" & "0" & """" & ",180,10,9," & """" & "LIBERTY CUT" & """")
                    End If



                    FileSystem.PrintLine(fNum, "TEXT 538,230," & """" & "0" & """" & ",180,8,9," & """" & "CONTENT :" & """")
                    'TEXT 439,258,"ROMAN.TTF",180,1,9,"SL18-CYAN"
                    FileSystem.PrintLine(fNum, "TEXT 439,258," & """" & "ROMAN.TTF" & """" & ",180,1,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 430,230," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 182,252," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                        'Text(109, 249, "0", 180, 11, 10, "Length")
                        FileSystem.PrintLine(fNum, "TEXT 109,249," & """" & "0" & """" & ",180,11,10," & """" & "Length" & """")
                        'Text(182, 211, "0", 180, 9, 17, "Code:")
                        FileSystem.PrintLine(fNum, "TEXT 182,211," & """" & "0" & """" & ",180,9,17," & """" & "Code:" & """")
                        FileSystem.PrintLine(fNum, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                        'TEXT 63,198,"0",180,7,10,"(Inch)"
                        FileSystem.PrintLine(fNum, "TEXT 63,198," & """" & "0" & """" & ",180,7,10," & """" & "(Inch)" & """")
                    Else
                        FileSystem.PrintLine(fNum, "TEXT 103,249," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")
                        FileSystem.PrintLine(fNum, "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """")
                        FileSystem.PrintLine(fNum, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                        FileSystem.PrintLine(fNum, "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """")
                    End If


                    If chktwin.Checked = True Then
                        FileSystem.PrintLine(fNum, "TEXT 172,158," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :2N" & """")
                    ElseIf chkset.Checked = True Then
                        FileSystem.PrintLine(fNum, "TEXT 172,158," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :" & Val(txtno.Text) & "N" & """")
                    Else
                        FileSystem.PrintLine(fNum, "TEXT 172,158," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1N" & """")
                    End If

                    FileSystem.PrintLine(fNum, "QRCODE 519,199,L,3,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 367,159," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 322,131," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 277,131," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 346,313," & """" & "ROMAN.TTF" & """" & ",180,1,7," & """" & "(Incl.of all Taxes)" & """")
                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                        FileSystem.PrintLine(fNum, "TEXT 538,133," & """" & "0" & """" & ",180,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If
                    If chktwin.Checked = True Then
                        FileSystem.PrintLine(fNum, "TEXT 182,291," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "(1+1) 2N" & """")
                    End If
                    If chkliberty.Checked = True Then
                        FileSystem.PrintLine(fNum, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & "LIBERTY CUT" & """")
                    End If


                    If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                        FileSystem.PrintLine(fNum, "TEXT 120,279," & """" & "0" & """" & ",180,12,9," & """" & (Trim(dg.Rows(i).Cells(15).Value) & " cm") & """")
                        FileSystem.PrintLine(fNum, "TEXT 182,283," & """" & "0" & """" & ",180,11,11," & """" & "Size:" & """")
                        'FileSystem.PrintLine(fNum, "TEXT 63,279," & """" & "0" & """" & ",180,10,9," & """" & "cm" & """")
                    End If
                End If



                'FileSystem.PrintLine(fNum, "PRINT 1," & Val(dg.Rows(i).Cells(14).Value))
                FileSystem.PrintLine(fNum, "PRINT 1,1")
                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")

                dg.Rows(i).Cells(0).Value = False

            End If
        Next
        FileSystem.FileClose(fNum)
        'FileClose(1)


        'PrintTextFile(mdir)
        'PrintToTSCPrinter(mdir, Trim(cmbprinter.Text))


        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            If mcitrix = "Y" Then

                'citrixprint(mdir)
                citrixprint2(mdir, cmbprinter.SelectedItem.ToString())

            Else
                If MsgBox("Lpt Port", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text) & ":")
                    Shell("rawprv.bat " & mdir)

                Else

                    'Dim text As String = File.ReadAllText(mdir)
                    Dim text As String = File.ReadAllText(mdir, System.Text.Encoding.GetEncoding(1252))
                    Dim pd As PrintDialog = New PrintDialog()
                    pd.PrinterSettings = New PrinterSettings()
                    If Len(Trim(cmbprinter.Text)) > 0 Then
                        BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName = Trim(cmbprinter.Text), text)
                    Else
                        BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName, text)
                    End If

                End If
            End If
        End If


    End Sub

    Private Sub Btnstart_Click(sender As Object, e As EventArgs) Handles Btnstart.Click
        'txtscanner.Text = ""

        'ShowCameraUnderTextbox()

        ''scanner = New QRScanner800(Txtscan, Piccamera)
        'scanner = New QRScanner800N(cameraForm.picCamera, Sub(qr)
        '                                                      ' This is called on QR scan
        '                                                      Invoke(Sub()
        '                                                                 txtscanner.Text = qr
        '                                                                 'lblStatus.Text = "QR Scanned: " & qr
        '                                                             End Sub)
        '                                                  End Sub)
        'scanner.StartCamera()
        'isCameraActive = True
        ''SendKeys.Send("{ENTER}")
        'Btnstart.Enabled = False
        'Btnstop.Enabled = True

        '**method
        'txtscanner.Text = ""
        'ShowCameraUnderTextbox()
        'Btnstart.Enabled = False
        'Btnstop.Enabled = True


        txtscanner.Text = ""
        'ShowOrReuseCamera()
        StartCameraDelayed()
        Btnstart.Enabled = False
        Btnstop.Enabled = True

        'StartCameraDelayed()

    End Sub

    Private Sub Btnstop_Click(sender As Object, e As EventArgs) Handles Btnstop.Click
        'scanner.StopCamera()
        'Btnstart.Enabled = True
        'Btnstop.Enabled = False
        ''txtscanner.Text = ""
        'CloseCameraForm()
        'Btnstart.Enabled = True
        'Btnstop.Enabled = False

        StopCamera()
        If Btnstart.Enabled = False Then Btnstart.Enabled = True
        If Btnstop.Enabled = True Then Btnstop.Enabled = False
    End Sub

    Private Sub Txtitcode_TextChanged(sender As Object, e As EventArgs) Handles Txtitcode.TextChanged

    End Sub

    Private Sub Chksr_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles Chksr.CheckedChanged

    End Sub


    Private Sub LoadAllPrinters()
        Try
            cmbprinter.Items.Clear()
            cmbvprinter.Items.Clear()

            For Each printerName As String In PrinterSettings.InstalledPrinters
                cmbprinter.Items.Add(printerName)
                cmbvprinter.Items.Add(printerName)
            Next

            ' Select default printer if present
            Dim defaultPrinter = New PrinterSettings().PrinterName
            If cmbprinter.Items.Contains(defaultPrinter) Then
                cmbprinter.SelectedItem = defaultPrinter
                cmbvprinter.SelectedItem = defaultPrinter
            ElseIf cmbprinter.Items.Count > 0 Then
                cmbprinter.SelectedIndex = 0
                cmbvprinter.SelectedIndex = 0
            Else
                cmbprinter.Items.Add("No printers found")
                cmbprinter.SelectedIndex = 0
                cmbvprinter.Items.Add("No printers found")
                cmbvprinter.SelectedIndex = 0
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading printers: " & ex.Message)
        End Try
    End Sub

    Private Sub btntwoprn_Click(sender As System.Object, e As System.EventArgs) Handles btntwoprn.Click
        typeText = Trim(cmbtype.Text)
        Dim t1 = New Threading.Thread(Sub() speedprintHboth())
        Dim t2 = New Threading.Thread(Sub() speedprint2vertboth())
        'If MsgBox("Print!", vbYesNo) = vbYes Then
        t1.Start()
        t2.Start()
        'End If

    End Sub

    Private Sub btnstopcam_Click(sender As Object, e As EventArgs) Handles btnstopcam.Click
        txtscanner.Text = ""
        'Btnstop.PerformClick()

        'Btnstart.PerformClick()

        StopCamera()
        If Btnstart.Enabled = False Then Btnstart.Enabled = True
        If Btnstop.Enabled = True Then Btnstop.Enabled = False

        txtscanner.Text = ""
        'ShowOrReuseCamera()
        StartCameraDelayed()

        If Btnstart.Enabled = True Then Btnstart.Enabled = False
        If Btnstop.Enabled = False Then Btnstop.Enabled = True

    End Sub

    Private Sub speedprintHboth()
        Dim dir As String

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "nsbarcodEH.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
        'If chkprndir.Checked = True Then
        '    FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        'Else


        Dim fNum As Integer = FileSystem.FreeFile()
        FileSystem.FileOpen(fNum, mdir, OpenMode.Output, OpenAccess.Write, OpenShare.Shared, -1)
        'FileSystem.PrintLine(fNum, "This is a line with the Rupee symbol: ₹")
        'FileSystem.FileClose(fNum)



        'FileOpen(1, mdir, OpenMode.Output)
        'End If
        Dim rupeeSymbol As String = ChrW(&H20B9)
        'Dim rupeeSymbol As String = ChrW(&H20B9)
        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then
                'PrintLine(1, TAB(0), DR.Item("firstdet"))

                FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum, "SPEED 7")
                FileSystem.PrintLine(fNum, "SET PEEL OFF")
                FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
                FileSystem.PrintLine(fNum, "CLS")
                FileSystem.PrintLine(fNum, "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                'PrintLine(1, TAB(0), "TEXT BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                '"₹"
                'PrintBitmap()
                FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                FileSystem.PrintLine(fNum, TAB(0), "TEXT 433,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP Rs." & """")
                'FileSystem.PrintLine(fNum, TAB(0), "TEXT 385,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP:" & """")
                'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "IndianRupee.TTF" & """" & ",180,32,1," & """" & "`" & """")

                'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "ITF Rupee.TTF" & """" & ",180,32,1," & """" & "S" & """")
                'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & "₹" & """")

                'FileSystem.PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & rupeeSymbol & """")

                FileSystem.PrintLine(fNum, "TEXT 277,339," & """" & "0" & """" & ",180,14,12," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                FileSystem.PrintLine(fNum, "TEXT 538,291," & """" & "0" & """" & ",180,11,12," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                'TEXT 538,258,"0",180,9,9,"COLOUR :"
                FileSystem.PrintLine(fNum, "TEXT 538,258," & """" & "0" & """" & ",180,9,9," & """" & "COLOUR :" & """")
                FileSystem.PrintLine(fNum, "TEXT 538,230," & """" & "0" & """" & ",180,8,9," & """" & "COMMODITY :" & """")
                'TEXT 439,258,"ROMAN.TTF",180,1,9,"SL18-CYAN"
                FileSystem.PrintLine(fNum, "TEXT 439,258," & """" & "ROMAN.TTF" & """" & ",180,1,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")

                FileSystem.PrintLine(fNum, "TEXT 410,230," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 367,189," & """" & "0" & """" & ",180,10,8," & """" & "Made in India" & """")


                FileSystem.PrintLine(fNum, "TEXT 182,252," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")


                If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                    'Text(109, 249, "0", 180, 11, 10, "Length")
                    FileSystem.PrintLine(fNum, "TEXT 109,249," & """" & "0" & """" & ",180,11,10," & """" & "Length" & """")
                    'Text(182, 211, "0", 180, 9, 17, "Code:")
                    FileSystem.PrintLine(fNum, "TEXT 182,211," & """" & "0" & """" & ",180,9,17," & """" & "Code:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    'TEXT 63,198,"0",180,7,10,"(Inch)"
                    FileSystem.PrintLine(fNum, "TEXT 63,198," & """" & "0" & """" & ",180,7,10," & """" & "(Inch)" & """")
                Else
                    FileSystem.PrintLine(fNum, "TEXT 103,249," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")
                    FileSystem.PrintLine(fNum, "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """")
                    FileSystem.PrintLine(fNum, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """")
                End If

                'TEXT 172,158,"0",180,11,9,"Net Qty :1N"
                'FileSystem.PrintLine(fNum, "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """")
                'FileSystem.PrintLine(fNum, "TEXT 116,216," & """" & "0" & """" & ",180,16,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                'FileSystem.PrintLine(fNum, "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """")
                If chktwin.Checked = True Then
                    FileSystem.PrintLine(fNum, "TEXT 182,160," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :2N" & """")
                ElseIf chkset.Checked = True Then
                    FileSystem.PrintLine(fNum, "TEXT 182,160," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :" & Val(txtno.Text) & "N" & """")
                Else
                    FileSystem.PrintLine(fNum, "TEXT 182,160," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1N" & """")
                End If

                FileSystem.PrintLine(fNum, "QRCODE 519,199,L,3,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 367,159," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 322,131," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 277,131," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 346,313," & """" & "ROMAN.TTF" & """" & ",180,1,7," & """" & "(Incl.of all Taxes)" & """")

                'typeText = Trim(cmbtype.Text)

                'If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                If typeText <> "Dealer" And typeText <> "TN" Then
                    FileSystem.PrintLine(fNum, "TEXT 538,133," & """" & "0" & """" & ",180,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                End If
                If chktwin.Checked = True Then
                    FileSystem.PrintLine(fNum, "TEXT 182,291," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "(1+1) 2N" & """")
                End If
                'If chkliberty.Checked = True Then
                'FileSystem.PrintLine(fNum, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & "LIBERTY CUT" & """")
                'FileSystem.PrintLine(fNum, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                'End If


                If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                    FileSystem.PrintLine(fNum, "TEXT 120,279," & """" & "0" & """" & ",180,12,9," & """" & (Trim(dg.Rows(i).Cells(15).Value) & " cm") & """")
                    FileSystem.PrintLine(fNum, "TEXT 182,283," & """" & "0" & """" & ",180,11,11," & """" & "Size:" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 63,279," & """" & "0" & """" & ",180,10,9," & """" & "cm" & """")
                End If
                If Chkshirt.Checked = True Then
                    FileSystem.PrintLine(fNum, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & Trim(dg.Rows(i).Cells(16).Value) & """")
                    FileSystem.PrintLine(fNum, "TEXT 182,130," & """" & "0" & """" & ",180,8,9," & """" & Trim(dg.Rows(i).Cells(17).Value) & """")
                End If

                'Text(182, 282, "0", 180, 8, 9, "PRESTIGIOUS FIT")
                'Text(182, 130, "0", 180, 8, 9, "LIBERTY CUT")

                'Text(120, 279, "0", 180, 12, 9, "106")
                'Text(182, 283, "0", 180, 11, 11, "Size:")
                'Text(63, 279, "0", 180, 10, 9, "cm")
                If chkmfg.Checked = False Then
                    FileSystem.PrintLine(fNum, "TEXT 464,107," & """" & "0" & """" & ",180,8,7," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    FileSystem.PrintLine(fNum, "TEXT 444,85," & """" & "0" & """" & ",180,8,7," & """" & "address details are available in the box" & """")
                End If



                'FileSystem.PrintLine(fNum, "PRINT 1," & Val(dg.Rows(i).Cells(14).Value))
                FileSystem.PrintLine(fNum, "PRINT 1,1")
                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")

                'dg.Rows(i).Cells(0).Value = False
            End If
        Next
        FileSystem.FileClose(fNum)

        If mcitrix = "Y" Then

            'citrixprint(mdir)
            citrixprint2(mdir, cmbprinter.SelectedItem.ToString())
        Else
            Shell("rawpr.bat " & mdir)
            'Shell("""" & System.Windows.Forms.Application.StartupPath & "\rawpr.bat"" """ & mdir & """", AppWinStyle.Hide)
        End If

    End Sub


    Private Sub speedprint2vertboth()
        Dim dirv, mdirv As String

        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"

        dirv = System.AppDomain.CurrentDomain.BaseDirectory()
        mdirv = Trim(dirv) & "nsbarcodEV.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
        'If chkprndir.Checked = True Then
        '    FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        'Else


        Dim fNum2 As Integer = FileSystem.FreeFile()
        FileSystem.FileOpen(fNum2, mdirv, OpenMode.Output, OpenAccess.Write, OpenShare.Shared, -1)
        'FileSystem.PrintLine(fNum, "This is a line with the Rupee symbol: ₹")
        'FileSystem.FileClose(fNum)



        'FileOpen(1, mdir, OpenMode.Output)
        'End If
        Dim rupeeSymbol As String = ChrW(&H20B9)
        'Dim rupeeSymbol As String = ChrW(&H20B9)



        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then
                'PrintLine(1, TAB(0), DR.Item("firstdet"))
                If Chkshirt.Checked = True Then
                    FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    FileSystem.PrintLine(fNum2, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum2, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum2, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum2, "SPEED 7")
                    FileSystem.PrintLine(fNum2, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum2, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                    FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
                    FileSystem.PrintLine(fNum2, "CLS")
                    FileSystem.PrintLine(fNum2, "BITMAP 520,161,4,24,1,ÿÿÜÿÿÇÌÿ‡ÌþÌüÌøÌð#ÌàsÈÀñÀð ø ü þ ?ÿŒÿÌÿÿÌÿÿÌÿÿïÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")


                    'PrintLine(1, TAB(0), "TEXT BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    '"₹"
                    'PrintBitmap()
                    FileSystem.PrintLine(fNum2, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum2, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum2, "TEXT 543,193," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum2, "TEXT 493,13," & """" & "0" & """" & ",90,8,11," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :9,10"
                    FileSystem.PrintLine(fNum2, "TEXT 462,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 430,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 460,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    'FileSystem.PrintLine(fNum, "TEXT 300,8," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 425,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 397,13," & """" & "ROMAN.TTF" & """" & ",90,1,10," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 397,70," & """" & "0" & """" & ",90,9,10," & """" & "SLEEVE" & """")

                    FileSystem.PrintLine(fNum2, "TEXT 398,193," & """" & "0" & """" & ",90,11,20," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 398,260," & """" & "0" & """" & ",90,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 400,310," & """" & "0" & """" & ",90,12,21," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 330,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :1 N" & """")
                    FileSystem.PrintLine(fNum2, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 295,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 330,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 330,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 517,90," & """" & "ROMAN.TTF" & """" & ",90,1,7," & """" & "(Incl.of all Taxes)" & """")

                    'typeText = Trim(cmbtype.Text)

                    ' If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 240,12," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    If chkvertfit.Checked = True Then
                        'fit
                        FileSystem.PrintLine(fNum2, "TEXT 270,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(16).Value) & """")
                        'cut
                        FileSystem.PrintLine(fNum2, "TEXT 360,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(17).Value) & """")
                    End If


                    '***
                    'FileSystem.PrintLine(fNum, "TEXT  523,281," & """" & "0" & """" & ",180,7,9," & """" & "Commodity Name:" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 300,8," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")


                    'FileSystem.PrintLine(fNum, "TEXT 524,67," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 524,40," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")

                    'FileSystem.PrintLine(fNum, "TEXT 149,245," & """" & "0" & """" & ",180,9,9," & """" & "Net Qty :" & """")
                    ''***
                    'FileSystem.PrintLine(fNum, "TEXT 270,8," & """" & "0" & """" & ",90,9,10," & """" & "SMART FIT" & """")
                    'If chkliberty.Checked = True Then
                    '    FileSystem.PrintLine(fNum, "TEXT 360,8," & """" & "0" & """" & ",90,9,10," & """" & "LIBERTY CUT" & """")
                    'End If


                ElseIf chkpant.Checked = True Then

                    FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    FileSystem.PrintLine(fNum2, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum2, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum2, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum2, "SPEED 7")
                    FileSystem.PrintLine(fNum2, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum2, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                    FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
                    FileSystem.PrintLine(fNum2, "CLS")
                    'FileSystem.PrintLine(fNum, "BITMAP 369,192,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    FileSystem.PrintLine(fNum2, "BITMAP 520,161,4,24,1,ÿÿÜÿÿÇÌÿ‡ÌþÌüÌøÌð#ÌàsÈÀñÀð ø ü þ ?ÿŒÿÌÿÿÌÿÿÌÿÿïÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")

                    'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    '"₹"
                    'PrintBitmap()
                    FileSystem.PrintLine(fNum2, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum2, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum2, "TEXT 543,191," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum2, "TEXT 491,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :"
                    FileSystem.PrintLine(fNum2, "TEXT 458,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 425,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 458,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 425,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 280,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 395,13," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 395,75," & """" & "0" & """" & ",90,8,12," & """" & "LENGTH" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 360,13," & """" & "0" & """" & ",90,11,20," & """" & "Code :" & """")


                    'FileSystem.PrintLine(fNum, "TEXT 183,312," & """" & "0" & """" & ",180,8,9," & """" & "Size: Waist" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 360,80S," & """" & "0" & """" & ",90,18,12," & """" & Trim(Math.Round(Val(dg.Rows(i).Cells(4).Value) * 2.54, 0)) & """")
                    'FileSystem.PrintLine(fNum, "TEXT 49,312," & """" & "0" & """" & ",180,8,10," & """" & "cm" & """")

                    FileSystem.PrintLine(fNum2, "TEXT 350,95," & """" & "0" & """" & ",90,18,12," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 365,140," & """" & "0" & """" & ",90,12,21," & """" & "(Inch)" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 305,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :1N" & """")
                    FileSystem.PrintLine(fNum2, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 275,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 306,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 306,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 517,90," & """" & "ROMAN.TTF" & """" & ",90,1,7," & """" & "(Incl.of all Taxes)" & """")

                    ' If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 249,13," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum2, "TEXT 390,240," & """" & "0" & """" & ",90,12,9," & """" & Trim(Math.Round(Val(dg.Rows(i).Cells(4).Value) * 2.54, 0)) & "cm" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 395,175," & """" & "0" & """" & ",90,11,11," & """" & "Size:" & """")

                    'FileSystem.PrintLine(fNum, "TEXT  390,240," & """" & "0" & """" & ",90,12,9," & """" & "Commodity Name:" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 527,147," & """" & "0" & """" & ",180,10,9," & """" & "Made in India" & """")

                    'FileSystem.PrintLine(fNum, "TEXT 524,67," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 524,40," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 149,245," & """" & "0" & """" & ",180,10,9," & """" & "Net Qty :" & """")

                    'FileSystem.PrintLine(fNum, "TEXT 85,311," & """" & "0" & """" & ",180,8,10," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    'FileSystem.PrintLine(fNum, "TEXT 130,153," & """" & "0" & """" & ",180,8,10," & """" & "Code :" & """")




                ElseIf chktwin.Checked = True Then
                    FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    FileSystem.PrintLine(fNum2, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum2, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum2, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum2, "SPEED 7")
                    FileSystem.PrintLine(fNum2, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum2, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                    FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
                    FileSystem.PrintLine(fNum2, "CLS")
                    ''FileSystem.PrintLine(fNum, "BITMAP 322,162,9,56,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    ''PrintLine(1, TAB(0), "TEXT BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'FileSystem.PrintLine(fNum, "BITMAP 370,190,3,32,1,ðÿø?ÿüÿþÿÿ ÿÿƒÿÿÁÿÿàÿÿðÿø?ÿüÿøÿ€ÿ ÿüÿü?ÿøÿøÿ€   ø?ÿüÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'FileSystem.PrintLine(fNum, "BITMAP 323,163,2,24,1,áÿðÿøü?þ?ÿÿÿÇÿü?øÿñÿÀ  ñÿø€  ÿÿÿÿÿÿÿÿÿÿÿÿ")

                    PrintLine(1, TAB(0), "BITMAP 520,161,4,24,1,ÿÿÜÿÿÇÌÿ‡ÌþÌüÌøÌð#ÌàsÈÀñÀð ø ü þ ?ÿŒÿÌÿÿÌÿÿÌÿÿïÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")


                    'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    '"₹"
                    'PrintBitmap()
                    FileSystem.PrintLine(fNum2, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum2, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum2, "TEXT 543,193," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum2, "TEXT 491,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :"
                    FileSystem.PrintLine(fNum2, "TEXT 459,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 430,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 455,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 427,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 270,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 397,13," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 397,75," & """" & "0" & """" & ",90,9,11," & """" & "SLEEVE" & """")

                    FileSystem.PrintLine(fNum2, "TEXT 360,13," & """" & "0" & """" & ",90,11,20," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 360,75," & """" & "0" & """" & ",90,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 360,130," & """" & "0" & """" & ",90,12,21," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :2N" & """")
                    FileSystem.PrintLine(fNum2, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 270,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 517,90," & """" & "ROMAN.TTF" & """" & ",90,1,9," & """" & "(Incl.of all Taxes)" & """")

                    'If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 240,13," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If
                    FileSystem.PrintLine(fNum2, "TEXT  493,220," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & "(1+1) 2N" & """")

                    'FileSystem.PrintLine(fNum, "TEXT  528,282," & """" & "0" & """" & ",180,8,9," & """" & "Commodity Name:" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 338,148," & """" & "0" & """" & ",180,10,9," & """" & "Made in India" & """")

                    'FileSystem.PrintLine(fNum, "TEXT 524,67," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 524,40," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 528,181," & """" & "0" & """" & ",180,9,8," & """" & "Unit Sale Price: Rs." & """")

                    'FileSystem.PrintLine(fNum, "TEXT 319,180," & """" & "0" & """" & ",180,11,8," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value) / 2, "######.00") & """")
                    'FileSystem.PrintLine(fNum, "TEXT 229,180," & """" & "0" & """" & ",180,6,7," & """" & "(Incl.of all Taxes)" & """")

                    'FileSystem.PrintLine(fNum, "TEXT 168,253," & """" & "0" & """" & ",180,10,9," & """" & "Net Qty :" & """")




                ElseIf chkset.Checked = True Then

                    FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    FileSystem.PrintLine(fNum2, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum2, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum2, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum2, "SPEED 7")
                    FileSystem.PrintLine(fNum2, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum2, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                    FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
                    FileSystem.PrintLine(fNum2, "CLS")
                    'FileSystem.PrintLine(fNum, "BITMAP 366,96,4,40,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'FileSystem.PrintLine(fNum, "BITMAP 214,152,21,24,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")

                    FileSystem.PrintLine(fNum2, "BITMAP 520,161,4,24,1,ÿÿÜÿÿÇÌÿ‡ÌþÌüÌøÌð#ÌàsÈÀñÀð ø ü þ ?ÿŒÿÌÿÿÌÿÿÌÿÿïÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")


                    FileSystem.PrintLine(fNum2, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum2, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum2, "TEXT 543,193," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum2, "TEXT 490,13," & """" & "0" & """" & ",90,8,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :"
                    FileSystem.PrintLine(fNum2, "TEXT 460,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 430,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 458,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 425,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 270,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 395,13," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 394,75," & """" & "0" & """" & ",90,9,10," & """" & "SLEEVE" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 360,13," & """" & "0" & """" & ",90,11,20," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum2, "TEXT  360,75," & """" & "0" & """" & ",90,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 360,130," & """" & "0" & """" & ",90,12,21," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :2N" & """")
                    FileSystem.PrintLine(fNum2, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 270,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 517,90," & """" & "ROMAN.TTF" & """" & ",90,1,7," & """" & "(Incl.of all Taxes)" & """")

                    'If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 240,13," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum2, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & "" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 182,130," & """" & "0" & """" & ",180,8,9," & """" & "" & """")



                    'FileSystem.PrintLine(fNum, "TEXT 528,316," & """" & "0" & """" & ",180,7,8," & """" & "Commodity Name:" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 134,188," & """" & "0" & """" & ",180,8,7," & """" & "Made in India" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 524,61," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 524,34," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 528,172," & """" & "0" & """" & ",180,6,8," & """" & "Unit Sale Price: Rs." & """")
                    'FileSystem.PrintLine(fNum, "TEXT 364,171," & """" & "0" & """" & ",180,9,8," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value) / 2, "######.00") & """")
                    'FileSystem.PrintLine(fNum, "TEXT 411,147," & """" & "0" & """" & ",180,6,6," & """" & "(Incl.of all Taxes)" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 527,239," & """" & "0" & """" & ",180,9,7," & """" & "Net Qty :" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 387,318," & """" & "0" & """" & ",180,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    ''*****

                    'FileSystem.PrintLine(fNum, "TEXT  259,286," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    'FileSystem.PrintLine(fNum, "TEXT 259,194," & """" & "0" & """" & ",180,8,7," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    'FileSystem.PrintLine(fNum, "TEXT 212,194," & """" & "0" & """" & ",180,6,7," & """" & "SLEEVE" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 527,261," & """" & "0" & """" & ",180,11,7," & """" & "Size:" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 224,264," & """" & "0" & """" & ",180,7,9," & """" & "cm" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 259,218," & """" & "0" & """" & ",180,8,7," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    'FileSystem.PrintLine(fNum, "TEXT 528,194," & """" & "0" & """" & ",180,11,7," & """" & "Style:" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 209,170," & """" & "0" & """" & ",180,9,8," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value) / 2, "######.00") & """")
                    'FileSystem.PrintLine(fNum, "TEXT 258,171," & """" & "0" & """" & ",180,8,8," & """" & "Rs." & """")
                    'FileSystem.PrintLine(fNum, "TEXT 259,147," & """" & "0" & """" & ",180,6,6," & """" & "(Incl.of all Taxes)" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 259,238," & """" & "0" & """" & ",180,8,7," & """" & "2 N" & """")
                    'FileSystem.PrintLine(fNum, "TEXT 259,238," & """" & "0" & """" & ",180,8,8," & """" & "Rs." & """")

                Else
                    FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='41.0 mm'></xpml>SIZE 67.5 mm, 41 mm")
                    FileSystem.PrintLine(fNum2, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum2, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum2, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum2, "SPEED 7")
                    FileSystem.PrintLine(fNum2, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum2, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                    FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='1' pitch='41.0 mm'></xpml>SET TEAR ON")
                    FileSystem.PrintLine(fNum2, "CLS")
                    FileSystem.PrintLine(fNum2, "BITMAP 371,192,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'PrintLine(1, TAB(0), "TEXT BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    '"₹"
                    'PrintBitmap()
                    FileSystem.PrintLine(fNum2, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum2, TAB(0), "TEXT 528,218," & """" & "0" & """" & ",180,13,12," & """" & "MRP Rs." & """")
                    'FileSystem.PrintLine(fNum, TAB(0), "TEXT 385,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP:" & """")
                    'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "IndianRupee.TTF" & """" & ",180,32,1," & """" & "`" & """")

                    'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "ITF Rupee.TTF" & """" & ",180,32,1," & """" & "S" & """")
                    'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & "₹" & """")

                    'FileSystem.PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & rupeeSymbol & """")

                    FileSystem.PrintLine(fNum2, "TEXT 366,218," & """" & "0" & """" & ",180,14,12," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum2, "TEXT 390,280," & """" & "0" & """" & ",180,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :"
                    FileSystem.PrintLine(fNum2, "TEXT 530,250," & """" & "0" & """" & ",180,10,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 528,313," & """" & "0" & """" & ",180,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 447,250," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 407,313," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 170,188," & """" & "0" & """" & ",180,11,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 93,185," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")

                    FileSystem.PrintLine(fNum2, "TEXT 155,317," & """" & "0" & """" & ",180,9,11," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 99,316," & """" & "0" & """" & ",180,18,11," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 49,317," & """" & "0" & """" & ",180,9,12," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 58,244," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1 N" & """")
                    FileSystem.PrintLine(fNum2, "QRCODE 121,115,L,5,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 528,178," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 524,115," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 465,110," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 215,212," & """" & "ROMAN.TTF" & """" & ",180,8,7," & """" & "(Incl.of all Taxes)" & """")

                    'If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 373,108," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum2, "TEXT  523,281," & """" & "0" & """" & ",180,7,9," & """" & "Commodity Name:" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 527,148," & """" & "0" & """" & ",180,10,9," & """" & "Made in India" & """")

                    FileSystem.PrintLine(fNum2, "TEXT 524,67," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 524,40," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 149,245," & """" & "0" & """" & ",180,9,9," & """" & "Net Qty :" & """")


                    If chkliberty.Checked = True Then
                        FileSystem.PrintLine(fNum2, "TEXT 170,152," & """" & "0" & """" & ",180,10,9," & """" & "LIBERTY CUT" & """")
                    End If



                    FileSystem.PrintLine(fNum2, "TEXT 538,230," & """" & "0" & """" & ",180,8,9," & """" & "CONTENT :" & """")
                    'TEXT 439,258,"ROMAN.TTF",180,1,9,"SL18-CYAN"
                    FileSystem.PrintLine(fNum2, "TEXT 439,258," & """" & "ROMAN.TTF" & """" & ",180,1,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 430,230," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 182,252," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                        'Text(109, 249, "0", 180, 11, 10, "Length")
                        FileSystem.PrintLine(fNum2, "TEXT 109,249," & """" & "0" & """" & ",180,11,10," & """" & "Length" & """")
                        'Text(182, 211, "0", 180, 9, 17, "Code:")
                        FileSystem.PrintLine(fNum2, "TEXT 182,211," & """" & "0" & """" & ",180,9,17," & """" & "Code:" & """")
                        FileSystem.PrintLine(fNum2, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                        'TEXT 63,198,"0",180,7,10,"(Inch)"
                        FileSystem.PrintLine(fNum2, "TEXT 63,198," & """" & "0" & """" & ",180,7,10," & """" & "(Inch)" & """")
                    Else
                        FileSystem.PrintLine(fNum2, "TEXT 103,249," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")
                        FileSystem.PrintLine(fNum2, "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """")
                        FileSystem.PrintLine(fNum2, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                        FileSystem.PrintLine(fNum2, "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """")
                    End If


                    If chktwin.Checked = True Then
                        FileSystem.PrintLine(fNum2, "TEXT 172,158," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :2N" & """")
                    ElseIf chkset.Checked = True Then
                        FileSystem.PrintLine(fNum2, "TEXT 172,158," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :" & Val(txtno.Text) & "N" & """")
                    Else
                        FileSystem.PrintLine(fNum2, "TEXT 172,158," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1N" & """")
                    End If

                    FileSystem.PrintLine(fNum2, "QRCODE 519,199,L,3,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 367,159," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 322,131," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 277,131," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 346,313," & """" & "ROMAN.TTF" & """" & ",180,1,7," & """" & "(Incl.of all Taxes)" & """")

                    'If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 538,133," & """" & "0" & """" & ",180,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If
                    If chktwin.Checked = True Then
                        FileSystem.PrintLine(fNum2, "TEXT 182,291," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "(1+1) 2N" & """")
                    End If
                    If chkliberty.Checked = True Then
                        FileSystem.PrintLine(fNum2, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & "LIBERTY CUT" & """")
                    End If


                    If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                        FileSystem.PrintLine(fNum2, "TEXT 120,279," & """" & "0" & """" & ",180,12,9," & """" & (Trim(dg.Rows(i).Cells(15).Value) & " cm") & """")
                        FileSystem.PrintLine(fNum2, "TEXT 182,283," & """" & "0" & """" & ",180,11,11," & """" & "Size:" & """")
                        'FileSystem.PrintLine(fNum, "TEXT 63,279," & """" & "0" & """" & ",180,10,9," & """" & "cm" & """")
                    End If
                End If



                'FileSystem.PrintLine(fNum2, "PRINT 1," & Val(dg.Rows(i).Cells(14).Value))
                FileSystem.PrintLine(fNum2, "PRINT 1,1")
                FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><end/></xpml>")


                dg.Rows(i).Cells(0).Value = False
            End If
        Next
        FileSystem.FileClose(fNum2)
        'FileClose(1)

        If mcitrix = "Y" Then

            'citrixprint(mdir)
            citrixprint2(mdirv, cmbvprinter.SelectedItem.ToString())
        Else
            Shell("rawprv.bat " & mdirv)
        End If


        ''PrintTextFile(mdir)
        ''PrintToTSCPrinter(mdir, Trim(cmbprinter.Text))


        'If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '    'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
        '    ' Call updateprn()

        '    If mcitrix = "Y" Then

        '        'citrixprint(mdir)
        '        citrixprint2(mdir, cmbprinter.SelectedItem.ToString())

        '    Else
        '        If MsgBox("Lpt Port", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

        '            'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text) & ":")
        '            Shell("rawprv.bat " & mdir)

        '        Else

        '            'Dim text As String = File.ReadAllText(mdir)
        '            Dim text As String = File.ReadAllText(mdir, System.Text.Encoding.GetEncoding(1252))
        '            Dim pd As PrintDialog = New PrintDialog()
        '            pd.PrinterSettings = New PrinterSettings()
        '            If Len(Trim(cmbprinter.Text)) > 0 Then
        '                BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName = Trim(cmbprinter.Text), text)
        '            Else
        '                BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName, text)
        '            End If

        '        End If
        '    End If
        'End If


    End Sub





    '    Private Async Sub btntwoprn_Click(sender As Object, e As EventArgs) Handles btntwoprn.Click
    '    If MsgBox("Print to both printers?", vbYesNo) = vbYes Then
    '        Await Task.WhenAll(
    '            Task.Run(Sub() speedprintHboth()),
    '            Task.Run(Sub() speedprint2vertboth())
    '        )
    '        MsgBox("Printed to both printers.")
    '    End If
    'End Sub

    'Dim t1 = New Threading.Thread(Sub() speedprintHboth())
    'Dim t2 = New Threading.Thread(Sub() speedprint2vertboth())

    'Private Sub btntwoprn_Click(sender As Object, e As EventArgs) Handles btntwoprn.Click
    '    If MsgBox("Print to both printers?", vbYesNo) = vbYes Then
    '        Try
    '            Dim data1 As String = speedprintHboth()
    '            Dim data2 As String = speedprint2vertboth()

    '            Dim printer1 As String = "TSC_Printer_1"
    '            Dim printer2 As String = "TSC_Printer_2"

    '            Task.Factory.StartNew(Sub()
    '                                      RawPrinterHelper.SendStringToPrinter(printer1, data1)
    '                                  End Sub)

    '            Task.Factory.StartNew(Sub()
    '                                      RawPrinterHelper.SendStringToPrinter(printer2, data2)
    '                                  End Sub)

    '        Catch ex As Exception
    '            MsgBox("Error: " & ex.Message)
    '        End Try
    '    End If
    'End Sub


    Private Sub loadecomprn()
        Dim dir, mdir As String
        Dim mtbox, j, txtbno As Integer
        Dim sno, lin As Integer
        Dim batPath As String = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "rawprv.bat")

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "Qrbarcode.txt"


        'Dim dir As String
        ''dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        ''mdir = Trim(dir) & "\sbarcodE.txt"

        'dir = System.AppDomain.CurrentDomain.BaseDirectory()
        'mdir = Trim(dir) & "sbarcodE.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)


        'If CHKDIRPRN.Checked = True Then
        '    FileOpen(1, "LPT" & Trim(TextBox2.Text), OpenMode.Output)
        'Else
        FileOpen(1, mdir, OpenMode.Output)
        'End If




        lin = 0
        Dim IQR As Integer = 0

        'Call MAIN()
        'Dim da As New SqlDataAdapter, 
        'Dim ds As New DataSet
        'Dim da As New OleDb.OleDbDataAdapter
        'da.SelectCommand = New OleDb.OleDbCommand
        'da.SelectCommand.Connection = con
        'da.SelectCommand.CommandType = CommandType.Text
        'da.SelectCommand.CommandText = "SELECT LineId FROM [QRIndentity]"
        'da.Fill(ds, "tbl2")
        'Dim dt As DataTable = ds.Tables("tbl2")
        'txtbno = 0
        'IQR = dt.Rows(0)("LineId")
        sno = 1
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
        'PrintLine(1, TAB(0), "^XA")
        'lin = lin + 1

        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            Dim qtty As Integer
            c = dg.Rows(i).Cells(0).Value
            'For Each row As DataGridViewRow In dg.Rows
            If c = True Then
                qtty = Val(dg.Rows(i).Cells(14).Value)
                Dim testPos As Integer = InStr(1, dg.Rows(i).Cells(9).Value, "-", CompareMethod.Text)
                If testPos > 0 Then
                    strArrc = dg.Rows(i).Cells(9).Value.ToString.Split("-")
                    mbarcode = strArrc(1).ToString
                End If
                Dim testcol As Integer = InStr(1, dg.Rows(i).Cells(5).Value, "-", CompareMethod.Text)
                If testcol > 0 Then
                    strcol = dg.Rows(i).Cells(5).Value.ToString.Split("-")
                    mkcolor = strcol(1).ToString
                End If

                For j = 1 To qtty

                    PrintLine(1, TAB(0), "^FO" & Trim(Str(90 + Val(txtbno))) & ",5^BQN,3,4^FD000" & Trim(mbarcode) & "^FS")
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(205 + Val(txtbno))) & ",10^A0R,25,25^CI13^FR^FD" & Trim(mkcolor) & "^FS")
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(185 + Val(txtbno))) & ",10^A0R,20,15^CI13^FR^FD" & Trim(dg.Rows(i).Cells(3).Value) & "^FS ")
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(185 + Val(txtbno))) & ",50^A0R,20,15^CI13^FR^FD" & Trim(dg.Rows(i).Cells(4).Value) & "^FS")
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(95 + Val(txtbno))) & ",137^A0N,15,13^CI13^FR^FD" & Trim(dg.Rows(i).Cells(2).Value) & "^FS")
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(90 + Val(txtbno))) & ",120^A0N,20,15^CI13^FR^FD" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & "^FS")
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(90 + Val(txtbno))) & ",103^A0N,20,15^CI13^FR^FD" & Trim(mbarcode) & "^FS ")



                    'Dim delimiter As Char = "/"
                    'Dim strArr As String() = row.Cells.Item("ITEMNAME").Value.Split(delimiter)


                    lin = lin + 1

                    '^FO20,149^A0N,20,20^CI13^FR^FD38F^FS
                    'IQR = IQR + 1
                    txtbno = txtbno + 160
                    If sno = 4 Then
                        PrintLine(1, TAB(0), "^PQ1,0,0,N")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^XZ")

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
                        'PrintLine(1, TAB(0), "^XA")
                        'lin = lin + 1
                        txtbno = 0
                        sno = 0
                    End If
                    sno = sno + 1

                    'PrintLine(1, TAB(0), "^PQ1,0,0,N")
                    'lin = lin + 1
                    'PrintLine(1, TAB(0), "^XZ")
                    ' IQR = IQR + 5

                Next j


            End If


        Next
        PrintLine(1, TAB(0), "^PQ1,0,0,N")
        lin = lin + 1
        PrintLine(1, TAB(0), "^XZ")


        ''Dim com As New SqlCommand
        'Dim com As New OLEDB.OleDbCommand
        'com.Connection = con
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If
        'com.CommandText = "UPDATE [QRIndentity] SET LINEID = '" & IQR & "'"
        'com.ExecuteNonQuery()
        'com.Dispose()


        FileClose(1)

        If mcitrix = "Y" Then
            'citrixprint(mdir)
            citrixprint2(mdir, cmbvprinter.SelectedItem.ToString())
        Else
            Shell("""" & batPath & """ " & mdir, AppWinStyle.NormalFocus)
            'Shell("rawprv.bat " & mdir)
        End If



        'If CHKDIRPRN.Checked = True Then
        '    If MsgBox("Ok", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
        '        'dir = System.AppDomain.CurrentDomain.BaseDirectory()
        '        'mdir = Trim(dir) & "Qrbarcode.txt"
        '        'Shell("cmd.exe /c" & "type " & mdir & " > lpt" & Trim(TextBox2.Text))
        '        'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(TextBox2.Text) & ":")
        '        Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(TextBox2.Text))

        '        'Dim proc As Process = New Process
        '        'proc.StartInfo.FileName = "cmd.exe "
        '        'proc.StartInfo.Arguments = "  /c type " & mdir & " > lpt" & Trim(TextBox2.Text)
        '        'proc.Start()

        '    End If
        'Else
        '    If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '        If MsgBox("Print on LPT", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '            Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(TextBox2.Text))
        '        Else
        '            Dim text As String = File.ReadAllText(mdir)
        '            Dim pd As PrintDialog = New PrintDialog()
        '            pd.PrinterSettings = New PrinterSettings()
        '            BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName, text)
        '        End If
        '    End If

        'End If



        'Shell("print /d:LPT" & Trim(TextBox2.Text) & mdir, vbNormalFocus)
        '        Shell("cmd.exe /c" & "type " & mdir & " > lpt1")
    End Sub




    Private Sub loadprimaprn()
        Dim dir, mdir As String
        Dim mtbox, j, txtbno As Integer
        Dim sno, lin As Integer

        Dim batPath As String = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "rawprv.bat")


        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "Qrbarcode.txt"


        'Dim dir As String
        ''dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        ''mdir = Trim(dir) & "\sbarcodE.txt"

        'dir = System.AppDomain.CurrentDomain.BaseDirectory()
        'mdir = Trim(dir) & "sbarcodE.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)


        'If CHKDIRPRN.Checked = True Then
        '    FileOpen(1, "LPT" & Trim(TextBox2.Text), OpenMode.Output)
        'Else
        FileOpen(1, mdir, OpenMode.Output)
        'End If




        lin = 0
        Dim IQR As Integer = 0


        sno = 1
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
        'PrintLine(1, TAB(0), "^XA")
        'lin = lin + 1

        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            Dim qtty As Integer
            c = dg.Rows(i).Cells(0).Value
            'For Each row As DataGridViewRow In dg.Rows
            If c = True Then
                qtty = Val(dg.Rows(i).Cells(14).Value)
                'Dim testPos As Integer = InStr(1, dg.Rows(i).Cells(9).Value, "-", CompareMethod.Text)
                'If testPos > 0 Then
                '    strArrc = dg.Rows(i).Cells(9).Value.ToString.Split("-")
                '    mbarcode = strArrc(1).ToString
                'End If
                Dim testcol As Integer = InStr(1, dg.Rows(i).Cells(5).Value, "-", CompareMethod.Text)
                If testcol > 0 Then
                    strcol = dg.Rows(i).Cells(5).Value.ToString.Split("-")
                    mkcolor = strcol(1).ToString
                End If

                For j = 1 To qtty

                    PrintLine(1, TAB(0), "^FO" & Trim(Str(100 + Val(txtbno))) & ",10^BQN,3,3^FD000" & Trim(dg.Rows(i).Cells(9).Value) & "^FS")
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(205 + Val(txtbno))) & ",15^A0R,25,25^CI13^FR^FD" & Trim(mkcolor) & "^FS")
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(95 + Val(txtbno))) & ",104^A0N,20,20^CI13^FR^FD" & Trim(dg.Rows(i).Cells(2).Value) & "^FS")
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(95 + Val(txtbno))) & ",124^A0N,20,15^CI13^FR^FD" & Trim(dg.Rows(i).Cells(3).Value) & "^FS ")
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(145 + Val(txtbno))) & ",124^A0N,20,15^CI13^FR^FD" & Trim(dg.Rows(i).Cells(4).Value) & "^FS")

                    'PrintLine(1, TAB(0), "^FO" & Trim(Str(90 + Val(txtbno))) & ",120^A0N,20,15^CI13^FR^FD" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & "^FS")
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(100 + Val(txtbno))) & ",141^A0N,15,10^CI13^FR^FD" & Trim(dg.Rows(i).Cells(10).Value) & "^FS ")



                    'Dim delimiter As Char = "/"
                    'Dim strArr As String() = row.Cells.Item("ITEMNAME").Value.Split(delimiter)


                    lin = lin + 1

                    '^FO20,149^A0N,20,20^CI13^FR^FD38F^FS
                    'IQR = IQR + 1
                    txtbno = txtbno + 160
                    If sno = 4 Then
                        PrintLine(1, TAB(0), "^PQ1,0,0,N")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^XZ")

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
                        'PrintLine(1, TAB(0), "^XA")
                        'lin = lin + 1
                        txtbno = 0
                        sno = 0
                    End If
                    sno = sno + 1

                    'PrintLine(1, TAB(0), "^PQ1,0,0,N")
                    'lin = lin + 1
                    'PrintLine(1, TAB(0), "^XZ")
                    ' IQR = IQR + 5

                Next j


            End If


        Next
        PrintLine(1, TAB(0), "^PQ1,0,0,N")
        lin = lin + 1
        PrintLine(1, TAB(0), "^XZ")




        FileClose(1)

        If mcitrix = "Y" Then
            'citrixprint(mdir)
            citrixprint2(mdir, cmbvprinter.SelectedItem.ToString())
        Else
            Shell("""" & batPath & """ " & mdir, AppWinStyle.NormalFocus)
            'Shell("rawprv.bat " & mdir)
        End If




    End Sub

    Private Sub chkecom_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkecom.CheckedChanged
        If chkecom.Checked = True Then
            chkprima.Checked = False
        End If
    End Sub

    Private Sub chkprima_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkprima.CheckedChanged
        If chkprima.Checked = True Then
            chkecom.Checked = False
        End If
    End Sub

    Private Sub shrmqrcode()

        Dim dir As String
        Dim mfno As Integer
        Dim mlno As Integer
        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "SHbarcodE.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
        'If chkprndir.Checked = True Then
        '    FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        'Else


        Dim fNum As Integer = FileSystem.FreeFile()
        FileSystem.FileOpen(fNum, mdir, OpenMode.Output, OpenAccess.Write, OpenShare.Shared, -1)
        'FileSystem.PrintLine(fNum, "This is a line with the Rupee symbol: ₹")
        'FileSystem.FileClose(fNum)

        mlno = 0
        mfno = 0
        'FileOpen(1, mdir, OpenMode.Output)
        'End If
        Dim rupeeSymbol As String = ChrW(&H20B9)
        'Dim rupeeSymbol As String = ChrW(&H20B9)
        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then
                FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='23.5 mm'></xpml>SIZE 77.6 mm, 45 mm")
                FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum, "SPEED 7")
                FileSystem.PrintLine(fNum, "SET PEEL OFF")
                FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='23.5 mm'></xpml>SET TEAR ON")
                FileSystem.PrintLine(fNum, "CLS")
                ' FileSystem.PrintLine(fNum, "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")


                'PrintLine(1, TAB(0), DR.Item("firstdet"))
                For j = 1 To Val(dg.Rows(i).Cells(14).Value)
                    'FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='23.5 mm'></xpml>SIZE 77.6 mm, 45 mm")
                    'FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    'FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    'FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    'FileSystem.PrintLine(fNum, "SPEED 7")
                    'FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    'FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    'FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    'FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='23.5 mm'></xpml>SET TEAR ON")
                    'FileSystem.PrintLine(fNum, "CLS")
                    '' FileSystem.PrintLine(fNum, "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")

                    'FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & (619 - mlno) & ",182," & """" & "0" & """" & ",180,8,10," & """" & "Reduced MRP due to GST reduction" & """")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & (619 - mlno) & ",152," & """" & "0" & """" & ",180,9,8," & """" & "Effective from 22-09-2025" & """")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & (619 - mlno) & ",127," & """" & "0" & """" & ",180,10,13," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & (619 - mlno) & ",92," & """" & "0" & """" & ",180,12,12," & """" & "Rs. " & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & (599 - mlno) & ",62," & """" & "0" & """" & ",180,7,8," & """" & "(Incl.of all Taxes)" & """")


                    If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                        FileSystem.PrintLine(fNum, "TEXT " & (409 - mlno) & ",92," & """" & "0" & """" & ",180,9,10," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                        FileSystem.PrintLine(fNum, "TEXT " & (379 - mlno) & ",92," & """" & "0" & """" & ",180,9,10," & """" & "(Inch)" & """")
                        FileSystem.PrintLine(fNum, "TEXT " & (459 - mlno) & ",64," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                        FileSystem.PrintLine(fNum, "TEXT " & (399 - mlno) & ",64," & """" & "0" & """" & ",180,8,8," & """" & "Length" & """")

                    Else

                        FileSystem.PrintLine(fNum, "TEXT " & (409 - mlno) & ",92," & """" & "0" & """" & ",180,9,10," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                        FileSystem.PrintLine(fNum, "TEXT " & (379 - mlno) & ",92," & """" & "0" & """" & ",180,9,10," & """" & "cm" & """")
                        FileSystem.PrintLine(fNum, "TEXT " & (459 - mlno) & ",64," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                        FileSystem.PrintLine(fNum, "TEXT " & (399 - mlno) & ",64," & """" & "0" & """" & ",180,8,8," & """" & "SLEEVE" & """")
                    End If


                    mfno = mfno + 1
                    mlno = mlno + 314
                    'If mfno = 2 Then
                    '    mfno = 0
                    '    mlno = 0
                    '    FileSystem.PrintLine(fNum, "PRINT 1,1")
                    '    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                    '    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='23.5 mm'></xpml>SIZE 77.6 mm, 45 mm")
                    '    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    '    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    '    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    '    FileSystem.PrintLine(fNum, "SPEED 7")
                    '    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    '    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    '    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    '    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='23.5 mm'></xpml>SET TEAR ON")
                    '    FileSystem.PrintLine(fNum, "CLS")
                    '    ' FileSystem.PrintLine(fNum, "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")

                    '    FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")

                    'End If

                    If (j Mod 2) = 0 Then
                        'If rwno > 314 Then
                        FileSystem.PrintLine(fNum, "PRINT 1,1")
                        FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                        mlno = 0
                        If j < Val(dg.Rows(i).Cells(14).Value) Then
                            FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='23.5 mm'></xpml>SIZE 77.6 mm, 45 mm")
                            FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                            FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                            FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                            FileSystem.PrintLine(fNum, "SPEED 7")
                            FileSystem.PrintLine(fNum, "SET PEEL OFF")
                            FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                            FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                            FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='23.5 mm'></xpml>SET TEAR ON")
                            FileSystem.PrintLine(fNum, "CLS")
                            FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                        End If


                    End If



                    If j >= Val(dg.Rows(i).Cells(14).Value) Then
                        mlno = 0
                        If (j Mod 2) <> 0 Then
                            FileSystem.PrintLine(fNum, "PRINT 1,1")
                            FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                        End If
                        'FileSystem.PrintLine(fNum, "PRINT 1,1")
                        'FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                    End If




                Next j


            End If
        Next
        'If mfno = 1 Then
        '    FileSystem.PrintLine(fNum, "PRINT 1,1")
        '    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
        'End If

        FileSystem.FileClose(fNum)
        'FileClose(1)


        'PrintTextFile(mdir)
        'PrintToTSCPrinter(mdir, Trim(cmbprinter.Text))




        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
            ' Call updateprn()
            If mcitrix = "Y" Then
                'citrixprint(mdir)
                citrixprint2(mdir, cmbprinter.SelectedItem.ToString())
            Else
                If MsgBox("Lpt Port", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'Shell("RAWPRINT " & Trim(cmbprinter.Text) & " " & mdir)
                    Shell("rawpr.bat " & mdir)

                Else

                    'Dim text As String = File.ReadAllText(mdir)
                    Dim text As String = File.ReadAllText(mdir, System.Text.Encoding.GetEncoding(1252))
                    Dim pd As PrintDialog = New PrintDialog()
                    pd.PrinterSettings = New PrinterSettings()
                    If Len(Trim(cmbprinter.Text)) > 0 Then
                        'MsgBox(cmbprinter.Text)
                        BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName = Trim(cmbprinter.Text), text)
                    Else
                        BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName, text)
                    End If

                End If
            End If
        End If

        cmbfit.Text = ""
        'chkliberty.Checked = False
    End Sub

    Private Sub Btnchk_Click(sender As Object, e As EventArgs) Handles Btnchk.Click
        Dim mtot, mtot2 As Integer
        For k As Integer = 0 To dg.Rows.Count - 1
            If Convert.ToInt32(dg.Rows(k).Cells(14).Value) <> Convert.ToInt32(dg.Rows(k).Cells(20).Value) Then
                dg.Rows(k).DefaultCellStyle.ForeColor = Color.Red
                'row.DefaultCellStyle.BackColor = Color.LightGreen
            Else
                dg.Rows(k).DefaultCellStyle.ForeColor = Color.Black
            End If
            mtot2 = mtot2 + Convert.ToInt32(dg.Rows(k).Cells(14).Value)
            mtot = mtot + Convert.ToInt32(dg.Rows(k).Cells(20).Value)

        Next k
        lblchkcnt.Text = mtot
        Lblqty.Text = mtot2
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim f2 As New Frmxlsrbarcode()
        f2.Show()
    End Sub

    Private Sub btnprintscan_Click(sender As Object, e As EventArgs) Handles btnprintscan.Click
        manual = 1
        'speedprint2vertbothNew()
        innerprintnew(Val(lbldocentry.Text), Val(txtpackno.Text))

    End Sub

    Private Sub Chkshirt_CheckedChanged(sender As Object, e As EventArgs) Handles Chkshirt.CheckedChanged
        If Chkshirt.Checked = True Then
            chkpant.Checked = False
        End If
    End Sub

    Private Sub chkpant_CheckedChanged(sender As Object, e As EventArgs) Handles chkpant.CheckedChanged
        If chkpant.Checked = True Then
            Chkshirt.Checked = False
        End If
    End Sub
    Private Sub loadpackitem(docentry As Integer, dtpkmaster As DataTable, invtable As String, Optional stboxsize As Integer = 0)
        Dim mtboxsize As Integer = 0
        Dgpk.DataSource = Nothing
        Dim mktruu As Boolean = False

        ' msql = "select linenum, itemcode,dscription itemname,sum(quantity) quantity from inv1 where docentry=" & docentry & " group by itemcode,dscription,linenum order by linenum"

        msql = "select linenum, itemcode,dscription itemname,sum(quantity) quantity from " & invtable & " where docentry=" & docentry & " group by itemcode,dscription,linenum order by linenum"

        dtpk = getDataTable(msql)
        If cmbtype.Text.Trim.ToLower = "showroom" Or cmbtype.Text.Trim.ToLower = "franchise" Then
            mktruu = True
        Else
            mktruu = False
        End If

        'Dim dtpkmaster As DataTable = DataGridViewToDataTable(dgas)


        'Dim dtp As DataTable = Module1.GenerateSmartBundles(dtpk, mktruu, docentry, stboxsize)
        'Dim dtp As DataTable = Module1.GenerateSmartBundles(dtpk, mktruu, docentry)
        'Dim dtp As DataTable = Module1.GenerateSmartBundles(dtpk, dtpkmaster, docentry)


        Dim dtp As DataTable = CreatePackageFromMaster(dtpk, dtpkmaster, docentry)

        Dgpk.DataSource = dtp
        'For Each col As DataGridViewColumn In Dgpk.Columns
        '    col.SortMode = DataGridViewColumnSortMode.NotSortable

        '    col.ReadOnly = True

        'Next
        'Dgpk.Columns("docentry").Width = 50
        'Dgpk.Columns("PackageCode").Width = 50
        'Dgpk.Columns("PackNo").Width = 50
        'Dgpk.Columns("ItemCode").Width = 120
        'Dgpk.Columns("ItemName").Width = 220
        'Dgpk.Columns("Qty").Width = 40
        'Dgpk.Columns("BoxSize").Width = 50
        'Dgpk.Columns("ScanQty").Width = 50

        dgformat(Dgpk)

        'Dgpk.Columns("GroupName").Width = 120


    End Sub
    Private Sub Btnpack_Click(sender As Object, e As EventArgs) Handles Btnpack.Click
        'If Dgpk.Visible = False Then Dgpk.Visible = True
        'Call loadpackitem(Convert.ToInt32(lbldocentry.Text), Convert.ToInt32(txtboxsize.Text))
        'ColorRowsByPackNo()
        Dim dtpkmaster As DataTable = DataGridViewToDataTable(dgas)
        'If dtpkmaster.Rows.Count > 0 Then
        If Dgpk.Visible = False Then Dgpk.Visible = True

        If OptSales.Checked = True Then
            minvtypeh = "rinv7"
            minvtyped = "rinv8"
            minvtable = "inv1"
        ElseIf optdateord.Checked = True Then
            minvtypeh = "rdln7"
            minvtyped = "rdln8"
            minvtable = "dln1"
        Else
            minvtypeh = "rinv7"
            minvtyped = "rinv8"
            minvtable = "inv1"

        End If

        Call loadpackitem(Convert.ToInt32(lbldocentry.Text), dtpkmaster, minvtable)
        ColorRowsByPackNo()
        loadconsolidatepack()
        dgformat(Dgpk)
        'Else
        'End If


    End Sub

    Private Sub ColorRowsByPackNo()

        Dim lastPackNo As Integer = -1
        Dim currentColor As Color = Color.LightBlue

        For Each row As DataGridViewRow In Dgpk.Rows

            Dim pno As Integer = CInt(row.Cells("PackNo").Value)

            ' When pack number changes → switch color
            If pno <> lastPackNo Then
                If currentColor = Color.LightBlue Then
                    currentColor = Color.LightGreen
                Else
                    currentColor = Color.LightBlue
                End If
                lastPackNo = pno
            End If

            row.DefaultCellStyle.BackColor = currentColor

        Next

    End Sub

    Private Sub Dgpk_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub Btndupdel_Click(sender As System.Object, e As System.EventArgs) Handles Btndupdel.Click
        remdupdg(dg)
    End Sub

    Private Sub txtscanner_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtscanner.KeyDown

        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Tab Then
            e.SuppressKeyPress = True
            'ProcessScannerText()
            If txtscanner.Text.Trim() <> "" Then
                ProcessScannerText()
            End If

        End If

        'If txtscanner.Text.Trim() <> "" Then
        '    ProcessScannerText()
        'End If

    End Sub
    Private Sub ProcessScan(itemCode As String, Optional ByVal batchnum As String = "")
        If Chkshirt.Checked = True Or chkpant.Checked = True Then
            'lblStatus.Text = "Scanned: " & itemCode
            If dg.InvokeRequired Then
                dg.Invoke(Sub() ProcessScan(itemCode, batchnum))
                Exit Sub
            End If
            ' Search if ItemCode already exists
            Dim found As Boolean = False
            For Each row As DataGridViewRow In dg.Rows
                If row.Cells(18).Value IsNot Nothing AndAlso Trim(row.Cells(18).Value.ToString()) = itemCode AndAlso Trim(row.Cells(19).Value.ToString()) = batchnum Then
                    'Btnstop.PerformClick()

                    ' Increment Qty
                    Dim qty As Integer = CInt(row.Cells(14).Value)
                    Dim qty2 As Integer = CInt(row.Cells(20).Value)
                    If qty2 < qty Then
                        row.Cells(0).Value = True
                        row.Cells(20).Value = CInt(row.Cells(20).Value) + 1
                        row.DefaultCellStyle.BackColor = Color.LightGreen
                        row.DefaultCellStyle.ForeColor = Color.Black
                        ' Update time
                        'row.Cells(2).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        found = True
                        'BtnPrint.PerformClick()

                        'If Trim(cmbtype.Text) <> "Showroom" Or Trim(cmbtype.Text) = "Franchise" Then

                        '    'btntwoprn.PerformClick()
                        '    'speedprintHboth()
                        '    'speedprint2vertboth()
                        '    speedprint2vertbothNew()
                        'Else

                        '    'BtnPrint.PerformClick()
                        '    speedprint2vertboth()
                        'End If
                        If chkecom.Checked = True Then
                            MsgBox("Cannot Print E-Commerce Qrcode For Auto Scan")
                        ElseIf Chkonline.Checked = True Then
                            MsgBox("Cannot Print Online Qrcode For Auto Scan")
                        Else

                            speedprint2vertbothNew()
                            AddQtyToItem(Dgpk, dgph, itemCode, 1)
                            Call innerprint()
                        End If
                    End If
                    'ShowCameraUnderTextbox()
                    Exit For
                End If
            Next
            Btnchk.PerformClick()
        Else
            MsgBox("Select Shirt or Pant Check box")
        End If

        ' If not found → add new row
        'If Not found Then
        '    dgvLog.Rows.Add(itemCode, 1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
        'End If
    End Sub

    Private Sub innerprint()

        For i As Integer = 0 To dgph.Rows.Count - 1

            Dim docentry As Integer = Convert.ToInt32(dgph.Rows(i).Cells("docentry").Value)
            Dim packno As Integer = Convert.ToInt32(dgph.Rows(i).Cells("PackNo").Value)
            'Dim scanvalue As Integer = Convert.ToInt32(dgph.Rows(i).Cells("TotScanQty").Value)

            ' Adjust scanValue logic if combined barcode
            Dim totalQty As Integer = CInt(dgph.Rows(i).Cells("TotalQty").Value)
            Dim scanQty As Integer = CInt(dgph.Rows(i).Cells("TotScanQty").Value)
            Dim printed As Integer = CInt(dgph.Rows(i).Cells("Printed").Value)

            ' Increment scanned qty

            'dgv.Rows(i).Cells("TotScanQty").Value = scanQty

            ' Check print condition
            If scanQty = totalQty And printed = 0 Then
                innerprintnew2(docentry, packno)
                'PrintInnerSlip(i)
                dgph.Rows(i).Cells("Printed").Value = 1
                'MessageBox.Show("Inner Slip Printed for Pack No : " & packno)
                Exit For
            End If

        Next

    End Sub


    'Private Sub inner()
    '    For j As Integer = 0 To dgph.Rows.Count - 1
    '        If dgph.Rows(j).Cells("TotalQty").Value = dgph.Rows(j).Cells("TotscanQty").Value And dgph.Rows(j).Cells("Printed").Value = 0 Then

    '        End If
    '    Next j

    'End Sub
    'Private Sub innerprint(docentry As Integer, packno As Integer)
    '    ' Dim qry As String = "select ROW_NUMBER() over (partition by packagenum order by packagenum,id) sno,packagenum, itemcode,catalogname itemname,u_style Style,u_size Size,Quantity from rinv8 where docentry=" & docentry & " and packagenum=" & packno
    '    'Dim qry As String = "select ROW_NUMBER() over (partition by b.packagenum order by b.packagenum,b.id) sno,c.docnum,b.docentry,c.docdate,c.U_Bundle, c.cardcode,c.cardname,b.packagenum, c.U_TransporterName,
    '    '                    c.U_Destination,c.U_Destion, b.itemcode,b.catalogname itemname,b.u_style Style,b.u_size Size,b.Quantity,sum(b.quantity) over (partition by packagenum) totpcs from rinv8 b
    '    '                    inner join oinv c with (nolock) on c.docentry=b.docentry
    '    '                    where b.docentry=" & docentry & " and b.packagenum=" & packno

    '    Dim qry As String = "select ROW_NUMBER() over (partition by b.packagenum order by b.packagenum,b.id) sno,c.docnum,b.docentry,c.docdate,c.U_Bundle, c.cardcode,b.packagenum, c.U_TransporterName,
    '                        c.U_Destination,c.U_Destion, b.itemcode,t.u_brandgroup itemname,b.u_style Style,b.u_size Size,b.Quantity,sum(b.quantity) over (partition by packagenum) totpcs,
    '                        case when isnull(e.u_brch,'')='' then c.cardname else e.u_brch end Cardname,e.building,e.block,e.street,e.city,e.zipcode,e.state,
    '                        'I-'+ltrim(rtrim(convert(varchar(15),b.docentry)))+'-'+ltrim(convert(varchar(5),b.packagenum)) bundleno from rinv8 b
    '                        inner join oinv c with (nolock) on c.docentry=b.docentry
    '                        inner join crd1 e on e.cardcode=c.cardcode and e.address=c.ShipToCode
    '                        inner join oitm t on t.itemcode=b.itemcode 
    '                        where b.docentry=865458 and b.packagenum=2"

    'End Sub
    Private Sub Dgpk_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles Dgpk.CellContentClick

    End Sub

    Private Sub dgph_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgph.CellContentClick

    End Sub

    Private Sub Btnshow_Click(sender As Object, e As EventArgs)
        If Dgpk.Visible = False Then Dgpk.Visible = True
    End Sub



    'Private Sub SearchAndSelect(ByVal itemcode As String, ByVal searchColumnIndex As Integer, Optional ByVal batchnum As String = "")
    '    ' Loop through all rows in the DataGridView
    '    Dim found As Boolean = False
    '    For Each row As DataGridViewRow In dg.Rows
    '        If Not row.IsNewRow Then
    '            Dim cellValue As String = Convert.ToString(row.Cells(searchColumnIndex).Value)

    '            ' Case-insensitive search
    '            If cellValue.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 Then
    '                ' Tick the checkbox in column(0)
    '                row.Cells(0).Value = True

    '                ' Optionally, highlight the row
    '                row.DefaultCellStyle.BackColor = Color.LightYellow
    '            Else
    '                ' Uncheck and remove highlight for non-matching rows
    '                row.Cells(0).Value = False
    '                row.DefaultCellStyle.BackColor = Color.White
    '            End If
    '        End If
    '    Next
    'End Sub



    Private Sub txtscanner_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtscanner.TextChanged

        'If txtscanner.Text.Trim() <> "" Then
        '    SendKeys.Send("{ENTER}")

        '    If scanner IsNot Nothing Then
        '        scanner.StopCamera()
        '        scanner = Nothing

        '    End If
        '    'Btnstop.PerformClick()
        '    ' Close or hide the camera form
        '    If cameraForm IsNot Nothing AndAlso Not cameraForm.IsDisposed Then
        '        'cameraForm.Hide()      ' Use Hide() to keep in memory
        '        cameraForm.Close()   ' Or use Close() if you want to dispose completely
        '    End If
        '    If Btnstart.Enabled = False Then Btnstart.Enabled = True
        '    If Btnstop.Enabled = True Then Btnstop.Enabled = False
        '    'Btnstop.PerformClick()
        'End If


        '***
        'If txtscanner.Text.Trim() <> "" Then
        '    ' QR code scanned → close camera
        '    SendKeys.Send("{ENTER}") ' simulate Enter if needed
        '    CloseCameraForm()
        '    Btnstart.Enabled = True
        '    Btnstop.Enabled = False
        'Else
        '    ' Textbox blank → optionally reopen camera automatically
        '    If Not isCameraActive Then
        '        ShowCameraUnderTextbox()
        '    End If
        'End If

        '**** method new

        'If txtscanner.Text.Trim() <> "" Then
        '    'SendKeys.Send("{ENTER}")
        '    ProcessScannerText()
        '    ' Stop scanner and close camera
        '    'CloseCameraForm()
        '    'StopCamera()
        '    'Btnstart.Enabled = True
        '    'Btnstop.Enabled = False
        '    'txtscanner.Focus()
        'End If
    End Sub



    Private Sub ScanTimer_Tick(sender As Object, e As EventArgs) Handles ScanTimer.Tick
        'ScanTimer.Stop()

        'Dim scannedText As String = txtscanner.Text.Trim()
        'If scannedText = "" OrElse scannedText = lastScanText Then Exit Sub
        'lastScanText = scannedText

        '' Process scanned text (as if Enter pressed)
        'ProcessScannerText()
    End Sub

    ' Main scanner processing method
    Private Sub ProcessScannerText()
        Dim scannedText As String = txtscanner.Text.Trim()
        If scannedText = "" Then Exit Sub

        Try
            ' Decode QR/barcode
            If scannedText.Contains("}") Then
                qrcodearr = sDecode(scannedText).Split("|"c)
            Else
                qrcodearr = DecryptString(scannedText).Split("|"c)
            End If
            'qrcodearr = sDecode(scannedText).Split("|"c)

            qrcod = qrcodearr(0).Trim()

            batch = If(qrcodearr.Length > 1, qrcodearr(1).Trim(), "")

            ' Show scanned data
            Txtitcode.Text = qrcod
            txtcolor.Text = batch

            ' Process scan: update DataGridView or any logic
            If qrcod <> "" Then
                If batch <> "" Then
                    ProcessScan(qrcod, batch)
                Else
                    ProcessScan(qrcod)
                End If



                ' Move conveyor
                'If ArduinoPort IsNot Nothing AndAlso ArduinoPort.IsOpen Then
                '    If serial IsNot Nothing AndAlso serial.IsOpen Then
                '        'serial.WriteLine("No Object → Motor Running")
                '        serial.WriteLine("RUN")
                '    End If
                'End If

                'txtscanner.Text = ""
                txtscanner.Focus()
                Txtitcode.Text = ""
                txtcolor.Text = ""
            End If

            ' Move focus to next control
            Me.SelectNextControl(txtscanner, True, True, True, True)

        Catch ex As Exception
            MessageBox.Show("Scan Error: " & ex.Message)
        Finally
            txtscanner.Clear()
            txtscanner.Focus()
            'Txtitcode.Text = ""
            'txtcolor.Text = ""
            'ScanTimer.Start()
        End Try
    End Sub

    Private Sub txtscanner_GotFocus(sender As Object, e As EventArgs) Handles txtscanner.GotFocus


        ' If txtscanner.Text.Trim() = "" Then

        'StartCameraDelayed()
        ' End If

    End Sub


    Private Sub ShowCameraUnderTextbox()


        'If cameraForm IsNot Nothing AndAlso Not cameraForm.IsDisposed Then
        '    'Try
        '    '    scanner.StopCamera()
        '    '    Btnstop.Enabled = False
        '    '    Btnstart.Enabled = True
        '    '    cameraForm.Close()
        '    'Catch
        '    'End Try
        '    CloseCameraForm()
        'End If

        '**method 3
        'CloseCameraForm()
        '' Create camera form instance
        'cameraForm = New FrmCamera()

        '' Calculate screen coordinates of txtscanner
        'Dim txtScreenLocation As Point = Me.PointToScreen(txtscanner.Parent.PointToScreen(txtscanner.Location))

        '' Optional: offset values
        'Dim offsetX As Integer = 0
        'Dim offsetY As Integer = txtscanner.Height + 5  ' 5px gap below textbox

        '' Set camera form location
        'cameraForm.StartPosition = FormStartPosition.Manual
        'cameraForm.Location = New Point(txtScreenLocation.X + offsetX, txtScreenLocation.Y + offsetY)

        '' Show form
        'cameraForm.Show()

        'scanner = New QRScanner800N(cameraForm.picCamera, Sub(qr)
        '                                                      Invoke(Sub()
        '                                                                 txtscanner.Text = qr
        '                                                             End Sub)
        '                                                  End Sub)
        'scanner.StartCamera()
        'isCameraActive = True


        '**mehod 4
        Try
            ' If camera already active, do nothing
            If isCameraActive AndAlso cameraForm IsNot Nothing AndAlso Not cameraForm.IsDisposed Then
                Exit Sub
            End If

            ' Create new camera form
            cameraForm = New FrmCamera()
            cameraForm.StartPosition = FormStartPosition.Manual

            ' Position under txtscanner
            Dim txtScreenLocation As Point = txtscanner.PointToScreen(Point.Empty)
            cameraForm.Location = New Point(txtScreenLocation.X, txtScreenLocation.Y + txtscanner.Height + 5)
            cameraForm.Show()

            ' Create new scanner and start camera
            scanner = New QRScanner800N(cameraForm.picCamera, Sub(qr)
                                                                  Invoke(Sub()
                                                                             txtscanner.Text = qr
                                                                         End Sub)
                                                              End Sub)
            scanner.StartCamera()
            isCameraActive = True

        Catch ex As Exception
            ' Optional: log exception
        End Try

    End Sub

    Private Sub txtnopack_TextChanged(sender As Object, e As EventArgs) Handles txtnopack.TextChanged

    End Sub

    Private Sub CloseCameraForm()
        Try
            ' Stop scanner
            If scanner IsNot Nothing Then
                scanner.StopCamera()
                scanner = Nothing
            End If

            ' Close camera form
            If cameraForm IsNot Nothing AndAlso Not cameraForm.IsDisposed Then
                cameraForm.Close()
                cameraForm = Nothing
            End If

            isCameraActive = False
        Catch ex As Exception
            ' Optional: log error
        End Try
    End Sub



    '****
    Private Sub ShowOrReuseCamera()
        'If cameraForm Is Nothing OrElse cameraForm.IsDisposed Then
        '    ' Create new form if not exists
        '    cameraForm = New FrmCamera()
        '    cameraForm.StartPosition = FormStartPosition.Manual
        '    Dim txtScreenLocation As Point = txtscanner.PointToScreen(Point.Empty)
        '    cameraForm.Location = New Point(txtScreenLocation.X, txtScreenLocation.Y + txtscanner.Height + 5)
        '    cameraForm.Show()

        '    ' Create scanner
        '    scanner = New QRScanner800N(cameraForm.picCamera, Sub(qr)
        '                                                          Invoke(Sub()
        '                                                                     txtscanner.Text = qr
        '                                                                 End Sub)
        '                                                      End Sub)

        '    scanner.StartCamera()
        'Else
        '    ' If already exists, just show the form
        '    If Not cameraForm.Visible Then cameraForm.Show()
        '    If scanner IsNot Nothing AndAlso Not isCameraActive Then
        '        scanner.StartCamera()
        '    End If
        'End If

        'isCameraActive = True

        '**method 2
        'Try
        '    If cameraForm Is Nothing OrElse cameraForm.IsDisposed Then
        '        ' First time: create form
        '        cameraForm = New FrmCamera()
        '        cameraForm.StartPosition = FormStartPosition.Manual
        '        Dim txtScreenLocation As Point = txtscanner.PointToScreen(Point.Empty)
        '        cameraForm.Location = New Point(txtScreenLocation.X, txtScreenLocation.Y + txtscanner.Height + 5)
        '        cameraForm.Show()

        '        ' Create scanner and start camera
        '        scanner = New QRScanner800N(cameraForm.picCamera, Sub(qr)
        '                                                              Invoke(Sub()
        '                                                                         txtscanner.Text = qr
        '                                                                     End Sub)
        '                                                          End Sub)
        '        scanner.StartCamera()
        '        isCameraActive = True
        '    Else
        '        ' Form already exists
        '        If Not cameraForm.Visible Then cameraForm.Show()

        '        ' Restart camera if not running
        '        If scanner Is Nothing Then
        '            scanner = New QRScanner800N(cameraForm.picCamera, Sub(qr)
        '                                                                  Invoke(Sub()
        '                                                                             txtscanner.Text = qr
        '                                                                         End Sub)
        '                                                              End Sub)
        '            scanner.StartCamera()
        '            isCameraActive = True
        '        Else
        '            ' If scanner exists, ensure camera is running
        '            scanner.StartCamera()
        '            isCameraActive = True
        '        End If
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show("Camera Error: " & ex.Message)
        'End Try
        If isCameraActive Then Exit Sub
        '*method 3
        'StopCamera()

        ' Create new camera form
        cameraForm = New FrmCamera()
        cameraForm.StartPosition = FormStartPosition.Manual

        ' Position under txtscanner
        Dim txtScreenLocation As Point = txtscanner.PointToScreen(Point.Empty)
        cameraForm.Location = New Point(txtScreenLocation.X, txtScreenLocation.Y + txtscanner.Height + 5)
        cameraForm.Show()

        ' Create new scanner and start camera
        scanner = New QRScanner800N(cameraForm.picCamera, Sub(qr)
                                                              Invoke(Sub()
                                                                         txtscanner.Text = qr
                                                                     End Sub)
                                                          End Sub)
        scanner.StartCamera()
        isCameraActive = True



    End Sub

    Private Sub dgas_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgas.CellContentClick

    End Sub

    ' ======== Stop Camera ========
    Private Sub StopCamera()
        Try
            If scanner IsNot Nothing Then
                scanner.StopCamera()
                scanner = Nothing
            End If

            If cameraForm IsNot Nothing AndAlso Not cameraForm.IsDisposed Then
                cameraForm.Hide() ' Keep in memory for reuse
            End If

            isCameraActive = False
            'txtscanner.Focus()
        Catch ex As Exception
            ' Optional logging
        End Try
    End Sub

    Private Sub StartCameraDelayed()
        ' Start camera after short delay to ensure UI is ready
        Dim t As New System.Windows.Forms.Timer()
        t.Interval = 500 ' half-second delay
        AddHandler t.Tick, Sub(sender, e)
                               t.Stop()
                               t.Dispose()

                               If txtscanner.Text.Trim() = "" Then
                                   ShowOrReuseCamera()
                               End If
                           End Sub
        t.Start()
    End Sub


    Private Sub unoopen()
        'serial.NewLine = vbCr
        'Try
        '    serial.Open()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

    End Sub


    'Private Sub serial_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles serial.DataReceived
    '    Try

    '        Dim message As String = serial.ReadLine().Trim()

    '        If message = "Object Detected → Motor Stopped" Then
    '            Me.Invoke(Sub() StartCameraDelayed())

    '            'If message = "OBJECT_DETECTED" Then
    '            ' Stop event received, start QR scan
    '            'Invoke(Sub()
    '            '           'ScanAndPrint()
    '            'End Sub)
    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ''If ArduinoPort IsNot Nothing AndAlso ArduinoPort.IsOpen Then
        ''    'ArduinoPort.WriteLine("MOVE")
        ''    ArduinoPort.WriteLine("No Object → Motor Running")
        ''End If

        'If serial IsNot Nothing AndAlso serial.IsOpen Then
        '    'serial.WriteLine("No Object → Motor Running")
        '    serial.WriteLine("RUN")
        'End If
    End Sub






    Private Sub speedprint2vertbothNewold()
        Dim dirv, mdirv As String

        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"

        dirv = System.AppDomain.CurrentDomain.BaseDirectory()
        mdirv = Trim(dirv) & "nsbarcodEVH.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
        'If chkprndir.Checked = True Then
        '    FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        'Else


        Dim fNum2 As Integer = FileSystem.FreeFile()
        FileSystem.FileOpen(fNum2, mdirv, OpenMode.Output, OpenAccess.Write, OpenShare.Shared, -1)
        'FileSystem.PrintLine(fNum, "This is a line with the Rupee symbol: ₹")
        'FileSystem.FileClose(fNum)



        'FileOpen(1, mdir, OpenMode.Output)
        'End If
        Dim rupeeSymbol As String = ChrW(&H20B9)
        'Dim rupeeSymbol As String = ChrW(&H20B9)



        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then
                'PrintLine(1, TAB(0), DR.Item("firstdet"))

                ' FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                    FileSystem.PrintLine(fNum2, "SIZE 69.10 mm, 45 mm")
                    FileSystem.PrintLine(fNum2, "GAP 45 mm,0")
                Else
                    FileSystem.PrintLine(fNum2, "SIZE 69.10 mm,43 mm")
                    FileSystem.PrintLine(fNum2, "GAP 45 mm,0")
                End If
                FileSystem.PrintLine(fNum2, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum2, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum2, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum2, "SPEED 7")
                FileSystem.PrintLine(fNum2, "SET PEEL OFF")
                FileSystem.PrintLine(fNum2, "SET CUTTER OFF")
                'FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
                End If

                FileSystem.PrintLine(fNum2, "SET TEAR ON")
                FileSystem.PrintLine(fNum2, "CLS")
                FileSystem.PrintLine(fNum2, "BITMAP 520,161,4,24,1,ÿÿÜÿÿÇÌÿ‡ÌþÌüÌøÌð#ÌàsÈÀñÀð ø ü þ ?ÿŒÿÌÿÿÌÿÿÌÿÿïÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                FileSystem.PrintLine(fNum2, TAB(0), "CODEPAGE 1252")

                If Chkshirt.Checked = True Then
                    FileSystem.PrintLine(fNum2, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum2, "TEXT 543,193," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum2, "TEXT 493,13," & """" & "0" & """" & ",90,8,11," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :9,10"
                    FileSystem.PrintLine(fNum2, "TEXT 462,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 430,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 460,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    'FileSystem.PrintLine(fNum, "TEXT 300,8," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 425,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 397,13," & """" & "ROMAN.TTF" & """" & ",90,1,10," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 397,70," & """" & "0" & """" & ",90,9,10," & """" & "SLEEVE" & """")

                    FileSystem.PrintLine(fNum2, "TEXT 398,193," & """" & "0" & """" & ",90,11,20," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 398,260," & """" & "0" & """" & ",90,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 400,310," & """" & "0" & """" & ",90,12,21," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 330,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :1 N" & """")
                    FileSystem.PrintLine(fNum2, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 295,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 330,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 330,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 517,90," & """" & "ROMAN.TTF" & """" & ",90,1,7," & """" & "(Incl.of all Taxes)" & """")

                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 240,12," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    If chkvertfit.Checked = True Then
                        'fit
                        FileSystem.PrintLine(fNum2, "TEXT 270,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(16).Value) & """")
                        'cut
                        FileSystem.PrintLine(fNum2, "TEXT 360,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(17).Value) & """")
                    End If



                ElseIf chkpant.Checked = True Then
                    FileSystem.PrintLine(fNum2, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum2, "TEXT 543,191," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum2, "TEXT 491,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 458,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 425,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 458,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 425,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 280,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 395,13," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 395,75," & """" & "0" & """" & ",90,8,12," & """" & "LENGTH" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 360,13," & """" & "0" & """" & ",90,11,20," & """" & "Code :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 350,95," & """" & "0" & """" & ",90,18,12," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 365,140," & """" & "0" & """" & ",90,12,21," & """" & "(Inch)" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 305,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :1N" & """")
                    FileSystem.PrintLine(fNum2, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 275,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 306,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 306,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 517,90," & """" & "ROMAN.TTF" & """" & ",90,1,7," & """" & "(Incl.of all Taxes)" & """")
                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 249,13," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If
                    FileSystem.PrintLine(fNum2, "TEXT 390,240," & """" & "0" & """" & ",90,12,9," & """" & Trim(Math.Round(Val(dg.Rows(i).Cells(4).Value) * 2.54, 0)) & "cm" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 395,175," & """" & "0" & """" & ",90,11,11," & """" & "Size:" & """")
                ElseIf chktwin.Checked = True Then
                    FileSystem.PrintLine(fNum2, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum2, "TEXT 543,193," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum2, "TEXT 491,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 459,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 430,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 455,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 427,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 270,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 397,13," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 397,75," & """" & "0" & """" & ",90,9,11," & """" & "SLEEVE" & """")

                    FileSystem.PrintLine(fNum2, "TEXT 360,13," & """" & "0" & """" & ",90,11,20," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 360,75," & """" & "0" & """" & ",90,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 360,130," & """" & "0" & """" & ",90,12,21," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :2N" & """")
                    FileSystem.PrintLine(fNum2, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 270,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 517,90," & """" & "ROMAN.TTF" & """" & ",90,1,9," & """" & "(Incl.of all Taxes)" & """")

                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 240,13," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If
                    FileSystem.PrintLine(fNum2, "TEXT  493,220," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & "(1+1) 2N" & """")

                ElseIf chkset.Checked = True Then
                    FileSystem.PrintLine(fNum2, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum2, "TEXT 543,193," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum2, "TEXT 490,13," & """" & "0" & """" & ",90,8,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 460,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 430,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 458,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 425,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 270,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 395,13," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 394,75," & """" & "0" & """" & ",90,9,10," & """" & "SLEEVE" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 360,13," & """" & "0" & """" & ",90,11,20," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum2, "TEXT  360,75," & """" & "0" & """" & ",90,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 360,130," & """" & "0" & """" & ",90,12,21," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :2N" & """")
                    FileSystem.PrintLine(fNum2, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 270,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 517,90," & """" & "ROMAN.TTF" & """" & ",90,1,7," & """" & "(Incl.of all Taxes)" & """")
                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 240,13," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum2, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & "" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 182,130," & """" & "0" & """" & ",180,8,9," & """" & "" & """")

                Else
                    FileSystem.PrintLine(fNum2, TAB(0), "TEXT 528,218," & """" & "0" & """" & ",180,13,12," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum2, "TEXT 366,218," & """" & "0" & """" & ",180,14,12," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum2, "TEXT 390,280," & """" & "0" & """" & ",180,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :"
                    FileSystem.PrintLine(fNum2, "TEXT 530,250," & """" & "0" & """" & ",180,10,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 528,313," & """" & "0" & """" & ",180,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 447,250," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 407,313," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 170,188," & """" & "0" & """" & ",180,11,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 93,185," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")

                    FileSystem.PrintLine(fNum2, "TEXT 155,317," & """" & "0" & """" & ",180,9,11," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 99,316," & """" & "0" & """" & ",180,18,11," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 49,317," & """" & "0" & """" & ",180,9,12," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 58,244," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1 N" & """")
                    FileSystem.PrintLine(fNum2, "QRCODE 121,115,L,5,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 528,178," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 524,115," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 465,110," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 215,212," & """" & "ROMAN.TTF" & """" & ",180,8,7," & """" & "(Incl.of all Taxes)" & """")

                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 373,108," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum2, "TEXT  523,281," & """" & "0" & """" & ",180,7,9," & """" & "Commodity Name:" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 527,148," & """" & "0" & """" & ",180,10,9," & """" & "Made in India" & """")

                    FileSystem.PrintLine(fNum2, "TEXT 524,67," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 524,40," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 149,245," & """" & "0" & """" & ",180,9,9," & """" & "Net Qty :" & """")


                    If chkliberty.Checked = True Then
                        FileSystem.PrintLine(fNum2, "TEXT 170,152," & """" & "0" & """" & ",180,10,9," & """" & "LIBERTY CUT" & """")
                    End If

                    FileSystem.PrintLine(fNum2, "TEXT 538,230," & """" & "0" & """" & ",180,8,9," & """" & "CONTENT :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 439,258," & """" & "ROMAN.TTF" & """" & ",180,1,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 430,230," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 182,252," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                        FileSystem.PrintLine(fNum2, "TEXT 109,249," & """" & "0" & """" & ",180,11,10," & """" & "Length" & """")
                        FileSystem.PrintLine(fNum2, "TEXT 182,211," & """" & "0" & """" & ",180,9,17," & """" & "Code:" & """")
                        FileSystem.PrintLine(fNum2, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                        FileSystem.PrintLine(fNum2, "TEXT 63,198," & """" & "0" & """" & ",180,7,10," & """" & "(Inch)" & """")
                    Else
                        FileSystem.PrintLine(fNum2, "TEXT 103,249," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")
                        FileSystem.PrintLine(fNum2, "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """")
                        FileSystem.PrintLine(fNum2, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                        FileSystem.PrintLine(fNum2, "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """")
                    End If


                    If chktwin.Checked = True Then
                        FileSystem.PrintLine(fNum2, "TEXT 172,158," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :2N" & """")
                    ElseIf chkset.Checked = True Then
                        FileSystem.PrintLine(fNum2, "TEXT 172,158," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :" & Val(txtno.Text) & "N" & """")
                    Else
                        FileSystem.PrintLine(fNum2, "TEXT 172,158," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1N" & """")
                    End If

                    FileSystem.PrintLine(fNum2, "QRCODE 519,199,L,3,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 367,159," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 322,131," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 277,131," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 346,313," & """" & "ROMAN.TTF" & """" & ",180,1,7," & """" & "(Incl.of all Taxes)" & """")
                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 538,133," & """" & "0" & """" & ",180,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If
                    If chktwin.Checked = True Then
                        FileSystem.PrintLine(fNum2, "TEXT 182,291," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "(1+1) 2N" & """")
                    End If
                    If chkliberty.Checked = True Then
                        FileSystem.PrintLine(fNum2, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & "LIBERTY CUT" & """")
                    End If


                    If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                        FileSystem.PrintLine(fNum2, "TEXT 120,279," & """" & "0" & """" & ",180,12,9," & """" & (Trim(dg.Rows(i).Cells(15).Value) & " cm") & """")
                        FileSystem.PrintLine(fNum2, "TEXT 182,283," & """" & "0" & """" & ",180,11,11," & """" & "Size:" & """")
                        'FileSystem.PrintLine(fNum, "TEXT 63,279," & """" & "0" & """" & ",180,10,9," & """" & "cm" & """")
                    End If
                End If
                FileSystem.PrintLine(fNum2, "PRINT 1,1")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><end/></xpml>")
                End If

                If Trim(cmbtype.Text) <> "Showroom" And Trim(cmbtype.Text) <> "Franchise" Then
                    speedprinthbothNewold(fNum2, dg, i)

                End If

                dg.Rows(i).Cells(0).Value = False
            End If
        Next
        FileSystem.FileClose(fNum2)
        'FileClose(1)

        If mos = "WIN" Then
            If mcitrix = "Y" Then

                'citrixprint(mdir)
                citrixprint2(mdirv, cmbvprinter.SelectedItem.ToString())
            Else
                Shell("rawprv.bat " & mdirv)
            End If
        Else


            Dim printer As String = tscprinter2
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "nsbarcodEVH.txt"
            PrintTscRaw(printer, filePathname)
            'PrintTscRaw("/dev/usb/lp0", "/home/user/label.txt")


        End If




    End Sub
    Private Sub speedprinthbothNewold(fnum As Integer, dg As DataGridView, i As Integer)

        'FileSystem.PrintLine(fnum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
        If mos = "WIN" Then
            FileSystem.PrintLine(fnum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
            FileSystem.PrintLine(fnum, "SIZE 69.10 mm, 45 mm")
        Else
            FileSystem.PrintLine(fnum, "SIZE 69.10 mm,43 mm")
            FileSystem.PrintLine(fnum, "GAP 45 mm,0")
        End If


        FileSystem.PrintLine(fnum, "DIRECTION 0,0")
        FileSystem.PrintLine(fnum, "REFERENCE 0,0")
        FileSystem.PrintLine(fnum, "OFFSET 0 mm")
        FileSystem.PrintLine(fnum, "SPEED 7")
        FileSystem.PrintLine(fnum, "SET PEEL OFF")
        FileSystem.PrintLine(fnum, "SET CUTTER OFF")
        'FileSystem.PrintLine(fnum, "SET PARTIAL_CUTTER OFF")
        If mos = "WIN" Then
            FileSystem.PrintLine(fnum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
        End If

        FileSystem.PrintLine(fnum, "SET TEAR ON")
        FileSystem.PrintLine(fnum, "CLS")
        FileSystem.PrintLine(fnum, "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
        FileSystem.PrintLine(fnum, TAB(0), "CODEPAGE 1252")
        FileSystem.PrintLine(fnum, TAB(0), "TEXT 433,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP Rs." & """")

        FileSystem.PrintLine(fnum, "TEXT 277,339," & """" & "0" & """" & ",180,14,12," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
        FileSystem.PrintLine(fnum, "TEXT 538,291," & """" & "0" & """" & ",180,11,12," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
        FileSystem.PrintLine(fnum, "TEXT 538,258," & """" & "0" & """" & ",180,9,9," & """" & "COLOUR :" & """")
        FileSystem.PrintLine(fnum, "TEXT 538,230," & """" & "0" & """" & ",180,8,9," & """" & "COMMODITY :" & """")
        FileSystem.PrintLine(fnum, "TEXT 439,258," & """" & "ROMAN.TTF" & """" & ",180,1,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")

        FileSystem.PrintLine(fnum, "TEXT 410,230," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
        FileSystem.PrintLine(fnum, "TEXT 367,189," & """" & "0" & """" & ",180,10,8," & """" & "Made in India" & """")


        FileSystem.PrintLine(fnum, "TEXT 182,252," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")


        If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
            FileSystem.PrintLine(fnum, "TEXT 109,249," & """" & "0" & """" & ",180,11,10," & """" & "Length" & """")
            FileSystem.PrintLine(fnum, "TEXT 182,211," & """" & "0" & """" & ",180,9,17," & """" & "Code:" & """")
            FileSystem.PrintLine(fnum, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
            FileSystem.PrintLine(fnum, "TEXT 63,198," & """" & "0" & """" & ",180,7,10," & """" & "(Inch)" & """")
        Else
            FileSystem.PrintLine(fnum, "TEXT 103,249," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")
            FileSystem.PrintLine(fnum, "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """")
            FileSystem.PrintLine(fnum, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
            FileSystem.PrintLine(fnum, "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """")
        End If
        If chktwin.Checked = True Then
            FileSystem.PrintLine(fnum, "TEXT 182,160," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :2N" & """")
        ElseIf chkset.Checked = True Then
            FileSystem.PrintLine(fnum, "TEXT 182,160," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :" & Val(txtno.Text) & "N" & """")
        Else
            FileSystem.PrintLine(fnum, "TEXT 182,160," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1N" & """")
        End If

        FileSystem.PrintLine(fnum, "QRCODE 519,199,L,3,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
        FileSystem.PrintLine(fnum, "TEXT 367,159," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
        FileSystem.PrintLine(fnum, "TEXT 322,131," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
        FileSystem.PrintLine(fnum, "TEXT 277,131," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
        FileSystem.PrintLine(fnum, "TEXT 346,313," & """" & "ROMAN.TTF" & """" & ",180,1,7," & """" & "(Incl.of all Taxes)" & """")
        If typeText <> "Dealer" And typeText <> "TN" Then
            FileSystem.PrintLine(fnum, "TEXT 538,133," & """" & "0" & """" & ",180,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
        End If
        If chktwin.Checked = True Then
            FileSystem.PrintLine(fnum, "TEXT 182,291," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "(1+1) 2N" & """")
        End If

        If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
            FileSystem.PrintLine(fnum, "TEXT 120,279," & """" & "0" & """" & ",180,12,9," & """" & (Trim(dg.Rows(i).Cells(15).Value) & " cm") & """")
            FileSystem.PrintLine(fnum, "TEXT 182,283," & """" & "0" & """" & ",180,11,11," & """" & "Size:" & """")
            'FileSystem.PrintLine(fNum, "TEXT 63,279," & """" & "0" & """" & ",180,10,9," & """" & "cm" & """")
        End If
        If Chkshirt.Checked = True Then
            FileSystem.PrintLine(fnum, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & Trim(dg.Rows(i).Cells(16).Value) & """")
            FileSystem.PrintLine(fnum, "TEXT 182,130," & """" & "0" & """" & ",180,8,9," & """" & Trim(dg.Rows(i).Cells(17).Value) & """")
        End If

        If chkmfg.Checked = False Then
            FileSystem.PrintLine(fnum, "TEXT 464,107," & """" & "0" & """" & ",180,8,7," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
            FileSystem.PrintLine(fnum, "TEXT 444,85," & """" & "0" & """" & ",180,8,7," & """" & "address details are available in the box" & """")
        End If
        FileSystem.PrintLine(fnum, "PRINT 1,1")
        If mos = "WIN" Then
            FileSystem.PrintLine(fnum, "<xpml></page></xpml><xpml><end/></xpml>")
        End If
    End Sub


    Private Sub speedprint2vertbothNew()
        Dim dirv, mdirv As String

        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"

        dirv = System.AppDomain.CurrentDomain.BaseDirectory()
        mdirv = Trim(dirv) & "nsbarcodEVH.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
        'If chkprndir.Checked = True Then
        '    FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        'Else


        Dim fNum2 As Integer = FileSystem.FreeFile()
        FileSystem.FileOpen(fNum2, mdirv, OpenMode.Output, OpenAccess.Write, OpenShare.Shared, -1)
        'FileSystem.PrintLine(fNum, "This is a line with the Rupee symbol: ₹")
        'FileSystem.FileClose(fNum)



        'FileOpen(1, mdir, OpenMode.Output)
        'End If
        Dim rupeeSymbol As String = ChrW(&H20B9)
        'Dim rupeeSymbol As String = ChrW(&H20B9)



        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then
                'PrintLine(1, TAB(0), DR.Item("firstdet"))

                ' FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                    FileSystem.PrintLine(fNum2, "SIZE 69.10 mm, 45 mm")
                    FileSystem.PrintLine(fNum2, "GAP 45 mm,0")
                Else
                    FileSystem.PrintLine(fNum2, "SIZE 69.10 mm,43 mm")
                    FileSystem.PrintLine(fNum2, "GAP 45 mm,0")
                End If
                FileSystem.PrintLine(fNum2, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum2, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum2, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum2, "SPEED 7")
                FileSystem.PrintLine(fNum2, "SET PEEL OFF")
                FileSystem.PrintLine(fNum2, "SET CUTTER OFF")
                'FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
                End If

                FileSystem.PrintLine(fNum2, "SET TEAR ON")
                FileSystem.PrintLine(fNum2, "CLS")
                FileSystem.PrintLine(fNum2, "BITMAP 515,161,4,24,1,ÿÿÜÿÿÇÌÿ‡ÌþÌüÌøÌð#ÌàsÈÀñÀð ø ü þ ?ÿŒÿÌÿÿÌÿÿÌÿÿïÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                FileSystem.PrintLine(fNum2, TAB(0), "CODEPAGE 1252")

                If Chkshirt.Checked = True Then
                    FileSystem.PrintLine(fNum2, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "       MRP " & """")
                    FileSystem.PrintLine(fNum2, "TEXT 543,193," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum2, "TEXT 493,13," & """" & "0" & """" & ",90,8,11," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :9,10"
                    FileSystem.PrintLine(fNum2, "TEXT 462,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 430,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 460,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    'FileSystem.PrintLine(fNum, "TEXT 300,8," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 425,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 397,13," & """" & "ROMAN.TTF" & """" & ",90,1,10," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 397,80," & """" & "0" & """" & ",90,9,10," & """" & "SLEEVE" & """")

                    FileSystem.PrintLine(fNum2, "TEXT 398,193," & """" & "0" & """" & ",90,11,20," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 398,260," & """" & "0" & """" & ",90,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 400,310," & """" & "0" & """" & ",90,12,21," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 330,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :1 N" & """")
                    FileSystem.PrintLine(fNum2, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    'FileSystem.PrintLine(fNum2, "TEXT 295,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """") 'mfg date
                    FileSystem.PrintLine(fNum2, "TEXT 330,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 330,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 515,115," & """" & "ROMAN.TTF" & """" & ",90,1,7," & """" & "(Incl.of all Taxes)" & """")

                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 240,12," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    If chkvertfit.Checked = True Then
                        'fit
                        FileSystem.PrintLine(fNum2, "TEXT 270,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(16).Value) & """")
                        'cut
                        FileSystem.PrintLine(fNum2, "TEXT 360,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(17).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum2, "TEXT 110,13," & """" & "0" & """" & ",90,7,9," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 85,70," & """" & "0" & """" & ",90,8,9," & """" & "address details are available in the box" & """")

                    FileSystem.PrintLine(fNum2, "Text 60, 75," & """" & "0" & """" & ", 90, 8, 9," & """" & "on the back side of the tag" & """")
                    FileSystem.PrintLine(fNum2, "Text 230, 13," & """" & "0" & """" & ", 90, 7, 9," & """" & "_____________________________________" & """")
                    FileSystem.PrintLine(fNum2, "Text 208, 10," & """" & "0" & """" & ", 90, 7, 26," & """" & "|" & """")
                    FileSystem.PrintLine(fNum2, "Text 194, 25," & """" & "0" & """" & ", 90, 8, 10," & """" & "Consumer is free to open and inspect" & """")
                    FileSystem.PrintLine(fNum2, "Text 169, 60," & """" & "0" & """" & ", 90, 8, 10," & "the product before buying it" & """")
                    FileSystem.PrintLine(fNum2, "Text 159, 13," & """" & "0" & """" & ", 90, 7, 9," & """" & "_____________________________________" & """")
                    FileSystem.PrintLine(fNum2, "Text 208, 339," & """" & "0" & """" & ", 90, 7, 26," & """" & "|" & """")



                ElseIf chkpant.Checked = True Then
                    FileSystem.PrintLine(fNum2, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum2, "TEXT 543,191," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum2, "TEXT 491,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 458,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 425,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 458,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 425,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 280,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 395,13," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 395,75," & """" & "0" & """" & ",90,8,12," & """" & "LENGTH" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 360,13," & """" & "0" & """" & ",90,11,20," & """" & "Code :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 350,95," & """" & "0" & """" & ",90,18,12," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 365,140," & """" & "0" & """" & ",90,12,21," & """" & "(Inch)" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 305,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :1N" & """")
                    FileSystem.PrintLine(fNum2, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 275,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 306,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 306,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 517,90," & """" & "ROMAN.TTF" & """" & ",90,1,7," & """" & "(Incl.of all Taxes)" & """")
                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 249,13," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If
                    FileSystem.PrintLine(fNum2, "TEXT 390,240," & """" & "0" & """" & ",90,12,9," & """" & Trim(Math.Round(Val(dg.Rows(i).Cells(4).Value) * 2.54, 0)) & "cm" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 395,175," & """" & "0" & """" & ",90,11,11," & """" & "Size:" & """")
                ElseIf chktwin.Checked = True Then
                    FileSystem.PrintLine(fNum2, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum2, "TEXT 543,193," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum2, "TEXT 491,13," & """" & "0" & """" & ",90,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 459,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 430,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 455,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 427,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 270,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 397,13," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 397,75," & """" & "0" & """" & ",90,9,11," & """" & "SLEEVE" & """")

                    FileSystem.PrintLine(fNum2, "TEXT 360,13," & """" & "0" & """" & ",90,11,20," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 360,75," & """" & "0" & """" & ",90,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 360,130," & """" & "0" & """" & ",90,12,21," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :2N" & """")
                    FileSystem.PrintLine(fNum2, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 270,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 517,90," & """" & "ROMAN.TTF" & """" & ",90,1,9," & """" & "(Incl.of all Taxes)" & """")

                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 240,13," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If
                    FileSystem.PrintLine(fNum2, "TEXT  493,220," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & "(1+1) 2N" & """")

                ElseIf chkset.Checked = True Then
                    FileSystem.PrintLine(fNum2, TAB(0), "TEXT 543,31," & """" & "0" & """" & ",90,12,11," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum2, "TEXT 543,193," & """" & "0" & """" & ",90,12,11," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum2, "TEXT 490,13," & """" & "0" & """" & ",90,8,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 460,13," & """" & "0" & """" & ",90,8,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 430,13," & """" & "0" & """" & ",90,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 458,80," & """" & "ROMAN.TTF" & """" & ",90,1,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 425,120," & """" & "0" & """" & ",90,8,8," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 270,13," & """" & "0" & """" & ",90,7,7," & """" & "Made in India" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 395,13," & """" & "ROMAN.TTF" & """" & ",90,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 394,75," & """" & "0" & """" & ",90,9,10," & """" & "SLEEVE" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 360,13," & """" & "0" & """" & ",90,11,20," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum2, "TEXT  360,75," & """" & "0" & """" & ",90,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 360,130," & """" & "0" & """" & ",90,12,21," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,13," & """" & "0" & """" & ",90,7,8," & """" & "Net Qty :2N" & """")
                    FileSystem.PrintLine(fNum2, "QRCODE 336,243,L,4,A,90,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 270,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 300,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 517,90," & """" & "ROMAN.TTF" & """" & ",90,1,7," & """" & "(Incl.of all Taxes)" & """")
                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 240,13," & """" & "0" & """" & ",90,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum2, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & "" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 182,130," & """" & "0" & """" & ",180,8,9," & """" & "" & """")

                Else
                    FileSystem.PrintLine(fNum2, TAB(0), "TEXT 528,218," & """" & "0" & """" & ",180,13,12," & """" & "MRP Rs." & """")
                    FileSystem.PrintLine(fNum2, "TEXT 366,218," & """" & "0" & """" & ",180,14,12," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
                    FileSystem.PrintLine(fNum2, "TEXT 390,280," & """" & "0" & """" & ",180,9,10," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    'TEXT 538,258,"0",180,9,9,"COLOUR :"
                    FileSystem.PrintLine(fNum2, "TEXT 530,250," & """" & "0" & """" & ",180,10,9," & """" & "Colour :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 528,313," & """" & "0" & """" & ",180,8,9," & """" & "Commodity :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 447,250," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 407,313," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 170,188," & """" & "0" & """" & ",180,11,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 93,185," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")

                    FileSystem.PrintLine(fNum2, "TEXT 155,317," & """" & "0" & """" & ",180,9,11," & """" & "Size:" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 99,316," & """" & "0" & """" & ",180,18,11," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 49,317," & """" & "0" & """" & ",180,9,12," & """" & "cm" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 58,244," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1 N" & """")
                    FileSystem.PrintLine(fNum2, "QRCODE 121,115,L,5,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 528,178," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 524,115," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 465,110," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 215,212," & """" & "ROMAN.TTF" & """" & ",180,8,7," & """" & "(Incl.of all Taxes)" & """")

                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 373,108," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If

                    FileSystem.PrintLine(fNum2, "TEXT  523,281," & """" & "0" & """" & ",180,7,9," & """" & "Commodity Name:" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 527,148," & """" & "0" & """" & ",180,10,9," & """" & "Made in India" & """")

                    FileSystem.PrintLine(fNum2, "TEXT 524,67," & """" & "0" & """" & ",180,7,8," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 524,40," & """" & "0" & """" & ",180,7,8," & """" & "address details are available in the box" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 149,245," & """" & "0" & """" & ",180,9,9," & """" & "Net Qty :" & """")


                    If chkliberty.Checked = True Then
                        FileSystem.PrintLine(fNum2, "TEXT 170,152," & """" & "0" & """" & ",180,10,9," & """" & "LIBERTY CUT" & """")
                    End If

                    FileSystem.PrintLine(fNum2, "TEXT 538,230," & """" & "0" & """" & ",180,8,9," & """" & "CONTENT :" & """")
                    FileSystem.PrintLine(fNum2, "TEXT 439,258," & """" & "ROMAN.TTF" & """" & ",180,1,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 430,230," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 182,252," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                        FileSystem.PrintLine(fNum2, "TEXT 109,249," & """" & "0" & """" & ",180,11,10," & """" & "Length" & """")
                        FileSystem.PrintLine(fNum2, "TEXT 182,211," & """" & "0" & """" & ",180,9,17," & """" & "Code:" & """")
                        FileSystem.PrintLine(fNum2, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                        FileSystem.PrintLine(fNum2, "TEXT 63,198," & """" & "0" & """" & ",180,7,10," & """" & "(Inch)" & """")
                    Else
                        FileSystem.PrintLine(fNum2, "TEXT 103,249," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")
                        FileSystem.PrintLine(fNum2, "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """")
                        FileSystem.PrintLine(fNum2, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                        FileSystem.PrintLine(fNum2, "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """")
                    End If


                    If chktwin.Checked = True Then
                        FileSystem.PrintLine(fNum2, "TEXT 172,158," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :2N" & """")
                    ElseIf chkset.Checked = True Then
                        FileSystem.PrintLine(fNum2, "TEXT 172,158," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :" & Val(txtno.Text) & "N" & """")
                    Else
                        FileSystem.PrintLine(fNum2, "TEXT 172,158," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1N" & """")
                    End If

                    FileSystem.PrintLine(fNum2, "QRCODE 519,199,L,3,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 367,159," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 322,131," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 277,131," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 346,313," & """" & "ROMAN.TTF" & """" & ",180,1,7," & """" & "(Incl.of all Taxes)" & """")
                    If typeText <> "Dealer" And typeText <> "TN" Then
                        FileSystem.PrintLine(fNum2, "TEXT 538,133," & """" & "0" & """" & ",180,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    End If
                    If chktwin.Checked = True Then
                        FileSystem.PrintLine(fNum2, "TEXT 182,291," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "(1+1) 2N" & """")
                    End If
                    If chkliberty.Checked = True Then
                        FileSystem.PrintLine(fNum2, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & "LIBERTY CUT" & """")
                    End If


                    If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
                        FileSystem.PrintLine(fNum2, "TEXT 120,279," & """" & "0" & """" & ",180,12,9," & """" & (Trim(dg.Rows(i).Cells(15).Value) & " cm") & """")
                        FileSystem.PrintLine(fNum2, "TEXT 182,283," & """" & "0" & """" & ",180,11,11," & """" & "Size:" & """")
                        'FileSystem.PrintLine(fNum, "TEXT 63,279," & """" & "0" & """" & ",180,10,9," & """" & "cm" & """")
                    End If
                End If
                FileSystem.PrintLine(fNum2, "PRINT 1,1")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><end/></xpml>")
                End If

                If Trim(cmbtype.Text) <> "Showroom" And Trim(cmbtype.Text) <> "Franchise" Then
                    speedprinthbothNew(fNum2, dg, i)

                End If

                dg.Rows(i).Cells(0).Value = False
            End If
        Next
        FileSystem.FileClose(fNum2)
        'FileClose(1)

        If mos = "WIN" Then
            If mcitrix = "Y" Then

                'citrixprint(mdir)
                citrixprint2(mdirv, cmbvprinter.SelectedItem.ToString())
            Else
                Shell("rawprv.bat " & mdirv)
            End If
        Else
            'Dim printer As String = mprinter
            'Dim filePath As String = mlinpath
            'Dim filePathname As String = mlinpath & "nsbarcodEVH.txt"
            ''psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

            'Dim psi As New ProcessStartInfo()
            'psi.FileName = "/bin/bash"
            'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

            'psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
            'psi.UseShellExecute = False
            'psi.CreateNoWindow = True
            ''TextBox1.Text = psi.FileName & psi.Arguments
            'Process.Start(psi)


            'Dim printer As String = mprinter
            'Dim filePath As String = mlinpath
            'Dim filePathname As String = mlinpath & "nsbarcodEVH.txt"
            ''psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

            'Dim psi As New ProcessStartInfo()
            'psi.FileName = "/bin/bash"
            'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""
            ''psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
            'psi.UseShellExecute = False
            'psi.CreateNoWindow = True

            'Process.Start(psi)

            Dim printer As String = tscprinter2
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "nsbarcodEVH.txt"
            PrintTscRaw(printer, filePathname)
            'PrintTscRaw("/dev/usb/lp0", "/home/user/label.txt")


        End If




    End Sub
    Private Sub speedprinthbothNew(fnum As Integer, dg As DataGridView, i As Integer)

        'FileSystem.PrintLine(fnum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
        If mos = "WIN" Then
            FileSystem.PrintLine(fnum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
            FileSystem.PrintLine(fnum, "SIZE 69.10 mm, 45 mm")
        Else
            FileSystem.PrintLine(fnum, "SIZE 69.10 mm,43 mm")
            FileSystem.PrintLine(fnum, "GAP 45 mm,0")
        End If


        FileSystem.PrintLine(fnum, "DIRECTION 0,0")
        FileSystem.PrintLine(fnum, "REFERENCE 0,0")
        FileSystem.PrintLine(fnum, "OFFSET 0 mm")
        FileSystem.PrintLine(fnum, "SPEED 7")
        FileSystem.PrintLine(fnum, "SET PEEL OFF")
        FileSystem.PrintLine(fnum, "SET CUTTER OFF")
        'FileSystem.PrintLine(fnum, "SET PARTIAL_CUTTER OFF")
        If mos = "WIN" Then
            FileSystem.PrintLine(fnum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
        End If

        FileSystem.PrintLine(fnum, "SET TEAR ON")
        FileSystem.PrintLine(fnum, "CLS")
        FileSystem.PrintLine(fnum, "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
        FileSystem.PrintLine(fnum, TAB(0), "CODEPAGE 1252")
        FileSystem.PrintLine(fnum, TAB(0), "TEXT 433,339," & """" & "0" & """" & ",180,13,12," & """" & "     MRP" & """")

        FileSystem.PrintLine(fnum, "TEXT 277,339," & """" & "0" & """" & ",180,14,12," & """" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & """")
        FileSystem.PrintLine(fnum, "TEXT 538,291," & """" & "0" & """" & ",180,11,12," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
        FileSystem.PrintLine(fnum, "TEXT 538,258," & """" & "0" & """" & ",180,9,9," & """" & "COLOUR :" & """")
        FileSystem.PrintLine(fnum, "TEXT 538,230," & """" & "0" & """" & ",180,8,9," & """" & "COMMODITY :" & """")
        FileSystem.PrintLine(fnum, "TEXT 439,258," & """" & "ROMAN.TTF" & """" & ",180,1,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")

        FileSystem.PrintLine(fnum, "TEXT 410,230," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
        FileSystem.PrintLine(fnum, "TEXT 367,189," & """" & "0" & """" & ",180,10,8," & """" & "Made in India" & """")


        FileSystem.PrintLine(fnum, "TEXT 182,252," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")


        If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
            FileSystem.PrintLine(fnum, "TEXT 109,249," & """" & "0" & """" & ",180,11,10," & """" & "Length" & """")
            FileSystem.PrintLine(fnum, "TEXT 182,211," & """" & "0" & """" & ",180,9,17," & """" & "Code:" & """")
            FileSystem.PrintLine(fnum, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
            FileSystem.PrintLine(fnum, "TEXT 63,198," & """" & "0" & """" & ",180,7,10," & """" & "(Inch)" & """")
        Else
            FileSystem.PrintLine(fnum, "TEXT 103,249," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")
            FileSystem.PrintLine(fnum, "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """")
            FileSystem.PrintLine(fnum, "TEXT 119,216," & """" & "0" & """" & ",180,18,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
            FileSystem.PrintLine(fnum, "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """")
        End If
        If chktwin.Checked = True Then
            FileSystem.PrintLine(fnum, "TEXT 182,160," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :2N" & """")
        ElseIf chkset.Checked = True Then
            FileSystem.PrintLine(fnum, "TEXT 182,160," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :" & Val(txtno.Text) & "N" & """")
        Else
            FileSystem.PrintLine(fnum, "TEXT 182,160," & """" & "0" & """" & ",180,11,9," & """" & "Net Qty :1N" & """")
        End If

        FileSystem.PrintLine(fnum, "QRCODE 519,199,L,3,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
        'FileSystem.PrintLine(fnum, "TEXT 367,159," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """") 'mfg date
        FileSystem.PrintLine(fnum, "TEXT 322,131," & """" & "0" & """" & ",180,18,12," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
        FileSystem.PrintLine(fnum, "TEXT 277,131," & """" & "0" & """" & ",180,9,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
        FileSystem.PrintLine(fnum, "TEXT 346,313," & """" & "ROMAN.TTF" & """" & ",180,1,7," & """" & "(Incl.of all Taxes)" & """")
        If typeText <> "Dealer" And typeText <> "TN" Then
            FileSystem.PrintLine(fnum, "TEXT 538,133," & """" & "0" & """" & ",180,9,7," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
        End If
        If chktwin.Checked = True Then
            FileSystem.PrintLine(fnum, "TEXT 182,291," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & "(1+1) 2N" & """")
        End If

        If chkpant.Checked = True Or dg.Rows(i).Cells(15).Value.ToString.Trim.Length > 0 Then
            FileSystem.PrintLine(fnum, "TEXT 120,279," & """" & "0" & """" & ",180,12,9," & """" & (Trim(dg.Rows(i).Cells(15).Value) & " cm") & """")
            FileSystem.PrintLine(fnum, "TEXT 182,283," & """" & "0" & """" & ",180,11,11," & """" & "Size:" & """")
            'FileSystem.PrintLine(fNum, "TEXT 63,279," & """" & "0" & """" & ",180,10,9," & """" & "cm" & """")
        End If
        If Chkshirt.Checked = True Then
            FileSystem.PrintLine(fnum, "TEXT 182,282," & """" & "0" & """" & ",180,8,9," & """" & Trim(dg.Rows(i).Cells(16).Value) & """")
            FileSystem.PrintLine(fnum, "TEXT 182,130," & """" & "0" & """" & ",180,8,9," & """" & Trim(dg.Rows(i).Cells(17).Value) & """")
        End If

        If chkmfg.Checked = False Then
            FileSystem.PrintLine(fnum, "TEXT 464,45," & """" & "0" & """" & ",180,8,7," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
            FileSystem.PrintLine(fnum, "TEXT 444,23," & """" & "0" & """" & ",180,8,7," & """" & "address details are available in the box" & """")
        End If

        FileSystem.PrintLine(fnum, "Text 543, 136," & """" & "0" & """" & ", 180, 7, 15," & """" & "_________________________________________________________" & """")
        FileSystem.PrintLine(fnum, "Text 551, 99," & """" & "0" & """" & ", 180, 15, 18," & """" & "|" & """")
        FileSystem.PrintLine(fnum, "Text 535, 86," & """" & "0" & """" & ", 180, 7, 10," & """" & "Consumer is free to open and inspect the product before buying it" & """")
        FileSystem.PrintLine(fnum, "Text 543, 89," & """" & "0" & """" & ", 180, 7, 15," & """" & "_________________________________________________________" & """")
        FileSystem.PrintLine(fnum, "Text 41, 99," & """" & "0" & """" & ", 180, 15, 18," & """" & "|" & """")


        FileSystem.PrintLine(fnum, "PRINT 1,1")
        If mos = "WIN" Then
            FileSystem.PrintLine(fnum, "<xpml></page></xpml><xpml><end/></xpml>")
        End If
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim qrry As String
        If OptSales.Checked = True Then
            qrry = "select docentry from rinv7 where docentry=" & Val(lbldocentry.Text)
        ElseIf optdateord.Checked = True Then
            qrry = "select docentry from rdln7 where docentry=" & Val(lbldocentry.Text)
        Else
            qrry = "select docentry from rinv7 where docentry=" & Val(lbldocentry.Text)
        End If

        Dim dtc As DataTable = getDataTable(qrry)
        If dtc.Rows.Count > 0 Then
            MsgBox("Already Exists!")
            savpack()
        Else
            savpack()
        End If

    End Sub

    Private Sub txtscanner_KeyUp(sender As Object, e As KeyEventArgs) Handles txtscanner.KeyUp
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Tab Then
            e.SuppressKeyPress = True
            'ProcessScannerText()
            If txtscanner.Text.Trim() <> "" Then
                ProcessScannerText()
            End If

        End If
    End Sub

    Private Sub txtscanner_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtscanner.PreviewKeyDown
        If e.KeyCode = Keys.Tab Then
            e.IsInputKey = True
        End If
    End Sub

    'Private Sub Dgpk_MouseDown(sender As Object, e As MouseEventArgs) Handles Dgpk.MouseDown
    '    If e.Button = System.Windows.Forms.MouseButtons.Left Then
    '        drag = True
    '        mouseX = System.Windows.Forms.Cursor.Position.X - Dgpk.Left
    '        mouseY = System.Windows.Forms.Cursor.Position.Y - Dgpk.Top
    '    End If
    'End Sub

    'Private Sub Dgpk_MouseMove(sender As Object, e As MouseEventArgs) Handles Dgpk.MouseMove
    '    If drag Then
    '        Dgpk.Left = System.Windows.Forms.Cursor.Position.X - mouseX
    '        Dgpk.Top = System.Windows.Forms.Cursor.Position.Y - mouseY
    '    End If
    'End Sub

    'Private Sub Dgpk_MouseUp(sender As Object, e As MouseEventArgs) Handles Dgpk.MouseUp
    '    drag = False
    'End Sub

    Private Sub loadconsolidatepack()
        dgph.DataSource = Nothing
        Dim dth As New DataTable
        dth.Columns.Add("docentry", GetType(Integer))
        dth.Columns.Add("PackNo")
        dth.Columns.Add("PackageCode")
        dth.Columns.Add("BoxSize")
        dth.Columns.Add("TotalQty", GetType(Integer))
        dth.Columns.Add("TotScanQty", GetType(Integer))
        dth.Columns.Add("Printed", GetType(Integer))


        'Use LINQ to group and sum
        Dim q = From r As DataGridViewRow In Dgpk.Rows
                Where Not r.IsNewRow
                Group r By
                            k1 = r.Cells("docentry").Value,
                            k2 = r.Cells("PackNo").Value,
                            k3 = r.Cells("PackageCode").Value,
                            k4 = r.Cells("BoxSize").Value
                Into grp = Group
                Select New With {
                    .docentry = k1,
                    .PackNo = k2,
                    .PackageCode = k3,
                    .BoxSize = k4,
                    .TotalQty = grp.Sum(Function(x) Convert.ToInt32(x.Cells("Qty").Value)),
                    .TotScanQty = grp.Sum(Function(x) Convert.ToInt32(x.Cells("ScanQty").Value))
                }

        'Fill result to datatable
        For Each item In q
            dth.Rows.Add(item.docentry, item.PackNo, item.PackageCode, item.BoxSize, item.TotalQty, item.TotScanQty)
        Next

        dth.DefaultView.Sort = "PackNo"
        'Show result in destination grid

        dgph.DataSource = dth

        'For Each rw As DataRow In dth.Rows
        '    n = dgph.Rows.Add
        '    'dgph.Rows(n).Cells()
        'Next

        dgph.AllowUserToOrderColumns = False
        If dgph.Columns.Contains("docentry") Then dgph.Columns("docentry").DisplayIndex = 0
        If dgph.Columns.Contains("PackNo") Then dgph.Columns("PackNo").DisplayIndex = 1
        If dgph.Columns.Contains("PackageCode") Then dgph.Columns("PackageCode").DisplayIndex = 2
        If dgph.Columns.Contains("BoxSize") Then dgph.Columns("BoxSize").DisplayIndex = 3
        If dgph.Columns.Contains("TotalQty") Then dgph.Columns("TotalQty").DisplayIndex = 4
        If dgph.Columns.Contains("TotScanQty") Then dgph.Columns("TotScanQty").DisplayIndex = 5
        'If dgph.Columns.Contains("PackageCode") Then dgph.Columns("PackageCode").DisplayIndex = 5
        If dgph.Columns.Contains("Printed") Then dgph.Columns("Printed").DisplayIndex = 6
        If dgph.Columns.Contains("PackageType") Then dgph.Columns("PackageType").DisplayIndex = 7


        'If dgph.Columns.Contains("PackageCode") Then
        '    dgph.Columns.Remove("PackageCode")
        'End If

        'Create ComboBox Column
        Dim cmb As New DataGridViewComboBoxColumn
        cmb.Name = "PackageType"
        cmb.HeaderText = "Package Type"
        cmb.DataPropertyName = "Pkgtype"   'Bind to DataTable column
        'cmb.DataPropertyName = "PackageCode"


        Dim dtpkg As DataTable = getDataTable("select pkgtype,pkgcode from opkg")

        If Not dgph.Columns.Contains("PackageType") Then


            cmb.DataSource = dtpkg
            cmb.ValueMember = "pkgcode"
            cmb.DisplayMember = "pkgtype"
            cmb.DataPropertyName = "pkgcode"

            'Add to grid
            dgph.Columns.Add(cmb)

        Else
            'Dim dtpkg As DataTable = getDataTable("select pkgtype,pkgcode from opkg")
            ' DirectCast(dgph.Columns("PackageType"), DataGridViewComboBoxColumn).DataSource = dtpkg
            cmb = DirectCast(dgph.Columns("PackageType"), DataGridViewComboBoxColumn)
        cmb.DataSource = dtpkg

        End If

        For k As Integer = 0 To dgph.Rows.Count - 1
            'dgph.Rows(k).Cells(7).Value = getpkgtype(Convert.ToInt32(dgph.Rows(k).Cells(2).Value))
            Dim pkgcode As Integer = Convert.ToInt32(dgph.Rows(k).Cells("PackageCode").Value)
            'dgph.Rows(k).Cells(7).Value = pkgcode
            dgph.Rows(k).Cells("PackageType").Value = pkgcode

        Next





    End Sub

    Private Function getpkgtype(pkgcode As Integer) As String
        Dim mstr2 As String
        msql2 = "select pkgtype, pkgcode from opkg where pkgcode=" & pkgcode
        Dim dtp As DataTable = getDataTable(msql2)
        If dtp.Rows.Count > 0 Then
            mstr2 = dtp.Rows(0)("pkgtype")
        Else
            mstr2 = ""
        End If
        Return mstr2
    End Function

    'Function getpkgcode(item As Integer) As Integer
    '    Dim dt = getDataTable("SELECT pkgcode FROM Opkg WHERE item = " & item)
    '    If dt.Rows.Count > 0 Then
    '        Return Convert.ToInt32(dt.Rows(0)("pkgcode"))
    '    End If
    '    Return 0
    'End Function


    Private Sub dgph_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgph.CellValueChanged

        'If e.RowIndex >= 0 AndAlso dgph.Columns(e.ColumnIndex).Name = "PackageType" Then

        '    Dim selectedValue = dgph.Rows(e.RowIndex).Cells("PackageType").Value

        '    'Write the selected pkgcode back into the real column
        '    dgph.Rows(e.RowIndex).Cells("PackageType").Value = selectedValue

        'End If

        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgph.Rows(e.RowIndex)

            If dgph.Columns(e.ColumnIndex).Name = "PackageType" Then
                Dim selectedValue = dgph.Rows(e.RowIndex).Cells("PackageType").Value

                'Write the selected pkgcode back into the real column
                dgph.Rows(e.RowIndex).Cells("PackageType").Value = selectedValue

            End If

            Dim totalQty As Integer = 0
            Dim totalScanQty As Integer = 0
            Dim printed As Integer = 0

            Integer.TryParse(row.Cells("totalqty").Value?.ToString(), totalQty)
            Integer.TryParse(row.Cells("totscanqty").Value?.ToString(), totalScanQty)
            Integer.TryParse(row.Cells("printed").Value?.ToString(), printed)

            ' Check condition
            If totalQty = totalScanQty AndAlso printed = 0 Then

                ' row.Cells("printed").Value = 1
            End If

        End If

    End Sub

    Private Sub dgph_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgph.CurrentCellDirtyStateChanged

        If dgph.IsCurrentCellDirty Then
            dgph.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If

    End Sub


    Private Sub dgpk_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles Dgpk.CellValueChanged

        'Check if ScanQty column changed
        If Dgpk.Columns(e.ColumnIndex).Name = "ScanQty" Then

            Dim row As DataGridViewRow = Dgpk.Rows(e.RowIndex)

            'Get the pack number from dgpk
            Dim pkno = row.Cells("PackNo").Value

            If pkno IsNot Nothing Then
                'Search dgph for matching PackNo rows
                For Each r As DataGridViewRow In dgph.Rows
                    If Not r.IsNewRow Then
                        If r.Cells("PackNo").Value = pkno Then
                            'Increase TotScanQty by 1
                            Dim oldVal = 0
                            If Not IsDBNull(r.Cells("TotScanQty").Value) Then
                                oldVal = CInt(r.Cells("TotScanQty").Value)
                            End If

                            r.Cells("TotScanQty").Value = oldVal + 1
                        End If
                    End If
                Next
            End If

        End If

    End Sub
    Private Sub dgpk_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles Dgpk.CurrentCellDirtyStateChanged
        If Dgpk.IsCurrentCellDirty Then
            Dgpk.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub


    'Private Sub onlineprintascan()
    '    Dim dir As String
    '    'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
    '    'mdir = Trim(dir) & "\sbarcodE.txt"
    '    Dim mno, rwno As Integer
    '    dir = System.AppDomain.CurrentDomain.BaseDirectory()
    '    mdir = Trim(dir) & "online.txt"

    '    'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
    '    'If chkprndir.Checked = True Then
    '    '    FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
    '    'Else
    '    mno = 1
    '    rwno = 0
    '    Dim fNum As Integer = FileSystem.FreeFile()
    '    FileSystem.FileOpen(fNum, mdir, OpenMode.Output, OpenAccess.Write, OpenShare.Shared, -1)
    '    'FileSystem.PrintLine(fNum, "This is a line with the Rupee symbol: ₹")
    '    'FileSystem.FileClose(fNum)



    '    'FileOpen(1, mdir, OpenMode.Output)
    '    'End If
    '    Dim rupeeSymbol As String = ChrW(&H20B9)
    '    'Dim rupeeSymbol As String = ChrW(&H20B9)
    '    For i As Integer = 0 To dg.Rows.Count - 1
    '        Dim c As Boolean
    '        c = dg.Rows(i).Cells(0).Value
    '        If c = True Then
    '            'PrintLine(1, TAB(0), DR.Item("firstdet"))

    '            FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='23.5 mm'></xpml>SIZE 77.6 mm, 45 mm")
    '            FileSystem.PrintLine(fNum, "DIRECTION 0,0")
    '            FileSystem.PrintLine(fNum, "REFERENCE 0,0")
    '            FileSystem.PrintLine(fNum, "OFFSET 0 mm")
    '            FileSystem.PrintLine(fNum, "SPEED 7")
    '            FileSystem.PrintLine(fNum, "SET PEEL OFF")
    '            FileSystem.PrintLine(fNum, "SET CUTTER OFF")
    '            FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
    '            FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='23.5 mm'></xpml>SET TEAR ON")
    '            FileSystem.PrintLine(fNum, "CLS")
    '            'FileSystem.PrintLine(fNum, "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
    '            '"₹"
    '            'PrintBitmap()
    '            FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")

    '            For j = 1 To Val(dg.Rows(i).Cells(14).Value)
    '                FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(619 - rwno) & ",182," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
    '                FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(619 - rwno) & ",142," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
    '                FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(619 - rwno) & ",107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
    '                FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(619 - rwno) & ",61," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
    '                FileSystem.PrintLine(fNum, "QRCODE " & Str(426 - rwno) & ",156,L,5,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
    '                FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(520 - rwno) & ",107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
    '                FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(495 - rwno) & ",107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
    '                rwno = rwno + 314
    '                If (j Mod 2) = 0 Then
    '                    'If rwno > 314 Then
    '                    FileSystem.PrintLine(fNum, "PRINT 1,1")
    '                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
    '                    rwno = 0
    '                    If j < Val(dg.Rows(i).Cells(14).Value) Then
    '                        FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='23.5 mm'></xpml>SIZE 77.6 mm, 45 mm")
    '                        FileSystem.PrintLine(fNum, "DIRECTION 0,0")
    '                        FileSystem.PrintLine(fNum, "REFERENCE 0,0")
    '                        FileSystem.PrintLine(fNum, "OFFSET 0 mm")
    '                        FileSystem.PrintLine(fNum, "SPEED 7")
    '                        FileSystem.PrintLine(fNum, "SET PEEL OFF")
    '                        FileSystem.PrintLine(fNum, "SET CUTTER OFF")
    '                        FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
    '                        FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='23.5 mm'></xpml>SET TEAR ON")
    '                        FileSystem.PrintLine(fNum, "CLS")
    '                        FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
    '                    End If


    '                End If



    '                If j >= Val(dg.Rows(i).Cells(14).Value) Then
    '                    rwno = 0
    '                    If (j Mod 2) <> 0 Then
    '                        FileSystem.PrintLine(fNum, "PRINT 1,1")
    '                        FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
    '                    End If
    '                    'FileSystem.PrintLine(fNum, "PRINT 1,1")
    '                    'FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
    '                End If
    '            Next j

    '            'FileSystem.PrintLine(fNum, TAB(0), "TEXT 308,182," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
    '            'FileSystem.PrintLine(fNum, TAB(0), "TEXT 308,142," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
    '            'FileSystem.PrintLine(fNum, TAB(0), "TEXT 305,107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
    '            'FileSystem.PrintLine(fNum, TAB(0), "TEXT 308,61," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
    '            'FileSystem.PrintLine(fNum, "QRCODE 110,156,L,5,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
    '            'FileSystem.PrintLine(fNum, TAB(0), "TEXT 215,107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
    '            'FileSystem.PrintLine(fNum, TAB(0), "TEXT 185,107," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")



    '            'FileSystem.PrintLine(fNum, "PRINT 1," & Val(dg.Rows(i).Cells(14).Value))
    '            'FileSystem.PrintLine(fNum, "PRINT 1,1")
    '            'FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")












    '        End If
    '    Next
    '    FileSystem.FileClose(fNum)


    '    If mcitrix = "Y" Then

    '        'citrixprint(mdir)
    '        citrixprint2(mdirv, cmbvprinter.SelectedItem.ToString())
    '    Else
    '        Shell("rawprv.bat " & mdirv)
    '    End If
    'End Sub



    'Private Sub loadecomprnscan()
    '    Dim dir, mdir As String
    '    Dim mtbox, j, txtbno As Integer
    '    Dim sno, lin As Integer
    '    Dim batPath As String = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "rawprv.bat")

    '    dir = System.AppDomain.CurrentDomain.BaseDirectory()
    '    mdir = Trim(dir) & "Qrbarcode.txt"


    '    'Dim dir As String
    '    ''dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
    '    ''mdir = Trim(dir) & "\sbarcodE.txt"

    '    'dir = System.AppDomain.CurrentDomain.BaseDirectory()
    '    'mdir = Trim(dir) & "sbarcodE.txt"

    '    'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)


    '    'If CHKDIRPRN.Checked = True Then
    '    '    FileOpen(1, "LPT" & Trim(TextBox2.Text), OpenMode.Output)
    '    'Else
    '    FileOpen(1, mdir, OpenMode.Output)
    '    'End If




    '    lin = 0
    '    Dim IQR As Integer = 0

    '    'Call MAIN()
    '    'Dim da As New SqlDataAdapter, 
    '    'Dim ds As New DataSet
    '    'Dim da As New OleDb.OleDbDataAdapter
    '    'da.SelectCommand = New OleDb.OleDbCommand
    '    'da.SelectCommand.Connection = con
    '    'da.SelectCommand.CommandType = CommandType.Text
    '    'da.SelectCommand.CommandText = "SELECT LineId FROM [QRIndentity]"
    '    'da.Fill(ds, "tbl2")
    '    'Dim dt As DataTable = ds.Tables("tbl2")
    '    'txtbno = 0
    '    'IQR = dt.Rows(0)("LineId")
    '    sno = 1
    '    PrintLine(1, TAB(0), "^XA")
    '    lin = lin + 1
    '    PrintLine(1, TAB(0), "^PRC")
    '    lin = lin + 1
    '    PrintLine(1, TAB(0), "^LH0,0^FS")
    '    lin = lin + 1
    '    PrintLine(1, TAB(0), "^LL304")
    '    lin = lin + 1
    '    PrintLine(1, TAB(0), "^MD0")
    '    lin = lin + 1
    '    PrintLine(1, TAB(0), "^MNY")
    '    lin = lin + 1
    '    PrintLine(1, TAB(0), "^LH0,0^FS")
    '    lin = lin + 1
    '    'PrintLine(1, TAB(0), "^XA")
    '    'lin = lin + 1

    '    For i As Integer = 0 To dg.Rows.Count - 1
    '        Dim c As Boolean
    '        Dim qtty As Integer
    '        c = dg.Rows(i).Cells(0).Value
    '        'For Each row As DataGridViewRow In dg.Rows
    '        If c = True Then
    '            qtty = Val(dg.Rows(i).Cells(14).Value)
    '            Dim testPos As Integer = InStr(1, dg.Rows(i).Cells(9).Value, "-", CompareMethod.Text)
    '            If testPos > 0 Then
    '                strArrc = dg.Rows(i).Cells(9).Value.ToString.Split("-")
    '                mbarcode = strArrc(1).ToString
    '            End If
    '            Dim testcol As Integer = InStr(1, dg.Rows(i).Cells(5).Value, "-", CompareMethod.Text)
    '            If testcol > 0 Then
    '                strcol = dg.Rows(i).Cells(5).Value.ToString.Split("-")
    '                mkcolor = strcol(1).ToString
    '            End If

    '            For j = 1 To qtty

    '                PrintLine(1, TAB(0), "^FO" & Trim(Str(90 + Val(txtbno))) & ",5^BQN,3,4^FD000" & Trim(mbarcode) & "^FS")
    '                PrintLine(1, TAB(0), "^FO" & Trim(Str(205 + Val(txtbno))) & ",10^A0R,25,25^CI13^FR^FD" & Trim(mkcolor) & "^FS")
    '                PrintLine(1, TAB(0), "^FO" & Trim(Str(185 + Val(txtbno))) & ",10^A0R,20,15^CI13^FR^FD" & Trim(dg.Rows(i).Cells(3).Value) & "^FS ")
    '                PrintLine(1, TAB(0), "^FO" & Trim(Str(185 + Val(txtbno))) & ",50^A0R,20,15^CI13^FR^FD" & Trim(dg.Rows(i).Cells(4).Value) & "^FS")
    '                PrintLine(1, TAB(0), "^FO" & Trim(Str(95 + Val(txtbno))) & ",137^A0N,15,13^CI13^FR^FD" & Trim(dg.Rows(i).Cells(2).Value) & "^FS")
    '                PrintLine(1, TAB(0), "^FO" & Trim(Str(90 + Val(txtbno))) & ",120^A0N,20,15^CI13^FR^FD" & Microsoft.VisualBasic.Format(Val(dg.Rows(i).Cells(12).Value), "######.00") & "^FS")
    '                PrintLine(1, TAB(0), "^FO" & Trim(Str(90 + Val(txtbno))) & ",103^A0N,20,15^CI13^FR^FD" & Trim(mbarcode) & "^FS ")



    '                'Dim delimiter As Char = "/"
    '                'Dim strArr As String() = row.Cells.Item("ITEMNAME").Value.Split(delimiter)


    '                lin = lin + 1

    '                '^FO20,149^A0N,20,20^CI13^FR^FD38F^FS
    '                'IQR = IQR + 1
    '                txtbno = txtbno + 160
    '                If sno = 4 Then
    '                    PrintLine(1, TAB(0), "^PQ1,0,0,N")
    '                    lin = lin + 1
    '                    PrintLine(1, TAB(0), "^XZ")

    '                    PrintLine(1, TAB(0), "^XA")
    '                    lin = lin + 1
    '                    PrintLine(1, TAB(0), "^PRC")
    '                    lin = lin + 1
    '                    PrintLine(1, TAB(0), "^LH0,0^FS")
    '                    lin = lin + 1
    '                    PrintLine(1, TAB(0), "^LL304")
    '                    lin = lin + 1
    '                    PrintLine(1, TAB(0), "^MD0")
    '                    lin = lin + 1
    '                    PrintLine(1, TAB(0), "^MNY")
    '                    lin = lin + 1
    '                    PrintLine(1, TAB(0), "^LH0,0^FS")
    '                    lin = lin + 1
    '                    'PrintLine(1, TAB(0), "^XA")
    '                    'lin = lin + 1
    '                    txtbno = 0
    '                    sno = 0
    '                End If
    '                sno = sno + 1

    '                'PrintLine(1, TAB(0), "^PQ1,0,0,N")
    '                'lin = lin + 1
    '                'PrintLine(1, TAB(0), "^XZ")
    '                ' IQR = IQR + 5

    '            Next j


    '        End If


    '    Next
    '    PrintLine(1, TAB(0), "^PQ1,0,0,N")
    '    lin = lin + 1
    '    PrintLine(1, TAB(0), "^XZ")


    '    If mcitrix = "Y" Then

    '        'citrixprint(mdir)
    '        citrixprint2(mdirv, cmbvprinter.SelectedItem.ToString())
    '    Else
    '        Shell("rawprv.bat " & mdirv)
    '    End If

    'End Sub



    Private Sub savpack()

        Dim commands As New List(Of SqlCommand)
        Dim msdate As DateTime
        Dim msize, mstyle, mpakagetype As String
        msize = ""
        mstyle = ""
        msdate = CDate(Microsoft.VisualBasic.Format(Now, "yyyy-MM-dd"))
        For i = 0 To dgph.Rows.Count - 1
            'If OptSales.Checked = True Then
            '    msql2 = "insert into rinv7(docentry,packagenum,packagetyp,Weight,WeightUnit,objtype,loginstanc,updtdate) Values (" & Val(dgph.Rows(i).Cells(0).Value) & "," & Val(dgph.Rows(i).Cells(1).Value) & ",'" & Trim(dgph.Rows(i).Cells(7).Value) & "',0.000,3,13,0,'" & msdate.ToString("yyyy-MM-dd") & "')"
            'ElseIf optdateord.Checked = True Then
            '    msql2 = "insert into rdln7(docentry,packagenum,packagetyp,Weight,WeightUnit,objtype,loginstanc,updtdate) Values (" & Val(dgph.Rows(i).Cells(0).Value) & "," & Val(dgph.Rows(i).Cells(1).Value) & ",'" & Trim(dgph.Rows(i).Cells(7).Value) & "',0.000,3,13,0,'" & msdate.ToString("yyyy-MM-dd") & "')"
            'End If
            mpakagetype = Trim(getpkgtype(Convert.ToInt32(dgph.Rows(i).Cells("PackageType").Value)))
            If OptSales.Checked = True Then
                'msql2 = "insert into rinv7(docentry,packagenum,packagetyp,Weight,WeightUnit,objtype,loginstanc,updtdate) Values (" & Val(dgph.Rows(i).Cells("docentry").Value) & "," & Val(dgph.Rows(i).Cells("PackNo").Value) & ",'" & Trim(dgph.Rows(i).Cells("PackageType").Value) & "',0.000,3,13,0,'" & msdate.ToString("yyyy-MM-dd") & "')"
                msql2 = "insert into rinv7(docentry,packagenum,packagetyp,Weight,WeightUnit,objtype,loginstanc,updtdate) Values (" & Val(dgph.Rows(i).Cells("docentry").Value) & "," & Val(dgph.Rows(i).Cells("PackNo").Value) & ",'" & mpakagetype & "',0.000,3,13,0,'" & msdate.ToString("yyyy-MM-dd") & "')"
            ElseIf optdateord.Checked = True Then
                'msql2 = "insert into rdln7(docentry,packagenum,packagetyp,Weight,WeightUnit,objtype,loginstanc,updtdate) Values (" & Val(dgph.Rows(i).Cells("docentry").Value) & "," & Val(dgph.Rows(i).Cells("PackNo").Value) & ",'" & Trim(dgph.Rows(i).Cells("PackageType").Value) & "',0.000,3,13,0,'" & msdate.ToString("yyyy-MM-dd") & "')"
                msql2 = "insert into rdln7(docentry,packagenum,packagetyp,Weight,WeightUnit,objtype,loginstanc,updtdate) Values (" & Val(dgph.Rows(i).Cells("docentry").Value) & "," & Val(dgph.Rows(i).Cells("PackNo").Value) & ",'" & mpakagetype & "',0.000,3,13,0,'" & msdate.ToString("yyyy-MM-dd") & "')"
            End If

            Dim cmd As New SqlCommand(msql2)
            commands.Add(cmd)

        Next i


        For k As Integer = 0 To Dgpk.Rows.Count - 1

            Dim dtt As DataTable = getDataTable("select itemcode,isnull(u_style,'') u_style,isnull(u_size,'') u_size from oitm where itemcode='" & Trim(Dgpk.Rows(k).Cells(3).Value) & "'")
            If dtt.Rows.Count > 0 Then
                For Each rw As DataRow In dtt.Rows
                    mstyle = rw("u_style")
                    msize = rw("u_size")
                Next
            Else
                mstyle = ""
                msize = ""
            End If

            If OptSales.Checked = True Then
                msql = "insert into rINV8(DocEntry,PackageNum,ItemCode,Quantity,LogInstanc,ObjType,catalogname,u_style,u_size) values (" & Val(Dgpk.Rows(k).Cells(0).Value) & "," & Val(Dgpk.Rows(k).Cells(2).Value) & ",'" & Trim(Dgpk.Rows(k).Cells(3).Value) & "'," & Val(Dgpk.Rows(k).Cells(5).Value) & ",0,13,'" & Trim(Dgpk.Rows(k).Cells(4).Value) & "','" & Trim(mstyle) & "','" & Trim(msize) & "')"
            ElseIf optdateord.Checked = True Then
                msql = "insert into rdln8(DocEntry,PackageNum,ItemCode,Quantity,LogInstanc,ObjType,catalogname,u_style,u_size) values (" & Val(Dgpk.Rows(k).Cells(0).Value) & "," & Val(Dgpk.Rows(k).Cells(2).Value) & ",'" & Trim(Dgpk.Rows(k).Cells(3).Value) & "'," & Val(Dgpk.Rows(k).Cells(5).Value) & ",0,13,'" & Trim(Dgpk.Rows(k).Cells(4).Value) & "','" & Trim(mstyle) & "','" & Trim(msize) & "')"
            End If
            Dim cmd As New SqlCommand(msql)
            commands.Add(cmd)
        Next k

        Dim result As Boolean = ExecuteTransactionWithCommands(commands)

        If result Then
            MsgBox("All records saved successfully!")

        Else
            MsgBox("Transaction failed. No data saved.")
        End If


    End Sub

    Private Sub dgph_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgph.DataError
        e.ThrowException = False
    End Sub

    Private Sub Btninner_Click(sender As Object, e As EventArgs) Handles Btninner.Click
        innerprintnew2(Convert.ToInt32(lbldocentry.Text), 1)
    End Sub

    Private Sub dgs_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgs.CellContentClick

    End Sub

    Private Sub txtscanner_ParentChanged(sender As Object, e As EventArgs) Handles txtscanner.ParentChanged

    End Sub

    Private Sub txtnopack_KeyUp(sender As Object, e As KeyEventArgs) Handles txtnopack.KeyUp
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Tab Then
            e.SuppressKeyPress = True
            dgas.Rows.Clear()
            'ProcessScannerText()
            If txtnopack.Text.Trim() <> "" Then
                For k As Integer = 1 To Val(txtnopack.Text)
                    n = dgas.Rows.Add()
                    dgas.Rows(n).Cells(0).Value = k
                Next

                Dim dtNew As DataTable
                dtNew = getDataTable("Select pkgcode, pkgtype FROM Opkg")

                'DirectCast(dgas.Columns("BoxSize"), DataGridViewComboBoxColumn).DataSource = dtNew


                Dim col As DataGridViewComboBoxColumn
                col = DirectCast(dgas.Columns("BoxSize"), DataGridViewComboBoxColumn)

                col.DataSource = dtNew
                col.DisplayMember = "pkgtype"   'Text shown in grid
                col.ValueMember = "pkgcode"

            End If

        End If
    End Sub

    Private Sub dgas_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgas.DataError
        e.ThrowException = False
    End Sub

    Private Sub dgas_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgas.CellEndEdit

    End Sub

    Private Sub dgas_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgas.EditingControlShowing
        If TypeOf e.Control Is ComboBox Then
            Dim cmb As ComboBox = DirectCast(e.Control, ComboBox)
            RemoveHandler cmb.SelectionChangeCommitted, AddressOf BoxsizeSelectionChanged
            AddHandler cmb.SelectionChangeCommitted, AddressOf BoxsizeSelectionChanged
        End If
    End Sub

    Private Sub BoxsizeSelectionChanged(sender As Object, e As EventArgs)
        Try
            Dim cmb As ComboBox = DirectCast(sender, ComboBox)
            Dim row As DataGridViewRow = dgas.CurrentRow

            If row IsNot Nothing Then
                Dim selectedValue As String = cmb.SelectedValue.ToString()

                ' Example: Lookup qty based on selected box size
                Dim dt As DataTable = getDataTable("Select pkgcode,pkgtype,CAST(LEFT(pkgtype, PATINDEX('%[^0-9]%', pkgtype + 'X') - 1)   AS INT) AS BoxQty from opkg WHERE pkgcode='" & selectedValue & "'")

                If dt.Rows.Count > 0 Then
                    row.Cells("BoxQty").Value = dt.Rows(0)("boxqty")
                    row.Cells("Qty").Value = dt.Rows(0)("boxqty")
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub loadpackdet(docentry As Integer, headtable As String, dettable As String)

        dgph.DataSource = Nothing
        Dgpk.DataSource = Nothing
        dgas.Rows.Clear()

        Dim dtNew As DataTable
        dtNew = getDataTable("Select pkgcode, pkgtype FROM Opkg")

        'DirectCast(dgas.Columns("BoxSize"), DataGridViewComboBoxColumn).DataSource = dtNew


        Dim col As DataGridViewComboBoxColumn
        col = DirectCast(dgas.Columns("BoxSize"), DataGridViewComboBoxColumn)

        col.DataSource = dtNew
        col.DisplayMember = "pkgtype"   'Text shown in grid
        col.ValueMember = "pkgcode"

        Dim qry As String = "Select b.docentry,b.packagenum PackNo,pkgcode PackageCode,CAST(LEFT(b.packagetyp, PATINDEX('%[^0-9]%', b.packagetyp + 'X') - 1)   AS INT) BoxSize,convert(integer,c.totqty) TotalQty,0 TotScanQty,b.packagetyp,0 Printed from " & headtable & " b
                            inner Join(select docentry, packagenum, sum(quantity) totqty from " & dettable & " group by docentry, packagenum) c on c.docentry=b.docentry And c.PackageNum=b.PackageNum
                            inner join opkg p on p.PkgType=b.packagetyp
                            where b.docentry =" & docentry

        Dim dt1 As DataTable = getDataTable(qry)
        dgph.DataSource = dt1

        'dgph.AllowUserToOrderColumns = False
        If dgph.Columns.Contains("docentry") Then dgph.Columns("docentry").DisplayIndex = 0
        If dgph.Columns.Contains("PackNo") Then dgph.Columns("PackNo").DisplayIndex = 1
        If dgph.Columns.Contains("PackageCode") Then dgph.Columns("PackageCode").DisplayIndex = 2
        If dgph.Columns.Contains("BoxSize") Then dgph.Columns("BoxSize").DisplayIndex = 3
        If dgph.Columns.Contains("TotalQty") Then dgph.Columns("TotalQty").DisplayIndex = 4
        If dgph.Columns.Contains("TotScanQty") Then dgph.Columns("TotScanQty").DisplayIndex = 5
        'If dgph.Columns.Contains("PackageCode") Then dgph.Columns("PackageCode").DisplayIndex = 5
        If dgph.Columns.Contains("Printed") Then dgph.Columns("Printed").DisplayIndex = 6
        If dgph.Columns.Contains("PackageType") Then dgph.Columns("PackageType").DisplayIndex = 7


        'Dim qry1 As String = "Select  b.Docentry,d.pkgcode PackageCode,b.packagenum PackNo,b.ItemCode ,b.Catalogname ItemName,convert(integer,b.quantity) Qty,CAST(LEFT(c.packagetyp, PATINDEX('%[^0-9]%', c.packagetyp + 'X') - 1)   AS INT) BoxSize,0 ScanQty from " & dettable & " b
        '                    inner Join " & headtable & " c on c.docentry=b.docentry And c.PackageNum=b.packagenum
        '                    inner Join opkg d on d.pkgtype=c.PackageTyp
        '                    where b.docentry =" & docentry

        Dim qry1 As String = "Select  b.Docentry,d.pkgcode PackageCode,b.packagenum PackNo,b.ItemCode ,b.Catalogname ItemName,convert(integer,b.quantity) Qty,CAST(LEFT(c.packagetyp, PATINDEX('%[^0-9]%', c.packagetyp + 'X') - 1)   AS INT) BoxSize,0 ScanQty from " & dettable & " b
                            inner Join " & headtable & " c on c.docentry=b.docentry And c.PackageNum=b.packagenum
                            inner join (select docentry,linenum,itemcode,quantity from inv1 where  treetype<>'I') e on e.docentry=b.docentry and e.itemcode=b.itemcode
                            inner Join opkg d on d.pkgtype=c.PackageTyp
                            where b.docentry =" & docentry & " order by e.linenum"


        Dim dt2 As DataTable = getDataTable(qry1)
        Dgpk.DataSource = dt2

        'Dim qry2 As String = " select b.packagenum PackNo,b.packagetyp, convert(integer,c.Totqty) Qty, CAST(LEFT(b.packagetyp, PATINDEX('%[^0-9]%', b.packagetyp + 'X') - 1)   AS INT) BoxQty from " & headtable & "  b
        '                        inner join (select docentry, packagenum,sum(quantity) totqty from " & dettable & " group by docentry,packagenum) c on c.docentry=b.docentry and c.PackageNum=b.PackageNum
        '                        where b.docentry=" & docentry

        Dim qry2 As String = " select b.packagenum PackNo,d.pkgcode, convert(integer,c.Totqty) Qty, CAST(LEFT(b.packagetyp, PATINDEX('%[^0-9]%', b.packagetyp + 'X') - 1)   AS INT) BoxQty from rinv7 b
                                inner join (select docentry, packagenum,sum(quantity) totqty from rinv8 group by docentry,packagenum) c on c.docentry=b.docentry and c.PackageNum=b.PackageNum
                                inner join opkg d on d.pkgtype=b.PackageTyp
                                where b.docentry = " & docentry

        Dim dt3 As DataTable = getDataTable(qry2)
        For Each row As DataRow In dt3.Rows
            n = dgas.Rows.Add
            dgas.Rows(n).Cells(0).Value = row(0)
            dgas.Rows(n).Cells(1).Value = row(1)
            dgas.Rows(n).Cells(2).Value = row(2)
            dgas.Rows(n).Cells(3).Value = row(3)
        Next

        dgformat(Dgpk)

    End Sub

    Private Sub dgformat(dgv As DataGridView)
        dgv.EnableHeadersVisualStyles = False
        dgv.ScrollBars = ScrollBars.Both
        dgv.SelectionMode = DataGridViewSelectionMode.CellSelect
        'dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        'dgv.ColumnHeadersHeight = 120 '

        For Each col As DataGridViewColumn In dgv.Columns

            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            col.HeaderCell.Style.WrapMode = DataGridViewTriState.True
            col.HeaderCell.Style.Font = New Font("Arial", 9, FontStyle.Bold)
            col.HeaderCell.Style.BackColor = Color.SteelBlue
            col.HeaderCell.Style.ForeColor = Color.White

            'If col.Index > 0 Then
            '    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '    col.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
            '    col.DefaultCellStyle.Format = "0"

            'End If
            'If col.Index = 0 Then
            '    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '    col.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
            'End If

            With col.DefaultCellStyle
                If col.Index > 0 Then
                    '.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Font = New Font("Arial", 9, FontStyle.Regular)
                    If col.HeaderText.ToUpper().Contains("QTY") Or col.HeaderText.ToUpper().Contains("QUANTITY") Or col.HeaderText.ToUpper().Contains("TOTALQTY") Then
                        .Format = "0" ' Decimal with 2 digits
                        .Alignment = DataGridViewContentAlignment.MiddleRight
                        'Else
                        '    '.Format = "0" ' Integer
                        '    .Alignment = DataGridViewContentAlignment.MiddleLeft
                    End If
                    '.Format = "0"
                Else
                    .Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Font = New Font("Arial", 9, FontStyle.Regular)
                End If
                If col.HeaderText.Contains("ItemName") Then
                    col.Width = 200
                ElseIf col.HeaderText.Contains("Docentry") Then
                    col.Width = 60
                ElseIf col.HeaderText.Contains("PackNo") Then
                    col.Width = 50
                ElseIf col.HeaderText.Contains("ItemCode") Then
                    col.Width = 100
                ElseIf col.HeaderText.Contains("PackageCode") Then
                    col.Width = 50
                End If

            End With




            col.SortMode = DataGridViewColumnSortMode.NotSortable
            col.ReadOnly = True
        Next
        ColorRowsByPackNo()


    End Sub

    Private Sub txtnopack_HelpRequested(sender As Object, hlpevent As HelpEventArgs) Handles txtnopack.HelpRequested

    End Sub

    Private Sub dgas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgas.CellClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            If TypeOf dgas.Columns(e.ColumnIndex) Is DataGridViewComboBoxColumn Then

                dgas.BeginEdit(True)

                Dim combo As ComboBox = TryCast(dgas.EditingControl, ComboBox)
                If combo IsNot Nothing Then
                    combo.DroppedDown = True   ' 🚀 Open on single click
                End If

            End If
        End If
    End Sub

    Private Sub txtnopack_StyleChanged(sender As Object, e As EventArgs) Handles txtnopack.StyleChanged

    End Sub

    Private Sub txtnopack_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtnopack.PreviewKeyDown
        If e.KeyCode = Keys.Tab Then
            e.IsInputKey = True   ' ✔ Tell textbox to treat TAB as a normal key
        End If
    End Sub

    Private Sub dgph_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgph.CellClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            If TypeOf dgph.Columns(e.ColumnIndex) Is DataGridViewComboBoxColumn Then

                dgph.BeginEdit(True)

                Dim combo As ComboBox = TryCast(dgph.EditingControl, ComboBox)
                If combo IsNot Nothing Then
                    combo.DroppedDown = True   ' 🚀 Open on single click
                End If

            End If
        End If
    End Sub

    Private Sub inner()
        For j As Integer = 0 To dgph.Rows.Count - 1
            If dgph.Rows(j).Cells("TotalQty").Value = dgph.Rows(j).Cells("TotscanQty").Value And dgph.Rows(j).Cells("Printed").Value = 0 Then

            End If
        Next j

    End Sub
    Private Sub innerprintnew(docentry As Integer, packno As Integer)
        ' Dim qry As String = "select ROW_NUMBER() over (partition by packagenum order by packagenum,id) sno,packagenum, itemcode,catalogname itemname,u_style Style,u_size Size,Quantity from rinv8 where docentry=" & docentry & " and packagenum=" & packno
        'Dim qry As String = "select ROW_NUMBER() over (partition by b.packagenum order by b.packagenum,b.id) sno,c.docnum,b.docentry,c.docdate,c.U_Bundle, c.cardcode,c.cardname,b.packagenum, c.U_TransporterName,
        '                    c.U_Destination,c.U_Destion, b.itemcode,b.catalogname itemname,b.u_style Style,b.u_size Size,b.Quantity,sum(b.quantity) over (partition by packagenum) totpcs from rinv8 b
        '                    inner join oinv c with (nolock) on c.docentry=b.docentry
        '                    where b.docentry=" & docentry & " and b.packagenum=" & packno
        Dim sb As New System.Text.StringBuilder()

        Dim qry As String = ""

        If OptSales.Checked = True Then
            qry = "select ROW_NUMBER() over (partition by b.packagenum order by b.packagenum,b.id) sno,c.docnum,b.docentry,c.docdate,d.bundleno U_Bundle, c.cardcode,b.packagenum,c.U_Transport,
                            c.U_Destination,c.U_Destion, b.itemcode,t.u_brandgroup itemname,b.u_style Style,b.u_size Size,b.Quantity,sum(b.quantity) over (partition by packagenum) totpcs,
                            case when isnull(e.u_brch,'')='' then isnull(r.cardfname, c.cardname) else e.u_brch end Cardname,e.building,e.block,e.street,e.city,e.zipcode,e.state,
                            'I-'+ltrim(rtrim(convert(varchar(15),b.docentry)))+'-'+ltrim(convert(varchar(5),b.packagenum)) bundleno,isnull(convert(nvarchar(max),c.u_remarks3),'') remarks,rtrim(isnull(convert(nvarchar(max),br.u_remarks),'')) as stype from rinv8 b
                            inner join oinv c with (nolock) on c.docentry=b.docentry
                            inner join (select max(packagenum) bundleno ,docentry from rinv7  group by docentry) d on d.docentry=c.docentry
                            inner join ocrd r with (nolock) on r.cardcode=c.cardcode  
                            inner join crd1 e on e.cardcode=c.cardcode and e.address=c.ShipToCode
                            inner join oitm t on t.itemcode=b.itemcode 
                            inner join nnm1 s on s.series=c.series
                            inner join [@incm_bnd1] br on br.u_name=c.u_brand
                            where b.docentry=" & docentry & " and b.packagenum=" & packno
        ElseIf optdateord.Checked = True Then
            qry = "select ROW_NUMBER() over (partition by b.packagenum order by b.packagenum,b.id) sno,c.docnum,b.docentry,c.docdate,d.bundleno U_Bundle, c.cardcode,b.packagenum,c.U_Transport,
                            c.U_Destination,c.U_Destion, b.itemcode,t.u_brandgroup itemname,b.u_style Style,b.u_size Size,b.Quantity,sum(b.quantity) over (partition by packagenum) totpcs,
                            case when isnull(e.u_brch,'')='' then isnull(r.cardfname,c.cardname) else e.u_brch end Cardname,e.building,e.block,e.street,e.city,e.zipcode,e.state,
                            'D-'+ltrim(rtrim(convert(varchar(15),b.docentry)))+'-'+ltrim(convert(varchar(5),b.packagenum)) bundleno,isnull(convert(nvarchar(max),c.u_remarks3),'') remarks,rtrim(isnull(convert(nvarchar(max),br.u_remarks),'')) as stype from rdln8 b
                            inner join odln c with (nolock) on c.docentry=b.docentry
                            inner join (select max(packagenum) bundleno ,docentry from rdln7  group by docentry) d on d.docentry=c.docentry 
                            inner join ocrd r with (nolock) on r.cardcode=c.cardcode  
                            inner join crd1 e on e.cardcode=c.cardcode and e.address=c.ShipToCode
                            inner join oitm t on t.itemcode=b.itemcode 
                            inner join nnm1 s on s.series=c.series
                            inner join [@incm_bnd1] br on br.u_name=c.u_brand
                            where b.docentry=" & docentry & " and b.packagenum=" & packno
        End If


        Dim dtc As DataTable = getDataTable(qry)
        If dtc.Rows.Count > 0 Then
            'Dim y As Integer = 3407
            'Dim gap As Integer = 44
            'Dim yy As Integer = 0

            Dim RowHeight As Integer = 44        ' each row height in dots
            Dim HeaderHeight As Integer = 625    ' top portion
            Dim FooterHeight As Integer = 300    ' bottom portion
            Dim TotalRows As Integer = dtc.Rows.Count

            Dim TotalHeight As Integer = HeaderHeight + FooterHeight + (TotalRows * RowHeight)

            Dim newy As Double = Convert.ToInt32((TotalHeight / 8) * 8)
            Dim scale As Single = (newy / 4024)
            Dim x As Integer = 0

            'Dim y As Integer = 3407
            Dim y As Integer = 0
            Dim gap As Integer = 44
            Dim yy As Integer = 0
            Dim ny As Integer = 0
            'Dim sno As Integer = 1

            Dim kcardname As String = Trim(dtc.Rows(0)("cardname"))
            Dim kcity As String = Trim(dtc.Rows(0)("city"))
            Dim kdocnum As Integer = dtc.Rows(0)("docnum")
            Dim kdocentry As Integer = dtc.Rows(0)("docentry")
            Dim ku_bundle As String = dtc.Rows(0)("u_bundle").ToString.Trim
            'Dim kpackagenum As String = "Packing No. : " & dtc.Rows(0)("docentry").ToString.Trim & " / " & dtc.Rows(0)("packagenum").ToString.Trim
            Dim kpackagenum As String = "Packing No. : " & dtc.Rows(0)("docnum").ToString.Trim & " / " & dtc.Rows(0)("u_bundle").ToString.Trim
            Dim ktransport As String = "Transport : " & dtc.Rows(0)("U_Transport").ToString.Trim
            Dim kdocdate As String = "Date : " & CDate(dtc.Rows(0)("docdate")).ToString("dd-MM-yyyy")
            Dim kbundleno As String = dtc.Rows(0)("bundleno").ToString.Trim
            Dim kpackno As String = "Bundle No : " & dtc.Rows(0)("packagenum").ToString.Trim
            Dim ktoday As String = Today.ToShortDateString.ToString()
            Dim ktime As String = TimeOfDay.ToString("hh:mm:ss tt")
            Dim ktotpcs As Integer = dtc.Rows(0)("totpcs")
            Dim kremarks As String = dtc.Rows(0)("remarks")
            Dim ktype As String = "Type : " & dtc.Rows(0)("stype")
            Dim kterms As String = "DO NOT ACCEPT IF STRAPPING TAGS AND BOXES ARE BROKEN"
            'sb.AppendLine("<xpml><page quantity='0' pitch='39.0 mm'></xpml>")
            'sb.AppendLine("SIZE 107.10 mm, 508 mm")
            sb.AppendLine($"SIZE 107.10 mm, {TotalHeight / 8} mm")
            sb.AppendLine("DIRECTION 0,0")
            sb.AppendLine("REFERENCE 0,0")
            sb.AppendLine("OFFSET 0 mm")
            sb.AppendLine("SPEED 7")
            'sb.AppendLine("SET MEDIA TYPE CONTINUOUS")
            'sb.AppendLine("SET BACKFEED 0")
            sb.AppendLine("SET PEEL OFF")
            sb.AppendLine("SET CUTTER OFF")
            'sb.AppendLine("SET PEEL OFF")
            'sb.AppendLine("SET CUTTER OFF")
            ' sb.AppendLine("<xpml></page></xpml><xpml><page quantity='1' pitch='39.0 mm'></xpml>")
            sb.AppendLine("SET TEAR ON")
            sb.AppendLine("CLS")

            sb.AppendLine("CODEPAGE 1252")
            'sb.AppendLine($"TEXT 551,{SY(4024, scale)},""ROMAN.TTF"",180,1,12,""Atithya Clothing Company - Madurai""")
            'sb.AppendLine($"TEXT 804,{SY(4024, scale)},""0"",180,12,12,""Type : SS""")
            'sb.AppendLine($"BAR 7,{SY(3960, scale)},847,3")
            'sb.AppendLine($"TEXT 688,{SY(3955, scale)},""ROMAN.TTF"",180,1,13,""{kcardname}""")
            'sb.AppendLine($"TEXT 754,{SY(3911, scale)},""ROMAN.TTF"",180,1,12,""{kcity}""")
            'sb.AppendLine($"BAR 7,{SY(3855, scale)},847,3")
            'sb.AppendLine($"QRCODE 241,{SY(3828, scale)},L,9,A,180,M2,S7,""{kbundleno}""")
            'sb.AppendLine($"TEXT 830, {SY(3807, scale)}, ""ROMAN.TTF"", 180, 1, 12, ""{kpackagenum}""")
            'sb.AppendLine($"TEXT 828,{SY(3741, scale)},""ROMAN.TTF"",180,1,12,""{kdocdate}""")
            'sb.AppendLine($"TEXT 828,3681,""ROMAN.TTF"",180,1,12,""{kpackno}""")
            'sb.AppendLine($"TEXT 828,{SY(3619, scale)},""ROMAN.TTF"",180,1,12,""{ktransport}""")
            'If Len(Trim(kremarks)) > 0 Then
            '    sb.AppendLine($"ERASE 257,{SY(3485, scale)},348,92")
            '    sb.AppendLine($"TEXT 604,{SY(3576, scale)},""0"",180,16,28,""{kremarks}""")
            '    sb.AppendLine($"REVERSE 257,{SY(3485, scale)},348,92")
            'End If
            'sb.AppendLine($"BAR 9,{SY(3486, scale)}, 847, 3")
            '****
            newy -= 64
            sb.AppendLine($"TEXT 551,{newy},""ROMAN.TTF"",180,1,12,""Atithya Clothing Company - Madurai""")
            sb.AppendLine($"TEXT 804,{newy},""0"",180,12,12,""{ktype}""")
            newy -= 64
            sb.AppendLine($"BAR 3,{newy},880,3")
            newy -= 5
            x = CenterX(kcardname, 13, 107.1, 180)
            'x = CenterXrotat(kcardname, 13, 107.1, True)
            'sb.AppendLine($"TEXT 688,{newy},""ROMAN.TTF"",180,1,13,""{kcardname}""")
            sb.AppendLine($"TEXT {x},{newy},""ROMAN.TTF"",180,1,13,""{kcardname}""")
            newy -= 44
            x = CenterX(kcity, 12, 107.1, 180)
            'x = CenterXrotat(kcity, 13, 107.1, True)
            'sb.AppendLine($"TEXT 754,{newy},""ROMAN.TTF"",180,1,12,""{kcity}""")
            sb.AppendLine($"TEXT {x},{newy},""ROMAN.TTF"",180,1,12,""{kcity}""")
            newy -= 56
            sb.AppendLine($"BAR 3,{newy},880,3")
            newy -= 27
            sb.AppendLine($"QRCODE 241,{newy},L,9,A,180,M2,S7,""{kbundleno}""")
            newy -= 21
            sb.AppendLine($"TEXT 830, {newy}, ""ROMAN.TTF"", 180, 1, 12, ""{kpackagenum}""")
            newy -= 66
            sb.AppendLine($"TEXT 828,{newy},""ROMAN.TTF"",180,1,12,""{kdocdate}""")
            newy -= 60
            sb.AppendLine($"TEXT 828,{newy},""ROMAN.TTF"",180,1,12,""{kpackno}""")
            newy -= 62
            sb.AppendLine($"TEXT 828,{newy},""ROMAN.TTF"",180,1,12,""{ktransport}""")
            'newy -= 134

            If Len(Trim(kremarks)) > 0 Then
                ny = newy - 58
                newy -= 134
                x = CenterX(kremarks, 28, 107.1, 180)
                sb.AppendLine($"ERASE 257,{newy},348,92")
                'ny = newy - 43
                sb.AppendLine($"TEXT 604,{ny},""0"",180,16,28,""{kremarks}""")
                'sb.AppendLine($"TEXT {x},{ny},""0"",180,16,28,""{kremarks}""")
                sb.AppendLine($"REVERSE 20,{newy},800,92")
            Else
                newy -= 43
            End If
            newy += 1
            sb.AppendLine($"BAR 3,{newy}, 880, 3")
            newy -= 27

            'sb.AppendLine("TEXT 838,3311,""ROMAN.TTF"",180,1,12,""Transport : KPG""")
            sb.AppendLine($"TEXT 832,{newy},""ROMAN.TTF"",180,1,11,""S.No""")
            sb.AppendLine($"TEXT 634,{newy},""ROMAN.TTF"",180,1,11,""Item Name""")
            sb.AppendLine($"TEXT 338,{newy},""ROMAN.TTF"",180,1,11,""Style""")
            sb.AppendLine($"TEXT 210,{newy},""ROMAN.TTF"",180,1,11,""Size""")
            sb.AppendLine($"TEXT 65,{newy},""ROMAN.TTF"",180,1,11,""Qty""")
            'sb.AppendLine("TEXT 550,3652,""ROMAN.TTF"",180,1,12,""Atithya Clothing Company - Madurai""")
            newy -= 38
            sb.AppendLine($"BAR 3,{newy}, 880, 3")
            newy -= 14
            y = newy
            For Each rw As DataRow In dtc.Rows
                Dim sno As Int16 = Convert.ToInt16(rw("sno").ToString())
                Dim item As String = rw("itemname").ToString()
                Dim style As String = rw("style").ToString()
                Dim size As String = rw("size").ToString()
                Dim qty As String = Convert.ToInt16(rw("quantity")).ToString()

                ' Format: SNo, Item Name, Style, Size, Qty
                sb.AppendLine($"TEXT 804,{y},""ROMAN.TTF"",180,1,11,""{sno}""")
                sb.AppendLine($"TEXT 716,{y},""ROMAN.TTF"",180,1,11,""{item}""")
                sb.AppendLine($"TEXT 333,{y},""ROMAN.TTF"",180,1,11,""{style}""")
                sb.AppendLine($"TEXT 197,{y},""ROMAN.TTF"",180,1,11,""{size}""")
                sb.AppendLine($"TEXT 59,{y},""ROMAN.TTF"",180,1,11,""{qty}""")

                y -= gap      ' Go to next line
                'sno += 1      ' Increase serial number
            Next
            ' y -= gap

            sb.AppendLine($"BAR 3,{y}, 880, 3")
            'sb.AppendLine("BAR 25,263, 768, 3")
            y -= 10
            sb.AppendLine($"TEXT 499,{y},""ROMAN.TTF"",180,1,11,""Total Pcs....""")
            sb.AppendLine($"TEXT 65,{y},""ROMAN.TTF"",180,1,11,""{ktotpcs}""")
            y -= gap
            y -= gap
            'sb.AppendLine("TEXT 504,232,""ROMAN.TTF"",180,1,11,""Total Pcs....""")
            'sb.AppendLine("TEXT 70,232,""ROMAN.TTF"",180,1,11,""200""")
            yy = y - 63
            'sb.AppendLine($"ERASE 38, {yy}, 780, 64")
            'sb.AppendLine($"TEXT 817, {(y - 15)}, ""0"", 180, 10, 19, ""Do Not ACCEPT If STRAPPING TAGS And BOXES ARE BROKEN""")
            'sb.AppendLine($"REVERSE 38, {yy}, 780, 64")
            'x = CenterX(kterms, 19, 107.1, 180)
            sb.AppendLine($"ERASE 3, {yy}, 880, 64")
            sb.AppendLine($"TEXT 716, {(y - 10)}, ""0"", 180, 10, 19, ""{kterms}""")
            sb.AppendLine($"REVERSE 3, {yy}, 880, 64")

            y -= 73
            sb.AppendLine($"TEXT 794,{y},""ROMAN.TTF"",180,1,11,""{ktoday}""")
            sb.AppendLine($"TEXT 157,{y},""ROMAN.TTF"",180,1,11,""{ktime}""")
            If optdateord.Checked = True Then
                sb.AppendLine($"TEXT 520,{y},""0"",180,12,12,""Date Order""")
            End If

            sb.AppendLine("PRINT 1,1")

            ' Send to printer
            'RawPrinterHelper.SendStringToPrinter("TSC TP244", sb.ToString())
            Dim sfd As New SaveFileDialog()
            sfd.Filter = "Text Files|*.txt"
            sfd.FileName = "testlabel.txt"

            If sfd.ShowDialog() = DialogResult.OK Then
                System.IO.File.WriteAllText(sfd.FileName, sb.ToString())
            End If
        End If
        'sb.AppendLine("TEXT 551," & SY(4024) & ", ...")
        'sb.AppendLine("BAR 7," & SY(3960) & ",847,3")
        'sb.AppendLine("QRCODE 241," & SY(3828) & ",L,9,A,180,M2,S7,""I-865458-2""")

    End Sub

    Private Function SY(y As Integer, scale As Single) As Integer
        'Dim scale As Double = 0.26
        Return CInt(y * scale)
    End Function


    Private Sub innerprintnew2(docentry As Integer, packno As Integer)
        ' Dim qry As String = "select ROW_NUMBER() over (partition by packagenum order by packagenum,id) sno,packagenum, itemcode,catalogname itemname,u_style Style,u_size Size,Quantity from rinv8 where docentry=" & docentry & " and packagenum=" & packno
        'Dim qry As String = "select ROW_NUMBER() over (partition by b.packagenum order by b.packagenum,b.id) sno,c.docnum,b.docentry,c.docdate,c.U_Bundle, c.cardcode,c.cardname,b.packagenum, c.U_TransporterName,
        '                    c.U_Destination,c.U_Destion, b.itemcode,b.catalogname itemname,b.u_style Style,b.u_size Size,b.Quantity,sum(b.quantity) over (partition by packagenum) totpcs from rinv8 b
        '                    inner join oinv c with (nolock) on c.docentry=b.docentry
        '                    where b.docentry=" & docentry & " and b.packagenum=" & packno
        Dim sb As New System.Text.StringBuilder()

        Dim qry As String = ""

        If OptSales.Checked = True Then
            qry = "select ROW_NUMBER() over (partition by b.packagenum order by b.packagenum,b.id) sno,c.docnum,b.docentry,c.docdate,c.U_Bundle, c.cardcode,b.packagenum, c.U_TransporterName,
                            c.U_Destination,c.U_Destion, b.itemcode,t.u_brandgroup itemname,b.u_style Style,b.u_size Size,b.Quantity,sum(b.quantity) over (partition by packagenum) totpcs,
                            case when isnull(e.u_brch,'')='' then c.cardname else e.u_brch end Cardname,e.building,e.block,e.street,e.city,e.zipcode,e.state,
                            'I-'+ltrim(rtrim(convert(varchar(15),b.docentry)))+'-'+ltrim(convert(varchar(5),b.packagenum)) bundleno,isnull(convert(nvarchar(max),c.u_remarks),'') remarks from rinv8 b
                            inner join oinv c with (nolock) on c.docentry=b.docentry
                            inner join crd1 e on e.cardcode=c.cardcode and e.address=c.ShipToCode
                            inner join oitm t on t.itemcode=b.itemcode 
                            where b.docentry=" & docentry & " and b.packagenum=" & packno
        ElseIf optdateord.Checked = True Then
            qry = "select ROW_NUMBER() over (partition by b.packagenum order by b.packagenum,b.id) sno,c.docnum,b.docentry,c.docdate,c.U_Bundle, c.cardcode,b.packagenum, c.U_TransporterName,
                            c.U_Destination,c.U_Destion, b.itemcode,t.u_brandgroup itemname,b.u_style Style,b.u_size Size,b.Quantity,sum(b.quantity) over (partition by packagenum) totpcs,
                            case when isnull(e.u_brch,'')='' then c.cardname else e.u_brch end Cardname,e.building,e.block,e.street,e.city,e.zipcode,e.state,
                            'D-'+ltrim(rtrim(convert(varchar(15),b.docentry)))+'-'+ltrim(convert(varchar(5),b.packagenum)) bundleno,isnull(convert(nvarchar(max),c.u_remarks),'') remarks from rdln8 b
                            inner join odln c with (nolock) on c.docentry=b.docentry
                            inner join crd1 e on e.cardcode=c.cardcode and e.address=c.ShipToCode
                            inner join oitm t on t.itemcode=b.itemcode 
                            where b.docentry=" & docentry & " and b.packagenum=" & packno
        End If


        Dim dtc As DataTable = getDataTable(qry)
        If dtc.Rows.Count > 0 Then
            Dim y As Integer = 3407
            Dim gap As Integer = 44
            Dim yy As Integer = 0

            Dim RowHeight As Integer = 44        ' each row height in dots
            Dim HeaderHeight As Integer = 625    ' top portion
            Dim FooterHeight As Integer = 200    ' bottom portion
            Dim TotalRows As Integer = dtc.Rows.Count

            Dim TotalHeight As Integer = HeaderHeight + FooterHeight + (TotalRows * RowHeight)


            'Dim sno As Integer = 1
            Dim ku_noofbun As String = dtc.Rows(0)("u_bundle").ToString.Trim
            Dim kcardname As String = dtc.Rows(0)("cardname")
            Dim kcity As String = dtc.Rows(0)("city") & "-" & dtc.Rows(0)("zipcode")
            Dim kdocnum As Integer = dtc.Rows(0)("docnum")
            Dim kdocentry As Integer = dtc.Rows(0)("docentry")
            'Dim kpackagenum As String = "Packing No. : " & dtc.Rows(0)("docentry").ToString.Trim & " / " & dtc.Rows(0)("packagenum").ToString.Trim
            Dim kpackagenum As String = "Packing No. : " & dtc.Rows(0)("docnum").ToString.Trim & " / " & dtc.Rows(0)("packagenum").ToString.Trim
            Dim ktransport As String = "Transport : " & dtc.Rows(0)("U_TransporterName").ToString.Trim
            Dim kdocdate As String = "Date : " & CDate(dtc.Rows(0)("docdate")).ToString("dd-MM-yyyy")
            Dim kbundleno As String = dtc.Rows(0)("bundleno").ToString.Trim
            Dim kpackno As String = "Bundle No : " & dtc.Rows(0)("packagenum").ToString.Trim
            Dim ktoday As String = Today.ToShortDateString.ToString()
            Dim ktime As String = TimeOfDay.ToString("hh:mm:ss tt")
            Dim ktotpcs As Integer = dtc.Rows(0)("totpcs")
            Dim kremarks As String = dtc.Rows(0)("remarks")
            'Dim bitmapData As String = File.ReadAllText("logo.txt", Encoding.Latin1)
            'Dim appPath As String = System.Windows.Forms.Application.StartupPath
            'Dim appPath As String = AppDomain.CurrentDomain.BaseDirectory
            'Dim filePath As String = System.IO.Path.Combine(appPath, "logo.txt")
            '
            'sb.AppendLine($"BITMAP 589,3822,27,40,1,{bitmapData}")
            'Dim bytes1 As Byte() = File.ReadAllBytes("logo.txt")
            'Dim hex1 As String = BitConverter.ToString(bytes1).Replace("-", "")

            'Dim bitmapData As String = File.ReadAllText(filePath, Encoding.GetEncoding(28591))

            ' Clean hex data
            'bitmapData = bitmapData.Replace(vbCr, "") _
            '                       .Replace(vbLf, "") _
            '                       .Replace(" ", "")

            'sb.AppendLine("BITMAP 589,3822,27,40,1," & bitmapData)

            'sb.AppendLine("<xpml><page quantity='0' pitch='39.0 mm'></xpml>")
            'sb.AppendLine("SIZE 107.10 mm, 508 mm")
            'sb.AppendLine($"SIZE 107.10 mm, {TotalHeight / 8} mm")

            If mos = "WIN" Then
                sb.AppendLine("SIZE 107.10 mm," & (TotalHeight / 8) & " mm")
            Else
                ' sb.AppendLine("SIZE 107.10," & TotalHeight)
                sb.AppendLine("SIZE 107.10 mm," & (TotalHeight / 8) & " mm")

            End If



            sb.AppendLine("DIRECTION 0,0")
            sb.AppendLine("REFERENCE 0,0")
            sb.AppendLine("OFFSET 0 mm")
            sb.AppendLine("SPEED 7")
            'sb.AppendLine("SET MEDIA TYPE CONTINUOUS")
            'sb.AppendLine("SET BACKFEED 0")
            sb.AppendLine("SET PEEL OFF")
            sb.AppendLine("SET CUTTER OFF")
            'sb.AppendLine("SET PEEL OFF")
            'sb.AppendLine("SET CUTTER OFF")
            ' sb.AppendLine("<xpml></page></xpml><xpml><page quantity='1' pitch='39.0 mm'></xpml>")
            sb.AppendLine("SET TEAR ON")
            sb.AppendLine("CLS")
            'sb.AppendLine("CODEPAGE 1252")
            'sb.AppendLine("TEXT 426,124,""0"",180,13,11,""" & podno & """")
            ' sb.AppendLine("BITMAP 630,3619,27,40,1,                       ÿÿÿ                       ÿÿÿ                       ÿÿÿ                       ÿÿÿ                       ÿÿÿ                       ÿÿÿ þ    €ðà0€   `  ÿÃÿ þ    €ðà0€   `  ÿÃÿ ÿÿÿÿÿÿþóüàÿÏÿÿÿÿ?ðÿÃÿ ÿÿÿÿÿÿþóüàÿÏÿÿÿÿ?ðÿÃÿ ÿÿÿÿÿÿþóüàÿÏÿÿÿÿ?ðÿÃÿÿÿƒóÿÿÿÿóüøÿÏùüüÿÿðÿ ÿÿÿƒóÿÿÿÿóüøÿÏùüüÿÿðÿ ÿÿƒÿÿø ÿÿðÿŸøÿùÿü ÿðÿ ÿÿƒÿÿø ÿÿðÿŸøÿùÿü ÿðÿ ÿÿ€ ÿÿøÿÿðÿÿÿÿùÿðÿÿðÿ ÿÿ€ ÿÿøÿÿðÿÿÿÿùÿðÿÿðÿ ÿÿ€ ÿùàþðÿÿÿÿ ÿðÿ?ðü ÿ€ ÿùàþðÿÿÿÿ ÿðÿ?ðü ÿ€ <yàþðÿþÿ yÿÀø?ððÃÿ€ <yàþðÿþÿ yÿÀø?ððÃÿ€ ?ÿ€ÿÿðÿøü ÿÀÿÿðó?çÿ€ ?ÿ€ÿÿðÿøü ÿÀÿÿðó?çÿ€ ?ÿ€ÿÿðÿøü ÿÀÿÿðó?çÿ€ ÿ€ÿÿð?øü ÿ ÿÿðóùÿ€ ÿ€ÿÿð?øü ÿ ÿÿðóù                       ÿÃÿ                       ÿÃÿü                      ÿÃÿü                      ÿÃÿÌ                      ÿÿÿÌ                      ÿÿÿ0                      ÿÿÿ0                      ÿÿÿ8€ ` 0ù˜sÀÁç€? ü1Ÿÿÿÿ8€ ` 0ù˜sÀÁç€? ü1Ÿÿÿÿ8€ ` 0ù˜sÀÁç€? ü1Ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ""")
            'bitmap
            'sb.AppendLine("BITMAP 589,3822,27,40,1,")
            'sb.AppendLine("BITMAP 589,3822,27,40,1," & hex1)
            ' sb.AppendLine("BITMAP 589,3822,27,40,1," & bitmapData)
            '' --- BITMAP DATA LINES ---
            'sb.AppendLine("ÿÿÿ")
            'sb.AppendLine("ÿÿÿ")
            'sb.AppendLine("ÿÿÿ")
            'sb.AppendLine("ÿÿÿ")
            'sb.AppendLine("ÿÿÿ")
            'sb.AppendLine("ÿÿÿ")
            'sb.AppendLine("þ    €ðà0€   `  ÿÃÿ")
            'sb.AppendLine("þ    €ðà0€   `  ÿÃÿ")
            'sb.AppendLine("ÿÿÿÿÿÿþóüàÿÏÿÿÿÿ?ðÿÃÿ")
            'sb.AppendLine("ÿÿÿÿÿÿþóüàÿÏÿÿÿÿ?ðÿÃÿ")
            'sb.AppendLine("ÿÿÿÿÿÿþóüàÿÏÿÿÿÿ?ðÿÃÿ")
            'sb.AppendLine("ÿÿƒóÿÿÿÿóüøÿÏùüüÿÿðÿ ÿ")
            'sb.AppendLine("ÿÿƒóÿÿÿÿóüøÿÏùüüÿÿðÿ ÿ")
            'sb.AppendLine("ÿƒÿÿø ÿÿðÿŸøÿùÿü ÿðÿ ÿ")
            'sb.AppendLine("ÿƒÿÿø ÿÿðÿŸøÿùÿü ÿðÿ ÿ")
            'sb.AppendLine("ÿ€ ÿÿøÿÿðÿÿÿÿùÿðÿÿðÿ ÿ")
            'sb.AppendLine("ÿ€ ÿÿøÿÿðÿÿÿÿùÿðÿÿðÿ ÿ")
            'sb.AppendLine("ÿ€ ÿùàþðÿÿÿÿ ÿðÿ?ðü ")
            'sb.AppendLine("ÿ€ ÿùàþðÿÿÿÿ ÿðÿ?ðü ")
            'sb.AppendLine("ÿ€ <yàþðÿþÿ yÿÀø?ððÃ")
            'sb.AppendLine("ÿ€ <yàþðÿþÿ yÿÀø?ððÃ")
            'sb.AppendLine("ÿ€ ?ÿ€ÿÿðÿøü ÿÀÿÿðó?ç")
            'sb.AppendLine("ÿ€ ?ÿ€ÿÿðÿøü ÿÀÿÿðó?ç")
            'sb.AppendLine("ÿ€ ?ÿ€ÿÿðÿøü ÿÀÿÿðó?ç")
            'sb.AppendLine("ÿ€ ÿ€ÿÿð?øü ÿ ÿÿðóù")
            'sb.AppendLine("ÿ€ ÿ€ÿÿð?øü ÿ ÿÿðóù")
            'sb.AppendLine("ÿÃÿ")
            'sb.AppendLine("ÿÃÿü")
            'sb.AppendLine("ÿÃÿü")
            'sb.AppendLine("ÿÃÿÌ")
            'sb.AppendLine("ÿÿÿÌ")
            'sb.AppendLine("ÿÿÿ0")
            'sb.AppendLine("ÿÿÿ0")
            'sb.AppendLine("ÿÿÿ8€ ` 0ù˜sÀÁç€? ü1Ÿÿÿÿ")
            'sb.AppendLine("ÿÿÿ8€ ` 0ù˜sÀÁç€? ü1Ÿÿÿÿ")
            'sb.AppendLine("ÿÿÿ8€ ` 0ù˜sÀÁç€? ü1Ÿÿÿÿ")
            'sb.AppendLine("ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
            sb.AppendLine("CODEPAGE 1252")
            sb.AppendLine("TEXT 551,4024,""ROMAN.TTF"",180,1,12,""Atithya Clothing Company - Madurai""")
            sb.AppendLine("TEXT 804,4024,""0"",180,12,12,""Type : SS""")
            sb.AppendLine("BAR 7,3960,847,3")
            sb.AppendLine("TEXT 688,3955,""ROMAN.TTF"",180,1,13,""" & kcardname & """")
            sb.AppendLine("TEXT 754,3911,""ROMAN.TTF"",180,1,12,""" & kcity & """")
            sb.AppendLine("BAR 7,3855,847,3")
            sb.AppendLine("QRCODE 241,3828,L,9,A,180,M2,S7,""" & kbundleno & """")
            sb.AppendLine("TEXT 830, 3807, ""ROMAN.TTF"", 180, 1, 12, """ & kpackagenum & """")
            sb.AppendLine("TEXT 828,3741,""ROMAN.TTF"",180,1,12,""" & kdocdate & """")
            sb.AppendLine("TEXT 828,3681,""ROMAN.TTF"",180,1,12,""" & kpackno & """")
            'sb.AppendLine($"TEXT 220,3451,""ROMAN.TTF"",180,1,12,""{kbundleno}""")
            'sb.AppendLine("BOX 10,3195,858,3471,3")
            sb.AppendLine("TEXT 828,3619,""ROMAN.TTF"",180,1,12,""" & ktransport & """")
            If Len(Trim(kremarks)) > 0 Then
                sb.AppendLine("ERASE 257,3485,348,92")
                sb.AppendLine("TEXT 604,3576,""0"",180,16,28,""" & kremarks & """")
                sb.AppendLine("REVERSE 257,3485,348,92")
            End If

            sb.AppendLine("BAR 9,3486, 847, 3")
            'sb.AppendLine("TEXT 838,3311,""ROMAN.TTF"",180,1,12,""Transport : KPG""")
            sb.AppendLine("TEXT 832,3459,""ROMAN.TTF"",180,1,11,""S.No""")
            sb.AppendLine("TEXT 634,3459,""ROMAN.TTF"",180,1,11,""Item Name""")
            sb.AppendLine("TEXT 338,3459,""ROMAN.TTF"",180,1,11,""Style""")
            sb.AppendLine("TEXT 210,3459,""ROMAN.TTF"",180,1,11,""Size""")
            sb.AppendLine("TEXT 65,3459,""ROMAN.TTF"",180,1,11,""Qty""")
            'sb.AppendLine("TEXT 550,3652,""ROMAN.TTF"",180,1,12,""Atithya Clothing Company - Madurai""")
            sb.AppendLine("BAR 3,3421, 851, 3")
            For Each rw As DataRow In dtc.Rows
                Dim sno As Int16 = Convert.ToInt16(rw("sno").ToString())
                Dim item As String = rw("itemname").ToString()
                Dim style As String = rw("style").ToString()
                Dim size As String = rw("size").ToString()
                Dim qty As String = rw("quantity").ToString()

                ' Format: SNo, Item Name, Style, Size, Qty
                sb.AppendLine("TEXT 804," & y & ",""ROMAN.TTF"",180,1,11,""" & sno & """")
                sb.AppendLine($"TEXT 716," & y & ",""ROMAN.TTF"",180,1,11,""" & item & """")
                sb.AppendLine($"TEXT 333," & y & ",""ROMAN.TTF"",180,1,11,""" & style & """")
                sb.AppendLine($"TEXT 197," & y & ",""ROMAN.TTF"",180,1,11,""" & size & """")
                sb.AppendLine($"TEXT 59," & y & ",""ROMAN.TTF"",180,1,11,""" & qty & """")

                y -= gap      ' Go to next line
                'sno += 1      ' Increase serial number
            Next
            y -= gap

            sb.AppendLine("BAR 7," & y & ", 847, 3")
            'sb.AppendLine("BAR 25,263, 768, 3")
            y -= gap
            sb.AppendLine("TEXT 499," & y & ",""ROMAN.TTF"",180,1,11,""Total Pcs....""")
            sb.AppendLine($"TEXT 65," & y & ",""ROMAN.TTF"",180,1,11,""" & ktotpcs & """")
            y -= gap
            'sb.AppendLine("TEXT 504,232,""ROMAN.TTF"",180,1,11,""Total Pcs....""")
            'sb.AppendLine("TEXT 70,232,""ROMAN.TTF"",180,1,11,""200""")
            yy = y - 63
            sb.AppendLine("ERASE 38," & yy & ", 780, 64")
            sb.AppendLine("TEXT 817," & y & ", ""0"", 180, 10, 19, ""Do Not ACCEPT If STRAPPING TAGS And BOXES ARE BROKEN""")
            sb.AppendLine("REVERSE 38," & yy & ", 780, 64")
            y -= 73
            sb.AppendLine("TEXT 794," & y & ",""ROMAN.TTF"",180,1,11,""" & ktoday & """")
            sb.AppendLine("TEXT 157," & y & ",""ROMAN.TTF"",180,1,11,""" & ktime & """")
            If optdateord.Checked = True Then
                sb.AppendLine("TEXT 520," & y & ",""0"",180,12,12,""Date Order""")
            End If

            sb.AppendLine("PRINT 1,1")

            ' Send to printer
            'RawPrinterHelper.SendStringToPrinter("TSC TP244", sb.ToString())
            Dim sfd As New SaveFileDialog()
            sfd.Filter = "Text Files|*.txt"
            sfd.FileName = "testlabel.txt"

            If sfd.ShowDialog() = DialogResult.OK Then
                System.IO.File.WriteAllText(sfd.FileName, sb.ToString())
            End If


            If mos = "WIN" Then
                Dim ok As Boolean = RawPrinterHelper.SendStringToPrinter3(mprinter, sb.ToString())

                If ok Then
                    'MsgBox("PRINTED SUCCESS")
                    If cmbtype.Text = "SALES" Then
                        qry = "update  oinv set u_pckdate=getdate(),  U_Noofbun = '" & ku_noofbun & "',U_Bundle = '" & ku_noofbun & "'  WHERE DOCENTRY =" & docentry
                    ElseIf cmbtype.Text = "DATE ORDER" Then
                        qry = "update  odln set u_pckdate=getdate(),  U_Noofbun = '" & ku_noofbun & "',U_Bundle = '" & ku_noofbun & "'  WHERE DOCENTRY =" & docentry
                    End If
                    Try
                        executeQuery(qry)
                    Catch ex As Exception
                        MsgBox("Error : " & ex.Message)
                    End Try

                Else
                    MsgBox("FAILED")
                End If
            Else
                ''Try
                ''    Dim printer As String = mvertprinter
                ''    'Dim filePath As String = mlinpath & "nsbarcodEV.txt"
                ''    '"/home/testing/Desktop/Barcodelinux/nsbarcodEV.txt"
                ''    Dim filePath As String = mlinpath
                ''    Dim filePathname As String = mlinpath & fileName

                ''    Dim psi As New ProcessStartInfo()
                ''    psi.FileName = "/bin/bash"
                ''    psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""
                ''    'psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
                ''    psi.UseShellExecute = False
                ''    psi.CreateNoWindow = True
                ''Process.Start(psi)

                Dim printer As String = tscprinter1
                Dim filePath As String = mlinpath
                Dim filePathname As String = mlinpath & fileName
                Dim success As Boolean = PrintTscRaw(printer, filePathname)


                If success = True Then
                    'MsgBox("PRINTED SUCCESS")
                    If cmbtype.Text = "SALES" Then
                        qry = "update  oinv set u_pckdate=getdate(),  U_Noofbun = '" & ku_noofbun & "',U_Bundle = '" & ku_noofbun & "'  WHERE DOCENTRY =" & docentry
                    ElseIf cmbtype.Text = "DATE ORDER" Then
                        qry = "update  odln set u_pckdate=getdate(),  U_Noofbun = '" & ku_noofbun & "',U_Bundle = '" & ku_noofbun & "'  WHERE DOCENTRY =" & docentry
                    End If
                    Try
                        executeQuery(qry)
                    Catch ex As Exception
                        MsgBox("Error : " & ex.Message)
                    End Try

                Else
                    MsgBox("FAILED")
                End If


            End If


        End If


    End Sub


    '    Dim bitmapData As String = File.ReadAllText("logo.txt", Encoding.GetEncoding(28591))

    '' Clean hex data
    'bitmapData = bitmapData.Replace(vbCr, "") _
    '                       .Replace(vbLf, "") _
    '                       .Replace(" ", "")

    'sb.AppendLine("BITMAP 589,3822,27,40,1," & bitmapData)

    ' Dim bitmapData As String = File.ReadAllText("logo.txt", Encoding.Latin1)
    'sb.AppendLine(bitmapData)
    'sb.AppendLine($"BITMAP {x},{y},27,40,1,")
    'sb.AppendLine(logoData)

    'Dim RowHeight As Integer = 40        ' each row height in dots
    '    Dim HeaderHeight As Integer = 900    ' top portion
    '    Dim FooterHeight As Integer = 200    ' bottom portion
    '    Dim TotalRows As Integer = dt.Rows.Count

    '    Dim TotalHeight As Integer = HeaderHeight + FooterHeight + (TotalRows * RowHeight)

    'sb.AppendLine($"SIZE 107.10 mm, {TotalHeight / 8} mm") 
    '****
    'qrcode condinution
    'select b.docnum,b.docdate,b.docentry,b.cardcode,b.cardname, b.u_noofbun,b.U_TransporterName,case when isnull(e.u_brch,'')='' then b.cardname else e.u_brch end Cardname,
    'e.building,e.block,e.street,e.city,e.zipcode,e.state,c.packagenum,t.u_brandgroup,t.u_style,t.u_size,d.quantity from oinv b
    'inner join rinv7 c On c.docentry=b.docentry
    'inner join rinv8 d On d.docentry=b.docentry And d.packagenum=c.PackageNum
    'inner join crd1 e On e.cardcode=b.cardcode And e.address=b.ShipToCode
    'inner join oitm t On t.itemcode=d.itemcode
    'where b.docentry=865458

    '    Dim y As Integer = 120      ' Starting Y position
    '    Dim gap As Integer = 28     ' Line height gap
    '    Dim sno As Integer = 1      ' Serial no start

    '    For Each rw As DataRow In dtc.Rows
    '    Dim item As String = rw("itemname").ToString()
    '    Dim style As String = rw("style").ToString()
    '    Dim size As String = rw("size").ToString()
    '    Dim qty As String = rw("qty").ToString()

    '    ' Format: SNo, Item Name, Style, Size, Qty
    '    sb.AppendLine($"TEXT 30, {y}, ""0"", 180, 11, 10, ""{sno}""")
    '    sb.AppendLine($"TEXT 80, {y}, ""0"", 180, 11, 10, ""{item}""")
    '    sb.AppendLine($"TEXT 260, {y}, ""0"", 180, 11, 10, ""{style}""")
    '    sb.AppendLine($"TEXT 360, {y}, ""0"", 180, 11, 10, ""{size}""")
    '    sb.AppendLine($"TEXT 440, {y}, ""0"", 180, 11, 10, ""{qty}""")

    '    y += gap      ' Go to next line
    '    sno += 1      ' Increase serial number
    'Next

End Class
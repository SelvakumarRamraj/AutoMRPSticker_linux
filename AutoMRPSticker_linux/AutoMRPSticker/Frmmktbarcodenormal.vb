Imports System.IO
Imports System.Drawing.Printing
Imports System.Configuration
Imports Microsoft.VisualBasic
'Imports BarTender
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
Imports Barcodelinux.connection



Public Class Frmmktbarcodenormal
    Private printDocument As New PrintDocument()
    Dim mfile, fwash, fsilk, fpant, mtype As String
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

    'Private printerIp As String
    'Private printerPort As Integer
    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal PSZpRINTER As String) As Boolean
    Private Sub Frmmktbarcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim x As Integer = (Me.ClientSize.Width - Groupbox1.Width) / 2
        Dim y As Integer = (Me.ClientSize.Height - Groupbox1.Height) / 2
        Groupbox1.Location = New Point(x, y)


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
        'mos = ConfigurationSettings.AppSettings("OS")
        'mvertprinter = ConfigurationSettings.AppSettings("Printername_Vertical")
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
                     & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))>0 then substring(substring(t.u_fitname,charindex('/',t.u_fitname)+1,30),1,charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))-1) else '' end cut " _
                     & " from bartemp b with (nolock) " _
                     & " inner join oitm t on t.itemcode=b.itemcode " _
                     & " where b.docentry=" & Val(lbldocentry.Text) & " and month(b.docdate)=" & Val(txtmont.Text) & " and year(b.docdate)=" & Val(txtyr.Text) & " order by b.linenum "

            ElseIf Trim(cmbtype.Text) = "Showroom" Then
                ' msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,autocode mbarcode,autocode txbarcode, boxmrp,mrp,boxqty,quantity,size2 from bartemp with (nolock) where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"

                msql = msql & " select b.docnum,b.u_subgrp6,b.u_style,b.u_size,b.color,b.u_itemgrp,case when len(rtrim(@mfd))>0 then @mfd else b.mfd end mfd,b.cstype,b.autocode mbarcode,b.autocode txbarcode, b.boxmrp,b.mrp,b.boxqty,b.quantity,b.size2, " _
                    & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',t.u_fitname)>0   then substring(t.u_fitname,1,charindex('/',t.u_fitname)-1) else '' end fit, " _
                    & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))>0 then substring(substring(t.u_fitname,charindex('/',t.u_fitname)+1,30),1,charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))-1) else '' end cut " _
                    & " from bartemp b with (nolock) " _
                    & " inner join oitm t on t.itemcode=b.itemcode " _
                    & " where b.docentry=" & Val(lbldocentry.Text) & " and month(b.docdate)=" & Val(txtmont.Text) & " and year(b.docdate)=" & Val(txtyr.Text) & " order by b.linenum "

            ElseIf Trim(cmbtype.Text) = "Franchise" Then
                'msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,autocode mbarcode,autocode txbarcode, boxmrp,mrp,boxqty,quantity,size2 from bartemp with (nolock) where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"
                msql = msql & " select b.docnum,b.u_subgrp6,b.u_style,b.u_size,b.color,b.u_itemgrp,case when len(rtrim(@mfd))>0 then @mfd else b.mfd end mfd,b.cstype,b.autocode mbarcode,b.autocode txbarcode, b.boxmrp,b.mrp,b.boxqty,b.quantity,b.size2," _
                       & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',t.u_fitname)>0   then substring(t.u_fitname,1,charindex('/',t.u_fitname)-1) else '' end fit, " _
                       & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))>0 then substring(substring(t.u_fitname,charindex('/',t.u_fitname)+1,30),1,charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))-1) else '' end cut " _
                       & " from bartemp b with (nolock)  " _
                       & " inner join oitm t on t.itemcode=b.itemcode " _
                       & " where b.docentry=" & Val(lbldocentry.Text) & " and month(b.docdate)=" & Val(txtmont.Text) & " and year(b.docdate)=" & Val(txtyr.Text) & " order by b.linenum "
            ElseIf Trim(cmbtype.Text) = "OS" Then 'u_remarks
                'msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,u_remarks mbarcode,u_remarks txbarcode, boxmrp,mrp,boxqty,quantity,size2 from bartemp with (nolock) where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"

                msql = msql & " select b.docnum,b.u_subgrp6,b.u_style,b.u_size,b.color,b.u_itemgrp,case when len(rtrim(@mfd))>0 then @mfd else b.mfd end mfd,b.cstype,b.u_remarks mbarcode,b.u_remarks txbarcode, b.boxmrp,b.mrp,b.boxqty,b.quantity,b.size2, " _
                      & "  case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',t.u_fitname)>0   then substring(t.u_fitname,1,charindex('/',t.u_fitname)-1) else '' end fit, " _
                      & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))>0 then substring(substring(t.u_fitname,charindex('/',t.u_fitname)+1,30),1,charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))-1) else '' end cut " _
                      & " from bartemp b with (nolock) " _
                      & " inner join oitm t on t.itemcode=b.itemcode  " _
                      & " where b.docentry=" & Val(lbldocentry.Text) & " and month(b.docdate)=" & Val(txtmont.Text) & " and year(b.docdate)=" & Val(txtyr.Text) & " order by b.linenum "

            ElseIf Trim(cmbtype.Text) = "Distributor" Then
                'msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,odoocode mbarcode,odoocode txbarcode, boxmrp,mrp,boxqty,quantity,size2 from bartemp with (nolock) where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"
                msql = msql & "select b.docnum,b.u_subgrp6,b.u_style,b.u_size,b.color,b.u_itemgrp,case when len(rtrim(@mfd))>0 then @mfd else b.mfd end mfd,b.cstype,b.odoocode mbarcode,b.odoocode txbarcode, b.boxmrp,b.mrp,b.boxqty,b.quantity,b.size2," _
                       & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',t.u_fitname)>0   then substring(t.u_fitname,1,charindex('/',t.u_fitname)-1) else '' end fit, " _
                       & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))>0 then substring(substring(t.u_fitname,charindex('/',t.u_fitname)+1,30),1,charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))-1) else '' end cut " _
                       & " from bartemp b with (nolock) " _
                       & " inner join oitm t on t.itemcode=b.itemcode " _
                       & " where b.docentry=" & Val(lbldocentry.Text) & " and month(b.docdate)=" & Val(txtmont.Text) & " and year(b.docdate)=" & Val(txtyr.Text) & " order by b.linenum "
            ElseIf Trim(cmbtype.Text) = "Pothys" Then
                'msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,autocode mbarcode,autocode txbarcode, boxmrp,mrp,boxqty,quantity,size2 from bartemp with (nolock) where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"
                msql = msql & " select b.docnum,b.u_subgrp6,b.u_style,b.u_size,b.color,b.u_itemgrp,case when len(rtrim(@mfd))>0 then @mfd else b.mfd end mfd,b.cstype,b.autocode mbarcode,b.autocode txbarcode, b.boxmrp,b.mrp,b.boxqty,b.quantity,b.size2," _
                       & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',t.u_fitname)>0   then substring(t.u_fitname,1,charindex('/',t.u_fitname)-1) else '' end fit, " _
                       & " case when len(rtrim(ltrim(isnull(t.u_fitname,''))))>0 and charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))>0 then substring(substring(t.u_fitname,charindex('/',t.u_fitname)+1,30),1,charindex('/',substring(t.u_fitname,charindex('/',t.u_fitname)+1,30))-1) else '' end cut  " _
                       & " from bartemp b with (nolock) " _
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

                Next
            Else
                'txtmont.Text = 0
            End If
            Lblcnt.Text = dg.Rows.Count
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
        If OptSamDrf.Checked = True Then
            Call samplebarcode(Val(lbldocentry.Text))
        Else
            Call loaddata()
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
                    If mos = "WIN" Then
                        onlineprint2()
                    Else
                        onlineprintlinux()
                    End If

                End If
                    Else
                'speedprint()
                If Chkvertical.Checked = True Then
                    'speedprint2vert()
                    speedprint2vertboth()
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
        'Dim btapp As New BarTender.Application
        'For i As Integer = 0 To dg.Rows.Count - 1
        '    Dim c As Boolean
        '    c = dg.Rows(i).Cells(0).Value
        '    If c = True Then
        '        'Dim btapp As New BarTender.Application
        '        Dim btFormat As BarTender.Format
        '        'btapp = New BarTender.Application
        '        mqty = Integer.Parse(txtno.Text.ToString)
        '        mfile = System.Windows.Forms.Application.StartupPath & "\Acclbl.btw"

        '        btFormat = btapp.Formats.Open(mfile, False, "")


        '        'specify printer. if not, printer specified in format is used.
        '        If Len(Trim(cmbprinter.Text)) > 0 Then
        '            btFormat.Printer = cmbprinter.Text
        '        End If


        '        btFormat.SetNamedSubStringValue("MRP", Val(dg.Rows(i).Cells(12).Value))
        '        btFormat.SetNamedSubStringValue("u_subgrp6", Trim(dg.Rows(i).Cells(2).Value))
        '        btFormat.SetNamedSubStringValue("COLOR", Trim(dg.Rows(i).Cells(5).Value))
        '        btFormat.SetNamedSubStringValue("U_ITEMGRP", Trim(dg.Rows(i).Cells(6).Value))
        '        btFormat.SetNamedSubStringValue("U_STYLE", Trim(dg.Rows(i).Cells(3).Value))
        '        btFormat.SetNamedSubStringValue("U_SIZE", Trim(dg.Rows(i).Cells(4).Value))
        '        btFormat.SetNamedSubStringValue("MFD", Trim(dg.Rows(i).Cells(7).Value))
        '        btFormat.SetNamedSubStringValue("CSTYPE", Trim(dg.Rows(i).Cells(8).Value))
        '        btFormat.SetNamedSubStringValue("DOCNUM", Trim(dg.Rows(i).Cells(1).Value))
        '        btFormat.SetNamedSubStringValue("MBarcode", Trim(dg.Rows(i).Cells(9).Value))
        '        If Trim(cmbtype.Text) <> "Dealer" Or Trim(cmbtype.Text) <> "TN" Then
        '            btFormat.SetNamedSubStringValue("AUTOCODE", Trim(dg.Rows(i).Cells(10).Value))
        '        End If


        '        'btFormat.IdenticalCopiesOfLabel = mqty
        '        btFormat.IdenticalCopiesOfLabel = Val(dg.Rows(i).Cells(14).Value)



        '        'Print the document

        '        'btFormat.PrintOut(False, False)

        '        'End the BarTender process
        '        btFormat.PrintOut(False, False)

        '        'btapp.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges)
        '        'System.Runtime.InteropServices.Marshal.ReleaseComObject(btapp)
        '    End If

        'Next i
        'btapp.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges)
        'System.Runtime.InteropServices.Marshal.ReleaseComObject(btapp)
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

                'FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                    FileSystem.PrintLine(fNum, "SIZE 69.10 mm, 45 mm")
                Else
                    FileSystem.PrintLine(fNum, "SIZE 69.10 mm,42 mm")
                    FileSystem.PrintLine(fNum, "GAP 45 mm,0")
                End If

                FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum, "SPEED 7")
                FileSystem.PrintLine(fNum, "SET PEEL OFF")
                FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
                End If

                FileSystem.PrintLine(fNum, "SET TEAR ON")
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



                FileSystem.PrintLine(fNum, "PRINT 1," & Val(dg.Rows(i).Cells(14).Value))
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                End If


            End If
        Next
        FileSystem.FileClose(fNum)
        'FileClose(1)


        'PrintTextFile(mdir)
        'PrintToTSCPrinter(mdir, Trim(cmbprinter.Text))


        If mos = "WIN" Then

            If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

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

        Else
            'Dim psi As New ProcessStartInfo()
            'psi.FileName = "/bin/bash"
            'psi.Arguments = "-c ""/home/testing/Desktop/Barcodlinux/print_raw.sh TTP-244-Pro /home/testing/Desktop/Barcodlinux/nsbarcodEH.txt"""
            'psi.UseShellExecute = False
            'psi.CreateNoWindow = True

            'Process.Start(psi)


            'Dim printer As String = mprinter
            'Dim filePath As String = mlinpath
            'Dim filePathname As String = mlinpath & "nsbarcodEH.txt"
            ''"/home/testing/Desktop/Barcodelinux/nsbarcodEH.txt"

            'Dim psi As New ProcessStartInfo()
            'psi.FileName = "/bin/bash"
            ''psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePathname & "'"""

            'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""
            'psi.UseShellExecute = False
            'psi.CreateNoWindow = True

            ''TextBox1.Text = psi.FileName & psi.Arguments
            'Process.Start(psi)

            Dim printer As String = tscprinter1
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "nsbarcodEH.txt"
            Dim success As Boolean = PrintTscRaw(printer, filePathname)
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

                'FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='23.5 mm'></xpml>SIZE 77.6 mm, 45 mm")

                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 77.6 mm,45 mm")

                Else
                    FileSystem.PrintLine(fNum, "SIZE 77.6 mm,43 mm")
                    FileSystem.PrintLine(fNum, "GAP 23.5 mm,0")
                End If



                FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum, "SPEED 7")
                FileSystem.PrintLine(fNum, "SET PEEL OFF")
                FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='23.5 mm'></xpml>")
                End If

                FileSystem.PrintLine(fNum, "SET TEAR ON")
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
                        If mos = "WIN" Then
                            FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                        End If
                        rwno = 0
                        If j < Val(dg.Rows(i).Cells(14).Value) Then
                            'FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='23.5 mm'></xpml>SIZE 77.6 mm, 45 mm")
                            If mos = "WIN" Then
                                FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 77.6 mm,45 mm")

                            Else
                                FileSystem.PrintLine(fNum, "SIZE 77.6 mm,43 mm")
                                FileSystem.PrintLine(fNum, "GAP 23.5 mm,0")
                            End If
                            FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                            FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                            FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                            FileSystem.PrintLine(fNum, "SPEED 7")
                            FileSystem.PrintLine(fNum, "SET PEEL OFF")
                            FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                            FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                            If mos = "WIN" Then
                                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='23.5 mm'></xpml>")
                            End If
                            FileSystem.PrintLine(fNum, "SET TEAR ON")
                            FileSystem.PrintLine(fNum, "CLS")
                            FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                        End If


                    End If



                    If j >= Val(dg.Rows(i).Cells(14).Value) Then
                        rwno = 0
                        If (j Mod 2) <> 0 Then
                            FileSystem.PrintLine(fNum, "PRINT 1,1")
                            If mos = "WIN" Then
                                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                            End If

                        End If
                        'FileSystem.PrintLine(fNum, "PRINT 1,1")
                        'FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                    End If
                Next j

            End If
        Next
        FileSystem.FileClose(fNum)

        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If mos = "WIN" Then

                If mcitrix = "Y" Then
                    'MsgBox("online print" & mdir)
                    'citrixprint(mdir)
                    citrixprint2(mdir, cmbprinter.SelectedItem.ToString())
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
            Else
                'Dim printer As String = mprinter
                'Dim filePath As String = mlinpath
                'Dim filePathname As String = mlinpath & "online.txt"
                ''psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

                'Dim psi As New ProcessStartInfo()
                'psi.FileName = "/bin/bash"
                'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

                ''psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
                'psi.UseShellExecute = False
                'psi.CreateNoWindow = True
                ''TextBox1.Text = psi.FileName & psi.Arguments
                'Process.Start(psi)


                Dim printer As String = tscprinter1
                Dim filePath As String = mlinpath
                Dim filePathname As String = mlinpath & "online.txt"
                Dim success As Boolean = PrintTscRaw(printer, filePathname)

            End If

        End If
        cmbfit.Text = ""
        'chkliberty.Checked = False


    End Sub

    Private Sub onlineprintlinux()
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

                'FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='23.5 mm'></xpml>SIZE 77.6 mm, 45 mm")

                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 77.6 mm,45 mm")

                Else
                    FileSystem.PrintLine(fNum, "SIZE 77.6 mm,43 mm")
                    FileSystem.PrintLine(fNum, "GAP 23.5 mm,0")
                End If



                FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum, "SPEED 7")
                FileSystem.PrintLine(fNum, "SET PEEL OFF")
                FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='23.5 mm'></xpml>")
                End If

                FileSystem.PrintLine(fNum, "SET TEAR ON")
                FileSystem.PrintLine(fNum, "CLS")
                'FileSystem.PrintLine(fNum, "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                '"₹"
                'PrintBitmap()
                FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")

                For j = 1 To Val(dg.Rows(i).Cells(14).Value)
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(615 - rwno) & ",145," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(615 - rwno) & ",103," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(615 - rwno) & ",67," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(615 - rwno) & ",22," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(10).Value) & """")
                    FileSystem.PrintLine(fNum, "QRCODE " & Str(420 - rwno) & ",117,L,5,A,180,M2,S7," & """" & Trim(dg.Rows(i).Cells(9).Value) & """")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(516 - rwno) & ",68," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                    FileSystem.PrintLine(fNum, TAB(0), "TEXT " & Str(491 - rwno) & ",68," & """" & "0" & """" & ",180,8,8," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                    rwno = rwno + 314
                    If (j Mod 2) = 0 Then
                        'If rwno > 314 Then
                        FileSystem.PrintLine(fNum, "PRINT 1,1")
                        If mos = "WIN" Then
                            FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                        End If
                        rwno = 0
                        If j < Val(dg.Rows(i).Cells(14).Value) Then
                            'FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='23.5 mm'></xpml>SIZE 77.6 mm, 45 mm")
                            If mos = "WIN" Then
                                FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 77.6 mm,45 mm")

                            Else
                                FileSystem.PrintLine(fNum, "SIZE 77.6 mm,43 mm")
                                FileSystem.PrintLine(fNum, "GAP 23.5 mm,0")
                            End If
                            FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                            FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                            FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                            FileSystem.PrintLine(fNum, "SPEED 7")
                            FileSystem.PrintLine(fNum, "SET PEEL OFF")
                            FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                            FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                            If mos = "WIN" Then
                                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='23.5 mm'></xpml>")
                            End If
                            FileSystem.PrintLine(fNum, "SET TEAR ON")
                            FileSystem.PrintLine(fNum, "CLS")
                            FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                        End If


                    End If



                    If j >= Val(dg.Rows(i).Cells(14).Value) Then
                        rwno = 0
                        If (j Mod 2) <> 0 Then
                            FileSystem.PrintLine(fNum, "PRINT 1,1")
                            If mos = "WIN" Then
                                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                            End If

                        End If
                        'FileSystem.PrintLine(fNum, "PRINT 1,1")
                        'FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                    End If
                Next j

            End If
        Next
        FileSystem.FileClose(fNum)

        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If mos = "WIN" Then

                If mcitrix = "Y" Then
                    'MsgBox("online print" & mdir)
                    'citrixprint(mdir)
                    citrixprint2(mdir, cmbprinter.SelectedItem.ToString())
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
            Else
                'Dim printer As String = mprinter
                'Dim filePath As String = mlinpath
                'Dim filePathname As String = mlinpath & "online.txt"
                ''psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

                'Dim psi As New ProcessStartInfo()
                'psi.FileName = "/bin/bash"
                'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

                ''psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
                'psi.UseShellExecute = False
                'psi.CreateNoWindow = True
                'TextBox1.Text = psi.FileName & psi.Arguments
                'Process.Start(psi)



                Dim printer As String = tscprinter1
                Dim filePath As String = mlinpath
                Dim filePathname As String = mlinpath & "online.txt"
                Dim success As Boolean = PrintTscRaw(printer, filePathname)
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
                                                    Dim fileContents As String = File.ReadAllText(filePath)

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
                                                    Dim fileContents As String = System.IO.File.ReadAllText(filePath)

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
        Dim Dir As String = System.AppDomain.CurrentDomain.BaseDirectory()
        ' mdir = Trim(dir) & "nsbarcodEV.txt"
        'mdir = IO.Path.Combine(Dir, "nsbarcodEV.txt")
        'MsgBox(mdir)
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

                'FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                    FileSystem.PrintLine(fNum, "SIZE 69.10 mm, 45 mm")
                Else
                    FileSystem.PrintLine(fNum, "SIZE 69.10 mm,43 mm")
                    FileSystem.PrintLine(fNum, "GAP 45 mm,0")
                End If
                FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum, "SPEED 7")
                FileSystem.PrintLine(fNum, "SET PEEL OFF")
                FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
                End If

                FileSystem.PrintLine(fNum, "SET TEAR ON")
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
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                End If


            End If
        Next
        FileSystem.FileClose(fNum)


        If mos = "WIN" Then
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
        Else
            'Dim printer As String = mprinter
            'Dim filePath As String = mlinpath
            'Dim filePathname As String = mlinpath & "sampbarcodE.txt"
            ''psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

            'Dim psi As New ProcessStartInfo()
            'psi.FileName = "/bin/bash"
            'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

            ''psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
            'psi.UseShellExecute = False
            'psi.CreateNoWindow = True
            'TextBox1.Text = psi.FileName & psi.Arguments
            'Process.Start(psi)



            Dim printer As String = tscprinter1
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "sampbarcodE.txt"
            Dim success As Boolean = PrintTscRaw(printer, filePathname)
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
        ' mdir = Trim(dir) & "nsbarcodEV.txt"
        mdir = IO.Path.Combine(dir, "nsbarcodEV.txt")


        Dim fNum As Integer = FileSystem.FreeFile()
        FileSystem.FileOpen(fNum, mdir, OpenMode.Output, OpenAccess.Write, OpenShare.Shared, -1)
        'FileSystem.PrintLine(fNum, "This is a line with the Rupee symbol: ₹")
        'FileSystem.FileClose(fNum)



        'FileOpen(1, mdir, OpenMode.Output)
        'End If
        'Dim rupeeSymbol As String = ChrW(&H20B9)
        'Dim rupeeSymbol As String = ChrW(&H20B9)



        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then
                'PrintLine(1, TAB(0), DR.Item("firstdet"))
                If Chkshirt.Checked = True Then
                    'FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                        FileSystem.PrintLine(fNum, "SIZE 69.10 mm, 45 mm")
                    Else
                        FileSystem.PrintLine(fNum, "SIZE 69.10 mm,43 mm")
                        FileSystem.PrintLine(fNum, "GAP 45 mm,0")
                    End If
                    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum, "SPEED 7")
                    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
                    End If

                    FileSystem.PrintLine(fNum, "SET TEAR ON")
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




                ElseIf chkpant.Checked = True Then

                    'FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                        FileSystem.PrintLine(fNum, "SIZE 69.10 mm, 45 mm")
                    Else
                        FileSystem.PrintLine(fNum, "SIZE 69.10 mm,43 mm")
                        FileSystem.PrintLine(fNum, "GAP 45 mm,0")
                    End If
                    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum, "SPEED 7")
                    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
                    End If

                    FileSystem.PrintLine(fNum, "SET TEAR ON")
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




                ElseIf chktwin.Checked = True Then
                    'FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                        FileSystem.PrintLine(fNum, "SIZE 69.10 mm, 45 mm")
                    Else
                        FileSystem.PrintLine(fNum, "SIZE 69.10 mm,43 mm")
                        FileSystem.PrintLine(fNum, "GAP 45 mm,0")
                    End If
                    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum, "SPEED 7")
                    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
                    End If
                    FileSystem.PrintLine(fNum, "SET TEAR ON")
                    FileSystem.PrintLine(fNum, "CLS")

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




                ElseIf chkset.Checked = True Then

                    'FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                        FileSystem.PrintLine(fNum, "SIZE 69.10 mm, 45 mm")
                    Else
                        FileSystem.PrintLine(fNum, "SIZE 69.10 mm,43 mm")
                        FileSystem.PrintLine(fNum, "GAP 45 mm,0")
                    End If
                    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum, "SPEED 7")
                    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
                    End If

                    FileSystem.PrintLine(fNum, "SET TEAR ON")
                    FileSystem.PrintLine(fNum, "CLS")

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



                Else
                    'FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='41.0 mm'></xpml>SIZE 67.5 mm, 41 mm")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                        FileSystem.PrintLine(fNum, "SIZE 69.10 mm, 45 mm")
                    Else
                        FileSystem.PrintLine(fNum, "SIZE 69.10 mm,43 mm")
                        FileSystem.PrintLine(fNum, "GAP 45 mm,0")
                    End If
                    FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                    FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                    FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                    FileSystem.PrintLine(fNum, "SPEED 7")
                    FileSystem.PrintLine(fNum, "SET PEEL OFF")
                    FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                    FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='41.0 mm'></xpml>")
                    End If
                    FileSystem.PrintLine(fNum, "SET TEAR ON")
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



                FileSystem.PrintLine(fNum, "PRINT 1," & Val(dg.Rows(i).Cells(14).Value))
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                End If


            End If
        Next
        FileSystem.FileClose(fNum)
        'FileClose(1)


        ''PrintTextFile(mdir)
        ''PrintToTSCPrinter(mdir, Trim(cmbprinter.Text))


        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            If mos = "WIN" Then
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
            Else
                'Dim printer As String = mprinter
                'Dim filePath As String = mlinpath
                'Dim filePathname As String = mlinpath & "nsbarcodEV.txt"
                ''psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

                'Dim psi As New ProcessStartInfo()
                'psi.FileName = "/bin/bash"
                'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

                ''psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
                'psi.UseShellExecute = False
                'psi.CreateNoWindow = True
                'TextBox1.Text = psi.FileName & psi.Arguments
                'Process.Start(psi)


                Dim printer As String = tscprinter2
                Dim filePath As String = mlinpath
                Dim filePathname As String = mlinpath & "nsbarcodEV.txt"
                Dim success As Boolean = PrintTscRaw(printer, filePathname)

            End If

        End If





        'Dim text As String = File.ReadAllText(mdir, System.Text.Encoding.GetEncoding(1252))
        ''Process.Start("bash", "-c ""lp -d  'TSC_TTP_244_Pro' -o raw '" & mdir & "'""")
        'Process.Start("bash", "-c ""lp -d '" & mvertprinter & "' -o raw '" & mdir & "'""")
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
        Dim t1 = New Threading.Thread(Sub() speedprintHboth())
        Dim t2 = New Threading.Thread(Sub() speedprint2vertboth())
        If MsgBox("Print!", vbYesNo) = vbYes Then
            t1.Start()
            t2.Start()
        End If

    End Sub


    Private Sub speedprintHbothold()
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

                'FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                    FileSystem.PrintLine(fNum, "SIZE 69.10 mm, 45 mm")
                Else
                    FileSystem.PrintLine(fNum, "SIZE 69.10 mm,43 mm")
                    FileSystem.PrintLine(fNum, "GAP 45 mm,0")
                End If


                FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum, "SPEED 7")
                FileSystem.PrintLine(fNum, "SET PEEL OFF")
                FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
                End If
                FileSystem.PrintLine(fNum, "SET TEAR ON")
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



                FileSystem.PrintLine(fNum, "PRINT 1," & Val(dg.Rows(i).Cells(14).Value))
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                End If


            End If
        Next
        FileSystem.FileClose(fNum)

        If mos = "WIN" Then
            If mcitrix = "Y" Then

                'citrixprint(mdir)
                citrixprint2(mdir, cmbprinter.SelectedItem.ToString())
            Else
                Shell("rawpr.bat " & mdir)
            End If
        Else



            Dim printer As String = tscprinter1
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "nsbarcodEH.txt"
            Dim success As Boolean = PrintTscRaw(printer, filePathname)

        End If



    End Sub


    Private Sub speedprint2vertbothwithadd()
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
                    'FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                        FileSystem.PrintLine(fNum2, "SIZE 69.10 mm, 45 mm")
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
                    FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
                    End If
                    FileSystem.PrintLine(fNum2, "SET TEAR ON")
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

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
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

                    'FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                        FileSystem.PrintLine(fNum2, "SIZE 69.10 mm, 45 mm")
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
                    FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
                    End If

                    FileSystem.PrintLine(fNum2, "SET TEAR ON")
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

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
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
                    'FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                        FileSystem.PrintLine(fNum2, "SIZE 69.10 mm, 45 mm")
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
                    FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
                    End If

                    FileSystem.PrintLine(fNum2, "SET TEAR ON")
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

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
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

                    'FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                        FileSystem.PrintLine(fNum2, "SIZE 69.10 mm, 45 mm")
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
                    FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
                    End If
                    FileSystem.PrintLine(fNum2, "SET TEAR ON")
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

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
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
                    'FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='41.0 mm'></xpml>SIZE 67.5 mm, 41 mm")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                        FileSystem.PrintLine(fNum2, "SIZE 69.10 mm, 45 mm")
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
                    FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='1' pitch='41.0 mm'></xpml>SET TEAR ON")
                    End If
                    FileSystem.PrintLine(fNum2, "SET TEAR ON")
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

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
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
                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
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



                FileSystem.PrintLine(fNum2, "PRINT 1," & Val(dg.Rows(i).Cells(14).Value))
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><end/></xpml>")
                End If




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

            'Dim printer As String = tscprinter2
            ''Dim filePath As String = mlinpath & "nsbarcodEV.txt"
            ''"/home/testing/Desktop/Barcodelinux/nsbarcodEV.txt"
            'Dim filePath As String = mlinpath
            'Dim filePathname As String = mlinpath & "nsbarcodEV.txt"

            'Dim psi As New ProcessStartInfo()
            'psi.FileName = "/bin/bash"
            'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""
            ''psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
            'psi.UseShellExecute = False
            'psi.CreateNoWindow = True
            'TextBox1.Text = psi.FileName & psi.Arguments
            'Process.Start(psi)

            Dim printer As String = tscprinter2
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "nsbarcodEV.txt"
            PrintTscRaw(printer, filePathname)
            'PrintTscRaw("/dev/usb/lp0", "/home/user/label.txt")
        End If



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

                'FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                    FileSystem.PrintLine(fNum, "SIZE 69.10 mm, 45 mm")
                Else
                    FileSystem.PrintLine(fNum, "SIZE 69.10 mm,43 mm")
                    FileSystem.PrintLine(fNum, "GAP 45 mm,0")
                End If


                FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum, "SPEED 7")
                FileSystem.PrintLine(fNum, "SET PEEL OFF")
                FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
                End If
                FileSystem.PrintLine(fNum, "SET TEAR ON")
                FileSystem.PrintLine(fNum, "CLS")
                FileSystem.PrintLine(fNum, "BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                'PrintLine(1, TAB(0), "TEXT BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                '"₹"
                'PrintBitmap()
                FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                FileSystem.PrintLine(fNum, TAB(0), "TEXT 433,339," & """" & "0" & """" & ",180,13,12," & """" & "     MRP" & """")
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
                'FileSystem.PrintLine(fNum, "TEXT 367,159," & """" & "0" & """" & ",180,10,8," & """" & Trim(dg.Rows(i).Cells(7).Value) & """") 'mfg date
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
                    FileSystem.PrintLine(fNum, "TEXT 464,45," & """" & "0" & """" & ",180,8,7," & """" & "* Manufacturer,Marketer & Consumer Care" & """")
                    FileSystem.PrintLine(fNum, "TEXT 444,23," & """" & "0" & """" & ",180,8,7," & """" & "address details are available in the box" & """")
                End If


                FileSystem.PrintLine(fNum, "Text 543, 136," & """" & "0" & """" & ", 180, 7, 15," & """" & "_________________________________________________________" & """")
                FileSystem.PrintLine(fNum, "Text 551, 99," & """" & "0" & """" & ", 180, 15, 18," & """" & "|" & """")
                FileSystem.PrintLine(fNum, "Text 535, 86," & """" & "0" & """" & ", 180, 7, 10," & """" & "Consumer is free to open and inspect the product before buying it" & """")
                FileSystem.PrintLine(fNum, "Text 543, 89," & """" & "0" & """" & ", 180, 7, 15," & """" & "_________________________________________________________" & """")
                FileSystem.PrintLine(fNum, "Text 41, 99," & """" & "0" & """" & ", 180, 15, 18," & """" & "|" & """")



                FileSystem.PrintLine(fNum, "PRINT 1," & Val(dg.Rows(i).Cells(14).Value))
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                End If


            End If
        Next
        FileSystem.FileClose(fNum)

        If mos = "WIN" Then
            If mcitrix = "Y" Then

                'citrixprint(mdir)
                citrixprint2(mdir, cmbprinter.SelectedItem.ToString())
            Else
                Shell("rawpr.bat " & mdir)
            End If
        Else



            Dim printer As String = tscprinter1
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "nsbarcodEH.txt"
            Dim success As Boolean = PrintTscRaw(printer, filePathname)

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
                    'FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                        FileSystem.PrintLine(fNum2, "SIZE 69.10 mm, 45 mm")
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
                    FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
                    End If
                    FileSystem.PrintLine(fNum2, "SET TEAR ON")
                    FileSystem.PrintLine(fNum2, "CLS")
                    FileSystem.PrintLine(fNum2, "BITMAP 515,161,4,24,1,ÿÿÜÿÿÇÌÿ‡ÌþÌüÌøÌð#ÌàsÈÀñÀð ø ü þ ?ÿŒÿÌÿÿÌÿÿÌÿÿïÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")


                    'PrintLine(1, TAB(0), "TEXT BITMAP 283,312,3,32,1,àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    'FileSystem.PrintLine(fNum, "BITMAP 88,312,27,32,1,àÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ€ÿüÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿà?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿðÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÀÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿüÿàÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿð?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþÿ€ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿøþÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿð9üÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ Cø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ ø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿý   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÀø?ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿàøÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ € ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿþ   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿü   ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ")
                    '"₹"
                    'PrintBitmap()
                    FileSystem.PrintLine(fNum2, TAB(0), "CODEPAGE 1252")
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
                    ' FileSystem.PrintLine(fNum2, "TEXT 295,122," & """" & "0" & """" & ",90,7,6," & """" & Trim(dg.Rows(i).Cells(7).Value) & """")  'mfg date
                    FileSystem.PrintLine(fNum2, "TEXT 330,100," & """" & "0" & """" & ",90,8,9," & """" & Trim(dg.Rows(i).Cells(8).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 330,122," & """" & "0" & """" & ",90,10,9," & """" & Trim(dg.Rows(i).Cells(1).Value) & """")
                    FileSystem.PrintLine(fNum2, "TEXT 515,115," & """" & "ROMAN.TTF" & """" & ",90,1,7," & """" & "(Incl.of all Taxes)" & """")

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
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

                    'FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                        FileSystem.PrintLine(fNum2, "SIZE 69.10 mm, 45 mm")
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
                    FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
                    End If

                    FileSystem.PrintLine(fNum2, "SET TEAR ON")
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

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
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
                    'FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                        FileSystem.PrintLine(fNum2, "SIZE 69.10 mm, 45 mm")
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
                    FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>")
                    End If

                    FileSystem.PrintLine(fNum2, "SET TEAR ON")
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

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
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

                    'FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                        FileSystem.PrintLine(fNum2, "SIZE 69.10 mm, 45 mm")
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
                    FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='3' pitch='45.0 mm'></xpml>SET TEAR ON")
                    End If
                    FileSystem.PrintLine(fNum2, "SET TEAR ON")
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

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
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
                    'FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='41.0 mm'></xpml>SIZE 67.5 mm, 41 mm")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                        FileSystem.PrintLine(fNum2, "SIZE 69.10 mm, 45 mm")
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
                    FileSystem.PrintLine(fNum2, "SET PARTIAL_CUTTER OFF")
                    If mos = "WIN" Then
                        FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><page quantity='1' pitch='41.0 mm'></xpml>SET TEAR ON")
                    End If
                    FileSystem.PrintLine(fNum2, "SET TEAR ON")
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

                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
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
                    If Trim(cmbtype.Text) <> "Dealer" And Trim(cmbtype.Text) <> "TN" Then
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



                FileSystem.PrintLine(fNum2, "PRINT 1," & Val(dg.Rows(i).Cells(14).Value))
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum2, "<xpml></page></xpml><xpml><end/></xpml>")
                End If




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

            'Dim printer As String = tscprinter2
            ''Dim filePath As String = mlinpath & "nsbarcodEV.txt"
            ''"/home/testing/Desktop/Barcodelinux/nsbarcodEV.txt"
            'Dim filePath As String = mlinpath
            'Dim filePathname As String = mlinpath & "nsbarcodEV.txt"

            'Dim psi As New ProcessStartInfo()
            'psi.FileName = "/bin/bash"
            'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""
            ''psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
            'psi.UseShellExecute = False
            'psi.CreateNoWindow = True
            'TextBox1.Text = psi.FileName & psi.Arguments
            'Process.Start(psi)

            Dim printer As String = tscprinter2
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "nsbarcodEV.txt"
            PrintTscRaw(printer, filePathname)
            'PrintTscRaw("/dev/usb/lp0", "/home/user/label.txt")
        End If



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

        If mos = "WIN" Then
            If mcitrix = "Y" Then
                'citrixprint(mdir)
                citrixprint2(mdir, cmbvprinter.SelectedItem.ToString())
            Else
                Shell("""" & batPath & """ " & mdir, AppWinStyle.NormalFocus)
                'Shell("rawprv.bat " & mdir)
            End If

        Else
            'Dim printer As String = mprinter
            'Dim filePath As String = mlinpath
            'Dim filePathname As String = mlinpath & "Qrbarcode.txt"
            ''psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

            'Dim psi As New ProcessStartInfo()
            'psi.FileName = "/bin/bash"
            'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

            ''psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
            'psi.UseShellExecute = False
            'psi.CreateNoWindow = True
            ''TextBox1.Text = psi.FileName & psi.Arguments
            'Process.Start(psi)


            Dim printer As String = tscprinter1
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "Qrbarcode.txt"
            Dim success As Boolean = PrintTscRaw(printer, filePathname)

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

        If mos = "WIN" Then
            If mcitrix = "Y" Then
                'citrixprint(mdir)
                citrixprint2(mdir, cmbvprinter.SelectedItem.ToString())
            Else
                Shell("""" & batPath & """ " & mdir, AppWinStyle.NormalFocus)
                'Shell("rawprv.bat " & mdir)
            End If
        Else
            ''Qrbarcode.txt
            'Dim printer As String = mprinter
            'Dim filePath As String = mlinpath
            'Dim filePathname As String = mlinpath & "Qrbarcode.txt"
            ''psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

            'Dim psi As New ProcessStartInfo()
            'psi.FileName = "/bin/bash"
            'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

            ''psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
            'psi.UseShellExecute = False
            'psi.CreateNoWindow = True
            'TextBox1.Text = psi.FileName & psi.Arguments
            'Process.Start(psi)

            Dim printer As String = tscprinter1
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "Qrbarcode.txt"
            Dim success As Boolean = PrintTscRaw(printer, filePathname)

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
                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                    FileSystem.PrintLine(fNum, "SIZE 77.6 mm, 45 mm")
                Else
                    FileSystem.PrintLine(fNum, "SIZE 77.6 mm,43 mm")
                    FileSystem.PrintLine(fNum, "GAP 23.5 mm,0")
                End If
                FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum, "SPEED 7")
                FileSystem.PrintLine(fNum, "SET PEEL OFF")
                FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")

                If mos = "WIN" Then
                    FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='23.5 mm'></xpml>")
                End If


                FileSystem.PrintLine(fNum, "SET TEAR ON")
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
                        If mos = "WIN" Then
                            FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                        End If

                        mlno = 0
                        If j < Val(dg.Rows(i).Cells(14).Value) Then
                            'FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='23.5 mm'></xpml>SIZE 77.6 mm, 45 mm")
                            If mos = "WIN" Then
                                FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>")
                                FileSystem.PrintLine(fNum, "SIZE 77.6 mm, 45 mm")
                            Else
                                FileSystem.PrintLine(fNum, "SIZE 77.6 mm,43 mm")
                                FileSystem.PrintLine(fNum, "GAP 23.5 mm,0")
                            End If
                            FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                            FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                            FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                            FileSystem.PrintLine(fNum, "SPEED 7")
                            FileSystem.PrintLine(fNum, "SET PEEL OFF")
                            FileSystem.PrintLine(fNum, "SET CUTTER OFF")
                            FileSystem.PrintLine(fNum, "SET PARTIAL_CUTTER OFF")
                            If mos = "WIN" Then
                                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><page quantity='1' pitch='23.5 mm'></xpml>")
                            End If

                            FileSystem.PrintLine(fNum, "SET TEAR ON")
                            FileSystem.PrintLine(fNum, "CLS")
                            FileSystem.PrintLine(fNum, TAB(0), "CODEPAGE 1252")
                        End If


                    End If



                    If j >= Val(dg.Rows(i).Cells(14).Value) Then
                        mlno = 0
                        If (j Mod 2) <> 0 Then
                            FileSystem.PrintLine(fNum, "PRINT 1,1")
                            If mos = "WIN" Then
                                FileSystem.PrintLine(fNum, "<xpml></page></xpml><xpml><end/></xpml>")
                            End If
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
            If mos = "WIN" Then
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
            Else
                'Dim printer As String = mprinter
                'Dim filePath As String = mlinpath
                'Dim filePathname As String = mlinpath & "SHbarcodE.txt"
                ''psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

                'Dim psi As New ProcessStartInfo()
                'psi.FileName = "/bin/bash"
                'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""

                ''psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
                'psi.UseShellExecute = False
                'psi.CreateNoWindow = True
                'TextBox1.Text = psi.FileName & psi.Arguments
                'Process.Start(psi)

                Dim printer As String = tscprinter1
                Dim filePath As String = mlinpath
                Dim filePathname As String = mlinpath & "SHbarcodE.txt"
                Dim success As Boolean = PrintTscRaw(printer, filePathname)


            End If

        End If

        cmbfit.Text = ""
        'chkliberty.Checked = False
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim f2 As New Frmxlsrbarcode()
        f2.Show()
    End Sub

    Private Sub Btndupdel_Click(sender As System.Object, e As System.EventArgs) Handles Btndupdel.Click
        remdupdg(dg)
    End Sub
End Class
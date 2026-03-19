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



Public Class Frmmktbarcode
    Private printDocument As New PrintDocument()
    Dim mfile, fwash, fsilk, fpant, mprinter, mtype As String
    Dim x, y, n, mqty, lstcol As Integer
    Private rowFilterText As String = String.Empty
    Private rowIndex As Integer = 0
    Dim mfil, mfildet, msql, prnon, msql2, mperiod, mdir As String

    Dim objsetting As New Printing.PrinterSettings
    Dim strPrinter As String
    Dim ci As CultureInfo = New CultureInfo("en-IN")
    Private printerIp As String
    Private printerPort As Integer
    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal PSZpRINTER As String) As Boolean
    Private Sub Frmmktbarcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim x As Integer = (Me.ClientSize.Width - Groupbox1.Width) / 2
        Dim y As Integer = (Me.ClientSize.Height - Groupbox1.Height) / 2
        Groupbox1.Location = New Point(x, y)


        Btnclear.PerformClick()
        Call loadprinter()
        mdbserver = System.Configuration.ConfigurationSettings.AppSettings("myservername")
        mdbname = ConfigurationSettings.AppSettings("mydbname")
        mdbuserid = ConfigurationSettings.AppSettings("userid")
        mdbpwd = decodefilesql(ConfigurationSettings.AppSettings("mypwd"))

        fwash = System.Configuration.ConfigurationSettings.AppSettings("Washcarefile")
        fsilk = ConfigurationSettings.AppSettings("Silkfile")
        fpant = ConfigurationSettings.AppSettings("Pantfile")
        mprinter = ConfigurationSettings.AppSettings("Printername")
        mperiod = ConfigurationSettings.AppSettings("period")
        cmbprinter.Text = mprinter

        OptSales.Checked = True

        'dtp.MinDate = New DateTime(1753, 1, 1)
        'dtp.MaxDate = DateTime.Today
        'dtp.Format = DateTimePickerFormat.Custom
        'dtp.CustomFormat = "yyyy"
        'dtp.Value = Today
        Call loadprinter()
        cmbprinter.Text = mprinter

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
    End Sub

    Private Sub dtp_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtp.ValueChanged
        txtyr.Text = Year(dtp.Value)
    End Sub


    Private Sub loadprinter()
        Dim pkInstalledPrinters As String

        ' Find all printers installed
        For Each pkInstalledPrinters In PrinterSettings.InstalledPrinters
            cmbprinter.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters

        ' Set the combo to the first printer in the list
        cmbprinter.SelectedIndex = -1
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
            msql = "Exec insertbartemp " & Val(txtdocnum.Text) & "," & Val(txtmont.Text) & "," & Val(txtyr.Text) & ",'" & Trim(prnon) & "',1"
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
            msql2 = "select month(docdate) monn,docentry,year(docdate) yr,cardcode,cardname from oinv where docnum=" & Val(txtdocnum.Text) & " and pindicator='" & Trim(cmbyr.Text) & "'"

        ElseIf optdateord.Checked = True Then  ' "DATE ORDER" 
            msql2 = "select month(docdate) monn,docentry,year(docdate) yr,cardcode,cardname from odln where docnum=" & Val(txtdocnum.Text) & " and pindicator='" & Trim(cmbyr.Text) & "'"

        ElseIf optsdraft.Checked = True Then ' "INV DRAFT" 
            msql2 = "select month(docdate) monn,docentry,year(docdate) yr,cardcode,cardname from odrf where docnum=" & Val(txtdocnum.Text) & " and pindicator='" & Trim(cmbyr.Text) & "'"

        ElseIf optsample.Checked = True Then  ' "SAMPLE" 
            msql2 = "select month(docdate) monn,docentry,year(docdate) yr,cardcode,cardname from v_sampoinv where docnum=" & Val(txtdocnum.Text) & " and pindicator='" & Trim(cmbyr.Text) & "'"
            'mfil = "V_sampinv1"
            'mfildet = "v_sampOinv"
            'prnon = "SAMPLE"
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

    Private Sub txtdocnum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdocnum.TextChanged

    End Sub

    Private Sub Groupbox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Groupbox1.Enter

    End Sub
    Private Sub loaddata()
        Call crtmptabst()
        dg.Rows.Clear()
        If Len(Trim(cmbtype.Text)) > 0 Then

            If Trim(cmbtype.Text) = "Dealer" Or Trim(cmbtype.Text) = "TN" Then
                msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,barcode2 mbarcode,barcode2 txbarcode, boxmrp,mrp,boxqty,quantity from bartemp where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"
            ElseIf Trim(cmbtype.Text) = "Showroom" Then
                msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,autocode mbarcode,autocode txbarcode, boxmrp,mrp,boxqty,quantity from bartemp with (nolock) where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"
            ElseIf Trim(cmbtype.Text) = "Franchise" Then
                msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,autocode mbarcode,autocode txbarcode, boxmrp,mrp,boxqty,quantity from bartemp with (nolock) where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"
            ElseIf Trim(cmbtype.Text) = "OS" Then 'u_remarks
                msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,u_remarks mbarcode,u_remarks txbarcode, boxmrp,mrp,boxqty,quantity from bartemp with (nolock) where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"

            ElseIf Trim(cmbtype.Text) = "Distributor" Then
                msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,odoocode mbarcode,odoocode txbarcode, boxmrp,mrp,boxqty,quantity from bartemp with (nolock) where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"
            ElseIf Trim(cmbtype.Text) = "Pothys" Then
                msql = "select docnum,u_subgrp6,u_style,u_size,color,u_itemgrp,mfd,cstype,autocode mbarcode,autocode txbarcode, boxmrp,mrp,boxqty,quantity from bartemp with (nolock) where docentry=" & Val(lbldocentry.Text) & " and month(docdate)=" & Val(txtmont.Text) & " and year(docdate)=" & Val(txtyr.Text) & " order by linenum"

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

                Next
            Else
                'txtmont.Text = 0
            End If
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
        Call loaddata()
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        'printsticker()
        speedprint()
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

    Private Sub speedprint()
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

                FileSystem.PrintLine(fNum, "<xpml><page quantity='0' pitch='45.0 mm'></xpml>SIZE 69.10 mm, 45 mm")
                FileSystem.PrintLine(fNum, "DIRECTION 0,0")
                FileSystem.PrintLine(fNum, "REFERENCE 0,0")
                FileSystem.PrintLine(fNum, "OFFSET 0 mm")
                FileSystem.PrintLine(fNum, "SPEED 10")
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
                FileSystem.PrintLine(fNum, TAB(0), "TEXT 385,339," & """" & "0" & """" & ",180,13,12," & """" & "MRP:" & """")
                'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "IndianRupee.TTF" & """" & ",180,32,1," & """" & "`" & """")

                'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "ITF Rupee.TTF" & """" & ",180,32,1," & """" & "S" & """")
                'PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & "₹" & """")

                'FileSystem.PrintLine(1, TAB(0), "TEXT 283,312," & """" & "0" & """" & ",180,32,1," & """" & rupeeSymbol & """")

                FileSystem.PrintLine(fNum, "TEXT 277,339," & """" & "0" & """" & ",180,14,12," & """" & Trim(dg.Rows(i).Cells(12).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 538,291," & """" & "0" & """" & ",180,11,12," & """" & Trim(dg.Rows(i).Cells(2).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 538,258," & """" & "0" & """" & ",180,10,9," & """" & "COL:" & """")
                FileSystem.PrintLine(fNum, "TEXT 538,230," & """" & "0" & """" & ",180,8,9," & """" & "CONTENT :" & """")
                FileSystem.PrintLine(fNum, "TEXT 476,258," & """" & "ROMAN.TTF" & """" & ",180,1,9," & """" & Trim(dg.Rows(i).Cells(5).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 430,230," & """" & "0" & """" & ",180,9,9," & """" & Trim(dg.Rows(i).Cells(6).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 182,252," & """" & "ROMAN.TTF" & """" & ",180,1,11," & """" & Trim(dg.Rows(i).Cells(3).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 103,249," & """" & "0" & """" & ",180,8,10," & """" & "SLEEVE" & """")
                FileSystem.PrintLine(fNum, "TEXT 182,213," & """" & "0" & """" & ",180,11,20," & """" & "SIZE:" & """")
                FileSystem.PrintLine(fNum, "TEXT 116,216," & """" & "0" & """" & ",180,16,21," & """" & Trim(dg.Rows(i).Cells(4).Value) & """")
                FileSystem.PrintLine(fNum, "TEXT 70,217," & """" & "0" & """" & ",180,12,21," & """" & "cm" & """")
                If chktwin.Checked = True Then
                    FileSystem.PrintLine(fNum, "TEXT 126,158," & """" & "0" & """" & ",180,12,9," & """" & "Qty :2N" & """")
                Else
                    FileSystem.PrintLine(fNum, "TEXT 126,158," & """" & "0" & """" & ",180,12,9," & """" & "Qty :1N" & """")
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
            If MsgBox("Lpt Port", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text) & ":")
                Shell("rawpr.bat " & mdir)

            Else
                Dim text As String = File.ReadAllText(mdir, System.Text.Encoding.GetEncoding(1252))
                Dim pd As PrintDialog = New PrintDialog()
                pd.PrinterSettings = New PrinterSettings()
                BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName, text)
                'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
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

    End Sub

    Private Sub Btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnclear.Click
        txtyr.Text = ""
        txtmont.Text = ""
        lbldocentry.Text = ""
        lblcardcode.Text = ""
        lblcardname.Text = ""
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
End Class
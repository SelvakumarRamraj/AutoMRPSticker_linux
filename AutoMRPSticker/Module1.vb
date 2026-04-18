Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports CarlosAg.ExcelXmlWriter
Imports System.IO.MemoryStream
'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.ReportSource
'Imports CrystalDecisions.CrystalReports.Engine.Section
'Imports CrystalDecisions.CrystalReports.Engine.Sections

Imports System.Drawing.Drawing2D
Imports System.Collections.Specialized
Imports System.Security
Imports System.Text
Imports System.Net.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Net.Mail.MailMessage
Imports System.Net.Mail.Attachment
Imports System.Net
Imports Microsoft.VisualBasic
Imports System.Management
Imports System.Drawing.Printing
Imports System.Drawing
Imports AxMSFlexGridLib
Imports NPOI.SS.UserModel
Imports NPOI.HSSF.UserModel
Imports NPOI.XSSF.UserModel
Imports Newtonsoft.Json
Imports System.Threading.Tasks

Module Module1
    Public mdbserver As String
    Public mdbname As String
    Public mdbuserid As String
    Public mdbpwd As String
    Public mprinter As String
    Dim tmpp As String
    Public mos As String
    Public mlinpath As String
    Public mperiod As String
    Public printerIp As String
    Public printerPort As String
    Public mcitrix As String
    Public mlpt As String
    Public mvertprinter As String
    Public mProdMktbarcode As String
    Public mproduction As String
    Public mlsprinter As String
    'transport
    Public DBINV As String
    Public DBINVPrint As String
    Public DBRINVPrint As String
    Public DBFRW As String
    Public DBllr As String
    Public dbcour As String
    Public dbtripsummary As String
    Public dblrpass As String
    Public dbGPSU As String
    Public dbcard As String
    Public dbobp As String
    Public DBTRANS As String
    Public mcmpid As String
    Public prntername As String
    Public mreppath As String
    Public tscprinter1 As String
    Public tscprinter2 As String
    Public mprintapi As String
    Public mgpfrom As String
    Public dbmGATEPASS As String
    Public dbGATEPASS As String
    Public dbcomp As String
    Public maddress As String
    Public mapppath As String
    Public mapiurl As String
    Public mlintmpfolder As String
    Public mxlfilepath As String
    Public mbarmsg As String
    Public mautoscan As String
    '<STAThread()>
    Sub main()
        mdbserver = System.Configuration.ConfigurationSettings.AppSettings("myservername")
        mdbname = ConfigurationSettings.AppSettings("mydbname")
        mdbuserid = ConfigurationSettings.AppSettings("userid")
        mdbpwd = decodefilesql(ConfigurationSettings.AppSettings("mypwd"))

        'fwash = System.Configuration.ConfigurationSettings.AppSettings("Washcarefile")
        'fsilk = ConfigurationSettings.AppSettings("Silkfile")
        'fpant = ConfigurationSettings.AppSettings("Pantfile")
        mprinter = ConfigurationSettings.AppSettings("Printername")
        mperiod = ConfigurationSettings.AppSettings("period")
        printerIp = ConfigurationSettings.AppSettings("printerip")
        printerPort = ConfigurationSettings.AppSettings("prnport")
        mcitrix = ConfigurationSettings.AppSettings("Citrix")
        mlpt = ConfigurationSettings.AppSettings("PrintLpt")
        mos = ConfigurationSettings.AppSettings("OS")
        mlinpath = ConfigurationSettings.AppSettings("Linuxfilepath")
        mProdMktbarcode = ConfigurationSettings.AppSettings("ProdMktbarcode")
        mproduction = ConfigurationSettings.AppSettings("Production")
        mlsprinter = ConfigurationSettings.AppSettings("LaserPrinter")
        mvertprinter = ConfigurationSettings.AppSettings("Printername_Vertical")

        'transport
        DBINV = ConfigurationSettings.AppSettings("INV")
        DBINVPrint = ConfigurationSettings.AppSettings("INVPRINT")
        DBRINVPrint = ConfigurationSettings.AppSettings("RINVPRINT")
        DBTRANS = ConfigurationSettings.AppSettings("TRANSPORT")
        DBFRW = ConfigurationSettings.AppSettings("FRW")
        dbcour = ConfigurationSettings.AppSettings("cour")
        dbcomp = ConfigurationSettings.AppSettings("Companycode")
        ' dbperiod = ConfigurationSettings.AppSettings("PERIOD")
        dbGATEPASS = ConfigurationSettings.AppSettings("GATEPASS")
        dbmGATEPASS = ConfigurationSettings.AppSettings("mGATEPASS")
        dbtripsummary = ConfigurationSettings.AppSettings("tripsummary")
        dblrpass = ConfigurationSettings.AppSettings("lrpass")
        dbGPSU = ConfigurationSettings.AppSettings("GPSu")
        dbobp = ConfigurationSettings.AppSettings("orderbackprint")
        dbobp = ConfigurationSettings.AppSettings("orderbackprint")
        tscprinter1 = ConfigurationSettings.AppSettings("TSCPrinter_1")
        tscprinter2 = ConfigurationSettings.AppSettings("TSCPrinter_2")
        mprintapi = ConfigurationSettings.AppSettings("PrintAPI")
        mgpfrom = ConfigurationSettings.AppSettings("GPFrom")
        dbcard = ConfigurationSettings.AppSettings("card")
        maddress = Trim(ConfigurationSettings.AppSettings("address"))
        mapppath = Trim(ConfigurationSettings.AppSettings("AppPath"))

        mapiurl = Trim(ConfigurationSettings.AppSettings("PrintAPI"))
        mlintmpfolder = Trim(ConfigurationSettings.AppSettings("tempfolder"))
        mxlfilepath = Trim(ConfigurationSettings.AppSettings("XLfilepath"))
        mbarmsg = ConfigurationSettings.AppSettings("barmsg")
        mautoscan = ConfigurationSettings.AppSettings("auto")

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        ' Show splash
        Dim splash As New SplashScreen1
        splash.Show()
        Application.DoEvents()

        ' Simulate loading
        Threading.Thread.Sleep(1000)

        splash.Close()

        ' 🔥 IMPORTANT: Run MDI FORM here
        Application.Run(New MDIParent1())


        'MDIParent1.Show()
    End Sub

    Public Function decodefile(ByVal srcfile As String) As String

        Dim decodedBytes As Byte()
        decodedBytes = Convert.FromBase64String(Decode(srcfile))

        Dim decodedText As String
        decodedText = Encoding.UTF8.GetString(decodedBytes)
        decodefile = decodedText
    End Function
    Public Function decodefilesql(ByVal srcfile As String) As String

        Dim decodedBytes As Byte()
        decodedBytes = Convert.FromBase64String(Decode(srcfile))

        Dim decodedText As String
        decodedText = Encoding.UTF8.GetString(decodedBytes)
        decodefilesql = decodedText
    End Function

    'Sub EncodeFile(ByVal srcFile As String, ByVal destfile As String)
    Public Function encodefile(ByVal srcfile As String) As String

        Dim bytesToEncode As Byte()
        bytesToEncode = Encoding.UTF8.GetBytes(srcfile)

        Dim encodedText As String
        encodedText = Convert.ToBase64String(bytesToEncode)
        encodefile = Encript(encodedText)
    End Function
    Public Function encodefilesql(ByVal srcfile As String) As String

        Dim bytesToEncode As Byte()
        bytesToEncode = Encoding.UTF8.GetBytes(srcfile)

        Dim encodedText As String
        encodedText = Convert.ToBase64String(bytesToEncode)
        encodefilesql = encodedText
    End Function
    Public Function Decode(ByVal Password As String) As String
        'Dim I As Integer
        Dim TMP As Long
        tmpp = ""
        For i = 1 To Len(Password)
            TMP = Asc(Mid(Password, i, 1))
            TMP = TMP - i
            tmpp = Trim(tmpp) & Chr(TMP)
            'Decode = Decode & Chr(TMP)
        Next i
        Decode = tmpp
        Return Decode
    End Function
    Public Function Encript(ByVal Password As String) As String
        ' Dim I As Integer
        'Dim tmpp As String
        Dim TMP As Long
        tmpp = ""
        For i = 1 To Len(Password)
            TMP = Asc(Mid(Password, i, 1))
            TMP = TMP + i
            tmpp = Trim(tmpp) + Chr(TMP)
            'Encript = Encript & Chr(TMP)
        Next i
        Encript = tmpp
        Return Encript
    End Function


    Function RupeesToWord(ByVal MyNumber)
        Dim Temp
        Dim Rupees, Paisa As String
        Dim DecimalPlace, iCount
        Dim Hundreds, Words As String
        Dim place(9) As String
        place(0) = " Thousand "
        place(2) = " Lakh "
        place(4) = " Crore "
        place(6) = " Arab "
        place(8) = " Kharab "
        On Error Resume Next
        ' Convert MyNumber to a string, trimming extra spaces.
        MyNumber = Trim(Str(MyNumber))

        ' Find decimal place.
        DecimalPlace = InStr(MyNumber, ".")

        ' If we find decimal place...
        If DecimalPlace > 0 Then
            ' Convert Paisa
            Temp = Left(Mid(MyNumber, DecimalPlace + 1) & "00", 2)
            Paisa = " And " & ConvertTens(Temp) & " Paisa"

            ' Strip off paisa from remainder to convert.
            MyNumber = Trim(Left(MyNumber, DecimalPlace - 1))
        End If

        '===============================================================
        Dim TM As String  ' If MyNumber between Rs.1 To 99 Only.
        TM = Right(MyNumber, 2)

        If Len(MyNumber) > 0 And Len(MyNumber) <= 2 Then
            If Len(TM) = 1 Then
                Words = ConvertDigit(TM)
                RupeesToWord = "Rupees :  " & Words & Paisa & " Only"

                Exit Function

            Else
                If Len(TM) = 2 Then
                    Words = ConvertTens(TM)
                    RupeesToWord = "Rupees : " & Words & Paisa & " Only"
                    Exit Function

                End If
            End If
        End If
        '===============================================================


        ' Convert last 3 digits of MyNumber to ruppees in word.
        Hundreds = ConvertHundreds(Right(MyNumber, 3))
        ' Strip off last three digits
        MyNumber = Left(MyNumber, Len(MyNumber) - 3)

        iCount = 0
        Do While MyNumber <> ""
            'Strip last two digits
            Temp = Right(MyNumber, 2)
            If Len(MyNumber) = 1 Then


                If Trim(Words) = "Thousand" Or
                Trim(Words) = "Lakh  Thousand" Or
                Trim(Words) = "Lakh" Or
                Trim(Words) = "Crore" Or
                Trim(Words) = "Crore  Lakh  Thousand" Or
                Trim(Words) = "Arab  Crore  Lakh  Thousand" Or
                Trim(Words) = "Arab" Or
                Trim(Words) = "Kharab  Arab  Crore  Lakh  Thousand" Or
                Trim(Words) = "Kharab" Then

                    Words = ConvertDigit(Temp) & place(iCount)
                    MyNumber = Left(MyNumber, Len(MyNumber) - 1)

                Else

                    Words = ConvertDigit(Temp) & place(iCount) & Words
                    MyNumber = Left(MyNumber, Len(MyNumber) - 1)

                End If
            Else

                If Trim(Words) = "Thousand" Or
                   Trim(Words) = "Lakh  Thousand" Or
                   Trim(Words) = "Lakh" Or
                   Trim(Words) = "Crore" Or
                   Trim(Words) = "Crore  Lakh  Thousand" Or
                   Trim(Words) = "Arab  Crore  Lakh  Thousand" Or
                   Trim(Words) = "Arab" Then


                    Words = ConvertTens(Temp) & place(iCount)


                    MyNumber = Left(MyNumber, Len(MyNumber) - 2)
                Else

                    '=================================================================
                    ' if only Lakh, Crore, Arab, Kharab

                    If Trim(ConvertTens(Temp) & place(iCount)) = "Lakh" Or
                       Trim(ConvertTens(Temp) & place(iCount)) = "Crore" Or
                       Trim(ConvertTens(Temp) & place(iCount)) = "Arab" Then

                        Words = Words
                        MyNumber = Left(MyNumber, Len(MyNumber) - 2)
                    Else
                        Words = ConvertTens(Temp) & place(iCount) & Words
                        MyNumber = Left(MyNumber, Len(MyNumber) - 2)
                    End If

                End If
            End If

            iCount = iCount + 2
        Loop

        RupeesToWord = "Rupees : " & Words & Hundreds & " Only"
    End Function

    ' Conversion for hundreds
    '*****************************************
    Private Function ConvertHundreds(ByVal MyNumber)
        Dim Result As String

        ' Exit if there is nothing to convert.
        If Val(MyNumber) = 0 Then Exit Function

        ' Append leading zeros to number.
        MyNumber = Right("000" & MyNumber, 3)

        ' Do we have a hundreds place digit to convert?
        If Left(MyNumber, 1) <> "0" Then
            Result = ConvertDigit(Left(MyNumber, 1)) & " Hundreds "
        End If

        ' Do we have a tens place digit to convert?
        If Mid(MyNumber, 2, 1) <> "0" Then
            Result = Result & ConvertTens(Mid(MyNumber, 2))
        Else
            ' If not, then convert the ones place digit.
            Result = Result & ConvertDigit(Mid(MyNumber, 3))
        End If

        ConvertHundreds = Trim(Result)
    End Function

    ' Conversion for tens
    '*****************************************
    Private Function ConvertTens(ByVal MyTens)
        Dim Result As String

        ' Is value between 10 and 19?
        If Val(Left(MyTens, 1)) = 1 Then
            Select Case Val(MyTens)
                Case 10 : Result = "Ten"
                Case 11 : Result = "Eleven"
                Case 12 : Result = "Twelve"
                Case 13 : Result = "Thirteen"
                Case 14 : Result = "Fourteen"
                Case 15 : Result = "Fifteen"
                Case 16 : Result = "Sixteen"
                Case 17 : Result = "Seventeen"
                Case 18 : Result = "Eighteen"
                Case 19 : Result = "Nineteen"
                Case Else
            End Select
        Else
            ' .. otherwise it's between 20 and 99.
            Select Case Val(Left(MyTens, 1))
                Case 2 : Result = "Twenty "
                Case 3 : Result = "Thirty "
                Case 4 : Result = "Forty "
                Case 5 : Result = "Fifty "
                Case 6 : Result = "Sixty "
                Case 7 : Result = "Seventy "
                Case 8 : Result = "Eighty "
                Case 9 : Result = "Ninety "
                Case Else
            End Select

            ' Convert ones place digit.
            Result = Result & ConvertDigit(Right(MyTens, 1))
        End If

        ConvertTens = Result
    End Function

    Private Function ConvertDigit(ByVal MyDigit)
        Select Case Val(MyDigit)
            Case 1 : ConvertDigit = "One"
            Case 2 : ConvertDigit = "Two"
            Case 3 : ConvertDigit = "Three"
            Case 4 : ConvertDigit = "Four"
            Case 5 : ConvertDigit = "Five"
            Case 6 : ConvertDigit = "Six"
            Case 7 : ConvertDigit = "Seven"
            Case 8 : ConvertDigit = "Eight"
            Case 9 : ConvertDigit = "Nine"
            Case Else : ConvertDigit = ""
        End Select
    End Function

    Function GetPrinterNamesWithSessions() As List(Of String)
        Dim printerNamesWithSessions As New List(Of String)

        Try
            ' Query WMI for all printers
            Dim query As String = "SELECT Name FROM Win32_Printer"
            Dim searcher As New ManagementObjectSearcher(query)

            ' Loop through the printer objects
            For Each printer As ManagementObject In searcher.Get()
                Dim printerName As String = printer("Name").ToString()

                ' Check if the printer name contains session info
                If printerName.Contains("in session") Then
                    printerNamesWithSessions.Add(printerName)
                End If
            Next

        Catch ex As Exception
            Console.WriteLine("Error: {ex.Message}")
        End Try

        Return printerNamesWithSessions
    End Function

    Public Sub loadcomboyr(ByVal mtable As String, ByVal combofield As String, ByVal mycombo As ComboBox)
        Dim msql As String

        msql = " select  CASE when LEFT(code,2)='FY' then  CONVERT(nvarchar(4),year(f_taxdate))+'-'+right(CONVERT(nvarchar(4),year(t_taxdate)),2) else code end code from " & Trim(mtable) & vbCrLf _
             & "group by CASE when LEFT(code,2)='FY' then  CONVERT(nvarchar(4),year(f_taxdate))+'-'+right(CONVERT(nvarchar(4),year(t_taxdate)),2) else code end "

        'msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " WHERE CMP_ID='" & mcmpid & "' group by " & Trim(mfield) & " order by " & Trim(mfield)

        Dim cmd As New SqlCommand(msql, con)
        'Dim CMD As New OleDb.OleDbCommand("SELECT sectionname FROM section GROUP BY sectionname ORDER BY sectionname", con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        ''Dim DR As SqlDataReader
        Dim DR As SqlDataReader
        DR = cmd.ExecuteReader
        If DR.HasRows = True Then
            mycombo.Items.Clear()
            While DR.Read
                mycombo.Items.Add(DR.Item(Trim(combofield)))
            End While
        End If
        DR.Close()
        cmd.Dispose()
    End Sub

    Public Function CLEAR(ByVal frm As Form)
        Dim I As Integer
        On Error Resume Next

        With frm
            For I = 0 To .Controls.Count - 1
                If TypeOf .Controls(I) Is TextBox Then
                    .Controls(I).Text = ""
                ElseIf TypeOf .Controls(I) Is ListBox Then
                    '.Controls(I).SelectedIndex = -1
                    .Controls(I).Text = ""
                ElseIf TypeOf .Controls(I) Is ComboBox Then
                    '.Controls(I).SELECTEDINDEX = -1
                    .Controls(I).Text = ""
                ElseIf TypeOf .Controls(I) Is MaskedTextBox Then
                    '.Controls(I).Mask = ""
                    '.Controls(I).Mask = "" - "" - """"
                    .Controls(I).Text = "__-__-____"
                    '.Controls(I).Mask = "##-##-####"
                    '.Controls(i).Text = "__-__-____"
                ElseIf TypeOf .Controls(I) Is CheckBox Then
                    ' .Controls(I).CHECKED = False
                    '.Controls(I).CheckState.Checked = CheckState.Unchecked

                ElseIf TypeOf .Controls(I) Is RadioButton Then
                    '.Controls(I).CHECKED = False
                    'ElseIf TypeOf .Controls(I) Is Label Then
                    '    .Controls(I).Caption = ""
                ElseIf TypeOf .Controls(I) Is Label Then
                    ' .Controls(I).Text = ""
                ElseIf TypeOf .Controls(I) Is Panel Then

                End If
            Next
        End With
        Return True
    End Function




    Public Function disable(ByVal Frm As Form)
        On Error Resume Next
        With Frm
            For I = 0 To .Controls.Count - 1
                If TypeOf .Controls(I) Is TextBox Then
                    .Controls(I).Enabled = False
                ElseIf TypeOf .Controls(I) Is ListBox Then
                    '.Controls(i).ListIndex = -1
                    '.Controls(i).Text = ""
                    .Controls(I).Enabled = False
                ElseIf TypeOf .Controls(I) Is ComboBox Then
                    .Controls(I).Enabled = False
                ElseIf TypeOf .Controls(I) Is MaskedTextBox Then
                    .Controls(I).Enabled = False
                    '.Controls(i).Text = "__-__-____"
                ElseIf TypeOf .Controls(I) Is AxMSFlexGrid Then
                    .Controls(I).Enabled = False
                ElseIf TypeOf .Controls(I) Is UserControl Then
                    .Controls(I).Enabled = False
                ElseIf TypeOf .Controls(I) Is CheckBox Then
                    .Controls(I).Enabled = False
                ElseIf TypeOf .Controls(I) Is RadioButton Then
                    .Controls(I).Enabled = False
                End If
            Next
        End With
        Return True
    End Function
    Public Function enable(ByVal Frm As Form)
        On Error Resume Next
        With Frm
            For I = 0 To .Controls.Count - 1
                If TypeOf .Controls(I) Is TextBox Then
                    .Controls(I).Enabled = True
                ElseIf TypeOf .Controls(I) Is ListBox Then
                    '.Controls(i).ListIndex = -1
                    '.Controls(i).Text = ""
                    .Controls(I).Enabled = True
                ElseIf TypeOf .Controls(I) Is ComboBox Then
                    .Controls(I).Enabled = True
                ElseIf TypeOf .Controls(I) Is MaskedTextBox Then
                    .Controls(I).Enabled = True
                    '.Controls(i).Text = "__-__-____"
                ElseIf TypeOf .Controls(I) Is AxMSFlexGrid Then
                    .Controls(I).Enabled = True
                ElseIf TypeOf .Controls(I) Is UserControl Then
                    .Controls(I).Enabled = True
                ElseIf TypeOf .Controls(I) Is CheckBox Then
                    .Controls(I).Enabled = True
                ElseIf TypeOf .Controls(I) Is RadioButton Then
                    .Controls(I).Enabled = True
                End If
            Next
        End With
        Return True
    End Function


    Public Sub exportexcelData(ByVal mtable As String, ByVal mwhere As String, ByVal morder As String)
        Dim mstrr As String
        Dim strData As String = ""
        Dim bolFirstPass As Boolean = True

        Dim ldir, lmdir As String
        If mos = "WIN" Then
            ldir = System.AppDomain.CurrentDomain.BaseDirectory()
            lmdir = Trim(ldir) & "barcod.xls"
        Else
            lmdir = mxlfilepath & "barcode.xls"
        End If


        Dim book As Workbook = New Workbook
        'Dim oCn As SqlConnection = Nothing
        Dim oCmd As SqlCommand = Nothing
        Dim oDr As SqlDataReader = Nothing


        Try
            'oCn = New SqlConnection("YOUR CONNECTION STRING ")
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            mstrr = "select * from bardet"
            If Len(Trim(mwhere)) > 0 Then
                mstrr = mstrr & " Where " & mwhere
            End If
            If Len(Trim(morder)) > 0 Then
                mstrr = mstrr & " order by " & morder
            End If

            oCmd = New SqlCommand(mstrr, con)
            'Dim da As SqlDataAdapter = New SqlDataAdapter(oCmd)
            'Dim dt As DataTable = New DataTable()
            'da.Fill(dt)
            'DatatableToExcel(dt)
            Dim sheet As Worksheet = book.Worksheets.Add("SampleSheet")
            oDr = oCmd.ExecuteReader(CommandBehavior.CloseConnection)
            While oDr.Read
                Dim Row0 As WorksheetRow = sheet.Table.Rows.Add


                For i As Integer = 0 To oDr.FieldCount - 1
                    'oDr.GetName(i)
                    Row0.Cells.Add(oDr.Item(oDr.GetName(i)))
                    'strData &= "<td>" & Replace(Replace(oDr.GetName(i), "[", ""), "]", "") & "</td>"
                Next

            End While
            book.Save(lmdir)

            If mos = "WIN" Then
                'open file
                Process.Start(lmdir)
            Else
                OpenWithLibreOffice(lmdir)
            End If

            'Response.Write("</table></body>")
            'Response.End()
        Catch ex As Exception
            If Not ex.Message.Contains("Thread was being aborted.") Then
                '_'global.WriteErrorLog("exportData.loadPage() failed! Error is: " & ex.Message)
                MsgBox(ex.Message)
            End If
        Finally
            If Not con Is Nothing Then If con.State = ConnectionState.Open Then con.Close()
            oDr.Close()
        End Try
    End Sub


    Public Sub exeltoflx(ByVal CTRL As AxMSFlexGrid)
        Dim jCol, Values_Col, iRow, Values_Text

        CTRL.Rows = 1
        CTRL.Cols = 0


        Values_Col = Split(Clipboard.GetText, vbCr)

        For jCol = 0 To UBound(Values_Col) - 1
            CTRL.Rows = CTRL.Rows + 1
            CTRL.Row = CTRL.Rows - 1
            Values_Text = Split(Values_Col(jCol), vbTab)
            For iRow = 0 To UBound(Values_Text)
                If jCol = 0 Then
                    CTRL.Cols = CTRL.Cols + 1
                End If
                CTRL.set_TextMatrix(CTRL.Row, iRow, LTrim(RTrim(Values_Text(iRow))))
                'CTRL.set_TextMatrix(CTRL.Row, iRow, Replace(LTrim(RTrim(Values_Text(iRow))), Chr(10), ""))

                'CTRL.TextMatrix(CTRL.Row, iRow) = LTrim(RTrim(Values_Text(iRow)))
            Next
        Next



    End Sub

    Public Function findid() As String
        '(@Param1) or (@Param1,@Parm2,...)
        Dim strsql As String = "SELECT newid() as tid"
        'Dim strSQL = "SELECT newid() as tid" & "(@Param1)"
        Dim cmdfid As SqlCommand = New SqlCommand(strsql, con)
        'cmd.Parameters.Add(New SqlParameter("@Param1", _
        'SqlDbType.Text)).Value = "Hello"
        If con2.State = ConnectionState.Closed Then
            con2.Open()
        End If
        'cmdfid.Transaction = TRANS
        Dim val As String = Replace(cmdfid.ExecuteScalar().ToString, "-", "").ToUpper
        con2.Close()

        findid = val
        cmdfid.Dispose()

        'MsgBox("Value is: " & val)
    End Function
    Public Function getid(ByVal mtab_name As String, ByVal mfieldname As String, ByVal mfindfield As String, ByVal mfindname As String) As String
        '(@Param1) or (@Param1,@Parm2,...)
        Dim strsql As String = "select " & mfieldname & " from " & mtab_name & " where " & mfindfield & "='" & mfindname & "'"
        'Dim strsql As String = "SELECT newid() as tid"
        'Dim strSQL = "SELECT newid() as tid" & "(@Param1)"
        Dim cmd As SqlCommand = New SqlCommand(strsql, con)
        'cmd.Parameters.Add(New SqlParameter("@Param1", _
        'SqlDbType.Text)).Value = "Hello"
        If con2.State = ConnectionState.Closed Then
            con2.Open()
        End If
        Dim val As String = Replace(cmd.ExecuteScalar().ToString, "-", "").ToUpper
        con2.Close()
        cmd.Dispose()
        getid = val
        'MsgBox("Value is: " & val)
    End Function
    'Public Function GetDefaultPrinterName() As String
    '    Try
    '        ' Create a PrinterSettings object
    '        Using settings As New PrinterSettings()
    '            ' Get the default printer name
    '            Return settings.PrinterName
    '        End Using
    '    Catch ex As Exception
    '        ' Handle exceptions, if any
    '    Console.WriteLine($"Error: {ex.Message}")
    '        Return String.Empty
    '    End Try
    'End Function




    Public Function GetDefaultPrinterName() As String
        Try
            Dim query As New SelectQuery("SELECT * FROM Win32_Printer WHERE Default = True")
            Using searcher As New ManagementObjectSearcher(query)
                For Each printer As ManagementObject In searcher.Get()
                    Return printer("Name").ToString()
                Next
            End Using
        Catch ex As Exception
            Console.WriteLine("Error: {ex.Message}")
        End Try
        Return String.Empty
    End Function


    'Public Sub loadcomboshow(ByVal mtable As String, ByVal combofield As String, ByVal mycombo As ComboBox, ByVal grpfield As String, ByVal wherfield As String)
    '    Dim msql As String
    '    'msql = mqry
    '    'msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " WHERE CMP_ID='" & mcmpid & "' group by " & Trim(mfield) & " order by " & Trim(mfield)

    '    If Len(Trim(wherfield)) > 0 Then
    '        If Len(Trim(grpfield)) > 0 Then
    '            msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " where cardtype='C' group by " & Trim(grpfield) & " order by " & Trim(grpfield)
    '        Else
    '            msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " where cardtype='C' group by " & Trim(grpfield) & " order by " & Trim(grpfield)
    '        End If

    '    Else
    '        If Len(Trim(grpfield)) > 0 Then
    '            msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " where cardtype ='C'  group by " & Trim(grpfield) & " order by " & Trim(grpfield)
    '        Else
    '            msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " where cardtype='C'  group by " & Trim(grpfield) & " order by " & Trim(grpfield)
    '        End If
    '    End If

    '    Dim dtc As DataTable = getDataTable(msql)
    '    mycombo.DataSource = dtc
    '    mycombo.DisplayMember = "CustomerName"  ' text shown to user
    '    mycombo.ValueMember = "CustomerID"      ' underlying value
    '    mycombo.SelectedIndex = -1

    '    'msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " WHERE CMP_ID='" & mcmpid & "' group by " & Trim(mfield) & " order by " & Trim(mfield)

    '    Dim cmd As New OleDb.OleDbCommand(msql, con)
    '    'Dim CMD As New OleDb.OleDbCommand("SELECT sectionname FROM section GROUP BY sectionname ORDER BY sectionname", con)
    '    If con.State = ConnectionState.Closed Then
    '        con.Open()
    '    End If
    '    ''Dim DR As SqlDataReader
    '    Dim DR As OleDb.OleDbDataReader
    '    DR = cmd.ExecuteReader
    '    If DR.HasRows = True Then
    '        mycombo.Items.Clear()
    '        While DR.Read
    '            mycombo.Items.Add(DR.Item(Trim(combofield)))
    '        End While
    '    End If
    '    DR.Close()
    '    cmd.Dispose()
    'End Sub
    Private Function SafeCellString(row As DataGridViewRow, colIndex As Integer) As String
        Dim val = row.Cells(colIndex).Value
        If val Is Nothing OrElse Convert.IsDBNull(val) Then
            Return String.Empty
        End If
        Return val.ToString().Trim()
    End Function

    Public Sub remdupdg(ByVal dgg As DataGridView)
        'For i As Integer = 0 To dgg.Rows.Count - 2
        '    For j As Integer = i + 1 To dgg.Rows.Count - 2
        '        Dim duplicate As Boolean = True

        '        ' Compare all cells in both rows
        '        For k As Integer = 0 To dgg.Columns.Count - 1
        '            If dgg.Rows(i).Cells(k).Value.ToString() <> dgg.Rows(j).Cells(k).Value.ToString() Then
        '                duplicate = False
        '                Exit For
        '            End If
        '        Next

        '        ' If duplicate, remove the row
        '        If duplicate Then
        '            dgg.Rows.RemoveAt(j)
        '            j -= 1
        '        End If
        '    Next
        'Next

        For i As Integer = 0 To dgg.Rows.Count - 2
            If dgg.Rows(i).IsNewRow Then Continue For

            Dim j As Integer = i + 1
            While j <= dgg.Rows.Count - 2
                If dgg.Rows(j).IsNewRow Then
                    j += 1
                    Continue While
                End If

                Dim isDuplicate As Boolean = True
                For k As Integer = 0 To dgg.Columns.Count - 1
                    If SafeCellString(dgg.Rows(i), k) <> SafeCellString(dgg.Rows(j), k) Then
                        isDuplicate = False
                        Exit For
                    End If
                Next

                If isDuplicate Then
                    dgg.Rows.RemoveAt(j)
                    ' do NOT increment j — next row shifted into index j
                Else
                    j += 1
                End If
            End While
        Next

    End Sub

    Public Function sDecode(ByVal Password As String) As String
        Dim j As Integer
        Dim TMP As Long
        tmpp = ""
        j = 1
        For i = 1 To Len(Password)
            TMP = Asc(Mid(Password, i, 1))
            TMP = TMP - j
            tmpp = Trim(tmpp) & Chr(TMP)
            'Decode = Decode & Chr(TMP)
        Next i
        sDecode = tmpp
        Return sDecode
    End Function

    Public Function sEncript(ByVal Password As String) As String
        Dim j As Integer
        'Dim tmpp As String

        Dim TMP As Long
        tmpp = ""
        j = 1
        For i = 1 To Len(Password)
            TMP = Asc(Mid(Password, i, 1))
            TMP = TMP + j
            tmpp = Trim(tmpp) + Chr(TMP)

            'Encript = Encript & Chr(TMP)
        Next i
        sEncript = tmpp
        Return sEncript
    End Function

    Public Function DecryptString(ByVal encrString As String) As String
        Dim b As Byte()
        Dim decrypted As String

        Try
            b = Convert.FromBase64String(encrString)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b)
        Catch fe As FormatException
            decrypted = ""
        End Try

        Return decrypted
    End Function

    Public Function EnryptString(ByVal strEncrypted As String) As String
        Dim b As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted)
        Dim encrypted As String = Convert.ToBase64String(b)
        Return encrypted
    End Function



    Public Function GenerateSmartBundlesold(dtItems As DataTable, showfranch As Boolean, mdocentry As Integer, Optional startboxsize As Integer = 0) As DataTable
        Dim mboxsize As Integer
        Dim mpackagecode As Integer
        Dim dtResult As New DataTable()
        dtResult.Columns.Add("docentry", GetType(Integer))
        dtResult.Columns.Add("Packagecode", GetType(Integer))
        dtResult.Columns.Add("PackNo", GetType(Integer))
        dtResult.Columns.Add("ItemCode", GetType(String))
        dtResult.Columns.Add("ItemName", GetType(String))
        dtResult.Columns.Add("Qty", GetType(Integer))
        dtResult.Columns.Add("BoxSize", GetType(Integer))
        dtResult.Columns.Add("Scanqty", GetType(Integer))

        'All items in FIFO style
        Dim itemList As New Queue(Of Tuple(Of String, String, Integer))()

        For Each r As DataRow In dtItems.Rows
            itemList.Enqueue(New Tuple(Of String, String, Integer)(r("ItemCode").ToString(), r("Itemname").ToString(),
                                                           CInt(r("Quantity"))))
        Next


        Dim totalInitialQty As Integer = dtItems.AsEnumerable().Sum(Function(r) CInt(r("Quantity")))

        Dim force7 As Boolean = False


        Dim packNo As Integer = 1

        If startboxsize > 0 Then

            mboxsize = startboxsize
            If startboxsize = 45 Then
                mpackagecode = 3
            ElseIf startboxsize = 40
                mpackagecode = 5
            ElseIf startboxsize = 30
                mpackagecode = 6
            ElseIf startboxsize = 20
                mpackagecode = 7
            ElseIf startboxsize = 10
                mpackagecode = 8
            End If
            mpackagecode = 3
        Else

            If showfranch = True Then
                'If groupname.ToLower = "shirt" Then
                mboxsize = 45
                mpackagecode = 3
                'ElseIf groupname.ToLower = "kurtha" Then
                'mboxsize = 40
                'End If
            Else
                mboxsize = 40
                mpackagecode = 5
            End If
        End If


        While itemList.Count > 0

            'Total remaining qty across all items
            Dim totalRemaining As Integer = itemList.Sum(Function(x) x.Item3)

            'Decide box size
            Dim boxSize As Integer

            If totalRemaining >= mboxsize Then
                boxSize = mboxsize
            ElseIf totalRemaining >= 22 Then
                boxSize = 30
                mpackagecode = 6
            ElseIf totalRemaining >= 11 Then
                boxSize = 20
                mpackagecode = 7
            ElseIf totalRemaining >= 8
                boxSize = 10
                mpackagecode = 8
            Else
                boxSize = 7
                mpackagecode = 9
            End If

            Dim room As Integer = boxSize

            'Start filling box
            While room > 0 AndAlso itemList.Count > 0

                Dim current = itemList.Dequeue()
                Dim itemCode = current.Item1
                Dim itemname = current.Item2
                Dim qty = current.Item3

                If qty <= room Then
                    'Whole qty fits
                    dtResult.Rows.Add(mdocentry, mpackagecode, packNo, itemCode, itemname, qty, boxSize, 0)
                    room -= qty
                    qty = 0

                Else
                    'Only part fits
                    dtResult.Rows.Add(mdocentry, mpackagecode, packNo, itemCode, itemname, room, boxSize, 0)
                    qty -= room
                    room = 0
                End If

                'If still some qty left, re-enqueue
                If qty > 0 Then
                    itemList.Enqueue(New Tuple(Of String, String, Integer)(itemCode, itemname, qty))
                End If

            End While

            packNo += 1

        End While

        Return dtResult

    End Function
    Public Function GenerateSmartBundles2(dtItems As DataTable, showfranch As Boolean, mdocentry As Integer, Optional startboxsize As Integer = 0) As DataTable
        Dim mboxsize As Integer
        Dim mpackagecode As Integer
        Dim dtResult As New DataTable()
        dtResult.Columns.Add("docentry", GetType(Integer))
        dtResult.Columns.Add("Packagecode", GetType(Integer))
        dtResult.Columns.Add("PackNo", GetType(Integer))
        dtResult.Columns.Add("ItemCode", GetType(String))
        dtResult.Columns.Add("ItemName", GetType(String))
        dtResult.Columns.Add("Qty", GetType(Integer))
        dtResult.Columns.Add("BoxSize", GetType(Integer))
        dtResult.Columns.Add("Scanqty", GetType(Integer))

        'All items in FIFO style
        Dim itemList As New Queue(Of Tuple(Of String, String, Integer))()

        For Each r As DataRow In dtItems.Rows
            itemList.Enqueue(New Tuple(Of String, String, Integer)(r("ItemCode").ToString(), r("Itemname").ToString(),
                                                           CInt(r("Quantity"))))
        Next
        Dim totalInitialQty As Integer = dtItems.AsEnumerable().Sum(Function(r) CInt(r("Quantity")))
        Dim packCounter As Integer = 1

        If totalInitialQty <= 7 Then
            While itemList.Count > 0
                Dim cur = itemList.Dequeue()
                dtResult.Rows.Add(mdocentry, 9, packCounter, cur.Item1, cur.Item2, cur.Item3, 7, 0)
            End While
            Return dtResult
        End If

        '------------------------------------------------------------
        ' Normal multi-box logic setup
        '------------------------------------------------------------
        If startboxsize > 0 Then
            mboxsize = startboxsize
            Select Case startboxsize
                Case 45 : mpackagecode = 3
                Case 40 : mpackagecode = 5
                Case 30 : mpackagecode = 6
                Case 20 : mpackagecode = 7
                Case 10 : mpackagecode = 8
                Case Else : mpackagecode = 3
            End Select
        Else
            If showfranch = True Then
                mboxsize = 45
                mpackagecode = 3
            Else
                mboxsize = 40
                mpackagecode = 5
            End If
        End If

        '------------------------------------------------------------
        ' Main packing loop
        '------------------------------------------------------------
        While itemList.Count > 0

            Dim totalRemaining As Integer = itemList.Sum(Function(x) x.Item3)

            '--------------------------------------------------------
            ' RULE 2:
            ' If FINAL remaining <=7 --> Merge ALL into ONE last box
            '--------------------------------------------------------
            If totalRemaining <= 7 AndAlso dtResult.Rows.Count > 0 Then

                Dim finalPackNo As Integer = packCounter
                While itemList.Count > 0
                    Dim cur = itemList.Dequeue()
                    dtResult.Rows.Add(
                        mdocentry,
                        9,                       'PackageCode
                        finalPackNo,             'PackNo
                        cur.Item1,
                        cur.Item2,
                        cur.Item3,
                        7,                       'Forced BoxSize = 7
                        0)
                End While

                Return dtResult
            End If

            '--------------------------------------------------------
            ' normal box selection
            '--------------------------------------------------------
            Dim boxSize As Integer

            If totalRemaining >= mboxsize Then
                boxSize = mboxsize
            ElseIf totalRemaining >= 22 Then
                boxSize = 30
                mpackagecode = 6
            ElseIf totalRemaining >= 11 Then
                boxSize = 20
                mpackagecode = 7
            ElseIf totalRemaining >= 8 Then
                boxSize = 10
                mpackagecode = 8
            Else
                boxSize = 7
                mpackagecode = 9
            End If

            Dim room As Integer = boxSize

            'Fill this box
            While room > 0 AndAlso itemList.Count > 0

                Dim current = itemList.Dequeue()
                Dim itemCode = current.Item1
                Dim itemName = current.Item2
                Dim qty = current.Item3

                If qty <= room Then
                    dtResult.Rows.Add(mdocentry, mpackagecode, packCounter, itemCode, itemName, qty, boxSize, 0)
                    room -= qty
                    qty = 0
                Else
                    dtResult.Rows.Add(mdocentry, mpackagecode, packCounter, itemCode, itemName, room, boxSize, 0)
                    qty -= room
                    room = 0
                End If

                If qty > 0 Then
                    itemList.Enqueue(New Tuple(Of String, String, Integer)(itemCode, itemName, qty))
                End If

            End While

            packCounter += 1

        End While

        Return dtResult



    End Function


    Public Function GenerateSmartBundles_latest(dtItems As DataTable, showfranch As Boolean, mdocentry As Integer, Optional startboxsize As Integer = 0) As DataTable
        Dim mboxsize As Integer
        Dim mpackagecode As Integer
        Dim dtResult As New DataTable()
        dtResult.Columns.Add("docentry", GetType(Integer))
        dtResult.Columns.Add("Packagecode", GetType(Integer))
        dtResult.Columns.Add("PackNo", GetType(Integer))
        dtResult.Columns.Add("ItemCode", GetType(String))
        dtResult.Columns.Add("ItemName", GetType(String))
        dtResult.Columns.Add("Qty", GetType(Integer))
        dtResult.Columns.Add("BoxSize", GetType(Integer))
        dtResult.Columns.Add("Scanqty", GetType(Integer))

        'All items in FIFO style
        Dim itemList As New Queue(Of Tuple(Of String, String, Integer))()

        For Each r As DataRow In dtItems.Rows
            itemList.Enqueue(New Tuple(Of String, String, Integer)(r("ItemCode").ToString(), r("Itemname").ToString(),
                                                           CInt(r("Quantity"))))
        Next

        Dim totalInitialQty As Integer = dtItems.AsEnumerable().Sum(Function(r) CInt(r("Quantity")))
        Dim force7 As Boolean = (totalInitialQty <= 7)
        Dim force10 As Boolean = (totalInitialQty <= 10)
        '------------------------------------------------------------
        ' If total <= 7, force everything into BoxSize = 7 / PackageCode = 9
        '------------------------------------------------------------
        If force7 Then
            Dim packNo As Integer = 1

            While itemList.Count > 0
                Dim current = itemList.Dequeue()
                dtResult.Rows.Add(mdocentry, 9, packNo, current.Item1, current.Item2, current.Item3, 7, 0)
            End While

            Return dtResult
        End If


        If force10 Then
            Dim packNo As Integer = 1

            While itemList.Count > 0
                Dim current = itemList.Dequeue()
                dtResult.Rows.Add(mdocentry, 8, packNo, current.Item1, current.Item2, current.Item3, 10, 0)
            End While

            Return dtResult
        End If

        '------------------------------------------------------------
        ' Normal multi-box logic
        '------------------------------------------------------------
        Dim packCounter As Integer = 1

        If startboxsize > 0 Then
            mboxsize = startboxsize
            Select Case startboxsize
                Case 45 : mpackagecode = 3
                Case 40 : mpackagecode = 5
                Case 30 : mpackagecode = 6
                Case 20 : mpackagecode = 7
                Case 10 : mpackagecode = 8
                Case Else : mpackagecode = 3
            End Select
        Else
            If showfranch = True Then
                mboxsize = 45
                mpackagecode = 3
            Else
                mboxsize = 40
                mpackagecode = 5
            End If
        End If

        '------------------------------------------------------------
        ' Main loop – create bundles until all items consumed
        '------------------------------------------------------------
        While itemList.Count > 0

            Dim totalRemaining As Integer = itemList.Sum(Function(x) x.Item3)
            Dim boxSize As Integer

            If showfranch = True Then

                If totalRemaining > 44 Then
                    boxSize = 45
                    'If (totalRemaining - 45) > 20 Then
                    mpackagecode = 3
                    'Else

                    'End If


                ElseIf totalRemaining > 30 Then
                    boxSize = 40
                    mpackagecode = 5

                ElseIf totalRemaining > 20 Then
                    boxSize = 30
                    mpackagecode = 6

                ElseIf totalRemaining > 10 Then
                    boxSize = 20
                    mpackagecode = 7

                Else
                    boxSize = 20
                    mpackagecode = 7

                End If
            Else


                If totalRemaining > 30 Then
                    boxSize = 40
                    mpackagecode = 5

                ElseIf totalRemaining > 20 Then
                    boxSize = 30
                    mpackagecode = 6

                ElseIf totalRemaining > 10 Then
                    boxSize = 20
                    mpackagecode = 7

                Else
                    boxSize = 10
                    mpackagecode = 8

                End If

            End If



            Dim room As Integer = boxSize

            '--------------------------------------------------------
            'Fill this box
            '--------------------------------------------------------
            While room > 0 AndAlso itemList.Count > 0

                Dim current = itemList.Dequeue()
                Dim itemCode = current.Item1
                Dim itemName = current.Item2
                Dim qty = current.Item3

                If qty <= room Then
                    'Fits completely
                    dtResult.Rows.Add(mdocentry, mpackagecode, packCounter, itemCode, itemName, qty, boxSize, 0)
                    room -= qty
                    qty = 0
                Else
                    'Partially fits
                    dtResult.Rows.Add(mdocentry, mpackagecode, packCounter, itemCode, itemName, room, boxSize, 0)
                    qty -= room
                    room = 0
                End If

                'Re-enqueue remaining qty
                If qty > 0 Then
                    itemList.Enqueue(New Tuple(Of String, String, Integer)(itemCode, itemName, qty))
                End If

            End While

            packCounter += 1

        End While

        Return dtResult


    End Function
    Public Function GenerateSmartBundlesold2(dtItems As DataTable, showfranch As Boolean, mdocentry As Integer, Optional startboxsize As Integer = 0) As DataTable

        Dim dtResult As New DataTable()
        dtResult.Columns.Add("docentry", GetType(Integer))
        dtResult.Columns.Add("Packagecode", GetType(Integer))
        dtResult.Columns.Add("PackNo", GetType(Integer))
        dtResult.Columns.Add("ItemCode", GetType(String))
        dtResult.Columns.Add("ItemName", GetType(String))
        dtResult.Columns.Add("Qty", GetType(Integer))
        dtResult.Columns.Add("BoxSize", GetType(Integer))
        dtResult.Columns.Add("Scanqty", GetType(Integer))

        'Load items into FIFO structure
        Dim itemList As New Queue(Of Tuple(Of String, String, Integer))()

        For Each r As DataRow In dtItems.Rows
            itemList.Enqueue(New Tuple(Of String, String, Integer)(
            r("ItemCode").ToString(),
            r("Itemname").ToString(),
            CInt(r("Quantity"))))
        Next

        Dim totalInitialQty As Integer = dtItems.AsEnumerable().Sum(Function(r) CInt(r("Quantity")))

        '------------------------------------------------------------
        ' SPECIAL CASES
        '------------------------------------------------------------
        'Total qty ≤ 7 → 1 box of size 7
        If totalInitialQty <= 7 Then

            Dim packNo As Integer = 1
            For Each r As DataRow In dtItems.Rows
                dtResult.Rows.Add(mdocentry, 9, packNo,
                              r("ItemCode").ToString(),
                              r("Itemname").ToString(),
                              CInt(r("Quantity")),
                              7,
                              0)
            Next

            Return dtResult
        End If

        'Total qty ≤ 10 → 1 box of size 10
        If totalInitialQty <= 10 Then

            Dim packNo As Integer = 1
            For Each r As DataRow In dtItems.Rows
                dtResult.Rows.Add(mdocentry, 8, packNo,
                              r("ItemCode").ToString(),
                              r("Itemname").ToString(),
                              CInt(r("Quantity")),
                              10,
                              0)
            Next

            Return dtResult
        End If

        '------------------------------------------------------------
        ' NORMAL MULTI-BOX PACKING
        '------------------------------------------------------------
        Dim packCounter As Integer = 1

        While itemList.Count > 0

            Dim totalRemaining As Integer = itemList.Sum(Function(x) x.Item3)
            Dim boxSize As Integer
            Dim pkgCode As Integer

            '--------------------------------------------------------
            ' BOX SIZE SELECTION RULES
            '--------------------------------------------------------
            If showfranch Then
                'Priority: 45 → 40 → 30 → 20  (No 10)
                If totalRemaining > 44 Then
                    boxSize = 45 : pkgCode = 3
                ElseIf totalRemaining > 39 Then
                    boxSize = 40 : pkgCode = 5
                ElseIf totalRemaining > 29 Then
                    boxSize = 30 : pkgCode = 6
                Else
                    boxSize = 20 : pkgCode = 7
                End If

            Else
                'Priority: 40 → 45 → 30 → 20  (No 10)
                If totalRemaining > 39 Then
                    boxSize = 40 : pkgCode = 5
                ElseIf totalRemaining > 34 Then
                    boxSize = 45 : pkgCode = 3
                ElseIf totalRemaining > 29 Then
                    boxSize = 30 : pkgCode = 6
                Else
                    boxSize = 20 : pkgCode = 7
                End If

            End If

            Dim room As Integer = boxSize

            '--------------------------------------------------------
            ' FILL THIS BOX
            '--------------------------------------------------------
            While room > 0 AndAlso itemList.Count > 0

                Dim current = itemList.Dequeue()
                Dim itemCode = current.Item1
                Dim itemName = current.Item2
                Dim qty = current.Item3

                If qty <= room Then
                    'Fits completely
                    dtResult.Rows.Add(mdocentry, pkgCode, packCounter, itemCode, itemName, qty, boxSize, 0)
                    room -= qty
                    qty = 0
                Else
                    'Partial
                    dtResult.Rows.Add(mdocentry, pkgCode, packCounter, itemCode, itemName, room, boxSize, 0)
                    qty -= room
                    room = 0
                End If

                'Requeue remaining qty
                If qty > 0 Then
                    itemList.Enqueue(New Tuple(Of String, String, Integer)(itemCode, itemName, qty))
                End If

            End While

            packCounter += 1

        End While

        Return dtResult
    End Function
    Public Function GenerateSmartBundles33(dtItems As DataTable,
                                     showfranch As Boolean,
                                     mdocentry As Integer) As DataTable

        Dim dtResult As New DataTable()
        dtResult.Columns.Add("docentry", GetType(Integer))
        dtResult.Columns.Add("Packagecode", GetType(Integer))
        dtResult.Columns.Add("PackNo", GetType(Integer))
        dtResult.Columns.Add("ItemCode", GetType(String))
        dtResult.Columns.Add("ItemName", GetType(String))
        dtResult.Columns.Add("Qty", GetType(Integer))
        dtResult.Columns.Add("BoxSize", GetType(Integer))
        dtResult.Columns.Add("Scanqty", GetType(Integer))

        'Queue FIFO
        Dim itemList As New Queue(Of Tuple(Of String, String, Integer))()
        For Each r As DataRow In dtItems.Rows
            itemList.Enqueue(New Tuple(Of String, String, Integer)(
            r("ItemCode").ToString(),
            r("Itemname").ToString(),
            CInt(r("Quantity"))))
        Next

        Dim totalQty As Integer = dtItems.AsEnumerable().Sum(Function(r) CInt(r("Quantity")))

        '-------------------------------------------
        ' SPECIAL SIMPLE CASES
        '-------------------------------------------
        If totalQty <= 7 Then
            Dim packNo = 1
            For Each r As DataRow In dtItems.Rows
                dtResult.Rows.Add(mdocentry, 9, packNo,
                              r("ItemCode"),
                              r("Itemname"),
                              CInt(r("Quantity")),
                              7, 0)
            Next
            Return dtResult
        End If

        If totalQty <= 10 Then
            Dim packNo = 1
            For Each r As DataRow In dtItems.Rows
                dtResult.Rows.Add(mdocentry, 8, packNo,
                              r("ItemCode"),
                              r("Itemname"),
                              CInt(r("Quantity")),
                              10, 0)
            Next
            Return dtResult
        End If

        '----------------------------------------------------------
        ' MULTI BOX – OPTIMIZED SPLITTING
        '----------------------------------------------------------
        'Allowed box sizes
        Dim boxSizes As Integer() = {45, 40, 30, 20}

        Dim packCounter As Integer = 1
        Dim firstBoxUsed As Boolean = False

        While totalQty > 0

            Dim selectedBox As Integer
            Dim pkgCode As Integer

            '---------------------------------------------------
            ' FIRST BOX RULE
            '---------------------------------------------------
            If Not firstBoxUsed Then
                If showfranch Then
                    selectedBox = 45
                    pkgCode = 3
                Else
                    selectedBox = 40
                    pkgCode = 5
                End If
                firstBoxUsed = True

            Else
                '---------------------------------------------------
                ' OPTIMAL BOX SELECTION FOR REMAINING
                '---------------------------------------------------
                selectedBox = FindBestBoxSize(totalQty)

                Select Case selectedBox
                    Case 45 : pkgCode = 3
                    Case 40 : pkgCode = 5
                    Case 30 : pkgCode = 6
                    Case 20 : pkgCode = 7
                End Select

            End If

            'This box takes items
            Dim remainingCapacity = selectedBox

            While remainingCapacity > 0 AndAlso itemList.Count > 0

                Dim current = itemList.Dequeue()
                Dim itemCode = current.Item1
                Dim itemName = current.Item2
                Dim qty = current.Item3

                If qty <= remainingCapacity Then
                    dtResult.Rows.Add(mdocentry, pkgCode, packCounter,
                              itemCode, itemName, qty, selectedBox, 0)
                    remainingCapacity -= qty
                    qty = 0

                Else
                    dtResult.Rows.Add(mdocentry, pkgCode, packCounter,
                              itemCode, itemName, remainingCapacity, selectedBox, 0)
                    qty -= remainingCapacity
                    remainingCapacity = 0
                End If

                If qty > 0 Then
                    itemList.Enqueue(New Tuple(Of String, String, Integer)(itemCode, itemName, qty))
                End If

            End While

            totalQty = itemList.Sum(Function(x) x.Item3)
            packCounter += 1

        End While
        AdjustLastBundle(dtResult)
        Return dtResult

    End Function
    Private Function FindBestBoxSize(remQty As Integer) As Integer

        'Allowed available box sizes
        Dim sizes As Integer() = {45, 40, 30, 20}

        'If perfect fit
        For Each s In sizes
            If remQty = s Then Return s
        Next

        'If > 45 choose 45
        If remQty > 45 Then Return 45

        'If best double fit exists
        If remQty <= 60 AndAlso remQty >= 55 Then Return 30
        If remQty <= 60 AndAlso remQty >= 40 Then Return 40
        If remQty <= 50 AndAlso remQty >= 30 Then Return 30
        If remQty <= 40 AndAlso remQty >= 20 Then Return 20

        'Small tail fallback
        If remQty <= 10 Then Return 10
        If remQty <= 7 Then Return 7

        'Default
        Return 20

    End Function
    Private Sub AdjustLastBundle(dtResult As DataTable)

        If dtResult.Rows.Count = 0 Then Exit Sub

        'Total remaining not needed, we only check last row
        Dim lastPack As DataRow = dtResult.Rows(dtResult.Rows.Count - 1)
        Dim lastBoxSize As Integer = CInt(lastPack("BoxSize"))

        'SUM remaining in the last pack
        Dim lastPackNo = CInt(lastPack("PackNo"))
        Dim remainingQty As Integer =
        dtResult.AsEnumerable().
        Where(Function(r) CInt(r("PackNo")) = lastPackNo).
        Sum(Function(r) CInt(r("Qty")))

        'If remaining is fine (>=7), nothing to fix
        If remainingQty >= 7 Then Exit Sub

        'Otherwise increase the previous bundle
        'Find previous bundle
        Dim prevPackNo As Integer = lastPackNo - 1
        If prevPackNo < 1 Then Exit Sub

        'Find previous bundle box size
        Dim prevRows = dtResult.AsEnumerable().
        Where(Function(r) CInt(r("PackNo")) = prevPackNo).ToList()

        If prevRows.Count = 0 Then Exit Sub

        Dim oldBoxSize As Integer = CInt(prevRows(0)("BoxSize"))
        Dim newBoxSize As Integer = oldBoxSize

        'Upgrade the previous box logically
        If oldBoxSize = 20 Then newBoxSize = 30
        If oldBoxSize = 30 Then newBoxSize = 40
        If oldBoxSize = 40 Then newBoxSize = 45

        'Apply new boxsize to all rows in previous pack
        For Each r In prevRows
            r("BoxSize") = newBoxSize
        Next

        'Now merge the last tiny qty into that box
        For Each r As DataRow In dtResult.AsEnumerable().
            Where(Function(x) CInt(x("PackNo")) = lastPackNo).ToList()

            'Change its packNo to previous box
            r("PackNo") = prevPackNo
            r("BoxSize") = newBoxSize
        Next

        'Remove empty rows and reorder pack numbers if needed

    End Sub

    Public Function GenerateSmartBundles_actual(dtItems As DataTable,
                                     showfranch As Boolean,
                                     mdocentry As Integer,
                                     Optional startboxsize As Integer = 0) As DataTable

        Dim dtResult As New DataTable()
        dtResult.Columns.Add("docentry", GetType(Integer))
        dtResult.Columns.Add("Packagecode", GetType(Integer))
        dtResult.Columns.Add("PackNo", GetType(Integer))
        dtResult.Columns.Add("ItemCode", GetType(String))
        dtResult.Columns.Add("ItemName", GetType(String))
        dtResult.Columns.Add("Qty", GetType(Integer))
        dtResult.Columns.Add("BoxSize", GetType(Integer))
        dtResult.Columns.Add("ScanQty", GetType(Integer))

        '-----------------------------------------------
        ' Build FIFO list of items
        '-----------------------------------------------
        Dim items As New Queue(Of Tuple(Of String, String, Integer))

        For Each r As DataRow In dtItems.Rows
            items.Enqueue(New Tuple(Of String, String, Integer)(
            r("ItemCode").ToString(),
            r("ItemName").ToString(),
            CInt(r("Quantity"))
        ))
        Next

        Dim totalQty As Integer = dtItems.AsEnumerable().Sum(Function(x) CInt(x("Quantity")))

        '-----------------------------------------------
        ' If total <=7 → 7 box always
        '-----------------------------------------------
        If totalQty <= 7 Then
            Dim pno = 1
            While items.Count > 0
                Dim c = items.Dequeue()
                dtResult.Rows.Add(mdocentry, 9, pno, c.Item1, c.Item2, c.Item3, 7, 0)
            End While
            Return dtResult
        End If

        '-----------------------------------------------
        ' If total >7 and <=10 → 10 box always
        '-----------------------------------------------
        If totalQty <= 10 Then
            Dim pno = 1
            While items.Count > 0
                Dim c = items.Dequeue()
                dtResult.Rows.Add(mdocentry, 8, pno, c.Item1, c.Item2, c.Item3, 10, 0)
            End While
            Return dtResult
        End If

        '===========================================================
        '  LARGE ALLOCATION (REAL PACKING BEGINS)
        '===========================================================
        Dim packNo As Integer = 1

        'Autobox function
        Dim CalcBoxSize =
        Function(remaining As Integer) As Tuple(Of Integer, Integer)

            'boxsize , packagecode
            If showfranch Then

                If remaining > 44 Then Return Tuple.Create(45, 3)
                If remaining > 30 Then Return Tuple.Create(40, 5)
                If remaining > 20 Then Return Tuple.Create(30, 6)
                If remaining > 10 Then Return Tuple.Create(20, 7)
                Return Tuple.Create(20, 7)

            Else

                If remaining > 30 Then Return Tuple.Create(40, 5)
                If remaining > 20 Then Return Tuple.Create(30, 6)
                If remaining > 10 Then Return Tuple.Create(20, 7)
                Return Tuple.Create(10, 8)

            End If

        End Function


        '===========================================================
        ' MAIN WHILE LOOP
        '===========================================================
        While items.Count > 0

            Dim remainingTotal As Integer = items.Sum(Function(x) x.Item3)

            Dim chosen = CalcBoxSize(remainingTotal)
            Dim boxSize = chosen.Item1
            Dim pkgCode = chosen.Item2

            Dim spaceLeft = boxSize

            '=======================================================
            'Fill box until spaceLeft = 0
            '=======================================================
            While spaceLeft > 0 AndAlso items.Count > 0

                Dim cur = items.Dequeue()
                Dim icode = cur.Item1
                Dim iname = cur.Item2
                Dim qty = cur.Item3

                If qty <= spaceLeft Then

                    'Full fit
                    dtResult.Rows.Add(mdocentry, pkgCode, packNo, icode, iname, qty, boxSize, 0)
                    spaceLeft -= qty
                    qty = 0

                Else

                    'Partial fit
                    dtResult.Rows.Add(mdocentry, pkgCode, packNo, icode, iname, spaceLeft, boxSize, 0)
                    qty -= spaceLeft
                    spaceLeft = 0

                End If

                'If qty still remains, push FIRST back into queue → next box continues same item
                If qty > 0 Then
                    Dim newQ As New Queue(Of Tuple(Of String, String, Integer))
                    newQ.Enqueue(New Tuple(Of String, String, Integer)(icode, iname, qty))

                    While items.Count > 0
                        newQ.Enqueue(items.Dequeue())
                    End While

                    items = newQ
                End If

            End While

            'Opened box completed → next
            packNo += 1

        End While

        '===========================================================
        ' SMALL REMAINDER REBALANCE (Optional)
        ' If last box has too tiny quantity like 1 or 2, shift
        '===========================================================
        Dim lastRows = dtResult.AsEnumerable().
        Where(Function(r) CInt(r("PackNo")) = packNo - 1).
        ToList()

        Dim lastTotal = lastRows.Sum(Function(r) CInt(r("Qty")))

        If lastTotal <= 2 And packNo > 2 Then

            'Take last rows out
            For Each rr In lastRows
                dtResult.Rows.Remove(rr)
            Next

            'Move them into previous pack
            Dim prevPackNo = packNo - 2

            For Each rr In lastRows
                dtResult.Rows.Add(
                mdocentry,
                rr("Packagecode"),
                prevPackNo,
                rr("ItemCode"),
                rr("ItemName"),
                rr("Qty"),
                rr("BoxSize"),
                0
            )
            Next

        End If

        Return dtResult

    End Function

    Public Function GenerateSmartBundles(dtItems As DataTable,
                                     dtPackMaster As DataTable,
                                     mdocentry As Integer) As DataTable

        Dim dtResult As New DataTable()
        dtResult.Columns.Add("docentry", GetType(Integer))
        dtResult.Columns.Add("Packagecode", GetType(Integer))   'PackNo from master
        dtResult.Columns.Add("PackNo", GetType(Integer))        'Running box number
        dtResult.Columns.Add("ItemCode", GetType(String))
        dtResult.Columns.Add("ItemName", GetType(String))
        dtResult.Columns.Add("Qty", GetType(Integer))
        dtResult.Columns.Add("BoxSize", GetType(Integer))       'BoxQty from master
        dtResult.Columns.Add("ScanQty", GetType(Integer))

        '----------------------------------------------------
        ' Build Queue of items (FIFO)
        '----------------------------------------------------
        Dim items As New Queue(Of Tuple(Of String, String, Integer))

        For Each r As DataRow In dtItems.Rows
            items.Enqueue(New Tuple(Of String, String, Integer)(
                      r("ItemCode").ToString(),
                      r("ItemName").ToString(),
                      CInt(r("Quantity"))
                  ))
        Next

        '----------------------------------------------------
        ' Prepare pack master index
        '----------------------------------------------------
        Dim packIndex As Integer = 0
        Dim runningPackNo As Integer = 1

        '----------------------------------------------------
        ' Function to pick next box size from Pack Master
        '----------------------------------------------------
        Dim CalcBoxSize =
    Function() As Tuple(Of Integer, Integer)

        If packIndex >= dtPackMaster.Rows.Count Then
            'If master rows are finished, use last definition repeatedly
            Dim last = dtPackMaster.Rows(dtPackMaster.Rows.Count - 1)
            Return Tuple.Create(CInt(last("BoxQty")), CInt(last("PackNo")))
        End If

        Dim r = dtPackMaster.Rows(packIndex)
        packIndex += 1

        Return Tuple.Create(CInt(r("BoxQty")), CInt(r("PackNo")))
    End Function


        '----------------------------------------------------
        ' MAIN PACKING LOOP
        '----------------------------------------------------
        While items.Count > 0

            'Pick next box definition from pack master
            Dim chosen = CalcBoxSize()
            Dim boxSize = chosen.Item1      'BoxQty
            Dim pkgCode = chosen.Item2      'PackNo from master
            Dim spaceLeft = boxSize

            '------------------------------------------------
            ' Fill until box is full
            '------------------------------------------------
            While spaceLeft > 0 AndAlso items.Count > 0

                Dim cur = items.Dequeue()
                Dim itmCode = cur.Item1
                Dim itmName = cur.Item2
                Dim qty = cur.Item3

                If qty <= spaceLeft Then
                    'Fits completely
                    dtResult.Rows.Add(mdocentry, pkgCode, runningPackNo,
                                  itmCode, itmName, qty, boxSize, 0)

                    spaceLeft -= qty
                    qty = 0

                Else
                    'Partial fit
                    dtResult.Rows.Add(mdocentry, pkgCode, runningPackNo,
                                  itmCode, itmName, spaceLeft, boxSize, 0)

                    qty -= spaceLeft
                    spaceLeft = 0
                End If

                'If quantity remains, push current item back to front of queue
                If qty > 0 Then
                    Dim newQ As New Queue(Of Tuple(Of String, String, Integer))
                    newQ.Enqueue(New Tuple(Of String, String, Integer)(itmCode, itmName, qty))

                    While items.Count > 0
                        newQ.Enqueue(items.Dequeue())
                    End While

                    items = newQ
                End If

            End While

            runningPackNo += 1

        End While

        Return dtResult

    End Function
    Public Function DataGridViewToDataTable(dgv As DataGridView) As DataTable

        Dim dt As New DataTable()

        '-------------------------------------------
        ' Create columns
        '-------------------------------------------
        For Each col As DataGridViewColumn In dgv.Columns
            dt.Columns.Add(col.Name, GetType(String))
        Next

        '-------------------------------------------
        ' Add rows
        '-------------------------------------------
        For Each row As DataGridViewRow In dgv.Rows
            If Not row.IsNewRow Then

                Dim dr As DataRow = dt.NewRow()

                For Each col As DataGridViewColumn In dgv.Columns
                    dr(col.Name) = If(row.Cells(col.Index).Value, "").ToString()
                Next

                dt.Rows.Add(dr)

            End If
        Next

        Return dt

    End Function

    Public Function CreatePackageFromMaster(dtItems As DataTable,
                                        dtPackMaster As DataTable,
                                        docentry As Integer) As DataTable

        Dim dtResult As New DataTable()
        dtResult.Columns.Add("docentry", GetType(Integer))
        dtResult.Columns.Add("Packagecode", GetType(String))   'PackNo from master
        dtResult.Columns.Add("PackNo", GetType(Integer))        'Running box count
        dtResult.Columns.Add("ItemCode", GetType(String))
        dtResult.Columns.Add("ItemName", GetType(String))
        dtResult.Columns.Add("Qty", GetType(Integer))
        dtResult.Columns.Add("BoxSize", GetType(Integer))
        dtResult.Columns.Add("ScanQty", GetType(Integer))

        '=========================================================
        ' Convert dtItems to FIFO queue
        '=========================================================
        Dim items As New Queue(Of Tuple(Of String, String, Integer))

        For Each r As DataRow In dtItems.Rows
            items.Enqueue(New Tuple(Of String, String, Integer)(
                      r("ItemCode").ToString(),
                      r("ItemName").ToString(),
                      CInt(r("Quantity"))
                  ))
        Next

        Dim packIndex As Integer = 0
        Dim runningPackNo As Integer = 1

        '=========================================================
        ' Returns next box from master
        '=========================================================
        Dim NextBox =
    Function() As Tuple(Of Integer, Integer, String)
        If packIndex >= dtPackMaster.Rows.Count Then
            'When no master rows remain, repeat last
            Dim last = dtPackMaster.Rows(dtPackMaster.Rows.Count - 1)
            Return Tuple.Create(
                CInt(last("Qty")),
                CInt(last("BoxQty")),
                last("BoxSize").ToString
            )
        End If

        Dim r = dtPackMaster.Rows(packIndex)
        packIndex += 1
        Return Tuple.Create(
            CInt(r("Qty")),      'capacity
            CInt(r("BoxQty")),    'print box size
            r("BoxSize").ToString
        )
    End Function


        '=========================================================
        ' MAIN PACKING LOOP
        '=========================================================
        While items.Count > 0

            Dim box = NextBox()
            Dim boxCapacity = box.Item1    'from PackMaster Qty
            Dim boxPrintSize = box.Item2   'from PackMaster BoxQty
            Dim spaceLeft = boxCapacity
            Dim pakboxsize = box.Item3

            '-----------------------------------------------------
            ' Fill the box
            '-----------------------------------------------------
            While spaceLeft > 0 AndAlso items.Count > 0

                Dim cur = items.Dequeue()
                Dim itemCode = cur.Item1
                Dim itemName = cur.Item2
                Dim qty = cur.Item3

                If qty <= spaceLeft Then

                    ''Complete fit
                    'dtResult.Rows.Add(docentry, packIndex, runningPackNo,
                    '              itemCode, itemName, qty, boxPrintSize)

                    'Complete fit
                    dtResult.Rows.Add(docentry, pakboxsize, runningPackNo,
                                  itemCode, itemName, qty, boxPrintSize, 0)

                    spaceLeft -= qty
                    qty = 0

                Else

                    'Partial fit
                    dtResult.Rows.Add(docentry, pakboxsize, runningPackNo,
                                  itemCode, itemName, spaceLeft, boxPrintSize, 0)
                    qty -= spaceLeft
                    spaceLeft = 0

                End If

                'If quantity remains, put back front of queue
                If qty > 0 Then
                    Dim newQ As New Queue(Of Tuple(Of String, String, Integer))
                    newQ.Enqueue(New Tuple(Of String, String, Integer)(itemCode, itemName, qty))

                    While items.Count > 0
                        newQ.Enqueue(items.Dequeue())
                    End While

                    items = newQ
                End If

            End While

            runningPackNo += 1

        End While

        Return dtResult

    End Function



    Public Sub AddQtyToItem(dgpp As DataGridView, dgph As DataGridView, ByVal itemCode As String, ByVal qtyToAdd As Integer)
        Dim currentQty As Integer = 0
        Dim packNo As Integer
        For i As Integer = 0 To dgpp.Rows.Count - 1

            'Check row itemCode
            If dgpp.Rows(i).Cells("ItemCode").Value.ToString().Trim() = itemCode.Trim() And Convert.ToInt32(dgpp.Rows(i).Cells("ScanQty").Value) <> Convert.ToInt32(dgpp.Rows(i).Cells("BoxSize").Value) Then

                'Dim currentQty As Integer = Convert.ToInt32(dgpp.Rows(i).Cells("Qty").Value)
                currentQty = Convert.ToInt32(dgpp.Rows(i).Cells("ScanQty").Value)
                Dim boxSize As Integer = Convert.ToInt32(dgpp.Rows(i).Cells("BoxSize").Value)
                packNo = Convert.ToInt32(dgpp.Rows(i).Cells("PackNo").Value)
                'How much space is left?
                Dim spaceLeft As Integer = boxSize - currentQty

                If spaceLeft > 0 Then

                    If qtyToAdd <= spaceLeft Then
                        'Everything fits in this row
                        dgpp.Rows(i).Cells("ScanQty").Value = currentQty + qtyToAdd
                        'qtyToAdd = 0
                        currentQty = Convert.ToInt32(dgpp.Rows(i).Cells("ScanQty").Value)
                        'packNo = Convert.ToInt32(dgpp.Rows(i).Cells("PackNo").Value)
                        UpdatePackScanQty(dgph, packNo, qtyToAdd)
                        qtyToAdd = 0
                        Exit Sub

                    Else
                        'Fill this box and reduce qty
                        dgpp.Rows(i).Cells("ScanQty").Value = boxSize
                        qtyToAdd -= spaceLeft
                    End If


                    'Dim packNo As Integer = Convert.ToInt32(dgpp.Rows(i).Cells("PackNo").Value)
                    'UpdatePackScanQty(dgph, packNo, qtyToAdd)

                    'Exit if everything is consumed
                    'If qtyToAdd = 0 Then Exit Sub


                End If

                'If this row is full, loop continues to next matching row
            End If
        Next

        'If still qty left → create a new row or show message
        If qtyToAdd > 0 Then
            'MessageBox.Show("No more boxes available for item: " & itemCode &
            '                vbCrLf & "Remaining Qty: " & qtyToAdd)
        End If

    End Sub




    Public Sub UpdatePackScanQty(dgph As DataGridView, packNo As Integer, qtyAdded As Integer)

        For Each r As DataGridViewRow In dgph.Rows
            If Not r.IsNewRow Then

                If Convert.ToInt32(r.Cells("PackNo").Value) = packNo Then

                    Dim oldTot As Integer = 0
                    If Not IsDBNull(r.Cells("TotScanQty").Value) Then
                        oldTot = CInt(r.Cells("TotScanQty").Value)
                    End If

                    r.Cells("TotScanQty").Value = oldTot + qtyAdded

                    Exit For
                End If

            End If
        Next

    End Sub

    'Public Function CenterX(text As String, fontSize As Integer, labelWidthMm As Double) As Integer
    '    Dim dotsPerMm As Double = 8 ' 203 dpi printer
    '    Dim labelWidthDots As Integer = CInt(labelWidthMm * dotsPerMm)
    '    Dim textWidth As Double = fontSize * text.Length * 0.6
    '    Return CInt((labelWidthDots - textWidth) / 2)
    'End Function


    Public Function CenterXold(text As String, fontScale As Integer, labelWidthMm As Double) As Integer
        Dim dotsPerMm As Double = 8 ' 203 dpi = 8 dots per mm
        Dim labelWidthDots As Integer = CInt(labelWidthMm * dotsPerMm)

        Dim baseCharWidth As Integer = 8 ' normal width in dots for ROMAN.TTF
        Dim textWidthDots As Integer = text.Length * baseCharWidth * fontScale

        Dim x As Integer = CInt((labelWidthDots - textWidthDots) / 2)
        If x < 0 Then x = 0

        Return x
    End Function

    Public Function CenterX(text As String, fontSize As Integer, labelWidthMM As Double, rotate As Integer) As Integer
        Dim dpi As Integer = 203
        Dim dotsPerMM As Double = dpi / 25.4   ' = 8 dots/mm

        Dim labelWidthDots As Integer = CInt(labelWidthMM * dotsPerMM)

        ' TSC TrueType font approximate width factor
        'Dim widthFactor As Double = 0.55       ' Good for ROMAN.TTF
        Dim widthFactor As Double = 1.28

        ' Estimated text width in dots
        Dim textWidthDots As Double = fontSize * widthFactor * text.Length

        ' Centering formula
        'Dim x As Integer = CInt((labelWidthDots - textWidthDots) / 2) + rotate
        'Dim x As Integer = CInt((labelWidthDots + textWidthDots) / 2) + 88

        Dim x As Integer

        If rotate = 180 Then
            ' RIGHT → LEFT text direction
            x = CInt((labelWidthDots + textWidthDots) / 2)
        Else
            ' LEFT → RIGHT text direction
            x = CInt((labelWidthDots - textWidthDots) / 2)
        End If


        Return x
    End Function
    Public Function CenterX_TSC(text As String, fontSize As Integer, labelWidthMM As Double) As Integer
        Dim dpi As Integer = 203
        Dim dotsPerMM As Double = dpi / 25.4   ' ≈ 8 dots/mm
        Dim labelWidthDots As Integer = CInt(labelWidthMM * dotsPerMM)

        ' TSC real width calculation:-
        ' 1.28 is the correct ROMAN.TTF width multiplier for 180° text
        Dim widthFactor As Double = 1.28

        Dim textWidthDots As Double = fontSize * widthFactor * text.Length

        ' centered position
        Dim x As Integer = CInt((labelWidthDots - textWidthDots) / 2)

        Return x
    End Function
    Public Function CenterXrotat(text As String, fontSize As Integer, labelWidthDots As Integer, rotate180 As Boolean) As Integer
        Dim textWidth As Double = fontSize * text.Length * 0.6
        Dim centerX As Integer = CInt((labelWidthDots - textWidth) / 2)

        If rotate180 Then
            centerX = labelWidthDots - centerX
            centerX = (centerX * 8) / 2

        End If

        Return centerX
    End Function

    Public Sub loadcombo(ByVal mtable As String, ByVal combofield As String, ByVal mycombo As ComboBox, ByVal grpfield As String)
        Dim msql As String
        'msql = mqry
        'msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " WHERE CMP_ID='" & mcmpid & "' group by " & Trim(mfield) & " order by " & Trim(mfield)

        If Len(Trim(grpfield)) > 0 Then
            msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " group by " & Trim(grpfield) & " order by " & Trim(grpfield)
        Else
            msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " group by " & Trim(grpfield) & " order by " & Trim(grpfield)
        End If


        ''Dim cmd As New SqlCommand(msql, con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If
        'Dim cmd As New SqlCommand(msql, con)


        'Dim DR As SqlDataReader = Nothing
        'dr = cmd.ExecuteReader
        'If DR.HasRows = True Then
        '    mycombo.Items.Clear()
        '    While DR.Read
        '        mycombo.Items.Add(DR.Item(Trim(combofield)))
        '    End While
        'End If
        'DR.Close()
        'cmd.Dispose()
        '**2
        'If con.State = ConnectionState.Closed Then con.Open()

        'mycombo.Items.Clear()

        'Using cmd As New SqlCommand(msql, con)
        '    Using DR As SqlDataReader = cmd.ExecuteReader()
        '        While DR.Read()
        '            mycombo.Items.Add(DR(combofield).ToString())
        '        End While
        '    End Using
        'End Using
        Task.Run(Sub()
                     Dim dt As DataTable = Nothing
                     'Dim qry As String = "select cardname,cardcode from partymaster order by cardname"
                     dt = getDataTable(msql)
                     If mycombo.InvokeRequired Then
                         mycombo.Invoke(Sub()
                                            mycombo.DataSource = Nothing
                                            mycombo.Items.Clear()
                                            mycombo.DataSource = dt
                                            mycombo.DisplayMember = combofield  ' what user sees
                                            mycombo.ValueMember = combofield       ' 💾 actual value
                                            mycombo.SelectedIndex = -1

                                        End Sub)
                     End If

                 End Sub)

    End Sub

    Public Sub loadcomboqry(ByVal msql As String, ByVal combofield As String, ByVal mycombo As ComboBox, ByVal valuefield As String)
        'Dim msql As String
        ''msql = mqry
        ''msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " WHERE CMP_ID='" & mcmpid & "' group by " & Trim(mfield) & " order by " & Trim(mfield)

        'If Len(Trim(grpfield)) > 0 Then
        '    msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " group by " & Trim(grpfield) & " order by " & Trim(grpfield)
        'Else
        '    msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " group by " & Trim(grpfield) & " order by " & Trim(grpfield)
        'End If
        Task.Run(Sub()

                     Dim dt As DataTable = Nothing
                     'Dim qry As String = "select cardname,cardcode from partymaster order by cardname"
                     dt = getDataTable(msql)
                     If mycombo.InvokeRequired Then
                         mycombo.Invoke(Sub()
                                            mycombo.DataSource = Nothing
                                            mycombo.Items.Clear()
                                            mycombo.DataSource = dt
                                            mycombo.DisplayMember = combofield  ' what user sees
                                            mycombo.ValueMember = valuefield       ' 💾 actual value
                                            mycombo.SelectedIndex = -1
                                        End Sub)
                     End If

                 End Sub)


                 End Sub


    Public Sub deletedata(ByVal mtable As String, ByVal mwherecond As String)
        Dim msql As String
        msql = "select from " & Trim(mtable) & " where " & mwherecond
        Dim CMD As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        CMD.ExecuteNonQuery()
        CMD.Dispose()
        MsgBox("Deleted!")

    End Sub

    Public Function loaditcoderev(ByVal mtable As String, ByVal mfield As String, ByVal mfindfield As String, ByVal findstr As Int32) As String
        Dim msql As String
        msql = "select " & Trim(mfield) & " as mkid from " & Trim(mtable) & " where " & Trim(mfindfield) & "=" & findstr
        Dim cmd As New OleDb.OleDbCommand(msql, con2)
        'Dim CMD As New OleDb.OleDbCommand("SELECT section_id FROM section where sectionname='" & txtsectionname.Text & "' and  cmp_id='" & mcmpid & "'", con)
        If con2.State = ConnectionState.Closed Then
            con2.Open()
        End If
        Dim itcod As String = IIf(IsDBNull(cmd.ExecuteScalar) = False, cmd.ExecuteScalar, 0)
        loaditcoderev = itcod
        cmd.Dispose()
        con2.Close()
        Return loaditcoderev

    End Function

    Public Sub editflx(ByVal eflx As AxMSFlexGrid, ByVal KeyAscii As Integer, ByVal focus As Control)
        Select Case KeyAscii
            Case 30 To 136 Or 8
                eflx.Text = eflx.Text & Chr(KeyAscii)
            Case 8 'IF KEY IS BACKSPACE THEN
                If eflx.Text <> "" Then eflx.Text = Left$(eflx.Text, (Len(eflx.Text) - 1))
            Case 13
                If eflx.Col < eflx.Cols - 1 Then
                    eflx.Col = eflx.Col + 1
                Else
                    '      If MsgBox("Add Record !", vbYesNo) = vbYes Then
                    If eflx.Row = eflx.Rows - 1 Then
                        eflx.Rows = eflx.Rows + 1
                        eflx.Row = eflx.Row + 1
                        eflx.Col = 0
                        Dim A As Boolean
                        A = eflx.CellTop
                        'cflx.Row = 1
                        'cflx.Col = 0
                        'cflx.SetFocus
                    Else
                        'focus.SetFocus
                    End If

                End If

        End Select


    End Sub

    Public Function findval(ByVal ctrl As AxMSFlexGrid, ByVal mfindstr As String, ByVal findcol As Int32) As Boolean
        findval = False
        For I = 1 To ctrl.Rows - 1
            If Trim(ctrl.get_TextMatrix(I, findcol)) = Trim(mfindstr) Then
                findval = True
                Exit Function
            End If
        Next I
    End Function
    Public Function loaditcode(ByVal mtable As String, ByVal mfield As String, ByVal mfindfield As String, ByVal findstr As String) As Int32
        Dim msql As String
        'msql = "select " & Trim(mfield) & " as mkid from " & Trim(mtable) & " where " & Trim(mfindfield) & "='" & Trim(findstr) & "' and cmp_id='" & mcmpid & "'"
        msql = "select " & Trim(mfield) & " as mkid from " & Trim(mtable) & " where " & Trim(mfindfield) & "='" & Trim(findstr) & "'"
        Dim cmd As New OleDb.OleDbCommand(msql, con2)
        'Dim CMD As New OleDb.OleDbCommand("SELECT section_id FROM section where sectionname='" & txtsectionname.Text & "' and  cmp_id='" & mcmpid & "'", con)
        If con2.State = ConnectionState.Closed Then
            con2.Open()
        End If
        Dim itcod As Int32 = IIf(IsDBNull(cmd.ExecuteScalar) = False, cmd.ExecuteScalar, 0)

        loaditcode = itcod
        cmd.Dispose()
        con2.Close()
        Return loaditcode

    End Function

    Public Function getyrchr(ByVal myr As Integer) As String
        Dim mr, mfr, masc, k, j As Integer
        Dim mcr, mkyr As String
        mr = 2017
        masc = 64
        k = 1
        'If myr = 2018 Then
        For j = 1 To 52
            mfr = mr + j
            mcr = Chr(masc + k)
            If (masc + k) = 90 Then
                k = k + 6
            End If
            If mfr = myr Then
                mkyr = mcr
                Exit For

            End If
            k = k + 1
        Next j

        Return mkyr

    End Function

    Public Sub searchflx(ByVal CTRL As AxMSFlexGrid, ByVal N As Integer, ByVal MCOL As Integer) 'n as keyascii
        Dim i, j, K As Integer
        Static TEST As String
        K = 0
        If N >= 32 And N <= 126 Then
            TEST = TEST & UCase(Chr(N))
            'With CTRL
            For i = 1 To CTRL.Rows - 1
                j = InStr(UCase(CTRL.get_TextMatrix(i, MCOL)), TEST)
                'j = InStr(UCase(CTRL.TextMatrix(i, MCOL)), TEST)  'Move Active Cell on Your desired Position
                If (j = 1) Then
                    CTRL.Row = i
                    CTRL.Col = MCOL
                    CTRL.RowSel = i
                    CTRL.ColSel = CTRL.Cols - 1
                    CTRL.TopRow = i
                    K = i
                    Exit Sub
                End If
            Next
            K = 0
            TEST = ""
            TEST = TEST & UCase(Chr(N))
            For i = 1 To CTRL.Rows - 1
                j = InStr(UCase(CTRL.get_TextMatrix(i, MCOL)), TEST)
                'j = InStr(UCase(CTRL.TextMatrix(i, MCOL)), TEST)
                If (j = 1) Then
                    CTRL.Row = i
                    CTRL.Col = MCOL
                    CTRL.RowSel = i
                    CTRL.ColSel = CTRL.Cols - 1
                    CTRL.TopRow = i
                    K = i
                    Exit Sub
                End If
            Next
        End If
        'EXAMPLE
        '    If KeyAscii <> 27 Then
        '  searchflx FLXCODE, KeyAscii
        ' End If
    End Sub


    Public Function loaddest(ByVal mdocentry As Integer) As String
        Dim msql As String
        msql = "select u_destination from oinv where docentry=" & mdocentry
        'Dim cmd As New OleDb.OleDbCommand(msql, con2)
        ''Dim CMD As New OleDb.OleDbCommand("SELECT section_id FROM section where sectionname='" & txtsectionname.Text & "' and  cmp_id='" & mcmpid & "'", con)
        'If con2.State = ConnectionState.Closed Then
        '    con2.Open()
        'End If
        'Dim itdest As String = IIf(IsDBNull(cmd.ExecuteScalar) = False, cmd.ExecuteScalar, 0)
        'loaddest = itdest
        'cmd.Dispose()


        'con2.Close()

        Dim itdest As String = executescalarQuery(msql)

        Return loaddest

    End Function

    Public Function loadcid(ByVal mtable As String, ByVal mfield As String, ByVal findstr As String) As String
        Dim msql As String
        msql = "select " & Trim(mfield) & " as mkid from " & Trim(mtable) & " where " & Trim(mfield) & "='" & Trim(findstr) & "' and cmp_id='" & mcmpid & "'"
        'Dim cmd As New SqlCommand(msql, con)
        ''Dim CMD As New OleDb.OleDbCommand("SELECT section_id FROM section where sectionname='" & txtsectionname.Text & "' and  cmp_id='" & mcmpid & "'", con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If
        'Dim secid As String = IIf(IsDBNull(cmd.ExecuteScalar) = False, cmd.ExecuteScalar, 0)
        'loadcid = secid
        'cmd.Dispose()

        Dim secid As String = executescalarQuery(msql)
        Return loadcid
    End Function

    Public Function imageToByteArray(ByVal imageIn As System.Drawing.Image) As Byte()
        Dim ms As MemoryStream = New MemoryStream
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif)
        Return ms.ToArray
    End Function

    Public Function WinPathToLinuxPath(winPath As String) As String
        Return winPath _
        .Replace("C:\", "/home/" & Environment.UserName & "/.wine/drive_c/") _
        .Replace("\", "/")
    End Function

    Public Sub OpenInLibreOffice(winFilePath As String)

        Dim linuxPath As String = WinPathToLinuxPath(winFilePath)
        'Dim linuxPath As String = filePath.Replace("C:\", "/home/" & Environment.UserName & "/.wine/drive_c/").Replace("\", "/")

        Process.Start("libreoffice", """" & linuxPath & """")
    End Sub


    Public Sub excelexport(ByVal CTRL As AxMSFlexGrid)


        Dim ldir, lmdir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\barcodadd.txt"

        If mos = "WIN" Then
            ldir = System.AppDomain.CurrentDomain.BaseDirectory()
            lmdir = Trim(ldir) & "PODDET.xls"
        Else
            lmdir = mxlfilepath & "PODDET.xls"
        End If


        Dim ticks As Integer = Environment.TickCount
        ' Create the workbook
        Dim book As Workbook = New Workbook
        ' Set the author
        book.Properties.Author = "CarlosAg"

        ' Add some style
        Dim style As WorksheetStyle = book.Styles.Add("style1")
        style.Font.Bold = True
        style.Font.Size = 12
        style.Alignment.Vertical = StyleVerticalAlignment.Center

        Dim sheet As Worksheet = book.Worksheets.Add("SampleSheet")
        'Dim style2 As WorksheetStyle = book.Styles.Add("style2")

        'style2.Font.Bold = False


        For I = 0 To CTRL.Rows - 1

            Dim Row0 As WorksheetRow = sheet.Table.Rows.Add

            For J = 0 To CTRL.Cols - 1

                ' Add a cell
                'Row0.Cells.Add("Hello World", DataType.String, "style1")
                Row0.Cells.Add(CTRL.get_TextMatrix(I, J), DataType.String, "style1")
            Next J
            If I = 0 Then
                'Dim style As WorksheetStyle = book.Styles.Add("style1")
                style.Font.Bold = True

            Else
                'style = book.Styles.Add("style1")
                'style = book.Styles.Add("style1")
                style.Font.Bold = False
                style.Font.Size = 10
                style.Alignment.Vertical = StyleVerticalAlignment.Top
                'style.Alignment.Horizontal = StyleHorizontalAlignment.Justify
            End If


        Next I

        ' Save it
        'book.Save("c:\test.xls")
        book.Save(lmdir)
        'open file
        If mos = "WIN" Then
            Process.Start(lmdir)
        Else
            OpenWithLibreOffice(lmdir)
        End If

        'Console.WriteLine("Time:{0}", (Environment.TickCount - ticks))
    End Sub

    Public Sub excelexport2(ByVal CTRL As AxMSFlexGrid, ByVal mhead As String)


        Dim ldir, lmdir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\barcodadd.txt"
        If mos = "WIN" Then
            ldir = System.AppDomain.CurrentDomain.BaseDirectory()
            lmdir = Trim(ldir) & "PODDET.xls"
        Else
            lmdir = mxlfilepath & "PODDET.xls"
        End If


        Dim ticks As Integer = Environment.TickCount
        ' Create the workbook
        Dim book As Workbook = New Workbook
        ' Set the author
        book.Properties.Author = "CarlosAg"

        ' Add some style
        Dim style As WorksheetStyle = book.Styles.Add("style1")
        style.Font.Bold = True
        style.Font.Size = 12
        style.Alignment.Vertical = StyleVerticalAlignment.Center

        Dim sheet As Worksheet = book.Worksheets.Add("SampleSheet")
        'Dim style2 As WorksheetStyle = book.Styles.Add("style2")

        'style2.Font.Bold = False
        If Len(Trim(mhead)) > 0 Then
            Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
            Row2.Cells.Add(mhead, DataType.String, "style1")
        End If

        For I = 0 To CTRL.Rows - 1

            Dim Row0 As WorksheetRow = sheet.Table.Rows.Add

            For J = 0 To CTRL.Cols - 1

                ' Add a cell
                'Row0.Cells.Add("Hello World", DataType.String, "style1")
                Row0.Cells.Add(CTRL.get_TextMatrix(I, J), DataType.String, "style1")
            Next J
            If I = 0 Then
                'Dim style As WorksheetStyle = book.Styles.Add("style1")
                style.Font.Bold = True

            Else
                'style = book.Styles.Add("style1")
                'style = book.Styles.Add("style1")
                style.Font.Bold = False
                style.Font.Size = 10
                style.Alignment.Vertical = StyleVerticalAlignment.Top
                'style.Alignment.Horizontal = StyleHorizontalAlignment.Justify
            End If


        Next I

        ' Save it
        'book.Save("c:\test.xls")
        book.Save(lmdir)
        'open file
        If mos = "WIN" Then
            Process.Start(lmdir)
        Else
            OpenWithLibreOffice(lmdir)
        End If


    End Sub

    Public Sub gridexcelexport(ByVal CTRL As DataGridView, ByVal lastcol As Integer)


        Dim ldir, lmdir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\barcodadd.txt"
        If mos = "WIN" Then
            ldir = System.AppDomain.CurrentDomain.BaseDirectory()
            lmdir = Trim(ldir) & "Perfrep.xls"
        Else
            lmdir = mxlfilepath & "Perfrep.xls"
        End If

        Dim ticks As Integer = Environment.TickCount
        ' Create the workbook
        Dim book As Workbook = New Workbook
        ' Set the author
        book.Properties.Author = "CarlosAg"

        ' Add some style
        Dim style As WorksheetStyle = book.Styles.Add("style1")
        style.Font.Bold = True
        style.Font.Size = 12
        style.Alignment.Vertical = StyleVerticalAlignment.Center

        Dim sheet As Worksheet = book.Worksheets.Add("SampleSheet")
        '
        Dim columnsCount As Integer = CTRL.Columns.Count - 1

        Dim Row As WorksheetRow = sheet.Table.Rows.Add
        'Dim column As Integer = 0
        Dim column = CTRL.Columns.Count - 1
        'Do Until column = columnsCount
        'For Each column In CTRL.Columns
        style.Font.Bold = True
        For iC As Integer = 0 To column
            'Do Until column = columnsCount - lastcol
            Row.Cells.Add(CTRL.Columns(iC).HeaderText & vbNullString, DataType.String, "style1")

            'Row2.Cells.Add(column.Name, DataType.String, "style1")
            'Worksheet.Cells(1, column.Index + 1).Value = column.Name
        Next
        'Export Header Name End
        style = book.Styles.Add("style2")
        style.Font.Bold = False
        style.Font.Size = 10
        style.Alignment.Vertical = StyleVerticalAlignment.Top

        'Export Each Row Start
        For i As Integer = 0 To CTRL.Rows.Count - 1
            'Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
            Row = sheet.Table.Rows.Add
            Dim columnIndex As Integer = 0
            'Do Until columnIndex = columnsCount - (lastcol - 1)
            Do Until columnIndex = columnsCount
                Row.Cells.Add(CTRL.Item(columnIndex, i).Value.ToString & vbNullString, DataType.String, "style2")
                'Row2.Cells(i + 2, columnIndex + 1).Value = CTRL.Item(columnIndex, i).Value.ToString
                columnIndex += 1
            Loop
        Next




        book.Save(lmdir)
        'open file
        If mos = "WIN" Then
            Process.Start(lmdir)
        Else
            OpenWithLibreOffice(lmdir)
        End If
    End Sub



    Public Sub MSFlexGridExportToCSV(ByVal grid As AxMSFlexGrid, Optional csvfilename As String = "")

        Dim baseDir As String = AppDomain.CurrentDomain.BaseDirectory
        Dim csvPath As String = IO.Path.Combine(baseDir, "PODDET.csv")
        If Len(Trim(csvfilename)) > 0 Then
            csvPath = IO.Path.Combine(baseDir, csvfilename)
        Else
            csvPath = IO.Path.Combine(baseDir, "PODDET.csv")
        End If

        Using sw As New IO.StreamWriter(csvPath, False, System.Text.Encoding.UTF8)

            For i As Integer = 0 To grid.Rows - 1

                Dim rowValues As New List(Of String)

                For j As Integer = 0 To grid.Cols - 1
                    Dim cellValue As String = grid.get_TextMatrix(i, j)
                    cellValue = cellValue.Replace("""", """""") ' escape quotes
                    rowValues.Add("""" & cellValue & """")
                Next

                sw.WriteLine(String.Join(",", rowValues))

            Next

        End Using

        ' Open in LibreOffice
        'OpenCSVInLibreOffice(csvPath)

    End Sub


    Public Sub OpenCSVInLibreOffice(winPath As String)

        Dim linuxPath As String =
            winPath.Replace("C:\", "/home/" & Environment.UserName & "/.wine-dotnet/drive_c/") _
                   .Replace("\", "/")

        Process.Start("libreoffice", """" & linuxPath & """")

    End Sub
    Public Sub OpenWithLibreOffice(filePath As String)
        'If Not IO.File.Exists(filePath) Then
        '    MessageBox.Show("File not found")
        '    Exit Sub
        'End If

        'Dim psi As New ProcessStartInfo()
        'psi.FileName = "cmd.exe"
        'psi.Arguments = "/c libreoffice --calc """ & filePath & """"
        'psi.CreateNoWindow = True
        'psi.UseShellExecute = False

        'Process.Start(psi)


        If Not IO.File.Exists(filePath) Then
            MessageBox.Show("File not found")
            Exit Sub
        End If

        Dim psi As New ProcessStartInfo()
        psi.FileName = "/usr/bin/xdg-open"
        psi.Arguments = """" & filePath & """"
        psi.UseShellExecute = False
        psi.CreateNoWindow = True

        Process.Start(psi)


    End Sub
    Public Sub OpenCalcDirect(filePath As String)
        Process.Start("libreoffice", "--calc """ & filePath & """")
    End Sub

    Public Sub ExportDgvToCsv(dgv As DataGridView, Optional csvfilename As String = "")

        Dim baseDir As String = AppDomain.CurrentDomain.BaseDirectory
        Dim csvPath As String
        If Len(Trim(csvfilename)) > 0 Then
            csvPath = IO.Path.Combine(baseDir, csvfilename)
        Else
            csvPath = IO.Path.Combine(baseDir, "PODDET.csv")
        End If


        Using sw As New IO.StreamWriter(csvPath, False, System.Text.Encoding.UTF8)

            ' Header
            Dim headers = dgv.Columns.Cast(Of DataGridViewColumn)().
                          Select(Function(c) c.HeaderText)
            sw.WriteLine(String.Join(",", headers))

            ' Rows
            For Each row As DataGridViewRow In dgv.Rows
                If Not row.IsNewRow Then
                    Dim cells = row.Cells.Cast(Of DataGridViewCell)().
                                Select(Function(c) """" & c.Value & """")
                    sw.WriteLine(String.Join(",", cells))
                End If
            Next

        End Using

    End Sub



    Public Function ReadExcelAnyOS(path As String) As DataTable

        Dim dt As New DataTable()
        Dim wb As IWorkbook

        Using fs As New IO.FileStream(path, IO.FileMode.Open, IO.FileAccess.Read)
            If path.ToLower().EndsWith(".xls") Then
                wb = New HSSFWorkbook(fs)
            Else
                wb = New XSSFWorkbook(fs)
            End If
        End Using

        Dim sheet = wb.GetSheetAt(0)
        Dim header = sheet.GetRow(0)

        For i As Integer = 0 To header.LastCellNum - 1
            dt.Columns.Add(header.GetCell(i).ToString())
        Next

        For r As Integer = 1 To sheet.LastRowNum
            Dim row = sheet.GetRow(r)
            If row IsNot Nothing Then
                Dim dr = dt.NewRow()
                For c As Integer = 0 To dt.Columns.Count - 1
                    Dim cell = row.GetCell(c)
                    dr(c) = If(cell Is Nothing, "", cell.ToString())
                Next
                dt.Rows.Add(dr)
            End If
        Next

        Return dt

        '**usage
        'DataGridView1.DataSource = ReadExcelAnyOS("C:\MyApp\Report.xlsx")
    End Function

    Public Sub LoadToMSFlexGrid(grid As AxMSFlexGrid, dt As DataTable)
        ' Clear existing grid
        grid.Clear()

        ' Set rows and columns
        grid.Rows = dt.Rows.Count + 1  ' +1 for header
        grid.Cols = dt.Columns.Count
        'grid.get_TextMatrix(1, 1)
        ' Fill header row
        For c As Integer = 0 To dt.Columns.Count - 1
            grid.set_TextMatrix(0, c, dt.Columns(c).ColumnName)
        Next

        ' Fill data rows
        For r As Integer = 0 To dt.Rows.Count - 1
            For c As Integer = 0 To dt.Columns.Count - 1
                grid.set_TextMatrix(r + 1, c, dt.Rows(r)(c).ToString())
            Next
        Next

        '**usage
        'LoadToMSFlexGrid(AxMSFlexGrid1, dt)
    End Sub


    Public Function CallCrystalPrintService(req As PrintRequest) As Boolean
        Try
            'Dim url As String = "http://yourserver/Print/PrintReport"
            ' Dim url As String = "http://localhost/crystalprintservice/api/Print/PrintReport"
            Dim url As String = mprintapi

            Dim json As String = JsonConvert.SerializeObject(req)
            Dim data As Byte() = Encoding.UTF8.GetBytes(json)

            Dim request = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.ContentLength = data.Length
            request.Timeout = 30000

            Using stream = request.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using

            Using response = CType(request.GetResponse(), HttpWebResponse)
                Return response.StatusCode = HttpStatusCode.OK
            End Using

        Catch ex As Exception
            ' Log ex.Message if needed
            Return False
        End Try
    End Function


    Public Function ViewCrystalPDF(req As PrintRequest) As Boolean
        Try
            ' Dim url As String = "http://yourserver/Print/ViewReport"
            Dim url As String = "http://localhost/crystalprintservice/api/Print/ViewReport"

            Dim json As String = JsonConvert.SerializeObject(req)
            Dim data As Byte() = Encoding.UTF8.GetBytes(json)

            Dim request = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.ContentLength = data.Length

            Using stream = request.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using

            Dim response = CType(request.GetResponse(), HttpWebResponse)

            ' Save PDF locally
            Using fs As New IO.FileStream("D:\Temp\report.pdf", IO.FileMode.Create)
                response.GetResponseStream().CopyTo(fs)
            End Using

            Process.Start("D:\Temp\report.pdf")
            Return True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function


    Public Function PrintTscRaw(device As String, filePath As String) As Boolean
        Try
            Dim psi As New ProcessStartInfo()
            Dim scriptPath As String = mapppath & "/print_raw.sh"
            psi.FileName = "/bin/bash"
            'psi.Arguments = "/home/user/print_raw.sh " & device & " """ & filePath & """"
            psi.Arguments = """" & scriptPath & """ " & device & " """ & filePath & """"

            psi.UseShellExecute = False
            psi.CreateNoWindow = True

            Process.Start(psi)
            Return True
        Catch
            Return False
        End Try
    End Function
    Public Function GetDefaultPrinter() As String
        Dim psi As New ProcessStartInfo()
        psi.FileName = "lpstat"
        psi.Arguments = "-d"
        psi.RedirectStandardOutput = True
        psi.UseShellExecute = False
        psi.CreateNoWindow = True

        Using p As Process = Process.Start(psi)
            Dim output As String = p.StandardOutput.ReadToEnd()
            p.WaitForExit()

            ' Output: "system default destination: HP_LaserJet"
            If output.Contains(":") Then
                Return output.Split(":"c)(1).Trim()
            End If
        End Using

        Return ""
    End Function

    Public Function PrintPdfSilent(pdfPath As String, Optional printerName As String = "") As Boolean
        Try
            If Not IO.File.Exists(pdfPath) Then Return False

            If printerName = "" Then
                printerName = GetDefaultPrinter()
            End If

            Dim args As String
            If printerName <> "" Then
                args = "-d """ & printerName & """ """ & pdfPath & """"
            Else
                args = """" & pdfPath & """"
            End If

            Dim psi As New ProcessStartInfo()
            psi.FileName = "lp"
            psi.Arguments = args
            psi.UseShellExecute = False
            psi.CreateNoWindow = True

            Process.Start(psi)
            Return True

        Catch
            Return False
        End Try
    End Function

    Public Function PrintTextFile(filePath As String, Optional printerName As String = "") As Boolean
        Try
            If printerName = "" Then
                printerName = GetDefaultPrinter()
            End If

            Dim args As String
            If printerName <> "" Then
                args = "-d """ & printerName & """ """ & filePath & """"
            Else
                args = """" & filePath & """"
            End If

            Process.Start(New ProcessStartInfo With {
                .FileName = "lp",
                .Arguments = args,
                .UseShellExecute = False,
                .CreateNoWindow = True
            })

            Return True
        Catch
            Return False
        End Try
    End Function
    Public Function PrintCrystalReportold(req As PrintRequest, ByVal printview As Boolean) As Boolean
        Try

            For Each f In IO.Directory.GetFiles(Path.GetTempPath(), "*.pdf")
                Try
                    IO.File.Delete(f)
                Catch
                    ' Ignore if file is in use
                End Try
            Next

            ' -----------------------------
            ' 1. API URL
            ' -----------------------------
            'Dim apiUrl As String = "http://localhost/crystalprintservice/api/Print/ViewReport"


            Dim apiUrl As String = mapiurl

            ' -----------------------------
            ' 2. Serialize request to JSON
            ' -----------------------------
            Dim json As String = JsonConvert.SerializeObject(req)
            Dim data As Byte() = Encoding.UTF8.GetBytes(json)

            ' -----------------------------
            ' 3. Create HTTP POST request
            ' -----------------------------
            Dim request = CType(WebRequest.Create(apiUrl), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.ContentLength = data.Length

            Using stream = request.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using

            ' -----------------------------
            ' 4. Get response from API
            ' -----------------------------
            Dim response = CType(request.GetResponse(), HttpWebResponse)

            ' -----------------------------
            ' 5. Save PDF locally
            ' -----------------------------
            'Dim localPdf As String = Path.Combine(Path.GetTempPath(), req.ReportName & "_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".pdf")
            Dim localPdf As String = "D:\Temp\" & req.ReportName & ".pdf"
            'Using fs As New FileStream(localPdf, FileMode.Create)
            '    response.GetResponseStream().CopyTo(fs)
            'End Using

            Using respStream As Stream = response.GetResponseStream()
                Using fs As New FileStream(localPdf, FileMode.Create, FileAccess.Write, FileShare.None)
                    respStream.CopyTo(fs)
                    fs.Flush()
                End Using
            End Using


            ' -----------------------------
            ' 6. Print PDF silently
            ' -----------------------------
            If printview = True Then
                If Not String.IsNullOrEmpty(req.PrinterName) Then
                    ' Try Adobe Reader method (specific printer)
                    Dim adobePath As String = "C:\Program Files\Adobe\Acrobat Reader DC\Reader\AcroRd32.exe"
                    If File.Exists(adobePath) Then
                        Dim psi As New ProcessStartInfo()
                        psi.FileName = adobePath
                        psi.Arguments = $"/s /o /t ""{localPdf}"" ""{req.PrinterName}"""
                        psi.CreateNoWindow = True
                        psi.WindowStyle = ProcessWindowStyle.Hidden
                        psi.UseShellExecute = False
                        Dim p As Process = Process.Start(psi)

                        ' Wait a few seconds for print job to start
                        'Threading.Thread.Sleep(5000)

                        '' Close Adobe Reader if still open
                        'If Not p.HasExited Then
                        '    p.Kill()
                        'End If


                        Dim waitTime As Integer = 0
                        While Not p.HasExited AndAlso waitTime < 10000
                            Threading.Thread.Sleep(500)
                            waitTime += 500
                        End While
                        If Not p.HasExited Then p.Kill()


                    Else
                        ' Fallback: default printer
                        Dim psi As New ProcessStartInfo()
                        psi.FileName = localPdf
                        psi.Verb = "print"
                        psi.CreateNoWindow = True
                        psi.WindowStyle = ProcessWindowStyle.Hidden
                        psi.UseShellExecute = True
                        Process.Start(psi)

                        ' Wait a few seconds
                        Threading.Thread.Sleep(5000)
                    End If
                End If
            Else
                '    Process.Start(New ProcessStartInfo(localPdf) With {
                '    .UseShellExecute = True
                '})

                Dim fileUri As String = New Uri(localPdf).AbsoluteUri

                Process.Start(New ProcessStartInfo() With {
                    .FileName = fileUri,
                    .UseShellExecute = True
                })
            End If


            ' -----------------------------
            ' 7. Delete PDF after printing
            ' -----------------------------
            If File.Exists(localPdf) Then
                If printview = False Then
                    File.Delete(localPdf)
                End If

            End If

            Return True

        Catch ex As Exception
            MessageBox.Show("Error printing report: " & ex.Message)
            Return False
        End Try
    End Function


    Public Function PrintCrystalReport(req As PrintRequest, ByVal printview As Boolean) As Boolean
        Try
            ' -----------------------------
            ' 0. Ensure folder exists
            ' -----------------------------
            Dim localPdf As String = ""
            Dim folder As String = ""
            If mos = "WIN" Then
                folder = "D:\Temp"
            Else
                folder = mlintmpfolder
            End If

            If Not Directory.Exists(folder) Then
                Directory.CreateDirectory(folder)
            End If
            'Dim localPdf As String = Path.Combine(folder, req.ReportName & ".pdf")

            localPdf = Path.Combine(folder, req.ReportName & ".pdf")
            If mos = "WIN" Then
                'localPdf = Path.Combine(folder, req.ReportName & ".pdf")
            Else
                localPdf = localPdf.Replace("\", "/")
            End If


            ' -----------------------------
            ' 1. API URL
            ' -----------------------------
            Dim apiUrl As String = mapiurl

            ' -----------------------------
            ' 2. Serialize request to JSON
            ' -----------------------------
            Dim json As String = JsonConvert.SerializeObject(req)
            Dim data As Byte() = Encoding.UTF8.GetBytes(json)

            ' -----------------------------
            ' 3. Create HTTP POST request
            ' -----------------------------
            Dim request = CType(WebRequest.Create(apiUrl), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.Accept = "application/pdf"
            request.Timeout = 600000
            request.ContentLength = data.Length

            Using stream = request.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using

            ' -----------------------------
            ' 4. Get response
            ' -----------------------------
            Dim response = CType(request.GetResponse(), HttpWebResponse)

            If response.StatusCode <> HttpStatusCode.OK Then
                Throw New Exception("API Error: " & response.StatusCode.ToString())
            End If

            ' -----------------------------
            ' 5. Save PDF locally
            ' -----------------------------
            'localPdf = Path.Combine(folder, req.ReportName & ".pdf")
            'If mos = "WIN" Then
            '    'localPdf = Path.Combine(folder, req.ReportName & ".pdf")
            'Else
            '    localPdf = localPdf.Replace("\", "/")
            'End If

            Using respStream As Stream = response.GetResponseStream()
                Using fs As New FileStream(localPdf, FileMode.Create, FileAccess.Write, FileShare.None)
                    respStream.CopyTo(fs)
                    fs.Flush()
                End Using
            End Using

            ' -----------------------------
            ' 5A. WAIT until file is fully written (CRITICAL)
            ' -----------------------------
            Dim retry As Integer = 0
            While (Not File.Exists(localPdf) OrElse New FileInfo(localPdf).Length = 0) AndAlso retry < 10
                Threading.Thread.Sleep(300)
                retry += 1
            End While

            If Not File.Exists(localPdf) Then
                Throw New Exception("PDF file not created")
            End If

            ' -----------------------------
            ' 6. Print OR View
            ' -----------------------------
            If mos = "WIN" Then
                If printview = True Then
                    If Not String.IsNullOrEmpty(req.PrinterName) Then
                        Dim adobePath As String = "C:\Program Files\Adobe\Acrobat Reader DC\Reader\AcroRd32.exe"

                        If File.Exists(adobePath) Then
                            Dim psi As New ProcessStartInfo()
                            psi.FileName = adobePath
                            psi.Arguments = $"/s /o /t ""{localPdf}"" ""{req.PrinterName}"""
                            psi.UseShellExecute = False
                            psi.CreateNoWindow = True
                            psi.WindowStyle = ProcessWindowStyle.Hidden

                            Dim p = Process.Start(psi)

                            Dim waitTime As Integer = 0
                            While Not p.HasExited AndAlso waitTime < 10000
                                Threading.Thread.Sleep(500)
                                waitTime += 500
                            End While
                            If Not p.HasExited Then p.Kill()
                        Else
                            Process.Start(New ProcessStartInfo(localPdf) With {
                        .Verb = "print",
                        .UseShellExecute = True,
                        .CreateNoWindow = True
                    })
                        End If
                    End If
                Else
                    ' VIEW PDF (DO NOT DELETE)
                    Process.Start(New ProcessStartInfo() With {
                .FileName = New Uri(localPdf).AbsoluteUri,
                .UseShellExecute = True
            })
                End If
            Else
                'linux
                If printview = True Then
                    If Not String.IsNullOrEmpty(req.PrinterName) Then

                        ' Linux bash path (Wine compatible)
                        Dim bashPath As String = "/bin/bash"
                        Dim scriptPath As String = mlinpath & "print_pdf.sh"

                        If Not File.Exists(scriptPath) Then
                            Throw New Exception("Print script not found: " & scriptPath)
                        End If

                        Dim psi As New ProcessStartInfo()
                            psi.FileName = bashPath
                            psi.Arguments = $"""{scriptPath}"" ""{localPdf}"" ""{req.PrinterName}"""
                            psi.UseShellExecute = False
                            psi.CreateNoWindow = True

                        psi.RedirectStandardOutput = False
                        psi.RedirectStandardError = False
                        Try
                            Dim p As Process = Process.Start(psi)
                        Catch ex As Exception
                            Throw New Exception("Error starting print command: " & ex.Message)
                        End Try

                        '    If p Is Nothing Then
                        '        Throw New Exception("Failed to start print process")
                        '    End If

                        '    p.WaitForExit(10000)

                        '    If Not p.HasExited Then
                        '        p.Kill()
                        '    End If

                        '    If Not p.WaitForExit(10000) Then
                        '        Try
                        '            p.Kill()
                        '        Catch
                        '        End Try
                        '    End If
                        '    Dim output As String = p.StandardOutput.ReadToEnd()
                        '    Dim err As String = p.StandardError.ReadToEnd()

                        'If Not String.IsNullOrEmpty(err) Then
                        '    Throw New Exception("Print error: " & err)
                        'End If

                    End If

                Else
                    ' VIEW PDF (Linux default viewer)
                    'Process.Start(New ProcessStartInfo() With {
                    '    .FileName = "/usr/bin/xdg-open",
                    '    .Arguments = """" & localPdf & """",
                    '    .UseShellExecute = True
                    '})

                    If Not File.Exists(localPdf) Then
                        Throw New Exception("PDF not found: " & localPdf)
                    End If

                    Process.Start(New ProcessStartInfo() With {
                        .FileName = "/bin/bash",
                        .Arguments = "-c ""xdg-open '" & localPdf & "'""",
                        .UseShellExecute = False,
                        .CreateNoWindow = True
                    })

                End If


            End If


            ' -----------------------------
            ' 7. Delete ONLY after printing
            ' -----------------------------
            If printview = True AndAlso File.Exists(localPdf) Then
                Try
                    File.Delete(localPdf)
                Catch
                End Try
            End If

            Return True

        Catch ex As Exception
            MessageBox.Show("Error printing report: " & ex.Message)
            Return False
        End Try
    End Function
    Public Function PrintCrystalReportlinux(req As PrintRequest, ByVal printview As Boolean, filename As String, Optional digitalsign As Boolean = False) As Boolean
        Try
            ' -----------------------------
            ' 0. Folder + Unique file
            ' -----------------------------
            Dim folder As String = mlintmpfolder

            If Not Directory.Exists(folder) Then
                Directory.CreateDirectory(folder)
            End If

            '  UNIQUE FILE NAME (CRITICAL FIX)
            Dim uniqueName As String = Guid.NewGuid().ToString()
            Dim localPdf As String = Path.Combine(folder, uniqueName & "_" & filename)

            ' -----------------------------
            ' 1. API Call
            ' -----------------------------
            '  ADD UNIQUE QUERY (ANTI-CACHE)
            'Dim apiUrl As String = mapiurl & "?t=" & DateTime.Now.Ticks.ToString()

            Dim apiUrl As String = mapiurl

            Dim json As String = JsonConvert.SerializeObject(req)
            Dim data As Byte() = Encoding.UTF8.GetBytes(json)

            Dim request = CType(WebRequest.Create(apiUrl), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.Accept = "application/pdf"
            request.Timeout = 600000
            request.ContentLength = data.Length

            '  NO CACHE HEADERS
            request.Headers.Add("Cache-Control", "no-cache")
            request.Headers.Add("Pragma", "no-cache")
            request.Headers.Add("Expires", "-1")

            Using stream = request.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using

            Dim response = CType(request.GetResponse(), HttpWebResponse)

            If response.StatusCode <> HttpStatusCode.OK Then
                Throw New Exception("API Error: " & response.StatusCode.ToString())
            End If

            ' -----------------------------
            ' 2. Save PDF
            ' -----------------------------
            Using respStream As Stream = response.GetResponseStream()
                Using fs As New FileStream(localPdf, FileMode.Create, FileAccess.Write, FileShare.None)
                    respStream.CopyTo(fs)
                    fs.Flush()
                End Using
            End Using

            '  WAIT UNTIL FILE READY
            Dim retry As Integer = 0
            While (Not File.Exists(localPdf) OrElse New FileInfo(localPdf).Length = 0) AndAlso retry < 10
                Threading.Thread.Sleep(300)
                retry += 1
            End While

            If Not File.Exists(localPdf) Then
                Throw New Exception("PDF not created")
            End If

            ' -----------------------------
            ' 3. PRINT / VIEW
            ' -----------------------------
            If printview = True Then

                If Not String.IsNullOrEmpty(req.PrinterName) Then

                    'Dim adobepath As String = GetAcrobatReaderPath()

                    'If Not String.IsNullOrEmpty(adobepath) AndAlso File.Exists(adobepath) Then

                    '    '  FORCE GC (release file locks)
                    '    GC.Collect()
                    '    GC.WaitForPendingFinalizers()

                    '    '  SILENT PRINT
                    '    Dim psi As New ProcessStartInfo()
                    '    psi.FileName = adobepath
                    '    psi.Arguments = "/s /o /t """ & localPdf & """ """ & req.PrinterName & """"
                    '    psi.UseShellExecute = False
                    '    psi.CreateNoWindow = True
                    '    psi.WindowStyle = ProcessWindowStyle.Hidden

                    '    Dim p = Process.Start(psi)
                    '    p.WaitForExit(10000)

                    '    If Not p.HasExited Then p.Kill()

                    'Else
                    '    ' FALLBACK PRINT
                    '    Process.Start(New ProcessStartInfo(localPdf) With {
                    '        .Verb = "print",
                    '        .UseShellExecute = True
                    '    })
                    'End If

                    Dim result As Boolean = PrintPdfToLaser(localPdf, req.PrinterName)
                    If Not result Then
                        Throw New Exception("Laser print failed")
                    Else
                        SafeDeleteFile(localPdf)
                    End If
                End If

            Else
                '  VIEW PDF (always fresh file)
                Threading.Thread.Sleep(300)

                Process.Start(New ProcessStartInfo() With {
                    .FileName = New Uri(localPdf).AbsoluteUri,
                    .UseShellExecute = True
                })
            End If

            ' -----------------------------
            ' 4. SAFE DELETE (BACKGROUND)
            ' -----------------------------
            If printview = True Then

                Threading.Tasks.Task.Factory.StartNew(Sub()
                                                          Try
                                                              Threading.Thread.Sleep(5000)

                                                              If File.Exists(localPdf) Then
                                                                  File.Delete(localPdf)
                                                              End If

                                                          Catch
                                                          End Try
                                                      End Sub)
            End If

            Return True

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Sub SafeDeleteFile(filePath As String)
        Threading.Tasks.Task.Factory.StartNew(Sub()
                                                  Try
                                                      ' Wait for spooler to finish
                                                      Threading.Thread.Sleep(8000)

                                                      Dim retry As Integer = 0

                                                      While File.Exists(filePath) AndAlso retry < 5
                                                          Try
                                                              File.Delete(filePath)
                                                              Exit While
                                                          Catch
                                                              Threading.Thread.Sleep(1000)
                                                              retry += 1
                                                          End Try
                                                      End While

                                                  Catch
                                                  End Try
                                              End Sub)
    End Sub

    Public Function printCrystalPDF(req As PrintRequest) As Boolean
        Try
            ' 1. API URL
            Dim url As String = "http://localhost/crystalprintservice/api/Print/ViewReport"

            ' 2. Serialize request to JSON
            Dim json As String = JsonConvert.SerializeObject(req)
            Dim data As Byte() = Encoding.UTF8.GetBytes(json)

            ' 3. Create POST request
            Dim request = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.ContentLength = data.Length

            Using stream = request.GetRequestStream()
                stream.Write(data, 0, data.Length)
            End Using

            ' 4. Get response
            Dim response = CType(request.GetResponse(), HttpWebResponse)

            ' 5. Save PDF locally
            Dim localPdf As String = IO.Path.Combine(IO.Path.GetTempPath(), req.ReportName & ".pdf")
            'Dim localPdf As String = "D:\Temp\report.pdf"
            Using fs As New IO.FileStream(localPdf, IO.FileMode.Create)
                response.GetResponseStream().CopyTo(fs)
            End Using

            ' 6. Print PDF using default PDF viewer
            ' For default printer (simplest):
            Dim psi As New ProcessStartInfo()
            psi.FileName = localPdf
            psi.Verb = "print"              ' Send to default printer
            psi.CreateNoWindow = True
            psi.WindowStyle = ProcessWindowStyle.Hidden
            psi.UseShellExecute = True
            Process.Start(psi)

            ' Optional: wait a few seconds for print job to start
            Threading.Thread.Sleep(5000)

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function


    Public Function PrintPdfToLaser(pdfPath As String, printerName As String) As Boolean
        Try
            '  Update this path based on your installation
            'Dim gsPath As String = "C:\Program Files\gs\gs10.02.0\bin\gswin64c.exe"

            Dim gsPath As String = mgspath & "gswin64c.exe"

            If Not IO.File.Exists(gsPath) Then
                Throw New Exception("Ghostscript not installed")
            End If

            '  IMPORTANT SETTINGS (NO SHRINK, A4 FIX)
            Dim args As String =
                "-dBATCH -dNOPAUSE -dNOSAFER " &
                "-sDEVICE=mswinpr2 " &
                "-dFIXEDMEDIA -dFitPage=false " &
                "-sOutputFile=""%printer%" & printerName & """ " &
                """" & pdfPath & """"

            Dim psi As New ProcessStartInfo()
            psi.FileName = gsPath
            psi.Arguments = args
            psi.UseShellExecute = False
            psi.CreateNoWindow = True

            Dim p As Process = Process.Start(psi)
            p.WaitForExit()

            If p.ExitCode <> 0 Then
                Throw New Exception("Ghostscript print failed")
            End If

            Return True

        Catch ex As Exception
            MessageBox.Show("Print Error: " & ex.Message)
            Return False
        End Try
    End Function

    'Public Async Function PrintReportFromAPI() As Task
    '    ' 1. API URL
    '    Dim apiUrl As String = "http://yourserver/api/PrintReport"

    '    ' 2. Create request object
    '    Dim req As New PrintRequest() With {
    '        .ReportName = "purorderpending",
    '        .PrinterName = "EPSON TM-T82",
    '        .UseDB = False,
    '        .ServerName = "",
    '        .DatabaseName = "",
    '        .DBUser = "",
    '        .DBPassword = "",
    '        .Parameters = New Dictionary(Of String, Object) From {
    '            {"ac", "VENNILA CLOTH. COMPANY-WAR(V)"}
    '        }
    '    }

    '    ' 3. Serialize to JSON
    '    Dim json As String = JsonConvert.SerializeObject(req)
    '    Dim content As New StringContent(json, Encoding.UTF8, "application/json")

    '    ' 4. Send POST request
    '    Using client As New HttpClient()
    '        Dim response = Await client.PostAsync(apiUrl, content)

    '        If response.IsSuccessStatusCode Then
    '            ' 5. Read PDF bytes
    '            Dim pdfBytes = Await response.Content.ReadAsByteArrayAsync()
    '            Dim localPdf As String = IO.Path.Combine(IO.Path.GetTempPath(), req.ReportName & ".pdf")

    '            ' 6. Save locally
    '            IO.File.WriteAllBytes(localPdf, pdfBytes)

    '            ' 7. Print PDF locally
    '            PrintPdf(localPdf, req.PrinterName)

    '        Else
    '            MessageBox.Show("API Error: " & response.StatusCode.ToString())
    '        End If
    '    End Using
    'End Function


End Module

'usage cups print
'Dim success As Boolean = PrintPdfSilent("/home/user/reports/invoice.pdf")

'If success Then
'MessageBox.Show("PDF sent to printer")
'Else
'MessageBox.Show("Print failed")
'End If

'🔹 Print text report

'PrintTextFile("/home/user/reports/forwarding.txt")


''Call api
'Dim paramDict As New Dictionary(Of String, Object)
'paramDict("FromDate") = fromDate.ToString("yyyy-MM-dd")
'paramDict("ToDate") = toDate.ToString("yyyy-MM-dd")
'paramDict("CustomerId") = customerId

'' Build request
'Dim req As New PrintRequest With {
'    .ReportName = reportName,
'    .PrinterName = printerName,
'    .UseDB = True,
'    .ServerName = serverName,
'    .DatabaseName = databaseName,
'    .DBUser = dbUser,
'    .DBPassword = dbPassword,
'    .Parameters = paramDict
'}

'' Call API
'Dim success As Boolean = CallCrystalPrintService(req)
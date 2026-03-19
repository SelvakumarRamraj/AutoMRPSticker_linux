Imports AxMSFlexGridLib
Imports System.IO
Imports System.Data
Imports CarlosAg.ExcelXmlWriter
Imports System.Data.OleDb
Imports System.Data.SqlClient
'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.ReportSource
'Imports CrystalDecisions.CrystalReports.Engine.Section
'Imports CrystalDecisions.CrystalReports.Engine.Sections

Imports System.Net.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Net.Mail.MailMessage
Imports System.Net.Mail.Attachment
Imports System.Configuration
Imports System.Collections.Specialized
Imports System.Text
Imports Microsoft.VisualBasic
Imports System.Drawing.Drawing2D
Imports System.Collections.Generic
Imports System
Imports System.Web
Imports Newtonsoft.Json
Imports System.Linq
Imports NPOI.SS.UserModel
Imports NPOI.HSSF.UserModel
Imports NPOI.XSSF.UserModel
Imports System.Net


'Imports Excel = Microsoft.Office.Interop.Excel

'Imports Excel
Module Module1
    Public fword As String
    Public sword As String
    Public wrdamt As String
    Public stramt As String
    Public stramts As String
    Public mpaise As String
    Public stramts1 As String
    Public SECOND As String
    Public FIRST As String
    Public muser As String
    Public musetyp As String
    'Public con As OleDb.OleDbConnection
    'Public con2 As OleDb.OleDbConnection
    'Public con3 As OleDb.OleDbConnection
    'Public con4 As OleDb.OleDbConnection


    Public con As SqlConnection
    Public con2 As SqlConnection
    Public con3 As SqlConnection
    Public con4 As SqlConnection


    Public constr As String
    Public OWIDTH As Integer
    Public OHEIGHT As Integer
    Public ORES As String
    Public OREF As Integer
    Public OBIT As Integer
    Public dbnam As String
    Public mkserver As String
    Public mcmpid As String
    Public mcmpyrid As String
    Public mcmpyr As String
    Public mcmpname As String
    Public mkserver2 As String
    Public dbnam2 As String
    Public constr2 As String
    Public constr4 As String
    Public dbnam4 As String
    Public mreppath As String
    Public dbuser As String
    Public winauth As String
    Public mkpwd As String
    Public mprovider As String
    Public mpostperiod As String
    Public mProdMktbarcode As String
    Public mcostdbnam As String
    Public pnldate As Date
    Public msameserver As String
    Public msamedb As String
    Public minvrepname As String
    Public mdelrepname As String
    Public mloryrepname As String
    Public mfwrepname As String
    Public mos As String
    Public mlinpath As String
    Public mlinapppath As String
    Public mlsprinter As String
    Public tscprinter1 As String
    Public tscprinter2 As String
    Public mprintapi As String
    Public mapppath As String
    Public mapiurl As String
    Public mlintmpfolder As String
    Public mxlfilepath As String

    'Public con As String
    'home
    ' Public con As New SqlClient.SqlConnection("Data Source=selvapc;Initial Catalog=purchaseorder;Persist Security Info=True;User ID=sa;Password=sa536")
    'rhl
    'Public CON As New SqlClient.SqlConnection("Data Source=SERVERHO;Initial Catalog=purchaseorder;Integrated Security=True")
    'ants
    'Public con As New SqlClient.SqlConnection("Data Source=ANTSHO;Initial Catalog=purchaseorder;User ID=sa")
    'Public con






    Dim I As Integer
    Dim J As Integer
    Dim MN As Integer
    Dim NTR As Integer
    Dim tmpp As String
    Dim strp As String
    Dim POSITION As Integer
    Dim MLACS, MCRORES, MTHOUS, MHUND, MTENS, MRS As String
    Dim myr As String
    Dim mkyr As String
    Dim mm As String
    'FORM RESIZE
    Dim c(6, 0) As String
    Dim xx, yy As Long
    'Dim msql As String
    Public rmkpwd As String
    Public rmprovider As String
    Public rmkserver As String
    Public rdbnam As String
    Public rdbuser As String
    Public rconstr As String
    Public networklib As String
    Public linkserver As String
    Public brlinkserver As String
    Public brdbnam As String
    Public curcompname As String
    Public mautopnl As String
    Public nxtyr As String
    Public mrtgsrepname As String
    Public Const MAX_IMAGE_SIZE As Long = 32768 ' 32 Ko
    Dim ms As New System.IO.MemoryStream
    Dim picture As Image
    Dim arrPicture() As Byte
    Dim arr() As Byte
    Public trans1 As OleDbTransaction
    Public trans As SqlTransaction
    Dim merr, merr2 As String
    Public msmtp, mport, mmailid, mmailpwd, mmailusername, mccmailid, mrdoccode, mpdffile As String

    Public mempno As String = String.Empty

    Public mempname As String = String.Empty
    Public mempid As String = String.Empty
    Public mempsalary As Double = 0
    Public mempgrade As String = String.Empty
    Public mprsno As Integer = 0
    Public mprocnam As String = String.Empty
    Public mcontprocfilt As String = String.Empty

    Public mopername As String = String.Empty
    Public mmactype As String = String.Empty
    Public msam As String = String.Empty
    Public mstyl As String = String.Empty
    Public mproces As String = String.Empty
    Public mitname As String = String.Empty
    Public mitstyle As String = String.Empty
    Public mjbgrade As String = String.Empty
    Public mkdate As Date











    Public Sub main()


        If Microsoft.VisualBasic.Format(CDate(Now()), "MM") > 3 And Microsoft.VisualBasic.Format(CDate(Now), "MM") <= 12 Then
            myr = Microsoft.VisualBasic.Format(CDate(Now()), "yyyy") & "-" & (Microsoft.VisualBasic.Format(CDate(Now()), "yyyy")) + 1
        Else
            myr = (Microsoft.VisualBasic.Format(CDate(Now()), "yyyy")) - 1 & "-" & Microsoft.VisualBasic.Format(CDate(Now()), "yyyy")
        End If



        mkserver = System.Configuration.ConfigurationSettings.AppSettings("myservername")
        dbnam = ConfigurationSettings.AppSettings("mydbname")
        mkpwd = decodefile(ConfigurationSettings.AppSettings("mypwd"))
        dbuser = ConfigurationSettings.AppSettings("userid")
        mreppath = ConfigurationSettings.AppSettings("reportpath")
        mprovider = ConfigurationSettings.AppSettings("provideroledb")
        mpostperiod = ConfigurationSettings.AppSettings("PostPeriod")
        mProdMktbarcode = ConfigurationSettings.AppSettings("ProdMktbarcode")
        mcostdbnam = ConfigurationSettings.AppSettings("mycostdbname")
        mautopnl = ConfigurationSettings.AppSettings("Autopnl")
        msameserver = ConfigurationSettings.AppSettings("SameServer")
        msamedb = ConfigurationSettings.AppSettings("SameDb")
        minvrepname = ConfigurationSettings.AppSettings("InvReportname")
        mdelrepname = ConfigurationSettings.AppSettings("DelReportname")
        mloryrepname = ConfigurationSettings.AppSettings("LorryReportname")
        mfwrepname = ConfigurationSettings.AppSettings("FwReportname")
        mrtgsrepname = ConfigurationSettings.AppSettings("Rtgscovername")

        msmtp = ConfigurationSettings.AppSettings("smtp")
        mport = ConfigurationSettings.AppSettings("port")
        mmailid = ConfigurationSettings.AppSettings("mailid")
        mmailpwd = ConfigurationSettings.AppSettings("mailpwd")
        mmailusername = ConfigurationSettings.AppSettings("mailusername")
        mccmailid = ConfigurationSettings.AppSettings("ccmailid")
        mrdoccode = ConfigurationSettings.AppSettings("rdoccode")
        mpdffile = ConfigurationSettings.AppSettings("pdffile")

        mcontprocfilt = ConfigurationSettings.AppSettings("contprocess")
        mos = ConfigurationSettings.AppSettings("OS")
        mlsprinter = ConfigurationSettings.AppSettings("LaserPrinter")
        mapppath = Trim(ConfigurationSettings.AppSettings("AppPath"))
        mapiurl = Trim(ConfigurationSettings.AppSettings("PrintAPI"))
        mlintmpfolder = Trim(ConfigurationSettings.AppSettings("tempfolder"))
        mxlfilepath = Trim(ConfigurationSettings.AppSettings("XLfilepath"))
        tscprinter1 = ConfigurationSettings.AppSettings("TSCPrinter_1")
        tscprinter2 = ConfigurationSettings.AppSettings("TSCPrinter_2")
        mprintapi = ConfigurationSettings.AppSettings("PrintAPI")

        'pnldate = ConfigurationSettings.AppSettings(" Nxtyrstart")

        'mProdMktbarcode
        dbnam4 = "prodbarcode"


        rmkserver = System.Configuration.ConfigurationSettings.AppSettings("rmyservername")
        rdbnam = ConfigurationSettings.AppSettings("rmydbname")
        rmkpwd = decodefile(ConfigurationSettings.AppSettings("rmypwd"))
        rdbuser = ConfigurationSettings.AppSettings("ruserid")
        'mreppath = ConfigurationSettings.AppSettings("reportpath")
        rmprovider = ConfigurationSettings.AppSettings("rprovideroledb")


        networklib = ConfigurationSettings.AppSettings("Networklibrary")
        linkserver = ConfigurationSettings.AppSettings("Linkedserver")
        nxtyr = ConfigurationSettings.AppSettings("Nxtyrstart")

        brlinkserver = ConfigurationSettings.AppSettings("cLinkedserver")
        brdbnam = ConfigurationSettings.AppSettings("condbname")






        'NyrNxtyrstart'

        'mm = InputBox("ENTER THE ACCOUNT YEAR (ex 2006-2007)", "Account Year", myr)
        'mkyr = mm
        'mkserver = "serverho"
        'mkserver = "selva-pc\sqlexpress"
        'mkserver = "192.168.0.32\sqlexpress"

        'mkserver = "192.168.0.5"
        'bb
        'mkserver = "192.167.0.6"
        'mkserver = "localhost\SQLEXPRESS"
        'mkserver = "admin\SQLEXPRESS"
        'mkserver = "edpmanager\sqlexpress"
        'mkserver = "oyez\sqlexpress"

        'mkserver = "localhost\SQLEXPRESS"
        'mkserver = "192.168.0.1"
        'mkserver = "192.168.0.32"
        'dbnam = "purchaseorder" & Mid(mm, 3, 2)
        ' MsgBox(dbnam)
        'dbnam = "purchaseorder"
        'dbnam = "ttt_1112"
        'dbnam = "rhl_training"
        'dbnam = "RHLLIVE"
        'dbnam = "BBLIVE"
        'dbnam = "BBTEST"
        'dbnam = "RHL160714"
        'dbnam = "rrlive"
        'dbnam = "eneslive"
        'dbnam = "ttt_1213sak"
        'dbnam = "ttt_1213vari"
        'dbnam = "rr_training"

        dbnam2 = "master"



        If ConfigurationSettings.AppSettings("internet") = "N" Then
            'windows auth


            If ConfigurationSettings.AppSettings("winauth") = "Y" Then
                'windows auth
                If Len(Trim(mprovider)) > 0 Then

                    constr = "Provider=" & Trim(mprovider) & ";Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";Integrated Security=True"
                    constr4 = "Provider=" & Trim(mprovider) & ";Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam4) & ";Integrated Security=True"

                Else
                    constr = "Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";Integrated Security=True"
                    constr4 = "Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam4) & ";Integrated Security=True"
                End If
            Else
                'sql auth
                If Len(Trim(mprovider)) > 0 Then
                    constr = "Provider=" & Trim(mprovider) & ";Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";Persist Security Info=true;User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)
                    constr4 = "Provider=" & Trim(mprovider) & ";Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam4) & ";Persist Security Info=true;User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)
                Else
                    constr = "Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";Persist Security Info=true;User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)
                    constr4 = "Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam4) & ";Persist Security Info=true;User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)
                End If
                'constr = "Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";Persist Security Info=true;User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)
            End If



            '    If ConfigurationSettings.AppSettings("rwinauth") = "Y" Then
            '        'windows auth
            '        If Len(Trim(rmprovider)) > 0 Then

            '            rconstr = "Provider=" & Trim(rmprovider) & ";Data Source=" & Trim(rmkserver) & ";Initial Catalog=" & Trim(rdbnam) & ";Integrated Security=True"

            '        Else
            '            rconstr = "Data Source=" & Trim(rmkserver) & ";Initial Catalog=" & Trim(rdbnam) & ";Integrated Security=True"
            '        End If
            '    Else
            '        'sql auth
            '        If Len(Trim(rmprovider)) > 0 Then
            '            rconstr = "Provider=" & Trim(rmprovider) & ";Data Source=" & Trim(rmkserver) & ";Initial Catalog=" & Trim(rdbnam) & ";Persist Security Info=true;User ID=" & Trim(rdbuser) & ";Password=" & Trim(rmkpwd)
            '        Else
            '            rconstr = "Data Source=" & Trim(rmkserver) & ";Initial Catalog=" & Trim(rdbnam) & ";Persist Security Info=true;User ID=" & Trim(rdbuser) & ";Password=" & Trim(rmkpwd)
            '        End If
            '        'constr = "Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";Persist Security Info=true;User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)
            '    End If

            'Else
            '    If Len(Trim(mprovider)) > 0 Then
            '        'constr = "Provider=" & Trim(mprovider) & ";Data Source=" & Trim(mkserver) & ";Network Library=" & Trim(networklib) & ";Initial Catalog=" & Trim(dbnam) & ";Persist Security Info=true;User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)
            '        'constr = "Driver=" & Trim(mprovider) & ";" & Trim(networklib) & ";Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)

            '        constr = "Provider=" & Trim(mprovider) & ";Data Source=" & Trim(mkserver) & ";" & Trim(networklib) & ";Initial Catalog=" & Trim(dbnam) & ";Persist Security Info=true;User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)
            '        'constr4 = "Provider=" & Trim(mprovider) & ";Data Source=" & Trim(mkserver) & ";" & Trim(networklib) & ";Initial Catalog=" & Trim(dbnam) & ";Persist Security Info=true;User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)

            '    Else
            '        constr = "Data Source=" & Trim(mkserver) & ";" & Trim(networklib) & ";Initial Catalog=" & Trim(dbnam) & ";Persist Security Info=true;User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)
            '    End If


            '    If ConfigurationSettings.AppSettings("rwinauth") = "Y" Then
            '        'windows auth
            '        If Len(Trim(rmprovider)) > 0 Then

            '            rconstr = "Provider=" & Trim(rmprovider) & ";Data Source=" & Trim(rmkserver) & ";Initial Catalog=" & Trim(rdbnam) & ";Integrated Security=True"

            '        Else
            '            rconstr = "Data Source=" & Trim(rmkserver) & ";Initial Catalog=" & Trim(rdbnam) & ";Integrated Security=True"
            '        End If
            '    Else
            '        'sql auth
            '        If Len(Trim(rmprovider)) > 0 Then
            '            rconstr = "Provider=" & Trim(rmprovider) & ";Data Source=" & Trim(rmkserver) & ";Initial Catalog=" & Trim(rdbnam) & ";Persist Security Info=true;User ID=" & Trim(rdbuser) & ";Password=" & Trim(rmkpwd)
            '        Else
            '            rconstr = "Data Source=" & Trim(rmkserver) & ";Initial Catalog=" & Trim(rdbnam) & ";Persist Security Info=true;User ID=" & Trim(rdbuser) & ";Password=" & Trim(rmkpwd)
            '        End If
            '        'constr = "Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";Persist Security Info=true;User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)
            '    End If


        End If

        If Len(Trim(mprovider)) > 0 Then
            constr = "Provider=" & Trim(mprovider) & ";Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";Persist Security Info=true;User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)
            constr4 = "Provider=" & Trim(mprovider) & ";Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam4) & ";Persist Security Info=true;User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)
        Else
            constr = "Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";Persist Security Info=true;User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)
            constr4 = "Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam4) & ";Persist Security Info=true;User ID=" & Trim(dbuser) & ";Password=" & Trim(mkpwd)
        End If

        '"Provider=sqloledb;Data Source=190.190.200.100,1433;Network Library=DBMSSOCN;Initial Catalog=myDataBase;User ID=myUsername;Password=myPassword;"





        'End If
        'home
        'constr="Data Source=selvapc;Initial Catalog=purchaseorder;Persist Security Info=True;User ID=sa;Password=sa536"
        ' Public con As New SqlClient.SqlConnection("Data Source=selvapc;Initial Catalog=purchaseorder;Persist Security Info=True;User ID=sa;Password=sa536")
        'rhl
        '  As New SqlClient.SqlConnection("Data Source=SERVERHO;Initial Catalog=purchaseorder;Integrated Security=True")
        'SHOWROOM
        'constr = "Data Source=RAMRAJHD;Initial Catalog=PURCHASEORDER;User ID=sa"
        'ANTS
        'constr = "Data Source=ANTSHO;Initial Catalog=purchaseorder;User ID=sa"
        'Public con As New SqlClient.SqlConnection("Data Source=ANTSHO;Initial Catalog=purchaseorder;User ID=sa")
        'axis
        'constr = "Data Source=axis;Initial Catalog=purchaseorder;Persist Security Info=false;User ID=sa;Password=sa536"
        'constr = "Data Source=server;Initial Catalog=purchaseorder;Persist Security Info=True;User ID=sa;Password=sa536"
        'rhl
        'constr = "Data Source=SERVERHO;Initial Catalog=purchaseorder;Integrated Security=True"
        'new**********
        'rhl
        'constr = "Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";Integrated Security=True"
        'ants
        'constr = "Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";User ID=sa"
        'home,axiz
        'sqlcon
        ''constr = "Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";Persist Security Info=true;User ID=sa;Password=sa536"
        'oledb con
        'constr = "Provider=SQLOLEDB;Data Source=" & Trim(mkserver) & ";Persist Security Info=True;Password=sa536;User ID=sa;Initial Catalog=" & Trim(dbnam)
        '*****
        'oledb auth
        'constr = "Provider=SQLOLEDB;Data Source=" & Trim(mkserver) & ";Persist Security Info=True;Password=iTTsA@536;User ID=sa;Initial Catalog=" & Trim(dbnam)

        'constr2 = "Provider=SQLOLEDB;Data Source=" & Trim(mkserver) & ";Persist Security Info=True;Password=sa536;User ID=sa;Initial Catalog=" & Trim(dbnam2)

        'windows auth
        'constr = "Provider=SQLOLEDB;Data Source=" & Trim(mkserver) & ";Integrated Security=SSPI;Initial Catalog=" & Trim(dbnam)
        'constr2 = "Provider=SQLOLEDB;Data Source=" & Trim(mkserver) & ";Integrated Security=SSPI;Initial Catalog=" & Trim(dbnam2)

        'acm-serverho
        'constr = "Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";User ID=sa"
        '*************
        'ACM-serverho
        'constr = "Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";User ID=sa"
        'constr = "Data Source=" & Trim(mkserver) & ";Initial Catalog=" & Trim(dbnam) & ";Persist Security Info=True;User ID=sa;Password=sa536"
        'ALAYA
        'constr = "Data Source=SERVERALAYA;Initial Catalog=PURCHASEORDER;Persist Security Info=True;User ID=SA"
        'constr = "Data Source=SERVERACM;Initial Catalog=PURCHASEORDER;User ID=sa"
        'acm-erode-rhl
        'constr = "Data Source=SERVERACM;Initial Catalog=PURCHASEORDER;User ID=sa"
        'con = New SqlClient.SqlConnection(constr)
        con = New SqlConnection(constr)
        '    con2 = New OleDb.OleDbConnection(constr)
        'con3 = New OleDb.OleDbConnection(rconstr)
        con4 = New SqlConnection(constr4)

        'If mautopnl = "Y" Then
        '    ' Frmpnlconsolidate.Show()
        'Else
        '    MDIFORM1.Show()
        'End If

        '**
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
        Application.Run(New MDIFORM1())




    End Sub
    Public Function chkchar(ByVal ctrl As Control, ByVal nchar As Integer) As Boolean
        If Len(Trim(ctrl.Text)) >= nchar Then
            chkchar = True
            ctrl.Text = Mid(ctrl.Text, 1, nchar)
            'ctrl.Selectionstart = 0
            'ctrl.Select
            'ctrl.Select(ctrl.Text.Length, 0)
            'ctrl.Select(ctrl.Text.Length, 0)
            Exit Function
        Else

        End If
    End Function
    Public Function CLEAR2(ByVal frm As Form, ByVal tc As TabControl)
        'Dim frm As Control = Me.FindControl("Form1")
        Dim ctl As Control
        For Each ctl In frm.Controls
            Dim tb As TextBox
            Dim ls As ListBox
            Dim cb As ComboBox
            If TypeOf ctl Is TextBox Then
                tb = CType(ctl, TextBox)
                tb.Text = String.Empty
            End If
            If TypeOf ctl Is ListBox Then
                ls = CType(ctl, ListBox)
                ls.Text = String.Empty
            End If
            If TypeOf ctl Is ComboBox Then
                cb = CType(ctl, ComboBox)
                cb.Text = String.Empty
            End If
            'For Each tb In tc.TabPages
            'For Each TB As Control In frm.TabControl1.Controls.Item(0).Controls()
            '    TB.Text = ""
            'Next
            ''tb.Clear()
            ''Next
            'For Each ls In tc.TabPages
            '    ls.Text = ""
            'Next

            'For Each cb In tc.TabPages
            '    cb.Text = ""
            'Next
        Next
        'For Each oTab As TabPage In TabControl1.TabPages
        '    For Each oControl As Control In oTab.Controls
        '        If TypeOf oControl Is TextBox Then
        '            ' If option strict is on we need to cast the control as a textbox.
        '            DirectCast(oControl, TextBox).Clear()
        '        End If

        '    Next
        'Next
        Return True
    End Function
    Public Function CLEAR(ByVal frm As Form)
        Dim I As Integer
        On Error Resume Next
        'For Each ctrl As Control In frm.Controls
        '    If TypeOf ctrl Is TextBox Then
        '        CType(ctrl, TextBox).Text = String.Empty

        '        ' Do whatever to the TextBox
        '    ElseIf TypeOf ctrl Is ListBox Then
        '        '.Controls(I).SelectedIndex = -1
        '        'ctrl.Text = ""
        '        CType(ctrl, ListBox).Text = String.Empty
        '    ElseIf TypeOf ctrl Is ComboBox Then
        '        '.Controls(I).SELECTEDINDEX = -1
        '        'ctrl.Text = ""
        '        CType(ctrl, ComboBox).Text = String.Empty
        '    ElseIf TypeOf ctrl Is MaskedTextBox Then
        '        '.Controls(I).Mask = ""
        '        '.Controls(I).Mask = "" - "" - """"
        '        'ctrl.Text = "__-__-____"
        '        '.Controls(I).Mask = "##-##-####"
        '        '.Controls(i).Text = "__-__-____"
        '    ElseIf TypeOf ctrl Is CheckBox Then
        '        ' .Controls(I).CHECKED = False
        '        'ctrl.CheckState.Checked = CheckState.Unchecked
        '    ElseIf TypeOf ctrl Is RadioButton Then
        '        '.Controls(I).CHECKED = False
        '        'ElseIf TypeOf .Controls(I) Is Label Then
        '        '    .Controls(I).Caption = ""
        '    End If
        ' If the control has children,
        ' recursively call this function
        'If ctrl.HasChildren Then
        'ProcessControls(ctrl)
        'End If
        'Next
        'tab control
        'For Each ctrl As Control In TabControl.Controls
        '    If TypeOf ctrl Is TextBox Then
        '        CType(ctrl, TextBox).Text = String.Empty
        '        ' Do whatever to the TextBox
        '    ElseIf TypeOf ctrl Is ListBox Then
        '        '.Controls(I).SelectedIndex = -1
        '        'ctrl.Text = ""
        '        CType(ctrl, ListBox).Text = String.Empty
        '    ElseIf TypeOf ctrl Is ComboBox Then
        '        '.Controls(I).SELECTEDINDEX = -1
        '        'ctrl.Text = ""
        '        CType(ctrl, ComboBox).Text = String.Empty
        '    ElseIf TypeOf ctrl Is MaskedTextBox Then
        '        '.Controls(I).Mask = ""
        '        '.Controls(I).Mask = "" - "" - """"
        '        'ctrl.Text = "__-__-____"
        '        '.Controls(I).Mask = "##-##-####"
        '        '.Controls(i).Text = "__-__-____"
        '    ElseIf TypeOf ctrl Is CheckBox Then
        '        ' .Controls(I).CHECKED = False
        '        'ctrl.CheckState.Checked = CheckState.Unchecked

        '    ElseIf TypeOf ctrl Is RadioButton Then
        '        '.Controls(I).CHECKED = False
        '        'ElseIf TypeOf .Controls(I) Is Label Then
        '        '    .Controls(I).Caption = ""
        '    End If
        '    ' If the control has children,
        '    ' recursively call this function
        '    'If ctrl.HasChildren Then
        '    'ProcessControls(ctrl)
        '    'End If
        'Next
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

    Public Function Decode(ByVal Password As String) As String
        'Dim I As Integer
        Dim TMP As Long
        tmpp = ""
        For I = 1 To Len(Password)
            TMP = Asc(Mid(Password, I, 1))
            TMP = TMP - I
            tmpp = Trim(tmpp) & Chr(TMP)
            'Decode = Decode & Chr(TMP)
        Next I
        Decode = tmpp
        Return Decode
    End Function
    Public Function Encript(ByVal Password As String) As String
        ' Dim I As Integer
        'Dim tmpp As String
        Dim TMP As Long
        tmpp = ""
        For I = 1 To Len(Password)
            TMP = Asc(Mid(Password, I, 1))
            TMP = TMP + I
            tmpp = Trim(tmpp) + Chr(TMP)
            'Encript = Encript & Chr(TMP)
        Next I
        Encript = tmpp
        Return Encript
    End Function
    Public Function dlin(ByVal CharC As String, ByVal length As Integer)
        ' Dim I As Integer
        'Dim str As String
        strp = ""
        For I = 1 To length
            strp = Trim(strp) + CharC
        Next
        dlin = strp
        Return dlin
    End Function
    Public Function word(ByVal amt As String) As String
        fword = " "
        sword = " "
        wrdamt = " "
        strp = ""
        If Val(amt) = 0 Then
            wrdamt = "Rs.Zero Only"
        End If
        If Val(amt) < 0 Then
            amt = "-" + LTrim(amt)
        End If
        NTR = InStr(1, amt, ".")
        If NTR <= 0 Then
            NTR = Len(RTrim(amt)) + 1
        End If
        stramt = Mid$(amt, 1, NTR - 1)
        stramts = LTrim(Mid$(stramt, 1, NTR - 1))
        If Len(stramts) < 9 Then
            MN = 9 - Len(stramts)
            For I = 1 To MN
                stramts = "0" + stramts
                'tramts=padl(stramts,9,'0')
            Next I
        End If
        stramts1 = Mid$(amt, NTR + 1, 2)
        If stramts1 = "00" Or stramts1 = " " Then
            mpaise = " "
        Else
            FIRST = Mid$(stramts1, 1, 1)
            SECOND = Mid$(stramts1, 2, 1)
            Call CONVERTT()
            mpaise = fword + sword
        End If
        If stramts = "000000000" Then
            MRS = "Rs. Zero"
            word = "Paise " + mpaise + " Only "
        Else
            FIRST = Mid$(stramts, 1, 1)
            SECOND = Mid$(stramts, 2, 1)
            '*************CRORE CONVERSION*****************
            If FIRST + SECOND = "00" Then
                MCRORES = " "
            Else
                Call CONVERTT()
                MCRORES = fword + sword + "Crore "
            End If
            FIRST = Mid$(stramts, 3, 1)
            SECOND = Mid$(stramts, 4, 1)
            '************LACS CONVERSION*******************
            If FIRST + SECOND = "00" Then
                MLACS = " "
            Else
                Call CONVERTT()
                MLACS = fword + sword + "Lac "
            End If
            '*************THOUSAND CONVERSION*************
            FIRST = Mid$(stramts, 5, 1)
            SECOND = Mid$(stramts, 6, 1)
            If FIRST + SECOND = "00" Then
                MTHOUS = " "
            Else
                Call CONVERTT()
                'convertt()
                MTHOUS = fword + sword + "Thousand "
            End If
            '**************HUNDRED CONVERSION***************
            FIRST = "0"
            SECOND = Mid$(stramts, 7, 1)
            If FIRST + SECOND = "00" Then
                MHUND = " "
            Else
                Call CONVERTT()
                'convertt()
                MHUND = fword + sword + "Hundred "
            End If
            '***************SINGLE DIGIT CONVERSION*********
            FIRST = Mid$(stramts, 8, 1)
            SECOND = Mid$(stramts, 9, 1)
            If FIRST + SECOND = "00" Then
                MTENS = " "
            Else
                Call CONVERTT()
                'convertt()
                MTENS = fword + sword
            End If
            MRS = MCRORES + MLACS + MTHOUS + MHUND + MTENS
            If Len(LTrim(mpaise)) = 0 Then
                strp = MRS + "Only "
            ElseIf Len(Trim(mpaise)) > 0 Then
                strp = MRS + " And" + " Paise " + mpaise + " Only "
            End If
        End If
        word = strp
        Return word
    End Function
    Private Function CONVERTT()
        Select Case SECOND
            Case "0"
                sword = " "
            Case "1"
                sword = "One "
            Case "2"
                sword = "Two "
            Case "3"
                sword = "Three "
            Case "4"
                sword = "Four "
            Case "5"
                sword = "Five "
            Case "6"
                sword = "Six "
            Case "7"
                sword = "Seven "
            Case "8"
                sword = "Eight "
            Case "9"
                sword = "Nine "
        End Select
        Select Case FIRST
            Case "0"
                fword = " "
            Case "1"
                fword = " "
                Select Case SECOND
                    Case "0"
                        sword = "Ten "
                    Case "1"
                        sword = "Eleven "
                    Case "2"
                        sword = "Tweleve "
                    Case "3"
                        sword = "Thirteen "
                    Case "4"
                        sword = "Fourteen "
                    Case "5"
                        sword = "Fifteen "
                    Case "6"
                        sword = "Sixteen "
                    Case "7"
                        sword = "Seventeen "
                    Case "8"
                        sword = "Eighteen "
                    Case "9"
                        sword = "Nineteen "
                End Select
            Case "2"
                fword = "Twenty "
            Case "3"
                fword = "Thirty "
            Case "4"
                fword = "Fourty "
            Case "5"
                fword = "Fifty "
            Case "6"
                fword = "Sixty "
            Case "7"
                fword = "Seventy "
            Case "8"
                fword = "Eighty "
            Case "9"
                fword = "Ninety "
        End Select
        Return True
    End Function
    Public Function KeyAscii(ByVal UserKeyArgument As KeyPressEventArgs) As Short
        KeyAscii = Asc(UserKeyArgument.KeyChar)
    End Function
    Public Sub cnum(ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyPressEvent)
        If InStr(1, "0123456789.-", Chr(e.keyAscii)) = 0 Then
            If e.keyAscii <> 8 And e.keyAscii <> 13 Then
                e.keyAscii = 0
            End If
        End If
    End Sub
    Private Function NumbersOnly(ByVal pstrChar As Char, ByVal oTextBox As TextBox) As Boolean
        'example keypress:= e.Handled = NumbersOnly(e.KeyChar, TextBox1)
        'validate the entry for a textbox limiting it to only numeric values and the decimal point
        If (Convert.ToString(pstrChar) = "." And InStr(oTextBox.Text, ".")) Then Return True 'accept only one instance of the decimal point
        If Convert.ToString(pstrChar) <> "." And pstrChar <> vbBack Then
            Return IIf(IsNumeric(pstrChar), False, True) 'check if numeric is returned
        End If
        Return False 'for backspace
    End Function
    Public Sub cupper(ByVal ctrl As Object) 'n as keyascii
        'Asc(e.KeyChar) = UCase(Chr(Asc(e.KeyChar)))
        'e.keyAscii = Asc(UCase(Chr(e.keyAscii)))

        ctrl.CharacterCasing = CharacterCasing.Upper
        '
    End Sub

    Public Function BSEARCH(ByVal ls As ListBox, ByVal KEYFIELD As String) As Integer
        Dim LOWER As Integer, UPPER As Integer, MIDDLE As Integer
        Dim MIDDLEITEM As String
        LOWER = 0
        UPPER = ls.Items.Count - 1
        While 1
            MIDDLE = Fix((LOWER + UPPER) / 2)
            MIDDLEITEM = ls.Items.Item(MIDDLE)
            If UPPER < LOWER Then
                BSEARCH = -1
                Exit Function
            End If
            If StrComp(KEYFIELD, Left(MIDDLEITEM, Len(KEYFIELD))) > 0 Then
                LOWER = MIDDLE + 1
            Else
                If StrComp(KEYFIELD, Left(MIDDLEITEM, Len(KEYFIELD))) < 0 Then
                    UPPER = MIDDLE - 1
                Else
                    BSEARCH = MIDDLE
                    Exit Function
                End If
            End If
        End While
        Return BSEARCH
    End Function
    Public Function txtchang(ByVal CTRL As TextBox, ByVal ctrl1 As ListBox)
        POSITION = BSEARCH(ctrl1, Trim$(CTRL.Text))
        If POSITION >= 0 Then
            ctrl1.SelectedIndex = POSITION
        Else
            ctrl1.SelectedIndex = -1
        End If
        Return True
    End Function
    'Public Function txtchange(ByVal ls As ListBox, ByVal text As TextBox)
    '    Dim index As Integer
    '    On Error Resume Next
    '    'Index = FindFirstMatch(ls, text.text, -1, optSearchType(0).value)
    '    index = FindFirstMatch(ls, text.Text, -1)
    '    ls.ListIndex = index
    '    If index >= 0 Then ls.Selected(index) = True
    '    'End If
    'End Function
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
    Public Sub FLXEMPTY(ByVal CTRL2 As AxMSFlexGrid)
        CTRL2.Row = CTRL2.Rows - 1
        If Len(Trim(CTRL2.get_TextMatrix(CTRL2.Row, 1))) <= 0 Then
            If CTRL2.Row > 1 Then
                CTRL2.RemoveItem(CTRL2.Row)
            End If
        End If
    End Sub
    'Public Sub FLXEMPTY2(ByVal CTRL2 As AxUFLX.AxUSERFLXNEW)
    '    CTRL2.Row = CTRL2.Rows - 1
    '    If Len(Trim(CTRL2.get_TextMatrix(CTRL2.Row, 1))) <= 0 Then
    '        If CTRL2.Row > 1 Then
    '            CTRL2.RemoveItem(CTRL2.Row)
    '        End If
    '    End If
    'End Sub

    'Public Sub EXCELEXPORT(ByVal CTRL As AxMSFlexGrid)
    '    Dim SPREADSHEET1 As Microsoft.Office.Interop.Excel.Application 'To open Excel
    '    Dim xlWrkB As Microsoft.Office.Interop.Excel.Workbook
    '    Dim xlWrkS As Microsoft.Office.Interop.Excel.Worksheet
    '    Dim misValue As Object = System.Reflection.Missing.Value
    '    SPREADSHEET1 = New Microsoft.Office.Interop.Excel.ApplicationClass
    '    xlWrkB = SPREADSHEET1.Workbooks.Add(misValue)
    '    xlWrkS = xlWrkB.Sheets("sheet1")
    '    'SPREADSHEET1 = CreateObject("Excel.application") 'Creates an object
    '    'SPREADSHEET1.Visible = True ' So you can see Excel
    '    SPREADSHEET1.Workbooks.Add()
    '    'Dim nfile As Integer
    '    For I = 0 To CTRL.Rows - 1
    '        For J = 0 To CTRL.Cols - 1
    '            If IsDate(CTRL.get_TextMatrix(I, J)) = True Then
    '                If Trim(CTRL.get_TextMatrix(I, J)) <> "0" And Trim(CTRL.get_TextMatrix(I, J)) <> "0.00" Then
    '                    'SPREADSHEET1.Cells(I + 1, j + 1) = "'" & CTRL.TextMatrix(I, j)
    '                    If InStr(CTRL.get_TextMatrix(I, J), "-") > 0 Or InStr(CTRL.get_TextMatrix(I, J), "-/") > 0 Then
    '                        SPREADSHEET1.Cells(I + 1, J + 1) = "'" & CTRL.get_TextMatrix(I, J)
    '                    Else
    '                        SPREADSHEET1.Cells(I + 1, J + 1) = CTRL.get_TextMatrix(I, J)
    '                    End If
    '                Else
    '                    SPREADSHEET1.Cells(I + 1, J + 1) = CTRL.get_TextMatrix(I, J)
    '                End If
    '            Else
    '                SPREADSHEET1.Cells(I + 1, J + 1) = CTRL.get_TextMatrix(I, J)
    '            End If
    '            'SPREADSHEET1.Cells(I + 1, j + 1) = CTRL.TextMatrix(I, j)
    '        Next
    '        'ApExcel.cells(1, 1).Formula = "HELLO"   'Add Text to a Cell
    '    Next
    '    'MM = CTRL.Cols - 1
    '    SPREADSHEET1.Visible = True
    '    'spreadsheet1.ActiveWorkbook.SaveAs App.Path & "\OTSTOCKN.XLS"
    '    'ApExcel.cells(1, 1).Formula = "HELLO"   'Add Text to a Cell
    '    xlWrkS.SaveAs("C:\vbexcel.xls")
    '    ' xlWrkB.Close()
    '    SPREADSHEET1.Application.Quit()
    '    ' releaseObject(SPREADSHEET1)
    '    ' releaseObject(xlWorkBook)
    '    ' releaseObject(xlWorkSheet)
    '    'SPREADSHEET1.Application.Quit()
    '    SPREADSHEET1 = Nothing
    'End Sub

    'Public Sub EXCELEXPORTnew(ByVal CTRL As AxMSFlexGrid, ByVal mcompname2 As String, ByVal mtitle As String, ByVal mtitle2 As String)
    '    Dim lk As Int32
    '    Dim SPREADSHEET1 As Microsoft.Office.Interop.Excel.Application 'To open Excel
    '    Dim xlWrkB As Microsoft.Office.Interop.Excel.Workbook
    '    Dim xlWrkS As Microsoft.Office.Interop.Excel.Worksheet
    '    Dim misValue As Object = System.Reflection.Missing.Value
    '    SPREADSHEET1 = New Microsoft.Office.Interop.Excel.ApplicationClass
    '    xlWrkB = SPREADSHEET1.Workbooks.Add(misValue)
    '    xlWrkS = xlWrkB.Sheets("sheet1")
    '    'SPREADSHEET1 = CreateObject("Excel.application") 'Creates an object
    '    'SPREADSHEET1.Visible = True ' So you can see Excel
    '    lk = 1


    '    SPREADSHEET1.Workbooks.Add()
    '    If Len(Trim(mcompname2)) > 0 Then
    '        'spreadsheet1.cells(lk,2)
    '        SPREADSHEET1.Cells(lk, 2).Font.Bold = True
    '        SPREADSHEET1.Cells(lk, 2).Font.Size = 14
    '        SPREADSHEET1.Cells(lk, 2) = mcompname2
    '        lk = lk + 1
    '    End If

    '    If Len(Trim(mtitle)) > 0 Then
    '        SPREADSHEET1.Cells(lk, 2).Font.Bold = True
    '        SPREADSHEET1.Cells(lk, 2).Font.Size = 12
    '        SPREADSHEET1.Cells(lk, 2) = mtitle
    '        lk = lk + 1
    '    End If

    '    If Len(Trim(mtitle2)) > 0 Then
    '        SPREADSHEET1.Cells(lk, 2).Font.Bold = True
    '        SPREADSHEET1.Cells(lk, 2).Font.Size = 12
    '        SPREADSHEET1.Cells(lk, 2) = mtitle2
    '        lk = lk + 1
    '    End If

    '    'Dim nfile As Integer
    '    For I = 0 To CTRL.Rows - 1
    '        For J = 0 To CTRL.Cols - 1
    '            If IsDate(CTRL.get_TextMatrix(I, J)) = True Then
    '                If Trim(CTRL.get_TextMatrix(I, J)) <> "0" And Trim(CTRL.get_TextMatrix(I, J)) <> "0.00" Then
    '                    'SPREADSHEET1.Cells(I + 1, j + 1) = "'" & CTRL.TextMatrix(I, j)
    '                    If InStr(CTRL.get_TextMatrix(I, J), "-") > 0 Or InStr(CTRL.get_TextMatrix(I, J), "-/") > 0 Then
    '                        SPREADSHEET1.Cells(lk, J + 1) = "'" & CTRL.get_TextMatrix(I, J)
    '                    Else
    '                        SPREADSHEET1.Cells(lk, J + 1) = CTRL.get_TextMatrix(I, J)
    '                    End If
    '                Else
    '                    SPREADSHEET1.Cells(lk, J + 1) = CTRL.get_TextMatrix(I, J)
    '                End If
    '            Else
    '                SPREADSHEET1.Cells(lk, J + 1) = CTRL.get_TextMatrix(I, J)
    '            End If
    '            'SPREADSHEET1.Cells(I + 1, j + 1) = CTRL.TextMatrix(I, j)
    '        Next
    '        lk = lk + 1
    '        'ApExcel.cells(1, 1).Formula = "HELLO"   'Add Text to a Cell
    '    Next
    '    'MM = CTRL.Cols - 1
    '    SPREADSHEET1.Visible = True
    '    'spreadsheet1.ActiveWorkbook.SaveAs App.Path & "\OTSTOCKN.XLS"
    '    'ApExcel.cells(1, 1).Formula = "HELLO"   'Add Text to a Cell
    '    ''xlWrkS.SaveAs("C:\vbexcel.xls")
    '    ' xlWrkB.Close()
    '    SPREADSHEET1.Application.Quit()
    '    ' releaseObject(SPREADSHEET1)
    '    ' releaseObject(xlWorkBook)
    '    ' releaseObject(xlWorkSheet)
    '    'SPREADSHEET1.Application.Quit()
    '    SPREADSHEET1 = Nothing
    'End Sub
    'Public Sub kingEXCELEXPORTnew(ByVal CTRL As AxMSFlexGrid, ByVal mcompname2 As String, ByVal mtitle As String, ByVal mtitle2 As String)
    '    Dim lk As Int32
    '    'Dim spreadsheet1 As Object
    '    Dim spreadsheet1 As ET.Application 'To open Excel
    '    Dim xlWrkB As ET.workbook
    '    Dim xlWrkS As ET.Worksheet

    '    Dim misValue As Object = System.Reflection.Missing.Value
    '    spreadsheet1 = New ET.Application

    '    xlWrkB = spreadsheet1.Workbooks.Add(misValue)
    '    xlWrkS = xlWrkB.Sheets("sheet1")
    '    'SPREADSHEET1 = CreateObject("Excel.application") 'Creates an object
    '    'SPREADSHEET1.Visible = True ' So you can see Excel
    '    lk = 1


    '    spreadsheet1.Workbooks.Add()

    '    If Len(Trim(mcompname2)) > 0 Then
    '        'spreadsheet1.cells(lk,2)
    '        SPREADSHEET1.Cells(lk, 2).Font.Bold = True
    '        SPREADSHEET1.Cells(lk, 2).Font.Size = 14
    '        SPREADSHEET1.Cells(lk, 2) = mcompname2
    '        lk = lk + 1
    '    End If

    '    If Len(Trim(mtitle)) > 0 Then
    '        SPREADSHEET1.Cells(lk, 2).Font.Bold = True
    '        SPREADSHEET1.Cells(lk, 2).Font.Size = 12
    '        SPREADSHEET1.Cells(lk, 2) = mtitle
    '        lk = lk + 1
    '    End If

    '    If Len(Trim(mtitle2)) > 0 Then
    '        SPREADSHEET1.Cells(lk, 2).Font.Bold = True
    '        SPREADSHEET1.Cells(lk, 2).Font.Size = 12
    '        SPREADSHEET1.Cells(lk, 2) = mtitle2
    '        lk = lk + 1
    '    End If

    '    'Dim nfile As Integer
    '    For I = 0 To CTRL.Rows - 1
    '        For J = 0 To CTRL.Cols - 1
    '            If IsDate(CTRL.get_TextMatrix(I, J)) = True Then
    '                If Trim(CTRL.get_TextMatrix(I, J)) <> "0" And Trim(CTRL.get_TextMatrix(I, J)) <> "0.00" Then
    '                    'SPREADSHEET1.Cells(I + 1, j + 1) = "'" & CTRL.TextMatrix(I, j)
    '                    If InStr(CTRL.get_TextMatrix(I, J), "-") > 0 Or InStr(CTRL.get_TextMatrix(I, J), "-/") > 0 Then
    '                        SPREADSHEET1.Cells(lk, J + 1) = "'" & CTRL.get_TextMatrix(I, J)
    '                    Else
    '                        SPREADSHEET1.Cells(lk, J + 1) = CTRL.get_TextMatrix(I, J)
    '                    End If
    '                Else
    '                    SPREADSHEET1.Cells(lk, J + 1) = CTRL.get_TextMatrix(I, J)
    '                End If
    '            Else
    '                SPREADSHEET1.Cells(lk, J + 1) = CTRL.get_TextMatrix(I, J)
    '            End If
    '            'SPREADSHEET1.Cells(I + 1, j + 1) = CTRL.TextMatrix(I, j)
    '        Next
    '        lk = lk + 1
    '        'ApExcel.cells(1, 1).Formula = "HELLO"   'Add Text to a Cell
    '    Next
    '    'MM = CTRL.Cols - 1
    '    spreadsheet1.Visible = True

    '    'spreadsheet1.ActiveWorkbook.SaveAs App.Path & "\OTSTOCKN.XLS"
    '    'ApExcel.cells(1, 1).Formula = "HELLO"   'Add Text to a Cell
    '    ''xlWrkS.SaveAs("C:\vbexcel.xls")
    '    ' xlWrkB.Close()
    '    spreadsheet1.Application.Quit()

    '    ' releaseObject(SPREADSHEET1)
    '    ' releaseObject(xlWorkBook)
    '    ' releaseObject(xlWorkSheet)
    '    'SPREADSHEET1.Application.Quit()
    '    spreadsheet1 = Nothing

    'End Sub
    'Public Sub KINGEXCELEXPORT(ByVal CTRL As AxMSFlexGrid)
    '    Dim SPREADSHEET1 As ET.Application 'To open Excel
    '    Dim xlWrkB As ET.workbook
    '    Dim xlWrkS As ET.Worksheet
    '    Dim misValue As Object = System.Reflection.Missing.Value
    '    SPREADSHEET1 = New ET.ApplicationClass
    '    xlWrkB = SPREADSHEET1.Workbooks.Add(misValue)
    '    xlWrkS = xlWrkB.Sheets("sheet1")
    '    'SPREADSHEET1 = CreateObject("Excel.application") 'Creates an object
    '    'SPREADSHEET1.Visible = True ' So you can see Excel
    '    SPREADSHEET1.Workbooks.Add()
    '    'Dim nfile As Integer
    '    For I = 0 To CTRL.Rows - 1
    '        For J = 0 To CTRL.Cols - 1
    '            If IsDate(CTRL.get_TextMatrix(I, J)) = True Then
    '                If Trim(CTRL.get_TextMatrix(I, J)) <> "0" And Trim(CTRL.get_TextMatrix(I, J)) <> "0.00" Then
    '                    'SPREADSHEET1.Cells(I + 1, j + 1) = "'" & CTRL.TextMatrix(I, j)
    '                    If InStr(CTRL.get_TextMatrix(I, J), "-") > 0 Or InStr(CTRL.get_TextMatrix(I, J), "-/") > 0 Then
    '                        SPREADSHEET1.Cells(I + 1, J + 1) = "'" & CTRL.get_TextMatrix(I, J)
    '                    Else
    '                        SPREADSHEET1.Cells(I + 1, J + 1) = CTRL.get_TextMatrix(I, J)
    '                    End If
    '                Else
    '                    SPREADSHEET1.Cells(I + 1, J + 1) = CTRL.get_TextMatrix(I, J)
    '                End If
    '            Else
    '                SPREADSHEET1.Cells(I + 1, J + 1) = CTRL.get_TextMatrix(I, J)
    '            End If
    '            'SPREADSHEET1.Cells(I + 1, j + 1) = CTRL.TextMatrix(I, j)
    '        Next
    '        'ApExcel.cells(1, 1).Formula = "HELLO"   'Add Text to a Cell
    '    Next
    '    'MM = CTRL.Cols - 1
    '    SPREADSHEET1.Visible = True
    '    'spreadsheet1.ActiveWorkbook.SaveAs App.Path & "\OTSTOCKN.XLS"
    '    'ApExcel.cells(1, 1).Formula = "HELLO"   'Add Text to a Cell
    '    xlWrkS.SaveAs("C:\vbexcel.xls")
    '    ' xlWrkB.Close()
    '    SPREADSHEET1.Application.Quit()
    '    ' releaseObject(SPREADSHEET1)
    '    ' releaseObject(xlWorkBook)
    '    ' releaseObject(xlWorkSheet)
    '    'SPREADSHEET1.Application.Quit()
    '    SPREADSHEET1 = Nothing
    'End Sub

    'Public SUB DBEXCELEXPORT(MTABLE As String, MORDER As String, Optional MWHERE As String)
    '    Dim SPREADSHEET1 As Object 'To open Excel
    '    Dim I As Long
    '    SPREADSHEET1 = CreateObject("Excel.application") 'Creates an object
    '    'SPREADSHEET1.Visible = True ' So you can see Excel
    '    SPREADSHEET1.Workbooks.Add()
    '    'Dim nfile As Integer
    '    If rs.State = 1 Then rs.Close()
    '    If Len(Trim(MWHERE)) > 0 Then
    '        Sql = "SELECT * FROM " & MTABLE & " WHERE " & MWHERE & ">0  ORDER BY " & MORDER
    '    Else
    '        Sql = "SELECT * FROM " & MTABLE & " ORDER BY " & MORDER
    '    End If
    '    rs.Open(Sql, con, adOpenDynamic, adLockOptimistic)
    '    If rs.RecordCount > 0 Then
    '        I = 0
    '        Do While Not rs.EOF

    '            For J = 0 To rs.Fields.Count - 1
    '                SPREADSHEET1.Cells(I + 1, J + 1) = rs.Fields(J)
    '            Next
    '            I = I + 1
    '            'ApExcel.cells(1, 1).Formula = "HELLO"   'Add Text to a Cell
    '            rs.MoveNext()
    '        Loop
    '    End If
    '    'MM = CTRL.Cols - 1

    '    SPREADSHEET1.Visible = True
    '    'spreadsheet1.ActiveWorkbook.SaveAs App.Path & "\OTSTOCKN.XLS"
    '    'ApExcel.cells(1, 1).Formula = "HELLO"   'Add Text to a Cell
    '    SPREADSHEET1.Application.Quit()
    '    SPREADSHEET1 = Nothing
    '    If rs.State = 1 Then rs.Close()
    'End Sub
    Public Sub taga(ByVal ct As Object, ByVal w As Long, ByVal h As Long)
        Dim i, k As Integer
        Dim ctl As Control
        For Each ctl In ct.Controls
            If ctl.Name = "" Then Exit For
            k = c.GetUpperBound(1)
            ReDim Preserve c(6, k + 1)
            c(0, k + 1) = ctl.Name
            c(1, k + 1) = ctl.Left
            c(2, k + 1) = ctl.Top
            c(3, k + 1) = ctl.Width
            c(4, k + 1) = ctl.Height
            c(5, k + 1) = ctl.Font.Size
            c(6, k + 1) = ctl.Font.Style
            i = ctl.Controls.Count
            If i > 0 Then
                taga(ctl, ctl.Width, ctl.Height)
            End If
        Next ctl
    End Sub
    Public Sub mudar(ByVal cm As Object, ByVal s As String, ByVal n As Integer, ByVal x As Double, ByVal y As Double)
        Dim i, k As Integer
        Dim ct As Control
        For Each ct In cm.controls
            If ct.Name = s Then
                ct.Left = c(1, n) * x
                ct.Top = c(2, n) * y
                ct.Width = c(3, n) * x
                ct.Height = c(4, n) * y
                If x < y Then
                    ct.Font = New System.Drawing.Font(ct.Font.Name, c(5, n) * x)
                Else
                    ct.Font = New System.Drawing.Font(ct.Font.Name, c(5, n) * y)
                End If
                ct.Font = New System.Drawing.Font(ct.Font, c(6, n))
                Exit For
            Else
                mudar(ct, s, n, x, y)
            End If
        Next
    End Sub
    Public Function DriveSerialNumber(ByVal Drive As String) As String
        Dim fso = CreateObject("Scripting.FileSystemObject")
        Dim drv
        Dim driveLetter, driveSerial As String
        'Dim driveGUID As Guid
        For Each drv In fso.Drives
            Try
                driveLetter = drv.DriveLetter
                driveSerial = drv.SerialNumber
                If Drive.ToUpper.Trim = driveLetter.ToUpper.Trim Then
                    DriveSerialNumber = driveSerial
                    Exit Function
                End If
            Catch
            End Try
        Next drv
        DriveSerialNumber = ""
    End Function
    Public Function findid() As String
        '(@Param1) or (@Param1,@Parm2,...)
        Dim strsql As String = "SELECT newid() as tid"
        'Dim strSQL = "SELECT newid() as tid" & "(@Param1)"
        Dim cmdfid As SqlCommand = New SqlCommand(strsql, con2)
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


    Public Function dataexists(ByVal sqlstr As String) As Boolean
        Dim mtru As Boolean
        Dim dt As DataTable = getDataTable(sqlstr)
        If dt.Rows.Count > 0 Then
            mtru = True
        Else
            mtru = False
        End If
        Return mtru
    End Function

    Public Function getid(ByVal mtab_name As String, ByVal mfieldname As String, ByVal mfindfield As String, ByVal mfindname As String) As String
        '(@Param1) or (@Param1,@Parm2,...)
        Dim strsql As String = "select " & mfieldname & " from " & mtab_name & " where " & mfindfield & "='" & mfindname & "'"
        'Dim strsql As String = "SELECT newid() as tid"
        'Dim strSQL = "SELECT newid() as tid" & "(@Param1)"
        Dim cmd As SqlCommand = New SqlCommand(strsql, con2)
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
    Public Function getsecurity(ByVal mtab_name As String, ByVal mfieldname As String, ByVal mfindfield As String, ByVal mfindname As String, ByVal mformname As String) As String
        '(@Param1) or (@Param1,@Parm2,...)
        Dim strsql As String = "select *  from " & mtab_name & " where " & mfindfield & "='" & mfindname & "' and formname='" & mformname & "'"
        'Dim strsql As String = "select *  from " & mtab_name & " where " & mfindfield & "='" & mfindname & "'"
        'Dim strsql As String = "SELECT newid() as tid"
        'Dim strSQL = "SELECT newid() as tid" & "(@Param1)"
        Dim cmd As SqlCommand = New SqlCommand(strsql, con)
        'cmd.Parameters.Add(New SqlParameter("@Param1", _
        'SqlDbType.Text)).Value = "Hello"
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim DR As SqlDataReader
        DR = cmd.ExecuteReader
        If DR.HasRows = True Then
            While DR.Read
                muser = DR.Item("Loginname")
                musetyp = DR.Item("usertype")
            End While
        End If
        Dim val As String = Replace(cmd.ExecuteScalar().ToString, "-", "").ToUpper
        'con.Close()
        cmd.Dispose()
        DR.Close()
        getsecurity = val
        'MsgBox("Value is: " & val)
    End Function
    Public Sub ClearAllCtrls(ByRef container As Panel, Optional ByVal Recurse As Boolean = True)
        'Clear all of the controls within the container object
        'If "Recurse" is true, then also clear controls within any sub-containers
        Dim ctrl As Control
        For Each ctrl In container.Controls
            If (ctrl.GetType() Is GetType(TextBox)) Then
                Dim txt As TextBox = CType(ctrl, TextBox)
                txt.Text = ""
            End If
            If (ctrl.GetType() Is GetType(CheckBox)) Then
                Dim chkbx As CheckBox = CType(ctrl, CheckBox)
                chkbx.Checked = False
            End If
            If (ctrl.GetType() Is GetType(ComboBox)) Then
                Dim cbobx As ComboBox = CType(ctrl, ComboBox)
                cbobx.SelectedIndex = -1
            End If
            If (ctrl.GetType() Is GetType(DateTimePicker)) Then
                Dim dtp As DateTimePicker = CType(ctrl, DateTimePicker)
                dtp.Value = Now()
            End If
            If Recurse Then
                If (ctrl.GetType() Is GetType(Panel)) Then
                    Dim pnl As Panel = CType(ctrl, Panel)
                    ClearAllCtrls(pnl, Recurse)
                    'ClearAllControls(pnl, Recurse)
                End If
                'If ctrl.GetType() Is GetType(GroupBox) Then
                '    Dim grbx As GroupBox = CType(ctrl, GroupBox)
                '    ClearAllControls(grbx, Recurse)
                'End If
            End If
        Next
    End Sub
    ' Public Sub ResetFields(ByRef Frm As Form)
    '     Dim ObjLvl1 As Object, ObjLvl2 As Object
    '     Dim tbx As TextBox
    '     Dim cmb As ComboBox
    '     Dim pnl As Panel
    '     Dim gpbx As GroupBox
    '     ' Run through each object on the form 
    '     For Each ObjLvl1 In Frm.Controls
    '         ' If the Level 1 Object is a Panel then
    '         ' run through each object in that panel
    '         If TypeOf ObjLvl1 Is Panel Then
    '             'Convert ObjLvl1 to a panel
    '             pnl = DirectCast(ObjLvl1, Panel)
    '             ' Run through all objects on the panel
    '             For Each ObjLvl2 In pnl.Controls
    '                 ' Level 2 object is a TextBox...
    '                 If TypeOf ObjLvl2 Is TextBox Then
    '                     ' Convert objlvl2 to a TextBox
    '                     tbx = DirectCast(ObjLvl2, TextBox)
    '                     ' Set the text to "" tbx.Text = ""
    '                     ' I also took this opportunity,
    '                     ' since I was touching all of
    '                     ' the objects, to add a handler
    '                     ' to check for item change.
    '                     ' This allows me to have a
    '                     ' "Save" button become Enabled
    '                     ' when any TextBox or ComboBox
    '                     ' changes.
    '                     AddHandler tbx.TextChanged, _ AddressOf (ItemChanged)
    '                 End If
    '                 ' Level 2 object is a ComboBox
    '                 If TypeOf ObjLvl1 Is ComboBox Then
    '                     cmb = DirectCast(ObjLvl1, ComboBox)
    '                     ' A random "bug" in VB.Net 
    '                     ' sometimes forces you to set
    '                     ' the index twice before it will
    '                     ' take it...
    '                     cmb.SelectedIndex = -1
    '                     cmb.SelectedIndex = -1
    '                     AddHandler cmb.SelectedIndexChanged,
    '                     _ AddressOf (ItemChanged)
    '                 End If
    '             Next
    '         End If
    '         ' Level 1 object is a GroupBox ...
    '         If TypeOf ObjLvl1 Is GroupBox Then
    '             'Convert ObjLvl1 to a GroupBox
    '             gpbx = DirectCast(ObjLvl1, GroupBox)
    '             ' Run through all objects in the GroupBox
    '             For Each ObjLvl2 In gpbx.Controls
    '                 ' Level 2 object is a TextBox...
    '                 If TypeOf ObjLvl2 Is TextBox Then
    '                     ' Convert objlvl2 to a TextBox 
    '                     tbx = DirectCast(ObjLvl2, TextBox)
    '                     tbx.Text = ""
    '      AddHandler tbx.TextChanged,
    '        _ AddressOf ItemChanged
    '                 End If
    '                 ' Level 2 object is a ComboBox 
    '                 If TypeOf ObjLvl1 Is ComboBox Then
    '                     cmb = DirectCast(ObjLvl1, ComboBox)
    '                     cmb.SelectedIndex = -1
    '                     cmb.SelectedIndex = -1
    '      AddHandler cmb.SelectedIndexChanged,
    '        _ AddressOf ItemChanged
    '                 End If
    '             Next
    '         End If
    '         ' Level object 1 is a TextBox (On the form)
    '         If TypeOf ObjLvl1 Is TextBox Then
    '             tbx = DirectCast(ObjLvl1, TextBox)
    '             tbx.Text = ""
    'AddHandler tbx.TextChanged,
    '  _ AddressOf ItemChanged 
    '         End If
    '         ' Level 1 object is a ComboBox 
    '         If TypeOf ObjLvl1 Is ComboBox Then
    '             cmb = DirectCast(ObjLvl1, ComboBox)
    '             cmb.SelectedIndex = -1
    '             cmb.SelectedIndex = -1
    'AddHandler cmb.SelectedIndexChanged,
    '  _ AddressOf ItemChanged
    '         End If
    '     Next
    ' End Sub
    Public Function loaditcode(ByVal mtable As String, ByVal mfield As String, ByVal mfindfield As String, ByVal findstr As String) As Int32
        Dim msql As String
        'msql = "select " & Trim(mfield) & " as mkid from " & Trim(mtable) & " where " & Trim(mfindfield) & "='" & Trim(findstr) & "' and cmp_id='" & mcmpid & "'"
        msql = "select " & Trim(mfield) & " as mkid from " & Trim(mtable) & " where " & Trim(mfindfield) & "='" & Trim(findstr) & "'"
        Dim cmd As New SqlCommand(msql, con2)
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
    Public Function loaditcoderev(ByVal mtable As String, ByVal mfield As String, ByVal mfindfield As String, ByVal findstr As Int32) As String
        Dim msql As String
        msql = "select " & Trim(mfield) & " as mkid from " & Trim(mtable) & " where " & Trim(mfindfield) & "=" & findstr
        Dim cmd As New SqlCommand(msql, con2)
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


    Public Function loaddest(ByVal mdocentry As Integer) As String
        Dim msql As String
        msql = "select u_destination from oinv where docentry=" & mdocentry
        Dim cmd As New SqlCommand(msql, con2)
        'Dim CMD As New OleDb.OleDbCommand("SELECT section_id FROM section where sectionname='" & txtsectionname.Text & "' and  cmp_id='" & mcmpid & "'", con)
        If con2.State = ConnectionState.Closed Then
            con2.Open()
        End If
        Dim itdest As String = IIf(IsDBNull(cmd.ExecuteScalar) = False, cmd.ExecuteScalar, 0)
        loaddest = itdest
        cmd.Dispose()
        con2.Close()
        Return loaddest

    End Function

    Public Function loadcid(ByVal mtable As String, ByVal mfield As String, ByVal findstr As String) As String
        Dim msql As String
        msql = "select " & Trim(mfield) & " as mkid from " & Trim(mtable) & " where " & Trim(mfield) & "='" & Trim(findstr) & "' and cmp_id='" & mcmpid & "'"
        Dim cmd As New SqlCommand(msql, con)
        'Dim CMD As New OleDb.OleDbCommand("SELECT section_id FROM section where sectionname='" & txtsectionname.Text & "' and  cmp_id='" & mcmpid & "'", con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim secid As String = IIf(IsDBNull(cmd.ExecuteScalar) = False, cmd.ExecuteScalar, 0)
        loadcid = secid
        cmd.Dispose()
        Return loadcid
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

        '--msql = " select  CASE when LEFT(code,2)='FY' then  CONVERT(nvarchar(4),year(f_taxdate))+'-'+right(CONVERT(nvarchar(4),year(t_taxdate)),2) else code end code from ofpr" & vbCrLf _
        ' --     & "group by CASE when LEFT(code,2)='FY' then  CONVERT(nvarchar(4),year(f_taxdate))+'-'+right(CONVERT(nvarchar(4),year(t_taxdate)),2) else code end "

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
    Public Sub loadcomboyr(ByVal mtable As String, ByVal combofield As String, ByVal mycombo As ComboBox)
        Dim msql As String
        'msql = mqry
        'msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " WHERE CMP_ID='" & mcmpid & "' group by " & Trim(mfield) & " order by " & Trim(mfield)

        'If Len(Trim(grpfield)) > 0 Then
        '    msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " group by " & Trim(grpfield) & " order by " & Trim(grpfield)
        'Else
        '    msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " group by " & Trim(grpfield) & " order by " & Trim(grpfield)
        'End If

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

    Public Sub loadcomboqry(ByVal mqry As String, ByVal combofield As String, ByVal mycombo As ComboBox)
        Dim msql As String
        'msql = mqry
        'msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " WHERE CMP_ID='" & mcmpid & "' group by " & Trim(mfield) & " order by " & Trim(mfield)
        If Len(Trim(mqry)) > 0 Then

            msql = mqry

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
        End If
    End Sub

    Public Sub loadcombow(ByVal mtable As String, ByVal combofield As String, ByVal mycombo As ComboBox, ByVal wherefield As String, ByVal mdocentry As Long)
        Dim msql As String
        'msql = mqry
        'msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " WHERE CMP_ID='" & mcmpid & "' group by " & Trim(mfield) & " order by " & Trim(mfield)


        msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " where " & wherefield & " =" & mdocentry

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
    Public Sub loadcomboshow(ByVal mtable As String, ByVal combofield As String, ByVal mycombo As ComboBox, ByVal grpfield As String, ByVal wherfield As String)
        Dim msql As String
        'msql = mqry
        'msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " WHERE CMP_ID='" & mcmpid & "' group by " & Trim(mfield) & " order by " & Trim(mfield)

        If Len(Trim(wherfield)) > 0 Then
            If Len(Trim(grpfield)) > 0 Then
                msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " where " & Trim(wherfield) & "='C' group by " & Trim(grpfield) & " order by " & Trim(grpfield)
            Else
                msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " where " & Trim(wherfield) & "='C' group by " & Trim(grpfield) & " order by " & Trim(grpfield)
            End If

        Else
            If Len(Trim(grpfield)) > 0 Then
                msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " group by " & Trim(grpfield) & " order by " & Trim(grpfield)
            Else
                msql = "select " & Trim(combofield) & " from " & Trim(mtable) & " group by " & Trim(grpfield) & " order by " & Trim(grpfield)
            End If
        End If

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

    Public Sub loadlist(ByVal mtable As String, ByVal mfield As String, ByVal mycombo As ListBox)
        Dim msql As String
        msql = "select " & Trim(mfield) & " from " & Trim(mtable) & " WHERE CMP_ID='" & mcmpid & "' group by " & Trim(mfield) & " order by " & Trim(mfield)
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
                mycombo.Items.Add(DR.Item(Trim(mfield)))
            End While
        End If
        DR.Close()
        cmd.Dispose()
    End Sub


    Public Function loadfname(ByVal mtable As String, ByVal mfield As String, ByVal mfindfield As String, ByVal mkid As String) As String
        Dim msql As String
        msql = "select " & Trim(mfindfield) & " from " & Trim(mtable) & " where " & Trim(mfield) & "='" & Trim(mkid) & " ' and cmp_id='" & mcmpid & "'"
        Dim cmd As New SqlCommand(msql, con)
        'Dim CMD As New OleDb.OleDbCommand("SELECT sectionname FROM section where section_id='" & loadsecid() & "' and  cmp_id='" & mcmpid & "'", con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim secnam As String = IIf(IsDBNull(cmd.ExecuteScalar) = False, cmd.ExecuteScalar, 0)
        loadfname = secnam
        cmd.Dispose()
        Return loadfname
    End Function
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
    Public Function expiredata(ByVal mtable As String, ByVal mfield As String, ByVal whefield As String, ByVal mdocnum As Int32) As Boolean
        Dim msql As String

        msql = "select " & Trim(mfield) & " from " & Trim(mtable) & " where " & Trim(whefield) & "=" & Trim(mdocnum)
        Dim cmd As New SqlCommand(msql, con)
        'Dim CMD As New OleDb.OleDbCommand("SELECT sectionname FROM section where section_id='" & loadsecid() & "' and  cmp_id='" & mcmpid & "'", con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim tt As String
        tt = "30/11/2015"
        Dim dt As DateTime = New DateTime()
        dt = DateTime.Parse(tt)

        Dim dt3 As DateTime = New DateTime()
        dt3 = DateTime.Parse(Now())

        Dim secnam As String = IIf(IsDBNull(cmd.ExecuteScalar) = False, cmd.ExecuteScalar, 0)
        Dim dt2 As DateTime = New DateTime()
        dt2 = DateTime.Parse(secnam)
        If dt2 > dt Or dt3 > dt Then
            'If Convert.ToDateTime(secnam).ToString("dd-MM-yyyy") > Convert.ToDateTime("10-31-2015").ToString("dd-MM-yyyy") Then
            expiredata = True
        Else
            expiredata = False
        End If
        'expiredata = secnam
        cmd.Dispose()
        Return expiredata
    End Function


    Public Function hashcode(ByVal str As String) As String
        Dim rethas As String
        rethas = ""
        Try
            Dim hash As System.Security.Cryptography.SHA1 = System.Security.Cryptography.SHA1.Create
            Dim encoder As System.Text.ASCIIEncoding = New System.Text.ASCIIEncoding
            Dim combined As Byte() = encoder.GetBytes(str)
            hash.ComputeHash(combined)
            rethas = Convert.ToBase64String(hash.Hash)
            hashcode = rethas
        Catch ex As Exception
            Dim strerr As String = "Error in Hashcode :" + ex.Message
        End Try


        Return rethas

    End Function
    Public Sub autosearch(ByVal sender As Object)
        Dim boxIndex As Integer, lExst As Boolean
        Dim box As ComboBox = sender
        Dim txt As String = box.Text
        Dim posCursor As Integer = box.SelectionStart

        ' If Cursor does not stay on the beginning of text box.
        If posCursor <> 0 Then
            lExst = False
            ' Go in cycle through the combo box list to find the appropriate entry in the list
            For boxIndex = 0 To box.Items.Count - 1
                If UCase(Mid(box.Items(boxIndex), 1, posCursor)) =
                   UCase(Mid(txt, 1, posCursor)) Then
                    box.Text = box.Items(boxIndex)
                    box.SelectionStart = posCursor
                    lExst = True
                    Exit For
                End If
            Next
            ' We didn't find appropriate entry and return previous value to text box
            If Not lExst Then
                box.Text = Mid(txt, 1, posCursor - 1) + Mid(txt, posCursor + 1)
                box.SelectionStart = posCursor - 1
            End If
        End If
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
    'Public Sub EXCELEXPORT(ByVal CTRL As AxMSFlexGrid)
    '    Dim SPREADSHEET1 As Excel.Application 'To open Excel

    '    Dim dir As String
    '    Dim mdir As String
    '    Dim xlWrkB As Excel.Workbook
    '    Dim xlWrkS As Excel.Worksheet
    '    Dim misValue As Object = System.Reflection.Missing.Value

    '    SPREADSHEET1 = New Excel.ApplicationClass
    '    xlWrkB = SPREADSHEET1.Workbooks.Add(misValue)
    '    xlWrkS = xlWrkB.Sheets("sheet1")

    '    'SPREADSHEET1 = CreateObject("Excel.application") 'Creates an object
    '    'SPREADSHEET1.Visible = True ' So you can see Excel
    '    SPREADSHEET1.Workbooks.Add()
    '    'Dim nfile As Integer

    '    For I = 0 To CTRL.Rows - 1
    '        For j = 0 To CTRL.Cols - 1
    '            If IsDate(CTRL.get_TextMatrix(I, J)) = True Then
    '                If Trim(CTRL.get_TextMatrix(I, J)) <> "0" And Trim(CTRL.get_TextMatrix(I, J)) <> "0.00" Then
    '                    'SPREADSHEET1.Cells(I + 1, j + 1) = "'" & CTRL.TextMatrix(I, j)
    '                    If InStr(CTRL.get_TextMatrix(I, J), "-") > 0 Or InStr(CTRL.get_TextMatrix(I, J), "-/") > 0 Then
    '                        SPREADSHEET1.Cells(I + 1, J + 1) = "'" & CTRL.get_TextMatrix(I, J)
    '                    Else
    '                        SPREADSHEET1.Cells(I + 1, J + 1) = CTRL.get_TextMatrix(I, J)
    '                    End If
    '                Else
    '                    SPREADSHEET1.Cells(I + 1, J + 1) = CTRL.get_TextMatrix(I, J)
    '                End If
    '            Else
    '                SPREADSHEET1.Cells(I + 1, J + 1) = CTRL.get_TextMatrix(I, J)
    '            End If


    '            'SPREADSHEET1.Cells(I + 1, j + 1) = CTRL.TextMatrix(I, j)
    '        Next
    '        'ApExcel.cells(1, 1).Formula = "HELLO"   'Add Text to a Cell
    '    Next
    '    'MM = CTRL.Cols - 1

    '    SPREADSHEET1.Visible = True
    '    'spreadsheet1.ActiveWorkbook.SaveAs App.Path & "\OTSTOCKN.XLS"
    '    'ApExcel.cells(1, 1).Formula = "HELLO"   'Add Text to a Cell
    '    Dir = System.AppDomain.CurrentDomain.BaseDirectory()
    '    mdir = Trim(dir) & "pod" & Microsoft.VisualBasic.Format(Now(), "ddmmyyyy") & ".txt"
    '    xlWrkS.SaveAs(mdir)
    '    'xlWrkS.SaveAs("C:\vbexcel.xls")

    '    ' xlWrkB.Close()
    '    SPREADSHEET1.Application.Quit()

    '    ' releaseObject(SPREADSHEET1)
    '    ' releaseObject(xlWorkBook)
    '    ' releaseObject(xlWorkSheet)

    '    'SPREADSHEET1.Application.Quit()
    '    SPREADSHEET1 = Nothing
    'End Sub


    Public Sub excelexport(ByVal CTRL As AxMSFlexGrid)


        Dim ldir, lmdir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\barcodadd.txt"

        ldir = System.AppDomain.CurrentDomain.BaseDirectory()
        lmdir = Trim(ldir) & "PODDET.xls"

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
        Process.Start(lmdir)
        'Console.WriteLine("Time:{0}", (Environment.TickCount - ticks))
    End Sub

    Public Sub excelexport2(ByVal CTRL As AxMSFlexGrid, ByVal mhead As String)


        Dim ldir, lmdir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\barcodadd.txt"

        ldir = System.AppDomain.CurrentDomain.BaseDirectory()
        lmdir = Trim(ldir) & "PODDET.xls"

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
        Process.Start(lmdir)
        'Console.WriteLine("Time:{0}", (Environment.TickCount - ticks))
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
        'Dim style2 As WorksheetStyle = book.Styles.Add("style2")

        'style2.Font.Bold = False

        'Export Header Names Start
        Dim columnsCount As Integer = CTRL.Columns.Count

        Dim Row As WorksheetRow = sheet.Table.Rows.Add
        'Dim column As Integer = 0
        Dim column = CTRL.Columns.Count
        'Do Until column = columnsCount
        'For Each column In CTRL.Columns
        style.Font.Bold = True
        For iC As Integer = 0 To column - lastcol
            'Do Until column = columnsCount - lastcol
            Row.Cells.Add(CTRL.Columns(iC).HeaderText, DataType.String, "style1")

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
            Do Until columnIndex = columnsCount - (lastcol - 1)
                Try
                    Row.Cells.Add(IIf(CTRL.Item(columnIndex, i).Value Is Nothing, "", CTRL.Item(columnIndex, i).Value.ToString & vbNullString), DataType.String, "style2")
                    'Row2.Cells(i + 2, columnIndex + 1).Value = CTRL.Item(columnIndex, i).Value.ToString
                    columnIndex += 1
                Catch ex As Exception
                    MsgBox(ex.Message & Str(columnIndex))
                End Try

            Loop
        Next



        'For I = 0 To CTRL.Rows - 1

        '    Dim Row0 As WorksheetRow = sheet.Table.Rows.Add

        '    For J = 0 To CTRL.Cols - 1

        '        ' Add a cell
        '        'Row0.Cells.Add("Hello World", DataType.String, "style1")
        '        Row0.Cells.Add(CTRL.get_TextMatrix(I, J), DataType.String, "style1")
        '    Next J
        '    If I = 0 Then
        '        'Dim style As WorksheetStyle = book.Styles.Add("style1")
        '        style.Font.Bold = True
        '    Else
        '        style.Font.Bold = False
        '        style.Font.Size = 10
        '        style.Alignment.Vertical = StyleVerticalAlignment.Top
        '        'style.Alignment.Horizontal = StyleHorizontalAlignment.Justify
        '    End If


        'Next I

        ' Save it
        'book.Save("c:\test.xls")
        book.Save(lmdir)
        If mos = "WIN" Then
            'open file
            Process.Start(lmdir)
        Else
            OpenWithLibreOffice(lmdir)
        End If

        'Console.WriteLine("Time:{0}", (Environment.TickCount - ticks))
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
    Friend Function ViewReport(ByVal sReportName As String, Optional ByVal sSelectionFormula As String = "", Optional ByVal param As String = "") As Boolean

        ''Declaring variables
        'Dim intCounter As Integer
        'Dim intCounter1 As Integer
        'Dim strTableName As String
        'Dim objReportsParameters As Frmorderprn
        ''frmReportsParameters()

        ''Crystal Report's report document object
        'Dim objReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument

        'Dim mySection As CrystalDecisions.CrystalReports.Engine.Section
        'Dim mySections As CrystalDecisions.CrystalReports.Engine.Sections

        ''object of table Log on info of Crystal report
        'Dim ConInfo As New CrystalDecisions.Shared.TableLogOnInfo

        ''Parameter value object of crystal report 
        '' parameters used for adding the value to parameter.
        'Dim paraValue As New CrystalDecisions.Shared.ParameterDiscreteValue

        ''Current parameter value object(collection) of crystal report parameters.
        'Dim currValue As CrystalDecisions.Shared.ParameterValues

        ''Sub report object of crystal report.
        'Dim SubRptO As CrystalDecisions.CrystalReports.Engine.SubreportObject

        ''Sub report document of crystal report.
        'Dim SubRptD As New CrystalDecisions.CrystalReports.Engine.ReportDocument

        'Dim strParamenters As String
        'Dim strParValPair() As String
        'Dim strVal() As String
        'Dim sFileName As String
        'Dim index As Integer

        'Try

        '    'Load the report
        '    'sFileName = DownloadReport(sReportName, m_strReportDir)
        '    'objReport.Load(sFileName)

        '    'sFileName = DownloadReport(sReportName, m_strReportDir)
        '    objReport.Load(sReportName)

        '    'Check if there are parameters or not in report.
        '    'As parameter fields collection also picks the selection formula 
        '    ' which is not the parameter so if total parameter count is 1 
        '    ' then we check whether its a parameter or selection formula.
        '    intCounter = objReport.DataDefinition.ParameterFields.Count
        '    If intCounter = 1 Then
        '        If InStr(objReport.DataDefinition.ParameterFields(0).ParameterFieldName, ".", CompareMethod.Text) > 0 Then
        '            intCounter = 0
        '        End If
        '    End If

        '    'If there are parameters in report and user has passed them then 
        '    ' split the parameter string and Apply the values to their concurrent parameters.
        '    If intCounter > 0 And Trim(param) <> "" Then
        '        strParValPair = strParamenters.Split("&")
        '        For index = 0 To UBound(strParValPair)
        '            If InStr(strParValPair(index), "=") > 0 Then
        '                strVal = strParValPair(index).Split("=")
        '                paraValue.Value = strVal(1)
        '                currValue = objReport.DataDefinition.ParameterFields(strVal(0)).CurrentValues
        '                currValue.Add(paraValue)
        '                objReport.DataDefinition.ParameterFields(strVal(0)).ApplyCurrentValues(currValue)
        '            End If
        '        Next
        '    End If

        '    'Set the connection information to ConInfo object so that we can apply the 
        '    ' connection information on each table in the report
        '    ConInfo.ConnectionInfo.UserID = objDataBase.UserName
        '    ConInfo.ConnectionInfo.Password = objDataBase.Password
        '    ConInfo.ConnectionInfo.ServerName = objDataBase.Server
        '    ConInfo.ConnectionInfo.DatabaseName = objDataBase.Database

        '    For intCounter = 0 To objReport.Database.Tables.Count - 1
        '        objReport.Database.Tables(intCounter).ApplyLogOnInfo(ConInfo)
        '    Next

        '    'Loop through each section on the report then look through each object
        '    ' in the section if the object is a subreport, then apply logon info 
        '    ' on each table of that sub report
        '    For index = 0 To objReport.ReportDefinition.Sections.Count - 1
        '        For intCounter = 0 To objReport.ReportDefinition.Sections(index).ReportObjects.Count - 1
        '            With objReport.ReportDefinition.Sections(index)
        '                If .ReportObjects(intCounter).Kind = CrystalDecisions.Shared.ReportObjectKind.SubreportObject Then
        '                    SubRptO = CType(.ReportObjects(intCounter), CrystalDecisions.CrystalReports.Engine.SubreportObject)
        '                    SubRptD = SubRptO.OpenSubreport(SubRptO.SubreportName)
        '                    For intCounter1 = 0 To SubRptD.Database.Tables.Count - 1
        '                        SubRptD.Database.Tables(intCounter1).ApplyLogOnInfo(ConInfo)
        '                    Next
        '                End If
        '            End With
        '        Next
        '    Next

        '    'If there is a selection formula passed to this function then use that
        '    If sSelectionFormula.Length > 0 Then
        '        objReport.RecordSelectionFormula = sSelectionFormula
        '    End If

        '    'Re setting control 
        '    rptViewer.ReportSource = Nothing

        '    'Set the current report object to report.
        '    rptViewer.ReportSource = objReport

        '    'Show the report
        '    rptViewer.Show()

        '    Application.DoEvents()

        '    Me.Text = sReportName
        '    MyBase.Visible = True
        '    Me.BringToFront()

        '    Return True

        'Catch ex As System.Exception
        '    MsgBox(ex.Message)
        'End Try

    End Function


    'Public Sub CrystalReportLogOn(ByVal reportParameters As ReportDocument, ByVal serverName As String, ByVal databaseName As String, ByVal userName As String, ByVal password As String)
    'Dim logOnInfo As TableLogOnInfo
    'Dim subRd As ReportDocument
    'Dim sects As Sections
    'Dim ros As ReportObjects
    'Dim sro As SubreportObject

    'If reportParameters Is Nothing Then
    '    Throw New ArgumentNullException("reportParameters")
    'End If

    'Try
    '    For Each t As CrystalDecisions.CrystalReports.Engine.Table In reportParameters.Database.Tables
    '        logOnInfo = t.LogOnInfo
    '        logOnInfo.ReportName = reportParameters.Name
    '        logOnInfo.ConnectionInfo.ServerName = serverName
    '        logOnInfo.ConnectionInfo.DatabaseName = databaseName
    '        logOnInfo.ConnectionInfo.UserID = userName
    '        logOnInfo.ConnectionInfo.Password = password
    '        logOnInfo.TableName = t.Name
    '        t.ApplyLogOnInfo(logOnInfo)
    '        t.Location = t.Name
    '    Next
    'Catch
    '    Throw
    'End Try

    'sects = reportParameters.ReportDefinition.Sections
    'For Each sect As Section In sects
    '    ros = sect.ReportObjects
    '    For Each ro As ReportObject In ros
    '        If ro.Kind = ReportObjectKind.SubreportObject Then
    '            sro = DirectCast(ro, SubreportObject)
    '            subRd = sro.OpenSubreport(sro.SubreportName)
    '            Try
    '                For Each t As CrystalDecisions.CrystalReports.Engine.Table In subRd.Database.Tables
    '                    logOnInfo = t.LogOnInfo
    '                    logOnInfo.ReportName = reportParameters.Name
    '                    logOnInfo.ConnectionInfo.ServerName = serverName
    '                    logOnInfo.ConnectionInfo.DatabaseName = databaseName
    '                    logOnInfo.ConnectionInfo.UserID = userName
    '                    logOnInfo.ConnectionInfo.Password = password
    '                    logOnInfo.TableName = t.Name
    '                    t.ApplyLogOnInfo(logOnInfo)
    '                Next
    '            Catch
    '                Throw
    '            End Try

    '        End If
    '    Next
    'Next
    'End Sub
    'Friend Function ViewReport(ByVal sReportName As String, Optional ByVal sSelectionFormula As String = "", Optional ByVal param As String = "") As Boolean

    '    'Declaring variables
    '    Dim intCounter As Integer
    '    Dim intCounter1 As Integer
    '    Dim strTableName As String
    '    Dim objReportsParameters As frmReportsParameters

    '    'Crystal Report's report document object
    '    Dim objReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument

    '    Dim mySection As CrystalDecisions.CrystalReports.Engine.Section
    '    Dim mySections As CrystalDecisions.CrystalReports.Engine.Sections

    '    'object of table Log on info of Crystal report
    '    Dim ConInfo As New CrystalDecisions.Shared.TableLogOnInfo

    '    'Parameter value object of crystal report 
    '    ' parameters used for adding the value to parameter.
    '    Dim paraValue As New CrystalDecisions.Shared.ParameterDiscreteValue

    '    'Current parameter value object(collection) of crystal report parameters.
    '    Dim currValue As CrystalDecisions.Shared.ParameterValues

    '    'Sub report object of crystal report.
    '    Dim SubRptO As CrystalDecisions.CrystalReports.Engine.SubreportObject

    '    'Sub report document of crystal report.
    '    Dim SubRptD As New CrystalDecisions.CrystalReports.Engine.ReportDocument

    '    Dim strParamenters As String
    '    Dim strParValPair() As String
    '    Dim strVal() As String
    '    Dim sFileName As String
    '    Dim index As Integer

    '    Try

    '        'Load the report
    '        sFileName = DownloadReport(sReportName, m_strReportDir)
    '        objReport.Load(sFileName)

    '        'Check if there are parameters or not in report.
    '        'As parameter fields collection also picks the selection formula 
    '        ' which is not the parameter so if total parameter count is 1 
    '        ' then we check whether its a parameter or selection formula.
    '        intCounter = objReport.DataDefinition.ParameterFields.Count
    '        If intCounter = 1 Then
    '            If InStr(objReport.DataDefinition.ParameterFields(0).ParameterFieldName, ".", CompareMethod.Text) > 0 Then
    '                intCounter = 0
    '            End If
    '        End If

    '        'If there are parameters in report and user has passed them then 
    '        ' split the parameter string and Apply the values to their concurrent parameters.
    '        If intCounter > 0 And Trim(param) <> "" Then
    '            strParValPair = strParamenters.Split("&")
    '            For index = 0 To UBound(strParValPair)
    '                If InStr(strParValPair(index), "=") > 0 Then
    '                    strVal = strParValPair(index).Split("=")
    '                    paraValue.Value = strVal(1)
    '                    currValue = objReport.DataDefinition.ParameterFields(strVal(0)).CurrentValues
    '                    currValue.Add(paraValue)
    '                    objReport.DataDefinition.ParameterFields(strVal(0)).ApplyCurrentValues(currValue)
    '                End If
    '            Next
    '        End If

    '        'Set the connection information to ConInfo object so that we can apply the 
    '        ' connection information on each table in the report
    '        ConInfo.ConnectionInfo.UserID = objDataBase.UserName
    '        ConInfo.ConnectionInfo.Password = objDataBase.Password
    '        ConInfo.ConnectionInfo.ServerName = objDataBase.Server
    '        ConInfo.ConnectionInfo.DatabaseName = objDataBase.Database

    '        For intCounter = 0 To objReport.Database.Tables.Count - 1
    '            objReport.Database.Tables(intCounter).ApplyLogOnInfo(ConInfo)
    '        Next

    '        'Loop through each section on the report then look through each object
    '        ' in the section if the object is a subreport, then apply logon info 
    '        ' on each table of that sub report
    '        For index = 0 To objReport.ReportDefinition.Sections.Count - 1
    '            For intCounter = 0 To objReport.ReportDefinition.Sections(index).ReportObjects.Count - 1
    '                With objReport.ReportDefinition.Sections(index)
    '                    If .ReportObjects(intCounter).Kind = CrystalDecisions.Shared.ReportObjectKind.SubreportObject Then
    '                        SubRptO = CType(.ReportObjects(intCounter), CrystalDecisions.CrystalReports.Engine.SubreportObject)
    '                        SubRptD = SubRptO.OpenSubreport(SubRptO.SubreportName)
    '                        For intCounter1 = 0 To SubRptD.Database.Tables.Count - 1
    '                            SubRptD.Database.Tables(intCounter1).ApplyLogOnInfo(ConInfo)
    '                        Next
    '                        If sSelectionFormula.Length > 0 Then
    '                            SubRptO.RecordSelectionFormula = sSelectionFormula
    '                        End If
    '                    End If
    '                End With
    '            Next
    '        Next

    '        'If there is a selection formula passed to this function then use that
    '        If sSelectionFormula.Length > 0 Then
    '            objReport.RecordSelectionFormula = sSelectionFormula
    '        End If

    '        'Re setting control 
    '        rptViewer.ReportSource = Nothing

    '        'Set the current report object to report.
    '        rptViewer.ReportSource = objReport

    '        'Show the report
    '        rptViewer.Show()

    '        Application.DoEvents()

    '        Me.Text = sReportName
    '        MyBase.Visible = True
    '        Me.BringToFront()

    '        Return True

    '    Catch ex As System.Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Function

    'Public Sub CrystalReportLogOn2(ByVal reportParameters As ReportDocument, ByVal serverName As String, ByVal databaseName As String, ByVal userName As String, ByVal password As String)
    '    Dim logOnInfo As TableLogOnInfo
    '    Dim subRd As ReportDocument
    '    Dim sects As Sections
    '    Dim ros As ReportObjects
    '    'Dim sro As SubreportObject
    '    If (reportParameters Is Nothing) Then
    '        Throw New ArgumentNullException("reportParameters")
    '    End If
    '    Try
    '        For Each t As CrystalDecisions.CrystalReports.Engine.Table In reportParameters.Database.Tables
    '            logOnInfo = t.LogOnInfo
    '            logOnInfo.ReportName = reportParameters.Name
    '            logOnInfo.ConnectionInfo.ServerName = serverName
    '            logOnInfo.ConnectionInfo.DatabaseName = databaseName
    '            logOnInfo.ConnectionInfo.UserID = userName
    '            logOnInfo.ConnectionInfo.Password = password
    '            logOnInfo.TableName = t.Name
    '            t.ApplyLogOnInfo(logOnInfo)
    '            t.Location = t.Name
    '        Next
    '    Catch ex As System.Exception
    '        MsgBox(ex.Message)
    '        Throw
    '    End Try

    '    'sects = reportParameters.ReportDefinition.Sections
    '    'For Each sect As Section In sects
    '    '    ros = sect.ReportObjects
    '    '    For Each ro As ReportObject In ros
    '    '        If (ro.Kind = ReportObjectKind.SubreportObject) Then
    '    '            sro = CType(ro, SubreportObject)
    '    '            subRd = sro.OpenSubreport(sro.SubreportName)
    '    '            Try
    '    '                For Each t As CrystalDecisions.CrystalReports.Engine.Table In subRd.Database.Tables
    '    '                    logOnInfo = t.LogOnInfo
    '    '                    logOnInfo.ReportName = reportParameters.Name
    '    '                    logOnInfo.ConnectionInfo.ServerName = serverName
    '    '                    logOnInfo.ConnectionInfo.DatabaseName = databaseName
    '    '                    logOnInfo.ConnectionInfo.UserID = userName
    '    '                    logOnInfo.ConnectionInfo.Password = password
    '    '                    logOnInfo.TableName = t.Name
    '    '                    t.ApplyLogOnInfo(logOnInfo)
    '    '                Next
    '    '            Catch ex As System.Exception
    '    '                Throw
    '    '            End Try
    '    '        End If
    '    '    Next
    '    'Next
    'End Sub


    Public Sub sendmail(ByVal username As String, ByVal pwd As String, ByVal smtphost As String, ByVal port As String, ByVal mailfrom As String, ByVal mailto As String, ByVal msubject As String, ByVal message As String, ByVal mccto As String, ByVal mattachment As String)

        Dim SmtpServer As New SmtpClient()
        Dim mail As New MailMessage()
        Dim attachToMsg As Attachment
        'Dim mail As New MailMessage()

        'SmtpServer.Credentials = New Net.NetworkCredential("username@gmail.com", "password")
        'SmtpServer.Credentials = New Net.NetworkCredential("selvakumar", txtPwd.Text)
        SmtpServer.Credentials = New Net.NetworkCredential(username, pwd)

        '587-out,25 local
        SmtpServer.Port = port
        SmtpServer.Host = smtphost
        mail = New MailMessage()
        mail.From = New MailAddress(mailfrom)

        If mailto.Contains(";") Then
            Dim emailList As String()
            emailList = mailto.Split(";")
            'emailList = Split(";", txtTo.Text) '  txtTo.Split(";")
            For Each email As String In emailList
                mail.To.Add(email)
            Next
        Else
            mail.To.Add(mailto)
        End If
        If mccto <> "" Then
            If mccto.Contains(";") Then
                Dim ccList As String()
                ccList = mccto.Split(";")
                For Each ccTo As String In ccList
                    mail.CC.Add(ccTo)
                Next
            Else
                mail.CC.Add(mccto)
            End If
        End If

        If mattachment <> "" Then
            If mattachment.Contains(",") Then
                Dim attachList As String()
                attachList = mattachment.Split(",")
                For Each attccTo As String In attachList
                    If Len(Trim(attccTo)) > 0 Then
                        attachToMsg = New Attachment(attccTo)
                        mail.Attachments.Add(attachToMsg)
                    End If
                    'mail.CC.Add(attccTo)
                Next
            Else
                'mail.CC.Add(txtcc.Text)
            End If
        End If



        mail.Subject = msubject
        mail.Body = message
        SmtpServer.Send(mail)
        MsgBox("mail send")
        'Catch ex As Exception
        ' MsgBox(ex.ToString)
        ' End Try
    End Sub

    Public Function findpartycode(ByVal mcont As Control, ByVal mcardcode As Int16, ByVal magent As Int16) As String
        'dbcon(mcomp)
        Dim mstr As String
        Dim msql As String
        If mcardcode = 1 And magent = 0 Then
            msql = "select cardcode  from  ocrd where cardname='" & Trim(mcont.Text) & "'"
        End If
        If magent = 1 And mcardcode = 0 Then
            msql = "select u_areacode  from  ocrd where cardname='" & Trim(mcont.Text) & "'"
        End If


        Dim CMD As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Try
            ''Dim DR As SqlDataReader
            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then

                While DR.Read
                    'mcont.Items.Add(DR.Item("u_areacode"))
                    If mcardcode = 1 And magent = 0 Then
                        findpartycode = DR.Item("cardcode")
                        mstr = DR.Item("cardcode")
                    End If
                    If magent = 1 And mcardcode = 0 Then
                        findpartycode = DR.Item("u_areacode")
                        mstr = DR.Item("u_areacode")
                    End If
                    'cmbagent.Items.Add(DR.Item("u_areacode"))
                End While

            End If
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            'cmbagent.Items.Clear()
            mcont.Text = ""
            'flx.Clear()
            'Call flxchead()
        End Try

        CMD.Dispose()
        Return mstr
    End Function
    Public Function findpartycode2(ByVal mcardname As String, ByVal mcardcode As Int16, ByVal magent As Int16) As String
        'dbcon(mcomp)
        Dim mstr2 As String
        Dim msql2 As String
        If mcardcode = 1 And magent = 0 Then
            msql2 = "select cardcode  from  ocrd where rtrim(cardname)='" & Trim(mcardname) & "'"
        End If
        If magent = 1 And mcardcode = 0 Then
            msql2 = "select u_areacode  from  ocrd where rtrim(cardname)='" & Trim(mcardname) & "'"
        End If


        Dim CMD As New SqlCommand(msql2, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Try
            ''Dim DR As SqlDataReader
            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then

                While DR.Read
                    'mcont.Items.Add(DR.Item("u_areacode"))
                    If mcardcode = 1 And magent = 0 Then
                        findpartycode2 = DR.Item("cardcode")
                        mstr2 = DR.Item("cardcode")
                    End If
                    If magent = 1 And mcardcode = 0 Then
                        findpartycode2 = DR.Item("u_areacode")
                        mstr2 = DR.Item("u_areacode")
                    End If
                    'cmbagent.Items.Add(DR.Item("u_areacode"))
                End While

            End If
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            'cmbagent.Items.Clear()
            'mcont.Text = ""
            'flx.Clear()
            'Call flxchead()
        End Try

        CMD.Dispose()
        Return mstr2
    End Function


    Public Sub loadparty2(ByVal mcont As ComboBox)
        'dbcon(mcomp)
        Dim msql As String
        msql = "select cardname  from  ocrd where cardtype='C'  group by cardname order by cardname"
        'msql = "select u_areacode  from  " & Trim(mcomp) & ".dbo.ocrd where u_areacode is not null group by u_areacode order by u_areacode"
        Dim CMD As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        mcont.Items.Clear()
        Try
            ''Dim DR As SqlDataReader
            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then

                While DR.Read
                    mcont.Items.Add(DR.Item("cardname"))
                    'cmbagent.Items.Add(DR.Item("u_areacode"))
                End While

            End If
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            'cmbagent.Items.Clear()
            mcont.Items.Clear()
            'flx.Clear()
            'Call flxchead()
        End Try

        CMD.Dispose()
    End Sub

    Public Sub loadwhs(ByVal mcont As ComboBox)
        'dbcon(mcomp)
        Dim msql As String
        msql = "select whscode  from  owhs   group by whscode order by whscode"
        'msql = "select u_areacode  from  " & Trim(mcomp) & ".dbo.ocrd where u_areacode is not null group by u_areacode order by u_areacode"
        Dim CMD As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        mcont.Items.Clear()
        Try
            ''Dim DR As SqlDataReader
            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then

                While DR.Read
                    mcont.Items.Add(DR.Item("whscode"))
                    'cmbagent.Items.Add(DR.Item("u_areacode"))
                End While

            End If
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            'cmbagent.Items.Clear()
            mcont.Items.Clear()
            'flx.Clear()
            'Call flxchead()
        End Try

        CMD.Dispose()
    End Sub

    Public Sub loadgrp(ByVal mcont As ComboBox, ByVal mfield As String)
        'dbcon(mcomp)
        Dim msql As String
        msql = "select " & Trim(mfield) & " as grp  from  oitm   group by " & mfield & " order by " & mfield
        'msql = "select u_areacode  from  " & Trim(mcomp) & ".dbo.ocrd where u_areacode is not null group by u_areacode order by u_areacode"
        Dim CMD As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        mcont.Items.Clear()
        Try
            ''Dim DR As SqlDataReader
            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then

                While DR.Read
                    mcont.Items.Add(DR.Item("grp"))
                    'cmbagent.Items.Add(DR.Item("u_areacode"))
                End While

            End If
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            'cmbagent.Items.Clear()
            mcont.Items.Clear()
            'flx.Clear()
            'Call flxchead()
        End Try

        CMD.Dispose()
    End Sub

    Public Sub loaditem(ByVal mcont As ComboBox)
        'dbcon(mcomp)
        Dim msql As String
        msql = "select itemname as item  from  oitm   where dfltwh in ('SALGOODS') order by itemname,u_style,u_size"
        'msql = "select u_areacode  from  " & Trim(mcomp) & ".dbo.ocrd where u_areacode is not null group by u_areacode order by u_areacode"
        Dim CMD As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        mcont.Items.Clear()
        Try
            ''Dim DR As SqlDataReader
            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then

                While DR.Read
                    mcont.Items.Add(DR.Item("item"))
                    'cmbagent.Items.Add(DR.Item("u_areacode"))
                End While

            End If
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            'cmbagent.Items.Clear()
            mcont.Items.Clear()
            'flx.Clear()
            'Call flxchead()
        End Try

        CMD.Dispose()
    End Sub




    Public Function decodefile(ByVal srcfile As String) As String

        Dim decodedBytes As Byte()
        decodedBytes = Convert.FromBase64String(Decode(srcfile))

        Dim decodedText As String
        decodedText = Encoding.UTF8.GetString(decodedBytes)
        decodefile = decodedText
    End Function

    'Sub EncodeFile(ByVal srcFile As String, ByVal destfile As String)
    Public Function encodefile(ByVal srcfile As String) As String

        Dim bytesToEncode As Byte()
        bytesToEncode = Encoding.UTF8.GetBytes(srcfile)

        Dim encodedText As String
        encodedText = Convert.ToBase64String(bytesToEncode)
        encodefile = Encript(encodedText)
    End Function


    Public Sub exportexcelData(ByVal mtable As String, ByVal mwhere As String, ByVal morder As String)
        Dim mstrr As String
        Dim strData As String = ""
        Dim bolFirstPass As Boolean = True

        Dim ldir, lmdir As String
        ldir = System.AppDomain.CurrentDomain.BaseDirectory()
        lmdir = Trim(ldir) & "barcod.xls"

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

                'If bolFirstPass Then
                '    bolFirstPass = False
                '    'Write the Header Row
                '    strData = "<body><meta http-equiv='Content-Type' content='text/html;charset=utf-8'/>"
                '    strData &= "<table border=1>"
                '    strData &= "<tr>"
                For i As Integer = 0 To oDr.FieldCount - 1
                    'oDr.GetName(i)
                    Row0.Cells.Add(oDr.Item(oDr.GetName(i)))
                    'strData &= "<td>" & Replace(Replace(oDr.GetName(i), "[", ""), "]", "") & "</td>"
                Next
                'strData &= "</tr>"
                'bolFirstPass = False
                'Response.Clear()
                'Response.ContentType = "application/vnd.ms-excel"
                'Response.AddHeader("content-disposition", "attachment; filename=filename.xls")
                'Response.Write(strData)
                'End If
                'strData = "<tr>"
                'For i As Integer = 0 To oDr.FieldCount - 1
                '    strData &= "<td>" & i.ToString() & "</td>"
                'Next
                'strData &= "</tr>"
                'Response.Write(strData)
            End While
            book.Save(lmdir)
            'open file
            Process.Start(lmdir)
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




    Public Function imageToByteArray(ByVal imageIn As System.Drawing.Image) As Byte()
        Dim ms As MemoryStream = New MemoryStream
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif)
        Return ms.ToArray
    End Function



    Public Sub findgridnew(ByVal strr As String, ByVal ctrl As DataGridView, ByVal mcol As Integer)

        Dim s As String = strr.ToString.ToUpper 'TextBox1.Text.Trim
        ctrl.ClearSelection()
        'MsgBox(s)
        ctrl.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        For x As Integer = 0 To ctrl.Rows.Count - 1
            'If dv1.CurrentCell.Selected = True Then
            If Mid(ctrl.Rows(x).Cells(mcol).Value, 1, Len(s)).ToUpper = s Then

                'If CStr(dv1.Rows(x).Cells(1).Value).StartsWith(s).ToString.ToUpper Then
                'ctrl.FirstDisplayedScrollingRowIndex = x
                ctrl.FirstDisplayedScrollingRowIndex = ctrl.Rows(x).Index

                ctrl.Refresh()
                ctrl.Rows(x).Selected = True
                ctrl.CurrentCell = ctrl.Rows(x).Cells(mcol)
                ctrl.Item(mcol, x).Selected = True
                'MsgBox(ctrl.Rows.Item(x).Cells(mcol))
                'ctrl.Rows(x).Selected = True
                'ctrl.roww.Select = x
                'dv1.FirstDisplayedScrollingRowIndex = x
                'dv1.Rows(x).Cells(1).Style.BackColor = Color.Yellow
                Exit Sub
            End If
            ' End If
        Next
    End Sub

    Public Sub exportexcelDataqry(ByVal mquery As String)
        Dim mstrr As String
        Dim strData As String = ""
        Dim bolFirstPass As Boolean = True

        Dim ldir, lmdir As String
        ldir = System.AppDomain.CurrentDomain.BaseDirectory()
        lmdir = Trim(ldir) & "Despatch.xls"

        Dim book As Workbook = New Workbook
        'Dim oCn As SqlConnection = Nothing
        Dim oCmd As SqlCommand = Nothing
        Dim oDr As SqlDataReader = Nothing


        Try
            'oCn = New SqlConnection("YOUR CONNECTION STRING ")
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            mstrr = mquery

            oCmd = New SqlCommand(mstrr, con)

            Dim sheet As Worksheet = book.Worksheets.Add("SampleSheet")
            oDr = oCmd.ExecuteReader(CommandBehavior.CloseConnection)
            Dim Row1 As WorksheetRow = sheet.Table.Rows.Add
            Row1.Cells.Add("Itemcode")
            Row1.Cells.Add("Item Name")
            Row1.Cells.Add("Style")
            Row1.Cells.Add("Size")
            Row1.Cells.Add("Qty")
            Row1.Cells.Add("Rate")
            Row1.Cells.Add("Linetotal")

            'For i As Integer = 0 To oDr.FieldCount - 1
            '    'oDr.GetName(i)
            '    Row1.Cells.Add(oDr.GetName(i))
            '    'strData &= "<td>" & Replace(Replace(oDr.GetName(i), "[", ""), "]", "") & "</td>"
            'Next


            '    'strData &= "<td>" & Replace(Replace(oDr.GetName(i), "[", ""), "]", "") & "</td>"
            'Next

            While oDr.Read
                Dim Row0 As WorksheetRow = sheet.Table.Rows.Add

                'If bolFirstPass Then
                '    bolFirstPass = False
                '    'Write the Header Row
                '    strData = "<body><meta http-equiv='Content-Type' content='text/html;charset=utf-8'/>"
                '    strData &= "<table border=1>"
                '    strData &= "<tr>"
                For i As Integer = 0 To oDr.FieldCount - 1
                    'oDr.GetName(i)
                    Row0.Cells.Add(oDr.Item(oDr.GetName(i)))
                    'strData &= "<td>" & Replace(Replace(oDr.GetName(i), "[", ""), "]", "") & "</td>"
                Next
                'strData &= "</tr>"
                'bolFirstPass = False
                'Response.Clear()
                'Response.ContentType = "application/vnd.ms-excel"
                'Response.AddHeader("content-disposition", "attachment; filename=filename.xls")
                'Response.Write(strData)
                'End If
                'strData = "<tr>"
                'For i As Integer = 0 To oDr.FieldCount - 1
                '    strData &= "<td>" & i.ToString() & "</td>"
                'Next
                'strData &= "</tr>"
                'Response.Write(strData)
            End While
            book.Save(lmdir)
            'open file
            Process.Start(lmdir)
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



    Public Sub exportexcelDataqry2(ByVal mquery As String, ByVal mquery1 As String, ByVal head1 As String)
        Dim mstrr As String
        Dim strData As String = ""
        Dim bolFirstPass As Boolean = True

        Dim ldir, lmdir As String
        ldir = System.AppDomain.CurrentDomain.BaseDirectory()
        lmdir = Trim(ldir) & "Incentive_Fine.xls"

        Dim book As Workbook = New Workbook
        'Dim oCn As SqlConnection = Nothing
        Dim oCmd As SqlCommand = Nothing
        Dim oDr As SqlDataReader = Nothing
        Dim oCmd1 As SqlCommand = Nothing
        Dim oDr1 As SqlDataReader = Nothing

        'Try
        'oCn = New SqlConnection("YOUR CONNECTION STRING ")
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        mstrr = mquery

        oCmd = New SqlCommand(mstrr, con)
        Dim sheet As Worksheet = book.Worksheets.Add("SampleSheet")
        oDr = oCmd.ExecuteReader(CommandBehavior.CloseConnection)

        Dim Row5 As WorksheetRow = sheet.Table.Rows.Add
        Dim style2 As WorksheetStyle = book.Styles.Add("Header23")
        'style2 = book.Styles.Add("Header23")
        style2.Alignment.Vertical = StyleVerticalAlignment.Center
        style2.Alignment.Horizontal = StyleHorizontalAlignment.Center

        style2.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style2.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        Dim cell1 As WorksheetCell = Row5.Cells.Add("ATITHYA CLOTHING COMPANY")
        cell1.MergeAcross = 14
        cell1.StyleID = "Header23"
        'cell1.StyleID = "Header11"
        Dim Row6 As WorksheetRow = sheet.Table.Rows.Add
        Dim cell2 As WorksheetCell = Row6.Cells.Add(Trim(head1))
        cell2.MergeAcross = 14
        cell2.StyleID = "Header23"


        Dim Row1 As WorksheetRow = sheet.Table.Rows.Add

        For i As Integer = 0 To oDr.FieldCount - 1
            'oDr.GetName(i)
            Row1.Cells.Add(oDr.GetName(i))
        Next
        While oDr.Read
            Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
            For i As Integer = 0 To oDr.FieldCount - 1
                'oDr.GetName(i)
                Row0.Cells.Add(oDr.Item(oDr.GetName(i)))
            Next
        End While

        If Len(Trim(mquery1)) > 0 Then
            oCmd1 = New SqlCommand(mquery1, con)
            Dim sheet1 As Worksheet = book.Worksheets.Add("Consolidated Fine")
            oDr1 = oCmd1.ExecuteReader(CommandBehavior.CloseConnection)


            Dim Row7 As WorksheetRow = sheet1.Table.Rows.Add

            Dim style3 As WorksheetStyle = book.Styles.Add("Header21")
            'style2 = book.Styles.Add("Header23")
            style3.Alignment.Vertical = StyleVerticalAlignment.Center
            style3.Alignment.Horizontal = StyleHorizontalAlignment.Center

            Dim cell3 As WorksheetCell = Row7.Cells.Add("ATITHYA CLOTHING COMPANY")
            cell3.MergeAcross = 3
            cell3.StyleID = "Header21"
            'cell1.StyleID = "Header11"
            Dim Row8 As WorksheetRow = sheet1.Table.Rows.Add
            Dim cell4 As WorksheetCell = Row8.Cells.Add(Trim(head1))
            cell4.MergeAcross = 3
            cell4.StyleID = "Header21"



            Dim Row2 As WorksheetRow = sheet1.Table.Rows.Add
            Dim style4 As WorksheetStyle = book.Styles.Add("Header12")
            style4.Alignment.Vertical = StyleVerticalAlignment.Top
            style4.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
            style4.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
            style4.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
            style4.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

            For i As Integer = 0 To oDr1.FieldCount - 1
                'oDr.GetName(i)
                Row2.Cells.Add(oDr1.GetName(i))
            Next
            While oDr1.Read
                'Dim Row3 As WorksheetRow = sheet1.Table.Rows.Add
                Row2 = sheet1.Table.Rows.Add
                For i As Integer = 0 To oDr1.FieldCount - 1
                    'oDr.GetName(i)
                    'oDr1.GetString(i)
                    Row2.Cells.Add(oDr1.Item(oDr1.GetName(i)))
                    'If oDr1.GetFieldType(i).Name = "String" Then
                    '    Row2.Cells.Add(oDr1.Item(oDr1.GetName(i)), DataType.String, "style4")
                    'ElseIf oDr1.GetFieldType(i).Name = "Int32" Or oDr1.GetFieldType(i).Name = "Int16" Then
                    '    Row2.Cells.Add(oDr1.Item(oDr1.GetName(i)), DataType.Integer, "style4")
                    'ElseIf oDr1.GetFieldType(i).Name = "Double" Or oDr1.GetFieldType(i).Name = "Float" Then
                    '    Row2.Cells.Add(oDr1.Item(oDr1.GetName(i)), DataType.Number, "style4")
                    'End If
                Next
            End While
        End If


        book.Save(lmdir)
        'open file
        Process.Start(lmdir)

        'Catch ex As Exception
        '    If Not ex.Message.Contains("Thread was being aborted.") Then
        '        '_'global.WriteErrorLog("exportData.loadPage() failed! Error is: " & ex.Message)
        '        MsgBox(ex.Message)
        '    End If
        'Finally
        '    If Not con Is Nothing Then If con.State = ConnectionState.Open Then con.Close()
        '    oDr.Close()
        'End Try
    End Sub



    Public Sub colostockexcel(ByVal ctrl As AxMSFlexGrid, Optional ByVal head1 As String = "", Optional ByVal head2 As String = "")
        Dim mstrr As String
        Dim strData As String = ""
        Dim bolFirstPass As Boolean = True

        Dim ldir, lmdir As String
        ldir = System.AppDomain.CurrentDomain.BaseDirectory()
        lmdir = Trim(ldir) & "ColorStock.xls"

        Dim book As Workbook = New Workbook
        'Dim oCn As SqlConnection = Nothing
        'Dim oCmd As OleDbCommand = Nothing
        'Dim oDr As OleDbDataReader = Nothing


        Try
            'oCn = New SqlConnection("YOUR CONNECTION STRING ")
            'If con.State = ConnectionState.Closed Then
            'con.Open()
            ' End If
            'mstrr = "select * from bardet"
            'If Len(Trim(mwhere)) > 0 Then
            '    mstrr = mstrr & " Where " & mwhere
            'End If
            'If Len(Trim(morder)) > 0 Then
            '    mstrr = mstrr & " order by " & morder
            'End If
            'mstrr = mquery

            'oCmd = New OleDbCommand(mstrr, con)
            'Dim da As SqlDataAdapter = New SqlDataAdapter(oCmd)
            'Dim dt As DataTable = New DataTable()
            'da.Fill(dt)
            'DatatableToExcel(dt)
            Dim sheet As Worksheet = book.Worksheets.Add("SampleSheet")
            'oDr = oCmd.ExecuteReader(CommandBehavior.CloseConnection)
            'For i As Integer = 0 To oDr.FieldCount - 1
            '    'oDr.GetName(i)

            Dim styl As WorksheetStyle = book.Styles.Add("style0")
            styl.Font.Bold = True
            styl.Font.Size = 14
            'style.Alignment.Vertical = StyleVerticalAlignment.Center
            styl.Alignment.Horizontal = StyleHorizontalAlignment.Center

            Dim Row2 As WorksheetRow = sheet.Table.Rows.Add

            Row2.Cells.Add(" ")
            Row2.Cells.Add(" ")
            'Row2.Cells.Add(head1, DataType.String, "style0")
            Dim cell As WorksheetCell = Row2.Cells.Add(head1)
            cell.MergeAcross = 12         ' // Merge two cells togethe
            cell.StyleID = "style0"


            Dim style As WorksheetStyle = book.Styles.Add("style1")
            style.Font.Bold = True
            style.Font.Size = 12
            'style.Alignment.Vertical = StyleVerticalAlignment.Center
            style.Alignment.Horizontal = StyleHorizontalAlignment.Center

            Dim Row3 As WorksheetRow = sheet.Table.Rows.Add
            Row3.Cells.Add(" ")
            Row3.Cells.Add(" ")
            'Row3.Cells.Add(head2, DataType.String, "style1")
            Dim cell1 As WorksheetCell = Row3.Cells.Add(head2)
            cell1.MergeAcross = 12         ' // Merge two cells togethe
            cell1.StyleID = "style1"

            Dim Row1 As WorksheetRow = sheet.Table.Rows.Add
            Row1.Cells.Add("Item Name", DataType.String, "style1")
            Row1.Cells.Add("Style", DataType.String, "style1")
            Row1.Cells.Add("btno", DataType.String, "style1")
            Row1.Cells.Add("Batchnum", DataType.String, "style1")
            Row1.Cells.Add("34", DataType.String, "style1")
            Row1.Cells.Add("36", DataType.String, "style1")
            Row1.Cells.Add("38", DataType.String, "style1")
            Row1.Cells.Add("40", DataType.String, "style1")
            Row1.Cells.Add("42", DataType.String, "style1")
            Row1.Cells.Add("44", DataType.String, "style1")
            Row1.Cells.Add("46", DataType.String, "style1")
            Row1.Cells.Add("48", DataType.String, "style1")
            Row1.Cells.Add("50", DataType.String, "style1")
            Row1.Cells.Add("52", DataType.String, "style1")
            Row1.Cells.Add("Total", DataType.String, "style1")

            '    'strData &= "<td>" & Replace(Replace(oDr.GetName(i), "[", ""), "]", "") & "</td>"
            'Next


            Dim style2 As WorksheetStyle = book.Styles.Add("style2")
            style2.Font.Bold = False
            style2.Font.Size = 10
            'style.Alignment.Vertical = StyleVerticalAlignment.Center
            'style2.Alignment.Horizontal = StyleHorizontalAlignment.Center

            For I = 1 To ctrl.Rows - 1
                If Len(Trim(ctrl.get_TextMatrix(I, 0))) > 0 Then
                    Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
                    For j As Integer = 1 To ctrl.Cols - 1
                        'oDr.GetName(i)
                        If j >= 5 Then
                            Row0.Cells.Add(ctrl.get_TextMatrix(I, j), DataType.Number, "style2")
                        Else
                            Row0.Cells.Add(ctrl.get_TextMatrix(I, j), DataType.String, "style2")
                        End If
                        'Row0.Cells.Add(ctrl.get_TextMatrix(I, j))

                    Next
                End If

            Next I
            book.Save(lmdir)
            ''open file
            'Process.Start(lmdir)

        Catch ex As Exception
            If Not ex.Message.Contains("Thread was being aborted.") Then
                '_'global.WriteErrorLog("exportData.loadPage() failed! Error is: " & ex.Message)
                MsgBox(ex.Message)
            End If
        Finally
            'If Not con Is Nothing Then If con.State = ConnectionState.Open Then con.Close()
            'oDr.Close()
        End Try
    End Sub

    Public Function nosunday(ByVal m As Integer, ByVal Y As Integer) As Integer
        Dim mstr As String
        Dim nk As Integer
        nk = TotalMonthDays(m, Y)
        For i = 1 To nk
            mstr = Format((str(i) + str(m) + str(Y)), "dddd")
            If UCase(mstr) = "SUNDAY" Then
                nosunday = nosunday + 1
            End If
        Next i
    End Function
    Public Function TotalMonthDays(ByVal m As Integer, ByVal yr As Integer) As Integer


        Select Case m
            Case 1, 3, 5, 7, 8, 10, 12 : TotalMonthDays = 31
            Case 2


                Select Case LeapYear(yr)
                    Case 1 'leap year
                        TotalMonthDays = 29
                    Case 0 'Non leap year
                        TotalMonthDays = 28
                End Select
            Case 4, 6, 9, 11 : TotalMonthDays = 30
        End Select
    End Function

    Public Function LeapYear(ByVal yr As Integer) As Integer
        '=======================================
        '     ==
        'Find if the yr is leap or not
        'Return value 1 = Leap Year
        'Return value 0 = not Leap Year
        '=======================================
        '     ==


        Select Case yr Mod 4
            Case 0 'Divide 4


                Select Case yr Mod 100
                    Case 0 'is century


                        Select Case yr Mod 400
                            Case 0 : LeapYear = 1
                            Case Else : LeapYear = 0
                        End Select
                    Case Else 'is not century
                        LeapYear = 1
                End Select
            Case Else 'not Divide 4
                LeapYear = 0
        End Select
    End Function


    Public Function getDataReader(ByVal SQL As String) As SqlDataReader
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim cmd As New SqlCommand(SQL, con)
        Dim dr As SqlDataReader
        'dr = cmd.ExecuteReader
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Return dr

    End Function

    Public Function getDataTableold(ByVal SQL As String) As DataTable
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim cmd As New SqlCommand(SQL, con)

        Dim table As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        cmd.CommandTimeout = 600
        da.Fill(table)
        Return table

    End Function

    Public Function getDataTable(ByVal SQL As String) As DataTable
        Dim table As New DataTable

        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(SQL, con)
                cmd.CommandTimeout = 600
                con.Open()

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(table)
                End Using
            End Using
        End Using

        Return table
    End Function


    Public Sub executeQuery(ByVal SQL As String)
        merr = ""

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If


        trans = con.BeginTransaction

        Dim cmd As New SqlCommand(SQL, con, trans)

        Try
            cmd.ExecuteNonQuery()
            trans.Commit()
            merr2 = "Saved!"
        Catch ex As Exception
            'If InStr(merr, "PRIMARY KEY") > 0 Then

            'End If
            merr = Trim(ex.Message)
            trans.Rollback()
            MsgBox(ex.Message)
        End Try

    End Sub


    Public Sub ngridexcelexport(ByVal CTRL As DataGridView, ByVal lastcol As Integer, Optional ByVal head1 As String = "", Optional ByVal head2 As String = "")


        Dim ldir, lmdir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\barcodadd.txt"
        If mos = "WIN" Then
            ldir = System.AppDomain.CurrentDomain.BaseDirectory()
            lmdir = Trim(ldir) & "Perf Report.xls"
        Else
            lmdir = mxlfilepath & "Perf Report.xls"
        End If


        Dim ticks As Integer = Environment.TickCount
        ' Create the workbook
        Dim book As Workbook = New Workbook
        ' Set the author
        book.Properties.Author = "CarlosAg"

        ' Add some style
        Dim style As WorksheetStyle = book.Styles.Add("Header1")
        style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style.Font.Bold = True
        style.Font.Size = 14
        style.Alignment.Vertical = StyleVerticalAlignment.Center

        Dim sheet As Worksheet = book.Worksheets.Add("SampleSheet")
        'Dim style2 As WorksheetStyle = book.Styles.Add("style2")

        'style2.Font.Bold = False

        Dim Row As WorksheetRow = sheet.Table.Rows.Add
        style.Font.Bold = True
        style.Font.Size = 14
        'styleh.Alignment.Vertical = StyleVerticalAlignment.Center
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Row.Cells.Add(head1, DataType.String, "Header1")

        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        Dim cell As WorksheetCell = Row.Cells.Add(head1)
        cell.MergeAcross = 22
        cell.StyleID = "Header1"

        'Dim stylh2 As WorksheetStyle = book.Styles.Add("Header2")
        'Dim Rh2 As WorksheetRow = sheet.Table.Rows.Add
        Row = sheet.Table.Rows.Add
        style = book.Styles.Add("Header2")
        style.Alignment.Vertical = StyleVerticalAlignment.Center
        style.Alignment.Horizontal = StyleHorizontalAlignment.Center

        style.Font.Bold = True
        style.Font.Size = 12
        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        cell = Row.Cells.Add(head2)
        cell.MergeAcross = 22
        cell.StyleID = "Header2"
        'style.Alignment.Vertical = StyleVerticalAlignment.Center
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Rh1.Cells.Add("  ", DataType.String, "style")
        'Row.Cells.Add(head2, DataType.String, "Header2")
        'Row3.Cells.Add("Absent", DataType.String, "style1")


        ' Add some style
        'Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
        Row = sheet.Table.Rows.Add
        style = book.Styles.Add("style1")
        style.Alignment.Vertical = StyleVerticalAlignment.Center
        style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style.Font.Bold = True
        style.Font.Size = 12
        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
        'style.Alignment.Vertical = StyleVerticalAlignment.Center


        'Export Header Names Start
        Dim columnsCount As Integer = CTRL.Columns.Count

        'Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
        'Dim column As Integer = 0
        Dim column = CTRL.Columns.Count
        'Do Until column = columnsCount
        'For Each column In CTRL.Columns
        style.Font.Bold = True
        For iC As Integer = 0 To column - lastcol
            '- lastcol
            'Do Until column = columnsCount - lastcol
            Row.Cells.Add(CTRL.Columns(iC).HeaderText, DataType.String, "style1")

            'Row2.Cells.Add(column.Name, DataType.String, "style1")
            'Worksheet.Cells(1, column.Index + 1).Value = column.Name
        Next
        'Export Header Name End
        'Dim style3 As WorksheetStyle = book.Styles.Add("style1")
        style = book.Styles.Add("style2")
        style.Font.Bold = False
        style.Font.Size = 10
        style.Alignment.Vertical = StyleVerticalAlignment.Top
        'style.Alignment.Horizontal = StyleHorizontalAlignment.Center
        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)

        'Export Each Row Start
        For i As Integer = 0 To CTRL.Rows.Count - 1
            'Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
            Row = sheet.Table.Rows.Add
            Dim columnIndex As Integer = 0
            Do Until columnIndex = columnsCount - (lastcol - 1)

                Row.Cells.Add(CTRL.Item(columnIndex, i).Value.ToString, DataType.String, "style2")
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

        'Console.WriteLine("Time:{0}", (Environment.TickCount - ticks))
    End Sub

    Public Sub loadrptdb3(ByVal reportdoccode As String, ByVal mpath As String)
        Dim filename As String
        Try
            'Dim crReportDocument As ReportDocument = Nothing
            ' Dim filename As String = String.Empty
            'Create a new SQL Connection to the database
            'Dim sqlCon As SqlConnection = New SqlConnection("Server=server;Database=db;uid=uis;pwd=pass")
            'Create a command object for reading document from Database
            'Dim sqlCom As OleDbCommand = New OleDbCommand("    Select rd_image from REPORT_DETAIL Where report_id=1", Con)

            Dim sqlCom As SqlCommand = New SqlCommand("select docname,Template from rdoc where Doccode='" & Trim(reportdoccode) & "'", con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            'Read data into DataReader
            Dim sqlDr As SqlDataReader = sqlCom.ExecuteReader

            While sqlDr.Read

                filename = (Trim(mpath) & "\" & Trim(sqlDr("docname")) & ".rpt")

                Dim fileBytes() As Byte = CType(sqlDr("template"), Byte())
                Dim oFileStream As FileStream = New FileStream(filename, FileMode.Create)
                'SaveFile
                oFileStream.Write(fileBytes, 0, fileBytes.Length)
                'Dispose the memory of used objects
                oFileStream.Close()
                oFileStream.Dispose()
                fileBytes = Nothing

            End While
            'Close Reader and Connection
            sqlDr.Close()

            'con.Close()
            'Dispose Memory
            sqlDr.Dispose()
            'con.Dispose()
            sqlCom.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'Return filename
    End Sub
    Public Function loadrptdb2(ByVal reportname As String, ByVal mpath As String) As String
        Dim filename As String
        Try

            Dim sqlCom As SqlCommand = New SqlCommand("select Template from rdoc where Doccode='" & Trim(reportname) & "'", con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            Dim sqlDr As SqlDataReader = sqlCom.ExecuteReader

            While sqlDr.Read

                filename = (Trim(mpath) & "\" & Trim(reportname) & ".rpt")

                Dim fileBytes() As Byte = CType(sqlDr("template"), Byte())

                Dim oFileStream As FileStream = New FileStream(filename, FileMode.Create)
                'SaveFile
                oFileStream.Write(fileBytes, 0, fileBytes.Length)
                'Dispose the memory of used objects
                oFileStream.Close()
                oFileStream.Dispose()
                fileBytes = Nothing

            End While
            'Close Reader and Connection
            sqlDr.Close()

            'con.Close()
            'Dispose Memory
            sqlDr.Dispose()
            'con.Dispose()
            sqlCom.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return filename
    End Function


    Public Function SaveReportInDB2(ByVal filepathname As String, ByVal msql As String, Optional ByVal repname As String = "") As Boolean
        Dim IsReportSavedInDB As Boolean = False
        Try

            Dim fs As FileStream = New FileStream(filepathname, FileMode.Open, FileAccess.Read)
            Dim br As BinaryReader = New BinaryReader(fs)
            Dim bytes As Byte() = br.ReadBytes(Convert.ToInt32(fs.Length))
            br.Close()
            fs.Close()



            Dim memStream As New MemoryStream
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If


            Dim myCommand As SqlCommand = New SqlCommand(msql, con)
            If Len(Trim(repname)) > 0 Then
                myCommand.Parameters.AddWithValue("@repname", repname)
            End If
            myCommand.Parameters.AddWithValue("@repdoc", bytes)

            myCommand.ExecuteNonQuery()
            IsReportSavedInDB = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return IsReportSavedInDB
    End Function
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


    Public Sub gridexcelexport3(ByVal CTRL As DataGridView, ByVal lastcol As Integer)


        Dim ldir, lmdir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\barcodadd.txt"

        ldir = System.AppDomain.CurrentDomain.BaseDirectory()
        lmdir = Trim(ldir) & "Perfrep.xls"

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

        'Export Header Names Start
        Dim columnsCount As Integer = CTRL.Columns.Count

        Dim Row As WorksheetRow = sheet.Table.Rows.Add
        'Dim column As Integer = 0
        Dim column = CTRL.Columns.Count
        'Do Until column = columnsCount
        'For Each column In CTRL.Columns
        style.Font.Bold = True
        For iC As Integer = 0 To column - lastcol
            'Do Until column = columnsCount - lastcol
            Row.Cells.Add(CTRL.Columns(iC).HeaderText, DataType.String, "style1")

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
            Do Until columnIndex = columnsCount - 1
                Row.Cells.Add(CTRL.Item(columnIndex, i).Value.ToString & vbNullString, DataType.String, "style2")
                'Row2.Cells(i + 2, columnIndex + 1).Value = CTRL.Item(columnIndex, i).Value.ToString
                columnIndex += 1
            Loop
        Next



        'For I = 0 To CTRL.Rows - 1

        '    Dim Row0 As WorksheetRow = sheet.Table.Rows.Add

        '    For J = 0 To CTRL.Cols - 1

        '        ' Add a cell
        '        'Row0.Cells.Add("Hello World", DataType.String, "style1")
        '        Row0.Cells.Add(CTRL.get_TextMatrix(I, J), DataType.String, "style1")
        '    Next J
        '    If I = 0 Then
        '        'Dim style As WorksheetStyle = book.Styles.Add("style1")
        '        style.Font.Bold = True
        '    Else
        '        style.Font.Bold = False
        '        style.Font.Size = 10
        '        style.Alignment.Vertical = StyleVerticalAlignment.Top
        '        'style.Alignment.Horizontal = StyleHorizontalAlignment.Justify
        '    End If


        'Next I

        ' Save it
        'book.Save("c:\test.xls")
        book.Save(lmdir)
        'open file
        Process.Start(lmdir)
        'Console.WriteLine("Time:{0}", (Environment.TickCount - ticks))
    End Sub

    Public Function WriteJason(ByVal dt As DataTable, ByVal path As String) As Boolean
        'Try
        '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer
        '    ' Dim rows As List(Of Dictionary) = New List(Of Dictionary)
        '    Dim rows As New List(Of Dictionary(Of String, String))
        '    '= New List(Of Dictionary)
        '    Dim row As Dictionary(Of String, String) = Nothing
        '    For Each dr As DataRow In dt.Rows
        '        row = New Dictionary(Of String, String)
        '        For Each col As DataColumn In dt.Columns
        '            row.Add(col.ColumnName.Trim.ToString, Convert.ToString(dr(col)))
        '        Next
        '        rows.Add(row)
        '    Next
        '    Dim jsonstring As String = serializer.Serialize(rows)
        '    Dim file = New StreamWriter(path, False)
        '    file.Write(jsonstring)
        '    file.Close()
        '    file.Dispose()
        '    Return True
        'Catch ex As System.Exception
        '    Return False
        'End Try
    End Function

    Public Sub gridexcelexport4(ByVal CTRL As DataGridView, ByVal lastcol As Integer, ByVal fname As String, ByVal mhead As String)


        Dim ldir, lmdir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\barcodadd.txt"

        If mos = "WIN" Then
            ldir = System.AppDomain.CurrentDomain.BaseDirectory()
            lmdir = Trim(ldir) & Trim(fname) & ".xls"
        Else

            lmdir = mxlfilepath & Trim(fname) & ".xls"
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
        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)

        Dim sheet As Worksheet = book.Worksheets.Add("SampleSheet")
        'Dim style2 As WorksheetStyle = book.Styles.Add("style2")

        style = book.Styles.Add("merge")
        style.Font.Bold = True
        style.Font.Size = 13
        style.Alignment.Horizontal = StyleHorizontalAlignment.Center

        'style2.Font.Bold = False
        If Len(Trim(mhead)) > 0 Then
            Dim Row2 As WorksheetRow = sheet.Table.Rows.Add
            'Row2.Cells.Add(mhead, DataType.String, "merge")
            Dim cell As WorksheetCell = Row2.Cells.Add(mhead)
            cell.MergeAcross = CTRL.ColumnCount - 1
            cell.StyleID = "merge"
        End If

        'Export Header Names Start
        Dim columnsCount As Integer = CTRL.Columns.Count - 1

        Dim Row As WorksheetRow = sheet.Table.Rows.Add
        'Dim column As Integer = 0
        Dim column = CTRL.Columns.Count
        style.Font.Bold = True



        For iC As Integer = 0 To column - 1
            '- lastcol
            'Do Until column = columnsCount - lastcol
            Row.Cells.Add(CTRL.Columns(iC).HeaderText & vbNullString, DataType.String, "style1")


        Next
        'Export Header Name End
        style = book.Styles.Add("style2")
        style.Font.Bold = False
        style.Font.Size = 10
        style.Alignment.Vertical = StyleVerticalAlignment.Top
        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)

        style = book.Styles.Add("numstyle")
        style.Font.Bold = False
        style.Font.Size = 10
        style.Alignment.Horizontal = StyleHorizontalAlignment.Right
        style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1)
        style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1)

        'Export Each Row Start
        For i As Integer = 0 To CTRL.Rows.Count - 1
            'Dim Row0 As WorksheetRow = sheet.Table.Rows.Add
            Row = sheet.Table.Rows.Add
            ' Dim columnIndex As Integer = 0
            'Do Until columnIndex = columnsCount
            For columnIndex As Integer = 0 To column - 1
                '- (lastcol - 1)
                'Row.Cells.Add(CTRL.Item(columnIndex, i).Value.ToString & vbNullString, DataType.String, "style2")

                If CTRL.Item(columnIndex, i).ValueType.ToString = "System.String" Then
                    Row.Cells.Add(IIf(IsDBNull(CTRL.Item(columnIndex, i).Value) = True, "", CTRL.Item(columnIndex, i).Value), DataType.String, "style2")
                    'Row.Cells.Add((CTRL.Item(columnIndex, i).Value.ToString & vbNullString), DataType.String, "style2")
                ElseIf CTRL.Item(columnIndex, i).ValueType.ToString = "System.Decimal" Then
                    'Row.Cells.Add(Microsoft.VisualBasic.Format(Val(CTRL.Item(columnIndex, i).Value.ToString), "########0"), DataType.Number, "numstyle")
                    Row.Cells.Add(IIf(IsDBNull(CTRL.Item(columnIndex, i).Value) = True, 0, CTRL.Item(columnIndex, i).Value), DataType.Number, "numstyle")
                ElseIf CTRL.Item(columnIndex, i).ValueType.ToString = "System.DateTime" Then
                    'Row.Cells.Add(CTRL.Item(columnIndex, i).Value.ToString & vbNullString, DataType.String, "style2")
                    If Len(Trim(IIf(IsDBNull(CTRL.Item(columnIndex, i).Value) = True, "", CTRL.Item(columnIndex, i).Value))) > 0 Then
                        Row.Cells.Add(Microsoft.VisualBasic.Format(CDate(CTRL.Item(columnIndex, i).Value.ToString & vbNullString), "dd-MM-yyyy"), DataType.String, "style2")
                    End If

                Else
                    Row.Cells.Add(IIf(IsDBNull(CTRL.Item(columnIndex, i).Value) = True, "", CTRL.Item(columnIndex, i).Value), DataType.String, "style2")
                    'Row.Cells.Add((CTRL.Item(columnIndex, i).Value.ToString & vbNullString), DataType.String, "style2")
                End If
                'style.Interior.Color = System.Drawing.ColorTranslator.ToOle(CTRL.Rows(i).Cells(J).Style.BackColor)
                'style.Font.Color = System.Drawing.ColorTranslator.ToOle(CTRL.Rows(i).Cells(J).Style.ForeColor)
                'Row2.Cells(i + 2, columnIndex + 1).Value = CTRL.Item(columnIndex, i).Value.ToString
                'columnIndex += 1
            Next
            'Loop
        Next




        'book.Save("c:\test.xls")
        book.Save(lmdir)

        If mos = "WIN" Then
            'open file
            Process.Start(lmdir)
        Else
            OpenWithLibreOffice(lmdir)
        End If


        'Console.WriteLine("Time:{0}", (Environment.TickCount - ticks))
    End Sub

    Public Sub ReleaseComObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        End Try
    End Sub

    Public Function getnodaymonth(ByVal datTim1 As Date, ByVal datTim2 As Date) As Integer
        'Dim datTim1 As Date = Format(CDate(mskdatefr.Text), "dd-MM-yyyy")
        'Dim datTim2 As Date = Format(CDate(Mskdateto.Text), "dd-MM-yyyy")

        Dim nBetweenDayCnt As Integer = 0
        Dim i As Integer = 0
        Dim temp As DateTime

        While True

            temp = datTim1.AddDays(i)
            'AndAlso temp.DayOfWeek <> DayOfWeek.Saturday
            If temp.DayOfWeek <> DayOfWeek.Sunday Then
                nBetweenDayCnt += 1
            End If

            Dim Between As TimeSpan = datTim2 - temp

            If Between.Days <= 0 Then
                Exit While
            End If

            temp = datTim1.AddDays(i)
            i += 1
        End While
        Return nBetweenDayCnt.ToString()

    End Function
    Public Sub PasteCells(ByVal datagridv As DataGridView)
        Dim s As String = Clipboard.GetText()  ' Retrieve text from the clipboard
        Dim ci As Integer = datagridv.CurrentCell.ColumnIndex  ' Current column index
        Dim ri As Integer = datagridv.CurrentCell.RowIndex  ' Current row index
        Dim colCount As Integer = datagridv.Columns.Count  ' Total number of columns

        ' Split the clipboard text by new lines
        Dim rows() As String = s.Split({ControlChars.CrLf}, StringSplitOptions.None)

        ' Ensure the DataGridView has enough rows
        If datagridv.Rows.Count < ri + rows.Length Then
            For i As Integer = datagridv.Rows.Count To ri + rows.Length - 1
                datagridv.Rows.Add()
            Next
        End If

        ' Paste data into DataGridView
        For Each row As String In rows
            Dim cellIndex As Integer = ci  ' Initialize cell index for each row
            Dim cellValues() As String = row.Split({ControlChars.Tab}, StringSplitOptions.None)

            For Each cellValue As String In cellValues
                If cellIndex >= colCount Then Exit For ' Exit if the column index exceeds the column count
                If ri < datagridv.Rows.Count Then  ' Ensure the row index is within the row count
                    datagridv(cellIndex, ri).Value = cellValue  ' Set the cell value
                End If
                cellIndex += 1  ' Move to the next column
            Next

            ri += 1  ' Move to the next row
            If ri >= datagridv.Rows.Count Then Exit For ' Exit if the row index exceeds the row count
        Next
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
        If Not IO.File.Exists(filePath) Then
            MessageBox.Show("File not found")
            Exit Sub
        End If

        Dim psi As New ProcessStartInfo()
        psi.FileName = "cmd.exe"
        psi.Arguments = "/c libreoffice --calc """ & filePath & """"
        psi.CreateNoWindow = True
        psi.UseShellExecute = False

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
End Module
'usage cups print
'Dim success As Boolean = PrintPdfSilent("/home/user/reports/invoice.pdf")

'If success Then
'MessageBox.Show("PDF sent to printer")
'Else
'MessageBox.Show("Print failed")
'End If

' Print text report

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

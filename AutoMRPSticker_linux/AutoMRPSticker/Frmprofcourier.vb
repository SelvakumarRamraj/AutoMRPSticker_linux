Imports System.IO
Imports System.Math
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.VbStrConv
Imports System.Drawing.Printing
'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.ReportSource

Public Class Frmprofcourier
    Private WithEvents PD As New PrintDocument()
    Dim mcomp As String
    Dim msql, msql3, sql2 As String
    Dim mcompname As String
    Dim mbuilding As String
    Dim mblock As String
    Dim mstreetno As String
    Dim mcity As String
    Dim mpincode As String
    Dim mstate As String
    Dim mmobileno As String
    Dim mareacode As Boolean

    Dim tcompname As String
    Dim tbuilding As String
    Dim tblock As String
    Dim tstreetno As String
    Dim tcity As String
    Dim tpincode As String
    Dim minvt As Boolean
    Dim mkadd(6) As String
    Dim mlrow As Long
    Dim mkrow As Long
    Dim mlin As Int16
    Dim m, k, i, l, j As Int16
    Dim msql2, mdir As String
    Dim sql As String
    'Dim xcon As New OleDbConnection
    Dim mstart, mend As Int64
    Dim mprfx, mplace As String

    Dim mbuild As String

    Dim mstreet As String

    Dim mzipcode As String
    Dim mdistrict As String

    Dim mcountry As String
    Dim mto As String
    Dim mcardname As String
    Dim lpodno As String
    Dim mdocnum As Integer
    Dim mkwgt As Double = 0.0
    Private SaveEnabledList As New Dictionary(Of Integer, Boolean)
    Dim mrowno As Integer

    'Dim cryRpt As New ReportDocument()
    'Dim cryRpt2 As New ReportDocument()
    'Dim cryRptex As New ReportDocument()


    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblname.Click

    End Sub

    Private Sub Frmprofcourier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIParent1.Height
        Me.Width = My.Computer.Screen.Bounds.Width

        cmbcomp.Items.Add("RandR")
        cmbcomp.Items.Add("RHL")
        cmbcomp.Items.Add("ENES")
        cmbcomp.Items.Add("ATITHYA")
        cmbcomp.Items.Add("TCC")



        cmbver.Items.Add("97")
        cmbver.Items.Add("2000")
        cmbver.Items.Add("2003")
        cmbver.Items.Add("2007")
        cmbver.Items.Add("2010")
        cmbver.Items.Add("2012")

        'cmbver.Text = 2010

        Call loaddata()


        CMBYR.Text = mperiod
        'cmbcomp.Items.Add("ENES")
        mskdate.Text = Microsoft.VisualBasic.Format(Now(), "dd-MM-yyyy")
        minvt = False



        'cryRpt.Load(Trim(mreppath) & "courier.rpt")
        'CrystalReportLogOn(cryRpt, Trim(mkserver), dbnam, Trim(dbuser), Trim(mkpwd))



        'cryRpt2.Load(Trim(mreppath) & "couriercardcode.rpt")
        'CrystalReportLogOn(cryRpt2, Trim(mkserver), dbnam, Trim(dbuser), Trim(mkpwd))

        'regasm myTest.dll /tlb:myTest.tlb

    End Sub
    Private Sub loaddata()
        'msql = "select startno,endno,prefix,place from couriernomast"

        'Dim CMD3 As New sqlcommand(msql, con)
        'Dim DR3 As SqlDataReader
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        ''Dim DR3 As OleDb.OleDbDataReader
        'DR3 = CMD3.ExecuteReader
        'If DR3.HasRows = True Then
        '    While DR3.Read
        '        mstart = DR3.Item("startno")
        '        mend = DR3.Item("endno")
        '        mprfx = DR3.Item("prefix") & vbNullString
        '        mplace = DR3.Item("place") & vbNullString

        '    End While
        'End If
        'DR3.Close()
        'CMD3.Dispose()

        msql = "select startno,endno,prefix,place from couriernomast"
        Dim dt As DataTable = getDataTable(msql)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                mstart = rw("startno")
                mend = rw("endno")
                mprfx = rw("prefix") & vbNullString
                mplace = rw("place") & vbNullString
            Next
        End If

    End Sub

    Private Sub cmbcomp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbcomp.KeyPress
        e.Handled = True
    End Sub

    Private Sub cmbcomp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcomp.SelectedIndexChanged
        If Trim(cmbcomp.Text) = "RandR" Then
            mcomp = "RRLIVE"
            mareacode = True
        ElseIf Trim(cmbcomp.Text) = "RHL" Then
            mcomp = "RHLLIVE"
            mareacode = True
        ElseIf Trim(cmbcomp.Text) = "ENES" Then
            mcomp = "ENESLIVE"
            mareacode = False
        ElseIf Trim(cmbcomp.Text) = "ATITHYA" Then
            mcomp = "ANTSPRODLIVE"
            mareacode = False
        ElseIf Trim(cmbcomp.Text) = "TCC" Then
            mcomp = "TCCLIVE"
            mareacode = False
        ElseIf Trim(cmbcomp.Text) = "ACM" Then
            mcomp = "ACMLIVE"
            mareacode = False
        End If

        If Len(Trim(mdbname)) > 0 Then
            mcomp = mdbname
            If Microsoft.VisualBasic.Left(mcomp, 2) = "RR" Or Microsoft.VisualBasic.Left(mcomp, 2) = "RH" Or Microsoft.VisualBasic.Left(mcomp, 2) = "BB" Then
                mareacode = True
            Else
                mareacode = False

            End If
        End If

        Call loadstate()
        If mareacode = True Then
            Call loadagent()
        End If
        Call loadcity()
        Call loaddistrict()
        Call LOADYR()

    End Sub

    Private Sub txtcardcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcardcode.KeyDown

    End Sub

    Private Sub txtcardcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcardcode.KeyPress

        If Asc(e.KeyChar) = 13 Then
            minvt = False
            'txt1.Focus()
            txtpodno.Focus()
            'Call addprn()
        End If
    End Sub

    Private Sub txtcardcode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcardcode.LostFocus
        'If Len(Trim(txtpodno.Text)) > 0 Then
        '    If Len(Trim(txtcardcode.Text)) > 0 Then
        '        Call podsave()
        '    End If
        'Else
        If chksearch.Checked = False Then
            If chkexcel.Checked = True Then
                'cryRptex.Load(Trim(mreppath) & "couriercardcodeEX.rpt")
                'CrystalReportLogOn(cryRptex, Trim(mkserver), dbnam, Trim(dbuser), Trim(mkpwd))
                Call addprnex()
            Else
                Call addprn()
            End If
        End If
        If chkprn.Checked = False Then
            Call AUTONO()
        Else
            'Call crystal()
        End If

        'End If
    End Sub


    Private Sub txtcardcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcardcode.TextChanged
        ' Call addprn()
        'If Len(Trim(txtcardcode.Text)) > 2 Then
        'Call addprn()
        'End If
    End Sub

    Private Sub LOADYR()

        msql = "SELECT CATEGORY FROM " & Trim(mcomp) & ".dbo.OFPR GROUP BY category ORDER BY category"

        Dim CMD As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        CMBYR.Items.Clear()
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()

        Try
            ''Dim DR As SqlDataReader
            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                While DR.Read

                    CMBYR.Items.Add(DR.Item("CATEGORY"))

                End While

            End If
            DR.Close()
        Catch EX As Exception

            MsgBox(EX.Message)

        End Try

    End Sub

    Private Sub addprn()

        Dim dir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\barcodadd.txt"

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "barcodadd.txt"

        If Len(Trim(mcomp)) > 0 Then
            'msql = "select docentry,comp,group1 as type,group2 as prntype,printon ,stickercol,labrow,labcol,printer from barhead where active=1"

            If Val(txtno.Text) > 0 Or Len(Trim(txtcardcode.Text)) > 0 Then
                If Val(txtno.Text) > 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                     & " isnull(t1.zipcode,'') as zipcode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                      & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where t0.cardcode in (select b.cardcode from " & Trim(mcomp) & ".dbo.oinv b LEFT join " & Trim(mcomp) & ".dbo.ofpr C ON C.AbsEntry=B.FinncPriod  where b.docnum=" & Val(txtno.Text) & " and c.category='" & CMBYR.Text & "')"

                ElseIf Len(Trim(txtcardcode.Text)) > 0 Then

                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                         & " isnull(t1.zipcode,'') as zipcode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                          & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where t0.cardcode='" & Trim(txtcardcode.Text) & "'"
                End If


                msql2 = "select b.docdate from " & Trim(mcomp) & ".dbo.oinv b LEFT join " & Trim(mcomp) & ".dbo.ofpr C ON C.AbsEntry=B.FinncPriod  where b.docnum=" & Val(txtno.Text) & " and c.category='" & CMBYR.Text & "')"
                ' msql2 = "select b.docdate from " & Trim(mcomp) & ".dbo.oinv b LEFT join " & Trim(mcomp) & ".dbo.ofpr C ON C.AbsEntry=B.FinncPriod  where b.docnum=" & Val(txtno.Text) & " and c.indicator='" & CMBYR.Text & "')"


                Dim CMD As New SqlCommand(msql, con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
                'trans.Begin()

                Try
                    ''Dim DR As SqlDataReader
                    Dim DR As SqlDataReader
                    DR = CMD.ExecuteReader
                    If DR.HasRows = True Then
                        While DR.Read
                            lblname.Text = Replace(DR.Item("cardfname") & vbNullString, "'", "`")
                            mcompname = DR.Item("cardfname") & vbNullString
                            mbuilding = DR.Item("building") & vbNullString
                            mblock = DR.Item("block") & vbNullString
                            mstreetno = DR.Item("street") & vbNullString
                            mcity = DR.Item("city") & vbNullString
                            mpincode = DR.Item("zipcode") & vbNullString
                        End While

                    End If
                    DR.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try

                CMD.Dispose()


                msql2 = "select t0.code,t0.compnyname,t0.printheadr,isnull(building,'') as building,isnull(block,'') as block,isnull(streetno,'') as streetno,isnull(street,'') as street," & vbCrLf _
                        & " isnull(city,'') as city,isnull(zipcode,'') as zipcode from " & Trim(mcomp) & ".dbo.oadm t0" & vbCrLf _
                        & "left join " & Trim(mcomp) & ".dbo.ADM1 t1 on t1.Code=t0.Code"


                Dim CMD2 As New SqlCommand(msql2, con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
                'trans.Begin()

                Try
                    ''Dim DR As SqlDataReader
                    Dim DR1 As SqlDataReader
                    DR1 = CMD2.ExecuteReader
                    If DR1.HasRows = True Then
                        While DR1.Read
                            tcompname = DR1.Item("printheadr") & vbNullString
                            tbuilding = DR1.Item("building") & vbNullString
                            tblock = DR1.Item("block") & vbNullString
                            tstreetno = Trim(DR1.Item("streetno")) & vbNullString & "," & Trim(DR1.Item("street")) & vbNullString
                            tcity = Trim(DR1.Item("city")) & vbNullString
                            tpincode = Trim(DR1.Item("zipcode")) & vbNullString
                        End While

                    End If
                    DR1.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try

                CMD.Dispose()

                msql2 = "select b.docdate,docnum,docentry from " & Trim(mcomp) & ".dbo.oinv b LEFT join " & Trim(mcomp) & ".dbo.ofpr C ON C.AbsEntry=B.FinncPriod  where b.docnum=" & Val(txtno.Text) & " and c.category='" & CMBYR.Text & "'"
                'msql2 = "select b.docdate,docnum,docentry from " & Trim(mcomp) & ".dbo.oinv b LEFT join " & Trim(mcomp) & ".dbo.ofpr C ON C.AbsEntry=B.FinncPriod  where b.docnum=" & Val(txtno.Text) & " and c.indicator='" & CMBYR.Text & "'"


                Dim CMD4 As New SqlCommand(msql2, con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
                'trans.Begin()

                Try
                    ''Dim DR As SqlDataReader
                    Dim DR As SqlDataReader
                    DR = CMD4.ExecuteReader
                    If DR.HasRows = True Then
                        While DR.Read
                            txtdocentry.Text = DR.Item("docentry")
                        End While
                    End If
                    DR.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                CMD4.Dispose()






                ''C:\Documents and Settings\postgres\My Documents

                ''If chkdirprn.Checked = True Then
                ''FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
                ''Else
                'If Len(Trim(mbuilding)) > 0 Or Len(Trim(mcity)) > 0 Then

                '    FileOpen(1, mdir, OpenMode.Output)
                '    'End If

                '    'FileOpen(1, mdir, OpenMode.Output)
                '    mlin = 1
                '    PrintLine(1, TAB(0), " ")
                '    mlin = mlin + 1

                '    Print(1, TAB(31), Microsoft.VisualBasic.Format(Now(), "dd-MM-yyyy"))
                '    PrintLine(1, TAB(54), mcity)
                '    mlin = mlin + 1
                '    m = 4 - mlin
                '    For k = 1 To m
                '        PrintLine(1, " ")
                '    Next

                '    Print(1, TAB(7), tcompname)
                '    PrintLine(1, TAB(44), mcompname)
                '    mlin = mlin + 1
                '    Print(1, TAB(7), tbuilding)
                '    PrintLine(1, TAB(44), mbuilding)
                '    mlin = mlin + 1
                '    Print(1, TAB(7), tblock)
                '    PrintLine(1, TAB(44), mblock)
                '    mlin = mlin + 1
                '    Print(1, TAB(7), tstreetno)
                '    PrintLine(1, TAB(44), mstreetno)
                '    mlin = mlin + 1
                '    Print(1, TAB(7), Trim(tcity) & " - " & Trim(tpincode))
                '    PrintLine(1, TAB(44), Trim(mcity) & " -" & Trim(mpincode))
                '    mlin = mlin + 1
                '    PrintLine(1, TAB(5), "")
                '    mlin = mlin + 1
                '    Print(1, TAB(13), "TPR")
                '    Print(1, TAB(23), Microsoft.VisualBasic.Format(Now(), "hh:mmtt"))
                '    PrintLine(1, TAB(33), Microsoft.VisualBasic.Format(Now(), "dd-MM-yyyy"))
                '    mlin = mlin + 1
                '    PrintLine(1, TAB(62), txtno.Text)
                '    mlin = mlin + 1

                '    If Trim(mcomp) = "RHLLIVE" Then
                '        PrintLine(1, TAB(52), Chr(27) + Chr(14) + "HO" + Chr(18))
                '    Else
                '        PrintLine(1, TAB(52), Chr(27) + Chr(14) + "PRPC" + Chr(18))
                '    End If

                '    mlin = mlin + 1

                '    m = 24 - mlin
                '    For k = 1 To m
                '        PrintLine(1, TAB(62), "")
                '        mlin = mlin + 1
                '    Next
                '    m = 0
                '    FileClose(1)

                '    'If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                '    'If chkdirprn.Checked = False Then
                '    'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                '    Shell("cmd.exe /c" & "type " & mdir & " > lpt" & Trim(txtport.Text))
                '    'End If
                '    'End If
                'End If
                txtpodno.Focus()


                'txtno.Text = ""
                'txtcardcode.Text = ""

                'If minvt = True Then
                '    txtno.Focus()
                'End If

                'If minvt = False Then
                '    txtcardcode.Focus()
                'End If

            End If
            'Shell("print /d:LPT1 c:\barcodadd.txt", vbNormalFocus)
        End If


    End Sub
    Private Sub addprnex()


        'Dim dir As String


        'excelcon(cmbver.Text, txtfile.Text)


        ''dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        ''mdir = Trim(dir) & "\barcodadd.txt"

        'dir = System.AppDomain.CurrentDomain.BaseDirectory()
        'mdir = Trim(dir) & "barcodadd.txt"

        'If Len(Trim(mcomp)) > 0 Then
        '    'msql = "select docentry,comp,group1 as type,group2 as prntype,printon ,stickercol,labrow,labcol,printer from barhead where active=1"

        '    If Val(txtno.Text) > 0 Or Len(Trim(txtcardcode.Text)) > 0 Then


        '        If Len(Trim(txtcardcode.Text)) > 0 Then
        '            msql = "select CardCode,CardName, mname as cardfname,add1 as building,add2 as block,add3 as street,add4 as city,zipcode,phone,mobileno,state,district from [sheet1$]  where cardcode=" & """" & Trim(txtcardcode.Text) & """"

        '            'msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
        '            '    & " isnull(t1.zipcode,'') as zipcode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
        '            '     & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where t0.cardcode='" & Trim(txtcardcode.Text) & "'"
        '        End If





        '        Dim CMD As New OleDb.OleDbCommand(msql, xcon)
        '        If xcon.State = ConnectionState.Closed Then
        '            xcon.Open()
        '        End If

        '        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        '        'trans.Begin()

        '        Try
        '            ''Dim DR As SqlDataReader
        '            Dim DR As OleDb.OleDbDataReader
        '            DR = CMD.ExecuteReader
        '            If DR.HasRows = True Then
        '                While DR.Read
        '                    lblname.Text = Replace(DR.Item("cardfname") & vbNullString, "'", "`")
        '                    mcompname = DR.Item("cardfname") & vbNullString
        '                    mbuilding = DR.Item("building") & vbNullString
        '                    mblock = DR.Item("block") & vbNullString
        '                    mstreetno = DR.Item("street") & vbNullString
        '                    mcity = DR.Item("city") & vbNullString
        '                    mpincode = DR.Item("zipcode") & vbNullString
        '                    mstate = DR.Item("state") & vbNullString
        '                    mmobileno = DR.Item("mobileno") & vbNullString
        '                End While

        '            End If
        '            DR.Close()
        '        Catch ex As Exception
        '            MsgBox(ex.Message)

        '        End Try

        '        CMD.Dispose()


        '        msql2 = "select t0.code,t0.compnyname,t0.printheadr,isnull(building,'') as building,isnull(block,'') as block,isnull(streetno,'') as streetno,isnull(street,'') as street," & vbCrLf _
        '                & " isnull(city,'') as city,isnull(zipcode,'') as zipcode from " & Trim(mcomp) & ".dbo.oadm t0" & vbCrLf _
        '                & "left join " & Trim(mcomp) & ".dbo.ADM1 t1 on t1.Code=t0.Code"


        '        Dim CMD2 As New OleDb.OleDbCommand(msql2, con)
        '        If con.State = ConnectionState.Closed Then
        '            con.Open()
        '        End If
        '        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        '        'trans.Begin()

        '        Try
        '            ''Dim DR As SqlDataReader
        '            Dim DR1 As OleDb.OleDbDataReader
        '            DR1 = CMD2.ExecuteReader
        '            If DR1.HasRows = True Then
        '                While DR1.Read
        '                    tcompname = DR1.Item("printheadr") & vbNullString
        '                    tbuilding = DR1.Item("building") & vbNullString
        '                    tblock = DR1.Item("block") & vbNullString
        '                    tstreetno = Trim(DR1.Item("streetno")) & vbNullString & "," & Trim(DR1.Item("street")) & vbNullString
        '                    tcity = Trim(DR1.Item("city")) & vbNullString
        '                    tpincode = Trim(DR1.Item("zipcode")) & vbNullString
        '                End While

        '            End If
        '            DR1.Close()
        '        Catch ex As Exception
        '            MsgBox(ex.Message)

        '        End Try

        '        CMD.Dispose()


        '        txtpodno.Focus()



        '    End If
        '    'Shell("print /d:LPT1 c:\barcodadd.txt", vbNormalFocus)
        'End If


    End Sub
    Private Sub txtno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtno.KeyPress
        If Asc(e.KeyChar) = 13 Then
            minvt = True
            txtpodno.Focus()
            'txt2.Focus()
            'Call addprn()
        End If
    End Sub
    Private Sub courbar(ByVal mcorno As String, ByVal mbillno As String)
        Dim dir As String
        mkrow = 0
        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "courbar.txt"
        'dir = System.AppDomain.CurrentDomain.BaseDirectory()
        'If chkdirprn.Checked = True Then
        ' FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        ' Else
        FileOpen(1, mdir, OpenMode.Output)
        PrintLine(1, TAB(0), "^XA")
        PrintLine(1, TAB(0), "^PRC")
        PrintLine(1, TAB(0), "^LH0,0^FS")
        PrintLine(1, TAB(0), "^LL304")
        PrintLine(1, TAB(0), "^MD0")
        PrintLine(1, TAB(0), "^MNY")
        PrintLine(1, TAB(0), "^LH0,0^FS")
        PrintLine(1, TAB(0), "^FO" & Trim(Str(Val(101) + mkrow)) & ",11^A0N,40,40^CI13^FR^FD" & Trim(mbillno) & "^FS")
        PrintLine(1, TAB(0), "^BY1.9,2.5^FO" & Trim(Str(Val(61) + mkrow)) & ",51^B3N,N,35,N,Y^FR^FD" & Trim(txtpodno.Text) & "^FS")
        PrintLine(1, TAB(0), "^FO" & Trim(Str(Val(81) + mkrow)) & ",90^A0N,31,28^CI13^FR^FD" & Trim(txtpodno.Text) & "^FS")
        mkrow = mkrow + 260
        If mkrow > 601 Then
            mkrow = 0
        End If

        'PrintLine(1, TAB(0), "^FO350,11^A0N,40,48^CI13^FR^FD" & Trim(mbillno) & "^FS")
        'PrintLine(1, TAB(0), "^BY1.9,2.5^FO320,51^B3N,N,35,N,Y^FR^FD" & Trim(txtpodno.Text) & "^FS")
        'PrintLine(1, TAB(0), "^FO341,90^A0N,31,28^CI13^FR^FD" & Trim(txtpodno.Text) & "^FS")

        'PrintLine(1, TAB(0), "^FO610,11^A0N,40,48^CI13^FR^FD" & Trim(mbillno) & "^FS")
        'PrintLine(1, TAB(0), "^BY1.9,2.5^FO580,51^B3N,N,35,N,Y^FR^FD" & Trim(txtpodno.Text) & "^FS")
        'PrintLine(1, TAB(0), "^FO601,90^A0N,31,28^CI13^FR^FD" & Trim(txtpodno.Text) & "^FS")

        PrintLine(1, TAB(0), "^PQ1,0,0,N")
        PrintLine(1, TAB(0), "^XZ")

        FileClose(1)

        'If chkdirprn.Checked = False Then
        'If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        If mos = "WIN" Then
            Shell("cmd.exe /c" & "type " & mdir & " > lpt" & Trim(txtport2.Text))
        Else
            'Dim printer As String = mprinter
            'Dim filePath As String = mlinpath
            'Dim filePathname As String = mlinpath & "courbar.txt"
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
            Dim filePathname As String = mlinpath & "courbar.txt"
            Dim success As Boolean = PrintTscRaw(printer, filePathname)


        End If
    End Sub

    Private Sub txtno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtno.LostFocus
        'If Len(Trim(txtpodno.Text)) > 0 Then
        '    If Val(txtno.Text) > 0 Then
        '        Call podsave()
        '    End If
        'Else
        If chkexcel.Checked = True Then
            Call addprnex()
        Else

            Call addprn()
        End If


        msql2 = "select * from " & Trim(mcomp) & ".dbo.courier where docentry=" & Val(txtdocentry.Text)


        'Dim CMD4 As New SqlCommand(msql2, con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        ''Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        ''trans.Begin()

        'Try
        '    ''Dim DR As SqlDataReader
        '    Dim DR As SqlDataReader
        '    DR = CMD4.ExecuteReader
        '    If DR.HasRows = True Then
        '        'MsgBox("Already Exist!")
        '        'If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '        If chkprn.Checked = True Then
        '            Call crystalrr()
        '        End If


        '        txtno.Text = ""
        '        txtdocentry.Text = ""
        '        lblname.Text = ""

        '        txtno.Focus()
        '    Else
        '        If chkprn.Checked = False Then
        '            Call AUTONO()
        '        End If
        '    End If
        '    DR.Close()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        'CMD4.Dispose()

        Try
            Dim dt As DataTable = getDataTable(msql2)
            If dt.Rows.Count > 0 Then
                If chkprn.Checked = True Then
                    Call crystalrr()
                End If


                txtno.Text = ""
                txtdocentry.Text = ""
                lblname.Text = ""

                txtno.Focus()
            Else
                If chkprn.Checked = False Then
                    Call AUTONO()
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try





        'Call AUTONO()
        'End If

    End Sub
    Private Sub AUTONO()

        'Dim CMD4 As New SqlCommand("SELECT MAX(courierno) AS TNO FROM courier where courierno>=" & mstart & " and courierno<=" & mend, con)
        '' End If


        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'Dim CBNO As Int32 = IIf(IsDBNull(CMD4.ExecuteScalar) = False, CMD4.ExecuteScalar, 0)

        ''txtno.Text = CBNO + 1
        'txtcourierno.Text = CBNO + 1
        'If Val(txtcourierno.Text) = 1 Then
        '    txtcourierno.Text = mstart
        'End If
        'txtpodno.Text = Trim(mprfx) + LTrim(txtcourierno.Text)
        'CMD4.Dispose()

        Dim sqry As String = "SELECT MAX(courierno) AS TNO FROM courier where courierno>=" & mstart & " and courierno<=" & mend
        Dim CBNO As Int32 = executescalarQuery(sqry)
        txtcourierno.Text = CBNO + 1
        If Val(txtcourierno.Text) = 1 Then
            txtcourierno.Text = mstart
        End If
        txtpodno.Text = Trim(mprfx) + LTrim(txtcourierno.Text)
        'con2.Close()
    End Sub


    Private Sub podsave()


        Dim cmds As SqlCommand
        Dim cmds1 As SqlCommand
        'Dim commands As New List(Of SqlCommand)

        If Val(txtno.Text) > 0 Then
            sql = "update " & Trim(mcomp) & ".dbo.oinv set u_courpodno='" & Trim(txtpodno.Text) & "' where docnum=" & Val(txtno.Text)
            If Len(Trim(txtremark.Text)) > 0 Then
                sql2 = "insert into " & Trim(mcomp) & ".dbo.courier (date,docnum,cardname,company,podno,remark,courierno,docentry) values('" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'," & Val(txtno.Text) & ",'" & Trim(lblname.Text) & "','" & Trim(cmbcomp.Text) & "','" & Trim(txtpodno.Text) & "','" & Trim(txtremark.Text) & "'," & Val(txtcourierno.Text) & "," & Val(txtdocentry.Text) & ")"
            Else
                sql2 = "insert into " & Trim(mcomp) & ".dbo.courier (date,docnum,cardname,company,podno,courierno,docentry) values('" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'," & Val(txtno.Text) & ",'" & Trim(lblname.Text) & "','" & Trim(cmbcomp.Text) & "','" & Trim(txtpodno.Text) & "'," & Val(txtcourierno.Text) & "," & Val(txtdocentry.Text) & ")"
            End If
            'sql2 = "insert into " & Trim(mcomp) & ".dbo.courier (date,docnum,cardname,company,podno) values('" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'," & Val(txtno.Text) & ",'" & Trim(lblname.Text) & "','" & Trim(cmbcomp.Text) & "','" & Trim(txtpodno.Text) & "')"

            'courbar(Trim(txtpodno.Text), Trim(txtno.Text))
        ElseIf Len(Trim(txtcardcode.Text)) > 0 Then
            If Len(Trim(txtremark.Text)) > 0 Then
                If chkexcel.Checked = True Then
                    sql = "insert into " & Trim(mcomp) & ".dbo.courier (date,cardcode,cardname,company,podno,remark,courierno,cardfname,add1,add2,add3,city,zipcode,state,mobileno) values('" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "','" & Trim(txtcardcode.Text) & "','" & Trim(lblname.Text) & "','" & Trim(cmbcomp.Text) & "','" & Trim(txtpodno.Text) & "','" & Trim(txtremark.Text) & "'," & Val(txtcourierno.Text) & ",'" & Trim(mcompname) & "','" & Trim(mbuilding) & "','" & Trim(mblock) & "','" & Trim(mstreetno) & "','" & Trim(mcity) & "','" & Trim(mpincode) & "','" & Trim(mstate) & "','" & Trim(mmobileno) & "'" & ")"
                Else
                    sql = "insert into " & Trim(mcomp) & ".dbo.courier (date,cardcode,cardname,company,podno,remark,courierno) values('" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "','" & Trim(txtcardcode.Text) & "','" & Trim(lblname.Text) & "','" & Trim(cmbcomp.Text) & "','" & Trim(txtpodno.Text) & "','" & Trim(txtremark.Text) & "'," & Val(txtcourierno.Text) & ")"
                End If

            Else
                If chkexcel.Checked = True Then
                    sql = "insert into " & Trim(mcomp) & ".dbo.courier (date,cardcode,cardname,company,podno,courierno,cardfname,add1,add2,add3,city,zipcode,state,mobileno) values('" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "','" & Trim(txtcardcode.Text) & "','" & Trim(lblname.Text) & "','" & Trim(cmbcomp.Text) & "','" & Trim(txtpodno.Text) & "'," & Val(txtcourierno.Text) & ",'" & Trim(mcompname) & "','" & Trim(mbuilding) & "','" & Trim(mblock) & "','" & Trim(mstreetno) & "','" & Trim(mcity) & "','" & Trim(mpincode) & "','" & Trim(mstate) & "','" & Trim(mmobileno) & "'" & ")"
                    'sql = "insert into " & Trim(mcomp) & ".dbo.courier (date,cardcode,cardname,company,podno,courierno,cardfname,add1,add2,add3,city,zipcode,state,mobileno) values('" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "','" & Trim(txtcardcode.Text) & "','" & Trim(lblname.Text) & "','" & Trim(cmbcomp.Text) & "','" & Trim(txtpodno.Text) & "','" & Trim(txtremark.Text) & "'," & Val(txtcourierno.Text) & ")"
                Else

                    sql = "insert into " & Trim(mcomp) & ".dbo.courier (date,cardcode,cardname,company,podno,courierno) values('" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "','" & Trim(txtcardcode.Text) & "','" & Trim(lblname.Text) & "','" & Trim(cmbcomp.Text) & "','" & Trim(txtpodno.Text) & "'," & Val(txtcourierno.Text) & ")"
                End If

            End If
            'courbar(Trim(txtpodno.Text), Trim(txtcardcode.Text))
        End If



        Try

            executeQuery(sql)

            'If con.State = ConnectionState.Closed Then
            '    con.Open()
            'End If

            'cmds = New SqlCommand(sql, con)
            'cmds.ExecuteNonQuery()
            'cmds.Dispose()

            If Len(Trim(sql2)) > 0 Then
                'cmds1 = New SqlCommand(sql2, con)
                'cmds1.ExecuteNonQuery()
                'cmds1.Dispose()
                executeQuery(sql2)
            End If

            If Val(txtno.Text) > 0 Then
                Call crystalrr()
            End If
            If Len(Trim(txtcardcode.Text)) > 0 And Val(txtno.Text) = 0 Then
                Call crystal()
            End If



            txtno.Text = ""
            txtcardcode.Text = ""
            txtpodno.Text = ""
            lblname.Text = ""
            txtcourierno.Text = ""
            sql2 = ""
        Catch ex As Exception
            MsgBox(ex.Message)
            txtno.Text = ""
            txtcardcode.Text = ""
            txtpodno.Text = ""
            txtcourierno.Text = ""

            lblname.Text = ""
            txtremark.Text = ""
            'MsgBox("Can not open connection ! ")
        End Try






        If minvt = True Then
            txtno.Focus()
        End If

        If minvt = False Then
            txtcardcode.Focus()
        End If

    End Sub
    Private Sub stickerprn()
        Dim dir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "sticker.txt"
        'dir = System.AppDomain.CurrentDomain.BaseDirectory()
        'If chkdirprn.Checked = True Then
        ' FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        ' Else
        FileOpen(1, mdir, OpenMode.Output)
        'End If

        mlin = 1



        With flx
            For i = 1 To .Rows - 1
                '.Row = I
                '.Col = 0
                If Len(Trim(.get_TextMatrix(i, 0))) > 0 Then
                    'If Len(.text) > 0 Then
                    If mareacode = True Then
                        msql3 = "select t0.CardCode,t0.CardName,t0.CardFName as mname,isnull(t1.building,'') as add1,isnull(t1.block,'') as add2,isnull(t1.street,'') as add3,isnull(t1.city,'') as add4, " & vbCrLf _
                        & "isnull(t1.zipcode,'') as zipcode,case when t0.phone1 is not null then t0.phone1 else t0.phone2 end as phone, t1.state,t0.U_AreaCode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                        & " left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.cardcode)='" & Trim(flx.get_TextMatrix(i, 1)) & "'"
                    Else
                        msql3 = "select t0.CardCode,t0.CardName,t0.CardFName as mname,isnull(t1.building,'') as add1,isnull(t1.block,'') as add2,isnull(t1.street,'') as add3,isnull(t1.city,'') as add4, " & vbCrLf _
                        & "isnull(t1.zipcode,'') as zipcode,case when t0.phone1 is not null then t0.phone1 else t0.phone2 end as phone, t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                        & " left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.cardcode)='" & Trim(flx.get_TextMatrix(i, 1)) & "'"
                    End If

                    Dim CMD3 As New SqlCommand(msql3, con)
                    Dim DR3 As SqlDataReader
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If

                    'Dim DR3 As OleDb.OleDbDataReader
                    DR3 = CMD3.ExecuteReader
                    If DR3.HasRows = True Then
                        While DR3.Read

                            If chkbox.Checked = True Then
                                PrintLine(1, TAB(0), "^FX Precompiled file for ID_100A.BCB width (pixels)=608 height (pixels)=358^FS")
                                PrintLine(1, TAB(0), "^FX Scale factors: xf=1.000 yf=1.000 orientation=0 scaled width (bytes)=76 scaled height (bytes)=358^FS")
                                PrintLine(1, TAB(0), "~DGID_100A ,27208,76,")
                                PrintLine(1, TAB(0), "H0mJFE,")
                                PrintLine(1, TAB(0), "07mKFE,")
                                PrintLine(1, TAB(0), "1mMF,")
                                PrintLine(1, TAB(0), "3mMFC,")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), "7mMFE,")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), "mNFE,")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), "IFCmG07HFE,")
                                For l = 1 To 330
                                    PrintLine(1, TAB(0), ":")
                                Next l
                                PrintLine(1, TAB(0), "mNFE,")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), "7mMFE,")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), "3mMFC,")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), "1mMF,")
                                PrintLine(1, TAB(0), "07mKFE,")
                            End If

                            PrintLine(1, TAB(0), "^XA")
                            PrintLine(1, TAB(0), "^PRC")
                            PrintLine(1, TAB(0), "^LH0,0^FS")
                            PrintLine(1, TAB(0), "^LL360")
                            PrintLine(1, TAB(0), "^MD5")
                            PrintLine(1, TAB(0), "^MNY")
                            PrintLine(1, TAB(0), "^LH0,0^FS")

                            PrintLine(1, TAB(0), "^FO153,64^A0N,31,28^CI13^FR^FD" & IIf(chkperson.Checked = False, "M/s.", "") & Trim(DR3.Item("mname")) & vbNullString & "^FS;")
                            PrintLine(1, TAB(0), "^FO153,104^A0N,32,28^CI13^FR^FD" & Trim(DR3.Item("add1")) & vbNullString & "^FS")
                            PrintLine(1, TAB(0), "^FO153,143^A0N,32,28^CI13^FR^FD" & Trim(DR3.Item("add2")) & vbNullString & "^FS")
                            PrintLine(1, TAB(0), "^FO153,184^A0N,32,28^CI13^FR^FD" & Trim(DR3.Item("add3")) & vbNullString & "^FS")

                            If IsDBNull(DR3.Item("ADD4")) = False Then
                                If Len(Trim(DR3.Item("ADD4"))) > 0 Then
                                    PrintLine(1, TAB(0), "^FO153,219^A0N,32,28^CI13^FR^FD" & Trim(DR3.Item("ADD4")) & "-" & Trim(DR3.Item("zipcode")) & vbNullString & "^FS")
                                End If
                            End If

                            If IsDBNull(DR3.Item("phone")) = False Then
                                If Len(Trim(DR3.Item("phone"))) > 0 Then
                                    PrintLine(1, TAB(0), "^FO153,259^A0N,32,28^CI13^FR^FDPh/Cell:" & Trim(DR3.Item("phone")) & vbNullString & "^FS")
                                End If
                            End If

                            If IsDBNull(DR3.Item("cardcode")) = False Then
                                If Len(Trim(DR3.Item("cardcode"))) > 0 Then
                                    PrintLine(1, TAB(0), "^^BY2,3.0^FO153,299^BCN,32,N,Y,N^FR^FD" & Trim(DR3.Item("cardcode")) & vbNullString & "^FS")

                                    '^BY2,3.0^FO153,299^BCN,32,N,Y,N^FR^FDC010226^FS'
                                End If
                            End If

                            PrintLine(1, TAB(0), "^FO125,27^A0N,32,28^CI13^FR^FDTO.^FS")
                            If chkbox.Checked = True Then
                                PrintLine(1, TAB(0), "^FO108,0^XGID_100A ,1,1^FS")
                            End If
                            PrintLine(1, TAB(0), "^PQ1,0,0,N")
                            PrintLine(1, TAB(0), "^XZ")
                            'PrintLine(1, TAB(0), DR.Item("firstdet"))
                        End While
                    End If

                    DR3.Close()
                    CMD3.Dispose()
                End If
            Next i

        End With

        FileClose(1)

        'If chkdirprn.Checked = False Then

        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If mos = "WIN" Then
                'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                Shell("cmd.exe /c" & "type " & mdir & " > lpt" & Trim(txtport.Text))
                'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
            Else
                'Dim printer As String = mprinter
                'Dim filePath As String = mlinpath
                'Dim filePathname As String = mlinpath & "sticker.txt"
                ''"/home/testing/Desktop/Barcodelinux/nsbarcodEH.txt"

                'Dim psi As New ProcessStartInfo()
                'psi.FileName = "/bin/bash"

                'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""
                'psi.UseShellExecute = False
                'psi.CreateNoWindow = True

                ''TextBox1.Text = psi.FileName & psi.Arguments
                'Process.Start(psi)


                Dim printer As String = tscprinter1
                Dim filePath As String = mlinpath
                Dim filePathname As String = mlinpath & "sticker.txt"
                Dim success As Boolean = PrintTscRaw(printer, filePathname)

            End If

        End If
        'End If

    End Sub
    Private Sub loadstick()
        If chkdealparty.Checked = True Then
            If chkvendor.Checked = False Then
                If Len(Trim(cmbagent.Text)) > 0 And Len(Trim(cmbstate.Text)) = 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                 & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.u_areacode)='" & Trim(cmbagent.Text) & "' and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"
                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                 & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.state)='" & Trim(cmbstate.Text) & "'  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName "

                ElseIf Len(Trim(cmbagent.Text)) > 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.u_areacode)='" & Trim(cmbagent.Text) & "' and rtrim(t1.state)='" & Trim(cmbstate.Text) & "'  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"

                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) > 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.CITY)='" & Trim(cmbcity.Text) & "' and rtrim(t1.state)='" & Trim(cmbstate.Text) & "'  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"

                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) > 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.COUNTY)='" & Trim(cmbdist.Text) & "' and rtrim(t1.state)='" & Trim(cmbstate.Text) & "'  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"

                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) = 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) > 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.COUNTY)='" & Trim(cmbdist.Text) & "'  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"
                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) = 0 And Len(Trim(cmbcity.Text)) > 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.CITY)='" & Trim(cmbcity.Text) & "'  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"
                End If
            Else
                msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                 & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='B' where t0.cardtype in ('S','V')  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.opch group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"
            End If


        Else
            If chkvendor.Checked = False Then

                If Len(Trim(cmbagent.Text)) > 0 And Len(Trim(cmbstate.Text)) = 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                 & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.u_areacode)='" & Trim(cmbagent.Text) & "' order by t1.state,isnull(t1.city,''),t0.CardName"
                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                 & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.state)='" & Trim(cmbstate.Text) & "' order by t1.state,isnull(t1.city,''),t0.CardName"

                ElseIf Len(Trim(cmbagent.Text)) > 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.u_areacode)='" & Trim(cmbagent.Text) & "' and rtrim(t1.state)='" & Trim(cmbstate.Text) & "' order by t1.state,isnull(t1.city,''),t0.CardName"

                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) > 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.CITY)='" & Trim(cmbcity.Text) & "' and rtrim(t1.state)='" & Trim(cmbstate.Text) & "' order by t1.state,isnull(t1.city,''),t0.CardName"

                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) > 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.COUNTY)='" & Trim(cmbdist.Text) & "' and rtrim(t1.state)='" & Trim(cmbstate.Text) & "' order by t1.state,isnull(t1.city,''),t0.CardName"

                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) = 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) > 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.COUNTY)='" & Trim(cmbdist.Text) & "' order by t1.state,isnull(t1.city,''),t0.CardName"
                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) = 0 And Len(Trim(cmbcity.Text)) > 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.CITY)='" & Trim(cmbcity.Text) & "' order by t1.state,isnull(t1.city,''),t0.CardName"
                End If
            Else
                msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                            & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                             & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='B' where t0.cardtype in ('S','V') order by t1.state,isnull(t1.city,''),t0.CardName"
            End If

        End If

        'Dim CMD2 As New SqlCommand(msql, con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()
        flx.Clear()
        flx.Visible = False
        Call flxhead()
        Try
            Dim dt As DataTable = getDataTable(msql)
            ''Dim DR As SqlDataReader
            'Dim DR2 As SqlDataReader
            'DR2 = CMD2.ExecuteReader
            If dt.Rows.Count > 0 Then

                With flx
                    'While DR2.Read
                    For Each rw As DataRow In dt.Rows
                            .Rows = .Rows + 1
                            .Row = .Rows - 1
                        '.set_TextMatrix(.Row, 0, DR.Item("docentry"))
                        .set_TextMatrix(.Row, 1, rw("cardcode") & vbNullString)
                        .set_TextMatrix(.Row, 2, rw("cardname") & vbNullString)
                        .set_TextMatrix(.Row, 3, rw("city") & vbNullString)
                        .set_TextMatrix(.Row, 4, rw("state") & vbNullString)
                    Next
                        'End While
                        Label12.Text = .Rows - 1
                End With
            End If
            flx.Visible = True
            'DR2.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            flx.Clear()
            Call flxhead()
        End Try
        flx.Visible = True
        'CMD2.Dispose()


    End Sub
    Private Sub loadstick22()
        If chkdealparty.Checked = True Then
            If chkvendor.Checked = False Then
                If Len(Trim(cmbagent.Text)) > 0 And Len(Trim(cmbstate.Text)) = 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                 & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.u_areacode)='" & Trim(cmbagent.Text) & "' and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"
                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                 & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.state)='" & Trim(cmbstate.Text) & "'  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName "

                ElseIf Len(Trim(cmbagent.Text)) > 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.u_areacode)='" & Trim(cmbagent.Text) & "' and rtrim(t1.state)='" & Trim(cmbstate.Text) & "'  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"

                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) > 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.CITY)='" & Trim(cmbcity.Text) & "' and rtrim(t1.state)='" & Trim(cmbstate.Text) & "'  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"

                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) > 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.COUNTY)='" & Trim(cmbdist.Text) & "' and rtrim(t1.state)='" & Trim(cmbstate.Text) & "'  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"

                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) = 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) > 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.COUNTY)='" & Trim(cmbdist.Text) & "'  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"
                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) = 0 And Len(Trim(cmbcity.Text)) > 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.CITY)='" & Trim(cmbcity.Text) & "'  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"
                ElseIf Val(Microsoft.VisualBasic.Format(CDate(mskdate.Text), "dd")) > 0 Then
                    If chkwinv.Checked = False Then
                        'msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                        '       & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                        '        & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where  t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv )  group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"
                        ''Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd")

                        msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.CITY)='" & Trim(cmbcity.Text) & "'  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"

                    Else
                        'msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                        '         & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                        '          & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.CITY)='" & Trim(cmbcity.Text) & "'  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.oinv group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"

                        msql = " select b.DocNum, t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city,isnull(t1.zipcode,'') as zipcode,t1.state from oinv b" & vbCrLf _
                             & "left join OCRD t0 on t0.CardCode=b.CardCode " & vbCrLf _
                             & "left join CRD1 t1 on t1.CardCode=t0.CardCode and t1.adrestype='B'" & vbCrLf _
                             & "where b.DocDate='" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"
                    End If

                End If
            Else
                msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                 & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='B' where t0.cardtype in ('S','V')  and t0.cardcode in (select cardcode from " & Trim(mcomp) & ".dbo.opch group by cardcode) order by t1.state,isnull(t1.city,''),t0.CardName"
            End If


        Else
            If chkvendor.Checked = False Then

                If Len(Trim(cmbagent.Text)) > 0 And Len(Trim(cmbstate.Text)) = 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                 & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.u_areacode)='" & Trim(cmbagent.Text) & "' order by t1.state,isnull(t1.city,''),t0.CardName"
                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                 & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.state)='" & Trim(cmbstate.Text) & "' order by t1.state,isnull(t1.city,''),t0.CardName"

                ElseIf Len(Trim(cmbagent.Text)) > 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.u_areacode)='" & Trim(cmbagent.Text) & "' and rtrim(t1.state)='" & Trim(cmbstate.Text) & "' order by t1.state,isnull(t1.city,''),t0.CardName"

                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) > 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.CITY)='" & Trim(cmbcity.Text) & "' and rtrim(t1.state)='" & Trim(cmbstate.Text) & "' order by t1.state,isnull(t1.city,''),t0.CardName"

                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) > 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) > 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.COUNTY)='" & Trim(cmbdist.Text) & "' and rtrim(t1.state)='" & Trim(cmbstate.Text) & "' order by t1.state,isnull(t1.city,''),t0.CardName"

                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) = 0 And Len(Trim(cmbcity.Text)) = 0 And Len(Trim(cmbdist.Text)) > 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.COUNTY)='" & Trim(cmbdist.Text) & "' order by t1.state,isnull(t1.city,''),t0.CardName"
                ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) = 0 And Len(Trim(cmbcity.Text)) > 0 And Len(Trim(cmbdist.Text)) = 0 Then
                    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                                 & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.CITY)='" & Trim(cmbcity.Text) & "' order by t1.state,isnull(t1.city,''),t0.CardName"
                End If
            Else
                msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
                            & " isnull(t1.zipcode,'') as zipcode,t1.state from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                             & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='B' where t0.cardtype in ('S','V') order by t1.state,isnull(t1.city,''),t0.CardName"
            End If

        End If

        'Dim CMD2 As New SqlCommand(msql, con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()
        flx.Clear()
        flx.Visible = False
        Call flxhead()
        Try
            Dim dt As DataTable = getDataTable(msql)
            ''Dim DR As SqlDataReader
            'Dim DR2 As SqlDataReader
            'DR2 = CMD2.ExecuteReader
            'If DR2.HasRows = True Then
            If dt.Rows.Count > 0 Then

                    With flx
                    'While DR2.Read
                    For Each rw As DataRow In dt.Rows
                            .Rows = .Rows + 1
                            .Row = .Rows - 1
                        '.set_TextMatrix(.Row, 0, DR.Item("docentry"))
                        .set_TextMatrix(.Row, 1, rw("cardcode") & vbNullString)
                        .set_TextMatrix(.Row, 2, rw("cardname") & vbNullString)
                        .set_TextMatrix(.Row, 3, rw("city") & vbNullString)
                        .set_TextMatrix(.Row, 4, rw("state") & vbNullString)
                        If chkwinv.Checked = True Then
                            .set_TextMatrix(.Row, 5, rw("docnum") & vbNullString)
                        End If
                        Next
                        'End While
                        Label12.Text = .Rows - 1
                    End With
                End If
                flx.Visible = True
            'DR2.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            flx.Clear()
            Call flxhead()
        End Try

        flx.Visible = True
        'CMD2.Dispose()


    End Sub
    Private Sub flxhead()
        flx.Rows = 1
        flx.Cols = 6
        flx.set_ColWidth(0, 900)
        flx.set_ColWidth(1, 1200)
        flx.set_ColWidth(2, 2300)
        flx.set_ColWidth(3, 1300)
        flx.set_ColWidth(4, 600)
        flx.set_ColWidth(5, 700)

        flx.Row = 0
        flx.Col = 0
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 0, "Sel")

        flx.Col = 1
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 1, "Code")

        flx.Col = 2
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 2, "Party Name")

        flx.Col = 3
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 3, "City")

        flx.Col = 4
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 4, "State")

        flx.Col = 5
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 5, "Docnum")

        'flxc.Col = 5
        'flxc.CellAlignment = 3
        'flxc.CellFontBold = True
        'flxc.set_TextMatrix(0, 5, "Stik-Col")

        'flxc.Col = 6
        'flxc.CellAlignment = 3
        'flxc.CellFontBold = True
        'flxc.set_TextMatrix(0, 6, "L.row")

        'flxc.Col = 7
        'flxc.CellAlignment = 3
        'flxc.CellFontBold = True
        'flxc.set_TextMatrix(0, 7, "L.Col")

        'flxc.Col = 8
        'flxc.CellAlignment = 3
        'flxc.CellFontBold = True
        'flxc.set_TextMatrix(0, 8, "Printer")


        flx.set_ColAlignment(0, 2)
        flx.set_ColAlignment(1, 2)
        flx.set_ColAlignment(2, 2)
        flx.set_ColAlignment(3, 2)
        flx.set_ColAlignment(4, 2)
        'flx.set_ColAlignment(8, 2)

    End Sub
    Private Sub flxhead2()
        flx.Rows = 1
        flx.Cols = 7
        flx.set_ColWidth(0, 1000)
        flx.set_ColWidth(1, 1200)
        flx.set_ColWidth(2, 1300)
        flx.set_ColWidth(3, 2300)
        flx.set_ColWidth(4, 1600)
        flx.set_ColWidth(5, 1600)
        flx.set_ColWidth(6, 1600)

        flx.Row = 0
        flx.Col = 0
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 0, "Date")

        flx.Col = 1
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 1, "Company")

        flx.Col = 2
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 2, "Inv.No/Cardcode")

        flx.Col = 3
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 3, "Cardname")

        flx.Col = 4
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 4, "PODNo")

        flx.Col = 5
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 5, "Destination")



        flx.set_ColAlignment(0, 2)
        flx.set_ColAlignment(1, 2)
        flx.set_ColAlignment(2, 2)
        flx.set_ColAlignment(3, 2)
        flx.set_ColAlignment(4, 2)
        flx.set_ColAlignment(5, 2)

    End Sub



    Private Sub loadstate()
        msql = "select state  from  " & Trim(mcomp) & ".dbo.crd1 group by state order by state"
        'Dim CMD As New SqlCommand(msql, con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'cmbstate.Items.Clear()
        'Try
        '    ''Dim DR As SqlDataReader
        '    Dim DR As SqlDataReader
        '    DR = CMD.ExecuteReader
        '    If DR.HasRows = True Then

        '        While DR.Read
        '            cmbstate.Items.Add(DR.Item("state"))
        '        End While

        '    End If
        '    DR.Close()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        '    cmbstate.Items.Clear()
        '    'flx.Clear()
        '    'Call flxchead()
        'End Try

        'CMD.Dispose()

        cmbstate.Items.Clear()
        Dim dt1 As DataTable = getDataTable(msql)
        If dt1.Rows.Count > 0 Then
            For Each rw As DataRow In dt1.Rows
                cmbstate.Items.Add(rw("state"))
            Next
        End If
        dt1.Dispose()
    End Sub

    Private Sub loadagent()
        msql = "select u_areacode  from  " & Trim(mcomp) & ".dbo.ocrd where u_areacode is not null group by u_areacode order by u_areacode"
        'Dim CMD As New SqlCommand(msql, con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If
        ''Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        ''trans.Begin()
        ''flx.Clear()
        ''Call flxchead()
        'cmbagent.Items.Clear()
        'Try
        '    ''Dim DR As SqlDataReader
        '    Dim DR As SqlDataReader
        '    DR = CMD.ExecuteReader
        '    If DR.HasRows = True Then

        '        While DR.Read
        '            cmbagent.Items.Add(DR.Item("u_areacode"))
        '        End While

        '    End If
        '    DR.Close()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        '    cmbagent.Items.Clear()
        '    'flx.Clear()
        '    'Call flxchead()
        'End Try

        'CMD.Dispose()


        cmbagent.Items.Clear()
        Dim dt1 As DataTable = getDataTable(msql)
        If dt1.Rows.Count > 0 Then
            For Each rw As DataRow In dt1.Rows
                cmbagent.Items.Add(rw("u_areacode"))
            Next
        End If
        dt1.Dispose()

    End Sub

    Private Sub loadcity()
        msql = "select city  from  " & Trim(mcomp) & ".dbo.crd1 where city is not null group by city order by city"
        'Dim CMD As New SqlCommand(msql, con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'cmbcity.Items.Clear()
        'Try
        '    ''Dim DR As SqlDataReader
        '    Dim DR As SqlDataReader
        '    DR = CMD.ExecuteReader
        '    If DR.HasRows = True Then

        '        While DR.Read
        '            cmbcity.Items.Add(DR.Item("city"))
        '        End While

        '    End If
        '    DR.Close()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        '    cmbagent.Items.Clear()
        '    'flx.Clear()
        '    'Call flxchead()
        'End Try

        'CMD.Dispose()


        cmbcity.Items.Clear()
        Dim dt1 As DataTable = getDataTable(msql)
        If dt1.Rows.Count > 0 Then
            For Each rw As DataRow In dt1.Rows
                cmbcity.Items.Add(rw("city"))
            Next
        End If
        dt1.Dispose()

    End Sub

    Private Sub loaddistrict()
        msql = "select county  from  " & Trim(mcomp) & ".dbo.crd1 where county is not null group by county order by county"
        'Dim CMD As New SqlCommand(msql, con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'cmbdist.Items.Clear()
        'Try
        '    ''Dim DR As SqlDataReader
        '    Dim DR As SqlDataReader
        '    DR = CMD.ExecuteReader
        '    If DR.HasRows = True Then

        '        While DR.Read
        '            cmbdist.Items.Add(DR.Item("county"))
        '        End While

        '    End If
        '    DR.Close()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        '    cmbagent.Items.Clear()
        '    'flx.Clear()
        '    'Call flxchead()
        'End Try

        'CMD.Dispose()


        cmbdist.Items.Clear()
        Dim dt1 As DataTable = getDataTable(msql)
        If dt1.Rows.Count > 0 Then
            For Each rw As DataRow In dt1.Rows
                cmbdist.Items.Add(rw("county"))
            Next
        End If
        dt1.Dispose()


    End Sub


    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.Click
        Label12.Text = ""
        If Len(Trim(mcomp)) > 0 Then
            If mareacode = True Then
                Call loadstick()
            Else
                Call loadstick22()
            End If
        End If
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        'cryRpt.Dispose()
        'cryRpt2.Dispose()
        Me.Close()
    End Sub

    Private Sub cmdsel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsel.Click
        For i = 1 To flx.Rows - 1
            If Len(Trim(flx.get_TextMatrix(i, 0))) = 0 Then
                flx.Row = i
                flx.Col = 0
                'flx.set_TextMatrix(i, 0, Chr(252))

                flx.CellFontName = "WinGdings"
                flx.CellFontSize = 14
                flx.CellAlignment = 4
                flx.CellFontBold = True
                flx.CellForeColor = Color.Red
                flx.set_TextMatrix(i, 0, Microsoft.VisualBasic.Chr(252))
                'flx.Text = Chr(252)
            Else
                flx.Col = 0
                'flx.Text = ""
                flx.set_TextMatrix(i, 0, "")
            End If
        Next i
    End Sub

    Private Sub flx_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flx.Enter

    End Sub

    Private Sub flx_KeyPressEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyPressEvent) Handles flx.KeyPressEvent
        If e.keyAscii = 32 Then
            flx.Row = flx.Row
            If flx.Row > 0 Then
                If Len(Trim(flx.get_TextMatrix(flx.Row, 0))) = 0 Then
                    flx.Col = 0
                    flx.CellFontName = "WinGdings"
                    flx.CellFontSize = 14
                    flx.CellAlignment = 4
                    flx.CellFontBold = True
                    flx.CellForeColor = Color.Red
                    flx.Text = Chr(252)
                Else
                    flx.Col = 0
                    flx.Text = ""
                End If
            End If
        End If
        If e.keyAscii <> 27 Then
            searchflx(flx, e.keyAscii, 2)
        End If

    End Sub

    Private Sub cmdprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdprint.Click
        'Call stickerprn()
        If chkexcel.Checked = True Then
            'CardCode	CardName	mname	add1	add2	add3	add4	zipcode	phone	state

            Call xlstickerprn2()
        Else
            Call stickerprn2()
        End If
    End Sub
    Private Sub stickerprn2()
        Dim dir As String

        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "sticker.txt"
        'dir = System.AppDomain.CurrentDomain.BaseDirectory()
        'If chkdirprn.Checked = True Then
        ' FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        ' Else
        FileOpen(1, mdir, OpenMode.Output)
        'End If

        mlin = 1
        'PrintLine(1, TAB(0), " ")

        'If Len(Trim(cmbagent.Text)) > 0 And Len(Trim(cmbstate.Text)) = 0 Then
        '    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
        '                & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
        '                 & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.u_areacode)='" & Trim(cmbagent.Text) & "'"
        'ElseIf Len(Trim(cmbagent.Text)) = 0 And Len(Trim(cmbstate.Text)) > 0 Then
        '    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
        '                & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
        '                 & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t1.state)='" & Trim(cmbstate.Text) & "'"

        'ElseIf Len(Trim(cmbagent.Text)) > 0 And Len(Trim(cmbstate.Text)) > 0 Then
        '    msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city," & vbCrLf _
        '                 & " isnull(t1.zipcode,'') as zipcode,t1.state,t0.u_areacode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
        '                  & "left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.u_areacode)='" & Trim(cmbagent.Text) & "' and rtrim(t1.state)='" & Trim(cmbstate.Text) & "'"

        'End If

        'msql = "select t0.CardCode,t0.CardName,t0.CardFName,isnull(t1.building,'') as building,isnull(t1.block,'') as block,isnull(t1.street,'') as street,isnull(t1.city,'') as city, " & vbCrLf _
        '   & "isnull(t1.zipcode,'') as zipcode,t1.state,t0.U_AreaCode from dbo.ocrd t0 " & vbCrLf _
        '   & " left join CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where t0.cardcode="




        With flx
            For i = 1 To .Rows - 1
                '.Row = I
                '.Col = 0
                If Len(Trim(.get_TextMatrix(i, 0))) > 0 Then
                    'If Len(.text) > 0 Then
                    If mareacode = True Then
                        If chkvendor.Checked = False Then
                            msql3 = "select t0.CardCode,t0.CardName,t0.CardFName as mname,isnull(t1.building,'') as add1,isnull(t1.block,'') as add2,isnull(t1.street,'') as add3,isnull(t1.city,'') as add4, " & vbCrLf _
                            & "isnull(t1.zipcode,'') as zipcode,case when t0.phone1 is not null then isnull(t0.phone1,'') else isnull(t0.phone2,'') end as phone,isnull(t0.cellular,'') as mobileno, t1.county as district, isnull(t1.state,'') state,t0.U_AreaCode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                            & " left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.cardcode)='" & Trim(flx.get_TextMatrix(i, 1)) & "'"
                        Else
                            msql3 = "select t0.CardCode,t0.CardName,t0.CardFName as mname,isnull(t1.building,'') as add1,isnull(t1.block,'') as add2,isnull(t1.street,'') as add3,isnull(t1.city,'') as add4, " & vbCrLf _
                           & "isnull(t1.zipcode,'') as zipcode,case when t0.phone1 is not null then isnull(t0.phone1,'') else isnull(t0.phone2,'') end as phone,isnull(t0.cellular,'') as mobileno, isnull(t1.county,'') as district, isnull(t1.state,'') state,t0.U_AreaCode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                           & " left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.cardcode)='" & Trim(flx.get_TextMatrix(i, 1)) & "'"
                        End If
                    Else
                        If chkvendor.Checked = False Then
                            msql3 = "select t0.CardCode,t0.CardName,t0.CardFName as mname,isnull(t1.building,'') as add1,isnull(t1.block,'') as add2,isnull(t1.street,'') as add3,isnull(t1.city,'') as add4, " & vbCrLf _
                            & "isnull(t1.zipcode,'') as zipcode,case when t0.phone1 is not null then isnull(t0.phone1,'') else isnull(t0.phone2,'') end as phone,isnull(t0.cellular,'') as mobileno, isnull(t1.county,'') as district, isnull(t1.state,'') state,'' as U_AreaCode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                            & " left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.cardcode)='" & Trim(flx.get_TextMatrix(i, 1)) & "'"
                        Else
                            msql3 = "select t0.CardCode,t0.CardName,t0.CardFName as mname,isnull(t1.building,'') as add1,isnull(t1.block,'') as add2,isnull(t1.street,'') as add3,isnull(t1.city,'') as add4, " & vbCrLf _
                           & "isnull(t1.zipcode,'') as zipcode,case when t0.phone1 is not null then isnull(t0.phone1,'') else isnull(t0.phone2,'') end as phone,isnull(t0.cellular,'') as mobileno, isnull(t1.county,'') as district, isnull(t1.state,'') state,'' as U_AreaCode from " & Trim(mcomp) & ".dbo.ocrd t0 " & vbCrLf _
                           & " left join " & Trim(mcomp) & ".dbo.CRD1 t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.cardcode)='" & Trim(flx.get_TextMatrix(i, 1)) & "'"
                        End If

                    End If

                    'MsgBox(msql3)

                    'Dim CMD3 As New SqlCommand(msql3, con)
                    'Dim DR3 As SqlDataReader
                    'If con.State = ConnectionState.Closed Then
                    '    con.Open()
                    'End If
                    j = 0
                    'Dim DR3 As OleDb.OleDbDataReader
                    'DR3 = CMD3.ExecuteReader
                    'If DR3.HasRows = True Then
                    '    While DR3.Read
                    '        If IsDBNull(DR3.Item("add1")) = False Then
                    '            If Len(Trim(DR3.Item("add1"))) > 0 Then
                    '                mkadd(j) = Trim(DR3.Item("add1"))
                    '                j = j + 1
                    '            End If
                    '        End If

                    '        If IsDBNull(DR3.Item("add2")) = False Then
                    '            If Len(Trim(DR3.Item("add2"))) > 0 Then
                    '                mkadd(j) = Trim(DR3.Item("add2"))
                    '                j = j + 1
                    '            End If
                    '        End If
                    '        If IsDBNull(DR3.Item("add3")) = False Then
                    '            If Len(Trim(DR3.Item("add3"))) > 0 Then
                    '                mkadd(j) = Trim(DR3.Item("add3"))
                    '                j = j + 1
                    '            End If
                    '        End If
                    '        If IsDBNull(DR3.Item("add4")) = False Then
                    '            If Len(Trim(DR3.Item("add4"))) > 0 Then
                    '                mkadd(j) = Trim(DR3.Item("add4")) & "," & Trim(DR3.Item("zipcode"))
                    '                j = j + 1
                    '            End If
                    '        End If

                    '        If IsDBNull(DR3.Item("district")) = False Then
                    '            If Len(Trim(DR3.Item("district"))) > 0 Then

                    '                mkadd(j) = "Dist: " & Trim(DR3.Item("district")) & "," & Trim(DR3.Item("state"))
                    '                j = j + 1
                    '            End If
                    '        End If


                    '        If IsDBNull(DR3.Item("phone")) = False Then
                    '            If Len(Trim(DR3.Item("phone"))) > 0 Then
                    '                mkadd(j) = "Phone: " & Trim(DR3.Item("phone"))
                    '                j = j + 1
                    '            End If
                    '        End If

                    '        If IsDBNull(DR3.Item("mobileno")) = False Then
                    '            If Len(Trim(DR3.Item("mobileno"))) > 0 Then
                    '                'mkadd(j) = "Mobile: " & Trim(DR3.Item("mobileno"))
                    '                mkadd(j) = "Mobile: " & Mid(Trim(DR3.Item("mobileno").ToString), 1, 11)
                    '                j = j + 1
                    '            End If
                    '        End If

                    Dim dts As DataTable = getDataTable(msql3)
                    'j = 0
                    If dts.Rows.Count > 0 Then
                        For Each rws As DataRow In dts.Rows
                            If IsDBNull(rws("add1")) = False Then
                                If Len(Trim(rws("add1"))) > 0 Then
                                    mkadd(j) = Trim(rws("add1"))
                                    j = j + 1
                                End If
                            End If

                            If IsDBNull(rws("add2")) = False Then
                                If Len(Trim(rws("add2"))) > 0 Then
                                    mkadd(j) = Trim(rws("add2"))
                                    j = j + 1
                                End If
                            End If
                            If IsDBNull(rws("add3")) = False Then
                                If Len(Trim(rws("add3"))) > 0 Then
                                    mkadd(j) = Trim(rws("add3"))
                                    j = j + 1
                                End If
                            End If
                            If IsDBNull(rws("add4")) = False Then
                                If Len(Trim(rws("add4"))) > 0 Then
                                    mkadd(j) = Trim(rws("add4")) & "," & Trim(rws("zipcode"))
                                    j = j + 1
                                End If
                            End If

                            If IsDBNull(rws("district")) = False Then
                                If Len(Trim(rws("district"))) > 0 Then

                                    mkadd(j) = "Dist: " & Trim(rws("district")) & "," & Trim(rws("state"))
                                    j = j + 1
                                End If
                            End If


                            If IsDBNull(rws("phone")) = False Then
                                If Len(Trim(rws("phone"))) > 0 Then
                                    mkadd(j) = "Phone: " & Trim(rws("phone"))
                                    j = j + 1
                                End If
                            End If

                            If IsDBNull(rws("mobileno")) = False Then
                                If Len(Trim(rws("mobileno"))) > 0 Then
                                    'mkadd(j) = "Mobile: " & Trim(DR3.Item("mobileno"))
                                    mkadd(j) = "Mobile: " & Mid(Trim(rws("mobileno").ToString), 1, 11)
                                    j = j + 1
                                End If
                            End If


                            'Next


                            If chkbox.Checked = True Then
                                PrintLine(1, TAB(0), "^FX Precompiled file for ID_100A.BCB width (pixels)=608 height (pixels)=358^FS")
                                PrintLine(1, TAB(0), "^FX Scale factors: xf=1.000 yf=1.000 orientation=0 scaled width (bytes)=76 scaled height (bytes)=358^FS")
                                PrintLine(1, TAB(0), "~DGID_100A ,27208,76,")
                                PrintLine(1, TAB(0), "H0mJFE,")
                                PrintLine(1, TAB(0), "07mKFE,")
                                PrintLine(1, TAB(0), "1mMF,")
                                PrintLine(1, TAB(0), "3mMFC,")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), "7mMFE,")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), "mNFE,")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), "IFCmG07HFE,")
                                For l = 1 To 330
                                    PrintLine(1, TAB(0), ":")
                                Next l
                                PrintLine(1, TAB(0), "mNFE,")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), "7mMFE,")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), "3mMFC,")
                                PrintLine(1, TAB(0), ":")
                                PrintLine(1, TAB(0), "1mMF,")
                                PrintLine(1, TAB(0), "07mKFE,")
                            End If

                            PrintLine(1, TAB(0), "^XA")
                            PrintLine(1, TAB(0), "^PRC")
                            PrintLine(1, TAB(0), "^LH0,0^FS")
                            PrintLine(1, TAB(0), "^LL360")
                            PrintLine(1, TAB(0), "^MD5")
                            PrintLine(1, TAB(0), "^MNY")
                            PrintLine(1, TAB(0), "^LH0,0^FS")

                            'PrintLine(1, TAB(0), "^FO153,64^A0N,31,28^CI13^FR^FD" & IIf(chkperson.Checked = False, "M/s.", "") & Trim(DR3.Item("mname")) & vbNullString & "^FS;")
                            PrintLine(1, TAB(0), "^FO153,62^A0N,27,28^CI13^FR^FD" & IIf(chkperson.Checked = False, "M/s.", "") & Trim(rws("mname")) & vbNullString & "^FS;")

                            'mlrow = 104
                            mlrow = 92
                            For k = 0 To j - 1
                                'PrintLine(1, TAB(0), "^FO153," & Trim(Str(mlrow)) & "^A0N,32,28^CI13^FR^FD" & Trim(mkadd(k)) & vbNullString & "^FS")
                                'mlrow = mlrow + 40
                                If k = 1 Then
                                    mlrow = mlrow - 4
                                End If
                                PrintLine(1, TAB(0), "^FO153," & Trim(Str(mlrow)) & "^A0N,27,28^CI13^FR^FD" & Trim(mkadd(k)) & vbNullString & "^FS")

                                mlrow = mlrow + 30

                            Next


                            If chkwinv.Checked = True Then
                                PrintLine(1, TAB(0), "^BY2,3.0^FO153,299^BCN,32,N,Y,N^FR^FD" & Trim(.get_TextMatrix(i, 5)) & vbNullString & "^FS")
                                PrintLine(1, TAB(0), "^FO453,299^A0N,32,28^CI13^FR^FD" & Trim(.get_TextMatrix(i, 5)) & "^FS")
                                'PrintLine(1, TAB(0), "^BY2,3.0^FO153,299^BCN,32,N,Y,N^FR^FD" & Trim(DR3.Item("cardcode")) & vbNullString & "^FS")
                            Else
                                If IsDBNull(rws("cardcode")) = False Then
                                    If Len(Trim(rws("cardcode"))) > 0 Then
                                        PrintLine(1, TAB(0), "^BY2,3.0^FO153,299^BCN,32,N,Y,N^FR^FD" & Trim(rws("cardcode")) & vbNullString & "^FS")
                                        '^BY2,3.0^FO153,299^BCN,32,N,Y,N^FR^FDC010226^FS'
                                    End If
                                End If
                            End If


                            PrintLine(1, TAB(0), "^FO125,27^A0N,32,28^CI13^FR^FDTO.^FS")
                            If chkbox.Checked = True Then
                                PrintLine(1, TAB(0), "^FO108,0^XGID_100A ,1,1^FS")
                            End If
                            PrintLine(1, TAB(0), "^PQ1,0,0,N")
                            PrintLine(1, TAB(0), "^XZ")
                            'PrintLine(1, TAB(0), DR.Item("firstdet"))
                        Next
                    End If
                    For k = 0 To 5
                        mkadd(k) = ""
                    Next
                    mlrow = 0

                End If
            Next i

        End With
        'DR3.Close()
        'CMD3.Dispose()

        'If rs.State = 1 Then rs.Close()
        'ADOCON = Nothing
        FileClose(1)

        If mos = "WIN" Then
            If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                'Shell("cmd.exe /c" & "type " & mdir & " > lpt" & Trim(txtport.Text))
                Shell("rawpr.bat " & mdir)
                'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
            End If
        Else
            'Dim printer As String = mprinter
            ''Dim filePath As String = mlinpath & "nsbarcodEV.txt"
            ''"/home/testing/Desktop/Barcodelinux/nsbarcodEV.txt"
            'Dim filePath As String = mlinpath
            'Dim filePathname As String = mlinpath & "sticker.txt"

            'Dim psi As New ProcessStartInfo()
            'psi.FileName = "/bin/bash"
            'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""
            ''psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
            'psi.UseShellExecute = False
            'psi.CreateNoWindow = True
            'Process.Start(psi)

            Dim printer As String = tscprinter1
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "sticker.txt"
            Dim success As Boolean = PrintTscRaw(printer, filePathname)

        End If





    End Sub

    Private Sub txtpodno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpodno.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If Len(Trim(txtpodno.Text)) > 0 Then
                If Val(txtno.Text) > 0 Or Len(Trim(txtcardcode.Text)) > 0 Then
                    If chkprn.Checked = False Then
                        podsave()
                    Else
                        If Len(Trim(txtcardcode.Text)) > 0 Then
                            Call crystal()
                        End If

                    End If
                End If
            End If


            'If minvt = True Then
            '    txtno.Focus()
            'ElseIf minvt = False Then
            '    txtcardcode.Focus()
            'End If
        End If
    End Sub

    Private Sub txtpodno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpodno.LostFocus
        'MsgBox(minvt)
        'MsgBox(txtno.Text)
        'MsgBox(txtpodno.Text)
        'If Len(Trim(txtpodno.Text)) > 0 Then
        '    If Val(txtno.Text) > 0 Or Len(Trim(txtcardcode.Text)) > 0 Then
        '        podsave()
        '    End If
        'End If
        'Else
        '/Call podsave()
        'If chkprn.Checked = True Then
        '    If Len(Trim(txtcardcode.Text)) > 0 Then
        '        Call crystal()
        '    End If
        'End If


    End Sub

    Private Sub txtpodno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpodno.TextChanged

    End Sub

    Private Sub txtno_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtno.TabIndexChanged

    End Sub


    Private Sub txtno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtno.TextChanged

    End Sub

    Private Sub cmdrep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdrep.Click
        Call loadrep()
    End Sub
    Private Sub loadrep()
        'Dim ds As New DataSet
        'Dim da1 As OleDb.OleDbDataAdapter
        sql = "select  * from " & Trim(mcomp) & ".dbo.courier where date='" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' order by docnum"
        'If con.State = ConnectionState.Closed Then
        ' con.Open()
        ' End If
        'da1 = New OleDb.OleDbDataAdapter(sql, con)
        'da1.Fill(ds, "courier")
        Dim dtt As DataTable = getDataTable(sql)
        ''msql = "select city  from  " & Trim(mcomp) & ".dbo.crd1 where city is not null group by city order by city"
        'Dim CMDr As New SqlCommand(sql, con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If
        Call flxhead2()
        Try
            ''Dim DR As SqlDataReader
            'Dim DRp As SqlDataReader
            'DRp = CMDr.ExecuteReader
            'If DRp.HasRows = True Then
            If dtt.Rows.Count > 0 Then
                For Each rww As DataRow In dtt.Rows
                    With flx
                        .Rows = .Rows + 1
                        .Row = .Rows - 1

                        .set_TextMatrix(.Row, 0, rww("date"))
                        .set_TextMatrix(.Row, 1, rww("Company"))
                        If IsDBNull(rww("docnum")) = False Then
                            .set_TextMatrix(.Row, 2, rww("docnum"))
                            .set_TextMatrix(.Row, 5, loaddest(rww("docentry")) & vbNullString)
                        Else
                            .set_TextMatrix(.Row, 2, rww("cardcode"))
                        End If

                        .set_TextMatrix(.Row, 3, rww("Cardname") & vbNullString)
                        .set_TextMatrix(.Row, 4, rww("podno") & vbNullString)

                        'cmbcity.Items.Add(DR.Item("city"))
                    End With

                Next

            End If
            'DRp.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            cmbagent.Items.Clear()
            'flx.Clear()
            'Call flxchead()
        End Try

        'CMDr.Dispose()


    End Sub


    Private Sub cmdexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexcel.Click
        'exeltoflx(flx)
        excelexport(flx)
    End Sub

    Private Sub excelcon(ByVal xlver As String, ByVal mkpath As String)
        'Dim mm As String
        'If xcon.State = ConnectionState.Open Then xcon.Close()
        'xcon.ConnectionString = ""
        'If Long.Parse(xlver) >= 2007 Then
        '    'xcon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & txtparth.text & ";Extended Properties=" & """Excel 8.0;HDR=Yes;IMEX=1"""
        '    If Long.Parse(xlver) = 2017 Then
        '        xcon.ConnectionString = "Provider=Microsoft.ACE.OLEDB.15.0;Data Source=" & Trim(mkpath) & ";Extended Properties=Excel 15.0;HDR=YES"
        '    Else
        '        xcon.ConnectionString = "Provider=Microsoft.ACE.OLEDB.14.0;Data Source=" & Trim(mkpath) & ";Extended Properties=Excel 14.0;HDR=YES"
        '        'xcon.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Trim(mkpath) & ";Extended Properties=Excel 12.0;HDR=YES"
        '    End If


        '    'xcon.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mkpath + ";Extended Properties=" + Convert.ToChar(34).ToString() + "Excel 12.0 Xml;HDR=Yes" + Convert.ToChar(34).ToString()

        'Else
        '    'mm = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mkpath + ";Extended Properties=" + Convert.ToChar(34).ToString() + "Excel 8.0;HDR=Yes;IMEX=1" + Convert.ToChar(34).ToString()
        '    'xcon.ConnectionString = "Microsoft.Jet.OLEDB.4.0;Data Source=" & Trim(mkpath) & ";Extended Properties=Excel 8.0;HDR=YES;IMEX=1"
        '    xcon.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & Trim(mkpath) & ";Extended Properties=Excel 8.0"

        '    'xcon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & trim(mkpath) & ";Extended Properties=" & """ & "Excel 8.0;HDR=Yes" & """
        'End If

        '    'constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Trim(txtfile.Text) & ";Extended Properties=Excel 12.0"

        '    'constr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & Trim(txtfile.Text) & ";Extended Properties=Excel 8.0"


        '    'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\MyExcel.xls;Extended Properties="Excel 8.0;HDR=Yes;IMEX=1";
    End Sub

    Private Sub xlstickerprn2()
        Dim dir As String
        excelcon(cmbver.Text, txtfile.Text)
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "sticker.txt"

        FileOpen(1, mdir, OpenMode.Output)
        'End If
        mlin = 1

        Dim dtx As DataTable = ReadExcelAnyOS(Trim(txtfile.Text))
        'CardCode,CardName, mname,add1,add2,add3,add4,zipcode,phone,mobileno,state,district

        dtx.Columns(0).ColumnName = "cardcode"
        dtx.Columns(1).ColumnName = "cardname"
        dtx.Columns(2).ColumnName = "mname"
        dtx.Columns(3).ColumnName = "add1"
        dtx.Columns(4).ColumnName = "add2"
        dtx.Columns(5).ColumnName = "add3"
        dtx.Columns(6).ColumnName = "add4"
        dtx.Columns(7).ColumnName = "zipcode"
        dtx.Columns(8).ColumnName = "district"
        dtx.Columns(9).ColumnName = "state"
        dtx.Columns(10).ColumnName = "phone"
        dtx.Columns(11).ColumnName = "mobileno"
        'dtx.Columns(10).ColumnName = "state"
        'dtx.Columns(11).ColumnName = "district"
        'msql3 = "select CardCode,CardName, mname,add1,add2,add3,add4,zipcode,phone,mobileno,state,district from [sheet1$] "
        ''& "zipcode as zipcode,phone,state from [sheet1$] "

        ''& " left join "[Sheet1$] t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.cardcode)='" & Trim(flx.get_TextMatrix(i, 1)) & "'"

        'Dim CMD3 As New OleDb.OleDbCommand(msql3, xcon)
        'Dim DR3 As OleDb.OleDbDataReader
        'If xcon.State = ConnectionState.Closed Then
        '    xcon.Open()
        'End If

        j = 0
        'Dim DR3 As OleDb.OleDbDataReader
        'DR3 = CMD3.ExecuteReader
        'If DR3.HasRows = True Then
        If dtx.Rows.Count > 0 Then

            'While DR3.Read
            For Each rw As DataRow In dtx.Rows
                If IsDBNull(rw("cardcode")) = False Then

                    If IsDBNull(rw("add1")) = False Then
                        If Len(Trim(rw("add1"))) > 0 Then
                            mkadd(j) = Trim(rw("add1"))
                            j = j + 1
                        End If
                    End If

                    If IsDBNull(rw("add2")) = False Then
                        If Len(Trim(rw("add2"))) > 0 Then
                            mkadd(j) = Trim(rw("add2"))
                            j = j + 1
                        End If
                    End If
                    If IsDBNull(rw("add3")) = False Then
                        If Len(Trim(rw("add3"))) > 0 Then
                            mkadd(j) = Trim(rw("add3"))
                            j = j + 1
                        End If
                    End If

                    If IsDBNull(rw("add4")) = False Then
                        If Len(Trim(rw("add4"))) > 0 Then
                            If IsDBNull(rw("zipcode")) = False Then
                                mkadd(j) = Trim(rw("add4")) & "," & Trim(rw("zipcode")) & vbNullString
                            Else
                                mkadd(j) = Trim(rw("add4"))
                            End If
                            j = j + 1
                        End If
                    End If

                    If IsDBNull(rw("district")) = False Then
                        If Len(Trim(rw("district"))) > 0 Then
                            'mkadd(j) = Trim(DR3.Item("district"))
                            'mkadd(j) = "Dist: " & Trim(DR3.Item("district"))
                            mkadd(j) = "Dist: " & Trim(rw("district")) & "," & Trim(rw("state"))
                            j = j + 1
                        End If
                    End If


                    If IsDBNull(rw("phone")) = False Then
                        If Len(Trim(rw("phone"))) > 0 Then
                            mkadd(j) = "Phone: " & Trim(rw("phone"))
                            j = j + 1
                        End If
                    End If

                    If IsDBNull(rw("mobileno")) = False Then
                        If Len(Trim(rw("mobileno"))) > 0 Then
                            mkadd(j) = "Mobile: " & Mid(Trim(rw("mobileno").ToString), 1, 11)
                            j = j + 1
                        End If
                    End If


                End If



                If chkbox.Checked = True Then
                    PrintLine(1, TAB(0), "^FX Precompiled file for ID_100A.BCB width (pixels)=608 height (pixels)=358^FS")
                    PrintLine(1, TAB(0), "^FX Scale factors: xf=1.000 yf=1.000 orientation=0 scaled width (bytes)=76 scaled height (bytes)=358^FS")
                    PrintLine(1, TAB(0), "~DGID_100A ,27208,76,")
                    PrintLine(1, TAB(0), "H0mJFE,")
                    PrintLine(1, TAB(0), "07mKFE,")
                    PrintLine(1, TAB(0), "1mMF,")
                    PrintLine(1, TAB(0), "3mMFC,")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), "7mMFE,")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), "mNFE,")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), "IFCmG07HFE,")
                    For l = 1 To 330
                        PrintLine(1, TAB(0), ":")
                    Next l
                    PrintLine(1, TAB(0), "mNFE,")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), "7mMFE,")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), "3mMFC,")
                    PrintLine(1, TAB(0), ":")
                    PrintLine(1, TAB(0), "1mMF,")
                    PrintLine(1, TAB(0), "07mKFE,")
                End If

                PrintLine(1, TAB(0), "^XA")
                PrintLine(1, TAB(0), "^PRC")
                PrintLine(1, TAB(0), "^LH0,0^FS")
                PrintLine(1, TAB(0), "^LL360")
                PrintLine(1, TAB(0), "^MD5")
                PrintLine(1, TAB(0), "^MNY")
                PrintLine(1, TAB(0), "^LH0,0^FS")

                '    PrintLine(1, TAB(0), "^FO153,64^A0N,31,28^CI13^FR^FD" & IIf(chkperson.Checked = False, "M/s.", "") & Trim(DR3.Item("mname")) & vbNullString & "^FS;")
                'mlrow = 104
                PrintLine(1, TAB(0), "^FO153,62^A0N,27,28^CI13^FR^FD" & IIf(chkperson.Checked = False, "M/s.", "") & Trim(rw("mname") & vbNullString) & "^FS;")
                mlrow = 92

                For k = 0 To j - 1
                    '    PrintLine(1, TAB(0), "^FO153," & Trim(Str(mlrow)) & "^A0N,32,28^CI13^FR^FD" & Trim(mkadd(k)) & vbNullString & "^FS")
                    'mlrow = mlrow + 40
                    If k = 1 Then
                        mlrow = mlrow - 4
                    End If
                    PrintLine(1, TAB(0), "^FO153," & Trim(Str(mlrow)) & "^A0N,27,28^CI13^FR^FD" & Trim(mkadd(k)) & vbNullString & "^FS")
                    mlrow = mlrow + 30

                Next



                If IsDBNull(rw.Item("cardcode")) = False Then
                    If Len(Trim(rw.Item("cardcode"))) > 0 Then
                        PrintLine(1, TAB(0), "^BY2,3.0^FO153,299^BCN,32,N,Y,N^FR^FD" & Trim(rw("cardcode")) & vbNullString & "^FS")

                        '^BY2,3.0^FO153,299^BCN,32,N,Y,N^FR^FDC010226^FS'
                    End If
                End If

                PrintLine(1, TAB(0), "^FO125,27^A0N,32,28^CI13^FR^FDTO.^FS")
                If chkbox.Checked = True Then
                    PrintLine(1, TAB(0), "^FO108,0^XGID_100A ,1,1^FS")
                End If
                PrintLine(1, TAB(0), "^PQ1,0,0,N")
                PrintLine(1, TAB(0), "^XZ")
                j = 0
                'PrintLine(1, TAB(0), DR.Item("firstdet"))
            Next
            'End While
        End If
            For k = 0 To 5
            mkadd(k) = ""
        Next
        j = 0
        mlrow = 0
        'DR3.Close()
        'CMD3.Dispose()

        FileClose(1)

        'If chkdirprn.Checked = False Then
        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If mos = "WIN" Then
                Shell("rawpr.bat " & mdir)
                'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
            Else
                'Dim printer As String = mprinter
                'Dim filePath As String = mlinpath
                'Dim filePathname As String = mlinpath & "sticker.txt"
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
                Dim filePathname As String = mlinpath & "sticker.txt"
                Dim success As Boolean = PrintTscRaw(printer, filePathname)

            End If

        End If




    End Sub



    Private Sub butbrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butbrowse.Click
        Dim fs As FileStream = New FileStream(Application.StartupPath & "\history.log", FileMode.OpenOrCreate, FileAccess.Write)
        Dim sw As StreamWriter = New StreamWriter(fs)
        Dialog1.Filter = "Excel (*.xls)|*.xls| Excel Files(.xlsx)|*.xlsx| Excel Files(*.xlsm)|*.xlsm"
        Dialog1.ShowDialog()
        txtfile.Text = Dialog1.FileName
        'Me.View1.ReportSource = Dialog1.FileName
        If File.Exists(Application.StartupPath & "\history.log") = False Then
            File.Create(Application.StartupPath & "\history.log")
        End If
        sw.BaseStream.Seek(0, SeekOrigin.End)
        'sw.NewLine.Insert(0, Dialog1.FileName)
        sw.WriteLine(Dialog1.FileName)
        sw.Flush()
        sw.Close()
    End Sub

    Private Sub butsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butsearch.Click


        msql3 = "select * from courier "
        If Len(Trim(txtpodno.Text)) > 0 Or Len(Trim(txtcardcode.Text)) > 0 Or Len(Trim(txtremark.Text)) > 0 Then
            msql3 = Trim(msql3) & " Where "
        End If
        If Len(Trim(txtpodno.Text)) > 0 And Len(Trim(txtcardcode.Text)) > 0 And Len(Trim(txtremark.Text)) > 0 Then
            msql3 = Trim(msql3) & " cardcode='" & txtcardcode.Text & "' and podno='" & txtpodno.Text & "' and remark='" & txtremark.Text & "'"
        End If
        If Len(Trim(txtpodno.Text)) > 0 And Len(Trim(txtcardcode.Text)) = 0 And Len(Trim(txtremark.Text)) = 0 Then
            msql3 = Trim(msql3) & " rtrim(podno)='" & Trim(txtpodno.Text) & "'"
        End If
        If Len(Trim(txtpodno.Text)) = 0 And Len(Trim(txtcardcode.Text)) > 0 And Len(Trim(txtremark.Text)) = 0 Then
            msql3 = Trim(msql3) & " cardcode='" & txtcardcode.Text & "'"
        End If
        If Len(Trim(txtpodno.Text)) = 0 And Len(Trim(txtcardcode.Text)) = 0 And Len(Trim(txtremark.Text)) > 0 Then
            msql3 = Trim(msql3) & " remark='" & txtremark.Text & "'"
        End If

        If Len(Trim(txtpodno.Text)) > 0 And Len(Trim(txtcardcode.Text)) > 0 And Len(Trim(txtremark.Text)) = 0 Then
            msql3 = Trim(msql3) & " cardcode='" & txtcardcode.Text & "' and podno='" & txtpodno.Text & "'"
        End If

        If Len(Trim(txtpodno.Text)) > 0 And Len(Trim(txtcardcode.Text)) = 0 And Len(Trim(txtremark.Text)) > 0 Then
            msql3 = Trim(msql3) & " remark='" & txtremark.Text & "' and podno='" & txtpodno.Text & "'"
        End If

        If Len(Trim(txtcardcode.Text)) > 0 And Len(Trim(txtremark.Text)) > 0 Then
            msql3 = Trim(msql3) & " cardcode='" & txtcardcode.Text & "' and remark='" & txtremark.Text & "'"
        End If


        '& "zipcode as zipcode,phone,state from [sheet1$] "


        '& " left join "[Sheet1$] t1 on t1.CardCode=t0.CardCode and t1.AdresType='S' where rtrim(t0.cardcode)='" & Trim(flx.get_TextMatrix(i, 1)) & "'"

        Dim CMD4 As New SqlCommand(msql3, con)


        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        'Dim DR4 As OleDb.OleDbDataReader

        Call flxthead()
        j = 0
        'Dim DR3 As OleDb.OleDbDataReader
        Dim dt As DataTable = getDataTable(msql3)
        'Dim DR4 As SqlDataReader
        'DR4 = CMD4.ExecuteReader

        'If DR4.HasRows = True Then
        If dt.Rows.Count > 0 Then

                With flxt
                'While DR4.Read
                For Each rw As DataRow In dt.Rows
                        flxt.Rows = flxt.Rows + 1
                        flxt.Row = flxt.Rows - 1
                    flxt.set_TextMatrix(flxt.Row, 0, rw("date"))
                    flxt.set_TextMatrix(flxt.Row, 1, rw("Company") & vbNullString)
                    flxt.set_TextMatrix(flxt.Row, 2, rw("cardcode") & vbNullString)
                    flxt.set_TextMatrix(flxt.Row, 3, rw("cardname") & vbNullString)
                    flxt.set_TextMatrix(flxt.Row, 4, rw("podno") & vbNullString)
                    flxt.set_TextMatrix(flxt.Row, 5, rw("remark") & vbNullString)
                    flxt.set_TextMatrix(flxt.Row, 6, rw("docnum") & vbNullString)
                Next
                'End While
                End With
            End If
        'DR4.Close()
        CMD4.Dispose()

    End Sub

    Private Sub flxthead()

        flxt.Rows = 1
        flxt.Cols = 7
        flxt.set_ColWidth(0, 1000)
        flxt.set_ColWidth(1, 1200)
        flxt.set_ColWidth(2, 1500)
        flxt.set_ColWidth(3, 1500)
        flxt.set_ColWidth(4, 1200)
        flxt.set_ColWidth(5, 1200)
        flxt.set_ColWidth(6, 1200)

        'flxh.set_ColWidth(3, 15)

        flxt.Row = 0
        flxt.Col = 0
        flxt.CellAlignment = 3
        flxt.CellFontBold = True
        flxt.set_TextMatrix(0, 0, "Date")

        flxt.Col = 1
        flxt.CellAlignment = 3
        flxt.CellFontBold = True
        flxt.set_TextMatrix(0, 1, "Company")

        flxt.Col = 2
        flxt.CellAlignment = 3
        flxt.CellFontBold = True
        flxt.set_TextMatrix(0, 2, "Cardcode")
        flxt.Col = 3
        flxt.CellAlignment = 3
        flxt.CellFontBold = True
        flxt.set_TextMatrix(0, 3, "Card Name")

        flxt.Col = 4
        flxt.CellAlignment = 3
        flxt.CellFontBold = True
        flxt.set_TextMatrix(0, 4, "POD No")
        flxt.Col = 5
        flxt.CellAlignment = 3
        flxt.CellFontBold = True
        flxt.set_TextMatrix(0, 5, "Remark")
        flxt.Col = 6
        flxt.CellAlignment = 3
        flxt.CellFontBold = True
        flxt.set_TextMatrix(0, 6, "Docnum")


    End Sub

    Private Sub butsrchexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butsrchexcel.Click
        excelexport(flxt)
    End Sub

    Private Sub CMBYR_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBYR.SelectedIndexChanged

    End Sub

    Private Sub crystalrr()

        'Me.Cursor = Cursors.WaitCursor


        'cryRpt.SetParameterValue("@Dockey", Val(txtdocentry.Text))

        'Me.view1.ReportSource = cryRpt
        'Me.view1.PrintReport()

        'Me.view1.Refresh()

        'Me.Cursor = Cursors.Default
        'If chkprn.Checked = True Then chkprn.Checked = False
    End Sub

    Private Sub crystal()

        'Me.Cursor = Cursors.WaitCursor

        'If chkexcel.Checked = True Then
        '    cryRptex.SetParameterValue("@cardcode", txtcardcode.Text)
        '    cryRptex.SetParameterValue("@podno", txtpodno.Text)
        '    Me.view1.ReportSource = cryRptex
        'Else

        '    cryRpt2.SetParameterValue("@cardcode", txtcardcode.Text)
        '    cryRpt2.SetParameterValue("@podno", txtpodno.Text)
        '    Me.view1.ReportSource = cryRpt2
        'End If



        'Me.view1.PrintReport()

        'Me.view1.Refresh()
        ''cryRpt2.Dispose()
        'Me.Cursor = Cursors.Default

    End Sub


    Private Sub chkexcel_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkexcel.CheckedChanged

    End Sub

    Private Sub chkprn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkprn.CheckedChanged

    End Sub

    Private Sub txtport2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtport2.TextChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim dir As String
        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "cmpysticker.txt"

        FileOpen(1, mdir, OpenMode.Output)

        mlin = 1

        PrintLine(1, TAB(0), "^XA")
        PrintLine(1, TAB(0), "^PRC")
        PrintLine(1, TAB(0), "^LH0,0^FS")
        PrintLine(1, TAB(0), "^LL360")
        PrintLine(1, TAB(0), "^MD5")
        PrintLine(1, TAB(0), "^MNY")
        PrintLine(1, TAB(0), "^LH0,0^FS")
        PrintLine(1, TAB(0), "^FO153,62^A0N,27,28^CI13^FR^FD" & "ATITHYA CLOTHING COMPANY" & "^FS;")
        mlrow = 92

        'mlrow = mlrow - 4

        PrintLine(1, TAB(0), "^FO153," & Trim(Str(mlrow)) & "^A0N,27,28^CI13^FR^FD" & "(Unit of ENES TEXTILE MILLS" & "^FS")
        mlrow = mlrow + 30
        mlrow = mlrow - 4
        PrintLine(1, TAB(0), "^FO153," & Trim(Str(mlrow)) & "^A0N,27,28^CI13^FR^FD" & "No.2/453,SVD Nagar" & "^FS")
        mlrow = mlrow + 30
        PrintLine(1, TAB(0), "^FO153," & Trim(Str(mlrow)) & "^A0N,27,28^CI13^FR^FD" & "Kovilpappakudi Village" & "^FS")
        mlrow = mlrow + 30
        PrintLine(1, TAB(0), "^FO153," & Trim(Str(mlrow)) & "^A0N,27,28^CI13^FR^FD" & "MADURAI - 625018" & "^FS")
        mlrow = mlrow + 30
        PrintLine(1, TAB(0), "^FO153," & Trim(Str(mlrow)) & "^A0N,27,28^CI13^FR^FD" & "Ph.No.7373024147,9688566147" & "^FS")
        mlrow = mlrow + 30

        If Val(txtnocopy.Text) > 0 Then
            PrintLine(1, TAB(0), "^PQ" & Trim(txtnocopy.Text) & ",0,0,N")
        Else
            PrintLine(1, TAB(0), "^PQ1,0,0,N")
        End If
        'PrintLine(1, TAB(0), "^PQ1,0,0,N")
        PrintLine(1, TAB(0), "^XZ")
        mlrow = 0
        FileClose(1)

        'If chkdirprn.Checked = False Then
        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            If mos = "WIN" Then
                'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                Shell("cmd.exe /c" & "type " & mdir & " > lpt" & Trim(txtport.Text))
                'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
            Else
                'Dim printer As String = mprinter
                'Dim filePath As String = mlinpath
                'Dim filePathname As String = mlinpath & "cmpysticker.txt"
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
                Dim filePathname As String = mlinpath & "cmpysticker.txt"
                Dim success As Boolean = PrintTscRaw(printer, filePathname)

            End If

        End If

    End Sub

    Private Sub txtcourierno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcourierno.TextChanged

    End Sub


    Private Sub courprn(docentry As Integer, destination As String, podno As String)
        Dim sql3 As String
        mto = destination
        'lpodno = "*" & Trim(podno) & "*"
        lpodno = Trim(podno)
        'sql3 = "select a.CardCode,a.CardFName,a.CardName,b.Building,b.Block,b.Street,b.City,b.ZipCode,b.county,b.country,b.state," _
        '       & " c.CompnyName,d.Building Cbuilding,b.Block Cblock,d.Street cstreet,d.City ccity,d.ZipCode czipcode,d.state cstate,d.county ccounty,d.country ccountry,e.DocNum,e.podno,i.DocDate " _
        '       & " from OCRD A " _
        '       & " inner join CRD1 b on b.CardCode = a.CardCode and b.AdresType ='S' " _
        '       & " inner join OINV i on i.CardCode=a.CardCode " _
        '       & "inner join courier e on e.docentry=i.DocEntry , " _
        '       & " OADM c,ADM1 d " _
        '       & " where e.DocEntry = " & docentry


        sql3 = "select i.docnum, a.CardCode,a.CardFName,a.CardName,b.Building,b.Block,b.Street,b.City,b.ZipCode,b.county,b.country,b.state," _
              & " c.CompnyName,d.Building Cbuilding,b.Block Cblock,d.Street cstreet,d.City ccity,d.ZipCode czipcode,d.state cstate,d.county ccounty,d.country ccountry,i.DocNum,i.DocDate,i.u_noofbun,i.U_LR_Weight " _
              & " from OCRD A " _
              & " inner join CRD1 b on b.CardCode = a.CardCode and b.AdresType ='S' " _
              & " inner join OINV i on i.CardCode=a.CardCode " _
              & " ,OADM c,ADM1 d " _
              & " where i.DocEntry = " & docentry


        '        sql3 = "select a.CardCode,CardFName,a.CardName,b.Building,b.Block,b.Street,b.City,b.ZipCode,b.county,b.country,b.state, " _
        '            c.CompnyName,d.Building Cbuilding,b.Block Cblock,d.Street cstreet,d.City ccity,d.ZipCode czipcode,d.state cstate,d.county ccounty,d.country ccountry,e.DocNum,e.podno,i.DocDate
        ' from OCRD A
        'Left join CRD1 b on b.CardCode = a.CardCode and b.AdresType ='S'
        'left join OINV i on i.CardCode=a.CardCode
        'left join courier e on e.docentry=i.DocEntry
        ',
        'OADM c,ADM1 d
        'where i.DocEntry={?@dockey}

        'excel cardcode
        'select e.cardcode,e.cardfname,e.cardname,e.add1 building,e.add2 block,e.add3 street,e.city,e.zipcode,'' county,'IN' country,e.state,
        'c.CompnyName, d.Building Cbuilding, d.Block Cblock, d.Street cstreet, d.City ccity, d.ZipCode czipcode, d.state cstate, d.county ccounty, d.country ccountry, E.podno, E.remark
        'From courier e,OADM c, ADM1 d
        'where E.cardcode ='{?@cardcode}' and e.podno='{?@podno}'



        Dim dtt As DataTable = getDataTable(sql3)
        If dtt.Rows.Count > 0 Then
            mcardname = Trim(dtt.Rows(0)("cardname"))
            mbuild = Trim(dtt.Rows(0)("building"))
            mblock = Trim(dtt.Rows(0)("block"))
            mstreet = Trim(dtt.Rows(0)("street"))
            mcity = Trim(dtt.Rows(0)("city"))
            mzipcode = Trim(dtt.Rows(0)("zipcode"))
            mdistrict = Trim(dtt.Rows(0)("county"))
            mstate = Trim(dtt.Rows(0)("state"))
            mcountry = Trim(dtt.Rows(0)("country"))
            mdocnum = Trim(dtt.Rows(0)("docnum"))
            'mnobund = Trim(dtt.Rows(0)("u_noofbun"))
            'mkwgt = Convert.ToDouble(dtt.Rows(0)("U_LR_Weight"))
        Else
            mbuild = ""
            mblock = ""
            mstreet = ""
            mcity = ""
            mzipcode = ""
            mdistrict = ""
            mstate = ""
            mdistrict = ""
            mstate = ""
            mcountry = ""
            mnobund = 0
            mkwgt = 0.0

        End If

        'PrintPreviewDialog1.Document = PD
        'PrintPreviewDialog1.ShowDialog()   ' For preview
        PD.PrinterSettings.PrinterName = mlsprinter

        If PD.PrinterSettings.IsValid = False Then
            MsgBox("Printer not found!", vbExclamation)
            Exit Sub
        End If
        PD.Print()
    End Sub

    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        Dim width As Integer = CInt(21 / 2.54 * 100)   ' 21 cm
        Dim height As Integer = CInt(10 / 2.54 * 100)  ' 10 cm

        'PD.DefaultPageSettings.PaperSize = New PaperSize("CourierSlip", width, height)
        'PD.DefaultPageSettings.Margins = New Margins(1, 1, 1, 1)
        'Dim ps As New PaperSize("CourierSlip", width, height)
        Dim ps As New PaperSize("CourierSlip", width, width)
        With PD.DefaultPageSettings
            .PaperSize = ps
            .Margins = New Margins(1, 1, 1, 1)
            .Landscape = False    ' *** THIS FORCES PORTRAIT ***
        End With

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        ' --- FONTS ---
        Dim f10 As New Font("Arial", 9, FontStyle.Regular)
        Dim f12B As New Font("Arial", 9, FontStyle.Bold)
        Dim f9 As New Font("Arial", 9, FontStyle.Bold)
        Dim f14B As New Font("Arial", 10, FontStyle.Bold)

        g.DrawString(Trim(mto), f12B, Brushes.Black, 425, 22)
        'g.DrawString(mnobund, f12B, Brushes.Black, 710, 22)
        g.DrawString(DateTime.Now.ToString("dd-MM-yyyy"), f9, Brushes.Black, 210, 23)


        ' --- DRAW TEXT IN COORDINATES (X,Y) ---
        ' Left sender block
        g.DrawString("Atithya Clothing Company", f12B, Brushes.Black, 45, 50)
        g.DrawString("(A Unit of ENES Textile Mills)", f10, Brushes.Black, 45, 65)
        g.DrawString("Kovilpappakudi", f10, Brushes.Black, 45, 80)
        g.DrawString("Madurai - 625018", f10, Brushes.Black, 45, 95)
        g.DrawString("TN, IN", f10, Brushes.Black, 45, 110)

        ' From + Date block
        'g.DrawString("From: IXM", f12B, Brushes.Black, 250, 30)

        Dim x As Integer = 370
        Dim y As Integer = 50
        Dim lineGap As Integer = 15       ' gap between lines

        ' Helper function to print only if not empty
        'Dim PrintIfNotEmpty As Action(Of String, Font) =
        '  Sub(value As String, font As Font)
        '      If Not String.IsNullOrWhiteSpace(value) Then
        '          g.DrawString(value, font, Brushes.Black, x, y)
        '          y += lineGap
        '      End If
        '  End Sub

        Dim maxLineLength As Integer = 30

        Dim PrintIfNotEmpty As Action(Of String, Font) =
            Sub(value As String, font As Font)
                If Not String.IsNullOrWhiteSpace(value) Then
                    ' Split the text into chunks of maxLineLength
                    Dim startIndex As Integer = 0
                    While startIndex < value.Length
                        Dim length = Math.Min(maxLineLength, value.Length - startIndex)
                        Dim lineText = value.Substring(startIndex, length)
                        g.DrawString(lineText, font, Brushes.Black, x, y)
                        y += lineGap
                        startIndex += length
                    End While
                End If
            End Sub



        PrintIfNotEmpty(mcardname, f12B)
        PrintIfNotEmpty(mbuild, f10)
        PrintIfNotEmpty(mblock, f10)
        g.DrawString(mkwgt.ToString("####0.000"), f12B, Brushes.Black, 700, 65)
        PrintIfNotEmpty(mstreet, f10)
        PrintIfNotEmpty(Trim(mcity) & " - " & mzipcode, f10)
        PrintIfNotEmpty(mdistrict & "," & mstate & "," & mcountry, f10)

        ' Receiver block

        'g.DrawString(mcardname, f12B, Brushes.Black, 350, 30)
        'g.DrawString(mbuild, f10, Brushes.Black, 350, 45)
        'g.DrawString(mblock, f10, Brushes.Black, 350, 60)
        'g.DrawString(mstreet, f10, Brushes.Black, 350, 75)
        'g.DrawString(Trim(mcity) & " - " & mzipcode, f10, Brushes.Black, 350, 90)
        'g.DrawString(mdistrict & mstate & "," & mcountry, f10, Brushes.Black, 350, 105)

        g.DrawString(DateTime.Now.ToString("HH:mm:ss"), f10, Brushes.Black, 153, 141)
        g.DrawString(DateTime.Now.ToString("dd-MM-yyyy"), f10, Brushes.Black, 258, 141)


        Dim fBarcode As New Font("Code39AzaleaWide2", 24, FontStyle.Regular)
        g.DrawString("*" & lpodno & "*", fBarcode, Brushes.Black, 35, 270)
        ' Barcode number
        g.DrawString(lpodno, f14B, Brushes.Black, 40, 305)
        g.DrawString(mdocnum, f14B, Brushes.Black, 195, 305)


        ' If more pages needed
        e.HasMorePages = False
    End Sub

End Class
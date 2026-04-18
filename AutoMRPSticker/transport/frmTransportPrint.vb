Imports System.Net.IPHostEntry
Imports System.Drawing.Printing
Imports System.IO
Imports System.Math
Imports Microsoft.VisualBasic
'Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.VbStrConv
'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.ReportSource
'Imports CrystalDecisions.Shared
Imports System.Configuration




Public Class frmTransportPrint

    Dim TABLE As String
    Dim TABLE1 As String
    Dim TABLE2 As String
    Dim TABLE3 As String
    Dim TABLE4 As String
    Dim TABLE5 As String
    Dim TABLE6 As String
    Dim TABLE7 As String
    Dim TABLE8 As String
    Dim TABLE9 As String
    Dim TABLE10 As String
    Dim TABLE11 As String
    Dim TABLE12 As String
    Dim TABLE13 As String
    Dim TABLE14 As String
    Dim TABLE15 As String
    Dim TABLE16 As String
    Dim TABLE17 As String
    Dim TABLE18 As String
    Dim TABLE19 As String
    Dim TABLE20 As String
    Dim TABLE21 As String
    Dim TABLE22 As String
    Dim TABLE23 As String
    Dim TABLE24 As String
    Dim con1 As SqlConnection


    Dim NAME1 As String
    Dim NAME2 As String
    Dim PSQL, PSQLCMP As String
    Dim RHLSQL, rccsql As String
    Dim RHLTSQL As String
    Dim RCCTSQL As String
    Dim TSQL As String
    Dim TSQLTAX As String
    Dim TSQL1 As String
    Dim TSQL2 As String
    Dim ySQL1 As String
    Dim ySQL2 As String
    Dim yrs As String
    Dim yirs As String
    Dim yfd As Date
    Dim ytd As Date
    Dim PAG As Integer
    Dim lin, n, k As Integer
    Dim mtotamt, mmtr As Double
    Dim famt As Double
    Dim mtotqty, mbox As Long
    Dim RHLPAG, rccpag As Integer
    Dim RHLlin, RHLn, rcclin, rccn As Integer
    Dim RHLmtotamt, RHLmmtr, rccmtotamt, rccmmtr As Double
    Dim RHLmtotqty, RHLmbox, rccmtotqty, rccmbox As Long
    Dim cryptfile As String







    Private Sub forwarding_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles forwarding.Click
        'Call MAIN()
        PSQL = "select isnull(a.U_Transport,'') U_Transport ,CASE when U_Brand in ('RAMRAJ SHIRT','VIVEAGA SHIRT','RAMRAJ HANKEYS') THEN 'MADURAI' ELSE 'TIRUPUR' END FROMPL,A.U_Dsnation AS tOPL,a.DocNum,ISNULL(A.docdate,'') DocDate,E.CardFNAME,ISNULL(F.Building,'') Building, ISNULL(F.Block,'') Block,ISNULL(F.Street,'') Street ,isnull(F.City,'') City,isnull(F.ZipCode,'') Zipcode,isnull(F.County,'') County,isnull(E.Cellular,'') Cellular,isnull(E.U_GSTIN,'') TINNO,isnull(A.U_Noofbun,'1') U_Noofbun ,CASE WHEN U_Brand in ('RAMRAJ SHIRT','VIVEAGA SHIRT','RAMRAJ HANKEYS')  THEN 'P/B READYMADE GOODS' ELSE 'P/B CH GOODS' END DSCPR,A.DocTotal ," & vbCrLf _
& "C.CompnyName,  D.Building CBUILDING,CASE when U_Brand in ('RAMRAJ SHIRT','VIVEAGA SHIRT','RAMRAJ HANKEYS') THEN '112/1A & 112/1B' ELSE D.Block END CBLOCK,CASE when U_Brand in ('RAMRAJ SHIRT','VIVEAGA SHIRT','RAMRAJ HANKEYS') THEN 'SVD NAGAR' ELSE D.StreetNo  END CSTREETNO,CASE when U_Brand in ('RAMRAJ SHIRT','VIVEAGA SHIRT','RAMRAJ HANKEYS') THEN 'KOVILPAPPAKUDI VILLAGE' ELSE D.STREET  END CSTREET,CASE when U_Brand in ('RAMRAJ SHIRT','VIVEAGA SHIRT','RAMRAJ HANKEYS') THEN 'MADURAI' ELSE D.City   END CCITY,CASE when U_Brand in ('RAMRAJ SHIRT','VIVEAGA SHIRT','RAMRAJ HANKEYS') THEN '625018' ELSE D.ZipCode END CZIPCODE,C.DdctFileNo CTINNO," & vbCrLf _
& "case when F.State = 'KA' Then 'e-sugam no.' + isnull(a.U_ESugam,'') when F.State in ('AP','TS') Then 'e-wayBill no.' + isnull(a.U_ESugam,'')  when F.State in ('KL') Then 'e-Token No.' + isnull(a.U_ESugam,'')  else '' end Esugam" & vbCrLf _
& "from " & TABLE & " A  LEFT JOIN " & TABLE12 & " G ON G.DocEntry = A.DocEntry LEFT JOIN OCRD E ON E.CardCode = A.CardCode LEFT JOIN CRD1 F ON F.CardCode = A.CardCode AND AdresType = 'B',oadm c ,adm1 d where  A.docnum in (" & No.Text & ") and   a.PIndicator = ('" & Year.Text & "') "


        Dim CMDp As New sqlcommand(PSQL, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim DRP As SqlDataReader
        DRP = CMDp.ExecuteReader
        DRP.Read()

        lin = 0
        mbox = 0

        FileOpen(1, " " + mlinpath + "\forwarding.txt", OpenMode.Output, OpenAccess.Write)
        'FileOpen(1, "e:\Test.txt", OpenMode.Output, OpenAccess.Write)
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, TAB(15), Chr(27) + Chr(45) + Chr(1) + Chr(14) + Chr(27) + Chr(69) + DRP.Item("U_Transport") + Chr(27) + Chr(70) + Chr(18) + Chr(27) + Chr(45) + Chr(0) + Chr(18))
        lin = lin + 1
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, TAB(10), Chr(27) + Chr(69) + (DRP("FROMPL").ToString), TAB(28), (DRP("TOPL").ToString), "", TAB(61), (DRP("DOCNUM").ToString), TAB(73), Microsoft.VisualBasic.FormatDateTime(DRP("docdate"), DateFormat.ShortDate) + Chr(27) + Chr(70) + Chr(18))
        lin = lin + 1
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, Space(5), "")
        lin = lin + 1
        If Self.Text = "SELF" Then PrintLine(1, TAB(5), Chr(27) + Chr(69) + DRP.Item("CompnyName").ToString + Chr(27) + Chr(70) + Chr(18), TAB(43), Chr(27) + Chr(69) + "", ("SELF No.: "), SPC(1), DRP.Item("DOCNUM").ToString + Chr(27) + Chr(70) + Chr(18)) Else PrintLine(1, TAB(5), Chr(27) + Chr(69) + DRP.Item("CompnyName").ToString + Chr(27) + Chr(70) + Chr(18), TAB(43), Chr(27) + Chr(69) + (DRP.Item("CARDFNAME").ToString) + Chr(27) + Chr(70) + Chr(18))
        lin = lin + 1
        If Self.Text = "SELF" Then PrintLine(1, TAB(1), DRP.Item("CBUILDING").ToString, TAB(38), DRP.Item("CITY").ToString) Else PrintLine(1, TAB(5), (DRP("CBUILDING").ToString), TAB(38), (DRP.Item("BUILDING").ToString))
        lin = lin + 1
        If Self.Text = "SELF" Then PrintLine(1, TAB(5), (DRP("CBLOCK").ToString), Space(1), "") Else PrintLine(1, TAB(5), (DRP("CBLOCK").ToString), TAB(38), (DRP("BLOCK").ToString))
        lin = lin + 1
        If Self.Text = "SELF" Then PrintLine(1, TAB(5), (DRP("CSTREETNO").ToString), Space(1), "") Else PrintLine(1, TAB(5), (DRP("CSTREETNO").ToString), TAB(38), (DRP("STREET").ToString))
        lin = lin + 1
        If Self.Text = "SELF" Then PrintLine(1, TAB(5), (Trim(DRP("CSTREET")).ToString), Space(1), "") Else PrintLine(1, TAB(5), (Trim(DRP("CSTREET")).ToString), TAB(38), (Trim(DRP("CITY")).ToString), SPC(1), "-", SPC(1), (DRP("ZIPCODE").ToString))
        lin = lin + 1
        If Self.Text = "SELF" Then PrintLine(1, TAB(5), (DRP("CCITY").ToString), SPC(1), "-", SPC(1), (Trim(DRP("cZIPCODE")).ToString)) Else PrintLine(1, TAB(5), (DRP("CCITY").ToString), SPC(1), "-", SPC(1), (Trim(DRP("cZIPCODE")).ToString))
        lin = lin + 1
        If Self.Text = "SELF" Then PrintLine(1, Space(1), "") Else PrintLine(1, TAB(38), SPC(1), "Mb No.:", SPC(1), (Trim(DRP("Cellular")).ToString))
        lin = lin + 1
        If Self.Text = "SELF" Then PrintLine(1, TAB(5), "GST IN.:", SPC(1), (DRP("CTINNO").ToString), Space(1), "") Else PrintLine(1, TAB(5), "GST IN.:", SPC(1), (DRP("CTINNO").ToString), TAB(38), "GST IN.:", SPC(1), (Trim(DRP("TINNO")).ToString))
        lin = lin + 1
        If Self.Text = "SELF" Then PrintLine(1, Space(1), "") Else PrintLine(1, Space(1), "")
        lin = lin + 1
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, TAB(22), NumeriCon.ConvertNum(DRP("U_Noofbun")))
        lin = lin + 1
        PrintLine(1, TAB(5), Chr(14) + Chr(27) + Chr(69) + DRP("U_Noofbun").ToString + Chr(27) + Chr(70) + Chr(27) + Chr(14) + Chr(18), Space(1), "", TAB(20), DRP.Item("DSCPR").ToString, TAB(48), (WGT.Text), TAB(58), Microsoft.VisualBasic.Format(DRP("doctotal"), "#######0.00") + Chr(27) + Chr(70) + Chr(18))
        lin = lin + 2
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, Space(5), "")
        lin = lin + 1
        PrintLine(1, TAB(5), Chr(14) + Chr(27) + Chr(69) + DRP("Esugam").ToString + Chr(27) + Chr(70) + Chr(27) + Chr(14) + Chr(18))
        lin = lin + 1

        n = 27 - lin
        For k = 1 To n
            PrintLine(1, TAB(1), "")
            lin = lin + 1
        Next k
        lin = 0

        FileClose(1)
        DRP.Close()
        con.Close()
        con.Close()

        ''Shell("command.com /c TYPE " & " dbreportpath +"TestFile.txt>PRN", AppWinStyle.Hide)
        'Shell("print /d:LPT" & lpt.Text & " " + dbreportpath + "\forwarding.txt", vbNormalFocus)

        Dim printer As String = mlsprinter 'laserprinter name
        Dim filePath As String = mlinpath
        Dim filePathname As String = mlinpath & "forwarding.txt"
        Dim success As Boolean = PrintTextFile(filePathname)

        'Dim success As Boolean = PrintTextFile("/home/user/reports/forwarding.txt")
        ''  Shell("print /d:LPT" & lpt.Text & " dbreportpath +"RRlorry.txt", vbNormalFocus)
        WGT.Text = "0.000"
    End Sub




    Private Function SetAlignment(ByVal sCaption As String, ByVal iFromCol As Integer, ByVal iToCol As Integer, ByVal sAlign As String) As String
        SetAlignment = Nothing
        Try
            Dim iFill As Integer

            If Len(sCaption) <= iToCol - iFromCol Then
                iFill = iToCol - iFromCol - Len(sCaption)
                Select Case UCase(sAlign)
                    Case "LEFT"
                        SetAlignment = sCaption & Space(iFill)
                    Case "RIGHT"
                        SetAlignment = Space(iFill) & sCaption
                    Case "CENTER"
                        SetAlignment = Space(iFill / 2) & sCaption & Space(iFill / 2)
                    Case Else
                        SetAlignment = sCaption
                End Select
            Else
                SetAlignment = sCaption
            End If
        Catch ex As Exception
            'MsgBox("SetAlignment() : " & ex.Message, MsgBoxStyle.Information)
            ''MyError.WriteLogFile("frmPOSBillingSystemNew", "SetAlignment()", ex)
        End Try
        Return SetAlignment
    End Function



    Private Sub form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        'Call MAIN()
        checkConnection()
        Year.Text = Trim(mperiod)
        cmp.Text = Trim(dbcomp)

        cmp.Enabled = False

        TextBox2.Enabled = False
        TextBox5.Enabled = False
        TextBox1.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        WGT.Enabled = False



        If cmp.Text = "ACC" Then
            btnInvoice.Text = "Transport Copy"
        Else
            btnInvoice.Text = "Invoice"
        End If

        Dim sql1 As String
        sql1 = "select Indicator from OPID Order by Indicator"
        Dim Ps1 As String
        Dim cmd1 As New SqlCommand(sql1, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim DR1 As SqlDataReader
        DR1 = cmd1.ExecuteReader
        If DR1.HasRows = True Then
            While DR1.Read
                Ps1 = DR1.Item("Indicator")
                Year.Items.Add(Ps1)
            End While
        End If
        DR1.Close()
        cmd1.Dispose()
        con.Close()
        con.Close()



        If ComboBox1.Text = "Sales" Then
            TABLE = "OINV"
            TABLE1 = "INV1"
            TABLE2 = "INV2"
            TABLE3 = "INV3"
            TABLE4 = "INV4"
            TABLE5 = "INV5"
            TABLE6 = "INV6"
            TABLE7 = "INV7"
            TABLE8 = "INV8"
            TABLE9 = "INV9"
            TABLE10 = "INV10"
            TABLE11 = "INV11"
            TABLE12 = "INV12"
            TABLE13 = "INV13"
            TABLE14 = "INV14"
            TABLE15 = "INV15"
            TABLE16 = "INV16"
            TABLE17 = "INV17"
            TABLE18 = "INV18"
            TABLE19 = "INV19"
            TABLE20 = "INV20"
            TABLE21 = "INV21"
            TABLE22 = "INV21"
            TABLE23 = "RINV7"
            TABLE24 = "RINV8"

        ElseIf ComboBox1.Text = "Delivery" Then
            TABLE = "ODLN"
            TABLE1 = "DLN1"
            TABLE2 = "DLN2"
            TABLE3 = "DLN3"
            TABLE4 = "DLN4"
            TABLE5 = "DLN5"
            TABLE6 = "DLN6"
            TABLE7 = "DLN7"
            TABLE8 = "DLN8"
            TABLE9 = "DLN9"
            TABLE10 = "DLN10"
            TABLE11 = "DLN11"
            TABLE12 = "DLN12"
            TABLE13 = "DLN13"
            TABLE14 = "DLN14"
            TABLE15 = "DLN15"
            TABLE16 = "DLN16"
            TABLE17 = "DLN17"
            TABLE18 = "DLN18"
            TABLE19 = "DLN19"
            TABLE20 = "DLN20"
            TABLE21 = "DLN21"
            TABLE22 = "DLN21"
            TABLE23 = "RDLN7"
            TABLE24 = "RDLN8"
        ElseIf ComboBox1.Text = "Order" Then
            TABLE = "ORDR"
            TABLE1 = "RDR1"
            TABLE2 = "RDR2"
            TABLE3 = "RDR3"
            TABLE4 = "RDR4"
            TABLE5 = "RDR5"
            TABLE6 = "RDR6"
            TABLE7 = "RDR7"
            TABLE8 = "RDR8"
            TABLE9 = "RDR9"
            TABLE10 = "RDR10"
            TABLE11 = "RDR11"
            TABLE12 = "RDR12"
            TABLE13 = "RDR13"
            TABLE14 = "RDR14"
            TABLE15 = "RDR15"
            TABLE16 = "RDR16"
            TABLE17 = "RDR17"
            TABLE18 = "RDR18"
            TABLE19 = "RDR19"
            TABLE20 = "RDR20"
            TABLE21 = "RDR21"
            TABLE22 = "RDR21"

        End If


    End Sub

    Private Sub No_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles No.KeyPress

    End Sub
    Private Sub No_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles No.TextChanged
        If cmp.Text = "RR" And Len(No.Text) > 0 Then
            'Call MAIN()
            Name = "SELECT isnull(cardcode,'') Cardcode ,ISNULL(U_LrWgt,0) WGT,isnull(U_PASS,'') U_PASS,isnull(u_refno,'') u_refno, isnull(U_Dsnation,'') U_Dsnation,isnull(CARDNAME,'') CArdname,isnull(u_esugam,'') u_esugam, isnull(u_areacode,'') u_areacode,isnull(docentry,'') Docentry ,isnull(U_Transport,'') U_Transport,isnull(u_noofbun,'') u_noofbun,case when isnull(U_Gpno,0) > 0 then 'Gate Pass no : '+U_Gpno else '' end GPNO  FROM  " & TABLE & "  where docnum = (" & No.Text & ") and PIndicator = ('" & Year.Text & "') "
            Dim CMDNAME As New SqlCommand(Name, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim DRPname As SqlDataReader
            DRPname = CMDNAME.ExecuteReader
            DRPname.Read()
            Label3.Text = DRPname("CARDNAME") + " - " + DRPname("Cardcode")
            Label4.Text = "eform no :" + DRPname("u_esugam") + " - " + DRPname("u_areacode")
            TextBox4.Text = DRPname("u_esugam")
            Label7.Text = DRPname("docentry")
            TextBox2.Text = DRPname("U_Transport")
            TextBox5.Text = DRPname("U_Dsnation")
            TextBox3.Text = DRPname("U_noofbun")
            Self.Text = DRPname("U_PASS")
            TextBox1.Text = DRPname("u_refno")
            lblgatepassno.Text = DRPname("GPNO")
            WGT.Text = Microsoft.VisualBasic.Format(DRPname("WGT"), "######0.000")
            DRPname.Close()
            con.Close()
            con.Close()


        ElseIf cmp.Text = "RHL" And Len(No.Text) > 0 Then
            'Call MAIN()
            NAME1 = "SELECT  isnull(cardcode,'') Cardcode ,ISNULL(U_LrWgt,0) WGT,isnull(U_PASS,'') U_PASS,isnull(u_refno,'') u_refno,isnull(U_Dsnation,'') U_Dsnation,isnull(CARDNAME,'') cardname ,isnull(u_esugam,'') u_esugam ,isnull(u_areacode,'') u_areacode ,isnull(docentry,'') docentry,isnull(U_Transport,'') u_transport,isnull(u_noofbun,'') u_noofbun,case when isnull(U_Gpno,0) > 0 then 'Gate Pass no : '+U_Gpno else '' end GPNO FROM  " & TABLE & "  where docnum = (" & No.Text & ") and PIndicator = ('" & Year.Text & "')"
            Dim CMDNAME1 As New SqlCommand(NAME1, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim DRPname1 As SqlDataReader
            DRPname1 = CMDNAME1.ExecuteReader
            DRPname1.Read()
            Label3.Text = DRPname1("CARDNAME") + " - " + DRPname1("Cardcode")
            Label4.Text = "eform no :" + DRPname1("u_esugam") + " - " + DRPname1("u_areacode")
            TextBox4.Text = DRPname1("u_esugam")
            Label7.Text = DRPname1("docentry")
            TextBox2.Text = DRPname1("U_Transport")
            TextBox5.Text = DRPname1("U_Dsnation")
            TextBox3.Text = DRPname1("U_noofbun")
            Self.Text = DRPname1("U_PASS")
            TextBox1.Text = DRPname1("u_refno")
            lblgatepassno.Text = DRPname1("GPNO")
            WGT.Text = Microsoft.VisualBasic.Format(DRPname1("WGT"), "######0.000")
            DRPname1.Close()
            con.Close()
            con.Close()



        ElseIf cmp.Text = "ATC" And Len(No.Text) > 0 Then
            'Call MAIN()
            NAME1 = "SELECT  isnull(cardcode,'') Cardcode ,ISNULL(U_LrWgt,0) WGT,isnull(U_PASS,'') U_PASS,isnull(u_refno,'') u_refno,isnull(U_Dsnation,'') U_Dsnation,isnull(CARDNAME,'') cardname ,isnull(u_esugam,'') u_esugam ,isnull(u_areacode,'') u_areacode ,isnull(docentry,'') docentry,isnull(U_Transport,'') u_transport,isnull(u_noofbun,'') u_noofbun,case when isnull(U_Gpno,0) > 0 then 'Gate Pass no : '+U_Gpno else '' end GPNO FROM  " & TABLE & "  where docnum = (" & No.Text & ") and PIndicator = ('" & Year.Text & "')"
            Dim CMDNAME1 As New SqlCommand(NAME1, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim DRPname1 As SqlDataReader
            DRPname1 = CMDNAME1.ExecuteReader
            DRPname1.Read()
            Label3.Text = DRPname1("CARDNAME") + " - " + DRPname1("Cardcode")
            Label4.Text = "eform no :" + DRPname1("u_esugam") + " - " + DRPname1("u_areacode")
            TextBox4.Text = DRPname1("u_esugam")
            Label7.Text = DRPname1("docentry")
            TextBox2.Text = DRPname1("U_Transport")
            TextBox5.Text = DRPname1("U_Dsnation")
            TextBox3.Text = DRPname1("U_noofbun")
            Self.Text = DRPname1("U_PASS")
            TextBox1.Text = DRPname1("u_refno")
            lblgatepassno.Text = DRPname1("GPNO")
            WGT.Text = Microsoft.VisualBasic.Format(DRPname1("WGT"), "######0.000")
            DRPname1.Close()
            con.Close()
            con.Close()



        ElseIf cmp.Text = "VT" And Len(No.Text) > 0 Then
            'Call MAIN()
            NAME1 = "SELECT  isnull(cardcode,'') Cardcode ,ISNULL(U_LrWgt,0) WGT,isnull(U_PASS,'') U_PASS,isnull(u_refno,'') u_refno,isnull(U_Dsnation,'') U_Dsnation,isnull(CARDNAME,'') cardname ,isnull(u_esugam,'') u_esugam ,isnull(u_areacode,'') u_areacode ,isnull(docentry,'') docentry,isnull(U_Transport,'') u_transport,isnull(u_noofbun,'') u_noofbun,case when isnull(U_Gpno,0) > 0 then 'Gate Pass no : '+U_Gpno else '' end GPNO FROM  " & TABLE & "  where docnum = (" & No.Text & ") and PIndicator = ('" & Year.Text & "')"
            Dim CMDNAME1 As New SqlCommand(NAME1, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim DRPname1 As SqlDataReader
            DRPname1 = CMDNAME1.ExecuteReader
            DRPname1.Read()
            Label3.Text = DRPname1("CARDNAME") + " - " + DRPname1("Cardcode")
            Label4.Text = "eform no :" + DRPname1("u_esugam") + " - " + DRPname1("u_areacode")
            TextBox4.Text = DRPname1("u_esugam")
            Label7.Text = DRPname1("docentry")
            TextBox2.Text = DRPname1("U_Transport")
            TextBox5.Text = DRPname1("U_Dsnation")
            TextBox3.Text = DRPname1("U_noofbun")
            Self.Text = DRPname1("U_PASS")
            TextBox1.Text = DRPname1("u_refno")
            lblgatepassno.Text = DRPname1("GPNO")
            WGT.Text = Microsoft.VisualBasic.Format(DRPname1("WGT"), "######0.000")
            DRPname1.Close()
            con.Close()
            con.Close()




        ElseIf cmp.Text = "RCC" And Len(No.Text) > 0 Then
            'Call MAIN()
            NAME1 = "SELECT  isnull(cardcode,'') Cardcode ,ISNULL(U_LrWgt,0) WGT,isnull(U_PASS,'') U_PASS,isnull(u_refno,'') u_refno,isnull(U_Dsnation,'') U_Dsnation,isnull(CARDNAME,'') cardname ,isnull(u_esugam,'') u_esugam ,isnull(u_areacode,'') u_areacode ,isnull(docentry,'') docentry,isnull(U_Transport,'') u_transport,isnull(u_noofbun,'') u_noofbun,case when isnull(U_Gpno,0) > 0 then 'Gate Pass no : '+U_Gpno else '' end GPNO FROM  " & TABLE & "  where docnum = (" & No.Text & ") and PIndicator = ('" & Year.Text & "')"
            Dim CMDNAME1 As New SqlCommand(NAME1, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim DRPname1 As SqlDataReader
            DRPname1 = CMDNAME1.ExecuteReader
            DRPname1.Read()
            Label3.Text = DRPname1("CARDNAME") + " - " + DRPname1("Cardcode")
            Label4.Text = "eform no :" + DRPname1("u_esugam") + " - " + DRPname1("u_areacode")
            TextBox4.Text = DRPname1("u_esugam")
            Label7.Text = DRPname1("docentry")
            TextBox2.Text = DRPname1("U_Transport")
            TextBox5.Text = DRPname1("U_Dsnation")
            TextBox3.Text = DRPname1("U_noofbun")
            Self.Text = DRPname1("U_PASS")
            TextBox1.Text = DRPname1("u_refno")
            lblgatepassno.Text = DRPname1("GPNO")
            WGT.Text = Microsoft.VisualBasic.Format(DRPname1("WGT"), "######0.000")
            DRPname1.Close()
            con.Close()
            con.Close()

        ElseIf cmp.Text = "ACC" And Len(No.Text) > 0 Then
            'Call MAIN()
            NAME2 = "SELECT isnull(cardcode,'') Cardcode ,ISNULL(U_LrWgt,0) WGT,isnull(U_PASS,'TO PAY') U_PASS,isnull(u_refno,'') u_refno,isnull(U_Dsnation,'') U_Dsnation,isnull(CARDNAME,'') CArdname,isnull(u_esugam,'') u_esugam, isnull(u_areacode,'') u_areacode,isnull(docentry,'') Docentry ,isnull(U_Transport,'') U_Transport,isnull(u_noofbun,'') u_noofbun   FROM  " & TABLE & "  where docnum = (" & No.Text & ") and PIndicator = ('" & Year.Text & "') "
            Dim CMDNAME2 As New SqlCommand(NAME2, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim DRPname2 As SqlDataReader
            DRPname2 = CMDNAME2.ExecuteReader
            DRPname2.Read()
            Label3.Text = DRPname2("CARDNAME") + " - " + DRPname2("Cardcode")
            Label4.Text = "eform no :" + DRPname2("u_esugam") + " - " + DRPname2("u_areacode")
            TextBox4.Text = DRPname2("u_esugam")
            Label7.Text = DRPname2("docentry")
            TextBox2.Text = DRPname2("U_Transport")
            TextBox5.Text = DRPname2("U_Dsnation")
            TextBox3.Text = DRPname2("U_noofbun")
            Self.Text = DRPname2("U_PASS")
            TextBox1.Text = DRPname2("u_refno")
            'lblgatepassno.Text = DRPname2("GPNO")
            WGT.Text = Microsoft.VisualBasic.Format(DRPname2("WGT"), "######0.000")
            DRPname2.Close()
            con.Close()
            con.Close()

        ElseIf Len(No.Text) < 0 Then
            Label3.Text = ""
        End If

    End Sub

    Private Sub Button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button5.Click
        If cmp.Text = "RR" Then
            ' Call MAIN()

            Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            ' conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1

            com.CommandText = "update " & TABLE & " set U_Pass = '" & Self.Text & "' where docnum = '" & No.Text & "' and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            con1.Close()
            con1.Close()



        ElseIf cmp.Text = "RHL" Then
            'Call MAIN()

            'Dim conString As String
            ' Dim con As SqlConnection

            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1

            com.CommandText = "update " & TABLE & " set U_Pass = '" & Self.Text & "' where docnum = '" & No.Text & "' and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()

            con1.Close()
            con1.Close()


        ElseIf cmp.Text = "ATC" Then
            'Call MAIN()

            'Dim conString As String
            'Dim con As SqlConnection

            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1

            com.CommandText = "update " & TABLE & " set U_Pass = '" & Self.Text & "' where docnum = '" & No.Text & "' and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            con1.Close()
            con1.Close()


        ElseIf cmp.Text = "VT" Then
            ' Call MAIN()

            Dim conString As String
            'Dim con As SqlConnection

            Dim com As New SqlCommand
            ' conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1

            com.CommandText = "update " & TABLE & " set U_Pass = '" & Self.Text & "' where docnum = '" & No.Text & "' and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            con1.Close()
            con1.Close()


        ElseIf cmp.Text = "RCC" Then
            'Call MAIN()

            Dim conString As String
            'Dim con As SqlConnection

            Dim com As New SqlCommand
            ' conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1

            com.CommandText = "update " & TABLE & " set U_Pass = '" & Self.Text & "' where docnum = '" & No.Text & "' and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            con1.Close()
            con1.Close()



        ElseIf cmp.Text = "ACC" Then
            'Call MAIN()

            Dim conString As String
            'Dim con As SqlConnection

            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1

            com.CommandText = "update " & TABLE & " set U_Pass = '" & Self.Text & "' where docnum = '" & No.Text & "' and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            con1.Close()
            con1.Close()


        End If
    End Sub


    Private Sub TextBox1_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox1.Leave
        If cmp.Text = "RR" Then
            'Call MAIN()

            Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1

            com.CommandText = "update " & TABLE & " set u_refno = '" & TextBox1.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            con1.Close()
            con1.Close()

            TextBox1.Enabled = False

        ElseIf cmp.Text = "RHL" Then

            'Call MAIN()

            Dim conString As String
            ' Dim con As SqlConnection

            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set u_refno = '" & TextBox1.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox1.Enabled = False
            con1.Close()
            con1.Close()

        ElseIf cmp.Text = "ATC" Then

            'Call MAIN()

            Dim conString As String
            'Dim con As SqlConnection

            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set u_refno = '" & TextBox1.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox1.Enabled = False
            con1.Close()
            con1.Close()

        ElseIf cmp.Text = "VT" Then

            'Call MAIN()

            Dim conString As String
            'Dim con As SqlConnection

            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set u_refno = '" & TextBox1.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox1.Enabled = False

            con1.Close()
            con1.Close()

        ElseIf cmp.Text = "RCC" Then

            'Call MAIN()

            ' Dim conString As String
            'Dim con As SqlConnection

            Dim com As New SqlCommand

            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set u_refno = '" & TextBox1.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox1.Enabled = False
            con1.Close()
            con1.Close()

        ElseIf cmp.Text = "ACC" Then
            'Call MAIN()
            Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1

            com.CommandText = "update " & TABLE & " set u_refno = '" & TextBox1.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox1.Enabled = False
            con1.Close()
            con1.Close()

        End If
    End Sub



    Private Sub Label5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label5.Click
        TextBox1.Enabled = True
    End Sub





    Private Sub Label8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label8.Click
        TextBox2.Enabled = True
    End Sub

    Private Sub TextBox2_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox2.Leave
        If cmp.Text = "RR" Then

            'Call MAIN()

            ' Dim conString As String
            'Dim con As SqlConnection

            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set U_Transport = '" & TextBox2.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            con1.Close()
            con1.Close()

            TextBox2.Enabled = False

        ElseIf cmp.Text = "RHL" Then
            'Call MAIN()
            ' Dim conString As String
            ' Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            If con1.State = ConnectionState.Closed Then con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set U_Transport = '" & TextBox2.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox2.Enabled = False

        ElseIf cmp.Text = "ATC" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set U_Transport = '" & TextBox2.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox2.Enabled = False
            con1.Close()
            con1.Close()

        ElseIf cmp.Text = "VT" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set U_Transport = '" & TextBox2.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox2.Enabled = False
            con1.Close()
            con1.Close()

        ElseIf cmp.Text = "RCC" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set U_Transport = '" & TextBox2.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()

            TextBox2.Enabled = False
            con1.Close()
            con1.Close()

        ElseIf cmp.Text = "ACC" Then

            ' Call MAIN()

            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set U_Transport = '" & TextBox2.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox2.Enabled = False
            con1.Close()
            con1.Close()

        End If

    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Label9_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label9.Click
        TextBox3.Enabled = True
    End Sub

    Private Sub TextBox3_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox3.Leave
        If cmp.Text = "RR" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set U_noofbun = '" & TextBox3.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox3.Enabled = False
            con1.Close()
            con1.Close()


        ElseIf cmp.Text = "RHL" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set  U_noofbun = '" & TextBox3.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox3.Enabled = False
            con1.Close()
            con1.Close()

        ElseIf cmp.Text = "ATC" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set  U_noofbun = '" & TextBox3.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox3.Enabled = False
            con1.Close()
            con1.Close()

        ElseIf cmp.Text = "VT" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set  U_noofbun = '" & TextBox3.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox3.Enabled = False
            con1.Close()
            con1.Close()


        ElseIf cmp.Text = "RCC" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set  U_noofbun = '" & TextBox3.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox3.Enabled = False
            con1.Close()
            con1.Close()


        ElseIf cmp.Text = "ACC" Then

            'Call MAIN()

            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set U_noofbun = '" & TextBox3.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox3.Enabled = False
            con1.Close()
            con1.Close()

        End If
    End Sub



    Private Sub Label4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label4.Click
        TextBox4.Visible = True
        TextBox4.Enabled = True

    End Sub

    Private Sub TextBox4_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox4.Leave

        If cmp.Text = "RR" Then

            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set u_esugam = '" & TextBox4.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox4.Enabled = False

            con1.Close()
            con1.Close()

        ElseIf cmp.Text = "RHL" Then
            'Call MAIN()

            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set u_esugam = '" & TextBox4.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox4.Enabled = False
            con1.Close()
            con1.Close()

        ElseIf cmp.Text = "ATC" Then
            'Call MAIN()

            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set u_esugam = '" & TextBox4.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox4.Enabled = False
            con1.Close()
            con1.Close()
        ElseIf cmp.Text = "VT" Then
            'Call MAIN()

            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set u_esugam = '" & TextBox4.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox4.Enabled = False

            con1.Close()
            con1.Close()
        ElseIf cmp.Text = "RCC" Then
            'Call MAIN()

            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set u_esugam = '" & TextBox4.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox4.Enabled = False
            con1.Close()
            con1.Close()




        ElseIf cmp.Text = "ACC" Then

            'Call MAIN()

            'Dim conString As String
            'Dim con As SqlConnection

            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set u_esugam = '" & TextBox4.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox4.Enabled = False
            con1.Close()
            con1.Close()
        End If

    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub Button8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button8.Click
        If MsgBox("Select Yes - New  or No - Old", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            ''frmEsugam.MdiParent = MDIFORM1
            ''frmEsugam.Show()
            ''frmEsugam.WindowState = FormWindowState.Maximized

        Else
            'live
            'Process.Start(System.Configuration.ConfigurationManager.AppSettings("eform"))

        End If
        'Me.BringToFront()
    End Sub
    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click



        Me.Close()
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        frmSummary.Show()
    End Sub



    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        frmgatepassdatagrid.Show()
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        'Call MAIN()
        'this live
        'Dim cryRpt3 As New ReportDocument()
        'cryRpt3.Load(Trim(dbreportpath) & Trim(dblrpass))
        'cryptfile = loadrptdb2(Trim(dblrpass), Trim(dbreportpath))
        'cryRpt3.Load(cryptfile)
        'CrystalReportLogOn(cryRpt3, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))
        'cryRpt3.Refresh()
        'cryRpt3.SetParameterValue("@date", dt.Value.ToString("yyyy-MM-dd"))
        'CrystalReportViewer1.Visible = True
        'Me.CrystalReportViewer1.ReportSource = cryRpt3
        'Me.CrystalReportViewer1.PrintReport()
        'Me.CrystalReportViewer1.Refresh()
        'cryRpt3.Refresh()


    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        mgatepass.Show()
    End Sub

    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label10.Click
        TextBox5.Enabled = True
    End Sub

    Private Sub TextBox5_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox5.Leave
        If cmp.Text = "RR" Then

            ' Call MAIN()

            'Dim conString As String
            'Dim con As SqlConnection

            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set U_Dsnation = '" & TextBox5.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()

            TextBox5.Enabled = False
            con1.Close()
            con1.Close()
        ElseIf cmp.Text = "RHL" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set U_Dsnation = '" & TextBox5.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox5.Enabled = False

            con1.Close()
            con1.Close()

        ElseIf cmp.Text = "ATC" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set U_Dsnation = '" & TextBox5.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox5.Enabled = False
            con1.Close()
            con1.Close()
        ElseIf cmp.Text = "VT" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set U_Dsnation = '" & TextBox5.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox5.Enabled = False

            con1.Close()
            con1.Close()
        ElseIf cmp.Text = "RCC" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set U_Dsnation = '" & TextBox5.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox5.Enabled = False
            con1.Close()
            con1.Close()
        ElseIf cmp.Text = "ACC" Then

            'Call MAIN()

            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set U_Dsnation = '" & TextBox5.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            TextBox5.Enabled = False
            con1.Close()
            con1.Close()
        End If

    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        'Call MAIN()
        'Me.Cursor = Cursors.WaitCursor
        'Dim cryRpt1 As New ReportDocument()
        'cryRpt1.Load(Trim(dbreportpath) & Trim(dbGPSU))

        'CrystalReportLogOn(cryRpt1, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))

        'cryRpt1.SetParameterValue("@fromdate", dt.Value.ToString("yyyy-MM-dd"))
        'cryRpt1.SetParameterValue("@todate", dt.Value.ToString("yyyy-MM-dd"))

        'Me.CrystalReportViewer1.ReportSource = cryRpt1
        'Me.CrystalReportViewer1.PrintReport()
        'Me.CrystalReportViewer1.Refresh()
        'cryRpt1.Dispose()
        'Me.Cursor = Cursors.Default
        frmGPSummary.Show()
    End Sub

    Private Sub btnInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInvoice.Click
        'live
        Dim strHostName As String
        Dim strIPAddress As String
        strHostName = System.Net.Dns.GetHostName()
        strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(0).ToString()
        Me.Cursor = Cursors.WaitCursor
        'Dim cryRpt As New ReportDocument()


        'cryptfile = loadrptdb2(Trim(DBINV), Trim(dbreportpath))
        'cryRpt.Load(cryptfile)
        'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))
        'cryRpt.SetParameterValue("Dockey@", Val(Label7.Text))
        'Me.CrystalReportViewer1.ReportSource = cryRpt
        'Me.CrystalReportViewer1.PrintReport()
        'Me.CrystalReportViewer1.Refresh()
        'cryRpt.Refresh()
        Me.Cursor = Cursors.Default


    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If cmp.Text = "RR" Then
            ' Call MAIN()
            '        PSQL = "select b.LineNum, CASE WHEN C.TransCat = 'FORM H' THEN 'Taxable' WHEN A.VatSum = '0' THEN 'Non  Taxable' ELSE 'Taxable' End AS TAXABLE, A.DOCENTRY,b.LineNum,a.address,a.address2,A.cardname,a.U_Noofbun,r.Building,isnull(r.Block,'') Block,r.City," & vbCrLf _
            '& "CASE WHEN R.State='OS' THEN R.StreetNo WHEN l.QryGroup5='Y' THEN R.StreetNo ELSE ST.Name END AS STATE,r.State State1," & vbCrLf _
            '& "r.ZipCode,isnull(r.Street,'')Street,r.country,r.county, A.u_ESUGAM, A.docnum,isnull(a.Docdate,'')Docdate ,A.u_orderby,A.u_arcode,A.u_transport,(A.u_docthrough + '   ' + a.numatcard) u_docthrough  ,isnull(convert(nvarchar(max),A.u_lrno),'') u_lrno, isnull(a.u_lrdat,'') u_lrdat ,ISNULL(A.u_lrwight,0) u_lrwight ,a.u_dsnation,a.U_lrval AS topay," & vbCrLf _
            '& "RTRIM(LEFT(((CASE when l.qrygroup4='Y' THEN ISNULL(CAST(P.U_Remarks AS NVARCHAR(150))+'-','') ELSE '' END)+ isnull(itm.U_SubGrp6,'') + ' ' + ISNULL(itb.U_ItemGrp,'')),30)) as item, " & vbCrLf _
            '& "b.u_style,b.u_size,b.baseref,b.price,b.linetotal,b.quantity,a.DiscSum,a.VatSum,a.RoundDif,D.firstname,f.U_remarks,b.taxcode," & vbCrLf _
            '& "b.BaseREf,(b.quantity/itm.SalPackUn) Box,a.TotalExpns,c.TransCat,(isnull(crdd1.TaxId1,''))as CSTNO,(isnull(crd.TaxId11,'')) as TINNO,l.CardFName," & vbCrLf _
            '& "a.U_Dis1'TRADE DISCOUNT',a.U_Dis2'CASH DISCOUNT',a.U_Dis3'CD/AD DRAFT DISC',a.U_Dis4'CD/AD/QTY DISC'" & vbCrLf _
            '& ",a.U_Dis5'CD/LR-AGAINST',a.U_Dis6'QTY DISCOUNT',a.U_Dis7'SPL DISCOUNT',a.U_Dis8'VAT EXCEMPATION',a.U_Dis9'TURNOVER'" & vbCrLf _
            '& ",isnull(a.U_Dis10,0)'VAT DISCOUNT',a.DiscPrcnt,Left(b.Taxcode,3) as TAXCODE,a.numatcard," & vbCrLf _
            '& "RTRIM(convert(nvarchar(100),isnull(r.building,'')))+','+rtrim(convert(nvarchar(100),isnull(r.block,'')))+','+RTRIM(convert(nvarchar(100),isnull(r.Street,'')))+'-'+RTRIM(convert(nvarchar(50),isnull(r.ZipCode,'')))+','+rtrim(CONVERT(nvarchar(100),isnull(r.city,'')))+','+" & vbCrLf _
            '& "RTRIM(CONVERT(nvarchar(10),isnull(r.State,'')))+','+RTRIM(CONVERT(nvarchar(50),ISNULL(r.county,'')))+','+RTRIM(CONVERT(nvarchar(10),ISNULL(r.country,''))) as address3" & vbCrLf _
            '& ",Isnull(Convert(Nvarchar,P.U_Remarks)+'-'+'','') [Item Remarks],b.PriceBefDi,b.Quantity*b.PriceBefDi [Before Disc],b.LineTotal,G.DiscPrcnt [Item Disc]" & vbCrLf _
            '& ",ISNULL(S.[FORWARDING CHARGES],0) [FORWARDING CHARGES],b.DiscPrcnt [Item Disc Prct]," & vbCrLf _
            '& "isnull(G.[Disc Amt],0) [Disc Amt]" & vbCrLf _
            '& ",CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent'" & vbCrLf _
            '& ",CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt',a.cardcode " & vbCrLf _
            '& " from " & TABLE & " A inner join " & TABLE1 & " b on a.DocEntry=b.DocEntry" & vbCrLf _
            '& "left outer join OHEM D on D.empid=a.ownercode" & vbCrLf _
            '& "Left Join [@INCM_BND1] F on F.U_Name = a.U_brand" & vbCrLf _
            '& "Left join " & TABLE12 & " as c on c.DocEntry = b.DocEntry	" & vbCrLf _
            '& "left join OITM as itm on itm.ItemCode = b.ItemCode" & vbCrLf _
            '& "left join (SELECT * FROM [dbo].[@INS_OPLM] WHERE DocEntry IN (SELECT DocEntry FROM [@INS_PLM1] WHERE U_Remarks IS NOT NULL)) " & vbCrLf _
            '& "as itb on itb.U_ItemCode =itm.ItemCode" & vbCrLf _
            '& "left join (SELECT DISTINCT CARDCODE,max(ISNULL(TAXID11,'')) TAXID11  FROM  CRD7  group by cardcode ) as crd on crd.CardCode = a.CardCode" & vbCrLf _
            '& "left join (SELECT DISTINCT CARDCODE,max(ISNULL(TaxId1,'')) TAXID1  FROM  CRD7  group by cardcode ) as crdd1 on crdd1.CardCode = a.CardCode" & vbCrLf _
            '& "left join CRD1 as r on r.CardCode = a.CardCode /*and a.paytocode = r.address*/ AND R.AdresType='B'" & vbCrLf _
            '& "Left Outer Join OCST ST ON R.State=ST.Code AND R.Country=ST.Country AND ST.Country='IN'" & vbCrLf _
            '& "left OUTER join OCRD as l on l.CardCode = a.CardCode " & vbCrLf _
            '& "left OUTER join (SELECT * FROM [@INS_PLM1] WHERE U_Remarks IS NOT NULL) P on P.U_ItemName=B.U_CatalogName " & vbCrLf _
            '& "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry," & vbCrLf _
            '& "sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from " & TABLE1 & " where DiscPrcnt=100 group by DocEntry) G " & vbCrLf _
            '& "on G.DocEntry=B.DocEntry" & vbCrLf _
            '& "left OUTER join (Select sum(linetotal) [FORWARDING CHARGES],DocEntry from " & TABLE3 & " Group by DocEntry ) S ON S.DocEntry=B.DocEntry " & vbCrLf _
            '& "where a.docnum in (" & No.Text & ") and a.PIndicator in ('" & Year.Text & "') and b.Treetype <> 'I'  and b.ItemCode<>'FrightCharges' Order by b.LineNum"
            PSQL = " select CASE WHEN C.TransCat = 'FORM H' THEN 'Taxable' WHEN A.VatSum = '0' THEN 'Non  Taxable' ELSE 'Taxable' End AS TAXABLE, l.QryGroup8,A.DOCENTRY,b.linenum,a.address,a.address2,A.cardname," & vbCrLf _
& " a.U_Noofbun,r.Building,isnull(r.Block,'') Block,r.City,CASE WHEN R.State='OS' THEN R.StreetNo WHEN l.QryGroup5='Y' THEN R.StreetNo ELSE ST.Name END AS STATE,r.State State1,r.ZipCode," & vbCrLf _
& " isnull(r.Street,'')Street,r.country,r.county, b.DiscPrcnt LDiscPrcnt,tx.TaxRate LTaxRate,itm.U_HSNCODE,l.U_GSTIN,st.eCode,sn.BeginStr,r1.Building SBuilding,isnull(r1.Block,'') SBlock," & vbCrLf _
& " r1.city SCity,CASE WHEN R1.State='OS' THEN R1.streetNo WHEN l.QryGroup5='Y' THEN R1.streetNo ELSE ST.Name END AS SSTATE,r1.State SState1,r1.ZipCode  SZipCode,isnull(r1.Street,'') SStreet," & vbCrLf _
& " r1.Country  Scountry,r1.County scounty,loc.Building LBuilding,isnull(loc.Block,'') LBlock,loc.city LCity, loc.State LState1,loc.ZipCode  LZipCode,isnull(loc.Street,'') LStreet," & vbCrLf _
& " loc.Country  Lcountry,loc.County Lcounty,A.u_ESUGAM,A.docnum,A.docdate,A.u_orderby,A.u_arcode,A.u_transport,A.u_docthrough,A.u_lrno,A.u_lrdat,A.u_lrwight,a.u_dsnation,a.U_lrval AS topay," & vbCrLf _
& " b.u_catalogname,B.ItemCode,(CASE when l.qrygroup4='Y' THEN ISNULL(CAST(P.U_Remarks AS NVARCHAR(150))+' - ','') ELSE '' END)+ isnull(itm.U_SubGrp6,'') + ' ' + ISNULL(itb.U_ItemGrp,'') as item," & vbCrLf _
& " b.u_style,b.u_size,b.price,b.linetotal,b.quantity,a.DiscSum,a.VatSum,a.RoundDif,D.firstname,isnull(f.U_remarks,'') U_remarks,b.taxcode,b.BaseREf,itm.SalPackUn,a.TotalExpns,c.TransCat," & vbCrLf _
& " (crd.TaxId1)as CSTNO,(l.U_GSTIN)as TINNO,l.CardFName,a.U_Dis1'TRADE DISCOUNT',a.U_Dis2'CASH DISCOUNT',a.U_Dis3'CD/AD DRAFT DISC',a.U_Dis4'CD/AD/QTY DISC',a.U_Dis5'CD/LR-AGAINST'," & vbCrLf _
    & " (b.price * b.Quantity) LinetValue , (b.quantity/itm.SalPackUn) Box,      a.U_Dis6 'QTY DISCOUNT',a.U_Dis7'SPL DISCOUNT',a.U_Dis8'VAT EXCEMPATION',a.U_Dis9'TURNOVER',isnull(a.U_Dis10,0)'VAT DISCOUNT',a.DiscPrcnt,Left(b.Taxcode,3) as TAXCODE,a.numatcard," & vbCrLf _
& " RTRIM(convert(nvarchar(100),isnull(r.building,'')))+','+rtrim(convert(nvarchar(100),isnull(r.block,'')))+','+RTRIM(convert(nvarchar(100),isnull(r.Street,'')))+'-'" & vbCrLf _
& " +RTRIM(convert(nvarchar(50),isnull(r.ZipCode,'')))+','+rtrim(CONVERT(nvarchar(100),isnull(r.city,'')))+','+RTRIM(CONVERT(nvarchar(10),isnull(r.State,'')))+','+RTRIM(CONVERT(nvarchar(50),ISNULL(r.county,'')))+','+RTRIM(CONVERT(nvarchar(10),ISNULL(r.country,''))) as address3," & vbCrLf _
& " Isnull(Convert(Nvarchar,P.U_Remarks)+'-'+'','') [Item Remarks],b.PriceBefDi,b.Quantity*b.PriceBefDi [Before Disc],b.LineTotal,G.DiscPrcnt [Item Disc],ISNULL(S.[FORWARDING CHARGES],0) [FORWARDING CHARGES]," & vbCrLf _
& " b.DiscPrcnt [Item Disc Prct],isnull(G.[Disc Amt],0) [Disc Amt],CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent',CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt',a.cardcode " & vbCrLf _
& " from OINV a inner join INV1 b on a.DocEntry=b.DocEntry" & vbCrLf _
& " left outer join OHEM D on D.empid=a.ownercode" & vbCrLf _
& " Left Join [@INCM_BND1] F on F.U_Name = a.U_brand" & vbCrLf _
& " --left outer  join [@INS_LRF1] x on x.U_InvNo=a.DocNum" & vbCrLf _
& " Left join (select LineNum,SUM(TaxRate) TAXRATE,DocEntry from  INV4 where RelateType = 1 group by DocEntry,LineNum) tx on tx.DocEntry = b.DocEntry and tx.LineNum= b.LineNum" & vbCrLf _
& " Left join INV12 as c on c.DocEntry = b.DocEntry	" & vbCrLf _
& " Left Join [NNM1] sn on sn.Series = a.Series" & vbCrLf _
& " left join OITM as itm on itm.ItemCode = b.ItemCode" & vbCrLf _
& " left join (SELECT * FROM [dbo].[@INS_OPLM] WHERE DocEntry IN (SELECT DocEntry FROM [@INS_PLM1] WHERE U_Remarks IS NOT NULL)) " & vbCrLf _
& " as itb on itb.U_ItemCode =itm.ItemCode" & vbCrLf _
& " left join CRD7 as crd on crd.CardCode = a.CardCode and LEN(rtrim(crd.Address))<=0 or crd.Address is null" & vbCrLf _
& " left join CRD1 as r on r.CardCode = a.CardCode /*and a.paytocode = r.address*/ AND R.AdresType='B'" & vbCrLf _
& " left join CRD1 as r1 on r1.CardCode = a.CardCode /*and a.paytocode = r.address*/ AND R1.AdresType='S'" & vbCrLf _
& " Left Outer Join OCST ST ON R.State=ST.Code AND R.Country=ST.Country AND ST.Country='IN'" & vbCrLf _
& " left OUTER join OCRD as l on l.CardCode = a.CardCode " & vbCrLf _
& " left OUTER join (SELECT * FROM [@INS_PLM1] WHERE U_Remarks IS NOT NULL) P on P.U_ItemName=B.U_CatalogName " & vbCrLf _
& " left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry," & vbCrLf _
& " sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from INV1 where DiscPrcnt=100 group by DocEntry) G " & vbCrLf _
& " on G.DocEntry=B.DocEntry" & vbCrLf _
& " --left OUTER join (Select ItemCode,LineTotal [FORWARDING CHARGES],DocEntry from INV1 WHERE ItemCode='FrightCharges') S ON S.DocEntry=B.DocEntry" & vbCrLf _
& " left OUTER join (Select LineTotal [FORWARDING CHARGES],DocEntry from INV3) S ON S.DocEntry=B.DocEntry" & vbCrLf _
& " Left join OLCT loc on loc.Code = b.LocCode" & vbCrLf _
& " where a.docnum in (" & No.Text & ") and a.PIndicator in ('" & Year.Text & "')" & vbCrLf _
& " and b.Treetype <> 'I'  and b.ItemCode<>'FrightCharges'"

            PSQLCMP = "select * from OADM ad1 Left join ADM1 ad2 on ad2.Code = ad1.Code Left join ocst ad3 on ad3.Code = ad1.State and ad1.Country = ad3.Country Left join ODSC ad4 on ad4.BankCode = ad1.DflBnkCode "

            mtotqty = 0
            mtotamt = 0
            PAG = 0
            'checkConnection()

            Dim CMDCMP As New SqlCommand(PSQLCMP, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim DRPCMP As SqlDataReader
            DRPCMP = CMDCMP.ExecuteReader
            DRPCMP.Read()


            Dim CMDp As New SqlCommand(PSQL, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim DRP As SqlDataReader
            DRP = CMDp.ExecuteReader
            DRP.Read()

            lin = 0
            FileOpen(1, " " + mlinpath + "\RRlorryNew.txt", OpenMode.Output, OpenAccess.Write)
            'FileOpen(1, " " + dbreportpath + "forwarding.txt", OpenMode.Output, OpenAccess.Write)
            PrintLine(1, TAB(1), Chr(27) + Chr(69) + DRPCMP.Item("CompnyName").ToString + Chr(27) + Chr(70) + Chr(18), SPC(1), TAB(61), Chr(27) + Chr(69) + "DUPLICATE FOR TRANSPORTER" + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(1), (DRPCMP("BUILDING").ToString))
            lin = lin + 1
            PrintLine(1, TAB(1), (DRPCMP("BLOCK").ToString), SPC(1), (DRPCMP("StreetNo").ToString), TAB(65), Chr(27) + Chr(69) + (DRP.Item("U_remarks").ToString), SPC(1), "", (DRP.Item("Docnum").ToString + Chr(27) + Chr(70) + Chr(18)))
            lin = lin + 1
            PrintLine(1, TAB(1), (DRPCMP("StreetNo").ToString), SPC(1), TAB(65), Microsoft.VisualBasic.FormatDateTime(DRP("docdate"), DateFormat.ShortDate))
            lin = lin + 1
            PrintLine(1, TAB(1), (DRPCMP("Street").ToString), SPC(1), (Trim(DRPCMP("CITY")).ToString), SPC(1), "-", SPC(1), (Trim(DRPCMP("ZIPCODE")).ToString), SPC(1), (Trim(DRPCMP("State")).ToString))
            lin = lin + 1
            PrintLine(1, TAB(1), "Customer Care : ", SPC(1), (DRPCMP("Phone1").ToString))
            lin = lin + 1
            PrintLine(1, TAB(1), "e-mail : ", SPC(1), (DRPCMP("E_Mail").ToString))
            lin = lin + 1
            PrintLine(1, TAB(1), "State Code : ", SPC(1), (DRPCMP("eCode").ToString), SPC(1), "PAN : ", SPC(1), (DRPCMP("RevOffice").ToString))
            lin = lin + 1
            PrintLine(1, TAB(1), "GST IN : ", SPC(1), (DRPCMP("DdctFileNo").ToString))
            lin = lin + 1
            PrintLine(1, TAB(0), "---------------------------------------------------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(1), "To,", TAB(45), "Order No :", SPC(1), (DRP("baseref").ToString), TAB(64), "L.R.No :", SPC(1), (DRP("u_lrno").ToString))
            lin = lin + 1
            PrintLine(1, TAB(1), Chr(27) + Chr(69) + DRP.Item("Cardfname").ToString + Chr(27) + Chr(70) + Chr(18), TAB(50), "Case NO :", TAB(68), SPC(1), "L.R.Dt :")
            lin = lin + 1
            PrintLine(1, TAB(1), (DRP("BUILDING").ToString), TAB(45), "Area Code :", SPC(1), DRP("u_arcode").ToString, TAB(64), "Lr.Wgt : 0.000 FGT : 0.000")
            lin = lin + 1
            PrintLine(1, TAB(1), (DRP("BLOCK").ToString), TAB(45), "Doc.through :", SPC(1), (DRP("u_docthrough")), TAB(70), "Order By :", SPC(1), (DRP("u_orderby").ToString))
            lin = lin + 1
            PrintLine(1, TAB(1), (DRP("STREET").ToString), TAB(45), "Transport :", SPC(1), (DRP("u_transport").ToString))
            lin = lin + 1
            PrintLine(1, TAB(1), (Trim(DRP("CITY")).ToString), SPC(1), "-", SPC(1), (Trim(DRP("ZIPCODE")).ToString), TAB(45), "Goods to :", SPC(1), (DRP("u_dsnation").ToString))
            lin = lin + 1
            PrintLine(1, TAB(1), "GST IN.", SPC(1), (Trim(DRP("TINNO")).ToString))
            lin = lin + 1
            PrintLine(1, TAB(0), "---------------------------------------------------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(25), DRPCMP.Item("CompnyName").ToString, TAB(50), DRPCMP.Item("DflBnkAcct").ToString)
            lin = lin + 1
            PrintLine(1, TAB(10), "Bank Details")
            lin = lin + 1
            PrintLine(1, TAB(25), (DRPCMP("BankName")), TAB(50), (DRPCMP("SwiftNum")))
            lin = lin + 1
            PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(0), Chr(27) + Chr(69) + "PARTICULARS             HSNCODE Style Size Rate/PC DISC NET RATE  QUANTITY TAX  TAXABLE" + Chr(27) + Chr(70) + Chr(18))
            PrintLine(1, TAB(0), Chr(27) + Chr(69) + "                                                     %  RATE/PC   BOXS PCS RATE  VALUE" + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(1), (DRP("item").ToString), TAB(25), (DRP("U_HSNCODE").ToString), TAB(35), (DRP("u_style").ToString), TAB(40), (DRP("u_size").ToString), TAB(50 - Len(Microsoft.VisualBasic.Format(DRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("PriceBefDi"), "#######0.00"), TAB(52), Microsoft.VisualBasic.Format(DRP("LDiscPrcnt"), "#######0"), TAB(55), Microsoft.VisualBasic.Format(DRP("box"), "#######0"), TAB(63 - Len(Microsoft.VisualBasic.Format(DRP("quantity"), "#######0"))), Microsoft.VisualBasic.Format(DRP("quantity"), "#######0"), TAB(65), Microsoft.VisualBasic.Format(DRP("LTaxRate"), "#######0"), TAB(77 - Len(Microsoft.VisualBasic.Format(DRP("LinetValue"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("LinetValue"), "#######0.00"))
            mtotamt = mtotamt + DRP("LinetValue")
            mtotqty = mtotqty + DRP("quantity")
            mbox = mbox + DRP("box")

            While DRP.Read
                PrintLine(1, TAB(1), (DRP("item").ToString), TAB(25), (DRP("U_HSNCODE").ToString), TAB(35), (DRP("u_style").ToString), TAB(40), (DRP("u_size").ToString), TAB(50 - Len(Microsoft.VisualBasic.Format(DRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("PriceBefDi"), "#######0.00"), TAB(52), Microsoft.VisualBasic.Format(DRP("LDiscPrcnt"), "#######0"), TAB(55), Microsoft.VisualBasic.Format(DRP("box"), "#######0"), TAB(63 - Len(Microsoft.VisualBasic.Format(DRP("quantity"), "#######0"))), Microsoft.VisualBasic.Format(DRP("quantity"), "#######0"), TAB(65), Microsoft.VisualBasic.Format(DRP("LTaxRate"), "#######0"), TAB(77 - Len(Microsoft.VisualBasic.Format(DRP("LinetValue"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("LinetValue"), "#######0.00"))
                lin = lin + 1
                mtotamt = mtotamt + DRP("LinetValue")
                mtotqty = mtotqty + DRP("quantity")
                mbox = mbox + DRP("box")
                If lin > 47 Then
                    n = 61 - lin
                    For k = 1 To n
                        PrintLine(1, Space(5), "")
                        lin = lin + 1
                    Next k
                    PrintLine(1, TAB(20), Chr(27) + Chr(69) + "Continue........", SPC(1), PAG + 1, TAB(59), Microsoft.VisualBasic.Format(mbox, "#####0"), TAB(67 - Len(Microsoft.VisualBasic.Format(mtotqty, "#######0"))), Microsoft.VisualBasic.Format(mtotqty, "#######0"), TAB(79 - Len(Microsoft.VisualBasic.Format(mtotamt, "#######0.00"))), Microsoft.VisualBasic.Format(mtotamt, "#######0.00") + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    n = 71 - lin
                    For k = 1 To n
                        PrintLine(1, Space(5), "")
                        lin = lin + 1
                    Next k
                    PAG = PAG + 1
                    lin = 0
                    PrintLine(1, TAB(5), Chr(27) + Chr(69) + DRPCMP.Item("CompnyName").ToString + Chr(27) + Chr(70) + Chr(18), SPC(1), TAB(61), Chr(27) + Chr(69) + "DUPLICATE FOR TRANSPORTER" + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, TAB(5), (DRPCMP("BUILDING").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(5), (DRPCMP("BLOCK").ToString), SPC(1), (DRPCMP("StreetNo").ToString), TAB(65), Chr(27) + Chr(69) + (DRP.Item("U_remarks").ToString), SPC(1), "", (DRP.Item("Docnum").ToString + Chr(27) + Chr(70) + Chr(18)))
                    lin = lin + 1
                    PrintLine(1, TAB(5), (DRPCMP("StreetNo").ToString), SPC(1), TAB(65), Microsoft.VisualBasic.FormatDateTime(DRP("docdate"), DateFormat.ShortDate))
                    lin = lin + 1
                    PrintLine(1, TAB(5), (DRPCMP("Street").ToString), SPC(1), (Trim(DRPCMP("CITY")).ToString), SPC(1), "-", SPC(1), (Trim(DRPCMP("ZIPCODE")).ToString), SPC(1), (Trim(DRPCMP("State")).ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(5), "Customer Care : ", Space(1), (DRPCMP("Phone1").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(5), "e-mail : ", Space(1), (DRPCMP("E_Mail").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(5), "State Code : ", Space(1), (DRPCMP("eCode").ToString), Space(1), "PAN : ", Space(1), (DRPCMP("RevOffice").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(5), "GST IN : ", Space(1), (DRPCMP("DdctFileNo").ToString))
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, TAB(5), "To,", TAB(55), (DRP("baseref").ToString), TAB(74), (DRP("u_lrno").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(5), Chr(27) + Chr(69) + DRP.Item("Cardfname").ToString + Chr(27) + Chr(70) + Chr(18), TAB(55), "Case NO", TAB(74), "L.R.Dt")
                    lin = lin + 1
                    PrintLine(1, TAB(5), (DRP("BUILDING").ToString), TAB(65), (DRP("u_arcode").ToString), TAB(74), Microsoft.VisualBasic.Format(DRP("u_lrwight"), "#######0.000"))
                    lin = lin + 1
                    PrintLine(1, TAB(5), (DRP("BLOCK").ToString), TAB(55), (DRP("u_docthrough")), TAB(74), (DRP("u_orderby").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(5), (DRP("STREET").ToString), TAB(55), (DRP("u_transport").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(5), (Trim(DRP("CITY")).ToString), SPC(1), "-", SPC(1), (Trim(DRP("ZIPCODE")).ToString), TAB(55), (DRP("u_dsnation").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(5), "GST IN.", SPC(1), (Trim(DRP("TINNO")).ToString))
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, TAB(25), DRPCMP.Item("CompnyName").ToString, TAB(50), DRPCMP.Item("DflBnkAcct").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(10), "Bank Details")
                    lin = lin + 1
                    PrintLine(1, TAB(25), (DRPCMP("BankName")), TAB(50), (DRPCMP("SwiftNum")))
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                End If
            End While
            DRP.Close()
            con.Close()
            con.Close()


            n = 47 - lin
            For k = 1 To n
                PrintLine(1, Space(5), "")
                lin = lin + 1
            Next k

            TSQL = "SELECT convert(nvarchar(max),k.Building) Building,convert(nvarchar(max),k.Block) Block,convert(nvarchar(max),k.City) City,convert(nvarchar(max),k.STATE) STATE,convert(nvarchar(max),k.Street) Street,convert(nvarchar(max),k.ZipCode) ZipCode,convert(nvarchar(max),k.country) country,convert(nvarchar(max),k.county) county,  QryGroup8, ('M/s. ' + Cardfname) Cardfname,  trdis + '%  = ' + convert(nvarchar(max),sum(cast(SchemeDiscAmt as numeric(19,2)))) TRadeDiscount,isnull(Esugam,'') Esugam,SUM(BeforeDisc) BeforeDisc,DiscAmt,MAX(SchemeDiscPercent)SchemeDiscPercent,[FORWARDING CHARGES] AS FORWARDINGCHARGES,DiscSum,sum(SchemeDiscAmt) SchemeDiscAmt ," & vbCrLf _
            & "(((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum ) ttl1,VatSum,RoundDif, (((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES] + VatSum + RoundDif ) GRANDTOTAL" & vbCrLf _
            & ",taxcode,CASE WHEN TransCat = 'Form C' THEN '[Against C Form]' ELSE ' ' END  CFROM,CASE WHEN TransCat = 'Form H' THEN 'TAX EXEMPTED AGAINST FORM H' ELSE ' ' END  HFROM,SUM(Quantity) QTY,SUM(box)  BOX" & vbCrLf _
            & "FROM (SELECT crd.QryGroup8,Cardfname,b.LineNum,(b.Quantity*b.PriceBefDi) BeforeDisc,isnull(G.[Disc Amt],0) DiscAmt ,ISNULL(S.[FORWARDING CHARGES],0) [FORWARDING CHARGES]," & vbCrLf _
            & "case when c.State = 'KA' Then 'e-sugam no.' + a.U_ESugam when c.State in ('AP','TS') Then 'e-wayBill no.' + a.U_ESugam  when c.State in ('KL') Then 'e-Token No.' + a.U_ESugam  else '' end Esugam," & vbCrLf _
            & "case when isnull(convert(nvARCHAR(MAX),crd.U_Dis1),'') <> '' then 'Trade Dsicount   ' + convert(nvarchar(max),(cast(crd.U_Dis1 as numeric(19,0))))   else '' end  trdis," & vbCrLf _
            & "sh.Building,isnull(sh.Block,'') Block,sh.City,CASE WHEN sh.State='OS' THEN sh.StreetNo WHEN crd.QryGroup5='Y' THEN sh.StreetNo ELSE ST.Name END AS STATE,sh.State State1,sh.ZipCode,isnull(sh.Street,'')Street,sh.country,sh.county," & vbCrLf _
            & "a.DiscSum,A.VatSum,A.RoundDif,B.TaxCode,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt',C.TransCat,B.Quantity,(b.Quantity/ITM.SalPackUn) Box,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent'" & vbCrLf _
            & "FROM " & TABLE & " A JOIN " & TABLE1 & " B ON B.DocEntry = A.DocEntry" & vbCrLf _
            & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry,sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from " & TABLE1 & " where DiscPrcnt=100 group by DocEntry) G on G.DocEntry=B.DocEntry" & vbCrLf _
            & "left OUTER join (Select sum(linetotal) [FORWARDING CHARGES],DocEntry from " & TABLE3 & " Group by Docentry) S ON S.DocEntry=B.DocEntry" & vbCrLf _
            & "left join  OCRD crd on crd.CardCode = a.CardCode" & vbCrLf _
            & "left join crd1 sh on sh.cardcode = a.cardcode AND sh.AdresType='S'" & vbCrLf _
            & "Left Outer Join OCST ST ON sh.State=ST.Code AND sh.Country=ST.Country AND ST.Country='IN'" & vbCrLf _
            & "left join OITM as itm on itm.ItemCode = b.ItemCode" & vbCrLf _
            & "Left join " & TABLE12 & " as c on c.DocEntry = b.DocEntry" & vbCrLf _
            & "WHERE A.docnum = (" & No.Text & ") and   a.PIndicator in ('" & Year.Text & "')  and b.Treetype <> 'I'  and b.ItemCode<>'FrightCharges' ) k GROUP BY QryGroup8,Cardfname,convert(nvarchar(max),k.Building) ,convert(nvarchar(max),k.Block) ,convert(nvarchar(max),k.City) ,convert(nvarchar(max),k.STATE) ,convert(nvarchar(max),k.Street) ,convert(nvarchar(max),k.ZipCode) ,convert(nvarchar(max),k.country) ,convert(nvarchar(max),k.county) ,trdis,DiscAmt,[FORWARDING CHARGES],DiscSum,VatSum,RoundDif,taxcode,TransCat,k.Esugam"

            TSQLTAX = "select  b.Name,(convert(nvarchar(max),replace(TaxRate,0,'')) + '%') TaxRate ,sum(TaxSum) Taxamount  from inv4 a Left join OSTT b on b.AbsId = a.staType WHERE  a.docentry in (" & Label7.Text & ") group by TAXRATE,name"


            Dim CMD As New SqlCommand(TSQL, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim RP As SqlDataReader
            RP = CMD.ExecuteReader
            RP.Read()

            Dim CMDtax As New SqlCommand(TSQLTAX, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim RPtax As SqlDataReader
            RPtax = CMDtax.ExecuteReader
            RPtax.Read()


            If Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), Chr(27) + Chr(69) + "Delivery Address : " + Chr(27) + Chr(70) + Chr(18), Space(5), "")
            ElseIf Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), Chr(27) + Chr(69) + "Delivery Address : " + Chr(27) + Chr(70) + Chr(18), TAB(71), "-------------")
            End If

            lin = lin + 1
            If Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), Chr(27) + Chr(69) + RP.Item("Cardfname").ToString + Chr(27) + Chr(70) + Chr(18), Space(5), "")
            ElseIf Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), Chr(27) + Chr(69) + RP.Item("Cardfname").ToString + Chr(27) + Chr(70) + Chr(18), TAB(66), Chr(27) + Chr(69) + "", TAB(84 - Len(Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") + Chr(27) + Chr(70) + Chr(18))
            End If
            lin = lin + 1


            lin = lin + 1
            If Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RP.Item("Building").ToString, TAB(40), "Add : Forwarding Charges", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00"))
            ElseIf Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), RP.Item("Building").ToString, Space(5), "")
            End If
            lin = lin + 1
            PrintLine(1, TAB(40), "Add :", SPC(1), RPtax("NAME"), SPC(1), RPtax("TAXRATE"), TAB(77 - Len(Microsoft.VisualBasic.Format(RPtax("Taxamount"), "#######0.00"))), Microsoft.VisualBasic.Format(RPtax("Taxamount"), "#######0.00"))
            While RPtax.Read
                PrintLine(1, TAB(40), "Add :", SPC(1), RPtax("NAME"), SPC(1), RPtax("TAXRATE"), TAB(77 - Len(Microsoft.VisualBasic.Format(RPtax("Taxamount"), "#######0.00"))), Microsoft.VisualBasic.Format(RPtax("Taxamount"), "#######0.00"))
                lin = lin + 1
            End While
            PrintLine(1, TAB(1), RP.Item("BLOCK").ToString, Space(5), "")
            ''If Microsoft.VisualBasic.Format(RP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(0), RP("TRadeDiscount"), SPC(1), TAB(40), "Less Discount", SPC(1), Microsoft.VisualBasic.Format((RP("SchemeDiscPercent")), "#######0"), TAB(56), SPC(1), "%", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(1), RP.Item("STREET").ToString, Space(5), "")
            ''If Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount :  ", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(1), RP.Item("CITY").ToString, SPC(1), "-", SPC(1), RP.Item("ZIPCODE").ToString)

            lin = lin + 1
            If Microsoft.VisualBasic.Format(RP("RoundDif"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Round Off :  ", TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RP("RoundDif"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("RoundDif"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, SPC(0), "---------------------------------------------------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(33), Chr(27) + Chr(69) + "Grand Total :", TAB(59), Microsoft.VisualBasic.Format(RP("box"), "#####0"), TAB(67 - Len(Microsoft.VisualBasic.Format(RP("qty"), "#######0"))), Microsoft.VisualBasic.Format(RP("qty"), "#######0"), TAB(79 - Len(Microsoft.VisualBasic.Format(RP("GRANDTOTAL"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("GRANDTOTAL"), "#######0.00") + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, SPC(0), "---------------------------------------------------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(2), Chr(27) + Chr(69) + "", SPC(1), RupeesToWord(RP("GRANDTOTAL")), SPC(1), "" + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, SPC(0), "---------------------------------------------------------------------------------")
            lin = lin + 1
            PrintLine(1, SPC(1), "Terms", TAB(61), "For ", SPC(1), Chr(27) + Chr(69) + "R and R Textile" + Chr(27) + Chr(70) + Chr(18), SPC(1))
            lin = lin + 1
            PrintLine(1, SPC(1), "*Payment Should Be made by A/C Payee Demand draft only.")
            lin = lin + 1
            PrintLine(1, SPC(1), "*Our responsibility ceases when the goods leave our premises / Godown.")
            lin = lin + 1
            PrintLine(1, SPC(1), "*Subject to Jurisdiction")
            lin = lin + 1
            PrintLine(1, Space(1), "")
            lin = lin + 1
            PrintLine(1, SPC(1), "Prepared By                Checked By                    Authorised Signatory")
            lin = lin + 1

            n = 71 - lin
            For k = 1 To n
                lin = lin + 1
                ''PrintLine(1, Space(5), "")
            Next k

            lin = 0
            mbox = 0
            mmtr = 0
            mtotqty = 0
            mtotamt = 0



            FileClose(1)
            RP.Close()
            con.Close()
            con.Close()

            ''Shell("print /d:LPT" & lpt.Text & " " + dbreportpath + "Rrlorry.txt", vbNormalFocus)
            ''Shell("print /d:LPT" & lpt.Text & " dbreportpath +"RRlorry.txt", vbNormalFocus)
            'Shell("print /d:LPT" & lpt.Text & " " + dbreportpath + "\RRlorryNew.txt", vbNormalFocus)
            Dim printer As String = mlsprinter 'laserprint
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "RRlorryNew.txt"
            Dim success As Boolean = PrintTextFile(filePathname)






        ElseIf cmp.Text = "RHL" Then

            'Call MAIN()
            RHLSQL = "select CASE WHEN C.TransCat = 'FORM H' THEN 'Taxable' WHEN A.VatSum = '0' THEN 'Non  Taxable' ELSE 'Taxable' End AS TAXABLE, A.DOCENTRY,isnull(b.LineNum,'') LineNum,a.address,a.address2,A.cardname,a.U_Noofbun,r.Building,isnull(r.Block,'') Block,(r.City+'-'+ r.ZipCode) city , " & vbCrLf _
    & "CASE WHEN R.State='OS' THEN R.StreetNo WHEN l.QryGroup5='Y' THEN R.StreetNo ELSE ST.Name END AS STATE,r.State State1, " & vbCrLf _
    & "isnull(r.Street,'')Street,r.country,r.county, A.u_ESUGAM,A.docnum,a.Docdate,A.u_orderby,A.u_arcode,A.u_transport,A.u_docthrough,A.u_lrno,convert(varchar(10),A.u_lrdat,110) u_lrdat,A.u_lrwight,a.u_dsnation,a.U_lrval AS topay, " & vbCrLf _
    & "CASE when isnull(b.FreeTxt,'') = '' then isnull(B.U_CatalogCode ,'')  else  isnull(B.FreeTxt,'')  end  as item, " & vbCrLf _
    & "b.u_style,CASE WHEN L.Cellular IS NULL THEN L.Phone1 ELSE L.Cellular END Cellular ,b.u_size,b.baseref,b.price,b.linetotal,b.quantity,a.DiscSum,a.VatSum,a.RoundDif,D.firstname,f.U_remarks,b.taxcode, " & vbCrLf _
    & "b.BaseREf,b.U_NoofPiece,b.Volume Box,a.TotalExpns,c.TransCat,(isnull(cRD.TaxId1,''))as CSTNO,(isnull(cRD.TaxId11,'')) as TINNO,l.CardFName, " & vbCrLf _
    & "a.U_Dis1'TRADE DISCOUNT',a.U_Dis2'CASH DISCOUNT',a.U_Dis3'CD/AD DRAFT DISC',a.U_Dis4'CD/AD/QTY DISC' " & vbCrLf _
    & ",A.U_AreaCode,a.U_Dis5'CD/LR-AGAINST',a.U_Dis6'QTY DISCOUNT',a.U_Dis7'SPL DISCOUNT',a.U_Dis8'VAT EXCEMPATION',a.U_Dis9'TURNOVER' " & vbCrLf _
    & ",isnull(a.U_Dis10,0)'VAT DISCOUNT',a.DiscPrcnt,Left(b.Taxcode,3) as TAXCODE,a.numatcard, " & vbCrLf _
    & "RTRIM(convert(nvarchar(100),isnull(r.building,'')))+','+rtrim(convert(nvarchar(100),isnull(r.block,'')))+','+RTRIM(convert(nvarchar(100),isnull(r.Street,'')))+'-'+RTRIM(convert(nvarchar(50),isnull(r.ZipCode,'')))+','+rtrim(CONVERT(nvarchar(100),isnull(r.city,'')))+','+ " & vbCrLf _
    & "RTRIM(CONVERT(nvarchar(10),isnull(r.State,'')))+','+RTRIM(CONVERT(nvarchar(50),ISNULL(r.county,'')))+','+RTRIM(CONVERT(nvarchar(10),ISNULL(r.country,''))) as address3 " & vbCrLf _
    & ",Isnull(Convert(Nvarchar,P.U_Remarks)+'-'+'','') [Item Remarks],b.PriceBefDi,b.Quantity*b.PriceBefDi [Before Disc],b.LineTotal,G.DiscPrcnt [Item Disc] " & vbCrLf _
    & ",ISNULL(S.[FORWARDING CHARGES],0) [FORWARDING CHARGES],b.DiscPrcnt [Item Disc Prct], " & vbCrLf _
    & "isnull(G.[Disc Amt],0) [Disc Amt] " & vbCrLf _
    & ",CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent' " & vbCrLf _
    & ",CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt',a.cardcode  " & vbCrLf _
     & "from " & TABLE & " A inner join " & TABLE1 & " b on a.DocEntry=b.DocEntry  " & vbCrLf _
    & "left outer join OHEM D on D.empid=a.ownercode  " & vbCrLf _
    & "Left Join [@INCM_BND1] F on F.U_Name = a.U_brand  " & vbCrLf _
    & "Left join " & TABLE12 & " as c on c.DocEntry = b.DocEntry	 " & vbCrLf _
    & "left join OITM as itm on itm.ItemCode = b.ItemCode  " & vbCrLf _
    & "left join (SELECT * FROM [dbo].[@INS_OPLM] WHERE DocEntry IN (SELECT DocEntry FROM [@INS_PLM1] WHERE U_Remarks IS NOT NULL))  " & vbCrLf _
    & "as itb on itb.U_ItemCode =itm.ItemCode  " & vbCrLf _
    & "left join (SELECT DISTINCT CARDCODE,ISNULL(U_ActTino,'') TAXID11,ISNULL(U_ActCstno,'') TAXID1 FROM  OCRD  ) as crd on crd.CardCode = a.CardCode " & vbCrLf _
    & "left join CRD1 as r on r.CardCode = a.CardCode /*and a.paytocode = r.address*/ AND R.AdresType='B'  " & vbCrLf _
    & "Left Outer Join OCST ST ON R.State=ST.Code AND R.Country=ST.Country AND ST.Country='IN'  " & vbCrLf _
    & "left OUTER join OCRD as l on l.CardCode = a.CardCode  " & vbCrLf _
    & "left OUTER join (SELECT DISTINCT CONVERT(NVARCHAR(MAX),U_ItemName) U_ItemName ,CONVERT(NVARCHAR(MAX),ISNULL(U_Remarks,'')) U_Remarks,U_Subbrand FROM [@INS_PLM1] WHERE u_lock = 'N') P on P.U_ItemName=B.U_CatalogName AND L.U_Subbrand = P.U_Subbrand" & vbCrLf _
    & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry,  " & vbCrLf _
    & "sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from " & TABLE1 & " where DiscPrcnt=100 group by DocEntry) G  " & vbCrLf _
    & "on G.DocEntry=B.DocEntry  " & vbCrLf _
    & "left OUTER join (Select ItemCode,sum(Price) [FORWARDING CHARGES],DocEntry from " & TABLE1 & " WHERE ItemCode='FrEightCharges' group by ItemCode,DocEntry) S ON S.DocEntry=B.DocEntry  " & vbCrLf _
    & "where a.docnum in (" & No.Text & ") and a.PIndicator in ('" & Year.Text & "') and b.Treetype <> 'I'  and b.ItemCode<>'FrEightCharges' order by b.LineNum"


            RHLmtotqty = 0
            RHLmtotamt = 0
            PAG = 0

            Dim CMDp As New SqlCommand(RHLSQL, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim RHLDRP As SqlDataReader
            RHLDRP = CMDp.ExecuteReader
            RHLDRP.Read()

            lin = 0
            FileOpen(1, " " + dbreportpath + "RHLlorry.txt", OpenMode.Output, OpenAccess.Write)
            ''FileOpen(1, Trim(dbreportpath) & "RHLlorry.txt", OpenMode.Output, OpenAccess.Write)
            'FileOpen(1, "e:\Test.txt", OpenMode.Output, OpenAccess.Write)
            PrintLine(1, TAB(33), Chr(27) + Chr(45) + Chr(1) + Chr(14) + Chr(27) + Chr(69) + "COPY INVOICE" + Chr(27) + Chr(70) + Chr(18) + Chr(27) + Chr(45) + Chr(0) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(0), "FROM,", TAB(51), "TO,")
            lin = lin + 1
            PrintLine(1, TAB(0), Chr(27) + Chr(69) + "RAMRAJ HANDLOOMS" + Chr(27) + Chr(70) + Chr(18), TAB(56), "M/s.", SPC(1), Chr(27) + Chr(69) + RHLDRP.Item("CardFName").ToString + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(0), "(A UNIT OF ENES TEXTILE MILLS),", TAB(51), RHLDRP.Item("BUILDING").ToString)
            lin = lin + 1
            PrintLine(1, TAB(0), "RAMRAJ-V-TOWER,", TAB(51), RHLDRP.Item("BLOCK").ToString)
            lin = lin + 1
            PrintLine(1, TAB(0), "10-SENGUNTHAPURAM,", TAB(51), RHLDRP.Item("STREET").ToString)
            lin = lin + 1
            PrintLine(1, TAB(0), "MANGALAM ROAD,TIRUPUR-641604.", TAB(51), RHLDRP.Item("CITY").ToString)
            lin = lin + 1
            PrintLine(1, TAB(0), "PH:0421-4304147", TAB(51), RHLDRP.Item("county").ToString, SPC(1), "Dt.", SPC(1), "(", RHLDRP.Item("state1").ToString, SPC(1), ")")
            lin = lin + 1
            PrintLine(1, TAB(0), "TIN NO.: 33652323660", TAB(51), "TIN NO.:", SPC(1), RHLDRP.Item("TINNO").ToString)
            lin = lin + 1
            PrintLine(1, TAB(0), "CST NO.: 334666 DATE:05.09.03", TAB(51), "CST NO.:", SPC(1), RHLDRP.Item("CSTNO").ToString)
            lin = lin + 1
            PrintLine(1, TAB(51), "Mob NO.:", SPC(1), RHLDRP.Item("Cellular").ToString)
            lin = lin + 1
            PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(33), Chr(27) + Chr(69) + "TRANSPORT COPY" + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(0), "AREA CODE :", SPC(1), (RHLDRP("u_arcode").ToString), TAB(32), "BY :", SPC(1), (RHLDRP("u_orderby").ToString), TAB(60), "DATE:", SPC(1), Microsoft.VisualBasic.FormatDateTime(RHLDRP("docdate"), DateFormat.ShortDate))
            lin = lin + 1
            PrintLine(1, TAB(0), "TRANSPORT :", SPC(1), Chr(27) + Chr(69) + (RHLDRP("u_transport").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(37), "TO :", SPC(1), Chr(27) + Chr(69) + (RHLDRP("u_dsnation").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(70), "INV No:", SPC(1), (RHLDRP("docnum").ToString))
            lin = lin + 1
            PrintLine(1, TAB(0), "DOCOUMENT THROUGH :", SPC(1), (RHLDRP("u_docthrough")), TAB(60), "BUNDLE:", SPC(1), Chr(27) + Chr(69) + (RHLDRP("U_Noofbun").ToString) + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(0), Chr(27) + Chr(69) + "SIZE         PARTICULARS                 QTY     MTRS     RATE       AMOUNT" + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(1), (RHLDRP("u_size").ToString), TAB(10), (RHLDRP("item").ToString), TAB(45 - Len(Microsoft.VisualBasic.Format(RHLDRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(RHLDRP("U_NoofPiece"), "#######0"), TAB(55 - Len(Microsoft.VisualBasic.Format(RHLDRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLDRP("box"), "#######0.00"), TAB(65 - Len(Microsoft.VisualBasic.Format(RHLDRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLDRP("PriceBefDi"), "#######0.00"), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLDRP("BEFORE DISC"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLDRP("BEFORE DISC"), "#######0.00"))
            RHLmtotamt = RHLmtotamt + RHLDRP("BEFORE DISC")
            RHLmtotqty = RHLmtotqty + RHLDRP("U_NoofPiece")
            RHLmbox = RHLmbox + RHLDRP("box")
            While RHLDRP.Read
                PrintLine(1, TAB(1), (RHLDRP("u_size").ToString), TAB(10), (RHLDRP("item").ToString), TAB(45 - Len(Microsoft.VisualBasic.Format(RHLDRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(RHLDRP("U_NoofPiece"), "#######0"), TAB(55 - Len(Microsoft.VisualBasic.Format(RHLDRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLDRP("box"), "#######0.00"), TAB(65 - Len(Microsoft.VisualBasic.Format(RHLDRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLDRP("PriceBefDi"), "#######0.00"), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLDRP("BEFORE DISC"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLDRP("BEFORE DISC"), "#######0.00"))
                lin = lin + 1
                RHLmtotamt = RHLmtotamt + RHLDRP("BEFORE DISC")
                RHLmtotqty = RHLmtotqty + RHLDRP("U_NoofPiece")
                RHLmbox = RHLmbox + RHLDRP("box")
                If lin > 48 Then
                    n = 59 - lin
                    For k = 1 To n
                        PrintLine(1, Space(5), "")
                        lin = lin + 1
                    Next k
                    PrintLine(1, TAB(66), "-------------")
                    lin = lin + 1
                    PrintLine(1, TAB(20), "Continue........", SPC(1), PAG + 1, TAB(45 - Len(Microsoft.VisualBasic.Format(RHLmtotqty, "#######0"))), Microsoft.VisualBasic.Format(RHLmtotqty, "#######0"), TAB(55 - Len(Microsoft.VisualBasic.Format(RHLmbox, "#######0.00"))), Microsoft.VisualBasic.Format(RHLmbox, "#######0.00"), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLmtotamt, "#######0.00"))), Microsoft.VisualBasic.Format(RHLmtotamt, "#######0.00"))
                    'PrintLine(1, TAB( Space(5), "")
                    lin = lin + 1

                    n = 72 - lin
                    For k = 1 To n
                        PrintLine(1, Space(5), "")
                    Next k
                    lin = 0
                    PAG = PAG + 1
                    PrintLine(1, TAB(33), Chr(27) + Chr(45) + Chr(1) + Chr(14) + Chr(27) + Chr(69) + "COPY INVOICE" + Chr(27) + Chr(70) + Chr(18) + Chr(27) + Chr(45) + Chr(0) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "FROM,", TAB(51), "TO,")
                    lin = lin + 1
                    PrintLine(1, TAB(0), Chr(27) + Chr(69) + "RAMRAJ HANDLOOMS" + Chr(27) + Chr(70) + Chr(18), TAB(56), "M/s.", SPC(1), Chr(27) + Chr(69) + RHLDRP.Item("CardFName").ToString + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "(A UNIT OF ENES TEXTILE MILLS),", TAB(51), RHLDRP.Item("BUILDING").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(0), "RAMRAJ-V-TOWER,", TAB(51), RHLDRP.Item("BLOCK").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(0), "10-SENGUNTHAPURAM,", TAB(51), RHLDRP.Item("STREET").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(0), "MANGALAM ROAD,TIRUPUR-641604.", TAB(51), RHLDRP.Item("CITY").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(0), "PH:0421-4304147", TAB(51), RHLDRP.Item("county").ToString, SPC(1), "Dt.")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "TIN NO.: 33652323660", TAB(51), "TIN NO.:", SPC(1), RHLDRP.Item("TINNO").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(0), "CST NO.: 334666 DATE:05.09.03", TAB(51), "CST NO.:", SPC(1), RHLDRP.Item("CSTNO").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(51), "Mob NO.:", SPC(1), RHLDRP.Item("Cellular").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
                    lin = lin + 1
                    PrintLine(1, TAB(33), Chr(27) + Chr(69) + "TRANSPORT COPY" + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "AREA CODE :", SPC(1), (RHLDRP("u_arcode").ToString), TAB(32), "BY :", SPC(1), (RHLDRP("u_orderby").ToString), TAB(60), "DATE:", SPC(1), Microsoft.VisualBasic.FormatDateTime(RHLDRP("docdate"), DateFormat.ShortDate))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "TRANSPORT :", SPC(1), Chr(27) + Chr(69) + (RHLDRP("u_transport").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(37), "TO :", SPC(1), Chr(27) + Chr(69) + (RHLDRP("u_dsnation").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(70), "INV No:", SPC(1), (RHLDRP("docnum").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "DOCOUMENT THROUGH :", SPC(1), (RHLDRP("u_docthrough")), TAB(60), "BUNDLE:", SPC(1), Chr(27) + Chr(69) + (RHLDRP("U_Noofbun").ToString) + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
                    lin = lin + 1
                    PrintLine(1, TAB(0), Chr(27) + Chr(69) + "SIZE         PARTICULARS                 QTY     MTRS     RATE       AMOUNT" + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
                    lin = lin + 1
                End If
            End While
            RHLDRP.Close()
            con.Close()
            con.Close()


            n = 48 - lin
            For k = 1 To n
                PrintLine(1, Space(5), "")
                lin = lin + 1
            Next k

            RHLTSQL = "SELECT convert(nvarchar(max),k.Building) Building,convert(nvarchar(max),k.Block) Block,convert(nvarchar(max),k.City) City,convert(nvarchar(max),k.STATE) STATE,convert(nvarchar(max),k.Street) Street,convert(nvarchar(max),k.ZipCode) ZipCode,convert(nvarchar(max),k.country) country,convert(nvarchar(max),k.county) county,  QryGroup14, ('M/s. ' + Cardfname) Cardfname, isnull(Esugam,'') Esugam,discprcnt,SUM(BeforeDisc) BeforeDisc,DiscAmt,MAX(SchemeDiscPercent)SchemeDiscPercent,[FORWARDING CHARGES] AS FORWARDINGCHARGES,DiscSum,sum(SchemeDiscAmt) SchemeDiscAmt ," & vbCrLf _
    & "(((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES] ) ttl1,VatSum,RoundDif, (((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES] + VatSum + RoundDif ) GRANDTOTAL" & vbCrLf _
    & ",taxcode,CASE WHEN TransCat = 'Form C' THEN '[Against C Form]' ELSE ' ' END  CFROM,CASE WHEN TransCat = 'Form H' THEN 'TAX EXEMPTED AGAINST FORM H' ELSE ' ' END  HFROM,sum(U_NoofPiece)U_NoofPiece,SUM(Quantity) QTY,SUM(box)  BOX" & vbCrLf _
    & "FROM (SELECT  crd.QryGroup14,Cardfname, (b.Quantity*b.PriceBefDi) BeforeDisc,isnull(G.[Disc Amt],0) DiscAmt ,ISNULL(S.[FORWARDING CHARGES],isnull(fr.LineTotal,0)) [FORWARDING CHARGES]," & vbCrLf _
    & "case when c.State = 'KA' Then 'e-sugam no.' + a.U_ESugam when c.State in ('AP','TS') Then 'e-wayBill no.' + a.U_ESugam  when c.State in ('KL') Then 'e-Token No.' + a.U_ESugam  else '' end Esugam," & vbCrLf _
    & "sh.Building,isnull(sh.Block,'') Block,sh.City,CASE WHEN sh.State='OS' THEN sh.StreetNo WHEN crd.QryGroup5='Y' THEN sh.StreetNo ELSE ST.Name END AS STATE,sh.State State1,sh.ZipCode,isnull(sh.Street,'')Street,sh.country,sh.county," & vbCrLf _
    & "a.discprcnt,a.DiscSum,A.VatSum,A.RoundDif,B.TaxCode,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt',C.TransCat,b.U_NoofPiece,B.Quantity,(b.Volume) Box,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent'" & vbCrLf _
    & "FROM " & TABLE & " A JOIN " & TABLE1 & " B ON B.DocEntry = A.DocEntry" & vbCrLf _
    & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry,sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from " & TABLE1 & " where DiscPrcnt=100 group by DocEntry) G on G.DocEntry=B.DocEntry" & vbCrLf _
    & "left OUTER join (Select ItemCode,sum(Price) [FORWARDING CHARGES],DocEntry from " & TABLE1 & " WHERE ItemCode='FrEightCharges' group by  ItemCode,DocEntry) S ON S.DocEntry=B.DocEntry  " & vbCrLf _
    & "left join OITM as itm on itm.ItemCode = b.ItemCode" & vbCrLf _
    & "left join  OCRD crd on crd.CardCode = a.CardCode" & vbCrLf _
    & "left join crd1 sh on sh.cardcode = a.cardcode AND sh.AdresType='S'" & vbCrLf _
     & "Left Outer Join OCST ST ON sh.State=ST.Code AND sh.Country=ST.Country AND ST.Country='IN'" & vbCrLf _
    & "Left join " & TABLE12 & " as c on c.DocEntry = b.DocEntry" & vbCrLf _
    & "Left join " & TABLE3 & " as fr on fr.DocEntry = b.DocEntry" & vbCrLf _
    & "WHERE A.docnum = (" & No.Text & ") and   a.PIndicator in ('" & Year.Text & "') and b.TreeType <> 'I' and b.ItemCode<>'FrEightCharges'  ) k GROUP BY  QryGroup14,Cardfname,convert(nvarchar(max),k.Building) ,convert(nvarchar(max),k.Block) ,convert(nvarchar(max),k.City) ,convert(nvarchar(max),k.STATE) ,convert(nvarchar(max),k.Street) ,convert(nvarchar(max),k.ZipCode) ,convert(nvarchar(max),k.country) ,convert(nvarchar(max),k.county) ,DiscAmt,[FORWARDING CHARGES],DiscSum,VatSum,RoundDif,taxcode,TransCat,Esugam,discprcnt"




            Dim CMD As New SqlCommand(RHLTSQL, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim RHLRP As SqlDataReader
            RHLRP = CMD.ExecuteReader
            RHLRP.Read()





            If RHLRP.Item("QryGroup14").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), "Delivery Address : ", TAB(5), Space(5), "")
            ElseIf RHLRP.Item("QryGroup14").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), "Delivery Address : ", TAB(66), "-------------")
            ElseIf RHLRP.Item("QryGroup14").ToString = "N" And Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(66), "-------------")
            Else
                PrintLine(1, Space(5), "")
            End If
            ' If Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(66), "-------------")
            lin = lin + 1



            If RHLRP.Item("QryGroup14").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), RHLRP.Item("Cardfname").ToString, TAB(5), Space(5), "")
            ElseIf RHLRP.Item("QryGroup14").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), RHLRP.Item("Cardfname").ToString, TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00"))
            ElseIf RHLRP.Item("QryGroup14").ToString = "N" And Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00"))
            Else
                PrintLine(1, Space(5), "")
            End If

            '   If Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00"))
            lin = lin + 1


            If RHLRP.Item("QryGroup14").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RHLRP.Item("Building").ToString, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00"))
            ElseIf RHLRP.Item("QryGroup14").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00") = "0.00" Then
                PrintLine(1, TAB(1), RHLRP.Item("Building").ToString, Space(5), "")
            ElseIf RHLRP.Item("QryGroup14").ToString = "N" And Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00"))
            Else
                PrintLine(1, Space(5), "")
            End If



            'If Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1


            If RHLRP.Item("QryGroup14").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RHLRP.Item("BLOCK").ToString, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((RHLRP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("SchemeDiscAmt"), "#######0.00"))
            ElseIf RHLRP.Item("QryGroup14").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("SchemeDiscPercent"), "#######0.00") = "0.00" Then
                PrintLine(1, TAB(1), RHLRP.Item("BLOCK").ToString, Space(5), "")
            ElseIf RHLRP.Item("QryGroup14").ToString = "N" And Microsoft.VisualBasic.Format(RHLRP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((RHLRP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("SchemeDiscAmt"), "#######0.00"))
            Else
                PrintLine(1, Space(5), "")
            End If


            ' If Microsoft.VisualBasic.Format(RHLRP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((RHLRP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("SchemeDiscAmt"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1


            If RHLRP.Item("QryGroup14").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RHLRP.Item("STREET").ToString, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00"))
            ElseIf RHLRP.Item("QryGroup14").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RHLRP.Item("STREET").ToString, Space(5), "")
            ElseIf RHLRP.Item("QryGroup14").ToString = "N" And Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00"))
            Else
                PrintLine(1, Space(5), "")
            End If


            ' If Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1


            If RHLRP.Item("QryGroup14").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RHLRP.Item("CITY").ToString, SPC(1), "-", SPC(1), RHLRP.Item("ZIPCODE").ToString, TAB(40), "Less Discount :  ", SPC(1), Microsoft.VisualBasic.Format((RHLRP("discprcnt")), "#######0"), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00"))
            ElseIf RHLRP.Item("QryGroup14").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00") = "0.00" Then
                PrintLine(1, TAB(1), RHLRP.Item("CITY").ToString, Space(5), "")
            ElseIf RHLRP.Item("QryGroup14").ToString = "N" And Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(40), "Less Discount :  ", SPC(1), Microsoft.VisualBasic.Format((RHLRP("discprcnt")), "#######0"), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00"))
            Else
                PrintLine(1, Space(5), "")
            End If


            '  If Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount :  ", SPC(1), Microsoft.VisualBasic.Format((RHLRP("discprcnt")), "#######0"), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1






            PrintLine(1, TAB(0), RHLRP("Esugam"), SPC(1), TAB(66), "-------------")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RHLRP("ttl1"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("ttl1"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("ttl1"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RHLRP("vatsum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(0), (RHLRP("HFROM")), TAB(30), "Add : ", TAB(40), (RHLRP("CFROM")), SPC(1), (RHLRP("taxcode")), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("vatsum"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("vatsum"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RHLRP("RoundDif"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Round Off :  ", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("RoundDif"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("RoundDif"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(40), "----------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(23), Chr(27) + Chr(69) + "Grand Total :", SPC(1), TAB(48 - Len(Microsoft.VisualBasic.Format(RHLRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(RHLRP("U_NoofPiece"), "#######0"), TAB(58 - Len(Microsoft.VisualBasic.Format(RHLRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("box"), "#######0.00"), TAB(79 - Len(Microsoft.VisualBasic.Format(RHLRP("GRANDTOTAL"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("GRANDTOTAL"), "#######0.00") + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(40), "----------------------------------------")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(2), "", SPC(1), RupeesToWord(RHLRP("GRANDTOTAL")), SPC(1), "")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RHLRP("vatsum"), "#######0.00") = "0.00" Then PrintLine(1, TAB(2), "") Else PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(66), Chr(27) + Chr(69) + "For RAMRAJ HANDLOOMS" + Chr(27) + Chr(70) + Chr(18))
            n = 72 - lin
            For k = 1 To n
                '' PrintLine(1, Space(5), "")
            Next k

            lin = 0
            mbox = 0
            mmtr = 0
            mtotqty = 0
            mtotamt = 0



            FileClose(1)
            RHLRP.Close()
            con.Close()
            con.Close()

            'Shell("print /d:LPT" & lpt.Text & " " + dbreportpath + "RHLlorry.txt", vbNormalFocus)

            'Dim printer As String = "" 'laserprint
            'Dim filePath As String = mlinpath
            'Dim filePathname As String = mlinpath & "RHLlorry.txt"
            'PrintTextFile(printer, filePathname)

            Dim printer As String = mlsprinter  'laserprint
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "RHLlorry.txt.txt"
            Dim success As Boolean = PrintTextFile(filePathname)

            'Shell("command.com /c TYPE " & " dbreportpath +"TestFile.txt>PRN", AppWinStyle.Hide)
            ''   Shell("print /d:LPT" & lpt.Text & " dbreportpath +"RRlorry.txt", vbNormalFocus)
        ElseIf cmp.Text = "RCC" Then
            'Call MAIN()
            rccsql = "select CASE WHEN C.TransCat = 'FORM H' THEN 'Taxable' WHEN A.VatSum = '0' THEN 'Non  Taxable' ELSE 'Taxable' End AS TAXABLE, A.DOCENTRY,isnull(b.LineNum,'') LineNum,a.address,a.address2,A.cardname,a.U_Noofbun,r.Building,isnull(r.Block,'') Block,(r.City+'-'+ r.ZipCode) city , " & vbCrLf _
    & "CASE WHEN R.State='OS' THEN R.StreetNo WHEN l.QryGroup5='Y' THEN R.StreetNo ELSE ST.Name END AS STATE,r.State State1, " & vbCrLf _
    & "isnull(r.Street,'')Street,r.country,r.county, A.u_ESUGAM,A.docnum,a.Docdate,A.u_orderby,A.u_arcode,A.u_transport,A.u_docthrough,A.u_lrno,convert(varchar(10),A.u_lrdat,110) u_lrdat,A.u_lrwight,a.u_dsnation,a.U_lrval AS topay, " & vbCrLf _
    & "CASE when isnull(b.FreeTxt,'') = '' then isnull(B.U_CatalogCode ,'')  else  isnull(B.FreeTxt,'')  end as item, " & vbCrLf _
    & "b.u_style,CASE WHEN L.Cellular IS NULL THEN L.Phone1 ELSE L.Cellular END Cellular ,b.u_size,b.baseref,b.price,b.linetotal,b.quantity,a.DiscSum,a.VatSum,a.RoundDif,D.firstname,f.U_remarks,b.taxcode, " & vbCrLf _
    & "b.BaseREf,b.U_NoofPiece,b.Volume Box,a.TotalExpns,c.TransCat,(isnull(cRD.TaxId1,''))as CSTNO,(isnull(cRD.TaxId11,'')) as TINNO,l.CardFName, " & vbCrLf _
    & "a.U_Dis1'TRADE DISCOUNT',a.U_Dis2'CASH DISCOUNT',a.U_Dis3'CD/AD DRAFT DISC',a.U_Dis4'CD/AD/QTY DISC' " & vbCrLf _
    & ",A.U_AreaCode,a.U_Dis5'CD/LR-AGAINST',a.U_Dis6'QTY DISCOUNT',a.U_Dis7'SPL DISCOUNT',a.U_Dis8'VAT EXCEMPATION',a.U_Dis9'TURNOVER' " & vbCrLf _
    & ",isnull(a.U_Dis10,0)'VAT DISCOUNT',a.DiscPrcnt,Left(b.Taxcode,3) as TAXCODE,a.numatcard, " & vbCrLf _
    & "RTRIM(convert(nvarchar(100),isnull(r.building,'')))+','+rtrim(convert(nvarchar(100),isnull(r.block,'')))+','+RTRIM(convert(nvarchar(100),isnull(r.Street,'')))+'-'+RTRIM(convert(nvarchar(50),isnull(r.ZipCode,'')))+','+rtrim(CONVERT(nvarchar(100),isnull(r.city,'')))+','+ " & vbCrLf _
    & "RTRIM(CONVERT(nvarchar(10),isnull(r.State,'')))+','+RTRIM(CONVERT(nvarchar(50),ISNULL(r.county,'')))+','+RTRIM(CONVERT(nvarchar(10),ISNULL(r.country,''))) as address3 " & vbCrLf _
    & ",Isnull(Convert(Nvarchar,P.U_Remarks)+'-'+'','') [Item Remarks],b.PriceBefDi,b.Quantity*b.PriceBefDi [Before Disc],b.LineTotal,G.DiscPrcnt [Item Disc] " & vbCrLf _
    & ",ISNULL(S.[FORWARDING CHARGES],0) [FORWARDING CHARGES],b.DiscPrcnt [Item Disc Prct], " & vbCrLf _
    & "isnull(G.[Disc Amt],0) [Disc Amt] " & vbCrLf _
    & ",CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent' " & vbCrLf _
    & ",CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt',a.cardcode  " & vbCrLf _
     & "from " & TABLE & " A inner join " & TABLE1 & " b on a.DocEntry=b.DocEntry  " & vbCrLf _
    & "left outer join OHEM D on D.empid=a.ownercode  " & vbCrLf _
    & "Left Join [@INCM_BND1] F on F.U_Name = a.U_brand  " & vbCrLf _
    & "Left join " & TABLE12 & " as c on c.DocEntry = b.DocEntry	 " & vbCrLf _
    & "left join OITM as itm on itm.ItemCode = b.ItemCode  " & vbCrLf _
    & "left join (SELECT * FROM [dbo].[@INS_OPLM] WHERE DocEntry IN (SELECT DocEntry FROM [@INS_PLM1] WHERE U_Remarks IS NOT NULL))  " & vbCrLf _
    & "as itb on itb.U_ItemCode =itm.ItemCode  " & vbCrLf _
    & "left join (SELECT DISTINCT CARDCODE,ISNULL(U_ActTino,'') TAXID11,ISNULL(U_ActCstno,'') TAXID1 FROM  OCRD  ) as crd on crd.CardCode = a.CardCode " & vbCrLf _
    & "left join CRD1 as r on r.CardCode = a.CardCode /*and a.paytocode = r.address*/ AND R.AdresType='B'  " & vbCrLf _
    & "Left Outer Join OCST ST ON R.State=ST.Code AND R.Country=ST.Country AND ST.Country='IN'  " & vbCrLf _
    & "left OUTER join OCRD as l on l.CardCode = a.CardCode  " & vbCrLf _
   & "left OUTER join (SELECT DISTINCT CONVERT(NVARCHAR(MAX),U_ItemName) U_ItemName ,CONVERT(NVARCHAR(MAX),ISNULL(U_Remarks,'')) U_Remarks,U_Subbrand FROM [@INS_PLM1] WHERE u_lock = 'N') P on P.U_ItemName=B.U_CatalogName AND L.U_Subbrand = P.U_Subbrand" & vbCrLf _
    & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry,  " & vbCrLf _
    & "sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from " & TABLE1 & " where DiscPrcnt=100 group by DocEntry) G  " & vbCrLf _
    & "on G.DocEntry=B.DocEntry  " & vbCrLf _
    & "left OUTER join (Select ItemCode,sum(Price) [FORWARDING CHARGES],DocEntry from " & TABLE1 & " WHERE ItemCode='FrEightCharges' group by ItemCode,DocEntry) S ON S.DocEntry=B.DocEntry  " & vbCrLf _
    & "where a.docnum in (" & No.Text & ") and a.PIndicator in ('" & Year.Text & "') and b.Treetype <> 'I'  and b.ItemCode<>'FrEightCharges' order by b.LineNum"


            rccmtotqty = 0
            rccmtotamt = 0
            PAG = 0

            Dim CMDp As New SqlCommand(rccsql, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim RCCDRP As SqlDataReader
            RCCDRP = CMDp.ExecuteReader
            RCCDRP.Read()

            lin = 0
            FileOpen(1, " " + dbreportpath + "RCClorry.txt", OpenMode.Output, OpenAccess.Write)
            ''FileOpen(1, Trim(dbreportpath) & "RCClorry.txt", OpenMode.Output, OpenAccess.Write)
            'FileOpen(1, "e:\Test.txt", OpenMode.Output, OpenAccess.Write)
            PrintLine(1, TAB(33), Chr(27) + Chr(45) + Chr(1) + Chr(14) + Chr(27) + Chr(69) + "COPY INVOICE" + Chr(27) + Chr(70) + Chr(18) + Chr(27) + Chr(45) + Chr(0) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(0), "FROM,", TAB(51), "TO,")
            lin = lin + 1
            PrintLine(1, TAB(0), Chr(27) + Chr(69) + "Ramco Clothing Company" + Chr(27) + Chr(70) + Chr(18), TAB(56), "M/s.", SPC(1), Chr(27) + Chr(69) + RCCDRP.Item("CardFName").ToString + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(0), "(A UNIT OF ENES TEXTILE MILLS),", TAB(51), RCCDRP.Item("BUILDING").ToString)
            lin = lin + 1
            PrintLine(1, TAB(0), "33/6,BHAVANI MAIN ROAD,", TAB(51), RCCDRP.Item("BLOCK").ToString)
            lin = lin + 1
            PrintLine(1, TAB(0), "R.N.PUDUR POST,", TAB(51), RCCDRP.Item("STREET").ToString)
            lin = lin + 1
            PrintLine(1, TAB(0), "ERODE-638 005.", TAB(51), RCCDRP.Item("CITY").ToString)
            lin = lin + 1
            PrintLine(1, TAB(0), "PH:0424-2534147", TAB(51), RCCDRP.Item("county").ToString, SPC(1), "Dt.", SPC(1), "(", RCCDRP.Item("state1").ToString, SPC(1), ")")
            lin = lin + 1
            PrintLine(1, TAB(0), "TIN NO.: 33652323660 (H.0. TIRUPUR)", TAB(51), "TIN NO.:", SPC(1), RCCDRP.Item("TINNO").ToString)
            lin = lin + 1
            PrintLine(1, TAB(0), "CST NO.: 334666 DATE:05.09.03", TAB(51), "CST NO.:", SPC(1), RCCDRP.Item("CSTNO").ToString)
            lin = lin + 1
            PrintLine(1, TAB(51), "Mob NO.:", SPC(1), RCCDRP.Item("Cellular").ToString)
            lin = lin + 1
            PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(33), Chr(27) + Chr(69) + "TRANSPORT COPY" + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(0), "AREA CODE :", SPC(1), (RCCDRP("u_arcode").ToString), TAB(32), "BY :", SPC(1), (RCCDRP("u_orderby").ToString), TAB(60), "DATE:", SPC(1), Microsoft.VisualBasic.FormatDateTime(RCCDRP("docdate"), DateFormat.ShortDate))
            lin = lin + 1
            PrintLine(1, TAB(0), "TRANSPORT :", SPC(1), Chr(27) + Chr(69) + (RCCDRP("u_transport").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(37), "TO :", SPC(1), Chr(27) + Chr(69) + (RCCDRP("u_dsnation").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(70), "INV No:", SPC(1), (RCCDRP("docnum").ToString))
            lin = lin + 1
            PrintLine(1, TAB(0), "DOCOUMENT THROUGH :", SPC(1), (RCCDRP("u_docthrough")), TAB(60), "BUNDLE:", SPC(1), Chr(27) + Chr(69) + (RCCDRP("U_Noofbun").ToString) + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(0), Chr(27) + Chr(69) + "SIZE         PARTICULARS                 QTY     MTRS     RATE       AMOUNT" + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(1), (RCCDRP("u_size").ToString), TAB(10), (RCCDRP("item").ToString), TAB(45 - Len(Microsoft.VisualBasic.Format(RCCDRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(RCCDRP("U_NoofPiece"), "#######0"), TAB(55 - Len(Microsoft.VisualBasic.Format(RCCDRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("box"), "#######0.00"), TAB(65 - Len(Microsoft.VisualBasic.Format(RCCDRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("PriceBefDi"), "#######0.00"), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCDRP("BEFORE DISC"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("BEFORE DISC"), "#######0.00"))
            rccmtotamt = rccmtotamt + RCCDRP("BEFORE DISC")
            rccmtotqty = rccmtotqty + RCCDRP("U_NoofPiece")
            rccmbox = rccmbox + RCCDRP("box")
            While RCCDRP.Read
                PrintLine(1, TAB(1), (RCCDRP("u_size").ToString), TAB(10), (RCCDRP("item").ToString), TAB(45 - Len(Microsoft.VisualBasic.Format(RCCDRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(RCCDRP("U_NoofPiece"), "#######0"), TAB(55 - Len(Microsoft.VisualBasic.Format(RCCDRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("box"), "#######0.00"), TAB(65 - Len(Microsoft.VisualBasic.Format(RCCDRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("PriceBefDi"), "#######0.00"), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCDRP("BEFORE DISC"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("BEFORE DISC"), "#######0.00"))
                lin = lin + 1
                rccmtotamt = rccmtotamt + RCCDRP("BEFORE DISC")
                rccmtotqty = rccmtotqty + RCCDRP("U_NoofPiece")
                rccmbox = rccmbox + RCCDRP("box")
                If lin > 48 Then
                    n = 59 - lin
                    For k = 1 To n
                        PrintLine(1, Space(5), "")
                        lin = lin + 1
                    Next k
                    PrintLine(1, TAB(66), "-------------")
                    lin = lin + 1
                    PrintLine(1, TAB(20), "Continue........", SPC(1), PAG + 1, TAB(45 - Len(Microsoft.VisualBasic.Format(rccmtotqty, "#######0"))), Microsoft.VisualBasic.Format(rccmtotqty, "#######0"), TAB(55 - Len(Microsoft.VisualBasic.Format(rccmbox, "#######0.00"))), Microsoft.VisualBasic.Format(rccmbox, "#######0.00"), TAB(77 - Len(Microsoft.VisualBasic.Format(rccmtotamt, "#######0.00"))), Microsoft.VisualBasic.Format(rccmtotamt, "#######0.00"))
                    'PrintLine(1, TAB( Space(5), "")
                    lin = lin + 1

                    n = 72 - lin
                    For k = 1 To n
                        PrintLine(1, Space(5), "")
                    Next k
                    lin = 0
                    PAG = PAG + 1
                    PrintLine(1, TAB(33), Chr(27) + Chr(45) + Chr(1) + Chr(14) + Chr(27) + Chr(69) + "COPY INVOICE" + Chr(27) + Chr(70) + Chr(18) + Chr(27) + Chr(45) + Chr(0) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "FROM,", TAB(51), "TO,")
                    lin = lin + 1
                    PrintLine(1, TAB(0), Chr(27) + Chr(69) + "Ramco Clothing Company" + Chr(27) + Chr(70) + Chr(18), TAB(56), "M/s.", SPC(1), Chr(27) + Chr(69) + RCCDRP.Item("CardFName").ToString + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "(A UNIT OF ENES TEXTILE MILLS),", TAB(51), RCCDRP.Item("BUILDING").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(0), "33/6,BHAVANI MAIN ROAD,", TAB(51), RCCDRP.Item("BLOCK").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(0), "R.N.PUDUR POST,", TAB(51), RCCDRP.Item("STREET").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(0), "ERODE-638 005.", TAB(51), RCCDRP.Item("CITY").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(0), "PH:0424-2534147", TAB(51), RCCDRP.Item("county").ToString, SPC(1), "Dt.", SPC(1), "(", RCCDRP.Item("state1").ToString, SPC(1), ")")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "TIN NO.: 33652323660", TAB(51), "TIN NO.:", SPC(1), RCCDRP.Item("TINNO").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(0), "CST NO.: 334666 DATE:05.09.03", TAB(51), "CST NO.:", SPC(1), RCCDRP.Item("CSTNO").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(51), "Mob NO.:", SPC(1), RCCDRP.Item("Cellular").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
                    lin = lin + 1
                    PrintLine(1, TAB(33), Chr(27) + Chr(69) + "TRANSPORT COPY" + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "AREA CODE :", SPC(1), (RCCDRP("u_arcode").ToString), TAB(32), "BY :", SPC(1), (RCCDRP("u_orderby").ToString), TAB(60), "DATE:", SPC(1), Microsoft.VisualBasic.FormatDateTime(RCCDRP("docdate"), DateFormat.ShortDate))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "TRANSPORT :", SPC(1), Chr(27) + Chr(69) + (RCCDRP("u_transport").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(37), "TO :", SPC(1), Chr(27) + Chr(69) + (RCCDRP("u_dsnation").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(70), "INV No:", SPC(1), (RCCDRP("docnum").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "DOCOUMENT THROUGH :", SPC(1), (RCCDRP("u_docthrough")), TAB(60), "BUNDLE:", SPC(1), Chr(27) + Chr(69) + (RCCDRP("U_Noofbun").ToString) + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
                    lin = lin + 1
                    PrintLine(1, TAB(0), Chr(27) + Chr(69) + "SIZE         PARTICULARS                 QTY     MTRS     RATE       AMOUNT" + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
                    lin = lin + 1
                End If
            End While
            RCCDRP.Close()
            con.Close()
            con.Close()


            n = 48 - lin
            For k = 1 To n
                PrintLine(1, Space(5), "")
                lin = lin + 1
            Next k

            RCCTSQL = "SELECT convert(nvarchar(max),k.Building) Building,convert(nvarchar(max),k.Block) Block,convert(nvarchar(max),k.City) City,convert(nvarchar(max),k.STATE) STATE,convert(nvarchar(max),k.Street) Street,convert(nvarchar(max),k.ZipCode) ZipCode,convert(nvarchar(max),k.country) country,convert(nvarchar(max),k.county) county,  QryGroup8, ('M/s. ' + Cardfname) Cardfname,isnull(Esugam,'') Esugam,discprcnt,SUM(BeforeDisc) BeforeDisc,DiscAmt,MAX(SchemeDiscPercent)SchemeDiscPercent,[FORWARDING CHARGES] AS FORWARDINGCHARGES,DiscSum,sum(SchemeDiscAmt) SchemeDiscAmt ," & vbCrLf _
    & "(((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES] ) ttl1,VatSum,RoundDif, (((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES] + VatSum + RoundDif ) GRANDTOTAL" & vbCrLf _
    & ",taxcode,CASE WHEN TransCat = 'Form C' THEN '[Against C Form]' ELSE ' ' END  CFROM,CASE WHEN TransCat = 'Form H' THEN 'TAX EXEMPTED AGAINST FORM H' ELSE ' ' END  HFROM,sum(U_NoofPiece)U_NoofPiece,SUM(Quantity) QTY,SUM(box)  BOX" & vbCrLf _
    & "FROM (SELECT crd.QryGroup8,Cardfname, (b.Quantity*b.PriceBefDi) BeforeDisc,isnull(G.[Disc Amt],0) DiscAmt ,ISNULL(S.[FORWARDING CHARGES],isnull(fr.LineTotal,0)) [FORWARDING CHARGES]," & vbCrLf _
    & "case when c.State = 'KA' Then 'e-sugam no.' + a.U_ESugam when c.State in ('AP','TS') Then 'e-wayBill no.' + a.U_ESugam  when c.State in ('KL') Then 'e-Token No.' + a.U_ESugam  else '' end Esugam," & vbCrLf _
    & "sh.Building,isnull(sh.Block,'') Block,sh.City,CASE WHEN sh.State='OS' THEN sh.StreetNo WHEN crd.QryGroup5='Y' THEN sh.StreetNo ELSE ST.Name END AS STATE,sh.State State1,sh.ZipCode,isnull(sh.Street,'')Street,sh.country,sh.county," & vbCrLf _
    & "a.discprcnt,a.DiscSum,A.VatSum,A.RoundDif,B.TaxCode,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt',C.TransCat,b.U_NoofPiece,B.Quantity,(b.Volume) Box,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent'" & vbCrLf _
    & "FROM " & TABLE & " A JOIN " & TABLE1 & " B ON B.DocEntry = A.DocEntry" & vbCrLf _
    & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry,sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from  " & TABLE1 & "  where DiscPrcnt=100 group by DocEntry) G on G.DocEntry=B.DocEntry" & vbCrLf _
    & "left OUTER join (Select ItemCode,sum(Price) [FORWARDING CHARGES],DocEntry from " & TABLE1 & " WHERE ItemCode='FrEightCharges' group by  ItemCode,DocEntry) S ON S.DocEntry=B.DocEntry  " & vbCrLf _
    & "left join OITM as itm on itm.ItemCode = b.ItemCode" & vbCrLf _
    & "left join  OCRD crd on crd.CardCode = a.CardCode" & vbCrLf _
    & "left join crd1 sh on sh.cardcode = a.cardcode AND sh.AdresType='S'" & vbCrLf _
     & "Left Outer Join OCST ST ON sh.State=ST.Code AND sh.Country=ST.Country AND ST.Country='IN'" & vbCrLf _
    & "Left join " & TABLE12 & " as c on c.DocEntry = b.DocEntry" & vbCrLf _
    & "Left join " & TABLE3 & " as fr on fr.DocEntry = b.DocEntry" & vbCrLf _
    & "WHERE A.docnum = (" & No.Text & ") and   a.PIndicator in ('" & Year.Text & "') and b.TreeType <> 'I' and b.ItemCode<>'FrEightCharges'  ) k GROUP BY   QryGroup8,Cardfname,convert(nvarchar(max),k.Building) ,convert(nvarchar(max),k.Block) ,convert(nvarchar(max),k.City) ,convert(nvarchar(max),k.STATE) ,convert(nvarchar(max),k.Street) ,convert(nvarchar(max),k.ZipCode) ,convert(nvarchar(max),k.country) ,convert(nvarchar(max),k.county) , DiscAmt,[FORWARDING CHARGES],DiscSum,VatSum,RoundDif,taxcode,TransCat,Esugam,discprcnt"




            Dim CMD As New SqlCommand(RCCTSQL, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim RCCRP As SqlDataReader
            RCCRP = CMD.ExecuteReader
            RCCRP.Read()




            If RCCRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RCCRP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), "Delivery Address : ", TAB(5), Space(5), "")
            ElseIf RCCRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RCCRP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), "Delivery Address : ", TAB(66), "-------------")
            ElseIf RCCRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RCCRP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(66), "-------------")
            Else
                PrintLine(1, Space(5), "")
            End If
            ' If Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RCCRP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(66), "-------------")
            lin = lin + 1



            If RCCRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RCCRP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), RCCRP.Item("Cardfname").ToString, TAB(5), Space(5), "")
            ElseIf RCCRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RCCRP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), RCCRP.Item("Cardfname").ToString, TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00"))
            ElseIf RCCRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RCCRP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00"))
            Else
                PrintLine(1, Space(5), "")
            End If

            '   If Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RCCRP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00"))
            lin = lin + 1


            If RCCRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RCCRP.Item("Building").ToString, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00"))
            ElseIf RCCRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00") = "0.00" Then
                PrintLine(1, TAB(1), RCCRP.Item("Building").ToString, Space(5), "")
            ElseIf RCCRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00"))
            Else
                PrintLine(1, Space(5), "")
            End If



            'If Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1


            If RCCRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RCCRP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RCCRP.Item("BLOCK").ToString, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((RCCRP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("SchemeDiscAmt"), "#######0.00"))
            ElseIf RCCRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RCCRP("SchemeDiscPercent"), "#######0.00") = "0.00" Then
                PrintLine(1, TAB(1), RCCRP.Item("BLOCK").ToString, Space(5), "")
            ElseIf RCCRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RCCRP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((RCCRP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("SchemeDiscAmt"), "#######0.00"))
            Else
                PrintLine(1, Space(5), "")
            End If


            ' If Microsoft.VisualBasic.Format(RCCRP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((RCCRP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("SchemeDiscAmt"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1


            If RCCRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RCCRP.Item("STREET").ToString, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00"))
            ElseIf RCCRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RCCRP.Item("STREET").ToString, Space(5), "")
            ElseIf RCCRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00"))
            Else
                PrintLine(1, Space(5), "")
            End If


            ' If Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1


            If RCCRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RCCRP("DiscSum"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RCCRP.Item("CITY").ToString, SPC(1), "-", SPC(1), RCCRP.Item("ZIPCODE").ToString, TAB(40), "Less Discount :  ", SPC(1), Microsoft.VisualBasic.Format((RCCRP("discprcnt")), "#######0"), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("DiscSum"), "#######0.00"))
            ElseIf RCCRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RCCRP("DiscSum"), "#######0.00") = "0.00" Then
                PrintLine(1, TAB(1), RCCRP.Item("CITY").ToString, Space(5), "")
            ElseIf RCCRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RCCRP("DiscSum"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(40), "Less Discount :  ", SPC(1), Microsoft.VisualBasic.Format((RCCRP("discprcnt")), "#######0"), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("DiscSum"), "#######0.00"))
            Else
                PrintLine(1, Space(5), "")
            End If


            '  If Microsoft.VisualBasic.Format(RCCRP("DiscSum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount :  ", SPC(1), Microsoft.VisualBasic.Format((RCCRP("discprcnt")), "#######0"), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("DiscSum"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1







            'If Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RCCRP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(66), "-------------")
            'lin = lin + 1
            'If Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RCCRP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00"))
            'lin = lin + 1
            'If Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00")) Else PrintLine(1, Space(5), "")
            'lin = lin + 1
            'If Microsoft.VisualBasic.Format(RCCRP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((RCCRP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("SchemeDiscAmt"), "#######0.00")) Else PrintLine(1, Space(5), "")
            'lin = lin + 1
            'If Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00")) Else PrintLine(1, Space(5), "")
            'lin = lin + 1
            'If Microsoft.VisualBasic.Format(RCCRP("DiscSum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount :  ", SPC(1), Microsoft.VisualBasic.Format((RCCRP("discprcnt")), "#######0"), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("DiscSum"), "#######0.00")) Else PrintLine(1, Space(5), "")
            'lin = lin + 1
            PrintLine(1, TAB(0), RCCRP("Esugam"), SPC(1), TAB(66), "-------------")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RCCRP("ttl1"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("ttl1"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("ttl1"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RCCRP("vatsum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(0), (RCCRP("HFROM")), TAB(30), "Add : ", TAB(40), (RCCRP("CFROM")), SPC(1), (RCCRP("taxcode")), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("vatsum"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("vatsum"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RCCRP("RoundDif"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Round Off :  ", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("RoundDif"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("RoundDif"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(40), "----------------------------------------")
            lin = lin + 1
            PrintLine(1, TAB(23), Chr(27) + Chr(69) + "Grand Total :", SPC(1), TAB(48 - Len(Microsoft.VisualBasic.Format(RCCRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(RCCRP("U_NoofPiece"), "#######0"), TAB(58 - Len(Microsoft.VisualBasic.Format(RCCRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("box"), "#######0.00"), TAB(79 - Len(Microsoft.VisualBasic.Format(RCCRP("GRANDTOTAL"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("GRANDTOTAL"), "#######0.00") + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(40), "----------------------------------------")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(2), "", SPC(1), RupeesToWord(RCCRP("GRANDTOTAL")), SPC(1), "")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RCCRP("vatsum"), "#######0.00") = "0.00" Then PrintLine(1, TAB(2), "") Else PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(66), Chr(27) + Chr(69) + "For Ramco Clothing Company" + Chr(27) + Chr(70) + Chr(18))
            n = 72 - lin
            For k = 1 To n
                '' PrintLine(1, Space(5), "")
            Next k

            lin = 0
            mbox = 0
            mmtr = 0
            mtotqty = 0
            mtotamt = 0



            FileClose(1)
            RCCRP.Close()
            con.Close()
            con.Close()

            ''  Shell("print /d:LPT" & lpt.Text & " " + dbreportpath + "RCClorry.txt", vbNormalFocus)
            ''Shell("command.com /c TYPE " & " dbreportpath +"TestFile.txt>PRN", AppWinStyle.Hide)
            ''   Shell("print /d:LPT" & lpt.Text & " dbreportpath +"RRlorry.txt", vbNormalFocus)
            'Shell("print /d:LPT" & lpt.Text & " " + dbreportpath + "\rcclorry.txt", vbNormalFocus)
            Dim printer As String = mlsprinter   'laserprinter name
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "rcclorry.txt"
            Dim success As Boolean = PrintTextFile(filePathname)


        ElseIf cmp.Text = "ACC" Then
            'Call MAIN()


            PSQL = "SELECT  'ORIGINAL' as grpcopy, ITM.U_BrandType,A.DocNum ,A.DocEntry ,A.CardCode ,A.CardName ,A.DocDate ,A.DocDueDate,B.ItemCode ," & vbCrLf _
    & "CASE when l.QryGroup3 ='Y' then convert(nvarchar(20),(itm.U_Scode +'-'+ itm.u_subgrp2)) else convert(nvarchar(20),B.Dscription) end Dscription,a.VatSumSy," & vbCrLf _
    & "CASE when l.QryGroup3 ='Y' then (itm.U_Scode +'-'+ itm.u_subgrp2) else itm.u_subgrp2 end BDscription," & vbCrLf _
    & "(B.Quantity) QUANTITY ,B.Price ,case when b.AssblValue>=1000 then " & vbCrLf _
    & "(b.Quantity)*(b.AssblValue*60/100) else  (b.Quantity)*b.AssblValue end  " & vbCrLf _
    & "MRPVALUE,case when b.AssblValue>=1000 then (b.Quantity)*b.AssblValue else  " & vbCrLf _
    & "(b.Quantity)*b.AssblValue end  mrpvalue1," & vbCrLf _
    & "case when b.AssblValue>=1000 then (b.Quantity)*(b.AssblValue*60/100) else  0 end  mrpvalue2," & vbCrLf _
    & "case when b.AssblValue<1000 then (b.Quantity)*b.AssblValue else 0 end  EXVALUE, " & vbCrLf _
    & "b.PriceBefDi,b.Quantity*b.PriceBefDi [Before Disc],b.LineTotal,tx.TransCat," & vbCrLf _
    & "(B.LineTotal) LINETOTAL ,A.DiscPrcnt ,A.DiscSum ,A.RoundDif ,A.DOCTOTAL ,isnull(s.[FORWARDING CHARGES],0) [FORWARDING CHARGES]," & vbCrLf _
    & "CONVERT(NVARCHAR(100),isnull(C.Building,'')) BBUILDING,upper(C.CardFName) BCUSNAME ," & vbCrLf _
    & "isnull(C.BLOCK,'') BBLOCK,isnull(C.Street,'') BSTRRET,isnull(C.ZipCode,'') BZIPCODE ,isnull(C.State,'') BSTATE,isnull(C.County,'')  BCOUNTY,isnull(C.Country,'')  BCOUNTRY, C.QryGroup1 BQRYGROUP1," & vbCrLf _
    & "C.PANNO BPANNO,ISNULL(C.CSTNO,'') BCSTNO,ISNULL(C.TINNO,'') CTINNO,D.PANNO SPANNO,D.CSTNO SCSTNO,D.TINNO STINNO,C.CITY," & vbCrLf _
    & "CONVERT(NVARCHAR(100),D.Building) SBUILDING,D.CardfName SCUSNAME ,D.BLOCK SBLOCK," & vbCrLf _
    & "D.Street SSTREET,D.ZipCode SZIPCODE,D.State SSTATE,D.Country SCOUNTRY,D.QryGroup1 SQRYGROUP1,D.CITY," & vbCrLf _
    & "upper(AA.CompnyName) compnyname ,AA.CompnyAddr ,B.TaxCode  ,A.NumAtCard CUSREF,E.U_Style ,E.U_Size, Replace(REPLACE(a.Comments,'Based On Sales Orders ','SO : '),'Based On Deliveries','Del : ') Comments ," & vbCrLf _
    & "BB.TaxIdNum5 CSTNO,BB.TaxIdNum6 TINNO,bb.EccNo,bb.CERegNo,bb.CERange,bb.CEDivision,bb.CeComRate,bb.MenuCode,bb.Jurisd," & vbCrLf _
     & "C.StreetNo bSTREETNO,d.StreetNo sstreetno,AA.revoffice cpanno," & vbCrLf _
    & "b.BaseEntry ,b.BaseRef ,ISNULL(Q1.DocNum,0) sorderno,q1.NumAtCard Refno,ISNULL(b.U_baseentry,0) u_BASEENTRY," & vbCrLf _
    & "b.AssblValue mrp,case when cn.TaxSum>0 then cn.TaxRate else 0 end taxrate,  cn.taxsum,pr.Indicator fperiod," & vbCrLf _
    & "case when C.QryGroup1='Y'  then 'INVOICE' else ' INVOICE' end as heading," & vbCrLf _
    & "case  when ISNULL(b.U_baseentry,0)>'0' THEN 'WITH SALES ORDER' else 'WITHOUT SALES ORDER' end as heading2,tt.mpvalue," & vbCrLf _
    & "b.DiscPrcnt [Item Disc Prct]," & vbCrLf _
    & "CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent'" & vbCrLf _
    & ",CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt'," & vbCrLf _
    & "CASE WHEN tx.TransCat = 'FORM H' THEN 'Taxable' WHEN A.VatSum = '0' THEN 'Non  Taxable' ELSE 'Taxable' End AS TAXABLE, " & vbCrLf _
    & "A.DOCENTRY, b.linenum, A.address, A.address2, A.cardname, A.U_Noofbun, " & vbCrLf _
    & "case when D.State = 'KA' Then 'e-sugam no.' + a.U_ESugam when d.State in ('AP','TS') Then 'e-wayBill no.' + a.U_ESugam  when d.State in ('KL') Then 'e-Token No.' + a.U_ESugam  else '' end Esugam," & vbCrLf _
    & "A.u_ESUGAM, A.docnum, A.docdate, A.u_orderby, A.u_arcode, A.u_transport, A.u_docthrough, A.u_lrno, " & vbCrLf _
    & "A.u_lrdat,A.u_lrwight,a.u_dsnation,a.U_lrval AS topay," & vbCrLf _
    & "b.u_catalogname,b.BaseREf,itm.SalPackUn,a.U_Dsnation,a.NumAtCard ,Ex.ExcRmvTime,ex.docnum EXDOCNUM" & vbCrLf _
    & "FROM  " & TABLE & "   A  WITH (NOLOCK)" & vbCrLf _
    & "INNER JOIN  " & TABLE1 & "  B WITH (NOLOCK) ON A.DOCENTRY=B.DOCENTRY" & vbCrLf _
    & "left join OITM  as itm  WITH (NOLOCK) on itm.ItemCode = b.ItemCode" & vbCrLf _
    & "left join OITB  as itb WITH (NOLOCK) on itb.ItmsGrpCod = itm.ItmsGrpCod" & vbCrLf _
    & "Left join OOEI  as Ex WITH (NOLOCK) on Ex.DocEntry =a.U_Exbaseentry " & vbCrLf _
    & "left join " & TABLE4 & "  cn WITH (NOLOCK) on cn.DocEntry=b.DocEntry and cn.LineNum=b.LineNum and cn.staType=7" & vbCrLf _
    & "Left join " & TABLE12 & "  as Tx WITH (NOLOCK) on TX.DocEntry = b.DocEntry	" & vbCrLf _
    & "left OUTER join OCRD  as l WITH (NOLOCK)on l.CardCode = a.CardCode " & vbCrLf _
    & "left OUTER join (SELECT U_Scode U_remarks,ItemCode,ItemName FROM OITM WITH (NOLOCK) WHERE U_Scode IS NOT NULL) P on P.ItemName=B.U_CatalogName " & vbCrLf _
    & "left join (  select K.docentry,SUM(k.mrpvalue) mpvalue from " & vbCrLf _
           & "(select b.docentry,CASE when b.AssblValue>=1000 then  SUM(b.quantity)*b.assblvalue else 0 end  as mrpvalue from " & TABLE1 & "  b WITH (NOLOCK) " & vbCrLf _
            & "group by b.DocEntry,b.AssblValue) k" & vbCrLf _
           & "group by k.DocEntry" & vbCrLf _
           & "having SUM(k.mrpvalue)>0 ) tt on tt.DocEntry=b.DocEntry" & vbCrLf _
    & "left join OFPR pr WITH (NOLOCK) on pr.AbsEntry=a.FinncPriod" & vbCrLf _
    & "LEFT JOIN (select  A.CardCode ,a.cardfname,A.CardName ,A.qrygroup1,B.Building,B.County,B.Block ,C.CSTNO ,C.PANNO ,C.TINNO " & vbCrLf _
    & ",B.Street ,B.City ,B.ZipCode ,ST.Name State ,B.Country ,B.StreetNo from OCRD a WITH (NOLOCK)" & vbCrLf _
    & "left join (select CardCode,Building ,Block,Street ,City ,ZipCode ,State ,County  ,Country ,StreetNo  From CRD1 a where  a.AdresType ='B') " & vbCrLf _
    & "b on a.CardCode  =b.CardCode " & vbCrLf _
    & "lEFT JOIN OCST ST WITH (NOLOCK) ON ST.Code = B.STATE and st.Country = 'IN'" & vbCrLf _
    & "inner join (SELECT  CardCode,TAXID0 AS PANNO,TaxId1 CSTNO,TaxId11 TINNO FROM CRD7  WITH (NOLOCK)" & vbCrLf _
    & "WHERE Address IS NULL OR LEN(RTRIM(LTRIM(Address)))=0)C ON C.CardCode =A.CardCode  ) C ON C.CardCode =A.CardCode " & vbCrLf _
    & "   Left Join" & vbCrLf _
    & "(" & vbCrLf _
    & "select  a.cardfname,A.CardCode ,A.CardName,A.qrygroup1 ,B.Building,B.County  ,B.Block ,C.CSTNO ,C.PANNO ,C.TINNO ,B.StreetNo ,B.Street ,B.City ,B.ZipCode ,B.State ,B.Country  from OCRD a WITH (NOLOCK)" & vbCrLf _
    & "left join (select CardCode,Building ,Block,Street ,City ,ZipCode ,State ,County,Country,StreetNo      from CRD1 a WITH (NOLOCK) where  a.AdresType ='S') b on a.CardCode  =b.CardCode " & vbCrLf _
    & "inner join (SELECT  CardCode,TAXID0 AS PANNO,TaxId1 CSTNO,TaxId11 TINNO FROM CRD7  WITH (NOLOCK)" & vbCrLf _
    & "WHERE Address IS NULL OR LEN(RTRIM(LTRIM(Address)))=0)C ON C.CardCode =A.CardCode )D ON D .CardCode =A.CardCode " & vbCrLf _
    & "LEFT JOIN OITM E WITH (NOLOCK) ON E.ItemCode =B.ItemCode " & vbCrLf _
    & "LEFT JOIN " & vbCrLf _
    & "(" & vbCrLf _
    & "SELECT B.BaseEntry,B.BaseRef ,A.DocNum,A.DocEntry,a.NumAtCard ,B.ItemCode ,b.linenum,b.U_BaseLine  " & vbCrLf _
     & "FROM ODLN  A WITH (NOLOCK) INNER JOIN DLN1  B WITH (NOLOCK) ON A.DOCENTRY=B.DOCENTRY" & vbCrLf _
    & ")q ON q.DocEntry =b.BaseEntry and q.DocNum =b.BaseRef AND q.LineNum =b.BaseLine " & vbCrLf _
    & "LEFT JOIN " & vbCrLf _
    & "(" & vbCrLf _
    & "SELECT B.BaseEntry,B.BaseRef ,A.DocNum,A.DocEntry,a.NumAtCard ,B.ItemCode ,b.LineNum " & vbCrLf _
     & "FROM ORDR   A WITH (NOLOCK) INNER JOIN RDR1   B WITH (NOLOCK) ON A.DOCENTRY=B.DOCENTRY" & vbCrLf _
    & ")q1 ON q1.DocEntry =b.U_BaseEntry  AND q1.LineNum =q.U_Baseline " & vbCrLf _
    & "left OUTER join " & vbCrLf _
    & "(Select ItemCode,LineTotal [FORWARDING CHARGES],DocEntry from  " & TABLE1 & "   WHERE ItemCode='FrightCharges') " & vbCrLf _
    & "S ON S.DocEntry=B.DocEntry" & vbCrLf _
    & ",OADM AA WITH (NOLOCK),ADM1 BB WITH (NOLOCK)" & vbCrLf _
    & "WHERE A.docnum in (" & No.Text & ") and a.PIndicator in ('" & Year.Text & "') and b.ItemCode <>'FrightCharges'" & vbCrLf _
    & "GROUP BY " & vbCrLf _
    & "tx.TransCat,a.VatSum,b.LineNum,a.Address,a.Address2,a.U_Noofbun,a.U_ESugam,a.U_OrderBy,a.U_Arcode," & vbCrLf _
    & "a.U_Transport,a.U_DocThrough,a.U_LRNO,a.U_Lrdat,a.U_Lrwight,a.U_Dsnation,a.U_Lrval,b.U_CatalogName,C.COUNTY," & vbCrLf _
    & "b.Quantity,b.LineTotal,c.CardFName,d.CardfName," & vbCrLf _
    & "A.DocNum ,A.DocEntry ,A.CardCode ,A.CardName ,A.DocDate ,A.DocDueDate,B.ItemCode ,B.Dscription ," & vbCrLf _
    & "B.Price  ,A.DiscPrcnt ,A.DiscSum ,A.RoundDif ,A.DOCTOTAL ,itm.U_Scode,l.QryGroup3," & vbCrLf _
    & "CONVERT(NVARCHAR(100),isnull(C.Building,'')) ,C.CardName   ," & vbCrLf _
    & "isnull(C.BLOCK,'') ,isnull(C.Street,'') ,isnull(C.ZipCode,'') ,isnull(C.State,'') ,isnull(C.Country,'') , C.QryGroup1 ,a.VatSumSy," & vbCrLf _
    & "CONVERT(NVARCHAR(100),D.Building) ,D.CardName   ,D.BLOCK ," & vbCrLf _
    & "D.Street ,D.ZipCode ,D.State ,D.Country ,D.QryGroup1 ,AA.CompnyName ,AA.CompnyAddr ,itm.u_subgrp2," & vbCrLf _
    & "C.PANNO ,C.CSTNO ,C.TINNO ,D.PANNO ,D.CSTNO ,D.TINNO ,B.TaxCode ,itm.SalPackUn," & vbCrLf _
    & "A.NumAtCard ,E.U_Style ,E.U_Size ,A.Comments ,BB.TaxIdNum5 ,BB.TaxIdNum6 ,s.[FORWARDING CHARGES],b.DiscPrcnt,b.PriceBefDi," & vbCrLf _
    & "bb.EccNo,bb.CERegNo,bb.CERange,bb.CEDivision,bb.CeComRate,bb.MenuCode,bb.Jurisd," & vbCrLf _
    & "C.City,D.CITY,C.StreetNo ,d.StreetNo ,b.BaseEntry ,b.BaseRef ,ISNULL(Q1.DocNum,0) ,q1.NumAtCard ,ISNULL(b.U_baseentry,0) ,b.AssblValue," & vbCrLf _
    & "case when cn.TaxSum>0 then cn.TaxRate else 0 end ,cn.taxsum,pr.Indicator,tt.mpvalue ,Ex.ExcRmvTime,ex.docnum,AA.revoffice,ITM.U_BrandType,tx.TransCat"


            mtotqty = 0
            mtotamt = 0
            PAG = 0

            Dim CMDp As New SqlCommand(PSQL, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim DRP As SqlDataReader
            DRP = CMDp.ExecuteReader
            DRP.Read()

            lin = 0

            FileOpen(1, " " + dbreportpath + "SSlorry.txt", OpenMode.Output, OpenAccess.Write)

            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(41), Chr(27) + Chr(69) + DRP.Item("BCUSNAME").ToString + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(41), (DRP("BBUILDING").ToString), (DRP("BBLOCK").ToString))
            lin = lin + 1
            PrintLine(1, TAB(41), (DRP("BSTRRET").ToString))
            lin = lin + 1
            PrintLine(1, TAB(41), (Trim(DRP("CITY")).ToString), SPC(1), "-", SPC(1), (Trim(DRP("BZIPCODE")).ToString))
            lin = lin + 1
            PrintLine(1, TAB(41), "TIN No.", SPC(1), (Trim(DRP("CTINNO")).ToString))
            lin = lin + 1
            PrintLine(1, TAB(41), "CST No.", SPC(1), (Trim(DRP("BCSTNO")).ToString))
            lin = lin + 1
            PrintLine(1, TAB(41), (DRP("BSTRRET").ToString))
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(0), "Order No :", SPC(1), (DRP("baseref").ToString), TAB(30), "L.R.No.:", SPC(1), (DRP("u_lrno").ToString), TAB(54), Chr(27) + Chr(69) + "INV.No." + Chr(27) + Chr(70) + Chr(18), SPC(1), Chr(27) + Chr(69) + (DRP.Item("U_BrandType").ToString) + Chr(27) + Chr(70) + Chr(18), SPC(1), Chr(27) + Chr(69) + (DRP.Item("Docnum").ToString) + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(0), "Order By :", SPC(1), (DRP("u_orderby").ToString), TAB(30), "L.R.Date :", SPC(1), Microsoft.VisualBasic.Format(DRP("u_lrwight"), "#######0.000"))
            lin = lin + 1
            PrintLine(1, TAB(0), "Case no :", SPC(1), (DRP("baseref").ToString), TAB(30), "L.R.Weight.:", SPC(1), Microsoft.VisualBasic.Format(DRP("u_lrwight"), "#######0.000"), TAB(54), "INV.Date:", SPC(1), Microsoft.VisualBasic.FormatDateTime(DRP("docdate"), DateFormat.ShortDate))
            lin = lin + 1
            PrintLine(1, TAB(0), "Area Code:", SPC(1), (DRP("u_arcode").ToString), TAB(30), "Freight.:", SPC(1), Microsoft.VisualBasic.Format(DRP("u_lrwight"), "#######0.000"))
            lin = lin + 1
            PrintLine(1, TAB(54), "Del.Date:", SPC(1), Microsoft.VisualBasic.FormatDateTime(DRP("DocDueDate"), DateFormat.ShortDate))
            lin = lin + 1
            PrintLine(1, TAB(0), "Goods To :", SPC(1), (DRP("u_dsnation").ToString))
            lin = lin + 1
            PrintLine(1, TAB(0), "Transport :", SPC(1), (DRP("u_transport").ToString), TAB(54), "Excise.No.:", SPC(1), (DRP.Item("EXDOCNUM").ToString))
            lin = lin + 1
            PrintLine(1, TAB(0), "Doc Through:", SPC(1), (DRP("u_docthrough").ToString))
            lin = lin + 1
            PrintLine(1, TAB(0), TAB(23), SPC(1), DRP("Esugam"), TAB(54), "Time.Of.Removal:", SPC(1), (DRP.Item("ExcRmvTime").ToString))
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(0), (DRP("Dscription").ToString), TAB(23), (DRP("u_style").ToString), TAB(30), (DRP("u_size").ToString), TAB(35), Microsoft.VisualBasic.Format(DRP("quantity"), "#######0"), TAB(48 - Len(Microsoft.VisualBasic.Format(DRP("mrp"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("mrp"), "#######0.00"), TAB(58 - Len(Microsoft.VisualBasic.Format(DRP("mrpvalue1"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("mrpvalue1"), "#######0.00"), TAB(68 - Len(Microsoft.VisualBasic.Format(DRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("PriceBefDi"), "#######0.00"), TAB(78 - Len(Microsoft.VisualBasic.Format(DRP("BEFORE DISC"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("BEFORE DISC"), "#######0.00"))
            mtotamt = mtotamt + DRP("BEFORE DISC")
            mtotqty = mtotqty + DRP("quantity")
            mbox = mbox + DRP("mrpvalue1")
            While DRP.Read
                PrintLine(1, TAB(0), (DRP("Dscription").ToString), TAB(23), (DRP("u_style").ToString), TAB(30), (DRP("u_size").ToString), TAB(35), Microsoft.VisualBasic.Format(DRP("quantity"), "#######0"), TAB(48 - Len(Microsoft.VisualBasic.Format(DRP("mrp"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("mrp"), "#######0.00"), TAB(58 - Len(Microsoft.VisualBasic.Format(DRP("mrpvalue1"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("mrpvalue1"), "#######0.00"), TAB(68 - Len(Microsoft.VisualBasic.Format(DRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("PriceBefDi"), "#######0.00"), TAB(78 - Len(Microsoft.VisualBasic.Format(DRP("BEFORE DISC"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("BEFORE DISC"), "#######0.00"))
                lin = lin + 1
                mtotamt = mtotamt + DRP("BEFORE DISC")
                mtotqty = mtotqty + DRP("quantity")
                mbox = mbox + DRP("mrpvalue1")
                If lin > 44 Then
                    n = 61 - lin
                    For k = 1 To n
                        PrintLine(1, Space(5), "")
                        lin = lin + 1
                    Next k
                    PrintLine(1, TAB(5), Chr(27) + Chr(69) + "Continue........", SPC(1), PAG + 1, TAB(39 - Len(Microsoft.VisualBasic.Format(mtotqty, "#######0"))), Microsoft.VisualBasic.Format(mtotqty, "#######0"), TAB(55), Microsoft.VisualBasic.Format(mbox, "#####0"), TAB(79 - Len(Microsoft.VisualBasic.Format(mtotamt, "#######0.00"))), Microsoft.VisualBasic.Format(mtotamt, "#######0.00") + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    n = 71 - lin
                    For k = 1 To n
                        PrintLine(1, Space(5), "")
                        lin = lin + 1
                    Next k
                    PAG = PAG + 1
                    lin = 0




                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, TAB(41), Chr(27) + Chr(69) + DRP.Item("BCUSNAME").ToString + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, TAB(41), (DRP("BBUILDING").ToString), (DRP("BBLOCK").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(41), (DRP("BSTRRET").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(41), (Trim(DRP("CITY")).ToString), SPC(1), "-", SPC(1), (Trim(DRP("BZIPCODE")).ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(41), "TIN No.", SPC(1), (Trim(DRP("CTINNO")).ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(41), "CST No.", SPC(1), (Trim(DRP("BCSTNO")).ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(41), (DRP("BSTRRET").ToString))
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "Order No :", SPC(1), (DRP("baseref").ToString), TAB(30), "L.R.No.:", SPC(1), (DRP("u_lrno").ToString), TAB(54), Chr(27) + Chr(69) + "INV.No." + Chr(27) + Chr(70) + Chr(18), SPC(1), Chr(27) + Chr(69) + (DRP.Item("U_BrandType").ToString) + Chr(27) + Chr(70) + Chr(18), SPC(1), Chr(27) + Chr(69) + (DRP.Item("Docnum").ToString) + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "Order By :", SPC(1), (DRP("u_orderby").ToString), TAB(30), "L.R.Date :", SPC(1), Microsoft.VisualBasic.Format(DRP("u_lrwight"), "#######0.000"))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "Case no :", SPC(1), (DRP("baseref").ToString), TAB(30), "L.R.Weight.:", SPC(1), Microsoft.VisualBasic.Format(DRP("u_lrwight"), "#######0.000"), TAB(54), "INV.Date:", SPC(1), Microsoft.VisualBasic.FormatDateTime(DRP("docdate"), DateFormat.ShortDate))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "Area Code:", SPC(1), (DRP("u_arcode").ToString), TAB(30), "Freight.:", SPC(1), Microsoft.VisualBasic.Format(DRP("u_lrwight"), "#######0.000"))
                    lin = lin + 1
                    PrintLine(1, TAB(54), "Del.Date:", SPC(1), Microsoft.VisualBasic.FormatDateTime(DRP("DocDueDate"), DateFormat.ShortDate))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "Goods To :", SPC(1), (DRP("u_dsnation").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "Transport :", SPC(1), (DRP("u_transport").ToString), TAB(54), "Excise.No.:", SPC(1), (DRP.Item("EXDOCNUM").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(0), "Doc Through:", SPC(1), (DRP("u_docthrough").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(0), TAB(23), SPC(1), DRP("Esugam"), TAB(54), "Time.Of.Removal:", SPC(1), (DRP.Item("ExcRmvTime").ToString))
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")

                End If
            End While
            DRP.Close()
            con.Close()
            con.Close()

            n = 44 - lin
            For k = 1 To n
                PrintLine(1, Space(5), "")
                lin = lin + 1
            Next k


            TSQL = "SELECT convert(nvarchar(max),k.Building) Building,convert(nvarchar(max),k.Block) Block,convert(nvarchar(max),k.City) City,convert(nvarchar(max),k.STATE) STATE,convert(nvarchar(max),k.Street) Street,convert(nvarchar(max),k.ZipCode) ZipCode,convert(nvarchar(max),k.country) country,convert(nvarchar(max),k.county) county,  QryGroup10, ('M/s. ' + Cardfname) Cardfname, SUM(Quantity) QTY, SUM(BeforeDisc) BeforeDisc,SUM(mrpvalue1) mrpvalue,trdis + '%  = ' + convert(nvarchar(max),sum(cast(SchemeDiscAmt as numeric(19,2)))) TRadeDiscount," & vbCrLf _
    & "DiscAmt,DiscSum,MAX(SchemeDiscPercent)SchemeDiscPercent,sum(SchemeDiscAmt) SchemeDiscAmt , (((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum ) ttl1," & vbCrLf _
    & "(((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES]  + VatSum + RoundDif ) GRANDTOTAL," & vbCrLf _
    & "sum(EXVALUE) exvalue, stacode1 , Taxsum taxsum1, isnull(stacode2,0) stacode2 , isnull(Taxsum2,0) taxsum2 ,  ISNULL(transcat,'') Trascat, CASE WHEN TransCat = 'Form C' THEN '[Against C Form]' ELSE ' ' END  CFROM," & vbCrLf _
    & "CASE WHEN TransCat = 'Form H' THEN 'TAX EXEMPTED AGAINST FORM H' ELSE ' ' END  HFROM,   VatSum,    [FORWARDING CHARGES] AS FORWARDINGCHARGES ,RoundDif " & vbCrLf _
    & "FROM (SELECT  crd.QryGroup10,Cardfname,b.LineNum,(b.Quantity*b.PriceBefDi) BeforeDisc, case when b.AssblValue>=1000 then (b.Quantity)*b.AssblValue else (b.Quantity)*b.AssblValue end mrpvalue1," & vbCrLf _
    & "sh.Building,isnull(sh.Block,'') Block,sh.City,CASE WHEN sh.State='OS' THEN sh.StreetNo WHEN crd.QryGroup5='Y' THEN sh.StreetNo ELSE ST.Name END AS STATE,sh.State State1,sh.ZipCode,isnull(sh.Street,'') Street,sh.country,sh.county," & vbCrLf _
    & "isnull(G.[Disc Amt],0) DiscAmt ,ISNULL(S.[FORWARDING CHARGES],0) [FORWARDING CHARGES], case when isnull(convert(nvARCHAR(MAX),crd.U_Dis1),'') <> '' then 'Trade Dsicount   ' +" & vbCrLf _
    & "convert(nvarchar(max),(cast(crd.U_Dis1 as numeric(19,0))))   else '' end  trdis,a.DiscSum,A.VatSum,A.RoundDif," & vbCrLf _
    & "CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal" & vbCrLf _
    & "End 'SchemeDiscAmt',C.TransCat,  B.Quantity,(b.Quantity/ITM.SalPackUn) Box,   CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent'," & vbCrLf _
    & "case when b.AssblValue<1000 then (b.Quantity)*b.AssblValue else 0 end  EXVALUE, tx.stacode1,tx.Taxsum,tx1.stacode1 stacode2,tx1.Taxsum taxsum2" & vbCrLf _
    & "FROM  " & TABLE & "  A JOIN  " & TABLE1 & "  B ON B.DocEntry = A.DocEntry " & vbCrLf _
    & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry,sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from  " & TABLE1 & "  where DiscPrcnt=100 group by DocEntry) G on G.DocEntry=B.DocEntry " & vbCrLf _
    & "left OUTER join (Select SUM(LineTotal) [FORWARDING CHARGES],DocEntry from  " & TABLE3 & "   GROUP BY DocEntry) S ON S.DocEntry=B.DocEntry   " & vbCrLf _
    & "left join OCRD crd on crd.CardCode = a.CardCode " & vbCrLf _
    & "left join crd1 sh on sh.cardcode = a.cardcode AND sh.AdresType='S'" & vbCrLf _
    & "Left Outer Join OCST ST ON sh.State=ST.Code AND sh.Country=ST.Country AND ST.Country='IN'" & vbCrLf _
    & "left join OITM as itm on itm.ItemCode = b.ItemCode " & vbCrLf _
    & "Left join  " & TABLE12 & "  as c on c.DocEntry = b.DocEntry  " & vbCrLf _
    & "Left join  (select case when a.statype=7 then  convert(nvarchar(8),convert(numeric(4,0),TaxRate)) + '% of Assessable Value (60% on MRP)' " & vbCrLf _
    & "when staType = 4 and  b.Name like '%CST%' and isnull(d.TransCat,'') <> '' then 'Add : Against ""C"" Form ' + b.Name" & vbCrLf _
    & "when staType = 4 and  b.Name like '%CST%' and isnull(d.TransCat,'') = '' then 'Add : ' + b.Name" & vbCrLf _
    & "when staType = 1 and b.Name Like '%VAT%'then 'Add: ' + b.Name" & vbCrLf _
    & "else b.Name end stacode1, CASE when a.staType=7 then 1       when a.staType=1 then 2       when a.staType=4 then 3 end as ctyp," & vbCrLf _
    & "a.DocEntry ,b.Name stacode ,a.TaxRate ,sum(a.TaxSum) Taxsum,a.statype,CASE when a.staType=7 then tt.mpvalue else 0 end as mpvalue," & vbCrLf _
    & "CASE when a.staType=7 then tt.mpvalue1 else 0 end as mpvalue1  from  " & TABLE4 & "  a " & vbCrLf _
    & "LEft join  " & TABLE & "  dd on dd.DocEntry = a.DocEntry" & vbCrLf _
    & "left join OSTA b  on b.Code =a.StaCode " & vbCrLf _
    & "left join (  select k.TransCat,K.docentry,SUM(k.mrpvalue) mpvalue,sum(k.mrpvalue1) mpvalue1 from       (select c.TransCat,b.docentry,CASE when b.AssblValue>=1000 then  SUM(b.quantity)*(b.assblvalue*60/100) else 0 end  as mrpvalue," & vbCrLf _
    & "CASE when b.AssblValue>=1000 then  SUM(b.quantity)*b.assblvalue else 0 end  as mrpvalue1   from  " & TABLE1 & "  b Left join  " & TABLE12 & "  c on c.DocEntry = b.DocEntry" & vbCrLf _
    & "group by b.DocEntry,b.AssblValue,c.TransCat) k        group by k.DocEntry,k.TransCat        having SUM(k.mrpvalue)>0 ) tt on tt.DocEntry=a.DocEntry" & vbCrLf _
    & "left join  " & TABLE12 & "  d on d.DocEntry = a.docentry where staType = 7 and TaxRate = 2 group by a.DocEntry ,a.StaCode ,a.TaxRate ,b.Name,a.statype,tt.mpvalue,tt.mpvalue1 ,d.TransCat" & vbCrLf _
    & ") tx on tx.DocEntry =a.DocEntry" & vbCrLf _
    & "Left join  (" & vbCrLf _
    & "select case when a.statype=7 then  convert(nvarchar(8),convert(numeric(4,0),TaxRate)) + '% of Assessable Value (60% on MRP)'" & vbCrLf _
    & "when staType = 4 and  b.Name like '%CST%' and isnull(d.TransCat,'') <> '' then 'Add : Against ""C"" Form ' + b.Name " & vbCrLf _
    & "when staType = 4 and  b.Name like '%CST%' and isnull(d.TransCat,'') = '' then 'Add : ' + b.Name" & vbCrLf _
    & "when staType = 1 and b.Name Like '%VAT%'then 'Add: ' + b.Name" & vbCrLf _
    & "else b.Name end stacode1, CASE when a.staType=7 then 1       when a.staType=1 then 2       when a.staType=4 then 3 end as ctyp," & vbCrLf _
    & "a.DocEntry ,b.Name stacode ,a.TaxRate ,sum(a.TaxSum) Taxsum,a.statype,CASE when a.staType=7 then tt.mpvalue else 0 end as mpvalue," & vbCrLf _
    & "CASE when a.staType=7 then tt.mpvalue1 else 0 end as mpvalue1  from  " & TABLE4 & "  a " & vbCrLf _
    & "LEft join  " & TABLE1 & "  dd on dd.DocEntry = a.DocEntry " & vbCrLf _
    & "left join OSTA b  on b.Code =a.StaCode " & vbCrLf _
    & "left join (  select k.TransCat,K.docentry,SUM(k.mrpvalue) mpvalue,sum(k.mrpvalue1) mpvalue1 from       (select c.TransCat,b.docentry,CASE when b.AssblValue>=1000 then  SUM(b.quantity)*(b.assblvalue*60/100) else 0 end  as mrpvalue," & vbCrLf _
    & "CASE when b.AssblValue>=1000 then  SUM(b.quantity)*b.assblvalue else 0 end  as mrpvalue1   from  " & TABLE1 & "  b Left join  " & TABLE12 & "  c on c.DocEntry = b.DocEntry " & vbCrLf _
    & "group by b.DocEntry,b.AssblValue,c.TransCat) k        group by k.DocEntry,k.TransCat        having SUM(k.mrpvalue)>0 ) tt on tt.DocEntry=a.DocEntry " & vbCrLf _
    & "left join  " & TABLE12 & "  d on d.DocEntry = a.docentry where staType <> 7  group by a.DocEntry ,a.StaCode ,a.TaxRate ,b.Name,a.statype,tt.mpvalue,tt.mpvalue1 ,d.TransCat " & vbCrLf _
    & ") tx1 on tx1.DocEntry =a.DocEntry " & vbCrLf _
    & "WHERE A.docnum = (" & No.Text & ") and   a.PIndicator in ('" & Year.Text & "')  and b.Treetype <> 'I'  and b.ItemCode<>'FrightCharges' " & vbCrLf _
    & ") k GROUP BY  QryGroup10,Cardfname,convert(nvarchar(max),k.Building) ,convert(nvarchar(max),k.Block) ,convert(nvarchar(max),k.City) ,convert(nvarchar(max),k.STATE) ,convert(nvarchar(max),k.Street) ,convert(nvarchar(max),k.ZipCode) ,convert(nvarchar(max),k.country) ,convert(nvarchar(max),k.county),QryGroup10,trdis,DiscAmt,[FORWARDING CHARGES],DiscSum,VatSum,RoundDif,TransCat,stacode1 , Taxsum , stacode2 , Taxsum2  " & vbCrLf


            Dim CMD As New SqlCommand(TSQL, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim RP As SqlDataReader
            RP = CMD.ExecuteReader
            RP.Read()


            If RP.Item("QryGroup10").ToString = "Y" And Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), "Delivery Address : ", TAB(5), Space(5), "")
            ElseIf RP.Item("QryGroup10").ToString = "Y" And Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), "Delivery Address : ", TAB(66), "-------------")
            ElseIf RP.Item("QryGroup10").ToString = "N" And Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(66), "-------------")
            Else
                PrintLine(1, Space(5), "")
            End If
            ' If Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(66), "-------------")
            lin = lin + 1



            If RP.Item("QryGroup10").ToString = "Y" And Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), RP.Item("Cardfname").ToString, TAB(5), Space(5), "")
            ElseIf RP.Item("QryGroup10").ToString = "Y" And Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(1), RP.Item("Cardfname").ToString, TAB(77 - Len(Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00"))
            ElseIf RP.Item("QryGroup10").ToString = "N" And Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00"))
            Else
                PrintLine(1, Space(5), "")
            End If

            '   If Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00"))
            lin = lin + 1


            If RP.Item("QryGroup10").ToString = "Y" And Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RP.Item("Building").ToString, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00"))
            ElseIf RP.Item("QryGroup10").ToString = "Y" And Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00") = "0.00" Then
                PrintLine(1, TAB(1), RP.Item("Building").ToString, Space(5), "")
            ElseIf RP.Item("QryGroup10").ToString = "N" And Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00"))
            Else
                PrintLine(1, Space(5), "")
            End If



            'If Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1


            If RP.Item("QryGroup10").ToString = "Y" And Microsoft.VisualBasic.Format(RP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RP.Item("BLOCK").ToString, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((RP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00"))
            ElseIf RP.Item("QryGroup10").ToString = "Y" And Microsoft.VisualBasic.Format(RP("SchemeDiscPercent"), "#######0.00") = "0.00" Then
                PrintLine(1, TAB(1), RP.Item("BLOCK").ToString, Space(5), "")
            ElseIf RP.Item("QryGroup10").ToString = "N" And Microsoft.VisualBasic.Format(RP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((RP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00"))
            Else
                PrintLine(1, Space(5), "")
            End If


            ' If Microsoft.VisualBasic.Format(RP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((RP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1


            If RP.Item("QryGroup10").ToString = "Y" And Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RP.Item("STREET").ToString, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00"))
            ElseIf RP.Item("QryGroup10").ToString = "Y" And Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RP.Item("STREET").ToString, Space(5), "")
            ElseIf RP.Item("QryGroup10").ToString = "N" And Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00"))
            Else
                PrintLine(1, Space(5), "")
            End If


            ' If Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1


            If RP.Item("QryGroup10").ToString = "Y" And Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(1), RP.Item("CITY").ToString, SPC(1), "-", SPC(1), RP.Item("ZIPCODE").ToString, TAB(40), "Less Discount :  ", SPC(1), Microsoft.VisualBasic.Format((RP("discprcnt")), "#######0"), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00"))
            ElseIf RP.Item("QryGroup10").ToString = "Y" And Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00") = "0.00" Then
                PrintLine(1, TAB(1), RP.Item("CITY").ToString, SPC(1), "-", SPC(1), RP.Item("ZIPCODE").ToString, Space(5), "")
            ElseIf RP.Item("QryGroup10").ToString = "N" And Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00") <> "0.00" Then
                PrintLine(1, TAB(40), "Less Discount :  ", SPC(1), Microsoft.VisualBasic.Format((RP("discprcnt")), "#######0"), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00"))
            Else
                PrintLine(1, Space(5), "")
            End If


            '  If Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount :  ", SPC(1), Microsoft.VisualBasic.Format((RP("discprcnt")), "#######0"), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1





            If Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(55), "-----------------------")
            lin = lin + 1
            PrintLine(1, TAB(37 - Len(Microsoft.VisualBasic.Format(RP("QTY"), "#######0"))), Microsoft.VisualBasic.Format(RP("QTY"), "#######0"), TAB(58 - Len(Microsoft.VisualBasic.Format(RP("mrpvalue"), "#######0"))), Microsoft.VisualBasic.Format(RP("mrpvalue"), "#######0"), TAB(78 - Len(Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00"))
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Season Discount", TAB(78 - Len(Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(0), RP("TRadeDiscount"), SPC(1), TAB(40), "Less Discount", SPC(1), Microsoft.VisualBasic.Format((RP("SchemeDiscPercent")), "#######0"), TAB(56), SPC(1), "%", TAB(78 - Len(Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount :  ", TAB(78 - Len(Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(55), "-----------------------")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RP("ttl1"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(66), Chr(27) + Chr(69) + "", TAB(80 - Len(Microsoft.VisualBasic.Format(RP("ttl1"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("ttl1"), "#######0.00") + Chr(27) + Chr(70) + Chr(18)) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RP("exvalue"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(5), "DUTY EXEMPTED VALUE ", SPC(1), TAB(61 - Len(Microsoft.VisualBasic.Format(RP("exvalue"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("exvalue"), "#######0"), TAB(78 - Len(Microsoft.VisualBasic.Format("0.00"))), Microsoft.VisualBasic.Format("0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RP("taxsum1"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(5), (RP("stacode1")), SPC(1), TAB(78 - Len(Microsoft.VisualBasic.Format(RP("taxsum1"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("taxsum1"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            If (RP("Trascat")) <> "" Then PrintLine(1, TAB(5), (RP("Trascat")), TAB(30), (RP("stacode2")), SPC(1), TAB(78 - Len(Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00")) Else If Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(5), (RP("stacode2")), SPC(1), TAB(78 - Len(Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00")) Else PrintLine(1, Space(5), "")
            ''if Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(5), (RP("stacode2")), SPC(1), TAB(75 - Len(Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00")) Else 
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Add : Forwarding Charges", TAB(79 - Len(Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RP("RoundDif"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Round Off :  ", TAB(66), TAB(78 - Len(Microsoft.VisualBasic.Format(RP("RoundDif"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("RoundDif"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(10), Chr(27) + Chr(69) + "Grand Total :", TAB(39 - Len(Microsoft.VisualBasic.Format(RP("qty"), "#######0"))), Microsoft.VisualBasic.Format(RP("qty"), "#######0"), TAB(54), Microsoft.VisualBasic.Format(mbox, "#####0"), TAB(79 - Len(Microsoft.VisualBasic.Format(RP("GRANDTOTAL"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("GRANDTOTAL"), "#######0.00") + Chr(27) + Chr(70) + Chr(18))

            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RP("taxsum1"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(5), "Total Amount Duty Paid : ", SPC(1), Microsoft.VisualBasic.Format(RP("taxsum1"), "#######0.00")) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            If Microsoft.VisualBasic.Format(RP("taxsum1"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(0), "Words :", SPC(1), RupeesToWord(RP("taxsum1"))) Else PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(0), Chr(27) + Chr(69) + "", SPC(1), RupeesToWord(RP("GRANDTOTAL")), SPC(1), "" + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            n = 71 - lin
            For k = 1 To n
                lin = lin + 1
                ''PrintLine(1, Space(5), "")
            Next k

            lin = 0
            mbox = 0
            mmtr = 0
            mtotqty = 0
            mtotamt = 0



            FileClose(1)
            RP.Close()
            con.Close()
            con.Close()


            ''Shell("command.com /c TYPE " & " dbreportpath +"TestFile.txt>PRN", AppWinStyle.Hide)
            'Shell("print /d:LPT" & lpt.Text & " " + dbreportpath + "SSlorry.txt", vbNormalFocus)
            Dim printer As String = mlsprinter   'laserprinter name
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "SSlorry.txt"
            Dim success As Boolean = PrintTextFile(filePathname)

        End If
    End Sub


    Private Sub btnwgt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnwgt.Click
        If cmp.Text = "RR" Then
            'Call MAIN()

            'Dim conString As String
            'Dim con As SqlConnection


            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1

            com.CommandText = "update " & TABLE & " set u_lrwgt = '" & WGT.Text & "',u_lr_weight = '" & WGT.Text & "',U_Lrwight = '" & WGT.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()

            con1.Close()
            con1.Close()
        ElseIf cmp.Text = "RHL" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection

            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1

            com.CommandText = "update " & TABLE & " set u_lrwgt = '" & WGT.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            con1.Close()
            con1.Close()
        ElseIf cmp.Text = "ATC" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection

            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1

            com.CommandText = "update " & TABLE & " set u_lrwgt = '" & WGT.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            con1.Close()
            con1.Close()
        ElseIf cmp.Text = "VT" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection

            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1

            com.CommandText = "update " & TABLE & " set u_lrwgt = '" & WGT.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            con1.Close()
            con1.Close()
        ElseIf cmp.Text = "RCC" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection

            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1

            com.CommandText = "update " & TABLE & " set u_lrwgt = '" & WGT.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            con1.Close()
            con1.Close()
        ElseIf cmp.Text = "ACC" Then
            'Call MAIN()
            'Dim conString As String
            'Dim con As SqlConnection

            Dim com As New SqlCommand
            'conString = "Data Source=" & Trim(dbmyservername) & ";Initial Catalog=" & Trim(dbmydbname) & ";User ID=" & Trim(dbuserid) & ";Password=" & Trim(dbmypwd) & ""
            con1 = New SqlConnection(constr)
            con1.Open()
            com.Connection = con1
            com.CommandText = "update " & TABLE & " set u_lrwgt = '" & WGT.Text & "',u_lr_weight = '" & WGT.Text & "',U_Lrwight = '" & WGT.Text & "' where docnum in (" & No.Text & ") and PIndicator in ('" & Year.Text & "') "
            com.ExecuteNonQuery()
            com.Dispose()
            con1.Close()
            con1.Close()
        End If
    End Sub

    Private Sub btnfrwd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfrwd.Click
        'live
        If cmp.Text = "RR" Then
            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt As New ReportDocument()
            ''cryRpt.Load(Trim(dbreportpath) & Trim(DBFRW))

            'cryptfile = loadrptdb2(Trim(DBFRW), Trim(dbreportpath))
            'cryRpt.Load(cryptfile)

            'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))

            'cryRpt.SetParameterValue("Dockey@", Val(Label7.Text))

            ''Me.CrystalReportViewer1.Font = "Draft 10cpi, 10pt"
            'Me.CrystalReportViewer1.ReportSource = cryRpt
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt.Refresh()

            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(Label7.Text))
            ' paramDict("Dockey@") = Val(Label7.Text)


            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = DBFRW,
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
        ElseIf cmp.Text = "RHL" Then
            'Call MAIN()
            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt As New ReportDocument()
            ''cryRpt.Load(Trim(dbreportpath) & Trim(DBFRW))

            'cryptfile = loadrptdb2(Trim(DBFRW), Trim(dbreportpath))
            'cryRpt.Load(cryptfile)

            'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))

            ''cryRpt.SetParameterValue("FromDate", "2016-06-06")
            ''cryRpt.SetParameterValue("ToDate", "2016-06-06")
            ''cryRpt.SetParameterValue("CardCode@FROM OCRD", "")
            ''cryRpt.SetParameterValue("Indicator@select distinct Indicator from ofpr", "")

            'cryRpt.SetParameterValue("Dockey@", Val(Label7.Text))
            'Me.CrystalReportViewer1.ReportSource = cryRpt
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt.Refresh()

            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(Label7.Text))
            ' paramDict("Dockey@") = Val(Label7.Text)


            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = DBFRW,
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

        ElseIf cmp.Text = "ATC" Then
            'Call MAIN()
            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt As New ReportDocument()
            ''cryRpt.Load(Trim(dbreportpath) & Trim(DBFRW))

            'cryptfile = loadrptdb2(Trim(DBFRW), Trim(dbreportpath))
            'cryRpt.Load(cryptfile)

            'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))

            ''cryRpt.SetParameterValue("FromDate", "2016-06-06")
            ''cryRpt.SetParameterValue("ToDate", "2016-06-06")
            ''cryRpt.SetParameterValue("CardCode@FROM OCRD", "")
            ''cryRpt.SetParameterValue("Indicator@select distinct Indicator from ofpr", "")

            'cryRpt.SetParameterValue("Dockey@", Val(Label7.Text))
            'Me.CrystalReportViewer1.ReportSource = cryRpt
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            ' cryRpt.Refresh()
            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(Label7.Text))

            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = DBFRW,
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


        ElseIf cmp.Text = "VT" Then
            'Call MAIN()
            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt As New ReportDocument()
            ''cryRpt.Load(Trim(dbreportpath) & Trim(DBFRW))

            'cryptfile = loadrptdb2(Trim(DBFRW), Trim(dbreportpath))
            'cryRpt.Load(cryptfile)

            'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))

            ''cryRpt.SetParameterValue("FromDate", "2016-06-06")
            ''cryRpt.SetParameterValue("ToDate", "2016-06-06")
            ''cryRpt.SetParameterValue("CardCode@FROM OCRD", "")
            ''cryRpt.SetParameterValue("Indicator@select distinct Indicator from ofpr", "")

            'cryRpt.SetParameterValue("Dockey@", Val(Label7.Text))
            'Me.CrystalReportViewer1.ReportSource = cryRpt
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt.Refresh()
            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(Label7.Text))
            ' paramDict("Dockey@") = Val(Label7.Text)

            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = DBFRW,
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

        ElseIf cmp.Text = "RCC" Then
            ' Call MAIN()
            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt As New ReportDocument()
            ''  cryRpt.Load(Trim(dbreportpath) & Trim(DBFRW))

            'cryptfile = loadrptdb2(Trim(DBFRW), Trim(dbreportpath))
            'cryRpt.Load(cryptfile)


            'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))

            ''cryRpt.SetParameterValue("FromDate", "2016-06-06")
            ''cryRpt.SetParameterValue("ToDate", "2016-06-06")
            ''cryRpt.SetParameterValue("CardCode@FROM OCRD", "")
            ''cryRpt.SetParameterValue("Indicator@select distinct Indicator from ofpr", "")

            'cryRpt.SetParameterValue("Dockey@", Val(Label7.Text))
            'Me.CrystalReportViewer1.ReportSource = cryRpt
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt.Refresh()
            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(Label7.Text))
            ' paramDict("Dockey@") = Val(Label7.Text)


            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = DBFRW,
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

        ElseIf cmp.Text = "ACC" Then
            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt3 As New ReportDocument()
            ''  cryRpt3.Load(Trim(dbreportpath) & Trim(DBFRW))

            'cryptfile = loadrptdb2(Trim(DBFRW), Trim(dbreportpath))
            'cryRpt3.Load(cryptfile)


            'CrystalReportLogOn(cryRpt3, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))


            'cryRpt3.SetParameterValue("Dockey@", Val(Label7.Text))
            ''Me.CrystalReportViewer1.Font = "Draft 10cpi, 10pt"

            'Me.CrystalReportViewer1.ReportSource = cryRpt3
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt3.Refresh()
            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(Label7.Text))
            ' paramDict("Dockey@") = Val(Label7.Text)


            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = DBFRW,
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
        End If
    End Sub

    Private Sub Year_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Year.SelectedIndexChanged

    End Sub

    Private Sub lWGT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lWGT.Click
        WGT.Enabled = True
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub cmp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmp.SelectedIndexChanged

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        cmp.Enabled = True
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub btnweight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnweight.Click
        frmBundleWeight.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'live
        If cmp.Text = "RR" Then
            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt As New ReportDocument()
            ''  cryRpt.Load(Trim(dbreportpath) & Trim(dbcard))

            'cryptfile = loadrptdb2(Trim(dbcard), Trim(dbreportpath))
            'cryRpt.Load(cryptfile)


            'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))
            'cryRpt.SetParameterValue("Dockey@", Val(No.Text))
            'Me.CrystalReportViewer1.ReportSource = cryRpt
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt.Refresh()
            ''cryRpt.Dispose()
            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(No.Text))
            ' paramDict("Dockey@") = Val(Label7.Text)

            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = dbcard,
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

        ElseIf cmp.Text = "RHL" Then
            'Call MAIN()
            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt As New ReportDocument()
            '' cryRpt.Load(Trim(dbreportpath) & Trim(dbcard))

            'cryptfile = loadrptdb2(Trim(dbcard), Trim(dbreportpath))
            'cryRpt.Load(cryptfile)

            'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))
            'cryRpt.SetParameterValue("Dockey@", Val(No.Text))
            'Me.CrystalReportViewer1.ReportSource = cryRpt
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt.Refresh()
            ''cryRpt.Dispose()
            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(No.Text))
            ' paramDict("Dockey@") = Val(Label7.Text)

            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = dbcard,
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


        ElseIf cmp.Text = "ATC" Then
            'Call MAIN()
            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt As New ReportDocument()
            '' cryRpt.Load(Trim(dbreportpath) & Trim(dbcard))

            'cryptfile = loadrptdb2(Trim(dbcard), Trim(dbreportpath))
            'cryRpt.Load(cryptfile)

            'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))
            'cryRpt.SetParameterValue("Dockey@", Val(No.Text))
            'Me.CrystalReportViewer1.ReportSource = cryRpt
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt.Refresh()
            ''cryRpt.Dispose()
            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(No.Text))
            ' paramDict("Dockey@") = Val(Label7.Text)


            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = dbcard,
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

        ElseIf cmp.Text = "VT" Then
            'Call MAIN()
            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt As New ReportDocument()
            '' cryRpt.Load(Trim(dbreportpath) & Trim(dbcard))

            'cryptfile = loadrptdb2(Trim(dbcard), Trim(dbreportpath))
            'cryRpt.Load(cryptfile)

            'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))
            'cryRpt.SetParameterValue("Dockey@", Val(No.Text))
            'Me.CrystalReportViewer1.ReportSource = cryRpt
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt.Refresh()
            ''cryRpt.Dispose()

            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(No.Text))
            ' paramDict("Dockey@") = Val(Label7.Text)


            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = dbcard,
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

        ElseIf cmp.Text = "RCC" Then
            'Call MAIN()
            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt As New ReportDocument()
            '' cryRpt.Load(Trim(dbreportpath) & Trim(dbcard))

            'cryptfile = loadrptdb2(Trim(dbcard), Trim(dbreportpath))
            'cryRpt.Load(cryptfile)

            'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))
            'cryRpt.SetParameterValue("Dockey@", Val(No.Text))
            'Me.CrystalReportViewer1.ReportSource = cryRpt
            'Me.CrystalReportViewer1.PrintReport()
            'cryRpt.Refresh()
            'Me.CrystalReportViewer1.Refresh()
            ''cryRpt.Dispose()

            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(No.Text))
            ' paramDict("Dockey@") = Val(Label7.Text)


            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = dbcard,
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


        ElseIf cmp.Text = "ACC" Then
            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt3 As New ReportDocument()
            ''cryRpt3.Load(Trim(dbreportpath) & Trim(dbcard))


            'cryptfile = loadrptdb2(Trim(dbcard), Trim(dbreportpath))
            'cryRpt3.Load(cryptfile)

            'CrystalReportLogOn(cryRpt3, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))


            'cryRpt3.SetParameterValue("Dockey@", Val(No.Text))
            ''Me.CrystalReportViewer1.Font = "Draft 10cpi, 10pt"

            'Me.CrystalReportViewer1.ReportSource = cryRpt3
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt3.Refresh()
            ''cryRpt3.Dispose()

            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(No.Text))
            ' paramDict("Dockey@") = Val(Label7.Text)

            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = dbcard,
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
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'live

        'Dim cryRpt As New ReportDocument()
        ''cryRpt.Load(Trim(dbreportpath) & Trim(System.Configuration.ConfigurationManager.AppSettings("address")))


        'cryptfile = loadrptdb2(Trim(System.Configuration.ConfigurationManager.AppSettings("address")), Trim(dbreportpath))
        'cryRpt.Load(cryptfile)
        'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))
        'cryRpt.SetParameterValue("Dockey@", Val(No.Text))
        'Me.CrystalReportViewer1.ReportSource = cryRpt
        'Me.CrystalReportViewer1.PrintReport()
        'Me.CrystalReportViewer1.Refresh()
        'cryRpt.Refresh()
        ''cryRpt.Dispose()
        ''Me.Cursor = Cursors.Default

        Dim paramDict As New Dictionary(Of String, Object)
        paramDict.Add("Dockey@", Val(No.Text))
        ' paramDict("Dockey@") = Val(Label7.Text)


        Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

        Dim req As New PrintRequest() With {
             .ReportName = maddress,
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


    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ComboBox1.Text = "Sales" Then
            'Call MAIN()
            rccsql = "select b.text,b.u_mrp,A.U_BRAND,CASE WHEN C.TransCat = 'FORM H' THEN 'Taxable' WHEN A.VatSum = '0' THEN 'Non  Taxable' ELSE 'Taxable' End AS TAXABLE, A.DOCENTRY,isnull(b.LineNum,'') LineNum,a.address,a.address2,A.cardname,a.U_Noofbun,r.Building,isnull(r.Block,'') Block,(r.City+'-'+ r.ZipCode) city , " & vbCrLf _
    & "CASE WHEN R.State='OS' THEN R.StreetNo WHEN l.QryGroup5='Y' THEN R.StreetNo ELSE ST.Name END AS STATE,r.State State1, " & vbCrLf _
    & "isnull(r.Street,'')Street,r.country,r.county, A.u_ESUGAM,A.docnum,a.Docdate,A.u_orderby,A.u_arcode,A.u_transport,A.u_docthrough,A.u_lrno,convert(varchar(10),A.u_lrdat,110) u_lrdat,A.u_lrwight,a.u_dsnation,a.U_lrval AS topay, " & vbCrLf _
    & "CASE when isnull(b.FreeTxt,'') = '' then isnull(B.U_CatalogCode ,'')  else  isnull(B.FreeTxt,'')  end as item, " & vbCrLf _
    & "b.u_style,CASE WHEN L.Cellular IS NULL THEN L.Phone1 ELSE L.Cellular END Cellular ,b.u_size,b.baseref,b.price,b.linetotal,b.quantity,a.DiscSum,a.VatSum,a.RoundDif,D.firstname,f.U_remarks,b.taxcode, " & vbCrLf _
    & "b.BaseREf,b.U_NoofPiece,b.Volume Box,a.TotalExpns,c.TransCat,(isnull(cRD.TaxId1,''))as CSTNO,(isnull(cRD.TaxId11,'')) as TINNO,l.CardFName, " & vbCrLf _
    & "a.U_Dis1'TRADE DISCOUNT',a.U_Dis2'CASH DISCOUNT',a.U_Dis3'CD/AD DRAFT DISC',a.U_Dis4'CD/AD/QTY DISC' " & vbCrLf _
    & ",A.U_AreaCode,a.U_Dis5'CD/LR-AGAINST',a.U_Dis6'QTY DISCOUNT',a.U_Dis7'SPL DISCOUNT',a.U_Dis8'VAT EXCEMPATION',a.U_Dis9'TURNOVER' " & vbCrLf _
    & ",isnull(a.U_Dis10,0)'VAT DISCOUNT',a.DiscPrcnt,Left(b.Taxcode,3) as TAXCODE,a.numatcard, " & vbCrLf _
    & "RTRIM(convert(nvarchar(100),isnull(r.building,'')))+','+rtrim(convert(nvarchar(100),isnull(r.block,'')))+','+RTRIM(convert(nvarchar(100),isnull(r.Street,'')))+'-'+RTRIM(convert(nvarchar(50),isnull(r.ZipCode,'')))+','+rtrim(CONVERT(nvarchar(100),isnull(r.city,'')))+','+ " & vbCrLf _
    & "RTRIM(CONVERT(nvarchar(10),isnull(r.State,'')))+','+RTRIM(CONVERT(nvarchar(50),ISNULL(r.county,'')))+','+RTRIM(CONVERT(nvarchar(10),ISNULL(r.country,''))) as address3 " & vbCrLf _
    & ",Isnull(Convert(Nvarchar,P.U_Remarks)+'-'+'','') [Item Remarks],b.PriceBefDi,b.Quantity*b.PriceBefDi [Before Disc],b.LineTotal,G.DiscPrcnt [Item Disc] " & vbCrLf _
    & ",ISNULL(S.[FORWARDING CHARGES],0) [FORWARDING CHARGES],b.DiscPrcnt [Item Disc Prct], " & vbCrLf _
    & "isnull(G.[Disc Amt],0) [Disc Amt] " & vbCrLf _
    & ",CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent' " & vbCrLf _
    & ",CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt',a.cardcode  " & vbCrLf _
     & "from " & TABLE & " A inner join " & TABLE1 & " b on a.DocEntry=b.DocEntry  " & vbCrLf _
    & "left outer join OHEM D on D.empid=a.ownercode  " & vbCrLf _
    & "Left Join [@INCM_BND1] F on F.U_Name = a.U_brand  " & vbCrLf _
    & "Left join " & TABLE12 & " as c on c.DocEntry = b.DocEntry	 " & vbCrLf _
    & "left join OITM as itm on itm.ItemCode = b.ItemCode  " & vbCrLf _
    & "left join (SELECT * FROM [dbo].[@INS_OPLM] WHERE DocEntry IN (SELECT DocEntry FROM [@INS_PLM1] WHERE U_Remarks IS NOT NULL))  " & vbCrLf _
    & "as itb on itb.U_ItemCode =itm.ItemCode  " & vbCrLf _
    & "left join (SELECT DISTINCT CARDCODE,ISNULL(U_ActTino,'') TAXID11,ISNULL(U_ActCstno,'') TAXID1 FROM  OCRD  ) as crd on crd.CardCode = a.CardCode " & vbCrLf _
    & "left join CRD1 as r on r.CardCode = a.CardCode /*and a.paytocode = r.address*/ AND R.AdresType='B'  " & vbCrLf _
    & "Left Outer Join OCST ST ON R.State=ST.Code AND R.Country=ST.Country AND ST.Country='IN'  " & vbCrLf _
    & "left OUTER join OCRD as l on l.CardCode = a.CardCode  " & vbCrLf _
    & "left OUTER join (SELECT DISTINCT CONVERT(NVARCHAR(MAX),U_ItemName) U_ItemName ,CONVERT(NVARCHAR(MAX),ISNULL(U_Remarks,'')) U_Remarks,U_Subbrand FROM [@INS_PLM1] WHERE u_lock = 'N') P on P.U_ItemName=B.U_CatalogName AND L.U_Subbrand AND P.U_Subbrand" & vbCrLf _
    & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry,  " & vbCrLf _
    & "sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from " & TABLE1 & " where DiscPrcnt=100 group by DocEntry) G  " & vbCrLf _
    & "on G.DocEntry=B.DocEntry  " & vbCrLf _
    & "left OUTER join (Select ItemCode,sum(Price) [FORWARDING CHARGES],DocEntry from " & TABLE1 & " WHERE ItemCode='FrEightCharges' group by ItemCode,DocEntry) S ON S.DocEntry=B.DocEntry  " & vbCrLf _
    & "where a.docnum in (" & No.Text & ") and a.PIndicator in ('" & Year.Text & "') and b.Treetype <> 'I'  and b.ItemCode<>'FrEightCharges' order by b.LineNum"


            rccmtotqty = 0
            rccmtotamt = 0
            PAG = 0

            Dim CMDp As New SqlCommand(rccsql, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim RCCDRP As SqlDataReader
            RCCDRP = CMDp.ExecuteReader
            RCCDRP.Read()

            lin = 0
            FileOpen(1, " " + mlinpath + "RCCDELIVERY.txt", OpenMode.Output, OpenAccess.Write)
            ''FileOpen(1, Trim(dbreportpath) & "RCClorry.txt", OpenMode.Output, OpenAccess.Write)
            'FileOpen(1, "e:\Test.txt", OpenMode.Output, OpenAccess.Write)
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, TAB(60), DateTime.Now.ToString())
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, TAB(5), "M/s.", SPC(1), Chr(27) + Chr(69) + RCCDRP.Item("CardFName").ToString + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(5), RCCDRP.Item("BUILDING").ToString)
            lin = lin + 1
            PrintLine(1, TAB(5), RCCDRP.Item("BLOCK").ToString, TAB(60), Chr(27) + Chr(63) + Chr(18), TAB(70), (RCCDRP("U_remarks").ToString), SPC(1), "", SPC(1), (RCCDRP("docnum").ToString))
            lin = lin + 1
            PrintLine(1, TAB(5), RCCDRP.Item("STREET").ToString)
            lin = lin + 1
            PrintLine(1, TAB(5), RCCDRP.Item("CITY").ToString)
            lin = lin + 1
            PrintLine(1, TAB(5), RCCDRP.Item("county").ToString, SPC(1), "Dt.", SPC(1), "(", RCCDRP.Item("state1").ToString, SPC(1), ")", TAB(63), SPC(1), Microsoft.VisualBasic.FormatDateTime(RCCDRP("docdate"), DateFormat.ShortDate))
            lin = lin + 1
            PrintLine(1, TAB(5), "TIN NO.:", SPC(1), RCCDRP.Item("TINNO").ToString)
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, TAB(12), (RCCDRP("u_arcode").ToString), TAB(25), SPC(1), (RCCDRP("u_orderby").ToString), TAB(44), SPC(1), Microsoft.VisualBasic.FormatDateTime(RCCDRP("docdate"), DateFormat.ShortDate), TAB(70), SPC(1), Chr(27) + Chr(69) + (RCCDRP("docnum").ToString), SPC(1), "/", SPC(1), (RCCDRP("U_Noofbun").ToString) + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, TAB(12), Chr(27) + Chr(69) + (RCCDRP("u_transport").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(70), SPC(1), Chr(27) + Chr(69) + (RCCDRP("u_dsnation").ToString))
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, TAB(25), (RCCDRP("u_docthrough")), TAB(65), SPC(1), Chr(27) + Chr(69) + (RCCDRP("U_BRAND").ToString))
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            PrintLine(1, TAB(11), (RCCDRP("u_size").ToString), TAB(21), (RCCDRP("item").ToString), TAB(55 - Len(Microsoft.VisualBasic.Format(RCCDRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(RCCDRP("U_NoofPiece"), "#######0"), TAB(65 - Len(Microsoft.VisualBasic.Format(RCCDRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("box"), "#######0.00"), TAB(72 - Len(Microsoft.VisualBasic.Format(RCCDRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("PriceBefDi"), "#######0.00"), TAB(81 - Len(Microsoft.VisualBasic.Format(RCCDRP("u_mrp"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("u_mrp"), "#######0.00"))
            lin = lin + 1
            PrintLine(1, TAB(5), (RCCDRP("text").ToString))
            lin = lin + 1
            rccmtotamt = rccmtotamt + RCCDRP("BEFORE DISC")
            rccmtotqty = rccmtotqty + RCCDRP("U_NoofPiece")
            rccmbox = rccmbox + RCCDRP("box")
            While RCCDRP.Read
                PrintLine(1, TAB(11), (RCCDRP("u_size").ToString), TAB(21), (RCCDRP("item").ToString), TAB(55 - Len(Microsoft.VisualBasic.Format(RCCDRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(RCCDRP("U_NoofPiece"), "#######0"), TAB(65 - Len(Microsoft.VisualBasic.Format(RCCDRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("box"), "#######0.00"), TAB(72 - Len(Microsoft.VisualBasic.Format(RCCDRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("PriceBefDi"), "#######0.00"), TAB(81 - Len(Microsoft.VisualBasic.Format(RCCDRP("u_mrp"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("u_mrp"), "#######0.00"))
                lin = lin + 1
                PrintLine(1, TAB(5), (RCCDRP("text").ToString))
                lin = lin + 1
                rccmtotamt = rccmtotamt + RCCDRP("BEFORE DISC")
                rccmtotqty = rccmtotqty + RCCDRP("U_NoofPiece")
                rccmbox = rccmbox + RCCDRP("box")
                If lin > 59 Then
                    n = 59 - lin
                    For k = 1 To n
                        PrintLine(1, Space(5), "")
                        lin = lin + 1
                    Next k
                    PrintLine(1, TAB(66), "")
                    lin = lin + 1
                    PrintLine(1, TAB(66), "")
                    lin = lin + 1
                    PrintLine(1, TAB(20), "Continue........", SPC(1), PAG + 1, TAB(55 - Len(Microsoft.VisualBasic.Format(rccmtotqty, "#######0"))), Microsoft.VisualBasic.Format(rccmtotqty, "#######0"), TAB(65 - Len(Microsoft.VisualBasic.Format(rccmbox, "#######0.00"))), Microsoft.VisualBasic.Format(rccmbox, "#######0.00"))
                    lin = lin + 1

                    n = 72 - lin
                    For k = 1 To n
                        PrintLine(1, Space(5), "")
                        lin = lin + 1
                    Next k

                    lin = 0
                    PAG = PAG + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, TAB(60), DateTime.Now.ToString())
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, TAB(5), "M/s.", SPC(1), Chr(27) + Chr(69) + RCCDRP.Item("CardFName").ToString + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, TAB(5), RCCDRP.Item("BUILDING").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(5), RCCDRP.Item("BLOCK").ToString, TAB(60), Chr(27) + Chr(63) + Chr(18), TAB(70), (RCCDRP("U_remarks").ToString), SPC(1), "", SPC(1), (RCCDRP("docnum").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(5), RCCDRP.Item("STREET").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(5), RCCDRP.Item("CITY").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(5), RCCDRP.Item("county").ToString, SPC(1), "Dt.", SPC(1), "(", RCCDRP.Item("state1").ToString, SPC(1), ")", TAB(63), SPC(1), Microsoft.VisualBasic.FormatDateTime(RCCDRP("docdate"), DateFormat.ShortDate))
                    lin = lin + 1
                    PrintLine(1, TAB(5), "TIN NO.:", SPC(1), RCCDRP.Item("TINNO").ToString)
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, TAB(12), (RCCDRP("u_arcode").ToString), TAB(25), SPC(1), (RCCDRP("u_orderby").ToString), TAB(44), SPC(1), Microsoft.VisualBasic.FormatDateTime(RCCDRP("docdate"), DateFormat.ShortDate), TAB(70), SPC(1), Chr(27) + Chr(69) + (RCCDRP("docnum").ToString), SPC(1), "/", SPC(1), (RCCDRP("U_Noofbun").ToString) + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, TAB(12), Chr(27) + Chr(69) + (RCCDRP("u_transport").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(70), SPC(1), Chr(27) + Chr(69) + (RCCDRP("u_dsnation").ToString))
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, TAB(25), (RCCDRP("u_docthrough")), TAB(65), SPC(1), Chr(27) + Chr(69) + (RCCDRP("U_BRAND").ToString))
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                End If
            End While
            RCCDRP.Close()
            con.Close()
            con.Close()

            n = 59 - lin
            For k = 1 To n
                PrintLine(1, Space(5), "")
                lin = lin + 1
            Next k


            RCCTSQL = "SELECT isnull(Esugam,'') Esugam,discprcnt,SUM(BeforeDisc) BeforeDisc,DiscAmt,MAX(SchemeDiscPercent)SchemeDiscPercent,[FORWARDING CHARGES] AS FORWARDINGCHARGES,DiscSum,sum(SchemeDiscAmt) SchemeDiscAmt ," & vbCrLf _
    & "(((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES] ) ttl1,VatSum,RoundDif, (((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES] + VatSum + RoundDif ) GRANDTOTAL" & vbCrLf _
    & ",taxcode,CASE WHEN TransCat = 'Form C' THEN '[Against C Form]' ELSE ' ' END  CFROM,CASE WHEN TransCat = 'Form H' THEN 'TAX EXEMPTED AGAINST FORM H' ELSE ' ' END  HFROM,sum(U_NoofPiece)U_NoofPiece,SUM(Quantity) QTY,SUM(box)  BOX" & vbCrLf _
    & "FROM (SELECT (b.Quantity*b.PriceBefDi) BeforeDisc,isnull(G.[Disc Amt],0) DiscAmt ,ISNULL(S.[FORWARDING CHARGES],isnull(fr.LineTotal,0)) [FORWARDING CHARGES]," & vbCrLf _
    & "case when c.State = 'KA' Then 'e-sugam no.' + a.U_ESugam when c.State in ('AP','TS') Then 'e-wayBill no.' + a.U_ESugam  when c.State in ('KL') Then 'e-Token No.' + a.U_ESugam  else '' end Esugam," & vbCrLf _
    & "a.discprcnt,a.DiscSum,A.VatSum,A.RoundDif,B.TaxCode,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt',C.TransCat,b.U_NoofPiece,B.Quantity,(b.Volume) Box,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent'" & vbCrLf _
    & "FROM " & TABLE & " A JOIN " & TABLE1 & " B ON B.DocEntry = A.DocEntry" & vbCrLf _
    & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry,sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from " & TABLE1 & " where DiscPrcnt=100 group by DocEntry) G on G.DocEntry=B.DocEntry" & vbCrLf _
    & "left OUTER join (Select ItemCode,sum(Price) [FORWARDING CHARGES],DocEntry from " & TABLE1 & " WHERE ItemCode='FrEightCharges' group by  ItemCode,DocEntry) S ON S.DocEntry=B.DocEntry  " & vbCrLf _
    & "left join OITM as itm on itm.ItemCode = b.ItemCode" & vbCrLf _
    & "Left join " & TABLE12 & " as c on c.DocEntry = b.DocEntry" & vbCrLf _
    & "Left join " & TABLE3 & " as fr on fr.DocEntry = b.DocEntry" & vbCrLf _
    & "WHERE A.docnum = (" & No.Text & ") and   a.PIndicator in ('" & Year.Text & "') and b.TreeType <> 'I' and b.ItemCode<>'FrEightCharges'  ) k GROUP BY DiscAmt,[FORWARDING CHARGES],DiscSum,VatSum,RoundDif,taxcode,TransCat,Esugam,discprcnt"


            Dim CMD As New SqlCommand(RCCTSQL, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim RCCRP As SqlDataReader
            RCCRP = CMD.ExecuteReader
            RCCRP.Read()
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(23), Chr(27) + Chr(69), TAB(2), "TEXTILE GOODS TAX EXEMPTED COMMODITY CODE", TAB(55 - Len(Microsoft.VisualBasic.Format(RCCRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(RCCRP("U_NoofPiece"), "#######0"), TAB(65 - Len(Microsoft.VisualBasic.Format(RCCRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("box"), "#######0.00"), Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(23), Chr(27) + Chr(69), TAB(2), "794,795,796")
            lin = lin + 1
            n = 72 - lin
            For k = 1 To n
                ' PrintLine(1, Space(5), "")
            Next k

            lin = 0
            mbox = 0
            mmtr = 0
            mtotqty = 0
            mtotamt = 0



            FileClose(1)
            RCCRP.Close()
            con.Close()
            con.Close()

            ''  Shell("print /d:LPT" & lpt.Text & " " + dbreportpath + "RCClorry.txt", vbNormalFocus)
            ''Shell("command.com /c TYPE " & " " + dbreportpath + "RCCDELIVERY.txt>PRN", AppWinStyle.Hide)
            ''   Shell("print /d:LPT" & lpt.Text & " dbreportpath +"RRlorry.txt", vbNormalFocus)

            'Shell("print /d:LPT" & lpt.Text & " " & " " + dbreportpath + "RCCDELIVERY.txt", vbNormalFocus)
            Dim printer As String = mlsprinter  'laserprinter name
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "RCCDELIVERY.txt"
            Dim success As Boolean = PrintTextFile(filePathname)
        Else
            ' Call MAIN()
            rccsql = "select b.text,b.u_mrp,A.U_BRAND,CASE WHEN C.TransCat = 'FORM H' THEN 'Taxable' WHEN A.VatSum = '0' THEN 'Non  Taxable' ELSE 'Taxable' End AS TAXABLE, A.DOCENTRY,isnull(b.LineNum,'') LineNum,a.address,a.address2,A.cardname,a.U_Noofbun,r.Building,isnull(r.Block,'') Block,(r.City+'-'+ r.ZipCode) city , " & vbCrLf _
    & "CASE WHEN R.State='OS' THEN R.StreetNo WHEN l.QryGroup5='Y' THEN R.StreetNo ELSE ST.Name END AS STATE,r.State State1, " & vbCrLf _
    & "isnull(r.Street,'')Street,r.country,r.county, A.u_ESUGAM,A.docnum,a.Docdate,A.u_orderby,A.u_arcode,A.u_transport,A.u_docthrough,A.u_lrno,convert(varchar(10),A.u_lrdat,110) u_lrdat,A.u_lrwight,a.u_dsnation,a.U_lrval AS topay, " & vbCrLf _
    & " CASE when isnull(b.FreeTxt,'') = '' then isnull(B.U_CatalogCode ,'')  else  isnull(B.FreeTxt,'')  end  as item, " & vbCrLf _
    & "b.u_style,CASE WHEN L.Cellular IS NULL THEN L.Phone1 ELSE L.Cellular END Cellular ,b.u_size,b.baseref,b.price,b.linetotal,b.quantity,a.DiscSum,a.VatSum,a.RoundDif,D.firstname,f.U_remarks,b.taxcode, " & vbCrLf _
    & "b.BaseREf,b.U_NoofPiece,b.Volume Box,a.TotalExpns,c.TransCat,(isnull(cRD.TaxId1,''))as CSTNO,(isnull(cRD.TaxId11,'')) as TINNO,l.CardFName, " & vbCrLf _
    & "a.U_Dis1'TRADE DISCOUNT',a.U_Dis2'CASH DISCOUNT',a.U_Dis3'CD/AD DRAFT DISC',a.U_Dis4'CD/AD/QTY DISC' " & vbCrLf _
    & ",A.U_AreaCode,a.U_Dis5'CD/LR-AGAINST',a.U_Dis6'QTY DISCOUNT',a.U_Dis7'SPL DISCOUNT',a.U_Dis8'VAT EXCEMPATION',a.U_Dis9'TURNOVER' " & vbCrLf _
    & ",isnull(a.U_Dis10,0)'VAT DISCOUNT',a.DiscPrcnt,Left(b.Taxcode,3) as TAXCODE,a.numatcard, " & vbCrLf _
    & "RTRIM(convert(nvarchar(100),isnull(r.building,'')))+','+rtrim(convert(nvarchar(100),isnull(r.block,'')))+','+RTRIM(convert(nvarchar(100),isnull(r.Street,'')))+'-'+RTRIM(convert(nvarchar(50),isnull(r.ZipCode,'')))+','+rtrim(CONVERT(nvarchar(100),isnull(r.city,'')))+','+ " & vbCrLf _
    & "RTRIM(CONVERT(nvarchar(10),isnull(r.State,'')))+','+RTRIM(CONVERT(nvarchar(50),ISNULL(r.county,'')))+','+RTRIM(CONVERT(nvarchar(10),ISNULL(r.country,''))) as address3 " & vbCrLf _
    & ",Isnull(Convert(Nvarchar,P.U_Remarks)+'-'+'','') [Item Remarks],b.PriceBefDi,b.Quantity*b.PriceBefDi [Before Disc],b.LineTotal,G.DiscPrcnt [Item Disc] " & vbCrLf _
    & ",ISNULL(S.[FORWARDING CHARGES],0) [FORWARDING CHARGES],b.DiscPrcnt [Item Disc Prct], " & vbCrLf _
    & "isnull(G.[Disc Amt],0) [Disc Amt] " & vbCrLf _
    & ",CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent' " & vbCrLf _
    & ",CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt',a.cardcode  " & vbCrLf _
     & "from " & TABLE & " A inner join " & TABLE1 & " b on a.DocEntry=b.DocEntry  " & vbCrLf _
    & "left outer join OHEM D on D.empid=a.ownercode  " & vbCrLf _
    & "Left Join [@INCM_BND1] F on F.U_Name = a.U_brand  " & vbCrLf _
    & "Left join " & TABLE12 & " as c on c.DocEntry = b.DocEntry	 " & vbCrLf _
    & "left join OITM as itm on itm.ItemCode = b.ItemCode  " & vbCrLf _
    & "left join (SELECT * FROM [dbo].[@INS_OPLM] WHERE DocEntry IN (SELECT DocEntry FROM [@INS_PLM1] WHERE U_Remarks IS NOT NULL))  " & vbCrLf _
    & "as itb on itb.U_ItemCode =itm.ItemCode  " & vbCrLf _
    & "left join (SELECT DISTINCT CARDCODE,ISNULL(U_ActTino,'') TAXID11,ISNULL(U_ActCstno,'') TAXID1 FROM  OCRD  ) as crd on crd.CardCode = a.CardCode " & vbCrLf _
    & "left join CRD1 as r on r.CardCode = a.CardCode /*and a.paytocode = r.address*/ AND R.AdresType='B'  " & vbCrLf _
    & "Left Outer Join OCST ST ON R.State=ST.Code AND R.Country=ST.Country AND ST.Country='IN'  " & vbCrLf _
    & "left OUTER join OCRD as l on l.CardCode = a.CardCode  " & vbCrLf _
    & "left OUTER join (SELECT DISTINCT CONVERT(NVARCHAR(MAX),U_ItemName) U_ItemName ,CONVERT(NVARCHAR(MAX),ISNULL(U_Remarks,'')) U_Remarks,U_Subbrand FROM [@INS_PLM1] WHERE u_lock = 'N') P on P.U_ItemName=B.U_CatalogName AND L.U_Subbrand = P.U_Subbrand" & vbCrLf _
    & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry,  " & vbCrLf _
    & "sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from " & TABLE1 & " where DiscPrcnt=100 group by DocEntry) G  " & vbCrLf _
    & "on G.DocEntry=B.DocEntry  " & vbCrLf _
    & "left OUTER join (Select ItemCode,sum(Price) [FORWARDING CHARGES],DocEntry from " & TABLE1 & " WHERE ItemCode='FrEightCharges' group by ItemCode,DocEntry) S ON S.DocEntry=B.DocEntry  " & vbCrLf _
    & "where a.docnum in (" & No.Text & ") and a.PIndicator in ('" & Year.Text & "') and b.Treetype <> 'I'  and b.ItemCode<>'FrEightCharges' order by b.LineNum"


            rccmtotqty = 0
            rccmtotamt = 0
            PAG = 0

            Dim CMDp As New SqlCommand(rccsql, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim RCCDRP As SqlDataReader
            RCCDRP = CMDp.ExecuteReader
            RCCDRP.Read()

            lin = 0
            FileOpen(1, " " + mlinpath + "RCCDELIVERY.txt", OpenMode.Output, OpenAccess.Write)
            ''FileOpen(1, Trim(dbreportpath) & "RCClorry.txt", OpenMode.Output, OpenAccess.Write)
            'FileOpen(1, "e:\Test.txt", OpenMode.Output, OpenAccess.Write)
            PrintLine(1, TAB(33), Chr(27) + Chr(45) + Chr(1) + Chr(14) + Chr(27) + Chr(69) + "DELIVERY" + Chr(27) + Chr(70) + Chr(18) + Chr(27) + Chr(45) + Chr(0) + Chr(18))
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, TAB(60), DateTime.Now.ToString())
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, TAB(5), "M/s.", SPC(1), Chr(27) + Chr(69) + RCCDRP.Item("CardFName").ToString + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(5), RCCDRP.Item("BUILDING").ToString)
            lin = lin + 1
            PrintLine(1, TAB(5), RCCDRP.Item("BLOCK").ToString, TAB(60), Chr(27) + Chr(63) + Chr(18), TAB(70), (RCCDRP("U_remarks").ToString), SPC(1), "", SPC(1), (RCCDRP("docnum").ToString))
            lin = lin + 1
            PrintLine(1, TAB(5), RCCDRP.Item("STREET").ToString)
            lin = lin + 1
            PrintLine(1, TAB(5), RCCDRP.Item("CITY").ToString)
            lin = lin + 1
            PrintLine(1, TAB(5), RCCDRP.Item("county").ToString, SPC(1), "Dt.", SPC(1), "(", RCCDRP.Item("state1").ToString, SPC(1), ")", TAB(63), SPC(1), Microsoft.VisualBasic.FormatDateTime(RCCDRP("docdate"), DateFormat.ShortDate))
            lin = lin + 1
            PrintLine(1, TAB(5), "TIN NO.:", SPC(1), RCCDRP.Item("TINNO").ToString)
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, TAB(12), (RCCDRP("u_arcode").ToString), TAB(25), SPC(1), (RCCDRP("u_orderby").ToString), TAB(44), SPC(1), Microsoft.VisualBasic.FormatDateTime(RCCDRP("docdate"), DateFormat.ShortDate), TAB(70), SPC(1), Chr(27) + Chr(69) + (RCCDRP("docnum").ToString), SPC(1), "/", SPC(1), (RCCDRP("U_Noofbun").ToString) + Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, TAB(12), Chr(27) + Chr(69) + (RCCDRP("u_transport").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(70), SPC(1), Chr(27) + Chr(69) + (RCCDRP("u_dsnation").ToString))
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, TAB(25), (RCCDRP("u_docthrough")), TAB(65), SPC(1), Chr(27) + Chr(69) + (RCCDRP("U_BRAND").ToString))
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            PrintLine(1, TAB(11), (RCCDRP("u_size").ToString), TAB(21), (RCCDRP("item").ToString), TAB(55 - Len(Microsoft.VisualBasic.Format(RCCDRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(RCCDRP("U_NoofPiece"), "#######0"), TAB(65 - Len(Microsoft.VisualBasic.Format(RCCDRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("box"), "#######0.00"), TAB(72 - Len(Microsoft.VisualBasic.Format(RCCDRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("PriceBefDi"), "#######0.00"), TAB(81 - Len(Microsoft.VisualBasic.Format(RCCDRP("u_mrp"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("u_mrp"), "#######0.00"))
            lin = lin + 1
            PrintLine(1, TAB(5), (RCCDRP("text").ToString))
            lin = lin + 1
            rccmtotamt = rccmtotamt + RCCDRP("BEFORE DISC")
            rccmtotqty = rccmtotqty + RCCDRP("U_NoofPiece")
            rccmbox = rccmbox + RCCDRP("box")
            While RCCDRP.Read
                PrintLine(1, TAB(11), (RCCDRP("u_size").ToString), TAB(21), (RCCDRP("item").ToString), TAB(55 - Len(Microsoft.VisualBasic.Format(RCCDRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(RCCDRP("U_NoofPiece"), "#######0"), TAB(65 - Len(Microsoft.VisualBasic.Format(RCCDRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("box"), "#######0.00"), TAB(72 - Len(Microsoft.VisualBasic.Format(RCCDRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("PriceBefDi"), "#######0.00"), TAB(81 - Len(Microsoft.VisualBasic.Format(RCCDRP("u_mrp"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCDRP("u_mrp"), "#######0.00"))
                lin = lin + 1
                PrintLine(1, TAB(5), (RCCDRP("text").ToString))
                lin = lin + 1
                rccmtotamt = rccmtotamt + RCCDRP("BEFORE DISC")
                rccmtotqty = rccmtotqty + RCCDRP("U_NoofPiece")
                rccmbox = rccmbox + RCCDRP("box")
                If lin > 59 Then
                    n = 59 - lin
                    For k = 1 To n
                        PrintLine(1, Space(5), "")
                        lin = lin + 1
                    Next k
                    PrintLine(1, TAB(66), "")
                    lin = lin + 1
                    PrintLine(1, TAB(66), "")
                    lin = lin + 1
                    PrintLine(1, TAB(20), "Continue........", SPC(1), PAG + 1, TAB(55 - Len(Microsoft.VisualBasic.Format(rccmtotqty, "#######0"))), Microsoft.VisualBasic.Format(rccmtotqty, "#######0"), TAB(65 - Len(Microsoft.VisualBasic.Format(rccmbox, "#######0.00"))), Microsoft.VisualBasic.Format(rccmbox, "#######0.00"))
                    lin = lin + 1

                    n = 72 - lin
                    For k = 1 To n
                        PrintLine(1, Space(5), "")
                        lin = lin + 1
                    Next k

                    lin = 0
                    PAG = PAG + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, TAB(60), DateTime.Now.ToString())
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, TAB(5), "M/s.", SPC(1), Chr(27) + Chr(69) + RCCDRP.Item("CardFName").ToString + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, TAB(5), RCCDRP.Item("BUILDING").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(5), RCCDRP.Item("BLOCK").ToString, TAB(60), Chr(27) + Chr(63) + Chr(18), TAB(70), (RCCDRP("U_remarks").ToString), SPC(1), "", SPC(1), (RCCDRP("docnum").ToString))
                    lin = lin + 1
                    PrintLine(1, TAB(5), RCCDRP.Item("STREET").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(5), RCCDRP.Item("CITY").ToString)
                    lin = lin + 1
                    PrintLine(1, TAB(5), RCCDRP.Item("county").ToString, SPC(1), "Dt.", SPC(1), "(", RCCDRP.Item("state1").ToString, SPC(1), ")", TAB(63), SPC(1), Microsoft.VisualBasic.FormatDateTime(RCCDRP("docdate"), DateFormat.ShortDate))
                    lin = lin + 1
                    PrintLine(1, TAB(5), "TIN NO.:", SPC(1), RCCDRP.Item("TINNO").ToString)
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, TAB(12), (RCCDRP("u_arcode").ToString), TAB(25), SPC(1), (RCCDRP("u_orderby").ToString), TAB(44), SPC(1), Microsoft.VisualBasic.FormatDateTime(RCCDRP("docdate"), DateFormat.ShortDate), TAB(70), SPC(1), Chr(27) + Chr(69) + (RCCDRP("docnum").ToString), SPC(1), "/", SPC(1), (RCCDRP("U_Noofbun").ToString) + Chr(27) + Chr(70) + Chr(18))
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, TAB(12), Chr(27) + Chr(69) + (RCCDRP("u_transport").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(70), SPC(1), Chr(27) + Chr(69) + (RCCDRP("u_dsnation").ToString))
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, TAB(25), (RCCDRP("u_docthrough")), TAB(65), SPC(1), Chr(27) + Chr(69) + (RCCDRP("U_BRAND").ToString))
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                End If
            End While
            RCCDRP.Close()
            con.Close()
            con.Close()

            n = 59 - lin
            For k = 1 To n
                PrintLine(1, Space(5), "")
                lin = lin + 1
            Next k


            RCCTSQL = "SELECT isnull(Esugam,'') Esugam,discprcnt,SUM(BeforeDisc) BeforeDisc,DiscAmt,MAX(SchemeDiscPercent)SchemeDiscPercent,[FORWARDING CHARGES] AS FORWARDINGCHARGES,DiscSum,sum(SchemeDiscAmt) SchemeDiscAmt ," & vbCrLf _
    & "(((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES] ) ttl1,VatSum,RoundDif, (((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES] + VatSum + RoundDif ) GRANDTOTAL" & vbCrLf _
    & ",taxcode,CASE WHEN TransCat = 'Form C' THEN '[Against C Form]' ELSE ' ' END  CFROM,CASE WHEN TransCat = 'Form H' THEN 'TAX EXEMPTED AGAINST FORM H' ELSE ' ' END  HFROM,sum(U_NoofPiece)U_NoofPiece,SUM(Quantity) QTY,SUM(box)  BOX" & vbCrLf _
    & "FROM (SELECT (b.Quantity*b.PriceBefDi) BeforeDisc,isnull(G.[Disc Amt],0) DiscAmt ,ISNULL(S.[FORWARDING CHARGES],isnull(fr.LineTotal,0)) [FORWARDING CHARGES]," & vbCrLf _
    & "case when c.State = 'KA' Then 'e-sugam no.' + a.U_ESugam when c.State in ('AP','TS') Then 'e-wayBill no.' + a.U_ESugam  when c.State in ('KL') Then 'e-Token No.' + a.U_ESugam  else '' end Esugam," & vbCrLf _
    & "a.discprcnt,a.DiscSum,A.VatSum,A.RoundDif,B.TaxCode,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt',C.TransCat,b.U_NoofPiece,B.Quantity,(b.Volume) Box,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent'" & vbCrLf _
    & "FROM " & TABLE & " A JOIN " & TABLE1 & " B ON B.DocEntry = A.DocEntry" & vbCrLf _
    & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry,sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from " & TABLE1 & " where DiscPrcnt=100 group by DocEntry) G on G.DocEntry=B.DocEntry" & vbCrLf _
    & "left OUTER join (Select ItemCode,sum(Price) [FORWARDING CHARGES],DocEntry from " & TABLE1 & " WHERE ItemCode='FrEightCharges' group by  ItemCode,DocEntry) S ON S.DocEntry=B.DocEntry  " & vbCrLf _
    & "left join OITM as itm on itm.ItemCode = b.ItemCode" & vbCrLf _
    & "Left join " & TABLE12 & " as c on c.DocEntry = b.DocEntry" & vbCrLf _
    & "Left join " & TABLE3 & " as fr on fr.DocEntry = b.DocEntry" & vbCrLf _
    & "WHERE A.docnum = (" & No.Text & ") and   a.PIndicator in ('" & Year.Text & "') and b.TreeType <> 'I' and b.ItemCode<>'FrEightCharges'  ) k GROUP BY DiscAmt,[FORWARDING CHARGES],DiscSum,VatSum,RoundDif,taxcode,TransCat,Esugam,discprcnt"


            Dim CMD As New SqlCommand(RCCTSQL, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim RCCRP As SqlDataReader
            RCCRP = CMD.ExecuteReader
            RCCRP.Read()
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, Space(5), "")
            lin = lin + 1
            PrintLine(1, TAB(23), Chr(27) + Chr(69), TAB(2), "", TAB(55 - Len(Microsoft.VisualBasic.Format(RCCRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(RCCRP("U_NoofPiece"), "#######0"), TAB(65 - Len(Microsoft.VisualBasic.Format(RCCRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("box"), "#######0.00"), Chr(27) + Chr(70) + Chr(18))
            lin = lin + 1
            PrintLine(1, TAB(23), Chr(27) + Chr(69), TAB(2), "")
            lin = lin + 1
            n = 72 - lin
            For k = 1 To n
                ' PrintLine(1, Space(5), "")
            Next k

            lin = 0
            mbox = 0
            mmtr = 0
            mtotqty = 0
            mtotamt = 0



            FileClose(1)
            RCCRP.Close()
            con.Close()
            con.Close()

            ''  Shell("print /d:LPT" & lpt.Text & " " + dbreportpath + "RCClorry.txt", vbNormalFocus)
            ''Shell("command.com /c TYPE " & " " + dbreportpath + "RCCDELIVERY.txt>PRN", AppWinStyle.Hide)
            ''   Shell("print /d:LPT" & lpt.Text & " dbreportpath +"RRlorry.txt", vbNormalFocus)
            'Shell("print /d:LPT" & lpt.Text & " " & " " + dbreportpath + "RCCDELIVERY.txt", vbNormalFocus)
            Dim printer As String = mlsprinter  'laserprinter name
            'Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "RCCDELIVERY.txt"
            Dim success As Boolean = PrintTextFile(filePathname)

        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text = "Sales" Then
            TABLE = "OINV"
            TABLE1 = "INV1"
            TABLE2 = "INV2"
            TABLE3 = "INV3"
            TABLE4 = "INV4"
            TABLE5 = "INV5"
            TABLE6 = "INV6"
            TABLE7 = "INV7"
            TABLE8 = "INV8"
            TABLE9 = "INV9"
            TABLE10 = "INV10"
            TABLE11 = "INV11"
            TABLE12 = "INV12"
            TABLE13 = "INV13"
            TABLE14 = "INV14"
            TABLE15 = "INV15"
            TABLE16 = "INV16"
            TABLE17 = "INV17"
            TABLE18 = "INV18"
            TABLE19 = "INV19"
            TABLE20 = "INV20"
            TABLE21 = "INV21"
            TABLE22 = "INV21"
            TABLE23 = "RINV7"
            TABLE24 = "RINV8"

        ElseIf ComboBox1.Text = "Delivery" Then
            TABLE = "ODLN"
            TABLE1 = "DLN1"
            TABLE2 = "DLN2"
            TABLE3 = "DLN3"
            TABLE4 = "DLN4"
            TABLE5 = "DLN5"
            TABLE6 = "DLN6"
            TABLE7 = "DLN7"
            TABLE8 = "DLN8"
            TABLE9 = "DLN9"
            TABLE10 = "DLN10"
            TABLE11 = "DLN11"
            TABLE12 = "DLN12"
            TABLE13 = "DLN13"
            TABLE14 = "DLN14"
            TABLE15 = "DLN15"
            TABLE16 = "DLN16"
            TABLE17 = "DLN17"
            TABLE18 = "DLN18"
            TABLE19 = "DLN19"
            TABLE20 = "DLN20"
            TABLE21 = "DLN21"
            TABLE22 = "DLN21"
            TABLE23 = "RDLN7"
            TABLE24 = "RDLN8"



         
        ElseIf ComboBox1.Text = "Order" Then
            TABLE = "ORDR"
            TABLE1 = "RDR1"
            TABLE2 = "RDR2"
            TABLE3 = "RDR3"
            TABLE4 = "RDR4"
            TABLE5 = "RDR5"
            TABLE6 = "RDR6"
            TABLE7 = "RDR7"
            TABLE8 = "RDR8"
            TABLE9 = "RDR9"
            TABLE10 = "RDR10"
            TABLE11 = "RDR11"
            TABLE12 = "RDR12"
            TABLE13 = "RDR13"
            TABLE14 = "RDR14"
            TABLE15 = "RDR15"
            TABLE16 = "RDR16"
            TABLE17 = "RDR17"
            TABLE18 = "RDR18"
            TABLE19 = "RDR19"
            TABLE20 = "RDR20"
            TABLE21 = "RDR21"
            TABLE22 = "RDR21"

        End If
   

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'live
        If cmp.Text = "RR" Then
            Dim strHostName As String
            Dim strIPAddress As String
            strHostName = System.Net.Dns.GetHostName()
            strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(0).ToString()

            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt As New ReportDocument()
            ''cryRpt.Load(Trim(dbreportpath) & Trim(DBTRANS))

            'cryptfile = loadrptdb2(Trim(DBTRANS), Trim(dbreportpath))
            'cryRpt.Load(cryptfile)

            'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))

            'cryRpt.SetParameterValue("Dockey@", Val(Label7.Text))
            ''cryRpt.PrintToPrinter(3, True, 1, 1)
            ''Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.ReportSource = cryRpt
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt.Refresh()

            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(Label7.Text))
            ' paramDict("Dockey@") = Val(Label7.Text)


            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = DBTRANS,
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

        ElseIf cmp.Text = "RHL" Then
            Dim strHostName As String
            Dim strIPAddress As String
            strHostName = System.Net.Dns.GetHostName()
            strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(0).ToString()


            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt As New ReportDocument()
            ''            cryRpt.Load(Trim(dbreportpath) & Trim(DBTRANS))

            'cryptfile = loadrptdb2(Trim(DBTRANS), Trim(dbreportpath))
            'cryRpt.Load(cryptfile)

            'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))

            'cryRpt.SetParameterValue("Dockey@", Val(Label7.Text))
            '' cryRpt.SetParameterValue("USERCODE@", (strHostName))
            'Me.CrystalReportViewer1.ReportSource = cryRpt
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt.Refresh()

            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(Label7.Text))
            ' paramDict("Dockey@") = Val(Label7.Text)

            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = DBTRANS,
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

        ElseIf cmp.Text = "ATC" Then
            Dim strHostName As String
            Dim strIPAddress As String
            strHostName = System.Net.Dns.GetHostName()
            strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(0).ToString()


            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt As New ReportDocument()
            ''            cryRpt.Load(Trim(dbreportpath) & Trim(DBTRANS))

            'cryptfile = loadrptdb2(Trim(DBTRANS), Trim(dbreportpath))
            'cryRpt.Load(cryptfile)

            'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))

            'cryRpt.SetParameterValue("Dockey@", Val(Label7.Text))
            '' cryRpt.SetParameterValue("USERCODE@", (strHostName))
            'Me.CrystalReportViewer1.ReportSource = cryRpt
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt.Refresh()

            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(Label7.Text))

            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = DBTRANS,
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

        ElseIf cmp.Text = "VT" Then
            Dim strHostName As String
            Dim strIPAddress As String
            strHostName = System.Net.Dns.GetHostName()
            strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(0).ToString()


            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt As New ReportDocument()
            ''            cryRpt.Load(Trim(dbreportpath) & Trim(DBTRANS))

            'cryptfile = loadrptdb2(Trim(DBTRANS), Trim(dbreportpath))
            'cryRpt.Load(cryptfile)

            'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))

            'cryRpt.SetParameterValue("Dockey@", Val(Label7.Text))
            '' cryRpt.SetParameterValue("USERCODE@", (strHostName))
            'Me.CrystalReportViewer1.ReportSource = cryRpt
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt.Refresh()
            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(Label7.Text))
            ' paramDict("Dockey@") = Val(Label7.Text)

            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = DBTRANS,
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



        ElseIf cmp.Text = "RCC" Then
            Dim strHostName As String
            Dim strIPAddress As String
            strHostName = System.Net.Dns.GetHostName()
            strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(0).ToString()


            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt As New ReportDocument()
            ''            cryRpt.Load(Trim(dbreportpath) & Trim(DBTRANS))

            'cryptfile = loadrptdb2(Trim(DBTRANS), Trim(dbreportpath))
            'cryRpt.Load(cryptfile)

            'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))

            'cryRpt.SetParameterValue("Dockey@", Val(Label7.Text))
            ''cryRpt.SetParameterValue("USERCODE@", (strHostName))
            'Me.CrystalReportViewer1.ReportSource = cryRpt
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt.Refresh()
            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(Label7.Text))
            ' paramDict("Dockey@") = Val(Label7.Text)


            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = DBTRANS,
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

        ElseIf cmp.Text = "ACC" Then
            'Call MAIN()
            Me.Cursor = Cursors.WaitCursor
            'Dim cryRpt1 As New ReportDocument()
            ''cryRpt1.Load(Trim(dbreportpath) & Trim(DBTRANS))

            'cryptfile = loadrptdb2(Trim(DBTRANS), Trim(dbreportpath))
            'cryRpt1.Load(cryptfile)

            'CrystalReportLogOn(cryRpt1, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))
            'cryRpt1.SetParameterValue("Dockey@", Val(Label7.Text))
            'Me.CrystalReportViewer1.ReportSource = cryRpt1
            'Me.CrystalReportViewer1.PrintReport()
            'Me.CrystalReportViewer1.Refresh()
            'cryRpt1.Refresh()
            'cryRpt1.Dispose()
            Dim paramDict As New Dictionary(Of String, Object)
            paramDict.Add("Dockey@", Val(Label7.Text))

            ' paramDict("Dockey@") = Val(Label7.Text)

            Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

            Dim req As New PrintRequest() With {
             .ReportName = DBTRANS,
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

        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'live
        Dim strHostName As String
        Dim strIPAddress As String
        strHostName = System.Net.Dns.GetHostName()
        strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(0).ToString()

        Me.Cursor = Cursors.WaitCursor
        'Dim cryRpt As New ReportDocument()
        ''cryRpt.Load(Trim(dbreportpath) & Trim(DBTRANS))

        'cryptfile = loadrptdb2(System.Configuration.ConfigurationManager.AppSettings("RDR"), Trim(dbreportpath))
        'cryRpt.Load(cryptfile)

        'CrystalReportLogOn(cryRpt, Trim(dbmyservername), Trim(dbmydbname), Trim(dbuserid), Trim(dbmypwd))

        'cryRpt.SetParameterValue("Dockey@", Val(Label7.Text))
        ''cryRpt.PrintToPrinter(3, True, 1, 1)
        ''Me.CrystalReportViewer1.PrintReport()
        'Me.CrystalReportViewer1.ReportSource = cryRpt
        'Me.CrystalReportViewer1.PrintReport()
        'Me.CrystalReportViewer1.Refresh()
        'cryRpt.Refresh()
        Dim paramDict As New Dictionary(Of String, Object)
        paramDict.Add("Dockey@", Val(Label7.Text))


        Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

        Dim req As New PrintRequest() With {
             .ReportName = DBTRANS,
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
    End Sub
End Class
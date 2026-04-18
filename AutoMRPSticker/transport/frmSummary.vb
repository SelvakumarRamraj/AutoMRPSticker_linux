Imports System.Drawing.Printing
Imports System.IO
Imports System.Math
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.VbStrConv
Imports AxMSFlexGridLib
Imports System.Net.IPHostEntry
'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.ReportSource
'Imports CrystalDecisions.Shared
Imports System.Configuration






Public Class frmSummary
    Dim SQL As String
    Dim ISQL As String
    Dim PSQL As String
    Dim SQL1 As String
    Dim SQL2 As String
    Dim rs As String
    Dim irs As String
    Dim fd As Date
    Dim td As Date

    Dim NAME1 As String
    Dim NAME2 As String
    Dim RHLSQL, rccsql As String
    Dim RHLTSQL, rcctsql As String
    Dim TSQL As String
    Dim TSQL1 As String
    Dim TSQL2 As String
    Dim ySQL1 As String
    Dim ySQL2 As String
    Dim yrs As String
    Dim yirs As String
    Dim yfd As Date
    Dim ytd As Date
    Dim PAG As Integer
    Dim lin, n, k, LOP As Integer
    Dim mtotamt, mmtr As Double
    Dim famt As Double
    Dim mtotqty, mbox As Long
    Dim RHLPAG, rccpag As Integer
    Dim RHLlin, RHLn, rcclin, rccn As Integer
    Dim RHLmtotamt, RHLmmtr, rccmtotamt, rccmmtr As Double
    Dim RHLmtotqty, RHLmbox, rccmtotqty, rccmbox As Long

    Private Sub Summary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Call MAIN()
        checkConnection()
        Button2.Enabled = False
        Button7.Enabled = False
        rhllorry.Enabled = False






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

        If Trim(dbcomp) = "RR" Then
            Button2.Enabled = True
        ElseIf Trim(dbcomp) = "ACC" Then
            Button7.Enabled = True
        Else
            rhllorry.Enabled = True
        End If

        Label14.Text = "0"
        itemflex.Visible = False
        ' Call MAIN()
        Call Flexhead()
        Otype.Text = "Sales Invoice"
        SQL = "SELECT CARDNAME  FROM OCRD Order by Cardname"
        Dim Ps As String
        Dim cmd As New SqlCommand(SQL, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim DR As SqlDataReader
        DR = cmd.ExecuteReader
        If DR.HasRows = True Then
            While DR.Read
                Ps = DR.Item("Cardname")
                Party.Items.Add(Ps)
            End While
        End If
        DR.Close()
        cmd.Dispose()
        con.Close()
        con.Close()


        sql1 = "select distinct U_AreaCode areacode from OCRD where U_AreaCode is not null order by U_AreaCode"
        Dim AC As String
        Dim CMDA As New SqlCommand(sql1, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim DR5 As SqlDataReader
        DR5 = CMDA.ExecuteReader
        If DR5.HasRows = True Then
            While DR5.Read
                AC = DR5.Item("areacode")
                Area.Items.Add(AC)
            End While
        End If
        DR5.Close()
        CMDA.Dispose()
        con.Close()
        con.Close()




        SQL2 = "select isnull(u_name,'') name from [@INCM_BND1] "
        Dim BC As String
        Dim CMDB As New SqlCommand(SQL2, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim DR2 As SqlDataReader
        DR2 = CMDB.ExecuteReader
        If DR2.HasRows = True Then
            While DR2.Read
                BC = DR2.Item("NAME")
                Brand.Items.Add(BC)
            End While
        End If
        DR2.Close()
        CMDB.Dispose()
        con.Close()
        con.Close()

        Dim SQLST1 As String
        SQLST1 = "select Code as STATE from OCST "
        Dim ACST As String
        Dim CMDAST As New SqlCommand(SQLST1, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim DR1ST As SqlDataReader
        DR1ST = CMDAST.ExecuteReader
        If DR1ST.HasRows = True Then
            While DR1ST.Read
                ACST = DR1ST.Item("STATE")
                STATE.Items.Add(ACST)
            End While
        End If
        DR1ST.Close()
        CMDAST.Dispose()
        con.Close()
        con.Close()

        FromDate.Value = DateTime.Now
        FromDate.Value = Now.Day & "-" & Now.Month & "-" & Now.Year

        Year.Text = Trim(mperiod)


    End Sub

    Private Sub GRIDDETAILS()
        Label1.Text = ""
        Label2.Text = ""
        Label3.Text = ""

        If Otype.Text = "Sales Invoice" Then
            rs = "OINV"
        ElseIf Otype.Text = "Sales Order" Then
            rs = "ORDR"
        ElseIf Otype.Text = "Purchase Order" Then
            rs = "OPOR"
        ElseIf Otype.Text = "Goods Receipt" Then
            rs = "OPDN"
        ElseIf Otype.Text = "Date Order" Then
            rs = "ODLN"
        ElseIf Otype.Text = "Credit Note" Then
            rs = "ORIN"
        ElseIf Otype.Text = "Debit Note" Then
            rs = "ORPC"
        End If











        If Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(cmbGPstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(u_gpno,0)) = 0 THEN 'Non Entry' ELSE 'ENTRY' END = '" & cmbGPstatus.Text & "'  and  isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'   Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(u_gpno,0)) = 0 THEN 'Non Entry' ELSE 'ENTRY' END = '" & cmbGPstatus.Text & "' Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(u_gpno,0)) = 0 THEN 'Non Entry' ELSE 'ENTRY' END = '" & cmbGPstatus.Text & "' AND  isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'   Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  and  isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'   Order by Docentry"




        ElseIf Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'   Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(u_gpno,0)) = 0 THEN 'Non Entry' ELSE 'ENTRY' END = '" & cmbGPstatus.Text & "'    Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' and  isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'    Order by Docentry"

        ElseIf Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(u_gpno,0)) = 0 THEN 'Non Entry' ELSE 'ENTRY' END = '" & cmbGPstatus.Text & "'    Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  and  isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'    Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  AND  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(u_gpno,0)) = 0 THEN 'Non Entry' ELSE 'ENTRY' END = '" & cmbGPstatus.Text & "'   and  isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'    Order by Docentry"


        ElseIf Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(cmbGPstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'   and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(u_gpno,0)) = 0 THEN 'Non Entry' ELSE 'ENTRY' END = '" & cmbGPstatus.Text & "'   and  isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'    Order by Docentry"





        ElseIf Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "'   Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(cmblrstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'    Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "'  and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(u_gpno,0)) = 0 THEN 'Non Entry' ELSE 'ENTRY' END = '" & cmbGPstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "'   and  isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"


        ElseIf Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "'   and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "'  and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'    Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "'  and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "'    and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(u_gpno,0)) = 0 THEN 'Non Entry' ELSE 'ENTRY' END = '" & cmbGPstatus.Text & "' Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "'  and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "'   and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(u_gpno,0)) = 0 THEN 'Non Entry' ELSE 'ENTRY' END <> '' and  isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"

        ElseIf Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "'  and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'   and  isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "'  and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'    and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(u_gpno,0)) = 0 THEN 'Non Entry' ELSE 'ENTRY' END  = '" & cmbGPstatus.Text & "'  Order by Docentry"


        ElseIf Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(u_gpno,0)) = 0 THEN 'Non Entry' ELSE 'ENTRY' END = '" & cmbGPstatus.Text & "'   Order by Docentry"
        ElseIf Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "'  and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  and   CASE WHEN CONVERT(NVARCHAR(MAX),isnull(u_gpno,0)) = 0 THEN 'Non Entry' ELSE 'ENTRY' END = '" & cmbGPstatus.Text & "'  and  isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"
        ElseIf Trim(Area.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "'  and c.U_AREACODE = '" & Area.Text & "'  and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'   and  isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"

        ElseIf Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(cmbGPstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "'  and cs.state  = '" & STATE.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(u_gpno,0)) = 0 THEN 'Non Entry' ELSE 'ENTRY' END = '" & cmbGPstatus.Text & "' and  isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"




        ElseIf Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" And Trim(STATE.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' and cs.state  = '" & STATE.Text & "' Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" And Trim(cmblrstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' anD isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Area.Text) <> "" And Trim(cmblrstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and c.U_AREACODE = '" & Area.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Area.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and c.U_AREACODE  = '" & Area.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Area.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and c.U_AREACODE = '" & Area.Text & "' anD isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and cs.state  = '" & STATE.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and cs.state  = '" & STATE.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(STATE.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and cs.state  = '" & STATE.Text & "' anD isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  anD isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(cmbGPstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'  anD isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"

        ElseIf Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(cmblrstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE  = '" & Area.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.U_BRAND = '" & Brand.Text & "' and c.U_AREACODE = '" & Area.Text & "' anD isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.U_BRAND = '" & Brand.Text & "' and cs.state  = '" & STATE.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.U_BRAND = '" & Brand.Text & "' and cs.state  = '" & STATE.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(STATE.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.U_BRAND = '" & Brand.Text & "' and cs.state  = '" & STATE.Text & "' anD isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.U_BRAND = '" & Brand.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.U_BRAND = '" & Brand.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  anD isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(cmbGPstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.U_BRAND = '" & Brand.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'  anD isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"



        ElseIf Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and c.U_AREACODE = '" & Area.Text & "'   and cs.state  = '" & STATE.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and c.U_AREACODE = '" & Area.Text & "'   and cs.state  = '" & STATE.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and c.U_AREACODE = '" & Area.Text & "'  and cs.state  = '" & STATE.Text & "' anD isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"
        ElseIf Trim(Area.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and c.U_AREACODE = '" & Area.Text & "'  and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'  Order by Docentry"
        ElseIf Trim(Area.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and c.U_AREACODE = '" & Area.Text & "'  and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  anD isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"
        ElseIf Trim(Area.Text) <> "" And Trim(cmbGPstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and c.U_AREACODE = '" & Area.Text & "'  and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'  anD isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"



        ElseIf Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and cs.state  = '" & STATE.Text & "'   and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'  Order by Docentry"
        ElseIf Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and cs.state  = '" & STATE.Text & "'   and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  anD isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"
        ElseIf Trim(STATE.Text) <> "" And Trim(cmbGPstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and cs.state  = '" & STATE.Text & "'   and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'  anD isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"


        ElseIf Trim(cmblrstatus.Text) <> "" And Trim(cmbGPstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "'  and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'  and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'  anD isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'  Order by Docentry"



        ElseIf Trim(Party.Text) <> "" And Trim(Brand.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_bisnull(u_gpno,'')rand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and a.U_BRAND = '" & Brand.Text & "' Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(Area.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and c.u_areacode = '" & Area.Text & "' Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(STATE.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and cs.state  = '" & STATE.Text & "' Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(cmblrstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'   Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.u_gpno,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'   Order by Docentry"
        ElseIf Trim(Party.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' and isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'   Order by Docentry"



        ElseIf Trim(Brand.Text) <> "" And Trim(Area.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.u_brand = '" & Brand.Text & "' and c.u_areacode = '" & Area.Text & "' Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(STATE.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.u_brand = '" & Brand.Text & "' and cs.state  = '" & STATE.Text & "' Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(cmblrstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.u_brand = '" & Brand.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'   Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.u_brand = '" & Brand.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.u_gpno,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'   Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.u_brand = '" & Brand.Text & "' and isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'   Order by Docentry"




        ElseIf Trim(Area.Text) <> "" And Trim(STATE.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and c.u_areacode = '" & Area.Text & "' and cs.state  = '" & STATE.Text & "' Order by Docentry"
        ElseIf Trim(Area.Text) <> "" And Trim(cmblrstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and c.u_areacode = '" & Area.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'   Order by Docentry"
        ElseIf Trim(Area.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and c.u_areacode = '" & Area.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.u_gpno,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'   Order by Docentry"
        ElseIf Trim(Area.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and c.u_areacode = '" & Area.Text & "' and isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'   Order by Docentry"



        ElseIf Trim(STATE.Text) <> "" And Trim(cmblrstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and cs.state  = '" & STATE.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'   Order by Docentry"
        ElseIf Trim(STATE.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and cs.state  = '" & STATE.Text & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.u_gpno,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'   Order by Docentry"
        ElseIf Trim(STATE.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and cs.state  = '" & STATE.Text & "'  and isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'   Order by Docentry"


        ElseIf Trim(cmblrstatus.Text) <> "" And Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'   and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.u_gpno,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'   Order by Docentry"
        ElseIf Trim(cmblrstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'   and isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'   Order by Docentry"


        ElseIf Trim(cmbGPstatus.Text) <> "" And Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_gpNo,'0')) = '0' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmbGPstatus.Text & "'   and isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'   Order by Docentry"




        ElseIf Trim(Area.Text) <> "" Then
            SQL = "select isnull(u_gpno,'')  as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and c.u_areacode = '" & Area.Text & "' Order by Docentry"
        ElseIf Trim(Party.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'') U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif   from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and a.CARDNAME = '" & Party.Text & "' Order by Docentry"
        ElseIf Trim(Brand.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'')U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif   from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and u_brand = '" & Brand.Text & "' Order by Docentry"
        ElseIf Trim(STATE.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats',CASE WHEN C.U_AREACODE = 'TN1' THEN 'SR' WHEN lEFT(C.U_AREACODE,2) = 'TN' THEN 'TN'  ELSE 'OS' END STATE , b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'')U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif   from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and cs.state  = '" & STATE.Text & "' Order by Docentry"
        ElseIf Trim(cmblrstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats',CASE WHEN C.U_AREACODE = 'TN1' THEN 'SR' WHEN lEFT(C.U_AREACODE,2) = 'TN' THEN 'TN'  ELSE 'OS' END STATE , b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'')U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif   from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(A.U_LRNo,'')) = '' THEN 'Non Entry' ELSE  'ENTRY' END = '" & cmblrstatus.Text & "'   Order by Docentry"
        ElseIf Trim(cmbGPstatus.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats',CASE WHEN C.U_AREACODE = 'TN1' THEN 'SR' WHEN lEFT(C.U_AREACODE,2) = 'TN' THEN 'TN'  ELSE 'OS' END STATE , b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'')U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif   from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and  CASE WHEN CONVERT(NVARCHAR(MAX),isnull(u_gpno,0)) = 0 THEN 'Non Entry' ELSE 'ENTRY' END = '" & cmbGPstatus.Text & "'   Order by Docentry"
        ElseIf Trim(CMBTRANSPORT.Text) <> "" Then
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats',CASE WHEN C.U_AREACODE = 'TN1' THEN 'SR' WHEN lEFT(C.U_AREACODE,2) = 'TN' THEN 'TN'  ELSE 'OS' END STATE , b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'')U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif   from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Left join (select distinct cardcode,state from crd1) cs on cs.cardcode= a.cardcode Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and  isnull(A.U_Transport,'') LIKE '%' + '" & CMBTRANSPORT.Text & "'+'%'   Order by Docentry"
        Else
            SQL = "select Confirmed,isnull(u_gpno,'') as 'Stats', b.u_remarks as u_brand,A.docnum,A.docentry,A.docdate,A.cardname,CONVERT(DECIMAL(19,2),A.doctotal) dOCTOTAL,isnull(CONVERT(DECIMAL(19,2),A.U_TotQty),0) U_TotQty ,isnull(CONVERT(DECIMAL(19,2),A.U_TotMTRS),0) U_TotMTRS ,isnull(A.U_LRNo,0) U_LRNo,isnull(A.U_Lrdat,'') U_Lrdat ,isnull(A.U_Transport,'') U_Transport ,isnull(A.U_Noofbun,0)U_Noofbun ,isnull(A.U_courpodno,0) U_courpodno ,isnull(C.U_AreaCode,'')U_AreaCode ,isnull(CONVERT(DECIMAL(19,2),A.U_Dis1),0) U_Dis1,isnull(CONVERT(DECIMAL(19,2),A.VatSum),0) VatSum,isnull(A.U_TaxCode,0) U_TaxCode ,isnull(CONVERT(DECIMAL(19,2),A.RoundDif),0) RoundDif   from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand LEFT JOIN OCRD C ON C.CARDCODE = A.CARDCODE Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' Order by Docentry"
        End If
        '        SQL = "select '' as 'Stats', b.u_remarks as u_brand,docnum,docentry,docdate,cardname,doctotal,isnull(U_TotQty,0) U_TotQty ,isnull(U_TotMTRS,0) U_TotMTRS ,isnull(U_LRNo,0) U_LRNo,isnull(U_Lrdat,'') U_Lrdat ,isnull(U_Transport,'') U_Transport ,isnull(U_Noofbun,0)U_Noofbun ,isnull(U_courpodno,0) U_courpodno ,isnull(U_AreaCode,'')U_AreaCode ,isnull(U_Dis1,0) U_Dis1,isnull(VatSum,0) VatSum,isnull(U_TaxCode,0) U_TaxCode ,isnull(RoundDif,0)RoundDif  from " & rs & " A  left join [@INCM_BND1] b  on b.u_name = a.U_brand Where docdate >= '" & FromDate.Value.ToString("yyyy-MM-dd") & "' and docdate <= '" & Todate.Value.ToString("yyyy-MM-dd") & "' and cardname = '" & Party.Text & "' Order by Docentry"



        Dim CMD As New SqlCommand(SQL, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        flex.Clear()
        Call Flexhead()
        Try

            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                With flex
                    While DR.Read
                        .Rows = .Rows + 1
                        .Row = .Rows - 1

                        .set_TextMatrix(.Row, 0, DR.Item("docentry"))
                        .set_TextMatrix(.Row, 2, DR.Item("U_BRAND") & "-" & DR.Item("docnum"))
                        .set_TextMatrix(.Row, 3, DR.Item("docdate"))
                        .set_TextMatrix(.Row, 4, DR.Item("cardname"))
                        .set_TextMatrix(.Row, 5, DR.Item("doctotal"))
                        .set_TextMatrix(.Row, 6, DR.Item("u_totqty"))
                        .set_TextMatrix(.Row, 7, DR.Item("U_TotMTRS"))
                        .set_TextMatrix(.Row, 8, DR.Item("U_LRNo"))
                        .set_TextMatrix(.Row, 9, DR.Item("U_Lrdat"))
                        .set_TextMatrix(.Row, 10, DR.Item("U_Transport"))
                        .set_TextMatrix(.Row, 11, DR.Item("U_Noofbun"))
                        .set_TextMatrix(.Row, 12, DR.Item("U_courpodno"))
                        .set_TextMatrix(.Row, 13, DR.Item("U_AreaCode"))
                        .set_TextMatrix(.Row, 14, DR.Item("U_Dis1"))
                        .set_TextMatrix(.Row, 15, DR.Item("VatSum"))
                        .set_TextMatrix(.Row, 16, DR.Item("U_TaxCode"))
                        .set_TextMatrix(.Row, 17, DR.Item("RoundDif"))
                        .set_TextMatrix(.Row, 18, DR.Item("Stats"))
                        .set_TextMatrix(.Row, 19, DR.Item("Confirmed"))








                        If dbcomp = "ACC" Then
                            Label1.Visible = False
                            Label2.Visible = False
                            Label3.Visible = False

                        Else
                            Label1.Text = Val(Label1.Text) + 1
                            Label2.Text = Val(Label2.Text) + flex.get_TextMatrix(.Row, 5)
                            Label3.Text = Val(Label3.Text) + flex.get_TextMatrix(.Row, 6)
                        End If
                    End While
                End With
            End If
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            flex.Clear()

            Call Flexhead()






        End Try
        CMD.Dispose()
        CMD.Cancel()
        con.Close()
        con.Close()


    End Sub
    Private Sub Flexhead()
        flex.Rows = 1
        flex.Cols = 20
        flex.set_ColWidth(1, 400)
        flex.set_ColWidth(0, 600)
        flex.set_ColWidth(2, 1000)
        flex.set_ColWidth(3, 1000)
        flex.set_ColWidth(4, 3500)
        flex.set_ColWidth(5, 1000)
        flex.set_ColWidth(6, 1000)
        flex.set_ColWidth(7, 1000)
        flex.set_ColWidth(8, 1000)
        flex.set_ColWidth(9, 1000)
        flex.set_ColWidth(10, 1500)
        flex.set_ColWidth(11, 500)
        flex.set_ColWidth(12, 1000)
        flex.set_ColWidth(13, 500)
        flex.set_ColWidth(14, 500)
        flex.set_ColWidth(15, 1000)
        flex.set_ColWidth(16, 500)
        flex.set_ColWidth(17, 1000)
        flex.set_ColWidth(18, 1000)
        flex.set_ColWidth(19, 1000)


        flex.set_TextMatrix(0, 0, ("docentry"))
        flex.set_TextMatrix(0, 1, ("SELECT"))
        flex.set_TextMatrix(0, 2, ("docnum"))
        flex.set_TextMatrix(0, 3, ("docdate"))
        flex.set_TextMatrix(0, 4, ("cardname"))
        flex.set_TextMatrix(0, 5, ("doctotal"))
        flex.set_TextMatrix(0, 6, ("totqty"))
        flex.set_TextMatrix(0, 7, ("TotMTRS"))
        flex.set_TextMatrix(0, 8, ("LRNo"))
        flex.set_TextMatrix(0, 9, ("Lrdate"))
        flex.set_TextMatrix(0, 10, ("Transport"))
        flex.set_TextMatrix(0, 11, ("Noofbun"))
        flex.set_TextMatrix(0, 12, ("courpodno"))
        flex.set_TextMatrix(0, 13, ("AreaCode"))
        flex.set_TextMatrix(0, 14, ("Discount"))
        flex.set_TextMatrix(0, 15, ("VatSum"))
        flex.set_TextMatrix(0, 16, ("TaxCode"))
        flex.set_TextMatrix(0, 17, ("RoundDif"))
        flex.set_TextMatrix(0, 18, ("Status"))
        flex.set_TextMatrix(0, 19, ("Confirmed"))




    End Sub

    Private Sub frm1Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles frm1Refresh.Click

        Call GRIDDETAILS()


        LOP = 0
        Label14.Text = 0
    End Sub





    Private Sub Flex_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles flex.DblClick

        itemflex.Visible = True
        Label1.Visible = False
        Label2.Visible = False
        Label3.Visible = False

        Label10.Visible = True
        Label11.Visible = True
        Label12.Visible = True
        Label13.Visible = True

        Label10.Text = ""
        Label11.Text = ""
        Label12.Text = ""
        Label13.Text = ""

        If Otype.Text = "Sales Invoice" Then
            irs = "inv1"
        ElseIf Otype.Text = "Sales Order" Then
            irs = "RDR1"
        ElseIf Otype.Text = "Purchase Order" Then
            irs = "POR1"
        ElseIf Otype.Text = "Goods Receipt" Then
            irs = "PDN1"
        ElseIf Otype.Text = "DATE ORDER" Then
            irs = "DLN1"
        End If


        ISQL = " select docentry,docdate,isnull(U_CatalogName,'') U_CatalogName,Dscription,u_size,isnull(u_style,'') u_style ,CONVERT(DECIMAL(19,2),Quantity) Quantity ,CONVERT(DECIMAL(19,2),OpenQty) OpenQty ,CONVERT(DECIMAL(19,2),(Quantity - OpenQty )) balance from " & rs & " where Treetype <> 'I' and  Dscription <> 'FrightCharges' and  docentry  =  " & flex.get_TextMatrix(flex.Row, 0)




        Dim iCMD As New SqlCommand(ISQL, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        itemflex.Clear()

        itemflex.Rows = 1
        itemflex.Cols = 9
        itemflex.set_ColWidth(0, 600)
        itemflex.set_ColWidth(1, 1000)
        itemflex.set_ColWidth(2, 3000)
        itemflex.set_ColWidth(3, 3000)
        itemflex.set_ColWidth(4, 1000)
        itemflex.set_ColWidth(5, 1000)
        itemflex.set_ColWidth(6, 1000)
        itemflex.set_ColWidth(7, 1000)
        itemflex.set_ColWidth(8, 1000)


        itemflex.set_TextMatrix(0, 0, ("Docentry"))
        itemflex.set_TextMatrix(0, 1, ("Docdate"))
        itemflex.set_TextMatrix(0, 2, ("CatalogName"))
        itemflex.set_TextMatrix(0, 3, ("Dscription"))
        itemflex.set_TextMatrix(0, 4, ("Size"))
        itemflex.set_TextMatrix(0, 5, ("Style"))
        itemflex.set_TextMatrix(0, 6, ("Quantity"))
        itemflex.set_TextMatrix(0, 7, ("OpenQty"))
        itemflex.set_TextMatrix(0, 8, ("Balance"))



        Try

            Dim iDR As SqlDataReader
            iDR = iCMD.ExecuteReader
            If iDR.HasRows = True Then
                With itemflex
                    While iDR.Read
                        .Rows = .Rows + 1
                        .Row = .Rows - 1
                        .set_TextMatrix(.Row, 0, iDR.Item("docentry"))
                        .set_TextMatrix(.Row, 1, iDR.Item("docdate"))
                        .set_TextMatrix(.Row, 2, iDR.Item("U_CatalogName"))
                        .set_TextMatrix(.Row, 3, iDR.Item("Dscription"))
                        .set_TextMatrix(.Row, 4, iDR.Item("u_size"))
                        .set_TextMatrix(.Row, 5, iDR.Item("u_style"))
                        .set_TextMatrix(.Row, 6, iDR.Item("Quantity"))
                        .set_TextMatrix(.Row, 7, iDR.Item("OpenQty"))
                        .set_TextMatrix(.Row, 8, iDR.Item("balance"))


                        Label13.Text = Val(Label13.Text) + 1
                        Label10.Text = Val(Label10.Text) + itemflex.get_TextMatrix(.Row, 6)
                        Label11.Text = Val(Label11.Text) + itemflex.get_TextMatrix(.Row, 7)
                        Label12.Text = Val(Label12.Text) + itemflex.get_TextMatrix(.Row, 8)

                    End While
                End With
            End If
            iDR.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            itemflex.Clear()


            itemflex.Rows = 1
            itemflex.Cols = 9
            itemflex.set_ColWidth(0, 600)
            itemflex.set_ColWidth(1, 1000)
            itemflex.set_ColWidth(2, 3000)
            itemflex.set_ColWidth(3, 3000)
            itemflex.set_ColWidth(4, 1000)
            itemflex.set_ColWidth(5, 1000)
            itemflex.set_ColWidth(6, 1000)
            itemflex.set_ColWidth(7, 1000)
            itemflex.set_ColWidth(8, 1000)


            itemflex.set_TextMatrix(0, 0, ("Docentry"))
            itemflex.set_TextMatrix(0, 1, ("Docdate"))
            itemflex.set_TextMatrix(0, 2, ("CatalogName"))
            itemflex.set_TextMatrix(0, 3, ("Dscription"))
            itemflex.set_TextMatrix(0, 4, ("Size"))
            itemflex.set_TextMatrix(0, 5, ("Style"))
            itemflex.set_TextMatrix(0, 6, ("Quantity"))
            itemflex.set_TextMatrix(0, 7, ("Balance"))
            itemflex.set_TextMatrix(0, 8, ("Del.Qty"))





        End Try

        iCMD.Dispose()
        con.Close()
        con.Close()
        itemflex.Focus()

    End Sub


    Private Sub Flex_KeyPressEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyPressEvent) Handles flex.KeyPressEvent
        If e.keyAscii = Keys.Enter Then
            itemflex.Visible = True
        End If
        If e.keyAscii = Keys.Space Then
            flex.get_RowData(1)
            If flex.Row > 0 Then
                If Len(Trim(flex.get_TextMatrix(flex.Row, 1))) = 0 Then
                    flex.Col = 1
                    flex.CellFontName = "WinGdings"
                    flex.CellFontSize = 14
                    flex.CellAlignment = 4
                    flex.CellFontBold = True
                    flex.CellForeColor = Color.Red
                    flex.Text = Chr(252)
                    Label10.Text = ""
                    Label11.Text = ""
                    Label12.Text = ""
                    If Len(Trim(flex.get_TextMatrix(flex.Row, 1))) > 0 Then
                        Label14.Text = Label14.Text + 1
                    End If

                Else
                    flex.Col = 1
                    flex.Text = ""
                    'Label14.Text = ""
                End If
            End If
            If Len(Trim(flex.get_TextMatrix(flex.Row, 1))) <= 0 Then
                Label14.Text = Label14.Text - 1
            End If
        End If



        Label1.Visible = False
        Label2.Visible = False
        Label3.Visible = False

        Label10.Visible = True
        Label11.Visible = True
        Label12.Visible = True
        Label13.Visible = True

        Label10.Text = ""
        Label11.Text = ""
        Label12.Text = ""
        Label13.Text = ""

        If Otype.Text = "Sales Invoice" Then
            irs = "inv1"
        ElseIf Otype.Text = "Sales Order" Then
            irs = "RDR1"
        ElseIf Otype.Text = "Purchase Order" Then
            irs = "POR1"
        ElseIf Otype.Text = "Goods Receipt" Then
            irs = "PDN1"
        ElseIf Otype.Text = "DATE ORDER" Then
            irs = "DLN1"
        End If


        ISQL = " select docentry,docdate,isnull(U_CatalogName,'') U_CatalogName,Dscription,u_size,isnull(u_style,'') u_style ,CONVERT(DECIMAL(19,2),Quantity) Quantity ,CONVERT(DECIMAL(19,2),OpenQty) OpenQty ,CONVERT(DECIMAL(19,2),(Quantity - OpenQty )) balance from  " & irs & "  where Treetype <> 'I' and  Dscription <> 'FrightCharges' and  docentry  =  " & flex.get_TextMatrix(flex.Row, 0)




        Dim iCMD As New SqlCommand(ISQL, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        itemflex.Clear()

        itemflex.Rows = 1
        itemflex.Cols = 9
        itemflex.set_ColWidth(0, 600)
        itemflex.set_ColWidth(1, 1000)
        itemflex.set_ColWidth(2, 3000)
        itemflex.set_ColWidth(3, 3000)
        itemflex.set_ColWidth(4, 1000)
        itemflex.set_ColWidth(5, 1000)
        itemflex.set_ColWidth(6, 1000)
        itemflex.set_ColWidth(7, 1000)
        itemflex.set_ColWidth(8, 1000)


        itemflex.set_TextMatrix(0, 0, ("Docentry"))
        itemflex.set_TextMatrix(0, 1, ("Docdate"))
        itemflex.set_TextMatrix(0, 2, ("CatalogName"))
        itemflex.set_TextMatrix(0, 3, ("Dscription"))
        itemflex.set_TextMatrix(0, 4, ("Size"))
        itemflex.set_TextMatrix(0, 5, ("Style"))
        itemflex.set_TextMatrix(0, 6, ("Quantity"))
        itemflex.set_TextMatrix(0, 7, ("OpenQty"))
        itemflex.set_TextMatrix(0, 8, ("Balance"))



        Try

            Dim iDR As SqlDataReader
            iDR = iCMD.ExecuteReader
            If iDR.HasRows = True Then
                With itemflex
                    While iDR.Read
                        .Rows = .Rows + 1
                        .Row = .Rows - 1
                        .set_TextMatrix(.Row, 0, iDR.Item("docentry"))
                        .set_TextMatrix(.Row, 1, iDR.Item("docdate"))
                        .set_TextMatrix(.Row, 2, iDR.Item("U_CatalogName"))
                        .set_TextMatrix(.Row, 3, iDR.Item("Dscription"))
                        .set_TextMatrix(.Row, 4, iDR.Item("u_size"))
                        .set_TextMatrix(.Row, 5, iDR.Item("u_style"))
                        .set_TextMatrix(.Row, 6, iDR.Item("Quantity"))
                        .set_TextMatrix(.Row, 7, iDR.Item("OpenQty"))
                        .set_TextMatrix(.Row, 8, iDR.Item("balance"))


                        Label13.Text = Val(Label13.Text) + 1
                        Label10.Text = Val(Label10.Text) + itemflex.get_TextMatrix(.Row, 6)
                        Label11.Text = Val(Label11.Text) + itemflex.get_TextMatrix(.Row, 7)
                        Label12.Text = Val(Label12.Text) + itemflex.get_TextMatrix(.Row, 8)

                    End While
                End With
            End If
            iDR.Close()
            con.Close()
            con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            itemflex.Clear()


            itemflex.Rows = 1
            itemflex.Cols = 9
            itemflex.set_ColWidth(0, 600)
            itemflex.set_ColWidth(1, 1000)
            itemflex.set_ColWidth(2, 3000)
            itemflex.set_ColWidth(3, 3000)
            itemflex.set_ColWidth(4, 1000)
            itemflex.set_ColWidth(5, 1000)
            itemflex.set_ColWidth(6, 1000)
            itemflex.set_ColWidth(7, 1000)
            itemflex.set_ColWidth(8, 1000)


            itemflex.set_TextMatrix(0, 0, ("Docentry"))
            itemflex.set_TextMatrix(0, 1, ("Docdate"))
            itemflex.set_TextMatrix(0, 2, ("CatalogName"))
            itemflex.set_TextMatrix(0, 3, ("Dscription"))
            itemflex.set_TextMatrix(0, 4, ("Size"))
            itemflex.set_TextMatrix(0, 5, ("Style"))
            itemflex.set_TextMatrix(0, 6, ("Quantity"))
            itemflex.set_TextMatrix(0, 7, ("Balance"))
            itemflex.set_TextMatrix(0, 8, ("Del.Qty"))





        End Try

        iCMD.Dispose()

        con.Close()
        con.Close()
        itemflex.Focus()

    End Sub



    Private Sub ItemFlex_KeyPressEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyPressEvent) Handles itemflex.KeyPressEvent
        If e.keyAscii = Keys.Escape Then
            itemflex.Visible = False

            Label10.Visible = False
            Label11.Visible = False
            Label12.Visible = False
            Label13.Visible = False

            Label1.Visible = True
            Label2.Visible = True
            Label3.Visible = True
        End If
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles STATE.SelectedIndexChanged

    End Sub

    Private Sub cmblrstatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmblrstatus.SelectedIndexChanged

    End Sub


    Private Sub rhllorry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rhllorry.Click
        Dim i As Integer
        FileOpen(1, " " + mlinpath + "RHLlorry.txt", OpenMode.Output, OpenAccess.Write)
        For i = flex.Rows - 1 To 1 Step -1

            If Len(Trim(flex.get_TextMatrix(i, 1))) > 0 Then

                ''  If MsgBox("Next Print " & flex.get_TextMatrix(i, 2), MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                'Call MAIN()
                RHLSQL = "select CASE WHEN C.TransCat = 'FORM H' THEN 'Taxable' WHEN A.VatSum = '0' THEN 'Non  Taxable' ELSE 'Taxable' End AS TAXABLE, A.DOCENTRY,isnull(b.LineNum,'') LineNum,a.address,a.address2,A.cardname,a.U_Noofbun,r.Building,isnull(r.Block,'') Block,(r.City+'-'+ r.ZipCode) city , " & vbCrLf _
        & "CASE WHEN R.State='OS' THEN R.StreetNo WHEN l.QryGroup5='Y' THEN R.StreetNo ELSE ST.Name END AS STATE,r.State State1, " & vbCrLf _
        & "isnull(r.Street,'')Street,r.country,r.county, A.u_ESUGAM,A.docnum,a.Docdate,A.u_orderby,A.u_arcode,A.u_transport,A.u_docthrough,A.u_lrno,convert(varchar(10),A.u_lrdat,110) u_lrdat,A.u_lrwight,a.u_dsnation,a.U_lrval AS topay, " & vbCrLf _
        & "isnull(B.U_CatalogCode ,'') as item, " & vbCrLf _
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
         & "from OINV A inner join INV1 b on a.DocEntry=b.DocEntry  " & vbCrLf _
        & "left outer join OHEM D on D.empid=a.ownercode  " & vbCrLf _
        & "Left Join [@INCM_BND1] F on F.U_Name = a.U_brand  " & vbCrLf _
        & "Left join INV12 as c on c.DocEntry = b.DocEntry	 " & vbCrLf _
        & "left join OITM as itm on itm.ItemCode = b.ItemCode  " & vbCrLf _
        & "left join (SELECT * FROM [dbo].[@INS_OPLM] WHERE DocEntry IN (SELECT DocEntry FROM [@INS_PLM1] WHERE U_Remarks IS NOT NULL))  " & vbCrLf _
        & "as itb on itb.U_ItemCode =itm.ItemCode  " & vbCrLf _
        & "left join (SELECT DISTINCT CARDCODE,ISNULL(U_ActTino,'') TAXID11,ISNULL(U_ActCstno,'') TAXID1 FROM  OCRD  ) as crd on crd.CardCode = a.CardCode " & vbCrLf _
        & "left join CRD1 as r on r.CardCode = a.CardCode /*and a.paytocode = r.address*/ AND R.AdresType='B'  " & vbCrLf _
        & "Left Outer Join OCST ST ON R.State=ST.Code AND R.Country=ST.Country AND ST.Country='IN'  " & vbCrLf _
        & "left OUTER join OCRD as l on l.CardCode = a.CardCode  " & vbCrLf _
        & "left OUTER join (SELECT DISTINCT CONVERT(NVARCHAR(MAX),U_ItemName) U_ItemName ,CONVERT(NVARCHAR(MAX),U_Remarks) U_Remarks FROM [@INS_PLM1] WHERE u_lock = 'N') P on P.U_ItemName=B.U_CatalogName" & vbCrLf _
        & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry,  " & vbCrLf _
        & "sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from INV1 where DiscPrcnt=100 group by DocEntry) G  " & vbCrLf _
        & "on G.DocEntry=B.DocEntry  " & vbCrLf _
        & "left OUTER join (Select ItemCode,sum(Price) [FORWARDING CHARGES],DocEntry from INV1 WHERE ItemCode='FrEightCharges' group by ItemCode,DocEntry) S ON S.DocEntry=B.DocEntry  " & vbCrLf _
        & "where a.docentry = '" & flex.get_TextMatrix(i, 0) & "' and a.PIndicator = '" & Year.Text & "' and b.Treetype <> 'I'  and b.ItemCode<>'FrEightCharges' order by b.LineNum"


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
                '' FileOpen(1, " dbreportpath +"RHLlorry.txt", OpenMode.Output, OpenAccess.Write)
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

                RHLTSQL = "SELECT convert(nvarchar(max),k.Building) Building,convert(nvarchar(max),k.Block) Block,convert(nvarchar(max),k.City) City,convert(nvarchar(max),k.STATE) STATE,convert(nvarchar(max),k.Street) Street,convert(nvarchar(max),k.ZipCode) ZipCode,convert(nvarchar(max),k.country) country,convert(nvarchar(max),k.county) county,  QryGroup8, ('M/s. ' + Cardfname) Cardfname, isnull(Esugam,'') Esugam,discprcnt,SUM(BeforeDisc) BeforeDisc,DiscAmt,MAX(SchemeDiscPercent)SchemeDiscPercent,[FORWARDING CHARGES] AS FORWARDINGCHARGES,DiscSum,sum(SchemeDiscAmt) SchemeDiscAmt ," & vbCrLf _
        & "(((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES] ) ttl1,VatSum,RoundDif, (((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES] + VatSum + RoundDif ) GRANDTOTAL" & vbCrLf _
        & ",taxcode,CASE WHEN TransCat = 'Form C' THEN '[Against C Form]' ELSE ' ' END  CFROM,CASE WHEN TransCat = 'Form H' THEN 'TAX EXEMPTED AGAINST FORM H' ELSE ' ' END  HFROM,sum(U_NoofPiece)U_NoofPiece,SUM(Quantity) QTY,SUM(box)  BOX" & vbCrLf _
        & "FROM (SELECT  crd.QryGroup8,Cardfname, (b.Quantity*b.PriceBefDi) BeforeDisc,isnull(G.[Disc Amt],0) DiscAmt ,ISNULL(S.[FORWARDING CHARGES],isnull(fr.LineTotal,0)) [FORWARDING CHARGES]," & vbCrLf _
        & "case when c.State = 'KA' Then 'e-sugam no.' + a.U_ESugam when c.State in ('AP','TS') Then 'e-wayBill no.' + a.U_ESugam  when c.State in ('KL') Then 'e-Token No.' + a.U_ESugam  else '' end Esugam," & vbCrLf _
        & "sh.Building,isnull(sh.Block,'') Block,sh.City,CASE WHEN sh.State='OS' THEN sh.StreetNo WHEN crd.QryGroup5='Y' THEN sh.StreetNo ELSE ST.Name END AS STATE,sh.State State1,sh.ZipCode,isnull(sh.Street,'')Street,sh.country,sh.county," & vbCrLf _
        & "a.discprcnt,a.DiscSum,A.VatSum,A.RoundDif,B.TaxCode,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt',C.TransCat,b.U_NoofPiece,B.Quantity,(b.Volume) Box,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent'" & vbCrLf _
        & "FROM OINV A JOIN INV1 B ON B.DocEntry = A.DocEntry" & vbCrLf _
        & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry,sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from INV1 where DiscPrcnt=100 group by DocEntry) G on G.DocEntry=B.DocEntry" & vbCrLf _
        & "left OUTER join (Select ItemCode,sum(Price) [FORWARDING CHARGES],DocEntry from INV1 WHERE ItemCode='FrEightCharges' group by  ItemCode,DocEntry) S ON S.DocEntry=B.DocEntry  " & vbCrLf _
        & "left join OITM as itm on itm.ItemCode = b.ItemCode" & vbCrLf _
         & "left join  OCRD crd on crd.CardCode = a.CardCode" & vbCrLf _
   & "left join crd1 sh on sh.cardcode = a.cardcode AND sh.AdresType='S'" & vbCrLf _
    & "Left Outer Join OCST ST ON sh.State=ST.Code AND sh.Country=ST.Country AND ST.Country='IN'" & vbCrLf _
   & "Left join INV12 as c on c.DocEntry = b.DocEntry" & vbCrLf _
        & "Left join INV3 as fr on fr.DocEntry = b.DocEntry" & vbCrLf _
        & "where a.docentry = '" & flex.get_TextMatrix(i, 0) & "' and a.PIndicator = '" & Year.Text & "' and b.TreeType <> 'I' and b.ItemCode<>'FrEightCharges'  ) k GROUP BY  QryGroup8,Cardfname,convert(nvarchar(max),k.Building) ,convert(nvarchar(max),k.Block) ,convert(nvarchar(max),k.City) ,convert(nvarchar(max),k.STATE) ,convert(nvarchar(max),k.Street) ,convert(nvarchar(max),k.ZipCode) ,convert(nvarchar(max),k.country) ,convert(nvarchar(max),k.county) ,DiscAmt,[FORWARDING CHARGES],DiscSum,VatSum,RoundDif,taxcode,TransCat,Esugam,discprcnt"






                Dim CMD As New SqlCommand(RHLTSQL, con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                Dim RHLRP As SqlDataReader
                RHLRP = CMD.ExecuteReader
                RHLRP.Read()
                'If Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(66), "-------------")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00"))
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RHLRP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((RHLRP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("SchemeDiscAmt"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount :  ", SPC(1), Microsoft.VisualBasic.Format((RHLRP("discprcnt")), "#######0"), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'PrintLine(1, TAB(0), RHLRP("Esugam"), SPC(1), TAB(66), "-------------")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RHLRP("ttl1"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("ttl1"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("ttl1"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RHLRP("vatsum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(0), (RHLRP("HFROM")), TAB(30), "Add : ", TAB(40), (RHLRP("CFROM")), SPC(1), (RHLRP("taxcode")), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("vatsum"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("vatsum"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RHLRP("RoundDif"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Round Off :  ", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("RoundDif"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("RoundDif"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'PrintLine(1, TAB(40), "----------------------------------------")
                'lin = lin + 1
                'PrintLine(1, TAB(23), Chr(27) + Chr(69) + "Grand Total :", SPC(1), TAB(48 - Len(Microsoft.VisualBasic.Format(RHLRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(RHLRP("U_NoofPiece"), "#######0"), TAB(58 - Len(Microsoft.VisualBasic.Format(RHLRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("box"), "#######0.00"), TAB(79 - Len(Microsoft.VisualBasic.Format(RHLRP("GRANDTOTAL"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("GRANDTOTAL"), "#######0.00") + Chr(27) + Chr(70) + Chr(18))
                'lin = lin + 1
                'PrintLine(1, TAB(40), "----------------------------------------")
                'lin = lin + 1
                'PrintLine(1, Space(5), "")
                'lin = lin + 1
                'PrintLine(1, TAB(2), "", SPC(1), RupeesToWord(RHLRP("GRANDTOTAL")), SPC(1), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RHLRP("vatsum"), "#######0.00") = "0.00" Then PrintLine(1, TAB(2), "TEXTILE GOODS TAX EXEMPTED COMMODITY CODE : 794,795,796") Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'PrintLine(1, TAB(66), Chr(27) + Chr(69) + "For RAMRAJ HANDLOOMS" + Chr(27) + Chr(70) + Chr(18))


                If RHLRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(1), "Delivery Address : ", TAB(5), Space(5), "")
                ElseIf RHLRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(1), "Delivery Address : ", TAB(66), "-------------")
                ElseIf RHLRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(66), "-------------")
                Else
                    PrintLine(1, Space(5), "")
                End If
                ' If Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(66), "-------------")
                lin = lin + 1



                If RHLRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(1), RHLRP.Item("Cardfname").ToString, TAB(5), Space(5), "")
                ElseIf RHLRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(1), RHLRP.Item("Cardfname").ToString, TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00"))
                ElseIf RHLRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00"))
                Else
                    PrintLine(1, Space(5), "")
                End If

                '   If Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RHLRP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("BeforeDisc"), "#######0.00"))
                lin = lin + 1


                If RHLRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(1), RHLRP.Item("Building").ToString, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00"))
                ElseIf RHLRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00") = "0.00" Then
                    PrintLine(1, TAB(1), RHLRP.Item("Building").ToString, Space(5), "")
                ElseIf RHLRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00"))
                Else
                    PrintLine(1, Space(5), "")
                End If



                'If Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("discamt"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1


                If RHLRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(1), RHLRP.Item("BLOCK").ToString, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((RHLRP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("SchemeDiscAmt"), "#######0.00"))
                ElseIf RHLRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("SchemeDiscPercent"), "#######0.00") = "0.00" Then
                    PrintLine(1, TAB(1), RHLRP.Item("BLOCK").ToString, Space(5), "")
                ElseIf RHLRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RHLRP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((RHLRP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("SchemeDiscAmt"), "#######0.00"))
                Else
                    PrintLine(1, Space(5), "")
                End If


                ' If Microsoft.VisualBasic.Format(RHLRP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((RHLRP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("SchemeDiscAmt"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1


                If RHLRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(1), RHLRP.Item("STREET").ToString, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00"))
                ElseIf RHLRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(1), RHLRP.Item("STREET").ToString, Space(5), "")
                ElseIf RHLRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00"))
                Else
                    PrintLine(1, Space(5), "")
                End If


                ' If Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("FORWARDINGCHARGES"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1


                If RHLRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(1), RHLRP.Item("CITY").ToString, SPC(1), "-", SPC(1), RHLRP.Item("ZIPCODE").ToString, TAB(40), "Less Discount :  ", SPC(1), Microsoft.VisualBasic.Format((RHLRP("discprcnt")), "#######0"), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00"))
                ElseIf RHLRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00") = "0.00" Then
                    PrintLine(1, TAB(1), RHLRP.Item("CITY").ToString, Space(5), "")
                ElseIf RHLRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RHLRP("DiscSum"), "#######0.00") <> "0.00" Then
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
                If Microsoft.VisualBasic.Format(RHLRP("vatsum"), "#######0.00") = "0.00" Then PrintLine(1, TAB(2), "TEXTILE GOODS TAX EXEMPTED COMMODITY CODE : 794,795,796") Else PrintLine(1, Space(5), "")
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

                RHLRP.Close()
                con.Close()
                con.Close()




            End If

            '    End If


        Next i

        FileClose(1)

        'Shell("print /d:LPT" & lpt.Text & " " + dbreportpath + "RHLlorry.txt", vbNormalFocus)
        Dim printer As String = mlsprinter   'laserprinter name
        Dim filePath As String = mlinpath
        Dim filePathname As String = mlinpath & "RHLlorry.txt"
        Dim success As Boolean = PrintTextFile(filePathname)

    End Sub

    Private Sub flex_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flex.Enter

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim i As Integer

        FileOpen(1, " " + mlinpath + "Rrlorry.txt", OpenMode.Output, OpenAccess.Write)

        For i = flex.Rows - 1 To 1 Step -1
            If Len(Trim(flex.get_TextMatrix(i, 1))) > 0 Then
                LOP = LOP + 1
                ' Call MAIN()
                PSQL = "select b.LineNum, Cardfname,CASE WHEN C.TransCat = 'FORM H' THEN 'Taxable' WHEN A.VatSum = '0' THEN 'Non  Taxable' ELSE 'Taxable' End AS TAXABLE, A.DOCENTRY,b.LineNum,a.address,a.address2,A.cardname,a.U_Noofbun,r.Building,isnull(r.Block,'') Block,r.City," & vbCrLf _
        & "CASE WHEN R.State='OS' THEN R.StreetNo WHEN l.QryGroup5='Y' THEN R.StreetNo ELSE ST.Name END AS STATE,r.State State1," & vbCrLf _
        & "r.ZipCode,isnull(r.Street,'')Street,r.country,r.county, A.u_ESUGAM, A.docnum,isnull(a.Docdate,'')Docdate ,A.u_orderby,A.u_arcode,A.u_transport,(A.u_docthrough + '   ' + a.numatcard) u_docthrough ,isnull(A.u_lrno,'') u_lrno, isnull(a.u_lrdat,'') u_lrdat ,isnull(A.u_lrwight,0) u_lrwight,a.u_dsnation,a.U_lrval AS topay," & vbCrLf _
        & "RTRIM(LEFT(((CASE when l.qrygroup4='Y' THEN ISNULL(CAST(P.U_Remarks AS NVARCHAR(150))+'-','') ELSE '' END)+ isnull(itm.U_SubGrp6,'') + ' ' + ISNULL(itb.U_ItemGrp,'')),30)) as item, " & vbCrLf _
        & "b.u_style,b.u_size,b.baseref,b.price,b.linetotal,b.quantity,a.DiscSum,a.VatSum,a.RoundDif,D.firstname,f.U_remarks,b.taxcode," & vbCrLf _
        & "b.BaseREf,(b.quantity/itm.SalPackUn) Box,a.TotalExpns,c.TransCat,(isnull(crdd1.TaxId1,''))as CSTNO,(isnull(crd.TaxId11,'')) as TINNO,l.CardFName," & vbCrLf _
        & "a.U_Dis1'TRADE DISCOUNT',a.U_Dis2'CASH DISCOUNT',a.U_Dis3'CD/AD DRAFT DISC',a.U_Dis4'CD/AD/QTY DISC'" & vbCrLf _
        & ",a.U_Dis5'CD/LR-AGAINST',a.U_Dis6'QTY DISCOUNT',a.U_Dis7'SPL DISCOUNT',a.U_Dis8'VAT EXCEMPATION',a.U_Dis9'TURNOVER'" & vbCrLf _
        & ",isnull(a.U_Dis10,0)'VAT DISCOUNT',a.DiscPrcnt,Left(b.Taxcode,3) as TAXCODE,a.numatcard," & vbCrLf _
        & "RTRIM(convert(nvarchar(100),isnull(r.building,'')))+','+rtrim(convert(nvarchar(100),isnull(r.block,'')))+','+RTRIM(convert(nvarchar(100),isnull(r.Street,'')))+'-'+RTRIM(convert(nvarchar(50),isnull(r.ZipCode,'')))+','+rtrim(CONVERT(nvarchar(100),isnull(r.city,'')))+','+" & vbCrLf _
        & "RTRIM(CONVERT(nvarchar(10),isnull(r.State,'')))+','+RTRIM(CONVERT(nvarchar(50),ISNULL(r.county,'')))+','+RTRIM(CONVERT(nvarchar(10),ISNULL(r.country,''))) as address3" & vbCrLf _
        & ",Isnull(Convert(Nvarchar,P.U_Remarks)+'-'+'','') [Item Remarks],b.PriceBefDi,b.Quantity*b.PriceBefDi [Before Disc],b.LineTotal,G.DiscPrcnt [Item Disc]" & vbCrLf _
        & ",ISNULL(S.[FORWARDING CHARGES],0) [FORWARDING CHARGES],b.DiscPrcnt [Item Disc Prct]," & vbCrLf _
        & "isnull(G.[Disc Amt],0) [Disc Amt]" & vbCrLf _
        & ",CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent'" & vbCrLf _
        & ",CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt',a.cardcode " & vbCrLf _
        & " from OINV A inner join INV1 b on a.DocEntry=b.DocEntry" & vbCrLf _
        & "left outer join OHEM D on D.empid=a.ownercode" & vbCrLf _
        & "Left Join [@INCM_BND1] F on F.U_Name = a.U_brand" & vbCrLf _
        & "Left join INV12 as c on c.DocEntry = b.DocEntry	" & vbCrLf _
        & "left join OITM as itm on itm.ItemCode = b.ItemCode" & vbCrLf _
        & "left join (SELECT * FROM [dbo].[@INS_OPLM] WHERE DocEntry IN (SELECT DocEntry FROM [@INS_PLM1] WHERE U_Remarks IS NOT NULL)) " & vbCrLf _
        & "as itb on itb.U_ItemCode =itm.ItemCode" & vbCrLf _
        & "left join (SELECT DISTINCT CARDCODE,max(ISNULL(TAXID11,'')) TAXID11  FROM  CRD7  group by cardcode ) as crd on crd.CardCode = a.CardCode" & vbCrLf _
        & "left join (SELECT DISTINCT CARDCODE,max(ISNULL(TaxId1,'')) TAXID1  FROM  CRD7  group by cardcode ) as crdd1 on crdd1.CardCode = a.CardCode" & vbCrLf _
        & "left join CRD1 as r on r.CardCode = a.CardCode /*and a.paytocode = r.address*/ AND R.AdresType='B'" & vbCrLf _
        & "Left Outer Join OCST ST ON R.State=ST.Code AND R.Country=ST.Country AND ST.Country='IN'" & vbCrLf _
        & "left OUTER join OCRD as l on l.CardCode = a.CardCode " & vbCrLf _
        & "left OUTER join (SELECT * FROM [@INS_PLM1] WHERE U_Remarks IS NOT NULL) P on P.U_ItemName=B.U_CatalogName " & vbCrLf _
        & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry," & vbCrLf _
        & "sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from INV1 where DiscPrcnt=100 group by DocEntry) G " & vbCrLf _
        & "on G.DocEntry=B.DocEntry" & vbCrLf _
        & "left OUTER join (Select sum(linetotal) [FORWARDING CHARGES],DocEntry from INV3 Group by DocEntry ) S ON S.DocEntry=B.DocEntry " & vbCrLf _
        & "where a.docentry = '" & flex.get_TextMatrix(i, 0) & "' and a.PIndicator = '" & Year.Text & "' and b.Treetype <> 'I'  and b.ItemCode<>'FrightCharges' Order by b.LineNum"


                mtotqty = 0
                mtotamt = 0
                PAG = 0
                lin = 0


                Dim CMDp As New SqlCommand(PSQL, con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                Dim DRP As SqlDataReader
                DRP = CMDp.ExecuteReader
                DRP.Read()

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
                PrintLine(1, TAB(5), Chr(27) + Chr(69) + DRP.Item("Cardfname").ToString + Chr(27) + Chr(70) + Chr(18))
                lin = lin + 1
                PrintLine(1, TAB(5), (DRP("BUILDING").ToString))
                lin = lin + 1
                PrintLine(1, TAB(5), (DRP("BLOCK").ToString), TAB(51), Chr(27) + Chr(69) + (DRP.Item("U_remarks").ToString), SPC(1), "", (DRP.Item("Docnum").ToString + Chr(27) + Chr(70) + Chr(18)))
                lin = lin + 1
                PrintLine(1, TAB(5), (DRP("STREET").ToString), TAB(35), (DRP.Item("Docnum").ToString), TAB(51), (DRP("TAXABLE").ToString))
                lin = lin + 1
                PrintLine(1, TAB(5), (Trim(DRP("CITY")).ToString), SPC(1), "-", SPC(1), (Trim(DRP("ZIPCODE")).ToString), SPC(1), TAB(51), Microsoft.VisualBasic.FormatDateTime(DRP("docdate"), DateFormat.ShortDate))
                lin = lin + 1
                PrintLine(1, TAB(5), "TIN No.", SPC(1), (Trim(DRP("TINNO")).ToString))
                lin = lin + 1
                PrintLine(1, TAB(5), "CST No.", SPC(1), (Trim(DRP("CSTNO")).ToString), TAB(51), (DRP("u_arcode").ToString))
                lin = lin + 1
                PrintLine(1, Space(5), "")
                lin = lin + 1
                PrintLine(1, TAB(10), (DRP("baseref").ToString), TAB(32), (DRP("u_dsnation").ToString), TAB(56), (DRP("u_lrno").ToString), TAB(74), Microsoft.VisualBasic.Format(DRP("u_lrwight"), "#######0.000"))
                'PrintLine(1, TAB(10), (DRP("baseref").ToString), TAB(32), (DRP("u_dsnation").ToString), TAB(56), (DRP("u_lrno").ToString))
                lin = lin + 1
                PrintLine(1, TAB(10), (DRP("u_orderby").ToString), TAB(32), (DRP("u_transport").ToString))
                lin = lin + 1
                PrintLine(1, Space(5), "")
                lin = lin + 1
                PrintLine(1, TAB(18), (DRP("u_docthrough")))
                lin = lin + 1
                PrintLine(1, Space(5), "")
                lin = lin + 1
                PrintLine(1, Space(5), "")
                lin = lin + 1
                PrintLine(1, Space(5), "")
                lin = lin + 1
                PrintLine(1, Space(5), "")
                lin = lin + 1
                PrintLine(1, TAB(1), (DRP("item").ToString), TAB(33), (DRP("u_style").ToString), TAB(40), (DRP("u_size").ToString), TAB(55 - Len(Microsoft.VisualBasic.Format(DRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("PriceBefDi"), "#######0.00"), TAB(57), Microsoft.VisualBasic.Format(DRP("box"), "#######0"), TAB(65 - Len(Microsoft.VisualBasic.Format(DRP("quantity"), "#######0"))), Microsoft.VisualBasic.Format(DRP("quantity"), "#######0"), TAB(77 - Len(Microsoft.VisualBasic.Format(DRP("BEFORE DISC"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("BEFORE DISC"), "#######0.00"))
                mtotamt = mtotamt + DRP("BEFORE DISC")
                mtotqty = mtotqty + DRP("quantity")
                mbox = mbox + DRP("box")
                While DRP.Read
                    PrintLine(1, TAB(1), (DRP("item").ToString), TAB(33), (DRP("u_style").ToString), TAB(40), (DRP("u_size").ToString), TAB(55 - Len(Microsoft.VisualBasic.Format(DRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("PriceBefDi"), "#######0.00"), TAB(57), Microsoft.VisualBasic.Format(DRP("box"), "#######0"), TAB(65 - Len(Microsoft.VisualBasic.Format(DRP("quantity"), "#######0"))), Microsoft.VisualBasic.Format(DRP("quantity"), "#######0"), TAB(77 - Len(Microsoft.VisualBasic.Format(DRP("BEFORE DISC"), "#######0.00"))), Microsoft.VisualBasic.Format(DRP("BEFORE DISC"), "#######0.00"))
                    lin = lin + 1
                    mtotamt = mtotamt + DRP("BEFORE DISC")
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
                        PrintLine(1, TAB(5), Chr(27) + Chr(69) + DRP.Item("Cardfname").ToString + Chr(27) + Chr(70) + Chr(18))
                        lin = lin + 1
                        PrintLine(1, TAB(5), (DRP("BUILDING").ToString))
                        lin = lin + 1
                        PrintLine(1, TAB(5), (DRP("BLOCK").ToString), TAB(51), Chr(27) + Chr(69) + (DRP.Item("U_remarks").ToString), SPC(1), "", (DRP.Item("Docnum").ToString + Chr(27) + Chr(70) + Chr(18)))
                        lin = lin + 1
                        PrintLine(1, TAB(5), (DRP("STREET").ToString), TAB(35), (DRP.Item("Docnum").ToString), TAB(51), (DRP("TAXABLE").ToString))
                        lin = lin + 1
                        PrintLine(1, TAB(5), (Trim(DRP("CITY")).ToString), SPC(1), "-", SPC(1), (Trim(DRP("ZIPCODE")).ToString), SPC(1), TAB(51), Microsoft.VisualBasic.FormatDateTime(DRP("docdate"), DateFormat.ShortDate))
                        lin = lin + 1
                        PrintLine(1, TAB(5), "TIN No.", SPC(1), (Trim(DRP("TINNO")).ToString))
                        lin = lin + 1
                        PrintLine(1, TAB(5), "CST No.", SPC(1), (Trim(DRP("CSTNO")).ToString), TAB(51), (DRP("u_arcode").ToString))
                        lin = lin + 1
                        PrintLine(1, Space(5), "")
                        lin = lin + 1
                        PrintLine(1, TAB(10), (DRP("baseref").ToString), TAB(32), (DRP("u_dsnation").ToString), TAB(56), (DRP("u_lrno").ToString), TAB(74), Microsoft.VisualBasic.Format(DRP("u_lrwight"), "#######0.000"))
                        lin = lin + 1
                        PrintLine(1, TAB(10), (DRP("u_orderby").ToString), TAB(32), (DRP("u_transport").ToString))
                        lin = lin + 1
                        PrintLine(1, Space(5), "")
                        lin = lin + 1
                        PrintLine(1, TAB(18), (DRP("u_docthrough")))
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

                TSQL = "SELECT convert(nvarchar(max),k.Building) Building,convert(nvarchar(max),k.Block) Block,convert(nvarchar(max),k.City) City,convert(nvarchar(max),k.STATE) STATE,convert(nvarchar(max),k.Street) Street,convert(nvarchar(max),k.ZipCode) ZipCode,convert(nvarchar(max),k.country) country,convert(nvarchar(max),k.county) county,  QryGroup8, ('M/s. ' + Cardfname) Cardfname, trdis + '%  = ' + convert(nvarchar(max),sum(cast(SchemeDiscAmt as numeric(19,2)))) TRadeDiscount,isnull(Esugam,'') Esugam,SUM(BeforeDisc) BeforeDisc,DiscAmt,MAX(SchemeDiscPercent)SchemeDiscPercent,[FORWARDING CHARGES] AS FORWARDINGCHARGES,DiscSum,sum(SchemeDiscAmt) SchemeDiscAmt ," & vbCrLf _
                & "(((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum ) ttl1,VatSum,RoundDif, (((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES] + VatSum + RoundDif ) GRANDTOTAL" & vbCrLf _
                & ",taxcode,CASE WHEN TransCat = 'Form C' THEN '[Against C Form]' ELSE ' ' END  CFROM,CASE WHEN TransCat = 'Form H' THEN 'TAX EXEMPTED AGAINST FORM H' ELSE ' ' END  HFROM,SUM(Quantity) QTY,SUM(box)  BOX" & vbCrLf _
                & "FROM (SELECT  crd.QryGroup8,Cardfname,b.LineNum,(b.Quantity*b.PriceBefDi) BeforeDisc,isnull(G.[Disc Amt],0) DiscAmt ,ISNULL(S.[FORWARDING CHARGES],0) [FORWARDING CHARGES]," & vbCrLf _
                & "case when c.State = 'KA' Then 'e-sugam no.' + a.U_ESugam when c.State in ('AP','TS') Then 'e-wayBill no.' + a.U_ESugam  when c.State in ('KL') Then 'e-Token No.' + a.U_ESugam  else '' end Esugam," & vbCrLf _
                & "case when isnull(convert(nvARCHAR(MAX),crd.U_Dis1),'') <> '' then 'Trade Dsicount   ' + convert(nvarchar(max),(cast(crd.U_Dis1 as numeric(19,0))))   else '' end  trdis," & vbCrLf _
                & "sh.Building,isnull(sh.Block,'') Block,sh.City,CASE WHEN sh.State='OS' THEN sh.StreetNo WHEN crd.QryGroup5='Y' THEN sh.StreetNo ELSE ST.Name END AS STATE,sh.State State1,sh.ZipCode,isnull(sh.Street,'')Street,sh.country,sh.county," & vbCrLf _
                & "a.DiscSum,A.VatSum,A.RoundDif,B.TaxCode,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt',C.TransCat,B.Quantity,(b.Quantity/ITM.SalPackUn) Box,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent'" & vbCrLf _
                & "FROM OINV A JOIN INV1 B ON B.DocEntry = A.DocEntry" & vbCrLf _
                & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry,sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from INV1 where DiscPrcnt=100 group by DocEntry) G on G.DocEntry=B.DocEntry" & vbCrLf _
                & "left OUTER join (Select sum(linetotal) [FORWARDING CHARGES],DocEntry from INV3 Group by Docentry) S ON S.DocEntry=B.DocEntry" & vbCrLf _
                & "left join  OCRD crd on crd.CardCode = a.CardCode" & vbCrLf _
                & "left join crd1 sh on sh.cardcode = a.cardcode AND sh.AdresType='S'" & vbCrLf _
                & "Left Outer Join OCST ST ON sh.State=ST.Code AND sh.Country=ST.Country AND ST.Country='IN'" & vbCrLf _
                & "left join OITM as itm on itm.ItemCode = b.ItemCode" & vbCrLf _
                & "Left join INV12 as c on c.DocEntry = b.DocEntry" & vbCrLf _
                & "where a.docentry = '" & flex.get_TextMatrix(i, 0) & "' and a.PIndicator = '" & Year.Text & "'  and b.Treetype <> 'I'  and b.ItemCode<>'FrightCharges' ) k GROUP BY QryGroup8,Cardfname,convert(nvarchar(max),k.Building) ,convert(nvarchar(max),k.Block) ,convert(nvarchar(max),k.City) ,convert(nvarchar(max),k.STATE) ,convert(nvarchar(max),k.Street) ,convert(nvarchar(max),k.ZipCode) ,convert(nvarchar(max),k.country) ,convert(nvarchar(max),k.county) , trdis,DiscAmt,[FORWARDING CHARGES],DiscSum,VatSum,RoundDif,taxcode,TransCat,k.Esugam"



                'Dim CMD As New sqlcommand(TSQL, con1)
                'If con.State = ConnectionState.Closed Then
                '    con1.Open()
                'End If
                'Dim RP As SqlDataReader
                'RP = CMD.ExecuteReader
                'RP.Read()
                'If Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(66), "-------------")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, Chr(27) + Chr(69) + "", TAB(79 - Len(Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") + Chr(27) + Chr(70) + Chr(18))
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Season Discount", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(0), RP("TRadeDiscount"), SPC(1), TAB(40), "Less Discount", SPC(1), Microsoft.VisualBasic.Format((RP("SchemeDiscPercent")), "#######0"), TAB(56), SPC(1), "%", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount :  ", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'PrintLine(1, TAB(0), Chr(27) + Chr(69) + RP("Esugam") + Chr(27) + Chr(70) + Chr(18), SPC(1), TAB(71), "-------------")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RP("ttl1"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(66), Chr(27) + Chr(69) + "", TAB(79 - Len(Microsoft.VisualBasic.Format(RP("ttl1"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("ttl1"), "#######0.00") + Chr(27) + Chr(70) + Chr(18)) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RP("vatsum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(0), (RP("HFROM")), TAB(30), "Add : ", SPC(1), (RP("CFROM")), SPC(1), (RP("taxcode")), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RP("vatsum"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("vatsum"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Add : Forwarding Charges", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(RP("RoundDif"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Round Off :  ", TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RP("RoundDif"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("RoundDif"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'PrintLine(1, TAB(55), "-----------------------")
                'lin = lin + 1
                'PrintLine(1, TAB(33), Chr(27) + Chr(69) + "Grand Total :", TAB(59), Microsoft.VisualBasic.Format(RP("box"), "#####0"), TAB(67 - Len(Microsoft.VisualBasic.Format(RP("qty"), "#######0"))), Microsoft.VisualBasic.Format(RP("qty"), "#######0"), TAB(79 - Len(Microsoft.VisualBasic.Format(RP("GRANDTOTAL"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("GRANDTOTAL"), "#######0.00") + Chr(27) + Chr(70) + Chr(18))
                'lin = lin + 1
                'PrintLine(1, Space(5), "")
                'lin = lin + 1
                'PrintLine(1, Space(5), "")
                'lin = lin + 1
                'PrintLine(1, TAB(2), Chr(27) + Chr(69) + "", SPC(1), RupeesToWord(RP("GRANDTOTAL")), SPC(1), "" + Chr(27) + Chr(70) + Chr(18))
                'lin = lin + 1
                Dim CMD As New SqlCommand(TSQL, con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                Dim RP As SqlDataReader
                RP = CMD.ExecuteReader
                RP.Read()

                If RP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(1), Chr(27) + Chr(69) + "Delivery Address : " + Chr(27) + Chr(70) + Chr(18), Space(5), "")
                ElseIf RP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(1), Chr(27) + Chr(69) + "Delivery Address : " + Chr(27) + Chr(70) + Chr(18), TAB(71), "-------------")
                ElseIf RP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(66), "-------------")
                Else
                    PrintLine(1, TAB(66), Space(5), "")
                End If

                lin = lin + 1
                If RP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(1), Chr(27) + Chr(69) + RP.Item("Cardfname").ToString + Chr(27) + Chr(70) + Chr(18), Space(5), "")
                ElseIf RP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(1), Chr(27) + Chr(69) + RP.Item("Cardfname").ToString + Chr(27) + Chr(70) + Chr(18), TAB(66), Chr(27) + Chr(69) + "", TAB(84 - Len(Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") + Chr(27) + Chr(70) + Chr(18))
                ElseIf RP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(66), Chr(27) + Chr(69) + "", TAB(79 - Len(Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") + Chr(27) + Chr(70) + Chr(18))
                Else
                    PrintLine(1, Space(5), "")
                End If
                lin = lin + 1

                If RP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(1), RP.Item("Building").ToString, TAB(40), "Less Season Discount", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00"))
                ElseIf RP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00") = "0.00" Then
                    PrintLine(1, TAB(1), RP.Item("Building").ToString, Space(5), "")
                ElseIf RP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(40), "Less Season Discount", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00"))
                Else
                    PrintLine(1, Space(5), "")
                End If


                ''If Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Season Discount", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1

                If RP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(1), RP.Item("BLOCK").ToString, TAB(5), RP("TRadeDiscount"), SPC(1), TAB(40), "Less Discount", SPC(1), Microsoft.VisualBasic.Format((RP("SchemeDiscPercent")), "#######0"), TAB(56), SPC(1), "%", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00"))
                ElseIf RP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RP("SchemeDiscPercent"), "#######0.00") = "0.00" Then
                    PrintLine(1, TAB(1), RP.Item("BLOCK").ToString, Space(5), "")
                ElseIf RP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(0), RP("TRadeDiscount"), SPC(1), TAB(40), "Less Discount", SPC(1), Microsoft.VisualBasic.Format((RP("SchemeDiscPercent")), "#######0"), TAB(56), SPC(1), "%", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00"))
                Else
                    PrintLine(1, Space(5), "")
                End If




                ''If Microsoft.VisualBasic.Format(RP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(0), RP("TRadeDiscount"), SPC(1), TAB(40), "Less Discount", SPC(1), Microsoft.VisualBasic.Format((RP("SchemeDiscPercent")), "#######0"), TAB(56), SPC(1), "%", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1

                If RP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(1), RP.Item("STREET").ToString, TAB(40), "Less Discount :  ", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00"))
                ElseIf RP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00") = "0.00" Then
                    PrintLine(1, TAB(1), RP.Item("STREET").ToString, Space(5), "")
                ElseIf RP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(40), "Less Discount :  ", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00"))

                Else
                    PrintLine(1, Space(5), "")
                End If




                ''If Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount :  ", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1

                If RP.Item("QryGroup8").ToString = "Y" Then
                    PrintLine(1, TAB(1), RP.Item("CITY").ToString, SPC(1), "-", SPC(1), RP.Item("ZIPCODE").ToString, TAB(10), Chr(27) + Chr(69) + RP("Esugam") + Chr(27) + Chr(70) + Chr(18), SPC(1), TAB(71), "-------------")
                ElseIf RP.Item("QryGroup8").ToString = "N" Then
                    PrintLine(1, TAB(10), Chr(27) + Chr(69) + RP("Esugam") + Chr(27) + Chr(70) + Chr(18), SPC(1), TAB(71), "-------------")
                Else
                    PrintLine(1, Space(5), "")
                End If
                'PrintLine(1, TAB(10), Chr(27) + Chr(69) + RP("Esugam") + Chr(27) + Chr(70) + Chr(18), SPC(1), TAB(71), "-------------")
                lin = lin + 1
                If Microsoft.VisualBasic.Format(RP("ttl1"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(66), Chr(27) + Chr(69) + "", TAB(79 - Len(Microsoft.VisualBasic.Format(RP("ttl1"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("ttl1"), "#######0.00") + Chr(27) + Chr(70) + Chr(18)) Else PrintLine(1, Space(5), "")
                lin = lin + 1
                If Microsoft.VisualBasic.Format(RP("vatsum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(0), (RP("HFROM")), TAB(30), "Add : ", SPC(1), (RP("CFROM")), SPC(1), (RP("taxcode")), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RP("vatsum"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("vatsum"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1
                If Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Add : Forwarding Charges", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1
                If Microsoft.VisualBasic.Format(RP("RoundDif"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Round Off :  ", TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RP("RoundDif"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("RoundDif"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1
                PrintLine(1, TAB(55), "-----------------------")
                lin = lin + 1
                PrintLine(1, TAB(33), Chr(27) + Chr(69) + "Grand Total :", TAB(59), Microsoft.VisualBasic.Format(RP("box"), "#####0"), TAB(67 - Len(Microsoft.VisualBasic.Format(RP("qty"), "#######0"))), Microsoft.VisualBasic.Format(RP("qty"), "#######0"), TAB(79 - Len(Microsoft.VisualBasic.Format(RP("GRANDTOTAL"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("GRANDTOTAL"), "#######0.00") + Chr(27) + Chr(70) + Chr(18))
                lin = lin + 1
                PrintLine(1, Space(5), "")
                lin = lin + 1
                PrintLine(1, Space(5), "")
                lin = lin + 1
                PrintLine(1, TAB(2), Chr(27) + Chr(69) + "", SPC(1), RupeesToWord(RP("GRANDTOTAL")), SPC(1), "" + Chr(27) + Chr(70) + Chr(18))
                lin = lin + 1

                If LOP = Label14.Text Then
                    n = 71 - lin
                    For k = 1 To n
                        'PrintLine(1, Space(5), "")
                        lin = lin + 1
                    Next k
                Else
                    n = 71 - lin
                    For k = 1 To n
                        PrintLine(1, Space(5), "")
                        lin = lin + 1
                    Next k
                End If
                n = 0
                lin = 0

                mbox = 0
                mmtr = 0
                mtotqty = 0
                mtotamt = 0




                RP.Close()
                con.Close()
                con.Close()




            End If

            '    End If

            '    LOP = LOP + 1
        Next i
        LOP = 0
        FileClose(1)
        'Shell("print /d:LPT" & lpt.Text & " " + dbreportpath + "Rrlorry.txt", vbNormalFocus)

        Dim printer As String = mlsprinter   'laserprinter name
        Dim filePath As String = mlinpath
        Dim filePathname As String = mlinpath & "Rrlorry.txt"
        Dim success As Boolean = PrintTextFile(filePathname)


    End Sub

    Private Sub Button7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button7.Click
        Dim i As Integer
        FileOpen(1, " " + mlinpath + "SSlorry.txt", OpenMode.Output, OpenAccess.Write)
        For i = flex.Rows - 1 To 1 Step -1

            If Len(Trim(flex.get_TextMatrix(i, 1))) > 0 Then
                LOP = LOP + 1
                ''  If MsgBox("Next Print " & flex.get_TextMatrix(i, 2), MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                'Call MAIN()
                PSQL = "SELECT  'ORIGINAL' as grpcopy, ITM.U_BrandType,A.DocNum ,A.DocEntry ,A.CardCode ,A.CardName ,A.DocDate ,A.DocDueDate,B.ItemCode ," & vbCrLf _
        & "CASE when l.QryGroup3 ='Y' then  convert(nvarchar(20),(itm.U_Scode +'-'+ itm.u_subgrp2)) else convert(nvarchar(20),B.Dscription) end Dscription,a.VatSumSy," & vbCrLf _
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
        & "FROM  OINV   A  WITH (NOLOCK)" & vbCrLf _
        & "INNER JOIN  INV1  B WITH (NOLOCK) ON A.DOCENTRY=B.DOCENTRY" & vbCrLf _
        & "left join OITM  as itm  WITH (NOLOCK) on itm.ItemCode = b.ItemCode" & vbCrLf _
        & "left join OITB  as itb WITH (NOLOCK) on itb.ItmsGrpCod = itm.ItmsGrpCod" & vbCrLf _
        & "Left join OOEI  as Ex WITH (NOLOCK) on Ex.DocEntry =a.U_Exbaseentry " & vbCrLf _
        & "left join INV4  cn WITH (NOLOCK) on cn.DocEntry=b.DocEntry and cn.LineNum=b.LineNum and cn.staType=7" & vbCrLf _
        & "Left join INV12  as Tx WITH (NOLOCK) on TX.DocEntry = b.DocEntry	" & vbCrLf _
        & "left OUTER join OCRD  as l WITH (NOLOCK)on l.CardCode = a.CardCode " & vbCrLf _
        & "left OUTER join (SELECT U_Scode U_remarks,ItemCode,ItemName FROM OITM WITH (NOLOCK) WHERE U_Scode IS NOT NULL) P on P.ItemName=B.U_CatalogName " & vbCrLf _
        & "left join (  select K.docentry,SUM(k.mrpvalue) mpvalue from " & vbCrLf _
               & "(select b.docentry,CASE when b.AssblValue>=1000 then  SUM(b.quantity)*b.assblvalue else 0 end  as mrpvalue from INV1  b WITH (NOLOCK) " & vbCrLf _
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
        & "(Select ItemCode,LineTotal [FORWARDING CHARGES],DocEntry from INV1 WHERE ItemCode='FrightCharges') " & vbCrLf _
        & "S ON S.DocEntry=B.DocEntry" & vbCrLf _
        & ",OADM AA WITH (NOLOCK),ADM1 BB WITH (NOLOCK)" & vbCrLf _
        & "where a.docentry = '" & flex.get_TextMatrix(i, 0) & "' and a.PIndicator = '" & Year.Text & "' and b.ItemCode <>'FrightCharges'" & vbCrLf _
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
                PAG = 1

                Dim CMDp As New SqlCommand(PSQL, con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                Dim DRP As SqlDataReader
                DRP = CMDp.ExecuteReader
                DRP.Read()

                lin = 0

                '' FileOpen(1,  dbreportpath +"\SSlorry.txt", OpenMode.Output, OpenAccess.Write)

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

                TSQL = "SELECT    SUM(Quantity) QTY, SUM(BeforeDisc) BeforeDisc,SUM(mrpvalue1) mrpvalue,trdis + '%  = ' + convert(nvarchar(max),sum(cast(SchemeDiscAmt as numeric(19,2)))) TRadeDiscount," & vbCrLf _
        & "DiscAmt,DiscSum,MAX(SchemeDiscPercent)SchemeDiscPercent,sum(SchemeDiscAmt) SchemeDiscAmt , (((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum ) ttl1," & vbCrLf _
        & "(((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES]  + VatSum + RoundDif ) GRANDTOTAL," & vbCrLf _
        & "sum(EXVALUE) exvalue, stacode1 , Taxsum taxsum1, isnull(stacode2,0) stacode2 , isnull(Taxsum2,0) taxsum2 ,  ISNULL(transcat,'') Trascat, CASE WHEN TransCat = 'Form C' THEN '[Against C Form]' ELSE ' ' END  CFROM," & vbCrLf _
        & "CASE WHEN TransCat = 'Form H' THEN 'TAX EXEMPTED AGAINST FORM H' ELSE ' ' END  HFROM,   VatSum,    [FORWARDING CHARGES] AS FORWARDINGCHARGES ,RoundDif " & vbCrLf _
        & "FROM (SELECT b.LineNum,(b.Quantity*b.PriceBefDi) BeforeDisc, case when b.AssblValue>=1000 then (b.Quantity)*b.AssblValue else (b.Quantity)*b.AssblValue end mrpvalue1," & vbCrLf _
        & "isnull(G.[Disc Amt],0) DiscAmt ,ISNULL(S.[FORWARDING CHARGES],0) [FORWARDING CHARGES], case when isnull(convert(nvARCHAR(MAX),crd.U_Dis1),'') <> '' then 'Trade Dsicount   ' +" & vbCrLf _
        & "convert(nvarchar(max),(cast(crd.U_Dis1 as numeric(19,0))))   else '' end  trdis,a.DiscSum,A.VatSum,A.RoundDif," & vbCrLf _
        & "CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal" & vbCrLf _
        & "End 'SchemeDiscAmt',C.TransCat,  B.Quantity,(b.Quantity/ITM.SalPackUn) Box,   CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent'," & vbCrLf _
        & "case when b.AssblValue<1000 then (b.Quantity)*b.AssblValue else 0 end  EXVALUE, tx.stacode1,tx.Taxsum,tx1.stacode1 stacode2,tx1.Taxsum taxsum2" & vbCrLf _
        & "FROM OINV A JOIN INV1 B ON B.DocEntry = A.DocEntry left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry," & vbCrLf _
        & "sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from INV1 " & vbCrLf _
        & "where DiscPrcnt=100 group by DocEntry) G on G.DocEntry=B.DocEntry" & vbCrLf _
        & " left OUTER join (Select SUM(LineTotal) [FORWARDING CHARGES],DocEntry from INV3  GROUP BY DocEntry) S ON S.DocEntry=B.DocEntry   left join  OCRD crd on crd.CardCode = a.CardCode  left join OITM as itm on itm.ItemCode = b.ItemCode" & vbCrLf _
        & "Left join INV12 as c on c.DocEntry = b.DocEntry  Left join  (" & vbCrLf _
        & "select case when a.statype=7 then  convert(nvarchar(8),convert(numeric(4,0),TaxRate)) + '% of Assessable Value (60% on MRP)' " & vbCrLf _
        & "when staType = 4 and  b.Name like '%CST%' and isnull(d.TransCat,'') <> '' then 'Add : Against ""C"" Form ' + b.Name" & vbCrLf _
        & "when staType = 4 and  b.Name like '%CST%' and isnull(d.TransCat,'') = '' then 'Add : ' + b.Name" & vbCrLf _
        & "when staType = 1 and b.Name Like '%VAT%'then 'Add: ' + b.Name" & vbCrLf _
        & "else b.Name end stacode1, CASE when a.staType=7 then 1       when a.staType=1 then 2       when a.staType=4 then 3 end as ctyp," & vbCrLf _
        & "a.DocEntry ,b.Name stacode ,a.TaxRate ,sum(a.TaxSum) Taxsum,a.statype,CASE when a.staType=7 then tt.mpvalue else 0 end as mpvalue," & vbCrLf _
        & "CASE when a.staType=7 then tt.mpvalue1 else 0 end as mpvalue1  from inv4 a " & vbCrLf _
        & "LEft join OINV dd on dd.DocEntry = a.DocEntry" & vbCrLf _
        & "left join OSTA b  on b.Code =a.StaCode " & vbCrLf _
        & "left join (  select k.TransCat,K.docentry,SUM(k.mrpvalue) mpvalue,sum(k.mrpvalue1) mpvalue1 from       (select c.TransCat,b.docentry,CASE when b.AssblValue>=1000 then  SUM(b.quantity)*(b.assblvalue*60/100) else 0 end  as mrpvalue," & vbCrLf _
        & "CASE when b.AssblValue>=1000 then  SUM(b.quantity)*b.assblvalue else 0 end  as mrpvalue1   from INV1 b Left join INV12 c on c.DocEntry = b.DocEntry" & vbCrLf _
        & "group by b.DocEntry,b.AssblValue,c.TransCat) k        group by k.DocEntry,k.TransCat        having SUM(k.mrpvalue)>0 ) tt on tt.DocEntry=a.DocEntry" & vbCrLf _
        & "left join INV12 d on d.DocEntry = a.docentry where staType = 7 and TaxRate = 2 group by a.DocEntry ,a.StaCode ,a.TaxRate ,b.Name,a.statype,tt.mpvalue,tt.mpvalue1 ,d.TransCat" & vbCrLf _
        & ") tx on tx.DocEntry =a.DocEntry" & vbCrLf _
        & "Left join  (" & vbCrLf _
        & "select case when a.statype=7 then  convert(nvarchar(8),convert(numeric(4,0),TaxRate)) + '% of Assessable Value (60% on MRP)'" & vbCrLf _
        & "when staType = 4 and  b.Name like '%CST%' and isnull(d.TransCat,'') <> '' then 'Add : Against ""C"" Form ' + b.Name " & vbCrLf _
        & "when staType = 4 and  b.Name like '%CST%' and isnull(d.TransCat,'') = '' then 'Add : ' + b.Name" & vbCrLf _
        & "when staType = 1 and b.Name Like '%VAT%'then 'Add: ' + b.Name" & vbCrLf _
        & "else b.Name end stacode1, CASE when a.staType=7 then 1       when a.staType=1 then 2       when a.staType=4 then 3 end as ctyp," & vbCrLf _
        & "a.DocEntry ,b.Name stacode ,a.TaxRate ,sum(a.TaxSum) Taxsum,a.statype,CASE when a.staType=7 then tt.mpvalue else 0 end as mpvalue," & vbCrLf _
        & "CASE when a.staType=7 then tt.mpvalue1 else 0 end as mpvalue1  from inv4 a " & vbCrLf _
        & "LEft join OINV dd on dd.DocEntry = a.DocEntry " & vbCrLf _
        & "left join OSTA b  on b.Code =a.StaCode " & vbCrLf _
        & "left join (  select k.TransCat,K.docentry,SUM(k.mrpvalue) mpvalue,sum(k.mrpvalue1) mpvalue1 from       (select c.TransCat,b.docentry,CASE when b.AssblValue>=1000 then  SUM(b.quantity)*(b.assblvalue*60/100) else 0 end  as mrpvalue," & vbCrLf _
        & "CASE when b.AssblValue>=1000 then  SUM(b.quantity)*b.assblvalue else 0 end  as mrpvalue1   from INV1 b Left join INV12 c on c.DocEntry = b.DocEntry " & vbCrLf _
        & "group by b.DocEntry,b.AssblValue,c.TransCat) k        group by k.DocEntry,k.TransCat        having SUM(k.mrpvalue)>0 ) tt on tt.DocEntry=a.DocEntry " & vbCrLf _
        & "left join INV12 d on d.DocEntry = a.docentry where staType <> 7  group by a.DocEntry ,a.StaCode ,a.TaxRate ,b.Name,a.statype,tt.mpvalue,tt.mpvalue1 ,d.TransCat " & vbCrLf _
        & ") tx1 on tx1.DocEntry =a.DocEntry " & vbCrLf _
        & "where a.docentry = '" & flex.get_TextMatrix(i, 0) & "' and a.PIndicator = '" & Year.Text & "' and b.Treetype <> 'I'  and b.ItemCode<>'FrightCharges' " & vbCrLf _
        & ") k GROUP BY trdis,DiscAmt,[FORWARDING CHARGES],DiscSum,VatSum,RoundDif,TransCat,stacode1 , Taxsum , stacode2 , Taxsum2  " & vbCrLf












                Dim CMD As New SqlCommand(TSQL, con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                Dim RP As SqlDataReader
                RP = CMD.ExecuteReader
                RP.Read()



                If Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(66), "-------------")
                lin = lin + 1
                PrintLine(1, TAB(42 - Len(Microsoft.VisualBasic.Format(RP("QTY"), "#######0"))), Microsoft.VisualBasic.Format(RP("QTY"), "#######0"), TAB(58 - Len(Microsoft.VisualBasic.Format(RP("mrpvalue"), "#######0"))), Microsoft.VisualBasic.Format(RP("mrpvalue"), "#######0"), TAB(75 - Len(Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("BeforeDisc"), "#######0.00"))
                lin = lin + 1
                If Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Season Discount", TAB(75 - Len(Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("discamt"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1
                If Microsoft.VisualBasic.Format(RP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(0), RP("TRadeDiscount"), SPC(1), TAB(40), "Less Discount", SPC(1), Microsoft.VisualBasic.Format((RP("SchemeDiscPercent")), "#######0"), TAB(56), SPC(1), "%", TAB(75 - Len(Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("SchemeDiscAmt"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1
                If Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount :  ", TAB(75 - Len(Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("DiscSum"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1
                PrintLine(1, TAB(52), "-----------------------")
                lin = lin + 1
                If Microsoft.VisualBasic.Format(RP("ttl1"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(66), Chr(27) + Chr(69) + "", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("ttl1"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("ttl1"), "#######0.00") + Chr(27) + Chr(70) + Chr(18)) Else PrintLine(1, Space(5), "")
                lin = lin + 1
                If Microsoft.VisualBasic.Format(RP("exvalue"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(5), "DUTY EXEMPTED VALUE ", SPC(1), TAB(61 - Len(Microsoft.VisualBasic.Format(RP("exvalue"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("exvalue"), "#######0"), TAB(75 - Len(Microsoft.VisualBasic.Format("0.00"))), Microsoft.VisualBasic.Format("0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1
                If Microsoft.VisualBasic.Format(RP("taxsum1"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(5), (RP("stacode1")), SPC(1), TAB(75 - Len(Microsoft.VisualBasic.Format(RP("taxsum1"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("taxsum1"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1
                If (RP("Trascat")) <> "" Then PrintLine(1, TAB(5), (RP("Trascat")), TAB(30), (RP("stacode2")), SPC(1), TAB(75 - Len(Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00")) Else If Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(5), (RP("stacode2")), SPC(1), TAB(75 - Len(Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00")) Else PrintLine(1, Space(5), "")
                ''if Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(5), (RP("stacode2")), SPC(1), TAB(75 - Len(Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("taxsum2"), "#######0.00")) Else 
                lin = lin + 1
                If Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Add : Forwarding Charges", TAB(77 - Len(Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("FORWARDINGCHARGES"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1
                If Microsoft.VisualBasic.Format(RP("RoundDif"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Round Off :  ", TAB(66), TAB(75 - Len(Microsoft.VisualBasic.Format(RP("RoundDif"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("RoundDif"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1
                PrintLine(1, TAB(52), "-----------------------")
                lin = lin + 1
                PrintLine(1, TAB(15), Chr(27) + Chr(69) + "Grand Total :", TAB(44 - Len(Microsoft.VisualBasic.Format(RP("qty"), "#######0"))), Microsoft.VisualBasic.Format(RP("qty"), "#######0"), TAB(77 - Len(Microsoft.VisualBasic.Format(RP("GRANDTOTAL"), "#######0.00"))), Microsoft.VisualBasic.Format(RP("GRANDTOTAL"), "#######0.00") + Chr(27) + Chr(70) + Chr(18))
                lin = lin + 1
                PrintLine(1, TAB(5), "Total Amount Duty Paid : ", SPC(1), Microsoft.VisualBasic.Format(RP("taxsum1"), "#######0.00"))
                lin = lin + 1
                PrintLine(1, TAB(5), "Words :", SPC(1), RupeesToWord(RP("taxsum1")))
                lin = lin + 1
                PrintLine(1, Space(5), "")
                lin = lin + 1
                PrintLine(1, TAB(5), Chr(27) + Chr(69) + "", SPC(1), RupeesToWord(RP("GRANDTOTAL")), SPC(1), "" + Chr(27) + Chr(70) + Chr(18))
                lin = lin + 1
                If LOP = Label14.Text Then
                    n = 71 - lin
                    For k = 1 To n
                        'PrintLine(1, Space(5), "")
                        lin = lin + 1
                    Next k
                Else
                    n = 71 - lin
                    For k = 1 To n
                        PrintLine(1, Space(5), "")
                        lin = lin + 1
                    Next k
                End If
                n = 0
                lin = 0

                mbox = 0
                mmtr = 0
                mtotqty = 0
                mtotamt = 0


                RP.Close()
                con.Close()
                con.Close()




            End If

            '    End If
            LOP = LOP + 1

        Next i
        LOP = 0
        FileClose(1)

        'Shell("print /d:LPT" & lpt.Text & " d:\SSlorry.txt", vbNormalFocus)


        Dim printer As String = mlsprinter   'laserprinter name
        Dim filePath As String = mlinpath
        Dim filePathname As String = mlinpath & "SSlorry.txt"
        Dim success As Boolean = PrintTextFile(filePathname)


    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub flex_KeyUpEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyUpEvent) Handles flex.KeyUpEvent

    End Sub

    Private Sub Party_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Party.SelectedIndexChanged

    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click

    End Sub


    Private Sub rcclorry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rcclorry.Click
        Dim i As Integer
        FileOpen(1, " " + mlinpath + "rcclorry.txt", OpenMode.Output, OpenAccess.Write)
        For i = flex.Rows - 1 To 1 Step -1

            If Len(Trim(flex.get_TextMatrix(i, 1))) > 0 Then

                ''  If MsgBox("Next Print " & flex.get_TextMatrix(i, 2), MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                'Call MAIN()
                rccsql = "select CASE WHEN C.TransCat = 'FORM H' THEN 'Taxable' WHEN A.VatSum = '0' THEN 'Non  Taxable' ELSE 'Taxable' End AS TAXABLE, A.DOCENTRY,isnull(b.LineNum,'') LineNum,a.address,a.address2,A.cardname,a.U_Noofbun,r.Building,isnull(r.Block,'') Block,(r.City+'-'+ r.ZipCode) city , " & vbCrLf _
        & "CASE WHEN R.State='OS' THEN R.StreetNo WHEN l.QryGroup5='Y' THEN R.StreetNo ELSE ST.Name END AS STATE,r.State State1, " & vbCrLf _
        & "isnull(r.Street,'')Street,r.country,r.county, A.u_ESUGAM,A.docnum,a.Docdate,A.u_orderby,A.u_arcode,A.u_transport,A.u_docthrough,A.u_lrno,convert(varchar(10),A.u_lrdat,110) u_lrdat,A.u_lrwight,a.u_dsnation,a.U_lrval AS topay, " & vbCrLf _
        & "isnull(B.U_CatalogCode ,'') as item, " & vbCrLf _
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
         & "from OINV A inner join INV1 b on a.DocEntry=b.DocEntry  " & vbCrLf _
        & "left outer join OHEM D on D.empid=a.ownercode  " & vbCrLf _
        & "Left Join [@INCM_BND1] F on F.U_Name = a.U_brand  " & vbCrLf _
        & "Left join INV12 as c on c.DocEntry = b.DocEntry	 " & vbCrLf _
        & "left join OITM as itm on itm.ItemCode = b.ItemCode  " & vbCrLf _
        & "left join (SELECT * FROM [dbo].[@INS_OPLM] WHERE DocEntry IN (SELECT DocEntry FROM [@INS_PLM1] WHERE U_Remarks IS NOT NULL))  " & vbCrLf _
        & "as itb on itb.U_ItemCode =itm.ItemCode  " & vbCrLf _
        & "left join (SELECT DISTINCT CARDCODE,ISNULL(U_ActTino,'') TAXID11,ISNULL(U_ActCstno,'') TAXID1 FROM  OCRD  ) as crd on crd.CardCode = a.CardCode " & vbCrLf _
        & "left join CRD1 as r on r.CardCode = a.CardCode /*and a.paytocode = r.address*/ AND R.AdresType='B'  " & vbCrLf _
        & "Left Outer Join OCST ST ON R.State=ST.Code AND R.Country=ST.Country AND ST.Country='IN'  " & vbCrLf _
        & "left OUTER join OCRD as l on l.CardCode = a.CardCode  " & vbCrLf _
        & "left OUTER join (SELECT DISTINCT CONVERT(NVARCHAR(MAX),U_ItemName) U_ItemName ,CONVERT(NVARCHAR(MAX),U_Remarks) U_Remarks FROM [@INS_PLM1] WHERE u_lock = 'N') P on P.U_ItemName=B.U_CatalogName" & vbCrLf _
        & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry,  " & vbCrLf _
        & "sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from INV1 where DiscPrcnt=100 group by DocEntry) G  " & vbCrLf _
        & "on G.DocEntry=B.DocEntry  " & vbCrLf _
        & "left OUTER join (Select ItemCode,sum(Price) [FORWARDING CHARGES],DocEntry from INV1 WHERE ItemCode='FrEightCharges' group by ItemCode,DocEntry) S ON S.DocEntry=B.DocEntry  " & vbCrLf _
        & "where a.docentry = '" & flex.get_TextMatrix(i, 0) & "' and a.PIndicator = '" & Year.Text & "' and b.Treetype <> 'I'  and b.ItemCode<>'FrEightCharges' order by b.LineNum"


                rccmtotqty = 0
                rccmtotamt = 0
                PAG = 0

                Dim CMDp As New SqlCommand(rccsql, con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                Dim rccDRP As SqlDataReader
                rccDRP = CMDp.ExecuteReader
                rccDRP.Read()

                lin = 0
                '' FileOpen(1,  dbreportpath +"\rcclorry.txt", OpenMode.Output, OpenAccess.Write)
                ''FileOpen(1, Trim(dbreportpath) & "rcclorry.txt", OpenMode.Output, OpenAccess.Write)
                'FileOpen(1, "e:\Test.txt", OpenMode.Output, OpenAccess.Write)
                PrintLine(1, TAB(33), Chr(27) + Chr(45) + Chr(1) + Chr(14) + Chr(27) + Chr(69) + "COPY INVOICE" + Chr(27) + Chr(70) + Chr(18) + Chr(27) + Chr(45) + Chr(0) + Chr(18))
                lin = lin + 1
                PrintLine(1, TAB(0), "FROM,", TAB(51), "TO,")
                lin = lin + 1
                PrintLine(1, TAB(0), Chr(27) + Chr(69) + "RAMRAJ HANDLOOMS" + Chr(27) + Chr(70) + Chr(18), TAB(56), "M/s.", SPC(1), Chr(27) + Chr(69) + rccDRP.Item("CardFName").ToString + Chr(27) + Chr(70) + Chr(18))
                lin = lin + 1
                PrintLine(1, TAB(0), "(A UNIT OF ENES TEXTILE MILLS),", TAB(51), rccDRP.Item("BUILDING").ToString)
                lin = lin + 1
                PrintLine(1, TAB(0), "RAMRAJ-V-TOWER,", TAB(51), rccDRP.Item("BLOCK").ToString)
                lin = lin + 1
                PrintLine(1, TAB(0), "10-SENGUNTHAPURAM,", TAB(51), rccDRP.Item("STREET").ToString)
                lin = lin + 1
                PrintLine(1, TAB(0), "MANGALAM ROAD,TIRUPUR-641604.", TAB(51), rccDRP.Item("CITY").ToString)
                lin = lin + 1
                PrintLine(1, TAB(0), "PH:0421-4304147", TAB(51), rccDRP.Item("county").ToString, SPC(1), "Dt.", SPC(1), "(", rccDRP.Item("state1").ToString, SPC(1), ")")
                lin = lin + 1
                PrintLine(1, TAB(0), "TIN NO.: 33652323660", TAB(51), "TIN NO.:", SPC(1), rccDRP.Item("TINNO").ToString)
                lin = lin + 1
                PrintLine(1, TAB(0), "CST NO.: 334666 DATE:05.09.03", TAB(51), "CST NO.:", SPC(1), rccDRP.Item("CSTNO").ToString)
                lin = lin + 1
                PrintLine(1, TAB(51), "Mob NO.:", SPC(1), rccDRP.Item("Cellular").ToString)
                lin = lin + 1
                PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
                lin = lin + 1
                PrintLine(1, TAB(33), Chr(27) + Chr(69) + "TRANSPORT COPY" + Chr(27) + Chr(70) + Chr(18))
                lin = lin + 1
                PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
                lin = lin + 1
                PrintLine(1, TAB(0), "AREA CODE :", SPC(1), (rccDRP("u_arcode").ToString), TAB(32), "BY :", SPC(1), (rccDRP("u_orderby").ToString), TAB(60), "DATE:", SPC(1), Microsoft.VisualBasic.FormatDateTime(rccDRP("docdate"), DateFormat.ShortDate))
                lin = lin + 1
                PrintLine(1, TAB(0), "TRANSPORT :", SPC(1), Chr(27) + Chr(69) + (rccDRP("u_transport").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(37), "TO :", SPC(1), Chr(27) + Chr(69) + (rccDRP("u_dsnation").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(70), "INV No:", SPC(1), (rccDRP("docnum").ToString))
                lin = lin + 1
                PrintLine(1, TAB(0), "DOCOUMENT THROUGH :", SPC(1), (rccDRP("u_docthrough")), TAB(60), "BUNDLE:", SPC(1), Chr(27) + Chr(69) + (rccDRP("U_Noofbun").ToString) + Chr(27) + Chr(70) + Chr(18))
                lin = lin + 1
                PrintLine(1, Space(5), "")
                lin = lin + 1
                PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
                lin = lin + 1
                PrintLine(1, TAB(0), Chr(27) + Chr(69) + "SIZE         PARTICULARS                 QTY     MTRS     RATE       AMOUNT" + Chr(27) + Chr(70) + Chr(18))
                lin = lin + 1
                PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
                lin = lin + 1
                PrintLine(1, TAB(1), (rccDRP("u_size").ToString), TAB(10), (rccDRP("item").ToString), TAB(45 - Len(Microsoft.VisualBasic.Format(rccDRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(rccDRP("U_NoofPiece"), "#######0"), TAB(55 - Len(Microsoft.VisualBasic.Format(rccDRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(rccDRP("box"), "#######0.00"), TAB(65 - Len(Microsoft.VisualBasic.Format(rccDRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(rccDRP("PriceBefDi"), "#######0.00"), TAB(77 - Len(Microsoft.VisualBasic.Format(rccDRP("BEFORE DISC"), "#######0.00"))), Microsoft.VisualBasic.Format(rccDRP("BEFORE DISC"), "#######0.00"))
                rccmtotamt = rccmtotamt + rccDRP("BEFORE DISC")
                rccmtotqty = rccmtotqty + rccDRP("U_NoofPiece")
                rccmbox = rccmbox + rccDRP("box")
                While rccDRP.Read
                    PrintLine(1, TAB(1), (rccDRP("u_size").ToString), TAB(10), (rccDRP("item").ToString), TAB(45 - Len(Microsoft.VisualBasic.Format(rccDRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(rccDRP("U_NoofPiece"), "#######0"), TAB(55 - Len(Microsoft.VisualBasic.Format(rccDRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(rccDRP("box"), "#######0.00"), TAB(65 - Len(Microsoft.VisualBasic.Format(rccDRP("PriceBefDi"), "#######0.00"))), Microsoft.VisualBasic.Format(rccDRP("PriceBefDi"), "#######0.00"), TAB(77 - Len(Microsoft.VisualBasic.Format(rccDRP("BEFORE DISC"), "#######0.00"))), Microsoft.VisualBasic.Format(rccDRP("BEFORE DISC"), "#######0.00"))
                    lin = lin + 1
                    rccmtotamt = rccmtotamt + rccDRP("BEFORE DISC")
                    rccmtotqty = rccmtotqty + rccDRP("U_NoofPiece")
                    rccmbox = rccmbox + rccDRP("box")
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
                        PrintLine(1, TAB(0), Chr(27) + Chr(69) + "RAMRAJ HANDLOOMS" + Chr(27) + Chr(70) + Chr(18), TAB(56), "M/s.", SPC(1), Chr(27) + Chr(69) + rccDRP.Item("CardFName").ToString + Chr(27) + Chr(70) + Chr(18))
                        lin = lin + 1
                        PrintLine(1, TAB(0), "(A UNIT OF ENES TEXTILE MILLS),", TAB(51), rccDRP.Item("BUILDING").ToString)
                        lin = lin + 1
                        PrintLine(1, TAB(0), "RAMRAJ-V-TOWER,", TAB(51), rccDRP.Item("BLOCK").ToString)
                        lin = lin + 1
                        PrintLine(1, TAB(0), "10-SENGUNTHAPURAM,", TAB(51), rccDRP.Item("STREET").ToString)
                        lin = lin + 1
                        PrintLine(1, TAB(0), "MANGALAM ROAD,TIRUPUR-641604.", TAB(51), rccDRP.Item("CITY").ToString)
                        lin = lin + 1
                        PrintLine(1, TAB(0), "PH:0421-4304147", TAB(51), rccDRP.Item("county").ToString, SPC(1), "Dt.")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "TIN NO.: 33652323660", TAB(51), "TIN NO.:", SPC(1), rccDRP.Item("TINNO").ToString)
                        lin = lin + 1
                        PrintLine(1, TAB(0), "CST NO.: 334666 DATE:05.09.03", TAB(51), "CST NO.:", SPC(1), rccDRP.Item("CSTNO").ToString)
                        lin = lin + 1
                        PrintLine(1, TAB(51), "Mob NO.:", SPC(1), rccDRP.Item("Cellular").ToString)
                        lin = lin + 1
                        PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
                        lin = lin + 1
                        PrintLine(1, TAB(33), Chr(27) + Chr(69) + "TRANSPORT COPY" + Chr(27) + Chr(70) + Chr(18))
                        lin = lin + 1
                        PrintLine(1, TAB(0), "------------------------------------------------------------------------------")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "AREA CODE :", SPC(1), (rccDRP("u_arcode").ToString), TAB(32), "BY :", SPC(1), (rccDRP("u_orderby").ToString), TAB(60), "DATE:", SPC(1), Microsoft.VisualBasic.FormatDateTime(rccDRP("docdate"), DateFormat.ShortDate))
                        lin = lin + 1
                        PrintLine(1, TAB(0), "TRANSPORT :", SPC(1), Chr(27) + Chr(69) + (rccDRP("u_transport").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(37), "TO :", SPC(1), Chr(27) + Chr(69) + (rccDRP("u_dsnation").ToString) + Chr(27) + Chr(70) + Chr(18), TAB(70), "INV No:", SPC(1), (rccDRP("docnum").ToString))
                        lin = lin + 1
                        PrintLine(1, TAB(0), "DOCOUMENT THROUGH :", SPC(1), (rccDRP("u_docthrough")), TAB(60), "BUNDLE:", SPC(1), Chr(27) + Chr(69) + (rccDRP("U_Noofbun").ToString) + Chr(27) + Chr(70) + Chr(18))
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
                rccDRP.Close()
                con.Close()
                con.Close()


                n = 48 - lin
                For k = 1 To n
                    PrintLine(1, Space(5), "")
                    lin = lin + 1
                Next k

                rcctsql = "SELECT convert(nvarchar(max),k.Building) Building,convert(nvarchar(max),k.Block) Block,convert(nvarchar(max),k.City) City,convert(nvarchar(max),k.STATE) STATE,convert(nvarchar(max),k.Street) Street,convert(nvarchar(max),k.ZipCode) ZipCode,convert(nvarchar(max),k.country) country,convert(nvarchar(max),k.county) county,  QryGroup8, ('M/s. ' + Cardfname) Cardfname, isnull(Esugam,'') Esugam,discprcnt,SUM(BeforeDisc) BeforeDisc,DiscAmt,MAX(SchemeDiscPercent)SchemeDiscPercent,[FORWARDING CHARGES] AS FORWARDINGCHARGES,DiscSum,sum(SchemeDiscAmt) SchemeDiscAmt ," & vbCrLf _
        & "(((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES] ) ttl1,VatSum,RoundDif, (((SUM(BeforeDisc)-DiscAmt)-SUM(SchemeDiscAmt))-DISCSum+[FORWARDING CHARGES] + VatSum + RoundDif ) GRANDTOTAL" & vbCrLf _
        & ",taxcode,CASE WHEN TransCat = 'Form C' THEN '[Against C Form]' ELSE ' ' END  CFROM,CASE WHEN TransCat = 'Form H' THEN 'TAX EXEMPTED AGAINST FORM H' ELSE ' ' END  HFROM,sum(U_NoofPiece)U_NoofPiece,SUM(Quantity) QTY,SUM(box)  BOX" & vbCrLf _
        & "FROM (SELECT   crd.QryGroup8,Cardfname,(b.Quantity*b.PriceBefDi) BeforeDisc,isnull(G.[Disc Amt],0) DiscAmt ,ISNULL(S.[FORWARDING CHARGES],isnull(fr.LineTotal,0)) [FORWARDING CHARGES]," & vbCrLf _
        & "case when c.State = 'KA' Then 'e-sugam no.' + a.U_ESugam when c.State in ('AP','TS') Then 'e-wayBill no.' + a.U_ESugam  when c.State in ('KL') Then 'e-Token No.' + a.U_ESugam  else '' end Esugam," & vbCrLf _
      & "sh.Building,isnull(sh.Block,'') Block,sh.City,CASE WHEN sh.State='OS' THEN sh.StreetNo WHEN crd.QryGroup5='Y' THEN sh.StreetNo ELSE ST.Name END AS STATE,sh.State State1,sh.ZipCode,isnull(sh.Street,'')Street,sh.country,sh.county," & vbCrLf _
      & "a.discprcnt,a.DiscSum,A.VatSum,A.RoundDif,B.TaxCode,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE round(cast(B.Quantity*B.PriceBefDi as numeric(19,6)),2)-B.LineTotal  END 'SchemeDiscAmt',C.TransCat,b.U_NoofPiece,B.Quantity,(b.Volume) Box,CASE WHEN b.DiscPrcnt=100.000000 THEN 0.000000 ELSE b.DiscPrcnt END 'SchemeDiscPercent'" & vbCrLf _
        & "FROM OINV A JOIN INV1 B ON B.DocEntry = A.DocEntry" & vbCrLf _
        & "left OUTER join (select sum(distinct(DiscPrcnt)) DiscPrcnt,DocEntry,sum(cast(Quantity*PriceBefDi as numeric(19,6))) [Disc Amt] from INV1 where DiscPrcnt=100 group by DocEntry) G on G.DocEntry=B.DocEntry" & vbCrLf _
        & "left OUTER join (Select ItemCode,sum(Price) [FORWARDING CHARGES],DocEntry from INV1 WHERE ItemCode='FrEightCharges' group by  ItemCode,DocEntry) S ON S.DocEntry=B.DocEntry  " & vbCrLf _
        & "left join OITM as itm on itm.ItemCode = b.ItemCode" & vbCrLf _
         & "left join  OCRD crd on crd.CardCode = a.CardCode" & vbCrLf _
  & "left join crd1 sh on sh.cardcode = a.cardcode AND sh.AdresType='S'" & vbCrLf _
   & "Left Outer Join OCST ST ON sh.State=ST.Code AND sh.Country=ST.Country AND ST.Country='IN'" & vbCrLf _
        & "Left join INV12 as c on c.DocEntry = b.DocEntry" & vbCrLf _
        & "Left join INV3 as fr on fr.DocEntry = b.DocEntry" & vbCrLf _
        & "where a.docentry = '" & flex.get_TextMatrix(i, 0) & "' and a.PIndicator = '" & Year.Text & "' and b.TreeType <> 'I' and b.ItemCode<>'FrEightCharges'  ) k GROUP BY QryGroup8,Cardfname,convert(nvarchar(max),k.Building) ,convert(nvarchar(max),k.Block) ,convert(nvarchar(max),k.City) ,convert(nvarchar(max),k.STATE) ,convert(nvarchar(max),k.Street) ,convert(nvarchar(max),k.ZipCode) ,convert(nvarchar(max),k.country) ,convert(nvarchar(max),k.county) ,DiscAmt,[FORWARDING CHARGES],DiscSum,VatSum,RoundDif,taxcode,TransCat,Esugam,discprcnt"






                Dim CMD As New SqlCommand(rcctsql, con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                Dim rccRP As SqlDataReader
                rccRP = CMD.ExecuteReader
                rccRP.Read()
                'If Microsoft.VisualBasic.Format(rccRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(rccRP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(66), "-------------")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(rccRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(rccRP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("BeforeDisc"), "#######0.00"))
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(rccRP("discamt"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("discamt"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(rccRP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((rccRP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("SchemeDiscAmt"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(rccRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("FORWARDINGCHARGES"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(rccRP("DiscSum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount :  ", SPC(1), Microsoft.VisualBasic.Format((rccRP("discprcnt")), "#######0"), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("DiscSum"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'PrintLine(1, TAB(0), rccRP("Esugam"), SPC(1), TAB(66), "-------------")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(rccRP("ttl1"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("ttl1"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("ttl1"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(rccRP("vatsum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(0), (rccRP("HFROM")), TAB(30), "Add : ", TAB(40), (rccRP("CFROM")), SPC(1), (rccRP("taxcode")), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("vatsum"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("vatsum"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(rccRP("RoundDif"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Round Off :  ", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("RoundDif"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("RoundDif"), "#######0.00")) Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'PrintLine(1, TAB(40), "----------------------------------------")
                'lin = lin + 1
                'PrintLine(1, TAB(23), Chr(27) + Chr(69) + "Grand Total :", SPC(1), TAB(48 - Len(Microsoft.VisualBasic.Format(rccRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(rccRP("U_NoofPiece"), "#######0"), TAB(58 - Len(Microsoft.VisualBasic.Format(rccRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("box"), "#######0.00"), TAB(79 - Len(Microsoft.VisualBasic.Format(rccRP("GRANDTOTAL"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("GRANDTOTAL"), "#######0.00") + Chr(27) + Chr(70) + Chr(18))
                'lin = lin + 1
                'PrintLine(1, TAB(40), "----------------------------------------")
                'lin = lin + 1
                'PrintLine(1, Space(5), "")
                'lin = lin + 1
                'PrintLine(1, TAB(2), "", SPC(1), RupeesToWord(rccRP("GRANDTOTAL")), SPC(1), "")
                'lin = lin + 1
                'If Microsoft.VisualBasic.Format(rccRP("vatsum"), "#######0.00") = "0.00" Then PrintLine(1, TAB(2), "TEXTILE GOODS TAX EXEMPTED COMMODITY CODE : 794,795,796") Else PrintLine(1, Space(5), "")
                'lin = lin + 1
                'PrintLine(1, TAB(66), Chr(27) + Chr(69) + "For Ramco Clothing Company" + Chr(27) + Chr(70) + Chr(18))


                If rccRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(rccRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(rccRP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(1), "Delivery Address : ", TAB(5), Space(5), "")
                ElseIf rccRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(rccRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(rccRP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(1), "Delivery Address : ", TAB(66), "-------------")
                ElseIf rccRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(rccRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(rccRP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(66), "-------------")
                Else
                    PrintLine(1, Space(5), "")
                End If
                ' If Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RCCRP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(66), "-------------")
                lin = lin + 1



                If rccRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(rccRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(rccRP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(1), rccRP.Item("Cardfname").ToString, TAB(5), Space(5), "")
                ElseIf rccRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(rccRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(rccRP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(1), rccRP.Item("Cardfname").ToString, TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("BeforeDisc"), "#######0.00"))
                ElseIf rccRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(rccRP("BeforeDisc"), "#######0.00") <> Microsoft.VisualBasic.Format(rccRP("TTL1"), "#######0.00") Then
                    PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("BeforeDisc"), "#######0.00"))
                Else
                    PrintLine(1, Space(5), "")
                End If

                '   If Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00") = Microsoft.VisualBasic.Format(RCCRP("TTL1"), "#######0.00") Then PrintLine(1, Space(5), "") Else PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("BeforeDisc"), "#######0.00"))
                lin = lin + 1


                If rccRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(rccRP("discamt"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(1), rccRP.Item("Building").ToString, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("discamt"), "#######0.00"))
                ElseIf rccRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(rccRP("discamt"), "#######0.00") = "0.00" Then
                    PrintLine(1, TAB(1), rccRP.Item("Building").ToString, Space(5), "")
                ElseIf rccRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(rccRP("discamt"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("discamt"), "#######0.00"))
                Else
                    PrintLine(1, Space(5), "")
                End If



                'If Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Season Discount", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("discamt"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1


                If rccRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(rccRP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(1), rccRP.Item("BLOCK").ToString, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((rccRP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("SchemeDiscAmt"), "#######0.00"))
                ElseIf rccRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(rccRP("SchemeDiscPercent"), "#######0.00") = "0.00" Then
                    PrintLine(1, TAB(1), rccRP.Item("BLOCK").ToString, Space(5), "")
                ElseIf rccRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(rccRP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((rccRP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("SchemeDiscAmt"), "#######0.00"))
                Else
                    PrintLine(1, Space(5), "")
                End If


                ' If Microsoft.VisualBasic.Format(RCCRP("SchemeDiscPercent"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Less Discount ", SPC(1), Microsoft.VisualBasic.Format((RCCRP("SchemeDiscPercent")), "#######0"), TAB(60), " %", TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("SchemeDiscAmt"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("SchemeDiscAmt"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1


                If rccRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(rccRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(1), rccRP.Item("STREET").ToString, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("FORWARDINGCHARGES"), "#######0.00"))
                ElseIf rccRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(rccRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(1), rccRP.Item("STREET").ToString, Space(5), "")
                ElseIf rccRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(rccRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("FORWARDINGCHARGES"), "#######0.00"))
                Else
                    PrintLine(1, Space(5), "")
                End If


                ' If Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Forwarding Charges", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00"))), Microsoft.VisualBasic.Format(RCCRP("FORWARDINGCHARGES"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1


                If rccRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(rccRP("DiscSum"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(1), rccRP.Item("CITY").ToString, SPC(1), "-", SPC(1), rccRP.Item("ZIPCODE").ToString, TAB(40), "Less Discount :  ", SPC(1), Microsoft.VisualBasic.Format((rccRP("discprcnt")), "#######0"), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("DiscSum"), "#######0.00"))
                ElseIf rccRP.Item("QryGroup8").ToString = "Y" And Microsoft.VisualBasic.Format(rccRP("DiscSum"), "#######0.00") = "0.00" Then
                    PrintLine(1, TAB(1), rccRP.Item("CITY").ToString, Space(5), "")
                ElseIf rccRP.Item("QryGroup8").ToString = "N" And Microsoft.VisualBasic.Format(rccRP("DiscSum"), "#######0.00") <> "0.00" Then
                    PrintLine(1, TAB(40), "Less Discount :  ", SPC(1), Microsoft.VisualBasic.Format((rccRP("discprcnt")), "#######0"), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("DiscSum"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("DiscSum"), "#######0.00"))
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
                PrintLine(1, TAB(0), rccRP("Esugam"), SPC(1), TAB(66), "-------------")
                lin = lin + 1
                If Microsoft.VisualBasic.Format(rccRP("ttl1"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("ttl1"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("ttl1"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1
                If Microsoft.VisualBasic.Format(rccRP("vatsum"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(0), (rccRP("HFROM")), TAB(30), "Add : ", TAB(40), (rccRP("CFROM")), SPC(1), (rccRP("taxcode")), SPC(1), "%", SPC(1), TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("vatsum"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("vatsum"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1
                If Microsoft.VisualBasic.Format(rccRP("RoundDif"), "#######0.00") <> "0.00" Then PrintLine(1, TAB(40), "Round Off :  ", SPC(1), TAB(66), TAB(77 - Len(Microsoft.VisualBasic.Format(rccRP("RoundDif"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("RoundDif"), "#######0.00")) Else PrintLine(1, Space(5), "")
                lin = lin + 1
                PrintLine(1, TAB(40), "----------------------------------------")
                lin = lin + 1
                PrintLine(1, TAB(23), Chr(27) + Chr(69) + "Grand Total :", SPC(1), TAB(48 - Len(Microsoft.VisualBasic.Format(rccRP("U_NoofPiece"), "#######0"))), Microsoft.VisualBasic.Format(rccRP("U_NoofPiece"), "#######0"), TAB(58 - Len(Microsoft.VisualBasic.Format(rccRP("box"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("box"), "#######0.00"), TAB(79 - Len(Microsoft.VisualBasic.Format(rccRP("GRANDTOTAL"), "#######0.00"))), Microsoft.VisualBasic.Format(rccRP("GRANDTOTAL"), "#######0.00") + Chr(27) + Chr(70) + Chr(18))
                lin = lin + 1
                PrintLine(1, TAB(40), "----------------------------------------")
                lin = lin + 1
                PrintLine(1, Space(5), "")
                lin = lin + 1
                PrintLine(1, TAB(2), "", SPC(1), RupeesToWord(rccRP("GRANDTOTAL")), SPC(1), "")
                lin = lin + 1
                If Microsoft.VisualBasic.Format(rccRP("vatsum"), "#######0.00") = "0.00" Then PrintLine(1, TAB(2), "TEXTILE GOODS TAX EXEMPTED COMMODITY CODE : 794,795,796") Else PrintLine(1, Space(5), "")
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

                rccRP.Close()
                con.Close()
                con.Close()




            End If

            '    End If


        Next i

        FileClose(1)

        'Shell("print /d:LPT" & lpt.Text & " d:\rcclorry.txt", vbNormalFocus)

        Dim printer As String = mlsprinter  'laserprinter name
        Dim filePath As String = mlinpath
        Dim filePathname As String = mlinpath & "rcclorry.txt"
        Dim success As Boolean = PrintTextFile(filePathname)



    End Sub

End Class

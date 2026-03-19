Imports System.IO
Imports System.Math
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.VbStrConv
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource

Public Class Frmorderprn
    Dim msel As Int16
    Dim msql, msql2, merr As String
    Dim i As Int32

    Private Sub Frmorderprn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        CLEAR(Me)
        disable(Me)
        Call loadparty2(cmbparty)
    End Sub
    Private Sub AUTONO()
        'Dim CMD As New SqlClient.SqlCommand("SELECT MAX(BNO) AS TNO FROM inv", con)

        Dim CMD4 As New OleDb.OleDbCommand("SELECT MAX(docnum) AS TNO FROM oinward", con)


        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim CBNO As Int32 = IIf(IsDBNull(CMD4.ExecuteScalar) = False, CMD4.ExecuteScalar, 0)

        txtno.Text = CBNO + 1
        CMD4.Dispose()
        'con2.Close()
    End Sub

    Private Sub SAVEREC()

        If msel = 1 Or msel = 2 Then
            'trans = con.BeginTransaction
            ' mnewid = findid()
            'Dim CMD As New SqlClient.SqlCommand("DELETE FROM inv WHERE BNO=" & Val(txtbno.Text), con)
            'Dim CMD2 As New SqlClient.SqlCommand("DELETE FROM binv WHERE BNO=" & Val(txtbno.Text), con)
            msql = "select * from oinward where docnum=0 "
            msql2 = "select * from inward1 where docnum=0 "

            'msql = "delete from purchasehead where purcno=" & Val(txtbno.Text) & " and cmp_id='" & mcmpid & "' and cmpyr_id='" & mcmpyrid & "'"
            'msql2 = "delete from purchaseline where purcno=" & Val(txtbno.Text) & " and cmp_id='" & mcmpid & "' and cmpyr_id='" & mcmpyrid & "'"

            Dim CMD As New OleDb.OleDbCommand("delete from oinward where docnum=" & Val(txtno.Text), con)
            Dim CMD2 As New OleDb.OleDbCommand("delete from inward1 where docnum=" & Val(txtno.Text), con)


            Dim DA As New OleDb.OleDbDataAdapter(msql, con)
            Dim DA1 As New OleDb.OleDbDataAdapter(msql2, con)
            Dim CB As New OleDb.OleDbCommandBuilder(DA)
            Dim CB1 As New OleDb.OleDbCommandBuilder(DA1)
            Dim DS As New DataSet
            Dim DS1 As New DataSet

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            Dim TRANS As OleDbTransaction = con.BeginTransaction

            Try
                If msel = 2 Then
                    CMD.Transaction = TRANS
                    CMD2.Transaction = TRANS
                    CMD.ExecuteNonQuery()
                    CMD2.ExecuteNonQuery()
                    'CMD.Dispose()
                    'CMD2.Dispose()
                End If
                'End If
                'Dim DA As New SqlClient.SqlDataAdapter("SELECT * FROM inv", con)
                'Dim DA1 As New SqlClient.SqlDataAdapter("SELECT * FROM Binv", con)
                'Dim CB As New SqlClient.SqlCommandBuilder(DA)
                'Dim CB1 As New SqlClient.SqlCommandBuilder(DA1)




                'Try
                DA.SelectCommand.Transaction = TRANS
                DA1.SelectCommand.Transaction = TRANS

                DA.Fill(DS, "oinward")
                Dim DSROW As DataRow

                'Dim dsrow As DataRow
                DSROW = DS.Tables("oinward").NewRow


                DSROW.Item("docnum") = Val(txtno.Text)
                DSROW.Item("date") = Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd")
                DS.Tables("oinward").Rows.Add(DSROW)





                DA1.Fill(DS1, "inward1")
                Dim DSRW As DataRow
                DSRW = DS1.Tables("inward1").NewRow

                DSRW.Item("docnum") = Val(txtno.Text)
                DSRW.Item("date") = Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd")
                DSRW.Item("cardcode") = lblcardcode.Text
                DSRW.Item("cardname") = cmbparty.Text
                DSRW.Item("ordno") = txtordno.Text & vbNullString
                DSRW.Item("orddate") = Microsoft.VisualBasic.Format(CDate(mskorddate.Text), "yyyy-MM-dd")

                DSRW.Item("prntno") = txtprnno.Text & vbNullString
                DSRW.Item("u_areacode") = lblagent.Text & vbNullString
                DSRW.Item("remark") = txtremark.Text & vbNullString

                DS1.Tables("inward1").Rows.Add(DSRW)

                DA.Update(DS, "oinward")
                DA1.Update(DS1, "inward1")

                TRANS.Commit()
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                'Call SAVTAX()

                MsgBox("SUCCESSFULLY SAVED!")
                'If Microsoft.VisualBasic.MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                '    'Call TAKPRINT()
                'End If

                'ClearAllCtrls(Panel1, True)
                'LBLNETVAT.Text = ""
                'TXTAMT.Text = 0
                'TXTDISCPER.Text = 0
                'TXTDISCAMT.Text = 0
                'TXTTAX.Text = 0
                'TXTTAXAMT.Text = 0
                'TXTROUND.Text = 0
                'TXTTOTAL.Text = 0
                'flxhead()
                'ls1.Visible = False
                'ls2.Visible = False
                Call loadgrid()
                CLEAR(Me)
                disable(Me)
                cmdsave.Enabled = False
                If flx.Enabled = False Then flx.Enabled = True

            Catch EX As FieldAccessException
                If Not TRANS Is Nothing Then
                    TRANS.Rollback()
                End If
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                MsgBox(EX.Message)
                CLEAR(Me)
                disable(Me)
                cmdsave.Enabled = False
            Catch EX As ExecutionEngineException
                If Not TRANS Is Nothing Then
                    TRANS.Rollback()
                End If
                'trans.Rollback()
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                MsgBox(EX.Message)
                CLEAR(Me)
                disable(Me)
                cmdsave.Enabled = False
            Catch EX As ApplicationException
                If Not TRANS Is Nothing Then
                    TRANS.Rollback()
                End If
                'trans.Rollback()
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                MsgBox(EX.Message)
                CLEAR(Me)
                disable(Me)
                cmdsave.Enabled = False
            Catch EX As ArgumentException
                If Not TRANS Is Nothing Then
                    TRANS.Rollback()
                End If
                'trans.Rollback()
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                MsgBox(EX.Message)
                CLEAR(Me)
                disable(Me)
                cmdsave.Enabled = False
            Catch EX As OleDbException
                If Not TRANS Is Nothing Then
                    TRANS.Rollback()
                End If
                'If MSEL = 2 Then
                ' trans.Rollback()
                ' End If
                'MsgBox(EX.Message)
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                merr = Trim(EX.Message)
                MsgBox(merr)
                If InStr(merr, "PRIMARY KEY") > 0 Then
                    

                    Dim CMD3 As New OleDb.OleDbCommand("SELECT MAX(docnum) AS TNO FROM oinward", con)


                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If

                    Dim CBNO As Int32 = IIf(IsDBNull(CMD3.ExecuteScalar) = False, CMD3.ExecuteScalar, 0)

                    txtno.Text = CBNO + 1
                    CMD3.Dispose()
                    'con.Close()
                    Call SAVEREC()


                End If


            Catch EX As ConstraintException
                'trans.Rollback()
                If Not TRANS Is Nothing Then
                    TRANS.Rollback()
                End If
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                MsgBox(EX.Message)
                CLEAR(Me)
                disable(Me)
                cmdsave.Enabled = False
            Catch EX As Exception
                If Not TRANS Is Nothing Then
                    TRANS.Rollback()
                End If
                MsgBox(EX.Message)
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                CLEAR(Me)
                disable(Me)
                cmdsave.Enabled = False
            Finally

                CMD.Dispose()
                CMD2.Dispose()
                TRANS.Dispose()
            End Try

        End If
        CLEAR(Me)
        'enable(Me)
        If flx.Enabled = False Then flx.Enabled = True
        'Call flxhead()
        'ls1.Visible = False
        'ls2.Visible = False
    End Sub
    Private Sub loadgrid()

        If flx.Enabled = False Then flx.Enabled = True
        msql = "select * from inward1 where date='" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' and docnum=" & Val(txtno.Text) & " order by docnum"

        Dim CMD As New OleDb.OleDbCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Try
            ''Dim DR As SqlDataReader
            Dim DR As OleDb.OleDbDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                With flx
                    While DR.Read
                        .Rows = .Rows + 1
                        .Row = .Rows - 1

                        If IsDBNull(DR.Item("docnum")) = False Then
                            .set_TextMatrix(.Row, 1, DR.Item("docnum"))

                        End If
                        .set_TextMatrix(.Row, 2, DR.Item("date"))
                        .set_TextMatrix(.Row, 3, DR.Item("cardcode") & vbNullString)
                        .set_TextMatrix(.Row, 4, DR.Item("cardname") & vbNullString)
                        .set_TextMatrix(.Row, 5, DR.Item("ordno") & vbNullString)
                        If IsDBNull(DR.Item("orddate")) = False Then
                            .set_TextMatrix(.Row, 6, DR.Item("orddate"))
                        End If
                        .set_TextMatrix(.Row, 7, DR.Item("u_areacode") & vbNullString)
                        .set_TextMatrix(.Row, 9, DR.Item("prntno") & vbNullString)
                        .set_TextMatrix(.Row, 10, DR.Item("remark") & vbNullString)

                    End While
                End With
            End If

            DR.Close()

        Catch sqlEx As OleDbException  '
            MsgBox(sqlEx.Message)


        Catch ex As Exception
            'MsgBox("Check " & DR.Item("quality"))

            MsgBox(ex.Message)
            'MsgBox("Check " & DR.Item("quality"))
            'dr.close()
            'flx.Clear()
            'Call flxhead()
        End Try

        CMD.Dispose()







        'flx.Rows = flx.Rows + 1
        'flx.Row = flx.Rows - 1
        'flx.set_TextMatrix(flx.Row, 1, Val(txtno.Text))
        'flx.set_TextMatrix(flx.Row, 2, mskdate.Text)
        'flx.set_TextMatrix(flx.Row, 3, lblcardcode.Text & vbNullString)
        'flx.set_TextMatrix(flx.Row, 4, cmbparty.Text & vbNullString)
        'flx.set_TextMatrix(flx.Row, 5, txtordno.Text & vbNullString)
        'flx.set_TextMatrix(flx.Row, 6, mskorddate.Text)
        'flx.set_TextMatrix(flx.Row, 7, lblagent.Text & vbNullString)
        'flx.set_TextMatrix(flx.Row, 9, txtprnno.Text & vbNullString)
        'flx.set_TextMatrix(flx.Row, 10, txtremark.Text & vbNullString)
    End Sub
    Private Sub LOADDATA()
        'Dim CMD As New SqlClient.SqlCommand("SELECT * FROM inv WHERE BNO=" & Val(txtbno.Text), con)
        'Dim CMD1 As New SqlClient.SqlCommand("SELECT * FROM Binv WHERE BNO=" & Val(txtbno.Text) & " order by bno,sno", con)

        Dim CMD As New OleDb.OleDbCommand("SELECT * FROM oinward WHERE docnum=" & Val(txtno.Text), con)
        Dim CMD1 As New OleDb.OleDbCommand("SELECT * FROM inward1 WHERE docnum=" & Val(txtno.Text) & " order by docnum", con)


        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim DR As SqlClient.SqlDataReader
        'Dim DR1 As SqlClient.SqlDataReader

        Dim DR As OleDb.OleDbDataReader
        Dim DR1 As OleDb.OleDbDataReader

        DR = CMD.ExecuteReader


        If DR.HasRows = True Then
            While DR.Read


                txtno.Text = DR.Item("docnum")
                mskdate.Text = DR.Item("date")
                'lblcardcode.Text = DR.Item("cardcode") & vbNullString
                'cmbparty.Text = DR.Item("cardname") & vbNullString
                'txtordno.Text = DR.Item("ordno")
                'mskorddate.Text = DR.Item("orddate")
                'txtprnno.Text = DR.Item("prntno")
                'lblagent.Text = DR.Item("u_areacode")
                'txtremark.Text = DR.Item("remark")
            End While

        End If
        DR.Close()

        DR1 = CMD1.ExecuteReader
        If DR1.HasRows = True Then


            While DR1.Read
                lblcardcode.Text = DR1.Item("cardcode") & vbNullString
                cmbparty.Text = DR1.Item("cardname") & vbNullString
                txtordno.Text = DR1.Item("ordno")
                mskorddate.Text = DR1.Item("orddate")

                txtprnno.Text = DR1.Item("prntno")
                lblagent.Text = DR1.Item("u_areacode")
                txtremark.Text = DR1.Item("remark")


            End While

        End If
        DR1.Close()
        CMD.Dispose()
        CMD1.Dispose()



    End Sub

    Private Sub flxhead()
        flx.Rows = 1
        flx.Cols = 11
        flx.set_ColWidth(0, 500)
        flx.set_ColWidth(1, 700)
        flx.set_ColWidth(2, 1000)
        flx.set_ColWidth(3, 1000)
        flx.set_ColWidth(4, 1800)
        flx.set_ColWidth(5, 1000)
        flx.set_ColWidth(6, 1000)
        flx.set_ColWidth(7, 1000)
        flx.set_ColWidth(8, 1000)
        flx.set_ColWidth(9, 1000)
        flx.set_ColWidth(10, 1800)



        flx.Row = 0
        flx.Col = 0
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 0, "Sel")

        flx.Col = 1
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 1, "Date")

        flx.Col = 2
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 2, "Docnum")

        flx.Col = 3
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 3, "Cardcode")

        flx.Col = 4
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 4, "Cardname")

        flx.Col = 5
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 5, "Ord.No")

        flx.Col = 6
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 6, "Ord.Date")

        flx.Col = 7
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 7, "Agent")

        flx.Col = 9
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 9, "Print on No")

        flx.Col = 10
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 10, "Remark")




        flx.set_ColAlignment(3, 2)
        flx.set_ColAlignment(4, 2)
        flx.set_ColAlignment(5, 2)
        flx.set_ColAlignment(7, 2)
        flx.set_ColAlignment(8, 2)
        flx.set_ColAlignment(10, 2)

    End Sub

    Private Sub Deldata()
        Try
            If msel = 3 Then
                If Microsoft.VisualBasic.MsgBox("R U Sure!", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'If MsgBox("R U Sure!", MsgBoxStyle.Critical, MsgBoxResult.Yes) = MsgBoxResult.Yes Then
                    Dim CMD As New OleDb.OleDbCommand("DELETE FROM oinward WHERE docnum=" & Val(txtno), con)
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If
                    CMD.ExecuteNonQuery()
                    CMD.Dispose()

                    Dim CMD1 As New OleDb.OleDbCommand("DELETE FROM inward1 WHERE docnum=" & Val(txtno), con)
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If
                    CMD1.ExecuteNonQuery()
                    CMD1.Dispose()

                    MsgBox("Deleted!")
                End If
            End If
        Catch ex As Exception
            'tranDEL.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        'txtitemname.Focus()
        msel = cmdadd.Tag
        'enable(Me)
       
        CLEAR(Me)
        enable(Me)
        Call AUTONO()
        Call flxhead()
        txtno.Enabled = False
        If cmdsave.Enabled = False Then
            cmdsave.Enabled = True
        End If
        mskdate.Focus()
        'txtno.Focus()
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdedit.Click
        msel = cmdedit.Tag
        'enable(Me)
        CLEAR(Me)
        enable(Me)
        Call flxhead()
        If cmdsave.Enabled = False Then
            cmdsave.Enabled = True
        End If
        txtno.Focus()

    End Sub

    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.Click
        msel = cmddisp.Tag
        'enable(Me)
        CLEAR(Me)
        enable(Me)
        Call flxhead()
        txtno.Focus()
    End Sub

    Private Sub cmddel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddel.Click
        msel = cmddel.Tag
        enable(Me)
        txtno.Focus()
    End Sub

    Private Sub txtno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtno.LostFocus
        If msel > 1 Then

            Call LOADDATA()
            If msel = 3 Then
                Call Deldata()
            End If
        End If
    End Sub

    Private Sub txtno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtno.TextChanged

    End Sub

    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        Call SAVEREC()
    End Sub

    Private Sub cmbparty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbparty.LostFocus
        lblcardcode.Text = findpartycode(cmbparty, 1, 0)
        lblagent.Text = findpartycode(cmbparty, 0, 1)


    End Sub

    Private Sub cmbparty_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbparty.SelectedIndexChanged

    End Sub

    Private Sub cmbparty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbparty.TextChanged
        Call autosearch(sender)
    End Sub

    Private Sub mskdate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles mskdate.GotFocus
        If msel = 1 Then
            'Dim currentdate As System.DateTime
            mskdate.Text = System.DateTime.Today
        End If
    End Sub

    Private Sub mskdate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskdate.MaskInputRejected
        
    End Sub

    Private Sub cmdprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdprint.Click
        If MsgBox("print in flx Grid", MsgBoxStyle.YesNo + MsgBoxStyle.Critical) = MsgBoxResult.Yes Then
            For i = 1 To flx.Rows - 1
                If Len(Trim(flx.get_TextMatrix(i, 0))) > 0 Then
                    lblcardcode.Text = findpartycode2(flx.get_TextMatrix(i, 4), 1, 0)
                    If MsgBox("Print", MsgBoxStyle.YesNo + MsgBoxStyle.Critical) = MsgBoxResult.Yes Then
                        Call crystalrr()
                        'Call crystalnew()
                    End If
                End If
            Next i
        Else
            Call crystalrr()
        End If

    End Sub

    Private Sub crystalnew()
        'Dim cryRpt As New ReportDocument
        'Dim crtableLogoninfos As New TableLogOnInfos
        'Dim crtableLogoninfo As New TableLogOnInfo
        'Dim crConnectionInfo As New ConnectionInfo
        'Dim CrTables As Tables
        'Dim CrTable As Table

        'cryRpt.Load("c:\Order back print.rpt")
        ''cryRpt.Load("PUT CRYSTAL REPORT PATH HERE\CrystalReport1.rpt")

        'With crConnectionInfo
        '    .ServerName = Trim(mkserver)
        '    .DatabaseName = Trim(dbnam)
        '    .UserID = Trim(dbuser)
        '    .Password = Trim(mkpwd)
        'End With

        'CrTables = cryRpt.Database.Tables
        'For Each CrTable In CrTables
        '    crtableLogoninfo = CrTable.LogOnInfo
        '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
        '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
        'Next

        'view1.ReportSource = cryRpt
        'Me.view1.PrintReport()
        'view1.Refresh()
    End Sub


    Private Sub crystalrr()

        Me.Cursor = Cursors.WaitCursor
        Dim cryRpt As New ReportDocument()
        Me.view1.Refresh()

        'cryRpt.Load(Trim(mreppath) & "Company Analysis Report.rpt")
       
        cryRpt.Load(Trim(mreppath) & "Order back print.rpt")
        'cryRpt.Load("c:\Order back print.rpt")
        'cryRpt.Load(Trim(mreppath) & "Inner Packing Slip RRdel.rpt")
        'cryRpt.Load("c:\Area Wise Summary Report.rpt")

       

        'cryRpt.Load("e:\kalai\Area Wise Summary Report.rpt")
        'report.SetDatabaseLogon("root", "", "localhost", "aerospace")
        'cryRpt.SetDatabaseLogon("sa", "SEsA@536", "192.168.0.5", dbnam)

        CrystalReportLogOn(cryRpt, Trim(mkserver), dbnam, Trim(dbuser), Trim(mkpwd))

        'CrystalReportLogOn(cryRpt, "192.168.0.5", dbnam, "sa", "iTTsA@536")

        



        'cryRpt.SetParameterValue("Dockey@", Val(lblcardcode.Text))
        'setlogon(cryRpt)
        'cryRpt.SetParameterValue("AREA@SELECT DISTINCT U_AreaCode From OCRD Order BY U_AreaCode", "TN11")
        cryRpt.SetParameterValue("CardCode@", lblcardcode.Text)
        'cryRpt.SetParameterValue("FromDate", Convert.ToDateTime(mskdatefr.Text))
        '' Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy/MM/dd"))
        'cryRpt.SetParameterValue("ToDate", DateTime.Now())
        'cryRpt.SetParameterValue("DocKey", Val(lbldocentry.Text))

        'Dim MyReport As ReportClass = DirectCast(ReportViewer.ReportSource, ReportClass)
        'MyReport.PrinterOptions.PrinterName = "name of default or desired printer here"
        'MyReport.PrintToPrinter()
        'Me.view1.printerOptions.Printername = "Bullzip PDF Printer"
        Me.view1.ReportSource = cryRpt
        Me.view1.PrintReport()
        'view1.PrintToPrinter(1, False, 1, 1)
        'Me.View1.ReportSource = cryRpt
        Me.view1.Refresh()
        cryRpt.Dispose()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub cmddispall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddispall.Click
        Call loaddataall()
    End Sub
    Private Sub loaddataall()

        Dim msql As String
        If Len(Trim(cmbparty.Text)) > 0 Then
            msql = "select * from inward1 where date>='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' and cardcode='" & Trim(lblcardcode.Text) & "' order by docnum"
        Else
            msql = "select * from inward1 where date>='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "' order by docnum"
        End If
        'msql = "select * from inward1 where date>='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "'"


        Dim CMD As New OleDb.OleDbCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        CMD.CommandTimeout = 300
        'MsgBox(msql)
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()
        flx.Clear()
        Call flxhead()

        Try
            ''Dim DR As SqlDataReader
            Dim DR As OleDb.OleDbDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                With flx
                    While DR.Read
                        .Rows = .Rows + 1
                        .Row = .Rows - 1
                   
                        If IsDBNull(DR.Item("docnum")) = False Then
                            .set_TextMatrix(.Row, 1, DR.Item("docnum"))
                        
                        End If
                        .set_TextMatrix(.Row, 2, DR.Item("date"))
                        .set_TextMatrix(.Row, 3, DR.Item("cardcode") & vbNullString)
                        .set_TextMatrix(.Row, 4, DR.Item("cardname") & vbNullString)
                        .set_TextMatrix(.Row, 5, DR.Item("ordno") & vbNullString)
                        If IsDBNull(DR.Item("orddate")) = False Then
                            .set_TextMatrix(.Row, 6, DR.Item("orddate"))
                        End If
                        .set_TextMatrix(.Row, 7, DR.Item("u_areacode") & vbNullString)
                        .set_TextMatrix(.Row, 9, DR.Item("prntno") & vbNullString)
                        .set_TextMatrix(.Row, 10, DR.Item("remark") & vbNullString)

                    End While
                End With
            End If

            DR.Close()
            
        Catch sqlEx As OleDbException  '
            MsgBox(sqlEx.Message)


        Catch ex As Exception
            'MsgBox("Check " & DR.Item("quality"))

            MsgBox(ex.Message)
            'MsgBox("Check " & DR.Item("quality"))
            'dr.close()
            flx.Clear()
            Call flxhead()
        End Try

        CMD.Dispose()

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
    End Sub

    Private Sub mskdatefr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles mskdatefr.GotFocus
        If cmbparty.Enabled = False Then cmbparty.Enabled = True
    End Sub

    Private Sub mskdatefr_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskdatefr.MaskInputRejected

    End Sub

    Private Sub lblagent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblagent.Click

    End Sub
End Class
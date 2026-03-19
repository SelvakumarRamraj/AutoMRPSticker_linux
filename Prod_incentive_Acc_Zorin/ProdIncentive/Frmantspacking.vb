Imports System.IO
Imports System.Math
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.VbStrConv
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Public Class frmantspacking
    Dim msel As Int16
    Dim msql, merr As String
    Dim msql2 As String
    Dim i, j As Int32
    Dim mtik As Int16

    Private Sub Frmantspacking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        CLEAR(Me)
        view1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        Call loadcode()
        view1.Show()

        
    End Sub
    Private Sub AUTONO()
        'Dim CMD As New SqlClient.SqlCommand("SELECT MAX(BNO) AS TNO FROM inv", con)

        Dim CMD4 As New OleDb.OleDbCommand("SELECT MAX(docentry) AS TNO FROM zodesp", con)


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
            msql = "select * from zodesp where docentry=0 "
            msql2 = "select * from zdesp1 where docentry=0 "

            'msql = "delete from purchasehead where purcno=" & Val(txtbno.Text) & " and cmp_id='" & mcmpid & "' and cmpyr_id='" & mcmpyrid & "'"
            'msql2 = "delete from purchaseline where purcno=" & Val(txtbno.Text) & " and cmp_id='" & mcmpid & "' and cmpyr_id='" & mcmpyrid & "'"

            Dim CMD As New OleDb.OleDbCommand("delete from zodesp where docentry=" & Val(txtno.Text), con)
            Dim CMD2 As New OleDb.OleDbCommand("delete from zdesp1 where docentry=" & Val(txtno.Text), con)


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

                DA.Fill(DS, "zodesp")
                Dim DSROW As DataRow

                'Dim dsrow As DataRow
                DSROW = DS.Tables("zodesp").NewRow


                DSROW.Item("docentry") = Val(txtno.Text)
                DSROW.Item("docdate") = Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd")
                DSROW.Item("docqty") = Val(lblqty.Text)
                DSROW.Item("doctotal") = Val(lblamt.Text)
                DS.Tables("zodesp").Rows.Add(DSROW)



                For j = 1 To flx.Rows - 1
                    If Len(Trim(flx.get_TextMatrix(j, 1))) > 0 Then
                        DA1.Fill(DS1, "zdesp1")
                        Dim DSRW As DataRow
                        DSRW = DS1.Tables("zdesp1").NewRow

                        DSRW.Item("docentry") = Val(txtno.Text)
                        DSRW.Item("docdate") = Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd")
                        DSRW.Item("itemcode") = flx.get_TextMatrix(j, 0)
                        DSRW.Item("itemname") = flx.get_TextMatrix(j, 1)
                        DSRW.Item("style") = flx.get_TextMatrix(j, 2)
                        DSRW.Item("size") = flx.get_TextMatrix(j, 3)
                        DSRW.Item("qty") = Val(flx.get_TextMatrix(j, 4))
                        DSRW.Item("rate") = Val(flx.get_TextMatrix(j, 5))
                        DSRW.Item("linetotal") = Val(flx.get_TextMatrix(j, 6))
                        DSRW.Item("boxno") = flx.get_TextMatrix(j, 7)
                        DS1.Tables("zdesp1").Rows.Add(DSRW)
                    End If
                Next j

                DA.Update(DS, "zodesp")
                DA1.Update(DS1, "zdesp1")

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
                cmdclear.PerformClick()
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
                'Call loadgrid()
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


                    Dim CMD3 As New OleDb.OleDbCommand("SELECT MAX(docentry) AS TNO FROM zodesp", con)


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
    Private Sub LOADDATA()
        'Dim CMD As New SqlClient.SqlCommand("SELECT * FROM inv WHERE BNO=" & Val(txtbno.Text), con)
        'Dim CMD1 As New SqlClient.SqlCommand("SELECT * FROM Binv WHERE BNO=" & Val(txtbno.Text) & " order by bno,sno", con)

        Dim CMD As New OleDb.OleDbCommand("SELECT * FROM zodesp WHERE docentry=" & Val(txtno.Text), con)
        Dim CMD1 As New OleDb.OleDbCommand("SELECT * FROM zdesp1 WHERE docentry=" & Val(txtno.Text) & " order by docentry", con)


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


                txtno.Text = DR.Item("docentry")
                mskdate.Text = DR.Item("docdate")
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

            With flx
                While DR1.Read
                    .Rows = .Rows + 1
                    .Row = .Rows - 1
                    .set_TextMatrix(.Row, 0, DR1.Item("itemcode"))
                    .set_TextMatrix(.Row, 1, DR1.Item("itemname"))
                    .set_TextMatrix(.Row, 2, DR1.Item("style") & vbNullString)
                    .set_TextMatrix(.Row, 3, DR1.Item("size") & vbNullString)
                    .set_TextMatrix(.Row, 4, DR1.Item("qty"))
                    .set_TextMatrix(.Row, 5, DR1.Item("rate"))
                    .set_TextMatrix(.Row, 6, DR1.Item("linetotal"))
                    .set_TextMatrix(.Row, 7, DR1.Item("boxno") & vbNullString)

                End While
            End With

        End If
        DR1.Close()
        CMD.Dispose()
        CMD1.Dispose()

        Call flxcalctot()
        Call TOT()


    End Sub

    Private Sub loadcode()
        msql = "Select b.itemcode,b.itemname,isnull(b.u_style,'') u_style,isnull(b.u_size,'') u_size,isnull(p.Price,0) price from oitm b " & vbCrLf _
          & "left join ITM1 p on p.ItemCode=b.ItemCode and p.PriceList=1 " & vbCrLf _
          & "where itmsgrpcod not in (102,105,111,112,113,110) order by itemname"

        'msql = "Select itemcode,itemname,u_style,u_size, from oitm where itmsgrpcod not in (102,105,111,112,113,110) order by itemname"


        Dim cmd As New OleDb.OleDbCommand(msql, con)


        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim dr As OleDb.OleDbDataReader
        dr = cmd.ExecuteReader
        If dr.HasRows = True Then
            Call flxcodehead()
            With flxcode
                While dr.Read
                    .Rows = .Rows + 1
                    .Row = .Rows - 1
                    .set_TextMatrix(.Row, 0, dr.Item("itemcode"))
                    .set_TextMatrix(.Row, 1, dr.Item("Itemname") & vbNullString)
                    .set_TextMatrix(.Row, 2, dr.Item("u_style") & vbNullString)
                    .set_TextMatrix(.Row, 3, dr.Item("u_size") & vbNullString)
                    .set_TextMatrix(.Row, 4, dr.Item("price"))
                End While
            End With
        End If
        dr.Close()
        cmd.Dispose()


        'msql = "SELECT series from nnm1 where objectcode='OTAR' and indicator='" & CMBYR.Text & "'"
        'Dim CMDchk As New OleDb.OleDbCommand(msql, con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'Dim DRchk As OleDb.OleDbDataReader
        'DRchk = CMDchk.ExecuteReader


        'If DRchk.HasRows = True Then
        '    While DRchk.Read
        '        txtyno.Text = DRchk.Item("series")
        '    End While
        'End If
        'DRchk.Close()
        'CMDchk.Dispose()

    End Sub
    Private Sub flxhead()
        flx.Rows = 1
        flx.Cols = 8
        flx.set_ColWidth(0, 1000)
        flx.set_ColWidth(1, 2500)
        flx.set_ColWidth(2, 1000)
        flx.set_ColWidth(3, 1000)
        flx.set_ColWidth(4, 1100)
        flx.set_ColWidth(5, 1000)
        flx.set_ColWidth(6, 1300)
        flx.set_ColWidth(7, 1100)



        flx.Row = 0
        flx.Col = 0
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 0, "Code")

        flx.Col = 1
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 1, "Item Name")

        flx.Col = 2
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 2, "Style")

        flx.Col = 3
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 3, "Size")

        flx.Col = 4
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 4, "Qty")
        flx.Col = 5
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 5, "Rate")

        flx.Col = 6
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 6, "Line Total")

        flx.Col = 7
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 7, "Box No")

        flx.set_ColAlignment(0, 2)
        flx.set_ColAlignment(1, 2)
        flx.set_ColAlignment(2, 2)
        flx.set_ColAlignment(3, 2)
        flx.set_ColAlignment(7, 2)

    End Sub

    Private Sub flxcodehead()

        flxcode.Rows = 1
        flxcode.Cols = 5
        flxcode.set_ColWidth(0, 1000)
        flxcode.set_ColWidth(1, 2500)
        flxcode.set_ColWidth(2, 1000)
        flxcode.set_ColWidth(3, 1000)
        flxcode.set_ColWidth(4, 1000)




        flxcode.Row = 0
        flxcode.Col = 0
        flxcode.CellAlignment = 3
        flxcode.CellFontBold = True
        flxcode.set_TextMatrix(0, 0, "Code")

        flxcode.Col = 1
        flxcode.CellAlignment = 3
        flxcode.CellFontBold = True
        flxcode.set_TextMatrix(0, 1, "Item Name")

        flxcode.Col = 2
        flxcode.CellAlignment = 3
        flxcode.CellFontBold = True
        flxcode.set_TextMatrix(0, 2, "Style")

        flxcode.Col = 3
        flxcode.CellAlignment = 3
        flxcode.CellFontBold = True
        flxcode.set_TextMatrix(0, 3, "Size")

        flxcode.Col = 4
        flxcode.CellAlignment = 3
        flxcode.CellFontBold = True
        flxcode.set_TextMatrix(0, 4, "Rate")

        flx.set_ColAlignment(0, 2)
        flx.set_ColAlignment(0, 2)
        flx.set_ColAlignment(0, 3)
        flx.set_ColAlignment(0, 4)


    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
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
    Private Sub Deldata()
        Try
            If msel = 3 Then
                If Microsoft.VisualBasic.MsgBox("R U Sure!", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'If MsgBox("R U Sure!", MsgBoxStyle.Critical, MsgBoxResult.Yes) = MsgBoxResult.Yes Then
                    Dim CMD As New OleDb.OleDbCommand("DELETE FROM zodesp WHERE docentry=" & Val(txtno.Text), con)
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If
                    CMD.ExecuteNonQuery()
                    CMD.Dispose()

                    Dim CMD1 As New OleDb.OleDbCommand("DELETE FROM zdesp1 WHERE docentry=" & Val(txtno.Text), con)
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

    Private Sub txtno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtno.TextChanged

    End Sub

    Private Sub cmdprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdprint.Click
        
        If MsgBox("Print Itemwise Packing", MsgBoxStyle.Critical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            mtik = 1
        ElseIf MsgBox("Print Boxwise Packing", MsgBoxStyle.Critical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            mtik = 2
        Else
            mtik = 0
        End If
        Call crystalrr()


    End Sub
    Private Sub crystalrr()

        Me.Cursor = Cursors.WaitCursor
        Dim cryRpt As New ReportDocument()
        ' Me.view1.Refresh()

        'ants--\\192.166.0.5\b1_shr\Reports

        'cryRpt.Load(Trim(mreppath) & "Company Analysis Report.rpt")

        If mtik = 0 Then
            cryRpt.Load(Trim(mreppath) & "Packing conslidate.rpt")
        ElseIf mtik = 1 Then
            cryRpt.Load(Trim(mreppath) & "Packing Itemwise.rpt")
        ElseIf (mtik = 2) Then
            cryRpt.Load(Trim(mreppath) & "Packing boxwise.rpt")
        End If
        'cryRpt.Load(Trim(mreppath) & "Order back print.rpt")
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
        '*cryRpt.SetParameterValue("CardCode@", lblcardcode.Text)

        'cryRpt.SetParameterValue("FromDate", Convert.ToDateTime(mskdatefr.Text))
        '' Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy/MM/dd"))
        'cryRpt.SetParameterValue("ToDate", DateTime.Now())
        cryRpt.SetParameterValue("@Docentry", Val(txtno.Text))

        'Dim MyReport As ReportClass = DirectCast(ReportViewer.ReportSource, ReportClass)
        'MyReport.PrinterOptions.PrinterName = "name of default or desired printer here"
        'MyReport.PrintToPrinter()
        'Me.view1.printerOptions.Printername = "Bullzip PDF Printer"
        Me.view1.ReportSource = cryRpt
        'Me.view1.PrintReport()
        'view1.PrintToPrinter(1, False, 1, 1)
        'Me.View1.ReportSource = cryRpt
        Me.view1.Refresh()
        'cryRpt.Dispose()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub mskdate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles mskdate.GotFocus
        If msel = 1 Then
            'Dim currentdate As System.DateTime
            mskdate.Text = System.DateTime.Today
        End If
    End Sub

    Private Sub mskdate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mskdate.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If msel = 1 Then
                Call flxhead()
                flx.Rows = flx.Rows + 1
                flx.Row = flx.Rows - 1
                flx.Row = 1
                flx.Col = 1
                flx.Focus()
            End If

        End If
    End Sub

    Private Sub mskdate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskdate.MaskInputRejected

    End Sub

    Private Sub flx_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flx.Enter

    End Sub

    Private Sub flx_KeyPressEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyPressEvent) Handles flx.KeyPressEvent

        If flx.Col >= 4 And flx.Col <= 5 Or (flx.Col = 7) Then
            editflx(flx, e.keyAscii, cmdsave)
        End If
        If e.keyAscii = 8 Then
            If Len(Trim(flx.get_TextMatrix(flx.Row, flx.Col))) = 0 Then
                e.keyAscii = 27
            End If
        End If


        If (flx.Col >= 4 And flx.Col <= 7) Then

            If InStr(1, "0123456789.-", Chr(e.keyAscii)) = 0 Then
                If e.keyAscii <> 8 And e.keyAscii <> 13 Then
                    e.keyAscii = 0
                End If
            End If


        End If
        If (flx.Col = 0) Or (flx.Col >= 1 And flx.Col <= 3) Then
            e.keyAscii = Asc(UCase(Chr(e.keyAscii)))
        End If

        If e.keyAscii = 13 Then
            'If flx.Col = 0 Then
            'flx.set_TextMatrix(flx.Row, 0, flx.Rows - 1)
            'End If
            If flx.Col = 1 Then
                If Len(Trim(flx.get_TextMatrix(flx.Row, 1))) = 0 Then
                    flxcode.Visible = True
                    flxcode.Row = flxcode.Row
                    flxcode.Col = 0
                    flxcode.Focus()
                End If
            End If
            If flx.Col = 0 Then
                flx.Col = 1
            End If


            If flx.Col = 6 Then
                flx.Col = 7
            End If
        End If





        If flx.Col = 5 And Val(flx.get_TextMatrix(flx.Row, 4)) = 0 Then
            'If CHKTAX.value = 0 Then
            flx.Col = 4
            'End If
        End If

        If flx.Col = 6 And Val(flx.get_TextMatrix(flx.Row, 5)) = 0 Then
            'If CHKTAX.value = 0 Then
            flx.Col = 5

            'End If
        End If

        If flx.Col = 7 And Val(flx.get_TextMatrix(flx.Row, 6)) = 0 Then
            'If CHKTAX.value = 0 Then
            flx.Col = 6
            'End If
        End If

        Call flxcalc()
        Call TOT()
    End Sub
    Private Sub flxcalc()

        'FLX.set_TextMatrix(FLX.Row, 4, Microsoft.VisualBasic.Format(Round(Val(FLX.get_TextMatrix(FLX.Row, 2)) * Val(FLX.get_TextMatrix(FLX.Row, 3)), 2), "#######0.00"))
        If Val(flx.get_TextMatrix(flx.Row, 5)) > 0 Then
            flx.set_TextMatrix(flx.Row, 6, Val(flx.get_TextMatrix(flx.Row, 4)) * Val(flx.get_TextMatrix(flx.Row, 5)))


            'flx.set_TextMatrix(flx.Row, 6, Microsoft.VisualBasic.Format(Val(flx.get_TextMatrix(flx.Row, 4)) + (Round(Val(flx.get_TextMatrix(flx.Row, 4)) * Val(flx.get_TextMatrix(flx.Row, 5)) / 100, 2)), "#######0.00"))
            'flx.set_TextMatrix(flx.Row, 14, Microsoft.VisualBasic.Format((Round(Val(flx.get_TextMatrix(flx.Row, 4)) * Val(flx.get_TextMatrix(flx.Row, 5)) / 100, 2)), "#######0.00"))
        End If

    End Sub
    Private Sub TOT()
        Dim I
        lblqty.Text = 0
        lblamt.Text = 0

        
        For I = FLX.FixedRows To FLX.Rows - 1
            lblqty.Text = Microsoft.VisualBasic.Format(Val(lblqty.Text) + Val(flx.get_TextMatrix(I, 4)), "#######0.00")
            ' LBLMTR.caption = Format(Val(LBLMTR.caption) + Val(FLX.TextMatrix(I, 4)), "########0.00")
            lblamt.Text = Microsoft.VisualBasic.Format(Val(lblamt.Text) + Val(flx.get_TextMatrix(I, 6)), "#########0.00")
            'lblamt.Caption = Format(Val(lblamt.Caption) + Val(flx.TextMatrix(i, 7)), "########0.00")
        Next I
    End Sub
    Private Sub flxcalctot()
        Dim I
        'TXTAMT.Text = 0


        For I = FLX.FixedRows To FLX.Rows - 1
            'FLX.set_TextMatrix(I, 4, Microsoft.VisualBasic.Format(Round(Val(FLX.get_TextMatrix(I, 2)) * Val(FLX.get_TextMatrix(I, 3)), 2), "#######0.00"))
            If Val(flx.get_TextMatrix(I, 5)) > 0 Then
                flx.set_TextMatrix(I, 6, Val(flx.get_TextMatrix(I, 4)) * Val(flx.get_TextMatrix(I, 5)))
                
            End If
            
        Next I
    End Sub

    Private Sub flxcode_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxcode.Enter

    End Sub

    Private Sub flxcode_KeyPressEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyPressEvent) Handles flxcode.KeyPressEvent
        If e.keyAscii = 13 Then
            flxcode.Visible = False
            flx.set_TextMatrix(flx.Row, 0, flxcode.get_TextMatrix(flxcode.Row, 0))
            flx.set_TextMatrix(flx.Row, 1, flxcode.get_TextMatrix(flxcode.Row, 1))
            flx.set_TextMatrix(flx.Row, 2, flxcode.get_TextMatrix(flxcode.Row, 2))
            flx.set_TextMatrix(flx.Row, 3, flxcode.get_TextMatrix(flxcode.Row, 3))
            flx.set_TextMatrix(flx.Row, 5, flxcode.get_TextMatrix(flxcode.Row, 4))



            flx.Row = flx.Row
            flx.Col = 4
            flx.Focus()
        Else
            If e.keyAscii <> 27 Then
                searchflx(flxcode, e.keyAscii, 1)
            Else
                e.keyAscii = 0
                flxcode.Visible = False
                flx.Row = flx.Row
                flx.Col = 0
                flx.Focus()
            End If

        End If
    End Sub

    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        Call SAVEREC()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR(Me)
        Call flxhead()
        lblqty.Text = 0
        lblamt.Text = 0
        disable(Me)
        cmdsave.Enabled = False
    End Sub

    Private Sub flx_KeyUpEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyUpEvent) Handles flx.KeyUpEvent
        If e.keyCode = Keys.F9 Then
            If flx.Row < flx.Rows - 1 Then
                flx.Row = flx.Row + 1
                flx.RemoveItem(flx.Row)
            Else
                flx.RemoveItem(flx.Row)
            End If
        End If
    End Sub

    Private Sub cmdexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexcel.Click
        msql = "select itemcode,itemname,style,size,sum(qty) qty,rate,sum(linetotal) linetotal from zdesp1 where docentry>=" & Val(txtbno1.Text) & " and docentry<=" & Val(txtbno2.Text) & " group by itemcode,itemname,style,size,rate  order by itemcode,style,size"
        Call exportexcelDataqry(msql)
    End Sub
End Class
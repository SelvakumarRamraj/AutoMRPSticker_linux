Imports System.IO
Imports System.Math
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.VbStrConv
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource

Public Class Frmantscan
    Dim msel, i, j, n, k As Int16
    Dim msql, msql2, merr, sqlqry As String
    Dim Foundt As Boolean = False
    Dim mcompp As String
    Dim mtotqty As Int32
    Private Sub Frmantscan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        CLEAR(Me)
        mcompp = "prodbarcode.dbo."
        cmbtype.Items.Add("RG")
        cmbtype.Items.Add("RW")
        cmbtype.Items.Add("SC")

        ' view1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        'Call loadcode()
        'loadcombo("ofpr", "code", cmbyr, "code")
        Call gridhead()
        Call gridhead2()
        dv.Rows.Clear()
        dv1.Rows.Clear()
        loadcombo("ofpr", "code", cmbyr, "code")
        loadcombo("[@inm_oolm]", "u_opercode", cmbprocessfr, "u_opercode")
        loadcombo("[@inm_oolm]", "u_opercode", cmbprocessto, "u_opercode")
        'loadcombo("oolm", "u_lineno", cmbprocessfr, "u_opercode")
        cmbyr.Text = mpostperiod


    End Sub
    Private m_SelectedStyle As DataGridViewCellStyle
    Private m_SelectedRow As Integer = -1
    Private Sub deletecol()
        For k = 0 To DV.Rows.Count - 1
            For i As Integer = DV.Columns.Count - 1 To 0 Step -1
                If Len(Trim(DV.Columns(i).Name)) = 0 Then
                    DV.Columns.Remove(i)
                End If

            Next i
        Next k

    End Sub
    Private Sub AUTONO()
        'Dim CMD As New SqlClient.SqlCommand("SELECT MAX(BNO) AS TNO FROM inv", con)

        Dim CMD4 As New SqlCommand("SELECT MAX(docnum) AS TNO FROM oscan", con4)


        If con4.State = ConnectionState.Closed Then
            con4.Open()
        End If

        Dim CBNO As Int32 = IIf(IsDBNull(CMD4.ExecuteScalar) = False, CMD4.ExecuteScalar, 0)

        txtno.Text = CBNO + 1
        CMD4.Dispose()
        'con2.Close()
    End Sub
    Private Sub Deldata()
        Try
            If msel = 3 Then
                If Microsoft.VisualBasic.MsgBox("R U Sure!", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'If MsgBox("R U Sure!", MsgBoxStyle.Critical, MsgBoxResult.Yes) = MsgBoxResult.Yes Then
                    Dim CMD As New SqlCommand("DELETE FROM oscan WHERE docnum=" & Val(txtno.Text), con4)
                    If con4.State = ConnectionState.Closed Then
                        con4.Open()
                    End If
                    CMD.ExecuteNonQuery()
                    CMD.Dispose()

                    Dim CMD1 As New SqlCommand("DELETE FROM scan1 WHERE docnum=" & Val(txtno.Text), con4)
                    If con4.State = ConnectionState.Closed Then
                        con4.Open()
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
    Private Sub loadcutno()
        msql = "select b.u_itemcode,c.itemname,c.U_Style,c.U_Size, b.U_CutNo,b.DocEntry woentry from [@INM_WOR8] b" & vbCrLf _
            & "left join OITM c on c.ItemCode=b.U_ItemCode " & vbCrLf _
            & "where(b.U_ItemCode Is Not null)" & vbCrLf _
            & "group by b.u_itemcode,c.itemname,c.U_Style,c.U_Size, b.U_CutNo,b.DocEntry"

    End Sub
   

    
    Private Sub gridhead()

        dv.ColumnCount = 9
        dv.Columns(0).Name = "ItemCode"
        dv.Columns(1).Name = "Itemname"
        dv.Columns(2).Name = "Style"
        dv.Columns(3).Name = "Size"
        dv.Columns(4).Name = "Cutno"
        dv.Columns(5).Name = "Wono"
        dv.Columns(6).Name = "Qty"
        dv.Columns(7).Name = "Cutno2"
        'dv.Columns(8).Name = "DeliveryNo"

        dv.Columns(0).Width = 100
        dv.Columns(1).Width = 250
        dv.Columns(2).Width = 75
        dv.Columns(3).Width = 75
        dv.Columns(4).Width = 100
        dv.Columns(5).Width = 100
        dv.Columns(6).Width = 100
        dv.Columns(7).Width = 100
        'dv.Columns(8).Width = 100


        Dv.ColumnHeadersDefaultCellStyle.Font = New Font(Dv.Font, FontStyle.Bold)
        Dv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub


    Private Sub gridhead2()

        dv1.ColumnCount = 9
        dv1.Columns(0).Name = "ItemCode"
        dv1.Columns(1).Name = "Itemname"
        dv1.Columns(2).Name = "Style"
        dv1.Columns(3).Name = "Size"
        dv1.Columns(4).Name = "Cutno"
        dv1.Columns(5).Name = "Wono"
        dv1.Columns(6).Name = "Qty"
        dv1.Columns(7).Name = "Cutno2"
        dv1.Columns(8).Name = "DeliveryNo"

        dv1.Columns(0).Width = 100
        dv1.Columns(1).Width = 200
        dv1.Columns(2).Width = 75
        dv1.Columns(3).Width = 75
        dv1.Columns(4).Width = 50
        dv1.Columns(5).Width = 100
        dv1.Columns(6).Width = 100
        dv1.Columns(7).Width = 100
        dv1.Columns(8).Width = 100
        dv1.ColumnHeadersDefaultCellStyle.Font = New Font(dv1.Font, FontStyle.Bold)
        dv1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub


    'Private Sub loadgrid()
    '    Call gridhead()
    '    Dim CMD As New OleDb.OleDbCommand("SELECT * FROM allowancemaster order by allowcode", con)

    '    If con.State = ConnectionState.Closed Then
    '        con.Open()
    '    End If
    '    'Dim DR As SqlClient.SqlDataReader
    '    'Dim DR1 As SqlClient.SqlDataReader

    '    Dim DR As OleDb.OleDbDataReader
    '    DR = CMD.ExecuteReader
    '    Dv.Rows.Clear()
    '    If DR.HasRows = True Then
    '        While DR.Read
    '            n = Dv.Rows.Add()
    '            Dv.Rows.Item(n).Cells(0).Value = DR.Item("allowcode")
    '            Dv.Rows.Item(n).Cells(1).Value = DR.Item("allowance")

    '        End While
    '    End If
    '    DR.Close()
    '    CMD.Dispose()

    'End Sub


    Private Sub saverec()


        If msel = 2 Then
            Dim CMD As New SqlCommand("delete from oscan where docnum=" & Val(txtno.Text) & " and period='" & cmbyr.Text & "'", con4)
            '                                  & Microsoft.VisualBasic.Format(CDate(mskddate.Text), "yyyy-MM-dd") & "' where docnum=" & Val(flxv.get_TextMatrix(k, 0)), con)
            If con4.State = ConnectionState.Closed Then
                con4.Open()
            End If
            CMD.ExecuteNonQuery()
            CMD.Dispose()
        End If




        Dim daemp As New SqlDataAdapter
        Dim dsemp As New DataSet

        'Dim dt As DataTable = dsemp.Tables.Item("employeemaster")
        If con4.State = ConnectionState.Closed Then
            con4.Open()
        End If
        Dim qrycustomers As String

        ' If msel = 1 Then
        qrycustomers = "SELECT * FROM oscan"
        ''ElseIf msel = 2 Then
        ' qrycustomers = "SELECT * FROM employeemaster where empcode=" & Val(Txtempcode.Text)
        ' End If
        'Dim qryCustomers As String = "SELECT * FROM employeemaster"


        daemp.SelectCommand = New SqlCommand(qrycustomers, con4)

        Dim cb As SqlCommandBuilder = New SqlCommandBuilder(daemp)

        daemp.Fill(dsemp, "oscan")

        Dim dt As DataTable = dsemp.Tables("oscan")

        If Val(txtno.Text) = 0 Or Len(Trim(cmbyr.Text)) = 0 Then
            MsgBox("Please fill up docnum or Period information.", MsgBoxStyle.Critical)

            Exit Sub
        End If

        Try
            If msel = 1 Or msel = 2 Then
                ' add a row
                Dim newRow As DataRow

                newRow = dt.NewRow()
                newRow("docnum") = Val(txtno.Text)
                newRow("docdate") = Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd")
                newRow("period") = cmbyr.Text & vbNullString
                newRow("processfrom") = cmbprocessfr.Text & vbNullString
                newRow("linefrom") = cmblinefr.Text & vbNullString
                newRow("processto") = cmbprocessto.Text & vbNullString
                newRow("lineto") = cmblineto.Text & vbNullString
                newRow("totqty") = Val(lbltotqty.Text)
                newRow("Ttype") = cmbtype.Text & vbNullString


                dt.Rows.Add(newRow)
                'ElseIf msel = 2 Then
                '    With dt
                '        .Rows(0)("empcode") = Val(Txtempcode.Text)
                '        .Rows(0)("empname") = Txtempname.Text & vbNullString
                '        .Rows(0)("dob") = txtDob.Value & vbNullString
                '        .Rows(0)("Gender") = Cmbgender.Text & vbNullString
                '        .Rows(0)("Mstatus") = Cmbmstatus.Text & vbNullString
                '        .Rows(0)("Pbuilding") = Txtpbuilding.Text & vbNullString
                '        .Rows(0)("pblock") = Txtpblock.Text & vbNullString
                '        .Rows(0)("pstreet") = Txtpstreet.Text & vbNullString
                '        .Rows(0)("Pcity") = Txtpcity.Text & vbNullString
                '        .Rows(0)("ppincode") = Txtppin.Text & vbNullString
                '        .Rows(0)("pstate") = Cmbpstate.Text & vbNullString
                '        .Rows(0)("pcountry") = Cmbpcountry.Text & vbNullString
                '        .Rows(0)("cbuilding") = Txtcbuilding.Text & vbNullString
                '        .Rows(0)("cblock") = Txtcblock.Text & vbNullString
                '        .Rows(0)("cstreet") = Txtcstreet.Text & vbNullString
                '        .Rows(0)("ccity") = Txtccity.Text & vbNullString
                '        .Rows(0)("cpincode") = Txtcpin.Text & vbNullString
                '        .Rows(0)("cstate") = Cmbcstate.Text & vbNullString
                '        .Rows(0)("ccountry") = Cmbccountry.Text & vbNullString
                '        .Rows(0)("Phoneno") = Txtphone.Text & vbNullString
                '        .Rows(0)("mobileno") = Txtmobile.Text & vbNullString
                '        .Rows(0)("salstructname") = Cmbsalstructure.Text & vbNullString
                '        '.Rows(0)("salstructcode") = getid("salarystrucmaster", "salstructcode", "salstructname", Cmbsalstructure.Text) & vbNullString


                '    End With
            End If
            daemp.Update(dsemp, "oscan")
            cb.Dispose()
            daemp.Dispose()
            dsemp.Dispose()
            dt.Dispose()

            Call savescan1()
            MsgBox("Saved Bill No." & txtno.Text)
            'cmdprint.PerformClick()
            cmdclear.PerformClick()


            ' MsgBox("Record successfully saved.", MsgBoxStyle.Information)

        Catch ex As SqlException

            'MsgBox(ex.ToString)
            ' DS.Dispose()
            ' DA.Dispose()
            'DS1.Dispose()
            'DA1.Dispose()
            cb.Dispose()
            daemp.Dispose()
            dsemp.Dispose()
            dt.Dispose()


            merr = Trim(ex.Message)
            'MsgBox(merr)
            If InStr(merr, "PRIMARY KEY") > 0 Then
                If msel = 1 Then

                    Dim CMD3 As New SqlCommand("SELECT MAX(docnum) AS TNO FROM oscan", con4)


                    If con4.State = ConnectionState.Closed Then
                        con4.Open()
                    End If

                    Dim CBNO As Int32 = IIf(IsDBNull(CMD3.ExecuteScalar) = False, CMD3.ExecuteScalar, 0)

                    txtno.Text = CBNO + 1
                    CMD3.Dispose()
                    'con.Close()
                    Call saverec()

                End If
            End If




        End Try
        'daemp.Dispose()
        'dsemp.Dispose()


    End Sub

    Private Sub savescan1()

        If msel = 2 Then
            Dim CMD As New SqlCommand("delete from scan1 where docnum=" & Val(txtno.Text) & " and period='" & cmbyr.Text & "'", con4)
            '                                  & Microsoft.VisualBasic.Format(CDate(mskddate.Text), "yyyy-MM-dd") & "' where docnum=" & Val(flxv.get_TextMatrix(k, 0)), con)
            If con4.State = ConnectionState.Closed Then
                con4.Open()
            End If
            CMD.ExecuteNonQuery()
            CMD.Dispose()
        End If

        'If msel = 1 Then
        'Dim dtt As DataTable = dsemp.Tables("relationmaster")
        sqlqry = "select * from scan1 "
        'ElseIf msel = 2 Then
        'Call delrecord2("select * from relationmaster where empcode=" & Val(Txtempcode.Text))
        'sqlqry = "select * from relationmaster where empcode=" & Val(Txtempcode.Text)
        'sqlqry = "select * from relationmaster"
        'End If

        Dim darel As New SqlDataAdapter
        Dim dsrel As New DataSet

        If con4.State = ConnectionState.Closed Then
            con4.Open()
        End If




        darel.SelectCommand = New SqlCommand(sqlqry, con4)

        Dim cb1 As SqlCommandBuilder = New SqlCommandBuilder(darel)

        darel.Fill(dsrel, "scan1")




        Dim dt1 As DataTable = dsrel.Tables("scan1")

        If Val(txtno.Text) = 0 Then
            MsgBox("Please fill up docnum or period information.", MsgBoxStyle.Critical)

            Exit Sub
        End If

        Try
            If msel = 1 Or msel = 2 Then
                ' add a row
                Dim newRow As DataRow

                For j = 0 To (dv1.RowCount - 1)
                    If Len(Trim(dv1.Rows.Item(j).Cells(0).Value)) > 0 Then
                        newRow = dt1.NewRow()
                        newRow("docnum") = Val(txtno.Text)
                        newRow("docdate") = Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd")
                        newRow("period") = cmbyr.Text & vbNullString
                        newRow("itemcode") = dv1.Rows.Item(j).Cells(0).Value & vbNullString
                        newRow("itemname") = dv1.Rows.Item(j).Cells(1).Value & vbNullString
                        newRow("style") = dv1.Rows.Item(j).Cells(2).Value & vbNullString
                        newRow("size") = dv1.Rows.Item(j).Cells(3).Value & vbNullString
                        newRow("wono") = Val(dv1.Rows.Item(j).Cells(5).Value)
                        newRow("qty") = Val(dv1.Rows.Item(j).Cells(6).Value)
                        newRow("cutno2") = dv1.Rows.Item(j).Cells(7).Value & vbNullString
                        newRow("delno") = Microsoft.VisualBasic.Val(dv1.Rows.Item(j).Cells(8).Value)

                        dt1.Rows.Add(newRow)
                    End If

                Next
                'ElseIf msel = 2 Then
                '    With dt1
                '        If .Rows.Count > 0 Then
                '            For j = 0 To (DV.RowCount - 1)

                '                .Rows(0)("empcode") = Val(Txtempcode.Text)
                '                .Rows(0)("relname") = DV.Rows.Item(j).Cells(0).Value & vbNullString
                '                .Rows(0)("reltype") = DV.Rows.Item(j).Cells(1).Value & vbNullString
                '                .Rows(0)("reldob") = DV.Rows.Item(j).Cells(2).Value & vbNullString
                '                .Rows(0)("relage") = Val(DV.Rows.Item(j).Cells(3).Value)
                '                .Rows(0)("reloccupy") = DV.Rows.Item(j).Cells(4).Value & vbNullString

                '            Next j
                '        End If

                '    End With
            End If
            darel.Update(dsrel, "scan1")

            'MsgBox("Record successfully saved.", MsgBoxStyle.Information)
            dt1.Dispose()
            darel.Dispose()
            dsrel.Dispose()
            cb1.Dispose()
            Call updatescan()
        Catch ex As sqlException
            MsgBox(ex.ToString)
        End Try
        darel.Dispose()
        dsrel.Dispose()

    End Sub
    Private Sub updatescan()
        If msel = 1 Then

            For n = 0 To (dv1.RowCount - 1)
                If Len(Trim(dv1.Rows.Item(n).Cells(0).Value)) > 0 Then

                    msql2 = "update [@inm_pde1] set u_scanupdt='Y' where docentry=" & Val(dv1.Rows.Item(n).Cells(8).Value) & " and U_ItemCode='" & Trim(dv1.Rows.Item(n).Cells(0).Value) & "' and U_WONo=" & Val(dv1.Rows.Item(n).Cells(5).Value) & " and U_CutNo='" & Trim(dv1.Rows.Item(n).Cells(7).Value) & "' and U_ScanQty>0"
                    'Dim CMD As New OleDb.OleDbCommand("update [@inm_pde1] set u_scanqty=isnull(u_scanqty,0)+1 where docentry=" & Val(txtdelno.Text) & " and lineid=" & Val(txtlineid.Text), con)
                    Dim CMD As New SqlCommand(msql2, con)
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If
                    Try
                        CMD.ExecuteNonQuery()
                        CMD.Dispose()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                        CMD.Dispose()
                    End Try
                    CMD.Dispose()
                End If
            Next

        End If






    End Sub
    Private Sub displydata()
        Dim daemp As New SqlDataAdapter
        Dim dsemp As New DataSet
        Dim qryCustomers As String = "SELECT * FROM oscan WHERE docnum = " & Val(txtno.Text) & " and period='" & cmbyr.Text & "'"

        If con4.State = ConnectionState.Closed Then
            con4.Open()
        End If
        daemp.SelectCommand = New SqlCommand(qryCustomers, con4)

        Dim cb As SqlCommandBuilder = New SqlCommandBuilder(daemp)

        daemp.Fill(dsemp, "oscan")

        Dim dt As DataTable = dsemp.Tables("oscan")

        Try
            With dt
                If .Rows.Count > 0 Then
                    txtno.Text = .Rows(0)("docnum")
                    mskdate.Text = .Rows(0)("docdate")
                    cmbyr.Text = .Rows(0)("period") & vbNullString
                    cmbprocessfr.Text = .Rows(0)("processfrom") & vbNullString
                    cmblinefr.Text = .Rows(0)("linefrom") & vbNullString
                    cmbprocessto.Text = .Rows(0)("processto") & vbNullString
                    cmblineto.Text = .Rows(0)("lineto") & vbNullString
                    lbltotqty.Text = Val(.Rows(0)("totqty"))


                    '.Rows(0)("salstructcode") = getid("salarystrucmaster", "salstructcode", "salstructname", Cmbsalstructure.Text) & vbNullString

                End If

            End With
            daemp.Dispose()
            dsemp.Dispose()
            dt.Dispose()
        Catch ex As OleDbException
            MsgBox(ex.ToString)
        Finally

            'cnCustomers.Close()
        End Try
        Call diplyscan1()

    End Sub


    Private Sub diplyscan1()
        'DV.DataSource = Nothing

        'dv1.AutoGenerateColumns = False
        'DV.Refresh()
        Dim j As Integer


        'Call dvhead()
        Dim darl As New SqlDataAdapter
        Dim dsrl As New DataSet



        Dim qryCustomers As String = "SELECT * FROM scan1 WHERE docnum = " & Val(txtno.Text) & " and period='" & cmbyr.Text & "'"

        If con4.State = ConnectionState.Closed Then
            con4.Open()
        End If
        darl.SelectCommand = New SqlCommand(qryCustomers, con4)

        Dim cb1 As SqlCommandBuilder = New SqlCommandBuilder(darl)

        darl.Fill(dsrl, "scan1")

        Dim dtd As DataTable = dsrl.Tables("scan1")

        Try

            'DV.DataSource = dsrel

            With dtd
                If .Rows.Count > 0 Then
                    For k = 0 To .Rows.Count - 1
                        'For j = 0 To (DV.RowCount - 1)
                        j = dv1.Rows.Add()
                        '.Rows(0)("empcode") = Val(Txtempcode.Text)
                        dv1.Rows.Item(j).Cells(0).Value = .Rows(k)("itemcode") & vbNullString
                        dv1.Rows.Item(j).Cells(1).Value = .Rows(k)("itemname") & vbNullString
                        dv1.Rows.Item(j).Cells(2).Value = .Rows(k)("style") & vbNullString
                        dv1.Rows.Item(j).Cells(3).Value = .Rows(k)("size") & vbNullString
                        dv1.Rows.Item(j).Cells(5).Value = Val(.Rows(k)("wono"))
                        dv1.Rows.Item(j).Cells(6).Value = Val(.Rows(k)("qty"))
                        dv1.Rows.Item(j).Cells(7).Value = .Rows(k)("cutno2") & vbNullString
                        dv1.Rows.Item(j).Cells(8).Value = Val(.Rows(k)("delno"))

                        ' Next j
                    Next k
                End If


                'DV.CurrentRow.Cells(4).Value = ""


                ''Txtempname.Text = IIf(IsDBNull(.Rows(0)("empname")), "", .Rows(0)("empname"))

            End With
            'Call deletecol()
            'DV.CurrentRow.Cells(4).Value = " "
            darl.Dispose()
            dsrl.Dispose()
            dtd.Dispose()
        Catch ex As OleDbException
            MsgBox(ex.ToString)
        Finally

            'cnCustomers.Close()
        End Try
    End Sub

    Private Sub txtcutno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcutno.KeyPress

        If e.KeyChar = "." Then
            txtcutno.Text = ""
            txtdelno.Text = ""
            txtlineid.Text = ""
            txtcutno.Focus()
        End If

        If Asc(e.KeyChar) = 13 Then
            'Call loadgrid2()
            If msel = 1 Or msel = 2 Then

                'Call chkcutno2new()
                Call loadbal()
                If Val(txtdelno.Text) > 0 Then
                    Call chkcutno()
                    'Call chkcutno2()
                    Call chkcutno2new()
                    Call totcalc()

                    txtcutno.Text = ""
                    txtdelno.Text = ""
                    txtlineid.Text = ""
                Else
                    MsgBox("Delivery no is null not allowed")
                    txtcutno.Text = ""
                    txtdelno.Text = ""
                    txtlineid.Text = ""
                    txtcutno.Focus()
                End If
                txtcutno.Focus()
            End If
        End If

    End Sub

   
    Private Sub loadgrid2()
        msql = "select b.u_itemcode,c.itemname,c.U_Style,c.U_Size, b.U_CutNo,b.DocEntry woentry,1 as qty from [@INM_WOR8] b" & vbCrLf _
        & "left join OITM c on c.ItemCode=b.U_ItemCode" & vbCrLf _
        & "where b.U_ItemCode is not null and b.u_cutno='" & Mid(Trim(txtcutno.Text), 1, InStr(Trim(txtcutno.Text), "-") - 1) & "'" & vbCrLf _
        & "group by b.u_itemcode,c.itemname,c.U_Style,c.U_Size, b.U_CutNo,b.DocEntry"


        Dim CMD As New SqlCommand(msql, con)

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim DR As SqlClient.SqlDataReader
        'Dim DR1 As SqlClient.SqlDataReader

        Dim DR As SqlDataReader
        DR = CMD.ExecuteReader
        'dv.Rows.Clear()
        If DR.HasRows = True Then
            While DR.Read
                n = dv.Rows.Add()
                dv.Rows.Item(n).Cells(0).Value = DR.Item("u_itemcode")
                dv.Rows.Item(n).Cells(1).Value = DR.Item("itemname")
                dv.Rows.Item(n).Cells(2).Value = DR.Item("u_Style")
                dv.Rows.Item(n).Cells(3).Value = DR.Item("u_size")
                dv.Rows.Item(n).Cells(4).Value = txtcutno.Text
                dv.Rows.Item(n).Cells(5).Value = DR.Item("woentry")
                dv.Rows.Item(n).Cells(6).Value = DR.Item("qty")
                dv.Rows.Item(n).Cells(7).Value = DR.Item("u_cutno")



            End While
        End If
        DR.Close()
        CMD.Dispose()
        Me.dv.FirstDisplayedScrollingRowIndex = Me.dv.RowCount - 1


    End Sub
    Private Sub loadgrid3()
        msql = "select b.u_itemcode,c.itemname,c.U_Style,c.U_Size, b.U_CutNo,b.DocEntry woentry,1 as qty from [@INM_WOR8] b" & vbCrLf _
        & "left join OITM c on c.ItemCode=b.U_ItemCode" & vbCrLf _
        & "where b.U_ItemCode is not null and b.u_cutno='" & Mid(Trim(txtcutno.Text), 1, InStr(Trim(txtcutno.Text), "-") - 1) & "'" & vbCrLf _
        & "group by b.u_itemcode,c.itemname,c.U_Style,c.U_Size, b.U_CutNo,b.DocEntry"


        Dim CMD1 As New SqlCommand(msql, con)

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim DR As SqlClient.SqlDataReader
        'Dim DR1 As SqlClient.SqlDataReader

        Dim DR1 As SqlDataReader
        DR1 = CMD1.ExecuteReader
        'dv.Rows.Clear()
        If DR1.HasRows = True Then
            While DR1.Read
                n = dv1.Rows.Add()
                dv1.Rows.Item(n).Cells(0).Value = DR1.Item("u_itemcode")
                dv1.Rows.Item(n).Cells(1).Value = DR1.Item("itemname")
                dv1.Rows.Item(n).Cells(2).Value = DR1.Item("u_Style")
                dv1.Rows.Item(n).Cells(3).Value = DR1.Item("u_size")
                'dv1.Rows.Item(n).Cells(4).Value = txtcutno.Text
                dv1.Rows.Item(n).Cells(5).Value = DR1.Item("woentry")
                dv1.Rows.Item(n).Cells(6).Value = DR1.Item("qty")
                dv1.Rows.Item(n).Cells(7).Value = DR1.Item("u_cutno")
                dv1.Rows.Item(n).Cells(8).Value = Val(txtdelno.Text)


                'lbltotqty.Text = Val(lbltotqty.Text) + Val(DR1.Item("qty"))
                'mtotqty = mtotqty + Val(DR1.Item("qty"))

            End While
        End If
        'lbltotqty.Text = Val(lbltotqty.Text) + mtotqty
        Call delnoupdt()
        DR1.Close()
        CMD1.Dispose()


        Me.dv1.FirstDisplayedScrollingRowIndex = Me.dv1.RowCount - 1


    End Sub


    Private Sub chkcutno()
        Dim Found As Boolean = False
        If (dv.Rows.Count > 0) Then
            'Check if the product Id exists with the same Price
            For Each row As DataGridViewRow In dv.Rows
                If ((Convert.ToString(row.Cells(4).Value) = txtcutno.Text)) Then
                    'Update the Quantity of the found row
                    'row.Cells(2).Value = Convert.ToString((1 + Convert.ToInt16(row.Cells(2).Value)))
                    Foundt = True
                    Found = True
                End If

            Next
            If Not Found Then
                'Add the row to grid view
                'dv.Rows.Add(txtcutno.Text, textBox_Price.Text, 1)
                'dv.Rows.Add()
                Foundt = False
                Call loadgrid2()
            End If

        Else
            'Add the row to grid view for the first time
            'dv.Rows.Add()
            Foundt = False
            Call loadgrid2()

        End If
    End Sub
    Private Sub totcalc()
        lbltotqty.Text = 0
        If (dv1.Rows.Count > 0) Then
            'Check if the product Id exists with the same Price
            For Each row As DataGridViewRow In dv1.Rows
                If Convert.ToInt16(row.Cells(6).Value) > 0 Then
                    lbltotqty.Text = Val(lbltotqty.Text) + Convert.ToInt16(row.Cells(6).Value)
                End If
            Next
        End If

    End Sub
    Private Sub chkcutno2()
        Dim Found2 As Boolean = False
        If (dv1.Rows.Count > 0) Then
            'Check if the product Id exists with the same Price
            For Each row As DataGridViewRow In dv1.Rows
                If ((Convert.ToString(row.Cells(7).Value) = Mid(Trim(txtcutno.Text), 1, InStr(Trim(txtcutno.Text), "-") - 1))) Then
                    'Update the Quantity of the found row
                    If Foundt = False Then
                        row.Cells(6).Value = Convert.ToString((1 + Convert.ToInt16(row.Cells(6).Value)))
                    End If
                    Found2 = True
                End If

            Next
            If Not Found2 Then
                'Add the row to grid view
                'dv.Rows.Add(txtcutno.Text, textBox_Price.Text, 1)
                'dv.Rows.Add()
                Call loadgrid3()
            End If

        Else
            'Add the row to grid view for the first time
            'dv.Rows.Add()
            Call loadgrid3()

        End If
    End Sub
    Private Sub chkcutno2new()
        Dim Found2 As Boolean = False
        If (dv1.Rows.Count > 0) Then
            'Check if the product Id exists with the same Price
            For Each row As DataGridViewRow In dv1.Rows
                If ((Convert.ToString(row.Cells(7).Value) = Mid(Trim(txtcutno.Text), 1, InStr(Trim(txtcutno.Text), "-") - 1))) And ((Convert.ToInt32(row.Cells(8).Value) = Val(txtdelno.Text))) Then
                    'Update the Quantity of the found row
                    If Foundt = False Then
                        row.Cells(6).Value = Convert.ToString((1 + Convert.ToInt16(row.Cells(6).Value)))
                    End If
                    Found2 = True
                    Call delnoupdt()
                End If

            Next
            If Not Found2 Then
                'Add the row to grid view
                'dv.Rows.Add(txtcutno.Text, textBox_Price.Text, 1)
                'dv.Rows.Add()
                Call loadgrid3()
            End If

        Else
            'Add the row to grid view for the first time
            'dv.Rows.Add()
            Call loadgrid3()

        End If
    End Sub

    Private Sub txtcutno_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcutno.SizeChanged

    End Sub



    Private Sub txtcutno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcutno.TextChanged

    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        msel = 1
        'enable(Me)

        CLEAR(Me)
        enable(Me)
        Call AUTONO()
        Call gridhead()
        Call gridhead2()
        lbltotqty.Text = 0
        mtotqty = 0

        txtno.Enabled = False
        If cmdsave.Enabled = False Then
            cmdsave.Enabled = True
        End If
        cmbyr.Text = mpostperiod
        mskdate.Focus()
    End Sub

    Private Sub cmdedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdedit.Click
        msel = 2
        'enable(Me)
        CLEAR(Me)
        enable(Me)
        Call gridhead()
        Call gridhead2()
        lbltotqty.Text = 0
        mtotqty = 0
        If cmdsave.Enabled = False Then
            cmdsave.Enabled = True
        End If
        cmbyr.Text = mpostperiod
        txtno.Focus()
    End Sub

    Private Sub cmddel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddel.Click
        msel = 3
        enable(Me)
        cmbyr.Text = mpostperiod
        txtno.Focus()
    End Sub

    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.Click
        'msel = 4
        'enable(Me)
        'CLEAR(Me)
        enable(Me)
        msel = 4
        lbltotqty.Text = 0
        Call gridhead()
        Call gridhead2()
        cmbyr.Text = mpostperiod
        txtno.Focus()
    End Sub

    

    Private Sub txtno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtno.KeyPress
        If Asc(e.KeyChar) = 13 Then
            mskdate.Focus()
        End If
    End Sub

    Private Sub txtno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtno.LostFocus
        If msel > 1 Then
            Call displydata()
            If msel = 3 Then
                Call Deldata()
            End If
        End If
    End Sub

    Private Sub txtno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtno.TextChanged

    End Sub

    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click

        Call saverec()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR(Me)
        'Call gridhead()
        'Call gridhead2()
        msel = 0
        dv1.Rows.Clear()
        dv.Rows.Clear()
        dv.RowCount = 1
        dv.ColumnCount = 0
        dv1.RowCount = 1
        dv1.ColumnCount = 0
        Call gridhead()
        Call gridhead2()
        'lblqty.Text = 0
        'lblamt.Text = 0
        cmbyr.Text = mpostperiod
        disable(Me)
        cmdsave.Enabled = False

    End Sub

    Private Sub cmblineto_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmblineto.GotFocus
        Call loadlinenoto()
    End Sub
    Private Sub loadlinenofr()
        If Len(Trim(cmbprocessfr.Text)) > 0 Then
            msql = "select u_lineno from [@inm_oolm] where u_opercode='" & cmbprocessfr.Text & "' group by u_lineno"
            loadcomboqry(msql, "u_lineno", cmblinefr)
        End If


    End Sub
    Private Sub loadlinenoto()
        If Len(Trim(cmbprocessto.Text)) > 0 Then
            msql = "select u_lineno from [@inm_oolm] where u_opercode='" & cmbprocessto.Text & "' group by u_lineno"
            loadcomboqry(msql, "u_lineno", cmblineto)
        End If
    End Sub

    Private Sub cmblineto_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmblineto.SelectedIndexChanged

    End Sub

    Private Sub cmblinefr_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmblinefr.GotFocus
        Call loadlinenofr()
    End Sub

    Private Sub cmblinefr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmblinefr.SelectedIndexChanged

    End Sub

    Private Sub cmbyr_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbyr.LostFocus
        'If msel > 1 Then
        '    Call displydata()
        '    If msel = 3 Then
        '        Call Deldata()
        '    End If
        'End If
    End Sub

    Private Sub cmbyr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbyr.SelectedIndexChanged

    End Sub

    Private Sub cmdprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdprint.Click
        ''If MsgBox("Print Itemwise Packing", MsgBoxStyle.Critical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        ''    mtik = 1
        ''ElseIf MsgBox("Print Boxwise Packing", MsgBoxStyle.Critical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        ''    mtik = 2
        ''Else
        ''    mtik = 0
        ''End If
        ''Dim f2 As New Frmscanprint

        'Frmscanprint.txtno.Text = txtno.Text
        'Frmscanprint.cmbyr.Text = cmbyr.Text
        'Frmscanprint.cmdok.PerformClick()
        'Frmscanprint.Visible = True

        ''Call Frmscanprint.crystalrr()
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub mskdate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles mskdate.GotFocus
        If msel = 1 Then
            'Dim currentdate As System.DateTime
            mskdate.Text = System.DateTime.Today
        End If
    End Sub

    Private Sub mskdate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskdate.MaskInputRejected

    End Sub

    Private Sub dv1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dv1.CellContentClick

    End Sub
    Private Sub loadbal()


        'Dim CMD As New OleDb.OleDbCommand("delete from [@inc_otar] where docentry=" & Val(txtno.Text), con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If
        'Try
        '    CMD.ExecuteNonQuery()
        '    CMD.Dispose()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        '    CMD.Dispose()
        'End Try

        msql = " select top 1 b.DocEntry, b.U_DTOperCode,a.lineid, a.U_ItemCode,c.ItemName,c.U_Style, c.U_Size, b.u_tlno, a.U_WONo,a.U_WOEntry,a.U_CutNo,sum(a.U_RecdQty) rcdqty,sum(a.u_cmplqty) relqty,sum(isnull(a.u_wipqty,0)) as openqty,  sum(a.u_cmplqty+ISNULL(a.u_wipqty,0)) cmplqty,sum(a.U_RecdQty-(a.U_CmplQty+ISNULL(a.U_WIPQty,0)+isnull(a.u_scanqty,0))) as balqty from [@inm_pde1] a,[@INM_OPDE] b,OITM c " & vbCrLf _
            & " where b.DocEntry=a.DocEntry and a.U_ItemCode=c.itemcode and b.u_docstatus='R' and   (b.u_dtopercode='" & Trim(cmbprocessfr.Text) & "') and (b.u_tlno='" & Trim(cmblinefr.Text) & "') and a.u_cutno='" & Trim(Mid(Trim(txtcutno.Text), 1, InStr(Trim(txtcutno.Text), "-") - 1)) & "' " & vbCrLf _
            & " group by a.lineid, b.DocEntry, b.U_dtOperCode, a.U_ItemCode,c.ItemName,c.U_Style, c.U_Size, a.U_WONo,a.U_WOEntry,a.U_CutNo,b.u_tlno " & vbCrLf _
            & " having sum(a.U_RecdQty-(a.U_CmplQty+ISNULL(U_WIPQty,0)+ISNULL(u_scanqty,0)))>0"


        Dim CMD1 As New SqlCommand(msql, con)

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim DR As SqlClient.SqlDataReader
        'Dim DR1 As SqlClient.SqlDataReader

        Dim DR1 As SqlDataReader
        DR1 = CMD1.ExecuteReader
        'dv.Rows.Clear()
        If DR1.HasRows = True Then
            While DR1.Read
                txtdelno.Text = DR1.Item("docentry")
                txtlineid.Text = DR1.Item("lineid")
                'lbltotqty.Text = Val(lbltotqty.Text) + Val(DR1.Item("qty"))
                'mtotqty = mtotqty + Val(DR1.Item("qty"))

            End While
        End If
        'lbltotqty.Text = Val(lbltotqty.Text) + mtotqty
        DR1.Close()
        CMD1.Dispose()

    End Sub
    Private Sub delnoupdt()
        If msel = 1 Then
            Dim CMD As New SqlCommand("update [@inm_pde1] set u_scanqty=isnull(u_scanqty,0)+1 where docentry=" & Val(txtdelno.Text) & " and lineid=" & Val(txtlineid.Text), con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Try
                CMD.ExecuteNonQuery()
                CMD.Dispose()
            Catch ex As Exception
                MsgBox(ex.Message)
                CMD.Dispose()
            End Try
            CMD.Dispose()
        End If
    End Sub

    '    declare @opername as nvarchar(30)
    'declare @lineno as nvarchar(10)
    'set @opername='KAJAGD'
    'set @lineno='16'
    'select top 1 b.DocEntry, b.U_DTOperCode, a.U_ItemCode,c.ItemName,c.U_Style, c.U_Size, b.u_tlno, a.U_WONo,a.U_WOEntry,a.U_CutNo,sum(a.U_RecdQty) rcdqty,sum(a.u_cmplqty) relqty,sum(isnull(a.u_wipqty,0)) as openqty,  sum(a.u_cmplqty+ISNULL(a.u_wipqty,0)) cmplqty,sum(a.U_RecdQty-(a.U_CmplQty+ISNULL(a.U_WIPQty,0)+isnull(a.u_scanqty,0))) as balqty from [@inm_pde1] a,[@INM_OPDE] b,OITM c
    'where b.DocEntry=a.DocEntry and a.U_ItemCode=c.itemcode and b.u_docstatus='R' and   (b.u_dtopercode=@opername ) and (b.u_tlno=@lineno ) and a.u_cutno='13573' 
    'group by b.DocEntry, b.U_dtOperCode, a.U_ItemCode,c.ItemName,c.U_Style, c.U_Size, a.U_WONo,a.U_WOEntry,a.U_CutNo,b.u_tlno
    'having sum(a.U_RecdQty-(a.U_CmplQty+ISNULL(U_WIPQty,0)+ISNULL(u_scanqty,0)))<>0
End Class
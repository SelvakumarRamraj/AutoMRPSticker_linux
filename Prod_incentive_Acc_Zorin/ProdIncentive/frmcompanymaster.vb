Imports System.Data.Sql
Public Class frmcompanymaster
    Dim msel As Integer
    Dim msql As String
    Dim mkcode As Int32
    Private Sub frmcompanymaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.Height = (MDIForm1.Height - MDIForm1.sstp.Height)
        ' Me.Height = (MDIForm1.Height - (MDIForm1.sstp.Height + MDIForm1.sstpb.Height))
        ' Me.Width = My.Computer.Screen.Bounds.Width + 10
    End Sub
    Private Sub saverec()
        If msel = 1 Or msel = 2 Then
            If msel = 1 Then
                msql = "select * from cmpmaster where cmp_id='" & mcmpid & "'"
                'msql = "SELECT sectionname FROM section GROUP BY sectionname ORDER BY sectionname"
            Else
                msql = "select * from cmpmaster where cmpname='" & cmpcmbname.Text & "' and cmp_id='" & mcmpid & "'"
            End If
            Dim da As New sqlDataAdapter(msql, con)
            Dim cb As New sqlcommand(da)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            'Dim trans As OleDb.OleDbTransaction = Nothing
            Dim ds As New DataSet
            da.Fill(ds, "cmpmaster")
            Try
                If msel = 1 Then
                    ' trans = con.BeginTransaction()
                    'Dim da As New OleDb.OleDbDataAdapter("select * from section where cmp_id='" & mcmpid & "'", con)
                    'Dim cb As New OleDb.OleDbCommandBuilder(da)
                    Dim dsrow As DataRow
                    dsrow = ds.Tables("cmpmaster").NewRow
                    dsrow.Item("cmp_id") = findid()
                    dsrow.Item("cmpname") = Trim(cmptxtname.Text)
                    dsrow.Item("add1") = Trim(cmptxtadd1.Text)
                    dsrow.Item("add2") = Trim(cmptxtadd2.Text)
                    dsrow.Item("add3") = Trim(cmptxtadd3.Text)
                    dsrow.Item("add4") = Trim(cmptxtadd4.Text)
                    dsrow.Item("add5") = Trim(cmptxtadd5.Text)
                    dsrow.Item("city") = Trim(cmptxtcity.Text)
                    dsrow.Item("pincode") = Trim(cmptxtpincode.Text)
                    dsrow.Item("state") = Trim(cmptxtstate.Text)
                    dsrow.Item("country") = Trim(cmptxtcountry.Text)
                    dsrow.Item("stdcode") = Trim(cmptxtstd.Text)
                    dsrow.Item("phoneno") = Trim(cmptxtphone.Text)
                    dsrow.Item("faxno") = Trim(cmptxtfax.Text)
                    dsrow.Item("mobile") = Trim(cmptxtmobile.Text)
                    dsrow.Item("email") = Trim(cmptxtemail.Text)
                    dsrow.Item("web") = Trim(cmptxtweb.Text)
                    dsrow.Item("tin") = Trim(cmptxttin.Text)
                    dsrow.Item("tngst") = Trim(cmptxttngst.Text)
                    dsrow.Item("cst") = Trim(cmptxtcstno.Text)
                    dsrow.Item("cenvatregno") = Trim(cmptxtcenvatno.Text)
                    dsrow.Item("division") = Trim(cmptxtdivision.Text)
                    dsrow.Item("range") = Trim(cmptxtrange.Text)
                    dsrow.Item("commissionerate") = Trim(cmptxtcommissionerate.Text)
                    dsrow.Item("cmpcode") = loadcode()
                    dsrow.Item("cmp_id") = mcmpid
                    ds.Tables("cmpmaster").Rows.Add(dsrow)
                    da.Update(ds, "cmpmaster")
                    'trans.Commit()
                Else
                    'Dim da As New OleDb.OleDbDataAdapter("select * from section where sectionname='" & seccmbsecname.Text & "' and cmp_id='" & mcmpid & "'", con)
                    'Dim cb As New OleDb.OleDbCommandBuilder(da)
                    'trans = con.BeginTransaction()
                    Dim dt As DataTable = ds.Tables("cmpmaster")
                    With dt
                        '.Rows(0)("section_id") = msecid
                        .Rows(0).Item("cmpname") = Trim(cmptxtname.Text)
                        .Rows(0).Item("add1") = Trim(cmptxtadd1.Text)
                        .Rows(0).Item("add2") = Trim(cmptxtadd2.Text)
                        .Rows(0).Item("add3") = Trim(cmptxtadd3.Text)
                        .Rows(0).Item("add4") = Trim(cmptxtadd4.Text)
                        .Rows(0).Item("add5") = Trim(cmptxtadd5.Text)
                        .Rows(0).Item("city") = Trim(cmptxtcity.Text)
                        .Rows(0).Item("pincode") = Trim(cmptxtpincode.Text)
                        .Rows(0).Item("state") = Trim(cmptxtstate.Text)
                        .Rows(0).Item("country") = Trim(cmptxtcountry.Text)
                        .Rows(0).Item("stdcode") = Trim(cmptxtstd.Text)
                        .Rows(0).Item("phoneno") = Trim(cmptxtphone.Text)
                        .Rows(0).Item("faxno") = Trim(cmptxtfax.Text)
                        .Rows(0).Item("mobile") = Trim(cmptxtmobile.Text)
                        .Rows(0).Item("email") = Trim(cmptxtemail.Text)
                        .Rows(0).Item("web") = Trim(cmptxtweb.Text)
                        .Rows(0).Item("tin") = Trim(cmptxttin.Text)
                        .Rows(0).Item("tngst") = Trim(cmptxttngst.Text)
                        .Rows(0).Item("cst") = Trim(cmptxtcstno.Text)
                        .Rows(0).Item("cenvatregno") = Trim(cmptxtcenvatno.Text)
                        .Rows(0).Item("division") = Trim(cmptxtdivision.Text)
                        .Rows(0).Item("range") = Trim(cmptxtrange.Text)
                        .Rows(0).Item("commissionerate") = Trim(cmptxtcommissionerate.Text)
                        .Rows(0).Item("cmpcode") = Trim(cmptxtcode.Text)
                    End With
                    da.Update(ds, "cmpmaster")
                    'trans.Commit()
                End If
                MsgBox("Successfully Saved!")
                'trans.Commit()
                ds.Dispose()
                da.Dispose()
                CLEAR(Me)
                loadname()
                disable(Me)
                cmpcmdsave.Enabled = False
            Catch EX As Exception
                'trans.Rollback()
                ds.Dispose()
                da.Dispose()
                MsgBox(EX.Message)
                CLEAR(Me)
                disable(Me)
                cmpcmdsave.Enabled = False
            End Try
        End If
    End Sub
    Private Sub loadname()
        Dim CMD As New OleDb.OleDbCommand("SELECT cmpname FROM cmpmaster GROUP BY cmpname ORDER BY cmpname", con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        ''Dim DR As SqlDataReader
        Dim DR As OleDb.OleDbDataReader
        DR = CMD.ExecuteReader
        If DR.HasRows = True Then
            cmpcmbname.Items.Clear()
            While DR.Read
                cmpcmbname.Items.Add(DR.Item("cmpname"))
            End While
        End If
        DR.Close()
        CMD.Dispose()
    End Sub
    Private Function loadcode() As Int32
        Dim CMD As New OleDb.OleDbCommand("SELECT MAX(cmpcode) AS code FROM cmpmaster where cmp_id='" & mcmpid & "'", con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim CBNO As Int32 = IIf(IsDBNull(CMD.ExecuteScalar) = False, CMD.ExecuteScalar, 0)
        loadcode = CBNO + 1
        CMD.Dispose()
    End Function

    Private Sub cmpcmdadd_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmpcmdadd.ClickButtonArea
        'txtitemname.Focus()
        msel = cmpcmdadd.Tag
        'enable(Me)
        CLEAR(Me)
        enable(Me)
        cmptxtcode.Text = loadcode()
        cmptxtcode.Enabled = False
        cmpcmbname.Enabled = False
        If cmptxtname.Enabled = False Then cmptxtname.Enabled = True
        cmptxtname.Focus()
    End Sub

    Private Sub cmpcmdedit_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmpcmdedit.ClickButtonArea
        msel = cmpcmdedit.Tag
        enable(Me)
        Call loadname()
        cmptxtcode.Enabled = False
        If cmpcmbname.Visible = False Then cmpcmbname.Visible = True
        If cmptxtname.Visible = True Then cmptxtname.Visible = False
        cmpcmbname.Focus()
    End Sub

    Private Sub cmpcmddel_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmpcmddel.ClickButtonArea
        msel = cmpcmddel.Tag
        enable(Me)
        Call loadname()
        cmptxtcode.Enabled = False
        If cmpcmbname.Visible = False Then cmpcmbname.Visible = True
        If cmptxtname.Visible = True Then cmptxtname.Visible = False
        cmpcmbname.Focus()
    End Sub

    Private Sub cmpcmddisp_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmpcmddisp.ClickButtonArea
        msel = cmpcmddisp.Tag
        enable(Me)
        Call loadname()
        cmptxtcode.Enabled = False
        If cmptxtname.Visible = True Then cmptxtname.Visible = False
        If cmpcmbname.Visible = False Then cmpcmbname.Visible = True
        cmpcmbname.Focus()
    End Sub

    Private Sub cmpcmdclear_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmpcmdclear.ClickButtonArea
        CLEAR(Me)
    End Sub

    Private Sub cmpcmdsave_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmpcmdsave.ClickButtonArea
        Call saverec()
        CLEAR(Me)
    End Sub

    Private Sub cmdcmdexit_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdcmdexit.ClickButtonArea
        con.Close()
        Me.Close()
    End Sub

    Private Sub cmpcmbname_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmpcmbname.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmpcmbname.Visible = False
            cmptxtname.Visible = True
            Call loaddata()
            cmptxtname.Focus()
        End If
    End Sub

    Private Sub cmpcmbname_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmpcmbname.Leave

    End Sub

    Private Sub cmpcmbname_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmpcmbname.LocationChanged

    End Sub

    Private Sub cmpcmbname_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmpcmbname.LostFocus
        Call Deldata()
    End Sub
    Private Sub Deldata()
        Try
            If msel = 3 Then
                If Microsoft.VisualBasic.MsgBox("R U Sure!", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'If MsgBox("R U Sure!", MsgBoxStyle.Critical, MsgBoxResult.Yes) = MsgBoxResult.Yes Then
                    Dim CMD As New OleDb.OleDbCommand("DELETE FROM cmpmaster WHERE cmpname='" & cmpcmbname.Text & "' and cmp_id='" & mcmpid & "'", con)
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If
                    CMD.ExecuteNonQuery()
                    CMD.Dispose()
                    MsgBox("Deleted!")
                End If
            End If
        Catch ex As Exception
            'tranDEL.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmpcmbname_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmpcmbname.SelectedIndexChanged

    End Sub
    Private Sub loaddata()
        Dim CMD As New OleDb.OleDbCommand("select * FROM cmpmaster WHERE cmpname='" & cmpcmbname.Text & "' and cmp_id='" & mcmpid & "'", con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()
        Try
            ''Dim DR As SqlDataReader
            Dim DR As OleDb.OleDbDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                While DR.Read
                    cmptxtcode.Text = IIf(IsDBNull(DR.Item("cmpcode")) = False, DR.Item("cmpcode"), "")
                    cmptxtname.Text = DR.Item("cmpname")
                    cmptxtadd1.Text = DR.Item("add1") & vbNullString
                    cmptxtadd2.Text = DR.Item("add2") & vbNullString
                    cmptxtadd3.Text = DR.Item("add3") & vbNullString
                    cmptxtadd4.Text = DR.Item("add4") & vbNullString
                    cmptxtadd5.Text = DR.Item("add5") & vbNullString
                    cmptxtcity.Text = DR.Item("city") & vbNullString
                    cmptxtpincode.Text = DR.Item("pincode") & vbNullString
                    cmptxtstate.Text = DR.Item("state") & vbNullString
                    cmptxtcountry.Text = DR.Item("country") & vbNullString
                    cmptxtstd.Text = DR.Item("stdcode") & vbNullString
                    cmptxtphone.Text = DR.Item("phoneno") & vbNullString
                    cmptxtmobile.Text = DR.Item("faxno") & vbNullString
                    cmptxtfax.Text = DR.Item("mobile") & vbNullString
                    cmptxtemail.Text = DR.Item("email") & vbNullString
                    cmptxtweb.Text = DR.Item("web") & vbNullString
                    cmptxttngst.Text = DR.Item("tngst") & vbNullString
                    cmptxtcstno.Text = DR.Item("cst") & vbNullString
                    cmptxttin.Text = DR.Item("tin") & vbNullString
                    cmptxtcenvatno.Text = DR.Item("cenvatregno") & vbNullString
                    cmptxtdivision.Text = DR.Item("division") & vbNullString
                    cmptxtrange.Text = DR.Item("range") & vbNullString
                    cmptxtcommissionerate.Text = DR.Item("commissionerate") & vbNullString

                    'txtsectionname.Text = DR.Item("section_id")
                    'txtsectionname.Text = loadsecname(DR.Item("SECTION_ID"))
                    'seccmbfloor.Text = IIf(IsDBNull(DR.Item("floor")) = False, DR.Item("floor"), "")
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmptxtname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtname.TextChanged
        cmptxtname.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtname, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtname.Select(cmptxtname.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmpcmbname_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmpcmbname.TextChanged
        Call autosearch(sender)
    End Sub

    Private Sub cmptxtadd1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtadd1.TextChanged
        cmptxtadd1.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtadd1, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtadd1.Select(cmptxtadd1.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtadd2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtadd2.TextChanged
        cmptxtadd2.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtadd2, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtadd2.Select(cmptxtadd2.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtadd3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtadd3.TextChanged
        cmptxtadd3.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtadd3, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtadd3.Select(cmptxtadd3.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtadd4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtadd4.TextChanged
        cmptxtadd4.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtadd4, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtadd4.Select(cmptxtadd4.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtadd5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtadd5.TextChanged
        cmptxtadd5.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtadd5, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtadd5.Select(cmptxtadd5.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtpincode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtpincode.TextChanged
        cmptxtpincode.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtpincode, 20) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtpincode.Select(cmptxtpincode.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtphone_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtphone.TextChanged
        cmptxtphone.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtphone, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtphone.Select(cmptxtphone.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtmobile_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtmobile.TextChanged
        cmptxtmobile.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtmobile, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtmobile.Select(cmptxtmobile.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtfax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtfax.TextChanged
        cmptxtfax.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtfax, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtfax.Select(cmptxtfax.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtstd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtstd.TextChanged
        cmptxtstd.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtstd, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtstd.Select(cmptxtstd.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxttngst_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxttngst.TextChanged
        cmptxttngst.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxttngst, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxttngst.Select(cmptxttngst.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxttin_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxttin.TextChanged
        cmptxttin.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxttin, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxttin.Select(cmptxttin.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtcstno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtcstno.TextChanged
        cmptxtcstno.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtcstno, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtcstno.Select(cmptxtcstno.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtcenvatno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtcenvatno.TextChanged
        cmptxtcenvatno.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtcenvatno, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtcenvatno.Select(cmptxtcenvatno.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtdivision_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtdivision.TextChanged
        cmptxtdivision.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtdivision, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtdivision.Select(cmptxtdivision.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtrange_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtrange.TextChanged
        cmptxtrange.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtrange, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtrange.Select(cmptxtrange.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtcommissionerate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtcommissionerate.TextChanged
        cmptxtcommissionerate.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtcommissionerate, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtcommissionerate.Select(cmptxtcommissionerate.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtemail_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtemail.TextChanged
        cmptxtemail.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtemail, 30) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtemail.Select(cmptxtemail.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtweb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtweb.TextChanged
        cmptxtweb.CharacterCasing = CharacterCasing.Upper
        If chkchar(cmptxtweb, 50) = True Then
            'MsgBox("Exceed the limit!")
            cmptxtweb.Select(cmptxtweb.Text.Length, 0)
            'tbPositionCursor.Text.Length()
        End If
    End Sub

    Private Sub cmptxtcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmptxtcode.TextChanged

    End Sub
End Class
Imports System.IO
Imports AxMSFlexGridLib
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class frmcostempmaster
    Dim msql, msql2, msql3, msql4 As String
    Dim mdocno As Long
    Dim j, i, msel As Int32
    Dim trans As SqlTransaction
    Private Sub frmcostempmaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height - 25
        Me.Width = My.Computer.Screen.Bounds.Width
        Call flxhead()
    End Sub
    Private Sub selall()
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
    Private Sub flxhead()
        flx.Rows = 1
        flx.Cols = 13
        flx.set_ColWidth(0, 700)
        flx.set_ColWidth(1, 1400)
        flx.set_ColWidth(2, 1900)
        flx.set_ColWidth(3, 1600)
        flx.set_ColWidth(4, 1100)
        flx.set_ColWidth(5, 1500)
        flx.set_ColWidth(6, 1500)
        flx.set_ColWidth(7, 1200)
        flx.set_ColWidth(8, 1000)
        flx.set_ColWidth(9, 700)

        flx.set_ColWidth(10, 700)
        flx.set_ColWidth(11, 700)
        flx.set_ColWidth(12, 700)


        flx.Row = 0
        flx.Col = 0
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 0, "Sel")

        flx.Col = 1
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 1, "empid")


        flx.Col = 2
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 2, "empname")

        flx.Col = 3
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 3, "Department")

        flx.Col = 4
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 4, "Salary")

        flx.Col = 5
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 5, "Sub_Dept")

        flx.Col = 6
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 6, "Wh_Type")


        flx.Col = 7
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 7, "Target")

        flx.Col = 8
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 8, "Per")

        flx.Col = 9
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 9, "Active")

        flx.Col = 10
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 10, "IH")

        flx.Col = 11
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 11, "SC")

        flx.Col = 12
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 12, "Line No")


        'flx.Col = 3
        'flx.CellAlignment = 3
        'flx.CellFontBold = True
        'flx.set_TextMatrix(0, 3, "BATCH")


        'flx.Col = 4
        'flx.CellAlignment = 3
        'flx.CellFontBold = True
        'flx.set_TextMatrix(0, 4, "Color Name")

        'flx.Col = 5
        'flx.CellAlignment = 3
        'flx.CellFontBold = True
        'flx.set_TextMatrix(0, 5, "StickerDate")

        'flx.Col = 6
        'flx.CellAlignment = 3
        'flx.CellFontBold = True
        'flx.set_TextMatrix(0, 6, "Month")

        'flx.Col = 7
        'flx.CellAlignment = 3
        'flx.CellFontBold = True
        'flx.set_TextMatrix(0, 7, "Year")

        'flx.Col = 8
        'flx.CellAlignment = 3
        'flx.CellFontBold = True
        'flx.set_TextMatrix(0, 8, "Line Id")

        'flx.Col = 9
        'flx.CellAlignment = 3
        'flx.CellFontBold = True
        'flx.set_TextMatrix(0, 9, "Visorder")



        flx.set_ColAlignment(2, 2)
        'flx.Rows = flx.Rows + 1
        'flx.Row = flx.Rows - 1
    End Sub


    Private Sub flx_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flx.Enter

    End Sub

    Private Sub flx_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles flx.GotFocus
        'If msel = 2 Then
        '    Call loaddata()
        'End If
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


        If e.keyAscii = 22 Then

            Call exeltoflx(flx)
            'Call loadlineid()
        End If
        If e.keyAscii <> 27 Then
            searchflx(flx, e.keyAscii, 0)
        Else
            ' flxc.Visible = False
            'flx.Row = flx.Row
            'flx.Col = 0
            'flx.Focus()
            'txtbno.Focus()
        End If

        If flx.Col >= 4 Or flx.Col <= 8 Then
            editflx(flx, e.keyAscii, flx)
        End If
    End Sub

    Private Sub delrec()
        'If MsgBox("Delete ? R U Sure!", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then

        '    If MsgBox("Delete ? R U Sure! No Doubt!", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
        '        If con.State = ConnectionState.Closed Then
        '            con.Open()
        '        End If
        '        trans = con.BeginTransaction
        '        Dim CMD As New OleDb.OleDbCommand("delete from " & Trim(mcostdbnam) & ".dbo.empmaster where empid='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'", con, trans)


        '        Try
        '            CMD.ExecuteNonQuery()
        '            trans.Commit()
        '            CMD.Dispose()
        '        Catch ex As Exception
        '            trans.Rollback()
        '            MsgBox(ex.Message)
        '            CMD.Dispose()
        '        End Try


        '        CMD.Dispose()
        '        CMD = Nothing



        '    End If
        'End If
    End Sub

    Private Sub savrec()
        'Dim CMD2 As New OleDb.OleDbCommand("select max(docentry) as no from [@inc_otar]", con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'Dim CBNO As Int32 = IIf(IsDBNull(CMD2.ExecuteScalar) = False, CMD2.ExecuteScalar, 0)

        'mdocno = CBNO + 1
        'CMD2.Dispose()

        If msel = 1 Or msel = 2 Then



            'If con.State = ConnectionState.Closed Then
            '    con.Open()
            'End If
            'trans = con.BeginTransaction
            'If msel = 2 Then
            '    Dim CMD As New OleDb.OleDbCommand("delete from " & Trim(mcostdbnam) & ".dbo.empmaster where empid='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'", con, trans)

            '    CMD.ExecuteNonQuery()
            'End If

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            trans = con.BeginTransaction
            Try
                For j = 1 To flx.Rows - 1

                    If Len(Trim(flx.get_TextMatrix(j, 0))) > 0 Then
                        If msel = 1 Then
                            'msql2 = "insert into " & Trim(mcostdbnam) & ".dbo.empmaster (empid,empname,department,salary,subdept,costtype,target,per,Active) " & vbCrLf _
                            '& " values( " & Val(flx.get_TextMatrix(j, 1)) & ",'" & Trim(flx.get_TextMatrix(j, 2)) & "','" & Trim(flx.get_TextMatrix(j, 3)) & "'," & Val(flx.get_TextMatrix(j, 4)) & ",'" & Trim(flx.get_TextMatrix(j, 5)) & "','" & Trim(flx.get_TextMatrix(j, 6)) & "'," & Val(flx.get_TextMatrix(j, 7)) & "," & Val(flx.get_TextMatrix(j, 8)) & ",'" & flx.get_TextMatrix(j, 9) & "'" & " )"

                            msql2 = "insert into " & Trim(mcostdbnam) & ".dbo.empmaster (empid,empname,department,salary,subdept,costtype,target,per,Active,ih,sc,[lineno]) " & vbCrLf _
                            & " values( " & Val(flx.get_TextMatrix(j, 1)) & ",'" & Trim(flx.get_TextMatrix(j, 2)) & "','" & Trim(flx.get_TextMatrix(j, 3)) & "'," & Val(flx.get_TextMatrix(j, 4)) & ",'" & Trim(flx.get_TextMatrix(j, 5)) & "','" & Trim(flx.get_TextMatrix(j, 6)) & "'," & Val(flx.get_TextMatrix(j, 7)) & "," & Val(flx.get_TextMatrix(j, 8)) & ",'" & flx.get_TextMatrix(j, 9) & "'," & Val(flx.get_TextMatrix(j, 10)) & "," & Val(flx.get_TextMatrix(j, 11)) & ",'" & flx.get_TextMatrix(j, 12) & "'" & " )"

                        End If

                        If msel = 2 Then
                            'msql2 = "update " & Trim(mcostdbnam) & ".dbo.empmaster set salary=" & Val(flx.get_TextMatrix(j, 4)) & ",subdept='" & Trim(flx.get_TextMatrix(j, 5)) & "',costtype='" & Trim(flx.get_TextMatrix(j, 6)) & "',target=" & Val(flx.get_TextMatrix(j, 7)) & ",per=" & Val(flx.get_TextMatrix(j, 8)) & ",active='" & flx.get_TextMatrix(j, 9) & "' where empid=" & Val(flx.get_TextMatrix(j, 1))

                            msql2 = "update " & Trim(mcostdbnam) & ".dbo.empmaster set salary=" & Val(flx.get_TextMatrix(j, 4)) & ",subdept='" & Trim(flx.get_TextMatrix(j, 5)) & "',costtype='" & Trim(flx.get_TextMatrix(j, 6)) & "',target=" & Val(flx.get_TextMatrix(j, 7)) & ",per=" & Val(flx.get_TextMatrix(j, 8)) & ",active='" & flx.get_TextMatrix(j, 9) & "',ih=" & Val(flx.get_TextMatrix(j, 10)) & ",sc=" & Val(flx.get_TextMatrix(j, 11)) & ",lineno='" & flx.get_TextMatrix(j, 12) & "' where empid=" & Val(flx.get_TextMatrix(j, 1))

                        End If

                        Dim dcmd As New SqlCommand(msql2, con, trans)

                        dcmd.ExecuteNonQuery()
                    End If
                Next j
                'CMD2.Dispose()
                trans.Commit()
                MsgBox("Saved!")


            Catch ex As Exception
                trans.Rollback()
                MsgBox(ex.Message)

                'CMD2.Dispose()
            End Try

            'TRANS.Commit()

            ' MsgBox("Saved!")
        Else

        End If

   


    End Sub

    Private Sub cmdadd_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.ClickButtonArea
        msel = 1
        flx.Rows = flx.Rows + 1
        flx.Row = flx.Rows - 1
        flx.Focus()
        flx.Row = 1
        flx.Col = 0
    End Sub

   

    Private Sub cmdedit_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdedit.ClickButtonArea
        msel = 2
        Call loaddata()
        flx.Focus()
    End Sub

    Private Sub loaddata()
        Dim msql As String

        msql = "select * from " & Trim(mcostdbnam) & ".dbo.empmaster  order by empid"


        'msql = "select * from inward1 where date>='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "'"


        Dim CMD As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        CMD.CommandTimeout = 300
        'MsgBox(msql)
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()
        flx.Clear()
        Call flxhead()
        flx.Visible = False
        Try
            ''Dim DR As SqlDataReader
            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                With flx
                    While DR.Read
                        .Rows = .Rows + 1
                        .Row = .Rows - 1


                        .set_TextMatrix(.Row, 1, DR.Item("empid"))
                        .set_TextMatrix(.Row, 2, DR.Item("empname") & vbNullString)
                        .set_TextMatrix(.Row, 3, DR.Item("department") & vbNullString)
                        .set_TextMatrix(.Row, 4, DR.Item("salary") & vbNullString)
                        .set_TextMatrix(.Row, 5, DR.Item("subdept") & vbNullString)
                        .set_TextMatrix(.Row, 6, DR.Item("costtype") & vbNullString)
                        .set_TextMatrix(.Row, 7, DR.Item("target") & vbNullString)
                        .set_TextMatrix(.Row, 8, DR.Item("per") & vbNullString)
                        If IsDBNull(DR.Item("active")) = True Then
                            .set_TextMatrix(.Row, 9, "Y")
                        Else
                            .set_TextMatrix(.Row, 9, DR.Item("active"))
                        End If
                        .set_TextMatrix(.Row, 10, DR.Item("ih") & vbNullString)
                        .set_TextMatrix(.Row, 11, DR.Item("sc") & vbNullString)
                        .set_TextMatrix(.Row, 12, DR.Item("lineno") & vbNullString)


                    End While
                End With
            End If

            DR.Close()

        Catch sqlEx As sqlException  '
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

        flx.Visible = True

    End Sub

    Private Sub cmdedit_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdedit.LostFocus
        'If msel > 1 Then
        '    Call loaddata()
        'End If
    End Sub

    

    Private Sub cmdexit_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.ClickButtonArea
        Me.Close()
    End Sub

    Private Sub cmdsel_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdsel.ClickButtonArea
        Call selall()
    End Sub

    Private Sub cmdupdt_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdupdt.ClickButtonArea
        Call savrec()
    End Sub

    Private Sub flx_KeyUpEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyUpEvent) Handles flx.KeyUpEvent
        If e.keyCode = Keys.F2 Then
            flx.Rows = flx.Rows + 1
            flx.Row = flx.Rows - 1

        End If
    End Sub

    Private Sub cmddel_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmddel.ClickButtonArea

    End Sub
End Class
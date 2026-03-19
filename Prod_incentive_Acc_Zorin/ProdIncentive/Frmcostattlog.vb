Imports System.IO
Imports AxMSFlexGridLib
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class Frmcostattlog
    Dim msql, msql2, msql3, msql4 As String
    Dim mdocno As Long
    Dim j, i, msel As Int32
    Dim trans As SqlTransaction


    Private Sub Frmcostattlog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.MdiParent = MDIForm
        Me.Height = MDIFORM1.Height - 25
        Me.Width = My.Computer.Screen.Bounds.Width
        Call flxhead()
    End Sub

    Private Sub flxmhead()

        flxm.Rows = 1
        flxm.Cols = 2
        flxm.set_ColWidth(0, 1300)
        flxm.set_ColWidth(1, 1200)

        'flx.set_ColWidth(3, 1500)
        'flx.set_ColWidth(4, 1500)
        'flx.set_ColWidth(5, 1200)
        'flx.set_ColWidth(6, 1000)
        'flx.set_ColWidth(7, 1000)
        'flx.set_ColWidth(8, 1000)
        'flx.set_ColWidth(9, 1000)

        flxm.Row = 0
        flxm.Col = 0
        flxm.CellAlignment = 3
        flxm.CellFontBold = True
        flxm.set_TextMatrix(0, 0, "Date")

        flxm.Col = 1
        flxm.CellAlignment = 3
        flxm.CellFontBold = True
        flxm.set_TextMatrix(0, 1, "Qty")

    End Sub

    Private Sub flxhead()
        flx.Rows = 1
        flx.Cols = 3
        flx.set_ColWidth(0, 1900)
        flx.set_ColWidth(1, 1400)
        flx.set_ColWidth(2, 1200)
        'flx.set_ColWidth(3, 1500)
        'flx.set_ColWidth(4, 1500)
        'flx.set_ColWidth(5, 1200)
        'flx.set_ColWidth(6, 1000)
        'flx.set_ColWidth(7, 1000)
        'flx.set_ColWidth(8, 1000)
        'flx.set_ColWidth(9, 1000)

        flx.Row = 0
        flx.Col = 0
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 0, "empid")

        flx.Col = 1
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 1, "prsnt")

        flx.Col = 2
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 2, "OTHOUR")

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



        'flx.set_ColAlignment(0, 2)
        flx.Rows = flx.Rows + 1
        flx.Row = flx.Rows - 1
    End Sub

    Private Sub flx_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flx.Enter

    End Sub

    Private Sub flx_KeyPressEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyPressEvent) Handles flx.KeyPressEvent
        If e.keyAscii = 22 Then
            flx.Visible = False
            Call exeltoflx(flx)
            flx.Visible = True
            lbltot.Text = flx.Rows - 1
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
        If flx.Col = 1 Or flx.Col = 2 Then
            editflx(flx, e.keyAscii, flx)
        End If
    End Sub

    Private Sub delrec()
        'If MsgBox("Delete ? R U Sure!", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then

        If MsgBox("Delete ? R U Sure! No Doubt!", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            trans = con.BeginTransaction
            Dim CMD As New SqlCommand("delete from " & Trim(mcostdbnam) & ".dbo.attlog where attdate='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'", con, trans)


            Try
                CMD.ExecuteNonQuery()
                trans.Commit()
                msel = 0
                flx.Clear()
                flxhead()
                CMD.Dispose()
            Catch ex As Exception
                trans.Rollback()
                MsgBox(ex.Message)
                CMD.Dispose()
            End Try


            CMD.Dispose()
            CMD = Nothing



        End If
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

            If Val(Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "dd")) > 0 Then


                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                trans = con.BeginTransaction
                If msel = 2 Then
                    Dim CMD As New SqlCommand("delete from " & Trim(mcostdbnam) & ".dbo.attlog where attdate='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'", con, trans)

                    CMD.ExecuteNonQuery()
                End If

                Try
                    For j = 1 To flx.Rows - 1
                        msql2 = "insert into " & Trim(mcostdbnam) & ".dbo.attlog (attdate,empid,prsnt,othour) " & vbCrLf _
                       & " values( '" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'," & Val(flx.get_TextMatrix(j, 0)) & "," & Val(flx.get_TextMatrix(j, 1)) & ",'" & IIf(Len(Trim(flx.get_TextMatrix(j, 2))) > 0, flx.get_TextMatrix(j, 2), "00:00:00") & "' )"

                        'If con.State = ConnectionState.Closed Then
                        '    con.Open()
                        'End If

                        ' trans = con.BeginTransaction

                        Dim dcmd As New SqlCommand(msql2, con, trans)

                        dcmd.ExecuteNonQuery()
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
                MsgBox("Select the  Date!")
            End If

        End If
            'mdocno = 0

            'If chksr.Checked = True Then
            '    Call updtshowcode()
            'End If


    End Sub

    Private Sub cmdadd_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.ClickButtonArea
        msel = 1
        mskdatefr.Focus()

    End Sub

    Private Sub cmdedit_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdedit.ClickButtonArea
        msel = 2
        mskdatefr.Focus()
    End Sub

    Private Sub cmddel_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmddel.ClickButtonArea
        msel = 3
        delrec()

        'mskdatefr.Focus()
    End Sub

    Private Sub cmdupdt_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdupdt.ClickButtonArea
        Call savrec()
        flx.Clear()
        flxhead()
    End Sub
    Private Sub loaddata()
        Dim msql As String
        If Val(Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "dd")) > 0 Then
            msql = "select * from " & Trim(mcostdbnam) & ".dbo.attlog where attdate='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' order by empid"


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


                            .set_TextMatrix(.Row, 0, DR.Item("empid"))
                            .set_TextMatrix(.Row, 1, DR.Item("prsnt"))
                            .set_TextMatrix(.Row, 2, DR.Item("othour"))

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
        End If
        flx.Visible = True
        lbltot.Text = flx.Rows - 1
    End Sub

    Private Sub mskdatefr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mskdatefr.KeyPress
        If msel > 1 Then
            If Asc(e.KeyChar) = 13 Then
                flx.Focus()
            End If
        End If
    End Sub

    Private Sub mskdatefr_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles mskdatefr.LostFocus
        If msel > 1 Then
            Call loaddata()
          
        End If
    End Sub

    Private Sub mskdatefr_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskdatefr.MaskInputRejected

    End Sub

    Private Sub cmdexit_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.ClickButtonArea
        Me.Close()
    End Sub

    Private Sub mskdatefr_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mskdatefr.MouseDown

    End Sub

    Private Sub savmaniron()
        If msel = 1 Or msel = 2 Then

            If Val(Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "dd")) > 0 Then


                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                trans = con.BeginTransaction

                Dim CMD As New SqlCommand("delete from " & Trim(mcostdbnam) & ".dbo.manualiron where idate='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'", con, trans)

                CMD.ExecuteNonQuery()


                Try
                    msql2 = "insert into " & Trim(mcostdbnam) & ".dbo.manualiron (idate,qty) " & vbCrLf _
                       & " values( '" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'," & Val(txtqty.Text) & " )"


                    Dim dcmd As New SqlCommand(msql2, con, trans)
                    dcmd.ExecuteNonQuery()

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
                MsgBox("Select the  Date!")
            End If

        End If
    End Sub

    Private Sub cmdirsave_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdirsave.ClickButtonArea
        savmaniron()
    End Sub



    Private Sub loaddata2()
        Dim msql As String
        If Val(Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "dd")) > 0 Then
            msql = "select * from " & Trim(mcostdbnam) & ".dbo.manualiron where attdate='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' order by idate"


            'msql = "select * from inward1 where date>='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and date<='" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "'"


            Dim CMD As New SqlCommand(msql, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            CMD.CommandTimeout = 300
            'MsgBox(msql)
            'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
            'trans.Begin()
            flxm.Clear()

            Call flxmhead()
            flx.Visible = False
            Try
                ''Dim DR As SqlDataReader
                Dim DR As SqlDataReader
                DR = CMD.ExecuteReader
                If DR.HasRows = True Then
                    With flxm
                        While DR.Read
                            .Rows = .Rows + 1
                            .Row = .Rows - 1

                            .set_TextMatrix(.Row, 0, DR.Item("Date"))
                            .set_TextMatrix(.Row, 1, DR.Item("Qty"))

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
        End If
        flx.Visible = True
        lbltot.Text = flx.Rows - 1
    End Sub

    Private Sub cmddisp_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.ClickButtonArea
        loaddata2()
    End Sub
End Class
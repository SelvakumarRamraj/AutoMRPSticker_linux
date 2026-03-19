Imports System.IO
Imports System.Data
Public Class Frmsrbarcode
    Dim msql, msql2, msql3, msql4 As String
    Dim mdocno As Long
    Dim j, i, msel As Int32
    Private Sub Frmsrbarcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.MdiParent = MDIForm
        Me.Height = MDIFORM1.Height - 25
        Me.Width = My.Computer.Screen.Bounds.Width

        loadcomboshow("ocrd", "Cardname", cmbparty, "cardname", "cardtype")
        Call flxhead()
        txtno.Focus()
    End Sub
    Private Sub loadlineid()
        Dim mst As String
        mst = Microsoft.VisualBasic.Left(flx.get_TextMatrix(2, 0), 1)
        For i = 1 To flx.Rows - 1
            flx.set_TextMatrix(i, 0, Replace(flx.get_TextMatrix(i, 0), Trim(mst), ""))
            flx.set_TextMatrix(i, 0, Replace(flx.get_TextMatrix(i, 0), "", ""))
            'flx.set_TextMatrix(i, 2, i)
            flx.set_TextMatrix(i, 2, (i - 1))
        Next i
    End Sub

    Private Sub flxhead()
        flx.Rows = 1
        flx.Cols = 6
        flx.set_ColWidth(0, 1900)
        flx.set_ColWidth(1, 1400)
        flx.set_ColWidth(2, 1200)
        flx.set_ColWidth(3, 1500)
        flx.set_ColWidth(4, 1500)
        flx.set_ColWidth(5, 1200)
        'flx.set_ColWidth(6, 1000)
        'flx.set_ColWidth(7, 1000)
        'flx.set_ColWidth(8, 1000)
        'flx.set_ColWidth(9, 1000)

        flx.Row = 0
        flx.Col = 0
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 0, "Itemcode")

        flx.Col = 1
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 1, "Quantity")

        flx.Col = 2
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 2, "Linenum")

        flx.Col = 3
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 3, "BATCH")


        flx.Col = 4
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 4, "Color Name")

        flx.Col = 5
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 5, "StickerDate")

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



        flx.set_ColAlignment(0, 2)




    End Sub
    Private Sub cmbparty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbparty.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtcardcode.Text = findpartycode(cmbparty, 1, 0)

            If flx.Row >= 1 Then
                flx.Row = 1
                flx.Col = 0
                flx.Focus()
            Else
                flx.Rows = flx.Rows + 1
                flx.Row = flx.Rows - 1
                flx.Row = 1
                flx.Col = 0
                flx.Focus()
            End If

        End If
    End Sub

    Private Sub cmbparty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbparty.LostFocus
        txtcardcode.Text = findpartycode(cmbparty, 1, 0)
    End Sub

    Private Sub cmbparty_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbparty.SelectedIndexChanged

    End Sub

    Private Sub flx_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flx.Enter

    End Sub

    Private Sub flx_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles flx.GotFocus
        If flx.Row >= 1 Then
            flx.Row = 1
            flx.Col = 0
            flx.Focus()
        Else
            flx.Rows = flx.Rows + 1
            flx.Row = flx.Rows - 1
            flx.Row = 1
            flx.Col = 0
            flx.Focus()
        End If
    End Sub

    Private Sub cmdadd_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.ClickButtonArea
        msel = 1
        Call autono()
        txtno.Focus()
    End Sub

    Private Sub autono()
        Dim CMD2 As New OleDb.OleDbCommand("select max(docnum) as no from srbarhead", con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim CBNO As Int32 = IIf(IsDBNull(CMD2.ExecuteScalar) = False, CMD2.ExecuteScalar, 0)

        txtno.Text = CBNO + 1
        CMD2.Dispose()
    End Sub

    Private Sub delrec()
        If MsgBox("Delete ? R U Sure!", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then

            If MsgBox("Delete ? R U Sure! No Doubt!", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
                Dim CMD As New OleDb.OleDbCommand("delete from srbarhead where docnum=" & Val(txtno.Text), con)
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
                CMD = Nothing


                Dim CMD3 As New OleDb.OleDbCommand("delete from srbardet where docentry=" & Val(txtno.Text), con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                Try
                    CMD3.ExecuteNonQuery()
                    CMD3.Dispose()
                Catch ex As Exception
                    MsgBox(ex.Message)
                    CMD3.Dispose()
                End Try


                CMD3.Dispose()
                CMD3 = Nothing
            End If
        End If
    End Sub

    Private Sub savrec()
        'Dim CMD2 As New OleDb.OleDbCommand("select max(docentry) as no from [@inc_otar]", con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'Dim CBNO As Int32 = IIf(IsDBNull(CMD2.ExecuteScalar) = False, CMD2.ExecuteScalar, 0)

        'mdocno = CBNO + 1
        'CMD2.Dispose()



        mdocno = Val(txtno.Text)

        If mdocno > 0 Then
            If msel = 1 Then
                msql = "insert into srbarhead(DocEntry,DocNum,docdate,cardcode,cardname) " & vbCrLf _
                 & "select " & mdocno & " as docentry," & mdocno & " as docnum,CONVERT(datetime,'" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "',102) as docdate,'" & Trim(txtcardcode.Text) & "','" & Trim(cmbparty.Text) & "'"


                

                'msql2 = "insert into [@inc_tar1] (docentry,LineId,VisOrder,Object,U_Ref4,U_Ref5,U_state,U_reptype,U_Brand,U_Rbm,U_Arcode,U_Target) " & vbCrLf _
                ' & "select " & mdocno & " as docentry,ROW_NUMBER() over(order by repname,brand) as lineid,(ROW_NUMBER() over(order by repname,brand))-1 as visorder ,'OTAR' as object, '" & Trim(flx.get_TextMatrix(j, 6)) & "' as u_ref4 ,'" & Trim(flx.get_TextMatrix(j, 7)) & "' as u_ref5,'" & Trim(flx.get_TextMatrix(j, 0)) & "' as u_state, '" & vbCrLf _
                ' & Trim(flx.get_TextMatrix(j, 1)) & "'  as u_reptype,'" & Trim(flx.get_TextMatrix(j, 2)) & "' as u_brand,'" & Trim(flx.get_TextMatrix(j, 3)) & "' as u_rbm,'" & Trim(flx.get_TextMatrix(j, 4)) & "' as u_arcode,'" & Trim(flx.get_TextMatrix(j, 5)) & "' as u_target from rrtarget3 where [MONTH] is not null and [month]='" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "MMM") & "' and [year]=" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy") & ""



                Dim CMD As New OleDb.OleDbCommand(msql, con)

                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                'dCMD.ExecuteNonQuery()


                Try
                    CMD.ExecuteNonQuery()
                    CMD.Dispose()
                    'CMD2.Dispose()
                Catch ex As Exception
                    'TRANS.Rollback()
                    MsgBox(ex.Message)
                    CMD.Dispose()
                    'CMD2.Dispose()
                End Try

                'TRANS.Commit()
                CMD.Dispose()
                CMD = Nothing



                For j = 1 To flx.Rows - 1

                    If chkrhl.Checked = True Then
                        msql2 = "insert into srbardet (docentry,docnum,Docdate,cardcode,cardname,itemcode,quantity,linenum,scode,batchnum,color,STIKDATE) " & vbCrLf _
                       & " values( " & mdocno & "," & mdocno & ",'" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "','" & Trim(txtcardcode.Text) & "','" & Trim(cmbparty.Text) & "','" & Trim(flx.get_TextMatrix(j, 0)) & "'," & Val(flx.get_TextMatrix(j, 1)) & "," & Val(flx.get_TextMatrix(j, 2)) & ",'" & Trim(flx.get_TextMatrix(j, 0)) & "','" & Trim(flx.get_TextMatrix(j, 3)) & "','" & Trim(flx.get_TextMatrix(j, 4)) & "','" & Microsoft.VisualBasic.Format(CDate(flx.get_TextMatrix(j, 5)), "yyyy-MM-dd") & "' )"
                    Else
                        msql2 = "insert into srbardet (docentry,docnum,Docdate,cardcode,cardname,itemcode,quantity,linenum,scode,batchnum,color,STIKDATE) " & vbCrLf _
                      & " values( " & mdocno & "," & mdocno & ",'" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "','" & Trim(txtcardcode.Text) & "','" & Trim(cmbparty.Text) & "','" & Trim(flx.get_TextMatrix(j, 0)) & "'," & Val(flx.get_TextMatrix(j, 1)) & "," & Val(flx.get_TextMatrix(j, 2)) & ",'" & Mid(Trim(flx.get_TextMatrix(j, 0)), 5, 16) & "','" & Trim(flx.get_TextMatrix(j, 3)) & "','" & Trim(flx.get_TextMatrix(j, 4)) & "','" & Microsoft.VisualBasic.Format(CDate(flx.get_TextMatrix(j, 5)), "yyyy-MM-dd") & "' )"
                    End If


                    ' msql2 = "insert into srbardet (docentry,docnum,Docdate,cardcode,cardname,itemcode,quantity,linenum,scode,batchnum,color) " & vbCrLf _
                    ' & " values( " & mdocno & "," & mdocno & ",'" & IIf(Len(Trim(flx.get_TextMatrix(j, 5))) > 0, Microsoft.VisualBasic.Format(CDate(flx.get_TextMatrix(j, 5)), "yyyy-MM-dd"), Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd")) & "','" & Trim(txtcardcode.Text) & "','" & Trim(cmbparty.Text) & "','" & Trim(flx.get_TextMatrix(j, 0)) & "'," & Val(flx.get_TextMatrix(j, 1)) & "," & Val(flx.get_TextMatrix(j, 2)) & ",'" & Mid(Trim(flx.get_TextMatrix(j, 0)), 5, 16) & "','" & Trim(flx.get_TextMatrix(j, 3)) & "','" & Trim(flx.get_TextMatrix(j, 4)) & "')"

                    '& "'" & Trim(flx.get_TextMatrix(j, 2)) & "','" & Trim(flx.get_TextMatrix(j, 3)) & "','" & Trim(flx.get_TextMatrix(j, 4)) & "','" & Trim(flx.get_TextMatrix(j, 5)) & "')"


                    Dim dcmd As New OleDb.OleDbCommand(msql2, con)
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If

                    Try
                        dcmd.ExecuteNonQuery()
                        dcmd.Dispose()
                        'CMD2.Dispose()
                    Catch ex As Exception
                        'TRANS.Rollback()
                        MsgBox(ex.Message)
                        dcmd.Dispose()
                        'CMD2.Dispose()
                    End Try

                    'TRANS.Commit()
                    dcmd.Dispose()
                    dcmd = Nothing
                Next j
                MsgBox("Saved!")


                mdocno = 0
            End If
            If chksr.Checked = True Then
                Call updtshowcode()
            End If

        End If
    End Sub
    Private Sub updtshowcode()


        'msql2 = "update srbardet set itemcode=c.itemcode from srbardet b,oitm c  where b.scode=c.U_Scode and b.docnum=" & Val(txtno.Text) & " and b.docdate='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'"
        If chkrhl.Checked = True Then
            msql2 = "update srbardet set itemcode=c.u_itemcode from srbardet b," & vbCrLf _
                       & "(select b.u_itemcode,convert(nvarchar(max),c.u_remarks) scode from [@ins_oplm] b " & vbCrLf _
                       & "inner join [@ins_plm1] c on c.docentry=b.docentry " & vbCrLf _
                       & "where len(rtrim(convert(nvarchar(max),c.u_remarks)))>0 group by b.u_itemcode,convert(nvarchar(max),c.u_remarks)) c " & vbCrLf _
                       & " where   b.scode=c.scode collate SQL_Latin1_General_CP1_CI_AS  and b.docnum=" & Val(txtno.Text) & " and b.docdate='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'"
        Else
            msql2 = "update srbardet set itemcode=c.itemcode from srbardet b,oitm c  where b.scode=c.U_Scode and b.docnum=" & Val(txtno.Text) & " and b.docdate='" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'"
        End If


        'insert into srbardet (docentry,docnum,Docdate,cardcode,cardname,itemcode,quantity,linenum,scode) " & vbCrLf _
        '             & " values( " & mdocno & "," & mdocno & ",'" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "','" & Trim(txtcardcode.Text) & "','" & Trim(cmbparty.Text) & "','" & Trim(flx.get_TextMatrix(j, 0)) & "'," & Val(flx.get_TextMatrix(j, 1)) & "," & Val(flx.get_TextMatrix(j, 2)) & ",'" & Mid(Trim(flx.get_TextMatrix(j, 0)), 5, 16) & "')"
        '        '& "'" & Trim(flx.get_TextMatrix(j, 2)) & "','" & Trim(flx.get_TextMatrix(j, 3)) & "','" & Trim(flx.get_TextMatrix(j, 4)) & "','" & Trim(flx.get_TextMatrix(j, 5)) & "')"


        Dim dcmd As New OleDb.OleDbCommand(msql2, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Try
            dcmd.ExecuteNonQuery()
            dcmd.Dispose()
            'CMD2.Dispose()
        Catch ex As Exception
            'TRANS.Rollback()
            MsgBox(ex.Message)
            dcmd.Dispose()
            'CMD2.Dispose()
        End Try

        'TRANS.Commit()
        dcmd.Dispose()
        dcmd = Nothing
    End Sub

    Private Sub cmddel_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmddel.ClickButtonArea
        msel = 2
        txtno.Focus()
    End Sub

    Private Sub cmdupdt_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdupdt.ClickButtonArea
        Call savrec()
        CLEAR(Me)
    End Sub

    Private Sub flx_KeyPressEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyPressEvent) Handles flx.KeyPressEvent
        'If e.keyAscii = 13 Then
        '    TextBox1.Text = Microsoft.VisualBasic.Left(flx.get_TextMatrix(flx.Row, 0), 1)
        'End If
        If e.keyAscii = 22 Then

            Call exeltoflx(flx)
            Call loadlineid()
        End If
    End Sub

    Private Sub txtno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtno.KeyPress
        If Asc(e.KeyChar) = 13 Then
            mskdatefr.Text = Format(Now(), "dd-MM-yyyy")
            mskdatefr.Focus()
        End If
    End Sub

    Private Sub txtno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtno.LostFocus
        If msel = 2 Then
            Call delrec()
            CLEAR(Me)
        End If
    End Sub

    Private Sub txtno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtno.TextChanged

    End Sub

    Private Sub cmdexit_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.ClickButtonArea
        Me.Close()
    End Sub

    Private Sub mskdatefr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mskdatefr.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbparty.Focus()
        End If
    End Sub
    Private Sub txtcardcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcardcode.KeyPress
        MsgBox(e.KeyChar)
    End Sub

    Private Sub txtcardcode_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtcardcode.KeyUp
        MsgBox(e.KeyCode)
    End Sub

    Private Sub txtcardcode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcardcode.LostFocus

    End Sub

    Private Sub txtcardcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcardcode.TextChanged

    End Sub
End Class
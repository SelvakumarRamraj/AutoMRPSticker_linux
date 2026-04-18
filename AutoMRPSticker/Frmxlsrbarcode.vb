Imports System.IO
Imports System.Data
Imports WashcareLbl.connection
Imports System.Text
Public Class Frmxlsrbarcode
    Dim msql, msql2, msql3, msql4 As String
    Dim mdocno As Long
    Dim strarr As String()
    Dim srcode As String
    Dim j, i, msel As Int32
    Private Sub Frmxlsrbarcode_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        msql = 0

        loadparty()
    End Sub

    Private Sub loadparty()
        msql = "select cardcode,cardname from ocrd where cardtype='C' and validFor='Y' group by cardcode,cardname order by cardname"

        Dim dtc As DataTable = getDataTable(msql)
        cmbparty.DataSource = dtc
        cmbparty.DisplayMember = "cardname"  ' text shown to user
        cmbparty.ValueMember = "cardcode"      ' underlying value
        cmbparty.SelectedIndex = -1
        'dtc.Dispose()
    End Sub

    Private Sub cmbparty_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbparty.SelectedIndexChanged
        If cmbparty.SelectedIndex <> -1 Then
            txtcardcode.Text = cmbparty.SelectedValue.ToString()
            'MessageBox.Show("Selected CardCode: " & cardCode)
        End If
    End Sub

    Private Sub cmdexit_Click(sender As System.Object, e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub autono()
        msql4 = "select max(docnum) as no from srbarhead"
        'Dim CMD2 As New OleDb.OleDbCommand("select max(docnum) as no from srbarhead", con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If
        Dim cbno As Int32 = Convert.ToInt32(Val(executescalarQuery(msql4)))



        'Dim CBNO As Int32 = IIf(IsDBNull(CMD2.ExecuteScalar) = False, CMD2.ExecuteScalar, 0)

        txtno.Text = CBNO + 1
        'CMD2.Dispose()
    End Sub

    Private Sub delrec()
        If MsgBox("Delete ? R U Sure!", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then

            If MsgBox("Delete ? R U Sure! No Doubt!", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then

                msql = "delete from srbarhead where docnum=" & Val(txtno.Text)
                msql2 = "delete from srbardet where docnum=" & Val(txtno.Text)
                'Dim CMD As New OleDb.OleDbCommand("delete from srbarhead where docnum=" & Val(txtno.Text), con)
                'If con.State = ConnectionState.Closed Then
                '    con.Open()
                'End If
                Try
                    executeQuery(msql)
                    executeQuery(msql2)
                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try

            End If

        End If
    End Sub

    Private Sub savrec()


        mdocno = Val(txtno.Text)

        If mdocno > 0 Then
            If msel = 1 Then
                msql = "insert into srbarhead(DocEntry,DocNum,docdate,cardcode,cardname) " & vbCrLf _
                 & "select " & mdocno & " as docentry," & mdocno & " as docnum,CONVERT(datetime,'" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "',102) as docdate,'" & Trim(txtcardcode.Text) & "','" & Trim(cmbparty.Text) & "'"




                'msql2 = "insert into [@inc_tar1] (docentry,LineId,VisOrder,Object,U_Ref4,U_Ref5,U_state,U_reptype,U_Brand,U_Rbm,U_Arcode,U_Target) " & vbCrLf _
                ' & "select " & mdocno & " as docentry,ROW_NUMBER() over(order by repname,brand) as lineid,(ROW_NUMBER() over(order by repname,brand))-1 as visorder ,'OTAR' as object, '" & Trim(flx.get_TextMatrix(j, 6)) & "' as u_ref4 ,'" & Trim(flx.get_TextMatrix(j, 7)) & "' as u_ref5,'" & Trim(flx.get_TextMatrix(j, 0)) & "' as u_state, '" & vbCrLf _
                ' & Trim(flx.get_TextMatrix(j, 1)) & "'  as u_reptype,'" & Trim(flx.get_TextMatrix(j, 2)) & "' as u_brand,'" & Trim(flx.get_TextMatrix(j, 3)) & "' as u_rbm,'" & Trim(flx.get_TextMatrix(j, 4)) & "' as u_arcode,'" & Trim(flx.get_TextMatrix(j, 5)) & "' as u_target from rrtarget3 where [MONTH] is not null and [month]='" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "MMM") & "' and [year]=" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy") & ""



                'Dim CMD As New OleDb.OleDbCommand(msql, con)

                'If con.State = ConnectionState.Closed Then
                '    con.Open()
                'End If

                ''dCMD.ExecuteNonQuery()


                Try
                    executeQuery(msql)
                    'CMD.ExecuteNonQuery()
                    'CMD.Dispose()
                    'CMD2.Dispose()
                Catch ex As Exception
                    'TRANS.Rollback()
                    MsgBox(ex.Message)
                    'CMD.Dispose()
                    'CMD2.Dispose()
                End Try

                'TRANS.Commit()
                'CMD.Dispose()
                'CMD = Nothing



                'For j = 1 To flx.Rows - 1
                For j = 0 To dg1.Rows.Count - 1

                    'Dim stikDateValue As String = "NULL" ' default

                    'Dim cellValue = dg1.Rows(j).Cells(5).Value

                    'If cellValue IsNot Nothing AndAlso Not IsDBNull(cellValue) AndAlso Not String.IsNullOrWhiteSpace(cellValue.ToString()) Then
                    '    Dim tempDate As Date
                    '    If Date.TryParse(cellValue.ToString(), tempDate) Then
                    '        stikDateValue = "'" & tempDate.ToString("yyyy-MM-dd") & "'"
                    '    End If
                    'End If

                    If dg1.Rows(j).Cells(0).Value.ToString().Trim().IndexOf("-"c) >= 0 Then
                        strarr = dg1.Rows(j).Cells(0).Value.ToString().Trim().Split("-")
                        srcode = strarr(1).ToString()
                        If Len(Trim(dg1.Rows(j).Cells(3).Value)) > 0 Then
                            srcode = srcode.Replace(dg1.Rows(j).Cells(3).Value.ToString(), "")
                        Else

                        End If

                    Else
                        srcode = Mid(Trim(dg1.Rows(j).Cells(0).Value), 5, 16)
                        If Len(Trim(dg1.Rows(j).Cells(3).Value)) > 0 Then
                            srcode = srcode.Replace(dg1.Rows(j).Cells(3).Value.ToString(), "")
                        Else
                        End If

                    End If



                    If chkrhl.Checked = True Then
                        msql2 = "insert into srbardet (docentry,docnum,Docdate,cardcode,cardname,itemcode,quantity,linenum,scode,batchnum,color,STIKDATE,mrp) " & vbCrLf _
                       & " values( " & mdocno & "," & mdocno & ",'" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "','" & Trim(txtcardcode.Text) & "','" & Trim(cmbparty.Text) & "','" & Trim(dg1.Rows(j).Cells(0).Value) & "'," & Val(dg1.Rows(j).Cells(1).Value) & "," & Val(dg1.Rows(j).Cells(2).Value) & ",'" & Trim(dg1.Rows(j).Cells(0).Value) & "','" & Trim(dg1.Rows(j).Cells(3).Value) & "','" & Trim(dg1.Rows(j).Cells(4).Value) & "','" & Microsoft.VisualBasic.Format(CDate(dg1.Rows(j).Cells(5).Value), "yyyy-MM-dd") & "'," & Val(dg1.Rows(j).Cells(6).Value) & ")"
                    Else
                        msql2 = "insert into srbardet (docentry,docnum,Docdate,cardcode,cardname,itemcode,quantity,linenum,scode,batchnum,color,STIKDATE,mrp,sbarcode) " & vbCrLf _
                        & " values( " & mdocno & "," & mdocno & ",'" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "','" & Trim(txtcardcode.Text) & "','" & Trim(cmbparty.Text) & "','" & Trim(dg1.Rows(j).Cells(0).Value) & "'," & Val(dg1.Rows(j).Cells(1).Value) & "," & Val(dg1.Rows(j).Cells(2).Value) & ",'" & Trim(srcode) & "','" & Trim(dg1.Rows(j).Cells(3).Value.ToString()) & "','" & Trim(dg1.Rows(j).Cells(4).Value) & "','" & Microsoft.VisualBasic.Format(CDate(dg1.Rows(j).Cells(5).Value), "yyyy-MM-dd") & "'," & Val(dg1.Rows(j).Cells(6).Value) & ",'" & dg1.Rows(j).Cells(7).Value & "' )"

                        ' msql2 = "insert into srbardet (docentry,docnum,Docdate,cardcode,cardname,itemcode,quantity,linenum,scode,batchnum,color,STIKDATE,mrp) " & vbCrLf _
                        '& " values( " & mdocno & "," & mdocno & ",'" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "','" & Trim(txtcardcode.Text) & "','" & Trim(cmbparty.Text) & "','" & Trim(dg1.Rows(j).Cells(0).Value) & "'," & Val(dg1.Rows(j).Cells(1).Value) & "," & Val(dg1.Rows(j).Cells(2).Value) & ",'" & Mid(Trim(dg1.Rows(j).Cells(0).Value), 5, 16) & "','" & Trim(dg1.Rows(j).Cells(3).Value) & "','" & Trim(dg1.Rows(j).Cells(4).Value) & "','" & Microsoft.VisualBasic.Format(CDate(dg1.Rows(j).Cells(5).Value), "yyyy-MM-dd") & "'," & Val(dg1.Rows(j).Cells(6).Value) & ")"


                    End If


                    ' msql2 = "insert into srbardet (docentry,docnum,Docdate,cardcode,cardname,itemcode,quantity,linenum,scode,batchnum,color) " & vbCrLf _
                    ' & " values( " & mdocno & "," & mdocno & ",'" & IIf(Len(Trim(flx.get_TextMatrix(j, 5))) > 0, Microsoft.VisualBasic.Format(CDate(flx.get_TextMatrix(j, 5)), "yyyy-MM-dd"), Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd")) & "','" & Trim(txtcardcode.Text) & "','" & Trim(cmbparty.Text) & "','" & Trim(flx.get_TextMatrix(j, 0)) & "'," & Val(flx.get_TextMatrix(j, 1)) & "," & Val(flx.get_TextMatrix(j, 2)) & ",'" & Mid(Trim(flx.get_TextMatrix(j, 0)), 5, 16) & "','" & Trim(flx.get_TextMatrix(j, 3)) & "','" & Trim(flx.get_TextMatrix(j, 4)) & "')"

                    '& "'" & Trim(flx.get_TextMatrix(j, 2)) & "','" & Trim(flx.get_TextMatrix(j, 3)) & "','" & Trim(flx.get_TextMatrix(j, 4)) & "','" & Trim(flx.get_TextMatrix(j, 5)) & "')"


                    'Dim dcmd As New OleDb.OleDbCommand(msql2, con)
                    'If con.State = ConnectionState.Closed Then
                    '    con.Open()
                    'End If

                    Try
                        executeQuery(msql2)
                        'dcmd.ExecuteNonQuery()
                        'dcmd.Dispose()

                    Catch ex As Exception
                        'TRANS.Rollback()
                        MsgBox(ex.Message)
                        'dcmd.Dispose()
                        'CMD2.Dispose()
                    End Try

                    'TRANS.Commit()

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


        'Dim dcmd As New OleDb.OleDbCommand(msql2, con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        Try
            executeQuery(msql2)
            'dcmd.ExecuteNonQuery()
            'dcmd.Dispose()

        Catch ex As Exception
            'TRANS.Rollback()
            MsgBox(ex.Message)
            'dcmd.Dispose()

        End Try

        'TRANS.Commit()
       
    End Sub

    Private Sub cmddel_Click(sender As System.Object, e As System.EventArgs) Handles cmddel.Click
        msel = 2
        txtno.Focus()
    End Sub

    Private Sub cmdupdt_Click(sender As System.Object, e As System.EventArgs) Handles cmdupdt.Click
        Call savrec()
        clearall()
    End Sub

    Private Sub clearall()
        dg1.Rows.Clear()
        cmbparty.SelectedIndex = -1
        txtcardcode.Text = ""
        mskdatefr.Text = Microsoft.VisualBasic.Format(Now(), "dd-MM-yyyy")
        txtno.Text = ""

    End Sub

    Private Sub txtno_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtno.KeyPress
        If Asc(e.KeyChar) = 13 Then
            mskdatefr.Text = Format(Now(), "dd-MM-yyyy")
            mskdatefr.Focus()
        End If
    End Sub

    Private Sub txtno_LostFocus(sender As Object, e As System.EventArgs) Handles txtno.LostFocus
        If msel = 2 Then
            Call delrec()
            clearall()
        End If
    End Sub

    Private Sub txtno_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtno.TextChanged

    End Sub

    Private Sub mskdatefr_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles mskdatefr.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbparty.Focus()
        End If
    End Sub

    Private Sub mskdatefr_MaskInputRejected(sender As System.Object, e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskdatefr.MaskInputRejected

    End Sub

    Private Sub cmdadd_Click(sender As System.Object, e As System.EventArgs) Handles cmdadd.Click
        msel = 1
        Call autono()
        mskdatefr.Text = Format(Now, "dd-MM-yyyy")
        txtno.Focus()
    End Sub

    Private Sub dg1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg1.CellContentClick

    End Sub

    Private Sub dg1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles dg1.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Dim clipboardText As String = Clipboard.GetText()
            If String.IsNullOrWhiteSpace(clipboardText) Then Exit Sub

            ' Split clipboard text into lines (rows)
            Dim lines() As String = clipboardText.Split(New String() {vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries)

            ' Get starting cell
            Dim startRow As Integer = If(dg1.CurrentCell IsNot Nothing, dg1.CurrentCell.RowIndex, 0)
            Dim startCol As Integer = If(dg1.CurrentCell IsNot Nothing, dg1.CurrentCell.ColumnIndex, 0)

            For i As Integer = 0 To lines.Length - 1
                Dim values() As String = lines(i).Split(vbTab) ' Excel separates by TAB

                ' Add new row if needed
                If startRow + i >= dg1.Rows.Count Then
                    dg1.Rows.Add()
                End If

                For j As Integer = 0 To values.Length - 1
                    If startCol + j < dg1.Columns.Count Then
                        dg1.Rows(startRow + i).Cells(startCol + j).Value = values(j).Trim()
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub cmdcls_Click(sender As System.Object, e As System.EventArgs) Handles cmdcls.Click
        clearall()
    End Sub
End Class
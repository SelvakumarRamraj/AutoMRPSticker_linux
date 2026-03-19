Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Public Class Frmincentive
    Dim msql, msql2, msql3, msql4 As String
    Dim mdocno, mdocentry As Long
    Dim j, i, msel As Int32
    'Public NotInheritable Class Clipboard
    Private Sub Frmincentive_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CMBYR.Items.Clear()
        Call flxhead()
        Call LOADYR()

        'msql = "insert into [@inc_otar](DocEntry,DocNum,period,Instance,series,Handwrtten,Canceled,Object,UserSign,Transfered,Status,CreateDate,CreateTime,UpdateDate,UpdateTime,DataSource,U_DocDate,U_EffctFrom,U_EffctTo) " & vbCrLf _
        '   & "select 5 as docentry,4 as docnum,15 as period,0 as instance,67 as series,'N' as handwritten,'N' as Canceled,'OTAR' as [object],31 as usersign,'N' as Transfered,0 as status,CONVERT(datetime,'2014-06-30',102) as createdate, " & vbCrLf _
        '  & "1435 as createtime,CONVERT(datetime,'2014-06-30',102) as updatedate,1257 as updatetime,'I' as datasource,CONVERT(datetime,'2014-06-30',102) as u_docdate,CONVERT(datetime,'2014-06-01',102) as u_effctfrom,CONVERT(datetime,'2014-06-30',102) as u_effctto "

        'msql2 = "insert into [@inc_tar1] (docentry,LineId,VisOrder,Object,U_Ref4,U_Ref5,U_state,U_reptype,U_Brand,U_Rbm,U_Arcode,U_Target) " & vbCrLf _
        ' & "select 5 as docentry,ROW_NUMBER() over(order by repname,brand) as lineid,(ROW_NUMBER() over(order by repname,brand))-1 as visorder ,'OTAR' as object, [MONTH] as u_ref4 ,[year] as u_ref5,state as u_state, " & vbCrLf _
        ' & "repname as u_reptype,brand as u_brand,rbm as u_rbm,areacode as u_arcode,[target] as u_target from rrtarget3 where [MONTH] is not null and [month]='June' and [year]=2014"

    End Sub

    Private Sub flx_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flx.Enter
        'Clipboard.SetDataObject(textBox1.SelectedText) 'copy
        ' Dim iData As IDataObject = Clipboard.GetDataObject() paste
        'textBox2.Text = CType(iData.GetData(DataFormats.Text), String) paste
    End Sub
    Private Sub flxhead()
        flx.Rows = 1
        flx.Cols = 10
        flx.set_ColWidth(0, 1000)
        flx.set_ColWidth(1, 1500)
        flx.set_ColWidth(2, 1500)
        flx.set_ColWidth(3, 1500)
        flx.set_ColWidth(4, 1500)
        flx.set_ColWidth(5, 1300)
        flx.set_ColWidth(6, 1000)
        flx.set_ColWidth(7, 1000)
        flx.set_ColWidth(8, 1000)
        flx.set_ColWidth(9, 1000)

        flx.Row = 0
        flx.Col = 0
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 0, "State")

        flx.Col = 1
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 1, "Rep Name")

        flx.Col = 2
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 2, "Brand Name")

        flx.Col = 3
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 3, "RBM Name")


        flx.Col = 4
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 4, " Name")

        flx.Col = 5
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 5, "Target")

        flx.Col = 6
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 6, "Month")

        flx.Col = 7
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 7, "Year")

        flx.Col = 8
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 8, "Line Id")

        flx.Col = 9
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 9, "Visorder")








    End Sub
    Private Sub flx_KeyPressEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyPressEvent) Handles flx.KeyPressEvent
        'If e.keyAscii = 13 Then
        ' Call exeltoflx(flx)
        ' End If
        If e.keyAscii = 22 Then
            Call exeltoflx(flx)
            Call loadlineid()
        End If
        'MsgBox(e.keyAscii)
        'Select Case e.keyAscii
        '    Case Convert.ToChar(3) 'Ctrl-C Copy
        '        'Do something when Ctrl-C encountered
        '    Case Convert.ToChar(22) 'Ctrl-V Paste
        '        'Do something when Ctrl-C encountered
        '    Case Convert.ToChar(24) 'Ctrl-X Cut
        '        'Do something when Ctrl-C encountered
        'End Select

    End Sub
    Private Sub loadlineid()
        For i = 1 To flx.Rows - 1
            flx.set_TextMatrix(i, 8, i)
            flx.set_TextMatrix(i, 9, (i - 1))
        Next i
    End Sub
    Private Sub flx_KeyUpEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyUpEvent) Handles flx.KeyUpEvent
        'if e.keycode=
        'Dim ShiftDown, AltDown, ctrldown, Txt
        'ShiftDown = (Shift And vbShiftMask) > 0
        'AltDown = (Shift And vbAltMask) > 0
        'ctrldown = (Shift And vbCtrlMask) > 0

        'If e.keyCode = Keys.V Then
        '    If Keys.Control.ToString Then
        '        Call exeltoflx(flx)
        '    End If
        'End If
        'If e.keyCode = Keys.F12 Then
        '    Call exeltoflx(flx)
        'MsgBox(e.keyCode)
        'End If
        'Select Case e.keyCode
        '    Case Keys.Modifiers = Keys.V
        'Call exeltoflx(flx)
        'End Select
        'If Keys.Control.ToString Then
        'If e.keyCode = Keys.F12 Then
        '    exeltoflx(flx)
        '    '  If e.Control.ToString And e.keyCode = Keys.P Then

        'End If
    End Sub

    Private Sub cmdadd_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.ClickButtonArea
        msel = 1
        Call autono()
        txtno.Focus()
        ''msql4 = "delete from bartemp "

        ''Dim dCMD As New OleDb.OleDbCommand(" select max(docentry) as no from [@inc_otar]", con)
        'Dim CMD2 As New OleDb.OleDbCommand("select max(docentry) as no from [@inc_otar]", con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'Dim CBNO As Int32 = IIf(IsDBNull(CMD2.ExecuteScalar) = False, CMD2.ExecuteScalar, 0)

        'mdocno = CBNO + 1
        'CMD2.Dispose()





        'msql = "insert into [@inc_otar](DocEntry,DocNum,period,Instance,series,Handwrtten,Canceled,Object,UserSign,Transfered,Status,CreateDate,CreateTime,UpdateDate,UpdateTime,DataSource,U_DocDate,U_EffctFrom,U_EffctTo) " & vbCrLf _
        ' & "select " & mdocno & " as docentry," & mdocno & " as docnum,15 as period,0 as instance,67 as series,'N' as handwritten,'N' as Canceled,'OTAR' as [object],31 as usersign,'N' as Transfered,0 as status,CONVERT(datetime,'" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "',102) as createdate, " & vbCrLf _
        '& "1435 as createtime,CONVERT(datetime,'" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "',102) as updatedate,1257 as updatetime,'I' as datasource,CONVERT(datetime,'" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "',102) as u_docdate," & vbCrLf _
        '& " CONVERT(datetime,'" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "',102) as u_effctfrom,CONVERT(datetime,'" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "',102) as u_effctto "


        ''msql2 = "insert into [@inc_tar1] (docentry,LineId,VisOrder,Object,U_Ref4,U_Ref5,U_state,U_reptype,U_Brand,U_Rbm,U_Arcode,U_Target) " & vbCrLf _
        '' & "select " & mdocno & " as docentry,ROW_NUMBER() over(order by repname,brand) as lineid,(ROW_NUMBER() over(order by repname,brand))-1 as visorder ,'OTAR' as object, '" & Trim(flx.get_TextMatrix(j, 6)) & "' as u_ref4 ,'" & Trim(flx.get_TextMatrix(j, 7)) & "' as u_ref5,'" & Trim(flx.get_TextMatrix(j, 0)) & "' as u_state, '" & vbCrLf _
        '' & Trim(flx.get_TextMatrix(j, 1)) & "'  as u_reptype,'" & Trim(flx.get_TextMatrix(j, 2)) & "' as u_brand,'" & Trim(flx.get_TextMatrix(j, 3)) & "' as u_rbm,'" & Trim(flx.get_TextMatrix(j, 4)) & "' as u_arcode,'" & Trim(flx.get_TextMatrix(j, 5)) & "' as u_target from rrtarget3 where [MONTH] is not null and [month]='" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "MMM") & "' and [year]=" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy") & ""



        'Dim CMD As New OleDb.OleDbCommand(msql, con)

        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        ''dCMD.ExecuteNonQuery()


        'Try
        '    CMD.ExecuteNonQuery()
        '    CMD.Dispose()
        '    'CMD2.Dispose()
        'Catch ex As Exception
        '    'TRANS.Rollback()
        '    MsgBox(ex.Message)
        '    CMD.Dispose()
        '    'CMD2.Dispose()
        'End Try

        ''TRANS.Commit()
        'CMD.Dispose()
        'CMD = Nothing



        'For j = 1 To flx.Rows - 1
        '    msql2 = "insert into [@inc_tar1] (docentry,LineId,VisOrder,Object,U_Ref4,U_Ref5,U_state,U_reptype,U_Brand,U_Rbm,U_Arcode,U_Target) " & vbCrLf _
        '     & "select " & mdocno & " as docentry,ROW_NUMBER() over(order by repname,brand) as lineid,(ROW_NUMBER() over(order by repname,brand))-1 as visorder ,'OTAR' as object, '" & Trim(flx.get_TextMatrix(j, 6)) & "' as u_ref4 ,'" & Trim(flx.get_TextMatrix(j, 7)) & "' as u_ref5,'" & Trim(flx.get_TextMatrix(j, 0)) & "' as u_state, '" & vbCrLf _
        '     & Trim(flx.get_TextMatrix(j, 1)) & "'  as u_reptype,'" & Trim(flx.get_TextMatrix(j, 2)) & "' as u_brand,'" & Trim(flx.get_TextMatrix(j, 3)) & "' as u_rbm,'" & Trim(flx.get_TextMatrix(j, 4)) & "' as u_arcode,'" & Trim(flx.get_TextMatrix(j, 5)) & "' as u_target from rrtarget3 where [MONTH] is not null and [month]='" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "MMM") & "' and [year]=" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy") & ""
        '    Dim dcmd As New OleDb.OleDbCommand(msql2, con)
        '    If con.State = ConnectionState.Closed Then
        '        con.Open()
        '    End If

        '    Try
        '        dCMD.ExecuteNonQuery()
        '        dCMD.Dispose()
        '        'CMD2.Dispose()
        '    Catch ex As Exception
        '        'TRANS.Rollback()
        '        MsgBox(ex.Message)
        '        dCMD.Dispose()
        '        'CMD2.Dispose()
        '    End Try

        '    'TRANS.Commit()
        '    dCMD.Dispose()
        '    dCMD = Nothing
        'Next j



        'mdocno = 0
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
        mdocentry = Val(txtdocentry.Text)


        If mdocno > 0 And Len(Trim(CMBYR.Text)) > 0 Then
            If msel = 1 Then
                msql = "insert into [@inc_otar](DocEntry,DocNum,period,Instance,series,Handwrtten,Canceled,Object,UserSign,Transfered,Status,CreateDate,CreateTime,UpdateDate,UpdateTime,DataSource,U_DocDate,U_EffctFrom,U_EffctTo) " & vbCrLf _
                 & "select " & mdocentry & " as docentry," & mdocno & " as docnum,15 as period,0 as instance,(SELECT SERIES FROM NNM1 WHERE OBJECTCODE='OTAR' AND INDICATOR='" & CMBYR.Text & "') as series,'N' as handwritten,'N' as Canceled,'OTAR' as [object],31 as usersign,'N' as Transfered,0 as status,CONVERT(datetime,'" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "',102) as createdate, " & vbCrLf _
                & "1435 as createtime,CONVERT(datetime,'" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "',102) as updatedate,1257 as updatetime,'I' as datasource,CONVERT(datetime,'" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "',102) as u_docdate," & vbCrLf _
                & " CONVERT(datetime,'" & Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "',102) as u_effctfrom,CONVERT(datetime,'" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy-MM-dd") & "',102) as u_effctto "


                'msql2 = "insert into [@inc_tar1] (docentry,LineId,VisOrder,Object,U_Ref4,U_Ref5,U_state,U_reptype,U_Brand,U_Rbm,U_Arcode,U_Target) " & vbCrLf _
                ' & "select " & mdocno & " as docentry,ROW_NUMBER() over(order by repname,brand) as lineid,(ROW_NUMBER() over(order by repname,brand))-1 as visorder ,'OTAR' as object, '" & Trim(flx.get_TextMatrix(j, 6)) & "' as u_ref4 ,'" & Trim(flx.get_TextMatrix(j, 7)) & "' as u_ref5,'" & Trim(flx.get_TextMatrix(j, 0)) & "' as u_state, '" & vbCrLf _
                ' & Trim(flx.get_TextMatrix(j, 1)) & "'  as u_reptype,'" & Trim(flx.get_TextMatrix(j, 2)) & "' as u_brand,'" & Trim(flx.get_TextMatrix(j, 3)) & "' as u_rbm,'" & Trim(flx.get_TextMatrix(j, 4)) & "' as u_arcode,'" & Trim(flx.get_TextMatrix(j, 5)) & "' as u_target from rrtarget3 where [MONTH] is not null and [month]='" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "MMM") & "' and [year]=" & Microsoft.VisualBasic.Format(CDate(mskdateto.Text), "yyyy") & ""



                Dim CMD As New sqlcommand(msql, con)

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
                    msql2 = "insert into [@inc_tar1] (docentry,LineId,VisOrder,Object,U_Ref4,U_Ref5,U_state,U_reptype,U_Brand,U_Rbm,U_Arcode,U_Target) " & vbCrLf _
                     & " values( " & mdocentry & "," & Val(flx.get_TextMatrix(j, 8)) & "," & Val(flx.get_TextMatrix(j, 9)) & ",'OTAR','" & Trim(flx.get_TextMatrix(j, 6)) & "','" & Trim(flx.get_TextMatrix(j, 7)) & "','" & Trim(flx.get_TextMatrix(j, 0)) & "','" & Trim(flx.get_TextMatrix(j, 1)) & "'," & vbCrLf _
                     & "'" & Trim(flx.get_TextMatrix(j, 2)) & "','" & Trim(flx.get_TextMatrix(j, 3)) & "','" & Trim(flx.get_TextMatrix(j, 4)) & "','" & Trim(flx.get_TextMatrix(j, 5)) & "')"


                    Dim dcmd As New SqlCommand(msql2, con)
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



                mdocno = 0
            End If
        End If
    End Sub

    Private Sub txtno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtno.KeyPress
        If Asc(e.KeyChar) = 13 Then
            mskdatefr.Focus()

        End If
    End Sub

    Private Sub LOADYR()
        msql = "SELECT CATEGORY from OFPR GROUP BY CATEGORY"
        Dim CMDchk As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim DRchk As SqlDataReader
        DRchk = CMDchk.ExecuteReader
        cmbyr.Items.Clear()

        If DRchk.HasRows = True Then
            While DRchk.Read
                CMBYR.Items.Add(DRchk.Item("CATEGORY"))
            End While
        End If
        DRchk.Close()
        CMDchk.Dispose()



    End Sub
    Private Sub getseries()
        msql = "SELECT series from nnm1 where objectcode='OTAR' and indicator='" & CMBYR.Text & "'"
        Dim CMDchk As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim DRchk As SqlDataReader
        DRchk = CMDchk.ExecuteReader


        If DRchk.HasRows = True Then
            While DRchk.Read
                txtyno.Text = DRchk.Item("series")
            End While
        End If
        DRchk.Close()
        CMDchk.Dispose()

        'Dim CMD2 As New OleDb.OleDbCommand("select max(docentry) as no from [@inc_tar1]", con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'Dim CBNO As Int32 = IIf(IsDBNull(CMD2.ExecuteScalar) = False, CMD2.ExecuteScalar, 0)

        'txtdocentry.Text = CBNO + 1
        'CMD2.Dispose()



    End Sub
    Private Sub autono()
        Dim CMD2 As New SqlCommand("select max(docnum) as no from [@inc_otar] where series=" & Val(txtyno.Text), con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim CBNO As Int32 = IIf(IsDBNull(CMD2.ExecuteScalar) = False, CMD2.ExecuteScalar, 0)

        txtno.Text = CBNO + 1
        CMD2.Dispose()

        Dim CMD3 As New SqlCommand("select max(docentry) as no from [@inc_otar]", con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim CBNO1 As Int32 = IIf(IsDBNull(CMD3.ExecuteScalar) = False, CMD3.ExecuteScalar, 0)

        txtdocentry.Text = CBNO1 + 1
        CMD3.Dispose()
    End Sub

    Private Sub txtno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtno.LostFocus
        If msel = 2 Then
            Call delrec()
        End If
        ' Call autono()
    End Sub
    Private Sub txtno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtno.TextChanged

    End Sub

    Private Sub mskdatefr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mskdatefr.KeyPress
        If Asc(e.KeyChar) = 13 Then
            mskdateto.Focus()
        End If
    End Sub


    Private Sub cmdexit_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.ClickButtonArea
        Me.Close()
    End Sub


    Private Sub cmddel_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmddel.ClickButtonArea
        msel = 2
        txtno.Focus()

    End Sub
    Private Sub delrec()
        If MsgBox("Delete ? R U Sure!", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then

            If MsgBox("Delete ? R U Sure! No Doubt!", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
                Dim CMD As New SqlCommand("delete from [@inc_otar] where docentry=" & Val(txtno.Text), con)
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


                Dim CMD3 As New SqlCommand("delete from [@inc_tar1] where docentry=" & Val(txtno.Text), con)
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
    Private Sub cmdupdt_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdupdt.ClickButtonArea
        Call savrec()
    End Sub

    Private Sub mskdateto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mskdateto.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If flx.Row <= 1 Then
                flx.Rows = flx.Rows + 1
                flx.Row = flx.Rows - 1
                flx.Row = 1
                flx.Col = 0
                flx.Focus()


            End If
        End If
    End Sub

    Private Sub mskdateto_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskdateto.MaskInputRejected

    End Sub

    Private Sub CMBYR_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMBYR.SelectedIndexChanged
        Call getseries()
        Call autono()
    End Sub
End Class
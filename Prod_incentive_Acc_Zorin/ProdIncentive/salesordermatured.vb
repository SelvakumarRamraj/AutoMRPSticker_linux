Public Class salesordermatured
    Dim msql As String
    Dim i As Integer

    Private Sub salesordermatured_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.MdiParent = MDIForm
        Me.Height = MDIFORM1.Height - 25
        Me.Width = My.Computer.Screen.Bounds.Width
        loadcombo("ocrd", "u_areacode", cmbagent, "u_areacode")


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

    Private Sub flxhead()
        flx.Rows = 1
        flx.Cols = 9
        flx.set_ColWidth(0, 500)
        flx.set_ColWidth(1, 1100)
        flx.set_ColWidth(2, 1000)
        flx.set_ColWidth(3, 1000)
        flx.set_ColWidth(4, 1100)
        flx.set_ColWidth(5, 2000)
        flx.set_ColWidth(6, 1000)
        flx.set_ColWidth(7, 1300)
        flx.set_ColWidth(8, 1300)




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
        flx.set_TextMatrix(0, 2, "Order No")

        flx.Col = 3
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 3, "Docentry")


        flx.Col = 4
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 4, "CardCode")


        flx.Col = 5
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 5, "Cardname")


        flx.Col = 6
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 6, "Areacode")


        flx.Col = 7
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 7, "Pendqty")

        flx.Col = 8
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 8, "Pendamt")

        


        flx.set_ColAlignment(4, 2)
        flx.set_ColAlignment(5, 2)
        flx.set_ColAlignment(6, 2)
        

    End Sub

    'Private Sub loaddata()
    '    msql = "select  k.DocDate,k.DocNum,k.DocEntry,k.u_areacode, k.CardCode,k.CardName,sum(k.pendqty) pendqty, sum(k.pendamt) pendamt from " & vbCrLf _
    '   & "(select t1.DocDate,t1.DocNum,t1.DocEntry,CR.u_areacode, t1.CardCode,t1.CardName,sum(t0.OpenQty) pendqty,t0.price, CASE when sum(t0.OpenQty)>0 then sum(t0.OpenQty)*t0.Price else 0 end pendamt  from RDR1 t0 " & vbCrLf _
    '   & " left join ORDR t1 on t0.DocEntry = t1.DocEntry  " & vbCrLf _
    '   & " Left join NNM1 b with (Nolock) on b.Series = t1.Series  and Left(b.SeriesName,2) = 'SS'  " & vbCrLf _
    '   & "left join OCRD cr on cr.CardCode=t1.CardCode " & vbCrLf _
    '   & " where t1.DocStatus = 'O' and t1.CANCELED = 'N' and left(b.SeriesName,2)='SS'" & vbCrLf _
    '   & "group by t1.DocDate,t1.DocNum,t1.DocEntry,t1.CardCode,t1.CardName,t0.price,cr.U_AreaCode " & vbCrLf _
    '   & "having sum(t0.OpenQty)>0) k " & vbCrLf _
    '   & "group by  k.DocDate,k.DocNum,k.DocEntry,k.u_areacode, k.CardCode,k.CardName " & vbCrLf _
    '   & "order by k.DocDate"
    'End Sub
    Private Sub LOADDATA()
        Call flxhead()
        Cursor = Cursors.WaitCursor
        flx.Visible = False
        'Dim CMD As New SqlClient.SqlCommand("SELECT * FROM inv WHERE BNO=" & Val(txtbno.Text), con)
        'Dim CMD1 As New SqlClient.SqlCommand("SELECT * FROM Binv WHERE BNO=" & Val(txtbno.Text) & " order by bno,sno", con)

        msql = "select  k.DocDate,k.DocNum,k.DocEntry,k.u_areacode, k.CardCode,k.CardName,sum(k.pendqty) pendqty, sum(k.pendamt) pendamt from " & vbCrLf _
       & "(select t1.DocDate,t1.DocNum,t1.DocEntry,CR.u_areacode, t1.CardCode,t1.CardName,sum(t0.OpenQty) pendqty,t0.price, CASE when sum(t0.OpenQty)>0 then sum(t0.OpenQty)*t0.Price else 0 end pendamt  from RDR1 t0 " & vbCrLf _
       & " left join ORDR t1 on t0.DocEntry = t1.DocEntry  " & vbCrLf _
       & " Left join NNM1 b with (Nolock) on b.Series = t1.Series  and Left(b.SeriesName,2) = 'SS'  " & vbCrLf _
       & "left join OCRD cr on cr.CardCode=t1.CardCode " & vbCrLf _
       & " where t1.DocStatus = 'O' and t1.CANCELED = 'N' and left(b.SeriesName,2)='SS'" & vbCrLf _
       & "group by t1.DocDate,t1.DocNum,t1.DocEntry,t1.CardCode,t1.CardName,t0.price,cr.U_AreaCode " & vbCrLf _
       & "having sum(t0.OpenQty)>0) k " & vbCrLf _
       & "where k.docdate<='" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"
        If Len(Trim(cmbagent.Text)) > 0 Then
            msql = msql & " and k.u_areacode='" & Trim(cmbagent.Text) & "'"
        End If
        msql = msql & " group by  k.DocDate,k.DocNum,k.DocEntry,k.u_areacode, k.CardCode,k.CardName " & vbCrLf _
       & "order by k.DocDate"


        Dim CMD As New OleDb.OleDbCommand(msql, con)
        'Dim CMD1 As New OleDb.OleDbCommand("SELECT * FROM inward1 WHERE docnum=" & Val(txtno.Text) & " order by docnum", con)


        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim DR As SqlClient.SqlDataReader
        'Dim DR1 As SqlClient.SqlDataReader

        Dim DR As OleDb.OleDbDataReader
        '--Dim DR1 As OleDb.OleDbDataReader

        DR = CMD.ExecuteReader


        If DR.HasRows = True Then
            With flx
                While DR.Read
                    .Rows = .Rows + 1
                    .Row = .Rows - 1




                    .set_TextMatrix(.Row, 1, DR.Item("docdate"))
                    .set_TextMatrix(.Row, 2, DR.Item("docnum"))
                    .set_TextMatrix(.Row, 3, DR.Item("docentry"))
                    .set_TextMatrix(.Row, 4, DR.Item("cardcode") & vbNullString)
                    .set_TextMatrix(.Row, 5, DR.Item("cardname") & vbNullString)
                    .set_TextMatrix(.Row, 6, DR.Item("u_areacode") & vbNullString)
                    .set_TextMatrix(.Row, 7, DR.Item("pendqty"))
                    .set_TextMatrix(.Row, 8, DR.Item("pendamt"))


                End While
            End With

        End If
        DR.Close()
        CMD.Dispose()
        flx.Visible = True
        Cursor = Cursors.Default


    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdupdt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdupdt.Click
        Cursor = Cursors.WaitCursor
        With flx
            For i = 1 To flx.Rows - 1
                If Len(Trim(flx.get_TextMatrix(i, 0))) > 0 Then
                    Dim CMD As New OleDb.OleDbCommand(" update ordr set docstatus='C' where docentry=" & Val(flx.get_TextMatrix(i, 3)), con)
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If
                    CMD.ExecuteNonQuery()
                End If

            Next i
        End With
        Cursor = Cursors.Default
    End Sub

    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.Click
        Call LOADDATA()
    End Sub

    Private Sub cmdxl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdxl.Click
        excelexport(flx)
    End Sub

    Private Sub cmdsel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsel.Click
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

    Private Sub cmbagent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbagent.SelectedIndexChanged

    End Sub
End Class
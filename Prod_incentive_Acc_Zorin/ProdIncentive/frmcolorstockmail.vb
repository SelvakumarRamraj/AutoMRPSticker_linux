Imports System
Imports System.IO
Imports System.Net.Mail
Imports Microsoft.VisualBasic
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
Imports System.Net

Public Class frmcolorstockmail
    Dim msql, msql2 As String
    Dim i, j As Integer
    Dim mluser As String
    Dim mlpwd As String
    Dim mlto As String
    Dim mlcc As String
    Dim mlport As String
    Dim mlsmtp As String
    Dim mlfrom As String
    Dim mlsub As String
    Dim mlbody As String
    Dim mattach As String
    'Dim atachment As String
    Dim atachment As System.Net.Mail.Attachment


    Private Shared Function customCertValidation(ByVal sender As Object, _
                                                 ByVal cert As X509Certificate, _
                                                 ByVal chain As X509Chain, _
                                                 ByVal errors As SslPolicyErrors) As Boolean

        Return True

    End Function
    Private Sub frmcolorstockmail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.MdiParent = MDIForm
        Me.Height = MDIFORM1.Height - 25
        Me.Width = My.Computer.Screen.Bounds.Width

        mluser = "selvakumar@atithya.net"
        mlpwd = "Selvauma@536"
        mlsmtp = "mail.atithya.net"
        '25-local,587-outside
        mlport = 587
        mlfrom = "selvakumar@atithya.net"
        mlcc = ""
        mlto = "selvakumar@atithya.net"
        mlbody = "test"

        Dim ldir, lmdir As String
        ldir = System.AppDomain.CurrentDomain.BaseDirectory()
        lmdir = Trim(ldir) & "ColorStock.xls"
        mattach = lmdir
        mlto = "selvakumar@atithya.net"
        mlbody = "test"
        Call loadparty()
    End Sub
    Private Sub loadparty()
        Call flxphead()
        msql2 = "select cardname,e_mail from ocrd where qrygroup9='Y'"
        Dim CMD As New OleDb.OleDbCommand(msql2, con)
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
            With flxp
                While DR.Read
                    .Rows = .Rows + 1
                    .Row = .Rows - 1
                    .set_TextMatrix(.Row, 1, DR.Item("cardname") & vbNullString)
                    .set_TextMatrix(.Row, 2, DR.Item("E_mail") & vbNullString)
                End While
            End With
        End If
        CMD.Dispose()
    End Sub
    Private Sub loaddata()
        'msql = "select k.U_BrandGroup,k.u_style,k.u_size,k.btno,k.BatchNum,sum(k.batchqty) batchqty,sum(k.stkqty) stkqty,sum(k.stockvalue) stkvalue from " & vbCrLf _
        ' & "(Select c.U_BrandGroup, b.itemcode,c.itemname,c.u_style,c.u_size , case when PATINDEX('%[0-9]%', bb.BatchNum)>0  then SUBSTRING(bb.batchnum, PATINDEX('%[0-9]%', bb.batchnum), LEN(bb.batchnum)) else 0 end btno,  bb.BatchNum,bb.batchqty, sum(b.inqty-b.outqty) as stkqty, sum(b.transvalue) as stockvalue, b.warehouse,1 as nno,c.u_subgroup,c.mngmethod from oinm b with (nolock) " & vbCrLf _
        ' & "left join oitm c with (nolock)  on c.itemcode=b.itemcode" & vbCrLf _
        '& "left join (select  b.ItemCode,b.ItemName, b.BatchNum,SUM(b.Quantity) batchqty,b.WhsCode from OIBT b  with (nolock) where b.WhsCode='SALGOODS'  group by b.ItemCode,b.ItemName,b.BatchNum,b.whscode having SUM(b.Quantity)>0 ) bb on bb.ItemCode=b.itemcode and bb.WhsCode=b.Warehouse" & vbCrLf _
        '& "where  b.docdate<='2017-02-20' and (c.MngMethod='A' and c.ManBtchNum='Y') and b.Warehouse='SALGOODS' " & vbCrLf _
        '& "group by c.U_BrandGroup, b.itemcode,c.itemname,c.u_style,u_size,b.warehouse,c.u_subgroup,c.mngmethod,c.ManBtchNum,bb.BatchNum,bb.batchqty) k " & vbCrLf _
        '& "group by k.U_BrandGroup,k.u_style,k.u_size,k.btno,k.BatchNum " & vbCrLf _
        '& "order by  k.u_brandgroup,k.U_Size,  convert(float,isnull(k.btno,0)),k.U_Style,k.batchnum"
        Call flxhead()
        Cursor = Cursors.WaitCursor
        flx.Visible = False

        msql = "select l.U_BrandGroup,l.u_style,l.btno,l.BatchNum,SUM(l.s34) '34'," & vbCrLf _
        & " SUM(l.s36) '36',SUM(l.s38) '38',SUM(l.s40) '40',SUM(l.s42) '42',SUM(l.s44) '44'," & vbCrLf _
        & "SUM(l.s46) '46',SUM(l.s48) '48',SUM(l.s50) '50',SUM(l.s52) '52', " & vbCrLf _
        & "isnull(SUM(l.s34+l.s36+l.s38+l.s40+l.s42+l.s44+l.s46+l.s48+l.s50+l.s52),0) total from " & vbCrLf _
        & " (select k.U_BrandGroup,k.u_style,k.btno,k.BatchNum," & vbCrLf _
        & " CASE when k.U_Size='34' then sum(k.batchqty) else 0 end s34," & vbCrLf _
        & "CASE when k.U_Size='36' then sum(k.batchqty) else 0 end s36," & vbCrLf _
        & "CASE when k.U_Size='38' then sum(k.batchqty) else 0 end s38," & vbCrLf _
        & "CASE when k.U_Size='40' then sum(k.batchqty) else 0 end s40," & vbCrLf _
        & "CASE when k.U_Size='42' then sum(k.batchqty) else 0 end s42," & vbCrLf _
        & "CASE when k.U_Size='44' then sum(k.batchqty) else 0 end s44," & vbCrLf _
        & "CASE when k.U_Size='46' then sum(k.batchqty) else 0 end s46," & vbCrLf _
        & "CASE when k.U_Size='48' then sum(k.batchqty) else 0 end s48," & vbCrLf _
        & "CASE when k.U_Size='50' then sum(k.batchqty) else 0 end s50," & vbCrLf _
       & "CASE when k.U_Size='52' then sum(k.batchqty) else 0 end s52    from " & vbCrLf _
       & "(Select c.U_BrandGroup, b.itemcode,c.itemname,c.u_style,c.u_size , case when PATINDEX('%[0-9]%', bb.BatchNum)>0  then SUBSTRING(bb.batchnum, PATINDEX('%[0-9]%', bb.batchnum), LEN(bb.batchnum)) else 0 end btno,  bb.BatchNum,bb.batchqty, sum(b.inqty-b.outqty) as stkqty, sum(b.transvalue) as stockvalue, b.warehouse,1 as nno,c.u_subgroup,c.mngmethod from oinm b with (nolock) " & vbCrLf _
       & "left join oitm c with (nolock)  on c.itemcode=b.itemcode" & vbCrLf _
       & "left join (select  b.ItemCode,b.ItemName, b.BatchNum,SUM(b.Quantity) batchqty,b.WhsCode from OIBT b  with (nolock) where b.WhsCode='SALGOODS'  group by b.ItemCode,b.ItemName,b.BatchNum,b.whscode having SUM(b.Quantity)>0 ) bb on bb.ItemCode=b.itemcode and bb.WhsCode=b.Warehouse" & vbCrLf _
       & "where  (c.MngMethod='A' and c.ManBtchNum='Y') and b.Warehouse='SALGOODS'" & vbCrLf _
       & "group by c.U_BrandGroup, b.itemcode,c.itemname,c.u_style,u_size,b.warehouse,c.u_subgroup,c.mngmethod,c.ManBtchNum,bb.BatchNum,bb.batchqty) k" & vbCrLf _
       & "group by k.U_BrandGroup,k.u_style,k.btno,k.BatchNum,k.u_size) l" & vbCrLf _
       & "group by l.U_BrandGroup,l.u_style,l.btno,l.BatchNum" & vbCrLf _
       & "order by  l.u_brandgroup, convert(float,isnull(l.btno,0)),l.U_Style,l.batchnum"

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




                    .set_TextMatrix(.Row, 1, DR.Item("u_brandgroup") & vbNullString)
                    .set_TextMatrix(.Row, 2, DR.Item("u_style") & vbNullString)
                    .set_TextMatrix(.Row, 3, DR.Item("btno") & vbNullString)
                    .set_TextMatrix(.Row, 4, DR.Item("batchnum") & vbNullString)
                    .set_TextMatrix(.Row, 5, Format(DR.Item("34"), "#####0"))
                    .set_TextMatrix(.Row, 6, Format(DR.Item("36"), "#####0"))
                    .set_TextMatrix(.Row, 7, Format(DR.Item("38"), "#####0"))
                    .set_TextMatrix(.Row, 8, Format(DR.Item("40"), "#####0"))
                    .set_TextMatrix(.Row, 9, Format(DR.Item("42"), "#####0"))
                    .set_TextMatrix(.Row, 10, Format(DR.Item("44"), "#####0"))
                    .set_TextMatrix(.Row, 11, Format(DR.Item("46"), "#####0"))
                    .set_TextMatrix(.Row, 12, Format(DR.Item("48"), "#####0"))
                    .set_TextMatrix(.Row, 13, Format(DR.Item("50"), "#####0"))
                    .set_TextMatrix(.Row, 14, Format(DR.Item("52"), "#####0"))
                    .set_TextMatrix(.Row, 15, Format(DR.Item("Total"), "#####0"))


                End While
            End With

        End If
        DR.Close()
        CMD.Dispose()
        flx.Visible = True
        Cursor = Cursors.Default


    End Sub


    Private Sub flxhead()
        flx.Rows = 1
        flx.Cols = 16
        flx.set_ColWidth(0, 500)
        flx.set_ColWidth(1, 1500)
        flx.set_ColWidth(2, 1000)
        flx.set_ColWidth(3, 1000)
        flx.set_ColWidth(4, 1000)
        flx.set_ColWidth(5, 900)
        flx.set_ColWidth(6, 900)
        flx.set_ColWidth(7, 900)
        flx.set_ColWidth(8, 900)
        flx.set_ColWidth(9, 900)
        flx.set_ColWidth(10, 900)
        flx.set_ColWidth(11, 900)
        flx.set_ColWidth(12, 900)
        flx.set_ColWidth(13, 900)
        flx.set_ColWidth(14, 900)
        flx.set_ColWidth(15, 1100)





        flx.Row = 0
        flx.Col = 0
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 0, "Sel")

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
        flx.set_TextMatrix(0, 3, "BTNO")


        flx.Col = 4
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 4, "BatchNo")


        flx.Col = 5
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 5, "34")


        flx.Col = 6
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 6, "36")


        flx.Col = 7
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 7, "38")

        flx.Col = 8
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 8, "40")

        flx.Col = 9
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 9, "42")

        flx.Col = 10
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 10, "44")

        flx.Col = 11
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 11, "46")

        flx.Col = 12
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 12, "48")

        flx.Col = 13
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 13, "50")

        flx.Col = 14
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 14, "52")

        flx.Col = 15
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 15, "Total")



        flx.set_ColAlignment(1, 2)
        flx.set_ColAlignment(2, 2)
        flx.set_ColAlignment(3, 2)
        flx.set_ColAlignment(4, 2)


    End Sub

    Private Sub flxphead()
        flxp.Rows = 1
        flxp.Cols = 3
        flxp.set_ColWidth(0, 500)
        flxp.set_ColWidth(1, 2500)
        flxp.set_ColWidth(2, 2500)
        




        flxp.Row = 0
        flxp.Col = 0
        flxp.CellAlignment = 3
        flxp.CellFontBold = True
        flxp.set_TextMatrix(0, 0, "Sel")

        flxp.Col = 1
        flxp.CellAlignment = 3
        flxp.CellFontBold = True
        flxp.set_TextMatrix(0, 1, "Party Name")

        flxp.Col = 2
        flxp.CellAlignment = 3
        flxp.CellFontBold = True
        flxp.set_TextMatrix(0, 2, "E-Mail")


        flxp.set_ColAlignment(1, 2)
        flxp.set_ColAlignment(2, 2)
        '       flx.set_ColAlignment(3, 2)
        '        flx.set_ColAlignment(4, 2)


    End Sub

    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.Click
        Call loaddata()
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
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

    Private Sub cmdxl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdxl.Click
        colostockexcel(flx, "R and R Textile", "Colorwise Stock Report")
    End Sub

    Private Sub cmdmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdmail.Click
        Cursor = Cursors.WaitCursor
        For j = 1 To flxp.Rows - 1

            If Len(Trim(flxp.get_TextMatrix(j, 0))) > 0 Then
                mluser = "selvakumar@atithya.net"
                mlpwd = "Selvauma@536"
                mlsmtp = "mail.atithya.net"
                '25-local,587-outside
                mlport = 587
                mlfrom = "selvakumar@atithya.net"
                mlcc = ""
                'mlto = "selvakumar@atithya.net"
                mlbody = "test"

                Dim ldir, lmdir As String
                ldir = System.AppDomain.CurrentDomain.BaseDirectory()
                lmdir = Trim(ldir) & "ColorStock.xls"
                mattach = lmdir
                mlto = Trim(flxp.get_TextMatrix(j, 2))
                mlbody = "test"



                Dim Mail As New MailMessage

                'If mattach <> "" Then
                '    If txtAttachements.Text.Contains(",") Then
                '        Dim attachList As String()
                '        attachList = txtAttachements.Text.Split(",")
                '        For Each attccTo As String In attachList
                '            If Len(Trim(attccTo)) > 0 Then
                '                attachToMsg = New Attachment(attccTo)
                '                Mail.Attachments.Add(attachToMsg)
                '            End If
                '            'mail.CC.Add(attccTo)
                '        Next
                '    Else
                '        'mail.CC.Add(txtcc.Text)
                '    End If
                'End If

                atachment = New System.Net.Mail.Attachment(mattach)
                Mail.Attachments.Add(atachment)
                Mail.Subject = mlsub
                Mail.To.Add(mlto)
                'Mail.CC.Add(mlcc)
                'Mail.From = New MailAddress(mlfrom)
                'MsgBox(mluser)
                'MsgBox(mluser)

                Mail.From = New MailAddress(mlfrom, mluser)
                Mail.Body = mlbody

                ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf customCertValidation)

                Dim SMTP As New SmtpClient(mlsmtp) '--smtp
                SMTP.EnableSsl = True
                SMTP.Credentials = New System.Net.NetworkCredential(mluser, mlpwd)
                SMTP.Port = mlport
                SMTP.Send(Mail)
                SMTP.Timeout = 21000
            End If
            ' MsgBox("Mail Send")
            'on.Close()
            'Me.Close()
        Next j
        Cursor = Cursors.Default

    End Sub

    Private Sub flxp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxp.Enter

    End Sub

    Private Sub flxp_KeyPressEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyPressEvent) Handles flxp.KeyPressEvent
        If e.keyAscii = 32 Then
            flxp.Row = flxp.Row
            If flxp.Row > 0 Then
                If Len(Trim(flxp.get_TextMatrix(flxp.Row, 0))) = 0 Then
                    flxp.Col = 0
                    flxp.CellFontName = "WinGdings"
                    flxp.CellFontSize = 14
                    flxp.CellAlignment = 4
                    flxp.CellFontBold = True
                    flxp.CellForeColor = Color.Red
                    flxp.Text = Chr(252)
                Else
                    flxp.Col = 0
                    flxp.Text = ""
                End If
            End If
        End If
    End Sub
End Class
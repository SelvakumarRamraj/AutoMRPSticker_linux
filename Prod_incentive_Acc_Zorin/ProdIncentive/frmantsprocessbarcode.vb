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

Public Class frmantsprocessbarcode
    Dim msql As String
    Dim i As Int32
    Dim mtxtbno As New Int32
    Dim msql2, msql4, msql3, msql5 As String
    Dim mkstr As String
    Dim mkstr2 As String
    Dim mkrow, mkcol, mksno, mksno2 As Int32
    Dim mfil, mfildet, mdir As String
    Dim mshmtr() As String
    Dim errcod As String

    Private Sub frmantsprocessbarcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        CLEAR(Me)
        ' view1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        'Call loadcode()
        loadcombo("ofpr", "code", cmbyr, "code")
        cmbyr.Text = mpostperiod
    End Sub

    Private Sub flxhead()
        flx.Rows = 1
        flx.Cols = 13
        flx.set_ColWidth(0, 600)
        flx.set_ColWidth(1, 1500)
        flx.set_ColWidth(2, 1500)
        flx.set_ColWidth(3, 900)
        flx.set_ColWidth(4, 800)
        flx.set_ColWidth(5, 1300)
        flx.set_ColWidth(6, 1200)
        flx.set_ColWidth(7, 1000)
        flx.set_ColWidth(8, 1200)
        flx.set_ColWidth(9, 1000)
        flx.set_ColWidth(10, 1000)
        flx.set_ColWidth(11, 1000)
        flx.set_ColWidth(12, 1000)
        'flx.set_ColWidth(10, 1)
        
        flx.Row = 0
        flx.Col = 0
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 0, "Sel")

        flx.Col = 1
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 1, "Itemcode")

        flx.Col = 2
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 2, "Item Name")

        flx.Col = 3
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 3, "Style")

        flx.Col = 4
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 4, "Size")

        flx.Col = 5
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 5, "CutNo")

        flx.Col = 6
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 6, "WoNo")

        flx.Col = 7
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 7, "OperCode")

        flx.Col = 8
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 8, "Accpt.Qty")

        flx.Col = 9
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 9, "Rew.Qty")

        flx.Col = 10
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 10, "Mon/Yr")

        flx.Col = 11
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 11, "Brand")

        flx.Col = 12
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 11, "StartNo")

        '10 width
        '11 length
        '12 u_itmgrp
        '13 u_size
        '14 linenum
        '15 dhoti
        '16 Shirt
        '17 Suit
        '18 towel
        '19 Invntitem
        '20 u_subgroup6 -Item Name
        '21 mtrstot

        flx.set_ColAlignment(0, 2)
        flx.set_ColAlignment(1, 2)
        flx.set_ColAlignment(2, 2)
        flx.set_ColAlignment(3, 2)
        flx.set_ColAlignment(4, 2)
        flx.set_ColAlignment(5, 2)
        flx.set_ColAlignment(7, 2)


    End Sub

    Private Sub loadno()
        msql = "select c.DocNum,c.Period, b.U_ItemCode,d.ItemName,D.U_BrandGroup,d.U_Style,d.U_Size,b.U_CutNo,b.U_WONo,U_AccpQty,U_RewQty," & vbCrLf _
             & " RIGHT(('0'+ convert(nvarchar(2),MONTH(c.u_docdate))),2)+'/'+RIGHT(convert(nvarchar(4),year(c.u_docdate)),4) as mnyr,b.u_opercode from [@inm_wip1] b " & vbCrLf _
             & " left join [@INM_OWIP] c on c.DocEntry=b.DocEntry " & vbCrLf _
             & " left join OITM d on d.ItemCode=b.U_ItemCode" & vbCrLf _
             & " left join OFPR pr on pr.AbsEntry=c.Period " & vbCrLf _
             & " where c.DocNum=" & Val(txtno.Text) & " and pr.Code= '" & cmbyr.Text & "'"

        'msql = "select docentry,comp,group1 as type,group2 as prntype,printon ,stickercol,labrow,labcol,printer from barhead where active=1"
        Dim CMD As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()
        flx.Clear()
        Call flxhead()
        Try
            ''Dim DR As SqlDataReader
            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                With flx
                    While DR.Read
                        .Rows = .Rows + 1
                        .Row = .Rows - 1
                        .set_TextMatrix(.Row, 1, DR.Item("u_itemcode"))
                        .set_TextMatrix(.Row, 2, DR.Item("itemname") & vbNullString)
                        .set_TextMatrix(.Row, 3, DR.Item("u_style") & vbNullString)
                        .set_TextMatrix(.Row, 4, DR.Item("u_size") & vbNullString)
                        .set_TextMatrix(.Row, 5, DR.Item("u_cutno"))
                        .set_TextMatrix(.Row, 6, DR.Item("u_wono"))
                        .set_TextMatrix(.Row, 7, DR.Item("u_opercode") & vbNullString)
                        .set_TextMatrix(.Row, 8, DR.Item("u_accpqty"))
                        .set_TextMatrix(.Row, 9, DR.Item("u_rewqty"))
                        .set_TextMatrix(.Row, 10, DR.Item("mnyr"))
                        .set_TextMatrix(.Row, 11, DR.Item("u_brandgroup") & vbNullString)
                        .set_TextMatrix(.Row, 12, 0)

                    End While
                End With
            End If
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            flx.Clear()
            Call flxhead()
        End Try

        CMD.Dispose()
    End Sub

    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.Click
        Call loadno()
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        CLEAR(Me)
        flx.Clear()
        Call flxhead()
        cmbyr.Text = mpostperiod
    End Sub

    Private Sub barprint()
        Dim i, k As Integer
        Dim sno, mksno, mkrow, mkcol As Integer
        'Dim mktt, mkt2 As Boolean



        mtxtbno = 0

        mksno = 0
        mkrow = 0
        mkcol = 0

        sno = 1

        '*** tmep
        Dim dir As String


        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "pbarcode.txt"


        If chkprndir.Checked = True Then
            FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        Else
            FileOpen(1, mdir, OpenMode.Output)
        End If
        PrintLine(1, TAB(0), "^XA")
        PrintLine(1, TAB(0), "^PRC")
        PrintLine(1, TAB(0), "^LH0,0^FS")
        PrintLine(1, TAB(0), "^LL160")
        PrintLine(1, TAB(0), "^MD0")
        PrintLine(1, TAB(0), "^MNY")
        PrintLine(1, TAB(0), "^LH0,0^FS")

        For i = 1 To flx.Rows - 1
            'PrintLine(1, TAB(0), "^XA")
            'PrintLine(1, TAB(0), "^PRC")
            'PrintLine(1, TAB(0), "^LH0,0^FS")
            'PrintLine(1, TAB(0), "^LL160")
            'PrintLine(1, TAB(0), "^MD0")
            'PrintLine(1, TAB(0), "^MNY")
            'PrintLine(1, TAB(0), "^LH0,0^FS")

            If Len(Trim(flx.get_TextMatrix(i, 0))) > 0 Then
                For k = 1 To (Val(flx.get_TextMatrix(i, 8)))


                    PrintLine(1, TAB(0), "^FO" & Trim(Str(15 + Val(mtxtbno))) & ",25^A0N,22,25^CI13^FR^FD" & Mid(Trim(flx.get_TextMatrix(i, 11)), 1, 30) & "^FS")
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(15 + Val(mtxtbno))) & ",55^A0N,22,25^CI13^FR^FD" & Trim(Mid(Trim(flx.get_TextMatrix(i, 11)), 31, 15)) & " - " & Trim(flx.get_TextMatrix(i, 3)) & "-" & Trim(flx.get_TextMatrix(i, 4)) & "^FS")
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(150 + Val(mtxtbno))) & ",140^A0N,21,21^CI13^FR^FD" & Trim(flx.get_TextMatrix(i, 10)) & "^FS")
                    'PrintLine(1, TAB(0), "^FO" & Trim(Str(17 + Val(mtxtbno))) & ",43^A0N,46,41^CI13^FR^FDRs.20.00/-^FS")
                    PrintLine(1, TAB(0), "^BY2,3.0^FO" & Trim(Str(30 + Val(mtxtbno))) & ",80^BCN,45,N,Y,N^FR^FD>:" & Trim(flx.get_TextMatrix(i, 5)) & "-" & Trim(Str(k + Val(flx.get_TextMatrix(i, 12)))) & "^FS")
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + Val(mtxtbno))) & ",140^A0N,21,21^CI13^FR^FD" & Trim(flx.get_TextMatrix(i, 5)) & "-" & Trim(Str(k + Val(flx.get_TextMatrix(i, 12)))) & "^FS")

                    mtxtbno = mtxtbno + 400

                    If sno = 2 Then
                        'sno = sno + 1
                        'PrintLine(1, TAB(0), "P1")
                        PrintLine(1, TAB(0), "^PQ1,0,0,N")
                        PrintLine(1, TAB(0), "^XZ")

                        'If sno > 2 Then
                        PrintLine(1, TAB(0), "^XA")
                        PrintLine(1, TAB(0), "^PRC")
                        PrintLine(1, TAB(0), "^LH0,0^FS")
                        PrintLine(1, TAB(0), "^LL160")
                        PrintLine(1, TAB(0), "^MD0")
                        PrintLine(1, TAB(0), "^MNY")
                        PrintLine(1, TAB(0), "^LH0,0^FS")
                        sno = 0
                        mtxtbno = 0
                    End If
                    sno = sno + 1
                Next k

            End If
        Next i
        PrintLine(1, TAB(0), "^PQ1,0,0,N")
        PrintLine(1, TAB(0), "^XZ")
        FileClose(1)

        If mos = "WIN" Then
            If chkprndir.Checked = False Then
                If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                    Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))

                End If
            End If
        Else
            Dim printer As String = tscprinter1
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "pbarcode.txt"
            Dim success As Boolean = PrintTscRaw(printer, filePathname)

        End If

    End Sub

    Private Sub cmdprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdprint.Click
        Call barprint()
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
        If flx.Col = 8 Or flx.Col = 12 Then
            editflx(flx, e.keyAscii, cmddisp)
        End If
    End Sub

    Private Sub cmbyr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbyr.SelectedIndexChanged

    End Sub

    Private Sub txtno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtno.TextChanged

    End Sub
End Class
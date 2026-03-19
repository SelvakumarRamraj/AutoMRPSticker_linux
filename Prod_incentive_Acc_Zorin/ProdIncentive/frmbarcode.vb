Imports System.IO
Imports System.Math
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.VbStrConv
'Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6
Public Class frmbarcode

    Dim mtxtbno As New Int32
    Dim msql As String
    Dim msql2, msql4, msql3, msql5 As String
    Dim mkstr As String
    Dim mkstr2 As String
    Dim mkrow, mkcol, mksno, mksno2, i As Int32
    Dim mfil, mfildet, mdir As String
    Dim mshmtr() As String
    Dim errcod As String




    Private Sub frmbarcode_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F12 Then
            If flxc.Visible = False Then flxc.Visible = True
        End If
    End Sub

    Private Sub frmbarcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        Call flxhead()
        'Call flxchead()
        Call loadhead()
        Call loadsetitem()
        Call loadsetcont()
        cmbyr.Items.Add("2014")
        cmbyr.Items.Add("2015")
        cmbyr.Items.Add("2016")
        cmbyr.Items.Add("2017")
        cmbyr.Items.Add("2018")
        cmbyr.Items.Add("2019")
        cmbyr.Items.Add("2020")

        cmbmont.Items.Add("1")
        cmbmont.Items.Add("2")
        cmbmont.Items.Add("3")
        cmbmont.Items.Add("4")
        cmbmont.Items.Add("5")
        cmbmont.Items.Add("6")
        cmbmont.Items.Add("7")
        cmbmont.Items.Add("8")
        cmbmont.Items.Add("9")
        cmbmont.Items.Add("10")
        cmbmont.Items.Add("11")
        cmbmont.Items.Add("12")

        loadcomboyr("ofpr", "code", cmbyr)
        cmbyear.Text = mpostperiod
        'yg()
        'If mProdMktbarcode = "1" Then
        chkmrp.Checked = True
        cmbyr.Text = mpostperiod
        cmbmont.Text = Microsoft.VisualBasic.Format(Now(), "MM")

    End Sub
    Private Sub loadsetitem()
        Dim chktab As Int16
        msql = "declare @sqlstr varchar(max)" & vbCrLf _
             & "DECLARE @subjectlist VARCHAR(MAX) " & vbCrLf _
             & "DECLARE @subjtot VARCHAR(MAX) " & vbCrLf _
             & "declare @subtot varchar(max)" & vbCrLf _
             & " select @SUBJtot=COALESCE(@SUBJtot +',','')+RTRIM(quotename(k.ugrp)), " & vbCrLf _
             & "@SUBtot=COALESCE(@SUBtot +'+isnull(','isnull(')+RTRIM(quotename(k.ugrp))+',0)' " & vbCrLf _
             & " From(select  case when charindex('DHOTI',t1.U_ItemGrp)>0 then 'DHOTI' " & vbCrLf _
             & " when charindex('SHIRT',t1.U_ItemGrp)>0 then 'SHIRTING'" & vbCrLf _
             & " when charindex('SUIT',t1.U_ItemGrp)>0 then 'SUITING'" & vbCrLf _
             & " when charindex('TOWEL',t1.U_ItemGrp)>0 then 'TOWEL' end as ugrp " & vbCrLf _
             & "from ITT1 as t0 " & vbCrLf _
             & "left join [@INS_OPLM] as t1 on t1.U_ItemCode=t0.code " & vbCrLf _
             & "left join [@INCM_SZE1] as tz on tz.U_Name=t1.U_Size" & vbCrLf _
             & "group by case when charindex('DHOTI',t1.U_ItemGrp)>0 then 'DHOTI' " & vbCrLf _
             & " when charindex('SHIRT',t1.U_ItemGrp)>0 then 'SHIRTING' " & vbCrLf _
             & " when charindex('SUIT',t1.U_ItemGrp)>0 then 'SUITING' " & vbCrLf _
             & " when charindex('TOWEL',t1.U_ItemGrp)>0 then 'TOWEL' end ) as k order by k.UGrp" & vbCrLf

        msql = msql & "set @sqlstr='insert into barcodebom select father,'+ @subjtot+ '  from ( " & vbCrLf _
                 & "select t0.father,case when charindex(''DHOTI'',t1.U_ItemGrp)>0 then ''DHOTI'' " & vbCrLf _
                 & "when charindex(''SHIRT'',t1.U_ItemGrp)>0 then ''SHIRTING''" & vbCrLf _
                 & "when charindex(''SUIT'',t1.U_ItemGrp)>0 then ''SUITING''" & vbCrLf _
                 & "when charindex(''TOWEL'',t1.U_ItemGrp)>0 then ''TOWEL'' end as ugrp," & vbCrLf _
                 & "RTRIM(tz.u_width)+''cmX''+rtrim(lTRIM(cast(cast(tz.u_length as real)*100 as char(10)))) +''cm'' as mksize " & vbCrLf _
                 & " from ITT1 as t0 " & vbCrLf _
                 & "left join [@INS_OPLM] as t1 on t1.U_ItemCode=t0.code " & vbCrLf _
                 & "left join [@INCM_SZE1] as tz on tz.U_Name=t1.U_Size ) p " & vbCrLf _
                 & "pivot(max(mksize) for ugrp IN('+@subjtot+')) AS p1'  " & vbCrLf _
                 & "execute(@sqlstr)"

        msql = "IF OBJECT_ID (N'barcodebom', N'U') IS NOT NULL SELECT 1 AS res ELSE SELECT 0 AS res"
        Dim CMDchk As New OleDb.OleDbCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim DRchk As OleDb.OleDbDataReader
        DRchk = CMDchk.ExecuteReader
        If DRchk.HasRows = True Then
            While DRchk.Read
                chktab = DRchk.Item("res")
            End While
        End If
        DRchk.Close()
        CMDchk.Dispose()

        If chktab = 0 Then
            msql2 = "CREATE TABLE [dbo].[barcodebom](" & vbCrLf _
               & "[code] [nchar](20) NULL," & vbCrLf _
               & "[dhoti] [nchar](30) NULL," & vbCrLf _
               & "[shirting] [nchar](30) NULL," & vbCrLf _
               & "[suiting] [nchar](30) NULL," & vbCrLf _
               & "[towel] [nchar](30) NULL," & vbCrLf _
               & "[pcs] [real] NULL" & vbCrLf _
               & " ) ON [PRIMARY]"
            Dim cmd2 As New OleDb.OleDbCommand(msql2, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd2.ExecuteNonQuery()
            cmd2.Dispose()

            Dim cmd3 As New OleDb.OleDbCommand(msql, con)

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            cmd3.ExecuteNonQuery()
            cmd3.Dispose()
        End If

    End Sub
    Private Sub loadsetcont()
        Dim chktab As Int16
        msql = "declare @sqlstr varchar(max)" & vbCrLf _
             & "DECLARE @subjectlist VARCHAR(MAX) " & vbCrLf _
             & "DECLARE @subjtot VARCHAR(MAX) " & vbCrLf _
             & "declare @subtot varchar(max)" & vbCrLf _
             & " select @SUBJtot=COALESCE(@SUBJtot +',','')+RTRIM(quotename(k.ugrp)), " & vbCrLf _
             & "@SUBtot=COALESCE(@SUBtot +'+isnull(','isnull(')+RTRIM(quotename(k.ugrp))+',0)' " & vbCrLf _
             & " From(select  case when charindex('DHOTI',t1.U_ItemGrp)>0 then 'DHOTI' " & vbCrLf _
             & " when charindex('SHIRT',t1.U_ItemGrp)>0 then 'SHIRTING'" & vbCrLf _
             & " when charindex('SUIT',t1.U_ItemGrp)>0 then 'SUITING'" & vbCrLf _
             & " when charindex('TOWEL',t1.U_ItemGrp)>0 then 'TOWEL' end as ugrp " & vbCrLf _
             & "from ITT1 as t0 " & vbCrLf _
             & "left join [@INS_OPLM] as t1 on t1.U_ItemCode=t0.code " & vbCrLf _
             & "left join [@INCM_SZE1] as tz on tz.U_Name=t1.U_Size" & vbCrLf _
             & "group by case when charindex('DHOTI',t1.U_ItemGrp)>0 then 'DHOTI' " & vbCrLf _
             & " when charindex('SHIRT',t1.U_ItemGrp)>0 then 'SHIRTING' " & vbCrLf _
             & " when charindex('SUIT',t1.U_ItemGrp)>0 then 'SUITING' " & vbCrLf _
             & " when charindex('TOWEL',t1.U_ItemGrp)>0 then 'TOWEL' end ) as k order by k.UGrp" & vbCrLf

        msql = msql & "set @sqlstr='insert into contentbom(code,dhoti,shirting,suiting,towel) select father,'+ @subjtot+ '  from ( " & vbCrLf _
                 & "select t0.father,case when charindex(''DHOTI'',t1.U_ItemGrp)>0 then ''DHOTI'' " & vbCrLf _
                 & "when charindex(''SHIRT'',t1.U_ItemGrp)>0 then ''SHIRTING''" & vbCrLf _
                 & "when charindex(''SUIT'',t1.U_ItemGrp)>0 then ''SUITING''" & vbCrLf _
                 & "when charindex(''TOWEL'',t1.U_ItemGrp)>0 then ''TOWEL'' end as ugrp," & vbCrLf _
                 & "RTRIM(tz.u_width)+''cmX''+rtrim(lTRIM(cast(cast(tz.u_length as real)*100 as char(10)))) +''cm'' as mksize " & vbCrLf _
                 & " from ITT1 as t0 " & vbCrLf _
                 & "left join [@INS_OPLM] as t1 on t1.U_ItemCode=t0.code " & vbCrLf _
                 & "left join [@INCM_SZE1] as tz on tz.U_Name=t1.U_Size ) p " & vbCrLf _
                 & "pivot(max(mksize) for ugrp IN('+@subjtot+')) AS p1'  " & vbCrLf _
                 & "execute(@sqlstr)"

        msql = "IF OBJECT_ID (N'contentbom', N'U') IS NOT NULL SELECT 1 AS res ELSE SELECT 0 AS res"
        Dim CMDchk As New OleDb.OleDbCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim DRchk As OleDb.OleDbDataReader
        DRchk = CMDchk.ExecuteReader
        If DRchk.HasRows = True Then
            While DRchk.Read
                chktab = DRchk.Item("res")
            End While
        End If
        DRchk.Close()
        CMDchk.Dispose()

        If chktab = 0 Then
            msql2 = "CREATE TABLE [dbo].[contentbom](" & vbCrLf _
               & "[code] [nchar](20) NULL," & vbCrLf _
               & "[dhoti] [nchar](30) NULL," & vbCrLf _
               & "[shirting] [nchar](30) NULL," & vbCrLf _
               & "[suiting] [nchar](30) NULL," & vbCrLf _
               & "[towel] [nchar](30) NULL," & vbCrLf _
               & "[pcs] [real] NULL," & vbCrLf _
               & "[perfume] [nchar](30) NULL," & vbCrLf _
               & "[belt] [nchar(30] NULL," & vbCrLf _
               & "[rate] [real] NULL" & vbCrLf _
               & " ) ON [PRIMARY]"
            Dim cmd2 As New OleDb.OleDbCommand(msql2, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd2.ExecuteNonQuery()
            cmd2.Dispose()

            Dim cmd3 As New OleDb.OleDbCommand(msql, con)

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            cmd3.ExecuteNonQuery()
            cmd3.Dispose()
        End If

    End Sub





    Private Sub loadhead()
        msql = "select docentry,comp,group1 as type,group2 as prntype,printon ,stickercol,labrow,labcol,printer from barhead where active=1"
        Dim CMD As New OleDb.OleDbCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()
        flxc.Clear()
        Call flxchead()
        Try
            ''Dim DR As SqlDataReader
            Dim DR As OleDb.OleDbDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                With flxc
                    While DR.Read
                        .Rows = .Rows + 1
                        .Row = .Rows - 1
                        .set_TextMatrix(.Row, 0, DR.Item("docentry"))
                        .set_TextMatrix(.Row, 1, DR.Item("comp"))
                        .set_TextMatrix(.Row, 2, DR.Item("type"))
                        .set_TextMatrix(.Row, 3, DR.Item("prntype"))
                        .set_TextMatrix(.Row, 4, DR.Item("printon"))
                        .set_TextMatrix(.Row, 5, DR.Item("stickercol"))
                        .set_TextMatrix(.Row, 6, DR.Item("labrow"))
                        .set_TextMatrix(.Row, 7, DR.Item("labcol"))
                        .set_TextMatrix(.Row, 8, DR.Item("printer") & vbNullString)


                    End While
                End With
            End If
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            flx.Clear()
            Call flxchead()
        End Try

        CMD.Dispose()
    End Sub
    Private Sub flxhead()
        flx.Rows = 1
        flx.Cols = 24
        flx.set_ColWidth(0, 600)
        flx.set_ColWidth(1, 2500)
        flx.set_ColWidth(2, 1000)
        flx.set_ColWidth(3, 1200)
        flx.set_ColWidth(4, 1000)
        flx.set_ColWidth(5, 1000)
        flx.set_ColWidth(6, 1000)
        flx.set_ColWidth(7, 1000)
        flx.set_ColWidth(8, 1000)
        flx.set_ColWidth(9, 1000)
        flx.set_ColWidth(10, 1)
        flx.set_ColWidth(11, 1)
        flx.set_ColWidth(12, 1)
        flx.set_ColWidth(13, 1)
        flx.set_ColWidth(14, 1)
        flx.set_ColWidth(15, 100)
        flx.set_ColWidth(16, 100)
        flx.set_ColWidth(17, 100)
        flx.set_ColWidth(18, 100)
        flx.set_ColWidth(19, 100)
        flx.set_ColWidth(20, 1)
        flx.set_ColWidth(21, 700)
        flx.set_ColWidth(22, 900)
        flx.set_ColWidth(23, 900)


        flx.Row = 0
        flx.Col = 0
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 0, "Sel")

        flx.Col = 1
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 1, "Quality")

        flx.Col = 2
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 2, "Size")

        flx.Col = 3
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 3, "Style")

        flx.Col = 4
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 4, "Qty")

        flx.Col = 5
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 5, "MRP")

        flx.Col = 6
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 6, "Rate")

        flx.Col = 7
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 7, "Box")

        flx.Col = 8
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 8, "InvnY/N")

        flx.Col = 9
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 9, "UOM")

        flx.Col = 10
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 10, "Width")
        flx.Col = 11
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 11, "LEN")
        flx.Col = 12
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 12, "U_ITMGRP")
        flx.Col = 15
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 15, "DHOTI")
        flx.Col = 16
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 16, "SHIRTING")
        flx.Col = 17
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 17, "SUITING")
        flx.Col = 18
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 18, "TOWEL")

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
        flx.Col = 22
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 22, "Color")

        flx.Col = 23
        flx.CellAlignment = 3
        flx.CellFontBold = True
        flx.set_TextMatrix(0, 22, "dbox")

        flx.set_ColAlignment(0, 2)
        flx.set_ColAlignment(1, 2)
        flx.set_ColAlignment(2, 2)
        flx.set_ColAlignment(3, 2)


    End Sub

    Private Sub flxchead()
        flxc.Rows = 1
        flxc.Cols = 9
        flxc.set_ColWidth(0, 900)
        flxc.set_ColWidth(1, 1100)
        flxc.set_ColWidth(2, 1000)
        flxc.set_ColWidth(3, 1200)
        flxc.set_ColWidth(4, 1200)
        flxc.set_ColWidth(5, 1200)
        flxc.set_ColWidth(6, 500)
        flxc.set_ColWidth(7, 500)
        flxc.set_ColWidth(8, 1200)

        flxc.Row = 0
        flxc.Col = 0
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 0, "No")

        flxc.Col = 1
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 1, "Company")

        flxc.Col = 2
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 2, "Type")

        flxc.Col = 3
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 3, "Prn-Type")

        flxc.Col = 4
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 4, "Printon")

        flxc.Col = 5
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 5, "Stik-Col")

        flxc.Col = 6
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 6, "L.row")

        flxc.Col = 7
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 7, "L.Col")

        flxc.Col = 8
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 8, "Printer")


        flxc.set_ColAlignment(0, 2)
        flxc.set_ColAlignment(1, 2)
        flxc.set_ColAlignment(2, 2)
        flxc.set_ColAlignment(3, 2)
        flxc.set_ColAlignment(8, 2)

    End Sub

    Private Sub barprn()
        Dim i, k As Integer
        Dim sno As Integer
        Dim mpurrate As Double
        mtxtbno = 0
        sno = 1

        FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
        'FileOpen(1, "c:\sbarcode.txt", OpenMode.Output, OpenAccess.Write)
        'FileOpen(1, "LPT1", OpenMode.Output)
        PrintLine(1, TAB(0), "I8,A,001")
        PrintLine(1, TAB(0), "")
        PrintLine(1, TAB(0), "")
        PrintLine(1, TAB(0), "Q318,024")
        PrintLine(1, TAB(0), "q831")
        PrintLine(1, TAB(0), "rN")
        PrintLine(1, TAB(0), "S4")
        PrintLine(1, TAB(0), "D10")
        PrintLine(1, TAB(0), "ZT")
        PrintLine(1, TAB(0), "JF")
        PrintLine(1, TAB(0), "O")
        PrintLine(1, TAB(0), "R16,0")
        PrintLine(1, TAB(0), "f100")
        PrintLine(1, TAB(0), "N")
        For i = 1 To flx.Rows - 1
            'k = 1
            If Len(Trim(flx.get_TextMatrix(i, 0))) > 0 Then
                For k = 1 To Val(flx.get_TextMatrix(i, 5))

                    mpurrate = Val(flx.get_TextMatrix(i, 6)) * 72
                    PrintLine(1, TAB(0), "A" & Trim(Str(21 + Val(mtxtbno))) & ",163,0,3,1,1,N," & """" & Trim(Str(mpurrate)) & "     " & Trim(txtbno.Text) & """")
                    PrintLine(1, TAB(0), "A" & Trim(Str(125 + Val(mtxtbno))) & ",101,0,3,2,2,N," & """" & Trim(flx.get_TextMatrix(i, 4)) & """")
                    PrintLine(1, TAB(0), "A" & Trim(Str(22 + Val(mtxtbno))) & ",100,0,3,2,2,N," & """" & "Rs:" & """")
                    PrintLine(1, TAB(0), "A" & Trim(Str(91 + Val(mtxtbno))) & ",60,0,2,1,1,N," & """" & Trim(flx.get_TextMatrix(i, 3)) & """")
                    PrintLine(1, TAB(0), "A" & Trim(Str(20 + Val(mtxtbno))) & ",61,0,2,1,1,N," & """" & "Size:" & """")
                    PrintLine(1, TAB(0), "A" & Trim(Str(18 + Val(mtxtbno))) & ",13,0,3,1,1,N," & """" & Trim(flx.get_TextMatrix(i, 2)) & """")
                    PrintLine(1, TAB(0), "B" & Trim(Str(19 + Val(mtxtbno))) & ",196,0,1,2,6,71,B," & """" & Trim(flx.get_TextMatrix(i, 1)) & """")
                    mtxtbno = mtxtbno + 415

                    If sno = 2 Then
                        'sno = sno + 1
                        PrintLine(1, TAB(0), "P1")
                        If k <> Val(flx.get_TextMatrix(i, 5)) Then
                            'If sno > 2 Then
                            PrintLine(1, TAB(0), "I8,A,001")
                            PrintLine(1, TAB(0), "")
                            PrintLine(1, TAB(0), "")

                            PrintLine(1, TAB(0), "Q318,024")
                            PrintLine(1, TAB(0), "q831")
                            PrintLine(1, TAB(0), "rN")
                            PrintLine(1, TAB(0), "S4")
                            PrintLine(1, TAB(0), "D10")
                            PrintLine(1, TAB(0), "ZT")
                            PrintLine(1, TAB(0), "JF")
                            PrintLine(1, TAB(0), "O")
                            PrintLine(1, TAB(0), "R16,0")
                            PrintLine(1, TAB(0), "f100")
                            PrintLine(1, TAB(0), "N")
                        End If
                        sno = 0
                        mtxtbno = 0
                    End If
                    sno = sno + 1
                    ' k = k + 1
                Next k
            End If
            'PrintLine(1, TAB(0), "p" & Trim(Str(Val(sno))))
        Next i
        If sno <> 1 Then
            PrintLine(1, TAB(0), "P1")
        End If

        FileClose(1)
        Shell("print /d:LPT1 c:\sbarcode.txt", vbNormalFocus)
        'Shell("print c:\sbarcode.txt.zpl")

    End Sub
    Private Sub loaddatan()
        Dim CMD As New OleDb.OleDbCommand("select stkcode,size,itcode,sizecode,mrprate,prate,qty FROM purchaseline WHERE purcno=" & Microsoft.VisualBasic.Val(txtbno.Text) & " and cmp_id='" & mcmpid & "'", con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()
        flx.Clear()
        Call flxhead()
        Try
            ''Dim DR As SqlDataReader
            Dim DR As OleDb.OleDbDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                With flx
                    While DR.Read
                        .Rows = .Rows + 1
                        .Row = .Rows - 1
                        .set_TextMatrix(.Row, 1, DR.Item("stkcode"))
                        .set_TextMatrix(.Row, 2, getid("itemmaster", "itemname", "itcode", DR.Item("itcode")))
                        .set_TextMatrix(.Row, 3, getid("sizemaster", "sizename", "sizecode", DR.Item("sizecode")))
                        .set_TextMatrix(.Row, 4, DR.Item("mrprate"))
                        .set_TextMatrix(.Row, 5, DR.Item("qty"))
                        .set_TextMatrix(.Row, 6, DR.Item("prate"))



                    End While
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            flx.Clear()
            Call flxhead()
        End Try

    End Sub
    Private Sub loaddatadet()
        errcod = ""
        If chksr.Checked = True Then
            mfil = "(select t0.docentry,t2.u_itemcode as itemcode,t0.itemcode remarks,t2.U_Size, t1.u_catalgcode,t1.u_itemname as u_catalogname,t0.linenum,t0.freetxt,t0.quantity,CASE when t3.InvntItem='N' then 'S' else '' end treetype,'' as text from srbardet t0 " & vbCrLf _
                & "left join [@INS_PLM1] t1 on  convert(nvarchar(40),t1.U_Remarks)= t0.itemcode " & vbCrLf _
                & " left join [@INS_OPLM] t2 on t2.DocEntry=t1.DocEntry " & vbCrLf _
                & "left join OITM t3 on t3.ItemCode=t2.U_ItemCode " & vbCrLf _
                & " where t1.U_Lock<>'Y')"
            mfildet = "(select docentry,docnum,docdate,CardCode,cardname from srbardet group by docentry,docnum,docdate,CardCode,cardname) "
            'and CHARINDEX('[AIR]',t1.u_catalgcode)>0'
            'mfil = "V_sampinv1"
            'mfildet = "v_sampOinv"
        Else
            If Trim(cmbprnon.Text) = "SALES" Then
                mfil = "INV1"
                mfildet = "OINV"
            ElseIf Trim(cmbprnon.Text) = "DATE ORDER" Then
                mfil = "DLN1"
                mfildet = "ODLN"
            ElseIf Trim(cmbprnon.Text) = "INV DRAFT" Then
                mfil = "DRF1"
                mfildet = "ODRF"
            ElseIf Trim(cmbprnon.Text) = "SAMPLE" Then
                mfil = "V_sampinv1"
                mfildet = "v_sampOinv"
            End If
        End If

        'If expiredata(mfildet, "docdate", "docnum", Val(txtbno.Text)) = True Then
        '    End
        'End If

        ''& "CASE when   CHARINDEX('CMS',k.u_length)<=0 and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp))in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  ltrim(convert(varchar,round(cast(replace(k.u_length,'CMS','') as real)/100,2)))+'mX' + rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end as msize," & vbCrLf
        ''     & "CASE when   CHARINDEX('CMS',k.u_length)<=0 and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp))in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  replace(RTRIM(k.u_length),'S','X')+ rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end as msize2, " & vbcrlf _



        ''msql = "select t0.docnum,t1.linenum,t0.docdate,t0.cardcode,t0.cardname,t1.itemcode,t1.u_catalogname as quality,t1.u_size,convert(decimal(16),t1.quantity) as quantity,floor(convert(decimal(16),t1.quantity)/cast(k.salpackun as int)) as boxqty,cr.state, " & vbCrLf _
        ''     & "k.u_catalgcode, k.u_itemgrp, k.salunitmsr as uom,case when bb.pcs>0 then cast(bb.pcs as int) else cast(k.salpackun as int) end as box, case when  CHARINDEX('CMS',k.u_length)<=0 then cast(isnull(k.u_length,0) as float) else 0 end  as u_length, cast(case when  CHARINDEX('CMS',k.u_width)<=0 then cast(isnull(k.u_width,0.0)as float) else 0.0 end as float)  as u_width, k.u_style, convert(decimal(11,2),k.U_SelPrice) as rate,cast((k.salpackun*k.u_mrp) as decimal(7,2)) as boxmrp, cast(k.u_mrp as decimal(7,2)) as mrp, k.U_Remarks, k.u_brand, k.invntitem,k.u_prntname,  " & vbCrLf _
        ''     & " ltrim(rtrim(right(YEAR(t0.docdate),2)))+right('0'+rtrim(ltrim(MONTH(t0.docdate))),2)+rtrim(ltrim(convert(nvarchar(100),k.u_remarks))) as autocode," & vbCrLf _
        ''     & "CASE when CHARINDEX('_',t1.U_Size)>0 then " & vbCrLf _
        ''     & " CASE when   CHARINDEX('CMS',k.u_length)<=0 and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp))in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0) then  RTRIM(k.u_width)+'inX'+rtrim(lTRIM(cast(cast(k.u_length as real)*1 as char(10)))) +'in' else  ltrim(convert(varchar,round(cast(replace(k.u_length,'CMS','') as real)/100,2)))+'mX' + rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end " & vbCrLf _
        ''     & " else " & vbCrLf _
        ''     & " CASE when   CHARINDEX('CMS',k.u_length)<=0 and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp))in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  ltrim(convert(varchar,round(cast(replace(k.u_length,'CMS','') as real)/100,2)))+'mX' + rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end end as msize, " & vbCrLf _
        ''     & "CASE when CHARINDEX('_',t1.U_Size)>0 then " & vbCrLf _
        ''     & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'inX'+rtrim(lTRIM(cast(cast(k.u_length as real)*1 as char(10)))) +'in' else  replace(RTRIM(k.u_length),'S','X')+ rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end " & vbCrLf _
        ''     & "else " & vbCrLf _
        ''     & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  replace(RTRIM(k.u_length),'S','X')+ rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end end as msize2, " & vbCrLf _
        ''     & "'MFD ' + substring(datename(m,GETDATE()),1,3) +' '+ CAST(DATEPART(YYYY,GETDATE()) as CHAR(4)) as mfd, " & vbCrLf _
        ''     & " (left(convert(varchar,t0.docnum) + '00000',5)+left(convert(varchar,t0.cardcode) +'0000000000000',13) + left(convert(varchar,convert(decimal(16),k.U_SelPrice)) + '00000',5)) as barcode, " & vbCrLf _
        ''     & " (ltrim(convert(varchar,t0.docnum)) + rtrim(convert(varchar,t0.cardcode))+ltrim(convert(varchar,convert(decimal(16),k.U_SelPrice)))) as barcode1,ltrim(convert(varchar,t0.docnum)) as barcode2,ltrim(convert(varchar,t0.docnum))+rtrim(t1.itemcode) as barcode3,bb.dhoti,bb.shirting,bb.suiting,bb.towel,bb.pcs as bompcs,k.u_subgrp6,k.color,convert(nvarchar(max),t1.text) as mtrstot  " & vbCrLf _
        ''     & "from " & Trim(mfil) & " as t1 " & vbCrLf _
        ''     & "left join " & Trim(mfildet) & " as t0 on t0.docentry=t1.DocEntry " & vbCrLf _
        ''     & "left join (select b.CardCode, CASE when len(rtrim(ltrim(u_salpricecode)))>0 and u_salpricecode IS not null then b.u_salpricecode else c.state end as state from OCRD b " & vbCrLf _
        ''     & "     left join CRD1 c on c.CardCode=b.cardcode and (c.Address='Office' or c.Address='Ship') and c.AdresType='B') cr on cr.CardCode=t0.CardCode " & vbCrLf _
        ''     & "left join barcodebom as bb on rtrim(BB.code)=rtrim(t1.itemcode) " & vbCrLf _
        ''     & "Left Join " & vbCrLf _
        ''     & "(select it.invntitem, t0.u_itemcode,t0.u_itemname as mainname,t1.u_catalgcode,t0.u_itemgrp,it.salunitmsr,it.salpackun,t0.u_size,tz.u_length,tz.u_width,case when t0.u_style IS null then ISNULL(t0.u_style,'') else  t0.u_style end u_style,case when t2.u_state='Other' then 'OS' else t2.U_State end u_state,t2.U_SelPrice,t2.u_mrp,convert(nvarchar(max),t1.U_Remarks) as u_remarks,t1.u_brand,it.u_subgrp5 as color,it.u_subgrp6,t1.u_prntname from [@INS_PLM1] as t1 " & vbCrLf _
        ''     & "left join [@INS_OPLM] as t0 on t0.DocEntry=t1.DocEntry " & vbCrLf _
        ''     & "left join [@INS_PLM2] as t2 on t2.DocEntry=t1.docentry and t2.U_RowID=t1.lineid" & vbCrLf _
        ''     & "left join [@INCM_SZE1] as tz on tz.U_Name=t0.u_size" & vbCrLf _
        ''     & "left join OITM as it on it.ItemCode=t0.U_ItemCode where t0.U_ItemCode is not null " & vbCrLf _
        ''     & "group by it.invntitem, t0.u_itemcode,t0.u_itemname,t1.u_catalgcode,t0.u_itemgrp,it.salunitmsr,it.salpackun,t0.u_size,tz.u_length,tz.u_width,case when t0.u_style IS null then ISNULL(t0.u_style,'') else  t0.u_style end ,case when t2.u_state='Other' then 'OS' else t2.U_State end ,t2.U_SelPrice,t2.u_mrp,convert(nvarchar(max),t1.U_Remarks),t1.u_brand,it.u_subgrp5,it.u_subgrp6,t1.u_prntname" & vbCrLf _
        ''     & ") as k on k.u_catalgcode=t1.U_CatalogName and k.u_itemcode=t1.ItemCode  and k.U_Size=t1.U_Size and k.U_State=cr.state" & vbCrLf
        ''If Trim(cmbprnon.Text) = "INV DRAFT" Then
        ''    msql = msql & " where t0.docentry=" & Val(txtbno.Text) & " and t1.TreeType<>'I' and t1.u_catalogname is not null and datepart(mm,t0.docdate)=" & Val(cmbmont.Text) & " and datepart(yyyy,t0.docdate)=" & Val(cmbyr.Text) & vbCrLf
        ''Else
        ''    msql = msql & " where t0.docnum=" & Val(txtbno.Text) & " and t1.TreeType<>'I' and t1.u_catalogname is not null  and datepart(mm,t0.docdate)=" & Val(cmbmont.Text) & " and datepart(yyyy,t0.docdate)=" & Val(cmbyr.Text) & vbCrLf
        ''End If

        ''msql = msql & " order by t1.linenum"
        ' ''--and k.U_Size=t1.U_Size
        ' ''and  isnull(k.U_Style,'')=isnull(t1.U_Style,'')

        ' ''& "left join CRD1 as cr on cr.CardCode=t0.CardCode and (cr.Address='Office' or cr.Address='Ship') and cr.AdresType='B' " & vbCrLf _

        If chksr.Checked = True Then
            msql = "Exec selectbarcode " & Val(txtbno.Text) & "," & Val(cmbmont.Text) & "," & Val(cmbyr.Text) & ",'" & Trim(cmbprnon.Text) & "',1"
        Else
            msql = "Exec selectbarcode " & Val(txtbno.Text) & "," & Val(cmbmont.Text) & "," & Val(cmbyr.Text) & ",'" & Trim(cmbprnon.Text) & "',0"
        End If
        ' msql4 = "Exec selectbarcode " & Val(txtbno.Text) & "," & val(  ",'" & Trim(cmbprnon.Text) & "','" & Trim(linkserver) & "','" & Trim(dbnam) & "'"
        'Dim rCMD As New OleDb.OleDbCommand(msql4, con3)



        Dim CMD As New OleDb.OleDbCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        CMD.CommandTimeout = 300
        'MsgBox(msql)
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()
        flx.Clear()
        Call flxhead()

        Try
            ''Dim DR As SqlDataReader
            Dim DR As OleDb.OleDbDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                With flx
                    While DR.Read
                        .Rows = .Rows + 1
                        .Row = .Rows - 1
                        'If .Row = 5 Then
                        '    'MsgBox("test")
                        'End If
                        '.set_TextMatrix(.Row, 0, DR.Item("docentry"))
                        '***
                        '####
                        errcod = DR.Item("itemcode") & vbNullString
                        If IsDBNull(DR.Item("mtrstot")) = False Then
                            If Len(RTrim(DR.Item("mtrstot"))) > 0 Then
                                mshmtr = Split(RTrim(DR.Item("mtrstot")), "+")
                                For i = 0 To UBound(mshmtr)
                                    If i > 0 Then
                                        .Rows = .Rows + 1
                                        .Row = .Rows - 1
                                    End If
                                    .set_TextMatrix(.Row, 1, DR.Item("quality") & vbNullString)
                                    If UCase(Trim(cmbtype.Text)) = "DHOTI" Then
                                        'If DR.Item("uom") = "MTRS" Then
                                        'If UCase(Trim(Mid(Trim(DR.Item("u_size")), 2, 59))) = "MSIZE" Then
                                        .set_TextMatrix(.Row, 21, Trim(Val(mshmtr(i))) & "M")
                                        '.set_TextMatrix(.Row, 2, Trim(DR.Item("msize")))
                                        '.set_TextMatrix(.Row, 2, Replace(Trim(DR.Item("msize")), "S", "X") & Trim(Val(mshmtr(i))))
                                        If Val(mshmtr(i)) > 0 Then
                                            '.set_TextMatrix(.Row, 2, Replace(Trim(DR.Item("u_size")), "S", "X") & Trim(Val(mshmtr(i))) & "M")
                                            .set_TextMatrix(.Row, 2, Microsoft.VisualBasic.Format(Val(Replace(Trim(DR.Item("u_size")), "CMS", "")) / 100, "##0.00") & "mX" & Trim(Microsoft.VisualBasic.Format(Val(mshmtr(i)), "#######.00")) & "m")


                                        Else
                                            .set_TextMatrix(.Row, 2, Trim(DR.Item("msize")))
                                        End If
                                        'Else
                                        '.set_TextMatrix(.Row, 2, (Trim(DR.Item("u_width")) & "cmX" & Trim(Val(DR.Item("u_length")) * 100) & "cm"))
                                        '  End If
                                        'Else
                                        '   .set_TextMatrix(.Row, 2, Trim(DR.Item("msize")))
                                        'End If

                                    Else
                                        If IsDBNull(DR.Item("u_length")) = False Then
                                            If Val(DR.Item("u_length")) > 0 Then
                                                .set_TextMatrix(.Row, 2, DR.Item("u_length"))
                                            Else
                                                .set_TextMatrix(.Row, 2, DR.Item("u_size"))
                                            End If
                                        Else
                                            .set_TextMatrix(.Row, 2, DR.Item("u_size"))
                                        End If

                                    End If
                                    '.set_TextMatrix(.Row, 2, DR.Item("u_size"))
                                    If IsDBNull(DR.Item("u_style")) = False Then
                                        .set_TextMatrix(.Row, 3, DR.Item("u_style"))
                                    Else
                                    End If
                                    If Trim(cmbprntype.Text) = "BOX" Then
                                        If Val(DR.Item("box")) > 0 And Val(DR.Item("quantity")) > 0 Then
                                            .set_TextMatrix(.Row, 4, 1)
                                        Else
                                            .set_TextMatrix(.Row, 4, 1)
                                        End If
                                        '.set_TextMatrix(.Row, 7, DR.Item("box"))
                                    Else
                                        .set_TextMatrix(.Row, 4, 1)
                                    End If
                                    '.set_TextMatrix(.Row, 4, Trim(Microsoft.VisualBasic.Format(Val(DR.Item("quantity")), "###########0")))
                                    '.set_TextMatrix(.Row, 4, DR.Item("quantity"))
                                    If IsDBNull(DR.Item("MRP")) = False Then
                                        .set_TextMatrix(.Row, 5, DR.Item("mrp"))
                                    Else
                                        .set_TextMatrix(.Row, 5, 0)
                                    End If
                                    If IsDBNull(DR.Item("rate")) = False Then
                                        .set_TextMatrix(.Row, 6, DR.Item("rate"))
                                    Else
                                        .set_TextMatrix(.Row, 6, 0)
                                    End If
                                    If IsDBNull(DR.Item("box")) = False Then
                                        .set_TextMatrix(.Row, 7, DR.Item("box"))

                                    Else
                                        .set_TextMatrix(.Row, 7, 1)
                                    End If

                                    .set_TextMatrix(.Row, 8, DR.Item("invntitem"))
                                    .set_TextMatrix(.Row, 9, DR.Item("uom") & vbNullString)
                                    .set_TextMatrix(.Row, 10, DR.Item("u_width"))
                                    .set_TextMatrix(.Row, 11, DR.Item("u_length"))
                                    .set_TextMatrix(.Row, 12, DR.Item("u_itemgrp"))
                                    .set_TextMatrix(.Row, 13, DR.Item("u_size"))
                                    .set_TextMatrix(.Row, 14, DR.Item("linenum"))
                                    'itemgrp()
                                    .set_TextMatrix(.Row, 15, DR.Item("DHOTI") & vbNullString)
                                    .set_TextMatrix(.Row, 16, DR.Item("SHIRTING") & vbNullString)
                                    .set_TextMatrix(.Row, 17, DR.Item("SUITING") & vbNullString)
                                    .set_TextMatrix(.Row, 18, DR.Item("TOWEL") & vbNullString)
                                    .set_TextMatrix(.Row, 19, DR.Item("invntitem") & vbNullString)
                                    .set_TextMatrix(.Row, 20, DR.Item("u_subgrp6") & vbNullString)
                                    .set_TextMatrix(.Row, 22, DR.Item("color") & vbNullString)








                                Next i
                                '///
                            Else
                                .set_TextMatrix(.Row, 1, DR.Item("quality") & vbNullString)
                                If UCase(Trim(cmbtype.Text)) = "DHOTI" Then
                                    'If DR.Item("uom") = "MTRS" Then
                                    ' .set_TextMatrix(.Row, 2, Replace(Trim(DR.Item("msize")), "S", "X") & Trim(Val(DR.Item("boxqty")) * 100) & "cm")
                                    'Else
                                    If InStr(DR.Item("u_size"), "_") > 0 Then
                                        .set_TextMatrix(.Row, 2, (Trim(DR.Item("u_width")) & "inX" & Trim(Microsoft.VisualBasic.Format(Val(DR.Item("u_length")) * 1, "####0.00")) & "in"))
                                    Else
                                        .set_TextMatrix(.Row, 2, (Trim(DR.Item("u_width")) & "cmX" & Trim(Microsoft.VisualBasic.Format(Val(DR.Item("u_length")) * 100, "####0.00")) & "cm"))
                                    End If
                                    '.set_TextMatrix(.Row, 2, (Trim(DR.Item("u_width")) & "cmX" & Trim(Microsoft.VisualBasic.Format(Val(DR.Item("u_length")) * 100, "####0.00")) & "cm"))
                                    'End If

                                Else
                                    If IsDBNull(DR.Item("u_length")) = False Then
                                        If Val(DR.Item("u_length")) > 0 Then
                                            .set_TextMatrix(.Row, 2, DR.Item("u_length"))
                                        Else
                                            .set_TextMatrix(.Row, 2, DR.Item("u_size"))
                                        End If
                                    Else
                                        .set_TextMatrix(.Row, 2, DR.Item("u_size"))
                                    End If

                                End If
                                '.set_TextMatrix(.Row, 2, DR.Item("u_size"))
                                If IsDBNull(DR.Item("u_style")) = False Then
                                    .set_TextMatrix(.Row, 3, DR.Item("u_style"))
                                Else
                                End If
                                If Trim(cmbprntype.Text) = "BOX" Then

                                    If IsDBNull(DR.Item("box")) = False Then

                                        If Val(DR.Item("box")) > 0 And Val(DR.Item("quantity")) > 0 And Val(DR.Item("dbox")) = 0 And Val(DR.Item("dboxqty")) = 0 Then
                                            .set_TextMatrix(.Row, 4, Trim(Microsoft.VisualBasic.Format((Val(DR.Item("quantity")) / Val(DR.Item("box"))), "###########0")))
                                        ElseIf Val(DR.Item("quantity")) > 0 And Val(DR.Item("dbox")) > 0 And Val(DR.Item("dboxqty")) > 0 Then
                                            .set_TextMatrix(.Row, 4, Trim(Microsoft.VisualBasic.Format(Val(DR.Item("dboxqty")), "###########0")))
                                        Else
                                            .set_TextMatrix(.Row, 4, Trim(Microsoft.VisualBasic.Format(Val(DR.Item("quantity")), "###########0")))
                                        End If
                                    Else
                                        .set_TextMatrix(.Row, 4, 0)
                                    End If


                                    '.set_TextMatrix(.Row, 7, DR.Item("box"))
                                Else
                                    .set_TextMatrix(.Row, 4, Trim(Microsoft.VisualBasic.Format(Val(DR.Item("quantity")), "###########0")))
                                End If
                                '.set_TextMatrix(.Row, 4, Trim(Microsoft.VisualBasic.Format(Val(DR.Item("quantity")), "###########0")))
                                '.set_TextMatrix(.Row, 4, DR.Item("quantity"))
                                If IsDBNull(DR.Item("MRP")) = False Then
                                    .set_TextMatrix(.Row, 5, DR.Item("mrp"))
                                Else
                                    .set_TextMatrix(.Row, 5, 0)
                                End If
                                If IsDBNull(DR.Item("rate")) = False Then
                                    .set_TextMatrix(.Row, 6, DR.Item("rate"))
                                Else
                                    .set_TextMatrix(.Row, 6, 0)
                                End If
                                If IsDBNull(DR.Item("box")) = False Then
                                    .set_TextMatrix(.Row, 7, DR.Item("box"))

                                Else
                                    .set_TextMatrix(.Row, 7, 1)
                                End If

                                .set_TextMatrix(.Row, 8, DR.Item("invntitem") & vbNullString)
                                .set_TextMatrix(.Row, 9, DR.Item("uom") & vbNullString)
                                .set_TextMatrix(.Row, 10, DR.Item("u_width"))
                                .set_TextMatrix(.Row, 11, DR.Item("u_length"))
                                .set_TextMatrix(.Row, 12, DR.Item("u_itemgrp") & vbNullString)
                                .set_TextMatrix(.Row, 13, DR.Item("u_size") & vbNullString)
                                .set_TextMatrix(.Row, 14, DR.Item("linenum"))
                                'itemgrp()
                                .set_TextMatrix(.Row, 15, DR.Item("DHOTI") & vbNullString)
                                .set_TextMatrix(.Row, 16, DR.Item("SHIRTING") & vbNullString)
                                .set_TextMatrix(.Row, 17, DR.Item("SUITING") & vbNullString)
                                .set_TextMatrix(.Row, 18, DR.Item("TOWEL") & vbNullString)
                                .set_TextMatrix(.Row, 19, DR.Item("invntitem") & vbNullString)
                                .set_TextMatrix(.Row, 20, DR.Item("u_subgrp6") & vbNullString)
                                .set_TextMatrix(.Row, 22, DR.Item("color") & vbNullString)



                            End If
                            '**
                        Else
                            ' End If

                            '******
                            '####
                            .set_TextMatrix(.Row, 1, DR.Item("quality") & vbNullString)
                            If UCase(Trim(cmbtype.Text)) = "DHOTI" Then
                                'If DR.Item("uom") = "MTRS" Then
                                ' .set_TextMatrix(.Row, 2, Replace(Trim(DR.Item("msize")), "S", "X") & Trim(Val(DR.Item("boxqty")) * 100) & "cm")
                                'Else

                                If InStr(DR.Item("u_size"), "_") > 0 Then
                                    .set_TextMatrix(.Row, 2, (Trim(DR.Item("u_width")) & "inX" & Trim(Microsoft.VisualBasic.Format(Val(DR.Item("u_length")) * 1, "####0.00")) & "in"))
                                Else
                                    .set_TextMatrix(.Row, 2, (Trim(DR.Item("u_width")) & "cmX" & Trim(Microsoft.VisualBasic.Format(Val(DR.Item("u_length")) * 100, "####0.00")) & "cm"))
                                End If
                                'End If

                            Else
                                If IsDBNull(DR.Item("u_length")) = False Then
                                    If Val(DR.Item("u_length")) > 0 Then
                                        .set_TextMatrix(.Row, 2, DR.Item("u_length"))
                                    Else
                                        .set_TextMatrix(.Row, 2, DR.Item("u_size"))
                                    End If
                                Else
                                    .set_TextMatrix(.Row, 2, DR.Item("u_size"))
                                End If

                            End If
                            '.set_TextMatrix(.Row, 2, DR.Item("u_size"))
                            If IsDBNull(DR.Item("u_style")) = False Then
                                .set_TextMatrix(.Row, 3, DR.Item("u_style"))
                            Else
                            End If
                            If Trim(cmbprntype.Text) = "BOX" Then
                                If IsDBNull(DR.Item("box")) = False Then
                                    If Val(DR.Item("box")) > 0 And Val(DR.Item("quantity")) > 0 And Val(DR.Item("dbox")) = 0 And Val(DR.Item("dboxqty")) = 0 Then
                                        .set_TextMatrix(.Row, 4, Trim(Microsoft.VisualBasic.Format((Val(DR.Item("quantity")) / Val(DR.Item("box"))), "###########0")))
                                    ElseIf Val(DR.Item("quantity")) > 0 And Val(DR.Item("dbox")) > 0 And Val(DR.Item("dboxqty")) > 0 Then
                                        .set_TextMatrix(.Row, 4, Trim(Microsoft.VisualBasic.Format(Val(DR.Item("dboxqty")), "###########0")))

                                    Else
                                        .set_TextMatrix(.Row, 4, Trim(Microsoft.VisualBasic.Format(Val(DR.Item("quantity")), "###########0")))
                                    End If
                                Else
                                    .set_TextMatrix(.Row, 4, 0)
                                End If

                                '.set_TextMatrix(.Row, 7, DR.Item("box"))
                            Else
                                .set_TextMatrix(.Row, 4, Trim(Microsoft.VisualBasic.Format(Val(DR.Item("quantity")), "###########0")))
                            End If
                            '.set_TextMatrix(.Row, 4, Trim(Microsoft.VisualBasic.Format(Val(DR.Item("quantity")), "###########0")))
                            '.set_TextMatrix(.Row, 4, DR.Item("quantity"))
                            If IsDBNull(DR.Item("MRP")) = False Then
                                .set_TextMatrix(.Row, 5, Microsoft.VisualBasic.Val(DR.Item("mrp")))
                            Else
                                .set_TextMatrix(.Row, 5, 0)
                            End If
                            If IsDBNull(DR.Item("rate")) = False Then
                                .set_TextMatrix(.Row, 6, DR.Item("rate"))
                            Else
                                .set_TextMatrix(.Row, 6, 0)
                            End If
                            If IsDBNull(DR.Item("box")) = False Then
                                .set_TextMatrix(.Row, 7, DR.Item("box"))

                            Else
                                .set_TextMatrix(.Row, 7, 1)
                            End If

                            .set_TextMatrix(.Row, 8, DR.Item("invntitem"))
                            .set_TextMatrix(.Row, 9, DR.Item("uom") & vbNullString)
                            .set_TextMatrix(.Row, 10, DR.Item("u_width"))
                            .set_TextMatrix(.Row, 11, DR.Item("u_length"))
                            .set_TextMatrix(.Row, 12, DR.Item("u_itemgrp") & vbNullString)
                            .set_TextMatrix(.Row, 13, DR.Item("u_size") & vbNullString)
                            .set_TextMatrix(.Row, 14, DR.Item("linenum"))
                            'itemgrp()
                            .set_TextMatrix(.Row, 15, DR.Item("DHOTI") & vbNullString)
                            .set_TextMatrix(.Row, 16, DR.Item("SHIRTING") & vbNullString)
                            .set_TextMatrix(.Row, 17, DR.Item("SUITING") & vbNullString)
                            .set_TextMatrix(.Row, 18, DR.Item("TOWEL") & vbNullString)
                            .set_TextMatrix(.Row, 19, DR.Item("invntitem") & vbNullString)
                            .set_TextMatrix(.Row, 20, DR.Item("u_subgrp6") & vbNullString)
                            .set_TextMatrix(.Row, 22, DR.Item("color") & vbNullString)
                            '#####
                        End If

                    End While
                End With
            End If
            DR.Close()
            'Catch sqlEx as oledbException When sqlEx.Number = [SQL error number]
            'Do something about the exception
            '--Catch sqlEx as oledbException When sqlEx.Number = [Another SQL error number]

            'Do something about the exception
        Catch sqlEx As OleDbException  '
            'MsgBox(sqlEx.Message)
            DisplaySqlErrors(sqlEx)

        Catch ex As Exception
            'MsgBox("Check " & DR.Item("quality"))

            MsgBox(ex.Message & " - " & errcod)
            'MsgBox("Check " & DR.Item("quality"))
            'dr.close()
            flx.Clear()
            Call flxchead()
        End Try

        CMD.Dispose()
        ' Call crtmptab()

    End Sub

    Private Sub crtmptabst()
        If chksr.Checked = True Then
            mfil = "(select t0.docentry,t2.u_itemcode as itemcode,t0.itemcode remarks,t2.U_Size, t1.u_catalgcode,t1.u_itemname as u_catalogname,t0.linenum,t0.freetxt,t0.quantity,CASE when t3.InvntItem='N' then 'S' else '' end treetype,'' as text from srbardet t0 " & vbCrLf _
                & "left join [@INS_PLM1] t1 on  convert(nvarchar(40),t1.U_Remarks)= t0.itemcode " & vbCrLf _
                & " left join [@INS_OPLM] t2 on t2.DocEntry=t1.DocEntry " & vbCrLf _
                & "left join OITM t3 on t3.ItemCode=t2.U_ItemCode " & vbCrLf _
                & " where t1.U_Lock<>'Y')"
            mfildet = "(select docentry,docnum,docdate,CardCode,cardname from srbardet group by docentry,docnum,docdate,CardCode,cardname) "

            'mfil = "V_sampinv1"
            'mfildet = "v_sampOinv"
        Else


            If Trim(cmbprnon.Text) = "SALES" Then
                mfil = "INV1"
                mfildet = "OINV"
            ElseIf Trim(cmbprnon.Text) = "DATE ORDER" Then
                mfil = "DLN1"
                mfildet = "ODLN"
            ElseIf Trim(cmbprnon.Text) = "INV DRAFT" Then
                mfil = "DRF1"
                mfildet = "ODRF"
            ElseIf Trim(cmbprnon.Text) = "SAMPLE" Then
                mfil = "V_sampinv1"
                mfildet = "v_sampOinv"
            End If
        End If


        If chksr.Checked = True Then
            msql = "Exec insertbartemp " & Val(txtbno.Text) & "," & Val(cmbmont.Text) & "," & Val(cmbyr.Text) & ",'" & Trim(cmbprnon.Text) & "',1"
        Else
            msql = "Exec insertbartemp " & Val(txtbno.Text) & "," & Val(cmbmont.Text) & "," & Val(cmbyr.Text) & ",'" & Trim(cmbprnon.Text) & "',0"
        End If


        'msql = "Exec insertbartemp " & Val(txtbno.Text) & "," & Val(cmbmont.Text) & "," & Val(cmbyr.Text) & ",'" & Trim(cmbprnon.Text) & "'"

        Dim dCMD As New OleDb.OleDbCommand(msql, con)

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        'dCMD.CommandText = "Exec inserbartemp 'Test', 'Test', 'Test'"
        dCMD.CommandText = msql
        dCMD.Connection = con 'Active Connection 
        Try
            dCMD.ExecuteNonQuery()

            dCMD.Dispose()
        Catch ex As Exception
            dCMD.Dispose()
            MsgBox(ex.Message)

        End Try

    End Sub
    Private Sub crtmptab()
        If chksr.Checked = True Then
            mfil = "(select t0.docentry,t2.u_itemcode as itemcode,t0.itemcode remarks,t2.U_Size, t1.u_catalgcode,t1.u_itemname as u_catalogname,t0.linenum,t0.freetxt,t0.quantity,CASE when t3.InvntItem='N' then 'S' else '' end treetype,'' as text from srbardet t0 " & vbCrLf _
                & "left join [@INS_PLM1] t1 on  convert(nvarchar(40),t1.U_Remarks)= t0.itemcode " & vbCrLf _
                & " left join [@INS_OPLM] t2 on t2.DocEntry=t1.DocEntry " & vbCrLf _
                & "left join OITM t3 on t3.ItemCode=t2.U_ItemCode " & vbCrLf _
                & " where t1.U_Lock<>'Y')"
            mfildet = "(select docentry,docnum,docdate,CardCode,cardname from srbardet group by docentry,docnum,docdate,CardCode,cardname) "

            'mfil = "V_sampinv1"
            'mfildet = "v_sampOinv"
        Else


            If Trim(cmbprnon.Text) = "SALES" Then
                mfil = "INV1"
                mfildet = "OINV"
            ElseIf Trim(cmbprnon.Text) = "DATE ORDER" Then
                mfil = "DLN1"
                mfildet = "ODLN"
            ElseIf Trim(cmbprnon.Text) = "INV DRAFT" Then
                mfil = "DRF1"
                mfildet = "ODRF"
            ElseIf Trim(cmbprnon.Text) = "SAMPLE" Then
                mfil = "V_sampinv1"
                mfildet = "v_sampOinv"
            End If
        End If

        'srbardet -table
        '         CREATE TABLE [dbo].[srbardet](
        '	[docentry] [int] NULL,
        '	[docnum] [int] NULL,
        '	[docdate] [datetime] NULL,
        '	[cardcode] [nvarchar](30) NULL,
        '	[cardname] [nvarchar](200) NULL,
        '	[linenum] [int] NULL,
        '	[itemcode] [nvarchar](40) NULL,
        '	[freetxt] [nvarchar](200) NULL,
        '	[quantity] [numeric](16, 9) NULL
        ') ON [PRIMARY]


        '        select t0.docentry,t2.u_itemcode as itemcode,t0.itemcode remarks,t2.U_Size, t1.u_catalgcode,t1.u_itemname as u_catalogname,t0.linenum,t0.freetxt,t0.quantity,CASE when t3.InvntItem='N' then 'S' else '' end treetype,'' as text from srbardet t0
        'left join [@INS_PLM1] t1 on  convert(nvarchar(40),t1.U_Remarks)= t0.itemcode
        'left join [@INS_OPLM] t2 on t2.DocEntry=t1.DocEntry
        'left join OITM t3 on t3.ItemCode=t2.U_ItemCode
        'where t1.U_Lock<>'Y'

        'select docentry,docnum,docdate,CardCode,cardname from srbardet group by docentry,docnum,docdate,CardCode,cardname 


        'drop

        'msql4 = " IF OBJECT_ID(N'tempdb..##barTemp') IS NOT NULL " & vbCrLf _
        '      & " BEGIN " & vbCrLf _
        '      & "  DROP TABLE ##barTemp " & vbCrLf _
        '      & " End "



        'Dim dCMD As New OleDb.OleDbCommand(msql4, con)
        ''Dim CMD2 As New SqlClient.SqlCommand("DELETE FROM BPORD WHERE BNO=" & Val(txtbno.Text), con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'dCMD.ExecuteNonQuery()


        'Try

        '    dCMD.Dispose()
        '    'CMD2.Dispose()
        'Catch ex As Exception
        '    'TRANS.Rollback()
        '    MsgBox(ex.Message)
        '    dCMD.Dispose()
        '    'CMD2.Dispose()
        'End Try

        ''TRANS.Commit()
        'dCMD.Dispose()
        'dCMD = Nothing



        'msql5 = "CREATE TABLE ##bartemp( " & vbCrLf _
        '& " docentry int NULL, " & vbCrLf _
        '& " docnum int NULL, " & vbCrLf _
        '& "linenum int NOT NULL, " & vbCrLf _
        '& "docdate datetime NULL, " & vbCrLf _
        '& " cardcode nvarchar(15) NULL, " & vbCrLf _
        '& "cardname nvarchar(100) NULL, " & vbCrLf _
        '& " itemcode nvarchar(20) NULL, " & vbCrLf _
        '& " quality nvarchar(100) NULL, " & vbCrLf _
        '& " u_size nvarchar(100) NULL, " & vbCrLf _
        '& "quantity decimal(16, 0) NULL, " & vbCrLf _
        '& "boxqty decimal(27, 0) NULL, " & vbCrLf _
        '& "state nvarchar(3) NULL, " & vbCrLf _
        '& "u_catalgcode nvarchar(50) NULL, " & vbCrLf _
        '& "u_itemgrp nvarchar(30) NULL, " & vbCrLf _
        '& "uom nvarchar(20) NULL, " & vbCrLf _
        '& " box int NULL," & vbCrLf _
        '& " u_length real NULL, " & vbCrLf _
        '& "u_width int NOT NULL, " & vbCrLf _
        '& "u_style nvarchar(30) NULL, " & vbCrLf _
        '& "rate decimal(11, 2) NULL, " & vbCrLf _
        '& " boxmrp decimal(7, 2) NULL, " & vbCrLf _
        '& " mrp decimal(7, 2) NULL," & vbCrLf _
        '& " U_Remarks ntext NULL, " & vbCrLf _
        '& " u_brand nvarchar(50) NULL, " & vbCrLf _
        '& " invntitem char(1) NULL, " & vbCrLf _
        '& " msize nvarchar(45) NULL, " & vbCrLf _
        '& " msize2 nvarchar(4000) NULL, " & vbCrLf _
        '& " mfd nvarchar(12) NULL, " & vbCrLf _
        '& " barcode varchar(23) NULL, " & vbCrLf _
        '& " barcode1 varchar(90) NULL, " & vbCrLf _
        '& " barcode2 varchar(30) NULL, " & vbCrLf _
        '& " barcode3 nvarchar(50) NULL, " & vbCrLf _
        '& " drbarcode nvarchar(50) NULL, " & vbCrLf _
        '& " dhoti nchar(30) NULL, " & vbCrLf _
        '& " shirting nchar(30) NULL, " & vbCrLf _
        '& " suiting nchar(30) NULL, " & vbCrLf _
        '& " towel nchar(30) NULL, " & vbCrLf _
        '& " u_subgrp6 nvarchar(100) NULL, " & vbCrLf _
        '& "color nvarchar(100) NULL, " & vbCrLf _
        '& " mtrstot ntext NULL," & vbCrLf _
        '& " u_catalogname nvarchar(50) NULL, " & vbCrLf _
        '& " autocode nvarchar(30) NULL " & vbCrLf _
        '& ") "


        'Dim cdCMD As New OleDb.OleDbCommand(msql5, con)
        ' ''Dim CMD2 As New SqlClient.SqlCommand("DELETE FROM BPORD WHERE BNO=" & Val(txtbno.Text), con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'cdCMD.ExecuteNonQuery()


        'Try
        '    'TRANS.Commit()
        '    ' MsgBox("Deleted")
        '    cdCMD.Dispose()
        '    'CMD2.Dispose()
        'Catch ex As Exception
        '    'TRANS.Rollback()
        '    MsgBox(ex.Message)
        '    cdCMD.Dispose()
        '    'CMD2.Dispose()
        'End Try










        ''msql = "select t0.docentry, t0.docnum,t1.linenum,t0.docdate,t0.cardcode,t0.cardname,t1.itemcode,case when t1.freetxt is null or len(rtrim(t1.freetxt))=0  then t1.u_catalogname else t1.freetxt  end  as quality,t1.u_size,convert(decimal(16),t1.quantity) as quantity,floor(convert(decimal(16),t1.quantity)/cast(k.salpackun as int)) as boxqty,cr.state, " & vbCrLf _
        ''    & "k.u_catalgcode, k.u_itemgrp, k.salunitmsr as uom,cast(k.salpackun as int) as box, case when CHARINDEX('CMS',k.u_length)<=0 then cast(isnull(k.u_length,0) as real) else 0 end  as u_length, case when CHARINDEX('CMS',k.u_width)<=0  then isnull(k.u_width,0) else 0 end  as u_width, k.u_style,convert(decimal(11,2), k.U_SelPrice) as rate,cast((k.salpackun*k.u_mrp) as decimal(7,2)) as boxmrp, cast(k.u_mrp as decimal(7,2)) as mrp, k.U_Remarks, k.u_brand, k.invntitem, " & vbCrLf _
        ''    & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  RTRIM(k.u_length)  end as msize," & vbCrLf _
        ''    & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  replace(RTRIM(k.u_length),'S','X')+ rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end as msize2," & vbCrLf _
        ''    & "'MFD ' + substring(datename(m,GETDATE()),1,3) +' '+ CAST(DATEPART(YYYY,GETDATE()) as CHAR(4)) as mfd, " & vbCrLf _
        ''    & " (left(convert(varchar,t0.docnum) + '00000',5)+left(convert(varchar,t0.cardcode) +'0000000000000',13) + left(convert(varchar,convert(decimal(16),k.U_SelPrice)) + '00000',5)) as barcode, " & vbCrLf _
        ''    & " (ltrim(convert(varchar,t0.docnum)) + rtrim(convert(varchar,t0.cardcode))+ltrim(convert(varchar,convert(decimal(16),k.U_SelPrice)))) as barcode1,ltrim(convert(varchar,t0.docnum)) as barcode2,ltrim(convert(varchar,t0.docnum))+rtrim(t1.itemcode) as barcode3,ltrim(convert(varchar,t0.docentry))+rtrim(t1.itemcode) as drbarcode,bb.dhoti,bb.shirting,bb.suiting,bb.towel,k.u_subgrp6,k.color,t1.text as mtrstot   " & vbCrLf _
        ''    & " into #barTemp " & vbCrLf _
        ''    & "from " & Trim(mfil) & " as t1 " & vbCrLf _
        ''    & "left join " & Trim(mfildet) & " as t0 on t0.docentry=t1.DocEntry " & vbCrLf _
        ''    & "left join CRD1 as cr on cr.CardCode=t0.CardCode and (cr.Address='Office' or cr.Address='Ship') and cr.AdresType='B' " & vbCrLf _
        ''     & "left join barcodebom as bb on BB.code=t1.itemcode " & vbCrLf _
        ''    & "Inner Join " & vbCrLf _
        ''    & "(select it.invntitem, t0.u_itemcode,t0.u_itemname as mainname,t1.u_catalgcode,t0.u_itemgrp,it.salunitmsr,it.salpackun,t0.u_size,tz.u_length,tz.u_width,case when t0.u_style IS null then ISNULL(t0.u_style,'') else  t0.u_style end u_style,case when t2.u_state='Other' then 'OS' else t2.U_State end u_state,t2.U_SelPrice,t2.u_mrp,t1.U_Remarks,t1.u_brand,it.u_subgrp5 as color,it.u_subgrp6 from [@INS_PLM1] as t1 " & vbCrLf _
        ''    & "Inner join [@INS_OPLM] as t0 on t0.DocEntry=t1.DocEntry " & vbCrLf _
        ''    & "Inner join [@INS_PLM2] as t2 on t2.DocEntry=t1.docentry and t2.U_RowID=t1.lineid" & vbCrLf _
        ''    & "Inner join [@INCM_SZE1] as tz on tz.U_Name=t0.u_size" & vbCrLf _
        ''    & "Inner join OITM as it on it.ItemCode=t0.U_ItemCode where t0.U_ItemCode is not null) as k on k.u_catalgcode=t1.U_CatalogName and k.u_itemcode=t1.ItemCode and k.U_Size=t1.U_Size  and k.U_State=cr.state" & vbCrLf
        ''If Trim(cmbprnon.Text) = "INV DRAFT" Then
        ''    msql = msql & " where t0.docentry=" & Val(txtbno.Text) & " and t1.TreeType<>'I'" & vbCrLf
        ''Else
        ''    msql = msql & " where t0.docnum=" & Val(txtbno.Text) & " and t1.TreeType<>'I' " & vbCrLf
        ''End If

        ''If Len(Trim(flx.get_TextMatrix(i, 3))) > 0 Then
        ''    msql = msql & " and k.u_style='" & Trim(flx.get_TextMatrix(i, 3)) & "'" & vbCrLf
        ''End If
        ''msql = msql & " order by t1.linenum"



        ''Dim crCMD As New OleDb.OleDbCommand(msql, con)
        ' ''Dim CMD2 As New SqlClient.SqlCommand("DELETE FROM BPORD WHERE BNO=" & Val(txtbno.Text), con)
        ''If con.State = ConnectionState.Closed Then
        ''    con.Open()
        ''End If

        ''crCMD.ExecuteNonQuery()

        ''Try
        ''    'TRANS.Commit()
        ''    ' MsgBox("Deleted")
        ''    crCMD.Dispose()
        ''    'CMD2.Dispose()
        ''Catch ex As Exception
        ''    'TRANS.Rollback()
        ''    MsgBox(ex.Message)
        ''    crCMD.Dispose()
        ''    'CMD2.Dispose()
        ''End Try

        ' ''TRANS.Commit()
        ''crCMD.Dispose()


        '' Dim crdCMD As New OleDb.OleDbCommand(msql5, con)
        '' 'Dim CMD2 As New SqlClient.SqlCommand("DELETE FROM BPORD WHERE BNO=" & Val(txtbno.Text), con)
        '' If con.State = ConnectionState.Closed Then
        ''     con.Open()
        '' End If

        '' crdCMD.ExecuteNonQuery()
        '*****
        msql4 = "delete from bartemp "
        If Trim(cmbprnon.Text) = "INV DRAFT" Then
            msql4 = msql4 & " where docentry=" & Val(txtbno.Text) & vbCrLf
        Else
            msql4 = msql4 & " where docnum=" & Val(txtbno.Text) & vbCrLf
        End If

        Dim dCMD As New OleDb.OleDbCommand(msql4, con)
        'Dim CMD2 As New SqlClient.SqlCommand("DELETE FROM BPORD WHERE BNO=" & Val(txtbno.Text), con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        dCMD.ExecuteNonQuery()


        Try

            dCMD.Dispose()
            'CMD2.Dispose()
        Catch ex As Exception
            'TRANS.Rollback()
            MsgBox(ex.Message)
            dCMD.Dispose()
            'CMD2.Dispose()
        End Try

        'TRANS.Commit()
        dCMD.Dispose()
        dCMD = Nothing







        'insert


        'msql = "INSERT INTO BARTEMP(docentry,docnum,linenum,docdate,cardcode,cardname,itemcode,quality,u_size,quantity,boxqty,state,u_catalgcode,u_itemgrp,uom,box,u_length,u_width,u_style,rate,boxmrp,mrp,U_Remarks,u_brand,invntitem,msize,msize2,mfd,barcode,barcode1,barcode2,barcode3," & vbCrLf _
        '    & "  drbarcode,dhoti,shirting,suiting,towel,u_subgrp6,color,mtrstot)"


        'old
        'msql = " insert into barTemp select t0.docentry, t0.docnum,t1.linenum,t0.docdate,t0.cardcode,t0.cardname,t1.itemcode,case when t1.freetxt is null or len(rtrim(t1.freetxt))=0  then t1.u_catalogname else t1.freetxt  end  as quality,t1.u_size,convert(decimal(16),t1.quantity) as quantity,floor(convert(decimal(16),t1.quantity)/cast(k.salpackun as int)) as boxqty,cr.state, " & vbCrLf _
        '    & "k.u_catalgcode, k.u_itemgrp, k.salunitmsr as uom,cast(k.salpackun as int) as box, case when CHARINDEX('CMS',k.u_length)<=0 then cast(isnull(k.u_length,0) as real) else 0 end  as u_length, case when CHARINDEX('CMS',k.u_width)<=0  then isnull(k.u_width,0) else 0 end  as u_width, k.u_style,convert(decimal(11,2), k.U_SelPrice) as rate,cast((k.salpackun*k.u_mrp) as decimal(7,2)) as boxmrp, cast(k.u_mrp as decimal(7,2)) as mrp, k.U_Remarks, k.u_brand, k.invntitem, " & vbCrLf _
        '    & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  RTRIM(k.u_length)  end as msize," & vbCrLf _
        '    & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  replace(RTRIM(k.u_length),'S','X')+ rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end as msize2," & vbCrLf _
        '    & "'MFD ' + substring(datename(m,GETDATE()),1,3) +' '+ CAST(DATEPART(YYYY,GETDATE()) as CHAR(4)) as mfd, " & vbCrLf _
        '    & " (left(convert(varchar,t0.docnum) + '00000',5)+left(convert(varchar,t0.cardcode) +'0000000000000',13) + left(convert(varchar,convert(decimal(16),k.U_SelPrice)) + '00000',5)) as barcode, " & vbCrLf _
        '    & " (ltrim(convert(varchar,t0.docnum)) + rtrim(convert(varchar,t0.cardcode))+ltrim(convert(varchar,convert(decimal(16),k.U_SelPrice)))) as barcode1,ltrim(convert(varchar,t0.docnum)) as barcode2,ltrim(convert(varchar,t0.docnum))+rtrim(t1.itemcode) as barcode3,ltrim(convert(varchar,t0.docentry))+rtrim(t1.itemcode) as drbarcode,bb.dhoti,bb.shirting,bb.suiting,bb.towel,k.u_subgrp6,k.color,t1.text as mtrstot,t1.u_catalogname  " & vbCrLf _
        '    & "from " & Trim(mfil) & " as t1 " & vbCrLf _
        '    & "left join " & Trim(mfildet) & " as t0 on t0.docentry=t1.DocEntry " & vbCrLf _
        '    & "left join CRD1 as cr on cr.CardCode=t0.CardCode and (cr.Address='Office' or cr.Address='Ship') and cr.AdresType='B' " & vbCrLf _
        '    & "left join barcodebom as bb on BB.code=t1.itemcode " & vbCrLf _
        '    & "Inner Join " & vbCrLf _
        '    & "(select it.invntitem, t0.u_itemcode,t0.u_itemname as mainname,t1.u_catalgcode,t0.u_itemgrp,it.salunitmsr,it.salpackun,t0.u_size,tz.u_length,tz.u_width,case when t0.u_style IS null then ISNULL(t0.u_style,'') else  t0.u_style end u_style,case when t2.u_state='Other' then 'OS' else t2.U_State end u_state,t2.U_SelPrice,t2.u_mrp,t1.U_Remarks,t1.u_brand,it.u_subgrp5 as color,it.u_subgrp6 from [@INS_PLM1] as t1 " & vbCrLf _
        '    & "Inner join [@INS_OPLM] as t0 on t0.DocEntry=t1.DocEntry " & vbCrLf _
        '    & "Inner join [@INS_PLM2] as t2 on t2.DocEntry=t1.docentry and t2.U_RowID=t1.lineid" & vbCrLf _
        '    & "Inner join [@INCM_SZE1] as tz on tz.U_Name=t0.u_size" & vbCrLf _
        '    & "Inner join OITM as it on it.ItemCode=t0.U_ItemCode where t0.U_ItemCode is not null) as k on k.u_catalgcode=t1.U_CatalogName and k.u_itemcode=t1.ItemCode and k.U_Size=t1.U_Size  and k.U_State=cr.state" & vbCrLf
        'If Trim(cmbprnon.Text) = "INV DRAFT" Then
        '    msql = msql & " where t0.docentry=" & Val(txtbno.Text) & " and t1.TreeType<>'I'" & vbCrLf
        'Else
        '    msql = msql & " where t0.docnum=" & Val(txtbno.Text) & " and t1.TreeType<>'I' " & vbCrLf
        'End If

        'old

        '& "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  RTRIM(k.u_length)  end as msize2," & vbCrLf _


        '& "CASE when   CHARINDEX('CMS',k.u_length)<=0 and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp))in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  ltrim(convert(varchar,round(cast(replace(k.u_length,'CMS','') as real)/100,2)))+'mX' + rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end as msize," & vbCrLf _
        '   & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  replace(RTRIM(k.u_length),'S','X')+ rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end as msize2," & vbCrLf _



        msql = " insert into bartemp (docentry,docnum,linenum,docdate,cardcode,cardname,itemcode,quality,u_size,quantity,boxqty,state,u_catalgcode,u_itemgrp,uom,box,u_length,u_width,u_style,rate,boxmrp,mrp,U_Remarks,u_brand,invntitem,u_prntname,msize,msize2,mfd,barcode,barcode1,barcode2,barcode3,drbarcode,dhoti,shirting,suiting,towel,bompcs,u_subgrp6,color,mtrstot,u_catalogname,autocode,contdhoti,contshirting,contsuiting,conttowel,contperfume,contbelt,contrate,contpcs) " & vbCrLf _
            & " select t0.docentry, t0.docnum,t1.linenum,t0.docdate,t0.cardcode,t0.cardname,t1.itemcode,case when t1.freetxt is null or len(rtrim(t1.freetxt))=0  then t1.u_catalogname else t1.freetxt  end  as quality,t1.u_size,convert(decimal(16),t1.quantity) as quantity,floor(convert(decimal(16),t1.quantity)/cast(k.salpackun as int)) as boxqty,cr.state, " & vbCrLf _
            & "k.u_catalgcode, k.u_itemgrp, k.salunitmsr as uom,case when bb.pcs>0 then cast(bb.pcs as int) else cast(k.salpackun as int) end as box,case when CHARINDEX('CMS',k.u_length)<=0 then cast(isnull(k.u_length,0) as float) else 0 end   as u_length, cast(case when CHARINDEX('CMS',k.u_width)<=0  then cast(isnull(k.u_width,0.0) as float) else 0.0 end as float)  as u_width, k.u_style,convert(decimal(11,2), k.U_SelPrice) as rate,cast((k.salpackun*k.u_mrp) as decimal(7,2)) as boxmrp, cast(k.u_mrp as decimal(7,2)) as mrp, k.U_Remarks, k.u_brand, k.invntitem,k.u_prntname,  " & vbCrLf _
            & "CASE when CHARINDEX('_',t1.U_Size)>0 then " & vbCrLf _
            & "CASE when   CHARINDEX('CMS',k.u_length)<=0 and cast(replace(k.u_length,'CMS','') as real)>0 and (rtrim(upper(k.u_itemgrp))in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0) then  RTRIM(k.u_width)+'inX'+rtrim(lTRIM(cast(cast(k.u_length as real)*1 as char(10)))) +'in' else  ltrim(convert(varchar,round(cast(replace(k.u_length,'CMS','') as real)/100,2)))+'mX' + rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end " & vbCrLf _
            & " else " & vbCrLf _
            & " CASE when   CHARINDEX('CMS',k.u_length)<=0 and cast(replace(k.u_length,'CMS','') as real)>0 and (rtrim(upper(k.u_itemgrp))in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  ltrim(convert(varchar,round(cast(replace(k.u_length,'CMS','') as real)/100,2)))+'mX' + rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end end as msize, " & vbCrLf _
            & "CASE when CHARINDEX('_',t1.U_Size)>0 then " & vbCrLf _
            & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(replace(k.u_length,'CMS','') as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'inX'+rtrim(lTRIM(cast(cast(k.u_length as real)*1 as char(10)))) +'in' else  replace(RTRIM(k.u_length),'S','X')+ rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end " & vbCrLf _
            & "else " & vbCrLf _
            & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(replace(k.u_length,'CMS','') as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  replace(RTRIM(k.u_length),'S','X')+ rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end end as msize2, " & vbCrLf _
            & "'MFD ' + substring(datename(m,GETDATE()),1,3) +' '+ CAST(DATEPART(YYYY,GETDATE()) as CHAR(4)) as mfd, " & vbCrLf _
            & " (left(convert(varchar,t0.docnum) + '00000',5)+left(convert(varchar,t0.cardcode) +'0000000000000',13) + left(convert(varchar,convert(decimal(16),k.U_SelPrice)) + '00000',5)) as barcode, " & vbCrLf _
            & " (ltrim(convert(varchar,t0.docnum)) + rtrim(convert(varchar,t0.cardcode))+ltrim(convert(varchar,convert(decimal(16),k.U_SelPrice)))) as barcode1,ltrim(convert(varchar,t0.docnum)) as barcode2,ltrim(convert(varchar,t0.docnum))+rtrim(t1.itemcode) as barcode3,ltrim(convert(varchar,t0.docentry))+rtrim(t1.itemcode) as drbarcode,bb.dhoti,bb.shirting,bb.suiting,bb.towel,bb.pcs as bompcs,k.u_subgrp6,k.color,convert(nvarchar(max),t1.text) as mtrstot,t1.u_catalogname,  " & vbCrLf _
            & " ltrim(rtrim(right(YEAR(t0.docdate),2)))+right('0'+rtrim(ltrim(MONTH(t0.docdate))),2)+rtrim(ltrim(convert(nvarchar(100),k.u_remarks))) as autocode, " & vbCrLf _
            & "cc.dhoti contdhoti,cc.shirting contshirting,cc.suiting contsuiting,cc.towel conttowel,cc.perfume,cc.belt,cc.rate contrate,cc.pcs contpcs " & vbCrLf _
            & "from " & Trim(mfil) & " as t1 " & vbCrLf _
            & "left join " & Trim(mfildet) & " as t0 on t0.docentry=t1.DocEntry " & vbCrLf _
            & "left join (select b.CardCode, CASE when len(rtrim(ltrim(u_salpricecode)))>0 and u_salpricecode IS not null then b.u_salpricecode else c.state end as state from OCRD b " & vbCrLf _
            & "            left join CRD1 c on c.CardCode=b.cardcode and (c.Address='Office' or c.Address='Ship') and c.AdresType='B') cr on cr.CardCode=t0.CardCode" & vbCrLf _
            & "left join barcodebom as bb on BB.code=t1.itemcode " & vbCrLf _
            & "left join contentbom as cc on cc.code=t1.itemcode " & vbCrLf _
            & "Inner Join " & vbCrLf _
            & "(select it.invntitem, t0.u_itemcode,t0.u_itemname as mainname,t1.u_catalgcode,t0.u_itemgrp,it.salunitmsr,it.salpackun,t0.u_size,tz.u_length,tz.u_width,case when t0.u_style IS null then ISNULL(t0.u_style,'') else  t0.u_style end u_style,case when t2.u_state='Other' then 'OS' else t2.U_State end u_state,t2.U_SelPrice,t2.u_mrp,convert(nvarchar(max),t1.U_Remarks) u_remarks,t1.u_brand,it.u_subgrp5 as color,it.u_subgrp6,t1.u_prntname from [@INS_PLM1] as t1 " & vbCrLf _
            & "Inner join [@INS_OPLM] as t0 on t0.DocEntry=t1.DocEntry " & vbCrLf _
            & "Inner join [@INS_PLM2] as t2 on t2.DocEntry=t1.docentry and t2.U_RowID=t1.lineid" & vbCrLf _
            & "Inner join [@INCM_SZE1] as tz on tz.U_Name=t0.u_size" & vbCrLf _
            & "Inner join OITM as it on it.ItemCode=t0.U_ItemCode where t0.U_ItemCode is not null " & vbCrLf _
            & " group by it.invntitem, t0.u_itemcode,t0.u_itemname,t1.u_catalgcode,t0.u_itemgrp,it.salunitmsr,it.salpackun,t0.u_size,tz.u_length,tz.u_width,case when t0.u_style IS null then ISNULL(t0.u_style,'') else  t0.u_style end ,case when t2.u_state='Other' then 'OS' else t2.U_State end ,t2.U_SelPrice,t2.u_mrp,convert(nvarchar(max),t1.U_Remarks),t1.u_brand,it.u_subgrp5,it.u_subgrp6,t1.u_prntname " & vbCrLf _
            & ") as k on k.u_catalgcode=t1.U_CatalogName and k.u_itemcode=t1.ItemCode  and k.U_State=cr.state" & vbCrLf
        If Trim(cmbprnon.Text) = "INV DRAFT" Then
            msql = msql & " where t0.docentry=" & Val(txtbno.Text) & " and t1.TreeType<>'I'  and datepart(mm,t0.docdate)=" & Val(cmbmont.Text) & " and datepart(yyyy,t0.docdate)=" & Val(cmbyr.Text) & vbCrLf
        Else
            msql = msql & " where t0.docnum=" & Val(txtbno.Text) & " and t1.TreeType<>'I'  and datepart(mm,t0.docdate)=" & Val(cmbmont.Text) & " and datepart(yyyy,t0.docdate)=" & Val(cmbyr.Text) & vbCrLf
        End If

        'and k.U_Size=t1.U_Size 
        '& "left join CRD1 as cr on cr.CardCode=t0.CardCode and (cr.Address='Office' or cr.Address='Ship') and cr.AdresType='B' " & vbCrLf _

        'If Len(Trim(flx.get_TextMatrix(i, 3))) > 0 Then
        '    msql = msql & " and k.u_style='" & Trim(flx.get_TextMatrix(i, 3)) & "'" & vbCrLf
        'End If
        msql = msql & " order by t1.linenum"




        'If Len(Trim(flx.get_TextMatrix(i, 3))) > 0 Then
        '    msql = msql & " and k.u_style='" & Trim(flx.get_TextMatrix(i, 3)) & "'" & vbCrLf
        'End If
        'msql = msql & " order by t1.linenum"



        Dim crCMD As New OleDb.OleDbCommand(msql, con)
        'Dim CMD2 As New SqlClient.SqlCommand("DELETE FROM BPORD WHERE BNO=" & Val(txtbno.Text), con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If



        Try
            'crCMD.ExecuteNonQuery()
            '    'TRANS.Commit()
            ' MsgBox("Deleted")
            crCMD.ExecuteNonQuery()
            crCMD.Dispose()
            '    'CMD2.Dispose()
        Catch ex As Exception
            '    'TRANS.Rollback()
            MsgBox(ex.Message)
            crCMD.Dispose()
            '    'CMD2.Dispose()
        End Try

        ''TRANS.Commit()
        'crCMD.Dispose()






    End Sub


    '   Private Function BARNEW()

    '       Dim TXTBNO As Integer
    '       Dim SNO As Integer


    '       If MsgBox("PRINT ALL STOCK", vbCritical + vbYesNo) = vbYes Then

    '           With FLX
    '               TXTBNO = 0
    '               SNO = 1
    'Open "c:\barcodE.TXT" For Output As #1
    '     Print #1, Tab(0); "^XA"
    '     Print #1, Tab(0); "^PRC"
    '     Print #1, Tab(0); "^LH0,0^FS"
    '     Print #1, Tab(0); "^LL160"
    '     Print #1, Tab(0); "^MD25"
    '     Print #1, Tab(0); "^MNY"
    '     Print #1, Tab(0); "^LH0,0^FS"
    '               For i = 1 To .Rows - 1
    '                   'k = 1
    '                   If Len(FLX.TextMatrix(i, 6)) > 0 Then
    '                       If Trim(.TextMatrix(i, 4)) = "NOS" Then

    '                           If Val(.TextMatrix(i, 5)) > 0 Then
    '                               For k = 1 To Val(.TextMatrix(i, 5))

    '      Print #1, Tab(0); "^FO"; Trim(STR(80 + Val(TXTBNO))); ",43^A0N,24,43^CI13^FR^FD"; Trim(.TextMatrix(i, 1)); "^FS"
    '      Print #1, Tab(0); "^BY2,3.0^FO"; Trim(STR(47 + Val(TXTBNO))); ",77^BCN,45,N,Y,N^FR^FD>:"; Trim(.TextMatrix(i, 1)); "^FS"
    '                                   If InStr(UCase(Trim(.TextMatrix(i, 3))), "X") > 0 Then
    '       Print #1, Tab(0); "^FO"; Trim(STR(30 + Val(TXTBNO))); ",138^A0N,20,23^CI13^FR^FD"; Trim(.TextMatrix(i, 2)); "-"; Trim(.TextMatrix(i, 3)); "^FS"
    '                                   Else
    '       Print #1, Tab(0); "^FO"; Trim(STR(30 + Val(TXTBNO))); ",138^A0N,20,23^CI13^FR^FD"; Trim(.TextMatrix(i, 2)); "^FS"
    '                                   End If

    '                                   TXTBNO = Val(TXTBNO) + 400
    '                                   If SNO = 2 Then
    '       Print #1, Tab(0); "^PQ1,0,0,N"
    '       Print #1, Tab(0); "^XZ"
    '       Print #1, Tab(0); "^XA"
    '       Print #1, Tab(0); "^PRC"
    '       Print #1, Tab(0); "^LH0,0^FS"
    '       Print #1, Tab(0); "^LL160"
    '       Print #1, Tab(0); "^MD25"
    '       Print #1, Tab(0); "^MNY"
    '       Print #1, Tab(0); "^LH0,0^FS"
    '                                       SNO = 0
    '                                       TXTBNO = 0
    '                                   End If
    '                                   SNO = SNO + 1
    '                               Next k
    '                           End If
    '                       Else
    '                           If Val(.TextMatrix(i, 5)) > 0 Then
    '                               For j = 1 To 1
    '                                   '        Print #1, Tab(0); "^FO"; Trim(STR(21 + Val(TXTBNO))); ",28^A0N,25,43^CI13^FR^FD"; Trim(.TextMatrix(i, 1)); "^FS"
    '                                   '        Print #1, Tab(0); "^BY1,3.0^FO"; Trim(STR(47 + Val(TXTBNO))); ",59^BCN,45,N,Y,N^FR^FD>:"; Trim(.TextMatrix(i, 1)); "^FS"
    '                                   '
    '       Print #1, Tab(0); "^FO"; Trim(STR(80 + Val(TXTBNO))); ",43^A0N,24,43^CI13^FR^FD"; Trim(.TextMatrix(i, 1)); "^FS"
    '       Print #1, Tab(0); "^BY2,3.0^FO"; Trim(STR(47 + Val(TXTBNO))); ",77^BCN,45,N,Y,N^FR^FD>:"; Trim(.TextMatrix(i, 1)); "^FS"
    '                                   If InStr(UCase(Trim(.TextMatrix(i, 3))), "X") > 0 Then
    '        Print #1, Tab(0); "^FO"; Trim(STR(30 + Val(TXTBNO))); ",138^A0N,20,23^CI13^FR^FD"; Trim(.TextMatrix(i, 2)); "-"; Trim(.TextMatrix(i, 3)); "^FS"
    '                                   Else
    '        Print #1, Tab(0); "^FO"; Trim(STR(30 + Val(TXTBNO))); ",138^A0N,20,23^CI13^FR^FD"; Trim(.TextMatrix(i, 2)); "^FS"
    '                                   End If

    '                                   TXTBNO = Val(TXTBNO) + 400
    '                                   If SNO = 2 Then
    '        Print #1, Tab(0); "^PQ1,0,0,N"
    '        Print #1, Tab(0); "^XZ"
    '        Print #1, Tab(0); "^XA"
    '        Print #1, Tab(0); "^PRC"
    '        Print #1, Tab(0); "^LH0,0^FS"
    '        Print #1, Tab(0); "^LL160"
    '        Print #1, Tab(0); "^MD25"
    '        Print #1, Tab(0); "^MNY"
    '        Print #1, Tab(0); "^LH0,0^FS"
    '                                       SNO = 0
    '                                       TXTBNO = 0
    '                                   End If
    '                                   SNO = SNO + 1
    '                               Next j
    '                           End If
    '                       End If
    '                   End If
    '               Next i
    '           End With
    '   Print #1, Tab(0); "^PQ1,0,0,N"
    '   Print #1, Tab(0); "^XZ"
    '   Close #1

    '       Else
    '           TXTBNO = 0
    '           SNO = 1
    '           'With FLX
    '   Open "c:\barcodE.TXT" For Output As #1
    '     Print #1, Tab(0); "^XA"
    '     Print #1, Tab(0); "^PRC"
    '     Print #1, Tab(0); "^LH0,0^FS"
    '     Print #1, Tab(0); "^LL160"
    '     Print #1, Tab(0); "^MD25"
    '     Print #1, Tab(0); "^MNY"
    '     Print #1, Tab(0); "^LH0,0^FS"



    '           If Val(TXTQTY.Text) > 0 Then
    '               For k = 1 To Val(TXTQTY.Text)

    '      Print #1, Tab(0); "^FO"; Trim(STR(80 + Val(TXTBNO))); ",43^A0N,24,43^CI13^FR^FD"; Trim(TXTCODE.Text); "^FS"
    '      Print #1, Tab(0); "^BY2,3.0^FO"; Trim(STR(47 + Val(TXTBNO))); ",77^BCN,45,N,Y,N^FR^FD>:"; Trim(TXTCODE.Text); "^FS"

    '                   '       Print #1, Tab(0); "^FO"; Trim(STR(21 + Val(TXTBNO))); ",28^A0N,25,43^CI13^FR^FD"; Trim(TXTCODE.Text); "^FS"
    '                   '       Print #1, Tab(0); "^BY1,3.0^FO"; Trim(STR(47 + Val(TXTBNO))); ",59^BCN,45,N,Y,N^FR^FD>:"; Trim(TXTCODE.Text); "^FS"
    '                   '
    '                   TXTBNO = Val(TXTBNO) + 400
    '                   If SNO = 2 Then
    '       Print #1, Tab(0); "^PQ1,0,0,N"
    '       Print #1, Tab(0); "^XZ"
    '       Print #1, Tab(0); "^XA"
    '       Print #1, Tab(0); "^PRC"
    '       Print #1, Tab(0); "^LH0,0^FS"
    '       Print #1, Tab(0); "^LL160"
    '       Print #1, Tab(0); "^MD25"
    '       Print #1, Tab(0); "^MNY"
    '       Print #1, Tab(0); "^LH0,0^FS"
    '                       SNO = 0
    '                       TXTBNO = 0
    '                   End If
    '                   SNO = SNO + 1
    '               Next k
    '           End If
    '    Print #1, Tab(0); "^PQ1,0,0,N"
    '       Print #1, Tab(0); "^XZ"
    '    Close #1

    '       End If

    '       '  If MsgBox("print usp port", vbCritical + vbYesNo) = vbYes Then
    '       '       Dim X As String
    '       '       'FileCopy "C:\BARCOD.PRN", "C:\BARCOD.TXT"
    '       '       X = ShellPrint(Me.hwnd, "C:\Barcod.TXT")
    '       '
    '       '     If X <> vbNullString Then
    '       '        MsgBox X
    '       '     End If
    '       '
    '       '
    '       '    Else

    '       If MsgBox("PRINT", vbCritical + vbYesNo) = vbYes Then
    '           Shell("command.com /c TYPE " & "c:\barcodE.TXT>PRN", vbHide)
    '       Else
    '           Shell("command.com /c edit " & "c:\barcodE.TXT", vbMaximizedFocus)
    '       End If
    '       'End If


    '   End Function



    Private Sub cmdok_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.ClickButtonArea
        'Call loaddata()
    End Sub

    Private Sub cmdexit_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.ClickButtonArea
        Me.Close()
    End Sub

    Private Sub cmdprint_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdprint.ClickButtonArea
        'Call barprn()
        If Val(txtstickcol.Text) > 1 Then
            Call barprnsapmulti2()
        Else

            Call barprnsap1()
        End If

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
        If flx.Col >= 4 And flx.Col <= 5 Then
            editflx(flx, e.keyAscii, cmdok)
        End If
    End Sub

    Private Sub txtbno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtbno.KeyDown

    End Sub

    Private Sub txtbno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtbno.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Call loadno()
            If Val(cmbmont.Text) > 0 And Val(cmbyr.Text) > 0 Then
                'Call loaddatadet()
                'Call crtmptab()
                Call crtmptabst()
                Call loaddatadet()
                lblrec.Text = flx.Rows - 1
            Else
                MsgBox("Pls select month and year")
            End If

        End If
    End Sub
    Private Sub loadno()

        If chksr.Checked = True Then
            mfil = "(select t0.docentry,t2.u_itemcode as itemcode,t0.itemcode remarks,t2.U_Size, t1.u_catalgcode,t1.u_itemname as u_catalogname,t0.linenum,t0.freetxt,t0.quantity,CASE when t3.InvntItem='N' then 'S' else '' end treetype,'' as text from srbardet t0 " & vbCrLf _
                & "left join [@INS_PLM1] t1 on  convert(nvarchar(40),t1.U_Remarks)= t0.itemcode " & vbCrLf _
                & "left join [@INS_OPLM] t2 on t2.DocEntry=t1.DocEntry " & vbCrLf _
                & "left join OITM t3 on t3.ItemCode=t2.U_ItemCode " & vbCrLf _
                & " where t1.U_Lock<>'Y')"
            mfildet = "(select docentry,docnum,docdate,CardCode,cardname from srbardet group by docentry,docnum,docdate,CardCode,cardname) "

            'mfil = "V_sampinv1"
            'mfildet = "v_sampOinv"
        Else


            If Trim(cmbprnon.Text) = "SALES" Then
                mfil = "INV1"
                mfildet = "OINV"
            ElseIf Trim(cmbprnon.Text) = "DATE ORDER" Then
                mfil = "DLN1"
                mfildet = "ODLN"
            ElseIf Trim(cmbprnon.Text) = "INV DRAFT" Then
                mfil = "DRF1"
                mfildet = "ODRF"
            ElseIf Trim(cmbprnon.Text) = "SAMPLE" Then
                mfil = "V_sampinv1"
                mfildet = "v_sampOinv"
            End If



            msql = "select b.DocNum,b.DocDate, b.pIndicator,DATEPART(mm,b.DocDate) as mkmon,DATEPART(yyyy,b.docdate) as yr from " & Trim(mfildet) & " b with (nolock) left join OFPR p on p.Indicator=b.PIndicator where b.DocNum=" & Val(txtbno.Text) & " and p.Code='" & Trim(cmbyear.Text) & "'"

            'Header
            Dim CMD As New OleDb.OleDbCommand(msql, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            Dim DR As OleDb.OleDbDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                While DR.Read
                    cmbmont.Text = DR.Item("mkmon")
                    cmbyr.Text = DR.Item("yr")
                End While
            End If
            DR.Close()
            CMD.Dispose()
        End If


    End Sub
    Private Sub txtbno_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtbno.KeyUp
        If e.KeyCode = Keys.F12 Then
            Call loadhead()
            If flxc.Visible = False Then flxc.Visible = True
        End If
    End Sub

    Private Sub flx_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flx.Enter

    End Sub

    Private Sub flxc_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxc.Enter

    End Sub

    Private Sub flxc_KeyPressEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyPressEvent) Handles flxc.KeyPressEvent
        If e.keyAscii = 13 Then
            flxc.Visible = False
            txtdocno.Text = flxc.get_TextMatrix(flxc.Row, 0)
            cmbcomp.Text = flxc.get_TextMatrix(flxc.Row, 1)
            cmbtype.Text = flxc.get_TextMatrix(flxc.Row, 2)
            cmbprntype.Text = flxc.get_TextMatrix(flxc.Row, 3)
            cmbprnon.Text = flxc.get_TextMatrix(flxc.Row, 4)
            txtstickcol.Text = flxc.get_TextMatrix(flxc.Row, 5)
            txtrow.Text = flxc.get_TextMatrix(flxc.Row, 6)
            txtcol.Text = flxc.get_TextMatrix(flxc.Row, 7)
            txtprint.Text = flxc.get_TextMatrix(flxc.Row, 8)

            txtbno.Focus()
        Else
            If e.keyAscii <> 27 Then
                searchflx(flxc, e.keyAscii, 0)
            Else
                flxc.Visible = False
                'flx.Row = flx.Row
                'flx.Col = 0
                'flx.Focus()
                txtbno.Focus()
            End If

        End If
    End Sub
    Private Sub barprnsap1()
        Dim i, k As Integer
        Dim sno, mdatasno As Integer
        Dim mpurrate As Double
        Dim ttrue, mktt, mkt2 As Boolean
        mtxtbno = 0
        sno = 1

        If chksr.Checked = True Then
            mfil = "(select t0.docentry,t2.u_itemcode as itemcode,t0.itemcode remarks,t2.U_Size, t1.u_catalgcode,t1.u_itemname as u_catalogname,t0.linenum,t0.freetxt,t0.quantity,CASE when t3.InvntItem='N' then 'S' else '' end treetype,'' as text from srbardet t0 " & vbCrLf _
                & "left join [@INS_PLM1] t1 on  convert(nvarchar(40),t1.U_Remarks)= t0.itemcode " & vbCrLf _
                & " left join [@INS_OPLM] t2 on t2.DocEntry=t1.DocEntry " & vbCrLf _
                & "left join OITM t3 on t3.ItemCode=t2.U_ItemCode " & vbCrLf _
                & " where t1.U_Lock<>'Y')"
            mfildet = "(select docentry,docnum,docdate,CardCode,cardname from srbardet group by docentry,docnum,docdate,CardCode,cardname) "

            'mfil = "V_sampinv1"
            'mfildet = "v_sampOinv"
        Else

            If Trim(cmbprnon.Text) = "SALES" Then
                mfil = "INV1"
                mfildet = "OINV"
            ElseIf Trim(cmbprnon.Text) = "DATE ORDER" Then
                mfil = "DLN1"
                mfildet = "ODLN"
            ElseIf Trim(cmbprnon.Text) = "INV DRAFT" Then
                mfil = "DRF1"
                mfildet = "ODRF"
            ElseIf Trim(cmbprnon.Text) = "SAMPLE" Then
                mfil = "V_sampinv1"
                mfildet = "v_sampOinv"
            End If

        End If


        'Call crtmptab()

        '**** tmp

        ' msql4 = " IF OBJECT_ID(N'tempdb..#barTemp') IS NOT NULL " & vbCrLf _
        '     & " BEGIN " & vbCrLf _
        '     & "  DROP TABLE #barTemp " & vbCrLf _
        '     & " End "
        ' Dim dCMD As New OleDb.OleDbCommand(msql4, con)
        ' 'Dim CMD2 As New SqlClient.SqlCommand("DELETE FROM BPORD WHERE BNO=" & Val(txtbno.Text), con)
        ' If con.State = ConnectionState.Closed Then
        '     con.Open()
        ' End If

        ' dCMD.ExecuteNonQuery()


        ' Try
        '     'TRANS.Commit()
        '     ' MsgBox("Deleted")
        '     dCMD.Dispose()
        '     'CMD2.Dispose()
        ' Catch ex As Exception
        '     'TRANS.Rollback()
        '     MsgBox(ex.Message)
        '     dCMD.Dispose()
        '     'CMD2.Dispose()
        ' End Try

        ' 'TRANS.Commit()
        ' dCMD.Dispose()


        ' msql5 = "CREATE TABLE ##bartemp( " & vbCrLf _
        ' & " docentry int NULL, " & vbCrLf _
        ' & " docnum int NULL, " & vbCrLf _
        ' & "linenum int NOT NULL, " & vbCrLf _
        ' & "docdate datetime NULL, " & vbCrLf _
        ' & " cardcode nvarchar(15) NULL, " & vbCrLf _
        ' & "cardname nvarchar(100) NULL, " & vbCrLf _
        '& " itemcode nvarchar(20) NULL, " & vbCrLf _
        '& " quality nvarchar(100) NULL, " & vbCrLf _
        '& " u_size nvarchar(100) NULL, " & vbCrLf _
        '& "quantity decimal(16, 0) NULL, " & vbCrLf _
        '& "boxqty decimal(27, 0) NULL, " & vbCrLf _
        '& "state nvarchar(3) NULL, " & vbCrLf _
        '& "u_catalgcode nvarchar(50) NULL, " & vbCrLf _
        '& "u_itemgrp nvarchar(30) NULL, " & vbCrLf _
        '& "uom nvarchar(20) NULL, " & vbCrLf _
        '& " box int NULL," & vbCrLf _
        '& " u_length real NULL, " & vbCrLf _
        '& "u_width int NOT NULL, " & vbCrLf _
        '& "u_style nvarchar(30) NULL, " & vbCrLf _
        '& "rate decimal(11, 2) NULL, " & vbCrLf _
        '& " boxmrp decimal(7, 2) NULL, " & vbCrLf _
        '& " mrp decimal(7, 2) NULL," & vbCrLf _
        '& " U_Remarks ntext NULL, " & vbCrLf _
        '& " u_brand nvarchar(50) NULL, " & vbCrLf _
        '& " invntitem char(1) NULL, " & vbCrLf _
        '& " msize nvarchar(45) NULL, " & vbCrLf _
        '& " msize2 nvarchar(4000) NULL, " & vbCrLf _
        '& " mfd nvarchar(12) NULL, " & vbCrLf _
        '& " barcode varchar(23) NULL, " & vbCrLf _
        '& " barcode1 varchar(90) NULL, " & vbCrLf _
        '& " barcode2 varchar(30) NULL, " & vbCrLf _
        '& " barcode3 nvarchar(50) NULL, " & vbCrLf _
        '& " drbarcode nvarchar(50) NULL, " & vbCrLf _
        '& " dhoti nchar(30) NULL, " & vbCrLf _
        '& " shirting nchar(30) NULL, " & vbCrLf _
        '& " suiting nchar(30) NULL, " & vbCrLf _
        '& " towel nchar(30) NULL, " & vbCrLf _
        '& " u_subgrp6 nvarchar(100) NULL, " & vbCrLf _
        '& "color nvarchar(100) NULL, " & vbCrLf _
        '& " mtrstot ntext NULL," & vbCrLf _
        '& " u_catalogname nvarchar(50) NULL " & vbCrLf _
        '& ") "

        ' Dim crdCMD As New OleDb.OleDbCommand(msql5, con)
        ' 'Dim CMD2 As New SqlClient.SqlCommand("DELETE FROM BPORD WHERE BNO=" & Val(txtbno.Text), con)
        ' If con.State = ConnectionState.Closed Then
        '     con.Open()
        ' End If

        ' crdCMD.ExecuteNonQuery()






        ' msql = " insert into #barTemp select t0.docentry, t0.docnum,t1.linenum,t0.docdate,t0.cardcode,t0.cardname,t1.itemcode,case when t1.freetxt is null or len(rtrim(t1.freetxt))=0  then t1.u_catalogname else t1.freetxt  end  as quality,t1.u_size,convert(decimal(16),t1.quantity) as quantity,floor(convert(decimal(16),t1.quantity)/cast(k.salpackun as int)) as boxqty,cr.state, " & vbCrLf _
        '     & "k.u_catalgcode, k.u_itemgrp, k.salunitmsr as uom,cast(k.salpackun as int) as box, case when CHARINDEX('CMS',k.u_length)<=0 then cast(isnull(k.u_length,0) as real) else 0 end  as u_length, cast(case when CHARINDEX('CMS',k.u_width)<=0  then isnull(k.u_width,0.0) else 0.0 end as real) as u_width, k.u_style,convert(decimal(11,2), k.U_SelPrice) as rate,cast((k.salpackun*k.u_mrp) as decimal(7,2)) as boxmrp, cast(k.u_mrp as decimal(7,2)) as mrp, k.U_Remarks, k.u_brand, k.invntitem, " & vbCrLf _
        '     & " ltrim(rtrim(right(YEAR(t0.docdate),2)))+right('0'+rtrim(ltrim(MONTH(t0.docdate))),2)+rtrim(ltrim(convert(nvarchar(100),k.u_remarks))) as autocode," & vbCrLf _
        '     & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  RTRIM(k.u_length)  end as msize," & vbCrLf _
        '     & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  replace(RTRIM(k.u_length),'S','X')+ rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end as msize2," & vbCrLf _
        '     & "'MFD ' + substring(datename(m,GETDATE()),1,3) +' '+ CAST(DATEPART(YYYY,GETDATE()) as CHAR(4)) as mfd, " & vbCrLf _
        '     & " (left(convert(varchar,t0.docnum) + '00000',5)+left(convert(varchar,t0.cardcode) +'0000000000000',13) + left(convert(varchar,convert(decimal(16),k.U_SelPrice)) + '00000',5)) as barcode, " & vbCrLf _
        '     & " (ltrim(convert(varchar,t0.docnum)) + rtrim(convert(varchar,t0.cardcode))+ltrim(convert(varchar,convert(decimal(16),k.U_SelPrice)))) as barcode1,ltrim(convert(varchar,t0.docnum)) as barcode2,ltrim(convert(varchar,t0.docnum))+rtrim(t1.itemcode) as barcode3,ltrim(convert(varchar,t0.docentry))+rtrim(t1.itemcode) as drbarcode,bb.dhoti,bb.shirting,bb.suiting,bb.towel,k.u_subgrp6,k.color,t1.text as mtrstot,t1.u_catalogname  " & vbCrLf _
        '     & "from " & Trim(mfil) & " as t1 " & vbCrLf _
        '     & "left join " & Trim(mfildet) & " as t0 on t0.docentry=t1.DocEntry " & vbCrLf _
        '     & "left join CRD1 as cr on cr.CardCode=t0.CardCode and (cr.Address='Office' or cr.Address='Ship') and cr.AdresType='B' " & vbCrLf _
        '     & "left join barcodebom as bb on BB.code=t1.itemcode " & vbCrLf _
        '     & "Inner Join " & vbCrLf _
        '     & "(select it.invntitem, t0.u_itemcode,t0.u_itemname as mainname,t1.u_catalgcode,t0.u_itemgrp,it.salunitmsr,it.salpackun,t0.u_size,tz.u_length,tz.u_width,case when t0.u_style IS null then ISNULL(t0.u_style,'') else  t0.u_style end u_style,case when t2.u_state='Other' then 'OS' else t2.U_State end u_state,t2.U_SelPrice,t2.u_mrp,t1.U_Remarks,t1.u_brand,it.u_subgrp5 as color,it.u_subgrp6 from [@INS_PLM1] as t1 " & vbCrLf _
        '     & "Inner join [@INS_OPLM] as t0 on t0.DocEntry=t1.DocEntry " & vbCrLf _
        '     & "Inner join [@INS_PLM2] as t2 on t2.DocEntry=t1.docentry and t2.U_RowID=t1.lineid" & vbCrLf _
        '     & "Inner join [@INCM_SZE1] as tz on tz.U_Name=t0.u_size" & vbCrLf _
        '     & "Inner join OITM as it on it.ItemCode=t0.U_ItemCode where t0.U_ItemCode is not null) as k on k.u_catalgcode=t1.U_CatalogName and k.u_itemcode=t1.ItemCode and k.U_Size=t1.U_Size  and k.U_State=cr.state" & vbCrLf
        ' If Trim(cmbprnon.Text) = "INV DRAFT" Then
        '     msql = msql & " where t0.docentry=" & Val(txtbno.Text) & " and t1.TreeType<>'I'" & vbCrLf
        ' Else
        '     msql = msql & " where t0.docnum=" & Val(txtbno.Text) & " and t1.TreeType<>'I' " & vbCrLf
        ' End If

        ' 'If Len(Trim(flx.get_TextMatrix(i, 3))) > 0 Then
        ' '    msql = msql & " and k.u_style='" & Trim(flx.get_TextMatrix(i, 3)) & "'" & vbCrLf
        ' 'End If
        ' msql = msql & " order by t1.linenum"



        ' Dim crCMD As New OleDb.OleDbCommand(msql, con)
        ' 'Dim CMD2 As New SqlClient.SqlCommand("DELETE FROM BPORD WHERE BNO=" & Val(txtbno.Text), con)
        ' If con.State = ConnectionState.Closed Then
        '     con.Open()
        ' End If

        ' 'crCMD.ExecuteNonQuery()

        ' Try
        '     crCMD.ExecuteNonQuery()
        '     '    'TRANS.Commit()
        '     ' MsgBox("Deleted")
        '     'crCMD.Dispose()
        '     '    'CMD2.Dispose()
        ' Catch ex As Exception
        '     '    'TRANS.Rollback()
        '     MsgBox(ex.Message)
        '     '    crCMD.Dispose()
        '     '    'CMD2.Dispose()
        ' End Try

        ' ''TRANS.Commit()
        ' 'crCMD.Dispose()






        '*** tmep


        Dim dir As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "sbarcodE.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
        If chkprndir.Checked = True Then
            FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        Else
            FileOpen(1, mdir, OpenMode.Output)
        End If
        'FileOpen(1, mdir, OpenMode.Output)

        'FileOpen(1, "c:\sbarcode.txt", OpenMode.Output, OpenAccess.Write)
        'FileOpen(1, "LPT1", OpenMode.Output)
        ' FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)

        'If Trim(cmbprnon.Text) = "SALES" Then
        '    mfil = "INV1"
        '    mfildet = "OINV"
        'ElseIf Trim(cmbprnon.Text) = "DATE ORDER" Then
        '    mfil = "DLN1"
        '    mfildet = "ODLN"
        'ElseIf Trim(cmbprnon.Text) = "INV DRAFT" Then
        '    mfil = "DRF1"
        '    mfildet = "ODRF"
        'ElseIf Trim(cmbprnon.Text) = "SAMPLE" Then
        '    mfil = "V_sampinv1"
        '    mfildet = "v_sampOinv"
        'End If


        For i = 1 To flx.Rows - 1

            If Len(Trim(flx.get_TextMatrix(i, 0))) > 0 Then


                msql = "SELECT  [docentry],[date],[linenum],[headtype],[firstdet],[lrow],[lcol],[fontdet],[ldata],[secdet] FROM bardet where docentry=" & Val(txtdocno.Text) & " and headtype='H' order by docentry,linenum"

                'Header
                Dim CMD As New OleDb.OleDbCommand(msql, con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                Dim DR As OleDb.OleDbDataReader
                DR = CMD.ExecuteReader
                If DR.HasRows = True Then
                    While DR.Read
                        PrintLine(1, TAB(0), DR.Item("firstdet"))
                    End While
                End If
                DR.Close()
                CMD.Dispose()




                '    msql = "select top 1 t0.docentry, t0.docnum,t1.linenum,t0.docdate,t0.cardcode,t0.cardname,t1.itemcode,case when t1.freetxt is null or len(rtrim(t1.freetxt))=0  then t1.u_catalogname else t1.freetxt  end  as quality,t1.u_size,convert(decimal(16),t1.quantity) as quantity,floor(convert(decimal(16),t1.quantity)/cast(k.salpackun as int)) as boxqty,cr.state, " & vbCrLf _
                '& "k.u_catalgcode, k.u_itemgrp, k.salunitmsr as uom,cast(k.salpackun as int) as box, case when CHARINDEX('CMS',k.u_length)<=0 then cast(isnull(k.u_length,0) as real) else 0 end  as u_length, cast(case when CHARINDEX('CMS',k.u_width)<=0  then isnull(k.u_width,0.0) else 0.0 end as real) as u_width, k.u_style,convert(decimal(11,2), k.U_SelPrice) as rate,cast((k.salpackun*k.u_mrp) as decimal(7,2)) as boxmrp, cast(k.u_mrp as decimal(7,2)) as mrp, k.U_Remarks, k.u_brand, k.invntitem, " & vbCrLf _
                '& "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  RTRIM(k.u_length)  end as msize," & vbCrLf _
                '& "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  replace(RTRIM(k.u_length),'S','X')+ rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end as msize2," & vbCrLf _
                '& "'MFD ' + substring(datename(m,GETDATE()),1,3) +' '+ CAST(DATEPART(YYYY,GETDATE()) as CHAR(4)) as mfd, " & vbCrLf _
                '& " (left(convert(varchar,t0.docnum) + '00000',5)+left(convert(varchar,t0.cardcode) +'0000000000000',13) + left(convert(varchar,convert(decimal(16),k.U_SelPrice)) + '00000',5)) as barcode, " & vbCrLf _
                '& " (ltrim(convert(varchar,t0.docnum)) + rtrim(convert(varchar,t0.cardcode))+ltrim(convert(varchar,convert(decimal(16),k.U_SelPrice)))) as barcode1,ltrim(convert(varchar,t0.docnum)) as barcode2,ltrim(convert(varchar,t0.docnum))+rtrim(t1.itemcode) as barcode3,ltrim(convert(varchar,t0.docentry))+rtrim(t1.itemcode) as drbarcode,bb.dhoti,bb.shirting,bb.suiting,bb.towel,k.u_subgrp6,k.color,t1.text as mtrstot   " & vbCrLf _
                '& "from " & Trim(mfil) & " as t1 " & vbCrLf _
                '& "left join " & Trim(mfildet) & " as t0 on t0.docentry=t1.DocEntry " & vbCrLf _
                '& "left join CRD1 as cr on cr.CardCode=t0.CardCode and (cr.Address='Office' or cr.Address='Ship') and cr.AdresType='B' " & vbCrLf _
                ' & "left join barcodebom as bb on BB.code=t1.itemcode " & vbCrLf _
                '& "Inner Join " & vbCrLf _
                '& "(select it.invntitem, t0.u_itemcode,t0.u_itemname as mainname,t1.u_catalgcode,t0.u_itemgrp,it.salunitmsr,it.salpackun,t0.u_size,tz.u_length,tz.u_width,case when t0.u_style IS null then ISNULL(t0.u_style,'') else  t0.u_style end u_style,case when t2.u_state='Other' then 'OS' else t2.U_State end u_state,t2.U_SelPrice,t2.u_mrp,t1.U_Remarks,t1.u_brand,it.u_subgrp5 as color,it.u_subgrp6 from [@INS_PLM1] as t1 " & vbCrLf _
                '& "Inner join [@INS_OPLM] as t0 on t0.DocEntry=t1.DocEntry " & vbCrLf _
                '& "Inner join [@INS_PLM2] as t2 on t2.DocEntry=t1.docentry and t2.U_RowID=t1.lineid" & vbCrLf _
                '& "Inner join [@INCM_SZE1] as tz on tz.U_Name=t0.u_size" & vbCrLf _
                '& "Inner join OITM as it on it.ItemCode=t0.U_ItemCode where t0.U_ItemCode is not null) as k on k.u_catalgcode=t1.U_CatalogName and k.u_itemcode=t1.ItemCode and k.U_Size=t1.U_Size  and k.U_State=cr.state" & vbCrLf
                '    If Trim(cmbprnon.Text) = "INV DRAFT" Then
                '        msql = msql & " where t0.docentry=" & Val(txtbno.Text) & " and t1.TreeType<>'I' and t1.u_catalogname='" & Trim(flx.get_TextMatrix(i, 1)) & "' and  k.u_size='" & Trim(flx.get_TextMatrix(i, 13)) & "'" & vbCrLf
                '    Else
                '        msql = msql & " where t0.docnum=" & Val(txtbno.Text) & " and t1.TreeType<>'I' and t1.u_catalogname='" & Trim(flx.get_TextMatrix(i, 1)) & "' and  k.u_size='" & Trim(flx.get_TextMatrix(i, 13)) & "'" & vbCrLf
                '    End If

                '    If Len(Trim(flx.get_TextMatrix(i, 3))) > 0 Then
                '        msql = msql & " and k.u_style='" & Trim(flx.get_TextMatrix(i, 3)) & "'" & vbCrLf
                '    End If
                '    msql = msql & " order by t1.linenum"


                'tmptab
                msql = "select * from barTemp " & vbCrLf
                If Trim(cmbprnon.Text) = "INV DRAFT" Then
                    msql = msql & " where docentry=" & Val(txtbno.Text) & "and u_catalogname='" & Trim(flx.get_TextMatrix(i, 1)) & "' and  u_size='" & Trim(flx.get_TextMatrix(i, 13)) & "' and datepart(mm,docdate)=" & Val(cmbmont.Text) & " and datepart(yyyy,docdate)=" & Val(cmbyr.Text) & vbCrLf
                Else
                    msql = msql & " where docnum=" & Val(txtbno.Text) & "  and u_catalogname='" & Trim(flx.get_TextMatrix(i, 1)) & "' and  u_size='" & Trim(flx.get_TextMatrix(i, 13)) & "' and datepart(mm,docdate)=" & Val(cmbmont.Text) & " and datepart(yyyy,docdate)=" & Val(cmbyr.Text) & vbCrLf
                End If

                If Len(Trim(flx.get_TextMatrix(i, 3))) > 0 Then
                    msql = msql & " and u_style='" & Trim(flx.get_TextMatrix(i, 3)) & "'" & vbCrLf
                End If

                If Len(Trim(flx.get_TextMatrix(i, 22))) > 0 Then
                    msql = msql & " and color='" & Trim(flx.get_TextMatrix(i, 22)) & "'" & vbCrLf
                End If
                msql = msql & " order by linenum"

                'and  isnull(k.U_Style,'')=isnull(t1.U_Style,'')

                '**tmpedb end

                msql2 = "SELECT  [docentry],[date],[linenum],[headtype],[firstdet],[lrow],[lcol],[fontdet],[ldata],[secdet] FROM bardet where docentry=" & Val(txtdocno.Text) & " and headtype='D' order by docentry,linenum"



                'Detail
                Dim CMD1 As New OleDb.OleDbCommand(msql2, con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                Try
                    Dim DR1 As OleDb.OleDbDataReader
                    DR1 = CMD1.ExecuteReader
                    If DR1.HasRows = True Then
                        While DR1.Read
                            mkrow = Val(DR1.Item("lrow"))
                            If Len(Trim(DR1.Item("ldata"))) <= 0 Then
                                mkstr = Trim(DR1.Item("firstdet"))
                            End If
                            'If Val(txtrow.Text) > 0 Then

                            '    mkstr = mkstr & Trim(Str(mkrow))


                            'End If
                            'If Val(txtcol.Text) > 0 Then
                            '    mkstr = mkstr & "," & Trim(txtcol.Text)
                            'End If
                            'If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                            '    mkstr = mkstr & "," & LTrim(DR1.Item("fontdet"))
                            'End If



                            If Len(Trim(DR1.Item("ldata"))) > 0 Then
                                Dim cmd4 As New OleDb.OleDbCommand(msql, con)
                                If con.State = ConnectionState.Closed Then
                                    con.Open()
                                End If

                                Dim DR4 As OleDb.OleDbDataReader
                                DR4 = cmd4.ExecuteReader
                                If DR4.HasRows = True Then
                                    While DR4.Read

                                        'If Trim(flx.get_TextMatrix(i, 17)) = "$SUITING" Then
                                        '    MsgBox("TEST")
                                        'End If

                                        If Mid(Trim(DR1.Item("ldata")), 1, 1) = "@" Then
                                            If Mid(Trim(DR1.Item("ldata")), 1, 2) = "@$" Then
                                                ' MsgBox("test")
                                                If Trim(flx.get_TextMatrix(i, 19)) = "N" Then
                                                    mkt2 = True
                                                Else
                                                    mkt2 = False
                                                End If

                                            Else
                                                mkt2 = False

                                            End If
                                            If Replace(UCase(Trim(Mid(Trim(DR1.Item("ldata")), 2, 59))), "$", "") = "MRP" Or Replace(UCase(Trim(Mid(Trim(DR1.Item("ldata")), 2, 59))), "$", "") = "BOXMRP" Then
                                                If chkmrp.Checked = True Then
                                                    If IsDBNull(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", ""))) = False Then
                                                        If Len(Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))) > 0 Then
                                                            mkstr = Trim(DR1.Item("firstdet"))
                                                            If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                                mkstr = mkstr & "," & LTrim(DR1.Item("fontdet"))
                                                            End If
                                                            If Val(flx.get_TextMatrix(i, 5)) <> Val(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", ""))) Then
                                                                If UCase(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")) = "BOXMRP" Then
                                                                    mkstr = mkstr & Trim(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")))
                                                                Else
                                                                    mkstr = mkstr & Trim(flx.get_TextMatrix(i, 5))
                                                                End If
                                                            Else
                                                                mkstr = mkstr & Trim(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")))
                                                            End If

                                                            'mkstr = mkstr & Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))
                                                            If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                                                mkstr = mkstr & Trim(DR1.Item("secdet"))
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            Else
                                                If mkt2 = True Then
                                                    mktt = IsDBNull(DR4.Item(Mid(Trim(DR1.Item("ldata")), 3, 59)))
                                                    mdatasno = Len(Trim(IsDBNull(DR4.Item(Mid(Trim(DR1.Item("ldata")), 3, 59)))))
                                                Else
                                                    mktt = IsDBNull(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")))
                                                    mdatasno = Len(Trim(IsDBNull(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")))))

                                                End If

                                                'If Trim(flx.get_TextMatrix(i, 19)) = "N" Then
                                                '    If Mid(Trim(DR1.Item("ldata")), 2, 1) = "$" Then
                                                '        mktt = IsDBNull(DR4.Item(Mid(Trim(DR1.Item("ldata")), 3, 59)))
                                                '        mdatasno = Len(Trim(IsDBNull(DR4.Item(Mid(Trim(DR1.Item("ldata")), 3, 59)))))
                                                '    Else
                                                '        mktt = IsDBNull(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))
                                                '        mdatasno = Len(Trim(IsDBNull(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))))
                                                '    End If

                                                'Else
                                                '    mktt = IsDBNull(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))
                                                '    mdatasno = Len(Trim(IsDBNull(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))))
                                                'End If
                                                If mktt = False Then
                                                    If mdatasno > 0 Then
                                                        mkstr = Trim(DR1.Item("firstdet"))
                                                        If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                            mkstr = mkstr & "," & LTrim(DR1.Item("fontdet"))
                                                        End If
                                                        If UCase(Trim(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", ""))) = "BAR" Then
                                                            If Len(Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))) > 0 Then
                                                                mkstr = mkstr & Trim(DR4.Item(Mid(Trim(DR1.Item("u_remark")), 2, 59)))
                                                            Else
                                                                mkstr = mkstr & Trim(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")))
                                                            End If
                                                        Else
                                                            '*****GRP
                                                            If Len(Trim(flx.get_TextMatrix(i, 21))) > 0 Then
                                                                'mkstr = mkstr & Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59))) & "X" & Trim(flx.get_TextMatrix(i, 21))
                                                                If UCase(Trim(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", ""))) = "MSIZE" Then
                                                                    mkstr = mkstr & Trim(flx.get_TextMatrix(i, 2))
                                                                Else
                                                                    mkstr = mkstr & Trim(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")))
                                                                End If
                                                                'mkstr = mkstr & Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))
                                                                'mkstr = mkstr & Trim(flx.get_TextMatrix(i, 2))
                                                            Else
                                                                '****$$
                                                                If Trim(flx.get_TextMatrix(i, 19)) = "N" Then
                                                                    If Mid(Trim(DR1.Item("ldata")), 2, 1) = "$" Then
                                                                        mkstr = mkstr '& Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 3, 59)))
                                                                    Else
                                                                        mkstr = mkstr & Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))
                                                                    End If

                                                                Else
                                                                    'If Mid(Trim(DR1.Item("ldata")), 2, 1) <> "$" Then
                                                                    mkstr = mkstr & Trim(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")))
                                                                    'Else
                                                                    'mkstr = mkstr & Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 3, 59)))
                                                                    'End If

                                                                End If

                                                            End If


                                                            'mkstr = mkstr & Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))
                                                        End If
                                                        If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                                            mkstr = mkstr & Trim(DR1.Item("secdet"))
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        Else
                                            If Mid(Trim(DR1.Item("ldata")), 1, 1) = "$" Then
                                                If Trim(flx.get_TextMatrix(i, 19)) = "N" Then

                                                    If Len(Trim(flx.get_TextMatrix(i, 15))) > 0 And Trim(Mid(Trim(DR1.Item("ldata")), 2, 59)) = "DHOTI" Then
                                                        mkstr = Trim(DR1.Item("firstdet"))
                                                        If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                            mkstr = mkstr & "," & LTrim(DR1.Item("fontdet"))
                                                        End If
                                                        mkstr = mkstr & Trim(Mid(Trim(DR1.Item("ldata")), 2, 59))
                                                        'mkstr = mkstr & Trim(DR1.Item("ldata"))
                                                        If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                                            mkstr = mkstr & Trim(DR1.Item("secdet"))
                                                        End If
                                                    End If

                                                    If Len(Trim(flx.get_TextMatrix(i, 16))) > 0 And Trim(Mid(Trim(DR1.Item("ldata")), 2, 59)) = "SHIRTING" Then
                                                        mkstr = Trim(DR1.Item("firstdet"))
                                                        If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                            mkstr = mkstr & "," & LTrim(DR1.Item("fontdet"))
                                                        End If
                                                        mkstr = mkstr & Trim(Mid(Trim(DR1.Item("ldata")), 2, 59))
                                                        'mkstr = mkstr & Trim(DR1.Item("ldata"))
                                                        If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                                            mkstr = mkstr & Trim(DR1.Item("secdet"))
                                                        End If
                                                    End If

                                                    If Len(Trim(flx.get_TextMatrix(i, 17))) > 0 And Trim(Mid(Trim(DR1.Item("ldata")), 2, 59)) = "SUITING" Then
                                                        mkstr = Trim(DR1.Item("firstdet"))
                                                        If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                            mkstr = mkstr & "," & LTrim(DR1.Item("fontdet"))
                                                        End If
                                                        mkstr = mkstr & Trim(Mid(Trim(DR1.Item("ldata")), 2, 59))
                                                        'mkstr = mkstr & Trim(DR1.Item("ldata"))
                                                        If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                                            mkstr = mkstr & Trim(DR1.Item("secdet"))
                                                        End If
                                                    End If

                                                    If Len(Trim(flx.get_TextMatrix(i, 18))) > 0 And Trim(Mid(Trim(DR1.Item("ldata")), 2, 59)) = "TOWEL" Then
                                                        mkstr = Trim(DR1.Item("firstdet"))
                                                        If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                            mkstr = mkstr & "," & LTrim(DR1.Item("fontdet"))
                                                        End If
                                                        mkstr = mkstr & Trim(Mid(Trim(DR1.Item("ldata")), 2, 59))
                                                        'mkstr = mkstr & Trim(DR1.Item("ldata"))
                                                        If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                                            mkstr = mkstr & Trim(DR1.Item("secdet"))
                                                        End If
                                                    End If

                                                End If
                                            Else
                                                mkstr = Trim(DR1.Item("firstdet"))
                                                If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                    mkstr = mkstr & "," & LTrim(DR1.Item("fontdet"))
                                                End If
                                                mkstr = mkstr & Trim(DR1.Item("ldata"))
                                                If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                                    mkstr = mkstr & Trim(DR1.Item("secdet"))
                                                End If

                                            End If
                                        End If

                                    End While
                                Else
                                    mkstr = Trim(DR1.Item("firstdet"))
                                    If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                        mkstr = mkstr & "," & LTrim(DR1.Item("fontdet"))
                                    End If
                                    mkstr = mkstr & Trim(DR1.Item("ldata"))
                                    If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                        mkstr = mkstr & Trim(DR1.Item("secdet"))
                                    End If
                                    DR4.Close()
                                    cmd4.Dispose()
                                End If

                                '*****
                                If Mid(Trim(DR1.Item("ldata")), 1, 1) = "$" Then
                                    If Trim(flx.get_TextMatrix(i, 19)) = "N" Then
                                        If Len(Trim(flx.get_TextMatrix(i, 15))) > 0 And Trim(Mid(Trim(DR1.Item("ldata")), 2, 59)) = "DHOTI" Then
                                            mkstr = Trim(DR1.Item("firstdet"))
                                            If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                mkstr = mkstr & "," & LTrim(DR1.Item("fontdet"))
                                            End If
                                            mkstr = mkstr & Trim(Mid(Trim(DR1.Item("ldata")), 2, 59))
                                            'mkstr = mkstr & Trim(DR1.Item("ldata"))
                                            If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                                mkstr = mkstr & Trim(DR1.Item("secdet"))
                                            End If
                                        End If
                                        If Len(Trim(flx.get_TextMatrix(i, 16))) > 0 And Trim(Mid(Trim(DR1.Item("ldata")), 2, 59)) = "SHIRTING" Then
                                            mkstr = Trim(DR1.Item("firstdet"))
                                            If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                mkstr = mkstr & "," & LTrim(DR1.Item("fontdet"))
                                            End If
                                            mkstr = mkstr & Trim(Mid(Trim(DR1.Item("ldata")), 2, 59))
                                            If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                                mkstr = mkstr & Trim(DR1.Item("secdet"))
                                            End If
                                        End If
                                        If Len(Trim(flx.get_TextMatrix(i, 17))) > 0 And Trim(Mid(Trim(DR1.Item("ldata")), 2, 59)) = "SUITING" Then
                                            mkstr = Trim(DR1.Item("firstdet"))
                                            If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                mkstr = mkstr & "," & LTrim(DR1.Item("fontdet"))
                                            End If
                                            'mkstr = mkstr & Trim(DR1.Item("ldata"))
                                            mkstr = mkstr & Trim(Mid(Trim(DR1.Item("ldata")), 2, 59))
                                            If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                                mkstr = mkstr & Trim(DR1.Item("secdet"))
                                            End If
                                        End If
                                        If Len(Trim(flx.get_TextMatrix(i, 18))) > 0 And Trim(Mid(Trim(DR1.Item("ldata")), 2, 59)) = "TOWEL" Then
                                            mkstr = Trim(DR1.Item("firstdet"))
                                            If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                mkstr = mkstr & "," & LTrim(DR1.Item("fontdet"))
                                            End If
                                            'mkstr = mkstr & Trim(DR1.Item("ldata"))
                                            mkstr = mkstr & Trim(Mid(Trim(DR1.Item("ldata")), 2, 59))
                                            If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                                mkstr = mkstr & Trim(DR1.Item("secdet"))
                                            End If
                                        End If

                                    End If
                                End If



                                'mkstr = mkstr & trim(BS.Fields(ps!ldata))
                                'mkstr = mkstr & Trim(DR1.Item("ldata"))
                            End If
                            '***
                            'If Len(Trim(DR1.Item("secdet"))) > 0 Then
                            ' mkstr = mkstr & Trim(DR1.Item("secdet"))
                            ' End If
                            If Trim(Len(mkstr)) > 0 Then
                                PrintLine(1, TAB(0), mkstr)
                            End If
                            mkstr = ""
                        End While
                        If Val(txtstickcol.Text) > 1 Then
                            mkrow = mkrow + Val(txtrow.Text)
                        End If
                    End If
                    DR1.Close()

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                CMD1.Dispose()


                msql2 = "SELECT  [docentry],[date],[linenum],[headtype],[firstdet],[lrow],[lcol],[fontdet],[ldata],[secdet] FROM bardet where docentry=" & Val(txtdocno.Text) & " and headtype='F' order by docentry,linenum"

                'footer
                Dim CMD2 As New OleDb.OleDbCommand(msql2, con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                Try
                    Dim DR2 As OleDb.OleDbDataReader
                    DR2 = CMD2.ExecuteReader
                    If DR2.HasRows = True Then
                        While DR2.Read
                            'mkrow = Val(DR1.Item("lrow"))
                            mkstr = Trim(DR2.Item("firstdet"))
                            If Len(Trim(DR2.Item("ldata"))) > 0 Then

                                Dim cmd5 As New OleDb.OleDbCommand(msql, con)
                                If con.State = ConnectionState.Closed Then
                                    con.Open()
                                End If

                                Dim DR5 As OleDb.OleDbDataReader
                                DR5 = cmd5.ExecuteReader
                                If DR5.HasRows = True Then
                                    ttrue = False
                                    While DR5.Read
                                        If Mid(Trim(DR2.Item("ldata")), 1, 1) = "$" Then
                                            If Val(DR5.Item(Mid(Trim(DR2.Item("ldata")), 2, 59))) <> Val(flx.get_TextMatrix(i, 4)) Then
                                                mkstr = mkstr & Trim(Microsoft.VisualBasic.Format((Val(flx.get_TextMatrix(i, 4)) / Val(flx.get_TextMatrix(i, 7))), "###########0"))
                                            Else
                                                mkstr = mkstr & Trim(Microsoft.VisualBasic.Format(Val(DR5.Item(Mid(Trim(DR2.Item("ldata")), 2, 59)))))
                                            End If

                                        ElseIf Mid(Trim(DR2.Item("ldata")), 1, 1) = "@" Then
                                            If Val(DR5.Item(Mid(Trim(DR2.Item("ldata")), 2, 59))) <> Val(flx.get_TextMatrix(i, 4)) Then
                                                If ttrue = False Then
                                                    mkstr = mkstr & Trim(Microsoft.VisualBasic.Format(Val(flx.get_TextMatrix(i, 4)), "###########0"))
                                                    ttrue = True
                                                End If
                                            Else

                                                If Mid(Trim(DR2.Item("firstdet")), 2, 2) = "PQ" Then
                                                    If ttrue = False Then
                                                        mkstr = mkstr & Trim(Microsoft.VisualBasic.Format(Val(DR5.Item(Mid(Trim(DR2.Item("ldata")), 2, 59)))))
                                                        ttrue = True
                                                    End If
                                                End If
                                                'mkstr = mkstr & Trim(Microsoft.VisualBasic.Format(Val(DR5.Item(Mid(Trim(DR2.Item("ldata")), 2, 59)))))

                                            End If

                                        Else
                                            mkstr = mkstr & Trim(Microsoft.VisualBasic.Format(Val(DR2.Item("ldata")), "###########0"))
                                        End If

                                    End While

                                End If

                                'mkstr = mkstr & trim(BS.Fields(ps!ldata))
                                'mkstr = mkstr & Trim(DR2.Item("ldata"))
                            Else
                                'mkstr = mkstr & Trim(Microsoft.VisualBasic.Format(Val(flx.get_TextMatrix(i, 4)), "###########0"))

                            End If
                            If Len(Trim(DR2.Item("secdet"))) > 0 Then
                                mkstr = mkstr & Trim(DR2.Item("secdet"))
                            End If

                            PrintLine(1, TAB(0), mkstr)
                            mkstr = ""

                        End While

                        If Val(txtstickcol.Text) > 1 Then
                            mkrow = mkrow + Val(txtrow.Text)
                        End If
                    End If
                    DR2.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                CMD2.Dispose()

                If mksno = Val(txtstickcol.Text) Then
                    mkrow = 0
                    mksno = 0
                End If

            End If
        Next i

        'k = 1

        'If sno <> 1 Then
        '    PrintLine(1, TAB(0), "P1")
        'End If

        'crCMD.Dispose()
        ' crdCMD.Dispose()
        FileClose(1)
        If chkprndir.Checked = False Then
            If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                If MsgBox("Lpt Port", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text) & ":")
                Else
                    Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                    'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                End If
            End If
        End If


        'Shell("print /d:LPT1 c:\sbarcode.txt", vbNormalFocus)
        'Shell("print c:\sbarcode.txt.zpl")

    End Sub
    Private Sub barprnsapmulti()
        Dim i, k As Integer
        Dim sno, mksno, mkrow, mkcol As Integer

        'Dim mpurrate As Double

        mtxtbno = 0

        mksno = 0
        mkrow = 0
        mkcol = 0


        If Trim(cmbprnon.Text) = "SALES" Then
            mfil = "INV1"
            mfildet = "OINV"
        ElseIf Trim(cmbprnon.Text) = "DATE ORDER" Then
            mfil = "DLN1"
            mfildet = "ODLN"
        ElseIf Trim(cmbprnon.Text) = "SAMPLE" Then
            mfil = "V_sampinv1"
            mfildet = "v_sampOinv"
        End If


        FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
        'FileOpen(1, "c:\sbarcode.txt", OpenMode.Output, OpenAccess.Write)
        'FileOpen(1, "LPT1", OpenMode.Output)


        For i = 1 To flx.Rows - 1

            If Len(Trim(flx.get_TextMatrix(i, 0))) > 0 Then
                msql = "SELECT  [docentry],[date],[linenum],[headtype],[firstdet],[lrow],[lcol],[fontdet],[ldata],[secdet] FROM bardet where docentry=" & Val(txtdocno.Text) & " and headtype='H' order by docentry,linenum"

                'Header
                Dim CMD As New OleDb.OleDbCommand(msql, con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                Dim DR As OleDb.OleDbDataReader
                DR = CMD.ExecuteReader
                If DR.HasRows = True Then
                    While DR.Read
                        PrintLine(1, TAB(0), DR.Item("firstdet"))
                    End While

                End If
                DR.Close()
                CMD.Dispose()

                For k = 1 To Val(flx.get_TextMatrix(i, 4))



                    ' msql = "select t0.docnum,t1.linenum,t0.docdate,t0.cardcode,t0.cardname,t1.itemcode,t1.u_catalogname as quality,t1.u_size,t1.quantity,cr.state, " & vbCrLf _
                    '& "k.u_catalgcode, k.u_itemgrp, k.salunitmsr as uom,k.salpackun as box, k.u_length, k.u_width, k.u_style, k.U_SelPrice as rate, k.u_mrp as mrp, k.U_Remarks, k.u_brand, k.invntitem, " & vbCrLf _
                    '& "CASE when cast(k.u_length as real)>0 and rtrim(upper(k.u_itemgrp))='DHOTI' then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  k.u_length end as msize,'MFD ' + substring(datename(m,GETDATE()),1,3) +' '+ CAST(DATEPART(YYYY,GETDATE()) as CHAR(4)) as mfd,  " & vbCrLf _
                    '& " (left(convert(varchar,t0.docnum)+'00000',5)+left(convert(varchar,t0.cardcode)+'0000000000000',13)+left(convert(varchar,k.U_SelPrice)+'00000',5)) as barcode, " & vbCrLf _
                    '& " (ltrim(convert(varchar,t0.docnum))+rtrim(convert(varchar,t0.cardcode))+ltrim(convert(varchar,k.U_SelPrice))) as barcode1  " & vbCrLf _
                    '& "from INV1 as t1 " & vbCrLf _
                    '& "left join OINV as t0 on t0.docentry=t1.DocEntry " & vbCrLf _
                    '& "left join CRD1 as cr on cr.CardCode=t0.CardCode " & vbCrLf _
                    '& "Left Join " & vbCrLf _
                    '& "(select it.invntitem, t0.u_itemcode,t0.u_itemname as mainname,t1.u_catalgcode,t0.u_itemgrp,it.salunitmsr,it.salpackun,t0.u_size,tz.u_length,tz.u_width,t0.u_style,t2.U_State,t2.U_SelPrice,t2.u_mrp,t1.U_Remarks,t1.u_brand from [@INS_PLM1] as t1 " & vbCrLf _
                    '& "left join [@INS_OPLM] as t0 on t0.DocEntry=t1.DocEntry " & vbCrLf _
                    '& "left join [@INS_PLM2] as t2 on t2.DocEntry=t1.docentry and t2.U_RowID=t1.lineid" & vbCrLf _
                    '& "left join [@INCM_SZE1] as tz on tz.U_Name=t0.u_size" & vbCrLf _
                    '& "left join OITM as it on it.ItemCode=t0.U_ItemCode) as k on k.u_catalgcode=t1.U_CatalogName and k.u_itemcode=t1.ItemCode and k.U_Size=t1.U_Size and k.U_State=cr.state" & vbCrLf _
                    '& " where t0.docnum=" & Val(txtbno.Text) & " and t1.u_catalogname='" & flx.get_TextMatrix(i, 1) & "' and t1.u_size='" & flx.get_TextMatrix(i, 13) & "' and t1.linenum=" & Val(flx.get_TextMatrix(i, 14)) & vbCrLf _
                    '& " order by t1.linenum"


                    msql = "select t0.docnum,t1.linenum,t0.docdate,t0.cardcode,t0.cardname,t1.itemcode,case when t1.freetxt is null or len(rtrim(t1.freetxt))=0  then t1.u_catalogname else t1.freetxt end  as quality,t1.u_size,convert(decimal(16),t1.quantity) as quantity,floor(convert(decimal(16),t1.quantity)/cast(k.salpackun as int)) as boxqty,cr.state, " & vbCrLf _
            & "k.u_catalgcode, k.u_itemgrp, k.salunitmsr as uom,cast(k.salpackun as int) as box, case when CHARINDEX('CMS',k.u_length)<=0  then cast(isnull(k.u_length,0) as real) else 0 end  as u_length,case when CHARINDEX('CMS',k.u_width)<=0 then isnull(k.u_width,0) else 0 end  as u_width, k.u_style, convert(decimal(11,2),k.U_SelPrice) as rate,cast((k.salpackun*k.u_mrp) as decimal(7,2)) as boxmrp, cast(k.u_mrp as decimal(7,2)) as mrp, k.U_Remarks, k.u_brand, k.invntitem, " & vbCrLf _
            & " ltrim(rtrim(right(YEAR(t0.docdate),2)))+right('0'+rtrim(ltrim(MONTH(t0.docdate))),2)+rtrim(ltrim(convert(nvarchar(100),k.u_remarks))) as autocode," & vbCrLf _
            & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  RTRIM(k.u_length)  end as msize," & vbCrLf _
           & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  replace(RTRIM(k.u_length),'S','X')+ rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end as msize2,'MFD ' + substring(datename(m,GETDATE()),1,3) +' '+ CAST(DATEPART(YYYY,GETDATE()) as CHAR(4)) as mfd, " & vbCrLf _
            & " (left(convert(varchar,t0.docnum) + '00000',5)+left(convert(varchar,t0.cardcode) +'0000000000000',13) + left(convert(varchar,convert(decimal(16),k.U_SelPrice)) + '00000',5)) as barcode, " & vbCrLf _
            & " (ltrim(convert(varchar,t0.docnum)) + rtrim(convert(varchar,t0.cardcode))+ltrim(convert(varchar,convert(decimal(16),k.U_SelPrice)))) as barcode1,ltrim(convert(varchar,t0.docnum)) as barcode2,ltrim(convert(varchar,t0.docentry))+rtrim(t1.itemcode) as drbarcode,bb.dhoti,bb.shirting,bb.suiting,bb.towel,k.u_subgrp6,k.color   " & vbCrLf _
            & "from " & Trim(mfil) & " as t1 " & vbCrLf _
            & "left join " & Trim(mfildet) & " as t0 on t0.docentry=t1.DocEntry " & vbCrLf _
            & "left join CRD1 as cr on cr.CardCode=t0.CardCode and (cr.Address='Office' or cr.Address='Ship') and cr.AdresType='B' " & vbCrLf _
             & "left join barcodebom as bb on BB.code=t1.itemcode " & vbCrLf _
            & "Left Join " & vbCrLf _
            & "(select it.invntitem, t0.u_itemcode,t0.u_itemname as mainname,t1.u_catalgcode,t0.u_itemgrp,it.salunitmsr,it.salpackun,t0.u_size,tz.u_length,tz.u_width,t0.u_style,case when t2.u_state='Other' then 'OS' else t2.U_State end u_state,t2.U_SelPrice,t2.u_mrp,t1.U_Remarks,t1.u_brand,it.u_subgrp6,it.u_subgrp5 as color from [@INS_PLM1] as t1 " & vbCrLf _
            & "left join [@INS_OPLM] as t0 on t0.DocEntry=t1.DocEntry " & vbCrLf _
            & "left join [@INS_PLM2] as t2 on t2.DocEntry=t1.docentry and t2.U_RowID=t1.lineid" & vbCrLf _
            & "left join [@INCM_SZE1] as tz on tz.U_Name=t0.u_size" & vbCrLf _
            & "left join OITM as it on it.ItemCode=t0.U_ItemCode where t0.U_ItemCode is not null) as k on k.u_catalgcode=t1.U_CatalogName and k.u_itemcode=t1.ItemCode and k.U_Size=t1.U_Size and isnull(k.U_Style,'')=isnull(t1.U_Style,'') and k.U_State=cr.state" & vbCrLf _
            & " where t0.docnum=" & Val(txtbno.Text) & " and t1.TreeType<>'I' and t1.u_catalogname='" & Trim(flx.get_TextMatrix(i, 1)) & "' and  k.u_size='" & Trim(flx.get_TextMatrix(i, 13)) & "'" & vbCrLf

                    If Len(Trim(flx.get_TextMatrix(i, 3))) > 0 Then
                        msql = msql & " and k.u_style='" & Trim(flx.get_TextMatrix(i, 3)) & "'" & vbCrLf
                    End If
                    msql = msql & " order by t1.linenum"
                    '& " where t0.docnum=" & Val(txtbno.Text) & " and t1.TreeType<>'I' " & vbCrLf _
                    '            & " order by t1.linenum"


                    msql2 = "SELECT  [docentry],[date],[linenum],[headtype],[firstdet],[lrow],[lcol],[fontdet],[ldata],[secdet] FROM bardet where docentry=" & Val(txtdocno.Text) & " and headtype='D' order by docentry,linenum"



                    'Detail
                    Dim CMD1 As New OleDb.OleDbCommand(msql2, con)
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If

                    Try
                        Dim DR1 As OleDb.OleDbDataReader
                        DR1 = CMD1.ExecuteReader
                        If DR1.HasRows = True Then
                            While DR1.Read
                                'mkrow = Val(DR1.Item("lrow"))
                                mkstr = Trim(DR1.Item("firstdet"))
                                'If Val(txtrow.Text) > 0 Then
                                ' mkstr = mkstr & Trim(Str(mkrow))
                                ' End If
                                If IsDBNull(DR1.Item("lrow")) = False Then
                                    If Val(DR1.Item("lrow")) > 0 Then
                                        mkstr = mkstr & Trim((Val(DR1.Item("lrow")) + Val(mkrow))) & ","
                                    End If
                                End If

                                If IsDBNull(DR1.Item("lcol")) = False Then
                                    If Val(DR1.Item("lcol")) > 0 Then
                                        mkstr = mkstr & Trim((Val(DR1.Item("lcol")) + Val(mkcol)))
                                    End If
                                End If
                                'If Val(txtcol.Text) > 0 Then
                                ' mkstr = mkstr & "," & Trim(txtcol.Text)
                                ' End If
                                If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                    mkstr = mkstr & "," & Trim(DR1.Item("fontdet"))
                                End If
                                If Len(Trim(DR1.Item("ldata"))) > 0 Then
                                    Dim cmd4 As New OleDb.OleDbCommand(msql, con)
                                    If con.State = ConnectionState.Closed Then
                                        con.Open()
                                    End If

                                    Dim DR4 As OleDb.OleDbDataReader
                                    DR4 = cmd4.ExecuteReader
                                    If DR4.HasRows = True Then
                                        While DR4.Read

                                            If Mid(Trim(DR1.Item("ldata")), 1, 1) = "@" Then
                                                mkstr = mkstr & Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))
                                            Else
                                                mkstr = mkstr & Trim(DR1.Item("ldata"))
                                            End If
                                            'mkstr = mkstr & Trim(DR4.Item(Trim(DR1.Item("ldata"))))
                                        End While
                                    Else
                                        mkstr = mkstr & Trim(DR1.Item("ldata"))
                                        DR4.Close()
                                        cmd4.Dispose()
                                    End If

                                    'mkstr = mkstr & trim(BS.Fields(ps!ldata))
                                    'mkstr = mkstr & Trim(DR1.Item("ldata"))
                                End If
                                If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                    mkstr = mkstr & Trim(DR1.Item("secdet"))
                                End If

                                PrintLine(1, TAB(0), mkstr)
                                mkstr = ""
                            End While

                            If Val(txtstickcol.Text) > 1 Then
                                If Val(txtrow.Text) > 0 Then
                                    mkrow = mkrow + Val(txtrow.Text)
                                End If
                                If Val(txtcol.Text) > 0 Then
                                    mkcol = mkcol + Val(txtcol.Text)
                                End If
                            End If

                        End If
                        DR1.Close()

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                    CMD1.Dispose()
                    mksno = mksno + 1

                    'msql2 = "SELECT  [docentry],[date],[linenum],[headtype],[firstdet],[lrow],[lcol],[fontdet],[ldata],[secdet] FROM [rhlinventry13].[dbo].[bardet]where docentry=" & Val(txtdocno.Text) & " and headtype='F' order by docentry,linenum"




                    If mksno = Val(txtstickcol.Text) Then
                        msql2 = "SELECT  [docentry],[date],[linenum],[headtype],[firstdet],[lrow],[lcol],[fontdet],[ldata],[secdet] FROM bardet where docentry=" & Val(txtdocno.Text) & " and headtype='F' order by docentry,linenum"
                        Dim CMD2 As New OleDb.OleDbCommand(msql2, con)
                        If con.State = ConnectionState.Closed Then
                            con.Open()
                        End If


                        Dim DR2 As OleDb.OleDbDataReader
                        DR2 = CMD2.ExecuteReader
                        If DR2.HasRows = True Then
                            While DR2.Read
                                'mkrow = Val(DR1.Item("lrow"))
                                mkstr = Trim(DR2.Item("firstdet"))
                                If Len(Trim(DR2.Item("ldata"))) > 0 Then
                                    mkstr = mkstr & Trim(DR2.Item("ldata"))
                                Else
                                    mkstr = mkstr

                                End If
                                If Len(Trim(DR2.Item("secdet"))) > 0 Then
                                    mkstr = mkstr & Trim(DR2.Item("secdet"))
                                End If

                                PrintLine(1, TAB(0), mkstr)
                                mkstr = ""
                            End While
                            If Val(txtstickcol.Text) > 1 Then
                                mkrow = mkrow + Val(txtrow.Text)
                            End If
                        End If
                        DR2.Close()

                        msql = "SELECT  [docentry],[date],[linenum],[headtype],[firstdet],[lrow],[lcol],[fontdet],[ldata],[secdet] FROM bardet where docentry=" & Val(txtdocno.Text) & " and headtype='H' order by docentry,linenum"
                        'Header
                        Dim CMDh As New OleDb.OleDbCommand(msql, con)
                        If con.State = ConnectionState.Closed Then
                            con.Open()
                        End If

                        Dim DRh As OleDb.OleDbDataReader
                        DRh = CMDh.ExecuteReader
                        If DRh.HasRows = True Then
                            While DRh.Read
                                PrintLine(1, TAB(0), DRh.Item("firstdet"))
                            End While
                        End If
                        DRh.Close()
                        CMDh.Dispose()
                        mkrow = 0
                        mkcol = 0
                        mksno = 0
                    End If

                Next k
            End If
        Next i

        'k = 1

        'If sno <> 1 Then
        '    PrintLine(1, TAB(0), "P1")
        'End If

        FileClose(1)
        'Shell("print /d:LPT1 c:\sbarcode.txt", vbNormalFocus)

        ''Shell("print c:\sbarcode.txt.zpl")

    End Sub
    Private Sub barprnsapmulti2()
        Dim i, k As Integer
        Dim sno, mksno, mkrow, mkcol, mdatasno As Integer
        Dim mktt, mkt2 As Boolean

        'Dim mpurrate As Double

        mtxtbno = 0

        mksno = 0
        mkrow = 0
        mkcol = 0

        If chksr.Checked = True Then
            mfil = "(select t0.docentry,t2.u_itemcode as itemcode,t0.itemcode remarks,t2.U_Size, t1.u_catalgcode,t1.u_itemname as u_catalogname,t0.linenum,t0.freetxt,t0.quantity,CASE when t3.InvntItem='N' then 'S' else '' end treetype,'' as text from srbardet t0 " & vbCrLf _
                & "left join [@INS_PLM1] t1 on  convert(nvarchar(40),t1.U_Remarks)= t0.itemcode " & vbCrLf _
                & " left join [@INS_OPLM] t2 on t2.DocEntry=t1.DocEntry " & vbCrLf _
                & "left join OITM t3 on t3.ItemCode=t2.U_ItemCode " & vbCrLf _
                & " where t1.U_Lock<>'Y')"
            mfildet = "(select docentry,docnum,docdate,CardCode,cardname from srbardet group by docentry,docnum,docdate,CardCode,cardname) "

            'mfil = "V_sampinv1"
            'mfildet = "v_sampOinv"
        Else

            If Trim(cmbprnon.Text) = "SALES" Then
                mfil = "INV1"
                mfildet = "OINV"
            ElseIf Trim(cmbprnon.Text) = "DATE ORDER" Then
                mfil = "DLN1"
                mfildet = "ODLN"
            ElseIf Trim(cmbprnon.Text) = "INV DRAFT" Then
                mfil = "DRF1"
                mfildet = "ODRF"
            ElseIf Trim(cmbprnon.Text) = "SAMPLE" Then
                mfil = "V_sampinv1"
                mfildet = "v_sampOinv"
            End If
        End If
        '**** tmp

        'msql4 = " IF OBJECT_ID(N'tempdb..#barTemp') IS NOT NULL " & vbCrLf _
        '    & " BEGIN " & vbCrLf _
        '    & "  DROP TABLE #barTemp " & vbCrLf _
        '    & " End "
        'Dim dCMD As New OleDb.OleDbCommand(msql4, con)
        ''Dim CMD2 As New SqlClient.SqlCommand("DELETE FROM BPORD WHERE BNO=" & Val(txtbno.Text), con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'dCMD.ExecuteNonQuery()


        'Try
        '    'TRANS.Commit()
        '    ' MsgBox("Deleted")
        '    dCMD.Dispose()
        '    'CMD2.Dispose()
        'Catch ex As Exception
        '    'TRANS.Rollback()
        '    MsgBox(ex.Message)
        '    dCMD.Dispose()
        '    'CMD2.Dispose()
        'End Try

        ''TRANS.Commit()
        'dCMD.Dispose()

        '& "CASE when   CHARINDEX('CMS',k.u_length)<=0 and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp))in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  ltrim(convert(varchar,round(cast(replace(k.u_length,'CMS','') as real)/100,2)))+'mX' + rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end as msize," & vbCrLf _

        'msql = "select t0.docentry, t0.docnum,t1.linenum,t0.docdate,t0.cardcode,t0.cardname,t1.itemcode,case when t1.freetxt is null or len(rtrim(t1.freetxt))=0  then t1.u_catalogname else t1.freetxt  end  as quality,t1.u_size,convert(decimal(16),t1.quantity) as quantity,floor(convert(decimal(16),t1.quantity)/cast(k.salpackun as int)) as boxqty,cr.state, " & vbCrLf _
        '    & "k.u_catalgcode, k.u_itemgrp, k.salunitmsr as uom,cast(k.salpackun as int) as box, case when CHARINDEX('CMS',k.u_length)<=0 then cast(isnull(k.u_length,0) as real) else 0 end  as u_length, cast(case when CHARINDEX('CMS',k.u_width)<=0  then isnull(k.u_width,0.0) else 0.0 end as real) as u_width, k.u_style,convert(decimal(11,2), k.U_SelPrice) as rate,cast((k.salpackun*k.u_mrp) as decimal(7,2)) as boxmrp, cast(k.u_mrp as decimal(7,2)) as mrp, k.U_Remarks, k.u_brand, k.invntitem, " & vbCrLf _
        '    & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  RTRIM(k.u_length)  end as msize," & vbCrLf _
        '    & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  replace(RTRIM(k.u_length),'S','X')+ rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end as msize2," & vbCrLf _
        '    & "'MFD ' + substring(datename(m,GETDATE()),1,3) +' '+ CAST(DATEPART(YYYY,GETDATE()) as CHAR(4)) as mfd, " & vbCrLf _
        '    & " (left(convert(varchar,t0.docnum) + '00000',5)+left(convert(varchar,t0.cardcode) +'0000000000000',13) + left(convert(varchar,convert(decimal(16),k.U_SelPrice)) + '00000',5)) as barcode, " & vbCrLf _
        '    & " (ltrim(convert(varchar,t0.docnum)) + rtrim(convert(varchar,t0.cardcode))+ltrim(convert(varchar,convert(decimal(16),k.U_SelPrice)))) as barcode1,ltrim(convert(varchar,t0.docnum)) as barcode2,ltrim(convert(varchar,t0.docnum))+rtrim(t1.itemcode) as barcode3,ltrim(convert(varchar,t0.docentry))+rtrim(t1.itemcode) as drbarcode,bb.dhoti,bb.shirting,bb.suiting,bb.towel,k.u_subgrp6,k.color,t1.text as mtrstot,t1.u_catalogname  " & vbCrLf _
        '    & " into #barTemp " & vbCrLf _
        '    & "from " & Trim(mfil) & " as t1 " & vbCrLf _
        '    & "left join " & Trim(mfildet) & " as t0 on t0.docentry=t1.DocEntry " & vbCrLf _
        '    & "left join CRD1 as cr on cr.CardCode=t0.CardCode and (cr.Address='Office' or cr.Address='Ship') and cr.AdresType='B' " & vbCrLf _
        '     & "left join barcodebom as bb on BB.code=t1.itemcode " & vbCrLf _
        '    & "Inner Join " & vbCrLf _
        '    & "(select it.invntitem, t0.u_itemcode,t0.u_itemname as mainname,t1.u_catalgcode,t0.u_itemgrp,it.salunitmsr,it.salpackun,t0.u_size,tz.u_length,tz.u_width,case when t0.u_style IS null then ISNULL(t0.u_style,'') else  t0.u_style end u_style,case when t2.u_state='Other' then 'OS' else t2.U_State end u_state,t2.U_SelPrice,t2.u_mrp,t1.U_Remarks,t1.u_brand,it.u_subgrp5 as color,it.u_subgrp6 from [@INS_PLM1] as t1 " & vbCrLf _
        '    & "Inner join [@INS_OPLM] as t0 on t0.DocEntry=t1.DocEntry " & vbCrLf _
        '    & "Inner join [@INS_PLM2] as t2 on t2.DocEntry=t1.docentry and t2.U_RowID=t1.lineid" & vbCrLf _
        '    & "Inner join [@INCM_SZE1] as tz on tz.U_Name=t0.u_size" & vbCrLf _
        '    & "Inner join OITM as it on it.ItemCode=t0.U_ItemCode where t0.U_ItemCode is not null) as k on k.u_catalgcode=t1.U_CatalogName and k.u_itemcode=t1.ItemCode and k.U_Size=t1.U_Size  and k.U_State=cr.state" & vbCrLf
        'If Trim(cmbprnon.Text) = "INV DRAFT" Then
        '    msql = msql & " where t0.docentry=" & Val(txtbno.Text) & " and t1.TreeType<>'I'" & vbCrLf
        'Else
        '    msql = msql & " where t0.docnum=" & Val(txtbno.Text) & " and t1.TreeType<>'I' " & vbCrLf
        'End If

        ''If Len(Trim(flx.get_TextMatrix(i, 3))) > 0 Then
        ''    msql = msql & " and k.u_style='" & Trim(flx.get_TextMatrix(i, 3)) & "'" & vbCrLf
        ''End If
        'msql = msql & " order by t1.linenum"

        'Dim crCMD As New OleDb.OleDbCommand(msql, con)
        ''Dim CMD2 As New SqlClient.SqlCommand("DELETE FROM BPORD WHERE BNO=" & Val(txtbno.Text), con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'crCMD.ExecuteNonQuery()

        ''Try
        ''    'TRANS.Commit()
        ''    ' MsgBox("Deleted")
        ''    crCMD.Dispose()
        ''    'CMD2.Dispose()
        ''Catch ex As Exception
        ''    'TRANS.Rollback()
        ''    MsgBox(ex.Message)
        ''    crCMD.Dispose()
        ''    'CMD2.Dispose()
        ''End Try

        ' ''TRANS.Commit()
        ''crCMD.Dispose()






        '*** tmep









        Dim dir As String




        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "sbarcode.txt"


        If chkprndir.Checked = True Then
            FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        Else
            FileOpen(1, mdir, OpenMode.Output)
        End If


        'FileOpen(1, mdir, OpenMode.Output)
        'FileOpen(1, "c:\sbarcode.txt", OpenMode.Output, OpenAccess.Write)
        'FileOpen(1, "LPT1", OpenMode.Output)

        msql = "SELECT  [docentry],[date],[linenum],[headtype],[firstdet],[lrow],[lcol],[fontdet],[ldata],[secdet] FROM bardet where docentry=" & Val(txtdocno.Text) & " and headtype='H' order by docentry,linenum"

        'Header
        Dim CMD As New OleDb.OleDbCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim DR As OleDb.OleDbDataReader
        DR = CMD.ExecuteReader
        If DR.HasRows = True Then
            While DR.Read
                PrintLine(1, TAB(0), DR.Item("firstdet"))
            End While

        End If
        DR.Close()
        CMD.Dispose()


        For i = 1 To flx.Rows - 1

            If Len(Trim(flx.get_TextMatrix(i, 0))) > 0 Then
                'msql = "SELECT  [docentry],[date],[linenum],[headtype],[firstdet],[lrow],[lcol],[fontdet],[ldata],[secdet] FROM bardet where docentry=" & Val(txtdocno.Text) & " and headtype='H' order by docentry,linenum"

                ''Header
                'Dim CMD As New OleDb.OleDbCommand(msql, con)
                'If con.State = ConnectionState.Closed Then
                '    con.Open()
                'End If

                'Dim DR As OleDb.OleDbDataReader
                'DR = CMD.ExecuteReader
                'If DR.HasRows = True Then
                '    While DR.Read
                '        PrintLine(1, TAB(0), DR.Item("firstdet"))
                '    End While

                'End If
                'DR.Close()
                'CMD.Dispose()

                For k = 1 To (Val(flx.get_TextMatrix(i, 4)))

                    'msql = "select top 1 t0.docnum,t1.linenum,t0.docdate,t0.cardcode,t0.cardname,t1.itemcode,case when t1.freetxt is null or len(rtrim(t1.freetxt))=0  then  t1.u_catalogname else t1.freetxt end  as quality,t1.u_size,convert(decimal(16),t1.quantity) as quantity,floor(convert(decimal(16),t1.quantity)/cast(k.salpackun as int)) as boxqty,cr.state, " & vbCrLf _
                    '& "k.u_catalgcode, k.u_itemgrp, k.salunitmsr as uom,cast(k.salpackun as int) as box, case when CHARINDEX('CMS',k.u_length)<=0 then cast(isnull(k.u_length,0) as real) else 0 end  as u_length,cast(case when CHARINDEX('CMS',k.u_width)<=0 then isnull(k.u_width,0.0) else 0.0 end as real) as u_width, k.u_style, convert(decimal(11,2),k.U_SelPrice) as rate,cast((k.salpackun*k.u_mrp) as decimal(7,2)) as boxmrp, cast(k.u_mrp as decimal(7,2)) as mrp, k.U_Remarks, k.u_brand, k.invntitem, " & vbCrLf _

                    '&        "CASE when CHARINDEX('_',t1.U_Size)>0 then " & vbCrLf _
                    '& "CASE when   CHARINDEX('CMS',k.u_length)<=0 and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp))in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0) then  RTRIM(k.u_width)+'inX'+rtrim(lTRIM(cast(cast(k.u_length as real)*1 as char(10)))) +'in' else  ltrim(convert(varchar,round(cast(replace(k.u_length,'CMS','') as real)/100,2)))+'mX' + rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end " & vbCrLf _
                    '& " else " & vbCrLf _
                    '& " CASE when   CHARINDEX('CMS',k.u_length)<=0 and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp))in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  ltrim(convert(varchar,round(cast(replace(k.u_length,'CMS','') as real)/100,2)))+'mX' + rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end end as msize, " & vbCrLf _
                    '& "CASE when CHARINDEX('_',t1.U_Size)>0 then " & vbCrLf _
                    '& "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'inX'+rtrim(lTRIM(cast(cast(k.u_length as real)*1 as char(10)))) +'in' else  replace(RTRIM(k.u_length),'S','X')+ rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end " & vbCrLf _
                    '& "else " & vbCrLf _
                    '& "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  replace(RTRIM(k.u_length),'S','X')+ rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end end as msize2, " & vbCrLf _




                    '& "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 ) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  RTRIM(k.u_length)  end as msize," & vbCrLf _



                    '& "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp)) in( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0 )then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else replace(RTRIM(k.u_length),'S','X')+ rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end as msize2,'MFD ' + substring(datename(m,GETDATE()),1,3) +' '+ CAST(DATEPART(YYYY,GETDATE()) as CHAR(4)) as mfd, " & vbCrLf _
                    '& " (left(convert(varchar,t0.docnum) + '00000',5)+left(convert(varchar,t0.cardcode) +'0000000000000',13) + left(convert(varchar,convert(decimal(16),k.U_SelPrice)) + '00000',5)) as barcode, " & vbCrLf _
                    '& " (ltrim(convert(varchar,t0.docnum)) + rtrim(convert(varchar,t0.cardcode))+ltrim(convert(varchar,convert(decimal(16),k.U_SelPrice)))) as barcode1,ltrim(convert(varchar,t0.docnum)) as barcode2,ltrim(convert(varchar,t0.docentry))+rtrim(t1.itemcode) as drbarcode,bb.dhoti,bb.shirting,bb.suiting,bb.towel,k.u_subgrp6,k.color   " & vbCrLf _
                    '& "from " & Trim(mfil) & " as t1 " & vbCrLf _
                    '& "left join " & Trim(mfildet) & " as t0 on t0.docentry=t1.DocEntry " & vbCrLf _
                    '& "left join CRD1 as cr on cr.CardCode=t0.CardCode and (cr.Address='Office' or cr.Address='Ship') and cr.AdresType='B' " & vbCrLf _
                    '& "left join barcodebom as bb on BB.code=t1.itemcode " & vbCrLf _
                    '& "Left Join " & vbCrLf _
                    '& "(select it.invntitem, t0.u_itemcode,t0.u_itemname as mainname,t1.u_catalgcode,t0.u_itemgrp,it.salunitmsr,it.salpackun,t0.u_size,tz.u_length,tz.u_width,case when t0.u_style IS null then ISNULL(t0.u_style,'') else  t0.u_style end u_style ,case when t2.u_state='Other' then 'OS' else t2.U_State end u_state,t2.U_SelPrice,t2.u_mrp,t1.U_Remarks,t1.u_brand,it.u_subgrp6,it.u_subgrp5 as color from [@INS_PLM1] as t1 " & vbCrLf _
                    '& "left join [@INS_OPLM] as t0 on t0.DocEntry=t1.DocEntry " & vbCrLf _
                    '& "left join [@INS_PLM2] as t2 on t2.DocEntry=t1.docentry and t2.U_RowID=t1.lineid" & vbCrLf _
                    '& "left join [@INCM_SZE1] as tz on tz.U_Name=t0.u_size" & vbCrLf _
                    '& "left join OITM as it on it.ItemCode=t0.U_ItemCode where t0.U_ItemCode is not null) as k on k.u_catalgcode=t1.U_CatalogName and k.u_itemcode=t1.ItemCode   and k.U_State=cr.state" & vbCrLf
                    'If Trim(cmbprnon.Text) = "INV DRAFT" Then
                    '    msql = msql & " where t0.docentry=" & Val(txtbno.Text) & " and t1.TreeType<>'I' and t1.u_catalogname='" & Trim(flx.get_TextMatrix(i, 1)) & "' and  k.u_size='" & Trim(flx.get_TextMatrix(i, 13)) & "'" & vbCrLf
                    'Else
                    '    msql = msql & " where t0.docnum=" & Val(txtbno.Text) & " and t1.TreeType<>'I' and t1.u_catalogname='" & Trim(flx.get_TextMatrix(i, 1)) & "' and  k.u_size='" & Trim(flx.get_TextMatrix(i, 13)) & "'" & vbCrLf
                    'End If
                    ''& " where t0.docnum=" & Val(txtbno.Text) & " and t1.TreeType<>'I' and t1.u_catalogname='" & Trim(flx.get_TextMatrix(i, 1)) & "' and  k.u_size='" & Trim(flx.get_TextMatrix(i, 13)) & "'" & vbCrLf

                    'If Len(Trim(flx.get_TextMatrix(i, 3))) > 0 Then
                    '    msql = msql & " and k.u_style='" & Trim(flx.get_TextMatrix(i, 3)) & "'" & vbCrLf
                    'End If
                    'msql = msql & " order by t1.linenum"


                    'BARTMP
                    msql = "select * from barTemp with (nolock)" & vbCrLf

                    If Trim(cmbprnon.Text) = "INV DRAFT" Then
                        msql = msql & " where docentry=" & Val(txtbno.Text) & "and u_catalogname='" & Trim(flx.get_TextMatrix(i, 1)) & "' and  u_size='" & Trim(flx.get_TextMatrix(i, 13)) & "' and datepart(mm,docdate)=" & Val(cmbmont.Text) & " and datepart(yyyy,docdate)=" & Val(cmbyr.Text) & vbCrLf
                    Else
                        msql = msql & " where docnum=" & Val(txtbno.Text) & "  and u_catalogname='" & Trim(flx.get_TextMatrix(i, 1)) & "' and  u_size='" & Trim(flx.get_TextMatrix(i, 13)) & "' and datepart(mm,docdate)=" & Val(cmbmont.Text) & " and datepart(yyyy,docdate)=" & Val(cmbyr.Text) & vbCrLf
                    End If

                    If Len(Trim(flx.get_TextMatrix(i, 3))) > 0 Then
                        msql = msql & " and u_style='" & Trim(flx.get_TextMatrix(i, 3)) & "'" & vbCrLf
                    End If
                    If Len(Trim(flx.get_TextMatrix(i, 22))) > 0 Then
                        msql = msql & " and color='" & Trim(flx.get_TextMatrix(i, 22)) & "'" & vbCrLf
                    End If

                    msql = msql & " order by linenum"





                    msql2 = "SELECT  [docentry],[date],[linenum],[headtype],[firstdet],[lrow],[lcol],[fontdet],[ldata],[secdet] FROM bardet where docentry=" & Val(txtdocno.Text) & " and headtype='D' order by docentry,linenum"



                    'Detail
                    Dim CMD1 As New OleDb.OleDbCommand(msql2, con)
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If

                    Try
                        Dim DR1 As OleDb.OleDbDataReader
                        DR1 = CMD1.ExecuteReader
                        If DR1.HasRows = True Then
                            While DR1.Read
                                'mkrow = Val(DR1.Item("lrow"))
                                'mkstr = Trim(DR1.Item("firstdet"))
                                'If Val(txtrow.Text) > 0 Then
                                ' mkstr = mkstr & Trim(Str(mkrow))
                                ' End If
                                'If IsDBNull(DR1.Item("lrow")) = False Then
                                '    If Val(DR1.Item("lrow")) > 0 Then
                                '        mkstr = mkstr & Trim((Val(DR1.Item("lrow")) + Val(mkrow))) & ","
                                '    End If
                                'End If

                                'If IsDBNull(DR1.Item("lcol")) = False Then
                                '    If Val(DR1.Item("lcol")) > 0 Then
                                '        mkstr = mkstr & Trim((Val(DR1.Item("lcol")) + Val(mkcol)))
                                '    End If
                                'End If
                                ''If Val(txtcol.Text) > 0 Then
                                '' mkstr = mkstr & "," & Trim(txtcol.Text)
                                '' End If
                                'If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                '    mkstr = mkstr & "," & Trim(DR1.Item("fontdet"))
                                'End If
                                If Len(Trim(DR1.Item("ldata"))) > 0 Then

                                    Dim cmd4 As New OleDb.OleDbCommand(msql, con)
                                    If con.State = ConnectionState.Closed Then
                                        con.Open()
                                    End If

                                    Dim DR4 As OleDb.OleDbDataReader
                                    DR4 = cmd4.ExecuteReader
                                    If DR4.HasRows = True Then
                                        While DR4.Read

                                            If Mid(Trim(DR1.Item("ldata")), 1, 1) = "@" Then

                                                If Mid(Trim(DR1.Item("ldata")), 1, 2) = "@$" Then
                                                    ' MsgBox("test")
                                                    If Trim(flx.get_TextMatrix(i, 19)) = "N" Then
                                                        mkt2 = True
                                                    Else
                                                        mkt2 = False
                                                    End If

                                                Else
                                                    mkt2 = False

                                                End If


                                                If UCase(Trim(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", ""))) = "MRP" Or UCase(Trim(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", ""))) = "BOXMRP" Then
                                                    '**
                                                    If chkmrp.Checked = True Then
                                                        If IsDBNull(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", ""))) = False Then
                                                            If Len(Trim(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")))) > 0 Then

                                                                mkstr = Trim(DR1.Item("firstdet"))
                                                                If IsDBNull(DR1.Item("lrow")) = False Then
                                                                    If Val(DR1.Item("lrow")) > 0 Then
                                                                        mkstr = mkstr & Trim((Val(DR1.Item("lrow")) + Val(mkrow))) & ","
                                                                    End If
                                                                End If

                                                                If IsDBNull(DR1.Item("lcol")) = False Then
                                                                    If Val(DR1.Item("lcol")) > 0 Then
                                                                        mkstr = mkstr & Trim((Val(DR1.Item("lcol")) + Val(mkcol))) & ","
                                                                    End If
                                                                End If

                                                                If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                                    mkstr = mkstr & Trim(DR1.Item("fontdet"))
                                                                End If
                                                                'If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                                ' mkstr = mkstr & LTrim(DR1.Item("fontdet"))
                                                                'End If
                                                                If Val(flx.get_TextMatrix(i, 5)) <> Val(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", ""))) Then
                                                                    If UCase(Mid(Trim(DR1.Item("ldata")), 2, 59)) = "BOXMRP" Then
                                                                        'mkstr = mkstr & Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))
                                                                        mkstr = Trim(mkstr) & Trim(Val(flx.get_TextMatrix(i, 5)) * Val(flx.get_TextMatrix(i, 7)))
                                                                    Else
                                                                        mkstr = Trim(mkstr) & Trim(flx.get_TextMatrix(i, 5))
                                                                    End If
                                                                Else
                                                                    mkstr = Trim(mkstr) & Trim(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")))
                                                                End If

                                                                If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                                                    mkstr = mkstr & Trim(DR1.Item("secdet"))
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                Else
                                                    '**

                                                    'If mkt2 = True Then
                                                    ' mktt = IsDBNull(DR4.Item(Mid(Trim(DR1.Item("ldata")), 3, 59)))
                                                    ' mdatasno = Len(Trim(IsDBNull(DR4.Item(Mid(Trim(DR1.Item("ldata")), 3, 59)))))
                                                    ' Else
                                                    ' mktt = IsDBNull(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")))
                                                    ' mdatasno = Len(Trim(IsDBNull(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")))))

                                                    ' End If


                                                    If IsDBNull(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", ""))) = False Then
                                                        If Len(Trim(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")))) > 0 Then

                                                            mkstr = Trim(DR1.Item("firstdet"))
                                                            If IsDBNull(DR1.Item("lrow")) = False Then
                                                                If Val(DR1.Item("lrow")) > 0 Then
                                                                    mkstr = mkstr & Trim((Val(DR1.Item("lrow")) + Val(mkrow))) & ","
                                                                End If
                                                            End If

                                                            If IsDBNull(DR1.Item("lcol")) = False Then
                                                                If Val(DR1.Item("lcol")) > 0 Then
                                                                    mkstr = mkstr & Trim((Val(DR1.Item("lcol")) + Val(mkcol))) & ","
                                                                End If
                                                            End If

                                                            If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                                mkstr = mkstr & Trim(DR1.Item("fontdet"))
                                                            End If
                                                            'If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                            ' mkstr = mkstr & LTrim(DR1.Item("fontdet"))
                                                            'End If
                                                            If Len(Trim(flx.get_TextMatrix(i, 21))) > 0 Then

                                                                If UCase(Trim(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", ""))) = "MSIZE" Then
                                                                    mkstr = mkstr & Trim(flx.get_TextMatrix(i, 2))
                                                                Else

                                                                    mkstr = Trim(mkstr) & Trim(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")))
                                                                End If
                                                            Else
                                                                '***grp
                                                                'If Trim(flx.get_TextMatrix(i, 19)) <> "N" Then
                                                                '    If Mid(Trim(DR1.Item("ldata")), 2, 1) = "$" Then
                                                                '        mkstr = mkstr & Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 3, 59)))
                                                                '    Else
                                                                '        mkstr = mkstr & Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))
                                                                '    End If

                                                                'Else
                                                                '    If Mid(Trim(DR1.Item("ldata")), 2, 1) <> "$" Then
                                                                '        mkstr = mkstr & Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))
                                                                '    End If

                                                                'End If
                                                                If Trim(flx.get_TextMatrix(i, 19)) = "N" Then
                                                                    If Mid(Trim(DR1.Item("ldata")), 2, 1) = "$" Then
                                                                        mkstr = mkstr '& Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 3, 59)))
                                                                    Else
                                                                        mkstr = mkstr & Trim(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")))
                                                                    End If

                                                                Else
                                                                    'If Mid(Trim(DR1.Item("ldata")), 2, 1) <> "$" Then
                                                                    mkstr = mkstr & Trim(DR4.Item(Replace(Mid(Trim(DR1.Item("ldata")), 2, 59), "$", "")))
                                                                    'Else
                                                                    'mkstr = mkstr & Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 3, 59)))
                                                                    'End If

                                                                End If



                                                                'mkstr = Trim(mkstr) & Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))
                                                            End If
                                                            If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                                                mkstr = mkstr & Trim(DR1.Item("secdet"))
                                                            End If
                                                        End If
                                                    End If
                                                    '*
                                                End If
                                                '**
                                                '    mkstr = mkstr & Trim(DR4.Item(Mid(Trim(DR1.Item("ldata")), 2, 59)))
                                            Else

                                                If Mid(Trim(DR1.Item("ldata")), 1, 1) = "$" Then

                                                    If Trim(flx.get_TextMatrix(i, 19)) = "N" Then
                                                        mkstr = Trim(DR1.Item("firstdet"))
                                                        If IsDBNull(DR1.Item("lrow")) = False Then
                                                            If Val(DR1.Item("lrow")) > 0 Then
                                                                mkstr = mkstr & Trim((Val(DR1.Item("lrow")) + Val(mkrow))) & ","
                                                            End If
                                                        End If

                                                        If IsDBNull(DR1.Item("lcol")) = False Then
                                                            If Val(DR1.Item("lcol")) > 0 Then
                                                                mkstr = mkstr & Trim((Val(DR1.Item("lcol")) + Val(mkcol))) & ","
                                                            End If
                                                        End If

                                                        If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                            mkstr = Trim(mkstr) & Trim(DR1.Item("fontdet"))
                                                        End If
                                                        mkstr = mkstr & Trim(Mid(Trim(DR1.Item("ldata")), 2, 59))
                                                        'mkstr = mkstr & Trim(DR1.Item("ldata"))
                                                        If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                                            mkstr = mkstr & Trim(DR1.Item("secdet"))
                                                        End If

                                                        'mkstr = Trim(mkstr) & Trim(DR1.Item("ldata"))
                                                    End If
                                                Else
                                                    mkstr = Trim(DR1.Item("firstdet"))
                                                    If IsDBNull(DR1.Item("lrow")) = False Then
                                                        If Val(DR1.Item("lrow")) > 0 Then
                                                            mkstr = mkstr & Trim((Val(DR1.Item("lrow")) + Val(mkrow))) & ","
                                                        End If
                                                    End If

                                                    If IsDBNull(DR1.Item("lcol")) = False Then
                                                        If Val(DR1.Item("lcol")) > 0 Then
                                                            mkstr = mkstr & Trim((Val(DR1.Item("lcol")) + Val(mkcol))) & ","
                                                        End If
                                                    End If

                                                    If Len(Trim(DR1.Item("fontdet"))) > 0 Then
                                                        mkstr = Trim(mkstr) & Trim(DR1.Item("fontdet"))
                                                    End If
                                                    mkstr = mkstr & Trim(DR1.Item("ldata"))
                                                    If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                                        mkstr = mkstr & Trim(DR1.Item("secdet"))
                                                    End If

                                                    'mkstr = Trim(mkstr) & Trim(DR1.Item("ldata"))
                                                End If

                                            End If
                                            'mkstr = mkstr & Trim(DR4.Item(Trim(DR1.Item("ldata"))))
                                        End While
                                    Else
                                        'mkstr = mkstr & Trim(DR1.Item("ldata"))
                                        DR4.Close()
                                        cmd4.Dispose()
                                    End If

                                    'mkstr = mkstr & trim(BS.Fields(ps!ldata))
                                    'mkstr = mkstr & Trim(DR1.Item("ldata"))
                                End If
                                'If Len(Trim(DR1.Item("secdet"))) > 0 Then
                                ' mkstr = mkstr & Trim(DR1.Item("secdet"))
                                ' End If
                                If Len(Trim(mkstr)) > 0 Then
                                    PrintLine(1, TAB(0), mkstr)
                                End If
                                mkstr = ""
                            End While

                            If Val(txtstickcol.Text) > 1 Then
                                If Val(txtrow.Text) > 0 Then
                                    mkrow = mkrow + Val(txtrow.Text)
                                End If
                                If Val(txtcol.Text) > 0 Then
                                    mkcol = mkcol + Val(txtcol.Text)
                                End If
                            End If

                        End If
                        DR1.Close()

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                    CMD1.Dispose()
                    mksno = mksno + 1

                    'msql2 = "SELECT  [docentry],[date],[linenum],[headtype],[firstdet],[lrow],[lcol],[fontdet],[ldata],[secdet] FROM [rhlinventry13].[dbo].[bardet]where docentry=" & Val(txtdocno.Text) & " and headtype='F' order by docentry,linenum"



                    'footer
                    mksno2 = mksno
                    If mksno = Val(txtstickcol.Text) Then
                        msql2 = "SELECT  [docentry],[date],[linenum],[headtype],[firstdet],[lrow],[lcol],[fontdet],[ldata],[secdet] FROM bardet where docentry=" & Val(txtdocno.Text) & " and headtype='F' order by docentry,linenum"
                        Dim CMD2 As New OleDb.OleDbCommand(msql2, con)
                        If con.State = ConnectionState.Closed Then
                            con.Open()
                        End If


                        Dim DR2 As OleDb.OleDbDataReader
                        DR2 = CMD2.ExecuteReader
                        If DR2.HasRows = True Then
                            While DR2.Read
                                'mkrow = Val(DR1.Item("lrow"))
                                mkstr = Trim(DR2.Item("firstdet"))
                                If Len(Trim(DR2.Item("ldata"))) > 0 Then
                                    mkstr = mkstr & Trim(DR2.Item("ldata"))
                                Else
                                    mkstr = mkstr

                                End If
                                If Len(Trim(DR2.Item("secdet"))) > 0 Then
                                    mkstr = mkstr & Trim(DR2.Item("secdet"))
                                End If

                                PrintLine(1, TAB(0), mkstr)
                                mkstr = ""
                            End While

                            'If Val(txtstickcol.Text) > 1 Then
                            '    mkrow = mkrow + Val(txtrow.Text)
                            'End If
                        End If
                        DR2.Close()

                        'If mksno = Val(txtstickcol.Text) Then
                        If Val(txtstickcol.Text) > 1 Then
                            mkrow = mkrow + Val(txtrow.Text)
                        End If
                        msql = "SELECT  [docentry],[date],[linenum],[headtype],[firstdet],[lrow],[lcol],[fontdet],[ldata],[secdet] FROM bardet where docentry=" & Val(txtdocno.Text) & " and headtype='H' order by docentry,linenum"
                        'Header
                        Dim CMDh As New OleDb.OleDbCommand(msql, con)
                        If con.State = ConnectionState.Closed Then
                            con.Open()
                        End If

                        Dim DRh As OleDb.OleDbDataReader
                        DRh = CMDh.ExecuteReader
                        If DRh.HasRows = True Then
                            While DRh.Read
                                PrintLine(1, TAB(0), DRh.Item("firstdet"))
                            End While
                        End If
                        DRh.Close()
                        CMDh.Dispose()
                        mkrow = 0
                        mkcol = 0
                        mksno = 0
                    End If


                Next k
            End If

        Next i

        'If (Microsoft.VisualBasic.Format((mksno2 / Val(txtstickcol.Text)), "######0.00") - Microsoft.VisualBasic.Format((mksno2 / Val(txtstickcol.Text)), "######0")) > 0 Then
        ' MsgBox("test")
        ' End If
        If mksno2 = 1 Then
            msql2 = "SELECT  [docentry],[date],[linenum],[headtype],[firstdet],[lrow],[lcol],[fontdet],[ldata],[secdet] FROM bardet where docentry=" & Val(txtdocno.Text) & " and headtype='F' order by docentry,linenum"
            Dim CMD6 As New OleDb.OleDbCommand(msql2, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If


            Dim DR6 As OleDb.OleDbDataReader
            DR6 = CMD6.ExecuteReader
            If DR6.HasRows = True Then
                While DR6.Read
                    'mkrow = Val(DR1.Item("lrow"))
                    mkstr = Trim(DR6.Item("firstdet"))
                    If Len(Trim(DR6.Item("ldata"))) > 0 Then
                        mkstr = mkstr & Trim(DR6.Item("ldata"))
                    Else
                        mkstr = mkstr

                    End If
                    If Len(Trim(DR6.Item("secdet"))) > 0 Then
                        mkstr = mkstr & Trim(DR6.Item("secdet"))
                    End If

                    PrintLine(1, TAB(0), mkstr)
                    mkstr = ""
                End While
                If Val(txtstickcol.Text) > 1 Then
                    mkrow = mkrow + Val(txtrow.Text)
                End If
            End If
            DR6.Close()
            mksno2 = 0
        End If


        'k = 1

        'If sno <> 1 Then
        '    PrintLine(1, TAB(0), "P1")
        'End If

        FileClose(1)

        If chkprndir.Checked = False Then
            If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))

            End If
        End If

        'Shell("print /d:LPT1 c:\sbarcode.txt", vbNormalFocus)

        ''Shell("print c:\sbarcode.txt.zpl")

    End Sub

    Private Sub cmbprnon_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbprnon.LostFocus

        If Trim(cmbprnon.Text) = "SALES" Then
            mfil = "INV1"
            mfildet = "OINV"
        ElseIf Trim(cmbprnon.Text) = "DATE ORDER" Then
            mfil = "DLN1"
            mfildet = "ODLN"
        ElseIf Trim(cmbprnon.Text) = "INV DRAFT" Then
            mfil = "DRF1"
            mfildet = "ODRF"
        ElseIf Trim(cmbprnon.Text) = "SAMPLE" Then
            mfil = "V_sampinv1"
            mfildet = "v_sampOinv"
        End If
    End Sub

    Private Sub cmbprnon_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbprnon.SelectedIndexChanged

        If Trim(cmbprnon.Text) = "SALES" Then
            mfil = "INV1"
            mfildet = "OINV"
        ElseIf Trim(cmbprnon.Text) = "DATE ORDER" Then
            mfil = "DLN1"
            mfildet = "ODLN"
        ElseIf Trim(cmbprnon.Text) = "INV DRAFT" Then
            mfil = "DRF1"
            mfildet = "ODRF"
        ElseIf Trim(cmbprnon.Text) = "SAMPLE" Then
            mfil = "V_sampinv1"
            mfildet = "v_sampOinv"
        End If
        'Call crtmptab()
        Call crtmptabst()
    End Sub

    Private Sub txtdocno_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtdocno.KeyUp
        If e.KeyCode = Keys.F2 Then
            Call flxhead()

            If flxc.Visible = False Then flxc.Visible = True
            flxc.Row = 1
            flxc.Col = 0
            flxc.Focus()
        End If
    End Sub

    Private Sub txtdocno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdocno.TextChanged

    End Sub

    Private Sub flx_KeyUpEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyUpEvent) Handles flx.KeyUpEvent
        If e.keyCode = 116 Then
            For i = 1 To flx.Rows - 1
                If Len(Trim(flx.get_TextMatrix(i, 0))) = 0 Then
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
            Next i

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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        For i = 1 To flx.Rows - 1
            flx.set_TextMatrix(i, 4, 1)
        Next
    End Sub

    Private Sub DisplaySqlErrors(ByVal exception As OleDbException)
        Dim i As Integer

        For i = 0 To exception.Errors.Count - 1
            Console.WriteLine("Index #" & i & ControlChars.NewLine & _
                "Error: " & exception.Errors(i).ToString() & ControlChars.NewLine)
        Next i
        Console.ReadLine()
    End Sub

    Private Sub txtbno_StyleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtbno.StyleChanged

    End Sub

    Private Sub txtbno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbno.TextChanged

    End Sub

    Private Sub cmbtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtype.SelectedIndexChanged

    End Sub

    Private Sub CButton1_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles CButton1.ClickButtonArea
        If MsgBox("R U Sure!", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
            msql4 = "delete from bartemp "
            'If Trim(cmbprnon.Text) = "INV DRAFT" Then
            ' msql4 = msql4 & " where docentry=" & Val(txtbno.Text) & vbCrLf
            ' Else
            ' msql4 = msql4 & " where docnum=" & Val(txtbno.Text) & vbCrLf
            ' End If

            Dim dCMD As New OleDb.OleDbCommand(msql4, con)
            'Dim CMD2 As New SqlClient.SqlCommand("DELETE FROM BPORD WHERE BNO=" & Val(txtbno.Text), con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            dCMD.ExecuteNonQuery()


            Try

                dCMD.Dispose()
                'CMD2.Dispose()
            Catch ex As Exception
                'TRANS.Rollback()
                MsgBox(ex.Message)
                dCMD.Dispose()
                'CMD2.Dispose()
            End Try

            'TRANS.Commit()
            dCMD.Dispose()
            dCMD = Nothing
        End If


    End Sub

    Private Sub cmbyr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbyr.SelectedIndexChanged

    End Sub

    'batch qry
    '    msql="  select  

    't0.DocEntry,t0.docnum,t1.linenum,t0.docdate,t0.cardcode,t0.cardname,t1.itemcode,t1.Dscri

    'ption as quality,k.u_size,
    '  case when bt.quantity is not null then convert(decimal(16),bt.quantity) else  

    'convert(decimal(16),t1.quantity) end as quantity,
    'floor(convert(decimal(16),t1.quantity)/cast(k.salpackun as int)) as boxqty,cr.state, 

    't1.Dscription u_catalgcode,t5.ItmsGrpNam u_itemgrp, k.salunitmsr as uom,
    'case when bb.pcs>0 then cast(bb.pcs as int) else cast(k.salpackun as int) end as box, 
    'case when  CHARINDEX('CMS',k.slength2)<=0 then cast(isnull(k.slength2,0) as float) else 

    '0 end  as u_length, 
    ' cast(case when  CHARINDEX('CMS',k.swidth1)<=0 then cast(isnull(k.swidth1,0.0)as float) 

    'else 0.0 end as float)  as u_width,  k.u_style, convert(decimal(11,2),t4.price) as 

    'rate,cast((k.salpackun*t1.AssblValue) as decimal(7,2)) as boxmrp, 
    ' cast(t1.AssblValue as decimal(7,2)) as mrp,  k.u_scode U_Remarks, k.u_subgrp1 u_brand, 

    'k.invntitem, 
    '' ' u_prntname,  
    '--ltrim(rtrim(right(YEAR(t0.docdate),2)))+right('0'+rtrim(ltrim(MONTH(t0.docdate))),2)+r

    'trim(ltrim(convert(nvarchar(100),k.u_scode))) as autocode,

    '   CASE when   CHARINDEX('CMS',k.slength2)<=0 and cast(k.slength2 as real)>0 and 

    '(rtrim(upper(t5.ItmsGrpNam))in ( select ItmsGrpNam from OITM b LEFT join OITB c on 

    'c.ItmsGrpCod = b.ItmsGrpCod where CHARINDEX('CMS',u_size)<=0 group by ItmsGrpNam) 
    '   OR CHARINDEX('DHOTI',UPPER(t5.ItmsGrpNam))>0) then  

    'RTRIM(k.swidth1)+'cmX'+rtrim(lTRIM(cast(cast(k.slength2 as real)*100 as char(10))))
    '    +'cm' else  ltrim(convert(varchar,round(cast(replace(k.slength2,'CMS','') as 

    'real)/100,2)))
    '    +'mX' + rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end as 

    'msize, 
    '                      CASE when CHARINDEX('CMS',k.slength2)<=0  and cast(k.slength2 as 

    'real)>0 
    '                      and rtrim(upper(t5.ItmsGrpNam)) 
    '                      in( select ItmsGrpNam from OITM b LEFT join OITB c on c.ItmsGrpCod 

    '= b.ItmsGrpCod
    '                       where CHARINDEX('CMS',u_size)<=0 
    '                      group by ItmsGrpNam) then  

    'RTRIM(k.swidth1)+'cmX'+rtrim(lTRIM(cast(cast(k.slength2 as real)*100 as char(10)))) 
    '                      +'cm' else  k.slength2 end as msize2,'MFD:' + 

    'substring(datename(m,GETDATE()),1,3) +' '+ CAST(DATEPART(YYYY,GETDATE()) as CHAR(4)) as 

    'mfd, 
    '(left(convert(varchar,t0.docnum) + '00000',5)+left(convert(varchar,t0.cardcode) 

    '+'0000000000000',13) + 
    'left(convert(varchar,convert(decimal(16),t4.price)) + '00000',5)) as barcode, 
    '(ltrim(convert(varchar,t0.docnum)) + 

    'rtrim(convert(varchar,t0.cardcode))+ltrim(convert(varchar,convert(decimal(16),t4.price))

    ')) as barcode1,
    'ltrim(convert(varchar,t0.docnum)) as 

    'barcode2,ltrim(convert(varchar,t0.docnum))+rtrim(t1.itemcode) as barcode3,
    'ltrim(convert(varchar,t0.docentry))+rtrim(t1.itemcode) as drbarcode,
    'bb.dhoti,bb.shirting,bb.suiting,bb.towel,
    'bb.pcs as bompcs,
    'k.u_subgrp6,

    'case when cl.colorname  is Not null then cl.colorname collate SQL_Latin1_General_CP850_CI_AS  else k.u_subgrp5 end  color,convert(nvarchar(max),t1.text) as mtrstot  ,
    't1.Dscription u_catalogname,  
    '  ltrim(rtrim(right(YEAR(t0.docdate),2)))+right('0'+rtrim(ltrim(MONTH(t0.docdate))),2)+  

    'rtrim(ltrim(convert(nvarchar(100),K.U_SCODE)))+ltrim(bt.batchnum) as autocode, 
    ' cc.dhoti contdhoti,cc.shirting contshirting,cc.suiting contsuiting,cc.towel conttowel,
    ' cc.perfume,cc.belt,cc.rate contrate,cc.pcs contpcs,bt.BatchNum 
    ' from INV1 t1
    'Left join oinv t0 on t0.docentry = t1.docentry
    'Left join OCRD t2 on t0.CardCode = t2.CardCode
    'Left join CRD1 cr on cr.CardCode = t2.CardCode and cr.AdresType = 'B'
    'Left join OITM k on k.ItemCode = t1.itemcode
    'left join barcodebom as bb with (nolock) on rtrim(BB.code)=rtrim(t1.itemcode) 
    'Left join (select * from ITM1) t4 on k.ItemCode = t4.ItemCode and t2.ListNum = 

    't4.PriceList
    'Left join OITB t5 on t5.ItmsGrpCod = k.ItmsGrpCod
    'left join contentbom as cc with (nolock) on cc.code=t1.itemcode 
    'left join (select 

    'docdate,itemcode,batchnum,whscode,itemname,BaseType,BaseEntry,BaseNum,BaseLinNum,sum(Qua

    'ntity) quantity from ibt1 with (nolock) 
    ' where basetype in (15,13) and whscode ='SALGOODS'
    ' group by 

    'docdate,itemcode,batchnum,whscode,itemname,BaseType,BaseEntry,BaseNum,BaseLinNum) bt on 

    'bt.BaseType=t1.BaseType and bt.BaseEntry=t1.BaseEntry and bt.BaseLinNum=t1.BaseLine
    'left join RRCOLOR.dbo.colormast cl on cl.colcode collate SQL_Latin1_General_CP850_CI_AS =bt.BatchNum
    'where DocNum = 1219 and datepart(mm,t0.docdate)=4 and datepart(yyyy,t0.docdate)=2016  

    'AND T1.ItemCode <> 'FrightCharges'
    'order by t1.linenum  "
End Class
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.ReportSource
'Imports QRCoder
Imports System.Text
Imports System.Data.DataTable
Imports System.Drawing.Printing



Public Class Frmpackage
    Dim msql, msql2, msql3, msql4, msql5, mdir, mkstr As String
    Dim mdocno As Long
    Dim j, i, msel As Int32
    Dim mktru As Boolean
    Dim mreportname As String
    Private Sub Frmpackage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Call main()
        Me.Height = MDIParent1.Height
        Me.Width = My.Computer.Screen.Bounds.Width

        cmbtype.Items.Add("SALES")
        cmbtype.Items.Add("DATE ORDER")
        cmbtype.Text = "SALES"

        mskdate.Text = Microsoft.VisualBasic.Format(Now, "dd-MM-yyyy")

        Call flxHhead()
        Call flxchead2()
        Call flxphead2()
        flxc.Rows = flxc.Rows + 1
        flxc.Row = flxc.Rows - 1

        flxc.Rows = flxc.Rows + 1
        flxc.Row = flxc.Rows - 1
        flxc.Rows = flxc.Rows + 1
        flxc.Row = flxc.Rows - 1
        flxc.Rows = flxc.Rows + 1
        flxc.Row = flxc.Rows - 1
        'For j = 1 To 100
        '    cmbboxno.Items.Add(j)
        'Next
        loadcombo("opkg", "pkgtype", cmbboxtype, "pkgtype")
        loadcombo("owgt", "unitname", cmbwgt, "unitname")
        cmbboxtype.Text = "BUNDLE"
        cmbwgt.Text = "Kilogramme"
        ' loadcombo("ofpr", "code", cmbyear, "code")
        loadcombo("ofpr", "indicator", cmbyear, "indicator")
        cmbyear.Text = mperiod
        ' mProdMktbarcode
        If mProdMktbarcode = "1" Then
            chkprod.Checked = True
        Else
            chkprod.Checked = False
        End If

    End Sub
    Private Sub deldata()
        deletedata("inv7", "docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text))

        deletedata("inv8", "docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text))
    End Sub
    Private Sub loadno()
        'If cmbtype.Text = "DATE ORDER" Then
        '    msql = "select b.DocNum,b.DocDate, b.pIndicator,DATEPART(mm,b.DocDate) as mkmon,DATEPART(yyyy,b.docdate) as yr from odln b with (nolock) left join OFPR p on p.Indicator=b.PIndicator where b.DocNum=" & Val(txtno.Text) & " and p.Code='" & Trim(cmbyear.Text) & "'"
        'Else
        '    msql = "select b.DocNum,b.DocDate, b.pIndicator,DATEPART(mm,b.DocDate) as mkmon,DATEPART(yyyy,b.docdate) as yr from oinv b with (nolock) left join OFPR p on p.Indicator=b.PIndicator where b.DocNum=" & Val(txtno.Text) & " and p.Code='" & Trim(cmbyear.Text) & "'"
        'End If

        If cmbtype.Text = "DATE ORDER" Then
            msql = "select b.DocNum,b.DocDate, b.pIndicator,DATEPART(mm,b.DocDate) as mkmon,DATEPART(yyyy,b.docdate) as yr,isnull(u_ordtype,'') u_ordtype from odln b with (nolock) left join OFPR p on p.Indicator=b.PIndicator where b.DocNum=" & Val(txtno.Text) & " and p.indicator='" & Trim(cmbyear.Text) & "'"
        Else
            msql = "select b.DocNum,b.DocDate, b.pIndicator,DATEPART(mm,b.DocDate) as mkmon,DATEPART(yyyy,b.docdate) as yr,isnull(u_ordtype,'') u_ordtype from oinv b with (nolock) left join OFPR p on p.Indicator=b.PIndicator where b.DocNum=" & Val(txtno.Text) & " and p.indicator='" & Trim(cmbyear.Text) & "'"
        End If


        'Header
        'Dim CMD As New OleDb.OleDbCommand(msql, con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'Dim table As New DataTable
        'Dim da As New OleDb.OleDbDataAdapter(CMD)
        'da.Fill(table)
        Dim dt As DataTable = getDataTable(msql)

        For Each row As DataRow In dt.Rows
            lbldate.Text = Format(row("docdate"), "dd-MM-yyyy")
            If row("u_ordtype") = "SO" Then
                chkscheme.Checked = True
            Else
                chkscheme.Checked = False
            End If
        Next
        dt.Dispose()
        'Dim DR2 As OleDb.OleDbDataReader

        'Try
        '    DR2 = CMD.ExecuteReader
        '    DR2.Read()
        '    If DR2.HasRows = True Then
        '        While DR2.Read
        '            lbldate.Text = Format(DR2.Item("docdate"), "dd-MM-yyyy")
        '        End While
        '    End If
        '    DR2.Close()
        '    CMD.Dispose()
        'Catch EX As Exception
        '    MsgBox(EX.Message)
        'End Try

    End Sub
    Private Sub loadinv()

        lblptot.Text = 0
        lblctot.Text = 0
        If cmbtype.Text = "DATE ORDER" Then
            msql = "select docentry,docnum,docdate,cardname,doctotal from odln where docnum=" & Microsoft.VisualBasic.Val(txtno.Text) & " and docdate='" & Microsoft.VisualBasic.Format(CDate(lbldate.Text), "yyyy-MM-dd") & "'"
        Else
            msql = "select docentry,docnum,docdate,cardname,doctotal from oinv where docnum=" & Microsoft.VisualBasic.Val(txtno.Text) & " and docdate='" & Microsoft.VisualBasic.Format(CDate(lbldate.Text), "yyyy-MM-dd") & "'"

        End If



        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim CMD As New SqlCommand(msql, con)
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()

        Try
            ''Dim DR As SqlDataReader
            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader
            'DR.Read()
            If DR.HasRows = True Then

                While DR.Read

                    lbldocentry.Text = DR.Item("docentry")
                    lbldate2.Text = DR.Item("docdate")
                    lblparty.Text = DR.Item("cardname") & vbNullString
                    lblamt.Text = DR.Item("doctotal") & vbNullString
                    mskdate.Text = Microsoft.VisualBasic.Format(Now, "dd-MM-yyyy")
                End While
            Else
                lbldocentry.Text = ""
                lbldate2.Text = ""
                lblparty.Text = ""
                lblamt.Text = ""
            End If
            DR.Close()
            CMD.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub loadflxc()
        'msql = "select  docentry,itemcode,U_CatalogCode as itemname,u_style as style,U_Size as size,sum(quantity) quantity from RHL160714..inv1 " & vbCrLf _
        ' & "where DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & "  group by docentry,itemcode,U_CatalogCode,u_style,U_Size order by ItemCode"
        If chkprod.Checked = True Then
            If cmbtype.Text = "DATE ORDER" Then
                msql = "select  linenum,docentry,itemcode,dscription,sum(quantity) quantity from dln1 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by linenum,docentry,itemcode,dscription,unitmsr order by linenum"
            Else
                ''msql = "select  docentry,itemcode,dscription,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from inv1 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by docentry,itemcode,dscription,unitmsr"
                'msql = "select linenum, docentry,itemcode,dscription,sum(quantity)  quantity from inv1 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by linenum,docentry,itemcode,dscription,unitmsr order by linenum"
                msql = "select linenum, docentry,itemcode,dscription,sum(quantity)  quantity from inv1 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by docentry,itemcode,dscription,unitmsr order by itemcode"
            End If
        Else
            If cmbtype.Text = "DATE ORDER" Then
                msql = "select linenum, docentry,itemcode,dscription,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from dln1 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by linenum,docentry,itemcode,dscription,unitmsr order by linenum"
            Else
                msql = "select linenum, docentry,itemcode,dscription,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from inv1 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by linenum,docentry,itemcode,dscription,unitmsr order by linenum"
                'msql = "select  docentry,itemcode,dscription,sum(quantity)  quantity from inv1 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by docentry,itemcode,dscription,unitmsr"
            End If

        End If
        'If cmbtype.Text = "DATE ORDER" Then
        '    msql = "select  docentry,itemcode,dscription,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from dln1 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by docentry,itemcode,dscription,unitmsr"
        'Else
        '    'msql = "select  docentry,itemcode,dscription,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from inv1 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by docentry,itemcode,dscription,unitmsr"
        '    msql = "select  docentry,itemcode,dscription,sum(quantity)  quantity from inv1 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by docentry,itemcode,dscription,unitmsr"
        'End If

        Dim CMD As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()

        Call flxchead()
        'Call flxchead2()
        Try
            ''Dim DR As SqlDataReader
            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                With flxc
                    While DR.Read
                        .Rows = .Rows + 1
                        .Row = .Rows - 1
                        .set_TextMatrix(.Row, 1, DR.Item("itemcode") & vbNullString)
                        .set_TextMatrix(.Row, 2, Microsoft.VisualBasic.Format(DR.Item("quantity"), "#######0.00"))


                        '.set_TextMatrix(.Row, 1, DR.Item("itemcode") & vbNullString)
                        '.set_TextMatrix(.Row, 2, DR.Item("Itemname"))
                        '.set_TextMatrix(.Row, 3, DR.Item("Style") & vbNullString)
                        '.set_TextMatrix(.Row, 4, DR.Item("Size") & vbNullString)
                        '.set_TextMatrix(.Row, 5, DR.Item("quantity"))


                        'lbldocentry.Text = DR.Item("docentry")
                        'lbldate.Text = DR.Item("docdate")
                        'lblparty.Text = DR.Item("cardname") & vbNullString
                        'lblamt.Text = DR.Item("doctotal") & vbNullString
                    End While
                    .Rows = .Rows + 1
                    .Row = .Rows - 1
                End With
                Call flxctot()
                DR.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        CMD.Dispose()
    End Sub

    Private Sub loadflxc2()
        lblctot.Text = 0
        lblptot.Text = 0

        If chkscheme.Checked = True Then
            If chkprod.Checked = True Then
                If cmbtype.Text = "DATE ORDER" Then
                    msql = "select  t0.linenum,t0.docentry,t0.itemcode,t0.dscription as itemname,it.u_style as style,it.U_Size as size, sum(quantity)  quantity from dln1 t0 " & vbCrLf _
                         & " left join oitm it on it.itemcode=t0.itemcode " & vbCrLf _
                    & "where t0.DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and t0.treetype<>'I'  group by   t0.linenum,t0.docentry,t0.itemcode,t0.dscription,it.u_style,it.U_Size,t0.unitmsr order by  t0.linenum, t0.dscription"
                Else
                    'msql = "select    t0.linenum,t0.docentry,t0.itemcode,t0.dscription as itemname,it.u_style as style,it.U_Size as size, sum(quantity) quantity from inv1 t0 " & vbCrLf _
                    '& " left join oitm it on it.itemcode=t0.itemcode " & vbCrLf _
                    ' & "where t0.DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and t0.treetype<>'I'  group by   t0.linenum,t0.docentry,t0.itemcode,t0.dscription,it.u_style,it.U_Size,t0.unitmsr order by  t0.linenum,t0.dscription"


                    msql = "select  t0.docentry,t0.itemcode,t0.dscription as itemname,it.u_style as style,it.U_Size as size, sum(quantity) quantity from inv1 t0 " & vbCrLf _
                                       & " left join oitm it on it.itemcode=t0.itemcode " & vbCrLf _
                                        & "where t0.DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and t0.treetype<>'I'  group by  t0.docentry,t0.itemcode,t0.dscription,it.u_style,it.U_Size,t0.unitmsr order by t0.dscription"

                End If
            Else
                If cmbtype.Text = "DATE ORDER" Then
                    msql = "select  linenum,docentry,itemcode,U_CatalogCode as itemname,u_style as style,U_Size as size,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from dln1 " & vbCrLf _
                    & "where DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by linenum,docentry,itemcode,U_CatalogCode,u_style,U_Size,unitmsr order by u_catalogcode"
                Else
                    msql = "select  linenum,docentry,itemcode,U_CatalogCode as itemname,u_style as style,U_Size as size,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from inv1 " & vbCrLf _
                     & "where DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by linenum,docentry,itemcode,U_CatalogCode,u_style,U_Size,unitmsr order by u_catalogcode"
                End If
            End If

        Else

            If chkprod.Checked = True Then
                If cmbtype.Text = "DATE ORDER" Then
                    msql = "select  t0.linenum, t0.docentry,t0.itemcode,t0.dscription as itemname,it.u_style as style,it.U_Size as size, sum(quantity)  quantity from dln1 t0 " & vbCrLf _
                         & " left join oitm it on it.itemcode=t0.itemcode " & vbCrLf _
                    & "where t0.DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and t0.treetype<>'I'  group by   t0.linenum, t0.docentry,t0.itemcode,t0.dscription,it.u_style,it.U_Size,t0.unitmsr order by  t0.linenum, t0.dscription"
                Else
                    'msql = "select    t0.linenum, t0.docentry,t0.itemcode,t0.dscription as itemname,it.u_style as style,it.U_Size as size, sum(quantity) quantity from inv1 t0 " & vbCrLf _
                    '& " left join oitm it on it.itemcode=t0.itemcode " & vbCrLf _
                    ' & "where t0.DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and t0.treetype<>'I'  group by   t0.linenum, t0.docentry,t0.itemcode,t0.dscription,it.u_style,it.U_Size,t0.unitmsr order by  t0.linenum,t0.dscription"

                    msql = "select  t0.docentry,t0.itemcode,t0.dscription as itemname,it.u_style as style,it.U_Size as size, sum(quantity) quantity from inv1 t0 " & vbCrLf _
                   & " left join oitm it on it.itemcode=t0.itemcode " & vbCrLf _
                    & "where t0.DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and t0.treetype<>'I'  group by  t0.docentry,t0.itemcode,t0.dscription,it.u_style,it.U_Size,t0.unitmsr order by t0.dscription"
                End If
            Else
                If cmbtype.Text = "DATE ORDER" Then
                    msql = "select   linenum, docentry,itemcode,U_CatalogCode as itemname,u_style as style,U_Size as size,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from dln1 " & vbCrLf _
                    & "where DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by linenum,docentry,itemcode,U_CatalogCode,u_style,U_Size,unitmsr order by  linenum,u_catalogcode"
                Else  'linenum
                    'msql = "select linenum, docentry,itemcode,U_CatalogCode as itemname,u_style as style,U_Size as size,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from inv1 " & vbCrLf _
                    ' & "where DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by linenum,docentry,itemcode,U_CatalogCode,u_style,U_Size,unitmsr order by linenum,u_catalogcode"

                    msql = "select  linenum,docentry,itemcode,U_CatalogCode as itemname,u_style as style,U_Size as size,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from inv1 " & vbCrLf _
                    & "where DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by linenum,docentry,itemcode,U_CatalogCode,u_style,U_Size,unitmsr order by u_catalogcode"

                End If

            End If
        End If
        'If cmbtype.Text = "DATE ORDER" Then
        '    msql = "select  docentry,itemcode,U_CatalogCode as itemname,u_style as style,U_Size as size,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from dln1 " & vbCrLf _
        '    & "where DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by docentry,itemcode,U_CatalogCode,u_style,U_Size,unitmsr order by  u_catalogcode"
        'Else
        '    msql = "select  docentry,itemcode,U_CatalogCode as itemname,u_style as style,U_Size as size,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from inv1 " & vbCrLf _
        '     & "where DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by docentry,itemcode,U_CatalogCode,u_style,U_Size,unitmsr order by u_catalogcode"
        'End If
        'msql = "select  docentry,itemcode,dscription,sum(quantity) quantity from inv1 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by docentry,itemcode,dscription"

        Dim CMD As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()

        'Call flxchead()
        Call flxchead2()
        Try
            ''Dim DR As SqlDataReader
            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                With flxc
                    While DR.Read
                        .Rows = .Rows + 1
                        .Row = .Rows - 1
                        .set_TextMatrix(.Row, 1, DR.Item("itemcode") & vbNullString)
                        .set_TextMatrix(.Row, 2, Microsoft.VisualBasic.Format(DR.Item("quantity"), "########0.00"))



                        .set_TextMatrix(.Row, 4, DR.Item("Itemname") & vbNullString)
                        .set_TextMatrix(.Row, 5, DR.Item("Style") & vbNullString)
                        .set_TextMatrix(.Row, 6, DR.Item("Size") & vbNullString)
                        '.set_TextMatrix(.Row, 5, DR.Item("quantity"))


                        'lbldocentry.Text = DR.Item("docentry")
                        'lbldate.Text = DR.Item("docdate")
                        'lblparty.Text = DR.Item("cardname") & vbNullString
                        'lblamt.Text = DR.Item("doctotal") & vbNullString
                    End While
                    .Rows = .Rows + 1
                    .Row = .Rows - 1
                End With
                Call flxctot()
                DR.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        CMD.Dispose()
    End Sub

    Private Sub loadexists()
        'msql = "select  docentry,itemcode,U_CatalogCode as itemname,u_style as style,U_Size as size,sum(quantity) quantity from RHL160714..inv1 " & vbCrLf _
        ' & "where DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & "  group by docentry,itemcode,U_CatalogCode,u_style,U_Size order by ItemCode"

        Call loadhead()
        loadcombo("opkg", "pkgtype", cmbboxtype, "pkgtype")
        loadcombo("owgt", "unitname", cmbwgt, "unitname")


        If cmbtype.Text = "DATE ORDER" Then
            msql = "select  docentry,packagenum,itemcode,quantity,catalogname,u_style,u_size from rdln8 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text)
        Else
            msql = "select  docentry,packagenum,itemcode,quantity,catalogname,u_style,u_size from rinv8 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text)
        End If
        'msql = "select  docentry,packagenum,itemcode,quantity,catalogname,u_style,u_size from rinv8 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text)

        'Dim CMD As New SqlCommand(msql, con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'Call flxphead2()
        ''Call flxchead2()
        'Try
        '    ''Dim DR As SqlDataReader
        '    Dim DR As SqlDataReader = Nothing
        '    DR = CMD.ExecuteReader
        '    If DR.HasRows = True Then
        '        mktru = True
        '        cmdupdt.Enabled = False
        '        With flxp
        '            While DR.Read
        '                .Rows = .Rows + 1
        '                .Row = .Rows - 1

        '                .set_TextMatrix(.Row, 0, DR.Item("packagenum") & vbNullString)
        '                .set_TextMatrix(.Row, 1, DR.Item("itemcode") & vbNullString)
        '                .set_TextMatrix(.Row, 2, Microsoft.VisualBasic.Format(DR.Item("quantity"), "#######0.00"))
        '                .set_TextMatrix(.Row, 3, DR.Item("docentry"))


        '                '.set_TextMatrix(.Row, 1, DR.Item("itemcode") & vbNullString)
        '                .set_TextMatrix(.Row, 4, DR.Item("Catalogname"))
        '                .set_TextMatrix(.Row, 5, DR.Item("u_Style") & vbNullString)
        '                .set_TextMatrix(.Row, 6, DR.Item("u_Size") & vbNullString)
        '                '.set_TextMatrix(.Row, 5, DR.Item("quantity"))

        '            End While
        '            .Rows = .Rows + 1
        '            .Row = .Rows - 1
        '        End With
        '    Else
        '        mktru = False
        '        loadcombo("opkg", "pkgtype", cmbboxtype, "pkgtype")
        '        loadcombo("owgt", "unitname", cmbwgt, "unitname")
        '        cmdupdt.Enabled = True

        '    End If
        '    DR.Close()
        'Catch ex As Exception

        '    mktru = False
        '    cmdupdt.Enabled = True
        '    MsgBox(ex.Message)
        'End Try
        'CMD.Dispose()

        '***new2
        Call flxphead2()

        Dim dt As DataTable = Nothing
        Try
            dt = getDataTable(msql)
            If dt.Rows.Count > 0 Then
                mktru = True
                cmdupdt.Enabled = False
                With flxp
                    For Each rw As DataRow In dt.Rows
                        .Rows = .Rows + 1
                        .Row = .Rows - 1

                        .set_TextMatrix(.Row, 0, rw("packagenum") & vbNullString)
                        .set_TextMatrix(.Row, 1, rw("itemcode") & vbNullString)
                        .set_TextMatrix(.Row, 2, Microsoft.VisualBasic.Format(rw("quantity"), "#######0.00"))
                        .set_TextMatrix(.Row, 3, rw("docentry"))


                        '.set_TextMatrix(.Row, 1, DR.Item("itemcode") & vbNullString)
                        .set_TextMatrix(.Row, 4, rw("Catalogname"))
                        .set_TextMatrix(.Row, 5, rw("u_Style") & vbNullString)
                        .set_TextMatrix(.Row, 6, rw("u_Size") & vbNullString)
                        '.set_TextMatrix(.Row, 5, DR.Item("quantity"))
                    Next
                    .Rows = .Rows + 1
                    .Row = .Rows - 1
                End With
            Else
                mktru = False
                loadcombo("opkg", "pkgtype", cmbboxtype, "pkgtype")
                loadcombo("owgt", "unitname", cmbwgt, "unitname")
                cmdupdt.Enabled = True
            End If
        Catch ex As Exception
            mktru = False
            cmdupdt.Enabled = True
            MsgBox(ex.Message)
        End Try



        Call flxptot()
    End Sub
    Private Sub loadhead()
        cmbboxno.Items.Clear()
        'cmbboxtype.Items.Clear()
        'cmbwgt.Items.Clear()

        If cmbtype.Text = "DATE ORDER" Then
            msql = "select  packagenum,packagetyp,weightunit,updtdate from rdln7 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text)
        Else
            msql = "select  packagenum,packagetyp,weightunit,updtdate from rinv7 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text)
        End If
        'msql = "select  packagenum,packagetyp,weightunit from rinv7 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text)

        Dim CMD1 As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()
        Call flxHhead()
        'Call flxphead()
        'Call flxchead2()
        Try
            'loadcombow("inv7", "packagenum", cmbboxno, "docentry", Microsoft.VisualBasic.Val(lbldocentry.Text))
            'loadcombow("inv7", "packagetyp", cmbboxtype, "docentry", Microsoft.VisualBasic.Val(lbldocentry.Text))
            'loadcombow("inv7", "weightunit", cmbwgt, "docentry", Microsoft.VisualBasic.Val(lbldocentry.Text))

            ''Dim DR As SqlDataReader
            Dim DR1 As SqlDataReader
            DR1 = CMD1.ExecuteReader
            If DR1.HasRows = True Then


                With flxh
                    While DR1.Read
                        .Rows = .Rows + 1
                        .Row = .Rows - 1
                        .set_TextMatrix(.Row, 1, DR1.Item("packagenum"))
                        .set_TextMatrix(.Row, 2, DR1.Item("packagetyp") & vbNullString)
                        .set_TextMatrix(.Row, 3, loaditcoderev("owgt", "unitname", "unitcode", DR1.Item("weightunit")))
                        If IsDBNull(DR1.Item("updtdate")) = False Then
                            mskdate.Text = Microsoft.VisualBasic.Format(DR1.Item("updtdate"), "dd-MM-yyyy")
                        Else
                            mskdate.Text = vbNullString
                        End If

                        'loaditcoderev("owgt", "unitname", "unitcode", DR1.Item("weightunit"))
                        '.set_TextMatrix(.Row, 2, DR1.Item("weightunit") & vbNullString)
                        'cmbboxno.Items.Add(DR1.Item("packagenum"))
                        'cmbboxtype.Items.Add(DR1.Item("packagetyp") & vbNullString)
                        'cmbwgt.Items.Add(DR1.Item("weightunit"))
                    End While
                End With
            End If
            DR1.Close()
        Catch ex As Exception
        End Try

        CMD1.Dispose()

    End Sub
    Private Sub flxptot()
        Dim k As Int32
        lblptot.Text = 0
        For k = 1 To flxp.Rows - 1
            lblptot.Text = Microsoft.VisualBasic.Val(lblptot.Text) + Microsoft.VisualBasic.Val(flxp.get_TextMatrix(k, 2))
        Next k
    End Sub
    Private Sub flxctot()
        Dim k As Int32
        lblctot.Text = 0
        For k = 1 To flxc.Rows - 1
            lblctot.Text = Microsoft.VisualBasic.Val(lblctot.Text) + Microsoft.VisualBasic.Val(flxc.get_TextMatrix(k, 2))
        Next k
    End Sub
    Private Sub flxctot2()
        Dim l As Int32
        lblctot2.Text = 0
        For l = 1 To flxc.Rows - 1
            If Len(Trim(flxc.get_TextMatrix(l, 0))) > 0 Then
                lblctot2.Text = Microsoft.VisualBasic.Val(lblctot2.Text) + Microsoft.VisualBasic.Val(flxc.get_TextMatrix(l, 2))
            End If
        Next l
    End Sub


    Private Sub flxHhead()
        flxh.Rows = 1
        flxh.Cols = 4

        flxh.set_ColWidth(0, 700)
        flxh.set_ColWidth(1, 1300)
        flxh.set_ColWidth(2, 1500)
        flxh.set_ColWidth(3, 1500)

        flxh.Row = 0
        flxh.Col = 0
        flxh.CellAlignment = 3
        flxh.CellFontBold = True
        flxh.set_TextMatrix(0, 0, "Sel")

        flxh.Col = 1
        flxh.CellAlignment = 3
        flxh.CellFontBold = True
        flxh.set_TextMatrix(0, 1, "Package No")

        flxh.Col = 2
        flxh.CellAlignment = 3
        flxh.CellFontBold = True
        flxh.set_TextMatrix(0, 2, "Package Type")

        flxh.Col = 3
        flxh.CellAlignment = 3
        flxh.CellFontBold = True
        flxh.set_TextMatrix(0, 3, "Weight")

    End Sub
    Private Sub flxchead()
        flxc.Rows = 1
        flxc.Cols = 4
        flxc.set_ColWidth(0, 700)
        flxc.set_ColWidth(1, 1500)
        flxc.set_ColWidth(2, 1500)
        flxc.set_ColWidth(3, 1500)
        'flxh.set_ColWidth(3, 15)

        flxc.Row = 0
        flxc.Col = 0
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 0, "Sel")

        flxc.Col = 1
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 1, "Item Code")

        flxc.Col = 2
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 2, "Available Qty")

        flxc.Col = 3
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 3, "Selected Qty")
    End Sub
    Private Sub flxchead2()
        flxc.Rows = 1
        flxc.Cols = 7
        flxc.set_ColWidth(0, 700)
        flxc.set_ColWidth(1, 1500)
        flxc.set_ColWidth(2, 1500)
        flxc.set_ColWidth(3, 1500)
        flxc.set_ColWidth(4, 1500)
        flxc.set_ColWidth(5, 1500)
        flxc.set_ColWidth(6, 1500)
        'flxh.set_ColWidth(3, 15)

        flxc.Row = 0
        flxc.Col = 0
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 0, "Sel")

        flxc.Col = 1
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 1, "Item Code")



        flxc.Col = 2
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 2, "Available Qty")

        flxc.Col = 3
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 3, "Selected Qty")


        flxc.Col = 4
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 1, "Item Name")

        flxc.Col = 5
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 1, "Style")

        flxc.Col = 6
        flxc.CellAlignment = 3
        flxc.CellFontBold = True
        flxc.set_TextMatrix(0, 1, "Size")


    End Sub

    Private Sub flxphead()
        flxp.Rows = 1
        flxp.Cols = 4
        flxp.set_ColWidth(0, 700)
        flxp.set_ColWidth(1, 1500)
        flxp.set_ColWidth(2, 1500)
        flxc.set_ColWidth(3, 1)
        'flxh.set_ColWidth(3, 15)

        flxp.Row = 0
        flxp.Col = 0
        flxp.CellAlignment = 3
        flxp.CellFontBold = True
        flxp.set_TextMatrix(0, 0, "#")

        flxp.Col = 1
        flxp.CellAlignment = 3
        flxp.CellFontBold = True
        flxp.set_TextMatrix(0, 1, "Item Code")

        flxp.Col = 2
        flxp.CellAlignment = 3
        flxp.CellFontBold = True
        flxp.set_TextMatrix(0, 2, "Qty")
        flxp.Col = 3
        flxp.CellAlignment = 3
        flxp.CellFontBold = True
        flxp.set_TextMatrix(0, 2, "Docentry")


    End Sub

    Private Sub flxphead2()
        flxp.Rows = 1
        flxp.Cols = 7
        flxp.set_ColWidth(0, 700)
        flxp.set_ColWidth(1, 1500)
        flxp.set_ColWidth(2, 1500)
        flxc.set_ColWidth(3, 1)
        flxp.set_ColWidth(4, 2000)
        flxp.set_ColWidth(5, 1000)
        flxp.set_ColWidth(6, 600)


        'flxh.set_ColWidth(3, 15)

        flxp.Row = 0
        flxp.Col = 0
        flxp.CellAlignment = 3
        flxp.CellFontBold = True
        flxp.set_TextMatrix(0, 0, "#")

        flxp.Col = 1
        flxp.CellAlignment = 3
        flxp.CellFontBold = True
        flxp.set_TextMatrix(0, 1, "Item Code")

        flxp.Col = 2
        flxp.CellAlignment = 3
        flxp.CellFontBold = True
        flxp.set_TextMatrix(0, 2, "Qty")
        flxp.Col = 3
        flxp.CellAlignment = 3
        flxp.CellFontBold = True
        flxp.set_TextMatrix(0, 3, "Docentry")

        flxp.Col = 4
        flxp.CellAlignment = 3
        flxp.CellFontBold = True
        flxp.set_TextMatrix(0, 4, "CatalogName")

        flxp.Col = 5
        flxp.CellAlignment = 3
        flxp.CellFontBold = True
        flxp.set_TextMatrix(0, 5, "Style")

        flxp.Col = 6
        flxp.CellAlignment = 3
        flxp.CellFontBold = True
        flxp.set_TextMatrix(0, 6, "Size")

    End Sub

    Private Sub flxc_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles flxc.DblClick

    End Sub


    Private Sub flxc_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxc.Enter

    End Sub

    Private Sub flxc_KeyDownEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyDownEvent) Handles flxc.KeyDownEvent
        'If Keys.Shift = 1 And (e.keyCode = 38 Or e.keyCode = 40) Then
        '    flxc.Redraw = False
        'Else
        '    flxc.Redraw = True
        'End If
    End Sub

    Private Sub flxc_KeyPressEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyPressEvent) Handles flxc.KeyPressEvent
        If e.keyAscii = 32 Then
            flxc.Row = flxc.Row
            If flxc.Row > 0 Then
                If Len(Trim(flxc.get_TextMatrix(flxc.Row, 0))) = 0 Then
                    flxc.Col = 0
                    flxc.CellFontName = "WinGdings"
                    flxc.CellFontSize = 14
                    flxc.CellAlignment = 4
                    flxc.CellFontBold = True
                    flxc.CellForeColor = Color.Red
                    flxc.Text = Chr(252)
                Else
                    flxc.Col = 0
                    flxc.Text = ""
                End If
            End If
        End If
        If flxc.Col = 2 Or flxc.Col = 3 Then
            editflx(flxc, e.keyAscii, flxc)
        End If
        Call flxctot2()
    End Sub

    Private Sub flxc_KeyUpEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyUpEvent) Handles flxc.KeyUpEvent
        'If flxc.Row - flxc.RowSel <> 0 Then
        '    'User selected more than one row
        '    'So Make the row and selected row the same
        '    flxc.Row = flxc.RowSel

        '    'To get highlight you must set focus to the control then back to whatever else
        '    flxc.Focus()
        'End If
        'flxc.Redraw = True
    End Sub



    Private Sub flxc_MouseDownEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_MouseDownEvent) Handles flxc.MouseDownEvent
        'flxc.Redraw = False
    End Sub

    Private Sub flxc_MouseUpEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_MouseUpEvent) Handles flxc.MouseUpEvent
        'If flxc.Row - flxc.RowSel <> 0 Then
        '    'User selected more than one row
        '    'So Make the row and selected row the same
        '    flxc.Row = flxc.RowSel

        '    'To get highlight you must set focus to the control then back to whatever else
        '    flxc.Focus()
        'End If
        'flxc.Redraw = True
    End Sub


    Private Sub cmdsend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsend.Click
        'For i = flxc.Rows - 1 To 1 Step -1
        '    If Len(Trim(flxc.get_TextMatrix(i, 0))) > 0 Then
        '        If Val(flxc.get_TextMatrix(i, 3)) > 0 Then
        '            'flxp.AddItem(flxp.Row & vbTab & flxc.get_TextMatrix(i, 1) & vbTab & flxc.get_TextMatrix(i, 3))
        '            flxp.Rows = flxp.Rows + 1
        '            flxp.Row = flxp.Rows - 1
        '            flxp.set_TextMatrix(flxp.Row, 0, cmbboxno.Text)
        '            flxp.set_TextMatrix(flxp.Row, 1, flxc.get_TextMatrix(i, 1))
        '            flxp.set_TextMatrix(flxp.Row, 3, lbldocentry.Text)

        '            If (Val(flxc.get_TextMatrix(i, 2)) - Val(flxc.get_TextMatrix(i, 3))) > 0 Then
        '                flxp.set_TextMatrix(flxp.Row, 2, Val(flxc.get_TextMatrix(i, 3)))
        '                'MsgBox(Val(flxc.get_TextMatrix(i, 3)))
        '                flxc.set_TextMatrix(i, 2, (Val(flxc.get_TextMatrix(i, 2)) - Val(flxc.get_TextMatrix(i, 3))))
        '                'flxp.set_TextMatrix(i, 2, Val(flxc.get_TextMatrix(i, 3)))

        '            Else
        '                flxp.set_TextMatrix(flxp.Row, 2, flxc.get_TextMatrix(i, 2))
        '                'flxc.RemoveItem(i)
        '                If flxc.Row < flxc.Rows - 1 Then
        '                    flxc.Row = flxc.Row + 1
        '                    flxc.RemoveItem(i)
        '                Else

        '                    flxc.RemoveItem(i)
        '                End If

        '            End If
        '            flxc.set_TextMatrix(i, 0, "")
        '            flxc.set_TextMatrix(i, 3, "")
        '        Else

        '            'flxp.AddItem(flxp.Row & vbTab & flxc.get_TextMatrix(i, 1), flxc.get_TextMatrix(i, 2))
        '            flxp.Rows = flxp.Rows + 1
        '            flxp.Row = flxp.Rows - 1

        '            flxp.set_TextMatrix(flxp.Row, 0, cmbboxno.Text)
        '            flxp.set_TextMatrix(flxp.Row, 1, flxc.get_TextMatrix(i, 1))
        '            flxp.set_TextMatrix(flxp.Row, 3, lbldocentry.Text)
        '            flxp.set_TextMatrix(flxp.Row, 2, flxc.get_TextMatrix(i, 2))

        '            'flxc.RemoveItem(i)
        '            If flxc.Row < flxc.Rows - 1 Then
        '                flxc.Row = flxc.Row + 1
        '                flxc.RemoveItem(i)
        '            Else

        '                flxc.RemoveItem(i)
        '            End If
        '        End If

        '    End If
        'Next
        'MsgBox(findval(flxh, cmbboxno.Text, 1))
        If findval(flxh, cmbboxno.Text, 1) = True Then
            Call loadpack2()
            Call flxptot()
            Call flxctot()
        End If

    End Sub


    Private Sub loadpack()
        For i = flxc.Rows - 1 To 1 Step -1
            If Len(Trim(flxc.get_TextMatrix(i, 0))) > 0 Then
                If Val(flxc.get_TextMatrix(i, 3)) > 0 Then
                    'flxp.AddItem(flxp.Row & vbTab & flxc.get_TextMatrix(i, 1) & vbTab & flxc.get_TextMatrix(i, 3))
                    flxp.Rows = flxp.Rows + 1
                    flxp.Row = flxp.Rows - 1
                    flxp.set_TextMatrix(flxp.Row, 0, cmbboxno.Text)
                    flxp.set_TextMatrix(flxp.Row, 1, flxc.get_TextMatrix(i, 1))
                    flxp.set_TextMatrix(flxp.Row, 3, lbldocentry.Text)

                    If (Val(flxc.get_TextMatrix(i, 2)) - Val(flxc.get_TextMatrix(i, 3))) > 0 Then
                        flxp.set_TextMatrix(flxp.Row, 2, Val(flxc.get_TextMatrix(i, 3)))
                        'MsgBox(Val(flxc.get_TextMatrix(i, 3)))
                        flxc.set_TextMatrix(i, 2, (Val(flxc.get_TextMatrix(i, 2)) - Val(flxc.get_TextMatrix(i, 3))))
                        'flxp.set_TextMatrix(i, 2, Val(flxc.get_TextMatrix(i, 3)))

                    Else
                        flxp.set_TextMatrix(flxp.Row, 2, flxc.get_TextMatrix(i, 2))
                        'flxc.RemoveItem(i)
                        If flxc.Row < flxc.Rows - 1 Then
                            flxc.Row = flxc.Row + 1
                            flxc.RemoveItem(i)
                        Else

                            flxc.RemoveItem(i)
                        End If

                    End If
                    flxc.set_TextMatrix(i, 0, "")
                    flxc.set_TextMatrix(i, 3, "")
                Else

                    'flxp.AddItem(flxp.Row & vbTab & flxc.get_TextMatrix(i, 1), flxc.get_TextMatrix(i, 2))
                    flxp.Rows = flxp.Rows + 1
                    flxp.Row = flxp.Rows - 1

                    flxp.set_TextMatrix(flxp.Row, 0, cmbboxno.Text)
                    flxp.set_TextMatrix(flxp.Row, 1, flxc.get_TextMatrix(i, 1))
                    flxp.set_TextMatrix(flxp.Row, 3, lbldocentry.Text)
                    flxp.set_TextMatrix(flxp.Row, 2, flxc.get_TextMatrix(i, 2))

                    'flxc.RemoveItem(i)
                    If flxc.Row < flxc.Rows - 1 Then
                        flxc.Row = flxc.Row + 1
                        flxc.RemoveItem(i)
                    Else

                        flxc.RemoveItem(i)
                    End If
                End If

            End If
        Next
    End Sub

    Private Sub loadpack2()
        For i = flxc.Rows - 1 To 1 Step -1
            If Len(Trim(flxc.get_TextMatrix(i, 0))) > 0 Then
                If Val(flxc.get_TextMatrix(i, 3)) > 0 Then
                    'flxp.AddItem(flxp.Row & vbTab & flxc.get_TextMatrix(i, 1) & vbTab & flxc.get_TextMatrix(i, 3))
                    flxp.Rows = flxp.Rows + 1
                    flxp.Row = flxp.Rows - 1
                    flxp.set_TextMatrix(flxp.Row, 0, cmbboxno.Text)
                    flxp.set_TextMatrix(flxp.Row, 1, flxc.get_TextMatrix(i, 1))
                    flxp.set_TextMatrix(flxp.Row, 3, lbldocentry.Text)
                    flxp.set_TextMatrix(flxp.Row, 4, flxc.get_TextMatrix(i, 4))
                    flxp.set_TextMatrix(flxp.Row, 5, flxc.get_TextMatrix(i, 5))
                    flxp.set_TextMatrix(flxp.Row, 6, flxc.get_TextMatrix(i, 6))

                    If (Val(flxc.get_TextMatrix(i, 2)) - Val(flxc.get_TextMatrix(i, 3))) > 0 Then
                        flxp.set_TextMatrix(flxp.Row, 2, Microsoft.VisualBasic.Format(Val(flxc.get_TextMatrix(i, 3)), "#######0.00"))
                        'MsgBox(Val(flxc.get_TextMatrix(i, 3)))
                        flxc.set_TextMatrix(i, 2, (Val(flxc.get_TextMatrix(i, 2)) - Val(flxc.get_TextMatrix(i, 3))))
                        'flxp.set_TextMatrix(i, 2, Val(flxc.get_TextMatrix(i, 3)))

                    Else
                        flxp.set_TextMatrix(flxp.Row, 2, Microsoft.VisualBasic.Format(Val(flxc.get_TextMatrix(i, 2)), "#######0.00"))
                        'flxc.RemoveItem(i)
                        If flxc.Row < flxc.Rows - 1 Then
                            flxc.Row = flxc.Row + 1
                            flxc.RemoveItem(i)
                        Else

                            flxc.RemoveItem(i)
                        End If

                    End If
                    flxc.set_TextMatrix(i, 0, "")
                    flxc.set_TextMatrix(i, 3, "")
                    'flxc.set_TextMatrix(i, 4, "")
                    'flxc.set_TextMatrix(i, 2, "")

                Else

                    'flxp.AddItem(flxp.Row & vbTab & flxc.get_TextMatrix(i, 1), flxc.get_TextMatrix(i, 2))
                    flxp.Rows = flxp.Rows + 1
                    flxp.Row = flxp.Rows - 1

                    flxp.set_TextMatrix(flxp.Row, 0, cmbboxno.Text)
                    flxp.set_TextMatrix(flxp.Row, 1, flxc.get_TextMatrix(i, 1))
                    flxp.set_TextMatrix(flxp.Row, 3, lbldocentry.Text)
                    flxp.set_TextMatrix(flxp.Row, 2, Microsoft.VisualBasic.Format(Val(flxc.get_TextMatrix(i, 2)), "#######0.00"))

                    flxp.set_TextMatrix(flxp.Row, 4, flxc.get_TextMatrix(i, 4))
                    flxp.set_TextMatrix(flxp.Row, 5, flxc.get_TextMatrix(i, 5))
                    flxp.set_TextMatrix(flxp.Row, 6, flxc.get_TextMatrix(i, 6))

                    'flxc.RemoveItem(i)
                    If flxc.Row < flxc.Rows - 1 Then
                        flxc.Row = flxc.Row + 1
                        flxc.RemoveItem(i)
                    Else

                        flxc.RemoveItem(i)
                    End If
                End If

            End If
        Next

    End Sub

    Private Sub txtno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtno.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If chkinvprn.Checked = True Then
                Call loadno()
                Call loadinv()
            Else
                Call loadno()
                'Call loadinv()
                'Call loadexists()
                'If mktru = False Then
                '    Call flxchead2()
                '    Call flxphead2()
                '    Call flxHhead()
                '    Call loadinv()
                '    'Call loadflxc()
                '    Call loadflxc2()
                '    'flxh.Rows = flxh.Rows + 1
                '    'flxh.Row = flxh.Rows - 1
                '    'flxh.set_TextMatrix(flxh.Row, 0, flxh.Row)
                '    cmbboxno.Items.Add(1)
                '    cmbboxno.Text = cmbboxno.Items.Item(cmbboxno.Items.Count - 1)
                'End If

                Call loadinv()
                Call loadexists()
                If mktru = False Then
                    Call flxchead2()
                    Call flxphead2()
                    Call flxHhead()
                    Call loadinv()
                    'Call loadflxc()
                    Call loadflxc2()
                    'flxh.Rows = flxh.Rows + 1
                    'flxh.Row = flxh.Rows - 1
                    'flxh.set_TextMatrix(flxh.Row, 0, flxh.Row)
                    cmbboxno.Items.Add(1)
                    cmbboxno.Text = cmbboxno.Items.Item(cmbboxno.Items.Count - 1)
                End If
            End If
        End If
    End Sub
    Private Sub saverec()
        Dim mltru As Boolean
        Dim k As Int32
        mltru = False
        For k = 1 To flxc.Rows - 1
            If Len(Trim(flxc.get_TextMatrix(k, 1))) > 0 And Val(flxc.get_TextMatrix(k, 2)) > 0 Then
                mltru = True
                Exit For
            End If
        Next

        If mltru = False Then
            If MsgBox("Are U Sure!", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then


                'For i = 0 To cmbboxno.Items.Count - 1
                For i = 1 To flxh.Rows - 1

                    If Trim(cmbtype.Text) = "DATE ORDER" Then
                        msql2 = "insert into rdln7(DocEntry,PackageNum,PackageTyp,Weight,WeightUnit,ObjType,LogInstanc,updtdate) values(" & Val(lbldocentry.Text) & "," & Val(flxh.get_TextMatrix(i, 1)) & ",'" & flxh.get_TextMatrix(i, 2) & "',0.000," & loaditcode("owgt", "unitcode", "unitname", Trim(flxh.get_TextMatrix(i, 3))) & ",13,0,'" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "')"
                    Else

                        msql2 = "insert into rinv7(DocEntry,PackageNum,PackageTyp,Weight,WeightUnit,ObjType,LogInstanc,updtdate) values(" & Val(lbldocentry.Text) & "," & Val(flxh.get_TextMatrix(i, 1)) & ",'" & flxh.get_TextMatrix(i, 2) & "',0.000," & loaditcode("owgt", "unitcode", "unitname", Trim(flxh.get_TextMatrix(i, 3))) & ",13,0,'" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "')"

                    End If
                    'msql2 = "insert into inv7(DocEntry,PackageNum,PackageTyp,Weight,WeightUnit,ObjType,LogInstanc) values(" & Val(txtno.Text) & "," & Val(cmbboxno.Items.Item(i)) & ",'" & cmbboxtype1.Items.Item(i) & "',0.000," & loaditcode("owgt", "unitcode", "unitname", Trim(cmbwgt1.Items.Item(i))) & ",13,0)"
                    'loaditcode("owgt", "unitcode", "unitname", Trim(cmbwgt.Items.Item(i)))

                    Dim cmd3 As New SqlCommand(msql2, con)
                    'Dim CMD2 As New OleDb.OleDbCommand("update ordr set u_team='WINNER',u_lr_date='" & Microsoft.VisualBasic.Format(CDate(mskddate.Text), "yyyy-MM-dd") & "' where docnum=" & Val(flxw.get_TextMatrix(j, 0)), con)
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If
                    Try
                        cmd3.ExecuteNonQuery()

                    Catch ex As Exception
                        MsgBox(ex.Message)


                    End Try

                Next i



                For j = 1 To flxp.Rows - 1

                    If Trim(cmbtype.Text) = "DATE ORDER" Then
                        msql = "insert into rdln8(DocEntry,PackageNum,ItemCode,Quantity,LogInstanc,ObjType,catalogname,u_style,u_size) values (" & Val(flxp.get_TextMatrix(j, 3)) & "," & Val(flxp.get_TextMatrix(j, 0)) & ",'" & Trim(flxp.get_TextMatrix(j, 1)) & "'," & Val(flxp.get_TextMatrix(j, 2)) & ",0,'13','" & flxp.get_TextMatrix(j, 4) & "','" & flxp.get_TextMatrix(j, 5) & "','" & flxp.get_TextMatrix(j, 6) & "')"
                    Else
                        msql = "insert into rINV8(DocEntry,PackageNum,ItemCode,Quantity,LogInstanc,ObjType,catalogname,u_style,u_size) values (" & Val(flxp.get_TextMatrix(j, 3)) & "," & Val(flxp.get_TextMatrix(j, 0)) & ",'" & Trim(flxp.get_TextMatrix(j, 1)) & "'," & Val(flxp.get_TextMatrix(j, 2)) & ",0,'13','" & flxp.get_TextMatrix(j, 4) & "','" & flxp.get_TextMatrix(j, 5) & "','" & flxp.get_TextMatrix(j, 6) & "')"
                    End If

                    Dim cmd2 As New SqlCommand(msql, con)
                    'Dim CMD2 As New OleDb.OleDbCommand("update ordr set u_team='WINNER',u_lr_date='" & Microsoft.VisualBasic.Format(CDate(mskddate.Text), "yyyy-MM-dd") & "' where docnum=" & Val(flxw.get_TextMatrix(j, 0)), con)
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If
                    Try
                        cmd2.ExecuteNonQuery()
                        'Call cmdclear


                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                Next j

                MsgBox("updated!")
                If MsgBox("Print", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
                    cmdprint.PerformClick()
                End If
                cmdclear.PerformClick()
                'MsgBox("Winner Team Saved!")

            End If
        Else
            MsgBox("Packaging Not Completed")
        End If

    End Sub

    Private Sub txtno_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtno.PreviewKeyDown

    End Sub

    Private Sub txtno_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtno.SizeChanged

    End Sub

    Private Sub txtno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtno.TextChanged

    End Sub

    Private Sub flxh_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles flxh.DblClick
        cmbboxno.Text = flxh.get_TextMatrix(flxh.Row, 1)
        cmbboxtype.Text = flxh.get_TextMatrix(flxh.Row, 2)
        cmbwgt.Text = flxh.get_TextMatrix(flxh.Row, 3)
    End Sub

    Private Sub flxh_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxh.Enter

    End Sub

    Private Sub cmbboxno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbboxno.KeyPress
        If Asc(e.KeyChar) = 13 Then
            'cmbboxno.Text = (cmbboxno.Text)) + 1
            'cmbboxno.Items.Add(cmbboxno.Text)
            '--cmbboxno.Items.IndexOf()

            'cmbboxno.Items.Add()
            'MsgBox(cmbboxno.Items.Count)
            If mktru = False Then
                cmbboxno.Items.Add((Val(cmbboxno.Items.Item(cmbboxno.Items.Count - 1))) + 1)
                cmbboxno.Text = cmbboxno.Items.Item(cmbboxno.Items.Count - 1)
                'cmbboxtype.Text = ""
                'cmbwgt1.Text = ""
                cmbboxtype.Text = "BUNDLE"
                cmbwgt1.Text = "Kilogramme"
            Else
                cmbboxtype.Text = "BUNDLE"
                cmbwgt1.Text = "Kilogramme"
                'cmbboxtype.Text = cmbboxtype1.Items.Item(cmbboxno.SelectedIndex)
                'cmbwgt.Text = cmbwgt1.Items.Item(cmbboxno.SelectedIndex)

            End If
        End If
    End Sub

    Private Sub cmbboxno_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbboxno.KeyUp
        If e.KeyCode = Keys.F11 Then
            cmbboxno.Items.Remove(cmbboxno.Items.Item(cmbboxno.Items.Count - 1))
        End If
    End Sub

    Private Sub cmbboxno_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbboxno.SelectedIndexChanged

    End Sub

    Private Sub cmdupdt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdupdt.Click
        Call saverec()
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click

        Call flxHhead()
        Call flxchead2()
        Call flxphead2()
        flxc.Rows = flxc.Rows + 1
        flxc.Row = flxc.Rows - 1

        flxc.Rows = flxc.Rows + 1
        flxc.Row = flxc.Rows - 1
        flxc.Rows = flxc.Rows + 1
        flxc.Row = flxc.Rows - 1
        flxc.Rows = flxc.Rows + 1
        flxc.Row = flxc.Rows - 1
        'For j = 1 To 100
        '    cmbboxno.Items.Add(j)
        'Next
        loadcombo("opkg", "pkgtype", cmbboxtype, "pkgtype")
        loadcombo("owgt", "unitname", cmbwgt, "unitname")
        lbldocentry.Text = ""
        lbldate2.Text = ""
        lblparty.Text = ""
        lblamt.Text = ""
        cmbboxno.Text = ""
        cmbboxtype.Text = ""
        cmbwgt.Text = ""
        'loadcombo("ofpr", "code", cmbyear, "code")
        loadcombo("ofpr", "indicator", cmbyear, "indicator")
        cmbyear.Text = mperiod
        If mProdMktbarcode = "1" Then
            chkprod.Checked = True
        Else
            chkprod.Checked = False
        End If


    End Sub

    'Private Sub cmbboxtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbboxtype.SelectedIndexChanged
    '    'If cmbboxno.Items.Count - 1 = cmbboxtype1.Items.Count - 1 Then
    '    cmbboxtype1.Items.Remove(cmbboxno.SelectedItem)
    '    cmbboxtype1.Items.Add(cmbboxtype.SelectedItem)
    '    '--Else
    '    'cmbboxtype1.Items.Add(cmbboxtype.SelectedItem)
    '    'End If
    'End Sub

    'Private Sub cmbwgt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbwgt.SelectedIndexChanged
    '    If cmbboxno.Items.Count > cmbwgt1.Items.Count Then
    '        cmbwgt1.Items.Add(cmbwgt.SelectedItem)
    '    Else
    '        cmbwgt1.Items.Remove(cmbboxno.SelectedItem)
    '        cmbwgt1.Items.Add(cmbwgt.SelectedItem)
    '    End If
    'End Sub

    Private Sub cmbwgt1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbwgt1.SelectedIndexChanged

    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        If mktru = False Then
            flxh.Rows = flxh.Rows + 1
            flxh.Row = flxh.Rows - 1
            flxh.set_TextMatrix(flxh.Row, 1, cmbboxno.Text)
            flxh.set_TextMatrix(flxh.Row, 2, cmbboxtype.Text)
            flxh.set_TextMatrix(flxh.Row, 3, cmbwgt.Text)
        End If
    End Sub
    Private Sub crttab()
        msql = "CREATE TABLE [dbo].[RINV7](" & vbCrLf _
    & "[DocEntry] [int] NOT NULL," & vbCrLf _
    & "[PackageNum] [int] NOT NULL," & vbCrLf _
   & "[PackageTyp] [nvarchar](30) NULL," & vbCrLf _
   & "[Weight] [numeric](19, 6) NULL," & vbCrLf _
   & "[WeightUnit] [smallint] NULL," & vbCrLf _
   & "[ObjType] [nvarchar](20) NULL," & vbCrLf _
   & "[LogInstanc] [int] NULL," & vbCrLf _
      & "CONSTRAINT [RINV7_PRIMARY] PRIMARY KEY CLUSTERED  " & vbCrLf _
      & "( [DocEntry] ASC," & vbCrLf _
      & "  [PackageNum](Asc)" & vbCrLf _
      & ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " & vbCrLf _
     & ") ON [PRIMARY]" & vbCrLf _
     & " GO() " & vbCrLf _
    & "ALTER TABLE [dbo].[RINV7] ADD  CONSTRAINT [DF_RINV7_ObjType]  DEFAULT ('13') FOR [ObjType]" & vbCrLf _
    & "    GO() " & vbCrLf _
     & "ALTER TABLE [dbo].[RINV7] ADD  CONSTRAINT [DF_RINV7_LogInstanc]  DEFAULT ((0)) FOR [LogInstanc]" & vbCrLf _
     & "   GO()"


        'RINV8

        msql = "CREATE TABLE [dbo].[RINV8](" & vbCrLf _
         & "[DocEntry] [int] NOT NULL," & vbCrLf _
         & "[PackageNum] [int] NOT NULL," & vbCrLf _
         & "[ItemCode] [nvarchar](20) NOT NULL," & vbCrLf _
         & "[Quantity] [numeric](19, 6) NULL," & vbCrLf _
         & "[LogInstanc] [int] NULL," & vbCrLf _
         & "[ObjType] [nvarchar](20) NULL," & vbCrLf _
         & "[Catalogname] [nvarchar](50) NOT NULL," & vbCrLf _
         & "[u_style] [nvarchar](100) NOT NULL," & vbCrLf _
         & "[u_size] [nvarchar](100) NOT NULL," & vbCrLf _
            & "CONSTRAINT [RINV8_PRIMARY] PRIMARY KEY CLUSTERED " & vbCrLf _
            & "(" & vbCrLf _
         & "[DocEntry] ASC," & vbCrLf _
         & "[PackageNum] ASC," & vbCrLf _
         & "[ItemCode] ASC," & vbCrLf _
         & "[Catalogname] ASC," & vbCrLf _
         & "[u_style] ASC," & vbCrLf _
            & "[u_size](Asc)" & vbCrLf _
            & ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" & vbCrLf _
            & ") ON [PRIMARY]" & vbCrLf _
            & "GO()" & vbCrLf _
        & "ALTER TABLE [dbo].[RINV8] ADD  CONSTRAINT [DF_RINV8_LogInstanc]  DEFAULT ((0)) FOR [LogInstanc]" & vbCrLf _
        & "GO()" & vbCrLf _
        & "ALTER TABLE [dbo].[RINV8] ADD  CONSTRAINT [DF_RINV8_ObjType]  DEFAULT ('13') FOR [ObjType]" & vbCrLf _
        & "GO()"



    End Sub

    Private Sub cmdrcd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdrcd.Click
        If MsgBox("Delete Package details!", vbYesNo) = vbYes Then
            If Trim(cmbtype.Text) = "DATE ORDER" Then
                msql = "delete from rdln7 where docentry=" & Val(lbldocentry.Text)
            Else
                msql = "delete from rINV7 where docentry=" & Val(lbldocentry.Text)
            End If
            'msql = "delete from rINV7 where docentry=" & Val(lbldocentry.Text)
            Dim cmd1 As New SqlCommand(msql, con)
            'Dim CMD2 As New OleDb.OleDbCommand("update ordr set u_team='WINNER',u_lr_date='" & Microsoft.VisualBasic.Format(CDate(mskddate.Text), "yyyy-MM-dd") & "' where docnum=" & Val(flxw.get_TextMatrix(j, 0)), con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Try
                cmd1.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            cmd1.Dispose()
            If Trim(cmbtype.Text) = "DATE ORDER" Then
                msql = "delete from rdln8 where docentry=" & Val(lbldocentry.Text)
            Else
                msql = "delete from rINV8 where docentry=" & Val(lbldocentry.Text)
            End If
            Dim cmd2 As New SqlCommand(msql, con)
            'Dim CMD2 As New OleDb.OleDbCommand("update ordr set u_team='WINNER',u_lr_date='" & Microsoft.VisualBasic.Format(CDate(mskddate.Text), "yyyy-MM-dd") & "' where docnum=" & Val(flxw.get_TextMatrix(j, 0)), con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Try
                cmd2.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            MsgBox("Deleted!")
            cmd2.Dispose()
            cmdclear.PerformClick()
        End If

    End Sub

    Private Sub flxp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxp.Enter

    End Sub
    Private Sub crystal()

        'Me.Cursor = Cursors.WaitCursor
        ''Dim cryRpt As New ReportDocument()
        'Dim cryrpt As New ReportDocument()


        ''cryRpt.Load(Trim(mreppath) & "Company Analysis Report.rpt")
        'If Trim(cmbtype.Text) = "DATE ORDER" Then
        '    If MsgBox("Single Sheet", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
        '        cryRpt.Load(Trim(mreppath) & "Packaging Single Sheetdel.rpt")
        '    Else
        '        cryRpt.Load(Trim(mreppath) & "Packaging Multi Sheetdel.rpt")
        '    End If
        'Else

        '    If MsgBox("Single Sheet", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
        '        cryRpt.Load(Trim(mreppath) & "Packaging Single Sheet.rpt")
        '    Else
        '        cryRpt.Load(Trim(mreppath) & "Packaging Multi Sheet.rpt")
        '    End If
        'End If


        'CrystalReportLogOn(cryRpt, Trim(mkserver), dbnam, Trim(dbuser), Trim(mkpwd))

        ''CrystalReportLogOn(cryRpt, "192.168.0.5", dbnam, "sa", "iTTsA@536")
        'cryRpt.SetParameterValue("Dockey@", Val(lbldocentry.Text))

        'Me.view1.ReportSource = cryRpt
        'Me.view1.PrintReport()
        ''view1.PrintToPrinter(1, False, 1, 1)
        ''Me.View1.ReportSource = cryRpt
        'Me.view1.Refresh()
        'cryRpt.Dispose()
        'Me.Cursor = Cursors.Default





        'Inner Packing Slip RR.rpt

        mreportname = "GST PREINVOICE cor"

        If Trim(cmbtype.Text) = "DATE ORDER" Then
            If MsgBox("Single Sheet", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
                mreportname = "Packaging Single Sheetdel"
            Else
                mreportname = "Packaging Multi Sheetdel"
            End If
        Else

            If MsgBox("Single Sheet", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
                mreportname = "Packaging Single Sheet"
            Else
                mreportname = "Packaging Multi Sheet"
            End If
        End If


        Dim paramDict As New Dictionary(Of String, Object)
        paramDict.Add("Dockey@", Val(lbldocentry.Text))
        ' paramDict("Dockey@") = Val(Label7.Text)

        Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

        Dim req As New PrintRequest() With {
             .ReportName = mreportname,
             .PrinterName = If(isPrint, mlsprinter, ""),     ' "" = preview
             .UseDB = True,
             .ServerName = mdbserver,
             .DatabaseName = mdbname,
             .DBUser = mdbuserid,
             .DBPassword = mdbpwd,
             .Parameters = paramDict
        }
        'Dim success As Boolean = CallCrystalPrintService(req)

        Dim success As Boolean
        success = PrintCrystalReport(req, isPrint)

    End Sub

    Private Sub cmdprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdprint.Click
        'rhl
        If MsgBox("Inner Slip Print on Barcode Printer Directly not Crystal Report", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            innerslip(Val(lbldocentry.Text))
        Else
            If mdbname = "RHLLIVE" Then
                Call crystal()
            Else

                '    'rr
                Call crystalrr()
            End If
        End If

        'innerslip(Val(lbldocentry.Text))




    End Sub
    'Private Sub setlogon(ByVal cryrpt As ReportDocument)
    '    'Dim cryRpt As New ReportDocument()
    '    cryRpt.SetDatabaseLogon("sa", "SEsA@536", "192.168.0.5", dbnam)

    '    For Each conInfo As IConnectionInfo In cryRpt.DataSourceConnections
    '        conInfo.SetConnection("192.168.0.5", dbnam, "sa", "SEsA@536")
    '    Next
    '    For Each table As CrystalDecisions.CrystalReports.Engine.Table In cryRpt.Database.Tables
    '        Dim logOnInfo As TableLogOnInfo = table.LogOnInfo
    '        If (Not (logOnInfo) Is Nothing) Then
    '            logOnInfo.TableName = table.Name
    '            logOnInfo.ConnectionInfo = ConnectionInfo
    '            table.ApplyLogOnInfo(logOnInfo)
    '            'table.Location = String.Format("{0}.dbo.{1}", dbnam, table.Location.Substring(table.Location.LastIndexO, (f(".") + 1)))
    '        End If
    '    Next
    '    ' Set subreport connection info
    '    For Each subreport As ReportDocument In cryRpt.Subreports
    '        For Each conInfo As IConnectionInfo In cryRpt.DataSourceConnections
    '            conInfo.SetConnection("192.168.0.5", dbnam, "sa", "SEsA@536")
    '        Next
    '        For Each table As Table In subreport.Database.Tables
    '            Dim logOnInfo As TableLogOnInfo = table.LogOnInfo
    '            If (Not (logOnInfo) Is Nothing) Then
    '                logOnInfo.TableName = table.Name
    '                logOnInfo.ConnectionInfo = ConnectionInfo
    '                table.ApplyLogOnInfo(logOnInfo)
    '                'table.Location = String.Format("{0}.dbo.{1}", Database, table.Location.Substring(table.Location.LastIndexO, (f(".") + 1)))
    '            End If
    '        Next
    '    Next



    '    '****



    '    'Dim ReportSections As Sections = cryRpt.ReportDefinition.Sections
    '    'Dim crReportObjects As ReportObjects
    '    'Dim crSubreportObject As SubreportObject
    '    'Dim crSubreportDocument As ReportDocument
    '    'Dim crDatabase As Database
    '    'Dim crTables As Tables
    '    'For Each section As Section In ReportSections
    '    '    crReportObjects = section.ReportObjects
    '    '    For Each crReportObject As ReportObject In crReportObjects
    '    '        If (crReportObject.Kind <> ReportObjectKind.SubreportObject) Then
    '    '            'TODO: Warning!!! continue If
    '    '        End If
    '    '        crSubreportObject = CType(crReportObject, SubreportObject)
    '    '        crSubreportDocument = crSubreportObject.OpenSubreport(crSubreportObject.SubreportName)
    '    '        crDatabase = crSubreportDocument.Database
    '    '        crTables = crDatabase.Tables
    '    '        For Each crTable As Table In crTables
    '    '            Dim crTableLogOnInfo As TableLogOnInfo = crTable.LogOnInfo
    '    '            crTableLogOnInfo.ConnectionInfo = ConnectionInfo
    '    '            crTable.ApplyLogOnInfo(crTableLogOnInfo)
    '    '        Next
    '    '    Next
    '    'Next
    '    'Dim tables As Tables = cryRpt.Database.Tables
    '    'For Each table As CrystalDecisions.CrystalReports.Engine.Table In tables
    '    '    Dim tableLogonInfo As TableLogOnInfo = table.LogOnInfo
    '    '    tableLogonInfo.ConnectionInfo = ConnectionInfo
    '    '    table.ApplyLogOnInfo(tableLogonInfo)
    '    'Next

    '    '** another method
    '    'Dim ds As New DataSet() ' your report data source    
    '    'Dim rd As New ReportDocument()
    '    'rd.Load(Server.MapPath("~/" + "Your Report name"))

    '    'If rd.Subreports.Count > 0 Then
    '    '    rd.Subreports(0).SetDataSource(ds.Tables(1)) ' define table data for sub report
    '    'End If
    '    'rd.SetDataSource(ds)

    'End Sub

    Private Sub crystalrr()

        'Me.Cursor = Cursors.WaitCursor
        'Dim cryRpt As New ReportDocument()


        ''cryRpt.Load(Trim(mreppath) & "Company Analysis Report.rpt")
        'If cmbtype.Text = "DATE ORDER" Then
        '    If MsgBox("Single Sheet", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
        '        cryRpt.Load(Trim(mreppath) & "Inner Packing Slip RRdel.rpt")
        '    Else
        '        cryRpt.Load(Trim(mreppath) & "Inner Packing Slip RRdel2.rpt")
        '    End If
        'Else

        '    If MsgBox("Single Sheet", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
        '        cryRpt.Load(Trim(mreppath) & "Inner Packing Slip RR.rpt")
        '    Else
        '        cryRpt.Load(Trim(mreppath) & "Inner Packing Slip RR2.rpt")
        '    End If
        'End If


        'CrystalReportLogOn(cryRpt, Trim(mkserver), dbnam, Trim(dbuser), Trim(mkpwd))





        'cryRpt.SetParameterValue("Dockey@", Val(lbldocentry.Text))

        'Me.view1.ReportSource = cryRpt
        'Me.view1.PrintReport()

        'Me.view1.Refresh()
        'cryRpt.Dispose()
        'Me.Cursor = Cursors.Default

        If cmbtype.Text = "DATE ORDER" Then
            If MsgBox("Single Sheet", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
                mreportname = "Inner Packing Slip RRdel"
            Else
                mreportname = "Inner Packing Slip RRdel2"
            End If
        Else

            If MsgBox("Single Sheet", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
                mreportname = "Inner Packing Slip RR"
            Else
                mreportname = "Inner Packing Slip RR2"
            End If
        End If



        Dim paramDict As New Dictionary(Of String, Object)
        paramDict.Add("Dockey@", Val(lbldocentry.Text))
        ' paramDict("Dockey@") = Val(Label7.Text)

        Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

        Dim req As New PrintRequest() With {
             .ReportName = mreportname,
             .PrinterName = If(isPrint, mlsprinter, ""),     ' "" = preview
             .UseDB = True,
             .ServerName = mdbserver,
             .DatabaseName = mdbname,
             .DBUser = mdbuserid,
             .DBPassword = mdbpwd,
             .Parameters = paramDict
        }
        'Dim success As Boolean = CallCrystalPrintService(req)

        Dim success As Boolean
        success = PrintCrystalReport(req, isPrint)

    End Sub

    Private Sub flxp_KeyUpEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyUpEvent) Handles flxp.KeyUpEvent
        'flxc.RemoveItem(i)
        If e.keyCode = Keys.F9 Then
            flxc.Rows = flxc.Rows + 1
            flxc.Row = flxc.Rows - 1

            'flxc.set_TextMatrix(flxc.Row, 0, flxp.get_TextMatrix(flxp.Row,0)
            flxc.set_TextMatrix(flxc.Row, 1, flxp.get_TextMatrix(flxp.Row, 1))
            flxc.set_TextMatrix(flxc.Row, 2, flxp.get_TextMatrix(flxp.Row, 2))
            flxc.set_TextMatrix(flxc.Row, 4, flxp.get_TextMatrix(flxp.Row, 4))
            flxc.set_TextMatrix(flxc.Row, 5, flxp.get_TextMatrix(flxp.Row, 5))
            flxc.set_TextMatrix(flxc.Row, 6, flxp.get_TextMatrix(flxp.Row, 6))


            'If (Val(flxc.get_TextMatrix(i, 2)) - Val(flxc.get_TextMatrix(i, 3))) > 0 Then
            'flxp.set_TextMatrix(flxp.Row, 2, Microsoft.VisualBasic.Format(Val(flxc.get_TextMatrix(i, 3)), "#######0.00"))
            ''MsgBox(Val(flxc.get_TextMatrix(i, 3)))
            ' flxc.set_TextMatrix(i, 2, (Val(flxc.get_TextMatrix(i, 2)) - Val(flxc.get_TextMatrix(i, 3))))
            ''flxp.set_TextMatrix(i, 2, Val(flxc.get_TextMatrix(i, 3)))

            If flxp.Row < flxp.Rows - 1 Then
                flxp.Row = flxp.Row + 1
                flxp.RemoveItem(flxp.Row)
            Else
                flxp.RemoveItem(flxp.Row)
            End If
            Call flxctot()
        End If
    End Sub

    Private Sub cmbboxtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbboxtype.SelectedIndexChanged

    End Sub

    Private Sub lbldate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lbldate.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If chkinvprn.Checked = True Then
                Call loadno()
                Call loadinv()
            End If
            'Call loadinv()
            'Call loadexists()
            'If mktru = False Then
            '    Call flxchead2()
            '    Call flxphead2()
            '    Call flxHhead()
            '    Call loadinv()
            '    'Call loadflxc()
            '    Call loadflxc2()
            '    'flxh.Rows = flxh.Rows + 1
            '    'flxh.Row = flxh.Rows - 1
            '    'flxh.set_TextMatrix(flxh.Row, 0, flxh.Row)
            '    cmbboxno.Items.Add(1)
            '    cmbboxno.Text = cmbboxno.Items.Item(cmbboxno.Items.Count - 1)
            'End If

        End If
    End Sub

    Private Sub lbldate_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lbldate.KeyUp

    End Sub


    Private Sub crystalinv()

        'Me.Cursor = Cursors.WaitCursor
        'Dim cryRpt As New ReportDocument()




        'cryRpt.Load(Trim(mreppath) & "GST PREINVOICE cor.rpt")


        'CrystalReportLogOn(cryRpt, Trim(mkserver), dbnam, Trim(dbuser), Trim(mkpwd))




        ''cryRpt.SetParameterValue(cryRpt.DataDefinition.ParameterFields(0).ParameterFieldName, Val(lbldocentry.Text))

        'cryRpt.SetParameterValue("Dockey@", Val(lbldocentry.Text))

        'Me.view1.ReportSource = cryRpt
        'Me.view1.PrintReport()
        ''view1.PrintToPrinter(1, False, 1, 1)
        ''Me.View1.ReportSource = cryRpt
        'Me.view1.Refresh()
        'cryRpt.Dispose()
        'Me.Cursor = Cursors.Default

        mreportname = "GST PREINVOICE cor"
        Dim paramDict As New Dictionary(Of String, Object)
        paramDict.Add("Dockey@", Val(lbldocentry.Text))
        ' paramDict("Dockey@") = Val(Label7.Text)


        Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

        Dim req As New PrintRequest() With {
             .ReportName = mreportname,
             .PrinterName = If(isPrint, mlsprinter, ""),     ' "" = preview
             .UseDB = True,
             .ServerName = mdbserver,
             .DatabaseName = mdbname,
             .DBUser = mdbuserid,
             .DBPassword = mdbpwd,
             .Parameters = paramDict
        }
        'Dim success As Boolean = CallCrystalPrintService(req)

        Dim success As Boolean
        success = PrintCrystalReport(req, isPrint)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Val(lbldocentry.Text) > 0 Then
            Call crystalinv()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Dim dir As String

        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "bundle.txt"
        'dir = System.AppDomain.CurrentDomain.BaseDirectory()
        'If chkdirprn.Checked = True Then
        ' FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        ' Else
        FileOpen(1, mdir, OpenMode.Output)

        If Trim(cmbtype.Text) = "DATE ORDER" Then
            msql5 = "select b.DocNum,b.DocEntry,b.DocDate,b.CardCode,b.CardName,b.U_Bundle,c.PackageNum,c.noqty,'D-'+rtrim(convert(nvarchar(100),d.U_Remarks))+' '+LTRIM(convert(nvarchar(20),b.docnum)) billno,isnull(U_RefNo,'') refno,ltrim(CONVERT(nvarchar(7),c.packagenum))+'/'+ltrim(CONVERT(nvarchar(7),b.u_bundle)) as packno,isnull(b.u_transport,'') transport,cr.state,r.u_showcode  from Odln b " & vbCrLf _
                   & " left join (select DocEntry,packagenum,SUM(Quantity) noqty from rdln8 with (nolock) group by DocEntry,packagenum) c  on c.DocEntry=b.DocEntry " & vbCrLf _
                   & " left join [@INCM_BND1] d on d.U_Name=b.u_brand " & vbCrLf _
                   & " left join (select cardcode,state from crd1 group by cardcode,state) cr on cr.cardcode=b.cardcode" & vbCrLf _
                   & " left join ocrd r on r.cardcode=b.cardcode" & vbCrLf _
                   & " where b.docentry = " & Val(lbldocentry.Text)
        Else

            msql5 = "select b.DocNum,b.DocEntry,b.DocDate,b.CardCode,b.CardName,b.U_Bundle,c.PackageNum,c.noqty,'I-'+rtrim(convert(nvarchar(100),d.U_Remarks))+' '+LTRIM(convert(nvarchar(20),b.docnum)) billno, j.docnums refno,ltrim(CONVERT(nvarchar(7),c.packagenum))+'/'+ltrim(CONVERT(nvarchar(7),b.u_bundle)) as packno,isnull(b.u_transport,'') transport,cr.state,r.u_showcode  from Oinv b " & vbCrLf _
                   & " left join (select DocEntry,packagenum,SUM(Quantity) noqty from rinv8 with (nolock) group by DocEntry,packagenum) c  on c.DocEntry=b.DocEntry " & vbCrLf _
                   & " left join (select distinct (convert(nvarchar(max),STUFF((select distinct ','+ convert(nvarchar(max),(t2.DocNum)) from oinv t2  With (Nolock) " & vbCrLf _
                   & " WHERE  t2.CardCode= t1.CardCode AND t1.PIndicator = t2.PIndicator and " & vbCrLf _
                   & " CASE when CONVERT(nvarchar(max),isnull(t2.U_RefNo,''))  = '' then CONVERT(nvarchar(max),isnull(t2.DocNum,''))  else CONVERT(nvarchar(max),isnull(t2.U_RefNo,'')) end  = " & vbCrLf _
                   & " CASE when CONVERT(nvarchar(max),isnull(t1.U_RefNo,''))  = '' then CONVERT(nvarchar(max),isnull(t1.DocNum,''))  else CONVERT(nvarchar(max),isnull(t1.U_RefNo,'')) end  " & vbCrLf _
                   & " for XML path('')), 1, 1,''))) as docnums,T1.DocEntry,CASE WHEN CONVERT(NVARCHAR(MAX), isnull(U_REFNO ,''))= '' 	THEN CONVERT(NVARCHAR(MAX), isnull(DOCNUM ,'')) 	ELSE CONVERT(NVARCHAR(MAX), isnull(U_REFNO ,'')) END REFNO 	from oinv t1  With (Nolock) ) j on j.docentry=b.docentry " & vbCrLf _
                   & " left join [@INCM_BND1] d on d.U_Name=b.u_brand " & vbCrLf _
                   & " left join (select cardcode,state from crd1 group by cardcode,state) cr on cr.cardcode=b.cardcode" & vbCrLf _
                   & " left join ocrd r on r.cardcode=b.cardcode" & vbCrLf _
                   & " where b.docentry = " & Val(lbldocentry.Text)

        End If


        Dim CMD3 As New SqlCommand(msql5, con)
        Dim DR3 As SqlDataReader
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        j = 0
        'Dim DR3 As OleDb.OleDbDataReader
        DR3 = CMD3.ExecuteReader
        If DR3.HasRows = True Then
            While DR3.Read
                PrintLine(1, TAB(0), "^XA")
                PrintLine(1, TAB(0), "^PRC")
                PrintLine(1, TAB(0), "^LH0,0^FS")
                PrintLine(1, TAB(0), "^LL360")
                PrintLine(1, TAB(0), "^MD5")
                PrintLine(1, TAB(0), "^MNY")
                PrintLine(1, TAB(0), "^LH0,0^FS")

                PrintLine(1, TAB(0), "^FO153,30^A0N,70,90^CI13^FR^FD" & DR3.Item("billno") & "^FS;")
                PrintLine(1, TAB(0), "^FO133,110^A0N,60,70^CI13^FR^FDBundle No:" & DR3.Item("packno") & "^FS;")
                PrintLine(1, TAB(0), "^FO283,175^A0N,60,70^CI13^FR^FDQty : " & Format(DR3.Item("noqty"), "###0") & "^FS;")
                'If Len(Trim(DR3.Item("refno"))) > 0 Then
                If InStr(DR3.Item("refno"), ",") > 0 Then
                    PrintLine(1, TAB(0), "^FO140,245^A0N,30,30^CI13^FR^FDJoint :" & DR3.Item("refno") & "^FS;")
                End If

                If UCase(Trim(DR3.Item("u_showcode"))) = "S" Then
                    PrintLine(1, TAB(0), "^FO140,285^A0N,30,30^CI13^FR^FDTrans :" & DR3.Item("transport") & " - SR" & "^FS;")
                Else
                    PrintLine(1, TAB(0), "^FO140,285^A0N,30,30^CI13^FR^FDTrans :" & DR3.Item("transport") & " - " & DR3.Item("state") & "^FS;")
                End If

                PrintLine(1, TAB(0), "^PQ1,0,0,N")
                PrintLine(1, TAB(0), "^XZ")

            End While
        End If
        FileClose(1)

        'If chkdirprn.Checked = False Then
        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
            'Shell("cmd.exe /c" & "type " & mdir & " > lpt" & Trim(txtport.Text))
            If mos = "WIN" Then
                Shell("rawpr.bat " & mdir)
            Else
                Dim printer As String = tscprinter1
                Dim filePath As String = mlinpath
                Dim filePathname As String = mlinpath & "bundle.txt"
                Dim success As Boolean = PrintTscRaw(printer, filePathname)
            End If


        End If

            DR3.Close()
        CMD3.Dispose()
    End Sub



    Private Sub crystalbunadd()
        'live
        'Me.Cursor = Cursors.WaitCursor
        'Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()



        'cryRpt.Load(Trim(mreppath) & "Address Print ws bundle-acc.rpt")


        'CrystalReportLogOn(cryRpt, Trim(mkserver), dbnam, Trim(dbuser), Trim(mkpwd))


        'cryRpt.SetParameterValue("Dockey@", Val(lbldocentry.Text))
        'cryRpt.SetParameterValue("period@", cmbyear.Text)

        'Me.view1.ReportSource = cryRpt
        'Me.view1.PrintReport()

        'Me.view1.Refresh()
        'cryRpt.Dispose()
        'Me.Cursor = Cursors.Default
        mreportname = "Address Print ws bundle-acc"
        Dim paramDict As New Dictionary(Of String, Object)
        paramDict.Add("DocKey@", Val(txtno.Text))
        paramDict.Add("period@", cmbyear.Text)
        ' paramDict("Dockey@") = Val(Label7.Text)
        ' Dim req As New PrintRequest() With {
        '     .ReportName = mreportname,
        '     .PrinterName = "",     ' "" = preview
        '     .UseDB = False,
        '     .Parameters = paramDict
        '}

        Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

        Dim req As New PrintRequest() With {
             .ReportName = mreportname,
             .PrinterName = If(isPrint, mlsprinter, ""),     ' "" = preview
             .UseDB = True,
             .ServerName = mdbserver,
             .DatabaseName = mdbname,
             .DBUser = mdbuserid,
             .DBPassword = mdbpwd,
             .Parameters = paramDict
        }
        'Dim success As Boolean = CallCrystalPrintService(req)

        Dim success As Boolean
        success = PrintCrystalReport(req, isPrint)
        'If MsgBox("Print", vbYesNo) = vbYes Then

        '    success = PrintCrystalReport(req, True)
        'Else

        '    success = PrintCrystalReport(req, False)
        'End If


    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Val(lbldocentry.Text) > 0 Then
            If MsgBox("Print via Crystal Report", vbYesNo) = vbYes Then
                Call crystalbunadd()
            Else
                Call bundappbarcode()
            End If

            'Call bundappbarcode()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'MsgBox(Format(CDate(Now()), "dd-MM-yyyy HH:mm:ss"))
        crystalrrQr(Val(lbldocentry.Text))
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        mreportname = "test"
        Dim paramDict As New Dictionary(Of String, Object)
        paramDict.Add("dockey@", Val(lbldocentry.Text))
        'paramDict.Add("period@", cmbyear.Text)
        ' paramDict("Dockey@") = Val(Label7.Text)
        ' Dim req As New PrintRequest() With {
        '     .ReportName = mreportname,
        '     .PrinterName = "",     ' "" = preview
        '     .UseDB = False,
        '     .Parameters = paramDict
        '}



        Dim isPrint As Boolean = (MsgBox("Print", vbYesNo) = vbYes)

        Dim req As New PrintRequest() With {
             .ReportName = mreportname,
             .PrinterName = If(isPrint, mlsprinter, ""),     ' "" = preview
             .UseDB = True,
             .ServerName = mdbserver,
             .DatabaseName = mdbname,
             .DBUser = mdbuserid,
             .DBPassword = mdbpwd,
             .Parameters = paramDict
        }
        'Dim success As Boolean = CallCrystalPrintService(req)

        Dim success As Boolean
        success = PrintCrystalReport(req, isPrint)


    End Sub

    Private Sub lbldate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles lbldate.MaskInputRejected

    End Sub

    Private Sub crystalrrQr(ByVal dcentry As Integer)

        'Me.Cursor = Cursors.WaitCursor
        'Dim cryRpt As New ReportDocument()
        'Dim tsql, path As String
        'Dim qrcodestr As String
        'Path = Application.StartupPath().TrimEnd("\") & "\Reports"
        ''cryRpt.Load(Trim(mreppath) & "Company Analysis Report.rpt")
        'If cmbtype.Text = "DATE ORDER" Then
        '    cryRpt.Load(Trim(mreppath) & "Inner Packing Slip RRdelQR.rpt")
        '    tsql = "select * from rdln7 where docentry=" & dcentry
        'Else
        '    If mproduction = "Y" Then
        '        cryRpt.Load(Trim(mreppath) & "Inner Packing Slip PrdQR.rpt")
        '    Else
        '        cryRpt.Load(Trim(mreppath) & "Inner Packing Slip RRQR.rpt")
        '    End If

        '    tsql = "select * from rinv7 where docentry=" & dcentry

        'End If


        '    Dim dt As DataTable = getDataTable(tsql)
        '    If dt.Rows.Count > 0 Then
        '        For Each rw As DataRow In dt.Rows

        '            If My.Computer.FileSystem.FileExists(Application.StartupPath + "\\Qrcode.jpg") Then
        '                My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\\Qrcode.jpg")
        '            End If

        '            'If My.Computer.FileSystem.FileExists(Trim(mreppath) & "Qrcode.jpg") Then
        '            '    My.Computer.FileSystem.DeleteFile(Trim(mreppath) & "Qrcode.jpg")
        '            'End If

        '            If cmbtype.Text = "DATE ORDER" Then
        '                qrcodestr = "D-" + rw("docentry").ToString.Trim() + "-" + rw("packagenum").ToString.Trim()
        '            Else
        '                qrcodestr = "I-" + rw("docentry").ToString.Trim() + "-" + rw("packagenum").ToString.Trim()
        '            End If

        '            'For Each row As DataGridViewRow In loaddv.Rows
        '            'If row.Cells(3).Value = Me.ListBox1.SelectedItem Then

        '            If IsDBNull(rw("docentry")) = False Then
        '                Dim qrGen = New QRCoder.QRCodeGenerator()
        '                Dim qrCode = qrGen.CreateQrCode(qrcodestr, QRCoder.QRCodeGenerator.ECCLevel.Q)
        '                Dim qrBmp = New BitmapByteQRCode(qrCode)
        '                Dim bt() As Byte = qrBmp.GetGraphic(2)
        '                Dim pictureBytes As New MemoryStream(bt)
        '                PictureBox1.Image = Image.FromStream(pictureBytes)
        '                PictureBox1.Image.Save(Application.StartupPath + "\\Qrcode.jpg")
        '                'PictureBox1.Image.Save(Trim(mreppath) & "Qrcode.jpg")
        '            Else
        '                Dim qrGen = New QRCoder.QRCodeGenerator()
        '                Dim qrCode = qrGen.CreateQrCode("eyjhb1", QRCoder.QRCodeGenerator.ECCLevel.Q)
        '                Dim qrBmp = New BitmapByteQRCode(qrCode)
        '                Dim bt() As Byte = qrBmp.GetGraphic(2)
        '                Dim pictureBytes As New MemoryStream(bt)
        '                PictureBox1.Image = Image.FromStream(pictureBytes)
        '                PictureBox1.Image.Save(Application.StartupPath + "\\Qrcode.jpg")
        '                'PictureBox1.Image.Save(Trim(mreppath) & "Qrcode.jpg")
        '            End If

        '            CrystalReportLogOn(cryRpt, Trim(mkserver), dbnam, Trim(dbuser), Trim(mkpwd))
        '            cryRpt.SetParameterValue("Dockey@", Val(lbldocentry.Text))
        '            cryRpt.SetParameterValue("packnum@", rw("packagenum"))
        '            cryRpt.SetParameterValue("Qrpath", Application.StartupPath + "\\Qrcode.jpg")
        '            'cryRpt.SetParameterValue("Qrpath", Trim(mreppath) & "Qrcode.jpg")

        '            Me.view1.ReportSource = cryRpt




        '            '**
        '            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        '            doctoprint.PrinterSettings.PrinterName = prntername '(ex. "Epson SQ-1170 ESC/P 2")
        '            For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
        '                Dim rawKind As Integer
        '                If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inner" Then
        '                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
        '                    cryRpt.PrintOptions.PaperSize = rawKind
        '                    Exit For
        '                End If
        '            Next


        '            cryRpt.PrintOptions.PrinterName = prntername
        '            cryRpt.PrintToPrinter(1, False, 0, 0)
        '            'Me.view1.PrintReport()
        '            Me.view1.Refresh()

        '        Next
        'End If

        '    cryRpt.Dispose()



        '    Me.Cursor = Cursors.Default

    End Sub


    Private Sub bundleaddress()
        'Me.Cursor = Cursors.WaitCursor
        'Dim cryRpt As New ReportDocument()

        'cryRpt.Load(Trim(mreppath) & "Address Print ws bundle-acc.rpt")
        'CrystalReportLogOn(cryRpt, Trim(mkserver), dbnam, Trim(dbuser), Trim(mkpwd))
        'cryRpt.SetParameterValue("Dockey@", Val(lbldocentry.Text))
        'cryRpt.SetParameterValue("period@", cmbyear.Text)
        'cryRpt.SetParameterValue("Wgt", cmbyear.Text)


        'Me.view1.ReportSource = cryRpt

        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        'doctoprint.PrinterSettings.PrinterName = prntername '(ex. "Epson SQ-1170 ESC/P 2")
        'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
        '    Dim rawKind As Integer
        '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inner" Then
        '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
        '        cryRpt.PrintOptions.PaperSize = rawKind
        '        Exit For
        '    End If
        'Next

        'cryRpt.PrintOptions.PrinterName = prntername
        'cryRpt.PrintToPrinter(1, False, 0, 0)
        ''Me.view1.PrintReport()
        'Me.view1.Refresh()
        'cryRpt.Dispose()
        'Me.Cursor = Cursors.Default
    End Sub
    Private Sub bundappbarcodeold()
        Dim dir As String
        Dim madd1, madd2, madd3, madd4, madd5, mcell, mcardfname, mtransport, mdes, mremark, minvno, mdist As String
        Dim mpackage, maxpack As Integer
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "bundadd.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)

        If Chkprndir.Checked = True Then
            FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        Else
            FileOpen(1, mdir, OpenMode.Output)
        End If

        'Dim dtw As DataTable = getDataTable("select docentry,packagenum,weight from rinv7 where docentry=" & Val(lbldocentry.Text)) & " and packagenum"


        Dim dt2 As DataTable = getDataTable("select max(packagenum) maxpak from rinv7 where docentry=" & Val(lbldocentry.Text))
        If dt2.Rows.Count > 0 Then
            For Each rw1 As DataRow In dt2.Rows
                maxpack = rw1("maxpak")
            Next
        End If

        Dim dt As DataTable = getDataTable("exec [@PRINTLAYOUTMAIN] 'Address','" & Trim(cmbyear.Text) & "'," & Val(txtno.Text))
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                mcardfname = Trim(rw("cardfname") & vbNullString)
                madd1 = rw("building") & vbNullString
                If Len(Trim(Replace(rw("block"), "-", ""))) > 0 Then
                    madd2 = Trim(rw("building") & vbNullString)
                Else
                    madd2 = ""
                End If
                If Len(Trim(Replace(rw("street"), "-", ""))) > 0 Then
                    madd3 = Trim(rw("street") & vbNullString)
                Else
                    madd3 = ""
                End If

                If Len(Trim(Replace(rw("City"), "-", ""))) > 0 Then
                    madd4 = rw("City") & " - " & rw("zipcode") & vbNullString
                Else
                    madd4 = ""
                End If

                If Len(Trim(Replace(rw("state"), "-", ""))) > 0 Then
                    madd5 = Trim(rw("state") & vbNullString)
                Else
                    madd5 = ""
                End If
                If Len(Trim(Replace(rw("Cellular"), "-", ""))) > 0 Then
                    mcell = Trim(rw("Cellular") & vbNullString)
                Else
                    mcell = ""
                End If
                If IsDBNull(rw("packagenum")) = False Then
                    mpackage = rw("packagenum")
                Else
                    mpackage = 0
                End If
                If IsDBNull(rw("u_transport")) = False Then
                    mtransport = rw("u_transport")
                Else
                    mtransport = ""
                End If
                If IsDBNull(rw("u_dsnation")) = False Then
                    mdes = rw("u_dsnation")
                Else
                    mdes = ""
                End If
                If IsDBNull(rw("u_remarks2")) = False Then
                    mremark = rw("u_remarks2")
                Else
                    mremark = ""
                End If
                If IsDBNull(rw("invoicenos")) = False Then
                    minvno = rw("invoicenos")
                Else
                    minvno = ""
                End If

                If IsDBNull(rw("district")) = False Then
                    mdist = rw("district")
                Else
                    mdist = ""
                End If



                PrintLine(1, TAB(0), "<xpml><page quantity='0' pitch='210.1 mm'></xpml>SIZE 107.5 mm, 210.1 mm")
                PrintLine(1, TAB(0), "DIRECTION 0,0")
                PrintLine(1, TAB(0), "REFERENCE 0,0")
                PrintLine(1, TAB(0), "OFFSET 0 mm")
                PrintLine(1, TAB(0), "SPEED 14")
                PrintLine(1, TAB(0), "SET PEEL OFF")
                PrintLine(1, TAB(0), "SET CUTTER OFF")
                PrintLine(1, TAB(0), "SET PARTIAL_CUTTER OFF")
                PrintLine(1, TAB(0), "<xpml></page></xpml><xpml><page quantity='1' pitch='210.1 mm'></xpml>SET TEAR ON")
                PrintLine(1, TAB(0), "CLS")
                PrintLine(1, TAB(0), "CODEPAGE 1252")
                PrintLine(1, TAB(0), "TEXT 46,1641," & """0""" & ",270,23,20," & """To,""")
                PrintLine(1, TAB(0), "TEXT 143,1618," & """0""" & ",270,34,27," & """M/s." & Trim(mcardfname) & """")
                PrintLine(1, TAB(0), "TEXT 643,1598," & """0""" & ",270,14,15," & """ATITHYA CLOTHING COMPANY """)
                PrintLine(1, TAB(0), "TEXT 692,1598," & """0""" & ",270,14,15," & """(A Unit of ENES Textile Mills),""")
                PrintLine(1, TAB(0), "TEXT 741,1598," & """0""" & ",270,14,15," & """No.2/453,SVD Nagar, Kovilpappakudi, """)
                PrintLine(1, TAB(0), "TEXT 790,1598," & """0""" & ",270,14,15," & """Madurai-625018, TN.""")
                PrintLine(1, TAB(0), "TEXT 645,860," & """0""" & ",270,14,17," & """Transport : """)
                PrintLine(1, TAB(0), "TEXT 646,664," & """0""" & ",270,16,20," & """" & Trim(mtransport) & """")
                PrintLine(1, TAB(0), "TEXT 711,860," & """0""" & ",270,14,17," & """Destination : """)
                PrintLine(1, TAB(0), "TEXT 712,642," & """0""" & ",270,16,20," & """" & Trim(mdes) & """")
                PrintLine(1, TAB(0), "TEXT 774,860," & """0""" & ",270,14,17," & """" & Trim(mremark) & """")
                PrintLine(1, TAB(0), "TEXT 775,700," & """0""" & ",270,17,20," & """ONLINE TIRUPUR""")
                PrintLine(1, TAB(0), "TEXT 35,804," & """0""" & ",270,20,20," & """INV : " & Trim(minvno) & " / Bundle No : " & mpackage & " / " & maxpack & """")
                PrintLine(1, TAB(0), "TEXT 225,1618," & """0""" & ",270,21,24," & """" & Trim(madd1) & "," & Trim(madd2) & ",""")
                PrintLine(1, TAB(0), "TEXT 300,1618," & """0""" & ",270,21,24," & """" & Trim(madd3) & """")
                PrintLine(1, TAB(0), "TEXT 368,1618," & """0""" & ",270,21,24," & """" & Trim(madd4) & """")
                PrintLine(1, TAB(0), "TEXT 445,1618," & """0""" & ",270,24,27," & """" & Trim(mdist) & " " & Trim(madd5) & ". " & IIf(Len(Trim(mcell)) > 0, "Mobile No: " & mcell, "") & """")
                'PrintLine(1, TAB(0), "Text(534, 1618, '0', 270, 21, 24," & "TIRUPUR")
                PrintLine(1, TAB(0), "PRINT 1,1")
                PrintLine(1, TAB(0), "<xpml></page></xpml><xpml><end/></xpml>")

            Next

        End If

        'PrintLine(1, TAB(0), mkstr)
        'mkstr = ""

        FileClose(1)
        If Chkprndir.Checked = False Then
            If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                ' Call updateprn()
                If MsgBox("Lpt Port", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text) & ":")
                    Shell("rawpr.bat " & mdir)

                Else
                    Dim text As String = File.ReadAllText(mdir)
                    Dim pd As PrintDialog = New PrintDialog()
                    pd.PrinterSettings = New PrinterSettings()
                    BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName, text)
                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                    'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                    'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                End If
            End If
        End If

    End Sub
    Private Sub bundappbarcode(Optional ByVal packageno As Int32 = 0)
        Dim dir As String
        Dim madd1, madd2, madd3, madd4, madd5, mcell, mcardfname, mtransport, mdes, mremark, minvno, mdist, mbar As String
        Dim mpackage, maxpack, mpackno As Integer
        Dim mwgt As String

        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "bundadd.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)

        If Chkprndir.Checked = True Then
            FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        Else
            FileOpen(1, mdir, OpenMode.Output)
        End If
        Dim dt2 As DataTable = getDataTable("select max(packagenum) maxpak from rinv7 where docentry=" & Val(lbldocentry.Text))
        If dt2.Rows.Count > 0 Then
            For Each rw1 As DataRow In dt2.Rows
                maxpack = rw1("maxpak")
            Next
        End If

        'Dim dt3 As DataTable = getDataTable("select packagenum,weight from rinv7 where docentry=" & Val(lbldocentry.Text) & " and packagenum=" & packageno)
        'If dt3.Rows.Count > 0 Then
        '    For Each rw2 As DataRow In dt3.Rows
        '        mpackno = rw2("packagenum")
        '        mwgt = rw2("weight")

        '    Next
        'End If



        Dim dt As DataTable = getDataTable("exec [@PRINTLAYOUTMAIN] 'Address','" & Trim(cmbyear.Text) & "'," & Val(txtno.Text))
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                mcardfname = Trim(rw("cardfname") & vbNullString)
                madd1 = rw("building") & vbNullString
                If Len(Trim(Replace(rw("block"), "-", ""))) > 0 Then
                    madd2 = Trim(rw("block") & vbNullString)
                Else
                    madd2 = ""
                End If
                If Len(Trim(Replace(rw("street"), "-", ""))) > 0 Then
                    madd3 = Trim(rw("street") & vbNullString)
                Else
                    madd3 = ""
                End If

                If Len(Trim(Replace(rw("City"), "-", ""))) > 0 Then
                    madd4 = rw("City") & " - " & rw("zipcode") & vbNullString
                Else
                    madd4 = ""
                End If

                If Len(Trim(Replace(rw("state"), "-", ""))) > 0 Then
                    madd5 = Trim(rw("state") & vbNullString)
                Else
                    madd5 = ""
                End If
                If Len(Trim(Replace(rw("Cellular"), "-", ""))) > 0 Then
                    mcell = Trim(rw("Cellular") & vbNullString)
                Else
                    mcell = ""
                End If
                If IsDBNull(rw("packagenum")) = False Then
                    mpackage = rw("packagenum")
                Else
                    mpackage = 0
                End If
                If IsDBNull(rw("u_transport")) = False Then
                    mtransport = rw("u_transport")
                Else
                    mtransport = ""
                End If
                If IsDBNull(rw("u_dsnation")) = False Then
                    mdes = rw("u_dsnation")
                Else
                    mdes = ""
                End If
                If IsDBNull(rw("u_remarks2")) = False Then
                    mremark = rw("u_remarks2")
                Else
                    mremark = ""
                End If
                If IsDBNull(rw("invoicenos")) = False Then
                    minvno = rw("invoicenos")
                Else
                    minvno = ""
                End If

                If IsDBNull(rw("district")) = False Then
                    mdist = rw("district")
                Else
                    mdist = ""
                End If

                mpackno = rw("packagenum")
                mwgt = Format(Convert.ToDecimal(rw("wgt")), "####.000").ToString.Trim
                mbar = rw("bar")
                If IsDBNull(rw("u_remarks2")) = False Then
                    mremark = rw("u_remarks2")
                Else
                    mremark = ""
                End If


                If mos = "WIN" Then
                    'PrintLine(1, TAB(0), "<xpml><page quantity='0' pitch='210.1 mm'></xpml>SIZE 107.10 mm, 209.6 mm")
                    PrintLine(1, TAB(0), "SIZE 107.10 mm, 209.6 mm")
                Else
                    PrintLine(1, TAB(0), "SIZE 107.10 mm, 209.6 mm")
                End If

                PrintLine(1, TAB(0), "DIRECTION 0,0")
                PrintLine(1, TAB(0), "REFERENCE 0,0")
                PrintLine(1, TAB(0), "OFFSET 0 mm")
                PrintLine(1, TAB(0), "SPEED 14")
                PrintLine(1, TAB(0), "SET PEEL OFF")
                PrintLine(1, TAB(0), "SET CUTTER OFF")
                PrintLine(1, TAB(0), "SET PARTIAL_CUTTER OFF")
                If mos = "WIN" Then
                    'PrintLine(1, TAB(0), "<xpml></page></xpml><xpml><page quantity='1' pitch='209.6 mm'></xpml>SET TEAR ON")
                    PrintLine(1, TAB(0), "SET TEAR ON")
                Else
                    PrintLine(1, TAB(0), "SET TEAR ON")
                End If

                PrintLine(1, TAB(0), "CLS")
                PrintLine(1, TAB(0), "CODEPAGE 1252")


                PrintLine(1, TAB(0), "TEXT 828,107," & """ROMAN.TTF""" & ",90,1,24," & """To,""")
                PrintLine(1, TAB(0), "TEXT 809,829," & """ROMAN.TTF""" & ",90,1,18," & """INV : " & Trim(minvno) & " / Bundle No : " & mpackage & " / " & maxpack & """")
                PrintLine(1, TAB(0), "BAR 311,1570,445,3")
                PrintLine(1, TAB(0), "BAR 38,1570,274,3")
                PrintLine(1, TAB(0), "BAR 38,102,3,1469")
                PrintLine(1, TAB(0), "BAR 39,102,274,3")
                PrintLine(1, TAB(0), "BAR 312,102,445,3")
                PrintLine(1, TAB(0), "BAR 755,103,3,1469")
                PrintLine(1, TAB(0), "BAR 311,102,3,1471")
                PrintLine(1, TAB(0), "TEXT 734,113," & """ROMAN.TTF""" & ",90,1,24," & """M/s." & Trim(mcardfname) & """")
                PrintLine(1, TAB(0), "TEXT 658,113," & """ROMAN.TTF""" & ",90,1,18," & """" & Trim(madd1) & ",""")
                PrintLine(1, TAB(0), "TEXT 599,113," & """ROMAN.TTF""" & ",90,1,20," & """" & Trim(madd2) & ",""")
                PrintLine(1, TAB(0), "TEXT 531,113," & """ROMAN.TTF""" & ",90,1,24," & """" & Trim(madd3) & """")
                PrintLine(1, TAB(0), "TEXT 451,113," & """ROMAN.TTF""" & ",90,1,24," & """" & Trim(madd4) & """")
                PrintLine(1, TAB(0), "TEXT 373,113," & """ROMAN.TTF""" & ",90,1,20," & """" & Trim(mdist) & " " & Trim(madd5) & ". " & IIf(Len(Trim(mcell)) > 0, "Mobile No: " & mcell, "") & """")

                PrintLine(1, TAB(0), "TEXT 306,113," & """ROMAN.TTF""" & ",90,1,15," & """From :""")

                PrintLine(1, TAB(0), "TEXT 258,113," & """ROMAN.TTF""" & ",90,1,15," & """ATITHYA CLOTHING COMPANY """)
                PrintLine(1, TAB(0), "TEXT 212,113," & """ROMAN.TTF""" & ",90,1,11," & """(A Unit of ENES Textile Mills),""")
                PrintLine(1, TAB(0), "TEXT 168,113," & """ROMAN.TTF""" & ",90,1,13," & """No.2/453,SVD Nagar, Kovilpappakudi, """)
                PrintLine(1, TAB(0), "TEXT 127,113," & """ROMAN.TTF""" & ",90,1,13," & """Madurai-625018, TN.""")
                PrintLine(1, TAB(0), "TEXT 289,797," & """ROMAN.TTF""" & ",90,1,15," & """Transport : " & Trim(mtransport) & """")
                PrintLine(1, TAB(0), "BAR 40,787, 272, 3")
                'PrintLine(1, TAB(0), "TEXT 646,664," & """0""" & ",270,16,20," & """" & Trim(mtransport) & """")
                PrintLine(1, TAB(0), "TEXT 239,797," & """ROMAN.TTF""" & ",90,1,18," & """Destination : " & Trim(mdes) & """")
                'PrintLine(1, TAB(0), "TEXT 712,642," & """0""" & ",270,16,20," & """" & Trim(mdes) & """")
                PrintLine(1, TAB(0), "TEXT 123,1356," & """ROMAN.TTF""" & ",90,1,20," & """Wt." & Trim(mwgt) & """")
                'PrintLine(1, TAB(0), "BARCODE 164,797," & """39""" & ",102,0,90,3,8," & """" & Trim(mbar) & """")

                'PrintLine(1, TAB(0), "TEXT 774, 860, " & """0""" & ", 270, 14, 17, " & """" & Trim(mremark) & """")
                'PrintLine(1, TAB(0), "TEXT 775, 700, " & """0""" & ", 270, 17, 20, " & """ONLINE TIRUPUR""")
                'PrintLine(1, TAB(0), "TEXT 35, 804, " & """0""" & ", 270, 20, 20, " & """INV :  " & Trim(minvno) & " / Bundle No : " & mpackage & " / " & maxpack & """")
                'PrintLine(1, TAB(0), "TEXT 225,1618," & """0""" & ",270,21,24," & """" & Trim(madd1) & "," & Trim(madd2) & ",""")
                'PrintLine(1, TAB(0), "TEXT 300,1618," & """0""" & ",270,21,24," & """" & Trim(madd3) & """")
                'PrintLine(1, TAB(0), "TEXT 368,1618," & """0""" & ",270,21,24," & """" & Trim(madd4) & """")
                'PrintLine(1, TAB(0), "TEXT 445,1618," & """0""" & ",270,24,27," & """" & Trim(mdist) & " " & Trim(madd5) & ". " & IIf(Len(Trim(mcell)) > 0, "Mobile No: " & mcell, "") & """")
                'PrintLine(1, TAB(0), "Text(534, 1618, '0', 270, 21, 24," & "TIRUPUR")
                PrintLine(1, TAB(0), "PRINT 1,1")
                If mos = "WIN" Then
                    'PrintLine(1, TAB(0), "<xpml></page></xpml><xpml><end/></xpml>")
                End If


            Next

        End If

        'PrintLine(1, TAB(0), mkstr)
        'mkstr = ""

        FileClose(1)
        If mos = "WIN" Then
            If Chkprndir.Checked = False Then
                If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                    ' Call updateprn()
                    If MsgBox("Lpt Port", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                        'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text) & ":")
                        Shell("rawpr.bat " & mdir)

                    Else
                        Dim text As String = File.ReadAllText(mdir)
                        Dim pd As PrintDialog = New PrintDialog()
                        pd.PrinterSettings = New PrinterSettings()
                        BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName, text)
                        'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                        'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                        'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                    End If
                End If
            End If
        Else
            Dim printer As String = tscprinter1
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "bundadd.txt"
            Dim success As Boolean = PrintTscRaw(printer, filePathname)
        End If


    End Sub
    Private Sub innerslip(docentry As Integer)
        If cmbtype.Text = "SALES" Then
            msql = "select * from rinv7 where docentry=" & docentry & " order by docentry,packagenum "
        ElseIf cmbtype.Text = "DATE ORDER" Then
            msql = "select * from rdln7 where docentry=" & docentry & " order by docentry,packagenum "
        End If
        Dim mpakno As Integer
        Dim dtk As DataTable = getDataTable(msql)
        If dtk.Rows.Count > 0 Then
            For Each rowk As DataRow In dtk.Rows
                mpakno = rowk("packagenum")
                innerprintnew(docentry, mpakno)
            Next
        End If
    End Sub


    Private Sub innerprintnew(docentry As Integer, packno As Integer)
      
        Dim sb As New System.Text.StringBuilder()

        Dim qry As String = ""

        If cmbtype.Text = "SALES" Then
            qry = "select ROW_NUMBER() over (partition by b.packagenum order by b.packagenum,b.id) sno,c.docnum,b.docentry,c.docdate,d.bundleno U_Bundle, c.cardcode,b.packagenum,c.U_Transport," _
                            & " c.U_Destination,c.U_Destion, b.itemcode,t.u_brandgroup itemname,b.u_style Style,b.u_size Size,isnull(t.u_colcode,'') colcode,b.Quantity,sum(b.quantity) over (partition by packagenum) totpcs, " _
                           & " case when isnull(e.u_brch,'')='' then isnull(r.cardfname,c.cardname) else e.u_brch end Cardname,e.building,e.block,e.street,e.city,e.zipcode,e.state, " _
            & "'I-'+ltrim(rtrim(convert(varchar(15),b.docentry)))+'-'+ltrim(convert(varchar(5),b.packagenum)) bundleno,isnull(convert(nvarchar(max),c.u_remarks3),'') remarks,rtrim(isnull(convert(nvarchar(max),br.u_remarks),'')) as stype from rinv8 b " _
                           & " inner join oinv c with (nolock) on c.docentry=b.docentry " _
                           & " inner join (select max(packagenum) bundleno ,docentry from rinv7  group by docentry) d on d.docentry=c.docentry " _
                           & " inner join ocrd r with (nolock) on r.cardcode=c.cardcode  " _
                           & " inner join crd1 e on e.cardcode=c.cardcode and e.address=c.ShipToCode " _
                           & " inner join oitm t on t.itemcode=b.itemcode " _
                           & " inner join nnm1 s on s.series=c.series " _
                           & " inner join [@incm_bnd1] br on br.u_name=c.u_brand " _
            & " where b.docentry = " & docentry & " And b.packagenum = " & packno _
            & "	order by b.docentry,b.PackageNum,ROW_NUMBER() over (partition by b.packagenum order by b.packagenum,b.id)"
        ElseIf cmbtype.Text = "DATE ORDER" Then
            qry = "select ROW_NUMBER() over (partition by b.packagenum order by b.packagenum,b.id) sno,c.docnum,b.docentry,c.docdate,d.bundleno U_Bundle, c.cardcode,b.packagenum,c.U_Transport," _
                            & " c.U_Destination,c.U_Destion, b.itemcode,t.u_brandgroup itemname,b.u_style Style,b.u_size Size,isnull(t.u_colcode,'') colcode,b.Quantity,sum(b.quantity) over (partition by packagenum) totpcs, " _
                            & " case when isnull(e.u_brch,'')='' then isnull(r.cardfname,c.cardname) else e.u_brch end Cardname,e.building,e.block,e.street,e.city,e.zipcode,e.state, " _
            & "'D-'+ltrim(rtrim(convert(varchar(15),b.docentry)))+'-'+ltrim(convert(varchar(5),b.packagenum)) bundleno,isnull(convert(nvarchar(max),c.u_remarks3),'') remarks,rtrim(isnull(convert(nvarchar(max),br.u_remarks),'')) as stype from rdln8 b " _
                            & "inner join odln c with (nolock) on c.docentry=b.docentry " _
                            & "inner join (select max(packagenum) bundleno ,docentry from rdln7  group by docentry) d on d.docentry=c.docentry " _
                            & " inner join ocrd r with (nolock) on r.cardcode=c.cardcode  " _
                            & "inner join crd1 e on e.cardcode=c.cardcode and e.address=c.ShipToCode " _
                            & "inner join oitm t on t.itemcode=b.itemcode  " _
                            & "inner join nnm1 s on s.series=c.series " _
                            & "inner join [@incm_bnd1] br on br.u_name=c.u_brand " _
            & " where b.docentry = " & docentry & " And b.packagenum = " & packno _
            & " 	order by b.docentry,b.PackageNum,ROW_NUMBER() over (partition by b.packagenum order by b.packagenum,b.id) "
        End If

        Dim mpackagenum As Integer = 0
        Dim dtc As DataTable = getDataTable(qry)
        If dtc.Rows.Count > 0 Then


            'Dim y As Integer = 3407
            'Dim gap As Integer = 44
            'Dim yy As Integer = 0

            Dim RowHeight As Integer = 44        ' each row height in dots
            Dim HeaderHeight As Integer = 625    ' top portion
            Dim FooterHeight As Integer = 300    ' bottom portion
            Dim TotalRows As Integer = dtc.Rows.Count

            Dim TotalHeight As Integer = HeaderHeight + FooterHeight + (TotalRows * RowHeight)

            Dim newy As Double = Convert.ToInt32((TotalHeight / 8) * 8)
            Dim scale As Single = (newy / 4024)
            Dim x As Integer = 0

            'Dim y As Integer = 3407
            Dim y As Integer = 0
            Dim gap As Integer = 44
            Dim yy As Integer = 0
            Dim ny As Integer = 0
            'Dim sno As Integer = 1
            Dim ku_noofbun As String = dtc.Rows(0)("u_bundle").ToString.Trim

            Dim kcardname As String = Trim(dtc.Rows(0)("cardname"))
            Dim kcity As String = Trim(dtc.Rows(0)("city"))
            Dim kdocnum As Integer = dtc.Rows(0)("docnum")
            Dim kdocentry As Integer = dtc.Rows(0)("docentry")
            'Dim kpackagenument As String = "Packing No. : " & dtc.Rows(0)("docentry").ToString.Trim & " / " & dtc.Rows(0)("packagenum").ToString.Trim
            Dim kpackagenum As String = "Packing No. : " & dtc.Rows(0)("docnum").ToString.Trim & " / " & dtc.Rows(0)("u_bundle").ToString.Trim
            Dim ktransport As String = "Transport : " & dtc.Rows(0)("U_Transport").ToString.Trim
            Dim kdocdate As String = "Date : " & CDate(dtc.Rows(0)("docdate")).ToString("dd-MM-yyyy")
            Dim kbundleno As String = dtc.Rows(0)("bundleno").ToString.Trim
            Dim kpackno As String = "Bundle No : " & dtc.Rows(0)("packagenum").ToString.Trim
            Dim ktoday As String = Today.ToShortDateString.ToString()
            Dim ktime As String = TimeOfDay.ToString("hh:mm:ss tt")
            Dim ktotpcs As Integer = dtc.Rows(0)("totpcs")
            Dim kremarks As String = dtc.Rows(0)("remarks")
            Dim ktype As String = "Type : " & dtc.Rows(0)("stype")
            Dim kterms As String = "DO NOT ACCEPT IF STRAPPING TAGS AND BOXES ARE BROKEN"
            'sb.AppendLine("<xpml><page quantity='0' pitch='39.0 mm'></xpml>")
            'sb.AppendLine("SIZE 107.10 mm, 508 mm")
            If mos = "WIN" Then
                sb.AppendLine("SIZE 107.10 mm," & (TotalHeight / 8) & " mm")
            Else
                ' sb.AppendLine("SIZE 107.10," & TotalHeight)
                sb.AppendLine("SIZE 107.10 mm," & (TotalHeight / 8) & " mm")

            End If

            sb.AppendLine("DIRECTION 0,0")
                sb.AppendLine("REFERENCE 0,0")
            sb.AppendLine("OFFSET 0 mm")
            If mos = "WIN" Then
            Else

                sb.AppendLine("GAP 0,0")
            End If
            sb.AppendLine("SPEED 7")
                'sb.AppendLine("SET MEDIA TYPE CONTINUOUS")
                'sb.AppendLine("SET BACKFEED 0")
                sb.AppendLine("SET PEEL OFF")
                sb.AppendLine("SET CUTTER OFF")
                'sb.AppendLine("SET PEEL OFF")
                'sb.AppendLine("SET CUTTER OFF")
                ' sb.AppendLine("<xpml></page></xpml><xpml><page quantity='1' pitch='39.0 mm'></xpml>")
                sb.AppendLine("SET TEAR ON")
                sb.AppendLine("CLS")

                sb.AppendLine("CODEPAGE 1252")

                '****
                newy -= 64
                sb.AppendLine("TEXT 551," & newy & ",""ROMAN.TTF"",180,1,12,""Atithya Clothing Company - Madurai""")
                sb.AppendLine("TEXT 804," & newy & ",""0"",180,12,12,""" & ktype & """")
                newy -= 64
                sb.AppendLine("BAR 3," & newy & ",880,3")
                newy -= 5
                x = CenterX(kcardname, 13, 107.1, 180)
                'x = CenterXrotat(kcardname, 13, 107.1, True)
                'sb.AppendLine($"TEXT 688,{newy},""ROMAN.TTF"",180,1,13,""{kcardname}""")
                sb.AppendLine("TEXT " & x & "," & newy & ",""ROMAN.TTF"",180,1,13,""" & kcardname & """")
                newy -= 44
                x = CenterX(kcity, 12, 107.1, 180)
                'x = CenterXrotat(kcity, 13, 107.1, True)
                'sb.AppendLine($"TEXT 754,{newy},""ROMAN.TTF"",180,1,12,""{kcity}""")
                sb.AppendLine("TEXT " & x & "," & newy & ",""ROMAN.TTF"",180,1,12,""" & kcity & """")
                newy -= 56
                sb.AppendLine("BAR 3," & newy & ",880,3")
                newy -= 27
                sb.AppendLine("QRCODE 241," & newy & ",L,9,A,180,M2,S7,""" & kbundleno & """")
                newy -= 21
                sb.AppendLine("TEXT 830, " & newy & ", ""ROMAN.TTF"", 180, 1, 12, """ & kpackagenum & """")
                newy -= 66
                sb.AppendLine("TEXT 828," & newy & ",""ROMAN.TTF"",180,1,12,""" & kdocdate & """")
                newy -= 60
                sb.AppendLine("TEXT 828," & newy & ",""ROMAN.TTF"",180,1,12,""" & kpackno & """")
                newy -= 62
                sb.AppendLine("TEXT 828," & newy & ",""ROMAN.TTF"",180,1,12,""" & ktransport & """")
            'newy -= 134

            If Len(Trim(kremarks)) > 0 Then

                ny = newy - 58
                newy -= 134
                If mos = "WIN" Then
                    sb.AppendLine("ERASE 257," & newy & ",348,92")
                    'ny = newy - 43
                    sb.AppendLine("TEXT 604," & ny & ",""0"",180,16,28,""" & kremarks & """")
                    sb.AppendLine("REVERSE 20," & newy & ",800,92")
                Else
                    sb.AppendLine("TEXT 604," & ny & ",""0"",180,16,28,""" & kremarks & """")
                End If

            Else
                newy -= 43

            End If
                newy += 1
                sb.AppendLine("BAR 3," & newy & ", 880, 3")
                newy -= 27

                'sb.AppendLine("TEXT 838,3311,""ROMAN.TTF"",180,1,12,""Transport : KPG""")
                sb.AppendLine("TEXT 832," & newy & ",""ROMAN.TTF"",180,1,11,""S.No""")
                sb.AppendLine("TEXT 634," & newy & ",""ROMAN.TTF"",180,1,11,""Item Name""")
                sb.AppendLine("TEXT 338," & newy & ",""ROMAN.TTF"",180,1,11,""Style""")
                sb.AppendLine("TEXT 210," & newy & ",""ROMAN.TTF"",180,1,11,""Size""")
                sb.AppendLine("TEXT 65," & newy & ",""ROMAN.TTF"",180,1,11,""Qty""")
                'sb.AppendLine("TEXT 550,3652,""ROMAN.TTF"",180,1,12,""Atithya Clothing Company - Madurai""")
                newy -= 38
                sb.AppendLine("BAR 3," & newy & ", 880, 3")
                newy -= 14
                y = newy
                For Each rw As DataRow In dtc.Rows
                    Dim sno As Int16 = Convert.ToInt16(rw("sno").ToString())
                    Dim colcode As String = rw("colcode").ToString()
                    Dim item As String = rw("itemname").ToString()
                    Dim style As String = rw("style").ToString()
                    Dim size As String = rw("size").ToString()
                    Dim qty As String = Convert.ToInt16(rw("quantity")).ToString()
                    Dim itemcolor As String = Trim(item) & "-" & Trim(colcode)
                    ' Format: SNo, Item Name, Style, Size, Qty
                    sb.AppendLine("TEXT 804," & y & ",""ROMAN.TTF"",180,1,11,""" & sno & """")
                    If Len(Trim(colcode)) > 0 Then
                        sb.AppendLine("TEXT 716," & y & ",""ROMAN.TTF"",180,1,11,""" & itemcolor & """")
                    Else
                        sb.AppendLine("TEXT 716," & y & ",""ROMAN.TTF"",180,1,11,""" & item & """")
                    End If

                    sb.AppendLine("TEXT 333," & y & ",""ROMAN.TTF"",180,1,11,""" & style & """")
                    sb.AppendLine("TEXT 197," & y & ",""ROMAN.TTF"",180,1,11,""" & size & """")
                    sb.AppendLine("TEXT 59," & y & ",""ROMAN.TTF"",180,1,11,""" & qty & """")

                    y -= gap      ' Go to next line
                    'sno += 1      ' Increase serial number
                Next
                ' y -= gap

                sb.AppendLine("BAR 3," & y & ", 880, 3")
                'sb.AppendLine("BAR 25,263, 768, 3")
                y -= 10
                sb.AppendLine("TEXT 499," & y & ",""ROMAN.TTF"",180,1,11,""Total Pcs....""")
                sb.AppendLine("TEXT 65," & y & ",""ROMAN.TTF"",180,1,11,""" & ktotpcs & """")
                y -= gap
                y -= gap
                yy = y - 63

            'x = CenterX(kterms, 19, 107.1, 180)
            If mos = "WIN" Then
                sb.AppendLine("ERASE 3," & yy & ", 880, 64")
                sb.AppendLine("TEXT 716, " & (y - 10) & ", ""0"", 180, 10, 19, """ & kterms & """")
                sb.AppendLine("REVERSE 3," & yy & ", 880, 64")
            Else
                sb.AppendLine("TEXT 716, " & (y - 10) & ", ""0"", 180, 10, 19, """ & kterms & """")
            End If


            y -= 73
                sb.AppendLine("TEXT 794," & y & ",""ROMAN.TTF"",180,1,11,""" & ktoday & """")
                sb.AppendLine("TEXT 157," & y & ",""ROMAN.TTF"",180,1,11,""" & ktime & """")
                'If optdateord.Checked = True Then
                If cmbtype.Text = "DATE ORDER" Then
                    sb.AppendLine("TEXT 520," & y & ",""0"",180,12,12,""Date Order""")
                End If

                sb.AppendLine("PRINT 1,1")


                Dim dir As String
                dir = System.AppDomain.CurrentDomain.BaseDirectory()

                Dim rawName As String = sb.ToString().Trim()
                Dim rawdata As String = rawName.Replace(vbCrLf, "").Replace(vbLf, "").Replace(vbCr, "")

                Dim fileName As String = "innerline.txt"

                File.WriteAllText(dir & fileName, sb.ToString().Trim())
                ' Send to printer
                'RawPrinterHelper.SendStringToPrinter(prntername, sb.ToString())

                If mos = "WIN" Then
                    Dim ok As Boolean = RawPrinterHelper.SendStringToPrinter3(mprinter, sb.ToString())

                    If ok Then
                        'MsgBox("PRINTED SUCCESS")
                        If cmbtype.Text = "SALES" Then
                            qry = "update  oinv set u_pckdate=getdate(),  U_Noofbun = '" & ku_noofbun & "',U_Bundle = '" & ku_noofbun & "'  WHERE DOCENTRY =" & docentry
                        ElseIf cmbtype.Text = "DATE ORDER" Then
                            qry = "update  odln set u_pckdate=getdate(),  U_Noofbun = '" & ku_noofbun & "',U_Bundle = '" & ku_noofbun & "'  WHERE DOCENTRY =" & docentry
                        End If
                        Try
                            executeQuery(qry)
                        Catch ex As Exception
                            MsgBox("Error : " & ex.Message)
                        End Try

                    Else
                        MsgBox("FAILED")
                    End If
                Else
                'Try
                '    Dim printer As String = mvertprinter
                '    'Dim filePath As String = mlinpath & "nsbarcodEV.txt"
                '    '"/home/testing/Desktop/Barcodelinux/nsbarcodEV.txt"
                '    Dim filePath As String = mlinpath
                '    Dim filePathname As String = mlinpath & fileName

                '    Dim psi As New ProcessStartInfo()
                '    psi.FileName = "/bin/bash"
                '    psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""
                '    'psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
                '    psi.UseShellExecute = False
                '    psi.CreateNoWindow = True
                'Process.Start(psi)

                Dim printer As String = tscprinter1
                Dim filePath As String = mlinpath
                Dim filePathname As String = mlinpath & fileName
                Dim success As Boolean = PrintTscRaw(printer, filePathname)







                '    If cmbtype.Text = "SALES" Then
                '        qry = "update  oinv set u_pckdate=getdate(),  U_Noofbun = '" & ku_noofbun & "',U_Bundle = '" & ku_noofbun & "'  WHERE DOCENTRY =" & docentry
                '    ElseIf cmbtype.Text = "DATE ORDER" Then
                '        qry = "update  odln set u_pckdate=getdate(),  U_Noofbun = '" & ku_noofbun & "',U_Bundle = '" & ku_noofbun & "'  WHERE DOCENTRY =" & docentry
                '    End If
                '    Try
                '        executeQuery(qry)
                '    Catch ex As Exception
                '        MsgBox("Error : " & ex.Message)
                '    End Try
                'Catch ex As Exception
                '    MsgBox("Filed! " & ex.Message)
                'End Try


            End If


                'Dim sfd As New SaveFileDialog()
                'sfd.Filter = "Text Files|*.txt"
                'sfd.FileName = "testlabel.txt"

                'If sfd.ShowDialog() = DialogResult.OK Then
                '    System.IO.File.WriteAllText(sfd.FileName, sb.ToString())
                'End If
            End If


    End Sub

    Private Sub btnautolbl_Click(sender As System.Object, e As System.EventArgs) Handles btnautolbl.Click
        printautolbl(Val(lbldocentry.Text))
    End Sub

    Private Sub printautolbl(ByVal dcentry As Integer)
        Dim dir As String
        Dim mbundlno, minvnum As String
        Dim adocnum As Integer
        Dim adocdate As String
        'dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        'mdir = Trim(dir) & "\sbarcodE.txt"

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "autolbl.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)

        If Chkprndir.Checked = True Then
            FileOpen(1, "LPT" & Trim(txtport.Text), OpenMode.Output)
        Else
            FileOpen(1, mdir, OpenMode.Output)
        End If


        If cmbtype.Text = "DATE ORDER" Then
            msql = "declare @docentry as integer " _
                   & "set @docentry= " & dcentry _
                  & "select k.docnum,k.docdate,k.docentry,k.brndtype,k.packagenum,sum(k.quantity) qty,k.maxpak,convert(varchar(10),k.packagenum) +'/' + convert(varchar(10),k.maxpak) bundleno,'D-'+rtrim(ltrim(convert(nvarchar(15),k.docentry)))+'-'+rtrim(ltrim(convert(varchar(10),k.packagenum))) invno from ( " _
                  & "select convert(nvarchar(100),bn.u_remarks) brndtype, b.PackageNum,b.DocEntry,t2.CardName, case when isnull(a.U_Distitemname,'')='' then a.U_Subgrp6 else isnull(a.U_Distitemname,'') end as Catalogname,b.u_style,b.u_size,sum(b.Quantity) as Quantity,t2.docnum," _
                  & " T3.City, t3.CardFName, t2.docdate, t2.U_Brand, t2.U_Transport, t2.U_Dsnation, t8.linenum, rr.maxpak  from  rdln8 B " _
                  & "left join OITM a on a.itemcode  = b.ItemCode " _
                  & "left join odln t2 on t2.DocEntry = b.DocEntry " _
                  & " left join (select distinct ItemCode,DocEntry,Dscription,TreeType,sum(LineNum) as Linenum from dln1 group by DocEntry,Dscription,TreeType,ItemCode " _
                  & " ) t8 on t8.docentry = B.docentry and t8.ItemCode = b.ItemCode and t8.TreeType <> 'I'  and t8.Dscription <> 'FrightCharges' " _
                  & " left join OCRD t3 on t3.Cardcode = t2.Cardcode and t3.CardType = 'C' " _
                  & " left join CRD1 t4 on t4.CardCode = t3.CardCode and t4.Address=t2.paytocode " _
                  & "left join  ( SELECT CardCode,MAX(ISNULL(taxid11,'')) taxid11,MAX(ISNULL(TAXID1,'')) TAXID1  FROM CRD7 GROUP BY CardCode) t5 on t5.CardCode = t3.CardCode " _
                  & "left join [@incm_bnd1] bn on bn.u_name=t2.u_brand " _
                  & "left join  (select docentry, max(packagenum) maxpak from rdln8  group by docentry) rr on rr.docentry=t2.docentry " _
                  & " where b.DocEntry =@docentry  and b.ItemCode <> 'FrightCharges' " _
                  & " group by b.PackageNum,b.DocEntry,t2.CardName,A.ItemName,b.u_style,b.u_size, T3.City,t3.CardFName,t2.docdate,t2.U_Brand,t2.U_Transport,t2.U_Dsnation, " _
                  & " t8.linenum,t2.docnum,isnull(a.U_Distitemname,''),a.U_Subgrp6,convert(nvarchar(100),bn.u_remarks),rr.maxpak) k " _
                  & " group by k.docentry,k.brndtype,k.packagenum,k.maxpak,k.docnum,k.docdate Order by k.PackageNum"
        Else
            msql = "declare @docentry as integer " _
                   & " set @docentry=" & dcentry _
                   & " select k.docnum,k.docdate,k.docentry,k.brndtype,k.packagenum,sum(k.quantity) qty,k.maxpak,convert(varchar(10),k.packagenum) +'/' + convert(varchar(10),k.maxpak) bundleno,'I-'+rtrim(ltrim(convert(nvarchar(15),k.docentry)))+'-'+rtrim(ltrim(convert(varchar(10),k.packagenum))) invno from ( " _
                   & " select convert(nvarchar(100),bn.u_remarks) brndtype, b.PackageNum,b.DocEntry,t2.CardName, " _
                   & "case when isnull(a.U_Distitemname,'')='' then a.U_Subgrp6 else isnull(a.U_Distitemname,'') end as Catalogname,b.u_style,b.u_size,sum(b.Quantity) as Quantity,t2.docnum, " _
                   & " T3.City, t3.CardFName, t2.docdate, t2.U_Brand, t2.U_Transport, t2.U_Dsnation, t8.linenum, rr.maxpak from  rinv8 B " _
                   & "left join OITM a on a.itemcode  = b.ItemCode " _
                   & "left join oinv t2 on t2.DocEntry = b.DocEntry " _
                   & "left join (select distinct ItemCode,DocEntry,Dscription,TreeType,sum(LineNum) as Linenum from inv1 group by DocEntry,Dscription,TreeType,ItemCode " _
                   & " ) t8 on t8.docentry = B.docentry and t8.ItemCode = b.ItemCode and t8.TreeType <> 'I'  and t8.Dscription <> 'FrightCharges' " _
                   & " left join OCRD t3 on t3.Cardcode = t2.Cardcode and t3.CardType = 'C'  " _
                   & " left join CRD1 t4 on t4.CardCode = t3.CardCode and t4.Address=t2.paytocode " _
                   & " left join  ( SELECT CardCode,MAX(ISNULL(taxid11,'')) taxid11,MAX(ISNULL(TAXID1,'')) TAXID1  FROM CRD7 GROUP BY CardCode) t5 on t5.CardCode = t3.CardCode " _
                   & " left join [@incm_bnd1] bn on bn.u_name=t2.u_brand " _
                   & " left join  (select docentry, max(packagenum) maxpak from rinv8  group by docentry) rr on rr.docentry=t2.docentry " _
                   & " where b.DocEntry =@docentry and b.ItemCode <> 'FrightCharges'  " _
                   & " group by b.PackageNum,b.DocEntry,t2.CardName,A.ItemName,b.u_style,b.u_size, T3.City,t3.CardFName,t2.docdate,t2.U_Brand,t2.U_Transport,t2.U_Dsnation, " _
                   & " t8.linenum,t2.docnum,isnull(a.U_Distitemname,''),a.U_Subgrp6,convert(nvarchar(100),bn.u_remarks),rr.maxpak) k " _
                   & " group by k.docentry,k.brndtype,k.packagenum,k.maxpak,k.docnum,k.docdate Order by k.PackageNum"

        End If
        Dim ddt As DataTable = getDataTable(msql)
        If ddt.Rows.Count > 0 Then
            For Each rw As DataRow In ddt.Rows
                mbundlno = Trim(rw("bundleno") & vbNullString)
                minvnum = Trim(rw("invno") & vbNullString)
                adocnum = rw("docnum")
                adocdate = Format(CDate(rw("docdate")), "dd-MM-yyyy")
                PrintLine(1, TAB(0), "^XA")
                PrintLine(1, TAB(0), "^PRC")
                PrintLine(1, TAB(0), "^LH0.0^FS")
                PrintLine(1, TAB(0), "^LL304")
                PrintLine(1, TAB(0), "^MD0")
                PrintLine(1, TAB(0), "^MNY")
                PrintLine(1, TAB(0), "^LH0.0^FS")
                If cmbtype.Text = "DATE ORDER" Then
                    PrintLine(1, TAB(0), "^FO370,61^A0N,50,43^CI13^FR^FDDelivery No. :^FS")
                Else

                    PrintLine(1, TAB(0), "^FO370,61^A0N,50,43^CI13^FR^FDInvoice No. :^FS")
                End If

                PrintLine(1, TAB(0), "^FO370,111^A0N,50,45^CI13^FR^FD" & adocnum & "^FS")
                PrintLine(1, TAB(0), "^FO370,181^A0N,50,33^CI13^FR^FDDate : " & Trim(adocdate) & "^FS")
                PrintLine(1, TAB(0), "^FO370,241^A0N,50,38^CI13^FR^FDBundle No. : " & mbundlno & "^FS")
                PrintLine(1, TAB(0), "^BY2,2.0^FO165,61^BQN,3,9,N,Y,N^FR^FD000" & minvnum & "^FS")
                PrintLine(1, TAB(0), "^PQ1,0,0,N")
                PrintLine(1, TAB(0), "^XZ")

            Next

        End If
        FileClose(1)

        If mos = "WIN" Then
            If Chkprndir.Checked = False Then
                If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                    ' Call updateprn()
                    If MsgBox("Lpt Port", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                        'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text) & ":")
                        Shell("rawpr.bat " & mdir)

                    Else
                        Dim text As String = File.ReadAllText(mdir)
                        Dim pd As PrintDialog = New PrintDialog()
                        pd.PrinterSettings = New PrinterSettings()
                        BarcodePrint.SendStringToPrinter(pd.PrinterSettings.PrinterName, text)
                        'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                        'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(txtport.Text))
                        'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
                    End If
                End If
            End If
        Else

            'Dim printer As String = mprinter
            ''Dim filePath As String = mlinpath & "nsbarcodEV.txt"
            ''"/home/testing/Desktop/Barcodelinux/nsbarcodEV.txt"
            'Dim filePath As String = mlinpath
            'Dim filePathname As String = mlinpath & "autolbl.txt"

            'Dim psi As New ProcessStartInfo()
            'psi.FileName = "/bin/bash"
            'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""
            ''psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
            'psi.UseShellExecute = False
            'psi.CreateNoWindow = True
            ''TextBox1.Text = psi.FileName & psi.Arguments
            'Process.Start(psi)

            Dim printer As String = tscprinter1
            Dim filePath As String = mlinpath
            Dim filePathname As String = mlinpath & "autolbl.txt"
            Dim success As Boolean = PrintTscRaw(printer, filePathname)

        End If

    End Sub
End Class
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource

'Imports System.Text


Public Class Frmpackage
    Dim msql, msql2, msql3, msql4, msql5, mdir As String
    Dim mdocno As Long
    Dim j, i, msel As Int32
    Dim mktru As Boolean
    Private Sub Frmpackage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
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
        cmbyear.Text = mpostperiod
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
        Dim CMD As New OleDb.OleDbCommand(msql, con)
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()

        Try
            ''Dim DR As SqlDataReader
            Dim DR As OleDb.OleDbDataReader
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
                'msql = "select  docentry,itemcode,dscription,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from inv1 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by docentry,itemcode,dscription,unitmsr"
                msql = "select linenum, docentry,itemcode,dscription,sum(quantity)  quantity from inv1 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by linenum,docentry,itemcode,dscription,unitmsr order by linenum"
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

        Dim CMD As New OleDb.OleDbCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()

        Call flxchead()
        'Call flxchead2()
        Try
            ''Dim DR As SqlDataReader
            Dim DR As OleDb.OleDbDataReader
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
                    msql = "select    t0.linenum,t0.docentry,t0.itemcode,t0.dscription as itemname,it.u_style as style,it.U_Size as size, sum(quantity) quantity from inv1 t0 " & vbCrLf _
                    & " left join oitm it on it.itemcode=t0.itemcode " & vbCrLf _
                     & "where t0.DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and t0.treetype<>'I'  group by   t0.linenum,t0.docentry,t0.itemcode,t0.dscription,it.u_style,it.U_Size,t0.unitmsr order by  t0.linenum,t0.dscription"
                End If
            Else
                If cmbtype.Text = "DATE ORDER" Then
                    msql = "select  docentry,itemcode,U_CatalogCode as itemname,u_style as style,U_Size as size,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from dln1 " & vbCrLf _
                    & "where DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by docentry,itemcode,U_CatalogCode,u_style,U_Size,unitmsr order by u_catalogcode"
                Else
                    msql = "select  docentry,itemcode,U_CatalogCode as itemname,u_style as style,U_Size as size,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from inv1 " & vbCrLf _
                     & "where DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by docentry,itemcode,U_CatalogCode,u_style,U_Size,unitmsr order by u_catalogcode"
                End If
            End If

        Else

            If chkprod.Checked = True Then
                If cmbtype.Text = "DATE ORDER" Then
                    msql = "select  t0.linenum,t0.docentry,t0.itemcode,t0.dscription as itemname,it.u_style as style,it.U_Size as size, sum(quantity)  quantity from dln1 t0 " & vbCrLf _
                         & " left join oitm it on it.itemcode=t0.itemcode " & vbCrLf _
                    & "where t0.DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and t0.treetype<>'I'  group by   t0.linenum,t0.docentry,t0.itemcode,t0.dscription,it.u_style,it.U_Size,t0.unitmsr order by  t0.linenum, t0.dscription"
                Else
                    msql = "select    t0.linenum,t0.docentry,t0.itemcode,t0.dscription as itemname,it.u_style as style,it.U_Size as size, sum(quantity) quantity from inv1 t0 " & vbCrLf _
                    & " left join oitm it on it.itemcode=t0.itemcode " & vbCrLf _
                     & "where t0.DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and t0.treetype<>'I'  group by   t0.linenum,t0.docentry,t0.itemcode,t0.dscription,it.u_style,it.U_Size,t0.unitmsr order by  t0.linenum,t0.dscription"
                End If
            Else
                If cmbtype.Text = "DATE ORDER" Then
                    msql = "select    linenum,docentry,itemcode,U_CatalogCode as itemname,u_style as style,U_Size as size,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from dln1 " & vbCrLf _
                    & "where DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by linenum,docentry,itemcode,U_CatalogCode,u_style,U_Size,unitmsr order by  linenum,u_catalogcode"
                Else
                    msql = "select  linenum,docentry,itemcode,U_CatalogCode as itemname,u_style as style,U_Size as size,case when unitmsr='MTRS' then sum(u_noofpiece) else sum(quantity) end quantity from inv1 " & vbCrLf _
                     & "where DocEntry=" & Microsoft.VisualBasic.Val(lbldocentry.Text) & " and treetype<>'I'  group by linenum,docentry,itemcode,U_CatalogCode,u_style,U_Size,unitmsr order by linenum,u_catalogcode"
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

        Dim CMD As New OleDb.OleDbCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()

        'Call flxchead()
        Call flxchead2()
        Try
            ''Dim DR As SqlDataReader
            Dim DR As OleDb.OleDbDataReader
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

        Dim CMD As New OleDb.OleDbCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()

        Call flxphead2()
        'Call flxchead2()
        Try
            'loadcombow("inv7", "packagenum", cmbboxno, "docentry", Microsoft.VisualBasic.Val(lbldocentry.Text))
            'loadcombow("inv7", "packagetyp", cmbboxtype, "docentry", Microsoft.VisualBasic.Val(lbldocentry.Text))
            'loadcombow("inv7", "weightunit", cmbwgt, "docentry", Microsoft.VisualBasic.Val(lbldocentry.Text))

            ''Dim DR As SqlDataReader
            Dim DR As OleDb.OleDbDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                mktru = True
                cmdupdt.Enabled = False
                With flxp
                    While DR.Read
                        .Rows = .Rows + 1
                        .Row = .Rows - 1

                        .set_TextMatrix(.Row, 0, DR.Item("packagenum") & vbNullString)
                        .set_TextMatrix(.Row, 1, DR.Item("itemcode") & vbNullString)
                        .set_TextMatrix(.Row, 2, Microsoft.VisualBasic.Format(DR.Item("quantity"), "#######0.00"))
                        .set_TextMatrix(.Row, 3, DR.Item("docentry"))


                        '.set_TextMatrix(.Row, 1, DR.Item("itemcode") & vbNullString)
                        .set_TextMatrix(.Row, 4, DR.Item("Catalogname"))
                        .set_TextMatrix(.Row, 5, DR.Item("u_Style") & vbNullString)
                        .set_TextMatrix(.Row, 6, DR.Item("u_Size") & vbNullString)
                        '.set_TextMatrix(.Row, 5, DR.Item("quantity"))


                        'lbldocentry.Text = DR.Item("docentry")
                        'lbldate.Text = DR.Item("docdate")
                        'lblparty.Text = DR.Item("cardname") & vbNullString
                        'lblamt.Text = DR.Item("doctotal") & vbNullString
                    End While
                    .Rows = .Rows + 1
                    .Row = .Rows - 1
                End With
            Else
                mktru = False
                loadcombo("opkg", "pkgtype", cmbboxtype, "pkgtype")
                loadcombo("owgt", "unitname", cmbwgt, "unitname")
                cmdupdt.Enabled = True

            End If
            DR.Close()
        Catch ex As Exception
            mktru = False
            cmdupdt.Enabled = True
            MsgBox(ex.Message)
        End Try
        CMD.Dispose()
        Call flxptot()
    End Sub
    Private Sub loadhead()
        cmbboxno.Items.Clear()
        cmbboxtype.Items.Clear()
        cmbwgt.Items.Clear()

        If cmbtype.Text = "DATE ORDER" Then
            msql = "select  packagenum,packagetyp,weightunit,updtdate from rdln7 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text)
        Else
            msql = "select  packagenum,packagetyp,weightunit,updtdate from rinv7 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text)
        End If
        'msql = "select  packagenum,packagetyp,weightunit from rinv7 where docentry=" & Microsoft.VisualBasic.Val(lbldocentry.Text)

        Dim CMD1 As New OleDb.OleDbCommand(msql, con)
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
            Dim DR1 As OleDb.OleDbDataReader
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
        If flxc.Col = 3 Then
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
            If Len(Trim(flxc.get_TextMatrix(k, 1))) > 0 Then
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

                    Dim cmd3 As New OleDb.OleDbCommand(msql2, con)
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

                    Dim cmd2 As New OleDb.OleDbCommand(msql, con)
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
        cmbyear.Text = mpostperiod
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
        If Trim(cmbtype.Text) = "DATE ORDER" Then
            msql = "delete from rdln7 where docentry=" & Val(lbldocentry.Text)
        Else
            msql = "delete from rINV7 where docentry=" & Val(lbldocentry.Text)
        End If
        'msql = "delete from rINV7 where docentry=" & Val(lbldocentry.Text)
        Dim cmd1 As New OleDb.OleDbCommand(msql, con)
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
        Dim cmd2 As New OleDb.OleDbCommand(msql, con)
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

    End Sub

    Private Sub flxp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxp.Enter

    End Sub
    Private Sub crystal()
       
        Me.Cursor = Cursors.WaitCursor
        'Dim cryRpt As New ReportDocument()
        Dim cryrpt As New ReportDocument()

       
        'cryRpt.Load(Trim(mreppath) & "Company Analysis Report.rpt")
        If Trim(cmbtype.Text) = "DATE ORDER" Then
            If MsgBox("Single Sheet", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
                cryRpt.Load(Trim(mreppath) & "Packaging Single Sheetdel.rpt")
            Else
                cryRpt.Load(Trim(mreppath) & "Packaging Multi Sheetdel.rpt")
            End If
        Else

            If MsgBox("Single Sheet", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
                cryRpt.Load(Trim(mreppath) & "Packaging Single Sheet.rpt")
            Else
                cryRpt.Load(Trim(mreppath) & "Packaging Multi Sheet.rpt")
            End If
        End If

        'cryRpt.Load("e:\kalai\Area Wise Summary Report.rpt")
        'report.SetDatabaseLogon("root", "", "localhost", "aerospace")
        'cryRpt.SetDatabaseLogon("sa", "SEsA@536", "192.168.0.5", dbnam)
        CrystalReportLogOn(cryRpt, Trim(mkserver), dbnam, Trim(dbuser), Trim(mkpwd))

        'CrystalReportLogOn(cryRpt, "192.168.0.5", dbnam, "sa", "iTTsA@536")
        cryRpt.SetParameterValue("Dockey@", Val(lbldocentry.Text))
        'setlogon(cryRpt)
        ''cryRpt.SetParameterValue("AREA@SELECT DISTINCT U_AreaCode From OCRD Order BY U_AreaCode", cmbagent.Text)
        'cryRpt.SetParameterValue("FromDate", Convert.ToDateTime(mskdatefr.Text))
        '' Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy/MM/dd"))
        'cryRpt.SetParameterValue("ToDate", Convert.ToDateTime(mskdateto.Text))
        'cryRpt.SetParameterValue("DocKey", Val(lbldocentry.Text))

        'Dim MyReport As ReportClass = DirectCast(ReportViewer.ReportSource, ReportClass)
        'MyReport.PrinterOptions.PrinterName = "name of default or desired printer here"
        'MyReport.PrintToPrinter()
        'Me.view1.printerOptions.Printername = "Bullzip PDF Printer"
        Me.view1.ReportSource = cryRpt
        Me.view1.PrintReport()
        'view1.PrintToPrinter(1, False, 1, 1)
        'Me.View1.ReportSource = cryRpt
        Me.view1.Refresh()
        cryRpt.Dispose()
        Me.Cursor = Cursors.Default





        'Inner Packing Slip RR.rpt


    End Sub

    Private Sub cmdprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdprint.Click
        'rhl
        If dbnam = "RHLLIVE" Then
            Call crystal()
        Else

            'rr
            Call crystalrr()
        End If





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

        Me.Cursor = Cursors.WaitCursor
        Dim cryRpt As New ReportDocument()


        'cryRpt.Load(Trim(mreppath) & "Company Analysis Report.rpt")
        If cmbtype.Text = "DATE ORDER" Then
            If MsgBox("Single Sheet", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
                cryRpt.Load(Trim(mreppath) & "Inner Packing Slip RRdel.rpt")
            Else
                cryRpt.Load(Trim(mreppath) & "Inner Packing Slip RRdel2.rpt")
            End If
        Else

            If MsgBox("Single Sheet", MsgBoxStyle.Critical + vbYesNo) = MsgBoxResult.Yes Then
                cryRpt.Load(Trim(mreppath) & "Inner Packing Slip RR.rpt")
            Else
                cryRpt.Load(Trim(mreppath) & "Inner Packing Slip RR2.rpt")
            End If
        End If

        'cryRpt.Load("e:\kalai\Area Wise Summary Report.rpt")
        'report.SetDatabaseLogon("root", "", "localhost", "aerospace")
        'cryRpt.SetDatabaseLogon("sa", "SEsA@536", "192.168.0.5", dbnam)

        CrystalReportLogOn(cryRpt, Trim(mkserver), dbnam, Trim(dbuser), Trim(mkpwd))
        'CrystalReportLogOn(cryRpt, "192.168.0.5", dbnam, "sa", "iTTsA@536")

        'MsgBox(cryRpt.DataDefinition.ParameterFields(0).ParameterFieldName)
        ''intCounter = cryRpt.DataDefinition.ParameterFields.Count
        ''If intCounter = 1 Then
        '' If InStr(cryRpt.DataDefinition.ParameterFields(0).ParameterFieldName, ".", CompareMethod.Text) > 0 Then
        '' intCounter = 0
        '' End If
        '' End If



        'cryRpt.SetParameterValue(cryRpt.DataDefinition.ParameterFields(0).ParameterFieldName, Val(lbldocentry.Text))

        cryRpt.SetParameterValue("Dockey@", Val(lbldocentry.Text))
        'setlogon(cryRpt)
        ''cryRpt.SetParameterValue("AREA@SELECT DISTINCT U_AreaCode From OCRD Order BY U_AreaCode", cmbagent.Text)
        'cryRpt.SetParameterValue("FromDate", Convert.ToDateTime(mskdatefr.Text))
        '' Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy/MM/dd"))
        'cryRpt.SetParameterValue("ToDate", Convert.ToDateTime(mskdateto.Text))
        'cryRpt.SetParameterValue("DocKey", Val(lbldocentry.Text))

        'Dim MyReport As ReportClass = DirectCast(ReportViewer.ReportSource, ReportClass)
        'MyReport.PrinterOptions.PrinterName = "name of default or desired printer here"
        'MyReport.PrintToPrinter()
        'Me.view1.printerOptions.Printername = "Bullzip PDF Printer"
        Me.view1.ReportSource = cryRpt
        Me.view1.PrintReport()
        'view1.PrintToPrinter(1, False, 1, 1)
        'Me.View1.ReportSource = cryRpt
        Me.view1.Refresh()
        cryRpt.Dispose()
        Me.Cursor = Cursors.Default

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

        Me.Cursor = Cursors.WaitCursor
        Dim cryRpt As New ReportDocument()


        'cryRpt.Load(Trim(mreppath) & "Company Analysis Report.rpt")
     
        cryRpt.Load(Trim(mreppath) & "GST PREINVOICE cor.rpt")
        'cryRpt.Load("e:\kalai\Area Wise Summary Report.rpt")
        'report.SetDatabaseLogon("root", "", "localhost", "aerospace")
        'cryRpt.SetDatabaseLogon("sa", "SEsA@536", "192.168.0.5", dbnam)

        CrystalReportLogOn(cryRpt, Trim(mkserver), dbnam, Trim(dbuser), Trim(mkpwd))
        'CrystalReportLogOn(cryRpt, "192.168.0.5", dbnam, "sa", "iTTsA@536")

        'MsgBox(cryRpt.DataDefinition.ParameterFields(0).ParameterFieldName)
        ''intCounter = cryRpt.DataDefinition.ParameterFields.Count
        ''If intCounter = 1 Then
        '' If InStr(cryRpt.DataDefinition.ParameterFields(0).ParameterFieldName, ".", CompareMethod.Text) > 0 Then
        '' intCounter = 0
        '' End If
        '' End If



        'cryRpt.SetParameterValue(cryRpt.DataDefinition.ParameterFields(0).ParameterFieldName, Val(lbldocentry.Text))

        cryRpt.SetParameterValue("Dockey@", Val(lbldocentry.Text))
        'setlogon(cryRpt)
        ''cryRpt.SetParameterValue("AREA@SELECT DISTINCT U_AreaCode From OCRD Order BY U_AreaCode", cmbagent.Text)
        'cryRpt.SetParameterValue("FromDate", Convert.ToDateTime(mskdatefr.Text))
        '' Microsoft.VisualBasic.Format(CDate(mskdatefr.Text), "yyyy/MM/dd"))
        'cryRpt.SetParameterValue("ToDate", Convert.ToDateTime(mskdateto.Text))
        'cryRpt.SetParameterValue("DocKey", Val(lbldocentry.Text))

        'Dim MyReport As ReportClass = DirectCast(ReportViewer.ReportSource, ReportClass)
        'MyReport.PrinterOptions.PrinterName = "name of default or desired printer here"
        'MyReport.PrintToPrinter()
        'Me.view1.printerOptions.Printername = "Bullzip PDF Printer"
        Me.view1.ReportSource = cryRpt
        Me.view1.PrintReport()
        'view1.PrintToPrinter(1, False, 1, 1)
        'Me.View1.ReportSource = cryRpt
        Me.view1.Refresh()
        cryRpt.Dispose()
        Me.Cursor = Cursors.Default

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
            msql5 = "select b.DocNum,b.DocEntry,b.DocDate,b.CardCode,b.CardName,b.U_Bundle,c.PackageNum,c.noqty,'D-'+rtrim(convert(nvarchar(100),d.U_Remarks))+' '+LTRIM(convert(nvarchar(20),b.docnum)) billno,isnull(U_RefNo,'') refno,ltrim(CONVERT(nvarchar(7),c.packagenum))+'/'+ltrim(CONVERT(nvarchar(7),b.u_bundle)) as packno,isnull(b.u_transport,'') transport  from Odln b " & vbCrLf _
                   & " left join (select DocEntry,packagenum,SUM(Quantity) noqty from rdln8 with (nolock) group by DocEntry,packagenum) c  on c.DocEntry=b.DocEntry " & vbCrLf _
                   & " left join [@INCM_BND1] d on d.U_Name=b.u_brand where b.docentry=" & Val(lbldocentry.Text)
        Else

            msql5 = "select b.DocNum,b.DocEntry,b.DocDate,b.CardCode,b.CardName,b.U_Bundle,c.PackageNum,c.noqty,'I-'+rtrim(convert(nvarchar(100),d.U_Remarks))+' '+LTRIM(convert(nvarchar(20),b.docnum)) billno, j.docnums refno,ltrim(CONVERT(nvarchar(7),c.packagenum))+'/'+ltrim(CONVERT(nvarchar(7),b.u_bundle)) as packno,isnull(b.u_transport,'') transport  from Oinv b " & vbCrLf _
                   & " left join (select DocEntry,packagenum,SUM(Quantity) noqty from rinv8 with (nolock) group by DocEntry,packagenum) c  on c.DocEntry=b.DocEntry " & vbCrLf _
                   & " left join (select distinct (convert(nvarchar(max),STUFF((select distinct ','+ convert(nvarchar(max),(t2.DocNum)) from oinv t2  With (Nolock) " & vbCrLf _
                   & " WHERE  t2.CardCode= t1.CardCode AND t1.PIndicator = t2.PIndicator and " & vbCrLf _
                   & " CASE when CONVERT(nvarchar(max),isnull(t2.U_RefNo,''))  = '' then CONVERT(nvarchar(max),isnull(t2.DocNum,''))  else CONVERT(nvarchar(max),isnull(t2.U_RefNo,'')) end  = " & vbCrLf _
                   & " CASE when CONVERT(nvarchar(max),isnull(t1.U_RefNo,''))  = '' then CONVERT(nvarchar(max),isnull(t1.DocNum,''))  else CONVERT(nvarchar(max),isnull(t1.U_RefNo,'')) end  " & vbCrLf _
                   & " for XML path('')), 1, 1,''))) as docnums,T1.DocEntry,CASE WHEN CONVERT(NVARCHAR(MAX), isnull(U_REFNO ,''))= '' 	THEN CONVERT(NVARCHAR(MAX), isnull(DOCNUM ,'')) 	ELSE CONVERT(NVARCHAR(MAX), isnull(U_REFNO ,'')) END REFNO 	from oinv t1  With (Nolock) ) j on j.docentry=b.docentry " & vbCrLf _
                   & " left join [@INCM_BND1] d on d.U_Name=b.u_brand where b.docentry=" & Val(lbldocentry.Text)
        End If


        Dim CMD3 As New OleDb.OleDbCommand(msql5, con)
        Dim DR3 As OleDb.OleDbDataReader
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
                PrintLine(1, TAB(0), "^FO140,285^A0N,30,30^CI13^FR^FDTrans :" & DR3.Item("transport") & "^FS;")
                PrintLine(1, TAB(0), "^PQ1,0,0,N")
                PrintLine(1, TAB(0), "^XZ")

            End While
        End If
        FileClose(1)

        'If chkdirprn.Checked = False Then
        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
            Shell("cmd.exe /c" & "type " & mdir & " > lpt" & Trim(txtport.Text))
            'Shell("print /d:LPT" & Trim(txtport.Text) & mdir, vbNormalFocus)
        End If

        DR3.Close()
        CMD3.Dispose()
    End Sub
End Class
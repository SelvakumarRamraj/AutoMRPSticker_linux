Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource

Public Class Frminvprn
    Dim msql, msql2, msql3, msql4 As String
    Dim mdocno As Long
    Dim j, i, msel, n As Int32
    Dim mktru As Boolean
    Private Sub Frminvprn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        loadcombo("oinv", "u_brand", cmbbrand, "u_brand")
    End Sub

    


    Private Sub loaddata()
        'If optinv.Checked = True Then
        If Len(Trim(cmbbrand.Text)) > 0 Then
            msql = "select docnum,docdate,docentry,u_brand,cardcode,cardname,doctotal,printed from oinv with (nolock) where docdate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and docdate<='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and u_brand='" & cmbbrand.Text & "'"
        Else
            msql = "select docnum,docdate,docentry,u_brand,cardcode,cardname,doctotal,printed from oinv with (nolock) where docdate>='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "' and docdate<='" & Format(CDate(mskdatefr.Text), "yyyy-MM-dd") & "'"
        End If


        'End If

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim CMD As New OleDb.OleDbCommand(msql, con)
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()
        dg.Rows.Clear()
        Try
            ''Dim DR As SqlDataReader
            Dim DR As OleDb.OleDbDataReader
            DR = CMD.ExecuteReader
            'DR.Read()
            If DR.HasRows = True Then

                While DR.Read
                    n = dg.Rows.Add
                    dg.Rows(n).Cells(1).Value = DR.Item("docnum")
                    dg.Rows(n).Cells(2).Value = DR.Item("docdate")
                    dg.Rows(n).Cells(3).Value = DR.Item("docentry")
                    dg.Rows(n).Cells(4).Value = DR.Item("cardcode")
                    dg.Rows(n).Cells(5).Value = DR.Item("cardname")
                    dg.Rows(n).Cells(6).Value = DR.Item("doctotal")
                    dg.Rows(n).Cells(7).Value = DR.Item("u_brand")
                    dg.Rows(n).Cells(8).Value = DR.Item("printed")
                End While
            End If

        Catch ex As Exception

        End Try


    End Sub

    Private Sub txtno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtno.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim temp As Integer = 0
            For i As Integer = 0 To dg.RowCount - 1
                'For j As Integer = 0 To gv.ColumnCount - 1
                If dg.Rows(i).Cells(1).Value = txtno.Text Then
                    'MsgBox("Item found")
                    dg.Rows(i).Cells(0).Value = "True"

                    dg.Sort(dg.Columns(0), System.ComponentModel.ListSortDirection.Descending)
                    'dg.Item(0, i).Value = "True"
                    'txtno.Text = ""
                    temp = 1
                End If
                'Next
            Next
            If temp = 0 Then
                MsgBox("Item not found")
            End If
        End If
    End Sub

    Private Sub txtno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtno.TextChanged

    End Sub

    Private Sub cmdDisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDisp.Click
        loaddata()
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdprint.Click
        If optinv.Checked = True Then
            For i As Integer = 0 To dg.RowCount - 1
                If dg.Item(0, i).Value = "True" Then
                    crystalinv(Val(dg.Rows(i).Cells(3).Value), minvrepname)
                End If
            Next
        End If
        If optdel.Checked = True Then
            For i As Integer = 0 To dg.RowCount - 1
                If dg.Item(0, i).Value = "True" Then
                    crystalinv(Val(dg.Rows(i).Cells(3).Value), mdelrepname)
                End If
            Next
        End If

        If optLorry.Checked = True Then
            For i As Integer = 0 To dg.RowCount - 1
                If dg.Item(0, i).Value = "True" Then
                    crystalinv(Val(dg.Rows(i).Cells(3).Value), mloryrepname)
                End If
            Next
        End If

        If optforward.Checked = True Then
            For i As Integer = 0 To dg.RowCount - 1
                If dg.Item(0, i).Value = "True" Then
                    crystalinv(Val(dg.Rows(i).Cells(3).Value), mfwrepname)
                End If
            Next
        End If



    End Sub

    Private Sub cmdsaverep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsaverep.Click
        If Len(Trim(txtreppath.Text)) > 0 Then
            loadrptdb3(Trim(txtrepcode.Text), Trim(txtreppath.Text))
        Else

            Call loadrptdb3(Trim(txtrepcode.Text), Trim(mreppath))

        End If

    End Sub

    Private Sub butsel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butsel.Click
        txtfile.Text = ""
        fd.Title = "Select your Crystal Rport File."
        fd.InitialDirectory = "C:\"
        fd.Filter = "Crystl Rpt Files|*.rpt;"
        fd.RestoreDirectory = False

        If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtfile.Text = fd.FileName
        End If

        msql = "update rdoc set template=@repdoc where doccode='" & Trim(txtrepcode.Text) & "'"
        'insert into reportmaster(reportname,repdoc) values(@repname,@repdoc)"
        'If SaveReportInDB2(txtfile.Text, "insert into reportmaster(reportname,repdoc) values(@repname,@repdoc)", Trim(TextBox4.Text)) = True Then
        If SaveReportInDB2(txtfile.Text, msql, "") = True Then
            MsgBox("saved!")
        Else
            MsgBox("Not Saved!")
        End If

    End Sub

    Private Sub crystalinv(ByVal mdocentry As Integer, ByVal mrepname As String)

        Me.Cursor = Cursors.WaitCursor
        Dim cryRpt As New ReportDocument()
        'cryRpt.Load(Trim(mreppath) & "GST PREINVOICE cor.rpt")
        cryRpt.Load(Trim(mreppath) & Trim(mrepname))
        CrystalReportLogOn(cryRpt, Trim(mkserver), dbnam, Trim(dbuser), Trim(mkpwd))
        cryRpt.SetParameterValue("Dockey@", mdocentry)

        Me.view1.ReportSource = cryRpt
        Me.view1.PrintReport()
        'view1.PrintToPrinter(1, False, 1, 1)
        'Me.View1.ReportSource = cryRpt
        Me.view1.Refresh()
        cryRpt.Dispose()
        Me.Cursor = Cursors.Default

    End Sub


    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick

    End Sub
End Class
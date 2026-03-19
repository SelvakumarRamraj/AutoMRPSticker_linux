Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Public Class Frmperfdataentry
    Dim i, j, msel, o_id, n As Integer
    Dim MSQL, qry, dqry1, dqry2, merr As String
    Dim Flag As Boolean
    Dim trans As SqlTransaction
    Dim l_strSerachData As String

    Private Sub Frmperfdataentry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        Dg.Width = My.Computer.Screen.Bounds.Width - 10
        'cmbstyle.Items.Add("GENERAL")
        'cmbstyle.Items.Add("FULL SLEEVE")
        'cmbstyle.Items.Add("HALF SLEEVE")
        'cmbstyle.Items.Add("FRENCH DRAW")
        cmbcol.Items.Add("White")
        cmbcol.Items.Add("Color")

        cmbprocess.Items.Add("CUTTING")
        cmbprocess.Items.Add("FUSHING")
        cmbprocess.Items.Add("EMB")
        cmbprocess.Items.Add("STITCHING")
        cmbprocess.Items.Add("CHECKING")
        cmbprocess.Items.Add("IRONING")

        'cmbbrand.Items.Add("GENERAL BRAND")
        'cmbbrand.Items.Add("COOL COTTON")
        'cmbbrand.Items.Add("SPECIAL")

        Call loadbrand()


        For i = 1 To 30
            cmbline.Items.Add(Str(i))
        Next
        cmbline.Items.Add("100")
        cmbline.Items.Add("200")
        cmbline.Items.Add("GENERAL")

        Call cancel()
        Call loadstyle()
        Call loadhead()
    End Sub

    Private Sub loadstyle()
        Dim query = "Select style From " & Trim(mcostdbnam) & ".dbo.stylemaster"
        Dim dr As SqlDataReader
        dr = getDataReader(query)
        'dr.Read()
        cmbstyle.Items.Clear()
        If dr.HasRows = True Then
            While dr.Read
                cmbstyle.Items.Add(dr.Item("style"))
            End While
        End If

        'o_id = dr("operid")
        'dr.Close()

        'Return o_id
    End Sub

    Private Sub loadbrand()
        Dim query = "Select brand From " & Trim(mcostdbnam) & ".dbo.incentivemaster group by brand"
        Dim dr As SqlDataReader
        dr = getDataReader(query)
        'dr.Read()
        cmbbrand.Items.Clear()
        If dr.HasRows = True Then
            While dr.Read
                cmbbrand.Items.Add(dr.Item("brand"))
            End While
        End If

        'o_id = dr("operid")
        dr.Close()

        'Return o_id
    End Sub

    'Private Sub gridhead()

    '    dv.ColumnCount = 16
    '    dv.Columns(0).Name = "Sno"
    '    dv.Columns(1).Name = "Emp.ID"
    '    dv.Columns(2).Name = "Emp.Name"
    '    dv.Columns(3).Name = "Operation Name"
    '    dv.Columns(4).Name = "SAM"
    '    dv.Columns(5).Name = "Tot.Production"
    '    dv.Columns(6).Name = "Tot.Min.Wrked"
    '    dv.Columns(7).Name = "Wait.Time"
    '    dv.Columns(8).Name = "MAC.Dwn.Time"
    '    dv.Columns(9).Name = "Emp.Dwn Time"
    '    dv.Columns(10).Name = "Others"
    '    dv.Columns(11).Name = "On.Std Time"
    '    dv.Columns(12).Name = "SAM.Prod."
    '    dv.Columns(13).Name = "Performance"
    '    dv.Columns(14).Name = "Efficiency"
    '    dv.Columns(15).Name = "Utilisation"

    '    'dv.Columns(8).Name = "DeliveryNo"

    '    dv.Columns(0).Width = 100
    '    dv.Columns(1).Width = 250
    '    dv.Columns(2).Width = 75
    '    dv.Columns(3).Width = 75
    '    dv.Columns(4).Width = 100
    '    dv.Columns(5).Width = 100
    '    dv.Columns(6).Width = 100
    '    dv.Columns(7).Width = 100
    '    dv.Columns(8).Width = 100
    '    dv.Columns(9).Width = 100
    '    dv.Columns(10).Width = 100
    '    dv.Columns(11).Width = 100
    '    dv.Columns(12).Width = 100
    '    dv.Columns(13).Width = 100
    '    dv.Columns(14).Width = 100
    '    dv.Columns(15).Width = 100


    '    Dv.ColumnHeadersDefaultCellStyle.Font = New Font(Dv.Font, FontStyle.Bold)
    '    Dv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    'End Sub

    Private Sub butadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butadd.Click
        mstyl = Regex.Replace(cmbbrand.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "").Replace("BRAND", " SHIRT").Replace("ITEM", " SHIRT"), "\d+", "").Trim()
        If msel = 1 Or msel = 2 Then
            Dg.Rows.Add()
            mproces = cmbprocess.Text
            If (Dg.RowCount - 1) > 0 Then
                'If dg.Rows(dg.RowCount - 1).Cells(0).Value = 0 Then
                Dg.Rows(Dg.RowCount - 1).Cells(0).Value = Dg.Rows(Dg.RowCount - 2).Cells(0).Value + 1
            Else
                'dg.Rows(dg.RowCount - 1).Cells(19).Value = txtwrkhour.Text
                Dg.Rows(Dg.RowCount - 1).Cells(0).Value = 1

            End If
        End If
    End Sub

    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dg.CellContentClick
        If e.ColumnIndex = 2 Then
            Dim OBJ As New FrmempCFL
            OBJ.ShowDialog()
            If Len(Trim(mempno)) > 0 Then
                Dg.Rows(e.RowIndex).Cells(1).Value = mempid
                Dg.Rows(e.RowIndex).Cells(2).Value = mempno
                Dg.Rows(e.RowIndex).Cells(3).Value = mempname
                Dg.Rows(e.RowIndex).Cells(24).Value = mempsalary
                Dg.Rows(e.RowIndex).Cells(26).Value = mjbgrade
                Dg.CurrentCell = Dg.Rows(e.RowIndex).Cells(4)
                Dg.BeginEdit(False)
            End If
            OBJ.Close()
        End If
        'mstyl = Regex.Replace(cmbbrand.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", ""), "\d+", "").Trim()
        mstyl = Regex.Replace(cmbbrand.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "").Replace("BRAND", " SHIRT").Replace("ITEM", " SHIRT"), "\d+", "").Trim()
        If e.ColumnIndex = 4 Then
            Dim OBJ2 As New FrmsamCFL
            OBJ2.ShowDialog()
            If Len(Trim(mopername)) > 0 Then
                Dg.Rows(e.RowIndex).Cells(4).Value = mopername
                Dg.Rows(e.RowIndex).Cells(5).Value = msam
                Dg.Rows(e.RowIndex).Cells(20).Value = mempgrade
                Dg.Rows(e.RowIndex).Cells(26).Value = mempgrade
                Dg.CurrentCell = Dg.Rows(e.RowIndex).Cells(6)
                Dg.BeginEdit(False)
            End If
            OBJ2.Close()
        End If

    End Sub

    Private Sub dg_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dg.CellContentDoubleClick
        'If e.ColumnIndex = 2 Then
        '    Dim OBJ As New FrmempCFL
        '    OBJ.ShowDialog()
        '    If Len(Trim(mempno)) > 0 Then
        '        dg.Rows(e.RowIndex).Cells(1).Value = mempid
        '        dg.Rows(e.RowIndex).Cells(2).Value = mempno
        '        dg.Rows(e.RowIndex).Cells(3).Value = mempname
        '        dg.Rows(e.RowIndex).Cells(24).Value = mempsalary

        '    End If
        'End If
    End Sub

    Private Sub dg_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dg.CellEnter
        If e.ColumnIndex = 2 Then
            Dim OBJ As New FrmempCFL
            OBJ.ShowDialog()
            If Len(Trim(mempno)) > 0 Then
                Dg.Rows(e.RowIndex).Cells(1).Value = mempid
                Dg.Rows(e.RowIndex).Cells(2).Value = mempno
                Dg.Rows(e.RowIndex).Cells(3).Value = mempname
                Dg.Rows(e.RowIndex).Cells(24).Value = mempsalary
                Dg.Rows(e.RowIndex).Cells(26).Value = mjbgrade
                'dg.CurrentCell = dg.Rows(e.RowIndex).Cells(4)
                'dg.BeginEdit(False)
            End If
        End If
        'mstyl = Regex.Replace(cmbbrand.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", ""), "\d+", "").Trim()
        mstyl = Regex.Replace(cmbbrand.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "").Replace("BRAND", " SHIRT").Replace("ITEM", " SHIRT"), "\d+", "").Trim()
        If e.ColumnIndex = 4 Then
            Dim OBJ2 As New FrmsamCFL
            OBJ2.ShowDialog()
            If Len(Trim(mopername)) > 0 Then
                Dg.Rows(e.RowIndex).Cells(4).Value = mopername
                Dg.Rows(e.RowIndex).Cells(5).Value = msam
                Dg.Rows(e.RowIndex).Cells(20).Value = mempgrade
                Dg.Rows(e.RowIndex).Cells(26).Value = mempgrade
                'dg.Rows(e.RowIndex).Cells(3).Value = mempname
                'dg.CurrentCell = dg.Rows(e.RowIndex).Cells(6)
                'dg.BeginEdit(False)
            End If
            'OBJ2.Close()
        End If
        If e.ColumnIndex = 25 Then
            Dg.BeginEdit(True)
        End If

    End Sub

    Private Sub dg_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Dg.CellMouseDoubleClick
        If e.ColumnIndex = 2 Then
            Dim OBJ As New FrmempCFL
            OBJ.ShowDialog()
            If Len(Trim(mempno)) > 0 Then
                Dg.Rows(e.RowIndex).Cells(1).Value = mempid
                Dg.Rows(e.RowIndex).Cells(2).Value = mempno
                Dg.Rows(e.RowIndex).Cells(3).Value = mempname
                Dg.Rows(e.RowIndex).Cells(24).Value = mempsalary
                Dg.Rows(e.RowIndex).Cells(26).Value = mjbgrade
                Dg.CurrentCell = Dg.Rows(e.RowIndex).Cells(4)
                Dg.BeginEdit(False)
            End If
        End If
        'mstyl = Regex.Replace(cmbbrand.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", ""), "\d+", "").Trim()
        mstyl = Regex.Replace(cmbbrand.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "").Replace("BRAND", " SHIRT").Replace("ITEM", " SHIRT"), "\d+", "").Trim()
        If e.ColumnIndex = 4 Then
            Dim OBJ2 As New FrmsamCFL
            OBJ2.ShowDialog()
            If Len(Trim(mopername)) > 0 Then
                Dg.Rows(e.RowIndex).Cells(4).Value = mopername
                Dg.Rows(e.RowIndex).Cells(5).Value = msam
                Dg.Rows(e.RowIndex).Cells(20).Value = mempgrade
                Dg.Rows(e.RowIndex).Cells(26).Value = mempgrade
                'dg.Rows(e.RowIndex).Cells(3).Value = mempname
                Dg.CurrentCell = Dg.Rows(e.RowIndex).Cells(6)
                Dg.BeginEdit(False)
            End If
            OBJ2.Close()
        End If

    End Sub

    Private Sub dg_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dg.CellValueChanged
        If Dg.Rows.Count > 0 Then
            Dg.Rows(e.RowIndex).Cells(12).Value = Val(Dg.Rows(e.RowIndex).Cells(8).Value) + Val(Dg.Rows(e.RowIndex).Cells(9).Value) + Val(Dg.Rows(e.RowIndex).Cells(10).Value) + Val(Dg.Rows(e.RowIndex).Cells(11).Value)
            Dg.Rows(e.RowIndex).Cells(13).Value = Val(Dg.Rows(e.RowIndex).Cells(7).Value) - Val(Dg.Rows(e.RowIndex).Cells(12).Value)
            Dg.Rows(e.RowIndex).Cells(14).Value = Val(Dg.Rows(e.RowIndex).Cells(5).Value) * Val(Dg.Rows(e.RowIndex).Cells(6).Value)
            If Val(Dg.Rows(e.RowIndex).Cells(14).Value) > 0 Then
                Dg.Rows(e.RowIndex).Cells(15).Value = Format((Val(Dg.Rows(e.RowIndex).Cells(14).Value) / Val(Dg.Rows(e.RowIndex).Cells(13).Value)) * 100, "###0.00")
            End If
            If Val(Dg.Rows(e.RowIndex).Cells(14).Value) > 0 Then
                Dg.Rows(e.RowIndex).Cells(16).Value = Format((Val(Dg.Rows(e.RowIndex).Cells(14).Value) / Val(Dg.Rows(e.RowIndex).Cells(7).Value)) * 100, "###0.00")
            End If


        End If

    End Sub

    Private Sub dg_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles Dg.DataError
        If (e.Context _
                 = (DataGridViewDataErrorContexts.Formatting Or DataGridViewDataErrorContexts.PreferredSize)) Then
            e.ThrowException = False
        End If
    End Sub

    Private Sub Dg_GotFocus(sender As Object, e As System.EventArgs) Handles Dg.GotFocus
        mkdate = Format(CDate(mskdate.Text), "yyyy-MM-dd")
    End Sub

    Private Sub dg_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dg.KeyDown
        If e.KeyCode = Keys.F2 Then
            If msel = 1 Or msel = 2 Then
                n = Dg.Rows.Add()
                mproces = cmbprocess.Text
                If (Dg.RowCount - 1) > 0 Then
                    'If dg.Rows(dg.RowCount - 1).Cells(0).Value = 0 Then
                    Dg.Rows(Dg.RowCount - 1).Cells(0).Value = Dg.Rows(Dg.RowCount - 2).Cells(0).Value + 1
                    'dg.CurrentCell = dg.Rows(dg.CurrentRow.Index + 1).Cells(1)
                    ' dg.CurrentCell = dg.Rows(dg.CurrentCell.RowIndex + 1).Cells(1)
                Else
                    'dg.Rows(dg.RowCount - 1).Cells(19).Value = txtwrkhour.Text
                    Dg.Rows(Dg.RowCount - 1).Cells(0).Value = 1
                End If
                Dg.CurrentCell = Dg.Rows(n).Cells(0)
                'dg.BeginEdit(True)
            End If
        End If

        If e.KeyCode = Keys.F5 Then
            Dim OBJ As New FrmsamCFL
            OBJ.ShowDialog()
            'mstyl = Trim(cmbstyle.Text).Replace("HALF", "").Replace("FULL", "").Replace("-", "")
            'mstyl = Regex.Replace(cmbbrand.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", ""), "\d+", "").Trim()
            mstyl = Regex.Replace(cmbbrand.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "").Replace("BRAND", " SHIRT").Replace("ITEM", " SHIRT"), "\d+", "").Trim()
            If Len(Trim(mopername)) > 0 Then
                Dg.Rows(Dg.CurrentCell.RowIndex).Cells(4).Value = mopername
                Dg.Rows(Dg.CurrentCell.RowIndex).Cells(5).Value = msam
                Dg.Rows(Dg.CurrentCell.RowIndex).Cells(20).Value = mempgrade
                Dg.Rows(Dg.CurrentCell.RowIndex).Cells(26).Value = mempgrade
                Dg.CurrentCell = Dg.Rows(Dg.CurrentCell.RowIndex).Cells(6)
                Dg.BeginEdit(False)
            End If
        End If

        If e.KeyCode = Keys.F3 Then
            Dim OBJ As New FrmempCFL
            OBJ.ShowDialog()
            If Len(Trim(mempno)) > 0 Then
                Dg.Rows(Dg.CurrentCell.RowIndex).Cells(1).Value = mempid
                Dg.Rows(Dg.CurrentCell.RowIndex).Cells(2).Value = mempno
                Dg.Rows(Dg.CurrentCell.RowIndex).Cells(3).Value = mempname
                Dg.Rows(Dg.CurrentCell.RowIndex).Cells(24).Value = mempsalary
                Dg.Rows(Dg.CurrentCell.RowIndex).Cells(26).Value = mjbgrade
                Dg.CurrentCell = Dg.Rows(Dg.CurrentCell.RowIndex).Cells(4)
                Dg.BeginEdit(False)
            End If
        End If

        If e.KeyCode = Keys.F9 Then
            Dg.Rows.RemoveAt(Dg.CurrentRow.Index)
        End If
        If e.KeyCode = Keys.F8 Then
            Call duplicate()
        End If
    End Sub

    Private Sub cancel()
        cmbline.Text = ""
        mskdate.Text = "  -  -    "
        txtitem.Text = ""
        cmbstyle.Text = ""
        cmbcol.Text = ""
        cmbprocess.Text = ""
        txtno.Text = ""
        dg2.Visible = False
        cmbline.Enabled = False
        mskdate.Enabled = False
        txtitem.Enabled = False
        cmbstyle.Enabled = False
        cmbcol.Enabled = False
        cmbprocess.Enabled = False
        msel = 0
        cmdsave.Enabled = False
        Dg.Rows.Clear()
    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        msel = 1
        txtno.Text = autoId()
        cmbline.Enabled = True
        mskdate.Enabled = True
        txtitem.Enabled = True
        cmbstyle.Enabled = True
        cmbcol.Enabled = True
        cmbprocess.Enabled = True
        mskdate.Text = Format(Now(), "dd-MM-yyyy")
        If cmdsave.Enabled = False Then cmdsave.Enabled = True
        txtno.Enabled = False
        cmbline.Focus()

    End Sub

    Private Sub cmdedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdedit.Click
        msel = 2
        cmbline.Enabled = True
        mskdate.Enabled = True
        txtitem.Enabled = True
        cmbstyle.Enabled = True
        cmbcol.Enabled = True
        cmbprocess.Enabled = True
        If txtno.Enabled = False Then txtno.Enabled = True
        'mskdate.Text = Format(Now(), "dd-MM-yyyy")
        If cmdsave.Enabled = False Then cmdsave.Enabled = True
        If dg2.Visible = False Then dg2.Visible = True
        cmbline.Enabled = True
        cmbline.Focus()
    End Sub

    Private Sub cmddel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddel.Click
        msel = 3
        cmbline.Enabled = True
        mskdate.Enabled = True
        txtitem.Enabled = True
        cmbstyle.Enabled = True
        cmbcol.Enabled = True
        cmbprocess.Enabled = True
        'mskdate.Text = Format(Now(), "dd-MM-yyyy")
        If cmdsave.Enabled = True Then cmdsave.Enabled = False
        If dg2.Visible = False Then dg2.Visible = True
        cmbline.Enabled = True
        cmbline.Focus()
    End Sub

    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.Click
        msel = 4
        cmbline.Enabled = True
        mskdate.Enabled = True
        txtitem.Enabled = True
        cmbstyle.Enabled = True
        cmbcol.Enabled = True
        cmbprocess.Enabled = True
        'mskdate.Text = Format(Now(), "dd-MM-yyyy")
        If cmdsave.Enabled = True Then cmdsave.Enabled = False
        If dg2.Visible = False Then dg2.Visible = True
        cmbline.Enabled = True
        cmbline.Focus()
    End Sub

    Private Sub loaddata()

        'msql = "select  sno, opername,mctype,SAM,pcs,manpower,jobgrade,style,process from " & Trim(mcostdbnam) & ".dbo.processjobmaster order by sno"


        'qry = " select * from " & Trim(mcostdbnam) & ".dbo.operf with (nolock) where bno=" & Val(txtno.Text) & " and  [lineno]='" & cmbline.Text & "' and date='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"

        qry = " select itemname,style,col,processname,brand,totprodqty,totrejqty,nomac,case when isnull(mergno,0)=0 then 1 else isnull(mergno,0) end mergno from " & Trim(mcostdbnam) & ".dbo.operf with (nolock) where bno=" & Val(txtno.Text) & " and  [lineno]='" & cmbline.Text & "' and date='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"



        'Dim query As String




        Dim dt1 As DataTable = getDataTable(qry)
        If dt1.Rows.Count > 0 Then
            For Each row As DataRow In dt1.Rows
                txtitem.Text = row("itemname")
                cmbstyle.Text = row("style")
                cmbcol.Text = row("col")
                cmbprocess.Text = row("processname")
                cmbbrand.Text = row("brand")
                txtprodqty.Text = row("totprodqty")
                txtrejqty.Text = row("totrejqty")
                txtnomac.Text = row("nomac")
                txtmergno.Text = row("mergno")
            Next

            MSQL = " SELECT SNO,EMPNO,EMPID,EMPNAME,opername,sam,totprod,totmin,isnull(wt,0) wt,isnull(mcdwntime,0) mcdwntime,isnull(elecdwntime,0) elecdwntime ,isnull(others,0) others,totoffstdtime,onstdtime,samproduced,per,effper,totper,uttper,jobgrade,isnull(incentive,0) incentive,isnull(amount,0) amount,brand,isnull(rejqty,0) rejqty,salary,incyn,empjobgrade from " & Trim(mcostdbnam) & ".dbo.perf1 where bno=" & Val(txtno.Text) & " and [lineno]='" & Trim(cmbline.Text) & "' and date='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' order by sno"
            'MSQL = " SELECT SNO,EMPNO,EMPID,EMPNAME,opername,sam,totprod,totmin,isnull(wt,0) wt,isnull(mcdwntime,0) mcdwntime,isnull(elecdwntime,0) elecdwntime ,isnull(others,0) others,totoffstdtime,onstdtime,samproduced,per,effper,totper,uttper,jobgrade,isnull(incentive,0) incentive,isnull(amount,0) amount,brand,isnull(rejqty,0) rejqty,salary,incyn from " & Trim(mcostdbnam) & ".dbo.perf1 where bno=" & Val(txtno.Text) & " and [lineno]='" & Trim(cmbline.Text) & "' and date='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' order by sno"



            Dg.Rows.Clear()
            Dim dt As DataTable = getDataTable(MSQL)
            For Each row As DataRow In dt.Rows
                Dg.Rows.Add()
                Dg.Rows(Dg.RowCount - 1).Cells(0).Value = row("sno")
                Dg.Rows(Dg.RowCount - 1).Cells(1).Value = row("empid")
                Dg.Rows(Dg.RowCount - 1).Cells(2).Value = row("empno")
                Dg.Rows(Dg.RowCount - 1).Cells(3).Value = row("empname")
                Dg.Rows(Dg.RowCount - 1).Cells(4).Value = row("opername")
                Dg.Rows(Dg.RowCount - 1).Cells(5).Value = row("sam")
                Dg.Rows(Dg.RowCount - 1).Cells(6).Value = row("totprod")
                Dg.Rows(Dg.RowCount - 1).Cells(7).Value = row("totmin")
                Dg.Rows(Dg.RowCount - 1).Cells(8).Value = row("wt")
                Dg.Rows(Dg.RowCount - 1).Cells(9).Value = row("mcdwntime")
                Dg.Rows(Dg.RowCount - 1).Cells(10).Value = row("elecdwntime")
                Dg.Rows(Dg.RowCount - 1).Cells(11).Value = row("others")
                Dg.Rows(Dg.RowCount - 1).Cells(12).Value = row("totoffstdtime")
                Dg.Rows(Dg.RowCount - 1).Cells(13).Value = row("onstdtime")
                Dg.Rows(Dg.RowCount - 1).Cells(14).Value = row("samproduced")
                Dg.Rows(Dg.RowCount - 1).Cells(15).Value = row("per")
                Dg.Rows(Dg.RowCount - 1).Cells(16).Value = row("effper")
                Dg.Rows(Dg.RowCount - 1).Cells(17).Value = row("totper")
                Dg.Rows(Dg.RowCount - 1).Cells(18).Value = row("uttper")
                Dg.Rows(Dg.RowCount - 1).Cells(19).Value = row("rejqty")
                Dg.Rows(Dg.RowCount - 1).Cells(20).Value = row("jobgrade")
                Dg.Rows(Dg.RowCount - 1).Cells(21).Value = row("incentive")
                Dg.Rows(Dg.RowCount - 1).Cells(22).Value = row("amount")
                Dg.Rows(Dg.RowCount - 1).Cells(23).Value = row("brand")
                Dg.Rows(Dg.RowCount - 1).Cells(24).Value = row("salary")
                Dg.Rows(Dg.RowCount - 1).Cells(25).Value = row("incyn") & vbNullString
                Dg.Rows(Dg.RowCount - 1).Cells(26).Value = row("empjobgrade") & vbNullString

            Next


            'Dim cmd As New OleDbCommand
            'Dim da As New OleDbDataAdapter

            'cmd.CommandText = MSQL
            'cmd.Connection = con
            'da.SelectCommand = cmd
            'Dim dt As New DataTable
            'da.Fill(dt)
            'dg.DataSource = dt
            ''dg.Columns(0).Width = 60
            ''dg.Columns(1).Width = 250
            ''dg.Columns(2).Width = 80
            ''dg.Columns(3).Width = 70
            ''dg.Columns(4).Width = 70
            ''dg.Columns(5).Width = 70
            ''dg.Columns(6).Width = 70
            ''dg.Columns(7).Width = 100
            ''dg.Columns(8).Width = 125
        End If
        dt1.Dispose()
        'mstyl = Regex.Replace(cmbbrand.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", ""), "\d+", "").Trim()
        mstyl = Regex.Replace(cmbbrand.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "").Replace("BRAND", " SHIRT").Replace("ITEM", " SHIRT"), "\d+", "").Trim()
        'mstyl = cmbbrand.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "")

    End Sub

    Private Sub cmbline_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbline.LostFocus
        'If msel > 1 Then
        '    Call loaddata()
        '    If msel = 3 Then
        '        If MsgBox("Delete!", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '            MSQL = "delete from " & Trim(mcostdbnam) & ".dbo.operf where lineno'" & cmbline.Text & "' and date='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"
        '            qry = "delete from " & Trim(mcostdbnam) & ".dbo.perf1 where lineno'" & cmbline.Text & "' and date='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"
        '            executeQuery(MSQL)
        '            executeQuery(qry)
        '            MsgBox("Deleted sucessfully")
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub saverec()
        'Dim strsql As String = "select * from " & Trim(mcostdbnam) & ".dbo.processjobmaster where opername='" & Trim(txtopername.Text) & "' and process='" & Trim(cmbdepartment.Text) & "'"
        'If dataexists(strsql) = False Then

        If msel = 1 Or msel = 2 Then
            If Val(cmbline.Text) > 0 Then
                If msel = 2 Then
                    'dqry1 = "delete from " & Trim(mcostdbnam) & ".dbo.operf where bno=" & Val(txtno.Text) & " and [lineno]='" & cmbline.Text & "' and date='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"
                    'dqry2 = "delete from " & Trim(mcostdbnam) & ".dbo.perf1 where [lineno]='" & cmbline.Text & "' and date='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"

                    dqry1 = "delete from " & Trim(mcostdbnam) & ".dbo.operf where bno=" & Val(txtno.Text) & " and [lineno]='" & cmbline.Text & "' and date='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"
                    dqry2 = "delete from " & Trim(mcostdbnam) & ".dbo.perf1 where bno=" & Val(txtno.Text) & " and [lineno]='" & cmbline.Text & "' and date='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"
                End If

                MSQL = "insert into " & Trim(mcostdbnam) & ".dbo.operf(bno,[lineno],date,style,col,itemname,processname,brand,totprodqty,totrejqty,nomac,mergno)" & vbCrLf _
                                & " Values (" & Val(txtno.Text) & ",'" & Trim(cmbline.Text) & "','" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "','" & Trim(cmbstyle.Text) & "','" & Trim(cmbcol.Text) & "','" & Trim(txtitem.Text) & "'" & vbCrLf _
                                & ",'" & Trim(cmbprocess.Text) & "','" & Trim(cmbbrand.Text) & "'," & Val(txtprodqty.Text) & "," & Val(txtrejqty.Text) & "," & Val(txtnomac.Text) & "," & Val(txtmergno.Text) & ")"


                trans = con.BeginTransaction

                Dim dcmd As New SqlCommand(dqry1, con, trans)
                Dim dcmd2 As New SqlCommand(dqry2, con, trans)

                Dim cmd As New SqlCommand(MSQL, con, trans)

                Try
                    'If Len(Trim(row.Cells(0).Value)) > 0 Then


                    If msel = 2 Then
                        dcmd.ExecuteNonQuery()
                        dcmd2.ExecuteNonQuery()
                    End If
                    cmd.ExecuteNonQuery()

                    For j = 0 To Dg.Rows.Count - 1
                        If msel = 1 Or msel = 2 Then
                            If IsNothing(Dg.Item(1, j).Value) = False Then
                                qry = " insert into " & mcostdbnam & ".dbo.perf1(bno,SNO,EMPID,empno,EMPNAME,opername,sam,totprod,totmin,wt,mcdwntime,elecdwntime,others,totoffstdtime,onstdtime,samproduced,per,effper,totper,uttper,[lineno],date,processname,rejqty,jobgrade,brand,salary,incyn,empjobgrade)" & vbCrLf _
                                   & " values(" & Val(txtno.Text) & "," & Val(Dg.Rows(j).Cells(0).Value) & "," & Val(Dg.Rows(j).Cells(1).Value) & "," & Val(Dg.Rows(j).Cells(2).Value) & ",'" & Trim(Dg.Rows(j).Cells(3).Value) & "'" & vbCrLf _
                                   & ",'" & Trim(Dg.Rows(j).Cells(4).Value) & "'," & Val(Dg.Rows(j).Cells(5).Value) & "," & Val(Dg.Rows(j).Cells(6).Value) & "," & Val(Dg.Rows(j).Cells(7).Value) & "," & Val(Dg.Rows(j).Cells(8).Value) & "" & vbCrLf _
                                   & "," & Val(Dg.Rows(j).Cells(9).Value) & "," & Val(Dg.Rows(j).Cells(10).Value) & "," & Val(Dg.Rows(j).Cells(11).Value) & "," & Val(Dg.Rows(j).Cells(12).Value) & "," & Val(Dg.Rows(j).Cells(13).Value) & "" & vbCrLf _
                                   & "," & Val(Dg.Rows(j).Cells(14).Value) & "," & Val(Dg.Rows(j).Cells(15).Value) & "," & Val(Dg.Rows(j).Cells(16).Value) & "," & Val(Dg.Rows(j).Cells(17).Value) & "," & Val(Dg.Rows(j).Cells(18).Value) & "" & vbCrLf _
                                   & ",'" & Trim(cmbline.Text) & "','" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "','" & Trim(cmbprocess.Text) & "'" & vbCrLf _
                                   & "," & Val(Dg.Rows(j).Cells(19).Value) & ",'" & Trim(Dg.Rows(j).Cells(20).Value) & "','" & Trim(cmbbrand.Text) & "'," & Val(Dg.Rows(j).Cells(24).Value) & ",'" & Dg.Rows(j).Cells(25).Value & "','" & Dg.Rows(j).Cells(26).Value & "'" & ")"
                                Dim ecmd As New SqlCommand(qry, con, trans)
                                ecmd.ExecuteNonQuery()
                            End If


                            'Dim ecmd As New OleDbCommand(qry, con, trans)
                            'ecmd.ExecuteNonQuery()
                        End If
                    Next j
                    'End If
                    'Next
                    MsgBox("Saved!")
                    trans.Commit()
                    'Call cancel()
                Catch ex As Exception
                    trans.Rollback()
                    MsgBox(ex.Message)

                    merr = Trim(ex.Message)
                    'MsgBox(merr)
                    If InStr(merr, "PRIMARY KEY") > 0 Then


                        'Dim CMD3 As New OleDb.OleDbCommand("SELECT MAX(docnum) AS TNO FROM oinward", con)


                        'If con.State = ConnectionState.Closed Then
                        '    con.Open()
                        'End If

                        'Dim CBNO As Int32 = IIf(IsDBNull(CMD3.ExecuteScalar) = False, CMD3.ExecuteScalar, 0)

                        'txtno.Text = CBNO + 1
                        'CMD3.Dispose()
                        'con.Close()
                        txtno.Text = autoId()
                        msel = 1
                        Call saverec()


                    End If

                End Try
            End If

        End If

    End Sub
    Private Sub cmbline_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbline.SelectedIndexChanged

    End Sub

    Private Function autoId() As Integer ' generating auto employee id for a new employee

        Dim query = "Select IsNull(Max(bno),0)+1 operid From " & Trim(mcostdbnam) & ".dbo.operf"
        Dim dr As SqlDataReader
        dr = getDataReader(query)
        dr.Read()
        o_id = dr("operid")
        dr.Close()

        Return o_id

    End Function

    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        If Val(txtno.Text) > 0 Then
            Call saverec()

            Call loadhead()
            Call cancel()
        End If

    End Sub

    Private Sub cmdexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexport.Click
        gridexcelexport(Dg, 1)
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        Call cancel()
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub mskdate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles mskdate.LostFocus
        If msel > 1 Then
            Call loaddata()
            If msel = 3 Then
                If MsgBox("Delete!", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    MSQL = "delete from " & Trim(mcostdbnam) & ".dbo.operf where bno=" & Val(txtno.Text) & " and [lineno]='" & cmbline.Text & "' and date='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"
                    qry = "delete from " & Trim(mcostdbnam) & ".dbo.perf1 where bno=" & Val(txtno.Text) & " and [lineno]='" & cmbline.Text & "' and date='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"
                    executeQuery(MSQL)
                    executeQuery(qry)
                    MsgBox("Deleted sucessfully")
                    Call loadhead()
                    msel = 0
                End If
            End If
        End If
        mkdate = Format(CDate(mskdate.Text), "yyyy-MM-dd")
    End Sub

    Private Sub mskdate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskdate.MaskInputRejected

    End Sub

    Private Sub cmbstyle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbstyle.SelectedIndexChanged

    End Sub

    Private Sub loadhead()


        If chkall.Checked = True Then
            qry = " select bno,[lineno] linno,date,processname,brand,totprodqty,totrejqty from " & Trim(mcostdbnam) & ".dbo.operf with (nolock) order by  bno,date"
        Else
            qry = " select bno,[lineno] linno,date,processname,brand,totprodqty,totrejqty from " & Trim(mcostdbnam) & ".dbo.operf with (nolock) where lock=0 order by bno,date"
        End If

        'qry = " select * from " & Trim(mcostdbnam) & ".dbo.operf with (nolock) where bno=" & Val(txtno.Text) & " and  [lineno]='" & cmbline.Text & "' and date='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"



        'Dim query As String


        dg2.Rows.Clear()

        Dim dt1 As DataTable = getDataTable(qry)
        If dt1.Rows.Count > 0 Then
            For Each row As DataRow In dt1.Rows
                n = dg2.Rows.Add
                'dg2.Rows(n).Cells(0).Value = Convert.ToDouble(rowV.Cells("Tot_Work_hours").Value)
                dg2.Rows(n).Cells(0).Value = Val(row("bno"))
                dg2.Rows(n).Cells(1).Value = row("date")
                dg2.Rows(n).Cells(2).Value = row("linno")
                dg2.Rows(n).Cells(3).Value = row("processname")
                dg2.Rows(n).Cells(4).Value = row("brand")
                dg2.Rows(n).Cells(5).Value = row("Totprodqty")
                dg2.Rows(n).Cells(6).Value = row("totrejqty")


            Next
        End If
        mskdate.Focus()
    End Sub

    Private Sub dg2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg2.CellContentClick

    End Sub

    Private Sub dg2_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dg2.CellMouseDoubleClick
        txtno.Text = dg2.Rows(e.RowIndex).Cells(0).Value
        mskdate.Text = dg2.Rows(e.RowIndex).Cells(1).Value
        cmbline.Text = dg2.Rows(e.RowIndex).Cells(2).Value
        If dg2.Visible = True Then dg2.Visible = False
        Call loaddata()

    End Sub


    Private Sub duplicate()
        Dg.Sort(Dg.Columns(2), System.ComponentModel.ListSortDirection.Ascending)

        For i = 0 To Dg.RowCount - 2
            For j = i + 1 To Dg.RowCount - 1
                Dim row2 = Dg.Rows(j)

                If Not row2.IsNewRow Then
                    Dim row1 = Dg.Rows(i)

                    If row1.Cells(2).Value.ToString() = row2.Cells(2).Value.ToString() And row1.Cells(25).Value.ToString() = row2.Cells(25).Value.ToString() Then
                        If row1.Cells(25).Value.ToString() = "Y" Then
                            row1.DefaultCellStyle.BackColor = Color.LightGreen
                            row2.DefaultCellStyle.BackColor = Color.LightGreen
                        End If
                    End If
                End If
            Next
        Next
    End Sub

   
    Private Sub chkall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkall.CheckedChanged
        'If chkall.Checked = True Then
        '    Call loadhead()
        'Else
        '    Call loadhead()
        'End If
    End Sub

    Private Sub chkall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkall.Click
        If chkall.Checked = True Then
            Call loadhead()
        Else
            Call loadhead()
        End If
    End Sub

    Private Sub txtno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtno.KeyPress
        'If Asc(e.KeyChar) = 13 Then
        '    Dim temp As Integer = 0
        '    For i As Integer = 0 To dg2.RowCount - 1
        '        'For j As Integer = 0 To gv.ColumnCount - 1
        '        If dg2.Rows(i).Cells(0).Value = txtno.Text Then
        '            'MsgBox("Item found")
        '            'dg2.Rows(i).Cells(0).Value = "True"

        '            dg2.Sort(dg2.Columns(0), System.ComponentModel.ListSortDirection.Descending)
        '            'dg.Item(0, i).Value = "True"
        '            'txtno.Text = ""
        '            temp = 1
        '        End If
        '        'Next
        '    Next
        '    If temp = 0 Then
        '        MsgBox("Item not found")
        '    End If
        'End If

        If msel > 1 Then
            For j As Integer = 0 To dg2.Rows.Count - 1
                If e.KeyChar <> Chr(Keys.Back) Then
                    If dg2.Rows(j).Cells(0).Value.ToString.StartsWith(txtno.Text & e.KeyChar, StringComparison.CurrentCultureIgnoreCase) Then
                        Flag = True
                        Exit For

                    Else
                        Flag = False

                    End If

                Else

                    If txtno.Text.Length <> 0 Then
                        Flag = True
                    End If
                End If

            Next

            If Flag = False Then
                'If e.KeyChar <> Chr(Keys.Up) Then
                If e.KeyChar <> Chr(Keys.Return) Then
                    If txtno.SelectedText = "" Then
                        e.Handled = True
                        Beep()
                    Else
                        Dim searchindex As Integer = 0
                        For Each row As DataGridViewRow In dg2.Rows
                            For Each cell As DataGridViewCell In row.Cells
                                If cell.Value.ToString.StartsWith(e.KeyChar, StringComparison.InvariantCultureIgnoreCase) Then
                                    cell.Selected = True
                                    MsgBox("Found")
                                    dg2.CurrentCell = dg2.SelectedCells(0)
                                    'Exit For
                                    cell.Style.BackColor = Color.Yellow
                                    searchindex += 1
                                End If
                            Next
                        Next
                    End If
                End If
            End If
        End If
        If Asc(e.KeyChar) = 27 Then
            If dg2.Visible = True Then dg2.Visible = False
        End If
        If Asc(e.KeyChar) = 13 Then
            If msel > 1 Then
                If dg2.Visible = True Then dg2.Visible = False
                'LOADEMPMASTER()
            End If
        End If

    End Sub

    Private Sub txtno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtno.TextChanged
        If msel = 2 Or msel = 3 Or msel = 4 Then
            txtno.CharacterCasing = CharacterCasing.Upper
            If msel > 1 Then
                If Val(txtno.Text) = 0 Then
                    If dg2.Visible = False Then dg2.Visible = True
                End If


                If Flag = True Then
                    dg2.ClearSelection()
                    For Each row As DataGridViewRow In dg2.Rows
                        For Each cell As DataGridViewCell In row.Cells
                            If cell.ColumnIndex = 0 Then
                                If cell.Value.ToString.StartsWith(txtno.Text.ToString, StringComparison.InvariantCultureIgnoreCase) Then

                                    cell.Selected = True
                                    dg2.CurrentCell = dg2.SelectedCells(0)
                                    'Exit For
                                End If
                            End If

                        Next
                    Next
                Else

                    If txtno.Text = "" Then
                        dg2.Rows(0).Selected = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtnomac_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtnomac.TextChanged

    End Sub

    Private Sub dg2_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dg2.CellPainting
        ' Highlight the Patient column data only.
        If Me.dg2.Columns(0).Index = e.ColumnIndex AndAlso e.RowIndex >= 0 Then

            ' Data need to highlight into Grid cell.
            Dim sw As String = l_strSerachData
            If Not String.IsNullOrEmpty(sw) Then
                '  If Not  String.IsNullOrWhiteSpace(e.FormattedValue.ToString()) Then
                If Not String.IsNullOrEmpty(e.FormattedValue.ToString()) Then

                    Dim val As String = Replace(DirectCast(e.FormattedValue, String), vbCrLf, String.Empty)
                    Dim sindx As Integer = val.ToLower.IndexOf(sw.ToLower)
                    If sindx >= 0 Then

                        e.Handled = True
                        e.PaintBackground(e.CellBounds, True)

                        'the highlite rectangle
                        Dim hl_rect As New Rectangle()
                        hl_rect.Y = e.CellBounds.Y + 2
                        hl_rect.Height = e.CellBounds.Height - 5

                        'find the size of the text before the search word
                        'and the size of the search word
                        Dim sBefore As String = val.Substring(0, sindx)
                        Dim sWord As String = val.Substring(sindx, sw.Length)
                        Dim s1 As Size = TextRenderer.MeasureText(e.Graphics, sBefore, e.CellStyle.Font, e.CellBounds.Size)
                        Dim s2 As Size = TextRenderer.MeasureText(e.Graphics, sWord, e.CellStyle.Font, e.CellBounds.Size)

                        'adjust the widths to make the highlite more accurate
                        If s1.Width > 5 Then
                            hl_rect.X = e.CellBounds.X + s1.Width - 5
                            hl_rect.Width = s2.Width - 6
                        Else
                            hl_rect.X = e.CellBounds.X + 2
                            hl_rect.Width = s2.Width - 6
                        End If

                        'use darker highlight when the row is selected
                        Dim hl_brush As SolidBrush

                        hl_brush = New SolidBrush(Color.Yellow)
                        'paint the background behind the search word
                        e.Graphics.FillRectangle(hl_brush, hl_rect)
                        hl_brush.Dispose()

                        'paint the content as usual
                        e.PaintContent(e.CellBounds)
                    End If

                End If

            End If

        End If

    End Sub

    Private Sub dg2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dg2.KeyUp
        l_strSerachData = e.KeyData.ToString()
    End Sub

    Private Sub cmbbrand_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbbrand.SelectedIndexChanged

    End Sub
End Class
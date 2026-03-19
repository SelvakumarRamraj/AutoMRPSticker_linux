Imports System.Data
'Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class Frmopermaster
    Dim msql, msql2, merr, qry, qry1 As String
    Dim i, k, j, msel, msel1 As Int16
    Dim o_id
    Dim e_id
    Dim flag As Boolean
    Dim icol As Int32
    Private transd As SqlTransaction
    Dim lastkey As String
    Private Sub Frmopermaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        cmbdepartment.Items.Add("CUTTING")
        cmbdepartment.Items.Add("FUSHING")
        cmbdepartment.Items.Add("EMB")
        cmbdepartment.Items.Add("STITCHING")
        cmbdepartment.Items.Add("CHECKING")
        cmbdepartment.Items.Add("IRONING")

        cmbdept.Items.Add("CUTTING")
        cmbdept.Items.Add("FUSHING")
        cmbdept.Items.Add("EMB")
        cmbdept.Items.Add("STITCHING")
        cmbdept.Items.Add("CHECKING")
        cmbdept.Items.Add("IRONING")


        'cmbstyle.Items.Add("GENERAL")
        'cmbstyle.Items.Add("FULL SLEEVE")
        'cmbstyle.Items.Add("HALF SLEEVE")
        'cmbstyle.Items.Add("FRENCH DRAW")

        cmbgrade.Items.Add("I")
        cmbgrade.Items.Add("II")
        cmbgrade.Items.Add("III")
        cmbgrade.Items.Add("IV")
        cmbgrade.Items.Add("V")
        cmbgrade.Items.Add("VI")
        cmbgrade.Items.Add("VII")
        cmbgrade.Items.Add("VIII")
        cmbgrade.Items.Add("IX")
        cmbgrade.Items.Add("X")

        Call loadstyle()

        Call loaddata()
        'Call loadsubdept()
    End Sub

    Private Function autoId() As Integer ' generating auto employee id for a new employee

        'Dim query = "Select IsNull(Max(PID),0)+1 operid From " & Trim(mcostdbnam) & ".dbo.operationmaster"
        'Dim dr As OleDbDataReader
        'dr = getDataReader(query)
        'dr.Read()
        'o_id = dr("operid")
        'dr.Close()

        'Return o_id

    End Function
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
        dr.Close()

        'Return o_id
    End Sub

    Private Sub loaddata()
        'msql = "select  opername,processname,isnull(style,'') style,isnull(mactype,'') mactype from " & Trim(mcostdbnam) & ".dbo.opermaster"

        msql = "select  sno, opername,mctype,SAM,pcs,manpower,jobgrade,style,process from " & Trim(mcostdbnam) & ".dbo.processjobmaster order by sno"

        'dg.Columns(0).Width = 100
        'dg.Columns(1).Width = 200
        'dg.Columns(2).Width = 150
        'dg.Columns(3).Width = 150
        'dg.Columns(4).Width = 100

        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter

        cmd.CommandText = msql
        cmd.Connection = con
        da.SelectCommand = cmd
        Dim dt As New DataTable
        da.Fill(dt)
        dg.DataSource = dt
        dg.Columns(0).Width = 60
        dg.Columns(1).Width = 250
        dg.Columns(2).Width = 80
        dg.Columns(3).Width = 70
        dg.Columns(4).Width = 70
        dg.Columns(5).Width = 70
        dg.Columns(6).Width = 70
        dg.Columns(7).Width = 100
        dg.Columns(8).Width = 125


        'Dim col2 As New DataGridViewComboBoxColumn
        'col2.DataPropertyName = "linno"
        'col2.HeaderText = "linno"
        'col2.Name = "linno"

        'col2.Items.Add("StockItem1")
        'col2.Items.Add("StockItem2")

        ''Dim dgvc As DataGridViewComboBoxCell
        ''dgvc = dg.Rows(0).Cells(4)
        ''dgvc.Items.Add("General")
        'For k = 1 To 16
        '    col2.Items.Add(k)
        'Next k

        dg.Columns(0).ReadOnly = True
        dg.Columns(1).ReadOnly = True
        dg.Columns(2).ReadOnly = True
        'dg.Columns(3).ReadOnly = True
        'dg.Columns(4).ReadOnly = True

    End Sub

    Private Sub cmbdepartment_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdepartment.SelectedIndexChanged

    End Sub

    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick

    End Sub

    Private Sub dg_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dg.CellMouseDoubleClick
        If e.RowIndex >= 0 Then
            cmboper.Text = dg.Rows(e.RowIndex).Cells(1).Value
            txtopername.Text = dg.Rows(e.RowIndex).Cells(1).Value
            txtmctype.Text = dg.Rows(e.RowIndex).Cells(2).Value
            txtsam.Text = Val(dg.Rows(e.RowIndex).Cells(3).Value)
            txtpcs.Text = Val(dg.Rows(e.RowIndex).Cells(4).Value)
            txtmanpower.Text = Val(dg.Rows(e.RowIndex).Cells(5).Value)
            cmbgrade.Text = dg.Rows(e.RowIndex).Cells(6).Value
            cmbdepartment.Text = dg.Rows(e.RowIndex).Cells(8).Value
            cmbstyle.Text = dg.Rows(e.RowIndex).Cells(7).Value
        End If
    End Sub

    Private Sub dg_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dg.DataError
        If (e.Exception.Message = "DataGridViewComboBoxCell value is not valid.") Then
            Dim value As Object = dg.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & vbNullString
            If Not CType(dg.Columns(e.ColumnIndex), DataGridViewComboBoxColumn).Items.Contains(Text) Then
                CType(dg.Columns(e.ColumnIndex), DataGridViewComboBoxColumn).Items.Add(Text)
                e.ThrowException = False
            End If

        End If
    End Sub

    Private Sub butsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butsave.Click
        'If msel = 1 Then
        'For Each row As DataGridViewRow In dgo.Rows
        Dim strsql As String = "select * from " & Trim(mcostdbnam) & ".dbo.processjobmaster where opername='" & Trim(txtopername.Text) & "' and style='" & Trim(cmbstyle.Text) & "' and process='" & Trim(cmbdepartment.Text) & "'"
        If dataexists(strsql) = False Then

            If Len(Trim(txtopername.Text)) > 0 And Len(Trim(cmbdepartment.Text)) > 0 Then
                msql = "insert into " & Trim(mcostdbnam) & ".dbo.processjobmaster(opername,process,style,mctype,jobgrade,SAM,pcs,manpower)" & vbCrLf _
                                & " Values ('" & Trim(txtopername.Text) & "','" & Trim(cmbdepartment.Text) & "','" & Trim(cmbstyle.Text) & "','" & Trim(txtmctype.Text) & "','" & Trim(cmbgrade.Text) & "'" & vbCrLf _
                                & "," & Val(txtsam.Text) & "," & Val(txtpcs.Text) & "," & Val(txtmanpower.Text) & ")"

                'msql = "insert into " & Trim(mcostdbnam) & ".dbo.opermaster(opername,processname,style,mactype)" & vbCrLf _
                '& " Values ('" & Trim(txtopername.Text) & "','" & Trim(cmbdepartment.Text) & "','" & Trim(cmbstyle.Text) & "','" & Trim(txtmctype.Text) & "'" & ")"
                Try
                    'If Len(Trim(row.Cells(0).Value)) > 0 Then
                    executeQuery(msql)
                    'End If
                    'Next
                    MsgBox("Saved!")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Else
                MsgBox("select Department and operation name !")
            End If
            Call cancel()
            'txtopername.Text = ""
            'cmbdepartment.Text = ""
            'cmbstyle.Text = ""

            Call loaddata()
        Else
            MsgBox("Already Exists!")
        End If

        'If msel = 2 Then
        '    For Each row As DataGridViewRow In dgo.Rows
        '        If Val(row.Cells(0).Value) > 0 Then
        '            msql = "update " & Trim(mcostdbnam) & ".dbo.operationmaster set operation='" & row.Cells(1).Value & "',  mactype='" & row.Cells(2).Value & "', sam=" & row.Cells(3).Value & ",totprod=" & row.Cells(4).Value & ",totmin=" & row.Cells(5).Value & " where id =" & Val(row.Cells(0).Value)
        '            executeQuery(msql)

        '        End If
        '    Next
        '    MsgBox("Updated!")
        'End If
    End Sub

    Private Sub butcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butcancel.Click
        txtopername.Text = ""
        cmbdepartment.Text = ""
        cmbstyle.Text = ""
        Call cancel()
    End Sub

    Private Sub butexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butexit.Click
        Me.Close()
    End Sub


    'Private Sub edit()

    '    If dg.SelectedRows.Count = 0 Then

    '        Return

    '    End If

    '    If cmdedit.Text = "Edit" Then

    '        txtid.Text = dg.SelectedRows(0).Cells(0).Value
    '        txtname.Text = Trim(dg.SelectedRows(0).Cells(1).Value & vbNullString)
    '        txtpname.Text = Trim(dg.SelectedRows(0).Cells(2).Value & vbNullString)
    '        txtbuild.Text = Trim(dg.SelectedRows(0).Cells(3).Value & vbNullString)
    '        txtblock.Text = Trim(dg.SelectedRows(0).Cells(4).Value & vbNullString)
    '        txtstreet.Text = Trim(dg.SelectedRows(0).Cells(5).Value & vbNullString)
    '        txtcity.Text = Trim(dg.SelectedRows(0).Cells(6).Value & vbNullString)
    '        txtpincode.Text = Trim(dg.SelectedRows(0).Cells(7).Value & vbNullString)
    '        txtdistrict.Text = Trim(dg.SelectedRows(0).Cells(8).Value & vbNullString)
    '        txtstate.Text = Trim(dg.SelectedRows(0).Cells(9).Value & vbNullString)
    '        txtcuntry.Text = Trim(dg.SelectedRows(0).Cells(10).Value & vbNullString)
    '        txtphone.Text = Trim(dg.SelectedRows(0).Cells(11).Value & vbNullString)
    '        txtmobile.Text = Trim(dg.SelectedRows(0).Cells(12).Value & vbNullString)
    '        txtemail.Text = Trim(dg.SelectedRows(0).Cells(13).Value & vbNullString)
    '        txtweb.Text = Trim(dg.SelectedRows(0).Cells(14).Value & vbNullString)
    '        txtpf.Text = Val(dg.SelectedRows(0).Cells(15).Value)
    '        txtesi.Text = Val(dg.SelectedRows(0).Cells(16).Value)
    '        txteps.Text = Val(dg.SelectedRows(0).Cells(17).Value & vbNullString)
    '        txtepf.Text = Val(dg.SelectedRows(0).Cells(18).Value & vbNullString)
    '        txtwrkhour.Text = Val(dg.SelectedRows(0).Cells(19).Value)
    '        'mskdate.Text = IIf(Not IsDBNull(dg.SelectedRows(0).Cells(23).Value.ToString), Format(CDate(dg.SelectedRows(0).Cells(23).Value.ToString), "yyyy-MM-dd"), "__-__-____")
    '        mskdate.Text = dg.SelectedRows(0).Cells(23).Value.ToString

    '        txtesiemp.Text = Val(dg.SelectedRows(0).Cells(24).Value & vbNullString)
    '        txtpfelgamt.Text = Val(dg.SelectedRows(0).Cells(25).Value & vbNullString)
    '        txtesielgamt.Text = Val(dg.SelectedRows(0).Cells(26).Value & vbNullString)
    '        txtpfelgper.Text = Val(dg.SelectedRows(0).Cells(27).Value & vbNullString)
    '        txtnocl.Text = Val(dg.SelectedRows(0).Cells(28).Value & vbNullString)

    '        'If IsDBNull(dg.SelectedRows(0).Cells(20).Value) = False Then
    '        '    If Val(Format(dg.SelectedRows(0).Cells(20).Value, "dd")) > 0 Then
    '        '        txtsaldatefrom.Text = dg.SelectedRows(0).Cells(20).Value
    '        '    End If
    '        'End If
    '        'If IsDBNull(dg.SelectedRows(0).Cells(21).Value) = False Then
    '        '    txtsaldateto.Text = dg.SelectedRows(0).Cells(21).Value
    '        'End If
    '        'If IsDBNull(dg.SelectedRows(0).Cells(22).Value) = False Then
    '        '    txtnodays.Text = dg.SelectedRows(0).Cells(22).Value
    '        'End If


    '        'txtid.Text = dgvDeptt.SelectedRows(0).Cells("DepttID").Value
    '        'txtname.Text = dgvDeptt.SelectedRows(0).Cells("DepttName").Value
    '        'txtphone.Text = dgvDeptt.SelectedRows(0).Cells("DepttPhone").Value
    '        'txtStrength.Text = dgvDeptt.SelectedRows(0).Cells("DepttStrength").Value
    '        'cmboxManagers.SelectedValue = dgvDeptt.SelectedRows(0).Cells("DepttManID").Value
    '        'cmboxManagers.Text = dgvDeptt.SelectedRows(0).Cells("DepttManName").Value
    '        dg.Enabled = False
    '        cmdedit.Text = "Update"
    '        If cmdsave.Enabled = True Then cmdsave.Enabled = False
    '        If cmdadd.Enabled = True Then cmdadd.Enabled = False
    '        If cmddel.Enabled = True Then cmddel.Enabled = False
    '        dg.SelectedRows(0).DefaultCellStyle.ForeColor = Color.Blue
    '    Else
    '        dg.SelectedRows(0).Cells(0).Value = txtid.Text
    '        dg.SelectedRows(0).Cells(1).Value = txtname.Text
    '        dg.SelectedRows(0).Cells(2).Value = txtpname.Text
    '        dg.SelectedRows(0).Cells(3).Value = txtbuild.Text
    '        dg.SelectedRows(0).Cells(4).Value = txtblock.Text
    '        dg.SelectedRows(0).Cells(5).Value = txtstreet.Text
    '        dg.SelectedRows(0).Cells(6).Value = txtcity.Text
    '        dg.SelectedRows(0).Cells(7).Value = txtpincode.Text
    '        dg.SelectedRows(0).Cells(8).Value = txtdistrict.Text
    '        dg.SelectedRows(0).Cells(9).Value = txtstate.Text
    '        dg.SelectedRows(0).Cells(10).Value = txtcuntry.Text
    '        dg.SelectedRows(0).Cells(11).Value = txtphone.Text
    '        dg.SelectedRows(0).Cells(12).Value = txtmobile.Text
    '        dg.SelectedRows(0).Cells(13).Value = txtemail.Text
    '        dg.SelectedRows(0).Cells(14).Value = txtweb.Text
    '        dg.SelectedRows(0).Cells(15).Value = txtpf.Text
    '        dg.SelectedRows(0).Cells(16).Value = txtesi.Text
    '        dg.SelectedRows(0).Cells(17).Value = txteps.Text
    '        dg.SelectedRows(0).Cells(18).Value = txtepf.Text
    '        dg.SelectedRows(0).Cells(19).Value = txtwrkhour.Text
    '        dg.SelectedRows(0).Cells(23).Value = mskdate.Text

    '        dg.SelectedRows(0).Cells(24).Value = txtesiemp.Text
    '        dg.SelectedRows(0).Cells(25).Value = txtpfelgamt.Text
    '        dg.SelectedRows(0).Cells(26).Value = txtesielgamt.Text
    '        dg.SelectedRows(0).Cells(27).Value = txtpfelgper.Text
    '        dg.SelectedRows(0).Cells(28).Value = txtnocl.Text
    '        'dg.SelectedRows(0).Cells(21).Value = txtsaldateto.Text
    '        'dg.SelectedRows(0).Cells(22).Value = txtnodays.Text



    '        dg.Enabled = True
    '        cmdedit.Text = "Edit"
    '        If cmdsave.Enabled = False Then cmdsave.Enabled = True
    '        If cmdadd.Enabled = False Then cmdadd.Enabled = True
    '        If cmddel.Enabled = False Then cmddel.Enabled = True
    '        cmdsave.PerformClick()
    '        dg.SelectedRows(0).DefaultCellStyle.ForeColor = Color.Black
    '    End If

    '    'dg.SelectedRows(0).DefaultCellStyle.ForeColor = Color.Blue
    '    'If cmdadd.Enabled = False Then cmdadd.Enabled = True

    'End Sub

    Private Sub butdel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butdel.Click

        If Len(Trim(txtopername.Text)) > 0 Then
            If MsgBox("Delete the data!", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                msql = "delete from " & Trim(mcostdbnam) & ".dbo.processjobmaster where opername='" & Trim(txtopername.Text) & "' and style='" & Trim(cmbstyle.Text) & "' and process='" & Trim(cmbdepartment.Text) & "'"

                Try
                    'If Len(Trim(row.Cells(0).Value)) > 0 Then
                    executeQuery(msql)
                    'End If
                    'Next
                    MsgBox("Deleted!")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If
        txtopername.Text = ""
        cmbdepartment.Text = ""
        cmbstyle.Text = ""
        Call cancel()
        Call loaddata()
    End Sub
    Private Sub cancel()
        txtopername.Text = ""
        txtmctype.Text = ""
        txtsam.Text = ""
        txtpcs.Text = ""
        txtmanpower.Text = ""
        cmbgrade.Text = ""
        cmbdepartment.Text = ""
        cmbstyle.Text = ""
    End Sub
    Private Sub butupdt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butupdt.Click

        If Len(Trim(cmboper.Text)) > 0 Then
            If Len(Trim(cmbdept.Text)) > 0 Then
                msql = "update " & Trim(mcostdbnam) & ".dbo.processjobmaster set opername='" & Trim(txtopername.Text) & "',process='" & Trim(cmbdept.Text) & "',style='" & Trim(cmbstyle.Text) & "'" & vbCrLf _
                      & ",mctype='" & Trim(txtmctype.Text) & "',jobgrade='" & Trim(cmbgrade.Text) & "'" & vbCrLf _
                      & ",sam=" & Val(txtsam.Text) & ",pcs=" & Val(txtpcs.Text) & ",manpower=" & Val(txtmanpower.Text) & " where opername='" & Trim(cmboper.Text) & "' and style='" & Trim(cmbstyle.Text) & "' and process='" & Trim(cmbdepartment.Text) & "'"
            Else
                msql = "update " & Trim(mcostdbnam) & ".dbo.processjobmaster set opername='" & Trim(txtopername.Text) & "',process='" & Trim(cmbdepartment.Text) & "',style='" & Trim(cmbstyle.Text) & "'" & vbCrLf _
                      & ",mctype='" & Trim(txtmctype.Text) & "',jobgrade='" & Trim(cmbgrade.Text) & "'" & vbCrLf _
                      & ",sam=" & Val(txtsam.Text) & ",pcs=" & Val(txtpcs.Text) & ",manpower=" & Val(txtmanpower.Text) & " where opername='" & Trim(cmboper.Text) & "' and style='" & Trim(cmbstyle.Text) & "' and process='" & Trim(cmbdepartment.Text) & "'"
            End If


            'msql = "insert into " & Trim(mcostdbnam) & ".dbo.processjobmaster(opername,process,style,mctype,jobgrade,SAM,pcs,manpower)" & vbCrLf _
            '                & " Values ('" & Trim(txtopername.Text) & "','" & Trim(cmbdepartment.Text) & "','" & Trim(cmbstyle.Text) & "','" & Trim(txtmctype.Text) & "','" & Trim(cmbgrade.Text) & "'" & vbCrLf _
            '                & "," & Val(txtsam.Text) & "," & Val(txtpcs.Text) & "," & Val(txtmanpower.Text) & ")"

            'msql = "insert into " & Trim(mcostdbnam) & ".dbo.opermaster(opername,processname,style,mactype)" & vbCrLf _
            '& " Values ('" & Trim(txtopername.Text) & "','" & Trim(cmbdepartment.Text) & "','" & Trim(cmbstyle.Text) & "','" & Trim(txtmctype.Text) & "'" & ")"
            Try
                'If Len(Trim(row.Cells(0).Value)) > 0 Then
                executeQuery(msql)
                'End If
                'Next
                MsgBox("Updated!")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
            Call cancel()
            'txtopername.Text = ""
            'cmbdepartment.Text = ""
            'cmbstyle.Text = ""

            Call loaddata()
    End Sub

    Private Sub cmboper_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmboper.SelectedIndexChanged

    End Sub
End Class
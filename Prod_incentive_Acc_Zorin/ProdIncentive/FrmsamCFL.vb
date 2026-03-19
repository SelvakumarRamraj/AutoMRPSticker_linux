Imports System.Data
Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Public Class FrmsamCFL
    Dim msql As String
    Private dt As DataTable = New DataTable
    Private KeyValue As Integer = 0
    Private blnFlag As Boolean = False
    Public rowID As Integer = 0
    Dim mkstyle As String

    Private Sub FrmsamCFL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call loadstyle()
        'mkstyle = cmbstyle.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "").Replace("SHIRT", "")
        mstyl = mstyl.Replace("SHIRT", "")
        AddGridViewColumns()
    End Sub
    Private Sub loaddt()
        'If Len(Trim(mproces)) > 0 And Len(Trim(mstyl)) > 0 Then
        '    msql = "select opername,mctype,sam,style,jobgrade from " & Trim(mcostdbnam) & ".dbo.processjobmaster with (nolock) where process='" & mproces & "' "
        'Else
        '    msql = "select opername,mctype,sam,style,jobgrade from " & Trim(mcostdbnam) & ".dbo.processjobmaster with (nolock) "
        'End If
        If Len(Trim(cmbstyle.Text)) = 0 Then
            mkstyle = Trim(mstyl)
        Else
            'mkstyle = cmbstyle.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "")
            mkstyle = cmbstyle.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "").Replace("SHIRT", "").Trim
        End If

        'If Len(Trim(mstyl)) > 0 And Len(Trim(mkstyle)) = 0 Then
        '    msql = "select opername,mctype,sam,style,jobgrade from " & Trim(mcostdbnam) & ".dbo.processjobmaster with (nolock) where style like '%" & mstyl & "%'"
        'ElseIf Len(Trim(mkstyle)) > 0 And Len(Trim(mstyl)) = 0 Then

        msql = "select opername,mctype,sam,style,jobgrade from " & Trim(mcostdbnam) & ".dbo.processjobmaster with (nolock) where style like '%" & Trim(mkstyle) & "%'"
        'Else
        'msql = "select opername,mctype,sam,style,jobgrade from " & Trim(mcostdbnam) & ".dbo.processjobmaster with (nolock) "
        'End If

        dgvGrid.Rows.Clear()
        'Dim query As String
        dt = getDataTable(msql)
    End Sub
    Private Sub AddGridViewColumns()
        'mkstyle = cmbstyle.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "")
        mkstyle = cmbstyle.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "").Replace("SHIRT", "")
        Call loaddt()
        Try
            'Dim dgvtxtID As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            'dgvtxtID.SortMode = DataGridViewColumnSortMode.NotSortable;
            Dim dgvCode As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            dgvCode.Name = "Operation"
            dgvCode.HeaderText = "opername"
            dgvCode.DataPropertyName = "opername"
            dgvCode.Visible = True
            dgvCode.Width = 200
            'dgvtxtEmployeeCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            Dim dgvName As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            dgvName.Name = "Mac Type"
            dgvName.HeaderText = "mctype"
            dgvName.DataPropertyName = "mctype"
            dgvName.Visible = True
            dgvName.Width = 100

            Dim dgvid As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            dgvid.Name = "SAM"
            dgvid.HeaderText = "SAM"
            dgvid.DataPropertyName = "SAM"
            dgvid.Visible = True
            dgvid.Width = 60

            Dim dgstyl As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            dgstyl.Name = "Style"
            dgstyl.HeaderText = "Style"
            dgstyl.DataPropertyName = "Style"
            dgstyl.Visible = True
            dgstyl.Width = 100



            Dim dggrade As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            dggrade.Name = "Jobgrade"
            dggrade.HeaderText = "jobgrade"
            dggrade.DataPropertyName = "jobgrade"
            dggrade.Visible = False
            dggrade.Width = 70

            dgvGrid.Columns.Add(dgvCode)
            dgvGrid.Columns.Add(dgvName)
            dgvGrid.Columns.Add(dgvid)
            dgvGrid.Columns.Add(dgstyl)
            dgvGrid.Columns.Add(dggrade)

            dgvGrid.DataSource = dt
        Catch ex As Exception
            'ErrorLogClass.SendError(ex, Me.formName, "AddGridViewColumns")
        End Try
    End Sub

    Private Sub dgvGrid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvGrid.CellContentClick

    End Sub
    Private Sub loadstyle()
        Dim query = "Select style From " & Trim(mcostdbnam) & ".dbo.stylemaster"
        Dim dr As sqlDataReader
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
    Private Sub dgvGrid_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvGrid.CellDoubleClick
        Try
            If (e.RowIndex >= 0) Then
                Me.blnFlag = True
                Me.rowID = Convert.ToInt32(e.RowIndex.ToString)
                ' CommonVariables.ItemID = Convert.ToInt32(dgvGrid.Rows(Me.rowID).Cells("ID").Value.ToString)
                mopername = dgvGrid.Rows(Me.rowID).Cells(0).Value.ToString
                mmactype = dgvGrid.Rows(Me.rowID).Cells(1).Value.ToString
                msam = dgvGrid.Rows(Me.rowID).Cells(2).Value.ToString
                mstyl = dgvGrid.Rows(Me.rowID).Cells(3).Value.ToString
                mempgrade = dgvGrid.Rows(Me.rowID).Cells(4).Value.ToString
                Me.Close()
            Else
                mempno = ""
                mempname = ""
                mempid = ""
                mempgrade = ""
                butcancel.PerformClick()
            End If

        Catch ex As Exception
            'ErrorLogClass.SendError(ex, Me.formName, "dgvGrid_CellDoubleClick")
        End Try
    End Sub

    Private Sub dgvGrid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvGrid.KeyDown
        Try
            'errorProvider1.Clear();
            Me.KeyValue = Convert.ToInt32(e.KeyValue)
            If (Me.KeyValue = 27) Then
                Me.Close()
            End If

        Catch ex As Exception
            'ErrorLogClass.SendError(ex, Me.formName, "dgvGrid_KeyDown")
        End Try
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        If Asc(e.KeyChar) = 13 Then
            butchoose.Focus()
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        Try
            Dim dtView As DataView = New DataView(Me.dt)
            If Len(Trim(cmbstyle.Text)) > 0 Then
                'mkstyle = cmbstyle.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "")
                mkstyle = cmbstyle.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "").Replace("SHIRT", "")

                'dtView.RowFilter = ("opername like '%" + txtName.Text.Trim + "%' and style like '%" + cmbstyle.Text.Trim + "%'")
                dtView.RowFilter = ("opername like '%" + txtName.Text.Trim + "%' and style like '%" + mkstyle.Trim + "%'")
            Else
                dtView.RowFilter = ("opername like ('%" _
                        + (txtName.Text.Trim + "%')"))

            End If

            dgvGrid.DataSource = dtView.ToTable
        Catch ex As Exception
            'ErrorLogClass.SendError(ex, Me.formName, "txtCustomerName_TextChanged")
        End Try
    End Sub

    Private Sub butchoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butchoose.Click
        Try
            mopername = dgvGrid.Rows(Me.rowID).Cells(0).Value.ToString
            mmactype = dgvGrid.Rows(Me.rowID).Cells(1).Value.ToString
            msam = dgvGrid.Rows(Me.rowID).Cells(2).Value.ToString
            mstyl = dgvGrid.Rows(Me.rowID).Cells(3).Value.ToString
            mempgrade = dgvGrid.Rows(Me.rowID).Cells(4).Value.ToString
            Me.Close()
        Catch ex As Exception
            MsgBox("Not Found!")
            txtName.Text = ""
            mopername = ""
            mmactype = ""
            msam = ""
            mstyl = ""
            mempgrade = ""
            butcancel.PerformClick()
        End Try

    End Sub

    Private Sub butcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butcancel.Click
        mopername = ""
        mmactype = ""
        msam = ""
        mstyl = ""
        mempgrade = ""
        Me.Close()
    End Sub

    Private Sub butchoose_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles butchoose.GotFocus
        SendKeys.Send("{ENTER}")
    End Sub

    Private Sub cmbstyle_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cmbstyle.KeyPress
        If Asc(e.KeyChar) = 13 Then
            mkstyle = cmbstyle.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "").Replace("SHIRT", "").Trim
            Try
                dgvGrid.DataSource = Nothing
                ' Call your load or refresh logic
                dgvGrid.Columns.Clear() ' Clear existing columns
                Call AddGridViewColumns() ' Reload columns and bind data
            Catch ex As Exception
                MessageBox.Show("Error loading grid: " & ex.Message)
            End Try
            txtName.Focus()
        End If
    End Sub

    Private Sub cmbstyle_LostFocus(sender As Object, e As System.EventArgs) Handles cmbstyle.LostFocus

        'mkstyle = cmbstyle.Text.Replace("FULL", "").Replace("HALF", "").Replace("-", "")
        'dgvGrid.DataSource = Nothing
        '' Call your load or refresh logic
        'dgvGrid.Columns.Clear() ' Clear existing columns
        'Call AddGridViewColumns()
    End Sub

    Private Sub cmbstyle_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbstyle.SelectedIndexChanged
        'Try
        '    dgvGrid.DataSource = Nothing
        '    ' Call your load or refresh logic
        '    dgvGrid.Columns.Clear() ' Clear existing columns
        '    Call AddGridViewColumns() ' Reload columns and bind data
        'Catch ex As Exception
        '    MessageBox.Show("Error loading grid: " & ex.Message)
        'End Try
    End Sub
End Class
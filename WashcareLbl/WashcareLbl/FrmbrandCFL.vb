Public Class FrmbrandCFL
    Dim msql As String
    Private dt As DataTable = New DataTable
    Private KeyValue As Integer = 0
    Private blnFlag As Boolean = False
    Public rowID As Integer = 0
    Private Sub FrmbrandCFL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mbrandgrp = ""

        AddGridViewColumns()
    End Sub

    Private Sub loaddt()
        msql = "select u_brandgroup brandgroup  from oitm with (nolock) where itmsgrpcod in (101,103,104,107,123,141) and isnull(u_brandgroup,'')<>'' group by u_brandgroup order by u_brandgroup"
        dgvGrid.Rows.Clear()
        'Dim query As String
        dt = getDataTable(msql)
    End Sub
    Private Sub AddGridViewColumns()
        Call loaddt()
        Try
            'Dim dgvtxtID As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            'dgvtxtID.SortMode = DataGridViewColumnSortMode.NotSortable;
            Dim dgvCode As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            dgvCode.Name = "brandgroup"
            dgvCode.HeaderText = "brandgroup"
            dgvCode.DataPropertyName = "brandgroup"
            dgvCode.Visible = True
            dgvCode.Width = 250
            ''dgvtxtEmployeeCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            'Dim dgvName As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            'dgvName.Name = "style"
            'dgvName.HeaderText = "style"
            'dgvName.DataPropertyName = "style"
            'dgvName.Visible = True
            'dgvName.Width = 100

            dgvGrid.Columns.Add(dgvCode)
            'dgvGrid.Columns.Add(dgvName)
            'dgvGrid.Columns.Add(dgvid)
            'dgvGrid.Columns.Add(dgsal)
            dgvGrid.DataSource = dt
        Catch ex As Exception
            'ErrorLogClass.SendError(ex, Me.formName, "AddGridViewColumns")
        End Try
    End Sub

    Private Sub dgvGrid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvGrid.CellContentClick

    End Sub

    Private Sub dgvGrid_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvGrid.CellDoubleClick
        Try
            If (e.RowIndex >= 0) Then
                Me.blnFlag = True
                Me.rowID = Convert.ToInt32(e.RowIndex.ToString)
                ' CommonVariables.ItemID = Convert.ToInt32(dgvGrid.Rows(Me.rowID).Cells("ID").Value.ToString)
                mbrandgrp = dgvGrid.Rows(Me.rowID).Cells(0).Value.ToString
                
                Me.Close()
            Else
                mbrandgrp = ""
                
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
            dtView.RowFilter = ("brandgroup like ('%" _
                        + (txtName.Text.Trim + "%')"))
            dgvGrid.DataSource = dtView.ToTable
        Catch ex As Exception
            'ErrorLogClass.SendError(ex, Me.formName, "txtCustomerName_TextChanged")
        End Try
    End Sub

    Private Sub butchoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butchoose.Click
        Try
            mbrandgrp = dgvGrid.Rows(Me.rowID).Cells(0).Value.ToString
            
            Me.Close()
        Catch ex As Exception
            MsgBox("Not Found!")
            'txtCode.Text = ""
            txtName.Text = ""
            mbrandgrp = ""
            
            butcancel.PerformClick()
        End Try

    End Sub

    Private Sub butcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butcancel.Click
        mbrandgrp = ""
        Me.Close()
    End Sub

    Private Sub butchoose_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles butchoose.GotFocus
        SendKeys.Send("{ENTER}")
    End Sub
End Class
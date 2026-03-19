Public Class FrmempCFL
    Dim msql As String
    Private dt As DataTable = New DataTable
    Private KeyValue As Integer = 0
    Private blnFlag As Boolean = False
    Public rowID As Integer = 0

    Private Sub FrmempCFL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Call loaddt()
        mempno = ""
        mempname = ""
        mempid = ""
        mempsalary = 0
        mjbgrade = ""

        AddGridViewColumns()
    End Sub
    Private Sub loaddt()
        msql = "select nempno,empname,nemp_id,salary,jobgrade from " & Trim(mcostdbnam) & ".dbo.empmaster with (nolock)"
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
            dgvCode.Name = "Emp No"
            dgvCode.HeaderText = "nempno"
            dgvCode.DataPropertyName = "nempno"
            dgvCode.Visible = True
            dgvCode.Width = 70
            'dgvtxtEmployeeCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            Dim dgvName As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            dgvName.Name = "Name"
            dgvName.HeaderText = "Emp Name"
            dgvName.DataPropertyName = "EMPName"
            dgvName.Visible = True
            dgvName.Width = 250

            Dim dgvid As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            dgvid.Name = "Emp ID"
            dgvid.HeaderText = "nemp_id"
            dgvid.DataPropertyName = "nemp_id"
            dgvid.Visible = False
            dgvid.Width = 70



            Dim dgsal As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            dgsal.Name = "salary"
            dgsal.HeaderText = "salary"
            dgsal.DataPropertyName = "salary"
            dgsal.Visible = False
            dgsal.Width = 70

            Dim dggrad As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
            dggrad.Name = "jobgrade"
            dggrad.HeaderText = "jobgrade"
            dggrad.DataPropertyName = "jobgrade"
            dggrad.Visible = True
            dggrad.Width = 70

            dgvGrid.Columns.Add(dgvCode)
            dgvGrid.Columns.Add(dgvName)
            dgvGrid.Columns.Add(dgvid)
            dgvGrid.Columns.Add(dgsal)
            dgvGrid.Columns.Add(dggrad)

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
                mempno = dgvGrid.Rows(Me.rowID).Cells(0).Value.ToString
                mempname = dgvGrid.Rows(Me.rowID).Cells(1).Value.ToString
                mempid = dgvGrid.Rows(Me.rowID).Cells(2).Value.ToString
                mempsalary = Val(dgvGrid.Rows(Me.rowID).Cells(3).Value.ToString)
                mjbgrade = dgvGrid.Rows(Me.rowID).Cells(4).Value.ToString
                Me.Close()
            Else
                mempno = ""
                mempname = ""
                mempid = ""
                mempsalary = 0
                mjbgrade = ""
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

    Private Sub txtCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If Asc(e.KeyChar) = 13 Then
            butchoose.Focus()
        End If
    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCode.TextChanged
        Try
            Dim dtView1 As DataView = New DataView(Me.dt)
            'dtView1.RowFilter = ("nempno like ('%" _
            '            + (txtCode.Text.Trim + "%')"))
            dtView1.RowFilter = ("Convert(nempno, 'System.String') like ('%" _
                        + (txtCode.Text.Trim + "%')"))
            dgvGrid.DataSource = dtView1.ToTable
        Catch ex As Exception
            'ErrorLogClass.SendError(ex, Me.formName, "txtCustomerCode_TextChanged")
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
            dtView.RowFilter = ("empname like ('%" _
                        + (txtName.Text.Trim + "%')"))
            dgvGrid.DataSource = dtView.ToTable
        Catch ex As Exception
            'ErrorLogClass.SendError(ex, Me.formName, "txtCustomerName_TextChanged")
        End Try
    End Sub

    Private Sub butchoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butchoose.Click
        Try
            mempno = dgvGrid.Rows(Me.rowID).Cells(0).Value.ToString
            mempname = dgvGrid.Rows(Me.rowID).Cells(1).Value.ToString
            mempid = dgvGrid.Rows(Me.rowID).Cells(2).Value.ToString
            mempsalary = Val(dgvGrid.Rows(Me.rowID).Cells(3).Value.ToString)
            mjbgrade = dgvGrid.Rows(Me.rowID).Cells(4).Value.ToString
            Me.Close()
        Catch ex As Exception
            MsgBox("Not Found!")
            txtCode.Text = ""
            txtName.Text = ""
            mempno = ""
            mempname = ""
            mempid = ""
            mempsalary = 0
            mjbgrade = ""
            butcancel.PerformClick()
        End Try



    End Sub

    Private Sub butcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butcancel.Click
        mempno = ""
        mempname = ""
        mempid = ""
        mempsalary = 0
        mjbgrade = ""
        Me.Close()
    End Sub

    Private Sub butchoose_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles butchoose.GotFocus
        SendKeys.Send("{ENTER}")
    End Sub
End Class
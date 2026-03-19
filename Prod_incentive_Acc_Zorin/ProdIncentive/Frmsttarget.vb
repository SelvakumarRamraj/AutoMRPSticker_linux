Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Windows.Forms
Public Class Frmsttarget
    Dim msql, sql1 As String
    Dim i, n As Integer
    Dim trans1 As SqlTransaction
    Dim isLastRow As Boolean 

    Private Sub Frmsttarget_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        mskdate.Text = Format(CDate(Now), "dd-MM-yyyy")
        Call loaddata()
        dgr.Visible = False
    End Sub

    '  [TDATE]
    '    ,[LineNumber]
    '    ,[TotalMachine]
    '    ,[MachineTarget]
    '    ,[TotalTarget]
    'FROM [ProdCost].[dbo].[STTarget]

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If chkreward.Checked = True Then
            Call savereward()
        Else
            Call saverec()
        End If

    End Sub

    Private Sub saverec()

        'sql1 = "delete from prodcost.dbo.sttarget where tdate='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"
        sql1 = "delete from prodcost.dbo.sttarget  where nmonth=" & Val(Format(CDate(mskdate.Text), "MM")) & " and yr=" & Val(Format(CDate(mskdate.Text), "yyyy"))





        'msql = "insert into prodcost.dbo.sttarget (tdate,LineNumber,TotalMachine,MachineTarget,TotalTarget) values('" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'" _
        '       & "," & Val(DG.Rows(i).Cells(0).Value) & "," & Val(DG.Rows(i).Cells(1).Value) & "," & Val(DG.Rows(i).Cells(2).Value) & "," & Val(DG.Rows(i).Cells(3).Value) & ")"


        If con.State = ConnectionState.Closed Then
            con.Open()
        End If


        trans1 = con.BeginTransaction
        Try
            Dim cmdd As New SqlCommand(sql1, con, trans1)
            cmdd.ExecuteNonQuery()



            For i = 0 To DG.Rows.Count - 1

                'msql = "insert into prodcost.dbo.sttarget (tdate,LineNumber,TotalMachine,MachineTarget,TotalTarget) values('" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'" _
                '    & "," & Val(DG.Rows(i).Cells(0).Value) & "," & Val(DG.Rows(i).Cells(1).Value) & "," & Val(DG.Rows(i).Cells(2).Value) & "," & Val(DG.Rows(i).Cells(3).Value) & ")"

                msql = "insert into prodcost.dbo.sttarget (tdate,LineNumber,TotalMachine,TotalTarget,montarget,cmonth,nmonth,yr) values('" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'" _
                                  & "," & Val(DG.Rows(i).Cells(0).Value) & "," & Val(DG.Rows(i).Cells(1).Value) & "," & Val(DG.Rows(i).Cells(2).Value) & "," & Val(DG.Rows(i).Cells(3).Value) & "," _
                                  & "'" & Format(CDate(mskdate.Text), "MMM") & "'," & Val(Format(CDate(mskdate.Text), "MM")) & "," & Val(Format(CDate(mskdate.Text), "yyyy")) & ")"



                Dim cmd As New SqlCommand(msql, con, trans1)
                cmd.ExecuteNonQuery()
            Next i

            trans1.Commit()
            MsgBox("Saved!")

        Catch ex As Exception
            'If InStr(merr, "PRIMARY KEY") > 0 Then

            'End If
            'merr = Trim(ex.Message)
            trans1.Rollback()
            MsgBox(ex.Message)
        End Try




    End Sub

    Private Sub savereward()


        sql1 = "delete from prodcost.dbo.empreward  where nmonth=" & Val(Format(CDate(mskdate.Text), "MM")) & " and ryear=" & Val(Format(CDate(mskdate.Text), "yyyy"))

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim nmon, myr As Integer
        Dim monname As String
        nmon = Format(CDate(mskdate.Text), "MM")
        monname = Format(CDate(mskdate.Text), "MMMM")
        myr = Format(CDate(mskdate.Text), "yyyy")

        trans1 = con.BeginTransaction
        Try
            Dim cmdd As New SqlCommand(sql1, con, trans1)
            cmdd.ExecuteNonQuery()



            For i = 0 To dgr.Rows.Count - 1

                'msql = "insert into prodcost.dbo.sttarget (tdate,LineNumber,TotalMachine,MachineTarget,TotalTarget) values('" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'" _
                '    & "," & Val(DG.Rows(i).Cells(0).Value) & "," & Val(DG.Rows(i).Cells(1).Value) & "," & Val(DG.Rows(i).Cells(2).Value) & "," & Val(DG.Rows(i).Cells(3).Value) & ")"
                'RMonth	Ryear	Nmonth	nEmpno	Empno	Rewardamt	RClass	Rmsg	Active
                msql = "insert into prodcost.dbo.empreward (RMonth,Ryear,Nmonth,nEmpno,Rewardamt,RClass,Active,sno,operation) values('" & monname & "'," & myr & "," & nmon & "" _
                                  & "," & Val(dgr.Rows(i).Cells(0).Value) & "," & Val(dgr.Rows(i).Cells(1).Value) & ",'" & dgr.Rows(i).Cells(2).Value & "','Y'," & (i + 1) & ",'" & dgr.Rows(i).Cells(3).Value.ToString() & vbNullString & "'" & ")"

                Dim cmd As New SqlCommand(msql, con, trans1)
                cmd.ExecuteNonQuery()
            Next i

            trans1.Commit()
            MsgBox("Saved!")

        Catch ex As Exception
            'If InStr(merr, "PRIMARY KEY") > 0 Then

            'End If
            'merr = Trim(ex.Message)
            trans1.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub loaddata()
        DG.Rows.Clear()
        'sql1 = "select TDATE,LineNumber,TotalMachine,MachineTarget,TotalTarget FROM ProdCost.dbo.STTarget where tdate='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'"
        sql1 = "select TDATE,LineNumber,TotalMachine,TotalTarget,montarget,cmonth,nmonth,yr FROM ProdCost.dbo.STTarget where nmonth=" & Val(Format(CDate(mskdate.Text), "MM")) & " and yr=" & Val(Format(CDate(mskdate.Text), "yyyy"))
        Dim dtt As DataTable = getDataTable(sql1)
        If dtt.Rows.Count > 0 Then

            For Each rw As DataRow In dtt.Rows
                n = DG.Rows.Add
                DG.Rows(n).Cells(0).Value = rw("linenumber")
                DG.Rows(n).Cells(1).Value = rw("totalmachine")
                DG.Rows(n).Cells(2).Value = rw("totaltarget")
                DG.Rows(n).Cells(3).Value = rw("montarget")
            Next
        End If
    End Sub

    Private Sub mskdate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mskdate.KeyPress
        If Asc(e.KeyChar) = 13 Then
            BtnDisplay.Focus()
        End If
    End Sub

    Private Sub mskdate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles mskdate.LostFocus
        Call loaddata()
    End Sub

    Private Sub mskdate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskdate.MaskInputRejected

    End Sub

    Private Sub DG_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DG.CellContentClick
        'isLastRow = e.RowIndex

    End Sub

    Private Sub DG_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DG.CellDoubleClick

    End Sub

    Private Sub DG_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DG.CellEndEdit
        ''DG.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange)
        'If Val(DG.Rows(e.RowIndex).Cells(1).Value) > 0 And Val(DG.Rows(e.RowIndex).Cells(2).Value) > 0 Then
        '    DG.Rows(e.RowIndex).Cells(3).Value = Val(DG.Rows(e.RowIndex).Cells(1).Value) * Val(DG.Rows(e.RowIndex).Cells(2).Value)
        '    'DG.CurrentCell(+1)
        '    'Dim col As Integer = DG.CurrentCell.ColumnIndex
        '    'Dim currentcell = DG.CurrentRow.Cells(col)
        '    'DG.CurrentCell = currentcell
        'End If
        ''Dim cell As Windows.Forms.DataGridViewCell = DG.Rows(e.RowIndex).Cells(e.ColumnIndex)
        ''DG.CurrentCell = cell
        ''DG.BeginEdit(True)

    End Sub

    Private Sub DG_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DG.GotFocus
        If (DG.Rows.Count - 1) = -1 Then
            n = DG.Rows.Add()
            Dim cell As System.Windows.Forms.DataGridViewCell = DG.Rows(n).Cells(0)
            DG.CurrentCell = cell
            DG.BeginEdit(True)

            'DG.Rows(n).Selected = True
            'DG.CurrentCell = DG.Rows(n).Cells(0)
            'DG.BeginEdit(True)


            ' DG.BeginEdit(True)
        End If
    End Sub

   

    Private Sub DG_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DG.KeyDown
       
        If e.KeyCode = Keys.F10 Then
            n = DG.Rows.Add()
            'Dim cell As Windows.Forms.DataGridViewCell = DG.Rows(n).Cells(0)
            DG.CurrentCell = DG.Rows(n).Cells(0)
            DG.Focus()
            e.Handled = True
            DG.BeginEdit(True)


            'DG.Rows(n).Selected = True
            'DG.CurrentCell = DG.Rows(n).Cells(0)
            'DG.BeginEdit(True)

            'DG.BeginEdit(True)
            'DG.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange)
        End If
        If e.KeyCode = Keys.F12 Then
            DG.Rows.RemoveAt(DG.CurrentRow.Index)
        End If


        If e.KeyCode = Keys.Enter Then

            If DG.CurrentRow.Index < DG.RowCount - 1 AndAlso DG.CurrentCell.ColumnIndex = DG.ColumnCount - 1 Then
                DG.CurrentCell = DG(0, DG.CurrentRow.Index + 1)
            Else
                Dim CurrentCell As DataGridViewCell = DG.CurrentCell
                Dim col As Integer = CurrentCell.ColumnIndex

                col = (col + 1) Mod DG.Columns.Count

                CurrentCell = DG.CurrentRow.Cells(col)
                DG.CurrentCell = CurrentCell
                'DG.Focus()
                'DG.BeginEdit(True)

            End If
            'DG.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange)
            e.Handled = True
            'DG.BeginEdit(True)

        End If

    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDisplay.Click
        'MsgBox(Format(CDate(mskdate.Text), "MMM"))
    End Sub

    Private Sub chkreward_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkreward.CheckedChanged
        If chkreward.Checked = True Then
            If DG.Visible = True Then DG.Visible = False
            If dgr.Visible = False Then dgr.Visible = True
            dgr.Rows.Add()
        Else
            If DG.Visible = False Then DG.Visible = True
            If dgr.Visible = True Then dgr.Visible = False
        End If
    End Sub

    Private Sub dgr_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgr.CellContentClick

    End Sub

    Private Sub dgr_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgr.KeyDown
        If e.Control Then
            Select Case e.KeyCode
                Case Keys.F5
                    dgr.Rows.Add()
                Case Keys.C
                    'CopyCells()
                    e.Handled = True
                Case Keys.V
                    PasteCells(dgr)
                    dgr.Rows.RemoveAt(dgr.Rows.Count - 1)

                   
                    e.Handled = True
            End Select
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If chkreward.Checked = True Then
            'msql = "update prodcost.dbo.empreward where nmonth=" & Val(Format(CDate(mskdate.Text), "MM")) & " and ryear=" & Val(Format(CDate(mskdate.Text), "yyyy"))
            msql = "update prodcost.dbo.empreward set active='N' where active='Y'"
            Try
                executeQuery(msql)
                MsgBox("DeActivated..")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If chkreward.Checked = True Then
            msql = "update prodcost.dbo.empreward set active='Y' where nmonth=" & Val(Format(CDate(mskdate.Text), "MM")) & " and ryear=" & Val(Format(CDate(mskdate.Text), "yyyy"))
            'msql = "update prodcost.dbo.empreward set active='N' where active='Y'"
            Try
                executeQuery(msql)
                MsgBox("Activated..")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If
    End Sub
End Class
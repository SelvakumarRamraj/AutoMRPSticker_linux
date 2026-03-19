Imports System
Imports System.Data
Imports System.Data.OleDb
'Imports System.Data.SqlClient
Imports System.Windows.Forms.DataGridView

Public Class FrmBOM
    Dim row As Integer
    Dim col As Integer
    Dim currentTime As System.DateTime = System.DateTime.Now
    Dim DS As New DataSet()
    'Dim com As New OleDb.OleDbCommandBuilder(OleDbDataAdapter1)
    Dim SQLString As String = "SELECT * FROM BARCODEBOM"
    Dim DA As System.Data.OleDb.OleDbDataAdapter = New System.Data.OleDb.OleDbDataAdapter(SQLString, con)

    Private Sub FrmBOM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width


        DA.Fill(DS, "BARCODEBOM")
        DataGridView1.DataSource = DS.Tables("BARCODEBOM")
        'TextBox1.Text = currentTime
    End Sub

    Private Sub CMDADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDADD.Click
        ''DataSet1.Tables("BARCODEBOM").AcceptChanges()
        ''OleDbDataAdapter1.Update(DataSet1, "BARCODEBOM")
        ''Dim item As New DataGridViewRow
        ''DataGridView1.AllowUserToAddRows = True
        ''item.CreateCells(DataGridView1)
        'Dim cmd As OleDbCommandBuilder
        'Dim changes As DataSet
        'cmd = New OleDbCommandBuilder(da)
        'changes = ds.GetChanges()
        'If changes IsNot Nothing Then
        '    da.Update(ds.Tables(0))
        'End If
        'DS.AcceptChanges()
        'DataGridView1.Refresh()
        Dim j As Int32

        'datagridview1.Rows(j).Cells(0).Value=true then

        j = 0
        ' DS.Tables("BARCODEBOM").Rows(j).Item(1) = TextBox2.Text
        For j = 1 To (DataGridView1.RowCount - 1)
            If DataGridView1.Rows(j).Selected = True Then

                Try

                    Dim str2 As String
                    'str2 = "update barcodebom set pcs=" & DataGridView1.SelectedCells(5).Value & " where code = '" + DataGridView1.SelectedCells(0).Value + "'"
                    str2 = "insert into barcodebom (code,dhoti,shirting,suiting,towel,pcs) values('" & DataGridView1.Rows(j).Cells(0).Value & "','" & DataGridView1.Rows(j).Cells(1).Value & "','" & DataGridView1.Rows(j).Cells(2).Value & "','" & DataGridView1.Rows(j).Cells(3).Value & "','" & DataGridView1.Rows(j).Cells(4).Value & "'," & DataGridView1.Rows(j).Cells(5).Value & ")"
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If
                    Dim str3 As New OleDbCommand(str2, con)
                    str3.ExecuteNonQuery()
                    con.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Next j
        MsgBox("Added!")
        cmdclear_Click(sender, New System.EventArgs())

    End Sub

    Private Sub CMDUPDT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDUPDT.Click
        DA.AcceptChangesDuringUpdate = True
        'OleDbDataAdapter1.UpdateCommand("table1")
        Dim j As Integer
        j = 0
        ' DS.Tables("BARCODEBOM").Rows(j).Item(1) = TextBox2.Text
        For j = 1 To (DataGridView1.RowCount - 1)
            If DataGridView1.Rows(j).Selected = True Then
                Try

                    Dim str2 As String
                    'str2 = "update barcodebom set pcs=" & DataGridView1.SelectedCells(5).Value & " where code = '" + DataGridView1.SelectedCells(0).Value + "'"
                    str2 = "update barcodebom set pcs=" & DataGridView1.Rows(j).Cells(5).Value & " where code = '" + DataGridView1.Rows(j).Cells(0).Value + "'"
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If
                    Dim str3 As New OleDbCommand(str2, con)
                    str3.ExecuteNonQuery()
                    'con.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Next j
        MsgBox("Updated!")
        cmdclear_Click(sender, New System.EventArgs())

    End Sub

    Private Sub CMDDEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMDDEL.Click
        'Dim i As Integer
        'i = 0
        'DS.Tables("BARCODEBOM").Rows(i).Delete()

        'Try
        '    Dim str2 As String
        '    str2 = "delete from barcodebom where code = '" + DataGridView1.SelectedCells(0).Value + "'"
        '    con.Open()
        '    Dim str3 As New OleDbCommand(str2, con)
        '    str3.ExecuteNonQuery()
        '    con.Close()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        Dim j As Integer
        j = 0
        ' DS.Tables("BARCODEBOM").Rows(j).Item(1) = TextBox2.Text
        For j = 1 To (DataGridView1.RowCount - 1)
            If DataGridView1.Rows(j).Selected = True Then
                Try

                    Dim str2 As String
                    'str2 = "update barcodebom set pcs=" & DataGridView1.SelectedCells(5).Value & " where code = '" + DataGridView1.SelectedCells(0).Value + "'"
                    str2 = "DELETE FROM Barcodebom where code='" & DataGridView1.Rows(j).Cells(0).Value & "'"
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If
                    Dim str3 As New OleDbCommand(str2, con)
                    str3.ExecuteNonQuery()
                    'con.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Next j
        MsgBox("Deleted!")
        cmdclear_Click(sender, New System.EventArgs())



    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        'Dim SQLString As String = "SELECT * FROM BARCODEBOM"
        'Dim DA As System.Data.OleDb.OleDbDataAdapter = New System.Data.OleDb.OleDbDataAdapter(SQLString, con)
        DS.Clear()
        'DataGridView1.Rows.Clear()
        'Me.DataGridView1.RowCount = 0
        'DataGridView1.Rows.Add(1)
        'DataGridView1.DataSource = Nothing
        DA.Fill(DS, "BARCODEBOM")
        DataGridView1.DataSource = DS.Tables("BARCODEBOM")
        DataGridView1.Refresh()
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub
End Class
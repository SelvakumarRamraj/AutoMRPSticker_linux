Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class Frmstylemaster

    Dim msql, msql2, merr, qry, qry1 As String
    Dim i, k, j, msel, msel1 As Int16
    Dim o_id
    Dim e_id
    Dim flag As Boolean
    Dim icol As Int32
    Private transd As SqlTransaction
    Dim lastkey As String

    Private Sub Frmstylemaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width

        Call loaddata()
    End Sub

    Private Sub loaddata()
        msql = "select style from " & Trim(mcostdbnam) & ".dbo.stylemaster"

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
        dg.Columns(0).Width = 100




        dg.Columns(0).Width = 250
        dg.Columns(0).ReadOnly = True
        'dg.Columns(1).ReadOnly = True
        'dg.Columns(2).ReadOnly = True
        'dg.Columns(3).ReadOnly = True
        'dg.Columns(4).ReadOnly = True

    End Sub

    
  

    Private Sub dg_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dg.CellMouseDoubleClick
        If e.RowIndex > 0 AndAlso e.ColumnIndex > 0 Then
            txtstyle.Text = dg.Rows(e.RowIndex).Cells(0).Value

        End If
    End Sub

    Private Sub butsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butsave.Click
        'If msel = 1 Then
        'For Each row As DataGridViewRow In dgo.Rows
        Dim strsql As String = "select * from " & Trim(mcostdbnam) & ".dbo.stylemaster where style='" & Trim(txtstyle.Text) & "'"
        If dataexists(strsql) = False Then

            If Len(Trim(txtstyle.Text)) > 0 Then
                msql = "insert into " & Trim(mcostdbnam) & ".dbo.stylemaster(style)" & vbCrLf _
                & " Values ('" & Trim(txtstyle.Text) & "'" & ")"
                Try
                    'If Len(Trim(row.Cells(0).Value)) > 0 Then
                    executeQuery(msql)
                    'End If
                    'Next
                    MsgBox("Saved!")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
            txtstyle.Text = ""

            Call loaddata()
        Else
            MsgBox("Already Exists!")
        End If

        
    End Sub

    Private Sub butcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butcancel.Click
        txtstyle.Text = ""

    End Sub

    Private Sub butexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butexit.Click
        Me.Close()
    End Sub

    Private Sub butdel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butdel.Click

        If Len(Trim(txtstyle.Text)) > 0 Then
            If MsgBox("Delete the data!", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                msql = "delete from " & Trim(mcostdbnam) & ".dbo.stylemaster where style='" & Trim(txtstyle.Text) & "'"

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
        txtstyle.Text = ""
        Call loaddata()
    End Sub
End Class
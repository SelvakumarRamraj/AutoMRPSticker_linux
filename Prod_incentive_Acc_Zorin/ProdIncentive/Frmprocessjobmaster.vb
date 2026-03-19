Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class Frmprocessjobmaster
    Dim msql, msql2, merr, qry, qry1 As String
    Dim i, k, j, msel, msel1 As Int16
    Dim o_id
    Dim e_id
    Dim flag As Boolean
    Dim icol As Int32
    Private transd As OleDbTransaction
    Dim lastkey As String
    Private Sub Frmprocessjobmaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
    End Sub

    Private Sub butsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butsave.Click

    End Sub
End Class
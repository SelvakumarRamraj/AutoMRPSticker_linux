Imports System.Data.SqlClient
Imports System.Data.OleDb

Module connection
    Public merr, merr2 As String
    Public con As SqlConnection
    Public con2 As OleDb.OleDbConnection
    Public trans As SqlTransaction
    Public mbrandgrp As String = String.Empty

    Public Sub createxcelcon(ByVal mexcelfilename As String)
        con2 = New OleDbConnection
        'con2.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\myFolder\myOldExcelFile.xls;Extended Properties="Excel 8.0;HDR=YES";""""
        con2.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" &
                                    "Data Source=" & mexcelfilename & ";" &
                                    "Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1;"""



        '"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\excel\\ExcelImport_8-2009.xlsx;Extended Properties=\"Excel 12.0 Xml;HDR=NO;IMEX=1;\""

        con2.Open()

    End Sub

    Public Sub createxcelcon2(ByVal mexcelfilename As String)
        con2 = New OleDbConnection
        'con2.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\myFolder\myOldExcelFile.xls;Extended Properties="Excel 8.0;HDR=YES";""""

        'If xlsxformat = "Y" Then
        '    con2.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" &
        '                            "Data Source=" & mexcelfilename & ";" &
        '                            "Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1;"""


        'Else
        con2.ConnectionString = "Provider= Microsoft.Jet.OLEDB.4.0;" &
                                 "Data Source=" & mexcelfilename & ";" &
                                 "Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;"""
        'End If


        '"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\excel\\ExcelImport_8-2009.xlsx;Extended Properties=\"Excel 12.0 Xml;HDR=NO;IMEX=1;\""

        con2.Open()

    End Sub

    'Public Sub chkexcelcon()
    '    If con Is Nothing OrElse con2.State = ConnectionState.Closed Then
    '        createxcelcon()
    '    End If
    'End Sub

    Public Sub createConnection()

        con = New SqlConnection
        'con.ConnectionString = "Data Source=edp-selva-pc\sqlexpress;Initial Catalog=vinhr;Integrated Security=true;"

        con.ConnectionString = " Server=" & mdbserver & ";Database=" & mdbname & ";User Id=" & mdbuserid & "; Password=" & mdbpwd & ";"

        ' con.ConnectionString = " Server=edp-selva-pc\sqlexpress;Database=vinhr;User Id=sa; Password=sa@536;"

        ' con.ConnectionString = "Data Source=selvalaptop;Initial Catalog=payroll;Integrated Security=true;"
        con.Open()

    End Sub

    Public Sub checkConnection()

        If con Is Nothing OrElse con.State = ConnectionState.Closed Then
            createConnection()
        End If

    End Sub

    Public Function getDataReader(ByVal SQL As String) As SqlDataReader

        checkConnection()
        Dim cmd As New SqlCommand(SQL, con)
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        Return dr

    End Function

    Public Function getDataTable(ByVal SQL As String) As DataTable

        checkConnection()
        Dim cmd As New SqlCommand(SQL, con)
        cmd.CommandTimeout = 600
        Dim table As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(table)
        Return table

    End Function

    Public Sub executeQuery(ByVal SQL As String)
        merr = ""
        checkConnection()


        trans = con.BeginTransaction

        Dim cmd As New SqlCommand(SQL, con, trans)
        cmd.CommandTimeout = 600

        Try
            cmd.ExecuteNonQuery()
            trans.Commit()
            merr2 = "Saved!"
        Catch ex As Exception
            'If InStr(merr, "PRIMARY KEY") > 0 Then

            'End If
            merr = Trim(ex.Message)
            trans.Rollback()
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Function executescalarQuery(ByVal SQL As String) As String

        checkConnection()


        trans = con.BeginTransaction

        Dim cmd As New SqlCommand(SQL, con, trans)

        Try
            Dim strval As String = String.Empty
            Dim oJobName As Object = cmd.ExecuteScalar()

            If oJobName Is Nothing Then
                'Do something with the error condition
            Else
                strval = Replace(oJobName.ToString, "-", "").ToUpper
            End If


            'Dim strval As String = Replace(cmd.ExecuteScalar().ToString, "-", "").ToUpper
            'cmd.ExecuteNonQuery()
            trans.Commit()
            executescalarQuery = strval
        Catch ex As Exception
            'If InStr(merr, "PRIMARY KEY") > 0 Then
            'Dim strval2 As String = cmd.ExecuteScalar().ToString.ToUpper
            'End If
            merr = Trim(ex.Message)
            trans.Rollback()
            If InStr(merr, "Invalid object") > 0 Then

            Else
                MsgBox(ex.Message)
            End If
            'MsgBox(ex.Message)
        End Try
        'Return strval
    End Function


    'Public Sub disableRights(ByRef btn1 As CloudToolkitN6.CloudButton, ByRef btn2 As CloudToolkitN6.CloudButton, ByRef btn3 As CloudToolkitN6.CloudButton, ByRef btn4 As CloudToolkitN6.CloudButton, ByRef menu As MenuStrip)

    '    btn1.Enabled = False
    '    btn2.Enabled = False
    '    btn3.Enabled = False
    '    btn4.Enabled = False
    '    menu.Enabled = False

    'End Sub


    'Public Sub enableRights(ByRef btn1 As CloudToolkitN6.CloudButton, ByRef btn2 As CloudToolkitN6.CloudButton, ByRef btn3 As CloudToolkitN6.CloudButton, ByRef btn4 As CloudToolkitN6.CloudButton, ByRef menu As MenuStrip)

    '    btn1.Enabled = True
    '    btn2.Enabled = True
    '    btn3.Enabled = True
    '    btn4.Enabled = True
    '    menu.Enabled = True

    'End Sub

    Public Function GetDataSet(ByVal cmd As String) As DataTable
        Dim adp As New SqlDataAdapter
        Dim dt As New DataTable
        checkConnection()

        'If con.State = ConnectionState.Open Then MyConn.Close()
        'MyConn.Open()

        adp = New SqlDataAdapter(cmd, con)
        dt = New DataTable
        adp.Fill(dt)
        con.Close()
        Return dt
    End Function
    Public Function GenerateNewIDNumber(ByVal srcTable As String, ByVal srcField As String) As Double
        Dim dt As New DataTable
        dt = GetDataSet("SELECT MAX(" & srcField & ") FROM " & srcTable)

        If dt.Rows.Count > 0 Then
            GenerateNewIDNumber = toNumber(dt.Rows(0).Item(0).ToString()) + 1
        Else
            GenerateNewIDNumber = 1
        End If

    End Function

    Public Function toNumber(ByVal srcCurrency As String, Optional ByRef RetZeroIfNegative As Boolean = False) As Double
        Dim retValue As Double
        If srcCurrency = "" Then
            toNumber = 0
        Else
            If InStr(1, srcCurrency, ",") > 0 Then
                retValue = Val(Replace(srcCurrency, ",", "", , , CompareMethod.Text))
            Else
                retValue = Val(srcCurrency)
            End If
            If RetZeroIfNegative = True Then
                If retValue < 1 Then retValue = 0
            End If
            toNumber = retValue
            retValue = 0
        End If
    End Function

    Public Function sqlSafe(ByRef p_string As String) As String
        sqlSafe = Replace(Trim(p_string), "'", "''")
    End Function
End Module



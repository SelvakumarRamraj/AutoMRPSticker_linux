Imports System.IO
Imports System.Data
'Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports Microsoft.VisualBasic
Public Class Frmlogin

    Private Sub CButton1_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles cmdlogin.ClickButtonArea
        'txtloginname.Text = findid()
        Call chklogin()
    End Sub

    Private Sub Frmlogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mkserver = "192.168.0.5"
        constr = "Provider=SQLOLEDB;Data Source=" & Trim(mkserver) & ";Persist Security Info=True;Password=RamRajsA@536;User ID=ramraj;Initial Catalog=SBO-COMMON"
        con2 = New OleDb.OleDbConnection(constr)

        'main()
        Call LOADcmpNAME()
        'Call LOADcmpyr()
        ' txtloginname.Focus()
    End Sub

    Private Sub CButton3_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles CButton3.ClickButtonArea
        End
    End Sub

    Private Sub txtloginname_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtloginname.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtpasswd.Focus()
        End If
    End Sub

    Private Sub txtloginname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtloginname.TextChanged

    End Sub
    Private Sub LOADcmpNAME()
        'Dim CMD As New SqlClient.SqlCommand("SELECT LOGINNAME FROM LOGINMAST GROUP BY LOGINNAME ORDER BY LOGINNAME", con)
        Dim CMD As New OleDb.OleDbCommand("SELECT compident as compname FROM slsp where comptype='CompanyDB' GROUP BY compident ORDER BY compident", con)

        If con2.State = ConnectionState.Closed Then
            con2.Open()
        End If
        'Dim DR As SqlDataReader
        Dim DR As OleDb.OleDbDataReader

        DR = CMD.ExecuteReader
        If DR.HasRows = True Then
            cmbcompname.Items.Clear()
            While DR.Read
                cmbcompname.Items.Add(DR.Item("compname"))
            End While
        End If
        DR.Close()
        CMD.Dispose()
        con2.Close()

    End Sub

    'Private Sub LOADcmpyr()
    '    'Dim CMD As New SqlClient.SqlCommand("SELECT LOGINNAME FROM LOGINMAST GROUP BY LOGINNAME ORDER BY LOGINNAME", con)
    '    Dim CMD As New OleDb.OleDbCommand("SELECT cmpyrname FROM cmpyrmaster GROUP BY cmpyrname ORDER BY cmpyrname", con)

    '    If con.State = ConnectionState.Closed Then
    '        con.Open()
    '    End If
    '    'Dim DR As SqlDataReader
    '    Dim DR As OleDb.OleDbDataReader

    '    DR = CMD.ExecuteReader
    '    If DR.HasRows = True Then
    '        cmbyear.Items.Clear()
    '        While DR.Read
    '            cmbyear.Items.Add(DR.Item("cmpyrname"))
    '        End While
    '    End If
    '    DR.Close()
    '    CMD.Dispose()
    '    con.Close()

    'End Sub
    Private Sub chklogin()
        'Dim CMD As New OleDbCommand("SELECT login_id,loginname,usertype* FROM LOGINMASTer WHERE RTRIM(LOGINNAME)='" & Trim(txtloginname.Text) & "' AND RTRIM(PASS)='" & Encript(Trim(txtpasswd.Text)) & "'", con)
        Dim CMD As New OleDbCommand("SELECT login_id,loginname,usertype FROM LOGINMASTer WHERE RTRIM(LOGINNAME)='" & Trim(txtloginname.Text) & "' AND RTRIM(PASSWD)='" & Trim(hashcode(Trim(txtpasswd.Text))) & "'", con)


        Try
            If con.State = ConnectionState.Closed Then

                con.Open()
            End If
        
            Dim DR As OleDb.OleDbDataReader
            DR = CMD.ExecuteReader

            If DR.HasRows = True Then
                While DR.Read
                    muser = DR.Item("Loginname")
                    musetyp = DR.Item("usertype")
        
                End While
        
                Me.Hide()

                mcmpid = getid("cmpmaster", "cmp_id", "cmpname", cmbcompname.Text)
                mcmpyrid = getid("cmpyrmaster", "cmpyr_id", "cmpyrname", cmbyear.Text)
                mcmpname = cmbcompname.Text
                mcmpyr = cmbyear.Text

                'MsgBox(mcmpid & "      " & mcmpyrid)


                MDIForm1.Show()
                'frmmdiform.Show()

                'MsgBox("Login Successfully!")

            Else
                MsgBox("Login Failed!")
            End If
            con.Close()
        Catch EX As Exception
            If InStr(EX.Message, "connection attempt failed") > 0 Then
                MsgBox("Connection Attempt Failed! Configure Connection String")
                con.Close()
            Else
                '*
                ' MsgBox(EX.Message())
                'If con.State = ConnectionState.Closed Then
                '    'MsgBox("OPENING...")
                '    con.Open()
                'End If
                'If con.State = ConnectionState.Open Then
                '    MsgBox("connected!")
                '    mcmpid = getid("cmpmaster", "cmp_id", "cmpname", cmbcompname.Text)
                '    mcmpyrid = getid("cmpyrmaster", "cmpyr_id", "cmpyrname", cmbyear.Text)
                '    MsgBox(mcmpid & "      " & mcmpyrid)


                '    'con.Close()
                'Else
                '    MsgBox("not connected!")
                'End If
                MsgBox(EX.Message)
            End If
            ' MsgBox(EX.Message())
            ' con.Close()

        End Try



    End Sub

    Private Sub txtpasswd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpasswd.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbcompname.Focus()
        End If
    End Sub

    Private Sub txtpasswd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpasswd.TextChanged

    End Sub

    Private Sub cmbcompname_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbcompname.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbyear.Focus()
        End If
    End Sub

    Private Sub cmbcompname_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcompname.SelectedIndexChanged

    End Sub

    Private Sub cmbyear_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbyear.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Call chklogin()
            'cmdlogin.Focus()
        End If
    End Sub

    Private Sub cmbyear_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbyear.SelectedIndexChanged

    End Sub

    Private Sub cmdlogin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmdlogin.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Call chklogin()
        End If
    End Sub

    Private Sub cmbcompname_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcompname.TextChanged
        Call autosearch(sender)
    End Sub

    Private Sub cmbyear_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbyear.TextChanged
        Call autosearch(sender)
    End Sub
End Class

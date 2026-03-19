Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class Frmempmaster
    Dim msql, msql2, merr, qry, qry1 As String
    Dim n, i, k, j, msel, msel1 As Integer
    Dim o_id
    Dim e_id
    Dim flag As Boolean
    Dim icol As Int32
    Private transd As SqlTransaction
    Dim lastkey As String
    Private Sub Frmempmaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        chkloc.Checked = True
        cmbprocess.Items.Add("CUTTING")
        cmbprocess.Items.Add("FUSHING")
        cmbprocess.Items.Add("EMB")
        cmbprocess.Items.Add("STITCHING")
        cmbprocess.Items.Add("CHECKING")
        cmbprocess.Items.Add("IRONING")

    End Sub
    Private Sub loaddata()

        If Len(Trim(cmbprocess.Text)) = 0 And optIHC.Checked = False And optIHP.Checked = False And optIHI.Checked = False Then
            If chkloc.Checked = True Then

                'msql = "select * from (select nemp_id,empno nempno,empname vname,isnull(gender,'') gender,division,branch,department,designation,category,salary  from " & Trim(mcostdbnam) & ".dbo.exempmaster) c" & vbCrLf _
                ' & " where not exists( select * from " & Trim(mcostdbnam) & ".dbo.empmaster b with (nolock)  where b.nempno=c.nempno) "

                'msql = "   select * from (select nemp_id,nempno,vname,''gender,'' division,'' branch,vdepartment department,vdesignation designation,vcategory category,linno [lineno],'STITCHING' processname,totsalary  from rrcolor.dbo.empmaster where cdepartment like '%STITCHING%' or cdepartment like  '%ST HELPER%') c" & vbCrLf _
                '                 & "where not exists( select * from prodcost.dbo.empmaster b with (nolock)  where b.nempno=c.nempno) "

                msql = "   select * from (select nemp_id,nempno,vname,''gender,'' division,'' branch,vdepartment department,vdesignation designation,vcategory category,linno [lineno],'STITCHING' processname,totsalary  from rrcolor.dbo.empmaster " & vbCrLf _
                       & "  where  isnull(Prdbranch,'')='IHP' or (cdepartment like '%STITCHING%' or cdepartment like  '%ST HELPER%')  or isnull(subdept,'')='STITCHING'  and  isnull(active,'')<>'N') c" & vbCrLf _
                                 & "where not exists( select * from prodcost.dbo.empmaster b with (nolock)  where b.nempno=c.nempno) "


            Else
                'msql = " select * from (SELECT em.nemp_id, b.[nempno],b.[vname],isnull(b.[Gender],'') gender,b.[Division],b.[Branch]" & vbCrLf _
                ' & ",b.[Department],b.[Designation],b.[Category] FROM hr.[eHR].[dbo].[veHREmps] b with (nolock) " & vbCrLf _
                ' & "left join hr.ehr.dbo.employee em with (nolock) on em.nempno=b.nempno " & vbCrLf _
                ' & "where b.division='ATITHYA CLOTHING COMPANY' ) c  " & vbCrLf _
                ' & "where not exists( select * from " & Trim(mcostdbnam) & ".dbo.empmaster b with (nolock) " & vbCrLf _
                ' & "where b.nempno=c.nempno) and c.department not in ('WAREHOUSE','WARE HOUSE','CANTEEN','FINISHING','ELECTRICAL'" & vbCrLf _
                ' & ",'HUMAN RESOURCE','CIVIL','SAMPLE','EDP','PUBLIC RELATION','DESPATCH','BOILER','HOUSE KEEPING','TRANSPORT','MACHINE MECHANISM','NURSE','SECURITY','ACCOUNTS','BOX FOLDING','LOADING','Store & Accessories','SOURCING')"
                msql = "select nemp_id,nempno,empname as vname,isnull(gender,'') gender,department,designation,category,[lineno] mline,isnull(processname,'') processname,jobgrade from " & Trim(mcostdbnam) & ".dbo.empmaster"
                '  where processname='" & Trim(cmbprocess.Text) & "'"

            End If

        Else
            If optIHC.Checked = True Then
                msql = "select nemp_id,nempno,empname as vname,isnull(gender,'') gender,department,designation,category,[lineno] mline,isnull(processname,'') processname,jobgrade from " & Trim(mcostdbnam) & ".dbo.empmaster where prdbranch='IHI"

                If Len(Trim(cmbprocess.Text)) > 0 Then
                    msql = msql & " or processname='" & Trim(cmbprocess.Text) & "'"
                End If

            ElseIf optIHP.Checked = True Then
                msql = "select nemp_id,nempno,empname as vname,isnull(gender,'') gender,department,designation,category,[lineno] mline,isnull(processname,'') processname,jobgrade from " & Trim(mcostdbnam) & ".dbo.empmaster where  prdbranch='IHP'"
                If Len(Trim(cmbprocess.Text)) > 0 Then
                    msql = msql & " or processname='" & Trim(cmbprocess.Text) & "'"
                End If
            ElseIf optIHI.Checked = True Then
                msql = "select nemp_id,nempno,empname as vname,isnull(gender,'') gender,department,designation,category,[lineno] mline,isnull(processname,'') processname,jobgrade from " & Trim(mcostdbnam) & ".dbo.empmaster where  prdbranch='IHI'"
                If Len(Trim(cmbprocess.Text)) > 0 Then
                    msql = msql & " or processname='" & Trim(cmbprocess.Text) & "'"
                End If
            Else
                msql = "select nemp_id,nempno,empname as vname,isnull(gender,'') gender,department,designation,category,[lineno] mline,isnull(processname,'') processname,jobgrade from " & Trim(mcostdbnam) & ".dbo.empmaster where  prdbranch='IHP'"
                If Len(Trim(cmbprocess.Text)) > 0 Then
                    msql = msql & " or processname='" & Trim(cmbprocess.Text) & "'"
                End If
            End If


        End If



            'Dim query As String

            Cursor = Cursors.WaitCursor
            dg.Rows.Clear()

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

        Dim cmd As New SqlCommand(msql, con)
        cmd.CommandTimeout = 600
            Dim dt As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)

            'Dim dt As DataTable = getDataTable(msql)

            For Each row As DataRow In dt.Rows

                n = dg.Rows.Add()
                dg.Rows(n).Cells(0).Value = row("nemp_id")
                dg.Rows(n).Cells(1).Value = row("nempno")
                dg.Rows(n).Cells(2).Value = row("vname")
                dg.Rows(n).Cells(3).Value = row("gender")
                dg.Rows(n).Cells(4).Value = row("Department")
                dg.Rows(n).Cells(5).Value = row("Designation")
                dg.Rows(n).Cells(6).Value = row("Category")
                If Len(Trim(cmbprocess.Text)) > 0 Then
                    If IsDBNull(row("mline")) = False Then
                        dg.Rows(n).Cells(7).Value = row("mline")
                    Else
                        dg.Rows(n).Cells(7).Value = "0"
                    End If

                    dg.Rows(n).Cells(8).Value = Trim(row("processname")) & vbNullString
                    dg.Rows(n).Cells(9).Value = row("jobgrade").ToString.Trim & vbNullString
                    'CType(dg.Rows(n).Cells(9), DataGridViewComboBoxCell).Value = row("jobgrade").ToString & vbNullString
                End If

            Next
            Cursor = Cursors.Default
    End Sub

    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.Click

        loaddata()
    End Sub

    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click
        'Dim strsql As String = "select * from " & Trim(mcostdbnam) & ".dbo.processjobmaster where opername='" & Trim(txtopername.Text) & "' and process='" & Trim(cmbdepartment.Text) & "'"
        'If dataexists(strsql) = False Then

        For Each row As DataGridViewRow In dg.Rows
            If Len(Trim(row.Cells(7).Value)) > 0 And Len(Trim(cmbprocess.Text)) = 0 Then
                msql = "insert into " & Trim(mcostdbnam) & ".dbo.empmaster(nemp_id,nempno,empname,gender,department,designation,category,[lineno],processname,jobgrade)" & vbCrLf _
                                    & " Values (" & Val(row.Cells(0).Value) & "," & Val(row.Cells(1).Value) & ",'" & Trim(row.Cells(2).Value) & "','" & Trim(row.Cells(3).Value) & vbNullString & "','" & Trim(row.Cells(4).Value) & "'" & vbCrLf _
                                    & ",'" & Trim(row.Cells(5).Value) & "','" & row.Cells(6).Value & "','" & Trim(row.Cells(7).Value) & "','" & Trim(row.Cells(8).Value) & vbNullString & "','" & Trim(row.Cells(9).Value) & vbNullString & "'" & ")"

                Try
                    executeQuery(msql)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            ElseIf Len(Trim(row.Cells(7).Value)) > 0 And Len(Trim(cmbprocess.Text)) > 0 Then
                If row.DefaultCellStyle.ForeColor = Color.Blue Then
                    msql = "update " & Trim(mcostdbnam) & ".dbo.empmaster set [lineno]='" & Trim(row.Cells(7).Value) & "',processname='" & Trim(row.Cells(8).Value) & "',jobgrade='" & Trim(row.Cells(9).Value) & "' where nemp_id= " & Val(row.Cells(0).Value)
                    'msql = "insert into " & Trim(mcostdbnam) & ".dbo.empmaster(nemp_id,nempno,empname,gender,department,designation,category,[lineno],processname)" & vbCrLf _
                    '                    & " Values (" & Val(row.Cells(0).Value) & "," & Val(row.Cells(1).Value) & ",'" & Trim(row.Cells(2).Value) & "','" & Trim(row.Cells(3).Value) & "','" & Trim(row.Cells(4).Value) & "'" & vbCrLf _
                    '                    & ",'" & Trim(row.Cells(5).Value) & "','" & row.Cells(6).Value & "','" & Trim(row.Cells(7).Value) & "','" & Trim(row.Cells(8).Value) & "'" & ")"

                    Try
                        executeQuery(msql)
                        row.DefaultCellStyle.ForeColor = Color.Black
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If

            End If



        Next
        'End If
        'Next
        MsgBox("Saved!")




        ' Call loaddataview()




    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick
        If e.ColumnIndex = 1 Then
            Dim OBJ As New FrmempCFL
            OBJ.ShowDialog()
            If Len(Trim(mempno)) > 0 Then

            End If
        End If
    End Sub

    Private Sub dg_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentDoubleClick
        'dg.SelectedRows(0).DefaultCellStyle.ForeColor = Color.Blue
        dg.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Blue
    End Sub

    Private Sub txtempid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtempid.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim temp As Integer = 0
            For i As Integer = 0 To dg.RowCount - 1
                'For j As Integer = 0 To gv.ColumnCount - 1
                If dg.Rows(i).Cells(1).Value = txtempid.Text Then
                    'MsgBox("Item found")
                    'dg.Rows(i).Cells(0).Value = "True"
                    dg.FirstDisplayedScrollingRowIndex = dg.Rows(i).Index

                    'dg.Refresh()
                    dg.Rows(i).Selected = True
                    dg.CurrentCell = dg.Rows(i).Cells(7)
                    dg.BeginEdit(False)
                    ' dg.BeginEdit = False
                    'dg.Item(mcol, x).Selected = True

                    'dg.Sort(dg.Columns(0), System.ComponentModel.ListSortDirection.Descending)
                    'dg.Item(0, i).Value = "True"
                    'txtno.Text = ""
                    temp = 1
                End If
                'Next
            Next
            If temp = 0 Then
                MsgBox("Item not found")
            End If
        End If
    End Sub

    Private Sub txtempid_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtempid.TextChanged

    End Sub

    Private Sub dg_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellEnter
        If e.RowIndex >= 0 Then
            dg.BeginEdit(False)
        End If
    End Sub

    Private Sub updatesalary()
        '        update exempmaster set nemp_id=d.nemp_id,gender=d.gender,division=d.division,branch=d.branch,designation=d.designation,category=d.category from exempmaster b,
        '(select b.nemp_id,c.nempno,c.vname,c.gender,c.division,c.branch,c.designation,c.department,c.category  from hr.ehr.dbo.vehremps c
        '        left join   hr.ehr.dbo.employee b on b.nempno=c.nempno
        ' where c.division='ATITHYA CLOTHING COMPANY') d
        '        where(d.nempno = b.empno)

        If Len(Trim(cmbprocess.Text)) = 0 Then
            If chkloc.Checked = True Then
                qry = " update " & Trim(mcostdbnam) & ".dbo.empmaster set salary=c.salary from " & Trim(mcostdbnam) & ".dbo.empmaster b, " & vbCrLf _
                  & "" & Trim(mcostdbnam) & ".dbo.exempmaster c " & vbCrLf _
                  & " where(b.nemp_id = c.nemp_id)"
            Else
                qry = " update " & Trim(mcostdbnam) & ".dbo.empmaster set salary=c.totsalary from " & Trim(mcostdbnam) & ".dbo.empmaster b, " & vbCrLf _
                  & " hr.ehr.dbo.esalaries c " & vbCrLf _
                  & " where(b.nemp_id = c.nemp_id)"
            End If
            Try
                executeQuery(qry)
                MsgBox("Updated Sucessfully!")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call updatesalary()
    End Sub

    Private Sub dg_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dg.DataError
        If (e.Context _
                 = (DataGridViewDataErrorContexts.Formatting Or DataGridViewDataErrorContexts.PreferredSize)) Then
            e.ThrowException = False
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        msql = "select empno,empname,department,doj,salary,division from " & Trim(mcostdbnam) & ".dbo.exempmaster"
        Dim dt As DataTable = getDataTable(msql)
        Dim filePath As String = "D:\test.json"
        WriteJason(dt, filePath)
    End Sub
    Private Sub newinsert()

        
        'msql = "insert into prodcost.dbo.empmaster (nemp_id,nempno,empname,department,designation,category,[lineno],processname,salary) " & vbCrLf _
        '     & " select * from (select nemp_id,nempno,vname as empname,vdepartment department,vdesignation designation,vcategory category,linno [lineno],'STITCHING' processname,totsalary salary from rrcolor.dbo.empmaster where cdepartment like '%STITCHING%' or cdepartment like  '%ST HELPER%' or cdepartment like '%QUALITY%') c" & vbCrLf _
        '     & " where not exists( select * from prodcost.dbo.empmaster b with (nolock)  where b.nempno=c.nempno) "


        'msql = "insert into prodcost.dbo.empmaster (nemp_id,nempno,empname,department,designation,category,[lineno],processname,salary) " & vbCrLf _
        '     & " select * from (select nemp_id,nempno,vname as empname,vdepartment department,vdesignation designation,vcategory category,linno [lineno],'STITCHING' processname,totsalary salary from rrcolor.dbo.empmaster " & vbCrLf _
        '     & "  where  isnull(Prdbranch,'')='IHP' or (cdepartment like '%STITCHING%' or cdepartment like  '%ST HELPER%' or cdepartment like '%QUALITY%') or subdept='STITCHING' and isnull(active,'')<>'N' ) c" & vbCrLf _
        '     & " where not exists( select * from prodcost.dbo.empmaster b with (nolock)  where b.nempno=c.nempno) "


        msql = "insert into prodcost.dbo.empmaster (nemp_id,nempno,empname,department,designation,category,[lineno],processname,salary) " & vbCrLf _
            & " select * from (select nemp_id,nempno,vname as empname,vdepartment department,vdesignation designation,vcategory category,linno [lineno],'STITCHING' processname,totsalary salary from rrcolor.dbo.empmaster " & vbCrLf _
            & "  where (isnull(subdept,'')='STITCHING' or vdepartment in ('TAILORING')) and isnull(active,'')<>'N' ) c" & vbCrLf _
            & " where not exists( select * from prodcost.dbo.empmaster b with (nolock)  where b.nempno=c.nempno) "



        Try
            executeQuery(msql)
            MsgBox("Added Sucessfully!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub cmbprocess_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbprocess.SelectedIndexChanged

    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        Cursor = Cursors.WaitCursor
        Call newinsert()
        Cursor = Cursors.Default

    End Sub

    Private Sub Butxlexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Butxlexport.Click

        gridexcelexport(dg, 1)
      
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Cursor = Cursors.WaitCursor
        msql = "UPDATE prodcost.dbo.empmaster set nempno=e.newempno from prodcost.dbo.empmaster d," & vbCrLf _
               & "(select b.nemp_id,b.nempno,b.empname,c.nempno newempno,c.vname from prodcost.dbo.empmaster b " & vbCrLf _
               & " inner join rrcolor.dbo.empmaster c on c.nemp_id=b.nemp_id " & vbCrLf _
               & " where   b.nempno <  12000000  and c.nempno>=12000000) e " & vbCrLf _
               & " where(d.nemp_id = e.nemp_id) "
        Try
            executeQuery(msql)
            MsgBox("Missed New EMp.No. updated!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        Cursor = Cursors.Default
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        chkloc.Checked = False
        optIHI.Checked = False
        optIHP.Checked = False
        optIHC.Checked = False
        dg.Rows.Clear()
    End Sub

    Private Sub Btnleft_Click(sender As System.Object, e As System.EventArgs) Handles Btnleft.Click
        Cursor = Cursors.WaitCursor
        msql = "update empmaster set active=c.active from empmaster b,rrcolor.dbo.empmaster c " _
            & " where b.nempno=c.nempno and b.active='Y' and c.active='N'"
        Try
            executeQuery(msql)
            MsgBox("Updated Left Emmployee!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class



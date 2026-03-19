Imports System.IO
Imports System.Math
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.VbStrConv
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports System.Data.SqlClient

Public Class Frmstockreport
    Dim arr() As Byte
    Dim k, j As Int32
    Private m_SelectedStyle As DataGridViewCellStyle
    Private m_SelectedRow As Integer = -1
    Dim msql As String
    Dim tmpstr As String
    Private Sub Frmstockreport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call loadwhs(cmbwhs)
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        Call dvhead()

        cmbwhs.Text = "SALGOODS"
    End Sub
    Private Sub dvhead()

        'DV.RowCount = 1
        'DV.ColumnCount = 3

        'Dim brand As New DataGridViewComboBoxColumn
        Dim mitemcode As New DataGridViewTextBoxColumn
        Dim mitemname As New DataGridViewTextBoxColumn
        Dim mstyle As New DataGridViewTextBoxColumn
        Dim msize As New DataGridViewTextBoxColumn
        Dim mcolcode As New DataGridViewTextBoxColumn
        Dim mcolname As New DataGridViewTextBoxColumn
        Dim mcolqty As New DataGridViewTextBoxColumn
        Dim mstkqty As New DataGridViewTextBoxColumn
        Dim mstkvalue As New DataGridViewTextBoxColumn
        Dim coltype As New DataGridViewTextBoxColumn
        Dim mskbrand As New DataGridViewTextBoxColumn
        Dim colimg As New DataGridViewImageColumn


        'Dim btn As New DataGridViewButtonColumn()

        Dim inImg As New DataGridViewImageCell()
        colimg.HeaderText = "Image"
        colimg.Name = "img"
        colimg.ImageLayout = DataGridViewImageCellLayout.Stretch

        'colorcode.ValueType = GetType(String)
        'colorcode.HeaderText = "Brand"



        'brand.ValueType = GetType(String)
        'brand.HeaderText = "Brand"

        mitemcode.ValueType = GetType(String)
        mitemcode.HeaderText = "Item Code"

        mitemname.ValueType = GetType(String)
        mitemname.HeaderText = "Item Name"

        mstyle.ValueType = GetType(String)
        mstyle.HeaderText = "Style"

        msize.ValueType = GetType(String)
        msize.HeaderText = "Size"

        mcolcode.ValueType = GetType(String)
        mcolcode.HeaderText = "Col.Code"

        mcolname.ValueType = GetType(String)
        mcolname.HeaderText = "Color Name"

        mcolqty.ValueType = GetType(Int32)
        mcolqty.HeaderText = "Col.Qty"

        mstkqty.ValueType = GetType(Int32)
        mstkqty.HeaderText = "Stk.Qty"

        mstkvalue.ValueType = GetType(Decimal)
        mstkvalue.HeaderText = "Stock.Value"

        coltype.ValueType = GetType(String)
        coltype.HeaderText = "Col.Type"

        mskbrand.ValueType = GetType(String)
        mskbrand.HeaderText = "Brand"





        'DataGridView1.Columns.Add(btn)
        'btn.HeaderText = "Select Image"
        'btn.Text = "Click Here"
        'btn.Name = "btn"
        'btn.UseColumnTextForButtonValue = True

        With dv
            'dv.Columns.Add(brand)
            dv.Columns.Add(mitemcode)
            dv.Columns.Add(mitemname)
            dv.Columns.Add(mstyle)
            dv.Columns.Add(msize)
            dv.Columns.Add(mcolcode)
            dv.Columns.Add(mcolname)
            dv.Columns.Add(mcolqty)
            dv.Columns.Add(mstkqty)
            dv.Columns.Add(mstkvalue)
            dv.Columns.Add(coltype)
            dv.Columns.Add(mskbrand)
            dv.Columns.Add(colimg)
            'dv.Columns.Add(coltype)

            'dvproof.Columns.Add(btn)
        End With
        dv.ColumnHeadersDefaultCellStyle.Font = New Font(dv.Font, FontStyle.Bold)
        dv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dv.Columns(0).Width = 70
        dv.Columns(1).Width = 150
        dv.Columns(2).Width = 50
        dv.Columns(3).Width = 50
        dv.Columns(4).Width = 70
        dv.Columns(5).Width = 100
        dv.Columns(6).Width = 70
        dv.Columns(7).Width = 70
        dv.Columns(8).Width = 100
        dv.Columns(9).Width = 70
        dv.Columns(10).Width = 1
        dv.Columns(11).Width = 70

        'Dim row As DataGridViewRow = dvproof.Rows(0)
        'row.Height = 25
        colimg.ImageLayout = DataGridViewImageCellLayout.Zoom
        'dvproof.ReadOnly = True
        dv.AllowUserToAddRows = False
        'dvproof.Rows.Add()

    End Sub
    Private Sub dv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dv.CellContentClick
        PictureBox2.Image = dv.Rows(e.RowIndex).Cells(11).Value
    End Sub
    Private Sub loadrep()


        msql = "select K.itemcode,k.itemname,k.u_style,k.u_size,k.batchnum,cc.colorname, k.batchqty, k.stkqty,k.stockvalue, k.warehouse,cc.colimage,cc.brand,cc.ctype,it.u_subgrp1 from " & vbCrLf _
            & "(Select  b.itemcode,c.itemname,c.u_style,c.u_size ,'' as batchnum,0 as batchqty, sum(b.inqty-b.outqty) as stkqty, sum(b.transvalue) as stockvalue, b.warehouse,1 as nno,c.u_subgroup from oinm b with (nolock)" & vbCrLf _
            & "left join oitm c with (nolock)  on c.itemcode=b.itemcode" & vbCrLf _
            & "where  b.docdate<='" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'" & vbCrLf _
            & "group by b.itemcode,c.itemname,c.u_style,u_size,b.warehouse,c.u_subgroup" & vbCrLf _
            & " union all" & vbCrLf _
            & "select  b.ItemCode,b.ItemName,c.U_Style,c.U_Size, replace(b.BatchNum,'-','') batchnum,SUM(b.Quantity) batchqty, 0 as stkqty,0 as stvalue,b.WhsCode,2 as nno,c.u_subgroup from OIBT b with (nolock)" & vbCrLf _
            & "left join OITM c with (nolock) on c.ItemCode=b.ItemCode" & vbCrLf _
            & "where b.indate<='" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "'" & vbCrLf _
            & "group by b.ItemCode,replace(b.BatchNum,'-',''),b.ItemName,b.WhsCode,c.U_Style,c.U_Size,c.u_subgroup" & vbCrLf _
            & "having  SUM(b.Quantity)>0 ) k" & vbCrLf _
            & "left join rrcolor.dbo.colormast cc with (nolock) on replace(CC.mktcode,'-','') collate SQL_Latin1_General_CP850_CI_AS =replace(k.batchnum,'-','')" & vbCrLf _
            & " left join oitm it on it.itemcode=k.itemcode " & vbCrLf _
            & "where k.warehouse='" & cmbwhs.Text & "' and (k.batchqty>0 or k.stkqty>0)" & vbCrLf _
            & "order by k.warehouse,k.itemname, k.u_style,k.u_size,k.batchnum" & vbCrLf


        Me.Cursor = Cursors.WaitCursor

        Dim j As Integer


        ' Call dvproofhead()
        'Dim darlf As New OleDb.OleDbDataAdapter
        'Dim dsrlf As New DataSet



        'darlf.SelectCommand = New OleDbCommand(msql, con)

        'Dim cb1 As OleDbCommandBuilder = New OleDbCommandBuilder(darlf)

        'darlf.Fill(dsrlf, "RRCOLOR.dbo.colormast")

        'Dim dtdf As DataTable = dsrlf.Tables("RRCOLOR.dbo.colormast")


        Dim CMD As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        CMD.CommandTimeout = 300
        'MsgBox(msql)
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()


        Try
            ''Dim DR As SqlDataReader
            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                While DR.Read
                    j = dv.Rows.Add()

                    dv.Rows.Item(j).Cells(0).Value = DR.Item("itemcode") & vbNullString
                    dv.Rows.Item(j).Cells(1).Value = DR.Item("itemname") & vbNullString
                    dv.Rows.Item(j).Cells(2).Value = DR.Item("u_style") & vbNullString
                    dv.Rows.Item(j).Cells(3).Value = DR.Item("u_size") & vbNullString
                    dv.Rows.Item(j).Cells(4).Value = DR.Item("batchnum") & vbNullString
                    dv.Rows.Item(j).Cells(5).Value = DR.Item("colorname") & vbNullString
                    dv.Rows.Item(j).Cells(6).Value = Val(DR.Item("batchqty"))
                    dv.Rows.Item(j).Cells(7).Value = Val(DR.Item("stkqty"))
                    dv.Rows.Item(j).Cells(8).Value = DR.Item("stockvalue") & vbNullString
                    dv.Rows.Item(j).Cells(9).Value = DR.Item("ctype") & vbNullString
                    dv.Rows.Item(j).Cells(10).Value = DR.Item("u_subgrp1") & vbNullString



                    If IsDBNull(DR.Item("colimage")) = False Then
                        arr = DR.Item("colimage")
                        ' bmpImage = DirectCast(Image.FromFile(filepath, True), Bitmap)
                        ' PictureBox1.Image = PictureBox1.Image.FromStream(New IO.MemoryStream(arr))
                        dv.Rows.Item(j).Cells(11).Value = System.Drawing.Image.FromStream(New IO.MemoryStream(arr))
                        arr = Nothing
                    End If
                End While
            End If




            DR.Close()
            CMD.Dispose()

        Catch ex As OleDbException
            MsgBox(ex.ToString)
        Finally

        End Try


        Me.Cursor = Cursors.Default



    End Sub

    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.Click
        Call loadrep()
    End Sub

    Private Sub CmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        Dim i As Integer
        For i = 0 To Me.dv.Rows.Count - 1
            Me.dv.Rows(0).Selected = True
            Me.dv.Rows(0).Dispose()
            Me.dv.Rows.RemoveAt(Me.dv.SelectedRows(0).Index)
        Next
        'txtcolcode.Text = ""
    End Sub

    Private Sub dv_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dv.KeyPress
        'If Asc(e.KeyChar) = 13 Then
        '    If dv.CurrentCell.RowIndex = dv.RowCount - 1 Then
        '        'dv.Rows(dv1.CurrentCell.RowIndex).Cells(0).Value()
        '        'dv.Rows(dv1.CurrentCell.RowIndex).Cells(1).Value()
        '    Else
        '        'Txtempcode.Text = dv1.Rows(dv1.CurrentCell.RowIndex - 1).Cells(0).Value
        '        'Txtempname.Text = dv1.Rows(dv1.CurrentCell.RowIndex - 1).Cells(1).Value

        '    End If



        '    tmpstr = ""
        'ElseIf Asc(e.KeyChar) = 27 Then
        '    tmpstr = ""

        'Else
        '    tmpstr = tmpstr + e.KeyChar
        '    'Call findgrid2(tmpstr)
        '    Call findgridnew(tmpstr, dv, 2)
        'End If

        If Asc(e.KeyChar) = 27 Then
            tmpstr = ""
        Else
            tmpstr = tmpstr + e.KeyChar
            Call findgrid(tmpstr)
        End If
        'Call findgrid(e.KeyChar)
        'Call Find(e.KeyChar)

        'keypress will only fire when the cell is read-only
        'If Char.IsLetterOrDigit(e.KeyChar) Then

        '    e.Handled = ShowItemSearch(dv.CurrentCell.ColumnIndex, dv.CurrentCell.RowIndex, e.KeyChar)
        'End If


    End Sub

    Private Sub findgrid(ByVal strr As String)

        Dim s As String = strr.Trim.ToUpper
        dv.ClearSelection()
        dv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        For x As Integer = 0 To dv.Rows.Count - 1
            If CStr(dv.Rows(x).Cells(1).Value).StartsWith(s).ToString.ToUpper Then
                dv.FirstDisplayedScrollingRowIndex = x
                dv.Item(1, x).Selected = True

                Exit Sub
            End If
        Next
    End Sub

    Private Sub cmdexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexcel.Click
        gridexcelexport(dv, 1)
    End Sub
End Class
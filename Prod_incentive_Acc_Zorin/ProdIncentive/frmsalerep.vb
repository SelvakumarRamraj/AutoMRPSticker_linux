Imports System.IO
Imports System.Math
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.VbStrConv

Public Class frmsalerep
    Dim arr() As Byte
    Dim k, j As Int32
    Private m_SelectedStyle As DataGridViewCellStyle
    Private m_SelectedRow As Integer = -1
    Dim msql As String
    Dim tmpstr As String
    Dim mtotqty As Integer
    Dim tot As Decimal
    Dim msql2 As String
    Private Sub frmsalerep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Call loadwhs(cmbwhs)
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        Call dvhead()
        Call dv1head()
        Call dv2head()
        Call loadparty2(cmbparty)
        Call loaditem(cmbitem)
    End Sub

   

    
    Private Sub loaddv()
        Call dvclear()

        'msql = "select DocNum,DocEntry,DocDate,CardCode,CardName,DocTotal,U_DocThrough,U_Transport,U_Destination,U_LRNO,U_Lrdat,u_podno,U_ESugam,U_Arcode * from OINV where DocDate>='" & Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' and docdate<='" & Format(CDate(msktodate.Text), "yyyy-MM-dd") & "'"
        If Len(Trim(cmbparty.Text)) > 0 Then
            msql = "select convert(nvarchar(max),f.U_Remarks) as stype, b.DocNum,b.DocEntry,b.DocDate,b.CardCode,b.CardName,SUM(c.quantity) as qty, b.DocTotal,b.U_DocThrough,b.U_Transport,b.U_Destination,b.U_LRNO,b.U_Lrdat,b.u_podno,b.U_ESugam,b.U_Arcode,b.u_brand from OINV b with (nolock) " & vbCrLf _
               & "left join INV1 c with (nolock) on c.DocEntry=b.DocEntry " & vbCrLf _
               & " left join [@INCM_BND1]  f with (nolock) on f.U_Name=b.u_brand and convert(nvarchar(max),f.U_Remarks) is not null " & vbCrLf _
               & " where b.DocDate>='" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' and b.docdate<='" & Microsoft.VisualBasic.Format(CDate(msktodate.Text), "yyyy-MM-dd") & "'" & vbCrLf _
               & " and b.cardname='" & cmbparty.Text & "'" & vbCrLf _
               & " group by convert(nvarchar(max),f.U_Remarks), b.DocNum,b.DocEntry,b.DocDate,b.CardCode,b.CardName, b.DocTotal,b.U_DocThrough,b.U_Transport,b.U_Destination,b.U_LRNO,b.U_Lrdat,b.u_podno,b.U_ESugam,b.U_Arcode,b.u_brand"

        ElseIf Len(Trim(cmbitem.Text)) > 0 Then
            msql = "select convert(nvarchar(max),f.U_Remarks) as stype, b.DocNum,b.DocEntry,b.DocDate,b.CardCode,b.CardName,SUM(c.quantity) as qty, b.DocTotal,b.U_DocThrough,b.U_Transport,b.U_Destination,b.U_LRNO,b.U_Lrdat,b.u_podno,b.U_ESugam,b.U_Arcode,b.u_brand from OINV b with (nolock) " & vbCrLf _
              & "left join INV1 c with (nolock) on c.DocEntry=b.DocEntry " & vbCrLf _
              & " left join [@INCM_BND1]  f with (nolock) on f.U_Name=b.u_brand and convert(nvarchar(max),f.U_Remarks) is not null " & vbCrLf _
              & " where b.DocDate>='" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' and b.docdate<='" & Microsoft.VisualBasic.Format(CDate(msktodate.Text), "yyyy-MM-dd") & "'" & vbCrLf _
              & " and c.dscription='" & cmbitem.Text & "'" & vbCrLf _
              & " group by convert(nvarchar(max),f.U_Remarks), b.DocNum,b.DocEntry,b.DocDate,b.CardCode,b.CardName, b.DocTotal,b.U_DocThrough,b.U_Transport,b.U_Destination,b.U_LRNO,b.U_Lrdat,b.u_podno,b.U_ESugam,b.U_Arcode,b.u_brand"



        Else
            msql = "select convert(nvarchar(max),f.U_Remarks) as stype, b.DocNum,b.DocEntry,b.DocDate,b.CardCode,b.CardName,SUM(c.quantity) as qty, b.DocTotal,b.U_DocThrough,b.U_Transport,b.U_Destination,b.U_LRNO,b.U_Lrdat,b.u_podno,b.U_ESugam,b.U_Arcode,b.u_brand from OINV b with (nolock) " & vbCrLf _
                   & "left join INV1 c with (nolock) on c.DocEntry=b.DocEntry " & vbCrLf _
                   & " left join [@INCM_BND1]  f with (nolock) on f.U_Name=b.u_brand and convert(nvarchar(max),f.U_Remarks) is not null " & vbCrLf _
                   & " where b.DocDate>='" & Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd") & "' and b.docdate<='" & Microsoft.VisualBasic.Format(CDate(msktodate.Text), "yyyy-MM-dd") & "'" & vbCrLf _
                   & "group by convert(nvarchar(max),f.U_Remarks), b.DocNum,b.DocEntry,b.DocDate,b.CardCode,b.CardName, b.DocTotal,b.U_DocThrough,b.U_Transport,b.U_Destination,b.U_LRNO,b.U_Lrdat,b.u_podno,b.U_ESugam,b.U_Arcode,b.u_brand"
        End If


        Me.Cursor = Cursors.WaitCursor

        Dim j As Integer




        Dim CMD As New SqlCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        CMD.CommandTimeout = 300


        Try
            ''Dim DR As SqlDataReader
            Dim DR As SqlDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                While DR.Read
                    j = dv.Rows.Add()

                    dv.Rows.Item(j).Cells(0).Value = DR.Item("stype") & vbNullString
                    dv.Rows.Item(j).Cells(1).Value = DR.Item("docnum")
                    dv.Rows.Item(j).Cells(2).Value = DR.Item("docentry")
                    dv.Rows.Item(j).Cells(3).Value = DR.Item("docdate")
                    dv.Rows.Item(j).Cells(4).Value = DR.Item("cardcode") & vbNullString
                    dv.Rows.Item(j).Cells(5).Value = DR.Item("cardname") & vbNullString
                    dv.Rows.Item(j).Cells(6).Value = Microsoft.VisualBasic.Format(Val(DR.Item("qty")), "########0")
                    dv.Rows.Item(j).Cells(7).Value = Microsoft.VisualBasic.Format(Val(DR.Item("doctotal")), "#############0.00")
                    dv.Rows.Item(j).Cells(8).Value = DR.Item("u_lrno") & vbNullString
                    dv.Rows.Item(j).Cells(9).Value = DR.Item("u_lrdat") & vbNullString
                    dv.Rows.Item(j).Cells(10).Value = DR.Item("u_transport") & vbNullString
                    'dv.Rows.Item(j).Cells(11).Value = DR.Item("basetype") & vbNullString



                    'If IsDBNull(DR.Item("colimage")) = False Then
                    '    arr = DR.Item("colimage")
                    '    ' bmpImage = DirectCast(Image.FromFile(filepath, True), Bitmap)
                    '    ' PictureBox1.Image = PictureBox1.Image.FromStream(New IO.MemoryStream(arr))
                    '    dv.Rows.Item(j).Cells(11).Value = System.Drawing.Image.FromStream(New IO.MemoryStream(arr))
                    '    arr = Nothing
                    'End If
                End While
            End If




            DR.Close()
            CMD.Dispose()

        Catch ex As sqlException
            MsgBox(ex.ToString)
        Finally

        End Try


        Me.Cursor = Cursors.Default




    End Sub



    Private Sub loaddv1(ByVal docentry As Integer)
        Call dv1clear()
        mtotqty = 0
        tot = 0
        'tmpstr = "select ItemCode,Dscription,Quantity,Price,linetotal from INV1 where DocEntry=" & docentry
        tmpstr = "select b.baseentry, b.ItemCode,b.Dscription,it.u_style,it.u_size, b.Quantity qty,b.Price,b.linetotal,b.basetype from INV1 b with (nolock) " & vbCrLf _
                & "left join oitm it with (nolock) on it.ItemCode=b.ItemCode  " & vbCrLf _
                & "where b.DocEntry = " & docentry

        Me.Cursor = Cursors.WaitCursor

        Dim k As Integer




        Dim CMD1 As New SqlCommand(tmpstr, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        CMD1.CommandTimeout = 300


        Try
            ''Dim DR As SqlDataReader
            Dim DR1 As SqlDataReader
            DR1 = CMD1.ExecuteReader
            If DR1.HasRows = True Then
                While DR1.Read
                    k = dv1.Rows.Add()

                    dv1.Rows.Item(k).Cells(0).Value = DR1.Item("itemcode") & vbNullString
                    dv1.Rows.Item(k).Cells(1).Value = DR1.Item("dscription")
                    dv1.Rows.Item(k).Cells(2).Value = DR1.Item("u_style")
                    dv1.Rows.Item(k).Cells(3).Value = DR1.Item("u_size")
                    dv1.Rows.Item(k).Cells(4).Value = Microsoft.VisualBasic.Format(Val(DR1.Item("qty")), "########0")
                    dv1.Rows.Item(k).Cells(5).Value = Microsoft.VisualBasic.Format(Val(DR1.Item("price")), "######0.00")
                    dv1.Rows.Item(k).Cells(6).Value = Microsoft.VisualBasic.Format(Val(DR1.Item("linetotal")), "##############0.00")
                    dv1.Rows.Item(k).Cells(7).Value = DR1.Item("basetype")
                    dv1.Rows.Item(k).Cells(8).Value = DR1.Item("baseentry")


                    mtotqty = mtotqty + Val(DR1.Item("qty"))
                    tot = tot + Val(DR1.Item("linetotal"))

                End While
                k = dv1.Rows.Add()
                k = dv1.Rows.Add()
                dv1.Rows.Item(k).Cells(1).Value = "Total"
                dv1.Rows.Item(k).Cells(4).Value = Microsoft.VisualBasic.Format(Val(mtotqty), "########0")
                dv1.Rows.Item(k).Cells(6).Value = Microsoft.VisualBasic.Format(Val(tot), "##############0.00")

            End If

            ' dv1.FooterRow.Cells(1).Text = "Total"
            'GridView1.FooterRow.Cells(1).HorizontalAlign = HorizontalAlign.Right
            'GridView1.FooterRow.Cells(2).Text = total.ToString("N2")


            DR1.Close()
            CMD1.Dispose()

        Catch ex As sqlException
            MsgBox(ex.ToString)
        Finally

        End Try


        Me.Cursor = Cursors.Default


        ' CType(GridControl1.MainView, GridView).Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

        '' In the below line we have to define the field name on which we have to perform the operation
        'CType(GridControl1.MainView, GridView).Columns(2).SummaryItem.FieldName = "Marks"

        ''in the below line we have to define the format of tect which will display on the footer. You can also round up the calcuations
        'CType(GridControl1.MainView, GridView).Columns(2).SummaryItem.DisplayFormat = " Totals Marks= {0}"

        'Dim total As Decimal = dt.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("Price"))
        'GridView1.FooterRow.Cells(1).Text = "Total"
        'GridView1.FooterRow.Cells(1).HorizontalAlign = HorizontalAlign.Right
        'GridView1.FooterRow.Cells(2).Text = total.ToString("N2")

    End Sub

    'Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

    'Dim TotalHours As Decimal = 0

    'If E.Row.RowType = DataControlRowType.DataRow Then

    '    Dim RowTotal As Decimal = Convert.ToDecimal(DataBinder.Eval(E.Row.DataItem, "Hours"))
    '    TotalHours = TotalHours + RowTotal

    '    If Not tableCopied Then
    '        originalDataTable = CType(E.Row.DataItem, System.Data.DataRowView).Row.Table.Copy()
    '        ViewState("originalValuesDataTable") = originalDataTable
    '        tableCopied = True
    '    End If

    'ElseIf E.Row.RowType = DataControlRowType.Footer Then
    '    E.Row.Cells(0).Text = "Total Hours:"
    '    E.Row.Cells(6).Text = TotalHours.ToString
    '    E.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
    '    E.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
    '    E.Row.Cells(0).Font.Name = "Arial"
    '    E.Row.Cells(6).Font.Name = "Arial"
    '    E.Row.Font.Bold = True

    'End If

    'End Sub


    Private Sub dvhead()

        'DV.RowCount = 1
        'DV.ColumnCount = 3

        'Dim brand As New DataGridViewComboBoxColumn
        Dim mstype As New DataGridViewTextBoxColumn
        Dim mdocnum As New DataGridViewTextBoxColumn
        Dim mdocentry As New DataGridViewTextBoxColumn
        Dim mdocdate As New DataGridViewTextBoxColumn
        Dim mcardcode As New DataGridViewTextBoxColumn
        Dim mcardname As New DataGridViewTextBoxColumn
        Dim mqty As New DataGridViewTextBoxColumn
        Dim mvalue As New DataGridViewTextBoxColumn
        Dim mlrno As New DataGridViewTextBoxColumn
        Dim mlrdat As New DataGridViewTextBoxColumn
        Dim mtrans As New DataGridViewTextBoxColumn
        'Dim colimg As New DataGridViewImageColumn


        'Dim btn As New DataGridViewButtonColumn()

        'Dim inImg As New DataGridViewImageCell()
        'colimg.HeaderText = "Image"
        'colimg.Name = "img"
        'colimg.ImageLayout = DataGridViewImageCellLayout.Stretch

        'colorcode.ValueType = GetType(String)
        'colorcode.HeaderText = "Brand"



        'brand.ValueType = GetType(String)
        'brand.HeaderText = "Brand"

        mstype.ValueType = GetType(String)
        mstype.HeaderText = "Stype"

        mdocnum.ValueType = GetType(String)
        mdocnum.HeaderText = "Doc.Num"

        mdocentry.ValueType = GetType(String)
        mdocentry.HeaderText = "Doc.Entry"

        mdocdate.ValueType = GetType(String)
        mdocdate.HeaderText = "Date"

        mcardcode.ValueType = GetType(String)
        mcardcode.HeaderText = "Cardcode"

        mcardname.ValueType = GetType(String)
        mcardname.HeaderText = "Party Name"

        mqty.ValueType = GetType(Int32)
        mqty.HeaderText = "Qty"

        mvalue.ValueType = GetType(Decimal)
        mvalue.HeaderText = "Amount"

        mlrno.ValueType = GetType(String)
        mlrno.HeaderText = "L.R.No"

        mlrdat.ValueType = GetType(String)
        mlrdat.HeaderText = "LR.Date"

        mtrans.ValueType = GetType(String)
        mtrans.HeaderText = "Transport"




        'DataGridView1.Columns.Add(btn)
        'btn.HeaderText = "Select Image"
        'btn.Text = "Click Here"
        'btn.Name = "btn"
        'btn.UseColumnTextForButtonValue = True

        With dv
            'dv.Columns.Add(brand)
            dv.Columns.Add(mstype)
            dv.Columns.Add(mdocnum)
            dv.Columns.Add(mdocentry)
            dv.Columns.Add(mdocdate)
            dv.Columns.Add(mcardcode)
            dv.Columns.Add(mcardname)
            dv.Columns.Add(mqty)
            dv.Columns.Add(mvalue)
            dv.Columns.Add(mlrno)
            dv.Columns.Add(mlrdat)
            dv.Columns.Add(mtrans)


            'dvproof.Columns.Add(btn)
        End With
        dv.ColumnHeadersDefaultCellStyle.Font = New Font(dv.Font, FontStyle.Bold)
        dv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dv.Columns(0).Width = 50
        dv.Columns(1).Width = 70
        dv.Columns(2).Width = 1
        dv.Columns(3).Width = 70
        dv.Columns(4).Width = 1
        dv.Columns(5).Width = 150
        dv.Columns(6).Width = 70
        dv.Columns(7).Width = 70
        dv.Columns(8).Width = 70
        dv.Columns(9).Width = 70
        dv.Columns(10).Width = 100

        dv.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        dv.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight

        'Dim row As DataGridViewRow = dvproof.Rows(0)
        'row.Height = 25
        'colimg.ImageLayout = DataGridViewImageCellLayout.Zoom
        'dvproof.ReadOnly = True
        dv.AllowUserToAddRows = False
        'dvproof.Rows.Add()

    End Sub

    Private Sub dv1head()

        'DV.RowCount = 1
        'DV.ColumnCount = 3

        'Dim brand As New DataGridViewComboBoxColumn
        Dim mcode As New DataGridViewTextBoxColumn
        Dim mitemname As New DataGridViewTextBoxColumn
        Dim mstyle As New DataGridViewTextBoxColumn
        Dim msize As New DataGridViewTextBoxColumn
        Dim mlqty As New DataGridViewTextBoxColumn
        Dim mprice As New DataGridViewTextBoxColumn
        Dim mlinetot As New DataGridViewTextBoxColumn

        Dim mbasetype As New DataGridViewTextBoxColumn
        Dim mdocentry As New DataGridViewTextBoxColumn

        mcode.ValueType = GetType(String)
        mcode.HeaderText = "Item Code"

        mitemname.ValueType = GetType(String)
        mitemname.HeaderText = "Item Name"

        mstyle.ValueType = GetType(String)
        mstyle.HeaderText = "Style"

        msize.ValueType = GetType(String)
        msize.HeaderText = "Size"

        mlqty.ValueType = GetType(Integer)
        mlqty.HeaderText = "Qty"

        mprice.ValueType = GetType(Decimal)
        mprice.HeaderText = "Price"

        mlinetot.ValueType = GetType(Decimal)
        mlinetot.HeaderText = "Line Total"

        mbasetype.ValueType = GetType(String)
        mbasetype.HeaderText = "basetype"

        mdocentry.ValueType = GetType(Integer)
        mdocentry.HeaderText = "Docentry"


        With dv1
            'dv.Columns.Add(brand)
            dv1.Columns.Add(mcode)
            dv1.Columns.Add(mitemname)
            dv1.Columns.Add(mstyle)
            dv1.Columns.Add(msize)
            dv1.Columns.Add(mlqty)
            dv1.Columns.Add(mprice)
            dv1.Columns.Add(mlinetot)
            dv1.Columns.Add(mbasetype)
            dv1.Columns.Add(mdocentry)




            'dvproof.Columns.Add(btn)
        End With
        dv1.ColumnHeadersDefaultCellStyle.Font = New Font(dv1.Font, FontStyle.Bold)
        dv1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dv1.Columns(0).Width = 50
        dv1.Columns(1).Width = 100
        dv1.Columns(2).Width = 70
        dv1.Columns(3).Width = 70
        dv1.Columns(4).Width = 75
        dv1.Columns(5).Width = 70
        dv1.Columns(6).Width = 80
        dv1.Columns(7).Width = 1
        dv1.Columns(8).Width = 1


        dv1.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        dv1.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        dv1.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        'Dim row As DataGridViewRow = dvproof.Rows(0)
        'row.Height =        25
        'colimg.ImageLayout = DataGridViewImageCellLayout.Zoom
        'dvproof.ReadOnly = True
        dv1.AllowUserToAddRows = False
        'dvproof.Rows.Add()

    End Sub
    Private Sub CmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdExit.Click
        Me.Close()
    End Sub


    Private Sub dv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dv.CellContentClick

    End Sub

    Private Sub dv_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dv.CellContentDoubleClick
        If dv1.Visible = False Then dv1.Visible = True
        loaddv1(dv.Rows(e.RowIndex).Cells(2).Value)
    End Sub

    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.Click
        'Call dvhead()
        Call loaddv()
    End Sub

    Private Sub dv1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dv1.CellContentClick

    End Sub

    Private Sub dv1_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dv1.CellContentDoubleClick
        'Call dv1clear()
        'If dv1.Visible = True Then dv1.Visible = False
        Call dv2clear()
        If dv2.Visible = False Then dv2.Visible = True
        dv2.BringToFront()
        loaddv2(dv1.Rows(e.RowIndex).Cells(8).Value, dv1.Rows(e.RowIndex).Cells(0).Value, dv1.Rows(e.RowIndex).Cells(7).Value)

    End Sub

    Private Sub dv1clear()
        Dim i As Integer
        For i = 0 To Me.dv1.Rows.Count - 1
            Me.dv1.Rows(0).Selected = True
            Me.dv1.Rows(0).Dispose()
            Me.dv1.Rows.RemoveAt(Me.dv1.SelectedRows(0).Index)
        Next
    End Sub
    Private Sub dv2clear()
        Dim i As Integer
        For i = 0 To Me.dv2.Rows.Count - 1
            Me.dv2.Rows(0).Selected = True
            Me.dv2.Rows(0).Dispose()
            Me.dv2.Rows.RemoveAt(Me.dv2.SelectedRows(0).Index)
        Next
    End Sub

    Private Sub dvclear()
        Dim i As Integer
        For i = 0 To Me.dv.Rows.Count - 1
            Me.dv.Rows(0).Selected = True
            Me.dv.Rows(0).Dispose()
            Me.dv.Rows.RemoveAt(Me.dv.SelectedRows(0).Index)
        Next
    End Sub
    
    Private Sub dv1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dv1.CellEndEdit
        'Dim totqty As Integer, tot As Double, i As Integer
        'If (e.ColumnIndex = 6) Then
        '    tot = 0.0
        '    totqty = 0
        '    For i = 0 To dv1.Rows.Count - 1
        '        tot = tot + Val(dv1.Rows(i).Cells(e.ColumnIndex).Value)
        '    Next i
        '    Total(Amount.Text = RefundTotal.ToString("$#,##0.00"))
        '    Total(Record, Text = DataGridView1.Rows.Count.ToString())
        'End If

    End Sub
    Private Sub dv2head()

        'DV.RowCount = 1
        'DV.ColumnCount = 3

        'Dim brand As New DataGridViewComboBoxColumn
        Dim mbcode As New DataGridViewTextBoxColumn
        Dim mbatchnum As New DataGridViewTextBoxColumn
        Dim mbcolor As New DataGridViewTextBoxColumn
        Dim mbqty As New DataGridViewTextBoxColumn
        

        mbcode.ValueType = GetType(String)
        mbcode.HeaderText = "Item Code"

        mbatchnum.ValueType = GetType(String)
        mbatchnum.HeaderText = "Col.Code"

        mbcolor.ValueType = GetType(String)
        mbcolor.HeaderText = "Color"

        mbqty.ValueType = GetType(Integer)
        mbqty.HeaderText = "Qty"

        
        With dv2
            'dv.Columns.Add(brand)
            dv2.Columns.Add(mbcode)
            dv2.Columns.Add(mbatchnum)
            dv2.Columns.Add(mbcolor)
            dv2.Columns.Add(mbqty)

            'dvproof.Columns.Add(btn)
        End With
        dv2.ColumnHeadersDefaultCellStyle.Font = New Font(dv1.Font, FontStyle.Bold)
        dv2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        dv2.Columns(0).Width = 50
        dv2.Columns(1).Width = 100
        dv2.Columns(2).Width = 70
        dv2.Columns(3).Width = 70
        
        dv2.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        'dv1.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        'dv1.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        'Dim row As DataGridViewRow = dvproof.Rows(0)
        'row.Height =        25
        'colimg.ImageLayout = DataGridViewImageCellLayout.Zoom
        'dvproof.ReadOnly = True
        dv2.AllowUserToAddRows = False
        'dvproof.Rows.Add()

    End Sub

    Private Sub loaddv2(ByVal docentry As Integer, ByVal mitemcode As String, ByVal basetype As String)
        Call dv2clear()
        mtotqty = 0
        tot = 0
        'tmpstr = "select ItemCode,Dscription,Quantity,Price,linetotal from INV1 where DocEntry=" & docentry
        'tmpstr = "select b.ItemCode,b.Dscription,it.u_style,it.u_size, b.Quantity qty,b.Price,b.linetotal from INV1 b with (nolock) " & vbCrLf _
        '        & "left join oitm it with (nolock) on it.ItemCode=b.ItemCode  " & vbCrLf _
        '        & "where b.DocEntry = " & docentry


        msql2 = "select b.itemcode,b.batchnum,c.colorname, SUM(b.quantity) qty from ibt1 b with (nolock) " & vbCrLf _
               & " left join RRCOLOR.dbo.colormast c on c.mktcode collate SQL_Latin1_General_CP850_CI_AS=b.batchnum " & vbCrLf _
               & " where b.BaseType=" & Val(basetype) & " and b.BaseEntry=" & docentry & " and b.ItemCode='" & mitemcode & "' " & vbCrLf _
               & " group by b.ItemCode,b.batchnum,c.colorname"


        Me.Cursor = Cursors.WaitCursor

        Dim l As Integer




        Dim CMD2 As New SqlCommand(msql2, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        CMD2.CommandTimeout = 300


        Try
            ''Dim DR As SqlDataReader
            Dim DR2 As SqlDataReader
            DR2 = CMD2.ExecuteReader
            If DR2.HasRows = True Then
                While DR2.Read
                    l = dv2.Rows.Add()

                    dv2.Rows.Item(l).Cells(0).Value = DR2.Item("itemcode") & vbNullString
                    dv2.Rows.Item(l).Cells(1).Value = DR2.Item("batchnum")
                    dv2.Rows.Item(l).Cells(2).Value = DR2.Item("colorname")
                    dv2.Rows.Item(l).Cells(3).Value = Microsoft.VisualBasic.Format(Val(DR2.Item("qty")), "########0")

                    'mtotqty = mtotqty + Val(DR1.Item("qty"))
                    'tot = tot + Val(DR1.Item("linetotal"))

                End While
                'k = dv1.Rows.Add()
                'k = dv1.Rows.Add()
                'dv1.Rows.Item(k).Cells(1).Value = "Total"
                'dv1.Rows.Item(k).Cells(4).Value = Microsoft.VisualBasic.Format(Val(mtotqty), "########0")
                'dv1.Rows.Item(k).Cells(6).Value = Microsoft.VisualBasic.Format(Val(tot), "##############0.00")

            End If


            DR2.Close()
            CMD2.Dispose()

        Catch ex As sqlException
            MsgBox(ex.ToString)
        Finally
        End Try
        Me.Cursor = Cursors.Default

    End Sub


    Private Sub dv1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dv1.KeyDown
        If e.KeyCode = Keys.Escape Then
            Call dv1clear()
            If dv1.Visible = True Then dv1.Visible = False
        End If
    End Sub

    Private Sub dv2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dv2.CellContentClick

    End Sub

    Private Sub dv2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dv2.KeyDown
        If e.KeyCode = Keys.Escape Then
            If dv2.Visible = True Then dv2.Visible = False
        End If
    End Sub

    Private Sub cmddisp2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp2.Click

    End Sub

    Private Sub cmdexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexcel.Click

    End Sub

    Private Sub cmbparty_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbparty.SelectedIndexChanged

    End Sub
End Class

Imports System
Imports System.Math
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.VbStrConv
Imports System.IO

Public Class RRQRCODE


    Dim lin, n, k As Integer
    Dim dir, mdir, msql As String
    Dim table1 As DataTable = New DataTable
    Private encryptedString As String = ""
    Private decryptedString As String = ""


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Len(Trim(Brand.Text)) > 0 Or Len(Trim(cmbbrand.Text)) > 0 Then
            chkih.Checked = False
            chksc.Checked = False
            chkgrn.Checked = False
            chkfob.Checked = False
        End If


        If chkstall.Checked = True Then
            loaddv.DataSource = Nothing
            loaddv.ColumnCount = 0
            loaddv.DataSource = Nothing
            loaddv.ColumnCount = 13
            loaddv.Columns.Insert(0, New DataGridViewCheckBoxColumn)


            loaddv.Columns(0).HeaderText = "sel"
            loaddv.Columns(0).Name = "sel"
            loaddv.Columns(0).DataPropertyName = "sel"
            loaddv.Columns(0).Width = 60

            loaddv.Columns(1).HeaderText = "itemcode"
            loaddv.Columns(1).Name = "itemcode"
            loaddv.Columns(1).DataPropertyName = "itemcode"
            loaddv.Columns(1).Width = 150
            loaddv.Columns(1).ReadOnly = True

            loaddv.Columns(2).Name = "ItemName"
            loaddv.Columns(2).HeaderText = "ItemName"
            loaddv.Columns(2).DataPropertyName = "ItemName"
            loaddv.Columns(2).Width = 200
            loaddv.Columns(2).ReadOnly = True

            loaddv.Columns(3).Name = "Style"
            loaddv.Columns(3).HeaderText = "Style"
            loaddv.Columns(3).DataPropertyName = "Style"
            loaddv.Columns(3).Width = 100
            loaddv.Columns(3).ReadOnly = True

            loaddv.Columns(4).Name = "Size"
            loaddv.Columns(4).HeaderText = "Size"
            loaddv.Columns(4).DataPropertyName = "Size"
            loaddv.Columns(4).Width = 60
            loaddv.Columns(4).ReadOnly = True

            loaddv.Columns(5).Name = "Colcode"
            loaddv.Columns(5).HeaderText = "Colcode"
            loaddv.Columns(5).DataPropertyName = "Colcode"
            loaddv.Columns(5).Width = 60
            loaddv.Columns(5).ReadOnly = True

            loaddv.Columns(6).Name = "Quantity"
            loaddv.Columns(6).HeaderText = "Quantity"
            loaddv.Columns(6).DataPropertyName = "Quantity"
            loaddv.Columns(6).Width = 100
            'loaddv.Columns.Item(6).ReadOnly = True

            loaddv.Columns(7).Name = "Rate"
            loaddv.Columns(7).HeaderText = "Rate"
            loaddv.Columns(7).DataPropertyName = "Rate"
            loaddv.Columns(7).Width = 100
            loaddv.Columns(7).ReadOnly = True

            loaddv.Columns(8).Name = "grp"
            loaddv.Columns(8).HeaderText = "grp"
            loaddv.Columns(8).DataPropertyName = "grp"
            loaddv.Columns(8).Width = 160
            loaddv.Columns(8).ReadOnly = True

            loaddv.Columns(9).Name = "stype"
            loaddv.Columns(9).HeaderText = "stype"
            loaddv.Columns(9).DataPropertyName = "stype"
            loaddv.Columns(9).Width = 100
            loaddv.Columns(9).ReadOnly = True

            loaddv.Columns(10).Name = "qty"
            loaddv.Columns(10).HeaderText = "qty"
            loaddv.Columns(10).DataPropertyName = "qty"
            loaddv.Columns(10).Width = 60
            loaddv.Columns(10).ReadOnly = True

            loaddv.Columns(11).Name = "Docdate"
            loaddv.Columns(11).HeaderText = "Docdate"
            loaddv.Columns(11).DataPropertyName = "Docdate"
            loaddv.Columns(11).Width = 100
            loaddv.Columns(11).ReadOnly = True

            loaddv.Columns(12).Name = "Docnum"
            loaddv.Columns(12).HeaderText = "Docnum"
            loaddv.Columns(12).DataPropertyName = "Docnum"
            loaddv.Columns(12).Width = 100
            loaddv.Columns(12).ReadOnly = True

        ElseIf chkATC.Checked = True Then
            loaddv.DataSource = Nothing
            loaddv.ColumnCount = 0
            loaddv.DataSource = Nothing
            loaddv.ColumnCount = 11
            loaddv.Columns.Insert(0, New DataGridViewCheckBoxColumn)


            loaddv.Columns(0).HeaderText = "sel"
            loaddv.Columns(0).Name = "sel"
            loaddv.Columns(0).DataPropertyName = "sel"
            loaddv.Columns(0).Width = 60

            loaddv.Columns(1).HeaderText = "itemcode"
            loaddv.Columns(1).Name = "itemcode"
            loaddv.Columns(1).DataPropertyName = "itemcode"
            loaddv.Columns(1).Width = 150
            loaddv.Columns(1).ReadOnly = True

            loaddv.Columns(2).Name = "atccode"
            loaddv.Columns(2).HeaderText = "atccode"
            loaddv.Columns(2).DataPropertyName = "atccode"
            loaddv.Columns(2).Width = 100
            loaddv.Columns(2).ReadOnly = True

            loaddv.Columns(3).Name = "ItemName"
            loaddv.Columns(3).HeaderText = "ItemName"
            loaddv.Columns(3).DataPropertyName = "ItemName"
            loaddv.Columns(3).Width = 200
            loaddv.Columns(3).ReadOnly = True

            loaddv.Columns(4).Name = "quantity"
            loaddv.Columns(4).HeaderText = "quantity"
            loaddv.Columns(4).DataPropertyName = "quantity"
            loaddv.Columns(4).Width = 60
            loaddv.Columns(4).ReadOnly = True

            loaddv.Columns(5).Name = "qty"
            loaddv.Columns(5).HeaderText = "qty"
            loaddv.Columns(5).DataPropertyName = "qty"
            loaddv.Columns(5).Width = 60
            loaddv.Columns(5).ReadOnly = True

            loaddv.Columns(6).Name = "u_colcode"
            loaddv.Columns(6).HeaderText = "u_colcode"
            loaddv.Columns(6).DataPropertyName = "u_colcode"
            loaddv.Columns(6).Width = 100
            'loaddv.Columns.Item(6).ReadOnly = True

            loaddv.Columns(7).Name = "Docnum"
            loaddv.Columns(7).HeaderText = "Docnum"
            loaddv.Columns(7).DataPropertyName = "Docnum"
            loaddv.Columns(7).Width = 100
            loaddv.Columns(7).ReadOnly = True

            loaddv.Columns(8).Name = "Docdate"
            loaddv.Columns(8).HeaderText = "Docdate"
            loaddv.Columns(8).DataPropertyName = "Docdate"
            loaddv.Columns(8).Width = 100
            loaddv.Columns(8).ReadOnly = True

            loaddv.Columns(9).Name = "Vendorcode"
            loaddv.Columns(9).HeaderText = "Vendorcode"
            loaddv.Columns(9).DataPropertyName = "Vendorcode"
            loaddv.Columns(9).Width = 100
            loaddv.Columns(9).ReadOnly = True

            loaddv.Columns(10).Name = "QR"
            loaddv.Columns(10).HeaderText = "QR"
            loaddv.Columns(10).DataPropertyName = "QR"
            loaddv.Columns(10).Width = 60
            loaddv.Columns(10).ReadOnly = True

        Else
            loaddv.DataSource = Nothing
            loaddv.ColumnCount = 0
            loaddv.DataSource = Nothing
            loaddv.ColumnCount = 7

            loaddv.Columns.Insert(0, New DataGridViewCheckBoxColumn)


            loaddv.Columns(0).HeaderText = "sel"
            loaddv.Columns(0).Name = "sel"
            loaddv.Columns(0).DataPropertyName = "sel"
            loaddv.Columns(0).Width = 60


            loaddv.Columns(1).HeaderText = "itemcode"
            loaddv.Columns(1).Name = "itemcode"
            loaddv.Columns(1).DataPropertyName = "itemcode"
            loaddv.Columns(1).Width = 150
            loaddv.Columns(1).ReadOnly = True


            loaddv.Columns(2).Name = "ItemName"
            loaddv.Columns(2).HeaderText = "ItemName"
            loaddv.Columns(2).DataPropertyName = "ItemName"
            loaddv.Columns(2).Width = 200
            loaddv.Columns(2).ReadOnly = True

            loaddv.Columns(3).Name = "qty"
            loaddv.Columns(3).HeaderText = "qty"
            loaddv.Columns(3).DataPropertyName = "qty"
            loaddv.Columns(3).Width = 60

            loaddv.Columns(4).Name = "grade"
            loaddv.Columns(4).HeaderText = "grade"
            loaddv.Columns(4).DataPropertyName = "grade"
            loaddv.Columns(4).Width = 260
            loaddv.Columns(4).ReadOnly = True

            loaddv.Columns(5).Name = "Docnum"
            loaddv.Columns(5).HeaderText = "Docnum"
            loaddv.Columns(5).DataPropertyName = "Docnum"
            loaddv.Columns(5).Width = 100
            loaddv.Columns(5).ReadOnly = True

            loaddv.Columns(6).Name = "Yr"
            loaddv.Columns(6).HeaderText = "Yr"
            loaddv.Columns(6).DataPropertyName = "Yr"
            loaddv.Columns(6).Width = 60
            loaddv.Columns(6).ReadOnly = True

        End If
        loaddv.AutoGenerateColumns = False

        Call main()

        If chkih.Checked = True Then

            'Dim da As System.Data.SqlClient.SqlDataAdapter
            Dim ds As New DataSet
            Dim da As OleDb.OleDbDataAdapter

            msql = "select 0 sel,k.u_qrname itemcode,isnull(k.u_colcode,'') itemname,convert(decimal(18,0),k.Qty) QTY,'' SHADE," & vbCrLf _
                 & "(k.u_rcode+'|'+ISNULL(k.u_colcode,'')+'|'+''+'|'+convert(nvarchar(max),convert(int,k.SalPackUn))+'|') GRADE,k.docnum,k.yr from " & vbCrLf _
                 & "(select c.docnum,c.docentry,c.u_docdate, b.U_ItemCode,case when len(rtrim(isnull(it.u_rrcode,'')))>0 then it.u_rrcode else b.U_ItemCode end u_rcode,it.u_rname,it.U_ColCode,it.u_qrname, it.U_Style,it.U_Size,it.SalPackUn, sum(b.U_AccpQty) qty,year(c.u_docdate) yr  from [@INm_wip1] b " & vbCrLf _
                 & "left join [@INm_Owip] c on c.DocEntry=b.DocEntry " & vbCrLf _
                 & " left join oitm it on it.itemcode=b.U_ItemCode " & vbCrLf _
                 & "left join NNM1 s with (nolock) on s.series=c.series" & vbCrLf _
                 & "where c.DocNum=" & Val(txtdocno.Text) & " and s.Indicator='" & Trim(cmbperiod.Text) & "' and b.U_AccptWhs='GFINISH'" & vbCrLf _
                 & "group by c.docnum,c.docentry,c.u_docdate, b.U_ItemCode,it.SalPackUn,case when len(rtrim(isnull(it.u_rrcode,'')))>0 then it.u_rrcode else b.U_ItemCode end ,it.u_rname,it.U_ColCode,it.u_qrname, it.U_Style,it.U_Size,year(c.u_docdate)) k "
            If con.State = ConnectionState.Broken Then
                con.Open()
            End If
            'da = New System.Data.SqlClient.SqlDataAdapter(msql, con)
            da = New OleDb.OleDbDataAdapter(msql, con)


            da.Fill(ds, "tbl1")
            loaddv.DataSource = ds.Tables("tbl1")
            loaddv.Select()

        ElseIf chksc.Checked = True Then

            Dim ds As New DataSet
            Dim da As OleDb.OleDbDataAdapter
            'Dim da As System.Data.SqlClient.SqlDataAdapter, ds As New DataSet
            'msql = "select 0 sel,k.u_qrname itemcode,isnull(k.u_colcode,'') itemname,convert(decimal(18,0),k.Qty) QTY,'' SHADE," & vbCrLf _
            '     & "(k.u_rcode+'|'+ISNULL(k.u_colcode,'')+'|'+''+'|'+convert(nvarchar(max),convert(int,k.SalPackUn))+'|') GRADE from " & vbCrLf _
            '     & "(select c.docnum,c.docentry,c.u_docdate, b.U_ItemCode,case when len(rtrim(isnull(it.u_rrcode,'')))>0 then it.u_rrcode else b.U_ItemCode end u_rcode,it.u_rname,it.U_ColCode,it.u_qrname, it.U_Style,it.U_Size,it.SalPackUn, sum(b.U_AccpQty) qty from [@INsc_pdn1] b  " & vbCrLf _
            '     & "left join [@INsc_Opdn] c on c.DocEntry=b.DocEntry " & vbCrLf _
            '     & " left join oitm it on it.itemcode=b.U_ItemCode " & vbCrLf _
            '     & "left join NNM1 s with (nolock) on s.series=c.series " & vbCrLf _
            '     & "where c.DocNum=" & Val(txtdocno.Text) & " and s.Indicator='" & Trim(cmbperiod.Text) & "' and b.U_AccptWhs='GFINISH' " & vbCrLf _
            '     & "group by c.docnum,c.docentry,c.u_docdate, b.U_ItemCode,it.SalPackUn,case when len(rtrim(isnull(it.u_rrcode,'')))>0 then it.u_rrcode else b.U_ItemCode end ,it.u_rname,it.U_ColCode,it.u_qrname, it.U_Style,it.U_Size) k"

            msql = "select 0 sel,k.u_qrname itemcode,isnull(k.u_colcode,'') itemname,convert(decimal(18,0),k.Qty) QTY,'' SHADE," & vbCrLf _
                  & "(k.u_rcode+'|'+ISNULL(k.u_colcode,'')+'|'+''+'|'+convert(nvarchar(max),convert(int,k.SalPackUn))+'|') GRADE,k.docnum,k.yr from " & vbCrLf _
                  & "(select c.docnum,c.docentry,c.u_docdate, b.U_ItemCode,case when len(rtrim(isnull(it.u_rrcode,'')))>0 then it.u_rrcode else b.U_ItemCode end u_rcode,it.u_rname,it.U_ColCode,it.u_qrname, it.U_Style,it.U_Size,it.SalPackUn, sum(b.U_orderQty) qty,YEAR(c.u_docdate) yr  from [@INsc_jor1] b  " & vbCrLf _
                  & " left join [@INsc_Ojor] c on c.DocEntry=b.DocEntry " & vbCrLf _
                  & "left join oitm it on it.itemcode=b.U_ItemCode " & vbCrLf _
                  & "left join NNM1 s with (nolock) on s.series=c.series " & vbCrLf _
                  & "where c.DocNum=" & Val(txtdocno.Text) & " and s.Indicator='" & Trim(cmbperiod.Text) & "' and b.U_opercode='IRONGD' " & vbCrLf _
                  & "group by c.docnum,c.docentry,c.u_docdate, b.U_ItemCode,it.SalPackUn,case when len(rtrim(isnull(it.u_rrcode,'')))>0 then it.u_rrcode else b.U_ItemCode end ,it.u_rname,it.U_ColCode,it.u_qrname, it.U_Style,it.U_Size,YEAR(c.u_docdate) ) k"


            If con.State = ConnectionState.Broken Then
                con.Open()
            End If
            da = New OleDb.OleDbDataAdapter(msql, con)
            'da = New System.Data.SqlClient.SqlDataAdapter(msql, con)

            da.Fill(ds, "tbl1")
            loaddv.DataSource = ds.Tables("tbl1")
            loaddv.Select()
        ElseIf chkfob.Checked = True Then
            Dim da As OleDb.OleDbDataAdapter
            Dim ds As New DataSet

            msql = "select 0 sel, u_qrname ItemCode,upper(isnull(batchnum,'')) ItemName,sum(qty) qty,'' shade,GRADE,DocNum,yr  from (" & vbCrLf _
                & "select b.ItemCode,  (b.ItemCode + rtrim(b.u_purdesign)) col,b.Dscription ItemName,b.WhsCode,b.u_purdesign BatchNum,b.Quantity quantity," & vbCrLf _
                & "t6.SalPackUn,   convert(int,((b.Quantity) / t6.SalPackUn)) qty," & vbCrLf _
                & "(b.ItemCode+'|'+isnull(b.u_purdesign,'')+'|'+''+'|'+convert(nvarchar(max),convert(int,t6.SalPackUn))+'|') GRADE,t6.u_qrname,a.DocNum,YEAR(a.docdate) yr " & vbCrLf _
                & "from OPor A " & vbCrLf _
                & "left join Por1 b on b.DocEntry = a.DocEntry " & vbCrLf _
                & "left join OITM t6 on t6.ItemCode = b.ItemCode " & vbCrLf _
                & "where a.DocNum =" & Val(txtdocno.Text) & " and a.pindicator='" & cmbperiod.Text & "' ) A " & vbCrLf _
                & "group by ItemCode,col,ItemName,WhsCode,BatchNum,SalPackUn,GRADE,u_qrname,DocNum,yr " & vbCrLf _
                & "Order by itemcode"

            If con.State = ConnectionState.Broken Then
                con.Open()
            End If
            da = New OleDb.OleDbDataAdapter(msql, con)

            da.Fill(ds, "tbl1")
            loaddv.DataSource = ds.Tables("tbl1")
            loaddv.Select()
        ElseIf chkstall.Checked = True Then
            Dim da As OleDb.OleDbDataAdapter
            Dim ds As New DataSet

            msql = "select 0 sel, b.DocNum,b.DocDate,b.DocEntry, c.ItemCode, c.Dscription ,it.U_BrandGroup Itemname,it.U_Style Style,it.U_Size Size," & vbCrLf _
             & "isnull(it.U_ColCode,'') colcode, sum(c.Quantity) quantity,isnull(pr.Price,0) as rate,'Seconds' stype,tb.ItmsGrpNam as grp,it.SalUnitMsr," & vbCrLf _
             & "it.NumInSale,'1N' as qty from WTR1 c" & vbCrLf _
             & "left join OWTR b on b.DocEntry=c.docentry " & vbCrLf _
             & "left join OCRD d on d.CardCode=b.CardCode " & vbCrLf _
             & "left join oitm it on it.ItemCode=c.ItemCode " & vbCrLf _
             & "left join oitb tb on tb.ItmsGrpCod=it.ItmsGrpCod " & vbCrLf _
             & "left join ITM1 pr on pr.ItemCode=it.ItemCode and pr.PriceList=d.U_MRPListnum" & vbCrLf _
             & "where b.docnum=" & Val(txtdocno.Text) & " and b.pIndicator='" & cmbperiod.Text & "'" & vbCrLf _
             & "group by b.DocNum,b.DocDate,b.DocEntry, c.ItemCode,c.Dscription,it.U_BrandGroup,it.U_Style,it.U_Size,it.U_ColCode, pr.Price,tb.ItmsGrpNam,it.SalUnitMsr,it.NumInSale "

            'msql = "select 0 sel, u_qrname ItemCode,upper(isnull(batchnum,'')) ItemName,sum(qty) qty,'' shade,GRADE,DocNum,yr  from (" & vbCrLf _
            '    & "select b.ItemCode,  (b.ItemCode + rtrim(b.u_purdesign)) col,b.Dscription ItemName,b.WhsCode,b.u_purdesign BatchNum,b.Quantity quantity," & vbCrLf _
            '    & "t6.SalPackUn,   convert(int,((b.Quantity) / t6.SalPackUn)) qty," & vbCrLf _
            '    & "(b.ItemCode+'|'+isnull(b.u_purdesign,'')+'|'+''+'|'+convert(nvarchar(max),convert(int,t6.SalPackUn))+'|') GRADE,t6.u_qrname,a.DocNum,YEAR(a.docdate) yr " & vbCrLf _
            '    & "from OPor A " & vbCrLf _
            '    & "left join Por1 b on b.DocEntry = a.DocEntry " & vbCrLf _
            '    & "left join OITM t6 on t6.ItemCode = b.ItemCode " & vbCrLf _
            '    & "where a.DocNum =" & Val(txtdocno.Text) & " and a.pindicator='" & cmbperiod.Text & "' ) A " & vbCrLf _
            '    & "group by ItemCode,col,ItemName,WhsCode,BatchNum,SalPackUn,GRADE,u_qrname,DocNum,yr " & vbCrLf _
            '    & "Order by itemcode"

            If con.State = ConnectionState.Broken Then
                con.Open()
            End If
            da = New OleDb.OleDbDataAdapter(msql, con)

            da.Fill(ds, "tbl2")
            loaddv.DataSource = ds.Tables("tbl2")
            loaddv.Select()

        ElseIf chkATC.Checked = True Then
            Dim da As OleDb.OleDbDataAdapter
            Dim ds As New DataSet

            'msql = "select 0 sel, b.DocNum,b.DocDate,b.DocEntry, c.ItemCode, c.Dscription ,it.U_BrandGroup Itemname,it.U_Style Style,it.U_Size Size," & vbCrLf _
            ' & "isnull(it.U_ColCode,'') colcode, sum(c.Quantity) quantity,isnull(pr.Price,0) as rate,'Seconds' stype,tb.ItmsGrpNam as grp,it.SalUnitMsr," & vbCrLf _
            ' & "it.NumInSale,'1N' as qty from WTR1 c" & vbCrLf _
            ' & "left join OWTR b on b.DocEntry=c.docentry " & vbCrLf _
            ' & "left join OCRD d on d.CardCode=b.CardCode " & vbCrLf _
            ' & "left join oitm it on it.ItemCode=c.ItemCode " & vbCrLf _
            ' & "left join oitb tb on tb.ItmsGrpCod=it.ItmsGrpCod " & vbCrLf _
            ' & "left join ITM1 pr on pr.ItemCode=it.ItemCode and pr.PriceList=d.U_MRPListnum" & vbCrLf _
            ' & "where b.docnum=" & Val(txtdocno.Text) & " and b.pIndicator='" & cmbperiod.Text & "'" & vbCrLf _
            ' & "group by b.DocNum,b.DocDate,b.DocEntry, c.ItemCode,c.Dscription,it.U_BrandGroup,it.U_Style,it.U_Size,it.U_ColCode, pr.Price,tb.ItmsGrpNam,it.SalUnitMsr,it.NumInSale "

            'msql = "select 0 as sel,b.ItemCode,isnull(it.u_atccode,'') atccode,it.ItemName,sum(b.quantity) quantity, 1 qty,it.u_colcode,c.DocNum," & vbCrLf _
            '     & "c.DocDate,d.u_ocardcode Vendorcode, (isnull(it.u_atccode,'')+'|'+isnull(d.u_ocardcode,'')+'/'+convert(varchar(20),c.docnum)+'/'+CONVERT(varchar,c.DocDate,112)+'/'+isnull(it.u_colcode,'') + '||1|') QR from dln1 b " & vbCrLf _
            '     & "left join odln c on c.docentry=b.docentry " & vbCrLf _
            '     & "left join OCRD d on d.CardCode=c.CardCode " & vbCrLf _
            '     & " left join oitm it on it.ItemCode=b.ItemCode " & vbCrLf _
            '     & "where c.docnum=" & Val(txtdocno.Text) & " and c.pIndicator='" & cmbperiod.Text & "' " & vbCrLf _
            '     & "group by b.ItemCode,it.U_atccode,it.ItemName,c.DocNum,c.DocDate,it.u_colcode,d.u_ocardcode"

            msql = "select 0 as sel,b.ItemCode,isnull(it.u_atccode,'') atccode,it.ItemName,sum(b.quantity) quantity, 1 qty,it.u_colcode,c.DocNum," & vbCrLf _
            & "c.DocDate,d.u_ocardcode Vendorcode, (isnull(it.u_atccode,'')+'|'+isnull(d.u_ocardcode,'')+'/'+convert(varchar(20),c.docnum)+'/'+CONVERT(varchar,c.DocDate,112)+'/'+isnull(it.u_colcode,'') + '||1|') QR from inv1 b " & vbCrLf _
            & "left join oinv c on c.docentry=b.docentry " & vbCrLf _
            & "left join OCRD d on d.CardCode=c.CardCode " & vbCrLf _
            & " left join oitm it on it.ItemCode=b.ItemCode " & vbCrLf _
            & "where c.docnum=" & Val(txtdocno.Text) & " and c.pIndicator='" & cmbperiod.Text & "' " & vbCrLf _
            & "group by b.ItemCode,it.U_atccode,it.ItemName,c.DocNum,c.DocDate,it.u_colcode,d.u_ocardcode"

            If con.State = ConnectionState.Broken Then
                con.Open()
            End If
            da = New OleDb.OleDbDataAdapter(msql, con)

            da.Fill(ds, "tbl2")
            loaddv.DataSource = ds.Tables("tbl2")
            loaddv.Select()


        ElseIf chkgrn.Checked = True Then
            'Dim da As New System.Data.SqlClient.SqlDataAdapter, 
            Dim ds As New DataSet
            Dim da As New OleDb.OleDbDataAdapter
            da.SelectCommand = New OleDb.OleDbCommand
            da.SelectCommand.Connection = con
            da.SelectCommand.CommandType = CommandType.StoredProcedure
            da.SelectCommand.CommandText = "[@qrbarcodeGRNWise]"
            da.SelectCommand.Parameters.Add("@Docnum", OleDbType.Integer).Value = Val(txtdocno.Text)
            da.SelectCommand.Parameters.Add("@yr", OleDbType.VarChar).Value = cmbperiod.Text

            da.Fill(ds, "tbl1")
            loaddv.DataSource = ds.Tables("tbl1")
            loaddv.Select()

        Else
            If Len(Trim(cmbbrand.Text)) > 0 Then
                'Dim da As System.Data.SqlClient.SqlDataAdapter, 
                Dim ds As New DataSet
                Dim da As OleDb.OleDbDataAdapter

                'msql = "select l.sel,l.itemcode,l.itemname,l.qty,l.shade,l.grade from " & vbCrLf _
                '      & "(select 0 SEL,T0.u_qrname as ItemCode,ISNULL(T1.[BatchNum],'') ITEMNAME, CASE when isnull(T1.[Quantity],0)>0 then convert(decimal(18,0), T1.[Quantity]) else  convert(decimal(18,0), T3.onhand) end QTY," & vbCrLf _
                '      & "'' SHADE,(T0.ItemCode+'|'+ISNULL(T1.[BatchNum],'')+'|'+''+'|'+convert(nvarchar(max),convert(int,T0.SalPackUn))+'|') GRADE" & vbCrLf _
                '      & " FROM OITM T0 " & vbCrLf _
                '      & "left JOIN OIBT T1 ON T0.ItemCode = T1.ItemCode" & vbCrLf _
                '      & "left JOIN OWHS T2 ON T1.WhsCode = T2.WhsCode" & vbCrLf _
                '      & "left join OITW t3 on t3.ItemCode=t0.ItemCode and t3.WhsCode='SALGOODS'" & vbCrLf _
                '      & "where t0.U_BrandGroup='" & cmbbrand.Text & " ') l  where(l.QTY > 0) Order by l.[ItemCode]"


                msql = "select l.sel,l.itemcode,l.itemname,l.qty,l.shade, (l.oItemCode+'|'+ISNULL(l.itemname,'')+'|'+''+'|'+convert(nvarchar(max),convert(int,l.SalPackUn))+'|') GRADE from  " & vbCrLf _
                      & "(select 0 SEL,t0.ItemCode oitemcode, T0.u_qrname as ItemCode,'' ITEMNAME,  convert(decimal(18,0), T3.onhand)  QTY, '' SHADE,T0.SalPackUn,t0.u_brandgroup FROM OITM T0 " & vbCrLf _
                      & "left join OITW t3 on t3.ItemCode=t0.ItemCode and t3.WhsCode='SALGOODS'  where t0.ManBtchNum='N' and t3.onhand>0" & vbCrLf _
                      & " union all " & vbCrLf _
                      & "select 0 SEL,t0.ItemCode oitemcode,T0.u_qrname as ItemCode,ISNULL(T1.[BatchNum],'') ITEMNAME,  sum(convert(decimal(18,0), T1.quantity))  QTY, '' SHADE,T0.SalPackUn,t0.u_brandgroup FROM OITM T0 " & vbCrLf _
                      & "inner JOIN OIBT T1 ON T1.ItemCode = T0.ItemCode and t1.WhsCode='SALGOODS'" & vbCrLf _
                      & "where t0.ManBtchNum='Y' " & vbCrLf _
                      & "group by T0.u_qrname,t0.ItemCode ,ISNULL(T1.[BatchNum],''),t0.u_brandgroup,T0.SalPackUn having sum(convert(decimal(18,0), T1.quantity))>0) l " & vbCrLf _
                      & "where l.U_BrandGroup='" & cmbbrand.Text & "'" & vbCrLf _
                      & "order by l.itemcode,l.itemname"


                If con.State = ConnectionState.Broken Then
                    con.Open()
                End If
                da = New OleDb.OleDbDataAdapter(msql, con)

                da.Fill(ds, "tbl1")
                loaddv.DataSource = ds.Tables("tbl1")
                loaddv.Select()



            Else


                'Dim da As New System.Data.SqlClient.SqlDataAdapter, 
                Dim ds As New DataSet
                Dim da As New OleDb.OleDbDataAdapter
                da.SelectCommand = New OleDb.OleDbCommand
                da.SelectCommand.Connection = con
                da.SelectCommand.CommandType = CommandType.StoredProcedure
                da.SelectCommand.CommandText = "[@qrbarcodelist]"
                da.SelectCommand.Parameters.Add("@itemcode", OleDbType.VarChar).Value = TextBox1.Text

                da.Fill(ds, "tbl1")
                loaddv.DataSource = ds.Tables("tbl1")
                loaddv.Select()
            End If
        End If



    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        If chkstall.Checked = True Then
            loadstall()
        ElseIf chkATC.Checked = True Then
            loadATC()
        Else
            loadprn()
        End If

        'Call loadprn()

        'Dim dir, mdir As String
        'dir = System.AppDomain.CurrentDomain.BaseDirectory()
        'mdir = Trim(dir) & "Qrbarcode.txt"

        'FileOpen(1, mdir, OpenMode.Output)
        'lin = 0
        'Dim IQR As Integer = 0

        'Call MAIN()
        'Dim da As New SqlDataAdapter, ds As New DataSet
        'da.SelectCommand = New SqlCommand
        'da.SelectCommand.Connection = con
        'da.SelectCommand.CommandType = CommandType.Text
        'da.SelectCommand.CommandText = "SELECT LineId FROM [QRIndentity]"
        'da.Fill(ds, "tbl2")
        'Dim dt As DataTable = ds.Tables("tbl2")

        'IQR = dt.Rows(0)("LineId")



        'For Each row As DataGridViewRow In loaddv.Rows
        '    If row.Cells.Item("SEL").Value = 1 Then

        '        'Dim delimiter As Char = "/"
        '        'Dim strArr As String() = row.Cells.Item("ITEMNAME").Value.Split(delimiter)
        '        Dim colour As String = ""
        '        'For ii As Integer = 0 To strArr.Length - 1
        '        '    colour = strArr(2).ToString()

        '        'Next
        '        colour = row.Cells.Item("ITEMNAME").Value


        '        For value As Integer = 0 To (Math.Floor((row.Cells.Item("QTY").Value) / 5)) - 1


        '            'EnryptString(TextBox1.Text.Trim)



        '            '  If (row.Cells.Item("QTY").Value) Then
        '            PrintLine(1, TAB(0), "^XA")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^LH0,0^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^LL304")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^MD0")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^MNY")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^LH0,0^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^XA")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO20,15^BQN,4,4^FD000" & EnryptString(row.Cells.Item("GRADE").Value.ToString & IQR) & "^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO20,135^A0N,20,20^CI13^FR^FD" & row.Cells.Item("itemcode").Value & colour & "^FS ")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO185,15^BQN,4,4^FD000" & EnryptString(row.Cells.Item("GRADE").Value.ToString & IQR + 1) & "^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO175,135^A0N,20,20^CI13^FR^FD" & row.Cells.Item("itemcode").Value & colour & "^FS ")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO345,15^BQN,4,4^FD000" & EnryptString(row.Cells.Item("GRADE").Value.ToString & IQR + 2) & "^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO335,135^A0N,20,20^CI13^FR^FD" & row.Cells.Item("itemcode").Value & colour & "^FS ")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO510,15^BQN,4,4^FD000" & EnryptString(row.Cells.Item("GRADE").Value.ToString & IQR + 3) & "^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO500,135^A0N,20,20^CI13^FR^FD" & row.Cells.Item("itemcode").Value & colour & "^FS ")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO670,15^BQN,4,4^FD000" & EnryptString(row.Cells.Item("GRADE").Value.ToString & IQR + 4) & "^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO660,135^A0N,20,20^CI13^FR^FD " & row.Cells.Item("itemcode").Value & colour & "^FS ")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^PQ1,0,0,N")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^XZ")
        '            IQR = IQR + 5

        '        Next


        '        If (row.Cells.Item("QTY").Value - (Math.Floor((row.Cells.Item("QTY").Value) / 5)) * 5) = 4 Then



        '            '  If (row.Cells.Item("QTY").Value) Then
        '            PrintLine(1, TAB(0), "^XA")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^LH0,0^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^LL304")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^MD0")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^MNY")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^LH0,0^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^XA")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO20,15^BQN,4,4^FD000" & EnryptString(row.Cells.Item("GRADE").Value.ToString & IQR) & "^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO10,135^A0N,20,20^CI13^FR^FD" & row.Cells.Item("itemcode").Value & colour & "^FS ")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO185,15^BQN,4,4^FD000" & EnryptString(row.Cells.Item("GRADE").Value.ToString & IQR + 1) & "^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO185,135^A0N,20,20^CI13^FR^FD" & row.Cells.Item("itemcode").Value & colour & "^FS ")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO345,15^BQN,4,4^FD000" & EnryptString(row.Cells.Item("GRADE").Value.ToString & IQR + 2) & "^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO345,135^A0N,20,20^CI13^FR^FD" & row.Cells.Item("itemcode").Value & colour & "^FS ")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO505,15^BQN,4,4^FD000" & EnryptString(row.Cells.Item("GRADE").Value.ToString & IQR + 3) & "^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO510,135^A0N,20,20^CI13^FR^FD" & row.Cells.Item("itemcode").Value & colour & "^FS ")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^PQ1,0,0,N")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^XZ")
        '            IQR = IQR + 5

        '        ElseIf (row.Cells.Item("QTY").Value - (Math.Floor((row.Cells.Item("QTY").Value) / 5)) * 5) = 3 Then
        '            PrintLine(1, TAB(0), "^XA")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^LH0,0^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^LL304")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^MD0")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^MNY")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^LH0,0^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^XA")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO20,15^BQN,4,4^FD000" & EnryptString(row.Cells.Item("GRADE").Value.ToString & IQR) & "^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO20,135^A0N,20,20^CI13^FR^FD" & row.Cells.Item("itemcode").Value & colour & "^FS ")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO185,15^BQN,4,4^FD000" & EnryptString(row.Cells.Item("GRADE").Value.ToString & IQR + 1) & "^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO185,135^A0N,20,20^CI13^FR^FD" & row.Cells.Item("itemcode").Value & colour & "^FS ")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO335,15^BQN,4,4^FD000" & EnryptString(row.Cells.Item("GRADE").Value.ToString & IQR + 2) & "^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO335,135^A0N,20,20^CI13^FR^FD" & row.Cells.Item("itemcode").Value & colour & "^FS ")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^PQ1,0,0,N")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^XZ")
        '            IQR = IQR + 5

        '        ElseIf (row.Cells.Item("QTY").Value - (Math.Floor((row.Cells.Item("QTY").Value) / 5)) * 5) = 2 Then
        '            PrintLine(1, TAB(0), "^XA")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^LH0,0^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^LL304")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^MD0")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^MNY")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^LH0,0^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^XA")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO20,15^BQN,4,4^FD000" & EnryptString(row.Cells.Item("GRADE").Value.ToString & IQR) & "^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO20,135^A0N,20,20^CI13^FR^FD" & row.Cells.Item("itemcode").Value & colour & "^FS ")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO185,15^BQN,4,4^FD000" & EnryptString(row.Cells.Item("GRADE").Value.ToString & IQR + 1) & "^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO185,135^A0N,20,20^CI13^FR^FD" & row.Cells.Item("itemcode").Value & colour & "^FS ")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^PQ1,0,0,N")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^XZ")
        '            IQR = IQR + 5

        '        ElseIf (row.Cells.Item("QTY").Value - (Math.Floor((row.Cells.Item("QTY").Value) / 5)) * 5) = 1 Then

        '            PrintLine(1, TAB(0), "^XA")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^LH0,0^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^LL304")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^MD0")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^MNY")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^LH0,0^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^XA")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO20,15^BQN,4,4^FD000" & EnryptString(row.Cells.Item("GRADE").Value.ToString & IQR) & "^FS")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^FO20,135^A0N,20,20^CI13^FR^FD" & row.Cells.Item("itemcode").Value & colour & "^FS ")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^PQ1,0,0,N")
        '            lin = lin + 1
        '            PrintLine(1, TAB(0), "^XZ")
        '            IQR = IQR + 5

        '        End If


        '    End If


        'Next



        'Dim com As New SqlCommand
        'com.Connection = con
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If
        'com.CommandText = "UPDATE [QRIndentity] SET LINEID = '" & IQR & "'"
        'com.ExecuteNonQuery()
        'com.Dispose()


        'FileClose(1)



        'dir = System.AppDomain.CurrentDomain.BaseDirectory()
        'mdir = Trim(dir) & "Qrbarcode.txt"
        ''Shell("cmd.exe /c" & "type " & mdir & " > lpt" & Trim(TextBox2.Text))
        ''Shell("print /d:LPT" & Trim(TextBox2.Text) & mdir, vbNormalFocus)
        ''        Shell("cmd.exe /c" & "type " & mdir & " > lpt1")
    End Sub

    'Private Sub barcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    TextBox1.Text = ""
    '    TextBox2.Text = "1"
    'End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        For i As Integer = 0 To loaddv.Rows.Count - 2
            If loaddv.Rows(i).Cells(0).Value = False Then
                loaddv.Rows(i).Cells(0).Value = True
            Else
                loaddv.Rows(i).Cells(0).Value = False
            End If
        Next

    End Sub

    'Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
    '    Dim conn As OleDbConnection
    '    Dim dtr As OleDbDataReader
    '    Dim dta As OleDbDataAdapter
    '    Dim cmd As OleDbCommand
    '    Dim dts As DataSet
    '    Dim excel As String
    '    Dim OpenFileDialog As New OpenFileDialog

    '    OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    '    OpenFileDialog.Filter = "All Files (*.*)|*.*|Excel files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv|XLS Files (*.xls)|*xls"

    '    If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then

    '        Dim fi As New FileInfo(OpenFileDialog.FileName)
    '        Dim FileName As String = OpenFileDialog.FileName

    '        excel = fi.FullName
    '        conn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excel + ";Extended Properties=Excel 12.0;")
    '        dta = New OleDbDataAdapter("Select * From [Sheet1$]", conn)
    '        dts = New DataSet
    '        dta.Fill(dts, "[Sheet1$]")
    '        loaddv.DataSource = dts
    '        loaddv.DataMember = "[Sheet1$]"
    '        conn.Close()

    '    End If

    'End Sub


    Private Sub RRQRCODE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        Call main()
        chkcrypt.Checked = True
        CHKDIRPRN.Checked = False
        'Dim da As New SqlDataAdapter, 
        Dim ds As New DataSet

        Dim da As New OleDb.OleDbDataAdapter
        da.SelectCommand = New OleDb.OleDbCommand
        da.SelectCommand.Connection = con
        da.SelectCommand.CommandType = CommandType.Text
        da.SelectCommand.CommandText = "SELECT DISTINCT a.itemcode,ItemName  FROM OITW A left join oitm b on b.itemcode = a.itemcode where ISNULL(ItemName,'') <> '' oRDER BY ItemName"
        da.Fill(ds, "tbl2")
        Brand.DataSource = ds.Tables("tbl2")
        Brand.DisplayMember = "ITEMNAME"
        Brand.ValueMember = "itemcode"
        TextBox1.Text = Brand.SelectedValue.ToString

        Brand.Text = ""
        Call loadcombo("ofpr", "indicator", cmbperiod, "indicator")
        Call loadcombo("oitm", "u_brandgroup", cmbbrand, "u_brandgroup")


        'Dim sql2 As String
        'sql2 = "select isnull(itemcode,'') name from oitm"
        'Dim BC As String
        'Dim CMDB As New OleDb.OleDbCommand(sql2, con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'Dim DR2 As OleDb.OleDbDataReader
        'DR2 = CMDB.ExecuteReader
        'If DR2.HasRows = True Then
        '    While DR2.Read
        '        BC = DR2.Item("NAME")
        '        Brand.Items.Add(BC)
        '    End While
        'End If
        'DR2.Close()
        'CMDB.Dispose()
    End Sub

    Private Sub Brand_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Brand.SelectedIndexChanged
        TextBox1.Text = Brand.SelectedValue.ToString
    End Sub

    Private Sub loaddv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles loaddv.CellContentClick

    End Sub

    Private Sub Button3_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button3.MouseClick

    End Sub

    Private Sub loaddv_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles loaddv.CellMouseClick

        If loaddv.CurrentCell.ColumnIndex = 0 Then

            If loaddv.Rows(loaddv.CurrentRow.Index).Cells(0).Value = 1 Then
                Dim row1 As DataRow = Nothing
                table1.Columns.Add("nos")
                table1.Columns.Add("BatchNo")
                row1 = table1.NewRow()
                row1("nos") = loaddv.Rows(loaddv.CurrentRow.Index).Cells(3).Value
                row1("BatchNo") = loaddv.Rows(loaddv.CurrentRow.Index).Cells(4).Value
                table1.Rows.Add(row1)

            End If
        End If







    End Sub





    Public Function DecryptString(ByVal encrString As String) As String
        Dim b As Byte()
        Dim decrypted As String

        Try
            b = Convert.FromBase64String(encrString)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b)
        Catch fe As FormatException
            decrypted = ""
        End Try

        Return decrypted
    End Function

    Public Function EnryptString(ByVal strEncrypted As String) As String
        Dim b As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted)
        Dim encrypted As String = Convert.ToBase64String(b)
        Return encrypted
    End Function

    'Public Function encrypt(ByVal encryptString As String) As String
    '    Dim EncryptionKey As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    '    Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(encryptString)

    '    Using encryptor As Aes = Aes.Create()
    '        Dim pdb As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
    '        encryptor.Key = pdb.GetBytes(32)
    '        encryptor.IV = pdb.GetBytes(16)

    '        Using ms As MemoryStream = New MemoryStream()

    '            Using cs As CryptoStream = New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
    '                cs.Write(clearBytes, 0, clearBytes.Length)
    '                cs.Close()
    '            End Using

    '            encryptString = Convert.ToBase64String(ms.ToArray())
    '        End Using
    '    End Using

    '    Return encryptString
    'End Function

    'Public Function Decrypt(ByVal cipherText As String) As String
    '    Dim EncryptionKey As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    '    cipherText = cipherText.Replace(" ", "+")
    '    Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)

    '    Using encryptor As Aes = Aes.Create()
    '        Dim pdb As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
    '        encryptor.Key = pdb.GetBytes(32)
    '        encryptor.IV = pdb.GetBytes(16)

    '        Using ms As MemoryStream = New MemoryStream()

    '            Using cs As CryptoStream = New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
    '                cs.Write(cipherBytes, 0, cipherBytes.Length)
    '                cs.Close()
    '            End Using

    '            cipherText = Encoding.Unicode.GetString(ms.ToArray())
    '        End Using
    '    End Using

    '    Return cipherText
    'End Function

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MsgBox(DecryptString("Q1NTNDA2fFYwMjA4Ny9DU1MvSzl8fDF8NjQw"))
    End Sub

    Private Sub loadprn()
        'Dim dir, mdir As String
        Dim mtbox, j, txtbno As Integer
        Dim sno As Integer

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "Qrbarcode.txt"


        'Dim dir As String
        ''dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        ''mdir = Trim(dir) & "\sbarcodE.txt"

        'dir = System.AppDomain.CurrentDomain.BaseDirectory()
        'mdir = Trim(dir) & "sbarcodE.txt"

        'FileOpen(1, "c:\sbarcodE.TXT", OpenMode.Output)
        If CHKDIRPRN.Checked = True Then
            FileOpen(1, "LPT" & Trim(TextBox2.Text), OpenMode.Output)
        Else
            FileOpen(1, mdir, OpenMode.Output)
        End If

        lin = 0
        Dim IQR As Integer = 0

        'Call MAIN()
        'Dim da As New SqlDataAdapter, 
        Dim ds As New DataSet
        Dim da As New OleDb.OleDbDataAdapter
        da.SelectCommand = New OleDb.OleDbCommand
        da.SelectCommand.Connection = con
        da.SelectCommand.CommandType = CommandType.Text
        da.SelectCommand.CommandText = "SELECT LineId FROM [QRIndentity]"
        da.Fill(ds, "tbl2")
        Dim dt As DataTable = ds.Tables("tbl2")
        txtbno = 0
        IQR = dt.Rows(0)("LineId")
        sno = 1
        PrintLine(1, TAB(0), "^XA")
        lin = lin + 1
        PrintLine(1, TAB(0), "^PRC")
        lin = lin + 1
        PrintLine(1, TAB(0), "^LH0,0^FS")
        lin = lin + 1
        PrintLine(1, TAB(0), "^LL304")
        lin = lin + 1
        PrintLine(1, TAB(0), "^MD0")
        lin = lin + 1
        PrintLine(1, TAB(0), "^MNY")
        lin = lin + 1
        PrintLine(1, TAB(0), "^LH0,0^FS")
        lin = lin + 1
        'PrintLine(1, TAB(0), "^XA")
        'lin = lin + 1


        For Each row As DataGridViewRow In loaddv.Rows
            If row.Cells.Item("SEL").Value = 1 Then

                'Dim delimiter As Char = "/"
                'Dim strArr As String() = row.Cells.Item("ITEMNAME").Value.Split(delimiter)
                Dim colour As String = ""
                'For ii As Integer = 0 To strArr.Length - 1
                '    colour = strArr(2).ToString()
                'Next
                colour = row.Cells.Item("ITEMNAME").Value & vbNullString
                For j = 1 To Val(row.Cells.Item("QTY").Value)
                    'For value As Integer = 0 To (Math.Floor((row.Cells.Item("QTY").Value) / 5)) - 1


                    'EnryptString(TextBox1.Text.Trim)



                    '  If (row.Cells.Item("QTY").Value) Then
                    'PrintLine(1, TAB(0), "^XA")
                    'lin = lin + 1
                    'PrintLine(1, TAB(0), "^LH0,0^FS")
                    'lin = lin + 1
                    'PrintLine(1, TAB(0), "^LL304")
                    'lin = lin + 1
                    'PrintLine(1, TAB(0), "^MD0")
                    'lin = lin + 1
                    'PrintLine(1, TAB(0), "^MNY")
                    'lin = lin + 1
                    'PrintLine(1, TAB(0), "^LH0,0^FS")
                    'lin = lin + 1
                    'PrintLine(1, TAB(0), "^XA")
                    'lin = lin + 1
                    If chkcrypt.Checked = True Then
                        If Len(Trim(sEncript(row.Cells.Item("GRADE").Value.ToString & IQR))) > 32 Then
                            PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + Val(txtbno))) & " ,15^BQN,3,3^FD000" & sEncript(row.Cells.Item("GRADE").Value.ToString & IQR) & "^FS")
                        Else
                            PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + Val(txtbno))) & " ,15^BQN,4,4^FD000" & sEncript(row.Cells.Item("GRADE").Value.ToString & IQR) & "^FS")
                        End If

                    Else
                        If Len(Trim(row.Cells.Item("GRADE").Value.ToString & IQR)) > 32 Then
                            PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + Val(txtbno))) & " ,15^BQN,3,3^FD000" & row.Cells.Item("GRADE").Value.ToString & IQR & "^FS")
                        Else
                            PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + Val(txtbno))) & " ,15^BQN,4,4^FD000" & row.Cells.Item("GRADE").Value.ToString & IQR & "^FS")
                        End If

                    End If

                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(130 + (Val(txtbno)))) & ",35^A0R,25,25^CI13^FR^FD" & UCase(colour) & "^FS ")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(15 + (Val(txtbno)))) & ",129^A0N,20,20^CI13^FR^FD" & Mid(row.Cells.Item("itemcode").Value, 1, InStr(row.Cells.Item("itemcode").Value, "_") - 1) & "^FS ")
                    lin = lin + 1
                    If chksc.Checked = True Then
                        PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + (Val(txtbno)))) & ",149^A0N,20,20^CI13^FR^FD" & Trim(Mid(row.Cells.Item("itemcode").Value, InStr(row.Cells.Item("itemcode").Value, "_") + 1, 6)) & "/" & row.Cells.Item("docnum").Value & "J" & getyrchr(Val(row.Cells.Item("yr").Value)) & "^FS ")
                    ElseIf chkih.Checked = True Then
                        PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + (Val(txtbno)))) & ",149^A0N,20,20^CI13^FR^FD" & Trim(Mid(row.Cells.Item("itemcode").Value, InStr(row.Cells.Item("itemcode").Value, "_") + 1, 6)) & "/" & row.Cells.Item("docnum").Value & "I" & getyrchr(Val(row.Cells.Item("yr").Value)) & "^FS ")
                    ElseIf chkgrn.Checked = True Then
                        PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + (Val(txtbno)))) & ",149^A0N,20,20^CI13^FR^FD" & Trim(Mid(row.Cells.Item("itemcode").Value, InStr(row.Cells.Item("itemcode").Value, "_") + 1, 6)) & "/" & row.Cells.Item("docnum").Value & "G" & getyrchr(Val(row.Cells.Item("yr").Value)) & "^FS ")
                    ElseIf chkfob.Checked = True Then
                        PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + (Val(txtbno)))) & ",149^A0N,20,20^CI13^FR^FD" & Trim(Mid(row.Cells.Item("itemcode").Value, InStr(row.Cells.Item("itemcode").Value, "_") + 1, 6)) & "/" & row.Cells.Item("docnum").Value & "F" & getyrchr(Val(row.Cells.Item("yr").Value)) & "^FS ")
                    Else
                        PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + (Val(txtbno)))) & ",149^A0N,20,20^CI13^FR^FD" & Trim(Mid(row.Cells.Item("itemcode").Value, InStr(row.Cells.Item("itemcode").Value, "_") + 1, 6)) & "^FS ")
                    End If

                    lin = lin + 1

                    '^FO20,149^A0N,20,20^CI13^FR^FD38F^FS
                    IQR = IQR + 1
                    txtbno = txtbno + 160
                    If sno = 5 Then
                        PrintLine(1, TAB(0), "^PQ1,0,0,N")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^XZ")

                        PrintLine(1, TAB(0), "^XA")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^PRC")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^LH0,0^FS")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^LL304")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^MD0")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^MNY")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^LH0,0^FS")
                        lin = lin + 1
                        'PrintLine(1, TAB(0), "^XA")
                        'lin = lin + 1
                        txtbno = 0
                        sno = 0
                    End If
                    sno = sno + 1

                    'PrintLine(1, TAB(0), "^PQ1,0,0,N")
                    'lin = lin + 1
                    'PrintLine(1, TAB(0), "^XZ")
                    ' IQR = IQR + 5

                Next j


            End If


        Next
        PrintLine(1, TAB(0), "^PQ1,0,0,N")
        lin = lin + 1
        PrintLine(1, TAB(0), "^XZ")


        'Dim com As New SqlCommand
        Dim com As New OleDb.OleDbCommand
        com.Connection = con
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        com.CommandText = "UPDATE [QRIndentity] SET LINEID = '" & IQR & "'"
        com.ExecuteNonQuery()
        com.Dispose()


        FileClose(1)


        If MsgBox("Ok", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            'dir = System.AppDomain.CurrentDomain.BaseDirectory()
            'mdir = Trim(dir) & "Qrbarcode.txt"
            'Shell("cmd.exe /c" & "type " & mdir & " > lpt" & Trim(TextBox2.Text))
            'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(TextBox2.Text) & ":")
            Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(TextBox2.Text))

            'Dim proc As Process = New Process
            'proc.StartInfo.FileName = "cmd.exe "
            'proc.StartInfo.Arguments = "  /c type " & mdir & " > lpt" & Trim(TextBox2.Text)
            'proc.Start()

        End If

        'Shell("print /d:LPT" & Trim(TextBox2.Text) & mdir, vbNormalFocus)
        '        Shell("cmd.exe /c" & "type " & mdir & " > lpt1")
    End Sub

    Private Sub Button5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'TextBox4.Text = EncryptData(TextBox3.Text)
        'TextBox4.Text = EncodeString(TextBox3.Text)
        'TextBox4.Text = DecodeString(TextBox3.Text)
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub chkih_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkih.CheckedChanged
        If chkih.Checked = True Then
            Brand.Text = ""
            cmbbrand.Text = ""
            chksc.Checked = False
            chkgrn.Checked = False
            chkfob.Checked = False
            chkstall.Checked = False
            chkATC.Checked = False
        End If
    End Sub

    Private Sub chksc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chksc.CheckedChanged
        If chksc.Checked = True Then
            Brand.Text = ""
            cmbbrand.Text = ""
            chkih.Checked = False
            chkgrn.Checked = False
            chkfob.Checked = False
            chkstall.Checked = False
            chkATC.Checked = False
        End If
    End Sub

    Private Sub Button5_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'MsgBox(Asc(TextBox5.Text))
        MsgBox(getyrchr(Val(txtdocno.Text)))

        'MsgBox(Asc("H"))
        'MsgBox(Chr((Asc("H") + 1)))
        'TextBox4.Text = sEncript(TextBox3.Text)
        'TextBox5.Text = sDecode(TextBox4.Text)
        'cusDecrypt()
    End Sub

    Private Sub cmbbrand_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbbrand.SelectedIndexChanged

    End Sub

    Private Sub chkgrn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkgrn.CheckedChanged
        If chkgrn.Checked = True Then
            Brand.Text = ""
            cmbbrand.Text = ""
            chksc.Checked = False
            chkih.Checked = False
            chkfob.Checked = False
            chkstall.Checked = False
            chkATC.Checked = False
        End If
    End Sub

    Private Sub chkfob_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkfob.CheckedChanged
        If chkfob.Checked = True Then
            Brand.Text = ""
            cmbbrand.Text = ""
            chksc.Checked = False
            chkih.Checked = False
            chkgrn.Checked = False
            chkstall.Checked = False
            chkATC.Checked = False
        End If
    End Sub

    Private Sub Button1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.GotFocus
        If Len(Trim(Brand.Text)) > 0 Or Len(Trim(cmbbrand.Text)) > 0 Then
            chkih.Checked = False
            chksc.Checked = False
            chkgrn.Checked = False
            chkfob.Checked = False
        End If
    End Sub

    Private Sub loadprn2()
        Dim dir, mdir As String
        Dim mtbox, j, txtbno As Integer
        Dim sno As Integer

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "Qrbarcode.txt"

        FileOpen(1, mdir, OpenMode.Output)
        lin = 0
        Dim IQR As Integer = 0

        Call MAIN()
        'Dim da As New SqlDataAdapter, 
        Dim ds As New DataSet
        Dim da As New OleDb.OleDbDataAdapter

        da.SelectCommand = New OleDb.OleDbCommand
        da.SelectCommand.Connection = con
        da.SelectCommand.CommandType = CommandType.Text
        da.SelectCommand.CommandText = "SELECT LineId FROM [QRIndentity]"
        da.Fill(ds, "tbl2")
        Dim dt As DataTable = ds.Tables("tbl2")
        txtbno = 0
        IQR = dt.Rows(0)("LineId")
        sno = 1
        PrintLine(1, TAB(0), "^XA")
        lin = lin + 1
        PrintLine(1, TAB(0), "^LH0,0^FS")
        lin = lin + 1
        PrintLine(1, TAB(0), "^LL304")
        lin = lin + 1
        PrintLine(1, TAB(0), "^MD0")
        lin = lin + 1
        PrintLine(1, TAB(0), "^MNY")
        lin = lin + 1
        PrintLine(1, TAB(0), "^LH0,0^FS")
        lin = lin + 1
        PrintLine(1, TAB(0), "^XA")
        lin = lin + 1


        For Each row As DataGridViewRow In loaddv.Rows
            If row.Cells.Item("SEL").Value = 1 Then

                'Dim delimiter As Char = "/"
                'Dim strArr As String() = row.Cells.Item("ITEMNAME").Value.Split(delimiter)
                Dim colour As String = ""
                'For ii As Integer = 0 To strArr.Length - 1
                '    colour = strArr(2).ToString()
                'Next
                colour = row.Cells.Item("ITEMNAME").Value
                For j = 1 To Val(row.Cells.Item("QTY").Value)
                    'For value As Integer = 0 To (Math.Floor((row.Cells.Item("QTY").Value) / 5)) - 1


                    EnryptString(TextBox1.Text.Trim)



                  
                    If chkcrypt.Checked = True Then
                        If Len(Trim(sEncript(row.Cells.Item("GRADE").Value.ToString & IQR))) > 32 Then
                            PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + Val(txtbno))) & " ,15^BQN,3,3^FD000" & sEncript(row.Cells.Item("GRADE").Value.ToString & IQR) & "^FS")
                        Else
                            PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + Val(txtbno))) & " ,15^BQN,4,4^FD000" & sEncript(row.Cells.Item("GRADE").Value.ToString & IQR) & "^FS")
                        End If

                    Else
                        If Len(Trim(row.Cells.Item("GRADE").Value.ToString & IQR)) > 32 Then
                            PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + Val(txtbno))) & " ,15^BQN,3,3^FD000" & row.Cells.Item("GRADE").Value.ToString & IQR & "^FS")
                        Else
                            PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + Val(txtbno))) & " ,15^BQN,4,4^FD000" & row.Cells.Item("GRADE").Value.ToString & IQR & "^FS")
                        End If

                    End If

                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(130 + (Val(txtbno)))) & ",35^A0R,25,25^CI13^FR^FD" & colour & "^FS ")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(15 + (Val(txtbno)))) & ",129^A0N,20,20^CI13^FR^FD" & Mid(row.Cells.Item("itemcode").Value, 1, InStr(row.Cells.Item("itemcode").Value, "_") - 1) & "^FS ")
                    lin = lin + 1
                    If chksc.Checked = True Then
                        PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + (Val(txtbno)))) & ",149^A0N,20,20^CI13^FR^FD" & Trim(Mid(row.Cells.Item("itemcode").Value, InStr(row.Cells.Item("itemcode").Value, "_") + 1, 6)) & "/" & row.Cells.Item("docnum").Value & "J" & getyrchr(Val(row.Cells.Item("yr").Value)) & "^FS ")
                    ElseIf chkih.Checked = True Then
                        PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + (Val(txtbno)))) & ",149^A0N,20,20^CI13^FR^FD" & Trim(Mid(row.Cells.Item("itemcode").Value, InStr(row.Cells.Item("itemcode").Value, "_") + 1, 6)) & "/" & row.Cells.Item("docnum").Value & "I" & getyrchr(Val(row.Cells.Item("yr").Value)) & "^FS ")
                    ElseIf chkgrn.Checked = True Then
                        PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + (Val(txtbno)))) & ",149^A0N,20,20^CI13^FR^FD" & Trim(Mid(row.Cells.Item("itemcode").Value, InStr(row.Cells.Item("itemcode").Value, "_") + 1, 6)) & "/" & row.Cells.Item("docnum").Value & "G" & getyrchr(Val(row.Cells.Item("yr").Value)) & "^FS ")
                    ElseIf chkfob.Checked = True Then
                        PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + (Val(txtbno)))) & ",149^A0N,20,20^CI13^FR^FD" & Trim(Mid(row.Cells.Item("itemcode").Value, InStr(row.Cells.Item("itemcode").Value, "_") + 1, 6)) & "/" & row.Cells.Item("docnum").Value & "F" & getyrchr(Val(row.Cells.Item("yr").Value)) & "^FS ")
                    Else
                        PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + (Val(txtbno)))) & ",149^A0N,20,20^CI13^FR^FD" & Trim(Mid(row.Cells.Item("itemcode").Value, InStr(row.Cells.Item("itemcode").Value, "_") + 1, 6)) & "^FS ")
                    End If

                    lin = lin + 1

                    '^FO20,149^A0N,20,20^CI13^FR^FD38F^FS
                    IQR = IQR + 1
                    txtbno = txtbno + 160
                    If sno = 5 Then
                        PrintLine(1, TAB(0), "^PQ1,0,0,N")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^XZ")

                        PrintLine(1, TAB(0), "^XA")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^LH0,0^FS")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^LL304")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^MD0")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^MNY")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^LH0,0^FS")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^XA")
                        lin = lin + 1
                        txtbno = 0
                        sno = 0
                    End If
                    sno = sno + 1

                    'PrintLine(1, TAB(0), "^PQ1,0,0,N")
                    'lin = lin + 1
                    'PrintLine(1, TAB(0), "^XZ")
                    ' IQR = IQR + 5

                Next j


            End If


        Next
        PrintLine(1, TAB(0), "^PQ1,0,0,N")
        lin = lin + 1
        PrintLine(1, TAB(0), "^XZ")


        'Dim com As New SqlCommand
        Dim com As New OleDb.OleDbCommand
        com.Connection = con
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        com.CommandText = "UPDATE [QRIndentity] SET LINEID = '" & IQR & "'"
        com.ExecuteNonQuery()
        com.Dispose()


        FileClose(1)


        If MsgBox("Ok", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            dir = System.AppDomain.CurrentDomain.BaseDirectory()
            mdir = Trim(dir) & "Qrbarcode.txt"
            Shell("cmd.exe /c" & "type " & mdir & " > lpt" & Trim(TextBox2.Text))
        End If

        'Shell("print /d:LPT" & Trim(TextBox2.Text) & mdir, vbNormalFocus)
        '        Shell("cmd.exe /c" & "type " & mdir & " > lpt1")
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Me.Close()
    End Sub

    Private Sub cmbbrand_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbbrand.TextChanged
        If Len(Trim(cmbbrand.Text)) > 0 Then
            Brand.Text = ""
        End If
    End Sub

    Private Sub Brand_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Brand.TextChanged
        If Len(Trim(Brand.Text)) > 0 Then
            cmbbrand.Text = ""
        End If
    End Sub

    Private Sub loadstall()
        Dim dir, mdir As String
        Dim mtbox, j, txtbno As Integer
        Dim sno As Integer

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "stalbarcode.txt"

        FileOpen(1, mdir, OpenMode.Output)
        lin = 0
        Dim IQR As Integer = 0

        Call main()
        
        txtbno = 0
        'IQR = dt.Rows(0)("LineId")
        sno = 1
        PrintLine(1, TAB(0), "^XA")
        lin = lin + 1
        PrintLine(1, TAB(0), "^PRC")
        lin = lin + 1
        PrintLine(1, TAB(0), "^LH0,0^FS")
        lin = lin + 1
        PrintLine(1, TAB(0), "^LL304")
        lin = lin + 1
        PrintLine(1, TAB(0), "^MD0")
        lin = lin + 1
        PrintLine(1, TAB(0), "^MNY")
        lin = lin + 1
        PrintLine(1, TAB(0), "^LH0,0^FS")
        lin = lin + 1
        PrintLine(1, TAB(0), "^XA")
        lin = lin + 1


        For Each row As DataGridViewRow In loaddv.Rows
            If row.Cells.Item("SEL").Value = 1 Then

                PrintLine(1, TAB(0), "^FO255,20^A0N,40,41^CI13^FR^FDMRP:Rs." & Microsoft.VisualBasic.Format(row.Cells.Item("Rate").Value, "######0.00") & "^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO310,54^A0N,24,26^CI13^FR^FD(Incl.of all Taxes)^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO145,78^A0N,40,35^CI13^FR^FD" & Trim(row.Cells.Item("Itemname").Value) & "^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO145,117^A0N,27,25^CI13^FR^FDCOL :" & Trim(row.Cells.Item("colcode").Value) & "^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO145,150^A0N,20,27^CI13^FR^FDCONTENT :" & Trim(row.Cells.Item("grp").Value) & "^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO390,170^A0N,23,28^CI13^FR^FDMFD:" & Microsoft.VisualBasic.Format(row.Cells.Item("Docdate").Value, "MMM") & " " & Microsoft.VisualBasic.Format(row.Cells.Item("Docdate").Value, "yyyy") & "^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO490,116^A0N,40,40^CI13^FR^FD" & Trim(row.Cells.Item("Style").Value) & "^FS")
                lin = lin + 1
                If (Trim(row.Cells.Item("style").Value) = "HALF") Or (Trim(row.Cells.Item("style").Value) = "FULL") Then
                    PrintLine(1, TAB(0), "^FO580,116^A0N,25,25^CI13^FR^FDSLEEVE^FS")
                    lin = lin + 1
                End If
                PrintLine(1, TAB(0), "^FO580,140^A0N,65,50^CI13^FR^FD" & Trim(row.Cells.Item("Size").Value) & "^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO630,140^A0N,25,20^CI13^FR^FDSIZE^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO630,160^A0N,25,25^CI13^FR^FDcm^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO580,230^A0N,25,29^CI13^FR^FDQTY:1N^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^BY2,3.0^FO135,190^B3N,N,61,N,Y^FR^FD" & Trim(row.Cells.Item("Itemcode").Value) & "^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO380,255^A0N,25,30^CI13^FR^FD" & row.Cells.Item("Docnum").Value & "^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO135,255^A0N,25,30^CI13^FR^FD" & Trim(row.Cells.Item("Itemcode").Value) & "^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO135,255^A0N,25,20^CI13^FR^FD_____________________________________________________^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO135,285^A0N,25,30^CI13^FR^FDAtithya Clothing Company^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO135,310^A0N,20,25^CI13^FR^FD(A Unit of ENES Textile Mills)^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO435,310^A0N,20,20^CI13^FR^FDNo.2/453, SVD Nagar,^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^FO135,330^A0N,20,20^CI13^FR^FDKovilpappagudi, Madurai-625018.TN. GSTIN:33AAIFA8010E1ZI^FS")
                lin = lin + 1
                PrintLine(1, TAB(0), "^PQ" & Microsoft.VisualBasic.Format(row.Cells.Item("Quantity").Value, "#####0") & ",0,0,N")
                lin = lin + 1
            End If
        Next
        PrintLine(1, TAB(0), "^XZ")
        FileClose(1)


        If MsgBox("Ok", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            dir = System.AppDomain.CurrentDomain.BaseDirectory()
            mdir = Trim(dir) & "stalbarcode.txt"
            Shell("cmd.exe /c" & "type " & mdir & " > lpt" & Trim(TextBox2.Text))
        End If

    End Sub

    Private Sub loadATC()
        'Dim dir, mdir As String
        Dim mtbox, j, txtbno As Integer
        Dim sno As Integer

        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "ATCQrbarcode.txt"


        If CHKDIRPRN.Checked = True Then
            FileOpen(1, "LPT" & Trim(TextBox2.Text), OpenMode.Output)
        Else
            FileOpen(1, mdir, OpenMode.Output)
        End If

        lin = 0
        Dim num As Integer = 0

        'Call MAIN()
        'Dim da As New SqlDataAdapter, 
        Dim ds As New DataSet
        Dim da As New OleDb.OleDbDataAdapter
        da.SelectCommand = New OleDb.OleDbCommand
        da.SelectCommand.Connection = con
        da.SelectCommand.CommandType = CommandType.Text
        da.SelectCommand.CommandText = "SELECT LineId FROM [QRIndentity]"
        da.Fill(ds, "tbl2")
        Dim dt As DataTable = ds.Tables("tbl2")
        txtbno = 0
        num = dt.Rows(0)("LineId")
        sno = 1
        PrintLine(1, TAB(0), "^XA")
        lin = lin + 1
        PrintLine(1, TAB(0), "^PRC")
        lin = lin + 1
        PrintLine(1, TAB(0), "^LH0,0^FS")
        lin = lin + 1
        PrintLine(1, TAB(0), "^LL304")
        lin = lin + 1
        PrintLine(1, TAB(0), "^MD0")
        lin = lin + 1
        PrintLine(1, TAB(0), "^MNY")
        lin = lin + 1
        PrintLine(1, TAB(0), "^LH0,0^FS")
        lin = lin + 1
        'PrintLine(1, TAB(0), "^XA")
        'lin = lin + 1


        For Each row As DataGridViewRow In loaddv.Rows
            If row.Cells.Item("SEL").Value = 1 Then

                'Dim delimiter As Char = "/"
                'Dim strArr As String() = row.Cells.Item("ITEMNAME").Value.Split(delimiter)
                Dim str3 As String = ""
                'For ii As Integer = 0 To strArr.Length - 1
                '    colour = strArr(2).ToString()
                'Next
                str3 = row.Cells.Item("ITEMNAME").Value & vbNullString
                For j = 1 To Val(row.Cells.Item("QTY").Value)

                    PrintLine(1, TAB(0), "^FO" & Trim(Str(20 + Val(txtbno))) & " ,25^BQN,3,3^FD000" & EnryptString(row.Cells.Item("QR").Value.ToString & num) & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO" & Trim(Str(15 + Val(txtbno))) & ",135^A0N,20,15^CI13^FR^FD", Mid((Trim(row.Cells.Item("u_colcode").Value) & "-" & Trim(row.Cells.Item("ITEMNAME").Value)), 1, 16), "^FS ")
                    lin = lin + 1


                   
                    '^FO20,149^A0N,20,20^CI13^FR^FD38F^FS
                    ' IQR = IQR + 1
                    txtbno = txtbno + 160
                    If sno = 5 Then
                        PrintLine(1, TAB(0), "^PQ1,0,0,N")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^XZ")

                        PrintLine(1, TAB(0), "^XA")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^PRC")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^LH0,0^FS")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^LL304")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^MD0")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^MNY")
                        lin = lin + 1
                        PrintLine(1, TAB(0), "^LH0,0^FS")
                        lin = lin + 1
                        'PrintLine(1, TAB(0), "^XA")
                        'lin = lin + 1
                        txtbno = 0
                        sno = 0
                    End If
                    sno = sno + 1

                    'PrintLine(1, TAB(0), "^PQ1,0,0,N")
                    'lin = lin + 1
                    'PrintLine(1, TAB(0), "^XZ")
                    ' IQR = IQR + 5

                Next j


            End If


        Next
        PrintLine(1, TAB(0), "^PQ1,0,0,N")
        lin = lin + 1
        PrintLine(1, TAB(0), "^XZ")


        'Dim com As New SqlCommand
        'Dim com As New OleDb.OleDbCommand
        'com.Connection = con
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If
        'com.CommandText = "UPDATE [QRIndentity] SET LINEID = '" & IQR & "'"
        'com.ExecuteNonQuery()
        'com.Dispose()


        FileClose(1)


        If MsgBox("Ok", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            'dir = System.AppDomain.CurrentDomain.BaseDirectory()
            mdir = Trim(dir) & "ATCQrbarcode.txt"
            'Shell("cmd.exe /c" & "type " & mdir & " > lpt" & Trim(TextBox2.Text))
            'Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(TextBox2.Text) & ":")
            Shell("cmd.exe /c" & " type " & mdir & " > lpt" & Trim(TextBox2.Text))

            'Dim proc As Process = New Process
            'proc.StartInfo.FileName = "cmd.exe "
            'proc.StartInfo.Arguments = "  /c type " & mdir & " > lpt" & Trim(TextBox2.Text)
            'proc.Start()

        End If

        'Shell("print /d:LPT" & Trim(TextBox2.Text) & mdir, vbNormalFocus)
        '        Shell("cmd.exe /c" & "type " & mdir & " > lpt1")
    End Sub




    Private Sub chkstall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkstall.CheckedChanged
        If chkstall.Checked = True Then
            Brand.Text = ""
            cmbbrand.Text = ""
            chksc.Checked = False
            chkih.Checked = False
            chkgrn.Checked = False
            chkfob.Checked = False
            chkATC.Checked = False
        End If
    End Sub

    Private Sub chkATC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkATC.CheckedChanged
        If chkATC.Checked = True Then
            Brand.Text = ""
            cmbbrand.Text = ""
            chksc.Checked = False
            chkih.Checked = False
            chkgrn.Checked = False
            chkfob.Checked = False
            chkstall.Checked = False
        End If
    End Sub
End Class
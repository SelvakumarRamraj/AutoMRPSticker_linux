Imports System.IO
Imports System.Math
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.VbStrConv
Imports AutoMRPSticker.connection
'Imports CButtonLib.CButton
Public Class frmbarsettings
    'Dim btn As New CButtonLib.CButton
    Dim msel As Integer
    Dim msql As String
    Dim msql2 As String
    Dim mkcode As Int32
    Dim merr As String
    Dim mktrue As Int16
    Dim mktrue2 As Int16
    Dim mktrue3 As Int16
    Dim mkvalue As String
    Dim i, j As Integer
    Dim MFIL, MFILDET As String
    Private Sub loadfield()

       
        If Trim(cmbprnon.Text) = "SALES" Then
            MFIL = "INV1"
            MFILDET = "OINV"
        ElseIf Trim(cmbprnon.Text) = "DATE ORDER" Then
            MFIL = "DLN1"
            MFILDET = "ODLN"
        ElseIf Trim(cmbprnon.Text) = "SAMPLE" Then

        Else
            MFIL = "INV1"
            MFILDET = "OINV"
        End If


        If chkprod.Checked = True Then

        ElseIf chktype.Checked = True Then
            msql = "select top 1 t0.docentry, t0.docnum,t1.linenum,t0.docdate,t0.cardcode,t0.cardname,t1.itemcode,t1.Dscription as quality,k.u_size,convert(decimal(16),t1.quantity) as quantity,floor(convert(decimal(16),t1.quantity)/cast(k.salpackun as int)) as boxqty,cr.state, " & vbCrLf _
                    & " t1.Dscription u_catalgcode, t5.ItmsGrpNam u_itemgrp, k.salunitmsr as uom, " & vbCrLf _
                    & "  case when bb.pcs>0 then bb.pcs else cast(k.salpackun as int) end as box, " & vbCrLf _
                    & "  case when  CHARINDEX('CMS',k.slength2)<=0 then cast(isnull(k.slength2,0) as real) else 0 end as u_length, " & vbCrLf _
                    & "  case when  CHARINDEX('CMS',k.swidth1)<=0 then k.swidth1 else 0 end as u_width, k.u_style, " & vbCrLf _
                    & "  t4.Price  as rate,cast((t4.price*k.AssblValue) as decimal(8,2)) as boxmrp, cast(k.AssblValue as decimal(8,2)) as mrp, " & vbCrLf _
                    & "  k.u_scode U_Remarks, k.u_subgrp1 u_brand, k.invntitem, '' u_prntname, " & vbCrLf _
                    & "  ltrim(rtrim(right(YEAR(t0.docdate),2)))+right('0'+rtrim(ltrim(MONTH(t0.docdate))),2)+rtrim(ltrim(convert(nvarchar(100),k.u_scode))) as autocode, " & vbCrLf _
                    & "  CASE when   CHARINDEX('CMS',k.slength2)<=0 and cast(k.slength2 as real)>0 and (rtrim(upper(t5.ItmsGrpNam))in ( select ItmsGrpNam from OITM b LEFT join OITB c on c.ItmsGrpCod = b.ItmsGrpCod where CHARINDEX('CMS',u_size)<=0 group by ItmsGrpNam) OR CHARINDEX('DHOTI',UPPER(t5.ItmsGrpNam))>0) then  RTRIM(k.swidth1)+'cmX'+rtrim(lTRIM(cast(cast(k.slength2 as real)*100 as char(10)))) +'cm' else  ltrim(convert(varchar,round(cast(replace(k.slength2,'CMS','') as real)/100,2)))+'mX' + rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end as msize,  " & vbCrLf _
                    & "  CASE when CHARINDEX('CMS',k.slength2)<=0  and cast(k.slength2 as real)>0 and rtrim(upper(t5.ItmsGrpNam)) in( select ItmsGrpNam from OITM b LEFT join OITB c on c.ItmsGrpCod = b.ItmsGrpCod where CHARINDEX('CMS',u_size)<=0 group by ItmsGrpNam) then  RTRIM(k.swidth1)+'cmX'+rtrim(lTRIM(cast(cast(k.slength2 as real)*100 as char(10)))) +'cm' else  k.slength2 end as msize2,'MFD ' + substring(datename(m,GETDATE()),1,3) +' '+ CAST(DATEPART(YYYY,GETDATE()) as CHAR(4)) as mfd, " & vbCrLf _
                    & "  (left(convert(varchar,t0.docnum) + '00000',5)+left(convert(varchar,t0.cardcode) +'0000000000000',13) + left(convert(varchar,t4.price) + '00000',5)) as barcode, " & vbCrLf _
                    & "  (ltrim(convert(varchar,t0.docnum)) + rtrim(convert(varchar,t0.cardcode))+ltrim(convert(varchar,t4.Price))) as barcode1,ltrim(convert(varchar,t0.docnum)) as barcode2,ltrim(convert(varchar,t0.docnum))+rtrim(t1.itemcode) as barcode3,ltrim(convert(varchar,t0.docentry))+rtrim(t1.itemcode) as drbarcode, bb.dhoti,bb.shirting,bb.suiting,bb.towel,bb.pcs as bompcs,k.u_subgrp6,k.u_subgrp5 color,  " & vbCrLf _
                    & "  cc.dhoti contdhoti,cc.shirting contshirting,cc.suiting contsuiting,cc.towel conttowel,cc.perfume contperfume,cc.belt contbelt,cc.pcs contpcs, cc.rate contrate ,isnull(t1.u_dbox,0) dbox,case when isnull(t1.u_dbox,0)>0 then floor(convert(decimal(16),t1.quantity)/cast(isnull(t1.u_dbox,0) as int)) else 0 end as dboxqty, ltrim(rtrim(right(YEAR(t0.docdate),2)))+right('0'+rtrim(ltrim(MONTH(t0.docdate))),2)+rtrim(ltrim(convert(nvarchar(100),ISNULL(k.u_scode,'')))) +rtrim(ltrim(CASE when ISNULL(t1.U_dbox,0)>0 then '|1' else '' end)) as dautocode, " & vbCrLf _
                    & "  '' cstype,'' sdocnum " & vbCrLf _
                    & "  from " & Trim(MFIL) & " as t1  " & vbCrLf _
                    & "  left join " & Trim(MFILDET) & "  as t0 on t0.docentry=t1.DocEntry " & vbCrLf _
                    & "  left join (select b.CardCode, CASE when len(rtrim(ltrim(ListNum)))>0 and ListNum IS not null then b.ListNum else c.state end as state " & vbCrLf _
                    & "  from OCRD b " & vbCrLf _
                    & "  left join CRD1 c on c.CardCode=b.cardcode and (c.Address='Office' or c.Address='Ship') and c.AdresType='B') cr on cr.CardCode=t0.CardCode " & vbCrLf _
                    & "  left join barcodebom as bb on BB.code=t1.itemcode " & vbCrLf _
                    & "  left join contentbom as cc on cc.code=t1.itemcode " & vbCrLf _
                    & "  Left join OITM k on k.ItemCode = t1.itemcode " & vbCrLf _
                    & "  Left join OITB t5 on t5.ItmsGrpCod = k.ItmsGrpCod " & vbCrLf _
                    & "  Left join (select * from ITM1) t4 on k.ItemCode = t4.ItemCode and cr.state = t4.PriceList "
        Else

            msql = "select top 1 t0.docentry, t0.docnum,t1.linenum,t0.docdate,t0.cardcode,t0.cardname,t1.itemcode,t1.u_catalogname as quality,t1.u_size,convert(decimal(16),t1.quantity) as quantity,floor(convert(decimal(16),t1.quantity)/cast(k.salpackun as int)) as boxqty,cr.state, " & vbCrLf _
                 & "k.u_catalgcode, k.u_itemgrp, k.salunitmsr as uom, case when bb.pcs>0 then bb.pcs else cast(k.salpackun as int) end as box, case when  CHARINDEX('CMS',k.u_length)<=0 then cast(isnull(k.u_length,0) as real) else 0 end as u_length, case when  CHARINDEX('CMS',k.u_width)<=0 then k.u_width else 0 end as u_width, k.u_style, k.U_SelPrice as rate,cast((k.salpackun*k.u_mrp) as decimal(8,2)) as boxmrp, cast(k.u_mrp as decimal(8,2)) as mrp, k.U_Remarks, k.u_brand, k.invntitem,k.u_prntname, " & vbCrLf _
                 & " ltrim(rtrim(right(YEAR(t0.docdate),2)))+right('0'+rtrim(ltrim(MONTH(t0.docdate))),2)+rtrim(ltrim(convert(nvarchar(100),k.u_remarks))) as autocode," & vbCrLf _
                 & "CASE when   CHARINDEX('CMS',k.u_length)<=0 and cast(k.u_length as real)>0 and (rtrim(upper(k.u_itemgrp))in ( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) OR CHARINDEX('DHOTI',UPPER(K.U_ItemGrp))>0) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  ltrim(convert(varchar,round(cast(replace(k.u_length,'CMS','') as real)/100,2)))+'mX' + rtrim(cast(convert(decimal(16,2),t1.quantity) as char(10)))+'M' end as msize," & vbCrLf _
                 & "CASE when CHARINDEX('CMS',k.u_length)<=0  and cast(k.u_length as real)>0 and rtrim(upper(k.u_itemgrp)) in( select u_itemgrp from [@ins_oplm] where CHARINDEX('CMS',u_size)<=0 group by u_itemgrp) then  RTRIM(k.u_width)+'cmX'+rtrim(lTRIM(cast(cast(k.u_length as real)*100 as char(10)))) +'cm' else  k.u_length end as msize2,'MFD ' + substring(datename(m,GETDATE()),1,3) +' '+ CAST(DATEPART(YYYY,GETDATE()) as CHAR(4)) as mfd, " & vbCrLf _
                 & " (left(convert(varchar,t0.docnum) + '00000',5)+left(convert(varchar,t0.cardcode) +'0000000000000',13) + left(convert(varchar,k.U_SelPrice) + '00000',5)) as barcode, " & vbCrLf _
                 & " (ltrim(convert(varchar,t0.docnum)) + rtrim(convert(varchar,t0.cardcode))+ltrim(convert(varchar,k.U_SelPrice))) as barcode1,ltrim(convert(varchar,t0.docnum)) as barcode2,ltrim(convert(varchar,t0.docnum))+rtrim(t1.itemcode) as barcode3,ltrim(convert(varchar,t0.docentry))+rtrim(t1.itemcode) as drbarcode, bb.dhoti,bb.shirting,bb.suiting,bb.towel,bb.pcs as bompcs,k.u_subgrp6,k.color,  " & vbCrLf _
                 & " cc.dhoti contdhoti,cc.shirting contshirting,cc.suiting contsuiting,cc.towel conttowel,cc.perfume contperfume,cc.belt contbelt,cc.pcs contpcs, cc.rate contrate,isnull(t1.u_dbox,0) dbox,case when isnull(t1.u_dbox,0)>0 then floor(convert(decimal(16),t1.quantity)/cast(isnull(t1.u_dbox,0) as int)) else 0 end as dboxqty, ltrim(rtrim(right(YEAR(t0.docdate),2)))+right('0'+rtrim(ltrim(MONTH(t0.docdate))),2)+rtrim(ltrim(convert(nvarchar(100),ISNULL(k.u_scode,'')))) +rtrim(ltrim(CASE when ISNULL(t1.U_dbox,0)>0 then '|1' else '' end)) as dautocode, " & vbCrLf _
                 & " cr.cstype,  cr.cstype +' ' +convert(nvarchar(20),t0.docnum) as sdocnum " & vbCrLf _
                 & "from " & Trim(MFIL) & " as t1 " & vbCrLf _
                 & "left join " & Trim(MFILDET) & " as t0 on t0.docentry=t1.DocEntry " & vbCrLf _
                 & "left join (select b.CardCode, CASE when len(rtrim(ltrim(u_salpricecode)))>0 and u_salpricecode IS not null then b.u_salpricecode else c.state end as state, " & vbCrLf _
                 & "            case when isnull(b.u_showcode,'')='DE'  then '*'   when isnull(b.u_showcode,'') ='DR' then '**'   when isnull(b.u_showcode,'') ='S' then '#' " & vbCrLf _
                 & "              when isnull(b.u_showcode,'') ='F' then '##'   else   ''   end cstype, from OCRD b " & vbCrLf _
                 & "            left join CRD1 c on c.CardCode=b.cardcode and (c.Address='Office' or c.Address='Ship') and c.AdresType='B') cr on cr.CardCode=t0.CardCode" & vbCrLf _
                 & "left join barcodebom as bb on BB.code=t1.itemcode " & vbCrLf _
                 & "left join contentbom as cc on cc.code=t1.itemcode " & vbCrLf _
                 & "Left Join " & vbCrLf _
                 & "(select it.invntitem, t0.u_itemcode,t0.u_itemname as mainname,t1.u_catalgcode,t0.u_itemgrp,it.salunitmsr,it.salpackun,t0.u_size,tz.u_length,tz.u_width,t0.u_style,t2.U_State,t2.U_SelPrice,t2.u_mrp,t1.U_Remarks,t1.u_brand,it.u_subgrp5 as color,it.u_subgrp6,t1.u_prntname from [@INS_PLM1] as t1 " & vbCrLf _
                 & "left join [@INS_OPLM] as t0 on t0.DocEntry=t1.DocEntry " & vbCrLf _
                 & "left join [@INS_PLM2] as t2 on t2.DocEntry=t1.docentry and t2.U_RowID=t1.lineid" & vbCrLf _
                 & "left join [@INCM_SZE1] as tz on tz.U_Name=t0.u_size" & vbCrLf _
                 & "left join OITM as it on it.ItemCode=t0.U_ItemCode) as k on k.u_catalgcode=t1.U_CatalogName and k.u_itemcode=t1.ItemCode and k.U_Size=t1.U_Size and k.U_State=cr.state"

        End If

        '& "left join CRD1 as cr on cr.CardCode=t0.CardCode and  cr.Address='Office' and cr.AdresType='B' " & vbCrLf _

        ' Dim CMD As New SqlCommand(msql, con)
        Using CMD As New SqlCommand(msql, con)
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If


            ' Dim DR As SqlDataReader
            Using DR As SqlDataReader = CMD.ExecuteReader
                If DR.HasRows = True Then
                    With flxcode
                        .set_ColAlignment(0, 2)
                        .set_ColAlignment(1, 2)
                        '.Rows = .Rows + 1
                        '.Row = .Rows - 1
                        While DR.Read

                            For i = 0 To DR.FieldCount - 1
                                'Console.WriteLine(drSQL.GetName(i) & " - " &
                                .Rows = .Rows + 1
                                .Row = .Rows - 1
                                .set_TextMatrix(.Row, 0, DR.GetName(i))
                                If IsDBNull(DR.Item((DR.GetName(i)))) = False Then
                                    .set_TextMatrix(.Row, 1, DR.Item((DR.GetName(i))))
                                Else
                                End If


                            Next
                        End While
                    End With
                End If
            End Using

        End Using

    End Sub
    Private Sub Frmbarsettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Me.Height = (MDIFORM1.Height - (MDIFORM1.sstp.Height + MDIFORM1.sstpb.Height))
        Me.Height = MDIParent1.Height

        Me.Width = My.Computer.Screen.Bounds.Width
        checkConnection()

        Call chktable()

        cmbcomp.Items.Add("RHL")
        cmbcomp.Items.Add("RR")
        cmbcomp.Items.Add("BB")

        cmbtype.Items.Add("Dhoti")
        cmbtype.Items.Add("Hosiery")
        cmbtype.Items.Add("Rdy Shirt")
        'cmbtype.Items.Add("Rdy Shirt")


        cmbprntype.Items.Add("BOX")
        cmbprntype.Items.Add("PCS")

        cmbprnon.Items.Add("SALES")
        cmbprnon.Items.Add("DATE ORDER")
        cmbprnon.Items.Add("INV DRAFT")
        cmbprnon.Items.Add("SAMPLE")

        'cmbprnon.Items.Add("PCS")
        'cmbprntype.Items.Add("Rdy Shirt")
        If mProdMktbarcode = "1" Then
            chktype.Checked = True
        Else
            chktype.Checked = False
        End If

        Call loadfield()

    End Sub
    Private Sub flxhead()
        flx.Rows = 2
        flx.Cols = 8
        flx.set_ColWidth(0, 700)
        flx.set_ColWidth(1, 700)
        flx.set_ColWidth(2, 2600)
        flx.set_ColWidth(3, 1000)
        flx.set_ColWidth(4, 1000)
        flx.set_ColWidth(5, 1800)
        flx.set_ColWidth(6, 2000)
        flx.set_ColWidth(7, 600)

        flx.set_ColAlignment(0, 2)
        flx.set_ColAlignment(1, 2)
        flx.set_ColAlignment(2, 2)
        flx.set_ColAlignment(3, 2)
        flx.set_ColAlignment(4, 2)
        flx.set_ColAlignment(5, 2)
        flx.set_ColAlignment(6, 2)
        flx.set_ColAlignment(7, 2)

        'flx.Row = 0
        'flx.Col = 0
        'flx.CellAlignment = 3
        'flx.CellFontBold = True
        'flx.set_TextMatrix(0, 0, "Code")

        'flx.Col = 1
        'flx.CellAlignment = 3
        'flx.CellFontBold = True
        'flx.set_TextMatrix(0, 1, "Item Description")

        'flx.Col = 2
        'flx.CellAlignment = 3
        'flx.CellFontBold = True
        'flx.set_TextMatrix(0, 2, "Size")

        'flx.Col = 3
        'flx.CellAlignment = 3
        'flx.CellFontBold = True
        'flx.set_TextMatrix(0, 3, "Pcs")

        'flx.Col = 4
        'flx.CellAlignment = 3
        'flx.CellFontBold = True
        'flx.set_TextMatrix(0, 4, "Qty")

        'flx.Col = 5
        'flx.CellAlignment = 3
        'flx.CellFontBold = True
        'flx.set_TextMatrix(0, 5, "Rate")

        'flx.Col = 6
        'flx.CellAlignment = 3
        'flx.CellFontBold = True
        'flx.set_TextMatrix(0, 6, "Amount")

        'flx.set_ColAlignment(0, 2)
        'flx.set_ColAlignment(1, 2)
        'flx.set_ColAlignment(2, 2)



    End Sub

   

    Private Sub SAVEREC()

        If msel = 1 Or msel = 2 Then
            'trans = con.BeginTransaction
            ' mnewid = findid()
            'Dim CMD As New SqlClient.SqlCommand("DELETE FROM inv WHERE BNO=" & Val(txtbno.Text), con)
            'Dim CMD2 As New SqlClient.SqlCommand("DELETE FROM binv WHERE BNO=" & Val(txtbno.Text), con)
            msql = "select * from barhead where docentry=0 "
            msql2 = "select * from bardet where docentry=0 "

            'msql = "delete from purchasehead where purcno=" & Val(txtbno.Text) & " and cmp_id='" & mcmpid & "' and cmpyr_id='" & mcmpyrid & "'"
            'msql2 = "delete from purchaseline where purcno=" & Val(txtbno.Text) & " and cmp_id='" & mcmpid & "' and cmpyr_id='" & mcmpyrid & "'"

            Dim CMD As New SqlCommand("delete from barhead where docentry=" & Val(txtno.Text), con)
            Dim CMD2 As New SqlCommand("delete from bardet where docentry=" & Val(txtno.Text), con)


            Dim DA As New SqlDataAdapter(msql, con)
            Dim DA1 As New SqlDataAdapter(msql2, con)
            Dim CB As New SqlCommandBuilder(DA)
            Dim CB1 As New SqlCommandBuilder(DA1)
            Dim DS As New DataSet
            Dim DS1 As New DataSet

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim TRANS As SqlTransaction = con.BeginTransaction

            Try
                If msel = 2 Then
                    CMD.Transaction = TRANS
                    CMD2.Transaction = TRANS
                    CMD.ExecuteNonQuery()
                    CMD2.ExecuteNonQuery()
                    'CMD.Dispose()
                    'CMD2.Dispose()
                End If
                'End If
                'Dim DA As New SqlClient.SqlDataAdapter("SELECT * FROM inv", con)
                'Dim DA1 As New SqlClient.SqlDataAdapter("SELECT * FROM Binv", con)
                'Dim CB As New SqlClient.SqlCommandBuilder(DA)
                'Dim CB1 As New SqlClient.SqlCommandBuilder(DA1)




                'Try
                DA.SelectCommand.Transaction = TRANS
                DA1.SelectCommand.Transaction = TRANS

                DA.Fill(DS, "barhead")
                Dim DSROW As DataRow

                'Dim dsrow As DataRow
                DSROW = DS.Tables("barhead").NewRow


                DSROW.Item("docentry") = Val(txtno.Text)
                DSROW.Item("date") = Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd")
                DSROW.Item("comp") = cmbcomp.Text
                DSROW.Item("group1") = cmbtype.Text
                DSROW.Item("group2") = cmbprntype.Text
                DSROW.Item("printon") = cmbprnon.Text
                DSROW.Item("stickercol") = Val(txtstickcol.Text)
                DSROW.Item("labrow") = Val(txtrow.Text)
                DSROW.Item("labcol") = Val(txtcol.Text)
                DSROW.Item("printer") = txtprint.Text & vbNullString
                If chkactive.Checked = True Then
                    DSROW.Item("active") = 1
                Else
                    DSROW.Item("active") = 0
                End If

                'If IsDBNull(DSROW.Item("doctype_id")) = False Then

                'DSROW.Item("totallineamt") = Microsoft.VisualBasic.Format(Val(txtlinenetamt.Text), "###########0.00")
                ''dsrow.Item("taxamt") = Microsoft.VisualBasic.Format(txtlinenetamt.Text, "###########0.00")

                'DSROW.Item("DISCPER") = Microsoft.VisualBasic.Format(Val(txtdiscper.Text), "#0.00")
                'DSROW.Item("discamt") = Microsoft.VisualBasic.Format(Val(txtdiscamt.Text), "###########0.00")
                'DSROW.Item("roundoff") = Microsoft.VisualBasic.Format(Val(txtround.Text), "###########0.00")

                'DSROW.Item("netamt") = Microsoft.VisualBasic.Format(Val(txtnetamt.Text), "###########0.00")
                'DSROW.Item("cmp_id") = mcmpid
                'DSROW.Item("cmpyr_id") = mcmpyrid

                DS.Tables("barhead").Rows.Add(DSROW)




                'DA.Update(DS, "inv")
                'DS.Dispose()
                'DA.Dispose()
                DA1.Fill(DS1, "bardet")
                Dim DSRW As DataRow

                'Dim DSRW As DataRow
                For i = 1 To flx.Rows - 1
                    DSRW = DS1.Tables("bardet").NewRow

                    DSRW.Item("docentry") = Val(txtno.Text)
                    DSRW.Item("date") = Microsoft.VisualBasic.Format(CDate(mskdate.Text), "yyyy-MM-dd")

                    DSRW.Item("linenum") = Val(flx.get_TextMatrix(i, 0))
                    DSRW.Item("headtype") = Trim(flx.get_TextMatrix(i, 1))
                    DSRW.Item("firstdet") = Trim(flx.get_TextMatrix(i, 2))
                    DSRW.Item("lrow") = Val(flx.get_TextMatrix(i, 3))
                    DSRW.Item("lcol") = Val(flx.get_TextMatrix(i, 4))


                    DSRW.Item("fontdet") = Trim(flx.get_TextMatrix(i, 5))
                    DSRW.Item("ldata") = Trim(flx.get_TextMatrix(i, 6))
                    DSRW.Item("secdet") = Trim(flx.get_TextMatrix(i, 7))




                    DS1.Tables("bardet").Rows.Add(DSRW)
                Next i
                DA.Update(DS, "barhead")
                DA1.Update(DS1, "bardet")
                'DA.Update(DS, "crdsalehead")




                'DA1.Update(DS1, "Binv")
                'DA.Update(DS, "inv")
                TRANS.Commit()
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                'Call SAVTAX()

                MsgBox("SUCCESSFULLY SAVED!")
                'If Microsoft.VisualBasic.MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                '    'Call TAKPRINT()
                'End If
                CLEAR(Me)
                'ClearAllCtrls(Panel1, True)
                'LBLNETVAT.Text = ""
                'TXTAMT.Text = 0
                'TXTDISCPER.Text = 0
                'TXTDISCAMT.Text = 0
                'TXTTAX.Text = 0
                'TXTTAXAMT.Text = 0
                'TXTROUND.Text = 0
                'TXTTOTAL.Text = 0
                flxhead()
                'ls1.Visible = False
                'ls2.Visible = False
                disable(Me)
                Btnsave.Enabled = False
            Catch EX As FieldAccessException
                If Not TRANS Is Nothing Then
                    TRANS.Rollback()
                End If
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                MsgBox(EX.Message)
                CLEAR(Me)
                disable(Me)
                Btnsave.Enabled = False
            Catch EX As ExecutionEngineException
                If Not TRANS Is Nothing Then
                    TRANS.Rollback()
                End If
                'trans.Rollback()
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                MsgBox(EX.Message)
                CLEAR(Me)
                disable(Me)
                Btnsave.Enabled = False
            Catch EX As ApplicationException
                If Not TRANS Is Nothing Then
                    TRANS.Rollback()
                End If
                'trans.Rollback()
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                MsgBox(EX.Message)
                CLEAR(Me)
                disable(Me)
                Btnsave.Enabled = False
            Catch EX As ArgumentException
                If Not TRANS Is Nothing Then
                    TRANS.Rollback()
                End If
                'trans.Rollback()
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                MsgBox(EX.Message)
                CLEAR(Me)
                disable(Me)
                Btnsave.Enabled = False
            Catch EX As SqlException
                If Not TRANS Is Nothing Then
                    TRANS.Rollback()
                End If
                'If MSEL = 2 Then
                ' trans.Rollback()
                ' End If
                'MsgBox(EX.Message)
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                merr = Trim(EX.Message)
                MsgBox(merr)
                If InStr(merr, "PRIMARY KEY") > 0 Then
                    ' = "Violation of PRIMARY KEY constraint 'PK_VOUCHER'. Cannot insert duplicate key in object 'VOUCHER'.The statement has been terminated." Then
                    'Dim CMD2 As New SqlClient.SqlCommand("SELECT MAX(VNO) AS TNO FROM VOUCHER", con)
                    'Dim CMD2 As New SqlClient.SqlCommand("SELECT MAX(BNO) AS TNO FROM INV", con)

                    'Dim CMD3 As New OleDb.OleDbCommand("SELECT MAX(BNO) AS TNO FROM INV", con)

                    'If con.State = ConnectionState.Closed Then
                    '    con.Open()
                    'End If

                    'Dim CBNO As Int32 = IIf(IsDBNull(CMD2.ExecuteScalar) = False, CMD3.ExecuteScalar, 0)

                    'txtbno.Text = CBNO + 1
                    'CMD3.Dispose()
                    'Call SAVEREC()

                    Dim CMD3 As New SqlCommand("SELECT MAX(docentry) AS TNO FROM barhead", con)


                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If

                    Dim CBNO As Int32 = IIf(IsDBNull(CMD3.ExecuteScalar) = False, CMD3.ExecuteScalar, 0)

                    txtno.Text = CBNO + 1
                    CMD3.Dispose()
                    'con.Close()
                    Call SAVEREC()


                End If


            Catch EX As ConstraintException
                'trans.Rollback()
                If Not TRANS Is Nothing Then
                    TRANS.Rollback()
                End If
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                MsgBox(EX.Message)
                CLEAR(Me)
                disable(Me)
                Btnsave.Enabled = False
            Catch EX As Exception
                If Not TRANS Is Nothing Then
                    TRANS.Rollback()
                End If
                MsgBox(EX.Message)
                DS.Dispose()
                DA.Dispose()
                DS1.Dispose()
                DA1.Dispose()
                CLEAR(Me)
                disable(Me)
                Btnsave.Enabled = False
            Finally

                CMD.Dispose()
                CMD2.Dispose()
                TRANS.Dispose()
            End Try

        End If
        CLEAR(Me)
        Call flxhead()
        'ls1.Visible = False
        'ls2.Visible = False
    End Sub
    Private Sub LOADDATA()
        'Dim CMD As New SqlClient.SqlCommand("SELECT * FROM inv WHERE BNO=" & Val(txtbno.Text), con)
        'Dim CMD1 As New SqlClient.SqlCommand("SELECT * FROM Binv WHERE BNO=" & Val(txtbno.Text) & " order by bno,sno", con)

        Dim CMD As New SqlCommand("SELECT * FROM barhead WHERE docentry=" & Val(txtno.Text), con)
        Dim CMD1 As New SqlCommand("SELECT * FROM bardet WHERE docentry=" & Val(txtno.Text) & " order by docentry,linenum", con)


        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim DR As SqlClient.SqlDataReader
        'Dim DR1 As SqlClient.SqlDataReader

        Dim DR As SqlDataReader
        Dim DR1 As SqlDataReader

        DR = CMD.ExecuteReader


        If DR.HasRows = True Then
            While DR.Read

                txtno.Text = DR.Item("docentry")
                'mskdate.Text = Microsoft.VisualBasic.Format(DR.Item("pdate"), "dd-MM-yyyy")
                mskdate.Text = DR.Item("date")
                'mskdate.Text = Microsoft.VisualBasic.Format(CDate(DR.Item("pdate")), "yyyy-mm-dd")
                'cmbparty.Text = getid("bpartner", "bpname", "bp_id", DR.Item("bp_id"))

                cmbcomp.Text = DR.Item("comp") & vbNullString
                cmbtype.Text = DR.Item("group1") & vbNullString
                cmbprntype.Text = DR.Item("group2") & vbNullString
                cmbprnon.Text = DR.Item("printon") & vbNullString
                txtrow.Text = DR.Item("labrow")
                txtstickcol.Text = DR.Item("stickercol") & vbNullString
                txtcol.Text = DR.Item("labcol") & vbNullString
                If IsDBNull(DR.Item("printer")) = False Then
                    txtprint.Text = DR.Item("printer") & vbNullString
                End If

                If IsDBNull(DR.Item("active")) = False Then
                    If DR.Item("active") > 0 Then
                        chkactive.Checked = True
                    Else
                        chkactive.Checked = False
                    End If
                Else
                    chkactive.Checked = False
                End If

            End While

        End If
        DR.Close()

        DR1 = CMD1.ExecuteReader
        If DR1.HasRows = True Then
            Call flxhead()
            With flx
                While DR1.Read
                    .Rows = .Rows + 1
                    .Row = .Rows - 1

                    'txtno.Text = DR1.Item("saleno")
                    'mskdate.Text = Microsoft.VisualBasic.Format(DR1.Item("pdate"), "dd-mm-yyyy")

                    .set_TextMatrix(.Row, 0, DR1.Item("linenum"))
                    .set_TextMatrix(.Row, 1, DR1.Item("headtype"))
                    .set_TextMatrix(.Row, 2, DR1.Item("firstdet") & vbNullString)
                    .set_TextMatrix(.Row, 3, DR1.Item("lrow"))
                    .set_TextMatrix(.Row, 4, DR1.Item("lcol"))
                    .set_TextMatrix(.Row, 5, DR1.Item("fontdet"))
                    .set_TextMatrix(.Row, 6, Trim(DR1.Item("ldata")))
                    .set_TextMatrix(.Row, 7, DR1.Item("secdet"))

                End While
            End With
        End If
        DR1.Close()
        CMD.Dispose()
        CMD1.Dispose()



    End Sub


    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        mskdate.Text = DateTimePicker1.Value
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub cmpcmdsave_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs)
        Call SAVEREC()
    End Sub
    Private Sub AUTONO()
        'Dim CMD As New SqlClient.SqlCommand("SELECT MAX(BNO) AS TNO FROM inv", con)

        Dim CMD4 As New SqlCommand("SELECT MAX(docentry) AS TNO FROM barhead", con)


        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim CBNO As Int32 = IIf(IsDBNull(CMD4.ExecuteScalar) = False, CMD4.ExecuteScalar, 0)

        txtno.Text = CBNO + 1
        CMD4.Dispose()
        'con2.Close()
    End Sub

    Private Sub chktable()
        'checkConnection()
        msql = "SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[barhead]') AND type in (N'U')"

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim CMD1 As New SqlCommand(msql, con)
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If
        'Dim DR As SqlClient.SqlDataReader
        'Dim DR1 As SqlClient.SqlDataReader

        Dim DR As SqlDataReader
        DR = CMD1.ExecuteReader

        If DR.HasRows = False Then

            mktrue = 1
        Else
            mktrue = 0
        End If
        DR.Close()
        CMD1.Dispose()


        msql2 = "SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bardet]') AND type in (N'U')"


        Dim CMD As New SqlCommand(msql2, con)

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If


        Dim DR1 As SqlDataReader
        DR1 = CMD.ExecuteReader

        If DR1.HasRows = False Then

            mktrue2 = 1
        Else
            mktrue2 = 0
        End If
        DR1.Close()
        CMD.Dispose()




        If mktrue = 1 Then
            msql2 = "CREATE TABLE [dbo].[barhead]( " & vbCrLf _
                & "[docentry] [int] primary key NOT NULL," & vbCrLf _
                & "[date] [datetime] NULL," & vbCrLf _
                & "[group1] [nchar](25) NULL," & vbCrLf _
                & "[group2] [nchar](25) NULL," & vbCrLf _
                & "[printon] [nchar](25) NULL," & vbCrLf _
                & "[labrow] [real] NULL," & vbCrLf _
                & "[labcol] [real] NULL," & vbCrLf _
                & "[labgap] [real] NULL," & vbCrLf _
                & "[stickercol] [real] NULL," & vbCrLf _
                & "[active] [int] NULL," & vbCrLf _
                & "[comp] [nchar](30) NULL )"


            Dim cmddel2 As New SqlCommand(msql2, con)

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            cmddel2.ExecuteNonQuery()
            cmddel2.Dispose()
        End If


        If mktrue2 = 1 Then
            msql2 = "CREATE TABLE [dbo].[bardet](" & vbCrLf _
               & "[docentry] [int] NULL," & vbCrLf _
              & "[date] [datetime] NULL," & vbCrLf _
              & "[linenum] [real] NULL," & vbCrLf _
              & "[headtype] [nchar](2) NULL," & vbCrLf _
              & "[firstdet] [nchar](150) NULL," & vbCrLf _
              & "[lrow] [real] NULL," & vbCrLf _
              & "[lcol] [real] NULL," & vbCrLf _
              & "[fontdet] [nchar](60) NULL," & vbCrLf _
              & "[ldata] [nchar](60) NULL," & vbCrLf _
              & "[secdet] [nchar](60) NULL" & vbCrLf _
              & ")"

            Dim cmddel As New SqlCommand(msql2, con)

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            cmddel.ExecuteNonQuery()
            cmddel.Dispose()
        End If

        msql2 = "SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[barcodebom]') AND type in (N'U')"

        Dim CMD3 As New SqlCommand(msql2, con)

        If con.State = ConnectionState.Closed Then
            con.Open()
        End If

        Dim DR4 As SqlDataReader
        DR4 = CMD3.ExecuteReader

        If DR4.HasRows = False Then

            mktrue3 = 1
        Else
            mktrue3 = 0
        End If
        DR4.Close()
        CMD3.Dispose()



        If mktrue3 = 1 Then
            msql2 = " CREATE TABLE [dbo].[barcodebom]( " & vbCrLf _
            & "[code] [nchar](20) NULL, " & vbCrLf _
            & " [dhoti] [nchar](30) NULL, " & vbCrLf _
            & "[shirting] [nchar](30) NULL," & vbCrLf _
            & "[suiting] [nchar](30) NULL," & vbCrLf _
            & "[towel] [nchar](30) NULL" & vbCrLf _
               & ")"
            Dim cmddel4 As New SqlCommand(msql2, con)

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If

            cmddel4.ExecuteNonQuery()
            cmddel4.Dispose()

        End If


        'DROP TABLE [dbo].[barhead]
        '      GO()
    End Sub



    Private Sub flx_KeyPressEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyPressEvent) Handles flx.KeyPressEvent
        If e.keyAscii = 32 Then
            flx.Row = flx.Row
            If flx.Row > 0 Then
                If Len(Trim(flx.get_TextMatrix(flx.Row, 0))) = 0 Then
                    flx.Col = 0
                    flx.CellFontName = "WinGdings"
                    flx.CellFontSize = 14
                    flx.CellAlignment = 4
                    flx.CellFontBold = True
                    flx.CellForeColor = Color.Red
                    flx.Text = Chr(252)
                Else
                    flx.Col = 0
                    flx.Text = ""
                End If
            End If
        End If
        If flx.Col >= 2 And flx.Col <= 7 Then
            editflx(flx, e.keyAscii, Btnclear)
        End If
    End Sub

    Private Sub flx_KeyUpEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyUpEvent) Handles flx.KeyUpEvent
        'If e.keyCode = Keys.ControlKey And e.keyCode = Keys.V Then
        '    'MsgBox("CTRL + B Pressed !")
        '    flx.Visible = False

        '    Call exeltoflx(flx)
        '    flx.Visible = True
        'End If
        If e.keyCode = Keys.F6 Then

            mkvalue = flx.get_TextMatrix(flx.Row, flx.Col)
        End If

        If e.keyCode = Keys.F7 Then
            flx.set_TextMatrix(flx.Row, flx.Col, mkvalue)
        End If

        Dim ShiftDown, AltDown, ctrldown, Txt
        ShiftDown = (Keys.Shift) > 0
        AltDown = (Keys.Alt) > 0
        ctrldown = (Keys.ControlKey) > 0


        If e.keyCode = 86 Then
            If ctrldown Then

                flx.Visible = False

                Call exeltoflx(flx)
                flx.Visible = True
                'Call CRATTABgeneral
                'Call sortload
            End If
        End If

    End Sub

    Private Sub cmdcmdexit_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs)
        Close()
    End Sub

   

    

    Private Sub txtno_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtno.LostFocus
        If msel > 1 Then
            Call LOADDATA()
        End If
    End Sub

    Private Sub txtno_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtno.TextChanged

    End Sub

    Private Sub flxf_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxcode.Enter
        'Clipboard.SetDataObject(Me.flxf.get_TextMatrix(flxf.Row, flxf.Col))

    End Sub

    Private Sub flxf_MouseDownEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_MouseDownEvent) Handles flxcode.MouseDownEvent
        'Clipboard.SetDataObject(Me.flxf.get_TextMatrix(flxf.Row, flxf.Col))
        'flxf.DoDragDrop(flxf.get_TextMatrix(flxf.Row, flxf.Col), DragDropEffects.Copy Or DragDropEffects.Move)

        'label1.Drag(vbBeginDrag)

    End Sub

    Private Sub flx_MouseDownEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_MouseDownEvent) Handles flx.MouseDownEvent

        ''Dim ix As Integer = flx.get_TextMatrix(flx.Row, flx.Col, ix)
        '' If ix <> -1 Then
        'flx.set_TextMatrix(flx.Row, flx.Col, flx.DoDragDrop(flx.get_TextMatrix(flx.Row, flx.Col), DragDropEffects.Move))
        ''ListBox2.DoDragDrop(ix.ToString, DragDropEffects.Move)
        'End If

    End Sub

    Private Sub flx_OLEDragOver(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_OLEDragOverEvent) Handles flx.OLEDragOver

        'If e.data.GetDataPresent(DataFormats.Text) Then
        '    e.effect = DragDropEffects.Move
        '    ListBox2.SelectedIndex = ListBox2.IndexFromPoint(ListBox2.PointToClient(New Point(e.x, e.y)))
        'End If

    End Sub

    Private Sub flx_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flx.Enter

    End Sub

    Private Sub cmdexcel_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs)
        Call exportexcelData("bardet", " docentry=" & Trim(txtno.Text), "linenum")
    End Sub

    Private Sub cmpcmddel_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmpcmdclear_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub BtnAdd_Click(sender As System.Object, e As System.EventArgs) Handles BtnAdd.Click
        msel = BtnAdd.Tag
        'enable(Me)
        CLEAR(Me)
        enable(Me)
        Call AUTONO()
        Call flxhead()
        txtno.Focus()

    End Sub

    Private Sub BtnEdit_Click(sender As System.Object, e As System.EventArgs) Handles BtnEdit.Click
        msel = BtnEdit.Tag
        'enable(Me)
        CLEAR(Me)
        enable(Me)
        Call flxhead()
        txtno.Focus()

    End Sub

    Private Sub Btndisp_Click(sender As System.Object, e As System.EventArgs) Handles Btndisp.Click
        msel = Btndisp.Tag
        'enable(Me)
        CLEAR(Me)
        enable(Me)
        Call flxhead()
        txtno.Focus()
    End Sub

    Private Sub Btnsave_Click(sender As System.Object, e As System.EventArgs) Handles Btnsave.Click
        Call SAVEREC()
    End Sub

    Private Sub Btnexit_Click(sender As System.Object, e As System.EventArgs) Handles Btnexit.Click
        Me.Close()
    End Sub

    Private Sub Btnxlexp_Click(sender As System.Object, e As System.EventArgs) Handles Btnxlexp.Click
        Call exportexcelData("bardet", " docentry=" & Trim(txtno.Text), "linenum")
    End Sub

    Private Sub Btndel_Click(sender As System.Object, e As System.EventArgs) Handles Btndel.Click

    End Sub

    Private Sub Btnclear_Click(sender As System.Object, e As System.EventArgs) Handles Btnclear.Click
        CLEAR(Me)
        Call flxhead()
    End Sub
End Class
Imports System
Imports System.IO
Imports AxMSFlexGridLib
Imports System.Math
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.VbStrConv

Public Class frmteamdist
    Dim msql, msql2 As String
    Dim i, k, j As Int32
    Private Sub frmteamdist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width

        Call flxhhead()
        Call flxvhead()
        Call flxwhead()
        mskddate.Text = Microsoft.VisualBasic.Format(Now, "dd-MM-yyyy")

        msql = "select k.docnum,k.docdate,k.cardcode,k.cardname,CASE when charindex('SHIRT', u_brand)<=0 then k.balordqty else 0 end as qty,CASE when charindex('SHIRT',u_brand)>0 then k.balordqty else 0 end as mtrs,k.U_brand from" & vbCrLf _
        & "(select T0.Docnum,T0.DocDate,T0.Cardcode,T0.Cardname ,isnull(sum(T1.linetotal),0) [OrderTotal], " & vbCrLf _
        & " isnull(td.deltotal,0) as [deltotal],isnull(td2.deltotal,0) as delctotal ,isnull(T2.invtotal,0) as [InvoiceTotal],isnull(t3.invtotal,0) as invctotal, isnull(T2.RtnTotal,0) as [Rtntotal]," & vbCrLf _
        & "(isnull(sum(T1.linetotal),0)-(isnull(td.deltotal,0)+ isnull(td2.deltotal,0)+isnull(T2.invtotal,0))) as balordamt," & vbCrLf _
        & " isnull(sum(t1.quantity),0) as ordqty,isnull(td.delqty,0) as [dqty],isnull(td2.delqty,0) as [dcqty],isnull(t2.invqty,0) as [invqty],isnull(t3.invqty,0) as [invcqty],isnull(t2.rtnqty,0) as [rtnqty]," & vbCrLf _
        & "(isnull(sum(t1.quantity),0)-(isnull(td.delqty,0)+isnull(td2.delqty,0)+isnull(t2.invqty,0)+isnull(t3.invqty,0))) as balordqty,Tp.agent,T0.U_Brand,tp.City,tp.district,tp.state,sd.u_name as subbrand " & vbCrLf _
        & " from Ordr T0 " & vbCrLf _
        & "INNER JOIN rdr1 T1 on T0.DocEntry=T1.DocEntry and t1.treetype<>'I' " & vbCrLf _
        & " left join ( select tr1.cardcode,tr1.cardname,tr1.u_areacode as agent,tr2.city,tr2.county as district,tr2.state from ocrd  tr1 " & vbCrLf _
        & " inner join CRD1 tr2 on tr2.CardCode=tr1.CardCode and tr2.AdresType='B') tp on tp.CardCode=T0.CardCode " & vbCrLf _
        & " Left Join " & vbCrLf _
        & "(select k.baseentry,k.basetype, k.ObjType,SUM(k.invtotal) as deltotal,SUM(k.invqty) as delqty from  " & vbCrLf _
        & "(Select  t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.lineTotal),0) as invTotal,sum(t1.quantity) as invqty  from Odln T0 " & vbCrLf _
        & "inner join dln1 as t1 on t1.docentry=T0.docentry and t1.TreeType<>'I' " & vbCrLf _
        & " group by t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType ) k " & vbCrLf _
        & " group by k.baseentry,k.basetype, k.ObjType)  td on td.basetype=T1.objtype and Td.baseentry=T1.docEntry " & vbCrLf _
        & " Left Join " & vbCrLf _
           & " (select k.BaseEntry as baseentry,k.basetype,k.ObjType,SUM(k.deltotal) as deltotal,SUM(k.delqty) as delqty ,SUM(k.rtntotal)  as rtntotal,SUM(k.rtnqty) as rtnqty " & vbCrLf _
           & "from (Select  t1.U_BDocENtry as baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as delTotal,isnull(sum(t1.quantity),0) as delqty ,0 as rtnTotal,0 as rtnqty " & vbCrLf _
           & " from Odln T0 " & vbCrLf _
           & " inner join dln1 as t1 on t1.docentry=T0.docentry and t1.BaseType=17 and t1.TreeType<>'I' " & vbCrLf _
            & " where(t1.U_BDocENtry Is Not null) " & vbCrLf _
            & " group by t1.U_BDocENtry,t1.BaseType,T0.DocEntry, T0.ObjType ,isnull(T0.DocTotal,0)  ) k " & vbCrLf _
            & " group by k.baseentry,k.basetype, k.ObjType) Td2 on td2.basetype=T1.objtype and Td2.baseentry=T1.docEntry  " & vbCrLf _
            & "   Left Join " & vbCrLf _
            & " (select k.baseentry,k.basetype,k.ObjType,SUM(k.invtotal) as invtotal,SUM(k.invqty) as invqty ,SUM(k.rtntotal) as rtntotal,SUM(k.rtnqty) as rtnqty  from " & vbCrLf _
            & " (Select  t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as invTotal,sum(t1.quantity) as  invqty ,isnull(Tr.rtntotal,0) as rtnTotal,tr.rtnqty as rtnqty " & vbCrLf _
            & " from Oinv T0 " & vbCrLf _
            & "inner join inv1 as t1 on t1.docentry=T0.docentry and t1.basetype=17  and t1.TreeType<>'I' " & vbCrLf _
            & " left join (Select  t1.BaseRef, t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as  rtnTotal,sum(t1.quantity) as rtnqty from Orin T0 " & vbCrLf _
            & " inner join rin1 as t1 on t1.docentry=T0.docentry and t1.TreeType<>'I' " & vbCrLf _
            & " group by t1.baseref,t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType ) tr on tr.Baseref=T1.DocEntry " & vbCrLf _
            & " where(t1.U_BDocENtry Is null) " & vbCrLf _
            & " group by t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(Tr.rtntotal,0),tr.rtnqty ) k " & vbCrLf _
            & " group by k.baseentry,k.basetype, k.ObjType) T2 on t2.basetype=T1.objtype and T2.baseentry=T1.docEntry  " & vbCrLf _
            & " Left Join " & vbCrLf _
            & " (select k.BaseEntry as baseentry,k.basetype,k.ObjType,SUM(k.invtotal) as invtotal,SUM(k.invqty) as invqty ,SUM(k.rtntotal) as rtntotal,SUM(k.rtnqty) as rtnqty " & vbCrLf _
            & " from (Select  t1.U_BDocENtry as baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as invTotal,isnull(sum(t1.quantity),0) as invqty ,0 as rtnTotal,0 as rtnqty " & vbCrLf _
            & " from Oinv T0 " & vbCrLf _
            & " inner join inv1 as t1 on t1.docentry=T0.docentry and t1.BaseType=17 and t1.TreeType<>'I' " & vbCrLf _
            & " where(t1.U_BDocENtry Is Not null) " & vbCrLf _
            & " group by t1.U_BDocENtry,t1.BaseType,T0.DocEntry, T0.ObjType ,isnull(T0.DocTotal,0)  ) k " & vbCrLf _
            & " group by k.baseentry,k.basetype, k.ObjType) T3 on t3.basetype=T1.objtype and T3.baseentry=T1.docEntry " & vbCrLf _
            & " left join [@incm_sbd1] sd on sd.LineId=T0.U_SubBrand " & vbCrLf _
            & " where T0.DocDate>='2014-06-01'   and  T0.DocDate<='2014-06-19' and T0.Canceled='N'  " & vbCrLf _
            & " group by T0.Docnum,T0.DocDate,T0.Cardcode,T0.Cardname,isnull(T0.doctotal,0),isnull(td.deltotal,0),isnull(td2.deltotal,0),  " & vbCrLf _
            & " isnull(T2.invtotal,0),isnull(t3.invtotal,0),isnull(T2.RtnTotal,0),isnull(td.delqty,0),isnull(td2.delqty,0),isnull(t2.invqty,0),isnull(t3.invqty,0),isnull(t2.rtnqty,0)," & vbCrLf _
            & " Tp.agent, T0.U_Brand, tp.City, tp.district, tp.state, sd.u_name " & vbCrLf _
           & " having (isnull(sum(t1.quantity),0)-(isnull(td.delqty,0)+isnull(td2.delqty,0)+isnull(t2.invqty,0)+isnull(t3.invqty,0)))<>0) k " & vbCrLf _
           & "  where(k.balordqty > 0) order by k.docnum"

    End Sub

    Private Sub flxhhead()
        flxh.Rows = 1
        flxh.Cols = 7
        flxh.set_ColWidth(0, 1000)
        flxh.set_ColWidth(1, 1000)
        flxh.set_ColWidth(2, 1200)
        flxh.set_ColWidth(3, 3000)
        flxh.set_ColWidth(4, 1200)
        flxh.set_ColWidth(5, 1200)
        flxh.set_ColWidth(6, 1600)

        flxh.Row = 0
        flxh.Col = 0
        flxh.CellAlignment = 3
        flxh.CellFontBold = True
        flxh.set_TextMatrix(0, 0, "Docnum")

        flxh.Col = 1
        flxh.CellAlignment = 3
        flxh.CellFontBold = True
        flxh.set_TextMatrix(0, 1, "DocDate")

        flxh.Col = 2
        flxh.CellAlignment = 3
        flxh.CellFontBold = True
        flxh.set_TextMatrix(0, 2, "Cardcode")

        flxh.Col = 3
        flxh.CellAlignment = 3
        flxh.CellFontBold = True
        flxh.set_TextMatrix(0, 3, "Party Name")

        flxh.Col = 4
        flxh.CellAlignment = 3
        flxh.CellFontBold = True
        flxh.set_TextMatrix(0, 4, "Qty")

        flxh.Col = 5
        flxh.CellAlignment = 3
        flxh.CellFontBold = True
        flxh.set_TextMatrix(0, 5, "Mtrs")

        flxh.Col = 6
        flxh.CellAlignment = 3
        flxh.CellFontBold = True
        flxh.set_TextMatrix(0, 6, "Brand")



        'flx.set_ColAlignment(0, 2)
        'flx.set_ColAlignment(1, 2)
        flxh.set_ColAlignment(2, 2)
        flxh.set_ColAlignment(3, 2)


    End Sub
    Private Sub flxvhead()
        flxv.Rows = 1
        flxv.Cols = 7
        flxv.set_ColWidth(0, 1000)
        flxv.set_ColWidth(1, 1000)
        flxv.set_ColWidth(2, 1200)
        flxv.set_ColWidth(3, 3000)
        flxv.set_ColWidth(4, 1200)
        flxv.set_ColWidth(5, 1200)
        flxv.set_ColWidth(6, 1600)

        flxv.Row = 0
        flxv.Col = 0
        flxv.CellAlignment = 3
        flxv.CellFontBold = True
        flxv.set_TextMatrix(0, 0, "Docnum")

        flxv.Col = 1
        flxv.CellAlignment = 3
        flxv.CellFontBold = True
        flxv.set_TextMatrix(0, 1, "DocDate")

        flxv.Col = 2
        flxv.CellAlignment = 3
        flxv.CellFontBold = True
        flxv.set_TextMatrix(0, 2, "Cardcode")

        flxv.Col = 3
        flxv.CellAlignment = 3
        flxv.CellFontBold = True
        flxv.set_TextMatrix(0, 3, "Party Name")

        flxv.Col = 4
        flxv.CellAlignment = 3
        flxv.CellFontBold = True
        flxv.set_TextMatrix(0, 4, "Qty")

        flxv.Col = 5
        flxv.CellAlignment = 3
        flxv.CellFontBold = True
        flxv.set_TextMatrix(0, 5, "Mtrs")

        flxv.Col = 6
        flxv.CellAlignment = 3
        flxv.CellFontBold = True
        flxv.set_TextMatrix(0, 6, "Brand")



        'flx.set_ColAlignment(0, 2)
        'flx.set_ColAlignment(1, 2)
        flxv.set_ColAlignment(2, 2)
        flxv.set_ColAlignment(3, 2)


    End Sub
    Private Sub flxwhead()
        flxw.Rows = 1
        flxw.Cols = 7
        flxw.set_ColWidth(0, 1000)
        flxw.set_ColWidth(1, 1000)
        flxw.set_ColWidth(2, 1200)
        flxw.set_ColWidth(3, 3000)
        flxw.set_ColWidth(4, 1200)
        flxw.set_ColWidth(5, 1200)
        flxw.set_ColWidth(6, 1600)

        flxw.Row = 0
        flxw.Col = 0
        flxw.CellAlignment = 3
        flxw.CellFontBold = True
        flxw.set_TextMatrix(0, 0, "Docnum")

        flxw.Col = 1
        flxw.CellAlignment = 3
        flxw.CellFontBold = True
        flxw.set_TextMatrix(0, 1, "DocDate")

        flxw.Col = 2
        flxw.CellAlignment = 3
        flxw.CellFontBold = True
        flxw.set_TextMatrix(0, 2, "Cardcode")

        flxw.Col = 3
        flxw.CellAlignment = 3
        flxw.CellFontBold = True
        flxw.set_TextMatrix(0, 3, "Party Name")

        flxw.Col = 4
        flxw.CellAlignment = 3
        flxw.CellFontBold = True
        flxw.set_TextMatrix(0, 4, "Qty")

        flxw.Col = 5
        flxw.CellAlignment = 3
        flxw.CellFontBold = True
        flxw.set_TextMatrix(0, 5, "Mtrs")

        flxw.Col = 6
        flxw.CellAlignment = 3
        flxw.CellFontBold = True
        flxw.set_TextMatrix(0, 6, "Brand")



        'flx.set_ColAlignment(0, 2)
        'flx.set_ColAlignment(1, 2)
        flxw.set_ColAlignment(2, 2)
        flxw.set_ColAlignment(3, 2)


    End Sub

    Private Sub loaddata()
        msql2 = "select k.docnum from" & vbCrLf _
       & "(select T0.Docnum,T0.DocDate,T0.Cardcode,T0.Cardname ,isnull(sum(T1.linetotal),0) [OrderTotal], " & vbCrLf _
       & " isnull(td.deltotal,0) as [deltotal],isnull(td2.deltotal,0) as delctotal ,isnull(T2.invtotal,0) as [InvoiceTotal],isnull(t3.invtotal,0) as invctotal, isnull(T2.RtnTotal,0) as [Rtntotal]," & vbCrLf _
       & "(isnull(sum(T1.linetotal),0)-(isnull(td.deltotal,0)+ isnull(td2.deltotal,0)+isnull(T2.invtotal,0))) as balordamt," & vbCrLf _
       & " isnull(sum(t1.quantity),0) as ordqty,isnull(td.delqty,0) as [dqty],isnull(td2.delqty,0) as [dcqty],isnull(t2.invqty,0) as [invqty],isnull(t3.invqty,0) as [invcqty],isnull(t2.rtnqty,0) as [rtnqty]," & vbCrLf _
       & "(isnull(sum(t1.quantity),0)-(isnull(td.delqty,0)+isnull(td2.delqty,0)+isnull(t2.invqty,0)+isnull(t3.invqty,0))) as balordqty,Tp.agent,T0.U_Brand,tp.City,tp.district,tp.state,sd.u_name as subbrand,t0.docstatus,t0.u_team,t0.u_lr_date " & vbCrLf _
       & " from Ordr T0 " & vbCrLf _
       & "INNER JOIN rdr1 T1 on T0.DocEntry=T1.DocEntry and t1.treetype<>'I' " & vbCrLf _
       & " left join ( select tr1.cardcode,tr1.cardname,tr1.u_areacode as agent,tr2.city,tr2.county as district,tr2.state from ocrd  tr1 " & vbCrLf _
       & " inner join CRD1 tr2 on tr2.CardCode=tr1.CardCode and tr2.AdresType='B') tp on tp.CardCode=T0.CardCode " & vbCrLf _
       & " Left Join " & vbCrLf _
       & "(select k.baseentry,k.basetype, k.ObjType,SUM(k.invtotal) as deltotal,SUM(k.invqty) as delqty from  " & vbCrLf _
       & "(Select  t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.lineTotal),0) as invTotal,sum(t1.quantity) as invqty  from Odln T0 " & vbCrLf _
       & "inner join dln1 as t1 on t1.docentry=T0.docentry and t1.TreeType<>'I' " & vbCrLf _
       & " group by t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType ) k " & vbCrLf _
       & " group by k.baseentry,k.basetype, k.ObjType)  td on td.basetype=T1.objtype and Td.baseentry=T1.docEntry " & vbCrLf _
       & " Left Join " & vbCrLf _
          & " (select k.BaseEntry as baseentry,k.basetype,k.ObjType,SUM(k.deltotal) as deltotal,SUM(k.delqty) as delqty ,SUM(k.rtntotal)  as rtntotal,SUM(k.rtnqty) as rtnqty " & vbCrLf _
          & "from (Select  t1.U_BDocENtry as baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as delTotal,isnull(sum(t1.quantity),0) as delqty ,0 as rtnTotal,0 as rtnqty " & vbCrLf _
          & " from Odln T0 " & vbCrLf _
          & " inner join dln1 as t1 on t1.docentry=T0.docentry and t1.BaseType=17 and t1.TreeType<>'I' " & vbCrLf _
           & " where(t1.U_BDocENtry Is Not null) " & vbCrLf _
           & " group by t1.U_BDocENtry,t1.BaseType,T0.DocEntry, T0.ObjType ,isnull(T0.DocTotal,0)  ) k " & vbCrLf _
           & " group by k.baseentry,k.basetype, k.ObjType) Td2 on td2.basetype=T1.objtype and Td2.baseentry=T1.docEntry  " & vbCrLf _
           & "   Left Join " & vbCrLf _
           & " (select k.baseentry,k.basetype,k.ObjType,SUM(k.invtotal) as invtotal,SUM(k.invqty) as invqty ,SUM(k.rtntotal) as rtntotal,SUM(k.rtnqty) as rtnqty  from " & vbCrLf _
           & " (Select  t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as invTotal,sum(t1.quantity) as  invqty ,isnull(Tr.rtntotal,0) as rtnTotal,tr.rtnqty as rtnqty " & vbCrLf _
           & " from Oinv T0 " & vbCrLf _
           & "inner join inv1 as t1 on t1.docentry=T0.docentry and t1.basetype=17  and t1.TreeType<>'I' " & vbCrLf _
           & " left join (Select  t1.BaseRef, t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as  rtnTotal,sum(t1.quantity) as rtnqty from Orin T0 " & vbCrLf _
           & " inner join rin1 as t1 on t1.docentry=T0.docentry and t1.TreeType<>'I' " & vbCrLf _
           & " group by t1.baseref,t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType ) tr on tr.Baseref=T1.DocEntry " & vbCrLf _
           & " where(t1.U_BDocENtry Is null) " & vbCrLf _
           & " group by t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(Tr.rtntotal,0),tr.rtnqty ) k " & vbCrLf _
           & " group by k.baseentry,k.basetype, k.ObjType) T2 on t2.basetype=T1.objtype and T2.baseentry=T1.docEntry  " & vbCrLf _
           & " Left Join " & vbCrLf _
           & " (select k.BaseEntry as baseentry,k.basetype,k.ObjType,SUM(k.invtotal) as invtotal,SUM(k.invqty) as invqty ,SUM(k.rtntotal) as rtntotal,SUM(k.rtnqty) as rtnqty " & vbCrLf _
           & " from (Select  t1.U_BDocENtry as baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as invTotal,isnull(sum(t1.quantity),0) as invqty ,0 as rtnTotal,0 as rtnqty " & vbCrLf _
           & " from Oinv T0 " & vbCrLf _
           & " inner join inv1 as t1 on t1.docentry=T0.docentry and t1.BaseType=17 and t1.TreeType<>'I' " & vbCrLf _
           & " where(t1.U_BDocENtry Is Not null) " & vbCrLf _
           & " group by t1.U_BDocENtry,t1.BaseType,T0.DocEntry, T0.ObjType ,isnull(T0.DocTotal,0)  ) k " & vbCrLf _
           & " group by k.baseentry,k.basetype, k.ObjType) T3 on t3.basetype=T1.objtype and T3.baseentry=T1.docEntry " & vbCrLf _
           & " left join [@incm_sbd1] sd on sd.LineId=T0.U_SubBrand " & vbCrLf _
           & " where T0.DocDate>='" & Microsoft.VisualBasic.Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "'   and  T0.DocDate<='" & Microsoft.VisualBasic.Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and T0.Canceled='N' and t0.confirmed='Y' " & vbCrLf _
           & " group by T0.Docnum,T0.DocDate,T0.Cardcode,T0.Cardname,isnull(T0.doctotal,0),isnull(td.deltotal,0),isnull(td2.deltotal,0),  " & vbCrLf _
           & " isnull(T2.invtotal,0),isnull(t3.invtotal,0),isnull(T2.RtnTotal,0),isnull(td.delqty,0),isnull(td2.delqty,0),isnull(t2.invqty,0),isnull(t3.invqty,0),isnull(t2.rtnqty,0)," & vbCrLf _
           & " Tp.agent, T0.U_Brand, tp.City, tp.district, tp.state, sd.u_name,t0.docstatus,t0.u_team,t0.u_lr_date " & vbCrLf _
          & " having (isnull(sum(t1.quantity),0)-(isnull(td.delqty,0)+isnull(td2.delqty,0)+isnull(t2.invqty,0)+isnull(t3.invqty,0)))<>0) k " & vbCrLf _
          & "  where k.u_lr_date='" & Microsoft.VisualBasic.Format(CDate(mskddate.Text), "yyyy-MM-dd") & "'"




        msql = "select k.docnum,k.docdate,k.cardcode,k.cardname,CASE when charindex('SHIRT', u_brand)<=0 then k.balordqty else 0 end as qty,CASE when charindex('SHIRT',u_brand)>0 then k.balordqty else 0 end as mtrs,k.U_brand from" & vbCrLf _
       & "(select T0.Docnum,T0.DocDate,T0.Cardcode,T0.Cardname ,isnull(sum(T1.linetotal),0) [OrderTotal], " & vbCrLf _
       & " isnull(td.deltotal,0) as [deltotal],isnull(td2.deltotal,0) as delctotal ,isnull(T2.invtotal,0) as [InvoiceTotal],isnull(t3.invtotal,0) as invctotal, isnull(T2.RtnTotal,0) as [Rtntotal]," & vbCrLf _
       & "(isnull(sum(T1.linetotal),0)-(isnull(td.deltotal,0)+ isnull(td2.deltotal,0)+isnull(T2.invtotal,0))) as balordamt," & vbCrLf _
       & " isnull(sum(t1.quantity),0) as ordqty,isnull(td.delqty,0) as [dqty],isnull(td2.delqty,0) as [dcqty],isnull(t2.invqty,0) as [invqty],isnull(t3.invqty,0) as [invcqty],isnull(t2.rtnqty,0) as [rtnqty]," & vbCrLf _
       & "(isnull(sum(t1.quantity),0)-(isnull(td.delqty,0)+isnull(td2.delqty,0)+isnull(t2.invqty,0)+isnull(t3.invqty,0))) as balordqty,Tp.agent,T0.U_Brand,tp.City,tp.district,tp.state,sd.u_name as subbrand,t0.docstatus " & vbCrLf _
       & " from Ordr T0 " & vbCrLf _
       & "INNER JOIN rdr1 T1 on T0.DocEntry=T1.DocEntry and t1.treetype<>'I' " & vbCrLf _
       & " left join ( select tr1.cardcode,tr1.cardname,tr1.u_areacode as agent,tr2.city,tr2.county as district,tr2.state from ocrd  tr1 " & vbCrLf _
       & " inner join CRD1 tr2 on tr2.CardCode=tr1.CardCode and tr2.AdresType='B') tp on tp.CardCode=T0.CardCode " & vbCrLf _
       & " Left Join " & vbCrLf _
       & "(select k.baseentry,k.basetype, k.ObjType,SUM(k.invtotal) as deltotal,SUM(k.invqty) as delqty from  " & vbCrLf _
       & "(Select  t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.lineTotal),0) as invTotal,sum(t1.quantity) as invqty  from Odln T0 " & vbCrLf _
       & "inner join dln1 as t1 on t1.docentry=T0.docentry and t1.TreeType<>'I' " & vbCrLf _
       & " group by t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType ) k " & vbCrLf _
       & " group by k.baseentry,k.basetype, k.ObjType)  td on td.basetype=T1.objtype and Td.baseentry=T1.docEntry " & vbCrLf _
       & " Left Join " & vbCrLf _
          & " (select k.BaseEntry as baseentry,k.basetype,k.ObjType,SUM(k.deltotal) as deltotal,SUM(k.delqty) as delqty ,SUM(k.rtntotal)  as rtntotal,SUM(k.rtnqty) as rtnqty " & vbCrLf _
          & "from (Select  t1.U_BDocENtry as baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as delTotal,isnull(sum(t1.quantity),0) as delqty ,0 as rtnTotal,0 as rtnqty " & vbCrLf _
          & " from Odln T0 " & vbCrLf _
          & " inner join dln1 as t1 on t1.docentry=T0.docentry and t1.BaseType=17 and t1.TreeType<>'I' " & vbCrLf _
           & " where(t1.U_BDocENtry Is Not null) " & vbCrLf _
           & " group by t1.U_BDocENtry,t1.BaseType,T0.DocEntry, T0.ObjType ,isnull(T0.DocTotal,0)  ) k " & vbCrLf _
           & " group by k.baseentry,k.basetype, k.ObjType) Td2 on td2.basetype=T1.objtype and Td2.baseentry=T1.docEntry  " & vbCrLf _
           & "   Left Join " & vbCrLf _
           & " (select k.baseentry,k.basetype,k.ObjType,SUM(k.invtotal) as invtotal,SUM(k.invqty) as invqty ,SUM(k.rtntotal) as rtntotal,SUM(k.rtnqty) as rtnqty  from " & vbCrLf _
           & " (Select  t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as invTotal,sum(t1.quantity) as  invqty ,isnull(Tr.rtntotal,0) as rtnTotal,tr.rtnqty as rtnqty " & vbCrLf _
           & " from Oinv T0 " & vbCrLf _
           & "inner join inv1 as t1 on t1.docentry=T0.docentry and t1.basetype=17  and t1.TreeType<>'I' " & vbCrLf _
           & " left join (Select  t1.BaseRef, t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as  rtnTotal,sum(t1.quantity) as rtnqty from Orin T0 " & vbCrLf _
           & " inner join rin1 as t1 on t1.docentry=T0.docentry and t1.TreeType<>'I' " & vbCrLf _
           & " group by t1.baseref,t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType ) tr on tr.Baseref=T1.DocEntry " & vbCrLf _
           & " where(t1.U_BDocENtry Is null) " & vbCrLf _
           & " group by t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(Tr.rtntotal,0),tr.rtnqty ) k " & vbCrLf _
           & " group by k.baseentry,k.basetype, k.ObjType) T2 on t2.basetype=T1.objtype and T2.baseentry=T1.docEntry  " & vbCrLf _
           & " Left Join " & vbCrLf _
           & " (select k.BaseEntry as baseentry,k.basetype,k.ObjType,SUM(k.invtotal) as invtotal,SUM(k.invqty) as invqty ,SUM(k.rtntotal) as rtntotal,SUM(k.rtnqty) as rtnqty " & vbCrLf _
           & " from (Select  t1.U_BDocENtry as baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as invTotal,isnull(sum(t1.quantity),0) as invqty ,0 as rtnTotal,0 as rtnqty " & vbCrLf _
           & " from Oinv T0 " & vbCrLf _
           & " inner join inv1 as t1 on t1.docentry=T0.docentry and t1.BaseType=17 and t1.TreeType<>'I' " & vbCrLf _
           & " where(t1.U_BDocENtry Is Not null) " & vbCrLf _
           & " group by t1.U_BDocENtry,t1.BaseType,T0.DocEntry, T0.ObjType ,isnull(T0.DocTotal,0)  ) k " & vbCrLf _
           & " group by k.baseentry,k.basetype, k.ObjType) T3 on t3.basetype=T1.objtype and T3.baseentry=T1.docEntry " & vbCrLf _
           & " left join [@incm_sbd1] sd on sd.LineId=T0.U_SubBrand " & vbCrLf _
           & " where T0.DocDate>='" & Microsoft.VisualBasic.Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "'   and  T0.DocDate<='" & Microsoft.VisualBasic.Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and T0.Canceled='N' and t0.confirmed='Y' " & vbCrLf _
           & " group by T0.Docnum,T0.DocDate,T0.Cardcode,T0.Cardname,isnull(T0.doctotal,0),isnull(td.deltotal,0),isnull(td2.deltotal,0),  " & vbCrLf _
           & " isnull(T2.invtotal,0),isnull(t3.invtotal,0),isnull(T2.RtnTotal,0),isnull(td.delqty,0),isnull(td2.delqty,0),isnull(t2.invqty,0),isnull(t3.invqty,0),isnull(t2.rtnqty,0)," & vbCrLf _
           & " Tp.agent, T0.U_Brand, tp.City, tp.district, tp.state, sd.u_name,t0.docstatus " & vbCrLf _
          & " having (isnull(sum(t1.quantity),0)-(isnull(td.delqty,0)+isnull(td2.delqty,0)+isnull(t2.invqty,0)+isnull(t3.invqty,0)))<>0) k " & vbCrLf _
          & "  where k.balordqty > 0 and k.docstatus='O' and k.docnum not in (" & msql2 & ") order by k.docnum"


        Dim CMD As New OleDb.OleDbCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()
        flxh.Clear()
        Call flxhhead()
        flxh.Visible = False

        Try
            ''Dim DR As SqlDataReader
            Dim DR As OleDb.OleDbDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then
                With flxh
                    While DR.Read
                        .Rows = .Rows + 1
                        .Row = .Rows - 1
                        .set_TextMatrix(.Row, 0, DR.Item("docnum"))
                        .set_TextMatrix(.Row, 1, DR.Item("docdate"))
                        .set_TextMatrix(.Row, 2, DR.Item("cardcode"))
                        .set_TextMatrix(.Row, 3, DR.Item("cardname"))
                        .set_TextMatrix(.Row, 4, DR.Item("qty"))
                        .set_TextMatrix(.Row, 5, DR.Item("mtrs"))
                        .set_TextMatrix(.Row, 6, DR.Item("u_brand") & vbNullString)
                        '.set_TextMatrix(.Row, 7, DR.Item("labcol"))
                        '.set_TextMatrix(.Row, 8, DR.Item("printer") & vbNullString)


                    End While
                End With
            End If
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            flxh.Clear()
            Call flxhhead()
        End Try
        flxh.Visible = True
        CMD.Dispose()
        Call TOTMAIN()
        Call TOTV()
        Call TOTw()
        Call loaddata2()


    End Sub

    Private Sub loaddata2()
        msql = "select k.docnum,k.docdate,k.cardcode,k.cardname,CASE when charindex('SHIRT', u_brand)<=0 then k.balordqty else 0 end as qty,CASE when charindex('SHIRT',u_brand)>0 then k.balordqty else 0 end as mtrs,k.U_brand,k.u_team from" & vbCrLf _
       & "(select T0.Docnum,T0.DocDate,T0.Cardcode,T0.Cardname ,isnull(sum(T1.linetotal),0) [OrderTotal], " & vbCrLf _
       & " isnull(td.deltotal,0) as [deltotal],isnull(td2.deltotal,0) as delctotal ,isnull(T2.invtotal,0) as [InvoiceTotal],isnull(t3.invtotal,0) as invctotal, isnull(T2.RtnTotal,0) as [Rtntotal]," & vbCrLf _
       & "(isnull(sum(T1.linetotal),0)-(isnull(td.deltotal,0)+ isnull(td2.deltotal,0)+isnull(T2.invtotal,0))) as balordamt," & vbCrLf _
       & " isnull(sum(t1.quantity),0) as ordqty,isnull(td.delqty,0) as [dqty],isnull(td2.delqty,0) as [dcqty],isnull(t2.invqty,0) as [invqty],isnull(t3.invqty,0) as [invcqty],isnull(t2.rtnqty,0) as [rtnqty]," & vbCrLf _
       & "(isnull(sum(t1.quantity),0)-(isnull(td.delqty,0)+isnull(td2.delqty,0)+isnull(t2.invqty,0)+isnull(t3.invqty,0))) as balordqty,Tp.agent,T0.U_Brand,tp.City,tp.district,tp.state,sd.u_name as subbrand,t0.docstatus,t0.u_team,t0.u_lr_date " & vbCrLf _
       & " from Ordr T0 " & vbCrLf _
       & "INNER JOIN rdr1 T1 on T0.DocEntry=T1.DocEntry and t1.treetype<>'I' " & vbCrLf _
       & " left join ( select tr1.cardcode,tr1.cardname,tr1.u_areacode as agent,tr2.city,tr2.county as district,tr2.state from ocrd  tr1 " & vbCrLf _
       & " inner join CRD1 tr2 on tr2.CardCode=tr1.CardCode and tr2.AdresType='B') tp on tp.CardCode=T0.CardCode " & vbCrLf _
       & " Left Join " & vbCrLf _
       & "(select k.baseentry,k.basetype, k.ObjType,SUM(k.invtotal) as deltotal,SUM(k.invqty) as delqty from  " & vbCrLf _
       & "(Select  t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.lineTotal),0) as invTotal,sum(t1.quantity) as invqty  from Odln T0 " & vbCrLf _
       & "inner join dln1 as t1 on t1.docentry=T0.docentry and t1.TreeType<>'I' " & vbCrLf _
       & " group by t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType ) k " & vbCrLf _
       & " group by k.baseentry,k.basetype, k.ObjType)  td on td.basetype=T1.objtype and Td.baseentry=T1.docEntry " & vbCrLf _
       & " Left Join " & vbCrLf _
          & " (select k.BaseEntry as baseentry,k.basetype,k.ObjType,SUM(k.deltotal) as deltotal,SUM(k.delqty) as delqty ,SUM(k.rtntotal)  as rtntotal,SUM(k.rtnqty) as rtnqty " & vbCrLf _
          & "from (Select  t1.U_BDocENtry as baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as delTotal,isnull(sum(t1.quantity),0) as delqty ,0 as rtnTotal,0 as rtnqty " & vbCrLf _
          & " from Odln T0 " & vbCrLf _
          & " inner join dln1 as t1 on t1.docentry=T0.docentry and t1.BaseType=17 and t1.TreeType<>'I' " & vbCrLf _
           & " where(t1.U_BDocENtry Is Not null) " & vbCrLf _
           & " group by t1.U_BDocENtry,t1.BaseType,T0.DocEntry, T0.ObjType ,isnull(T0.DocTotal,0)  ) k " & vbCrLf _
           & " group by k.baseentry,k.basetype, k.ObjType) Td2 on td2.basetype=T1.objtype and Td2.baseentry=T1.docEntry  " & vbCrLf _
           & "   Left Join " & vbCrLf _
           & " (select k.baseentry,k.basetype,k.ObjType,SUM(k.invtotal) as invtotal,SUM(k.invqty) as invqty ,SUM(k.rtntotal) as rtntotal,SUM(k.rtnqty) as rtnqty  from " & vbCrLf _
           & " (Select  t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as invTotal,sum(t1.quantity) as  invqty ,isnull(Tr.rtntotal,0) as rtnTotal,tr.rtnqty as rtnqty " & vbCrLf _
           & " from Oinv T0 " & vbCrLf _
           & "inner join inv1 as t1 on t1.docentry=T0.docentry and t1.basetype=17  and t1.TreeType<>'I' " & vbCrLf _
           & " left join (Select  t1.BaseRef, t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as  rtnTotal,sum(t1.quantity) as rtnqty from Orin T0 " & vbCrLf _
           & " inner join rin1 as t1 on t1.docentry=T0.docentry and t1.TreeType<>'I' " & vbCrLf _
           & " group by t1.baseref,t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType ) tr on tr.Baseref=T1.DocEntry " & vbCrLf _
           & " where(t1.U_BDocENtry Is null) " & vbCrLf _
           & " group by t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(Tr.rtntotal,0),tr.rtnqty ) k " & vbCrLf _
           & " group by k.baseentry,k.basetype, k.ObjType) T2 on t2.basetype=T1.objtype and T2.baseentry=T1.docEntry  " & vbCrLf _
           & " Left Join " & vbCrLf _
           & " (select k.BaseEntry as baseentry,k.basetype,k.ObjType,SUM(k.invtotal) as invtotal,SUM(k.invqty) as invqty ,SUM(k.rtntotal) as rtntotal,SUM(k.rtnqty) as rtnqty " & vbCrLf _
           & " from (Select  t1.U_BDocENtry as baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as invTotal,isnull(sum(t1.quantity),0) as invqty ,0 as rtnTotal,0 as rtnqty " & vbCrLf _
           & " from Oinv T0 " & vbCrLf _
           & " inner join inv1 as t1 on t1.docentry=T0.docentry and t1.BaseType=17 and t1.TreeType<>'I' " & vbCrLf _
           & " where(t1.U_BDocENtry Is Not null) " & vbCrLf _
           & " group by t1.U_BDocENtry,t1.BaseType,T0.DocEntry, T0.ObjType ,isnull(T0.DocTotal,0)  ) k " & vbCrLf _
           & " group by k.baseentry,k.basetype, k.ObjType) T3 on t3.basetype=T1.objtype and T3.baseentry=T1.docEntry " & vbCrLf _
           & " left join [@incm_sbd1] sd on sd.LineId=T0.U_SubBrand " & vbCrLf _
           & " where T0.DocDate>='" & Microsoft.VisualBasic.Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "'   and  T0.DocDate<='" & Microsoft.VisualBasic.Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and T0.Canceled='N'  " & vbCrLf _
           & " group by T0.Docnum,T0.DocDate,T0.Cardcode,T0.Cardname,isnull(T0.doctotal,0),isnull(td.deltotal,0),isnull(td2.deltotal,0),  " & vbCrLf _
           & " isnull(T2.invtotal,0),isnull(t3.invtotal,0),isnull(T2.RtnTotal,0),isnull(td.delqty,0),isnull(td2.delqty,0),isnull(t2.invqty,0),isnull(t3.invqty,0),isnull(t2.rtnqty,0)," & vbCrLf _
           & " Tp.agent, T0.U_Brand, tp.City, tp.district, tp.state, sd.u_name,t0.docstatus,t0.u_team,t0.u_lr_date " & vbCrLf _
           & " having (isnull(sum(t1.quantity),0)-(isnull(td.delqty,0)+isnull(td2.delqty,0)+isnull(t2.invqty,0)+isnull(t3.invqty,0)))<>0) k " & vbCrLf _
           & "  where k.u_lr_date='" & Microsoft.VisualBasic.Format(CDate(mskddate.Text), "yyyy-MM-dd") & "'  order by k.docnum"


        Dim CMD As New OleDb.OleDbCommand(msql, con)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        'Dim trans As OleDb.OleDbTransaction = con.BeginTransaction
        'trans.Begin()
        flxv.Clear()
        Call flxvhead()
        flxw.Clear()
        Call flxwhead()
        Try
            ''Dim DR As SqlDataReader
            Dim DR As OleDb.OleDbDataReader
            DR = CMD.ExecuteReader
            If DR.HasRows = True Then

                While DR.Read
                    
                    If Trim(DR.Item("u_team")) = "VICTORY" Then
                        flxv.Rows = flxv.Rows + 1
                        flxv.Row = flxv.Rows - 1
                        flxv.set_TextMatrix(flxv.Row, 0, DR.Item("docnum"))
                        flxv.set_TextMatrix(flxv.Row, 1, DR.Item("docdate"))
                        flxv.set_TextMatrix(flxv.Row, 2, DR.Item("cardcode"))
                        flxv.set_TextMatrix(flxv.Row, 3, DR.Item("cardname"))
                        flxv.set_TextMatrix(flxv.Row, 4, DR.Item("qty"))
                        flxv.set_TextMatrix(flxv.Row, 5, DR.Item("mtrs"))
                        flxv.set_TextMatrix(flxv.Row, 6, DR.Item("u_brand") & vbNullString)
                    End If
                    If Trim(DR.Item("u_team")) = "WINNER" Then
                        flxw.Rows = flxw.Rows + 1
                        flxw.Row = flxw.Rows - 1
                        flxw.set_TextMatrix(flxw.Row, 0, DR.Item("docnum"))
                        flxw.set_TextMatrix(flxw.Row, 1, DR.Item("docdate"))
                        flxw.set_TextMatrix(flxw.Row, 2, DR.Item("cardcode"))
                        flxw.set_TextMatrix(flxw.Row, 3, DR.Item("cardname"))
                        flxw.set_TextMatrix(flxw.Row, 4, DR.Item("qty"))
                        flxw.set_TextMatrix(flxw.Row, 5, DR.Item("mtrs"))
                        flxw.set_TextMatrix(flxw.Row, 6, DR.Item("u_brand") & vbNullString)
                    End If

                End While

            End If
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            flxv.Clear()
            Call flxvhead()
            flxw.Clear()
            Call flxwhead()
        End Try

        CMD.Dispose()
        Call TOTMAIN()
        Call TOTV()
        Call TOTw()

    End Sub

    Private Sub cmddisp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddisp.Click
        Call loaddata()

    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub flxh_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxh.Enter

    End Sub

    Private Sub flxh_KeyUpEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyUpEvent) Handles flxh.KeyUpEvent
        'Victory F9
        If e.keyCode = Keys.F9 Then
            flxv.Rows = flxv.Rows + 1
            flxv.Row = flxv.Rows - 1
            flxv.set_TextMatrix(flxv.Row, 0, flxh.get_TextMatrix(flxh.Row, 0))
            flxv.set_TextMatrix(flxv.Row, 1, flxh.get_TextMatrix(flxh.Row, 1))
            flxv.set_TextMatrix(flxv.Row, 2, flxh.get_TextMatrix(flxh.Row, 2))
            flxv.set_TextMatrix(flxv.Row, 3, flxh.get_TextMatrix(flxh.Row, 3))
            flxv.set_TextMatrix(flxv.Row, 4, flxh.get_TextMatrix(flxh.Row, 4))
            flxv.set_TextMatrix(flxv.Row, 5, flxh.get_TextMatrix(flxh.Row, 5))
            flxv.set_TextMatrix(flxv.Row, 6, flxh.get_TextMatrix(flxh.Row, 6))

            If flxh.Rows - 1 > 0 Then
                flxh.RemoveItem(flxh.Row)
            Else
                flxh.Clear()
                Call flxhhead()
            End If
            Call TOTMAIN()
            Call TOTV()
            flxh.Focus()
        End If
        'Winner -F10
        If e.keyCode = Keys.F10 Then
            flxw.Rows = flxw.Rows + 1
            flxw.Row = flxw.Rows - 1
            flxw.set_TextMatrix(flxw.Row, 0, flxh.get_TextMatrix(flxh.Row, 0))
            flxw.set_TextMatrix(flxw.Row, 1, flxh.get_TextMatrix(flxh.Row, 1))
            flxw.set_TextMatrix(flxw.Row, 2, flxh.get_TextMatrix(flxh.Row, 2))
            flxw.set_TextMatrix(flxw.Row, 3, flxh.get_TextMatrix(flxh.Row, 3))
            flxw.set_TextMatrix(flxw.Row, 4, flxh.get_TextMatrix(flxh.Row, 4))
            flxw.set_TextMatrix(flxw.Row, 5, flxh.get_TextMatrix(flxh.Row, 5))
            flxw.set_TextMatrix(flxw.Row, 6, flxh.get_TextMatrix(flxh.Row, 6))

            If flxh.Rows - 1 > 0 Then
                flxh.RemoveItem(flxh.Row)
            Else
                flxh.Clear()
                Call flxhhead()
            End If
            Call TOTMAIN()
            Call TOTw()
            flxh.Focus()
        End If



    End Sub

    Private Sub TOTMAIN()
        lblhqty.Text = 0
        lblhmtrs.Text = 0

        For i = 1 To flxh.Rows - 1
            lblhqty.Text = Microsoft.VisualBasic.Format(Val(lblhqty.Text) + Val(flxh.get_TextMatrix(i, 4)), "########0")
            lblhmtrs.Text = Microsoft.VisualBasic.Format(Val(lblhmtrs.Text) + Val(flxh.get_TextMatrix(i, 5)), "########0.00")
        Next i

    End Sub

    Private Sub TOTV()
        lblvqty.Text = 0
        lblvmtrs.Text = 0
        For k = 1 To flxv.Rows - 1
            lblvqty.Text = Microsoft.VisualBasic.Format(Val(lblvqty.Text) + Val(flxv.get_TextMatrix(k, 4)), "########0")
            lblvmtrs.Text = Microsoft.VisualBasic.Format(Val(lblvmtrs.Text) + Val(flxv.get_TextMatrix(k, 5)), "########0.00")
        Next k

    End Sub

    Private Sub TOTw()
        lblwqty.Text = 0
        lblwmtrs.Text = 0
        For j = 1 To flxw.Rows - 1
            lblwqty.Text = Microsoft.VisualBasic.Format(Val(lblwqty.Text) + Val(flxw.get_TextMatrix(j, 4)), "########0")
            lblwmtrs.Text = Microsoft.VisualBasic.Format(Val(lblwmtrs.Text) + Val(flxw.get_TextMatrix(j, 5)), "########0.00")
        Next j
    End Sub


    Private Sub flxv_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxv.Enter

    End Sub

    Private Sub flxv_KeyUpEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyUpEvent) Handles flxv.KeyUpEvent
        'F9-VICTORY
        If e.keyCode = Keys.F9 Then
            flxh.Rows = flxh.Rows + 1
            flxh.Row = flxh.Rows - 1
            flxh.set_TextMatrix(flxh.Row, 0, flxv.get_TextMatrix(flxv.Row, 0))
            flxh.set_TextMatrix(flxh.Row, 1, flxv.get_TextMatrix(flxv.Row, 1))
            flxh.set_TextMatrix(flxh.Row, 2, flxv.get_TextMatrix(flxv.Row, 2))
            flxh.set_TextMatrix(flxh.Row, 3, flxv.get_TextMatrix(flxv.Row, 3))
            flxh.set_TextMatrix(flxh.Row, 4, flxv.get_TextMatrix(flxv.Row, 4))
            flxh.set_TextMatrix(flxh.Row, 5, flxv.get_TextMatrix(flxv.Row, 5))
            flxh.set_TextMatrix(flxh.Row, 6, flxv.get_TextMatrix(flxv.Row, 6))


            If flxv.Rows - 1 > 1 Then
                flxv.RemoveItem(flxv.Row)
            Else
                flxv.Clear()
                Call flxvhead()
            End If
            Call TOTMAIN()
            Call TOTV()
        End If
    End Sub

    Private Sub flxw_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxw.Enter

    End Sub

    Private Sub flxw_KeyUpEvent(ByVal sender As Object, ByVal e As AxMSFlexGridLib.DMSFlexGridEvents_KeyUpEvent) Handles flxw.KeyUpEvent
        If e.keyCode = Keys.F9 Then
            flxh.Rows = flxh.Rows + 1
            flxh.Row = flxh.Rows - 1
            flxh.set_TextMatrix(flxh.Row, 0, flxw.get_TextMatrix(flxw.Row, 0))
            flxh.set_TextMatrix(flxh.Row, 1, flxw.get_TextMatrix(flxw.Row, 1))
            flxh.set_TextMatrix(flxh.Row, 2, flxw.get_TextMatrix(flxw.Row, 2))
            flxh.set_TextMatrix(flxh.Row, 3, flxw.get_TextMatrix(flxw.Row, 3))
            flxh.set_TextMatrix(flxh.Row, 4, flxw.get_TextMatrix(flxw.Row, 4))
            flxh.set_TextMatrix(flxh.Row, 5, flxw.get_TextMatrix(flxw.Row, 5))
            flxh.set_TextMatrix(flxh.Row, 6, flxw.get_TextMatrix(flxw.Row, 6))


            If flxw.Rows - 1 > 1 Then
                flxw.RemoveItem(flxw.Row)
            Else
                flxw.Clear()
                Call flxwhead()
            End If
            Call TOTMAIN()
            Call TOTw()
        End If
    End Sub

    Private Sub cmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsave.Click

        If MsgBox("Are U Sure!", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then


            For k = 1 To flxv.Rows - 1

                Dim CMD As New OleDb.OleDbCommand("update ordr set u_team='VICTORY',u_lr_date='" & Microsoft.VisualBasic.Format(CDate(mskddate.Text), "yyyy-MM-dd") & "' where docnum=" & Val(flxv.get_TextMatrix(k, 0)), con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                Try
                    CMD.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                '--CMD.ExecuteNonQuery()
            Next k
            MsgBox("Victory Team Saved!")
            For j = 1 To flxw.Rows - 1

                Dim CMD2 As New OleDb.OleDbCommand("update ordr set u_team='WINNER',u_lr_date='" & Microsoft.VisualBasic.Format(CDate(mskddate.Text), "yyyy-MM-dd") & "' where docnum=" & Val(flxw.get_TextMatrix(j, 0)), con)
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                Try
                    CMD2.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Next j
            MsgBox("Winner Team Saved!")

        End If


    End Sub

    Private Sub flxh_SelChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles flxh.SelChange
        flxh.Col = 0
        flxh.Sort = 1
    End Sub

    Private Sub cmdexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexcel.Click
        If MsgBox("Excel Export to Victory Team", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            excelexport2(flxv, "VICTORY - " & mskddate.Text)
        Else
            excelexport2(flxw, "WINNER - " & mskddate.Text)
        End If

    End Sub
End Class
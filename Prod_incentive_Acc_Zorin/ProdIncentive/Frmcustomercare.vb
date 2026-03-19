Public Class Frmcustomercare
    Dim msqlperf, msqlsum, msqlitemsum As String
    Dim msqlout, msqlso, msqlitso As String

    Private Sub StatusStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)

    End Sub

    Private Sub Frmcustomercare_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        msqlperf = " select lk.CardCode,lk.cardname,lk.city,lk.state,lk.agent,lk.district,SUM(lk.pymtdays)/COUNT(lk.Docentry) as Avgpymtdays from " & vbCrLf _
       & "(select t1.docentry,Convert(varchar,t1.docnum) As DocNum,t1.docdate,t1.cardcode,t1.cardname,cr.city,cr.state,t1.doctotal as billamt, isnull(sum(t3.rtnamt),0) as rtnamt, isnull(sum(t2.rctamt),0) as ramt,(t1.doctotal-(isnull(sum(t2.rctamt),0)+isnull(sum(t3.rtnamt),0)) ) as pendamt,datediff(d,t1.docdate,getdate()) as nodays, " & vbCrLf _
       & "cr.agent,cr.district,t1.u_brand as brand,DATEDIFF(DAY,t1.docdate,t2.ReconDate) as pymtdays  from oinv  t1 " & vbCrLf _
       & " Left Join " & vbCrLf _
       & "(select max(d.recondate) as recondate,d.recontype,e.srcobjtyp,e.srcobjabs,e.shortname,sum(e.reconsum) as rctamt from oitr  d " & vbCrLf _
       & " inner join itr1  e on e.reconnum=d.reconnum and e.srcobjtyp=13 and d.ReconType=3 " & vbCrLf _
       & " group by d.recondate,d.recontype,e.srcobjtyp,e.srcobjabs,e.shortname) as t2 on t2.srcobjtyp=t1.objtype and t2.srcobjabs=t1.docEntry " & vbCrLf _
       & " Left Join " & vbCrLf _
       & " (select max(d.recondate) as recondate,d.recontype,e.srcobjtyp,e.srcobjabs,e.shortname,sum(e.reconsum) as rtnamt from oitr  d " & vbCrLf _
       & "  inner join itr1  e on e.reconnum=d.reconnum and e.srcobjtyp=13 and d.ReconType=4 " & vbCrLf _
       & " group by d.recondate,d.recontype,e.srcobjtyp,e.srcobjabs,e.shortname) as t3 on t3.srcobjtyp=t1.objtype and t3.srcobjabs=t1.docEntry " & vbCrLf _
       & " left join (select tr1.cardcode,tr1.cardname,tr1.u_areacode as agent,tr2.city,tr2.county as district,tr2.state from ocrd tr1 " & vbCrLf _
       & "    inner join CRD1 tr2 on tr2.CardCode=tr1.CardCode and tr2.AdresType='B')  cr on cr.cardcode=t1.cardcode " & vbCrLf _
       & " where t1.canceled='N' and  t1.paidtodate>0 " & vbCrLf _
       & " group by t1.docnum,t1.docdate,t1.cardcode,t1.cardname,t1.doctotal,cr.city,cr.state,t1.canceled,t2.ReconDate, cr.agent,cr.district,t1.u_brand,t1.DocEntry " & vbCrLf _
       & " having isnull(sum(t2.rctamt),0) >0 and (t1.doctotal-(isnull(sum(t2.rctamt),0)+isnull(sum(t3.rtnamt),0)) )=0) lk " & vbCrLf _
       & " group by lk.CardCode,lk.cardname,lk.city,lk.state,lk.agent,lk.district " & vbCrLf _
       & " order by lk.agent,lk.City,lk.cardcode"

        msqlsum = "SELECT convert(nvarchar(max),f.U_Remarks) as stype,d.docnum,d.docdate,d.cardcode,d.cardname,c.city,c.state,sum(e.quantity) as totqty,((d.doctotal-(d.vatsum+d.rounddif))+d.discsum) as amount,d.discsum,d.vatsum,d.rounddif,d.doctotal,d.u_taxcode, d.u_brand," & vbCrLf _
                & "D.U_LRNO,D.U_LR_DATE,D.U_PODNo, D.U_SALEMPCODE,D.U_ArCode,D.U_TEAM,c.distributor,c.franchise,c.showroom FROM oinv  d " & vbCrLf _
                & "inner join inv1  e on e.docentry=d.docentry " & vbCrLf _
                & "left join [@INCM_BND1]  f on f.U_Name=d.u_brand and convert(nvarchar(max),f.U_Remarks) is not null " & vbCrLf _
                & "left join (select c0.cardcode,c0.state,c0.city,c0.address,c1.QryGroup1 as distributor,c1.QryGroup2 as franchise,c1.QryGroup3 as showroom from crd1 c0 left join OCRD c1 on c1.CardCode=c0.cardcode where adrestype='B' group by c0.cardcode,c0.state,c0.city,c0.address,c1.QryGroup1,c1.QryGroup2,c1.QryGroup3)  c on c.cardcode=d.cardcode WHERE d.canceled='N' " & vbCrLf _
                & "and (d.DocDate>=[%0] or d.DocDate>=[%0] or  [%0] = '' )" & vbCrLf _
                & "and (d.DocDate<=[%1] or  [%1] = '' )" & vbCrLf _
                & "and (d.u_arcode=[%2] or [%2] = '' )" & vbCrLf _
                & "and (d.cardname=[%3] or [%3] = '' )" & vbCrLf _
                & "and (d.u_brand = [%4] or [%4] = '' )" & vbCrLf _
                & " GROUP BY convert(nvarchar(max),f.U_Remarks),d.u_brand, d.docnum,d.docdate,d.cardcode,d.cardname,c.city,c.state,d.discsum,d.vatsum,d.rounddif,d.doctotal,d.u_taxcode, D.U_LRNO,D.U_LR_DATE,D.U_PODNo, " & vbCrLf _
                & "D.U_SALEMPCODE,D.U_ArCode,D.U_TEAM,c.distributor,c.franchise,c.showroom ORDER BY d.docnum,d.docdate "

        msqlout = "select isnull(case when k.brand is null OR LEN(rtrim(k.brand))=0 then tk.u_remarks else k.brand end,'')  as btype,k.ref1 as docnum,k.duedat as date,k.cardcode,k.cardname,isnull(tk.doctotal,0) as billamt, k.balduedeb as pendamt,case when k.BalDueDeb>0 then datediff(d,k.duedat,getdate()) else 0 end as nodays,lk.agent,lk.City,lk.state,k.memo from " & vbCrLf _
            & "(SELECT T2.CardCode, T2.[CardName],T0.[RefDate],T1.[BaseRef],T1.[Debit],T1.[Credit], " & vbCrLf _
            & "T1.[BalDueDeb], T1.[LineMemo],T2.[MailCity],T3.GroupName,T1.Ref1,case when t1.transtype=-2 then T1.DueDate else T0.RefDate end as duedat,t1.ocrcode3 as brand,t0.memo " & vbCrLf _
            & "FROM dbo.OJDT T0   " & vbCrLf _
            & "INNER JOIN dbo.JDT1 T1 ON T0.TransId = T1.TransId " & vbCrLf _
            & "INNER JOIN dbo.OCRD T2 on T2.CardCode = T1.[ShortName] " & vbCrLf _
            & "INNER JOIN dbo.OCRG T3 on T2.GroupCode = T3.GroupCode " & vbCrLf _
            & "WHERE  T1.[BalDueDeb]<>'0'  and t2.cardtype='C' " & vbCrLf _
            & " union all " & vbCrLf _
            & "SELECT T2.CardCode, T2.[CardName],T0.[RefDate],T1.[BaseRef],T1.[Debit],T1.[Credit], " & vbCrLf _
            & "T1.[BalDueCred]*-1 as balduedeb, T1.[LineMemo],T2.[MailCity],T3.GroupName,T1.Ref1,case when t1.transtype=-2 then T1.DueDate else T0.RefDate end duedat,t1.ocrcode3 as brand,t0.memo " & vbCrLf _
            & "FROM dbo.OJDT T0  " & vbCrLf _
            & "INNER JOIN dbo.JDT1 T1 ON T0.TransId = T1.TransId " & vbCrLf _
            & "INNER JOIN dbo.OCRD T2 on T2.CardCode = T1.[ShortName] " & vbCrLf _
            & "INNER JOIN dbo.OCRG T3 on T2.GroupCode = T3.GroupCode" & vbCrLf _
            & "WHERE   T1.[BalDueCred]<>'0' and t2.cardtype='C'" & vbCrLf _
            & " Group By T2.CardCode, T2.[CardName],T0.[RefDate],T1.[BaseRef],T1.[Debit],T1.[Credit]," & vbCrLf _
            & " T1.[BalDueDeb], T1.[BalDueCred]*-1 ,  T1.[LineMemo],T2.[MailCity],T3.GroupName,T1.Ref1,T1.DueDate,T1.transtype,t1.ocrcode3,t0.memo)k " & vbCrLf _
            & " left join (select tl.docnum,tl.docdate,tl.doctotal,tl.cardcode,tl.u_brand,tg.u_remarks from OINV tl " & vbCrLf _
            & " left join [@INCM_BND1] tg on tg.U_Name=RTRIM(tl.u_brand)) tk on convert(nvarchar(20),tk.DocNum)=k.ref1  and tk.CardCode=k.CardCode  and month(tk.docdate)=MONTH(k.duedat) and YEAR(tk.docdate)=YEAR(k.duedat)  " & vbCrLf _
            & " left join ( select CR1.cardcode,cr1.cardname,cr1.u_areacode as agent,cr2.city,cr2.state from OCRD cr1 " & vbCrLf _
            & " left join CRD1 cr2 on cr2.CardCode=cr1.cardcode and cr2.AdresType='B') lk on lk.CardCode=k.CardCode   " & vbCrLf _
            & " where (k.duedat<=[%0] or [%0] = '' ) and (k.cardname=[%1] or [%1] = '') and (lk.agent = [%2] or [%2] = '') " & vbCrLf _
            & " order by k.CardCode,k.duedat "

        msqlso = "Select T0.Docnum,T0.DocDate,T0.Cardcode,T0.Cardname ,isnull(sum(T1.linetotal),0) [OrderTotal], " & vbCrLf _
            & " isnull(td.deltotal,0) as [deltotal],isnull(td2.deltotal,0) as delctotal ,isnull(T2.invtotal,0) as " & vbCrLf _
            & " [InvoiceTotal],isnull(t3.invtotal,0) as invctotal, isnull(T2.RtnTotal,0) as [Rtntotal]," & vbCrLf _
            & " (isnull(sum(T1.linetotal),0)-(isnull(td.deltotal,0)+ isnull(td2.deltotal,0)+isnull(T2.invtotal,0))) as balordamt," & vbCrLf _
            & " isnull(sum(t1.quantity),0) as ordqty,isnull(td.delqty,0) as [dqty],isnull(td2.delqty,0) as [dcqty],isnull(t2.invqty,0) as " & vbCrLf _
            & " [invqty],isnull(t3.invqty,0) as [invcqty],isnull(t2.rtnqty,0) as [rtnqty]," & vbCrLf _
            & " (isnull(sum(t1.quantity),0)-(isnull(td.delqty,0)+isnull(td2.delqty,0)+isnull(t2.invqty,0)+isnull(t3.invqty,0))) as balordqty," & vbCrLf _
            & " Tp.agent,T0.U_Brand,tp.City,tp.district,tp.state,sd.u_name as subbrand  from Ordr T0 " & vbCrLf _
            & "INNER JOIN rdr1 T1 on T0.DocEntry=T1.DocEntry and t1.treetype<>'I' " & vbCrLf _
            & "left join ( select tr1.cardcode,tr1.cardname,tr1.u_areacode as agent,tr2.city,tr2.county as district,tr2.state from ocrd   tr1 " & vbCrLf _
            & " inner join CRD1 tr2 on tr2.CardCode=tr1.CardCode and tr2.AdresType='B') tp on tp.CardCode=T0.CardCode" & vbCrLf _
            & " Left Join " & vbCrLf _
            & " (select k.baseentry,k.basetype, k.ObjType,SUM(k.invtotal) as deltotal,SUM(k.invqty) as delqty from  " & vbCrLf _
            & " (Select  t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.lineTotal),0) as invTotal,sum(t1.quantity) as " & vbCrLf _
            & " invqty  from Odln T0 " & vbCrLf _
            & " inner join dln1 as t1 on t1.docentry=T0.docentry and t1.TreeType<>'I'" & vbCrLf _
            & " group by t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType ) k " & vbCrLf _
            & " group by k.baseentry,k.basetype, k.ObjType)  td on td.basetype=T1.objtype and Td.baseentry=T1.docEntry " & vbCrLf _
            & "  Left Join " & vbCrLf _
            & "(select k.BaseEntry as baseentry,k.basetype,k.ObjType,SUM(k.deltotal) as deltotal,SUM(k.delqty) as delqty ,SUM(k.rtntotal)  as rtntotal,SUM(k.rtnqty) as rtnqty " & vbCrLf _
            & " from  (Select  t1.U_BDocENtry as baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as delTotal,isnull(sum(t1.quantity),0) as delqty ,0 as rtnTotal,0 as rtnqty " & vbCrLf _
            & " from Odln T0  " & vbCrLf _
            & " inner join dln1 as t1 on t1.docentry=T0.docentry and t1.BaseType=17 and t1.TreeType<>'I' " & vbCrLf _
            & " where(t1.U_BDocENtry Is Not null) " & vbCrLf _
            & " group by t1.U_BDocENtry,t1.BaseType,T0.DocEntry, T0.ObjType ,isnull(T0.DocTotal,0)  ) k " & vbCrLf _
            & " group by k.baseentry,k.basetype, k.ObjType) Td2 on td2.basetype=T1.objtype and Td2.baseentry=T1.docEntry  " & vbCrLf _
            & " Left Join " & vbCrLf _
            & "(select k.baseentry,k.basetype,k.ObjType,SUM(k.invtotal) as invtotal,SUM(k.invqty) as invqty ,SUM(k.rtntotal) as rtntotal,SUM(k.rtnqty) as rtnqty  from " & vbCrLf _
            & " (Select  t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as invTotal,sum(t1.quantity) as invqty ,isnull(sum(Tr.rtntotal),0) as rtnTotal,sum(tr.rtnqty) as rtnqty " & vbCrLf _
            & " from Oinv T0 " & vbCrLf _
            & " inner join inv1 as t1 on t1.docentry=T0.docentry and t1.basetype=17  and t1.TreeType<>'I' " & vbCrLf _
            & " left join (Select  t1.BaseRef, t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as rtnTotal,sum(t1.quantity) as rtnqty,t1.itemcode,t1.dscription,t1.u_catalogname,t1.u_size,t1.u_style from Orin T0 " & vbCrLf _
            & " inner join rin1 as t1 on t1.docentry=T0.docentry and t1.TreeType<>'I' " & vbCrLf _
            & " group by t1.baseref,t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType ,t1.itemcode,t1.dscription,t1.u_catalogname,t1.u_size,t1.u_style) tr on tr.Baseref=T1.DocEntry " & vbCrLf _
            & " where t1.U_BDocENtry Is null " & vbCrLf _
            & " group by t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType ,isnull(T0.DocTotal,0),isnull(Tr.rtntotal,0), t1.itemcode,t1.dscription,t1.u_catalogname,t1.u_size,t1.u_style ) k " & vbCrLf _
            & " group by k.baseentry,k.basetype, k.ObjType) T2 on t2.basetype=T1.objtype and T2.baseentry=T1.docEntry " & vbCrLf _
            & " Left Join " & vbCrLf _
            & " (select k.BaseEntry as baseentry,k.basetype,k.ObjType,SUM(k.invtotal) as invtotal,SUM(k.invqty) as invqty ,SUM(k.rtntotal) as rtntotal,SUM(k.rtnqty) as rtnqty " & vbCrLf _
            & " from " & vbCrLf _
            & " (Select  t1.U_BDocENtry as baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as invTotal,isnull(sum(t1.quantity),0) as invqty ,0 as rtnTotal,0 as rtnqty " & vbCrLf _
            & " from Oinv T0 " & vbCrLf _
            & " inner join inv1 as t1 on t1.docentry=T0.docentry and t1.BaseType=17 and t1.TreeType<>'I' " & vbCrLf _
            & " where(t1.U_BDocENtry Is Not null) " & vbCrLf _
            & " group by t1.U_BDocENtry,t1.BaseType,T0.DocEntry, T0.ObjType ,isnull(T0.DocTotal,0)  ) k " & vbCrLf _
            & " group by k.baseentry,k.basetype, k.ObjType) T3 on t3.basetype=T1.objtype and T3.baseentry=T1.docEntry " & vbCrLf _
            & " left join [@incm_sbd1] sd on sd.LineId=T0.U_SubBrand " & vbCrLf _
            & " where T0.DocDate>='2014-05-01'   and  T0.DocDate<='2014-05-15' and T0.Canceled='N' " & vbCrLf _
            & " group by T0.Docnum,T0.DocDate,T0.Cardcode,T0.Cardname,isnull(T0.doctotal,0),isnull(td.deltotal,0),isnull(td2.deltotal,0)," & vbCrLf _
            & " isnull(T2.invtotal,0),isnull(t3.invtotal,0),isnull(T2.RtnTotal,0),isnull(td.delqty,0),isnull(td2.delqty,0),isnull(t2.invqty,0),isnull(t3.invqty,0),isnull(t2.rtnqty,0)," & vbCrLf _
            & " Tp.agent, T0.U_Brand, tp.City, tp.district, tp.state, sd.u_name " & vbCrLf _
            & " having (isnull(sum(t1.quantity),0)-(isnull(td.delqty,0)+isnull(td2.delqty,0)+isnull(t2.invqty,0)+isnull(t3.invqty,0)))<>0 " & vbCrLf _
            & "ORDER BY T0.DOCNUM "


        msqlitso = "Select T0.Docnum,T0.DocDate, t1.itemcode,t1.dscription as itemname,t1.u_catalogname,t1.u_size,t1.u_style " & vbCrLf _
            & " ,isnull(sum(T1.linetotal),0) [OrderTotal],isnull(td.deltotal,0) as [deltotal],ISNULL(td2.deltotal,0) as delctotal, isnull(T2.invtotal,0) as [InvoiceTotal]," & vbCrLf _
            & " isnull(T3.invtotal,0) as [InvoiceCTotal],isnull(T2.RtnTotal,0) as [Rtntotal]," & vbCrLf _
            & " (isnull(sum(T1.linetotal),0)-(isnull(td.deltotal,0)+ isnull(td2.deltotal,0)+ isnull(T2.invtotal,0)+isnull(T3.invtotal,0))) as balordamt," & vbCrLf _
            & " isnull(sum(t1.quantity),0) as ordqty,isnull(td.delqty,0) as [dqty],ISNULL(td2.delqty,0) as delcqty, isnull(t2.invqty,0) as [invqty]," & vbCrLf _
            & " isnull(t3.invqty,0) as invcqty, isnull(t2.rtnqty,0) as [rtnqty]," & vbCrLf _
            & " (isnull(sum(t1.quantity),0)-(isnull(td.delqty,0)+ISNULL(td2.delqty,0)+ isnull(t2.invqty,0)+ISNULL(t3.invqty,0))) as balordqty," & vbCrLf _
            & " T0.Cardcode,T0.Cardname,Tp.agent,T0.U_Brand,tp.City,tp.district,tp.state,sd.u_name as subbrand," & vbCrLf _
            & " T3.orgcode, T3.orgname, ss.u_length from Ordr T0 " & vbCrLf _
            & " INNER JOIN rdr1 T1 on T0.DocEntry=T1.DocEntry and t1.treetype<>'I'" & vbCrLf _
            & " inner join [@INCM_SZE1] ss on ss.U_Name=T1.U_Size " & vbCrLf _
            & " left join ( select tr1.cardcode,tr1.cardname,tr1.u_areacode as agent,tr2.city,tr2.county as district,tr2.state from ocrd  tr1 " & vbCrLf _
            & " inner join CRD1 tr2 on tr2.CardCode=tr1.CardCode and tr2.AdresType='B') tp on tp.CardCode=T0.CardCode " & vbCrLf _
            & " Left Join " & vbCrLf _
            & " (select k.baseentry,k.basetype, k.ObjType,SUM(k.invtotal) as deltotal,SUM(k.invqty) as delqty,k.itemcode,k.dscription,k.u_catalogname,k.u_size,k.u_style,k.U_Length  from  " & vbCrLf _
            & " (Select  t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.lineTotal),0) as invTotal,sum(t1.quantity) as invqty,t1.itemcode,t1.dscription,t1.u_catalogname,t1.u_size,t1.u_style,ss.U_Length  from Odln T0 " & vbCrLf _
            & " inner join dln1 as t1 on t1.docentry=T0.docentry and t1.TreeType<>'I' " & vbCrLf _
            & " inner join [@INCM_SZE1] ss on ss.U_Name=T1.U_Size " & vbCrLf _
            & " group by t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType ,t1.itemcode,t1.dscription,t1.u_catalogname,t1.u_size,t1.u_style,ss.U_Length) k " & vbCrLf _
            & " group by k.baseentry,k.basetype, k.ObjType,k.itemcode,k.dscription,k.u_catalogname,k.u_size,k.u_style,k.U_Length)  td on  " & vbCrLf _
            & " td.basetype=T1.objtype and Td.baseentry=T1.docEntry  and td.itemcode=t1.itemcode and " & vbCrLf _
            & "  TD.u_catalogname = t1.u_catalogname And td.U_Length = ss.U_Length " & vbCrLf _
            & " Left Join " & vbCrLf _
            & " (select k.BaseEntry as baseentry,k.basetype,k.ObjType,SUM(k.invtotal) as deltotal,SUM(k.invqty) as delqty " & vbCrLf _
            & " ,k.itemcode,k.dscription,k.u_catalogname,k.u_size,k.u_style,k.u_length from " & vbCrLf _
            & " (Select  t1.U_BDocENtry as baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as invTotal,sum(t1.quantity) as invqty ,0 as rtnTotal,0 as rtnqty," & vbCrLf _
            & " t1.U_PriCode as itemcode,t1.dscription,t1.U_PriName as u_catalogname ,t1.u_size,t1.u_style,t1.u_catalogname AS orgname,t1.ItemCode AS orgcode,ss.U_Length from Odln T0 " & vbCrLf _
            & " inner join dln1 as t1 on t1.docentry=T0.docentry and t1.BaseType=17 and t1.TreeType<>'I' " & vbCrLf _
            & " inner join [@INCM_SZE1] ss on ss.U_Name=T1.U_Size " & vbCrLf _
            & " where t1.U_BDocENtry Is Not null " & vbCrLf _
            & " group by t1.U_BDocENtry,t1.BaseType,T0.DocEntry, T0.ObjType ,isnull(T0.DocTotal,0), " & vbCrLf _
            & "t1.U_PriCode,t1.dscription,t1.U_PriName,t1.u_size,t1.u_style,t1.u_catalogname,t1.ItemCode,ss.U_Length) k " & vbCrLf _
            & " group by k.baseentry,k.basetype, k.ObjType,k.itemcode,k.dscription,k.u_catalogname,k.u_size,k.u_style,k.orgcode,k.orgname,k.u_length) td2 on  td2.basetype=T1.objtype " & vbCrLf _
            & " And Td2.baseentry = T1.docEntry  and td2.itemcode=t1.itemcode and Td2.u_catalogname=t1.u_catalogname and Td2.u_length=ss.u_length " & vbCrLf _
            & " Left Join " & vbCrLf _
            & " (select k.BaseEntry as baseentry,k.basetype,k.ObjType,SUM(k.invtotal) as invtotal,SUM(k.invqty) as invqty ,SUM(k.rtntotal) " & vbCrLf _
            & " as rtntotal,SUM(k.rtnqty) as rtnqty,k.itemcode,k.dscription,k.u_catalogname,k.u_size,k.u_style,k.U_Length " & vbCrLf _
            & " from " & vbCrLf _
            & " (Select  t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as invTotal,isnull(sum(t1.quantity),0) as invqty ,isnull(sum(Tr.rtntotal),0) as rtnTotal,sum(tr.rtnqty) as rtnqty," & vbCrLf _
            & "t1.itemcode, t1.dscription, t1.u_catalogname, t1.u_size, t1.u_style, ss.U_Length   from Oinv T0 " & vbCrLf _
            & " inner join inv1 as t1 on t1.docentry=T0.docentry and t1.basetype=17 and t1.TreeType<>'I' " & vbCrLf _
            & " inner join [@INCM_SZE1] ss on ss.U_Name=T1.U_Size " & vbCrLf _
            & " left join (Select  t1.BaseRef, t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as rtnTotal,sum(t1.quantity) as rtnqty,t1.itemcode,t1.dscription,t1.u_catalogname,t1.u_size,t1.u_style from Orin T0 " & vbCrLf _
            & " inner join rin1 as t1 on t1.docentry=T0.docentry and t1.TreeType<>'I' " & vbCrLf _
            & "  group by t1.baseref,t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType " & vbCrLf _
            & " ,t1.itemcode,t1.dscription,t1.u_catalogname,t1.u_size,t1.u_style) tr on tr.Baseref=T1.DocEntry " & vbCrLf _
            & " where t1.U_BDocENtry Is null " & vbCrLf _
            & " group by t1.baseentry,t1.basetype,T0.DocEntry, T0.ObjType ,isnull(T0.DocTotal,0),isnull(Tr.rtntotal,0), " & vbCrLf _
            & " t1.itemcode, t1.dscription, t1.u_catalogname, t1.u_size, t1.u_style, ss.U_Length ) k " & vbCrLf _
            & " group by k.baseentry,k.basetype, k.ObjType,k.itemcode,k.dscription,k.u_catalogname,k.u_size,k.u_style,k.U_Length) T2 on " & vbCrLf _
            & "   t2.basetype = T1.objtype And T2.baseentry = T1.docEntry  and t2.itemcode=t1.itemcode and T2.u_catalogname=t1.u_catalogname and T2.u_length=ss.u_length " & vbCrLf _
            & " Left Join " & vbCrLf _
            & " (select k.BaseEntry as baseentry,k.basetype,k.ObjType,SUM(k.invtotal) as invtotal,SUM(k.invqty) as invqty ,SUM(k.rtntotal) as rtntotal,SUM(k.rtnqty) as rtnqty,k.itemcode,k.dscription,k.u_catalogname," & vbCrLf _
            & " k.u_size, k.u_style, k.orgname, k.orgcode, k.u_length from " & vbCrLf _
            & " (Select  t1.U_BDocENtry as baseentry,t1.basetype,T0.DocEntry, T0.ObjType,isnull(sum(T1.linetotal),0) as invTotal,isnull(sum(t1.quantity),0) as invqty ,0 as rtnTotal,0 as rtnqty," & vbCrLf _
            & " t1.U_PriCode as itemcode,t1.dscription,t1.U_PriName as u_catalogname ,t1.u_catalogname AS orgname,t1.ItemCode AS orgcode, t1.u_size, t1.u_style, ss.U_Length " & vbCrLf _
            & " from Oinv T0 " & vbCrLf _
            & " inner join inv1 as t1 on t1.docentry=T0.docentry and t1.BaseType=17 and t1.TreeType<>'I'  " & vbCrLf _
            & " inner join [@INCM_SZE1] ss on ss.U_Name=t1.U_Size " & vbCrLf _
            & "  where t1.U_BDocENtry Is Not null " & vbCrLf _
            & " group by t1.U_BDocENtry,t1.BaseType,T0.DocEntry, T0.ObjType ,isnull(T0.DocTotal,0), t1.U_PriCode,t1.dscription,t1.U_PriName,t1.u_size,t1.u_style,t1.u_catalogname,t1.ItemCode,ss.U_Length) k " & vbCrLf _
            & " group by k.baseentry,k.basetype, k.ObjType,k.itemcode,k.dscription,k.u_catalogname,k.u_size,k.u_style,k.orgcode,k.orgname,k.u_length) T3 on t3.basetype=T1.objtype and T3.baseentry=T1.docEntry " & vbCrLf _
            & " And t3.itemcode = t1.itemcode And RTrim(T3.u_catalogname) = RTrim(t1.u_catalogname) And T3.u_length = ss.u_length " & vbCrLf _
            & " left join [@incm_sbd1] sd on sd.LineId=T0.U_SubBrand " & vbCrLf _
            & " where (T0.DocDate>=@datefr or [%0] = '' )   and  (T0.DocDate<= @dateto or [%1] = '')  " & vbCrLf _
            & " and  (t1.u_catalogname=@tcatname  or [%2] = '') " & vbCrLf _
            & " and (T0.U_Brand = @brand or [%3] = '') " & vbCrLf _
            & " and (Tp.agent = @tagent or [%4] = '')" & vbCrLf _
            & " and (T0.docstatus = @docstat or [%5] = '')" & vbCrLf _
            & " and T0.Canceled='N' " & vbCrLf _
            & " group by T0.Docnum,T0.DocDate,T0.Cardcode,T0.Cardname,isnull(T0.doctotal,0),isnull(td.deltotal,0),isnull(td2.deltotal,0), " & vbCrLf _
            & " isnull(T2.invtotal,0),isnull(T3.invtotal,0),isnull(T2.RtnTotal,0)," & vbCrLf _
            & " isnull(td.delqty,0),isnull(td2.delqty,0), isnull(t2.invqty,0),isnull(T3.invqty,0), isnull(t2.rtnqty,0)," & vbCrLf _
            & " Tp.agent, T0.U_Brand, tp.City, tp.district, tp.state, t1.itemcode, t1.dscription ,t1.u_catalogname,t1.u_size,t1.u_style,sd.u_name,T3.orgcode,T3.orgname,ss.U_Length " & vbCrLf _
            & " order by T0.docnum "

    End Sub

    Private Sub mskdatefr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mskdatefr.KeyPress
        If Asc(e.KeyChar) = 13 Then
            mskdateto.Focus()
        End If
    End Sub

    Private Sub mskdatefr_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskdatefr.MaskInputRejected

    End Sub

    Private Sub mskdateto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mskdateto.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbparty.Focus()
        End If
    End Sub

    Private Sub mskdateto_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mskdateto.MaskInputRejected

    End Sub

    Private Sub cmbparty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbparty.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbagent.Focus()
        End If
    End Sub

    Private Sub cmbparty_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbparty.SelectedIndexChanged

    End Sub

    Private Sub cmbagent_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbagent.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbbrand.Focus()
        End If
    End Sub

    Private Sub cmbagent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbagent.SelectedIndexChanged

    End Sub

    Private Sub flxsummary_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxsummary.Enter

    End Sub
End Class
Public Class Frmprint
    Dim msql, msql2, msql3 As String
    Dim mdocnum As Int32

    Private Sub Frmprint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub
    Private Sub loaddata()
        msql = "select t0.docnum,t0.DocDate,t0.CardCode,t0.CardName,((t0.doctotal-(t0.vatsum+t0.rounddif))+t0.discsum) as amount,t0.discsum,t0.vatsum,t0.rounddif,t0.doctotal, t1.docentry,t1.itemcode,t1.u_catalogname,t1.u_style,t1.u_size,t1.u_noofpiece,t1.quantity,t1.volume as mtr,t1.Price,t1.linetotal,t1.u_mrp," & vbCrLf _
               & " c1.cardfname, t3.Building,t3.Block,t3.Street,t3.City, t3.state,t3.cstno,t3.tinno,c1.zipcode, " & vbCrLf _
               & " t0.U_Transport,t0.U_Destination,t0.u_arcode,t0.U_TaxCode,t0.u_esugam, " & vbCrLf _
               & " t0.U_Bundle,t0.U_OrderBy,t0.U_LRNo,t0.U_LrDat,t0.U_AreaCode,t0.U_DocThrough,t0.u_team from inv1 t1 " & vbCrLf _
               & " left join OINV t0 on t0.DocEntry=t1.DocEntry " & vbCrLf _
               & " left join (select c0.cardcode,convert(nvarchar(max),c0.Building) as building,c0.Block,c0.Street,c0.City, c0.state,c2.TaxId1 as cstno,c2.TaxId11 tinno, c0.address,c1.QryGroup1 as distributor,c1.QryGroup2 as franchise,c1.QryGroup3 as showroom from crd1 c0 " & vbCrLf _
               & " left join OCRD c1 on c1.CardCode=c0.cardcode " & vbCrLf _
               & "left join crd7 c2 on c2.CardCode=c0.CardCode and (len(rtrim(c2.Address))=0 or c2.Address is null )" & vbCrLf _
               & "where c0.adrestype='B' and upper(c0.Address)='OFFICE' and t0.docnum=" & Val(mdocnum) & vbCrLf _
               & "group by c1.cardfname,c1.zipcode,c0.CardCode,convert(nvarchar(max),c0.Building),c0.Block,c0.Street,c0.City, c0.state,c2.TaxId1,c2.TaxId11 , c0.address,c1.QryGroup1,c1.QryGroup2,c1.qrygroup3) t3 on t3.CardCode=t0.CardCode " & vbCrLf _
               & "order by t0.DocNum"


        msql2 = "select k.CardCode,k.CardName,k.DocNum,k.U_Transport,k.U_Dsnation,k.U_DocThrough,k.Docdate,k.U_Arcode,k.Address,k.Address2," & vbCrLf _
            & "k.item,k.u_style,SUM(k.qty36) as qtyr36,SUM(k.qty38) as qtyr38,SUM(k.qty40) as qtyr40,SUM(k.qty42) as qtyr42," & vbCrLf _
            & "SUM(k.qty44) as qtyr44,SUM(k.qty46) as qtyr46,SUM(k.qty48) as qtyr48,SUM(k.qty50) as qtyr50, " & vbCrLf _
            & " sum(k.qty36+k.qty38+k.qty40+k.qty42+k.qty44+k.qty46+k.qty48+k.qty50) as total,sum(k.qty36+k.qty38+k.qty40+k.qty42+k.qty44+k.qty46+k.qty48+k.qty50)/convert(int,k.box) as boxqty " & vbCrLf _
            & " from " & vbCrLf _
            & "(select t2.CardCode,t2.CardName,t2.DocNum,t2.U_Transport,t2.U_Dsnation,t2.U_DocThrough,t2.Docdate,t2.U_Arcode,t2.Address,t2.Address2," & vbCrLf _
            & "case when charindex('_', t1.u_catalogname)>0 then substring(t1.U_CatalogName,1,CHARINDEX('_',t1.U_CatalogName)-1) " & vbCrLf _
            & "else case when charindex('-', t1.u_catalogname)>0 then substring(t1.U_CatalogName,1,CHARINDEX('-',t1.U_CatalogName)-1) else t1.U_CatalogName end end as item, " & vbCrLf _
            & "t1.u_style ,case when t1.u_size in ('36','45','75') then sum(t1.quantity) else 0 end qty36, " & vbCrLf _
            & "case when t1.u_size in ('38','50','80') then sum(t1.quantity) else 0 end qty38," & vbCrLf _
            & "case when t1.u_size in ('40','55','85') then sum(t1.quantity) else 0 end qty40," & vbCrLf _
            & "case when t1.u_size in ('42','60','90') then sum(t1.quantity) else 0 end qty42," & vbCrLf _
            & "case when t1.u_size in ('44','65','95') then sum(t1.quantity) else 0 end qty44," & vbCrLf _
            & "case when t1.u_size in ('46','70','100') then sum(t1.quantity) else 0 end qty46," & vbCrLf _
            & " case when t1.u_size in ('48','105') then sum(t1.quantity) else 0 end qty48, " & vbCrLf _
            & "case when t1.u_size in ('110') then sum(t1.quantity) else 0 end qty50,tm.SalPackUn as box" & vbCrLf _
            & " from INV1 t1 " & vbCrLf _
            & "left join OINV t2 on t2.DocEntry=t1.DocEntry" & vbCrLf _
            & "left join OITM tm on tm.ItemCode=t1.ItemCode " & vbCrLf _
            & "where t2.docnum=" & Val(mdocnum) & vbCrLf _
            & "group by  t2.CardCode,t2.CardName,t2.DocNum,t2.U_Transport,t2.U_Dsnation,t2.U_DocThrough,t2.Docdate,t2.U_Arcode,t2.Address,t2.Address2," & vbCrLf _
            & "case when charindex('_', t1.u_catalogname)>0 then substring(t1.U_CatalogName,1,CHARINDEX('_',t1.U_CatalogName)-1) " & vbCrLf _
            & "else case when charindex('-', t1.u_catalogname)>0 then substring(t1.U_CatalogName,1,CHARINDEX('-',t1.U_CatalogName)-1) else t1.U_CatalogName end end," & vbCrLf _
            & "t1.u_style, t1.u_size, tm.SalPackUn ) k" & vbCrLf _
            & "group by k.CardCode,k.CardName,k.DocNum,k.U_Transport,k.U_Dsnation,k.U_DocThrough,k.Docdate,k.U_Arcode,k.Address,k.Address2,k.u_style, k.item, k.box"


    End Sub
    Private Sub rrpack()
        'If Val(TXTNO.text) <= 0 Then
        '    Call CMDCANCEL_Click()
        '    Exit Sub
        'End If

        'Open "c:\DEL.PRN" For Output As #1
        FileOpen(1, "test.txt", OpenMode.Output)
        'Print #1,
        Call DELHEAD()
        '27
        ' M = 28 - lin
        ' For j = 1 To M
        '  Print #1,
        '  lin = lin + 1
        ' Next


        For i = 1 To flx.Rows - 1
            Print #1, Tab(0); FLX.TextMatrix(i, 0);
            Print #1, Tab(10); FLX.TextMatrix(i, 2);
            If Len(Trim(flx.TextMatrix(i, 7))) > 0 Then
             Print #1, Tab(21); FLX.TextMatrix(i, 7);
            Else
                Print #1, Tab(21); FLX.TextMatrix(i, 1);
            End If
            Print #1, Tab(56 - Len(Format(FLX.TextMatrix(i, 3), "####0"))); Format(FLX.TextMatrix(i, 3), "####0");
            Print #1, Tab(68 - Len(Format(FLX.TextMatrix(i, 4), "####0.00"))); Format(FLX.TextMatrix(i, 4), "####0.00");
            Print #1, Tab(76 - Len(Format(FLX.TextMatrix(i, 5), "###0.00"))); Format(FLX.TextMatrix(i, 5), "###0.00")
            'Print #1, Tab(80 - Len(Format(FLX.TextMatrix(i, 6), "######0.00"))); Format(FLX.TextMatrix(i, 6), "######0.00")
            MQTY = MQTY + Val(flx.TextMatrix(i, 3))
            MMTR = MMTR + Val(flx.TextMatrix(i, 4))
            'mamt = mamt + Val(FLX.TextMatrix(i, 6))
            LIN = LIN + 1
            If LIN > 59 Then
                '61
                '62
                M = 62 - LIN
                For j = 1 To M
                    Print #1,
                    LIN = LIN + 1
                Next j

            Print #1, Tab(36); "C/o";
            Print #1, Tab(56 - Len(Format(MQTY, "####0"))); Format(MQTY, "####0");
            Print #1, Tab(68 - Len(Format(MMTR, "####0.00"))); Format(MMTR, "####0.00")
                LIN = LIN + 1
                M = 73 - LIN
                For j = 1 To M
                    Print #1,
                Next j
                PAG = PAG + 1
                Call DELHEAD()
            End If
        Next
        '61
        '62
        M = 62 - LIN
        For j = 1 To M
       Print #1,
            LIN = LIN + 1
        Next j

      Print #1, Tab(56 - Len(Format(MQTY, "####0"))); Format(MQTY, "####0");
      Print #1, Tab(68 - Len(Format(MMTR, "####0.00"))); Format(MMTR, "####0.00")
        'Print #1, Tab(80 - Len(Format(mamt, "######0.00"))); Format(mamt, "######0.00")
        LIN = LIN + 1
        'Print #1,
        M = 73 - LIN
        For j = 1 To M
        Print #1,
        Next j
        'Print #1,
        'Print #1, Chr(12)
       Close #1
        LIN = 1
        PAG = 1
        MQTY = 0
        MMTR = 0.0#
        'Shell "command.com /c TYPE " & "c:\DEL.prn>PRN", vbMaximizedFocus

        'Shell "command.com /c edit " & "c:\DEL.prn", vbMaximizedFocus
        'Print #1, Tab(70 - Len(Format(mamt, "######0.00"))); Format(mamt, "######0.00")
    End Sub

    Private Sub lorycopy()
        'Open "c:\invlor.PRN" For Output As #1
Open "lpt1" For Output As #1
        Call alainvheadlor()
        If InStr(CMBBRAND.text, "RAMYAM") > 0 Then
            'Print #1, Tab(2); IIf(Len(RTrim(loadbrandled(CMBBRAND.text))) > 0 And RTrim(loadbrandled(CMBBRAND.text)) <> "NULL", RTrim(loadbrandled(CMBBRAND.text)) + " - ", ""); Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "_") - 1); " "; IT!Group;
            'Printer.Print 'If(Len(RTrim(loadbrandled(CMBBRAND.text))) > 0 And RTrim(loadbrandled(CMBBRAND.text)) <> "NULL", RTrim(loadbrandled(CMBBRAND.text)) + " - ", "");
    Print #1, Tab(2); "RAMYAM"
            LIN = LIN + 1
        End If

        For i = 1 To flx.Rows - 1
            'If InStr(CMBBRAND.text, "RAMYAM") > 0 Then
            ' Print #1, Tab(2); "RAMYAM"
            ' LIN = LIN + 1
            'End If
            If Len(Trim(flx.TextMatrix(i, 7))) > 0 Then
     Print #1, Tab(1); FLX.TextMatrix(i, 7);
            Else
                If CHKSHIRT.value = 1 Then
                    'Print #1, Tab(1); "RAMRAJ - "; Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "-") - 1);
                    If CHKSHOW.value = 1 Then
                        If IT.State = 1 Then IT.Close()
                        IT.Open("SELECT * FROM QITMAINNEW WHERE CODE='" & flx.TextMatrix(i, 0) & "'", con, adOpenForwardOnly, adLockOptimistic)
                        If IT.RecordCount > 0 Then
                            If IsNull(IT!RCODE) = False Then
                                If Len(Trim(IT!RCODE)) > 0 Then
          Print #1, Tab(1); IT!RCODE; "- "; Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "-") - 1);
                                    'Print #1, Tab(1); IT!RCODE; " "; Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "_") - 1); " "; IT!Group;
                                Else

                                    'Print #1, Tab(1); IIf(InStr(CMBBRAND.text, "RAMRAJ") > 0 Or Len(Trim(CMBBRAND.text)) = 0, "RAMRAJ - ", "RAMYAM - "); Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "-") - 1);
           Print #1, Tab(1); IIf(Len(RTrim(loadbrandled(CMBBRAND.text))) > 0 And RTrim(loadbrandled(CMBBRAND.text)) <> "NULL", RTrim(loadbrandled(CMBBRAND.text)) + " - ", ""); Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "-") - 1);

                                    ''Print #1, Tab(1); "RAMRAJ - "; Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "-") - 1);
                                End If
                            Else
                                'IIf(InStr(cmbbrand.text, "RAMRAJ") > 0, "RAMRAJ - ", "RAMYAM - ");
                                'Print #1, Tab(1); IIf(InStr(CMBBRAND.text, "RAMRAJ") > 0 Or Len(Trim(CMBBRAND.text)) = 0, "RAMRAJ - ", "RAMYAM - "); Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "-") - 1);
           Print #1, Tab(1); IIf(Len(RTrim(loadbrandled(CMBBRAND.text))) > 0 And RTrim(loadbrandled(CMBBRAND.text)) <> "NULL", RTrim(loadbrandled(CMBBRAND.text)) + " - ", ""); Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "-") - 1);
                                ''Print #1, Tab(1); "RAMRAJ - "; Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "-") - 1);
                            End If
                        End If
                        If IT.State = 1 Then IT.Close()
                    Else

                        'Print #1, Tab(1); IIf(InStr(CMBBRAND.text, "RAMRAJ") > 0 Or Len(Trim(CMBBRAND.text)) = 0, "RAMRAJ - ", "RAMYAM - "); Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "-") - 1);
       Print #1, Tab(1); IIf(Len(RTrim(loadbrandled(CMBBRAND.text))) > 0 And RTrim(loadbrandled(CMBBRAND.text)) <> "NULL", RTrim(loadbrandled(CMBBRAND.text)) + " - ", ""); Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "-") - 1);
                        ''Print #1, Tab(1); "RAMRAJ - "; Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "-") - 1);
                    End If

                    'FLX.TextMatrix(i, 1);
                Else

                    If IT.State = 1 Then IT.Close()
                    IT.Open("SELECT * FROM QITMAINNEW WHERE CODE='" & flx.TextMatrix(i, 0) & "'", con, adOpenForwardOnly, adLockOptimistic)
                    If IT.RecordCount > 0 Then
                        If CHKSHOW.value = 1 Then
                            If IsNull(IT!RCODE) = False Then
           Print #1, Tab(1); IT!RCODE; " "; Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "_") - 1); " "; IT!Group;
                            Else
                                'IIf(InStr(cmbbrand.text, "RAMRAJ") > 0, "RAMRAJ - ", "RAMYAM - ");
                                'Print #1, Tab(1); IIf(InStr(CMBBRAND.text, "RAMRAJ") > 0 Or Len(Trim(CMBBRAND.text)) = 0, "RAMRAJ - ", "RAMYAM - "); " "; Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "_") - 1); " "; IT!Group;
           Print #1, Tab(1); IIf(Len(RTrim(loadbrandled(CMBBRAND.text))) > 0 And RTrim(loadbrandled(CMBBRAND.text)) <> "NULL", RTrim(loadbrandled(CMBBRAND.text)) + " - ", ""); Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "_") - 1); " "; IT!Group;
                                ''Print #1, Tab(1); "RAMRAJ"; " "; Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "_") - 1); " "; IT!Group;
                            End If
                        Else
                            'IIf(InStr(CMBBRAND.text, "RAMRAJ") > 0, "RAMRAJ - ", "RAMYAM - ");

          Print #1, Tab(1); Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "_") - 1); " "; IT!Group;
                        End If
                    End If
                    IT.Close()
                    'Else
                    ' Print #1, Tab(1); Mid(FLX.TextMatrix(i, 1), 1, InStr(FLX.TextMatrix(i, 1), "_") - 1);
                    'End If
                End If
            End If
            'OLD
            'Print #1, Tab(32); Mid(FLX.TextMatrix(i, 2), InStr(FLX.TextMatrix(i, 2), "X") + 1, 6);
            'Print #1, Tab(45); Mid(FLX.TextMatrix(i, 2), 1, InStr(FLX.TextMatrix(i, 2), "X") - 1);
            'NEW
   Print #1, Tab(31); Mid(FLX.TextMatrix(i, 2), InStr(FLX.TextMatrix(i, 2), "X") + 1, 6);
   Print #1, Tab(39); Mid(FLX.TextMatrix(i, 2), 1, InStr(FLX.TextMatrix(i, 2), "X") - 1);

            'Print #1, Tab(62 - Len(Format(FLX.TextMatrix(i, 5), "###0.00"))); Format(FLX.TextMatrix(i, 5), "###0.00");
            If IT.State = 1 Then IT.Close()
            IT.Open("SELECT * FROM ITMAIN WHERE CODE='" & flx.TextMatrix(i, 0) & "'", con, adOpenForwardOnly, adLockOptimistic)
            If IT.RecordCount > 0 Then
                If IsNull(IT!BOX) = False Then
                    If Val(IT!BOX) > 0 Then
                        MBOX = Fix(Val(flx.TextMatrix(i, 3)) / IT!BOX)
                    Else
                        MBOX = 0
                    End If
                End If
                'MTBOX = Val(FLX.TextMatrix(i, 5)) * IT!BOX
                'Print #1, Tab(56 - Len(Format(MTBOX, "###0"))); Format(MTBOX, "###0");
                'OLD
                'Print #1, Tab(63 - Len(Format(FLX.TextMatrix(i, 5), "###0.00"))); Format(FLX.TextMatrix(i, 5), "###0.00");
                'Print #1, Tab(66 - Len(Format(MBOX, "##0"))); Format(MBOX, "##0");
                'NEW
     Print #1, Tab(54 - Len(Format(FLX.TextMatrix(i, 5), "###0.00"))); Format(FLX.TextMatrix(i, 5), "###0.00");
     Print #1, Tab(57 - Len(Format(MBOX, "##0"))); Format(MBOX, "##0");
                MMTR = MMTR + MBOX
            End If
            IT.Close()
            'OLD
            'Print #1, Tab(72 - Len(Format(FLX.TextMatrix(i, 3), "####0"))); Format(FLX.TextMatrix(i, 3), "####0");
            ''Print #1, Tab(60 - Len(Format(FLX.TextMatrix(i, 4), "####0.00"))); Format(FLX.TextMatrix(i, 4), "####0.00");
            ''Print #1, Tab(69 - Len(Format(FLX.TextMatrix(i, 5), "###0.00"))); Format(FLX.TextMatrix(i, 5), "###0.00");
            'Print #1, Tab(85 - Len(Format(FLX.TextMatrix(i, 6), "######0.00"))); Format(FLX.TextMatrix(i, 6), "######0.00")

            'NEW
    Print #1, Tab(64 - Len(Format(FLX.TextMatrix(i, 3), "####0"))); Format(FLX.TextMatrix(i, 3), "####0");
    Print #1, Tab(77 - Len(Format(FLX.TextMatrix(i, 6), "######0.00"))); Format(FLX.TextMatrix(i, 6), "######0.00")



            MQTY = MQTY + Val(flx.TextMatrix(i, 3))
            'MMTR = MMTR + Val(FLX.TextMatrix(i, 4))
            MAMT = MAMT + Val(flx.TextMatrix(i, 6))

            LIN = LIN + 1
            If LIN > MN Then
                M = 57 - LIN
                For j = 1 To M
       Print #1, ""
                    LIN = LIN + 1
                Next j
      Print #1, Tab(36); "C/o";
                '66
      Print #1, Tab(57 - Len(Format(MMTR, "##0"))); Format(MMTR, "##0");
                '72
      Print #1, Tab(64 - Len(Format(MQTY, "####0"))); Format(MQTY, "####0");
                '85
      Print #1, Tab(77 - Len(Format(MAMT, "######0.00"))); Format(MAMT, "######0.00")
                LIN = LIN + 1
      Print #1,
                LIN = LIN + 1
                PAG = PAG + 1
      Print #1, Tab(54); "Contined....."; Format(PAG, "###")
                LIN = LIN + 1
                'Print #1,
                'LIN = LIN + 1
                'MN = 0
                ' Print #1, Chr(12)
                M = 73 - LIN
                For j = 1 To M
        Print #1,
                    LIN = LIN + 1
                Next j
                LIN = 1
                Call alainvheadlor()
            End If
        Next
        M = MN - LIN
        For j = 1 To M
       Print #1,
            LIN = LIN + 1
        Next j


        If Val(LBLSCHEME.Caption) > 0 Then
            '73
      Print #1, Tab(66); dlin("-", 12)
            LIN = LIN + 1
      Print #1, Tab(77 - Len(Format(MAMT, "######0.00"))); Format(MAMT, "######0.00")
            LIN = LIN + 1
      Print #1, Tab(47); "Less Season Discount ";
      Print #1, Tab(77 - Len(Format(LBLSCHEME.Caption, "######0.00"))); Format(LBLSCHEME.Caption, "######0.00")
            LIN = LIN + 1
            MAMT = MAMT - Val(LBLSCHEME.Caption)
        End If

        If Val(txtidiscamt.text) > 0 Then
            '73
            If Val(LBLSCHEME.Caption) <= 0 Then
       Print #1, Tab(66); dlin("-", 12)
                LIN = LIN + 1
       Print #1, Tab(77 - Len(Format(MAMT, "######0.00"))); Format(MAMT, "######0.00")
                LIN = LIN + 1
            End If
      Print #1, Tab(42); "Less Introduction Offer "; Trim(txtidisc.text); "%";
      Print #1, Tab(77 - Len(Format(txtidiscamt.text, "######0.00"))); Format(txtidiscamt.text, "######0.00")
            LIN = LIN + 1
            MAMT = MAMT - Val(txtidiscamt.text)
        End If

        If Val(txtdiscamt) > 0 Then

      Print #1, Tab(66); dlin("-", 12)
            LIN = LIN + 1

            K = 7
            If Val(TXTLRAGST.text) > 0 Then
                '7
        Print #1, Tab(K); " %"; Tab(K + 3); "Trade Disc";
                K = K + 14
            End If
            If Val(TXT15DAYS.text) > 0 Then
                '21
        Print #1, Tab(K); " %"; Tab(K + 3); "Turnover";
                K = K + 12
            End If
            If Val(TXT30DAYS.text) > 0 Then
                '33
        Print #1, Tab(K); " %"; Tab(K + 3); IIf(Len(Trim(CMBDISC.text)) > 0, Trim(CMBDISC.text), "Special");
            End If
      Print #1, Tab(77 - Len(Format(MAMT, "######0.00"))); Format(MAMT, "######0.00")
            LIN = LIN + 1
            K = 7
            If Val(TXTLRAGST.text) > 0 Then
                '7,10
        Print #1, Tab(K); Trim(TXTLRAGST.text); "-"; Tab(K + 3); Format((MAMT * Val(TXTLRAGST)) / 100, "#####0.00");
                '"+";
                If Val(TXT15DAYS.text) > 0 Or Val(TXT30DAYS) > 0 Then
         Print #1, "+";
                End If
                K = K + 14
            End If
            If Val(TXT15DAYS.text) > 0 Then
                '21,24
        Print #1, Tab(K); Trim(TXT15DAYS.text); "-"; Tab(K + 3); Format((MAMT * Val(TXT15DAYS)) / 100, "#####0.00");
                If Val(TXT30DAYS.text) > 0 Then
         Print #1, "+";
                End If
                K = K + 12
            End If
            If Val(TXT30DAYS.text) > 0 Then
                '33,36
        Print #1, Tab(K); Trim(TXT30DAYS.text); "-"; Tab(K + 3); Format((MAMT * Val(TXT30DAYS)) / 100, "#####0.00"); " =";
            End If

      Print #1, Tab(37); "Less Discount "; Format(Val(txtdisc), "#0.00"); "%";
      Print #1, Tab(77 - Len(Format(txtdiscamt, "######0.00"))); Format(txtdiscamt, "######0.00")
            LIN = LIN + 1

            MAMT = MAMT - Val(txtdiscamt)
            If Val(TXTTAXAMT) <= 0 Then
        Print #1, Tab(66); dlin("-", 12)
                LIN = LIN + 1
        Print #1, Tab(77 - Len(Format(MAMT, "######0.00"))); Format(MAMT, "######0.00")
                LIN = LIN + 1
            End If
        End If
        'new forward
        If Val(txtforward) > 0 Then
       Print #1, Tab(37); "Add: Forwarding Charges";
       Print #1, Tab(77 - Len(Format(txtforward, "######0.00"))); Format(txtforward, "######0.00")
            LIN = LIN + 1
            MAMT = MAMT + Val(txtforward)
        End If

        If Val(TXTVDISCAMT) > 0 Then
            If Val(txtdiscamt.text) <= 0 Then
       Print #1, Tab(66); dlin("-", 12)
                LIN = LIN + 1
       Print #1, Tab(77 - Len(Format(MAMT, "######0.00"))); Format(MAMT, "######0.00")
                LIN = LIN + 1
            End If
            'Print #1, Tab(53); "Less Vat Exemption ";
      Print #1, Tab(47); "Less Discount ";
      Print #1, Tab(77 - Len(Format(TXTVDISCAMT, "######0.00"))); Format(TXTVDISCAMT, "######0.00")
            LIN = LIN + 1

            MAMT = MAMT - Val(TXTVDISCAMT)
        End If

        If Val(TXTTAXAMT) > 0 Then
      Print #1, Tab(66); dlin("-", 12)
            LIN = LIN + 1
      Print #1, Tab(77 - Len(Format(MAMT, "######0.00"))); Format(MAMT, "######0.00")
            LIN = LIN + 1
            If CHKCFORM.value = 1 Then
         Print #1, Tab(37); "Add: Against C'FORM "; Trim(cmbtaxtyp); " "; Val(TXTTAX); "%";
            Else
         Print #1, Tab(37); "Add: "; Trim(cmbtaxtyp); " "; Val(TXTTAX); "%";
            End If

      Print #1, Tab(77 - Len(Format(TXTTAXAMT, "######0.00"))); Format(TXTTAXAMT, "######0.00")
            LIN = LIN + 1
            'MAMT = MAMT + Val(TXTTAXAMT)
            If Trim(cmbtaxtyp.text) <> "CST" Then
                If Val(TXTSCHARGE) > 0 Then
         Print #1, Tab(37); "Surcharge "; Val(TXTTAXSUR); "%";
         Print #1, Tab(77 - Len(Format(TXTSCHARGE, "######0.00"))); Format(TXTSCHARGE, "######0.00")
                    LIN = LIN + 1
                End If
            End If
            MAMT = MAMT + Val(TXTTAXAMT) + Val(TXTSCHARGE)

        End If

        '**old forward
        '      If Val(txtforward) > 0 Then
        '       Print #1, Tab(37); "Forwarding Charges";
        '       Print #1, Tab(77 - Len(Format(txtforward, "######0.00"))); Format(txtforward, "######0.00")
        '       LIN = LIN + 1
        '       MAMT = MAMT + Val(txtforward)
        '      End If
        'If Val(txtround) < 0 Or Val(txtround) > 0 Then
        If Val(txtround) <> 0 Then
       Print #1, Tab(37); "Rounded Off";
       Print #1, Tab(77 - Len(Format(txtround, "######0.00"))); Format(txtround, "######0.00")
            LIN = LIN + 1
            MAMT = MAMT + Val(txtround)
        End If
        If Val(txtround) <> 0 And Val(txtdiscamt) = 0 And Val(TXTTAXAMT) = 0 And Val(txtforward) = 0 Then
       Print #1,
            LIN = LIN + 1
       Print #1,
            LIN = LIN + 1
        End If

        M = 56 - LIN
        For j = 1 To M
       Print #1,
            LIN = LIN + 1
        Next j
      Print #1, Tab(56); dlin("-", 22)
        LIN = LIN + 1
        If CHKHFORM.value = 1 Then
       Print #1, Tab(10); "Tax Exempted Against FORM H";
        End If
      Print #1, Tab(57 - Len(Format(MMTR, "##0"))); Format(MMTR, "##0");
      Print #1, Tab(64 - Len(Format(MQTY, "####0"))); Format(MQTY, "####0");
      Print #1, Tab(77 - Len(Format(MAMT, "######0.00"))); Format(MAMT, "######0.00")
        LIN = LIN + 1
      Print #1, Tab(56); dlin("-", 22)
        LIN = LIN + 1
        If Len(Trim(txtesugamno.text)) > 0 Then
        Print #1, Tab(28); Chr(14); Chr(15); "* e-Sugam No : "; Trim(txtesugamno.text); Chr(18)
            LIN = LIN + 1
        Else
        Print #1,
            LIN = LIN + 1
        End If
       Print #1,
        LIN = LIN + 1

      Print #1,
        LIN = LIN + 1
      Print #1,
        LIN = LIN + 1

        If Len(word(Str(MAMT))) > 78 Then
       Print #1, Tab(1); "Rupees:"; Tab(9); Chr(15); word(str(MAMT)); Chr(18)
            LIN = LIN + 1
        Else
       Print #1, Tab(1); "Rupees:"; Tab(9); word(str(MAMT))
            LIN = LIN + 1
        End If
        'Print #1, Chr(12)
        M = 67 - LIN
        For j = 1 To M
         Print #1,
            LIN = LIN + 1
        Next j

        '        If Val(TXTLRAGST.Text) > 0 Then
        '        Else
        '         Print #1, Tab(43); "CD 5% "; Format((MAMT * 5 / 100), "#######0.00")
        '         LIN = LIN + 1
        '         Print #1, Tab(43); Chr(27); Chr(69); "Due Date:"; Format(MSKDUEDATE.Text, "DD-MM-YYYY")
        '         LIN = LIN + 1
        '        End If

        '
        '       Print #1, Tab(53); Mid(KUSER, 1, 15)
        'LIN = LIN + 1
        M = 73 - LIN
        For j = 1 To M
        Print #1,
        Next j

       Close #1
        LIN = 1
        PAG = 1
        MN = 0
        MQTY = 0
        MMTR = 0
        MAMT = 0
        MBOX = 0
        MTBOX = 0
        If CS.State = 1 Then CS.Close()
        'CON.Execute "update inv set prnupdt=1 where invno=" & Val(TXTNO.text) & " and [DATE]='" & CStr(Format(MSKDATE.text, "YYYY-MM-DD")) & "'"
        'Shell "command.com /c EDIT " & "c:\inv.prn", vbMaximizedFocus
        'Shell "command.com /c TYPE " & "c:\invlor.prn>PRN", vbHide

    End Sub
    Private Sub alainvheadlor()
        LIN = 1
        '55
        MN = 50
        'MN = 54
        MN = MN - 2
        If Val(LBLSCHEME.Caption) > 0 Then
            MN = MN - 1
        End If
        If Val(txtidiscamt) > 0 Then
            MN = MN - 1
        End If

        If Val(txtdiscamt) > 0 Then
            MN = MN - 1
        End If
        If Val(TXTTAXAMT) > 0 Then
            MN = MN - 1
        End If

        If Val(TXTSCHARGE) > 0 Then
            MN = MN - 1
        End If

        If Val(txtforward) > 0 Then
            MN = MN - 1
        End If

        If Val(txtround) <> 0 Then
            MN = MN - 1
        End If
        ' If Val(txtdiscamt) > 0 And Val(TXTTAXAMT) > 0 Then
        '  MN = MN - 6
        ' End If
        '
        ' If Val(txtdiscamt) > 0 And Val(TXTTAXAMT) = 0 Then
        '  MN = MN - 5
        ' End If
        '
        ' If Val(TXTTAXAMT) > 0 And Val(txtdiscamt) = 0 Then
        '  MN = MN - 3
        ' End If
        '
        ' If Val(TXTTAXSUR) > 0 Then
        '  MN = MN - 1
        ' End If
        '
        ' If Val(txtforward) > 0 Then
        '  MN = MN - 1
        ' End If
        '
        ' If Val(txtround) > 0 Or Val(txtround) < 0 Then
        '  MN = MN - 1
        ' End If
        '  If PAG > 1 Then
        '  'REVERSE FEEDING
        '  Print #1, Chr(27); Chr(106); "9"
        '  Print #1, Chr(27); Chr(106); "9"
        ' End If
        '11
Print #1, Tab(5); Chr(14); Chr(15); Tab(30); "Transport Copy"; Chr(18)
        LIN = LIN + 1

        For j = 1 To 10
   Print #1,
            LIN = LIN + 1
        Next j
        '  Print #1,
        '  'Print #1, Tab(43); Chr(27); Chr(69); "TIN No: "; mtinno; Chr(27); Chr(70); Chr(18)
        '  LIN = LIN + 1
        '  Print #1,
        '  LIN = LIN + 1
        '  Print #1,
        '  LIN = LIN + 1
        If CS.State = 1 Then CS.Close()
        CS.Open("SELECT * FROM COMPANYMAST WHERE ID=" & MCODE, con, adOpenForwardOnly, adLockOptimistic)
        If CS.RecordCount > 0 Then
            'Print #1, Tab(60); "Tngst :"; Tab(67); CS!TNGST
            'Print #1,
            'LIN = LIN + 1
            'Print #1, Tab(60); "Cst No:"; Tab(67); Mid(CS!CST, 1, 11)
            'Print #1,
            'LIN = LIN + 1
        End If
        If CS.State = 1 Then CS.Close()
        '11
        ' M = 12 - LIN
        ' For J = 1 To M
        '  Print #1,
        '  LIN = LIN + 1
        ' Next J


        '  For J = 1 To 11
        '   Print #1,
        '   LIN = LIN + 1
        '  Next J
        'LIN = 11

        If ps.State = 1 Then ps.Close()
        ps.Open("SELECT * FROM LEDQRY WHERE [NAME]='" & TXTPARTY.text & "'", con, adOpenForwardOnly, adLockOptimistic)
        If ps.RecordCount > 0 Then
            '6
            '3
   Print #1, Tab(5); Chr(14); Chr(15); ps!mname; Chr(18)
            LIN = LIN + 1
            '10
            '4
            'If CHKSHIRT.value = 1 Then
            ' Print #1, Tab(5); ps!add1; Tab(50); Chr(27); "W1"; Chr(27); "G"; "SS"; TXTNO.text; Chr(27); "H"; Chr(27); "W0"
            'Else
            ' Print #1, Tab(5); ps!add1; Tab(50); Chr(27); "W1"; Chr(27); "G"; "RR"; TXTNO.text; Chr(27); "H"; Chr(27); "W0"
            'End If
            If CHKSHIRT.value = 1 Then
    Print #1, Tab(5); ps!add1; Tab(50); Chr(14); Chr(15); Trim(TXTSTYPE.text); TXTNO.text; Chr(18)
            Else
    Print #1, Tab(5); ps!add1; Tab(50); Chr(14); Chr(15); Trim(TXTSTYPE.text); TXTNO.text; Chr(18)
                'If InStr(CMBBRAND.text, "RAMRAJ") > 0 Or Len(Trim(CMBBRAND.text)) = 0 Then
                ' Print #1, Tab(5); ps!add1; Tab(50); Chr(14); Chr(15); Trim(TXTSTYPE.text); TXTNO.text; Chr(18)
                'Else
                ' Print #1, Tab(5); ps!add1; Tab(50); Chr(14); Chr(15); "RL"; TXTNO.text; Chr(18)
                'End If
            End If


            LIN = LIN + 1
            'Print #1, Tab(10); Chr(14); Chr(15); PS!Add2; Tab(64); Chr(27); Chr(69); TXTNO.Text; Chr(27); Chr(70); Chr(18)
            'If CHKSHIRT.value = 1 Then
            If CHKHFORM.value = 1 Then
    Print #1, Tab(5); ps!add2&; vbNullString; Tab(49); Chr(27); Chr(69); "Form H"; Chr(27); Chr(70); Chr(18)
            ElseIf CHKTAX.value = 1 Then
    Print #1, Tab(5); ps!add2&; vbNullString; Tab(49); Chr(27); Chr(69); "Taxable"; Chr(27); Chr(70); Chr(18)
            ElseIf CHKNONTAX.value = 1 Then
    Print #1, Tab(5); ps!add2 & vbNullString; Tab(49); Chr(27); Chr(69); "Non Taxable"; Chr(27); Chr(70); Chr(18)
            End If
            'Print #1, Tab(4); PS!Add2; Tab(52); Chr(27); "W1"; Chr(27); "G"; "SS "; TXTNO.Text; Chr(27); "H"; Chr(27); "W0"
            'Else
            'Print #1, Tab(4); PS!Add2; Tab(52); Chr(27); "W1"; Chr(27); "G"; "RR "; TXTNO.Text; Chr(27); "H"; Chr(27); "W0"
            'End If
            LIN = LIN + 1
            'If CHKTAX.value = 1 Then
    Print #1, Tab(5); ps!add3
            '& vbNullString; Tab(49); Chr(27); Chr(69); "Taxable"; Chr(27); Chr(70); Chr(18)
            'ElseIf CHKNONTAX.value = 1 Then
            ' Print #1, Tab(5); ps!add3 & vbNullString; Tab(49); Chr(27); Chr(69); "Non Taxable"; Chr(27); Chr(70); Chr(18)
            'End If

            'Print #1, Tab(13); Chr(14); Chr(15); ps!Add3; Tab(61); Chr(27); Chr(69); TXTNO.Text; Chr(27); Chr(70)
            LIN = LIN + 1
   Print #1, Tab(5); ps![tax] & vbNullString; Tab(49); Chr(27); Chr(69); Format(MSKDATE.text, "dd-mm-yyyy"); Chr(27); Chr(70); Chr(18)
            LIN = LIN + 1
   Print #1, Tab(5); Trim(ps![TNGST]) & vbNullString; Tab(53); TXTAREA.text; Chr(27); Chr(18)
            '; Tab(61); Chr(27); Chr(69); Format(MSKDATE.Text, "dd-mm-yyyy"); Chr(27); Chr(70); Chr(18)
            LIN = LIN + 1
        Else
   Print #1, Tab(5); Chr(14); Chr(15); TXTPARTY.text; Chr(18)
            LIN = LIN + 1
   Print #1,
            LIN = LIN + 1
            'Print #1, Tab(51); Chr(27); "W1"; Chr(27); "G"; TXTNO.text; Chr(27); "H"; Chr(27); "W0"
   Print #1, Tab(51); Chr(14); Chr(15); TXTNO.text; Chr(18)
            'Print #1, Tab(64); Chr(27); Chr(69); TXTNO.Text; Chr(27); Chr(70); Chr(18)
            LIN = LIN + 1
            'Print #1, " "
   Print #1,
            'Tab(61); Chr(27); Chr(69); TXTNO.Text; Chr(27); Chr(70); Chr(18)
            LIN = LIN + 1
            '48
   Print #1, Tab(49); Chr(27); Chr(69); Format(MSKDATE.text, "dd-mm-yyyy"); Chr(27); Chr(70); Chr(18)
            LIN = LIN + 1
   Print #1, Tab(53); TXTAREA.text; Chr(27); Chr(18)
            'Tab(13); Tab(61); Chr(27); Chr(69); Format(MSKDATE.Text, "dd-mm-yyyy"); Chr(27); Chr(70); Chr(18)
            LIN = LIN + 1

        End If
        If ps.State = 1 Then ps.Close()
        '18
        M = 15 - LIN
        For j = 1 To M
   Print #1,
            LIN = LIN + 1
        Next

  Print #1,
        LIN = LIN + 1
        '7
  Print #1, Tab(9); txtordno.text;
        '29
  Print #1, Tab(30); TXTTO.text;
        '63
        '64
  Print #1, Tab(54); TXTWBNO.text;
        '82
  Print #1, Tab(72); txtwgt.text
        LIN = LIN + 1
  Print #1, Tab(9); TXTBY.text;
  Print #1, Tab(30); TXTCARR.text;
  Print #1, Tab(54); Format(MSKWBDATE.text, "DD-MM-YYYY");
        '82
  Print #1, Tab(72); txtpay.text
        LIN = LIN + 1
  Print #1,
        LIN = LIN + 1

  Print #1, Tab(15); TXTDOC.text; Tab(53); "Case No :"; Trim(TXTNO.text); "/"; Trim(TXTBUNDLE.text); Tab(75); Trim(TXTBRAND.text)
        LIN = LIN + 1

        M = 27 - LIN
        For j = 1 To M
  Print #1,
            LIN = LIN + 1
        Next j
        If PAG > 1 Then
     Print #1, Tab(36); "B/f";
      Print #1, Tab(54 - Len(Format(MMTR, "###0.00"))); Format(MMTR, "###0");
      Print #1, Tab(64 - Len(Format(MQTY, "####0"))); Format(MQTY, "####0");
      Print #1, Tab(77 - Len(Format(MAMT, "######0.00"))); Format(MAMT, "######0.00")
            LIN = LIN + 1
            If InStr(CMBBRAND.text, "RAMYAM") > 0 Then
        Print #1, Tab(2); "RAMYAM"
                LIN = LIN + 1
            End If
      Print #1,
            LIN = LIN + 1
        Else
            MQTY = 0
            MMTR = 0
            MAMT = 0
            MBOX = 0
            MTBOX = 0
            PAG = 1
        End If

    End Sub
    Private Sub testprint()

        msql3 = "select t0.compnyname,t1.building,t1.block,t1.streetno,t1.street,t1.city,t1.zipcode,t1.state,TaxIdNum6 as tinno,TaxIdNum5 as cstno  from oadm t0" & vbCrLf _
              & "left join adm1 t1 on t1.Code=t0.Code"

        msql = "select convert(nvarchar(max),f.U_Remarks) as stype,t0.docnum,t0.DocDate,t0.CardCode,t0.CardName,((t0.doctotal-(t0.vatsum+t0.rounddif))+t0.discsum) as amount,t0.discsum,t0.vatsum,t0.rounddif,t0.doctotal, t1.docentry,t1.itemcode,t1.u_catalogname,t1.u_style,t1.u_size,t1.u_noofpiece,t1.quantity,t1.volume as mtr,t1.Price,t1.linetotal,t1.u_mrp," & vbCrLf _
               & " c1.cardfname, t3.Building,t3.Block,t3.Street,t3.City, t3.state,t3.cstno,t3.tinno,c1.zipcode, " & vbCrLf _
               & " t0.U_Transport,t0.U_Destination,t0.u_arcode,t0.U_TaxCode,t0.u_esugam, " & vbCrLf _
               & " t0.U_Bundle,t0.U_OrderBy,t0.U_LRNo,t0.U_LrDat,t0.U_AreaCode,t0.U_DocThrough,t0.u_team from inv1 t1 " & vbCrLf _
               & " left join OINV t0 on t0.DocEntry=t1.DocEntry " & vbCrLf _
               & " left join (select c0.cardcode,convert(nvarchar(max),c0.Building) as building,c0.Block,c0.Street,c0.City, c0.state,c2.TaxId1 as cstno,c2.TaxId11 tinno, c0.address,c1.QryGroup1 as distributor,c1.QryGroup2 as franchise,c1.QryGroup3 as showroom from crd1 c0 " & vbCrLf _
               & " left join OCRD c1 on c1.CardCode=c0.cardcode " & vbCrLf _
               & " left join crd7 c2 on c2.CardCode=c0.CardCode and (len(rtrim(c2.Address))=0 or c2.Address is null )" & vbCrLf
               & " left join [@INCM_BND1]  f on f.U_Name=t0.u_brand and convert(nvarchar(max),f.U_Remarks) is not null" & vbcrlf _
               & " where c0.adrestype='B' and upper(c0.Address)='OFFICE' and t0.docnum=" & Val(mdocnum) & vbCrLf _
               & " group by c1.cardfname,c1.zipcode,c0.CardCode,convert(nvarchar(max),c0.Building),c0.Block,c0.Street,c0.City, c0.state,c2.TaxId1,c2.TaxId11 , c0.address,c1.QryGroup1,c1.QryGroup2,c1.qrygroup3) t3 on t3.CardCode=t0.CardCode " & vbCrLf _
               & " order by t0.DocNum"


        msql2 = "select k.CardCode,k.CardName,k.DocNum,k.U_Transport,k.U_Dsnation,k.U_DocThrough,k.Docdate,k.U_Arcode,k.Address,k.Address2," & vbCrLf _
            & "k.prnname, k.item,k.u_style,SUM(k.qty36) as qtyr36,SUM(k.qty38) as qtyr38,SUM(k.qty40) as qtyr40,SUM(k.qty42) as qtyr42," & vbCrLf _
            & "SUM(k.qty44) as qtyr44,SUM(k.qty46) as qtyr46,SUM(k.qty48) as qtyr48,SUM(k.qty50) as qtyr50, " & vbCrLf _
            & " sum(k.qty36+k.qty38+k.qty40+k.qty42+k.qty44+k.qty46+k.qty48+k.qty50) as total,sum(k.qty36+k.qty38+k.qty40+k.qty42+k.qty44+k.qty46+k.qty48+k.qty50)/convert(int,k.box) as boxqty " & vbCrLf _
            & " from " & vbCrLf _
            & "(select t2.CardCode,t2.CardName,t2.DocNum,t2.U_Transport,t2.U_Dsnation,t2.U_DocThrough,t2.Docdate,t2.U_Arcode,t2.Address,t2.Address2," & vbCrLf _
            & "case when charindex('_', t1.u_catalogname)>0 then substring(t1.U_CatalogName,1,CHARINDEX('_',t1.U_CatalogName)-1) " & vbCrLf _
            & "else case when charindex('-', t1.u_catalogname)>0 then substring(t1.U_CatalogName,1,CHARINDEX('-',t1.U_CatalogName)-1) else t1.U_CatalogName end end as item, " & vbCrLf _
            & "t1.u_style ,case when t1.u_size in ('XS','36','45','75') then sum(t1.quantity) else 0 end qty36, " & vbCrLf _
            & "case when t1.u_size in ('S','38','50','80') then sum(t1.quantity) else 0 end qty38," & vbCrLf _
            & "case when t1.u_size in ('M','40','55','85') then sum(t1.quantity) else 0 end qty40," & vbCrLf _
            & "case when t1.u_size in ('L','42','60','90') then sum(t1.quantity) else 0 end qty42," & vbCrLf _
            & "case when t1.u_size in ('XL','44','65','95') then sum(t1.quantity) else 0 end qty44," & vbCrLf _
            & "case when t1.u_size in ('XXL','46','70','100') then sum(t1.quantity) else 0 end qty46," & vbCrLf _
            & " case when t1.u_size in ('3XL','48','105') then sum(t1.quantity) else 0 end qty48, " & vbCrLf _
            & "case when t1.u_size in ('4XL','110') then sum(t1.quantity) else 0 end qty50,tm.SalPackUn as box," & vbCrLf _
            & " tm.u_subgrp6 as prnname from INV1 t1 " & vbCrLf _
            & "left join OINV t2 on t2.DocEntry=t1.DocEntry" & vbCrLf _
            & "left join OITM tm on tm.ItemCode=t1.ItemCode " & vbCrLf _
            & "where t2.docnum=" & Val(mdocnum) & vbCrLf _
            & "group by  tm.u_subgrp6,t2.CardCode,t2.CardName,t2.DocNum,t2.U_Transport,t2.U_Dsnation,t2.U_DocThrough,t2.Docdate,t2.U_Arcode,t2.Address,t2.Address2," & vbCrLf _
            & "case when charindex('_', t1.u_catalogname)>0 then substring(t1.U_CatalogName,1,CHARINDEX('_',t1.U_CatalogName)-1) " & vbCrLf _
            & "else case when charindex('-', t1.u_catalogname)>0 then substring(t1.U_CatalogName,1,CHARINDEX('-',t1.U_CatalogName)-1) else t1.U_CatalogName end end," & vbCrLf _
            & "t1.u_style, t1.u_size, tm.SalPackUn ) k" & vbCrLf _
            & "group by k.prnname,k.CardCode,k.CardName,k.DocNum,k.U_Transport,k.U_Dsnation,k.U_DocThrough,k.Docdate,k.U_Arcode,k.Address,k.Address2,k.u_style, k.item, k.box"







        FileOpen(1, "C:\PAK.TXT", OpenMode.Output, OpenAccess.Write)
        lin = 1
        PAG = 1
        PrintLine(1, TAB((75 - Len("PACKING DELIVERY")) / 2), Chr(27) + Chr(45) + Chr(1) + Chr(27) + Chr(69) + "PACKING DELIVERY" + Chr(27) + Chr(45) + Chr(0) + Chr(18) + Chr(27) + Chr(70) + Chr(18))
        lin = lin + 1
        PrintLine(1, dlin(Chr(196), 77))
        lin = lin + 1
        PrintLine(1)
        lin = lin + 1
        PrintLine(1, TAB(5), "From", TAB(47), "To")
        lin = lin + 1

        Dim CMD3 As New SqlClient.SqlCommand(msql3, con)
        Dim DR3 As SqlClient.SqlDataReader
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        DR3 = CMD3.ExecuteReader
        If DR3.HasRows = True Then
            While DR3.Read
                MFNAME = DR3.Item("compnyname")
                MFADD1 = DR3.Item("building")
                MFADD2 = Trim(DR3.Item("block")) & Trim(DR3.Item("streetno"))
                MFADD3 = DR3.Item("street")
                MFADD4 = trim(DR3.Item("city")) & "-" trim(DR3.Item("zipcode"))
                MFTIN = "TIN No: " & Trim(DR3.Item("TINno")) & "CST No:" & Trim(DR3.Item("CSTNO"))
                'MFSHORT = DR3.Item("CMPSHORT")
            End While
        End If
        DR3.Close()
        CMD3.Dispose()

        Dim CMD2 As New SqlClient.SqlCommand(msql, con)
        Dim DR2 As SqlClient.SqlDataReader
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        DR2 = CMD2.ExecuteReader
        If DR2.HasRows = True Then
            While DR2.Read
                MTNAME = DR2.Item("cardfNAME")
                MADD1 = DR2.Item("building")
                MADD2 = Trim(DR2.Item("block"))
                MADD3 = DR2.Item("street")
                MADD4 = Trim(DR2.Item("city")) & "-" & Trim(DR2.Item("zipcode"))
                MTIN = "TIN No: " & Trim(DR2.Item("TINno")) & "  " & "CST No: " & Trim(DR2.Item("cstno"))
                'MFSHORT = DR2.Item("CMPSHORT")
                '    End While
                'End If
                'DR2.Close()
                'CMD2.Dispose()

        PrintLine(1, TAB(5), Chr(27) + Chr(69) + Chr(18) + Trim(MFNAME) + Chr(27) + Chr(70) + Chr(18), TAB(38), "|", TAB(48), Chr(27) + Chr(69) + MTNAME + Chr(27) + Chr(70) + Chr(18))
        lin = lin + 1
        PrintLine(1, TAB(5), Chr(27) + Chr(69) + Chr(18) + Trim(MFADD1) + Chr(27) + Chr(70) + Chr(18), TAB(38), "|", TAB(48), Chr(27) + Chr(69) + MADD1 + Chr(27) + Chr(70) + Chr(18))
        lin = lin + 1
        PrintLine(1, TAB(5), Chr(27) + Chr(69) + Chr(18) + Trim(MFADD2) + Chr(27) + Chr(70) + Chr(18), TAB(38), "|", TAB(48), Chr(27) + Chr(69) + MADD2 + Chr(27) + Chr(70) + Chr(18))
        lin = lin + 1
        PrintLine(1, TAB(5), Chr(27) + Chr(69) + Chr(18) + Trim(MFADD3) + Chr(27) + Chr(70) + Chr(18), TAB(38), "|", TAB(48), Chr(27) + Chr(69) + MADD3 + Chr(27) + Chr(70) + Chr(18))
        lin = lin + 1
        PrintLine(1, TAB(5), Chr(27) + Chr(69) + Chr(18) + Trim(MFADD4) + Chr(27) + Chr(70) + Chr(18), TAB(38), "|", TAB(48), Chr(27) + Chr(69) + MADD4 + Chr(27) + Chr(70) + Chr(18))
        lin = lin + 1
        PrintLine(1, TAB(5), Chr(27) + Chr(69) + Chr(18) + Trim(MFTIN) + Chr(27) + Chr(70) + Chr(18), TAB(38), "|", TAB(48), Chr(27) + Chr(69) + MTIN + Chr(27) + Chr(70) + Chr(18))
        lin = lin + 1
        'PrintLine(1, TAB(5), MFNAME, TAB(48), MTNAME)
        'lin = lin + 1
        PrintLine(1)
        lin = lin + 1
                PrintLine(1, dlin(Chr(196), 77))
                lin = lin + 1
            PrintLine(1, TAB(5), "Packing.No : ", TAB(19),  Trim(dr2.Item("docnum")), TAB(45), "Date : " & dr2.Item("docdate")
                lin = lin + 1
            PrintLine(1, TAB(5), "Inv.No : ", TAB(16), Trim(dr2.Item("Stype")) & Trim(dr2.Item("docnum")), TAB(45), "Date : " & dr2.Item("docdate")
                lin = lin + 1
            printline(1,tab(5),"Area Code" & trim(dr2.Item("u_areacode") & tab(45),"Document Through" & dr2.Item("U_DocThrough") )
                lin = lin + 1
            PrintLine(1, TAB(5), "Lorry : ", TAB(16), Trim(dr2.Item("u_transport")), TAB(45), "To : " & dr2.Item("u_destination")
                lin = lin + 1
        PrintLine(1)
        lin = lin + 1
        PrintLine(1, dlin(Chr(196), 77))
        lin = lin + 1
        

        PrintLine(1, TAB(1), "", TAB(27), "", TAB(33), "45", TAB(38),"|",tab(39), "50",tab(44),"|",tab(45),"55",tab(50),"|" tab(51),"60",tab(56),"|",tab(57),"65",tab(62),"|",tab(63),"70",tab(68),"|",tab(69),"",tab(74),"|",tab(75),"", tab(80),"Total Pcs")
        lin = lin + 1
        PrintLine(1, TAB(1), "Brand Name", TAB(27), "Style", TAB(33), "75", TAB(38),"|",tab(39), "80",tab(44),"|",tab(45),"85",tab(50),"|" tab(51),"90",tab(56),"|",tab(57),"95",tab(62),"|",tab(63),"100",tab(68),"|",tab(69),"105",tab(74),"|",tab(75),"110", tab(80),"Total Pcs")
        lin = lin + 1
        PrintLine(1, TAB(1), "", TAB(27), "", TAB(33), "36", TAB(38),"|",tab(39), "38",tab(44),"|",tab(45),"40",tab(50),"|" tab(51),"42",tab(56),"|",tab(57),"44",tab(62),"|",tab(63),"46",tab(68),"|",tab(69),"48",tab(74),"|",tab(75),"50", tab(80),"Total Pcs")
        lin = lin + 1
        PrintLine(1, TAB(1), "",tab(26),"|", TAB(27), "", TAB(33), "XS", TAB(38),"|",tab(39), "S",tab(44),"|",tab(45),"M",tab(50),"|" tab(51),"L",tab(56),"|",tab(57),"XL",tab(62),"|",tab(63),"XXL",tab(68),"|",tab(69),"3XL",tab(74),"|",tab(75),"4XL", tab(80),"Total Pcs")
        lin = lin + 1
        'PrintLine(1, TAB(1), "S.No", TAB(24), "Particulars", TAB(44), "Qty", TAB(55), "Rate", TAB(64), "Tax", TAB(72), "Amount")
        'lin = lin + 1
        PrintLine(1, dlin(Chr(196), 77))
        lin = lin + 1


        Dim CMD4 As New SqlClient.SqlCommand(msql2, con)
        Dim DR4 As SqlClient.SqlDataReader
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        DR4 = CMD4.ExecuteReader
        If DR4.HasRows = True Then
            While DR4.Read
                Print(1, TAB(1), Mid(DR4.Item("item"), 1, 25))
                Print(1, TAB(26), "|", TAB(27), DR4.Item("u_style"))
                print(1, tab(26),"|",  TAB(37),"|",tab(42),"|",tab(47),"|" tab(52),"|",tab(57),"|",tab(62),"|",tab(69),"",tab(74),"|",tab(75),"", tab(80),"Total Pcs")
                Print(1, TAB(36 - Len(Microsoft.VisualBasic.Format(DR4.Item("qtyr36"), "###0"))), Microsoft.VisualBasic.Format(DR4.Item("qtyr36"), "###0"))
                Print(1, TAB(37), "|")
                Print(1, TAB(41 - Len(Microsoft.VisualBasic.Format(DR4.Item("qtyr38"), "###0"))), Microsoft.VisualBasic.Format(DR4.Item("qtyr38"), "###0"))
                Print(1, TAB(42), "|")
                Print(1, TAB(46 - Len(Microsoft.VisualBasic.Format(DR4.Item("qtyr40"), "###0"))), Microsoft.VisualBasic.Format(DR4.Item("qtyr40"), "###0"))
                Print(1, TAB(47), "|")
                Print(1, TAB(51 - Len(Microsoft.VisualBasic.Format(DR4.Item("qtyr42"), "###0"))), Microsoft.VisualBasic.Format(DR4.Item("qtyr42"), "###0"))
                Print(1, TAB(52), "|")
                Print(1, TAB(56 - Len(Microsoft.VisualBasic.Format(DR4.Item("qtyr44"), "###0"))), Microsoft.VisualBasic.Format(DR4.Item("qtyr44"), "###0"))
                Print(1, TAB(57), "|")
                Print(1, TAB(61 - Len(Microsoft.VisualBasic.Format(DR4.Item("qtyr46"), "###0"))), Microsoft.VisualBasic.Format(DR4.Item("qtyr46"), "###0"))
                Print(1, TAB(62), "|")
                Print(1, TAB(66 - Len(Microsoft.VisualBasic.Format(DR4.Item("qtyr48"), "###0"))), Microsoft.VisualBasic.Format(DR4.Item("qtyr48"), "###0"))
                Print(1, TAB(67), "|")
                Print(1, TAB(71 - Len(Microsoft.VisualBasic.Format(DR4.Item("qtyr50"), "###0"))), Microsoft.VisualBasic.Format(DR4.Item("qtyr50"), "###0"))
                Print(1, TAB(72), "|")
                Print(1, TAB(78 - Len(Microsoft.VisualBasic.Format(DR4.Item("total"), "#####0"))), Microsoft.VisualBasic.Format(DR4.Item("total"), "#####0"))
                Print(1, TAB(79), "/")
                PrintLine(1, TAB(83 - Len(Microsoft.VisualBasic.Format(DR4.Item("boxqty"), "###0"))), Microsoft.VisualBasic.Format(DR4.Item("boxqty"), "###0"))
                lin = lin + 1
                MQTY = MQTY + Val(DR4.Item("total"))
                MAMT = MAMT + Val(DR4.Item("boxqty"))
                        If lin > 28 Then
                            PrintLine(1, dlin(Chr(196), 77))
                            lin = lin + 1
                            Print(1, TAB(15), "C/o")
                            Print(1, TAB(45 - Len(Microsoft.VisualBasic.Format(MQTY, "#####0"))), Microsoft.VisualBasic.Format(MQTY, "#####0"))
                            'Print(1, TAB(61 - Len(Microsoft.VisualBasic.Format(Val(.get_TextMatrix(I, 3)), "#######0.00"))), Microsoft.VisualBasic.Format(Val(.get_TextMatrix(I, 3)), "#######0.00"))
                            PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(MAMT, "#######0.00"))), Microsoft.VisualBasic.Format(MAMT, "#######0.00"))
                            lin = lin + 1
                            PrintLine(1, dlin(Chr(196), 77))
                            lin = lin + 1
                            PrintLine(1, Chr(12))
                            lin = 1
                            PAG = PAG + 1
                            PrintLine(1, TAB((75 - Len("PACKING DELIVERY")) / 2), Chr(27) + Chr(45) + Chr(1) + Chr(27) + Chr(69) + "PACKING DELIVERY" + Chr(27) + Chr(70) + Chr(27) + Chr(45) + Chr(0) + Chr(18))
                            PrintLine(1, dlin(Chr(196), 77))
                            lin = lin + 1
                            PrintLine(1)
                            lin = lin + 1
                            PrintLine(1, TAB(5), "From", TAB(47), "To")
                            lin = lin + 1
                            PrintLine(1, TAB(5), Chr(27) + Chr(69) + MFNAME + Chr(27) + Chr(70), TAB(38), "|", TAB(48), Chr(27) + Chr(69) + MTNAME + Chr(27) + Chr(70) + Chr(18))
                            lin = lin + 1
                            PrintLine(1, TAB(5), Chr(27) + Chr(69) + MFADD1 + Chr(27) + Chr(70), TAB(38), "|", TAB(48), Chr(27) + Chr(69) + MADD1 + Chr(27) + Chr(70))
                            lin = lin + 1
                            PrintLine(1, TAB(5), Chr(27) + Chr(69) + MFADD2 + Chr(27) + Chr(70), TAB(38), "|", TAB(48), Chr(27) + Chr(69) + MADD2 + Chr(27) + Chr(70))
                            lin = lin + 1
                            PrintLine(1, TAB(5), Chr(27) + Chr(69) + MFADD3 + Chr(27) + Chr(70), TAB(38), "|", TAB(48), Chr(27) + Chr(69) + MADD3 + Chr(27) + Chr(70))
                            lin = lin + 1
                            PrintLine(1, TAB(5), Chr(27) + Chr(69) + MFADD4 + Chr(27) + Chr(70), TAB(38), "|", TAB(48), Chr(27) + Chr(69) + MADD4 + Chr(27) + Chr(70))
                            lin = lin + 1
                            PrintLine(1, TAB(5), Chr(27) + Chr(69) + MFTIN + Chr(27) + Chr(70), TAB(38), "|", TAB(48), Chr(27) + Chr(69) + MTIN + Chr(27) + Chr(70))
                            lin = lin + 1

                            PrintLine(1, dlin(Chr(196), 77))
                            lin = lin + 1
                            PrintLine(1, TAB(5), "Packing.No : ", TAB(19),  Trim(dr2.Item("docnum")), TAB(45), "Date : " & dr2.Item("docdate")
                            lin = lin + 1
                            PrintLine(1, TAB(5), "Inv.No : ", TAB(16), Trim(dr2.Item("Stype")) & Trim(dr2.Item("docnum")), TAB(45), "Date : " & dr2.Item("docdate")
                            lin = lin + 1
                            printline(1,tab(5),"Area Code" & trim(dr2.Item("u_areacode") & tab(45),"Document Through" & dr2.Item("U_DocThrough") )
                            lin = lin + 1
                            PrintLine(1, TAB(5), "Lorry : ", TAB(16), Trim(dr2.Item("u_transport")), TAB(45), "To : " & dr2.Item("u_destination")
                            lin = lin + 1
                            PrintLine(1)
                            lin = lin + 1
                            PrintLine(1, dlin(Chr(196), 77))
                            lin = lin + 1


                            PrintLine(1, TAB(1), "", TAB(27), "", TAB(33), "45", TAB(38),"|",tab(39), "50",tab(44),"|",tab(45),"55",tab(50),"|" tab(51),"60",tab(56),"|",tab(57),"65",tab(62),"|",tab(63),"70",tab(68),"|",tab(69),"",tab(74),"|",tab(75),"", tab(80),"Total Pcs")
                            lin = lin + 1
                            PrintLine(1, TAB(1), "Brand Name", TAB(27), "Style", TAB(33), "75", TAB(38),"|",tab(39), "80",tab(44),"|",tab(45),"85",tab(50),"|" tab(51),"90",tab(56),"|",tab(57),"95",tab(62),"|",tab(63),"100",tab(68),"|",tab(69),"105",tab(74),"|",tab(75),"110", tab(80),"Total Pcs")
                            lin = lin + 1
                            PrintLine(1, TAB(1), "", TAB(27), "", TAB(33), "36", TAB(38),"|",tab(39), "38",tab(44),"|",tab(45),"40",tab(50),"|" tab(51),"42",tab(56),"|",tab(57),"44",tab(62),"|",tab(63),"46",tab(68),"|",tab(69),"48",tab(74),"|",tab(75),"50", tab(80),"Total Pcs")
                            lin = lin + 1
                            PrintLine(1, TAB(1), "", TAB(27), "", TAB(33), "XS", TAB(38),"|",tab(39), "S",tab(44),"|",tab(45),"M",tab(50),"|" tab(51),"L",tab(56),"|",tab(57),"XL",tab(62),"|",tab(63),"XXL",tab(68),"|",tab(69),"3XL",tab(74),"|",tab(75),"4XL", tab(80),"Total Pcs")
                            lin = lin + 1
                            PrintLine(1, dlin(Chr(196), 77))
                            lin = lin + 1



                        End If


            End While
        End If
        DR4.Close()
        CMD4.Dispose()

            End While
        End If
        DR2.Close()
        CMD2.Dispose()


        LK = 60 - lin
        For K = lin To LK
            PrintLine(1)
            lin = lin + 1
        Next
        PrintLine(1, dlin(Chr(196), 77))
        lin = lin + 1
        Print(1, TAB(15), "Total ")
        Print(1, TAB(45 - Len(Microsoft.VisualBasic.Format(MQTY, "#####0"))), Microsoft.VisualBasic.Format(MQTY, "#####0"))
        'Print(1, TAB(61 - Len(Microsoft.VisualBasic.Format(Val(.get_TextMatrix(I, 3)), "#######0.00"))), Microsoft.VisualBasic.Format(Val(.get_TextMatrix(I, 3)), "#######0.00"))
        PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(MAMT, "#######0.00"))), Microsoft.VisualBasic.Format(MAMT, "#######0.00"))
        lin = lin + 1
        PrintLine(1, dlin(Chr(196), 77))
        lin = lin + 1
        If Val(TXTDISCAMT.Text) > 0 Then
            Print(1, TAB(40), "Less:Discount :" + Trim(TXTDISCPER.Text) + "%")
            PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(Val(TXTDISCAMT.Text), "#######0.00"))), Microsoft.VisualBasic.Format(Val(TXTDISCAMT.Text), "#######0.00"))
            lin = lin + 1
            MAMT = MAMT - Val(TXTDISCAMT.Text)
        End If
        If Val(TXTTAXAMT.Text) > 0 Then
            Print(1, TAB(40), "Add: Vat : " + Trim(TXTTAX.Text) + "%")
            PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(Val(TXTTAXAMT.Text), "#######0.00"))), Microsoft.VisualBasic.Format(Val(TXTTAXAMT.Text), "#######0.00"))
            lin = lin + 1
            MAMT = MAMT + Val(TXTTAXAMT.Text)
        End If
        If Val(TXTROUND.Text) <> 0 Then
            Print(1, TAB(40), "Round Off : ")
            PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(Val(TXTROUND.Text), "#######0.00"))), Microsoft.VisualBasic.Format(Val(TXTROUND.Text), "#######0.00"))
            lin = lin + 1
            MAMT = MAMT + Val(TXTROUND.Text)
        End If


        PrintLine(1, TAB(40), dlin(Chr(196), 37))
        lin = lin + 1
        Print(1, TAB(40), "Net  : ")
        PrintLine(1, TAB(77 - Len(Microsoft.VisualBasic.Format(MAMT, "#######0.00"))), Microsoft.VisualBasic.Format(MAMT, "#######0.00"))
        lin = lin + 1
        PrintLine(1, dlin(Chr(196), 77))
        lin = lin + 1
        Print(1, TAB(5), "Rupees: ")
        If Len(Trim(Str(MAMT))) > 78 Then
            PrintLine(1, TAB(13), Chr(15) + word(Str(MAMT)) + Chr(18))
        Else
            PrintLine(1, TAB(13), word(Str(MAMT)))
        End If

        lin = lin + 1
        PrintLine(1, dlin(Chr(196), 77))
        lin = lin + 1
        PrintLine(1, TAB(2), Chr(27) + Chr(45) + Chr(1) + Chr(27) + Chr(69) + "Terms & Conditons" + Chr(27) + Chr(70) + Chr(18) + Chr(27) + Chr(45) + Chr(0) + Chr(18))
        lin = lin + 1
        PrintLine(1, TAB(4), "1. " & Trim(txtterm1.Text))
        lin = lin + 1
        PrintLine(1, TAB(4), "2. " & Trim(txtterm2.Text))
        lin = lin + 1
        PrintLine(1, TAB(4), "3. " & Trim(txtterm3.Text))
        lin = lin + 1
        PrintLine(1, dlin(Chr(196), 77))
        lin = lin + 1
        PrintLine(1, TAB(50), "For " & Trim(MFNAME))
        lin = lin + 1
        PrintLine(1, Chr(12))
        lin = 1
        FileClose(1)
        MQTY = 0
        MAMT = 0

        'Shell("command.com /c TYPE " & "c:\SAMPDEL.PRN>" & "lpt" & LTrim(Str(lptport)))
        If MsgBox("Print", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Shell("command.com /c TYPE " & "c:\pord.txt>PRN", AppWinStyle.Hide)
        Else
            Shell("command.com /c edit " & "c:\pord.txt", AppWinStyle.MaximizedFocus)
        End If

            End If
        End If
    End Sub
End Class
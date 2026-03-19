Imports System.io
Imports CarlosAg.ExcelXmlWriter
Public Class frmlineloadrep
    Dim msql, sql2, sql3 As String
    Dim i, j As Int16
    Dim mhtot, motot, mshtot As Int32
    Dim msal, msal1 As Double
    Private Sub Frmlineloadrep_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        Me.Height = My.Computer.Screen.Bounds.Height - 20
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If chkcomplete.Checked = True Then
            Call cmpltloaddata()
        Else

            Call loaddata()
        End If

    End Sub
    Private Sub loaddata()

        If Chkdat.Checked = True Then
            msql = " select c.u_docdate, c.u_tlno, it.U_BrandGroup,it.U_Style,sum(b.U_RecdQty) issqty from [@INM_PDE1] b " & vbCrLf _
                               & " left join [@INM_OPDE] c on c.DocEntry=b.DocEntry " & vbCrLf _
                               & " left join oitm it on it.ItemCode=b.U_ItemCode " & vbCrLf _
                               & " where c.U_DocDate>='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and c.U_DocDate<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and c.U_DTOperCode in ('KAJAGD','STGD') and c.U_Type='RG' " & vbCrLf

            If Val(txtlineno.Text) > 0 Then
                msql = msql & " and c.u_tlno='" & Trim(txtlineno.Text) & "' "
            End If
            msql = msql & " group by c.u_docdate, c.u_tlno, it.U_BrandGroup,it.U_Style " & vbCrLf _
                               & "order by c.u_docdate, c.U_TLNo,it.U_Style,it.U_BrandGroup"


        Else
            msql = " select c.u_tlno, it.U_BrandGroup,it.U_Style,sum(b.U_RecdQty) issqty from [@INM_PDE1] b " & vbCrLf _
                   & " left join [@INM_OPDE] c on c.DocEntry=b.DocEntry " & vbCrLf _
                   & " left join oitm it on it.ItemCode=b.U_ItemCode " & vbCrLf _
                   & " where c.U_DocDate>='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and c.U_DocDate<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and c.U_DTOperCode in ('KAJAGD','STGD') and c.U_Type='RG' " & vbCrLf
            If Val(txtlineno.Text) > 0 Then
                msql = msql & " and c.u_tlno='" & Trim(txtlineno.Text) & "' "
            End If
            msql = msql & " group by c.u_tlno, it.U_BrandGroup,it.U_Style " & vbCrLf _
                   & "order by c.U_TLNo,it.U_Style,it.U_BrandGroup"
        End If


        Dim dt4 As DataTable = getDataTable(msql)
        ' mtot = 0
        If dt4.Rows.Count > 0 Then

            dg.DataSource = dt4

            dg.ColumnHeadersDefaultCellStyle.Font = New Font(DataGridView.DefaultFont, FontStyle.Bold)
            dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            If Chkdat.Checked = True Then
                dg.Columns(0).Width = 60
                dg.Columns(1).Width = 50
                dg.Columns(2).Width = 200
                dg.Columns(3).Width = 60
                dg.Columns(4).Width = 80
               
            Else

                dg.Columns(0).Width = 50
                dg.Columns(1).Width = 200
                dg.Columns(2).Width = 60
                dg.Columns(3).Width = 80
                
            End If


            For i = 0 To dg.Columns.Count - 1
                '        dgsr.Columns(i).ReadOnly = True
                If Chkdat.Checked = True Then
                    If i = 4 Then
                        dg.Columns(i).ValueType = GetType(Int32)
                        dg.Columns(i).DefaultCellStyle.Format = ("0")
                        dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    End If
                Else
                    If i = 3 Then
                        dg.Columns(i).ValueType = GetType(Int32)
                        dg.Columns(i).DefaultCellStyle.Format = ("0")
                        dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    End If
                End If


               
                'dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                '        If i > 1 Then
                '            dgsr.Columns(i).ValueType = GetType(Decimal)
                '            dgsr.Columns(i).DefaultCellStyle.Format = ("0.00")
                '        End If
            Next

        Else
            dg.DataSource = Nothing
        End If


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub butexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butexcel.Click
        gridexcelexport(dg, 1)
    End Sub

    Private Sub cmpltloaddata()
        Cursor = Cursors.WaitCursor

        msql = "select k.u_lineno,isnull(sum(k.total),0) Total," _
              & " isnull(sum(k.[1]),0) [1],isnull(sum(k.[2]),0) [2],isnull(sum(k.[3]),0) [3],isnull(sum(k.[4]),0) [4],isnull(sum(k.[5]),0) [5],isnull(sum(k.[6]),0) [6],isnull(sum(k.[7]),0) [7],isnull(sum(k.[8]),0) [8],isnull(sum(k.[9]),0) [9],isnull(sum(k.[10]),0) [10], " _
              & " isnull(sum(k.[11]),0) [11],isnull(sum(k.[12]),0) [12],isnull(sum(k.[13]),0) [13],isnull(sum(k.[14]),0) [14],isnull(sum(k.[15]),0) [15],isnull(sum(k.[16]),0) [16],isnull(sum(k.[17]),0) [17],isnull(sum(k.[18]),0) [18],isnull(sum(k.[19]),0) [19],isnull(sum(k.[20]),0) [20], " _
              & " isnull(sum(k.[21]),0) [21],isnull(sum(k.[22]),0) [22],isnull(sum(k.[23]),0) [23],isnull(sum(k.[24]),0) [24],isnull(sum(k.[25]),0) [25],isnull(sum(k.[26]),0) [26],isnull(sum(k.[27]),0) [27],isnull(sum(k.[28]),0) [28], " _
              & " isnull(sum(k.[29]),0) [29],isnull(sum(k.[30]),0) [30],isnull(sum(k.[31]),0) [31] from ( " _
              & " select u_lineno,isnull([1],0) [1],isnull([2],0) [2],isnull([3],0) [3],isnull([4],0) [4],isnull([5],0) [5],isnull([6],0) [6],isnull([7],0) [7],isnull([8],0) [8],isnull([9],0) [9],isnull([10],0) [10], " _
              & " isnull([11],0) [11],isnull([12],0) [12],isnull([13],0) [13],isnull([14],0) [14],isnull([15],0) [15],isnull([16],0) [16],isnull([17],0) [17],isnull([18],0) [18],isnull([19],0) [19],isnull([20],0) [20], " _
              & " isnull([21],0) [21],isnull([22],0) [22],isnull([23],0) [23],isnull([24],0) [24],isnull([25],0) [25],isnull([26],0) [26],isnull([27],0) [27],isnull([28],0) [28], " _
              & " isnull([29],0) [29],isnull([30],0) [30],isnull([31],0) [31], " _
              & " (isnull([1],0)+isnull([2],0)+isnull([3],0)+isnull([4],0)+isnull([5],0)+isnull([6],0)+isnull([7],0)+isnull([8],0)+isnull([9],0)+isnull([10],0)+isnull([11],0)+isnull([12],0)+ " _
              & " isnull([13],0)+isnull([14],0)+isnull([15],0)+isnull([16],0)+isnull([17],0)+isnull([18],0)+isnull([19],0)+isnull([20],0)+isnull([21],0)+isnull([22],0)+isnull([23],0)+ " _
              & " isnull([24],0)+isnull([25],0)+isnull([26],0)+isnull([27],0)+isnull([28],0)+isnull([29],0)+isnull([30],0)+isnull([31],0)) Total from ( " _
              & " select b.u_docdate,DATEPART(d,b.u_docdate) datpart, b.u_lineno,sum(c.U_AccpQty) qty from [@inm_owip] b with (nolock)  " _
              & " inner join [@inm_wip1] c with (nolock) on b.docentry=c.docentry " _
              & " where b.u_docdate>='" & Format(CDate(mskdatfr.Text), "yyyy-MM-dd") & "' and b.u_docdate<='" & Format(CDate(mskdatto.Text), "yyyy-MM-dd") & "' and b.u_opercode in ('KAJAGD') and b.U_TranType='RG' " _
              & " group by u_docdate,b.u_lineno) s " _
              & " Pivot (sum(qty) " _
              & "   for datpart in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31]) " _
           & " ) p) k group by k.U_LineNo order by k.u_lineno "

        Dim dt4 As DataTable = getDataTable(msql)
        ' mtot = 0
        If dt4.Rows.Count > 0 Then

            dg.DataSource = dt4

            For i = 0 To dg.Columns.Count - 1
                '        dgsr.Columns(i).ReadOnly = True

                If i >= 1 Then
                    dg.Columns(i).Width = 50
                    dg.Columns(i).ValueType = GetType(Int32)
                    dg.Columns(i).DefaultCellStyle.Format = ("0")
                    dg.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
                dg.Columns(i).ReadOnly = True
            Next


        End If

        Cursor = Cursors.Default

    End Sub
End Class
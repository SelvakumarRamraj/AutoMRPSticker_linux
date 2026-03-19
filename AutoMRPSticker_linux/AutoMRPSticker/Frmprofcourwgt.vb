Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO
Public Class Frmprofcourwgt
    Dim msql, sql1, sql2 As String
    Dim mstart, mend As Int64
    Dim mnobund As Integer
    Dim mprfx, mplace As String
    Private WithEvents PD As New PrintDocument()
    Dim mbuild As String
    Dim mblock As String
    Dim mstreet As String
    Dim mcity As String
    Dim mzipcode As String
    Dim mdistrict As String
    Dim mstate As String
    Dim mcountry As String
    Dim mto As String
    Dim mcardname As String
    Dim lpodno As String
    Dim mdocnum As Integer
    Dim mkwgt As Double = 0.0
    Private SaveEnabledList As New Dictionary(Of Integer, Boolean)
    Dim mrowno As Integer

    Private Sub Btndisp_Click(sender As System.Object, e As System.EventArgs) Handles Btndisp.Click
        Call loaddata()
    End Sub

    Private Sub loaddata()
        Dg.DataSource = Nothing
        If MsgBox("Display All", vbYesNo) = vbYes Then
            msql = "select b.docnum,b.docdate,b.docentry,b.cardcode,b.cardname,b.U_Transport,b.U_TransporterName,b.U_Noofbun,isnull(c.cnt,0) actbundle,b.U_LR_Weight, b.U_courpodno,isnull(b.U_LRNO,'') U_LRNO,isnull(b.u_destination,'') destination from oinv b " _
                   & " inner join nnm1 s on s.series=b.series " _
                   & "  left join (select docentry, count(*) cnt from rinv7 where packagetyp not like '%Cover%' group by docentry) c on c.DocEntry=b.docentry " _
                   & " where b.docdate>='" & dtpfr.Value.ToString("yyyy-MM-dd") & "' and b.docdate<='" & dtpto.Value.ToString("yyyy-MM-dd") & "' " _
                   & " and b.canceled='N' and left(s.seriesname,2)='SS' and u_transport like '%COURIER%' and u_transport not like '%ST%'"
        Else

            msql = "select b.docnum,b.docdate,b.docentry,b.cardcode,b.cardname,b.U_Transport,b.U_TransporterName,b.U_Noofbun,isnull(c.cnt,0) actbundle,b.U_LR_Weight, b.U_courpodno,isnull(b.U_LRNO,'') U_LRNO,isnull(b.u_destination,'') destination from oinv b " _
                   & " inner join nnm1 s on s.series=b.series " _
                   & "  left join (select docentry, count(*) cnt from rinv7 where packagetyp not like '%Cover%' group by docentry) c on c.DocEntry=b.docentry " _
                   & " where b.docdate>='" & dtpfr.Value.ToString("yyyy-MM-dd") & "' and b.docdate<='" & dtpto.Value.ToString("yyyy-MM-dd") & "' " _
                   & " and b.canceled='N' and left(s.seriesname,2)='SS' and isnull(b.u_lrno,'')='' and u_transport like '%COURIER%' and u_transport not like '%ST%'"
        End If

        Dim dt As DataTable = getDataTable(msql)
        Dg.DataSource = dt
        AddButtonColumn()
        'MarkSaveStatus()
        EvaluateAllRowsSaveEnabled()
    End Sub

    Private Sub AddButtonColumnold()

        'If Dg.Columns.Contains("btnSave") = False Then
        '    Dim btn As New DataGridViewButtonColumn
        '    btn.Name = "btnSave"
        '    btn.HeaderText = "Save"
        '    btn.Text = "Save"
        '    btn.UseColumnTextForButtonValue = True
        '    btn.Width = 70

        '    Dg.Columns.Add(btn)
        'End If

        If Dg.Columns.Contains("btnSave") = False Then
            Dim btn As New DataGridViewButtonColumn
            btn.Name = "btnSave"
            btn.HeaderText = "Save"
            btn.Text = "Save"
            btn.UseColumnTextForButtonValue = True
            btn.Width = 70
            btn.FlatStyle = FlatStyle.Flat   ' 🔥 VERY IMPORTANT
            Dg.Columns.Add(btn)
        End If

    End Sub

    Private Sub AddButtonColumn()

        'Remove old button column if already exists
        If Dg.Columns.Contains("btnSave") Then
            Dg.Columns.Remove("btnSave")
        End If

        'Create new button column
        Dim btn As New DataGridViewButtonColumn
        btn.Name = "btnSave"
        btn.HeaderText = "Save"
        btn.Text = "Save"
        btn.UseColumnTextForButtonValue = True
        btn.Width = 70
        btn.FlatStyle = FlatStyle.Flat   ' 🔥 VERY IMPORTANT
        'btn.Visible = False
        'Add as LAST COLUMN
        Dg.Columns.Add(btn)

    End Sub
    Private Sub MarkSaveStatus()
        For i As Integer = 0 To Dg.Rows.Count - 1
            If IsSaveEnabledForRow(i) Then
                Dg.Rows(i).Cells("btnSave").Value = "1"
            Else
                Dg.Rows(i).Cells("btnSave").Value = "0"
            End If
        Next
    End Sub

    Private Sub EvaluateAllRowsSaveEnabled()
        SaveEnabledList.Clear()

        For i As Integer = 0 To Dg.Rows.Count - 1
            SaveEnabledList(i) = IsSaveEnabledForRow(i)
        Next
    End Sub
    Private Sub podsav()
        Dim kpodno As String
        Dim mcourno As Integer
        Dim commands As New List(Of SqlCommand)

        For i As Integer = 0 To Dg.Rows.Count - 1


            'msql = "UPDATE OINV SET PODNO = CASE WHEN ISNULL(PODNO, '') = ''  THEN '" & Trim(mpodno) & "' ELSE PODNO + '/' + RIGHT('" & Trim(mpodno) & "', 3)   End WHERE DocEntry=" & docentry
            ''msql = "update oinv set u_process='Y' where docentry in (" & docEntryList & ")"
            'Dim cmd As New OleDbCommand(msql)
            'commands.Add(cmd)

            kpodno = AUTONO()
            mcourno = Convert.ToInt32(kpodno.Replace(mprfx, ""))
            sql1 = "insert into courier (date,docnum,cardcpde,cardname,company,podno,courierno,docentry) " _
                 & " values('" & Microsoft.VisualBasic.Format(CDate(Dg.Rows(i).Cells(1).Value), "yyyy-MM-dd") & "'," & Convert.ToInt32(Dg.Rows(i).Cells(0).Value) & "," _
                 & "'" & Dg.Rows(i).Cells(3).Value & "','" & Dg.Rows(i).Cells(4).Value & "','ATITHYA'," & kpodno & "'," & mcourno & "," & Convert.ToInt32(Dg.Rows(i).Cells(2).Value) & ")"

            Dim cmd As New SqlCommand(sql1)
            commands.Add(cmd)


            'msql = "UPDATE OINV SET PODNO = CASE WHEN ISNULL(PODNO, '') = ''  THEN '" & Trim(mpodno) & "' ELSE PODNO + '/' + RIGHT('" & Trim(mpodno) & "', 3)   End WHERE DocEntry=" & docentry
            msql = "UPDATE OINV SET U_LRNO = CASE WHEN ISNULL(U_LRNO, '') = ''  THEN '" & Trim(kpodno) & "' ELSE U_LRNO + '/' + RIGHT('" & Trim(kpodno) & "', 3)   End WHERE DocEntry=" & Convert.ToInt32(Dg.Rows(i).Cells(2).Value)
            'msql = "update oinv set u_process='Y' where docentry in (" & docEntryList & ")"
            Dim cmd1 As New SqlCommand(msql)
            commands.Add(cmd1)

        Next i

        Dim result As Boolean = ExecuteTransactionWithCommands(commands)

        If result Then
            'MsgBox("All records saved successfully!")
        Else
            MsgBox("Transaction failed. No data saved.")
        End If


    End Sub

    Private Sub loadcourdata()
        msql = "select startno,endno,prefix,place from couriernomast"

        'Dim CMD3 As New SqlCommand(msql, con)
        'Dim DR3 As SqlDataReader
        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        ''Dim DR3 As OleDb.OleDbDataReader
        'DR3 = CMD3.ExecuteReader
        'If DR3.HasRows = True Then
        '    While DR3.Read
        '        mstart = DR3.Item("startno")
        '        mend = DR3.Item("endno")
        '        mprfx = DR3.Item("prefix") & vbNullString
        '        mplace = DR3.Item("place") & vbNullString

        '    End While
        'End If
        'DR3.Close()
        'CMD3.Dispose()

        Dim dt As DataTable = getDataTable(msql)
        If dt.Rows.Count > 0 Then
            For Each rw As DataRow In dt.Rows
                mstart = rw("startno")
                mend = rw("endno")
                mprfx = rw("prefix") & vbNullString
                mplace = rw("place") & vbNullString
            Next
        End If


    End Sub

    Private Function AUTONO() As String
        Dim courstr, podstr As String

        'Dim CMD4 As New SqlCommand("SELECT MAX(courierno) AS TNO FROM courier where courierno>=" & mstart & " and courierno<=" & mend, con)
        '' End If


        'If con.State = ConnectionState.Closed Then
        '    con.Open()
        'End If

        'Dim CBNO As Int32 = IIf(IsDBNull(CMD4.ExecuteScalar) = False, CMD4.ExecuteScalar, 0)

        ''txtno.Text = CBNO + 1
        'courstr = CBNO + 1
        'If Val(courstr) = 1 Then
        '    courstr = mstart
        'End If
        'podstr = Trim(mprfx) + LTrim(courstr)
        'CMD4.Dispose()


        Dim sqry As String = "SELECT MAX(courierno) AS TNO FROM courier where courierno>=" & mstart & " and courierno<=" & mend
        Dim cbno As Int32 = Convert.ToInt32(executescalarQuery(sqry))
        courstr = CBNO + 1
        If Val(courstr) = 1 Then
            courstr = mstart
        End If
        podstr = Trim(mprfx) + LTrim(courstr)

        Return podstr
        'con2.Close()
    End Function

    Private Sub Dg_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dg.CellContentClick
        'If Dg.Columns(e.ColumnIndex).Name = "btnSave" AndAlso e.RowIndex >= 0 Then
        '    If Dg.Columns(e.ColumnIndex).Name <> "btnSave" Then Return

        '    If Not IsSaveEnabledForRow(e.RowIndex) Then
        '        MsgBox("LR Weight must be greater than 0 to save POD!", vbExclamation)
        '        Return
        '    End If


        '    podsav2(Microsoft.VisualBasic.Format(CDate(Dg.Rows(e.RowIndex).Cells("docdate").Value), "yyyy-MM-dd"), Convert.ToInt32(Dg.Rows(e.RowIndex).Cells("docnum").Value), Convert.ToInt32(Dg.Rows(e.RowIndex).Cells("docentry").Value), Trim(Dg.Rows(e.RowIndex).Cells("cardcode").Value), Trim(Dg.Rows(e.RowIndex).Cells("cardname").Value), Convert.ToInt16(Dg.Rows(e.RowIndex).Cells("u_noofbun").Value), Trim(Dg.Rows(e.RowIndex).Cells("u_lrno").Value), Trim(Dg.Rows(e.RowIndex).Cells("destination").Value), Convert.ToDouble(Dg.Rows(e.RowIndex).Cells("U_LR_Weight").Value))
        'End If

        '**** method 2
        If e.RowIndex < 0 Then Exit Sub

        If Dg.Columns(e.ColumnIndex).Name = "btnSave" Then

            Dim enabled As Boolean = SaveEnabledList(e.RowIndex)
            mrowno = e.RowIndex
            If Not enabled Then
                MsgBox("LR Weight must be greater than 0 to save POD!", vbExclamation)
                Return
            End If

            'Your save function
            podsav2(Microsoft.VisualBasic.Format(CDate(Dg.Rows(e.RowIndex).Cells("docdate").Value), "yyyy-MM-dd"), Convert.ToInt32(Dg.Rows(e.RowIndex).Cells("docnum").Value), Convert.ToInt32(Dg.Rows(e.RowIndex).Cells("docentry").Value), Trim(Dg.Rows(e.RowIndex).Cells("cardcode").Value), Trim(Dg.Rows(e.RowIndex).Cells("cardname").Value), Convert.ToInt16(Dg.Rows(e.RowIndex).Cells("u_noofbun").Value), Trim(Dg.Rows(e.RowIndex).Cells("u_lrno").Value), Trim(Dg.Rows(e.RowIndex).Cells("destination").Value), Convert.ToDouble(Dg.Rows(e.RowIndex).Cells("U_LR_Weight").Value))

            Dg.Rows.RemoveAt(mrowno)
            'Recheck row after save
            'SaveEnabledList(e.RowIndex) = IsSaveEnabledForRow(e.RowIndex)

            EvaluateAllRowsSaveEnabled()
            Dg.InvalidateRow(e.RowIndex)

        End If


    End Sub

    Private Sub podsav2(mdate As DateTime, docnum As Integer, docentry As Integer, cardcode As String, cardname As String, noofbund As Integer, mpodno As String, destination As String, wgt As Double)
        Dim kpodno As String
        Dim mcourno As Integer
        Dim mtru As Boolean = False
        Dim commands As New List(Of SqlCommand)



        'sql2 = "select distinct k.docentry from (select docentry from courier where docentry=" & docentry _
        '      & " union all " _
        '      & " select docentry from oinv where docentry=" & docentry & " and u_lrno='" & mpodno & "' ) k"

        sql2 = "select distinct k.docentry from (select docentry from courier where docentry=" & docentry _
             & " union all " _
             & " select docentry from oinv where docentry=" & docentry & " and len(rtrim(ltrim(isnull(u_lrno,''))))>0 ) k"



        Dim dtt As DataTable = getDataTable(sql2)
        If dtt.Rows.Count > 0 Then
            mtru = True
        Else
            mtru = False
        End If
        If mtru = True Then Exit Sub

        kpodno = AUTONO()
        mcourno = Convert.ToInt32(kpodno.Replace(mprfx, ""))
        'courprn(docentry, destination, kpodno)
        'courstikprint(kpodno, mdate.ToString("dd-MM-yyyy"), destination, wgt, docnum, docentry)

        sql1 = "insert into courier (date,docnum,cardcode,cardname,company,podno,courierno,docentry) " _
             & " values('" & Microsoft.VisualBasic.Format(CDate(mdate), "yyyy-MM-dd") & "'," & docnum & ",'" & Trim(cardcode) & "','" & Trim(cardname) & "','ATITHYA','" & Trim(kpodno) & "'," & mcourno & "," & docentry & ")"


        Dim cmd As New SqlCommand(sql1)
        commands.Add(cmd)


        'msql = "UPDATE OINV SET PODNO = CASE WHEN ISNULL(PODNO, '') = ''  THEN '" & Trim(mpodno) & "' ELSE PODNO + '/' + RIGHT('" & Trim(mpodno) & "', 3)   End WHERE DocEntry=" & docentry
        msql = "UPDATE OINV SET U_LRNO = CASE WHEN ISNULL(U_LRNO, '') = ''  THEN '" & Trim(kpodno) & "' ELSE U_LRNO + '/' + RIGHT('" & Trim(kpodno) & "', 3)   End WHERE DocEntry=" & docentry
        'msql = "update oinv set u_process='Y' where docentry in (" & docEntryList & ")"
        Dim cmd1 As New SqlCommand(msql)
        commands.Add(cmd1)



        Dim result As Boolean = ExecuteTransactionWithCommands(commands)

        If result Then
            'MsgBox("All records saved successfully!")
            courprn(docentry, destination, kpodno)
            courstikprint(kpodno, mdate.ToString("dd-MM-yyyy"), destination, wgt, docnum, docentry)

            MsgBox("Saved!")
        Else
            MsgBox("Transaction failed. No data saved.")
        End If


    End Sub

    Private Sub Frmprofcourwgt_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIParent1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        ' Call main()
        loadcourdata()
        optinv.Checked = True
        'Dim width As Integer = CInt(21 / 2.54 * 100)   ' 21 cm
        'Dim height As Integer = CInt(10 / 2.54 * 100)  ' 10 cm

        ''PD.DefaultPageSettings.PaperSize = New PaperSize("CourierSlip", width, height)
        ''PD.DefaultPageSettings.Margins = New Margins(1, 1, 1, 1)
        'Dim ps As New PaperSize("CourierSlip", width, height)
        'With PD.DefaultPageSettings
        '    .PaperSize = ps
        '    .Margins = New Margins(1, 1, 1, 1)
        '    .Landscape = False    ' *** THIS FORCES PORTRAIT ***
        'End With

    End Sub



    Private Sub btnexit_Click(sender As System.Object, e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub Dg_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles Dg.CellFormatting
        'If Dg.Columns(e.ColumnIndex).Name = "btnSave" AndAlso e.RowIndex >= 0 Then

        '    Dim w As Decimal = 0

        '    If Not IsDBNull(Dg.Rows(e.RowIndex).Cells("U_LR_Weight").Value) Then
        '        w = Convert.ToDecimal(Dg.Rows(e.RowIndex).Cells("U_LR_Weight").Value)
        '    End If

        '    ' Enable / Disable button based on LR Weight
        '    If w > 0 Then
        '        Dg.Rows(e.RowIndex).Cells("btnSave").ReadOnly = False
        '        Dg.Rows(e.RowIndex).Cells("btnSave").Style.ForeColor = Color.Black
        '    Else
        '        Dg.Rows(e.RowIndex).Cells("btnSave").ReadOnly = True
        '        Dg.Rows(e.RowIndex).Cells("btnSave").Style.ForeColor = Color.LightGray
        '    End If
        'End If
        '****method 2
        'If e.RowIndex < 0 Then Exit Sub

        'If Dg.Columns(e.ColumnIndex).Name = "btnSave" Then
        '    Dim enable As Boolean = IsSaveEnabledForRow(e.RowIndex)

        '    Dim cell = Dg.Rows(e.RowIndex).Cells("btnSave")

        '    If enable Then
        '        cell.ReadOnly = False
        '        'cell.Style.ForeColor = Color.Black
        '        cell.Style.ForeColor = Color.Green
        '        cell.Style.BackColor = Color.White
        '    Else
        '        cell.ReadOnly = True
        '        'cell.Style.ForeColor = Color.LightGray
        '        cell.Style.ForeColor = Color.Red
        '        cell.Style.BackColor = Color.LightGray
        '    End If
        'End If

    End Sub

    Private Function IsSaveEnabledForRow(rowIndex As Integer) As Boolean
        'Try
        '    If rowIndex < 0 OrElse rowIndex >= Dg.Rows.Count Then Return False
        '    Dim cell = Dg.Rows(rowIndex).Cells("U_LR_Weight")
        '    If cell Is Nothing OrElse IsDBNull(cell.Value) Then Return False
        '    Dim w As Decimal = 0
        '    Decimal.TryParse(cell.Value.ToString(), w)
        '    Return w > 0
        'Catch ex As Exception
        '    Return False
        'End Try

        '***method2

        'Try
        '    If rowIndex < 0 OrElse rowIndex >= Dg.Rows.Count Then Return False

        '    Dim row = Dg.Rows(rowIndex)

        '    Dim bundleNo As String = Trim(row.Cells("U_Nofobun").Value & "")
        '    Dim actualBundleNo As String = Trim(row.Cells("Actbundle").Value & "")
        '    Dim lrNo As String = Trim(row.Cells("U_LRNo").Value & "")
        '    Dim weightValue As String = Trim(row.Cells("U_LR_Weight").Value & "")

        '    ' Weight must be > 0
        '    Dim weight As Decimal = 0
        '    Decimal.TryParse(weightValue, weight)

        '    ' Conditions:
        '    ' 1. BundleNo = ActualBundleNo
        '    ' 2. Weight > 0
        '    ' 3. LRNo must be blank
        '    If bundleNo = actualBundleNo AndAlso weight > 0 AndAlso lrNo = "" Then
        '        Return True
        '    Else
        '        Return False
        '    End If

        '    'Return False

        'Catch ex As Exception
        '    Return False
        'End Try

        '***mehtod3

        Try
            If rowIndex < 0 OrElse rowIndex >= Dg.Rows.Count Then Return False

            Dim row = Dg.Rows(rowIndex)

            Dim bundleNo As String = Trim("" & row.Cells("U_Noofbun").Value)
            Dim actualBundleNo As String = Trim("" & row.Cells("Actbundle").Value)
            Dim lrNo As String = Trim("" & row.Cells("U_LRNo").Value)
            Dim weightValue As String = Trim("" & row.Cells("U_LR_Weight").Value)

            Dim weight As Decimal
            Decimal.TryParse(weightValue, weight)

            Return (Val(bundleNo) = Val(actualBundleNo) AndAlso weight > 0 AndAlso lrNo = "")
            'Return (weight > 0)

        Catch
            Return False
        End Try

    End Function

    Private Sub Dg_CellMouseMove(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Dg.CellMouseMove
        'If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then
        '    Dg.Cursor = Cursors.Default
        '    Return
        'End If

        'If Dg.Columns(e.ColumnIndex).Name = "btnSave" Then
        '    If IsSaveEnabledForRow(e.RowIndex) Then
        '        Dg.Cursor = Cursors.Hand
        '    Else
        '        Dg.Cursor = Cursors.No
        '    End If
        'Else
        '    Dg.Cursor = Cursors.Default
        'End If

        '*** method2
        'If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then
        '    Dg.Cursor = Cursors.Default
        '    Return
        'End If

        'If Dg.Columns(e.ColumnIndex).Name = "btnSave" Then
        '    Dim enabled As Boolean = CBool(Dg.Rows(e.RowIndex).Cells("CanSave").Value)
        '    Dg.Cursor = If(enabled, Cursors.Hand, Cursors.No)
        'Else
        '    Dg.Cursor = Cursors.Default
        'End If

        '** method3
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then
            Dg.Cursor = Cursors.Default
            Return
        End If

        If Dg.Columns(e.ColumnIndex).Name = "btnSave" Then
            Dim enabled As Boolean = SaveEnabledList(e.RowIndex)
            Dg.Cursor = If(enabled, Cursors.Hand, Cursors.No)
        Else
            Dg.Cursor = Cursors.Default
        End If


    End Sub

    Private Sub Dg_CellPainting(sender As Object, e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles Dg.CellPainting
        'If e.RowIndex < 0 Then Return
        'If Dg.Columns(e.ColumnIndex).Name <> "btnSave" Then Return

        'e.PaintBackground(e.CellBounds, True)

        'Dim enabled As Boolean = IsSaveEnabledForRow(e.RowIndex)

        '' Determine button rectangle
        'Dim buttonRect As Rectangle = e.CellBounds
        ''buttonRect.Inflate(-4, -4)
        'buttonRect.Inflate(-2, -2)


        'Dim btnText As String = ""
        'If e.FormattedValue IsNot Nothing Then
        '    btnText = e.FormattedValue.ToString()
        'Else
        '    btnText = "Save"
        'End If


        'If enabled Then
        '    ' Normal button look
        '    ButtonRenderer.DrawButton(e.Graphics, buttonRect, btnText, Dg.Font, False, System.Windows.Forms.VisualStyles.PushButtonState.Normal)
        '    TextRenderer.DrawText(e.Graphics, btnText, Dg.Font, buttonRect, Dg.ForeColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
        'Else
        '    ' Disabled look
        '    ButtonRenderer.DrawButton(e.Graphics, buttonRect, btnText, Dg.Font, False, System.Windows.Forms.VisualStyles.PushButtonState.Disabled)
        '    TextRenderer.DrawText(e.Graphics, btnText, Dg.Font, buttonRect, Color.Gray, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
        'End If

        'e.Handled = True

        '**** method2
        'If e.RowIndex < 0 Then Return
        'If Dg.Columns(e.ColumnIndex).Name <> "btnSave" Then Return

        'e.PaintBackground(e.CellBounds, True)

        'Dim enabled As Boolean = CBool(Dg.Rows(e.RowIndex).Cells("CanSave").Value)

        'Dim buttonRect As Rectangle = e.CellBounds
        'buttonRect.Inflate(-2, -2)

        'Dim btnText As String = If(e.FormattedValue IsNot Nothing, e.FormattedValue.ToString(), "Save")

        'If enabled Then
        '    ButtonRenderer.DrawButton(e.Graphics, buttonRect, btnText, Dg.Font, False, VisualStyles.PushButtonState.Normal)
        '    TextRenderer.DrawText(e.Graphics, btnText, Dg.Font, buttonRect, Dg.ForeColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
        'Else
        '    ButtonRenderer.DrawButton(e.Graphics, buttonRect, btnText, Dg.Font, False, VisualStyles.PushButtonState.Disabled)
        '    TextRenderer.DrawText(e.Graphics, btnText, Dg.Font, buttonRect, Color.Gray, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
        'End If

        'e.Handled = True


        If e.RowIndex < 0 Then Exit Sub

        If Dg.Columns(e.ColumnIndex).Name = "btnSave" Then
            e.Handled = True
            e.PaintBackground(e.ClipBounds, False)

            Dim enabled As Boolean = False
            If SaveEnabledList.ContainsKey(e.RowIndex) Then
                enabled = SaveEnabledList(e.RowIndex)
            End If

            Dim textColor As Color = If(enabled, Color.Green, Color.Red)
            Dim borderColor As Color = If(enabled, Color.DarkGreen, Color.Gray)
            Dim text As String = "Save"

            TextRenderer.DrawText(e.Graphics, text, Dg.Font,
                                  e.CellBounds, textColor,
                                  TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)

            Using p As Pen = New Pen(borderColor)
                e.Graphics.DrawRectangle(p, e.CellBounds.Left, e.CellBounds.Top,
                                         e.CellBounds.Width - 1, e.CellBounds.Height - 1)
            End Using
        End If

    End Sub

    Private Sub courstikprint(podno As String, kdate As String, destination As String, wgt As Double, docnum As Integer, docentry As Integer)
        Dim bundno As String
        Dim mwgt As Double = 0.0
        Dim sql2 As String = ""

        If optdateord.Checked = True Then
            sql2 = "select packagenum,weight from rdln7 where docentry=" & docentry
        ElseIf optinv.Checked = True Then
            sql2 = "select packagenum,weight from rinv7 where docentry=" & docentry
        End If

        Dim dtc As DataTable = getDataTable(sql2)

        Dim prn As String = mlsprinter

        Dim sb As New System.Text.StringBuilder()
        If dtc.Rows.Count > 0 Then
            For Each rw As DataRow In dtc.Rows
                bundno = docnum.ToString("##########0") & "/" & Trim(rw("packagenum").ToString())
                mwgt = Convert.ToDouble(rw("weight"))
                If mos = "WIN" Then
                    sb.AppendLine("<xpml><page quantity='0' pitch='39.0 mm'></xpml>")
                End If

                sb.AppendLine("SIZE 57.5 mm, 39 mm")
                sb.AppendLine("DIRECTION 0,0")
                sb.AppendLine("REFERENCE 0,0")
                sb.AppendLine("OFFSET 0 mm")
                sb.AppendLine("SET PEEL OFF")
                sb.AppendLine("SET CUTTER OFF")
                sb.AppendLine("<xpml></page></xpml><xpml><page quantity='1' pitch='39.0 mm'></xpml>")
                sb.AppendLine("SET TEAR ON")
                sb.AppendLine("CLS")
                sb.AppendLine("CODEPAGE 1252")
                sb.AppendLine("TEXT 426,124,""0"",180,13,11,""" & podno & """")
                sb.AppendLine("BARCODE 429,206,""39"",71,0,180,2,5,""" & podno & """")
                sb.AppendLine("TEXT 163,120,""0"",180,11,10,""" & kdate & """")
                sb.AppendLine("TEXT 435,290,""0"",180,11,9,""To: """)
                sb.AppendLine("TEXT 389,290,""0"",180,11,9,""" & destination & """")
                sb.AppendLine("TEXT 429,254,""0"",180,14,11,""Wt:" & mwgt.ToString("####0.000") & """")
                sb.AppendLine("TEXT 105,243,""0"",180,11,10,""Pcs: 1""")
                sb.AppendLine("TEXT 431,79,""0"",180,10,9,""Cne: RAMRAJ COTTON""")
                'sb.AppendLine("TEXT 398,44,""0"",180,11,9,""TPC/IXM    Ph: 0452-4372244""")
                sb.AppendLine("TEXT 398,44,""0"",180,11,9,""" & Trim(bundno) & """")
                sb.AppendLine("PRINT 1,1")
                If mos = "WIN" Then
                    sb.AppendLine("<xpml></page></xpml><xpml><end/></xpml>")
                End If
            Next
            If mos = "WIN" Then
                RawPrinterHelper.SendStringToPrinter(prn, sb.ToString())
            Else

                Dim dir As String
                dir = System.AppDomain.CurrentDomain.BaseDirectory()

                Dim rawName As String = sb.ToString().Trim()
                Dim rawdata As String = rawName.Replace(vbCrLf, "").Replace(vbLf, "").Replace(vbCr, "")

                Dim fileName As String = "courbundle.txt"

                File.WriteAllText(dir & fileName, sb.ToString().Trim())


                'Dim printer As String = mvertprinter
                ''Dim filePath As String = mlinpath & "nsbarcodEV.txt"
                ''"/home/testing/Desktop/Barcodelinux/nsbarcodEV.txt"
                'Dim filePath As String = mlinpath
                'Dim filePathname As String = mlinpath & fileName

                'Dim psi As New ProcessStartInfo()
                'psi.FileName = "/bin/bash"
                'psi.Arguments = "-c " & """" & filePath & "print_raw.sh '" & printer & "' '" & filePathname & "'"""
                ''psi.Arguments = "-c " & """" & "/home/testing/Desktop/Barcodelinux/print_raw.sh '" & printer & "' '" & filePath & "'"""
                'psi.UseShellExecute = False
                'psi.CreateNoWindow = True
                'Process.Start(psi)

                Dim printer As String = tscprinter1
                Dim filePath As String = mlinpath
                Dim filePathname As String = mlinpath & fileName
                Dim success As Boolean = PrintTscRaw(printer, filePathname)

            End If

        End If
    End Sub
    Private Sub courprn(docentry As Integer, destination As String, podno As String)
        Dim sql3 As String
        mto = destination
        'lpodno = "*" & Trim(podno) & "*"
        lpodno = Trim(podno)
        'sql3 = "select a.CardCode,a.CardFName,a.CardName,b.Building,b.Block,b.Street,b.City,b.ZipCode,b.county,b.country,b.state," _
        '       & " c.CompnyName,d.Building Cbuilding,b.Block Cblock,d.Street cstreet,d.City ccity,d.ZipCode czipcode,d.state cstate,d.county ccounty,d.country ccountry,e.DocNum,e.podno,i.DocDate " _
        '       & " from OCRD A " _
        '       & " inner join CRD1 b on b.CardCode = a.CardCode and b.AdresType ='S' " _
        '       & " inner join OINV i on i.CardCode=a.CardCode " _
        '       & "inner join courier e on e.docentry=i.DocEntry , " _
        '       & " OADM c,ADM1 d " _
        '       & " where e.DocEntry = " & docentry


        sql3 = "select a.CardCode,a.CardFName,a.CardName,b.Building,b.Block,b.Street,b.City,b.ZipCode,b.county,b.country,b.state," _
              & " c.CompnyName,d.Building Cbuilding,b.Block Cblock,d.Street cstreet,d.City ccity,d.ZipCode czipcode,d.state cstate,d.county ccounty,d.country ccountry,i.DocNum,i.DocDate,i.u_noofbun,i.U_LR_Weight " _
              & " from OCRD A " _
              & " inner join CRD1 b on b.CardCode = a.CardCode and b.AdresType ='S' " _
              & " inner join OINV i on i.CardCode=a.CardCode " _
              & " ,OADM c,ADM1 d " _
              & " where i.DocEntry = " & docentry


        Dim dtt As DataTable = getDataTable(sql3)
        If dtt.Rows.Count > 0 Then
            mcardname = Trim(dtt.Rows(0)("cardname"))
            mbuild = Trim(dtt.Rows(0)("building"))
            mblock = Trim(dtt.Rows(0)("block"))
            mstreet = Trim(dtt.Rows(0)("street"))
            mcity = Trim(dtt.Rows(0)("city"))
            mzipcode = Trim(dtt.Rows(0)("zipcode"))
            mdistrict = Trim(dtt.Rows(0)("county"))
            mstate = Trim(dtt.Rows(0)("state"))
            mcountry = Trim(dtt.Rows(0)("country"))
            mdocnum = Trim(dtt.Rows(0)("docnum"))
            mnobund = Trim(dtt.Rows(0)("u_noofbun"))
            mkwgt = Convert.ToDouble(dtt.Rows(0)("U_LR_Weight"))
        Else
            mbuild = ""
            mblock = ""
            mstreet = ""
            mcity = ""
            mzipcode = ""
            mdistrict = ""
            mstate = ""
            mdistrict = ""
            mstate = ""
            mcountry = ""
            mnobund = 0
            mkwgt = 0.0

        End If

        'PrintPreviewDialog1.Document = PD
        'PrintPreviewDialog1.ShowDialog()   ' For preview
        PD.PrinterSettings.PrinterName = mlsprinter

        If PD.PrinterSettings.IsValid = False Then
            MsgBox("Printer not found!", vbExclamation)
            Exit Sub
        End If
        PD.Print()
    End Sub

    Private Sub Btnsave_Click(sender As Object, e As EventArgs) Handles Btnsave.Click

    End Sub

    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        Dim width As Integer = CInt(21 / 2.54 * 100)   ' 21 cm
        Dim height As Integer = CInt(10 / 2.54 * 100)  ' 10 cm

        'PD.DefaultPageSettings.PaperSize = New PaperSize("CourierSlip", width, height)
        'PD.DefaultPageSettings.Margins = New Margins(1, 1, 1, 1)
        'Dim ps As New PaperSize("CourierSlip", width, height)
        Dim ps As New PaperSize("CourierSlip", width, width)
        With PD.DefaultPageSettings
            .PaperSize = ps
            .Margins = New Margins(1, 1, 1, 1)
            .Landscape = False    ' *** THIS FORCES PORTRAIT ***
        End With

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        ' --- FONTS ---
        Dim f10 As New Font("Arial", 9, FontStyle.Regular)
        Dim f12B As New Font("Arial", 9, FontStyle.Bold)
        Dim f9 As New Font("Arial", 9, FontStyle.Bold)
        Dim f14B As New Font("Arial", 10, FontStyle.Bold)

        g.DrawString(Trim(mto), f12B, Brushes.Black, 425, 22)
        g.DrawString(mnobund, f12B, Brushes.Black, 710, 22)
        g.DrawString(DateTime.Now.ToString("dd-MM-yyyy"), f9, Brushes.Black, 210, 23)
        'g.DrawString(Trim(mto), f12B, Brushes.Black, 400, 8)
        'g.DrawString(mnobund, f12B, Brushes.Black, 690, 8)

        ' --- DRAW TEXT IN COORDINATES (X,Y) ---
        ' Left sender block
        g.DrawString("Atithya Clothing Company", f12B, Brushes.Black, 45, 50)
        g.DrawString("(A Unit of ENES Textile Mills)", f10, Brushes.Black, 45, 65)
        g.DrawString("Kovilpappakudi", f10, Brushes.Black, 45, 80)
        g.DrawString("Madurai - 625018", f10, Brushes.Black, 45, 95)
        g.DrawString("TN, IN", f10, Brushes.Black, 45, 110)

        ' From + Date block
        'g.DrawString("From: IXM", f12B, Brushes.Black, 250, 30)

        Dim x As Integer = 370
        Dim y As Integer = 50
        Dim lineGap As Integer = 15       ' gap between lines

        ' Helper function to print only if not empty
        'Dim PrintIfNotEmpty As Action(Of String, Font) =
        '  Sub(value As String, font As Font)
        '      If Not String.IsNullOrWhiteSpace(value) Then
        '          g.DrawString(value, font, Brushes.Black, x, y)
        '          y += lineGap
        '      End If
        '  End Sub

        Dim maxLineLength As Integer = 30

        Dim PrintIfNotEmpty As Action(Of String, Font) =
            Sub(value As String, font As Font)
                If Not String.IsNullOrWhiteSpace(value) Then
                    ' Split the text into chunks of maxLineLength
                    Dim startIndex As Integer = 0
                    While startIndex < value.Length
                        Dim length = Math.Min(maxLineLength, value.Length - startIndex)
                        Dim lineText = value.Substring(startIndex, length)
                        g.DrawString(lineText, font, Brushes.Black, x, y)
                        y += lineGap
                        startIndex += length
                    End While
                End If
            End Sub



        PrintIfNotEmpty(mcardname, f12B)
        PrintIfNotEmpty(mbuild, f10)
        PrintIfNotEmpty(mblock, f10)
        g.DrawString(mkwgt.ToString("####0.000"), f12B, Brushes.Black, 700, 65)
        PrintIfNotEmpty(mstreet, f10)
        PrintIfNotEmpty(Trim(mcity) & " - " & mzipcode, f10)
        PrintIfNotEmpty(mdistrict & "," & mstate & "," & mcountry, f10)

        ' Receiver block

        'g.DrawString(mcardname, f12B, Brushes.Black, 350, 30)
        'g.DrawString(mbuild, f10, Brushes.Black, 350, 45)
        'g.DrawString(mblock, f10, Brushes.Black, 350, 60)
        'g.DrawString(mstreet, f10, Brushes.Black, 350, 75)
        'g.DrawString(Trim(mcity) & " - " & mzipcode, f10, Brushes.Black, 350, 90)
        'g.DrawString(mdistrict & mstate & "," & mcountry, f10, Brushes.Black, 350, 105)

        g.DrawString(DateTime.Now.ToString("HH:mm:ss"), f10, Brushes.Black, 153, 141)
        g.DrawString(DateTime.Now.ToString("dd-MM-yyyy"), f10, Brushes.Black, 258, 141)


        Dim fBarcode As New Font("Code39AzaleaWide2", 24, FontStyle.Regular)
        g.DrawString("*" & lpodno & "*", fBarcode, Brushes.Black, 35, 270)
        ' Barcode number
        g.DrawString(lpodno, f14B, Brushes.Black, 40, 305)
        g.DrawString(mdocnum, f14B, Brushes.Black, 195, 305)


        ' If more pages needed
        e.HasMorePages = False
    End Sub
    Private Function ComputeSaveStatus(row As DataGridViewRow) As Boolean
        Dim bundleNo As String = Trim(row.Cells("U_Nofobun").Value & "")
        Dim actualBundleNo As String = Trim(row.Cells("Actbundle").Value & "")
        Dim lrNo As String = Trim(row.Cells("U_LRNo").Value & "")
        Dim weightValue As String = Trim(row.Cells("U_LR_Weight").Value & "")

        Dim weight As Decimal = 0
        Decimal.TryParse(weightValue, weight)

        Return (bundleNo = actualBundleNo AndAlso weight > 0 AndAlso lrNo = "")
    End Function
   
    Private Sub Dg_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dg.CellValueChanged
        If e.RowIndex < 0 Then Exit Sub

        ' Only recalc when LR Weight or other related columns change
        If Dg.Columns(e.ColumnIndex).Name = "U_LR_Weight" Then

            ' Recalculate save enabled for this row ONLY
            SaveEnabledList(e.RowIndex) = IsSaveEnabledForRow(e.RowIndex)

            ' Refresh button cell
            Dg.InvalidateCell(Dg.Columns("btnSave").Index, e.RowIndex)

        End If

    End Sub

    Private Sub Dg_RowPrePaint(sender As Object, e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles Dg.RowPrePaint
        'If e.RowIndex < 0 Then Exit Sub

        'If Dg.Columns(e.ColumnIndex).Name = "btnSave" Then
        '    Dim enable As Boolean = IsSaveEnabledForRow(e.RowIndex)

        '    Dim cell = Dg.Rows(e.RowIndex).Cells("btnSave")

        '    If enable Then
        '        cell.ReadOnly = False
        '        'cell.Style.ForeColor = Color.Black
        '        cell.Style.ForeColor = Color.Green
        '        cell.Style.BackColor = Color.White
        '    Else
        '        cell.ReadOnly = True
        '        'cell.Style.ForeColor = Color.LightGray
        '        cell.Style.ForeColor = Color.Red
        '        cell.Style.BackColor = Color.LightGray
        '    End If
        'End If


        'If e.RowIndex < 0 Then Exit Sub

        'Dim row = Dg.Rows(e.RowIndex)
        'Dim btnCell = row.Cells("btnSave")

        'Dim canSave As Boolean = (row.Cells("btnSave").Value = "1")

        'If canSave Then
        '    btnCell.ReadOnly = False
        '    btnCell.Style.ForeColor = Color.Green
        '    btnCell.Style.BackColor = Color.White
        'Else
        '    btnCell.ReadOnly = True
        '    btnCell.Style.ForeColor = Color.Red
        '    btnCell.Style.BackColor = Color.LightGray
        'End If


    End Sub
End Class
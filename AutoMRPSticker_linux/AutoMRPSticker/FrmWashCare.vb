Imports System.IO
Imports System.Drawing.Printing
Imports System.Configuration
Imports Microsoft.VisualBasic
Imports BarTender
Imports WashcareLbl.connection
Imports WashcareLbl.Module1
Imports System.Windows.Forms
Public Class FrmWashCare
    Dim mfile, fwash, fsilk, fpant, mprinter, mtype As String
    Dim x, y, n, mqty, lstcol As Integer
    Private rowFilterText As String = String.Empty
    Private rowIndex As Integer = 0

    Private Sub FrmWashCare_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim x As Integer = (Me.ClientSize.Width - GroupBox1.Width) / 2
        Dim y As Integer = (Me.ClientSize.Height - GroupBox1.Height) / 2
        GroupBox1.Location = New Point(x, y)


        Btnclear.PerformClick()
        Call loadprinter()
        mdbserver = System.Configuration.ConfigurationSettings.AppSettings("myservername")
        mdbname = ConfigurationSettings.AppSettings("mydbname")
        mdbuserid = ConfigurationSettings.AppSettings("userid")
        mdbpwd = decodefilesql(ConfigurationSettings.AppSettings("mypwd"))

        fwash = System.Configuration.ConfigurationSettings.AppSettings("Washcarefile")
        fsilk = ConfigurationSettings.AppSettings("Silkfile")
        fpant = ConfigurationSettings.AppSettings("Pantfile")
        mprinter = ConfigurationSettings.AppSettings("Printername")
        cmbprinter.Text = mprinter
    End Sub

    Private Sub loadprinter()
        Dim pkInstalledPrinters As String

        ' Find all printers installed
        For Each pkInstalledPrinters In PrinterSettings.InstalledPrinters
            cmbprinter.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters

        ' Set the combo to the first printer in the list
        cmbprinter.SelectedIndex = -1
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()

    End Sub

    Private Sub washcareprn()
        Dim btapp As New BarTender.Application
        Dim btFormat As BarTender.Format
        'btapp = New BarTender.Application
        mqty = Integer.Parse(txtno.Text.ToString)
        If optwash.Checked = True Then

            'mfile = System.Windows.Forms.Application.StartupPath & "\WASHCARELBLN.btw"
            mfile = System.Windows.Forms.Application.StartupPath & "\" & fwash
            mtype = "Wash"
        ElseIf optsilk.Checked = True Then
            'mfile = System.Windows.Forms.Application.StartupPath & "\WASHCARE SILKNew.btw"
            mfile = System.Windows.Forms.Application.StartupPath & "\" & fsilk
            mtype = "Silk"
        ElseIf optpant.Checked = True Then
            'mfile = System.Windows.Forms.Application.StartupPath & "\PANT LBL New.btw"
            mfile = System.Windows.Forms.Application.StartupPath & "\" & fpant
            mtype = "Pant"
        Else
            mfile = System.Windows.Forms.Application.StartupPath & "\WASHCARELBLN.btw"
            mtype = "Wash"
            'mfile = System.Windows.Forms.Application.StartupPath & "\" & fwash
        End If
        'mfile = Application.StartupPath & "\"
        btFormat = btapp.Formats.Open(mfile, False, "")

        'btFormat = btapp.Formats.Open("d:\Addreslbl2x1.btw", False, "")
        ' btFormat.MeasurementUnits = BarTender.BtUnits.btUnitsMillimeters

        'specify printer. if not, printer specified in format is used.
        If Len(Trim(cmbprinter.Text)) > 0 Then
            btFormat.Printer = cmbprinter.Text
        End If

        'btFormat.Printer = ""
        If mtype = "Wash" Then
            btFormat.SetNamedSubStringValue("UnitCode", txtunit.Text)
            btFormat.SetNamedSubStringValue("CutNo", txtcutno.Text)
            btFormat.SetNamedSubStringValue("RDType", txtrdtype.Text)
            btFormat.SetNamedSubStringValue("YrDate", txtyrdt.Text)
            btFormat.SetNamedSubStringValue("BrandName", txtbrand.Text)
            btFormat.SetNamedSubStringValue("Barcutno", txtcutno.Text)
            btFormat.SetNamedSubStringValue("BrandType", cmbtype.Text)
            'btFormat.IdenticalCopiesOfLabel = Val(txtno.Text)
        ElseIf mtype = "Pant" Then
            btFormat.SetNamedSubStringValue("UnitCode", txtunit.Text)
            btFormat.SetNamedSubStringValue("CutNo", txtcutno.Text)
            'btFormat.SetNamedSubStringValue("RDType", txtrdtype.Text)
            btFormat.SetNamedSubStringValue("YrDate", txtyrdt.Text)
            btFormat.SetNamedSubStringValue("BrandName", txtbrand.Text)
            btFormat.SetNamedSubStringValue("Barcutno", txtcutno.Text)
            'btFormat.IdenticalCopiesOfLabel = Val(txtno.Text)

        ElseIf mtype = "Silk" Then
            btFormat.SetNamedSubStringValue("UnitCode", txtunit.Text)
            btFormat.SetNamedSubStringValue("CutNo", txtcutno.Text)
            btFormat.SetNamedSubStringValue("RDType", txtrdtype.Text)
            btFormat.SetNamedSubStringValue("YrDate", txtyrdt.Text)
            'btFormat.SetNamedSubStringValue("BrandName", txtbrand.Text)
            btFormat.SetNamedSubStringValue("Barcutno", txtcutno.Text)
            'btFormat.IdenticalCopiesOfLabel = Val(txtno.Text)

        End If


        'btFormat.IdenticalCopiesOfLabel = mqty
        btFormat.IdenticalCopiesOfLabel = Val(txtno.Text)



        'Print the document

        'btFormat.PrintOut(False, False)

        'End the BarTender process
        btFormat.PrintOut(False, False)

        btapp.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges)
    End Sub

    Private Sub Btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnclear.Click
        txtbrand.Text = ""
        txtcutno.Text = ""
        txtno.Text = "1"
        txtunit.Text = ""
        txtrdtype.Text = ""

        txtbrand.Visible = False
        txtcutno.Visible = False
        txtno.Visible = False
        txtunit.Visible = False
        txtrdtype.Visible = False
        'txtyrdt.Text = Format(Now, "yy") & Format(Now, "dd")
        txtyrdt.Visible = False


        'txtyrdt.Text = Format(Now, "yy") & Format(Now, "dd")
        txtyrdt.Text = Microsoft.VisualBasic.Format(Now, "yy") & Microsoft.VisualBasic.Format(Now, "MM")
        dg.Rows.Clear()



    End Sub

    Private Sub optsilk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optsilk.CheckedChanged
        'If optsilk.Checked = True Then
        '    txtbrand.Visible = False
        '    ' txtbrand.Visible = True
        '    txtcutno.Visible = True
        '    txtno.Visible = True
        '    txtunit.Visible = True
        '    txtrdtype.Visible = True
        '    'txtyrdt.Text = Format(Now, "yy") & Format(Now, "dd")
        '    txtyrdt.Visible = True
        'End If
    End Sub

    Private Sub optwash_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optwash.CheckedChanged
        'If optwash.Checked = True Then
        '    txtbrand.Visible = True
        '    txtcutno.Visible = True
        '    txtno.Visible = True
        '    txtunit.Visible = True
        '    txtrdtype.Visible = True
        '    'txtyrdt.Text = Format(Now, "yy") & Format(Now, "dd")
        '    txtyrdt.Visible = True
        'End If
    End Sub

    Private Sub optpant_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optpant.CheckedChanged
        'If optpant.Checked = True Then
        '    txtbrand.Visible = False
        '    txtcutno.Visible = True
        '    txtno.Visible = True
        '    txtunit.Visible = True
        '    txtrdtype.Visible = False
        '    'txtyrdt.Text = Format(Now, "yy") & Format(Now, "dd")
        '    txtyrdt.Visible = True
        'End If
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        'Call washcareprn()
        Call washcareprn2()
    End Sub

    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick
        'If e.ColumnIndex = 1 Then
        '    Dim OBJ As New FrmbrandCFL
        '    OBJ.ShowDialog()
        '    If Len(Trim(mbrandgrp)) > 0 Then
        '        dg.Rows(e.RowIndex).Cells(1).Value = mbrandgrp
        '        'dg.Rows(e.RowIndex).Cells(2).Value = mempno
        '        'dg.Rows(e.RowIndex).Cells(3).Value = mempname
        '        'dg.Rows(e.RowIndex).Cells(24).Value = mempsalary
        '        'dg.Rows(e.RowIndex).Cells(26).Value = mjbgrade
        '        dg.CurrentCell = dg.Rows(e.RowIndex).Cells(2)
        '        dg.BeginEdit(False)
        '    End If
        '    OBJ.Close()
        'End If
    End Sub

    Private Sub dg_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellDoubleClick
        'If e.ColumnIndex = 1 Then
        '    Dim OBJ As New FrmbrandCFL
        '    OBJ.ShowDialog()
        '    If Len(Trim(mbrandgrp)) > 0 Then
        '        dg.Rows(e.RowIndex).Cells(1).Value = mbrandgrp
        '        'dg.Rows(e.RowIndex).Cells(2).Value = mempno
        '        'dg.Rows(e.RowIndex).Cells(3).Value = mempname
        '        'dg.Rows(e.RowIndex).Cells(24).Value = mempsalary
        '        'dg.Rows(e.RowIndex).Cells(26).Value = mjbgrade
        '        dg.CurrentCell = dg.Rows(e.RowIndex).Cells(2)
        '        dg.BeginEdit(False)
        '    End If
        '    OBJ.Close()
        'End If
    End Sub

    Private Sub dg_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellEndEdit
        Me.dgvEndEditCell = dg(e.ColumnIndex, e.RowIndex)
        lstcol = dg.Columns.Count - 1
        'If dg.Rows.Count - 1 = e.RowIndex AndAlso e.ColumnIndex <> dg.Columns.Count - 1 Then
        '    dg.CurrentCell = dg(e.ColumnIndex + 1, e.RowIndex)
        'End If
        'dg.EndEdit()

        If e.ColumnIndex = dg.Columns.Count - 1 Then
            If e.ColumnIndex < lstcol Then
                dg.CurrentCell = dg.Rows(dg.CurrentRow.Index + 1).Cells(0)

                dg.EndEdit(False)
               
            End If
            'SendKeys.Send("{ENTER}")
            ' SendKeys.Send("{ENTER}")
        Else
            dg.EndEdit(True)
            'SendKeys.Send("{ENTER}")
            'SendKeys.Send("{UP}")
            'SendKeys.Send("{left}")
        End If


    End Sub

    Private Sub dg_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellEnter
        If e.ColumnIndex > 0 Then


            If e.ColumnIndex = 1 Then
                Dim OBJ As New FrmbrandCFL
                OBJ.ShowDialog()
                If Len(Trim(mbrandgrp)) > 0 Then
                    dg.Rows(e.RowIndex).Cells(1).Value = mbrandgrp
                    'dg.Rows(e.RowIndex).Cells(2).Value = mempno
                    'dg.Rows(e.RowIndex).Cells(3).Value = mempname
                    'dg.Rows(e.RowIndex).Cells(24).Value = mempsalary
                    'dg.Rows(e.RowIndex).Cells(26).Value = mjbgrade
                    'dg.CurrentCell = dg.Rows(e.RowIndex).Cells(2)
                    'dg.BeginEdit(True)
                    SendKeys.Send("{ENTER}")
                    SendKeys.Send("{ENTER}")
                End If
                OBJ.Close()
            End If
            dg.BeginEdit(True)
        End If

    End Sub

    Private Sub dg_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dg.CellMouseUp
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            dg.Rows(e.RowIndex).Selected = True
            Me.rowIndex = e.RowIndex
            Me.dg.CurrentCell = Me.dg.Rows(e.RowIndex).Cells(1)
            Me.ContextMenuStrip1.Show(Me.dg, e.Location)
            ContextMenuStrip1.Show(System.Windows.Forms.Cursor.Position)
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        If Not Me.dg.Rows(Me.rowIndex).IsNewRow Then
            Me.dg.Rows.RemoveAt(Me.rowIndex)
            'Call loadtot()
        End If
    End Sub

    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        n = dg.Rows.Add()
    End Sub

    Private Sub chksel_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chksel.CheckedChanged
        If chksel.Checked = True Then
            'For i As Integer = 0 To dg.RowCount - 1
            '    'For j As Integer = 0 To gv.ColumnCount - 1
            '    If dg.Rows(i).Cells(1).Value = txtno.Text Then
            '        'MsgBox("Item found")
            '        dg.Rows(i).Cells(0).Value = "True"

            '        dg.Sort(dg.Columns(0), System.ComponentModel.ListSortDirection.Descending)
            '        'dg.Item(0, i).Value = "True"
            '        'txtno.Text = ""

            '    End If
            '    'Next
            'Next

            For Each Rw As DataGridViewRow In dg.Rows

                dg.Item(0, Rw.Index).Value = True
                'item(Col.index, Row.index) so you can set value on each cell of the datagrid

            Next

        Else
            'For i As Integer = 0 To dg.RowCount - 1
            '    'For j As Integer = 0 To gv.ColumnCount - 1
            '    If dg.Rows(i).Cells(1).Value = txtno.Text Then
            '        'MsgBox("Item found")
            '        dg.Rows(i).Cells(0).Value = "False"

            '        dg.Sort(dg.Columns(0), System.ComponentModel.ListSortDirection.Descending)
            '        'dg.Item(0, i).Value = "True"
            '        'txtno.Text = ""

            '    End If
            '    'Next
            'Next
            For Each Rw As DataGridViewRow In dg.Rows

                dg.Item(0, Rw.Index).Value = False
                'item(Col.index, Row.index) so you can set value on each cell of the datagrid

            Next
        End If
    End Sub

    Private Sub dg_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dg.CurrentCellDirtyStateChanged
        If Me.dg.IsCurrentCellDirty AndAlso TypeOf Me.dg.CurrentCell Is DataGridViewCheckBoxCell Then
            Me.dg.EndEdit()
        End If
    End Sub

    Private Sub dg_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dg.DataError
        If (e.Context _
                = (DataGridViewDataErrorContexts.Formatting Or DataGridViewDataErrorContexts.PreferredSize)) Then
            e.ThrowException = False
        End If
    End Sub

    Private Sub dg_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dg.EditingControlShowing
        If TypeOf e.Control Is TextBox Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
        End If

        'Dim tb As DataGridViewTextBoxEditingControl = CType(e.Control, DataGridViewTextBoxEditingControl)
        'tb.KeyDown += New KeyEventHandler(dg_KeyPressEvent)
    End Sub

    Private Sub dg_GiveFeedback(ByVal sender As Object, ByVal e As System.Windows.Forms.GiveFeedbackEventArgs) Handles dg.GiveFeedback

    End Sub

    Private Sub dg_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dg.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            'dg.EndEdit(False)
            Dim iColumn As Integer = dg.CurrentCell.ColumnIndex
            Dim iRow As Integer = dg.CurrentCell.RowIndex

            If iColumn = dg.Columns.Count - 1 AndAlso iRow <> dg.Rows.Count - 1 Then
                dg.CurrentCell = dg(0, iRow + 1)
            ElseIf iColumn = dg.Columns.Count - 1 AndAlso iRow = dg.Rows.Count - 1 Then
            Else
                dg.CurrentCell = dg(iColumn + 1, iRow)
            End If
        End If


        If e.KeyCode = Keys.F5 Then
            Dim OBJ As New FrmbrandCFL
            OBJ.ShowDialog()
            If Len(Trim(mbrandgrp)) > 0 Then
                dg.Rows(dg.CurrentCell.RowIndex).Cells(0).Value = mbrandgrp
                dg.CurrentCell = dg.Rows(dg.CurrentCell.RowIndex).Cells(2)
                dg.BeginEdit(False)
            End If
        End If
    End Sub

    Private Sub dg_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dg.KeyPress

    End Sub

    Private Sub dg_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dg.KeyUp
        If e.KeyCode = Keys.F7 Then
            n = dg.Rows.Add
        End If
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Dim dgvEndEditCell As DataGridViewCell
    Private _EnterMoveNext As Boolean = True

    <System.ComponentModel.DefaultValue(True)> _
    Public Property OnEnterKeyMoveNext() As Boolean
        Get
            Return Me._EnterMoveNext
        End Get
        Set(ByVal value As Boolean)
            Me._EnterMoveNext = value
        End Set
    End Property

    Private Sub dg_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dg.SelectionChanged
        If Me._EnterMoveNext AndAlso MouseButtons = 0 Then
            If Me.dgvEndEditCell IsNot Nothing AndAlso dg.CurrentCell IsNot Nothing Then
                If dg.CurrentCell.RowIndex = Me.dgvEndEditCell.RowIndex + 1 _
                    AndAlso dg.CurrentCell.ColumnIndex = Me.dgvEndEditCell.ColumnIndex Then
                    Dim iColNew As Integer
                    Dim iRowNew As Integer
                    If Me.dgvEndEditCell.ColumnIndex >= dg.ColumnCount - 1 Then
                        iColNew = 0
                        iRowNew = dg.CurrentCell.RowIndex
                    Else
                        iColNew = Me.dgvEndEditCell.ColumnIndex + 1
                        iRowNew = Me.dgvEndEditCell.RowIndex
                    End If
                    dg.CurrentCell = dg(iColNew, iRowNew)
                    'SendKeys.Send("{ENTER}")
                    'SendKeys.Send("{ENTER}")
                End If
            End If
            Me.dgvEndEditCell = Nothing
        End If
    End Sub




    Private Sub washcareprn2()

        For i As Integer = 0 To dg.Rows.Count - 1
            Dim c As Boolean
            c = dg.Rows(i).Cells(0).Value
            If c = True Then
                Dim btapp As New BarTender.Application
                Dim btFormat As BarTender.Format
                'btapp = New BarTender.Application
                mqty = Integer.Parse(txtno.Text.ToString)
                If optwash.Checked = True Then

                    'mfile = System.Windows.Forms.Application.StartupPath & "\WASHCARELBLN.btw"
                    mfile = System.Windows.Forms.Application.StartupPath & "\" & fwash
                    mtype = "Wash"
                ElseIf optsilk.Checked = True Then
                    'mfile = System.Windows.Forms.Application.StartupPath & "\WASHCARE SILKNew.btw"
                    mfile = System.Windows.Forms.Application.StartupPath & "\" & fsilk
                    mtype = "Silk"
                ElseIf optpant.Checked = True Then
                    'mfile = System.Windows.Forms.Application.StartupPath & "\PANT LBL New.btw"
                    mfile = System.Windows.Forms.Application.StartupPath & "\" & fpant
                    mtype = "Pant"
                Else
                    mfile = System.Windows.Forms.Application.StartupPath & "\WASHCARELBLN.btw"
                    mtype = "Wash"
                    'mfile = System.Windows.Forms.Application.StartupPath & "\" & fwash
                End If
                'mfile = Application.StartupPath & "\"
                btFormat = btapp.Formats.Open(mfile, False, "")

                'btFormat = btapp.Formats.Open("d:\Addreslbl2x1.btw", False, "")
                ' btFormat.MeasurementUnits = BarTender.BtUnits.btUnitsMillimeters

                'specify printer. if not, printer specified in format is used.
                If Len(Trim(cmbprinter.Text)) > 0 Then
                    btFormat.Printer = cmbprinter.Text
                End If
                txtbrand.Text = dg.Rows(i).Cells(1).Value
                txtunit.Text = dg.Rows(i).Cells(2).Value
                txtcutno.Text = dg.Rows(i).Cells(3).Value
                txtrdtype.Text = dg.Rows(i).Cells(4).Value
                txtyrdt.Text = dg.Rows(i).Cells(5).Value
                txtno.Text = dg.Rows(i).Cells(6).Value
                cmbtype.Text = dg.Rows(i).Cells(7).Value
                'btFormat.Printer = ""
                If mtype = "Wash" Then
                    btFormat.SetNamedSubStringValue("UnitCode", txtunit.Text)
                    btFormat.SetNamedSubStringValue("CutNo", txtcutno.Text)
                    btFormat.SetNamedSubStringValue("RDType", txtrdtype.Text)
                    btFormat.SetNamedSubStringValue("YrDate", txtyrdt.Text)
                    btFormat.SetNamedSubStringValue("BrandName", txtbrand.Text)
                    btFormat.SetNamedSubStringValue("Barcutno", txtcutno.Text)
                    btFormat.SetNamedSubStringValue("BrandType", cmbtype.Text)
                    'btFormat.IdenticalCopiesOfLabel = Val(txtno.Text)
                ElseIf mtype = "Pant" Then
                    btFormat.SetNamedSubStringValue("UnitCode", txtunit.Text)
                    btFormat.SetNamedSubStringValue("CutNo", txtcutno.Text)
                    'btFormat.SetNamedSubStringValue("RDType", txtrdtype.Text)
                    btFormat.SetNamedSubStringValue("YrDate", txtyrdt.Text)
                    ' btFormat.SetNamedSubStringValue("BrandName", txtbrand.Text)
                    btFormat.SetNamedSubStringValue("Barcutno", txtcutno.Text)
                    'btFormat.IdenticalCopiesOfLabel = Val(txtno.Text)

                ElseIf mtype = "Silk" Then
                    btFormat.SetNamedSubStringValue("UnitCode", txtunit.Text)
                    btFormat.SetNamedSubStringValue("CutNo", txtcutno.Text)
                    btFormat.SetNamedSubStringValue("RDType", txtrdtype.Text)
                    btFormat.SetNamedSubStringValue("YrDate", txtyrdt.Text)
                    btFormat.SetNamedSubStringValue("BrandName", txtbrand.Text)
                    btFormat.SetNamedSubStringValue("Barcutno", txtcutno.Text)
                    'btFormat.IdenticalCopiesOfLabel = Val(txtno.Text)

                End If


                'btFormat.IdenticalCopiesOfLabel = mqty
                btFormat.IdenticalCopiesOfLabel = Val(txtno.Text)



                'Print the document

                'btFormat.PrintOut(False, False)

                'End the BarTender process
                btFormat.PrintOut(False, False)

                btapp.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges)
            End If

        Next i


    End Sub



    Private Sub cutterbatcode()
        '        <xpml><page quantity ='0' pitch='45.0 mm'></xpml>
        'Size 69.10 mm, 45 mm
        'DIRECTION 0, 0
        'REFERENCE 0, 0
        'OFFSET 0 mm
        'SPEED 7
        'SET PEEL OFF
        'SET CUTTER ON
        'SET PARTIAL_CUTTER OFF
        '<xpml></page></xpml>

        '<xpml><page quantity ='3' pitch='45.0 mm'></xpml>
        'SET TEAR ON
        'CLS
        '        Bitmap 283, 312, 3, 32, 1, àÿð?ÿøÿüÿþÿÿÿÿÿÿÀÿÿàÿð?ÿøÿøÿ€þÿüÿø?ÿø?ÿø?ÿ€   ø?ÿøÿ€     ÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿÿ
        'CODEPAGE 1252
        'Text 433, 339, "0", 180, 13, 12, "MRP Rs."
        'Text 277, 339, "0", 180, 14, 12, "875.00"
        'Text 538, 291, "0", 180, 11, 12, "IDENTITY 3"
        'Text 538, 258, "0", 180, 9, 9, "COLOUR :"
        'Text 538, 230, "0", 180, 8, 9, "COMMODITY :"
        'Text 439, 258, "ROMAN.TTF", 180, 1, 9, "LT Blue-IC3"
        'Text 410, 230, "0", 180, 9, 9, "SHIRT"
        'Text 367, 189, "0", 180, 10, 8, "Made in India"
        'Text 182, 252, "ROMAN.TTF", 180, 1, 11, "FULL"
        'Text 103, 249, "0", 180, 8, 10, "SLEEVE"
        'Text 182, 213, "ROMAN.TTF", 180, 1, 20, "SIZE:"
        'Text 119, 216, "0", 180, 18, 21, "40"
        'Text 70, 217, "0", 180, 12, 21, "cm"
        'Text 182, 160, "0", 180, 11, 9, "Net Qty :1N"
        'QRCODE 519, 199, L, 3, A, 180, M2, S7, "9802F40IC3"
        'Text 367, 159, "0", 180, 10, 8, "MFG:Nov 2025"
        'Text 322, 131, "0", 180, 18, 12, "*"
        'Text 277, 131, "0", 180, 9, 8, "200"
        'Text 346, 313, "ROMAN.TTF", 180, 1, 7, "(Incl.of all Taxes)"
        'Text 538, 133, "0", 180, 9, 7, "9802F40IC3"
        'Text 464, 107, "0", 180, 8, 7, "* Manufacturer,Marketer & Consumer Care"
        'Text 444, 85, "0", 180, 8, 7, "address details are available in the box"

        'Print 1, 1
        'CUT

        '<xpml></page></xpml>
        '<xpml><end/></xpml>

    End Sub
End Class

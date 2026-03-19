Imports System.IO
Imports System.Math
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports Microsoft.VisualBasic.VBMath
Imports Microsoft.VisualBasic.VbStrConv
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Public Class Frmscanprint

    Private Sub Frmscanprint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        'CLEAR(Me)
        view1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        view1.Show()
        cmdok.PerformClick()


    End Sub

    Private Sub crystalrr()

        Me.Cursor = Cursors.WaitCursor
        Dim cryRpt As New ReportDocument()
        ' Me.view1.Refresh()

        'ants--\\192.166.0.5\b1_shr\Reports

        'cryRpt.Load(Trim(mreppath) & "Company Analysis Report.rpt")

        'If mtik = 0 Then
        cryRpt.Load(Trim(mreppath) & "Scanprint.rpt")
        'ElseIf mtik = 1 Then
        'cryRpt.Load(Trim(mreppath) & "Packing Itemwise.rpt")
        'ElseIf (mtik = 2) Then
        'cryRpt.Load(Trim(mreppath) & "Packing boxwise.rpt")
        'End If





        CrystalReportLogOn(cryRpt, Trim(mkserver), dbnam4, Trim(dbuser), Trim(mkpwd))

        'CrystalReportLogOn(cryRpt, "192.168.0.5", dbnam, "sa", "iTTsA@536")






        cryRpt.SetParameterValue("@Docnum", Val(txtno.Text))
        cryRpt.SetParameterValue("@period", "'" & Trim(cmbyr.Text) & "'")



        Me.view1.ReportSource = cryRpt

        Me.view1.Refresh()

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Call crystalrr()
    End Sub
End Class
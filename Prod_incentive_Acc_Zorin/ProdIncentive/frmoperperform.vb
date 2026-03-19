Public Class frmoperperform

    Private Sub tab1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tab1.Click

    End Sub

    Private Sub tab1_MouseCaptureChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tab1.MouseCaptureChanged

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabc1.SelectedIndexChanged
        If tabc1.SelectedIndex = 2 Then

        End If
    End Sub

    Private Sub frmoperperform_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width

    End Sub
    Private Sub gridhead()

        dv.ColumnCount = 16
        dv.Columns(0).Name = "Sno"
        dv.Columns(1).Name = "Emp.ID"
        dv.Columns(2).Name = "Emp.Name"
        dv.Columns(3).Name = "Operation Name"
        dv.Columns(4).Name = "SAM"
        dv.Columns(5).Name = "Tot.Production"
        dv.Columns(6).Name = "Tot.Min.Wrked"
        dv.Columns(7).Name = "Wait.Time"
        dv.Columns(8).Name = "MAC.Dwn.Time"
        dv.Columns(9).Name = "Emp.Dwn Time"
        dv.Columns(10).Name = "Others"
        dv.Columns(11).Name = "On.Std Time"
        dv.Columns(12).Name = "SAM.Prod."
        dv.Columns(13).Name = "Performance"
        dv.Columns(14).Name = "Efficiency"
        dv.Columns(15).Name = "Utilisation"

        'dv.Columns(8).Name = "DeliveryNo"

        dv.Columns(0).Width = 100
        dv.Columns(1).Width = 250
        dv.Columns(2).Width = 75
        dv.Columns(3).Width = 75
        dv.Columns(4).Width = 100
        dv.Columns(5).Width = 100
        dv.Columns(6).Width = 100
        dv.Columns(7).Width = 100
        dv.Columns(8).Width = 100
        dv.Columns(9).Width = 100
        dv.Columns(10).Width = 100
        dv.Columns(11).Width = 100
        dv.Columns(12).Width = 100
        dv.Columns(13).Width = 100
        dv.Columns(14).Width = 100
        dv.Columns(15).Width = 100


        Dv.ColumnHeadersDefaultCellStyle.Font = New Font(Dv.Font, FontStyle.Bold)
        Dv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

    End Sub

    Private Sub ocmdsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdsave.Click

    End Sub
End Class
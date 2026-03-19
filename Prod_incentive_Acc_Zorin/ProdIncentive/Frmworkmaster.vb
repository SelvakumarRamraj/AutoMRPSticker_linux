Public Class Frmworkmaster

    Private Sub Frmworkmaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        CLEAR(Me)
    End Sub
End Class
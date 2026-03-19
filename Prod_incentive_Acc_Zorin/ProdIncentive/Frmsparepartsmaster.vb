Public Class Frmsparepartsmaster

    Private Sub Frmsparepartsmaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.Height = MDIFORM1.Height
        Me.Width = My.Computer.Screen.Bounds.Width
        CLEAR(Me)
        'Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub butdel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butdel.Click

    End Sub

    Private Sub butedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butedit.Click

    End Sub

    Private Sub butnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butnew.Click

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
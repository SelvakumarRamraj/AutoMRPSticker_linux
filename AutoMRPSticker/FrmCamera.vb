Public Class FrmCamera
    Public WithEvents picCamera As PictureBox

    Public Sub New()
        InitializeComponent()
        picCamera = New PictureBox() With {
            .Dock = DockStyle.Fill,
            .SizeMode = PictureBoxSizeMode.StretchImage
        }
        Me.Controls.Add(picCamera)
        Me.Text = "Camera Preview"
        Me.Size = New Size(640, 480)
    End Sub

    Private Sub FrmCamera_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
    End Sub
End Class
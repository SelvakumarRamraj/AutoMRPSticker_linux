Imports AForge.Video
Imports AForge.Video.DirectShow
Imports ZXing
Imports ZXing.Common
Imports System.Threading
Imports System.Media
Imports System.Drawing
Imports System.Threading.Tasks
Public Class QRScanner800N
    Private videoSource As VideoCaptureDevice
    Private reader As BarcodeReader
    Private decoding As Boolean = False
    Private lastQRCode As String = Nothing
    Private lockObj As New Object()

    Private targetPictureBox As PictureBox
    Private qrDetectedCallback As Action(Of String)

    Private scanInterval As TimeSpan = TimeSpan.FromSeconds(1)
    Private lastScanTime As DateTime = DateTime.MinValue
    Private scanBoxSize As Integer = 800

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="pictureBox">PictureBox for live camera preview</param>
    ''' <param name="qrCallback">Callback to handle scanned QR codes</param>
    Public Sub New(pictureBox As PictureBox, qrCallback As Action(Of String))
        targetPictureBox = pictureBox
        qrDetectedCallback = qrCallback
    End Sub

    ''' <summary>
    ''' Start camera and scanning
    ''' </summary>
    Public Sub StartCamera()
        Try
            StopCamera()

            Dim devices = New FilterInfoCollection(FilterCategory.VideoInputDevice)
            If devices.Count = 0 Then
                MessageBox.Show("No camera found.")
                Return
            End If

            Dim camDevice = devices.Cast(Of FilterInfo).FirstOrDefault(Function(d) Not d.Name.ToLower().Contains("virtual"))
            If camDevice Is Nothing Then camDevice = devices(0)

            videoSource = New VideoCaptureDevice(camDevice.MonikerString)
            If videoSource.VideoCapabilities.Length > 0 Then
                videoSource.VideoResolution = videoSource.VideoCapabilities(0)
            End If

            reader = New BarcodeReader() With {
                .AutoRotate = True,
                .TryInverted = True,
                .Options = New DecodingOptions() With {
                    .TryHarder = True,
                    .PossibleFormats = New List(Of BarcodeFormat) From {BarcodeFormat.QR_CODE}
                }
            }

            AddHandler videoSource.NewFrame, AddressOf CaptureFrame
            videoSource.Start()
        Catch ex As Exception
            MessageBox.Show("Camera start error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Stop camera safely
    ''' </summary>
    Public Sub StopCamera()
        Try
            If videoSource IsNot Nothing Then
                RemoveHandler videoSource.NewFrame, AddressOf CaptureFrame

                Dim vs As VideoCaptureDevice = videoSource
                videoSource = Nothing

                Task.Run(Sub()
                             Try
                                 If vs.IsRunning Then
                                     vs.SignalToStop()
                                     Thread.Sleep(200)
                                     If vs.IsRunning Then vs.Stop()
                                 End If
                             Catch
                             End Try
                         End Sub)
            End If

            If targetPictureBox IsNot Nothing Then
                Try
                    If targetPictureBox.InvokeRequired Then
                        targetPictureBox.Invoke(Sub()
                                                    If targetPictureBox.Image IsNot Nothing Then
                                                        targetPictureBox.Image.Dispose()
                                                        targetPictureBox.Image = Nothing
                                                    End If
                                                End Sub)
                    Else
                        If targetPictureBox.Image IsNot Nothing Then
                            targetPictureBox.Image.Dispose()
                            targetPictureBox.Image = Nothing
                        End If
                    End If
                Catch
                End Try
            End If

            decoding = False
            reader = Nothing
        Catch ex As Exception
            MessageBox.Show("Stop camera error: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Process each frame
    ''' </summary>
    Private Sub CaptureFrame(sender As Object, eventArgs As NewFrameEventArgs)
        Try
            Dim frame As Bitmap = DirectCast(eventArgs.Frame.Clone(), Bitmap)
            Dim displayFrame As Bitmap = DirectCast(frame.Clone(), Bitmap)

            ' Center scan box
            Dim centerX As Integer = displayFrame.Width \ 2
            Dim centerY As Integer = displayFrame.Height \ 2
            Dim left As Integer = Math.Max(centerX - scanBoxSize \ 2, 0)
            Dim top As Integer = Math.Max(centerY - scanBoxSize \ 2, 0)
            Dim boxWidth As Integer = Math.Min(scanBoxSize, displayFrame.Width - left)
            Dim boxHeight As Integer = Math.Min(scanBoxSize, displayFrame.Height - top)
            Dim cropRect As New Rectangle(left, top, boxWidth, boxHeight)

            ' Draw scan box
            Using g As Graphics = Graphics.FromImage(displayFrame)
                g.DrawRectangle(Pens.LimeGreen, cropRect)
            End Using

            ' Crop for decoding
            Dim scanArea As New Bitmap(cropRect.Width, cropRect.Height)
            Using g As Graphics = Graphics.FromImage(scanArea)
                g.DrawImage(frame, 0, 0, cropRect, GraphicsUnit.Pixel)
            End Using

            ' Decode QR in background
            If Not decoding Then
                decoding = True
                ThreadPool.QueueUserWorkItem(Sub()
                                                 Try
                                                     Dim result = reader.Decode(scanArea)
                                                     If result IsNot Nothing Then
                                                         Dim qrText = result.Text.Trim()
                                                         If qrText <> "" AndAlso qrText <> lastQRCode AndAlso DateTime.Now - lastScanTime > scanInterval Then
                                                             SyncLock lockObj
                                                                 lastQRCode = qrText
                                                                 lastScanTime = DateTime.Now

                                                                 ' Trigger callback instead of direct TextBox
                                                                 qrDetectedCallback?.Invoke(qrText)
                                                                 SystemSounds.Beep.Play()
                                                             End SyncLock
                                                         End If
                                                     End If
                                                 Catch
                                                 Finally
                                                     decoding = False
                                                     scanArea.Dispose()
                                                     frame.Dispose()
                                                 End Try
                                             End Sub)
            Else
                scanArea.Dispose()
                frame.Dispose()
            End If

            ' Display live preview
            If targetPictureBox IsNot Nothing Then
                If targetPictureBox.InvokeRequired Then
                    targetPictureBox.Invoke(Sub()
                                                If targetPictureBox.Image IsNot Nothing Then targetPictureBox.Image.Dispose()
                                                targetPictureBox.Image = displayFrame
                                            End Sub)
                Else
                    If targetPictureBox.Image IsNot Nothing Then targetPictureBox.Image.Dispose()
                    targetPictureBox.Image = displayFrame
                End If
            End If

        Catch
        End Try
    End Sub
End Class

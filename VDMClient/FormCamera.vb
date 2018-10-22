Imports System.Threading
Imports Accord.Imaging.Filters
Imports Accord.Video
Imports Accord.Video.DirectShow
Imports Accord.Vision.Detection
Imports Accord.Vision.Detection.Cascades

Public Class FormCamera

    Public Property MainForm As FormMain
    Public MainURL As String = ""

    Private marker As RectanglesMarker
    Private res As ResizeNearestNeighbor = New ResizeNearestNeighbor(200, 200)
    Private resizer As ResizeNearestNeighbor = New ResizeNearestNeighbor(320, 200)
    Private faces As Rectangle() = Nothing
    Public aliasname As String = "You"
    Private videoDevices As FilterInfoCollection
    Private ldevice As String
    Private videoSource As IVideoSource = Nothing
    Private Detector As HaarObjectDetector

    Private DetectCount As Integer = 0

    Private Sub FormCamera_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.BackColor = Color.White

        SetDoubleBuffered(Player)
        SetDoubleBuffered(pbDetected)

        '----------- For Test
        Player.Location = New Point(33, 140)

        Detector = New HaarObjectDetector(New FaceHaarCascade(), 32, ObjectDetectorSearchMode.Average, 1.5F, ObjectDetectorScalingMode.GreaterToSmaller)
        Detector.MaxSize = New Size(320, 320)
        Detector.UseParallelProcessing = True
        Detector.Suppression = 2
        ListLocalCamera()

        ResetCapture()
    End Sub

    Private Sub btnStart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStart.Click
        StartCapture()
    End Sub

    Private Sub Form_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        Player.Dispose()
        Me.Dispose()
    End Sub

    Public Sub ResetCapture()

        'Try
        CloseVideoSource()
        'Catch ex As Exception
        'End Try

        pbStart.Visible = True
        pbGet.Visible = False
        btnOK.Visible = False
        btnAgain.Visible = False

        pbDetected.Visible = False
        Player.Visible = False
        btnStart.Visible = True

        pbDetected.Image = Nothing

        TimerDetected.Stop()
    End Sub

    Public Sub ImageReady()

        Dim Img As Image = pbDetected.Image.Clone
        ResetCapture()

        pbStart.Visible = False
        pbGet.Visible = True
        btnOK.Visible = True
        btnAgain.Visible = True

        pbDetected.Visible = True
        Player.Visible = False
        btnStart.Visible = False

        pbDetected.Image = Img

    End Sub

    Private Sub StartCapture()

        If cmbcamera.Items.Count > 0 Then

            DetectCount = 0

            pbStart.Visible = True
            pbGet.Visible = False
            btnOK.Visible = False
            btnAgain.Visible = False

            pbDetected.Visible = True
            Player.Visible = True
            btnStart.Visible = False

            Player.BringToFront()

            Try
                videoDevices = New FilterInfoCollection(FilterCategory.VideoInputDevice)
                If videoDevices.Count = 0 Then Return
                ldevice = videoDevices(cmbcamera.SelectedIndex).MonikerString
                selectLocalVideoDevice(ldevice)
            Catch Ex As Exception
            End Try

            TimerDetected.Start()
        End If
    End Sub

    Public Shared Sub SetDoubleBuffered(ByVal c As System.Windows.Forms.Control)
        Dim aProp As System.Reflection.PropertyInfo = GetType(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance)
        aProp.SetValue(c, True, Nothing)
    End Sub

    Private Sub ListLocalCamera()
        Try
            videoDevices = New FilterInfoCollection(FilterCategory.VideoInputDevice)
            If videoDevices.Count = 0 Then Return
            For i As Integer = 0 To videoDevices.Count - 1
                cmbcamera.Items.Add(videoDevices(i).Name)
            Next
            cmbcamera.SelectedIndex = 0
        Catch
        End Try
    End Sub

    Private Sub selectLocalVideoDevice(ByVal dev As String)
        Dim videoSource As VideoCaptureDevice = New VideoCaptureDevice(dev)
        videoSource.VideoResolution = selectResolution(videoSource)
        OpenVideoSource(videoSource)
    End Sub

    Private Shared Function selectResolution(ByVal device As VideoCaptureDevice) As VideoCapabilities
        For Each cap In device.VideoCapabilities
            If cap.FrameSize.Width = 1280 Then Return cap
            If cap.FrameSize.Height = 800 Then Return cap
        Next
        Return device.VideoCapabilities.Last()
    End Function

    Private Sub OpenVideoSource(ByVal source As IVideoSource)

        'Try
        CloseVideoSource()
        'Catch ex As Exception
        'End Try

        Player.VideoSource = source
        videoSource = source
        Player.Start()
    End Sub

    Private Sub CloseVideoSource()

        Player.SignalToStop()
        While Player.IsRunning
            Thread.Sleep(100)
        End While
        Player.Stop()

    End Sub

    Private Sub writetoframeimage(ByVal aliasname As String, ByVal image As Bitmap, ByVal linex As Integer, ByVal liney As Integer)

        Dim g As Graphics = Graphics.FromImage(image)
        g.DrawString(aliasname, Me.Font, Brushes.Yellow, New PointF(linex, liney))
        g.Flush()

        DetectCount += 1
    End Sub

    Private Sub Player_NewFrame(sender As Object, ByRef image As Bitmap) Handles Player.NewFrame

        Dim ximage As Bitmap = image
        ximage = resizer.Apply(ximage)
        faces = Detector.ProcessFrame(ximage)
        If faces.Length > 0 Then
            For i = 0 To faces.Length - 1
                faces(i).X -= 10
                faces(i).Y -= 10
                faces(i).Height += 20
                faces(i).Width += 20
                Dim faceimage = New Crop(faces(i)).Apply(ximage)
                faceimage = res.Apply(faceimage)
                pbDetected.Image = faceimage
                writetoframeimage(aliasname, ximage, faces(i).X - 4, faces(i).Y - 14)
            Next
        End If

        marker = New RectanglesMarker(faces)
        marker.MarkerColor = Color.Yellow
        marker.ApplyInPlace(ximage)
        image = ximage
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        MainForm.ChromeBrowser.Load(MainURL)
        MainForm.ClearCamera()
    End Sub

    Private Sub TimerDetected_Tick(sender As Object, e As EventArgs) Handles TimerDetected.Tick
        If DetectCount > 15 Then
            ImageReady()
        End If
    End Sub

    Private Sub btnAgain_Click(sender As Object, e As EventArgs) Handles btnAgain.Click
        StartCapture()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim DetectedImage As Image = pbDetected.Image.Clone
        Dim C As New Converter
        Dim Script As String = "PostCustomerFace('"
        Script &= C.ImageToBlob(DetectedImage)
        Script &= "');"
        MainForm.ChromeBrowser.GetBrowser.MainFrame.ExecuteJavaScriptAsync(Script)
        MainForm.ClearCamera()
    End Sub
End Class
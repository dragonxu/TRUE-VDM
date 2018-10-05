Imports System.Drawing
Public Class Test_DrawImage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        txtImage1.Text = ""
        txtImage2.Text = ""
        txtImage_Merge.Text = ""

        '--ภาพที่ 1
        Dim fs As System.IO.Stream = FileUpload1.PostedFile.InputStream
        Dim br As New System.IO.BinaryReader(fs)
        Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Integer))
        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
        '--แสดงไฟล์ที่อัพโหลด
        txtImage1.Text = base64String
        Image1.ImageUrl = "data:image/png;base64," & base64String
        Image1.Visible = True

        '--ภาพที่ 2
        Dim fs_2 As System.IO.Stream = FileUpload2.PostedFile.InputStream
        Dim br_2 As New System.IO.BinaryReader(fs_2)
        Dim bytes_2 As Byte() = br_2.ReadBytes(CType(fs_2.Length, Integer))
        Dim base64String_2 As String = Convert.ToBase64String(bytes_2, 0, bytes_2.Length)
        '--แสดงไฟล์ที่อัพโหลด
        txtImage2.Text = base64String_2
        Image2.ImageUrl = "data:image/png;base64," & base64String_2
        Image2.Visible = True

        '--Merge image
        Dim C As New Converter
        Dim BL As New VDM_BL

        Dim Merge As Image
        Merge = CombineImages(C.BlobToImage(base64String.ToString()), C.BlobToImage(base64String_2.ToString()))

        '--Save File Merge
        Dim FileName As String = "Temp/Merge/img_merge"
        Dim ExcelPath As String = Server.MapPath(FileName)
        ModuleGlobal.SaveFile(ExcelPath, C.ImageToByte(Merge))
        Dim Merge_bytes As Byte() = C.ImageToByte(Merge)

        Dim Merge_base64String As String = Convert.ToBase64String(Merge_bytes, 0, Merge_bytes.Length)
        txtImage_Merge.Text = Merge_base64String
        Image_Merge.ImageUrl = "data:image/png;base64," & Merge_base64String
        Image_Merge.Visible = True

    End Sub

    Public Function CombineImages(ByVal img1 As Image, ByVal img2 As Image) As Image
        Dim bmp As New Bitmap(Math.Max(img1.Width, img2.Width), img1.Height + img2.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)

        g.DrawImage(img1, 0, 0, img1.Width, img1.Height)
        g.DrawImage(img2, 0, img1.Height, img2.Width, img2.Height)

        Dim theString As String = "ใช้สำหรับลงทะเบียนเบอร์โทรศัพท์ ทรู เท่านั้น" & vbNewLine & "dealer:xx" & vbNewLine & "หมายเลขโทรศัพท์:xxxxxxxxx"
        Dim the_font As Font = New Font("Comic Sans MS", 35, FontStyle.Bold)
        'g.RotateTransform(-45)
        Dim sz As SizeF = g.VisibleClipBounds.Size
        sz = g.MeasureString(theString, the_font)
        g.DrawString(theString, the_font, Brushes.Red, 10, (img1.Height) - 50)
        g.ResetTransform()


        g.Dispose()

        Return bmp
    End Function


End Class
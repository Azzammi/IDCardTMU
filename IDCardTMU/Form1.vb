Imports System.IO
Imports System.Data.OleDb
Imports System.Drawing.Printing

Public Class Form1

    Dim qr As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
    Dim Conn As OleDbConnection
    Dim da As OleDbDataAdapter
    Dim ds As DataSet
    Dim LokasiDB As String
    Dim printed As Boolean = False
    Dim orient As Integer = 0
    Dim pwdt As Integer = 0
    Dim phgt As Integer = 0

    Private Sub PrintPreviewDialog1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles PrintPreviewDialog1.FormClosed
        printed = False
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim x As Integer = 0
        Dim y As Integer = 0
        If orient = 1 Then
            x = 20
            y = 240
        Else
            x = 240
            y = 20
        End If
        Dim bmp As Bitmap = New Bitmap(Panel2.Width, Panel2.Height)
        Panel2.DrawToBitmap(bmp, Panel2.ClientRectangle())
        Dim img As Image = DirectCast(bmp, Image)
        e.Graphics.DrawImage(img, 20, 20)
        Dim bmp2 As Bitmap = New Bitmap(Panel3.Width, Panel3.Height)
        Panel3.DrawToBitmap(bmp2, Panel3.ClientRectangle())
        Dim img2 As Image = DirectCast(bmp2, Image)
        e.Graphics.DrawImage(img2, x, y)
    End Sub

    Private Sub PrintDocument1_EndPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.EndPrint
        If printed And Windows.Forms.DialogResult.Cancel Then
            Dim CMD As OleDbCommand
            'Call koneksi()
            LokasiDB = "Provider=Microsoft.jet.OLEDB.4.0;Data Source=idcardtmu.mdb"
            Conn = New OleDbConnection(LokasiDB)
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            Dim simpan As String = "insert into IDCard(nik, nama) values ('" & TextBox2.Text & "','" & TextBox1.Text & "')"
            CMD = New OleDbCommand(simpan, Conn)
            Try
                'Handle If something Error
                CMD.ExecuteNonQuery()
                MsgBox("Kartu Telah Selesai Dibuat", MsgBoxStyle.Information, "Berhasil")
            Catch
                'If Error Then Do Nothing Lol
            End Try
        Else
            printed = True
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DirectCast(PrintPreviewDialog1, Form).WindowState = FormWindowState.Maximized
        Button7.BackColor = ColorDialog1.Color
        Button8.BackColor = ColorDialog1.Color
        Button9.BackColor = ColorDialog1.Color
        Button10.BackColor = ColorDialog1.Color
        Button11.BackColor = Color.White
    End Sub
    'Nama Karyawan
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Label3.Text = TextBox1.Text
    End Sub
    'NIK
    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
                'MsgBox("Masukan hanya angka !", vbCritical, "Warning ! ! !")
            End If
        End If
    End Sub
    'NIK 
    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Label4.Text = TextBox2.Text
        qr.QRCodeVersion = 2
        Try
            PictureBox3.Image = qr.Encode(TextBox2.Text)
        Catch
            MsgBox("Panjang NIK melebihi 27 Karakter!", vbCritical, "Error")
        End Try
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Label8.Text = TextBox3.Text
        Label6.Text = TextBox3.Text
        Label6.TextAlign = HorizontalAlignment.Center
        Label8.TextAlign = HorizontalAlignment.Center
    End Sub

    'Portrait
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        orient = 0
        Panel2.Width = 204
        Panel2.Height = 325
        Panel3.Width = 204
        Panel3.Height = 325
        Panel2.Location = New Point(88, 55)
        Panel3.Location = New Point(333, 55)
        PictureBox1.Location = New Point(60, 126)
        PictureBox2.Location = New Point(14, 11)
        PictureBox2.Width = 179
        PictureBox2.Height = 45
        PictureBox3.Location = New Point(52, 200)
        PictureBox4.Location = New Point(14, 11)
        Label3.Location = New Point(7, 242)
        Label3.Width = 190
        Label3.Height = 48
        Label3.TextAlign = ContentAlignment.BottomCenter
        Label4.Location = New Point(11, 297)
        Label4.Width = 182
        Label4.Height = 20
        Label4.TextAlign = ContentAlignment.TopCenter
        Label7.Location = New Point(14, 81)
        pwdt = Panel2.Width
        phgt = Panel2.Height
    End Sub
    'Landscape
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        orient = 1
        Panel2.Width = 325
        Panel2.Height = 204
        Panel3.Width = 325
        Panel3.Height = 204
        Panel2.Location = New Point(130, 8)
        Panel3.Location = New Point(130, 218)
        PictureBox1.Location = New Point(12, 79)
        PictureBox2.Location = New Point(11, 11)
        PictureBox2.Width = 186
        PictureBox2.Height = 45
        PictureBox3.Location = New Point(11, 92)
        PictureBox4.Location = New Point(11, 11)
        Label3.Location = New Point(100, 79)
        Label3.Width = 214
        Label3.Height = 48
        Label3.TextAlign = ContentAlignment.BottomLeft
        Label4.Location = New Point(101, 127)
        Label4.Width = 175
        Label4.Height = 20
        Label4.TextAlign = ContentAlignment.TopLeft
        Label7.Location = New Point(130, 88)
        pwdt = Panel2.Width
        phgt = Panel2.Height
    End Sub
    'Add Background
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If OFGSelectImage.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fs As System.IO.FileStream
            ' Specify a valid picture file path on your computer.
            fs = New System.IO.FileStream(OFGSelectImage.FileName,
                 IO.FileMode.Open, IO.FileAccess.Read)
            fs.Close()
            Dim bgImg As Image = Image.FromFile(OFGSelectImage.FileName)
            Button11.BackColor = Color.White
            Panel2.BackColor = Color.White
            Panel3.BackColor = Color.White
            Panel2.BackgroundImage = bgImg
            Panel3.BackgroundImage = bgImg
        End If
    End Sub
    ' Bg Color
    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        If ColorDialog3.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Button11.BackColor = ColorDialog3.Color
            Panel2.BackgroundImage = Nothing
            Panel3.BackgroundImage = Nothing
            Panel2.BackColor = ColorDialog3.Color
            Panel3.BackColor = ColorDialog3.Color
        End If
    End Sub

    'Color Picker
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If ColorDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Button7.BackColor = ColorDialog1.Color
            Label3.ForeColor = ColorDialog1.Color
        End If
    End Sub
    'Color Picker
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        If ColorDialog2.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Button8.BackColor = ColorDialog2.Color
            Label4.ForeColor = ColorDialog2.Color
        End If
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        If ColorDialog4.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Button10.BackColor = ColorDialog4.Color
            Label7.ForeColor = ColorDialog4.Color
        End If
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        If ColorDialog5.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Button9.BackColor = ColorDialog5.Color
            Label6.ForeColor = ColorDialog5.Color
            Label8.ForeColor = ColorDialog5.Color
        End If
    End Sub

    'Add Photo
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fs As System.IO.FileStream
            ' Specify a valid picture file path on your computer.
            fs = New System.IO.FileStream(OpenFileDialog1.FileName,
                 IO.FileMode.Open, IO.FileAccess.Read)
            fs.Close()
            Dim Img As Image = Image.FromFile(OpenFileDialog1.FileName)
            PictureBox1.Image = Img
        End If
    End Sub
    'Add Logo
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If OpenFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fs As System.IO.FileStream
            ' Specify a valid picture file path on your computer.
            fs = New System.IO.FileStream(OpenFileDialog2.FileName,
                 IO.FileMode.Open, IO.FileAccess.Read)
            fs.Close()
            Dim Img As Image = Image.FromFile(OpenFileDialog2.FileName)
            PictureBox2.Image = Img
            PictureBox4.Image = Img
        End If
    End Sub
    'Print
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If TextBox1.Text <> "" Or TextBox2.Text <> "" Then
            PrintPreviewDialog1.ShowDialog()
        Else
            MsgBox("Beberapa input belum diisi.")
        End If
    End Sub

    Private Sub Button10_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button10.MouseHover
        ToolTip1.Show("Keterangan", Button10)
    End Sub

End Class
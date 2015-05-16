Public Class Form2
    Public a() As String
    Public actual As Integer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        d.ShowDialog()
        Dim nombre As String = d.SafeFileName
        Try
            PictureBox1.Image = Image.FromFile(d.FileName)
            DividirImagen(nombre, PictureBox1.Image, 5, 5)
            If IO.File.Exists(Mid(nombre, 1, Len(nombre) - 4) & "\Puzzle.txt") Then
                Dim i As Integer
                For i = 0 To 23
                    Form1.botones(i).Image = Image.FromFile(Mid(nombre, 1, Len(nombre) - 4) & "\" & Mid(nombre, 1, Len(nombre) - 4) & i & ".bmp")
                Next
                MsgBox("La imagen se cargó exitosamente.!", vbInformation)
            End If
        Catch

        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If IO.File.Exists(SacarNombre(a(actual)) & "\Puzzle.txt") Then
            Dim i As Integer
            PictureBox1.Image = Image.FromFile(SacarNombre(a(actual)) & "\" & SacarNombre(a(actual)) & ".bmp")
            For i = 0 To 23
                Form1.botones(i).Image = Image.FromFile(SacarNombre(a(actual)) & "\" & SacarNombre(a(actual)) & i & ".bmp")
            Next
            MsgBox("La imagen se cargó exitosamente.!", vbInformation)
        End If
    End Sub

    Public Function SacarNombre(ByVal path As String) As String
        Dim i As Integer
        For i = Len(path) To 1 Step -1
            If Mid(path, i, 1) = "\" Then
                Exit For
            End If
        Next
        Return Mid(path, i + 1, Len(path) - i)
    End Function

    Public Sub DividirImagen(ByVal filename As String, ByVal imagen As Bitmap, ByVal px As Integer, ByVal py As Integer)

        Dim i, j, cont As Integer
        cont = 0

        imagen = RedimImagen(imagen, 400, 400)

        Dim part As Bitmap
        part = New Bitmap(imagen.Width \ px, imagen.Height \ py)

        Try
            MkDir(Mid(filename, 1, Len(filename) - 4))
            imagen.Save(Mid(filename, 1, Len(filename) - 4) & "\" & Mid(filename, 1, Len(filename) - 4) & ".bmp")
        Catch ex As Exception

        End Try

        System.IO.File.Create(Mid(filename, 1, Len(filename) - 4) & "\Puzzle.txt")

        For j = 0 To imagen.Height - 1 Step imagen.Height \ py
            For i = 0 To imagen.Width - 1 Step imagen.Width \ px
                For x = 0 To imagen.Width \ px - 1
                    For y = 0 To imagen.Height \ py - 1
                        part.SetPixel(x, y, imagen.GetPixel(i + x, j + y))
                    Next
                Next
                part.Save(Mid(filename, 1, Len(filename) - 4) & "\" & Mid(filename, 1, Len(filename) - 4) & cont & ".bmp")
                cont += 1
            Next
        Next
    End Sub

    Public Function RedimImagen(ByVal imagen As Bitmap, ByVal Width As Integer, ByVal Height As Integer) As Bitmap
        Dim img = New Bitmap(Width, Height)

        Using gr As Graphics = Graphics.FromImage(img)
            gr.DrawImage(imagen, 0, 0, img.Width, img.Height)
        End Using

        Return img
    End Function

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim encontrado = False
        While encontrado = False
            actual -= 1
            If actual < 0 Then
                actual = a.Count - 1
            End If
            If IO.File.Exists(SacarNombre(a(actual)) & "\" & SacarNombre(a(actual) & ".bmp")) Then
                encontrado = True
            End If
        End While
        PictureBox1.Image = Image.FromFile(SacarNombre(a(actual)) & "\" & SacarNombre(a(actual)) & ".bmp")
    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        a = IO.Directory.EnumerateDirectories(Application.StartupPath).ToArray()
        PictureBox1.Image = Image.FromFile(SacarNombre(a(0)) & "\" & SacarNombre(a(0)) & ".bmp")
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim encontrado = False
        While encontrado = False
            actual += 1
            If actual > a.Count - 1 Then
                actual = 0
            End If
            If IO.File.Exists(SacarNombre(a(actual)) & "\" & SacarNombre(a(actual) & ".bmp")) Then
                encontrado = True
            End If
        End While

        PictureBox1.Image = Image.FromFile(SacarNombre(a(actual)) & "\" & SacarNombre(a(actual)) & ".bmp")
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        a = IO.Directory.EnumerateDirectories(Application.StartupPath).ToArray()
    End Sub
End Class
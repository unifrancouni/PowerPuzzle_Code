Public Class imagen
    Public a() As String
    Public actual As Integer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        d.ShowDialog()
        Dim nombre As String = d.SafeFileName
        Try
            Dim img As New Bitmap(d.FileName)
            DividirImagen(nombre, img, puzzle.N + 1, puzzle.N + 1)
            ActualizarLista()
            Me.RecreateHandle()
        Catch

        End Try

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

        imagen = RedimImagen(imagen, 400, 400)

        Dim part As Bitmap
        part = New Bitmap(imagen.Width \ px, imagen.Height \ py)
        Dim rectPart As New Rectangle(0, 0, imagen.Width \ px, imagen.Height \ py)
        Dim rectorig As Rectangle
        Dim g = Graphics.FromImage(part)

        Try
            MkDir(Mid(filename, 1, Len(filename) - 4) & puzzle.N + 1 & "x" & puzzle.N + 1)
            imagen.Save(Mid(filename, 1, Len(filename) - 4) & puzzle.N + 1 & "x" & puzzle.N + 1 & "\" & Mid(filename, 1, Len(filename) - 4) & puzzle.N + 1 & "x" & puzzle.N + 1 & ".bmp")

            System.IO.File.Create(Mid(filename, 1, Len(filename) - 4) & puzzle.N + 1 & "x" & puzzle.N + 1 & "\Puzzle.txt")

            Dim c As Integer = 0
            Dim X, Y As Integer
            X = 0
            Y = 0
            For cont As Integer = 0 To puzzle.botones.Length - 1
                rectorig = New Rectangle(X, Y, imagen.Width \ px, imagen.Height \ py)
                part = New Bitmap(imagen.Width \ px, imagen.Height \ py)
                g = Graphics.FromImage(part)
                g.DrawImage(imagen, rectPart, rectorig, GraphicsUnit.Pixel)
                part = RedimImagen(part, 80, 80)
                part.Save(Mid(filename, 1, Len(filename) - 4) & puzzle.N + 1 & "x" & puzzle.N + 1 & "\" & Mid(filename, 1, Len(filename) - 4) & puzzle.N + 1 & "x" & puzzle.N + 1 & cont & ".bmp")
                c += 1
                X += imagen.Width \ px
                If c >= px Then
                    c = 0
                    X = 0
                    Y += imagen.Height \ py
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function RedimImagen(ByVal imagen As Bitmap, ByVal Width As Integer, ByVal Height As Integer) As Bitmap
        Dim img = New Bitmap(Width, Height)

        Using gr As Graphics = Graphics.FromImage(img)
            gr.DrawImage(imagen, 0, 0, img.Width, img.Height)
        End Using

        Return img
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        On Error Resume Next
        ActualizarLista()
        If IO.File.Exists(SacarNombre(a(actual)) & "\Puzzle.txt") Then
            Dim i As Integer
            For i = 0 To puzzle.botones.Length - 1
                puzzle.botones(i).Image = Image.FromFile(SacarNombre(a(actual)) & "\" & SacarNombre(a(actual)) & i & ".bmp")
            Next
            MsgBox("La imagen se cargó exitosamente.!", vbInformation)
        End If

        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'On Error Resume Next
        Dim encontrado = False
        While encontrado = False
            actual -= 1
            If actual < 0 Then
                actual = a.Count - 1
            End If
            If IO.File.Exists(SacarNombre(a(actual)) & "\" & SacarNombre(a(actual) & ".bmp")) Then
                Dim carp As String = Mid(SacarNombre(a(actual)), Len(SacarNombre(a(actual))), 1)
                If carp = CStr(puzzle.N + 1) Then
                    encontrado = True
                End If
            End If
        End While
        PictureBox1.Image = Image.FromFile(SacarNombre(a(actual)) & "\" & SacarNombre(a(actual)) & ".bmp")
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'On Error Resume Next
        Dim encontrado = False
        While encontrado = False
            actual += 1
            If actual > a.Count - 1 Then
                actual = 0
            End If
            If IO.File.Exists(SacarNombre(a(actual)) & "\" & SacarNombre(a(actual) & ".bmp")) Then
                Dim carp As String = Mid(SacarNombre(a(actual)), Len(SacarNombre(a(actual))), 1)
                If carp = CStr(puzzle.N + 1) Then
                    encontrado = True
                End If
            End If
        End While
        PictureBox1.Image = Image.FromFile(SacarNombre(a(actual)) & "\" & SacarNombre(a(actual)) & ".bmp")
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        ActualizarLista()
    End Sub

    Public Sub ActualizarLista()
        a = IO.Directory.EnumerateDirectories(Application.StartupPath).ToArray()
    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActualizarLista()
        actual = 0
    End Sub
End Class
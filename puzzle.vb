Public Class puzzle
    'controla tamaño de tablero
    Public N As Integer = 2
    'controla el hueco, y los minimos y maximos en el tablero
    Private Xhueco, Yhueco, MinX, MaxX, MinY, MaxY As Integer
    'variable q contiene las piezas y su contador
    Public botones(0 To ((N + 1) * (N + 1)) - 2) As Button
    Private nom As Integer
    'variables para interfaz
    Private dentro As Boolean = False
    Private alternate As Boolean = False
    Private juegoiniciado As Boolean = False
    'controla el nodo actual
    Private NodoActual(N, N) As Integer
    'almacena movimientos
    Private pila As New Stack(Of Integer)()

    Public Sub BotonesDinamicos(ByVal indice As Integer, ByVal x As Integer, ByVal y As Integer)
        botones(indice) = New Button()
        botones(indice).Text = "" & (nom + 1)
        botones(indice).ForeColor = Color.Red
        botones(indice).Location = New Point(x, y)
        botones(indice).Size = New Size(80, 80)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MinX = 0         'minimo boton en x
        MaxX = 240 - 80 'maximo boton en x
        MinY = 24       'minimo boton en y
        MaxY = 24 + 240 - 80 'maximo boton en y

        Xhueco = 0 + N * 80
        Yhueco = 24 + N * 80
        nom = 0 'cuenta la cantidad de botones

        For i = 0 To N
            For j = 0 To N
                If i <> N Or j <> N Then
                    BotonesDinamicos(nom, j * 80, 24 + i * 80)
                    nom += 1
                End If
            Next
        Next

        For i = 0 To ((N + 1) * (N + 1)) - 2
            AddHandler botones(i).Click, AddressOf Botones_Click
        Next

        Me.Controls.AddRange(botones.ToArray)

        ActualizarNodoactual()

        Arbol.Location = New Point(MaxX + 85, Arbol.Location.Y)
        ListBox1.Location = New Point(MaxX + 85, ListBox1.Location.Y)
        Me.Width = MaxX + 1.5 * 82
    End Sub

    Private Sub Botones_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If juegoiniciado = True Then
            Dim index As Integer = SacarIndice(sender)
            index = index - 1

            If MoverPieza(botones(index)) Then
                pila.Push(index)
            End If
        End If
        ActualizarNodoactual()
    End Sub

    Public Function SacarIndice(ByVal sender As Object) As Integer
        Dim s, si As String
        Dim enc As Integer

        s = sender.ToString

        For i = 1 To Len(s)
            If Mid(s, i, 6) = "Text: " Then
                enc = i
                Exit For
            End If
        Next
        si = Mid(s, enc + 6, Len(s) - enc)
        Return Integer.Parse(si)
    End Function

    Private Sub CargarImagenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CargarImagenToolStripMenuItem.Click
        imagen.Show()
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        If alternate = False Then
            'For i = Me.Width To Me.Width + TreeView1.Width + 10 Step 15
            Me.Width = Me.Width + Arbol.Width + 10
            Me.Refresh()
            'Next
            PictureBox1.Image = Image.FromFile("atras.png")
            alternate = True
        Else
            'For i = Me.Width To Me.Width - TreeView1.Width - 10 Step -15
            Me.Width = Me.Width - Arbol.Width - 10
            Me.Refresh()
            'Next
            PictureBox1.Image = Image.FromFile("sigue.png")
            alternate = False
        End If
    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        Dim col As Color
        col = PictureBox1.BackColor
        PictureBox1.BackColor = ColorTranslator.FromWin32(RGB(col.R - 10, col.G - 10, col.B - 10))
    End Sub

    Private Sub PictureBox1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseLeave
        Dim col As Color
        If dentro = True Then
            col = PictureBox1.BackColor
            PictureBox1.BackColor = ColorTranslator.FromWin32(RGB(col.R + 10, col.G + 10, col.B + 10))
            dentro = False
        End If
    End Sub

    Private Sub PictureBox1_MouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        Dim col As Color
        If dentro = False Then
            col = PictureBox1.BackColor
            PictureBox1.BackColor = ColorTranslator.FromWin32(RGB(col.R - 10, col.G - 10, col.B - 10))
            dentro = True
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        Dim col As Color
        col = PictureBox1.BackColor
        PictureBox1.BackColor = ColorTranslator.FromWin32(RGB(col.R + 10, col.G + 10, col.B + 10))
    End Sub

    'funciones basadas realmente

    Private Overloads Function EsEsquinero(ByVal XHueco As Integer, ByVal YHueco As Integer) As Boolean
        If ((XHueco = MinX Or XHueco = MaxX) And (YHueco = MinY Or YHueco = MaxY)) Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Overloads Function EsBanda(ByVal XHueco As Integer, ByVal YHueco As Integer) As Boolean
        If ((YHueco = MinY Or YHueco = MaxY Or XHueco = MinX Or XHueco = MaxX)) Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Overloads Function EsMedio(ByVal XHueco As Integer, ByVal YHueco As Integer) As Boolean
        If Not EsEsquinero(XHueco, YHueco) And Not EsBanda(XHueco, YHueco) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Overloads Function PosiblesMov(ByVal XHueco As Integer, ByVal YHueco As Integer) As Integer
        Dim Result As Integer = 0
        If EsEsquinero(XHueco, YHueco) Then
            Result = 2
        ElseIf EsBanda(XHueco, YHueco) And Not EsEsquinero(XHueco, YHueco) Then
            Result = 3
        ElseIf EsMedio(XHueco, YHueco) Then
            Result = 4
        End If
        Return Result
    End Function
    Public Overloads Function Cuales(ByVal XHueco As Integer, ByVal YHueco As Integer) As ArrayList
        Dim a As New ArrayList(PosiblesMov(XHueco, YHueco))

        If EsEsquinero(XHueco, YHueco) Then
            If XHueco = MinX Then
                a.Add(ContiguoDerecho(XHueco, YHueco))
            ElseIf XHueco = MaxX Then
                a.Add(ContiguoIzquierdo(XHueco, YHueco))
            End If
            If YHueco = MinY Then
                a.Add(ContiguoAbajo(XHueco, YHueco))
            ElseIf YHueco = MaxY Then
                a.Add(ContiguoArriba(XHueco, YHueco))
            End If
        ElseIf EsBanda(XHueco, YHueco) And Not EsEsquinero(XHueco, YHueco) Then
            If YHueco = MinY Then
                a.Add(ContiguoDerecho(XHueco, YHueco))
                a.Add(ContiguoIzquierdo(XHueco, YHueco))
                a.Add(ContiguoAbajo(XHueco, YHueco))
            ElseIf YHueco = MaxY Then
                a.Add(ContiguoDerecho(XHueco, YHueco))
                a.Add(ContiguoIzquierdo(XHueco, YHueco))
                a.Add(ContiguoArriba(XHueco, YHueco))
            End If
            If XHueco = MinX Then
                a.Add(ContiguoDerecho(XHueco, YHueco))
                a.Add(ContiguoArriba(XHueco, YHueco))
                a.Add(ContiguoAbajo(XHueco, YHueco))
            ElseIf XHueco = MaxX Then
                a.Add(ContiguoIzquierdo(XHueco, YHueco))
                a.Add(ContiguoArriba(XHueco, YHueco))
                a.Add(ContiguoAbajo(XHueco, YHueco))
            End If
        ElseIf EsMedio(XHueco, YHueco) Then
            a.Add(ContiguoDerecho(XHueco, YHueco))
            a.Add(ContiguoIzquierdo(XHueco, YHueco))
            a.Add(ContiguoArriba(XHueco, YHueco))
            a.Add(ContiguoAbajo(XHueco, YHueco))
        End If

        Return (a)
    End Function

    Public Overloads Function ContiguoDerecho(ByVal XHueco As Integer, ByVal YHueco As Integer) As Integer
        Return IndiceBotonEn(XHueco + 80, YHueco)
    End Function
    Public Overloads Function ContiguoIzquierdo(ByVal XHueco As Integer, ByVal YHueco As Integer) As Integer
        Return IndiceBotonEn(XHueco - 80, YHueco)
    End Function
    Public Overloads Function ContiguoArriba(ByVal XHueco As Integer, ByVal YHueco As Integer) As Integer
        Return IndiceBotonEn(XHueco, YHueco - 80)
    End Function
    Public Overloads Function ContiguoAbajo(ByVal XHueco As Integer, ByVal YHueco As Integer) As Integer
        Return IndiceBotonEn(XHueco, YHueco + 80)
    End Function

    'fin funciones basadas realmente
    'funciones basadas virtualmente

    Private Overloads Function EsEsquinero(ByVal nodo As Integer(,)) As Boolean
        Dim i, j As Integer
        For i = 0 To N
            For j = 0 To N
                If nodo(j, i) = 0 Then
                    GoTo Encontrado
                End If
            Next
        Next
Encontrado:
        If (i = 0 And (j = 0 Or j = N)) Or (i = N And (j = 0 Or j = N)) Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Overloads Function EsBanda(ByVal nodo As Integer(,)) As Boolean
        Dim i, j As Integer
        For i = 0 To N
            For j = 0 To N
                If nodo(j, i) = 0 Then
                    GoTo Encontrado
                End If
            Next
        Next
Encontrado:
        If (i = 0 Or j = 0 Or i = N Or j = N) Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Overloads Function EsMedio(ByVal nodo As Integer(,)) As Boolean
        If Not EsEsquinero(nodo) And Not EsBanda(nodo) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Overloads Function PosiblesMov(ByVal nodo As Integer(,)) As Integer
        Dim Result As Integer = 0
        If EsEsquinero(nodo) Then
            Result = 2
        ElseIf EsBanda(nodo) And Not EsEsquinero(Xhueco, Yhueco) Then
            Result = 3
        ElseIf EsMedio(nodo) Then
            Result = 4
        End If
        Return Result
    End Function
    Public Overloads Function Cuales(ByVal nodo As Integer(,)) As ArrayList
        Dim i, j As Integer
        For i = 0 To N
            For j = 0 To N
                If nodo(i, j) = 0 Then
                    GoTo Encontrado
                End If
            Next
        Next
Encontrado:

        Dim a As New ArrayList(PosiblesMov(nodo))

        If EsEsquinero(nodo) Then
            If i = 0 Then
                a.Add(ContiguoAbajo(nodo))
            ElseIf i = N Then
                a.Add(ContiguoArriba(nodo))
            End If
            If j = 0 Then
                a.Add(ContiguoDerecho(nodo))
            ElseIf j = N Then
                a.Add(ContiguoIzquierdo(nodo))
            End If
        ElseIf EsBanda(nodo) And Not EsEsquinero(nodo) Then
            If j = 0 Then
                a.Add(ContiguoArriba(nodo))
                a.Add(ContiguoAbajo(nodo))
                a.Add(ContiguoDerecho(nodo))
            ElseIf j = N Then
                a.Add(ContiguoArriba(nodo))
                a.Add(ContiguoAbajo(nodo))
                a.Add(ContiguoIzquierdo(nodo))
            End If
            If i = 0 Then
                a.Add(ContiguoDerecho(nodo))
                a.Add(ContiguoIzquierdo(nodo))
                a.Add(ContiguoAbajo(nodo))
            ElseIf i = N Then
                a.Add(ContiguoDerecho(nodo))
                a.Add(ContiguoIzquierdo(nodo))
                a.Add(ContiguoArriba(nodo))
            End If
        ElseIf EsMedio(nodo) Then
            a.Add(ContiguoArriba(nodo))
            a.Add(ContiguoAbajo(nodo))
            a.Add(ContiguoDerecho(nodo))
            a.Add(ContiguoIzquierdo(nodo))
        End If

        Return (a)
    End Function

    Public Overloads Function ContiguoDerecho(ByVal nodo As Integer(,)) As Integer
        Dim i, j As Integer
        For i = 0 To N
            For j = 0 To N
                If nodo(j, i) = 0 Then
                    GoTo Encontrado
                End If
            Next
        Next
Encontrado:

        Return nodo(j, i + 1)
    End Function
    Public Overloads Function ContiguoIzquierdo(ByVal nodo As Integer(,)) As Integer
        Dim i, j As Integer
        For i = 0 To N
            For j = 0 To N
                If nodo(j, i) = 0 Then
                    GoTo Encontrado
                End If
            Next
        Next
Encontrado:
        Return nodo(j, i - 1)
    End Function
    Public Overloads Function ContiguoArriba(ByVal nodo As Integer(,)) As Integer
        Dim i, j As Integer
        For i = 0 To N
            For j = 0 To N
                If nodo(j, i) = 0 Then
                    GoTo Encontrado
                End If
            Next
        Next
Encontrado:
        Return nodo(j - 1, i)
    End Function
    Public Overloads Function ContiguoAbajo(ByVal nodo As Integer(,)) As Integer
        Dim i, j As Integer
        For i = 0 To N
            For j = 0 To N
                If nodo(j, i) = 0 Then
                    GoTo Encontrado
                End If
            Next
        Next
Encontrado:
        Return nodo(j + 1, i)
    End Function

    'fin funciones basadas virtualmente


    Public Function IndiceBotonEn(ByVal X As Integer, ByVal Y As Integer) As Integer
        Dim j As Integer = 0
        For i = 0 To botones.Length - 1
            Try
                If botones(i).Location.X = X And botones(i).Location.Y = Y Then
                    Return i + 1
                End If
            Catch

            End Try
        Next
        Return j
    End Function
    Public Function IndiceBotonCon(ByVal str As String) As Integer
        Dim i As Integer
        For i = 0 To botones.Length - 2
            If botones(i).Text = str Then
                Exit For
            End If
        Next
        Return i
    End Function

    Public Function MoverPieza(ByRef boton As Button) As Boolean
        Dim i As Integer
        If boton.Location.X = Xhueco Then
            If boton.Location.Y + 80 = Yhueco Then
                Yhueco = boton.Location.Y
                For i = boton.Location.Y To boton.Location.Y + 80
                    boton.Location = New Point(Xhueco, i)
                Next
                ActualizarNodoactual()
                Return True
            ElseIf boton.Location.Y - 80 = Yhueco Then
                Yhueco = boton.Location.Y
                For i = boton.Location.Y To boton.Location.Y - 80 Step -1
                    boton.Location = New Point(Xhueco, i)
                Next
                ActualizarNodoactual()
                Return True
            Else
                Return False
            End If
        ElseIf boton.Location.Y = Yhueco Then
            If boton.Location.X + 80 = Xhueco Then
                Xhueco = boton.Location.X
                For i = boton.Location.X To boton.Location.X + 80
                    boton.Location = New Point(i, Yhueco)
                Next
                ActualizarNodoactual()
                Return True
            ElseIf boton.Location.X - 80 = Xhueco Then
                Xhueco = boton.Location.X
                For i = boton.Location.X To boton.Location.X - 80 Step -1
                    boton.Location = New Point(i, Yhueco)
                Next
                ActualizarNodoactual()
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Sub ActualizarNodoactual()
        If N = 2 Then
            For i = 0 To 2
                For j = 0 To 2
                    NodoActual(j, i) = IndiceBotonEn(i * 80, 24 + j * 80)
                Next
            Next
        End If
    End Sub

    Public Sub CopiaNodo(ByRef NodoBase As Integer(,), ByRef NodoNuevo As Integer(,))
        For i = 0 To N
            For j = 0 To N
                NodoNuevo(i, j) = NodoBase(i, j)
            Next
        Next
    End Sub

    Private Function Mover(ByVal Nodo(,) As Integer, ByVal pieza As Integer) As Integer(,)
        Dim Copia(N, N) As Integer

        CopiaNodo(Nodo, Copia)

        For i = 0 To N
            For j = 0 To N
                If Nodo(i, j) = 0 Then
                    Copia(i, j) = pieza
                ElseIf Nodo(i, j) = pieza Then
                    Copia(i, j) = 0
                End If
            Next
        Next

        Return Copia
    End Function

    Public root As TreeNode

    Private Sub CrearArbol(ByVal Objetivo As String)
        root = New TreeNode()
        root.Nodes.Clear()
        q.Clear()
        root.Name = String_Nodo(NodoActual)
        root.Text = String_Nodo(NodoActual) & " H=" & HeuristicaManhattan(root.Name, Objetivo)
        PrimeroEnAnchura(root, Objetivo)
    End Sub

    Public Sub copiaArreglo(ByVal arregloBase() As Integer, ByRef arregloNuevo() As Integer)
        For i = 0 To arregloBase.Length - 1
            arregloNuevo(i) = arregloBase(i)
        Next
    End Sub

    '*******************************************************************************
    '*******************************************************************************
    '*******************************************************************************
    Public q As New Queue(Of TreeNode)

    Public Sub PrimeroEnAnchura(ByRef NodoPadre As TreeNode, ByVal Objetivo As String)
        buscando.Refresh()
        Dim temp As TreeNode
        NodoPadre.ForeColor = Color.Blue
        q.Enqueue(NodoPadre)

        While (q.Count > 0)
            buscando.Refresh()
            temp = q.Dequeue
            If EstaArmado(temp.Name, Objetivo) Then
                Exit Sub
            Else
                Dim c As ArrayList
                c = Cuales(Nodo_String(temp.Name))
                cantnodos += c.Count
                For i = 0 To c.Count - 1
                    Dim movido As String = _
                        String_Nodo(Mover(Nodo_String(temp.Name), c(i)))

                    If EstaArmado(movido, Objetivo) Then
                        temp.Nodes.Add(movido, movido & " H=0").ForeColor = Color.Red
                        Exit Sub
                    End If
                    If Not root.Name = movido And Not BuscaNodo(root, movido) Then
                        temp.Nodes.Add(movido, movido & " H=" & HeuristicaManhattan(movido, Objetivo))
                    End If
                Next

                If temp.Nodes.Count > 0 Then
                    Dim mejores() As Integer = IndiceMejorHijo(temp, _
                                                             Objetivo)
                    For Each i In mejores
                        temp.Nodes(i).ForeColor = Color.Blue
                        q.Enqueue(temp.Nodes(i))
                    Next
                End If
            End If
        End While
    End Sub

    Public Function IndiceMejorHijo(ByVal NodoPadre As TreeNode, ByVal Objetivo As String) As Integer()
        On Error Resume Next
        Dim i As Integer = 0

        Dim mejores(0), Heuristicas(NodoPadre.Nodes.Count - 1), cont, menor As Integer
        cont = 0

        For i = 0 To NodoPadre.Nodes.Count - 1
            Heuristicas(i) = HeuristicaManhattan(NodoPadre.Nodes(i).Name, Objetivo)
        Next
        menor = 0
        For i = 0 To Heuristicas.Length - 1
            If Heuristicas(i) < Heuristicas(menor) Then
                menor = i
            End If
        Next
        For i = 0 To Heuristicas.Length - 1
            If Heuristicas(menor) = Heuristicas(i) Then
                ReDim Preserve mejores(cont)
                mejores(cont) = i
                cont += 1
            End If
        Next

        Return mejores
    End Function

    Public Function HeuristicaPiezaOut(ByVal Estado As String, ByVal Objetivo As String) As Integer
        Dim Malos As Integer = 0
        Dim est(8) As Integer
        Dim obj(8) As Integer

        copiaArreglo(String_To_Arreglo(Objetivo), obj)
        copiaArreglo(String_To_Arreglo(Estado), est)

        For i = 1 To est.Length - 1 Step 1
            If est(i) <> obj(i) Then
                Malos += 1
            End If
        Next

        Return Malos
    End Function

    Public Function HeuristicaManhattan(ByVal Estado As String, ByRef Objetivo As String)
        Dim piezas(8) As Integer

        copiaArreglo(String_To_Arreglo(Estado), piezas)
        Dim Manhattan As Integer = 0

        For Each i In piezas
            If i <> 0 Then
                Manhattan += DistanciaManhattan(Estado, Objetivo, i)
            End If
        Next

        Return Manhattan
    End Function

    Public Function DistanciaManhattan(ByVal Estado As String, ByVal Objetivo As String, ByVal pieza As Integer) As Integer
        Dim est(N, N) As Integer
        Dim obj(N, N) As Integer
        CopiaNodo(Nodo_String(Estado), est)
        CopiaNodo(Nodo_String(Objetivo), obj)

        Dim Distancia As Integer = 0
        Dim i1, i2, j1, j2 As Integer

        For i = 0 To N
            For j = 0 To N
                If est(i, j) = pieza Then
                    i1 = i
                    j1 = j
                End If
            Next
        Next
        For i = 0 To N
            For j = 0 To N
                If obj(i, j) = pieza Then
                    i2 = i
                    j2 = j
                End If
            Next
        Next

        Distancia = Math.Abs(i1 - i2) + Math.Abs(j1 - j2)

        Return Distancia
    End Function

    Public Function String_To_Arreglo(ByVal Strng As String) As Integer()
        Dim arr((N + 1) * (N + 1) - 1) As Integer
        Dim temp As String = ""
        Dim cont As Integer = 0

        For i = 1 To Len(Strng) Step 2
            arr((i - 1) \ 2) = Mid(Strng, i, 1)
        Next

        Return arr
    End Function

    Private Function BuscaNodo(ByRef NodoPadre As TreeNode, ByVal Nombre As String) As Boolean
        Dim nodo() As TreeNode = NodoPadre.Nodes.Find(Nombre, True)

        If nodo.Length > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function Mover_String(ByVal Nodo1 As Integer(,), ByVal pieza As Integer) As String
        Dim NodoResultante As Integer(,) = Mover(Nodo1, pieza)
        Return String_Nodo(NodoResultante)
    End Function

    Public Function String_Nodo(ByVal Nodo As Integer(,))
        Dim resp As String = ""
        For i = 0 To N
            For j = 0 To N
                resp &= Nodo(i, j) & ","
            Next
        Next
        Return resp
    End Function
    Public Function Arreglo_To_String(ByVal Arreglo() As Integer)
        Dim resp As String = ""
        For i = 0 To Arreglo.Length - 1
            resp &= Arreglo(i) & ","
        Next
        Return resp
    End Function

    Public Function Nodo_String(ByVal StringNodo As String) As Integer(,)
        Dim NodoResultante(N, N) As Integer
        Dim contfila As Byte = 0
        Dim contcol As Byte = 0

        Dim temp As String = ""

        For i = 1 To Len(StringNodo)
            If contfila <= N Then
                If Mid(StringNodo, i, 1) <> "," Then
                    temp &= Mid(StringNodo, i, 1)
                Else
                    NodoResultante(contfila, contcol) = CInt(temp)
                    temp = ""
                    contcol += 1
                    If contcol = 3 Then
                        contcol = 0
                        contfila += 1
                    End If
                End If
            End If
        Next

        Return NodoResultante
    End Function

    Private Sub VerPosiblesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VerPosiblesToolStripMenuItem.Click
        npieza = 0
        Timer1.Enabled = True
    End Sub

    Private Sub Mostrar(ByVal nodo(,) As Integer)
        Dim sal As String = ""

        For i = 0 To N
            For j = 0 To N
                sal &= nodo(i, j) & "___"
            Next
            sal &= Chr(13) + Chr(10)
        Next
        MsgBox(sal)
    End Sub

    Public Function EstaArmado(ByVal nodo As String, ByVal Objetivo As String)
        If nodo = Objetivo Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub AcercaDeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AcercaDeToolStripMenuItem.Click
        'Acercade.Show()
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If pila.Count > 0 Then
            MoverPieza(botones(pila.Pop))
        Else
            Timer2.Enabled = False
        End If
    End Sub

    Dim tinicio, tfin, t, cantnodos, cantcaminos, nmov
    Private Sub OrdearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrdearToolStripMenuItem.Click
            ReDim piezas(0)
            If EstaArmado(String_Nodo(NodoActual), "1,2,3,4,5,6,7,8,0,") Then
                Arbol.Nodes.Clear()
                MsgBox("Ya está armado!", vbInformation)
            Else
                Arbol.Nodes.Clear()
                Me.Refresh()
                If N = 2 Then
                    'inicio
                    buscando.Show()
                    buscando.Refresh()
                    cantnodos = 0
                    nmov = 0
                    cantcaminos = 1
                tinicio = Now.Millisecond
                    CrearArbol("1,2,3,4,5,6,7,8,0,")
                tfin = Now.Millisecond
                t = (tfin - tinicio)
                    buscando.Close()
                    'fin

                    Arbol.Nodes.Add(root)
                    If Arbol.Nodes.Find("1,2,3,4,5,6,7,8,0,", True).Length > 0 Then
                        Arbol.Nodes(0).ExpandAll()
                        MsgBox("Solución encontrada!", vbInformation)


                        Arbol.SelectedNode = Arbol.Nodes.Find("1,2,3,4,5,6,7,8,0,", True).ElementAt(0)
                        Arbol.Select()

                        Dim arr() As TreeNode
                        Dim cad As String = ""
                        arr = Arbol.Nodes.Find("1,2,3,4,5,6,7,8,0,", True)
                        For Each i In arr
                            cad = i.FullPath
                        Next
                        LlenarLista(cad)
                    Else
                        MsgBox("Solución no encontrada... :(", vbInformation)
                    End If
                Else
                    AlReves()
                End If
            End If
    End Sub

    Public Sub AlReves()
        Timer2.Enabled = True
        juegoiniciado = False
    End Sub

    Private piezas() As Integer
    Public Sub LlenarLista(ByVal cad As String)
        Dim arr(0) As String
        Dim cont As Integer = -1
        Dim temp As String = ""
        For i = 1 To Len(cad)
            If Mid(cad, i, 1) <> "\" Then
                temp &= Mid(cad, i, 1)
            Else
                ReDim Preserve arr(cont + 1)
                arr(cont + 1) = temp
                cont += 1
                temp = ""
            End If
        Next
        ReDim Preserve arr(cont + 1)
        arr(cont + 1) = temp

        ListBox1.Items.Clear()

        ReDim piezas(cont)
        piezas = PiezasQueSeMueven(arr)
        For Each i In piezas
            ListBox1.Items.Add(ListBox1.Items.Count + 1 & ". " & i.ToString)
        Next
        ListBox1.Items.Add("*********************************")
        ListBox1.Items.Add("Movimientos necesarios: " & piezas.Length)
        ListBox1.Items.Add("Nodos visitados: " & cantnodos)
        ListBox1.Items.Add("Caminos encontrados: " & cantcaminos)
        ListBox1.Items.Add("Tiempo: " & (t / 1000).ToString.Replace(",", ".") & " seg.")
        ListBox1.Items.Add("Rendimiento: " & Math.Round(1000 / t, 4))
        ListBox1.Items.Add("Productividad: " & Math.Round(cantnodos / CDbl(t / 1000), 4).ToString.Replace(",", ".") & " n/s")
        VerPosiblesToolStripMenuItem.Enabled = True
    End Sub

    Public Function PiezasQueSeMueven(ByRef arr() As String) As Integer()
        Dim piezas(arr.Length - 2) As Integer
        MejorarLista(arr)
        For i = 0 To arr.Length - 2
            piezas(i) = PiezaQueSeMueve(arr(i), arr(i + 1))
        Next
        Return piezas
    End Function

    Sub MejorarLista(ByRef arr() As String)
        For Each i In arr
            i = Mid(i, 1, 18)
        Next
    End Sub

    Private Function PiezaQueSeMueve(ByVal anterior As String, ByVal siguiente As String) As Integer
        Dim a(2, 2) As Integer
        Dim b(2, 2) As Integer
        Dim x1, y1, x2, y2 As Integer
        CopiaNodo(Nodo_String(anterior), a)
        CopiaNodo(Nodo_String(siguiente), b)
        'buscamos el 0 en el primer nodo (i,j)==0
        For i = 0 To 2
            For j = 0 To 2
                If a(i, j) = 0 Then
                    x1 = i
                    y1 = j
                End If
            Next
        Next
        'buscamos el 0 en el segundo nodo (i,j)==0
        For i = 0 To 2
            For j = 0 To 2
                If b(i, j) = 0 Then
                    x2 = i
                    y2 = j
                End If
            Next
        Next
        'si el 0 se movio de a(x1,y1) hasta b(x2,y2)
        'entonces la pieza queda en b(x1,y1), quedando el hueco en a(x2,y2)
        Return b(x1, y1)
    End Function

    Private Sub X4ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles X4ToolStripMenuItem.Click
        N = 2
        RefrescarTodo()
    End Sub

    Private Sub X5ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles X5ToolStripMenuItem.Click
        N = 3
        RefrescarTodo()
    End Sub

    Private Sub X5ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles X5ToolStripMenuItem1.Click
        N = 4
        RefrescarTodo()
    End Sub

    Private Sub IniciarJuegoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IniciarJuegoToolStripMenuItem.Click
        juegoiniciado = True
        pila.Clear()
    End Sub

    Public Sub RefrescarTodo()
        For i = 0 To botones.Length - 1
            Me.Controls.Remove(botones(i))
        Next

        ReDim botones(0 To ((N + 1) * (N + 1)) - 2)
        ReDim NodoActual(N, N)

        juegoiniciado = False
        alternate = False
        PictureBox1.Image = Image.FromFile(Application.StartupPath & "\" & "sigue.png")

        MinX = 0
        MaxX = N * 80
        MinY = 24
        MaxY = 24 + N * 80

        Xhueco = 0 + N * 80
        Yhueco = 24 + N * 80
        nom = 0

        For i = 0 To N
            For j = 0 To N
                If i <> N Or j <> N Then
                    BotonesDinamicos(nom, j * 80, 24 + i * 80)
                    nom += 1
                End If
            Next
        Next

        For i = 0 To ((N + 1) * (N + 1)) - 2
            AddHandler botones(i).Click, AddressOf Botones_Click
        Next

        Me.Controls.AddRange(botones.ToArray)

        ActualizarNodoactual()

        Arbol.Location = New Point(MaxX + 85, Arbol.Location.Y)
        ListBox1.Location = New Point(MaxX + 85, ListBox1.Location.Y)
        Me.Width = MaxX + 1.5 * 82

        Arbol.Nodes.Clear()
        ListBox1.Items.Clear()
    End Sub
    Private npieza As Integer = 0
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If npieza <> piezas.Length Then
            MoverPieza(botones(piezas(npieza) - 1))
            npieza += 1
        Else
            Timer1.Enabled = False
            VerPosiblesToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub DesordenarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DesordenarToolStripMenuItem.Click
        If N = 2 Then
            Dim Objetivo As String = RandomSolubleString()
            MsgBox("Se tratará de llegar a: " & Objetivo, vbInformation)

            Arbol.Nodes.Clear()
            Me.Refresh()
            If N = 2 Then
                'inicio
                buscando.Show()
                buscando.Refresh()
                cantnodos = 0
                nmov = 0
                cantcaminos = 1
                tinicio = Now
                CrearArbol(Objetivo)
                tfin = Now
                t = DateDiff(DateInterval.Second, tinicio, tfin)
                buscando.Close()
                'fin

                Arbol.Nodes.Add(root)
                If Arbol.Nodes.Find(Objetivo, True).Length > 0 Then
                    Arbol.Nodes(0).ExpandAll()
                    MsgBox("Solución encontrada!", vbInformation)
                    Dim arr() As TreeNode
                    Dim cad As String = ""
                    arr = Arbol.Nodes.Find(Objetivo, True)
                    For Each i In arr
                        cad = i.FullPath
                    Next
                    LlenarLista(cad)
                Else
                    MsgBox("Solución no encontrada... :(", vbInformation)
                End If
            End If
        Else
            MsgBox("Solamente disponible para 3x3", vbInformation, "Lo sentimos... :(")
        End If
    End Sub

    Public Function RandomSolubleString() As String
        Dim strfind As String

        Do
            strfind = Arreglo_To_String(RandomArreglo())
            esSoluble(String_To_Arreglo(strfind), N + 1)
        Loop While Not esSoluble(String_To_Arreglo(strfind), N + 1)

        Return strfind
    End Function

    Public Function RandomArreglo() As Integer()
        On Error Resume Next
        Randomize()

        Dim disponibles As New ArrayList()
        For i = 1 To 8
            disponibles.Add(i)
        Next
        disponibles.Add(0)
        Dim desordenados As New ArrayList()
        Dim RandomIndex As Byte

        While disponibles.Count > 0
            RandomIndex = Rnd(disponibles.Count)
            desordenados.Add(disponibles(RandomIndex))
            disponibles.RemoveAt(RandomIndex)
        End While

        Dim ret(8) As Integer
        For i = 0 To 8
            ret(i) = desordenados(i)
        Next
        Return ret
    End Function

    Public Function esSoluble(ByVal Estado() As Integer, ByVal n As Integer) As Boolean
        Dim NActual As Integer = Estado.Length
        Dim dimen As Integer = n
        Dim inversions As Integer = 0

        For i = 0 To NActual - 1
            Dim iPieza As Integer = Estado(i)
            If (iPieza <> 0) Then
                For j = i + 1 To NActual - 1
                    Dim jPieza As Integer = Estado(j)
                    If (jPieza <> 0 And jPieza < iPieza) Then
                        inversions += 1
                    End If
                Next
            Else
                If (dimen Mod 2 = 0) Then
                    inversions += (1 + i / dimen)
                End If
            End If
        Next
        If (inversions Mod 2 <> 0) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub SalirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalirToolStripMenuItem.Click
        End
    End Sub
End Class
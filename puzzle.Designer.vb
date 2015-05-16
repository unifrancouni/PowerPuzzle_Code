<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class puzzle
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(puzzle))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ProgramaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IniciarJuegoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AyudaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TamañoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.X4ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.X5ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.X5ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CargarImagenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResolverToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.OrdearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DesordenarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VerPosiblesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AyudaToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManualDeUsuarioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManualTécnicoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AcercaDeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Arbol = New System.Windows.Forms.TreeView()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.SlateGray
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgramaToolStripMenuItem, Me.AyudaToolStripMenuItem, Me.AyudaToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(489, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ProgramaToolStripMenuItem
        '
        Me.ProgramaToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IniciarJuegoToolStripMenuItem, Me.SalirToolStripMenuItem})
        Me.ProgramaToolStripMenuItem.Name = "ProgramaToolStripMenuItem"
        Me.ProgramaToolStripMenuItem.Size = New System.Drawing.Size(71, 20)
        Me.ProgramaToolStripMenuItem.Text = "Programa"
        '
        'IniciarJuegoToolStripMenuItem
        '
        Me.IniciarJuegoToolStripMenuItem.Name = "IniciarJuegoToolStripMenuItem"
        Me.IniciarJuegoToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.IniciarJuegoToolStripMenuItem.Text = "Iniciar Juego"
        '
        'SalirToolStripMenuItem
        '
        Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SalirToolStripMenuItem.Text = "Salir"
        '
        'AyudaToolStripMenuItem
        '
        Me.AyudaToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TamañoToolStripMenuItem, Me.CargarImagenToolStripMenuItem, Me.ResolverToolStripMenuItem1, Me.VerPosiblesToolStripMenuItem})
        Me.AyudaToolStripMenuItem.Name = "AyudaToolStripMenuItem"
        Me.AyudaToolStripMenuItem.Size = New System.Drawing.Size(69, 20)
        Me.AyudaToolStripMenuItem.Text = "Opciones"
        '
        'TamañoToolStripMenuItem
        '
        Me.TamañoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.X4ToolStripMenuItem, Me.X5ToolStripMenuItem, Me.X5ToolStripMenuItem1})
        Me.TamañoToolStripMenuItem.Name = "TamañoToolStripMenuItem"
        Me.TamañoToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.TamañoToolStripMenuItem.Text = "Tamaño"
        '
        'X4ToolStripMenuItem
        '
        Me.X4ToolStripMenuItem.Name = "X4ToolStripMenuItem"
        Me.X4ToolStripMenuItem.Size = New System.Drawing.Size(91, 22)
        Me.X4ToolStripMenuItem.Text = "3x3"
        '
        'X5ToolStripMenuItem
        '
        Me.X5ToolStripMenuItem.Name = "X5ToolStripMenuItem"
        Me.X5ToolStripMenuItem.Size = New System.Drawing.Size(91, 22)
        Me.X5ToolStripMenuItem.Text = "4x4"
        '
        'X5ToolStripMenuItem1
        '
        Me.X5ToolStripMenuItem1.Name = "X5ToolStripMenuItem1"
        Me.X5ToolStripMenuItem1.Size = New System.Drawing.Size(91, 22)
        Me.X5ToolStripMenuItem1.Text = "5x5"
        '
        'CargarImagenToolStripMenuItem
        '
        Me.CargarImagenToolStripMenuItem.Name = "CargarImagenToolStripMenuItem"
        Me.CargarImagenToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.CargarImagenToolStripMenuItem.Text = "Cargar Imagen..."
        '
        'ResolverToolStripMenuItem1
        '
        Me.ResolverToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OrdearToolStripMenuItem, Me.DesordenarToolStripMenuItem})
        Me.ResolverToolStripMenuItem1.Name = "ResolverToolStripMenuItem1"
        Me.ResolverToolStripMenuItem1.Size = New System.Drawing.Size(164, 22)
        Me.ResolverToolStripMenuItem1.Text = "Resolver"
        '
        'OrdearToolStripMenuItem
        '
        Me.OrdearToolStripMenuItem.Name = "OrdearToolStripMenuItem"
        Me.OrdearToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.OrdearToolStripMenuItem.Text = "Ordenar"
        '
        'DesordenarToolStripMenuItem
        '
        Me.DesordenarToolStripMenuItem.Name = "DesordenarToolStripMenuItem"
        Me.DesordenarToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.DesordenarToolStripMenuItem.Text = "Desordenar"
        '
        'VerPosiblesToolStripMenuItem
        '
        Me.VerPosiblesToolStripMenuItem.Enabled = False
        Me.VerPosiblesToolStripMenuItem.Name = "VerPosiblesToolStripMenuItem"
        Me.VerPosiblesToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.VerPosiblesToolStripMenuItem.Text = "Ver Movimientos"
        '
        'AyudaToolStripMenuItem1
        '
        Me.AyudaToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ManualDeUsuarioToolStripMenuItem, Me.ManualTécnicoToolStripMenuItem, Me.AcercaDeToolStripMenuItem})
        Me.AyudaToolStripMenuItem1.Name = "AyudaToolStripMenuItem1"
        Me.AyudaToolStripMenuItem1.Size = New System.Drawing.Size(53, 20)
        Me.AyudaToolStripMenuItem1.Text = "Ayuda"
        '
        'ManualDeUsuarioToolStripMenuItem
        '
        Me.ManualDeUsuarioToolStripMenuItem.Name = "ManualDeUsuarioToolStripMenuItem"
        Me.ManualDeUsuarioToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.ManualDeUsuarioToolStripMenuItem.Text = "Manual de Usuario"
        '
        'ManualTécnicoToolStripMenuItem
        '
        Me.ManualTécnicoToolStripMenuItem.Name = "ManualTécnicoToolStripMenuItem"
        Me.ManualTécnicoToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.ManualTécnicoToolStripMenuItem.Text = "Manual Técnico"
        '
        'AcercaDeToolStripMenuItem
        '
        Me.AcercaDeToolStripMenuItem.Name = "AcercaDeToolStripMenuItem"
        Me.AcercaDeToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.AcercaDeToolStripMenuItem.Text = "Acerca de..."
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Right
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(455, 24)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(34, 404)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'Timer2
        '
        Me.Timer2.Interval = 500
        '
        'Arbol
        '
        Me.Arbol.Location = New System.Drawing.Point(233, 26)
        Me.Arbol.Name = "Arbol"
        Me.Arbol.Size = New System.Drawing.Size(220, 194)
        Me.Arbol.TabIndex = 3
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.SystemColors.MenuText
        Me.ListBox1.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox1.ForeColor = System.Drawing.Color.Lime
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 12
        Me.ListBox1.Location = New System.Drawing.Point(233, 226)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(220, 196)
        Me.ListBox1.TabIndex = 4
        '
        'puzzle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.ClientSize = New System.Drawing.Size(489, 428)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Arbol)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "puzzle"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Power Puzzle"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ProgramaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AyudaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CargarImagenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResolverToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OrdearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DesordenarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AyudaToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ManualDeUsuarioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ManualTécnicoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AcercaDeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VerPosiblesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Arbol As System.Windows.Forms.TreeView
    Friend WithEvents IniciarJuegoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TamañoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents X4ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents X5ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents X5ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox

End Class

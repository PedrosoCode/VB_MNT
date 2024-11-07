Namespace VB_MNT
    Partial Public Class HomeWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
        End Sub

        ' Evento para abrir uma nova aba
        Private Sub OpenTab_Click(sender As Object, e As RoutedEventArgs)
            Dim menuItem As MenuItem = CType(sender, MenuItem)
            Dim tabTitle As String = menuItem.Header.ToString()

            ' Verifica se a aba já está aberta
            For Each tab As TabItem In MainTabControl.Items
                If tab.Header.ToString() = tabTitle Then
                    MainTabControl.SelectedItem = tab
                    Return
                End If
            Next

            ' Cria uma nova TabItem
            Dim newTab As New TabItem() With {
                .Header = tabTitle
            }

            ' Verifica se o controle é CadAtivo
            If tabTitle = "Usuários" Then
                ' Carrega o controle CadAtivo dentro da aba
                Dim cadAtivoControl As New CadAtivo()
                newTab.Content = cadAtivoControl
            Else
                ' Caso contrário, cria uma aba com conteúdo padrão (ou outro controle)
                Dim subTabControl As New TabControl()
                Dim defaultSubTab As New TabItem() With {
                    .Header = "Sub-Aba 1",
                    .Content = New TextBlock() With {.Text = $"Conteúdo da {tabTitle} - Sub-Aba 1"}
                }
                subTabControl.Items.Add(defaultSubTab)
                newTab.Content = subTabControl
            End If

            ' Adiciona a nova aba ao controle principal de abas
            MainTabControl.Items.Add(newTab)
            MainTabControl.SelectedItem = newTab
        End Sub

        ' Evento de logout
        Private Sub Logout_Click(sender As Object, e As RoutedEventArgs)
            Dim loginWindow As New MainWindow()
            loginWindow.Show()
            Me.Close()
        End Sub
    End Class
End Namespace

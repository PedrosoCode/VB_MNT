Imports System.Windows

Namespace VB_MNT
    Partial Public Class HomeWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
        End Sub

        ' Evento de logout
        Private Sub Logout_Click(sender As Object, e As RoutedEventArgs)
            Dim loginWindow As New MainWindow()
            loginWindow.Show()
            Me.Close()
        End Sub
    End Class
End Namespace

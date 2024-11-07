Imports System.ComponentModel
Imports System.Windows

Namespace VB_MNT
    Public Class CadAtivo
        Inherits UserControl
        Implements INotifyPropertyChanged

        Public Sub New()
            InitializeComponent()
            Me.DataContext = Me
            ' Inicialize a visibilidade como Collapsed para que a aba "Dados" comece invisível.
            _isDadosTabVisible = Visibility.Collapsed
        End Sub

        Private _selectedTabIndex As Integer
        Public Property SelectedTabIndex As Integer
            Get
                Return _selectedTabIndex
            End Get
            Set(ByVal value As Integer)
                _selectedTabIndex = value
                NotificarMudancaDePropriedade(NameOf(SelectedTabIndex))
            End Set
        End Property

        Private _isDadosTabVisible As Visibility = Visibility.Collapsed
        Public Property IsDadosTabVisible As Visibility
            Get
                Return _isDadosTabVisible
            End Get
            Set(ByVal value As Visibility)
                _isDadosTabVisible = value
                NotificarMudancaDePropriedade(NameOf(IsDadosTabVisible))
            End Set
        End Property

        Private Sub EditarButton_Click(sender As Object, e As RoutedEventArgs)
            ' Torna a aba de dados visível e troca para ela.
            IsDadosTabVisible = Visibility.Visible
            SelectedTabIndex = 1 ' Índice da aba de dados.
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Protected Sub NotificarMudancaDePropriedade(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class
End Namespace

Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data
Imports VB_MNT.VB_MNT

Class MainWindow
    Private connectionString As String = ConfigurationManager.ConnectionStrings("SQLconnect").ConnectionString

    Public Sub New()
        ' Esta chamada é exigida pelo designer.
        InitializeComponent()

        ' Carrega as empresas no ComboBox ao abrir a janela de login
        CarregarEmpresas()
    End Sub

    Private Sub CarregarEmpresas()
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                ' Chama a stored procedure para obter a lista de empresas
                Dim query As String = "sp_select_empresas"
                Using command As New SqlCommand(query, connection)
                    command.CommandType = CommandType.StoredProcedure

                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            ' Adiciona cada empresa ao ComboBox, usando o codigo_empresa como valor e o nome_empresa como exibição
                            Dim empresa = New With {
                                .CodigoEmpresa = reader("codigo_empresa"),
                                .NomeEmpresa = reader("nome_empresa").ToString()
                            }
                            cbEmpresa.Items.Add(empresa)
                        End While
                    End Using
                End Using
            End Using

            ' Define o campo a ser exibido no ComboBox
            cbEmpresa.DisplayMemberPath = "NomeEmpresa"
            cbEmpresa.SelectedValuePath = "CodigoEmpresa"
        Catch ex As Exception
            MessageBox.Show("Erro ao carregar empresas: " & ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As RoutedEventArgs)
        Dim usuario As String = txtUsuario.Text
        Dim codigoEmpresa As Integer? = CType(cbEmpresa.SelectedValue, Integer)
        Dim senha As String = txtSenha.Password

        If String.IsNullOrEmpty(usuario) OrElse Not codigoEmpresa.HasValue OrElse String.IsNullOrEmpty(senha) Then
            MessageBox.Show("Por favor, preencha todos os campos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        If AutenticarUsuario(usuario, codigoEmpresa.Value, senha) Then
            MessageBox.Show("Login bem-sucedido!")
            Dim homeWindow As New HomeWindow()
            homeWindow.Show()
            Me.Close()
        Else
            MessageBox.Show("Usuário ou senha incorretos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
    End Sub

    Private Function AutenticarUsuario(usuario As String, codigoEmpresa As Integer, senha As String) As Boolean
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT senha_hash FROM tb_usuarios WHERE nome_usuario = @usuario AND codigo_empresa = @codigoEmpresa"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@usuario", usuario)
                    command.Parameters.AddWithValue("@codigoEmpresa", codigoEmpresa)

                    Dim senhaHash As Object = command.ExecuteScalar()
                    If senhaHash IsNot Nothing AndAlso BCrypt.Net.BCrypt.Verify(senha, senhaHash.ToString()) Then
                        Return True
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Erro ao conectar com o banco de dados: " & ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

        Return False
    End Function
End Class

Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data
Imports VB_MNT.VB_MNT

Class MainWindow
    Private connectionString As String = ConfigurationManager.ConnectionStrings("SQLconnect").ConnectionString

    Public Sub New()

        InitializeComponent()
        CarregarEmpresas()
    End Sub

    Private Sub CarregarEmpresas()
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "sp_select_empresas"
                Using command As New SqlCommand(query, connection)
                    command.CommandType = CommandType.StoredProcedure

                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            Dim empresa = New With {
                                .CodigoEmpresa = reader("codigo_empresa"),
                                .NomeEmpresa = reader("nome_empresa").ToString()
                            }
                            cbEmpresa.Items.Add(empresa)
                        End While
                    End Using
                End Using
            End Using

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

        Dim userId = AutenticarUsuario(usuario, codigoEmpresa.Value, senha)
        If userId.HasValue Then
            MessageBox.Show("Login bem-sucedido!")
            varGlobal.idUsuario = userId.Value
            varGlobal.idEmpresa = codigoEmpresa.Value

            Dim homeWindow As New HomeWindow()
            homeWindow.Show()
            Me.Close()
        Else
            MessageBox.Show("Usuário ou senha incorretos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
    End Sub

    Private Function AutenticarUsuario(usuario As String, codigoEmpresa As Integer, senha As String) As Integer?
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT id_usuario, senha_hash FROM tb_usuarios WHERE nome_usuario = @usuario AND codigo_empresa = @codigoEmpresa"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@usuario", usuario)
                    command.Parameters.AddWithValue("@codigoEmpresa", codigoEmpresa)

                    Using reader As SqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            Dim senhaHash = reader("senha_hash").ToString()
                            If BCrypt.Net.BCrypt.Verify(senha, senhaHash) Then
                                ' Retorna o código do usuário caso a senha seja válida
                                Return CType(reader("id_usuario"), Integer)
                            End If
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Erro ao conectar com o banco de dados: " & ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

        Return Nothing
    End Function

End Class

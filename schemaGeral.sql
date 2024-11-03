USE [VB_MNT]
GO
/****** Object:  Table [dbo].[tb_info_empresa]    Script Date: 03/11/2024 16:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_info_empresa](
	[id_empresa] [int] NOT NULL,
	[nome_empresa] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_usuarios]    Script Date: 03/11/2024 16:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_usuarios](
	[id_usuario] [int] NOT NULL,
	[nome_usuario] [varchar](150) NOT NULL,
	[codigo_empresa] [int] NOT NULL,
	[senha_hash] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC,
	[codigo_empresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_AutenticarUsuario]    Script Date: 03/11/2024 16:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AutenticarUsuario]
    @usuario NVARCHAR(50),
    @codigoEmpresa INT
AS
BEGIN

    SELECT senha_hash 
    FROM tb_usuarios 
    WHERE nome_usuario = @usuario 
    AND codigo_empresa = @codigoEmpresa;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_select_empresas]    Script Date: 03/11/2024 16:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_select_empresas]

AS

BEGIN

 SELECT 
 	id_empresa AS codigo_empresa,
 	nome_empresa
 FROM tb_info_empresa

END
GO

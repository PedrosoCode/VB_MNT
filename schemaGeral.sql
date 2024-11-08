USE [VB_MNT]
GO
/****** Object:  Table [dbo].[tb_cad_ativo]    Script Date: 03/11/2024 22:31:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_cad_ativo](
	[codigo] [int] NOT NULL,
	[codigo_cliente] [int] NULL,
	[numero_serie] [varchar](255) NULL,
	[codigo_fabricante] [int] NULL,
	[modelo] [varchar](255) NULL,
	[observacao] [varchar](max) NULL,
	[data_input] [date] NULL,
	[codigo_empresa] [int] NOT NULL,
 CONSTRAINT [tb_cad_ativo_pkey] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC,
	[codigo_empresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_cad_parceiro_negocio]    Script Date: 03/11/2024 22:31:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_cad_parceiro_negocio](
	[codigo] [bigint] NOT NULL,
	[nome_razao_social] [varchar](255) NULL,
	[is_cnpj] [bit] NULL,
	[documento] [varchar](255) NULL,
	[endereco] [varchar](255) NULL,
	[cidade] [varchar](255) NULL,
	[estado] [varchar](255) NULL,
	[cep] [varchar](10) NULL,
	[telefone] [varchar](20) NULL,
	[email] [varchar](255) NULL,
	[data_cadastro] [datetime] NULL,
	[tipo_parceiro] [varchar](1) NULL,
	[codigo_empresa] [int] NOT NULL,
 CONSTRAINT [tb_cad_parceiro_negocio_pkey] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC,
	[codigo_empresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_info_empresa]    Script Date: 03/11/2024 22:31:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_info_empresa](
	[id_empresa] [int] NOT NULL,
	[nome_empresa] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_usuarios]    Script Date: 03/11/2024 22:31:26 ******/
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
ALTER TABLE [dbo].[tb_cad_ativo] ADD  DEFAULT (getdate()) FOR [data_input]
GO
/****** Object:  StoredProcedure [dbo].[sp_AutenticarUsuario]    Script Date: 03/11/2024 22:31:26 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_select_empresas]    Script Date: 03/11/2024 22:31:26 ******/
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


CREATE DATABASE AssetDB
GO 
USE AssetDB
GO

CREATE TABLE Cargo
(
CargoId					UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
Nome					VARCHAR(20) NOT NULL
);
GO

CREATE TABLE TipoUsuario
(
TipoUsuarioId			UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
Nome					VARCHAR(20) NOT NULL
)
GO

CREATE TABLE Cidade
(
CidadeId				UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
NomeCidade				VARCHAR(50),
Estado					VARCHAR(2)
)
GO

CREATE TABLE Bairro
(
BairroId				UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
NomeBairro				VARCHAR(30),
CidadeId				UNIQUEIDENTIFIER NOT NULL,
CONSTRAINT BairroCidade_ID_FK FOREIGN KEY (CidadeId)
		REFERENCES Cidade(CidadeId)
)

GO

CREATE TABLE Endereco
(
EnderecoId				UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
Logradoura				VARCHAR(100) NOT NULL,
Numero					INT NOT NULL,
Complemento				VARCHAR(50),
CEP						VARCHAR(12) NOT NULL,
CidadeId				UNIQUEIDENTIFIER NOT NULL,
BairroId				UNIQUEIDENTIFIER NOT NULL,

CONSTRAINT EnderecoBairro_ID_FK FOREIGN KEY (BairroId) 
		REFERENCES Bairro(BairroId),
CONSTRAINT EnderecoCidade_ID_FK FOREIGN KEY (CidadeId) 
		REFERENCES Cidade(CidadeId) 
)


GO

CREATE TABLE Usuario
(
UsuarioId				UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
Nome					NVARCHAR(100) NOT NULL,
RG						VARCHAR(15) UNIQUE,
CPF						NVARCHAR(12) UNIQUE NOT NULL,
CarteiraTrabalho		VARCHAR(14) UNIQUE NOT NULL,
Email					VARCHAR(150) NOT NULL,
Senha					VARBINARY(32) NOT NULL, 
Ativo					BIT DEFAULT 1,

EnderecoId				UNIQUEIDENTIFIER NOT NULL,
CargoId					UNIQUEIDENTIFIER NOT NULL,
TipoUsuarioId			UNIQUEIDENTIFIER NOT NULL,

CONSTRAINT FK_UsuarioEndereco_ID FOREIGN KEY (EnderecoId) 
		REFERENCES Endereco(EnderecoId),  

CONSTRAINT FK_UsuarioCargo_ID FOREIGN KEY (CargoId) 
		REFERENCES Cargo(CargoId),  

CONSTRAINT FK_UsuarioTipoUsuario_ID FOREIGN KEY (TipoUsuarioId) 
		REFERENCES TipoUsuario(TipoUsuarioId)  
)
GO

CREATE TABLE StatusTransferencia
(
StatusTransferenciaId	UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
StatusTransferencia		VARCHAR(50) NOT NULL,
)
GO

CREATE TABLE StatusPatrimonio
(
StatusPatrimonioId	    UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
StatusPatrimonio		VARCHAR(50) NOT NULL,
)
GO

CREATE TABLE TipoPatrimonio
(
TipoPatrimonioId		UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
NomeTipo                VARCHAR(50) NOT NULL
)

GO

CREATE TABLE Area
(
AreaId				    UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
NomeArea                NVARCHAR(50) NOT NULL
)

GO

CREATE TABLE Localizacao
(
LocalizacaoId			UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
NomeLocalizacao			VARCHAR(50) NOT NULL,
LocalizacaoSAP			INT NOT NULL,
DescricaoSAP		    VARCHAR(100) NOT NULL,
Ativo					BIT DEFAULT 1,
AreaId				    UNIQUEIDENTIFIER NOT NULL, 
CONSTRAINT FK_LocalizacaoArea_ID  
		FOREIGN KEY (AreaId) REFERENCES Area(AreaId)  
)

CREATE TABLE Patrimonio
(
PatrimonioId			UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
Denominacao				VARCHAR(MAX) NOT NULL,
Valor					DECIMAL(10,2) NOT NULL,
NumeroSerie				VARCHAR(30) UNIQUE NOT NULL,
Imagem					VARCHAR(MAX) NULL,
LocalizacaoId			UNIQUEIDENTIFIER NOT NULL,
TipoPatrimonioId		UNIQUEIDENTIFIER NOT NULL,
StatusPatrimonioId		UNIQUEIDENTIFIER NOT NULL,

CONSTRAINT FK_PatrimonioLocalizacao_ID  FOREIGN KEY(LocalizacaoId)
		REFERENCES Localizacao(LocalizacaoId),  

CONSTRAINT FK_Patrimonio_TipoPatrimonio_ID  FOREIGN KEY(TipoPatrimonioId)
		REFERENCES TipoPatrimonio(TipoPatrimonioId),  

CONSTRAINT FK_Patrimonio_StatusPatrimonio_ID  FOREIGN KEY (StatusPatrimonioId)
		REFERENCES StatusPatrimonio(StatusPatrimonioId)  
)

GO

CREATE TABLE TipoAlteracao
(
TipoAlteracaoId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
NomeAlteracao NVARCHAR(50) NOT NULL,
)

CREATE TABLE LogPatrimonio
(
LogPatrimonioId		    UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
DataTransferencia		DATETIME2(0) NOT NULL,
PatrimonioId			UNIQUEIDENTIFIER NOT NULL,
UsuarioId				UNIQUEIDENTIFIER NOT NULL,
LocalizacaoId			UNIQUEIDENTIFIER NOT NULL,
StatusPatrimonioId		UNIQUEIDENTIFIER NOT NULL,
TipoAlteracaoId			UNIQUEIDENTIFIER NOT NULL,


CONSTRAINT FK_Log_Patrimonio_ID  FOREIGN KEY (PatrimonioId) 
		REFERENCES Patrimonio(PatrimonioId),  
			

CONSTRAINT FK_Log_Usuario_ID  FOREIGN KEY (UsuarioId) 
		REFERENCES Usuario(UsuarioId),  

CONSTRAINT FK_Log_Localizacao_ID  FOREIGN KEY (LocalizacaoId) 
		REFERENCES Localizacao(LocalizacaoId) ON DELETE NO ACTION,

CONSTRAINT FK_Log_StatusPatrimonio_ID  FOREIGN KEY (StatusPatrimonioId) 
		REFERENCES StatusPatrimonio(StatusPatrimonioId) ON DELETE NO ACTION,

CONSTRAINT FK_Log_TipoAlteracao_ID FOREIGN KEY (TipoAlteracaoId) 
		REFERENCES TipoAlteracao(TipoAlteracaoId)  
)

GO

CREATE TABLE SolicitacaoTransferencia
(
SolicitacaoTransId		UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
DataSolicitacao			DATETIME2(0) NOT NULL,
DataResposta			DATETIME2(0) NOT NULL,
Justificativa			NVARCHAR(MAX) NOT NULL,

PatrimonioId			UNIQUEIDENTIFIER NOT NULL,
UsuarioSolicitandoId	UNIQUEIDENTIFIER NOT NULL,
UsuarioAprovacaoId		UNIQUEIDENTIFIER,
LocalizacaoId			UNIQUEIDENTIFIER NOT NULL,

CONSTRAINT FK_SolicitacaoTransferencia_Patrimonio_ID FOREIGN KEY (PatrimonioId) 
		REFERENCES Patrimonio(PatrimonioId) ON DELETE NO ACTION ,

CONSTRAINT FK_SolicitacaoTransferencia_UsuarioSolicitacao_ID FOREIGN KEY (UsuarioSolicitandoId) 
		REFERENCES Usuario(UsuarioId) ON DELETE NO ACTION ,

CONSTRAINT FK_SolicitacaoTransferencia_UsuarioAprovacao_ID FOREIGN KEY (UsuarioAprovacaoId) 
		REFERENCES Usuario(UsuarioId) ON DELETE NO ACTION ,

CONSTRAINT FK_SolicitacaoTransferencia_Localizacao_ID FOREIGN KEY (LocalizacaoId) 
		REFERENCES Localizacao(LocalizacaoId) ON DELETE NO ACTION ,


)
GO

CREATE TABLE LocalUsuario
(
LocalizacaoId UNIQUEIDENTIFIER,
UsuarioId UNIQUEIDENTIFIER,
CONSTRAINT PK_LocalizacaoUsuario_ID PRIMARY KEY (LocalizacaoId, UsuarioId),

CONSTRAINT FK_LocalUsuario_Localizacao FOREIGN KEY (LocalizacaoId) 
			REFERENCES Localizacao(LocalizacaoId),

CONSTRAINT FK_LocalUsuario_Usuario_ID FOREIGN KEY (UsuarioId)
			REFERENCES Usuario(UsuarioId)
)

GO
--triggers para exclusao 
CREATE TRIGGER trg_Usuario_SoftDelete
	ON Usuario
	INSTEAD OF DELETE
		AS
	BEGIN
		UPDATE Usuario	
			SET Ativo = 0
			WHERE UsuarioId IN (SELECT UsuarioId FROM deleted)
		END
		GO
 

CREATE TRIGGER trg_Localizacao_SoftDelete
	ON Localizacao
	INSTEAD OF DELETE
		AS
		BEGIN
			UPDATE Localizacao
				SET Ativo = 0 
			WHERE LocalizacaoId IN (SELECT LocalizacaoId FROM deleted)
		END
		GO

CREATE TRIGGER trg_Patrimonio_SoftDelete
	ON Patrimonio
	INSTEAD OF DELETE
		AS
			BEGIN
				UPDATE Patrimonio
				SET StatusPatrimonioId = 
					(
					SELECT StatusPatrimonioId 
							FROM StatusPatrimonio 
									WHERE StatusPatrimonio = 'Inativo'
					)
					WHERE PatrimonioId IN (SELECT PatrimonioId FROM deleted)
			END 
			GO


			
INSERT INTO Area(NomeArea)
VALUES
('Bloco A - Térreo'),
('Bloco B - Segundo Andar')

GO

INSERT INTO TipoUsuario(Nome)
VALUES
('Responsável'),
('Coordenador')

GO

INSERT INTO Cargo(Nome)
VALUES
('Diretor'),
('Instrutor de Formação Profissional'),
('Desenvolvedor Full-Stack'),
('Analista de Redes')

GO

INSERT INTO TipoPatrimonio(NomeTipo)
VALUES
('Hardware'),
('Móvel'),
('Notebook')

GO

INSERT INTO StatusPatrimonio(StatusPatrimonio)
VALUES
('Ativo'),
('Inativo'),
('Transferido'),
('Em Manutenção')

GO
INSERT INTO StatusTransferencia(StatusTransferencia)
VALUES
('Aprovada'),
('Pendente'),
('Negada')

GO

INSERT INTO TipoAlteracao(NomeAlteracao)
VALUES
('Atualização de dados'),
('Transferência')
GO

INSERT INTO Cidade(NomeCidade)
VALUES
('São Caetano Do Sul'),
('São Bernardo Do Campo'),
('Diadema'),
('Mauá')

GO


INSERT INTO Localizacao(LocalizacaoSAP, DescricaoSAP, NomeLocalizacao, AreaId)
VALUES
(1, 'nulla', 'Manutenção', (SELECT AreaId FROM Area WHERE NomeArea = 'Bloco A - Térreo'))

GO

INSERT INTO Bairro(NomeBairro, CidadeId)
VALUES
('Centro', (SELECT CidadeId FROM Cidade WHERE NomeCidade = 'Diadema'))

GO
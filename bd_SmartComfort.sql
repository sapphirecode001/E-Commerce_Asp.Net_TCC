-- base de dados da empresa para o uso no Asp.net
-- feito por João Lucas
drop database dbSmartComfort;
create database dbSmartComfort;
use dbSmartComfort;

CREATE TABLE tbPJ (
IdPJ int primary key auto_increment,
Cnpj numeric(14) not null unique,
RazaoSocial varchar(250) not null,
NomeResponsavel varchar(200) not null,
IdUsu int 
);

CREATE TABLE tbPF (
IdPF int primary key auto_increment,
Cpf numeric(11) not null unique,
NomeCompleto varchar(200) not null,
IdUsu int
);

CREATE TABLE tbEndereco (
IdEnd int PRIMARY KEY auto_increment,
cepEnd numeric(8) not null,
numeroEnd smallint unsigned not null,
LogradouroEnd varchar(250) not null,
ComplementoEnd varchar(255),
IdBai int,
IdEst int,
IdCid int
);

CREATE TABLE tbUsuario (
IdUsu int PRIMARY KEY auto_increment,
EmailUsu varchar(100) not null unique,
SenhaUsu varchar(30) not null,
TelefoneUsu1 numeric(11) not null,
TelefoneUsu2 numeric(11),
TipoUsu char(2) default 'PF',
DataCadUsu datetime default current_timestamp(),
IdEnd int,
FOREIGN KEY(IdEnd) REFERENCES tbEndereco (IdEnd)
);

CREATE TABLE tbEstado (
IdEst int PRIMARY KEY auto_increment,
UfEstado char(2) unique not null
);

CREATE TABLE tbBairro (
IdBai int PRIMARY KEY auto_increment,
Bairro varchar(100) not null
);

CREATE TABLE tbCidade (
IdCid int PRIMARY KEY auto_increment,
Cidade varchar(100) not null
);

CREATE TABLE tbPedido (
IdPed int PRIMARY KEY auto_increment,
DataPed datetime not null default current_timestamp(),
TotalPed decimal(10,2) not null,
IdUsu int,
NumNF int,
IdPag int,
IdEnd int,
FOREIGN KEY(IdUsu) REFERENCES tbUsuario (IdUsu),
FOREIGN KEY(IdEnd) REFERENCES tbEndereco (IdEnd)
);

CREATE TABLE tbPagamento (
IdPag int PRIMARY KEY auto_increment,
StatusPag varchar(50) not null,
MetodoPag varchar(50) not null
);

CREATE TABLE tbNotaFiscal (
NumNF int PRIMARY KEY auto_increment,
ChaveAcessoNF bigint unsigned not null unique,
DataNF date not null,
ValorNF decimal(10,2) not null
);

CREATE TABLE tbItemPedido (
IdIte int PRIMARY KEY auto_increment,
QtdIte smallint unsigned not null,
PrecoIte decimal(10,2) not null,
IdPed int,
Id int,
FOREIGN KEY(IdPed) REFERENCES tbPedido (IdPed)
);

CREATE TABLE tbFavoritos (
IdFav int PRIMARY KEY auto_increment,
Id int,
IdUsu int,
FOREIGN KEY(IdUsu) REFERENCES tbUsuario (IdUsu)
);

CREATE TABLE tbProdutoAutomacao (
Id int primary key auto_increment,
CodBar bigint,
NomePro varchar(250) not null,
PrecoPro decimal(10,2) not null,
QtdEstoquePro int unsigned not null,
GarantiaPro date not null,
ImgUrlPro varchar(255) not null,
Voltagem varchar(10) not null,
IdCategoria int
);

CREATE TABLE tbCategoria (
IdCategoria int PRIMARY KEY auto_increment,
NomeCategoria varchar(50) not null
);

CREATE TABLE tbFuncionario (
IdFunc int PRIMARY KEY auto_increment,
DataEntradaFunc datetime not null default current_timestamp,
EmailFunc varchar(250) not null unique,
NomeFunc varchar(200) not null,
SenhaFunc varchar(50) not null
);


ALTER TABLE tbPJ ADD FOREIGN KEY(IdUsu) REFERENCES tbUsuario (IdUsu);
ALTER TABLE tbPF ADD FOREIGN KEY(IdUsu) REFERENCES tbUsuario (IdUsu);
ALTER TABLE tbEndereco ADD FOREIGN KEY(IdBai) REFERENCES tbBairro (IdBai);
ALTER TABLE tbEndereco ADD FOREIGN KEY(IdEst) REFERENCES tbEstado (IdEst);
ALTER TABLE tbEndereco ADD FOREIGN KEY(IdCid) REFERENCES tbCidade (IdCid);
ALTER TABLE tbPedido ADD FOREIGN KEY(NumNF) REFERENCES tbNotaFiscal (NumNF);
ALTER TABLE tbPedido ADD FOREIGN KEY(IdPag) REFERENCES tbPagamento (IdPag);
ALTER TABLE tbItemPedido ADD FOREIGN KEY(Id) REFERENCES tbProdutoAutomacao (Id);
ALTER TABLE tbFavoritos ADD FOREIGN KEY(Id) REFERENCES tbProdutoAutomacao (Id);
ALTER TABLE tbProdutoAutomacao ADD FOREIGN KEY(IdCategoria) REFERENCES tbCategoria (IdCategoria);

-- Inserindo estados
INSERT INTO tbEstado (UfEstado) VALUES ('SP');
INSERT INTO tbEstado (UfEstado) VALUES ('RJ');

-- Inserindo cidades
INSERT INTO tbCidade (Cidade) VALUES ('São Paulo');
INSERT INTO tbCidade (Cidade) VALUES ('Rio de Janeiro');

-- Inserindo bairros
INSERT INTO tbBairro (Bairro) VALUES ('Centro');
INSERT INTO tbBairro (Bairro) VALUES ('Jardim Botânico');

-- Inserindo endereços
INSERT INTO tbEndereco (cepEnd, numeroEnd, LogradouroEnd, ComplementoEnd, IdBai, IdEst, IdCid) 
VALUES (12345678, 123, 'Rua das Flores', 'Apt 45', 1, 1, 1);

INSERT INTO tbEndereco (cepEnd, numeroEnd, LogradouroEnd, ComplementoEnd, IdBai, IdEst, IdCid) 
VALUES (87654321, 456, 'Avenida Brasil', '', 2, 2, 2);

-- Inserindo usuários
INSERT INTO tbUsuario (EmailUsu, SenhaUsu, TelefoneUsu1, TelefoneUsu2, TipoUsu, IdEnd) 
VALUES ('user1@example.com', 'senha123', 11987654321, NULL, 'PJ', 1);

INSERT INTO tbUsuario (EmailUsu, SenhaUsu, TelefoneUsu1, TelefoneUsu2, IdEnd) 
VALUES ('user2@example.com', 'senha456', 21987654321, 21912345678, 2);

-- Inserindo clientes pessoa jurídica (PJ)
INSERT INTO tbPJ (Cnpj, RazaoSocial, NomeResponsavel, IdUsu) 
VALUES (12345678000199, 'Empresa XYZ Ltda', 'João Silva', 1);

-- Inserindo clientes pessoa física (PF)
INSERT INTO tbPF (Cpf, NomeCompleto, IdUsu) 
VALUES (12345678901, 'Maria Oliveira', 2);

-- Inserindo funcionários
INSERT INTO tbFuncionario (EmailFunc, NomeFunc, SenhaFunc) 
VALUES ('func1@empresa.com', 'Carlos Souza', 'senha789');

-- Inserindo categorias de produtos
INSERT INTO tbCategoria (NomeCategoria) 
VALUES ('Iluminação');

INSERT INTO tbCategoria (NomeCategoria) 
VALUES ('Segurança');

-- Inserindo produtos de automação
INSERT INTO tbProdutoAutomacao (CodBar, NomePro, PrecoPro, QtdEstoquePro, GarantiaPro, Voltagem, ImgUrlPro, IdCategoria) 
VALUES (7891234567891, 'Lâmpada Inteligente', 99.90, 100, '2025-09-10', 110, 'https://imageurl.com/lampada', 1);

INSERT INTO tbProdutoAutomacao (CodBar, NomePro, PrecoPro, QtdEstoquePro, GarantiaPro, Voltagem, ImgUrlPro, IdCategoria) 
VALUES (7899876543210, 'Câmera de Segurança', 299.90, 50, '2026-09-10', 110,  'https://imageurl.com/camera', 1);

INSERT INTO tbProdutoAutomacao (CodBar, NomePro, PrecoPro, QtdEstoquePro, GarantiaPro, Voltagem, ImgUrlPro, IdCategoria) 
VALUES (7899876543210, 'Câmera de Segurança Original', 299.90, 50, '2026-09-10', 110,  'https://imageurl.com/camera', 1);

-- Inserindo pagamentos
INSERT INTO tbPagamento (StatusPag, MetodoPag) 
VALUES ('Pendente', 'Cartão de Crédito');

INSERT INTO tbPagamento (StatusPag, MetodoPag) 
VALUES ('Pago', 'Boleto Bancário');

-- Inserindo notas fiscais
INSERT INTO tbNotaFiscal (ChaveAcessoNF, DataNF, ValorNF) 
VALUES (123456, '2024-09-12', 99.90);

INSERT INTO tbNotaFiscal (ChaveAcessoNF, DataNF, ValorNF) 
VALUES (3456, '2024-09-12', 299.90);

-- Inserindo pedidos
INSERT INTO tbPedido (DataPed, TotalPed, IdUsu, NumNF, IdPag, IdEnd) 
VALUES ('2024-09-12', 99.90, 1, 1, 1, 1);

INSERT INTO tbPedido (DataPed, TotalPed, IdUsu, NumNF, IdPag, IdEnd) 
VALUES ('2024-09-12', 299.90, 2, 2, 2, 2);

-- Inserindo itens do pedido
INSERT INTO tbItemPedido (QtdIte, PrecoIte, IdPed, Id) 
VALUES (1, 99.90, 1, 1);

INSERT INTO tbItemPedido (QtdIte, PrecoIte, IdPed, Id) 
VALUES (2, 299.90, 2, 2);

-- Inserindo favoritos
INSERT INTO tbFavoritos (Id, IdUsu) 
VALUES (1, 1);

INSERT INTO tbFavoritos (Id, IdUsu) 
VALUES (2, 2);

DELIMITER $$
create PROCEDURE sp_LoginUsuario(
    IN `p_email` VARCHAR(100),
    IN `p_senha` VARCHAR(30)
)
BEGIN
    DECLARE tipo_usuario CHAR(2);
    DECLARE id_usuario INT;

    -- Passo 1: Verificar se o usuário existe na tabela tbUsuario
    SELECT TipoUsu, IdUsu
    INTO tipo_usuario, id_usuario
    FROM tbUsuario
    WHERE EmailUsu = p_email AND SenhaUsu = p_senha;

    -- Se o usuário não for encontrado, retorna NULL
    IF tipo_usuario IS NULL THEN
        SELECT NULL AS IdUsu, NULL AS TipoUsu, NULL AS CPF, NULL AS CNPJ, NULL AS NomeCompleto, NULL AS RazaoSocial;
        -- Simplesmente termina aqui
    ELSE
        -- Passo 2: Buscar dados adicionais dependendo do tipo de usuário
        IF tipo_usuario = 'PF' THEN
            -- Buscar dados da tabela PF
            SELECT u.IdUsu, u.TipoUsu, u.EmailUsu, u.SenhaUsu, u.TelefoneUsu1, u.TelefoneUsu2, u.DataCadUsu, pf.IdPF, pf.Cpf, pf.NomeCompleto
            FROM tbUsuario u
            JOIN tbPF pf ON u.IdUsu = pf.IdUsu
            WHERE u.IdUsu = id_usuario;
        ELSEIF tipo_usuario = 'PJ' THEN
            -- Buscar dados da tabela PJ
            SELECT u.IdUsu, u.TipoUsu, u.EmailUsu, u.SenhaUsu, u.TelefoneUsu1, u.TelefoneUsu2, u.DataCadUsu, pj.IdPJ, pj.Cnpj, pj.RazaoSocial, pj.NomeResponsavel
            FROM tbUsuario u
            JOIN tbPJ pj ON u.IdUsu = pj.IdUsu
            WHERE u.IdUsu = id_usuario;
        END IF;
    END IF;
END $$
DELIMITER ;
call sp_LoginUsuario('joaolucas@gmail.com', 'senhaforte');


Delimiter $$
create procedure sp_InserirUsuarioPF(
_NomeCompleto varchar(200),
_Cpf numeric(11),
_EmailUsu varchar(100),
_SenhaUsu varchar(30),
_TelefoneUsu1 numeric(11),
_TelefoneUsu2 numeric(11)
)
begin
	declare _IdUsu int;
    
    insert into tbUsuario(EmailUsu, SenhaUsu, TelefoneUsu1, TelefoneUsu2, TipoUsu) values
    (_EmailUsu, _SenhaUsu, _TelefoneUsu1, _TelefoneUsu2, 'PF');
    
    set _IdUsu = LAST_INSERT_ID();
    
    insert into tbPF(Cpf, NomeCompleto, IdUsu) values
    (_Cpf, _NomeCompleto, _IdUsu);
    
end $$
call sp_InserirUsuarioPF('João Lucas', '35647386453', 'joaolucas@gmail.com', 'senhaforte', '11923453423', '11924356253');


Delimiter $$
create procedure sp_InserirUsuarioPJ(
_RazaoSocial varchar(250),
_Cnpj numeric(14),
_NomeResponsavel varchar(200),
_EmailUsu varchar(100),
_SenhaUsu varchar(30),
_TelefoneUsu1 numeric(11),
_TelefoneUsu2 numeric(11)
)
begin
	declare _IdUsu int;
    
    insert into tbUsuario(EmailUsu, SenhaUsu, TelefoneUsu1, TelefoneUsu2, TipoUsu) values
    (_EmailUsu, _SenhaUsu, _TelefoneUsu1, _TelefoneUsu2, 'PJ');
    
    set _IdUsu = LAST_INSERT_ID();
    
    insert into tbPJ(Cnpj, NomeResponsavel, RazaoSocial, IdUsu) values
    (_Cnpj, _NomeResponsavel, _RazaoSocial, _IdUsu);
    
end $$
call sp_InserirUsuarioPJ('JL Busness', '13245676543456', 'João Luis', 'jlbusnes@gmail.com', 'senha123', '1123232323', null);


Delimiter $$
create procedure sp_AtualizarUsuario(
-- tbPF
_NovoNomeCompleto varchar(200),
_NovoCpf numeric(11),

-- tbPJ
_NovoRazaoSocial varchar(250),
_NovoCnpj numeric(14),
_NovoNomeResponsavel varchar(200),

-- tbUsu
_NovoEmailUsu varchar(100),
_NovoSenhaUsu varchar(30),
_NovoTelefoneUsu1 numeric(11),
_NovoTelefoneUsu2 numeric(11),

-- chaves
_IdUsu int,
_IdPF int,
_IdPJ int
)
begin
    declare tipo_usuario char(2);
    select TipoUsu into tipo_usuario from tbUsuario where IdUsu = _IdUsu;
    
    update tbUsuario 
    set
        EmailUsu = COALESCE(_NovoEmailUsu, EmailUsu), -- COALESCE SÓ ALTERA O CAMPO SE NÃO FOR NULO
        TelefoneUsu1 = COALESCE(_NovoTelefoneUsu1, TelefoneUsu1),
        TelefoneUsu2 = COALESCE(_NovoTelefoneUsu2, TelefoneUsu2),
        SenhaUsu = COALESCE(_NovoSenhaUsu, SenhaUsu)
	where IdUsu = _IdUsu;
    
    IF tipo_usuario = 'PF' THEN
        update tbPF 
        set
			NomeCompleto = COALESCE(_NovoNomeCompleto, NomeCompleto),
			Cpf = COALESCE(_NovoCpf, Cpf)
		where IdPF  = _IdPF;
        
	ELSEIF tipo_usuario = 'PJ' THEN
        update tbPJ 
        set
			RazaoSocial = COALESCE(_NovoRazaoSocial, RazaoSocial),
			Cnpj = COALESCE(_NovoCnpj, Cnpj),
			NomeResponsavel = COALESCE(_NovoNomeResponsavel, NomeResponsavel)
		where IdPJ  = _IdPJ;
    END IF;
end $$

describe tbPJ


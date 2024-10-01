-- base de dados da empresa para o uso no Asp.net
-- feito por João Lucas
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
DataPed date not null,
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
CodBar numeric(13) ,
FOREIGN KEY(IdPed) REFERENCES tbPedido (IdPed)
);

CREATE TABLE tbFavoritos (
IdFav int PRIMARY KEY auto_increment,
CodBar numeric(13) ,
IdUsu int,
FOREIGN KEY(IdUsu) REFERENCES tbUsuario (IdUsu)
);

CREATE TABLE tbProdutoAutomacao (
CodBar numeric(13) PRIMARY KEY,
NomePro varchar(250) not null,
PrecoPro decimal(10,2) not null,
QtdEstoquePro int unsigned not null,
GarantiaPro date not null,
MarcaPro varchar(200) not null,
ModeloPro varchar(200) not null,
PesoPro decimal(10,3) not null,
AlturaPro decimal(10,2) not null,
LarguraPro decimal(10,2) not null,
ComprimentoPro decimal(10,2) not null,
ImgUrlPro varchar(255) not null,
IdFunc int,
IdCategoria int,
IdVoltagem int
);

CREATE TABLE tbVoltagem (
IdVoltagem int PRIMARY KEY auto_increment,
NumVoltagem int not null,
descVol varchar(200)
);

CREATE TABLE tbCategoria (
IdCategoria int PRIMARY KEY auto_increment,
NomeCategoria varchar(50) not null,
descCat varchar(200)
);

CREATE TABLE tbFuncionario (
IdFunc int PRIMARY KEY auto_increment,
DataEntradaFunc date not null,
EmailFunc varchar(250) not null unique,
NomeFunc varchar(200) not null,
SenhaFunc varchar(50) not null,
CargoFunc varchar(50) not null
);
ALTER TABLE tbPJ ADD FOREIGN KEY(IdUsu) REFERENCES tbUsuario (IdUsu);
ALTER TABLE tbPF ADD FOREIGN KEY(IdUsu) REFERENCES tbUsuario (IdUsu);
ALTER TABLE tbEndereco ADD FOREIGN KEY(IdBai) REFERENCES tbBairro (IdBai);
ALTER TABLE tbEndereco ADD FOREIGN KEY(IdEst) REFERENCES tbEstado (IdEst);
ALTER TABLE tbEndereco ADD FOREIGN KEY(IdCid) REFERENCES tbCidade (IdCid);
ALTER TABLE tbPedido ADD FOREIGN KEY(NumNF) REFERENCES tbNotaFiscal (NumNF);
ALTER TABLE tbPedido ADD FOREIGN KEY(IdPag) REFERENCES tbPagamento (IdPag);
ALTER TABLE tbItemPedido ADD FOREIGN KEY(CodBar) REFERENCES tbProdutoAutomacao (CodBar);
ALTER TABLE tbFavoritos ADD FOREIGN KEY(CodBar) REFERENCES tbProdutoAutomacao (CodBar);
ALTER TABLE tbProdutoAutomacao ADD FOREIGN KEY(IdFunc) REFERENCES tbFuncionario (IdFunc);
ALTER TABLE tbProdutoAutomacao ADD FOREIGN KEY(IdCategoria) REFERENCES tbCategoria (IdCategoria);
ALTER TABLE tbProdutoAutomacao ADD FOREIGN KEY(IdVoltagem) REFERENCES tbVoltagem (IdVoltagem);

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
INSERT INTO tbUsuario (EmailUsu, SenhaUsu, TelefoneUsu1, TelefoneUsu2, IdEnd) 
VALUES ('user1@example.com', 'senha123', 11987654321, NULL, 1);

INSERT INTO tbUsuario (EmailUsu, SenhaUsu, TelefoneUsu1, TelefoneUsu2, IdEnd) 
VALUES ('user2@example.com', 'senha456', 21987654321, 21912345678, 2);

-- Inserindo clientes pessoa jurídica (PJ)
INSERT INTO tbPJ (Cnpj, RazaoSocial, NomeResponsavel, IdUsu) 
VALUES (12345678000199, 'Empresa XYZ Ltda', 'João Silva', 1);

-- Inserindo clientes pessoa física (PF)
INSERT INTO tbPF (Cpf, NomeCompleto, IdUsu) 
VALUES (12345678901, 'Maria Oliveira', 2);

-- Inserindo funcionários
INSERT INTO tbFuncionario (DataEntradaFunc, EmailFunc, NomeFunc, SenhaFunc, CargoFunc) 
VALUES ('2024-01-01', 'func1@empresa.com', 'Carlos Souza', 'senha789', 'Gerente');

-- Inserindo categorias de produtos
INSERT INTO tbCategoria (NomeCategoria, descCat) 
VALUES ('Iluminação', 'Produtos de iluminação residencial');

INSERT INTO tbCategoria (NomeCategoria, descCat) 
VALUES ('Segurança', 'Equipamentos de segurança residencial');

-- Inserindo voltagens
INSERT INTO tbVoltagem (NumVoltagem, descVol) 
VALUES (110, 'Voltagem padrão 110V');

INSERT INTO tbVoltagem (NumVoltagem, descVol) 
VALUES (220, 'Voltagem padrão 220V');

-- Inserindo produtos de automação
INSERT INTO tbProdutoAutomacao (CodBar, NomePro, PrecoPro, QtdEstoquePro, GarantiaPro, MarcaPro, ModeloPro, PesoPro, AlturaPro, LarguraPro, ComprimentoPro, ImgUrlPro, IdFunc, IdCategoria, IdVoltagem) 
VALUES (7891234567891, 'Lâmpada Inteligente', 99.90, 100, '2025-09-10', 'Philips', 'HUE123', 0.300, 10.0, 5.0, 5.0, 'https://imageurl.com/lampada', 1, 1, 1);

INSERT INTO tbProdutoAutomacao (CodBar, NomePro, PrecoPro, QtdEstoquePro, GarantiaPro, MarcaPro, ModeloPro, PesoPro, AlturaPro, LarguraPro, ComprimentoPro, ImgUrlPro, IdFunc, IdCategoria, IdVoltagem) 
VALUES (7899876543210, 'Câmera de Segurança', 299.90, 50, '2026-09-10', 'Intelbras', 'CAM2021', 0.500, 15.0, 8.0, 8.0, 'https://imageurl.com/camera', 1, 2, 2);

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
INSERT INTO tbItemPedido (QtdIte, PrecoIte, IdPed, CodBar) 
VALUES (1, 99.90, 1, 7891234567891);

INSERT INTO tbItemPedido (QtdIte, PrecoIte, IdPed, CodBar) 
VALUES (2, 299.90, 2, 7899876543210);

-- Inserindo favoritos
INSERT INTO tbFavoritos (CodBar, IdUsu) 
VALUES (7891234567891, 1);

INSERT INTO tbFavoritos (CodBar, IdUsu) 
VALUES (7899876543210, 2);

select * from tbUsuario
select * from tbPJ
select * from tbPF

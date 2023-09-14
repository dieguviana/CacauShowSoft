create database Soft_CacauShow;
use Soft_CacauShow;

create table Cliente(
id_cli int primary key auto_increment,
nome_cli varchar (300),
data_nasc_cli date,
cpf_cli varchar(200),
rg_cli varchar(300),
contato_cli varchar(300),
email_cli varchar(100),
endereco_cli varchar(500),
cep_cli varchar(100),
uf_cli varchar(100),
bairro_cli varchar(100),
municipio_cli varchar(100)
);

create table Usuario(
id_usu int primary key auto_increment,
nome_usu varchar (100),
data_nasc_usu date,
rg_usu varchar(100),
cpf_usu varchar(100),
email_usu varchar(150),
funcao_usu varchar(100),
contato_usu varchar(100),
endereco_usu varchar(500),
cep_usu varchar(100),
uf_usu varchar(100),
bairro_usu varchar(100),
municipio_usu varchar(100)
);

create table Login(
id_log int primary key auto_increment,
hora_log time,
data_log date,
hora_logout_log time,
id_usu_fk int not null,
foreign key (id_usu_fk) references usuario (id_usu)
);

create table Fornecedor(
id_for int primary key auto_increment,
nome_for varchar(300),
email_for varchar(300),
cnpj_for varchar(300),
telefone_for varchar(300),
endereco_for varchar(500),
cep_for varchar(100),
uf_for varchar(100),
bairro_for varchar(100),
municipio_for varchar(100)
);

create table Produto(
id_pro int primary key auto_increment,
nome_pro varchar(300),
codigo_pro varchar(500),
data_venc_pro date,
valor_unit_pro float,
descricao_pro varchar(100)
);

create table Venda(
id_ven int primary key auto_increment,
data_ven date,
hora_ven time,
desconto_ven float,
parcelas_ven int,
form_pag_ven varchar(300),
id_usu_fk int,
foreign key (id_usu_fk) references Usuario (id_usu),
id_cli_fk int,
foreign key (id_cli_fk) references Cliente (id_cli)
);

create table Recebimento(
id_rec int primary key auto_increment,
vencimento_pag date,
valor_pag double,
valor_venda_pag double,
parcela_pag int,
id_ven_fk int not null,
foreign key (id_ven_fk) references venda (id_ven)
);

create table Compra(
id_com int primary key auto_increment,
data_com date,
forma_pag_com varchar(200),
valor_total_com float,
status_com varchar(200),
id_usu_fk int,
foreign key (id_usu_fk) references Usuario (id_usu),
id_for_fk int,
foreign key (id_for_fk) references Fornecedor(id_for),
id_pro_fk int,
foreign key (id_pro_fk) references Produto(id_pro)
);

create table Pagamento(
id_pag int primary key auto_increment, 
vencimento_pag date,
valor_pag double,
parcela_pag int,
id_com_fk int not null,
foreign key (id_com_fk) references compra (id_com)
);

create table Produto_Venda(
id_pro_ven int primary key auto_increment,
valor_pro_ven float,
id_pro_fk int,
foreign key (id_pro_fk) references Produto (id_pro),
id_ven_fk int,
foreign key (id_ven_fk) references Venda (id_ven)
);

create table Produto_Compra(
id_pro_com int primary key auto_increment,
quant_pro_com int,
valor_pro_com float,
id_pro_fk int,
foreign key (id_pro_fk) references Produto (id_pro),
id_com_fk int,
foreign key (id_com_fk) references Compra (id_com)
);

delimiter $$
create procedure InserirVenda(dia date, hora time, desconto float, parcelas int, forma_pagamento varchar(100), funcionario_fk int, cliente_fk int)
begin
if (parcelas <> '' and forma_pagamento <> '') then
	insert into Venda values (null, dia, hora, desconto, parcelas, forma_pagamento, funcionario_fk, cliente_fk);
else
	select 'O número de parcelas e a forma de pagamento são informações obrigatórias para cadastrar uma venda. Preencha essas informações e tente novamente';
end if;
end;
$$ delimiter ;

insert into Produto values (null, 'Chocolate', 1, '2024-09-14', 100, 'É de chocolate');
call InserirVenda(curdate(), curtime(), 0, 1, 'Dinheiro', 1, 1);
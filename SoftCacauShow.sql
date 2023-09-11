create database Soft_CacauShow;
use Soft_CacauShow;

create table Caixa(
id_cai int primary key auto_increment,
data_cai date,
saldo_cai double,
hora_abertura_cai time,
hora_fechamento_cai time,
saldo_final_cai double,
saldo_inicial_cai double
);

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

create table Funcionario(
id_fun int primary key auto_increment,
nome_fun varchar (100),
data_nasc_fun date,
rg_fun varchar(100),
cpf_fun varchar(100),
email_fun varchar(150),
funcao_fun varchar(100),
contato_fun varchar(100),
endereco_fun varchar(500),
cep_fun varchar(100),
uf_fun varchar(100),
bairro_fun varchar(100),
municipio_fun varchar(100)
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

create table Pagamento(
id_pag int primary key auto_increment, 
vencimento_pag date,
valor_pag double,
parcela_pag int,
id_for_fk int not null,
id_cai_fk int not null,
foreign key (id_for_fk) references fornecedor (id_for),
foreign key (id_cai_fk) references caixa (id_cai)
);

create table Venda(
id_ven int primary key auto_increment,
data_ven date,
desconto_ven float,
hora_ven time,
valor_ven float,
parcela_ven double,
form_pag_ven varchar(300),
id_fun_fk int,
foreign key (id_fun_fk) references Funcionario (id_fun),
id_cli_fk int,
foreign key (id_cli_fk) references Cliente (id_cli)
);

create table Recebimento(
id_rec int primary key auto_increment,
vencimento_pag date,
valor_pag double,
parcela_pag int,
id_ven_fk int not null,
id_cai_fk int not null,
foreign key (id_ven_fk) references venda (id_ven),
foreign key (id_cai_fk) references caixa (id_cai)
);

create table Login(
id_log int primary key auto_increment,
hora_log time,
data_log date,
hora_logout_log time,
id_fun_fk int not null,
foreign key (id_fun_fk) references funcionario (id_fun)
);

create table Compra(
id_com int primary key auto_increment,
data_com date,
forma_pag_com varchar(200),
valor_total_com float,
status_com varchar(200),
id_fun_fk int,
foreign key (id_fun_fk) references Funcionario(id_fun),
id_for_fk int,
foreign key (id_for_fk) references Fornecedor(id_for),
id_pro_fk int,
foreign key (id_pro_fk) references Produto(id_pro)
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

create table Produto_Venda(
id_pro_ven int primary key auto_increment,
valor_pro_ven float,
quant_pro_ven int,
id_pro_fk int,
foreign key (id_pro_fk) references Produto (id_pro),
id_ven_fk int,
foreign key (id_ven_fk) references Venda (id_ven)
);

select * from cliente;
insert into cliente values (null, 'diego', '2006-12-01', '000', '000', '000', '000', '000', '0000', '00', '999', '000');
insert into funcionario values (null, 'Diego', '2011-02-20', '02932', '0201', 'udiegoviana', 'motorista', '6999832', 'diado', '010220', 'Rondonia', 'Parque', 'ji-paraná');

insert into venda values (null, '2000-01-01', 111, '00:00:00', 2101, 1, 'Á vista', 1, 1);
select * from venda;
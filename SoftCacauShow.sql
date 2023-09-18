create database Soft_CacauShow;
use Soft_CacauShow;

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
valor_compra_pro double,
valor_venda_pro double,
descricao_pro varchar(100)
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

create table Produto_Compra(
id_pro_com int primary key auto_increment,
quant_pro_com int,
valor_pro_com float,
id_pro_fk int,
foreign key (id_pro_fk) references Produto (id_pro),
id_com_fk int,
foreign key (id_com_fk) references Compra (id_com)
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

create table Venda(
id_ven int primary key auto_increment,
data_hora_ven datetime,
id_usu_fk int,
foreign key (id_usu_fk) references Usuario (id_usu),
id_cli_fk int,
foreign key (id_cli_fk) references Cliente (id_cli)
);

create table Produto_Venda(
id_pro_ven int primary key auto_increment,
id_pro_fk int,
foreign key (id_pro_fk) references Produto (id_pro),
id_ven_fk int,
foreign key (id_ven_fk) references Venda (id_ven)
);

create table Recebimento(
id_rec int primary key auto_increment,
valor_venda_rec double,
desconto_rec double,
valor_entrada_rec double,
forma_pagamento_rec varchar(100),
id_ven_fk int,
foreign key (id_ven_fk) references venda (id_ven)
);

# Diego Viana

delimiter $$
create procedure InserirVenda(data_hora datetime, usuario_fk int, cliente_fk int)
begin
declare teste varchar(100);
set teste = (select id_cli from cliente where id_cli = cliente_fk);
	if (cliente_fk is not null) then
		if (teste <> null or teste <> 0) then
			insert into venda values (null, data_hora, usuario_fk, cliente_fk);
            select "Venda cadastrada com sucesso!" as 'Confirmação';
		else
			select "O cliente fornecido não existe no sistema. Realize o cadastro do cliente ou cadastre uma venda sem informar o cliente" as Erro;
		end if;
    else
		insert into venda values (null, data_hora, usuario_fk, null);
		select "Venda cadastrada com sucesso sem cliente!" as 'Atenção';
    end if;
end
$$ delimiter ;

delimiter $$
create procedure InserirProdutoVenda(produto_fk int, venda_fk int)
begin
	if (produto_fk <> null or produto_fk <> 0) then
		insert into Produto_Venda values (null, produto_fk, venda_fk);
        select 'Produto inserido com sucesso!' as 'Confirmação';
	else
		select 'É obrigatório informar o produto que deseja cadastrar na compra!' as 'Erro';
	end if;
end
$$ delimiter ;

delimiter $$

create procedure InserirRecebimento(valor_venda double, desconto double, valor_entrada double, forma_pagamento varchar(100), venda_fk int)
begin
	if (venda_fk <> 0) then
		insert into Recebimento values (null, valor_venda, desconto, valor_entrada, forma_pagamento, venda_fk);
        select 'Recebimento cadastrado com suceso!' as 'Confirmação';
	else
		insert into Recebimento values (null, valor_venda, desconto, valor_entrada, forma_pagamento, venda_fk);
        select 'Recebimento cadastrado com sucesso sem informar de qual venda!' as 'Atenção';
    end if;
end
$$ delimiter ;

insert into Usuario values (null, 'Thauany', '2000-01-01', '1111111-11', '000.000.000-00', 'thauany@celestino.com', 'Gerente', '69984777384', 'Nova Londrina', '00', 'RO', 'Não informado', 'Ji-Paraná');
insert into Usuario values (null, 'Niic', '2000-01-01', '1111111-11', '000.000.000-00', 'niic@celestino.com', 'Gerente', '69984777384', 'Jipa', '00', 'RO', 'Não informado', 'Ji-Paraná');
insert into Usuario values (null, 'Jussara', '2000-01-01', '1111111-11', '000.000.000-00', 'jussara@celestino.com', 'Gerente', '69984777384', 'Jipa', '00', 'RO', 'Não informado', 'Ji-Paraná');
insert into Produto values (null, 'Chocolate', '01010101', '2024-09-17', 1, 2.50, 'Barra de chocolate');
insert into Produto values (null, 'Chocolate de Morango', '0202020202', '2024-09-17', 1, 2.50, 'Barra de morango');
insert into Produto values (null, 'Chocolate de Maracujá', '030303033', '2024-09-17', 1, 2.50, 'Barra de maracujá');
insert into Cliente values (null, 'Diego', '2000-01-01', '111.111.111-11', '01293-11', '69 9 8477-7384', 'diegu@gmail.com', 'Rua Dario', '919191', 'RO', 'Parque', 'Ji-Pa');
insert into Cliente values (null, 'Hilary', '2000-01-01', '111.111.111-11', '01293-11', '69 9 8477-7384', 'hilary@gmail.com', 'Rua Dario', '919191', 'RO', 'Parque', 'Ji-Pa');
insert into Cliente values (null, 'Emily', '2000-01-01', '111.111.111-11', '01293-11', '69 9 8477-7384', 'emily@gmail.com', 'Rua Dario', '919191', 'RO', 'Parque', 'Ji-Pa');

call InserirVenda('2025-09-17 00:00:00', 1, 1);
call InserirProdutoVenda(1,1);
call InserirProdutoVenda(2,1);
call InserirProdutoVenda(3,1);
call InserirRecebimento(10, 0, 10, 'Cartão de débito', 1);
call InserirVenda('2024-09-17 23:00:00', 2, 2);
call InserirProdutoVenda(1,2);
call InserirProdutoVenda(2,2);
call InserirProdutoVenda(3,2);
call InserirRecebimento(10, 0, 10, 'Cartão de crédito', 2);
call InserirVenda('2022-09-17 21:30:10', 3, 4); #Cliente que não existe no sistema
call InserirVenda('2022-09-17 21:30:10', 3, null); #Sem informar cliente
call InserirProdutoVenda(1,3);
call InserirProdutoVenda(2,3);
call InserirProdutoVenda(null,3); #Sem informar produto
call InserirRecebimento(10, 0, 10, 'Dinheiro', null); #Sem informar de qual venda

select * from recebimento;
select * from produto_venda;
select * from venda;


# PRODUTO
DELIMITER $$
create procedure InserirProduto (
  nome VARCHAR(200), 
  codigo VARCHAR(200), 
  data_venc DATE, 
  valor_compra DOUBLE, 
  valor_venda DOUBLE, 
  descricao VARCHAR(200)
)
begin
	declare teste_cod varchar (300);
	set teste_cod = (select codigo_pro from produto where codigo_pro = codigo );
		if (teste_cod = '') or (teste_cod is null) then    
			insert into produto values (null, nome, codigo, data_venc, valor_compra, valor_venda, descricao);
			select concat('O Produto ', nome, ' foi salvo com sucesso!') AS Confirmacao;
		else
			select 'O Produto informado já está cadastrado!' AS Alerta;
  end if;
end;
$$
DELIMITER ;

call InserirProduto('Trufa', '934794794279', '2030-09-08', '20.00', '40.00', 'recheio de maracujá');

select * from produto;

#alterei
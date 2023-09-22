#RESPONSABILIDADE DAS TABELAS DO BANCO DE DADOS
#Cliente - Jussara
#Usuario, Login - Emily
#Fornecedor - Niic
#Produto - Thauany
#Compra, Produto_Compra, Pagamento - Hilary
#Venda, Produto_Venda, Recebimento - Diego

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

insert into Usuario values (null, 'Diego', '2000-01-01', '1', '1', 'u@g', 'Atendente', '69', 'Rua', '1', 'RO', 'Pq', 'Ji');
insert into Cliente values (null, 'Hilary', '2005-10-10', '101.313.123-12', '21231', '699847773844', 'udiegoviana@gmail.com', 'Rua', '13121', 'RO', 'Parque', 'ji-paraná');
insert into Venda values (null, '2023-09-21 00:00:00', 1, 1);
insert into Recebimento values (null, 2, 0, 2, 'Á vista', 1);
insert into Produto values (null, 'Chocolate', '11321', '2024-09-21', 1, 2, '');
insert into Produto_Venda values (1, 1);

insert into Usuario values (null, 'Thauany', '2000-01-01', '1', '1', 'u@g', 'Atendente', '69', 'Rua', '1', 'RO', 'Pq', 'Ji');
insert into Cliente values (null, 'Niic', '2005-10-10', '101.313.123-12', '21231', '699847773844', 'udiegoviana@gmail.com', 'Rua', '13121', 'RO', 'Parque', 'ji-paraná');
insert into Venda values (null, '2023-09-21 00:10:00', 2, 2);
insert into Recebimento values (null, 2, 0, 2, 'Parcelado', 2);
insert into Produto values (null, 'Morango', '2121', '2024-09-21', 1, 2, '');
insert into Produto_Venda values (2, 2);

select
Venda.id_ven as "Código",
Venda.data_hora_ven as "Data e hora",
Usuario.nome_usu as "Usuário",
Cliente.nome_cli as "Cliente",
Recebimento.valor_venda_rec as "Valor",
Recebimento.desconto_rec as "Desconto",
Recebimento.valor_entrada_rec as 'Valor Entrada',
Recebimento.forma_pagamento_rec as 'Forma de Pagamento'
from
Venda, Usuario, Cliente, Recebimento
where
(Usuario.id_usu = Venda.id_usu_fk) and
(Cliente.id_cli = Venda.id_cli_fk) and
(Recebimento.id_ven_fk = Venda.id_ven);

select
Produto.codigo_pro as 'Código',
Produto.nome_pro as 'Produto',
Produto.valor_venda_pro as 'Valor unitário'
from
Produto, Produto_Venda
where
(Produto_Venda.id_ven_fk is not null) and
(Produto_Venda.id_pro_fk = Produto.id_pro);

/*
#Emily
delimiter $$
create procedure cadastrar_usuario (nome varchar (100), data date, rg varchar(100), cpf varchar(100), email varchar(150), funcao varchar(100), contato varchar(100), endereco varchar(500), cep varchar(100), uf varchar(100), bairro varchar(100), municipio varchar(100))
begin
declare usar_cpf varchar (300);

set usar_cpf = (select cpf_usu from usuario where (cpf_usu = cpf));

if  (cpf <> '') then 
	if (usar_cpf = '') or (usar_cpf is null) then         
				insert into usuario values (null, nome, data, rg, cpf, email, funcao, contato, endereco, cep, uf, bairro, municipio);
                select concat('O usuário ', nome, ' foi inserido com sucesso!') as Confirmacao;
    else
		select 'O CPF informado já esta cadastrado!' as Alerta;
    end if;
else
	select 'O campo cpf é obrigatório!' as Alerta;
end if;
end;
$$ delimiter ;

call cadastrar_usuario ('emilinha linda', '2111-04-12', '455786544', '456.125.356-95',  'batatas.com', 'operador de caixa' , '69 9 54887965', 'cracola', '8555548', 'RO', 'jardim verde', 'bolinha');
call cadastrar_usuario ('emilinha linda', '2111-04-12', '455786544', '458.125.356-95',  'batatas.com', 'operador de caixa' , '69 9 54887965', 'cracola', '8555548', 'RO', 'jardim verde', 'bolinha');
call cadastrar_usuario ('emilinha linda', '2111-04-12', '455786544', '457.125.356-95',  'batatas.com', 'operador de caixa' , '69 9 54887965', 'cracola', '8555548', 'RO', 'jardim verde', 'bolinha');

select * from usuario;

#jussara
DELIMITER $$
CREATE PROCEDURE InserirCliente (
  nome VARCHAR(300), 
  data_nasc DATE, 
  cpf VARCHAR(200), 
  rg VARCHAR(300), 
  contato VARCHAR(300),
  email VARCHAR(100),
  endereco VARCHAR(500),
  cep VARCHAR(100),
  uf VARCHAR(100),
  bairro VARCHAR(100),
  municipio VARCHAR(100)
)
BEGIN
  DECLARE teste_cpf VARCHAR(200);
  SET teste_cpf = (SELECT cpf_cli FROM Cliente WHERE cpf_cli = cpf);
  
  IF (teste_cpf = '' OR teste_cpf IS NULL) THEN    
    INSERT INTO Cliente VALUES (NULL, nome, data_nasc, cpf, rg, contato, email, endereco, cep, uf, bairro, municipio);
    SELECT 'O Cliente foi salvo com sucesso!' AS Confirmacao;
  ELSE
    SELECT 'O Cliente informado já existe!' AS Alerta;
  END IF;
END;
$$
DELIMITER ;

CALL InserirCliente('João Silva', '1990-01-01', '12345678901', '12345678', '(11) 98765-4321', 'joao@gmail.com', 'Rua das Flores, 123', '01000-000', 'SP', 'Centro', 'São Paulo');
CALL InserirCliente('Maria Santos', '1995-02-02', '12345678902', '87654321', '(22) 98765-4321', 'maria@gmail.com', 'Avenida Brasil, 456', '02000-000', 'SP', 'Copacabana', 'Rio de Janeiro');
CALL InserirCliente('Pedro Souza', '1998-03-03', '12345678903', '54321678', '(33) 98765-4321', 'pedro@gmail.com', 'Rua dos Cravos, 789', '03000-000', 'SP', 'Vila Nova', 'Belo Horizonte');

select * from cliente;

#Niic Dias
DELIMITER $$
create procedure InserirFornecedor (
  nome varchar(300), 
  email varchar(300), 
  cnpj varchar(100), 
  telefone varchar(100), 
  endereco varchar(500),
  cep varchar(100),
  uf varchar(100),
  bairro varchar(100),
  municipio varchar(100)
)
begin
	declare teste_cnpj varchar (100);
	set teste_cnpj = (select cnpj_for from Fornecedor where cnpj_for = cnpj);
	
			if (teste_cnpj = '') or (teste_cnpj is null) then    
				insert into Fornecedor values (null, nome, email, cnpj, telefone, endereco, cep, uf, bairro, municipio);
				select ('O Fornecedor foi salvo com sucesso!') as Confirmacao;
			else
				select 'O Fornecedor informado já está existe!' as Alerta;
		end if;
        
end;
$$
DELIMITER ;

call InserirFornecedor('J', 'fornecedorJ@email.com', '8928942883498934', '(11) 98765-4321', 'Rua A, 123', '01000-000', 'SP', 'Bairro A', 'Cidade A');
call InserirFornecedor('H', 'fornecedorH@email.com', '98408938948798938', '(22) 98765-4321', 'Rua B, 456', '02000-000', 'SP', 'Bairro B', 'Cidade B');
call InserirFornecedor('I', 'fornecedorI@email.com', '8928942883498934', '(33) 98765-4321', 'Rua C, 789', '03000-000', 'SP', 'Bairro C', 'Cidade C');

SELECT * FROM Fornecedor;

#Thauany Celestino
#PRODUTO
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
  
  if codigo <> '' then
		if (teste_cod = '') or (teste_cod is null) then    
			insert into produto values (null, nome, codigo, data_venc, valor_compra, valor_venda, descricao);
			select concat('O Produto ', nome, ' foi salvo com sucesso!') AS Confirmacao;
		else
			select 'O Produto informado já está cadastrado!' AS Alerta;
  end if;
   else
  select 'O campo codigo deve ser preenchido!' as Erro;
  end if;
end;
$$
DELIMITER ;

call InserirProduto('Trufa de Morango', '883283389238378', '2030-09-08', '2.00', '4.00', 'recheio de morango');
call InserirProduto('Trufa de Morango', '883283389238379', '2030-09-08', '2.00', '4.00', 'recheio de morango');
call InserirProduto('Trufa de Morango', '883283389238370', '2030-09-08', '2.00', '4.00', 'recheio de morango');
call InserirProduto('Trufa de limão', '', '2030-09-08', '10.00', '30.00', 'recheio de maracujá'); #Não vai ser inserido porque o campo está vazio.
call InserirProduto('La creme', '883283389238378', '2030-09-08', '5.00', '10.00', 'recheio de maracujá'); #Não vai ser inserido porque o codigo informado já existe.
SELECT * FROM Produto;

#Hilary Souza de Oliveira 
delimiter $$
create procedure InserirCompra(
data_com date,
forma_pag varchar(100), 
valor_total float,
status_com varchar(100),
id_usu_fk int,
id_for_fk int,
id_pro_fk int)
begin
	if(id_usu_fk <> null or id_usu_fk <> 0) then
			insert into Compra values(null, data_com, forma_pag, valor_total,status_com, id_usu_fk, id_for_fk, id_pro_fk);
		select 'Compra inserida com sucesso' as 'Confirmação';
        else
			select 'É obrigatório informar o Usuário, Fornecedor e Produto da Compra!' as 'Erro';
		end if;
	end;
$$ delimiter ;

call InserirCompra('2025-09-20', 'cartão','100.00','pago','1','2','3');
call InserirCompra('2024-09-20', 'cartão','100.00','pago','1','2','3');
call InserirCompra('2023-09-20', 'cartão','100.00','pago','1','2','3');

#Hilary Souza de Oliveira 
delimiter $$
create procedure InserirProdutoCompra(quant int,valor float, produto_fk int, compra_fk int)
	begin
    declare teste varchar(100);
    set teste = (select id_pro from produto where id_pro = produto_fk);
   
		if(produto_fk is not null <> null or produto_fk <> 0) then
				insert into Produto_Compra values (null, produto_fk, compra_fk);
            select 'Produto inserido com sucesso!' as 'Confirmação';
            else
					select 'É obrigatório informar o produto da compra!' as 'Erro';
			end if;
	end;
            
    
$$ delimiter ;

select * from Compra;

#Diego Viana
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
end;
$$ delimiter ;

call InserirVenda('2025-09-17 00:00:00', 1, 1);
call InserirVenda('2024-09-17 23:00:00', 2, 2);
call InserirVenda('2022-09-17 21:30:10', 3, 4); #Da erro pelo cliente não existir no sistema
call InserirVenda('2022-09-17 21:30:10', 3, null); #Insere, mas da um 'Atenção'
select * from Venda;

#Diego Viana
delimiter $$
create procedure InserirProdutoVenda(produto_fk int, venda_fk int)
begin
	if (produto_fk <> null or produto_fk <> 0) then
		insert into Produto_Venda values (null, produto_fk, venda_fk);
        select 'Produto inserido com sucesso!' as 'Confirmação';
	else
		select 'É obrigatório informar o produto que deseja cadastrar na compra!' as 'Erro';
	end if;
end;
$$ delimiter ;

call InserirProdutoVenda(1,1);
call InserirProdutoVenda(2,1);
call InserirProdutoVenda(null,1); #Da erro por não informar o produto
call InserirProdutoVenda(3,1);
select * from produto_venda;

select * from produto;

#Diego Viana
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
end;
$$ delimiter ;

select * from venda;

call InserirRecebimento(10, 0, 10, 'Cartão de débito', 1);
call InserirRecebimento(10, 0, 10, 'Cartão de crédito', 2);
call InserirRecebimento(10, 0, 10, 'Dinheiro', null); #Cadastra, mas da um 'Atenção' por não informar de qual venda é o recebimento

select
Produto_Venda.id_pro_ven,
Produto.nome_pro,
Produto.valor_venda_pro
from
Produto, Produto_Venda, Venda
where
(Produto_Venda.id_ven_fk = Venda.id_ven) and
(Produto_Venda.id_pro_fk = Produto.id_pro);*/
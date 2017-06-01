INSERT INTO `db_psi`.`atendente`
(`idatendente`,
`funcao`)
VALUES
(1,
1);

INSERT INTO `db_psi`.`veterinario`
(`idveterinario`,
`numero_crmv`)
VALUES
(1,
"12345687");

INSERT INTO `db_psi`.`funcionario`
(`idfuncionario`,
`numero_contrato`,
`salario`,
`setor`,
`data_admisão`,
`atendente_idatendente`,
`veterinario_idveterinario`)
VALUES
(1,
"123456789",
1500.00,
2,
2016-12-08,
1,
null);

INSERT INTO `db_psi`.`funcionario`
(`idfuncionario`,
`numero_contrato`,
`salario`,
`setor`,
`data_admisão`,
`atendente_idatendente`,
`veterinario_idveterinario`)
VALUES
(2,
"123452789",
3000.00,
1,
now(),
null,
1);


INSERT INTO `db_psi`.`cliente`
(`idcliente`,
`email`)
VALUES
(1,
"lucas@teste.com.br");

INSERT INTO `db_psi`.`pessoa`
(`nome`,
`cpf`,
`data_nascimento`,
`telefone_fixo`,
`telefone_celular`,
`endereco`,
`complemento`,
`numero`,
`bairro`,
`cliente_idcliente`,
`funcionario_idfuncionario`,
`cidade_idcidade`)
VALUES
("Gabriel",
"11354878894",
now(),
"3112548795",
"31987456897",
"rua teste",
"apt",
275,
"Planalto",
null,
2,
1630);

INSERT INTO `db_psi`.`pessoa`
(`nome`,
`cpf`,
`data_nascimento`,
`telefone_fixo`,
`telefone_celular`,
`endereco`,
`complemento`,
`numero`,
`bairro`,
`cliente_idcliente`,
`funcionario_idfuncionario`,
`cidade_idcidade`)
VALUES
("lucas",
"11354878894",
1995-11-22,
"3112548795",
"31987456897",
"rua teste",
"apt",
274,
"Planalto",
1,
null,
1630);

INSERT INTO `db_psi`.`pessoa`
(`nome`,
`cpf`,
`data_nascimento`,
`telefone_fixo`,
`telefone_celular`,
`endereco`,
`complemento`,
`numero`,
`bairro`,
`cliente_idcliente`,
`funcionario_idfuncionario`,
`cidade_idcidade`)
VALUES
("igor",
"11354878894",
1995-11-22,
"3112548795",
"31987456897",
"rua teste",
"apt",
274,
"Planalto",
null,
1,
1631);


INSERT INTO `db_psi`.`produto`
(`idproduto`,
`nome`,
`descricao`,
`valor`,
`qtd_estoque`)
VALUES
(1,
"Shampoo",
"ashsah sahsha hash ashs a",
15,
150);

INSERT INTO `db_psi`.`venda`
(`idvenda`,
`data`,
`valor_total`,
`forma_pgto`,
`atendente_idatendente`,
`cliente_idcliente`)
VALUES
(1,
now(),
30,
1,
1,
1);


INSERT INTO `db_psi`.`venda_has_produto`
(`venda_idvenda`,
`produto_idproduto`)
VALUES
(1,
1);


INSERT INTO `db_psi`.`pet`
(`idpet`,
`pessoa_idpessoa`,
`nome`,
`data_nascimento`,
`raca`,
`sexo`,
`tipo`,
`cliente_idcliente`)
VALUES
(1,
1,
"Rosk",
now(),
null,
null,
1,
1);


INSERT INTO `db_psi`.`consulta`
(`idconsulta`,
`data`,
`pet_idpet`,
`veterinario_idveterinario`)
VALUES
(1,
now(),
1,
1);

INSERT INTO `db_psi`.`diagnostico`
(`iddiagnostico`,
`posologia`,
`medicacao`,
`descicao`,
`consulta_idconsulta`)
VALUES
(1,
"2 doses",
"dipirona",
"febre",
1);





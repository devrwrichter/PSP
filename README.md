# PSP
Construção de uma versão super simplificada de um Payment Service Provider (PSP) e talvez aprender um pouco mais sobre como funcionam pagamentos no Brasil.

Oi! Tudo bem? Espero que sim, abaixo estão as informações! 

Como executar o projeto?

	1 - Abra a solution Stone.PSP.sln
	2 - No topo escolha a opção Debug ou Release
		2.1 - Em debug você conseguirá colocar break points e andar passo a passo (mas com certeza você já sabe disso);
		2.2 - Em release a aplicação vai rodar um pouco mais rápido;
	3 - No topo escolha a opção docker-compose e clique no play (botão verde)
	4 - Vai demorar um pouco para ele construir os containeres, logo que terminar ele abrirá a página: https://localhost:52961/swagger/index.html
		4.1 -  Ele abrirá o swagger com todos os endpoints disponíveis, mas eu vou falar de cada um deles abaixo:
	
[healthCheck] --> Para ver a saúde da aplicação	
	https://localhost:52961/health

[Controller CashIn -Endpoints]

	GetById --> Obtém uma Transaction pelo Id, eu utilizo esse endpoint para fornecer o location do endpoint ProcessTransaction;
	GetTransactions --> Obtém todas as transações existentes no banco de dados;
	GetTransactionsWithPagination --> Obtém as Transactions por quantidade definida por vc no payLoad;
	ProcessTransactions --> Esse é o mais importante, ele cria a Transaction e também a Payable;

	GetPayableStatus --> Só prá ajudar como eu configurei os enums (Não daria tempo criando as tabelas de domínio)
	GetPaymentMethods --> Só prá ajudar como eu configurei os enums (Não daria tempo criando as tabelas de domínio)


[Controller CashOut - Endpoints]

	GetBalance --> Esse endpoint obtém o saldo do cliente;
	
[Olhando o banco de dados]

	Eu usei o SQLServer, para acessar as tabelas no container sql basta usar as informações abaixo na sua IDE preferida:
	Authentication: SqlServerAuthentication
	Login: sa
	Password: Psp@2023@ps
	
[Cenários de teste]
	Vou disponibilizar uma collection do Postman na pasta do projeto, ela contém alguns cenários de erro, ao todo há 16 cenários para todos os endpoints.
	Algumas vezes é preciso mudar a porta nos environments do Postman, pois as vezes start numa porta diferente.

	Importar no Postman: Develop.postman_environment.json, Payment Service Provider.postman_collection.json

[Testes Unitários]
	- Podem ser rodados pelo TestExplorer do VisualStudio;

[Testes de Integração]
	- Podem ser rodados pelo TestExplorer do VisualStudio;
	- Eu fiz testes de integração que cobrem Controller, Application e Repositórios(Mock);
	
[Possíveis erros que podem ocorrer]

	Algumas vezes pode dar erro na criação dos containeres, mas é só restar, ou deletar o container que "sobrou" no docker e startar de novo.


	Boa sorte! Um forte abraço.

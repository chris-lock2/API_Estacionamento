# API_Estacionamento

Este projeto utiliza Puppeteer para automatizar a geração de dados fictícios de pessoas e veículos no site 4Devs.

Pré-requisitos
.NET Core SDK
Node.js
Google Chrome instalado no caminho C:\Program Files\Google\Chrome\Application\chrome.exe
Configuração
1. Clone este repositório: 
git clone [https://github.com/seu-usuario/gerador-pessoas-veiculos.git](https://github.com/chris-lock2/API_Estacionamento.git) 
cd gerador-pessoas-veiculos

2. Restaure as dependências do projeto:
dotnet restore

3. Instale PuppeteerSharp:
dotnet add package PuppeteerSharp

4. Instale Newtonsoft.Json:
dotnet add package Newtonsoft.Json

# Executando o Projeto
1. Compile o projeto:
dotnet build

2. Execute o projeto:
dotnet run

3. Quando solicitado, insira a quantidade de pessoas a serem geradas (entre 1 e 30).

4. Aguarde enquanto o programa gera os dados e baixa o arquivo JSON contendo as informações.

# Estrutura do Projeto
Program.cs: Contém a lógica principal para automatizar a geração de dados.
PessoaHelper.cs: Contém métodos auxiliares para ler e processar os dados JSON.
Model/PessoaModel.cs: Define a estrutura dos dados de uma pessoa.
Model/VeiculoModel.cs: Define a estrutura dos dados de um veículo.

# Notas
Certifique-se de que o Google Chrome está instalado no caminho padrão (C:\Program Files\Google\Chrome\Application\chrome.exe). Se estiver em um caminho diferente, atualize a variável chromePath no código conforme necessário.
O programa irá abrir uma janela do Chrome para executar as automações, então não feche a janela enquanto o programa estiver em execução.

# Contribuindo
Sinta-se à vontade para abrir issues ou enviar pull requests. Toda contribuição é bem-vinda!

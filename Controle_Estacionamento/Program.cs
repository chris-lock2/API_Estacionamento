using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PuppeteerSharp;
using Newtonsoft.Json;
using Controle_Estacionamento.Model;

class Program
{
    static async Task Main(string[] args)
    {
        var chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

        using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = false,  // Defina como true para rodar em modo headless
            ExecutablePath = chromePath,
            Args = new[]
            {
                "--window-position=1000,1000",  // Define a posição da janela
                "--window-size=800,600",        // Define o tamanho da janela
                "--start-maximized"             // Opcional: Maximiza a janela
            }
        }))
        {
            using (var page = await browser.NewPageAsync())
            {
                // Navegar até a URL https://www.4devs.com.br/gerador_de_pessoas
                await page.GoToAsync("https://www.4devs.com.br/gerador_de_pessoas");

                Console.Write("Digite a quantidade de pessoas a gerar (1-30): ");
                var quantidade = Console.ReadLine();

                await page.EvaluateExpressionAsync($"document.querySelector('#txt_qtde').value = '{quantidade}';");
                Console.WriteLine("Aguarde, está sendo gerado...");

                var gerarButton = await page.QuerySelectorAsync("#bt_gerar_pessoa");
                await gerarButton.ClickAsync();

                await Task.Delay(5000);

                var jsonTabButton = await page.QuerySelectorAsync("#btn_json_tab");
                await jsonTabButton.ClickAsync();

                // Clicar no botão para baixar o JSON usando XPath direto
                var downloadButton = await page.XPathAsync("//*[@id='area_resposta_json']/div/button[1]");
                await downloadButton[0].ClickAsync();

                await Task.Delay(5000);  // Ajuste o tempo conforme necessário

                var downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "data.json");

                var listaPessoas = await PessoaHelper.LerPessoas(downloadPath);

                await page.GoToAsync("https://www.4devs.com.br/gerador_de_veiculos");

                foreach (var pessoa in listaPessoas)
                {
                    var gerarVeiculoButton = await page.QuerySelectorAsync("#bt_gerar");
                    await gerarVeiculoButton.ClickAsync();

                    await Task.Delay(5000);

                    pessoa.Veiculo = new VeiculoModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Marca = await page.EvaluateExpressionAsync<string>("document.querySelector('#marca').value"),
                        Modelo = await page.EvaluateExpressionAsync<string>("document.querySelector('#modelo').value"),
                        Ano = await page.EvaluateExpressionAsync<string>("document.querySelector('#ano').value"),
                        Placa = await page.EvaluateExpressionAsync<string>("document.querySelector('#placa_veiculo').value")
                    };
                }

                foreach (var pessoa in listaPessoas)
                {
                    Console.WriteLine($"ID: {pessoa.Id}");
                    Console.WriteLine($"Nome: {pessoa.Nome}");
                    Console.WriteLine($"CPF: {pessoa.CPF}");
                    Console.WriteLine($"Data de Nascimento: {pessoa.DataNascimento.ToShortDateString()}");
                    Console.WriteLine($"Estado: {pessoa.Estado}");
                    Console.WriteLine($"Telefone: {pessoa.Telefone}");
                    if (pessoa.Veiculo != null)
                    {
                        Console.WriteLine($"Veículo ID: {pessoa.Veiculo.Id}");
                        Console.WriteLine($"Marca: {pessoa.Veiculo.Marca}");
                        Console.WriteLine($"Modelo: {pessoa.Veiculo.Modelo}");
                        Console.WriteLine($"Ano: {pessoa.Veiculo.Ano}");
                        Console.WriteLine($"Placa: {pessoa.Veiculo.Placa}");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}

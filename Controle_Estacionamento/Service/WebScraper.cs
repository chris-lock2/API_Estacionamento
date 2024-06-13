using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estacionamento.Service
{
    public class WebScraper
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string urlPessoas = "https://www.4devs.com.br/gerador_de_pessoas";
        private static readonly string downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "data.json");

        public static async Task<string> BaixarJsonAsync(int quantidade)
        {
            try
            {
                var formData = new MultipartFormDataContent();
                formData.Add(new StringContent(quantidade.ToString()), "txt_qtde");

                var response = await client.PostAsync(urlPessoas, formData);
                response.EnsureSuccessStatusCode();

                await Task.Delay(5000);

                var jsonUrl = "https://www.4devs.com.br/gerador_de_pessoas?json=true";
                var jsonBytes = await client.GetByteArrayAsync(jsonUrl);
                await File.WriteAllBytesAsync(downloadPath, jsonBytes);

                return downloadPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao baixar JSON: {ex.Message}");
                return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Controle_Estacionamento.Model;

public static class PessoaHelper
{
    public static async Task<List<PessoaModel>> LerPessoas(string filePath)
    {
        try
        {
            var jsonContent = await File.ReadAllTextAsync(filePath);
            var pessoas = JsonConvert.DeserializeObject<List<dynamic>>(jsonContent);

            var listaPessoas = new List<PessoaModel>();

            foreach (var pessoa in pessoas)
            {
                DateTime dataNascimento;
                if (!DateTime.TryParse(pessoa.data_nasc.ToString(), out dataNascimento))
                {
                    throw new FormatException($"Data de nascimento inválida para {pessoa.nome}");
                }

                listaPessoas.Add(new PessoaModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Nome = pessoa.nome,
                    CPF = pessoa.cpf,
                    DataNascimento = dataNascimento,
                    Estado = pessoa.estado,
                    Telefone = pessoa.telefone_fixo
                });
            }

            return listaPessoas;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao ler o arquivo: {ex.Message}");
            return new List<PessoaModel>();
        }
    }
}

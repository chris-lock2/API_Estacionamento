using Controle_Estacionamento.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estacionamento.Service
{
    public static class JsonHelper
    {
        public static List<PessoaModel> LerPessoas(string filePath)
        {
            var jsonContent = File.ReadAllText(filePath);
            var pessoas = JsonConvert.DeserializeObject<List<dynamic>>(jsonContent);

            var listaPessoas = new List<PessoaModel>();

            foreach (var pessoa in pessoas)
            {
                listaPessoas.Add(new PessoaModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Nome = pessoa.Nome,
                    CPF = pessoa.CPF,
                    DataNascimento = DateTime.Parse(pessoa.DataNascimento),
                    Estado = pessoa.Estado,
                    Telefone = pessoa.Telefone
                });
            }

            return listaPessoas;
        }
    }
}

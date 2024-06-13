using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estacionamento.Model
{
    public class PessoaModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }
        public VeiculoModel Veiculo { get; set; }
    }
}

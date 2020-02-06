using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Funcionario
    {
        public int ID  { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public bool EhAtivo { get; set; }
        public virtual ICollection<Locacao> Locacoes { get; set; }

        public Funcionario()
        {

        }

        public Funcionario(int id, string nome, string email, string cpf, DateTime dataNascimento, string telefone, string senha, bool ehAtivo)
        {
            ID = id;
            Nome = nome;
            Email = email;
            CPF = cpf;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Senha = senha;
            EhAtivo = ehAtivo;
        }
    }
}

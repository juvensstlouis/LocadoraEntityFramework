using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Genero
    {
        //Por convention, no EF um campo inteiro chamado ID
        //gerará uma Primary KEY Identity
        public int ID { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Filme> Filmes { get; set; }

        public Genero()
        {

        }

        public Genero(int id, string nome)
        {
            ID = id;
            Nome = nome;
        }
    }
}

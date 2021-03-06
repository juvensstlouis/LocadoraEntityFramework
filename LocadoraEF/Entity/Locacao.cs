﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Locacao 
    {
        public int ID { get; set; }
        public int ClienteID { get; set; }
        public virtual Cliente Cliente { get; set; }
        public int FuncionarioID { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public double Preco { get; set; }
        public double Multa { get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime DataPrevistaDevolucao { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public bool FoiPago { get; set; }
        public virtual ICollection<Filme> Filmes { get; set; }
    }
}

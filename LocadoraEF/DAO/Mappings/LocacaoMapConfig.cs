using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    class LocacaoMapConfig : EntityTypeConfiguration<Locacao>
    {
        public LocacaoMapConfig()
        {
            this.ToTable("LOCACOES");
        }
    }
}

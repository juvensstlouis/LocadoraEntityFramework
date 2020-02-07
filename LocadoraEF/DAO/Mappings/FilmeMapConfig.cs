using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    class FilmeMapConfig : EntityTypeConfiguration<Filme>
    {
        public FilmeMapConfig()
        {
            this.ToTable("FILMES");
            this.Property(f => f.Nome).HasMaxLength(50).IsUnicode(false).IsRequired();
            this.Property(f => f.DataLancamento).HasColumnType("DATE");
        }
    }
}

using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    class GeneroMapConfig : EntityTypeConfiguration<Genero>
    {
        public GeneroMapConfig()
        {
            this.ToTable("GENEROS");
            this.Property(g => g.Nome).HasMaxLength(20).IsUnicode(false).IsRequired();
            this.HasIndex(g => g.Nome).IsUnique();
        }
    }
}

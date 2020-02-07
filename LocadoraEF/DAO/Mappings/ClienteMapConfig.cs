using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    internal class ClienteMapConfig : EntityTypeConfiguration<Cliente>
    {           
        public ClienteMapConfig()
        {
            this.ToTable("CLIENTES");
            
            this.Property(c => c.Nome).HasMaxLength(30).IsUnicode(false).IsRequired();
            this.Property(c => c.CPF).IsFixedLength().HasMaxLength(14).IsUnicode(false).IsRequired();
            this.Property(c => c.Email).HasMaxLength(50).IsUnicode(false).IsRequired();
            this.Property(c => c.DataNascimento).HasColumnType("DATE");

            this.HasIndex(c => c.CPF).IsUnique();
            this.HasIndex(c => c.Email).IsUnique();
        }
    }
}

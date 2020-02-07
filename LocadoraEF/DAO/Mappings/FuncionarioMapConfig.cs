using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    class FuncionarioMapConfig : EntityTypeConfiguration<Funcionario>
    {
        public FuncionarioMapConfig()
        {
            this.ToTable("FUNCIONARIOS");

            this.Property(f => f.Nome).HasMaxLength(30).IsUnicode(false).IsRequired();
            this.Property(f => f.CPF).IsFixedLength().HasMaxLength(14).IsUnicode(false).IsRequired();
            this.Property(f => f.Email).HasMaxLength(50).IsUnicode(false).IsRequired();
            this.Property(f => f.DataNascimento).HasColumnType("DATE");

            this.Property(f => f.Telefone).HasMaxLength(20).IsUnicode(false).IsRequired();
            this.Property(f => f.Senha).HasMaxLength(50).IsUnicode(false).IsRequired();

            this.HasIndex(f => f.CPF).IsUnique();
            this.HasIndex(f => f.Email).IsUnique();
        }
    }
}

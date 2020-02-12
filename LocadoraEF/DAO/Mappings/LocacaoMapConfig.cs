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
            this.HasMany(l => l.Filmes)
                .WithMany(f => f.Locacoes)
                .Map(lf =>
                 {
                     lf.ToTable("LOCACOESFILMES");
                     lf.MapLeftKey("LocacaoID");
                     lf.MapRightKey("FilmeID");
                 });
        }
    }
}

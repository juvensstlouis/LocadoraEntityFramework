using DAO.Mappings;
using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class LocadoraDBContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }

        public LocadoraDBContext() : base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\900185\source\git_repos\LocadoraEntityFramework\LocadoraDB.mdf;Integrated Security=True;Connect Timeout=30")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new ClienteMapConfig());
            modelBuilder.Configurations.Add(new FuncionarioMapConfig());
            modelBuilder.Configurations.Add(new GeneroMapConfig());
            modelBuilder.Configurations.Add(new FilmeMapConfig());
            modelBuilder.Configurations.Add(new LocacaoMapConfig());
            
            base.OnModelCreating(modelBuilder);
        }

    }
}

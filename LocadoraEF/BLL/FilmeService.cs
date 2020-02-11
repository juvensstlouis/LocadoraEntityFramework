using BLL.Interfaces;
using DAO;
using Entity;
using Entity.Enums;
using Entity.ResultSets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FilmeService : IEntityCRUD<Filme>, IFilmeService
    {
        public Response Insert(Filme item)
        {
            Response response = Validate(item);

            if (response.HasErrors())
            {
                response.Sucesso = false;
                return response;
            }

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    db.Filmes.Add(item);
                    db.SaveChanges();
                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Erros.Add("Erro ao adicionar um filme. Contate o admin!");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        public Response Update(Filme item)
        {
            Response response = Validate(item);

            if (response.HasErrors())
            {
                response.Sucesso = false;
                return response;
            }

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Erros.Add("Erro ao atualizar um filme. Contate o admin!");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        public Response Delete(int id)
        {
            Response response = new Response();

            if (id <= 0)
            {
                response.Erros.Add("ID do filme não foi informado.");
                return response;
            }

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    db.Entry(new Filme() { ID = id }).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Erros.Add("Erro ao deletar um filme. Contate o admin!");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        public DataResponse<Filme> GetData()
        {
            DataResponse<Filme> response = new DataResponse<Filme>();

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    response.Data = db.Filmes.ToList();
                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Erros.Add("Erro no banco de dados. Contate o admin!");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        public DataResponse<FilmeResultSet> GetFilmes()
        {
            throw new NotImplementedException();
        }

        public DataResponse<Filme> GetByID(int id)
        {
            DataResponse<Filme> response = new DataResponse<Filme>();

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    List<Filme> filmes = new List<Filme>();

                    Filme filme = db.Filmes.Find(id);

                    if (filme != null)
                    {
                        filmes.Add(filme);
                    }

                    response.Sucesso = true;
                    response.Data = filmes;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Erros.Add("Erro no banco de dados. Contate o admin!");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        public DataResponse<FilmeResultSet> GetFilmesByName(string nome)
        {
            throw new NotImplementedException();
        }

        public DataResponse<FilmeResultSet> GetFilmesByGenero(int genero)
        {
            throw new NotImplementedException();
        }

        public DataResponse<FilmeResultSet> GetFilmesByClassificacao(Classificacao classificacao)
        {
            throw new NotImplementedException();
        }

        private Response Validate(Filme item)
        {
            Response response = new Response();

            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                response.Erros.Add("O nome do filme deve ser informado.");
            }
            else
            {
                item.Nome = item.Nome.Normatizar();

                if (item.Nome.Length < 2 || item.Nome.Length > 100)
                {
                    response.Erros.Add("O nome do filme deve conter entre 2 e 100 caracteres.");
                }
            }

            if (item.Duracao <= 0)
            {
                response.Erros.Add("A duração do filme não é valido.");
            }

            if (item.DataLancamento == DateTime.MinValue
                ||
                item.DataLancamento > DateTime.Now)
            {
                response.Erros.Add("Data inválida.");
            }

            return response;
        }

    }
}

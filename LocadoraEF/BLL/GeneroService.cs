using BLL.Interfaces;
using DAO;
using Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GeneroService : IEntityCRUD<Genero>
    {
        public Response Insert(Genero item)
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
                    db.Generos.Add(item);
                    db.SaveChanges();
                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Erros.Add("Erro ao adicionar um genero. Contate o admin!");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        public Response Update(Genero item)
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
                    response.Erros.Add("Erro ao atualizar um genero. Contate o adminGenero!");
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
                response.Erros.Add("ID do genero não foi informado.");
                return response;
            }

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    db.Entry(new Genero() { ID = id }).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Erros.Add("Erro ao deletar um genero. Contate o admin!");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        public DataResponse<Genero> GetData()
        {
            DataResponse<Genero> response = new DataResponse<Genero>();

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    response.Data = db.Generos.ToList();
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

        public DataResponse<Genero> GetByID(int id)
        {
            DataResponse<Genero> response = new DataResponse<Genero>();

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    List<Genero> generos = new List<Genero>();

                    Genero genero = db.Generos.Find(id);

                    if (genero != null)
                    {
                        generos.Add(genero);
                    }

                    response.Sucesso = true;
                    response.Data = generos;
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

        private Response Validate(Genero item)
        {
            Response response = new Response();

            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                response.Erros.Add("O nome do gênero deve ser informado.");
            }
            else
            {
                item.Nome = item.Nome.Normatizar();

                if (item.Nome.Length < 2 || item.Nome.Length > 50)
                {
                    response.Erros.Add("O nome do gênero deve conter entre 2 e 50 caracteres");
                }
            }

            return response;
        }
    }
}

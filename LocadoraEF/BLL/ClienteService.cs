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
    public class ClienteService : IEntityCRUD<Cliente>
    {
        public Response Insert(Cliente item)
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
                    db.Clientes.Add(item);
                    db.SaveChanges();
                    response.Sucesso = true;
                    return response;
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    response.Sucesso = false;

                    if (ex.InnerException.ToString().Contains("IX_CPF"))
                    {
                        response.Erros.Add("CPF já cadastrado.");
                    }
                    else if (ex.InnerException.ToString().Contains("IX_Email"))
                    {
                        response.Erros.Add("Email já cadastrado.");
                    }
                    else
                    {
                        response.Erros.Add("Erro ao adicionar um cliente. Contate o admin!");
                        File.WriteAllText("log.txt", ex.Message);
                    }

                    return response;
                }
                catch (Exception ex)
                {
                    response.Erros.Add("Erro ao adicionar um cliente. Contate o admin!");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        public Response Update(Cliente item)
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
                    response.Erros.Add("Erro ao adicionar um cliente. Contate o admin!");
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
                response.Erros.Add("ID do cliente não foi informado.");
                return response;
            }

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    db.Entry(new Cliente() { ID = id}).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Erros.Add("Erro ao adicionar um cliente. Contate o admin!");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        public DataResponse<Cliente> GetData()
        {
            DataResponse<Cliente> response = new DataResponse<Cliente>();

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    response.Data = db.Clientes.ToList();
                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Erros.Add("Erro ao adicionar um cliente. Contate o admin!");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        public DataResponse<Cliente> GetByID(int id)
        {
            DataResponse<Cliente> response = new DataResponse<Cliente>();

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    List<Cliente> clientes = new List<Cliente>();

                    Cliente cliente = db.Clientes.Find(id);
                    if (cliente != null)
                    {
                        clientes.Add(cliente); 
                    }

                    response.Sucesso = true;
                    response.Data = clientes;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Erros.Add("Erro ao adicionar um cliente. Contate o admin!");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        private Response Validate(Cliente item)
        {
            Response response = new Response();

            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                response.Erros.Add("O nome do cliente deve ser informado.");
            }
            else
            {
                item.Nome = item.Nome.Normatizar();

                if (item.Nome.Length < 2 || item.Nome.Length > 50)
                {
                    response.Erros.Add("O nome do cliente deve conter entre 2 e 50 caracteres.");
                }
            }

            if (string.IsNullOrWhiteSpace(item.CPF))
            {
                response.Erros.Add("O CPF do cliente deve ser informado.");
            }
            else
            {
                if (!item.CPF.IsCpf())
                {
                    response.Erros.Add("O CPF do cliente é invalido.");
                }

            }

            if (string.IsNullOrWhiteSpace(item.Email))
            {
                response.Erros.Add("O email do cliente deve ser informado.");
            }
            else
            {
                if (!item.Email.IsEmail())
                {
                    response.Erros.Add("O email do cliente é invalido.");
                }
            }

            return response;
        }
    }
}

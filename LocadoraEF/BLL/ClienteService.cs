using BLL.Interfaces;
using DAO;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ClienteService : IEntityCRUD<Cliente>
    {
        public Response Insert(Cliente jdfksfdak)
        {
            Response response = new Response();

            /*
            Response response = Validate(item);
            
            if (response.HasErrors())
            {
                response.Sucesso = false;
                return response;
            }
            */

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                Cliente item = new Cliente()
                {
                    Nome = "Bla",
                    CPF = "707.212.344-12",
                    Email = "bla@bla.bla",
                    DataNascimento = DateTime.Now,
                    EhAtivo = true
                };

                db.Clientes.Add(item);
                db.SaveChanges();
            }

            return response;
        }

        public Response Update(Cliente item)
        {
            throw new NotImplementedException();
        }

        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DataResponse<Cliente> GetData()
        {
            throw new NotImplementedException();
        }

        public DataResponse<Cliente> GetByID(int id)
        {
            throw new NotImplementedException();
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

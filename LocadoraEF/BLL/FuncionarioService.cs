using BLL.Interfaces;
using BLL.Security;
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
    public class FuncionarioService : IEntityCRUD<Funcionario>, IFuncionarioService
    {
        public Response Insert(Funcionario item)
        {
            Response response = Validate(item);

            if (response.HasErrors())
            {
                response.Sucesso = false;
                return response;
            }

            item.EhAtivo = true;
            item.Senha = HashUtils.HashPassword(item.Senha);

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    db.Funcionarios.Add(item);
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
                        response.Erros.Add("Erro ao adicionar um funcionário. Contate o admin!");
                        File.WriteAllText("log.txt", ex.Message);
                    }

                    return response;
                }
                catch (Exception ex)
                {
                    response.Erros.Add("Erro ao adicionar um funcionário. Contate o admin!");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        public Response Update(Funcionario item)
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
                    response.Erros.Add("Erro ao atualizar um funcionário. Contate o admin!");
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
                response.Erros.Add("ID do funcionário não foi informado.");
                return response;
            }

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    db.Entry(new Funcionario() { ID = id }).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    response.Sucesso = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Erros.Add("Erro ao deletar um funcionário. Contate o admin!");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        public DataResponse<Funcionario> GetData()
        {
            DataResponse<Funcionario> response = new DataResponse<Funcionario>();

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    response.Data = db.Funcionarios.ToList();
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

        public DataResponse<Funcionario> GetByID(int id)
        {
            DataResponse<Funcionario> response = new DataResponse<Funcionario>();

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    List<Funcionario> funcionarios = new List<Funcionario>();
                    
                    Funcionario funcionario = db.Funcionarios.Find(id);
                    
                    if (funcionario != null)
                    {
                        funcionarios.Add(funcionario);
                    }

                    response.Sucesso = true;
                    response.Data = funcionarios;
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

        public DataResponse<Funcionario> Autenticar(string email, string senha)
        {
            DataResponse<Funcionario> response = new DataResponse<Funcionario>();

            if (!email.IsEmail() || !senha.IsValidPassword())
            {
                response.Sucesso = false;
                response.Erros.Add("Email e/ou Senha incorreto(s).");
                return response;
            }

            string senhaHasheada = HashUtils.HashPassword(senha);

            using (LocadoraDBContext db = new LocadoraDBContext())
            {
                try
                {
                    response.Data = db.Funcionarios.Where(f => f.Email == email &&
                                                          f.Senha == senhaHasheada)
                                                   .ToList();
                    response.Sucesso = true;
                    User.FuncionarioLogado = response.Data[0];
                    return response;
                }
                catch (Exception ex)
                {
                    response.Erros.Add("Erro ao adicionar um funcionário. Contate o admin!");
                    File.WriteAllText("log.txt", ex.Message);
                    return response;
                }
            }
        }

        private Response Validate(Funcionario item)
        {
            Response response = new Response();

            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                response.Erros.Add("O nome do funcionário deve ser informado.");
            }
            else
            {
                item.Nome = item.Nome.Normatizar();

                if (item.Nome.Length < 2 || item.Nome.Length > 50)
                {
                    response.Erros.Add("O nome do funcionário deve conter entre 2 e 50 caracteres.");
                }
            }

            if (string.IsNullOrWhiteSpace(item.CPF))
            {
                response.Erros.Add("O CPF do funcionário deve ser informado.");
            }
            else
            {
                if (!item.CPF.IsCpf())
                {
                    response.Erros.Add("O CPF do funcionário é invalido.");
                }
            }

            if (string.IsNullOrWhiteSpace(item.Telefone))
            {
                response.Erros.Add("O telefone do funcionário deve ser informado.");
            }
            else
            {
                item.Telefone = item.Telefone.Trim();

                if (item.Telefone.IsTelefone())
                {
                    response.Erros.Add("O telefone do funcionário é invalido.");
                }
            }

            if (string.IsNullOrWhiteSpace(item.Email))
            {
                response.Erros.Add("O email do funcionário deve ser informado.");
            }
            else
            {
                if (!item.Email.IsEmail())
                {
                    response.Erros.Add("O email do funcionário é invalido.");
                }
            }

            if (string.IsNullOrWhiteSpace(item.Senha))
            {
                response.Erros.Add("Uma senha deve ser informada.");
            }
            else
            {
                if (!item.Senha.IsValidPassword())
                {
                    response.Erros.Add("A senha deve conter de 8 a 15 caracteres e pelo menos 1 caixa alta e 1 simbolo");
                }
            }

            return response;
        }
    }
}

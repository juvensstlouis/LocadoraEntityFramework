using Entity;
using Entity.Enums;
using Entity.ResultSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IFilmeService
    {
        DataResponse<FilmeResultSet> GetFilmes();
        DataResponse<FilmeResultSet> GetFilmesByName(string nome);
        DataResponse<FilmeResultSet> GetFilmesByGenero(int genero);
        DataResponse<FilmeResultSet> GetFilmesByClassificacao(Classificacao classificacao);
    }
}

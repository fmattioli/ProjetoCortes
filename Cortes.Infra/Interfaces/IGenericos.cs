using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Infra.Interfaces
{
    public interface IGenericos
    {
        Task<string> MontarInsert<T>(T objeto, IList<string> desconsiderarColuna, bool plural);
        Task<string> MontarSelectObjeto<T>(T objeto);
        Task<string> MontarSelectWithJoin(string tableName, IList<(string tabela, string tabelaJoin, string colunaJoin)> join, IList<(bool isInt, string nome)> fields, IList<(bool isInt, string nome, string valor)> temWhere, bool plural = true);
        Task<string> MontarSelectGetId(string tableName, IList<(bool isInt, string nome, string valor)> temWhere);
        Task<string> Atualizar(string tableName, IList<(bool isInt, string campo, string valor)> campos, IList<(bool isInt, string nome, string valor)> temWhere);
    }
}

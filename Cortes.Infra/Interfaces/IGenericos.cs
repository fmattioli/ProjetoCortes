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
        Task<DataTable> Select(string query);
        Task RunSQLCommand(string query);
        Task<string> MontarInsert<T>(T objeto);
    }
}

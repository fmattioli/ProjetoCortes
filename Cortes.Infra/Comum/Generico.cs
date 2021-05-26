using Cortes.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Infra.Comum
{
    public class Generico : IGenericos
    {
        public string conn { get; set; }
        public Generico(IConfiguration config)
        {
            conn = config.GetConnectionString("CortesBD");
        }

        public Task RunSQLCommand(string query)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> Select(string query)
        {
            throw new NotImplementedException();
        }
    }
}

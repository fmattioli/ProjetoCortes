using Cortes.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cortes.Infra.Comum
{
    public class Generico : IGenericos
    {
        private string conn { get; set; }
        private StringBuilder SQL = new StringBuilder();
        public Generico(IConfiguration config)
        {
            conn = config.GetConnectionString("CortesBD");
        }

        public async Task RunSQLCommand(string query)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> Select(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<string> MontarInsert<T>(T objeto)
        {
            string sqlInsert = "";
            await Task.Run(() =>
            {
                SQL.Clear();
                var prop = objeto.GetType().GetProperties();
                prop = prop.Where(a => a.Name != "Id").ToArray();
                string lastItem = prop[prop.Length - 1].Name;
                SQL.AppendLine($"INSERT INTO {objeto.GetType().Name}");
                SQL.AppendLine($"(");
                foreach (var item in prop)
                {
                    if (item.Name != lastItem)
                        SQL.AppendLine($"{item.Name},");
                    else
                        SQL.AppendLine($"{item.Name}");
                }
                SQL.AppendLine($")");
                SQL.AppendLine($"VALUES");
                SQL.AppendLine($"(");
                foreach (var item in prop)
                {
                    if (item.Name != lastItem)
                    {
                        if (item.PropertyType.FullName.Contains("String"))
                            SQL.AppendLine($"'{item.GetValue(objeto)}',");
                        else
                            SQL.AppendLine($"{item.GetValue(objeto)},");
                    }
                    else
                    {
                        if (item.PropertyType.FullName.Contains("String"))
                            SQL.AppendLine($"'{item.GetValue(objeto)}'");
                        else
                            SQL.AppendLine($"{item.GetValue(objeto)}");
                    }
                }
                SQL.AppendLine($")");
                sqlInsert = SQL.ToString();
            });
            return sqlInsert;
        }
    }
}


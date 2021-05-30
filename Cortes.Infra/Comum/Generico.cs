using Cortes.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            try
            {
                // create connection and command
                using (SqlConnection cn = new SqlConnection(conn))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // open connection, execute INSERT, close connection
                    cn.Open();
                    await cmd.ExecuteNonQueryAsync();
                    cn.Close();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DataTable> Select(string query)
        {
            DataTable dt = new DataTable();
            await Task.Run(() =>
            {
                using (SqlConnection con = new SqlConnection(conn))
                {

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

            });
            return dt;
        }

        public async Task<string> MontarInsert<T>(T objeto, bool plural = true)
        {
            string sqlInsert = "";
            await Task.Run(() =>
            {
                SQL.Clear();
                var prop = objeto.GetType().GetProperties();
                prop = prop.Where(a => a.Name != "Id").ToArray();
                string lastItem = prop[prop.Length - 1].Name;

                if(plural)
                    SQL.AppendLine($"INSERT INTO {objeto.GetType().Name}s");
                else
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

        public async Task<string> MontarSelect<T>(T objeto, IList<string> temWhere, bool plural = true)
        {
            string sqlInsert = "";
            await Task.Run(() =>
            {
                SQL.Clear();
                var prop = objeto.GetType().GetProperties();
                string lastItem = prop[prop.Length - 1].Name;
                SQL.AppendLine($"SELECT ");
                foreach (var item in prop)
                {
                    if (item.Name != lastItem)
                        SQL.AppendLine($"{item.Name},");
                    else
                        SQL.AppendLine($"{item.Name}");
                }
                if(plural)
                    SQL.AppendLine($"FROM {objeto.GetType().Name}s");
                else
                    SQL.AppendLine($"FROM {objeto.GetType().Name}");
                if (temWhere != null)
                {
                    SQL.AppendLine($"WHERE 1 = 1");
                    foreach (var item in temWhere)
                    {
                        if (objeto.GetType().GetProperties().Where(a => a.Name == item).ToArray()[0].PropertyType.FullName.Contains("String"))
                        {
                            string valor = objeto.GetType().GetProperties().Where(a => a.Name == item).ToArray()[0].GetValue(objeto).ToString();
                            SQL.AppendLine($"AND {item} = '{valor}'");
                        }
                        else
                        {
                            string valor = objeto.GetType().GetProperties().Where(a => a.Name == item).ToArray()[0].GetValue(objeto).ToString();
                            SQL.AppendLine($"AND {item} = {valor}");
                        }
                    }
                }

                sqlInsert = SQL.ToString();
            });
            return sqlInsert;
        }
    }
}


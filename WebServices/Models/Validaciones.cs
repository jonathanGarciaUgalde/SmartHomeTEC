using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;


namespace WebServices.Models
{
    public class Validaciones
    {

        NpgsqlConnection connection = new NpgsqlConnection();
        ServerConexion server = new ServerConexion();

        public Validaciones()
        {



        }

        public bool exists(NpgsqlCommand conector)
        {
            try
            {
                NpgsqlDataReader dr = conector.ExecuteReader();
                dr.Read();
                //  System.Diagnostics.Debug.Print("User: " + (string)dr[0] + " ya existe");
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool validaEstado(NpgsqlCommand command)
        {
            try
            {
                NpgsqlDataReader dr = command.ExecuteReader();
                dr.Read();
                if (!(bool)dr["estadoActivo"])
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }

        }

        public bool verifyuser(NpgsqlCommand command, string element)
        {
            try
            {
                NpgsqlDataReader dr = command.ExecuteReader();
                dr.Read();
                if (dr["correo"].ToString().ToLowerInvariant().Equals(element.ToLowerInvariant()))
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Valida si existe una tupla de dos keys en la tabla de la entrada
        /// </summary>
        /// <param name="table"></param>
        /// <param name="tableKey1"></param>
        /// <param name="tableKey2"></param>
        /// <param name="userKey1"></param>
        /// <param name="userKey2"></param>
        /// <returns>bool</returns>
        public bool validation(string table, string tableKey1, string tableKey2, string userKey1, string userKey2)
        {
            connection.ConnectionString = server.init();

            connection.Open();
            string query = "select " + tableKey1 + " from " + table + " where " + tableKey1 + " = '" + userKey1 + "' and " + tableKey2 + "= '" + userKey2 + "';";
            Debug.Print(query);
            NpgsqlCommand conector = new NpgsqlCommand(query, connection);
            try
            {
                NpgsqlDataReader dr = conector.ExecuteReader();
                dr.Read();
                Debug.Print("User: " + (string)dr[0] + " ya existe");
                return false;
            }
            catch
            {
                return true;
            }
        }

    }
}



using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebServices.Models
{

    public class LoginModel
    {
        /// <summary>
        /// Verifica los datos del login
        /// </summary>
        /// <param name="conector"></param>
        /// <param name="password"></param>
        /// <returns>bool</returns>
        public bool verifyLogin(NpgsqlCommand conector, string correo, string password)
        {
            try
            {
                NpgsqlDataReader dr = conector.ExecuteReader();
                dr.Read();
                if (dr[0].ToString() == correo && dr[1].ToString() == password)
                {
                    return true;
                }
                Debug.Print("hola");
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Valida si el usuario existe
        /// </summary>
        /// <param name="conector"></param>
        /// <returns>bool</returns>
        public bool exist(NpgsqlCommand conector)
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

    }
}
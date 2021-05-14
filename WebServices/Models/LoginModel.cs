﻿using Newtonsoft.Json.Linq;
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
            /// <param name="command"></param>
            /// <param name="password"></param>
            /// <returns>bool</returns>
            public bool verifyLogin(NpgsqlCommand command, string correo,string password)
            {
                try
                {
                    NpgsqlDataReader dr = command.ExecuteReader();
                    dr.Read();
                    if (dr["correo"].ToString()==correo && dr["password"].ToString()==password)
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


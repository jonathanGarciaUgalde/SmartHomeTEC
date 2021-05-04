using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using WebServices.Models;
using Npgsql;



using WebServices.Models;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        NpgsqlConnection connection = new NpgsqlConnection();
        ServerConexion server = new ServerConexion();
        LoginModel lg = new LoginModel();


        /*
        * Método que se comunica mediante el protocolo http para validar si el usuario que inicia sesión está registrado.
        */
        [HttpPost]
        [Route("verify/Login/{username}/{password}")]
        public async Task<IActionResult> Login(string username, string password)
        {

            connection.ConnectionString = server.init();
            connection.Open();

            string query = "select correo,password from usuario where correo = '" + username + "'";
            NpgsqlCommand conector = new NpgsqlCommand(query, connection);

            if (lg.verifyLogin(conector, username, password))
            {
                connection.Close();
                return Ok(true);
            }
           
           
            return BadRequest("Username or password is incorrect");
        }
        /*
        * Método que se comunica mediante el protocolo http y retorna todos los usuarios registrados.
        */


        /*
        * Método que se comunica mediante el protocolo http  y  este retorna el usuario del id que se consulta 
        * 
        */

        /*
        * Método que se comunica mediante el protocolo http para insertar nuevos clientes en el app.
        */
        [HttpPost("insert/user")]
            public async Task<IActionResult> AddClienteAsync(User newUser)
            {
                connection.ConnectionString = server.init();
                // string query = "select Correo from usuario where correo = '" +newUser.Correo +"'";
                //NpgsqlCommand conector = new NpgsqlCommand(query, connection);
                //if (exist(conector))
                //{
                //  connection.Close();
                //return BadRequest("User already exist");
                //}
                //else
                

                    string query = "insert into usuario VALUES(";
                    query += "'" + newUser.Correo + "'," + "'" + newUser.Password + "'," + "'" + newUser.Nombre + "'," + "'" + newUser.Apellidos + "')";



                    connection.Open();

                    NpgsqlCommand execute = new NpgsqlCommand(query, connection);
                    execute.ExecuteNonQuery();
                    query = " insert into region_x_usuario VALUES(";
                    query += "'" + newUser.Region.Pais + "','" + newUser.Correo + "','" + newUser.Region.Continente + "')";
                    NpgsqlCommand execute1 = new NpgsqlCommand(query, connection);
                    execute1.ExecuteNonQuery();





                    int i = 0;
                    while (newUser.Direccion.Count > i)
                    {
                        query = "insert into direccion VALUES(";
                        query += "'" + newUser.Correo + "'," + "'" + newUser.Direccion.ElementAt(i).Provincia + "','" + newUser.Direccion.ElementAt(i).Canton + "','" + newUser.Direccion.ElementAt(i).Distrito + "')";

                        NpgsqlCommand execute3 = new NpgsqlCommand(query, connection);
                        execute3.ExecuteNonQuery();
                        i++;



                    }
                    connection.Close();
                    return Ok("Success");


                }






            } } 
    
    



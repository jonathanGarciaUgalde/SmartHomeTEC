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
    [Route("api/[controller]/[action]")]
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
        [Route("{correo}/{password}")]
        public async Task<IActionResult> Login(string correo, string password)
        {

            connection.ConnectionString = server.init();
            connection.Open();

            string query = $"SELECT \"correo\",\"password\" FROM \"Usuario\" WHERE \"correo\" = '{correo}';";
            NpgsqlCommand conector = new NpgsqlCommand(query, connection);

            if (lg.verifyLogin(conector, correo, password))
            {
                connection.Close();
                return Ok(true);
            }


            return BadRequest("Username or password is incorrect");
        }
       
        [HttpPost]
        public async Task<IActionResult> Signin([FromBody] User newUser)
        {
            connection.ConnectionString = server.init();

      

            string query = $"insert into \"Usuario\" VALUES('{newUser.Correo}','{newUser.Password}', '{newUser.Nombre}', '{newUser.Apellidos}', '{newUser.Region.Continente}', '{newUser.Region.Pais}')";

            connection.Open();

            NpgsqlCommand execute = new NpgsqlCommand(query, connection);
            execute.ExecuteNonQuery();

          
            int i = 0;
            while (newUser.Direccion.Count > i)
            {
                query = $"insert into \"DireccionEntrega\" (\"correo\",\"ubicacion\" ) VALUES('{newUser.Correo}','{ newUser.Direccion.ElementAt(i).Ubicacion}');";

                NpgsqlCommand execute3 = new NpgsqlCommand(query, connection);
                execute3.ExecuteNonQuery();
                i++;

            }
            connection.Close();
            return Ok();
        }


        [HttpPost] //Route-> api/User/Credenciales
        public async Task<IActionResult> Credenciales([FromBody] User user)
        {
            connection.ConnectionString = server.init();
            string query = $"SELECT " +
                $"              \"nombre\", \"apellidos\", \"pais\", \"continente\" " +
                $"         FROM " +
                $"              \"Usuario\" " +
                $"         WHERE \"correo\" = '{user.Correo}';";

            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            NpgsqlDataReader dr = command.ExecuteReader();
            dr.Read();

            region outputRegion = new region() { Pais = (string)dr["pais"], Continente = (string)dr["continente"] };
            User outputUser = new User() { Nombre = (string)dr["Nombre"], Apellidos = (string)dr["apellidos"], Region = outputRegion };

            connection.Close();


            query = $"SELECT " +
                    $"      \"ubicacion\" " +
                    $"FROM " +
                    $"      \"DireccionEntrega\" " +
                    $"WHERE \"correo\" = '{user.Correo}';";

            connection.Open();
            command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            dr = command.ExecuteReader();

            List<Direccion> direcciones = new List<Direccion>();
            while (dr.Read())
            {
                Direccion direccion = new Direccion() { Ubicacion = (string)dr["Ubicacion"] };
                direcciones.Add(direccion);
            }
            outputUser.Direccion = direcciones;

            connection.Close();
            return Ok(outputUser);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateUsuario([FromBody] User newUse)
        {
            connection.ConnectionString = server.init();
            try

            {
                        
               
                string query = $"UPDATE \"Usuario\" SET \"nombre\"='{ newUse.Nombre}', \"apellidos\"='{ newUse.Apellidos}'," +
                    $"\"continente\"='{newUse.Region.Continente}' , \"pais\"='{newUse.Region.Pais}'" +
                   $"         WHERE   \"correo\" = '{newUse.Correo}' ;";

                NpgsqlCommand conector = new NpgsqlCommand(query, connection);
                connection.Open();
                conector.ExecuteNonQuery();
                connection.Close();

                int i = 0;
                while (newUse.Direccion.Count >i)
                {
                    query = $"UPDATE \"DireccionEntrega\" SET \"ubicacion\"='{ newUse.Direccion.ElementAt(i).Ubicacion}' " +
                          $"         WHERE   \"correo\" ='{newUse.Correo}';";

                    NpgsqlCommand execute3 = new NpgsqlCommand(query, connection);
                    connection.Open();
                    execute3.ExecuteNonQuery();
                    connection.Close();
                    i++;

                }
               
                return Ok();

            }
            catch
            {
                return BadRequest("No se pudo actualizar el dispositivo");
            }
        }






    }
}

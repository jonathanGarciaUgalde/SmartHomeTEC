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

            string query = $"SELECT \"Correo\",\"Password\" FROM \"usuario\" WHERE \"Correo\" = '{correo}';";
            NpgsqlCommand conector = new NpgsqlCommand(query, connection);

            if (lg.verifyLogin(conector, correo, password))
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
        [HttpPost]
        public async Task<IActionResult> Signin([FromBody] User  newUser)
        {
            connection.ConnectionString = server.init();

            /*
            string query = "select Correo from usuario where correo = '" +newUser.Correo +"'";
            NpgsqlCommand conector = new NpgsqlCommand(query, connection);
            if (exist(conector))
            {
              connection.Close();
            return BadRequest("User already exist");
            }
            else
            */

            string query = $"insert into usuario VALUES('{newUser.Correo}','{newUser.Password}', '{newUser.Nombre}', '{newUser.Apellidos}')";
            
            connection.Open();

            NpgsqlCommand execute = new NpgsqlCommand(query, connection);
            execute.ExecuteNonQuery();

            query = $"insert into region_x_usuario VALUES('{newUser.Region.Pais}','{newUser.Correo}','{newUser.Region.Continente}')";                    
            NpgsqlCommand execute1 = new NpgsqlCommand(query, connection);
            execute1.ExecuteNonQuery();

            int i = 0;
            while (newUser.Direccion.Count > i)
            {
                query = $"insert into direccion VALUES('{newUser.Correo}','{ newUser.Direccion.ElementAt(i).Provincia}','{newUser.Direccion.ElementAt(i).Canton}','{newUser.Direccion.ElementAt(i).Distrito}')";
                
                NpgsqlCommand execute3 = new NpgsqlCommand(query, connection);
                execute3.ExecuteNonQuery();
                i++;

            }
            connection.Close();
            return Ok("Success");
        }


        [HttpGet] //Route-> api/User/Credenciales
        public async Task<IActionResult> Credenciales([FromBody] User user)
        {
            connection.ConnectionString = server.init();
            string query = $"SELECT " +
                $"              \"Nombre\", \"Apellidos\", \"Pais\", \"Continente\" " +
                $"         FROM " +
                $"              \"usuario\" " +
                $"         INNER JOIN \"region_x_usuario\" " +
                $"            ON \"Correo\" = \"Usuario_asociado\"" +
                $"         WHERE \"Correo\" = '{user.Correo}';";

            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            NpgsqlDataReader dr = command.ExecuteReader();
            dr.Read();

            Region outputRegion = new Region() { Pais = (string)dr["Pais"], Continente = (string)dr["Continente"] };
            User outputUser = new User() { Nombre = (string)dr["Nombre"], Apellidos = (string)dr["Apellidos"], Region = outputRegion };
            
            connection.Close();

            
            query = $"SELECT " +
                    $"      \"Provincia\", \"Canton\", \"Distrito\" " +
                    $"FROM " +
                    $"      \"usuario\" " +
                    $"INNER JOIN \"direccion\" " +
                    $"  ON \"Correo\" = \"Id_usuario\"" +
                    $"WHERE \"Correo\" = '{user.Correo}';";

            connection.Open();
            command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            dr = command.ExecuteReader();

            List<Direccion> direcciones = new List<Direccion>();
            while (dr.Read())
            {                
                Direccion direccion = new Direccion() { Provincia = (string)dr["Provincia"], Canton = (string)dr["Canton"], Distrito = (string)dr["Distrito"] };
                direcciones.Add(direccion);

            }
            outputUser.Direccion = direcciones;
            
            connection.Close();
            return Ok(outputUser);
        }



    }
} 
    
    



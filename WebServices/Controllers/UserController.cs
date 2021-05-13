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
        
        //Método que se comunica mediante el protocolo http para validar si el usuario que inicia sesión está registrado.        
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

        

        /*
        * Método que se comunica mediante el protocolo http para registrar nuevos clientes en el app.
        */
        [HttpPost]
        public async Task<IActionResult> Signin([FromBody] User  newUser)
        {            
            connection.ConnectionString = server.init();
            connection.Open();

            string query = $"insert into \"Usuario\" VALUES('{newUser.Correo}','{newUser.Password}', '{newUser.Nombre}', '{newUser.Apellidos}', '{newUser.Region.Continente}', '{newUser.Region.Pais}')";
            NpgsqlCommand execute = new NpgsqlCommand(query, connection);
            execute.ExecuteNonQuery();

            int i = 0;
            while (newUser.Direccion.Count > i)
            {
                query = $"insert into \"direccionEntrega\" VALUES('{newUser.Correo}','{ newUser.Direccion.ElementAt(i).Ubicacion}');";
                
                NpgsqlCommand execute3 = new NpgsqlCommand(query, connection);
                execute3.ExecuteNonQuery();
                i++;

            }
            connection.Close();
            return Ok("Success");
        }

        [HttpPost] //api/User/Aposento
        public async Task<IActionResult> Aposento([FromBody] Aposento aposento)
        {

            connection.ConnectionString = server.init();

            try
            {
                connection.Open();
                string userQuery = $"SELECT \"correo\" FROM \"Usuario\" WHERE \"correo\" = '{aposento.Correo}';";
                NpgsqlCommand userCommand = new NpgsqlCommand(userQuery, connection);
                userCommand.ExecuteNonQuery();

                NpgsqlDataReader dr = userCommand.ExecuteReader();                
                dr.Read(); //Si no existen filas por ller este metodo fallaria. Es debido a esto que se usa el try/catch.
                User currentUser = new User() { Correo = (string)dr["correo"] };
                connection.Close();
            }
            catch
            {
                connection.Close();
                return BadRequest("User not found");
            }

            try
            {
                connection.Open();
                string query = $"INSERT INTO \"Aposento\" VALUES('{aposento.Correo}','{aposento.Nombre}');";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();

                return Ok();
            }
            catch
            {
                connection.Close();
                return BadRequest("Aposento ya definido");
            }

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

            try
            {
                NpgsqlDataReader dr = command.ExecuteReader();
                dr.Read();

                Region outputRegion = new Region() { Pais = (string)dr["pais"], Continente = (string)dr["continente"] };
                User outputUser = new User() { Nombre = (string)dr["nombre"], Apellidos = (string)dr["apellidos"], Region = outputRegion };

                connection.Close();


                query = $"SELECT " +
                        $"      \"ubicacion\" " +
                        $"FROM " +
                        $"      \"direccionEntrega\" " +
                        $"WHERE \"correo\" = '{user.Correo}';";

                connection.Open();
                command = new NpgsqlCommand(query, connection);
                command.ExecuteNonQuery();
                dr = command.ExecuteReader();

                List<Direccion> direcciones = new List<Direccion>();
                while (dr.Read())
                {
                    Direccion direccion = new Direccion() { Ubicacion = (string)dr["ubicacion"] };
                    direcciones.Add(direccion);
                }
                outputUser.Direccion = direcciones;

                connection.Close();
                return Ok(outputUser);
            }
            
            catch
            {
                return BadRequest("User not found");
            }
            

        }



    }
} 
    
    



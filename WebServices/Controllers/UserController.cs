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
     

        /*
        * Método que se comunica mediante el protocolo http para validar si el usuario que inicia sesión está registrado.
        */
        [HttpGet("id/{id}")]
        public async Task<IActionResult> ValidUser(int id)
        {
            return Ok(id);
        }
        /*
        * Método que se comunica mediante el protocolo http y retorna todos los usuarios registrados.
        */
        [HttpGet("All/Users")]
        public async Task<IActionResult> GetallAsync()
        {
            return Ok();
        }
        /*
        * Método que se comunica mediante el protocolo http  y  este retorna el usuario del id que se consulta 
        * 
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteAsync(int id)
        {

            return Ok();
        }
        /*
        * Método que se comunica mediante el protocolo http para insertar nuevos clientes en el app.
        */
        [HttpPost("insert/user")]
        public async Task<IActionResult> AddClienteAsync(User newUser)
        {
            connection.ConnectionString ="Server=localhost; Port = 5432;User Id=postgres;Password = 12345; Database = SmartHomeTec";
           
            
            String query = "insert into usuario VALUES(";
            query+="'"+newUser.Correo+"'," + "'"+newUser.Password+"',"+"'"+ newUser.Nombre+"',"+"'" + newUser.Apellido1+"',"+"'"+ newUser.Apellido2+"',"+"'"+newUser.Direccion+"',"+"'"+newUser.Pais+"',"+"'"+newUser.Continente+"')";
            connection.Open();

            NpgsqlCommand execute = new NpgsqlCommand(query, connection);
            execute.ExecuteNonQuery();
            connection.Close();

            return Ok("Success");






        }
    }
}


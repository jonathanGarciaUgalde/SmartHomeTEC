using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebServices.Models;

namespace WebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        NpgsqlConnection connection = new NpgsqlConnection();
        ServerConexion server = new ServerConexion();
        LoginModel lg = new LoginModel();

        [HttpPost] // api/Admin/Signin
        public async Task<IActionResult> Signin([FromBody] User newAdmin)
        {

            connection.ConnectionString = server.init();
            connection.Open();

            string query = $"INSERT INTO \"Admin\" VALUES('{newAdmin.Correo}','{newAdmin.Password}');";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();

            connection.Close();
            return Ok();
        }

        [HttpGet] // api/Admin/Login
        public async Task<IActionResult> Login([FromBody] User admin) 
        {
            connection.ConnectionString = server.init();
            connection.Open();
            
            string query = $"SELECT \"correo\",\"password\" FROM \"Admin\" WHERE \"correo\" = '{admin.Correo}';";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();


            if (lg.verifyLogin(command, admin.Correo, admin.Password))
            {
                connection.Close();
                return Ok();
            }

            connection.Close();
            return BadRequest("Username or password is incorrect");
            
        }

    }
}

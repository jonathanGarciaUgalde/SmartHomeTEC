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

            string query = $"INSERT INTO \"Admin\" VALUES('{newAdmin.correo}','{newAdmin.password}');";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();

            connection.Close();
            return Ok();
        }

        [HttpPost] // api/Admin/Login
        public async Task<IActionResult> Login([FromBody] User admin) 
        {
            connection.ConnectionString = server.init();
            connection.Open();
            
            string query = $"SELECT \"correo\",\"password\" FROM \"Admin\" WHERE \"correo\" = '{admin.correo}';";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();


            if (lg.verifyLogin(command, admin.correo, admin.password))
            {
                connection.Close();
                return Ok();
            }

            connection.Close();
            return BadRequest("Mail or password incorrect");
            
        }

    }
}

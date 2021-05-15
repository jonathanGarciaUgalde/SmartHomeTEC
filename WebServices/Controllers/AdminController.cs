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
            return BadRequest("Mail or password incorrect");
            
        }

        [HttpGet] // api/Admin/DispositivosActivos
        public async Task<IActionResult> DispositivosActivos()
        {
            connection.ConnectionString = server.init();
            connection.Open();

            string query = $"SELECT COUNT(*) AS \"DispActivos\" FROM \"Dispositivo\" WHERE \"estadoActivo\" = TRUE;";

            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();

            NpgsqlDataReader dr = command.ExecuteReader();

            dr.Read();

            Reporte reporte = new Reporte() { DispActivos = (int)(long)dr["DispActivos"] };

            return Ok(reporte);

        }

        [HttpGet] // api/Admin/PromDispositivosPorUsuario
        public async Task<IActionResult> PromDispositivosPorUsuario()
        {
            connection.ConnectionString = server.init();
            connection.Open();

            string query = $"SELECT CAST(COUNT(*) AS FLOAT)/(SELECT COUNT(*) FROM \"Usuario\") AS \"PromedioDispositivosPorUsuario\" FROM \"Dispositivo\";";

            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();

            NpgsqlDataReader dr = command.ExecuteReader();

            dr.Read();

            Reporte reporte = new Reporte() { PromDispPorUsuario = (double)dr["PromedioDispositivosPorUsuario"] };

            return Ok(reporte);

        }





    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServices.Models;
using Npgsql;

namespace WebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GestionRegionController : ControllerBase
    {
        NpgsqlConnection connection = new NpgsqlConnection();// instacias  mecanismo de comunicacion con postgre
        ServerConexion server = new ServerConexion();// conexion de servidor 
        Validaciones validaciones = new Validaciones();

        // Este Metodo hace  que se  se le muestre al usuario los dispositivos asociados a la region
        // Este devuelve mediante el protocolo http   las restriciones  que deben tomarse en cuenta en la base de datos para obtenerlos

        [HttpPost]
        public async Task<IActionResult> GetDispositivoStock(User usuario)
        { connection.ConnectionString = server.init();
            List<Distribuidor> ListDistribuidores = new List<Distribuidor>();
            try
            {
                string query = $"SELECT \"cedulaJuridica\", \"nombre\", \"continente\", \"pais\" " +
                    $"         FROM       \"Distribuidor\"WHERE  \"pais\"= '{usuario.Region.Pais}'  ;";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                NpgsqlDataReader dr = command.ExecuteReader();
               
                while (dr.Read())
                {
                    region Region1 = new region()
                    {
                        Continente = (string)dr["continente"],
                        Pais = (string)dr["pais"],
                    };
                    Distribuidor distribuidor = new Distribuidor()
                    {
                        CedulaJuridica = (int)dr["cedulaJuridica"],
                        Nombre = (string)dr["nombre"],
                        Region = Region1
                    };

                    ListDistribuidores.Add(distribuidor);
                }
                connection.Close();
                return Ok(ListDistribuidores);
            }
            catch {

                return BadRequest("Ese usuario no tiene una  region permitida"); }

        }
      


    }
}

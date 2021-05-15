using System;
using System.Collections.Generic;

using System.Text;
using System.Linq;
using Npgsql;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebServices.Models;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServices.Controllers
{





    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DispositivoController : ControllerBase
    {

        NpgsqlConnection connection = new NpgsqlConnection();// instacias  mecanismo de comunicacion con postgre
        ServerConexion server = new ServerConexion();// conexion de servidor 
        Validaciones validaciones = new Validaciones();


        [HttpPost]
        public async Task<IActionResult> setDispositivo(Dispositivo dis)
        {
            connection.ConnectionString = server.init();
            string query = $"SELECT \"correo\" FROM \"Dispositivo\" WHERE \"numeroSerie\" = '{dis.NumeroSerie} ;"; 
            NpgsqlCommand conector = new NpgsqlCommand(query, connection);
            connection.Open();
            
            if (validaciones.exists(conector))
            {
                connection.Close();
                return BadRequest("element already exist");
            }
            else
            {
                query = $"INSERT INTO \"Dispositivo\" VALUES({dis.NumeroSerie},{dis.Consumo},'{dis.Marca}',{dis.EstadoActivo}," +
                    $"'{dis.NombreAposento}','{dis.CorreoPosedor}','{dis.Tipo}','{dis.TiempoGarantia}','{dis.Descripcion}');";
                NpgsqlCommand execute = new NpgsqlCommand(query, connection);
                execute.ExecuteNonQuery();
                connection.Close();
                return Ok("insercion exitosa");
            }
        }



        [HttpPost]
        public async Task<IActionResult> GetDispositivo()
        {

            return Ok(getListcurrentDisp());
        }



        public List<Dispositivo> getListcurrentDisp()
        { connection.ConnectionString = server.init();
            
            string query = $"SELECT " +
               $"                    \"numeroSerie\", \"consumoElectrico\", \"marca\", \"EstadoActivo\", \"nombreAposento\", \"correoPosedor\" , \"tipo\" , \"tiempoGarantia\", \"descripcion\"" +
               $"         FROM       \"Dispositivo\";";
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            NpgsqlDataReader dr = command.ExecuteReader();
            List<Dispositivo> ListDispositivos = new List<Dispositivo>();
            while (dr.Read())
            {
                Dispositivo dispositivo = new Dispositivo() { NumeroSerie = (int)dr["numeroSerie"], Consumo = (double)dr["consumoElectrico"], Marca = (string)dr["marca"],
                    EstadoActivo = (bool)dr["EstadoActivo"], NombreAposento = (string)dr["nombreAposento"],CorreoPosedor=(string)dr["correoPosedor"], Tipo = (string)dr["tipo"],TiempoGarantia = (int)dr["tiempoGarantia"],Descripcion=(string)dr["descripcion"] };
                ListDispositivos.Add(dispositivo);
            }
            connection.Close();
            return ListDispositivos;

        }

        [HttpDelete("{numeroSerie}")]
        public async Task<IActionResult>DeleteDisp(int numeroSerie)
        {

            connection.ConnectionString = server.init();




            //REVISAR  PORQUE NO ESTA CUMPLIENDO CON EL AND AND Dispositivo.\"estadoActivo\"={false}  AND  \"estadoActivo\"= {false};
            try
            {
                string query1 = $"DELETE FROM \"Dispositivo\" WHERE \"estadoActivo\" = " +
                    $"{false} AND \"numeroSerie\"={numeroSerie};";
                connection.Open();

                NpgsqlCommand command1 = new NpgsqlCommand(query1, connection);
                   command1.ExecuteNonQuery();
                    connection.Close();
                return Ok();      

            }


            catch
            {

                return BadRequest(" el dispositivo no forma parte del stock");
            }

            
           
        }


        [HttpPost]
        public async Task<IActionResult> UpdateDispositivo([FromBody]  Dispositivo disp)
        {
            connection.ConnectionString = server.init();
            try

            {
       

                connection.Open();
                string query = $"UPDATE \"Dispositivo\" SET \"consumoElectrico\"={ disp.Consumo}, \"marca\"='{ disp.Marca}'," +
                    $" \"nombreAposento\"='{disp.NombreAposento}' , \"tipo\"='{disp.Tipo}'" +
                    $" , \"tiempoGarantia\"={disp.TiempoGarantia}  , \"descripcion\"='{disp.Descripcion}'"  +
                   $"         WHERE   \"numeroSerie\" = {disp.NumeroSerie} ;";
                NpgsqlCommand conector = new NpgsqlCommand(query, connection);
                conector.ExecuteNonQuery();
                connection.Close();
                return Ok("Success");
            }
            catch
            {
                return BadRequest("No se pudo actualizar el dispositivo");
            }
        }



     


    }
}




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

        NpgsqlConnection connection = new NpgsqlConnection();
        ServerConexion server = new ServerConexion();
        Validaciones validaciones = new Validaciones();

        // se debe realizar un cambio para que no hayan in consitencias  en la tabla dispositivo

        //Metodos para dispositivos en Stock

        [HttpPost]
        public async Task<IActionResult> insertDispositivoStock([FromBody] DispositivoStock newDispositivo)
        {

            connection.ConnectionString = server.init();
            connection.Open();
            string query = $"INSERT INTO \"DispositivoStock\" VALUES({newDispositivo.NumeroSerie},'{newDispositivo.Marca}',{newDispositivo.ConsumoElectrico},{newDispositivo.CedulaJuridica},'{newDispositivo.Tipo}','{newDispositivo.TiempoGarantia}','{newDispositivo.Descripcion}',{newDispositivo.EnVenta});";

            NpgsqlCommand execute = new NpgsqlCommand(query, connection);

            execute.ExecuteNonQuery();
            connection.Close();
           /*  connection.Open();

           string query1 = $"INSERT INTO \"Tipo\" VALUES('{newDispositivo.Tipo.Nombre}',{newDispositivo.NumeroSerie},'{newDispositivo.Tipo.Descripcion},'{newDispositivo.Tipo.TiempoGarantia}');";
            NpgsqlCommand execute1 = new NpgsqlCommand(query1, connection);
            execute1.ExecuteNonQuery();

            connection.Close();*/
            return Ok("insercion exitosa");






        }



       // metodos  de dispostivo desde app movil 

        [HttpPost]
        public async Task<IActionResult> insertDispositivo(Dispositivo dis)
        {
            connection.ConnectionString = server.init();
            string query = $"SELECT \"correo\" FROM \"Dispositivo\" WHERE \"numeroSerie\" = '{dis.NumeroSerie}';"; 
            NpgsqlCommand conector = new NpgsqlCommand(query, connection);
           
            connection.Open();

            
            if (validaciones.exists(conector))
            {
                connection.Close();
                return BadRequest("element already exist");

            }

            else
            {
                query = $"INSERT INTO \"Dispositivo\" VALUES({dis.NumeroSerie},{dis.Consumo},'{dis.Marca}',{dis.EstadoActivo},'{dis.NombreAposento}','{dis.CorreoPosedor}','{dis.Tipo}','{dis.TiempoGarantia}');";

               

      
                NpgsqlCommand execute = new NpgsqlCommand(query, connection);
                execute.ExecuteNonQuery();
                connection.Close();





                return Ok("insercion exitosa");


                /*  query = " INSERT INTO tipo (numeroserie,nombre, descripcion) VALUES(";
                  query += "'" + dispositivo.NumeroSerie+ "','" + dispositivo.Tipo.Nombre+ "','" + dispositivo.Tipo.Descripcion + "')";
                  NpgsqlCommand execute1 = new NpgsqlCommand(query, connection);
                  execute1.ExecuteNonQuery();

                 Npgsql.PostgresException (0x80004005): 42703: no existe la columna «numeroserie» en la relación «tipo»
     at Npgsql.NpgsqlConnector.<ReadMessage>g__ReadMessageLong|194_0(NpgsqlConnector connector, Boolean async, DataRowLoadingMode dataRowLoadingMode, Boolean readingNotifications, Boolean isReadingPrependedMessage)

                 */
                

            }


        }


    



        private string GetCode(int length)
        {
            StringBuilder comprobante = new StringBuilder();
            var rng = new RNGCryptoServiceProvider();
            var rnd = new byte[1];
            int n = 0;
            while (n < length)
            {
                rng.GetBytes(rnd);
                rnd[0] %= 64;
                if (rnd[0] < 62)
                {
                    n++;
                    comprobante.Append((byte)((rnd[0] <= 9 ? '0' : rnd[0] <= 35 ? 'A' - 10 : 'a' - 36) + rnd[0]));
                }
            }
            return (comprobante.ToString());
        }
    }
}




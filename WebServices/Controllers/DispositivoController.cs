using System;
using System.Collections.Generic;

using System.Text;
using System.Linq;
using Npgsql;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebServices.Models;
using System.Security.Cryptography;


namespace WebServices.Controllers
{

    ///<summary>
    /// Esta Clase   tiene la funcion  para poder  realizar  Gestion des dispositivos que se insertan  a traves de la  aplicación
    ///</summary>
    ///<remarks>
    ///
    ///</remarks>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DispositivoController : ControllerBase
    {

        NpgsqlConnection connection = new NpgsqlConnection();// instacias  mecanismo de comunicacion con postgre
        ServerConexion server = new ServerConexion();// conexion de servidor 
        Validaciones validaciones = new Validaciones();// se validan  restriciones  en la clase 



        // Metodo  que permite la  insercion  de  un dispositivo desde  la aplicacion  movil
        [HttpPost]
        public async Task<IActionResult> setDispositivo(Dispositivo dis)
        {
            connection.ConnectionString = server.init();// se inicia la conexion  con el servidor
            string query = $"SELECT \"correo\" FROM \"Dispositivo\" WHERE \"numeroSerie\" = '{dis.NumeroSerie} ;"; //query  con que se consulta si el dispositivo ya ha sido registrado
            NpgsqlCommand conector = new NpgsqlCommand(query, connection);// relaciona la consulta   con el servidor 
            connection.Open();
            
            if (validaciones.exists(conector))
            {
                connection.Close();
                return BadRequest("element already exist");
            }
            else
            { // si  se demuestra  que no se ha  insertado se procede a  insertar el dispostivo
                query = $"INSERT INTO \"Dispositivo\" VALUES({dis.NumeroSerie},{dis.Consumo},'{dis.Marca}',{dis.EstadoActivo}," +
                    $"'{dis.NombreAposento}','{dis.CorreoPosedor}','{dis.Tipo}','{dis.TiempoGarantia}','{dis.Descripcion}');";
                NpgsqlCommand execute = new NpgsqlCommand(query, connection);
                execute.ExecuteNonQuery();
                connection.Close();
                return Ok();
            }
        }


        // se  devuelven los dispositivos  que se han insertado
        [HttpPost]
        public async Task<IActionResult> GetDispositivo()
        {
            return Ok(getListcurrentDisp());
        }


        //Metodo que genera una lista de  los los dispositivos
        public List<Dispositivo> getListcurrentDisp()
        { connection.ConnectionString = server.init();
            // se  realiza el quer para  traer los dispositivos
            string query = $"SELECT " +
               $"                    \"numeroSerie\", \"consumoElectrico\", \"marca\", \"EstadoActivo\", \"nombreAposento\", \"correoPosedor\" , \"tipo\" , \"tiempoGarantia\", \"descripcion\"" +
               $"         FROM       \"Dispositivo\";";
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            NpgsqlDataReader dr = command.ExecuteReader();
            List<Dispositivo> ListDispositivos = new List<Dispositivo>();
            while (dr.Read())// aqui se lee hasta que en la tabla resultante del query  esté vacia
            {
                Dispositivo dispositivo = new Dispositivo() { NumeroSerie = (int)dr["numeroSerie"], Consumo = (double)dr["consumoElectrico"], Marca = (string)dr["marca"],
                    EstadoActivo = (bool)dr["EstadoActivo"], NombreAposento = (string)dr["nombreAposento"],CorreoPosedor=(string)dr["correoPosedor"], Tipo = (string)dr["tipo"],TiempoGarantia = (int)dr["tiempoGarantia"],Descripcion=(string)dr["descripcion"] };
                ListDispositivos.Add(dispositivo);// se van insertando los dispositivos, al final  se  retornan al HTTp
            }
            connection.Close();
            return ListDispositivos;

        }
        // se borra  el dispositivo  que se selecione 
        [HttpDelete("{numeroSerie}")]
        public async Task<IActionResult>DeleteDisp(int numeroSerie)
        {

            connection.ConnectionString = server.init();

              try
            {
                string query1 = $"DELETE FROM \"Dispositivo\" WHERE \"estadoActivo\" = " +
                    $"{false} AND \"numeroSerie\"={numeroSerie};";// debe cumplir la restriccion de  que solo puede borrar dispositivos  que no esten asociados a un usuario
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

        // se actualizan los parametros  que se solicitan en el enunciado
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




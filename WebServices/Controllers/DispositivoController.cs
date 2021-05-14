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

        



        //Metodos para dispositivos en Stock
       
        //Retorna todos los dispositivos  asociados a un  proveedor 
        [HttpPost]
        public async Task<IActionResult> GetDispositivoStock()
        {
            
            return Ok(getListcurrentDispStock());
        }


        public List<DispositivoStock> getListcurrentDispStock() {
            string query = $"SELECT " +
               $"                    \"numeroSerie\", \"marca\", \"consumoElectrico\", \"cedulaJuridica\", \"tipo\", \"tiempoGarantia\" , \"descripcion\" , \"enVenta\"" +
               $"         FROM       \"DispositivoStock\";";
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            NpgsqlDataReader dr = command.ExecuteReader();
           List<DispositivoStock> ListDispositivosStock = new List<DispositivoStock>();
            while (dr.Read())
            {
                DispositivoStock dispositivoStock = new DispositivoStock() { numeroSerie = (int)dr["numeroSerie"],
                    marca = (string)dr["marca"], consumoElectrico = (double)dr["consumoElectrico"], cedulaJuridica = (int)dr["cedulaJuridica"], 
                    tipo = (string)dr["tipo"], tiempoGarantia = (int)dr["tiempoGarantia"], descripcion = (string)dr["descripcion"], enVenta = (bool)dr["enVenta"] };
                ListDispositivosStock.Add(dispositivoStock);
            }
            connection.Close();
            return ListDispositivosStock;
        }
        [HttpPost]
        public async Task<IActionResult> setDispositivoStock([FromBody] DispositivoStock newDispositivo)
        {
            connection.ConnectionString = server.init();
            connection.Open();
            string query = $"INSERT INTO \"DispositivoStock\" VALUES({newDispositivo.numeroSerie},'{newDispositivo.marca}'," +
                $"{newDispositivo.consumoElectrico},{newDispositivo.cedulaJuridica},'{newDispositivo.tipo}',{newDispositivo.tiempoGarantia}," +
                $"'{newDispositivo.descripcion}',{newDispositivo.enVenta});";
            NpgsqlCommand execute = new NpgsqlCommand(query, connection);

            execute.ExecuteNonQuery();
            connection.Close();
          
            return Ok("insercion exitosa");


        }

        // Metodo para  insertar el Stock cargado por un administrador atraves de  un documento Excel.
        [HttpPost]
        public async Task<IActionResult> setListDispositivosStock([FromBody]  ListaDispositivoStock newDispositivo) {
            connection.ConnectionString = server.init();
            int i = 0;
            while (newDispositivo.stocks.Count > i)
            {
                string query = $"INSERT INTO \"DispositivoStock\" VALUES({newDispositivo.stocks.ElementAt(i).numeroSerie},'{newDispositivo.stocks.ElementAt(i).marca}'," +
                    $"{newDispositivo.stocks.ElementAt(i).consumoElectrico},{newDispositivo.stocks.ElementAt(i).cedulaJuridica},'{newDispositivo.stocks.ElementAt(i).tipo}'," +
                    $"{newDispositivo.stocks.ElementAt(i).tiempoGarantia}," +
                    $"'{newDispositivo.stocks.ElementAt(i).descripcion}',{newDispositivo.stocks.ElementAt(i).enVenta});";
                connection.Open();
                NpgsqlCommand execute = new NpgsqlCommand(query, connection);

                execute.ExecuteNonQuery();
                connection.Close();
                i++;
            }
            return Ok("Success");
      
        }
        // Este metodo se encarg a de eliminar los dispotisitivos por DISTRIBUIDOR que se encuentren disponibles en la tienda, y no se hayan vendido
        [HttpDelete("{numeroSerie}")]
        public async Task<IActionResult> DeleteDispStock(int numeroSerie)
        {

            connection.ConnectionString = server.init();

            try
            {
                
                string query1 = $"DELETE FROM \"DispositivoStock\" WHERE \"numeroSerie\" ={numeroSerie}'AND \"estadoActivo\" = {true};";
                connection.Open();

                NpgsqlCommand command1 = new NpgsqlCommand(query1, connection);
                command1.ExecuteNonQuery();

                connection.Close();

                if (getListcurrentDispStock().Count == 0)
                {
                    return Ok(false);
                }
                return Ok(getListcurrentDispStock());
            }
            catch
            {
                return BadRequest(" el dispositivo no forma parte del stock");


            }        }
       [HttpPost]
        public async Task<IActionResult> UpdateDispositivoStock([FromBody] DispositivoStock disp)
        {
            connection.ConnectionString = server.init();
            try

            {

                connection.Open();
                string query = $"UPDATE DispositivoStock SET \"consumoElectrico\"={ disp.consumoElectrico}, \"marca\"={disp.marca}," +
                    $" \"estadoActivo\"={disp.enVenta} , \"tipo\"={disp.tipo} , \"tiempoGarantia\"={disp.tiempoGarantia} , \"descripcion\"={disp.descripcion}" +
                   $"         WHERE   \"numeroSerie\" = {disp.numeroSerie} ";

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



        //--------------------------------------------------------------------------dispositivos manuales-----------------------------------------------------------






        [HttpPost]
        public async Task<IActionResult> setDispositivo(Dispositivo dis)
        {
            connection.ConnectionString = server.init();
            string query = $"SELECT \"correo\" FROM \"Dispositivo\" WHERE \"numeroSerie\" = '{dis.numeroSerie} ;"; 
            NpgsqlCommand conector = new NpgsqlCommand(query, connection);
            connection.Open();
            
            if (validaciones.exists(conector))
            {
                connection.Close();
                return BadRequest("element already exist");
            }
            else
            {
                query = $"INSERT INTO \"Dispositivo\" VALUES({dis.numeroSerie},{dis.consumo},'{dis.marca}',{dis.estadoActivo}," +
                    $"'{dis.nombreAposento}','{dis.correoPosedor}','{dis.tipo}','{dis.tiempoGarantia}','{dis.descripcion}');";
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
               $"                    \"numeroSerie\", \"consumoElectrico\", \"marca\", \"estadoActivo\", \"nombreAposento\", \"correoPosedor\" , \"tipo\" , \"tiempoGarantia\", \"descripcion\"" +
               $"         FROM       \"Dispositivo\";";
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            NpgsqlDataReader dr = command.ExecuteReader();
            List<Dispositivo> ListDispositivos = new List<Dispositivo>();
            while (dr.Read())
            {
                Dispositivo dispositivo = new Dispositivo() { numeroSerie = (int)dr["numeroSerie"], consumo = (double)dr["consumoElectrico"], marca = (string)dr["marca"],
                    estadoActivo = (bool)dr["estadoActivo"], nombreAposento = (string)dr["nombreAposento"],correoPosedor=(string)dr["correoPosedor"], tipo = (string)dr["tipo"],tiempoGarantia = (int)dr["tiempoGarantia"],descripcion=(string)dr["descripcion"] };
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
                string query1 = $"DELETE FROM \"Dispositivo\" WHERE \"numeroSerie\" = {numeroSerie} ";
                connection.Open();

                NpgsqlCommand command1 = new NpgsqlCommand(query1, connection);
                if (validaciones.validaEstado(command1))
                { 


                    command1.ExecuteNonQuery();

                    connection.Close();
                    if (getListcurrentDisp().Count == 0)
                    {
                        return Ok(false);
                    }
                    else
                    {
                        return Ok(getListcurrentDisp());
                    }

                }
                else
                {
                    return BadRequest(" el dispositivo no forma parte del stock");
                }

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
                string query = $"UPDATE Dispositivo SET \"consumoElectrico\"={ disp.consumo}, \"marca\"={disp.marca}," +
                    $" \"estadoActivo\"={disp.estadoActivo},\"nombreAposento\"={disp.nombreAposento} , \"tipo\"={disp.tipo}" +
                    $" , \"tiempoGarantia\"={disp.tiempoGarantia}  , \"descripcion\"={disp.descripcion}"  +
                   $"         WHERE   \"numeroSerie\" = {disp.numeroSerie} ";



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




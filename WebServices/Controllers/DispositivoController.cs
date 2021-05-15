﻿using System;
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
        [HttpGet]
        public async Task<IActionResult> GetDispositivoStock()
        {
            
            return Ok(getListcurrentDispStock());
        }


        public List<DispositivoStock> getListcurrentDispStock() {
            connection.ConnectionString = server.init();
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
                DispositivoStock dispositivoStock = new DispositivoStock() { NumeroSerie = (int)dr["numeroSerie"],
                    Marca = (string)dr["marca"], ConsumoElectrico = (double)dr["consumoElectrico"], CedulaJuridica = (int)dr["cedulaJuridica"], 
                    Tipo = (string)dr["tipo"], TiempoGarantia = (int)dr["tiempoGarantia"], Descripcion = (string)dr["descripcion"], EnVenta = (bool)dr["enVenta"] };
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
            string query = $"INSERT INTO \"DispositivoStock\" VALUES({newDispositivo.NumeroSerie},'{newDispositivo.Marca}'," +
                $"{newDispositivo.ConsumoElectrico},{newDispositivo.CedulaJuridica},'{newDispositivo.Tipo}',{newDispositivo.TiempoGarantia}," +
                $"'{newDispositivo.Descripcion}',{newDispositivo.EnVenta});";
            NpgsqlCommand execute = new NpgsqlCommand(query, connection);

            execute.ExecuteNonQuery();
            connection.Close();
          
            return Ok();


        }

        // Metodo para  insertar el Stock cargado por un administrador atraves de  un documento Excel.
        [HttpPost]
        public async Task<IActionResult> setListDispositivosStock([FromBody]  ListaDispositivoStock newDispositivo) {
            connection.ConnectionString = server.init();
            int i = 0;
            while (newDispositivo.Stocks.Count > i)
            {
                string query = $"INSERT INTO \"DispositivoStock\" VALUES({newDispositivo.Stocks.ElementAt(i).NumeroSerie},'{newDispositivo.Stocks.ElementAt(i).Marca}'," +
                    $"{newDispositivo.Stocks.ElementAt(i).ConsumoElectrico},{newDispositivo.Stocks.ElementAt(i).CedulaJuridica},'{newDispositivo.Stocks.ElementAt(i).Tipo}'," +
                    $"{newDispositivo.Stocks.ElementAt(i).TiempoGarantia}," +
                    $"'{newDispositivo.Stocks.ElementAt(i).Descripcion}',{newDispositivo.Stocks.ElementAt(i).EnVenta});";
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
                
                string query1 = $"DELETE FROM \"DispositivoStock\" WHERE \"numeroSerie\" ='{numeroSerie}';";
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

                connection.Open();
                string query = $"UPDATE \"DispositivoStock\" SET \"consumoElectrico\" = {disp.ConsumoElectrico}, \"marca\" = '{disp.Marca}'," +
                    $" \"tipo\" = '{disp.Tipo}' , \"tiempoGarantia\" = {disp.TiempoGarantia} , \"descripcion\" = '{disp.Descripcion}'" +
                   $"         WHERE   \"numeroSerie\" = {disp.NumeroSerie} ;";


                NpgsqlCommand conector = new NpgsqlCommand(query, connection);
                conector.ExecuteNonQuery();
                connection.Close();
                return Ok();
            
            
               
            
        }

        


        //--------------------------------------------------------------------------dispositivos manuales-----------------------------------------------------------






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




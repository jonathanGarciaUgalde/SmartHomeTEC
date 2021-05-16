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
    /// Esta Clase   tiene la funcion  para poder  realizar  Gestion des dispositivos que se insertan  a traves de la pagina web 
    ///</summary>
    ///<remarks>
    ///
    ///</remarks>

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DispostivoStockController : ControllerBase
    {
        NpgsqlConnection connection = new NpgsqlConnection();// instacias  mecanismo de comunicacion con postgre
        ServerConexion server = new ServerConexion();// conexion de servidor 
        Validaciones validaciones = new Validaciones();

        //Retorna todos los dispositivos  asociados a un  proveedor 
        [HttpGet]
        public async Task<IActionResult> GetDispositivoStock()
        {
            return Ok(getListcurrentDispStock());
        }

        //Metodo  que devuelve  los dispositivos  insertados  en la pagina web
        public List<DispositivoStock> getListcurrentDispStock()
        {
            connection.ConnectionString = server.init();
            string query = $"SELECT " +
               $"                    \"numeroSerie\", \"marca\", \"consumoElectrico\", \"cedulaJuridica\", \"tipo\", \"tiempoGarantia\" , \"descripcion\" , \"precio\" , \"enVenta\"" +
               $"         FROM       \"DispositivoStock\";";
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            NpgsqlDataReader dr = command.ExecuteReader();
            List<DispositivoStock> ListDispositivosStock = new List<DispositivoStock>();
            while (dr.Read())
            {// objeto de dispositivo stock, se carga con la informacion de la tabla, hasta que dr.read() sea null
                DispositivoStock dispositivoStock = new DispositivoStock()
                {
                    NumeroSerie = (int)dr["numeroSerie"],
                    Marca = (string)dr["marca"],
                    ConsumoElectrico = (double)dr["consumoElectrico"],
                    CedulaJuridica = (int)dr["cedulaJuridica"],
                    Tipo = (string)dr["tipo"],
                    TiempoGarantia = (int)dr["tiempoGarantia"],
                    Descripcion = (string)dr["descripcion"],
                    Precio = (int)dr["precio"],

                    EnVenta = (bool)dr["enVenta"]
                };
                ListDispositivosStock.Add(dispositivoStock);
            }
            connection.Close();
            return ListDispositivosStock;
        }

        //Este metodo permite insertar  de forma manual un dispositivo.
        [HttpPost]
       public async Task<IActionResult> setDispositivoStock([FromBody] DispositivoStock newDispositivo)
        {
            connection.ConnectionString = server.init();
            connection.Open();
            string query = $"INSERT INTO \"DispositivoStock\" VALUES({newDispositivo.NumeroSerie},'{newDispositivo.Marca}'," +
                $"{newDispositivo.ConsumoElectrico},{newDispositivo.CedulaJuridica},'{newDispositivo.Tipo}',{newDispositivo.TiempoGarantia}," +
                $"'{newDispositivo.Descripcion}',{newDispositivo.Precio},{newDispositivo.EnVenta});";
            NpgsqlCommand execute = new NpgsqlCommand(query, connection);
            execute.ExecuteNonQuery();
            connection.Close();
            return Ok();
        }
        // Metodo para  insertar el Stock cargado por un administrador atraves de  un documento Excel.
         //toma los valores  que se van  evaluando el  while y prodece  a insertarlo. devuelve un mensaje de confirmacion Ok
        [HttpPost]
        public async Task<IActionResult> setListDispositivosStock([FromBody] ListaDispositivoStock newDispositivo)
        {
            connection.ConnectionString = server.init();
            int i = 0;
            while (newDispositivo.Stocks.Count > i)
            {// inserta 1 por cada vez que i aumente 
                string query = $"INSERT INTO \"DispositivoStock\" VALUES({newDispositivo.Stocks.ElementAt(i).NumeroSerie},'{newDispositivo.Stocks.ElementAt(i).Marca}'," +
                    $"{newDispositivo.Stocks.ElementAt(i).ConsumoElectrico},{newDispositivo.Stocks.ElementAt(i).CedulaJuridica},'{newDispositivo.Stocks.ElementAt(i).Tipo}'," +
                    $"{newDispositivo.Stocks.ElementAt(i).TiempoGarantia}," +
                    $"'{newDispositivo.Stocks.ElementAt(i).Descripcion}',{newDispositivo.Stocks.ElementAt(i).Precio},{newDispositivo.Stocks.ElementAt(i).EnVenta});";
                connection.Open();
                NpgsqlCommand execute = new NpgsqlCommand(query, connection);
                execute.ExecuteNonQuery();
                connection.Close();
                i++;
            }
            return Ok();

        }
        // Este metodo Recibe  un numero de serie, este  dispositivo solo e posible eliminarlo  si  no ha sido vendido, 
        // por esa razón se evalua  que  los elementos  tengan el parametro  "enVenta"=true para saber que no se han selecionado aun 
         [HttpDelete("{numeroSerie}")]
        public async Task<IActionResult> DeleteDispStock(int numeroSerie)
        {

            connection.ConnectionString = server.init();

            try
            {

                string query1 = $"DELETE FROM \"DispositivoStock\" WHERE \"numeroSerie\" ={numeroSerie};";
                connection.Open();

                NpgsqlCommand command1 = new NpgsqlCommand(query1, connection);
                command1.ExecuteNonQuery();

                connection.Close();
                if (getListcurrentDispStock().Count == 0)
                {
                    return Ok();

                }

                return Ok(getListcurrentDispStock());
            }
            catch
            {
                return BadRequest(" el dispositivo no forma parte del stock");


            }
        }

        //Este metodo recibe  un dispositivo de la pagina web  y lo actualiza, validano que este esté almacenado y que los parametros 
        // de inserción sean los permitidos
        [HttpPost]
        public async Task<IActionResult> UpdateDispositivoStock([FromBody] DispositivoStock disp)
        {
            connection.ConnectionString = server.init();
            connection.Open();
            string query = $"UPDATE \"DispositivoStock\" SET \"consumoElectrico\" = {disp.ConsumoElectrico}, \"marca\" = '{disp.Marca}'," +
                $" \"tipo\" = '{disp.Tipo}' , \"tiempoGarantia\" = {disp.TiempoGarantia} , \"descripcion\" = '{disp.Descripcion}', \"precio\" = {disp.Precio}" +
               $"         WHERE   \"numeroSerie\" = {disp.NumeroSerie} ;";

            NpgsqlCommand conector = new NpgsqlCommand(query, connection);
            conector.ExecuteNonQuery();
            connection.Close();
            return Ok();




        }






    }
}

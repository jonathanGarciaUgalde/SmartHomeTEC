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
    public class PedidoFacturaController : ControllerBase
    {



        NpgsqlConnection connection = new NpgsqlConnection();
        ServerConexion server = new ServerConexion();
        Validaciones val = new Validaciones();


        // lista general de los pedidos que se han realizado en la tienda 
        [HttpPost]
        public async Task<IActionResult> GetPedidos()
        {
            return Ok(getListcurrentDispStock());
        }
        public List<Pedido> getListcurrentDispStock()
        {
            string query = $"SELECT " +
               $"                    \"numero\", \"correoComprador\", \"fecha\", \"numeroSerie\"\"" +
               $"         FROM       \"Pedido\";";
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);

            command.ExecuteNonQuery();
            NpgsqlDataReader dr = command.ExecuteReader();
            List<Pedido> ListPedidos = new List<Pedido>();
            while (dr.Read())
            {
                Pedido pedidos = new Pedido() {
                    numeroPedido = (int)dr["numero"],
                    correoComprador = (string)dr["correoComprador"],
                    fecha = (string)dr["fecha"],
                    numeroSerie = (int)dr["numeroSerie"]
                };
                ListPedidos.Add(pedidos);
            }
            connection.Close();
            return ListPedidos;
        }


        // Metodo para  insertar el Stock cargado por un administrador atraves de  un documento Excel.
        [HttpPost]
        public async Task<IActionResult> setPedido([FromBody] Pedido pedido)
        {
            connection.ConnectionString = server.init();
            string query = $"INSERT INTO \"Pedido\" VALUES('{pedido.correoComprador}','{pedido.fecha}',{pedido.numeroSerie});";
            connection.Open();
            NpgsqlCommand execute = new NpgsqlCommand(query, connection);
            execute.ExecuteNonQuery();
            connection.Close();
            return Ok("Success");

        }


        [HttpPost]
        public async Task<IActionResult> GetAllPedidos()
        {
            connection.ConnectionString = server.init();

            string query = $"SELECT " +
               $"                    \"numero\", \"correoComprador\", \"fecha\", \"numeroSerie\"" +
               $"         FROM       \"Pedido\";";
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            NpgsqlDataReader dr = command.ExecuteReader();
            List<Pedido> ListPedidos = new List<Pedido>();
            while (dr.Read())
            {
                Dispositivo dispositivo = new Dispositivo() {
                    numeroSerie = (int)dr["numeroSerie"], consumo = (double)dr["consumoElectrico"], marca = (string)dr["marca"],
                    estadoActivo = (bool)dr["estadoActivo"], nombreAposento = (string)dr["nombreAposento"], correoPosedor = (string)dr["correoPosedor"], tipo = (string)dr["tipo"], tiempoGarantia = (int)dr["tiempoGarantia"], descripcion = (string)dr["descripcion"]
                };
                ListPedidos.Add(dispositivo);
            }
            connection.Close();
        }





    }
}


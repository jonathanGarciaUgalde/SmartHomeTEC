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
                    NumeroPedido = (int)dr["numero"],
                    CorreoComprador = (string)dr["correoComprador"],
                    Fecha = (string)dr["fecha"],
                    NumeroSerie = (int)dr["numeroSerie"]
                };
                ListPedidos.Add(pedidos);
            }
            connection.Close();
            return ListPedidos;
        }

/*
        [HttpPost]
        public async Task<IActionResult> SetVenta([FromBody] Pedido pedido)
        {

            try
            {
                connection.ConnectionString = server.init();
                string query = $"INSERT INTO \"Pedido\" VALUES('{pedido.CorreoComprador}','{pedido.Fecha}',{pedido.NumeroSerie});";
                connection.Open();
                NpgsqlCommand execute = new NpgsqlCommand(query, connection);
                execute.ExecuteNonQuery();
                connection.Close();

                // se toma el dispositivo y se le  actualiza el  estado para que ya no esté disponible en los dispositivos
                string query1 = $"UPDATE \"DispositivoStock\" SET \"enVenta\" = {true} WHERE \"numeroSerie\" = {pedido.NumeroSerie};";
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(query1, connection);
                command.ExecuteNonQuery();
                connection.Close();

                return Ok();

            }
            catch { 
            
            
            
            
            }

        }*/


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
                 Pedido pedido = new Pedido() {
                    NumeroPedido = (int)dr["numero"], CorreoComprador = (string)dr["correoComprador"], NumeroSerie = (int)dr["numeroSerie"]
                   
                };
                ListPedidos.Add(pedido);
            }
            connection.Close();
            return Ok(ListPedidos);
        }





    }
}


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
                Pedido pedidos = new Pedido()
                {
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
    }
}


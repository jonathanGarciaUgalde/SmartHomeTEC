using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServices.Models;
using Npgsql;
using System.Text;
using System.Security.Cryptography;

namespace WebServices.Controllers
{



    ///<summary>
    /// Esta Clase  le permmite al usuario  realizar un pedido del producto selecionado  y poder   obtener así su factura  y  cerfificado de garantia  
    ///</summary>
    ///<remarks>
    ///
    ///</remarks>

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
        //Devuelve los pedidos que se han realizado 
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

//Este metodo es el  encargado de realizar la gestion del pedido, lo toma, lo valida,  lo  inserta
// i se devuelve el valor  de los 
        [HttpPost]
        public async Task<IActionResult> SetVenta([FromBody] Pedido pedido)
        {
            connection.ConnectionString = server.init();
            connection.Open();
            string query = $"INSERT INTO \"Pedido\"(\"correoComprador\"," +
                $"\"fecha\",\"numeroSerie\") VALUES('{pedido.CorreoComprador}'" +
                $",'{pedido.Fecha}',{pedido.NumeroSerie});";

            NpgsqlCommand insertVenta = new NpgsqlCommand(query, connection);
            insertVenta.ExecuteNonQuery();

            // se toma el dispositivo y se le  actualiza el  estado para que 
            //ya no esté disponible en los dispositivos
            string query1 = $"UPDATE \"DispositivoStock\" SET \"enVenta\" ={ false } WHERE \"numeroSerie\" = {pedido.NumeroSerie};";

            NpgsqlCommand command = new NpgsqlCommand(query1, connection);
            command.ExecuteNonQuery();
            connection.Close();
           // Tuple<Factura, CertificadoGarantia> request = new Tuple<Factura, CertificadoGarantia>(GetFactura(pedido), GetCertificado(pedido));



            return Ok(GetFactura(pedido));
        }


          
        
        public Tuple<Factura, CertificadoGarantia> GetFactura(Pedido pedido) {
            connection.ConnectionString = server.init();
          
            // se trae el Dispositivo para  insertarlo en la Factura 
            string queryDipositivoStock =  $"SELECT " +
               $"                    \"numeroSerie\", \"marca\", \"consumoElectrico\"," +
               $" \"cedulaJuridica\", \"tipo\", \"tiempoGarantia\" , \"descripcion\", \"precio\", \"enVenta\"" +
               $"         FROM      " +
               $" \"DispositivoStock\"" +
               $" WHERE" +
               $" \"numeroSerie\" = {pedido.NumeroSerie};";
            connection.Open();
            NpgsqlCommand commandDisStock = new NpgsqlCommand(queryDipositivoStock, connection);
            commandDisStock.ExecuteNonQuery();
            NpgsqlDataReader dr = commandDisStock.ExecuteReader();
            dr.Read();
            DispositivoStock dispositivoStock = new DispositivoStock()
            {
                NumeroSerie = (int)dr["numeroSerie"],
                Marca = (string)dr["marca"],
                ConsumoElectrico = (double)dr["consumoElectrico"],
                CedulaJuridica = (int)dr["cedulaJuridica"],
                Tipo = (string)dr["tipo"],
                TiempoGarantia = (int)dr["tiempoGarantia"],
                Descripcion = (string)dr["descripcion"],
                Precio=(int)dr["precio"],
                EnVenta = (bool)dr["enVenta"]
            };
            Factura factura = new Factura() {

                Consecutivo = GetCode(3),
                Fecha =$" {DateTime.Now.DayOfWeek.ToString()} {DateTime.Now.Day.ToString()}of the year {DateTime.Now.Year.ToString()}",
                Hora = $"{DateTime.Now.Hour.ToString()}:{DateTime.Now.Minute.ToString()}",
                CorreoComprador = pedido.CorreoComprador,
                Producto = dispositivoStock
            };
            CertificadoGarantia garantia = new CertificadoGarantia()
            {
                Serie = pedido.NumeroSerie,

                CorreoUsuario = pedido.CorreoComprador,
                FechaLimite = factura.Hora
            };


            Tuple<Factura, CertificadoGarantia> request = new Tuple<Factura, CertificadoGarantia>(factura, garantia);

            connection.Close();
            return request;
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
                 Pedido pedido = new Pedido() {
                    NumeroPedido = (int)dr["numero"], CorreoComprador = (string)dr["correoComprador"], NumeroSerie = (int)dr["numeroSerie"]
                   
                };
                ListPedidos.Add(pedido);
            }
            connection.Close();
            return Ok(ListPedidos);
        }


        public  string GetCode(int length)
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
            return comprobante.ToString();
        }




    }
}


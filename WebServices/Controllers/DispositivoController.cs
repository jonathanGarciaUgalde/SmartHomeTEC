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
    [Route("api/[controller]")]
    [ApiController]
    public class DispositivoController : ControllerBase
    {

        NpgsqlConnection connection = new NpgsqlConnection();
        ServerConexion server = new ServerConexion();
        Validaciones validaciones = new Validaciones();

        // se debe realizar un cambio para que no hayan in consitencias  en la tabla dispositivo
        [HttpPost("add/dispositivo")]
        public async Task<IActionResult> insertDispositivo(Dispositivo newDispositivo)
        {
            connection.ConnectionString = server.init();
            string query = "select Numero_serie from dispositivo where dispositivo.Numero_serie= '" + newDispositivo.Numero_serie + "'";
            NpgsqlCommand conector = new NpgsqlCommand(query, connection);
            if (validaciones.exists(conector))
            {
                connection.Close();
                return BadRequest("Este dispositivo ya está  registrado");
            }
            else

            var serie = GetCode(4);
            query = "insert into dispositivo VALUES(";
            query += "'" +serie+ "'," + "'" + newDispositivo.Marca + "'," + "'" + newDispositivo.Consumo + "'," + "'" + newDispositivo.Estado + "')";



            connection.Open();

            NpgsqlCommand execute = new NpgsqlCommand(query, connection);
            execute.ExecuteNonQuery();

            query = " insert into tipo VALUES(";
            query += "'" + newDispositivo.Numero_serie + "','" + newDispositivo.Tipo.Nombre + "','" + newDispositivo.Tipo.Descripcion + "')";
            NpgsqlCommand execute1 = new NpgsqlCommand(query, connection);
            execute1.ExecuteNonQuery();
            return Ok("insercion exitosa");






        }


  

        [HttpPost("add/dispositivoManual")]
        public async<IActionResult> AddClienteAsync(DispositivoManual dispositivo)
        {
            connection.ConnectionString = server.init();
            string query = "select Numero_serie from dispositivo_manual where dispositivo_manual.Numero_serie = '" + dispositivo.Numero_serie + "'";
            NpgsqlCommand conector = new NpgsqlCommand(query, connection);
            if (validaciones.exists(conector))
            {
                connection.Close();
                return BadRequest("User already exist");

            }


            else
            {
                query = "insert into usuario VALUES(";
                query += "'" + newUser.Correo + "'," + "'" + newUser.Password + "'," + "'" + newUser.Nombre + "'," + "'" + newUser.Apellidos + "')";

                connection.Open();

                NpgsqlCommand execute = new NpgsqlCommand(query, connection);
                execute.ExecuteNonQuery();
                query = " insert into region_x_usuario VALUES(";
                query += "'" + newUser.Region.Pais + "','" + newUser.Correo + "','" + newUser.Region.Continente + "')";
                NpgsqlCommand execute1 = new NpgsqlCommand(query, connection);
                execute1.ExecuteNonQuery();

                return Ok("Success");




        public int Numero_serie { get; set; }
        public string Marca { get; set; }
        public string Consumo { get; set; }
        public string Estado { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }// estos son atributos  que pueden ser  null  porque  en la tabla estan validados como tal
        public string FechaLimiteGarantia { get; set; }// estos son atributos  que pueden ser  null  porque  en la tabla estan validados como tal
        public Tipo Tipo { get; set; } // se le asigna al modelo de tipo la estructura 

    



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




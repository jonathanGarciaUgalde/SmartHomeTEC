using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Npgsql;
using WebServices.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DistribuidorController : ControllerBase
    {
        NpgsqlConnection connection = new NpgsqlConnection();
        ServerConexion server = new ServerConexion();
        Distribuidor distModel = new Distribuidor();

        // GET: api/<DistribuidorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DistribuidorController>
        [HttpGet]
        public async Task<IActionResult> GetDistribuidores([FromBody] User user)
        {                        
            connection.ConnectionString = server.init();
            string query = $"SELECT " +
                $"                    \"Nombre\", \"Pais\", \"Continente\" " +
                $"         FROM       \"distribuidor\" " +
                $"         INNER JOIN \"region_x_distribuidor\" " +
                $"            ON \"Cedula_juridica\" = \"Distribuidor_asociado\";";

            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            NpgsqlDataReader dr = command.ExecuteReader();
            List<Distribuidor> distribuidores = new List<Distribuidor>();


            int numeroDistribuidor = 1;
            while (dr.Read())
            {
                Region region = new Region() { Pais = (string)dr["Pais"], Continente = (string)dr["Continente"] };
                Distribuidor distribuidor = new Distribuidor() { Nombre = (string)dr["Nombre"], Region = region };
                distribuidores.Add(distribuidor);

            }
            connection.Close();
            return Ok(distribuidores);
        }

        // POST api/<DistribuidorController>/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Distribuidor dist)
        {
            connection.ConnectionString = server.init();
            string query = $"INSERT INTO distribuidor VALUES({dist.CedulaJuridica},'{dist.Nombre}');";
            connection.Open();

            NpgsqlCommand command1 = new NpgsqlCommand(query, connection);
            command1.ExecuteNonQuery();

            query = $"INSERT INTO region_x_distribuidor VALUES('{dist.Region.Pais}',{dist.CedulaJuridica},'{dist.Region.Continente}')";
            NpgsqlCommand command2 = new NpgsqlCommand(query, connection);
            command2.ExecuteNonQuery();
            
            connection.Close();
            return Ok();
        }

        // PUT api/<DistribuidorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DistribuidorController>/5
        [HttpDelete("{CedulaJuridica}")]
        public async Task<IActionResult> Delete(int CedulaJuridica)
        {

            connection.ConnectionString = server.init();
            string query = $"DELETE FROM region_x_distribuidor WHERE \"Distribuidor_asociado\" = {CedulaJuridica};";
            
            connection.Open();


            NpgsqlCommand command1 = new NpgsqlCommand(query, connection);
            command1.ExecuteNonQuery();

            query = $"DELETE FROM distribuidor WHERE \"Cedula_juridica\" = {CedulaJuridica};";
            NpgsqlCommand command2 = new NpgsqlCommand(query, connection);
            command2.ExecuteNonQuery();

            connection.Close();
            return Ok();
        }
    }
}

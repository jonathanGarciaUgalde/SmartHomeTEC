using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebServices.Models;

namespace WebServices.Controllers { 

    [Route("api/[controller]/[action]")]
[ApiController]
public class TipoAdminController : ControllerBase

{

    NpgsqlConnection connection = new NpgsqlConnection();
    ServerConexion server = new ServerConexion();
    Validaciones validaciones = new Validaciones();


        
        [HttpPost]
        public async Task<IActionResult> GetTipo()
        {
            connection.ConnectionString = server.init();
            string query = $"SELECT " +
                $"                    \"correoDeAdmin\", \"nombre\", \"descripcion\", \"tiempoGarantia\" " +
                $"         FROM       \"Tipo\";";

            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            NpgsqlDataReader dr = command.ExecuteReader();
            List<TipoAdmin> listTipos = new List<TipoAdmin>();
            while (dr.Read())
            {
        TipoAdmin estructurasTipo= new TipoAdmin() { correoAdmin = (string)dr["correoDelAdmin"], nombre =(string)dr["nombre"],
            descripcionDelTipo=(string)dr["descripcion"],tiempoGarantia=(int)dr["tiempoGarantia"]};
                listTipos.Add(estructurasTipo);
            }
            connection.Close();
            return Ok(listTipos);
        }



        // POST api/<DistribuidorController>/Create
        [HttpPost]
        public async Task<IActionResult> SetTipo([FromBody] TipoAdmin newTipo)
        {
            connection.ConnectionString = server.init();
            string query = $"INSERT INTO \"Tipo\" VALUES({newTipo.correoAdmin},'{newTipo.nombre}','{newTipo.descripcionDelTipo}',{newTipo.tiempoGarantia});";
            connection.Open();

            NpgsqlCommand command1 = new NpgsqlCommand(query, connection);
            command1.ExecuteNonQuery();


            connection.Close();
            return Ok();
        }

       
        // DELETE api/<DistribuidorController>/5
        [HttpDelete("{nombre}")]
        public async Task<IActionResult> Delete(String nombre)
        {

            connection.ConnectionString = server.init();
            string query = $"DELETE FROM \"Tipo\" WHERE \"nombre\" = {nombre};";
            connection.Open();

            NpgsqlCommand command1 = new NpgsqlCommand(query, connection);
            command1.ExecuteNonQuery();

            connection.Close();
            return Ok();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using WebServices.Models;
using Npgsql;



using WebServices.Models;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        NpgsqlConnection connection = new NpgsqlConnection();
        ServerConexion server = new ServerConexion();
        LoginModel lg = new LoginModel();
        
        //Método que se comunica mediante el protocolo http para validar si el usuario que inicia sesión está registrado.        
        [HttpPost]
        [Route("{correo}/{password}")]
        public async Task<IActionResult> Login(string correo, string password)
        {

            connection.ConnectionString = server.init();
            connection.Open();

            string query = $"SELECT \"correo\",\"password\" FROM \"Usuario\" WHERE \"correo\" = '{correo}';";
            NpgsqlCommand conector = new NpgsqlCommand(query, connection);

            if (lg.verifyLogin(conector, correo, password))
            {
                connection.Close();
                return Ok(true);
            }
           
           
            return BadRequest("Username or password is incorrect");
        }
        
        [HttpPost]
        public async Task<IActionResult> Signin([FromBody] User  newUser)
        {            
            connection.ConnectionString = server.init();
            connection.Open();

            string query = $"insert into \"Usuario\" VALUES('{newUser.Correo}','{newUser.Password}', '{newUser.Nombre}', '{newUser.Apellidos}', '{newUser.Region.Continente}', '{newUser.Region.Pais}')";
            NpgsqlCommand execute = new NpgsqlCommand(query, connection);
            execute.ExecuteNonQuery();

            int i = 0;
            while (newUser.Direccion.Count > i)
            {
                query = $"insert into \"direccionEntrega\" VALUES('{newUser.Correo}','{ newUser.Direccion.ElementAt(i).Ubicacion}');";
                
                NpgsqlCommand execute3 = new NpgsqlCommand(query, connection);
                execute3.ExecuteNonQuery();
                i++;

            }
            connection.Close();
            return Ok("Success");
        }

        [HttpPost] //api/User/Aposento
        public async Task<IActionResult> Aposento([FromBody] Aposento aposento)
        {

            connection.ConnectionString = server.init();

            try
            {
                connection.Open();
                string userQuery = $"SELECT \"correo\" FROM \"Usuario\" WHERE \"correo\" = '{aposento.Correo}';";
                NpgsqlCommand userCommand = new NpgsqlCommand(userQuery, connection);
                userCommand.ExecuteNonQuery();

                NpgsqlDataReader dr = userCommand.ExecuteReader();                
                dr.Read(); //Si no existen filas por ller este metodo fallaria. Es debido a esto que se usa el try/catch.
                User currentUser = new User() { Correo = (string)dr["correo"] };
                connection.Close();
            }
            catch
            {
                connection.Close();
                return BadRequest("User not found");
            }

            try
            {
                connection.Open();
                string query = $"INSERT INTO \"Aposento\" VALUES('{aposento.Correo}','{aposento.Nombre}');";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();

                return Ok();
            }
            catch
            {
                connection.Close();
                return BadRequest("Aposento ya definido");
            }

        }

        [HttpPost] //Route-> api/User/Credenciales
        public async Task<IActionResult> Credenciales([FromBody] User user)
        {
            connection.ConnectionString = server.init();
            string query = $"SELECT " +
                $"              \"nombre\", \"apellidos\", \"pais\", \"continente\" " +
                $"         FROM " +
                $"              \"Usuario\" " +
                $"         WHERE \"correo\" = '{user.Correo}';";

            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();

            try
            {
                NpgsqlDataReader dr = command.ExecuteReader();
                dr.Read();

                Region outputRegion = new Region() { Pais = (string)dr["pais"], Continente = (string)dr["continente"] };
                User outputUser = new User() { Nombre = (string)dr["nombre"], Apellidos = (string)dr["apellidos"], Region = outputRegion };

                connection.Close();


                query = $"SELECT " +
                        $"      \"ubicacion\" " +
                        $"FROM " +
                        $"      \"direccionEntrega\" " +
                        $"WHERE \"correo\" = '{user.Correo}';";

                connection.Open();
                command = new NpgsqlCommand(query, connection);
                command.ExecuteNonQuery();
                dr = command.ExecuteReader();

                List<Direccion> direcciones = new List<Direccion>();
                while (dr.Read())
                {
                    Direccion direccion = new Direccion() { Ubicacion = (string)dr["ubicacion"] };
                    direcciones.Add(direccion);
                }
                outputUser.Direccion = direcciones;

                connection.Close();
                return Ok(outputUser);
            }
            
            catch
            {
                return BadRequest("User not found");
            }
            

        }

        [HttpPost] //Route-> api/User/ActivarDispositivo
        public async Task<IActionResult> ActivarDispositivo([FromBody] Historial historialDisp)
        {
            if (historialDisp.EstadoActivo)
            {
                connection.ConnectionString = server.init();
                string query = $"UPDATE \"Dispositivo\" SET \"estadoActivo\" = {true} WHERE \"numeroSerie\" = {historialDisp.NumeroSerie};";

                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();

                var dateString = DateTime.Now.ToString("yyyy-MM-dd");
                var timeZone = DateTime.Now.ToString("hh:mm:ss");

                connection.Open();
                query = $"INSERT INTO \"Historial\"(\"fechaActivacion\",\"horaActivacion\",\"numeroSerie\") " +
                        $"  VALUES('{dateString}','{timeZone}', {historialDisp.NumeroSerie});";

                command = new NpgsqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();
                return Ok();
            }
            else
            {
                return BadRequest("Para activar debe confirmalo con el atributo EstadoActivo = true");
            }            
        }

        [HttpPost] //Route-> api/User/DesactivarDispositivo
        public async Task<IActionResult> DesactivarDispositivo([FromBody] Historial historialDispositivo)
        {
            if (!historialDispositivo.EstadoActivo)
            {
                connection.ConnectionString = server.init();
                string query = $"UPDATE \"Dispositivo\" SET \"estadoActivo\" = {false} WHERE \"numeroSerie\" = {historialDispositivo.NumeroSerie};";

                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();

                var dateString = DateTime.Now.ToString("yyyy-MM-dd");
                var timeZone = DateTime.Now.ToString("hh:mm:ss");

                
                query = $"UPDATE \"Historial\"" +
                        $"SET    \"fechaDesactivacion\" =  '{dateString}', \"horaDesactivacion\" = '{timeZone}'" +
                        $"WHERE \"numeroSerie\" = {historialDispositivo.NumeroSerie} " +
                        $"AND \"horaActivacion\" = " +
                        $" (SELECT \"horaActivacion\" " +
                        $"  FROM \"Historial\" " +
                        $"  WHERE \"numeroSerie\" = {historialDispositivo.NumeroSerie} " +
                        $"  ORDER BY \"horaActivacion\" DESC" +
                        $"  LIMIT 1);";
                connection.Open();

                command = new NpgsqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();

                return Ok();
            }
            else
            {
                return BadRequest("Para desactivar debe confirmalo con el atributo EstadoActivo = false");
            }
        }

        [HttpPost] //Route-> api/User/DesactivarDispositivo
        public async Task<IActionResult> GetDispositivos([FromBody] User user)
        {
            connection.ConnectionString = server.init();
            string query = $"SELECT " +
                $"                \"nombreAposento\", \"tipo\", \"numeroSerie\", \"estadoActivo\", \"consumoElectrico\", \"correoPosedor\", \"marca\" " +
                $"          FROM  \"Dispositivo\" " +
                $"          WHERE \"correoPosedor\" = '{user.Correo}';";

            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            NpgsqlDataReader dr = command.ExecuteReader();

            List<Dispositivo> dispositivos = new List<Dispositivo>();

            try
            {
                while (dr.Read())
                {                    
                    Dispositivo dispositivo = new Dispositivo() { 
                                                                    Tipo = (string)dr["tipo"],
                                                                    NombreAposento = (string)dr["nombreAposento"],
                                                                    NumeroSerie = (int)dr["numeroSerie"]
                    };
                    dispositivos.Add(dispositivo);

                }
                connection.Close();
                return Ok(dispositivos);
            }
            catch
            {
                connection.Close();
                return BadRequest("Usted no posee dispositivos");
            }            
        }

        [HttpPost] //Route-> api/User/DesactivarDispositivo
        public async Task<IActionResult> GetEstadoDispositivos([FromBody] User user)
        {
            connection.ConnectionString = server.init();
            string query = $"SELECT " +
                $"                \"nombreAposento\", \"tipo\", \"numeroSerie\", \"estadoActivo\" " +
                $"          FROM  \"Dispositivo\" " +
                $"          WHERE \"correoPosedor\" = '{user.Correo}';";

            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            NpgsqlDataReader dr = command.ExecuteReader();

            List<Dispositivo> dispositivos = new List<Dispositivo>();

            try
            {
                while (dr.Read())
                {
                    Dispositivo dispositivo = new Dispositivo()
                    {
                        Tipo = (string)dr["tipo"],
                        NombreAposento = (string)dr["nombreAposento"],
                        NumeroSerie = (int)dr["numeroSerie"],
                        EstadoActivo = (bool)dr["estadoActivo"]
                    };
                    dispositivos.Add(dispositivo);

                }
                connection.Close();
                return Ok(dispositivos);
            }
            catch
            {
                connection.Close();
                return BadRequest("Usted no posee dispositivos");
            }
        }

        [HttpPost] //Route-> api/User/DesactivarDispositivo
        public async Task<IActionResult> GetHistorial([FromBody] Dispositivo disp)
        {
            connection.ConnectionString = server.init();
            string query = $"SELECT " +
                $"                * " +
                $"          FROM  \"Historial\" " +
                $"          WHERE \"numeroSerie\" = {disp.NumeroSerie} AND \"fechaDesactivacion\" IS NOT NULL;";

            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            NpgsqlDataReader dr = command.ExecuteReader();

            List<Historial> historial = new List<Historial>();

            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var fechaAct = (string)dr["fechaActivacion"];
                        var fechaDesact = (string)dr["fechaDesactivacion"];

                        Historial historialActual = new Historial()
                        {
                            FechaActivacion = (string)dr["fechaActivacion"],
                            FechaDesactivacion = (string)dr["fechaDesactivacion"],
                            HoraActivacion = (string)dr["horaActivacion"],
                            HoraDesactivacion = (string)dr["horaDesactivacion"],
                            NumeroSerie = (int)dr["NumeroSerie"]
                        };
                        historial.Add(historialActual);

                    }
                    connection.Close();
                    return Ok(historial);
                }
                else
                {
                    connection.Close();
                    return BadRequest("Dispositivo no tiene un historial asociado");
                }

            }
            catch
            {
                connection.Close();
                return BadRequest("Dispositivo no tiene un historial asociado");
            }
        }

        [HttpPost] //Route-> api/User/PasarDispositivo
        public async Task<IActionResult> PasarDispositivo([FromBody] PasarDispositivo disp)
        {

            connection.ConnectionString = server.init();
            string query = $"SELECT " +
                $"              \"correoPosedor\" " +
                $"          FROM \"Dispositivo\" " +
                $"          WHERE \"numeroSerie\" = {disp.NumeroSerie} AND \"correoPosedor\" = '{disp.PosedorActual}';";

            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            NpgsqlDataReader dr = command.ExecuteReader();
            

            if (dr.HasRows)
            {
                connection.Close();
                try
                {
                    query = $"UPDATE \"Dispositivo\" SET \"correoPosedor\" = '{disp.FuturoPosedor}' WHERE  \"numeroSerie\" = {disp.NumeroSerie}  AND \"correoPosedor\" = '{disp.PosedorActual}';";
                    connection.Open();
                    command = new NpgsqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    connection.Close();

                    return Ok();
                    
                }
                catch
                {
                    connection.Close();
                    return BadRequest($"El usuario {disp.PosedorActual} no puede pasar un dispositivo a un usuario no registrado: {disp.FuturoPosedor}");
                }
            }
            else
            {
                connection.Close();
                return BadRequest($"El usuario {disp.PosedorActual} no es el posedor actual del dispositivo {disp.NumeroSerie}");
            }


        }
    }
} 
    
    



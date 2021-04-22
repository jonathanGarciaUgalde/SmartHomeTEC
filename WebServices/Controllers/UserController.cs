using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

<<<<<<< HEAD
using WebServices.Models;
=======
>>>>>>> API
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
<<<<<<< HEAD

        /*
        * Método que se comunica mediante el protocolo http para validar si el usuario que inicia sesión está registrado.
        */
        [HttpGet("{Username}/{Password}")]
        public async Task<IActionResult> ValidUser(string Username, string Password)
        {
            return Ok();
        }
        /*
        * Método que se comunica mediante el protocolo http y retorna todos los usuarios registrados.
        */
        [HttpGet("All/Users")]
        public async Task<IActionResult> GetallAsync()
        {
            return Ok();
        }
        /*
        * Método que se comunica mediante el protocolo http  y  este retorna el usuario del id que se consulta 
        * 
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteAsync(int id)
        {
         
            return Ok();
        }
        /*
        * Método que se comunica mediante el protocolo http para insertar nuevos clientes en el app.
        */
        [HttpPost]
        public async Task<IActionResult> AddClienteAsync(User newUser)
        {
          
            return CreatedAtRoute("default", new { id = newUser.Correo}, newUser);
        }
      
        /*
        * Método que se comunica mediante el protocolo http para eliminar el usuario que se indica en la página web
        */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClienteAsync(int id)
        {
           
            return Ok("toDelete");
=======
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
>>>>>>> API
        }
    }
}

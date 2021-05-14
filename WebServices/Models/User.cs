using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class User
    {
        public string Correo { get; set; }//PK
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public Region Region { get; set; }
        public List<Direccion> Direccion{get;set;}
       





        
    }
}

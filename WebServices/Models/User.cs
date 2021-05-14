using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class User
    {
        public string correo { get; set; }//PK
        public string password { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public Region region { get; set; }
        public List<Direccion> direccion{get;set;}
       





        
    }
}

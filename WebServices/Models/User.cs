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
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Direccion { get; set; }
        public string Pais { get; set; }
        public string Continente { get; set; }
    



        /*
        public Apellidos Apellido{ get; set; }
        public Region Region { get; set; }
        public List<Direccion> Direcciones { get; set; }
       
   
   
    }

    public class Apellidos
    {
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
    }
    public class Region
    {
        public string Continente { get; set; }
        public string Pais { get; set; }


        */
    }
}

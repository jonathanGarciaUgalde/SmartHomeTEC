using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    // modelo  que convierte  las peticiones 
    //que se realicen con él por los metodos POST,
    //GET,PUT,DELETE, y generan estructuras de Factura 
    public class Factura
    { public string Consecutivo { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; } 
        public string CorreoComprador {get; set;}
        public DispositivoStock Producto { get; set; }




    }
}

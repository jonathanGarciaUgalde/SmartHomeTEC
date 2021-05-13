using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServices.Models;

namespace WebServices.Models
{
    public class Tienda
    {
        public string IdGestion { get; set; }
        public List<TipoAdmin>Distribuidor { get; set; }// se deben insertar  con un PK combinando Iddistribuidor+idProducto para id Gestion
        public List<DispositivoStock> Dispositivos { get; set; }//
        public int Cantidad { get; set; }


    }
}

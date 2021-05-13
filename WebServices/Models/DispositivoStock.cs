using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServices.Models;

namespace WebServices.Models
{
    public class DispositivoStock
    {
        public int NumeroSerie { get; set; }
        public string Marca { get; set; }
        public double ConsumoElectrico { get; set; }
        public int CedulaJuridica { get; set; }
        public bool EnVenta { get; set; }

      
        //tipo
      
        public string Tipo {get; set;}
        public string Descripcion {get; set;}
        public DateTime TiempoGarantia { get; set; }

    }

}

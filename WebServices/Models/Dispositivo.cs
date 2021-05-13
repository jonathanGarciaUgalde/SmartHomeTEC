using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServices.Models;

namespace WebServices.Models
{
    public class Dispositivo
    {
        public int NumeroSerie { get; set; }
        public string Marca { get; set; }
        public double ConsumoElectrico { get; set; }
  
        public Tipo Tipo { get; set; } // se le asigna al modelo de tipo la estructura 
    }
}

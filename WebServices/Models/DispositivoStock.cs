using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServices.Models;

namespace WebServices.Models
{
    public class DispositivoStock
    {
        public int numeroSerie { get; set; }
        public string marca { get; set; }
        public double consumoElectrico { get; set; }
        public int cedulaJuridica { get; set; }
        public bool enVenta { get; set; }

      
        //tipo
      
        public string tipo {get; set;}
        public string descripcion {get; set;}
        public int tiempoGarantia { get; set; }

    }
    public class ListaDispositivoStock { 
        public List<DispositivoStock> stocks { get; set; }
    
    }

}

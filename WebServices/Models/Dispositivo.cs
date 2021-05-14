using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class Dispositivo
    {

        public int numeroSerie { get; set; }
        public string marca { get; set; }
        public double consumo { get; set; }
        public bool estadoActivo { get; set; }
        public string correoPosedor { get; set; }
        public string nombreAposento { get; set; }// estos son atributos  que pueden ser  null  porque  en la tabla estan validados como ta
        // tipo
        public string tipo { get; set; }
        public int tiempoGarantia { get; set; } 
        public string descripcion { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class Dispositivo
    {

        public int NumeroSerie { get; set; }
        public string Marca { get; set; }
        public  double Consumo { get; set; }
        public bool EstadoActivo { get; set; }
        public string CorreoPosedor { get; set; }
        public string NombreAposento { get; set; }// estos son atributos  que pueden ser  null  porque  en la tabla estan validados como ta
        // tipo
        public string Tipo { get; set; }
        public DateTime TiempoGarantia { get; set; }

    }
}

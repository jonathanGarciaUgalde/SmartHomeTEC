using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class Historial
    {
        public bool EstadoActivo { get; set; }
        public string FechaActivacion { get; set; }
        public string FechaDesactivacion { get; set; }
        public string HoraActivacion { get; set; }
        public string HoraDesactivacion { get; set; }
        public int NumeroSerie { get; set; }
    }
}

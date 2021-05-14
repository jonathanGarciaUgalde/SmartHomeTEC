using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class Historial
    {
        public bool estadoActivo { get; set; }
        public string fechaActivacion { get; set; }
        public string fechaDesactivacion { get; set; }
        public string horaActivacion { get; set; }
        public string horaDesactivacion { get; set; }
        public int numeroSerie { get; set; }
    }
}

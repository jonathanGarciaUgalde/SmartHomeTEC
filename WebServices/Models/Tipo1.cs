using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class Tipo
    {
        public int CedulaJuridica { get; set; }
        public string Nombre { get; set; }
        public Region Region { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class CertificadoGarantia
    { 
        // modelo  que convierte  las peticiones 
        //que se realicen con él por los metodos POST,
        //GET,PUT,DELETE, y generan estructuras de Certificado de Garantía 
        public int Serie { get; set; }
        public string CorreoUsuario { get; set; }
        public string FechaLimite { get; set;}




    }
}

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
        public int TiempoGarantia { get; set; }//Estos son atributos  que pueden ser  null  porque  en la tabla estan validados como tal
        public double ConsumoElectrico { get; set; }
        public bool EstadoActivo { get; set; }
        public string NombreAposento { get; set; }

        public string Tipo { get; set; } //Se le asigna al modelo de tipo la estructura 
        public string CorreoPosedor { get; set; }
        public string Descripcion { get; set; } //Estos son atributos  que pueden ser  null  porque  en la tabla estan validados como tal
    }
}

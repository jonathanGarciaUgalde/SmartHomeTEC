using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class DispositivoManual
    {

        public int Numero_serie { get; set; }
        public string Marca { get; set; }
        public string Consumo { get; set; }
        public string Estado { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }// estos son atributos  que pueden ser  null  porque  en la tabla estan validados como tal
        public string FechaLimiteGarantia { get; set; }// estos son atributos  que pueden ser  null  porque  en la tabla estan validados como tal
        public Tipo Tipo { get; set; } // se le asigna al modelo de tipo la estructura 


    }
}

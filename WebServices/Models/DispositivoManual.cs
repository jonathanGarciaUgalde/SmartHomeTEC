using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public class DispositivoManual
    {

        public int NumeroSerie { get; set; }
        public string Marca { get; set; }
        public  int Consumo { get; set; }
        public bool EstadoActivo { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }// estos son atributos  que pueden ser  null  porque  en la tabla estan validados como ta

        public string Aposento { get; set; }// estos son atributos  que pueden ser  null  porque  en la tabla estan validados como ta





        public Tipo Tipo { get; set; } // se le asigna al modelo de tipo la estructura 


    }
}

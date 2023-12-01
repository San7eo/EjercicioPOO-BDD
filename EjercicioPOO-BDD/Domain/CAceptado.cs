using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioPOO_BDD.Domain
{
    public class CAceptado
    {   
        public int IdAceptado { get; set; }
        public DateTime FechaInforme {  get; set; }

        public string CodigoVendedor { get; set; } = string.Empty;

        public double Venta {  get; set; }

        public bool TamañoEmpresa { get; set; }





    }
}

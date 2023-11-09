using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CuadroPondercion
{
    public class CAreaGrafico
    {
        public List<string> espacios { get; set; }
        public List<int> rangos { get; set; }
        public Color colorArea;
        public double anguloInicio;
        public double anguloFin { get; set; }
        public int tipoArea; // 0 - Social, 1 - Semi-Social, 2 - Servicio, 3 - Privado
        public CAreaGrafico(double anguloInicio, int tipoArea)
        {
            this.anguloInicio = anguloInicio;
            this.tipoArea = tipoArea;
            espacios = new List<string>();
            rangos = new List<int>();
        }

    }
}

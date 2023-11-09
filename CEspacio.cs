using System.Drawing;

namespace CuadroPondercion
{
    public class CEspacio
    {
        public string nombre;
        public Color color { get; set; }
        public string area; // 0-Verde Social;  1-Naranja SemiSocial; 2-Amarillo Servicio, 3-Rojo Privada
        public int rango { get; set; }

        public CEspacio(string area, string nombre)
        {
            this.area = area;
            this.nombre = nombre;
            
        }
    }
}

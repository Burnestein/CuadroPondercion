using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CuadroPondercion
{
    public class CColor
    {
        //---------------------------------------------------------------------
        //atributos
        //---------------------------------------------------------------------
        private int Red;
        private int Green;
        private int Blue;

        //---------------------------------------------------------------------
        //constructor
        //---------------------------------------------------------------------
        public CColor()
        {

            Red = 0;
            Green = 0;
            Blue = 0;

        }
        //---------------------------------------------------------------------
        //constructor
        //---------------------------------------------------------------------
        public CColor(int Red, int Green, int Blue)
        {

            this.Red = Red;
            this.Green = Green;
            this.Blue = Blue;

        }
        //---------------------------------------------------------------------
        //constructor
        //---------------------------------------------------------------------
        public CColor(string NombreColor)
        {
            Color ColorInterno;

            ColorInterno = Color.FromName(NombreColor);

            Red = ColorInterno.R;
            Green = ColorInterno.G;
            Blue = ColorInterno.B;


        }


        //---------------------------------------------------------------------
        //Obtiene el nombre del color.
        //---------------------------------------------------------------------
        public string GetNombreColor()
        {
            Color colorInterno;


            colorInterno = Color.FromArgb(Red, Green, Blue);

            return colorInterno.Name;
        }
        //---------------------------------------------------------------------
        //Obtiene el color primitivo
        //---------------------------------------------------------------------
        public Color GetColorPrimitivo()
        {
            Color ColorInterno;

            ColorInterno = Color.FromArgb(Red, Green, Blue);

            return ColorInterno;

        }
    }
}

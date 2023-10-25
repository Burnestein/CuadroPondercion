using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CuadroPondercion
{
    internal class CFigura
    {
        //---------------------------------------------------------------------
        //Atributos
        //---------------------------------------------------------------------
        protected Pen Lapiz;
        protected int X;
        protected int Y;

        //---------------------------------------------------------------------
        //constructor
        //---------------------------------------------------------------------
        public CFigura(int X, int Y, Color Color)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}

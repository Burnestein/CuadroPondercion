using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CuadroPondercion
{
    internal class CRombo : CFigura
    {
        //---------------------------------------------------------------------
        //atributos
        //---------------------------------------------------------------------
        private Point[] Puntos;

        //---------------------------------------------------------------------
        //constructor
        //---------------------------------------------------------------------
        public CRombo(int s, int t, int u, int v, int w, int x, int y, int z, Color Color) : base(0, 0, Color)
        {

            Puntos = new Point[]
            {
                new Point { X = s, Y = t},
                new Point { X = u, Y = v},
                new Point { X = w, Y = x},
                new Point { X = y, Y = z},
            };
            Lapiz = new Pen(Color, 1);
        }

        //---------------------------------------------------------------------
        //Dibuja el poligono
        //---------------------------------------------------------------------
        public void Dibuja(Graphics AreaDibujo)
        {
            AreaDibujo.DrawPolygon(Lapiz, Puntos);
        }
    }
}

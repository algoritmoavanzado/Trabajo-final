using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace ProbandoMap
{
    public class Marcador
    {
        public int x, y;
        public Color color;
        public Marcador(Color color)
        {
            x = y = -10;
            this.color = color;
        }
        public void Dibujar(System.Drawing.Graphics gr)
        {
            //gr.FillEllipse(new SolidBrush(color), x - 5 / 2, y - 5 / 2, 10, 10);
            string p = Directory.GetCurrentDirectory();
            Image i = Image.FromFile(p + @"\marker.png");
            gr.DrawImage(i,x-i.Width/2,y-i.Height);
        }
    }

}

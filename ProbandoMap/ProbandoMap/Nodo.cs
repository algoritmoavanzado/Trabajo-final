using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace ProbandoMap
{
    public class Nodo
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public int indice { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public List<Arista>  conexiones { get; set; }
        public ParaderoButton  btn { get; set; }
        public List<int> rutasQuePasanPorEsteParadero;

        public Nodo(int x,int y,double lat,double lng,int pIndice,System.Windows.Forms.Form c)
        {
            Latitud = lat;
            Longitud = lng;
            indice = pIndice;
             conexiones = new List<Arista>();
             btn = new ParaderoButton(x, y, indice);
             btn.Parent = c;

             this.x = x;
             this.y = y;
        }

        public void initConexiones()
        {
             conexiones = new List<Arista>();
        }

        public void addConexion(int indiceDestino,double pPeso){
             conexiones.Add(new Arista {indexDestino=indiceDestino, peso = pPeso });
        }

        public void updateXY(int X,int Y){
            x = X; y = Y;
             btn.x = X;
             btn.y = Y;
        }

        public void Dibujar(System.Drawing.Graphics gr,System.Windows.Forms.Form c)
        {
            //gr.FillEllipse(new SolidBrush(color), x - 5 / 2, y - 5 / 2, 10, 10);
            if ( btn == null)
            {
                 btn = new ParaderoButton(x, y, indice);
                 btn.Parent = c;
            }

            //string p = Directory.GetCurrentDirectory();
            //Image i = Image.FromFile(p + @"\marker.png");
            //gr.DrawImage(i, x - i.Width / 2, y - i.Height);
        }
        
    }

    

}

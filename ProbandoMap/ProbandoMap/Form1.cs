using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ProbandoMap
{
    public partial class Form1 : Form
    {
        

      //DATOS POR DEFAULT
        public int AnchoOriginal,AltoOriginal;
        public double LatitudSup, LongitudSup, LatitudInf, LongitudInf;
        public int Wreal, Hreal, margenOficialW, margenOficialH;
        public int zoom;        
        //PILA PARA EL ZOOM VOLVER AL ESTADO INICIAL
        public Stack<Stack<int>> movimientos;
        public Stack<int> auxMov;
        //MOVER EL MAPA
        public int XReal, YReal;
        //ORDENADAS DEL PANEL CON RESPECTO A LA IMAGEN MAPEADA 
        public int XPanel, YPanel;
        //MARCADOR
        
        

        public List<List<Tuple<double, double>>> matrizCoord;


        private Controlador controlador;

        public Form1()
        {
            InitializeComponent();

            //LATITUDES QUE ME DARAN
            LatitudSup = -12.0543677233623 ; //Y
            LongitudSup = -77.0491576194763 - 2 * 0.00562018152180599 - (0.00562018152180599/2); //X
            //-0.00562018152180599
            LatitudInf = -12.1173196930678; //Y
            LongitudInf = -77.1974945068359; //x

            //ZOOM
            AnchoOriginal = pictureBox1.Width;
            AltoOriginal = pictureBox1.Height;
            
            zoom = 0;            
            Wreal = (int)panel2.Width;
            Hreal = (int)panel2.Height;
            margenOficialW = Wreal;
            margenOficialH = Hreal;
                        
            //MOVER EL MAPA
            XReal = 0;
            YReal = 0;
            XPanel = YPanel = 0;
            //CONTROLAR ALEJAR - ar=1,ab=2,izq=3,der=4           
            movimientos = new Stack<Stack<int>>();
            matrizCoord = new List<List<Tuple<double, double>>>();
            auxMov = new Stack<int>();
            //----------------------

            //MessageBox.Show((-77.041476 - (-77.0358558184782))+"");
            controlador = new Controlador();
            Actualizar();
            
            //MARCADOR
            
            

           

        }

        public void CargarData()
        {
            TextReader tr = new StreamReader("datos.txt");

            int NroNodos=Convert.ToInt32(tr.ReadLine());

            for (int i = 0; i < controlador.fil; i++)
            {
                List<Tuple<double, double>> aux = new List<Tuple<double, double>>();
                Tuple<double, double> nuevo;
                for (int j = 0; j < controlador.col; j++)
                {
                    String coordenada = tr.ReadLine();
                    string[] coordenadas = coordenada.Split(',');
                    //LATITUTD Y LONG
                    nuevo = new Tuple<double, double>(Convert.ToDouble(coordenadas[0]), Convert.ToDouble(coordenadas[1]));
                    aux.Add(nuevo);
                }
                matrizCoord.Add(aux);
            }
            //CONVERTIR A GRAFO YA TENEMOS LOS NODOS


            //YA CARGAMOS TODA LA MATRIZ DE COORDENADAS
            int nroRutas = Convert.ToInt32(tr.ReadLine());
            String linea = tr.ReadLine();
            string[] par = linea.Split(',');
            int cantNodosPorRuta = Int32.Parse(par[1]);
            List<int> Ruta = new List<int>();
            for (int i = 0; i < cantNodosPorRuta; i++)
            {
                Ruta.Add(Int32.Parse(tr.ReadLine()));
            }

            controlador.Path = Ruta;
            
            
            tr.Close();

        }

        public void Actualizar()
        {
            controlador.Wreal = this.Wreal;
            controlador.Hreal = this.Hreal;
            controlador.XReal = this.XReal;
            controlador.YReal = this.YReal;
            controlador.XPanel = this.XPanel;
            controlador.YPanel = this.YPanel;
            controlador.zoom = this.zoom;
            controlador.AnchoOriginal = this.AnchoOriginal;
            controlador.AltoOriginal = this.AltoOriginal;
            controlador.LatitudSup = this.LatitudSup;
            controlador.LongitudSup = this.LongitudSup;
            controlador.LatitudInf = this.LatitudInf;
            controlador.LongitudInf = this.LongitudInf;

            controlador.RecalcularXY();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            System.Drawing.Graphics gr = panel2.CreateGraphics();
            BufferedGraphicsContext espaciobuffer = BufferedGraphicsManager.Current;
            BufferedGraphics buffer = espaciobuffer.Allocate(gr,new Rectangle(0,0,Wreal,Hreal));
            buffer.Graphics.Clear(Color.White);
            buffer.Graphics.DrawImage(pictureBox1.Image,XReal,YReal,Wreal,Hreal);

            //controlador.DibujarMatriz(buffer.Graphics,XReal,YReal);
            //controlador.DibujarPath(buffer.Graphics);
            //controlador.DibujarParaderos(buffer.Graphics);

            controlador.dibujaLinea(buffer.Graphics);

             buffer.Render(gr);
             gr.Dispose();
             espaciobuffer.Dispose();
             buffer.Dispose();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            //controlador.agregarParadero(
        }

        private bool verificarMargenes()
        {
            
            if (margenOficialW < Wreal || margenOficialH < Hreal) //SI ESTA EN ZOOM PUEDE MOVERSE
            {
               return true;               
            }
            
                return false;
        }

        private void btnAcercar_Click(object sender, EventArgs e)
        {
            if (zoom == 25 * 70) //5 veces de zoom
                return;
            
                Wreal += 70;
                Hreal += 70;
                zoom += 70;
                Actualizar();
                movimientos.Push(auxMov);

                //XPanel += 70;
                //YPanel += 70;
                //controlador.GenerarPuntos(Wreal, Hreal);
            
        }

        private void arreglarBajar()
        {
            YReal -= 70;
            YPanel += 70;
            Actualizar();
        }
        private void arreglarSubir()
        {
            YReal += 70;
            YPanel -= 70;
            Actualizar();
        }
        private void arreglarDerecha()
        {
            XReal -= 70;
            XPanel += 70;
            Actualizar();
        }
        private void arreglarIzquierda()
        {
            XReal += 70;
            XPanel -= 70;
            Actualizar();
        }

        private void btnAlejar_Click(object sender, EventArgs e)
        {
            //AL ALEJAR VOLVER AL ULTIMO MOVIMIENTO PARA ANTES DE ALEJARLO TAMBIEN ACOMODAR LOS EJES XD

            if (!(margenOficialH >= Hreal && margenOficialW >= Wreal))
            {
                Wreal -= 70;
                Hreal -= 70;
                zoom -= 70;
                Actualizar();
                //controlador.GenerarPuntos(Wreal, Hreal);

                //XPanel -= 70;
                //YPanel -= 70;
                

                if (movimientos.Count > 0)
                {
                    Stack<int> ultimoauxMov = movimientos.Pop();
                    
                    while (ultimoauxMov.Count > 0)
                    {
                        int ultimoaux=ultimoauxMov.Pop();
                        switch (ultimoaux)
                        {
                            case 1: arreglarBajar(); break; //si es que habia subido lo bajo pe
                            case 2: arreglarSubir(); break; //si es que habia bajado lo subo pe
                            case 3: arreglarDerecha(); break; //si es que se fue a la izq lo mando a la derecha
                            case 4: arreglarIzquierda(); break; //si es que se fue a la der lo mando a la izq
                        }
                    }                
                
                }

            }
        }

        private void btnArriba_Click(object sender, EventArgs e)
        {
            if (!verificarMargenes())
                return;

            if (YPanel - 70 < 0)
                return;


            YReal += 70;
            YPanel -= 70;
            Actualizar();

            auxMov.Push(1); //ar
        }

        private void btnAbajo_Click(object sender, EventArgs e)
        {
            if (!verificarMargenes())
                return;
            if (YPanel + margenOficialH + 70 > Hreal)
                return;

            YReal -=70;
            YPanel += 70;
            Actualizar();
            auxMov.Push(2); //ab
        }

        private void btnIzquierda_Click(object sender, EventArgs e)
        {
            if (!verificarMargenes())
                return;
            if (XPanel - 70 < 0)
                return;

            XReal += 70;
            XPanel -= 70;
            Actualizar();
            auxMov.Push(3); //izq
        }

        private void btnDerecha_Click(object sender, EventArgs e)
        {
            if (!verificarMargenes())
                return;
            if (XPanel + margenOficialW + 70 > Wreal)
                return;

            XReal -= 70;
            XPanel += 70;
            Actualizar();
            auxMov.Push(4); //der
        }

        public Tuple<double, double> obtenerLatitudLong(int eX, int eY)
        {
            double proporcionW = (double)Wreal / (double)AnchoOriginal;
            double proporcionH = (double)Hreal / (double)AltoOriginal;

            double XOriginal = (double)(eX + XPanel) / proporcionW;
            double YOriginal = (double)(eY + YPanel) / proporcionH;

            double altoLatitudes = Math.Abs(LatitudInf - LatitudSup);
            double anchoLongitudes = Math.Abs(LongitudInf - LongitudSup);

            double operadorLongPanel = (XPanel * anchoLongitudes) / AnchoOriginal;
            double operadorLatPanel = (YPanel * altoLatitudes) / AltoOriginal;

            double operadorLong = (XOriginal * anchoLongitudes) / AnchoOriginal;
            double operadorLat = (YOriginal * altoLatitudes) / AltoOriginal;

            Tuple<double, double> par = new Tuple<double, double>(LatitudSup-operadorLat,LongitudSup+operadorLong);//(LATITUD,LONGITUD)

            return par;
       } 

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            
            //Tuple<double, double> coord = obtenerLatitudLong(e.X, e.Y);

            //txtLat.Text = coord.Item1 + "";
            //txtLong.Text = coord.Item2 + "";
            ////MessageBox.Show("LatFinal = " + coord.Item1+"\nLongFinal = "+coord.Item2);

            //Tuple<int, int> clickeado = new Tuple<int, int>(e.X, e.Y);
            //if (rbNodoOrigen.Checked)
            //{
            //    controlador.setearPunto(0, e.X,e.Y);           
            //}
            //else if (rbNodoDestino.Checked)
            //{
            //    controlador.setearPunto(1, e.X,e.Y);
            //}




            //controlador.LimpiarPath();

            controlador.agregarParadero(e.X, e.Y,this,this.panel2);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //controlador.GenerarPuntos((int)this.panel2.Width+25*70, (int)this.panel2.Height+25*70);
            //CargarData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Tuple<List<int>,int> result=controlador.Dijkstra(nodoOrigen, nodoDestino);
           
            //String resultado = "Pasos:\n";

            //for (int i = 0; i < result.Item1.Count; i++)
            //{
            //    resultado += result.Item1[i] + ",";
            //}
            //resultado += "\nDistancia corta: " + result.Item2;
            controlador.hallaCamino();

            //MessageBox.Show(resultado);

        }

        private void rbNodoOrigen_CheckedChanged(object sender, EventArgs e)
        {
            controlador.LimpiarPath();
        }

        private void rbNodoDestino_CheckedChanged(object sender, EventArgs e)
        {
            controlador.LimpiarPath();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            TextWriter tw = new StreamWriter("datos.txt");

            // write a line of text to the file
            List<List<Tuple<int, int, double, double>>> matriz = controlador.matriz;

            tw.WriteLine(matriz.Count * matriz[0].Count);
            for (int i = 0; i < matriz.Count; i++)
            {

                for (int j = 0; j < matriz[i].Count; j++)
                {
                    tw.WriteLine(matriz[i][j].Item3 + "," + matriz[i][j].Item4);
                }
            }
            //ACA YA LLENE TODAS LAS LONGITUDES
            tw.WriteLine(1+""); //NUMERO DE RUTAS , RUTA1
            tw.WriteLine(1 + "," + 40);

            int ruta1 = 5;
            tw.WriteLine(5);
            
            for (int i = 1; i <= 19; i++)
                tw.WriteLine(ruta1 + controlador.col * i);

            for(int j=1;j<=20;j++)
            tw.WriteLine(ruta1 + controlador.col * 19 + j);
            
            

            

            //tw.WriteLine(DateTime.Now);

            // close the stream
            tw.Close();


        }

        //Click en Trazar ruta
        private void button3_Click(object sender, EventArgs e)
        {

        }

        //Click en agregar ruta
        private void button4_Click(object sender, EventArgs e)
        {
            controlador.LimpiarPath();
        }





    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;
using Newtonsoft.Json;
using Sinbadsoft.Lib.Collections;
using System.Collections;
using System.Windows.Forms;

namespace ProbandoMap
{
    
    
    public class Controlador
    {


        public List<List<Nodo>> myGrafo;
        
        public List<List<Tuple<int,int,double,double>>> matriz;
        public int CantZooms;
        public int Wreal, Hreal, XReal, YReal, XPanel, YPanel, zoom, AnchoOriginal, AltoOriginal,valor,col,fil;
        public double LatitudSup, LongitudSup, LatitudInf, LongitudInf;
        private int INFINITO;
        public List<int> Path;
        public Marcador marcadorOrigen, marcadorDestino;
        public int nodoOrigen, nodoDestino;
        public const int KILOMETROS_X_HORA_DRIVING = 50;

        public List<Nodo> paraderos;
        public int contador;
        public bool modoAgregarRuta = false;
        public int previousIndice = -1;

        public int rutaActual = 0;

        public Controlador()
        {            
            CantZooms = 25;
            INFINITO = 100000000;
            Path = new List<int>();
            contador = 0;
            marcadorOrigen = new Marcador(Color.Green);
            marcadorDestino = new Marcador(Color.Blue);
            nodoOrigen = nodoDestino = -1;
            paraderos = new List<Nodo>();
        }

        public void RecalcularXY( ){
            //if (matriz == null) return;
            //Recalcula puntos para matriz
            //for (int i = 0; i < matriz.Count; i++)
            //{
            //    for (int j = 0; j < matriz[i].Count; j++)
            //    {
            //         Tuple<int,int,double,double> actual=matriz[i][j],nuevo;
            //         Tuple<double, double> nuevosXY = obtenerXY(actual.Item3, actual.Item4);
            //         nuevo = new Tuple<int, int, double, double>((int)nuevosXY.Item1, (int)nuevosXY.Item2, actual.Item3, actual.Item4);
            //        matriz[i][j] = nuevo;
            //    }
            //}
            //Recalcula puntos para el marcador inicial
            Tuple<double, double> nuevoXY;
            nuevoXY=obtenerXY(marcadorOrigen.lat, marcadorOrigen.lng);
            marcadorOrigen.x = (int)nuevoXY.Item1; marcadorOrigen.y = (int) nuevoXY.Item2;

            nuevoXY = obtenerXY(marcadorDestino.lat, marcadorDestino.lng);
            marcadorDestino.x = (int)nuevoXY.Item1; marcadorDestino.y = (int)nuevoXY.Item2;

            for (int i = 0; i < paraderos.Count; i++)
            {
                Nodo tempParadero = paraderos[i];
                Tuple<double, double> nuevos_xy;
                nuevos_xy = obtenerXY(tempParadero.Latitud, tempParadero.Longitud);
                tempParadero.x = (int)nuevos_xy.Item1; tempParadero.y = (int)nuevos_xy.Item2;
                tempParadero.btn.rePosition(nuevos_xy.Item1, nuevos_xy.Item2);
            }
            if(paraderos.Count>0)
                Application.DoEvents();

        }

        public void setearPunto(int a,int eX,int eY)
        {
            Tuple<double, double> coord = obtenerLatitudLong(eX, eY);
            if (a == 0) //Setear origen
            {

                marcadorOrigen.x = eX;
                marcadorOrigen.y = eY;
                marcadorOrigen.lat = coord.Item1; marcadorOrigen.lng = coord.Item2;
                //nodoOrigen = clickeado.Item4 * col + clickeado.Item3; //X = j , Y=i


            }
            else //Setear destino
            {
                marcadorDestino.x = eX;
                marcadorDestino.y = eY;
                marcadorDestino.lat = coord.Item1; marcadorDestino.lng = coord.Item2;
                //nodoDestino = clickeado.Item4 * col + clickeado.Item3;
            }

        }



        public Tuple<double, double> obtenerXY(double lat, double lng)//Realiza el proceso inverso a obtenerLatLong
        {
            double proporcionW = (double)Wreal / (double)AnchoOriginal;
            double proporcionH = (double)Hreal / (double)AltoOriginal;

            double altoLatitudes = Math.Abs(LatitudInf - LatitudSup);
            double anchoLongitudes = Math.Abs(LongitudInf - LongitudSup);

            double operadorLong=lng-LongitudSup;
            double operadorLat = LatitudSup - lat;

            double XOriginal = operadorLong * AnchoOriginal / anchoLongitudes;
            double YOriginal = operadorLat * AltoOriginal / altoLatitudes;

            double X = XOriginal * proporcionW - XPanel;
            double Y = YOriginal * proporcionH - YPanel;

            Tuple<double, double> par = new Tuple<double, double>(X,Y);
            return par;

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

            Tuple<double, double> par = new Tuple<double, double>(LatitudSup - operadorLat, LongitudSup + operadorLong);//(LATITUD,LONGITUD)

            return par;
        }

        public Tuple<int,int,int,int> ObtenerIndiceAprox (int eX,int eY)
        {
            int r = eX / valor;
            int datoX = valor * r;
            int aproxX = eX - datoX;
            int respuestaX;
            if (aproxX > valor / 2)
            {
                respuestaX = valor * (r + 1);
            }
            else
            {
                respuestaX = valor * r;
            }
            int r2 = eY / valor;
            int datoY = valor * r2;
            int aproxY = eY - datoY;
            int respuestaY;
            if (aproxY > valor / 2)
            {
                respuestaY = valor * (r2 + 1);
            }
            else
            {
                respuestaY = valor * r2;
            }

            Tuple<int, int,int,int> aux = new Tuple<int, int,int,int>(respuestaX, respuestaY,(respuestaX/valor)-1,(respuestaY/valor)-1);
            return aux;

        }

        public void GenerarPuntos(int Wreal, int Hreal)
        {
            valor = 20;
            int xAcumular = 0, yAcumular = 0, x = valor, y = valor;
            col = Wreal / valor - 1;
            fil = Hreal / valor - 1;

            matriz = new List<List<Tuple<int, int, double, double>>>();//MATRIZ DE X,Y PROBAR
            List<Tuple<int, int, double, double>> aux; //TODA UNA FILA
            for (int i = 0; i < fil; i++)
            {
                aux = new List<Tuple<int, int, double, double>>();
                Tuple<int, int, double, double> nuevo;
                for (int j = 0; j < col; j++)
                {
                    Tuple<double, double> coord = obtenerLatitudLong(x + xAcumular, y + yAcumular);
                    nuevo = new Tuple<int, int, double, double>(x + xAcumular, y + yAcumular, coord.Item1, coord.Item2);
                    aux.Add(nuevo);
                    xAcumular += valor;
                }
                matriz.Add(aux); //AGREGO TODA LA FILA
                xAcumular = 0;
                yAcumular += valor;
                nuevo = null;
            }



            aux = null;
            //VolverGrafoSinPeso(fil, col);
            //PonerlePesosMaps(fil, col);

        }

        //public void VolverGrafoSinPeso(int fil,int col)
        //{
            

        //    myGrafo = new List<List<Nodo>>(); 
		
        //    List<Nodo> aux;
        //    for (int i = 0; i < fil; i++)
        //    {
                
        //        Nodo nuevo,nuevo2,nuevo3,nuevo4;
        //        for (int j = 0; j < col; j++)
        //        {
        //            aux = new List<Nodo>();
        //            nuevo=new Nodo();
        //            nuevo2 = new Nodo();
        //            nuevo3 = new Nodo();
        //            nuevo4 = new Nodo();
        //            nuevo.Peso=0; 
        //            if(i==0 && j==0)
        //            {
        //               nuevo.indiceNodoDestino=i*col+(j+1);
        //                aux.Add(nuevo);//AÑADO EL DE LA DERECHA
        //                nuevo2.indiceNodoDestino=(i+1)*col+(j);
        //                aux.Add(nuevo2); //AÑADO EL DE ABAJO               
        //            }
        //            else if(i==0 && j==col-1)
        //            {
        //                nuevo.indiceNodoDestino=i*col+(j-1);
        //                aux.Add(nuevo);//AÑADO EL DE LA IZQUIERDA
        //                nuevo2.indiceNodoDestino=(i+1)*col+(j);
        //                aux.Add(nuevo2); //AÑADO EL DE ABAJO 
        //            }
        //            else if(i==fil-1 && j==0)
        //            {
        //                nuevo.indiceNodoDestino=i*col+(j+1);
        //                aux.Add(nuevo);//AÑADO EL DE LA IZQUIERDA
        //                nuevo2.indiceNodoDestino=(i-1)*col+(j);
        //                aux.Add(nuevo2); //AÑADO EL DE ARRIBA
        //            }
        //            else if(i==fil-1 && j==col-1)
        //            {
        //                 nuevo.indiceNodoDestino=i*col+(j-1);
        //                aux.Add(nuevo);//AÑADO EL DE LA DERECHA
        //                nuevo2.indiceNodoDestino=(i-1)*col+(j);
        //                aux.Add(nuevo2); //AÑADO EL DE ARRIBA 
        //            }
        //            else if(i==0) //SUPERIOR
        //            {
        //                nuevo.indiceNodoDestino=(i+1)*col+(j); 
        //                aux.Add(nuevo); //ABAJO
        //                nuevo2.indiceNodoDestino=(i)*col+(j-1);
        //                aux.Add(nuevo2); //IZQUIERDA
        //                nuevo3.indiceNodoDestino=(i)*col+(j+1);
        //                aux.Add(nuevo3); //DERECHA
        //            }
        //            else if(i==fil-1) //INFERIOR
        //            {
        //                nuevo.indiceNodoDestino=(i-1)*col+(j); 
        //                aux.Add(nuevo); //ARRIBA
        //                nuevo2.indiceNodoDestino=(i)*col+(j-1);
        //                aux.Add(nuevo2); //IZQUIERDA
        //                nuevo3.indiceNodoDestino=(i)*col+(j+1);
        //                aux.Add(nuevo3); //DERECHA
        //            }
        //            else if(j==0) //BORDE IZQUIERDO
        //            {
        //                nuevo.indiceNodoDestino=(i-1)*col+(j);                 
        //                aux.Add(nuevo); //ARRIBA
        //                nuevo2.indiceNodoDestino=(i)*col+(j+1);
        //                aux.Add(nuevo2); //DERECHA
        //                nuevo3.indiceNodoDestino=(i+1)*col+(j); 
        //                aux.Add(nuevo3); //ABAJO
        //            }
        //            else if(j==col-1) //BORDE DERECHO
        //            {
        //                nuevo.indiceNodoDestino=(i-1)*col+(j);                 
        //                aux.Add(nuevo); //ARRIBA
        //                nuevo2.indiceNodoDestino=(i)*col+(j-1);
        //                aux.Add(nuevo2); //IZQUIERDA
        //                nuevo3.indiceNodoDestino=(i+1)*col+(j); 
        //                aux.Add(nuevo3); //ABAJO
        //            }
        //            else //TODO LO DE ADENTRO
        //            {
        //                nuevo.indiceNodoDestino=(i-1)*col+(j);                 
        //                aux.Add(nuevo); //ARRIBA                
        //                nuevo2.indiceNodoDestino=(i+1)*col+(j); 
        //                aux.Add(nuevo2); //ABAJO
        //                nuevo3.indiceNodoDestino=(i)*col+(j-1);
        //                aux.Add(nuevo3); //IZQUIERDA
        //                nuevo4.indiceNodoDestino=(i)*col+(j+1);
        //                aux.Add(nuevo4); //DERECHA
        //            }

        //            myGrafo.Add(aux);
        //        }
                               
        //    }

        //    //int x = 3;
        //}

        //public void PonerlePesosMaps(int fil, int col)
        //{
        //    System.Random r=new System.Random((int)DateTime.Now.Ticks);
        //    //WebClient client;
           
        //    for (int i = 0; i < myGrafo.Count; i++)
        //    {
        //        for (int j = 0; j < myGrafo[i].Count; j++)
        //        {
        //            //client = new WebClient();
        //            int jInicio,iInicio,jFinal,iFinal;
        //            jInicio = i % col;
        //            iInicio = i / col;
        //            jFinal=myGrafo[i][j].indiceNodoDestino%col;
        //            iFinal=myGrafo[i][j].indiceNodoDestino/col;


        //            var inicio = matriz[iInicio][jInicio].Item3 + "," + matriz[iInicio][jInicio].Item4;
        //            var destino = matriz[iFinal][jFinal].Item3 + "," + matriz[iFinal][jFinal].Item4;


        //            myGrafo[i][j].Peso=r.Next(100,601);

        //        }
        //    }
        //    //int delex = 0;
        //}

        
        public void DibujarMatriz(System.Drawing.Graphics gr,int XReal,int YReal)
        {
            SolidBrush brocha = new SolidBrush(Color.Red);
            for (int i = 0; i < matriz.Count; i++)
                for (int j = 0; j < matriz[i].Count; j++)
                {
                    int x = matriz[i][j].Item1, y = matriz[i][j].Item2;
                    Tuple<double, double> nuevosXY = obtenerXY(matriz[i][j].Item3, matriz[i][j].Item4);
                    if(nuevosXY.Item1>=0 && nuevosXY.Item2>=0)
                        //gr.FillEllipse(brocha, matriz[i][j].Item1 + XReal, matriz[i][j].Item2 + YReal, 3, 3);
                        gr.FillEllipse(brocha,(float) nuevosXY.Item1, (float) nuevosXY.Item2, 3, 3);
                }

            marcadorOrigen.Dibujar(gr);
            marcadorDestino.Dibujar(gr);
            brocha.Dispose();
        }

        public void hallaCamino()
        {
            Dijkstra(nodoOrigen, nodoDestino);
        }

        public void Dijkstra(int pNodoOrigen,int pNodoDestino)
        {
            if (nodoDestino == -1 || nodoOrigen == -1)
                return;

            Path = new List<int>();  
       
	        List<int> ComollegoA=new List<int>();

            MinHeap ColaPendiente = new MinHeap();
    

            List<double> Distancias=new List<double>();

            for(int i=0;i<myGrafo.Count;i++)
                ComollegoA.Add(-1);

            for (int i = 0; i < myGrafo.Count; i++)
                Distancias.Add(INFINITO);
    
            Distancias[pNodoOrigen]=0;
            Arista a = new Arista(); a.indexDestino = pNodoDestino; /*a.indexOrigen = nodoOrigen;*/ a.peso = Distancias[pNodoOrigen];
            ColaPendiente.Add(new Arista {peso=Distancias[pNodoOrigen], indexDestino=pNodoDestino }); //Mete el primero el cual se le asigna una distancia 0

            while(ColaPendiente.Count>0)
            {
                Arista tempNodo=ColaPendiente.RemoveMin();
               //POP();

                if (pNodoDestino == tempNodo.indexDestino) break; //si es que nodo temp que estoy recorriendo llega a ser igual que el nodo destino significa que lo encontro(minimamente)
                for (int i = 0; i < myGrafo[tempNodo.indexDestino].Count; i++)
                {
                     Arista auxNodo=paraderos[tempNodo.indexDestino].conexiones[i];
                     //PARTTE DEL RELAX - auxNodo.first ES EL PEDACITO
                     if ((Distancias[tempNodo.indexDestino] + auxNodo.peso) < Distancias[auxNodo.indexDestino])
                     {
                         Distancias[auxNodo.indexDestino] = Distancias[tempNodo.indexDestino] + auxNodo.peso;
                         ColaPendiente.Add(new Arista{peso=Distancias[auxNodo.indexDestino], indexDestino=auxNodo.indexDestino}); //LO AGREGO A LA COLITA DE PEND (NO SE QUEDARA VACIO)
                         ComollegoA[auxNodo.indexDestino] = tempNodo.indexDestino; //el tempNodo es el que reviso todos sus aristas tonce por ese medio llefo a aUxNODO
                     }
                }
            }

            double DistanciaFinal=Distancias[pNodoDestino];
            List<int> Pasos = new List<int>();
            Pasos.Add(pNodoDestino);
            while(ComollegoA[pNodoDestino]!=-1) //paso del vector de como llego a a un veector de pasos y lo revierto para tener el camino
            {
                Pasos.Add(ComollegoA[pNodoDestino]);
                pNodoDestino=ComollegoA[pNodoDestino];
            }

            Pasos.Reverse();
            //reverse(Pasos.begin(),Pasos.end());
            //Tuple<List<int>, int> par = new Tuple<List<int>, int>(Pasos,DistanciaFinal);

            //return par;
            Path = Pasos;

        }

        public void LimpiarPath()
        {
            Path = new List<int>();
        }

        public void DibujarPath(System.Drawing.Graphics gr)
        {
            if (Path.Count < 2)
                return;
            for (int k = 0; k < Path.Count - 1;k++ )
            {
                int i, j, i2, j2;
                j = Path[k] % col;
                i = Path[k] / col;

                j2 = Path[k+1] % col;
                i2 = Path[k+1] / col;

                gr.DrawLine(new Pen(Color.Blue,2),(float)matriz[i][j].Item1,(float)matriz[i][j].Item2,(float)matriz[i2][j2].Item1,(float)matriz[i2][j2].Item2);
                                
            }
        }

        public void agregarParadero(int X, int Y,System.Windows.Forms.Form c,System.Windows.Forms.Panel p)
        {
            Tuple<double,double> coord= obtenerLatitudLong(X,Y);
            Nodo nuevo = new Nodo(X,Y,coord.Item1,coord.Item2,contador++,c);

            //nuevo.Latitud = coord.Item1;
            //nuevo.Longitud = coord.Item2;
            //nuevo.indice = contador++;
            //nuevo

            nuevo.btn.Click += new EventHandler(btnParadero_Click);

            paraderos.Add(nuevo);
            p.Controls.Add(nuevo.btn);
        }

        private void btnParadero_Click(object sender, EventArgs e)
        {

            if (!modoAgregarRuta)
            {
                ParaderoButton btn = (ParaderoButton)sender;
                int indiceParadero = btn.indiceParadero;

                if (previousIndice != -1)
                {
                    double distance = GeoCodeCalc.CalcDistance(paraderos[previousIndice].Latitud, paraderos[previousIndice].Longitud,
                                                               paraderos[indiceParadero].Latitud, paraderos[indiceParadero].Longitud,
                                                               GeoCodeCalcMeasurement.Meters);
                    paraderos[previousIndice].addConexion(indiceParadero,distance);
                    paraderos[indiceParadero].addConexion(previousIndice, distance);

                    Panel p = (Panel)btn.Parent;
                    System.Drawing.Graphics gr = p.CreateGraphics();
                    gr.DrawLine(new Pen(new SolidBrush(Color.Blue), 3), paraderos[previousIndice].btn.x, paraderos[previousIndice].btn.y, btn.x, btn.y);
                    previousIndice = -1;
                }else
                previousIndice = indiceParadero;

            }

        }

        public void dibujaLinea(Graphics gr)
        {
            for (int i = 0; i < paraderos.Count; i++)
            {
                for (int j = 0; j < paraderos[i].conexiones.Count; j++)
                {
                    Tuple<int, int> xy1 = new Tuple<int, int>(paraderos[i].x, paraderos[i].y);
                    Tuple<int, int> xy2 = new Tuple<int, int>(paraderos[paraderos[i].conexiones[j].indexDestino].x,
                                                                paraderos[paraderos[i].conexiones[j].indexDestino].y);
                    gr.DrawLine(new Pen(new SolidBrush(Color.Blue), 3), xy1.Item1, xy1.Item2, xy2.Item1, xy2.Item2);
                }
                    


            }

        }


        //public void DibujarParaderos(System.Drawing.Graphics gr)
        //{
        //    for (int i = 0; i < paraderos.Count; i++)
        //        paraderos[i].Dibujar(gr);
        //}



    }
}
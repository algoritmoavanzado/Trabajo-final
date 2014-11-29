using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;
using Newtonsoft.Json;
using Sinbadsoft.Lib.Collections;
using System.Collections;

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
        public const int KILOMETROS_X_HORA_DRIVING = 50;

        public List<Nodo> paraderos;


        public Controlador()
        {            
            CantZooms = 25;
            INFINITO = 100000000;
            Path = new List<int>();
        }

        public void RecalcularXY( ){
            if (matriz == null) return;
            for (int i = 0; i < matriz.Count; i++)
            {
                for (int j = 0; j < matriz[i].Count; j++)
                {
                     Tuple<int,int,double,double> actual=matriz[i][j],nuevo;
                     Tuple<double, double> nuevosXY = obtenerXY(actual.Item3, actual.Item4);
                     nuevo = new Tuple<int, int, double, double>((int)nuevosXY.Item1, (int)nuevosXY.Item2, actual.Item3, actual.Item4);
                    matriz[i][j] = nuevo;
                }
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
            VolverGrafoSinPeso(fil, col);
            PonerlePesosMaps(fil, col);

        }

        public void VolverGrafoSinPeso(int fil,int col)
        {
            

            myGrafo = new List<List<Nodo>>(); 
		
            List<Nodo> aux;
	        for (int i = 0; i < fil; i++)
	        {
                
                Nodo nuevo,nuevo2,nuevo3,nuevo4;
		        for (int j = 0; j < col; j++)
		        {
                    aux = new List<Nodo>();
			        nuevo=new Nodo();
                    nuevo2 = new Nodo();
                    nuevo3 = new Nodo();
                    nuevo4 = new Nodo();
			        nuevo.Peso=0; 
                    if(i==0 && j==0)
                    {
                       nuevo.indiceNodoDestino=i*col+(j+1);
                        aux.Add(nuevo);//AÑADO EL DE LA DERECHA
                        nuevo2.indiceNodoDestino=(i+1)*col+(j);
                        aux.Add(nuevo2); //AÑADO EL DE ABAJO               
                    }
                    else if(i==0 && j==col-1)
                    {
                        nuevo.indiceNodoDestino=i*col+(j-1);
                        aux.Add(nuevo);//AÑADO EL DE LA IZQUIERDA
                        nuevo2.indiceNodoDestino=(i+1)*col+(j);
                        aux.Add(nuevo2); //AÑADO EL DE ABAJO 
                    }
                    else if(i==fil-1 && j==0)
                    {
                        nuevo.indiceNodoDestino=i*col+(j+1);
                        aux.Add(nuevo);//AÑADO EL DE LA IZQUIERDA
                        nuevo2.indiceNodoDestino=(i-1)*col+(j);
                        aux.Add(nuevo2); //AÑADO EL DE ARRIBA
                    }
                    else if(i==fil-1 && j==col-1)
                    {
                         nuevo.indiceNodoDestino=i*col+(j-1);
                        aux.Add(nuevo);//AÑADO EL DE LA DERECHA
                        nuevo2.indiceNodoDestino=(i-1)*col+(j);
                        aux.Add(nuevo2); //AÑADO EL DE ARRIBA 
                    }
                    else if(i==0) //SUPERIOR
                    {
                        nuevo.indiceNodoDestino=(i+1)*col+(j); 
                        aux.Add(nuevo); //ABAJO
                        nuevo2.indiceNodoDestino=(i)*col+(j-1);
                        aux.Add(nuevo2); //IZQUIERDA
                        nuevo3.indiceNodoDestino=(i)*col+(j+1);
                        aux.Add(nuevo3); //DERECHA
                    }
                    else if(i==fil-1) //INFERIOR
                    {
                        nuevo.indiceNodoDestino=(i-1)*col+(j); 
                        aux.Add(nuevo); //ARRIBA
                        nuevo2.indiceNodoDestino=(i)*col+(j-1);
                        aux.Add(nuevo2); //IZQUIERDA
                        nuevo3.indiceNodoDestino=(i)*col+(j+1);
                        aux.Add(nuevo3); //DERECHA
                    }
                    else if(j==0) //BORDE IZQUIERDO
                    {
                        nuevo.indiceNodoDestino=(i-1)*col+(j);                 
                        aux.Add(nuevo); //ARRIBA
                        nuevo2.indiceNodoDestino=(i)*col+(j+1);
                        aux.Add(nuevo2); //DERECHA
                        nuevo3.indiceNodoDestino=(i+1)*col+(j); 
                        aux.Add(nuevo3); //ABAJO
                    }
                    else if(j==col-1) //BORDE DERECHO
                    {
                        nuevo.indiceNodoDestino=(i-1)*col+(j);                 
                        aux.Add(nuevo); //ARRIBA
                        nuevo2.indiceNodoDestino=(i)*col+(j-1);
                        aux.Add(nuevo2); //IZQUIERDA
                        nuevo3.indiceNodoDestino=(i+1)*col+(j); 
                        aux.Add(nuevo3); //ABAJO
                    }
                    else //TODO LO DE ADENTRO
                    {
                        nuevo.indiceNodoDestino=(i-1)*col+(j);                 
                        aux.Add(nuevo); //ARRIBA                
                        nuevo2.indiceNodoDestino=(i+1)*col+(j); 
                        aux.Add(nuevo2); //ABAJO
                        nuevo3.indiceNodoDestino=(i)*col+(j-1);
                        aux.Add(nuevo3); //IZQUIERDA
                        nuevo4.indiceNodoDestino=(i)*col+(j+1);
                        aux.Add(nuevo4); //DERECHA
                    }

                    myGrafo.Add(aux);
		        }
                               
	        }

            //int x = 3;
        }

        public void PonerlePesosMaps(int fil, int col)
        {
            System.Random r=new System.Random((int)DateTime.Now.Ticks);
            //WebClient client;
           
            for (int i = 0; i < myGrafo.Count; i++)
            {
                for (int j = 0; j < myGrafo[i].Count; j++)
                {
                    //client = new WebClient();
                    int jInicio,iInicio,jFinal,iFinal;
                    jInicio = i % col;
                    iInicio = i / col;
                    jFinal=myGrafo[i][j].indiceNodoDestino%col;
                    iFinal=myGrafo[i][j].indiceNodoDestino/col;


                    var inicio = matriz[iInicio][jInicio].Item3 + "," + matriz[iInicio][jInicio].Item4;
                    var destino = matriz[iFinal][jFinal].Item3 + "," + matriz[iFinal][jFinal].Item4;
                    //var strJson2 = client.DownloadString("http://maps.googleapis.com/maps/api/distancematrix/json?origins=" + inicio + "&destinations=" + destino + "&mode=driving&sensor=false");
                    //dynamic dynJson = JsonConvert.DeserializeObject(strJson2);
                    //foreach (dynamic row in dynJson.rows)
                    //{
                    //    foreach (dynamic element in row.elements)
                    //    {
                    //        myGrafo[i][j].Peso = Convert.ToInt32("" + element.distance.value);

                    //    }
                    //}


                    myGrafo[i][j].Peso=r.Next(100,601);

                    //myGrafo[i][j].Peso=GeoCodeCalc.CalcDistance( matriz[iInicio][jInicio].Item3, matriz[iInicio][jInicio].Item4,
                    //                                             matriz[iFinal][jFinal].Item3,matriz[iFinal][jFinal].Item4,
                    //                                             GeoCodeCalcMeasurement.Miles);

                    //double peso=GeoCodeCalc.CalcDistance(matriz[iInicio][jInicio].Item3, matriz[iInicio][jInicio].Item4,
                    //    matriz[iFinal][jFinal].Item3, matriz[iFinal][jFinal].Item4, GeoCodeCalcMeasurement.Kilometers);

                    //myGrafo[i][j].Peso = peso;

                    //myGrafo[i][j].Peso = 10;
                }
            }

            //int delex = 0;
        }

        
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

            brocha.Dispose();
        }
                    
        public void Dijkstra(int pNodoOrigen,int pNodoDestino)
        {
            Path = new List<int>();  
       
	        List<int> ComollegoA=new List<int>();

            MinHeap ColaPendiente = new MinHeap();
    

            List<double> Distancias=new List<double>();

            for(int i=0;i<myGrafo.Count;i++)
                ComollegoA.Add(-1);

            for (int i = 0; i < myGrafo.Count; i++)
                Distancias.Add(INFINITO);
    
            Distancias[pNodoOrigen]=0;
            ColaPendiente.Add(new Nodo{Peso=Distancias[pNodoOrigen],indiceNodoDestino=pNodoOrigen}); //Mete el primero el cual se le asigna una distancia 0

            while(ColaPendiente.Count>0)
            {
                Nodo tempNodo=ColaPendiente.RemoveMin();
               //POP();

                if(pNodoDestino==tempNodo.indiceNodoDestino) break; //si es que nodo temp que estoy recorriendo llega a ser igual que el nodo destino significa que lo encontro(minimamente)
                for(int i=0;i<myGrafo[tempNodo.indiceNodoDestino].Count;i++)
                {
                     Nodo auxNodo=myGrafo[tempNodo.indiceNodoDestino][i];
                     //PARTTE DEL RELAX - auxNodo.first ES EL PEDACITO
                     if ((Distancias[tempNodo.indiceNodoDestino] + auxNodo.Peso) < Distancias[auxNodo.indiceNodoDestino])
                     {
                         Distancias[auxNodo.indiceNodoDestino] = Distancias[tempNodo.indiceNodoDestino] + auxNodo.Peso;
                         ColaPendiente.Add(new Nodo{ Peso=Distancias[auxNodo.indiceNodoDestino], indiceNodoDestino=auxNodo.indiceNodoDestino}); //LO AGREGO A LA COLITA DE PEND (NO SE QUEDARA VACIO)
                         ComollegoA[auxNodo.indiceNodoDestino] = tempNodo.indiceNodoDestino; //el tempNodo es el que reviso todos sus aristas tonce por ese medio llefo a aUxNODO
				 
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

    }
}
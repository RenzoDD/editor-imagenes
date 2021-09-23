using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor_de_imagenes
{
    public class Imagen
    {
        public int[,] R { get; set; } //R es el blanco y negro
        public int[,] G { get; set; }
        public int[,] B { get; set; }

        public int ancho { get; set; }
        public int alto { get; set; }
        public Imagen(int ancho, int alto)
        {
            this.ancho = ancho;
            this.alto = alto;
            R = new int[ancho, alto];
            G = new int[ancho, alto];
            B = new int[ancho, alto];
        }
        public Imagen(double[,]muestra, int ancho, int alto)
        {
            this.ancho = ancho;
            this.alto = alto;
            R = new int[ancho, alto];
            G = new int[ancho, alto];
            B = new int[ancho, alto];

            bool t1, t2, t3, t4;
            t1 = true; t2 = true; t3 = true; t4 = true;
            (new Task(() =>
            {
                for (int x = 0; x < ancho / 2; x++)
                    for (int y = 0; y < alto / 2; y++)
                        R[x, y] = G[x, y] = B[x, y] = (int)muestra[x, y];
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ancho / 2; x < ancho; x++)
                    for (int y = 0; y < alto / 2; y++)
                        R[x, y] = G[x, y] = B[x, y] = (int)muestra[x, y];

                t2 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = 0; x < ancho / 2; x++)
                    for (int y = alto / 2; y < alto; y++)
                        R[x, y] = G[x, y] = B[x, y] = (int)muestra[x, y];

                t3 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ancho / 2; x < ancho; x++)
                    for (int y = alto / 2; y < alto; y++)
                        R[x, y] = G[x, y] = B[x, y] = (int)muestra[x, y];

                t4 = false;
            })).Start();
            while (t1 || t2 || t3 || t4) { }

        }
        public Imagen(string ruta)
        {
            Bitmap img = new Bitmap(ruta);

            ancho = img.Width;
            alto = img.Height;
            R = new int[ancho, alto];
            G = new int[ancho, alto];
            B = new int[ancho, alto];

            for (int x = 0; x < ancho; x++)
                for (int y = 0; y < alto; y++)
                {
                    R[x, y] = img.GetPixel(x, y).R;
                    G[x, y] = img.GetPixel(x, y).G;
                    B[x, y] = img.GetPixel(x, y).B;
                }
            img.Dispose();
        }
    }
    public class EdicionImagenes
    {
        List<Imagen> imagenes = new List<Imagen>();
        public int operaciones { get; set; }
        public int completado { get; set; }
        public int hilo1 { get; set; }
        public int hilo2 { get; set; }
        public int hilo3 { get; set; }
        public int hilo4 { get; set; }
        public string state { get; set; }

        bool t1, t2, t3, t4;
        public bool NuevaImagen { get; set; }
        public EdicionImagenes(string nombre)
        {
            state = "Cargando imagen";

            Bitmap img = new Bitmap(nombre);
            Imagen ans = new Imagen(img.Width, img.Height);
            operaciones = ans.ancho * ans.alto;
            completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;
            for (int x = 0; x < ans.ancho; x++)
                for (int y = 0; y < ans.alto; y++)
                {
                    ans.R[x, y] = img.GetPixel(x, y).R;
                    ans.G[x, y] = img.GetPixel(x, y).G;
                    ans.B[x, y] = img.GetPixel(x, y).B;
                    completado++;
                }
            

            imagenes.Add(ans);
            NuevaImagen = true;
            state = "Listo";
        }
        public EdicionImagenes()
        {

        }

        public Bitmap LastImage()
        {
            NuevaImagen = false;
            return ComponerImagen();
        }
        public Bitmap ComponerImagen()
        {
            Imagen last = imagenes.Last();
            state = "Componiendo imagen";
            Bitmap img = new Bitmap(last.ancho, last.alto);
            operaciones = last.ancho * last.alto;
            completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;
            for (int x = 0; x < last.ancho; x++)
                for (int y = 0; y < last.alto; y++)
                {
                    int g = last.R[x, y];

                    try
                    {
                        img.SetPixel(x, y, Color.FromArgb(last.R[x, y], last.G[x, y], last.B[x, y]));
                    }
                    catch
                    {
                        img.SetPixel(x, y, Color.White);

                    }
                    finally
                    {
                        completado++;

                    }
                }
            state = "Listo";
            return img;
        }

        public void Deshacer()
        {
            if (imagenes.Count >= 2)
            {
                imagenes.RemoveAt(imagenes.Count - 1);
                NuevaImagen = true;
            }

        }
        public void CrearNuevaImagen(int ancho, int alto)
        {
            Imagen img = new Imagen(ancho, alto);
            imagenes.Add(img);
            NuevaImagen = true;
        }

        public void BlancoNegro()
        {
            Imagen img = imagenes.Last();
            Imagen ans = new Imagen(img.ancho, img.alto);

            operaciones = 2 * ans.ancho * ans.alto;
            completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;

            state = "Promediando RGB";
            t1 = true; t2 = true; t3 = true; t4 = true;
            (new Task(() =>
            {
                for (int x = 0; x < img.ancho / 2; x++)
                    for (int y = 0; y < img.alto / 2; y++)
                    {
                        ans.R[x, y] = (img.R[x, y] + img.G[x, y] + img.B[x, y]) / 3;
                        hilo1++;
                    }
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                    {
                        ans.R[x, y] = (img.R[x, y] + img.G[x, y] + img.B[x, y]) / 3;
                        hilo2++;
                    }
                t2 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                    {
                        ans.R[x, y] = (img.R[x, y] + img.G[x, y] + img.B[x, y]) / 3;
                        hilo3++;
                    }
                t3 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                    {
                        ans.R[x, y] = (img.R[x, y] + img.G[x, y] + img.B[x, y]) / 3;
                        hilo4++;
                    }
                t4 = false;
            })).Start();
            while (t1 || t2 || t3 || t4) { }
            ans.G = ans.B = ans.R;

            operaciones = completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;
            imagenes.Add(ans);
            NuevaImagen = true;
        }
        public void MediaBlancoNegro()
        {
            Imagen img = imagenes.Last();
            Imagen ans = new Imagen(img.ancho, img.alto);

            operaciones = 2 * ans.ancho * ans.alto;
            completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;

            state = "Aplicando máscara de la media";
            t1 = true; t2 = true; t3 = true; t4 = true;
            (new Task(() =>
            {
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            ans.R[x, y] = (img.R[x - 1, y - 1] + img.R[x + 0, y - 1] + img.R[x + 1, y - 1] +
                                           img.R[x - 1, y + 0] + img.R[x + 0, y + 0] + img.R[x + 1, y + 0] +
                                           img.R[x - 1, y + 1] + img.R[x + 0, y + 1] + img.R[x + 1, y + 1]) / 9;
                            
                            hilo1++;
                        }
                        else
                            hilo1++;
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            ans.R[x, y] = (img.R[x - 1, y - 1] + img.R[x + 0, y - 1] + img.R[x + 1, y - 1] +
                                           img.R[x - 1, y + 0] + img.R[x + 0, y + 0] + img.R[x + 1, y + 0] +
                                           img.R[x - 1, y + 1] + img.R[x + 0, y + 1] + img.R[x + 1, y + 1]) / 9;
                            hilo2++;
                        }
                        else
                            hilo2++;
                t2 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            ans.R[x, y] = (img.R[x - 1, y - 1] + img.R[x + 0, y - 1] + img.R[x + 1, y - 1] +
                                           img.R[x - 1, y + 0] + img.R[x + 0, y + 0] + img.R[x + 1, y + 0] +
                                           img.R[x - 1, y + 1] + img.R[x + 0, y + 1] + img.R[x + 1, y + 1]) / 9;
                            hilo3++;
                        }
                        else
                            hilo3++;
                t3 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            ans.R[x, y] = (img.R[x - 1, y - 1] + img.R[x + 0, y - 1] + img.R[x + 1, y - 1] +
                                           img.R[x - 1, y + 0] + img.R[x + 0, y + 0] + img.R[x + 1, y + 0] +
                                           img.R[x - 1, y + 1] + img.R[x + 0, y + 1] + img.R[x + 1, y + 1]) / 9;
                            hilo4++;
                        }
                        else
                            hilo4++;
                t4 = false;
            })).Start();
            
            while (t1 || t2 || t3 || t4) { }

            ans.G = ans.B = ans.R;
            operaciones = completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;
            imagenes.Add(ans);
            NuevaImagen = true;
        }
        public void MediaColor()
        {
            Imagen img = imagenes.Last();
            Imagen ans = new Imagen(img.ancho, img.alto);

            operaciones = ans.ancho * ans.alto;
            completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;

            state = "Aplicando máscara de la media RGB";
            t1 = true; t2 = true; t3 = true; t4 = true;
            (new Task(() =>
            {
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            ans.R[x, y] = (img.R[x - 1, y - 1] + img.R[x + 0, y - 1] + img.R[x + 1, y - 1] +
                                           img.R[x - 1, y + 0] + img.R[x + 0, y + 0] + img.R[x + 1, y + 0] +
                                           img.R[x - 1, y + 1] + img.R[x + 0, y + 1] + img.R[x + 1, y + 1]) / 9;
                            ans.G[x, y] = (img.G[x - 1, y - 1] + img.G[x + 0, y - 1] + img.G[x + 1, y - 1] +
                                           img.G[x - 1, y + 0] + img.G[x + 0, y + 0] + img.G[x + 1, y + 0] +
                                           img.G[x - 1, y + 1] + img.G[x + 0, y + 1] + img.G[x + 1, y + 1]) / 9;
                            ans.B[x, y] = (img.B[x - 1, y - 1] + img.B[x + 0, y - 1] + img.B[x + 1, y - 1] +
                                           img.B[x - 1, y + 0] + img.B[x + 0, y + 0] + img.B[x + 1, y + 0] +
                                           img.B[x - 1, y + 1] + img.B[x + 0, y + 1] + img.B[x + 1, y + 1]) / 9;

                            hilo1++;
                        }
                        else
                            hilo1++;
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            ans.R[x, y] = (img.R[x - 1, y - 1] + img.R[x + 0, y - 1] + img.R[x + 1, y - 1] +
                                           img.R[x - 1, y + 0] + img.R[x + 0, y + 0] + img.R[x + 1, y + 0] +
                                           img.R[x - 1, y + 1] + img.R[x + 0, y + 1] + img.R[x + 1, y + 1]) / 9;
                            ans.G[x, y] = (img.G[x - 1, y - 1] + img.G[x + 0, y - 1] + img.G[x + 1, y - 1] +
                                           img.G[x - 1, y + 0] + img.G[x + 0, y + 0] + img.G[x + 1, y + 0] +
                                           img.G[x - 1, y + 1] + img.G[x + 0, y + 1] + img.G[x + 1, y + 1]) / 9;
                            ans.B[x, y] = (img.B[x - 1, y - 1] + img.B[x + 0, y - 1] + img.B[x + 1, y - 1] +
                                           img.B[x - 1, y + 0] + img.B[x + 0, y + 0] + img.B[x + 1, y + 0] +
                                           img.B[x - 1, y + 1] + img.B[x + 0, y + 1] + img.B[x + 1, y + 1]) / 9;
                            hilo2++;
                        }
                        else
                            hilo2++;
                t2 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            ans.R[x, y] = (img.R[x - 1, y - 1] + img.R[x + 0, y - 1] + img.R[x + 1, y - 1] +
                                           img.R[x - 1, y + 0] + img.R[x + 0, y + 0] + img.R[x + 1, y + 0] +
                                           img.R[x - 1, y + 1] + img.R[x + 0, y + 1] + img.R[x + 1, y + 1]) / 9;
                            ans.G[x, y] = (img.G[x - 1, y - 1] + img.G[x + 0, y - 1] + img.G[x + 1, y - 1] +
                                           img.G[x - 1, y + 0] + img.G[x + 0, y + 0] + img.G[x + 1, y + 0] +
                                           img.G[x - 1, y + 1] + img.G[x + 0, y + 1] + img.G[x + 1, y + 1]) / 9;
                            ans.B[x, y] = (img.B[x - 1, y - 1] + img.B[x + 0, y - 1] + img.B[x + 1, y - 1] +
                                           img.B[x - 1, y + 0] + img.B[x + 0, y + 0] + img.B[x + 1, y + 0] +
                                           img.B[x - 1, y + 1] + img.B[x + 0, y + 1] + img.B[x + 1, y + 1]) / 9;
                            hilo3++;
                        }
                        else
                            hilo3++;
                t3 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            ans.R[x, y] = (img.R[x - 1, y - 1] + img.R[x + 0, y - 1] + img.R[x + 1, y - 1] +
                                           img.R[x - 1, y + 0] + img.R[x + 0, y + 0] + img.R[x + 1, y + 0] +
                                           img.R[x - 1, y + 1] + img.R[x + 0, y + 1] + img.R[x + 1, y + 1]) / 9;
                            ans.G[x, y] = (img.G[x - 1, y - 1] + img.G[x + 0, y - 1] + img.G[x + 1, y - 1] +
                                           img.G[x - 1, y + 0] + img.G[x + 0, y + 0] + img.G[x + 1, y + 0] +
                                           img.G[x - 1, y + 1] + img.G[x + 0, y + 1] + img.G[x + 1, y + 1]) / 9;
                            ans.B[x, y] = (img.B[x - 1, y - 1] + img.B[x + 0, y - 1] + img.B[x + 1, y - 1] +
                                           img.B[x - 1, y + 0] + img.B[x + 0, y + 0] + img.B[x + 1, y + 0] +
                                           img.B[x - 1, y + 1] + img.B[x + 0, y + 1] + img.B[x + 1, y + 1]) / 9;
                            hilo4++;
                        }
                        else
                            hilo4++;
                t4 = false;
            })).Start();
            while (t1 || t2 || t3 || t4) { }
            
            operaciones = completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;
            imagenes.Add(ans);
            NuevaImagen = true;
        }

        void Reescalamiento(Imagen ans, double menor, double mayor)
        {
            state = "Reescalando imagen";
            t1 = true; t2 = true; t3 = true; t4 = true;
            (new Task(() =>
            {
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                        {
                            int c =  ans.R[x, y] = Convert.ToInt32((double)(255 / (double)(mayor - menor)) * ((double)ans.R[x, y] - menor));

                            hilo1++;
                        }
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                        {
                            ans.R[x, y] = Convert.ToInt32((double)(255 / (double)(mayor - menor)) * (ans.R[x, y] - menor));
                            hilo2++;
                        }
                t2 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                        {
                            ans.R[x, y] = Convert.ToInt32((double)(255 / (double)(mayor - menor)) * (ans.R[x, y] - menor));
                            hilo3++;
                        }
                t3 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                    {
                        ans.R[x, y] = Convert.ToInt32((double)(255 / (double)(mayor - menor)) * (ans.R[x, y] - menor));
                        hilo4++;
                    } 
                t4 = false;
            })).Start();
            while (t1 || t2 || t3 || t4) { }
            ans.G = ans.B = ans.R;
        }
        double[,] Reescalamiento(double[,] ans, double menor, double mayor, int ancho, int alto)
        {
            state = "Reescalando imagen";
            t1 = true; t2 = true; t3 = true; t4 = true;
            (new Task(() =>
            {
                for (int x = 0; x < ancho / 2; x++)
                    for (int y = 0; y < alto / 2; y++)
                        if (!(x < 1 || y < 1 || x >= ancho - 1 || y >= alto - 1))
                        {
                            ans[x, y] = (double)(255 / (double)(mayor - menor)) * ((double)ans[x, y] - menor);
                            hilo1++;
                        }
                        else
                            hilo1++;
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ancho / 2; x < ancho; x++)
                    for (int y = 0; y < alto / 2; y++)
                        if (!(x < 1 || y < 1 || x >= ancho - 1 || y >= alto - 1))
                        {
                            ans[x, y] = (double)(255 / (double)(mayor - menor)) * (ans[x, y] - menor);
                            hilo2++;
                        }
                        else
                            hilo2++;
                t2 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = 0; x < ancho / 2; x++)
                    for (int y = alto / 2; y < alto; y++)
                        if (!(x < 1 || y < 1 || x >= ancho - 1 || y >= alto - 1))
                        {
                            ans[x, y] = (double)(255 / (double)(mayor - menor)) * (ans[x, y] - menor);
                            hilo3++;
                        }
                        else
                            hilo3++;
                t3 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ancho / 2; x < ancho; x++)
                    for (int y = alto / 2; y < alto; y++)
                        if (!(x < 1 || y < 1 || x >= ancho - 1 || y >= alto - 1))
                        {
                            ans[x, y] = (double)(255 / (double)(mayor - menor)) * (ans[x, y] - menor);
                            hilo4++;
                        }
                        else
                            hilo4++;
                t4 = false;
            })).Start();
            while (t1 || t2 || t3 || t4) { }
            return ans;
        }
        public void Laplace()
        {
            Imagen img = imagenes.Last();
            Imagen ans = new Imagen(img.ancho, img.alto);

            operaciones = 2 * ans.ancho * ans.alto;
            completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;

            ////////////////////////////////FILTRO LAPLACE////////////////////////////////
            state = "Aplicando máscara de laplace";
            t1 = true; t2 = true; t3 = true; t4 = true;
            int menor1 = int.MaxValue, mayor1 = int.MinValue;
            int menor2 = int.MaxValue, mayor2 = int.MinValue;
            int menor3 = int.MaxValue, mayor3 = int.MinValue;
            int menor4 = int.MaxValue, mayor4 = int.MinValue;
            (new Task(() =>
            {
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            ans.R[x, y] = (                       img.R[x + 0, y - 1] +
                                            img.R[x - 1, y + 0] + img.R[x + 0, y + 0] * (-4) + img.R[x + 1, y + 0] +
                                                                  img.R[x + 0, y + 1]);

                            if (ans.R[x, y] > mayor1)
                                mayor1 = ans.R[x, y];
                            if (ans.R[x, y] < menor1)
                                menor1 = ans.R[x, y];
                            
                            hilo1++;
                        }
                        else
                            hilo1++;
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            ans.R[x, y] = (                       img.R[x + 0, y - 1] +
                                            img.R[x - 1, y + 0] + img.R[x + 0, y + 0] * (-4) + img.R[x + 1, y + 0] +
                                                                  img.R[x + 0, y + 1]);

                            if (ans.R[x, y] > mayor1)
                                mayor1 = ans.R[x, y];
                            if (ans.R[x, y] < menor1)
                                menor1 = ans.R[x, y];

                            hilo2++;
                        }
                        else
                            hilo2++;
                t2 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            ans.R[x, y] = (                       img.R[x + 0, y - 1] +
                                            img.R[x - 1, y + 0] + img.R[x + 0, y + 0] * (-4) + img.R[x + 1, y + 0] +
                                                                  img.R[x + 0, y + 1]);

                            if (ans.R[x, y] > mayor1)
                                mayor1 = ans.R[x, y];
                            if (ans.R[x, y] < menor1)
                                menor1 = ans.R[x, y];

                            hilo3++;
                        }
                        else
                            hilo3++;
                t3 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            ans.R[x, y] = (                       img.R[x + 0, y - 1] +
                                            img.R[x - 1, y + 0] + img.R[x + 0, y + 0] * (-4) + img.R[x + 1, y + 0] +
                                                                  img.R[x + 0, y + 1]);

                            if (ans.R[x, y] > mayor1)
                                mayor1 = ans.R[x, y];
                            if (ans.R[x, y] < menor1)
                                menor1 = ans.R[x, y];

                            hilo4++;
                        }
                        else
                            hilo4++;
                t4 = false;
            })).Start();
            while (t1 || t2 || t3 || t4) { }
            int menor = Math.Min(Math.Min(Math.Min(menor1, menor2), menor3), menor4);
            int mayor = Math.Max(Math.Max(Math.Max(mayor1, mayor2), mayor3), mayor4);
            //////////////////////////////////////////////////////////////////////////////

            Reescalamiento(ans, menor, mayor);

            ans.B = ans.G = ans.R;
            operaciones = completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;
            imagenes.Add(ans);
            NuevaImagen = true;
        }
        public void Sobel()
        {
            Imagen img = imagenes.Last();
            Imagen ans = new Imagen(img.ancho, img.alto);
            
            operaciones = 2 * ans.ancho * ans.alto;
            completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;

            /////////////////////////////////FILTRO SOBEL/////////////////////////////////
            state = "Aplicando máscara de gradiente en x e y";
            bool t1 = true, t2 = true, t3 = true, t4 = true;
            int menor1 = int.MaxValue, mayor1 = int.MinValue;
            int menor2 = int.MaxValue, mayor2 = int.MinValue;
            int menor3 = int.MaxValue, mayor3 = int.MinValue;
            int menor4 = int.MaxValue, mayor4 = int.MinValue;
            (new Task(() =>
            {
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            int gX = img.R[x - 1, y - 1] * (-1) + img.R[x + 1, y - 1] * (1) +
                                     img.R[x - 1, y + 0] * (-2) + img.R[x + 1, y + 0] * (2) +
                                     img.R[x - 1, y + 1] * (-1) + img.R[x + 1, y + 1] * (1);


                            int gY = img.R[x - 1, y - 1] * (-1) + img.R[x + 0, y - 1] * (-2) + img.R[x + 1, y - 1] * (-1) +
                                     img.R[x - 1, y + 1] * ( 1) + img.R[x + 0, y + 1] * ( 2) + img.R[x + 1, y + 1] * ( 1);

                            ans.R[x, y] = (int)Math.Sqrt(gX * gX + gY * gY);

                            if (ans.R[x, y] > mayor1)
                                mayor1 = ans.R[x, y];
                            if (ans.R[x, y] < menor1)
                                menor1 = ans.R[x, y];
                            
                            hilo1++;
                        }
                        else
                            hilo1++;
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            int gX = img.R[x - 1, y - 1] * (-1) + img.R[x + 1, y - 1] * (1) +
                                     img.R[x - 1, y + 0] * (-2) + img.R[x + 1, y + 0] * (2) +
                                     img.R[x - 1, y + 1] * (-1) + img.R[x + 1, y + 1] * (1);


                            int gY = img.R[x - 1, y - 1] * (-1) + img.R[x + 0, y - 1] * (-2) + img.R[x + 1, y - 1] * (-1) +
                                     img.R[x - 1, y + 1] * ( 1) + img.R[x + 0, y + 1] * ( 2) + img.R[x + 1, y + 1] * ( 1);

                            ans.R[x, y] = (int)Math.Sqrt(gX * gX + gY * gY);

                            if (ans.R[x, y] > mayor1)
                                mayor1 = ans.R[x, y];
                            if (ans.R[x, y] < menor1)
                                menor1 = ans.R[x, y];

                            hilo2++;
                        }
                        else
                            hilo2++;
                t2 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            int gX = img.R[x - 1, y - 1] * (-1) + img.R[x + 1, y - 1] * (1) +
                                     img.R[x - 1, y + 0] * (-2) + img.R[x + 1, y + 0] * (2) +
                                     img.R[x - 1, y + 1] * (-1) + img.R[x + 1, y + 1] * (1);


                            int gY = img.R[x - 1, y - 1] * (-1) + img.R[x + 0, y - 1] * (-2) + img.R[x + 1, y - 1] * (-1) +
                                     img.R[x - 1, y + 1] * ( 1) + img.R[x + 0, y + 1] * ( 2) + img.R[x + 1, y + 1] * ( 1);

                            ans.R[x, y] = (int)Math.Sqrt(gX * gX + gY * gY);

                            if (ans.R[x, y] > mayor1)
                                mayor1 = ans.R[x, y];
                            if (ans.R[x, y] < menor1)
                                menor1 = ans.R[x, y];

                            hilo3++;
                        }
                        else
                            hilo3++;
                t3 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            int gX = img.R[x - 1, y - 1] * (-1) + img.R[x + 1, y - 1] * (1) +
                                     img.R[x - 1, y + 0] * (-2) + img.R[x + 1, y + 0] * (2) +
                                     img.R[x - 1, y + 1] * (-1) + img.R[x + 1, y + 1] * (1);


                            int gY = img.R[x - 1, y - 1] * (-1) + img.R[x + 0, y - 1] * (-2) + img.R[x + 1, y - 1] * (-1) +
                                     img.R[x - 1, y + 1] * ( 1) + img.R[x + 0, y + 1] * ( 2) + img.R[x + 1, y + 1] * ( 1);

                            ans.R[x, y] = (int)Math.Sqrt(gX * gX + gY * gY);

                            if (ans.R[x, y] > mayor1)
                                mayor1 = ans.R[x, y];
                            if (ans.R[x, y] < menor1)
                                menor1 = ans.R[x, y];

                            hilo4++;
                        }
                        else
                            hilo4++;
                t4 = false;
            })).Start();
            while (t1 || t2 || t3 || t4) { }
            int menor = Math.Min(Math.Min(Math.Min(menor1, menor2), menor3), menor4);
            int mayor = Math.Max(Math.Max(Math.Max(mayor1, mayor2), mayor3), mayor4);
            //////////////////////////////////////////////////////////////////////////////
            
            Reescalamiento(ans, menor, mayor);
            ans.G = ans.B = ans.R;
            operaciones = completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;
            imagenes.Add(ans);
            NuevaImagen = true;
        }

        public void Canny(int sensibilidad)
        {
            Imagen img = imagenes.Last();
            Imagen ans = new Imagen(img.ancho, img.alto);
            
            operaciones = 5 * img.ancho * img.alto;
            completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;

            /////////////////////////////////FILTRO SOBEL/////////////////////////////////
            state = "Aplicando filtro de sobel";
            bool t1 = true, t2 = true, t3 = true, t4 = true;
            (new Task(() =>
            {
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            int gX = img.R[x - 1, y - 1] * (-1) + img.R[x + 1, y - 1] +
                                     img.R[x - 1, y + 0] * (-2) + img.R[x + 1, y + 0] * (2) +
                                     img.R[x - 1, y + 1] * (-1) + img.R[x + 1, y + 1];


                            int gY = img.R[x - 1, y - 1] * (-1) + img.R[x + 0, y - 1] * (-2) + img.R[x + 1, y - 1] * (-1) +
                                     img.R[x - 1, y + 1] * ( 1) + img.R[x + 0, y + 1] * ( 2) + img.R[x + 1, y + 1] * ( 1);

                            ans.G[x, y] = (int)Math.Sqrt(gX * gX + gY * gY);
                            
                            int angulo = (int)(Math.Atan2(gY, gX) / 0.0174533);

                            if (((angulo < 22.5) && (angulo > -22.5)) || (angulo > 157.5) || (angulo < -157.5))
                                angulo = 0;
                            if (((angulo > 22.5) && (angulo < 67.5)) || ((angulo < -112.5) && (angulo > -157.5)))
                                angulo = 45;
                            if (((angulo > 67.5) && (angulo < 112.5)) || ((angulo < -67.5) && (angulo > -112.5)))
                                angulo = 90;
                            if (((angulo > 112.5) && (angulo < 157.5)) || ((angulo < -22.5) && (angulo > -67.5)))
                                angulo = 135;

                            ans.B[x, y] = angulo;

                            hilo1++;
                        }
                        else
                            hilo1++;
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            int gX = img.R[x - 1, y - 1] * (-1) + img.R[x + 1, y - 1] +
                                                   img.R[x - 1, y + 0] * (-2) + img.R[x + 1, y + 0] * (2) +
                                                   img.R[x - 1, y + 1] * (-1) + img.R[x + 1, y + 1];


                            int gY = img.R[x - 1, y - 1] * (-1) + img.R[x + 0, y - 1] * (-2) + img.R[x + 1, y - 1] * (-1) +
                                 img.R[x - 1, y + 1] + img.R[x + 0, y + 1] * (2) + img.R[x + 1, y + 1];

                            ans.G[x, y] = (int)Math.Sqrt(gX * gX + gY * gY);
                            
                            int angulo = (int)(Math.Atan2(gY, gX) / 0.0174533);

                            if (((angulo < 22.5) && (angulo > -22.5)) || (angulo > 157.5) || (angulo < -157.5))
                                angulo = 0;
                            if (((angulo > 22.5) && (angulo < 67.5)) || ((angulo < -112.5) && (angulo > -157.5)))
                                angulo = 45;
                            if (((angulo > 67.5) && (angulo < 112.5)) || ((angulo < -67.5) && (angulo > -112.5)))
                                angulo = 90;
                            if (((angulo > 112.5) && (angulo < 157.5)) || ((angulo < -22.5) && (angulo > -67.5)))
                                angulo = 135;

                            ans.B[x, y] = angulo;

                            hilo2++;
                        }
                        else
                            hilo2++;
                t2 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            int gX = img.R[x - 1, y - 1] * (-1) + img.R[x + 1, y - 1] +
                                                   img.R[x - 1, y + 0] * (-2) + img.R[x + 1, y + 0] * (2) +
                                                   img.R[x - 1, y + 1] * (-1) + img.R[x + 1, y + 1];


                            int gY = img.R[x - 1, y - 1] * (-1) + img.R[x + 0, y - 1] * (-2) + img.R[x + 1, y - 1] * (-1) +
                                 img.R[x - 1, y + 1] + img.R[x + 0, y + 1] * (2) + img.R[x + 1, y + 1];

                            ans.G[x, y] = (int)Math.Sqrt(gX * gX + gY * gY);
                           
                            int angulo = (int)(Math.Atan2(gY, gX) / 0.0174533);

                            if (((angulo < 22.5) && (angulo > -22.5)) || (angulo > 157.5) || (angulo < -157.5))
                                angulo = 0;
                            if (((angulo > 22.5) && (angulo < 67.5)) || ((angulo < -112.5) && (angulo > -157.5)))
                                angulo = 45;
                            if (((angulo > 67.5) && (angulo < 112.5)) || ((angulo < -67.5) && (angulo > -112.5)))
                                angulo = 90;
                            if (((angulo > 112.5) && (angulo < 157.5)) || ((angulo < -22.5) && (angulo > -67.5)))
                                angulo = 135;

                            ans.B[x, y] = angulo;

                            hilo3++;
                        }
                        else
                            hilo3++;
                t3 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                        if (!(x < 1 || y < 1 || x >= ans.ancho - 1 || y >= ans.alto - 1))
                        {
                            int gX = img.R[x - 1, y - 1] * (-1) + img.R[x + 1, y - 1] +
                                                   img.R[x - 1, y + 0] * (-2) + img.R[x + 1, y + 0] * (2) +
                                                   img.R[x - 1, y + 1] * (-1) + img.R[x + 1, y + 1];


                            int gY = img.R[x - 1, y - 1] * (-1) + img.R[x + 0, y - 1] * (-2) + img.R[x + 1, y - 1] * (-1) +
                                     img.R[x - 1, y + 1] + img.R[x + 0, y + 1] * (2) + img.R[x + 1, y + 1];

                            ans.G[x, y] = (int)Math.Sqrt(gX * gX + gY * gY);
                            
                            int angulo = (int)(Math.Atan2(gY, gX) / 0.0174533);

                            if (((angulo < 22.5) && (angulo > -22.5)) || (angulo > 157.5) || (angulo < -157.5))
                                angulo = 0;
                            if (((angulo > 22.5) && (angulo < 67.5)) || ((angulo < -112.5) && (angulo > -157.5)))
                                angulo = 45;
                            if (((angulo > 67.5) && (angulo < 112.5)) || ((angulo < -67.5) && (angulo > -112.5)))
                                angulo = 90;
                            if (((angulo > 112.5) && (angulo < 157.5)) || ((angulo < -22.5) && (angulo > -67.5)))
                                angulo = 135;

                            ans.B[x, y] = angulo;

                            hilo4++;
                        }
                        else
                            hilo4++;
                t4 = false;
            })).Start();
            while (t1 || t2 || t3 || t4) { }
            //////////////////////////////////////////////////////////////////////////////


            /////////////////////////////////NONMAX SUPPR/////////////////////////////////
            state =  "Suprimiendo los No-Máximos";
            int[,] NonMaskSuppress = new int[ans.ancho, ans.alto];
            t1 = true; t2 = true; t3 = true; t4 = true;
            (new Task(() =>
            {
                for (int i = 1; i < (ans.ancho - 1) / 2; i++)
                    for (int j = 1; j < (ans.alto - 1) / 2; j++)
                    {
                        switch (ans.B[i, j])
                        {
                            case 0:
                                if ((ans.G[i, j] >= ans.G[i - 1, j]) && (ans.G[i, j] >= ans.G[i + 1, j]))
                                {
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                }
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            case 45:
                                if ((ans.G[i, j] >= ans.G[i - 1, j - 1]) && (ans.G[i, j] >= ans.G[i + 1, j + 1]))
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            case 90:
                                if ((ans.G[i, j] >= ans.G[i, j - 1]) && (ans.G[i, j] >= ans.G[i, j + 1]))
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            case 135:
                                if ((ans.G[i, j] >= ans.G[i - 1, j + 1]) && (ans.G[i, j] >= ans.G[i + 1, j - 1]))
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            default:
                                break;
                        }

                        hilo1++;
                    }
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                for (int i = (ans.ancho - 1) / 2; i < ans.ancho - 1; i++)
                    for (int j = 1; j < (ans.alto - 1) / 2; j++)
                    {
                        switch (ans.B[i, j])
                        {
                            case 0:
                                if ((ans.G[i, j] >= ans.G[i - 1, j]) && (ans.G[i, j] >= ans.G[i + 1, j]))
                                {
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                }
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            case 45:
                                if ((ans.G[i, j] >= ans.G[i - 1, j - 1]) && (ans.G[i, j] >= ans.G[i + 1, j + 1]))
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            case 90:
                                if ((ans.G[i, j] >= ans.G[i, j - 1]) && (ans.G[i, j] >= ans.G[i, j + 1]))
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            case 135:
                                if ((ans.G[i, j] >= ans.G[i - 1, j + 1]) && (ans.G[i, j] >= ans.G[i + 1, j - 1]))
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            default:
                                break;
                        }

                        hilo2++;
                    }
                t2 = false;
            })).Start();
            (new Task(() =>
            {
                for (int i = 1; i < (ans.ancho - 1) / 2; i++)
                    for (int j = (ans.alto - 1) / 2; j < ans.alto - 1; j++)
                    {
                        switch (ans.B[i, j])
                        {
                            case 0:
                                if ((ans.G[i, j] >= ans.G[i - 1, j]) && (ans.G[i, j] >= ans.G[i + 1, j]))
                                {
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                }
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            case 45:
                                if ((ans.G[i, j] >= ans.G[i - 1, j - 1]) && (ans.G[i, j] >= ans.G[i + 1, j + 1]))
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            case 90:
                                if ((ans.G[i, j] >= ans.G[i, j - 1]) && (ans.G[i, j] >= ans.G[i, j + 1]))
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            case 135:
                                if ((ans.G[i, j] >= ans.G[i - 1, j + 1]) && (ans.G[i, j] >= ans.G[i + 1, j - 1]))
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            default:
                                break;
                        }

                        hilo3++;
                    }
                t3 = false;
            })).Start();
            (new Task(() =>
            {
                for (int i = (ans.ancho - 1) / 2; i < ans.ancho - 1; i++)
                    for (int j = (ans.alto - 1) / 2; j < ans.alto - 1; j++)
                    {
                        switch (ans.B[i, j])
                        {
                            case 0:
                                if ((ans.G[i, j] >= ans.G[i - 1, j]) && (ans.G[i, j] >= ans.G[i + 1, j]))
                                {
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                }
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            case 45:
                                if ((ans.G[i, j] >= ans.G[i - 1, j - 1]) && (ans.G[i, j] >= ans.G[i + 1, j + 1]))
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            case 90:
                                if ((ans.G[i, j] >= ans.G[i, j - 1]) && (ans.G[i, j] >= ans.G[i, j + 1]))
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            case 135:
                                if ((ans.G[i, j] >= ans.G[i - 1, j + 1]) && (ans.G[i, j] >= ans.G[i + 1, j - 1]))
                                    NonMaskSuppress[i, j] = ans.G[i, j];
                                else
                                    NonMaskSuppress[i, j] = 0;
                                break;
                            default:
                                break;
                        }

                        hilo4++;
                    }
                t4 = false;
            })).Start();
            while (t1 || t2 || t3 || t4) { }


            state = "Obteniendo el No-Maximo Mayor y Menor";
            int menor1 = int.MaxValue, mayor1 = int.MinValue;
            int menor2 = int.MaxValue, mayor2 = int.MinValue;
            int menor3 = int.MaxValue, mayor3 = int.MinValue;
            int menor4 = int.MaxValue, mayor4 = int.MinValue;
            t1 = true; t2 = true; t3 = true; t4 = true;
            (new Task(() =>
            {
                for (int i = 2; i < (ans.ancho - 2) / 2; i++)
                    for (int j = 2; j < (ans.alto - 2) / 2; j++)
                    {
                        if (NonMaskSuppress[i, j] > mayor1)
                            mayor1 = NonMaskSuppress[i, j];
                        else if (NonMaskSuppress[i, j] < menor1)
                            menor1 = NonMaskSuppress[i, j];
                        hilo1++;
                    }
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                for (int i = (ans.ancho - 2) / 2; i < ans.ancho - 2; i++)
                    for (int j = 2; j < (ans.alto - 2) / 2; j++)
                    {
                        if (NonMaskSuppress[i, j] > mayor2)
                            mayor2 = NonMaskSuppress[i, j];
                        else if (NonMaskSuppress[i, j] < menor2)
                            menor2 = NonMaskSuppress[i, j];

                        hilo2++;
                    }
                t2 = false;
            })).Start();
            (new Task(() =>
            {
                for (int i = 2; i < (ans.ancho - 2) / 2; i++)
                    for (int j = (ans.alto - 2) / 2; j < ans.alto - 2; j++)
                    {
                        if (NonMaskSuppress[i, j] > mayor3)
                            mayor3 = NonMaskSuppress[i, j];
                        else if (NonMaskSuppress[i, j] < menor3)
                            menor3 = NonMaskSuppress[i, j];

                        hilo3++;
                    }
                t3 = false;
            })).Start();
            (new Task(() =>
            {
                for (int i = (ans.ancho - 2) / 2; i < ans.ancho - 2; i++)
                    for (int j = (ans.alto - 2) / 2; j < ans.alto - 2; j++)
                    {
                        if (NonMaskSuppress[i, j] > mayor4)
                            mayor4 = NonMaskSuppress[i, j];
                        else if (NonMaskSuppress[i, j] < menor4)
                            menor4 = NonMaskSuppress[i, j];

                        hilo4++;
                    }
                t4 = false;
            })).Start();
            while (t1 || t2 || t3 || t4) { }
            int min = Math.Min(Math.Min(Math.Min(menor1, menor2), menor3), menor4);
            int max = Math.Max(Math.Max(Math.Max(mayor1, mayor2), mayor3), mayor4);


            state = "Creando matriz temporal";
            t1 = true; t2 = true; t3 = true; t4 = true;
            (new Task(() =>
            {
                int temporary;
                for (int i = 1; i < (ans.ancho - 1) / 2; i++)
                    for (int j = 1; j < (ans.alto - 1) / 2; j++)
                    {
                        if (NonMaskSuppress[i, j] > 0)
                            temporary = (NonMaskSuppress[i, j] - min) * 255 / (max - min);
                        else temporary = 0;
                        NonMaskSuppress[i, j] = temporary;

                        hilo1++;
                    }
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                int temporary;
                for (int i = (ans.ancho - 1) / 2; i < ans.ancho - 1; i++)
                    for (int j = 1; j < (ans.alto - 1) / 2; j++)
                    {
                        if (NonMaskSuppress[i, j] > 0)
                            temporary = (NonMaskSuppress[i, j] - min) * 255 / (max - min);
                        else temporary = 0;
                        NonMaskSuppress[i, j] = temporary;

                        hilo2++;
                    }
                t2 = false;
            })).Start();
            (new Task(() =>
            {
                int temporary;
                for (int i = 1; i < (ans.ancho - 1) / 2; i++)
                    for (int j = (ans.alto - 1) / 2; j < ans.alto - 1; j++)
                    {
                        if (NonMaskSuppress[i, j] > 0)
                            temporary = (NonMaskSuppress[i, j] - min) * 255 / (max - min);
                        else temporary = 0;
                        NonMaskSuppress[i, j] = temporary;

                        hilo3++;
                    }
                t3 = false;
            })).Start();
            (new Task(() =>
            {
                int temporary;
                for (int i = (ans.ancho - 1) / 2; i < ans.ancho - 1; i++)
                    for (int j = (ans.alto - 1) / 2; j < ans.alto - 1; j++)
                    {
                        if (NonMaskSuppress[i, j] > 0)
                            temporary = (NonMaskSuppress[i, j] - min) * 255 / (max - min);
                        else temporary = 0;
                        NonMaskSuppress[i, j] = temporary;

                        hilo4++;
                    }
                t4 = false;
            })).Start();
            while (t1 || t2 || t3 || t4) { }

            //////////////////////////////////////////////////////////////////////////////


            /////////////////////////////////TRACE EDGES//////////////////////////////////
            int UpperThres = sensibilidad;
            int LowerThres = (int)(UpperThres / (double)3);
            state = "Dibujando bordes";
            t1 = true; t2 = true; t3 = true; t4 = true;
            (new Task(() =>
            {
                for (int i = 1; i < (ans.ancho - 1) / 2; i++)
                    for (int j = 1; j < (ans.alto - 1) / 2; j++)
                    {
                        if (NonMaskSuppress[i, j] > UpperThres)
                            ans.R[i, j] = 255;
                        else if (NonMaskSuppress[i, j] < LowerThres) //Lower thres = Upper thres / 3
                            ans.R[i, j] = 0;
                        else
                            switch (img.B[i, j])
                            {
                                case 0:
                                    if ((NonMaskSuppress[i - 1, j] > UpperThres) || (NonMaskSuppress[i - 1, j] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                case 45:
                                    if ((NonMaskSuppress[i - 1, j - 1] > UpperThres) || (NonMaskSuppress[i + 1, j + 1] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                case 90:
                                    if ((NonMaskSuppress[i, j - 1] > UpperThres) || (NonMaskSuppress[i, j + 1] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                case 135:
                                    if ((NonMaskSuppress[i - 1, j + 1] > UpperThres) || (NonMaskSuppress[i + 1, j - 1] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                default:
                                    break;
                            }
                        hilo1++;
                    }
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                for (int i = 1; i < (ans.ancho - 1) / 2; i++)
                    for (int j = (ans.alto - 1) / 2; j < ans.alto - 1; j++)
                    {
                        if (NonMaskSuppress[i, j] > UpperThres)
                            ans.R[i, j] = 255;
                        else if (NonMaskSuppress[i, j] < LowerThres) //Lower thres = Upper thres / 3
                            ans.R[i, j] = 0;
                        else
                            switch (img.B[i, j])
                            {
                                case 0:
                                    if ((NonMaskSuppress[i - 1, j] > UpperThres) || (NonMaskSuppress[i - 1, j] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                case 45:
                                    if ((NonMaskSuppress[i - 1, j - 1] > UpperThres) || (NonMaskSuppress[i + 1, j + 1] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                case 90:
                                    if ((NonMaskSuppress[i, j - 1] > UpperThres) || (NonMaskSuppress[i, j + 1] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                case 135:
                                    if ((NonMaskSuppress[i - 1, j + 1] > UpperThres) || (NonMaskSuppress[i + 1, j - 1] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                default:
                                    break;
                            }
                        hilo2++;
                    }
                t2 = false;
            })).Start();
            (new Task(() =>
            {
                for (int i = (ans.ancho - 1) / 2; i < ans.ancho - 1; i++)
                    for (int j = 1; j < (ans.alto - 1) / 2; j++)
                    {
                        if (NonMaskSuppress[i, j] > UpperThres)
                            ans.R[i, j] = 255;
                        else if (NonMaskSuppress[i, j] < LowerThres) //Lower thres = Upper thres / 3
                            ans.R[i, j] = 0;
                        else
                            switch (img.B[i, j])
                            {
                                case 0:
                                    if ((NonMaskSuppress[i - 1, j] > UpperThres) || (NonMaskSuppress[i - 1, j] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                case 45:
                                    if ((NonMaskSuppress[i - 1, j - 1] > UpperThres) || (NonMaskSuppress[i + 1, j + 1] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                case 90:
                                    if ((NonMaskSuppress[i, j - 1] > UpperThres) || (NonMaskSuppress[i, j + 1] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                case 135:
                                    if ((NonMaskSuppress[i - 1, j + 1] > UpperThres) || (NonMaskSuppress[i + 1, j - 1] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                default:
                                    break;
                            }
                        hilo3++;
                    }
                t3 = false;
            })).Start();
            (new Task(() =>
            {
                for (int i = (ans.ancho - 1) / 2; i < ans.ancho - 1; i++)
                    for (int j = (ans.alto - 1) / 2; j < ans.alto - 1; j++)
                    {
                        if (NonMaskSuppress[i, j] > UpperThres)
                            ans.R[i, j] = 255;
                        else if (NonMaskSuppress[i, j] < LowerThres) //Lower thres = Upper thres / 3
                            ans.R[i, j] = 0;
                        else
                            switch (img.B[i, j])
                            {
                                case 0:
                                    if ((NonMaskSuppress[i - 1, j] > UpperThres) || (NonMaskSuppress[i - 1, j] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                case 45:
                                    if ((NonMaskSuppress[i - 1, j - 1] > UpperThres) || (NonMaskSuppress[i + 1, j + 1] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                case 90:
                                    if ((NonMaskSuppress[i, j - 1] > UpperThres) || (NonMaskSuppress[i, j + 1] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                case 135:
                                    if ((NonMaskSuppress[i - 1, j + 1] > UpperThres) || (NonMaskSuppress[i + 1, j - 1] > UpperThres))
                                        ans.R[i, j] = 255;
                                    else
                                        ans.R[i, j] = 0;
                                    break;
                                default:
                                    break;
                            }
                        hilo4++;
                    }
                t4 = false;
            })).Start();
            while (t1 || t2 || t3 || t4) { }
            //////////////////////////////////////////////////////////////////////////////
            
            ans.G = ans.B = ans.R;

            operaciones = completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;
            imagenes.Add(ans);
            NuevaImagen = true;
        }

        public void Fourier()
        {
            Imagen img = imagenes.Last();
            Imagen ans = new Imagen(img.ancho, img.alto);
            
            operaciones = 3 * img.ancho * img.alto;
            completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;

            state = "Obteniendo dominio en el espectro de frecuencia";

            int menor =int.MaxValue, mayor = int.MinValue;
            for (int i = 0; i < ans.ancho; i++)
                for (int j = 0; j < ans.alto; j++)
                {
                    int x = ans.R[i, j] = PixelFourier(img, i, j);
                    if (x < menor)
                        menor = x;
                    if (x > mayor)
                        mayor = x;
                    hilo1++;
                }
            
           
            //Reescalamiento(ans, menor, mayor);
            int[,] G = new int[img.ancho,img.alto];
            for (int i = 0; i < ans.ancho; i++)
                for (int j = 0; j < ans.alto; j++)
                {
                    G[(i + (img.ancho / 2)) % img.ancho, (j + (img.alto / 2)) % img.alto] = ans.R[i, j];
                }
            ans.G = ans.B = ans.R = G;

            operaciones = completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;
            imagenes.Add(ans);
            NuevaImagen = true;
        }
        int PixelFourier(Imagen img, double u, double v)
        {
            double cos = 0;
            double sin = 0;
            for (int x = 0; x < img.ancho; x++)
                for (int y = 0; y < img.alto; y++)
                {
                    double w = -2 * Math.PI * (((u * x) / img.ancho) + ((v * y) / img.alto));
                    int c = img.R[x, y];
                    cos += c * Math.Cos(w);
                    sin += c * Math.Sin(w);
                }
            double K = (double)(img.ancho * img.alto);
            //cos = cos / K;
            //sin = sin / K;
            int n = (int)Math.Round((Math.Sqrt(  (cos * cos / K) + (sin * sin / K)   )), 0);
            return n;
        }
 
        public void FourierInversa()
        {
            Imagen img = imagenes.Last();
            double[,] ans = new double[img.ancho, img.alto];

            operaciones = 3 * img.ancho * img.alto;
            hilo1 = hilo2 = hilo3 = hilo4 = completado = 0;

            state = "Regresando al dominio del espacio";
            //Fourier Inversa
            double mayor = double.MinValue, menor = double.MaxValue;
            for (int i = 0; i < img.ancho; i++)
                for (int j = 0; j < img.alto; j++)
                {
                    double c = ans[i, j] = PixelFourierInversa(img, i, j);
                    if (c < menor)
                        menor = c;
                    if (c > mayor)
                        mayor = c;
                    hilo1++;
                }

            double[,] G = new double[img.ancho, img.alto];


            //Reposicionamiento
            for (int x = 0; x < img.ancho; x++)
                for (int y = 0; y < img.alto; y++)
                {
                    G[(x + (img.ancho / 2)) % img.ancho, (y + (img.alto / 2)) % img.alto] = ans[x, y];
                    hilo1++;
                }
            Imagen answer = new Imagen(img.ancho, img.alto);
            //Reescalar
            for (int x = 0; x < img.ancho; x++)
                for (int y = 0; y < img.alto; y++)
                {
                    answer.R[x, y] = (int)((255 / (double)(mayor - menor)) * ((double)G[x, y] - menor));
                    hilo1++;
                }

            answer.G = answer.B = answer.R;


            operaciones = completado = hilo1 = 0 ;
            imagenes.Add(answer);
            NuevaImagen = true;
        }
        double PixelFourierInversa(Imagen img, double x, double y)
        {
            double cos = 0;
            double sin = 0;
            for (int u = 0; u < img.ancho; u++)
                for (int v = 0; v < img.alto; v++)
                {
                    double w = 2 * Math.PI * (((u * x) / img.ancho) + ((v * y) / img.alto));
                    int c = img.R[u, v];
                    cos += c * Math.Cos(w);
                    sin += c * Math.Sin(w);
                }
            int M = img.ancho * img.alto;
            double n = Math.Log( 1 + Math.Round(Math.Sqrt( (cos * cos) + ( sin * sin) ), 0 ) );
            return n ;
        }


        public void AgregarRuidoBlancoNegro(double porcentaje)
        {
            Imagen img = imagenes.Last();
            Imagen ans = new Imagen(img.ancho, img.alto);

            operaciones = img.ancho * img.alto;
            completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;

            t1 = true; t2 = true; t3 = true; t4 = true;
            (new Task(() =>
            {
                Random j = new Random();
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = 0; y < img.alto / 2; y++)
                    {
                        int n = j.Next(256);
                        ans.R[x, y] = (int)(img.R[x, y] * (1 - porcentaje) + n * porcentaje);
                        ans.G[x, y] = (int)(img.G[x, y] * (1 - porcentaje) + n * porcentaje);
                        ans.B[x, y] = (int)(img.B[x, y] * (1 - porcentaje) + n * porcentaje);

                        hilo1++;
                    }
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                Random j = new Random();
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                    {
                        int n = j.Next(256);
                        ans.R[x, y] = (int)(img.R[x, y] * (1 - porcentaje) + n * porcentaje);
                        ans.G[x, y] = (int)(img.G[x, y] * (1 - porcentaje) + n * porcentaje);
                        ans.B[x, y] = (int)(img.B[x, y] * (1 - porcentaje) + n * porcentaje);
                        hilo2++;
                    }
                t2 = false;
            })).Start();
            (new Task(() =>
            {
                Random j = new Random();
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                    {
                        int n = j.Next(256);
                        ans.R[x, y] = (int)(img.R[x, y] * (1 - porcentaje) + n * porcentaje);
                        ans.G[x, y] = (int)(img.G[x, y] * (1 - porcentaje) + n * porcentaje);
                        ans.B[x, y] = (int)(img.B[x, y] * (1 - porcentaje) + n * porcentaje);
                        hilo3++;
                    }
                t3 = false;
            })).Start();
            (new Task(() =>
            {
                Random j = new Random();
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                    {
                        int n = j.Next(256);
                        ans.R[x, y] = (int)(img.R[x, y] * (1 - porcentaje) + n * porcentaje);
                        ans.G[x, y] = (int)(img.G[x, y] * (1 - porcentaje) + n * porcentaje);
                        ans.B[x, y] = (int)(img.B[x, y] * (1 - porcentaje) + n * porcentaje);
                        hilo4++;
                    }
                t4 = false;
            })).Start();
            while (t1 || t2 || t3 || t4) { }

            operaciones = completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;
            imagenes.Add(ans);
            NuevaImagen = true;

        }
        public void AgregarRuidoColor(double porcentaje)
        {
            Imagen img = imagenes.Last();
            Imagen ans = new Imagen(img.ancho, img.alto);

            operaciones = img.ancho * img.alto;
            completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;

            t1 = true; t2 = true; t3 = true; t4 = true;
            (new Task(() =>
            {
                Random j = new Random();
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = 0; y < img.alto / 2; y++)
                    {
                        ans.R[x, y] = (int)(img.R[x, y] * (1 - porcentaje) + j.Next(256) * porcentaje);
                        ans.G[x, y] = (int)(img.G[x, y] * (1 - porcentaje) + j.Next(256) * porcentaje);
                        ans.B[x, y] = (int)(img.B[x, y] * (1 - porcentaje) + j.Next(256) * porcentaje);

                        hilo1++;
                    }
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                Random j = new Random();
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                    {
                        int n = j.Next(256);
                        ans.R[x, y] = (int)(img.R[x, y] * (1 - porcentaje) + j.Next(256) * porcentaje);
                        ans.G[x, y] = (int)(img.G[x, y] * (1 - porcentaje) + j.Next(256) * porcentaje);
                        ans.B[x, y] = (int)(img.B[x, y] * (1 - porcentaje) + j.Next(256) * porcentaje);
                        hilo2++;
                    }
                t2 = false;
            })).Start();
            (new Task(() =>
            {
                Random j = new Random();
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                    {
                        int n = j.Next(256);
                        ans.R[x, y] = (int)(img.R[x, y] * (1 - porcentaje) + j.Next(256) * porcentaje);
                        ans.G[x, y] = (int)(img.G[x, y] * (1 - porcentaje) + j.Next(256) * porcentaje);
                        ans.B[x, y] = (int)(img.B[x, y] * (1 - porcentaje) + j.Next(256) * porcentaje);
                        hilo3++;
                    }
                t3 = false;
            })).Start();
            (new Task(() =>
            {
                Random j = new Random();
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                    {
                        int n = j.Next(256);
                        ans.R[x, y] = (int)(img.R[x, y] * (1 - porcentaje) + j.Next(256) * porcentaje);
                        ans.G[x, y] = (int)(img.G[x, y] * (1 - porcentaje) + j.Next(256) * porcentaje);
                        ans.B[x, y] = (int)(img.B[x, y] * (1 - porcentaje) + j.Next(256) * porcentaje);
                        hilo4++;
                    }
                t4 = false;
            })).Start();
            while (t1 || t2 || t3 || t4) { }

            operaciones = completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;
            imagenes.Add(ans);
            NuevaImagen = true;
        }

        public void MezclarOnda(double porcentaje, int desface)
        {
            Imagen img = imagenes.Last();
            Imagen ans = new Imagen(img.ancho, img.alto);

            operaciones = img.ancho * img.alto;
            completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;
            int n = 0;
            t1 = true; t2 = true; t3 = true; t4 = true;
            (new Task(() =>
            {

                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = 0; y < img.alto / 2; y++)
                    {
                        ans.R[x, y] = (int)(img.R[x, y] * (1 - porcentaje) + (128 * Math.Sin(desface * x) + 128) * porcentaje);
                        ans.G[x, y] = (int)(img.G[x, y] * (1 - porcentaje) + (128 * Math.Sin(desface * x) + 128) * porcentaje);
                        ans.B[x, y] = (int)(img.B[x, y] * (1 - porcentaje) + (128 * Math.Sin(desface * x) + 128) * porcentaje);

                        hilo1++;
                    }
                t1 = false;
            })).Start();
            (new Task(() =>
            {
                Random j = new Random();
                for (int x = 0; x < ans.ancho / 2; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                    {
                        ans.R[x, y] = (int)(img.R[x, y] * (1 - porcentaje) + (128 * Math.Sin(desface * x) + 128) * porcentaje);
                        ans.G[x, y] = (int)(img.G[x, y] * (1 - porcentaje) + (128 * Math.Sin(desface * x) + 128) * porcentaje);
                        ans.B[x, y] = (int)(img.B[x, y] * (1 - porcentaje) + (128 * Math.Sin(desface * x) + 128) * porcentaje);
                        hilo2++;
                    }
                t2 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = 0; y < ans.alto / 2; y++)
                    {
                        ans.R[x, y] = (int)(img.R[x, y] * (1 - porcentaje) + (128 * Math.Sin(desface * x) + 128) * porcentaje);
                        ans.G[x, y] = (int)(img.G[x, y] * (1 - porcentaje) + (128 * Math.Sin(desface * x) + 128) * porcentaje);
                        ans.B[x, y] = (int)(img.B[x, y] * (1 - porcentaje) + (128 * Math.Sin(desface * x) + 128) * porcentaje);
                        hilo3++;
                    }
                t3 = false;
            })).Start();
            (new Task(() =>
            {
                for (int x = ans.ancho / 2; x < ans.ancho; x++)
                    for (int y = ans.alto / 2; y < ans.alto; y++)
                    {
                        ans.R[x, y] = (int)(img.R[x, y] * (1 - porcentaje) + (128 * Math.Sin(desface * x) + 128) * porcentaje);
                        ans.G[x, y] = (int)(img.G[x, y] * (1 - porcentaje) + (128 * Math.Sin(desface * x) + 128) * porcentaje);
                        ans.B[x, y] = (int)(img.B[x, y] * (1 - porcentaje) + (128 * Math.Sin(desface * x) + 128) * porcentaje);
                        hilo4++;
                    }
                t4 = false;
            })).Start();
            while (t1 || t2 || t3 || t4) { }

            operaciones = completado = hilo1 = hilo2 = hilo3 = hilo4 = 0;
            imagenes.Add(ans);
            NuevaImagen = true;
        }
    }
}

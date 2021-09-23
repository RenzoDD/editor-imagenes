using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor_de_imagenes
{
    public partial class Principal : Form
    {
        EdicionImagenes editor;
        public Principal()
        {
            InitializeComponent();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Imagen myForm = new Imagen();
            //myForm.TopLevel = false;
            //myForm.AutoScroll = true;
            //WorkBench.Controls.Add(myForm);
            //myForm.Show();
        }

        private void AparienciaImagen(object sender, EventArgs e)
        {
            estrechoToolStripMenuItem.Checked = false;
            centroToolStripMenuItem.Checked = false;
            zoomToolStripMenuItem.Checked = false;
            ToolStripMenuItem obj = sender as ToolStripMenuItem;
            obj.Checked = true;
            if (obj.Text == "Estrecho")
                imagen.SizeMode = PictureBoxSizeMode.StretchImage;
            else if (obj.Text == "Centro")
                imagen.SizeMode = PictureBoxSizeMode.CenterImage;
            else if (obj.Text == "Zoom")
                imagen.SizeMode = PictureBoxSizeMode.Zoom;
        }

        

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            editor = new EdicionImagenes();
            editor.CrearNuevaImagen(101, 101);
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog obj = new OpenFileDialog();

            obj.Filter = "Archivos JPG (*.jpg)|*.jpg|Archivos PNG (*.png)|*.png|Archivos BMP (*.bmp)|*.bmp|All files (*.*)|*.*";
            obj.FilterIndex = 1;

            if (obj.ShowDialog() == DialogResult.OK)
            {
                (new Task(() => { editor = new EdicionImagenes(obj.FileName); })).Start();
            }
        }

        private void btnBlancoNegro_Click(object sender, EventArgs e)
        {
            if (editor != null)
            {
                Task tarea = new Task(() => { editor.BlancoNegro(); });
                tarea.Start();
            }
        }


        private void btnLaplace_Click(object sender, EventArgs e)
        {
            if (editor != null)
            {
                Task tarea = new Task(() => { editor.Laplace(); });
                tarea.Start();
            }
        }

        private void btnSobel_Click(object sender, EventArgs e)
        {
            if (editor != null)
            {

                Task tarea = new Task(() => { editor.Sobel(); });
                tarea.Start();
            }
        }
        private void btnCanny_Click(object sender, EventArgs e)
        {
            int n = tbPorcentaje.Value;
            if (editor != null)
            {
                
                Task tarea = new Task(() => { editor.Canny(n / 2); });
                tarea.Start();
            }
        }

        private void carga_Tick(object sender, EventArgs e)
        {
            if (editor != null)
            {
                if (editor.NuevaImagen)
                {
                    (new Task(() => 
                    {
                        Bitmap bmp = editor.LastImage();

                        Size sz = bmp.Size;
                        Bitmap zoomed = (Bitmap)imagen.Image;
                        if (zoomed != null) zoomed.Dispose();

                        float zoom = (float)(10 / 4f + 1);
                        zoomed = new Bitmap((int)(sz.Width * zoom), (int)(sz.Height * zoom));

                        using (Graphics g = Graphics.FromImage(zoomed))
                        {
                            g.InterpolationMode = InterpolationMode.NearestNeighbor;
                            g.DrawImage(bmp, new Rectangle(Point.Empty, zoomed.Size));
                        }
                        imagen.Image = zoomed;

                    })).Start();





                    









                }

                lblEstado.Text = editor.state;

                int max = editor.operaciones;
                int val = editor.completado + editor.hilo1 + editor.hilo2 + editor.hilo3 + editor.hilo4;

                if (max >= val)
                {
                    progressBar1.Maximum = max;
                    progressBar1.Value = val;
                }



                label2.Text = Math.Round(((editor.completado + editor.hilo1 + editor.hilo2 + editor.hilo3 + editor.hilo4) / (float)editor.operaciones) * 100, 2).ToString() + "%";
                
            }
        }

        private void btnMediaColor_Click(object sender, EventArgs e)
        {
            if (editor != null)
            {
                Task tarea = new Task(() => { editor.MediaColor(); });
                tarea.Start();
            }
        }

        private void btnMediaBN_Click(object sender, EventArgs e)
        {
            if (editor != null)
            {
                Task tarea = new Task(() => { editor.MediaBlancoNegro(); });
                tarea.Start();
            }
        }

        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editor != null)
                editor.Deshacer();
        }

        private void ShortCuts(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
                deshacerToolStripMenuItem_Click(null, null);
        }

        private void btnFourier_Click(object sender, EventArgs e)
        {
            if (editor != null)
            {
                Task tarea = new Task(() => { editor.Fourier(); });
                tarea.Start();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            imagen.Image.Save(@"C:\Users\Renzo Diaz\Desktop\dimgS.png");
        }

        private void btnFourierInversa_Click(object sender, EventArgs e)
        {
            if (editor != null)
            {
                Task tarea = new Task(() => { editor.FourierInversa(); });
                tarea.Start();
            }

        }

        private void fILTROSToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void graficador_Tick(object sender, EventArgs e)
        {

        }

        private void Principal_Load(object sender, EventArgs e)
        {

        }

        private void btnRuidoBlancoNegro_Click(object sender, EventArgs e)
        {
            editor.AgregarRuidoBlancoNegro((double)tbPorcentaje.Value / 100);
        }

        private void btnRuidoColor_Click(object sender, EventArgs e)
        {
            editor.AgregarRuidoColor((double)tbPorcentaje.Value / 100);
        }

        private void tbPorcentaje_Scroll(object sender, EventArgs e)
        {
            lblPorcentaje.Text = tbPorcentaje.Value.ToString() + "%";
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.MezclarOnda((double)tbPorcentaje.Value / 100, tbPorcentaje.Value * 2 );
        }

        private void imagen_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            (new Task(() => { editor = new EdicionImagenes(files[0]); })).Start();


        }

        private void imagen_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;

        }

        private void btnFourier_Click_1(object sender, EventArgs e)
        {

        }
    }
}

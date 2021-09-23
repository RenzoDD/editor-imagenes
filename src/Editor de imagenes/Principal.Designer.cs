namespace Editor_de_imagenes
{
    partial class Principal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aRCHIVOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNuevo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGuardar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGuardarComo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.vERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estrechoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fILTROSToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deshacerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBlancoNegro = new System.Windows.Forms.ToolStripMenuItem();
            this.ruidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRuidoBlancoNegro = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRuidoColor = new System.Windows.Forms.ToolStripMenuItem();
            this.mascarasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMedia = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMediaBN = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMediaColor = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLaplace = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSobel = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCanny = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFourier = new System.Windows.Forms.ToolStripMenuItem();
            this.transformarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFourierInversa = new System.Windows.Forms.ToolStripMenuItem();
            this.agregarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aYUDAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graficador = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.carga = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPorcentaje = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPorcentaje = new System.Windows.Forms.TrackBar();
            this.imagen = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPorcentaje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagen)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aRCHIVOToolStripMenuItem,
            this.vERToolStripMenuItem,
            this.fILTROSToolStripMenuItem1,
            this.aYUDAToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(500, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShortCuts);
            // 
            // aRCHIVOToolStripMenuItem
            // 
            this.aRCHIVOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnAbrir,
            this.btnGuardar,
            this.btnGuardarComo,
            this.btnSalir});
            this.aRCHIVOToolStripMenuItem.Name = "aRCHIVOToolStripMenuItem";
            this.aRCHIVOToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.aRCHIVOToolStripMenuItem.Text = "ARCHIVO";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(152, 22);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnAbrir
            // 
            this.btnAbrir.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrir.Image")));
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(152, 22);
            this.btnAbrir.Text = "Abrir";
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(152, 22);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnGuardarComo
            // 
            this.btnGuardarComo.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarComo.Image")));
            this.btnGuardarComo.Name = "btnGuardarComo";
            this.btnGuardarComo.Size = new System.Drawing.Size(152, 22);
            this.btnGuardarComo.Text = "Guardar Como";
            // 
            // btnSalir
            // 
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(152, 22);
            this.btnSalir.Text = "Salir";
            // 
            // vERToolStripMenuItem
            // 
            this.vERToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.estrechoToolStripMenuItem,
            this.centroToolStripMenuItem,
            this.zoomToolStripMenuItem});
            this.vERToolStripMenuItem.Name = "vERToolStripMenuItem";
            this.vERToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.vERToolStripMenuItem.Text = "VER";
            // 
            // estrechoToolStripMenuItem
            // 
            this.estrechoToolStripMenuItem.Name = "estrechoToolStripMenuItem";
            this.estrechoToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.estrechoToolStripMenuItem.Text = "Estrecho";
            this.estrechoToolStripMenuItem.Click += new System.EventHandler(this.AparienciaImagen);
            // 
            // centroToolStripMenuItem
            // 
            this.centroToolStripMenuItem.Name = "centroToolStripMenuItem";
            this.centroToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.centroToolStripMenuItem.Text = "Centro";
            this.centroToolStripMenuItem.Click += new System.EventHandler(this.AparienciaImagen);
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.Checked = true;
            this.zoomToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.zoomToolStripMenuItem.Text = "Zoom";
            this.zoomToolStripMenuItem.Click += new System.EventHandler(this.AparienciaImagen);
            // 
            // fILTROSToolStripMenuItem1
            // 
            this.fILTROSToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deshacerToolStripMenuItem,
            this.btnBlancoNegro,
            this.ruidoToolStripMenuItem,
            this.mascarasToolStripMenuItem,
            this.btnFourier,
            this.agregarToolStripMenuItem});
            this.fILTROSToolStripMenuItem1.Name = "fILTROSToolStripMenuItem1";
            this.fILTROSToolStripMenuItem1.Size = new System.Drawing.Size(61, 20);
            this.fILTROSToolStripMenuItem1.Text = "FILTROS";
            this.fILTROSToolStripMenuItem1.Click += new System.EventHandler(this.fILTROSToolStripMenuItem1_Click);
            // 
            // deshacerToolStripMenuItem
            // 
            this.deshacerToolStripMenuItem.Name = "deshacerToolStripMenuItem";
            this.deshacerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deshacerToolStripMenuItem.Text = "Deshacer";
            this.deshacerToolStripMenuItem.Click += new System.EventHandler(this.deshacerToolStripMenuItem_Click);
            // 
            // btnBlancoNegro
            // 
            this.btnBlancoNegro.Name = "btnBlancoNegro";
            this.btnBlancoNegro.Size = new System.Drawing.Size(180, 22);
            this.btnBlancoNegro.Text = "Blanco y Negro";
            this.btnBlancoNegro.Click += new System.EventHandler(this.btnBlancoNegro_Click);
            // 
            // ruidoToolStripMenuItem
            // 
            this.ruidoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRuidoBlancoNegro,
            this.btnRuidoColor});
            this.ruidoToolStripMenuItem.Name = "ruidoToolStripMenuItem";
            this.ruidoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ruidoToolStripMenuItem.Text = "Agregar Ruido";
            // 
            // btnRuidoBlancoNegro
            // 
            this.btnRuidoBlancoNegro.Name = "btnRuidoBlancoNegro";
            this.btnRuidoBlancoNegro.Size = new System.Drawing.Size(155, 22);
            this.btnRuidoBlancoNegro.Text = "Blanco y Negro";
            this.btnRuidoBlancoNegro.Click += new System.EventHandler(this.btnRuidoBlancoNegro_Click);
            // 
            // btnRuidoColor
            // 
            this.btnRuidoColor.Name = "btnRuidoColor";
            this.btnRuidoColor.Size = new System.Drawing.Size(155, 22);
            this.btnRuidoColor.Text = "Color";
            this.btnRuidoColor.Click += new System.EventHandler(this.btnRuidoColor_Click);
            // 
            // mascarasToolStripMenuItem
            // 
            this.mascarasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMedia,
            this.btnLaplace,
            this.btnSobel,
            this.btnCanny});
            this.mascarasToolStripMenuItem.Name = "mascarasToolStripMenuItem";
            this.mascarasToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.mascarasToolStripMenuItem.Text = "Mascaras";
            // 
            // btnMedia
            // 
            this.btnMedia.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMediaBN,
            this.btnMediaColor});
            this.btnMedia.Name = "btnMedia";
            this.btnMedia.Size = new System.Drawing.Size(114, 22);
            this.btnMedia.Text = "Media";
            // 
            // btnMediaBN
            // 
            this.btnMediaBN.Name = "btnMediaBN";
            this.btnMediaBN.Size = new System.Drawing.Size(155, 22);
            this.btnMediaBN.Text = "Blanco y Negro";
            this.btnMediaBN.Click += new System.EventHandler(this.btnMediaBN_Click);
            // 
            // btnMediaColor
            // 
            this.btnMediaColor.Name = "btnMediaColor";
            this.btnMediaColor.Size = new System.Drawing.Size(155, 22);
            this.btnMediaColor.Text = "Color";
            this.btnMediaColor.Click += new System.EventHandler(this.btnMediaColor_Click);
            // 
            // btnLaplace
            // 
            this.btnLaplace.Name = "btnLaplace";
            this.btnLaplace.Size = new System.Drawing.Size(114, 22);
            this.btnLaplace.Text = "Laplace";
            this.btnLaplace.Click += new System.EventHandler(this.btnLaplace_Click);
            // 
            // btnSobel
            // 
            this.btnSobel.Name = "btnSobel";
            this.btnSobel.Size = new System.Drawing.Size(114, 22);
            this.btnSobel.Text = "Sobel";
            this.btnSobel.Click += new System.EventHandler(this.btnSobel_Click);
            // 
            // btnCanny
            // 
            this.btnCanny.Name = "btnCanny";
            this.btnCanny.Size = new System.Drawing.Size(114, 22);
            this.btnCanny.Text = "Canny";
            this.btnCanny.Click += new System.EventHandler(this.btnCanny_Click);
            // 
            // btnFourier
            // 
            this.btnFourier.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transformarToolStripMenuItem,
            this.btnFourierInversa});
            this.btnFourier.Name = "btnFourier";
            this.btnFourier.Size = new System.Drawing.Size(180, 22);
            this.btnFourier.Text = "Fourier";
            this.btnFourier.Click += new System.EventHandler(this.btnFourier_Click_1);
            // 
            // transformarToolStripMenuItem
            // 
            this.transformarToolStripMenuItem.Name = "transformarToolStripMenuItem";
            this.transformarToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.transformarToolStripMenuItem.Text = "Transformada";
            this.transformarToolStripMenuItem.Click += new System.EventHandler(this.btnFourier_Click);
            // 
            // btnFourierInversa
            // 
            this.btnFourierInversa.Name = "btnFourierInversa";
            this.btnFourierInversa.Size = new System.Drawing.Size(186, 22);
            this.btnFourierInversa.Text = "Transformada Inversa";
            this.btnFourierInversa.Click += new System.EventHandler(this.btnFourierInversa_Click);
            // 
            // agregarToolStripMenuItem
            // 
            this.agregarToolStripMenuItem.Name = "agregarToolStripMenuItem";
            this.agregarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.agregarToolStripMenuItem.Text = "Agregar";
            this.agregarToolStripMenuItem.Click += new System.EventHandler(this.agregarToolStripMenuItem_Click);
            // 
            // aYUDAToolStripMenuItem
            // 
            this.aYUDAToolStripMenuItem.Name = "aYUDAToolStripMenuItem";
            this.aYUDAToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.aYUDAToolStripMenuItem.Text = "AYUDA";
            // 
            // graficador
            // 
            this.graficador.Enabled = true;
            this.graficador.Interval = 500;
            this.graficador.Tick += new System.EventHandler(this.graficador_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.progressBar1.Location = new System.Drawing.Point(317, 6);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(142, 18);
            this.progressBar1.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(263, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Progreso:";
            // 
            // carga
            // 
            this.carga.Enabled = true;
            this.carga.Interval = 1;
            this.carga.Tick += new System.EventHandler(this.carga_Tick);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(461, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "0,00%";
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panel1.Controls.Add(this.lblPorcentaje);
            this.panel1.Controls.Add(this.lblEstado);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 296);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 28);
            this.panel1.TabIndex = 25;
            this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.imagen_DragDrop);
            this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.imagen_DragEnter);
            // 
            // lblPorcentaje
            // 
            this.lblPorcentaje.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPorcentaje.AutoSize = true;
            this.lblPorcentaje.BackColor = System.Drawing.Color.Transparent;
            this.lblPorcentaje.ForeColor = System.Drawing.Color.White;
            this.lblPorcentaje.Location = new System.Drawing.Point(9, 8);
            this.lblPorcentaje.Name = "lblPorcentaje";
            this.lblPorcentaje.Size = new System.Drawing.Size(27, 13);
            this.lblPorcentaje.TabIndex = 27;
            this.lblPorcentaje.Text = "50%";
            // 
            // lblEstado
            // 
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEstado.AutoSize = true;
            this.lblEstado.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado.ForeColor = System.Drawing.Color.White;
            this.lblEstado.Location = new System.Drawing.Point(91, 8);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(36, 13);
            this.lblEstado.TabIndex = 26;
            this.lblEstado.Text = "Vacío";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(42, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Estado:";
            // 
            // tbPorcentaje
            // 
            this.tbPorcentaje.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbPorcentaje.LargeChange = 10;
            this.tbPorcentaje.Location = new System.Drawing.Point(0, 24);
            this.tbPorcentaje.Maximum = 100;
            this.tbPorcentaje.Name = "tbPorcentaje";
            this.tbPorcentaje.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbPorcentaje.Size = new System.Drawing.Size(45, 272);
            this.tbPorcentaje.TabIndex = 26;
            this.tbPorcentaje.TickFrequency = 10;
            this.tbPorcentaje.Value = 50;
            this.tbPorcentaje.Scroll += new System.EventHandler(this.tbPorcentaje_Scroll);
            // 
            // imagen
            // 
            this.imagen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imagen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagen.Location = new System.Drawing.Point(45, 24);
            this.imagen.Name = "imagen";
            this.imagen.Size = new System.Drawing.Size(455, 272);
            this.imagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imagen.TabIndex = 27;
            this.imagen.TabStop = false;
            this.imagen.DragDrop += new System.Windows.Forms.DragEventHandler(this.imagen_DragDrop);
            this.imagen.DragEnter += new System.Windows.Forms.DragEventHandler(this.imagen_DragEnter);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(73)))), ((int)(((byte)(106)))));
            this.ClientSize = new System.Drawing.Size(500, 324);
            this.Controls.Add(this.imagen);
            this.Controls.Add(this.tbPorcentaje);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(516, 363);
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Principal_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShortCuts);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPorcentaje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aRCHIVOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fILTROSToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem estrechoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnNuevo;
        private System.Windows.Forms.ToolStripMenuItem btnAbrir;
        private System.Windows.Forms.ToolStripMenuItem btnGuardar;
        private System.Windows.Forms.ToolStripMenuItem btnGuardarComo;
        private System.Windows.Forms.ToolStripMenuItem btnSalir;
        private System.Windows.Forms.ToolStripMenuItem aYUDAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnBlancoNegro;
        private System.Windows.Forms.Timer graficador;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer carga;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem deshacerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnFourier;
        private System.Windows.Forms.ToolStripMenuItem btnFourierInversa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem ruidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnRuidoBlancoNegro;
        private System.Windows.Forms.ToolStripMenuItem btnRuidoColor;
        private System.Windows.Forms.TrackBar tbPorcentaje;
        private System.Windows.Forms.PictureBox imagen;
        private System.Windows.Forms.Label lblPorcentaje;
        private System.Windows.Forms.ToolStripMenuItem mascarasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnMedia;
        private System.Windows.Forms.ToolStripMenuItem btnMediaColor;
        private System.Windows.Forms.ToolStripMenuItem btnMediaBN;
        private System.Windows.Forms.ToolStripMenuItem btnLaplace;
        private System.Windows.Forms.ToolStripMenuItem btnSobel;
        private System.Windows.Forms.ToolStripMenuItem btnCanny;
        private System.Windows.Forms.ToolStripMenuItem transformarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem agregarToolStripMenuItem;
    }
}


namespace ProbandoMap
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
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
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.rbNodoDestino = new System.Windows.Forms.RadioButton();
            this.rbNodoOrigen = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLong = new System.Windows.Forms.TextBox();
            this.txtLat = new System.Windows.Forms.TextBox();
            this.btnIzquierda = new System.Windows.Forms.Button();
            this.btnDerecha = new System.Windows.Forms.Button();
            this.btnAbajo = new System.Windows.Forms.Button();
            this.btnArriba = new System.Windows.Forms.Button();
            this.btnAlejar = new System.Windows.Forms.Button();
            this.btnAcercar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.rbNodoDestino);
            this.panel1.Controls.Add(this.rbNodoOrigen);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtLong);
            this.panel1.Controls.Add(this.txtLat);
            this.panel1.Controls.Add(this.btnIzquierda);
            this.panel1.Controls.Add(this.btnDerecha);
            this.panel1.Controls.Add(this.btnAbajo);
            this.panel1.Controls.Add(this.btnArriba);
            this.panel1.Controls.Add(this.btnAlejar);
            this.panel1.Controls.Add(this.btnAcercar);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 611);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(53, 469);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Dijkstra";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rbNodoDestino
            // 
            this.rbNodoDestino.AutoSize = true;
            this.rbNodoDestino.Location = new System.Drawing.Point(53, 421);
            this.rbNodoDestino.Name = "rbNodoDestino";
            this.rbNodoDestino.Size = new System.Drawing.Size(90, 17);
            this.rbNodoDestino.TabIndex = 12;
            this.rbNodoDestino.TabStop = true;
            this.rbNodoDestino.Text = "Nodo Destino";
            this.rbNodoDestino.UseVisualStyleBackColor = true;
            this.rbNodoDestino.CheckedChanged += new System.EventHandler(this.rbNodoDestino_CheckedChanged);
            // 
            // rbNodoOrigen
            // 
            this.rbNodoOrigen.AutoSize = true;
            this.rbNodoOrigen.Location = new System.Drawing.Point(53, 365);
            this.rbNodoOrigen.Name = "rbNodoOrigen";
            this.rbNodoOrigen.Size = new System.Drawing.Size(85, 17);
            this.rbNodoOrigen.TabIndex = 11;
            this.rbNodoOrigen.TabStop = true;
            this.rbNodoOrigen.Text = "Nodo Origen";
            this.rbNodoOrigen.UseVisualStyleBackColor = true;
            this.rbNodoOrigen.CheckedChanged += new System.EventHandler(this.rbNodoOrigen_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 266);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Longitud";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Latitud";
            // 
            // txtLong
            // 
            this.txtLong.Location = new System.Drawing.Point(26, 291);
            this.txtLong.Name = "txtLong";
            this.txtLong.Size = new System.Drawing.Size(143, 20);
            this.txtLong.TabIndex = 8;
            // 
            // txtLat
            // 
            this.txtLat.Location = new System.Drawing.Point(26, 230);
            this.txtLat.Name = "txtLat";
            this.txtLat.Size = new System.Drawing.Size(143, 20);
            this.txtLat.TabIndex = 7;
            // 
            // btnIzquierda
            // 
            this.btnIzquierda.Location = new System.Drawing.Point(53, 114);
            this.btnIzquierda.Name = "btnIzquierda";
            this.btnIzquierda.Size = new System.Drawing.Size(29, 23);
            this.btnIzquierda.TabIndex = 5;
            this.btnIzquierda.Text = "o";
            this.btnIzquierda.UseVisualStyleBackColor = true;
            this.btnIzquierda.Click += new System.EventHandler(this.btnIzquierda_Click);
            // 
            // btnDerecha
            // 
            this.btnDerecha.Location = new System.Drawing.Point(110, 114);
            this.btnDerecha.Name = "btnDerecha";
            this.btnDerecha.Size = new System.Drawing.Size(29, 23);
            this.btnDerecha.TabIndex = 4;
            this.btnDerecha.Text = "o";
            this.btnDerecha.UseVisualStyleBackColor = true;
            this.btnDerecha.Click += new System.EventHandler(this.btnDerecha_Click);
            // 
            // btnAbajo
            // 
            this.btnAbajo.Location = new System.Drawing.Point(82, 138);
            this.btnAbajo.Name = "btnAbajo";
            this.btnAbajo.Size = new System.Drawing.Size(29, 23);
            this.btnAbajo.TabIndex = 3;
            this.btnAbajo.Text = "o";
            this.btnAbajo.UseVisualStyleBackColor = true;
            this.btnAbajo.Click += new System.EventHandler(this.btnAbajo_Click);
            // 
            // btnArriba
            // 
            this.btnArriba.Location = new System.Drawing.Point(82, 89);
            this.btnArriba.Name = "btnArriba";
            this.btnArriba.Size = new System.Drawing.Size(29, 23);
            this.btnArriba.TabIndex = 2;
            this.btnArriba.Text = "o";
            this.btnArriba.UseVisualStyleBackColor = true;
            this.btnArriba.Click += new System.EventHandler(this.btnArriba_Click);
            // 
            // btnAlejar
            // 
            this.btnAlejar.Location = new System.Drawing.Point(108, 45);
            this.btnAlejar.Name = "btnAlejar";
            this.btnAlejar.Size = new System.Drawing.Size(40, 23);
            this.btnAlejar.TabIndex = 1;
            this.btnAlejar.Text = "-";
            this.btnAlejar.UseVisualStyleBackColor = true;
            this.btnAlejar.Click += new System.EventHandler(this.btnAlejar_Click);
            // 
            // btnAcercar
            // 
            this.btnAcercar.Location = new System.Drawing.Point(47, 45);
            this.btnAcercar.Name = "btnAcercar";
            this.btnAcercar.Size = new System.Drawing.Size(40, 23);
            this.btnAcercar.TabIndex = 0;
            this.btnAcercar.Text = "+";
            this.btnAcercar.UseVisualStyleBackColor = true;
            this.btnAcercar.Click += new System.EventHandler(this.btnAcercar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(16, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 149);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controles";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(198, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(971, 611);
            this.panel2.TabIndex = 1;
            this.panel2.Click += new System.EventHandler(this.panel2_Click);
            this.panel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ProbandoMap.Properties.Resources.mapa_lima_chico;
            this.pictureBox1.Location = new System.Drawing.Point(130, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(4000, 3000);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(53, 518);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Guardar Matriz";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 611);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Probando Maps";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnIzquierda;
        private System.Windows.Forms.Button btnDerecha;
        private System.Windows.Forms.Button btnAbajo;
        private System.Windows.Forms.Button btnArriba;
        private System.Windows.Forms.Button btnAlejar;
        private System.Windows.Forms.Button btnAcercar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtLong;
        private System.Windows.Forms.TextBox txtLat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbNodoDestino;
        private System.Windows.Forms.RadioButton rbNodoOrigen;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}


namespace PixARK_Tools
{
    partial class Ventana_Semillas_Parecidas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Menú_Contextual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Menú_Contextual_Visor_Ayuda = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Acerca = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Depurador_Excepciones = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Abrir_Carpeta = new System.Windows.Forms.ToolStripMenuItem();
            this.Menú_Contextual_Separador_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Menú_Contextual_Mostrar_Mapas_Originales = new System.Windows.Forms.ToolStripMenuItem();
            this.Barra_Estado = new System.Windows.Forms.ToolStrip();
            this.Barra_Estado_Botón_Excepción = new System.Windows.Forms.ToolStripButton();
            this.Barra_Estado_Separador_1 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Etiqueta_CPU = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Separador_2 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Etiqueta_Memoria = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Separador_3 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Etiqueta_FPS = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Separador_4 = new System.Windows.Forms.ToolStripSeparator();
            this.Barra_Estado_Etiqueta_Sugerencia = new System.Windows.Forms.ToolStripLabel();
            this.Barra_Estado_Separador_5 = new System.Windows.Forms.ToolStripSeparator();
            this.Temporizador_Principal = new System.Windows.Forms.Timer(this.components);
            this.DataGridView_Principal = new System.Windows.Forms.DataGridView();
            this.Columna_Imagen = new System.Windows.Forms.DataGridViewImageColumn();
            this.Columna_Último_Byte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Últimos_8_Bits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Semillas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Semillas_Similares = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Semillas_Binarias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columna_Semillas_Predecidas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableLayoutPanel_Mapas = new System.Windows.Forms.TableLayoutPanel();
            this.Picture_Mapa_1 = new System.Windows.Forms.PictureBox();
            this.Picture_Mapa_2 = new System.Windows.Forms.PictureBox();
            this.Picture_Mapa_6 = new System.Windows.Forms.PictureBox();
            this.Picture_Mapa_3 = new System.Windows.Forms.PictureBox();
            this.Picture_Mapa_5 = new System.Windows.Forms.PictureBox();
            this.Picture_Mapa_4 = new System.Windows.Forms.PictureBox();
            this.NumericUpDown_Semilla = new System.Windows.Forms.NumericUpDown();
            this.TableLayoutPanel_Semillas = new System.Windows.Forms.TableLayoutPanel();
            this.TextBox_Semilla_8_Bits_Binaria = new System.Windows.Forms.TextBox();
            this.TextBox_Semilla_8_Bits = new System.Windows.Forms.TextBox();
            this.TextBox_Semilla_Binaria = new System.Windows.Forms.TextBox();
            this.Menú_Contextual.SuspendLayout();
            this.Barra_Estado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Principal)).BeginInit();
            this.TableLayoutPanel_Mapas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Mapa_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Mapa_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Mapa_6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Mapa_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Mapa_5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Mapa_4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Semilla)).BeginInit();
            this.TableLayoutPanel_Semillas.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menú_Contextual
            // 
            this.Menú_Contextual.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menú_Contextual_Visor_Ayuda,
            this.Menú_Contextual_Acerca,
            this.Menú_Contextual_Depurador_Excepciones,
            this.Menú_Contextual_Abrir_Carpeta,
            this.Menú_Contextual_Separador_1,
            this.Menú_Contextual_Mostrar_Mapas_Originales});
            this.Menú_Contextual.Name = "Menú_Contextual";
            this.Menú_Contextual.Size = new System.Drawing.Size(252, 120);
            // 
            // Menú_Contextual_Visor_Ayuda
            // 
            this.Menú_Contextual_Visor_Ayuda.Image = global::PixARK_Tools.Properties.Resources.Ayuda;
            this.Menú_Contextual_Visor_Ayuda.Name = "Menú_Contextual_Visor_Ayuda";
            this.Menú_Contextual_Visor_Ayuda.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.Menú_Contextual_Visor_Ayuda.Size = new System.Drawing.Size(251, 22);
            this.Menú_Contextual_Visor_Ayuda.Text = "Help viewer...";
            this.Menú_Contextual_Visor_Ayuda.Click += new System.EventHandler(this.Menú_Contextual_Visor_Ayuda_Click);
            // 
            // Menú_Contextual_Acerca
            // 
            this.Menú_Contextual_Acerca.Image = global::PixARK_Tools.Properties.Resources.Jupisoft_16;
            this.Menú_Contextual_Acerca.Name = "Menú_Contextual_Acerca";
            this.Menú_Contextual_Acerca.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.Menú_Contextual_Acerca.Size = new System.Drawing.Size(251, 22);
            this.Menú_Contextual_Acerca.Text = "About...";
            this.Menú_Contextual_Acerca.Click += new System.EventHandler(this.Menú_Contextual_Acerca_Click);
            // 
            // Menú_Contextual_Depurador_Excepciones
            // 
            this.Menú_Contextual_Depurador_Excepciones.Image = global::PixARK_Tools.Properties.Resources.Excepción;
            this.Menú_Contextual_Depurador_Excepciones.Name = "Menú_Contextual_Depurador_Excepciones";
            this.Menú_Contextual_Depurador_Excepciones.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.Menú_Contextual_Depurador_Excepciones.Size = new System.Drawing.Size(251, 22);
            this.Menú_Contextual_Depurador_Excepciones.Text = "Exception debugger...";
            this.Menú_Contextual_Depurador_Excepciones.Click += new System.EventHandler(this.Menú_Contextual_Depurador_Excepciones_Click);
            // 
            // Menú_Contextual_Abrir_Carpeta
            // 
            this.Menú_Contextual_Abrir_Carpeta.Image = global::PixARK_Tools.Properties.Resources.Ejecutar;
            this.Menú_Contextual_Abrir_Carpeta.Name = "Menú_Contextual_Abrir_Carpeta";
            this.Menú_Contextual_Abrir_Carpeta.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.Menú_Contextual_Abrir_Carpeta.Size = new System.Drawing.Size(251, 22);
            this.Menú_Contextual_Abrir_Carpeta.Text = "Open the default save folder...";
            this.Menú_Contextual_Abrir_Carpeta.Click += new System.EventHandler(this.Menú_Contextual_Abrir_Carpeta_Click);
            // 
            // Menú_Contextual_Separador_1
            // 
            this.Menú_Contextual_Separador_1.Name = "Menú_Contextual_Separador_1";
            this.Menú_Contextual_Separador_1.Size = new System.Drawing.Size(248, 6);
            // 
            // Menú_Contextual_Mostrar_Mapas_Originales
            // 
            this.Menú_Contextual_Mostrar_Mapas_Originales.CheckOnClick = true;
            this.Menú_Contextual_Mostrar_Mapas_Originales.Name = "Menú_Contextual_Mostrar_Mapas_Originales";
            this.Menú_Contextual_Mostrar_Mapas_Originales.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.Menú_Contextual_Mostrar_Mapas_Originales.Size = new System.Drawing.Size(251, 22);
            this.Menú_Contextual_Mostrar_Mapas_Originales.Text = "Show the original size maps";
            this.Menú_Contextual_Mostrar_Mapas_Originales.CheckedChanged += new System.EventHandler(this.Menú_Contextual_Mostrar_Mapas_Originales_CheckedChanged);
            // 
            // Barra_Estado
            // 
            this.Barra_Estado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Barra_Estado.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Barra_Estado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Barra_Estado_Botón_Excepción,
            this.Barra_Estado_Separador_1,
            this.Barra_Estado_Etiqueta_CPU,
            this.Barra_Estado_Separador_2,
            this.Barra_Estado_Etiqueta_Memoria,
            this.Barra_Estado_Separador_3,
            this.Barra_Estado_Etiqueta_FPS,
            this.Barra_Estado_Separador_4,
            this.Barra_Estado_Etiqueta_Sugerencia,
            this.Barra_Estado_Separador_5});
            this.Barra_Estado.Location = new System.Drawing.Point(0, 436);
            this.Barra_Estado.Name = "Barra_Estado";
            this.Barra_Estado.Size = new System.Drawing.Size(884, 25);
            this.Barra_Estado.TabIndex = 2;
            this.Barra_Estado.Text = "Status bar";
            // 
            // Barra_Estado_Botón_Excepción
            // 
            this.Barra_Estado_Botón_Excepción.AutoToolTip = false;
            this.Barra_Estado_Botón_Excepción.Image = global::PixARK_Tools.Properties.Resources.Excepción_Gris;
            this.Barra_Estado_Botón_Excepción.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Botón_Excepción.Name = "Barra_Estado_Botón_Excepción";
            this.Barra_Estado_Botón_Excepción.Size = new System.Drawing.Size(95, 22);
            this.Barra_Estado_Botón_Excepción.Text = "Exceptions: 0";
            this.Barra_Estado_Botón_Excepción.Visible = false;
            this.Barra_Estado_Botón_Excepción.Click += new System.EventHandler(this.Barra_Estado_Botón_Excepción_Click);
            // 
            // Barra_Estado_Separador_1
            // 
            this.Barra_Estado_Separador_1.Name = "Barra_Estado_Separador_1";
            this.Barra_Estado_Separador_1.Size = new System.Drawing.Size(6, 25);
            this.Barra_Estado_Separador_1.Visible = false;
            // 
            // Barra_Estado_Etiqueta_CPU
            // 
            this.Barra_Estado_Etiqueta_CPU.Image = global::PixARK_Tools.Properties.Resources.CPU;
            this.Barra_Estado_Etiqueta_CPU.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Etiqueta_CPU.Name = "Barra_Estado_Etiqueta_CPU";
            this.Barra_Estado_Etiqueta_CPU.Size = new System.Drawing.Size(71, 22);
            this.Barra_Estado_Etiqueta_CPU.Text = "CPU: 0 %";
            // 
            // Barra_Estado_Separador_2
            // 
            this.Barra_Estado_Separador_2.Name = "Barra_Estado_Separador_2";
            this.Barra_Estado_Separador_2.Size = new System.Drawing.Size(6, 25);
            // 
            // Barra_Estado_Etiqueta_Memoria
            // 
            this.Barra_Estado_Etiqueta_Memoria.Image = global::PixARK_Tools.Properties.Resources.RAM;
            this.Barra_Estado_Etiqueta_Memoria.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Etiqueta_Memoria.Name = "Barra_Estado_Etiqueta_Memoria";
            this.Barra_Estado_Etiqueta_Memoria.Size = new System.Drawing.Size(82, 22);
            this.Barra_Estado_Etiqueta_Memoria.Text = "RAM: 0 MB";
            // 
            // Barra_Estado_Separador_3
            // 
            this.Barra_Estado_Separador_3.Name = "Barra_Estado_Separador_3";
            this.Barra_Estado_Separador_3.Size = new System.Drawing.Size(6, 25);
            // 
            // Barra_Estado_Etiqueta_FPS
            // 
            this.Barra_Estado_Etiqueta_FPS.Image = global::PixARK_Tools.Properties.Resources.Temporizador;
            this.Barra_Estado_Etiqueta_FPS.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Etiqueta_FPS.Name = "Barra_Estado_Etiqueta_FPS";
            this.Barra_Estado_Etiqueta_FPS.Size = new System.Drawing.Size(54, 22);
            this.Barra_Estado_Etiqueta_FPS.Text = "FPS: 0";
            // 
            // Barra_Estado_Separador_4
            // 
            this.Barra_Estado_Separador_4.Name = "Barra_Estado_Separador_4";
            this.Barra_Estado_Separador_4.Size = new System.Drawing.Size(6, 25);
            // 
            // Barra_Estado_Etiqueta_Sugerencia
            // 
            this.Barra_Estado_Etiqueta_Sugerencia.Image = global::PixARK_Tools.Properties.Resources.Ayuda;
            this.Barra_Estado_Etiqueta_Sugerencia.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.Barra_Estado_Etiqueta_Sugerencia.Name = "Barra_Estado_Etiqueta_Sugerencia";
            this.Barra_Estado_Etiqueta_Sugerencia.Size = new System.Drawing.Size(674, 16);
            this.Barra_Estado_Etiqueta_Sugerencia.Text = "Tip: the worlds will be very similar if the last 8 seed bits are the same. So sad" +
    "ly there are only around 256 unique base worlds.";
            // 
            // Barra_Estado_Separador_5
            // 
            this.Barra_Estado_Separador_5.Name = "Barra_Estado_Separador_5";
            this.Barra_Estado_Separador_5.Size = new System.Drawing.Size(6, 25);
            // 
            // Temporizador_Principal
            // 
            this.Temporizador_Principal.Interval = 1;
            this.Temporizador_Principal.Tick += new System.EventHandler(this.Temporizador_Principal_Tick);
            // 
            // DataGridView_Principal
            // 
            this.DataGridView_Principal.AllowUserToAddRows = false;
            this.DataGridView_Principal.AllowUserToDeleteRows = false;
            this.DataGridView_Principal.AllowUserToResizeRows = false;
            this.DataGridView_Principal.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView_Principal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView_Principal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView_Principal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_Principal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Columna_Imagen,
            this.Columna_Último_Byte,
            this.Columna_Últimos_8_Bits,
            this.Columna_Semillas,
            this.Columna_Semillas_Similares,
            this.Columna_Semillas_Binarias,
            this.Columna_Semillas_Predecidas});
            this.TableLayoutPanel_Semillas.SetColumnSpan(this.DataGridView_Principal, 2);
            this.DataGridView_Principal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DataGridView_Principal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridView_Principal.Location = new System.Drawing.Point(0, 40);
            this.DataGridView_Principal.Margin = new System.Windows.Forms.Padding(0);
            this.DataGridView_Principal.MultiSelect = false;
            this.DataGridView_Principal.Name = "DataGridView_Principal";
            this.DataGridView_Principal.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView_Principal.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView_Principal.RowHeadersVisible = false;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView_Principal.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridView_Principal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView_Principal.Size = new System.Drawing.Size(243, 396);
            this.DataGridView_Principal.TabIndex = 0;
            this.DataGridView_Principal.SelectionChanged += new System.EventHandler(this.DataGridView_Principal_SelectionChanged);
            this.DataGridView_Principal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridView_Principal_KeyDown);
            // 
            // Columna_Imagen
            // 
            this.Columna_Imagen.HeaderText = "";
            this.Columna_Imagen.Name = "Columna_Imagen";
            this.Columna_Imagen.ReadOnly = true;
            // 
            // Columna_Último_Byte
            // 
            this.Columna_Último_Byte.HeaderText = "Byte";
            this.Columna_Último_Byte.Name = "Columna_Último_Byte";
            this.Columna_Último_Byte.ReadOnly = true;
            // 
            // Columna_Últimos_8_Bits
            // 
            this.Columna_Últimos_8_Bits.HeaderText = "Last bits";
            this.Columna_Últimos_8_Bits.Name = "Columna_Últimos_8_Bits";
            this.Columna_Últimos_8_Bits.ReadOnly = true;
            // 
            // Columna_Semillas
            // 
            this.Columna_Semillas.HeaderText = "Seeds";
            this.Columna_Semillas.Name = "Columna_Semillas";
            this.Columna_Semillas.ReadOnly = true;
            // 
            // Columna_Semillas_Similares
            // 
            this.Columna_Semillas_Similares.HeaderText = "Similar seeds";
            this.Columna_Semillas_Similares.Name = "Columna_Semillas_Similares";
            this.Columna_Semillas_Similares.ReadOnly = true;
            // 
            // Columna_Semillas_Binarias
            // 
            this.Columna_Semillas_Binarias.HeaderText = "Binary seeds";
            this.Columna_Semillas_Binarias.Name = "Columna_Semillas_Binarias";
            this.Columna_Semillas_Binarias.ReadOnly = true;
            // 
            // Columna_Semillas_Predecidas
            // 
            this.Columna_Semillas_Predecidas.HeaderText = "Predicted seeds";
            this.Columna_Semillas_Predecidas.Name = "Columna_Semillas_Predecidas";
            this.Columna_Semillas_Predecidas.ReadOnly = true;
            // 
            // TableLayoutPanel_Mapas
            // 
            this.TableLayoutPanel_Mapas.ColumnCount = 3;
            this.TableLayoutPanel_Mapas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel_Mapas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.TableLayoutPanel_Mapas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel_Mapas.Controls.Add(this.Picture_Mapa_1, 0, 0);
            this.TableLayoutPanel_Mapas.Controls.Add(this.Picture_Mapa_2, 2, 0);
            this.TableLayoutPanel_Mapas.Controls.Add(this.Picture_Mapa_6, 2, 4);
            this.TableLayoutPanel_Mapas.Controls.Add(this.Picture_Mapa_3, 0, 2);
            this.TableLayoutPanel_Mapas.Controls.Add(this.Picture_Mapa_5, 0, 4);
            this.TableLayoutPanel_Mapas.Controls.Add(this.Picture_Mapa_4, 2, 2);
            this.TableLayoutPanel_Mapas.Dock = System.Windows.Forms.DockStyle.Right;
            this.TableLayoutPanel_Mapas.Location = new System.Drawing.Point(243, 0);
            this.TableLayoutPanel_Mapas.Name = "TableLayoutPanel_Mapas";
            this.TableLayoutPanel_Mapas.RowCount = 5;
            this.TableLayoutPanel_Mapas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33408F));
            this.TableLayoutPanel_Mapas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.TableLayoutPanel_Mapas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33407F));
            this.TableLayoutPanel_Mapas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.TableLayoutPanel_Mapas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33185F));
            this.TableLayoutPanel_Mapas.Size = new System.Drawing.Size(641, 436);
            this.TableLayoutPanel_Mapas.TabIndex = 1;
            // 
            // Picture_Mapa_1
            // 
            this.Picture_Mapa_1.BackColor = System.Drawing.Color.Gray;
            this.Picture_Mapa_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture_Mapa_1.InitialImage = null;
            this.Picture_Mapa_1.Location = new System.Drawing.Point(0, 0);
            this.Picture_Mapa_1.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_Mapa_1.Name = "Picture_Mapa_1";
            this.Picture_Mapa_1.Size = new System.Drawing.Size(320, 144);
            this.Picture_Mapa_1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Picture_Mapa_1.TabIndex = 6;
            this.Picture_Mapa_1.TabStop = false;
            // 
            // Picture_Mapa_2
            // 
            this.Picture_Mapa_2.BackColor = System.Drawing.Color.Gray;
            this.Picture_Mapa_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture_Mapa_2.InitialImage = null;
            this.Picture_Mapa_2.Location = new System.Drawing.Point(321, 0);
            this.Picture_Mapa_2.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_Mapa_2.Name = "Picture_Mapa_2";
            this.Picture_Mapa_2.Size = new System.Drawing.Size(320, 144);
            this.Picture_Mapa_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Picture_Mapa_2.TabIndex = 7;
            this.Picture_Mapa_2.TabStop = false;
            // 
            // Picture_Mapa_6
            // 
            this.Picture_Mapa_6.BackColor = System.Drawing.Color.Gray;
            this.Picture_Mapa_6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture_Mapa_6.InitialImage = null;
            this.Picture_Mapa_6.Location = new System.Drawing.Point(321, 290);
            this.Picture_Mapa_6.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_Mapa_6.Name = "Picture_Mapa_6";
            this.Picture_Mapa_6.Size = new System.Drawing.Size(320, 146);
            this.Picture_Mapa_6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Picture_Mapa_6.TabIndex = 11;
            this.Picture_Mapa_6.TabStop = false;
            // 
            // Picture_Mapa_3
            // 
            this.Picture_Mapa_3.BackColor = System.Drawing.Color.Gray;
            this.Picture_Mapa_3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture_Mapa_3.InitialImage = null;
            this.Picture_Mapa_3.Location = new System.Drawing.Point(0, 145);
            this.Picture_Mapa_3.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_Mapa_3.Name = "Picture_Mapa_3";
            this.Picture_Mapa_3.Size = new System.Drawing.Size(320, 144);
            this.Picture_Mapa_3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Picture_Mapa_3.TabIndex = 8;
            this.Picture_Mapa_3.TabStop = false;
            // 
            // Picture_Mapa_5
            // 
            this.Picture_Mapa_5.BackColor = System.Drawing.Color.Gray;
            this.Picture_Mapa_5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture_Mapa_5.InitialImage = null;
            this.Picture_Mapa_5.Location = new System.Drawing.Point(0, 290);
            this.Picture_Mapa_5.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_Mapa_5.Name = "Picture_Mapa_5";
            this.Picture_Mapa_5.Size = new System.Drawing.Size(320, 146);
            this.Picture_Mapa_5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Picture_Mapa_5.TabIndex = 10;
            this.Picture_Mapa_5.TabStop = false;
            // 
            // Picture_Mapa_4
            // 
            this.Picture_Mapa_4.BackColor = System.Drawing.Color.Gray;
            this.Picture_Mapa_4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Picture_Mapa_4.InitialImage = null;
            this.Picture_Mapa_4.Location = new System.Drawing.Point(321, 145);
            this.Picture_Mapa_4.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_Mapa_4.Name = "Picture_Mapa_4";
            this.Picture_Mapa_4.Size = new System.Drawing.Size(320, 144);
            this.Picture_Mapa_4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Picture_Mapa_4.TabIndex = 9;
            this.Picture_Mapa_4.TabStop = false;
            // 
            // NumericUpDown_Semilla
            // 
            this.NumericUpDown_Semilla.BackColor = System.Drawing.Color.White;
            this.NumericUpDown_Semilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NumericUpDown_Semilla.Location = new System.Drawing.Point(0, 0);
            this.NumericUpDown_Semilla.Margin = new System.Windows.Forms.Padding(0);
            this.NumericUpDown_Semilla.Maximum = new decimal(new int[] {
            -1,
            2147483647,
            0,
            0});
            this.NumericUpDown_Semilla.Name = "NumericUpDown_Semilla";
            this.NumericUpDown_Semilla.Size = new System.Drawing.Size(121, 20);
            this.NumericUpDown_Semilla.TabIndex = 1;
            this.NumericUpDown_Semilla.ThousandsSeparator = true;
            this.NumericUpDown_Semilla.ValueChanged += new System.EventHandler(this.NumericUpDown_Semilla_ValueChanged);
            this.NumericUpDown_Semilla.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Semillas_Parecidas_KeyDown);
            // 
            // TableLayoutPanel_Semillas
            // 
            this.TableLayoutPanel_Semillas.ColumnCount = 2;
            this.TableLayoutPanel_Semillas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel_Semillas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel_Semillas.Controls.Add(this.TextBox_Semilla_8_Bits_Binaria, 1, 1);
            this.TableLayoutPanel_Semillas.Controls.Add(this.DataGridView_Principal, 0, 2);
            this.TableLayoutPanel_Semillas.Controls.Add(this.TextBox_Semilla_8_Bits, 1, 0);
            this.TableLayoutPanel_Semillas.Controls.Add(this.NumericUpDown_Semilla, 0, 0);
            this.TableLayoutPanel_Semillas.Controls.Add(this.TextBox_Semilla_Binaria, 0, 1);
            this.TableLayoutPanel_Semillas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayoutPanel_Semillas.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanel_Semillas.Name = "TableLayoutPanel_Semillas";
            this.TableLayoutPanel_Semillas.RowCount = 3;
            this.TableLayoutPanel_Semillas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel_Semillas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel_Semillas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel_Semillas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel_Semillas.Size = new System.Drawing.Size(243, 436);
            this.TableLayoutPanel_Semillas.TabIndex = 0;
            // 
            // TextBox_Semilla_8_Bits_Binaria
            // 
            this.TextBox_Semilla_8_Bits_Binaria.BackColor = System.Drawing.Color.White;
            this.TextBox_Semilla_8_Bits_Binaria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_Semilla_8_Bits_Binaria.Location = new System.Drawing.Point(121, 20);
            this.TextBox_Semilla_8_Bits_Binaria.Margin = new System.Windows.Forms.Padding(0);
            this.TextBox_Semilla_8_Bits_Binaria.Name = "TextBox_Semilla_8_Bits_Binaria";
            this.TextBox_Semilla_8_Bits_Binaria.ReadOnly = true;
            this.TextBox_Semilla_8_Bits_Binaria.Size = new System.Drawing.Size(122, 20);
            this.TextBox_Semilla_8_Bits_Binaria.TabIndex = 4;
            this.TextBox_Semilla_8_Bits_Binaria.Text = "00000000";
            // 
            // TextBox_Semilla_8_Bits
            // 
            this.TextBox_Semilla_8_Bits.BackColor = System.Drawing.Color.White;
            this.TextBox_Semilla_8_Bits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_Semilla_8_Bits.Location = new System.Drawing.Point(121, 0);
            this.TextBox_Semilla_8_Bits.Margin = new System.Windows.Forms.Padding(0);
            this.TextBox_Semilla_8_Bits.Name = "TextBox_Semilla_8_Bits";
            this.TextBox_Semilla_8_Bits.ReadOnly = true;
            this.TextBox_Semilla_8_Bits.Size = new System.Drawing.Size(122, 20);
            this.TextBox_Semilla_8_Bits.TabIndex = 2;
            this.TextBox_Semilla_8_Bits.Text = "0";
            // 
            // TextBox_Semilla_Binaria
            // 
            this.TextBox_Semilla_Binaria.BackColor = System.Drawing.Color.White;
            this.TextBox_Semilla_Binaria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_Semilla_Binaria.Location = new System.Drawing.Point(0, 20);
            this.TextBox_Semilla_Binaria.Margin = new System.Windows.Forms.Padding(0);
            this.TextBox_Semilla_Binaria.Name = "TextBox_Semilla_Binaria";
            this.TextBox_Semilla_Binaria.ReadOnly = true;
            this.TextBox_Semilla_Binaria.Size = new System.Drawing.Size(121, 20);
            this.TextBox_Semilla_Binaria.TabIndex = 3;
            this.TextBox_Semilla_Binaria.Text = "00000000";
            this.TextBox_Semilla_Binaria.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Semillas_Parecidas_KeyDown);
            // 
            // Ventana_Semillas_Parecidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.ContextMenuStrip = this.Menú_Contextual;
            this.Controls.Add(this.TableLayoutPanel_Semillas);
            this.Controls.Add(this.TableLayoutPanel_Mapas);
            this.Controls.Add(this.Barra_Estado);
            this.DoubleBuffered = true;
            this.Name = "Ventana_Semillas_Parecidas";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Similar Seeds by Jupisoft";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventana_Semillas_Parecidas_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventana_Semillas_Parecidas_FormClosed);
            this.Load += new System.EventHandler(this.Ventana_Semillas_Parecidas_Load);
            this.Shown += new System.EventHandler(this.Ventana_Semillas_Parecidas_Shown);
            this.SizeChanged += new System.EventHandler(this.Ventana_Semillas_Parecidas_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventana_Semillas_Parecidas_KeyDown);
            this.Menú_Contextual.ResumeLayout(false);
            this.Barra_Estado.ResumeLayout(false);
            this.Barra_Estado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Principal)).EndInit();
            this.TableLayoutPanel_Mapas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Mapa_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Mapa_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Mapa_6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Mapa_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Mapa_5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Mapa_4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Semilla)).EndInit();
            this.TableLayoutPanel_Semillas.ResumeLayout(false);
            this.TableLayoutPanel_Semillas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip Menú_Contextual;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Visor_Ayuda;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Acerca;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Depurador_Excepciones;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Abrir_Carpeta;
        private System.Windows.Forms.ToolStripSeparator Menú_Contextual_Separador_1;
        private System.Windows.Forms.ToolStripMenuItem Menú_Contextual_Mostrar_Mapas_Originales;
        private System.Windows.Forms.ToolStrip Barra_Estado;
        private System.Windows.Forms.ToolStripButton Barra_Estado_Botón_Excepción;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_1;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_CPU;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_2;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_Memoria;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_3;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_FPS;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_4;
        private System.Windows.Forms.ToolStripSeparator Barra_Estado_Separador_5;
        private System.Windows.Forms.ToolStripLabel Barra_Estado_Etiqueta_Sugerencia;
        private System.Windows.Forms.Timer Temporizador_Principal;
        private System.Windows.Forms.DataGridView DataGridView_Principal;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel_Mapas;
        private System.Windows.Forms.PictureBox Picture_Mapa_1;
        private System.Windows.Forms.PictureBox Picture_Mapa_4;
        private System.Windows.Forms.PictureBox Picture_Mapa_3;
        private System.Windows.Forms.PictureBox Picture_Mapa_2;
        private System.Windows.Forms.PictureBox Picture_Mapa_5;
        private System.Windows.Forms.PictureBox Picture_Mapa_6;
        private System.Windows.Forms.NumericUpDown NumericUpDown_Semilla;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanel_Semillas;
        private System.Windows.Forms.TextBox TextBox_Semilla_Binaria;
        private System.Windows.Forms.TextBox TextBox_Semilla_8_Bits_Binaria;
        private System.Windows.Forms.TextBox TextBox_Semilla_8_Bits;
        private System.Windows.Forms.DataGridViewImageColumn Columna_Imagen;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Último_Byte;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Últimos_8_Bits;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Semillas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Semillas_Similares;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Semillas_Binarias;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columna_Semillas_Predecidas;
    }
}
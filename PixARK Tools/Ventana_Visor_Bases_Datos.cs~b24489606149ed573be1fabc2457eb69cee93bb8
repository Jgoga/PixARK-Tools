using Finisar.SQLite;
using PixARK_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixARK_Tools
{
    public partial class Ventana_Visor_Bases_Datos : Form
    {
        public Ventana_Visor_Bases_Datos()
        {
            InitializeComponent();
        }
        internal static readonly string Ruta_SQLite = Application.StartupPath + "\\SQLite";
        internal readonly string Texto_Título = "SQLite Data Base Viewer for " + Program.Texto_Usuario + " by Jupisoft";
        internal bool Variable_Siempre_Visible = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal static Stopwatch FPS_Cronómetro = Stopwatch.StartNew();
        internal long FPS_Segundo_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;
        internal bool Ocupado = false;

        //internal SQLiteConnection Conexión_SQL = null;
        internal SQLiteDataAdapter Adaptador_SQL = null;
        internal DataSet Set_Datos = null;

        private void Ventana_Visor_Bases_Datos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = Texto_Título;
                this.WindowState = FormWindowState.Maximized;
                if (ComboBox_Ruta.Items.Count > 0) ComboBox_Ruta.SelectedIndex = 0;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Bases_Datos_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Bases_Datos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
                if (Set_Datos != null)
                {
                    Set_Datos.Dispose();
                    Set_Datos = null;
                }
                if (Adaptador_SQL != null)
                {
                    Adaptador_SQL.Dispose();
                    Adaptador_SQL = null;
                }
                /*if (Conexión_SQL != null)
                {
                    Conexión_SQL.Close();
                    Conexión_SQL.Dispose();
                    Conexión_SQL = null;
                }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Bases_Datos_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Bases_Datos_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Bases_Datos_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                {
                    string[] Matriz_Rutas = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                                {
                                    if (!ListView_Bytes.Visible)
                                    {
                                        ComboBox_Ruta.Text = Ruta;
                                        break;
                                    }
                                    else
                                    {
                                        FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                                        if (Lector != null)
                                        {
                                            if (Lector.Length > 0L)
                                            {
                                                Lector.Seek(0L, SeekOrigin.Begin);
                                                byte[] Matriz_Bytes = new byte[Lector.Length];
                                                int Longitud = Lector.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                                                Lector.Close();
                                                Lector.Dispose();
                                                Lector = null;
                                                if (Longitud > 0)
                                                {
                                                    if (Matriz_Bytes.Length != Longitud)
                                                    {
                                                        Array.Resize(ref Matriz_Bytes, Longitud);
                                                    }
                                                    Establecer_Matriz_Bytes_Arco_Iris(Matriz_Bytes);
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                Lector.Close();
                                                Lector.Dispose();
                                                Lector = null;
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                        Matriz_Rutas = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Ventana_Visor_Bases_Datos_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Visor_Bases_Datos_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Escape)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        this.Close();
                    }
                    else if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Ruta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Cargar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(ComboBox_Ruta.Text) && File.Exists(ComboBox_Ruta.Text))
                {
                    ComboBox_Fila.Items.Clear();
                    string Texto_Conexión = "Data Source=" + ComboBox_Ruta.Text + ";New=False;Version=3";
                    SQLiteConnection Conexión_SQL = new SQLiteConnection(Texto_Conexión);
                    Conexión_SQL.Open();
                    string Texto_Origen = "sqlite_master";
                    string Texto_Comando = "Select name from sqlite_master;"; // Get the main names.
                    SQLiteDataAdapter Adaptador_SQL = new SQLiteDataAdapter(Texto_Comando, Conexión_SQL);
                    DataSet Set_Datos = new DataSet();
                    Set_Datos.RemotingFormat = SerializationFormat.Binary;
                    Adaptador_SQL.Fill(Set_Datos, Texto_Origen);
                    if (Set_Datos.Tables != null && Set_Datos.Tables.Count > 0)
                    {
                        foreach (DataTable Tabla in Set_Datos.Tables)
                        {
                            try
                            {
                                if (Tabla != null &&
                                    Tabla.Columns != null &&
                                    Tabla.Columns.Count > 0 &&
                                    Tabla.Columns[0] != null &&
                                    Tabla.Rows != null &&
                                    Tabla.Rows.Count > 0)
                                {
                                    foreach (DataRow Fila in Tabla.Rows)
                                    {
                                        try
                                        {
                                            if (Fila != null && !string.IsNullOrEmpty(Fila[0] as string))
                                            {
                                                ComboBox_Fila.Items.Add(Fila[0] as string);
                                            }
                                        }
                                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                    }
                                }
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                    }
                    Set_Datos.Dispose();
                    Set_Datos = null;
                    Adaptador_SQL.Dispose();
                    Adaptador_SQL = null;
                    Conexión_SQL.Close();
                    Conexión_SQL.Dispose();
                    Conexión_SQL = null;
                    if (ComboBox_Fila.Items.Count > 0) ComboBox_Fila.SelectedIndex = 0;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void DataGridView_Principal_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                //Depurador.Escribir_Excepción(e.Exception != null ? e.Exception.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true;
                e.Cancel = false;
                e.ThrowException = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Cargar_Opciones()
        {
            try
            {
                /*RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Template");

                // bool
                try { Variable_ = bool.Parse((string)Clave.GetValue("Variable_", bool.TrueString)); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_ = true; }

                // int
                try { Variable_ = (int)Clave.GetValue("Variable_", 0); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Variable_ = 0; }
                
                // Correct any bad value after loading:
                if ((int)Variable_ < 0 || (int)Variable_ > (int)Variables.Variable) Variable_ = Variables.Variable;

                // Apply all the loaded values:
                ComboBox_Variable_.SelectedIndex = (int)Variable_;

                Menú_Contextual_Variable_.Checked = Variable_;*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Guardar_Opciones()
        {
            try
            {
                /*RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Template");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        Clave.DeleteValue(Matriz_Nombres[Índice]);
                    }
                }
                Matriz_Nombres = null;
                
                // bool
                try { Clave.SetValue("Variable_", Variable_doDaylightCycle.ToString(), RegistryValueKind.String); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                // int
                try { Clave.SetValue("Tickspeed", (int)Variable_, RegistryValueKind.DWord); }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Registro_Restablecer_Opciones()
        {
            try
            {
                /*RegistryKey Clave = Registry.CurrentUser.CreateSubKey("Software\\Jupisoft\\Minecraft Tools\\" + Program.Texto_Versión + "\\Template");
                string[] Matriz_Nombres = Clave.GetValueNames();
                if (Matriz_Nombres != null && Matriz_Nombres.Length > 0)
                {
                    for (int Índice = 0; Índice < Matriz_Nombres.Length; Índice++)
                    {
                        try { Clave.DeleteValue(Matriz_Nombres[Índice]); }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                    Matriz_Nombres = null;
                }
                Clave.Close();
                Clave = null;*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                /*Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Main_window;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;*/
                SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Acerca_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Acerca Ventana = new Ventana_Acerca();
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Depurador_Excepciones_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Depurador_Excepciones Ventana = new Ventana_Depurador_Excepciones();
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Abrir_Carpeta_Click(object sender, EventArgs e)
        {
            try
            {
                //Program.Crear_Carpetas(Program.Ruta_Minecraft);
                //Program.Ejecutar_Ruta(Program.Ruta_Guardado_Minecraft, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                /*if (Picture.Image != null)
                {
                    Clipboard.SetImage(Picture.Image);
                    SystemSounds.Asterisk.Play();
                }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                /*if (Picture.Image != null)
                {
                    Program.Crear_Carpetas(Program.Ruta_Minecraft);
                    Picture.Image.Save(Program.Ruta_Minecraft + "\\" + Program.Obtener_Nombre_Temporal_Sin_Guiones() + ".png", ImageFormat.Png);
                    SystemSounds.Asterisk.Play();
                }*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Barra_Estado_Botón_Excepción_Click(object sender, EventArgs e)
        {
            try
            {
                Variable_Excepción = false;
                Variable_Excepción_Imagen = false;
                Variable_Excepción_Total = 0;
                Barra_Estado_Botón_Excepción.Visible = false;
                Barra_Estado_Separador_1.Visible = false;
                Barra_Estado_Botón_Excepción.Image = Resources.Excepción_Gris;
                Barra_Estado_Botón_Excepción.ForeColor = Color.Black;
                Barra_Estado_Botón_Excepción.Text = "Exceptions: 0";
                Ventana_Depurador_Excepciones Ventana = new Ventana_Depurador_Excepciones();
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Temporizador_Principal_Tick(object sender, EventArgs e)
        {
            try
            {
                int Tick = Environment.TickCount;
                try
                {
                    if (Variable_Excepción)
                    {
                        if ((Environment.TickCount / 500) % 2 == 0)
                        {
                            if (!Variable_Excepción_Imagen)
                            {
                                Variable_Excepción_Imagen = true;
                                Barra_Estado_Botón_Excepción.Image = Resources.Excepción;
                                Barra_Estado_Botón_Excepción.ForeColor = Color.Red;
                                Barra_Estado_Botón_Excepción.Text = "Exceptions: " + Program.Traducir_Número(Variable_Excepción_Total);
                            }
                        }
                        else
                        {
                            if (Variable_Excepción_Imagen)
                            {
                                Variable_Excepción_Imagen = false;
                                Barra_Estado_Botón_Excepción.Image = Resources.Excepción_Gris;
                                Barra_Estado_Botón_Excepción.ForeColor = Color.Black;
                                Barra_Estado_Botón_Excepción.Text = "Exceptions: " + Program.Traducir_Número(Variable_Excepción_Total);
                            }
                        }
                        if (!Barra_Estado_Botón_Excepción.Visible) Barra_Estado_Botón_Excepción.Visible = true;
                        if (!Barra_Estado_Separador_1.Visible) Barra_Estado_Separador_1.Visible = true;
                    }
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                try
                {
                    try
                    {
                        if (Tick % 250 == 0) // Only update every quarter second
                        {
                            if (Program.Rendimiento_Procesador != null)
                            {
                                double CPU = (double)Program.Rendimiento_Procesador.NextValue();
                                if (CPU < 0d) CPU = 0d;
                                else if (CPU > 100d) CPU = 100d;
                                Barra_Estado_Etiqueta_CPU.Text = "CPU: " + Program.Traducir_Número_Decimales_Redondear(CPU, 2) + " %";
                            }
                            Program.Proceso.Refresh();
                            long Memoria_Bytes = Program.Proceso.PagedMemorySize64;
                            Barra_Estado_Etiqueta_Memoria.Text = "RAM: " + Program.Traducir_Tamaño_Bytes_Automático(Memoria_Bytes, 2, true);
                            if (Memoria_Bytes < 4294967296L) // < 4 GB
                            {
                                if (Variable_Memoria)
                                {
                                    Variable_Memoria = false;
                                    Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Black;
                                }
                            }
                            else // >= 4 GB
                            {
                                if ((Environment.TickCount / 500) % 2 == 0)
                                {
                                    if (!Variable_Memoria)
                                    {
                                        Variable_Memoria = true;
                                        Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Red;
                                    }
                                }
                                else
                                {
                                    if (Variable_Memoria)
                                    {
                                        Variable_Memoria = false;
                                        Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Black;
                                    }
                                }
                            }
                        }
                    }
                    catch { Barra_Estado_Etiqueta_Memoria.Text = "RAM: ? MB (? GB)"; }
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                long FPS_Milisegundo = FPS_Cronómetro.ElapsedMilliseconds;
                long FPS_Segundo = FPS_Milisegundo / 1000L;
                if (FPS_Segundo != FPS_Segundo_Anterior)
                {
                    FPS_Segundo_Anterior = FPS_Segundo;
                    FPS_Real = FPS_Temporal;
                    Barra_Estado_Etiqueta_FPS.Text = FPS_Real.ToString() + " FPS";
                    FPS_Temporal = 0L;
                }
                FPS_Temporal++;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Fila_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(ComboBox_Ruta.Text) && File.Exists(ComboBox_Ruta.Text))
                {
                    //DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    //DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                    DataGridView_Principal.DataMember = null;
                    DataGridView_Principal.DataSource = null;
                    //DataGridView_Principal.Rows.Clear();
                    //DataGridView_Principal.Columns.Clear();
                    if (Set_Datos != null)
                    {
                        Set_Datos.Dispose();
                        Set_Datos = null;
                    }
                    if (Adaptador_SQL != null)
                    {
                        Adaptador_SQL.Dispose();
                        Adaptador_SQL = null;
                    }
                    /*if (Conexión_SQL != null)
                    {
                        Conexión_SQL.Close();
                        Conexión_SQL.Dispose();
                        Conexión_SQL = null;
                    }*/
                    string Texto_Conexión = "Data Source=" + ComboBox_Ruta.Text + ";New=False;Version=3";
                    SQLiteConnection Conexión_SQL = new SQLiteConnection(Texto_Conexión);
                    Conexión_SQL.Open();
                    string Texto_Fila = ComboBox_Fila.Text;
                    string Texto_Comando = "Select * from " + Texto_Fila + ";";
                    Adaptador_SQL = new SQLiteDataAdapter(Texto_Comando, Conexión_SQL);
                    Set_Datos = new DataSet();
                    Set_Datos.RemotingFormat = SerializationFormat.Binary;
                    Adaptador_SQL.Fill(Set_Datos, Texto_Fila);
                    //dataGridView1.DataSource = Set_Datos;
                    //dataGridView1.DataMember = Texto_Fila;
                    DataGridView_Principal.DataSource = Set_Datos;
                    DataGridView_Principal.DataMember = Texto_Fila;
                    if (DataGridView_Principal.Columns != null && DataGridView_Principal.Columns.Count > 0)
                    {
                        foreach (DataGridViewColumn Columna in DataGridView_Principal.Columns)
                        {
                            //Columna.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                    }
                    this.Text = Texto_Título + " - [Tables: " + Program.Traducir_Número(ComboBox_Fila.Items.Count) + ", Columns: " + Program.Traducir_Número(ComboBox_Fila.Items.Count) + ", Rows: " + Program.Traducir_Número(DataGridView_Principal.Rows.Count) + "]";
                    Conexión_SQL.Close();
                    Conexión_SQL.Dispose();
                    Conexión_SQL = null;
                    //DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    //DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    /*Set_Datos.Dispose();
                    Set_Datos = null;
                    Adaptador_SQL.Dispose();
                    Adaptador_SQL = null;
                    Conexión_SQL.Close();
                    Conexión_SQL.Dispose();
                    Conexión_SQL = null;*/
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void ListView_Bytes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ListView_Bytes.SelectedIndices != null && ListView_Bytes.SelectedIndices.Count > 0)
                {
                    this.Text = Texto_Título + " - [Selected bytes: " + Program.Traducir_Número(ListView_Bytes.SelectedIndices.Count) + "]";
                }
                else
                {
                    this.Text = Texto_Título + " - [Loaded bytes: " + Program.Traducir_Número(ListView_Bytes.Items.Count) + "]";
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Explorar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ListView_Bytes.Visible = !ListView_Bytes.Visible;
                if (ListView_Bytes.Visible)
                {
                    if (DataGridView_Principal.CurrentCell != null && DataGridView_Principal.CurrentCell.Value != null)
                    {
                        byte[] Matriz_Bytes = DataGridView_Principal.CurrentCell.Value as byte[];
                        if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                        {
                            Establecer_Matriz_Bytes_Arco_Iris(Matriz_Bytes);
                        }
                        Matriz_Bytes = null;
                    }
                }
                else
                {
                    this.Text = Texto_Título + " - [Tables: " + Program.Traducir_Número(ComboBox_Fila.Items.Count) + ", Columns: " + Program.Traducir_Número(ComboBox_Fila.Items.Count) + ", Rows: " + Program.Traducir_Número(DataGridView_Principal.Rows.Count) + "]";
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Botón_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DataGridView_Principal.CurrentCell != null && DataGridView_Principal.CurrentCell.Value != null)
                {
                    byte[] Matriz_Bytes = DataGridView_Principal.CurrentCell.Value as byte[];
                    if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                    {
                        uint CRC_32 = Program.Calcular_CRC32(Matriz_Bytes);
                        string Texto_CRC_32 = Convert.ToString(CRC_32, 16).ToUpperInvariant();
                        while (Texto_CRC_32.Length < 8) Texto_CRC_32 = '0' + Texto_CRC_32;
                        string Ruta = Application.StartupPath + "\\SQLite";
                        Program.Crear_Carpetas(Ruta);
                        Ruta += "\\" + Texto_CRC_32 + ".txt";
                        if (!File.Exists(Ruta))
                        {
                            FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                            Lector.SetLength(0L);
                            Lector.Seek(0L, SeekOrigin.Begin);
                            Lector.Write(Matriz_Bytes, 0, Matriz_Bytes.Length);
                            Lector.Close();
                            Lector.Dispose();
                            Lector = null;
                            SystemSounds.Asterisk.Play();
                        }
                        Ruta = null;
                    }
                    else
                    {
                        string Texto_Posición = DataGridView_Principal.CurrentCell.Value as string;
                        if (!string.IsNullOrEmpty(Texto_Posición) &&
                            Texto_Posición.Contains(','))
                        {
                            string[] Matriz_Líneas = Texto_Posición.Replace("(", null).Replace(")", null).Replace(" ", null).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            if (Matriz_Líneas != null && Matriz_Líneas.Length >= 2)
                            {
                                Point Posición = new Point(int.Parse(Matriz_Líneas[0]), int.Parse(Matriz_Líneas[1]));
                                MessageBox.Show(this, Posición.ToString(), Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            Matriz_Líneas = null;
                        }
                    }
                    Matriz_Bytes = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        internal void Establecer_Matriz_Bytes_Arco_Iris(byte[] Matriz_Bytes)
        {
            try
            {
                if (ListView_Bytes.Visible)
                {
                    if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                    {
                        ListView_Bytes.Items.Clear();
                        this.Text = Texto_Título + " - [Total bytes: " + Program.Traducir_Número(Matriz_Bytes.Length) + ", Loaded bytes: " + Program.Traducir_Número(Math.Min(Matriz_Bytes.Length, 16 * 1024)) + "]";
                        int Longitud_Máxima = (Matriz_Bytes.Length - 1).ToString().Length;
                        for (int Índice_Byte = 0; Índice_Byte < Math.Min(Matriz_Bytes.Length, 16 * 1024); Índice_Byte++) // <= 16 KB.
                        {
                            string Texto_Índice = Program.Traducir_Número(Índice_Byte);
                            while (Texto_Índice.Length < Longitud_Máxima)
                            {
                                Texto_Índice = '0' + Texto_Índice;
                            }
                            string Texto_Binario = Convert.ToString(Matriz_Bytes[Índice_Byte], 2);
                            while (Texto_Binario.Length < 8)
                            {
                                Texto_Binario = '0' + Texto_Binario;
                            }
                            string Texto_Byte = Matriz_Bytes[Índice_Byte].ToString();
                            while (Texto_Byte.Length < 3)
                            {
                                Texto_Byte = '0' + Texto_Byte;
                            }
                            string Texto_Caracter = "\"" + ((char)Matriz_Bytes[Índice_Byte]).ToString() + "\"";
                            string Texto_Objeto = Texto_Índice + ", " + Texto_Binario + ", " + Texto_Byte + ", " + Texto_Caracter;
                            ListViewItem Objeto = new ListViewItem(Texto_Objeto);
                            //Objeto.BackColor = Program.Matriz_Colores_12_Notas[Índice_Byte % 12];
                            //Objeto.BackColor = Program.Matriz_Colores_12_Notas[Matriz_Bytes[Índice_Byte] % 12];
                            Objeto.BackColor = Program.Matriz_Colores_12_Notas[Matriz_Bytes[Índice_Byte] % 12];
                            Objeto.ForeColor = Color.Black;
                            ListView_Bytes.Items.Add(Objeto);
                        }
                    }
                    Matriz_Bytes = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}

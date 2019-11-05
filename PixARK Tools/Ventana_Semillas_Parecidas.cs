using PixARK_Tools.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixARK_Tools
{
    /// <summary>
    /// Note: after finding by chance that worlds were almost the same in the generated maps within
    /// a few minutes, I thought that perhaps they were similar due to some values in their seeds.
    /// So I converted them to binary and the last 8 bits were exactly the same, and as I started
    /// to suspect I thought that perhaps it was happening with other randomly generated seeds, but
    /// luckily I had already around 200 unique maps, each for a unique random seed, so I made a new
    /// code to just detect bit similarities in the last 8 bits within al my saved maps and the
    /// results caught me by surprise, because around half of the saved seeds were almost repeated,
    /// which is also very strange, but I couldn't confirm one by one if they were the same, so this
    /// new tool was born, just out of curiosity, and finally I can absolutely confirm that most
    /// worlds from PixARK will tend to repeat themselves, perhaps with some little variations around
    /// the edges, but all the main biomes almost always will be around the same central positions,
    /// which sadly means that there are very few unique worlds in PixARK... I suppose the creators
    /// from the game already knew that, but I'm not gonna contact with them to tell this, so if you
    /// want you might further investigate this possible issue with how the biome terrain is generated,
    /// which I believe it should be expanded a bit more, specially because it seems designed for worlds
    /// of 5.120 x 5.120, but because of the world borders too many biomes end up being unusable and
    /// very often missing from the playable world, even the 9 patches of novice grassland around the
    /// edges and center of all the worlds, resulting in "randomly" generated spawn places, very often
    /// repeated and just separated a few blocks. Finally just as a fun fact, I was looking for the
    /// "perfect" world (and I still am), but after generating almost 300 new worlds (and saved all
    /// their maps as images with this application), I picked the seed "1.502", which had all the
    /// biomes, but I also discarded 3 other seeds that were almost the same one, strange isn't it?
    /// That has now kept me thinking about why I liked that one but not the others if were the same,
    /// only that I still didn't know they were almost the same...
    /// Also at the moment (2019_10_31_18_16_17_956), the lowest seed I've found is "95", and the
    /// highest one is "32.726". So it seems that are always positive and within the range of a
    /// short (Int16) maximum value. From a total of 278 saved maps, 192 were "similar" to other maps.
    /// But on the positive side, now you know that you can literally select the seed after only a few
    /// dozens of tries as a maximum, since they will almost be the same due to the very few options.
    /// </summary>
    public partial class Ventana_Semillas_Parecidas : Form
    {
        public Ventana_Semillas_Parecidas()
        {
            InitializeComponent();
        }

        internal readonly string Texto_Título = "Similar Seeds for " + Program.Texto_Usuario + " by Jupisoft";
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
        internal static readonly string Ruta_Mapas = Application.StartupPath + "\\Seeds";
        internal PictureBox[] Matriz_Pictures = null;
        internal static int Variable_Semilla = 0;
        internal static bool Variable_Mostrar_Mapas_Originales = false;

        private void Ventana_Semillas_Parecidas_Load(object sender, EventArgs e)
        {
            try
            {
                PixARK_Seeds.Buscar_Semillas_Parecidas(); //string.Compare(Environment.UserName, "Jupisoft", true) == 0);
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                int Bytes_Únicos = 0;
                int Semillas_Parecidas = 0;
                int Semilla_Mínima = int.MaxValue;
                int Semilla_Máxima = int.MinValue;
                foreach (KeyValuePair<byte, List<int>> Entrada in PixARK_Seeds.Lista_Semilas_Parecidas)
                {
                    try
                    {
                        if (Entrada.Value != null && Entrada.Value.Count > 0)
                        {
                            Bytes_Únicos++;
                            foreach (int Semilla in Entrada.Value)
                            {
                                try
                                {
                                    if (Semilla < Semilla_Mínima) Semilla_Mínima = Semilla;
                                    if (Semilla > Semilla_Máxima) Semilla_Máxima = Semilla;
                                }
                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                            }
                            Semillas_Parecidas += Entrada.Value.Count;
                        }
                    }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                this.Text = Texto_Título + " - [Unique Bytes Found: " + Bytes_Únicos.ToString() + " of 256, Similar Seeds: " + Program.Traducir_Número(Semillas_Parecidas) + ", Seeds Range: " + Program.Traducir_Número(Semilla_Mínima) + " to " + Program.Traducir_Número(Semilla_Máxima) + "]";
                this.WindowState = FormWindowState.Maximized;
                Matriz_Pictures = new PictureBox[6]
                {
                    Picture_Mapa_1,
                    Picture_Mapa_2,
                    Picture_Mapa_3,
                    Picture_Mapa_4,
                    Picture_Mapa_5,
                    Picture_Mapa_6
                };
                //DataGridViewRow[] Matriz_Filas = new DataGridViewRow[256];
                for (int Índice_Byte = 0; Índice_Byte < 256; Índice_Byte++)
                {
                    string Texto_8_Bits = Convert.ToString((byte)Índice_Byte, 2);
                    if (Texto_8_Bits.Length < 8) Texto_8_Bits = new string('0', 8 - Texto_8_Bits.Length) + Texto_8_Bits;
                    List<int> Lista_Semillas_Predecidas = new List<int>();
                    for (int Índice = Índice_Byte; Índice < 32768; Índice += 256)
                    {
                        try
                        {
                            Lista_Semillas_Predecidas.Add(Índice);
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                    bool Existente = false;
                    foreach (KeyValuePair<byte, List<int>> Entrada in PixARK_Seeds.Lista_Semilas_Parecidas)
                    {
                        try
                        {
                            if (Entrada.Key == Índice_Byte)
                            {
                                List<string> Lista_Semillas_Binarias = new List<string>();
                                foreach (int Semilla in Entrada.Value)
                                {
                                    try
                                    {
                                        Lista_Semillas_Binarias.Add(Convert.ToString(Semilla, 2));
                                    }
                                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                }
                                DataGridView_Principal.Rows.Add(new object[] { Program.Obtener_Imagen_Byte_2D(Entrada.Key), Entrada.Key, Texto_8_Bits, Entrada.Value.Count, Program.Traducir_Lista_Variables(Entrada.Value), Program.Traducir_Lista_Variables(Lista_Semillas_Binarias), Program.Traducir_Lista_Variables(Lista_Semillas_Predecidas) });
                                /*DataGridViewRow Fila = new DataGridViewRow();
                                Fila.DefaultCellStyle = DataGridView_Principal.DefaultCellStyle;
                                Fila.SetValues(new object[] { Program.Obtener_Imagen_Byte_2D(Entrada.Key), Entrada.Key, Texto_8_Bits, Entrada.Value.Count, Program.Traducir_Lista_Variables(Entrada.Value), Program.Traducir_Lista_Variables(Lista_Semillas_Binarias), Program.Traducir_Lista_Variables(Lista_Semillas_Predecidas) });
                                Matriz_Filas[Índice_Byte] = Fila;*/
                                Lista_Semillas_Binarias = null;
                                Existente = true;
                                break;
                            }
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                    if (!Existente)
                    {
                        DataGridView_Principal.Rows.Add(new object[] { Program.Obtener_Imagen_Byte_2D(Índice_Byte), (byte)Índice_Byte, Texto_8_Bits, 0, string.Empty, string.Empty, Program.Traducir_Lista_Variables(Lista_Semillas_Predecidas) });
                        /*DataGridViewRow Fila = new DataGridViewRow();
                        Fila.DefaultCellStyle = DataGridView_Principal.DefaultCellStyle;
                        Fila.SetValues(new object[] { Program.Obtener_Imagen_Byte_2D(Índice_Byte), (byte)Índice_Byte, Texto_8_Bits, 0, string.Empty, string.Empty, Program.Traducir_Lista_Variables(Lista_Semillas_Predecidas) });
                        Matriz_Filas[Índice_Byte] = Fila;*/
                    }
                    Lista_Semillas_Predecidas = null;
                    Texto_8_Bits = null;
                }
                //DataGridView_Principal.Rows.AddRange(Matriz_Filas);
                //DataGridView_Principal.Sort(Columna_Último_Byte, ListSortDirection.Ascending);
                DataGridView_Principal.Sort(Columna_Semillas, ListSortDirection.Descending);
                DataGridView_Principal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                DataGridView_Principal.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                DataGridView_Principal.Rows[0].Cells[Columna_Semillas.Index].Selected = true;
                NumericUpDown_Semilla.Value = Variable_Semilla;
                Ocupado = true;
                Registro_Cargar_Opciones();
                Ocupado = false;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Semillas_Parecidas_Shown(object sender, EventArgs e)
        {
            try
            {
                /*if (string.Compare(Environment.UserName, "Jupisoft", true) == 0)
                {
                    Buscar_Semillas_Parecidas(); // Only for me, update the similar seed list.
                }*/
                this.Activate();
                Temporizador_Principal.Start();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Semillas_Parecidas_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Semillas_Parecidas_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Semillas_Parecidas_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Semillas_Parecidas_KeyDown(object sender, KeyEventArgs e)
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

        private void DataGridView_Principal_KeyDown(object sender, KeyEventArgs e)
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
                        if (DataGridView_Principal.SelectedRows != null && DataGridView_Principal.SelectedRows.Count > 0)
                        {
                            Program.Crear_Carpetas(Ruta_Mapas);
                            int Valor = (byte)DataGridView_Principal.SelectedRows[0].Cells[Columna_Último_Byte.Index].Value;
                            for (int Índice = 0; Índice < PixARK_Seeds.Lista_Semilas_Parecidas.Count; Índice++)
                            {
                                try
                                {
                                    if (PixARK_Seeds.Lista_Semilas_Parecidas[Índice].Key == Valor)
                                    {
                                        foreach (int Semilla in PixARK_Seeds.Lista_Semilas_Parecidas[Índice].Value)
                                        {
                                            try
                                            {
                                                Program.Ejecutar_Ruta(Ruta_Mapas + "\\" + Semilla.ToString() + ".png", ProcessWindowStyle.Maximized);
                                            }
                                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                        }
                                        break;
                                    }
                                }
                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                            }
                        }
                    }
                }
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
                Program.Crear_Carpetas(Ruta_Mapas);
                Program.Ejecutar_Ruta(Ruta_Mapas, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mostrar_Mapas_Originales_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Mostrar_Mapas_Originales = Menú_Contextual_Mostrar_Mapas_Originales.Checked;
                foreach (PictureBox Picture in Matriz_Pictures)
                {
                    try
                    {
                        Picture.SizeMode = !Variable_Mostrar_Mapas_Originales ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.CenterImage;
                    }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
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

        private void NumericUpDown_Semilla_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string Texto_Bits = Convert.ToString((long)NumericUpDown_Semilla.Value, 2);
                if (Texto_Bits.Length < 8) Texto_Bits = new string('0', 8 - Texto_Bits.Length) + Texto_Bits;
                TextBox_Semilla_Binaria.Text = Texto_Bits;
                Texto_Bits = Texto_Bits.Substring(Texto_Bits.Length - 8);
                TextBox_Semilla_8_Bits_Binaria.Text = Texto_Bits;
                byte Valor = Convert.ToByte(Texto_Bits, 2);
                TextBox_Semilla_8_Bits.Text = Valor.ToString();
                Texto_Bits = null;
                if (DataGridView_Principal.Rows != null && DataGridView_Principal.Rows.Count > 0)
                {
                    foreach (DataGridViewRow Fila in DataGridView_Principal.Rows)
                    {
                        if (Fila != null && (byte)Fila.Cells[Columna_Último_Byte.Index].Value == Valor)
                        {
                            Fila.Cells[Columna_Semillas.Index].Selected = true;
                            return;
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void DataGridView_Principal_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                for (int Índice = 0; Índice < Matriz_Pictures.Length; Índice++)
                {
                    Matriz_Pictures[Índice].Image = null;
                }
                if (DataGridView_Principal.SelectedRows != null && DataGridView_Principal.SelectedRows.Count > 0)
                {
                    Program.Crear_Carpetas(Ruta_Mapas);
                    int Valor = (byte)DataGridView_Principal.SelectedRows[0].Cells[Columna_Último_Byte.Index].Value;
                    for (int Índice = 0; Índice < PixARK_Seeds.Lista_Semilas_Parecidas.Count; Índice++)
                    {
                        try
                        {
                            if (PixARK_Seeds.Lista_Semilas_Parecidas[Índice].Key == Valor)
                            {
                                for (int Índice_Semilla = 0; Índice_Semilla < Math.Min(PixARK_Seeds.Lista_Semilas_Parecidas[Índice].Value.Count, Matriz_Pictures.Length); Índice_Semilla++)
                                {
                                    try
                                    {
                                        Matriz_Pictures[Índice_Semilla].Image = Program.Cargar_Imagen_Ruta(Ruta_Mapas + "\\" + PixARK_Seeds.Lista_Semilas_Parecidas[Índice].Value[Índice_Semilla].ToString() + ".png", CheckState.Indeterminate);
                                    }
                                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                }
                                break;
                            }
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}

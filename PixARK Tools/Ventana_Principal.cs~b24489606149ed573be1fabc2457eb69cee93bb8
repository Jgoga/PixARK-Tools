using Finisar.SQLite;
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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixARK_Tools
{
    public partial class Ventana_Principal : Form
    {
        public Ventana_Principal()
        {
            InitializeComponent();
        }

        // TODO: draw at least 1 pixel where there are biomes with ore resources to spot them better.
        // TODO: decipher how the "Guid" from the worlds name is generated and reverse that process.
        // TODO: add the change log window, by just porting the one from the "Minecraft Tools" project.

        /// <summary>
        /// Comparer used to sort the worlds by name in the main ComboBox.
        /// </summary>
        internal class Comparador_KeyValuePair_String_String : IComparer<KeyValuePair<string, string>>
        {
            public int Compare(KeyValuePair<string, string> X, KeyValuePair<string, string> Y)
            {
                return string.Compare(X.Value, Y.Value, false);
            }
        }

        internal readonly string Texto_Título = "PixARK Tools for " + Program.Texto_Usuario + " by Jupisoft";
        internal static bool Variable_Siempre_Visible = false;
        internal static bool Variable_Pantalla_Completa = false;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal static Stopwatch FPS_Cronómetro = Stopwatch.StartNew();
        internal long FPS_Segundo_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;
        internal bool Ocupado = false;
        /// <summary>
        /// The seed of the currently loaded PixARK world. If it's zero, then it shouldn't be a world loaded.
        /// </summary>
        internal int Semilla = 0;
        /// <summary>
        /// Image that stores the original world map extracted and converted from the
        /// "terrain.db" file from any PixARK world, it should have 5.120 x 5.120
        /// pixels including the world border by default, so it's hard too see all the
        /// details using only the main PictureBox, so it includes several functions to
        /// export this image with or without the world borders to see it completely.
        /// </summary>
        internal Bitmap Imagen_Biomas_Original = null;
        /// <summary>
        /// "Copy" of the previous image, but without showing any ore biomes, this is the closest
        /// image to the one shown in game in PixARK.
        /// </summary>
        internal Bitmap Imagen_Biomas_Original_Simple = null;
        /// <summary>
        /// Image that stores the original world height map, based on the found chunks.
        /// </summary>
        internal Bitmap Imagen_Altura_Original = null;
        /// <summary>
        /// Image that stores the original rainbow world height map, based on the found chunks.
        /// </summary>
        internal Bitmap Imagen_Arco_Iris_Original = null;
        /// <summary>
        /// Image used to store a shrinked copy of the world map, that is used to show it
        /// on the main PictureBox and also to get it's pixels and see over what biome is
        /// the mouse cursor within the PictureBox, this should help to quickly identify
        /// all the biomes, while also saving a lot of CPU in the process.
        /// </summary>
        internal Bitmap Imagen_Picture = null;
        internal Dictionary<string, string> Diccionario_Mundos_Nombres = new Dictionary<string, string>();
        internal static bool Variable_Cargar_Automáticamente = true;
        internal static bool Variable_Ocultar_Bordes = true;
        internal static bool Variable_Mostrar_Minerales = true;
        internal static bool Variable_Mostrar_Regla = true;
        internal static bool Variable_Mostrar_Lista_Trucos = false;
        internal static int Variable_Mostrar_Mapa = 0;
        internal PictureBox[] Matriz_Pictures = null;
        internal Label[] Matriz_Etiquetas = null;
        internal Color Color_ARGB_Bioma = Color.Empty;

        private void Ventana_Principal_Load(object sender, EventArgs e)
        {
            try
            {
                if (Program.Icono_Jupisoft == null) Program.Icono_Jupisoft = this.Icon.Clone() as Icon;
                this.Text = Texto_Título + " - [Drag and drop any world save folder here to load it]";
                this.WindowState = FormWindowState.Maximized;
                Matriz_Pictures = new PictureBox[24]
                {
                    Picture_Leyenda_0,
                    Picture_Leyenda_1,
                    Picture_Leyenda_2,
                    Picture_Leyenda_3,
                    Picture_Leyenda_4,
                    Picture_Leyenda_5,
                    Picture_Leyenda_6,
                    Picture_Leyenda_7,
                    Picture_Leyenda_8,
                    Picture_Leyenda_9,
                    Picture_Leyenda_10,
                    Picture_Leyenda_11,
                    null,
                    Picture_Leyenda_13,
                    Picture_Leyenda_14,
                    Picture_Leyenda_15,
                    Picture_Leyenda_16,
                    Picture_Leyenda_17,
                    Picture_Leyenda_18,
                    null,
                    Picture_Leyenda_20,
                    Picture_Leyenda_21,
                    Picture_Leyenda_22,
                    Picture_Leyenda_23,
                };
                Matriz_Etiquetas = new Label[24]
                {
                    Etiqueta_Leyenda_0,
                    Etiqueta_Leyenda_1,
                    Etiqueta_Leyenda_2,
                    Etiqueta_Leyenda_3,
                    Etiqueta_Leyenda_4,
                    Etiqueta_Leyenda_5,
                    Etiqueta_Leyenda_6,
                    Etiqueta_Leyenda_7,
                    Etiqueta_Leyenda_8,
                    Etiqueta_Leyenda_9,
                    Etiqueta_Leyenda_10,
                    Etiqueta_Leyenda_11,
                    null,
                    Etiqueta_Leyenda_13,
                    Etiqueta_Leyenda_14,
                    Etiqueta_Leyenda_15,
                    Etiqueta_Leyenda_16,
                    Etiqueta_Leyenda_17,
                    Etiqueta_Leyenda_18,
                    null,
                    Etiqueta_Leyenda_20,
                    Etiqueta_Leyenda_21,
                    Etiqueta_Leyenda_22,
                    Etiqueta_Leyenda_23,
                };
                for (int Índice = 0; Índice < 24; Índice++)
                {
                    if (Índice != 12 &&
                        Índice != 19 &&
                        Índice < Matriz_Etiquetas.Length &&
                        Índice < PixARK.Biomas.Matriz_Biomas.Length)
                    {
                        Matriz_Pictures[Índice].BackColor = PixARK.Biomas.Matriz_Biomas[Índice].Color;
                        Matriz_Etiquetas[Índice].ForeColor = Color.Black;
                        Matriz_Etiquetas[Índice].Text = PixARK.Biomas.Matriz_Biomas[Índice].Índice.ToString() + ": " + (string.Compare(PixARK.Biomas.Matriz_Biomas[Índice].Nombre, PixARK.Biomas.Matriz_Biomas[Índice].Nombre_Simple, true) != 0 ? PixARK.Biomas.Matriz_Biomas[Índice].Nombre_Simple + (!PixARK.Biomas.Matriz_Biomas[Índice].Nombre_Simple.EndsWith("s") ? "'s " : "' ") : null) + PixARK.Biomas.Matriz_Biomas[Índice].Nombre + (PixARK.Biomas.Matriz_Biomas[Índice].Minerales != PixARK.Minerales.Unknown ? " (" + PixARK.Biomas.Matriz_Biomas[Índice].Minerales.ToString().Replace('_', ' ') + ")" : null) + ".";
                    }
                }
                CheckBox_Cargar_Automáticamente.Checked = Variable_Cargar_Automáticamente;
                ListBox_Objeto.Items.Add("0: None");
                if (PixARK_Cheats.Matriz_Objetos != null && PixARK_Cheats.Matriz_Objetos.Length > 0)
                {
                    for (int Índice_Objeto = 0; Índice_Objeto < PixARK_Cheats.Matriz_Objetos.Length; Índice_Objeto++)
                    {
                        ListBox_Objeto.Items.Add(Program.Traducir_Número(Índice_Objeto + 1) + ": " + PixARK_Cheats.Matriz_Objetos[Índice_Objeto]);
                    }
                    //ListBox_Objeto.Items.AddRange(PixARK_Cheats.Matriz_Objetos);
                }
                if (ListBox_Objeto.Items.Count >= 913) ListBox_Objeto.SelectedIndex = 913;
                else ListBox_Objeto.SelectedIndex = 0;
                if (ComboBox_Truco.Items.Count > 0) ComboBox_Truco.SelectedIndex = 0;
                Menú_Contextual_Siempre_Visible.Checked = Variable_Siempre_Visible;
                Menú_Contextual_Ocultar_Bordes.Checked = Variable_Ocultar_Bordes;
                Menú_Contextual_Mostrar_Minerales.Checked = Variable_Mostrar_Minerales;
                Menú_Contextual_Mostrar_Regla.Checked = Variable_Mostrar_Regla;
                Menú_Contextual_Mostrar_Lista_Trucos.Checked = Variable_Mostrar_Lista_Trucos;
                Barra_Estado_Etiqueta_Bioma.Image = Program.Crear_Imagen_Color_Fondo(Color.Gray);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Principal_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
                this.Refresh(); // Avoid weirdness when first loading a map.
                Temporizador_Principal.Start();
                // The world names seem to be a "Guid", so try to decipher them.
                // Result: failure, at least for now.
                /*byte[] q = new byte[16];
                BitConverter.GetBytes(20947).CopyTo(q, 0);
                Guid Gd = new Guid(q);
                MessageBox.Show(this, Gd.ToString());
                q = null;*/
                foreach (string Ruta in PixARK.Matriz_Rutas_PixARK)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(Ruta))
                        {
                            ComboBox_Ruta_PixARK.Items.Add(Ruta); // Add al the known paths.
                            if (Directory.Exists(Ruta))
                            {
                                if (string.IsNullOrEmpty(ComboBox_Ruta_PixARK.Text))
                                {
                                    ComboBox_Ruta_PixARK.Text = Ruta; // Save the first valid match.
                                }
                            }
                        }
                    }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Principal_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, true) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Principal_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                {
                    string[] Matriz_Rutas = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(Ruta) && (Directory.Exists(Ruta) || File.Exists(Ruta)))
                                {
                                    string Ruta_Base = Directory.Exists(Ruta) ? Ruta : Path.GetDirectoryName(Ruta);
                                    bool Mundo_Válido = false;
                                    try
                                    {
                                        if (File.Exists(Ruta_Base + "\\terrain.db"))
                                        {
                                            Mundo_Válido = true; // It should be a valid world folder (hopefully).
                                        }
                                        else
                                        {
                                            // I found out that the world folder names seem to be "Guid", so try to load it.
                                            Guid Identificador = new Guid(Path.GetFileName(Ruta_Base));
                                            Mundo_Válido = true; // It should be a valid world folder (hopefully).
                                        }
                                    }
                                    catch { Mundo_Válido = false; }
                                    if (Mundo_Válido) // It should be a valid world folder.
                                    {
                                        // Avoid loading multiple worlds.
                                        bool Cargar_Automáticamente = CheckBox_Cargar_Automáticamente.Checked;
                                        CheckBox_Cargar_Automáticamente.Checked = false;
                                        string Ruta_PixARK = Path.GetDirectoryName(Ruta_Base);
                                        ComboBox_Ruta_PixARK.Text = Ruta_PixARK;
                                        ComboBox_Ruta_PixARK_SelectedIndexChanged(ComboBox_Ruta_PixARK, EventArgs.Empty);
                                        string Ruta_Mundo = Ruta_Base;
                                        ComboBox_Mundo_PixARK.Text = Ruta_Mundo;
                                        CheckBox_Cargar_Automáticamente.Checked = Cargar_Automáticamente;
                                        ComboBox_Mundo_PixARK_SelectedIndexChanged(ComboBox_Mundo_PixARK, EventArgs.Empty);
                                    }
                                    else
                                    {
                                        // Assume it's a folder with at least one world subfolder inside.
                                        ComboBox_Ruta_PixARK.Text = Ruta_Base;
                                        ComboBox_Ruta_PixARK_SelectedIndexChanged(ComboBox_Ruta_PixARK, EventArgs.Empty);
                                    }
                                    Ruta_Base = null;
                                    break;
                                }
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                        Matriz_Rutas = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Principal_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState != FormWindowState.Minimized)
                {
                    Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Ventana_Principal_KeyDown(object sender, KeyEventArgs e)
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

        private void ComboBox_Ruta_PixARK_KeyDown(object sender, KeyEventArgs e)
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
                        ComboBox_Ruta_PixARK_SelectedIndexChanged(ComboBox_Ruta_PixARK, EventArgs.Empty);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Mundo_PixARK_KeyDown(object sender, KeyEventArgs e)
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
                        ComboBox_Mundo_PixARK_SelectedIndexChanged(ComboBox_Mundo_PixARK, EventArgs.Empty);
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Barra_Estado_Botón_Excepción_Click(object sender, EventArgs e)
        {
            try
            {
                if (Variable_Pantalla_Completa) Cursor.Show();
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
                if (Variable_Pantalla_Completa) Cursor.Hide();
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

        private void ComboBox_Ruta_PixARK_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Ruta_PixARK.Refresh();
                Semilla = 0;
                Imagen_Biomas_Original = null;
                Imagen_Biomas_Original_Simple = null;
                Imagen_Altura_Original = null;
                Imagen_Arco_Iris_Original = null;
                this.Text = Texto_Título + " - [Drag and drop any world save folder here to load it]";
                ComboBox_Mundo_PixARK.Items.Clear();
                ComboBox_Mundo_PixARK.Text = null;
                Establecer_Imagen_Picture(null, Color_ARGB_Bioma);
                Barra_Estado_Etiqueta_Semilla.Text = "Seed: ?";
                Barra_Estado_Etiqueta_Mundo.Text = "World: ?";
                if (!string.IsNullOrEmpty(ComboBox_Ruta_PixARK.Text) && Directory.Exists(ComboBox_Ruta_PixARK.Text))
                {
                    // First try to load the list that should contain the names for all the saved PixARK worlds.
                    List<KeyValuePair<string, string>> Lista_Mundos_Nombres = new List<KeyValuePair<string, string>>();
                    try
                    {
                        Diccionario_Mundos_Nombres.Clear();
                        string Ruta_MapSaves_INI = ComboBox_Ruta_PixARK.Text + "\\MapSaves.ini";
                        if (File.Exists(Ruta_MapSaves_INI))
                        {
                            FileStream Lector = new FileStream(Ruta_MapSaves_INI, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            if (Lector != null)
                            {
                                if (Lector.Length > 0L)
                                {
                                    Lector.Seek(0L, SeekOrigin.Begin);
                                    StreamReader Lector_Texto = new StreamReader(Lector, Encoding.UTF8, true);
                                    if (Lector_Texto != null)
                                    {
                                        while (!Lector_Texto.EndOfStream)
                                        {
                                            try
                                            {
                                                string Línea = Lector_Texto.ReadLine();
                                                if (!string.IsNullOrEmpty(Línea))
                                                {
                                                    string[] Matriz_Líneas = Línea.Trim().Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                                    if (Matriz_Líneas != null && Matriz_Líneas.Length >= 2 && !string.IsNullOrEmpty(Matriz_Líneas[0]) && !string.IsNullOrEmpty(Matriz_Líneas[1]))
                                                    {
                                                        Diccionario_Mundos_Nombres.Add(Matriz_Líneas[0].Trim(), Matriz_Líneas[1].Trim());
                                                        Lista_Mundos_Nombres.Add(new KeyValuePair<string, string>(Matriz_Líneas[0].Trim(), Matriz_Líneas[1].Trim()));
                                                    }
                                                    Matriz_Líneas = null;
                                                }
                                            }
                                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                        }
                                        Lector_Texto.Close();
                                        Lector_Texto.Dispose();
                                        Lector_Texto = null;
                                    }
                                }
                                Lector.Close();
                                Lector.Dispose();
                                Lector = null;
                            }
                        }
                    }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                    if (Lista_Mundos_Nombres.Count > 1) Lista_Mundos_Nombres.Sort(new Comparador_KeyValuePair_String_String());
                    if (Lista_Mundos_Nombres.Count > 0) // Risk it with the sorted worlds list (ignore the existing folders).
                    {
                        foreach (KeyValuePair<string, string> Entrada in Lista_Mundos_Nombres)
                        {
                            try
                            {
                                if (Directory.Exists(ComboBox_Ruta_PixARK.Text + "\\" + Entrada.Key))
                                {
                                    ComboBox_Mundo_PixARK.Items.Add(ComboBox_Ruta_PixARK.Text + "\\" + Entrada.Key);
                                }
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                        // Now add also any other existing folder not found in the world list.
                        string[] Matriz_Rutas = Directory.GetDirectories(ComboBox_Ruta_PixARK.Text, "*", SearchOption.TopDirectoryOnly);
                        if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                        {
                            if (Matriz_Rutas.Length > 1) Array.Sort(Matriz_Rutas);
                            foreach (string Ruta in Matriz_Rutas)
                            {
                                try
                                {
                                    string Nombre = Path.GetFileName(Ruta);
                                    bool Repetido = false;
                                    foreach (KeyValuePair<string, string> Entrada in Lista_Mundos_Nombres)
                                    {
                                        try
                                        {
                                            if (string.Compare(Entrada.Key, Nombre, true) == 0)
                                            {
                                                Repetido = true;
                                                break;
                                            }
                                        }
                                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                    }
                                    if (!Repetido) ComboBox_Mundo_PixARK.Items.Add(Ruta);
                                }
                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                            }
                        }
                        Matriz_Rutas = null;
                    }
                    else // Couldn't sort the worlds by their name.
                    {
                        string[] Matriz_Rutas = Directory.GetDirectories(ComboBox_Ruta_PixARK.Text, "*", SearchOption.TopDirectoryOnly);
                        if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                        {
                            if (Matriz_Rutas.Length > 1) Array.Sort(Matriz_Rutas);
                            foreach (string Ruta in Matriz_Rutas)
                            {
                                try { ComboBox_Mundo_PixARK.Items.Add(Ruta); }
                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                            }
                        }
                        Matriz_Rutas = null;
                    }
                    Lista_Mundos_Nombres = null;
                    if (ComboBox_Mundo_PixARK.Items.Count > 0) // See if we found any world and load it.
                    {
                        ComboBox_Mundo_PixARK.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Mundo_PixARK_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Mundo_PixARK.Refresh();
                if (Variable_Cargar_Automáticamente) Botón_Cargar.PerformClick();
                else
                {
                    Semilla = 0;
                    Imagen_Biomas_Original = null;
                    Imagen_Biomas_Original_Simple = null;
                    Imagen_Altura_Original = null;
                    Imagen_Arco_Iris_Original = null;
                    this.Text = Texto_Título + " - [Drag and drop any world save folder here to load it]";
                    Establecer_Imagen_Picture(null, Color_ARGB_Bioma);
                    Grupo_Biomas_Fáciles.Text = "Easy difficulty main biomes";
                    Grupo_Biomas_Medios.Text = "Medium difficulty main biomes";
                    Grupo_Biomas_Difíciles.Text = "Hard difficulty main biomes";
                    Grupo_Recursos_Fáciles.Text = "Easy difficulty resources biomes";
                    Grupo_Recursos_Medios.Text = "Medium difficulty resources biomes";
                    Grupo_Recursos_Difíciles.Text = "Hard difficulty resources biomes";
                    for (int Índice = 0; Índice < 24; Índice++)
                    {
                        if (Índice != 12 &&
                            Índice != 19 &&
                            Índice < Matriz_Etiquetas.Length &&
                            Índice < PixARK.Biomas.Matriz_Biomas.Length)
                        {
                            Matriz_Etiquetas[Índice].ForeColor = Color.Black;
                            Matriz_Etiquetas[Índice].Text = PixARK.Biomas.Matriz_Biomas[Índice].Índice.ToString() + ": " + (string.Compare(PixARK.Biomas.Matriz_Biomas[Índice].Nombre, PixARK.Biomas.Matriz_Biomas[Índice].Nombre_Simple, true) != 0 ? PixARK.Biomas.Matriz_Biomas[Índice].Nombre_Simple + (!PixARK.Biomas.Matriz_Biomas[Índice].Nombre_Simple.EndsWith("s") ? "'s " : "' ") : null) + PixARK.Biomas.Matriz_Biomas[Índice].Nombre + (PixARK.Biomas.Matriz_Biomas[Índice].Minerales != PixARK.Minerales.Unknown ? " (" + PixARK.Biomas.Matriz_Biomas[Índice].Minerales.ToString().Replace('_', ' ') + ")" : null) + ".";
                        }
                    }
                    TextBox_Análisis.Text = null;
                    Barra_Estado_Etiqueta_Semilla.Text = "Seed: ?";
                    Barra_Estado_Etiqueta_Mundo.Text = "World: ?";
                    Barra_Estado_Etiqueta_Minerales.Text = "Ores: 0,00 %";
                    Barra_Estado_Etiqueta_Fácil.Text = "0,00 %";
                    Barra_Estado_Etiqueta_Media.Text = "0,00 %";
                    Barra_Estado_Etiqueta_Difícil.Text = "0,00 %";
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Cargar_Automáticamente_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Cargar_Automáticamente = CheckBox_Cargar_Automáticamente.Checked;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Explorar_Click(object sender, EventArgs e)
        {
            try
            {
                Ventana_Visor_Bases_Datos Ventana = new Ventana_Visor_Bases_Datos();
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                if (ComboBox_Mundo_PixARK.Items.Count > 0)
                {
                    foreach (string Ruta in ComboBox_Mundo_PixARK.Items)
                    {
                        Ventana.ComboBox_Ruta.Items.Add(Ruta + "\\terrain.db");
                    }
                }
                if (Ventana.ShowDialog(this) == DialogResult.OK)
                {
                    // ...
                }
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Cargar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Semilla = 0;
                Imagen_Biomas_Original = null;
                Imagen_Biomas_Original_Simple = null;
                Imagen_Altura_Original = null;
                Imagen_Arco_Iris_Original = null;
                this.Text = Texto_Título + " - [Drag and drop any world save folder here to load it]";
                Establecer_Imagen_Picture(null, Color_ARGB_Bioma);
                Grupo_Biomas_Fáciles.Text = "Easy difficulty main biomes";
                Grupo_Biomas_Medios.Text = "Medium difficulty main biomes";
                Grupo_Biomas_Difíciles.Text = "Hard difficulty main biomes";
                Grupo_Recursos_Fáciles.Text = "Easy difficulty resources biomes";
                Grupo_Recursos_Medios.Text = "Medium difficulty resources biomes";
                Grupo_Recursos_Difíciles.Text = "Hard difficulty resources biomes";
                for (int Índice = 0; Índice < 24; Índice++)
                {
                    if (Índice != 12 &&
                        Índice != 19 &&
                        Índice < Matriz_Etiquetas.Length &&
                        Índice < PixARK.Biomas.Matriz_Biomas.Length)
                    {
                        Matriz_Etiquetas[Índice].ForeColor = Color.Black;
                        Matriz_Etiquetas[Índice].Text = PixARK.Biomas.Matriz_Biomas[Índice].Índice.ToString() + ": " + (string.Compare(PixARK.Biomas.Matriz_Biomas[Índice].Nombre, PixARK.Biomas.Matriz_Biomas[Índice].Nombre_Simple, true) != 0 ? PixARK.Biomas.Matriz_Biomas[Índice].Nombre_Simple + (!PixARK.Biomas.Matriz_Biomas[Índice].Nombre_Simple.EndsWith("s") ? "'s " : "' ") : null) + PixARK.Biomas.Matriz_Biomas[Índice].Nombre + (PixARK.Biomas.Matriz_Biomas[Índice].Minerales != PixARK.Minerales.Unknown ? " (" + PixARK.Biomas.Matriz_Biomas[Índice].Minerales.ToString().Replace('_', ' ') + ")" : null) + ".";
                    }
                }
                TextBox_Análisis.Text = null;
                Barra_Estado_Etiqueta_Semilla.Text = "Seed: ?";
                Barra_Estado_Etiqueta_Mundo.Text = "World: ?";
                Barra_Estado_Etiqueta_Minerales.Text = "Ores: 0,00 %";
                Barra_Estado_Etiqueta_Fácil.Text = "0,00 %";
                Barra_Estado_Etiqueta_Media.Text = "0,00 %";
                Barra_Estado_Etiqueta_Difícil.Text = "0,00 %";
                this.Refresh();
                if (!string.IsNullOrEmpty(ComboBox_Mundo_PixARK.Text) && Directory.Exists(ComboBox_Mundo_PixARK.Text))
                {
                    string Nombre = Path.GetFileName(ComboBox_Mundo_PixARK.Text);
                    if (Diccionario_Mundos_Nombres.ContainsKey(Nombre)) Barra_Estado_Etiqueta_Mundo.Text = "World: \"" + Diccionario_Mundos_Nombres[Nombre] + "\"";
                    int Ancho = PixARK.Dimensiones_Mundo; // Start with the default world size.
                    int Alto = PixARK.Dimensiones_Mundo;
                    try
                    {
                        // First try to get the world X and Z dimensions to correctly build the map.
                        string Ruta_World_INI = ComboBox_Mundo_PixARK.Text + "\\World.ini";
                        if (File.Exists(Ruta_World_INI))
                        {
                            FileStream Lector = new FileStream(Ruta_World_INI, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            if (Lector != null)
                            {
                                if (Lector.Length > 0L)
                                {
                                    Lector.Seek(0L, SeekOrigin.Begin);
                                    StreamReader Lector_Texto = new StreamReader(Lector, Encoding.UTF8, true);
                                    if (Lector_Texto != null)
                                    {
                                        while (!Lector_Texto.EndOfStream)
                                        {
                                            try
                                            {
                                                string Línea = Lector_Texto.ReadLine();
                                                if (!string.IsNullOrEmpty(Línea))
                                                {
                                                    Línea = Línea.ToLowerInvariant().Trim();
                                                    if (Línea.Contains("worldwidth")) // WorldWidth, X.
                                                    {
                                                        int Índice_Igual = Línea.IndexOf('=');
                                                        if (Índice_Igual > -1)
                                                        {
                                                            List<char> Lista_Caracteres = new List<char>();
                                                            for (int Índice_Caracter = Índice_Igual + 1; Índice_Caracter < Línea.Length; Índice_Caracter++)
                                                            {
                                                                if (char.IsDigit(Línea[Índice_Caracter]))
                                                                {
                                                                    Lista_Caracteres.Add(Línea[Índice_Caracter]);
                                                                }
                                                            }
                                                            if (Lista_Caracteres.Count > 0)
                                                            {
                                                                try { Ancho = int.Parse(new string(Lista_Caracteres.ToArray())); }
                                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Ancho = PixARK.Dimensiones_Mundo; }
                                                            }
                                                            Lista_Caracteres = null;
                                                        }
                                                    }
                                                    else if (Línea.Contains("worldlength")) // WorldLength, Z (Y).
                                                    {
                                                        int Índice_Igual = Línea.IndexOf('=');
                                                        if (Índice_Igual > -1)
                                                        {
                                                            List<char> Lista_Caracteres = new List<char>();
                                                            for (int Índice_Caracter = Índice_Igual + 1; Índice_Caracter < Línea.Length; Índice_Caracter++)
                                                            {
                                                                if (char.IsDigit(Línea[Índice_Caracter]))
                                                                {
                                                                    Lista_Caracteres.Add(Línea[Índice_Caracter]);
                                                                }
                                                            }
                                                            if (Lista_Caracteres.Count > 0)
                                                            {
                                                                try { Alto = int.Parse(new string(Lista_Caracteres.ToArray())); }
                                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Alto = PixARK.Dimensiones_Mundo; }
                                                            }
                                                            Lista_Caracteres = null;
                                                        }
                                                    }
                                                    else if (Línea.Contains("serverseed")) // ServerSeed.
                                                    {
                                                        int Índice_Igual = Línea.IndexOf('=');
                                                        if (Índice_Igual > -1)
                                                        {
                                                            List<char> Lista_Caracteres = new List<char>();
                                                            for (int Índice_Caracter = Índice_Igual + 1; Índice_Caracter < Línea.Length; Índice_Caracter++)
                                                            {
                                                                if (char.IsDigit(Línea[Índice_Caracter]))
                                                                {
                                                                    Lista_Caracteres.Add(Línea[Índice_Caracter]);
                                                                }
                                                            }
                                                            if (Lista_Caracteres.Count > 0)
                                                            {
                                                                try
                                                                {
                                                                    Semilla = int.Parse(new string(Lista_Caracteres.ToArray()));
                                                                    Barra_Estado_Etiqueta_Semilla.Text = "Seed: " + Program.Traducir_Número(Semilla);
                                                                }
                                                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Semilla = 0; }
                                                            }
                                                            Lista_Caracteres = null;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                        }
                                        Lector_Texto.Close();
                                        Lector_Texto.Dispose();
                                        Lector_Texto = null;
                                    }
                                }
                                Lector.Close();
                                Lector.Dispose();
                                Lector = null;
                            }
                        }
                    }
                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                    // Now try to load the "terrain.db" file from this world, which contains the world map and chunks.
                    // I'm using the free library "Finisar SQLite", which works, but requires to be compiled with
                    // "unsafe", which is not desired, so feel free to replace the lbrary with any other taht might
                    // also work as expected. Also the library "Sqlite3.dll" is required near the main application.
                    string Ruta_Terrain_DB = ComboBox_Mundo_PixARK.Text + "\\terrain.db";
                    if (File.Exists(Ruta_Terrain_DB))
                    {
                        // SQL codes made around 2012, based on a WhatsApp database extractor for iPhone.
                        // Translated by me to make it work with PixARK, although I never used SQL before.
                        // I know it can also be linked to a DataGridView to explore the database contents.
                        // But first redirect the "DataError" function to avoid thousands of errors, and also
                        // I already tested it for the chunks, and it hung up almost 5 minutes until it showed
                        // thousands of chunks at the end, but they didn't seem to contain all the blocks from
                        // the world, but instead probably only the ones changed by the player and maybe even
                        // the plants randomly generated on the surface, because every chunk file I checked was
                        // only a few hundreds of bytes in length which I found unexpected, so now I'm assumming
                        // each time a chunk loads is generated from scratch based on the seed, and not saved
                        // anywhere unless it contains blocks changed by a player. Also I found the hard way that
                        // this game is not like Minecraft, when you die, and if you log off, your grave with it's
                        // items gets lost forever, so watch out for that and also I found you can drink Petroleum
                        // as it was water in a desert, which was very strange since it refilled al my water.
                        string Texto_Conexión = "Data Source=" + Ruta_Terrain_DB + ";New=False;Version=3";
                        SQLiteConnection Conexión_SQL = new SQLiteConnection(Texto_Conexión);
                        Conexión_SQL.Open();

                        // Get the final width and height of the world map.
                        Ancho += PixARK.Borde_Mundo_Doble;
                        Alto += PixARK.Borde_Mundo_Doble;
                        int Total_Píxeles = Ancho * Alto;
                        int Ancho_Mitad = Ancho / 2;
                        int Alto_Mitad = Alto / 2;

                        // Now try to read the biomes and resources world map.
                        string Texto_Comando = "Select * from " + PixARK.Texto_Clave_Mundo + ";";
                        SQLiteDataAdapter Adaptador_SQL = new SQLiteDataAdapter(Texto_Comando, Conexión_SQL); // Re-use the same connection.
                        DataSet Set_Datos = new DataSet();
                        Set_Datos.RemotingFormat = SerializationFormat.Binary;
                        Adaptador_SQL.Fill(Set_Datos, PixARK.Texto_Clave_Mundo);
                        // Code to extract the world chunks from the "terrain.db" file.
                        /*byte[] Matriz_Bytes = Set_Datos.Tables[0].Rows[1000]["chunk_data"] as byte[];
                        if (Matriz_Bytes != null && Matriz_Bytes.Length > 0)
                        {
                            File.WriteAllBytes(Application.StartupPath + "\\PixARK_Chunk_1000.bin", Matriz_Bytes);
                        }*/
                        // Code to extract the world map from the "terrain.db" file.
                        byte[] Matriz_Bytes_Biomas = Set_Datos.Tables[0].Rows[0]["data"] as byte[]; // This should be the map.
                        if (Matriz_Bytes_Biomas == null || Matriz_Bytes_Biomas.Length < Total_Píxeles)
                        {
                            // We failed to get the world biomes map, so make an empty one with deep ocean.
                            Matriz_Bytes_Biomas = new byte[Total_Píxeles];
                        }
                        int Índice_Byte_Bioma_Inicio = Math.Max(Matriz_Bytes_Biomas.Length - Total_Píxeles, 0);
                        // Close the SQL connection as soon as we get the world biomes byte array.
                        Set_Datos.Dispose();
                        Set_Datos = null;
                        Adaptador_SQL.Dispose();
                        Adaptador_SQL = null;

                        try
                        {
                            // Now try to read any existing chunk of 16 x 16 blocks and draw it's height.
                            Texto_Comando = "Select * from nx_chunks;";
                            Adaptador_SQL = new SQLiteDataAdapter(Texto_Comando, Conexión_SQL);
                            Set_Datos = new DataSet();
                            Set_Datos.RemotingFormat = SerializationFormat.Binary;
                            Adaptador_SQL.Fill(Set_Datos, "nx_chunks");

                            if (Set_Datos.Tables[0] != null &&
                                Set_Datos.Tables[0].Columns != null &&
                                Set_Datos.Tables[0].Columns.Count > 0 &&
                                //Set_Datos.Tables[0].Columns.Contains("chunk_data") &&
                                Set_Datos.Tables[0].Columns.Contains("chunk_posi") &&
                                Set_Datos.Tables[0].Columns.Contains("height_data") &&
                                //Set_Datos.Tables[0].Columns.Contains("top_block") &&
                                Set_Datos.Tables[0].Rows != null &&
                                Set_Datos.Tables[0].Rows.Count > 0) // Valid table, columns and rows.
                            {
                                //int Índice_Columna_Chunk = Set_Datos.Tables[0].Columns.IndexOf("chunk_data");
                                int Índice_Columna_Posición = Set_Datos.Tables[0].Columns.IndexOf("chunk_posi");
                                int Índice_Columna_Altura = Set_Datos.Tables[0].Columns.IndexOf("height_data");
                                //int Índice_Columna_Bloques = Set_Datos.Tables[0].Columns.IndexOf("top_block");
                                Imagen_Altura_Original = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                                BitmapData Bitmap_Data_Altura = Imagen_Altura_Original.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen_Altura_Original.PixelFormat);
                                byte[] Matriz_Bytes_ARGB_Altura = new byte[Math.Abs(Bitmap_Data_Altura.Stride) * Alto];
                                Marshal.Copy(Bitmap_Data_Altura.Scan0, Matriz_Bytes_ARGB_Altura, 0, Matriz_Bytes_ARGB_Altura.Length);
                                int Bytes_Aumento_Altura = Image.IsAlphaPixelFormat(Imagen_Altura_Original.PixelFormat) ? 4 : 3;
                                int Bytes_Diferencia_Altura = Math.Abs(Bitmap_Data_Altura.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen_Altura_Original.PixelFormat)) / 8);
                                int Bytes_Ancho_Altura = Math.Abs(Bitmap_Data_Altura.Stride);
                                //List<ushort> Lista_ID_Bloques = new List<ushort>();
                                List<byte> Lista_Bytes_Altura = new List<byte>();
                                //long Altura_Media = 0L;
                                //int Altura_Total = 0;
                                byte[] Matriz_Bytes_ARGB_Arco_Iris = Matriz_Bytes_ARGB_Altura.Clone() as byte[];
                                for (int Índice_Fila = 0; Índice_Fila < Set_Datos.Tables[0].Rows.Count; Índice_Fila++)
                                {
                                    string Texto_Posición = Set_Datos.Tables[0].Rows[Índice_Fila][Índice_Columna_Posición] as string;
                                    if (!string.IsNullOrEmpty(Texto_Posición) &&
                                        Texto_Posición.Contains(','))
                                    {
                                        string[] Matriz_Líneas = Texto_Posición.Replace("(", null).Replace(")", null).Replace(" ", null).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                        if (Matriz_Líneas != null && Matriz_Líneas.Length >= 2)
                                        {
                                            // We got the chunk position in the world, that should have a
                                            // range for a default world of 5.120 x 5.120 blocks of width /
                                            // 16 = (320 / 2) = 160 = [-160 to +159] for both X and Y.
                                            // So convert the chunk position to positive coordinates and
                                            // to world block coordinates.
                                            Point Posición = new Point((int.Parse(Matriz_Líneas[0]) * 16) + Ancho_Mitad, (int.Parse(Matriz_Líneas[1]) * 16) + Alto_Mitad);
                                            // Try to get the chunk data, which might contain the changed blocks in that chunk by the player
                                            // and perhaps also other things like grass or trees that have naturally grown there. Unconfirmed!
                                            // The first 6 bytes might have either coordinates or length, maybe as 16 bit values.
                                            // The next 256 bytes seem to be world height in a 16 x 16 format, decoded in deep ocean chunks.
                                            // The array always seems to have at least 1 extra byte, so it doesn't fit between 2 or 4 bytes.
                                            // It might store 2 or 3 coordinates for each block, either as byte, ushort or even int values.
                                            // Although this should end up being very inneficient, so I think it might store a full 16 x 16
                                            // "section" for each Y value and it might only add that "section" if a block in there was
                                            // changed somehow, although this is only a theory for now, since I know nothing about the format.
                                            //byte[] Matriz_Bytes_Chunk = Set_Datos.Tables[0].Rows[Índice_Fila][Índice_Columna_Chunk] as byte[];
                                            // Get the chunk height data as an array of bytes, it should have 256 bytes.
                                            // Where each byte means the world height ranges from 0 to 255.
                                            byte[] Matriz_Bytes_Altura = Set_Datos.Tables[0].Rows[Índice_Fila][Índice_Columna_Altura] as byte[];
                                            // Now get the byte array that might store the surface top block IDs.
                                            //byte[] Matriz_Bytes_Bloques = Set_Datos.Tables[0].Rows[Índice_Fila][Índice_Columna_Bloques] as byte[];
                                            if (Matriz_Bytes_Altura != null && Matriz_Bytes_Altura.Length >= 256/* &&
                                            Matriz_Bytes_Bloques != null && Matriz_Bytes_Bloques.Length >= 512*/)
                                            {
                                                for (int Índice_Y = 0, Índice_Byte_Altura = 0, Índice_Byte_Bloque = 0; Índice_Y < 16; Índice_Y++)
                                                {
                                                    for (int Índice_X = 0; Índice_X < 16; Índice_X++, Índice_Byte_Altura++, Índice_Byte_Bloque += 2)
                                                    {
                                                        // First try to get the top block ID, assume it uses 2 bytes per block.
                                                        /*ushort ID_Bloque = BitConverter.ToUInt16(Matriz_Bytes_Bloques, Índice_Byte_Bloque);
                                                        if (!Lista_ID_Bloques.Contains(ID_Bloque))
                                                        {
                                                            Lista_ID_Bloques.Add(ID_Bloque); // Why this always was zero on my testings... Unused?
                                                        }*/
                                                        // Use a gray color to fully show the height range where
                                                        // darker colors means less height and brighter ones means
                                                        // more hegiht. 95 might be the water height, so color it,
                                                        // but only if on that spot it should be either deep ocean
                                                        // or river biomes. This map can reveal ruins and other
                                                        // useful things like exposed caves and mountains, and even
                                                        // player built bases over the surface, etc.
                                                        int Índice_ARGB_Altura = ((Posición.Y + Índice_Y) * Bytes_Ancho_Altura) + ((Posición.X + Índice_X) * Bytes_Aumento_Altura);
                                                        //int Altura = Matriz_Bytes_Altura[Índice_Byte_Altura];

                                                        // Add all the unique heights found in a list for later "normalization".
                                                        if (!Lista_Bytes_Altura.Contains(Matriz_Bytes_Altura[Índice_Byte_Altura]))
                                                        {
                                                            Lista_Bytes_Altura.Add(Matriz_Bytes_Altura[Índice_Byte_Altura]);
                                                        }
                                                        // Also add each found height to find the average global height.
                                                        //Altura_Media += Matriz_Bytes_Altura[Índice_Byte_Altura];
                                                        //Altura_Total++;

                                                        // Store the height of each block in a pixel as gray scale.
                                                        Matriz_Bytes_ARGB_Altura[Índice_ARGB_Altura + 3] = 255;
                                                        Matriz_Bytes_ARGB_Altura[Índice_ARGB_Altura + 2] = Matriz_Bytes_Altura[Índice_Byte_Altura];
                                                        Matriz_Bytes_ARGB_Altura[Índice_ARGB_Altura + 1] = Matriz_Bytes_Altura[Índice_Byte_Altura];
                                                        Matriz_Bytes_ARGB_Altura[Índice_ARGB_Altura] = Matriz_Bytes_Altura[Índice_Byte_Altura];

                                                        Matriz_Bytes_ARGB_Arco_Iris[Índice_ARGB_Altura + 3] = 255;
                                                        Matriz_Bytes_ARGB_Arco_Iris[Índice_ARGB_Altura + 2] = Matriz_Bytes_Altura[Índice_Byte_Altura];
                                                        Matriz_Bytes_ARGB_Arco_Iris[Índice_ARGB_Altura + 1] = Matriz_Bytes_Altura[Índice_Byte_Altura];
                                                        Matriz_Bytes_ARGB_Arco_Iris[Índice_ARGB_Altura] = Matriz_Bytes_Altura[Índice_Byte_Altura];
                                                    }
                                                }
                                            }
                                            Matriz_Bytes_Altura = null;
                                        }
                                        Matriz_Líneas = null;
                                    }
                                }
                                // Now once we have drawn the height of the found chunks as gray scale, convert to final colors.
                                if (Lista_Bytes_Altura.Count > 0)
                                {
                                    //Altura_Media /= Altura_Total;
                                    if (Lista_Bytes_Altura.Count > 1) Lista_Bytes_Altura.Sort();
                                    // Now do a "normalization", first in gray scale and then to color.
                                    // Use an array of 256 bytes (one for each possible bit combination).
                                    // And simply calculate the values that were found before to "redirect"
                                    // them into a "normalized" value. The rest of bytes are to keep in sync.
                                    byte[] Matriz_Bytes_Normalización_Altura = new byte[256];
                                    byte[] Matriz_Bytes_Normalización_Arco_Iris = new byte[256];
                                    for (int Índice = 0; Índice < Lista_Bytes_Altura.Count; Índice++)
                                    {
                                        byte Altura = Lista_Bytes_Altura[Índice];
                                        int Altura_Normalizada = ((Índice + 1) * 255) / Lista_Bytes_Altura.Count;
                                        if (Altura_Normalizada < 0) Altura_Normalizada = 0;
                                        else if (Altura_Normalizada > 255) Altura_Normalizada = 255;
                                        Matriz_Bytes_Normalización_Altura[Lista_Bytes_Altura[Índice]] = (byte)Altura_Normalizada;
                                        Matriz_Bytes_Normalización_Arco_Iris[Lista_Bytes_Altura[Índice]] = (byte)Índice;
                                    }
                                    Imagen_Arco_Iris_Original = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                                    BitmapData Bitmap_Data_Arco_Iris = Imagen_Arco_Iris_Original.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.WriteOnly, Imagen_Arco_Iris_Original.PixelFormat);
                                    //byte[] Matriz_Bytes_ARGB_Arco_Iris = Matriz_Bytes_ARGB_Altura.Clone() as byte[];
                                    // Now just apply the new "normalization" array to the main byte array with the RGB pixels.
                                    /*long Altura_Media = 0L;
                                    int Altura_Total = 0;
                                    for (int Y = 0, Índice = 0, Índice_Byte_Bioma = Índice_Byte_Bioma_Inicio; Y < Alto; Y++, Índice += Bytes_Diferencia_Altura)
                                    {
                                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento_Altura, Índice_Byte_Bioma++)
                                        {
                                            Altura_Media += Matriz_Bytes_Normalización_Altura[Matriz_Bytes_ARGB_Altura[Índice]];
                                            Altura_Total++;
                                        }
                                    }*/
                                    for (int Y = 0, Índice = 0, Índice_Byte_Bioma = Índice_Byte_Bioma_Inicio; Y < Alto; Y++, Índice += Bytes_Diferencia_Altura)
                                    {
                                        for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento_Altura, Índice_Byte_Bioma++)
                                        {
                                            if (Matriz_Bytes_ARGB_Altura[Índice + 3] > 0)
                                            {
                                                /*Matriz_Bytes_ARGB_Altura[Índice + 2] = Matriz_Bytes_Normalización_Altura[Matriz_Bytes_ARGB_Altura[Índice]];
                                                Matriz_Bytes_ARGB_Altura[Índice + 1] = Matriz_Bytes_Normalización_Altura[Matriz_Bytes_ARGB_Altura[Índice]];
                                                Matriz_Bytes_ARGB_Altura[Índice] = Matriz_Bytes_Normalización_Altura[Matriz_Bytes_ARGB_Altura[Índice]];
                                                */
                                                Color Color_Arco_Iris = Program.Matriz_Colores_Arco_Iris_16[Matriz_Bytes_ARGB_Altura[Índice]];
                                                Matriz_Bytes_ARGB_Altura[Índice + 2] = Color_Arco_Iris.R;
                                                Matriz_Bytes_ARGB_Altura[Índice + 1] = Color_Arco_Iris.G;
                                                Matriz_Bytes_ARGB_Altura[Índice] = Color_Arco_Iris.B;

                                                //Matriz_Bytes_ARGB_Altura[Índice + 2] = Program.Matriz_Colores_Termografía_256[255 - Matriz_Bytes_ARGB_Altura[Índice + 2]].R;
                                                //Matriz_Bytes_ARGB_Altura[Índice + 1] = Program.Matriz_Colores_Termografía_256[255 - Matriz_Bytes_ARGB_Altura[Índice + 1]].G;
                                                //Matriz_Bytes_ARGB_Altura[Índice] = Program.Matriz_Colores_Termografía_256[255 - Matriz_Bytes_ARGB_Altura[Índice]].B;

                                                //Matriz_Bytes_ARGB_Altura[Índice + 2] = Program.Matriz_Colores_Arco_Iris_256[255 - Matriz_Bytes_ARGB_Altura[Índice + 2]].R;
                                                //Matriz_Bytes_ARGB_Altura[Índice + 1] = Program.Matriz_Colores_Arco_Iris_256[255 - Matriz_Bytes_ARGB_Altura[Índice + 1]].G;
                                                //Matriz_Bytes_ARGB_Altura[Índice] = Program.Matriz_Colores_Arco_Iris_256[255 - Matriz_Bytes_ARGB_Altura[Índice]].B;

                                                /*Color Color_Arco_Iris = Program.Matriz_Colores_Arco_Iris_16[Matriz_Bytes_Normalización_Arco_Iris[Matriz_Bytes_ARGB_Arco_Iris[Índice]]];
                                                Matriz_Bytes_ARGB_Arco_Iris[Índice + 2] = Color_Arco_Iris.R;
                                                Matriz_Bytes_ARGB_Arco_Iris[Índice + 1] = Color_Arco_Iris.G;
                                                Matriz_Bytes_ARGB_Arco_Iris[Índice] = Color_Arco_Iris.B;*/

                                                /*int Rojo = PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte_Bioma]].Color.R;
                                                int Verde = PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte_Bioma]].Color.G;
                                                int Azul = PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte_Bioma]].Color.B;

                                                //if (Matriz_Bytes_ARGB_Arco_Iris[Índice] != 95)
                                                {
                                                    if (Matriz_Bytes_Normalización_Arco_Iris[Matriz_Bytes_ARGB_Arco_Iris[Índice]] < Lista_Bytes_Altura.Count / 2)
                                                    {
                                                        Rojo -= (Matriz_Bytes_Normalización_Arco_Iris[Matriz_Bytes_ARGB_Arco_Iris[Índice]] * 32) / (Lista_Bytes_Altura.Count - 1);
                                                        Verde -= (Matriz_Bytes_Normalización_Arco_Iris[Matriz_Bytes_ARGB_Arco_Iris[Índice]] * 32) / (Lista_Bytes_Altura.Count - 1);
                                                        Azul -= (Matriz_Bytes_Normalización_Arco_Iris[Matriz_Bytes_ARGB_Arco_Iris[Índice]] * 32) / (Lista_Bytes_Altura.Count - 1);
                                                    }
                                                    else
                                                    {
                                                        Rojo += 32 - ((Matriz_Bytes_Normalización_Arco_Iris[Matriz_Bytes_ARGB_Arco_Iris[Índice]] * 32) / (Lista_Bytes_Altura.Count - 1));
                                                        Verde += 32 - ((Matriz_Bytes_Normalización_Arco_Iris[Matriz_Bytes_ARGB_Arco_Iris[Índice]] * 32) / (Lista_Bytes_Altura.Count - 1));
                                                        Azul += 32 - ((Matriz_Bytes_Normalización_Arco_Iris[Matriz_Bytes_ARGB_Arco_Iris[Índice]] * 32) / (Lista_Bytes_Altura.Count - 1));
                                                    }
                                                }

                                                if (Rojo < 0) Rojo = 0;
                                                else if (Rojo > 255) Rojo = 255;
                                                if (Verde < 0) Verde = 0;
                                                else if (Verde > 255) Verde = 255;
                                                if (Azul < 0) Azul = 0;
                                                else if (Azul > 255) Azul = 255;

                                                Matriz_Bytes_ARGB_Arco_Iris[Índice + 2] = (byte)Rojo;
                                                Matriz_Bytes_ARGB_Arco_Iris[Índice + 1] = (byte)Verde;
                                                Matriz_Bytes_ARGB_Arco_Iris[Índice] = (byte)Azul;*/

                                                /*Color Color_Arco_Iris = Program.Obtener_Color_Puro_1530(1529 - ((Matriz_Bytes_Normalización_Arco_Iris[Matriz_Bytes_ARGB_Arco_Iris[Índice]] * 1529) / Lista_Bytes_Altura.Count));
                                                Matriz_Bytes_ARGB_Arco_Iris[Índice + 2] = Color_Arco_Iris.R;
                                                Matriz_Bytes_ARGB_Arco_Iris[Índice + 1] = Color_Arco_Iris.G;
                                                Matriz_Bytes_ARGB_Arco_Iris[Índice] = Color_Arco_Iris.B;
                                                */
                                                /*Matriz_Bytes_ARGB_Arco_Iris[Índice + 2] = Matriz_Bytes_Normalización_Arco_Iris[Matriz_Bytes_ARGB_Arco_Iris[Índice]];
                                                Matriz_Bytes_ARGB_Arco_Iris[Índice + 1] = Matriz_Bytes_Normalización_Arco_Iris[Matriz_Bytes_ARGB_Arco_Iris[Índice]];
                                                Matriz_Bytes_ARGB_Arco_Iris[Índice] = Matriz_Bytes_Normalización_Arco_Iris[Matriz_Bytes_ARGB_Arco_Iris[Índice]];

                                                Matriz_Bytes_ARGB_Arco_Iris[Índice + 2] = Program.Matriz_Colores_Termografía_256[Matriz_Bytes_ARGB_Arco_Iris[Índice]].R;
                                                Matriz_Bytes_ARGB_Arco_Iris[Índice + 1] = Program.Matriz_Colores_Termografía_256[Matriz_Bytes_ARGB_Arco_Iris[Índice]].G;
                                                Matriz_Bytes_ARGB_Arco_Iris[Índice] = Program.Matriz_Colores_Termografía_256[Matriz_Bytes_ARGB_Arco_Iris[Índice]].B;
                                                */
                                                //Matriz_Bytes_ARGB_Arco_Iris[Índice + 2] = Program.Matriz_Colores_Arco_Iris_256[255 - Matriz_Bytes_ARGB_Arco_Iris[Índice]].R;
                                                //Matriz_Bytes_ARGB_Arco_Iris[Índice + 1] = Program.Matriz_Colores_Arco_Iris_256[255 - Matriz_Bytes_ARGB_Arco_Iris[Índice]].G;
                                                //Matriz_Bytes_ARGB_Arco_Iris[Índice] = Program.Matriz_Colores_Arco_Iris_256[255 - Matriz_Bytes_ARGB_Arco_Iris[Índice]].B;
                                            }
                                        }
                                    }
                                    Marshal.Copy(Matriz_Bytes_ARGB_Arco_Iris, 0, Bitmap_Data_Arco_Iris.Scan0, Matriz_Bytes_ARGB_Arco_Iris.Length);
                                    Imagen_Arco_Iris_Original.UnlockBits(Bitmap_Data_Arco_Iris);
                                    Bitmap_Data_Arco_Iris = null;
                                    Matriz_Bytes_ARGB_Arco_Iris = null;
                                }
                                Marshal.Copy(Matriz_Bytes_ARGB_Altura, 0, Bitmap_Data_Altura.Scan0, Matriz_Bytes_ARGB_Altura.Length);
                                Imagen_Altura_Original.UnlockBits(Bitmap_Data_Altura);
                                Bitmap_Data_Altura = null;
                                Matriz_Bytes_ARGB_Altura = null;
                                /*if (Lista_ID_Bloques.Count > 0)
                                {
                                    if (Lista_ID_Bloques.Count > 1) Lista_ID_Bloques.Sort();
                                    Clipboard.SetText(Program.Traducir_Lista_Variables(Lista_ID_Bloques));
                                }*/
                            }
                            Set_Datos.Dispose();
                            Set_Datos = null;
                            Adaptador_SQL.Dispose();
                            Adaptador_SQL = null;
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                        Conexión_SQL.Close();
                        Conexión_SQL.Dispose();
                        Conexión_SQL = null;

                        // Code to replace by brute force the biomes in the SQL data base.
                        // Result: it worked without corrupting the file but PixARK replaced
                        // the file each time it loaded the world, and when kept as read-only
                        // PixARK didn't change it, but the world loaded as always, so it seems
                        // to generate the biome map each time and keeps it into memory, so I
                        // ignore why it exports this file with the biomes... well at least it's
                        // useful to this application.
                        /*bool Forzar_Bioma_Único = false; // false; // true;
                        if (Forzar_Bioma_Único)
                        {
                            FileStream Lector = new FileStream(Ruta_Terrain_DB, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                            byte[] Matriz_Bytes_Completa = new byte[Lector.Length];
                            int Longitud = Lector.Read(Matriz_Bytes_Completa, 0, Matriz_Bytes_Completa.Length);
                            List<Point> Lista_Índices_Tamaños = new List<Point>();
                            List<byte> Lista_Bytes = new List<byte>(Matriz_Bytes);
                            //Lista_Índices_Tamaños.Add(new Point(15297, 1083)); // Start.
                            for (int Índice_Byte = 0; Índice_Byte < Matriz_Bytes_Completa.Length - Lista_Bytes.Count; Índice_Byte++)
                            {
                                bool Encontrado = true;
                                int Escribir = Matriz_Bytes.Length;
                                for (int Índice = 0; Índice < Lista_Bytes.Count; Índice++)
                                {
                                    //break;
                                    if (Matriz_Bytes_Completa[Índice_Byte + Índice] != Lista_Bytes[Índice])
                                    {
                                        if (Índice >= 1000) // 1083.
                                        {
                                            //Encontrado = true;
                                            //Escribir = Índice;
                                            Lista_Índices_Tamaños.Add(new Point(Índice_Byte, Índice));
                                            Lista_Bytes.RemoveRange(0, Índice);
                                            //Índice_Global += Índice;
                                            Índice_Byte += Índice - 1;
                                            break;
                                        }
                                        else
                                        {
                                            Encontrado = false;
                                            break;
                                        }
                                    }
                                    //else Índice_Byte++;
                                }
                            }
                            if (Lista_Índices_Tamaños.Count > 0)
                            {
                                int Total = 0;
                                for (int j = 0; j < Lista_Índices_Tamaños.Count; j++)
                                {
                                    Total += Lista_Índices_Tamaños[j].Y;
                                }
                                ;
                                for (int j = 0; j < Lista_Índices_Tamaños.Count; j++)
                                {
                                    byte[] q = new byte[Math.Min(Lista_Índices_Tamaños[j].Y, 4000)]; // 1083.
                                    for (int k = (j != 0 ? 0 : 16); k < q.Length; k++)
                                    {
                                        q[k] = (byte)10; // Novice Grassland.
                                        //q[k] = (byte)Program.Rand.Next(0, 12);
                                    }
                                    Lector.Seek(Lista_Índices_Tamaños[j].X, SeekOrigin.Begin);
                                    Lector.Write(q, 0, q.Length);
                                }
                            }
                            Lector.Close();
                            Lector.Dispose();
                            Lector = null;
                        }*/
                        /*// Try to change the whole world biomes into a single one.
                        // Results: only errors...
                        for (int i = Índice_Byte_Inicio; i < Matriz_Bytes.Length; i++)
                        {
                            Matriz_Bytes[i] = 10; // Novice Grassland.
                        }
                        Set_Datos.Tables[0].Rows[0]["data"] = Matriz_Bytes;
                        //Set_Datos.Tables[0].Rows[0].AcceptChanges();
                        //Set_Datos.Tables[0].AcceptChanges();
                        // MessageBox.Show(this, Set_Datos.HasChanges().ToString()); // True.
                        Adaptador_SQL.UpdateCommand = new SQLiteCommand("Insert * from " + PixARK.Texto_Clave_Mundo + ";", Conexión_SQL);
                        Adaptador_SQL.Update(Set_Datos, PixARK.Texto_Clave_Mundo);
                        //Set_Datos.AcceptChanges();
                        //Adaptador_SQL.Update(Set_Datos);*/

                        // Always seems to be 16 bytes that are not biome indexes, what are they?
                        if (Índice_Byte_Bioma_Inicio != 16 &&
                            string.Compare(Environment.UserName, "Jupisoft", true) == 0)
                        {
                            MessageBox.Show(this, "Índice_Byte_Inicio = " + Índice_Byte_Bioma_Inicio.ToString() + ".", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        int[] Matriz_Biomas = new int[256]; // Use 256 to support future updates.
                        List<byte> Lista_Biomas = new List<byte>();
                        //byte Mínimo = byte.MaxValue;
                        //byte Máximo = byte.MinValue;
                        int Total_Biomas = 0; // Total of counted biomes, without the borders.
                        int Total_Fácil = 0;
                        int Total_Media = 0;
                        int Total_Difícil = 0;
                        int Total_Agua = 0;
                        int Total_Minerales = 0;
                        int Total_Cobre = 0;
                        int Total_Hierro = 0;
                        int Total_Hierro_Fácil = 0;
                        int Total_Hierro_Medio = 0;
                        int Total_Hierro_Difícil = 0;
                        int Total_Plata = 0; // Always hard to get.
                        if (Variable_Ocultar_Bordes)
                        {
                            // Count only the playable biome percentages, excluding the ones outside the border.
                            for (int Y = 0, Índice_Byte = Índice_Byte_Bioma_Inicio; Y < Alto; Y++)
                            {
                                for (int X = 0; X < Ancho; X++, Índice_Byte++)
                                {
                                    if (Índice_Byte < Matriz_Bytes_Biomas.Length &&
                                        X >= PixARK.Borde_Mundo &&
                                        X < Ancho - PixARK.Borde_Mundo &&
                                        Y >= PixARK.Borde_Mundo &&
                                        Y < Alto - PixARK.Borde_Mundo)
                                    {
                                        Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]]++;
                                        if (!Lista_Biomas.Contains(Matriz_Bytes_Biomas[Índice_Byte])) Lista_Biomas.Add(Matriz_Bytes_Biomas[Índice_Byte]);
                                        Total_Biomas++;
                                        if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Dificultad == PixARK.Dificultades.Easy) Total_Fácil++;
                                        else if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Dificultad == PixARK.Dificultades.Medium) Total_Media++;
                                        else if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Dificultad == PixARK.Dificultades.Hard) Total_Difícil++;

                                        if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 0 ||
                                            //PixARK.Biomas.Matriz_Biomas[Matriz_Bytes[Índice_Byte]].Índice == 11 ||
                                            PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 13) Total_Agua++;

                                        if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 15 ||
                                            PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 18)
                                        {
                                            Total_Minerales++;
                                            Total_Cobre++;
                                        }

                                        if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 17)
                                        {
                                            Total_Minerales++;
                                            Total_Hierro++;
                                            Total_Hierro_Fácil++;
                                        }

                                        if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 14 ||
                                            PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 16 ||
                                            PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 21)
                                        {
                                            Total_Minerales++;
                                            Total_Hierro++;
                                            Total_Hierro_Medio++;
                                        }

                                        if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 20 ||
                                            PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 22)
                                        {
                                            Total_Minerales++;
                                            Total_Hierro++;
                                            Total_Hierro_Difícil++;
                                        }

                                        if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 23)
                                        {
                                            Total_Minerales++;
                                            Total_Plata++;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Count all biome percentages, even the ones outside the world border.
                            for (int Índice_Byte = Índice_Byte_Bioma_Inicio; Índice_Byte < Total_Píxeles; Índice_Byte++)
                            {
                                if (Índice_Byte < Matriz_Bytes_Biomas.Length)
                                {
                                    //byte Valor = Matriz_Bytes[Índice_Byte];
                                    //if (Valor < Mínimo) Mínimo = Valor;
                                    //if (Valor > Máximo) Máximo = Valor;
                                    Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]]++;
                                    if (!Lista_Biomas.Contains(Matriz_Bytes_Biomas[Índice_Byte])) Lista_Biomas.Add(Matriz_Bytes_Biomas[Índice_Byte]);
                                    if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Dificultad == PixARK.Dificultades.Easy) Total_Fácil++;
                                    else if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Dificultad == PixARK.Dificultades.Medium) Total_Media++;
                                    else if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Dificultad == PixARK.Dificultades.Hard) Total_Difícil++;

                                    if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 0 ||
                                            //PixARK.Biomas.Matriz_Biomas[Matriz_Bytes[Índice_Byte]].Índice == 11 ||
                                            PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 13) Total_Agua++;

                                    if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 15 ||
                                        PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 18)
                                    {
                                        Total_Minerales++;
                                        Total_Cobre++;
                                    }

                                    if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 17)
                                    {
                                        Total_Minerales++;
                                        Total_Hierro++;
                                        Total_Hierro_Fácil++;
                                    }

                                    if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 14 ||
                                        PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 16 ||
                                        PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 21)
                                    {
                                        Total_Minerales++;
                                        Total_Hierro++;
                                        Total_Hierro_Medio++;
                                    }

                                    if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 20 ||
                                        PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 22)
                                    {
                                        Total_Minerales++;
                                        Total_Hierro++;
                                        Total_Hierro_Difícil++;
                                    }

                                    if (PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Índice == 23)
                                    {
                                        Total_Minerales++;
                                        Total_Plata++;
                                    }
                                }
                            }
                            Total_Biomas = Total_Píxeles; // Use all the biomes, even the ones unreachable.
                        }
                        int[] Matriz_Porcentajes = new int[101]; // Array with biome percentages (0 to 100).
                        for (int Índice_Porcentaje = 0; Índice_Porcentaje <= 100; Índice_Porcentaje++)
                        {
                            Matriz_Porcentajes[Índice_Porcentaje] = (int)Math.Round(((double)Índice_Porcentaje * (double)Total_Biomas) / 100d, MidpointRounding.AwayFromZero);
                        }
                        Grupo_Biomas_Fáciles.Text = "Easy difficulty main biomes - [" + Program.Traducir_Número_Decimales_Redondear(((double)(Matriz_Biomas[10] + Matriz_Biomas[2] + Matriz_Biomas[5] + Matriz_Biomas[8] + Matriz_Biomas[13]) * 100d) / (double)Total_Biomas, 4) + " %]";
                        Grupo_Biomas_Medios.Text = "Medium difficulty main biomes - [" + Program.Traducir_Número_Decimales_Redondear(((double)(Matriz_Biomas[0] + Matriz_Biomas[1] + Matriz_Biomas[4] + Matriz_Biomas[7]) * 100d) / (double)Total_Biomas, 4) + " %]";
                        Grupo_Biomas_Difíciles.Text = "Hard difficulty main biomes - [" + Program.Traducir_Número_Decimales_Redondear(((double)(Matriz_Biomas[9] + Matriz_Biomas[6] + Matriz_Biomas[3]) * 100d) / (double)Total_Biomas, 4) + " %]";
                        Grupo_Recursos_Fáciles.Text = "Easy difficulty resources biomes - [" + Program.Traducir_Número_Decimales_Redondear(((double)(Matriz_Biomas[15] + Matriz_Biomas[17] + Matriz_Biomas[18] + Matriz_Biomas[11]) * 100d) / (double)Total_Biomas, 4) + " %]";
                        Grupo_Recursos_Medios.Text = "Medium difficulty resources biomes - [" + Program.Traducir_Número_Decimales_Redondear(((double)(Matriz_Biomas[21] + Matriz_Biomas[16] + Matriz_Biomas[14]) * 100d) / (double)Total_Biomas, 4) + " %]";
                        Grupo_Recursos_Difíciles.Text = "Hard difficulty resources biomes - [" + Program.Traducir_Número_Decimales_Redondear(((double)(Matriz_Biomas[20] + Matriz_Biomas[22] + Matriz_Biomas[23]) * 100d) / (double)Total_Biomas, 4) + " %]";
                        for (int Índice = 0; Índice < 24; Índice++)
                        {
                            if (Índice != 12 &&
                                Índice != 19 &&
                                Índice < Matriz_Etiquetas.Length &&
                                Índice < PixARK.Biomas.Matriz_Biomas.Length)
                            {
                                Matriz_Etiquetas[Índice].ForeColor = Matriz_Biomas[Índice] > 0 ? (Matriz_Biomas[Índice] >= Matriz_Porcentajes[8] ? Color.Blue : Color.Black) : Color.Red;
                                Matriz_Etiquetas[Índice].Text = PixARK.Biomas.Matriz_Biomas[Índice].Índice.ToString() + ": " + (string.Compare(PixARK.Biomas.Matriz_Biomas[Índice].Nombre, PixARK.Biomas.Matriz_Biomas[Índice].Nombre_Simple, true) != 0 ? PixARK.Biomas.Matriz_Biomas[Índice].Nombre_Simple + (!PixARK.Biomas.Matriz_Biomas[Índice].Nombre_Simple.EndsWith("s") ? "'s " : "' ") : null) + PixARK.Biomas.Matriz_Biomas[Índice].Nombre + (PixARK.Biomas.Matriz_Biomas[Índice].Minerales != PixARK.Minerales.Unknown ? " (" + PixARK.Biomas.Matriz_Biomas[Índice].Minerales.ToString().Replace('_', ' ') + ")" : null) + (Índice != 12 && Índice != 19 ? " [" + Program.Traducir_Número_Decimales_Redondear(((double)Matriz_Biomas[Índice] * 100d) / (double)Total_Biomas, 4) + " %]." : ".");
                            }
                        }

                        // Find the more and less common biomes.
                        int Mínimo_Índice = -1;
                        int Mínimo_Total = int.MaxValue;
                        int Mínimo_Cero_Índice = -1;
                        int Mínimo_Cero_Total = int.MaxValue;
                        int Máximo_Índice = -1;
                        int Máximo_Total = int.MinValue;
                        for (int Índice = 0; Índice < PixARK.Biomas.Matriz_Biomas.Length; Índice++)
                        {
                            if (!PixARK.Biomas.Matriz_Biomas[Índice].Bioma_Minerales)
                            {
                                if (Matriz_Biomas[Índice] < Mínimo_Total &&
                                    Índice != 10) // Minimum biome found, but ignore the biome 10 (Novice Grassland).
                                {
                                    Mínimo_Índice = Índice;
                                    Mínimo_Total = Matriz_Biomas[Índice];
                                }
                                if (Matriz_Biomas[Índice] > 0 &&
                                    Matriz_Biomas[Índice] < Mínimo_Cero_Total &&
                                    Índice != 10) // Minimum existing biome found.
                                {
                                    // Ignore the biome index 10 (Novice Grassland) since after
                                    // generating a few hundreds of levels it seems that it always
                                    // generates 9 small patches of this biome, and always separated
                                    // as if the world was splitted into a 3 x 3 grid and then was
                                    // always placed one of these biomes in each section, although
                                    // they often seem to appear inside the border zone of the world
                                    // meaning most of them won't even be visible at all, so just
                                    // ignore that biome to see the full world biome balance.
                                    Mínimo_Cero_Índice = Índice;
                                    Mínimo_Cero_Total = Matriz_Biomas[Índice];
                                }
                                if (Matriz_Biomas[Índice] > Máximo_Total) // Maximum biome found.
                                {
                                    Máximo_Índice = Índice;
                                    Máximo_Total = Matriz_Biomas[Índice];
                                }
                            }
                        }
                        // Exclude the currently non existing biomes from the difference.
                        int Diferencia_Máximo_Mínimo = Máximo_Total - Mínimo_Cero_Total;

                        // Now make some kind of world analysis like the ones on the PixARK world maps wiki.
                        string Texto_Análisis = "The seed " + Program.Traducir_Número(Semilla) + " has ";

                        // Add the biome count.
                        if (Lista_Biomas.Count >= 22) Texto_Análisis += "all biomes, ";
                        else if (Lista_Biomas.Count >= 20) Texto_Análisis += "almost all biomes, ";
                        else if (Lista_Biomas.Count >= 18) Texto_Análisis += "several biomes missing, ";
                        else if (Lista_Biomas.Count >= 16) Texto_Análisis += "lots of biomes missing, ";
                        else Texto_Análisis += "too many biomes missing, ";

                        if (Diferencia_Máximo_Mínimo < Matriz_Porcentajes[10])
                        {
                            Texto_Análisis += "perfectly balanced, ";
                        }
                        else if (Diferencia_Máximo_Mínimo < Matriz_Porcentajes[15])
                        {
                            Texto_Análisis += "very well balanced, ";
                        }
                        else if (Diferencia_Máximo_Mínimo < Matriz_Porcentajes[20])
                        {
                            Texto_Análisis += "well balanced, ";
                        }
                        else if (Diferencia_Máximo_Mínimo >= Matriz_Porcentajes[30])
                        {
                            Texto_Análisis += "badly balanced, ";
                        }

                        // Add the more common biome in the world.
                        if (Máximo_Total >= Matriz_Porcentajes[25])
                        {
                            Texto_Análisis += "with mostly " + PixARK.Biomas.Matriz_Biomas[Máximo_Índice].Nombre.ToLowerInvariant() + " ";
                        }
                        else
                        {
                            Texto_Análisis += "with lots of " + PixARK.Biomas.Matriz_Biomas[Máximo_Índice].Nombre.ToLowerInvariant() + " ";
                        }

                        // Add the less common biomes in the world.
                        if (Mínimo_Total > 0)
                        {
                            Texto_Análisis += "and very few " + PixARK.Biomas.Matriz_Biomas[Mínimo_Índice].Nombre.ToLowerInvariant() + ", ";
                        }
                        else
                        {
                            List<string> Lista_Biomas_Inexistentes = new List<string>();
                            for (int Índice = 0; Índice < 14/*PixARK.Biomas.Matriz_Biomas.Length*/; Índice++)
                            {
                                if (Índice != 10 && // Again ignore the biome 10 or "Novice Grassland".
                                    Índice != 12 &&
                                    Índice != 19 &&
                                    Matriz_Biomas[Índice] <= 0)
                                {
                                    Lista_Biomas_Inexistentes.Add(PixARK.Biomas.Matriz_Biomas[Índice].Nombre.ToLowerInvariant());
                                }
                            }
                            if (Lista_Biomas_Inexistentes.Count > 0)
                            {
                                // More than one biome might be missing, so sort and show all the missing names.
                                if (Lista_Biomas_Inexistentes.Count > 1) Lista_Biomas_Inexistentes.Sort();
                                string Texto_Biomas_Inexistentes = null;
                                for (int Índice = 0; Índice < Lista_Biomas_Inexistentes.Count; Índice++)
                                {
                                    Texto_Biomas_Inexistentes += Lista_Biomas_Inexistentes[Índice] + (Índice != Lista_Biomas_Inexistentes.Count - 2 ? ", " : " and ");
                                }
                                Texto_Análisis += "and nothing of " + Texto_Biomas_Inexistentes;
                            }
                            else // This should never happen.
                            {
                                Texto_Análisis += "and nothing of " + PixARK.Biomas.Matriz_Biomas[Mínimo_Índice].Nombre.ToLowerInvariant() + ", ";
                            }
                        }

                        // Add the biomes difficulty.
                        byte Recomendado = 0; // 1 for very novices, 2 for novices, 3 for experts and 4 for very experts.
                        if (Total_Difícil >= Total_Media * 2 && Total_Difícil >= Total_Fácil * 2)
                        {
                            Texto_Análisis += string.IsNullOrEmpty(Texto_Análisis) ? "It's a very hard biomes map" : "it's a very hard biomes map";
                            if (Total_Media < Matriz_Porcentajes[20]) Texto_Análisis += " with very few medium biomes";
                            else if (Total_Fácil < Matriz_Porcentajes[20])
                            {
                                Texto_Análisis += " with very few easy biomes";
                                Recomendado = 4;
                            }
                        }
                        else if (Total_Media >= Total_Difícil * 2 && Total_Media >= Total_Fácil * 2)
                        {
                            Texto_Análisis += string.IsNullOrEmpty(Texto_Análisis) ? "It's a very medium biomes map" : "it's a very medium biomes map";
                            if (Total_Difícil < Matriz_Porcentajes[20]) Texto_Análisis += " with very few hard biomes";
                            else if (Total_Fácil < Matriz_Porcentajes[20]) Texto_Análisis += " with very few easy biomes";
                        }
                        else if (Total_Fácil >= Total_Difícil * 2 && Total_Fácil >= Total_Media * 2)
                        {
                            Texto_Análisis += string.IsNullOrEmpty(Texto_Análisis) ? "It's a very easy biomes map" : "it's a very easy biomes map";
                            if (Total_Difícil < Matriz_Porcentajes[20])
                            {
                                Texto_Análisis += " with very few hard biomes";
                                Recomendado = 1;
                            }
                            else if (Total_Media < Matriz_Porcentajes[20]) Texto_Análisis += " with very few medium biomes";
                        }
                        else if (Total_Difícil >= Total_Media && Total_Difícil >= Total_Fácil)
                        {
                            Texto_Análisis += string.IsNullOrEmpty(Texto_Análisis) ? "It's mostly a hard biomes map" : "it's mostly a hard biomes map";
                            if (Total_Media < Matriz_Porcentajes[20]) Texto_Análisis += " with very few medium biomes";
                            else if (Total_Fácil < Matriz_Porcentajes[20])
                            {
                                Texto_Análisis += " with very few easy biomes";
                                Recomendado = 3;
                            }
                        }
                        else if (Total_Media >= Total_Difícil && Total_Media >= Total_Fácil)
                        {
                            Texto_Análisis += string.IsNullOrEmpty(Texto_Análisis) ? "It's mostly a medium biomes map" : "it's mostly a medium biomes map";
                            if (Total_Difícil < Matriz_Porcentajes[20]) Texto_Análisis += " with very few hard biomes";
                            else if (Total_Fácil < Matriz_Porcentajes[20]) Texto_Análisis += " with very few easy biomes";
                        }
                        else if (Total_Fácil >= Total_Difícil && Total_Fácil >= Total_Media)
                        {
                            Texto_Análisis += string.IsNullOrEmpty(Texto_Análisis) ? "It's mostly an easy biomes map" : "it's mostly an easy biomes map";
                            if (Total_Difícil < Matriz_Porcentajes[20])
                            {
                                Texto_Análisis += " with very few hard biomes";
                                Recomendado = 2;
                            }
                            else if (Total_Media < Matriz_Porcentajes[20]) Texto_Análisis += " with very few medium biomes";
                        }

                        // Add the water to see if it's useful for using boats.
                        if (Total_Agua >= Matriz_Porcentajes[25])
                        {
                            Texto_Análisis += ", perfect to navigate using boats";
                        }
                        else if (Total_Agua >= Matriz_Porcentajes[20])
                        {
                            Texto_Análisis += ", useful to navigate using boats";
                        }
                        else if (Total_Agua <= 0) // Dry world.
                        {
                            Texto_Análisis += ", doesn't have any water to navigate";
                        }
                        else if (Total_Agua <= Matriz_Porcentajes[5])
                        {
                            Texto_Análisis += ", doesn't have enough water to navigate";
                        }
                        else if (Total_Agua <= Matriz_Porcentajes[10])
                        {
                            Texto_Análisis += ", doesn't have much water to navigate";
                        }

                        // Add the more common surface metal ores.
                        if (Total_Plata >= Total_Cobre && Total_Plata >= Total_Hierro)
                        {
                            Texto_Análisis += " and it should have hard to get silver ores exposed on the surface among other resources";
                        }
                        else if (Total_Cobre >= Total_Plata && Total_Cobre >= Total_Hierro)
                        {
                            Texto_Análisis += " and it should have easy to get copper ores exposed on the surface among other resources";
                        }
                        else// if (Total_Hierro >= Total_Plata && Total_Hierro >= Total_Cobre)
                        {
                            if (Total_Hierro_Difícil >= Total_Hierro_Medio && Total_Hierro_Difícil >= Total_Hierro_Fácil)
                            {
                                Texto_Análisis += " and it should have hard to get iron ores exposed on the surface among other resources";
                            }
                            else if (Total_Hierro_Fácil >= Total_Hierro_Difícil && Total_Hierro_Fácil >= Total_Hierro_Medio)
                            {
                                Texto_Análisis += " and it should have easy to get iron ores exposed on the surface among other resources";
                            }
                            else
                            {
                                Texto_Análisis += " and it should have iron ores exposed on the surface among other resources";
                            }
                        }

                        // Add the world recommendation.
                        if (Recomendado == 1)
                        {
                            Texto_Análisis += ", highly recommended for novices";
                        }
                        else if (Recomendado == 2)
                        {
                            Texto_Análisis += ", recommended for novices";
                        }
                        else if (Recomendado == 3)
                        {
                            Texto_Análisis += ", recommended for experts";
                        }
                        else if (Recomendado == 4)
                        {
                            Texto_Análisis += ", only recommended for experts";
                        }

                        Matriz_Porcentajes = null;
                        TextBox_Análisis.Text = Texto_Análisis + ".";
                        Barra_Estado_Etiqueta_Minerales.Text = "Ores: " + Program.Traducir_Número_Decimales_Redondear(((double)Total_Minerales * 100d) / (double)Total_Biomas, 2) + " %";
                        Barra_Estado_Etiqueta_Fácil.Text = Program.Traducir_Número_Decimales_Redondear(((double)Total_Fácil * 100d) / (double)Total_Biomas, 2) + " %";
                        Barra_Estado_Etiqueta_Media.Text = Program.Traducir_Número_Decimales_Redondear(((double)Total_Media * 100d) / (double)Total_Biomas, 2) + " %";
                        Barra_Estado_Etiqueta_Difícil.Text = Program.Traducir_Número_Decimales_Redondear(((double)Total_Difícil * 100d) / (double)Total_Biomas, 2) + " %";
                        if (Lista_Biomas.Count > 1) Lista_Biomas.Sort();
                        if (string.Compare(Environment.UserName, "Jupisoft", true) == 0)
                        {
                            int Biomas_Desconocidos = 0;
                            foreach (int Índice_Bioma in Lista_Biomas)
                            {
                                try
                                {
                                    if (Índice_Bioma == 12 ||
                                        Índice_Bioma == 19 ||
                                        Índice_Bioma >= 24)
                                    {
                                        Biomas_Desconocidos++;
                                    }
                                }
                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                            }
                            if (Biomas_Desconocidos > 0)
                            {
                                MessageBox.Show(this, Program.Traducir_Número(Biomas_Desconocidos) + (Biomas_Desconocidos != 1 ? " unknown biomes found!" : " unknown biome found!"), Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        this.Text = Texto_Título + " - [" + Program.Traducir_Número(Lista_Biomas.Count) + (Lista_Biomas.Count != 1 ? " Biomes" : " Biome") + " found: " + Program.Traducir_Lista_Variables(Lista_Biomas) + "]";
                        Imagen_Biomas_Original = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                        Imagen_Biomas_Original_Simple = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                        BitmapData Bitmap_Data = Imagen_Biomas_Original.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen_Biomas_Original.PixelFormat);
                        BitmapData Bitmap_Data_Simple = Imagen_Biomas_Original_Simple.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen_Biomas_Original_Simple.PixelFormat);
                        byte[] Matriz_Bytes_ARGB = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                        byte[] Matriz_Bytes_ARGB_Simple = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                        int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen_Biomas_Original.PixelFormat) ? 4 : 3;
                        int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen_Biomas_Original.PixelFormat)) / 8);
                        // Usually start the byte index at 16, since it seems to be some sort of header or so.
                        for (int Y = 0, Índice = 0, Índice_Byte = Índice_Byte_Bioma_Inicio; Y < Alto; Y++, Índice += Bytes_Diferencia)
                        {
                            for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento, Índice_Byte++)
                            {
                                // Known biome found, draw it's approximate colors.
                                if (Índice_Byte < Matriz_Bytes_Biomas.Length &&
                                    Matriz_Bytes_Biomas[Índice_Byte] < PixARK.Biomas.Matriz_Biomas.Length)
                                {
                                    // Main biome map with ores and resources.
                                    Matriz_Bytes_ARGB[Índice + 3] = 255;
                                    Matriz_Bytes_ARGB[Índice + 2] = PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Color.R;
                                    Matriz_Bytes_ARGB[Índice + 1] = PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Color.G;
                                    Matriz_Bytes_ARGB[Índice] = PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Color.B;

                                    // Main biome map without ores and resources (the one PixARK shows).
                                    Matriz_Bytes_ARGB_Simple[Índice + 3] = 255;
                                    Matriz_Bytes_ARGB_Simple[Índice + 2] = PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Color_Simple.R;
                                    Matriz_Bytes_ARGB_Simple[Índice + 1] = PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Color_Simple.G;
                                    Matriz_Bytes_ARGB_Simple[Índice] = PixARK.Biomas.Matriz_Biomas[Matriz_Bytes_Biomas[Índice_Byte]].Color_Simple.B;
                                }
                                else // Unknown biome found, use black color instead.
                                {
                                    // Main biome map with ores and resources.
                                    Matriz_Bytes_ARGB[Índice + 3] = 255; // Alpha.
                                    Matriz_Bytes_ARGB[Índice + 2] = 0; // Red.
                                    Matriz_Bytes_ARGB[Índice + 1] = 0; // Green.
                                    Matriz_Bytes_ARGB[Índice] = 0; // Blue.

                                    // Main biome map without ores and resources (the one PixARK shows).
                                    Matriz_Bytes_ARGB_Simple[Índice + 3] = 255; // Alpha.
                                    Matriz_Bytes_ARGB_Simple[Índice + 2] = 0; // Red.
                                    Matriz_Bytes_ARGB_Simple[Índice + 1] = 0; // Green.
                                    Matriz_Bytes_ARGB_Simple[Índice] = 0; // Blue.
                                }
                            }
                        }
                        Marshal.Copy(Matriz_Bytes_ARGB, 0, Bitmap_Data.Scan0, Matriz_Bytes_ARGB.Length);
                        Marshal.Copy(Matriz_Bytes_ARGB_Simple, 0, Bitmap_Data_Simple.Scan0, Matriz_Bytes_ARGB_Simple.Length);
                        Imagen_Biomas_Original.UnlockBits(Bitmap_Data);
                        Imagen_Biomas_Original_Simple.UnlockBits(Bitmap_Data_Simple);
                        Bitmap_Data = null;
                        Bitmap_Data_Simple = null;
                        Matriz_Bytes_ARGB = null;
                        Matriz_Bytes_ARGB_Simple = null;
                        // Debug test for Jupisoft that saves all the original generated maps.
                        if (string.Compare(Environment.UserName, "Jupisoft", true) == 0)
                        {
                            string Ruta_Mapas_Jupisoft = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Jupisoft PixARK Maps";
                            Program.Crear_Carpetas(Ruta_Mapas_Jupisoft);
                            Ruta_Mapas_Jupisoft += "\\" + Semilla.ToString() + ".png";
                            if (!File.Exists(Ruta_Mapas_Jupisoft))
                            {
                                Imagen_Biomas_Original.Save(Ruta_Mapas_Jupisoft, ImageFormat.Png);
                            }
                            Ruta_Mapas_Jupisoft = null;
                        }
                        // Get only the "real map", without the world borders extra part, which isn't mineable at all.
                        Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
                        Matriz_Bytes_Biomas = null;
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally
            {
                GC.Collect();
                GC.GetTotalMemory(true);
                this.Cursor = Cursors.Default;
            }
        }

        private void Menú_Contextual_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                if (Variable_Pantalla_Completa) Cursor.Show();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            try
            {
                if (Variable_Pantalla_Completa) Cursor.Hide();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                if (Variable_Pantalla_Completa) Cursor.Show();
                MessageBox.Show(this, "[Temporary help file] [2019_10_15_19_02_08_949]\r\n\r\n" +
                    "This application can load the map from any PixARK world and show a lot more details than the in game map, like surface ores and extra biomes, and it also has full resolution (1 pixel per block).\r\n\r\n" +
                    "If you can't load any map, locate your PixARK save folder and try to find a folder called \"CubeWorld_Light\" inside of it, then copy it's full path and paste it in the top text box of the application and press \"Enter\", if done correct now you should see a map loading after a few seconds.\r\n\r\n" +
                    "Your worlds will always be loaded as read-only, so they will never be modified in any way by this application, which also means that you can quickly see the full map of any world, even if you don't use the cheat to reveal the map or you haven't explored the full world yet, once you see the several markers to spawn in a new world and even with PixARK started you can already load that world in this application without risk.\r\n\r\n" +
                    "At the bottom right corner you'll see a world analysis text based on the currently loaded world, that analysis was inspired by the PixARK wiki page about the interesting map seeds found by the community, so this might help you post your seed findings onto that page if you want, and also quickly see in detail if a newly created world might be what you were looking for or you need to create a new one.\r\n\r\n" +
                    "So far in a few hundreds of newly created worlds I've never found the biomes \"12\" and \"19\", so perhaps they are related to some boss fights, etc. For now I don't know it's contents or if they even exist at all. Also I've found that most worlds doesn't seem to have much mountain forest, and always there are 9 patches of novice grassland divided in a 3 x 3 grid, although some end up outside the world border, making a random number of spawn places each time, since only the \"complete\" ones seem to be valid spawns.\r\n\r\n" +
                    "Now when pressing \"F9\" it shows all the known PixARK cheats to save you some time.\r\n\r\n" +
                    "I've tested this application with the PixARK versions 1.54 and 1.67, and for now it's working as expected, it might be that a future update stops this application from working as intented, so if you find any bug, please post a comment on GitHub or send me an e-mail from the main context menu and I'll fix it as soon as I can. Thank you for you collaboration.",
                    "Help Viewer for " + Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Question);
                if (Variable_Pantalla_Completa) Cursor.Hide();
                /*Ventana_Visor_Ayuda Ventana = new Ventana_Visor_Ayuda();
                Ventana.Ayuda = Ventana_Visor_Ayuda.Ayudas.Main_window;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;*/
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Acerca_Click(object sender, EventArgs e)
        {
            try
            {
                if (Variable_Pantalla_Completa) Cursor.Show();
                Ventana_Acerca Ventana = new Ventana_Acerca();
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
                if (Variable_Pantalla_Completa) Cursor.Hide();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Depurador_Excepciones_Click(object sender, EventArgs e)
        {
            try
            {
                if (Variable_Pantalla_Completa) Cursor.Show();
                Variable_Excepción = false;
                Variable_Excepción_Imagen = false;
                Variable_Excepción_Total = 0;
                Barra_Estado_Botón_Excepción.Visible = false;
                Barra_Estado_Separador_1.Visible = false;
                Barra_Estado_Botón_Excepción.Image = Resources.Excepción_Gris;
                Barra_Estado_Botón_Excepción.ForeColor = Color.Black;
                Barra_Estado_Botón_Excepción.Text = "Exceptions: 0";
                Ventana_Depurador_Excepciones Ventana = new Ventana_Depurador_Excepciones();
                Ventana.Variable_Siempre_Visible = Variable_Siempre_Visible;
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
                if (Variable_Pantalla_Completa) Cursor.Hide();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Siempre_Visible_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Siempre_Visible = Menú_Contextual_Siempre_Visible.Checked;
                this.TopMost = Variable_Siempre_Visible;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                // Avoid multiple worlds loading.
                bool Cargar_Automáticamente = CheckBox_Cargar_Automáticamente.Checked;
                CheckBox_Cargar_Automáticamente.Checked = false;
                string Ruta_Mundo = ComboBox_Mundo_PixARK.Text; // Remember the current world.
                // This should reload the worlds list, making newly created ones also appear.
                ComboBox_Ruta_PixARK_SelectedIndexChanged(ComboBox_Ruta_PixARK, EventArgs.Empty);
                if (!string.IsNullOrEmpty(Ruta_Mundo) && ComboBox_Mundo_PixARK.Items.Contains(Ruta_Mundo))
                {
                    ComboBox_Mundo_PixARK.Text = Ruta_Mundo;
                }
                else if (ComboBox_Mundo_PixARK.Items.Count > 0)
                {
                    ComboBox_Mundo_PixARK.SelectedIndex = 0;
                }
                CheckBox_Cargar_Automáticamente.Checked = Cargar_Automáticamente;
                ComboBox_Mundo_PixARK_SelectedIndexChanged(ComboBox_Mundo_PixARK, EventArgs.Empty);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Ocultar_Bordes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Variable_Ocultar_Bordes = Menú_Contextual_Ocultar_Bordes.Checked;
                Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Mostrar_Minerales_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Variable_Mostrar_Minerales = Menú_Contextual_Mostrar_Minerales.Checked;
                Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Mostrar_Regla_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Variable_Mostrar_Regla = Menú_Contextual_Mostrar_Regla.Checked;
                Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Mostrar_Lista_Trucos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Variable_Mostrar_Lista_Trucos = Menú_Contextual_Mostrar_Lista_Trucos.Checked;
                if (!Variable_Mostrar_Lista_Trucos)
                {
                    Picture.Visible = true;
                    Grupo_Trucos.Visible = false;
                    ComboBox_Mundo_PixARK.Select();
                    ComboBox_Mundo_PixARK.Focus();
                }
                else
                {
                    Grupo_Trucos.Visible = true;
                    Picture.Visible = false;
                    ComboBox_Truco.Select();
                    ComboBox_Truco.Focus();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Abrir_PixARK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Ruta_PixARK.Text) && Directory.Exists(ComboBox_Ruta_PixARK.Text))
                {
                    Program.Ejecutar_Ruta(ComboBox_Ruta_PixARK.Text, ProcessWindowStyle.Maximized);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Abrir_Mundo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ComboBox_Mundo_PixARK.Text) && Directory.Exists(ComboBox_Mundo_PixARK.Text))
                {
                    Program.Ejecutar_Ruta(ComboBox_Mundo_PixARK.Text, ProcessWindowStyle.Maximized);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Abrir_Carpeta_Mapas_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Crear_Carpetas(Application.StartupPath + "\\Maps");
                if (Directory.Exists(Application.StartupPath + "\\Maps"))
                {
                    Program.Ejecutar_Ruta(Application.StartupPath + "\\Maps", ProcessWindowStyle.Maximized);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Análisis_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(TextBox_Análisis.Text))
                {
                    Clipboard.SetText(TextBox_Análisis.Text);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (((Variable_Mostrar_Mapa == 0 && Imagen_Biomas_Original != null && Imagen_Biomas_Original_Simple != null) ||
                    (Variable_Mostrar_Mapa == 1 && Imagen_Altura_Original != null) ||
                    (Variable_Mostrar_Mapa == 2 && Imagen_Arco_Iris_Original != null)) &&
                    !string.IsNullOrEmpty(ComboBox_Mundo_PixARK.Text))
                {
                    string Ruta = Application.StartupPath + "\\Maps";
                    Program.Crear_Carpetas(Ruta);
                    Bitmap Imagen = null;
                    if (Variable_Ocultar_Bordes)
                    {
                        if (Variable_Mostrar_Mapa == 0)
                        {
                            if (Variable_Mostrar_Minerales) Imagen = Imagen_Biomas_Original.Clone(new Rectangle(512, 512, Imagen_Biomas_Original.Width - 1024, Imagen_Biomas_Original.Height - 1024), Imagen_Biomas_Original.PixelFormat);
                            else Imagen = Imagen_Biomas_Original_Simple.Clone(new Rectangle(512, 512, Imagen_Biomas_Original.Width - 1024, Imagen_Biomas_Original.Height - 1024), Imagen_Biomas_Original.PixelFormat);
                        }
                        else if (Variable_Mostrar_Mapa == 1)
                        {
                            Imagen = Imagen_Altura_Original.Clone(new Rectangle(512, 512, Imagen_Altura_Original.Width - 1024, Imagen_Altura_Original.Height - 1024), Imagen_Altura_Original.PixelFormat);
                        }
                        else
                        {
                            Imagen = Imagen_Arco_Iris_Original.Clone(new Rectangle(512, 512, Imagen_Altura_Original.Width - 1024, Imagen_Altura_Original.Height - 1024), Imagen_Altura_Original.PixelFormat);
                        }
                    }
                    else
                    {
                        if (Variable_Mostrar_Mapa == 0)
                        {
                            if (Variable_Mostrar_Minerales) Imagen = Imagen_Biomas_Original.Clone() as Bitmap;
                            else Imagen = Imagen_Biomas_Original_Simple.Clone() as Bitmap;
                        }
                        else if (Variable_Mostrar_Mapa == 1)
                        {
                            Imagen = Imagen_Altura_Original.Clone() as Bitmap;
                        }
                        else
                        {
                            Imagen = Imagen_Arco_Iris_Original.Clone() as Bitmap;
                        }
                    }
                    if (Imagen != null)
                    {
                        if (Variable_Mostrar_Regla) // Add a full resolution semi-transparent grid.
                        {
                            int Ancho = Imagen.Width;
                            int Alto = Imagen.Height;
                            Graphics Pintar = Graphics.FromImage(Imagen);
                            Pintar.CompositingMode = CompositingMode.SourceOver; // Draw over the map.
                            Pintar.CompositingQuality = CompositingQuality.HighQuality;
                            Pintar.InterpolationMode = InterpolationMode.NearestNeighbor; // Pixelated.
                            Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Pintar.SmoothingMode = SmoothingMode.None; // Pixelated.
                            Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                            SolidBrush Pincel = new SolidBrush(Color.FromArgb(128, 0, 0, 0));
                            for (int Índice_Y = PixARK.Borde_Mundo; Índice_Y < Alto; Índice_Y += PixARK.Borde_Mundo)
                            {
                                int Y = (Índice_Y * Alto) / (Alto - PixARK.Borde_Mundo_Doble);
                                Pintar.FillRectangle(Pincel, 0, Y, Ancho, 1);
                            } // Vertical (Y) lines.
                            for (int Índice_X = PixARK.Borde_Mundo; Índice_X < Ancho; Índice_X += PixARK.Borde_Mundo)
                            {
                                int X = (Índice_X * Ancho) / (Ancho - PixARK.Borde_Mundo_Doble);
                                Pintar.FillRectangle(Pincel, X, 0, 1, Alto);
                            } // Horizontal (X) lines.
                            Pincel.Dispose();
                            Pincel = null;
                            Pintar.Dispose();
                            Pintar = null;
                        }
                        Clipboard.SetImage(Imagen);
                        Imagen.Dispose();
                        Imagen = null;
                        SystemSounds.Asterisk.Play();
                    }
                    Ruta = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally
            {
                GC.Collect();
                GC.GetTotalMemory(true);
                this.Cursor = Cursors.Default;
            }
        }

        private void Menú_Contextual_Guardar_Análisis_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(TextBox_Análisis.Text))
                {
                    string Ruta = Application.StartupPath + "\\Maps";
                    Program.Crear_Carpetas(Ruta);
                    Ruta += "\\" + Program.Obtener_Nombre_Temporal() + ", Seed " + Semilla.ToString() + ".txt";
                    FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    StreamWriter Lector_Texto = new StreamWriter(Lector, Encoding.UTF8);
                    Lector_Texto.WriteLine(TextBox_Análisis.Text);
                    Lector_Texto.Flush();
                    Lector_Texto.Close();
                    Lector_Texto.Dispose();
                    Lector_Texto = null;
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                    Ruta = null;
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (((Variable_Mostrar_Mapa == 0 && Imagen_Biomas_Original != null && Imagen_Biomas_Original_Simple != null) ||
                    (Variable_Mostrar_Mapa == 1 && Imagen_Altura_Original != null) ||
                    (Variable_Mostrar_Mapa == 2 && Imagen_Arco_Iris_Original != null)) &&
                    !string.IsNullOrEmpty(ComboBox_Mundo_PixARK.Text))
                {
                    string Ruta = Application.StartupPath + "\\Maps";
                    Program.Crear_Carpetas(Ruta);
                    Bitmap Imagen = null;
                    if (Variable_Ocultar_Bordes)
                    {
                        if (Variable_Mostrar_Mapa == 0)
                        {
                            if (Variable_Mostrar_Minerales) Imagen = Imagen_Biomas_Original.Clone(new Rectangle(512, 512, Imagen_Biomas_Original.Width - 1024, Imagen_Biomas_Original.Height - 1024), Imagen_Biomas_Original.PixelFormat);
                            else Imagen = Imagen_Biomas_Original_Simple.Clone(new Rectangle(512, 512, Imagen_Biomas_Original.Width - 1024, Imagen_Biomas_Original.Height - 1024), Imagen_Biomas_Original.PixelFormat);
                        }
                        else if (Variable_Mostrar_Mapa == 1)
                        {
                            Imagen = Imagen_Altura_Original.Clone(new Rectangle(512, 512, Imagen_Altura_Original.Width - 1024, Imagen_Altura_Original.Height - 1024), Imagen_Altura_Original.PixelFormat);
                        }
                        else
                        {
                            Imagen = Imagen_Arco_Iris_Original.Clone(new Rectangle(512, 512, Imagen_Altura_Original.Width - 1024, Imagen_Altura_Original.Height - 1024), Imagen_Altura_Original.PixelFormat);
                        }
                    }
                    else
                    {
                        if (Variable_Mostrar_Mapa == 0)
                        {
                            if (Variable_Mostrar_Minerales) Imagen = Imagen_Biomas_Original.Clone() as Bitmap;
                            else Imagen = Imagen_Biomas_Original_Simple.Clone() as Bitmap;
                        }
                        else if (Variable_Mostrar_Mapa == 1)
                        {
                            Imagen = Imagen_Altura_Original.Clone() as Bitmap;
                        }
                        else
                        {
                            Imagen = Imagen_Arco_Iris_Original.Clone() as Bitmap;
                        }
                    }
                    if (Imagen != null)
                    {
                        if (Variable_Mostrar_Regla) // Add a full resolution semi-transparent grid.
                        {
                            int Ancho = Imagen.Width;
                            int Alto = Imagen.Height;
                            Graphics Pintar = Graphics.FromImage(Imagen);
                            Pintar.CompositingMode = CompositingMode.SourceOver; // Draw over the map.
                            Pintar.CompositingQuality = CompositingQuality.HighQuality;
                            Pintar.InterpolationMode = InterpolationMode.NearestNeighbor; // Pixelated.
                            Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Pintar.SmoothingMode = SmoothingMode.None; // Pixelated.
                            Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                            SolidBrush Pincel = new SolidBrush(Color.FromArgb(128, 0, 0, 0));
                            for (int Índice_Y = PixARK.Borde_Mundo; Índice_Y < Alto; Índice_Y += PixARK.Borde_Mundo)
                            {
                                //int Y = (Índice_Y * Alto) / (Alto - PixARK.Borde_Mundo_Doble);
                                Pintar.FillRectangle(Pincel, 0, Índice_Y, Ancho, 1);
                            } // Vertical (Y) lines.
                            for (int Índice_X = PixARK.Borde_Mundo; Índice_X < Ancho; Índice_X += PixARK.Borde_Mundo)
                            {
                                //int X = (Índice_X * Ancho) / (Ancho - PixARK.Borde_Mundo_Doble);
                                Pintar.FillRectangle(Pincel, Índice_X, 0, 1, Alto);
                            } // Horizontal (X) lines.
                            Pincel.Dispose();
                            Pincel = null;
                            Pintar.Dispose();
                            Pintar = null;
                        }
                        Imagen.Save(Ruta + "\\" + Program.Obtener_Nombre_Temporal() + ", Seed " + Semilla.ToString() + /*" " + Path.GetFileName(ComboBox_Mundo_PixARK.Text) + */".png", ImageFormat.Png);
                        Imagen.Dispose();
                        Imagen = null;
                        SystemSounds.Asterisk.Play();
                    }
                    Ruta = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally
            {
                GC.Collect();
                GC.GetTotalMemory(true);
                this.Cursor = Cursors.Default;
            }
        }

        private void Picture_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (Color_ARGB_Bioma != Color.Empty) // Reset.
                {
                    Color_ARGB_Bioma = Color.Empty;
                    Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
                }
                Barra_Estado_Etiqueta_Bioma.Image = Program.Crear_Imagen_Color_Fondo(Color.Gray);
                Barra_Estado_Etiqueta_Bioma.Text = "Biome: ?";
                Barra_Estado_Etiqueta_Coordenadas.Text = "XY: ?, ?";
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                // The X and Y coordinates should be in client size "format".
                if (Imagen_Biomas_Original != null && Imagen_Biomas_Original_Simple != null && Imagen_Picture != null)
                {
                    int Ancho = Imagen_Picture.Width;
                    int Alto = Imagen_Picture.Height;
                    int X = e.X - ((Picture.ClientSize.Width - Ancho) / 2);
                    int Y = e.Y - ((Picture.ClientSize.Height - Alto) / 2);
                    if (X > -1 && X < Ancho && Y > -1 && Y < Alto)
                    {
                        if ((e.Button == MouseButtons.None || e.Button == MouseButtons.Right) && Color_ARGB_Bioma != Color.Empty) // Reset.
                        {
                            Color_ARGB_Bioma = Color.Empty;
                            Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
                        }
                        // Using "GetPixel()" is very slow, but for a single pixel it should be enough.
                        Color Color_ARGB = Imagen_Picture.GetPixel(X, Y);
                        string Texto_Bioma = "?";
                        for (int Índice_Bioma = 0; Índice_Bioma < PixARK.Biomas.Matriz_Biomas.Length; Índice_Bioma++)
                        {
                            if (PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Color.R == Color_ARGB.R &&
                                PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Color.G == Color_ARGB.G &&
                                PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Color.B == Color_ARGB.B)
                            {
                                Texto_Bioma = PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Índice.ToString() + ", " + PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Nombre + " (" + PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Dificultad.ToString() + ")" + (PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Minerales != PixARK.Minerales.Unknown ? " (" + PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Minerales.ToString().Replace('_', ' ') + ")" : null);
                                break;
                            }
                        }
                        Barra_Estado_Etiqueta_Bioma.Image = Program.Crear_Imagen_Color_Fondo(Color_ARGB);
                        Barra_Estado_Etiqueta_Bioma.Text = "Biome: " + Texto_Bioma;
                        int Ancho_Borde = Variable_Ocultar_Bordes ? Imagen_Biomas_Original.Width - PixARK.Borde_Mundo_Doble : Imagen_Biomas_Original.Width;
                        int Alto_Borde = Variable_Ocultar_Bordes ? Imagen_Biomas_Original.Height - PixARK.Borde_Mundo_Doble : Imagen_Biomas_Original.Height;
                        int Coordenada_X = ((X * Ancho_Borde) / Ancho) - (Ancho_Borde / 2);
                        // PixARK seems to use the "Y" as the depth in the 3D world instead of the usual "Z" and the "Z" is used for the height as the "Y".
                        int Coordenada_Y = ((Y * Alto_Borde) / Alto) - (Alto_Borde / 2);
                        Barra_Estado_Etiqueta_Coordenadas.Text = "XY: " + Program.Traducir_Número(Coordenada_X) + ", " + Program.Traducir_Número(Coordenada_Y);
                    }
                    else
                    {
                        Barra_Estado_Etiqueta_Bioma.Image = Program.Crear_Imagen_Color_Fondo(Color.Gray);
                        Barra_Estado_Etiqueta_Bioma.Text = "Biome: ?";
                        Barra_Estado_Etiqueta_Coordenadas.Text = "XY: ?, ?";
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Right)
                {
                    // The X and Y coordinates should be in client size "format".
                    if (Imagen_Biomas_Original != null && Imagen_Biomas_Original_Simple != null && Imagen_Picture != null)
                    {
                        int Ancho = Imagen_Picture.Width;
                        int Alto = Imagen_Picture.Height;
                        int X = e.X - ((Picture.ClientSize.Width - Ancho) / 2);
                        int Y = e.Y - ((Picture.ClientSize.Height - Alto) / 2);
                        if (X > -1 && X < Ancho && Y > -1 && Y < Alto)
                        {
                            // Using "GetPixel()" is very slow, but for a single pixel it should be enough.
                            Color Color_ARGB = Imagen_Picture.GetPixel(X, Y);
                            string Texto_Bioma = "?";
                            for (int Índice_Bioma = 0; Índice_Bioma < PixARK.Biomas.Matriz_Biomas.Length; Índice_Bioma++)
                            {
                                if (PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Color.R == Color_ARGB.R &&
                                    PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Color.G == Color_ARGB.G &&
                                    PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Color.B == Color_ARGB.B)
                                {
                                    Texto_Bioma = PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Índice.ToString() + ", " + PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Nombre + " (" + PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Dificultad.ToString() + ")" + (PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Minerales != PixARK.Minerales.Unknown ? " (" + PixARK.Biomas.Matriz_Biomas[Índice_Bioma].Minerales.ToString().Replace('_', ' ') + ")" : null);
                                    Color_ARGB_Bioma = Color_ARGB;
                                    Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
                                    break;
                                }
                            }
                            Barra_Estado_Etiqueta_Bioma.Image = Program.Crear_Imagen_Color_Fondo(Color_ARGB);
                            Barra_Estado_Etiqueta_Bioma.Text = "Biome: " + Texto_Bioma;
                        }
                        else
                        {
                            if (Color_ARGB_Bioma != Color.Empty) // Reset.
                            {
                                Color_ARGB_Bioma = Color.Empty;
                                Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
                            }
                            Barra_Estado_Etiqueta_Bioma.Image = Program.Crear_Imagen_Color_Fondo(Color.Gray);
                            Barra_Estado_Etiqueta_Bioma.Text = "Biome: ?";
                        }
                    }
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Function that makes a shrinked copy of the world map and places it in the main PictureBox.
        /// </summary>
        /// <param name="Imagen">Any valid image.</param>
        /// <param name="Color_ARGB">Any valid color to highlight, used to quickly spot the desired biomes. Use "Color.Empty" to ignore this function.</param>
        internal void Establecer_Imagen_Picture(Bitmap Imagen, Color Color_ARGB)
        {
            try
            {
                if (Imagen != null)
                {
                    int Ancho_Original = Imagen.Width;
                    int Alto_Original = Imagen.Height;
                    int Ancho_Cliente = Picture.ClientSize.Width;
                    int Alto_Cliente = Picture.ClientSize.Height;
                    // Keep the map's original aspect ratio.
                    int Ancho = (Alto_Cliente * Ancho_Original) / Alto_Original;
                    int Alto = (Ancho_Cliente * Alto_Original) / Ancho_Original;
                    if (Ancho <= Ancho_Cliente) Alto = Alto_Cliente;
                    else if (Alto <= Alto_Cliente) Ancho = Ancho_Cliente;
                    // Start the image that will be looked when the mouse moves over it to get the biomes.
                    Imagen_Picture = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                    Graphics Pintar = Graphics.FromImage(Imagen_Picture);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.NearestNeighbor; // Pixelated.
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None; // Pixelated. Makes any difference?
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                    // Also start the image to simulate the grid like PixARK does in game.
                    Bitmap Imagen_Regla = null;
                    Graphics Pintar_Regla = null;
                    SolidBrush Pincel_Regla = null;
                    if (Variable_Mostrar_Regla)
                    {
                        Imagen_Regla = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
                        Pintar_Regla = Graphics.FromImage(Imagen_Regla);
                        Pintar_Regla.CompositingMode = CompositingMode.SourceCopy;
                        Pintar_Regla.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar_Regla.InterpolationMode = InterpolationMode.NearestNeighbor;
                        Pintar_Regla.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar_Regla.SmoothingMode = SmoothingMode.None;
                        Pintar_Regla.TextRenderingHint = TextRenderingHint.AntiAlias;
                        Pincel_Regla = new SolidBrush(Color.FromArgb(128, 0, 0, 0));
                    }
                    if (Variable_Ocultar_Bordes)
                    {
                        Pintar.DrawImage(Imagen, new Rectangle(0, 0, Ancho, Alto), new Rectangle(PixARK.Borde_Mundo, PixARK.Borde_Mundo, Imagen.Width - PixARK.Borde_Mundo_Doble, Imagen.Height - PixARK.Borde_Mundo_Doble), GraphicsUnit.Pixel);
                        if (Variable_Mostrar_Regla)
                        {
                            for (int Índice_Y = PixARK.Borde_Mundo; Índice_Y < Alto_Original - PixARK.Borde_Mundo_Doble; Índice_Y += PixARK.Borde_Mundo)
                            {
                                int Y = (Índice_Y * Alto) / (Alto_Original - PixARK.Borde_Mundo_Doble);
                                Pintar_Regla.FillRectangle(Pincel_Regla, 0, Y, Ancho, 1);
                            } // Vertical (Y) lines.
                            for (int Índice_X = PixARK.Borde_Mundo; Índice_X < Ancho_Original - PixARK.Borde_Mundo_Doble; Índice_X += PixARK.Borde_Mundo)
                            {
                                int X = (Índice_X * Ancho) / (Ancho_Original - PixARK.Borde_Mundo_Doble);
                                Pintar_Regla.FillRectangle(Pincel_Regla, X, 0, 1, Alto);
                            } // Horizontal (X) lines.
                        }
                    }
                    else
                    {
                        Pintar.DrawImage(Imagen, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Imagen.Width, Imagen.Height), GraphicsUnit.Pixel);
                        if (Variable_Mostrar_Regla)
                        {
                            for (int Índice_Y = PixARK.Borde_Mundo; Índice_Y < Alto_Original; Índice_Y += PixARK.Borde_Mundo)
                            {
                                int Y = (Índice_Y * Alto) / Alto_Original;
                                Pintar_Regla.FillRectangle(Pincel_Regla, 0, Y, Ancho, 1);
                            } // Vertical (Y) lines.
                            for (int Índice_X = PixARK.Borde_Mundo; Índice_X < Ancho_Original; Índice_X += PixARK.Borde_Mundo)
                            {
                                int X = (Índice_X * Ancho) / Ancho_Original;
                                Pintar_Regla.FillRectangle(Pincel_Regla, X, 0, 1, Alto);
                            } // Horizontal (X) lines.
                        }
                    }
                    if (Variable_Mostrar_Regla)
                    {
                        Pincel_Regla.Dispose();
                        Pincel_Regla = null;
                        Pintar_Regla.Dispose();
                        Pintar_Regla = null;
                    }
                    Pintar.Dispose();
                    Pintar = null;
                    if (Color_ARGB != Color.Empty)
                    {
                        BitmapData Bitmap_Data = Imagen_Picture.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen_Picture.PixelFormat);
                        byte[] Matriz_Bytes_ARGB = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                        Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes_ARGB, 0, Matriz_Bytes_ARGB.Length);
                        int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen_Picture.PixelFormat) ? 4 : 3;
                        int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen_Picture.PixelFormat)) / 8);
                        for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                        {
                            for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                            {
                                if (Matriz_Bytes_ARGB[Índice + 3] > 0)
                                {
                                    if (Matriz_Bytes_ARGB[Índice + 2] != Color_ARGB.R ||
                                        Matriz_Bytes_ARGB[Índice + 1] != Color_ARGB.G ||
                                        Matriz_Bytes_ARGB[Índice] != Color_ARGB.B)
                                    {
                                        // Only leave the desired color.
                                        Matriz_Bytes_ARGB[Índice + 2] = 128;
                                        Matriz_Bytes_ARGB[Índice + 1] = 128;
                                        Matriz_Bytes_ARGB[Índice + 0] = 128;
                                    }
                                }
                            }
                        }
                        Marshal.Copy(Matriz_Bytes_ARGB, 0, Bitmap_Data.Scan0, Matriz_Bytes_ARGB.Length);
                        Imagen_Picture.UnlockBits(Bitmap_Data);
                        Bitmap_Data = null;
                        Matriz_Bytes_ARGB = null;
                    }
                    Picture.BackgroundImage = Imagen_Picture;
                    Picture.Image = Imagen_Regla;
                }
                else
                {
                    Imagen_Picture = null;
                    Picture.BackgroundImage = null;
                    Picture.Image = null;
                }
                Picture.Refresh();
                GC.Collect();
                GC.GetTotalMemory(true);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Pictures_Leyenda_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                Color_ARGB_Bioma = Color.Empty;
                Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Controles_Leyenda_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Right)
                {
                    Control Control = sender as Control;
                    if (Control != null)
                    {
                        int Índice_Bioma = -1;
                        try
                        {
                            Índice_Bioma = int.Parse(Control.Name.Replace("Picture_Leyenda_", null).Replace("Etiqueta_Leyenda_", null));
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; Índice_Bioma = -1; }
                        if (Índice_Bioma > -1)
                        {
                            Color_ARGB_Bioma = Matriz_Pictures[Índice_Bioma].BackColor;
                        }
                        else Color_ARGB_Bioma = Color.Empty;
                    }
                    else Color_ARGB_Bioma = Color.Empty;
                    Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Controles_Leyenda_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Color_ARGB_Bioma = Color.Empty;
                Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Donar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Program.Ejecutar_Ruta("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=KSMZ3XNG2R9P6", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Correo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Program.Ejecutar_Ruta("mailto:jupitermauro@gmail.com?subject=" + Program.Texto_Título + " " + Program.Texto_Versión + ", " + Program.Texto_Fecha.Replace("_", null), ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Jupisoft_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Program.Ejecutar_Ruta("http://jupisoft.x10host.com/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Hermitcraft_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Program.Ejecutar_Ruta("http://hermitcraft.com/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_GitHub_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Program.Ejecutar_Ruta("https://github.com/Jupisoft111/PixARK-Tools", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_PixARK_Wiki_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Program.Ejecutar_Ruta("https://pixark.gamepedia.com/index.php", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Pantalla_Completa_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.SizeChanged -= Ventana_Principal_SizeChanged;
                Variable_Pantalla_Completa = Menú_Contextual_Pantalla_Completa.Checked;
                Menú_Contextual_Siempre_Visible.Enabled = !Variable_Pantalla_Completa;
                if (!Variable_Pantalla_Completa)
                {
                    this.TopMost = Variable_Siempre_Visible;
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.WindowState = FormWindowState.Maximized;
                    Cursor.Show();
                }
                else
                {
                    Cursor.Hide();
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    this.TopMost = true;
                }
                this.SizeChanged += Ventana_Principal_SizeChanged;
                Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_PixARK_Wiki_Mapas_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Program.Ejecutar_Ruta("https://pixark.gamepedia.com/Maps#List_of_Seeds", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Sitio_PixARK_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Program.Ejecutar_Ruta("https://pixark.snail.com/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void ComboBox_Truco_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Truco.Refresh();
                Establecer_Texto_Código();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void ListBox_Objeto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //ListBox_Objeto.Refresh();
                Establecer_Texto_Código();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Cantidad_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Establecer_Texto_Código();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Calidad_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Establecer_Texto_Código();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Plano_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Establecer_Texto_Código();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Código_TextChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Copiar_Código_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(TextBox_Código.Text))
                {
                    Clipboard.SetText(TextBox_Código.Text);
                    SystemSounds.Asterisk.Play();
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        internal void Establecer_Texto_Código()
        {
            try
            {
                if (ComboBox_Truco.SelectedIndex > -1)
                {
                    if (ComboBox_Truco.SelectedIndex == 0)
                    {
                        ListBox_Objeto.Enabled = false;
                        NumericUpDown_Cantidad.Enabled = false;
                        NumericUpDown_Calidad.Enabled = false;
                        NumericUpDown_Plano.Enabled = false;
                        TextBox_Código.Text = "cheat OpenFogOfWar";
                    }
                    else if (ComboBox_Truco.SelectedIndex == 1)
                    {
                        ListBox_Objeto.Enabled = true;
                        NumericUpDown_Cantidad.Enabled = true;
                        NumericUpDown_Calidad.Enabled = true;
                        NumericUpDown_Plano.Enabled = true;
                        TextBox_Código.Text = "cheat GiveItemNum " + ListBox_Objeto.SelectedIndex.ToString() + " " + NumericUpDown_Cantidad.Value.ToString() + " " + NumericUpDown_Calidad.Value.ToString() + " " + NumericUpDown_Plano.Value.ToString();
                    }
                }
                else
                {
                    ListBox_Objeto.Enabled = false;
                    NumericUpDown_Cantidad.Enabled = false;
                    NumericUpDown_Calidad.Enabled = false;
                    NumericUpDown_Plano.Enabled = false;
                    TextBox_Código.Text = null;
                }
                TextBox_Código.Refresh();
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mostrar_Mapa_Biomas_Click(object sender, EventArgs e)
        {
            try
            {
                Variable_Mostrar_Mapa = 0;
                Menú_Contextual_Mostrar_Mapa_Altura.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Arco_Iris.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Biomas.Checked = true;
                Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mostrar_Mapa_Altura_Click(object sender, EventArgs e)
        {
            try
            {
                Variable_Mostrar_Mapa = 1;
                Menú_Contextual_Mostrar_Mapa_Biomas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Arco_Iris.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Altura.Checked = true;
                Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Mostrar_Mapa_Arco_Iris_Click(object sender, EventArgs e)
        {
            try
            {
                Variable_Mostrar_Mapa = 2;
                Menú_Contextual_Mostrar_Mapa_Biomas.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Altura.Checked = false;
                Menú_Contextual_Mostrar_Mapa_Arco_Iris.Checked = true;
                Establecer_Imagen_Picture(Variable_Mostrar_Mapa == 0 ? (Variable_Mostrar_Minerales ? Imagen_Biomas_Original : Imagen_Biomas_Original_Simple) : (Variable_Mostrar_Mapa == 1 ? Imagen_Altura_Original : Imagen_Arco_Iris_Original), Color_ARGB_Bioma);
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}

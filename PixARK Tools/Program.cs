using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixARK_Tools
{
    /// <summary>
    /// WARNING: The "Finisar SQLite" library seems to need the "unsafe" compile option.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The build and update date of this tool, used to know if older settings should be reset.
        /// </summary>
        internal static readonly string Texto_Fecha = "2019_10_18_20_55_58_069"; // 2019_10_07_09_47_43_204.
        /// <summary>
        /// The maximum PixARK version that this tool was tested with (if I looked it right). Others might not work at all.
        /// </summary>
        internal static readonly string Texto_PixARK_Versión = "1.67";

        /// <summary>
        /// Since the application was first designed for "Xisumavoid", this will be the default user name, but can be changed later from the help menu.
        /// </summary>
        internal static string Texto_Usuario = Environment.UserName;
        internal static string Texto_Título = "PixARK Tools by Jupisoft";
        internal static string Texto_Programa = "PixARK Tools";
        internal static readonly string Texto_Versión = "1.0";
        internal static readonly string Texto_Versión_Fecha = Texto_Versión + " (" + Texto_Fecha/*.Replace("_", null)*/ + ")";
        internal static string Texto_Título_Versión = Texto_Título + " " + Texto_Versión;

        internal static Random Rand = new Random();

        /// <summary>
        /// Using this icon instead of adding it to the designer of each form saved almost 11 MB of space in the whole application. Why doesn't .NET make a CRC of the icon and only adds it once to the whole project?
        /// </summary>
        internal static Icon Icono_Jupisoft = null;

        internal static Process Proceso = Process.GetCurrentProcess();
        internal static PerformanceCounter Rendimiento_Procesador = null;

        internal static List<char> Lista_Caracteres_Prohibidos = new List<char>();
        internal static readonly char Caracter_Coma_Decimal = (0.5d).ToString()[1];
        internal static readonly char Caracter_Punto_Decimal = Caracter_Coma_Decimal != '.' ? '.' : ',';
        internal static readonly char Caracter_Signo_Negativo = (-1).ToString()[0];

        internal static readonly Color[] Matriz_12_Colores = new Color[12]
        {
            Color.FromArgb(255, 255, 0, 0), // Red.
            Color.FromArgb(255, 255, 160, 0), // Orange.
            Color.FromArgb(255, 255, 255, 0), // Yellow.
            Color.FromArgb(255, 160, 255, 0), // Lime.
            Color.FromArgb(255, 0, 255, 0), // Green.
            Color.FromArgb(255, 0, 255, 160), // Turquoise.
            Color.FromArgb(255, 0, 255, 255), // Cyan.
            Color.FromArgb(255, 0, 160, 255), // Light blue.
            Color.FromArgb(255, 0, 0, 255), // Blue.
            Color.FromArgb(255, 160, 0, 255), // Purple.
            Color.FromArgb(255, 255, 0, 255), // Magenta.
            Color.FromArgb(255, 255, 0, 160), // Pink.
        };
        internal static readonly Color[] Matriz_8_Colores = new Color[8]
        {
            Color.FromArgb(255, 0, 0, 0), // Black.
            Color.FromArgb(255, 255, 0, 0), // Red.
            Color.FromArgb(255, 255, 255, 0), // Yellow.
            Color.FromArgb(255, 0, 255, 0), // Green.
            Color.FromArgb(255, 0, 255, 255), // Cyan.
            Color.FromArgb(255, 0, 0, 255), // Blue.
            Color.FromArgb(255, 255, 0, 255), // Magenta.
            Color.FromArgb(255, 255, 255, 255) // White.
        };
        internal static readonly Color[] Matriz_Colores_12_Notas = new Color[12] { Color.FromArgb(255, 0, 0), Color.FromArgb(255, 144, 0), Color.FromArgb(255, 176, 0), Color.FromArgb(255, 216, 0), Color.FromArgb(255, 255, 0), Color.FromArgb(0, 255, 0), Color.FromArgb(0, 255, 192), Color.FromArgb(0, 96, 255), Color.FromArgb(80, 0, 255), Color.FromArgb(128, 0, 255), Color.FromArgb(160, 0, 255), Color.FromArgb(255, 0, 176) };
        internal static readonly Pen[] Matriz_Lápices_12_Notas = new Pen[12] { new Pen(Color.FromArgb(255, 0, 0)), new Pen(Color.FromArgb(255, 144, 0)), new Pen(Color.FromArgb(255, 176, 0)), new Pen(Color.FromArgb(255, 216, 0)), new Pen(Color.FromArgb(255, 255, 0)), new Pen(Color.FromArgb(0, 255, 0)), new Pen(Color.FromArgb(0, 255, 192)), new Pen(Color.FromArgb(0, 96, 255)), new Pen(Color.FromArgb(80, 0, 255)), new Pen(Color.FromArgb(128, 0, 255)), new Pen(Color.FromArgb(160, 0, 255)), new Pen(Color.FromArgb(255, 0, 176)) };
        internal static readonly SolidBrush[] Matriz_Pinceles_12_Notas = new SolidBrush[12] { new SolidBrush(Color.FromArgb(255, 0, 0)), new SolidBrush(Color.FromArgb(255, 144, 0)), new SolidBrush(Color.FromArgb(255, 176, 0)), new SolidBrush(Color.FromArgb(255, 216, 0)), new SolidBrush(Color.FromArgb(255, 255, 0)), new SolidBrush(Color.FromArgb(0, 255, 0)), new SolidBrush(Color.FromArgb(0, 255, 192)), new SolidBrush(Color.FromArgb(0, 96, 255)), new SolidBrush(Color.FromArgb(80, 0, 255)), new SolidBrush(Color.FromArgb(128, 0, 255)), new SolidBrush(Color.FromArgb(160, 0, 255)), new SolidBrush(Color.FromArgb(255, 0, 176)) };

        /// <summary>
        /// Array that stores 16 of the 1.530 unique colors with pure hue, maximum saturation and middle lightness, but in a loop of 16 for it's 256 values.
        /// </summary>
        internal static Color[] Matriz_Colores_Arco_Iris_16 = null;
        /// <summary>
        /// Array that stores the 256 unique gray scale colors in 8 bits, so the red, green and blue channels are the same for each color. Can also represent each possible byte with a unique gray tone.
        /// </summary>
        internal static Color[] Matriz_Colores_Grises_256 = null;
        /// <summary>
        /// Array that stores 256 of the 1.530 unique colors with pure hue, maximum saturation and middle lightness.
        /// </summary>
        internal static Color[] Matriz_Colores_Arco_Iris_256 = null;
        /// <summary>
        /// Array that stores 256 colors with pure hue, maximum saturation and middle lightness, but used for termography or topography, where the index 0 is fuchsia (magenta) and the index 255 in red, so it contains 83,3333 % of a full rainbow (5 of 6) but in reverse order.
        /// </summary>
        internal static Color[] Matriz_Colores_Termografía_256 = null;

        /// <summary>
        /// Function that returns one of the 1.530 possible 24 bits RGB colors with full saturation and middle brightness.
        /// </summary>
        /// <param name="Índice">Any value between 0 and 1529. Red = 0, Yellow = 255, Green = 510, Cyan = 765, blue = 1020, purple = 1275. If the value is below 0 or above 1529, pure white will be returned instead.</param>
        /// <returns>Returns an ARGB color based on the selected index, or white if out of bounds.</returns>
        internal static Color Obtener_Color_Puro_1530(int Índice)
        {
            try
            {
                if (Índice >= 0 && Índice <= 1529)
                {
                    if (Índice < 255) return Color.FromArgb(255, Índice, 0);
                    else if (Índice < 510) return Color.FromArgb(510 - Índice, 255, 0);
                    else if (Índice < 765) return Color.FromArgb(0, 255, 255 - (765 - Índice));
                    else if (Índice < 1020) return Color.FromArgb(0, 1020 - Índice, 255);
                    else if (Índice < 1275) return Color.FromArgb(255 - (1275 - Índice), 0, 255);
                    else return Color.FromArgb(255, 0, 1530 - Índice);
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return Color.FromArgb(255, 255, 255);
        }

        internal static class HSL
        {
            /// <summary>
            /// Convierte un color RGB en uno HSL.
            /// </summary>
            /// <param name="Rojo">Valor entre 0 y 255.</param>
            /// <param name="Verde">Valor entre 0 y 255.</param>
            /// <param name="Azul">Valor entre 0 y 255.</param>
            /// <param name="Matiz">Valor entre 0 y 360.</param>
            /// <param name="Saturación">Valor entre 0 y 100.</param>
            /// <param name="Luminosidad">Valor entre 0 y 100.</param>
            internal static void From_RGB(byte Rojo, byte Verde, byte Azul, out double Matiz, out double Saturación, out double Luminosidad)
            {
                Matiz = 0d;
                Saturación = 0d;
                Luminosidad = 0d;
                double Rojo_1 = Rojo / 255d;
                double Verde_1 = Verde / 255d;
                double Azul_1 = Azul / 255d;
                double Máximo, Mínimo, Diferencia;
                Máximo = Math.Max(Rojo_1, Math.Max(Verde_1, Azul_1));
                Mínimo = Math.Min(Rojo_1, Math.Min(Verde_1, Azul_1));
                Luminosidad = (Mínimo + Máximo) / 2d;
                if (Luminosidad <= 0d) return;
                Diferencia = Máximo - Mínimo;
                Saturación = Diferencia;
                if (Saturación > 0d) Saturación /= (Luminosidad <= 0.5d) ? (Máximo + Mínimo) : (2d - Máximo - Mínimo);
                else
                {
                    //Luminosidad = Math.Round(Luminosidad * 100d, 1, MidpointRounding.AwayFromZero);
                    Luminosidad *= Luminosidad * 100d;
                    return;
                }
                double Rojo_2 = (Máximo - Rojo_1) / Diferencia;
                double Verde_2 = (Máximo - Verde_1) / Diferencia;
                double Azul_2 = (Máximo - Azul_1) / Diferencia;
                if (Rojo_1 == Máximo) Matiz = (Verde_1 == Mínimo ? 5d + Azul_2 : 1d - Verde_2);
                else if (Verde_1 == Máximo) Matiz = (Azul_1 == Mínimo ? 1d + Rojo_2 : 3d - Azul_2);
                else Matiz = (Rojo_1 == Mínimo ? 3d + Verde_2 : 5d - Rojo_2);
                Matiz /= 6d;
                if (Matiz >= 1d) Matiz = 0d;
                Matiz *= 360d;
                Saturación *= 100d;
                Luminosidad *= 100d;
                //if (Matiz < 0d || Matiz >= 360d) MessageBox.Show("To Matiz", Matiz.ToString());
                //if (Saturación < 0d || Saturación > 100d) MessageBox.Show("To Saturación");
                //if (Luminosidad < 0d || Luminosidad > 100d) MessageBox.Show("To Luminosidad");
                //Matiz = Math.Round(Matiz * 360d, 1, MidpointRounding.AwayFromZero); // 0.0d ~ 360.0d
                //Saturación = Math.Round(Saturación * 100d, 1, MidpointRounding.AwayFromZero); // 0.0d ~ 100.0d
                //Luminosidad = Math.Round(Luminosidad * 100d, 1, MidpointRounding.AwayFromZero); // 0.0d ~ 100.0d
                //if (Matiz >= 360d) Matiz = 0d;
            }

            /// <summary>
            /// Convierte un color HSL en uno RGB.
            /// </summary>
            /// <param name="Matiz">Valor entre 0 y 360.</param>
            /// <param name="Saturación">Valor entre 0 y 100.</param>
            /// <param name="Luminosidad">Valor entre 0 y 100.</param>
            /// <param name="Rojo">Valor entre 0 y 255.</param>
            /// <param name="Verde">Valor entre 0 y 255.</param>
            /// <param name="Azul">Valor entre 0 y 255.</param>
            internal static void To_RGB(double Matiz, double Saturación, double Luminosidad, out byte Rojo, out byte Verde, out byte Azul)
            {
                if (Matiz >= 360d) Matiz = 0d;
                //Matiz = Math.Round(Matiz, 1, MidpointRounding.AwayFromZero);
                //Saturación = Math.Round(Saturación, 1, MidpointRounding.AwayFromZero);
                //Luminosidad = Math.Round(Luminosidad, 1, MidpointRounding.AwayFromZero);
                Matiz /= 360d; // 0.0d ~ 1.0d
                Saturación /= 100d; // 0.0d ~ 1.0d
                Luminosidad /= 100d; // 0.0d ~ 1.0d
                double Rojo_Temporal = Luminosidad; // Default to Gray
                double Verde_Temporal = Luminosidad;
                double Azul_Temporal = Luminosidad;
                double v = Luminosidad <= 0.5d ? (Luminosidad * (1d + Saturación)) : (Luminosidad + Saturación - Luminosidad * Saturación);
                if (v > 0d)
                {
                    double m, sv, Sextante, fract, vsf, mid1, mid2;
                    m = Luminosidad + Luminosidad - v;
                    sv = (v - m) / v;
                    Matiz *= 6d;
                    Sextante = Math.Floor(Matiz);
                    fract = Matiz - Sextante;
                    vsf = v * sv * fract;
                    mid1 = m + vsf;
                    mid2 = v - vsf;
                    if (Sextante == 0d)
                    {
                        Rojo_Temporal = v;
                        Verde_Temporal = mid1;
                        Azul_Temporal = m;
                    }
                    else if (Sextante == 1d)
                    {
                        Rojo_Temporal = mid2;
                        Verde_Temporal = v;
                        Azul_Temporal = m;
                    }
                    else if (Sextante == 2d)
                    {
                        Rojo_Temporal = m;
                        Verde_Temporal = v;
                        Azul_Temporal = mid1;
                    }
                    else if (Sextante == 3d)
                    {
                        Rojo_Temporal = m;
                        Verde_Temporal = mid2;
                        Azul_Temporal = v;
                    }
                    else if (Sextante == 4d)
                    {
                        Rojo_Temporal = mid1;
                        Verde_Temporal = m;
                        Azul_Temporal = v;
                    }
                    else if (Sextante == 5d)
                    {
                        Rojo_Temporal = v;
                        Verde_Temporal = m;
                        Azul_Temporal = mid2;
                    }
                }
                Rojo = (byte)Math.Round(Rojo_Temporal * 255d, MidpointRounding.AwayFromZero);
                Verde = (byte)Math.Round(Verde_Temporal * 255d, MidpointRounding.AwayFromZero);
                Azul = (byte)Math.Round(Azul_Temporal * 255d, MidpointRounding.AwayFromZero);
            }

            /// <summary>
            /// Obtains a hue value between 0 and 11 for the specified color, or 12 if it's in gray scale.
            /// </summary>
            /// <param name="Rojo">Red value between 0 and 255.</param>
            /// <param name="Verde">Green value between 0 and 255.</param>
            /// <param name="Azul">Blue value between 0 and 255.</param>
            /// <returns>Returns a value between 0 and 11, or 12 if the color it's in gray scale or on any error.</returns>
            internal static int Obtener_Matiz_0_a_11(byte Rojo, byte Verde, byte Azul)
            {
                try
                {
                    if (Rojo != Verde || Rojo != Azul) // Not gray.
                    {
                        double Rojo_1 = Rojo / 255d;
                        double Verde_1 = Verde / 255d;
                        double Azul_1 = Azul / 255d;
                        double Mínimo = Math.Min(Rojo_1, Math.Min(Verde_1, Azul_1));
                        double Máximo = Math.Max(Rojo_1, Math.Max(Verde_1, Azul_1));
                        double Diferencia = Máximo - Mínimo;
                        double Rojo_2 = (Máximo - Rojo_1) / Diferencia;
                        double Verde_2 = (Máximo - Verde_1) / Diferencia;
                        double Azul_2 = (Máximo - Azul_1) / Diferencia;
                        double Matiz_Temporal = 0d;
                        if (Rojo_1 == Máximo) Matiz_Temporal = (Verde_1 == Mínimo ? 5d + Azul_2 : 1d - Verde_2);
                        else if (Verde_1 == Máximo) Matiz_Temporal = (Azul_1 == Mínimo ? 1d + Rojo_2 : 3d - Azul_2);
                        else Matiz_Temporal = (Rojo_1 == Mínimo ? 3d + Verde_2 : 5d - Rojo_2);
                        if (Matiz_Temporal >= 6d) Matiz_Temporal = 0d;
                        int Matiz = (int)(Matiz_Temporal * 510d);
                        //int Matiz = (int)(Matiz_Temporal * 2d);
                        if (Matiz >= 0 || Matiz <= 1529)
                        {
                            if (Matiz > 2933 || Matiz <= 128) return 0;
                            else if (Matiz <= 383) return 1;
                            else if (Matiz <= 638) return 2;
                            else if (Matiz <= 893) return 3;
                            else if (Matiz <= 1148) return 4;
                            else if (Matiz <= 1403) return 5;
                            else if (Matiz <= 1658) return 6;
                            else if (Matiz <= 1913) return 7;
                            else if (Matiz <= 2168) return 8;
                            else if (Matiz <= 2423) return 9;
                            else if (Matiz <= 2678) return 10;
                            else return 11;
                        }
                        else Matiz = 12;
                        return Matiz;
                    }
                    /*int Matiz = Obtener_Matiz_0_a_1529(Rojo, Verde, Azul);
                    if (Matiz != 1530)
                    {
                        if (Matiz > 2933 || Matiz <= 128) return 0;
                        else if (Matiz <= 383) return 1;
                        else if (Matiz <= 638) return 2;
                        else if (Matiz <= 893) return 3;
                        else if (Matiz <= 1148) return 4;
                        else if (Matiz <= 1403) return 5;
                        else if (Matiz <= 1658) return 6;
                        else if (Matiz <= 1913) return 7;
                        else if (Matiz <= 2168) return 8;
                        else if (Matiz <= 2423) return 9;
                        else if (Matiz <= 2678) return 10;
                        else return 11;
                    }*/
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                return 12; // Gray.
            }

            /// <summary>
            /// Obtains a hue value between 0 and 1529 for the specified color, or 1530 if it's in gray scale.
            /// </summary>
            /// <param name="Rojo">Red value between 0 and 255.</param>
            /// <param name="Verde">Green value between 0 and 255.</param>
            /// <param name="Azul">Blue value between 0 and 255.</param>
            /// <returns>Returns a value between 0 and 1529, or 1530 if the color it's in gray scale or on any error.</returns>
            internal static int Obtener_Matiz_0_a_1529(byte Rojo, byte Verde, byte Azul)
            {
                try
                {
                    if (Rojo != Verde || Rojo != Azul) // Not gray.
                    {
                        double Rojo_1 = Rojo / 255d;
                        double Verde_1 = Verde / 255d;
                        double Azul_1 = Azul / 255d;
                        double Mínimo = Math.Min(Rojo_1, Math.Min(Verde_1, Azul_1));
                        double Máximo = Math.Max(Rojo_1, Math.Max(Verde_1, Azul_1));
                        double Diferencia = Máximo - Mínimo;
                        double Rojo_2 = (Máximo - Rojo_1) / Diferencia;
                        double Verde_2 = (Máximo - Verde_1) / Diferencia;
                        double Azul_2 = (Máximo - Azul_1) / Diferencia;
                        double Matiz_Temporal = 0d;
                        if (Rojo_1 == Máximo) Matiz_Temporal = (Verde_1 == Mínimo ? 5d + Azul_2 : 1d - Verde_2);
                        else if (Verde_1 == Máximo) Matiz_Temporal = (Azul_1 == Mínimo ? 1d + Rojo_2 : 3d - Azul_2);
                        else Matiz_Temporal = (Rojo_1 == Mínimo ? 3d + Verde_2 : 5d - Rojo_2);
                        if (Matiz_Temporal >= 6d) Matiz_Temporal = 0d;
                        int Matiz = (int)(Matiz_Temporal * 255d);
                        if (Matiz < 0 || Matiz > 1529) Matiz = 1530;
                        return Matiz;
                    }
                    /*if (Rojo != Verde || Rojo != Azul)
                    {
                        byte Mínimo = Math.Min(Rojo, Math.Min(Verde, Azul));
                        byte Máximo = Math.Max(Rojo, Math.Max(Verde, Azul));
                        double Diferencia = (double)(Máximo - Mínimo);
                        double Matiz_Temporal = 0d;
                        if (Rojo == Máximo)
                        {
                            if (Verde == Mínimo) Matiz_Temporal = 1275d + ((double)(Máximo - Azul) / Diferencia);
                            else Matiz_Temporal = 255d - ((double)(Máximo - Verde) / Diferencia);
                        }
                        else if (Verde == Máximo)
                        {
                            if (Azul == Mínimo) Matiz_Temporal = 255d + ((double)(Máximo - Rojo) / Diferencia);
                            else Matiz_Temporal = 765d - ((double)(Máximo - Azul) / Diferencia);
                        }
                        else
                        {
                            if (Rojo == Mínimo) Matiz_Temporal = 765d + ((double)(Máximo - Verde) / Diferencia);
                            else Matiz_Temporal = 1275d - ((double)(Máximo - Rojo) / Diferencia);
                        }
                        int Matiz = (int)Matiz_Temporal;
                        if (Matiz < 0) Matiz = 0;
                        else if (Matiz > 1529) Matiz = 0;
                        return Matiz;
                    }*/
                }
                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                return 1530; // Gray.
            }

            internal static byte Obtener_Matiz_0_a_255(byte Rojo, byte Verde, byte Azul)
            {
                /*int Matiz = 0;
                int Saturación = 0;
                int Luminosidad = 0;
                //double Rojo_1 = Rojo / 255d;
                //double Verde_1 = Verde / 255d;
                //double Azul_1 = Azul / 255d;
                //double Máximo, Mínimo, Diferencia;
                int Máximo = Math.Max(Rojo, Math.Max(Verde, Azul));
                int Mínimo = Math.Min(Rojo, Math.Min(Verde, Azul));
                Luminosidad = (Mínimo + Máximo) / 2;
                if (Luminosidad <= 0) return 0;
                int Diferencia = Máximo - Mínimo;
                Saturación = Diferencia;
                if (Saturación > 0) Saturación /= (Luminosidad <= 128) ? (Máximo + Mínimo) : (510 - Máximo - Mínimo);
                else
                {
                    //Luminosidad = Math.Round(Luminosidad * 100d, 1, MidpointRounding.AwayFromZero);
                    return 0;
                }
                int Rojo_2 = (Máximo - Rojo) / Diferencia;
                int Verde_2 = (Máximo - Verde) / Diferencia;
                int Azul_2 = (Máximo - Azul) / Diferencia;
                if (Rojo == Máximo) Matiz = (Verde == Mínimo ? 1275 + Azul_2 : 255 - Verde_2);
                else if (Verde == Máximo) Matiz = (Azul == Mínimo ? 255 + Rojo_2 : 765 - Azul_2);
                else Matiz = (Rojo == Mínimo ? 765 + Verde_2 : 1275 - Rojo_2);
                if (Matiz >= 1530) Matiz = 0;
                Matiz /= 6;

                //Matiz *= 360d;
                //Saturación *= 100d;
                //Luminosidad *= 100d;



                if (Rojo != Verde || Rojo != Azul)
                {
                    int Matiz = 0;
                    Byte Mínimo = Math.Min(Rojo, Math.Min(Verde, Azul));
                    Byte Máximo = Math.Max(Rojo, Math.Max(Verde, Azul));
                    if (Rojo == Máximo) Matiz = (Verde == Mínimo ? (5 * 255) + (((Máximo - Azul) * 255) / (Máximo - Mínimo)) : (1 * 255) - (((Máximo - Verde) * 255) / (Máximo - Mínimo)));
                    else if (Verde == Máximo) Matiz = (Azul == Mínimo ? (1 * 255) + (((Máximo - Rojo) * 255) / (Máximo - Mínimo)) : (3 * 255) - (((Máximo - Azul) * 255) / (Máximo - Mínimo)));
                    else Matiz = (Rojo == Mínimo ? (3 * 255) + (((Máximo - Verde) * 255) / (Máximo - Mínimo)) : (5 * 255) - (((Máximo - Rojo) * 255) / (Máximo - Mínimo)));
                    Matiz++; // 2013_02_10_09_13_04_593
                    if (Matiz >/*=*//* 1530) Matiz = 0;
                    return (Byte)(Matiz / 6);
                }*/
                return 0;
            }

            internal static byte Obtener_Saturación_0_a_255(byte Rojo, byte Verde, byte Azul)
            {
                if (Rojo != Verde || Rojo != Azul)
                {
                    byte Mínimo = Math.Min(Rojo, Math.Min(Verde, Azul));
                    byte Máximo = Math.Max(Rojo, Math.Max(Verde, Azul));
                    return (byte)(((Máximo - Mínimo) * 255) / ((((Mínimo + Máximo) / 2) <= 128) ? (Máximo + Mínimo) : (510 - Máximo - Mínimo)));
                }
                return 0;
            }

            internal static byte Obtener_Brillo_0_a_255(byte Rojo, byte Verde, byte Azul)
            {
                return (byte)((Math.Min(Rojo, Math.Min(Verde, Azul)) + Math.Max(Rojo, Math.Max(Verde, Azul))) / 2);
            }
        }

        /// <summary>
        /// Creates all the directories is the specified path if they don't exist yet, without showing any exception.
        /// </summary>
        /// <param name="Ruta">Any valid directory path.</param>
        /// <returns>Returns true if the specified directories in the path now exist. Returns false on any exception, possibly indicating that the directories might not exist.</returns>
        internal static bool Crear_Carpetas(string Ruta)
        {
            try
            {
                if (!Directory.Exists(Ruta))
                {
                    Directory.CreateDirectory(Ruta);
                    return true;
                }
                else return true;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return false;
        }

        /// <summary>
        /// Function that generates an image of 16 x 16 with a black border around and colored with the desired color.
        /// </summary>
        /// <param name="Color_ARGB">Any valid color.</param>
        /// <returns>Returns a new colored image. Returns null on any error.</returns>
        internal static Bitmap Crear_Imagen_Color_Fondo(Color Color_ARGB)
        {
            try
            {
                Bitmap Imagen = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.Clear(Color.Black); // For the borders.
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.HighQuality;
                Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                SolidBrush Pincel = new SolidBrush(!Color_ARGB.IsEmpty ? Color_ARGB : Color.FromArgb(255, 128, 128, 128));
                Pintar.FillRectangle(Pincel, 1, 1, 14, 14);
                Pincel.Dispose();
                Pincel = null;
                Pintar.Dispose();
                Pintar = null;
                return Imagen;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Executes the specified file, directory or URL, with the specified window style.
        /// </summary>
        /// <param name="Ruta">Any valid file or directory path.</param>
        /// <param name="Estado">Any valid window style.</param>
        /// <returns>Returns true if the process can be executed. Returns false if it can't be executed.</returns>
        internal static bool Ejecutar_Ruta(string Ruta, ProcessWindowStyle Estado)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta))
                {
                    Process Proceso = new Process();
                    Proceso.StartInfo.Arguments = null;
                    Proceso.StartInfo.ErrorDialog = false;
                    Proceso.StartInfo.FileName = Ruta;
                    Proceso.StartInfo.UseShellExecute = true;
                    Proceso.StartInfo.Verb = "open";
                    Proceso.StartInfo.WindowStyle = Estado;
                    if (File.Exists(Ruta)) Proceso.StartInfo.WorkingDirectory = Ruta;
                    else if (Directory.Exists(Ruta)) Proceso.StartInfo.WorkingDirectory = Ruta;
                    bool Resultado;
                    try { Resultado = Proceso.Start(); }
                    catch { Resultado = false; }
                    Proceso.Close();
                    Proceso.Dispose();
                    Proceso = null;
                    return Resultado;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return false;
        }

        /// <summary>
        /// Obtains a miniature (or any size really) from any valid image, keeping it's original aspect ratio if desired.
        /// </summary>
        /// <param name="Imagen_Original">Any valid image.</param>
        /// <param name="Ancho_Miniatura">The desired width of the miniature.</param>
        /// <param name="Alto_Miniatura">The desired height of the miniature.</param>
        /// <param name="Relación_Aspecto">If true the miniature will keep the original aspect ratio.</param>
        /// <param name="Antialiasing">If true the miniature will be drawn with high interpolation, reducing the alias effect, at the cost of getting a bit blurred.</param>
        /// <param name="Alfa">If it's Indeterminate the returned image will contain alpha (transparency) only it if had it before. If it's Checked the returned image will always have alpha. Otherwise it will never have alpha.</param>
        /// <returns>Returns the miniature drawn with the specified options. On any error it will return null.</returns>
        internal static Bitmap Obtener_Imagen_Miniatura(Image Imagen_Original, int Ancho_Miniatura, int Alto_Miniatura, bool Relación_Aspecto, bool Antialiasing, CheckState Alfa)
        {
            try
            {
                if (Imagen_Original != null)
                {
                    int Ancho_Original = Imagen_Original.Width;
                    int Alto_Original = Imagen_Original.Height;
                    int Ancho = Ancho_Miniatura;
                    int Alto = Alto_Miniatura;
                    if (Relación_Aspecto) // Keep the original aspect ratio.
                    {
                        Ancho = (Alto_Miniatura * Ancho_Original) / Alto_Original;
                        Alto = (Ancho_Miniatura * Alto_Original) / Ancho_Original;
                        if (Ancho <= Ancho_Miniatura) Alto = Alto_Miniatura;
                        else if (Alto <= Alto_Miniatura) Ancho = Ancho_Miniatura;
                    }
                    if (Ancho < 1) Ancho = 1;
                    if (Alto < 1) Alto = 1;
                    Bitmap Imagen = new Bitmap(Ancho, Alto, Alfa == CheckState.Indeterminate ? (Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb) : Alfa == CheckState.Checked ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    //Pintar.Clear(Color.Black);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = !Antialiasing ? InterpolationMode.NearestNeighbor : InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho_Original, Alto_Original), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Reads all the bytes of any file at once and returns them in a new byte array.
        /// </summary>
        /// <param name="Ruta">Any valid and existing file path.</param>
        /// <returns>Returns all the bytes of a file in a byte array. Returns null on any error.</returns>
        internal static byte[] Obtener_Matriz_Bytes_Archivo(string Ruta)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    if (Lector.Length > 0L) // Overflow warning: it's not checking for too big files.
                    {
                        Lector.Seek(0L, SeekOrigin.Begin);
                        byte[] Matriz_Bytes = new byte[Lector.Length];
                        int Longitud = Lector.Read(Matriz_Bytes, 0, Matriz_Bytes.Length);
                        if (Longitud > -1)
                        {
                            if (Matriz_Bytes.Length != Longitud) Array.Resize(ref Matriz_Bytes, Longitud);
                        }
                        else Matriz_Bytes = null;
                        Lector.Close();
                        Lector.Dispose();
                        Lector = null;
                        return Matriz_Bytes;
                    }
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Obtener_Nombre_Temporal()
        {
            try
            {
                DateTime Fecha = DateTime.Now;
                string Año = Fecha.Year.ToString();
                string Mes = Fecha.Month.ToString();
                string Día = Fecha.Day.ToString();
                string Hora = Fecha.Hour.ToString();
                string Minuto = Fecha.Minute.ToString();
                string Segundo = Fecha.Second.ToString();
                string Milisegundo = Fecha.Millisecond.ToString();
                while (Año.Length < 4) Año = '0' + Año;
                while (Mes.Length < 2) Mes = '0' + Mes;
                while (Día.Length < 2) Día = '0' + Día;
                while (Hora.Length < 2) Hora = '0' + Hora;
                while (Minuto.Length < 2) Minuto = '0' + Minuto;
                while (Segundo.Length < 2) Segundo = '0' + Segundo;
                while (Milisegundo.Length < 3) Milisegundo = '0' + Milisegundo;
                return Año + "_" + Mes + "_" + Día + "_" + Hora + "_" + Minuto + "_" + Segundo + "_" + Milisegundo;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return "0000_00_00_00_00_00_000";
        }

        internal static string Traducir_Fecha_Hora(DateTime Fecha)
        {
            try
            {
                if (Fecha != null && Fecha >= DateTime.MinValue && Fecha <= DateTime.MaxValue)
                {
                    string Año = Fecha.Year.ToString();
                    string Mes = Fecha.Month.ToString();
                    string Día = Fecha.Day.ToString();
                    string Hora = Fecha.Hour.ToString();
                    string Minuto = Fecha.Minute.ToString();
                    string Segundo = Fecha.Second.ToString();
                    string Milisegundo = Fecha.Millisecond.ToString();
                    while (Año.Length < 4) Año = "0" + Año;
                    while (Mes.Length < 2) Mes = "0" + Mes;
                    while (Día.Length < 2) Día = "0" + Día;
                    while (Hora.Length < 2) Hora = "0" + Hora;
                    while (Minuto.Length < 2) Minuto = "0" + Minuto;
                    while (Segundo.Length < 2) Segundo = "0" + Segundo;
                    while (Milisegundo.Length < 3) Milisegundo = "0" + Milisegundo;
                    return Día + "-" + Mes + "-" + Año + ", " + Hora + ":" + Minuto + ":" + Segundo + "." + Milisegundo;
                }
            }
            catch (Exception Excepción) { Application.OnThreadException(Excepción); }
            return "??-??-????, ??:??:??.???";
        }

        internal static string Traducir_Lista_Variables(List<string> Lista_Líneas)
        {
            try
            {
                if (Lista_Líneas != null && Lista_Líneas.Count > 0)
                {
                    string Texto = null;
                    foreach (string Línea in Lista_Líneas)
                    {
                        try { Texto += Línea + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(string[] Matriz_Líneas)
        {
            try
            {
                if (Matriz_Líneas != null && Matriz_Líneas.Length > 0)
                {
                    string Texto = null;
                    foreach (string Línea in Matriz_Líneas)
                    {
                        try { Texto += Línea + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(List<object> Lista_Objetos)
        {
            try
            {
                if (Lista_Objetos != null && Lista_Objetos.Count > 0)
                {
                    string Texto = null;
                    foreach (object Objeto in Lista_Objetos)
                    {
                        try { Texto += Objeto.ToString() + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(object[] Matriz_Objetos)
        {
            try
            {
                if (Matriz_Objetos != null && Matriz_Objetos.Length > 0)
                {
                    string Texto = null;
                    foreach (object Objeto in Matriz_Objetos)
                    {
                        try { Texto += Objeto.ToString() + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(List<byte> Lista_Objetos)
        {
            try
            {
                if (Lista_Objetos != null && Lista_Objetos.Count > 0)
                {
                    string Texto = null;
                    foreach (byte Valor in Lista_Objetos)
                    {
                        try { Texto += Valor.ToString() + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(byte[] Matriz_Objetos)
        {
            try
            {
                if (Matriz_Objetos != null && Matriz_Objetos.Length > 0)
                {
                    string Texto = null;
                    foreach (byte Valor in Matriz_Objetos)
                    {
                        try { Texto += Valor.ToString() + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(List<short> Lista_Objetos)
        {
            try
            {
                if (Lista_Objetos != null && Lista_Objetos.Count > 0)
                {
                    string Texto = null;
                    foreach (short Valor in Lista_Objetos)
                    {
                        try { Texto += Valor.ToString() + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(short[] Matriz_Objetos)
        {
            try
            {
                if (Matriz_Objetos != null && Matriz_Objetos.Length > 0)
                {
                    string Texto = null;
                    foreach (short Valor in Matriz_Objetos)
                    {
                        try { Texto += Valor.ToString() + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(List<ushort> Lista_Objetos)
        {
            try
            {
                if (Lista_Objetos != null && Lista_Objetos.Count > 0)
                {
                    string Texto = null;
                    foreach (ushort Valor in Lista_Objetos)
                    {
                        try { Texto += Valor.ToString() + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Lista_Variables(ushort[] Matriz_Objetos)
        {
            try
            {
                if (Matriz_Objetos != null && Matriz_Objetos.Length > 0)
                {
                    string Texto = null;
                    foreach (ushort Valor in Matriz_Objetos)
                    {
                        try { Texto += Valor.ToString() + ", "; }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    if (!string.IsNullOrEmpty(Texto)) return Texto.TrimEnd(", ".ToCharArray());
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Traducir_Número(sbyte Valor)
        {
            return Valor.ToString();
        }

        internal static string Traducir_Número(byte Valor)
        {
            return Valor.ToString();
        }

        internal static string Traducir_Número(short Valor)
        {
            return Valor > -1000 && Valor < 1000 ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(ushort Valor)
        {
            return Valor < 1000 ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(int Valor)
        {
            return Valor > -1000 && Valor < 1000 ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(uint Valor)
        {
            return Valor < 1000 ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(long Valor)
        {
            return Valor > -1000L && Valor < 1000L ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(ulong Valor)
        {
            return Valor < 1000UL ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(float Valor)
        {
            //if (Single.IsNegativeInfinity(Valor)) return "-?";
            //else if (Single.IsPositiveInfinity(Valor)) return "+?";
            //else if (Single.IsNaN(Valor)) return "?";
            if (float.IsInfinity(Valor) || float.IsNaN(Valor)) return "0";
            else return Valor > -1000f && Valor < 1000f ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(double Valor)
        {
            //if (Double.IsNegativeInfinity(Valor)) return "-?";
            //else if (Double.IsPositiveInfinity(Valor)) return "+?";
            //else if (Double.IsNaN(Valor)) return "?";
            if (double.IsInfinity(Valor) || double.IsNaN(Valor)) return "0";
            else return Valor > -1000d && Valor < 1000d ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(decimal Valor)
        {
            return Valor > -1000m && Valor < 1000m ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(string Texto)
        {
            Texto = Texto.Replace(Caracter_Coma_Decimal, ',').Replace(".", null);
            for (int Índice = !Texto.Contains(",") ? Texto.Length - 3 : Texto.IndexOf(',') - 3, Índice_Final = !Texto.StartsWith("-") ? 0 : 1; Índice > Índice_Final; Índice -= 3) Texto = Texto.Insert(Índice, ".");
            return Texto;
            /*Texto = Texto.Replace(Caracter_Coma_Decimal, ',');
            if (Texto.Contains(".")) Texto = Texto.Replace(".", null);
            int Índice = Texto.IndexOf(',');
            for (Índice = Índice < 0 ? Texto.Length - 3 : Índice - 3; Índice > (Texto[0] != '-' ? 0 : 1); Índice -= 3) Texto = Texto.Insert(Índice, ".");
            return Texto;*/
        }

        internal static string Traducir_Número_Decimales_Redondear(double Valor, int Decimales)
        {
            Valor = Math.Round(Valor, Decimales, MidpointRounding.AwayFromZero);
            string Texto = double.IsInfinity(Valor) || double.IsNaN(Valor) ? "0" : Valor > -1000d && Valor < 1000d ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
            if (Texto.Contains(",") == false) Texto += ',' + new string('0', Decimales);
            else
            {
                Decimales = Decimales - (Texto.Length - (Texto.IndexOf(',') + 1));
                if (Decimales > 0) Texto += new string('0', Decimales);
            }
            return Texto;
        }

        internal static string Traducir_Número_Decimales_Redondear(decimal Valor, int Decimales)
        {
            Valor = Math.Round(Valor, Decimales, MidpointRounding.AwayFromZero);
            string Texto = Valor > -1000m && Valor < 1000m ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
            if (Texto.Contains(",") == false) Texto += ',' + new string('0', Decimales);
            else
            {
                Decimales -= Texto.Length - (Texto.IndexOf(',') + 1);
                if (Decimales > 0) Texto += new string('0', Decimales);
            }
            return Texto;
        }

        internal static string Traducir_Número_Decimales(double Valor, int Decimales)
        {
            string Texto = double.IsInfinity(Valor) || double.IsNaN(Valor) ? "0" : Valor > -1000d && Valor < 1000d ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
            if (Texto.Contains(",") == false) Texto += ',' + new string('0', Decimales);
            else
            {
                Decimales = Decimales - (Texto.Length - (Texto.IndexOf(',') + 1));
                if (Decimales > 0) Texto += new string('0', Decimales);
            }
            return Texto;
        }

        internal static readonly byte[] Matriz_Potencias_Base_2 = new byte[8] { 128, 64, 32, 16, 8, 4, 2, 1 };

        /// <summary>
        /// Requiere pretraducir las comas que lo deban ser y sean puntos a comas, o se borrarán...
        /// </summary>
        internal static string Traducir_Número_Puntuación_Miles(string Texto)
        {
            Texto = Texto.Replace(".", null);
            int Índice = Texto.IndexOf(',');
            for (Índice = Índice < 0 ? Texto.Length - 3 : Índice - 3; Índice > (Texto[0] != '-' ? 0 : 1); Índice -= 3) Texto = Texto.Insert(Índice, ".");
            return Texto;
        }

        internal static readonly double[] Matriz_Divisores_Tamaños = new double[8] { 8, 1024, 1024, 1024, 1024, 1024, 1024, 1024 };

        internal static string Traducir_Tamaño_Bits(double Tamaño_Bits, int Decimales, bool Decimales_Cero)
        {
            try
            {
                int Índice_Divisor = 0;
                for (; Índice_Divisor < Matriz_Divisores_Tamaños.Length; Índice_Divisor++)
                {
                    if (Tamaño_Bits >= Matriz_Divisores_Tamaños[Índice_Divisor]) Tamaño_Bits /= Matriz_Divisores_Tamaños[Índice_Divisor];
                    else break;
                }
                string Texto = Traducir_Número_Puntuación_Miles(Math.Round(Tamaño_Bits, Decimales, MidpointRounding.AwayFromZero).ToString());
                if (Decimales_Cero)
                {
                    if (!Texto.Contains(Caracter_Coma_Decimal.ToString())) Texto += Caracter_Coma_Decimal + new string('0', Decimales);
                    else
                    {
                        Decimales = Decimales - (Texto.Length - (Texto.IndexOf(Caracter_Coma_Decimal) + 1));
                        if (Decimales > 0) Texto += new string('0', Decimales);
                    }
                }
                if (Índice_Divisor == 0) return Texto + (Tamaño_Bits == 1d ? " Bit" : " Bits");
                else if (Índice_Divisor == 1) return Texto + (Tamaño_Bits == 1d ? " Byte" : " Bytes");
                else if (Índice_Divisor == 2) return Texto + " KB";
                else if (Índice_Divisor == 3) return Texto + " MB";
                else if (Índice_Divisor == 4) return Texto + " GB";
                else if (Índice_Divisor == 5) return Texto + " TB";
                else if (Índice_Divisor == 6) return Texto + " PB";
                else return Texto + " EB";
            }
            catch { }
            return "? Bits";
        }

        internal static string Traducir_Tamaño_Bits_Segundo(long Tamaño_Bits, double Segundos, int Decimales, bool Decimales_Cero)
        {
            try
            {
                return Traducir_Tamaño_Bits(Tamaño_Bits / Segundos, Decimales, Decimales_Cero) + "/s";
            }
            catch { }
            return "? Bits/s";
        }

        internal static string Traducir_Tamaño_Bytes(long Tamaño_Bytes, int Decimales, bool Decimales_Cero)
        {
            try
            {
                decimal Valor = (decimal)Tamaño_Bytes;
                int Índice = 0;
                for (; Índice < 7; Índice++)
                {
                    if (Valor < 1024m) break;
                    else Valor = Valor / 1024m;
                }
                string Texto = Traducir_Número(Math.Round(Valor, Decimales, MidpointRounding.AwayFromZero));
                if (Decimales_Cero)
                {
                    if (!Texto.Contains(Caracter_Coma_Decimal.ToString())) Texto += ',' + new string('0', Decimales);
                    else
                    {
                        Decimales = Decimales - (Texto.Length - (Texto.IndexOf(Caracter_Coma_Decimal) + 1));
                        if (Decimales > 0) Texto += new string('0', Decimales);
                    }
                }
                if (Índice == 0) Texto += Tamaño_Bytes == 1L ? " Byte" : " Bytes";
                else if (Índice == 1) Texto += " KB";
                else if (Índice == 2) Texto += " MB";
                else if (Índice == 3) Texto += " GB";
                else if (Índice == 4) Texto += " TB";
                else if (Índice == 5) Texto += " PB";
                else if (Índice == 6) Texto += " EB";
                return Texto;
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
            return "? Bytes";
        }

        internal static string Traducir_Tamaño_Bytes_Automático(long Tamaño_Bytes, int Decimales, bool Decimales_Cero)
        {
            try
            {
                decimal Valor = (decimal)Tamaño_Bytes;
                int Índice = 0;
                for (; Índice < 7; Índice++)
                {
                    if (Valor < 1024m) break;
                    else Valor = Valor / 1024m;
                }
                string Texto = Traducir_Número(Math.Round(Valor, Decimales, MidpointRounding.AwayFromZero));
                if (Decimales_Cero)
                {
                    if (!Texto.Contains(Caracter_Coma_Decimal.ToString())) Texto += ',' + new string('0', Decimales);
                    else
                    {
                        Decimales = Decimales - (Texto.Length - (Texto.IndexOf(Caracter_Coma_Decimal) + 1));
                        if (Decimales > 0) Texto += new string('0', Decimales);
                    }
                }
                if (Índice == 0) Texto += Tamaño_Bytes == 1L ? " Byte" : " Bytes";
                else if (Índice == 1) Texto += " KB";
                else if (Índice == 2) Texto += " MB";
                else if (Índice == 3) Texto += " GB";
                else if (Índice == 4) Texto += " TB";
                else if (Índice == 5) Texto += " PB";
                else if (Índice == 6) Texto += " EB";
                return Texto;
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return "? Bytes";
        }

        internal static readonly uint[] Matriz_CRC_32 = new uint[256]
        {
            0x00000000, 0x77073096, 0xEE0E612C, 0x990951BA, 0x076DC419,
            0x706AF48F, 0xE963A535, 0x9E6495A3, 0x0EDB8832, 0x79DCB8A4,
            0xE0D5E91E, 0x97D2D988, 0x09B64C2B, 0x7EB17CBD, 0xE7B82D07,
            0x90BF1D91, 0x1DB71064, 0x6AB020F2, 0xF3B97148, 0x84BE41DE,
            0x1ADAD47D, 0x6DDDE4EB, 0xF4D4B551, 0x83D385C7, 0x136C9856,
            0x646BA8C0, 0xFD62F97A, 0x8A65C9EC, 0x14015C4F, 0x63066CD9,
            0xFA0F3D63, 0x8D080DF5, 0x3B6E20C8, 0x4C69105E, 0xD56041E4,
            0xA2677172, 0x3C03E4D1, 0x4B04D447, 0xD20D85FD, 0xA50AB56B,
            0x35B5A8FA, 0x42B2986C, 0xDBBBC9D6, 0xACBCF940, 0x32D86CE3,
            0x45DF5C75, 0xDCD60DCF, 0xABD13D59, 0x26D930AC, 0x51DE003A,
            0xC8D75180, 0xBFD06116, 0x21B4F4B5, 0x56B3C423, 0xCFBA9599,
            0xB8BDA50F, 0x2802B89E, 0x5F058808, 0xC60CD9B2, 0xB10BE924,
            0x2F6F7C87, 0x58684C11, 0xC1611DAB, 0xB6662D3D, 0x76DC4190,
            0x01DB7106, 0x98D220BC, 0xEFD5102A, 0x71B18589, 0x06B6B51F,
            0x9FBFE4A5, 0xE8B8D433, 0x7807C9A2, 0x0F00F934, 0x9609A88E,
            0xE10E9818, 0x7F6A0DBB, 0x086D3D2D, 0x91646C97, 0xE6635C01,
            0x6B6B51F4, 0x1C6C6162, 0x856530D8, 0xF262004E, 0x6C0695ED,
            0x1B01A57B, 0x8208F4C1, 0xF50FC457, 0x65B0D9C6, 0x12B7E950,
            0x8BBEB8EA, 0xFCB9887C, 0x62DD1DDF, 0x15DA2D49, 0x8CD37CF3,
            0xFBD44C65, 0x4DB26158, 0x3AB551CE, 0xA3BC0074, 0xD4BB30E2,
            0x4ADFA541, 0x3DD895D7, 0xA4D1C46D, 0xD3D6F4FB, 0x4369E96A,
            0x346ED9FC, 0xAD678846, 0xDA60B8D0, 0x44042D73, 0x33031DE5,
            0xAA0A4C5F, 0xDD0D7CC9, 0x5005713C, 0x270241AA, 0xBE0B1010,
            0xC90C2086, 0x5768B525, 0x206F85B3, 0xB966D409, 0xCE61E49F,
            0x5EDEF90E, 0x29D9C998, 0xB0D09822, 0xC7D7A8B4, 0x59B33D17,
            0x2EB40D81, 0xB7BD5C3B, 0xC0BA6CAD, 0xEDB88320, 0x9ABFB3B6,
            0x03B6E20C, 0x74B1D29A, 0xEAD54739, 0x9DD277AF, 0x04DB2615,
            0x73DC1683, 0xE3630B12, 0x94643B84, 0x0D6D6A3E, 0x7A6A5AA8,
            0xE40ECF0B, 0x9309FF9D, 0x0A00AE27, 0x7D079EB1, 0xF00F9344,
            0x8708A3D2, 0x1E01F268, 0x6906C2FE, 0xF762575D, 0x806567CB,
            0x196C3671, 0x6E6B06E7, 0xFED41B76, 0x89D32BE0, 0x10DA7A5A,
            0x67DD4ACC, 0xF9B9DF6F, 0x8EBEEFF9, 0x17B7BE43, 0x60B08ED5,
            0xD6D6A3E8, 0xA1D1937E, 0x38D8C2C4, 0x4FDFF252, 0xD1BB67F1,
            0xA6BC5767, 0x3FB506DD, 0x48B2364B, 0xD80D2BDA, 0xAF0A1B4C,
            0x36034AF6, 0x41047A60, 0xDF60EFC3, 0xA867DF55, 0x316E8EEF,
            0x4669BE79, 0xCB61B38C, 0xBC66831A, 0x256FD2A0, 0x5268E236,
            0xCC0C7795, 0xBB0B4703, 0x220216B9, 0x5505262F, 0xC5BA3BBE,
            0xB2BD0B28, 0x2BB45A92, 0x5CB36A04, 0xC2D7FFA7, 0xB5D0CF31,
            0x2CD99E8B, 0x5BDEAE1D, 0x9B64C2B0, 0xEC63F226, 0x756AA39C,
            0x026D930A, 0x9C0906A9, 0xEB0E363F, 0x72076785, 0x05005713,
            0x95BF4A82, 0xE2B87A14, 0x7BB12BAE, 0x0CB61B38, 0x92D28E9B,
            0xE5D5BE0D, 0x7CDCEFB7, 0x0BDBDF21, 0x86D3D2D4, 0xF1D4E242,
            0x68DDB3F8, 0x1FDA836E, 0x81BE16CD, 0xF6B9265B, 0x6FB077E1,
            0x18B74777, 0x88085AE6, 0xFF0F6A70, 0x66063BCA, 0x11010B5C,
            0x8F659EFF, 0xF862AE69, 0x616BFFD3, 0x166CCF45, 0xA00AE278,
            0xD70DD2EE, 0x4E048354, 0x3903B3C2, 0xA7672661, 0xD06016F7,
            0x4969474D, 0x3E6E77DB, 0xAED16A4A, 0xD9D65ADC, 0x40DF0B66,
            0x37D83BF0, 0xA9BCAE53, 0xDEBB9EC5, 0x47B2CF7F, 0x30B5FFE9,
            0xBDBDF21C, 0xCABAC28A, 0x53B39330, 0x24B4A3A6, 0xBAD03605,
            0xCDD70693, 0x54DE5729, 0x23D967BF, 0xB3667A2E, 0xC4614AB8,
            0x5D681B02, 0x2A6F2B94, 0xB40BBE37, 0xC30C8EA1, 0x5A05DF1B,
            0x2D02EF8D
        };

        /// <summary>
        /// Calcula el CRC de 32 bits de la matriz de bytes indicada.
        /// </summary>
        internal static uint Calcular_CRC32(byte[] Matriz_Bytes)
        {
            if (Matriz_Bytes == null) return 0;
            uint CRC_32_Bits = 0xFFFFFFFF;
            for (int Índice = 0; Índice < Matriz_Bytes.Length; Índice++) CRC_32_Bits = Matriz_CRC_32[(Byte)(CRC_32_Bits ^ Matriz_Bytes[Índice])] ^ (CRC_32_Bits >> 8);
            return ~CRC_32_Bits;
        }

        /// <summary>
        /// The main entry point for the "PixARK Tools" application.
        /// </summary>
        [STAThread]
        static void Main(string[] Matriz_Argumentos)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Matriz_Argumentos != null && Matriz_Argumentos.Length > 0)
            {
                /*string Argumento = Matriz_Argumentos[0];
                if (!string.IsNullOrEmpty(Argumento))
                {
                    Argumento = Argumento.ToLowerInvariant();
                    if (Argumento.Contains("/c")) // Configure
                    {
                        Ventana_Salvapantallas_Bloques.Argumento_Salvapantallas = CheckState.Indeterminate;
                        Application.Run(new Ventana_Salvapantallas_Bloques());
                        return;
                    }
                    else if (Argumento.Contains("/p")) // Preview
                    {
                        //Ventana_Salvapantallas.Argumento_Salvapantallas = true;
                        //Application.Run(new Ventana_Salvapantallas());
                        return;
                    }
                    else// if (Argumento.Contains("s")) // Screensaver
                    {
                        Ventana_Salvapantallas_Bloques.Argumento_Salvapantallas = CheckState.Checked;
                        Application.Run(new Ventana_Salvapantallas_Bloques());
                        return;
                    }
                }*/
                /*else
                {
                    Ventana_Salvapantallas.Argumento_Salvapantallas = CheckState.Checked;
                    Application.Run(new Ventana_Salvapantallas());
                    return;
                }*/
            }
            Depurador.Iniciar_Depurador();
            Matriz_Colores_Arco_Iris_16 = new Color[256];
            Matriz_Colores_Grises_256 = new Color[256];
            Matriz_Colores_Arco_Iris_256 = new Color[256];
            Matriz_Colores_Termografía_256 = new Color[256];
            for (int Índice = 0; Índice < 256; Índice++)
            {
                int Índice_Arco_Iris_16 = ((Índice % 16) * 1529) / 16;
                int Índice_Arco_Iris = (Índice * 1529) / 255;
                int Índice_Termografía = 1275 - ((Índice * 1275) / 255);
                Matriz_Colores_Arco_Iris_16[Índice] = Obtener_Color_Puro_1530(Índice_Arco_Iris_16);
                //Matriz_Colores_Arco_Iris_16[Índice] = Color.FromArgb(255, ((Índice % 16) * 16) + 15, ((Índice % 16) * 16) + 15, ((Índice % 16) * 16) + 15);
                Matriz_Colores_Grises_256[Índice] = Color.FromArgb(255, Índice, Índice, Índice);
                Matriz_Colores_Arco_Iris_256[Índice] = Obtener_Color_Puro_1530(Índice_Arco_Iris);
                Matriz_Colores_Termografía_256[Índice] = Obtener_Color_Puro_1530(Índice_Termografía);
            }
            Lista_Caracteres_Prohibidos.AddRange(Path.GetInvalidPathChars());
            Lista_Caracteres_Prohibidos.AddRange(Path.GetInvalidFileNameChars());
            try { Rendimiento_Procesador = new PerformanceCounter("Processor", "% Processor Time", "_Total", true); }
            catch { Rendimiento_Procesador = null; }
            Application.Run(new Ventana_Principal());
        }
    }
}

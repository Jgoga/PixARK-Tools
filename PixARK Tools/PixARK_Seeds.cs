using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixARK_Tools
{
    /// <summary>
    /// Class designed to hold all the disocvered information about how the seeds work in PixARK.
    /// So far it seems that the randomly generated seeds are converted to binary and if it's last
    /// bits are very similar, then the resulting world will be almost a copy of another similar seed.
    /// </summary>
    internal static class PixARK_Seeds
    {
        internal static readonly string Ruta_Mapas = Application.StartupPath + "\\Seeds";

        /// <summary>
        /// List that holds all the similar seeds found so far while testing real PixARK worlds.
        /// The first byte is a reference to the last 8 bits in each seed and the bits that are
        /// set to true (1) mean that the seeds included had the same bit on that final position.
        /// </summary>
        internal static List<KeyValuePair<byte, List<int>>> Lista_Semilas_Parecidas = new List<KeyValuePair<byte, List<int>>>(new KeyValuePair<byte, List<int>>[256]);
        /*(
            new KeyValuePair<byte, int[]>[]
            {
                new KeyValuePair<byte, int[]>(0, new int[]
                {
                    2304,
                    10240,
                    11264
                }),
                new KeyValuePair<byte, int[]>(3, new int[]
                {
                    7683,
                    22531
                }),
                new KeyValuePair<byte, int[]>(4, new int[]
                {
                    1028,
                    27652
                }),
                new KeyValuePair<byte, int[]>(5, new int[]
                {
                    6149,
                    21509
                }),
                new KeyValuePair<byte, int[]>(10, new int[]
                {
                    2314,
                    7178,
                    31242
                }),
                new KeyValuePair<byte, int[]>(11, new int[]
                {
                    2571,
                    11531,
                    25867
                }),
                new KeyValuePair<byte, int[]>(22, new int[]
                {
                    1046,
                    1302,
                    2582
                }),
                new KeyValuePair<byte, int[]>(23, new int[]
                {
                    31767,
                    32279
                }),
                new KeyValuePair<byte, int[]>(24, new int[]
                {
                    1816,
                    12312,
                    17176
                }),
                new KeyValuePair<byte, int[]>(36, new int[]
                {
                    13860,
                    27684
                }),
                new KeyValuePair<byte, int[]>(38, new int[]
                {
                    14118,
                    23078
                }),
                new KeyValuePair<byte, int[]>(39, new int[]
                {
                    1063,
                    1319,
                    1575
                }),
                new KeyValuePair<byte, int[]>(40, new int[]
                {
                    10792,
                    18728,
                    22568,
                    25128
                }),
                new KeyValuePair<byte, int[]>(42, new int[]
                {
                    1834,
                    4650,
                    25130
                }),
                new KeyValuePair<byte, int[]>(44, new int[]
                {
                    1836,
                    32044
                }),
                new KeyValuePair<byte, int[]>(46, new int[]
                {
                    17966,
                    23342
                }),
                new KeyValuePair<byte, int[]>(47, new int[]
                {
                    18991,
                    31023,
                    31791
                }),
                new KeyValuePair<byte, int[]>(48, new int[]
                {
                    1584,
                    27696
                }),
                new KeyValuePair<byte, int[]>(51, new int[]
                {
                    19763,
                    20787
                }),
                new KeyValuePair<byte, int[]>(55, new int[]
                {
                    3895,
                    25143,
                    31799
                }),
                new KeyValuePair<byte, int[]>(61, new int[]
                {
                    4669,
                    10557
                }),
                new KeyValuePair<byte, int[]>(66, new int[]
                {
                    13378,
                    32578
                }),
                new KeyValuePair<byte, int[]>(67, new int[]
                {
                    1603,
                    13123,
                    14915,
                    30787
                }),
                new KeyValuePair<byte, int[]>(68, new int[]
                {
                    3140,
                    3652,
                    28484
                }),
                new KeyValuePair<byte, int[]>(70, new int[]
                {
                    18502,
                    21062
                }),
                new KeyValuePair<byte, int[]>(71, new int[]
                {
                    9031,
                    27207,
                    31559
                }),
                new KeyValuePair<byte, int[]>(74, new int[]
                {
                    16714,
                    31562
                }),
                new KeyValuePair<byte, int[]>(75, new int[]
                {
                    10571,
                    13131,
                    27211,
                    32587
                }),
                new KeyValuePair<byte, int[]>(78, new int[]
                {
                    20558,
                    26190
                }),
                new KeyValuePair<byte, int[]>(81, new int[]
                {
                    15185,
                    19025
                }),
                new KeyValuePair<byte, int[]>(86, new int[]
                {
                    3926,
                    10582,
                    14678,
                    26454
                }),
                new KeyValuePair<byte, int[]>(88, new int[]
                {
                    29272,
                    32600
                }),
                new KeyValuePair<byte, int[]>(89, new int[]
                {
                    15449,
                    29273
                }),
                new KeyValuePair<byte, int[]>(90, new int[]
                {
                    2650,
                    17498
                }),
                new KeyValuePair<byte, int[]>(91, new int[]
                {
                    12379,
                    27739,
                    29787
                }),
                new KeyValuePair<byte, int[]>(93, new int[]
                {
                    3421,
                    14941
                }),
                new KeyValuePair<byte, int[]>(102, new int[]
                {
                    2406,
                    21862,
                    27750,
                    30566
                }),
                new KeyValuePair<byte, int[]>(103, new int[]
                {
                    17767,
                    31079
                }),
                new KeyValuePair<byte, int[]>(105, new int[]
                {
                    8297,
                    16233,
                    25705,
                    31337
                }),
                new KeyValuePair<byte, int[]>(106, new int[]
                {
                    19818,
                    28266,
                    30314
                }),
                new KeyValuePair<byte, int[]>(107, new int[]
                {
                    107,
                    26475
                }),
                new KeyValuePair<byte, int[]>(109, new int[]
                {
                    621,
                    5997,
                    26989
                }),
                new KeyValuePair<byte, int[]>(110, new int[]
                {
                    2158,
                    3182
                }),
                new KeyValuePair<byte, int[]>(114, new int[]
                {
                    1394,
                    10610,
                    28018
                }),
                new KeyValuePair<byte, int[]>(120, new int[]
                {
                    28536,
                    29816
                }),
                new KeyValuePair<byte, int[]>(124, new int[]
                {
                    4476,
                    5756,
                    15996
                }),
                new KeyValuePair<byte, int[]>(127, new int[]
                {
                    2943,
                    20863
                }),
                new KeyValuePair<byte, int[]>(129, new int[]
                {
                    1921,
                    10113
                }),
                new KeyValuePair<byte, int[]>(130, new int[]
                {
                    2434,
                    8066
                }),
                new KeyValuePair<byte, int[]>(131, new int[]
                {
                    5507,
                    7555
                }),
                new KeyValuePair<byte, int[]>(136, new int[]
                {
                    3720,
                    24712,
                    25736,
                    28808
                }),
                new KeyValuePair<byte, int[]>(139, new int[]
                {
                    3211,
                    26251
                }),
                new KeyValuePair<byte, int[]>(140, new int[]
                {
                    4492,
                    20108
                }),
                new KeyValuePair<byte, int[]>(141, new int[]
                {
                    12173,
                    24717
                }),
                new KeyValuePair<byte, int[]>(142, new int[]
                {
                    4238,
                    16014
                }),
                new KeyValuePair<byte, int[]>(143, new int[]
                {
                    13967,
                    16015,
                    18063,
                    26255
                }),
                new KeyValuePair<byte, int[]>(145, new int[]
                {
                    6801,
                    15761
                }),
                new KeyValuePair<byte, int[]>(147, new int[]
                {
                    15763,
                    16531
                }),
                new KeyValuePair<byte, int[]>(148, new int[]
                {
                    16276,
                    25748
                }),
                new KeyValuePair<byte, int[]>(149, new int[]
                {
                    16021,
                    17045
                }),
                new KeyValuePair<byte, int[]>(151, new int[]
                {
                    6551,
                    16023
                }),
                new KeyValuePair<byte, int[]>(155, new int[]
                {
                    411,
                    9883,
                    25243
                }),
                new KeyValuePair<byte, int[]>(161, new int[]
                {
                    161,
                    1953,
                    16289
                }),
                new KeyValuePair<byte, int[]>(165, new int[]
                {
                    7845,
                    31397
                }),
                new KeyValuePair<byte, int[]>(172, new int[]
                {
                    23212,
                    30636
                }),
                new KeyValuePair<byte, int[]>(174, new int[]
                {
                    22446,
                    28078
                }),
                new KeyValuePair<byte, int[]>(175, new int[]
                {
                    4783,
                    22191,
                    28335
                }),
                new KeyValuePair<byte, int[]>(177, new int[]
                {
                    1969,
                    25777,
                    28337
                }),
                new KeyValuePair<byte, int[]>(180, new int[]
                {
                    13492,
                    21940,
                    27316
                }),
                new KeyValuePair<byte, int[]>(181, new int[]
                {
                    6069,
                    24501
                }),
                new KeyValuePair<byte, int[]>(185, new int[]
                {
                    10937,
                    15801,
                    20409
                }),
                new KeyValuePair<byte, int[]>(187, new int[]
                {
                    2491,
                    16827
                }),
                new KeyValuePair<byte, int[]>(188, new int[]
                {
                    8380,
                    18364,
                    25532
                }),
                new KeyValuePair<byte, int[]>(190, new int[]
                {
                    4798,
                    7358,
                    31422
                }),
                new KeyValuePair<byte, int[]>(191, new int[]
                {
                    2239,
                    20415
                }),
                new KeyValuePair<byte, int[]>(193, new int[]
                {
                    15297,
                    32449
                }),
                new KeyValuePair<byte, int[]>(194, new int[]
                {
                    14530,
                    17858
                }),
                new KeyValuePair<byte, int[]>(195, new int[]
                {
                    1987,
                    19395
                }),
                new KeyValuePair<byte, int[]>(197, new int[]
                {
                    15813,
                    23493,
                    24517
                }),
                new KeyValuePair<byte, int[]>(198, new int[]
                {
                    14790,
                    25286
                }),
                new KeyValuePair<byte, int[]>(206, new int[]
                {
                    4558,
                    11470
                }),
                new KeyValuePair<byte, int[]>(208, new int[]
                {
                    2256,
                    3280,
                    12240,
                    29648
                }),
                new KeyValuePair<byte, int[]>(211, new int[]
                {
                    979,
                    21971,
                    22739
                }),
                new KeyValuePair<byte, int[]>(214, new int[]
                {
                    470,
                    6614,
                    21462,
                    22998,
                    32726
                }),
                new KeyValuePair<byte, int[]>(216, new int[]
                {
                    24024,
                    24280
                }),
                new KeyValuePair<byte, int[]>(218, new int[]
                {
                    13274,
                    29914
                }),
                new KeyValuePair<byte, int[]>(219, new int[]
                {
                    3547,
                    25563
                }),
                new KeyValuePair<byte, int[]>(220, new int[]
                {
                    1756,
                    2012,
                    2268,
                    10972,
                    11996
                }),
                new KeyValuePair<byte, int[]>(222, new int[]
                {
                    1502,
                    4318,
                    15070,
                    30686
                }),
                new KeyValuePair<byte, int[]>(223, new int[]
                {
                    3807,
                    4575
                }),
                new KeyValuePair<byte, int[]>(225, new int[]
                {
                    2017,
                    3041
                }),
                new KeyValuePair<byte, int[]>(226, new int[]
                {
                    2018,
                    25826
                }),
                new KeyValuePair<byte, int[]>(227, new int[]
                {
                    15587,
                    26595
                }),
                new KeyValuePair<byte, int[]>(228, new int[]
                {
                    5604,
                    24036,
                    28388
                }),
                new KeyValuePair<byte, int[]>(229, new int[]
                {
                    27365,
                    31461
                }),
                new KeyValuePair<byte, int[]>(235, new int[]
                {
                    5867,
                    6635
                }),
                new KeyValuePair<byte, int[]>(236, new int[]
                {
                    6380,
                    6892
                }),
                new KeyValuePair<byte, int[]>(237, new int[]
                {
                    11501,
                    20461,
                    24557
                }),
                new KeyValuePair<byte, int[]>(241, new int[]
                {
                    16625,
                    27377
                }),
                new KeyValuePair<byte, int[]>(243, new int[]
                {
                    5875,
                    24307
                }),
                new KeyValuePair<byte, int[]>(246, new int[]
                {
                    2294,
                    15862
                }),
                new KeyValuePair<byte, int[]>(247, new int[]
                {
                    759,
                    1271,
                    2295
                }),
                new KeyValuePair<byte, int[]>(248, new int[]
                {
                    7928,
                    9208
                }),
                new KeyValuePair<byte, int[]>(249, new int[]
                {
                    5369,
                    30201
                }),
                new KeyValuePair<byte, int[]>(253, new int[]
                {
                    9469,
                    31229
                }),
                new KeyValuePair<byte, int[]>(255, new int[]
                {
                    1535,
                    5375,
                    24575
                }),
            }
        );*/

        /// <summary>
        /// Function that generates a new C# code for the "PixARK_Seeds" class that copies to the
        /// clipboard a new "Lista_Semilas_Parecidas" list, which stores all the similar seeds found
        /// so far, but note that this function never looks at any of the actual PixARK worlds or maps,
        /// it simply gets the seed numbers, turn them into binary and looks if the last 8 bits for
        /// each seed are the same with another seed, and if so it adds those seeds to the previously
        /// mentioned list. This started as a theory, but aftyer finding hundreds of similar worlds,
        /// at this point I can say that most PixARK worlds will repeat themselves with little
        /// variations, but their main terrain shape and biomes will be almost the same in all the
        /// similar map seeds.
        /// How to quickly generate lots of PixARK seed maps with this tool: start a new PixARK local
        /// world and once the screen to select a spawn point appears, press "escape" and select
        /// return to the main menu. Once there press "F5" in this tool on it's main window, and
        /// the resulting map (if not already found at any time) will be added to the seed maps.
        /// The go back to PixARK (always without closing it and neither this program), and delete
        /// the previous world (unless you like it), then generate a new random world and start again.
        /// Finally press "Ctrl+I" on this tool to show the similar seeds found so far and then press
        /// "F5" to generate the new list of similar seeds. Of course once the code is in the
        /// clipboard this application will need to be compiled again with the updated code, so
        /// Microsoft Visual Studio will be required as well as some minor programming knowledge. Use
        /// this code at your risk. But of course once the code is in the clipboard it might also be
        /// useful like that since it will show the main similar last 8 bits converted to a byte (from
        /// 0 to 255) and inside of each of those bytes will be all the similar seeds found. So this
        /// directly might also be useful as it will be displayed as editable text.
        /// </summary>
        internal static void Buscar_Semillas_Parecidas(/*bool Copiar_Portapapeles*/)
        {
            try
            {
                string[] Matriz_Rutas = Directory.GetFiles(Ruta_Mapas, "*.png", SearchOption.TopDirectoryOnly);
                if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                {
                    PixARK_Seeds.Lista_Semilas_Parecidas.Clear(); // Reset the list each time.
                    for (int Índice_Byte = 0; Índice_Byte < 256; Índice_Byte++)
                    {
                        try
                        {
                            PixARK_Seeds.Lista_Semilas_Parecidas.Add(new KeyValuePair<byte, List<int>>((byte)Índice_Byte, new List<int>()));
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    foreach (string Ruta in Matriz_Rutas)
                    {
                        try
                        {
                            int Semilla = int.Parse(Path.GetFileNameWithoutExtension(Ruta));
                            string Texto_Bits = Convert.ToString(Semilla, 2);
                            if (Texto_Bits.Length < 8) Texto_Bits = new string('0', 8 - Texto_Bits.Length) + Texto_Bits;
                            byte Valor = Convert.ToByte(Texto_Bits.Substring(Texto_Bits.Length - 8), 2);
                            PixARK_Seeds.Lista_Semilas_Parecidas[Valor].Value.Add(Semilla);
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    for (int Índice_Byte = 0; Índice_Byte < 256; Índice_Byte++)
                    {
                        try
                        {
                            if (PixARK_Seeds.Lista_Semilas_Parecidas[Índice_Byte].Value.Count > 1)
                            {
                                PixARK_Seeds.Lista_Semilas_Parecidas[Índice_Byte].Value.Sort();
                            }
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                    Matriz_Rutas = null;
                    /*// Test to generate thumbnails of the previously generated maps. // Worked as expected.
                    string Ruta_Mapas_Jupisoft = Application.StartupPath + "\\Seeds";
                    foreach (string Ruta in Matriz_Rutas)
                    {
                        try
                        {
                            Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Indeterminate);
                            if (Imagen_Original != null)
                            {
                                // This thumbnail is exactly 16 times smaller, so 1 chunk now equals 1 pixel.
                                Bitmap Imagen_320 = Program.Obtener_Imagen_Miniatura(Imagen_Original, 320, 320, true, false, CheckState.Unchecked);
                                if (Imagen_320 != null)
                                {
                                    Program.Crear_Carpetas(Ruta_Mapas_Jupisoft);
                                    Imagen_320.Save(Ruta_Mapas_Jupisoft + "\\" + Path.GetFileNameWithoutExtension(Ruta) + ".png", ImageFormat.Png);
                                    Imagen_320.Dispose();
                                    Imagen_320 = null;
                                }
                                Imagen_Original.Dispose();
                                Imagen_Original = null;
                            }
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                    Ruta_Mapas_Jupisoft = null;*/
                    /*Dictionary<string, string> Diccionario_Números_Rutas = new Dictionary<string, string>();
                    foreach (string Ruta in Matriz_Rutas)
                    {
                        try
                        {
                            string Nombre = Path.GetFileNameWithoutExtension(Ruta);
                            string Número = null;
                            foreach (char Caracter in Nombre)
                            {
                                if (char.IsDigit(Caracter))
                                {
                                    Número += Caracter;
                                }
                            }
                            if (!string.IsNullOrEmpty(Número))
                            {
                                Número = Convert.ToString(long.Parse(Número), 2);
                                if (Número.Length < 32) Número = new string('0', 32 - Número.Length) + Número;
                                if (Número.Length > 32) Número = Número.Substring(Número.Length - 32);
                                Diccionario_Números_Rutas.Add(Número, Ruta);
                            }
                        }
                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                    if (Diccionario_Números_Rutas.Count > 0)
                    {
                        SortedDictionary<long, List<long>> Diccionario_Bits_Parecidos = new SortedDictionary<long, List<long>>();
                        foreach (KeyValuePair<string, string> Entrada in Diccionario_Números_Rutas)
                        {
                            try
                            {
                                foreach (KeyValuePair<string, string> Subentrada in Diccionario_Números_Rutas)
                                {
                                    try
                                    {
                                        if (string.Compare(Entrada.Key, Subentrada.Key, true) != 0) // Not the same number.
                                        {
                                            int Bits_Iguales = 0;
                                            char[] Matriz_Caracteres = new string('0', 8).ToCharArray();
                                            for (int Índice = 0, Índice_Caracter = Entrada.Key.Length - 8; Índice < 8; Índice++, Índice_Caracter++)
                                            {
                                                if (Entrada.Key[Índice_Caracter] == Subentrada.Key[Índice_Caracter])
                                                {
                                                    Bits_Iguales++;
                                                    Matriz_Caracteres[Índice] = '1';
                                                }
                                            }
                                            if (Bits_Iguales >= 8)
                                            {
                                                long Número_8_Bits = Convert.ToInt64(Entrada.Key.Substring(Entrada.Key.Length - 8), 2);
                                                long Número_1 = Convert.ToInt64(Entrada.Key, 2);
                                                long Número_2 = Convert.ToInt64(Subentrada.Key, 2);
                                                //string Texto_8_Bits = Entrada.Key.Substring(Entrada.Key.Length - 8); //new string(Matriz_Caracteres);
                                                // First add a new list to hold the possible new numbers.
                                                if (!Diccionario_Bits_Parecidos.ContainsKey(Número_8_Bits))
                                                {
                                                    Diccionario_Bits_Parecidos.Add(Número_8_Bits, new List<long>());
                                                }
                                                // Now try to add any not existing number to that list.
                                                if (!Diccionario_Bits_Parecidos[Número_8_Bits].Contains(Número_1))
                                                {
                                                    Diccionario_Bits_Parecidos[Número_8_Bits].Add(Número_1);
                                                }
                                                if (!Diccionario_Bits_Parecidos[Número_8_Bits].Contains(Número_2))
                                                {
                                                    Diccionario_Bits_Parecidos[Número_8_Bits].Add(Número_2);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                }
                            }
                            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                        if (Diccionario_Bits_Parecidos.Count > 0)
                        {
                            PixARK_Seeds.Lista_Semilas_Parecidas.Clear(); // Reset the list each time to fully update it.
                            string Texto = "        internal static readonly List<KeyValuePair<byte, int[]>> Lista_Semilas_Parecidas = new List<KeyValuePair<byte, int[]>>\r\n        (\r\n            new KeyValuePair<byte, int[]>[]\r\n            {\r\n";
                            foreach (KeyValuePair<long, List<long>> Entrada in Diccionario_Bits_Parecidos)
                            {
                                try
                                {
                                    List<int> Lista_Semillas = new List<int>();
                                    //KeyValuePair<long, List<long>> q = new KeyValuePair<long, List<long>>(0, 0).;
                                    Diccionario_Bits_Parecidos[Entrada.Key].Sort(); // Always sort each list.
                                    Texto += "                new KeyValuePair<byte, int[]>(" + Entrada.Key.ToString() + ", new int[]\r\n                {\r\n";
                                    for (int Índice_Número = 0; Índice_Número < Entrada.Value.Count; Índice_Número++)
                                    {
                                        try
                                        {
                                            Lista_Semillas.Add((int)Entrada.Value[Índice_Número]);
                                            Texto += "                    " + Entrada.Value[Índice_Número].ToString() + (Índice_Número < Entrada.Value.Count - 1 ? ",\r\n" : "\r\n");
                                        }
                                        catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                    }
                                    PixARK_Seeds.Lista_Semilas_Parecidas.Add(new KeyValuePair<byte, int[]>((byte)Entrada.Key, Lista_Semillas.ToArray()));
                                    Lista_Semillas = null;
                                    Texto += "                }),\r\n";
                                }
                                catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                            }
                            Texto += "            }\r\n        );";
                            if (Copiar_Portapapeles) Clipboard.SetText(Texto);
                        }
                    }*/
                }
            }
            catch (Exception Excepción) { Depurador.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }
    }
}

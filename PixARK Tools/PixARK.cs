<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixARK_Tools
{
    /// <summary>
    /// Class designed to store all the known information related to the PixARK game.
    /// </summary>
    internal static class PixARK
    {
        // The "terrain.db" file seems to have those keys inside it and around 25 MB are for the map:
        /*[Columns]
        "name";

        [Rows]
        "nx_chunks", // It seems that the full world isn't saved, only the blocks changed by the players.
        "Idx_uid",
        "nx_world", // This key has the world map inside it.
        "Idx_name",
        "nx_configure",
        "sqlite_autoindex_nx_configure_1";*/

        /// <summary>
        /// Enumeration used to identify the climates of a PixARK biome. Not related to any in game numbers.
        /// </summary>
        internal enum Climas : int
        {
            Unknown = 0,
            Sunny_Day,
            Wind,
            Sandstorm,
            Snowy,
            Rainy,
            Thunderstorm,
            Rain,
            Miasma,
        }

        /// <summary>
        /// Enumeration used to identify the difficulty of a PixARK biome. Not related to any in game numbers.
        /// </summary>
        internal enum Dificultades : int
        {
            Unknown = 0,
            Easy = 1,
            Medium = 2,
            Hard = 3
        }

        /// <summary>
        /// Enumeration used to identify the different PixARK ores. Not related to any in game numbers.
        /// </summary>
        [Flags]
        internal enum Minerales : int
        {
            /// <summary>
            /// Gives: Unknown.
            /// </summary>
            Unknown = 0,

            /// <summary>
            /// Gives: Iron.
            /// </summary>
            Iron = 1,
            /// <summary>
            /// Gives: Copper.
            /// </summary>
            Copper = 2,
            /// <summary>
            /// Gives: Gold.
            /// </summary>
            Gold = 4,
            /// <summary>
            /// Gives: Silver.
            /// </summary>
            Silver = 8,

            // These 2 sections are separated to achieve the proper sorting order later on.

            /// <summary>
            /// Gives: Thunder Magic Stone.
            /// </summary>
            Amethyst = 16,
            /// <summary>
            /// Gives: Dark Magic Stone.
            /// </summary>
            Black_agate = 32,
            /// <summary>
            /// Gives: Sulfur.
            /// </summary>
            Sulfur = 64,
            /// <summary>
            /// Gives: Coal.
            /// </summary>
            Coal = 128,
            /// <summary>
            /// Gives: Wind Magic Stone.
            /// </summary>
            Emerald = 256,
            /// <summary>
            /// Gives: Flint.
            /// </summary>
            Flint = 512,
            /// <summary>
            /// Gives: Quartz.
            /// </summary>
            Quartz = 1024,
            /// <summary>
            /// Gives: Fire Magic Stone.
            /// </summary>
            Ruby = 2048,
            /// <summary>
            /// Gives: Sharp Crystal.
            /// </summary>
            Sharp_Crystal = 4096,
            /// <summary>
            /// Gives: Water Magic Stone.
            /// </summary>
            Topaz = 8192,
            /// <summary>
            /// Gives: Light Magic Stone.
            /// </summary>
            White_jade = 16384,
            /// <summary>
            /// Gives: Earth Magic Stone.
            /// </summary>
            Yellow_amber = 32768,

            Sand = 65536,
            Clay = 131072,

            // All the existing blocks, copied from the blocks "creative menu".
            /*Clay,
            Grass_cube,
            Marble,
            Soul_sand,
            Ice_crystal,
            Sea_crystal,
            Amethyst_cube, // Thunder_magic_stone.
            Black_agate_cube, // Dark_magic_stone.
            Emerald_cube, // Wind_magic_stone.
            Ruby_cube, // Fire_magic_stone.
            Topaz, // Water_magic_stone.
            White_jade_cube, // Light_magic_stone.
            Yellow_amber_cube, // Earth_magic_stone.
            Brenstone_cube, // Sulphur.
            Coal_cube, // Coal.
            Quartz_cube, // Quartz.
            Spinel_cube, // Sharp_crystal.
            Coral_rock,
            Cursed_soil,
            Dirt,
            Flash_rock,
            Ice,
            Magic_dirt,
            Magic_grass_Cube,
            Sand, // Turns into clay with water.
            Sandstone,
            Scorched_earth,
            Snow,
            Rock,
            Cursed_grass_block,
            Flash_grass_block,
            Copper_cube,
            Gold_cube,
            Iron_ore_cube,
            Silver_ore_cube,
            Volcanic_rock_cube,
            Gravel_cube, // Flint.
            Water_cube, // Liquid.
            Lava_cube, // Liquid.
            Oil, // Liquid.*/
        }

        /// <summary>
        /// Structure that holds up the current position of a Minecraft player.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Biomas
        {
            /// <summary>
            /// The biome byte index as used in game by PixARK.
            /// </summary>
            internal byte Índice;
            /// <summary>
            /// The biome name as shown in game by PixARK.
            /// </summary>
            internal string Nombre;
            /// <summary>
            /// The biome name as shown in game by PixARK. It overrides the ore biome names and shows instead the main biome where those are located.
            /// </summary>
            internal string Nombre_Simple;
            /// <summary>
            /// The biome difficulty as shown in game by PixARK.
            /// </summary>
            internal Dificultades Dificultad;
            /// <summary>
            /// The known ores that might spawn at least in the ore biomes. This doesn't include ores for the "regular" biomes.
            /// </summary>
            internal Minerales Minerales;
            /// <summary>
            /// The biome color, very close to the one shown in game by PixARK. It contains custom colors for the ore biomes, not shown in game.
            /// </summary>
            internal Color Color;
            /// <summary>
            /// The biome color, very close to the one shown in game by PixARK. It overrides the ore biome colors to hide them in the map, and also uses the same color for deep oceans and rivers.
            /// </summary>
            internal Color Color_Simple;
            /// <summary>
            /// If it's false this biome is a big regular biome, if it's true it's a small biome with several ores on it's surface.
            /// </summary>
            internal bool Bioma_Minerales;

            internal Biomas(byte Índice, string Nombre, string Nombre_Simple, Dificultades Dificultad, Minerales Minerales, Color Color, Color Color_Simple, bool Bioma_Minerales)
            {
                this.Índice = Índice;
                this.Nombre = Nombre;
                this.Nombre_Simple = Nombre_Simple;
                this.Dificultad = Dificultad;
                this.Minerales = Minerales;
                this.Color = Color;
                this.Color_Simple = Color_Simple;
                this.Bioma_Minerales = Bioma_Minerales;
            }

            /// <summary>
            /// Array that stores all the known PixARK biomes with all of it's features. Sorted by biome index.
            /// </summary>
            internal static readonly Biomas[] Matriz_Biomas = new Biomas[]
            {
                /*new Biomas(10, "Novice Grassland", "Novice Grassland", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 127, 233, 54), Color.FromArgb(255, 127, 233, 54), false),
                new Biomas(2, "Grassland", "Grassland", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 76, 201, 48), Color.FromArgb(255, 76, 201, 48), false),
                new Biomas(5, "Swamp", "Swamp", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 45, 152, 38), Color.FromArgb(255, 45, 152, 38), false),
                new Biomas(8, "Mountain Forest", "Mountain Forest", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 30, 105, 46), Color.FromArgb(255, 30, 105, 46), false),
                new Biomas(13, "River", "Deep Ocean", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 200, 252, 255), Color.FromArgb(255, 0, 252, 255), false),

                new Biomas(0, "Deep Ocean", "Deep Ocean", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 0, 252, 255), Color.FromArgb(255, 0, 252, 255), false),
                new Biomas(1, "Desert", "Desert", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 255, 255, 97), Color.FromArgb(255, 255, 255, 97), false),
                new Biomas(4, "Magic Forest", "Magic Forest", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 115, 115, 255), Color.FromArgb(255, 115, 115, 255), false),
                new Biomas(7, "Golden Realm", "Golden Realm", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 164, 128, 35), Color.FromArgb(255, 164, 128, 35), false),

                new Biomas(6, "Dark Forest", "Dark Forest", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 66, 91, 96), Color.FromArgb(255, 66, 91, 96), false),
                new Biomas(9, "Frozen Land", "Frozen Land", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 181, 255, 222), Color.FromArgb(255, 181, 255, 222), false),
                new Biomas(3, "Doom Lands", "Doom Lands", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 118, 101, 73), Color.FromArgb(255, 118, 101, 73), false),

                new Biomas(11, "Normal Island", "Deep Ocean", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 200, 176, 0), Color.FromArgb(255, 0, 252, 255), true),
                new Biomas(14, "Plateau Iron Pit", "Golden Realm", Dificultades.Medium, Minerales.Iron | Minerales.Yellow_amber, Color.FromArgb(255, 255, 255, 0), Color.FromArgb(255, 164, 128, 35), true),
                new Biomas(15, "Plains Coal Mine", "Grassland", Dificultades.Easy, Minerales.Copper | Minerales.Coal, Color.FromArgb(255, 96, 80, 64), Color.FromArgb(255, 76, 201, 48), true),
                new Biomas(16, "Plateau Magic Pit", "Magic Forest", Dificultades.Medium, Minerales.Iron | Minerales.Emerald, Color.FromArgb(255, 0, 255, 0), Color.FromArgb(255, 115, 115, 255), true),
                new Biomas(17, "Brenstone Mine", "Swamp", Dificultades.Easy, Minerales.Brenstone | Minerales.Iron, Color.FromArgb(255, 255, 208, 0), Color.FromArgb(255, 45, 152, 38), true),
                new Biomas(18, "Plains Copper Mine", "Mountain Forest", Dificultades.Easy, Minerales.Copper | Minerales.Coal, Color.FromArgb(255, 255, 144, 0), Color.FromArgb(255, 30, 105, 46), true),
                new Biomas(20, "Plateau Iron Mine", "Frozen Land", Dificultades.Hard, Minerales.Iron | Minerales.Topaz, Color.FromArgb(255, 64, 96, 255), Color.FromArgb(255, 181, 255, 222), true),
                new Biomas(21, "Quartz Pit", "Desert", Dificultades.Medium, Minerales.Iron | Minerales.Quartz, Color.FromArgb(255, 176, 192, 176), Color.FromArgb(255, 255, 255, 97), true),
                new Biomas(22, "Magic Mine", "Dark Forest", Dificultades.Hard, Minerales.Iron | Minerales.Amethyst, Color.FromArgb(255, 255, 0, 192), Color.FromArgb(255, 66, 91, 96), true),
                new Biomas(23, "Silver Pit", "Doom Lands", Dificultades.Hard, Minerales.Silver | Minerales.Ruby, Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 118, 101, 73), true),
                new Biomas(12, "?", "?", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 32, 32, 32), Color.FromArgb(255, 32, 32, 32), true),
                new Biomas(19, "?", "?", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 224, 224, 224), Color.FromArgb(255, 224, 224, 224), true),
                */
                new Biomas(0, "Deep Ocean", "Deep Ocean", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 0, 252, 255), Color.FromArgb(255, 0, 252, 255), false),
                new Biomas(1, "Desert", "Desert", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 255, 255, 97), Color.FromArgb(255, 255, 255, 97), false),
                new Biomas(2, "Grassland", "Grassland", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 76, 201, 48), Color.FromArgb(255, 76, 201, 48), false),
                new Biomas(3, "Doom Lands", "Doom Lands", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 118, 101, 73), Color.FromArgb(255, 118, 101, 73), false),
                new Biomas(4, "Magic Forest", "Magic Forest", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 115, 115, 255), Color.FromArgb(255, 115, 115, 255), false),
                new Biomas(5, "Swamp", "Swamp", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 45, 152, 38), Color.FromArgb(255, 45, 152, 38), false),
                new Biomas(6, "Dark Forest", "Dark Forest", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 66, 91, 96), Color.FromArgb(255, 66, 91, 96), false),
                new Biomas(7, "Golden Realm", "Golden Realm", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 164, 128, 35), Color.FromArgb(255, 164, 128, 35), false),
                new Biomas(8, "Mountain Forest", "Mountain Forest", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 30, 105, 46), Color.FromArgb(255, 30, 105, 46), false),
                new Biomas(9, "Frozen Land", "Frozen Land", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 181, 255, 222), Color.FromArgb(255, 181, 255, 222), false),
                new Biomas(10, "Novice Grassland", "Novice Grassland", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 127, 233, 54), Color.FromArgb(255, 127, 233, 54), false),
                new Biomas(11, "Normal Island", "Deep Ocean", Dificultades.Easy, Minerales.Sand | Minerales.Clay, Color.FromArgb(255, 200, 176, 0), Color.FromArgb(255, 0, 252, 255), true),
                new Biomas(12, "?", "?", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 32, 32, 32), Color.FromArgb(255, 32, 32, 32), true),
                new Biomas(13, "River", "River", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 200, 252, 255), Color.FromArgb(255, 0, 252, 255), false),
                new Biomas(14, "Plateau Iron Pit", "Golden Realm", Dificultades.Medium, Minerales.Iron | Minerales.Yellow_amber, Color.FromArgb(255, 255, 255, 0), Color.FromArgb(255, 164, 128, 35), true),
                new Biomas(15, "Plains Coal Mine", "Grassland", Dificultades.Easy, Minerales.Copper | Minerales.Coal, Color.FromArgb(255, 96, 80, 64), Color.FromArgb(255, 76, 201, 48), true),
                new Biomas(16, "Plateau Magic Pit", "Magic Forest", Dificultades.Medium, Minerales.Iron | Minerales.Emerald, Color.FromArgb(255, 0, 255, 0), Color.FromArgb(255, 115, 115, 255), true),
                new Biomas(17, "Brenstone Mine", "Swamp", Dificultades.Easy, Minerales.Sulfur | Minerales.Iron, Color.FromArgb(255, 255, 208, 0), Color.FromArgb(255, 45, 152, 38), true),
                new Biomas(18, "Plains Copper Mine", "Mountain Forest", Dificultades.Easy, Minerales.Copper | Minerales.Coal, Color.FromArgb(255, 255, 144, 0), Color.FromArgb(255, 30, 105, 46), true),
                new Biomas(19, "?", "?", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 224, 224, 224), Color.FromArgb(255, 224, 224, 224), true),
                new Biomas(20, "Plateau Iron Mine", "Frozen Land", Dificultades.Hard, Minerales.Iron | Minerales.Topaz, Color.FromArgb(255, 64, 96, 255), Color.FromArgb(255, 181, 255, 222), true),
                new Biomas(21, "Quartz Pit", "Desert", Dificultades.Medium, Minerales.Iron | Minerales.Quartz, Color.FromArgb(255, 176, 192, 176), Color.FromArgb(255, 255, 255, 97), true),
                new Biomas(22, "Magic Mine", "Dark Forest", Dificultades.Hard, Minerales.Iron | Minerales.Amethyst, Color.FromArgb(255, 255, 0, 192), Color.FromArgb(255, 66, 91, 96), true),
                new Biomas(23, "Silver Pit", "Doom Lands", Dificultades.Hard, Minerales.Silver | Minerales.Ruby, Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 118, 101, 73), true),
            };
        }

        /// <summary>
        /// The extra terrain that exists outside the world border at each side in blocks.
        /// </summary>
        internal static readonly int Borde_Mundo = 512;

        /// <summary>
        /// The extra terrain that exists outside the world border at each side in blocks.
        /// </summary>
        internal static readonly int Borde_Mundo_Doble = Borde_Mundo * 2;

        /// <summary>
        /// The default world size in blocks for the X and Z dimensions (note that PixARK calls "Y" to the "Z" coordinate in the game map).
        /// </summary>
        internal static readonly int Dimensiones_Mundo = 4096;

        /// <summary>
        /// In game press "Tab" and write this line of text, then press "Enter", now you should see the whole world map.
        /// </summary>
        internal static readonly string Texto_Clave_Mundo = "nx_world";

        /// <summary>
        /// Array that stores several known path to the world saves of PixARK.
        /// Index 0: Default local path to the PixARK world saves folder.
        /// Index 1: Default server path to the PixARK world saves folder.
        /// Index 2: Alternate path to the local PixARK world saves folder.
        /// </summary>
        internal static readonly string[] Matriz_Rutas_PixARK = new string[]
        {
            //string.Compare(Environment.UserName, "Jupisoft", true) == 0 ? Application.StartupPath + "\\Saves" : null, // Application save path for world testings.
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Steam\\steamapps\\common\\PixARK\\ShooterGame\\Saved\\CubeSingles\\CubeWorld_Light", // Single player.
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Steam\\steamapps\\common\\PixARK\\ShooterGame\\Saved\\Cube_LocalNetwork\\CubeWorld_Light", // LAN.
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\ShooterGame\\Saved\\CubeServers\\CubeWorld_Light\\CubeWorld", // Server.
            Environment.GetFolderPath(Environment.SpecialFolder.Windows)[0] + ":\\Games\\PixARK\\ShooterGame\\Saved\\CubeSingles\\CubeWorld_Light", // Alternate single player.
            Environment.GetFolderPath(Environment.SpecialFolder.Windows)[0] + ":\\Games\\PixARK\\ShooterGame\\Saved\\Cube_LocalNetwork\\CubeWorld_Light", // Alternate LAN.
            // Feel free to add here any other valid path you known of to extend future support.
        };

        /*/// <summary>
        /// Array that stores all the known biome names as well as the biome colors. Access by the biome index.
        /// </summary>
        internal static readonly KeyValuePair<string, Color>[] Matriz_Biomas_Nombres_Colores_Simples = new KeyValuePair<string, Color>[]
        {
            new KeyValuePair<string, Color>("Deep Ocean (Medium)", Color.FromArgb(255, 0, 252, 255)), // 0.
            new KeyValuePair<string, Color>("Desert (Medium)", Color.FromArgb(255, 255, 255, 97)), // 1.
            new KeyValuePair<string, Color>("Grassland (Easy)", Color.FromArgb(255, 76, 201, 48)), // 2.
            new KeyValuePair<string, Color>("Doom Lands (Hard)", Color.FromArgb(255, 118, 101, 73)), // 3.
            new KeyValuePair<string, Color>("Magic Forest (Medium)", Color.FromArgb(255, 115, 115, 255)), // 4.
            new KeyValuePair<string, Color>("Swamp (Easy)", Color.FromArgb(255, 45, 152, 38)), // 5.
            new KeyValuePair<string, Color>("Dark Forest (Hard)", Color.FromArgb(255, 66, 91, 96)), // 6.
            new KeyValuePair<string, Color>("Golden Realm (Medium)", Color.FromArgb(255, 164, 128, 35)), // 7.
            new KeyValuePair<string, Color>("Mountain Forest (Easy)", Color.FromArgb(255, 30, 105, 46)), // 8.
            new KeyValuePair<string, Color>("Frozen Land (Hard)", Color.FromArgb(255, 181, 255, 222)), // 9.
            new KeyValuePair<string, Color>("Novice Grassland (Easy)", Color.FromArgb(255, 127, 233, 54)), // 10.
            new KeyValuePair<string, Color>("Deep Ocean (Medium)", Color.FromArgb(255, 0, 252, 255)), // 11.
            new KeyValuePair<string, Color>("? (?)", Color.FromArgb(255, 0, 0, 0)), // 12, I've never found this biome yet.
            new KeyValuePair<string, Color>("Deep Ocean (Medium)", Color.FromArgb(255, 0, 252, 255)), // 13.
            new KeyValuePair<string, Color>("Golden Realm (Medium)", Color.FromArgb(255, 164, 128, 35)), // 14, Golden realm ores.
            new KeyValuePair<string, Color>("Grassland (Easy)", Color.FromArgb(255, 76, 201, 48)), // 15, Grassland ores.
            new KeyValuePair<string, Color>("Magic Forest (Medium)", Color.FromArgb(255, 115, 115, 255)), // 16, Magic forest ores.
            new KeyValuePair<string, Color>("Swamp (Easy)", Color.FromArgb(255, 45, 152, 38)), // 17, Swamp ores.
            new KeyValuePair<string, Color>("Mountain Forest (Easy)", Color.FromArgb(255, 30, 105, 46)), // 18, Mountain forest ores.
            new KeyValuePair<string, Color>("? (?)", Color.FromArgb(255, 255, 255, 255)), // 19, I've never found this biome yet.
            new KeyValuePair<string, Color>("Frozen Land (Hard)", Color.FromArgb(255, 181, 255, 222)), // 20, Frozen land ores.
            new KeyValuePair<string, Color>("Desert (Medium)", Color.FromArgb(255, 255, 255, 97)), // 21, Desert ores.
            new KeyValuePair<string, Color>("Dark Forest (Hard)", Color.FromArgb(255, 66, 91, 96)), // 22, Dark forest ores.
            new KeyValuePair<string, Color>("Doom Lands (Hard)", Color.FromArgb(255, 118, 101, 73)), // 23, Doom lands ores.
        };

        /// <summary>
        /// Array that stores all the known biome names as well as the biome colors. Access by the biome index.
        /// </summary>
        internal static readonly KeyValuePair<string, Color>[] Matriz_Biomas_Nombres_Colores_Minerales = new KeyValuePair<string, Color>[]
        {
            new KeyValuePair<string, Color>("Deep Ocean (Medium)", Color.FromArgb(255, 0, 252, 255)), // 0.
            new KeyValuePair<string, Color>("Desert (Medium)", Color.FromArgb(255, 255, 255, 97)), // 1.
            new KeyValuePair<string, Color>("Grassland (Easy)", Color.FromArgb(255, 76, 201, 48)), // 2.
            new KeyValuePair<string, Color>("Doom Lands (Hard)", Color.FromArgb(255, 118, 101, 73)), // 3.
            new KeyValuePair<string, Color>("Magic Forest (Medium)", Color.FromArgb(255, 115, 115, 255)), // 4.
            new KeyValuePair<string, Color>("Swamp (Easy)", Color.FromArgb(255, 45, 152, 38)), // 5.
            new KeyValuePair<string, Color>("Dark Forest (Hard)", Color.FromArgb(255, 66, 91, 96)), // 6.
            new KeyValuePair<string, Color>("Golden Realm (Medium)", Color.FromArgb(255, 164, 128, 35)), // 7.
            new KeyValuePair<string, Color>("Mountain Forest (Easy)", Color.FromArgb(255, 30, 105, 46)), // 8.
            new KeyValuePair<string, Color>("Frozen Land (Hard)", Color.FromArgb(255, 181, 255, 222)), // 9.
            new KeyValuePair<string, Color>("Novice Grassland (Easy)", Color.FromArgb(255, 127, 233, 54)), // 10.
            new KeyValuePair<string, Color>("Normal Island (Easy)", Color.FromArgb(255, 200, 176, 0)), // 11.
            new KeyValuePair<string, Color>("? (?)", Color.FromArgb(255, 0, 0, 0)), // 12, I've never found this biome yet.
            new KeyValuePair<string, Color>("River (Easy)", Color.FromArgb(255, 200, 252, 255)), // 13.
            new KeyValuePair<string, Color>("Plateau Iron Pit (Medium) (Yellow amber and iron ores)", Color.FromArgb(255, 255, 255, 0)), // 14, Golden realm ores.
            new KeyValuePair<string, Color>("Plains Coal Mine (Easy) (Coal and copper ores)", Color.FromArgb(255, 96, 80, 64)), // 15, Grassland ores, Coal and copper ores.
            new KeyValuePair<string, Color>("Plateau Magic Pit (Medium) (Emerald and iron ores)", Color.FromArgb(255, 0, 255, 0)), // 16, Magic forest ores, Emerald, amethyst, ruby and iron ores.
            new KeyValuePair<string, Color>("Brenstone Mine (Easy) (Sulphur and iron ores)", Color.FromArgb(255, 255, 208, 0)), // 17, Swamp ores, Sulphur and iron ores.
            new KeyValuePair<string, Color>("Plains Copper Mine (Easy) (Coal and copper ores)", Color.FromArgb(255, 255, 144, 0)), // 18, Mountain forest ores, Coal and copper ores.
            new KeyValuePair<string, Color>("? (?) (?)", Color.FromArgb(255, 255, 255, 255)), // 19, I've never found this biome yet.
            new KeyValuePair<string, Color>("Plateau Iron Mine (Hard) (Topaz and iron ores)", Color.FromArgb(255, 64, 96, 255)), // 20, Frozen land ores, Topaz and iron ores.
            new KeyValuePair<string, Color>("Quartz Pit (Medium) (Quartz and iron ores)", Color.FromArgb(255, 176, 192, 176)), // 21, Desert ores, Quartz and iron ores.
            new KeyValuePair<string, Color>("Magic Mine (Hard) (Amethyst and iron ores)", Color.FromArgb(255, 255, 0, 192)), // 22, Dark forest ores, Amethyst, iron and thunder magic stone ores.
            new KeyValuePair<string, Color>("Silver Pit (Hard) (Ruby and silver ores)", Color.FromArgb(255, 255, 0, 0)), // 23, Doom lands ores.
        };*/
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixARK_Tools
{
    /// <summary>
    /// Class designed to store all the known information related to the PixARK game.
    /// </summary>
    internal static class PixARK
    {
        // The "terrain.db" file seems to have those keys inside it and around 25 MB are for the map:
        /*[Columns]
        "name";

        [Rows]
        "nx_chunks", // It seems that the full world isn't saved, only the blocks changed by the players.
        "Idx_uid",
        "nx_world", // This key has the world map inside it.
        "Idx_name",
        "nx_configure",
        "sqlite_autoindex_nx_configure_1";*/

        /// <summary>
        /// Enumeration used to identify the climates of a PixARK biome. Not related to any in game numbers.
        /// </summary>
        internal enum Climas : int
        {
            Unknown = 0,
            Sunny_Day,
            Wind,
            Sandstorm,
            Snowy,
            Rainy,
            Thunderstorm,
            Rain,
            Miasma,
        }

        /// <summary>
        /// Enumeration used to identify the difficulty of a PixARK biome. Not related to any in game numbers.
        /// </summary>
        internal enum Dificultades : int
        {
            Unknown = 0,
            Easy = 1,
            Medium = 2,
            Hard = 3
        }

        /// <summary>
        /// Enumeration used to identify the different PixARK ores. Not related to any in game numbers.
        /// </summary>
        [Flags]
        internal enum Minerales : int
        {
            /// <summary>
            /// Gives: Unknown.
            /// </summary>
            Unknown = 0,

            /// <summary>
            /// Gives: Iron.
            /// </summary>
            Iron = 1,
            /// <summary>
            /// Gives: Copper.
            /// </summary>
            Copper = 2,
            /// <summary>
            /// Gives: Gold.
            /// </summary>
            Gold = 4,
            /// <summary>
            /// Gives: Silver.
            /// </summary>
            Silver = 8,

            // These 2 sections are separated to achieve the proper sorting order later on.

            /// <summary>
            /// Gives: Thunder Magic Stone.
            /// </summary>
            Amethyst = 16,
            /// <summary>
            /// Gives: Dark Magic Stone.
            /// </summary>
            Black_agate = 32,
            /// <summary>
            /// Gives: Sulfur.
            /// </summary>
            Sulfur = 64,
            /// <summary>
            /// Gives: Coal.
            /// </summary>
            Coal = 128,
            /// <summary>
            /// Gives: Wind Magic Stone.
            /// </summary>
            Emerald = 256,
            /// <summary>
            /// Gives: Flint.
            /// </summary>
            Flint = 512,
            /// <summary>
            /// Gives: Quartz.
            /// </summary>
            Quartz = 1024,
            /// <summary>
            /// Gives: Fire Magic Stone.
            /// </summary>
            Ruby = 2048,
            /// <summary>
            /// Gives: Sharp Crystal.
            /// </summary>
            Sharp_Crystal = 4096,
            /// <summary>
            /// Gives: Water Magic Stone.
            /// </summary>
            Topaz = 8192,
            /// <summary>
            /// Gives: Light Magic Stone.
            /// </summary>
            White_jade = 16384,
            /// <summary>
            /// Gives: Earth Magic Stone.
            /// </summary>
            Yellow_amber = 32768,

            Sand = 65536,
            Clay = 131072,

            // All the existing blocks, copied from the blocks "creative menu".
            /*Clay,
            Grass_cube,
            Marble,
            Soul_sand,
            Ice_crystal,
            Sea_crystal,
            Amethyst_cube, // Thunder_magic_stone.
            Black_agate_cube, // Dark_magic_stone.
            Emerald_cube, // Wind_magic_stone.
            Ruby_cube, // Fire_magic_stone.
            Topaz, // Water_magic_stone.
            White_jade_cube, // Light_magic_stone.
            Yellow_amber_cube, // Earth_magic_stone.
            Brenstone_cube, // Sulphur.
            Coal_cube, // Coal.
            Quartz_cube, // Quartz.
            Spinel_cube, // Sharp_crystal.
            Coral_rock,
            Cursed_soil,
            Dirt,
            Flash_rock,
            Ice,
            Magic_dirt,
            Magic_grass_Cube,
            Sand, // Turns into clay with water.
            Sandstone,
            Scorched_earth,
            Snow,
            Rock,
            Cursed_grass_block,
            Flash_grass_block,
            Copper_cube,
            Gold_cube,
            Iron_ore_cube,
            Silver_ore_cube,
            Volcanic_rock_cube,
            Gravel_cube, // Flint.
            Water_cube, // Liquid.
            Lava_cube, // Liquid.
            Oil, // Liquid.*/
        }

        /// <summary>
        /// Structure that holds up the current position of a Minecraft player.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Biomas
        {
            /// <summary>
            /// The biome byte index as used in game by PixARK.
            /// </summary>
            internal byte Índice;
            /// <summary>
            /// The biome name as shown in game by PixARK.
            /// </summary>
            internal string Nombre;
            /// <summary>
            /// The biome name as shown in game by PixARK. It overrides the ore biome names and shows instead the main biome where those are located.
            /// </summary>
            internal string Nombre_Simple;
            /// <summary>
            /// The biome difficulty as shown in game by PixARK.
            /// </summary>
            internal Dificultades Dificultad;
            /// <summary>
            /// The known ores that might spawn at least in the ore biomes. This doesn't include ores for the "regular" biomes.
            /// </summary>
            internal Minerales Minerales;
            /// <summary>
            /// The biome color, very close to the one shown in game by PixARK. It contains custom colors for the ore biomes, not shown in game.
            /// </summary>
            internal Color Color;
            /// <summary>
            /// The biome color, very close to the one shown in game by PixARK. It overrides the ore biome colors to hide them in the map, and also uses the same color for deep oceans and rivers.
            /// </summary>
            internal Color Color_Simple;
            /// <summary>
            /// If it's false this biome is a big regular biome, if it's true it's a small biome with several ores on it's surface.
            /// </summary>
            internal bool Bioma_Minerales;

            internal Biomas(byte Índice, string Nombre, string Nombre_Simple, Dificultades Dificultad, Minerales Minerales, Color Color, Color Color_Simple, bool Bioma_Minerales)
            {
                this.Índice = Índice;
                this.Nombre = Nombre;
                this.Nombre_Simple = Nombre_Simple;
                this.Dificultad = Dificultad;
                this.Minerales = Minerales;
                this.Color = Color;
                this.Color_Simple = Color_Simple;
                this.Bioma_Minerales = Bioma_Minerales;
            }

            /// <summary>
            /// Array that stores all the known PixARK biomes with all of it's features. Sorted by biome index.
            /// </summary>
            internal static readonly Biomas[] Matriz_Biomas = new Biomas[]
            {
                /*new Biomas(10, "Novice Grassland", "Novice Grassland", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 127, 233, 54), Color.FromArgb(255, 127, 233, 54), false),
                new Biomas(2, "Grassland", "Grassland", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 76, 201, 48), Color.FromArgb(255, 76, 201, 48), false),
                new Biomas(5, "Swamp", "Swamp", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 45, 152, 38), Color.FromArgb(255, 45, 152, 38), false),
                new Biomas(8, "Mountain Forest", "Mountain Forest", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 30, 105, 46), Color.FromArgb(255, 30, 105, 46), false),
                new Biomas(13, "River", "Deep Ocean", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 200, 252, 255), Color.FromArgb(255, 0, 252, 255), false),

                new Biomas(0, "Deep Ocean", "Deep Ocean", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 0, 252, 255), Color.FromArgb(255, 0, 252, 255), false),
                new Biomas(1, "Desert", "Desert", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 255, 255, 97), Color.FromArgb(255, 255, 255, 97), false),
                new Biomas(4, "Magic Forest", "Magic Forest", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 115, 115, 255), Color.FromArgb(255, 115, 115, 255), false),
                new Biomas(7, "Golden Realm", "Golden Realm", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 164, 128, 35), Color.FromArgb(255, 164, 128, 35), false),

                new Biomas(6, "Dark Forest", "Dark Forest", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 66, 91, 96), Color.FromArgb(255, 66, 91, 96), false),
                new Biomas(9, "Frozen Land", "Frozen Land", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 181, 255, 222), Color.FromArgb(255, 181, 255, 222), false),
                new Biomas(3, "Doom Lands", "Doom Lands", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 118, 101, 73), Color.FromArgb(255, 118, 101, 73), false),

                new Biomas(11, "Normal Island", "Deep Ocean", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 200, 176, 0), Color.FromArgb(255, 0, 252, 255), true),
                new Biomas(14, "Plateau Iron Pit", "Golden Realm", Dificultades.Medium, Minerales.Iron | Minerales.Yellow_amber, Color.FromArgb(255, 255, 255, 0), Color.FromArgb(255, 164, 128, 35), true),
                new Biomas(15, "Plains Coal Mine", "Grassland", Dificultades.Easy, Minerales.Copper | Minerales.Coal, Color.FromArgb(255, 96, 80, 64), Color.FromArgb(255, 76, 201, 48), true),
                new Biomas(16, "Plateau Magic Pit", "Magic Forest", Dificultades.Medium, Minerales.Iron | Minerales.Emerald, Color.FromArgb(255, 0, 255, 0), Color.FromArgb(255, 115, 115, 255), true),
                new Biomas(17, "Brenstone Mine", "Swamp", Dificultades.Easy, Minerales.Brenstone | Minerales.Iron, Color.FromArgb(255, 255, 208, 0), Color.FromArgb(255, 45, 152, 38), true),
                new Biomas(18, "Plains Copper Mine", "Mountain Forest", Dificultades.Easy, Minerales.Copper | Minerales.Coal, Color.FromArgb(255, 255, 144, 0), Color.FromArgb(255, 30, 105, 46), true),
                new Biomas(20, "Plateau Iron Mine", "Frozen Land", Dificultades.Hard, Minerales.Iron | Minerales.Topaz, Color.FromArgb(255, 64, 96, 255), Color.FromArgb(255, 181, 255, 222), true),
                new Biomas(21, "Quartz Pit", "Desert", Dificultades.Medium, Minerales.Iron | Minerales.Quartz, Color.FromArgb(255, 176, 192, 176), Color.FromArgb(255, 255, 255, 97), true),
                new Biomas(22, "Magic Mine", "Dark Forest", Dificultades.Hard, Minerales.Iron | Minerales.Amethyst, Color.FromArgb(255, 255, 0, 192), Color.FromArgb(255, 66, 91, 96), true),
                new Biomas(23, "Silver Pit", "Doom Lands", Dificultades.Hard, Minerales.Silver | Minerales.Ruby, Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 118, 101, 73), true),
                new Biomas(12, "?", "?", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 32, 32, 32), Color.FromArgb(255, 32, 32, 32), true),
                new Biomas(19, "?", "?", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 224, 224, 224), Color.FromArgb(255, 224, 224, 224), true),
                */
                new Biomas(0, "Deep Ocean", "Deep Ocean", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 0, 252, 255), Color.FromArgb(255, 0, 252, 255), false),
                new Biomas(1, "Desert", "Desert", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 255, 255, 97), Color.FromArgb(255, 255, 255, 97), false),
                new Biomas(2, "Grassland", "Grassland", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 76, 201, 48), Color.FromArgb(255, 76, 201, 48), false),
                new Biomas(3, "Doom Lands", "Doom Lands", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 118, 101, 73), Color.FromArgb(255, 118, 101, 73), false),
                new Biomas(4, "Magic Forest", "Magic Forest", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 115, 115, 255), Color.FromArgb(255, 115, 115, 255), false),
                new Biomas(5, "Swamp", "Swamp", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 45, 152, 38), Color.FromArgb(255, 45, 152, 38), false),
                new Biomas(6, "Dark Forest", "Dark Forest", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 66, 91, 96), Color.FromArgb(255, 66, 91, 96), false),
                new Biomas(7, "Golden Realm", "Golden Realm", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 164, 128, 35), Color.FromArgb(255, 164, 128, 35), false),
                new Biomas(8, "Mountain Forest", "Mountain Forest", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 30, 105, 46), Color.FromArgb(255, 30, 105, 46), false),
                new Biomas(9, "Frozen Land", "Frozen Land", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 181, 255, 222), Color.FromArgb(255, 181, 255, 222), false),
                new Biomas(10, "Novice Grassland", "Novice Grassland", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 127, 233, 54), Color.FromArgb(255, 127, 233, 54), false),
                new Biomas(11, "Normal Island", "Deep Ocean", Dificultades.Easy, Minerales.Sand | Minerales.Clay, Color.FromArgb(255, 200, 176, 0), Color.FromArgb(255, 0, 252, 255), true),
                new Biomas(12, "?", "?", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 32, 32, 32), Color.FromArgb(255, 32, 32, 32), true),
                new Biomas(13, "River", "River", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 200, 252, 255), Color.FromArgb(255, 0, 252, 255), false),
                new Biomas(14, "Plateau Iron Pit", "Golden Realm", Dificultades.Medium, Minerales.Iron | Minerales.Yellow_amber, Color.FromArgb(255, 255, 255, 0), Color.FromArgb(255, 164, 128, 35), true),
                new Biomas(15, "Plains Coal Mine", "Grassland", Dificultades.Easy, Minerales.Copper | Minerales.Coal, Color.FromArgb(255, 96, 80, 64), Color.FromArgb(255, 76, 201, 48), true),
                new Biomas(16, "Plateau Magic Pit", "Magic Forest", Dificultades.Medium, Minerales.Iron | Minerales.Emerald, Color.FromArgb(255, 0, 255, 0), Color.FromArgb(255, 115, 115, 255), true),
                new Biomas(17, "Brenstone Mine", "Swamp", Dificultades.Easy, Minerales.Sulfur | Minerales.Iron, Color.FromArgb(255, 255, 208, 0), Color.FromArgb(255, 45, 152, 38), true),
                new Biomas(18, "Plains Copper Mine", "Mountain Forest", Dificultades.Easy, Minerales.Copper | Minerales.Coal, Color.FromArgb(255, 255, 144, 0), Color.FromArgb(255, 30, 105, 46), true),
                new Biomas(19, "?", "?", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 224, 224, 224), Color.FromArgb(255, 224, 224, 224), true),
                new Biomas(20, "Plateau Iron Mine", "Frozen Land", Dificultades.Hard, Minerales.Iron | Minerales.Topaz, Color.FromArgb(255, 64, 96, 255), Color.FromArgb(255, 181, 255, 222), true),
                new Biomas(21, "Quartz Pit", "Desert", Dificultades.Medium, Minerales.Iron | Minerales.Quartz, Color.FromArgb(255, 176, 192, 176), Color.FromArgb(255, 255, 255, 97), true),
                new Biomas(22, "Magic Mine", "Dark Forest", Dificultades.Hard, Minerales.Iron | Minerales.Amethyst, Color.FromArgb(255, 255, 0, 192), Color.FromArgb(255, 66, 91, 96), true),
                new Biomas(23, "Silver Pit", "Doom Lands", Dificultades.Hard, Minerales.Silver | Minerales.Ruby, Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 118, 101, 73), true),
            };
        }

        /// <summary>
        /// The extra terrain that exists outside the world border at each side in blocks.
        /// </summary>
        internal static readonly int Borde_Mundo = 512;

        /// <summary>
        /// The extra terrain that exists outside the world border at each side in blocks.
        /// </summary>
        internal static readonly int Borde_Mundo_Doble = Borde_Mundo * 2;

        /// <summary>
        /// The default world size in blocks for the X and Z dimensions (note that PixARK calls "Y" to the "Z" coordinate in the game map).
        /// </summary>
        internal static readonly int Dimensiones_Mundo = 4096;

        /// <summary>
        /// In game press "Tab" and write this line of text, then press "Enter", now you should see the whole world map.
        /// </summary>
        internal static readonly string Texto_Clave_Mundo = "nx_world";

        /// <summary>
        /// Array that stores several known path to the world saves of PixARK.
        /// Index 0: Default local path to the PixARK world saves folder.
        /// Index 1: Default server path to the PixARK world saves folder.
        /// Index 2: Alternate path to the local PixARK world saves folder.
        /// </summary>
        internal static readonly string[] Matriz_Rutas_PixARK = new string[]
        {
            //string.Compare(Environment.UserName, "Jupisoft", true) == 0 ? Application.StartupPath + "\\Saves" : null, // Application save path for world testings.
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Steam\\steamapps\\common\\PixARK\\ShooterGame\\Saved\\CubeSingles\\CubeWorld_Light", // Single player.
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Steam\\steamapps\\common\\PixARK\\ShooterGame\\Saved\\Cube_LocalNetwork\\CubeWorld_Light", // LAN.
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\ShooterGame\\Saved\\CubeServers\\CubeWorld_Light\\CubeWorld", // Server.
            Environment.GetFolderPath(Environment.SpecialFolder.Windows)[0] + ":\\Games\\PixARK\\ShooterGame\\Saved\\CubeSingles\\CubeWorld_Light", // Alternate single player.
            Environment.GetFolderPath(Environment.SpecialFolder.Windows)[0] + ":\\Games\\PixARK\\ShooterGame\\Saved\\Cube_LocalNetwork\\CubeWorld_Light", // Alternate LAN.
            // Feel free to add here any other valid path you known of to extend future support.
        };

        /*/// <summary>
        /// Array that stores all the known biome names as well as the biome colors. Access by the biome index.
        /// </summary>
        internal static readonly KeyValuePair<string, Color>[] Matriz_Biomas_Nombres_Colores_Simples = new KeyValuePair<string, Color>[]
        {
            new KeyValuePair<string, Color>("Deep Ocean (Medium)", Color.FromArgb(255, 0, 252, 255)), // 0.
            new KeyValuePair<string, Color>("Desert (Medium)", Color.FromArgb(255, 255, 255, 97)), // 1.
            new KeyValuePair<string, Color>("Grassland (Easy)", Color.FromArgb(255, 76, 201, 48)), // 2.
            new KeyValuePair<string, Color>("Doom Lands (Hard)", Color.FromArgb(255, 118, 101, 73)), // 3.
            new KeyValuePair<string, Color>("Magic Forest (Medium)", Color.FromArgb(255, 115, 115, 255)), // 4.
            new KeyValuePair<string, Color>("Swamp (Easy)", Color.FromArgb(255, 45, 152, 38)), // 5.
            new KeyValuePair<string, Color>("Dark Forest (Hard)", Color.FromArgb(255, 66, 91, 96)), // 6.
            new KeyValuePair<string, Color>("Golden Realm (Medium)", Color.FromArgb(255, 164, 128, 35)), // 7.
            new KeyValuePair<string, Color>("Mountain Forest (Easy)", Color.FromArgb(255, 30, 105, 46)), // 8.
            new KeyValuePair<string, Color>("Frozen Land (Hard)", Color.FromArgb(255, 181, 255, 222)), // 9.
            new KeyValuePair<string, Color>("Novice Grassland (Easy)", Color.FromArgb(255, 127, 233, 54)), // 10.
            new KeyValuePair<string, Color>("Deep Ocean (Medium)", Color.FromArgb(255, 0, 252, 255)), // 11.
            new KeyValuePair<string, Color>("? (?)", Color.FromArgb(255, 0, 0, 0)), // 12, I've never found this biome yet.
            new KeyValuePair<string, Color>("Deep Ocean (Medium)", Color.FromArgb(255, 0, 252, 255)), // 13.
            new KeyValuePair<string, Color>("Golden Realm (Medium)", Color.FromArgb(255, 164, 128, 35)), // 14, Golden realm ores.
            new KeyValuePair<string, Color>("Grassland (Easy)", Color.FromArgb(255, 76, 201, 48)), // 15, Grassland ores.
            new KeyValuePair<string, Color>("Magic Forest (Medium)", Color.FromArgb(255, 115, 115, 255)), // 16, Magic forest ores.
            new KeyValuePair<string, Color>("Swamp (Easy)", Color.FromArgb(255, 45, 152, 38)), // 17, Swamp ores.
            new KeyValuePair<string, Color>("Mountain Forest (Easy)", Color.FromArgb(255, 30, 105, 46)), // 18, Mountain forest ores.
            new KeyValuePair<string, Color>("? (?)", Color.FromArgb(255, 255, 255, 255)), // 19, I've never found this biome yet.
            new KeyValuePair<string, Color>("Frozen Land (Hard)", Color.FromArgb(255, 181, 255, 222)), // 20, Frozen land ores.
            new KeyValuePair<string, Color>("Desert (Medium)", Color.FromArgb(255, 255, 255, 97)), // 21, Desert ores.
            new KeyValuePair<string, Color>("Dark Forest (Hard)", Color.FromArgb(255, 66, 91, 96)), // 22, Dark forest ores.
            new KeyValuePair<string, Color>("Doom Lands (Hard)", Color.FromArgb(255, 118, 101, 73)), // 23, Doom lands ores.
        };

        /// <summary>
        /// Array that stores all the known biome names as well as the biome colors. Access by the biome index.
        /// </summary>
        internal static readonly KeyValuePair<string, Color>[] Matriz_Biomas_Nombres_Colores_Minerales = new KeyValuePair<string, Color>[]
        {
            new KeyValuePair<string, Color>("Deep Ocean (Medium)", Color.FromArgb(255, 0, 252, 255)), // 0.
            new KeyValuePair<string, Color>("Desert (Medium)", Color.FromArgb(255, 255, 255, 97)), // 1.
            new KeyValuePair<string, Color>("Grassland (Easy)", Color.FromArgb(255, 76, 201, 48)), // 2.
            new KeyValuePair<string, Color>("Doom Lands (Hard)", Color.FromArgb(255, 118, 101, 73)), // 3.
            new KeyValuePair<string, Color>("Magic Forest (Medium)", Color.FromArgb(255, 115, 115, 255)), // 4.
            new KeyValuePair<string, Color>("Swamp (Easy)", Color.FromArgb(255, 45, 152, 38)), // 5.
            new KeyValuePair<string, Color>("Dark Forest (Hard)", Color.FromArgb(255, 66, 91, 96)), // 6.
            new KeyValuePair<string, Color>("Golden Realm (Medium)", Color.FromArgb(255, 164, 128, 35)), // 7.
            new KeyValuePair<string, Color>("Mountain Forest (Easy)", Color.FromArgb(255, 30, 105, 46)), // 8.
            new KeyValuePair<string, Color>("Frozen Land (Hard)", Color.FromArgb(255, 181, 255, 222)), // 9.
            new KeyValuePair<string, Color>("Novice Grassland (Easy)", Color.FromArgb(255, 127, 233, 54)), // 10.
            new KeyValuePair<string, Color>("Normal Island (Easy)", Color.FromArgb(255, 200, 176, 0)), // 11.
            new KeyValuePair<string, Color>("? (?)", Color.FromArgb(255, 0, 0, 0)), // 12, I've never found this biome yet.
            new KeyValuePair<string, Color>("River (Easy)", Color.FromArgb(255, 200, 252, 255)), // 13.
            new KeyValuePair<string, Color>("Plateau Iron Pit (Medium) (Yellow amber and iron ores)", Color.FromArgb(255, 255, 255, 0)), // 14, Golden realm ores.
            new KeyValuePair<string, Color>("Plains Coal Mine (Easy) (Coal and copper ores)", Color.FromArgb(255, 96, 80, 64)), // 15, Grassland ores, Coal and copper ores.
            new KeyValuePair<string, Color>("Plateau Magic Pit (Medium) (Emerald and iron ores)", Color.FromArgb(255, 0, 255, 0)), // 16, Magic forest ores, Emerald, amethyst, ruby and iron ores.
            new KeyValuePair<string, Color>("Brenstone Mine (Easy) (Sulphur and iron ores)", Color.FromArgb(255, 255, 208, 0)), // 17, Swamp ores, Sulphur and iron ores.
            new KeyValuePair<string, Color>("Plains Copper Mine (Easy) (Coal and copper ores)", Color.FromArgb(255, 255, 144, 0)), // 18, Mountain forest ores, Coal and copper ores.
            new KeyValuePair<string, Color>("? (?) (?)", Color.FromArgb(255, 255, 255, 255)), // 19, I've never found this biome yet.
            new KeyValuePair<string, Color>("Plateau Iron Mine (Hard) (Topaz and iron ores)", Color.FromArgb(255, 64, 96, 255)), // 20, Frozen land ores, Topaz and iron ores.
            new KeyValuePair<string, Color>("Quartz Pit (Medium) (Quartz and iron ores)", Color.FromArgb(255, 176, 192, 176)), // 21, Desert ores, Quartz and iron ores.
            new KeyValuePair<string, Color>("Magic Mine (Hard) (Amethyst and iron ores)", Color.FromArgb(255, 255, 0, 192)), // 22, Dark forest ores, Amethyst, iron and thunder magic stone ores.
            new KeyValuePair<string, Color>("Silver Pit (Hard) (Ruby and silver ores)", Color.FromArgb(255, 255, 0, 0)), // 23, Doom lands ores.
        };*/
    }
}
>>>>>>> df48069b781d9a0232ba911bbc0a68b857f30765

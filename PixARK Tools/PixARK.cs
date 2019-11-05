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
        /// Might need to be changed to "long" if the game is updated with new resource block types.
        /// </summary>
        [Flags]
        internal enum Minerales : int
        {
            /// <summary>
            /// Gives: Unknown.
            /// </summary>
            Unknown = 0,

            /// <summary>
            /// Gives: Iron Ore.
            /// </summary>
            Iron = 1,
            /// <summary>
            /// Gives: Copper Ore.
            /// </summary>
            Copper = 2,
            /// <summary>
            /// Gives: Gold Ore.
            /// </summary>
            Gold = 4,
            /// <summary>
            /// Gives: Silver Ore.
            /// </summary>
            Silver = 8,
            /// <summary>
            /// Gives: Cobalt Ore.
            /// </summary>
            Cobalt = 16,

            // These 2 sections are separated to achieve the proper sorting order later on.

            /// <summary>
            /// Gives: Thunder Magic Stone.
            /// </summary>
            Amethyst = 256,
            /// <summary>
            /// Gives: Dark Magic Stone.
            /// </summary>
            Black_agate = 512,
            /// <summary>
            /// Gives: Sulfur.
            /// </summary>
            Sulfur = 1024,
            /// <summary>
            /// Gives: Coal.
            /// </summary>
            Coal = 2048,
            /// <summary>
            /// Gives: Cobalt Crystal.
            /// </summary>
            Cobalt_ore = 4096,
            /// <summary>
            /// Gives: Wind Magic Stone.
            /// </summary>
            Emerald = 8192,
            /// <summary>
            /// Gives: Flint.
            /// </summary>
            Flint = 16384,
            /// <summary>
            /// Turns randomly into: Spine Fossil, Thighbone Fossil or Wing Bone Fossil.
            /// </summary>
            Fossil_stone = 32768,
            /// <summary>
            /// Gives: Quartz.
            /// </summary>
            Quartz = 65536,
            /// <summary>
            /// Gives: Fire Magic Stone.
            /// </summary>
            Ruby = 131072,
            /// <summary>
            /// Gives: Sharp Crystal.
            /// </summary>
            Sharp_Crystal = 262144,
            /// <summary>
            /// Gives: Water Magic Stone.
            /// </summary>
            Topaz = 524288,
            /// <summary>
            /// Gives: Light Magic Stone.
            /// </summary>
            White_jade = 1048576,
            /// <summary>
            /// Gives: Earth Magic Stone.
            /// </summary>
            Yellow_amber = 2097152,

            Sand = 4194304,
            Clay = 8388608,

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
            /// The order byte index as displayed in the legend of this application.
            /// </summary>
            internal byte Orden;
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

            internal Biomas(byte Índice, byte Orden, string Nombre, string Nombre_Simple, Dificultades Dificultad, Minerales Minerales, Color Color, Color Color_Simple, bool Bioma_Minerales)
            {
                this.Índice = Índice;
                this.Orden = Orden;
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
            internal static readonly Biomas[][] Matriz_Biomas_PixARK_SkyARK = new Biomas[2][]
            {
                // Index "0" for default PixARK biomes.
                new Biomas[]
                {
                    new Biomas(0, 5, "Deep Ocean", "Deep Ocean", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 0, 251, 255), Color.FromArgb(255, 0, 251, 255), false),
                    new Biomas(1, 6, "Desert", "Desert", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 255, 255, 98), Color.FromArgb(255, 255, 255, 98), false),
                    new Biomas(2, 1, "Grassland", "Grassland", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 77, 200, 46), Color.FromArgb(255, 77, 200, 46), false),
                    new Biomas(3, 11, "Doom Lands", "Doom Lands", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 117, 101, 75), Color.FromArgb(255, 117, 101, 75), false),
                    new Biomas(4, 7, "Magic Forest", "Magic Forest", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 113, 113, 255), Color.FromArgb(255, 113, 113, 255), false),
                    new Biomas(5, 2, "Swamp", "Swamp", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 46, 150, 43), Color.FromArgb(255, 46, 150, 43), false),
                    new Biomas(6, 10, "Dark Forest", "Dark Forest", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 66, 90, 95), Color.FromArgb(255, 66, 90, 95), false),
                    new Biomas(7, 8, "Golden Realm", "Golden Realm", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 162, 127, 39), Color.FromArgb(255, 162, 127, 39), false),
                    new Biomas(8, 3, "Mountain Forest", "Mountain Forest", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 34, 105, 46), Color.FromArgb(255, 34, 105, 46), false),
                    new Biomas(9, 9, "Frozen Land", "Frozen Land", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 179, 255, 221), Color.FromArgb(255, 179, 255, 221), false),
                    new Biomas(10, 0, "Novice Grassland", "Novice Grassland", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 126, 234, 53), Color.FromArgb(255, 126, 234, 53), false),
                    new Biomas(11, 15, "Normal Island", "Deep Ocean", Dificultades.Easy, Minerales.Sand | Minerales.Clay, Color.FromArgb(255, 200, 176, 0), Color.FromArgb(255, 0, 251, 255), true),
                    new Biomas(12, 22, "?", "?", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 32, 32, 32), Color.FromArgb(255, 32, 32, 32), true),
                    new Biomas(13, 4, "River", "River", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 200, 252, 255), Color.FromArgb(255, 0, 252, 255), false),
                    new Biomas(14, 18, "Plateau Iron Pit", "Golden Realm", Dificultades.Medium, Minerales.Iron | Minerales.Yellow_amber, Color.FromArgb(255, 255, 255, 0), Color.FromArgb(255, 162, 127, 39), true),
                    new Biomas(15, 12, "Plains Coal Mine", "Grassland", Dificultades.Easy, Minerales.Copper | Minerales.Coal, Color.FromArgb(255, 96, 80, 64), Color.FromArgb(255, 77, 200, 46), true),
                    new Biomas(16, 17, "Plateau Magic Pit", "Magic Forest", Dificultades.Medium, Minerales.Iron | Minerales.Emerald, Color.FromArgb(255, 0, 255, 0), Color.FromArgb(255, 113, 113, 255), true),
                    new Biomas(17, 13, "Brenstone Mine", "Swamp", Dificultades.Easy, Minerales.Sulfur | Minerales.Iron, Color.FromArgb(255, 255, 208, 0), Color.FromArgb(255, 46, 150, 43), true),
                    new Biomas(18, 14, "Plains Copper Mine", "Mountain Forest", Dificultades.Easy, Minerales.Copper | Minerales.Coal, Color.FromArgb(255, 255, 144, 0), Color.FromArgb(255, 34, 105, 46), true),
                    new Biomas(19, 23, "?", "?", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 224, 224, 224), Color.FromArgb(255, 224, 224, 224), true),
                    new Biomas(20, 19, "Plateau Iron Mine", "Frozen Land", Dificultades.Hard, Minerales.Iron | Minerales.Topaz, Color.FromArgb(255, 64, 96, 255), Color.FromArgb(255, 179, 255, 221), true),
                    new Biomas(21, 16, "Quartz Pit", "Desert", Dificultades.Medium, Minerales.Iron | Minerales.Quartz, Color.FromArgb(255, 176, 192, 176), Color.FromArgb(255, 255, 255, 98), true),
                    new Biomas(22, 20, "Magic Mine", "Dark Forest", Dificultades.Hard, Minerales.Iron | Minerales.Amethyst, Color.FromArgb(255, 255, 0, 192), Color.FromArgb(255, 66, 90, 95), true),
                    new Biomas(23, 21, "Silver Pit", "Doom Lands", Dificultades.Hard, Minerales.Silver | Minerales.Ruby, Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 117, 101, 75), true),
                },
                // Index "1" for SkyARK DLC biomes.
                new Biomas[]
                {
                    new Biomas(0, 0, "Void Lands", "Void Lands", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 156, 178, 195), Color.FromArgb(255, 195, 223, 244), false),
                    new Biomas(1, 2, "Woodland", "Woodland", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 210, 207, 146), Color.FromArgb(255, 210, 207, 146), false),
                    new Biomas(2, 8, "Blackrock Mountain", "Blackrock Mountain", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 73, 68, 82), Color.FromArgb(255, 73, 68, 82), false),
                    new Biomas(3, 6, "Crystal Island", "Crystal Island", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 216, 236, 238), Color.FromArgb(255, 216, 236, 238), false),
                    new Biomas(4, 1, "Dawn Island", "Dawn Island", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 199, 220, 78), Color.FromArgb(255, 199, 220, 78), false),
                    new Biomas(5, 3, "Glittering Ridge", "Glittering Ridge", Dificultades.Easy, Minerales.Unknown, Color.FromArgb(255, 218, 150, 95), Color.FromArgb(255, 218, 150, 95), false),
                    new Biomas(6, 5, "Mystical Valley", "Mystical Valley", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 255, 185, 162), Color.FromArgb(255, 255, 185, 162), false),
                    new Biomas(7, 4, "Twilight Woods", "Twilight Woods", Dificultades.Medium, Minerales.Unknown, Color.FromArgb(255, 111, 109, 179), Color.FromArgb(255, 111, 109, 179), false),
                    new Biomas(8, 17, "Blackrock Mountain Mine", "Blackrock Mountain", Dificultades.Hard, Minerales.Silver | Minerales.Black_agate, Color.FromArgb(255, 240, 240, 240), Color.FromArgb(255, 73, 68, 82), true),
                    new Biomas(9, 15, "Crystal Island Mine", "Crystal Island", Dificultades.Hard, Minerales.Iron | Minerales.Topaz, Color.FromArgb(255, 64, 96, 255), Color.FromArgb(255, 216, 236, 238), true),
                    new Biomas(10, 12, "Glittering Ridge Mine", "Glittering Ridge", Dificultades.Easy, Minerales.Iron | Minerales.Copper, Color.FromArgb(255, 144, 144, 144), Color.FromArgb(255, 218, 150, 95), true),
                    new Biomas(11, 14, "Mystical Valley Mine", "Mystical Valley", Dificultades.Medium, Minerales.Iron | Minerales.Sulfur, Color.FromArgb(255, 255, 208, 0), Color.FromArgb(255, 255, 185, 162), true),
                    new Biomas(12, 13, "Twilight Woods Mine", "Twilight Woods", Dificultades.Medium, Minerales.Iron | Minerales.Emerald, Color.FromArgb(255, 0, 255, 0), Color.FromArgb(255, 111, 109, 179), true),
                    new Biomas(13, 11, "Woodland Mine", "Woodland", Dificultades.Easy, Minerales.Copper | Minerales.Coal, Color.FromArgb(255, 96, 80, 64), Color.FromArgb(255, 210, 207, 146), true),
                    new Biomas(14, 7, "Cobalt Island", "Cobalt Island", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 110, 95, 70), Color.FromArgb(255, 110, 95, 70), false),
                    new Biomas(15, 16, "Cobalt Island Mine", "Cobalt Island", Dificultades.Hard, Minerales.Iron | Minerales.Cobalt, Color.FromArgb(255, 0, 0, 128), Color.FromArgb(255, 110, 95, 70), true),
                    new Biomas(16, 10, "Wasteland", "Wasteland", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 102, 141, 139), Color.FromArgb(255, 102, 141, 139), false),
                    new Biomas(17, 19, "Wasteland Mine", "Wasteland", Dificultades.Hard, Minerales.Gold | Minerales.Cobalt, Color.FromArgb(255, 192, 192, 96), Color.FromArgb(255, 102, 141, 139), true),
                    new Biomas(18, 9, "Burial Ground", "Burial Ground", Dificultades.Hard, Minerales.Unknown, Color.FromArgb(255, 200, 171, 125), Color.FromArgb(255, 200, 171, 125), false),
                    new Biomas(19, 18, "Burial Ground Mine", "Burial Ground", Dificultades.Hard, Minerales.Gold | Minerales.Fossil_stone, Color.FromArgb(255, 255, 255, 0), Color.FromArgb(255, 200, 171, 125), true),
                    /*new Biomas(byte.MaxValue, 0000, "", "", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), false),
                    new Biomas(byte.MaxValue, 0000, "", "", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), false),
                    new Biomas(byte.MaxValue, 0000, "", "", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), false),
                    new Biomas(byte.MaxValue, 0000, "", "", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), false),
                    new Biomas(byte.MaxValue, 0000, "", "", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), false),
                    new Biomas(byte.MaxValue, 0000, "", "", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), false),
                    *///new Biomas(byte.MaxValue, 0000, "", "", Dificultades.Unknown, Minerales.Unknown, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), false),
                }
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
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Steam\\steamapps\\common\\PixARK\\ShooterGame\\Saved\\CubeSingles\\SkyPiea_light", // Single player SkyARK.
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Steam\\steamapps\\common\\PixARK\\ShooterGame\\Saved\\Cube_LocalNetwork\\CubeWorld_Light", // LAN.
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Steam\\steamapps\\common\\PixARK\\ShooterGame\\Saved\\Cube_LocalNetwork\\SkyPiea_light", // LAN SkyARK.
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\ShooterGame\\Saved\\CubeServers\\CubeWorld_Light\\CubeWorld", // Server.
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\ShooterGame\\Saved\\CubeServers\\SkyPiea_light\\CubeWorld", // Server SkyARK.
            Environment.GetFolderPath(Environment.SpecialFolder.Windows)[0] + ":\\Games\\PixARK\\ShooterGame\\Saved\\CubeSingles\\CubeWorld_Light", // Alternate single player.
            Environment.GetFolderPath(Environment.SpecialFolder.Windows)[0] + ":\\Games\\PixARK\\ShooterGame\\Saved\\CubeSingles\\SkyPiea_light", // Alternate single player SkyARK.
            Environment.GetFolderPath(Environment.SpecialFolder.Windows)[0] + ":\\Games\\PixARK\\ShooterGame\\Saved\\Cube_LocalNetwork\\CubeWorld_Light", // Alternate LAN.
            Environment.GetFolderPath(Environment.SpecialFolder.Windows)[0] + ":\\Games\\PixARK\\ShooterGame\\Saved\\Cube_LocalNetwork\\SkyPiea_light", // Alternate LAN SkyARK.
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

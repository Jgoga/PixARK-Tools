<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PixARK_Tools
{
    internal static class Registro_Cambios
    {
        /// <summary>
        /// Structure that holds up all the information about a change log entry.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Cambios
        {
            /// <summary>
            /// The date at which a change in the application took place. The hour should be ignored, and the date should represent the exact day the changes were released to the public.
            /// </summary>
            internal DateTime Fecha;
            /// <summary>
            /// The version of the application at which a change took place. Minecraft itself uses strange version increment, since affter 1.9 it should have been 2.0, but instead it was 1.10, so keeping in mind this behavior, this application will be maintained as 1.0 at least until all of it's planned features are fully implemented. So please, use the dates in the change log to know which version is newer.
            /// </summary>
            internal string Versión;
            /// <summary>
            /// The lines of text that describes what changed in the application.
            /// </summary>
            internal string[] Matriz_Líneas;

            internal Cambios(DateTime Fecha, string Versión, string[] Matriz_Líneas)
            {
                this.Fecha = Fecha;
                this.Versión = Versión;
                this.Matriz_Líneas = Matriz_Líneas;
            }

            /// <summary>
            /// Array that contains a registry of the changes done in the application over time.
            /// </summary>
            internal static readonly Cambios[] Matriz_Cambios = new Cambios[]
            {
                new Cambios(new DateTime(2019, 10, 18), "1.0.0.0", new string[]
                {
                    "Currently working on a code to detect structures based on the surface differences, it might take several days to finish it and it should be very slow and useful.",
                    "Fixed a bug where couldn't save or copy the current map if any of the other maps was null or empty.",
                    "Fixed a bug where biomes map without ores was fully transparent because it's alpha value wasn't properly set to 255.",
                    "Fixed another bug where the ore types and percetages weren't counted if the map included the outside world borders.",
                }),
                new Cambios(new DateTime(2019, 10, 16), "1.0.0.0", new string[]
                {
                    "Tried to decrpyt the \"chunk_data\" values from the \"nx_chunks\" table, it seems to have 6 unknown bytes and then 256 bytes that should be height, the rest is unknown.",
                    "Added a new byte explorer that shows the first 16 KB of any file, including SQLite objects, as rainbow bytes, to quickly see equal values.",
                    "Added a new map in gray scale that shows the surface height and the existing chunks, including ruins as well as any exposed caves.",
                    "Learned that the cheat to reveal the full world map doesn't generate all the chunks in a world, it still needs to be explored manually to fully see it's ruins map.",
                    "Changed several options in the context menu, including adding several new map types.",
                }),
                new Cambios(new DateTime(2019, 10, 15), "1.0.0.0", new string[]
                {
                    "Added the class \"PixARK_Cheats\", which now contains all the known PixARK cheat codes, even to obtain all the game items.",
                    "Added a new group box full of options to generate the desired cheat codes and copy them directly to the clipboard.",
                    "Improved the context menu by removing some redundant options and added a few new ones.",
                    "Added a full SQLite 3 data base explorer to investigate as read-only the \"terrain.db\" world files and see if they have other useful information like ruins locations.",
                }),
                new Cambios(new DateTime(2019, 10, 14), "1.0.0.0", new string[]
                {
                    "Added a debug function for Jupisoft to always save a full resolution world map with the seed, useful for testing lots of worlds and remember the best ones.",
                    "Improved the biome balance calculation by always ignoring the biome 10 (Novice Grassland), since it only generates 9 small patches and not always visible.",
                    "Divided the biomes in the legend into 6 categories based on it's difficulty and the ore biomes, they are no longer sorted by it's biome indexes.",
                    "Added a new option to color in red the text of the biomes legend to quickly spot the ones missing in a world.",
                    "Also added a blue color text if a biome is equal or bigger than 8 percent of the total biomes in a world, meaning it's of an ideal size or very big.",
                    "Added a smarter drag and drop support by auto-detecting if the dropped folder has a file called \"terrain.db\" inside of it, meaning it should be a valid world.",
                    "Fixed a bug that loaded 2 times the same world after doing a drag and drop if the world folder was considered valid.",
                    "Added mouse click support also for the text labels in the biome legend.",
                    "Added on the biomes group box text the percentages of all the biomes contained in each group box added together.",
                }),
                new Cambios(new DateTime(2019, 10, 13), "1.0.0.0", new string[]
                {
                    "Changed 2 access keys for the copy options in the context menu.",
                    "Shrinked the legend picture boxes by 3 pixels on each direction to save more space and add later on more controls.",
                    "Updated the Finisar SQLite code, although it's kinda obsolete and very old, might need a replacement.",
                    "Tested several codes to replace the world biomes, and they worked but not on PixARK, only on the world terrain data base.",
                }),
                new Cambios(new DateTime(2019, 10, 12), "1.0.0.0", new string[]
                {
                    "Fixed another drag and drop bug that made the dopped world never load at the first try.",
                    "Updated the application to fully support PixARK 1.67 (DLC Skyward) worlds.",
                    "Added 2 new PixARK known world save path for LAN worlds.",
                    "Fixed a bug where refreshing the worlds list kept deleted world folders.",
                    "Now any found folder not listed in the worlds list is also included as a possibly found world.",
                }),
                new Cambios(new DateTime(2019, 10, 11), "1.0.0.0", new string[]
                {
                    "Added a new status bar label to show the percentage of surface ore biomes.",
                    "Added a lot of new context menu items with useful links a new features like a full screen mode.",
                    "Added a new option to save the world analysis text as UTF-8 in the exported maps folder.",
                    "Fixed a few bugs were the mouse cursor wasn't shown again before loading another window or message box.",
                }),
                new Cambios(new DateTime(2019, 10, 10), "1.0.0.0", new string[]
                {
                    "Added a boolean option to quickly see if a known biome is one of the small types that usually contain ores on it's surfaces or not.",
                    "Added the option to the world analysis to tell the more and less common biomes, excluding the ones that are small and usually have ores.",
                    "Improved a lot more functions in the application, and extended quite a lot the world analysis text.",
                    "Improved the drag and drop support to auto-detect as \"Guid\" the world save folder name to see if it's valid or not.",
                }),
                new Cambios(new DateTime(2019, 10, 09), "1.0.0.0", new string[]
                {
                    "Fixed a bug where it was showing iron ore instead of copper ore in the \"Plains Coal Mine\".",
                    "Avoided the double world loading by temporary changing the auto load maps option (I just dream this fix and it worked on first try).",
                    "Fixed a nother bug where it wasn't showing the world difficulty if it was mostly easy.",
                    "Improved a lot the world analysis to give really detailed information about things like water quantity, exposed ores, difficulty, etc",
                }),
                new Cambios(new DateTime(2019, 10, 08), "1.0.0.0", new string[]
                {
                    "Improved a lot the whole application, and fully redone the biome structures with a lot more information.",
                    "Now the exported full resolution world maps use the world seed as the image name.",
                    "Added a first try of a world analysis text, based on the biomes found in each world.",
                    "Added several new options in the context menu and removed a few other ones for copying and exporting, now one for each is enough.",
                    "Improved a lot the change in maps with and without ores, now it doesn't require to load the world again, since now it makes 2 maps.",
                    "Added lots of new features like highlighting unique biomes on mouse click, showing XY coordinates, difficulty icons, etc.",
                }),
                new Cambios(new DateTime(2019, 10, 07), "1.0.0.0", new string[]
                {
                    "This application was born today, with a lot of new options than the try on Minecraft Tools.",
                    "Fully finished the first stable release of the application, than can extract full world maps, even with surface ores visible.",
                    "Added tons of new options like percentage of biomes, world sorting by name, biome difficulty, world seed, etc.",
                }),
                new Cambios(new DateTime(2019, 10, 06), "1.0.0.0", new string[]
                {
                    "A bit off-topic I discovered how PixARK world saves are stored, in SQLite 3 format.",
                    "I tried to extract and geenrate the world map, but I failed for all the tries I did.",
                    "After giving up, the same day at night I had another idea, and after a few hours, it ended up extracting the biome world map in Minecraft Tools.",
                }),
            };
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PixARK_Tools
{
    internal static class Registro_Cambios
    {
        /// <summary>
        /// Structure that holds up all the information about a change log entry.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Cambios
        {
            /// <summary>
            /// The date at which a change in the application took place. The hour should be ignored, and the date should represent the exact day the changes were released to the public.
            /// </summary>
            internal DateTime Fecha;
            /// <summary>
            /// The version of the application at which a change took place. Minecraft itself uses strange version increment, since affter 1.9 it should have been 2.0, but instead it was 1.10, so keeping in mind this behavior, this application will be maintained as 1.0 at least until all of it's planned features are fully implemented. So please, use the dates in the change log to know which version is newer.
            /// </summary>
            internal string Versión;
            /// <summary>
            /// The lines of text that describes what changed in the application.
            /// </summary>
            internal string[] Matriz_Líneas;

            internal Cambios(DateTime Fecha, string Versión, string[] Matriz_Líneas)
            {
                this.Fecha = Fecha;
                this.Versión = Versión;
                this.Matriz_Líneas = Matriz_Líneas;
            }

            /// <summary>
            /// Array that contains a registry of the changes done in the application over time.
            /// </summary>
            internal static readonly Cambios[] Matriz_Cambios = new Cambios[]
            {
                new Cambios(new DateTime(2019, 10, 18), "1.0.0.0", new string[]
                {
                    "Currently working on a code to detect structures based on the surface differences, it might take several days to finish it and it should be very slow and useful.",
                    "Fixed a bug where couldn't save or copy the current map if any of the other maps was null or empty.",
                    "Fixed a bug where biomes map without ores was fully transparent because it's alpha value wasn't properly set to 255.",
                    "Fixed another bug where the ore types and percetages weren't counted if the map included the outside world borders.",
                }),
                new Cambios(new DateTime(2019, 10, 16), "1.0.0.0", new string[]
                {
                    "Tried to decrpyt the \"chunk_data\" values from the \"nx_chunks\" table, it seems to have 6 unknown bytes and then 256 bytes that should be height, the rest is unknown.",
                    "Added a new byte explorer that shows the first 16 KB of any file, including SQLite objects, as rainbow bytes, to quickly see equal values.",
                    "Added a new map in gray scale that shows the surface height and the existing chunks, including ruins as well as any exposed caves.",
                    "Learned that the cheat to reveal the full world map doesn't generate all the chunks in a world, it still needs to be explored manually to fully see it's ruins map.",
                    "Changed several options in the context menu, including adding several new map types.",
                }),
                new Cambios(new DateTime(2019, 10, 15), "1.0.0.0", new string[]
                {
                    "Added the class \"PixARK_Cheats\", which now contains all the known PixARK cheat codes, even to obtain all the game items.",
                    "Added a new group box full of options to generate the desired cheat codes and copy them directly to the clipboard.",
                    "Improved the context menu by removing some redundant options and added a few new ones.",
                    "Added a full SQLite 3 data base explorer to investigate as read-only the \"terrain.db\" world files and see if they have other useful information like ruins locations.",
                }),
                new Cambios(new DateTime(2019, 10, 14), "1.0.0.0", new string[]
                {
                    "Added a debug function for Jupisoft to always save a full resolution world map with the seed, useful for testing lots of worlds and remember the best ones.",
                    "Improved the biome balance calculation by always ignoring the biome 10 (Novice Grassland), since it only generates 9 small patches and not always visible.",
                    "Divided the biomes in the legend into 6 categories based on it's difficulty and the ore biomes, they are no longer sorted by it's biome indexes.",
                    "Added a new option to color in red the text of the biomes legend to quickly spot the ones missing in a world.",
                    "Also added a blue color text if a biome is equal or bigger than 8 percent of the total biomes in a world, meaning it's of an ideal size or very big.",
                    "Added a smarter drag and drop support by auto-detecting if the dropped folder has a file called \"terrain.db\" inside of it, meaning it should be a valid world.",
                    "Fixed a bug that loaded 2 times the same world after doing a drag and drop if the world folder was considered valid.",
                    "Added mouse click support also for the text labels in the biome legend.",
                    "Added on the biomes group box text the percentages of all the biomes contained in each group box added together.",
                }),
                new Cambios(new DateTime(2019, 10, 13), "1.0.0.0", new string[]
                {
                    "Changed 2 access keys for the copy options in the context menu.",
                    "Shrinked the legend picture boxes by 3 pixels on each direction to save more space and add later on more controls.",
                    "Updated the Finisar SQLite code, although it's kinda obsolete and very old, might need a replacement.",
                    "Tested several codes to replace the world biomes, and they worked but not on PixARK, only on the world terrain data base.",
                }),
                new Cambios(new DateTime(2019, 10, 12), "1.0.0.0", new string[]
                {
                    "Fixed another drag and drop bug that made the dopped world never load at the first try.",
                    "Updated the application to fully support PixARK 1.67 (DLC Skyward) worlds.",
                    "Added 2 new PixARK known world save path for LAN worlds.",
                    "Fixed a bug where refreshing the worlds list kept deleted world folders.",
                    "Now any found folder not listed in the worlds list is also included as a possibly found world.",
                }),
                new Cambios(new DateTime(2019, 10, 11), "1.0.0.0", new string[]
                {
                    "Added a new status bar label to show the percentage of surface ore biomes.",
                    "Added a lot of new context menu items with useful links a new features like a full screen mode.",
                    "Added a new option to save the world analysis text as UTF-8 in the exported maps folder.",
                    "Fixed a few bugs were the mouse cursor wasn't shown again before loading another window or message box.",
                }),
                new Cambios(new DateTime(2019, 10, 10), "1.0.0.0", new string[]
                {
                    "Added a boolean option to quickly see if a known biome is one of the small types that usually contain ores on it's surfaces or not.",
                    "Added the option to the world analysis to tell the more and less common biomes, excluding the ones that are small and usually have ores.",
                    "Improved a lot more functions in the application, and extended quite a lot the world analysis text.",
                    "Improved the drag and drop support to auto-detect as \"Guid\" the world save folder name to see if it's valid or not.",
                }),
                new Cambios(new DateTime(2019, 10, 09), "1.0.0.0", new string[]
                {
                    "Fixed a bug where it was showing iron ore instead of copper ore in the \"Plains Coal Mine\".",
                    "Avoided the double world loading by temporary changing the auto load maps option (I just dream this fix and it worked on first try).",
                    "Fixed a nother bug where it wasn't showing the world difficulty if it was mostly easy.",
                    "Improved a lot the world analysis to give really detailed information about things like water quantity, exposed ores, difficulty, etc",
                }),
                new Cambios(new DateTime(2019, 10, 08), "1.0.0.0", new string[]
                {
                    "Improved a lot the whole application, and fully redone the biome structures with a lot more information.",
                    "Now the exported full resolution world maps use the world seed as the image name.",
                    "Added a first try of a world analysis text, based on the biomes found in each world.",
                    "Added several new options in the context menu and removed a few other ones for copying and exporting, now one for each is enough.",
                    "Improved a lot the change in maps with and without ores, now it doesn't require to load the world again, since now it makes 2 maps.",
                    "Added lots of new features like highlighting unique biomes on mouse click, showing XY coordinates, difficulty icons, etc.",
                }),
                new Cambios(new DateTime(2019, 10, 07), "1.0.0.0", new string[]
                {
                    "This application was born today, with a lot of new options than the try on Minecraft Tools.",
                    "Fully finished the first stable release of the application, than can extract full world maps, even with surface ores visible.",
                    "Added tons of new options like percentage of biomes, world sorting by name, biome difficulty, world seed, etc.",
                }),
                new Cambios(new DateTime(2019, 10, 06), "1.0.0.0", new string[]
                {
                    "A bit off-topic I discovered how PixARK world saves are stored, in SQLite 3 format.",
                    "I tried to extract and geenrate the world map, but I failed for all the tries I did.",
                    "After giving up, the same day at night I had another idea, and after a few hours, it ended up extracting the biome world map in Minecraft Tools.",
                }),
            };
        }
    }
}
>>>>>>> df48069b781d9a0232ba911bbc0a68b857f30765

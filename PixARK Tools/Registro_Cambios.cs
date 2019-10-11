using System;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;
using System.Diagnostics;
using Mono.Options;

namespace BFGFontTool
{
    static class Program
    {
        public static void createBFG(string bmFontInputFileName, string bfgFontOutputFileName)
        {
            string outFileName = Path.Combine(Path.GetDirectoryName(bmFontInputFileName), "48.dat");
            if (bfgFontOutputFileName != null)
                outFileName = bfgFontOutputFileName;

            BMFont font = new BMFont();
            font.Load(bmFontInputFileName);
            font.SaveBFGFont(outFileName);
        }

        /// <summary>
        /// Decompose the Doom 3 font into individual character images.
        /// It also applies The Dark Mod's code page conversion.
        /// To ignore any conversions and use iso-8859-1 code page choose "english" language.
        /// </summary>
        /// <param name="d3FontInputFileName">Doom 3 .dat font file.</param>
        /// <param name="dirWithFontTextures">Path to directory containing font textures, like "arial_0_48.dds".</param>
        /// <param name="lang">The Dark Mod's language to use. Can be "english", "polish", "german", etc.</param>
        /// <param name="dirWithLangMaps">Optional: Directory containing The Dark Mod's code page remapping maps, like "polish.map".</param>
        /// <param name="zeroSizeImage">Optional: image that have 0x0 size.</param>
        /// <param name="bmConfigFile">File to output BMFont's external image configuration to.</param>
        /// <param name="imageOutputDir">Directory into which output character images.</param>
        public static void decomposeD3(string d3FontInputFileName, string dirWithFontTextures, string[] langs, string dirWithLangMaps, string zeroSizeImage, string bmConfigFile, string imageOutputDir)
        {
            D3Font d3Font = new D3Font();
            d3Font.Load(d3FontInputFileName);

            D3FontDecompose.Decompose(
                d3Font,
                dirWithFontTextures,
                langs,
                dirWithLangMaps,
                zeroSizeImage,
                bmConfigFile,
                imageOutputDir
                );
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new BFGFontTool());

                return;
            }

            if (args[0].ToLowerInvariant() == "create-bfg")
            {
                string bmFontInputFileName = null;
                string bfgFontOutputFileName = null;
                bool showHelp = false;
                var p = new OptionSet () {
                    { "h|help",  "show this message and exit", 
                        v => showHelp |= v != null },
                    { "bm|bmfont=", "file name of the input BMFont (i.e. bm=ArialNarrow.fnt)",
                        v => bmFontInputFileName = v },
                    { "bfg|bfgfont:",  "file name of the output BFG font (default: bfg=48.dat)",
                        (string v) => bfgFontOutputFileName = v },
                };

                try {
                    List<string> extra = p.Parse (args.Skip(1));
                    if (extra.Count > 0)
                    {
                        Console.WriteLine("Don't know what to do with those arguments: {0}", string.Join(", ", extra));
                        showHelp = true;
                    }
                }
                catch (OptionException e) {
                    Console.WriteLine (e.Message);
                    showHelp = true;
                }

                if (showHelp)
                {
                    Console.WriteLine("BFGFontTool create-bfg options...");
                    Console.WriteLine("    converts AngelCode BMFont's .fnt plain-text font descriptor into Doom 3 BFG Edition .dat font file");
                    p.WriteOptionDescriptions(Console.Out);
                    return;
                }
        
                createBFG(bmFontInputFileName, bfgFontOutputFileName);
                return;
            }

            if (args[0].ToLowerInvariant() == "decompose-d3")
            {
                string d3FontInputFileName = null;
                string dirWithFontTextures = null;
                string bmConfigFile = "myConf.bmfc";
                string imageOutputDir = null;
                string[] langs = D3FontDecompose.allLangsExceptRussian;
                string dirWithLangMaps = null;
                string zeroSizeImage = null;

                string allButRussian = string.Join(",", D3FontDecompose.allLangsExceptRussian);
                string onlyRussian = string.Join(",", D3FontDecompose.onlyRussian);

                bool showHelp = false;
                var p = new OptionSet () {
                    { "h|help",  "show this message and exit", 
                        v => showHelp |= v != null },
                    { "d3|d3font=", "file name of the input Doom 3 .dat font (i.e. d3=fontimage_48.dat)",
                        v => d3FontInputFileName = v },
                    { "d3texs=",  "directory containing font's textures (files like: arial_0_48.dds)",
                        (string v) => dirWithFontTextures = v },
                    { "bmfc|bmfconfig:", "file to which to append character descriptions (in BMFont's configuration file format) (default: myConf.bmfc)",
                        v => bmConfigFile = v },
                    { "imgout:", "directory to which to write character images (default: bmfc file's directory)",
                        v => imageOutputDir = v },
                    { "lang|language,", "comma separated list of The Dark Mod's languages to use during font's conversion (default: all except russian)\n" +
                                         "available langs: " + string.Join(",", allButRussian, onlyRussian),
                        (string v) => langs = v.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries) },
                    { "remap|remapdir:", "directory in which The Dark Mod's remap tables are (like: polish.map)",
                        v => dirWithLangMaps = v },
                };

                try {
                    List<string> extra = p.Parse(args.Skip(1));
                    if (extra.Count > 0)
                    {
                        Console.WriteLine("Don't know what to do with those arguments: {0}", string.Join(", ", extra));
                        showHelp = true;
                    }
                }
                catch (OptionException e) {
                    Console.WriteLine (e.Message);
                    showHelp = true;
                }

                if (showHelp)
                {
                    Console.WriteLine("BFGFontTool decompose-d3 options...");
                    Console.WriteLine("    decomposes Doom 3's .dat font into BMFont's character descriptions");
                    p.WriteOptionDescriptions(Console.Out);
                    return;
                }
        
                decomposeD3(d3FontInputFileName, dirWithFontTextures, langs, dirWithLangMaps, zeroSizeImage, bmConfigFile, imageOutputDir);
                return;
            }

            System.Console.WriteLine("Usage:");
            System.Console.WriteLine("    BFGFontTool create-bfg --help");
            System.Console.WriteLine("    BFGFontTool decompose-d3 --help");
        }
    }
}

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
using System.Xml.Serialization;

namespace BFGFontTool
{
    public static class Program
    {
        public static void createBFG(CreateBFGOptions options)
        {
            string outputDirectory = Path.GetDirectoryName(options.bfgFontOutputFileName);

            BMFont font = new BMFont();
            font.Load(options.bmFontInputFileName);
            font.SaveBFGFont(options.bfgFontOutputFileName);
            //font.SaveD3Fonts(outputDirectory);
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
        public static void decomposeD3(DecomposeD3Options options)
        {
            D3Font d3Font = new D3Font();
            d3Font.Load(options.d3FontInputFileName);

            if (!File.Exists(options.bmConfigFile))
            {
                File.Create(options.bmConfigFile).Dispose();
            }

            IList<string> bmIcons = D3FontDecompose.Decompose(
                d3Font,
                options.dirWithFontTextures,
                options.langs,
                options.dirWithLangMaps,
                options.zeroSizeImage,
                options.imageOutputDir
                );

            IList<string> bmConfig = File.ReadAllLines(options.bmConfigFile).ToList();

            var noIconsConfig = from line in bmConfig
                                where !line.StartsWith("icon=")
                                select line;

            var newConfig = (options.bmConfigAppend ? bmConfig : noIconsConfig).Union(bmIcons);

            File.WriteAllLines(options.bmConfigFile, newConfig);
        }

        public class ValidationException : ApplicationException
        {
            public ValidationException(string message)
                : base(message)
            {}
        }

        public class ShowHelpException : ApplicationException
        {}

        public class CreateBFGOptions
        {
            public string bmFontInputFileName = null;
            public string bfgFontOutputFileName = null;

            public void Validate()
            {
                if (string.IsNullOrWhiteSpace(bmFontInputFileName))
                    throw new ValidationException("Input BM font file name not specified.");
                if (!File.Exists(bmFontInputFileName))
                    throw new ValidationException("Input BM font file does not exist.");

                if (string.IsNullOrWhiteSpace(bfgFontOutputFileName))
                {
                    string defaultName = Path.ChangeExtension(Path.GetFileName(bmFontInputFileName), ".dat ");
                    bfgFontOutputFileName = Path.Combine(Path.GetDirectoryName(bmFontInputFileName), defaultName);
                }

                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(bfgFontOutputFileName));
                    File.Create(bfgFontOutputFileName).Dispose();
                }
                catch
                {
                    throw new ValidationException("Invalid output BFG font file name.");
                }
            }

            public void PrintHelp(TextWriter output)
            {
                output.WriteLine("BFGFontTool create-bfg options...");
                output.WriteLine("    converts AngelCode BMFont's .fnt plain-text font descriptor into Doom 3 BFG Edition .dat font file");
                GetOptions().WriteOptionDescriptions(output);
            }

            public OptionSet GetOptions()
            {
                return new OptionSet () {
                    { "h|help",  "show this message and exit", 
                        v => { if (v != null) throw new ShowHelpException(); } },
                    { "bm|bmfont=", "file name of the input BMFont (i.e. bm=ArialNarrow.fnt)",
                        v => bmFontInputFileName = v },
                    { "bfg|bfgfont=",  "file name of the output BFG font (default: bfg=<FontName>.dat)",
                        (string v) => bfgFontOutputFileName = v },
                };
            }
        }

        public class DecomposeD3Options
        {
            public string d3FontInputFileName;
            public string dirWithFontTextures;
            public string bmConfigFile;
            public bool bmConfigAppend;
            public string imageOutputDir;
            public string[] langs;
            public string dirWithLangMaps;
            public string zeroSizeImage;

            // helpers
            public static string allButRussian = string.Join(",", D3FontDecompose.allLangsExceptRussian);
            public static string onlyRussian = string.Join(",", D3FontDecompose.onlyRussian);

            public void Validate()
            {
                if (string.IsNullOrWhiteSpace(d3FontInputFileName))
                    throw new ValidationException("Input D3 font file name not specified.");
                if (!File.Exists(d3FontInputFileName))
                    throw new ValidationException("Input D3 font file does not exist.");

                if (!Directory.Exists(dirWithFontTextures))
                {
                    throw new ValidationException("Directory with font textures does not exist.");
                }

                if (string.IsNullOrWhiteSpace(bmConfigFile))
                {
                    string defaultName = Path.ChangeExtension(Path.GetFileName(d3FontInputFileName), ".txt ");
                    bmConfigFile = Path.Combine(Path.GetDirectoryName(d3FontInputFileName), defaultName);
                }

                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(bmConfigFile));
                    if (!File.Exists(bmConfigFile))
                        File.Create(bmConfigFile).Dispose();
                }
                catch
                {
                    throw new ValidationException("Invalid output file name.");
                }

                if (string.IsNullOrWhiteSpace(imageOutputDir))
                {
                    imageOutputDir = Path.GetDirectoryName(bmConfigFile);
                }

                try
                {
                    Directory.CreateDirectory(imageOutputDir);
                }
                catch
                {
                    throw new ValidationException("Invalid output file name.");
                }

                if (langs == null)
                {
                    langs = D3FontDecompose.allLangsExceptRussian;
                }
            }

            public void PrintHelp(TextWriter output)
            {
                output.WriteLine("BFGFontTool decompose-d3 options...");
                output.WriteLine("    decomposes Doom 3's .dat font into BMFont's character descriptions");
                GetOptions().WriteOptionDescriptions(output);
            }

            public OptionSet GetOptions()
            {
                return new OptionSet () {
                    { "h|help",  "show this message and exit", 
                        v => { if (v != null) throw new ShowHelpException(); } },
                    { "d3|d3font=", "file name of the input Doom 3 .dat font (i.e. d3=fontimage_48.dat)",
                        v => d3FontInputFileName = v },
                    { "d3texs=",  "directory containing font's textures (files like: arial_0_48.dds)",
                        (string v) => dirWithFontTextures = v },
                    { "o|bmfc=", "BM configiguration file to which to write character descriptions (default: <FontName>.txt)",
                        v => bmConfigFile = v },
                    { "append", "Do not replace icons in BM configiguration file, but append new instead (default: false)",
                        v => bmConfigAppend = v != null },
                    { "imgout=", "directory to which to write character images (default: bmfc file's directory)",
                        v => imageOutputDir = v },
                    { "lang|language=", "comma separated list of The Dark Mod's languages to use during font's conversion (default: all except russian)\n" +
                                         "available langs: " + string.Join(",", DecomposeD3Options.allButRussian, DecomposeD3Options.onlyRussian),
                        (string v) => langs = v.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries) },
                    { "remap|remapdir=", "directory in which The Dark Mod's remap tables are (like: polish.map)",
                        v => dirWithLangMaps = v },
                };
            }
        }

        public class ProgramOptions
        {
            public enum EProgramMode
            {
                NotSpecified,
                CreateBFGFontFromBMFont,
                DecomposeD3Font,
            }

            public EProgramMode programMode = EProgramMode.NotSpecified;
            public CreateBFGOptions createBFGOptions = new CreateBFGOptions();
            public DecomposeD3Options decomposeD3Options = new DecomposeD3Options();

            public void ParseArgs(string[] args)
            {
                Debug.Assert(args.Length > 0);

                OptionSet options = null;

                if (args[0].ToLowerInvariant() == "create-bfg")
                {
                    programMode = EProgramMode.CreateBFGFontFromBMFont;
                    options = createBFGOptions.GetOptions();
                }
                else
                if (args[0].ToLowerInvariant() == "decompose-d3")
                {
                    programMode = EProgramMode.CreateBFGFontFromBMFont;
                    options = createBFGOptions.GetOptions();
                }

                bool showHelp = false;

                if (options == null)
                {
                    showHelp = true;
                }
                else
                {
                    try {
                        List<string> extra = options.Parse (args.Skip(1));
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
                    catch (ShowHelpException e) {
                        showHelp = true;
                    }
                }

                if (showHelp)
                {
                    PrintHelp(Console.Out);
                    return;
                }

                ValidateOrHelp(Console.Out);
            }

            public bool ValidateOrHelp(TextWriter output)
            {
                try
                {
                    programOptions.Validate();
                    return true;
                }
                catch (ValidationException exc)
                {
                    output.WriteLine("{0}", exc.Message);
                    PrintHelp(output);
                    return false;
                }
            }

            public void Validate()
            {
                switch(programMode)
                {
                    case EProgramMode.NotSpecified: throw new ValidationException("Program mode not specified.");
                    case EProgramMode.CreateBFGFontFromBMFont: createBFGOptions.Validate(); return;
                    case EProgramMode.DecomposeD3Font: decomposeD3Options.Validate(); return;
                }
            }

            public void PrintHelp(TextWriter output)
            {
                switch (programMode)
                {
                    case EProgramMode.NotSpecified:
                        {
                            output.WriteLine("Usage:");
                            output.WriteLine("    BFGFontTool create-bfg --help");
                            output.WriteLine("    BFGFontTool decompose-d3 --help");
                        }
                        break;
                    case EProgramMode.CreateBFGFontFromBMFont:
                        {
                            createBFGOptions.PrintHelp(output);
                        }
                        break;
                    case EProgramMode.DecomposeD3Font:
                        {
                            decomposeD3Options.PrintHelp(output);
                        }
                        break;
                }
            }
        }

        public static ProgramOptions programOptions = new ProgramOptions();

        public static void SaveOptions()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ProgramOptions));
                using (TextWriter tw = new StreamWriter("BFGFontTool.cfg"))
                {
                    serializer.Serialize(tw, programOptions);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error while saving configuration: {0}", exc.Message);
            }
        }

        public static bool LoadOptions()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ProgramOptions));
                using (FileStream file = File.OpenRead("BFGFontTool.cfg"))
                {
                    programOptions = (ProgramOptions)serializer.Deserialize(file);
                }
                return true;
            }
            catch
            {
                return false;
            }
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

            programOptions.ParseArgs(args);

            switch(programOptions.programMode)
            {
                case ProgramOptions.EProgramMode.CreateBFGFontFromBMFont:
                    createBFG(programOptions.createBFGOptions);
                    break;
                case ProgramOptions.EProgramMode.DecomposeD3Font:
                    decomposeD3(programOptions.decomposeD3Options);
                    break;
                case ProgramOptions.EProgramMode.NotSpecified:
                    Debug.Assert(false);
                    break;
            }
        }
    }
}

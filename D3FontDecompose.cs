using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;
using ImageMagick;
using System.Drawing;

namespace BFGFontTool
{
    /// <summary>
    /// This class decomposes the Doom 3 font into individual character images.
    /// It also applies The Dark Mod's code page conversion.
    /// To ignore any conversions and use iso-8859-1 code page choose "english" language.
    /// </summary>
    class D3FontDecompose
    {
        public TextWriter textOut = Console.Out;

        D3Font font;
        string dirWithFontTextures;
        string imageOutputDir;
        string bmConfigFile;
        string[] langs;
        string dirWithLangMaps;
        string zeroSizeImage;
        IDictionary<string, Encoding> baseCodePageEncodings = new Dictionary<string, Encoding>();
        IDictionary<string, IDictionary<byte, byte> > invRemapTables = new Dictionary<string, IDictionary<byte, byte> >();

        Dictionary<string, MagickImage> textures = new Dictionary<string, MagickImage>();
        IDictionary<int, BMIconInfo> iconInfos = new Dictionary<int, BMIconInfo>();

        /// <summary>
        /// Decompose the Doom 3 font into individual character images.
        /// It also applies The Dark Mod's code page conversion.
        /// To ignore any conversions and use iso-8859-1 code page choose "english" language.
        /// </summary>
        /// <param name="font">Doom 3 font</param>
        /// <param name="dirWithFontTextures">Path to directory containing font textures, like "arial_0_48.dds".</param>
        /// <param name="lang">The Dark Mod's language to use. Can be "english", "polish", "german", etc.</param>
        /// <param name="dirWithLangMaps">Optional: Directory containing The Dark Mod's code page remapping maps, like "polish.map".</param>
        /// <param name="zeroSizeImage">Optional: image that have 0x0 size.</param>
        /// <param name="bmConfigFile">File to output BMFont's external image configuration to.</param>
        /// <param name="imageOutputDir">Directory into which output character images.</param>
        public static void Decompose(D3Font font, string dirWithFontTextures, string[] langs, string dirWithLangMaps, string zeroSizeImage, string bmConfigFile, string imageOutputDir)
        {
            var fontDecomposer = new D3FontDecompose(font, dirWithFontTextures, langs, dirWithLangMaps, zeroSizeImage, bmConfigFile, imageOutputDir);
            fontDecomposer.Decompose();
        }

        private D3FontDecompose(D3Font font, string dirWithFontTextures, string[] langs, string dirWithLangMaps, string zeroSizeImage, string bmConfigFile, string imageOutputDir)
        {
            this.font = font;
            this.dirWithFontTextures = dirWithFontTextures;
            this.langs = langs;
            this.dirWithLangMaps = dirWithLangMaps;
            this.imageOutputDir = imageOutputDir;
            this.bmConfigFile = bmConfigFile;
            this.zeroSizeImage = zeroSizeImage;

            Directory.CreateDirectory(imageOutputDir);

            foreach (string lang in langs)
            {
                baseCodePageEncodings[lang] = BaseCodePageEncodingForLang(lang);
                LoadRemapTableForLang(lang);
            }
        }

        private void Decompose()
        {
            var infosDict = new Dictionary<int, IList<string>>();
            for (int i = 0; i < D3Font.GLYPHS_PER_FONT; i++)
            {
                IList<string> infos;
                int utf32Char = Remap((byte)i, out infos);
                if (iconInfos.ContainsKey(utf32Char))
                {
                    textOut.WriteLine("WARNING: The glyph 0x{0:X2} maps to previously outputted char '{1}' - IGNORING", i, char.ConvertFromUtf32(utf32Char));
                    textOut.WriteLine("    Current output:");
                    foreach (var info in infos)
                    {
                        textOut.WriteLine("        {0}", info);
                    }
                    textOut.WriteLine("    First output:");
                    foreach (var info in infosDict[utf32Char])
                    {
                        textOut.WriteLine("        {0}", info);
                    }
                    continue;
                }
                infosDict[utf32Char] = infos;
                D3Glyph glyph = font.glyphs[i];
                Decompose(glyph, utf32Char);
            }

            if (!File.Exists(bmConfigFile))
            {
                File.Create(bmConfigFile).Dispose();
            }
            IList<string> bmConfig = File.ReadAllLines(bmConfigFile).ToList();
            foreach (var kv in iconInfos)
            {
                bmConfig.Add(kv.Value.ToString());
            }
            File.WriteAllLines(bmConfigFile, bmConfig);
        }

        private void Decompose(D3Glyph glyph, int utf32Char)
        {
            string textureName = Path.Combine(dirWithFontTextures, Path.GetFileName(glyph.textureName));
            if (!textures.ContainsKey(textureName))
            {
                string realName = textureName;
                if (!File.Exists(realName))
                {
                    realName = Path.ChangeExtension(textureName, "dds");
                }
                MagickImage newImage = new MagickImage(realName);
                textures[textureName] = newImage;
            }

            MagickImage image = textures[textureName].Clone();
            

            BMIconInfo icon = new BMIconInfo();
            icon.imageFile = Path.Combine(imageOutputDir, string.Format("{0}_char{1}.tga", font.name, utf32Char));
            icon.charId = utf32Char;

            // baseline position in pixels from top edges of the image
            int baseline = glyph.height - glyph.top; // maybe... maybe it's glyph.imageHeight - glyph.top... and maybe it's something else...

            Debug.Assert(image.Width == 256);
            Debug.Assert(image.Height == 256);
            icon.xoffset = 0;
            icon.yoffset = font.maxHeight - glyph.top - glyph.bottom; // -baseline;
            int xstart = (int)(glyph.s * image.Width); // +glyph.pitch; // this "+ glyph.pitch" is a guess
            int ystart = (int)(glyph.t * image.Height);
            int xend = (int)(glyph.s2 * image.Width);
            int yend = (int)(glyph.t2 * image.Height);
            int glyphWidth = xend - xstart;
            int glyphHeight = yend - ystart;
            Debug.Assert(glyph.imageWidth == glyphWidth);
            Debug.Assert(glyph.imageHeight == glyphHeight);
            xstart += glyph.pitch; // this "+ glyph.pitch" is a guess

            // this damn ImageMagick can't create zero size images!
            if (zeroSizeImage == null)
            {
                if (glyphWidth == 0)
                {
                    glyphWidth = 1;
                }
                if (glyphHeight == 0)
                {
                    glyphHeight = 1;
                }
            }

            icon.xadvance = glyph.xSkip - glyphWidth; // maybe add "- glyph.pitch" here?

            iconInfos[icon.charId] = icon;

            MagickGeometry geom = new MagickGeometry(xstart, ystart, glyphWidth, glyphHeight);
            if (glyphWidth == 0 || glyphHeight == 0)
            {
                //Bitmap bmp = new Bitmap(0, 0, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                var newImage = new MagickImage(zeroSizeImage);
                //newImage.ColorType = image.ColorType;
                //newImage.ColorSpace = image.ColorSpace;
                //image.Crop(1, 0);
                //image.Trim();
                image = newImage;
            }
            else
            {
                image.Crop(geom);
            }
            image.Format = MagickFormat.Tga;
            image.Write(icon.imageFile);
        }

        private int Remap(byte tdmChar, out IList<string> infos)
        {
            // base code page for lang: codepage char -> utf32
            // remap file for lang: codepage char -> TDM char
            // fonts: TDM char
            // so: char number n in TDM font is:
            //     utf32Char = codepage[ remap^-1[n] ]

            var chars = new HashSet<int>();
            var mappedChars = new HashSet<int>();
            int? utf32Eng = null;
            infos = new List<string>();
            foreach (string lang in langs)
            {
                var invRemapTable = invRemapTables[lang];
                byte unmapped = invRemapTable.ContainsKey(tdmChar) ? invRemapTable[tdmChar] : tdmChar;
                int utf32Char = CodePageToUTF32(unmapped, lang);
                if (unmapped != tdmChar)
                {
                    mappedChars.Add(utf32Char);
                    infos.Add(string.Format("{0}: 0x{1:X2} -> 0x{2:X2} -> '{3}'", lang, tdmChar, unmapped, char.ConvertFromUtf32(utf32Char)));
                }
                else
                {
                    chars.Add(utf32Char);
                    infos.Add(string.Format("{0}: 0x{1:X2} -> '{2}'", lang, tdmChar, char.ConvertFromUtf32(utf32Char)));
                }
                if (lang == "english")
                {
                    utf32Eng = utf32Char;
                }
            }

            if (mappedChars.Count > 1)
            {
                textOut.WriteLine("WARNING: The character 0x{0:X2} maps to following utf8 chars:", tdmChar);
                foreach (var info in infos)
                {
                    textOut.WriteLine("    {0}", info);
                }
                textOut.WriteLine("    Taking: '{0}'", char.ConvertFromUtf32(mappedChars.First()));
            }
            if (mappedChars.Count == 1)
            {
                return mappedChars.First();
            }

            if (chars.Count > 1)
            {
                if (utf32Eng.HasValue)
                {
                    textOut.WriteLine("INFO: The character 0x{0:X2} maps to different chars in different codepages. Taking english: '{1}'", tdmChar, char.ConvertFromUtf32(chars.First()));
                    return utf32Eng.Value;
                }
                textOut.WriteLine("WARNING: The character 0x{0:X2} maps to different chars in different codepages.", tdmChar);
                textOut.WriteLine("    Taking: '{0}'", char.ConvertFromUtf32(chars.First()));
            }
            return chars.First();
        }

        public static string[] allLangsExceptRussian = { "czech", "danish", "dutch", "english", "french", "german", "hungarian", "italian", "polish", "portuguese", "slovak", "spanish" };
        public static string[] onlyRussian = { "russian" };
        public string BaseCodePageNameForLang(string lang)
        {
            switch(lang)
            {
                case "czech": return "iso-8859-2";
                case "danish": return "iso-8859-1";
                case "dutch": return "iso-8859-1";
                case "english": return "iso-8859-1";
                case "french": return "iso-8859-15";
                case "german": return "iso-8859-1";
                case "hungarian": return "iso-8859-2";
                case "italian": return "iso-8859-1";
                case "polish": return "iso-8859-2";
                case "portuguese": return "iso-8859-1";
                case "russian": return "windows-1251";
                case "slovak": return "iso-8859-2";
                case "spanish": return "iso-8859-1";
            }

            textOut.WriteLine("WARNING: unknown language: {0}", lang);
            return "iso-8859-1"; // default to english...
        }

        public Encoding BaseCodePageEncodingForLang(string lang)
        {
            string codePageName = BaseCodePageNameForLang(lang);
            Encoding codePageEncoding = Encoding.GetEncoding(codePageName);
            return codePageEncoding;
        }

        public int BaseCodePageForLang(string lang)
        {
            Encoding codePageEncoding = BaseCodePageEncodingForLang(lang);
            return codePageEncoding.CodePage;
        }

        public int CodePageToUTF32(byte codePageChar, string lang)
        {
            string decodedChar = baseCodePageEncodings[lang].GetString(new byte[] { codePageChar }, 0, 1);

            Debug.Assert((decodedChar.Length == 1) || ((decodedChar.Length == 2) && char.IsSurrogatePair(decodedChar, 0)));

            int utf32Char = char.ConvertToUtf32(decodedChar, 0);
            //byte[] utf32Bytes = Encoding.UTF32.GetBytes(decodedChar);

            return utf32Char;
        }

        void LoadRemapTableForLang(string lang)
        {
            var invRemapTable = new Dictionary<byte, byte>();
            invRemapTables[lang] = invRemapTable;

            string[] allLines;
            try
            {
                string mapFile = Path.Combine(dirWithLangMaps, Path.ChangeExtension(lang, "map"));
                allLines = File.ReadAllLines(mapFile);
            }
            catch
            {
                textOut.WriteLine("Couldn't load remap table for language: {0}.", lang);
                return;
            }

            string pattern = @"^\s+0x([0-9A-F]{2})\s+0x([0-9A-F]{2})";
            var regex = new Regex(pattern);
            foreach (string line in allLines)
            {
                var match = regex.Match(line);
                if (!match.Success)
                    continue;

                byte fromChar = byte.Parse(match.Groups[1].Value, NumberStyles.HexNumber);
                byte toChar = byte.Parse(match.Groups[2].Value, NumberStyles.HexNumber);
                invRemapTable[toChar] = fromChar;
            }
        }

    }
}

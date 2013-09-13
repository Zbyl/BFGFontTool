using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace BFGFontTool
{
    /// <summary>
    /// Classes here are used to load Doom 3 .dat font files.
    /// Those are different font files than those used by Doom 3 BFG edition.
    /// </summary>
    class D3Glyph
    {
        public int height;	    	// number of scan lines
        public int top;			    // top of glyph in buffer
        public int bottom;			// bottom of glyph in buffer
        public int pitch;			// width for copying
        public int xSkip;			// x adjustment
        public int imageWidth;		// width of actual image
        public int imageHeight;	    // height of actual image
        public float s;				// x offset in image where glyph starts
        public float t;				// y offset in image where glyph starts
        public float s2;
        public float t2;
        public string textureName;

        public void Scale(float glyphScale)
        {
            height      = (int)(height * glyphScale);
            top         = (int)(top * glyphScale);
            bottom      = (int)(bottom * glyphScale);
            pitch       = (int)(pitch * glyphScale);
            xSkip       = (int)(xSkip * glyphScale);
            imageHeight = (int)(imageHeight * glyphScale);
            imageWidth  = (int)(imageWidth * glyphScale);
        }

        public void Load(BMFont font, BMGlyph glyph, bool hackForBFG, float hackScaleForBFG = 1.0f)
        {
            int textureWidth = hackForBFG ? 256 : font.scaleW;
            int textureHeight = hackForBFG ? 256 : font.scaleH;

            height = glyph.height;
            top = font.lineHeight - glyph.height;
            bottom = 0;
            pitch = glyph.width;
            xSkip = glyph.xadvance;
            imageHeight = glyph.height;
            imageWidth = glyph.width;

            s = (float)glyph.x / textureWidth;
            t = (float)glyph.y / textureHeight;
            s2 = (float)(glyph.x + glyph.width) / textureWidth;
            t2 = (float)(glyph.y + glyph.height) / textureHeight;

            if (hackForBFG)
            {
                Scale(hackScaleForBFG);
                s2 = s + (float)imageWidth / textureWidth;
                t2 = t + (float)imageHeight / textureHeight;
            }

            Debug.Assert(font.pages.Count == 1);
            textureName = "fonts/" + font.pages[0].file;
            //textureName = "fonts/" + font.faceName;
        }

        public void Load(BinaryReader br)
        {
            height = br.ReadInt32();
            top = br.ReadInt32();
            bottom = br.ReadInt32();
            pitch = br.ReadInt32();
            xSkip = br.ReadInt32();
            imageWidth = br.ReadInt32();
            imageHeight = br.ReadInt32();
            s = br.ReadSingle();
            t = br.ReadSingle();
            s2 = br.ReadSingle();
            t2 = br.ReadSingle();
            int junk = br.ReadInt32();

            byte[] ascii = new byte[32];
            br.Read(ascii, 0, 32);
            ASCIIEncoding asen = new ASCIIEncoding();
            textureName = asen.GetString(ascii); // prefixed with "fonts/"
            textureName = textureName.Replace("\0", "");
        }

        public void Save(BinaryWriter bw)
        {
            bw.Write((int)height);
            bw.Write((int)top);
            bw.Write((int)bottom);
            bw.Write((int)pitch);
            bw.Write((int)xSkip);
            bw.Write((int)imageWidth);
            bw.Write((int)imageHeight);
            bw.Write((float)s);
            bw.Write((float)t);
            bw.Write((float)s2);
            bw.Write((float)t2);
            bw.Write((int)0);

            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ascii = asen.GetBytes(textureName); // prefixed with "fonts/"

            for (int i = 0; i < 32; ++i)
            {
                if (i < ascii.Length)
                    bw.Write(ascii, i, 1);
                else
                    bw.Write((byte)0);
            }
        }
    }

    class D3Font
    {
        public const int GLYPHS_PER_FONT = 256;
        public D3Glyph[] glyphs = new D3Glyph[GLYPHS_PER_FONT];
        public float glyphScale;
        public string name;
        public int maxHeight;
        public int maxWidth;

        public void Load(BMFont font, float glyphScale = 1.0f)
        {
            Debug.Assert(font.pages.Count == 1);
            BMPage page = font.pages[0];
            var bmglyphs = from g in font.glyphs
                         where g.page == 0
                         select g;

            var dict = bmglyphs.ToDictionary(g => g.id, g => g);

            maxHeight = 0;
            maxWidth = 0;
            for (int i = 0; i < GLYPHS_PER_FONT; ++i)
            {
                BMGlyph bmglyph = dict.ContainsKey(i) ? dict[i] : bmglyphs.First();

                D3Glyph glyph = new D3Glyph();
                glyph.Load(font, bmglyph, true, glyphScale);
                name = font.faceName;

                if (maxHeight < glyph.height)
                    maxHeight = glyph.height;
                if (maxWidth < glyph.xSkip)
                    maxWidth = glyph.xSkip;

                glyphs[i] = glyph;
            }

            this.glyphScale = glyphScale;
        }

        public void Load(string fileName)
        {
            FileStream fs = File.OpenRead(fileName);
            BinaryReader br = new BinaryReader(fs);

            name = FriendlyFontFace(fileName);

            maxHeight = 0;
            maxWidth = 0;
            for (int i = 0; i < GLYPHS_PER_FONT; i++)
            {
                D3Glyph glyph = new D3Glyph();
                glyph.Load(br);
                glyphs[i] = glyph;

                if (maxHeight < glyph.height)
                    maxHeight = glyph.height;
                if (maxWidth < glyph.xSkip)
                    maxWidth = glyph.xSkip;
            }

            glyphScale = br.ReadSingle();

            br.Close();
            fs.Close();
        }

        static string FriendlyFontFace(string fileName)
        {
            // fileName == something like fonts/stone/fontimage_24.dat
            // in D3 it is named "fonts/stone"
            // we'll return just "stone"
            return Path.GetFileName(Path.GetDirectoryName(fileName)); // fonts/stone/fontimage_24.dat -> fonts/stone -> stone
        }


        public void Save(string fileName)
        {
            FileStream fs = File.Create(fileName);
            BinaryWriter bw = new BinaryWriter(fs);

            foreach (var glyph in glyphs)
            {
                glyph.Save(bw);
            }

            bw.Write((float)glyphScale);

            bw.Close();
            fs.Close();
        }
    }

    static class B3Fonts
    {
        public static void SaveD3Font(this BMFont font, string fileName, float glyphScale = 1.0f)
        {
            D3Font d3font = new D3Font();
            d3font.Load(font, glyphScale);
            d3font.Save(fileName);
        }

        public static void SaveD3Fonts(this BMFont font, string outputDirectory)
        {
            string fileNameSmall = Path.Combine(outputDirectory, "fontimage_12.dat");
            string fileNameMedium = Path.Combine(outputDirectory, "fontimage_24.dat");
            string fileNameBig = Path.Combine(outputDirectory, "fontimage_48.dat");
            font.SaveD3Font(fileNameSmall, 0.25f);
            font.SaveD3Font(fileNameMedium, 0.5f);
            font.SaveD3Font(fileNameBig, 1.0f);
        }
    }
}

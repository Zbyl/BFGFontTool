using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace BFGFontTool
{
    /// <summary>
    /// Here we have conversion from BMFont to a Doom 3 BFG edition .dat font file.
    /// Only fonts with one page are supported.
    /// </summary>
    public class BFGGlyph
    {
        public int id;          // UTF32 - stored separately

        public byte width;      // width of glyph in pixels
        public byte height;     // height of glyph in pixels
        public byte top;        // distance in pixels from the base line to the top of the glyph
        public byte left;       // distance in pixels from the pen to the left edge of the glyph
        public byte xSkip;      // x adjustment after rendering this glyph
        public ushort s;        // x offset in image where glyph starts (in pixels)
        public ushort t;        // y offset in image where glyph starts (in pixels)

        public void Load(BMFont font, BMGlyph glyph)
        {
            id = glyph.id;

            width = (byte)glyph.width;
            height = (byte)glyph.height;
            top = (byte)(font.fontBase - glyph.yoffset);
            //top = (byte)(font.lineHeight - glyph.yoffset);
            left = (byte)glyph.xoffset;
            xSkip = (byte)glyph.xadvance;
            s = (ushort)glyph.x;
            t = (ushort)glyph.y;
        }
    }

    public class BFGFont
    {
        public List<BFGGlyph> glyphs = new List<BFGGlyph>();

        short pointSize = 48; // must be 48!
        short ascender;
        short descender;

        public void Load(BMFont font)
        {
            Debug.Assert(font.pages.Count == 1);
            BMPage page = font.pages[0];
            var bmGlyphs = from g in font.glyphs
                         where g.page == 0
                         select g;

            pointSize = 48; // must be 48!
            ascender = (short)font.fontBase;
            descender = (short)(font.fontBase - font.lineHeight);

            int lowestPoint = 0;
            foreach (BMGlyph g in bmGlyphs)
            {
                BFGGlyph glyph = new BFGGlyph();
                glyph.Load(font, g);
                glyphs.Add(glyph);

                int lowestGlyph = g.yoffset + glyph.height;
                if (lowestGlyph > lowestPoint)
                    lowestPoint = lowestGlyph;

                if (lowestGlyph > font.lineHeight)
                {
                    Console.WriteLine("WARNING: Glyph {0} is too high {1} for line height {2}.", g.id, lowestGlyph, font.lineHeight);
                }
            }

            if (lowestPoint != font.lineHeight)
            {
                Console.WriteLine("WARNING: Line height ({0}) is not what I thought it would be ({1}).", font.lineHeight, lowestPoint);
                Console.WriteLine("         BMFont's settings used to generate source font might be wrong.");
                Console.WriteLine("         Also descender might be wrong: {0} instead of {1}.", descender, font.fontBase - lowestPoint);
            }

        }

        public void Save(string fileName)
        {
            FileStream fs = File.Create(fileName);
            BinaryWriter bw = new BinaryWriter(fs);

            const int FONT_INFO_VERSION = 42;
            //const int FONT_INFO_MAGIC = (FONT_INFO_VERSION | ('i' << 24) | ('d' << 16) | ('f' << 8));

            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ascii = asen.GetBytes("idf1");
            ascii[3] = FONT_INFO_VERSION;
            bw.Write(ascii);

            bw.WriteBig(pointSize);
            bw.WriteBig(ascender);
            bw.WriteBig(descender);

            short numGlyphs = (short)glyphs.Count();
            bw.WriteBig(numGlyphs);

            foreach (var glyph in glyphs)
            {
                bw.Write(glyph.width);
                bw.Write(glyph.height);
                bw.Write(glyph.top);
                bw.Write(glyph.left);
                bw.Write(glyph.xSkip);
                byte padding = 0;
                bw.Write(padding);
                bw.Write(glyph.s);
                bw.Write(glyph.t);
            }

            foreach (var glyph in glyphs)
            {
                bw.Write(glyph.id);
            }

            bw.Close();
            fs.Close();
        }
    }

    static class BFGFonts
    {
        public static void WriteBig(this BinaryWriter bw, byte val)
        {
            bw.Write(val);
        }

        public static void WriteBig(this BinaryWriter bw, short val)
        {
            bw.Write((byte)(val >> 8));
            bw.Write((byte)(val & 0xFF));
        }

        public static void WriteBig(this BinaryWriter bw, int val)
        {
            bw.WriteBig((short)(val >> 16));
            bw.WriteBig((short)(val & 0xFFFF));
        }

        public static void SaveBFGFont(this BMFont font, string fileName)
        {
            BFGFont bfgFont = new BFGFont();
            bfgFont.Load(font);
            bfgFont.Save(fileName);
        }
    }
}

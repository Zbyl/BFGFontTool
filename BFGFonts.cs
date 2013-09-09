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
            FileStream fs = File.Create(fileName);
            BinaryWriter bw = new BinaryWriter(fs);

            const int FONT_INFO_VERSION = 42;
            //const int FONT_INFO_MAGIC = (FONT_INFO_VERSION | ('i' << 24) | ('d' << 16) | ('f' << 8));

            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ascii = asen.GetBytes("idf1");
            ascii[3] = FONT_INFO_VERSION;
            bw.Write(ascii);

            Debug.Assert(font.pages.Count == 1);
            BMPage page = font.pages[0];
            var glyphs = from g in font.glyphs
                         where g.page == 0
                         select g;

            short pointSize = 48; // must be 48!
            short ascender = 0;
            short descender = 0;
            bw.WriteBig(pointSize);
            bw.WriteBig(ascender);
            bw.WriteBig(descender);

            short numGlyphs = (short)glyphs.Count();
            bw.WriteBig(numGlyphs);

            foreach (var glyph in glyphs)
            {
                bw.Write((byte)glyph.width);
                bw.Write((byte)glyph.height);
                bw.Write((byte)(font.fontBase - glyph.yoffset));
                bw.Write((byte)glyph.xoffset);
                bw.Write((byte)glyph.xadvance);
                byte padding = 0;
                bw.Write((byte)padding);
                bw.Write((short)glyph.x);
                bw.Write((short)glyph.y);
            }

            foreach (var glyph in glyphs)
            {
                bw.Write(glyph.id);
            }

            bw.Close();
            fs.Close();
        }
    }
}

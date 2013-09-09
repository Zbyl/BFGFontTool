using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
    }

    class D3Font
    {
        public const int GLYPHS_PER_FONT = 256;
        public D3Glyph[] glyphs;
        public float glyphScale;
        public string name;
        public int maxHeight;
        public int maxWidth;

        public void Load(string fileName)
        {
            FileStream fs = File.OpenRead(fileName);
            BinaryReader br = new BinaryReader(fs);

            name = FriendlyFontFace(fileName);

            maxHeight = 0;
            maxWidth = 0;
            glyphs = new D3Glyph[GLYPHS_PER_FONT];
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
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Diagnostics;

namespace BFGFontTool
{
    class RemapHelper
    {
        /// <summary>
        /// All UTF32 characters that we want to support in our remap table.
        /// </summary>
        HashSet<int> allInterestingCharacters = new HashSet<int>();

        public void AddCharactersFromFont(BMFont font)
        {
            allInterestingCharacters.UnionWith(from glyph in font.glyphs select glyph.id);
        }

        public void AddCharactersFromUTF8File(string fileName)
        {
            AddCharactersFromFile(fileName, Encoding.UTF8);
        }

        public void AddCharactersFromFile(string fileName, Encoding encoding)
        {
            byte[] bytes = File.ReadAllBytes(fileName);
            string text = encoding.GetString(bytes);

            byte[] utf32bytes = Encoding.UTF32.GetBytes(text);
            Debug.Assert(utf32bytes.Length % 4 == 0);

            for (int i = 0; i < utf32bytes.Length; i += 4)
            {
                int utf32 = utf32bytes[0];
                utf32 += (int)utf32bytes[1] << 8;
                utf32 += (int)utf32bytes[2] << 16;
                utf32 += (int)utf32bytes[3] << 24;

                allInterestingCharacters.Add(utf32);
            }
        }

        public IDictionary<int, T> GenerateMap<T>(Func<int, T> fun)
        {
            return allInterestingCharacters.ToDictionary(c => c, c => fun(c));
        }

        public void SaveAsRemapTable<T>(string fileName, IDictionary<int, T> table)
        {
            FileStream fs = File.Create(fileName);
            BinaryWriter bw = new BinaryWriter(fs);

            const int CHARACTER_REMAP_TABLE_VERSION = 1;
            //const int CHARACTER_REMAP_TABLE_MAGIC1 = (CHARACTER_REMAP_TABLE_VERSION | ('t' << 24) | ('d' << 16) | ('m' << 8));
            //const int CHARACTER_REMAP_TABLE_MAGIC2 = (('c' << 24) | ('r' << 16) | ('m' << 8) | ('t'));	// Character ReMap Table

            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ascii = asen.GetBytes("tdm1crmt");
            ascii[3] = CHARACTER_REMAP_TABLE_VERSION;
            bw.Write(ascii);

            bw.Write((Int32)table.Count);

            foreach (var kv in table)
            {
                bw.Write((Int32)kv.Key);
                bw.Write(System.Convert.ToInt32(kv.Value));
            }

            bw.Close();
            fs.Close();
        }

        static int CharToUTF32(string utf16OneChar)
        {
            return char.ConvertToUtf32(utf16OneChar, 0);
        }

        static string ToUpper(string utf16OneChar)
        {
            return utf16OneChar.ToUpperInvariant();
        }

        static string ToLower(string utf16OneChar)
        {
            return utf16OneChar.ToLowerInvariant();
        }

        static string StripDiacritics(string utf16OneChar)
        {
            string result = "";

            //var normalization = NormalizationForm.FormKD;
            var normalization = NormalizationForm.FormD;
            string normalized = utf16OneChar.Normalize(normalization);
            foreach (char c in normalized)
                switch (CharUnicodeInfo.GetUnicodeCategory(c))
                {
                    case UnicodeCategory.NonSpacingMark:
                    case UnicodeCategory.SpacingCombiningMark:
                    case UnicodeCategory.EnclosingMark:
                        //do nothing
                        break;
                    default:
                        result += c;
                        break;
                }

            return result;
        }

        static string SortOrder(string utf16OneChar)
        {
            return utf16OneChar.ToUpperInvariant();
        }

        static int Convert(Func<string, string> fun, int c)
        {
            string utf16OneChar = char.ConvertFromUtf32(c);
            string converted = fun(utf16OneChar);
            return CharToUTF32(converted);
        }

        public void SaveToUpperTable(string fileName)
        {
            var map = GenerateMap(c => Convert(ToUpper, c));
            SaveAsRemapTable(fileName, map);
        }

        public void SaveToLowerTable(string fileName)
        {
            var map = GenerateMap(c => Convert(ToLower, c));
            SaveAsRemapTable(fileName, map);
        }

        public void SaveStipDiacriticsTable(string fileName)
        {
            var map = GenerateMap(c => Convert(StripDiacritics, c));
            SaveAsRemapTable(fileName, map);
        }

        class Comp : IComparer<string>
        {
            CultureInfo culture;

            public Comp(CultureInfo culture)
            {
                this.culture = culture;
            }

            public int Compare(string x, string y)
            {
                return culture.CompareInfo.Compare(x, y, CompareOptions.StringSort);
            }
        }

        public void SaveSortOrderTable(string fileName)
        {
            SaveSortOrderTable(fileName, System.Globalization.CultureInfo.InvariantCulture);
        }

        public void SaveSortOrderTable(string fileName, CultureInfo culture)
        {

            var utf16s = (from c in allInterestingCharacters
                          select new { Key = c, Value = char.ConvertFromUtf32(c) })
                          .OrderBy(c => c.Value, new Comp(culture));

            int idx = 0;
            var sortIdxs = new Dictionary<int, int>();
            foreach (var kv in utf16s)
            {
                sortIdxs.Add(kv.Key, idx);
                idx++;
            }

            SaveAsRemapTable(fileName, sortIdxs);
        }
    }
}

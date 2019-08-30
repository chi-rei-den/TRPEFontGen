using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using TRPEFontGen;

namespace Example
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                GenMicrosoftYaheiFont();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void DecodeFile(string path)
        {
                var reader = new Reader();
                var fontFile = reader.Read(path);

                Console.WriteLine(fontFile.Chars.Count);
                foreach (var c in fontFile.Chars)
                {
                    File.AppendAllText($"{path}.decoded", $"[{c.Char}]: G:[{c.Glyph}], C:[{c.Cropping}], K:[{c.Kerning}]\n");
                }

                Console.WriteLine($"LineSpacing: {fontFile.LineSpacing}");
                Console.WriteLine($"Spacing: {fontFile.Spacing}");
                Console.WriteLine($"DefaultChar: {fontFile.DefaultChar}");
        }

        public static void GenMicrosoftYaheiFont()
        {
            var chars = new List<char>();
            var encoding = Encoding.Unicode;
                
            addChar(chars, 0, 0x52F);
            addChar(chars, 0x2E80, 0x33FF);
            addChar(chars, 0x4E00, 0x9FA5);
            addChar(chars, 0xFF00, 0xFFEF);
                
            var generator = new Generator();
            var font = new Font("微软雅黑", 15, FontStyle.Regular);
            var fontFile = generator.Generate(font, 4096, chars.ToArray());
            var writer = new Writer();
            
            writer.Write("./MicrosoftYahei.txt", fontFile);
        }

        private static void addChar(List<char> chars, int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                chars.Add(Encoding.Unicode.GetChars(BitConverter.GetBytes(i))[0]);
            }
        }
    }
}
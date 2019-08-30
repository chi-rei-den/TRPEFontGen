using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace TRPEFontGen
{
    public class Reader
    {
        public Reader()
        {
            
        }

        public FontFile Read(string path, string texturePath = "")
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not exist!: {path}");
                return null;
            }

            using (var fs = new FileStream(path, FileMode.Open))
            {
                using (var br = new BinaryReader(fs, Encoding.Unicode))
                {
                    var charCount = br.ReadInt32();
                    if (charCount < 1)
                    {
                        Console.WriteLine($"Char count cannot less than 1!: {charCount}");
                        return null;
                    }

                    var fontFile = new FontFile(charCount);

                    for (int i = 0; i < charCount; i++)
                    {
                        fontFile.Chars.Add(new FontChar
                        {
                            Glyph = br.ReadRectangle(),
                            Cropping = br.ReadRectangle(),
                            Char = br.ReadChar(),
                            Kerning = br.ReadVector3(),
                        });
                    }

                    fontFile.LineSpacing = br.ReadInt32();
                    fontFile.Spacing = br.ReadSingle();

                    if (br.ReadBoolean())
                    {
                        fontFile.DefaultChar = br.ReadChar();
                        fontFile.HasDefaultChar = true;
                    }
                    else
                    {
                        fontFile.HasDefaultChar = false;
                    }

                    if (!string.IsNullOrWhiteSpace(texturePath))
                        fontFile.Texture = ReadTexture(texturePath);

                    return fontFile;
                }
            }
            
            
        }

        public void PrintChars(FontFile fontFile, string dirPath)
        {
            if(fontFile.Texture == null)
                return;

            foreach (var c in fontFile.Chars)
            {
                try
                {
                    var bitmap = new Bitmap(c.Glyph.Width, c.Glyph.Height);
                    using (var g = Graphics.FromImage(bitmap))
                    {
                        g.DrawImage(fontFile.Texture, new RectangleF(0, 0, c.Glyph.Width, c.Glyph.Height),
                            c.Glyph.Convert(), GraphicsUnit.Pixel);
                        
                        var path = $"{dirPath}/{c.Char}.png";
                        if (Path.GetInvalidPathChars().Contains(c.Char))
                        {
                            path = $"{dirPath}/{CharToUnicode(c.Char)}.png";
                        }
                        
                        bitmap.Save(path, ImageFormat.Png);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static string CharToUnicode(char c)
        {
            return $"\\u{Encoding.Unicode.GetBytes(c.ToString()):X2}";
        }

        private Image ReadTexture(string path)
        {
            if (!File.Exists(path))
                return null;
            return Bitmap.FromFile(path);
        }
    }
}
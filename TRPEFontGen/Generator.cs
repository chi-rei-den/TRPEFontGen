using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Microsoft.Xna.Framework;

using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace TRPEFontGen
{
    public class Generator
    {
        public Generator()
        {
            
        }

        public FontFile Generate(Font font, int size, char[] chars, int minusKerning = 5, 
            int compressSize = 2, int lineSpacing = 24, float spacing = 0, char defaultChar = '*')
        {
            var fontFile = new FontFile(chars.Length);
            fontFile.LineSpacing = lineSpacing;
            fontFile.Spacing = spacing;
            fontFile.HasDefaultChar = true;
            fontFile.DefaultChar = '*';
            
            Bitmap png = new Bitmap(size, size);
            using (var g = Graphics.FromImage(png))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                float xNow = 1;
                float yNow = 1;
                float yMax = 0;
                int count = 0;
                foreach (var c in chars)
                {
                    var charRect = g.MeasureString(c.ToString(), font);
                    charRect.Width -= compressSize;
                    charRect.Height -= compressSize;
                
                    if (charRect.Height > yMax)
                        yMax = charRect.Height;
                    if (xNow + charRect.Width + 10 > size)
                    {
                        yNow += yMax;
                        yMax = 0;
                        xNow = 1;
                    }
                    if(yNow > size)
                        Console.WriteLine(count);
                
                    g.DrawString(c.ToString(), font, Brushes.White, 
                        new RectangleF((int)xNow, (int)yNow, (int)charRect.Width, (int)charRect.Height));
                
                    var fontChar = new FontChar
                    {
                        Char = c,
                        Cropping = new Rectangle(0, 0, (int)charRect.Width, (int)charRect.Height+2),
                        Glyph = new Rectangle((int)xNow, (int)yNow, (int)charRect.Width, (int)charRect.Height),
                        Kerning = new Vector3(0, charRect.Width - minusKerning, 0)
                    };
                    fontFile.Chars.Add(fontChar);
                
                    xNow += charRect.Width;

                    count++;
                }

                fontFile.Texture = png;
            
                return fontFile;
            }
        }
    }
}
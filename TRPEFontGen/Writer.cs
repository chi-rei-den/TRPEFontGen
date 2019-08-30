using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace TRPEFontGen
{
    public class Writer
    {
        public Writer()
        {
            
        }

        public void Write(string path, FontFile fontFile)
        {
            using (var fs = new FileStream(path, FileMode.Create))
            {
                using (var bw = new BinaryWriter(fs, Encoding.Unicode))
                {
                    bw.Write(fontFile.CharCount);

                    foreach (var c in fontFile.Chars)
                    {
                        bw.Write(c.Glyph);
                        bw.Write(c.Cropping);
                        bw.Write(c.Char);
                        bw.Write(c.Kerning);
                    }
                    
                    bw.Write(fontFile.LineSpacing);
                    bw.Write(fontFile.Spacing);
                    bw.Write(fontFile.HasDefaultChar);
                    if(fontFile.HasDefaultChar)
                        bw.Write(fontFile.DefaultChar);
                }
            }
            
            if(fontFile.Texture == null)
                return;
            
            fontFile.Texture.Save(path + ".png", ImageFormat.Png);
        }
    }
}
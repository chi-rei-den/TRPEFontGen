using System.Collections.Generic;
using System.Drawing;
using Microsoft.Xna.Framework;

namespace TRPEFontGen
{
    public class FontFile
    {
        public int CharCount { get; set; }
        
        public int LineSpacing { get; set; }
        
        public float Spacing { get; set; }
        
        public bool HasDefaultChar { get; set; }
        
        public char DefaultChar { get; set; }
        
        public List<FontChar> Chars { get; set; }
        
        public Image Texture { get; set; }

        public FontFile(int charCount)
        {
            CharCount = charCount;
            
            Chars = new List<FontChar>();
        }
    }
}
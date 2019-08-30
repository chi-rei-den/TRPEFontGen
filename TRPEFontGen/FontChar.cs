using Microsoft.Xna.Framework;

namespace TRPEFontGen
{
    public class FontChar
    {
        public Rectangle Glyph { get; set; }
        public Rectangle Cropping { get; set; }
        public char Char { get; set; }
        public Vector3 Kerning { get; set; }
    }
}
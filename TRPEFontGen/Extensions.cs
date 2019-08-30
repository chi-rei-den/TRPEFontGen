using System.Drawing;
using System.IO;
using Microsoft.Xna.Framework;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace TRPEFontGen
{
    public static class Extensions
    {
        public static Rectangle ReadRectangle(this BinaryReader br)
        {
            var x = br.ReadInt32();
            var y = br.ReadInt32();
            var w = br.ReadInt32();
            var h = br.ReadInt32();
            
            return new Rectangle(x, y, w, h);
        }
        
        public static Vector3 ReadVector3(this BinaryReader br)
        {
            var x = br.ReadSingle();
            var y = br.ReadSingle();
            var z = br.ReadSingle();
            
            return new Vector3(x, y, z);
        }
        
        public static void Write(this BinaryWriter bw, Rectangle rect)
        {
            bw.Write(rect.X);
            bw.Write(rect.Y);
            bw.Write(rect.Width);
            bw.Write(rect.Height);
        }
        
        public static void Write(this BinaryWriter bw, Vector3 vec)
        {
            bw.Write(vec.X);
            bw.Write(vec.Y);
            bw.Write(vec.Z);
        }

        public static Rectangle Convert(this RectangleF rect)
        {
            return new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }

        public static RectangleF Convert(this Rectangle rect)
        {
            return new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
        }
    }
}
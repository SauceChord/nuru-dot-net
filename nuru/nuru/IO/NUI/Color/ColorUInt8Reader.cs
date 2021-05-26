using BigEndian.IO;

namespace nuru.IO.NUI
{
    public class ColorUInt8Reader : IColorReader
    {
        public virtual Color Read(BigEndianBinaryReader reader)
        {
            byte foreground = reader.ReadByte();
            byte background = reader.ReadByte();
            return new Color(foreground, background);
        }
    }
}

using BigEndian.IO;

namespace nuru.IO.NUI
{
    public class ColorUInt8Reader : IColorReader
    {
        public virtual NUIColor Read(BigEndianBinaryReader reader)
        {
            byte foreground = reader.ReadByte();
            byte background = reader.ReadByte();
            return new NUIColor(foreground, background);
        }
    }
}

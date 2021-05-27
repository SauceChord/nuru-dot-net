using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Color
{
    public class ColorUInt8Reader : IColorReader
    {
        public virtual ColorData Read(BigEndianBinaryReader reader)
        {
            byte foreground = reader.ReadByte();
            byte background = reader.ReadByte();
            return new ColorData(foreground, background);
        }
    }
}

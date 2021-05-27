using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Color
{
    public class ColorUInt4Reader : IColorReader
    {
        public virtual ColorData Read(BigEndianBinaryReader reader)
        {
            int b = reader.ReadByte();
            return new ColorData((byte)(b >> 4), (byte)(b & 0xf));
        }
    }
}

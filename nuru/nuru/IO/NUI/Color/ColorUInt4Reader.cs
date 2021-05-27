using BigEndian.IO;

namespace nuru.IO.NUI
{
    public class ColorUInt4Reader : IColorReader
    {
        public virtual NUIColor Read(BigEndianBinaryReader reader)
        {
            int b = reader.ReadByte();
            return new NUIColor((byte)(b >> 4), (byte)(b & 0xf));
        }
    }
}

using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Color
{
    public class ColorUInt4Writer : IColorWriter
    {
        public virtual void Write(BigEndianBinaryWriter writer, ColorData pair)
        {
            writer.Write((byte)((pair.Foreground << 4) | (pair.Background & 0x0f)));
        }
    }
}

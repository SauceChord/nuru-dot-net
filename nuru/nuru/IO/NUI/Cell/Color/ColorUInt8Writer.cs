using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Color
{
    public class ColorUInt8Writer : IColorWriter
    {
        public virtual void Write(BigEndianBinaryWriter writer, ColorData pair)
        {
            writer.Write(pair.Foreground);
            writer.Write(pair.Background);
        }
    }
}

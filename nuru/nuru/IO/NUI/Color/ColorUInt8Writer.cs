using BigEndian.IO;

namespace nuru.IO.NUI
{
    public class ColorUInt8Writer : IColorWriter
    {
        public virtual void Write(BigEndianBinaryWriter writer, NUIColor pair)
        {
            writer.Write(pair.Foreground);
            writer.Write(pair.Background);
        }
    }
}

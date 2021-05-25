using BigEndian.IO;

namespace nuru.NUI.Writers
{
    public class ColorPairUInt8Writer : IColorPairWriter
    {
        public void Write(BigEndianBinaryWriter writer, ColorPair pair)
        {
            writer.Write(pair.Background); // big endian low byte
            writer.Write(pair.Foreground); // big endian high byte
        }
    }
}

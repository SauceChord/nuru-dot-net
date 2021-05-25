using System.IO;

namespace nuru.NUI.Writers
{
    public class ColorPairUInt8Writer : WriterBase, IColorPairWriter
    {
        public ColorPairUInt8Writer(Stream stream) : base(stream)
        {
        }

        public void Write(ColorPair pair)
        {
            writer.Write(pair.Background); // big endian low byte
            writer.Write(pair.Foreground); // big endian high byte
        }
    }
}

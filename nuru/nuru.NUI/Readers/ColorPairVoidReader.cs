using BigEndian.IO;

namespace nuru.NUI.Readers
{
    public class ColorPairVoidReader : IColorPairReader
    {
        public ColorPair Read(BigEndianBinaryReader reader)
        {
            return default;
        }
    }
}

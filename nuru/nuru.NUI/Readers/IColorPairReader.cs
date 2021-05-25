using BigEndian.IO;

namespace nuru.NUI.Readers
{
    public interface IColorPairReader
    {
        ColorPair Read(BigEndianBinaryReader reader);
    }
}

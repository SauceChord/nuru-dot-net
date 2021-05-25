using BigEndian.IO;

namespace nuru.NUI.Writers
{
    public interface IColorPairWriter
    {
        void Write(BigEndianBinaryWriter writer, ColorPair pair);
    }
}

using BigEndian.IO;

namespace nuru.IO.NUI
{
    public interface IColorWriter
    {
        void Write(BigEndianBinaryWriter writer, NUIColor pair);
    }
}

using BigEndian.IO;

namespace nuru.IO.NUI
{
    public interface IColorReader
    {
        Color Read(BigEndianBinaryReader reader);
    }
}

using BigEndian.IO;

namespace nuru.IO.NUI
{
    public interface IColorReader
    {
        NUIColor Read(BigEndianBinaryReader reader);
    }
}

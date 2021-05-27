using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Color
{
    public interface IColorReader
    {
        ColorData Read(BigEndianBinaryReader reader);
    }
}

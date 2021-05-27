using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Color
{
    public interface IColorWriter
    {
        void Write(BigEndianBinaryWriter writer, ColorData pair);
    }
}

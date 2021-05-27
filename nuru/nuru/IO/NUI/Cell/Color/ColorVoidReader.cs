using BigEndian.IO;

namespace nuru.IO.NUI.Cell.Color
{
    public class ColorVoidReader : IColorReader
    {
        public virtual ColorData Read(BigEndianBinaryReader reader)
        {
            return default;
        }
    }
}

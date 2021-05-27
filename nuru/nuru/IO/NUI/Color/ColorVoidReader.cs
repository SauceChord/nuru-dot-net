using BigEndian.IO;

namespace nuru.IO.NUI
{
    public class ColorVoidReader : IColorReader
    {
        public virtual NUIColor Read(BigEndianBinaryReader reader)
        {
            return default;
        }
    }
}

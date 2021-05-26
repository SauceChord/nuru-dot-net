using BigEndian.IO;

namespace nuru.IO.NUI
{
    public class ColorVoidReader : IColorReader
    {
        public virtual Color Read(BigEndianBinaryReader reader)
        {
            return default;
        }
    }
}

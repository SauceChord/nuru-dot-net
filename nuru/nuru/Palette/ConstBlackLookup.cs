namespace nuru.Palette
{
    public class ConstBlackLookup : IRGBLookup
    {
        public RGB LookupRGB(byte index)
        {
            return new RGB();
        }
    }
}

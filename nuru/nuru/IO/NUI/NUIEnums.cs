namespace nuru.IO.NUI
{
    public enum GlyphMode : byte
    {
        None = 0,
        ASCII = 1,
        UTF16 = 2,
        Palette = 129,
    }

    public enum ColorMode : byte
    {
        None = 0,
        FourBit = 1,
        EightBit = 2,
        Palette = 130,
    }

    public enum MetadataMode : byte
    {
        None = 0,
        EightBit = 1,
        SixteenBit = 2,
    }
}

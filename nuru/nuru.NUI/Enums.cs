namespace nuru.NUI
{
    public enum GlyphMode : byte
    {
        None = 0,
        Ascii = 1,
        Unicode = 2,
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
        OneByte = 1,
        TwoBytes = 2,
    }
}

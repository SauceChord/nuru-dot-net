namespace nuru.Palette
{
    public class NoGlyphLookup : IGlyphLookup
    {
        public char LookupGlyph(char index)
        {
            return index;
        }
    }
}

namespace nuru
{
    public interface IImage
    {
        ushort Width { get; }
        ushort Height { get; }

        RGB GetForeground(int x, int y);
        RGB GetBackground(int x, int y);
        char GetGlyph(int x, int y);
        ushort GetMetadata(int x, int y);
    }
}

using nuru.IO.NUI;
using nuru.IO.NUP;
using nuru.Palette;
using System;
using System.Collections.Generic;
using System.Text;

namespace nuru
{
    public class FileImage : IImage
    {
        readonly NUIFile nuiFile;
        readonly NUPFile nupGlyphFile;
        readonly NUPFile nupColorFile;

        readonly IRGBLookup rgbLookup;
        readonly IGlyphLookup glyphLookup;

        public FileImage(NUIFile nuiFile, NUPFile nupGlyphFile = null, NUPFile nupColorFile = null)
        {
            this.nuiFile = nuiFile;
            this.nupGlyphFile = nupGlyphFile;
            this.nupColorFile = nupColorFile;

            switch (nuiFile.Header.ColorMode)
            {
                case ColorMode.None:
                    rgbLookup = new ConstBlackLookup();
                    break;
                case ColorMode.FourBit:
                    rgbLookup = new ConstANSI4Lookup();
                    break;
                case ColorMode.EightBit:
                    rgbLookup = new ConstANSI8Lookup();
                    break;
                case ColorMode.Palette:
                    switch (nupColorFile.Header.Type)
                    {
                        case PaletteType.ANSI8:
                            rgbLookup = new FileANSI8Lookup(nupColorFile);
                            break;
                        case PaletteType.RGB:
                            rgbLookup = new FileRGBLookup(nupColorFile);
                            break;
                        case PaletteType.Unicode:
                        case PaletteType.None:
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            switch (nuiFile.Header.GlyphMode)
            {
                case GlyphMode.None:
                case GlyphMode.ASCII:
                case GlyphMode.UTF16:
                    glyphLookup = new NoGlyphLookup();
                    break;
                case GlyphMode.Palette:
                    glyphLookup = new FileGlyphLookup(nupGlyphFile);
                    break;
                default:
                    break;
            }
        }

        public ushort Width { get { return nuiFile.Header.Width; } }
        public ushort Height { get { return nuiFile.Header.Height; } }
        public RGB GetForeground(int x, int y)
        {
            var cell = nuiFile.Payload[x, y];
            return rgbLookup.LookupRGB(cell.Color.Foreground);
        }

        public RGB GetBackground(int x, int y)
        {
            var cell = nuiFile.Payload[x, y];
            return rgbLookup.LookupRGB(cell.Color.Background);
        }

        public char GetGlyph(int x, int y)
        {
            var cell = nuiFile.Payload[x, y];
            return glyphLookup.LookupGlyph(cell.Glyph);
        }

        public ushort GetMetadata(int x, int y)
        {
            var cell = nuiFile.Payload[x, y];
            return cell.Metadata;
        }
    }
}

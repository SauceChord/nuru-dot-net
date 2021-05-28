using NUnit.Framework;
using System;

namespace nuru.IO
{
    public class PaletteFileTests
    {
        [Test]
        public void Constructor_GivenValidPaletteType_InitializesFields()
        {
            PaletteFile file = new PaletteFile(PaletteType.RGB);

            Assert.That(file.PaletteType, Is.EqualTo(PaletteType.RGB));
            Assert.That(file.KeyChar, Is.EqualTo(' '));
            Assert.That(file.KeyFore, Is.EqualTo(15));
            Assert.That(file.KeyBack, Is.EqualTo(0));
            Assert.That(file.Userdata, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_GivenInvalidPaletteType_Throws()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                    () => new PaletteFile((PaletteType)255)
                );
            Assert.That(exception.ParamName, Is.EqualTo("paletteType"));
        }

        [Test]
        public void GetANSI8_GivenCallToSetANSI8_ReturnsSameValue()
        {
            PaletteFile file = new PaletteFile(PaletteType.ANSI8);
            file.SetANSI8(0, 1);
            file.SetANSI8(255, 2);

            Assert.That(file.GetANSI8(0), Is.EqualTo(1));
            Assert.That(file.GetANSI8(255), Is.EqualTo(2));
        }

        [Test]
        public void GetChar_GivenCallToSetChar_ReturnsSameValue()
        {
            PaletteFile file = new PaletteFile(PaletteType.Unicode);
            file.SetChar(0, 'A');
            file.SetChar(255, 'B');
            file.SetChar(1, 'Ɠ');

            Assert.That(file.GetChar(0), Is.EqualTo('A'));
            Assert.That(file.GetChar(255), Is.EqualTo('B'));
            Assert.That(file.GetChar(1), Is.EqualTo('Ɠ'));
        }

        [Test]
        public void GetRGB_GivenCallToSetRGB_ReturnsSameValue()
        {
            PaletteFile file = new PaletteFile(PaletteType.RGB);
            file.SetRGB(0, new RGB(1, 2, 3));
            file.SetRGB(255, new RGB(4, 5, 6));

            Assert.That(file.GetRGB(0), Is.EqualTo(new RGB(1, 2, 3)));
            Assert.That(file.GetRGB(255), Is.EqualTo(new RGB(4, 5, 6)));
        }

        [Test]
        public void GetANSI8_GivenNonANSI8PaletteType_Throws()
        {
            PaletteFile file = new PaletteFile(PaletteType.Unicode);
            Assert.Throws<InvalidOperationException>(
                    () => file.GetANSI8(0)
                );
        }

        [Test]
        public void SetANSI8_GivenNonANSI8PaletteType_Throws()
        {
            PaletteFile file = new PaletteFile(PaletteType.Unicode);
            Assert.Throws<InvalidOperationException>(
                    () => file.SetANSI8(0, 0)
                );
        }

        [Test]
        public void GetChar_GivenNonUnicodePaletteType_Throws()
        {
            PaletteFile file = new PaletteFile(PaletteType.ANSI8);
            Assert.Throws<InvalidOperationException>(
                    () => file.GetChar(0)
                );
        }

        [Test]
        public void SetChar_GivenNonUnicodePaletteType_Throws()
        {
            PaletteFile file = new PaletteFile(PaletteType.ANSI8);
            Assert.Throws<InvalidOperationException>(
                    () => file.SetChar(0, 'A')
                );
        }

        [Test]
        public void GetRGB_GivenNonRGBPaletteType_Throws()
        {
            PaletteFile file = new PaletteFile(PaletteType.ANSI8);
            Assert.Throws<InvalidOperationException>(
                    () => file.GetRGB(0)
                );
        }

        [Test]
        public void SetRGB_GivenNonRGBPaletteType_Throws()
        {
            PaletteFile file = new PaletteFile(PaletteType.ANSI8);
            Assert.Throws<InvalidOperationException>(
                    () => file.SetRGB(0, new RGB())
                );
        }
    }
}
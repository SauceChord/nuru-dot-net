﻿using NUnit.Framework;
using nuru.NUI.Writers;

namespace nuru.NUI.Tests.Writers
{
    public class GlyphWritersTests : ReadWriteBaseTests
    {
        protected IGlyphWriter voidWriter;
        protected IGlyphWriter asciiWriter;
        protected IGlyphWriter unicodeWriter;

        public override void Setup()
        {
            base.Setup();
            voidWriter = new GlyphVoidWriter();
            asciiWriter = new GlyphASCIIWriter(stream);
            unicodeWriter = new GlyphUnicodeWriter(stream);
        }

        [Test]
        public void TestWriteVoidDoesNothing()
        {
            voidWriter.Write('A');
        }

        [TestCase(' ', ExpectedResult = 0x20)]
        [TestCase('A', ExpectedResult = 0x41)]
        [TestCase('ÿ', ExpectedResult = 0xff)]
        public byte TestWriteASCII(char testCase)
        {
            asciiWriter.Write(testCase);
            RewindStream();
            Assert.That(stream.Length, Is.EqualTo(1));
            return reader.ReadByte();
        }
       
        [TestCase(' ', ExpectedResult = 0x2000)]
        [TestCase('A', ExpectedResult = 0x4100)]
        [TestCase('ÿ', ExpectedResult = 0xff00)]
        [TestCase('Ā', ExpectedResult = 0x0001)]
        [TestCase('ℇ', ExpectedResult = 0x0721)]
        public ushort TestWriteUnicode(char testCase)
        {
            unicodeWriter.Write(testCase);
            RewindStream();
            Assert.That(stream.Length, Is.EqualTo(2));
            return reader.ReadUInt16();
        }
    }
}
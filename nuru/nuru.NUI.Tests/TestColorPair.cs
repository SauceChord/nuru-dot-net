using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace nuru.NUI.Tests
{
    public class TestColorPair
    {
        [TestCase(0, 1, ExpectedResult = "0, 1")]
        [TestCase(255, 0, ExpectedResult = "255, 0")]
        [TestCase(125, 200, ExpectedResult = "125, 200")]
        public string TestToString(int fg, int bg)
        {
            return new ColorPair((byte)fg, (byte)bg).ToString();
        }
    }
}

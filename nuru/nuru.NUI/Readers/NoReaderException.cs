using System;
using System.Collections.Generic;
using System.Text;

namespace nuru.NUI.Readers
{
    public class NoReaderException : InvalidOperationException
    {
        public NoReaderException(string message) : base(message)
        {
        }
    }
}

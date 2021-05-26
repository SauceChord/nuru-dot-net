﻿using System;

namespace nuru.IO
{
    public class FormatException : Exception
    {
        public FormatException(string message)
            : base(message)
        {
        }

        public FormatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

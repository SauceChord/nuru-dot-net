using System;

namespace nuru.NUI
{
    public class ImageLoadException : Exception
    {
        public ImageLoadException(string message)
            : base(message)
        {
        }

        public ImageLoadException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

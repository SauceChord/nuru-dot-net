using System;

namespace nuru.IO.NUI.Cell
{
    public class CellFactoryException : InvalidOperationException
    {
        public CellFactoryException(string message) : base(message)
        {
        }
    }
}

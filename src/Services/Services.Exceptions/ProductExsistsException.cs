using System;

namespace Services.Exceptions
{
    public class ProductExsistsException : Exception
    {
        public ProductExsistsException(string message)
            :base(message)
        {
        }
    }
}

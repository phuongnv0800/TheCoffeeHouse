using System;

namespace Settings.Error
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }
    }
}

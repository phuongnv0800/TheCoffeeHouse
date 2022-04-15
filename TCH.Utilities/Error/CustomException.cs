using System;

namespace TCH.Utilities.Error;

public class CustomException : Exception
{
    public CustomException(string message) : base(message)
    {
    }
}

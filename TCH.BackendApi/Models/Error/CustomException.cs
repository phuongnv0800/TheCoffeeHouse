using System;

namespace TCH.BackendApi.Models.Error
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }
    }
}

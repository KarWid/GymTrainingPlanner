namespace GymTrainingPlanner.Common.Exceptions
{
    using System;
    using System.Collections.Generic;

    public class BaseApiException : Exception
    {
        public BaseApiException(string message) : base(message)
        {
        }

        public virtual string GetErrorMessage()
        {
            return Message;
        }

        public virtual List<string> GetErrorMessages()
        {
            return new List<string> { Message };
        }
    }
}

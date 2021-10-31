namespace GymTrainingPlanner.Common.Exceptions
{
    using System;

    public class BaseApiException : Exception
    {
        public BaseApiException(string message) : base(message)
        {
        }

        public virtual string GetErrorMessage()
        {
            return Message;
        }
    }
}

namespace Manufacturer.Project.Core.Base
{
    public class RejectOperationException : Exception
    {
        public int ErrorCode { get; set; }

        public string UserMessage { get; set; }

        public RejectOperationException(string userMessage, int errorCode = 0)
        {
            ErrorCode = errorCode;
            UserMessage = userMessage;
        }

        public RejectOperationException(string message, int errorCode, string userMessage)
            : base(message)
        {
            ErrorCode = errorCode;
            UserMessage = userMessage;
        }
    }
}
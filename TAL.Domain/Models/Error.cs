using System;
using System.Collections.Generic;
using System.Text;

namespace TAL.Domain.Models
{
    public class Error
    {
        public string Subject { get; }
        public string Message { get; }
        public ErrorType ErrorType { get; }
        public Exception Exception { get; }

        private Error(string subject, string message, ErrorType errorType, Exception exception)
        {
            Subject = subject;
            Message = message;
            ErrorType = errorType;
            Exception = exception;
        }

        public static Error CreateFrom(string subject, ErrorType type, string message = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = DefaultMessage(type);
            }

            return new Error(subject, message, type, null);
        }

        public static Error CreateFrom(string subject, Exception exception)
        {
            return new Error(subject, exception.Message, ErrorType.InternalServerError, exception);
        }

        public static string DefaultMessage(ErrorType type)
        {
            return type.Title;
        }
    }
}

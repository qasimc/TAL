using System;
using System.Collections.Generic;
using System.Text;

namespace TAL.Domain.Models
{
    public static class Result
    {
        public static ResultValue<T> Ok<T>(T value)
        {
            return new ResultValue<T>(value);
        }

        public static ResultValue<T> Ok<T>(T value, byte[] timestamp)
        {
            return new ResultValue<T>(value, timestamp);
        }

        public static ResultValue<T> Ok<T>(byte[] timestamp)
        {
            return new ResultValue<T>(timestamp);
        }

        public static ResultValue<T> Ok<T>()
        {
            return new ResultValue<T>();
        }

        public static ResultValue<T> Failed<T>(IEnumerable<Error> errors)
        {
            return new ResultValue<T>(errors);
        }

        public static ResultValue<T> Failed<T>(Error error)
        {
            return new ResultValue<T>(new List<Error> { error });
        }
    }
}

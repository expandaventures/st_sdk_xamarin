using System;
using System.Net;

namespace SinTrafico
{
    public class Response<T>
    {
        internal Response(T result, HttpStatusCode statusCode, string errorMessage = null)
        {
            Result = result;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public T Result { get; }

        public HttpStatusCode StatusCode { get; }

        public string ErrorMessage { get; }
    }
}

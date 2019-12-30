using System;
using System.Net;
using HealthyWayOfLife.Model.Enums;

namespace HealthyWayOfLife.Model.Model
{
    public class CustomCodeException : Exception
    {
        public LogType LogType { get; set; }
        public HttpStatusCode StatusCode { get; }
        public string ExceptionString { get; }

        public CustomCodeException(HttpStatusCode statusCode, string exceptionString, LogType logType = LogType.Message)
        {
            StatusCode = statusCode;
            ExceptionString = exceptionString;
            LogType = logType;
        }
        public CustomCodeException(string exceptionString, LogType logType = LogType.Message)
        {
            StatusCode = HttpStatusCode.NotAcceptable;
            ExceptionString = exceptionString;
            LogType = logType;
        }
    }
}
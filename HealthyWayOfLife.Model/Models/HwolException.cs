using System;
using System.Net;
using HealthyWayOfLife.Model.Enums;

namespace HealthyWayOfLife.Model.Models
{
    public class HwolException : Exception
    {
        public LogType LogType { get; set; }
        public HttpStatusCode StatusCode { get; }
        public string UserInfo { get; }

        public HwolException(HttpStatusCode statusCode, string userInfo, LogType logType = LogType.Message, Exception e = null) : base( e?.Message, e )
        {
            StatusCode = statusCode;
            UserInfo = userInfo ?? "Some error occured, try again please";
            LogType = logType;
        }
        public HwolException(string userInfo, LogType logType = LogType.Message, Exception e = null) : base(e?.Message, e)
        {
            StatusCode = HttpStatusCode.NotAcceptable;
            UserInfo = userInfo ?? "Some error occured, try again please";
            LogType = logType;
        }
    }
}
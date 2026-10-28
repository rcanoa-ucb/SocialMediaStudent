using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SocialMedia.Core.Exceptions
{
    public class BussinesException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public object? Details { get; }

        public BussinesException(string message, 
            HttpStatusCode statusCode = HttpStatusCode.BadRequest,
            object? details = null)
        {
            StatusCode = statusCode;
            Details = details;
        }
    }
}

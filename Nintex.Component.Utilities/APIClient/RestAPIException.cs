﻿
using Nintex.NetCore.Component.Utilities.ErrorHandling;
using System.Net;
using System.Net.Http;


namespace Nintex.NetCore.Component.Utilities.APIClient
{
    public class RestAPIException : HttpRequestException
    {
        public HttpStatusCode HttpStatus { private set; get; }
        public HttpExceptionDetails ExceptionDetails { private set; get; }
        public RestAPIException(string ServiceName, HttpStatusCode httpStatusCode, string ResponseAsString) : base(string.Format("Request to [{1}] faild with following response: {0}", ResponseAsString, ServiceName))
        {
            ExceptionDetails = new HttpExceptionDetails();
            if(httpStatusCode!=HttpStatusCode.BadRequest)
            {
                ExceptionDetails.StatusCode = (int)HttpStatusCode.InternalServerError;
                ExceptionDetails.ErrorMessage = "Internal Server Error";
            }
            else
            {
                ExceptionDetails.StatusCode = (int)HttpStatusCode.BadRequest;
                ExceptionDetails.ErrorMessage = "Bad Request";
            }
          

        }

    }
}

using System.Net;
using Common.ExceptionHandling.Exceptions;

namespace Common.ExceptionHanding.Exceptions
{
    public class BadRequestException : BaseException 
    {

        public BadRequestException(string message, string origin) : base(message, origin, HttpStatusCode.BadRequest) {
            this.message = message;
            this.origin = origin;
            this.code = HttpStatusCode.BadRequest;
        }
    }
}
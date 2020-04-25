using System.Net;
using Common.ExceptionHandling.Exceptions;

namespace Common.ExceptionHanding.Exceptions
{
    public class UnknownException : BaseException {

        public UnknownException(string message, string origin) : base(message, origin, HttpStatusCode.InternalServerError) {
            this.message = message;
            this.origin = origin;
            this.code = HttpStatusCode.InternalServerError;
        }

    }
}
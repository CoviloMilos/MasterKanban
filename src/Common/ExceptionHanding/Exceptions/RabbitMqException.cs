using System.Net;
using Common.ExceptionHandling.Exceptions;

namespace Common.ExceptionHanding.Exceptions
{
    public class RabbitMqException : BaseException {

        public RabbitMqException(string message) : base(message, "distribudetexceptionhandling", HttpStatusCode.InternalServerError) {
            this.message = message;
            this.code = HttpStatusCode.InternalServerError;
        }

    }
}
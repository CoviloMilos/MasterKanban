using System.Net;

namespace Common.Dto
{
    public class EntityDeletedSuccessfully
    {
        public string EntityType { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public EntityDeletedSuccessfully(string entityType, string message, HttpStatusCode statusCode)
        {
            EntityType = entityType;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
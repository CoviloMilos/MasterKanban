using System;
using Newtonsoft.Json;

namespace Common.Dto
{
    public class QueueExceptionDto
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public string RequestedUri { get; set; }
        public string Timestamp { get; set; }
        public string Origin { get; set; }

        public QueueExceptionDto DeserializeModel(String model)
        {
            return JsonConvert.DeserializeObject<QueueExceptionDto>(model);
        }

        public string SerializeModel()
        {
            return JsonConvert.SerializeObject (this);
        }
    }
}
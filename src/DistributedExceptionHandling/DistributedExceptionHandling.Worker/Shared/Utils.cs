using System;
using Common.Dto;
using DistributedExceptionHandling.Worker.Model;

namespace DistributedExceptionHandling.Worker.Shared
{
    public class Utils
    {
        public static ExceptionModel mapQueueExceptionRequestDtoToExceptionModel(QueueExceptionDto excetionRequestDto) 
        {
            ExceptionModel model = new ExceptionModel();
            model.Message = excetionRequestDto.Message;
            model.Origin = excetionRequestDto.Origin;
            model.RequestedUri = excetionRequestDto.RequestedUri;
            model.OcurredExceptionDate = DateTime.ParseExact(excetionRequestDto.Timestamp, "yyyy-MM-dd HH:mm tt", null);
            model.StatusCode = excetionRequestDto.Status;

            return model;
        }
    }
}
using System;
using Common.DistribudetExcetionHandling;
using Common.ExceptionHanding.Exceptions;
using KanbanManagement.API.Consts;
using KanbanManagement.API.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace KanbanManagement.API.Shared
{
    public static class Utils
    {
        public static AssignmentStatusEnum convertStringToAssigmentStatusEnum(string status) 
        {
            switch(status)
            {
                case "REQUESTED":
                    return AssignmentStatusEnum.REQUESTED;
                case "TODO":
                    return AssignmentStatusEnum.TODO;
                case "INPROGRESS":
                    return AssignmentStatusEnum.INPROGRESS;
                case "DONE":
                    return AssignmentStatusEnum.DONE; 
                default:
                    throw new BadRequestException("Invalid value of status field. Accepted valus: REQUESTED, TODO, INPROGRESS, DONE", 
                        DomainConsts.APPLICATION_NAME);
            }
        }

        public static string convertAssigmentStatusEnumToString(AssignmentStatusEnum status) 
        {
            switch(status)
            {
                case AssignmentStatusEnum.REQUESTED:
                    return "REQUESTED";
                case AssignmentStatusEnum.TODO:
                    return "TODO";
                case AssignmentStatusEnum.INPROGRESS:
                    return "INPROGRESS";
                default:
                    return "DONE"; 
            }
        }

        public static Guid checkGuidFormat(string guid)
        {
            try
            {
                return Guid.Parse(guid);
            }
            catch (System.Exception)
            { 
                throw new BadRequestException("Invaid request. Could not process id.", DomainConsts.APPLICATION_NAME);
            }
        }

        public static string extractRabbitMqConfigDataToString(RabbitMqOptions rabbitOpt) 
        {
            return $"amqp://{rabbitOpt.UserName}:{rabbitOpt.Password}@{rabbitOpt.HostName}:{rabbitOpt.Port}{rabbitOpt.VHost}";
        }
    }
}
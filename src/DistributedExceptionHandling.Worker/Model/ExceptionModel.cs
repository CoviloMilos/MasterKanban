using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace DistributedExceptionHandling.Worker.Model
{
    [Table("DistributedException")]
    public class ExceptionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string StatusCode { get; set; }
        [Required]
        public string RequestedUri { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public DateTime OcurredExceptionDate { get; set; }
        public DateTime QueuePulledExceptionDate { get; set; }

        public ExceptionModel() 
        {
            QueuePulledExceptionDate = DateTime.Now;
        }

        public static ExceptionModel DeserializeModel(String model)
        {
            return JsonConvert.DeserializeObject<ExceptionModel>(model);
        }
    }
}
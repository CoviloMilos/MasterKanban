using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KanbanManagement.API.Model
{
    [Table("Assignment")]
    public class Assignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DateOfCreation { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }
        [Required]
        public Guid AssignedTo { get; set; }
        [Required]
        public AssignmentStatusEnum Status { get; set; }
        public Project Project { get; set; }
        [Required]
        [ForeignKey("FK_Project_Id")]
        public Guid ProjectId { get; set; }

        public Assignment()
        {
            DateOfCreation = DateTime.Now;
        } 
    }
}
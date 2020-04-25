using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanManagement.API.Model
{
    [Table("Project")]
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime DateOfCreation { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }
        [NotMapped]
        [Required]
        public ICollection<Assignment> Assignments { get; set; }

        public Project()
        {
            Assignments = new Collection<Assignment>();
            DateOfCreation = DateTime.Now;
        }

    }
}
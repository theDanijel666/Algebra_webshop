using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ispit.Todo.Models
{
    public class Todolist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string UserId { get; set; }

        public string? Description { get; set; }


        [ForeignKey("TodolistId")]
        public virtual ICollection<Task> Tasks { get; set; }

    }
}

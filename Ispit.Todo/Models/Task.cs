using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ispit.Todo.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        public int TodolistId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string? Description { get; set; }

        public bool Status { get; set; }
    }
}

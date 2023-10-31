using Ispit.Todo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ispit.Todo.Data
{
    public class ApplicationUser: IdentityUser
    {
        [ForeignKey("UserId")]
        public virtual ICollection<Todolist> Todolist { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Todolist> Todolists { get; set; }

    }
}
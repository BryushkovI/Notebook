using System.Collections.Generic;
using AuthAppLib.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotebookContext.Model;

namespace NotebookContext
{
    public class NotebookContext : IdentityDbContext<User>
    {
        public NotebookContext(DbContextOptions<NotebookContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Contact> Contact { get; set; } = default!;
    }
}

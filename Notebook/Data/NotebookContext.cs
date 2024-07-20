using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Notebook.AuthApp;
using Notebook.Model;

namespace Notebook.Data
{
    public class NotebookContext : IdentityDbContext<User>
    {
        public NotebookContext (DbContextOptions<NotebookContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Contact> Contact { get; set; } = default!;
    }
}

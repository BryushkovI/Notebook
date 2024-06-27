using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notebook.Model;

namespace Notebook.Data
{
    public class NotebookContext : DbContext
    {
        public NotebookContext (DbContextOptions<NotebookContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Notebook.Model.Contact> Contact { get; set; } = default!;
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Models
{
    public class NoteModelContext : DbContext
    {
        public DbSet<NoteModel> Notes { get; set; }

        public NoteModelContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = NotesDB.db");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}

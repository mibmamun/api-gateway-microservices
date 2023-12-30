using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using NotebookApi.Models;

namespace NotebookApi.Data
{
    public class NotebookDbContext:DbContext
    {
        public NotebookDbContext(DbContextOptions<NotebookDbContext> options):base(options)
        {
            try
            {
                var databaseCreator= Database.GetService<IRelationalDatabaseCreator>();
                if(databaseCreator != null)
                {
                    if(!databaseCreator.CanConnect()) databaseCreator.Create();
                    if(!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public DbSet<Note> Notes { get; set; }
    }
}

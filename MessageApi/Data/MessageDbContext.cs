using MessageApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace MessageApi.Data
{
    public class MessageDbContext:DbContext
    {
        public MessageDbContext(DbContextOptions<MessageDbContext> options):base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IRelationalDatabaseCreator>();
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public DbSet<Message> Messages { get; set; }
    }
}

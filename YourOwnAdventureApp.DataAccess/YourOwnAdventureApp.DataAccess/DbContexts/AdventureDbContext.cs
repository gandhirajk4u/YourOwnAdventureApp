using Microsoft.EntityFrameworkCore;
using YourOwnAdventureApp.Models.Models;

namespace YourOwnAdventureApp.DataAccess.DbContexts
{
    public class AdventureDbContext : DbContext
    {
        public AdventureDbContext(DbContextOptions<AdventureDbContext> options) : base(options)
        {         
        }

        /// <summary>
        /// Gets or sets the collection of Adventure DB Model
        /// </summary>
        public DbSet<AdventureDbModel> tblAdventures { get; set; }

        /// <summary>
        /// Gets or sets the collection of Adventure User DB Model
        /// </summary>
        public DbSet<AdventureUserDbModel> tblAdventureUser { get; set; }
    }
}

using Bookcase.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace Bookcase.Data
{
    class ApplicationContext : DbContext
    {
        readonly IDbConnection? _dbConnection;
        public DbSet<Book> Books { get; set; } = null!;

        /// <summary>
        /// Configuration connection
        /// </summary>
        public ApplicationContext(IDbConnection connection)
        {
            _dbConnection = connection;
        }
        public ApplicationContext()
        {
            _dbConnection = null;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_dbConnection == null) optionsBuilder.UseSqlite("Data Source=Database.db");
            else optionsBuilder.UseSqlServer(_dbConnection.ConnectionString);
        }
        /// <summary>
        /// Configuration Entity
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(b => b.Author).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Book>().Property(b => b.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Book>().Property(b => b.Genre).IsRequired().HasMaxLength(50);


            base.OnModelCreating(modelBuilder);
        }

    }
}

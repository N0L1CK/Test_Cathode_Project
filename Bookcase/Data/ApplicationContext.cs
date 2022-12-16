using Bookcase.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace Bookcase.Data
{
    class ApplicationContext : DbContext
    {
        IDbConnection? dbConnection;
        public DbSet<Book> Books { get; set; } = null!;
        /// <summary>
        /// Configuration connection
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public ApplicationContext(IDbConnection connection) 
        {
            dbConnection = connection;
        }
        public ApplicationContext()
        {
            dbConnection = null;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (dbConnection == null) optionsBuilder.UseSqlite("Data Source=Database.db");
            else optionsBuilder.UseSqlServer(dbConnection.ConnectionString);
        }
        /// <summary>
        /// Configuration Entity
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(b=>b.Author).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Book>().Property(b => b.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Book>().Property(b => b.Genre).IsRequired().HasMaxLength(50);


            base.OnModelCreating(modelBuilder);
        }
        
    }
}

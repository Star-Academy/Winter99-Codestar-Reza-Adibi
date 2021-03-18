using Microsoft.EntityFrameworkCore;

namespace Project_05 {
    class SqlDatabaseContext : DbContext {
        private static readonly string connectionString = @"Server=localhost;Database=Codestar_Project05;Trusted_Connection=True;";
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Document> Documents { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

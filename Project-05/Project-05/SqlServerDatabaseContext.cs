using Microsoft.EntityFrameworkCore;

namespace Project_05 {
    public class SqlServerDatabaseContext : SqlDatabaseContext {
        private static readonly string connectionString = @"Server=localhost;Database=Codestar_Project05;Trusted_Connection=True;";
        public override DbSet<Token> Tokens { get; set; }
        public override DbSet<Document> Documents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

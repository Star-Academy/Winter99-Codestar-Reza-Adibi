using Microsoft.EntityFrameworkCore;

namespace Project_05 {
    public class SqliteDatabaseContext : SqlDatabaseContext {
        private static readonly string connectionString = "Data Source=\"E:\\Programing\\CodeStarWinter99\\Project-05\\TestData\\sqlitedb.db\"";
        public override DbSet<Token> Tokens { get; set; }
        public override DbSet<Document> Documents { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}

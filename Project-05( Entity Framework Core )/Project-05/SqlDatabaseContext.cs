using Microsoft.EntityFrameworkCore;

namespace Project_05 {
    public class SqlDatabaseContext : DbContext {
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Document> Documents { get; set; }

        public SqlDatabaseContext(DbContextOptions<SqlDatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Document>().
                HasIndex(doc => doc.DocumentPath).
                IsUnique();
            builder.Entity<Token>().
                HasIndex(tkn => tkn.TokenText).
                IsUnique();
            builder.Entity<Document>().HasMany(doc => doc.Tokens).WithMany(tkn => tkn.Documents);
        }
    }
}

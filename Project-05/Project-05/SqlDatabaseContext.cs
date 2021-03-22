using Microsoft.EntityFrameworkCore;

namespace Project_05 {
    public abstract class SqlDatabaseContext : DbContext {
        public abstract DbSet<Token> Tokens { get; set; }
        public abstract DbSet<Document> Documents { get; set; }
        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Document>()
              .HasIndex(doc => doc.DocumentPath)
                .IsUnique();
            builder.Entity<Token>()
              .HasIndex(tkn => tkn.TokenText)
                .IsUnique();
        }
    }
}

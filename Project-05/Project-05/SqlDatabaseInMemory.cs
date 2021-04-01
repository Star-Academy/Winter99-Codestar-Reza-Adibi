using Microsoft.EntityFrameworkCore;

namespace Project_05 {
    public class SqlDatabaseInMemory : SqlDatabase {
        public SqlDatabaseInMemory(string databaseName) {
            var dbConnectionString = databaseName;
            var options = new DbContextOptionsBuilder<SqlDatabaseContext>().UseInMemoryDatabase(dbConnectionString).Options;
            this.databaseContext = new SqlDatabaseContext(options);
            this.databaseContext.Database.EnsureCreated();
            this.saveOnInsert = true;
        }
    }
}

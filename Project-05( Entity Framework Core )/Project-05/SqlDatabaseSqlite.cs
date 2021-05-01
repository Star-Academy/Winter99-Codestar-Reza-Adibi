using Microsoft.EntityFrameworkCore;

namespace Project_05 {
    public class SqlDatabaseSqlite : SqlDatabase {
        public SqlDatabaseSqlite(string dataSource) {
            var dbConnectionString = "Data Source =\"" + dataSource + "\"";
            var options = new DbContextOptionsBuilder<SqlDatabaseContext>().UseSqlite(dbConnectionString).Options;
            this.databaseContext = new SqlDatabaseContext(options);
            this.databaseContext.Database.EnsureCreated();
            this.saveOnInsert = true;
        }
    }
}


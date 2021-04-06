using Microsoft.EntityFrameworkCore;

namespace Project_05 {
    class SqlDatabaseSqlServer : SqlDatabase {
        public SqlDatabaseSqlServer(string server, string database) {
            var dbConnectionString = @"Server=" + server + ";Database=" + database + ";Trusted_Connection=True;";
            var options = new DbContextOptionsBuilder<SqlDatabaseContext>().UseSqlServer(dbConnectionString).Options;
            this.databaseContext = new SqlDatabaseContext(options);
            this.databaseContext.Database.EnsureCreated();
            this.saveOnInsert = true;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Project_05 {
    public class SqlDatabase : ProgramDatabase {
        private readonly SqlDatabaseContext databaseContext;
        public SqlDatabase(DbmsName dbms, string dbConnectionString) {
            DbContextOptions<SqlDatabaseContext> options = null;
            switch (dbms) {
                case DbmsName.Sqlite: {
                        options =
                            new DbContextOptionsBuilder<SqlDatabaseContext>()
                            .UseSqlite(dbConnectionString)
                            .Options;
                        break;
                    }
                case DbmsName.SqlServer: {
                        options =
                            new DbContextOptionsBuilder<SqlDatabaseContext>()
                            .UseSqlServer(dbConnectionString)
                            .Options;
                        break;
                    }
                case DbmsName.Memory: {
                        options =
                            new DbContextOptionsBuilder<SqlDatabaseContext>()
                            .UseInMemoryDatabase(dbConnectionString)
                            .Options;
                        break;
                    }
            }
            this.databaseContext = new SqlDatabaseContext(options);
            this.databaseContext.Database.EnsureCreated();
        }
        public override void InsertData(string token, string documentID) {
            var newToken = GetOrCreateToken(token);
            var newDocument = GetOrCreateDocument(documentID);
            if (!databaseContext.Documents.Any(doc => doc == newDocument && doc.Tokens.Any(tkn => tkn == newToken))) {
                databaseContext.Documents.Find(newDocument.ID).Tokens.Add(newToken);
                databaseContext.SaveChanges();
            }
        }
        private Document GetOrCreateDocument(string documentPath) {
            Document document = databaseContext.Documents.FirstOrDefault(doc => doc.DocumentPath == documentPath);
            if (document == null) {
                document = new Document() { DocumentPath = documentPath, Tokens = new List<Token>() };
                databaseContext.Documents.Add(document);
                databaseContext.SaveChanges();
            }
            return document;
        }
        private Token GetOrCreateToken(string tokenText) {
            Token token = databaseContext.Tokens.FirstOrDefault(tkn => tkn.TokenText == tokenText);
            if (token == null) {
                token = new Token() { TokenText = tokenText, Documents = new List<Document>() };
                databaseContext.Tokens.Add(token);
                databaseContext.SaveChanges();
            }
            return token;
        }
        public override bool TryGetTokenDocumentIDs(string token, out List<string> output) {
            var theToken = databaseContext.Tokens.Include(tkn => tkn.Documents).Where(tkn => tkn.TokenText == token).FirstOrDefault();
            var tokenDocuments = theToken == null ? null : theToken.Documents;
            if (tokenDocuments == null) {
                output = new List<string>();
                return false;
            }
            else {
                output = tokenDocuments.Select((doc) => { return doc.DocumentPath; }).ToList();
                return true;
            }
        }
    }
}

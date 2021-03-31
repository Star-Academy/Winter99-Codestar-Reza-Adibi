using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_05 {
    public class SqlDatabase : IProgramDatabase {
        private readonly SqlDatabaseContext databaseContext;
        private bool saveOnInsert;
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
            this.saveOnInsert = true;
        }
        public void InsertDataList(List<Tuple<string, string>> data) {
            this.saveOnInsert = false;
            try {
                foreach (Tuple<string, string> pair in data)
                    InsertData(pair.Item2, pair.Item1);
                this.databaseContext.SaveChanges();
            }
            finally {
                this.saveOnInsert = true;
            }
        }
        public void InsertData(string token, string documentID) {
            var newToken = GetOrCreateToken(token);
            var newDocument = GetOrCreateDocument(documentID);
            if (!this.databaseContext.Documents.Any(doc => doc == newDocument && doc.Tokens.Any(tkn => tkn == newToken))) {
                this.databaseContext.Documents.Find(newDocument.ID).Tokens.Add(newToken);
            }
            if (this.saveOnInsert)
                this.databaseContext.SaveChanges();
        }
        private Document GetOrCreateDocument(string documentPath) {
            Document document = this.databaseContext.Documents.FirstOrDefault(doc => doc.DocumentPath == documentPath);
            if (document == null) {
                document = new Document() { DocumentPath = documentPath, Tokens = new List<Token>() };
                this.databaseContext.Documents.Add(document);
            }
            return document;
        }
        private Token GetOrCreateToken(string tokenText) {
            Token token = this.databaseContext.Tokens.FirstOrDefault(tkn => tkn.TokenText == tokenText);
            if (token == null) {
                token = new Token() { TokenText = tokenText, Documents = new List<Document>() };
                this.databaseContext.Tokens.Add(token);
            }
            return token;
        }
        public bool TryGetTokenDocumentIDs(string token, out List<string> output) {
            var theToken = this.databaseContext.Tokens.Include(tkn => tkn.Documents).Where(tkn => tkn.TokenText == token).FirstOrDefault();
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

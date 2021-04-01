using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_05 {
    public class SqlDatabase : IProgramDatabase {
        protected SqlDatabaseContext databaseContext;
        protected bool saveOnInsert;
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
            var document = this.databaseContext.Documents.FirstOrDefault(doc => doc.DocumentPath == documentPath);
            if (document != null) {
                return document;
            }
            var localDocument = this.databaseContext.Documents.Local.FirstOrDefault(doc => doc.DocumentPath == documentPath);
            if (localDocument != null) {
                return localDocument;
            }
            else {
                var newDocument = new Document() { DocumentPath = documentPath, Tokens = new List<Token>() };
                this.databaseContext.Documents.Add(newDocument);
                return newDocument;
            }
        }
        private Token GetOrCreateToken(string tokenText) {
            var token = this.databaseContext.Tokens.FirstOrDefault(tkn => tkn.TokenText == tokenText);
            if (token != null) {
                return token;
            }
            var localToken = this.databaseContext.Tokens.Local.FirstOrDefault(tkn => tkn.TokenText == tokenText);
            if (localToken != null) {
                return localToken;
            }
            else {
                var newToken = new Token() { TokenText = tokenText, Documents = new List<Document>() };
                this.databaseContext.Tokens.Add(newToken);
                return newToken;
            }
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

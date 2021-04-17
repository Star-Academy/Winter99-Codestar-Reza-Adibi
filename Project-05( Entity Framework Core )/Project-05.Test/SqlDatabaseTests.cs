using Microsoft.EntityFrameworkCore;
using Project_05;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Xunit;

namespace Project_05Test {
    [ExcludeFromCodeCoverage]
    public class SqlDatabaseTests : IDisposable {
        private readonly SqlDatabase database;
        private readonly SqlDatabaseContext localDatabaseContext;
        private bool disposedValue;

        public SqlDatabaseTests() {
            var databaseName = "inMemoryDatabase";
            database = new SqlDatabaseInMemory(databaseName);
            localDatabaseContext = new SqlDatabaseContext(
                new DbContextOptionsBuilder<SqlDatabaseContext>().
                UseInMemoryDatabase(databaseName).
                Options
            );
        }

        [Fact]
        public void InsertDataTestSingleData() {
            var expectedResult = new List<string> { "testFile1" };
            database.InsertData("testToken1", "testFile1");
            var theToken =
                localDatabaseContext.Tokens.Include(tkn => tkn.Documents).
                Where(tkn => tkn.TokenText == "testToken1").FirstOrDefault();
            var testResult =
                theToken == null ?
                null :
                theToken.Documents.Select((doc) => { return doc.DocumentPath; }).ToList();
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void InsertDataTestMultiSameData() {
            database.InsertData("testToken2", "testFile2");
            database.InsertData("testToken2", "testFile2");
            var tokenCount =
                localDatabaseContext.Tokens.Include(tkn => tkn.Documents).
                Where(tkn => tkn.TokenText == "testToken2").Count();
            Assert.Equal(1, tokenCount);
        }

        [Fact]
        public void InsertDataListTestSingleTokenMultiDocuments() {
            var expectedResult = new List<string> { "testFile3", "testFile4", "testFile5" };
            var dataList = new List<DocToken>{
                new DocToken { DocumentID = "testFile3", Token = "testToken3" },
                new DocToken { DocumentID = "testFile4", Token = "testToken3" },
                new DocToken { DocumentID = "testFile5", Token = "testToken3" }
            };
            database.InsertDataList(dataList);
            var theToken =
                localDatabaseContext.Tokens.Include(tkn => tkn.Documents).
                Where(tkn => tkn.TokenText == "testToken3").FirstOrDefault();
            var testResult =
                theToken == null ?
                null :
                theToken.Documents.Select((doc) => { return doc.DocumentPath; }).ToList();
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void InsertDataListTestMultiTokenSingleDocuments() {
            var expectedResult = new List<string> { "testToken4", "testToken5", "testToken6" };
            var dataList = new List<DocToken>{
                new DocToken { DocumentID = "testFile6", Token = "testToken4" },
                new DocToken { DocumentID = "testFile6", Token = "testToken5" },
                new DocToken { DocumentID = "testFile6", Token = "testToken6" }
            };
            database.InsertDataList(dataList);
            var theDocument =
                localDatabaseContext.Documents.Include(doc => doc.Tokens).
                Where(doc => doc.DocumentPath == "testFile6").FirstOrDefault();
            var testResult =
                theDocument == null ?
                null :
                theDocument.Tokens.Select((tkn) => { return tkn.TokenText; }).ToList();
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void TryGetTokenDocumentIDsTestInvertedIndexContainsToken() {
            var expectedResult = new List<string> { "testFile7" };
            var newToken = new Token { TokenText = "testToken7", Documents = new List<Document>() };
            var newDocument = new Document { DocumentPath = "testFile7", Tokens = new List<Token>() };
            newToken.Documents.Add(newDocument);
            localDatabaseContext.Documents.Add(newDocument);
            localDatabaseContext.Tokens.Add(newToken);
            localDatabaseContext.SaveChanges();
            database.TryGetTokenDocumentIDs("testToken7", out var testResult);
            Assert.Equal(expectedResult, testResult);
        }

        [Fact]
        public void TryGetTokenDocumentIDsTestInvertedIndexDoesNotContainsToken() {
            Assert.False(database.TryGetTokenDocumentIDs("bye", out var testResult));
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    localDatabaseContext.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~SqlDatabaseTests() {
            Dispose(disposing: false);
        }
    }
}

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
        private SqlDatabase database;
        private SqlDatabaseContext localDatabaseContext;
        private static Semaphore isRunningLock = new Semaphore(1, 1);
        private static int isRunning = 0;
        public SqlDatabaseTests() {
            isRunningLock.WaitOne();
            isRunning++;
            isRunningLock.Release();
            database = new SqlDatabase(DbmsName.Sqlite);
            localDatabaseContext = new SqliteDatabaseContext();
        }
        [Fact]
        public void InsertDatasTestSingleData() {
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
        public void InsertDatasTestMultySameData() {
            var expectedResult = new List<string> { "testFile1" };
            database.InsertData("testToken1", "testFile1");
            database.InsertData("testToken1", "testFile1");
            var tokenCount =
                localDatabaseContext.Tokens.Include(tkn => tkn.Documents).
                Where(tkn => tkn.TokenText == "testToken1").Count();
            Assert.Equal(1, tokenCount);
        }
        [Fact]
        public void TryGetTokenDocumentIDsTestInvertedIndexContainsToken() {
            var expectedResult = new List<string> { "testFile2" };
            var newToken = new Token { TokenText = "testToken2", Documents = new List<Document>() };
            var newDocument = new Document { DocumentPath = "testFile2", Tokens = new List<Token>() };
            newToken.Documents.Add(newDocument);
            localDatabaseContext.Documents.Add(newDocument);
            localDatabaseContext.Tokens.Add(newToken);
            localDatabaseContext.SaveChanges();
            List<string> testResult;
            database.TryGetTokenDocumentIDs("testToken2", out testResult);
            Assert.Equal(expectedResult, testResult);
        }
        [Fact]
        public void TryGetTokenDocumentIDsTestInvertedIndexDoesNotContainsToken() {
            Assert.False(database.TryGetTokenDocumentIDs("bye", out var testResult));
        }
        public void Dispose() {
            isRunningLock.WaitOne();
            isRunning--;
            isRunningLock.Release();
            if (isRunning == 0) {
                localDatabaseContext.Tokens.RemoveRange(localDatabaseContext.Tokens);
                localDatabaseContext.Documents.RemoveRange(localDatabaseContext.Documents);
                localDatabaseContext.SaveChanges();
            }
        }
        ~SqlDatabaseTests() {
            Dispose();
        }
    }
}

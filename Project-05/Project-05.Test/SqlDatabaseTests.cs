using Project_05;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Project_05Test {
    [ExcludeFromCodeCoverage]
    public class SqlDatabaseTests : IDisposable {
        //private SqlDatabase database;
        //public SqlDatabaseTests() {
        //    database = new SqlDatabase();
        //}
        //[Fact]
        //public void InsertDatasTestSingleData() {
        //    database.InsertData("testToken1", "testFile1");
        //    Assert.True(database.TryGetTokenDocumentIDs("testToken1", out var output));
        //}
        //[Fact]
        //public void GetTokenDocumentIDsTestInvertedIndexContainsToken() {
        //    var dataMap = new List<Tuple<string, string>> { new Tuple<string, string>("testFile2", "testToken2") };
        //    var expectedResult = new List<string> { "file1" };
        //    database.InsertDataList(dataMap);
        //    List<string> testResult;
        //    database.TryGetTokenDocumentIDs("hello", out testResult);
        //    Assert.Equal(expectedResult, testResult);
        //}
        //[Fact]
        //public void GetTokenDocumentIDsTestInvertedIndexDoesNotContainsToken() {
        //    var dataMap = new List<Tuple<string, string>> { new Tuple<string, string>("file1", "hello") };
        //    var expectedResult = new List<string> { "file1" };
        //    database.InsertDataList(dataMap);
        //    List<string> testResult;
        //    Assert.False(database.TryGetTokenDocumentIDs("bye", out testResult));
        //}
        //[Fact]
        //public void GetTokenDocumentIDsTestInvertedIndexIsEmpty() {
        //    var expectedResult = new List<string> { "file1" };
        //    List<string> testResult;
        //    Assert.False(database.TryGetTokenDocumentIDs("bye", out testResult));
        //}
        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}

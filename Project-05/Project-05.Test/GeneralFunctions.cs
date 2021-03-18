using Moq;
using Project_05;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Project_03Test {
    [ExcludeFromCodeCoverage]
    public class GeneralFunctions {
        /// <summary>
        /// Get mocked database and add new TryGetTokenDocumentIDs setup to it. 
        /// </summary>
        /// <param name="mockedDatabase"> The mockd database that we add new setup to it. </param>
        /// <param name="token"> The TryGetTokenDocumentIDs token input. </param>
        /// <param name="tokenDocumentIDs"> The TryGetTokenDocumentIDs output documentIDs. </param>
        /// <param name="returnValue"> The TryGetTokenDocumentIDs return value. </param>
        /// <returns> The mocked database with one more setup. </returns>
        internal static Mock<ProgramDatabase> SetupDatabaseTryGetTokenDocumentIDs(Mock<ProgramDatabase> mockedDatabase, string token, List<string> tokenDocumentIDs, bool returnValue) {
            mockedDatabase.Setup(db => db.TryGetTokenDocumentIDs(token, out tokenDocumentIDs)).Returns(returnValue);
            return mockedDatabase;
        }
    }
}

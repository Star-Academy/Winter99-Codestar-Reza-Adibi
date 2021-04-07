using Moq;
using Nest;
using System;
using System.Collections.Generic;
using Xunit;

namespace Libraries.Tests {
    public class ElasticIndexTests {
        private readonly Mock<IElasticClient> mockedClient;
        private readonly Mock<PingResponse> mockedPingResponse;
        public ElasticIndexTests() {
            mockedClient = new Mock<IElasticClient>();
            mockedPingResponse = new Mock<PingResponse>();
            mockedPingResponse.
                Setup(mockedResponse => mockedResponse.IsValid).
                Returns(true);
            mockedClient.
                Setup(mockedClient => mockedClient.Ping(It.IsAny<Func<PingDescriptor, IPingRequest>>())).
                Returns(mockedPingResponse.Object);
        }

        [Fact]
        public void ConstructorTestPingIsValid() {
            new TextDocumentIndex("test1", mockedClient.Object);
            Assert.True(true);
        }

        [Fact]
        public void ConstructorTestPingIsNotValid() {
            mockedPingResponse.
                Setup(mockedResponse => mockedResponse.IsValid).
                Returns(false);
            mockedClient.
                Setup(mockedClient => mockedClient.Ping(It.IsAny<Func<PingDescriptor, IPingRequest>>())).
                Returns(mockedPingResponse.Object);
            Assert.Throws<Exception>(() => new TextDocumentIndex("test2", mockedClient.Object));
        }

        private void AddToIndexTest(bool isValid = true) {
            var mockedBulkResponse = new Mock<BulkResponse>();
            mockedBulkResponse.
               Setup(mockedResponse => mockedResponse.IsValid).
               Returns(isValid);
            mockedClient.
                Setup(mockedClient => mockedClient.Bulk(It.IsAny<BulkDescriptor>())).
                Returns(mockedBulkResponse.Object);
            var elasticIndex = new TextDocumentIndex("test5", mockedClient.Object);
            elasticIndex.AddToIndex(new List<TextDocument> {
                new TextDocument{
                    Path=@"lalalalalal\lalalllalalalla\la.la",
                    Text="dtfyguhkjlef shsgsfgbkjs dfjkgbjs bsb jskb  skbs bsdfjgjbd sbhbjs  jkhdsfjkbhsdjg dsbgjdbg jgjsg"
                }
            });
        }

        [Fact]
        public void AddToIndexTestIsNotValid() {
            Assert.Throws<Exception>(() => AddToIndexTest(false));
        }

        [Fact]
        public void AddToIndexTestIsValid() {
            AddToIndexTest();
            Assert.True(true);
        }
    }
}

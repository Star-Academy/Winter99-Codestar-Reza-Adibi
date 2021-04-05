using Moq;
using Nest;
using Nest.Specification.IndicesApi;
using System;
using Xunit;

namespace Libraries.Tests {
    public class PeopleIndexTests {
        private readonly Mock<IElasticClient> mockedClient;
        private readonly Mock<PingResponse> mockedPingResponse;
        public PeopleIndexTests() {
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
            new PeopleIndex("test1", mockedClient.Object);
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
            Assert.Throws<Exception>(() => new PeopleIndex("test2", mockedClient.Object));
        }

        [Fact]
        public void CreateIndexTestIsValid() {
            var mockedCreateresponse = new Mock<CreateIndexResponse>();
            mockedCreateresponse.
                Setup(response => response.IsValid).
                Returns(true);
            mockedClient.
                Setup(mockedClient => mockedClient.Indices.Create(It.IsAny<string>(), It.IsAny<Func<CreateIndexDescriptor, ICreateIndexRequest>>())).
                Returns(mockedCreateresponse.Object);
            var index = new PeopleIndex("test3", mockedClient.Object);
            index.CreateIndex();
            Assert.True(true);
        }

        [Fact]
        public void CreateIndexTestIsNotValid() {
            var mockedCreateresponse = new Mock<CreateIndexResponse>();
            mockedCreateresponse.
                Setup(response => response.IsValid).
                Returns(true);
            var mockedIndices = new Mock<IndicesNamespace>();
            mockedIndices.
                Setup(indices => indices.Create(It.IsAny<string>(), It.IsAny<Func<CreateIndexDescriptor, ICreateIndexRequest>>())).
                Returns(mockedCreateresponse.Object);
            mockedClient.
                Setup(mockedClient => mockedClient.Indices).
                Returns(mockedIndices.Object);
            var index = new PeopleIndex("test4", mockedClient.Object);
            Assert.Throws<Exception>(() => index.CreateIndex());
        }

        [Fact]
        public void AddToIndexTest() {
            Assert.True(false, "This test needs an implementation");
        }
    }
}

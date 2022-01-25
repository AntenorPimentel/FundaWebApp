using Funda.Business.Services;
using Funda.Data.Gateways.Interfaces;
using Funda.Test.Extensions;
using Funda.Test.ServiceTests;
using System.Collections.Generic;
using Moq;
using Xunit;
using System;
using Funda.Data.DTO;

namespace Funda.Test
{
    public class MakelaarServiceTests : ServiceTestsBase
    {
        private readonly Mock<IKoopwoningenGateway> _mockKoopwoningenGateway;
        private readonly MakelaarService _sut;

        public MakelaarServiceTests()
        {
            _mockKoopwoningenGateway = new Mock<IKoopwoningenGateway>();
            _sut = new MakelaarService(_mockKoopwoningenGateway.Object, _apiPaggingConfig, _mapper);
        }

        [Fact]
        public async void When_GetMakelaarWithMostHouseForSale_HasNumberOfTopMakelaar_Then_GetHousesForSaleIsInvoked()
        {
            var request = 10;
            _mockKoopwoningenGateway.Setup(g => g.GetHousesForSale(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new MakelaarsPersistence().Build());

            var result = await _sut.GetMakelaarWithMostHouseForSale(request);

            _mockKoopwoningenGateway.Verify(s => s.GetHousesForSale(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()), Times.AtLeastOnce());
        }

        [Fact]
        public async void When_GetMakelaarWithMostHouseForSaleWithTuin_HasNumberOfTopMakelaar_Then_GetHousesForSaleIsInvoked()
        {
            var request = 10;
            _mockKoopwoningenGateway.Setup(g => g.GetHousesForSale(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new MakelaarsPersistence().Build());

            var result = await _sut.GetMakelaarWithMostHouseForSaleWithTuin(request);

            _mockKoopwoningenGateway.Verify(s => s.GetHousesForSale(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()), Times.AtLeastOnce());
        }

        [Fact]
        public async void When_GetMakelaarWithMostHouseForSale_DoesNotHaveNumberOfTopMakelaar_Then_ThrowArgumentException()
        {
            var request = 0;
            _mockKoopwoningenGateway.Setup(g => g.GetHousesForSale(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new MakelaarsPersistence().Build());

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetMakelaarWithMostHouseForSale(request));

            Assert.Equal("NumberOfTopMakelaar is not valid", ex.Message);
            _mockKoopwoningenGateway.Verify(s => s.GetHousesForSale(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(0));
        }

        [Fact]
        public async void When_GetMakelaarWithMostHouseForSaleWithTuin_DoesNotHaveNumberOfTopMakelaar_Then_ThrowArgumentException()
        {
            var request = 0;
            _mockKoopwoningenGateway.Setup(g => g.GetHousesForSale(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new MakelaarsPersistence().Build());

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetMakelaarWithMostHouseForSaleWithTuin(request));

            Assert.Equal("NumberOfTopMakelaar is not valid", ex.Message);
            _mockKoopwoningenGateway.Verify(s => s.GetHousesForSale(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(0));
        }

        [Fact]
        public async void Try_GetMakelaarWithMostHouseForSale_And_GetHousesForSale_ReturnsEmpty_Then_ThrowArgumentNullException()
        {
            var request = 10;
            _mockKoopwoningenGateway.Setup(g => g.GetHousesForSale(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new MakelaarsPersistence().Build().WithNull());

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.GetMakelaarWithMostHouseForSale(request));

            Assert.Equal("Value cannot be null. (Parameter 'makelaarsPersistence')", ex.Message);
            _mockKoopwoningenGateway.Verify(s => s.GetHousesForSale(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()), Times.AtLeastOnce());
        }

        [Fact]
        public async void Try_GetMakelaarWithMostHouseForSale_And_HasNullOrEmptyMakelaarNaam_Then_ThrowArgumentNullException()
        {
            var request = 10;
            _mockKoopwoningenGateway.Setup(g => g.GetHousesForSale(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new MakelaarsPersistence().Build().WithEmptyMakelaarNaam());

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetMakelaarWithMostHouseForSale(request));

            Assert.Equal("MakelaarId is required, MakelaarNaam is required", ex.Message);
            _mockKoopwoningenGateway.Verify(s => s.GetHousesForSale(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()), Times.AtLeastOnce());
        }
    }
}
using JaVisitei.Brasil.Business.ViewModels.Response.Island;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JaVisitei.Brasil.Business.Service.Test.Mocks;
using JaVisitei.Brasil.Business.Service.Services;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;

namespace JaVisitei.Brasil.Business.Service.Test.Services
{
    [TestClass]
    public class IslandServiceTest
    {
        private readonly IslandService _islandService;
        private readonly Mock<IIslandRepository> _mockIslandRepository;
        private readonly Mock<IMapper> _mockMapper;

        public IslandServiceTest()
        {
            _mockIslandRepository = new Mock<IIslandRepository>();
            _mockMapper = new Mock<IMapper>();
            _islandService = new IslandService(_mockIslandRepository.Object, _mockMapper.Object);
        }

        #region Islands by state

        [TestMethod("Islands by state Correct return")]
        public async Task GetByStateAsync_ShouldCorrectReturn_IslandsByState()
        {
            var stateId = "ba_bahia_estado";
            var islandsEntity = IslandMock.ReturnIslandListMock();
            var islandsResponse = IslandMock.ReturnIslandResponseListMock();

            _ = _mockIslandRepository
                .Setup(x => x.GetByStateAsync(stateId))
                .ReturnsAsync(islandsEntity);

            _ = _mockMapper
                .Setup(x => x.Map<IEnumerable<IslandResponse>>(islandsEntity))
                .Returns(islandsResponse);

            var result = await _islandService.GetByStateAsync<IslandResponse>(stateId) as List<IslandResponse>;

            Assert.IsNotNull(result);
            Assert.AreEqual(islandsResponse, result);
            Assert.AreEqual(islandsEntity[0].Id, result[0].Id);
            Assert.AreEqual(islandsEntity[0].Name, result[0].Name);
            Assert.AreEqual(islandsEntity[0].ArchipelagoId, result[0].ArchipelagoId);
            Assert.AreEqual(islandsEntity[0].Id, islandsResponse[0].Id);
        }

        [TestMethod("Islands by state Null return")]
        public async Task GetByStateAsync_ShouldNullReturn_IslandsByState()
        {
            _ = _mockIslandRepository
                .Setup(x => x.GetByStateAsync(It.IsAny<string>()))
                .ReturnsAsync((List<Island>)null);

            var result = await _islandService.GetByStateAsync<IslandResponse>(It.IsAny<string>()) as List<IslandResponse>;

            Assert.IsNull(result);
        }

        [TestMethod("Islands by state empty return")]
        public async Task GetByStateAsync_ShouldEmptyReturn_IslandsByState()
        {
            _ = _mockIslandRepository
                .Setup(x => x.GetByStateAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Island>());

            var result = await _islandService.GetByStateAsync<IslandResponse>(It.IsAny<string>()) as List<IslandResponse>;

            Assert.IsNull(result);
        }

        #endregion

        #region Islands by macroregion

        [TestMethod("Islands by macroregion Correct return")]
        public async Task GetByMacroregionAsync_ShouldCorrectReturn_IslandsByState()
        {
            var stateId = "ba_metropolitana_de_salvador_macro";
            var islandsEntity = IslandMock.ReturnIslandListMock();
            var islandsResponse = IslandMock.ReturnIslandResponseListMock();

            _ = _mockIslandRepository
                .Setup(x => x.GetByMacroregionAsync(stateId))
                .ReturnsAsync(islandsEntity);

            _ = _mockMapper
                .Setup(x => x.Map<IEnumerable<IslandResponse>>(islandsEntity))
                .Returns(islandsResponse);

            var result = await _islandService.GetByMacroregionAsync<IslandResponse>(stateId) as List<IslandResponse>;

            Assert.IsNotNull(result);
            Assert.AreEqual(islandsResponse, result);
            Assert.AreEqual(islandsEntity[0].Id, result[0].Id);
            Assert.AreEqual(islandsEntity[0].Name, result[0].Name);
            Assert.AreEqual(islandsEntity[0].ArchipelagoId, result[0].ArchipelagoId);
            Assert.AreEqual(islandsEntity[0].Id, islandsResponse[0].Id);
        }

        [TestMethod("Islands by macroregion Null return")]
        public async Task GetByMacroregionAsync_ShouldNullReturn_IslandsByState()
        {
            _ = _mockIslandRepository
                .Setup(x => x.GetByMacroregionAsync(It.IsAny<string>()))
                .ReturnsAsync((List<Island>)null);

            var result = await _islandService.GetByMacroregionAsync<IslandResponse>(It.IsAny<string>()) as List<IslandResponse>;

            Assert.IsNull(result);
        }

        [TestMethod("Islands by macroregion empty return")]
        public async Task GetByMacroregionAsync_ShouldEmptyReturn_IslandsByState()
        {
            _ = _mockIslandRepository
                .Setup(x => x.GetByMacroregionAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Island>());

            var result = await _islandService.GetByMacroregionAsync<IslandResponse>(It.IsAny<string>()) as List<IslandResponse>;

            Assert.IsNull(result);
        }

        #endregion
    }
}

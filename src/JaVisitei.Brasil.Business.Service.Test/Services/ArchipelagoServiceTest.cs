using JaVisitei.Brasil.Business.ViewModels.Response.Archipelago;
using JaVisitei.Brasil.Business.Service.Services;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JaVisitei.Brasil.Business.Service.Test.Mocks;
using System.Collections.Generic;
using JaVisitei.Brasil.Data.Entities;
using System.Threading.Tasks;
using AutoMapper;
using Moq;

namespace JaVisitei.Brasil.Business.Service.Test.Services
{
    [TestClass]
    public class ArchipelagoServiceTest
    {
        private readonly ArchipelagoService _archipelagoService;
        private readonly Mock<IArchipelagoRepository> _mockArchipelagoRepository;
        private readonly Mock<IMapper> _mockMapper;

        public ArchipelagoServiceTest()
        {
            _mockArchipelagoRepository = new Mock<IArchipelagoRepository>();
            _mockMapper = new Mock<IMapper>();
            _archipelagoService = new ArchipelagoService(_mockArchipelagoRepository.Object, _mockMapper.Object);
        }

        #region Archipelagos

        [TestMethod("Archipelagos by state Correct return")]
        public async Task GetByStateAsync_ShouldCorrectReturn_Archipelagos()
        {
            var stateId = "ba_bahia_estado";
            var archipelagosEntity = ArchipelagoMock.ReturnArchipelagoListMock();
            var archipelagosResponse = ArchipelagoMock.ReturnArchipelagoResponseListMock();

            _ = _mockArchipelagoRepository
                .Setup(x => x.GetByStateAsync(stateId))
                .ReturnsAsync(archipelagosEntity);
            
            _ = _mockMapper
                .Setup(x => x.Map<IEnumerable<ArchipelagoResponse>>(archipelagosEntity))
                .Returns(archipelagosResponse);

            var result = await _archipelagoService.GetByStateAsync<ArchipelagoResponse>(stateId) as List<ArchipelagoResponse>;

            Assert.IsNotNull(result);
            Assert.AreEqual(archipelagosResponse, result);
            Assert.AreEqual(archipelagosEntity[0].Id, result[0].Id);
            Assert.AreEqual(archipelagosEntity[0].Name, result[0].Name);
            Assert.AreEqual(archipelagosEntity[0].MacroregionId, result[0].MacroregionId);
            Assert.AreEqual(archipelagosEntity[0].Id, archipelagosResponse[0].Id);
        }

        [TestMethod("Archipelagos by state Null return")]
        public async Task GetByStateAsync_ShouldNullReturn_Archipelagos()
        {
            _ = _mockArchipelagoRepository
                .Setup(x => x.GetByStateAsync(It.IsAny<string>()))
                .ReturnsAsync((List<Archipelago>)null);

            var result = await _archipelagoService.GetByStateAsync<ArchipelagoResponse>(It.IsAny<string>()) as List<ArchipelagoResponse>;

            Assert.IsNull(result);
        }

        [TestMethod("Archipelagos by state empty return")]
        public async Task GetByStateAsync_ShouldEmptyReturn_Archipelagos()
        {
            _ = _mockArchipelagoRepository
                .Setup(x => x.GetByStateAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Archipelago>());

            var result = await _archipelagoService.GetByStateAsync<ArchipelagoResponse>(It.IsAny<string>()) as List<ArchipelagoResponse>;

            Assert.IsNull(result);
        }

        #endregion
    }
}

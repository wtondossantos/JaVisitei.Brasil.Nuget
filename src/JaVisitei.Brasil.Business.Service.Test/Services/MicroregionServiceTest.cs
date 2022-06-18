using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
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
    public class MicroregionServiceTest
    {
        private readonly MicroregionService _microregionService;
        private readonly Mock<IMicroregionRepository> _mockMicroregionRepository;
        private readonly Mock<IMapper> _mockMapper;

        public MicroregionServiceTest()
        {
            _mockMicroregionRepository = new Mock<IMicroregionRepository>();
            _mockMapper = new Mock<IMapper>();
            _microregionService = new MicroregionService(_mockMicroregionRepository.Object, _mockMapper.Object);
        }

        #region Microregions

        [TestMethod("Microregions by state Correct return")]
        public async Task GetByStateAsync_ShouldCorrectReturn_Microregions()
        {
            var stateId = "ba_bahia_estado";
            var microregionsEntity = MicroregionMock.ReturnMicroregionListMock();
            var microregionsResponse = MicroregionMock.ReturnMicroregionResponseListMock();

            _ = _mockMicroregionRepository
                .Setup(x => x.GetByStateAsync(stateId))
                .ReturnsAsync(microregionsEntity);

            _ = _mockMapper
                .Setup(x => x.Map<IEnumerable<MicroregionResponse>>(microregionsEntity))
                .Returns(microregionsResponse);

            var result = await _microregionService.GetByStateAsync<MicroregionResponse>(stateId) as List<MicroregionResponse>;

            Assert.IsNotNull(result);
            Assert.AreEqual(microregionsResponse, result);
            Assert.AreEqual(microregionsEntity[0].Id, result[0].Id);
            Assert.AreEqual(microregionsEntity[0].Name, result[0].Name);
            Assert.AreEqual(microregionsEntity[0].MacroregionId, result[0].MacroregionId);
            Assert.AreEqual(microregionsEntity[0].Id, microregionsResponse[0].Id);
        }

        [TestMethod("Microregions by state Null return")]
        public async Task GetByStateAsync_ShouldNullReturn_Microregions()
        {
            _ = _mockMicroregionRepository
                .Setup(x => x.GetByStateAsync(It.IsAny<string>()))
                .ReturnsAsync((List<Microregion>)null);

            var result = await _microregionService.GetByStateAsync<MicroregionResponse>(It.IsAny<string>()) as List<MicroregionResponse>;

            Assert.IsNull(result);
        }

        [TestMethod("Microregions by state empty return")]
        public async Task GetByStateAsync_ShouldEmptyReturn_Microregions()
        {
            _ = _mockMicroregionRepository
                .Setup(x => x.GetByStateAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Microregion>());

            var result = await _microregionService.GetByStateAsync<MicroregionResponse>(It.IsAny<string>()) as List<MicroregionResponse>;

            Assert.IsNull(result);
        }

        #endregion
    }
}

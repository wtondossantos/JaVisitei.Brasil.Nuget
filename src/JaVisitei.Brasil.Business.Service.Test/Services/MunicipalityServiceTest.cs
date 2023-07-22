using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JaVisitei.Brasil.Business.Service.Test.Mocks;
using JaVisitei.Brasil.Business.Service.Services;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using JaVisitei.Brasil.Caching.Service.Interfaces;

namespace JaVisitei.Brasil.Business.Service.Test.Services
{
    [TestClass]
    public class MunicipalityServiceTest
    {
        private readonly MunicipalityService _municipalityService;
        private readonly Mock<IMunicipalityRepository> _mockMunicipalityRepository;
        private readonly Mock<IMunicipalityCachingService> _mockMunicipalityCachingService;
        private readonly Mock<IMapper> _mockMapper;

        public MunicipalityServiceTest()
        {
            _mockMunicipalityRepository = new Mock<IMunicipalityRepository>();
            _mockMapper = new Mock<IMapper>();
            _municipalityService = new MunicipalityService(_mockMunicipalityRepository.Object, _mockMunicipalityCachingService.Object, _mockMapper.Object);
        }

        #region Municipalities by state

        [TestMethod("Municipalities by state Correct return")]
        public async Task GetByStateAsync_ShouldCorrectReturn_MunicipalitysByState()
        {
            var stateId = "ba_bahia_estado";
            var municipalitiesEntity = MunicipalityMock.ReturnMunicipalityListMock();
            var municipalitiesResponse = MunicipalityMock.ReturnMunicipalityResponseListMock();

            _ = _mockMunicipalityRepository
                .Setup(x => x.GetByStateAsync(stateId))
                .ReturnsAsync(municipalitiesEntity);

            _ = _mockMapper
                .Setup(x => x.Map<IEnumerable<MunicipalityResponse>>(municipalitiesEntity))
                .Returns(municipalitiesResponse);

            var result = await _municipalityService.GetByStateAsync<MunicipalityResponse>(stateId) as List<MunicipalityResponse>;

            Assert.IsNotNull(result);
            Assert.AreEqual(municipalitiesResponse, result);
            Assert.AreEqual(municipalitiesEntity[0].Id, result[0].Id);
            Assert.AreEqual(municipalitiesEntity[0].Name, result[0].Name);
            Assert.AreEqual(municipalitiesEntity[0].MicroregionId, result[0].MicroregionId);
            Assert.AreEqual(municipalitiesEntity[0].Id, municipalitiesResponse[0].Id);
        }

        [TestMethod("Municipalities by state Null return")]
        public async Task GetByStateAsync_ShouldNullReturn_MunicipalitysByState()
        {
            _ = _mockMunicipalityRepository
                .Setup(x => x.GetByStateAsync(It.IsAny<string>()))
                .ReturnsAsync((List<Municipality>)null);

            var result = await _municipalityService.GetByStateAsync<MunicipalityResponse>(It.IsAny<string>()) as List<MunicipalityResponse>;

            Assert.IsNull(result);
        }

        [TestMethod("Municipalities by state empty return")]
        public async Task GetByStateAsync_ShouldEmptyReturn_MunicipalitysByState()
        {
            _ = _mockMunicipalityRepository
                .Setup(x => x.GetByStateAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Municipality>());

            var result = await _municipalityService.GetByStateAsync<MunicipalityResponse>(It.IsAny<string>()) as List<MunicipalityResponse>;

            Assert.IsNull(result);
        }

        #endregion

        #region Municipalities by macroregion

        [TestMethod("Municipalities by macroregion Correct return")]
        public async Task GetByMacroregionAsync_ShouldCorrectReturn_MunicipalitysByState()
        {
            var stateId = "ba_metropolitana_de_salvador_macro";
            var municipalitiesEntity = MunicipalityMock.ReturnMunicipalityListMock();
            var municipalitiesResponse = MunicipalityMock.ReturnMunicipalityResponseListMock();

            _ = _mockMunicipalityRepository
                .Setup(x => x.GetByMacroregionAsync(stateId))
                .ReturnsAsync(municipalitiesEntity);

            _ = _mockMapper
                .Setup(x => x.Map<IEnumerable<MunicipalityResponse>>(municipalitiesEntity))
                .Returns(municipalitiesResponse);

            var result = await _municipalityService.GetByMacroregionAsync<MunicipalityResponse>(stateId) as List<MunicipalityResponse>;

            Assert.IsNotNull(result);
            Assert.AreEqual(municipalitiesResponse, result);
            Assert.AreEqual(municipalitiesEntity[0].Id, result[0].Id);
            Assert.AreEqual(municipalitiesEntity[0].Name, result[0].Name);
            Assert.AreEqual(municipalitiesEntity[0].MicroregionId, result[0].MicroregionId);
            Assert.AreEqual(municipalitiesEntity[0].Id, municipalitiesResponse[0].Id);
        }

        [TestMethod("Municipalities by macroregion Null return")]
        public async Task GetByMacroregionAsync_ShouldNullReturn_MunicipalitysByState()
        {
            _ = _mockMunicipalityRepository
                .Setup(x => x.GetByMacroregionAsync(It.IsAny<string>()))
                .ReturnsAsync((List<Municipality>)null);

            var result = await _municipalityService.GetByMacroregionAsync<MunicipalityResponse>(It.IsAny<string>()) as List<MunicipalityResponse>;

            Assert.IsNull(result);
        }

        [TestMethod("Municipalities by macroregion empty return")]
        public async Task GetByMacroregionAsync_ShouldEmptyReturn_MunicipalitysByState()
        {
            _ = _mockMunicipalityRepository
                .Setup(x => x.GetByMacroregionAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Municipality>());

            var result = await _municipalityService.GetByMacroregionAsync<MunicipalityResponse>(It.IsAny<string>()) as List<MunicipalityResponse>;

            Assert.IsNull(result);
        }

        #endregion
    }
}

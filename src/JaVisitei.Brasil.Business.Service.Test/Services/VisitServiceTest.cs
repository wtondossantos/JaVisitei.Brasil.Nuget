using JaVisitei.Brasil.Business.Service.Services;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JaVisitei.Brasil.Business.Service.Test.Mocks;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using JaVisitei.Brasil.Data.Entities;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service.Test.Services
{
    [TestClass]
    public class VisitServiceTest
    {
        private VisitService _visitService;
        private readonly Mock<IVisitRepository> _mockVisitRepository;
        private readonly Mock<IMunicipalityService> _mockMunicipalityService;
        private readonly Mock<IIslandService> _mockIslandService;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IMapper> _mockMapper;

        public VisitServiceTest()
        {
            _mockVisitRepository = new Mock<IVisitRepository>();
            _mockMunicipalityService = new Mock<IMunicipalityService>();
            _mockIslandService = new Mock<IIslandService>();
            _mockUserService = new Mock<IUserService>();
            _mockMapper = new Mock<IMapper>();
        }

        #region Visit by id

        [TestMethod("Visit by id Correct return")]
        public async Task GetByIdAsync_ShouldCorrectReturn_VisitById()
        {
            var request = VisitMock.VisitKeyRequestMock();
            var response = VisitMock.VisitMunicipalityMock();
            var responseMapper = VisitMock.ReturnVisitMunicipalityMock();

            _visitService = new VisitService(_mockVisitRepository.Object,
                null, null, null, new VisitValidator(), _mockMapper.Object);

            _ = _mockVisitRepository
               .Setup(x => x.GetFirstOrDefaultAsync(c => c.UserId.Equals(request.UserId) &&
                               c.RegionId.Equals(request.RegionId) &&
                               c.RegionTypeId.Equals(request.RegionTypeId), null))
               .ReturnsAsync(response);

            _ = _mockMapper
                .Setup(x => x.Map<VisitResponse>(response))
                .Returns(responseMapper);

            var result = await _visitService.GetByIdAsync<VisitResponse>(request);

            Assert.IsNotNull(result);
            Assert.AreEqual(responseMapper, result);
        }

        #endregion

        #region Visit by user id

        [TestMethod("Visit by user id Correct return")]
        public async Task GetByUserIdAsync_ShouldCorrectReturn_VisitById()
        {
            var userId = 1;
            var response = VisitMock.VisitListMock();
            var responseMapper = VisitMock.ReturnVisitListMock();

            _visitService = new VisitService(_mockVisitRepository.Object,
                null, null, null, new VisitValidator(), _mockMapper.Object);

            _ = _mockVisitRepository
               .Setup(x => x.GetAsync(c => c.UserId.Equals(userId), null))
               .ReturnsAsync(response);

            _ = _mockMapper
                .Setup(x => x.Map<IEnumerable<VisitResponse>>(response))
                .Returns(responseMapper);

            var result = await _visitService.GetByUserIdAsync<VisitResponse>(userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(responseMapper, result);
        }

        #endregion

        #region Insert visit

        [TestMethod("Insert visit Correct return municipaly")]
        public async Task InsertAsync_ShouldCorrectReturn_InsertVisitMunicipaly()
        {
            var request = VisitMock.CreateVisitRequestMock();
            var mapperVisit = VisitMock.VisitMunicipalityMock();
            var mapperVisitResponse = VisitMock.ReturnVisitMunicipalityMock();

            _visitService = new VisitService(_mockVisitRepository.Object,
                _mockMunicipalityService.Object, null, _mockUserService.Object, new VisitValidator(), _mockMapper.Object);

            _ = _mockMunicipalityService
                .Setup(x => x.AnyAsync(c => c.Id.Equals(request.RegionId)))
                .ReturnsAsync(true);

            _ = _mockMapper
                .Setup(x => x.Map<Visit>(request))
                .Returns(mapperVisit);

            _ = _mockVisitRepository
                .Setup(x => x.AnyAsync(mapperVisit))
                .ReturnsAsync(false);

            _ = _mockUserService
                .Setup(x => x.AnyAsync(c => c.Id.Equals(mapperVisit.UserId)))
                .ReturnsAsync(true);

            _ = _mockVisitRepository
                .Setup(x => x.InsertAsync(mapperVisit))
                .ReturnsAsync(true);

            _ = _mockMapper
                .Setup(x => x.Map<VisitResponse>(mapperVisit))
                .Returns(mapperVisitResponse);

            var result = await _visitService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Errors.Count.Equals(0));
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(mapperVisitResponse, result.Data);
        }

        [TestMethod("Insert visit Correct return island")]
        public async Task InsertAsync_ShouldCorrectReturn_InsertVisitIsland()
        {
            var request = VisitMock.CreateVisitIslandRequestMock();
            var mapperVisit = VisitMock.VisitIslandMock();
            var mapperVisitResponse = VisitMock.ReturnVisitIslandMock();

            _visitService = new VisitService(_mockVisitRepository.Object,
                null, _mockIslandService.Object, _mockUserService.Object, new VisitValidator(), _mockMapper.Object);

            _ = _mockIslandService
                .Setup(x => x.AnyAsync(c => c.Id.Equals(request.RegionId)))
                .ReturnsAsync(true);

            _ = _mockMapper
                .Setup(x => x.Map<Visit>(request))
                .Returns(mapperVisit);

            _ = _mockVisitRepository
                .Setup(x => x.AnyAsync(mapperVisit))
                .ReturnsAsync(false);

            _ = _mockUserService
                .Setup(x => x.AnyAsync(c => c.Id.Equals(mapperVisit.UserId)))
                .ReturnsAsync(true);

            _ = _mockVisitRepository
                .Setup(x => x.InsertAsync(mapperVisit))
                .ReturnsAsync(true);

            _ = _mockMapper
                .Setup(x => x.Map<VisitResponse>(mapperVisit))
                .Returns(mapperVisitResponse);

            var result = await _visitService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Errors.Count.Equals(0));
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(mapperVisitResponse, result.Data);
        }

        [TestMethod("Insert visit Invalid return mapper visit response nullable")]
        public async Task InsertAsync_ShouldCorrectReturn_MapperVisitResponseNullable()
        {
            var request = VisitMock.CreateVisitRequestMock();
            var mapperVisit = VisitMock.VisitMunicipalityMock();
            var mapperVisitResponse = VisitMock.ReturnVisitMunicipalityMock();

            _visitService = new VisitService(_mockVisitRepository.Object,
                _mockMunicipalityService.Object, null, _mockUserService.Object, new VisitValidator(), _mockMapper.Object);

            _ = _mockMunicipalityService
                .Setup(x => x.AnyAsync(c => c.Id.Equals(request.RegionId)))
                .ReturnsAsync(true);

            _ = _mockMapper
                .Setup(x => x.Map<Visit>(request))
                .Returns(mapperVisit);

            _ = _mockVisitRepository
                .Setup(x => x.AnyAsync(mapperVisit))
                .ReturnsAsync(false);

            _ = _mockUserService
                .Setup(x => x.AnyAsync(c => c.Id.Equals(mapperVisit.UserId)))
                .ReturnsAsync(true);

            _ = _mockVisitRepository
                .Setup(x => x.InsertAsync(mapperVisit))
                .ReturnsAsync(true);

            _ = _mockMapper
                .Setup(x => x.Map<VisitResponse>(mapperVisit))
                .Returns((VisitResponse)null);

            var result = await _visitService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Errors.Count.Equals(0));
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
        }

        [TestMethod("Insert visit Invalid return visit not created")]
        public async Task InsertAsync_ShouldCorrectReturn_VisitNotCreated()
        {
            var request = VisitMock.CreateVisitRequestMock();
            var mapperVisit = VisitMock.VisitMunicipalityMock();

            _visitService = new VisitService(_mockVisitRepository.Object,
                _mockMunicipalityService.Object, null, _mockUserService.Object, new VisitValidator(), _mockMapper.Object);

            _ = _mockMunicipalityService
                .Setup(x => x.AnyAsync(c => c.Id.Equals(request.RegionId)))
                .ReturnsAsync(true);

            _ = _mockMapper
                .Setup(x => x.Map<Visit>(request))
                .Returns(mapperVisit);

            _ = _mockVisitRepository
                .Setup(x => x.AnyAsync(mapperVisit))
                .ReturnsAsync(false);

            _ = _mockUserService
                .Setup(x => x.AnyAsync(c => c.Id.Equals(mapperVisit.UserId)))
                .ReturnsAsync(true);

            _ = _mockVisitRepository
                .Setup(x => x.InsertAsync(mapperVisit))
                .ReturnsAsync(false);

            var result = await _visitService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Insert visit Invalid return user nullable")]
        public async Task InsertAsync_ShouldCorrectReturn_UserNullable()
        {
            var request = VisitMock.CreateVisitRequestMock();
            var mapperVisit = VisitMock.VisitMunicipalityMock();

            _visitService = new VisitService(_mockVisitRepository.Object,
                _mockMunicipalityService.Object, null, _mockUserService.Object, new VisitValidator(), _mockMapper.Object);

            _ = _mockMunicipalityService
                .Setup(x => x.AnyAsync(c => c.Id.Equals(request.RegionId)))
                .ReturnsAsync(true);

            _ = _mockMapper
                .Setup(x => x.Map<Visit>(request))
                .Returns(mapperVisit);

            _ = _mockVisitRepository
                .Setup(x => x.AnyAsync(mapperVisit))
                .ReturnsAsync(false);

            _ = _mockUserService
                .Setup(x => x.AnyAsync(c => c.Id.Equals(mapperVisit.UserId)))
                .ReturnsAsync(false);

            var result = await _visitService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Insert visit Invalid return visit exists")]
        public async Task InsertAsync_ShouldCorrectReturn_VisitExists()
        {
            var request = VisitMock.CreateVisitRequestMock();
            var mapperVisit = VisitMock.VisitMunicipalityMock();

            _visitService = new VisitService(_mockVisitRepository.Object,
                _mockMunicipalityService.Object, null, null, new VisitValidator(), _mockMapper.Object);

            _ = _mockMunicipalityService
                .Setup(x => x.AnyAsync(c => c.Id.Equals(request.RegionId)))
                .ReturnsAsync(true);

            _ = _mockMapper
                .Setup(x => x.Map<Visit>(request))
                .Returns(mapperVisit);

            _ = _mockVisitRepository
                .Setup(x => x.AnyAsync(mapperVisit))
                .ReturnsAsync(true);

            var result = await _visitService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Insert visit Invalid return mapper visit nullable")]
        public async Task InsertAsync_ShouldCorrectReturn_MapperVisitNullable()
        {
            var request = VisitMock.CreateVisitRequestMock();

            _visitService = new VisitService(null, _mockMunicipalityService.Object, null, null, new VisitValidator(), _mockMapper.Object);

            _ = _mockMunicipalityService
                .Setup(x => x.AnyAsync(c => c.Id.Equals(request.RegionId)))
                .ReturnsAsync(true);

            _ = _mockMapper
                .Setup(x => x.Map<Visit>(request))
                .Returns((Visit)null);

            var result = await _visitService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Insert visit Invalid return municipality not find")]
        public async Task InsertAsync_ShouldCorrectReturn_MunipipalityNotfind()
        {
            var request = VisitMock.CreateVisitRequestMock();

            _visitService = new VisitService(null, _mockMunicipalityService.Object, null, null, new VisitValidator(), null);

            _ = _mockMunicipalityService
                .Setup(x => x.AnyAsync(c => c.Id.Equals(request.RegionId)))
                .ReturnsAsync(false);

            var result = await _visitService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Insert visit Invalid return island not find")]
        public async Task InsertAsync_ShouldCorrectReturn_IslandNotfind()
        {
            var request = VisitMock.CreateVisitRequestMock();

            _visitService = new VisitService(null, null, _mockIslandService.Object, null, new VisitValidator(), null);

            _ = _mockIslandService
                .Setup(x => x.AnyAsync(c => c.Id.Equals(request.RegionId)))
                .ReturnsAsync(false);

            var result = await _visitService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Insert visit Invalid return region not exists")]
        public async Task InsertAsync_ShouldCorrectReturn_RegionNotExists()
        {
            var request = VisitMock.CreateVisitRegionNotExistsRequestMock();

            _visitService = new VisitService(null, null, null, null, new VisitValidator(), null);

            var result = await _visitService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Insert visit Invalid validation")]
        public async Task InsertAsync_ShouldInvalidReturn_Validation()
        {
            var request = VisitMock.CreateVisitRegionNotExistsRequestMock();
            var visitValidationInvalid = VisitMock.VisitValidatorErrorMock();
            var visitService = new VisitService(null, null, null, null, visitValidationInvalid, null);

            _visitService = new VisitService(null, null, null, null, new VisitValidator(), null);

            var result = await visitService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Insert visit Return exception")]
        public async Task InsertAsync_ShouldProbrem_Exception()
        {
            var request = VisitMock.CreateVisitRequestMock();
            var message = "Exception test";

            _visitService = new VisitService(null, _mockMunicipalityService.Object, null, null, new VisitValidator(), null);

            _ = _mockMunicipalityService
                .Setup(x => x.AnyAsync(c => c.Id.Equals(request.RegionId)))
                .Throws(new Exception(message));

            var result = await _visitService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors[0].Contains(message));
        }

        #endregion

        #region Delete

        [TestMethod("Delete visit Correct return")]
        public async Task DeleteAsync_ShouldCorrectReturn_Visit()
        {
            var request = VisitMock.VisitKeyRequestMock();
            var response = VisitMock.VisitMunicipalityMock();

            _visitService = new VisitService(_mockVisitRepository.Object, 
                null, null, null, new VisitValidator(), _mockMapper.Object);

            _ = _mockVisitRepository
               .Setup(x => x.GetFirstOrDefaultAsync(c => c.UserId.Equals(request.UserId) &&
                               c.RegionId.Equals(request.RegionId) &&
                               c.RegionTypeId.Equals(request.RegionTypeId), null))
               .ReturnsAsync(response);

            _ = _mockMapper
                .Setup(x => x.Map<Visit>(response))
                .Returns(response);

            _ = _mockVisitRepository
                .Setup(x => x.DeleteAsync(response))
                .ReturnsAsync(true);

            var result = await _visitService.DeleteAsync(request);
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNotNull(result.Data);
        }

        [TestMethod("Delete visit Invalid delete")]
        public async Task DeleteAsync_ShouldInvalidReturn_Delete()
        {
            var request = VisitMock.VisitKeyRequestMock();
            var response = VisitMock.VisitMunicipalityMock();

            _visitService = new VisitService(_mockVisitRepository.Object,
                null, null, null, new VisitValidator(), _mockMapper.Object);

            _ = _mockVisitRepository
               .Setup(x => x.GetFirstOrDefaultAsync(c => c.UserId.Equals(request.UserId) &&
                               c.RegionId.Equals(request.RegionId) &&
                               c.RegionTypeId.Equals(request.RegionTypeId), null))
               .ReturnsAsync(response);

            _ = _mockMapper
                .Setup(x => x.Map<Visit>(response))
                .Returns(response);

            _ = _mockVisitRepository
                .Setup(x => x.DeleteAsync(response))
                .ReturnsAsync(false);

            var result = await _visitService.DeleteAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Delete visit Invalid Mapper nullable")]
        public async Task DeleteAsync_ShouldInvalidReturn_MapperNullable()
        {
            var request = VisitMock.VisitKeyRequestMock();
            var response = VisitMock.VisitMunicipalityMock();

            _visitService = new VisitService(_mockVisitRepository.Object,
                null, null, null, new VisitValidator(), _mockMapper.Object);

            _ = _mockVisitRepository
               .Setup(x => x.GetFirstOrDefaultAsync(c => c.UserId.Equals(request.UserId) &&
                               c.RegionId.Equals(request.RegionId) &&
                               c.RegionTypeId.Equals(request.RegionTypeId), null))
               .ReturnsAsync(response);

            _ = _mockMapper
                .Setup(x => x.Map<Visit>(response))
                .Returns((Visit)null);

            var result = await _visitService.DeleteAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNotNull(result.Data);
            Assert.IsFalse(result.Errors.Count > 0);
        }

        [TestMethod("Delete visit Invalid visit nullable")]
        public async Task DeleteAsync_ShouldInvalidReturn_VisitNullable()
        {
            var request = VisitMock.VisitKeyRequestMock();

            _visitService = new VisitService(_mockVisitRepository.Object,
                null, null, null, new VisitValidator(), null);

            _ = _mockVisitRepository
               .Setup(x => x.GetFirstOrDefaultAsync(c => c.UserId.Equals(request.UserId) &&
                               c.RegionId.Equals(request.RegionId) &&
                               c.RegionTypeId.Equals(request.RegionTypeId), null))
               .ReturnsAsync((Visit)null);

            var result = await _visitService.DeleteAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNotNull(result.Data);
            Assert.IsFalse(result.Errors.Count > 0);
        }

        [TestMethod("Delete visit Invalid validation")]
        public async Task DeleteAsync_ShouldInvalidReturn_Validation()
        {
            var request = VisitMock.CreateVisitRegionNotExistsRequestMock();
            var visitValidationInvalid = VisitMock.VisitValidatorErrorMock();
            var visitService = new VisitService(null, null, null, null, visitValidationInvalid, null);

            var result = await visitService.DeleteAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Delete visit Return exception")]
        public async Task DeleteAsync_ShouldProbrem_Exception()
        {
            var request = VisitMock.CreateVisitRequestMock();
            var message = "Exception test";

            _visitService = new VisitService(_mockVisitRepository.Object,
                null, null, null, new VisitValidator(), null);

            _ = _mockVisitRepository
               .Setup(x => x.GetFirstOrDefaultAsync(c => c.UserId.Equals(request.UserId) &&
                               c.RegionId.Equals(request.RegionId) &&
                               c.RegionTypeId.Equals(request.RegionTypeId), null))
                .Throws(new Exception(message));

            var result = await _visitService.DeleteAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors[0].Contains(message));
        }

        #endregion
    }
}

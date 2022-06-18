using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using JaVisitei.Brasil.Data.Entities;
using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service.Test.Mocks
{
    public static class VisitMock
    {
        public static GetVisitUserRequest UserId1RequestMock()
        {
            return new GetVisitUserRequest
            {
                UserId = 1
            };
        }
        public static GetVisitUserRequest UserId99RequestMock()
        {
            return new GetVisitUserRequest
            {
                UserId = 99
            };
        }
        public static VisitKeyRequest VisitKeyRequestMock()
        {
            return new VisitKeyRequest
            {
                RegionTypeId = 6,
                RegionId = "ce_aquiraz",
                UserId = 1
            };
        }
        public static InsertVisitRequest CreateVisitRequestMock()
        {
            return new InsertVisitRequest
            {
                Color = "200,100,50",
                VisitationDate = DateTime.Now.ToShortDateString(),
                RegionTypeId = 6,
                RegionId = "ce_aquiraz",
                UserId = 1
            };
        }
        public static InsertVisitRequest CreateVisitIslandRequestMock()
        {
            return new InsertVisitRequest
            {
                Color = "200,100,50",
                VisitationDate = DateTime.Now.ToShortDateString(),
                RegionTypeId = 7,
                RegionId = "es_ilha_trindade",
                UserId = 1
            };
        }
        public static InsertVisitRequest CreateVisitRegionNotExistsRequestMock()
        {
            return new InsertVisitRequest
            {
                Color = "200,1",
                VisitationDate = "02-022",
                RegionTypeId = 99,
                RegionId = "not_exists",
                UserId = 0
            };
        }

        public static VisitValidator CreatedVisitResponseMock()
        {
            return new VisitValidator
            {
                Data = new VisitResponse
                {
                    UserId = 1,
                    RegionTypeId = 6,
                    RegionId = "ce_aquiraz",
                    Color = "200,100,50",
                    VisitDate = DateTime.Now,
                    RegistryDate = DateTime.Now
                },
                Message = "sucess",
                Errors = new List<string>()
            };
        }

        public static VisitValidator VisitResponseMock()
        {
            return new VisitValidator
            {
                Data = new VisitResponse
                {
                    UserId = 1
                },
                Message = "sucess deletion",
                Errors = new List<string>()
            };
        }

        public static VisitValidator VisitValidatorErrorMock()
        {
            return new VisitValidator
            {
                Errors = new List<string> {
                    "Invalid return"
                }
            };
        }

        public static IEnumerable<VisitResponse> ReturnVisitListMock()
        {
            return new List<VisitResponse> {
                ReturnVisitMunicipalityMock(),
                ReturnVisitIslandMock()
            };
        }

        public static VisitResponse ReturnVisitMunicipalityMock()
        {
            return new VisitResponse
            {
                UserId = 1,
                RegionTypeId = 6,
                RegionId = "ce_aquiraz",
                Color = "200,100,50",
                VisitDate = DateTime.Now,
                RegistryDate = DateTime.Now
            };
        }

        public static VisitResponse ReturnVisitIslandMock()
        {
            return new VisitResponse
            {
                UserId = 1,
                RegionTypeId = 7,
                RegionId = "pe_ilha_de_sao_pedro_e_sao_paulo",
                Color = "200,150,50",
                VisitDate = DateTime.Now,
                RegistryDate = DateTime.Now
            };
        }

        public static IEnumerable<Visit> VisitListMock()
        {
            return new List<Visit> {
                VisitMunicipalityMock(),
                VisitIslandMock()
            };
        }

        public static Visit VisitMunicipalityMock()
        {
            return new Visit
            {
                UserId = 1,
                RegionTypeId = 6,
                RegionId = "ce_aquiraz",
                Color = "200,100,50",
                VisitDate = DateTime.Now,
                RegistryDate = DateTime.Now,
                RegionType = new RegionType(),
                User = new User()
            };
        }
        public static Visit VisitIslandMock()
        {
            return new Visit
            {
                UserId = 1,
                RegionTypeId = 7,
                RegionId = "es_ilha_trindade",
                Color = "200,100,50",
                VisitDate = DateTime.Now,
                RegistryDate = DateTime.Now,
                RegionType = new RegionType(),
                User = new User()
            };
        }
    }
}

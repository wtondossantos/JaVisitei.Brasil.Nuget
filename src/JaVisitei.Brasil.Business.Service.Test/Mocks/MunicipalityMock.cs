using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service.Test.Mocks
{
    public static class MunicipalityMock
    {
        public static List<Municipality> ReturnMunicipalityListMock()
        {
            return new List<Municipality> {
                ReturnMunicipality1Mock(),
                ReturnMunicipality2Mock(),
                ReturnMunicipality3Mock()
            };
        }

        public static Municipality ReturnMunicipality1Mock()
        {
            return new Municipality
            {
                Id = "ba_catu",
                Name = "Catu",
                Canvas = "ASDG464236 DGsSD GD434 ASDGAS4564",
                MicroregionId = "ba_catu_micro",
                Microregion = new Microregion()
            };
        }

        public static Municipality ReturnMunicipality2Mock()
        {
            return new Municipality
            {
                Id = "es_afonso_claudio",
                Name = "Afonso Cláudio",
                Canvas = "ASDG46423s6 DGSD GD434 ASDGAS4564",
                MicroregionId = "es_afonso_claudio_micro",
                Microregion = new Microregion()
            };
        }

        public static Municipality ReturnMunicipality3Mock()
        {
            return new Municipality
            {
                Id = "pe_aracoiaba",
                Name = "Araçoiaba",
                Canvas = "ASDG464236 DGSD GD434 ASDGAS4564",
                MicroregionId = "pe_itamaraca_micro",
                Microregion = new Microregion()
            };
        }

        public static List<MunicipalityResponse> ReturnMunicipalityResponseListMock()
        {
            return new List<MunicipalityResponse> {
                ReturnMunicipalityResponse1Mock(),
                ReturnMunicipalityResponse2Mock(),
                ReturnMunicipalityResponse3Mock()
            };
        }

        public static MunicipalityResponse ReturnMunicipalityResponse1Mock()
        {
            return new MunicipalityResponse
            {
                Id = "ba_catu",
                Name = "Catu",
                MicroregionId = "ba_catu_micro",
                Canvas = "ASDG464236 DGsSD GD434 ASDGAS4564"
            };
        }

        public static MunicipalityResponse ReturnMunicipalityResponse2Mock()
        {
            return new MunicipalityResponse
            {
                Id = "es_afonso_claudio",
                Name = "Afonso Cláudio",
                MicroregionId = "es_afonso_claudio_micro",
                Canvas = "ASDG46423s6 DGSD GD434 ASDGAS4564"
            };
        }

        public static MunicipalityResponse ReturnMunicipalityResponse3Mock()
        {
            return new MunicipalityResponse
            {
                Id = "pe_aracoiaba",
                Name = "Araçoiaba",
                MicroregionId = "pe_itamaraca_micro",
                Canvas = "ASDG464236 DGSD GD434 ASDGAS4564"
            };
        }
    }
}

using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service.Test.Mocks
{
    public static class MicroregionMock
    {
        public static List<Microregion> ReturnMicroregionListMock()
        {
            return new List<Microregion> {
                ReturnMicroregion1Mock(),
                ReturnMicroregion2Mock(),
                ReturnMicroregion3Mock()
            };
        }

        public static Microregion ReturnMicroregion1Mock()
        {
            return new Microregion
            {
                Id = "ba_catu_micro",
                Name = "Catu",
                MacroregionId = "ba_metropolitana_de_salvador_macro",
                Canvas = "1ASDG464236 DGSD GD434 ASDGAS4564",
                Macroregion = new Macroregion(),
                Municipalities = new List<Municipality>()
            };
        }

        public static Microregion ReturnMicroregion2Mock()
        {
            return new Microregion
            {
                Id = "es_afonso_claudio_micro",
                Name = "Afonso Cláudio",
                MacroregionId = "es_central_espirito_santense_macro",
                Canvas = "2ASDG464236 DGSD GD434 ASDGAS4564",
                Macroregion = new Macroregion(),
                Municipalities = new List<Municipality>()
            };
        }

        public static Microregion ReturnMicroregion3Mock()
        {
            return new Microregion
            {
                Id = "pe_itamaraca_micro",
                Name = "Itamaracá",
                MacroregionId = "pe_metropolitana_do_recife_macro",
                Canvas = "3ASDG464236 DGSD GD434 ASDGAS4564",
                Macroregion = new Macroregion(),
                Municipalities = new List<Municipality>()
            };
        }
        public static List<MicroregionResponse> ReturnMicroregionResponseListMock()
        {
            return new List<MicroregionResponse> {
                ReturnMicroregionResponse1Mock(),
                ReturnMicroregionResponse2Mock(),
                ReturnMicroregionResponse3Mock()
            };
        }

        public static MicroregionResponse ReturnMicroregionResponse1Mock()
        {
            return new MicroregionResponse
            {
                Id = "ba_catu_micro",
                Name = "Catu",
                MacroregionId = "ba_metropolitana_de_salvador_macro",
                Canvas = "1ASDG464236 DGSD GD434 ASDGAS4564"
            };
        }

        public static MicroregionResponse ReturnMicroregionResponse2Mock()
        {
            return new MicroregionResponse
            {
                Id = "es_afonso_claudio_micro",
                Name = "Afonso Cláudio",
                MacroregionId = "es_central_espirito_santense_macro",
                Canvas = "2ASDG464236 DGSD GD434 ASDGAS4564"
            };
        }

        public static MicroregionResponse ReturnMicroregionResponse3Mock()
        {
            return new MicroregionResponse
            {
                Id = "pe_itamaraca_micro",
                Name = "Itamaracá",
                MacroregionId = "pe_metropolitana_do_recife_macro",
                Canvas = "3ASDG464236 DGSD GD434 ASDGAS4564"
            };
        }
    }
}

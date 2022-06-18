using JaVisitei.Brasil.Business.ViewModels.Response.Archipelago;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service.Test.Mocks
{
    public static class ArchipelagoMock
    {
        public static List<Archipelago> ReturnArchipelagoListMock()
        {
            return new List<Archipelago> {
                ReturnArchipelago1Mock(),
                ReturnArchipelago2Mock(),
                ReturnArchipelago3Mock()
            };
        }

        public static Archipelago ReturnArchipelago1Mock()
        {
            return new Archipelago
            {
                Id = "ba_arquipelago_de_abrolhos_ilha",
                Name = "Arquipélago de Abrolhos",
                MacroregionId = "ba_metropolitana_de_salvador_macro",
                Macroregion = new Macroregion(),
                Islands = new List<Island>()
            };
        }

        public static Archipelago ReturnArchipelago2Mock()
        {
            return new Archipelago
            {
                Id = "es_arquipelago_de_trindade_e_martim_vaz_ilha",
                Name = "Arquipélago de Trindade e Martim Vaz",
                MacroregionId = "es_central_espirito_santense_macro",
                Macroregion = new Macroregion(),
                Islands = new List<Island>()
            };
        }

        public static Archipelago ReturnArchipelago3Mock()
        {
            return new Archipelago
            {
                Id = "pe_arquipelago_de_sao_pedro_e_sao_paulo_ilha",
                Name = "Arquipélago de São Pedro e São Paulo",
                MacroregionId = "pe_metropolitana_do_recife_macro",
                Macroregion = new Macroregion(),
                Islands = new List<Island>()
            };
        }
        public static List<ArchipelagoResponse> ReturnArchipelagoResponseListMock()
        {
            return new List<ArchipelagoResponse> {
                ReturnArchipelagoResponse1Mock(),
                ReturnArchipelagoResponse2Mock(),
                ReturnArchipelagoResponse3Mock()
            };
        }

        public static ArchipelagoResponse ReturnArchipelagoResponse1Mock()
        {
            return new ArchipelagoResponse
            {
                Id = "ba_arquipelago_de_abrolhos_ilha",
                Name = "Arquipélago de Abrolhos",
                MacroregionId = "ba_metropolitana_de_salvador_macro"
            };
        }

        public static ArchipelagoResponse ReturnArchipelagoResponse2Mock()
        {
            return new ArchipelagoResponse
            {
                Id = "es_arquipelago_de_trindade_e_martim_vaz_ilha",
                Name = "Arquipélago de Trindade e Martim Vaz",
                MacroregionId = "es_central_espirito_santense_macro"
            };
        }

        public static ArchipelagoResponse ReturnArchipelagoResponse3Mock()
        {
            return new ArchipelagoResponse
            {
                Id = "pe_arquipelago_de_sao_pedro_e_sao_paulo_ilha",
                Name = "Arquipélago de São Pedro e São Paulo",
                MacroregionId = "pe_metropolitana_do_recife_macro"
            };
        }
    }
}

using JaVisitei.Brasil.Business.ViewModels.Response.Island;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service.Test.Mocks
{
    public static class IslandMock
    {
        public static List<Island> ReturnIslandListMock()
        {
            return new List<Island> {
                ReturnIsland1Mock(),
                ReturnIsland2Mock(),
                ReturnIsland3Mock()
            };
        }

        public static Island ReturnIsland1Mock()
        {
            return new Island
            {
                Id = "ba_ilha_redonda",
                Name = "Ilha Redonda",
                Canvas = "ASDG464236 DGsSD GD434 ASDGAS4564",
                ArchipelagoId = "ba_arquipelago_de_abrolhos_ilha",
                Archipelago = new Archipelago()
            };
        }

        public static Island ReturnIsland2Mock()
        {
            return new Island
            {
                Id = "es_ilha_trindade",
                Name = "Ilha Trindade",
                Canvas = "ASDG46423s6 DGSD GD434 ASDGAS4564",
                ArchipelagoId = "es_arquipelago_de_trindade_e_martim_vaz_ilha",
                Archipelago = new Archipelago()
            };
        }

        public static Island ReturnIsland3Mock()
        {
            return new Island
            {
                Id = "pe_ilha_de_sao_pedro_e_sao_paulo",
                Name = "Arquipélago de São Pedro e São Paulo",
                Canvas = "ASDG464236 DGSD GD434 ASDGAS4564",
                ArchipelagoId = "pe_arquipelago_de_sao_pedro_e_sao_paulo_ilha",
                Archipelago = new Archipelago()
            };
        }

        public static List<IslandResponse> ReturnIslandResponseListMock()
        {
            return new List<IslandResponse> {
                ReturnIslandResponse1Mock(),
                ReturnIslandResponse2Mock(),
                ReturnIslandResponse3Mock()
            };
        }

        public static IslandResponse ReturnIslandResponse1Mock()
        {
            return new IslandResponse
            {
                Id = "ba_ilha_redonda",
                Name = "Ilha Redonda",
                ArchipelagoId = "ba_arquipelago_de_abrolhos_ilha",
                Canvas = "ASDG464236 DGsSD GD434 ASDGAS4564"
            };
        }

        public static IslandResponse ReturnIslandResponse2Mock()
        {
            return new IslandResponse
            {
                Id = "es_ilha_trindade",
                Name = "Ilha Trindade",
                ArchipelagoId = "es_arquipelago_de_trindade_e_martim_vaz_ilha",
                Canvas = "ASDG46423s6 DGSD GD434 ASDGAS4564"
            };
        }

        public static IslandResponse ReturnIslandResponse3Mock()
        {
            return new IslandResponse
            {
                Id = "pe_ilha_de_sao_pedro_e_sao_paulo",
                Name = "Arquipélago de São Pedro e São Paulo",
                ArchipelagoId = "pe_arquipelago_de_sao_pedro_e_sao_paulo_ilha",
                Canvas = "ASDG464236 DGSD GD434 ASDGAS4564"
            };
        }
    }
}

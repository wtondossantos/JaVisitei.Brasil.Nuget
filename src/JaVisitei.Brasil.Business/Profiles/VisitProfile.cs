using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Helper.Others;
using AutoMapper;
using System;
using JaVisitei.Brasil.Helper.Formatting;

namespace JaVisitei.Brasil.Business.Profiles
{
    public class VisitProfile : Profile
    {
        public VisitProfile()
        {
            CreateMap<InsertVisitRequest, Visit>()
                .BeforeMap((src, dest) =>
                {
                    src.Color ??= Utility.RandomColorRBGString();
                    src.RegionId = src.RegionId.ToLower();
                }).AfterMap((src, dest) => {
                    dest.RegistryDate = DateTime.Now;
                    dest.VisitDate = DateTime.Parse(src.VisitationDate);
                });

            CreateMap<Visit, VisitResponse>()
                .AfterMap((src, dest) => {
                    dest.Color = Format.ConvertRGBToHex(src.Color);
                });

            CreateMap<VisitKeyRequest, Visit>();

            CreateMap<UpdateVisitRequest, Visit>()
                .BeforeMap((src, dest) =>
                {
                    src.Color ??= Utility.RandomColorRBGString();
                    src.RegionId = src.RegionId.ToLower();
                }).AfterMap((src, dest) => {
                    dest.RegistryDate = DateTime.Now;
                    dest.VisitDate = DateTime.Parse(src.VisitationDate);
                });
        }
    }
}

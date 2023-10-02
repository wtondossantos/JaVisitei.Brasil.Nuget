using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using AutoMapper;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class RegionTypeService : ReadOnlyService<RegionType>, IRegionTypeService
    {
        public RegionTypeService(IRegionTypeRepository regionTypeRepository, IMapper mapper) : base(regionTypeRepository, mapper) { }
    }
}

using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class StateRepository : ReadOnlyRepository<State>, IStateRepository
    {
        public StateRepository(DbJaVisiteiBrasilContext context) : base(context) { }

        public async Task<List<State>> GetByCountryMapTypeIdAsync(short mapTypeId)
        {
            return await (from states in _context.States
                          join country in _context.Countries on states.CountryId equals country.Id
                          where country.MapTypeId.Equals(mapTypeId)
                          select new State { 
                              Id = states.Id,
                              Name = states.Name,
                              Canvas = states.Canvas,
                              CountryId = states.CountryId,
                              Country = new Country { 
                                  Name = country.Name,
                              }
                          }).ToListAsync();
        }
    }
}

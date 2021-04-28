using System.Collections.Generic;
using Domain.Entities;
using Domain.Abstract;

namespace Domain.DataBase
{
    public class EFCandyRepository : ICandyRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Candy> Candies
        {
            get { return context.Candies; }
        }
    }
}

using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface ICandyRepository
    {
        IEnumerable<Candy> Candies { get; }
        void SaveChanges(Candy candy);
        Candy Remove(int candyId);
    }
}

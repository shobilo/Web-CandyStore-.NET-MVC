using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface ICandyRepository
    {
        IEnumerable<Candy> Candies { get; }
    }
}

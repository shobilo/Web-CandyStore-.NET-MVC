using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.ViewModels
{
    public class CandiesListViewModel
    {
        public IEnumerable<Candy> Candies { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
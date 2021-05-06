using System.Collections.Generic;
using Domain.Entities;
using Domain.Abstract;
using System.Web;
using Microsoft.SqlServer.Server;

namespace Domain.DataBase
{
    public class EFCandyRepository : ICandyRepository
    {
        EFDbContext context;

        public EFCandyRepository()
        {
            string mdfFilePath = HttpContext.Current.Server.MapPath("~/App_Data/CandyStore.mdf");
            context = new EFDbContext(string.Format(@"Data Source=USER-PC;Initial Catalog=CandyStore;Integrated Security=True", mdfFilePath));
        }

        public IEnumerable<Candy> Candies
        {
            get { return context.Candies; }
        }
    }
}

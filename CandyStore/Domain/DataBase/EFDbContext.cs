using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Data.Entity;


namespace Domain.DataBase
{
    public class EFDbContext : DbContext
    {
        public DbSet<Candy> Candies { get; set; }
        public EFDbContext(string connectionString) : base(nameOrConnectionString: connectionString)
        { }
    }
}

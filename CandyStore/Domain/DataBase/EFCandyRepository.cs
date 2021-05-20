using System.Collections.Generic;
using Domain.Entities;
using Domain.Abstract;
using System.Web;
using Microsoft.SqlServer.Server;

namespace Domain.DataBase
{
    public class EFCandyRepository : ICandyRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Candy> Candies
        {
            get { return context.Candies; }
        }

        public void SaveChanges(Candy candy)
        {
            if (candy.CandyId == 0)
            {
                context.Candies.Add(candy);
            }
            else
            {
                Candy editedCandy = context.Candies.Find(candy.CandyId);
                if (editedCandy != null)
                {
                    editedCandy.Name = candy.Name;
                    editedCandy.Description = candy.Description;
                    editedCandy.Price = candy.Price;
                    editedCandy.Weight = candy.Weight;
                    editedCandy.Category = candy.Category;
                    editedCandy.ImageData = candy.ImageData;
                    editedCandy.ImageMimeType = candy.ImageMimeType;
                }
            }

            context.SaveChanges();
        }
        public Candy Remove(int candyId)
        {
            Candy removeCandy = context.Candies.Find(candyId);
            if (removeCandy != null)
            {
                context.Candies.Remove(removeCandy);
                context.SaveChanges();
            }
            return removeCandy;
        }
    }
}

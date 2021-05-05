using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Candy candy, int quantity)
        {
            CartLine line = lineCollection
                .Where(c => c.Candy.CandyId == candy.CandyId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Candy = candy,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Candy candy)
        {
            lineCollection.RemoveAll(l => l.Candy.CandyId == candy.CandyId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Candy.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Candy Candy { get; set; }
        public int Quantity { get; set; }
    }
}

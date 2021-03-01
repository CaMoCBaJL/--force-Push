using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySiteServer.Data.Repository
{
    public class SQLRepository : IRepository
    {
        private readonly DB context;
        public SQLRepository(DB context)
        {
            this.context = context;
        }

        public void AddPerfume(string goodName, int producerId, int stackAmount, string pathToImg, int price)
        {
            Good newPerfume = new Good()
            {
                GoodId = null,
                GoodName = goodName,
                GoodProducerId = producerId,
                GoodStackAmount = stackAmount,
                PathToGoodImg = pathToImg,
                GoodPrice = price
            };

            context.Goods.Add(newPerfume);
            context.SaveChanges();
        }

        public void DeleteGood(int goodId)
        {
            var deletedGood = context.Goods.Find(goodId);

            if (deletedGood != null)
            {
                context.Goods.Remove(deletedGood);
                context.SaveChanges();
            }
        }

        public IEnumerable<Good> GetAllGoods()
        {
            return context.Goods;
        }

        public void GoodChanged(Good changedGood)
        {
            var item = context.Goods.Attach(changedGood);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();
        }

    
    }
}

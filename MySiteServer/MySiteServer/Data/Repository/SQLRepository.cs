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
        public void AddPerfume(int goodId, string goodName, int producerId, int stackAmount, int price)
        {
            Good newPerfume = new Good()
            {
                GoodId = goodId,
                GoodName = goodName,
                GoodProducerId = producerId,
                GoodStackAmount = stackAmount,
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
        public IEnumerable<User> GetAllUsers()
        {
            return context.Users;
        }
        public IEnumerable<Producer> GetAllProducers()
        {
            return context.Producers;
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

        public void AddUser(User user)
        {
            context.Add(user);
        }
        public void RememberUser(int id)
        {
            
        }
    
    }
}

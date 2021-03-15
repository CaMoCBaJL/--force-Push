using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySiteServer
{
    public class Good
    {
        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public int GoodProducerId { get; set; }
        public int GoodStackAmount { get; set; }
        public int GoodPrice { get; set; }
        public string GoodInfo { get; set; }
    }
}

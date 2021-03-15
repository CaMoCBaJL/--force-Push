using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySiteServer
{
    public class NewsItem
    {
        public int NewsItemId { get; set; }
        public string NewsItemTitle { get; set; }
        public DateTime ReleaseDateTime { get; set; }
        public string NewsItemContent { get; set; }
    }
}

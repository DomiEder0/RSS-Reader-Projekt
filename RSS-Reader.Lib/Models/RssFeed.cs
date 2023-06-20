using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_Reader.Lib.Models
{
    public class RssFeed
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public IEnumerable<RssPost> Items { get; set; }

    }
}

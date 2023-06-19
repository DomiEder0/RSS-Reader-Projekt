using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_Reader.Lib.Models
{
    public class RssFeedItem
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTimeOffset PublishDate { get; set; }
        public bool New { get; set; }

        public bool Selected { get; set; }
    }
}

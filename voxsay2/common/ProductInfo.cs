using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace voxsay2
{
    public class ProductInfo
    {
        public string Hostname { get; set; }

        public int? Portnumber { get; set; }

        public string Context { get; set; }

        public ProdnameEnum Product { get; set; }

        public ProductInfo(string host, int port, string ctx, ProdnameEnum prod)
        {
            Hostname = host;
            Portnumber = port;
            Context = ctx;
            Product = prod;
        }
    }
}

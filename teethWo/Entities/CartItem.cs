using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teethWo.Entities
{
    public class CartItem
    {
        public Product Product { get; set; }
        public double Size { get; set; }
        public int Quantity { get; set; }
        public double Sum { get; set; }
    }
}

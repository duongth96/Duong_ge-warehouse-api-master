using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GE.WareHouse.Models
{
    public class TransactionItemModel
    {
        public IOStatus Status { get; set; }

        public string QRCode { get; set; }

        public int Quantity { get; set; }
    }
}

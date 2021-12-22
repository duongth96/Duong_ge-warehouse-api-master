using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GE.WareHouse.Models
{
    public class InventoryTransactionModel
    {
        public int WarehouseId { get; set; }

        public TransactionItemModel[] Items { get; set; }
    }
}

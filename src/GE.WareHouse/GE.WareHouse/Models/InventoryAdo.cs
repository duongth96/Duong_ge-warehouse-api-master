using Dapper;
using GE.WareHouse.Common;
using GE.WareHouse.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GE.WareHouse.Models
{
    public class InventoryAdo
    {
        private SQLCon _sQLCon;
        private AppSettings _appSettings;
        public InventoryAdo(AppSettings appSetting)
        {
            this._appSettings = appSetting;
            _sQLCon = new SQLCon(appSetting.SqlCnnString);
        }


        public List<WHMobiModel> findAll()
        {
            string query = $"select * from WHMobi";
            DynamicParameters pr = new DynamicParameters();
            var rs = _sQLCon.ExecuteListDapperText<WHMobiModel>(query, pr);
            return rs.ToList();
        }

        public WHMobiModel findById(int id)
        {
            string query = $"select * from WHMobi where id={id}";
            DynamicParameters pr = new DynamicParameters();
            var rs = _sQLCon.ExecuteListDapperText<WHMobiModel>(query, pr);
            return rs.FirstOrDefault();
        }

        public int Insert(IList<IOInventoryModel> entities)
        {
            DynamicParameters pr = new DynamicParameters();
            int stt = 0;
            foreach (var item in entities)
            {
                string query =
                    $@"insert into IOInventory (QRCode,Quantity,[Status],WarehouseId,WarehouseName,Username,TransactionId)
                    values (
                        N'{item.QRCode}',
                        {item.Quantity},
                        {(int) item.Status},
                        {item.WarehouseId},N'{item.WarehouseName}',N'{item.Username}',N'{item.TransactionId}')";

                stt = _sQLCon.ExecuteDapper<int>(query,pr);
            }
            return stt;
        }

    }
}

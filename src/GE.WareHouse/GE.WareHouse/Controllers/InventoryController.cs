using GE.WareHouse.Helpers;
using GE.WareHouse.Models;
using GE.WareHouse.Models.ModelBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GE.WareHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class InventoryController : ControllerBase
    {

        private readonly AppSettings _appSettings;
        private IHttpContextAccessor _context;
        public InventoryController(IOptions<AppSettings> appSettings, IHttpContextAccessor context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public object JsonConvert { get; private set; }

        [HttpPost]
        public ApiResultModel Index(InventoryTransactionModel model)
        {

            ApiResultModel result = new ApiResultModel();
            try
            {
                var ado = new InventoryAdo(_appSettings);

                var currentUser = _context.HttpContext.Items["User"];
                var username = (string) currentUser.GetType().GetProperty("Username").GetValue(currentUser);

                var warehouse = ado.findById(model.WarehouseId);
                if (warehouse == null)
                {
                    throw new Exception("Warehouse is not found");
                }

                if (model.Items == null || model.Items.Length == 0)
                {
                    throw new Exception("Please add at least an item");
                }

                var transactionId = DateTime.Now.ToString("yyyyMMddHmm") + "_" + username;

                List<IOInventoryModel> entities = model.Items.Select((item) => {
                    IOInventoryModel entity = new IOInventoryModel
                    {
                        QRCode = item.QRCode,
                        Quantity = item.Quantity,
                        Status = item.Status,
                        TransactionId = transactionId,
                        Username = username,
                        WarehouseId = warehouse.Id,
                        WarehouseName = warehouse.Name,
                    };

                    return entity;
                }).ToList();
                ado.Insert(entities);
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Messages.Add(ex.Message);
            }

            return result;
        }
    }
}

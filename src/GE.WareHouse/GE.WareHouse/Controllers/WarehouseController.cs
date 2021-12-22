using GE.WareHouse.Helpers;
using GE.WareHouse.Models;
using GE.WareHouse.Models.ModelBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GE.WareHouse.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WarehouseController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private IHttpContextAccessor _context;
        public WarehouseController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public ApiResultModel<List<WHMobiModel>> Index()
        {
            var ado = new InventoryAdo(_appSettings);
            var result = new ApiResultModel<List<WHMobiModel>>();

            try
            {
                result.Data = ado.findAll();
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

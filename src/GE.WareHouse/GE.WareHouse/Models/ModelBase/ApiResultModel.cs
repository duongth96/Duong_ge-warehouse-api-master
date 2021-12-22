using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GE.WareHouse.Models.ModelBase
{
    public class ApiResultModel<T>
    {
        public ApiResultModel()
        {
            Messages = new List<string>();
        }
        public bool IsSuccessful { get; set; }
        public List<string> Messages { get; set; }
        public T Data { get; set; }
    }

    public class ApiResultModel
    {
        public ApiResultModel()
        {
            Messages = new List<string>();
        }
        public bool IsSuccessful { get; set; }
        public List<string> Messages { get; set; }
    }
}

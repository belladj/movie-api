using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movies_api.Models
{
    public class BaseResult
    {
        public int Code { get; set; }
        public string Msg { get; set; }
    }

    public class BaseResult<T> : BaseResult
    {
        public T Data { get; set; }
    }

}
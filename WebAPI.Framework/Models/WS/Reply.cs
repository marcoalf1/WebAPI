using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Framework.Models.WS
{
    public class Reply
    {
        public int Result { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
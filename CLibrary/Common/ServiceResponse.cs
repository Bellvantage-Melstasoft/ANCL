using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Common
{
    public class ServiceResponse
    {
        public int Code { set; get; }
        public string Message { set; get; }
        public string Data { set; get; }
    }
}

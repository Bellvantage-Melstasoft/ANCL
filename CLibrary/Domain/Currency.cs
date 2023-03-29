using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
   public class Currency
    {
        [DBField("ID")]
        public int Id { get; set; }

        [DBField("NAME")]
        public string  Name { get; set; }

    }
}

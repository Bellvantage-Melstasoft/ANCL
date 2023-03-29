using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class CommonReference
    {
        [DBField("ID")]
        public int Id { get; set; }

        [DBField("NAME")]
        public string Name { get; set; }

        public int OrderId { get; set; }

        public int ReferenceId {get;set;}

        public string Date { get; set; }

        public string ReferenceNo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class PR_SupportiveDocument
    {
        [DBField("DEPARTMENT_ID")]
        public int DepartmentId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("PR_ID")]
        public int PrId { get; set; }

        [DBField("FILE_PATH")]
        public string FilePath { get; set; }

        [DBField("FILE_NAME")]
        public string FileName { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

       
    }
}

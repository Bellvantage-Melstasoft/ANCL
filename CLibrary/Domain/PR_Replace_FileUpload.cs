using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class PR_Replace_FileUpload
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

        [DBField("IS_DEFAULT_IMAGE")]
        public int isDefaultReplaceImage { get; set; }

        
    }
}

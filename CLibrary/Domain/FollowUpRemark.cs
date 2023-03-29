using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class FollowUpRemark {

        [DBField("ID")]
        public int Id { get; set; }

        [DBField("PO_ID")]
        public int PoId { get; set; }

        [DBField("REMARK")]
        public string Remark { get; set; }

        [DBField("USER_ID")]
        public int UserId { get; set; }

        [DBField("REMARK_DATE")]
        public DateTime RemarkDate { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("DELETED_USE")]
        public int DeletedUser { get; set; }

        [DBField("DELETED_DATE")]
        public DateTime DeletedDate { get; set; }

        [DBField("USER_NAME")]
        public string UserName { get; set; }
    }
}

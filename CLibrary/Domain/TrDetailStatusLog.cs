using CLibrary.Common;
using System;

namespace CLibrary.Domain
{
    public class TrDetailStatusLog
    {
        [DBField("MRND_ID")]
        public int MrndId { get; set; }

        [DBField("STATUS")]
        public int Status { get; set; }

        [DBField("LOGGED_DATE")]
        public DateTime LoggedDate { get; set; }

        [DBField("USER_ID")]
        public int UserId { get; set; }

        [DBField("FIRST_NAME")]
        public string FirstName { get; set; }

    }
}

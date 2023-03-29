using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using System.ComponentModel.DataAnnotations;

namespace CLibrary.Domain
{
    public class PRDetailsStatusLog
    {

        [DBField("PRD_ID")]
        public int PrdId { get; set; }

        [DBField("STATUS")]
        public int Status { get; set; }

        [DBField("LOGGED_DATE")]
        public DateTime LoggedDate { get; set; }

        [DBField("USER_ID")]
        public int UserId { get; set; }

        [DBField("USER_NAME")]
        public string UserName { get; set; }

        [DBField("LOG_NAME")]
        public string LogName { get; set; }

        
    }
}

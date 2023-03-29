using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using System.ComponentModel.DataAnnotations;

namespace CLibrary.Domain
{
    public class MRNDetailsStatusLog
    {

        [DBField("MRND_ID")]
        public int Mrnd_ID { get; set; }

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

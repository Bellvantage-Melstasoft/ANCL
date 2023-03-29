using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
   public class SuperAdmin
    {
        private int adminId;
        private string username;
        private string password;
        private int isActive;

        [DBField("IS_ACTIVE")]
        public int IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        [DBField("PASSWORD")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [DBField("USER_NAME")]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [DBField("ADMIN_ID")]
        public int AdminId
        {
            get { return adminId; }
            set { adminId = value; }
        }

    }
}

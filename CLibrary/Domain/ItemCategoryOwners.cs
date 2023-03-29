﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class ItemCategoryOwners
    {
        [DBField("ID")]
        public int Id { get; set; }

        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [DBField("OWNER_TYPE")]
        public string OwnerType { get; set; }

        [DBField("USER_ID")]
        public int UserId { get; set; }

        [DBField("EFFECTIVE_DATE")]
        public DateTime EffectiveDate  { get; set; }

        [DBField("CREATED_USER")]
        public int CreatedUser { get; set; }

        [DBField("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [DBField("FIRST_NAME")]
        public string FName { get; set; }
    }
}

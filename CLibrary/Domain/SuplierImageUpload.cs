
using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Domain
{
    public class SuplierImageUpload
    {
        private int supplierId;
        private string supplierImagePath;
        private int isActive;
        private string imageFileName;
        private String imageId;


        [DBField("IMAGE_ID")]
        public String ImageId
        {
            get { return imageId; }
            set { imageId = value; }
        }


        [DBField("FILE_NAME")]
        public string ImageFileName
        {
            get { return imageFileName; }
            set { imageFileName = value; }
        }


        [DBField("IS_ACTIVE")]
        public int IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        [DBField("IMAGE_PATH")]
        public string SupplierImagePath
        {
            get { return supplierImagePath; }
            set { supplierImagePath = value; }
        }

        [DBField("SUPPLIER_ID")]
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

    }

    
}

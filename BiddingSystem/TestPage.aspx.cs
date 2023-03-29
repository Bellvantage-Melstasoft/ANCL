using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace BiddingSystem
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string contentType = "image/png";
            string fileName = string.Empty;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile postedFile = Request.Files[i];
                if (postedFile.ContentLength > 0)
                {
                    fileName = Path.GetFileName(postedFile.FileName);
                    Stream fs = postedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                    // Call saving method by accessing the parameters
                   // SaveImageFile(fileName, contentType, bytes);
                }

            }
        }
    }
}
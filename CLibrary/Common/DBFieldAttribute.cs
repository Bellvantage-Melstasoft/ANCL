using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLibrary.Common
{

    [AttributeUsage(AttributeTargets.Property)]
    public class DBFieldAttribute:Attribute
    {

        private string mFieldName;

        public DBFieldAttribute(string fieldName)
        {
            mFieldName = fieldName;
        }

        public string fieldName
        {
            get { return mFieldName; }
        }
    }
}

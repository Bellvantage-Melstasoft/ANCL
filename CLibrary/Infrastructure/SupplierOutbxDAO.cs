using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Infrastructue
{
   public interface SupplierOutbxDAO
    {
    }

    public class SupplierOutbxDAOImpl: SupplierOutbxDAO
    {
    }

    public class SupplierOutbxDAOSQLImpl : SupplierOutbxDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];
    }
}

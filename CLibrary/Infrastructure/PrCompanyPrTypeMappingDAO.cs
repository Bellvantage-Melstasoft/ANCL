using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface PrCompanyPrTypeMappingDAO
    {
    }

    public class PrCompanyPrTypeMappingDAOImpl : PrCompanyPrTypeMappingDAO
    { 
    
    }

    public class PrCompanyPrTypeMappingDAOSQLImpl : PrCompanyPrTypeMappingDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface PrAdditionalColumnDAO
    {
    }

    public class PrAdditionalColumnDAOImpl : PrAdditionalColumnDAO
    { 
    
    }

    public class PrAdditionalColumnDAOSQLImpl : PrAdditionalColumnDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];
    }
    
}

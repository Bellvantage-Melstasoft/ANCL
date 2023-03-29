using log4net;
using System;

namespace BiddingSystem.Helpers {
    public static class Logger {
        public static void LogError(Exception ex) {
            try {
                ILog logger = LogManager.GetLogger("logger");
                logger.Error(ex.Message, ex);
            }
            catch (Exception exs) {

            }
        }
    }
}
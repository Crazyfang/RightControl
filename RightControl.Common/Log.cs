using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace RightControl.Common
{
    public class Log
    {
        public static void WriteFatal(Exception ex)
        {
            var log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            log.Fatal("严重错误", ex);
        }

        public static void WriteLog(Type t, Exception e)
        {
            var log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            log.Error("Error", e);
        }

        public static void WriteLog(Exception ex)
        {
            var log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            log.Error("Error", ex);
        }

        public static void WriteDebug(object text)
        {
            var log = log4net.LogManager.GetLogger("Debug信息");
            log.Debug(text);
        }

        public static void WriteInfo(string text)
        {
            var log = LogManager.GetLogger("程序运行信息");
            log.Info(text);
        }
    }
}

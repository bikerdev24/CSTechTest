using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CSTechTest.Common
{
    /// <summary>
    /// Some commment
    /// </summary>
    public class LogeHelper
    {
        public static log4net.ILog GetLogger([CallerFilePath] string fileName = "")
        {
            return log4net.LogManager.GetLogger(fileName);
        }
    }
}

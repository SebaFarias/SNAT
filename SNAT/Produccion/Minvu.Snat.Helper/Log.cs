using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minvu.Snat.Helper
{
    public class Log
    {
        public static Logger Instance { get; private set; }

        static Log()
        {
            LogManager.ReconfigExistingLoggers();
            Instance = LogManager.GetCurrentClassLogger();
        }

    }
}

using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcUnit.TcUnit_Runner
{
    class TcFileUtilities
    {
        private static ILog log = LogManager.GetLogger("TcUnit-Runner");

        // Singleton constructor
        private TcFileUtilities()
        { }

        /// <summary>
        /// Returns the version of TwinCAT that was used to create the PLC project
        /// </summary>
        /// <returns>Empty string if not found</returns>
        public static string GetTcVersion(string TwinCATProjectFilePath)
        {
            string tcVersion = "";
            System.IO.StreamReader file = new System.IO.StreamReader(@TwinCATProjectFilePath);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("TcVersion"))
                {
                    // Get the string between TcVersion=" and "
                    int start = line.IndexOf("TcVersion=\"") + 11;
                    string subString = line.Substring(start);
                    int end = subString.IndexOf("\"");
                    if (end > 0)
                    {
                        tcVersion = subString.Substring(0, end);
                        log.Info("In TwinCAT project file, found TwinCAT version " + tcVersion);
                    }
                    break;
                }
            }
            file.Close();
            return tcVersion;
        }
    }
}

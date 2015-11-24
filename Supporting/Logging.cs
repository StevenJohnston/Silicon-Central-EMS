using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Supporting
{
    /// <summary>
    /// This represents the Logging functions that will log the information to a file
    /// </summary>
    public class Logging
    {
        /// <summary>
        /// Log the string that is already formatted with the date and time of the current information
        /// put it in the log folder name logFile.txt
        /// </summary>
        /// <param name="toLog"></param>
        /// <returns></returns>
        public static bool LogString(string toLog)
        {
            bool success = false; //!< status if it was sucessful or not loggin the files
            string path = "Log\\LogFile.txt"; //!< the path of the file 
            string temp = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");//!< date and time

            try
            {
                if (!Directory.Exists("Log"))
                {
                    Directory.CreateDirectory("Log");
                }
                StreamWriter logFile = new StreamWriter(path, true);
                logFile.Write(temp);
                logFile.Write(toLog + "\n");
                logFile.Close();
                success = true;
            }
            catch
            {
                //error message thing here
            }
            return success;
        }


    }
}

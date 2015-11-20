using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Supporting
{
    public class Logging
    {

        public static bool LogString(string toLog)
        {
            bool success = false;
            string path = "Log\\LogFile.txt";
            string temp = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");

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

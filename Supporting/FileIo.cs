using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Supporting
{
    public struct Message
    {
        public int code;
        public string message;
    }
    public class FileIO
    {
        public delegate Message loadFunc(object param);
        public static object lockObj = new object();
        string path = "DBase/EMS project DBase.txt";
        public FileIO()
        {
            
        }
        public Message Load(loadFunc func)
        {
            Message returnMessage = new Message();
            returnMessage.code = 200;
            lock(lockObj)
            {
                try
                {
                    using (StreamReader fileIn = new StreamReader(path))
                    {
                        func(fileIn.ReadLine());
                    }
                }
                catch (Exception e)
                {

                }
                finally
                {

                }
            }
            return returnMessage;
        }
        public Message write(loadFunc func, object param)
        {
            return new Message();
        } 
    }
}

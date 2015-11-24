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
        public delegate Message saveFunc(StreamWriter fileOut);
        public static object lockObj = new object();
        string path = "DBase";
        string file = "EMS Project DBase.txt";
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
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (File.Exists(path+ "\\" + file))
                    {
                        using (StreamReader fileIn = new StreamReader(path+"\\"+file))
                        {
                            while (!fileIn.EndOfStream)
                            {
                                func(fileIn.ReadLine());
                            }
                        }
                    }
                    else
                    {
                        File.Create(path+ "\\" + file);
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
        public Message Save(saveFunc func)
        {
            Message returnMessage = new Message();
            returnMessage.code = 200;
            lock (lockObj)
            {
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    File.WriteAllText(path+"\\"+file, string.Empty);
                    using (StreamWriter fileOut = File.AppendText(path + "\\" + file))
                    {
                        func(fileOut);
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
        public Message Clear()
        {
            Message returnMessage = new Message();
            returnMessage.code = 200;
            lock (lockObj)
            {
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    File.WriteAllText(path, String.Empty);
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
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EMSExceptions;
namespace Supporting
{

    /// <summary>
    /// Class contains the definitions and functionalities requried to perform file-related handling, like
    /// loading content from a flat file (.txt extension), storing data content back into the file for permanent 
    /// storage 
    /// </summary>
    public class FileIO
    {
        public delegate void loadFunc(object param); //!< Delegate that retrieves information from a specified file

        public delegate void saveFunc(StreamWriter fileOut); //!< Delegate that passes information into a specified pathfile      

        public static object lockObj = new object(); //!< Static object used as a key to access flat file database, used so that users can access database one at a time

        string path = "DBase"; //!< Default path for the location of the directory containing the flat file database

        string file = "EMS Project DBase.txt"; //!< Default path for the location of the flat file (.txt extension) containing the database full of records


        /// <summary>
        /// Initiliazes the data members with the default path and filename
        /// </summary>
        public FileIO()
        {
            


        }


        /// <summary>
        /// The 'Load' method is used as the name implies to load the records from the flat file into a generic
        /// container that can be used to view data at ease and to perform changes amongst the data records if necessary
        /// and future use.  
        /// </summary>
        /// <param name="func"></param>
        /// <returns>Returns the Message Struct that indicates whether extracting the data from the file was 
        /// succesful or not</returns>
        public void Load(loadFunc func)
        {
            lock (lockObj)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (File.Exists(path + "\\" + file))
                {
                    FileLoadException fLE =null;
                    using (StreamReader fileIn = new StreamReader(path + "\\" + file))
                    {
                        while (!fileIn.EndOfStream)
                        {
                            try
                            {
                                func(fileIn.ReadLine());
                            }
                            catch (EmployeeException eE)
                            {
                                fLE = new FileLoadException("Found invalid employee data in Database. Records will be removed on next save");
                            }
                        }
                    }
                    if (fLE != null)
                    {
                        throw fLE;
                    }
                }
                else
                {
                    File.Create(path + "\\" + file);
                }
            }
        }


        /// <summary>
        /// The 'Save' method is used to store the records back into the flat (dsv) file for permanent storage
        /// and future use.  
        /// </summary>
        /// <param name="func">Argument passed in is a delegate of type 'savefunc' which is essentially a 
        /// a specifier for the file being written to using the 'streamwriter' stream</param>
        /// <returns>Returns the Message Struct that indicates whether re-storing data into the file was 
        /// succesful or not</returns>
        public void Save(saveFunc func)
        {
            lock (lockObj)
            {
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    File.WriteAllText(path + "\\" + file, string.Empty);

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
        }



        //public Message Clear()
        //{
        //    Message returnMessage = new Message();
        //    returnMessage.code = 200;
        //    lock (lockObj)
        //    {
        //        try
        //        {
        //            if (!Directory.Exists(path))
        //            {
        //                Directory.CreateDirectory(path);
        //            }
        //            File.WriteAllText(path, String.Empty);
        //        }
        //        catch (Exception e)
        //        {

        //        }
        //        finally
        //        {

        //        }
        //    }
        //    return returnMessage;
        //}
    }
}

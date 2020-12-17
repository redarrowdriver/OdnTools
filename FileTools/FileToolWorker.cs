using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTools
{
    public class FileToolWorker
    {
        public void emptyDirectories(string dirToCheck)
        {
            //okay so this way only returns the length of the directory name
            //need to do it like this
            //get into the directory check if there is a file in the directory if not, the dir is empty
            //this should give us our empty directories
            //need to check for files in each dir

            string[] dirs = Directory.GetDirectories(dirToCheck, "*", SearchOption.AllDirectories);
            foreach (string dir in dirs)
            {
                int fileCount = Directory.GetFiles(dir, "*", SearchOption.AllDirectories).Length;
                if (fileCount == 0)
                {
                    Console.WriteLine(dir);
                }
            }

            //int fileCount = Directory.GetFiles(dirToCheck, "*", SearchOption.AllDirectories).Length;
            //Console.WriteLine(fileCount.ToString());





            //string[] directories = Directory.GetFiles(dirToCheck, "*", SearchOption.AllDirectories);
            //foreach (string dir in directories)
            //{
            //    Console.WriteLine(dir + "         " + dir.Length.ToString());

            //}

            


            //DirectoryInfo di = new DirectoryInfo(dirToCheck);
            //DirectoryInfo[] subDirs = di.GetDirectories();
            //foreach (DirectoryInfo info in subDirs)
            //{
            //    Console.WriteLine(info.FullName.ToString());
            //}
        }
    }
}

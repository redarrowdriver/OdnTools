using System;
using System.IO;
using FileTools;

namespace OdnTools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ODN Consulting, LLC Tools");
            Console.WriteLine("Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            Console.WriteLine("Enter a file path: ");
            DirectoryInfo path =  new DirectoryInfo(Console.ReadLine());

            FileToolWorker fileChecker = new FileToolWorker();

            fileChecker.emptyDirectories(path);

            fileChecker.creationDates(@"F:\temp\pictures\catch\SnapChat-2063693639.jpg");
            fileChecker.creationDates(@"F:\temp\pictures\2008\06\15\PICT0290.jpg");

        }
    }
}

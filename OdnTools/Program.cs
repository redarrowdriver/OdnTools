using System;
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
            string path = Console.ReadLine();

            FileToolWorker fileChecker = new FileToolWorker();

            fileChecker.emptyDirectories(path);

            fileChecker.creationDates(@"F:\temp\pictures\catch\SnapChat-2063693639.jpg");

        }
    }
}

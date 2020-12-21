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
            FileToolWorker fileWorker = new FileToolWorker();

            //if (args.Length == 0)
            //{
            //    Console.WriteLine("");
            //}

            int loopControl = 0;
            while (loopControl != 3)
            {
                Console.WriteLine("1 - Empty Directory Removal");
                Console.WriteLine("2 - Image Library Structurer");
                Console.WriteLine("3 - Exit");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter a file path: ");
                        DirectoryInfo path = new DirectoryInfo(Console.ReadLine());
                        fileWorker.srcPath = path;
                        fileWorker.emptyDirectories(path);
                        break;
                    case "2":
                        Console.WriteLine("Enter a file path: ");
                        DirectoryInfo path2 = new DirectoryInfo(Console.ReadLine());
                        fileWorker.srcPath = path2;
                        fileWorker.structureImageLibrary(path2);
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                }
            }

            //DirectoryInfo path = new DirectoryInfo(Console.ReadLine());
            //fileWorker.srcPath = path;
            //fileWorker.emptyDirectories(path);

            

            //fileChecker.creationDates(@"F:\temp\pictures\catch\SnapChat-2063693639.jpg");
            //fileWorker.creationDates(@"F:\temp\pictures\2008\06\15\PICT0290.jpg");

        }
    }
}

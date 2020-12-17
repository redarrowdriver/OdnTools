using System;

namespace OdnTools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ODN Consulting, LLC Tools");
            Console.WriteLine("Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication24
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "test.txt";
            var file = File.Create(path);
            var watcher = new FileWatcher(path);
            file.Close();
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write("1");
                
            }
            watcher.OnWatch += FileChanged;
            watcher.Start();
        }

        public static void  FileChanged()
        {
            Console.WriteLine("The file was changed");
            if (string.Equals(File.ReadAllText("test.txt"), "1", StringComparison.OrdinalIgnoreCase))
            {
                File.WriteAllText("test.txt", "0");
                Thread.Sleep(3000);
            }
            
        }
    }
}

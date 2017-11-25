using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("F:\\12.bin"))
            {
                BinaryReader reader = new BinaryReader(File.Open("F:\\12.bin", FileMode.Open));
                while (true)
                {
                    try { Console.WriteLine(reader.ReadInt64()); }
                    catch
                    {
                        break;
                    }
                    
                }           
            }
        }
    }
}

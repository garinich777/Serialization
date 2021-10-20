using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            ListRandom listRandom = new ListRandom();
            for (int i = 0; i < 100; i++)
            {
                listRandom.Add($"Текст {i}");
            }
            using (FileStream stream = new FileStream("1", FileMode.Create, FileAccess.Write))
            {
                listRandom.Serialize(stream);
            }
            Console.WriteLine("Values:");
            foreach (var item in listRandom)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Deserialize:");
            using (FileStream stream = new FileStream("1", FileMode.Open, FileAccess.Read))
            {
                listRandom.Deserialize(stream);
            }
            foreach (string item in listRandom)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
        }
    }
}

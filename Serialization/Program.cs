using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            ListRandom listRandom = new ListRandom();
            listRandom.Add("Тест");
            listRandom.Add("Тест1");
            listRandom.Add("Тест2");
            foreach (var item in listRandom)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            listRandom.Remove("Тест1");
            foreach (var item in listRandom)
            {
                Console.WriteLine(item);
            }
            int a = listRandom.Count;
            listRandom.Clear();
            int b = listRandom.Count;
        }
    }
}

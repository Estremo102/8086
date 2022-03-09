using System;
using Intel8086;

namespace _8086
{
    class Program
    {
        static void Main(string[] args)
        {
            Procesor procesor = new Procesor();
            //procesor.Operacja();
            bool succeed = false;
            do
            {
                Console.Write(">");
                succeed = procesor.ExecuteOperation(Console.ReadLine());
                if (!succeed) Console.WriteLine("Wrong Command");
            }
            while (!succeed) ;
            Console.WriteLine(procesor);
        }
    }
}

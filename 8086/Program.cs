using System;
using Intel8086;

namespace _8086
{
    class Program
    {
        static void Main(string[] args)
        {
            Procesor procesor = new Procesor();
            procesor.Operacja();
            Console.WriteLine(procesor);
        }
    }
}

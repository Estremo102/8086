using System;
using Intel8086;

namespace _8086
{
    class Program
    {
        static void Main(string[] args)
        {
            string ah, al, bh, bl, ch, cl, dh, dl;
            Procesor proc;
            do
            {
                Console.WriteLine("AH: ");
                ah = Console.ReadLine();
            } while (Procesor.CheckData(ah, 2));
            do
            {
                Console.WriteLine("AL: ");
                al = Console.ReadLine();
            } while (Procesor.CheckData(ah, 2));
            do
            {
                Console.WriteLine("BH: ");
                bh = Console.ReadLine();
            } while (Procesor.CheckData(ah, 2));
            do
            {
                Console.WriteLine("BL: ");
                bl = Console.ReadLine();
            } while (Procesor.CheckData(ah, 2));
            do
            {
                Console.WriteLine("CH: ");
                ch = Console.ReadLine();
            } while (Procesor.CheckData(ah, 2));
            do
            {
                Console.WriteLine("CL: ");
                cl = Console.ReadLine();
            } while (Procesor.CheckData(ah, 2));
            do
            {
                Console.WriteLine("DH: ");
                dh = Console.ReadLine();
            } while (Procesor.CheckData(ah, 2));
            do
            {
                Console.WriteLine("DL: ");
                dl = Console.ReadLine();
            } while (Procesor.CheckData(ah, 2));

            proc = new Procesor(ah, al, bh, bl, ch, cl, dh, dl);
            Console.WriteLine("\n" + proc);
            Console.Write("> ");
            if (proc.ExecuteOperation(Console.ReadLine()))
                Console.WriteLine("\n" + proc);
            else Console.WriteLine("Wrong command");
        }
    }
}

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

            //public Procesor()
            //{
            //    string ah, al, bh, bl, ch, cl, dh, dl;
            //    while (!InputData("AH", out ah)) { Console.WriteLine("Wrong data"); }
            //    while (!InputData("AL", out al)) { Console.WriteLine("Wrong data"); }
            //    while (!InputData("BH", out bh)) { Console.WriteLine("Wrong data"); }
            //    while (!InputData("BL", out bl)) { Console.WriteLine("Wrong data"); }
            //    while (!InputData("CH", out ch)) { Console.WriteLine("Wrong data"); }
            //    while (!InputData("CL", out cl)) { Console.WriteLine("Wrong data"); }
            //    while (!InputData("DH", out dh)) { Console.WriteLine("Wrong data"); }
            //    while (!InputData("DL", out dl)) { Console.WriteLine("Wrong data"); }
            //    AH = ah;
            //    AL = al;
            //    BH = bh;
            //    BL = bl;
            //    CH = ch;
            //    CL = cl;
            //    DH = dh;
            //    DL = dl;
            //}


            //bool InputData(string whatIsInput, out string a)
            //{
            //    Console.Write(whatIsInput + ": ");
            //    a = Console.ReadLine().ToUpper();
            //    if (a.Length != 2) return false;
            //    if ((((int)a[0] >= 48 && (int)a[0] <= 57) || ((int)a[0] >= 65 && (int)a[0] <= 70)) && (((int)a[1] >= 48 && (int)a[1] <= 57) || ((int)a[1] >= 65 && (int)a[1] <= 70)))
            //        return true;
            //    return false;
            //}

            bool succeed;
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

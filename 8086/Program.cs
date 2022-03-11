using System;
using Intel8086;

namespace _8086
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("wersja konsolowa nieczynna w związku ze zmianami w bibiliotece");
            //Procesor procesor = new Procesor();
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

            //bool succeed;
            //do
            //{
            //    Console.Write(">");
            //    succeed = procesor.ExecuteOperation(Console.ReadLine());
            //    if (!succeed) Console.WriteLine("Wrong Command");
            //}
            //while (!succeed) ;
            //Console.WriteLine(procesor);



            //public void Operacja()
            //{
            //    Console.Write("Podaj rozkaz symulacji: ");
            //    string a = Console.ReadLine().ToUpper();
            //    Operation o;
            //    switch (a)
            //    {
            //        case "MOV":
            //            o = new Operation(MOV);
            //            break;
            //        case "XCHG":
            //            o = new Operation(XCHG);
            //            break;
            //        default:
            //            Console.WriteLine("Podano nieprawidłowe dane");
            //            Operacja();
            //            return;
            //    }
            //    string r1;
            //    string r2;
            //    while (!InputRejestr("Pierwszy Rejestr", out r1)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            //    while (!InputRejestr("Drugi Rejestr", out r2)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            //    o(RegisterToInt(r1), RegisterToInt(r2));
            //}



            //static bool InputRejestr(string whatIsInput, out string a)
            //{
            //    Console.WriteLine("Podaj " + whatIsInput);
            //    a = Console.ReadLine().ToUpper();
            //    if (a.Length != 2) return false;
            //    if (((int)a[0] >= 65 && (int)a[0] <= 68) && (a[1] == 'H' || a[1] == 'L'))
            //        return true;
            //    return false;
            //}
        }
    }
}

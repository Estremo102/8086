using System;

namespace _8086
{
    class Program
    {
        static void Main(string[] args)
        {
            while (!Input("AH", out string AH)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            while (!Input("AL", out string AL)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            while (!Input("BH", out string BH)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            while (!Input("BL", out string BL)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            while (!Input("CH", out string CH)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            while (!Input("CL", out string CL)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            while (!Input("DH", out string DH)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            while (!Input("DL", out string DL)) { Console.WriteLine("Podano nieprawidłowe dane"); }

            
        }

        static bool Input(string whatIsInput, out string a)
        {
            Console.WriteLine("Podaj " + whatIsInput);
            a = Console.ReadLine().ToUpper();
            if (a.Length != 2) return false;
            if ((((int)a[0] >= 48 && (int)a[0] <= 57) || ((int)a[0] >= 65 && (int)a[0] <= 70)) && (((int)a[1] >= 48 && (int)a[1] <= 57) || ((int)a[1] >= 65 && (int)a[1] <= 70)))
                return true;
            return false;
        }

        static void Operacja()
        {
            Console.Write("Podaj rozkaz symulacji: ");
            string a = Console.ReadLine().ToUpper();
            switch(a)
            {
                case "MOV":
                    break;
            }
        }

        static void MOV(ref string a, ref string b) => a = b;
        static void XCH(ref string a, ref string b)
        {
            string temp = a;
            a = b;
            b = temp;
        }
    }

    class Rejestr
    {

    }
}

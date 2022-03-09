using System;

namespace Intel8086
{
    public class Procesor
    {
        string[] rejestr = new string[8];

        delegate void Operation(int a, int b);

        public Procesor()
        {
            string ah, al, bh, bl, ch, cl, dh, dl;
            while (!InputData("AH", out ah)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            while (!InputData("AL", out al)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            while (!InputData("BH", out bh)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            while (!InputData("BL", out bl)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            while (!InputData("CH", out ch)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            while (!InputData("CL", out cl)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            while (!InputData("DH", out dh)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            while (!InputData("DL", out dl)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            rejestr[0] = ah;
            rejestr[1] = al;
            rejestr[2] = bh;
            rejestr[3] = bl;
            rejestr[4] = ch;
            rejestr[5] = cl;
            rejestr[6] = dh;
            rejestr[7] = dl;
        }

        bool InputData(string whatIsInput, out string a)
        {
            Console.WriteLine("Podaj " + whatIsInput);
            a = Console.ReadLine().ToUpper();
            if (a.Length != 2) return false;
            if ((((int)a[0] >= 48 && (int)a[0] <= 57) || ((int)a[0] >= 65 && (int)a[0] <= 70)) && (((int)a[1] >= 48 && (int)a[1] <= 57) || ((int)a[1] >= 65 && (int)a[1] <= 70)))
                return true;
            return false;
        }

        static bool InputRejestr(string whatIsInput, out string a)
        {
            Console.WriteLine("Podaj " + whatIsInput);
            a = Console.ReadLine().ToUpper();
            if (a.Length != 2) return false;
            if (((int)a[0] >= 65 && (int)a[0] <= 68) && (a[1] == 'H' || a[1] == 'L'))
                return true;
            return false;
        }

        static bool CheckRegister(string check)
        {
            if (check.Length != 2) return false;
            if (((int)check[0] >= 65 && (int)check[0] <= 68) && (check[1] == 'H' || check[1] == 'L'))
                return true;
            return false;
        }

        public bool ExecuteOperation(string input)
        {
            string[] a;
            a = input.ToUpper().Split(' ');
            Operation o;
            switch (a[0])
            {
                case "MOV":
                    o = new Operation(MOV);
                    break;
                case "XCH":
                    o = new Operation(XCH);
                    break;
                default:
                    return false;
            }
            a = a[1].Split(',');
            if(!CheckRegister(a[0]) || !CheckRegister(a[1])) return false;
            o(RegisterToInt(a[0]), RegisterToInt(a[1]));
            return true;
        }

        public void Operacja()
        {
            Console.Write("Podaj rozkaz symulacji: ");
            string a = Console.ReadLine().ToUpper();
            Operation o;
            switch (a)
            {
                case "MOV":
                    o = new Operation(MOV);
                    break;
                case "XCH":
                    o = new Operation(XCH);
                    break;
                default:
                    Console.WriteLine("Podano nieprawidłowe dane");
                    Operacja();
                    return;
            }
            string r1;
            string r2;
            while (!InputRejestr("Pierwszy Rejestr", out r1)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            while (!InputRejestr("Drugi Rejestr", out r2)) { Console.WriteLine("Podano nieprawidłowe dane"); }
            o(RegisterToInt(r1), RegisterToInt(r2));
        }

        void MOV(int a, int b) => rejestr[a] = rejestr[b];
        void XCH(int a, int b)
        {
            string temp = rejestr[a];
            rejestr[a] = rejestr[b];
            rejestr[b] = temp;
        }

        static int RegisterToInt(string r)
        {
            switch (r)
            {
                case "AH": return 0;
                case "AL": return 1;
                case "BH": return 2;
                case "BL": return 3;
                case "CH": return 4;
                case "CL": return 5;
                case "DH": return 6;
                case "DL": return 7;
                default: return -1;
            }
        }

        public override string ToString()
        {
            return $"AX: AH[{rejestr[0]}] AL[{rejestr[1]}]\n" +
                   $"BX: BH[{rejestr[2]}] BL[{rejestr[3]}]\n" +
                   $"CX: CH[{rejestr[4]}] CL[{rejestr[5]}]\n" +
                   $"DX: DH[{rejestr[6]}] DL[{rejestr[7]}]";
        }
    }
}
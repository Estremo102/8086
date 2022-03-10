using System;

namespace Intel8086
{
    public class Procesor
    {
        private string[] register = new string[8];
        public string AH { get => register[0]; private set => register[0] = value; }
        public string AL { get => register[1]; private set => register[0] = value; }
        public string BH { get => register[2]; private set => register[0] = value; }
        public string BL { get => register[3]; private set => register[0] = value; }
        public string CH { get => register[4]; private set => register[0] = value; }
        public string CL { get => register[5]; private set => register[0] = value; }
        public string DH { get => register[6]; private set => register[0] = value; }
        public string DL { get => register[7]; private set => register[0] = value; }

        delegate void Operation(int a, int b);

        public Procesor()
        {
            string ah, al, bh, bl, ch, cl, dh, dl;
            while (!InputData("AH", out ah)) { Console.WriteLine("Wrong data"); }
            while (!InputData("AL", out al)) { Console.WriteLine("Wrong data"); }
            while (!InputData("BH", out bh)) { Console.WriteLine("Wrong data"); }
            while (!InputData("BL", out bl)) { Console.WriteLine("Wrong data"); }
            while (!InputData("CH", out ch)) { Console.WriteLine("Wrong data"); }
            while (!InputData("CL", out cl)) { Console.WriteLine("Wrong data"); }
            while (!InputData("DH", out dh)) { Console.WriteLine("Wrong data"); }
            while (!InputData("DL", out dl)) { Console.WriteLine("Wrong data"); }
            register[0] = ah;
            register[1] = al;
            register[2] = bh;
            register[3] = bl;
            register[4] = ch;
            register[5] = cl;
            register[6] = dh;
            register[7] = dl;
        }

        public Procesor(params string[] registers)
        {
            if (registers.Length != 8) throw new ArgumentException();
            foreach (var register in registers) if (CheckData(register)) throw new ArgumentException();
            AH = registers[0];
            AL = registers[1];
            BH = registers[2];
            BL = registers[3];
            CH = registers[4];
            CL = registers[5];
            DH = registers[6];
            DL = registers[7];
        }

        bool InputData(string whatIsInput, out string a)
        {
            Console.Write(whatIsInput + ": ");
            a = Console.ReadLine().ToUpper();
            if (a.Length != 2) return false;
            if ((((int)a[0] >= 48 && (int)a[0] <= 57) || ((int)a[0] >= 65 && (int)a[0] <= 70)) && (((int)a[1] >= 48 && (int)a[1] <= 57) || ((int)a[1] >= 65 && (int)a[1] <= 70)))
                return true;
            return false;
        }

        bool CheckData(string data)
        {
            data = data.ToUpper();
            if (data.Length != 2) return false;
            if ((((int)data[0] >= 48 && (int)data[0] <= 57) || ((int)data[0] >= 65 && (int)data[0] <= 70)) && (((int)data[1] >= 48 && (int)data[1] <= 57) || ((int)data[1] >= 65 && (int)data[1] <= 70)))
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

        void MOV(int a, int b) => register[a] = register[b];
        void XCH(int a, int b)
        {
            string temp = register[a];
            register[a] = register[b];
            register[b] = temp;
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

        public override string ToString() => $"AX: AH[{register[0]}] AL[{register[1]}]\n" +
                                             $"BX: BH[{register[2]}] BL[{register[3]}]\n" +
                                             $"CX: CH[{register[4]}] CL[{register[5]}]\n" +
                                             $"DX: DH[{register[6]}] DL[{register[7]}]";
    }
}
using System;

namespace Intel8086
{
    public class Procesor
    {
        private int[] register = new int[8];
        public string AH { get => ToHex(register[0]); private set => register[0] = ToDecimal(value); }
        public string AL { get => ToHex(register[1]); private set => register[1] = ToDecimal(value); }
        public string BH { get => ToHex(register[2]); private set => register[2] = ToDecimal(value); }
        public string BL { get => ToHex(register[3]); private set => register[3] = ToDecimal(value); }
        public string CH { get => ToHex(register[4]); private set => register[4] = ToDecimal(value); }
        public string CL { get => ToHex(register[5]); private set => register[5] = ToDecimal(value); }
        public string DH { get => ToHex(register[6]); private set => register[6] = ToDecimal(value); }
        public string DL { get => ToHex(register[7]); private set => register[7] = ToDecimal(value); }

        delegate void Operation(int a, int b);

        public Procesor()
        {

        }

        public Procesor(params string[] registers)
        {
            if (registers.Length != 8) throw new ArgumentException();
            foreach (var register in registers) if (!CheckData(register)) throw new ArgumentException();
            AH = registers[0];
            AL = registers[1];
            BH = registers[2];
            BL = registers[3];
            CH = registers[4];
            CL = registers[5];
            DH = registers[6];
            DL = registers[7];
        }

        public static string ToHex(int x)
        {
            if (x < 16) return "0" + Convert.ToString(x, 16).ToUpper();
            return Convert.ToString(x, 16).ToUpper();
        }
        public static int ToDecimal(string x) => Convert.ToInt32(x, 16);

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
            if (check[0] >= 65 && check[0] <= 68 && (check[1] == 'H' || check[1] == 'L'))
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
                case "XCHG":
                    o = new Operation(XCHG);
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
                case "XCHG":
                    o = new Operation(XCHG);
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
        void XCHG(int a, int b)
        {
            int temp = register[a];
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

        public override string ToString() => $"AX: AH[{AH,2}] AL[{AL,2}]\n" +
                                             $"BX: BH[{BH,2}] BL[{BL,2}]\n" +
                                             $"CX: CH[{CH,2}] CL[{CL,2}]\n" +
                                             $"DX: DH[{DH,2}] DL[{DL,2}]";
    }
}
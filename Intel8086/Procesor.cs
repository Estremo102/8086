using System;

namespace Intel8086
{
    public class Procesor
    {
        private byte[] register = new byte[8];
        public string AH { get => ToHex(register[0]); private set => register[0] = (byte)ToDecimal(value); }
        public string AL { get => ToHex(register[1]); private set => register[1] = (byte)ToDecimal(value); }
        public string BH { get => ToHex(register[2]); private set => register[2] = (byte)ToDecimal(value); }
        public string BL { get => ToHex(register[3]); private set => register[3] = (byte)ToDecimal(value); }
        public string CH { get => ToHex(register[4]); private set => register[4] = (byte)ToDecimal(value); }
        public string CL { get => ToHex(register[5]); private set => register[5] = (byte)ToDecimal(value); }
        public string DH { get => ToHex(register[6]); private set => register[6] = (byte)ToDecimal(value); }
        public string DL { get => ToHex(register[7]); private set => register[7] = (byte)ToDecimal(value); }

        delegate void Operation(int a, int b);
        delegate void OperationSR(int a);

        public Procesor() { }

        public Procesor(int seed)
        {
            Random random = new Random(seed);
            for (int i = 0; i < register.Length; i++)
            {
                register[i] = (byte)random.Next(256);
            }
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
            if (((data[0] >= 48 && data[0] <= 57) || (data[0] >= 65 && data[0] <= 70)) && ((data[1] >= 48 && data[1] <= 57) || (data[1] >= 65 && data[1] <= 70)))
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
            Operation o = null;
            OperationSR osr = null;
            switch (a[0])
            {
                case "MOV":
                    o = MOV;
                    break;
                case "XCHG":
                    o = XCHG;
                    break;
                case "INC":
                    osr = INC;
                    break;
                case "DEC":
                    osr = DEC;
                    break;
                case "NOT":
                    osr = NOT;
                    break;
                case "NEG":
                    osr = NOT;
                    osr += INC;
                    break;
                default:
                    return false;
            }
            if (o == null)
            {
                a = a[1].Split(',');
                if (!CheckRegister(a[0])) return false;
                osr(RegisterToInt(a[0]));
                return true;
            }
            else
            {
                a = a[1].Split(',');
                if (!CheckRegister(a[0]) || !CheckRegister(a[1])) return false;
                o(RegisterToInt(a[0]), RegisterToInt(a[1]));
                return true;
            }
        }

        void MOV(int a, int b) => register[a] = register[b];
        void XCHG(int a, int b)
        {
            byte temp = register[a];
            register[a] = register[b];
            register[b] = temp;
        }

        void INC(int a) => register[a]++;
        void DEC(int a) => register[a]--;
        void NOT(int a) => register[a] = (byte)~register[a];

        static int RegisterToInt(string r)
        {
            return r switch
            {
                "AH" => 0,
                "AL" => 1,
                "BH" => 2,
                "BL" => 3,
                "CH" => 4,
                "CL" => 5,
                "DH" => 6,
                "DL" => 7,
                _ => -1,
            };
        }

        public override string ToString() => $"AX: AH[{AH,2}] AL[{AL,2}]\n" +
                                             $"BX: BH[{BH,2}] BL[{BL,2}]\n" +
                                             $"CX: CH[{CH,2}] CL[{CL,2}]\n" +
                                             $"DX: DH[{DH,2}] DL[{DL,2}]";
    }
}
using System;

namespace Intel8086
{
    public class Procesor
    {
        private byte[] register = new byte[8];
        private ushort[] addressRegister = new ushort[3];
        public string AH { get => ToHex(register[0]); private set => register[0] = (byte)ToDecimal(value); }
        public string AL { get => ToHex(register[1]); private set => register[1] = (byte)ToDecimal(value); }
        public string BH { get => ToHex(register[2]); private set => register[2] = (byte)ToDecimal(value); }
        public string BL { get => ToHex(register[3]); private set => register[3] = (byte)ToDecimal(value); }
        public string CH { get => ToHex(register[4]); private set => register[4] = (byte)ToDecimal(value); }
        public string CL { get => ToHex(register[5]); private set => register[5] = (byte)ToDecimal(value); }
        public string DH { get => ToHex(register[6]); private set => register[6] = (byte)ToDecimal(value); }
        public string DL { get => ToHex(register[7]); private set => register[7] = (byte)ToDecimal(value); }
        public string SI { get => ToHex(addressRegister[0]); private set => addressRegister[0] = (ushort)ToDecimal(value); }
        public string DI { get => ToHex(addressRegister[1]); private set => addressRegister[1] = (ushort)ToDecimal(value); }
        public string BP { get => ToHex(addressRegister[2]); private set => addressRegister[2] = (ushort)ToDecimal(value); }
        public string BX { get => ToHex(register[2]) + ToHex(register[3]); }

        public Memory memory { get; set; } = new Memory();

        delegate void Operation(int a, int b);
        delegate void OperationDR(string a, int b);
        delegate void OperationRD(int a, string b);
        delegate void OperationSR(int a);
        delegate void OperationSRD(string a);

        public Procesor() { }

        public Procesor(int seed)
        {
            Random random = new Random(seed);
            for (int i = 0; i < register.Length; i++)
                register[i] = (byte)random.Next(256);
            for (int i = 0; i < addressRegister.Length; i++)
                addressRegister[i] = (ushort)random.Next(65536);
        }

        public Procesor(params string[] registers)
        {
            if (registers.Length != 8 && registers.Length != 11) throw new ArgumentException();
            foreach (var register in registers) if (!CheckData(register)) throw new ArgumentException();
            AH = registers[0];
            AL = registers[1];
            BH = registers[2];
            BL = registers[3];
            CH = registers[4];
            CL = registers[5];
            DH = registers[6];
            DL = registers[7];
            if (registers.Length == 11)
            {
                SI = registers[8];
                DI = registers[9];
                BP = registers[10];
            }
        }

        public static string ToHex(byte x) => x.ToString("x2").ToUpper();
        public static string ToHex(ushort x) => x.ToString("x4").ToUpper();

        public static int ToDecimal(string x) => Convert.ToInt32(x, 16);

        public static bool CheckData(string data)
        {
            try
            {
                Convert.ToInt32(data, 16);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckData(string data, int length)
        {
            if (data.Length != length) return false;
            return CheckData(data);
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
            OperationDR odr = null;
            OperationRD ord = null;
            OperationSR osr = null;
            OperationSRD osrD = null;
            switch (a[0])
            {
                case "MOV":
                    o = MOV;
                    odr = MOV;
                    ord = MOV;
                    break;
                case "XCHG":
                    o = XCHG;
                    ord = XCHG;
                    odr = XCHG;
                    break;
                case "INC":
                    osr = INC;
                    osrD = INC;
                    break;
                case "DEC":
                    osr = DEC;
                    osrD = DEC;
                    break;
                case "NOT":
                    osr = NOT;
                    osrD = NOT;
                    break;
                case "NEG":
                    osr = NOT;
                    osr += INC;
                    osrD = NOT;
                    osrD += INC;
                    break;
                case "AND":
                    o = AND;
                    ord = AND;
                    odr = AND;
                    break;
                case "OR":
                    o = OR;
                    ord = OR;
                    odr = OR;
                    break;
                case "XOR":
                    o = XOR;
                    ord = XOR;
                    odr = XOR;
                    break;
                case "ADD":
                    o = ADD;
                    ord = ADD;
                    odr = ADD;
                    break;
                case "SUB":
                    o = SUB;
                    ord = SUB;
                    odr = SUB;
                    break;
                default:
                    return false;
            }
            if (o == null)
            {
                a = a[1].Split(',');
                if (!CheckRegister(a[0]))
                {
                    if (!Memory.CheckAddress(a[0])) return false;
                    osrD(a[0]);
                    return true;
                }
                osr(RegisterToInt(a[0]));
                return true;
            }
            else
            {
                a = a[1].Split(',');
                if (!CheckRegister(a[0]) || !CheckRegister(a[1]))
                {
                    if (CheckRegister(a[0]) && Memory.CheckAddress(a[1]))
                    {
                        ord(RegisterToInt(a[0]), a[1]);
                        return true;
                    }
                    if (Memory.CheckAddress(a[0]) && CheckRegister(a[1]))
                    {
                        odr(a[0], RegisterToInt(a[1]));
                        return true;
                    }
                    return false;
                }
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
        void AND(int a, int b) => register[a] = (byte)(register[a] & register[b]);
        void OR(int a, int b) => register[a] = (byte)(register[a] | register[b]);
        void XOR(int a, int b) => register[a] = (byte)(register[a] ^ register[b]);
        void ADD(int a, int b) => register[a] += register[b];
        void SUB(int a, int b) => register[a] -= register[b];


        void INC(string a) => memory.data[Memory.StringToAddress(a, this)]++;
        void DEC(string a) => memory.data[Memory.StringToAddress(a, this)]--;
        void NOT(string a) => memory.data[Memory.StringToAddress(a, this)] = (byte)~memory.data[Memory.StringToAddress(a, this)];
        void MOV(int a, string b) => register[a] = memory.data[Memory.StringToAddress(b, this)];
        void XCHG(int a, string b)
        {
            byte temp = register[a];
            register[a] = memory.data[Memory.StringToAddress(b, this)];
            memory.data[Memory.StringToAddress(b, this)] = temp;
        }
        void AND(int a, string b) => register[a] = (byte)(register[a] & memory.data[Memory.StringToAddress(b, this)]);
        void OR(int a, string b) => register[a] = (byte)(register[a] | memory.data[Memory.StringToAddress(b, this)]);
        void XOR(int a, string b) => register[a] = (byte)(register[a] ^ memory.data[Memory.StringToAddress(b, this)]);
        void ADD(int a, string b) => register[a] += memory.data[Memory.StringToAddress(b, this)];
        void SUB(int a, string b) => register[a] -= memory.data[Memory.StringToAddress(b, this)];
        void MOV(string a, int b) => memory.data[Memory.StringToAddress(a, this)] = register[b];
        void XCHG(string a, int b)
        {
            byte temp = memory.data[Memory.StringToAddress(a, this)];
            memory.data[Memory.StringToAddress(a, this)] = register[b];
            register[b] = temp;
        }
        void AND(string a, int b) => memory.data[Memory.StringToAddress(a, this)] = Convert.ToByte(memory.data[Memory.StringToAddress(a, this)] & register[b]);
        void OR(string a, int b) => memory.data[Memory.StringToAddress(a, this)] = Convert.ToByte(memory.data[Memory.StringToAddress(a, this)] | register[b]);
        void XOR(string a, int b) => memory.data[Memory.StringToAddress(a, this)] = Convert.ToByte(memory.data[Memory.StringToAddress(a, this)] ^ register[b]);
        void ADD(string a, int b) => memory.data[Memory.StringToAddress(a, this)] += register[b];
        void SUB(string a, int b) => memory.data[Memory.StringToAddress(a, this)] -= register[b];

        static int RegisterToInt(string r) =>
            r switch
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

        public string AddressRegisters() => $"SI: [{SI}]\n" +
                                            $"DI: [{DI}]\n" +
                                            $"BP: [{BP}]\n" +
                                            $"BX: [{BX}]";

        public override string ToString() => $"AX: AH[{AH,2}] AL[{AL,2}]\n" +
                                             $"BX: BH[{BH,2}] BL[{BL,2}]\n" +
                                             $"CX: CH[{CH,2}] CL[{CL,2}]\n" +
                                             $"DX: DH[{DH,2}] DL[{DL,2}]";
    }
}
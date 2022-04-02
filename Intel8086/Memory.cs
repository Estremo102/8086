using System;
using System.Text;
using System.IO;

namespace Intel8086
{
    public class Memory
    {
        public byte[] data = new byte[65536];
        public Memory(int seed)
        {
            Random random = new Random(seed);
            for (int i = 0; i < data.Length; i++)
                data[i] = (byte)random.Next(256);
        }
        public Memory() { }

        public static bool CheckAddress(string address)
        {
            if (address[0] != '[' || address[address.Length - 1] != ']') return false;
            address = address.Substring(1, address.Length - 2);
            string[] tmp = address.Split('+');
            if (tmp.Length > 3) return false;
            bool a = false;
            bool b = false;
            bool c = false;
            foreach (string s in tmp)
            {
                try
                {
                    if (s.Length == 4 && !a)
                    {
                        Convert.ToInt32(s, 16);
                        a = true;
                    }
                    else throw new Exception();
                }
                catch
                {
                    if ((s == "SI" || s == "DI") && !c)
                    {
                        c = true;
                        continue;
                    }
                    if ((s == "BP" || s == "BX") && !b)
                    {
                        b = true;
                        continue;
                    }
                    return false;
                }
            }
            return true;
        }

        public static int StringToAddress(string address, Procesor p)
        {
            address = address.Substring(1, address.Length - 2);
            string[] tmp = address.Split('+');
            ushort adr = 0;
            foreach (string s in tmp)
            {
                switch (s)
                {
                    case "SI":
                        adr += (ushort)Procesor.ToDecimal(p.SI);
                        break;
                    case "DI":
                        adr += (ushort)Procesor.ToDecimal(p.DI);
                        break;
                    case "BP":
                        adr += (ushort)Procesor.ToDecimal(p.BP);
                        break;
                    case "BX":
                        adr += (ushort)Procesor.ToDecimal(p.BX);
                        break;
                    default:
                        adr += (ushort)Procesor.ToDecimal(s);
                        break;
                }
            }
            return adr;
        }

        public void Save()
        {
            Save("data.8086");
        }
        public void Save(string fileName)
        {
            string[] tmp = fileName.Split('.');
            if(tmp[tmp.Length - 1] == "txt")
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.Write(this.ToString());
                }
                return;
            }
            using (BinaryWriter w = new BinaryWriter(File.Create(fileName)))
            {
                w.Write(data);
            }
        }

        public void Load()
        {
            Load("data.8086");
        }
        public void Load(string fileName)
        {
            using (BinaryReader r = new BinaryReader(File.OpenRead(fileName)))
            {
                data = r.ReadBytes(data.Length);
            }
        }

        public string DisplayData(string s) => (Convert.ToInt32(s, 16).ToString("x4") + " " + data[Convert.ToInt32(s, 16)].ToString("x2")).ToUpper();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sb.AppendLine($"{i.ToString("x4").ToUpper()} {data[i].ToString("x2").ToUpper()}");
            return sb.ToString();
        }
    }
}

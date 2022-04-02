using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Intel8086;
using Microsoft.Win32;

namespace SymulatorIntel8086
{
    public partial class MainWindow : Window
    {
        Procesor proc;
        Memory mem;
        public MainWindow()
        {
            InitializeComponent();
            ChooseOperation.Items.Add("MOV");
            ChooseOperation.Items.Add("XCHG");
            ChooseOperation.Items.Add("INC");
            ChooseOperation.Items.Add("DEC");
            ChooseOperation.Items.Add("NOT");
            ChooseOperation.Items.Add("NEG");
            ChooseOperation.Items.Add("AND");
            ChooseOperation.Items.Add("OR");
            ChooseOperation.Items.Add("XOR");
            ChooseOperation.Items.Add("ADD");
            ChooseOperation.Items.Add("SUB");
            Register1.Items.Add("AH");
            Register1.Items.Add("AL");
            Register1.Items.Add("BH");
            Register1.Items.Add("BL");
            Register1.Items.Add("CH");
            Register1.Items.Add("CL");
            Register1.Items.Add("DH");
            Register1.Items.Add("DL");
            Register2.Items.Add("AH");
            Register2.Items.Add("AL");
            Register2.Items.Add("BH");
            Register2.Items.Add("BL");
            Register2.Items.Add("CH");
            Register2.Items.Add("CL");
            Register2.Items.Add("DH");
            Register2.Items.Add("DL");
            proc = new Procesor();
            mem = new Memory();
            proc.memory = mem;
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                proc = new Procesor(AH.Text, AL.Text, BH.Text, BL.Text, CH.Text, CL.Text, DH.Text, DL.Text, SI.Text, DI.Text, BP.Text);
                RefreshRegisters();
                proc.memory = mem;
            }
            catch (ArgumentException)
            {
                proc = new Procesor();
                proc.memory = mem;
                RefreshRegisters(false);
            }
        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            if (proc.ExecuteOperation($"{ChooseOperation.SelectedItem} {Register1.SelectedItem},{Register2.SelectedItem}"))
            {
                RefreshRegisters();
            }
            else
                MessageBox.Show("Proszę wybrać operację oraz sektory");
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            proc = new Procesor(Convert.ToInt32(DateTime.Now.Millisecond));
            proc.memory = mem;
            RefreshRegisters();
        }

        private void ChooseOperation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChoosenOperation()) Reg2.Visibility = Visibility.Hidden;
            else Reg2.Visibility = Visibility.Visible;
        }

        bool ChoosenOperation()
        {
            string op = ChooseOperation.SelectedItem.ToString();
            return op == "INC" || op == "DEC" || op == "NOT" || op == "NEG";
        }

        private void ExecuteAssembler_Click(object sender, RoutedEventArgs e)
        {
            string[] commands = AssemblerBox.Text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string cmd in commands)
                if (!proc.ExecuteOperation(cmd))
                {
                    AssemblerBox.Text = "Napotkano błąd w:\n" + cmd + "\n\n" + AssemblerBox.Text;
                    RefreshRegisters();
                    return;
                }
            RefreshRegisters();
        }

        private void RefreshRegisters(bool success = true)
        {
            if (success)
            {
                RegistersView.Text = "REJESTRY\n" + proc.ToString();
                AddressRegistersView.Text = "REJESTRY ADRESOWE\n" + proc.AddressRegisters();
            }
            else
            {
                RegistersView.Text = "BŁĘDNE DANE\n" + proc.ToString();
                AddressRegistersView.Text = "BŁĘDNE DANE\n" + proc.AddressRegisters();
            }
        }

        private void RandomData_Click(object sender, RoutedEventArgs e)
        {
            mem = new Memory(Convert.ToInt32(DateTime.Now.Millisecond));
            proc.memory = mem;
        }

        private void ShowData_Click(object sender, RoutedEventArgs e)
        {
            if (Procesor.CheckData(DataAddress.Text))
                DataViewSingle.Text = "PAMIĘĆ\n" + mem.DisplayData(DataAddress.Text);
            else
                DataViewSingle.Text = "ZŁY ADRES";
        }

        private void InsertData_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "data";
            ofd.DefaultExt = ".8086";
            ofd.Filter = "intel 8086 data file|*.8086";
            Nullable<bool> result = ofd.ShowDialog();
            if (result == true)
            {
                string fn = ofd.FileName;
                mem.Load(fn);
            }
        }

        private void SaveData_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "data";
            sfd.DefaultExt = ".8086";
            sfd.Filter = "intel 8086 data file|*.8086|plik tekstowy|*.txt";
            if (sfd.ShowDialog() == true)
                    mem.Save(sfd.FileName);
        }
    }
}

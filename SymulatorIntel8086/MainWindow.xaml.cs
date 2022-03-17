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

namespace SymulatorIntel8086
{
    public partial class MainWindow : Window
    {
        Procesor proc;
        public MainWindow()
        {
            InitializeComponent();
            ChooseOperation.Items.Add("MOV");
            ChooseOperation.Items.Add("XCHG");
            ChooseOperation.Items.Add("INC");
            ChooseOperation.Items.Add("DEC");
            ChooseOperation.Items.Add("NOT");
            ChooseOperation.Items.Add("NEG");
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
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                proc = new Procesor(AH.Text, AL.Text, BH.Text, BL.Text, CH.Text, CL.Text, DH.Text, DL.Text);
                RegistersView.Text = "REJESTRY\n" + proc.ToString();
            }
            catch (ArgumentException)
            {
                proc = new Procesor();
                RegistersView.Text = "BŁĘDNE DANE\n" + proc.ToString();
            }
        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            if (proc.ExecuteOperation($"{ChooseOperation.SelectedItem} {Register1.SelectedItem},{Register2.SelectedItem}"))
                RegistersView.Text = "REJESTRY\n" + proc.ToString();
            else
                MessageBox.Show("Proszę wybrać operację oraz sektory");
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            proc = new Procesor(Convert.ToInt32(DateTime.Now.Millisecond));
            RegistersView.Text = "REJESTRY\n" + proc.ToString();
        }

        private void ChooseOperation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Register2.IsEnabled = !ChoosenOperation();
            if (ChoosenOperation()) Reg2.Visibility = Visibility.Hidden;
            else Reg2.Visibility = Visibility.Visible;
        }

        bool ChoosenOperation()
        {
            string op = ChooseOperation.SelectedItem.ToString();
            return op == "INC" || op == "DEC" || op == "NOT" || op =="NEG";
                
        }
    }
}

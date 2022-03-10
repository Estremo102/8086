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
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                proc = new Procesor(AH.Text, AL.Text, BH.Text, BL.Text, CH.Text, CL.Text, DH.Text, DL.Text);
                RegistersView.Text = "REJESTRY\n" + proc.ToString();
            }
            catch(ArgumentException)
            {
                proc = new Procesor();
                RegistersView.Text = "PODANO BŁĘDNE DANE\n" + proc.ToString();
            }
        }
    }
}

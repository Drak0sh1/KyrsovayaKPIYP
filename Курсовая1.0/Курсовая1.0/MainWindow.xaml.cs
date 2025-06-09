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

namespace Курсовая1._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PasswordBox_Click(object sender, MouseButtonEventArgs e)
        {
            PasswordBox.Password = "";
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            if(PasswordBox.Password =="13022007")
            {
                new ProgramMain().Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный пароль");
            }
        }
    }
}

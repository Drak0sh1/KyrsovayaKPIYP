using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace Курсовая1._0
{
    /// <summary>
    /// Логика взаимодействия для EditTeacherPage.xaml
    /// </summary>
    public partial class EditTeacherPage : Window
    {
        public EditTeacherPage()
        {
            InitializeComponent();
        }
        private Teachers teach;
        public EditTeacherPage(Teachers teacher)
        {
            InitializeComponent();
            this.teach = teacher;
            TeacherName.Text = teacher.Name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string teachName = TeacherName.Text;
            Teachers teacher = new Teachers();
            teach.Name = teachName;
            KBPClassBetaEntities1.GetContext().SaveChanges();
            this.DialogResult = true;
            this.Close();
        }
    }
}

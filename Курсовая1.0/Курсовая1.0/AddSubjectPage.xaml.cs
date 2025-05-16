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
    /// Логика взаимодействия для AddSubjectPage.xaml
    /// </summary>
    public partial class AddSubjectPage : Window
    {
        public AddSubjectPage()
        {
            InitializeComponent();
            List<String> teacher = new List<String>();
            teacher = KBPClassBetaEntities1.GetContext().Teachers.Select(n => n.Name).ToList();
            Teacher.ItemsSource = teacher;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Disciplines subject = new Disciplines();
            if(Subject.Text == "")
            {
                MessageBox.Show("Введите название предмета");
            }
            else if(int.TryParse(Subject.Text, out int p))
            {
                MessageBox.Show("Поле не должно содержать цифры");
            }
            else if (!int.TryParse(Hour.Text, out int t))
            {
                MessageBox.Show("Введите корректное кол-во часов");
            }
            else if(Teacher.SelectedItem == null)
            {
                MessageBox.Show("Заполните поле учителя");
            }
            else
            {
                string teach = Teacher.SelectedItem.ToString();
                string subjName = Subject.Text;
                int countHour = Convert.ToInt32(Hour.Text);
                Disciplines discipline = new Disciplines();
                discipline.Name = subjName;
                discipline.IDTeacher = KBPClassBetaEntities1.GetContext().Teachers.FirstOrDefault(m => m.Name == teach).IDTeacher;
                discipline.Hours = countHour;
                KBPClassBetaEntities1.GetContext().Disciplines.Add(discipline);
                KBPClassBetaEntities1.GetContext().SaveChanges();
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}

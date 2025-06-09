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
using System.Windows.Shapes;

namespace Курсовая1._0
{
    /// <summary>
    /// Логика взаимодействия для EditSubjectPage.xaml
    /// </summary>
    public partial class EditSubjectPage : Window
    {
        public EditSubjectPage()
        {
            InitializeComponent();
        }
        SubjectToTeacher d;
        public EditSubjectPage(SubjectToTeacher subject)
        {
            InitializeComponent();
            d = subject;
            var tech = KBPClassBetaEntities1.GetContext().Teachers.Select(cc => cc.Name).ToList();
            Teacher.ItemsSource = tech;
            Subject.Text=subject.Name;
            Hours.Text=subject.Hour.ToString();
            foreach (var c in Teacher.ItemsSource)
            {
                if (c.ToString() == subject.idTeacher) 
                {
                    Teacher.SelectedItem = c;
                    break;
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string teacherName = Teacher.Text;
                string subjc = Subject.Text;
                int hours = Convert.ToInt32(Hours.Text);
                var disp = KBPClassBetaEntities1.GetContext().Disciplines.FirstOrDefault(c => c.IDDisciplines == d.id);
                disp.IDTeacher = KBPClassBetaEntities1.GetContext().Teachers.FirstOrDefault(c => c.Name == teacherName).IDTeacher;
                disp.Name = subjc;
                disp.Hours = hours;
                KBPClassBetaEntities1.GetContext().SaveChanges();
                this.DialogResult = true;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Введите коректные данные");
            }
        }
    }
}

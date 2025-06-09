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
    /// Логика взаимодействия для EditTaskPage.xaml
    /// </summary>
    public partial class EditTaskPage : Window
    {
        public EditTaskPage()
        {
            InitializeComponent();
        }
        TaskForTeacher tft;
        public EditTaskPage(TaskForTeacher tasking)
        {
            InitializeComponent();
            tft = tasking;
            var tech = KBPClassBetaEntities1.GetContext().Teachers.Select(cc => cc.Name).ToList();
            TeacherComboBox.ItemsSource = tech;
            IssuanceDate.Text = tasking.issuence;
            DueDate.Text = tasking.due;
            DescriptionTextBox.Text = tasking.descript;
            foreach (var c in TeacherComboBox.ItemsSource)
            {
                if (c.ToString() == tasking.nameTeacher)
                {
                    TeacherComboBox.SelectedItem = c;
                    break;
                }

            }
        }

        private void AddTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            string teacherName = TeacherComboBox.Text;
            DateTime issuenseD = IssuanceDate.SelectedDate.Value;
            DateTime dueD = DueDate.SelectedDate.Value;
            string ttask = DescriptionTextBox.Text;
            var tasks = KBPClassBetaEntities1.GetContext().Tasks.FirstOrDefault(c => c.IDTask == tft.id);
            tasks.IDTeacher = KBPClassBetaEntities1.GetContext().Teachers.FirstOrDefault(c => c.Name == teacherName).IDTeacher;
            tasks.IssuanceDate = issuenseD;
            tasks.DueDate = dueD;
            tasks.Description = ttask;
            KBPClassBetaEntities1.GetContext().SaveChanges();
            this.DialogResult = true;
            this.Close();
        }
    }
}

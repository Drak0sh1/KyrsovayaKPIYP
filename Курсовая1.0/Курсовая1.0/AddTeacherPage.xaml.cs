using System.Linq;
using System.Windows;

namespace Курсовая1._0
{
    /// <summary>
    /// Логика взаимодействия для AddTeacherPage.xaml
    /// </summary>
    public partial class AddTeacherPage : Window
    {
        public AddTeacherPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Teachers teacher = new Teachers();
            bool f = false;
            var teachers = KBPClassBetaEntities1.GetContext().Teachers.ToList();
            foreach (var t in teachers)
            {
                if (t.Name.Trim().CompareTo(TeacherName.Text.TrimEnd()) == 0)
                {
                    f = true;
                }
            }
            if (TeacherName.Text == "")
            {
                MessageBox.Show("Введите имя преподавателя");
            }
            else if (int.TryParse(TeacherName.Text, out int p))
            {
                MessageBox.Show("Поле не должно содержать цифры");
            }
            else if (f) 
            {
                MessageBox.Show($"Преподаватель {TeacherName.Text} уже добавлен");
            }
            else 
            { 
                teacher.Name = TeacherName.Text.TrimEnd();
                KBPClassBetaEntities1.GetContext().Teachers.Add(teacher);
                KBPClassBetaEntities1.GetContext().SaveChanges();
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}

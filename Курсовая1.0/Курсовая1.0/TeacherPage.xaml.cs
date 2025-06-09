using System;
using System.Linq;
using System.Windows;

namespace Курсовая1._0
{
    /// <summary>
    /// Логика взаимодействия для TeacherPage.xaml
    /// </summary>
    public partial class TeacherPage : Window
    {
        public TeacherPage()
        {
            InitializeComponent();
            TeacherGrid.ItemsSource = KBPClassBetaEntities1.GetContext().Teachers.ToList();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            ProgramMain programMain = new ProgramMain();
            programMain.Show();
            this.Close();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddTeacherPage addTeacherPage = new AddTeacherPage();
            if (addTeacherPage.ShowDialog() == true)
            {
                TeacherGrid.ItemsSource = KBPClassBetaEntities1.GetContext().Teachers.ToList();
            }
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            var buttom = sender as System.Windows.Controls.Button;
            var curTeacher = buttom?.DataContext as Teachers;
            EditTeacherPage addedit = new EditTeacherPage(curTeacher);
            if (addedit.ShowDialog() == true)
            {
                TeacherGrid.ItemsSource = KBPClassBetaEntities1.GetContext().Teachers.ToList();
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            //var teacherForRemoving = TeacherGrid.SelectedItems.Cast<Teachers>().ToList();

            //if (MessageBox.Show($"Вы точно хотите удалить следующие {teacherForRemoving.Count} элементов?", "Внимание",
            //    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //{
            //    try
            //    {
            //        KBPClassBetaEntities1.GetContext().Teachers.RemoveRange(teacherForRemoving);
            //        KBPClassBetaEntities1.GetContext().SaveChanges();
            //        MessageBox.Show("Данные удалены");

            //        TeacherGrid.ItemsSource = KBPClassBetaEntities1.GetContext().Teachers.ToList();
            //    }
            //    catch (System.Exception ex)
            //    {
            //        MessageBox.Show(ex.Message.ToString());
            //    }
            //}
            var teachersToDelete = TeacherGrid.SelectedItems.Cast<Teachers>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить {teachersToDelete.Count} преподавателей?",
                               "Подтверждение удаления",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                        foreach (var teacher in teachersToDelete)
                        {
                            var disciplines = KBPClassBetaEntities1.GetContext().Disciplines.Where(d => d.IDTeacher == teacher.IDTeacher).ToList();
                            var tasksByTeacher = KBPClassBetaEntities1.GetContext().Tasks.Where(t => t.IDTeacher == teacher.IDTeacher).ToList();
                            KBPClassBetaEntities1.GetContext().Tasks.RemoveRange(tasksByTeacher);
                            KBPClassBetaEntities1.GetContext().Disciplines.RemoveRange(disciplines);
                            KBPClassBetaEntities1.GetContext().Teachers.Remove(teacher);
                        }
                        KBPClassBetaEntities1.GetContext().SaveChanges();
                        MessageBox.Show("Преподаватель и связанные данные удалены успешно!");
                        TeacherGrid.ItemsSource = KBPClassBetaEntities1.GetContext().Teachers.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}


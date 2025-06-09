using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Button = System.Windows.Controls.Button;
using Word = Microsoft.Office.Interop.Word;
namespace Курсовая1._0
{
    /// <summary>
    /// Логика взаимодействия для ProgramMain.xaml
    /// </summary>


    public partial class ProgramMain : System.Windows.Window
    {
        class WordReplacer
        {
            public void ReplacePlaceholders(string templatePath, string outputPath, string number, string date)
            {
                var wordApp = new Word.Application();
                var document = wordApp.Documents.Open(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templatePath));

                FindAndReplace(wordApp, "{{номер}}", number);
                FindAndReplace(wordApp, "{{дата}}", date);
                document.SaveAs2(outputPath);
                document.Close();
                wordApp.Quit();
                var process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = outputPath,
                    UseShellExecute = true
                };
                process.Start();
            }

            private void FindAndReplace(Word.Application wordApp, string findText, string replaceText)
            {
                wordApp.Selection.Find.ClearFormatting();
                wordApp.Selection.Find.Text = findText;
                wordApp.Selection.Find.Replacement.ClearFormatting();
                wordApp.Selection.Find.Replacement.Text = replaceText;

                object replaceAll = Word.WdReplace.wdReplaceAll;

                wordApp.Selection.Find.Execute(
                    Replace: ref replaceAll,
                    Forward: true,
                    Wrap: Word.WdFindWrap.wdFindContinue
                );
            }
        }

        public ProgramMain()
        {
            InitializeComponent();
            List<String> teacher = new List<String>();
            teacher = KBPClassBetaEntities1.GetContext().Teachers.Select(n => n.Name).ToList();
            taskForTeacher = new ObservableCollection<TaskForTeacher>();
            var taskLoad = KBPClassBetaEntities1.GetContext().Tasks.ToList();
            foreach (var d in taskLoad)
            {
                taskForTeacher.Add(new TaskForTeacher(d));
            }
            TaskGrid.ItemsSource = taskForTeacher;
            TeacherComboBox.ItemsSource = teacher;
            TeacherDoc.ItemsSource = teacher;
            FilterComboBox.ItemsSource = teacher;
            foreach (var d in teacher)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PlansWork");
                path += $"\\{d.TrimEnd()}";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            List<string> MyStringList = new List<string> { "ИТК", "ОКР", "Литература", "Учебная программа" };
            Documents.ItemsSource = MyStringList;
            foreach (var d in teacher)
            {
                foreach (var t in MyStringList)
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PlansWork");
                    path += $"\\{d.TrimEnd()}\\" + $"{t.TrimEnd()}";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
            }
            disciplines = new ObservableCollection<SubjectToTeacher>();
            var disciplinesDistinct = KBPClassBetaEntities1.GetContext().Disciplines.ToList();
            foreach (var d in disciplinesDistinct)
            {
                disciplines.Add(new SubjectToTeacher(d));
            }
            TeacherGrid.ItemsSource = disciplines;

            TeacherFilterComboBox.ItemsSource = KBPClassBetaEntities1.GetContext().Teachers.ToList();

            IssuanceDateFromFilter.SelectedDate = null; //DateTime.Today.AddMonths(-1);
            IssuanceDateToFilter.SelectedDate = null;// DateTime.Today;
            DueDateFromFilter.SelectedDate = null;// DateTime.Today;
            DueDateToFilter.SelectedDate = null;// DateTime.Today.AddMonths(1);

            LoadFilteredData();
        }
        static ObservableCollection<SubjectToTeacher> disciplines;
        static ObservableCollection<TaskForTeacher> taskForTeacher;

        private void TeacherPageBtn_Click(object sender, RoutedEventArgs e)
        {
            TeacherPage teacherPage = new TeacherPage();
            teacherPage.Show();
            this.Close();
        }

        private void SubjectsBtn_Click(object sender, RoutedEventArgs e)
        {
            SubjectPage subjectsPage = new SubjectPage();
            subjectsPage.Show();
            this.Close();
        }
        private void AddTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TeacherComboBox.SelectedItem == null)
                {
                    throw new Exception("Не выбран преподаватель");
                }
                string teachers = TeacherComboBox.SelectedItem.ToString();
                if (!IssuanceDate.SelectedDate.HasValue)
                {
                    throw new Exception("Не выбрана дата выдачи");
                }
                DateTime issuanseD = IssuanceDate.SelectedDate.Value;
                if (!DueDate.SelectedDate.HasValue)
                {
                    throw new Exception("Не выбрана дата сдачи");
                }
                DateTime dueD = DueDate.SelectedDate.Value;
                if (dueD < issuanseD)
                {
                    throw new Exception("Дата сдачи не может быть раньше даты выдачи");
                }

                if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
                {
                    System.Windows.MessageBox.Show("Описание задания не может быть пустым");
                    throw new Exception("Описание задания не может быть пустым");
                }
                Tasks tasks = new Tasks();
                tasks.Description = DescriptionTextBox.Text;
                tasks.IssuanceDate = issuanseD;
                tasks.DueDate = dueD;
                tasks.IDTeacher = KBPClassBetaEntities1.GetContext().Teachers.FirstOrDefault(t => t.Name == teachers).IDTeacher;
                KBPClassBetaEntities1.GetContext().Tasks.Add(tasks);
                try
                {
                    KBPClassBetaEntities1.GetContext().SaveChanges();

                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var error in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in error.ValidationErrors)
                        {
                            System.Windows.MessageBox.Show($"{validationError.PropertyName}\n{validationError.ErrorMessage}");
                        }
                    }
                    throw;
                }
                taskForTeacher = new ObservableCollection<TaskForTeacher>();
                var TaskAdd = KBPClassBetaEntities1.GetContext().Tasks.ToList();
                foreach (var d in TaskAdd)
                {
                    taskForTeacher.Add(new TaskForTeacher(d));
                }
                TaskGrid.ItemsSource = taskForTeacher;


                //string teache = TeacherComboBox.SelectedItem.ToString();

                // Проверка даты выдачи

                //DateTime issuansD = IssuanceDate.SelectedDate.Value;

                // Проверка даты сдачи

                // DateTime dueD = DueDate.SelectedDate.Value;

                // Проверка что дата сдачи не раньше даты выдачи

            }
            catch (Exception ex)
            {
                //System.Windows.MessageBox.Show("Введите коректные данные");
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            var buttom = sender as Button;
            var curTask = buttom?.DataContext as TaskForTeacher;
            EditTaskPage addedit = new EditTaskPage(curTask);
            if (addedit.ShowDialog() == true)
            {
                taskForTeacher = new ObservableCollection<TaskForTeacher>();
                var taskReload = KBPClassBetaEntities1.GetContext().Tasks.ToList();
                foreach (var d in taskReload)
                {
                    taskForTeacher.Add(new TaskForTeacher(d));
                }
                TaskGrid.ItemsSource = taskForTeacher;
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show($"Вы точно хотите удалить данную задачу?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    var but = sender as Button;
                    var tsk = but?.DataContext as TaskForTeacher;
                    var r = KBPClassBetaEntities1.GetContext().Tasks.FirstOrDefault(c => c.IDTask == tsk.id);
                    KBPClassBetaEntities1.GetContext().Tasks.Remove(r);
                    KBPClassBetaEntities1.GetContext().SaveChanges();

                    taskForTeacher = new ObservableCollection<TaskForTeacher>();
                    var taski = KBPClassBetaEntities1.GetContext().Tasks.ToList();
                    foreach (var t in taski)
                    {
                        taskForTeacher.Add(new TaskForTeacher(t));
                    }
                    TaskGrid.ItemsSource = taskForTeacher;
                }
                catch (System.Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void AddProtocolBtn_Click(object sender, RoutedEventArgs e)
        {
            string Unumber = ProtocolNum.Text;
            string Udate = ProtocolDate.Text;
            string outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"Protocols\Протокол_№{0}_{1}.docx", Unumber, Udate));

            var replacer = new WordReplacer();
            replacer.ReplacePlaceholders(
                templatePath: "Подопытный.docx",
                outputPath: outputDir,
                number: Unumber,
                date: Udate
            );
        }

        private void ViewTeachersBtn_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "Состав ЦК.docx";
            try
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Не удалось открыть файл: " + ex.Message);
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PlansWork");
            if ((TeacherDoc.SelectedItem != null && Documents.SelectedItem != null) || (TeacherDoc.SelectedItem != null && Documents.SelectedItem == null))
            {
                if (TeacherDoc.SelectedItem != null)
                {
                    path += "\\" + TeacherDoc.SelectedItem.ToString().TrimEnd();
                }
                if (Documents.SelectedItem != null)
                {
                    path += "\\" + Documents.SelectedItem.ToString();
                }
                Process.Start(path);
            }
            else
            {
                System.Windows.MessageBox.Show("Выберите преподавателя.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (FileBox.SelectedItem != null)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PlansWork");
                path += "\\" + TeacherDoc.SelectedItem.ToString().TrimEnd() + "\\" + Documents.SelectedItem.ToString() + "\\" + FileBox.SelectedItem.ToString();
                if (System.Windows.MessageBox.Show("Вы точно хотите удалить данный файл?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    File.Delete(path);
                    System.Windows.MessageBox.Show("Файл был успешно удаён.");
                }
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PlansWork");
                path += "\\" + TeacherDoc.SelectedItem.ToString().TrimEnd() + "\\" + Documents.SelectedItem.ToString();
                var files = Directory.GetFiles(path);
                FileBox.ItemsSource = files.Select(f => new FileInfo(f).Name).ToList();
            }

        }

        private void DocChange_Change(object sender, SelectionChangedEventArgs e)
        {
            if (TeacherDoc.SelectedItem != null && Documents.SelectedItem != null)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PlansWork");
                path += "\\" + TeacherDoc.SelectedItem.ToString().TrimEnd() + "\\" + Documents.SelectedItem.ToString();
                var files = Directory.GetFiles(path);
                FileBox.ItemsSource = files.Select(f => new FileInfo(f).Name).ToList();
            }
        }

        private void ApplyFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (FilterComboBox.SelectedItem != null)
            {
                var item = FilterComboBox.SelectedItem.ToString();
                var teachId = KBPClassBetaEntities1.GetContext().Teachers.FirstOrDefault(t => t.Name == item).IDTeacher;
                var list = new List<SubjectToTeacher>();
                foreach (var i in KBPClassBetaEntities1.GetContext().Disciplines.Where(d => d.IDTeacher == teachId).ToList())
                {
                    list.Add(new SubjectToTeacher(i));
                }

                TeacherGrid.ItemsSource = list;
            }
        }

        private void ClearFiltersBtn_Click(object sender, RoutedEventArgs e)
        {
            var list = new List<SubjectToTeacher>();
            foreach (var i in KBPClassBetaEntities1.GetContext().Disciplines.ToList())
            {
                list.Add(new SubjectToTeacher(i));
            }

            TeacherGrid.ItemsSource = list;
        }

        private void LoadFilteredData()
        {
            try
            {

                IQueryable<Tasks> query = KBPClassBetaEntities1.GetContext().Tasks;

                // Фильтр по преподавателю
                if (TeacherFilterComboBox.SelectedItem != null)
                {
                    var selectedTeacher = (Teachers)TeacherFilterComboBox.SelectedItem;
                    query = query.Where(t => t.IDTeacher == selectedTeacher.IDTeacher);
                }

                // Фильтр по дате выдачи
                if (IssuanceDateFromFilter.SelectedDate != null)
                {
                    query = query.Where(t => t.IssuanceDate >= IssuanceDateFromFilter.SelectedDate);
                }
                if (IssuanceDateToFilter.SelectedDate != null)
                {
                    query = query.Where(t => t.IssuanceDate <= IssuanceDateToFilter.SelectedDate);
                }

                // Фильтр по сроку сдачи
                if (DueDateFromFilter.SelectedDate != null)
                {
                    query = query.Where(t => t.DueDate >= DueDateFromFilter.SelectedDate);
                }
                if (DueDateToFilter.SelectedDate != null)
                {
                    query = query.Where(t => t.DueDate <= DueDateToFilter.SelectedDate);
                }

                // Загрузка данных с включением связанных сущностей
                var result = query.Include(t => t.Teachers).ToList().Select(t => new TaskForTeacher(t)).ToList();

                TaskGrid.ItemsSource = new ObservableCollection<TaskForTeacher>(result);

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ApplyFiltrBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadFilteredData();
        }

        private void ResetFltBtn_Click(object sender, RoutedEventArgs e)
        {
            TeacherFilterComboBox.SelectedItem = null;
            IssuanceDateFromFilter.SelectedDate = null;//DateTime.Today.AddMonths(-1);
            IssuanceDateToFilter.SelectedDate = null;//DateTime.Today;
            DueDateFromFilter.SelectedDate = null;//DateTime.Today;
            DueDateToFilter.SelectedDate = null;//DateTime.Today.AddMonths(1);

            LoadFilteredData();
        }
    }

}

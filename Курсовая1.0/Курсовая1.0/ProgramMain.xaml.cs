using System.Windows;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;


using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Collections.ObjectModel;
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
                var document = wordApp.Documents.Open(templatePath);

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
        }
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
            TextRange textRange = new TextRange(Descript.Document.ContentStart, Descript.Document.ContentEnd);
            string teacher = TeacherComboBox.SelectedItem.ToString();
            DateTime issuanseD = IssuanceDate.SelectedDate.Value;
            DateTime dueD = DueDate.SelectedDate.Value;
            string descr = textRange.Text;
            Tasks tasks = new Tasks();
            tasks.Description = descr;
            tasks.IssuanceDate= issuanseD;
            tasks.DueDate= dueD;
            tasks.IDTeacher = KBPClassBetaEntities1.GetContext().Teachers.FirstOrDefault(t => t.Name == teacher).IDTeacher;
            KBPClassBetaEntities1.GetContext().Tasks.Add(tasks);
            KBPClassBetaEntities1.GetContext().SaveChanges();
            taskForTeacher = new ObservableCollection<TaskForTeacher>();
            var TaskAdd = KBPClassBetaEntities1.GetContext().Tasks.ToList();
            foreach (var d in TaskAdd)
            {
                taskForTeacher.Add(new TaskForTeacher(d));
            }
            TaskGrid.ItemsSource = taskForTeacher;
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddProtocolBtn_Click(object sender, RoutedEventArgs e)
        {
            string Unumber = ProtocolNum.Text;
            string Udate = ProtocolDate.Text;
            string outputDir = string.Format(@"C:\Papka\Протокол_№{0}_{1}.docx", Unumber, Udate);

            var replacer = new WordReplacer();
            replacer.ReplacePlaceholders(
                templatePath: @"C:\Users\LocalUSer\Desktop\Подопытный.docx",
                outputPath: outputDir,
                number: Unumber,
                date: Udate
            );
        }

        private void ViewTeachersBtn_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "C:\\Users\\LocalUSer\\Desktop\\Курсовая зачем мне вообще это надо\\Состав ЦК.docx";
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

        
    }
}

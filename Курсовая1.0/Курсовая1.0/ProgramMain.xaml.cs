using System.Windows;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;


using System.Diagnostics;
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

                // Заменяем плейсхолдеры
                FindAndReplace(wordApp, "{{номер}}", number);
                FindAndReplace(wordApp, "{{дата}}", date);
                // Сохраняем в новый файл
                document.SaveAs2(outputPath);
                document.Close();
                wordApp.Quit();
                var process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = outputPath,
                    UseShellExecute = true // обязательно для открытия в Word
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
        }

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

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TeacherComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
    }
}

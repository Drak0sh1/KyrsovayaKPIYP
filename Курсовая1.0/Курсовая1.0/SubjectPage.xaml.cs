using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Курсовая1._0
{
    /// <summary>
    /// Логика взаимодействия для SubjectPage.xaml
    /// </summary>
    public partial class SubjectPage : Window
    {
        public SubjectPage()
        {
            InitializeComponent();
            disciplines = new ObservableCollection<SubjectToTeacher>();
            var disciplinesDistinct = KBPClassBetaEntities1.GetContext().Disciplines.ToList();
            foreach (var d in disciplinesDistinct)
            {
                disciplines.Add(new SubjectToTeacher(d));
            }
            SubjectGrid.ItemsSource = disciplines;


        }
        static ObservableCollection<SubjectToTeacher> disciplines;
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            ProgramMain programMain = new ProgramMain();
            programMain.Show();
            this.Close();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddSubjectPage addSubjectPage = new AddSubjectPage();
            if(addSubjectPage.ShowDialog() == true)
            {
                disciplines = new ObservableCollection<SubjectToTeacher>();
                var disciplinesDistinct = KBPClassBetaEntities1.GetContext().Disciplines.ToList();
                foreach (var d in disciplinesDistinct)
                {
                    disciplines.Add(new SubjectToTeacher(d));
                }
                SubjectGrid.ItemsSource = disciplines;
            }
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            var buttom = sender as Button;
            var curSubject = buttom?.DataContext as SubjectToTeacher;
            EditSubjectPage addedit = new EditSubjectPage(curSubject);
            if (addedit.ShowDialog() == true)
            {
                disciplines = new ObservableCollection<SubjectToTeacher>();
                var disciplinesReload = KBPClassBetaEntities1.GetContext().Disciplines.ToList();
                foreach (var d in disciplinesReload)
                {
                    disciplines.Add(new SubjectToTeacher(d));
                }
                SubjectGrid.ItemsSource = disciplines;
            }

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
           
            if (MessageBox.Show($"Вы точно хотите удалить данный предмет?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    var but = sender as Button;
                    var subj = but?.DataContext as SubjectToTeacher;
                    var r = KBPClassBetaEntities1.GetContext().Disciplines.FirstOrDefault(c => c.IDDisciplines == subj.id);
                    KBPClassBetaEntities1.GetContext().Disciplines.Remove(r);
                    KBPClassBetaEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");


                    disciplines = new ObservableCollection<SubjectToTeacher>();
                    var disciplinesDistinct = KBPClassBetaEntities1.GetContext().Disciplines.ToList();
                    foreach (var d in disciplinesDistinct)
                    {
                        disciplines.Add(new SubjectToTeacher(d));
                    }
                    SubjectGrid.ItemsSource = disciplines;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}

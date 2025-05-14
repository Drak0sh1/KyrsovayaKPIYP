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
    /// Логика взаимодействия для SubjectPage.xaml
    /// </summary>
    public partial class SubjectPage : Window
    {
        public SubjectPage()
        {
            InitializeComponent();
            SubjectGrid.ItemsSource = KBPClassBetaEntities1.GetContext().Disciplines.ToList();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            ProgramMain programMain = new ProgramMain();
            programMain.Show();
            this.Close();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddSubjectPage addSubjectPage = new AddSubjectPage();
            addSubjectPage.Show();
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

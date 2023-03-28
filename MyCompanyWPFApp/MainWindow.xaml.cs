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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyCompanyWPFApp.Windows;

namespace MyCompanyWPFApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        #region Методы для работы с кнопками
        private void QuestionsWindow_Click(object sender, RoutedEventArgs e) //Метод для перехода на страницу с вопросами
        {
            this.Hide();
            QuestionWindow questionsWindow = new QuestionWindow();
            questionsWindow.Show();
        }
        private void ProjectsWindow_Click(object sender, RoutedEventArgs e) //Метод для перехода на страницу с проектами
        {
            this.Hide();
            ProjectWindow projectsWindow = new ProjectWindow();
            projectsWindow.Show();
        }
        private void ServicesWindow_Click(object sender, RoutedEventArgs e) //Метод для перехода на страницу с услугами
        {
            this.Hide();
            ServiceWindow servicesWindow = new ServiceWindow();
            servicesWindow.Show();
        }
        private void BlogWindow_Click(object sender, RoutedEventArgs e) //Метод для перехода на страницу со статьями блога
        {
            this.Hide();
            BlogWindow blogWindow = new BlogWindow();
            blogWindow.Show();
        }
        private void MainTextWindow_Click(object sender, RoutedEventArgs e) //Метод для перехода на страницу с текстовыми полями
        {
            this.Hide();
            TextWindow mainTextWindow = new TextWindow();
            mainTextWindow.Show();
        }
        #endregion
    }
}

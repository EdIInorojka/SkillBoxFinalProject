using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
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
using MyCompanyWPFApp.DataAPI;
using MyCompanyWPFApp.Domain;
using MyCompanyWPFApp.Domain.Entities;

namespace MyCompanyWPFApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        QuestionsDataAPI db; //Подключение контекста бд
        public QuestionWindow()
        {
            InitializeComponent();
            db = new QuestionsDataAPI(); //Инициализация контекста бд
            LoadDb();
            //Добавление значений для выпадающего списка статуса вопроса
            StatusBox.Items.Add("Принят");
            StatusBox.Items.Add("В работе");
            StatusBox.Items.Add("Выполнен");
            StatusBox.Items.Add("Отклонен");
            StatusBox.Items.Add("Отменен");
        }
        private void LoadDb() //Метод для загрузки данных из бд в таблицу
        {
            QuestionsGrid.ItemsSource = null; //Обнуление значений таблицы
            QuestionsGrid.ItemsSource = db.GetQuestions(); //Вставка значений из бд в таблицу
            lblTotalQuestions.Content = $"Всего вопросов: {QuestionsGrid.Items.Count}"; //Вывод кол-ва значений в таблице
        }
        #region Методы для работы с кнопками
        private void MainWindow_Click(object sender, RoutedEventArgs e) //Переход на главное окно
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void SortByDates_Click(object sender, RoutedEventArgs e) //Метод для сортировки значений в таблице по дате добавления
        {
            QuestionsGrid.ItemsSource = null; //Обнуление значений таблицы
            List<Question> SortedQuestionList = new List<Question>();
            DateTime date1 = Convert.ToDateTime(FromDate.SelectedDate); //Дата от которой идет сортировка
            DateTime date2 = Convert.ToDateTime(ToDate.SelectedDate); //Дата до которой идет сортировка
            foreach (Question question in db.GetQuestions()) //Перебор значений в бд в соответствии с датами
            {
                if (question.DateAdded >= date1 && question.DateAdded<=date2)
                {
                    SortedQuestionList.Add(question);
                    QuestionsGrid.ItemsSource = null;
                    QuestionsGrid.ItemsSource = SortedQuestionList;
                }
            }
            lblTotalQuestions.Content = $"Всего вопросов: {SortedQuestionList.Count}";
        }
        private void LoadAll_Click(object sender, RoutedEventArgs e) //Метод для вывода всех значений из бд в таблицу
        {
            LoadDb();
        }
        private void Delete_Click(object sender, RoutedEventArgs e) //Метод для удаления выбранного вопроса
        {
            if (QuestionsGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < QuestionsGrid.SelectedItems.Count; i++)
                {
                    Question question = QuestionsGrid.SelectedItems[i] as Question;
                    if (question != null)
                    {
                        db.DeleteQuestion(question.Id);
                    }
                }
            }
            LoadDb();
        }

        private void SwapStatus_Click(object sender, RoutedEventArgs e) //Метод для изменения статуса выбранного вопроса
        {
            if (QuestionsGrid.SelectedItems.Count > 0)
            {
                Question question = QuestionsGrid.SelectedItem as Question;
                question.QuestionStatus = StatusBox.SelectedValue.ToString();
            }
            Question questions = QuestionsGrid.SelectedItem as Question;
            db.SaveQuestion(questions);
            LoadDb();
        }
        #endregion

    }
}

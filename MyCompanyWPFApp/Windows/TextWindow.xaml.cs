using MyCompanyWPFApp.DataAPI;
using MyCompanyWPFApp.Domain;
using MyCompanyWPFApp.Domain.Entities;
using System;
using System.Collections.Generic;
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

namespace MyCompanyWPFApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для TextWindow.xaml
    /// </summary>
    public partial class TextWindow : Window
    {
        TextFieldsDataAPI db; //Подключение контекста бд
        public TextWindow() 
        {
            InitializeComponent();

            LoadDb();
        }
        private void LoadDb() //метод для загрузки данных в таблицу
        {
            TextFieldsGrid.ItemsSource = null; //Обнуление данных таблицы
            db = new TextFieldsDataAPI(); //Инициализация контекста бд
            TextFieldsGrid.ItemsSource = db.GetTextFields(); //Вставка данных из бд в таблицу
        }
        private void MainWindow_Click(object sender, RoutedEventArgs e) //Метод для перехода на главное окно
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e) //Метод для обновления значений в бд и таблице
        {
            TextField textField = TextFieldsGrid.SelectedItem as TextField;
            db.SaveTextField(textField); //Сохранение изменений в бд
            LoadDb();
        }
    }
}

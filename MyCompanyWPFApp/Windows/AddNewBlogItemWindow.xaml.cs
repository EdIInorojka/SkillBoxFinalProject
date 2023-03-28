using Microsoft.Win32;
using MyCompanyWPFApp.DataAPI;
using MyCompanyWPFApp.Domain;
using MyCompanyWPFApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
using Path = System.IO.Path;

namespace MyCompanyWPFApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddNewBlogItemWindow.xaml
    /// </summary>
    public partial class AddNewBlogItemWindow : Window
    {
        BlogItemsDataAPI db; //Подключение контекста бд
        public AddNewBlogItemWindow()
        {
            InitializeComponent();
            db = new BlogItemsDataAPI(); //Инициализация контеста бд
            LoadDb();
        }
        private void LoadDb() //Метод для загрузки данных из бд в таблицу
        {
            BlogGrid.ItemsSource = null;  //Обнуление данных в таблице
            BlogGrid.ItemsSource = db.GetBlogItems(); //Вставка данных из бд в таблицу
        }
        #region Методы для работы с кнопками
        private void AddNewItem_Click(object sender, RoutedEventArgs e) //Метод для добавления новой услуги
        {
            BlogItem blogItem = new BlogItem();
            blogItem.Id = Guid.NewGuid();
            blogItem.DateAdded = DateTime.Today;
            blogItem.TitleImagePath = Image.Text;
            blogItem.Title = Title.Text;
            blogItem.Subtitle = Subtitle.Text;
            blogItem.Text = ItemText.Text;
            db.SaveBlogItem(blogItem);
            LoadDb();
        }
        private void SelectImage_Click(object sender, RoutedEventArgs e) //Метод для смены изображения в выбранной услуге
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); //Инициализация окна для выбора файла
            openFileDialog.ShowDialog(); //Вывод окна для выбора файла
            string imageName = openFileDialog.FileName; //Значение названия выбранного файла
            string path = Path.GetFullPath(openFileDialog.FileName); //Значение полного пути к выбранному файлу
            string toPath = "C:\\Users\\AV_Kokh\\source\\repos\\MyCompany\\MyCompany\\wwwroot\\images\\"; //Значение пути для конечной папки
            //Проверка есть ли выбранный файл в папке
            try //Если файла нет
            {
                string imageInFolder = toPath + imageName; //Значение конечного полного пути файла

                if (File.Exists(imageInFolder)) //Проверка наличия файла в папке
                {
                    File.Copy(path, toPath + Path.GetFileName(openFileDialog.FileName)); //Если файла нет, копирование файла в папку
                }
            }
            catch (Exception) //Если файл есть
            {
                Console.WriteLine($"Файл {imageName} уже есть в папке");
                return;
            }
            if (BlogGrid.SelectedItems.Count > 0) //Обновление картинки в таблице
            {
                BlogItem blogItem = BlogGrid.SelectedItem as BlogItem;
                blogItem.TitleImagePath = Path.GetFileName(openFileDialog.FileName);
            }
            BlogItem blogitem = BlogGrid.SelectedItem as BlogItem;
            db.SaveBlogItem(blogitem);
            LoadDb();
        }
        
        private void Return_Click(object sender, RoutedEventArgs e) //Метод для возвращения на окно со статьями блога
        {
            this.Hide();
            BlogWindow blogWindow = new BlogWindow();
            blogWindow.Show();
        }
#endregion

    }
}

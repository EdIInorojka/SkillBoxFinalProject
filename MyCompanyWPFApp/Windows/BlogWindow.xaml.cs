using Microsoft.Win32;
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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Net;
using Path = System.IO.Path;
using MyCompanyWPFApp.DataAPI;

namespace MyCompanyWPFApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для BlogWindow.xaml
    /// </summary>
    public partial class BlogWindow : Window
    {
        BlogItemsDataAPI db;  //Подключение контекста бд
        public BlogWindow()
        {
            InitializeComponent();
            db = new BlogItemsDataAPI(); //Инициализация контекста бд
            LoadDb();
        }

        private void LoadDb() //Метод для загрузки данных из бд в таблицу
        {
            BlogGrid.ItemsSource = null; //Обнуление данных в таблице
            BlogGrid.ItemsSource = db.GetBlogItems(); //Вставка данных из бд в таблицу
        }

        #region Методы для работы кнопок
        private void MainWindow_Click(object sender, RoutedEventArgs e)  //Метод для перехода на главное окно
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e) //Метод для обновления данных в бд и таблице
        {
            BlogItem blogItem = BlogGrid.SelectedItem as BlogItem;
            db.SaveBlogItem(blogItem);  //Сохранение изменений в бд
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
            if (BlogGrid.SelectedItems.Count > 0) //Изменение значения картинки в таблице
            {
                BlogItem blogItem = BlogGrid.SelectedItem as BlogItem;
                blogItem.TitleImagePath = Path.GetFileName(openFileDialog.FileName);
            }
            BlogItem blogitem = BlogGrid.SelectedItem as BlogItem;
            db.SaveBlogItem(blogitem); //Сохранение изменений в бд
            LoadDb(); //Обновление таблицы
        }

        private void Delete_Click(object sender, RoutedEventArgs e)  //Метод для удаления выбранной статьи блога
        {
            if (BlogGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < BlogGrid.SelectedItems.Count; i++)
                {
                    BlogItem blogItem = BlogGrid.SelectedItems[i] as BlogItem;
                    if (blogItem != null)
                    {
                        db.DeleteBlogItem(blogItem.Id);
                    }
                }
            }
            LoadDb();
        }

        private void AddNewItems_Click(object sender, RoutedEventArgs e) //Метод для перехода в окно добавления новой статьи блога
        {
            this.Hide();
            AddNewBlogItemWindow addNewBlogItemWindow = new AddNewBlogItemWindow();
            addNewBlogItemWindow.Show();
        }
        #endregion
    }
}

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
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        ServiceItemsDataAPI db; //Подключение контекста бд
        public ServiceWindow()
        {
            InitializeComponent();
            db = new ServiceItemsDataAPI(); //Инициализация контекста бд
            LoadDb();
        }
        #region Методы для работы кнопок
        private void LoadDb() //Метод для загрузки данных из бд в таблицу
        {
            ServicesGrid.ItemsSource = null; //Обнуление данных в таблице
            ServicesGrid.ItemsSource = db.GetServiceItems(); //Вставка данных из бд в таблицу
        }
        private void MainWindow_Click(object sender, RoutedEventArgs e) //Метод для перехода на главное окно
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e) //Метод для обновления данных в бд и таблице
        {
            ServiceItem serviceItem = ServicesGrid.SelectedItem as ServiceItem;
            db.SaveServiceItem(serviceItem); //Сохранение изменений в бд
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
            if (ServicesGrid.SelectedItems.Count > 0) //Изменение значения картинки в таблице
            {
                ServiceItem serviceItem = ServicesGrid.SelectedItem as ServiceItem;
                serviceItem.TitleImagePath = Path.GetFileName(openFileDialog.FileName);
            }
            ServiceItem serviceitem = ServicesGrid.SelectedItem as ServiceItem;
            db.SaveServiceItem(serviceitem); //Сохранение изменений в бд
            LoadDb(); //Обновление таблицы
        }

        private void Delete_Click(object sender, RoutedEventArgs e) //Метод для удаления выбранной услуги
        {
            if (ServicesGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < ServicesGrid.SelectedItems.Count; i++)
                {
                    ServiceItem serviceItem = ServicesGrid.SelectedItems[i] as ServiceItem;
                    if (serviceItem != null)
                    {
                        db.DeleteServiceItem(serviceItem.Id);
                    }
                }
            }
            LoadDb();
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e) //Метод для перехода в окно добавления новой услуги
        {
            this.Hide();
            AddNewServiceItemWindow addNewServiceItemWindow = new AddNewServiceItemWindow();
            addNewServiceItemWindow.Show();
        }
        #endregion
    }
}

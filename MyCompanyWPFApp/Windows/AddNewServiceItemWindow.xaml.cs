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
    /// Логика взаимодействия для AddNewServiceItemWindow.xaml
    /// </summary>
    public partial class AddNewServiceItemWindow : Window
    {
        ServiceItemsDataAPI db; //Подключение контекста бд
        public AddNewServiceItemWindow()
        {
            InitializeComponent();
            db = new ServiceItemsDataAPI();//Инициализация контеста бд
            LoadDb();
        }
        private void LoadDb() //Метод для загрузки данных из бд в таблицу
        {
            ServicesGrid.ItemsSource = null; //Обнуление данных в таблице
            ServicesGrid.ItemsSource = db.GetServiceItems(); //Вставка данных из бд в таблицу
        }
        #region Методы для работы с кнопками
        private void AddNewItem_Click(object sender, RoutedEventArgs e) //Метод для добавления новой услуги
        {
            ServiceItem serviceItem = new ServiceItem();
            serviceItem.Id = Guid.NewGuid();
            serviceItem.DateAdded = DateTime.Today;
            serviceItem.TitleImagePath = Image.Text;
            serviceItem.Title = Title.Text;
            serviceItem.Subtitle = Subtitle.Text;
            serviceItem.Text = ItemText.Text;
            db.SaveServiceItem(serviceItem);
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
            if (ServicesGrid.SelectedItems.Count > 0) //Обновление картинки в таблице
            {
                ServiceItem serviceItem = ServicesGrid.SelectedItem as ServiceItem;
                serviceItem.TitleImagePath = Path.GetFileName(openFileDialog.FileName);
            }
            ServiceItem serviceitem = ServicesGrid.SelectedItem as ServiceItem;
            db.SaveServiceItem(serviceitem);
            LoadDb();
        }
        private void Return_Click(object sender, RoutedEventArgs e) //Метод для возвращения на окно с услугами
        {
            this.Hide();
            ServiceWindow serviceWindow = new ServiceWindow();
            serviceWindow.Show();
        }
#endregion
    }
}

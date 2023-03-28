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
    /// Логика взаимодействия для AddNewProjectItemWindow.xaml
    /// </summary>
    public partial class AddNewProjectItemWindow : Window
    {
        ProjectItemsDataAPI db; //Подключение контекста бд
        public AddNewProjectItemWindow()
        {
            InitializeComponent();
            db = new ProjectItemsDataAPI(); //Инициализация контеста бд
            LoadDb();
        }
        private void LoadDb() //Метод для загрузки данных из бд в таблицу
        {
            ProjectsGrid.ItemsSource = null; //Обнуление данных в таблице
            ProjectsGrid.ItemsSource = db.GetProjectItems(); //Вставка данных из бд в таблицу
        }
        #region Методы для работы с кнопками
        private void AddNewItem_Click(object sender, RoutedEventArgs e) //Метод для добавления новой услуги
        {
            ProjectItem projectItem = new ProjectItem();
            projectItem.Id = Guid.NewGuid();
            projectItem.DateAdded = DateTime.Today;
            projectItem.TitleImagePath = Image.Text;
            projectItem.Title = Title.Text;
            projectItem.Subtitle = Subtitle.Text;
            projectItem.Text = ItemText.Text;
            db.SaveProjectItem(projectItem);
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
            if (ProjectsGrid.SelectedItems.Count > 0) //Обновление картинки в таблице
            {
                ProjectItem projectItem = ProjectsGrid.SelectedItem as ProjectItem;
                projectItem.TitleImagePath = Path.GetFileName(openFileDialog.FileName);
            }
            ProjectItem projectitem = ProjectsGrid.SelectedItem as ProjectItem;
            db.SaveProjectItem(projectitem);
            LoadDb();
        }
        
        private void Return_Click(object sender, RoutedEventArgs e) //Метод для возвращения на окно с проектами
        {
            this.Hide();
            ProjectWindow projectWindow = new ProjectWindow();
            projectWindow.Show();
        }
        #endregion
    }
}

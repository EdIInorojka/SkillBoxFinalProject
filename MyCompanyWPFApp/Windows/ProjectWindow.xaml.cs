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
    /// Логика взаимодействия для ProjectWindow.xaml
    /// </summary>
    public partial class ProjectWindow : Window
    {
        ProjectItemsDataAPI db; //Подключение контекста бд
        public ProjectWindow()
        {
            InitializeComponent();
            db = new ProjectItemsDataAPI(); //Инициализация контекста бд
            LoadDb();
        }
        private void LoadDb() //Метод для загрузки данных из бд в таблицу
        {
            ProjectsGrid.ItemsSource = null; //Обнуление данных в таблице
            ProjectsGrid.ItemsSource = db.GetProjectItems(); //Вставка данных из бд в таблицу
        }
        #region Методы для работы кнопок
        private void MainWindow_Click(object sender, RoutedEventArgs e) //Метод для перехода на главное окно
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e) //Метод для обновления данных в бд и таблице
        {
            ProjectItem projectItem = ProjectsGrid.SelectedItem as ProjectItem;
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
            if (ProjectsGrid.SelectedItems.Count > 0) //Изменение значения картинки в таблице
            {
                ProjectItem projectItem = ProjectsGrid.SelectedItem as ProjectItem;
                projectItem.TitleImagePath = Path.GetFileName(openFileDialog.FileName);
            }
            ProjectItem projectitem = ProjectsGrid.SelectedItem as ProjectItem;
            db.SaveProjectItem(projectitem);
            LoadDb(); //Обновление таблицы
        }

        private void Delete_Click(object sender, RoutedEventArgs e) //Метод для удаления выбранного проекта
        {
            if (ProjectsGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < ProjectsGrid.SelectedItems.Count; i++)
                {
                    ProjectItem projectItem = ProjectsGrid.SelectedItems[i] as ProjectItem;
                    if (projectItem != null)
                    {
                        db.DeleteProjectItem(projectItem.Id);
                    }
                }
            }
            LoadDb();
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e) //Метод для перехода в окно добавления новой услуги
        {
            this.Hide();
            AddNewProjectItemWindow addNewProjectItemWindow = new AddNewProjectItemWindow();
            addNewProjectItemWindow.Show();
        }
        #endregion
    }
}

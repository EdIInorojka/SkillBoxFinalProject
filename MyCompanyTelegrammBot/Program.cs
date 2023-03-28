using Microsoft.VisualBasic;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using MyCompanyTelegramBot.Domain;
using MyCompanyTelegramBot.Domain.Entities;
using System.Data.Entity;
using static System.Net.Mime.MediaTypeNames;
using MyCompanyTelegramBot.DataAPI;
using MyCompanyTelegramBot.DataAPI;

namespace MyCompanyTelegramBot
{
    internal class Program
    {
        
        private static BlogItemsDataAPI dbBlog;
        private static ServiceItemsDataAPI dbService;
        private static ProjectItemsDataAPI dbProject;
        private static string token { get; set; } = "6051548061:AAE-NM7GBYK8hmi20lum-PMaPlO0r8AUcYM";
        static void Main(string[] args)
        {
            var client = new TelegramBotClient(token); //Инициализация бота
            client.StartReceiving(Update, Error); //Начало работы бота
            dbBlog = new BlogItemsDataAPI();
            dbService = new ServiceItemsDataAPI();
            dbProject = new ProjectItemsDataAPI();
            LoadDb(); //Подгрузка данных из бд
            Console.WriteLine("Бот запущен");
            Console.ReadLine();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken Token)
        {
            var message = update.Message; //Получение текста сообщения
            Console.WriteLine($"{message.Chat.FirstName} || сообщение: {message.Text}"); //Вывод полученного сообщения в консоль
            if (message.Text!=null) //Проверка на текстовое значение сообщения (против спама всем, кроме сообщений)
            {
                #region Код для выполнения команд бота
                switch (message.Text) 
                {
                    case ("/start"): //Начальная команда
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Здравствуйте, список команд для бота представлен в меню");
                        break;
                    case ("/services"): //Команда для вывода услуг из бд
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Наши услуги:");
                        foreach (ServiceItem item in dbService.GetServiceItems())
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, Convert.ToString(item.Title));
                        }
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Чтобы узнать об услуге больше, введите название услуги");
                        break;
                    case ("/projects"): //Команда для вывода проектов из бд
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Наши проекты:");
                        foreach (ProjectItem item in dbProject.GetProjectItems())
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, Convert.ToString(item.Title));
                        }
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Чтобы узнать об проекте больше, введите название проекта");
                        break;
                    case ("/blog"): //Команда для вывода статей блога из бд
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Наш блог:");
                        foreach (BlogItem item in dbBlog.GetBlogItems())
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, Convert.ToString(item.Title));
                        }
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Чтобы узнать о статье больше, введите название статьи");
                        break;
                    case ("/contacts"): //Команда для вывода контактов
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Телефон горячей линии: +7 (111) 111-11-11");
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Наш email: contact@mycompany.com");
                        break;
                    case ("/question"): //Команда для добавления вопроса
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Чтобы задать вопрос, перейдите на главную страницу нашего сайта: https://localhost:44340");
                        break;
                    default: //Проверка сообщения при несоответствии командам
                        int count = 0; //Счетчик для проверки услуг/блога/проектов
                        string title = message.Text; 
                        foreach (BlogItem blogItem in dbBlog.GetBlogItems()) //Проверка соответствия сообщения и названия статьи блога
                        {
                            if (blogItem.Title == title)
                            {
                                await botClient.SendTextMessageAsync(message.Chat.Id, blogItem.Subtitle); //Вывод краткого описания статьи
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Чтобы узнать полную информацию, перейдите по ссылке: https://localhost:44340/Blogs");
                                count++;
                            };
                        }
                        foreach (ServiceItem serviceItem in dbService.GetServiceItems()) //Проверка соответствия сообщения и названия услуги
                        {
                            if (serviceItem.Title == title)
                            {
                                await botClient.SendTextMessageAsync(message.Chat.Id, serviceItem.Subtitle); //Вывод краткого описания услуги
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Чтобы узнать полную информацию, перейдите по ссылке: https://localhost:44340/Services");
                                count++;
                            };
                        }
                        foreach (ProjectItem projectItem in dbProject.GetProjectItems()) //Проверка соответствия сообщения и названия проекта
                        {
                            if (projectItem.Title == title)
                            {
                                await botClient.SendTextMessageAsync(message.Chat.Id, projectItem.Subtitle); //Вывод краткого описания проекта
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Чтобы узнать полную информацию, перейдите по ссылке: https://localhost:44340/Projects");
                                count++;
                            };
                        }
                        if (count == 0) //Проверка вывода статьи/услуги/проекта
                        {
                            //Вывод сообщения, если не было соответствий
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Данной команды не существует");
                        }
                           
                        break;
                }
                #endregion
            }
            else
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Бот не принимает не текстовые сообщения");
            }
        }
        private static void LoadDb() //Метод для подгрузки данных из бд
        {
            dbBlog.GetBlogItems();
            dbProject.GetProjectItems();
            dbService.GetServiceItems();
        }
        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}
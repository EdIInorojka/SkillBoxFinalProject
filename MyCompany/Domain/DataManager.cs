using Microsoft.AspNetCore.Localization;
using MyCompany.Domain.Repositories.Abstract;

namespace MyCompany.Domain
{
    public class DataManager
    {
        /// <summary>
        /// Класс для обращения к базе данных
        /// </summary>
        #region Инициализация интерфейсов для страниц
        public ITextFieldsRepository TextFields { get; set; } //Интерфейс для редактирования текста страниц
        public IServiceItemsRepository ServiceItems { get; set; } //Интерфейс для услуги
        public IQuestionsRepository QuestionRepository { get; set; } //Интерфейс для вопросов
        public IProjectItemsRepository ProjectItems { get; set; } //Интерфейс для проектов
        public IBlogItemsRepository BlogItems { get; set; } //Интерфейс для блога
        #endregion

        public DataManager(ITextFieldsRepository textFieldsRepository,
            IServiceItemsRepository serviceItemsRepository,
            IQuestionsRepository questionRepository,
            IProjectItemsRepository projectItemsRepository,
            IBlogItemsRepository blogItemsRepository)
        {
            TextFields = textFieldsRepository;
            ServiceItems = serviceItemsRepository;
            QuestionRepository = questionRepository;
            ProjectItems = projectItemsRepository;
            BlogItems = blogItemsRepository;
        }
    }
}

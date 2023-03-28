namespace MyCompany.Service
{
    public static class Extensions
    {
        /// <summary>
        /// Класс для возврата имени контроллера
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CutController(this string str) //Метод для возврата имени контроллера
        {
            return str.Replace("Controller", "");
        }
    }
}

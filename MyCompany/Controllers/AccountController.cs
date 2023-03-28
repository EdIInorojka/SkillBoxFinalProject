using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Models;

namespace MyCompany.Controllers
{
    /// <summary>
    /// Страница входа для админа
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        #region Инициализация элементов для входа в админ панель
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signinMgr)
        {
            userManager = userMgr;
            signInManager = signinMgr;
        }
        #endregion
        [AllowAnonymous]
        public IActionResult Login(string returnUrl) //Переход на страницу входа
        {
            ViewBag.returnUrl = returnUrl;
            return View(new LoginViewModel());
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl) //Проверка логина и пароля для входа
        {
            if (ModelState.IsValid) //Проверка валидности модели
            {
                IdentityUser user = await userManager.FindByNameAsync(model.UserName); //Проверка логина
                if (user != null) //Вход в пользователя админ
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Неверный логин или пароль"); //Ошибка при неверном логине или пароле
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout() //Выход из панели администратора
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
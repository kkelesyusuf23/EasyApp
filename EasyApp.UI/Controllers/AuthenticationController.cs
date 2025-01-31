using Azure.Core;
using EasyApp.UI.Models;
using EasyApp.UI.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace EasyApp.UI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthenticationController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "AdminDefault");
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    PhoneNumber = model.Phone,
                    UserName = model.Username,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Onay kodu oluştur
                    var verificationCode = new Random().Next(100000, 999999).ToString();
                    user.EmailVerificationCode = verificationCode;
                    user.VerificationCodeExpires = DateTime.UtcNow.AddMinutes(2);

                    // Kullanıcıyı güncelle
                    await _userManager.UpdateAsync(user);

                    // E-posta gönder
                    SendEmail(user, verificationCode);

                    // Doğrulama ekranına yönlendirme
                    TempData["UserId"] = user.Id;
                    return RedirectToAction("VerifyEmail");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        private async Task SendEmail(AppUser user, string verificationCode)
        {
            // Generate email content
            var mailMessage = new MailMessage
            {
                From = new MailAddress("yukeworking@gmail.com"),
                Subject = "E-posta Doğrulama",
                IsBodyHtml = true,
                Body = $@"
<html>
    <body style='font-family: Arial, sans-serif; background-color: #f9f9f9; padding: 20px;'>
        <div style='max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);'>
            <h2 style='background-color: #4CAF50; color: white; padding: 15px; margin: 0; text-align: center;'>E-posta Doğrulama</h2>
            <p style='padding: 15px;'>Merhaba {user.Name},</p>
            <p style='padding: 15px;'>Lütfen aşağıdaki doğrulama kodunu kullanarak kaydınızı tamamlayınız:</p>
            <p style='font-size: 20px; font-weight: bold; text-align: center;'>{verificationCode}</p>
            <p style='padding: 15px;'>Bu kod yalnızca 2 dakika geçerlidir.</p>
            <p style='padding: 15px;'>Teşekkürler,<br>EasyApp Ekibi</p>
        </div>
    </body>
</html>"
            };

            mailMessage.To.Add(user.Email);

            // Configure SMTP client
            using var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("yukeworking@gmail.com", "snvv fjdc xfre luaf"),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mailMessage);
        }


        [HttpGet]
        public IActionResult VerifyEmail()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> VerifyEmail(string verificationCode)
        {
            var userId = TempData["UserId"]?.ToString();
            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError("", "Session expired. Please register again.");
                return RedirectToAction("VerifyEmail");
            }
            TempData.Keep("UserId");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return RedirectToAction("Register");
            }

            // Kodun süresini kontrol et
            if (user.VerificationCodeExpires <= DateTime.UtcNow)
            {
                // Süre dolmuşsa kullanıcıyı sil
                await _userManager.DeleteAsync(user);
                ModelState.AddModelError("", "Verification code expired. Your registration has been canceled. Please register again.");
                return RedirectToAction("Register");
            }

            // Kodun doğruluğunu kontrol et
            if (user.EmailVerificationCode != verificationCode)
            {
                ModelState.AddModelError("", "Invalid verification code. Please try again.");
                return View(); // Hatalı kodda aynı sayfada kalır
            }

            // Doğruysa onaylama işlemini tamamla
            user.EmailConfirmed = true;
            user.EmailVerificationCode = null;
            user.VerificationCodeExpires = null;

            await _userManager.UpdateAsync(user);

            TempData["SuccessMessage"] = "Your email has been verified successfully.";
            return RedirectToAction("Index", "Authentication"); // Başarılı doğrulama sonrası giriş ekranına yönlendirme
        }





        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Authentication");
        }
    }
}

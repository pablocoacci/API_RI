using Application.Razor.AccountForms.Get.ResetPasswordForm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers.Razor
{
    [Route("/accountviews")]
    public class AccountFormsController : Controller
    {
        [AllowAnonymous]
        [HttpGet("resetPasswordForm")]
        public IActionResult ResetPasswordForm(string token, string email)
        {
            var model = new ResetPasswordFormModel()
            {
                Token = token,
                Email = email
            };

            return View(model);
        }
    }
}

using Mardul.VKBackup.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mardul.VKBackup.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly VKService vkService;

        public AuthenticationController(VKService vkService)
        {
            this.vkService = vkService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="twoFactorCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login(string userName, string password, string twoFactorCode)
        {
            await vkService.LoginTwoFactor(userName, password, twoFactorCode);
            return Ok();
        }
        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {

            await vkService.Logout();
            return Ok();
        }
    }
}

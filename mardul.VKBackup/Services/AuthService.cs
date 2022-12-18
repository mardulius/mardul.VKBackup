using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace Mardul.VKBackup.Services
{
    public class AuthService : IAuthService
    {
        private readonly VkApi VkService;

        public AuthService(VKService vkService) { VkService = vkService.VkApi; }
        public async Task Login(string username, string password)
        {
            await VkService.AuthorizeAsync(new ApiAuthParams
            {
                ApplicationId = 51481587,
                Login = username,
                Password = password,
                Settings = Settings.All,
            });
        }

        public async Task LoginTwoFactor(string username, string password, string twoFactorCode)
        {
            await VkService.AuthorizeAsync(new ApiAuthParams
            {
                ApplicationId = 51481587,
                Login = username,
                Password = password,
                Settings = Settings.All,
                TwoFactorAuthorization = () => twoFactorCode
            });
        }

        public void Logout()
        {
            VkService.LogOut();
        }
    }
}

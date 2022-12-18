using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;

namespace Mardul.VKBackup.Services
{
    public interface IAuthService
    {
        Task Login(string username, string password);
        Task LoginTwoFactor(string username, string password, string twoFactorCode);
        void Logout();
    }
}

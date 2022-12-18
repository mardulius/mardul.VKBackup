using System.Net;

namespace Mardul.VKBackup.Services
{
    public class PhotoSaveService : IPhotoSaveService
    {
        private readonly WebClient webClient;

        public PhotoSaveService(WebClient webClient)
        {
            this.webClient = webClient;
        }

        public async Task SavePhoto(Uri photoUrl, string filePath)
        {
           
              webClient.DownloadFileAsync(photoUrl, filePath);
           
        }
    }
}

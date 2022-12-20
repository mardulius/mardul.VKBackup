using Mardul.VKBackup.Dto;
using System.Net;

namespace Mardul.VKBackup.Services
{
    public class PhotoSaveService : IPhotoSaveService
    {
        private readonly VKService vkService;
        public PhotoSaveService(VKService vkService)
        {
            this.vkService = vkService;
        }

        public async Task SaveAllPhotos(List<PhotoAlbumDto> albums, string DirectoryPath)
        {
            foreach (var album in albums)
            {
                var photos = await vkService.GetPhotos(album.Id, album.Name);
                await this.SaveAllPhotosFromAlbum(photos, DirectoryPath);
            }
        }

        public async Task SaveAllPhotosFromAlbum(PhotoDto[] photos, string DirectoryPath)
        {
            int counter = 1;
            var path = $"{DirectoryPath}\\{photos.FirstOrDefault().AlbumName}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach(PhotoDto photo in photos)
            { 
                await this.SavePhoto(photo.Url, $"{path}\\{counter}.jpeg");
                counter++;
            }
        }

        public async Task SavePhoto(Uri photoUrl, string filePath)
        {
            WebClient wc = new WebClient();
            wc.DownloadFileAsync(photoUrl, filePath);
           
        }

    }
}

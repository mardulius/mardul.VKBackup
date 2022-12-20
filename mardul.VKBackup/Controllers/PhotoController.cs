using Mardul.VKBackup.Dto;
using Mardul.VKBackup.Services;
using Microsoft.AspNetCore.Mvc;
using VkNet.Model.Attachments;

namespace Mardul.VKBackup.Controllers
{
    public class PhotoController : Controller
    {
        private readonly VKService vkService;
        private readonly IPhotoSaveService photoSaveService;
        public PhotoController(VKService vkService, IPhotoSaveService photoSaveService)
        {
            this.vkService = vkService;
            this.photoSaveService = photoSaveService;
        }

        [HttpGet]
        [Route("get_photo_albums")]
        public async Task<IActionResult> GetPhotoAlbums()
        {
            var result = await vkService.GetPhotoAlbums();
            return Ok(result);
        }
        [HttpGet]
        [Route("get_photos")]
        public async Task<IActionResult> GetPhotos([FromQuery] long albumId, string albumName)
        {
            var result = await vkService.GetPhotos(albumId, albumName);
            return Ok(result);
        }
        [HttpPost]
        [Route("save_photo")]
        public async Task<IActionResult> SavePhoto(Uri photoUri, string filePath)
        {
            await photoSaveService.SavePhoto(photoUri, filePath);
            return Ok();
        }

        [HttpPost]
        [Route("save_all_photos_from_album")]
        public async Task<IActionResult> SaveAllPhotosFromAlbum(long albumId, string albumName, string DirectoryPath)
        {
            var photos = await vkService.GetPhotos(albumId, albumName);
            await photoSaveService.SaveAllPhotosFromAlbum(photos, DirectoryPath);
            return Ok();
        }

        [HttpPost]
        [Route("save_all_photos")]
        public async Task<IActionResult> SaveAllPhotosFromAlbum(string DirectoryPath)
        {
            var albums = await vkService.GetPhotoAlbums();
            await photoSaveService.SaveAllPhotos(albums, DirectoryPath);

            return Ok("Фотографии успешно загружены");
        }
    }
}

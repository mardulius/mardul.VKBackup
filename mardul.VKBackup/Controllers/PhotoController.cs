using Mardul.VKBackup.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetPhotos([FromQuery] int albumId)
        {
            var result = await vkService.GetPhotos(albumId);
            return Ok(result);
        }
        [HttpPost]
        [Route("save_photo")]
        public async Task<IActionResult> SavePhoto(Uri photoUri, string filePath)
        {
            await photoSaveService.SavePhoto(photoUri, filePath);
            return Ok();
        }
    }
}

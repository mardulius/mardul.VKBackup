using Mardul.VKBackup.Dto;

namespace Mardul.VKBackup.Services
{
    public interface IPhotoSaveService
    {
        Task SavePhoto(Uri photoId, string filePath);
        Task SaveAllPhotosFromAlbum(PhotoDto[] photos, string DirectoryPath);
        Task SaveAllPhotos(List<PhotoAlbumDto> albums, string DirectoryPath);
    }
}

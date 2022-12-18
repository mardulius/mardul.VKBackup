namespace Mardul.VKBackup.Services
{
    public interface IPhotoSaveService
    {
        Task SavePhoto(Uri photoId, string filePath);
    }
}

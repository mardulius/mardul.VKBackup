using Mardul.VKBackup.Dto;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace Mardul.VKBackup.Services
{
    public class VKService
    {
        public VkApi VkApi;

        public VKService()
        {
            VkApi = new VkApi();
        }
        public async Task Login(string username, string password)
        {
            await VkApi.AuthorizeAsync(new ApiAuthParams
            {
                ApplicationId = 51481587,
                Login = username,
                Password = password,
                Settings = Settings.All,
            });
        }
        public async Task LoginTwoFactor(string username, string password, string twoFactorCode)
        {
            await VkApi.AuthorizeAsync(new ApiAuthParams
            {
                ApplicationId = 51503542,
                Login = username,
                Password = password,
                Settings = Settings.All,
                TwoFactorAuthorization = () => twoFactorCode
            });
            Console.WriteLine(VkApi.Token);
        }

        public async Task Logout()
        {
            await VkApi.LogOutAsync();
        }

        public async Task<List<PhotoAlbumDto>> GetPhotoAlbums()
        {
            VkCollection<PhotoAlbum> vkAlbums = await VkApi.Photo.GetAlbumsAsync(new PhotoGetAlbumsParams
            {
            });
            List<PhotoAlbumDto> albums = new List<PhotoAlbumDto>();
            foreach (PhotoAlbum vkAlbum in vkAlbums)
            {
                albums.Add(new PhotoAlbumDto()
                {
                    Id = vkAlbum.Id,
                    Name = vkAlbum.Title,
                    DataCreated = vkAlbum.Created,
                    Description = vkAlbum.Description,
                    Size = vkAlbum.Size,
                });

            }
            return albums;
        }

        public async Task<PhotoDto[]> GetPhotos(long albumId)
        {
            VkCollection<Photo> vkPhotos = await VkApi.Photo.GetAsync(new PhotoGetParams
            {
                AlbumId = PhotoAlbumType.Id(albumId)
            });
            var photos = (from photo in vkPhotos
                          select new PhotoDto()
                          {
                              Id = photo.Id,
                              AlbumId = photo.AlbumId,
                              UserId = photo.UserId ?? 0,
                              OwnerId = photo.OwnerId ?? 0,
                              CreateDate = photo.CreateTime,
                              Description = photo.Text ?? "",
                              Url = photo.Sizes.LastOrDefault().Url
                          }).ToArray();
            return photos;
        }
    }
}

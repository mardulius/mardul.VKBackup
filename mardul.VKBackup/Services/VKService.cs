﻿using Mardul.VKBackup.Dto;
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
        private readonly VkApi VkApi;
        private readonly ulong applicationId; 
        public VKService(IConfiguration configuration)
        {
            VkApi = new VkApi();
            IConfigurationSection vkSettings = configuration.GetSection("VkSettings");
            this.applicationId = Convert.ToUInt64(vkSettings.GetSection("ApplivationId").Value);
        }
        public async Task Login(string username, string password)
        {
            await VkApi.AuthorizeAsync(new ApiAuthParams
            {
                ApplicationId = this.applicationId,
                Login = username,
                Password = password,
                Settings = Settings.All,
            });
        }
        public async Task<string> LoginTwoFactor(string username, string password, string twoFactorCode)
        {
            await VkApi.AuthorizeAsync(new ApiAuthParams
            {
                ApplicationId = 51503542,
                Login = username,
                Password = password,
                Settings = Settings.All,
                TwoFactorAuthorization = () => twoFactorCode
            });
            return VkApi.Token;
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

        public async Task<PhotoDto[]> GetPhotos(long albumId, string albumName)
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
                              AlbumName = albumName,
                              UserId = photo.UserId ?? 0,
                              OwnerId = photo.OwnerId ?? 0,
                              CreateDate = photo.CreateTime,
                              Description = photo.Text ?? "",
                              Url = photo.Sizes.LastOrDefault().Url
                          }).ToArray();
            return photos;
        }

        public async Task LoginToken(string token)
        {
           await VkApi.AuthorizeAsync(new ApiAuthParams
            {
                AccessToken = token
            });

        }
    }
}

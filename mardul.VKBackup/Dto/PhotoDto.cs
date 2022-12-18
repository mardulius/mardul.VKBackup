namespace Mardul.VKBackup.Dto
{
    public class PhotoDto
    {
        public long? Id { get; set; }
        public long? AlbumId { get; set; }
        public long? OwnerId { get; set; }
        public long? UserId { get; set; }
        public string? Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public Uri? Url { get; set; }

    }
}
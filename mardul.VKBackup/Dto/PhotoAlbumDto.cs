using System;
using System.Collections.Generic;

namespace Mardul.VKBackup.Dto
{
    public class PhotoAlbumDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DataCreated { get; set; }
        public int? Size { get; set; }

    }
}

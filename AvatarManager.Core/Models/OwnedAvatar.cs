﻿using System.ComponentModel;

namespace AvatarManager.Core.Models
{
    public class OwnedAvatar
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public string ImagePath { get; set; }
        [DefaultValue(null)]
        public string? DisplayName { get; set; }
    }
}

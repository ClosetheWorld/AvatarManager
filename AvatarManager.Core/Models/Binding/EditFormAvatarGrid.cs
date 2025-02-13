using System.Drawing;

namespace AvatarManager.Core.Models.Binding
{
    public class EditFormAvatarGrid
    {
        public bool IsSelected { get; set; }

        public Bitmap AvatarThumbnail { get; set; }

        public string AvatarName { get; set; }

        public string AvatarId { get; set; }
    }
}


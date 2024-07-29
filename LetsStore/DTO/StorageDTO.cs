using LetsStore.Models;

namespace LetsStore.DTO
{
    public class StorageDTO
    {
        public string FilePath { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

        public string StoredFileName { get; set; } = null!;

        public string StoredFileType { get; set; } = null!;

        public string? UploadedDevice { get; set; }

        public virtual ICollection<CategoryMapDTO> CategoryMapDTOs { get; set; } = new List<CategoryMapDTO>();

        public virtual ICollection<StorageMapDTO> StorageMapDTOs { get; set; } = new List<StorageMapDTO>();
    }
}

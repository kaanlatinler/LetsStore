using LetsStore.Models;

namespace LetsStore.DTO
{
    public class UserDTO
    {
        public string UserName { get; set; } = null!;

        public string UserLastName { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public string UserPassword { get; set; } = null!;

        public virtual ICollection<StorageMap> StorageMaps { get; set; } = new List<StorageMap>();
    }
}

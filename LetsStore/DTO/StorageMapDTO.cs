using LetsStore.Models;

namespace LetsStore.DTO
{
    public class StorageMapDTO
    {
        public Guid StorageId { get; set; }

        public Guid UserId { get; set; }

        public virtual StorageDTO StorageDTO { get; set; } = null!;

        public virtual UserDTO UserDTO { get; set; } = null!;
    }
}

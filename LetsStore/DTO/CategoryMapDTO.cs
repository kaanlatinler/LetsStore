using LetsStore.Models;

namespace LetsStore.DTO
{
    public class CategoryMapDTO
    {
        public Guid CatId { get; set; }

        public Guid StorageId { get; set; }

        public virtual FileCategoryDTO CatDTO { get; set; } = null!;

        public virtual StorageDTO StorageDTO { get; set; } = null!;
    }
}

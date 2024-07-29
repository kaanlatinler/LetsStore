using LetsStore.Models;

namespace LetsStore.DTO
{
    public class FileCategoryDTO
    {
        public string CatName { get; set; } = null!;

        public virtual ICollection<CategoryMapDTO> CategoryMapDTOs { get; set; } = new List<CategoryMapDTO>();
    }
}

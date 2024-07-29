using LetsStore.DTO;

namespace LetsStore.ViewModels
{
    public class AllFilesViewModel
    {
        public StorageDTO StorageDTO { get; set; }
        public List<StorageMapDTO> StorageMapDTO { get; set; }

        public List<CategoryMapDTO> CategoryMapDTO { get; set; }
        public List<FileCategoryDTO> FileCategoryDTO { get; set; }
    }
}

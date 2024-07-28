using System;
using System.Collections.Generic;

namespace LetsStore.Models;

public partial class Storage
{
    public Guid StorageId { get; set; }

    public string FilePath { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public string StoredFileName { get; set; } = null!;

    public string StoredFileType { get; set; } = null!;

    public string? UploadedDevice { get; set; }

    public virtual ICollection<CategoryMap> CategoryMaps { get; set; } = new List<CategoryMap>();

    public virtual ICollection<StorageMap> StorageMaps { get; set; } = new List<StorageMap>();
}

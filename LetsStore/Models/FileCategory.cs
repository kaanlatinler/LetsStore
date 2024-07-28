using System;
using System.Collections.Generic;

namespace LetsStore.Models;

public partial class FileCategory
{
    public Guid CatId { get; set; }

    public string CatName { get; set; } = null!;

    public virtual ICollection<CategoryMap> CategoryMaps { get; set; } = new List<CategoryMap>();
}

using System;
using System.Collections.Generic;

namespace LetsStore.Models;

public partial class CategoryMap
{
    public Guid CategoryMapId { get; set; }

    public Guid CatId { get; set; }

    public Guid StorageId { get; set; }

    public virtual FileCategory Cat { get; set; } = null!;

    public virtual Storage Storage { get; set; } = null!;
}

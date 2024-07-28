using System;
using System.Collections.Generic;

namespace LetsStore.Models;

public partial class StorageMap
{
    public Guid StorageMapId { get; set; }

    public Guid StorageId { get; set; }

    public Guid UserId { get; set; }

    public virtual Storage Storage { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

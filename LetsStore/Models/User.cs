using System;
using System.Collections.Generic;

namespace LetsStore.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserLastName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public virtual ICollection<StorageMap> StorageMaps { get; set; } = new List<StorageMap>();
}

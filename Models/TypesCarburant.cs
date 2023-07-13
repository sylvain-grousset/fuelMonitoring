using System;
using System.Collections.Generic;

namespace Fuel.Models;

public partial class TypesCarburant
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<PrixCarburantFrance> PrixCarburantFrances { get; set; } = new List<PrixCarburantFrance>();
}

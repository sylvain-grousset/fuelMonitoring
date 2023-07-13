using System;
using System.Collections.Generic;

namespace Fuel.Models;

public partial class PrixCarburantFrance
{
    public int IdPrix { get; set; }

    public DateOnly? Date { get; set; }

    public double? PrixMoyen { get; set; }

    public int? IdCarburant { get; set; }

    public virtual TypesCarburant? IdCarburantNavigation { get; set; }
}

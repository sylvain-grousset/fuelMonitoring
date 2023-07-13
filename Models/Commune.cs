using System;
using System.Collections.Generic;

namespace Fuel.Models;

public partial class Commune
{
    public int IdCommune { get; set; }

    public string? CodePostal { get; set; }

    public string? Ville { get; set; }
}

using System;
using System.Collections.Generic;

namespace Fuel.Models;

public partial class Histo
{
    public int Id { get; set; }

    public DateOnly? Date { get; set; }

    public double? Sp98 { get; set; }

    public double? Gazole { get; set; }

    public double? Sp95 { get; set; }

    public double? E85 { get; set; }
}

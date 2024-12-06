using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace asp_hydrometric_api_vs.Models;

public partial class HydrometricAnnualPeak
{
    public string Id { get; set; } = null!;

    public Point? Geom { get; set; }

    public string? PeakCodeEn { get; set; }

    public double? Peak { get; set; }

    public string? Identifier { get; set; }

    public string? SymbolFr { get; set; }

    public string? ProvTerrStateLoc { get; set; }

    public string? StationName { get; set; }

    public string? UnitsEn { get; set; }

    public string? TimezoneOffset { get; set; }

    public string? SymbolEn { get; set; }

    public string? StationNumber { get; set; }

    public DateTime? Date { get; set; }

    public string? UnitsFr { get; set; }

    public string? PeakCodeFr { get; set; }

    public string? DataTypeFr { get; set; }

    public string? DataTypeEn { get; set; }
}

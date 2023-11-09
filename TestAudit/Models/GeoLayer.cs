namespace TestAudit.Models;

public class GeoLayer
{
    public long? GeoLayerId { get; set; }
    public string? GeoLayerName { get; set; }
    public GeoObject? GeoObject { get; set; }
    public string? StatGeoObjectReport { get; set; }
}
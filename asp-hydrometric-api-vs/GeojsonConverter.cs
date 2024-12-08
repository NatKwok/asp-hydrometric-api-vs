using GeoJSON.Text;
using System.Text.Json;
using GeoJSON.Text.Feature;
using Newtonsoft.Json;

namespace asp_hydrometric_api_vs
{
    public class GeojsonConverter()
    {
        public static string CreateFeatureCollection(List<Feature> features)
        {
            var featureCollection = new FeatureCollection(features);
            return JsonConvert.SerializeObject(featureCollection);
        }

        public static Feature CreatePointFeature(double latitude, double longitude, Dictionary<string, object> properties)
        {
            var point = new GeoJSON.Text.Geometry.Point(new GeoJSON.Text.Geometry.Position(latitude, longitude));
            return new Feature(point, properties);
        }

        public static Feature CreateLineStringFeature(List<Position> coordinates, Dictionary<string, object> properties)
        {
            var lineString = new GeoJSON.Text.Geometry.LineString(new GeoJSON.Text.Ge)
            return new Feature(lineString, properties);
        }

        public static Feature CreatePolygonFeature(List<LineString> coordinates, Dictionary<string, object> properties)
        {
            var polygon = new Polygon(coordinates);
            return new Feature(polygon, properties);
        }
    }
}

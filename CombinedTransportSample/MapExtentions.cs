using Ptv.XServer.Controls.Map;
using Ptv.XServer.Controls.Map.Layers.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinedTransportSample
{
    public static class MapExtentions
    {
        public static void ZoomToAll(this Map map)
        {
            var rectangle = map.Layers.GetMapRectangleFromShapeLayers();
            if (rectangle == null) return;

            map.SetEnvelope(rectangle, (map.Layers.First(l => l.GetType() == typeof(ShapeLayer)) as ShapeLayer).SpatialReferenceId);
        }

        public static MapRectangle GetMapRectangleFromShapeLayers(this LayerCollection layers)
        {
            var shapeLayers = layers.Where(l => l.GetType() == typeof(ShapeLayer)).Select(l => l as ShapeLayer);
            if (shapeLayers.Count() == 0) return null;

            double up = double.MinValue, down = double.MaxValue, left = double.MaxValue, rigth = double.MinValue;

            foreach (var layer in shapeLayers)
            {
                if (layer.Shapes.Count == 0) continue;
                //up = Math.Max(up, layer.Shapes.Where(s => s.GetType().IsSubclassOf(typeof(MapPolylineBase))).Select(l => l as MapPolylineBase).Select(mlb => mlb.GetMaxY()).Max());
                //down = Math.Min(up, layer.Shapes.Where(s => s.GetType().IsSubclassOf(typeof(MapPolylineBase))).Select(l => l as MapPolylineBase).Select(mlb => mlb.GetMinY()).Min());
                //left = Math.Min(up, layer.Shapes.Where(s => s.GetType().IsSubclassOf(typeof(MapPolylineBase))).Select(l => l as MapPolylineBase).Select(mlb => mlb.GetMinX()).Min());
                //rigth = Math.Max(up, layer.Shapes.Where(s => s.GetType().IsSubclassOf(typeof(MapPolylineBase))).Select(l => l as MapPolylineBase).Select(mlb => mlb.GetMaxX()).Max());

                up = Math.Max(up, layer.Shapes.Select(s => ShapeCanvas.GetLocation(s).Y).Max());
                down = Math.Min(down, layer.Shapes.Select(s => ShapeCanvas.GetLocation(s).Y).Min());
                left = Math.Min(left, layer.Shapes.Select(s => ShapeCanvas.GetLocation(s).X).Min());
                rigth = Math.Max(rigth, layer.Shapes.Select(s => ShapeCanvas.GetLocation(s).X).Max());
            }

            if (up == double.MinValue) return null;

            return
                new Ptv.XServer.Controls.Map.MapRectangle()
                {
                    North = up,
                    South = down,
                    West = left,
                    East = rigth,
                }.Inflate(1.05, 1.05);
        }

        public static double GetMaxX(this MapPolylineBase mapPolyLine)
        {
            return mapPolyLine.Points.Select(p => p.X).Max();
        }

        public static double GetMaxY(this MapPolylineBase mapPolyLine)
        {
            return mapPolyLine.Points.Select(p => p.Y).Max();
        }
        public static double GetMinX(this MapPolylineBase mapPolyLine)
        {
            return mapPolyLine.Points.Select(p => p.X).Min();
        }
        public static double GetMinY(this MapPolylineBase mapPolyLine)
        {
            return mapPolyLine.Points.Select(p => p.Y).Max();
        }
    }
}

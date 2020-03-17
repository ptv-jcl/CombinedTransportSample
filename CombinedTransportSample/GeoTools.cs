using XServers.XLocate;
using System;

namespace Geotools
{
    public enum CoordinateFormat
    {
        /// <summary>
        /// Lat/Lon EPGS:4326
        /// </summary>
        Wgs84,

        /// <summary>
        /// PTV Spherical Mercator EPSG:505456
        /// </summary>
        Ptv_Mercator,

        /// <summary>
        /// PTV Geodecimal (WGS * 100000)
        /// </summary>
        Ptv_Geodecimal,

        /// <summary>
        /// PTV Smart Units
        /// </summary>
        Ptv_SmartUnits,

        /// <summary>
        /// Spherical Web Mercator Projection
        /// (aka Google Mercator) EPSG:900913
        /// </summary>
        Web_Mercator,
    }

    public static class AirLineDistanceCalculator
    {
        // Calcluate airline distance based on mercator distance.
        // This approximation formula is sufficiently accurate for
        // our needs for distances of up to 600 km and
        // 80° latitude (the error is never more than 5% even for
        // extreme values).
        public static double CalculateUsingWGS84(PlainPoint start, PlainPoint end)
        {
            var startMerc = GeoTransform.Trans(CoordinateFormat.Wgs84, CoordinateFormat.Ptv_Mercator, start);
            var endMerc = GeoTransform.Trans(CoordinateFormat.Wgs84, CoordinateFormat.Ptv_Mercator, end);

            double dist = Math.Sqrt((startMerc.x - endMerc.x) * (startMerc.x - endMerc.x) + (startMerc.y - endMerc.y) * (startMerc.y - endMerc.y));
            return dist * Math.Cos(start.y * Math.PI / 180.0);
        }

        public static double CalculateUsingMercator(PlainPoint start, PlainPoint end)
        {
            var startWgs84 = GeoTransform.Trans(CoordinateFormat.Ptv_Mercator, CoordinateFormat.Wgs84, start);

            double dist = Math.Sqrt((start.x - end.x) * (start.x - end.x) + (start.y - end.y) * (start.y - end.y));
            return dist * Math.Cos(startWgs84.y * Math.PI / 180.0);
        }
    }

    public static class GeoTransform
    {
        public static PlainPoint Trans(CoordinateFormat inFormat, CoordinateFormat outFormat, PlainPoint point)
        {
            // int == out -> return
            if (inFormat == outFormat)
                return point;

            // direct transformations
            if (inFormat == CoordinateFormat.Ptv_Geodecimal && outFormat == CoordinateFormat.Wgs84)
                return Geodecimal_2_WGS84(point);

            if (inFormat == CoordinateFormat.Wgs84 && outFormat == CoordinateFormat.Ptv_Geodecimal)
                return WGS84_2_Geodecimal(point);

            if (inFormat == CoordinateFormat.Ptv_Mercator && outFormat == CoordinateFormat.Ptv_SmartUnits)
                return Mercator_2_SmartUnits(point);

            if (inFormat == CoordinateFormat.Ptv_SmartUnits && outFormat == CoordinateFormat.Ptv_Mercator)
                return SmartUnits_2_Mercator(point);

            if (inFormat == CoordinateFormat.Ptv_Mercator && outFormat == CoordinateFormat.Wgs84)
                return SphereMercator_2_Wgs(point, Ptv_Radius);

            if (inFormat == CoordinateFormat.Wgs84 && outFormat == CoordinateFormat.Ptv_Mercator)
                return Wgs_2_SphereMercator(point, Ptv_Radius);

            if (inFormat == CoordinateFormat.Web_Mercator && outFormat == CoordinateFormat.Wgs84)
                return SphereMercator_2_Wgs(point, Google_Radius);

            if (inFormat == CoordinateFormat.Wgs84 && outFormat == CoordinateFormat.Web_Mercator)
                return Wgs_2_SphereMercator(point, Google_Radius);

            if (inFormat == CoordinateFormat.Ptv_Mercator && outFormat == CoordinateFormat.Web_Mercator)
                return Ptv_2_Google(point);

            if (inFormat == CoordinateFormat.Web_Mercator && outFormat == CoordinateFormat.Ptv_Mercator)
                return Google_2_Ptv(point);

            // transitive transformations
            if (inFormat == CoordinateFormat.Ptv_SmartUnits)
                return Trans(CoordinateFormat.Ptv_Mercator, outFormat, SmartUnits_2_Mercator(point));

            if (outFormat == CoordinateFormat.Ptv_SmartUnits)
                return Mercator_2_SmartUnits(Trans(inFormat, CoordinateFormat.Ptv_Mercator, point));

            if (inFormat == CoordinateFormat.Ptv_Geodecimal)
                return Trans(CoordinateFormat.Wgs84, outFormat, Geodecimal_2_WGS84(point));

            if (outFormat == CoordinateFormat.Ptv_Geodecimal)
                return WGS84_2_Geodecimal(Trans(inFormat, CoordinateFormat.Wgs84, point));

            // this should not happen
            throw new NotImplementedException(string.Format("transformation not implemented for {0} to {1}",
              inFormat.ToString(), outFormat.ToString()));
        }

        #region geographic formats

        public static PlainPoint Geodecimal_2_WGS84(PlainPoint point)
        {
            return new PlainPoint { x = point.x / 100000.0, y = point.y / 100000.0 };
        }

        public static PlainPoint WGS84_2_Geodecimal(PlainPoint point)
        {
            return new PlainPoint { x = point.x * 100000.0, y = point.y * 100000.0 };
        }

        #endregion geographic formats

        #region smart units

        private const double SMARTFACTOR = 4.809543;
        private const int SMART_OFFSET = 20015087;

        public static PlainPoint SmartUnits_2_Mercator(PlainPoint point)
        {
            return new PlainPoint
            {
                x = (point.x * SMARTFACTOR) - SMART_OFFSET,
                y = (point.y * SMARTFACTOR) - SMART_OFFSET,
            };
        }

        public static PlainPoint Mercator_2_SmartUnits(PlainPoint point)
        {
            return new PlainPoint
            {
                x = (point.x + SMART_OFFSET) / SMARTFACTOR,
                y = (point.y + SMART_OFFSET) / SMARTFACTOR
            };
        }

        #endregion smart units

        #region Spherical Mercator

        private const double Ptv_Radius = 6371000.0;
        private const double Google_Radius = 6378137.0;

        public static PlainPoint Ptv_2_Google(PlainPoint point)
        {
            return new PlainPoint { x = point.x / Ptv_Radius * Google_Radius, y = point.y / Ptv_Radius * Google_Radius };
        }

        public static PlainPoint Google_2_Ptv(PlainPoint point)
        {
            return new PlainPoint { x = point.x / Google_Radius * Ptv_Radius, y = point.y / Google_Radius * Ptv_Radius };
        }

        public static PlainPoint Wgs_2_SphereMercator(PlainPoint point, double earthRadius)
        {
            double x = earthRadius * point.x * Math.PI / 180.0;
            double y = earthRadius * Math.Log(Math.Tan(Math.PI / 4.0 + point.y * Math.PI / 360.0));

            return new PlainPoint { x = x, y = y };
        }

        public static PlainPoint SphereMercator_2_Wgs(PlainPoint point, double earthRadius)
        {
            double x = (180.0 / Math.PI) * (point.x / earthRadius);
            double y = (360 / Math.PI) * (Math.Atan(Math.Exp(point.y / earthRadius)) - (Math.PI / 4));

            return new PlainPoint { x = x, y = y };
        }

        #endregion Spherical Mercator
    }
}
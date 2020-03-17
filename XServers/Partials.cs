using System;
using System.Linq;
using System.Xml.Serialization;

namespace XServers.XLocate
{
    public partial class ResultCombinedTransport
    {
        [XmlIgnore]
        public string StartName { get { return start?.name; } set { } }

        [XmlIgnore]
        public string DestinationName { get { return destination?.name; } set { } }

        [XmlIgnore]
        public string TruckBlock { get { return wrappedCombinedTransportFields.FirstOrDefault(f => f.field == ResultField.ISBLOCKEDFORTRUCKS)?.value; } set { } }

        [XmlIgnore]
        public string HazardousBlock { get { return wrappedCombinedTransportFields.FirstOrDefault(f => f.field == ResultField.ISBLOCKEDFORHAZARDOUSGOODS)?.value; } set { } }

        [XmlIgnore]
        public string CombustibleBlock { get { return wrappedCombinedTransportFields.FirstOrDefault(f => f.field == ResultField.ISBLOCKEDFORCOMBUSTIBLEGOODS)?.value; } set { } }

        [XmlIgnore]
        public string Time { get { return TimeSpan.FromSeconds(duration).ToString(); } set { } }
    }
}

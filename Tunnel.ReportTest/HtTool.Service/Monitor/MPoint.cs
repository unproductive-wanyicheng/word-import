using System;
using System.Runtime.Serialization;

namespace HtTool.Service.Monitor
{
    [Serializable]
    [DataContract]
    public class MPoint
    {
        [DataMember]
        public float X { get; set; }

        [DataMember]
        public float Y { get; set; }
    }

    [Serializable]
    [DataContract]
    public class ChartPoint
    {
        [DataMember]
        public DateTime X { get; set; }

        [DataMember]
        public float Y { get; set; }
    }
}

using System.Collections.Generic;
using System.Data;

namespace HtTool.Service.Monitor
{
    public class Section
    {
        public string name { set; get; }

        public string EquipId { get; set; }

        /// <summary>
        /// 仪器常数
        /// </summary>
        public string gStrEquipConst = "0";
        /// <summary>
        /// 预留变形量 Uo
        /// </summary>
        public string gStrU0 = "180";

        public List<Line> lines = new List<Line>();

      

        public Section(string name)
        {
            this.name = name;
        }
    }

    public class Line
    {
        private string p;

        public Line(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }
        public string name { set; get; }
        public DataTable dataTable { set; get; }

        //public ChartPoint[] ChartData { get; set; }
    }


}

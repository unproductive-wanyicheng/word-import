using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tunnel.Word.Table
{
    /// <summary>
    /// 埋设断面表
    /// </summary>
    public class ChuzhiGZCModel : TableDataModel
    {
        /// <summary>
        /// 埋设断面表
        /// </summary>
        public ChuzhiGZCModel() { }
        public List<ChuzhiGZCDataModel> ChuzhiGZCDatas { get; set; }
    }

    public class ChuzhiGZCDataModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string Index { get; set; }
        /// <summary>
        /// 里程
        /// </summary>
        public string ParamsMileage { get; set; }
        /// <summary>
        /// 围岩级别
        /// </summary>
        public string SurroundingRockLevel { get; set; }
        /// <summary>
        /// 支护类型
        /// </summary>
        public string ProtectType { get; set; }
        
        /// <summary>
        /// 钢支撑设计榀数
        /// </summary>
        public string DesginNums { get; set; }
        /// <summary>
        /// 钢支撑实际榀数
        /// </summary>
        public string FactNums { get; set; }
        /// <summary>
        /// 钢支撑设计间距
        /// </summary>
        public string DesginSpacing { get; set; }
        /// <summary>
        /// 钢支撑实际间距
        /// </summary>
        public string FactSpacing { get; set; }
    }
}

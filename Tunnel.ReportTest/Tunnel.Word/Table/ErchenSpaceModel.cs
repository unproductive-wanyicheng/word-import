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
    public class ErchenSpaceModel : TableDataModel
    {
        /// <summary>
        /// 埋设断面表
        /// </summary>
        public ErchenSpaceModel() { }
        public List<ErchenSpaceDataModel> ErchenSpaceDatas { get; set; }
    }

    public class ErchenSpaceDataModel
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
        public string FactMax { get; set; }
        /// <summary>
        /// 钢支撑设计间距
        /// </summary>
        public string FactMin { get; set; }
        /// <summary>
        /// 钢支撑实际间距
        /// </summary>
        public string FactAve { get; set; }
        /// <summary>
        ///  备注
        /// </summary>
        public string Remark { get; set; }
    }
}


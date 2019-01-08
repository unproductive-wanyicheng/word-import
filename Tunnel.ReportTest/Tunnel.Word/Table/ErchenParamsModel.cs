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
    public class ErchenParamsModel : TableDataModel
    {
        /// <summary>
        /// 埋设断面表
        /// </summary>
        public ErchenParamsModel() { }
        public List<ErchenParamsDataModel> ErchenParamsDatas { get; set; }
    }

    public class ErchenParamsDataModel
    {
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
        /// 二次衬砌厚度
        /// </summary>
        public string ErchenThickness { get; set; }
        /// <summary>
        /// 衬砌钢筋间距
        /// </summary>
        public string ErchenSpacingOfSteelSupport { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}


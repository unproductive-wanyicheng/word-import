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
    public class ChuzhiParamsModel : TableDataModel
    {
        /// <summary>
        /// 埋设断面表
        /// </summary>
        public ChuzhiParamsModel() { }
        public List<ChuzhiParamsDataModel> ChuzhiParamsDatas { get; set; }
    }

    public class ChuzhiParamsDataModel
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
        /// 喷射混凝土厚度
        /// </summary>
        public string ShotcreteThickness { get; set; }
        /// <summary>
        /// 钢支撑间距
        /// </summary>
        public string SpacingOfSteelSupport { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}


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
    public class ErchenDefectModel : TableDataModel
    {
        /// <summary>
        /// 埋设断面表
        /// </summary>
        public ErchenDefectModel() { }
        public List<ErchenDefectDataModel> ErchenDefectDatas { get; set; }
    }

    public class ErchenDefectDataModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string Index { get; set; }
        /// <summary>
        /// 测线位置
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// <summary>
        /// 里程
        /// </summary>
        public string ParamsMileage { get; set; }
        /// <summary>
        /// 缺陷长度
        /// </summary>
        public string BadLength { get; set; }
        /// <summary>
        /// 缺陷类型
        /// </summary>
        public string BadType { get; set; }
        /// <summary>
        /// 缺陷深度
        /// </summary>
        public string BadDeepth { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}



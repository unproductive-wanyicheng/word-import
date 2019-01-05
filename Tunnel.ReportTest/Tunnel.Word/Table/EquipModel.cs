using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tunnel.Word.Table
{
    /// <summary>
    /// 仪器设备统计表
    /// </summary>
    public class EquipModel:TableDataModel
    {
        /// <summary>
        /// 仪器设备统计数据
        /// </summary>
        public List<EquipDataModel> EquipDatas { get; set; }
    }

    public class EquipDataModel
    {
        /// <summary>
        /// 设备名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string EquipModel { get; set; }

        /// <summary>
        /// 厂家
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public string Accuracy { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}

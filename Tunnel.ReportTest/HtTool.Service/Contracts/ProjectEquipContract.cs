using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtTool.Service.Contracts
{
    /// <summary>
    /// 项目仪器设备表
    /// </summary>
    public class ProjectEquipContract
    {
        public Guid Id { get; set; }


        public DateTime CreateTime { get; set; }

        public string CreateBy { get; set; }

        public Guid ProjectId { get; set; }

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

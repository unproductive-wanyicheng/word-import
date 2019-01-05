using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtTool.Service.Contracts
{
    /// <summary>
    /// 支护观察记录表
    /// </summary>
    public class RecordContract
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }

        /// <summary>
        /// 隧道id
        /// </summary>
        public Guid TunnelId { get; set; }

        /// <summary>
        /// 隧道名称
        /// </summary>
        public string TunnelName { get; set; }

        /// <summary>
        /// 进口出口
        /// </summary>
        public string TunnelDesc { get; set; }

        /// <summary>
        /// 左幅、右幅
        /// </summary>
        public string LrDesc { get; set; }

        /// <summary>
        /// 里程桩号
        /// </summary>
        public string SectionName { get; set; }

        /// <summary>
        /// 观测时间
        /// </summary>
        public DateTime ObservationDate { get; set; }

        /// <summary>
        /// 观测人
        /// </summary>
        public string ObservationMan { get; set; }

        /// <summary>
        /// 地质素描图
        /// </summary>
        public string GeologicalSketchMap { get; set; }

        /// <summary>
        /// 掌子面照片
        /// </summary>
        /// <returns></returns>
        public string PhotoPalm { get; set; }

        /// <summary>
        /// 地质描述
        /// </summary>
        public string GeologicalDescription { get; set; }

        /// <summary>
        /// 支护状况描述
        /// </summary>
        public string DescriptionSupportCondition { get; set; }

        /// <summary>
        /// 边仰坡及地表裂缝描述
        /// </summary>
        public string SurfaceCrackDescription { get; set; }
    }
}

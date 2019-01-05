using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HtTool.Service.Contracts
{
    [Serializable]
    [DataContract]
    public class TunnelInfoContract
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        [DataMember]
        public string GeoInfoName { get; set; }

        /// <summary>
        /// 区域code
        /// </summary>
        [DataMember]
        public string GeoInfoCode { get; set; }

        /// <summary>
        /// 隧道ID，系统
        /// </summary>
        [DataMember]
        public System.Guid TunnelId { get; set; }
        /// <summary>
        /// 所属线路ID
        /// </summary>
        [DataMember]
        public Guid RoadlineBaseInfoId { get; set; }

        /// <summary>
        /// 所属工程项目ID
        /// </summary>
        [DataMember]
        public Guid? ProjectId { get; set; }

        /// <summary>
        /// 所属工程项目名称
        /// </summary>
        [DataMember]
        public string ProjectName { get; set; }

        /// <summary>
        /// 所属线路code
        /// </summary>
        [DataMember]
        public string RoadlineCode { get; set; }
        /// <summary>
        /// 所属线路name
        /// </summary>
        [DataMember]
        public string RoadlineName { get; set; }
        /// <summary>
        /// 隧道Code
        /// </summary>
        [DataMember]
        public string TunnelCode { get; set; }
        /// <summary>
        /// 隧道名称
        /// </summary>
        [DataMember]
        public string TunnelName { get; set; }
        /// <summary>
        /// 区域id
        /// </summary>
        [DataMember]
        public System.Guid GeoInfoId { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>
        [DataMember]
        public System.Guid DepartmentId { get; set; }
        /// <summary>
        /// 左/右隧道
        /// </summary>
        [DataMember]
        public int TunnelRightLeft { get; set; }
        /// <summary>
        /// 设计单位
        /// </summary>
        [DataMember]
        public string DesignCompany { get; set; }
        /// <summary>
        /// 维护单位
        /// </summary>
        [DataMember]
        public string MaintOrg { get; set; }
        /// <summary>
        /// 监理单位
        /// </summary>
        [DataMember]
        public string SupervCompany { get; set; }
        /// <summary>
        /// 建设单位
        /// </summary>
        [DataMember]
        public string BuilderCompany { get; set; }
        /// <summary>
        /// 管理部门code
        /// </summary>
        [DataMember]
        public string DeptInfoCode { get; set; }
        /// <summary>
        /// 管理部门名称
        /// </summary>
        [DataMember]
        public string DeptInfoName { get; set; }
       
        [DataMember]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 隧道长度
        /// </summary>
        [DataMember]
        public System.Nullable<decimal> TunnelLength { get; set; }
        /// <summary>
        /// 隧道起点桩号
        /// </summary>
        [DataMember]

        public string StartPileno { get; set; }
        /// <summary>
        /// 隧道中心桩号
        /// </summary>
        [DataMember]

        public string CenterPileno { get; set; }
    
        /// <summary>
        /// 隧道高度
        /// </summary>
        [DataMember]
        public System.Nullable<decimal> TunnelHeight { get; set; }
        /// <summary>
        /// 隧道宽度
        /// </summary>
        [DataMember]
        public System.Nullable<decimal> TunnelWidth { get; set; }

        /// <summary>
        /// 隧道结束桩号
        /// </summary>
        [DataMember]
        public string EndPileno { get; set; }

        /// <summary>
        /// 线路简介
        /// </summary>
        public string RoadAbout { get; set; }

        /// <summary>
        /// 隧道简介
        /// </summary>
        public string TunnelAbout { get; set; }

        /// <summary>
        /// 任务来源
        /// </summary>
        public string TaskSource { get; set; }

        /// <summary>
        /// 合同段
        /// </summary>
        public string ContractSection { get; set; }
    }
}

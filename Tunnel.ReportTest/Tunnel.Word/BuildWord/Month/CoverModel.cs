using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunnel.Word.Model;

namespace Tunnel.Word.BuildWord.Month
{
    /// <summary>
    /// 封面模型
    /// </summary>
    public class CoverModel
    {
        public CoverModel()
        {
            this.MonitoringCompany = "贵州省交通建设工程检测中心有限责任公司";
            this.ProjectDepartment = "云南保泸高速公路隧道检测第一合同项目经理部";
            this.CreateDate = DateTime.Now.ToString("yyyy年M月d日");

        }
        /// <summary>
        /// P1_报告编号 
        /// GJJCZX-BLGS-LYJKLC-JK-001
        /// </summary>
        [WordMark(MarkName = "P1_报告编号")]
        public string ReportNumber { get; set; }

        /// <summary>
        /// P1_项目名称
        /// 云南保山至泸水高速公路老营特长隧道
        /// </summary>
        [WordMark(MarkName = "P1_项目名称")]
        public string ProjectName { get; set; }

        /// <summary>
        /// P1_合同段
        ///       土建S1合同         
        /// </summary>
        [WordMark(MarkName = "P1_合同段")]
        public string ContractSection { get; set; }

        /// <summary>
        /// P1_隧道名称
        ///   老营特长隧道（进口端）  
        /// </summary>
        [WordMark(MarkName = "P1_隧道名称")]
        public string TunnelName { get; set; }

        /// <summary>
        /// P1_监测范围1
        ///  左幅（ZK1+558～ZK1+623） 
        /// </summary>
        [WordMark(MarkName = "P1_监测范围1")]
        public string MonitoringRange1 { get; set; }
        /// <summary>
        /// P1_监测范围2
        ///  右幅（K1+466～K1+556）   
        /// </summary>
        [WordMark(MarkName = "P1_监测范围2")]
        public string MonitoringRange2 { get; set; }

        /// <summary>
        /// P1_监测日期
        ///   2016.3.28～2016.4.28    
        /// </summary>
        [WordMark(MarkName = "P1_监测日期")]
        public string MonitoringDateRange { get; set; }

        /// <summary>
        /// P1_监测公司  P2_监测公司  P3_监测公司
        /// 贵州省交通建设工程检测中心有限责任公司
        /// </summary>
        [WordMark(MarkName = "P1_监测公司,P2_监测公司,P3_监测公司")]
        public string MonitoringCompany { get; set; }

        /// <summary>
        /// P1_项目部  P3_项目部
        /// 云南保泸高速公路隧道检测第一合同项目经理部
        /// </summary>
        [WordMark(MarkName = "P1_项目部,P3_项目部")]
        public string ProjectDepartment { get; set; }

        /// <summary>
        /// P3_日期
        /// 2016年4月28日
        /// </summary>
        [WordMark(MarkName = "P3_日期")]
        public string CreateDate { get; set; }
    }
}

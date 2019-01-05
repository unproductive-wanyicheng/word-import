using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HtTool.Service.Contracts
{
    public class DicContract
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Belong { get; set; }

        [DataMember]
        public int? SOrder { get; set; }

        /// <summary>
        /// 用于标注道路类型或道路等级的标示编码
        /// </summary>
        [DataMember]
        public int? Code { get; set; }
    }
}

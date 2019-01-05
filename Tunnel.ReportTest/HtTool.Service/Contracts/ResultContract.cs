using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtTool.Service.Contracts
{
    public class ResultContract
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public object Data { get; set; }
    }
}

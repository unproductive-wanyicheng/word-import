using System;
using System.Collections.Generic;
using System.Text;

namespace HtTool.Service.Contracts
{
    [Serializable]
    public class ResponseContract<T>
    {
        private int _code = 0;

        public int Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _message = "success";

     
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private double _vs = 1.0;

      
        public double Version
        {
            get { return _vs; }
            set { _vs = value; }
        }

        private string _cTime = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm.fff");

       
        public string LastUpdateTime
        {
            get { return _cTime; }
            set { _cTime = value; }
        }

        
        public T Data { get; set; }
    }
}

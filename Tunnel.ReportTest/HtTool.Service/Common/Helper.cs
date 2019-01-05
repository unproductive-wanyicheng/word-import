using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HtTool.Service.Common
{
    public static class Helper
    {
        public static bool IsNotDoubleChar(char key)
        {
            //===48代表0，57代表9，8代表空格，46代表小数点 
            if ((key < 48 || key > 57) && (key != 8) && (key != 46))
                return true;
            else
                return false;
        }

        public static bool IsNotDoubleAndCopy(char key)
        {
            // 允许输入:数字、退格键(8)、全选(1)、复制(3)、粘贴(22)、46代表小数点 
            if (!Char.IsDigit(key) && key != 8 &&
            key != 1 && key != 3 && key != 22 && key != 46)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 小数
        /// </summary>
        /// <param name="txtstr"></param>
        /// <returns></returns>
        public static bool IsDouble(string txtstr)
        {
            var reg = new Regex(@"^[-]?\d+[.]?\d*$");
            var str = txtstr;
            var sb = new StringBuilder();
            if (reg.IsMatch(str))
            {
                return true;
            }
            return false;
        }
    }
}
